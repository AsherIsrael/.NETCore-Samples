using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting();
    }

    public void Configure(IApplicationBuilder app)
    {            
        var router = new RouteBuilder(app);

        router.MapGet("hello", context => 
        {
            Console.WriteLine("Hello World!");
            return;
        });
        
        var routes = router.Build();
        app.UseRouter(routes);
    }
}