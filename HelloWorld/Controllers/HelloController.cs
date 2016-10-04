// using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace YourNamespace.Controllers
{
	public class HelloController : Controller
	{
		[HttpGet]
		[Route("test")]
		public IActionResult Index()
		{
			List<string> SomeThing = new List<string>();
			SomeThing.Add("first");
			SomeThing.Add("second");
			SomeThing.Add("thi");
			ViewBag.List = SomeThing;
			ViewBag.Thing = "stuff";
			string test = "test";
			int Soze = test.Length;
			Math.Abs(-5);
			return View();
		}

	}
}