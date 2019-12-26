namespace Ordermanagement_01.Employee
{
    partial class Production_Time_Update
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
            this.components = new System.ComponentModel.Container();
            this.Production_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Production_Timer
            // 
            this.Production_Timer.Enabled = true;
            this.Production_Timer.Interval = 600000;
            this.Production_Timer.Tick += new System.EventHandler(this.Production_Timer_Tick);
            // 
            // Production_Time_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 262);
            this.Name = "Production_Time_Update";
            this.Text = "Production_Time_Update";
            this.Load += new System.EventHandler(this.Production_Time_Update_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer Production_Timer;
    }
}