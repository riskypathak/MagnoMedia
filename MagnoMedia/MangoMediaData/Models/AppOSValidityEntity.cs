using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
   public class AppOSValidityEntity
    {
        //FK
        public int ThirdPartyApplicationId { get; set; }

        //FK OSEntity
        public int OSID { get; set; }

        //Priority
        public int Order { get; set; }

    }
}
