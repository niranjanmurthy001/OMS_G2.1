namespace Ordermanagement_01.Employee
{
    partial class State_Wise_Tax_Due_Date
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
            this.Grid_Tax = new System.Windows.Forms.DataGridView();
            this.lbl_Header = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Tax)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid_Tax
            // 
            this.Grid_Tax.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid_Tax.Location = new System.Drawing.Point(36, 59);
            this.Grid_Tax.Name = "Grid_Tax";
            this.Grid_Tax.Size = new System.Drawing.Size(786, 155);
            this.Grid_Tax.TabIndex = 0;
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_Header.Location = new System.Drawing.Point(306, 9);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(283, 31);
            this.lbl_Header.TabIndex = 94;
            this.lbl_Header.Text = "U.S State Tax Office Due Dates";
            // 
            // State_Wise_Tax_Due_Date
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 281);
            this.Controls.Add(this.lbl_Header);
            this.Controls.Add(this.Grid_Tax);
            this.Name = "State_Wise_Tax_Due_Date";
            this.Text = "State_Wise_Tax_Due_Date";
            this.Load += new System.EventHandler(this.State_Wise_Tax_Due_Date_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Tax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Grid_Tax;
        private System.Windows.Forms.Label lbl_Header;
    }
}