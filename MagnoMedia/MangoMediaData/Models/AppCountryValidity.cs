
namespace MagnoMedia.Data.Models
{
    public class AppCountryValidity : DBEntity
    {
        //FK ThirdPartyApplicationId
        public int ThirdPartyApplicationId { get; set; }

        //FK CountryDBEntity
        //[ForeignKey(typeof(Country))]
        public int  CountryId { get; set; }

        //[Reference]
        //public CountryDBEntity Country{ get; set; }

        //Priority
        public int Order { get; set; }

    }
}
