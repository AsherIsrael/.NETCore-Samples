using System;
using Microsoft.AspNetCore.Mvc;
using QuotingDojoWithEFWoutTagH.Models;
using System.Linq;

namespace QuotingDojoWithEFWoutTagH.Controllers
{
    public class QuotesController : Controller
    {
        private QuotingContext _context;

        public QuotesController(QuotingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Console.WriteLine("index");
            return View(_context.Quote.ToList());
        }

        [HttpGet]
        public IActionResult New()
        {
            Console.WriteLine("new");
            return View("create");
        }

        [HttpPost]
        public IActionResult Create(string Name, string QuoteText)
        {
            Console.WriteLine("create");
            if(ModelState.IsValid)
            {
                quote.CreatedAt = DateTime.Now;
                _context.Quote.Add(quote);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quote);
        }
    }
}