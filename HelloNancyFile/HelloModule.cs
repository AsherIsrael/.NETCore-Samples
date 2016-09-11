using Nancy;

namespace HelloNancyFile
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get("/", args => 
            {
                return View["Hello"];
            });
        }
    }
}