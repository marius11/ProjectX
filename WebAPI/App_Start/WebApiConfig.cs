using System.Web.Http;
using System.Web.Http.Cors;


namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // enable cors
            var cors = new EnableCorsAttribute("http://localhost:51610", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // return JSON format
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
