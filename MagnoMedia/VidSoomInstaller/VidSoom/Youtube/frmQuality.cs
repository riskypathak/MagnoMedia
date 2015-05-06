using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VidSoom;

namespace YouTube
{
    public partial class frmQuality : Form
    {
        public YouTubeVideoQuality quality;
        private List<YouTubeVideoQuality> urls;
        public frmQuality(List<YouTubeVideoQuality> urls, string url)
        {
            InitializeComponent();
            this.urls = urls;
            foreach (var item in urls)
            {
                string videoExtention = item.Extention;
                string videoDimension = formatSize(item.Dimension);
                string videoSize = formatSizeBinary(item.VideoSize);
                //string videoTitle = item.VideoTitle.Replace(@"\", "").Replace("&#39;", "'").Replace("&quot;", "'").Replace("&lt;", "(").Replace("&gt;", ")").Replace("+", " ");

                quality_ComboBox.Items.Add(String.Format("{0} ({1}) - {2}", videoExtention.ToUpper(), videoDimension, videoSize));
                quality_ComboBox.Text = quality_ComboBox.Items[0].ToString();
                quality_ComboBox.Enabled = true;

                lblTitulo.Text = item.VideoTitle;
                pictureBox2.ImageLocation = string.Format("http://i3.ytimg.com/vi/{0}/default.jpg", Helper.GetVideoIDFromUrl(url));
            }
        }
        private string formatSizeBinary(Int64 size, Int32 decimals = 2)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            double formattedSize = size;
            Int32 sizeIndex = 0;
            while (formattedSize >= 1024 & sizeIndex < sizes.Length)
            {
                formattedSize /= 1024;
                sizeIndex += 1;
            }
            return string.Format("{0} {1}", Math.Round(formattedSize, decimals).ToString(), sizes[sizeIndex]);
        }
        private string formatSize(Size value)
        {
            string s = value.Height >= 720 ? " HD" : "";
            return value.Width + "x" + value.Height + s;
        }
        private void frmQuality_Load(object sender, EventArgs e)
        {
            Language.translateForm(this);
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                if (quality_ComboBox.SelectedIndex == -1)
                    throw new Exception(Language.getString(this, "msgSeleccione"));
                quality = urls[quality_ComboBox.SelectedIndex];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            btnDescargar.PerformClick();
        }
    }
}
