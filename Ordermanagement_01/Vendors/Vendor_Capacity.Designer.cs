namespace Ordermanagement_01.Vendors
{
    partial class Vendor_Capacity
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.GridVendor_Capacity = new System.Windows.Forms.DataGridView();
            this.btn_SaveAll = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.Sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.County = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridVendor_Capacity)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.label2.Location = new System.Drawing.Point(197, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 31);
            this.label2.TabIndex = 140;
            this.label2.Text = "VENDOR CAPACITY";
            // 
            // GridVendor_Capacity
            // 
            this.GridVendor_Capacity.AllowUserToAddRows = false;
            this.GridVendor_Capacity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridVendor_Capacity.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridVendor_Capacity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridVendor_Capacity.ColumnHeadersHeight = 36;
            this.GridVendor_Capacity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sno,
            this.State,
            this.County,
            this.Column1,
            this.Column2});
            this.GridVendor_Capacity.Location = new System.Drawing.Point(10, 52);
            this.GridVendor_Capacity.Name = "GridVendor_Capacity";
            this.GridVendor_Capacity.RowHeadersVisible = false;
            this.GridVendor_Capacity.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GridVendor_Capacity.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.GridVendor_Capacity.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridVendor_Capacity.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.GridVendor_Capacity.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.GridVendor_Capacity.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.GridVendor_Capacity.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridVendor_Capacity.Size = new System.Drawing.Size(600, 385);
            this.GridVendor_Capacity.TabIndex = 149;
            this.GridVendor_Capacity.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.GridVendor_Capacity_CellValidating);
            // 
            // btn_SaveAll
            // 
            this.btn_SaveAll.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_SaveAll.Location = new System.Drawing.Point(235, 443);
            this.btn_SaveAll.Name = "btn_SaveAll";
            this.btn_SaveAll.Size = new System.Drawing.Size(126, 37);
            this.btn_SaveAll.TabIndex = 151;
            this.btn_SaveAll.Text = "Save All changes";
            this.btn_SaveAll.UseVisualStyleBackColor = true;
            this.btn_SaveAll.Click += new System.EventHandler(this.btn_SaveAll_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(12, 12);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(34, 34);
            this.btn_Refresh.TabIndex = 152;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // Sno
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Sno.DefaultCellStyle = dataGridViewCellStyle2;
            this.Sno.FillWeight = 19.794F;
            this.Sno.HeaderText = "S.No";
            this.Sno.MinimumWidth = 80;
            this.Sno.Name = "Sno";
            this.Sno.ReadOnly = true;
            // 
            // State
            // 
            this.State.FillWeight = 154.7963F;
            this.State.HeaderText = "Vendor Name";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // County
            // 
            this.County.FillWeight = 154.7963F;
            this.County.HeaderText = "No of Orders (Capacity)";
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
            this.Column2.HeaderText = "Ven_CapID";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // Vendor_Capacity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 487);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btn_SaveAll);
            this.Controls.Add(this.GridVendor_Capacity);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.Name = "Vendor_Capacity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendor_Capacity";
            this.Load += new System.EventHandler(this.Vendor_Capacity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridVendor_Capacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView GridVendor_Capacity;
        private System.Windows.Forms.Button btn_SaveAll;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn County;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}