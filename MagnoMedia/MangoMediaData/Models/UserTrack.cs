using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class UserTrack : DBEntity
    {
        [References(typeof(SessionDetail))]
        public int SessionDetailId { get; set; }

        [Reference]
        public SessionDetail SessionDetail { get; set; }

        [References(typeof(User))]
        public int? UserId { get; set; }

        [Reference]
        public User User { get; set; }

        public UserTrackState State { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Message { get; set; }
    }
}
