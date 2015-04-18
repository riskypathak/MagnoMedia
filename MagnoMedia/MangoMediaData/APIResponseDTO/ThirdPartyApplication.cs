using MagnoMedia.Data.APIResponseDTO;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magno.Data
{
    public class ThirdPartyApplication : DBEntity
    {
      
        public string Name { get; set; }
        public string Url { get; set; }
        public bool HasUrl { get; set; }
        public string InstallerName { get; set; }
        public string DownloadUrl { get; set; }
        public string Arguments { get; set; }
    }
}
