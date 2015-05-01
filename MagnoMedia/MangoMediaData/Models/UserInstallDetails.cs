using MagnoMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
   public class UserInstallDetails:DBEntity
    {

        //FK
        public int ThirdPartyApplicationId { get; set; }

        public string MachineFingerPrint { get; set; }

        public InstallationState State { get; set; }

        public string OtherDetails { get; set; }

    }
}
