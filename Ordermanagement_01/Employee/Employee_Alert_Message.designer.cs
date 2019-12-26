namespace Ordermanagement_01.Employee
{
    partial class Employee_Alert_Message
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
            this.txt_Order_Instructions = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txt_Order_Instructions
            // 
            this.txt_Order_Instructions.BackColor = System.Drawing.Color.PeachPuff;
            this.txt_Order_Instructions.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Order_Instructions.Location = new System.Drawing.Point(17, 21);
            this.txt_Order_Instructions.Multiline = true;
            this.txt_Order_Instructions.Name = "txt_Order_Instructions";
            this.txt_Order_Instructions.ReadOnly = true;
            this.txt_Order_Instructions.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Order_Instructions.Size = new System.Drawing.Size(661, 317);
            this.txt_Order_Instructions.TabIndex = 99;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
           // this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Employee_Alert_Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(689, 356);
            this.Controls.Add(this.txt_Order_Instructions);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Employee_Alert_Message";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee_Alert_Message";
            this.Load += new System.EventHandler(this.Employee_Alert_Message_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Order_Instructions;
        private System.Windows.Forms.Timer timer1;
    }
}