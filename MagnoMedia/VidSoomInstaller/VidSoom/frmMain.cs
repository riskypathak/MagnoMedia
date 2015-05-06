using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyDownloader.Core;
using MyDownloader.Protocols;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Net;
using MyDownloader.PersistedList;

namespace VidSoom
{
    public partial class frmMain : Form
    {
        Dictionary<Downloader, DataGridViewRow> rowDownloads;
        DateTime inicio;
        PersistedListExtension PLE;
        public static Downloader PreviewDownload;

        double PreviewTransfered;

        public frmMain()
        {
            Form.CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
        }
        
        public void setLanguage()
        {

            Language.currentLang = (Language.eLang) Properties.Settings.Default.Idioma;

            Language.translateForm(this);
        }

        

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Ruta == "")
            {
                Properties.Settings.Default.Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Downloads\\";
                
                Properties.Settings.Default.Save();
            }
            if (!Directory.Exists(Properties.Settings.Default.Ruta))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Ruta);
            }

            ProtocolProviderFactory.RegisterProtocolHandler("http", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("https", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("ftp", typeof(FtpProtocolProvider));


            inicio = DateTime.Now;
 
            rowDownloads = new Dictionary<Downloader, DataGridViewRow>();

            setLanguage();


            PLE = new PersistedListExtension();

            DataGridViewRow row;

            foreach (Downloader D in DownloadManager.Instance.Downloads)
            {
                row = grdDatos.Rows[grdDatos.Rows.Add()];
                row.Tag = D;

                rowDownloads.Add(D, row);

                D.updateRow(row);
            }

            checkPausa();

            /*Thread checkUpdates = new Thread(this.checkUpdate);
            checkUpdates.Start();*/
        }

        void checkUpdate()
        {
            WebClient client = new WebClient();
            String version = "VidSoom@1.0.0"; //client.DownloadString("http://localhost/version.txt");

            //this.Text = version;
            if (version.StartsWith("VidSoom@"))
            {
                if (version.Substring(7) != Application.ProductVersion)
                {
                    if (MessageBox.Show(Language.getString(this, "msgNewVersion"), "VidSoom", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        addItem("http://www.vidsoom.com/vidsoom_setup.exe");
                    }
                }
            }
        }

        private void tURL_Enter(object sender, EventArgs e)
        {
            if (tURL.Text == Language.getString(this, "tURL"))
                tURL.Text = "";
        }

        private void tURL_Leave(object sender, EventArgs e)
        {
            if (tURL.Text == "")
                tURL.Text = Language.getString(this, "tURL");
        }

        public void addItem(string url)
        {
            DataGridViewRow row;
            if (url.IndexOf("://") < 0)
            {
                url = "http://" + url;
            }


            try
            {
                Downloader download = DownloadManager.Instance.Add(ResourceLocation.FromURL(url), null, Properties.Settings.Default.Ruta, 1, false);



                //row = new DataGridViewRow { Tag = download };

                row = grdDatos.Rows[grdDatos.Rows.Add()];
                row.Tag = download;
                rowDownloads.Add(download, row);


                download.Start();
                download.updateRow(row);
            } catch (Exception ex) {
                MessageBox.Show(Language.getString(this, "msgBadProtocol"), "VidSoom", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void refreshGrid()
        {
            DataGridViewRow row;
            double velocidad = 0;
            int descargando = 0;
            int total = 0;
            foreach (Downloader item in DownloadManager.Instance.Downloads)
            {
                try
                {
                    row = rowDownloads[item];
                    item.updateRow(row);
                    velocidad += item.Rate;
                    if (item.State == DownloaderState.Working) descargando++;
                    if (item.State != DownloaderState.Ended) total++;
                }
                catch { }
            }
            sVelocidad.Text = Language.getString(this, "sVelocidad") + Downloader.convertRango(velocidad, velocidad) + Downloader.nameRango(velocidad) + "/s";
            sDescargando.Text = Language.getString(this, "sDescargando") + descargando + "/" + total;

        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            addItem(tURL.Text);
        }

        private void tRefresh_Tick(object sender, EventArgs e)
        {
            refreshGrid();
            sHora.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            TimeSpan ts = (DateTime.Now - inicio);
            sTiempo.Text = Language.getString(this, "sTiempo") + ((int)ts.TotalHours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0') + " / " + Language.getString(this, "Descargado") + ": " + Downloader.convertRango(Downloader.totalDescargado, Downloader.totalDescargado) + Downloader.nameRango(Downloader.totalDescargado);
            pBarra.Refresh();
            if (PreviewDownload != null)
            {
                lblMediPos.Text = WMP.Ctlcontrols.currentPositionString;
                lblMediaTop.Text = WMP.currentMedia.durationString;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnPausa_Click(object sender, EventArgs e)
        {
            if (grdDatos.SelectedRows.Count > 0)
            {
                Downloader d;
                foreach (DataGridViewRow row in grdDatos.SelectedRows)
                {
                    d = (Downloader)row.Tag;
                    if ((int)btnPausa.Tag == 0)
                    {
                        d.Pause();
                    }
                    else
                    {
                        d.Start();
                    }
                }
                checkPausa();
            }
        }

        private void grdDatos_SelectionChanged(object sender, EventArgs e)
        {
            checkPausa();
        }

        void checkPausa()
        {
            Downloader d;
            int pausados = 0;
            foreach (DataGridViewRow row in grdDatos.SelectedRows)
            {
                if (row.Tag != null)
                {
                    d = (Downloader)row.Tag;
                    if (d.State == DownloaderState.Paused || d.State == DownloaderState.Prepared || d.State == DownloaderState.EndedWithError)
                    {
                        pausados++;
                    }
                }
            }
            if (pausados == grdDatos.SelectedRows.Count)
            {
                btnPausa.Tag = 1;
                btnPausa.Text = Language.getString(this, "btnPausa_Continuar");
                btnPausa.Image = global::VidSoom.Iconos.control_play;
            }
            else
            {
                btnPausa.Tag = 0;
                btnPausa.Text = Language.getString(this, "btnPausa");
                btnPausa.Image = global::VidSoom.Iconos.control_pause;
            }
            cmPausa.Text = btnPausa.Text;
            cmPausa.Image = btnPausa.Image;
        }

        private void cmPausa_Click(object sender, EventArgs e)
        {
            btnPausa.PerformClick();
        }

        private void cmEliminar_Click(object sender, EventArgs e)
        {
            btnEliminar.PerformClick();
        }

        private void cmPegar_Click(object sender, EventArgs e)
        {
            btnPegar.PerformClick();
        }
        public static List<string> FormatUrls(string input)
        {
            List<string> output = new List<string>();
            Regex regx = new Regex("http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*([a-zA-Z0-9\\?\\#\\=\\/]){1})?", RegexOptions.IgnoreCase);

            MatchCollection mactches = regx.Matches(input);

            foreach (Match match in mactches)
            {
                output.Add(match.Value);
            }
            return output;
        }

        private void btnPegar_Click(object sender, EventArgs e)
        {
            List<string> urls = FormatUrls(Clipboard.GetText());
            foreach (string url in urls)
            {
                addItem(url);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grdDatos.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(Language.getString(this, "Confirm_Eliminar"), "VidSoom", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    Downloader d;
                    foreach (DataGridViewRow row in grdDatos.SelectedRows)
                    {
                        d = (Downloader)row.Tag;
                        DownloadManager.Instance.RemoveDownload(d);
                        if (File.Exists(d.LocalFile))
                        {
                            try
                            {
                                File.Delete(d.LocalFile);
                            }
                            catch { }

                        }
                        rowDownloads.Remove(d);
                        grdDatos.Rows.Remove(row);
                    }
                    checkPausa();
                }
            }
        }

        private void btnDescargas_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = Properties.Settings.Default.Ruta;
                prc.Start();
            }
            catch
            {
                MessageBox.Show("Path doesn't exist.", "VidSoom", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmAbrir_Click(object sender, EventArgs e)
        {
            if (grdDatos.SelectedRows.Count > 0) {
           
                string filePath = ((Downloader)grdDatos.SelectedRows[0].Tag).LocalFile;
                if (!File.Exists(filePath))
                {
                    return;
                }

               string argument = @"/select, " + filePath;

               System.Diagnostics.Process.Start("explorer.exe", argument);

            }
        }

        private void grdDatos_DoubleClick(object sender, EventArgs e)
        {
            cmAbrir.PerformClick();
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            frmConfiguracion formConfiguracion;
            formConfiguracion = new frmConfiguracion();
            if (formConfiguracion.ShowDialog() == DialogResult.OK)
            {
                setLanguage();
            }
        }

        private void pegarEnlacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPegar.PerformClick();
        }

        private void abrirCarpetaDeDescargasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDescargas.PerformClick();
        }

        private void mmConfiguracion_Click(object sender, EventArgs e)
        {
            btnConfiguracion.PerformClick();
        }

        private void mmEliminar_Click(object sender, EventArgs e)
        {
            btnEliminar.PerformClick();
        }

        private void mmPausar_Click(object sender, EventArgs e)
        {
            btnPausa.PerformClick();
        }

        private void mmSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lstCarpetas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DataGridViewRow row;
            if (e.Node.ImageIndex == 0)
            {
                rowDownloads.Clear();
                grdDatos.Rows.Clear();
                foreach (Downloader download in DownloadManager.Instance.Downloads)
                {
                    row = grdDatos.Rows[grdDatos.Rows.Add()];
                    row.Tag = download;
                    rowDownloads.Add(download, row);
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PLE.Dispose();
        }

        private void pBarra_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            int puntoProgress = 0;
            int puntoProgress2 = 0;

            if (WMP.currentMedia != null && WMP.currentMedia.duration > 0) 
            {
                puntoProgress = (int)((pBarra.Width - 6) * WMP.Ctlcontrols.currentPosition / WMP.currentMedia.duration);
                puntoProgress2 = (int)((pBarra.Width - 4 - 23) * WMP.Ctlcontrols.currentPosition / WMP.currentMedia.duration); 
            }
            e.Graphics.DrawImage(global::VidSoom.Iconos.barraizq, 0, 0, 6, 9);
            e.Graphics.DrawImage(global::VidSoom.Iconos.barrallena, 6, 0, puntoProgress2, 9);
            e.Graphics.DrawImage(global::VidSoom.Iconos.barravacia, 6 + puntoProgress2, 0, pBarra.Width - 6 - puntoProgress2, 9);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 71,77,83)), 6 + puntoProgress, 2, (int)(pBarra.Width * PreviewTransfered / 100) - 6 - puntoProgress, 6);
            e.Graphics.DrawImage(global::VidSoom.Iconos.barrader, pBarra.Width - 5, 0);
            e.Graphics.DrawImage(global::VidSoom.Iconos.barratap, 3 + puntoProgress2, 0);
        }

        private void tURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                btnDescargar.PerformClick();
            }
        }

        private void btnPrevisualizar_Click(object sender, EventArgs e)
        {
            PreviewDownload = (Downloader)grdDatos.SelectedRows[0].Tag;
            PreviewTransfered = PreviewDownload.Progress;

            WMP.Ctlcontrols.stop();
            WMP.close();
            File.Copy(PreviewDownload.LocalFile, Application.StartupPath + "\\temp", true);

            WMP.URL = Application.StartupPath + "\\temp"; //PreviewDownload.LocalFile; //"C:\\Users\\Javier\\Documents\\Downloads\\Nutella Handbag.mp4"; 
            WMP.Ctlcontrols.play();
            btnPlay.Image = global::VidSoom.Iconos.playdown;
        }

        private void mmPrevisualizar_Click(object sender, EventArgs e)
        {
            btnPrevisualizar.PerformClick();
        }

        private void pBarra_Click(object sender, EventArgs e)
        {

        }

        private void cmPrevisualizar_Click(object sender, EventArgs e)
        {
            btnPrevisualizar.PerformClick();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            WMP.Ctlcontrols.stop();
            WMP.close();
            btnPlay.Image = global::VidSoom.Iconos.playup;
        }

        private void btnStop_MouseEnter(object sender, EventArgs e)
        {
            btnStop.Image = global::VidSoom.Iconos.stopdown;
        }

        private void btnStop_MouseLeave(object sender, EventArgs e)
        {
            btnStop.Image = global::VidSoom.Iconos.stopup;
        }

        private void btnPlay_MouseEnter(object sender, EventArgs e)
        {
            btnPlay.Image = global::VidSoom.Iconos.playdown;
        }

        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {
            if (WMP.playState != WMPLib.WMPPlayState.wmppsPlaying) btnPlay.Image = global::VidSoom.Iconos.playup;
        }

        private void btnPrev_MouseEnter(object sender, EventArgs e)
        {
            btnPrev.Image = global::VidSoom.Iconos.revdown;
        }

        private void btnPrev_MouseLeave(object sender, EventArgs e)
        {
            btnPrev.Image = global::VidSoom.Iconos.revup;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying) {
                WMP.Ctlcontrols.pause();
            } else {
                WMP.Ctlcontrols.play();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            WMP.Ctlcontrols.currentPosition = 0;
        }

        private void pBarra_MouseUp(object sender, MouseEventArgs e)
        {
            int X = e.X;
            if (X < 10) X = 10;
            if (X > pBarra.Width - 3) X = pBarra.Width - 3;
            WMP.Ctlcontrols.currentPosition = (X - 10) * WMP.currentMedia.duration / (pBarra.Width - 23);
        }

        private void WMP_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (WMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                btnPlay.Image = global::VidSoom.Iconos.playdown;
            }
            else
            {
                btnPlay.Image = global::VidSoom.Iconos.playup;
            }
        }
    }
}
