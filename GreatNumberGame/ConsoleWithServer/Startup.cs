using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ConsoleWithServer.MySql;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;


namespace ConsoleWithServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var connection = @"Server=localhost;database=efStudents;uid=root;pwd=root;";
            services.AddDbContext<StudentContext>(options => options.UseMySql(connection));
        }

        public void Configure(IApplicationBuilder app)
        {            

            app.UseMvc( routes => {
                routes.MapRoute(
                    name: "",
                    template: "{action}",
                    defaults: new {controller = "Student", action = "index"}
                );
            });
        }
    }
}
