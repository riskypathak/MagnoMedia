using MagnoMedia.Data.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.APIResponseDTO
{
   public class Browser:DBEntity
    {
        public string BrowserName { get; set; }
    }
}
