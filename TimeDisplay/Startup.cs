
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TimeDisplay
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder App, ILoggerFactory LoggerFactory)
        {
            LoggerFactory.AddConsole();
            App.UseStaticFiles();

            App.UseMvc( routes =>
            {
                routes.MapRoute(
                    name: "",
                    template: "",
                    defaults: new {controller = "Times", action = "Index"}
                );
            });
        }
    }
}