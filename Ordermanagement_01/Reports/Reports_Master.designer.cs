namespace Ordermanagement_01.Reports
{
    partial class Reports_Master
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_from = new System.Windows.Forms.Label();
            this.lbl_to = new System.Windows.Forms.Label();
            this.btn_Report = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.tvwRightSide = new System.Windows.Forms.TreeView();
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.txt_Fromdate = new System.Windows.Forms.DateTimePicker();
            this.txt_Todate = new System.Windows.Forms.DateTimePicker();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.Grd_OrderTime = new System.Windows.Forms.DataGridView();
            this.pnl_report = new System.Windows.Forms.Panel();
            this.lbl_Error = new System.Windows.Forms.Label();
            this.crViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.gridclient = new System.Windows.Forms.DataGridView();
            this.grp_Report = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ddl_Task = new System.Windows.Forms.ComboBox();
            this.ddl_OrderNumber = new System.Windows.Forms.ComboBox();
            this.ddl_Status = new System.Windows.Forms.ComboBox();
            this.ddl_SubProcess = new System.Windows.Forms.ComboBox();
            this.ddl_EmployeeName = new System.Windows.Forms.ComboBox();
            this.ddl_ClientName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ddl_Client_Status = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_Subprocess_Status = new System.Windows.Forms.Label();
            this.ddl_Subprocess_Status = new System.Windows.Forms.ComboBox();
            this.btn_treeview = new System.Windows.Forms.Button();
            this.lbl_Chk_Task = new System.Windows.Forms.Label();
            this.ddl_Check_List_Task = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ddl_Check_List_UserName = new System.Windows.Forms.ComboBox();
            this.lbl_User_summary = new System.Windows.Forms.Label();
            this.btn_Clear_All = new System.Windows.Forms.Button();
            this.ddl_OrderTYpe_Abr = new System.Windows.Forms.ComboBox();
            this.lbl_OrderTypr_Abr = new System.Windows.Forms.Label();
            this.pnlSideTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grd_OrderTime)).BeginInit();
            this.pnl_report.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridclient)).BeginInit();
            this.grp_Report.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_from
            // 
            this.lbl_from.AutoSize = true;
            this.lbl_from.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_from.Location = new System.Drawing.Point(226, 49);
            this.lbl_from.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_from.Name = "lbl_from";
            this.lbl_from.Size = new System.Drawing.Size(77, 20);
            this.lbl_from.TabIndex = 3;
            this.lbl_from.Text = "From Date :";
            // 
            // lbl_to
            // 
            this.lbl_to.AutoSize = true;
            this.lbl_to.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_to.Location = new System.Drawing.Point(497, 50);
            this.lbl_to.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_to.Name = "lbl_to";
            this.lbl_to.Size = new System.Drawing.Size(62, 20);
            this.lbl_to.TabIndex = 4;
            this.lbl_to.Text = "To Date :";
            // 
            // btn_Report
            // 
            this.btn_Report.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Report.Location = new System.Drawing.Point(566, 239);
            this.btn_Report.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Report.Name = "btn_Report";
            this.btn_Report.Size = new System.Drawing.Size(85, 30);
            this.btn_Report.TabIndex = 5;
            this.btn_Report.Text = "Refresh";
            this.btn_Report.UseVisualStyleBackColor = true;
            this.btn_Report.Click += new System.EventHandler(this.btn_Report_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Location = new System.Drawing.Point(698, 239);
            this.btn_Export.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(85, 30);
            this.btn_Export.TabIndex = 6;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // tvwRightSide
            // 
            this.tvwRightSide.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwRightSide.Location = new System.Drawing.Point(0, 0);
            this.tvwRightSide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tvwRightSide.Name = "tvwRightSide";
            this.tvwRightSide.Size = new System.Drawing.Size(218, 717);
            this.tvwRightSide.TabIndex = 68;
            this.tvwRightSide.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwRightSide_AfterSelect);
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.tvwRightSide);
            this.pnlSideTree.Location = new System.Drawing.Point(0, 0);
            this.pnlSideTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(218, 720);
            this.pnlSideTree.TabIndex = 69;
            // 
            // txt_Fromdate
            // 
            this.txt_Fromdate.CustomFormat = "MM/DD/YYYY";
            this.txt_Fromdate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_Fromdate.Location = new System.Drawing.Point(310, 47);
            this.txt_Fromdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Fromdate.Name = "txt_Fromdate";
            this.txt_Fromdate.Size = new System.Drawing.Size(173, 25);
            this.txt_Fromdate.TabIndex = 71;
            this.txt_Fromdate.Value = new System.DateTime(2014, 11, 11, 0, 0, 0, 0);
            // 
            // txt_Todate
            // 
            this.txt_Todate.CustomFormat = "MM/DD/YYYY";
            this.txt_Todate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_Todate.Location = new System.Drawing.Point(565, 48);
            this.txt_Todate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Todate.Name = "txt_Todate";
            this.txt_Todate.Size = new System.Drawing.Size(170, 25);
            this.txt_Todate.TabIndex = 72;
            this.txt_Todate.Value = new System.DateTime(2014, 11, 11, 0, 0, 0, 0);
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.AutoSize = true;
            this.Lbl_Title.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Title.ForeColor = System.Drawing.Color.SteelBlue;
            this.Lbl_Title.Location = new System.Drawing.Point(652, 2);
            this.Lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(165, 31);
            this.Lbl_Title.TabIndex = 74;
            this.Lbl_Title.Text = "REPORT MASTER";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Grd_OrderTime
            // 
            this.Grd_OrderTime.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.Grd_OrderTime.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_OrderTime.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grd_OrderTime.ColumnHeadersHeight = 30;
            this.Grd_OrderTime.Location = new System.Drawing.Point(0, 0);
            this.Grd_OrderTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Grd_OrderTime.Name = "Grd_OrderTime";
            this.Grd_OrderTime.ReadOnly = true;
            this.Grd_OrderTime.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_OrderTime.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Grd_OrderTime.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Grd_OrderTime.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.Grd_OrderTime.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_OrderTime.RowTemplate.Height = 25;
            this.Grd_OrderTime.Size = new System.Drawing.Size(1094, 429);
            this.Grd_OrderTime.TabIndex = 0;
            this.Grd_OrderTime.DataSourceChanged += new System.EventHandler(this.Grd_OrderTime_DataSourceChanged);
            this.Grd_OrderTime.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grd_OrderTime_CellClick);
            this.Grd_OrderTime.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grd_OrderTime_CellContentClick);
            // 
            // pnl_report
            // 
            this.pnl_report.Controls.Add(this.lbl_Error);
            this.pnl_report.Controls.Add(this.crViewer);
            this.pnl_report.Controls.Add(this.Grd_OrderTime);
            this.pnl_report.Controls.Add(this.gridclient);
            this.pnl_report.Location = new System.Drawing.Point(239, 271);
            this.pnl_report.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl_report.Name = "pnl_report";
            this.pnl_report.Size = new System.Drawing.Size(1090, 424);
            this.pnl_report.TabIndex = 0;
            // 
            // lbl_Error
            // 
            this.lbl_Error.AutoSize = true;
            this.lbl_Error.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Error.ForeColor = System.Drawing.Color.Red;
            this.lbl_Error.Location = new System.Drawing.Point(391, 13);
            this.lbl_Error.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Size = new System.Drawing.Size(260, 20);
            this.lbl_Error.TabIndex = 86;
            this.lbl_Error.Text = "Select Proper fileds in the left side treeview";
            this.lbl_Error.Visible = false;
            // 
            // crViewer
            // 
            this.crViewer.ActiveViewIndex = -1;
            this.crViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crViewer.Location = new System.Drawing.Point(0, 36);
            this.crViewer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.crViewer.Name = "crViewer";
            this.crViewer.ShowGroupTreeButton = false;
            this.crViewer.Size = new System.Drawing.Size(1090, 388);
            this.crViewer.TabIndex = 5;
            this.crViewer.Load += new System.EventHandler(this.crViewer_Load);
            // 
            // gridclient
            // 
            this.gridclient.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.gridclient.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridclient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridclient.ColumnHeadersHeight = 30;
            this.gridclient.Location = new System.Drawing.Point(0, 0);
            this.gridclient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridclient.Name = "gridclient";
            this.gridclient.ReadOnly = true;
            this.gridclient.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridclient.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridclient.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridclient.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridclient.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridclient.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.gridclient.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.gridclient.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.gridclient.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.gridclient.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridclient.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridclient.RowTemplate.Height = 25;
            this.gridclient.Size = new System.Drawing.Size(1090, 429);
            this.gridclient.TabIndex = 86;
            this.gridclient.Visible = false;
            // 
            // grp_Report
            // 
            this.grp_Report.Controls.Add(this.label5);
            this.grp_Report.Controls.Add(this.ddl_Task);
            this.grp_Report.Controls.Add(this.ddl_OrderNumber);
            this.grp_Report.Controls.Add(this.ddl_Status);
            this.grp_Report.Controls.Add(this.ddl_SubProcess);
            this.grp_Report.Controls.Add(this.ddl_EmployeeName);
            this.grp_Report.Controls.Add(this.ddl_ClientName);
            this.grp_Report.Controls.Add(this.label8);
            this.grp_Report.Controls.Add(this.label7);
            this.grp_Report.Controls.Add(this.label6);
            this.grp_Report.Controls.Add(this.label4);
            this.grp_Report.Controls.Add(this.label3);
            this.grp_Report.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Report.Location = new System.Drawing.Point(239, 130);
            this.grp_Report.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grp_Report.Name = "grp_Report";
            this.grp_Report.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grp_Report.Size = new System.Drawing.Size(1083, 103);
            this.grp_Report.TabIndex = 75;
            this.grp_Report.TabStop = false;
            this.grp_Report.Text = "REPORT DETAILS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(362, 27);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "SubProcessName :";
            // 
            // ddl_Task
            // 
            this.ddl_Task.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Task.FormattingEnabled = true;
            this.ddl_Task.Location = new System.Drawing.Point(853, 61);
            this.ddl_Task.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Task.Name = "ddl_Task";
            this.ddl_Task.Size = new System.Drawing.Size(164, 28);
            this.ddl_Task.TabIndex = 29;
            // 
            // ddl_OrderNumber
            // 
            this.ddl_OrderNumber.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_OrderNumber.FormattingEnabled = true;
            this.ddl_OrderNumber.Location = new System.Drawing.Point(853, 24);
            this.ddl_OrderNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_OrderNumber.Name = "ddl_OrderNumber";
            this.ddl_OrderNumber.Size = new System.Drawing.Size(164, 28);
            this.ddl_OrderNumber.TabIndex = 28;
            // 
            // ddl_Status
            // 
            this.ddl_Status.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Status.FormattingEnabled = true;
            this.ddl_Status.Location = new System.Drawing.Point(526, 61);
            this.ddl_Status.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Status.Name = "ddl_Status";
            this.ddl_Status.Size = new System.Drawing.Size(164, 28);
            this.ddl_Status.TabIndex = 27;
            // 
            // ddl_SubProcess
            // 
            this.ddl_SubProcess.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_SubProcess.FormattingEnabled = true;
            this.ddl_SubProcess.Location = new System.Drawing.Point(526, 24);
            this.ddl_SubProcess.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_SubProcess.Name = "ddl_SubProcess";
            this.ddl_SubProcess.Size = new System.Drawing.Size(164, 28);
            this.ddl_SubProcess.TabIndex = 26;
            // 
            // ddl_EmployeeName
            // 
            this.ddl_EmployeeName.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_EmployeeName.FormattingEnabled = true;
            this.ddl_EmployeeName.Location = new System.Drawing.Point(176, 62);
            this.ddl_EmployeeName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_EmployeeName.Name = "ddl_EmployeeName";
            this.ddl_EmployeeName.Size = new System.Drawing.Size(160, 28);
            this.ddl_EmployeeName.TabIndex = 25;
            // 
            // ddl_ClientName
            // 
            this.ddl_ClientName.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_ClientName.FormattingEnabled = true;
            this.ddl_ClientName.Location = new System.Drawing.Point(176, 24);
            this.ddl_ClientName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_ClientName.Name = "ddl_ClientName";
            this.ddl_ClientName.Size = new System.Drawing.Size(160, 28);
            this.ddl_ClientName.TabIndex = 24;
            this.ddl_ClientName.SelectedIndexChanged += new System.EventHandler(this.ddl_ClientName_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(713, 64);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "Task";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(712, 27);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "OrderNumber :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(362, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Status :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(65, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "UserName :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(65, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Client Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(227, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 76;
            this.label1.Text = "Client Name :";
            // 
            // ddl_Client_Status
            // 
            this.ddl_Client_Status.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Client_Status.FormattingEnabled = true;
            this.ddl_Client_Status.Location = new System.Drawing.Point(328, 89);
            this.ddl_Client_Status.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Client_Status.Name = "ddl_Client_Status";
            this.ddl_Client_Status.Size = new System.Drawing.Size(173, 28);
            this.ddl_Client_Status.TabIndex = 77;
            this.ddl_Client_Status.SelectedIndexChanged += new System.EventHandler(this.ddl_Client_Status_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1039, 238);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 30);
            this.button1.TabIndex = 81;
            this.button1.Text = "Export Order Counts";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // lbl_Subprocess_Status
            // 
            this.lbl_Subprocess_Status.AutoSize = true;
            this.lbl_Subprocess_Status.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Subprocess_Status.Location = new System.Drawing.Point(513, 94);
            this.lbl_Subprocess_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Subprocess_Status.Name = "lbl_Subprocess_Status";
            this.lbl_Subprocess_Status.Size = new System.Drawing.Size(118, 20);
            this.lbl_Subprocess_Status.TabIndex = 82;
            this.lbl_Subprocess_Status.Text = "SubProcessName :";
            this.lbl_Subprocess_Status.Click += new System.EventHandler(this.lbl_Subprocess_Status_Click);
            // 
            // ddl_Subprocess_Status
            // 
            this.ddl_Subprocess_Status.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Subprocess_Status.FormattingEnabled = true;
            this.ddl_Subprocess_Status.Location = new System.Drawing.Point(639, 88);
            this.ddl_Subprocess_Status.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Subprocess_Status.Name = "ddl_Subprocess_Status";
            this.ddl_Subprocess_Status.Size = new System.Drawing.Size(173, 28);
            this.ddl_Subprocess_Status.TabIndex = 83;
            // 
            // btn_treeview
            // 
            this.btn_treeview.BackColor = System.Drawing.Color.Transparent;
            this.btn_treeview.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_treeview.Image = global::Ordermanagement_01.Properties.Resources.left;
            this.btn_treeview.Location = new System.Drawing.Point(219, 0);
            this.btn_treeview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_treeview.Name = "btn_treeview";
            this.btn_treeview.Size = new System.Drawing.Size(36, 26);
            this.btn_treeview.TabIndex = 70;
            this.btn_treeview.UseVisualStyleBackColor = false;
            this.btn_treeview.Visible = false;
            this.btn_treeview.Click += new System.EventHandler(this.btn_treeview_Click);
            // 
            // lbl_Chk_Task
            // 
            this.lbl_Chk_Task.AutoSize = true;
            this.lbl_Chk_Task.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Chk_Task.Location = new System.Drawing.Point(775, 49);
            this.lbl_Chk_Task.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Chk_Task.Name = "lbl_Chk_Task";
            this.lbl_Chk_Task.Size = new System.Drawing.Size(38, 20);
            this.lbl_Chk_Task.TabIndex = 84;
            this.lbl_Chk_Task.Text = "Task:";
            // 
            // ddl_Check_List_Task
            // 
            this.ddl_Check_List_Task.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Check_List_Task.FormattingEnabled = true;
            this.ddl_Check_List_Task.Location = new System.Drawing.Point(845, 44);
            this.ddl_Check_List_Task.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Check_List_Task.Name = "ddl_Check_List_Task";
            this.ddl_Check_List_Task.Size = new System.Drawing.Size(169, 28);
            this.ddl_Check_List_Task.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(824, 93);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 85;
            this.label2.Text = "UserName :";
            this.label2.Visible = false;
            // 
            // ddl_Check_List_UserName
            // 
            this.ddl_Check_List_UserName.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Check_List_UserName.FormattingEnabled = true;
            this.ddl_Check_List_UserName.Location = new System.Drawing.Point(908, 88);
            this.ddl_Check_List_UserName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Check_List_UserName.Name = "ddl_Check_List_UserName";
            this.ddl_Check_List_UserName.Size = new System.Drawing.Size(145, 28);
            this.ddl_Check_List_UserName.TabIndex = 30;
            this.ddl_Check_List_UserName.Visible = false;
            // 
            // lbl_User_summary
            // 
            this.lbl_User_summary.AutoSize = true;
            this.lbl_User_summary.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_User_summary.ForeColor = System.Drawing.Color.Red;
            this.lbl_User_summary.Location = new System.Drawing.Point(630, 122);
            this.lbl_User_summary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_User_summary.Name = "lbl_User_summary";
            this.lbl_User_summary.Size = new System.Drawing.Size(240, 20);
            this.lbl_User_summary.TabIndex = 30;
            this.lbl_User_summary.Text = "* Userwise Client Names Not yet added";
            this.lbl_User_summary.Visible = false;
            // 
            // btn_Clear_All
            // 
            this.btn_Clear_All.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear_All.Location = new System.Drawing.Point(824, 239);
            this.btn_Clear_All.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Clear_All.Name = "btn_Clear_All";
            this.btn_Clear_All.Size = new System.Drawing.Size(85, 30);
            this.btn_Clear_All.TabIndex = 86;
            this.btn_Clear_All.Text = "Clear";
            this.btn_Clear_All.UseVisualStyleBackColor = true;
            this.btn_Clear_All.Click += new System.EventHandler(this.btn_Clear_All_Click);
            // 
            // ddl_OrderTYpe_Abr
            // 
            this.ddl_OrderTYpe_Abr.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ddl_OrderTYpe_Abr.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_OrderTYpe_Abr.FormattingEnabled = true;
            this.ddl_OrderTYpe_Abr.Location = new System.Drawing.Point(1169, 43);
            this.ddl_OrderTYpe_Abr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_OrderTYpe_Abr.Name = "ddl_OrderTYpe_Abr";
            this.ddl_OrderTYpe_Abr.Size = new System.Drawing.Size(140, 28);
            this.ddl_OrderTYpe_Abr.TabIndex = 108;
            // 
            // lbl_OrderTypr_Abr
            // 
            this.lbl_OrderTypr_Abr.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.lbl_OrderTypr_Abr.AutoSize = true;
            this.lbl_OrderTypr_Abr.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OrderTypr_Abr.Location = new System.Drawing.Point(1041, 47);
            this.lbl_OrderTypr_Abr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_OrderTypr_Abr.Name = "lbl_OrderTypr_Abr";
            this.lbl_OrderTypr_Abr.Size = new System.Drawing.Size(116, 20);
            this.lbl_OrderTypr_Abr.TabIndex = 109;
            this.lbl_OrderTypr_Abr.Text = "Order Type Abbr :";
            // 
            // Reports_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 707);
            this.Controls.Add(this.ddl_OrderTYpe_Abr);
            this.Controls.Add(this.lbl_OrderTypr_Abr);
            this.Controls.Add(this.btn_Clear_All);
            this.Controls.Add(this.lbl_User_summary);
            this.Controls.Add(this.ddl_Check_List_UserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ddl_Check_List_Task);
            this.Controls.Add(this.lbl_Chk_Task);
            this.Controls.Add(this.ddl_Subprocess_Status);
            this.Controls.Add(this.lbl_Subprocess_Status);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ddl_Client_Status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grp_Report);
            this.Controls.Add(this.Lbl_Title);
            this.Controls.Add(this.txt_Todate);
            this.Controls.Add(this.txt_Fromdate);
            this.Controls.Add(this.btn_treeview);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.lbl_to);
            this.Controls.Add(this.lbl_from);
            this.Controls.Add(this.btn_Report);
            this.Controls.Add(this.pnl_report);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Reports_Master";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports_Master";
            this.Load += new System.EventHandler(this.Reports_Master_Load);
            this.pnlSideTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grd_OrderTime)).EndInit();
            this.pnl_report.ResumeLayout(false);
            this.pnl_report.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridclient)).EndInit();
            this.grp_Report.ResumeLayout(false);
            this.grp_Report.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_from;
        private System.Windows.Forms.Label lbl_to;
        private System.Windows.Forms.Button btn_Report;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.TreeView tvwRightSide;
        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.Button btn_treeview;
        private System.Windows.Forms.DateTimePicker txt_Fromdate;
        private System.Windows.Forms.DateTimePicker txt_Todate;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.DataGridView Grd_OrderTime;
        private System.Windows.Forms.Panel pnl_report;
        private System.Windows.Forms.GroupBox grp_Report;
        private System.Windows.Forms.ComboBox ddl_Task;
        private System.Windows.Forms.ComboBox ddl_OrderNumber;
        private System.Windows.Forms.ComboBox ddl_Status;
        private System.Windows.Forms.ComboBox ddl_SubProcess;
        private System.Windows.Forms.ComboBox ddl_EmployeeName;
        private System.Windows.Forms.ComboBox ddl_ClientName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddl_Client_Status;
        private System.Windows.Forms.Button button1;
        internal CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer;
        private System.Windows.Forms.Label lbl_Subprocess_Status;
        private System.Windows.Forms.ComboBox ddl_Subprocess_Status;
        private System.Windows.Forms.Label lbl_Chk_Task;
        private System.Windows.Forms.ComboBox ddl_Check_List_Task;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddl_Check_List_UserName;
        private System.Windows.Forms.Label lbl_User_summary;
        private System.Windows.Forms.DataGridView gridclient;
        private System.Windows.Forms.Label lbl_Error;
        private System.Windows.Forms.Button btn_Clear_All;
        private System.Windows.Forms.ComboBox ddl_OrderTYpe_Abr;
        private System.Windows.Forms.Label lbl_OrderTypr_Abr;
    }
}