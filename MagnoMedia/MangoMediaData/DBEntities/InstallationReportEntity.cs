﻿using MagnoMedia.Data.APIResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.DBEntities
{
  
    public class InstallationReportEntity:DBEntity
    {

        public int ThirdPartyApplicationId { get; set; }
        public string ThirdPartyApplicationState { get; set; }
        public string Message { get; set; }

        public string MachineUID { get; set; }

    }
}