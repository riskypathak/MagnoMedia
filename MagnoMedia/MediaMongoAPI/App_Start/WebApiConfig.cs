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


                //ThirdPartyApplication
                if (db.TableExists<ThirdPartyApplication>())
                {
                    db.DropAndCreateTable<ThirdPartyApplication>();
                }
                else
                {
                    db.CreateTable<ThirdPartyApplication>();
                }

                //Country
                //if (db.TableExists<Country>())
                //{
                //    db.DropAndCreateTable<Country>();
                //}
                //else
                //{
                //    db.CreateTable<Country>();
                //}


                //AppCountryValidity
                if (db.TableExists<AppCountryValidity>())
                {
                    db.DropAndCreateTable<AppCountryValidity>();
                }
                else
                {
                    db.CreateTable<AppCountryValidity>();
                }

                //Browser
                if (db.TableExists<Browser>())
                {
                    db.DropAndCreateTable<Browser>();
                }
                else
                {
                    db.CreateTable<Browser>();
                }

                //AppBrowserValidity
                if (db.TableExists<AppBrowserValidity>())
                {
                    db.DropAndCreateTable<AppBrowserValidity>();
                }
                else
                {
                    db.CreateTable<AppBrowserValidity>();
                }



                //InstallationReport
                if (db.TableExists<InstallationReport>())
                {
                    db.DropAndCreateTable<InstallationReport>();
                }
                else
                {
                    db.CreateTable<InstallationReport>();
                }

                //UserInstallError
                if (db.TableExists<UserInstallError>())
                {
                    db.DropAndCreateTable<UserInstallError>();
                }
                else
                {
                    db.CreateTable<UserInstallError>();
                }

                //User
                if (db.TableExists<User>())
                {
                    db.DropAndCreateTable<User>();
                }
                else
                {
                    db.CreateTable<User>();
                }

                //UserInstallDetails
                if (db.TableExists<UserInstallDetails>())
                {
                    db.DropAndCreateTable<UserInstallDetails>();
                }
                else
                {
                    db.CreateTable<UserInstallDetails>();
                }


               // db.InsertAll<Country>(GetAllCountries());
                db.InsertAll<ThirdPartyApplication>(GetSoftwareList());
                db.InsertAll<AppCountryValidity>(GetAllAppCountryValidity());

                //sample to call foreign key
                var all = db.Select<AppCountryValidity>();
            }
        }

        private static IEnumerable<AppCountryValidity> GetAllAppCountryValidity()
        {
            IDbConnectionFactory dbFactory =
    new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            List<Country> availableCountries = null;

            using (IDbConnection db = dbFactory.Open())
            {
                availableCountries = db.Select<Country>();
            }

            return new List<AppCountryValidity>()
            {
                new AppCountryValidity()
                {
                    Order = 1,
                    ThirdPartyApplicationId = 1,
                    CountryId = availableCountries.Single(c=>c.Country_name == "India").Id
                },
                new AppCountryValidity()
                {
                    Order = 2,
                    ThirdPartyApplicationId = 2,
                    CountryId = availableCountries.Single(c=>c.Country_name == "Pakistan").Id
                },
                new AppCountryValidity()
                {
                    Order = 3,
                    ThirdPartyApplicationId = 2,
                    CountryId = availableCountries.Single(c=>c.Country_name == "Spain").Id
                },
            };
        }

        private static IEnumerable<Country> GetAllCountries()
        {
            return new List<Country>()
            {
                new Country()
                {
                    Iso = "IN",
                    Country_name = "India"
                }
                
            };
        }

        private static IEnumerable<ThirdPartyApplication> GetSoftwareList()
        {
            return new List<ThirdPartyApplication>()
            {

                new ThirdPartyApplication
                      { 
                          DownloadUrl ="http://aff-software.s3-website-us-east-1.amazonaws.com/2ab4a67f644e190dfd416f2fb7f36c06/Cloud_Backup_Setup.exe",
                          HasUrl = true,
                          Name= "Cloud_Backup",
                          Url = "www.Notepadplus.com/about",
                          InstallerName = "Cloud_Backup_Setup.exe"
                      },

                     new ThirdPartyApplication
                      { 
                          DownloadUrl ="http://188.42.227.39/vidsoom/unicobrowser.exe",
                          HasUrl = true,
                          Name= "unicobrowser",
                          Url = "www.unicobrowser.com/about",
                          InstallerName = "unicobrowser.exe"
                      }

                      


                };
          
           
           
        }
    }
}
