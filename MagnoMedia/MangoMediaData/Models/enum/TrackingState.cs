using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Data.Models
{
    public enum TrackingState
    {
        LandingPage,
        DownloadRequest,
        InstallInit,
        InstallStart,
        InstallComplete,
        InstallFail
    }
}
