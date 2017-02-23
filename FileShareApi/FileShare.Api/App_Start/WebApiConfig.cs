using System.Web.Http;
using System.Web.Http.Cors;

namespace FileShare.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //var localCors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            //var prodCors = new EnableCorsAttribute("http://file-share.ttiamus.com", "*", "*");
            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);
            //config.EnableCors(localCors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
