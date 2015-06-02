using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;

namespace MagnoMedia.Web.Api.Utilities
{
    public class ServerHelper
    {
        public static string GetClientIPAddress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        public static string GetCountryCode(string ip)
        {
            using(WebClient client = new WebClient())
            {
                string response = client.DownloadString(string.Format("http://188.164.255.74/na.php?ip={0}", ip));
                if(!string.IsNullOrEmpty(response) && !response.Contains("**"))
                {
                    return Convert.ToString(Json.Decode(response).Country);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}