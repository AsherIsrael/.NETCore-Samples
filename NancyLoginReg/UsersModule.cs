using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CryptoHelper;
using DbConnection;
using Nancy;

namespace NancyLoginReg
{
    public class UsersModule : NancyModule
    {
        public UsersModule()
        {

            Get("/", args =>
            {
                if(Session["Errors"] != null)
                {
                    return View["index", Session["Errors"]];
                }
                else
                {
                    return View["index", new List<string>()];
                }
            });

            Post("/register", args =>
            {
                List<string> Errors = new List<string>();
                string FirstName = Request.Form.FirstName;
                string LastName = Request.Form.LastName;
                string Email = Request.Form.Email;
                string Password = Request.Form.Password;
                string PasswordConfirm = Request.Form.PasswordConfirm;
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
                    Session["Errors"] = Errors;
                    return Response.AsRedirect("/");
                }
                else
                {
                    string PasswordHash = Crypto.HashPassword(Password);
                    string query = $"INSERT INTO user (FirstName, LastName, Email, Password, CreatedAt, UpdatedAt) VALUES ('{FirstName}', '{LastName}', '{Email}', '{PasswordHash}', NOW(), NOW())";
                    Console.WriteLine("response");
                    Console.WriteLine(DbConnector.ExecuteQuery(query));
                    query = "SELECT * FROM user ORDER BY UserId DESC LIMIT 1";
                    List<Dictionary<string, object>> user = DbConnector.ExecuteQuery(query);
                    Dictionary<string, object> newUser = user[0];
                    Session["CurrentUser"] = (int)newUser["UserId"];
                    return Response.AsRedirect("/user");
                }
            });

            Get("/user", args =>
            {
                string query = $"SELECT * FROM user WHERE UserId = {Session["CurrentUser"]}";
                List<Dictionary<string, object>> user = DbConnector.ExecuteQuery(query);
                ViewBag.User = user[0];
                return View["show"];
            });

            Post("/login", args =>
            {
                List<string> Errors = new List<string>();
                string Email = Request.Form.Email;
                string Password = Request.Form.Password;
                string query = $"SELECT * FROM user WHERE Email = '{Email}' LIMIT 1";
                List<Dictionary<string, object>> user = DbConnector.ExecuteQuery(query);
                if(user.Count == 0)
                {
                    Errors.Add("Email or password is incorrect");
                    Session["Errors"] = Errors;
                    return Response.AsRedirect("/");
                }
                else
                {
                    Dictionary<string, object> checkUser = user[0];
                    bool correct = Crypto.VerifyHashedPassword((string)checkUser["Password"], Password);
                    Console.WriteLine(correct);
                    if(correct)
                    {
                        Session["CurrentUser"] = (int)checkUser["UserId"];
                        return Response.AsRedirect("/user");
                    }
                    else
                    {
                        Errors.Add("Email or password is incorrect");
                        Session["Errors"] = Errors;
                        return Response.AsRedirect("/");
                    }
                }
            });
        }
    }
}
