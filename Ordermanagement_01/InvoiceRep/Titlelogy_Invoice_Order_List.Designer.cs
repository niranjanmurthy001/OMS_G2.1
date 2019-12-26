namespace Ordermanagement_01.InvoiceRep
{
    partial class Titlelogy_Invoice_Order_List
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btn_Complete = new System.Windows.Forms.Button();
            this.check_All = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblRecordsStatus = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_Total_Orders = new System.Windows.Forms.Label();
            this.cbo_colmn = new System.Windows.Forms.ComboBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.rbtn_Invoice_Sended = new System.Windows.Forms.RadioButton();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.Column20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Number = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATECOUNTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btn_New_Invoice = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtn_Invoice_NotSended = new System.Windows.Forms.RadioButton();
            this.txt_orderserach_Number = new System.Windows.Forms.TextBox();
            this.btn_Send_All = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 53.59796F;
            this.dataGridViewImageColumn1.HeaderText = "VIEW";
            this.dataGridViewImageColumn1.Image = global::Ordermanagement_01.Properties.Resources.Preview;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ToolTipText = "View";
            this.dataGridViewImageColumn1.Width = 39;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.FillWeight = 87.36401F;
            this.dataGridViewImageColumn2.HeaderText = "PDF";
            this.dataGridViewImageColumn2.Image = global::Ordermanagement_01.Properties.Resources.PDF;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 64;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.FillWeight = 87.36401F;
            this.dataGridViewImageColumn3.HeaderText = "EMAIL";
            this.dataGridViewImageColumn3.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn3.Width = 65;
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.HeaderText = "DELETE";
            this.dataGridViewImageColumn4.Image = global::Ordermanagement_01.Properties.Resources.Delete;
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.Width = 65;
            // 
            // btn_Complete
            // 
            this.btn_Complete.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Complete.Location = new System.Drawing.Point(791, 66);
            this.btn_Complete.Name = "btn_Complete";
            this.btn_Complete.Size = new System.Drawing.Size(152, 33);
            this.btn_Complete.TabIndex = 213;
            this.btn_Complete.Text = "Click to Complete";
            this.btn_Complete.UseVisualStyleBackColor = true;
            // 
            // check_All
            // 
            this.check_All.AutoSize = true;
            this.check_All.Location = new System.Drawing.Point(12, 103);
            this.check_All.Name = "check_All";
            this.check_All.Size = new System.Drawing.Size(71, 17);
            this.check_All.TabIndex = 212;
            this.check_All.Text = "Check All";
            this.check_All.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnFirst);
            this.panel1.Controls.Add(this.lblRecordsStatus);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lbl_Total_Orders);
            this.panel1.Location = new System.Drawing.Point(8, 516);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1389, 38);
            this.panel1.TabIndex = 211;
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(250, 6);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 24);
            this.btnFirst.TabIndex = 16;
            this.btnFirst.Text = "|< First";
            this.btnFirst.UseVisualStyleBackColor = false;
            // 
            // lblRecordsStatus
            // 
            this.lblRecordsStatus.AutoSize = true;
            this.lblRecordsStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecordsStatus.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lblRecordsStatus.Location = new System.Drawing.Point(585, 9);
            this.lblRecordsStatus.Name = "lblRecordsStatus";
            this.lblRecordsStatus.Size = new System.Drawing.Size(38, 22);
            this.lblRecordsStatus.TabIndex = 15;
            this.lblRecordsStatus.Text = "0 / 0";
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrevious.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Location = new System.Drawing.Point(395, 6);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 24);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.Text = "< Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(871, 7);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 24);
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = "Last >|";
            this.btnLast.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(721, 7);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 24);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1133, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 19);
            this.label8.TabIndex = 25;
            this.label8.Text = "Total Orders:";
            // 
            // lbl_Total_Orders
            // 
            this.lbl_Total_Orders.AutoSize = true;
            this.lbl_Total_Orders.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total_Orders.ForeColor = System.Drawing.Color.Red;
            this.lbl_Total_Orders.Location = new System.Drawing.Point(1220, 10);
            this.lbl_Total_Orders.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Total_Orders.Name = "lbl_Total_Orders";
            this.lbl_Total_Orders.Size = new System.Drawing.Size(14, 17);
            this.lbl_Total_Orders.TabIndex = 26;
            this.lbl_Total_Orders.Text = "T";
            // 
            // cbo_colmn
            // 
            this.cbo_colmn.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_colmn.FormattingEnabled = true;
            this.cbo_colmn.Items.AddRange(new object[] {
            "Order Number",
            "Invoice Number",
            "Invoice Date",
            "Client",
            "Sub Client",
            "Received Date",
            "Order Type"});
            this.cbo_colmn.Location = new System.Drawing.Point(98, 71);
            this.cbo_colmn.Name = "cbo_colmn";
            this.cbo_colmn.Size = new System.Drawing.Size(222, 28);
            this.cbo_colmn.TabIndex = 205;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(8, 35);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(40, 30);
            this.btn_Refresh.TabIndex = 208;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            // 
            // rbtn_Invoice_Sended
            // 
            this.rbtn_Invoice_Sended.AutoSize = true;
            this.rbtn_Invoice_Sended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_Sended.Location = new System.Drawing.Point(633, 35);
            this.rbtn_Invoice_Sended.Name = "rbtn_Invoice_Sended";
            this.rbtn_Invoice_Sended.Size = new System.Drawing.Size(104, 20);
            this.rbtn_Invoice_Sended.TabIndex = 210;
            this.rbtn_Invoice_Sended.Text = "Invoice Sent";
            this.rbtn_Invoice_Sended.UseVisualStyleBackColor = true;
            // 
            // grd_order
            // 
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.AllowUserToResizeRows = false;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_order.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_order.ColumnHeadersHeight = 30;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column20,
            this.SNo,
            this.Order_Number,
            this.Column4,
            this.Client_Name,
            this.Sub_ProcessName,
            this.Order_Type,
            this.STATECOUNTY,
            this.Date,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column15,
            this.Column13,
            this.Column9,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column21,
            this.Column22});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle3;
            this.grd_order.Location = new System.Drawing.Point(8, 130);
            this.grd_order.Name = "grd_order";
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.Size = new System.Drawing.Size(1309, 388);
            this.grd_order.TabIndex = 203;
            // 
            // Column20
            // 
            this.Column20.FillWeight = 302.5341F;
            this.Column20.HeaderText = "Chk";
            this.Column20.Name = "Column20";
            this.Column20.Width = 223;
            // 
            // SNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.SNo.FillWeight = 79.82497F;
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            this.SNo.Width = 59;
            // 
            // Order_Number
            // 
            this.Order_Number.FillWeight = 134.2747F;
            this.Order_Number.HeaderText = "ORDER NUMBER";
            this.Order_Number.Name = "Order_Number";
            this.Order_Number.ReadOnly = true;
            this.Order_Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Order_Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Order_Number.Width = 99;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 87.36401F;
            this.Column4.HeaderText = "INVOICE";
            this.Column4.Name = "Column4";
            this.Column4.Width = 64;
            // 
            // Client_Name
            // 
            this.Client_Name.FillWeight = 53.59796F;
            this.Client_Name.HeaderText = "CLIENT";
            this.Client_Name.Name = "Client_Name";
            this.Client_Name.ReadOnly = true;
            this.Client_Name.Width = 39;
            // 
            // Sub_ProcessName
            // 
            this.Sub_ProcessName.FillWeight = 109.1479F;
            this.Sub_ProcessName.HeaderText = "SUB CLIENT";
            this.Sub_ProcessName.Name = "Sub_ProcessName";
            this.Sub_ProcessName.ReadOnly = true;
            this.Sub_ProcessName.Width = 80;
            // 
            // Order_Type
            // 
            this.Order_Type.FillWeight = 113.8334F;
            this.Order_Type.HeaderText = "ORDER TYPE";
            this.Order_Type.Name = "Order_Type";
            this.Order_Type.ReadOnly = true;
            this.Order_Type.Width = 83;
            // 
            // STATECOUNTY
            // 
            this.STATECOUNTY.FillWeight = 128.6095F;
            this.STATECOUNTY.HeaderText = "STATE & COUNTY";
            this.STATECOUNTY.Name = "STATECOUNTY";
            this.STATECOUNTY.ReadOnly = true;
            this.STATECOUNTY.Width = 95;
            // 
            // Date
            // 
            this.Date.FillWeight = 115.8661F;
            this.Date.HeaderText = "RECEIVED DATE";
            this.Date.Name = "Date";
            this.Date.Width = 85;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 87.36401F;
            this.Column1.HeaderText = "SEARCH COST";
            this.Column1.Name = "Column1";
            this.Column1.Width = 64;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 87.36401F;
            this.Column2.HeaderText = "COPY COST";
            this.Column2.Name = "Column2";
            this.Column2.Width = 64;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 87.36401F;
            this.Column3.HeaderText = "TOTAL";
            this.Column3.Name = "Column3";
            this.Column3.Width = 65;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 87.36401F;
            this.Column7.HeaderText = "INVOICE_DATE";
            this.Column7.Name = "Column7";
            this.Column7.Width = 65;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Column15";
            this.Column15.Name = "Column15";
            this.Column15.Visible = false;
            // 
            // Column13
            // 
            this.Column13.FillWeight = 53.59796F;
            this.Column13.HeaderText = "VIEW";
            this.Column13.Image = global::Ordermanagement_01.Properties.Resources.Preview;
            this.Column13.Name = "Column13";
            this.Column13.ToolTipText = "View";
            this.Column13.Width = 39;
            // 
            // Column9
            // 
            this.Column9.FillWeight = 87.36401F;
            this.Column9.HeaderText = "PDF";
            this.Column9.Image = global::Ordermanagement_01.Properties.Resources.PDF;
            this.Column9.Name = "Column9";
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column9.Width = 64;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 87.36401F;
            this.Column5.HeaderText = "EMAIL";
            this.Column5.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column5.Width = 65;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Client_Id";
            this.Column21.Name = "Column21";
            this.Column21.Visible = false;
            // 
            // Column22
            // 
            this.Column22.HeaderText = "DELETE";
            this.Column22.Image = global::Ordermanagement_01.Properties.Resources.Delete;
            this.Column22.Name = "Column22";
            this.Column22.Width = 65;
            // 
            // btn_New_Invoice
            // 
            this.btn_New_Invoice.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_New_Invoice.Location = new System.Drawing.Point(633, 67);
            this.btn_New_Invoice.Name = "btn_New_Invoice";
            this.btn_New_Invoice.Size = new System.Drawing.Size(152, 33);
            this.btn_New_Invoice.TabIndex = 207;
            this.btn_New_Invoice.Text = "New Invoice";
            this.btn_New_Invoice.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 204;
            this.label2.Text = "Search By:\r\n";
            // 
            // rbtn_Invoice_NotSended
            // 
            this.rbtn_Invoice_NotSended.AutoSize = true;
            this.rbtn_Invoice_NotSended.Checked = true;
            this.rbtn_Invoice_NotSended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_NotSended.Location = new System.Drawing.Point(497, 35);
            this.rbtn_Invoice_NotSended.Name = "rbtn_Invoice_NotSended";
            this.rbtn_Invoice_NotSended.Size = new System.Drawing.Size(130, 20);
            this.rbtn_Invoice_NotSended.TabIndex = 209;
            this.rbtn_Invoice_NotSended.TabStop = true;
            this.rbtn_Invoice_NotSended.Text = "Invoice Not Sent";
            this.rbtn_Invoice_NotSended.UseVisualStyleBackColor = true;
            // 
            // txt_orderserach_Number
            // 
            this.txt_orderserach_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_orderserach_Number.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_orderserach_Number.Location = new System.Drawing.Point(326, 71);
            this.txt_orderserach_Number.Multiline = true;
            this.txt_orderserach_Number.Name = "txt_orderserach_Number";
            this.txt_orderserach_Number.Size = new System.Drawing.Size(301, 27);
            this.txt_orderserach_Number.TabIndex = 206;
            // 
            // btn_Send_All
            // 
            this.btn_Send_All.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Send_All.Location = new System.Drawing.Point(594, 560);
            this.btn_Send_All.Name = "btn_Send_All";
            this.btn_Send_All.Size = new System.Drawing.Size(152, 33);
            this.btn_Send_All.TabIndex = 214;
            this.btn_Send_All.Text = "Send";
            this.btn_Send_All.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.MediumBlue;
            this.label25.Location = new System.Drawing.Point(465, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(340, 31);
            this.label25.TabIndex = 215;
            this.label25.Text = "TITLELOGY INVOICE ORDER DETAILS";
            this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Titlelogy_Invoice_Order_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 607);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.btn_Send_All);
            this.Controls.Add(this.btn_Complete);
            this.Controls.Add(this.check_All);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbo_colmn);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.rbtn_Invoice_Sended);
            this.Controls.Add(this.grd_order);
            this.Controls.Add(this.btn_New_Invoice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbtn_Invoice_NotSended);
            this.Controls.Add(this.txt_orderserach_Number);
            this.Name = "Titlelogy_Invoice_Order_List";
            this.Text = "Titlelogy_Invoice_Order_List";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.Button btn_Complete;
        private System.Windows.Forms.CheckBox check_All;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblRecordsStatus;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_Total_Orders;
        private System.Windows.Forms.ComboBox cbo_colmn;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.RadioButton rbtn_Invoice_Sended;
        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewButtonColumn Order_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATECOUNTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewImageColumn Column13;
        private System.Windows.Forms.DataGridViewImageColumn Column9;
        private System.Windows.Forms.DataGridViewImageColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewImageColumn Column22;
        private System.Windows.Forms.Button btn_New_Invoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtn_Invoice_NotSended;
        private System.Windows.Forms.TextBox txt_orderserach_Number;
        private System.Windows.Forms.Button btn_Send_All;
        private System.Windows.Forms.Label label25;

    }
}