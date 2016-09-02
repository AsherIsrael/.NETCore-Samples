using Nancy;

namespace HelloNancy
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get("/", args => "Hello Nancy!");
        }
    }
}