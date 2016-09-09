using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GreatNumberGame
{
    
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc( routes => {
                routes.MapRoute(
                    name: "",
                    template: "{action}",
                    defaults: new {controller = "Number", action = "index"}
                );
            });
        }
    }
}
