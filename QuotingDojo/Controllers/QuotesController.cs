using System;
using Microsoft.AspNetCore.Mvc;
using DbConnection;
using System.Collections.Generic;

namespace QuotingDojo.Controllers
{
    public class QuotesController : Controller
    {
        [HttpGet]
        public IActionResult New()
        {
            Console.WriteLine("new");
            if(TempData["Errors"] == null)
            {
                ViewData["Errors"] = new string[0];
            }
            else
            {
                ViewData["Errors"] = TempData["Errors"];
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string quote)
        {
            Console.WriteLine("create");
            List<string> Errors = new List<string>();
            if(name == null)
            {
                Errors.Add("Name cannot be blank");
            }
            if(quote == null)
            {
                Errors.Add("Quote cannot be blank");
            }
            if(Errors.Count > 0)
            {
                TempData["Errors"] = Errors;
                return RedirectToAction("New");
            }
            else
            {
                string query = $"INSERT INTO quote (Name, QuoteText, CreatedAt) VALUES ('{name}', '{quote}', NOW())";
                DbConnector.ExecuteQuery(query);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            Console.WriteLine("index");
            string query = "SELECT * FROM quote ORDER BY CreatedAt DESC";
            List<Dictionary<string, object>> quotes = DbConnector.ExecuteQuery(query);
            ViewData["quotes"] = quotes.ToArray();
            return View();
        }
    }
}