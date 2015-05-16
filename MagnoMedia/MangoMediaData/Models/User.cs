﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class User : DBEntity
    {
        public string FingerPrint { get; set; }

        public string OSName { get; set; }

        public string BrowserName { get; set; }

        public string CountryName { get; set; }

        public string IP { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
