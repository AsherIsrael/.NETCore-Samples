using Nancy;
using Nancy.Configuration;
using Nancy.Session;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace NancyNinjaGold
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            CookieBasedSessions.Enable(pipelines);
        }
    }
}