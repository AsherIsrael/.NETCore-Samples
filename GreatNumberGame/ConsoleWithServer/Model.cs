using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConsoleWithServer.MySql
{
    public class StudentContext : DbContext
    {

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
            { }

        public DbSet<Student> Students { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder
        //         .UseMySql(@"Server=localhost;database=efStudents;uid=root;pwd=root;");
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}