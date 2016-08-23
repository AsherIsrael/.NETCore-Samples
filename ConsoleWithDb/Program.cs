using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConsoleWithDb
{

    // StudentContext extends DbContext, which knows how to translate
    // our database content into simple objects that are easy to work with,
    // and vice versa.
    public class StudentContext : DbContext
    {

        // A DbSet represents a table in our connected database.
        // Each table we want to use would need its own DbSet declaration.
        public DbSet<Student> Students { get; set; }

        // Here we configure the StudentContext to connect to our MySql database.
        // Change the database name, uid(UserID), and pwd(password) as needed.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;database=efstudents;uid=root;pwd=root;");
                // .UseMySql(@"Server=localhost;database=<DatabaseName>;uid=<UserID>;pwd=<Password>;");
        
    }

    // For each table we want to interact with, we need a local model class that
    // matches the shape of that table's data.
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class Program
    {

        // The Main method is the starting point for our application, where execution begins.
        public static void Main()
        {

            // We instantiate our database in a using block so it will be automatically cleaned up when we're
            // done with it.
            using(var db = new StudentContext())
            {

                // db.Database.EnsureCreated();

                Console.WriteLine("Welcome to the CRUDdy Console!");
                string choice = "init";
                while(choice != "exit")
                {
                    Console.WriteLine("Would you like to 'create', 'read', 'readall', 'update', or 'destroy'? ('exit' to leave)");
                    // Read in the user's choice and run the appropriate method, passing our DbContext along.
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
                        case "destroy":
                            Destroy(db);
                            break;
                        case "exit":
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Not a valid command");
                            break;
                    }
                }
            }
        }

        // This method allows the user to create a new student.
        //
        // Accepts a StudentContext to interact with.
        public static void Create(StudentContext db)
        {
            Console.WriteLine("Let's make a new student!");
            Console.Write("Student's first name: ");
            string First_Name = Console.ReadLine();
            Console.Write("Student's last name: ");
            string Last_Name = Console.ReadLine();
            // Create a new Student object.
            db.Add(new Student { FirstName = First_Name, LastName = Last_Name });
            // Translate the Student object into a database entry.
            db.SaveChanges();
        }

        // This method allows the user to choose a student to view.
        //
        // Accepts a StudentContext to interact with.
        public static void Show(StudentContext db)
        {
            Console.WriteLine("What is the ID of the student you would like to see?");
            string idInput = Console.ReadLine();
            try
            {
                // Parse the user's input into an integer.
                int studentId = int.Parse(idInput);
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
            foreach(Student student in db.Students)
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
                int studentId = int.Parse(idInput);
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

        public static void Destroy(StudentContext db)
        {
            Console.WriteLine("What is the ID of the student you would like to delete?");
            string idInput = Console.ReadLine();
            try
            {
                int studentId = int.Parse(idInput);
                Student theStudent = db.Students.Single(student => student.StudentId == studentId);
                db.Students.Remove(theStudent);
                db.SaveChanges();
                Console.WriteLine($"{theStudent.FirstName} {theStudent.LastName} has been deleted");
            }
            catch
            {
                Console.WriteLine("I couldn't find that student");
            }
        }
    }
}