
using ServiceStack.DataAnnotations;
using System;
namespace MagnoMedia.Data.Models
{
    public class UserAppTrack : DBEntity
    {
        [References(typeof(ThirdPartyApplication))]
        public int ApplicationId { get; set; }

        [Reference]
        public ThirdPartyApplication Application { get; set; }

        [References(typeof(User))]
        public int UserId { get; set; }

        [Reference]
        public User User { get; set; }

        [References(typeof(SessionDetail))]
        public int SessionDetailId { get; set; }

        [Reference]
        public SessionDetail SessionDetail { get; set; }

        public AppInstallState State { get; set; }

        public string Message { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
