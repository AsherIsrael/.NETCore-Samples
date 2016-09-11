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

        RouteBuilder router = new RouteBuilder(app);

        router.MapGet("hello", context =>
        {

            return context.Response.WriteAsync("Hello World!");
        }); 
        
        IRouter routes = router.Build();
        app.UseRouter(routes);
    }
}