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
        public IEnumerable<ThirdPartyApplication> Get([FromUri] string SessionCode, [FromUri] string UserCode)
        {
            // Insert USER Data into Db
            IDbConnectionFactory dbFactory =
              new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                int sessionDetailID = db.Single<SessionDetail>(r => r.SessionCode == SessionCode).Id;

                User user = db.Single<User>(u => u.SessionDetailId == sessionDetailID && u.FingerPrint == UserCode);

                if (user != null)
                {
                    IEnumerable<int> appValidOSIds = db.Select<AppOSValidity>(a => a.OSId == user.OsId).Select(a => a.ApplicationId);
                    IEnumerable<int> appValidBrowserIds = db.Select<AppBrowserValidity>(a => a.BrowserId == user.BrowserId).Select(a => a.ApplicationId);
                    IEnumerable<int> appValidCountryIds = db.Select<AppCountryValidity>(a => a.CountryId == user.CountryId).Select(a => a.ApplicationId);

                    IEnumerable<int> validAppIds = appValidBrowserIds.Intersect(appValidOSIds).Intersect(appValidCountryIds);

                    //Insert into tracking
                    UserTrack userTrack = new UserTrack() { SessionDetailId = sessionDetailID, UserId = user.Id, UpdatedDate = DateTime.Now, State = UserTrackState.InstallStart };
                    db.Insert<UserTrack>(userTrack);

                    return db.Select<ThirdPartyApplication>(t => validAppIds.Contains(t.Id));
                }
            }

            return null;
        }

        [Route("applicationpath")]
        [HttpPost]
        public HttpResponseMessage ApplicationPath(MagnoMedia.Web.Models.UserInstallRequest request)
        {
            //TODO hard coding zip on server

            IDbConnectionFactory dbFactory =
            new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["db"].ConnectionString, MySqlDialect.Provider);

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
                    User user = GetUser(request, dbFactory, sessionDetail);
                    int userId = (int)db.Insert<User>(user);

                    UserTrack userTrack = new UserTrack() { SessionDetailId = sessionDetail.Id, UserId = userId, UpdatedDate = DateTime.Now, State = UserTrackState.InstallInit };
                    db.Insert<UserTrack>(userTrack);
                }
                else
                {
                    // This is returning user with same machine. So we will just update his last details
                    // and put old details in log table

                    SessionUserLog log = new SessionUserLog()
                    {
                        BrowserId = existingUser.BrowserId,
                        CountryId = existingUser.CountryId,
                        CreationDate = existingUser.CreationDate,
                        IP = existingUser.IP,
                        OsId = existingUser.OsId,
                        SessionDetailId = existingUser.SessionDetailId,
                        UserId = existingUser.Id
                    };
                    db.Insert<SessionUserLog>(log);

                    // Get new user object and copy it into existing
                    User user = GetUser(request, dbFactory, sessionDetail);

                    existingUser.SessionDetailId = user.SessionDetailId;
                    existingUser.BrowserId = user.BrowserId;
                    existingUser.CountryId = user.CountryId;
                    existingUser.IP = user.IP;
                    existingUser.OsId = user.OsId;
                    existingUser.CreationDate = DateTime.Now;

                    db.Save(existingUser);


                    UserTrack userTrack = new UserTrack() { SessionDetailId = sessionDetail.Id, UserId = existingUser.Id, UpdatedDate = DateTime.Now, State = UserTrackState.InstallInit };
                    db.Insert<UserTrack>(userTrack);
                }

                string vidsoomPath = HttpContext.Current.Server.MapPath("~/App_Data\\Application\\vidsoom.zip");

                var stream = new FileStream(vidsoomPath, FileMode.Open);
                var content = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };
                content.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return content;
            }
        }

        private Data.Models.User GetUser(MagnoMedia.Web.Models.UserInstallRequest request, IDbConnectionFactory dbFactory, SessionDetail sessionDetail)
        {
            string ipAddress = ServerHelper.GetClientIPAddress();

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
            int osId = DbHelper.SaveInDB<MagnoMedia.Data.Models.OperatingSystem>(dbFactory, os, x => request.OSName.ToLower().Contains(x.OSName.ToLower()));

            int countryId = ProcessCountry(request, dbFactory, ipAddress);

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

            return user;
        }

        private static int ProcessCountry(MagnoMedia.Web.Models.UserInstallRequest request, IDbConnectionFactory dbFactory, string ipAddress)
        {

            string countryCode = ServerHelper.GetCountryCode(ipAddress);

            Country country = null;

            if (string.IsNullOrEmpty(countryCode))
            {
                //Country Save
                country = new Country
                {
                    Country_name = request.CountryName,
                    Iso = request.CountryName
                };
            }
            else
            {
                //Country Save
                country = new Country
                {
                    Country_name = countryCode,
                    Iso = countryCode
                };
            }

            return DbHelper.SaveInDB<Country>(dbFactory, country, x => x.Iso.ToLower().Equals(country.Iso.ToLower()));
        }
    }
}
