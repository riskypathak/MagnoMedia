using Magno.Data;
using MagnoMedia.Data.Models;
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



            using (IDbConnection db = dbFactory.Open())
            {
                User _usr = new User
                {
                    BrowserName = request.DefaultBrowser,
                    CreationDate = DateTime.Now,
                    FingerPrint = request.MachineUID,
                    OSName = request.OSName,
                    RefererId = 1111,
                    IP = request.IPAddress,
                    CountryName = request.CountryName
                };
                long count = db.Insert<User>(_usr);
            }


            return GetSoftwareList();
        }


        [Route("{id:int}")]
        public ThirdPartyApplicationDetails Get(int id)
        {
            return GetSoftwareDetails(id);
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


        private ThirdPartyApplicationDetails GetSoftwareDetails(int id)
        {
            IDbConnectionFactory dbFactory =
               new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {

                return db.SingleById<ThirdPartyApplicationDetails>(id);
            }
        }
    }
}
