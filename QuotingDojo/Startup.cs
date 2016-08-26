using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuotingDojo
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
                    name: "New",
                    template: "",
                    defaults: new {controller = "Quotes", action = "New"}
                );

                routes.MapRoute(
                    name: "Create",
                    template: "quotes",
                    defaults: new {Controller = "Quotes", action = "Create"}
                );

                routes.MapRoute(
                    name: "Index",
                    template: "quotes",
                    defaults: new {Controller = "Quotes", action = "Index"}
                );
            });
        }
    }
}