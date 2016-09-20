using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using DbConnection;
using System;
using Microsoft.AspNetCore.Http;
using CryptoHelper;

namespace LoginReg.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult New()
        {
            Console.WriteLine("new");
            if(TempData["Errors"] != null)
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
        public IActionResult Register(string FirstName, string LastName, string Email, string Password, string PasswordConfirm)
        {
            Console.WriteLine("register");
            List<string> Errors = new List<string>();
            if(FirstName == null)
            {
                Errors.Add("First name cannot be blank");
            }
            else if(!Regex.IsMatch(FirstName, @"^[a-zA-Z]+$"))
            {
                Errors.Add("First name can only contain letters");
            }
            else if(FirstName.Length < 2)
            {
                Errors.Add("First name must be at least 2 charaters long");
            }

            if(LastName == null)
            {
                Errors.Add("Last name cannot be blank");
            }
            else if(!Regex.IsMatch(LastName, @"^[a-zA-Z]+$"))
            {
                Errors.Add("Last name can only contain letters");
            }
            else if(LastName.Length < 2)
            {
                Errors.Add("Last name must be at least 2 charaters long");
            }

            if(Email == null)
            {
                Errors.Add("Email cannot be blank");
            }
            else if(!Regex.IsMatch(Email, @"^[a-zA-Z0-9\.\+_-]+@[a-zA-Z0-9\._-]+\.[a-zA-Z]*$"))
            {
                Errors.Add("Email must be a valid email");
            }

            if(Password == null)
            {
                Errors.Add("Password cannot be blank");
            }
            else if(Password.Length < 8)
            {
                Errors.Add("Password must be at least 8 characters");
            }
            else if(Password != PasswordConfirm)
            {
                Errors.Add("Password and Confirmation must match");
            }

            if(Errors.Count > 0)
            {
                TempData["Errors"] = Errors;
                return RedirectToAction("New");
            }
            else
            {
                // using(var hasher = new PasswordHasher(compatMode: null))
                // {
                //     string PasswordHash = hasher.HashPassword(null, Password);
                // }
                string PasswordHash = Crypto.HashPassword(Password);
                string query = $"INSERT INTO user (FirstName, LastName, Email, Password, CreatedAt, UpdatedAt) VALUES ('{FirstName}', '{LastName}', '{Email}', '{PasswordHash}', NOW(), NOW())";
                Console.WriteLine("response");
                Console.WriteLine(DbConnector.ExecuteQuery(query));
                query = "SELECT * FROM user ORDER BY UserId DESC LIMIT 1";
                List<Dictionary<string, object>> user = DbConnector.ExecuteQuery(query);
                Dictionary<string, object> newUser = user[0];
                HttpContext.Session.SetInt32("currentUser", (int)newUser["UserId"]);
                return RedirectToAction("Show");
            }
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            Console.WriteLine("login");
            List<string> Errors = new List<string>();
            string query = $"SELECT * FROM user WHERE Email = '{Email}' LIMIT 1";
            List<Dictionary<string, object>> user = DbConnector.ExecuteQuery(query);
            if(user.Count == 0)
            {
                Errors.Add("Email or password is incorrect");
                TempData["Errors"] = Errors;
                return RedirectToAction("New");
            }
            else
            {
                Dictionary<string, object> checkUser = user[0];
                bool correct = Crypto.VerifyHashedPassword((string)checkUser["Password"], Password);
                Console.WriteLine(correct);
                if(correct)
                {
                    HttpContext.Session.SetInt32("currentUser", (int)checkUser["UserId"]);
                    return RedirectToAction("Show");
                }
                else
                {
                    Errors.Add("Email or password is incorrect");
                    TempData["Errors"] = Errors;
                    return RedirectToAction("New");
                }
            }
        }

        [HttpGet]
        public IActionResult Show()
        {
            Console.WriteLine("show");
            string query = $"SELECT * FROM user WHERE UserId = {HttpContext.Session.GetInt32("currentUser")}";
            List<Dictionary<string, object>> user = DbConnector.ExecuteQuery(query);
            ViewData["User"] = user[0];
            return View();
        }
    }
}