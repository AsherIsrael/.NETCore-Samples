
using LoginRegWithEF.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoginRegWithEF
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=localhost;database=efwall;uid=root;pwd=root;";
            services.AddDbContext<TestContext>(options => options.UseMySql(connection));
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseSession();
            app.UseStaticFiles();

            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    name: "New",
                    template: "",
                    defaults: new {Controller = "Users", action = "Register"}
                );

                routes.MapRoute(
                    name: "Register",
                    template: "register",
                    defaults: new {Controller = "Users", action = "Register"}
                );

                routes.MapRoute(
                    name: "Show",
                    template: "user",
                    defaults: new {Controller = "Users", action = "Show"}
                );

                routes.MapRoute(
                    name: "Login",
                    template: "login",
                    defaults: new {Controller = "Users", action = "Login"}
                );
            });
        }
    }
}