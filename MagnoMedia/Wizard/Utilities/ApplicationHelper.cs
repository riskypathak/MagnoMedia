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

        internal static IEnumerable<ThirdPartyApplication> GetAllApplicableSoftWare()
        {
            // it will get all softwares to install in background based on users location
            // return some junk values as for now 

            List<ThirdPartyApplication> response = new List<ThirdPartyApplication>();

            string url = string.Format("software?SessionCode={0}&UserCode={1}", StaticData.SessionCode, StaticData.UserCode);

            HttpResponseMessage apiResponse = HttpClientHelper.Get(url);

            if (apiResponse != null && apiResponse.IsSuccessStatusCode)
            {
                response = apiResponse.Content.ReadAsAsync<List<ThirdPartyApplication>>().Result;
            }

            return response;
        }

        internal static bool IsAlreadyExist(ThirdPartyApplication app)
        {
            // If no registry provided
            if (string.IsNullOrEmpty(app.RegistryCheck))
            {
                return IsExistWithOtherMethods(app.OtherNames);
            }
            else
            {
                return IsRegistryExist(app);
            }
        }

        private static bool IsRegistryExist(ThirdPartyApplication app)
        {
            string[] registryKeys = app.RegistryCheck.Split(new String[] { RegistrySpliter }, StringSplitOptions.RemoveEmptyEntries);
            bool keyFound = false;

            foreach (var registryKey in registryKeys)
            {
                string registryRoot = string.Empty;
                string value = registryKey.Split('\\').Last();
                string root = string.Empty;

                if (registryKey.StartsWith("HKEY_LOCAL_MACHINE"))
                {
                    root = "HKLM";
                    registryRoot = registryKey.Replace("HKEY_LOCAL_MACHINE\\", string.Empty).Replace(string.Format("\\{0}", value), string.Empty);
                }
                else if (registryKey.StartsWith("HKEY_CURRENT_USER"))
                {
                    root = "HKCU";
                    registryRoot = registryKey.Replace("HKEY_CURRENT_USER\\", string.Empty).Replace(string.Format("\\{0}", value), string.Empty);
                }

                if (RegistryValueExists(root, registryRoot, value))
                {
                    keyFound = true;
                    break;
                }
            }

            return keyFound;
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

        internal static bool IsExistWithOtherMethods(string appOtherNames)
        {
            if (!string.IsNullOrEmpty(appOtherNames))
            {
                foreach (string appName in appOtherNames.Split(new string[] { RegistrySpliter }, StringSplitOptions.RemoveEmptyEntries).ToArray())
                {
                    string displayName;
                    RegistryKey key;


                    // search in: CurrentUser
                    // https://social.msdn.microsoft.com/Forums/en-US/94c2f14d-c45e-4b55-9ba0-eb091bac1035/c-get-installed-programs

                    //different for 32 bit and 64 bit
                    key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

                    key = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = Convert.ToString(subkey.GetValue("DisplayName"));

                        if (displayName != null && appName.Trim().Equals(displayName.Trim(), StringComparison.OrdinalIgnoreCase) == true)
                        {
                            return true;
                        }
                    }

                    // search in: LocalMachine_32
                    //different for 32 bit and 64 bit
                    key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

                    key = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = Convert.ToString(subkey.GetValue("DisplayName"));

                        if (displayName != null && appName.Trim().Equals(displayName.Trim(), StringComparison.OrdinalIgnoreCase) == true)
                        {
                            return true;
                        }
                    }

                    // search in: LocalMachine_64
                    //different for 32 bit and 64 bit
                    key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

                    key = key.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                    foreach (String keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subkey = key.OpenSubKey(keyName);
                        displayName = Convert.ToString(subkey.GetValue("DisplayName"));

                        if (displayName != null && appName.Trim().Equals(displayName.Trim(), StringComparison.OrdinalIgnoreCase) == true)
                        {
                            return true;
                        }
                    }
                }
            }

            // NOT FOUND
            return false;
        }

        internal static void PostApplicationStatus(int applicationID, AppInstallState state, string message = "")
        {
            HttpClientHelper.Post<UserAppTrack>(
                string.Format("Installer/SaveInstallerState?SessionCode={0}&UserCode={1}", StaticData.SessionCode, StaticData.UserCode),
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
                string.Format("Installer/SaveState?SessionCode={0}&UserCode={1}", StaticData.SessionCode, StaticData.UserCode),
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
