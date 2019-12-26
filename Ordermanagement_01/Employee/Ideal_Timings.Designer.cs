namespace Ordermanagement_01.Employee
{
    partial class Ideal_Timings
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
            this.Ideal_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Ideal_Timer
            // 
            this.Ideal_Timer.Enabled = true;
            this.Ideal_Timer.Interval = 60000;
            this.Ideal_Timer.Tick += new System.EventHandler(this.Ideal_Timer_Tick);
            // 
            // Ideal_Timings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 262);
            this.Name = "Ideal_Timings";
            this.Text = "Ideal_Timings";
            this.Load += new System.EventHandler(this.Ideal_Timings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer Ideal_Timer;

    }
}