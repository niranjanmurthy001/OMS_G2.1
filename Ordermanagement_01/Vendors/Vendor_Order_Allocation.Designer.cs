namespace Ordermanagement_01.Vendors
{
    partial class Vendor_Order_Allocation
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
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Order_Number = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Order_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATECOUNTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.btn_Allocate = new System.Windows.Forms.Button();
            this.grd_order_Allocated = new System.Windows.Forms.DataGridView();
            this.lbl_allocated_user = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.txt_SearchOrdernumber = new System.Windows.Forms.TextBox();
            this.btn_move_to_inhouse = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.ddl_Vendor_Name = new System.Windows.Forms.ComboBox();
            this.lbl_Vendor_Name = new System.Windows.Forms.Label();
            this.btn_Reallocate = new System.Windows.Forms.Button();
            this.btn_Rallocate_Move_To_Inhouse = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.grid_Export = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.ddl_Username = new System.Windows.Forms.ComboBox();
            this.btn_user_Reallocate = new System.Windows.Forms.Button();
            this.btn_Vendor_Export = new System.Windows.Forms.Button();
            this.grid_Vendor_Order_Export = new System.Windows.Forms.DataGridView();
            this.Chk_Allocate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SNo_allocate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_Name_All = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordertype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order_Allocated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Export)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Vendor_Order_Export)).BeginInit();
            this.SuspendLayout();
            // 
            // grd_order
            // 
            this.grd_order.AllowDrop = true;
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_order.ColumnHeadersHeight = 30;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Chk,
            this.SNo,
            this.Client_Name,
            this.Sub_ProcessName,
            this.Column6,
            this.Order_Number,
            this.Order_Type,
            this.STATECOUNTY,
            this.Date,
            this.Column11,
            this.Column13,
            this.Order_ID,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column19,
            this.Column20,
            this.Column21});
            this.grd_order.Location = new System.Drawing.Point(228, 56);
            this.grd_order.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grd_order.Name = "grd_order";
            this.grd_order.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grd_order.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_order.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.grd_order.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_order.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_order.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_order.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.RowTemplate.Height = 25;
            this.grd_order.Size = new System.Drawing.Size(1044, 292);
            this.grd_order.TabIndex = 121;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            // 
            // Chk
            // 
            this.Chk.HeaderText = "Chk";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Chk.Width = 50;
            // 
            // SNo
            // 
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            this.SNo.Width = 50;
            // 
            // Client_Name
            // 
            this.Client_Name.HeaderText = "CLIENT";
            this.Client_Name.Name = "Client_Name";
            this.Client_Name.ReadOnly = true;
            this.Client_Name.Width = 110;
            // 
            // Sub_ProcessName
            // 
            this.Sub_ProcessName.HeaderText = "SUB CLIENT";
            this.Sub_ProcessName.Name = "Sub_ProcessName";
            this.Sub_ProcessName.ReadOnly = true;
            this.Sub_ProcessName.Width = 111;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "DRN ORDER.NO";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Order_Number
            // 
            this.Order_Number.HeaderText = "ORDER NUMBER";
            this.Order_Number.Name = "Order_Number";
            this.Order_Number.ReadOnly = true;
            this.Order_Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Order_Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Order_Number.Width = 150;
            // 
            // Order_Type
            // 
            this.Order_Type.HeaderText = "ORDER TYPE";
            this.Order_Type.Name = "Order_Type";
            this.Order_Type.ReadOnly = true;
            this.Order_Type.Width = 111;
            // 
            // STATECOUNTY
            // 
            this.STATECOUNTY.HeaderText = "STATE & COUNTY";
            this.STATECOUNTY.Name = "STATECOUNTY";
            this.STATECOUNTY.ReadOnly = true;
            this.STATECOUNTY.Width = 110;
            // 
            // Date
            // 
            this.Date.HeaderText = "RECEIVED DATE";
            this.Date.Name = "Date";
            this.Date.Width = 110;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "ASSIGNED_DATE";
            this.Column11.Name = "Column11";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "VENDOR NAME";
            this.Column13.Name = "Column13";
            // 
            // Order_ID
            // 
            this.Order_ID.HeaderText = "Order_ID";
            this.Order_ID.Name = "Order_ID";
            this.Order_ID.Visible = false;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.Name = "Column9";
            this.Column9.Visible = false;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Name = "Column10";
            this.Column10.Visible = false;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Client_Id";
            this.Column19.Name = "Column19";
            this.Column19.Visible = false;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Sub_Client_Id";
            this.Column20.Name = "Column20";
            this.Column20.Visible = false;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Order_Type_Id";
            this.Column21.Name = "Column21";
            this.Column21.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.GhostWhite;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 24);
            this.label1.TabIndex = 120;
            this.label1.Text = "USER NAME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TreeView1
            // 
            this.TreeView1.AllowDrop = true;
            this.TreeView1.BackColor = System.Drawing.SystemColors.Info;
            this.TreeView1.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TreeView1.Indent = 15;
            this.TreeView1.ItemHeight = 20;
            this.TreeView1.Location = new System.Drawing.Point(3, 35);
            this.TreeView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(210, 315);
            this.TreeView1.TabIndex = 119;
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // btn_Allocate
            // 
            this.btn_Allocate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Allocate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Allocate.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Allocate.ForeColor = System.Drawing.Color.White;
            this.btn_Allocate.Location = new System.Drawing.Point(379, 349);
            this.btn_Allocate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Allocate.Name = "btn_Allocate";
            this.btn_Allocate.Size = new System.Drawing.Size(136, 32);
            this.btn_Allocate.TabIndex = 122;
            this.btn_Allocate.Text = "Allocate";
            this.btn_Allocate.UseVisualStyleBackColor = false;
            this.btn_Allocate.Click += new System.EventHandler(this.btn_Allocate_Click);
            // 
            // grd_order_Allocated
            // 
            this.grd_order_Allocated.AllowUserToAddRows = false;
            this.grd_order_Allocated.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grd_order_Allocated.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order_Allocated.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grd_order_Allocated.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order_Allocated.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grd_order_Allocated.ColumnHeadersHeight = 30;
            this.grd_order_Allocated.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Chk_Allocate,
            this.SNo_allocate,
            this.Client_Name_All,
            this.Column1,
            this.Column12,
            this.Column2,
            this.Column3,
            this.ordertype,
            this.Column4,
            this.Column14,
            this.Column15,
            this.Column22,
            this.orderid,
            this.Status_Id,
            this.Column5,
            this.Column16,
            this.Column17,
            this.Column18});
            this.grd_order_Allocated.Location = new System.Drawing.Point(3, 387);
            this.grd_order_Allocated.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.grd_order_Allocated.Name = "grd_order_Allocated";
            this.grd_order_Allocated.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order_Allocated.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grd_order_Allocated.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_order_Allocated.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.grd_order_Allocated.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_order_Allocated.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_order_Allocated.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_order_Allocated.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order_Allocated.RowTemplate.Height = 25;
            this.grd_order_Allocated.Size = new System.Drawing.Size(1277, 177);
            this.grd_order_Allocated.TabIndex = 124;
            this.grd_order_Allocated.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_Allocated_CellClick);
            this.grd_order_Allocated.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_Allocated_CellContentClick);
            // 
            // lbl_allocated_user
            // 
            this.lbl_allocated_user.AutoSize = true;
            this.lbl_allocated_user.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbl_allocated_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_allocated_user.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_allocated_user.ForeColor = System.Drawing.Color.Black;
            this.lbl_allocated_user.Location = new System.Drawing.Point(96, 363);
            this.lbl_allocated_user.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_allocated_user.Name = "lbl_allocated_user";
            this.lbl_allocated_user.Size = new System.Drawing.Size(44, 21);
            this.lbl_allocated_user.TabIndex = 132;
            this.lbl_allocated_user.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 362);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 22);
            this.label4.TabIndex = 131;
            this.label4.Text = "Allocate To :";
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Header.Location = new System.Drawing.Point(554, 5);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(349, 31);
            this.lbl_Header.TabIndex = 134;
            this.lbl_Header.Text = "VENDOR ORDER ALLOCATION QUEUE";
            this.lbl_Header.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_SearchOrdernumber
            // 
            this.txt_SearchOrdernumber.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SearchOrdernumber.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.txt_SearchOrdernumber.Location = new System.Drawing.Point(1048, 8);
            this.txt_SearchOrdernumber.Name = "txt_SearchOrdernumber";
            this.txt_SearchOrdernumber.Size = new System.Drawing.Size(224, 28);
            this.txt_SearchOrdernumber.TabIndex = 149;
            this.txt_SearchOrdernumber.Text = "Search by order number...";
            this.txt_SearchOrdernumber.Click += new System.EventHandler(this.txt_SearchOrdernumber_Click);
            this.txt_SearchOrdernumber.TextChanged += new System.EventHandler(this.txt_SearchOrdernumber_TextChanged);
            // 
            // btn_move_to_inhouse
            // 
            this.btn_move_to_inhouse.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_move_to_inhouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_move_to_inhouse.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_move_to_inhouse.ForeColor = System.Drawing.Color.White;
            this.btn_move_to_inhouse.Location = new System.Drawing.Point(519, 349);
            this.btn_move_to_inhouse.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_move_to_inhouse.Name = "btn_move_to_inhouse";
            this.btn_move_to_inhouse.Size = new System.Drawing.Size(136, 32);
            this.btn_move_to_inhouse.TabIndex = 150;
            this.btn_move_to_inhouse.Text = "Move to Inhouse";
            this.btn_move_to_inhouse.UseVisualStyleBackColor = false;
            this.btn_move_to_inhouse.Click += new System.EventHandler(this.btn_move_to_inhouse_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(228, 4);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(39, 28);
            this.btn_Refresh.TabIndex = 151;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // ddl_Vendor_Name
            // 
            this.ddl_Vendor_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Vendor_Name.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Vendor_Name.FormattingEnabled = true;
            this.ddl_Vendor_Name.Location = new System.Drawing.Point(182, 574);
            this.ddl_Vendor_Name.Margin = new System.Windows.Forms.Padding(4);
            this.ddl_Vendor_Name.Name = "ddl_Vendor_Name";
            this.ddl_Vendor_Name.Size = new System.Drawing.Size(239, 28);
            this.ddl_Vendor_Name.TabIndex = 155;
            // 
            // lbl_Vendor_Name
            // 
            this.lbl_Vendor_Name.AutoSize = true;
            this.lbl_Vendor_Name.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Vendor_Name.ForeColor = System.Drawing.Color.Black;
            this.lbl_Vendor_Name.Location = new System.Drawing.Point(92, 578);
            this.lbl_Vendor_Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Vendor_Name.Name = "lbl_Vendor_Name";
            this.lbl_Vendor_Name.Size = new System.Drawing.Size(92, 19);
            this.lbl_Vendor_Name.TabIndex = 152;
            this.lbl_Vendor_Name.Text = "Vendor Name :";
            // 
            // btn_Reallocate
            // 
            this.btn_Reallocate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Reallocate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Reallocate.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reallocate.ForeColor = System.Drawing.Color.White;
            this.btn_Reallocate.Location = new System.Drawing.Point(427, 572);
            this.btn_Reallocate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Reallocate.Name = "btn_Reallocate";
            this.btn_Reallocate.Size = new System.Drawing.Size(136, 32);
            this.btn_Reallocate.TabIndex = 156;
            this.btn_Reallocate.Text = "Re Allocate";
            this.btn_Reallocate.UseVisualStyleBackColor = false;
            this.btn_Reallocate.Click += new System.EventHandler(this.btn_Reallocate_Click);
            // 
            // btn_Rallocate_Move_To_Inhouse
            // 
            this.btn_Rallocate_Move_To_Inhouse.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Rallocate_Move_To_Inhouse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Rallocate_Move_To_Inhouse.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Rallocate_Move_To_Inhouse.ForeColor = System.Drawing.Color.White;
            this.btn_Rallocate_Move_To_Inhouse.Location = new System.Drawing.Point(567, 572);
            this.btn_Rallocate_Move_To_Inhouse.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Rallocate_Move_To_Inhouse.Name = "btn_Rallocate_Move_To_Inhouse";
            this.btn_Rallocate_Move_To_Inhouse.Size = new System.Drawing.Size(136, 32);
            this.btn_Rallocate_Move_To_Inhouse.TabIndex = 157;
            this.btn_Rallocate_Move_To_Inhouse.Text = "Move to Inhouse";
            this.btn_Rallocate_Move_To_Inhouse.UseVisualStyleBackColor = false;
            this.btn_Rallocate_Move_To_Inhouse.Click += new System.EventHandler(this.btn_Rallocate_Move_To_Inhouse_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Location = new System.Drawing.Point(272, 4);
            this.btn_Export.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(84, 32);
            this.btn_Export.TabIndex = 158;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // grid_Export
            // 
            this.grid_Export.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_Export.Location = new System.Drawing.Point(361, 5);
            this.grid_Export.Name = "grid_Export";
            this.grid_Export.Size = new System.Drawing.Size(74, 31);
            this.grid_Export.TabIndex = 159;
            this.grid_Export.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(796, 574);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 160;
            this.label2.Text = "User Name :";
            // 
            // ddl_Username
            // 
            this.ddl_Username.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Username.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Username.FormattingEnabled = true;
            this.ddl_Username.Location = new System.Drawing.Point(888, 570);
            this.ddl_Username.Margin = new System.Windows.Forms.Padding(4);
            this.ddl_Username.Name = "ddl_Username";
            this.ddl_Username.Size = new System.Drawing.Size(239, 28);
            this.ddl_Username.TabIndex = 161;
            // 
            // btn_user_Reallocate
            // 
            this.btn_user_Reallocate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_user_Reallocate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_user_Reallocate.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_user_Reallocate.ForeColor = System.Drawing.Color.White;
            this.btn_user_Reallocate.Location = new System.Drawing.Point(1142, 567);
            this.btn_user_Reallocate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_user_Reallocate.Name = "btn_user_Reallocate";
            this.btn_user_Reallocate.Size = new System.Drawing.Size(136, 32);
            this.btn_user_Reallocate.TabIndex = 162;
            this.btn_user_Reallocate.Text = "Re Allocate";
            this.btn_user_Reallocate.UseVisualStyleBackColor = false;
            this.btn_user_Reallocate.Click += new System.EventHandler(this.btn_user_Reallocate_Click);
            // 
            // btn_Vendor_Export
            // 
            this.btn_Vendor_Export.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Vendor_Export.Location = new System.Drawing.Point(6, 568);
            this.btn_Vendor_Export.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Vendor_Export.Name = "btn_Vendor_Export";
            this.btn_Vendor_Export.Size = new System.Drawing.Size(84, 32);
            this.btn_Vendor_Export.TabIndex = 163;
            this.btn_Vendor_Export.Text = "Export";
            this.btn_Vendor_Export.UseVisualStyleBackColor = true;
            this.btn_Vendor_Export.Click += new System.EventHandler(this.btn_Vendor_Export_Click);
            // 
            // grid_Vendor_Order_Export
            // 
            this.grid_Vendor_Order_Export.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_Vendor_Order_Export.Location = new System.Drawing.Point(441, 5);
            this.grid_Vendor_Order_Export.Name = "grid_Vendor_Order_Export";
            this.grid_Vendor_Order_Export.Size = new System.Drawing.Size(74, 31);
            this.grid_Vendor_Order_Export.TabIndex = 164;
            this.grid_Vendor_Order_Export.Visible = false;
            // 
            // Chk_Allocate
            // 
            this.Chk_Allocate.FillWeight = 159.8985F;
            this.Chk_Allocate.HeaderText = "Chk_Allocate";
            this.Chk_Allocate.Name = "Chk_Allocate";
            this.Chk_Allocate.Width = 50;
            // 
            // SNo_allocate
            // 
            this.SNo_allocate.FillWeight = 92.51269F;
            this.SNo_allocate.HeaderText = "S. No";
            this.SNo_allocate.Name = "SNo_allocate";
            this.SNo_allocate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SNo_allocate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SNo_allocate.Width = 124;
            // 
            // Client_Name_All
            // 
            this.Client_Name_All.FillWeight = 92.51269F;
            this.Client_Name_All.HeaderText = "CLIENT";
            this.Client_Name_All.Name = "Client_Name_All";
            this.Client_Name_All.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Client_Name_All.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Client_Name_All.Width = 124;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 92.51269F;
            this.Column1.HeaderText = "SUB CLIENT";
            this.Column1.Name = "Column1";
            this.Column1.Width = 124;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "DRN ORDER.NO";
            this.Column12.Name = "Column12";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 92.51269F;
            this.Column2.HeaderText = "ORDER NUMBER";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.Width = 124;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 92.51269F;
            this.Column3.HeaderText = "STATE & COUNTY";
            this.Column3.Name = "Column3";
            this.Column3.Width = 124;
            // 
            // ordertype
            // 
            this.ordertype.HeaderText = "ORDER TYPE";
            this.ordertype.Name = "ordertype";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 92.51269F;
            this.Column4.HeaderText = "RECEIVED DATE";
            this.Column4.Name = "Column4";
            this.Column4.Width = 124;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "ASSIGNED_DATE";
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "VENDOR NAME";
            this.Column15.Name = "Column15";
            // 
            // Column22
            // 
            this.Column22.HeaderText = "STATUS";
            this.Column22.Name = "Column22";
            // 
            // orderid
            // 
            this.orderid.HeaderText = "orderid";
            this.orderid.Name = "orderid";
            this.orderid.Visible = false;
            // 
            // Status_Id
            // 
            this.Status_Id.HeaderText = "Status_Id";
            this.Status_Id.Name = "Status_Id";
            this.Status_Id.Visible = false;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Countyid";
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Client_Id";
            this.Column16.Name = "Column16";
            this.Column16.Visible = false;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Subprocess_Id";
            this.Column17.Name = "Column17";
            this.Column17.Visible = false;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Order_Type_Id";
            this.Column18.Name = "Column18";
            this.Column18.Visible = false;
            // 
            // Vendor_Order_Allocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 608);
            this.Controls.Add(this.grid_Vendor_Order_Export);
            this.Controls.Add(this.btn_Vendor_Export);
            this.Controls.Add(this.btn_user_Reallocate);
            this.Controls.Add(this.ddl_Username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grid_Export);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.btn_Rallocate_Move_To_Inhouse);
            this.Controls.Add(this.btn_Reallocate);
            this.Controls.Add(this.ddl_Vendor_Name);
            this.Controls.Add(this.lbl_Vendor_Name);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btn_move_to_inhouse);
            this.Controls.Add(this.txt_SearchOrdernumber);
            this.Controls.Add(this.lbl_Header);
            this.Controls.Add(this.lbl_allocated_user);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grd_order_Allocated);
            this.Controls.Add(this.btn_Allocate);
            this.Controls.Add(this.grd_order);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TreeView1);
            this.Name = "Vendor_Order_Allocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendor_Order_Allocation";
            this.Load += new System.EventHandler(this.Vendor_Order_Allocation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order_Allocated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Export)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Vendor_Order_Export)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView TreeView1;
        private System.Windows.Forms.Button btn_Allocate;
        private System.Windows.Forms.DataGridView grd_order_Allocated;
        private System.Windows.Forms.Label lbl_allocated_user;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.TextBox txt_SearchOrdernumber;
        private System.Windows.Forms.Button btn_move_to_inhouse;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.ComboBox ddl_Vendor_Name;
        private System.Windows.Forms.Label lbl_Vendor_Name;
        private System.Windows.Forms.Button btn_Reallocate;
        private System.Windows.Forms.Button btn_Rallocate_Move_To_Inhouse;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.DataGridView grid_Export;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddl_Username;
        private System.Windows.Forms.Button btn_user_Reallocate;
        private System.Windows.Forms.Button btn_Vendor_Export;
        private System.Windows.Forms.DataGridView grid_Vendor_Order_Export;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_ProcessName;
        private System.Windows.Forms.DataGridViewButtonColumn Column6;
        private System.Windows.Forms.DataGridViewButtonColumn Order_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATECOUNTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk_Allocate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo_allocate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name_All;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column12;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordertype;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
    }
}