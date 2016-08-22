using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseStaticFiles();

            app.UseMvc( routes => {
                routes.MapRoute(
                    name: "",
                    template: "{name}",
                    defaults: new {controller = "Simple", action = "index"}
                );
            });
        }
    }