﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magno.Data
{
    public class UserData
    {
        public int SessionID { get; set; }
        public string MachineUID { get; set; }
        public string OSName { get; set; }
        public string DefaultBrowser { get; set; }
        public string CountryName { get; set; }
    }
}
