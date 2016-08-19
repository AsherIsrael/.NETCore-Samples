using Nancy;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Nancy.Owin;

public class NumberModule : NancyModule
{
    public NumberModule()
    {
        Get.("/", args => 
            Console.WriteLine(args);
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
                ViewData["LastGuess"] = args;
            }

            ViewData["TheNumber"] = HttpContext.Session.GetInt32("TheNumber");
            return View("index");
        );
        // Post.("/guess", args => );
        // Get.("/reset", args => );
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}