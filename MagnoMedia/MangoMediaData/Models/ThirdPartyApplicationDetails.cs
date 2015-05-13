using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class ThirdPartyApplicationDetails : DBEntity
    {
        //FK
        public int ThirdPartyApplicationId { get; set; }
        public string AgreementText { get; set; }
        public string Name { get; set; }
        public bool HasImage { get; set; }
        public string ImageUrl { get; set; }
        public string HtmlData { get; set; }

    }
}
