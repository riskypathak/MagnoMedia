using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagnoMedia.Web.Models
{
    public class UserInstallRequest
    {
        public string SessionID { get; set; }
        public string MachineUID { get; set; }
        public string OSName { get; set; }
        public string DefaultBrowser { get; set; }
        public string CountryName { get; set; }
    }
}