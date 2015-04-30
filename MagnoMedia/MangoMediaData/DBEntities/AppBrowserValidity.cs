using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.DBEntities
{
    public class AppBrowserValidity
    {
        //FK
        public int ThirdPartyApplicationId { get; set; }

        //FK BrowserEntity
        public int BrowserId { get; set; }

        //Priority
        public int Order { get; set; }

    }
}
