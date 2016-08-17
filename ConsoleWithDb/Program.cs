using System;
using System.Collections.Generic;
// using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConsoleWithDb
{

    public class StudentContext : DbContext
    {

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;database=efStudents;uid=root;pwd=root;");
        
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Program
    {

        public static void Main()
        {

            using(var db = new StudentContext())
            {

                db.Database.EnsureCreated();

                Console.WriteLine("Welcome to the Cruddy Console!");
                string choice;
                do
                {
                    Console.WriteLine("Would you like to 'create', 'read', 'readall', 'update', or 'destroy'? ('exit' to leave)");
                    choice = Console.ReadLine();
                    switch(choice)
                    {
                        case "create":
                            Create(db);
                            break;
                        case "read":
                            Show(db);
                            break;
                        case "readall":
                            Index(db);
                            break;
                        case "update":
                            Update(db);
                            break;
                        case "exit":
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Not a valid command");
                            break;
                    }
                } while (choice != "exit");
            }
        }

        public static void Create(StudentContext db)
        {
            Console.WriteLine("Let's make a new student!");
            Console.Write("Student's first name: ");
            string First_Name = Console.ReadLine();
            Console.Write("Student's last name: ");
            string Last_Name = Console.ReadLine();
            db.Add(new Student { FirstName = First_Name, LastName = Last_Name });
            db.SaveChanges();
        }

        public static void Show(StudentContext db)
        {
            Console.WriteLine("What is the ID of the student you would like to see?");
            string idInput = Console.ReadLine();
            try
            {
                int studentId = Int32.Parse(idInput);
                Student theStudent = db.Students.Single(student => student.StudentId == studentId);
                Console.WriteLine($"{theStudent.FirstName} {theStudent.LastName}");
            }
            catch
            {
                Console.WriteLine("I couldn't find that student");
            }
        }

        public static void Index(StudentContext db)
        {
            foreach(var student in db.Students)
            {
                Console.WriteLine($"{student.StudentId}: {student.FirstName} {student.LastName}");
            }
        }

        public static void Update(StudentContext db)
        {
            Console.WriteLine("What is the ID of the student you would like to update?");
            string idInput = Console.ReadLine();
            try
            {
                int studentId = Int32.Parse(idInput);
                Student theStudent = db.Students.Single(student => student.StudentId == studentId);
                Console.Write("Student's new first name: ");
                string First_Name = Console.ReadLine();
                Console.Write("Student's new last name: ");
                string Last_Name = Console.ReadLine();
                theStudent.FirstName = First_Name;
                theStudent.LastName = Last_Name;
                db.SaveChanges();
            }
            catch
            {
                Console.WriteLine("I couldn't find that student");
            }
        }
    }
}