using MagnoMedia.Data.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.APIResponseDTO
{
    public class CountryDBEntity : DBEntity
    {
      public string CountryName { get; set; }
      public string CountryCode { get; set; }

    }
}
