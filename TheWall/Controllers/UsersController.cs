using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace TheWall.Controllers
{
    public class UsersController : Controller
    {
        private TestContext _context;

        public UsersController(TestContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(Register user)
        {
            if(ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = user.FirstName, 
                    LastName = user.LastName, 
                    Email = user.Email, 
                    Password = user.Password, 
                    CreatedAt = DateTime.Now, 
                    UpdatedAt = DateTime.Now
                };

                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.Password = hasher.HashPassword(newUser, user.Password);

                _context.User.Add(newUser);
                Console.WriteLine(_context.SaveChanges());
                int newUserId = _context.User.Max(u => u.UserId);
                HttpContext.Session.SetInt32("currentUser", newUserId);
                return RedirectToAction("Show");
            }
            return View(user);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email = " ", string Password = " ")
        {

            User checkUser = _context.User.Where(u => u.Email == Email).SingleOrDefault();
            Console.WriteLine((checkUser == null));
            Console.WriteLine("after this");
            if(checkUser != null && Password != null)
            {
            
                PasswordHasher<User> hasher = new PasswordHasher<User>();

                if(0 != hasher.VerifyHashedPassword(checkUser, checkUser.Password, Password))
                {
                    HttpContext.Session.SetInt32("currentUser", checkUser.UserId);
                    return RedirectToAction("Show");
                }
                
            }
            ViewData["Error"] = "Email or Password was incorrect";
            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            ViewData["Error"] = "";
            return View();
        }

        [HttpGet]
        [Route("user")]
        public IActionResult Show()
        {
            return View(_context.User.Single(u => u.UserId == HttpContext.Session.GetInt32("currentUser")));
        }
    }
}