using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using LoginRegWithEF.Models;
using System.Linq;
using System.Security.Cryptography;

namespace LoginRegWithEF.Controllers
{
    public class UsersController : Controller
    {
        private TestContext _context;

        public UsersController(TestContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            Console.WriteLine("new");
            if(TempData["Errors"] != null )
            {
                ViewData["Errors"] = TempData["Errors"];
            }
            else
            {
                ViewData["Errors"] = new string[0];
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register user)
        {
            Console.WriteLine("register");
            if(ModelState.IsValid)
            {
                User newUser = new User{
                    FirstName = user.FirstName, 
                    LastName = user.LastName, 
                    Email = user.Email, 
                    Password = user.Password, 
                    CreatedAt = DateTime.Now, 
                    UpdatedAt = DateTime.Now};
                _context.User.Add(newUser);
                Console.WriteLine(_context.SaveChanges());
                int newUserId = _context.User.Max(u => u.UserId);
                HttpContext.Session.SetInt32("currentUser", newUserId);
                return RedirectToAction("Show");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            Console.WriteLine("login");

            User checkUser = _context.User.Single(u => u.Email == Email);
            if(checkUser != null)
            {
                if(Password == checkUser.Password)
                {
                    HttpContext.Session.SetInt32("currentUser", checkUser.UserId);
                    return RedirectToAction("Show");
                }
            }
            ViewData["Error"] = "Email or Password was incorrect";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Error"] = "";
            return View();
        }

        [HttpGet]
        public IActionResult Show()
        {
            Console.WriteLine("show");
            return View(_context.User.Single(u => u.UserId == HttpContext.Session.GetInt32("currentUser")));
        }
    }
}