using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
namespace VidSoom
{
    class Installer
    {
        private Guid UninstallGuid { get { return new Guid("47e66e72-d4da-4eef-9f6f-75661ae2fc38"); } }

        public int Install()
        {
            try
            {
                string path = ProgramFilesx86() + @"\VidSoom\";

                Directory.CreateDirectory(path);

                File.Copy(Application.StartupPath + @"\vidsoom.exe", path + "vidsoom.exe");
                File.Copy(Application.StartupPath + @"\Interop.WMPLib.dll", path + "Interop.WMPLib.dll");
                File.Copy(Application.StartupPath + @"\AxInterop.WMPLib.dll", path + "AxInterop.WMPLib.dll");
            }
            catch
            {
                return 3;
            }
            return CreateUninstaller();
        }

        public int Uninstall()
        {
            return RemoveUninstaller();
        }
        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }


        private int CreateUninstaller()
        {
            using (RegistryKey parent = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (parent == null)
                {
                    return 4; // throw new Exception("Uninstall registry key not found.");
                }
                try
                {
                    RegistryKey key = null;

                    try
                    {
                        string guidText = UninstallGuid.ToString("B");
                        key = parent.OpenSubKey(guidText, true) ??
                              parent.CreateSubKey(guidText);

                        if (key == null)
                        {
                            return 5; //throw new Exception(String.Format("Unable to create uninstaller '{0}\\{1}'", @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", guidText));
                        }

                        Assembly asm = GetType().Assembly;
                        Version v = asm.GetName().Version;
                        string exe = "\"" + ProgramFilesx86() + @"\VidSoom\vidsoom.exe" + "\"";

                        key.SetValue("DisplayName", "VidSoom");
                        key.SetValue("ApplicationVersion", v.ToString());
                        key.SetValue("Publisher", "VidSoom");
                        key.SetValue("DisplayIcon", exe);
                        key.SetValue("DisplayVersion", v.ToString(2));
                        key.SetValue("URLInfoAbout", "http://www.vidsoom.com");
                        key.SetValue("Contact", "support@vidsoom.com");
                        key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                        key.SetValue("UninstallString", exe + " /uninstall");
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 6;
                    //throw new Exception(
                    //    "An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.",
                    //    ex);
                }
            }
            return 0;
        }
        private int RemoveUninstaller()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true))
            {
                if (key == null)
                {
                    return 2;
                }
                try
                {
                    string guidText = UninstallGuid.ToString("B");
                    RegistryKey child = key.OpenSubKey(guidText);
                    if (child != null)
                    {
                        child.Close();
                        key.DeleteSubKey(guidText);
                    }
                }
                catch (Exception ex)
                {
                    return 3; //throw new Exception("An error occurred removing uninstall information from the registry.  The service was uninstalled will still show up in the add/remove program list.  To remove it manually delete the entry HKLM. " + @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" + "\\" + UninstallGuid, ex);
                }
            }
            return 0;
        }
    }
}
