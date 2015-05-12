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

        public string CompleteRequestUri { get; set; }

        public string Referer { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public DateTime RequestDate { get; set; }

        public bool IsValid { get; set; }
    }
}
