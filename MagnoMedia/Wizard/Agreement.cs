using MagnoMedia.Data.DBEntities;
using MagnoMedia.Windows.Utilities;
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
    public partial class Agreement : Form
    {

        private int CurrentThirdPartySwIndex;
        public Agreement()
        {
            InitializeComponent();

        }

        public Agreement(int CurrentThirdPartySwIndex)
        {
            // TODO: Complete member initialization
            this.CurrentThirdPartySwIndex = CurrentThirdPartySwIndex;
            InitializeComponent();
            GetDetails(Form1.SWList.ElementAt(this.CurrentThirdPartySwIndex));
        }

        private void GetDetails(Magno.Data.ThirdPartyApplication thirdPartyApplication)
        {
            ThirdPartyApplicationDetails appDetails = OtherSoftwareHelper.GetSoftWareDetails(thirdPartyApplication.Id);

            //TODO Use Some HTML control instead of plain textbox/label 
            textBoxAgreementTXT.Text = appDetails.AgreementText;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.CurrentThirdPartySwIndex++;
            GetDetails(Form1.SWList.ElementAt(this.CurrentThirdPartySwIndex));
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to close?", "MagnoMedia", MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
