
namespace MagnoMedia.Data.Models
{
    public class AppBrowserValidity: DBEntity
    {
        //FK
        public int ThirdPartyApplicationId { get; set; }

        //FK BrowserEntity
        public int BrowserId { get; set; }

        //Priority
        public int Order { get; set; }

    }
}
