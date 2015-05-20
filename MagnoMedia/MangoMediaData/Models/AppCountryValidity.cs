
using ServiceStack.DataAnnotations;
namespace MagnoMedia.Data.Models
{
    public class AppCountryValidity : DBEntity
    {
        [References(typeof(ThirdPartyApplication))]
        public int ApplicationId { get; set; }

        [Reference]
        public ThirdPartyApplication Application { get; set; }

        [References(typeof(Country))]
        public int  CountryId { get; set; }

        [Reference]
        public Country Country { get; set; }

        //Priority
        public int Order { get; set; }

    }
}
