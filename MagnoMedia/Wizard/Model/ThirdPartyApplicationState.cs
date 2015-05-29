using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Windows.Model
{
    public class ApplicationState
    {
        public int ApplicationId { get; set; }

        public bool IsDownloaded { get; set; }

        public bool IsInstalled { get; set; }
    }
}
