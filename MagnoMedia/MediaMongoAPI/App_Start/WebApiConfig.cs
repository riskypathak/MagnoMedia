using Magno.Data;
using MagnoMedia.Common;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Http;
using System.Linq;
using MagnoMedia.Data.Models;

namespace MagnoMedia.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Logging.Log.Info("Inside Register Method");

            // Web API configuration and services

            

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
