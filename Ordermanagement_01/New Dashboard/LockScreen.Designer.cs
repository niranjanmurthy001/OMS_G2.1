namespace Ordermanagement_01.New_Dashboard
{
    partial class LockScreen
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBoxProfile = new System.Windows.Forms.PictureBox();
            this.lblEmployeeName = new DevExpress.XtraEditors.LabelControl();
            this.lblCopyright = new DevExpress.XtraEditors.LabelControl();
            this.pictureEditLogo = new DevExpress.XtraEditors.PictureEdit();
            this.btnViewPassword = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.pictureEditClose = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditClose.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.panelControl1.Controls.Add(this.pictureBoxProfile);
            this.panelControl1.Controls.Add(this.lblEmployeeName);
            this.panelControl1.Controls.Add(this.lblCopyright);
            this.panelControl1.Controls.Add(this.pictureEditLogo);
            this.panelControl1.Controls.Add(this.btnViewPassword);
            this.panelControl1.Controls.Add(this.btnLogin);
            this.panelControl1.Controls.Add(this.textEditPassword);
            this.panelControl1.Controls.Add(this.pictureEditClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(385, 503);
            this.panelControl1.TabIndex = 1;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // pictureBoxProfile
            // 
            this.pictureBoxProfile.Location = new System.Drawing.Point(143, 117);
            this.pictureBoxProfile.Name = "pictureBoxProfile";
            this.pictureBoxProfile.Size = new System.Drawing.Size(97, 90);
            this.pictureBoxProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProfile.TabIndex = 10;
            this.pictureBoxProfile.TabStop = false;
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(111)))), ((int)(((byte)(225)))));
            this.lblEmployeeName.Appearance.Options.UseFont = true;
            this.lblEmployeeName.Appearance.Options.UseForeColor = true;
            this.lblEmployeeName.Appearance.Options.UseTextOptions = true;
            this.lblEmployeeName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblEmployeeName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblEmployeeName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblEmployeeName.Location = new System.Drawing.Point(39, 213);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(300, 40);
            this.lblEmployeeName.TabIndex = 9;
            this.lblEmployeeName.Text = "Employee Name";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Appearance.Options.UseFont = true;
            this.lblCopyright.Appearance.Options.UseTextOptions = true;
            this.lblCopyright.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblCopyright.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCopyright.Location = new System.Drawing.Point(39, 478);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(300, 13);
            this.lblCopyright.TabIndex = 8;
            // 
            // pictureEditLogo
            // 
            this.pictureEditLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEditLogo.Location = new System.Drawing.Point(39, 30);
            this.pictureEditLogo.Name = "pictureEditLogo";
            this.pictureEditLogo.Properties.AllowFocused = false;
            this.pictureEditLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEditLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEditLogo.Size = new System.Drawing.Size(284, 64);
            this.pictureEditLogo.TabIndex = 7;
            this.pictureEditLogo.EditValueChanged += new System.EventHandler(this.pictureEditLogo_EditValueChanged);
            // 
            // btnViewPassword
            // 
            this.btnViewPassword.AllowFocus = false;
            this.btnViewPassword.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnViewPassword.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnViewPassword.Appearance.Options.UseBackColor = true;
            this.btnViewPassword.Appearance.Options.UseForeColor = true;
            this.btnViewPassword.AppearanceHovered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(111)))), ((int)(((byte)(225)))));
            this.btnViewPassword.AppearanceHovered.Options.UseForeColor = true;
            this.btnViewPassword.AppearancePressed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(91)))), ((int)(((byte)(234)))));
            this.btnViewPassword.AppearancePressed.Options.UseForeColor = true;
            this.btnViewPassword.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnViewPassword.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnViewPassword.Location = new System.Drawing.Point(343, 308);
            this.btnViewPassword.Name = "btnViewPassword";
            this.btnViewPassword.Size = new System.Drawing.Size(28, 22);
            this.btnViewPassword.TabIndex = 6;
            this.btnViewPassword.ToolTip = "View Password";
            this.btnViewPassword.Visible = false;
            this.btnViewPassword.Click += new System.EventHandler(this.btnViewPassword_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(91)))), ((int)(((byte)(234)))));
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Appearance.Options.UseForeColor = true;
            this.btnLogin.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(111)))), ((int)(((byte)(225)))));
            this.btnLogin.AppearanceHovered.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.AppearanceHovered.ForeColor = System.Drawing.Color.White;
            this.btnLogin.AppearanceHovered.Options.UseBackColor = true;
            this.btnLogin.AppearanceHovered.Options.UseFont = true;
            this.btnLogin.AppearanceHovered.Options.UseForeColor = true;
            this.btnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnLogin.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnLogin.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnLogin.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnLogin.Location = new System.Drawing.Point(39, 372);
            this.btnLogin.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(300, 40);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // textEditPassword
            // 
            this.textEditPassword.EditValue = "";
            this.textEditPassword.Location = new System.Drawing.Point(37, 299);
            this.textEditPassword.MaximumSize = new System.Drawing.Size(300, 40);
            this.textEditPassword.MinimumSize = new System.Drawing.Size(250, 40);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEditPassword.Properties.Appearance.Options.UseFont = true;
            this.textEditPassword.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(111)))), ((int)(((byte)(225)))));
            this.textEditPassword.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(111)))), ((int)(((byte)(225)))));
            this.textEditPassword.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.textEditPassword.Properties.AppearanceFocused.Options.UseForeColor = true;
            this.textEditPassword.Properties.AutoHeight = false;
            this.textEditPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.textEditPassword.Properties.ContextImageOptions.AllowChangeAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.textEditPassword.Properties.UseSystemPasswordChar = true;
            this.textEditPassword.Properties.EditValueChanged += new System.EventHandler(this.textEditPassword_EditValueChanged);
            this.textEditPassword.Size = new System.Drawing.Size(300, 40);
            this.textEditPassword.TabIndex = 2;
            this.textEditPassword.ToolTip = "Password";
            // 
            // pictureEditClose
            // 
            this.pictureEditClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEditClose.Location = new System.Drawing.Point(359, 5);
            this.pictureEditClose.Name = "pictureEditClose";
            this.pictureEditClose.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEditClose.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopRight;
            this.pictureEditClose.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEditClose.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEditClose.Size = new System.Drawing.Size(21, 21);
            this.pictureEditClose.TabIndex = 0;
            this.pictureEditClose.ToolTip = "Exit";
            this.pictureEditClose.Click += new System.EventHandler(this.pictureEditClose_Click);
            // 
            // LockScreen
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 503);
            this.Controls.Add(this.panelControl1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LockScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LockScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LockScreen_FormClosing);
            this.Load += new System.EventHandler(this.LockScreen_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LockScreen_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditClose.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        public DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.PictureEdit pictureEditClose;
        private DevExpress.XtraEditors.PictureEdit pictureEditLogo;
        private DevExpress.XtraEditors.LabelControl lblCopyright;
        public DevExpress.XtraEditors.LabelControl lblEmployeeName;
        public System.Windows.Forms.PictureBox pictureBoxProfile;
        public DevExpress.XtraEditors.SimpleButton btnViewPassword;
    }
}