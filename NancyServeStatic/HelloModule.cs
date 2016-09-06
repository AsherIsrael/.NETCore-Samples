using Nancy;

namespace NancyServeStatic
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get("/", args => 
            {
                return View["Index"];
            });
        }
    }
}