using MagnoMedia.Common;
using MagnoMedia.Windows.Utilities;
using System;
using System.Windows.Forms;
using System.Linq;
using IWshRuntimeLibrary;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using MagnoMedia.Windows.Model;

namespace MagnoMedia.Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

#if DEBUG
            var list = args.ToList();
            list.Add("e50f442a-2c72-4a43-8343-d9feeebc9f3b");
            //list.Add("resume");
            args = list.ToArray();
#endif

            if (args.Length == 1)
            {
                if (args[0] == "resume")
                {
                    string applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string jsonconfigFile = Path.Combine(applicationDataFolder, "vidsoomconfig");
                    string jsonContent = System.IO.File.ReadAllText(jsonconfigFile);

                    try
                    {
                        JToken jt = JToken.Parse(jsonContent);
                        StaticData.ApplicationStates = JsonConvert.DeserializeObject<List<ApplicationState>>(jsonContent);
                        StaticData.IsResume = true;
                    }
                    catch (Exception)
                    {
                        StaticData.SessionCode = jsonContent;
                    }
                }
                else
                {
                    StaticData.SessionCode = args[0];
                }
            }
            else
            {
                return;
            }

            Logging.Log.Info("Application Started");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ApplicationExit += new EventHandler(OnProcessExit);
            Application.Run(new Form1());

            //risky
            //OnProcessExit(null, null);
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            SaveState();
            SaveAppShortCut();
        }

        static void SaveState()
        {
            string applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string jsonconfigFile = Path.Combine(applicationDataFolder, "vidsoomconfig");

            string json = string.Empty;

            if (StaticData.ApplicationStates.Count > 0 && StaticData.ApplicationStates.Any(a => a.IsDownloaded == true))
            {
                json = JsonConvert.SerializeObject(StaticData.ApplicationStates, Formatting.Indented);
            }
            else
            {
                json = StaticData.SessionCode;
            }
            System.IO.File.WriteAllText(jsonconfigFile, json);
        }

        static void SaveAppShortCut()
        {
            string desktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            WshShell shell = new WshShell();
            string shortcutAddress = desktopDirectory + @"\Vidsoom.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Resume Vidsoom Installation";
            shortcut.TargetPath = string.Format("\"{0}\"", Application.ExecutablePath);
            shortcut.Arguments = "resume";
            shortcut.Save();
        }
    }
}
