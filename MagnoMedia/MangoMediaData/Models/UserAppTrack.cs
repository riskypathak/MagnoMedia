
using ServiceStack.DataAnnotations;
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

        public AppInstallState State { get; set; }

        public string OtherDetails { get; set; }

        public string ErrorMessage { get; set; }

    }
}
