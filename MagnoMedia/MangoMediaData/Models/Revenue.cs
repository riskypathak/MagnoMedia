using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class Revenue : DBEntity
    {
        [References(typeof(ThirdPartyApplication))]
        public int ApplicationId { get; set; }

        [Reference]
        public ThirdPartyApplication Application { get; set; }

        public DateTime Date { get; set; }

        [References(typeof(Country))]
        public int CountryId { get; set; }

        [Reference]
        public Country Country { get; set; }

        public float Value { get; set; }
    }
}
