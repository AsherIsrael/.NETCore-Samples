using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuotingDojoWithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace QuotingDojoWithEF
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=localhost;database=efquotes;uid=root;pwd=root;";
            services.AddDbContext<QuotingContext>(options => options.UseMySql(connection));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseStaticFiles();

            app.UseMvc( routes => {
                routes.MapRoute(
                    name: "New",
                    template: "",
                    defaults: new {Controller = "Quotes", action = "New"}
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