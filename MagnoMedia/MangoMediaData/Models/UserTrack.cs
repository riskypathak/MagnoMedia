using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class UserTrack : DBEntity
    {
        public string SessionId { get; set; }

        public string FingerPrint { get; set; }

        public UserTrackState State { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
