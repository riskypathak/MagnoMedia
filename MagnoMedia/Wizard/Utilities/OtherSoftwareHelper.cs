using Magno.Data;
using MagnoMedia;
using MagnoMedia.Data.Models;
using Microsoft.Win32;
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

        private const string RegistrySpliter = ",##,";

        internal static IEnumerable<ThirdPartyApplication> GetAllApplicableSoftWare(string SessionID)
        {

            // it will get all softwares to install in background based on users location
            // return some junk values as for now 

            List<ThirdPartyApplication> response = new List<ThirdPartyApplication>();

            string url = String.Format("software?request.SessionCode={0}", SessionID);

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


        internal static ThirdPartyApplication GetSoftWareDetails(int SoftWareId)
        {

            // it will get details for software to install


            ThirdPartyApplication response = new ThirdPartyApplication();

            string url = String.Format("software/{0}", SoftWareId);

            HttpResponseMessage apiResponse = HttpClientHelper.Get(url);

            if (apiResponse != null && apiResponse.IsSuccessStatusCode)
            {

                response = apiResponse.Content.ReadAsAsync<ThirdPartyApplication>().Result;

            }


            //TODO remove junk data 
            return new ThirdPartyApplication()
            {
                AgreementText = "Attribute Routing \n now provides an extensibility point called IDirectRouteProvider, which allows full control over how attribute routes are discovered and configured. An IDirectRouteProvider is responsible for providing a list of actions and controllers along with associated route information to specify exactly what routing configuration is desired for those actions. An IDirectRouteProvider implementation may be specified when calling MapAttributes/MapHttpAttributeRoutes.Customizing IDirectRouteProvider will be easiest by extending our default implementation, DefaultDirectRouteProvider. This class provides separate overridable virtual methods to change the logic for discovering attributes, creating route entries, and discovering route prefix and area prefix.Following are some examples on what you could do with this new extensibility point:"


            };

            return response;

        }

         internal static bool CheckRegistryExistance(ThirdPartyApplication sw)
        {
            // If no registry provided
            if (String.IsNullOrEmpty(sw.RegistryCheck))
            {
                return CheckApplicationExistance(sw.Name);
            }
            else
            {
                //Using ,##, to split registry keys
                string[] registryKeys = sw.RegistryCheck.Split(new String[] { RegistrySpliter }, StringSplitOptions.RemoveEmptyEntries);
                int keyFound = 0;
                foreach (var registryKey in registryKeys)
                {
                    using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(registryKey))
                        if (Key != null)
                        {
                            keyFound++;

                        }
                        else
                        {


                        }

                }
                if (keyFound > 0)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
        }

         internal static  bool CheckApplicationExistance(string appName)
        {

            string displayName;
            RegistryKey key;

            // search in: CurrentUser
            // https://social.msdn.microsoft.com/Forums/en-US/94c2f14d-c45e-4b55-9ba0-eb091bac1035/c-get-installed-programs
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
            }

            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
            }

            // search in: LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
            }

            // NOT FOUND
            return false;

        }

    }
}
