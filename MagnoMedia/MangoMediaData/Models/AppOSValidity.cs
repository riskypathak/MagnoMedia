
using ServiceStack.DataAnnotations;
namespace MagnoMedia.Data.Models
{
   public class AppOSValidity: DBEntity
    {
        [References(typeof(ThirdPartyApplication))]
        public int ApplicationId { get; set; }

        [Reference]
        public ThirdPartyApplication Application { get; set; }

        [References(typeof(OperatingSystem))]
        public int OSId { get; set; }

        [Reference]
        public OperatingSystem OperatingSystem { get; set; }

    }
}
