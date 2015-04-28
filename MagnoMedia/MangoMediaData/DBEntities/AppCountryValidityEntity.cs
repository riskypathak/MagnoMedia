using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.DBEntities
{
    public class AppCountryValidityEntity : DBEntity
    {
        //FK ThirdPartyApplicationId
        public int ThirdPartyApplicationId { get; set; }

        //FK CountryDBEntity
        public int  CountryCode { get; set; }

        //Priority
        public int Order { get; set; }

    }
}
