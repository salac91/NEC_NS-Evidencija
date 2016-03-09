using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetsNS_Evidencija
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ReportRoute",
               url: "{controller}/{action}/{id},{fromDate},{toDate}",
               defaults: new { controller = "Report", action = "ExportCoachReport", id = UrlParameter.Optional, fromDate = UrlParameter.Optional, toDate = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ReportRoute2",
               url: "{controller}/{action}/{id},{fromDate},{toDate},{name}",
               defaults: new { controller = "Report", action = "ExportCoachReport", id = UrlParameter.Optional, fromDate = UrlParameter.Optional, toDate = UrlParameter.Optional,
               name = UrlParameter.Optional}
           );

            routes.MapRoute(
                name: "ImageUpload",
                url: "{controller}/{action}",
                defaults: new { controller = "ImageUpload", action = "Upload"}
            );

             routes.MapRoute(
               name: "LogOffRoute",
               url: "Account/LogOff"         
            );
            
        }
    }
}