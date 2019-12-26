namespace Ordermanagement_01.Vendors
{
    partial class Vendor_Order_Queau
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblRecordsStatus = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lbl_count = new System.Windows.Forms.Label();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.OrderNumber = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.rb_Completed = new System.Windows.Forms.RadioButton();
            this.Rb_Current = new System.Windows.Forms.RadioButton();
            this.txt_SearchOrdernumber = new System.Windows.Forms.TextBox();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_Set_Priority = new System.Windows.Forms.Button();
            this.grid_Export = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Export)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnFirst);
            this.panel1.Controls.Add(this.lblRecordsStatus);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.lbl_count);
            this.panel1.Location = new System.Drawing.Point(2, 572);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1219, 47);
            this.panel1.TabIndex = 25;
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFirst.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.btnFirst.Location = new System.Drawing.Point(230, 10);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 27);
            this.btnFirst.TabIndex = 16;
            this.btnFirst.Text = "|< First";
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblRecordsStatus
            // 
            this.lblRecordsStatus.AutoSize = true;
            this.lblRecordsStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecordsStatus.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lblRecordsStatus.Location = new System.Drawing.Point(608, 12);
            this.lblRecordsStatus.Name = "lblRecordsStatus";
            this.lblRecordsStatus.Size = new System.Drawing.Size(38, 22);
            this.lblRecordsStatus.TabIndex = 15;
            this.lblRecordsStatus.Text = "0 / 0";
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrevious.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.btnPrevious.Location = new System.Drawing.Point(415, 11);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(50, 27);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.Text = "< Pervious";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLast.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.btnLast.Location = new System.Drawing.Point(934, 9);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 28);
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = "Last >|";
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNext.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.btnNext.Location = new System.Drawing.Point(809, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 26);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lbl_count
            // 
            this.lbl_count.AutoSize = true;
            this.lbl_count.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_count.Location = new System.Drawing.Point(1081, 13);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(46, 20);
            this.lbl_count.TabIndex = 2;
            this.lbl_count.Text = "label1";
            // 
            // grd_order
            // 
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_order.ColumnHeadersHeight = 32;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column12,
            this.Column5,
            this.Column14,
            this.OrderNumber,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column8,
            this.Column9,
            this.Column6,
            this.Column10,
            this.Column13,
            this.Column11});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle3;
            this.grd_order.Location = new System.Drawing.Point(2, 67);
            this.grd_order.Name = "grd_order";
            this.grd_order.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.RowHeadersWidth = 30;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_order.RowTemplate.Height = 25;
            this.grd_order.Size = new System.Drawing.Size(1219, 502);
            this.grd_order.TabIndex = 24;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            // 
            // Column12
            // 
            this.Column12.HeaderText = "S.No";
            this.Column12.Name = "Column12";
            this.Column12.Width = 50;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Rush";
            this.Column5.Name = "Column5";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "DRN ORDER.NO";
            this.Column14.Name = "Column14";
            this.Column14.Width = 120;
            // 
            // OrderNumber
            // 
            this.OrderNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.OrderNumber.HeaderText = "ORDER NUMBER";
            this.OrderNumber.MinimumWidth = 2;
            this.OrderNumber.Name = "OrderNumber";
            this.OrderNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OrderNumber.Text = "";
            this.OrderNumber.Width = 150;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CLIENT";
            this.Column1.Name = "Column1";
            this.Column1.Width = 110;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "SUB CLIENT";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "RECEIVED DATE";
            this.Column3.Name = "Column3";
            this.Column3.Width = 96;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ORDER TYPE";
            this.Column4.Name = "Column4";
            this.Column4.Width = 160;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "STATE COUNTY";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "TASK";
            this.Column9.Name = "Column9";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "STATUS";
            this.Column6.Name = "Column6";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "VENDOR";
            this.Column10.Name = "Column10";
            this.Column10.Width = 120;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "ASSIGNED DATE";
            this.Column13.Name = "Column13";
            this.Column13.Width = 120;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "ORDER ID";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            this.Column11.Width = 85;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.label3.Location = new System.Drawing.Point(509, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 31);
            this.label3.TabIndex = 26;
            this.label3.Text = "VENDOR ORDER QUEUE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rb_Completed
            // 
            this.rb_Completed.AutoSize = true;
            this.rb_Completed.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.rb_Completed.Location = new System.Drawing.Point(655, 33);
            this.rb_Completed.Name = "rb_Completed";
            this.rb_Completed.Size = new System.Drawing.Size(139, 24);
            this.rb_Completed.TabIndex = 31;
            this.rb_Completed.TabStop = true;
            this.rb_Completed.Text = "Completed Orders";
            this.rb_Completed.UseVisualStyleBackColor = true;
            this.rb_Completed.CheckedChanged += new System.EventHandler(this.rb_Completed_CheckedChanged);
            // 
            // Rb_Current
            // 
            this.Rb_Current.AutoSize = true;
            this.Rb_Current.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.Rb_Current.Location = new System.Drawing.Point(531, 33);
            this.Rb_Current.Name = "Rb_Current";
            this.Rb_Current.Size = new System.Drawing.Size(118, 24);
            this.Rb_Current.TabIndex = 30;
            this.Rb_Current.TabStop = true;
            this.Rb_Current.Text = "Current Orders";
            this.Rb_Current.UseVisualStyleBackColor = true;
            this.Rb_Current.CheckedChanged += new System.EventHandler(this.Rb_Current_CheckedChanged);
            // 
            // txt_SearchOrdernumber
            // 
            this.txt_SearchOrdernumber.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SearchOrdernumber.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.txt_SearchOrdernumber.Location = new System.Drawing.Point(4, 33);
            this.txt_SearchOrdernumber.Name = "txt_SearchOrdernumber";
            this.txt_SearchOrdernumber.Size = new System.Drawing.Size(224, 28);
            this.txt_SearchOrdernumber.TabIndex = 150;
            this.txt_SearchOrdernumber.Text = "Search by order number...";
            this.txt_SearchOrdernumber.Click += new System.EventHandler(this.txt_SearchOrdernumber_Click);
            this.txt_SearchOrdernumber.TextChanged += new System.EventHandler(this.txt_SearchOrdernumber_TextChanged);
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Location = new System.Drawing.Point(1088, 29);
            this.btn_Export.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(132, 32);
            this.btn_Export.TabIndex = 151;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Set_Priority
            // 
            this.btn_Set_Priority.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Set_Priority.Location = new System.Drawing.Point(233, 30);
            this.btn_Set_Priority.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Set_Priority.Name = "btn_Set_Priority";
            this.btn_Set_Priority.Size = new System.Drawing.Size(132, 32);
            this.btn_Set_Priority.TabIndex = 152;
            this.btn_Set_Priority.Text = "Set Priority";
            this.btn_Set_Priority.UseVisualStyleBackColor = true;
            this.btn_Set_Priority.Click += new System.EventHandler(this.btn_Set_Priority_Click);
            // 
            // grid_Export
            // 
            this.grid_Export.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_Export.Location = new System.Drawing.Point(373, 30);
            this.grid_Export.Name = "grid_Export";
            this.grid_Export.Size = new System.Drawing.Size(152, 32);
            this.grid_Export.TabIndex = 153;
            this.grid_Export.Visible = false;
            // 
            // Vendor_Order_Queau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 614);
            this.Controls.Add(this.grid_Export);
            this.Controls.Add(this.btn_Set_Priority);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.txt_SearchOrdernumber);
            this.Controls.Add(this.rb_Completed);
            this.Controls.Add(this.Rb_Current);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grd_order);
            this.Name = "Vendor_Order_Queau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendor_Order_Queau";
            this.Load += new System.EventHandler(this.Vendor_Order_Queau_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Export)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblRecordsStatus;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lbl_count;
        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rb_Completed;
        private System.Windows.Forms.RadioButton Rb_Current;
        private System.Windows.Forms.TextBox txt_SearchOrdernumber;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column14;
        private System.Windows.Forms.DataGridViewButtonColumn OrderNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.Button btn_Set_Priority;
        private System.Windows.Forms.DataGridView grid_Export;
    }
}