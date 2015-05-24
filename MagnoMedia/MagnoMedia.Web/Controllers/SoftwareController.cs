using Magno.Data;
using MagnoMedia.Data.Models;
using MagnoMedia.Web.Api.Utilities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Http;

namespace MagnoMedia.Web.Api.Controllers
{

    [RoutePrefix("api/software")]
    public class SoftwareController : ApiController
    {
        // GET api/software
        [Route(Name = "list")]
        public IEnumerable<ThirdPartyApplication> Get([FromUri] UserData request)
        {
            // Insert USER Data into Db
            IDbConnectionFactory dbFactory =
              new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);


            string ipAddress = ServerHelper.GetClinetIpAddress();
            using (IDbConnection db = dbFactory.Open())
            {
                //check for already existing user
                User existingUser = db.Single<User>(x => x.FingerPrint == request.MachineUID);
                if (existingUser != null)
                {
                    //existingUser.BrowserId = request.DefaultBrowser;
                    existingUser.CreationDate = DateTime.Now;
                    db.Save(existingUser);
                }
                else
                {
                    User _usr = new User
                    {
                        //BrowserId = request.DefaultBrowser,
                        CreationDate = DateTime.Now,
                        FingerPrint = request.MachineUID,
                        //OsId = request.OSName,
                        IP = ipAddress,
                        //CountryId = request.CountryName
                    };
                    long count = db.Insert<User>(_usr);
                }
            }


            return GetSoftwareList();
        }


        [Route("{id:int}")]
        public ThirdPartyApplication Get(int id)
        {
            return GetSoftwareDetails(id);
        }

        [Route("applicationpath")]
        [HttpPost]
        public string ApplicationPath(UserData request)
        {
            //TODO hard coding zip on server

            IDbConnectionFactory dbFactory =
            new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
            string ipAddress = ServerHelper.GetClinetIpAddress();
            using (IDbConnection db = dbFactory.Open())
            {
                //check for already existing user
                User existingUser = db.Single<User>(x => x.FingerPrint == request.MachineUID);
                if (existingUser != null)
                {
                    //existingUser.BrowserId = request.DefaultBrowser;
                    existingUser.CreationDate = DateTime.Now;
                    db.Save(existingUser);
                }
                else
                {

                    SessionDetail sessionDetail = db.Single<SessionDetail>(x => x.Id == request.SessionID);

                    // Browser Save
                    Browser browser = new Browser
                    {
                        BrowserName = request.DefaultBrowser
                    };
                    int browserId = DbHelper.SaveInDB<Browser>(dbFactory, browser, x => x.BrowserName.Contains(request.DefaultBrowser));

                    // OS Save
                    MagnoMedia.Data.Models.OperatingSystem os = new Data.Models.OperatingSystem
                    {
                        OSName = request.OSName
                    };
                    int osId = DbHelper.SaveInDB<MagnoMedia.Data.Models.OperatingSystem>(dbFactory, os, x => x.OSName.Contains(request.OSName));

                    //Country Save
                    Country country = new Country
                    {
                        Country_name = request.CountryName,
                        Iso = request.CountryName
                    };
                    int countryId = DbHelper.SaveInDB<Country>(dbFactory, country, x => x.Iso.Equals(country.Iso));

                    User _usr = new User
                    {
                        BrowserId = browserId,
                        OsId = osId,
                        CountryId = countryId,
                        CreationDate = DateTime.Now,
                        FingerPrint = request.MachineUID,
                        //OsId = request.OSName,
                        IP = ipAddress,
                        //CountryId = request.CountryName
                        SessionDetailId = sessionDetail.Id,

                    };
                    long count = db.Insert<User>(_usr);
                }
            }


            return "http://188.42.227.39/vidsoom/Debug.zip";

        }

        private IEnumerable<ThirdPartyApplication> GetSoftwareList()
        {
            IDbConnectionFactory dbFactory =
               new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {

                return db.Select<ThirdPartyApplication>();
            }
        }


        private ThirdPartyApplication GetSoftwareDetails(int id)
        {
            IDbConnectionFactory dbFactory =
               new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {

                return db.SingleById<ThirdPartyApplication>(id);
            }
        }

        //private static int SaveInDB<T>(IDbConnectionFactory dbFactory, T data,Func<T,bool> predicate)where T : DBEntity
        //{
        //    using (IDbConnection db = dbFactory.Open())
        //    {
        //        var existingData = db.Single<T>(predicate);
        //        if (existingData != null)
        //            return existingData.Id;
        //        return (int)db.Insert<T>(data, selectIdentity: true);
        //    }
        //}
    }
}
