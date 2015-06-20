using MagnoMedia.Data.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MagnoMedia.Web.Api.Controllers
{
    [RoutePrefix("api/installer")]
    public class InstallerController : ApiController
    {

        [HttpPost(), Route("api/SaveInstallerState")]
        public bool SaveInstallerState([FromUri] string SessionCode, [FromUri] string UserCode, UserTrack userTrack)
        {
            IDbConnectionFactory dbFactory =
              new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                int sessionDetailID = db.Single<SessionDetail>(r => r.SessionCode == SessionCode).Id;
                userTrack.UserId = db.Single<User>(r => r.SessionDetailId == sessionDetailID && r.FingerPrint == UserCode).Id;

                userTrack.SessionDetailId = sessionDetailID;

                userTrack.UpdatedDate = DateTime.Now;
                db.Insert<UserTrack>(userTrack);

                return true;
            }
        }
    }
}
