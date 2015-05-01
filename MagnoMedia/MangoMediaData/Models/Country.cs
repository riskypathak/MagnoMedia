using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class Country : DBEntity
    {
        public string Iso { get; set; }
        public string Country_name { get; set; }

    }
}
