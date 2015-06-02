using MagnoMedia.Data.Models;
using MagnoMedia.Windows.Model;
using MagnoMedia.Windows.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace MagnoMedia.Windows
{
    public partial class Form1 : Form
    {
        private bool IsResume;

        public Form1()
        {
            InitializeComponent();

            StaticData.IsResume = true;
            bool stateReadSuccessFlag = false;
            try
            {
                //string applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                //string jsonconfigFile = Path.Combine(applicationDataFolder, "vidsoomConfig.json");
                //string jsonContent = File.ReadAllText(jsonconfigFile);
                //InstallerHelper.ThirdPartyApplicationStates = JsonConvert.DeserializeObject<List<ThirdPartyApplicationState>>(jsonContent);
                //if (InstallerHelper.ThirdPartyApplicationStates != null)
                //{
                //    IsResume = stateReadSuccessFlag = true;
                //}
            }
            catch
            {
                stateReadSuccessFlag = false;

            }
            finally
            {
                if (!stateReadSuccessFlag)
                {
                    GetApplicationDetails();
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            if (IsResume)
                LoadScreen();
            base.OnShown(e);
        }
        private void LoadScreen()
        {
            Second step2 = new Second();
            step2.Show();
            this.Hide();
        }

        private void GetApplicationDetails()
        {
            Thread threadBackground = new Thread(() => LoadSoftwareList());
            threadBackground.Start();
        }

        private void LoadSoftwareList()
        {
            string currentText = "Analyzing Components...";
            SetCurrentText(currentText);

            StaticData.Applications = ApplicationHelper.GetAllApplicableSoftWare();

            foreach (ThirdPartyApplication application in StaticData.Applications)
            {
                if (StaticData.ApplicationStates.Count() >= 8)
                {
                    ApplicationHelper.PostApplicationStatus(application.Id, AppInstallState.NotInstalledDueToOverLimit);
                }

                //check Registry if already exist in m/c
                if (!ApplicationHelper.IsAlreadyExist(application))
                {
                    ApplicationState applicationState = new ApplicationState
                    {
                        ApplicationId = application.Id
                    };

                    StaticData.ApplicationStates.Add(applicationState);
                }
                else
                {
                    ApplicationHelper.PostApplicationStatus(application.Id, AppInstallState.AlreadyExist);
                }
            }

            currentText = "Initializing Setup Wizard";
            SetCurrentText(currentText);
            GotoHomeScreen();
        }

        private void SetCurrentText(string currentText)
        {
            this.Invoke((MethodInvoker)delegate
            {
                labelInitialStep.Text = currentText;
            });
        }

        private void GotoHomeScreen()
        {
            this.Invoke((MethodInvoker)delegate
            {
                First HomeScreen = new First();
                HomeScreen.Show();
                this.Hide();
            });
        }

        void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}
