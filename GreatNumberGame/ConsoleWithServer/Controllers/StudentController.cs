using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConsoleWithServer.MySql;

namespace ConsoleWithServer.Controllers
{
    public class StudentController : Controller
    {

        private StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        public void index()
        {
            foreach(var student in _context.Students)
            {
                Console.WriteLine($"{student.StudentId}: {student.FirstName} {student.LastName}");
            }
            // Console.WriteLine("blah");
            return;
        }
    }
}