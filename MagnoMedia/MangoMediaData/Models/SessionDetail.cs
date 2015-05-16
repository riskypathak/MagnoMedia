using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class SessionDetail : DBEntity
    {
        public string SessionId { get; set; }

        public string FingerPrint { get; set; } //This will be used later to track user. This should be foreign key to User table

        public string CompleteRequestUri { get; set; }

        public string Referer { get; set; }

        public string RefererId { get; set; } //This will give us the refer who passes us the info. This should be FK to Referer table

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public DateTime RequestDate { get; set; }

        public bool IsValid { get; set; }
    }
}
