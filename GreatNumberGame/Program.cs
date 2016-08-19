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
        public IActionResult index()
        {

            if(HttpContext.Session.GetInt32("TheNumber") == null)
            {
                HttpContext.Session.SetInt32("TheNumber", (new Random().Next(1, 100)));
            }

            if(TempData["result"] == null)
            {
                ViewData["LastGuess"] = "";
            }
            else
            {
                ViewData["LastGuess"] = TempData["result"];
            }

            ViewData["TheNumber"] = HttpContext.Session.GetInt32("TheNumber");

            return View("index");
        }

        [HttpPost]
        public IActionResult guess(int Number)
        {

            if(Number > HttpContext.Session.GetInt32("TheNumber"))
            {
                TempData["result"] = "Too High";
            }
            else if(Number < HttpContext.Session.GetInt32("TheNumber"))
            {
                TempData["result"] = "Too Low";
            }
            else
            {
                TempData["result"] = "You Got It!";
            }

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult reset()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("index");
        }
    }
}
