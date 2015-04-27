using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.linkLabelLicence.Links.Add(111, 17, "www.magnomedia.com/licence");
            this.linkLabelLicence.Links.Add(133, 14, "www.magnomedia.com/privacy");
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Agreement step2 = new Agreement(CurrentThirdPartySwIndex:0);
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
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
