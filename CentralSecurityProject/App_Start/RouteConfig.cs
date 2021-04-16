using System.Web.Mvc;
using System.Web.Routing;

namespace CentralSecurityProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // تنظیمات مربوط به مسیر منابع
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // The First Pattern : الگوی اول تنظیمات مربوط به مسیریاب
            routes.MapRoute(
                 name: "FirstPattern",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );

            /* 
             * Sample 1 : http://localhost:6119/
             * Sample 2 : http://localhost:6119/Home
             * Sample 3 : http://localhost:6119/Home/Index
             * Sample 4 : http://localhost:6119/Home/Index/100
             */

            // The Second Pattern : الگوی دوم تنظیمات مربوط به مسیریاب
            /*
            routes.MapRoute(
                name: "SecondPattern",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
            */

            // The Third Pattern : الگوی سوم تنظیمات مربوط به مسیریاب
            /*
            routes.MapRoute(
                name: "ThirdPattern",
                url: "Security/{controller}/{action}/{id}",
                defaults: new { controller = "ApplicationGroup", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "CentralSecurityProject.Controllers.Security" } // Define Namespace
            );
            */

            // The Fourth Pattern : الگوی چهارم تنظیمات مربوط به مسیریاب
            App_Start.ApplicationConfig.GetSchemas().ForEach(f =>
            routes.MapRoute(
                name: f,
                url: $"{f}/{{controller}}/{{action}}/{{id}}",
                defaults: new { controller = "ApplicationGroup", action = "Index", id = UrlParameter.Optional }
            ));
        }
    }
}
