namespace MagnoMedia.Windows
{
    partial class Form1
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
            this.buttonInstall = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.flowLayoutPanelSoftwareList = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panelInstallCancel = new System.Windows.Forms.Panel();
            this.progressBarInstall = new System.Windows.Forms.ProgressBar();
            this.labelAgreementTxt = new System.Windows.Forms.Label();
            this.checkedListBoxSW = new System.Windows.Forms.CheckedListBox();
            this.buttonCustomize = new System.Windows.Forms.Button();
            this.flowLayoutPanelSoftwareList.SuspendLayout();
            this.panelInstallCancel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInstall
            // 
            this.buttonInstall.Location = new System.Drawing.Point(0, 27);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(75, 23);
            this.buttonInstall.TabIndex = 0;
            this.buttonInstall.Text = "Install";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(125, 27);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // flowLayoutPanelSoftwareList
            // 
            this.flowLayoutPanelSoftwareList.Controls.Add(this.linkLabel1);
            this.flowLayoutPanelSoftwareList.Location = new System.Drawing.Point(32, 344);
            this.flowLayoutPanelSoftwareList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.flowLayoutPanelSoftwareList.Name = "flowLayoutPanelSoftwareList";
            this.flowLayoutPanelSoftwareList.Size = new System.Drawing.Size(262, 134);
            this.flowLayoutPanelSoftwareList.TabIndex = 2;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(3, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(100, 23);
            this.linkLabel1.TabIndex = 0;
            // 
            // panelInstallCancel
            // 
            this.panelInstallCancel.Controls.Add(this.buttonInstall);
            this.panelInstallCancel.Controls.Add(this.buttonCancel);
            this.panelInstallCancel.Location = new System.Drawing.Point(383, 378);
            this.panelInstallCancel.Name = "panelInstallCancel";
            this.panelInstallCancel.Size = new System.Drawing.Size(200, 100);
            this.panelInstallCancel.TabIndex = 3;
            // 
            // progressBarInstall
            // 
            this.progressBarInstall.Location = new System.Drawing.Point(383, 281);
            this.progressBarInstall.Name = "progressBarInstall";
            this.progressBarInstall.Size = new System.Drawing.Size(200, 23);
            this.progressBarInstall.TabIndex = 4;
            // 
            // labelAgreementTxt
            // 
            this.labelAgreementTxt.AutoSize = true;
            this.labelAgreementTxt.Location = new System.Drawing.Point(32, 325);
            this.labelAgreementTxt.Name = "labelAgreementTxt";
            this.labelAgreementTxt.Size = new System.Drawing.Size(190, 13);
            this.labelAgreementTxt.TabIndex = 5;
            this.labelAgreementTxt.Text = "Following Applications will be installed .";
            // 
            // checkedListBoxSW
            // 
            this.checkedListBoxSW.CheckOnClick = true;
            this.checkedListBoxSW.FormattingEnabled = true;
            this.checkedListBoxSW.Location = new System.Drawing.Point(383, 244);
            this.checkedListBoxSW.Name = "checkedListBoxSW";
            this.checkedListBoxSW.Size = new System.Drawing.Size(200, 94);
            this.checkedListBoxSW.TabIndex = 6;
            this.checkedListBoxSW.Visible = false;
            // 
            // buttonCustomize
            // 
            this.buttonCustomize.Location = new System.Drawing.Point(383, 281);
            this.buttonCustomize.Name = "buttonCustomize";
            this.buttonCustomize.Size = new System.Drawing.Size(75, 27);
            this.buttonCustomize.TabIndex = 7;
            this.buttonCustomize.Text = "Customize";
            this.buttonCustomize.UseVisualStyleBackColor = true;
            this.buttonCustomize.Click += new System.EventHandler(this.buttonCustomize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 510);
            this.Controls.Add(this.buttonCustomize);
            this.Controls.Add(this.checkedListBoxSW);
            this.Controls.Add(this.labelAgreementTxt);
            this.Controls.Add(this.progressBarInstall);
            this.Controls.Add(this.panelInstallCancel);
            this.Controls.Add(this.flowLayoutPanelSoftwareList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.flowLayoutPanelSoftwareList.ResumeLayout(false);
            this.panelInstallCancel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSoftwareList;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panelInstallCancel;
        private System.Windows.Forms.ProgressBar progressBarInstall;
        private System.Windows.Forms.Label labelAgreementTxt;
        private System.Windows.Forms.CheckedListBox checkedListBoxSW;
        private System.Windows.Forms.Button buttonCustomize;
    }
}

