namespace Ordermanagement_01.Employee
{
    partial class ErrorComments
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
            this.memoEditComments = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComments.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEditComments
            // 
            this.memoEditComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEditComments.EditValue = "";
            this.memoEditComments.Enabled = false;
            this.memoEditComments.Location = new System.Drawing.Point(0, 0);
            this.memoEditComments.Name = "memoEditComments";
            this.memoEditComments.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.memoEditComments.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoEditComments.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.memoEditComments.Properties.Appearance.Options.UseBackColor = true;
            this.memoEditComments.Properties.Appearance.Options.UseFont = true;
            this.memoEditComments.Properties.Appearance.Options.UseForeColor = true;
            this.memoEditComments.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEditComments.Properties.LookAndFeel.SkinName = "Office 2010 Blue";
            this.memoEditComments.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.memoEditComments.Properties.ReadOnly = true;
            this.memoEditComments.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.memoEditComments.Size = new System.Drawing.Size(684, 312);
            this.memoEditComments.TabIndex = 1;
            // 
            // ErrorComments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 312);
            this.Controls.Add(this.memoEditComments);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorComments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comments";
            this.Load += new System.EventHandler(this.ErrorComments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComments.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEditComments;
    }
}