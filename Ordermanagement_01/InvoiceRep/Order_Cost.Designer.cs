namespace Ordermanagement_01.InvoiceRep
{
    partial class Order_Cost
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Number = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Client_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATECOUNTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbo_colmn = new System.Windows.Forms.ComboBox();
            this.btn_New_Invoice = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_orderserach_Number = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.label25 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_First = new System.Windows.Forms.Button();
            this.lbl_Record_status = new System.Windows.Forms.Label();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.btn_Last = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_Total_orders = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.rbtn_Invoice_NotSended = new System.Windows.Forms.RadioButton();
            this.rbtn_Invoice_Sended = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grd_order
            // 
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.AllowUserToResizeRows = false;
            this.grd_order.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_order.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grd_order.ColumnHeadersHeight = 30;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.SNo,
            this.Order_Number,
            this.Client_Name,
            this.Sub_ProcessName,
            this.Order_Type,
            this.STATECOUNTY,
            this.Date,
            this.Column1,
            this.Column7,
            this.Column9,
            this.Column5,
            this.Column15,
            this.Column6,
            this.Column8});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle9;
            this.grd_order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_order.Location = new System.Drawing.Point(3, 139);
            this.grd_order.Name = "grd_order";
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.Size = new System.Drawing.Size(1218, 350);
            this.grd_order.TabIndex = 192;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            // 
            // Check
            // 
            this.Check.HeaderText = "Chk";
            this.Check.Name = "Check";
            // 
            // SNo
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SNo.DefaultCellStyle = dataGridViewCellStyle8;
            this.SNo.FillWeight = 91.37055F;
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            // 
            // Order_Number
            // 
            this.Order_Number.FillWeight = 153.6957F;
            this.Order_Number.HeaderText = "ORDER NUMBER";
            this.Order_Number.Name = "Order_Number";
            this.Order_Number.ReadOnly = true;
            this.Order_Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Order_Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Client_Name
            // 
            this.Client_Name.FillWeight = 61.35016F;
            this.Client_Name.HeaderText = "CLIENT";
            this.Client_Name.Name = "Client_Name";
            this.Client_Name.ReadOnly = true;
            // 
            // Sub_ProcessName
            // 
            this.Sub_ProcessName.FillWeight = 124.9346F;
            this.Sub_ProcessName.HeaderText = "SUB CLIENT";
            this.Sub_ProcessName.Name = "Sub_ProcessName";
            this.Sub_ProcessName.ReadOnly = true;
            // 
            // Order_Type
            // 
            this.Order_Type.FillWeight = 130.2978F;
            this.Order_Type.HeaderText = "ORDER TYPE";
            this.Order_Type.Name = "Order_Type";
            this.Order_Type.ReadOnly = true;
            // 
            // STATECOUNTY
            // 
            this.STATECOUNTY.FillWeight = 147.2111F;
            this.STATECOUNTY.HeaderText = "STATE & COUNTY";
            this.STATECOUNTY.Name = "STATECOUNTY";
            this.STATECOUNTY.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.FillWeight = 132.6245F;
            this.Date.HeaderText = "RECEIVED DATE";
            this.Date.Name = "Date";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ORDER COST";
            this.Column1.Name = "Column1";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "GEN_DATE";
            this.Column7.Name = "Column7";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "PDF";
            this.Column9.Image = global::Ordermanagement_01.Properties.Resources.PDF;
            this.Column9.Name = "Column9";
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "EMAIL";
            this.Column5.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Column15";
            this.Column15.Name = "Column15";
            this.Column15.Visible = false;
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
            // cbo_colmn
            // 
            this.cbo_colmn.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_colmn.FormattingEnabled = true;
            this.cbo_colmn.Items.AddRange(new object[] {
            "Order Number",
            "GEN_Date",
            "Client",
            "Sub Client",
            "Received Date",
            "Order Type"});
            this.cbo_colmn.Location = new System.Drawing.Point(319, 8);
            this.cbo_colmn.Name = "cbo_colmn";
            this.cbo_colmn.Size = new System.Drawing.Size(255, 28);
            this.cbo_colmn.TabIndex = 198;
            // 
            // btn_New_Invoice
            // 
            this.btn_New_Invoice.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_New_Invoice.Location = new System.Drawing.Point(884, 7);
            this.btn_New_Invoice.Name = "btn_New_Invoice";
            this.btn_New_Invoice.Size = new System.Drawing.Size(152, 32);
            this.btn_New_Invoice.TabIndex = 200;
            this.btn_New_Invoice.Text = "New Order Cost";
            this.btn_New_Invoice.UseVisualStyleBackColor = true;
            this.btn_New_Invoice.Click += new System.EventHandler(this.btn_New_Invoice_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(242, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 197;
            this.label2.Text = "Search By:\r\n";
            // 
            // txt_orderserach_Number
            // 
            this.txt_orderserach_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_orderserach_Number.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_orderserach_Number.Location = new System.Drawing.Point(589, 9);
            this.txt_orderserach_Number.Multiline = true;
            this.txt_orderserach_Number.Name = "txt_orderserach_Number";
            this.txt_orderserach_Number.Size = new System.Drawing.Size(279, 27);
            this.txt_orderserach_Number.TabIndex = 199;
            this.txt_orderserach_Number.TextChanged += new System.EventHandler(this.txt_orderserach_Number_TextChanged);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "PDF";
            this.dataGridViewImageColumn1.Image = global::Ordermanagement_01.Properties.Resources.PDF;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 97;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "EMAIL";
            this.dataGridViewImageColumn2.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 97;
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.MediumBlue;
            this.label25.Location = new System.Drawing.Point(526, 6);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(214, 31);
            this.label25.TabIndex = 202;
            this.label25.Text = "ORDER COST  DETAILS";
            this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label25.Click += new System.EventHandler(this.label25_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Linen;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btn_First);
            this.panel2.Controls.Add(this.lbl_Record_status);
            this.panel2.Controls.Add(this.btn_Previous);
            this.panel2.Controls.Add(this.btn_Last);
            this.panel2.Controls.Add(this.btn_Next);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lbl_Total_orders);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 495);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1218, 40);
            this.panel2.TabIndex = 205;
            // 
            // btn_First
            // 
            this.btn_First.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_First.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_First.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_First.Location = new System.Drawing.Point(250, 6);
            this.btn_First.Name = "btn_First";
            this.btn_First.Size = new System.Drawing.Size(75, 24);
            this.btn_First.TabIndex = 16;
            this.btn_First.Text = "|< First";
            this.btn_First.UseVisualStyleBackColor = false;
            this.btn_First.Click += new System.EventHandler(this.btn_First_Click);
            // 
            // lbl_Record_status
            // 
            this.lbl_Record_status.AutoSize = true;
            this.lbl_Record_status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Record_status.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_Record_status.Location = new System.Drawing.Point(585, 9);
            this.lbl_Record_status.Name = "lbl_Record_status";
            this.lbl_Record_status.Size = new System.Drawing.Size(38, 22);
            this.lbl_Record_status.TabIndex = 15;
            this.lbl_Record_status.Text = "0 / 0";
            // 
            // btn_Previous
            // 
            this.btn_Previous.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_Previous.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Previous.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Previous.Location = new System.Drawing.Point(395, 6);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(75, 24);
            this.btn_Previous.TabIndex = 13;
            this.btn_Previous.Text = "< Previous";
            this.btn_Previous.UseVisualStyleBackColor = false;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // btn_Last
            // 
            this.btn_Last.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_Last.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Last.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Last.Location = new System.Drawing.Point(871, 7);
            this.btn_Last.Name = "btn_Last";
            this.btn_Last.Size = new System.Drawing.Size(75, 24);
            this.btn_Last.TabIndex = 14;
            this.btn_Last.Text = "Last >|";
            this.btn_Last.UseVisualStyleBackColor = false;
            this.btn_Last.Click += new System.EventHandler(this.btn_Last_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Next.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Next.Location = new System.Drawing.Point(721, 7);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(75, 24);
            this.btn_Next.TabIndex = 11;
            this.btn_Next.Text = "Next >";
            this.btn_Next.UseVisualStyleBackColor = false;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(994, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 19);
            this.label5.TabIndex = 25;
            this.label5.Text = "Total Orders:";
            // 
            // lbl_Total_orders
            // 
            this.lbl_Total_orders.AutoSize = true;
            this.lbl_Total_orders.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total_orders.ForeColor = System.Drawing.Color.Red;
            this.lbl_Total_orders.Location = new System.Drawing.Point(1079, 11);
            this.lbl_Total_orders.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Total_orders.Name = "lbl_Total_orders";
            this.lbl_Total_orders.Size = new System.Drawing.Size(14, 17);
            this.lbl_Total_orders.TabIndex = 26;
            this.lbl_Total_orders.Text = "T";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(546, 543);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(132, 29);
            this.btnSend.TabIndex = 206;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.grd_order, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnSend, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1224, 578);
            this.tableLayoutPanel3.TabIndex = 209;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label25);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1218, 34);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_Refresh);
            this.panel3.Controls.Add(this.rbtn_Invoice_NotSended);
            this.panel3.Controls.Add(this.rbtn_Invoice_Sended);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1218, 40);
            this.panel3.TabIndex = 1;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(3, 3);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(40, 38);
            this.btn_Refresh.TabIndex = 201;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // rbtn_Invoice_NotSended
            // 
            this.rbtn_Invoice_NotSended.AutoSize = true;
            this.rbtn_Invoice_NotSended.Checked = true;
            this.rbtn_Invoice_NotSended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_NotSended.Location = new System.Drawing.Point(516, 10);
            this.rbtn_Invoice_NotSended.Name = "rbtn_Invoice_NotSended";
            this.rbtn_Invoice_NotSended.Size = new System.Drawing.Size(119, 20);
            this.rbtn_Invoice_NotSended.TabIndex = 203;
            this.rbtn_Invoice_NotSended.TabStop = true;
            this.rbtn_Invoice_NotSended.Text = "Email Not Sent";
            this.rbtn_Invoice_NotSended.UseVisualStyleBackColor = true;
            this.rbtn_Invoice_NotSended.CheckedChanged += new System.EventHandler(this.rbtn_Invoice_NotSended_CheckedChanged);
            // 
            // rbtn_Invoice_Sended
            // 
            this.rbtn_Invoice_Sended.AutoSize = true;
            this.rbtn_Invoice_Sended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_Sended.Location = new System.Drawing.Point(677, 10);
            this.rbtn_Invoice_Sended.Name = "rbtn_Invoice_Sended";
            this.rbtn_Invoice_Sended.Size = new System.Drawing.Size(93, 20);
            this.rbtn_Invoice_Sended.TabIndex = 204;
            this.rbtn_Invoice_Sended.Text = "Email Sent";
            this.rbtn_Invoice_Sended.UseVisualStyleBackColor = true;
            this.rbtn_Invoice_Sended.CheckedChanged += new System.EventHandler(this.rbtn_Invoice_Sended_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_New_Invoice);
            this.panel4.Controls.Add(this.txt_orderserach_Number);
            this.panel4.Controls.Add(this.cbo_colmn);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 89);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1218, 44);
            this.panel4.TabIndex = 2;
            // 
            // Order_Cost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 578);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "Order_Cost";
            this.Text = "Order_Cost";
            this.Load += new System.EventHandler(this.Order_Cost_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.ComboBox cbo_colmn;
        private System.Windows.Forms.Button btn_New_Invoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_orderserach_Number;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_First;
        private System.Windows.Forms.Label lbl_Record_status;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Button btn_Last;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_Total_orders;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewButtonColumn Order_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATECOUNTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewImageColumn Column9;
        private System.Windows.Forms.DataGridViewImageColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.RadioButton rbtn_Invoice_NotSended;
        private System.Windows.Forms.RadioButton rbtn_Invoice_Sended;
        private System.Windows.Forms.Panel panel4;
    }
}