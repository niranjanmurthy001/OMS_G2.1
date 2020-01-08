namespace Ordermanagement_01.New_Dashboard.Employee
{
    partial class messageform
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
            this.Message_Form = new DevExpress.XtraEditors.GroupControl();
            this.memomessage = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.Message_Form)).BeginInit();
            this.Message_Form.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memomessage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Message_Form
            // 
            this.Message_Form.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message_Form.AppearanceCaption.Options.UseFont = true;
            this.Message_Form.Controls.Add(this.memomessage);
            this.Message_Form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Message_Form.Location = new System.Drawing.Point(0, 0);
            this.Message_Form.Name = "Message_Form";
            this.Message_Form.Size = new System.Drawing.Size(578, 514);
            this.Message_Form.TabIndex = 2;
            this.Message_Form.Text = "Message";
            // 
            // memomessage
            // 
            this.memomessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memomessage.Location = new System.Drawing.Point(2, 21);
            this.memomessage.Name = "memomessage";
            this.memomessage.Properties.AllowFocused = false;
            this.memomessage.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.memomessage.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memomessage.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.memomessage.Properties.Appearance.Options.UseBackColor = true;
            this.memomessage.Properties.Appearance.Options.UseFont = true;
            this.memomessage.Properties.Appearance.Options.UseForeColor = true;
            this.memomessage.Size = new System.Drawing.Size(574, 491);
            this.memomessage.TabIndex = 4;
            // 
            // messageform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(578, 514);
            this.Controls.Add(this.Message_Form);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximumSize = new System.Drawing.Size(594, 1080);
            this.MinimumSize = new System.Drawing.Size(594, 552);
            this.Name = "messageform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message";
            this.Load += new System.EventHandler(this.messageform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Message_Form)).EndInit();
            this.Message_Form.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memomessage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl Message_Form;
        private DevExpress.XtraEditors.MemoEdit memomessage;
    }
}