using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Windows.Model
{
    public class ThirdPartyApplicationState
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string InstallerName { get; set; }
        public string DownloadUrl { get; set; }
        public string Arguments { get; set; }
        public string RegistoryCheck { get; set; }
        public bool IsDownloaded { get; set; }
        public string SessionID { get; set; }
    }
}
