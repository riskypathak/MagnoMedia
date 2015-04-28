using MagnoMedia.Data.APIResponseDTO;
using ServiceStack.DataAnnotations;
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
        [ForeignKey(typeof(CountryDBEntity))]
        public int  CountryId { get; set; }

        //[Reference]
        //public CountryDBEntity Country{ get; set; }

        //Priority
        public int Order { get; set; }

    }
}
