﻿using ServiceStack.DataAnnotations;
using System;

namespace MagnoMedia.Data.Models
{
    public class SessionUserLog : DBEntity
    {
        [References(typeof(SessionDetail))]
        public int SessionDetailId { get; set; }

        [Reference]
        public SessionDetail SessionDetail { get; set; }

        [References(typeof(User))]
        public int UserId { get; set; }

        [Reference]
        public User User { get; set; }

        [References(typeof(OperatingSystem))]
        public int OsId { get; set; }

        [Reference]
        public OperatingSystem OperatingSystem { get; set; }

        [References(typeof(Browser))]
        public int BrowserId { get; set; }

        [Reference]
        public Browser Browser { get; set; }

        [References(typeof(Country))]
        public int CountryId { get; set; }

        [Reference]
        public Country Country { get; set; }

        public string IP { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
