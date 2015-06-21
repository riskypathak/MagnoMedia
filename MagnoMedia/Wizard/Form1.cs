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
        public Form1()
        {
            InitializeComponent();
            GetApplicationDetails();
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
            if (!StaticData.IsResume)
            {
                SetCurrentText("Analyzing Components...");
            }
            else
            {
                SetCurrentText("Resuming Vidsoom Installation...");
            }

            StaticData.Applications = ApplicationHelper.GetAllApplicableSoftWare();

            // this will be only done if no states already are there
            if (!StaticData.IsResume)
            {
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
                SetCurrentText("Initializing Setup Wizard");
                GotoHomeScreen();
            }
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
