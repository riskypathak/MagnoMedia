﻿using MagnoMedia.Data.Models;
using MagnoMedia.Windows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Windows.Utilities
{
    public class StaticData
    {
        public static bool IsResume { get; set; }

        public static IEnumerable<ThirdPartyApplication> Applications;

        public static List<ApplicationState> ApplicationStates = new List<ApplicationState>();

        public static string SessionCode { get; set; }

        public static string UserCode = MachineHelper.UniqueIdentifierValue();

#if DEBUG
        public static string ApiHost = "http://localhost:4387/api";
#else
        //risky
        public static string ApiHost = "http://188.42.227.39/vidsoom/api";
#endif

        public static int WaitMinutes = 15;

        public static int RetryInMilliSeconds = 15;
    }
}
