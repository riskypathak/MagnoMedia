using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class AppReferValidity : DBEntity
    {
        [References(typeof(ThirdPartyApplication))]
        public int ApplicationId { get; set; }

        [Reference]
        public ThirdPartyApplication Application { get; set; }

        [References(typeof(Referer))]
        public int ReferId { get; set; }

        [Reference]
        public Referer Refer { get; set; }

        //Priority
        public int Order { get; set; }
    }
}
