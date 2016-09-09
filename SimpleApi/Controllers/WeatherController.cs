using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult index()
        {
            return View("index");
        }
    }
}