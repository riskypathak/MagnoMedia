using Magno.Data;
using MagnoMedia.Data.DBEntities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
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
            // Apply Filter
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
