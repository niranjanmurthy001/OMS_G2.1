namespace Ordermanagement_01.Tax
{
    partial class Tax_Completed_Mail
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
            this.lbl_Header = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.txt_Search_Order_no = new System.Windows.Forms.TextBox();
            this.rbtn_TaxInvoice_Sended = new System.Windows.Forms.RadioButton();
            this.rbtn_TaxInvoice_NotSended = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_Total_Orders = new System.Windows.Forms.Label();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblRecordsStatus = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.grd_All_Tax_Completed_orders = new System.Windows.Forms.DataGridView();
            this.btn_Send_All = new System.Windows.Forms.Button();
            this.chk_All = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_All_Tax_Completed_orders)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Header
            // 
            this.lbl_Header.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Header.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.Black;
            this.lbl_Header.Location = new System.Drawing.Point(459, 2);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(244, 31);
            this.lbl_Header.TabIndex = 157;
            this.lbl_Header.Text = "ORDER DELIVERY STATUS";
            this.lbl_Header.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Controls.Add(this.txt_Search_Order_no);
            this.panel1.Controls.Add(this.lbl_Header);
            this.panel1.Controls.Add(this.rbtn_TaxInvoice_Sended);
            this.panel1.Controls.Add(this.rbtn_TaxInvoice_NotSended);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1202, 87);
            this.panel1.TabIndex = 158;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(7, 44);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(37, 33);
            this.btn_Refresh.TabIndex = 224;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // txt_Search_Order_no
            // 
            this.txt_Search_Order_no.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txt_Search_Order_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Search_Order_no.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Search_Order_no.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txt_Search_Order_no.Location = new System.Drawing.Point(863, 51);
            this.txt_Search_Order_no.Multiline = true;
            this.txt_Search_Order_no.Name = "txt_Search_Order_no";
            this.txt_Search_Order_no.Size = new System.Drawing.Size(325, 27);
            this.txt_Search_Order_no.TabIndex = 233;
            this.txt_Search_Order_no.TextChanged += new System.EventHandler(this.txt_Search_Order_no_TextChanged);
            this.txt_Search_Order_no.MouseEnter += new System.EventHandler(this.txt_Search_Order_no_MouseEnter);
            // 
            // rbtn_TaxInvoice_Sended
            // 
            this.rbtn_TaxInvoice_Sended.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbtn_TaxInvoice_Sended.AutoSize = true;
            this.rbtn_TaxInvoice_Sended.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_TaxInvoice_Sended.Location = new System.Drawing.Point(600, 47);
            this.rbtn_TaxInvoice_Sended.Name = "rbtn_TaxInvoice_Sended";
            this.rbtn_TaxInvoice_Sended.Size = new System.Drawing.Size(94, 28);
            this.rbtn_TaxInvoice_Sended.TabIndex = 231;
            this.rbtn_TaxInvoice_Sended.Text = "Delivered";
            this.rbtn_TaxInvoice_Sended.UseVisualStyleBackColor = true;
            this.rbtn_TaxInvoice_Sended.CheckedChanged += new System.EventHandler(this.rbtn_TaxInvoice_Sended_CheckedChanged);
            // 
            // rbtn_TaxInvoice_NotSended
            // 
            this.rbtn_TaxInvoice_NotSended.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rbtn_TaxInvoice_NotSended.AutoSize = true;
            this.rbtn_TaxInvoice_NotSended.Checked = true;
            this.rbtn_TaxInvoice_NotSended.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_TaxInvoice_NotSended.Location = new System.Drawing.Point(441, 47);
            this.rbtn_TaxInvoice_NotSended.Name = "rbtn_TaxInvoice_NotSended";
            this.rbtn_TaxInvoice_NotSended.Size = new System.Drawing.Size(134, 28);
            this.rbtn_TaxInvoice_NotSended.TabIndex = 230;
            this.rbtn_TaxInvoice_NotSended.TabStop = true;
            this.rbtn_TaxInvoice_NotSended.Text = "Delivery Queue";
            this.rbtn_TaxInvoice_NotSended.UseVisualStyleBackColor = true;
            this.rbtn_TaxInvoice_NotSended.CheckedChanged += new System.EventHandler(this.rbtn_TaxInvoice_NotSended_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.grd_All_Tax_Completed_orders);
            this.panel3.Controls.Add(this.btn_Send_All);
            this.panel3.Controls.Add(this.chk_All);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1202, 539);
            this.panel3.TabIndex = 235;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lbl_Total_Orders);
            this.panel2.Controls.Add(this.btnFirst);
            this.panel2.Controls.Add(this.lblRecordsStatus);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnPrevious);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Location = new System.Drawing.Point(13, 490);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1176, 40);
            this.panel2.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1069, 10);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 19);
            this.label8.TabIndex = 27;
            this.label8.Text = "Total Orders:";
            // 
            // lbl_Total_Orders
            // 
            this.lbl_Total_Orders.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Total_Orders.AutoSize = true;
            this.lbl_Total_Orders.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total_Orders.ForeColor = System.Drawing.Color.Red;
            this.lbl_Total_Orders.Location = new System.Drawing.Point(1156, 11);
            this.lbl_Total_Orders.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Total_Orders.Name = "lbl_Total_Orders";
            this.lbl_Total_Orders.Size = new System.Drawing.Size(14, 17);
            this.lbl_Total_Orders.TabIndex = 28;
            this.lbl_Total_Orders.Text = "T";
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(207, 8);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 24);
            this.btnFirst.TabIndex = 21;
            this.btnFirst.Text = "|< First";
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblRecordsStatus
            // 
            this.lblRecordsStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRecordsStatus.AutoSize = true;
            this.lblRecordsStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecordsStatus.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lblRecordsStatus.Location = new System.Drawing.Point(535, 10);
            this.lblRecordsStatus.Name = "lblRecordsStatus";
            this.lblRecordsStatus.Size = new System.Drawing.Size(38, 22);
            this.lblRecordsStatus.TabIndex = 20;
            this.lblRecordsStatus.Text = "0 / 0";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(656, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 24);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrevious.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Location = new System.Drawing.Point(365, 8);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 24);
            this.btnPrevious.TabIndex = 18;
            this.btnPrevious.Text = "< Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(807, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 24);
            this.btnLast.TabIndex = 19;
            this.btnLast.Text = "Last >|";
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // grd_All_Tax_Completed_orders
            // 
            this.grd_All_Tax_Completed_orders.AllowUserToAddRows = false;
            this.grd_All_Tax_Completed_orders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_All_Tax_Completed_orders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_All_Tax_Completed_orders.ColumnHeadersHeight = 35;
            this.grd_All_Tax_Completed_orders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column6,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column4,
            this.Column11,
            this.Column5,
            this.Column13,
            this.Column14,
            this.Column12,
            this.Column7});
            this.grd_All_Tax_Completed_orders.Location = new System.Drawing.Point(12, 53);
            this.grd_All_Tax_Completed_orders.Name = "grd_All_Tax_Completed_orders";
            this.grd_All_Tax_Completed_orders.RowHeadersVisible = false;
            this.grd_All_Tax_Completed_orders.Size = new System.Drawing.Size(1176, 436);
            this.grd_All_Tax_Completed_orders.TabIndex = 2;
            this.grd_All_Tax_Completed_orders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_All_Tax_Completed_orders_CellContentClick);
            // 
            // btn_Send_All
            // 
            this.btn_Send_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Send_All.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Send_All.Location = new System.Drawing.Point(1086, 8);
            this.btn_Send_All.Name = "btn_Send_All";
            this.btn_Send_All.Size = new System.Drawing.Size(102, 39);
            this.btn_Send_All.TabIndex = 1;
            this.btn_Send_All.Text = "Send All";
            this.btn_Send_All.UseVisualStyleBackColor = true;
            this.btn_Send_All.Click += new System.EventHandler(this.btn_Send_All_Click);
            // 
            // chk_All
            // 
            this.chk_All.AutoSize = true;
            this.chk_All.Location = new System.Drawing.Point(12, 18);
            this.chk_All.Name = "chk_All";
            this.chk_All.Size = new System.Drawing.Size(72, 21);
            this.chk_All.TabIndex = 0;
            this.chk_All.Text = "Check All";
            this.chk_All.UseVisualStyleBackColor = true;
            this.chk_All.CheckedChanged += new System.EventHandler(this.chk_All_CheckedChanged);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 31.5096F;
            this.Column1.HeaderText = "Chk";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 42.02385F;
            this.Column2.HeaderText = "S.No";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 157.2442F;
            this.Column3.HeaderText = "ORDER NUMBER";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 100.8307F;
            this.Column6.HeaderText = "ORDER TYPE";
            this.Column6.Name = "Column6";
            // 
            // Column8
            // 
            this.Column8.FillWeight = 167.219F;
            this.Column8.HeaderText = "BORROWER NAME";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.FillWeight = 100.8307F;
            this.Column9.HeaderText = "ADDRESS";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.FillWeight = 112.6826F;
            this.Column10.HeaderText = "RECEIVED DATE";
            this.Column10.Name = "Column10";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 100.8307F;
            this.Column4.HeaderText = "STATE & COUNTY";
            this.Column4.Name = "Column4";
            // 
            // Column11
            // 
            this.Column11.FillWeight = 100.8307F;
            this.Column11.HeaderText = "APN";
            this.Column11.Name = "Column11";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "SENDING DATE";
            this.Column5.Name = "Column5";
            // 
            // Column13
            // 
            this.Column13.FillWeight = 32.34114F;
            this.Column13.HeaderText = "PDF";
            this.Column13.Image = global::Ordermanagement_01.Properties.Resources.PDF;
            this.Column13.Name = "Column13";
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column13.Visible = false;
            // 
            // Column14
            // 
            this.Column14.FillWeight = 40.05685F;
            this.Column14.HeaderText = "EMAIL";
            this.Column14.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.Column14.Name = "Column14";
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Order_Id";
            this.Column12.Name = "Column12";
            this.Column12.Visible = false;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Email_Id";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            // 
            // Tax_Completed_Mail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(242)))), ((int)(((byte)(237)))));
            this.ClientSize = new System.Drawing.Size(1202, 626);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Tax_Completed_Mail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tax_Completed_Mail";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Tax_Completed_Mail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_All_Tax_Completed_orders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.RadioButton rbtn_TaxInvoice_Sended;
        private System.Windows.Forms.RadioButton rbtn_TaxInvoice_NotSended;
        private System.Windows.Forms.TextBox txt_Search_Order_no;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chk_All;
        private System.Windows.Forms.DataGridView grd_All_Tax_Completed_orders;
        private System.Windows.Forms.Button btn_Send_All;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblRecordsStatus;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_Total_Orders;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewImageColumn Column13;
        private System.Windows.Forms.DataGridViewImageColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}