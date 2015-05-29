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
    public class ApplicationHelper
    {

        private const string RegistrySpliter = "|";

        internal static IEnumerable<ThirdPartyApplication> GetAllApplicableSoftWare(string SessionID)
        {

            // it will get all softwares to install in background based on users location
            // return some junk values as for now 

            List<ThirdPartyApplication> response = new List<ThirdPartyApplication>();

            string url = string.Format("software?SessionCode={0}", SessionID);

            HttpResponseMessage apiResponse = HttpClientHelper.Get(url);

            if (apiResponse != null && apiResponse.IsSuccessStatusCode)
            {

                response = apiResponse.Content.ReadAsAsync<List<ThirdPartyApplication>>().Result;

            }

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
                string[] registryKeys = sw.RegistryCheck.Split(new String[] { RegistrySpliter }, StringSplitOptions.RemoveEmptyEntries);
                bool keyFound = false;

                foreach (var registryKey in registryKeys)
                {
                    string registryRoot = string.Empty;
                    string value = registryKey.Split('\\').Last();
                    string root = string.Empty;

                    if (registryKey.StartsWith("HKEY_LOCAL_MACHINE"))
                    {
                        root = "HKLM";
                        registryRoot = registryKey.Replace("HKEY_LOCAL_MACHINE\\", string.Empty).Replace(string.Format("\\{0}",value), string.Empty);
                    }
                    else if  (registryKey.StartsWith("HKEY_CURRENT_USER"))
                    {
                        root = "HKCU";
                        registryRoot = registryKey.Replace("HKEY_CURRENT_USER\\", string.Empty).Replace(string.Format("\\{0}",value), string.Empty);
                    }
                       
                    if(RegistryValueExists(root, registryRoot, value))
                    {
                        keyFound = true;
                        break;
                    }
                }

                return keyFound;
            }
        }

        public static bool RegistryValueExists(string hive_HKLM_or_HKCU, string registryRoot, string valueName)
        {
            RegistryKey root;
            switch (hive_HKLM_or_HKCU.ToUpper())
            {
                case "HKLM":
                    root = Registry.LocalMachine.OpenSubKey(registryRoot, false);
                    break;
                case "HKCU":
                    root = Registry.CurrentUser.OpenSubKey(registryRoot, false);
                    break;
                default:
                    throw new System.InvalidOperationException("parameter registryRoot must be either \"HKLM\" or \"HKCU\"");
            }

            if (root != null)
            {
                if (root.GetValue(valueName) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool CheckApplicationExistance(string appName)
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

        internal static void PostApplicationStatus(int applicationID, AppInstallState state, string message = "")
        {
            HttpClientHelper.Post<UserAppTrack>(
                string.Format("Installer/SaveInstallerState?SessionCode={0}", StaticData.SessionCode),
        new UserAppTrack
        {
            Message = message,
            ApplicationId = applicationID,
            State = state,
        });
        }

        internal static void PostInstallerStatus(UserTrackState state, string message = "")
        {
            HttpClientHelper.Post<UserTrack>(
                string.Format("Installer/SaveState?SessionCode={0}", StaticData.SessionCode),
                new UserTrack
                {
                    Message = message,
                    State = state,
                });
        }

        internal static string CreatErrorMessage(Exception ex)
        {
            return ex.Message;
        }
    }
}
