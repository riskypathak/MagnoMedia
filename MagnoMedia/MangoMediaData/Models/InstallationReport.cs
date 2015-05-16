
namespace MagnoMedia.Data.Models
{

    public class InstallationReport : DBEntity
    {
        public int ThirdPartyApplicationId { get; set; }
        public string ThirdPartyApplicationState { get; set; }
        public string Message { get; set; }

        public string MachineUID { get; set; }

    }
}
