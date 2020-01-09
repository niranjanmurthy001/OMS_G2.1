namespace Ordermanagement_01.Tax
{
    partial class Tax_Inhouse_Order_View
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
            this.lbl_Header = new System.Windows.Forms.Label();
            this.txt_Order_Number = new System.Windows.Forms.TextBox();
            this.grd_Admin_orders = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Admin_orders)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbl_Header.Location = new System.Drawing.Point(496, 9);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(167, 31);
            this.lbl_Header.TabIndex = 150;
            this.lbl_Header.Text = "ORDERS DETAILS";
            this.lbl_Header.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_Order_Number
            // 
            this.txt_Order_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Order_Number.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Order_Number.ForeColor = System.Drawing.Color.SlateGray;
            this.txt_Order_Number.Location = new System.Drawing.Point(940, 23);
            this.txt_Order_Number.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Order_Number.Name = "txt_Order_Number";
            this.txt_Order_Number.Size = new System.Drawing.Size(269, 28);
            this.txt_Order_Number.TabIndex = 152;
            this.txt_Order_Number.Text = "Search Order number.....";
            this.txt_Order_Number.TextChanged += new System.EventHandler(this.txt_Order_Number_TextChanged);
            this.txt_Order_Number.MouseEnter += new System.EventHandler(this.txt_Order_Number_MouseEnter);
            // 
            // grd_Admin_orders
            // 
            this.grd_Admin_orders.AllowUserToAddRows = false;
            this.grd_Admin_orders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Admin_orders.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grd_Admin_orders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Admin_orders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Admin_orders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Admin_orders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_Admin_orders.ColumnHeadersHeight = 30;
            this.grd_Admin_orders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column11,
            this.Column6,
            this.Column7,
            this.Column13});
            this.grd_Admin_orders.Location = new System.Drawing.Point(2, 53);
            this.grd_Admin_orders.Name = "grd_Admin_orders";
            this.grd_Admin_orders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_Admin_orders.RowHeadersVisible = false;
            this.grd_Admin_orders.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.grd_Admin_orders.Size = new System.Drawing.Size(1210, 457);
            this.grd_Admin_orders.TabIndex = 153;
            this.grd_Admin_orders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Admin_orders_CellClick);
            // 
            // Column8
            // 
            this.Column8.FillWeight = 39.77002F;
            this.Column8.HeaderText = "S. No";
            this.Column8.Name = "Column8";
            // 
            // Column1
            // 
            this.Column1.FillWeight = 67.16708F;
            this.Column1.HeaderText = "CLIENT";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 132.2441F;
            this.Column2.HeaderText = "SUB CLIENT";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 103.8503F;
            this.Column3.HeaderText = "ORDER NUMBER";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 91.25575F;
            this.Column4.HeaderText = "ORDER TYPE";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ORDER ASSIGNED TYPE";
            this.Column5.Name = "Column5";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "STATECOUNTY";
            this.Column11.Name = "Column11";
            // 
            // Column6
            // 
            dataGridViewCellStyle2.Format = "MM/dd/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column6.FillWeight = 86.96323F;
            this.Column6.HeaderText = "ASSIGNED DATE";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.FillWeight = 92.61018F;
            this.Column7.HeaderText = "STATUS";
            this.Column7.Name = "Column7";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Order_Id";
            this.Column13.Name = "Column13";
            this.Column13.Visible = false;
            // 
            // Tax_Inhouse_Order_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 522);
            this.Controls.Add(this.grd_Admin_orders);
            this.Controls.Add(this.txt_Order_Number);
            this.Controls.Add(this.lbl_Header);
            this.Name = "Tax_Inhouse_Order_View";
            this.Text = "Tax_Inhouse_Order_View";
            this.Load += new System.EventHandler(this.Tax_Inhouse_Order_View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Admin_orders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.TextBox txt_Order_Number;
        private System.Windows.Forms.DataGridView grd_Admin_orders;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    }
}