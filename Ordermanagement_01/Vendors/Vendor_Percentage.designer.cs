namespace Ordermanagement_01.Vendors
{
    partial class Vendor_Percentage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_SaveAll = new System.Windows.Forms.Button();
            this.GridVendor_Percentage = new System.Windows.Forms.DataGridView();
            this.Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.County = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.lbl_Total = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GridVendor_Percentage)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_SaveAll
            // 
            this.btn_SaveAll.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_SaveAll.Location = new System.Drawing.Point(228, 443);
            this.btn_SaveAll.Name = "btn_SaveAll";
            this.btn_SaveAll.Size = new System.Drawing.Size(126, 37);
            this.btn_SaveAll.TabIndex = 155;
            this.btn_SaveAll.Text = "Save All changes";
            this.btn_SaveAll.UseVisualStyleBackColor = true;
            this.btn_SaveAll.Click += new System.EventHandler(this.btn_SaveAll_Click);
            // 
            // GridVendor_Percentage
            // 
            this.GridVendor_Percentage.AllowUserToAddRows = false;
            this.GridVendor_Percentage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridVendor_Percentage.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridVendor_Percentage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridVendor_Percentage.ColumnHeadersHeight = 36;
            this.GridVendor_Percentage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sno,
            this.State,
            this.County,
            this.Column1,
            this.Column2});
            this.GridVendor_Percentage.Location = new System.Drawing.Point(6, 52);
            this.GridVendor_Percentage.Name = "GridVendor_Percentage";
            this.GridVendor_Percentage.RowHeadersVisible = false;
            this.GridVendor_Percentage.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GridVendor_Percentage.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.GridVendor_Percentage.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridVendor_Percentage.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.GridVendor_Percentage.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.GridVendor_Percentage.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.GridVendor_Percentage.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridVendor_Percentage.Size = new System.Drawing.Size(600, 385);
            this.GridVendor_Percentage.TabIndex = 154;
            this.GridVendor_Percentage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridVendor_Percentage_CellEnter);
            this.GridVendor_Percentage.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridVendor_Percentage_CellValueChanged);
            this.GridVendor_Percentage.CurrentCellDirtyStateChanged += new System.EventHandler(this.GridVendor_Percentage_CurrentCellDirtyStateChanged);
            this.GridVendor_Percentage.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GridVendor_Percentage_EditingControlShowing);
            this.GridVendor_Percentage.Enter += new System.EventHandler(this.GridVendor_Percentage_Enter);
            this.GridVendor_Percentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridVendor_Percentage_KeyDown);
            // 
            // Sno
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Sno.DefaultCellStyle = dataGridViewCellStyle4;
            this.Sno.FillWeight = 25.0802F;
            this.Sno.HeaderText = "S.No";
            this.Sno.Name = "Sno";
            // 
            // State
            // 
            this.State.FillWeight = 152.1532F;
            this.State.HeaderText = "Vendor Name";
            this.State.Name = "State";
            // 
            // County
            // 
            this.County.FillWeight = 152.1532F;
            this.County.HeaderText = "Percentage";
            this.County.Name = "County";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Vendor_ID";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Ven_PercentageID";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.label2.Location = new System.Drawing.Point(186, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 31);
            this.label2.TabIndex = 153;
            this.label2.Text = "VENDOR PERCENTAGE";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(8, 12);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(34, 34);
            this.btn_Refresh.TabIndex = 156;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // lbl_Total
            // 
            this.lbl_Total.AutoSize = true;
            this.lbl_Total.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total.Location = new System.Drawing.Point(574, 24);
            this.lbl_Total.Name = "lbl_Total";
            this.lbl_Total.Size = new System.Drawing.Size(0, 20);
            this.lbl_Total.TabIndex = 158;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(436, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 20);
            this.label4.TabIndex = 157;
            this.label4.Text = "Total Percentage(%) :";
            // 
            // Vendor_Percentage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 487);
            this.Controls.Add(this.lbl_Total);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btn_SaveAll);
            this.Controls.Add(this.GridVendor_Percentage);
            this.Controls.Add(this.label2);
            this.Name = "Vendor_Percentage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendor_Percentage";
            this.Load += new System.EventHandler(this.Vendor_Percentage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridVendor_Percentage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_SaveAll;
        private System.Windows.Forms.DataGridView GridVendor_Percentage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn County;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label lbl_Total;
        private System.Windows.Forms.Label label4;
    }
}