using MagnoMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagnoMedia.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            //Insert into SessionDetails
            SessionDetail session = new SessionDetail();
            session.SessionId = Session.SessionID;
            session.CompleteRequestUri = Request.Url.ToString();
            session.Referer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : null;
            session.RequestDate = DateTime.Now;
            session.UserAgent = Request.UserAgent != null ? Request.UserAgent.ToString() : null;
            session.IPAddress = Request.UserHostAddress;

            //Save into db here

            //Insert into tracking
            UserTrack userTrack = new UserTrack();
            userTrack.UpdatedDate = DateTime.Now;
            userTrack.SessionId = session.SessionId;
            userTrack.State = TrackingState.LandingPage;

            return View();
        }

        public ActionResult Download()
        {
            //Check here if session already exist in database table.
            //Also Check only in last 5 minutes. Because a session expire in 20 minutes by default so lets take a 1/4th value
            //Also as sessionid can be repeated so this will help us to track unique session in last 5 min utes

            //if(db.Sessions.Where(s=>s.SessionId == Session.SessionID && s.RequestDate > DateTime.Now.AddMinutes(-5)))
            //{
            //Create a folder here.

            string downloadFolderPath = Path.GetFullPath(string.Format("Temp//{0}", Session.SessionID));
            System.IO.Directory.CreateDirectory(downloadFolderPath);

            //Transfer all files from a static folder(//AppData/Application) to above created folder
            //1. the parent exe
            //2. the zipped files of child exes+dlls



            //redirect download link of above download folder path

            //insert new tracking
            UserTrack userTrack = null;// here find row on basis of sessionid
            userTrack.UpdatedDate = DateTime.Now;
            userTrack.SessionId = Session.SessionID;
            userTrack.State = TrackingState.LandingPage;




            //}

            //else redirect to Landing Page because we assume that user has not been come directly via LP


            return View();
        }


    }
}
