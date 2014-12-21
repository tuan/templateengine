using System;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Logging;
using Microsoft.Owin;
using Owin;
using TemplateEngine.Security;

[assembly: OwinStartup(typeof(TemplateEngine.Startup))]

namespace TemplateEngine
{
    public class Startup
    {
        /// <summary>
        /// Configures Web API. The Startup class is specified as a type parameter in WebApp.Start method.
        /// For tutorial, visit http://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
        /// </summary>
        /// <param name="app">an instance of <see cref="IAppBuilder"/></param>
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();

            config.Filters.Clear();
            config.Filters.Add(new ApiKeyAuthenticationFilter(new Logger()));

            config.Routes.MapHttpRoute(
                name: "RenderApi",
                routeTemplate: "api/{controller}");

            app.UseWebApi(config);
        }
    }
}
