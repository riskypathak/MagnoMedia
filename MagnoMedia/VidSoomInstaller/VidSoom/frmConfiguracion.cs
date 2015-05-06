using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VidSoom
{
    public partial class frmConfiguracion : Form
    {
        public frmConfiguracion()
        {
            InitializeComponent();
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                tRuta.Text = FBD.SelectedPath;
                if (tRuta.Text.Substring(tRuta.Text.Length - 1, 1) != "\\") tRuta.Text += "\\";
            }
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            Language.translateForm(this);
            tRuta.Text = Properties.Settings.Default.Ruta;
            cUpdates.Checked = Properties.Settings.Default.Updates;
            cmbIdioma.SelectedIndex = Properties.Settings.Default.Idioma;
            cantDescargas.Value = Properties.Settings.Default.MaxDescargas;
            cantVelocidad.Value = Properties.Settings.Default.MaxVelocidad;
            cVelocidad.Checked = cantVelocidad.Value > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Ruta = tRuta.Text;
            Properties.Settings.Default.Updates = cUpdates.Checked;
            Properties.Settings.Default.Idioma = cmbIdioma.SelectedIndex;
            Properties.Settings.Default.MaxDescargas = (int)cantDescargas.Value;
            Properties.Settings.Default.MaxVelocidad = cVelocidad.Checked ? (int)cantVelocidad.Value : 0;
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Dispose();

        }
    }
}
