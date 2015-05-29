using MagnoMedia.Data.Models;
using MagnoMedia.Web.Api.Utilities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace MagnoMedia.Web.Api.Controllers
{
    [RoutePrefix("api/software")]
    public class SoftwareController : ApiController
    {
        // GET api/software
        [Route(Name = "list")]
        public IEnumerable<ThirdPartyApplication> Get([FromUri] string SessionCode)
        {
            // Insert USER Data into Db
            IDbConnectionFactory dbFactory =
              new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            string ipAddress = ServerHelper.GetClientIPAddress();
            using (IDbConnection db = dbFactory.Open())
            {
                int sessionDetailID = db.Single<SessionDetail>(r => r.SessionCode == SessionCode).Id;

                User user = db.Single<User>(u => u.SessionDetailId == sessionDetailID);

                IEnumerable<int> appValidOSIds = db.Select<AppOSValidity>(a => a.OSId == user.OsId).Select(a => a.ApplicationId);
                IEnumerable<int> appValidBrowserIds = db.Select<AppBrowserValidity>(a => a.BrowserId == user.BrowserId).Select(a => a.ApplicationId);
                IEnumerable<int> appValidCountryIds = db.Select<AppCountryValidity>(a => a.CountryId == user.CountryId).Select(a => a.ApplicationId);

                IEnumerable<int> validAppIds = appValidBrowserIds.Intersect(appValidOSIds).Intersect(appValidCountryIds);

                //Insert into tracking
                UserTrack userTrack = new UserTrack() { SessionDetailId = sessionDetailID, UserId = user.Id, UpdatedDate = DateTime.Now, State = UserTrackState.InstallStart };
                db.Insert<UserTrack>(userTrack);

                return db.Select<ThirdPartyApplication>(t => validAppIds.Contains(t.Id));
            }

            return null;
        }


        [Route("{id:int}")]
        public ThirdPartyApplication Get(int id)
        {
            return GetSoftwareDetails(id);
        }

        [Route("applicationpath")]
        [HttpPost]
        public HttpResponseMessage ApplicationPath(MagnoMedia.Web.Models.UserInstallRequest request)
        {
            //TODO hard coding zip on server

            IDbConnectionFactory dbFactory =
            new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);
            string ipAddress = ServerHelper.GetClientIPAddress();
            using (IDbConnection db = dbFactory.Open())
            {
                SessionDetail sessionDetail = db.Select<SessionDetail>().SingleOrDefault(s => s.SessionCode == request.SessionID);

                if (sessionDetail == null)
                {
                    return null; // we will assume that a request without session is invalid
                }

                //check for already existing user and insert
                User existingUser = db.Single<User>(x => x.FingerPrint == request.MachineUID);

                //This means that user is installing it for the first time
                if (existingUser == null)
                {
                    // Browser Save
                    Browser browser = new Browser
                    {
                        BrowserName = request.DefaultBrowser
                    };

                    int browserId = DbHelper.SaveInDB<Browser>(dbFactory, browser, x => request.DefaultBrowser.ToLower().Contains(x.BrowserName.ToLower()));

                    // OS Save
                    MagnoMedia.Data.Models.OperatingSystem os = new Data.Models.OperatingSystem
                    {
                        OSName = request.OSName
                    };
                    int osId = DbHelper.SaveInDB<MagnoMedia.Data.Models.OperatingSystem>(dbFactory, os, x => request.OSName.Contains(x.OSName));

                    //Country Save
                    Country country = new Country
                    {
                        Country_name = request.CountryName,
                        Iso = request.CountryName
                    };
                    int countryId = DbHelper.SaveInDB<Country>(dbFactory, country, x => x.Iso.Equals(country.Iso));

                    User user = new User
                    {
                        BrowserId = browserId,
                        OsId = osId,
                        CountryId = countryId,
                        CreationDate = DateTime.Now,
                        FingerPrint = request.MachineUID,
                        IP = ipAddress,
                        SessionDetailId = sessionDetail.Id,
                    };
                    long count = db.Insert<User>(user);

                    UserTrack userTrack = new UserTrack() { SessionDetailId = sessionDetail.Id, UserId = user.Id, UpdatedDate = DateTime.Now, State = UserTrackState.InstallInit };
                    db.Insert<UserTrack>(userTrack);
                }
                else
                {
                    // This is returning user with same machine. So we will just update his last session
                    existingUser.SessionDetailId = sessionDetail.Id;
                    existingUser.CreationDate = DateTime.Now;
                    db.Save(existingUser);


                    UserTrack userTrack = new UserTrack() { SessionDetailId = sessionDetail.Id, UserId = existingUser.Id, UpdatedDate = DateTime.Now, State = UserTrackState.InstallInit };
                    db.Insert<UserTrack>(userTrack);
                }

                string newVidsoomPath = HttpContext.Current.Server.MapPath("~/Temp\\" + sessionDetail.SessionCode + "\\vidsoom.zip");

                string vidsoomPath = HttpContext.Current.Server.MapPath("~/App_Data\\Application\\vidsoom.zip");

                //File.Copy(HttpContext.Current.Server.MapPath("~/App_Data\\Application\\vidsoom.zip"), newVidsoomPath, true);

                //risky
                //newVidsoomPath = "http://188.42.227.39/vidsoom/Debug.zip";

                var stream = new FileStream(vidsoomPath, FileMode.Open);
                var content = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };
                content.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return content;

                //return newVidsoomPath;
            }

            return null;
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
