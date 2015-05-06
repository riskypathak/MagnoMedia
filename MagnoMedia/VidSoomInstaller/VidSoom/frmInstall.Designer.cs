namespace VidSoom
{
    partial class frmInstall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInstall));
            this.cmbIdioma = new System.Windows.Forms.ComboBox();
            this.lblIdioma = new System.Windows.Forms.Label();
            this.oTipica = new System.Windows.Forms.RadioButton();
            this.oPersonalizada = new System.Windows.Forms.RadioButton();
            this.cInicio = new System.Windows.Forms.CheckBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblTipica = new System.Windows.Forms.Label();
            this.gTipo = new System.Windows.Forms.GroupBox();
            this.cAcceso = new System.Windows.Forms.CheckBox();
            this.lblAcuerdo = new System.Windows.Forms.LinkLabel();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.gTipo.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbIdioma
            // 
            this.cmbIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdioma.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIdioma.FormattingEnabled = true;
            this.cmbIdioma.Items.AddRange(new object[] {
            "Español (Spanish)",
            "English (English)"});
            this.cmbIdioma.Location = new System.Drawing.Point(249, 91);
            this.cmbIdioma.Name = "cmbIdioma";
            this.cmbIdioma.Size = new System.Drawing.Size(145, 21);
            this.cmbIdioma.TabIndex = 0;
            this.cmbIdioma.SelectedIndexChanged += new System.EventHandler(this.cmbIdioma_SelectedIndexChanged);
            // 
            // lblIdioma
            // 
            this.lblIdioma.BackColor = System.Drawing.Color.Transparent;
            this.lblIdioma.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdioma.ForeColor = System.Drawing.Color.White;
            this.lblIdioma.Location = new System.Drawing.Point(246, 60);
            this.lblIdioma.Name = "lblIdioma";
            this.lblIdioma.Size = new System.Drawing.Size(148, 28);
            this.lblIdioma.TabIndex = 1;
            this.lblIdioma.Tag = "lang";
            this.lblIdioma.Text = "Selecciona el idioma del programa de instalación:";
            // 
            // oTipica
            // 
            this.oTipica.AutoSize = true;
            this.oTipica.Checked = true;
            this.oTipica.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oTipica.ForeColor = System.Drawing.Color.Black;
            this.oTipica.Location = new System.Drawing.Point(6, 20);
            this.oTipica.Name = "oTipica";
            this.oTipica.Size = new System.Drawing.Size(213, 17);
            this.oTipica.TabIndex = 2;
            this.oTipica.TabStop = true;
            this.oTipica.Tag = "lang";
            this.oTipica.Text = "Instalación típica (recomendada)";
            this.oTipica.UseVisualStyleBackColor = true;
            // 
            // oPersonalizada
            // 
            this.oPersonalizada.AutoSize = true;
            this.oPersonalizada.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oPersonalizada.ForeColor = System.Drawing.Color.Black;
            this.oPersonalizada.Location = new System.Drawing.Point(6, 101);
            this.oPersonalizada.Name = "oPersonalizada";
            this.oPersonalizada.Size = new System.Drawing.Size(170, 17);
            this.oPersonalizada.TabIndex = 3;
            this.oPersonalizada.Tag = "lang";
            this.oPersonalizada.Text = "Instalación personalizada";
            this.oPersonalizada.UseVisualStyleBackColor = true;
            this.oPersonalizada.CheckedChanged += new System.EventHandler(this.oPersonalizada_CheckedChanged);
            // 
            // cInicio
            // 
            this.cInicio.Checked = true;
            this.cInicio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cInicio.Enabled = false;
            this.cInicio.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cInicio.ForeColor = System.Drawing.Color.Black;
            this.cInicio.Location = new System.Drawing.Point(23, 124);
            this.cInicio.Name = "cInicio";
            this.cInicio.Size = new System.Drawing.Size(348, 31);
            this.cInicio.TabIndex = 4;
            this.cInicio.Tag = "lang";
            this.cInicio.Text = "Define y mantiene Vidsoom.com como mi página de inicio y como motor de búsqueda p" +
    "redeterminado.";
            this.cInicio.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(12, 378);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(83, 26);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Tag = "lang";
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(258, 378);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(136, 26);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Tag = "lang";
            this.btnAceptar.Text = "&Aceptar e Instalar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblTipica
            // 
            this.lblTipica.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipica.ForeColor = System.Drawing.Color.Black;
            this.lblTipica.Location = new System.Drawing.Point(20, 43);
            this.lblTipica.Name = "lblTipica";
            this.lblTipica.Size = new System.Drawing.Size(351, 51);
            this.lblTipica.TabIndex = 10;
            this.lblTipica.Tag = "lang";
            this.lblTipica.Text = "• Define y mantiene Vidsoom.com como mi página de inicio y como motor de búsqueda" +
    " predeterminado.\r\n• Crea un acceso directo en el escritorio.";
            // 
            // gTipo
            // 
            this.gTipo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gTipo.Controls.Add(this.cAcceso);
            this.gTipo.Controls.Add(this.lblTipica);
            this.gTipo.Controls.Add(this.oTipica);
            this.gTipo.Controls.Add(this.oPersonalizada);
            this.gTipo.Controls.Add(this.cInicio);
            this.gTipo.ForeColor = System.Drawing.Color.Teal;
            this.gTipo.Location = new System.Drawing.Point(12, 128);
            this.gTipo.Name = "gTipo";
            this.gTipo.Size = new System.Drawing.Size(382, 209);
            this.gTipo.TabIndex = 11;
            this.gTipo.TabStop = false;
            this.gTipo.Tag = "lang";
            this.gTipo.Text = "Tipo de instalación";
            // 
            // cAcceso
            // 
            this.cAcceso.Checked = true;
            this.cAcceso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cAcceso.Enabled = false;
            this.cAcceso.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAcceso.ForeColor = System.Drawing.Color.Black;
            this.cAcceso.Location = new System.Drawing.Point(23, 161);
            this.cAcceso.Name = "cAcceso";
            this.cAcceso.Size = new System.Drawing.Size(348, 31);
            this.cAcceso.TabIndex = 11;
            this.cAcceso.Tag = "lang";
            this.cAcceso.Text = "Crear acceso directo en el escritorio.";
            this.cAcceso.UseVisualStyleBackColor = true;
            // 
            // lblAcuerdo
            // 
            this.lblAcuerdo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAcuerdo.AutoSize = true;
            this.lblAcuerdo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcuerdo.Location = new System.Drawing.Point(10, 357);
            this.lblAcuerdo.Name = "lblAcuerdo";
            this.lblAcuerdo.Size = new System.Drawing.Size(99, 13);
            this.lblAcuerdo.TabIndex = 13;
            this.lblAcuerdo.TabStop = true;
            this.lblAcuerdo.Tag = "lang";
            this.lblAcuerdo.Text = "Acuerdo de licencia";
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLeyenda.AutoSize = true;
            this.lblLeyenda.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeyenda.ForeColor = System.Drawing.Color.Black;
            this.lblLeyenda.Location = new System.Drawing.Point(10, 340);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(226, 13);
            this.lblLeyenda.TabIndex = 12;
            this.lblLeyenda.Tag = "lang";
            this.lblLeyenda.Text = "Al hacer clic en \"Aceptar e instalar\" aceptas el";
            // 
            // frmInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::VidSoom.Iconos.vidsoomlogo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(406, 412);
            this.Controls.Add(this.lblAcuerdo);
            this.Controls.Add(this.lblLeyenda);
            this.Controls.Add(this.gTipo);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblIdioma);
            this.Controls.Add(this.cmbIdioma);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmInstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "lang";
            this.Text = "Instalación de VidSoom";
            this.Load += new System.EventHandler(this.frmInstall_Load);
            this.gTipo.ResumeLayout(false);
            this.gTipo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbIdioma;
        private System.Windows.Forms.Label lblIdioma;
        private System.Windows.Forms.RadioButton oTipica;
        private System.Windows.Forms.RadioButton oPersonalizada;
        private System.Windows.Forms.CheckBox cInicio;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblTipica;
        private System.Windows.Forms.GroupBox gTipo;
        private System.Windows.Forms.CheckBox cAcceso;
        private System.Windows.Forms.LinkLabel lblAcuerdo;
        private System.Windows.Forms.Label lblLeyenda;
    }
}