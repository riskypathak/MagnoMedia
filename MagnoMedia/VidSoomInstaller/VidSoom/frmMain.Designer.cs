namespace VidSoom
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrev = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.PictureBox();
            this.lblMediPos = new System.Windows.Forms.Label();
            this.lblMediaTop = new System.Windows.Forms.Label();
            this.pBarra = new System.Windows.Forms.PictureBox();
            this.WMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.grdDatos = new System.Windows.Forms.DataGridView();
            this.Titulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Velocidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Progreso = new VidSoom.DataGridViewProgressColumn();
            this.Descargado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mMenuGrilla = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmPegar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmPausa = new System.Windows.Forms.ToolStripMenuItem();
            this.cmEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPrevisualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.tURL = new System.Windows.Forms.TextBox();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPegar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPausa = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnPrevisualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargas = new System.Windows.Forms.ToolStripButton();
            this.btnConfiguracion = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.sTiempo = new System.Windows.Forms.ToolStripStatusLabel();
            this.sDescargando = new System.Windows.Forms.ToolStripStatusLabel();
            this.sVelocidad = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mmArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mmPegar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mmAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.mmSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.mmEdicion = new System.Windows.Forms.ToolStripMenuItem();
            this.mmPausar = new System.Windows.Forms.ToolStripMenuItem();
            this.mmEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.mmPrevisualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mmConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.mmAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.mmAcerca = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewProgressColumn1 = new VidSoom.DataGridViewProgressColumn();
            this.tRefresh = new System.Windows.Forms.Timer(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBarra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDatos)).BeginInit();
            this.mMenuGrilla.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.WMP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdDatos);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1069, 497);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Controls.Add(this.btnPlay);
            this.panel2.Controls.Add(this.lblMediPos);
            this.panel2.Controls.Add(this.lblMediaTop);
            this.panel2.Controls.Add(this.pBarra);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 429);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(277, 68);
            this.panel2.TabIndex = 5;
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrev.Image = global::VidSoom.Iconos.revup;
            this.btnPrev.Location = new System.Drawing.Point(86, 32);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(23, 24);
            this.btnPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPrev.TabIndex = 9;
            this.btnPrev.TabStop = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            this.btnPrev.MouseEnter += new System.EventHandler(this.btnPrev_MouseEnter);
            this.btnPrev.MouseLeave += new System.EventHandler(this.btnPrev_MouseLeave);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStop.Image = global::VidSoom.Iconos.stopup;
            this.btnStop.Location = new System.Drawing.Point(167, 32);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 24);
            this.btnStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnStop.TabIndex = 8;
            this.btnStop.TabStop = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.MouseEnter += new System.EventHandler(this.btnStop_MouseEnter);
            this.btnStop.MouseLeave += new System.EventHandler(this.btnStop_MouseLeave);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPlay.Image = global::VidSoom.Iconos.playup;
            this.btnPlay.Location = new System.Drawing.Point(115, 19);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(46, 47);
            this.btnPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPlay.TabIndex = 3;
            this.btnPlay.TabStop = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.MouseEnter += new System.EventHandler(this.btnPlay_MouseEnter);
            this.btnPlay.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            // 
            // lblMediPos
            // 
            this.lblMediPos.AutoSize = true;
            this.lblMediPos.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMediPos.ForeColor = System.Drawing.Color.White;
            this.lblMediPos.Location = new System.Drawing.Point(4, 3);
            this.lblMediPos.Name = "lblMediPos";
            this.lblMediPos.Size = new System.Drawing.Size(36, 11);
            this.lblMediPos.TabIndex = 7;
            this.lblMediPos.Text = "00:00";
            // 
            // lblMediaTop
            // 
            this.lblMediaTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMediaTop.AutoSize = true;
            this.lblMediaTop.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMediaTop.ForeColor = System.Drawing.Color.White;
            this.lblMediaTop.Location = new System.Drawing.Point(240, 3);
            this.lblMediaTop.Name = "lblMediaTop";
            this.lblMediaTop.Size = new System.Drawing.Size(36, 11);
            this.lblMediaTop.TabIndex = 6;
            this.lblMediaTop.Text = "00:00";
            // 
            // pBarra
            // 
            this.pBarra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pBarra.BackColor = System.Drawing.Color.White;
            this.pBarra.Location = new System.Drawing.Point(40, 4);
            this.pBarra.Name = "pBarra";
            this.pBarra.Size = new System.Drawing.Size(199, 9);
            this.pBarra.TabIndex = 5;
            this.pBarra.TabStop = false;
            this.pBarra.Click += new System.EventHandler(this.pBarra_Click);
            this.pBarra.Paint += new System.Windows.Forms.PaintEventHandler(this.pBarra_Paint);
            this.pBarra.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pBarra_MouseUp);
            // 
            // WMP
            // 
            this.WMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WMP.Enabled = true;
            this.WMP.Location = new System.Drawing.Point(0, 0);
            this.WMP.Name = "WMP";
            this.WMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WMP.OcxState")));
            this.WMP.Size = new System.Drawing.Size(277, 497);
            this.WMP.TabIndex = 5;
            this.WMP.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.WMP_PlayStateChange);
            // 
            // grdDatos
            // 
            this.grdDatos.AllowUserToAddRows = false;
            this.grdDatos.AllowUserToDeleteRows = false;
            this.grdDatos.AllowUserToOrderColumns = true;
            this.grdDatos.AllowUserToResizeRows = false;
            this.grdDatos.BackgroundColor = System.Drawing.Color.White;
            this.grdDatos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdDatos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.grdDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Titulo,
            this.Estado,
            this.Velocidad,
            this.Progreso,
            this.Descargado,
            this.Peso,
            this.Origen});
            this.grdDatos.ContextMenuStrip = this.mMenuGrilla;
            this.grdDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDatos.Location = new System.Drawing.Point(0, 27);
            this.grdDatos.Name = "grdDatos";
            this.grdDatos.ReadOnly = true;
            this.grdDatos.RowHeadersWidth = 25;
            this.grdDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDatos.Size = new System.Drawing.Size(788, 470);
            this.grdDatos.TabIndex = 2;
            this.grdDatos.SelectionChanged += new System.EventHandler(this.grdDatos_SelectionChanged);
            this.grdDatos.DoubleClick += new System.EventHandler(this.grdDatos_DoubleClick);
            // 
            // Titulo
            // 
            this.Titulo.HeaderText = "Titulo";
            this.Titulo.Name = "Titulo";
            this.Titulo.ReadOnly = true;
            this.Titulo.Width = 250;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            // 
            // Velocidad
            // 
            this.Velocidad.HeaderText = "Velocidad";
            this.Velocidad.Name = "Velocidad";
            this.Velocidad.ReadOnly = true;
            // 
            // Progreso
            // 
            this.Progreso.HeaderText = "Progreso";
            this.Progreso.Name = "Progreso";
            this.Progreso.ReadOnly = true;
            // 
            // Descargado
            // 
            this.Descargado.HeaderText = "Descargado";
            this.Descargado.Name = "Descargado";
            this.Descargado.ReadOnly = true;
            // 
            // Peso
            // 
            this.Peso.HeaderText = "Tamaño";
            this.Peso.Name = "Peso";
            this.Peso.ReadOnly = true;
            // 
            // Origen
            // 
            this.Origen.HeaderText = "Origen";
            this.Origen.Name = "Origen";
            this.Origen.ReadOnly = true;
            this.Origen.Width = 250;
            // 
            // mMenuGrilla
            // 
            this.mMenuGrilla.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmPegar,
            this.toolStripMenuItem1,
            this.cmPausa,
            this.cmEliminar,
            this.cmPrevisualizar,
            this.cmAbrir});
            this.mMenuGrilla.Name = "mMenuGrilla";
            this.mMenuGrilla.Size = new System.Drawing.Size(171, 120);
            // 
            // cmPegar
            // 
            this.cmPegar.Image = global::VidSoom.Iconos.folder_add;
            this.cmPegar.Name = "cmPegar";
            this.cmPegar.Size = new System.Drawing.Size(170, 22);
            this.cmPegar.Text = "Pegar enlaces";
            this.cmPegar.Click += new System.EventHandler(this.cmPegar_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // cmPausa
            // 
            this.cmPausa.Image = global::VidSoom.Iconos.control_pause;
            this.cmPausa.Name = "cmPausa";
            this.cmPausa.Size = new System.Drawing.Size(170, 22);
            this.cmPausa.Text = "Pausa";
            this.cmPausa.Click += new System.EventHandler(this.cmPausa_Click);
            // 
            // cmEliminar
            // 
            this.cmEliminar.Image = global::VidSoom.Iconos.delete;
            this.cmEliminar.Name = "cmEliminar";
            this.cmEliminar.Size = new System.Drawing.Size(170, 22);
            this.cmEliminar.Text = "Eliminar";
            this.cmEliminar.Click += new System.EventHandler(this.cmEliminar_Click);
            // 
            // cmPrevisualizar
            // 
            this.cmPrevisualizar.Image = global::VidSoom.Iconos.television;
            this.cmPrevisualizar.Name = "cmPrevisualizar";
            this.cmPrevisualizar.Size = new System.Drawing.Size(170, 22);
            this.cmPrevisualizar.Text = "Previsualizar";
            this.cmPrevisualizar.Click += new System.EventHandler(this.cmPrevisualizar_Click);
            // 
            // cmAbrir
            // 
            this.cmAbrir.Image = global::VidSoom.Iconos.folder_go;
            this.cmAbrir.Name = "cmAbrir";
            this.cmAbrir.Size = new System.Drawing.Size(170, 22);
            this.cmAbrir.Text = "Abrir en directorio";
            this.cmAbrir.Click += new System.EventHandler(this.cmAbrir_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDescargar);
            this.panel1.Controls.Add(this.tURL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 27);
            this.panel1.TabIndex = 0;
            // 
            // btnDescargar
            // 
            this.btnDescargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDescargar.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargar.Image")));
            this.btnDescargar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDescargar.Location = new System.Drawing.Point(703, 2);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(82, 23);
            this.btnDescargar.TabIndex = 1;
            this.btnDescargar.Tag = "lang";
            this.btnDescargar.Text = "Descargar";
            this.btnDescargar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDescargar.UseVisualStyleBackColor = true;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // tURL
            // 
            this.tURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tURL.Location = new System.Drawing.Point(3, 4);
            this.tURL.Name = "tURL";
            this.tURL.Size = new System.Drawing.Size(697, 20);
            this.tURL.TabIndex = 0;
            this.tURL.Tag = "lang";
            this.tURL.Text = "Pegue aquí los enlaces a descargar...";
            this.tURL.Enter += new System.EventHandler(this.tURL_Enter);
            this.tURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tURL_KeyPress);
            this.tURL.Leave += new System.EventHandler(this.tURL_Leave);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "lightning.png");
            this.imgList.Images.SetKeyName(1, "music.png");
            this.imgList.Images.SetKeyName(2, "folder.png");
            this.imgList.Images.SetKeyName(3, "film.png");
            this.imgList.Images.SetKeyName(4, "page_word.png");
            this.imgList.Images.SetKeyName(5, "plugin.png");
            this.imgList.Images.SetKeyName(6, "page_white_zip.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPegar,
            this.toolStripSeparator3,
            this.btnPausa,
            this.btnEliminar,
            this.btnPrevisualizar,
            this.toolStripSeparator4,
            this.btnDescargas,
            this.btnConfiguracion});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1069, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnPegar
            // 
            this.btnPegar.Image = global::VidSoom.Iconos.folder_add;
            this.btnPegar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPegar.Name = "btnPegar";
            this.btnPegar.Size = new System.Drawing.Size(83, 35);
            this.btnPegar.Tag = "";
            this.btnPegar.Text = "Pegar enlaces";
            this.btnPegar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPegar.Click += new System.EventHandler(this.btnPegar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // btnPausa
            // 
            this.btnPausa.Image = global::VidSoom.Iconos.control_pause;
            this.btnPausa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPausa.Name = "btnPausa";
            this.btnPausa.Size = new System.Drawing.Size(42, 35);
            this.btnPausa.Tag = "0";
            this.btnPausa.Text = "Pausa";
            this.btnPausa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPausa.Click += new System.EventHandler(this.btnPausa_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = global::VidSoom.Iconos.delete;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(54, 35);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnPrevisualizar
            // 
            this.btnPrevisualizar.Image = global::VidSoom.Iconos.television;
            this.btnPrevisualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevisualizar.Name = "btnPrevisualizar";
            this.btnPrevisualizar.Size = new System.Drawing.Size(76, 35);
            this.btnPrevisualizar.Text = "Previsualizar";
            this.btnPrevisualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrevisualizar.Click += new System.EventHandler(this.btnPrevisualizar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 38);
            // 
            // btnDescargas
            // 
            this.btnDescargas.Image = global::VidSoom.Iconos.folder_go;
            this.btnDescargas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargas.Name = "btnDescargas";
            this.btnDescargas.Size = new System.Drawing.Size(86, 35);
            this.btnDescargas.Text = "Mis Descargas";
            this.btnDescargas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDescargas.Click += new System.EventHandler(this.btnDescargas_Click);
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.Image = global::VidSoom.Iconos.wrench;
            this.btnConfiguracion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Size = new System.Drawing.Size(87, 35);
            this.btnConfiguracion.Text = "Configuración";
            this.btnConfiguracion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConfiguracion.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sHora,
            this.sTiempo,
            this.sDescargando,
            this.sVelocidad});
            this.statusStrip1.Location = new System.Drawing.Point(0, 559);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1069, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sHora
            // 
            this.sHora.Name = "sHora";
            this.sHora.Size = new System.Drawing.Size(122, 17);
            this.sHora.Text = "03/11/2012 04:15 p.m.";
            // 
            // sTiempo
            // 
            this.sTiempo.Name = "sTiempo";
            this.sTiempo.Size = new System.Drawing.Size(310, 17);
            this.sTiempo.Spring = true;
            this.sTiempo.Text = "Tiempo de sesión: 00:00:00 / Descargado: 0 kb";
            // 
            // sDescargando
            // 
            this.sDescargando.Name = "sDescargando";
            this.sDescargando.Size = new System.Drawing.Size(310, 17);
            this.sDescargando.Spring = true;
            this.sDescargando.Text = "Descargando: 0/0";
            // 
            // sVelocidad
            // 
            this.sVelocidad.Name = "sVelocidad";
            this.sVelocidad.Size = new System.Drawing.Size(310, 17);
            this.sVelocidad.Spring = true;
            this.sVelocidad.Text = "Velocidad total: 0 kb/s";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(87, 35);
            this.toolStripButton5.Text = "Configuración";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "toolStripButton7";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "toolStripButton8";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "toolStripButton9";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "toolStripButton10";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmArchivo,
            this.mmEdicion,
            this.mmAyuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1069, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mmArchivo
            // 
            this.mmArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmPegar,
            this.toolStripMenuItem2,
            this.mmAbrir,
            this.mmSalir});
            this.mmArchivo.Name = "mmArchivo";
            this.mmArchivo.Size = new System.Drawing.Size(60, 20);
            this.mmArchivo.Text = "&Archivo";
            // 
            // mmPegar
            // 
            this.mmPegar.Image = global::VidSoom.Iconos.folder_add;
            this.mmPegar.Name = "mmPegar";
            this.mmPegar.Size = new System.Drawing.Size(213, 22);
            this.mmPegar.Text = "Pegar enlaces";
            this.mmPegar.Click += new System.EventHandler(this.pegarEnlacesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(210, 6);
            // 
            // mmAbrir
            // 
            this.mmAbrir.Image = global::VidSoom.Iconos.folder_go;
            this.mmAbrir.Name = "mmAbrir";
            this.mmAbrir.Size = new System.Drawing.Size(213, 22);
            this.mmAbrir.Text = "Abrir carpeta de descargas";
            this.mmAbrir.Click += new System.EventHandler(this.abrirCarpetaDeDescargasToolStripMenuItem_Click);
            // 
            // mmSalir
            // 
            this.mmSalir.Name = "mmSalir";
            this.mmSalir.Size = new System.Drawing.Size(213, 22);
            this.mmSalir.Text = "Salir";
            this.mmSalir.Click += new System.EventHandler(this.mmSalir_Click);
            // 
            // mmEdicion
            // 
            this.mmEdicion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmPausar,
            this.mmEliminar,
            this.mmPrevisualizar,
            this.toolStripMenuItem3,
            this.mmConfiguracion});
            this.mmEdicion.Name = "mmEdicion";
            this.mmEdicion.Size = new System.Drawing.Size(58, 20);
            this.mmEdicion.Text = "&Edición";
            // 
            // mmPausar
            // 
            this.mmPausar.Image = global::VidSoom.Iconos.control_pause;
            this.mmPausar.Name = "mmPausar";
            this.mmPausar.Size = new System.Drawing.Size(164, 22);
            this.mmPausar.Text = "Pausar/Reanudar";
            this.mmPausar.Click += new System.EventHandler(this.mmPausar_Click);
            // 
            // mmEliminar
            // 
            this.mmEliminar.Image = global::VidSoom.Iconos.delete;
            this.mmEliminar.Name = "mmEliminar";
            this.mmEliminar.Size = new System.Drawing.Size(164, 22);
            this.mmEliminar.Text = "Eliminar";
            this.mmEliminar.Click += new System.EventHandler(this.mmEliminar_Click);
            // 
            // mmPrevisualizar
            // 
            this.mmPrevisualizar.Image = global::VidSoom.Iconos.television;
            this.mmPrevisualizar.Name = "mmPrevisualizar";
            this.mmPrevisualizar.Size = new System.Drawing.Size(164, 22);
            this.mmPrevisualizar.Text = "Previsualizar";
            this.mmPrevisualizar.Click += new System.EventHandler(this.mmPrevisualizar_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(161, 6);
            // 
            // mmConfiguracion
            // 
            this.mmConfiguracion.Image = global::VidSoom.Iconos.wrench;
            this.mmConfiguracion.Name = "mmConfiguracion";
            this.mmConfiguracion.Size = new System.Drawing.Size(164, 22);
            this.mmConfiguracion.Text = "Configuración";
            this.mmConfiguracion.Click += new System.EventHandler(this.mmConfiguracion_Click);
            // 
            // mmAyuda
            // 
            this.mmAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmAcerca});
            this.mmAyuda.Name = "mmAyuda";
            this.mmAyuda.Size = new System.Drawing.Size(53, 20);
            this.mmAyuda.Text = "Ayuda";
            // 
            // mmAcerca
            // 
            this.mmAcerca.Name = "mmAcerca";
            this.mmAcerca.Size = new System.Drawing.Size(177, 22);
            this.mmAcerca.Text = "Acerca de VidSoom";
            // 
            // dataGridViewProgressColumn1
            // 
            this.dataGridViewProgressColumn1.HeaderText = "Progreso";
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            // 
            // tRefresh
            // 
            this.tRefresh.Enabled = true;
            this.tRefresh.Interval = 300;
            this.tRefresh.Tick += new System.EventHandler(this.tRefresh_Tick);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(83, 35);
            this.toolStripButton1.Text = "Pegar enlaces";
            this.toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(42, 35);
            this.toolStripButton2.Text = "Pausa";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(54, 35);
            this.toolStripButton3.Text = "Eliminar";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(85, 35);
            this.toolStripButton4.Text = "Mis descargas";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 581);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VidSoom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBarra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDatos)).EndInit();
            this.mMenuGrilla.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.TextBox tURL;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton btnPegar;
        private System.Windows.Forms.ToolStripButton btnPausa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripButton btnDescargas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnConfiguracion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mmArchivo;
        private System.Windows.Forms.ToolStripMenuItem mmEdicion;
        private System.Windows.Forms.ToolStripMenuItem mmAyuda;
        private System.Windows.Forms.DataGridView grdDatos;
        private DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Titulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Velocidad;
        private DataGridViewProgressColumn Progreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descargado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Peso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Origen;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Timer tRefresh;
        private System.Windows.Forms.ContextMenuStrip mMenuGrilla;
        private System.Windows.Forms.ToolStripMenuItem cmPegar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmPausa;
        private System.Windows.Forms.ToolStripMenuItem cmEliminar;
        private System.Windows.Forms.ToolStripMenuItem cmAbrir;
        private System.Windows.Forms.ToolStripStatusLabel sHora;
        private System.Windows.Forms.ToolStripStatusLabel sTiempo;
        private System.Windows.Forms.ToolStripStatusLabel sDescargando;
        private System.Windows.Forms.ToolStripStatusLabel sVelocidad;
        private System.Windows.Forms.ToolStripMenuItem mmPegar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mmAbrir;
        private System.Windows.Forms.ToolStripMenuItem mmPausar;
        private System.Windows.Forms.ToolStripMenuItem mmEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mmConfiguracion;
        private System.Windows.Forms.ToolStripMenuItem mmAcerca;
        private System.Windows.Forms.ToolStripMenuItem mmSalir;
        private AxWMPLib.AxWindowsMediaPlayer WMP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pBarra;
        private System.Windows.Forms.ToolStripButton btnPrevisualizar;
        private System.Windows.Forms.ToolStripMenuItem mmPrevisualizar;
        private System.Windows.Forms.Label lblMediPos;
        private System.Windows.Forms.Label lblMediaTop;
        private System.Windows.Forms.ToolStripMenuItem cmPrevisualizar;
        private System.Windows.Forms.PictureBox btnPrev;
        private System.Windows.Forms.PictureBox btnStop;
        private System.Windows.Forms.PictureBox btnPlay;
    }
}

