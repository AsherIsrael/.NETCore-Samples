using Nancy;
using Nancy.Session.Persistable;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.Session.InMemory;
using Nancy.Configuration;
// using Nancy.InMemorySessions;

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
            // base.ApplicationStartup(container, pipelines);
            PersistableSessions.Enable(pipelines, new InMemorySessionConfiguration());
            // InMemorySessions.Enable(pipelines);
        }
    }
}