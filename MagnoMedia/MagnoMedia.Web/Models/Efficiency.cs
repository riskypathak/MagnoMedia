using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MagnoMedia.Data.Models;

using System.Web.Mvc;
namespace MagnoMedia.Web.Models
{
    public class Efficiency
    {
        public Efficiency()
        {
            InstallerECPM = new List<string>();
            ApplicationECPM = new List<string>();
            InstallEfficiency = new List<string>();
        }
        public List<SelectListItem> ApplicationList { get; set; }
        public List<SelectListItem> ReferList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public string Referer { get; set; }
        public string Country { get; set; }
        public string Application { get; set; }
        public string SessionId { get; set; }
        public List<string> InstallerECPM { get; set; }
        public List<string> ApplicationECPM { get; set; }
        public List<string> InstallEfficiency { get; set; }
        public string UpdatedDate { get; set; }

    }
}