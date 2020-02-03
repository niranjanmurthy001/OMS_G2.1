namespace Ordermanagement_01.New_Dashboard.Settings
{
    partial class Demo
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
            this.btn_Processsettings = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Orderentry = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btn_Processsettings
            // 
            this.btn_Processsettings.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Processsettings.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.btn_Processsettings.Appearance.Options.UseFont = true;
            this.btn_Processsettings.Appearance.Options.UseForeColor = true;
            this.btn_Processsettings.AutoSize = true;
            this.btn_Processsettings.Location = new System.Drawing.Point(123, 197);
            this.btn_Processsettings.Name = "btn_Processsettings";
            this.btn_Processsettings.Size = new System.Drawing.Size(107, 22);
            this.btn_Processsettings.TabIndex = 0;
            this.btn_Processsettings.Text = "Process Settings";
            this.btn_Processsettings.Click += new System.EventHandler(this.btn_Processsettings_Click);
            // 
            // btn_Orderentry
            // 
            this.btn_Orderentry.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Orderentry.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.btn_Orderentry.Appearance.Options.UseFont = true;
            this.btn_Orderentry.Appearance.Options.UseForeColor = true;
            this.btn_Orderentry.AutoSize = true;
            this.btn_Orderentry.Location = new System.Drawing.Point(268, 197);
            this.btn_Orderentry.Name = "btn_Orderentry";
            this.btn_Orderentry.Size = new System.Drawing.Size(78, 22);
            this.btn_Orderentry.TabIndex = 1;
            this.btn_Orderentry.Text = "Order Entry";
            this.btn_Orderentry.Click += new System.EventHandler(this.btn_Orderentry_Click);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 482);
            this.Controls.Add(this.btn_Orderentry);
            this.Controls.Add(this.btn_Processsettings);
            this.LookAndFeel.SkinName = "Office 2013";
            this.Name = "Demo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btn_Processsettings;
        private DevExpress.XtraEditors.SimpleButton btn_Orderentry;
    }
}