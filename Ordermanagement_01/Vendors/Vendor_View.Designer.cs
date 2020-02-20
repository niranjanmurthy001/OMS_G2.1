namespace Ordermanagement_01.Vendors
{
    partial class Vendor_View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vendor_View));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorCreateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorClientSubclientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capacityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.percentageOfOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorTypeMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Vendor_User_toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.grd_Services = new System.Windows.Forms.DataGridView();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cbo_colmn = new System.Windows.Forms.ComboBox();
            this.txt_SearchVendor_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblRecordsStatus = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_Total_Orders = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.keywordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Services)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(155)))), ((int)(((byte)(234)))));
            this.MenuStrip.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.Vendor_User_toolStripMenuItem1});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(1296, 27);
            this.MenuStrip.TabIndex = 99;
            this.MenuStrip.Text = "MenuStrip";
            this.MenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuStrip_ItemClicked);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vendorCreateToolStripMenuItem1,
            this.vendorClientSubclientsToolStripMenuItem,
            this.orderTypeToolStripMenuItem,
            this.capacityToolStripMenuItem,
            this.percentageOfOrderToolStripMenuItem,
            this.toolStripMenuItem1,
            this.vendorTypeMasterToolStripMenuItem,
            this.vendToolStripMenuItem,
            this.keywordsToolStripMenuItem});
            this.toolStripMenuItem2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(62, 23);
            this.toolStripMenuItem2.Text = "Masters";
            this.toolStripMenuItem2.DropDownClosed += new System.EventHandler(this.toolStripMenuItem2_DropDownClosed);
            this.toolStripMenuItem2.DropDownOpened += new System.EventHandler(this.toolStripMenuItem2_DropDownOpened);
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click_1);
            // 
            // vendorCreateToolStripMenuItem1
            // 
            this.vendorCreateToolStripMenuItem1.Name = "vendorCreateToolStripMenuItem1";
            this.vendorCreateToolStripMenuItem1.Size = new System.Drawing.Size(288, 24);
            this.vendorCreateToolStripMenuItem1.Text = "Vendor Create";
            this.vendorCreateToolStripMenuItem1.Click += new System.EventHandler(this.vendorCreateToolStripMenuItem1_Click);
            // 
            // vendorClientSubclientsToolStripMenuItem
            // 
            this.vendorClientSubclientsToolStripMenuItem.Name = "vendorClientSubclientsToolStripMenuItem";
            this.vendorClientSubclientsToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.vendorClientSubclientsToolStripMenuItem.Text = "Client Subclients";
            this.vendorClientSubclientsToolStripMenuItem.Click += new System.EventHandler(this.vendorClientSubclientsToolStripMenuItem_Click);
            // 
            // orderTypeToolStripMenuItem
            // 
            this.orderTypeToolStripMenuItem.Name = "orderTypeToolStripMenuItem";
            this.orderTypeToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.orderTypeToolStripMenuItem.Text = "Order Type";
            this.orderTypeToolStripMenuItem.Click += new System.EventHandler(this.orderTypeToolStripMenuItem_Click);
            // 
            // capacityToolStripMenuItem
            // 
            this.capacityToolStripMenuItem.Name = "capacityToolStripMenuItem";
            this.capacityToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.capacityToolStripMenuItem.Text = "Capacity";
            this.capacityToolStripMenuItem.Click += new System.EventHandler(this.capacityToolStripMenuItem_Click);
            // 
            // percentageOfOrderToolStripMenuItem
            // 
            this.percentageOfOrderToolStripMenuItem.Name = "percentageOfOrderToolStripMenuItem";
            this.percentageOfOrderToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.percentageOfOrderToolStripMenuItem.Text = "Percentage(%) of Order";
            this.percentageOfOrderToolStripMenuItem.Click += new System.EventHandler(this.percentageOfOrderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(288, 24);
            this.toolStripMenuItem1.Text = "Client Instructions";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click_2);
            // 
            // vendorTypeMasterToolStripMenuItem
            // 
            this.vendorTypeMasterToolStripMenuItem.Name = "vendorTypeMasterToolStripMenuItem";
            this.vendorTypeMasterToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.vendorTypeMasterToolStripMenuItem.Text = "Vendor Typing Master";
            this.vendorTypeMasterToolStripMenuItem.Click += new System.EventHandler(this.vendorTypeMasterToolStripMenuItem_Click);
            // 
            // vendToolStripMenuItem
            // 
            this.vendToolStripMenuItem.Name = "vendToolStripMenuItem";
            this.vendToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.vendToolStripMenuItem.Text = "Vendor Typing Client Wise Master Fields";
            this.vendToolStripMenuItem.Click += new System.EventHandler(this.vendToolStripMenuItem_Click);
            // 
            // Vendor_User_toolStripMenuItem1
            // 
            this.Vendor_User_toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.Vendor_User_toolStripMenuItem1.Name = "Vendor_User_toolStripMenuItem1";
            this.Vendor_User_toolStripMenuItem1.Size = new System.Drawing.Size(85, 23);
            this.Vendor_User_toolStripMenuItem1.Text = "Vendor User";
            this.Vendor_User_toolStripMenuItem1.DropDownClosed += new System.EventHandler(this.Vendor_User_toolStripMenuItem1_DropDownClosed);
            this.Vendor_User_toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(155)))), ((int)(((byte)(234)))));
            this.ToolStrip1.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton10,
            this.toolStripSeparator4,
            this.toolStripButton1,
            this.ToolStripSeparator15,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton4});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 27);
            this.ToolStrip1.MaximumSize = new System.Drawing.Size(0, 50);
            this.ToolStrip1.MinimumSize = new System.Drawing.Size(0, 50);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(1296, 50);
            this.ToolStrip1.TabIndex = 100;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // ToolStripButton10
            // 
            this.ToolStripButton10.ForeColor = System.Drawing.Color.White;
            this.ToolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton10.Image")));
            this.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton10.Name = "ToolStripButton10";
            this.ToolStripButton10.Size = new System.Drawing.Size(121, 47);
            this.ToolStripButton10.Text = "Vendor Queue";
            this.ToolStripButton10.Click += new System.EventHandler(this.ToolStripButton10_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = global::Ordermanagement_01.Properties.Resources.vendor_new1;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 47);
            this.toolStripButton1.Text = "Vendors";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // ToolStripSeparator15
            // 
            this.ToolStripSeparator15.Name = "ToolStripSeparator15";
            this.ToolStripSeparator15.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripButton2.Image = global::Ordermanagement_01.Properties.Resources.Battery_128;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(131, 47);
            this.toolStripButton2.Text = "Vendor Capacity";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripButton3.Image = global::Ordermanagement_01.Properties.Resources._048_128;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(151, 47);
            this.toolStripButton3.Text = "Vendor (%) of Order";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 50);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.ForeColor = System.Drawing.Color.White;
            this.toolStripButton4.Image = global::Ordermanagement_01.Properties.Resources.user_128;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(109, 47);
            this.toolStripButton4.Text = "Vendor User";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // grd_Services
            // 
            this.grd_Services.AllowUserToAddRows = false;
            this.grd_Services.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grd_Services.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Services.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Services.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Services.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_Services.ColumnHeadersHeight = 30;
            this.grd_Services.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNO,
            this.Column7,
            this.Column1,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column3,
            this.Column4,
            this.Column2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Services.DefaultCellStyle = dataGridViewCellStyle3;
            this.grd_Services.Location = new System.Drawing.Point(1, 136);
            this.grd_Services.Name = "grd_Services";
            this.grd_Services.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Services.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_Services.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Services.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grd_Services.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.grd_Services.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_Services.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grd_Services.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_Services.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_Services.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_Services.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Services.RowTemplate.Height = 25;
            this.grd_Services.Size = new System.Drawing.Size(1275, 463);
            this.grd_Services.TabIndex = 3;
            this.grd_Services.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Services_CellClick);
            // 
            // SNO
            // 
            this.SNO.FillWeight = 2.207937F;
            this.SNO.HeaderText = "S. No";
            this.SNO.MinimumWidth = 80;
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            this.SNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column7
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column7.FillWeight = 204.8032F;
            this.Column7.HeaderText = "VENDOR NAME";
            this.Column7.MinimumWidth = 350;
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 104.3426F;
            this.Column1.HeaderText = "PHONE NO";
            this.Column1.MinimumWidth = 140;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 64.16607F;
            this.Column5.HeaderText = "EMAIL";
            this.Column5.MinimumWidth = 180;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 27.60588F;
            this.Column6.HeaderText = "FAX NO";
            this.Column6.MinimumWidth = 160;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Vendor_Id";
            this.Column8.MinimumWidth = 50;
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 161.7389F;
            this.Column3.HeaderText = "STATE COUNTY";
            this.Column3.MinimumWidth = 100;
            this.Column3.Name = "Column3";
            this.Column3.Text = "View/Edit";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 170.0975F;
            this.Column4.HeaderText = "USER";
            this.Column4.MinimumWidth = 100;
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 102.7684F;
            this.Column2.HeaderText = "DELETE";
            this.Column2.MinimumWidth = 38;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cbo_colmn
            // 
            this.cbo_colmn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_colmn.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_colmn.FormattingEnabled = true;
            this.cbo_colmn.Items.AddRange(new object[] {
            "VENDOR NAME",
            "EMAIL",
            "PHONE NO"});
            this.cbo_colmn.Location = new System.Drawing.Point(196, 98);
            this.cbo_colmn.Name = "cbo_colmn";
            this.cbo_colmn.Size = new System.Drawing.Size(247, 28);
            this.cbo_colmn.TabIndex = 1;
            this.cbo_colmn.SelectedIndexChanged += new System.EventHandler(this.cbo_colmn_SelectedIndexChanged);
            // 
            // txt_SearchVendor_Name
            // 
            this.txt_SearchVendor_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_SearchVendor_Name.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_SearchVendor_Name.Location = new System.Drawing.Point(471, 99);
            this.txt_SearchVendor_Name.Name = "txt_SearchVendor_Name";
            this.txt_SearchVendor_Name.Size = new System.Drawing.Size(436, 25);
            this.txt_SearchVendor_Name.TabIndex = 2;
            this.txt_SearchVendor_Name.TextChanged += new System.EventHandler(this.txt_Vendor_Name_TextChanged);
            this.txt_SearchVendor_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_SearchVendor_Name_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(71, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 24);
            this.label2.TabIndex = 105;
            this.label2.Text = "Search Vendor:\r\n";
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
            this.panel1.Location = new System.Drawing.Point(0, 596);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1276, 43);
            this.panel1.TabIndex = 131;
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.Gainsboro;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.Location = new System.Drawing.Point(291, 8);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(75, 24);
            this.btnFirst.TabIndex = 4;
            this.btnFirst.Text = "|< First";
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblRecordsStatus
            // 
            this.lblRecordsStatus.AutoSize = true;
            this.lblRecordsStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecordsStatus.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lblRecordsStatus.Location = new System.Drawing.Point(626, 11);
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
            this.btnPrevious.Location = new System.Drawing.Point(436, 8);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 24);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.Text = "< Pervious";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.Location = new System.Drawing.Point(912, 9);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 24);
            this.btnLast.TabIndex = 7;
            this.btnLast.Text = "Last >|";
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(762, 9);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 24);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1131, 12);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 19);
            this.label8.TabIndex = 25;
            this.label8.Text = "Total Vendors :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_Total_Orders
            // 
            this.lbl_Total_Orders.AutoSize = true;
            this.lbl_Total_Orders.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total_Orders.ForeColor = System.Drawing.Color.Red;
            this.lbl_Total_Orders.Location = new System.Drawing.Point(1227, 13);
            this.lbl_Total_Orders.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Total_Orders.Name = "lbl_Total_Orders";
            this.lbl_Total_Orders.Size = new System.Drawing.Size(14, 17);
            this.lbl_Total_Orders.TabIndex = 26;
            this.lbl_Total_Orders.Text = "T";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(8, 92);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(32, 32);
            this.btn_Refresh.TabIndex = 104;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // keywordsToolStripMenuItem
            // 
            this.keywordsToolStripMenuItem.Name = "keywordsToolStripMenuItem";
            this.keywordsToolStripMenuItem.Size = new System.Drawing.Size(288, 24);
            this.keywordsToolStripMenuItem.Text = "Keywords";
            this.keywordsToolStripMenuItem.Click += new System.EventHandler(this.keywordsToolStripMenuItem_Click);
            // 
            // Vendor_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 640);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbo_colmn);
            this.Controls.Add(this.txt_SearchVendor_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.grd_Services);
            this.Controls.Add(this.ToolStrip1);
            this.Controls.Add(this.MenuStrip);
            this.MaximizeBox = false;
            this.Name = "Vendor_View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendor_View";
            this.Load += new System.EventHandler(this.Vendor_View_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Services)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.MenuStrip MenuStrip;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripButton ToolStripButton10;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator15;
        private System.Windows.Forms.DataGridView grd_Services;
        private System.Windows.Forms.ComboBox cbo_colmn;
        private System.Windows.Forms.TextBox txt_SearchVendor_Name;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblRecordsStatus;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_Total_Orders;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem Vendor_User_toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem vendorCreateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem vendorClientSubclientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capacityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem percentageOfOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem vendorTypeMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private System.Windows.Forms.ToolStripMenuItem keywordsToolStripMenuItem;
    }
}