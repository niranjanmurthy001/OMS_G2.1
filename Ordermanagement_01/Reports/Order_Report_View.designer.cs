namespace Ordermanagement_01.Reports
{
    partial class Order_Report_View
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
            this.grd_Rpt_order_source = new System.Windows.Forms.DataGridView();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.btn_Export = new System.Windows.Forms.Button();
            this.lbl_Total_orders = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Rpt_order_source)).BeginInit();
            this.SuspendLayout();
            // 
            // grd_Rpt_order_source
            // 
            this.grd_Rpt_order_source.AllowUserToAddRows = false;
            this.grd_Rpt_order_source.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_Rpt_order_source.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Rpt_order_source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Rpt_order_source.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Rpt_order_source.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Rpt_order_source.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_Rpt_order_source.ColumnHeadersHeight = 30;
            this.grd_Rpt_order_source.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.grd_Rpt_order_source.Location = new System.Drawing.Point(12, 53);
            this.grd_Rpt_order_source.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grd_Rpt_order_source.Name = "grd_Rpt_order_source";
            this.grd_Rpt_order_source.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_Rpt_order_source.RowHeadersVisible = false;
            this.grd_Rpt_order_source.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.grd_Rpt_order_source.RowTemplate.Height = 30;
            this.grd_Rpt_order_source.Size = new System.Drawing.Size(1032, 552);
            this.grd_Rpt_order_source.TabIndex = 77;
            this.grd_Rpt_order_source.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Rpt_order_source_CellContentClick);
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Orderid";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 42.6396F;
            this.Column1.HeaderText = "S.No";
            this.Column1.Name = "Column1";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 105.2146F;
            this.Column4.HeaderText = "Order Number";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 105.2146F;
            this.Column2.HeaderText = "Client";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 105.2146F;
            this.Column3.HeaderText = "Sub Process";
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.FillWeight = 105.2146F;
            this.Column5.HeaderText = "State";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.FillWeight = 105.2146F;
            this.Column6.HeaderText = "County";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.FillWeight = 105.2146F;
            this.Column7.HeaderText = "Order Type";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.FillWeight = 105.2146F;
            this.Column8.HeaderText = "Orde Type ABS";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.FillWeight = 105.2146F;
            this.Column9.HeaderText = "Borrower Name";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "User Name";
            this.Column10.Name = "Column10";
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.AutoSize = true;
            this.Lbl_Title.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Title.ForeColor = System.Drawing.Color.SteelBlue;
            this.Lbl_Title.Location = new System.Drawing.Point(397, 13);
            this.Lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(0, 31);
            this.Lbl_Title.TabIndex = 78;
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_Export
            // 
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Location = new System.Drawing.Point(958, 13);
            this.btn_Export.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(85, 32);
            this.btn_Export.TabIndex = 79;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // lbl_Total_orders
            // 
            this.lbl_Total_orders.AutoSize = true;
            this.lbl_Total_orders.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total_orders.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_Total_orders.Location = new System.Drawing.Point(17, 18);
            this.lbl_Total_orders.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Total_orders.Name = "lbl_Total_orders";
            this.lbl_Total_orders.Size = new System.Drawing.Size(117, 26);
            this.lbl_Total_orders.TabIndex = 80;
            this.lbl_Total_orders.Text = "Total Orders : ";
            this.lbl_Total_orders.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Order_Report_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 618);
            this.Controls.Add(this.lbl_Total_orders);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.Lbl_Title);
            this.Controls.Add(this.grd_Rpt_order_source);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Order_Report_View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order_Report_View";
            this.Load += new System.EventHandler(this.Order_Report_View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Rpt_order_source)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grd_Rpt_order_source;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Label lbl_Total_orders;
    }
}