using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Nancy.Owin;

namespace NancyNumberGame
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseOwin(x => x.UseNancy());
        }
    }
}