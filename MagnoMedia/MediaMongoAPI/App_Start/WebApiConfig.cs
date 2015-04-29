using Magno.Data;
using MagnoMedia.Common;
using MagnoMedia.Data.APIResponseDTO;
using MagnoMedia.Data.DBEntities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Http;
using System.Linq;

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

            Init();
        }

        private static void Init()
        {
            IDbConnectionFactory dbFactory =
                new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                if (db.TableExists<AppCountryValidityEntity>())
                {
                    db.DropAndCreateTable<AppCountryValidityEntity>();
                }
                else
                {
                    db.CreateTable<AppCountryValidityEntity>();
                }


                if (db.TableExists<ThirdPartyApplication>())
                {
                    db.DropAndCreateTable<ThirdPartyApplication>();
                }
                else
                {
                    db.CreateTable<ThirdPartyApplication>();
                }

                if (db.TableExists<CountryDBEntity>())
                {
                    db.DropAndCreateTable<CountryDBEntity>();
                }
                else
                {
                    db.CreateTable<CountryDBEntity>();
                }

                
                if (db.TableExists<InstallationReportEntity>())
                {
                    db.DropAndCreateTable<InstallationReportEntity>();
                }
                else
                {
                    db.CreateTable<InstallationReportEntity>();
                }

                db.InsertAll<CountryDBEntity>(GetAllCountries());
                db.InsertAll<ThirdPartyApplication>(GetSoftwareList());
                db.InsertAll<AppCountryValidityEntity>(GetAllAppCountryValidity());

                //sample to call foreign key
                var all = db.Select<AppCountryValidityEntity>();
            }
        }

        private static IEnumerable<AppCountryValidityEntity> GetAllAppCountryValidity()
        {
            IDbConnectionFactory dbFactory =
    new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            List<CountryDBEntity> availableCountries = null;

            using (IDbConnection db = dbFactory.Open())
            {
                availableCountries = db.Select<CountryDBEntity>();
            }

            return new List<AppCountryValidityEntity>()
            {
                new AppCountryValidityEntity()
                {
                    Order = 1,
                    ThirdPartyApplicationId = 1,
                    CountryId = availableCountries.Single(c=>c.CountryName == "India").Id
                },
                new AppCountryValidityEntity()
                {
                    Order = 2,
                    ThirdPartyApplicationId = 2,
                    CountryId = availableCountries.Single(c=>c.CountryName == "Pakistan").Id
                },
                new AppCountryValidityEntity()
                {
                    Order = 3,
                    ThirdPartyApplicationId = 3,
                    CountryId = availableCountries.Single(c=>c.CountryName == "India").Id
                },
            };
        }

        private static IEnumerable<CountryDBEntity> GetAllCountries()
        {
            return new List<CountryDBEntity>()
            {
                new CountryDBEntity()
                {
                    CountryCode = "IN",
                    CountryName = "India"
                },
                new CountryDBEntity()
                {
                    CountryCode = "PK",
                    CountryName = "Pakistan"
                },
                new CountryDBEntity()
                {
                    CountryCode = "SL",
                    CountryName = "Sri Lanka"
                }
            };
        }

        private static IEnumerable<ThirdPartyApplication> GetSoftwareList()
        {
            return new List<ThirdPartyApplication>()
            {
           
                new ThirdPartyApplication()
                 { 
                     DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
                     HasUrl = true,
                     Name= "Robots",
                     Url = "www.Notepadplus.com/about",
                     InstallerName = "SkypeSetup.exe",
                     Arguments = ""
                 },
                 new ThirdPartyApplication()
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
