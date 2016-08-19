using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


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

        public void Configure(IApplicationBuilder app)
        {

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
