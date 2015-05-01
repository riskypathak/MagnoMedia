using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagnoMedia.Data.Models;

namespace MagnoMedia.Data.APIRequestDTO
{
   public  class InstallerData
    {
        public int ThirdPartyApplicationId { get; set; }
        public InstallationState ThirdPartyApplicationState { get; set; }
        public string Message { get; set; }
        public string MachineUID { get; set; }

    }
}
