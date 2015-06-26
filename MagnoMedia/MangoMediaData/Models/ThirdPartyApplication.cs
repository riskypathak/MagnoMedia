
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public class ThirdPartyApplication : DBEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool HasUrl { get; set; }

        public string InstallerName { get; set; }

        public string DownloadUrl { get; set; }

        public string Arguments { get; set; }

        [StringLengthAttribute(1000)]
        public string RegistryCheck { get; set; }

        [StringLengthAttribute(1000)]
        public string OtherNames { get; set; }

        public string AgreementText { get; set; }

        public bool HasImage { get; set; }

        public string ImageUrl { get; set; }

        public string HtmlData { get; set; }

        //Priority
        public int Order { get; set; }
    }
}
