
namespace MagnoMedia.Data.Models
{
    public class UserAppTrack : DBEntity
    {
        //FK
        public int ThirdPartyApplicationId { get; set; }

        public string FingerPrint { get; set; }

        public AppInstallState State { get; set; }

        public string OtherDetails { get; set; }

        public string ErrorMessage { get; set; }

    }
}
