﻿using Magno.Data;
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
                    existingUser.BrowserName = request.DefaultBrowser;
                    existingUser.CreationDate = DateTime.Now;
                    db.Save(existingUser);
                }
                else
                {
                    User _usr = new User
                    {
                        BrowserName = request.DefaultBrowser,
                        CreationDate = DateTime.Now,
                        FingerPrint = request.MachineUID,
                        OSName = request.OSName,
                        IP = ipAddress,
                        CountryName = request.CountryName
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
    }
}
