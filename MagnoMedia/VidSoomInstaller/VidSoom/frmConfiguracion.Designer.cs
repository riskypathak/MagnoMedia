namespace VidSoom
{
    partial class frmConfiguracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tGeneral = new System.Windows.Forms.TabPage();
            this.btnRuta = new System.Windows.Forms.Button();
            this.cUpdates = new System.Windows.Forms.CheckBox();
            this.tRuta = new System.Windows.Forms.TextBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.lblIdioma = new System.Windows.Forms.Label();
            this.cmbIdioma = new System.Windows.Forms.ComboBox();
            this.tDescargas = new System.Windows.Forms.TabPage();
            this.cVelocidad = new System.Windows.Forms.CheckBox();
            this.cantVelocidad = new System.Windows.Forms.NumericUpDown();
            this.lblDescargas = new System.Windows.Forms.Label();
            this.cantDescargas = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.FBD = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tGeneral.SuspendLayout();
            this.tDescargas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cantVelocidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cantDescargas)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tGeneral);
            this.tabControl1.Controls.Add(this.tDescargas);
            this.tabControl1.Location = new System.Drawing.Point(12, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(322, 197);
            this.tabControl1.TabIndex = 0;
            // 
            // tGeneral
            // 
            this.tGeneral.Controls.Add(this.btnRuta);
            this.tGeneral.Controls.Add(this.cUpdates);
            this.tGeneral.Controls.Add(this.tRuta);
            this.tGeneral.Controls.Add(this.lblRuta);
            this.tGeneral.Controls.Add(this.lblIdioma);
            this.tGeneral.Controls.Add(this.cmbIdioma);
            this.tGeneral.Location = new System.Drawing.Point(4, 22);
            this.tGeneral.Name = "tGeneral";
            this.tGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tGeneral.Size = new System.Drawing.Size(314, 171);
            this.tGeneral.TabIndex = 0;
            this.tGeneral.Text = "General";
            this.tGeneral.UseVisualStyleBackColor = true;
            // 
            // btnRuta
            // 
            this.btnRuta.Location = new System.Drawing.Point(276, 70);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(32, 20);
            this.btnRuta.TabIndex = 6;
            this.btnRuta.Text = "...";
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // cUpdates
            // 
            this.cUpdates.AutoSize = true;
            this.cUpdates.Location = new System.Drawing.Point(6, 105);
            this.cUpdates.Name = "cUpdates";
            this.cUpdates.Size = new System.Drawing.Size(224, 17);
            this.cUpdates.TabIndex = 5;
            this.cUpdates.Text = "Verificar actualizaciones automáticamente";
            this.cUpdates.UseVisualStyleBackColor = true;
            // 
            // tRuta
            // 
            this.tRuta.Location = new System.Drawing.Point(6, 70);
            this.tRuta.Name = "tRuta";
            this.tRuta.ReadOnly = true;
            this.tRuta.Size = new System.Drawing.Size(264, 20);
            this.tRuta.TabIndex = 4;
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Location = new System.Drawing.Point(6, 54);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(100, 13);
            this.lblRuta.TabIndex = 3;
            this.lblRuta.Text = "Ruta de descargas:";
            // 
            // lblIdioma
            // 
            this.lblIdioma.AutoSize = true;
            this.lblIdioma.Location = new System.Drawing.Point(6, 11);
            this.lblIdioma.Name = "lblIdioma";
            this.lblIdioma.Size = new System.Drawing.Size(41, 13);
            this.lblIdioma.TabIndex = 2;
            this.lblIdioma.Text = "Idioma:";
            // 
            // cmbIdioma
            // 
            this.cmbIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdioma.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIdioma.FormattingEnabled = true;
            this.cmbIdioma.Items.AddRange(new object[] {
            "Español (Spanish)",
            "English (English)"});
            this.cmbIdioma.Location = new System.Drawing.Point(6, 27);
            this.cmbIdioma.Name = "cmbIdioma";
            this.cmbIdioma.Size = new System.Drawing.Size(185, 21);
            this.cmbIdioma.TabIndex = 1;
            // 
            // tDescargas
            // 
            this.tDescargas.Controls.Add(this.cVelocidad);
            this.tDescargas.Controls.Add(this.cantVelocidad);
            this.tDescargas.Controls.Add(this.lblDescargas);
            this.tDescargas.Controls.Add(this.cantDescargas);
            this.tDescargas.Location = new System.Drawing.Point(4, 22);
            this.tDescargas.Name = "tDescargas";
            this.tDescargas.Padding = new System.Windows.Forms.Padding(3);
            this.tDescargas.Size = new System.Drawing.Size(314, 171);
            this.tDescargas.TabIndex = 1;
            this.tDescargas.Text = "Descargas";
            this.tDescargas.UseVisualStyleBackColor = true;
            // 
            // cVelocidad
            // 
            this.cVelocidad.AutoSize = true;
            this.cVelocidad.Location = new System.Drawing.Point(9, 61);
            this.cVelocidad.Name = "cVelocidad";
            this.cVelocidad.Size = new System.Drawing.Size(145, 17);
            this.cVelocidad.TabIndex = 4;
            this.cVelocidad.Text = "Velocidad máxima (kb/s):";
            this.cVelocidad.UseVisualStyleBackColor = true;
            // 
            // cantVelocidad
            // 
            this.cantVelocidad.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cantVelocidad.Location = new System.Drawing.Point(9, 84);
            this.cantVelocidad.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.cantVelocidad.Name = "cantVelocidad";
            this.cantVelocidad.Size = new System.Drawing.Size(62, 20);
            this.cantVelocidad.TabIndex = 3;
            // 
            // lblDescargas
            // 
            this.lblDescargas.AutoSize = true;
            this.lblDescargas.Location = new System.Drawing.Point(6, 13);
            this.lblDescargas.Name = "lblDescargas";
            this.lblDescargas.Size = new System.Drawing.Size(171, 13);
            this.lblDescargas.TabIndex = 1;
            this.lblDescargas.Text = "Máximo de descargas simultaneas:";
            // 
            // cantDescargas
            // 
            this.cantDescargas.Location = new System.Drawing.Point(9, 29);
            this.cantDescargas.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.cantDescargas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cantDescargas.Name = "cantDescargas";
            this.cantDescargas.Size = new System.Drawing.Size(48, 20);
            this.cantDescargas.TabIndex = 0;
            this.cantDescargas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(178, 212);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Aceptar";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(259, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 247);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfiguracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.frmConfiguracion_Load);
            this.tabControl1.ResumeLayout(false);
            this.tGeneral.ResumeLayout(false);
            this.tGeneral.PerformLayout();
            this.tDescargas.ResumeLayout(false);
            this.tDescargas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cantVelocidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cantDescargas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tGeneral;
        private System.Windows.Forms.TabPage tDescargas;
        private System.Windows.Forms.CheckBox cUpdates;
        private System.Windows.Forms.TextBox tRuta;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.Label lblIdioma;
        private System.Windows.Forms.ComboBox cmbIdioma;
        private System.Windows.Forms.CheckBox cVelocidad;
        private System.Windows.Forms.NumericUpDown cantVelocidad;
        private System.Windows.Forms.Label lblDescargas;
        private System.Windows.Forms.NumericUpDown cantDescargas;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog FBD;
    }
}