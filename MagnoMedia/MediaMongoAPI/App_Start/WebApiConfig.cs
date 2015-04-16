using Magno.Data;
using MagnoMedia.Common;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Http;

namespace MagnoMedia.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            Logging.Log.Info("Inside Register Method");

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            init();
        }

        private static void init()
        {
            IDbConnectionFactory dbFactory =
                new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                if (db.TableExists<ThirdPartyApplication>())
                {
                    db.DropAndCreateTable<ThirdPartyApplication>();
                }
                else
                {
                    db.CreateTable<ThirdPartyApplication>();
                }

                db.InsertAll<ThirdPartyApplication>(GetSoftwareList());

            }
        }

        private static IEnumerable<ThirdPartyApplication> GetSoftwareList()
        {
            return new List<ThirdPartyApplication>()
            {
           
                new ThirdPartyApplication
                 { 
                     DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
                     HasUrl = true,
                     Name= "Robots",
                     Url = "www.Notepadplus.com/about",
                     InstallerName = "SkypeSetup.exe",
                     Arguments = ""
                 },
                 new ThirdPartyApplication
                 { 
                     DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
                     Name= "Robots2",
                     Url = "www.Notepadplus.com/about",
                     InstallerName = "SkypeSetup.exe",
                     Arguments = ""
                 }
          
           
           };
        }
    }
}
