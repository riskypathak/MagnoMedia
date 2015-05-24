using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
  public  class SearchResult
    {
        public string Country { get; set; }
        public UserTrackState _UserTrackState { get; set; }
        public int LPCount { get; set; }
        public int DownLoadCount { get; set; }
    }
}
