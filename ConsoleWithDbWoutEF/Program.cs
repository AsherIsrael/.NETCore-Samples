using System;
using System.Collections.Generic;
using DbConnection;

namespace ConsoleWithDb
{

    public class Program
    {

        public static void Main()
        {

                Console.WriteLine("Welcome to the Cruddy Console!");
                string choice;
                do
                {
                    Console.WriteLine("Would you like to 'create', 'read', 'readall', 'update', or 'destroy'? ('exit' to leave)");
                    choice = Console.ReadLine();
                    switch(choice)
                    {
                        case "create":
                            Create();
                            break;
                        case "read":
                            Show();
                            break;
                        case "readall":
                            Index();
                            break;
                        case "update":
                            Update();
                            break;
                        case "destroy":
                            Destroy();
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

        public static void Create()
        {
            Console.WriteLine("Let's make a new student!");
            Console.Write("Student's first name: ");
            string First_Name = Console.ReadLine();
            Console.Write("Student's last name: ");
            string Last_Name = Console.ReadLine();
            string query = $"INSERT INTO students (FirstName, LastName) VALUES('{First_Name}', '{Last_Name}')";
            DbConnector.ExecuteQuery(query);
        }

        public static void Show()
        {
            Console.WriteLine("What is the ID of the student you would like to see?");
            string idInput = Console.ReadLine();
            try
            {
                int studentId = Int32.Parse(idInput);
                string query = $"SELECT * FROM students WHERE StudentId = {idInput}";
                List<Dictionary<string, object>> vals = DbConnector.ExecuteQuery(query);
                var theStudent = vals[0];
                Console.WriteLine($"{theStudent["FirstName"]} {theStudent["LastName"]}");
            }
            catch
            {
                Console.WriteLine("I couldn't find that student");
            }
        }
        
        public static void Index()
        {
            string query = "SELECT * FROM students";
            List<Dictionary<string, object>> vals = DbConnector.ExecuteQuery(query);
            foreach(var student in vals)
            {
                Console.WriteLine($"{student["StudentId"]}: {student["FirstName"]} {student["LastName"]}");
            }
        }

        public static void Update()
        {
            Console.WriteLine("What is the ID of the student you would like to update?");
            string idInput = Console.ReadLine();
            try
            {
                int studentId = Int32.Parse(idInput);
                Console.Write("Student's new first name: ");
                string First_Name = Console.ReadLine();
                Console.Write("Student's new last name: ");
                string Last_Name = Console.ReadLine();
                string query = $"UPDATE students SET FirstName = '{First_Name}', LastName = '{Last_Name}' WHERE StudentId = {idInput}";
                DbConnector.ExecuteQuery(query);
            }
            catch
            {
                Console.WriteLine("I couldn't find that student");
            }
        }

        public static void Destroy()
        {
            Console.WriteLine("What is the ID of the student you would like to delete?");
            string idInput = Console.ReadLine();
            try
            {
                int studentId = Int32.Parse(idInput);
                string query = $"DELETE FROM students WHERE StudentId = {idInput}";
                DbConnector.ExecuteQuery(query);
            }
            catch
            {
                Console.WriteLine("I couldn't find that student");
            }
        }
    }
}