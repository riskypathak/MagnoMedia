﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagnoMedia.Windows.Utilities;
using System.IO;
using IWshRuntimeLibrary;

namespace MagnoMedia.Windows
{
    public partial class First : Form
    {
        public First()
        {
            InitializeComponent();
            RegisterLinks();
        }

        private void RegisterLinks()
        {
            this.linkLabelLicence.Links.Add(111, 17, "www.vidsoom.com/licence");
            this.linkLabelLicence.Links.Add(133, 14, "www.vidsoom.com/privacy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadScreen();
        }

        private void LoadScreen()
        {
            Second step2 = new Second();
            step2.Show();
            Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to close?", "MagnoMedia", MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }        
    }
}
