namespace Ordermanagement_01.Employee
{
    partial class Employee_User_Order_Data_Update
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
            this.User_Order_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // User_Order_Timer
            // 
            this.User_Order_Timer.Enabled = true;
            this.User_Order_Timer.Interval = 60000;
            this.User_Order_Timer.Tick += new System.EventHandler(this.User_Order_Timer_Tick);
            // 
            // Employee_User_Order_Data_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 262);
            this.Name = "Employee_User_Order_Data_Update";
            this.Text = "Employee_User_Order_Data_Update";
            this.Load += new System.EventHandler(this.Employee_User_Order_Data_Update_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer User_Order_Timer;
    }
}