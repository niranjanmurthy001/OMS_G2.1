namespace Ordermanagement_01.Tax
{
    partial class Tax_Reports
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("EXTERNAL PRODUCTION REPORT");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("INTERNAL PRODUCTION REPORT");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("VIOLATION REPORT");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tax_Reports));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Tree_View_Report = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ddl_Subclient = new System.Windows.Forms.ComboBox();
            this.ddl_Client = new System.Windows.Forms.ComboBox();
            this.lbl_SubClient = new System.Windows.Forms.Label();
            this.lbl_Client = new System.Windows.Forms.Label();
            this.rbtn_Completed = new System.Windows.Forms.RadioButton();
            this.rbtn_Recived_Date = new System.Windows.Forms.RadioButton();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.txt_Todate = new System.Windows.Forms.DateTimePicker();
            this.txt_Fromdate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to = new System.Windows.Forms.Label();
            this.lbl_from = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Grd_OrderTime = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grd_OrderTime)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1146, 696);
            this.tableLayoutPanel1.TabIndex = 0;
            //this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1140, 630);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Controls.Add(this.Tree_View_Report, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1140, 630);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // Tree_View_Report
            // 
            this.Tree_View_Report.AllowDrop = true;
            this.Tree_View_Report.BackColor = System.Drawing.Color.SteelBlue;
            this.Tree_View_Report.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Tree_View_Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tree_View_Report.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tree_View_Report.ForeColor = System.Drawing.Color.White;
            this.Tree_View_Report.ImageIndex = 0;
            this.Tree_View_Report.ImageList = this.imageList1;
            this.Tree_View_Report.Indent = 28;
            this.Tree_View_Report.ItemHeight = 28;
            this.Tree_View_Report.Location = new System.Drawing.Point(0, 0);
            this.Tree_View_Report.Margin = new System.Windows.Forms.Padding(0);
            this.Tree_View_Report.Name = "Tree_View_Report";
            treeNode1.Name = "Node2";
            treeNode1.Text = "EXTERNAL PRODUCTION REPORT";
            treeNode2.Name = "Node0";
            treeNode2.Text = "INTERNAL PRODUCTION REPORT";
            treeNode3.Name = "Node0";
            treeNode3.Text = "VIOLATION REPORT";
            this.Tree_View_Report.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.Tree_View_Report.PathSeparator = "";
            this.Tree_View_Report.SelectedImageIndex = 0;
            this.Tree_View_Report.Size = new System.Drawing.Size(228, 610);
            this.Tree_View_Report.TabIndex = 160;
            this.Tree_View_Report.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree_View_Report_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Reports.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(231, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(906, 604);
            this.panel2.TabIndex = 161;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(906, 604);
            this.tableLayoutPanel3.TabIndex = 77;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ddl_Subclient);
            this.panel4.Controls.Add(this.ddl_Client);
            this.panel4.Controls.Add(this.lbl_SubClient);
            this.panel4.Controls.Add(this.lbl_Client);
            this.panel4.Controls.Add(this.rbtn_Completed);
            this.panel4.Controls.Add(this.rbtn_Recived_Date);
            this.panel4.Controls.Add(this.btn_Export);
            this.panel4.Controls.Add(this.btn_Refresh);
            this.panel4.Controls.Add(this.txt_Todate);
            this.panel4.Controls.Add(this.txt_Fromdate);
            this.panel4.Controls.Add(this.lbl_to);
            this.panel4.Controls.Add(this.lbl_from);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(900, 195);
            this.panel4.TabIndex = 0;
            // 
            // ddl_Subclient
            // 
            this.ddl_Subclient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Subclient.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Subclient.FormattingEnabled = true;
            this.ddl_Subclient.Location = new System.Drawing.Point(471, 68);
            this.ddl_Subclient.Name = "ddl_Subclient";
            this.ddl_Subclient.Size = new System.Drawing.Size(173, 28);
            this.ddl_Subclient.TabIndex = 85;
            // 
            // ddl_Client
            // 
            this.ddl_Client.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Client.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Client.FormattingEnabled = true;
            this.ddl_Client.Location = new System.Drawing.Point(212, 70);
            this.ddl_Client.Name = "ddl_Client";
            this.ddl_Client.Size = new System.Drawing.Size(173, 28);
            this.ddl_Client.TabIndex = 84;
            this.ddl_Client.SelectedIndexChanged += new System.EventHandler(this.ddl_Client_SelectedIndexChanged);
            // 
            // lbl_SubClient
            // 
            this.lbl_SubClient.AutoSize = true;
            this.lbl_SubClient.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SubClient.Location = new System.Drawing.Point(393, 73);
            this.lbl_SubClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SubClient.Name = "lbl_SubClient";
            this.lbl_SubClient.Size = new System.Drawing.Size(70, 20);
            this.lbl_SubClient.TabIndex = 83;
            this.lbl_SubClient.Text = "Sub Client:";
            // 
            // lbl_Client
            // 
            this.lbl_Client.AutoSize = true;
            this.lbl_Client.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Client.Location = new System.Drawing.Point(157, 73);
            this.lbl_Client.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Client.Name = "lbl_Client";
            this.lbl_Client.Size = new System.Drawing.Size(48, 20);
            this.lbl_Client.TabIndex = 81;
            this.lbl_Client.Text = "Client :";
            // 
            // rbtn_Completed
            // 
            this.rbtn_Completed.AutoSize = true;
            this.rbtn_Completed.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Completed.Location = new System.Drawing.Point(433, 4);
            this.rbtn_Completed.Name = "rbtn_Completed";
            this.rbtn_Completed.Size = new System.Drawing.Size(138, 21);
            this.rbtn_Completed.TabIndex = 80;
            this.rbtn_Completed.Text = "Completed Date Wise";
            this.rbtn_Completed.UseVisualStyleBackColor = true;
            //this.rbtn_Completed.CheckedChanged += new System.EventHandler(this.rbtn_Completed_CheckedChanged);
            // 
            // rbtn_Recived_Date
            // 
            this.rbtn_Recived_Date.AutoSize = true;
            this.rbtn_Recived_Date.Checked = true;
            this.rbtn_Recived_Date.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Recived_Date.Location = new System.Drawing.Point(304, 5);
            this.rbtn_Recived_Date.Name = "rbtn_Recived_Date";
            this.rbtn_Recived_Date.Size = new System.Drawing.Size(127, 21);
            this.rbtn_Recived_Date.TabIndex = 79;
            this.rbtn_Recived_Date.TabStop = true;
            this.rbtn_Recived_Date.Text = "Recieved Date Wise";
            this.rbtn_Recived_Date.UseVisualStyleBackColor = true;
            //this.rbtn_Recived_Date.CheckedChanged += new System.EventHandler(this.rbtn_Recived_Date_CheckedChanged);
            // 
            // btn_Export
            // 
            this.btn_Export.BackColor = System.Drawing.Color.Transparent;
            this.btn_Export.BackgroundImage = global::Ordermanagement_01.Properties.Resources.button;
            this.btn_Export.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Export.FlatAppearance.BorderSize = 0;
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Export.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.ForeColor = System.Drawing.Color.White;
            this.btn_Export.Location = new System.Drawing.Point(425, 116);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(160, 41);
            this.btn_Export.TabIndex = 78;
            this.btn_Export.Text = "EXPORT";
            this.btn_Export.UseVisualStyleBackColor = false;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.GreeRoundbuttonNew;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Refresh.FlatAppearance.BorderSize = 0;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Refresh.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.White;
            this.btn_Refresh.Location = new System.Drawing.Point(248, 117);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(160, 40);
            this.btn_Refresh.TabIndex = 77;
            this.btn_Refresh.Text = "REFRESH";
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // txt_Todate
            // 
            this.txt_Todate.CustomFormat = "MM/DD/YYYY";
            this.txt_Todate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_Todate.Location = new System.Drawing.Point(471, 32);
            this.txt_Todate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Todate.Name = "txt_Todate";
            this.txt_Todate.Size = new System.Drawing.Size(170, 25);
            this.txt_Todate.TabIndex = 76;
            this.txt_Todate.Value = new System.DateTime(2014, 11, 11, 0, 0, 0, 0);
            // 
            // txt_Fromdate
            // 
            this.txt_Fromdate.CustomFormat = "MM/DD/YYYY";
            this.txt_Fromdate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_Fromdate.Location = new System.Drawing.Point(212, 31);
            this.txt_Fromdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Fromdate.Name = "txt_Fromdate";
            this.txt_Fromdate.Size = new System.Drawing.Size(173, 25);
            this.txt_Fromdate.TabIndex = 75;
            this.txt_Fromdate.Value = new System.DateTime(2014, 11, 11, 0, 0, 0, 0);
            // 
            // lbl_to
            // 
            this.lbl_to.AutoSize = true;
            this.lbl_to.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_to.Location = new System.Drawing.Point(396, 34);
            this.lbl_to.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_to.Name = "lbl_to";
            this.lbl_to.Size = new System.Drawing.Size(62, 20);
            this.lbl_to.TabIndex = 74;
            this.lbl_to.Text = "To Date :";
            // 
            // lbl_from
            // 
            this.lbl_from.AutoSize = true;
            this.lbl_from.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_from.Location = new System.Drawing.Point(128, 34);
            this.lbl_from.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_from.Name = "lbl_from";
            this.lbl_from.Size = new System.Drawing.Size(77, 20);
            this.lbl_from.TabIndex = 73;
            this.lbl_from.Text = "From Date :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.Grd_OrderTime);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 204);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(900, 397);
            this.panel5.TabIndex = 1;
            // 
            // Grd_OrderTime
            // 
            this.Grd_OrderTime.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.Grd_OrderTime.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_OrderTime.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.Grd_OrderTime.ColumnHeadersHeight = 30;
            this.Grd_OrderTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grd_OrderTime.Location = new System.Drawing.Point(0, 0);
            this.Grd_OrderTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Grd_OrderTime.Name = "Grd_OrderTime";
            this.Grd_OrderTime.ReadOnly = true;
            this.Grd_OrderTime.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_OrderTime.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.Grd_OrderTime.RowHeadersVisible = false;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Grd_OrderTime.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_OrderTime.RowTemplate.Height = 25;
            this.Grd_OrderTime.Size = new System.Drawing.Size(900, 397);
            this.Grd_OrderTime.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Lbl_Title);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1140, 34);
            this.panel3.TabIndex = 1;
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.AutoSize = true;
            this.Lbl_Title.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Title.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Title.Location = new System.Drawing.Point(599, 1);
            this.Lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(137, 31);
            this.Lbl_Title.TabIndex = 75;
            this.Lbl_Title.Text = "TAX REPORTS";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Tax_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 696);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Tax_Reports";
            this.Text = "Tax_Reports";
            this.Load += new System.EventHandler(this.Tax_Reports_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grd_OrderTime)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView Tree_View_Report;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DateTimePicker txt_Todate;
        private System.Windows.Forms.DateTimePicker txt_Fromdate;
        private System.Windows.Forms.Label lbl_to;
        private System.Windows.Forms.Label lbl_from;
        internal System.Windows.Forms.Button btn_Export;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.DataGridView Grd_OrderTime;
        private System.Windows.Forms.RadioButton rbtn_Completed;
        private System.Windows.Forms.RadioButton rbtn_Recived_Date;
        private System.Windows.Forms.Label lbl_SubClient;
        private System.Windows.Forms.Label lbl_Client;
        private System.Windows.Forms.ComboBox ddl_Subclient;
        private System.Windows.Forms.ComboBox ddl_Client;
    }
}