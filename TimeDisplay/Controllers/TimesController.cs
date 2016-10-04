using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace TimeDisplay.Controllers
{
    public class TimesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            DateTime CurrentTime = DateTime.Now;
            ViewData["Time"] = CurrentTime.ToString(new CultureInfo("en-US"));
            return View();
        }
    }
}