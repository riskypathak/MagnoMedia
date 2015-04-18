using Magno.Data;
using MagnoMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MagnoMedia.Windows.Utilities
{
   public class OtherSoftwareHelper
    {


       internal static IEnumerable<ThirdPartyApplication> GetAllApplicableSoftWare(string MachineUID, string OSName, string DefaultBrowser) 
       {
       
           // it will get all softwares to install in background based on users location
           // return some junk values as for now 

           List<ThirdPartyApplication> response = new List<ThirdPartyApplication>();

           string url = String.Format("software?request.MachineUID={0}&request.OSName={1}&request.DefaultBrowser={2}", MachineUID, OSName, DefaultBrowser);

           HttpResponseMessage apiResponse = HttpClinetHelper.Get(url);

           if (apiResponse != null && apiResponse.IsSuccessStatusCode)
           {

               response = apiResponse.Content.ReadAsAsync<List<ThirdPartyApplication>>().Result;
           
           }

           //return new List<ThirdPartyApplication>(){
           //new ThirdPartyApplication
           //      { 
           //          DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
           //          HasUrl = true,
           //          Name= "Robots",
           //          Url = "www.Notepadplus.com/about",
           //          InstallerName = "SkypeSetup.exe"
           //      },
           //      new ThirdPartyApplication
           //      { 
           //          DownloadUrl ="http://download.skype.com/cc8c0832c80579731d528a2dabcb134c/SkypeSetup.exe",
           //          Name= "Robots2",
           //          Url = "www.Notepadplus.com/about",
           //          InstallerName = "SkypeSetup.exe"
           //      }
          
           
           //};

          return response;
        
       }
    }
}
