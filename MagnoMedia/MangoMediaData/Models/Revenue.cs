using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class Revenue : DBEntity
    {
        //[FK]
        public int AppId { get; set; }

        public DateTime Date { get; set; }

        public string CountryCode { get; set; }

        public float Value { get; set; }
    }
}
