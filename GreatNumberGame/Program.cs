using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace GreatNumberGame
{

    public class Program
    {

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class NumberController : Controller
    {

        [HttpGet]
        public IActionResult index(string result)
        {

            if(HttpContext.Session.GetInt32("TheNumber") == null)
            {
                HttpContext.Session.SetInt32("TheNumber", (new Random().Next(1, 100)));
            }

            if(result == null)
            {
                ViewData["LastGuess"] = "";
            }
            else
            {
                ViewData["LastGuess"] = result;
            }

            ViewData["TheNumber"] = HttpContext.Session.GetInt32("TheNumber");
            return View("index");
        }

        [HttpPost]
        public IActionResult guess(int Number)
        {

            string result;
            if(Number > HttpContext.Session.GetInt32("TheNumber"))
            {
                result = "Too High";
            }
            else if(Number < HttpContext.Session.GetInt32("TheNumber"))
            {
                result = "Too Low";
            }
            else
            {
                result = "You Got It!";
            }
            
            return index(result);
        }

        [HttpGet]
        public IActionResult reset()
        {
            HttpContext.Session.Clear();
            return index(null);
        }
    }
}
