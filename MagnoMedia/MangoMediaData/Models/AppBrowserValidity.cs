
using ServiceStack.DataAnnotations;
namespace MagnoMedia.Data.Models
{
    public class AppBrowserValidity: DBEntity
    {
        [References(typeof(ThirdPartyApplication))]
        public int ApplicationId { get; set; }

        [Reference]
        public ThirdPartyApplication Application { get; set; }

        [References(typeof(Browser))]
        public int BrowserId { get; set; }

        [Reference]
        public Browser Browser { get; set; }

    }
}
