
namespace MagnoMedia.Data.Models
{
   public class AppOSValidity: DBEntity
    {
        //FK
        public int ThirdPartyApplicationId { get; set; }

        //FK OSEntity
        public int OSID { get; set; }

        //Priority
        public int Order { get; set; }

    }
}
