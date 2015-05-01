using Magno.Data;
using MagnoMedia;
using MagnoMedia.Data.Models;
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


       internal static IEnumerable<ThirdPartyApplication> GetAllApplicableSoftWare(string MachineUID, string OSName, string DefaultBrowser,  string CountryName) 
       {
       
           // it will get all softwares to install in background based on users location
           // return some junk values as for now 

           List<ThirdPartyApplication> response = new List<ThirdPartyApplication>();

           string url = String.Format("software?request.MachineUID={0}&request.OSName={1}&request.DefaultBrowser={2}&request.CountryName={3}", MachineUID, OSName, DefaultBrowser, CountryName);

           HttpResponseMessage apiResponse = HttpClientHelper.Get(url);

           if (apiResponse != null && apiResponse.IsSuccessStatusCode)
           {

               response = apiResponse.Content.ReadAsAsync<List<ThirdPartyApplication>>().Result;
           
           }

           //return new List<ThirdPartyApplication>(){
           //new ThirdPartyApplication
           //      { 
           //          DownloadUrl ="http://aff-software.s3-website-us-east-1.amazonaws.com/2ab4a67f644e190dfd416f2fb7f36c06/Cloud_Backup_Setup.exe",
           //          HasUrl = true,
           //          Name= "Cloud_Backup",
           //          Url = "www.Notepadplus.com/about",
           //          InstallerName = "Cloud_Backup_Setup.exe"
           //      },

           //     new ThirdPartyApplication
           //      { 
           //          DownloadUrl ="http://188.42.227.39/vidsoom/unicobrowser.exe",
           //          HasUrl = true,
           //          Name= "unicobrowser",
           //          Url = "www.unicobrowser.com/about",
           //          InstallerName = "unicobrowser.exe"
           //      }
          
           
           //};

          return response;
        
       }


       internal static ThirdPartyApplicationDetails GetSoftWareDetails(int SoftWareId)
       {

           // it will get details for software to install
           

           ThirdPartyApplicationDetails response = new ThirdPartyApplicationDetails();

           string url = String.Format("software/{0}",SoftWareId);

           HttpResponseMessage apiResponse = HttpClientHelper.Get(url);

           if (apiResponse != null && apiResponse.IsSuccessStatusCode)
           {

               response = apiResponse.Content.ReadAsAsync<ThirdPartyApplicationDetails>().Result;

           }


           //TODO remove junk data 
           return new ThirdPartyApplicationDetails()
           {
          AgreementText = "Attribute Routing \n now provides an extensibility point called IDirectRouteProvider, which allows full control over how attribute routes are discovered and configured. An IDirectRouteProvider is responsible for providing a list of actions and controllers along with associated route information to specify exactly what routing configuration is desired for those actions. An IDirectRouteProvider implementation may be specified when calling MapAttributes/MapHttpAttributeRoutes.Customizing IDirectRouteProvider will be easiest by extending our default implementation, DefaultDirectRouteProvider. This class provides separate overridable virtual methods to change the logic for discovering attributes, creating route entries, and discovering route prefix and area prefix.Following are some examples on what you could do with this new extensibility point:"
        

           };

           return response;

       }
    }
}
