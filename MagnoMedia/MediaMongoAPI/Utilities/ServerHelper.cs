﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagnoMedia.Web.Api.Utilities
{
    public class ServerHelper
    {

        public static string GetClinetIpAddress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
    }
}