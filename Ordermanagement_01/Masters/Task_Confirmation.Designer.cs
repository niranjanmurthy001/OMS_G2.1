namespace Ordermanagement_01.Masters
{
    partial class Task_Confirmation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Task_Confirmation));
            this.grp_OrderType = new System.Windows.Forms.GroupBox();
            this.ddl_Task = new System.Windows.Forms.ComboBox();
            this.txt_Information = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.Grid_Comments = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Move_Down = new System.Windows.Forms.Button();
            this.btn_Move_Up = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ddl_Yes_No = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Datagrid_Options = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_Option = new System.Windows.Forms.Label();
            this.datagrid_Reasons = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Options = new System.Windows.Forms.TextBox();
            this.txt_Reason = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Question_Reason = new System.Windows.Forms.Label();
            this.txt_Message = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.ddl_Sub_Task = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Delete_Node = new System.Windows.Forms.Button();
            this.btn_Add_Node = new System.Windows.Forms.Button();
            this.Tree_View_Task = new System.Windows.Forms.TreeView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.grp_OrderType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Comments)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datagrid_Options)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_Reasons)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_OrderType
            // 
            this.grp_OrderType.Controls.Add(this.ddl_Task);
            this.grp_OrderType.Controls.Add(this.txt_Information);
            this.grp_OrderType.Controls.Add(this.label2);
            this.grp_OrderType.Controls.Add(this.label1);
            this.grp_OrderType.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_OrderType.Location = new System.Drawing.Point(16, 19);
            this.grp_OrderType.Name = "grp_OrderType";
            this.grp_OrderType.Size = new System.Drawing.Size(1211, 194);
            this.grp_OrderType.TabIndex = 74;
            this.grp_OrderType.TabStop = false;
            this.grp_OrderType.Text = "TASK CONFERMATION DETAILS";
            // 
            // ddl_Task
            // 
            this.ddl_Task.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Task.FormattingEnabled = true;
            this.ddl_Task.Location = new System.Drawing.Point(194, 35);
            this.ddl_Task.Name = "ddl_Task";
            this.ddl_Task.Size = new System.Drawing.Size(253, 28);
            this.ddl_Task.TabIndex = 82;
            this.ddl_Task.SelectedIndexChanged += new System.EventHandler(this.ddl_Task_SelectedIndexChanged);
            // 
            // txt_Information
            // 
            this.txt_Information.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Information.Location = new System.Drawing.Point(194, 74);
            this.txt_Information.Multiline = true;
            this.txt_Information.Name = "txt_Information";
            this.txt_Information.Size = new System.Drawing.Size(509, 112);
            this.txt_Information.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label2.Location = new System.Drawing.Point(71, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 81;
            this.label2.Text = "Information:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label1.Location = new System.Drawing.Point(71, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 80;
            this.label1.Text = "Task:";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(623, 220);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(85, 35);
            this.btn_Delete.TabIndex = 81;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(732, 220);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(61, 35);
            this.btn_Cancel.TabIndex = 80;
            this.btn_Cancel.Text = "Clear";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Save.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(510, 220);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(81, 35);
            this.btn_Save.TabIndex = 79;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Grid_Comments
            // 
            this.Grid_Comments.AllowUserToAddRows = false;
            this.Grid_Comments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Grid_Comments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grid_Comments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Comments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid_Comments.ColumnHeadersHeight = 30;
            this.Grid_Comments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column5,
            this.Column7,
            this.Column8,
            this.Column1,
            this.Column3,
            this.Column4});
            this.Grid_Comments.Location = new System.Drawing.Point(22, 260);
            this.Grid_Comments.Name = "Grid_Comments";
            this.Grid_Comments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Grid_Comments.RowHeadersVisible = false;
            this.Grid_Comments.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Grid_Comments.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.Grid_Comments.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Grid_Comments.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Grid_Comments.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.Grid_Comments.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.Grid_Comments.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Comments.RowTemplate.Height = 25;
            this.Grid_Comments.Size = new System.Drawing.Size(1117, 296);
            this.Grid_Comments.TabIndex = 82;
            this.Grid_Comments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_Comments_CellClick);
            this.Grid_Comments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_Comments_CellContentClick);
            // 
            // Column2
            // 
            this.Column2.FillWeight = 20.30457F;
            this.Column2.HeaderText = "S.No";
            this.Column2.Name = "Column2";
            // 
            // Column5
            // 
            this.Column5.FillWeight = 32.18202F;
            this.Column5.HeaderText = "";
            this.Column5.Name = "Column5";
            // 
            // Column7
            // 
            this.Column7.FillWeight = 173.7567F;
            this.Column7.HeaderText = "TASK";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column8
            // 
            this.Column8.FillWeight = 173.7567F;
            this.Column8.HeaderText = "MESSAGE";
            this.Column8.Name = "Column8";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Task_Confirm_Id";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Order_Staus";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Grid_Order_Id";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(6, 18);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1252, 615);
            this.tabControl1.TabIndex = 85;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.Grid_Comments);
            this.tabPage1.Controls.Add(this.grp_OrderType);
            this.tabPage1.Controls.Add(this.btn_Move_Down);
            this.tabPage1.Controls.Add(this.btn_Delete);
            this.tabPage1.Controls.Add(this.btn_Move_Up);
            this.tabPage1.Controls.Add(this.btn_Save);
            this.tabPage1.Controls.Add(this.btn_Cancel);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1244, 582);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main Task";
            // 
            // btn_Move_Down
            // 
            this.btn_Move_Down.Image = global::Ordermanagement_01.Properties.Resources.Down;
            this.btn_Move_Down.Location = new System.Drawing.Point(1191, 260);
            this.btn_Move_Down.Name = "btn_Move_Down";
            this.btn_Move_Down.Size = new System.Drawing.Size(36, 35);
            this.btn_Move_Down.TabIndex = 84;
            this.btn_Move_Down.UseVisualStyleBackColor = true;
            this.btn_Move_Down.Click += new System.EventHandler(this.btn_Move_Down_Click);
            // 
            // btn_Move_Up
            // 
            this.btn_Move_Up.Image = global::Ordermanagement_01.Properties.Resources.Up;
            this.btn_Move_Up.Location = new System.Drawing.Point(1148, 260);
            this.btn_Move_Up.Name = "btn_Move_Up";
            this.btn_Move_Up.Size = new System.Drawing.Size(36, 35);
            this.btn_Move_Up.TabIndex = 83;
            this.btn_Move_Up.UseVisualStyleBackColor = true;
            this.btn_Move_Up.Click += new System.EventHandler(this.btn_Move_Up_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.ddl_Yes_No);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.btn_Refresh);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.radioButton2);
            this.tabPage2.Controls.Add(this.radioButton1);
            this.tabPage2.Controls.Add(this.ddl_Sub_Task);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btn_Delete_Node);
            this.tabPage2.Controls.Add(this.btn_Add_Node);
            this.tabPage2.Controls.Add(this.Tree_View_Task);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1244, 582);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sub Task";
            // 
            // ddl_Yes_No
            // 
            this.ddl_Yes_No.FormattingEnabled = true;
            this.ddl_Yes_No.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.ddl_Yes_No.Location = new System.Drawing.Point(767, 40);
            this.ddl_Yes_No.Name = "ddl_Yes_No";
            this.ddl_Yes_No.Size = new System.Drawing.Size(126, 28);
            this.ddl_Yes_No.TabIndex = 154;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(612, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 19);
            this.label7.TabIndex = 151;
            this.label7.Text = "Populate Question on ";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(393, 36);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(38, 28);
            this.btn_Refresh.TabIndex = 150;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Datagrid_Options);
            this.groupBox2.Controls.Add(this.lbl_Option);
            this.groupBox2.Controls.Add(this.datagrid_Reasons);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_Options);
            this.groupBox2.Controls.Add(this.txt_Reason);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(609, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(784, 362);
            this.groupBox2.TabIndex = 149;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reason/Option";
            // 
            // Datagrid_Options
            // 
            this.Datagrid_Options.AllowUserToAddRows = false;
            this.Datagrid_Options.AllowUserToDeleteRows = false;
            this.Datagrid_Options.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Datagrid_Options.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Datagrid_Options.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Datagrid_Options.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.Datagrid_Options.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Datagrid_Options.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Datagrid_Options.ColumnHeadersHeight = 30;
            this.Datagrid_Options.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn21});
            this.Datagrid_Options.Location = new System.Drawing.Point(117, 63);
            this.Datagrid_Options.Name = "Datagrid_Options";
            this.Datagrid_Options.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Datagrid_Options.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Datagrid_Options.RowHeadersVisible = false;
            this.Datagrid_Options.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Datagrid_Options.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.Datagrid_Options.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Datagrid_Options.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Datagrid_Options.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.Datagrid_Options.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.Datagrid_Options.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Datagrid_Options.RowTemplate.Height = 25;
            this.Datagrid_Options.Size = new System.Drawing.Size(519, 112);
            this.Datagrid_Options.TabIndex = 146;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.FillWeight = 243.5829F;
            this.dataGridViewTextBoxColumn19.HeaderText = "OPTIONS";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "OPtion_ID";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.Visible = false;
            // 
            // lbl_Option
            // 
            this.lbl_Option.AutoSize = true;
            this.lbl_Option.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Option.Location = new System.Drawing.Point(6, 86);
            this.lbl_Option.Name = "lbl_Option";
            this.lbl_Option.Size = new System.Drawing.Size(62, 20);
            this.lbl_Option.TabIndex = 114;
            this.lbl_Option.Text = "Options :";
            // 
            // datagrid_Reasons
            // 
            this.datagrid_Reasons.AllowUserToAddRows = false;
            this.datagrid_Reasons.AllowUserToDeleteRows = false;
            this.datagrid_Reasons.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.datagrid_Reasons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagrid_Reasons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.datagrid_Reasons.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.datagrid_Reasons.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagrid_Reasons.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.datagrid_Reasons.ColumnHeadersHeight = 30;
            this.datagrid_Reasons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.datagrid_Reasons.Location = new System.Drawing.Point(117, 247);
            this.datagrid_Reasons.Name = "datagrid_Reasons";
            this.datagrid_Reasons.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagrid_Reasons.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.datagrid_Reasons.RowHeadersVisible = false;
            this.datagrid_Reasons.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.datagrid_Reasons.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.datagrid_Reasons.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.datagrid_Reasons.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.datagrid_Reasons.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.datagrid_Reasons.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.datagrid_Reasons.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.datagrid_Reasons.RowTemplate.Height = 25;
            this.datagrid_Reasons.Size = new System.Drawing.Size(519, 120);
            this.datagrid_Reasons.TabIndex = 147;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 243.5829F;
            this.dataGridViewTextBoxColumn1.HeaderText = "REASONS";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Reason_Id";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 117;
            this.label4.Text = "Reason Text :";
            // 
            // txt_Options
            // 
            this.txt_Options.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Options.Location = new System.Drawing.Point(117, 15);
            this.txt_Options.Multiline = true;
            this.txt_Options.Name = "txt_Options";
            this.txt_Options.Size = new System.Drawing.Size(519, 42);
            this.txt_Options.TabIndex = 118;
            // 
            // txt_Reason
            // 
            this.txt_Reason.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Reason.Location = new System.Drawing.Point(117, 196);
            this.txt_Reason.Multiline = true;
            this.txt_Reason.Name = "txt_Reason";
            this.txt_Reason.Size = new System.Drawing.Size(516, 45);
            this.txt_Reason.TabIndex = 121;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 119;
            this.label5.Text = "Reason :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 120;
            this.label6.Text = "Options Text :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Question_Reason);
            this.groupBox1.Controls.Add(this.txt_Message);
            this.groupBox1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(610, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 100);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sub Question";
            // 
            // lbl_Question_Reason
            // 
            this.lbl_Question_Reason.AutoSize = true;
            this.lbl_Question_Reason.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Question_Reason.Location = new System.Drawing.Point(15, 23);
            this.lbl_Question_Reason.Name = "lbl_Question_Reason";
            this.lbl_Question_Reason.Size = new System.Drawing.Size(68, 20);
            this.lbl_Question_Reason.TabIndex = 112;
            this.lbl_Question_Reason.Text = "Question :";
            // 
            // txt_Message
            // 
            this.txt_Message.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Message.Location = new System.Drawing.Point(117, 18);
            this.txt_Message.Multiline = true;
            this.txt_Message.Name = "txt_Message";
            this.txt_Message.Size = new System.Drawing.Size(512, 70);
            this.txt_Message.TabIndex = 113;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(668, 9);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(115, 23);
            this.radioButton2.TabIndex = 111;
            this.radioButton2.Text = "Reason/Options";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(558, 9);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(106, 23);
            this.radioButton1.TabIndex = 110;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Sub Questions";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // ddl_Sub_Task
            // 
            this.ddl_Sub_Task.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Sub_Task.FormattingEnabled = true;
            this.ddl_Sub_Task.Location = new System.Drawing.Point(55, 36);
            this.ddl_Sub_Task.Name = "ddl_Sub_Task";
            this.ddl_Sub_Task.Size = new System.Drawing.Size(253, 28);
            this.ddl_Sub_Task.TabIndex = 109;
            this.ddl_Sub_Task.SelectedIndexChanged += new System.EventHandler(this.ddl_Sub_Task_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 108;
            this.label3.Text = "Task :";
            // 
            // btn_Delete_Node
            // 
            this.btn_Delete_Node.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Delete_Node.BackgroundImage")));
            this.btn_Delete_Node.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Delete_Node.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Delete_Node.Location = new System.Drawing.Point(352, 36);
            this.btn_Delete_Node.Name = "btn_Delete_Node";
            this.btn_Delete_Node.Size = new System.Drawing.Size(35, 30);
            this.btn_Delete_Node.TabIndex = 107;
            this.btn_Delete_Node.UseVisualStyleBackColor = true;
            this.btn_Delete_Node.Click += new System.EventHandler(this.btn_Delete_Node_Click);
            // 
            // btn_Add_Node
            // 
            this.btn_Add_Node.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Add_Node.BackgroundImage")));
            this.btn_Add_Node.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Add_Node.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Add_Node.Location = new System.Drawing.Point(314, 36);
            this.btn_Add_Node.Name = "btn_Add_Node";
            this.btn_Add_Node.Size = new System.Drawing.Size(32, 28);
            this.btn_Add_Node.TabIndex = 106;
            this.btn_Add_Node.UseVisualStyleBackColor = true;
            this.btn_Add_Node.Click += new System.EventHandler(this.btn_Add_Node_Click);
            // 
            // Tree_View_Task
            // 
            this.Tree_View_Task.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tree_View_Task.HideSelection = false;
            this.Tree_View_Task.LineColor = System.Drawing.Color.White;
            this.Tree_View_Task.Location = new System.Drawing.Point(2, 69);
            this.Tree_View_Task.Name = "Tree_View_Task";
            this.Tree_View_Task.ShowNodeToolTips = true;
            this.Tree_View_Task.Size = new System.Drawing.Size(599, 492);
            this.Tree_View_Task.TabIndex = 1;
            this.Tree_View_Task.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree_View_Task_AfterSelect);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Delete";
            this.dataGridViewImageColumn1.Image = global::Ordermanagement_01.Properties.Resources.Delete;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.Width = 150;
            // 
            // Task_Confirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 623);
            this.Controls.Add(this.tabControl1);
            this.Name = "Task_Confirmation";
            this.Text = "Task_Confirmation";
            this.Load += new System.EventHandler(this.Task_Confirmation_Load);
            this.grp_OrderType.ResumeLayout(false);
            this.grp_OrderType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Comments)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datagrid_Options)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_Reasons)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_OrderType;
        private System.Windows.Forms.TextBox txt_Information;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddl_Task;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.DataGridView Grid_Comments;
        private System.Windows.Forms.Button btn_Move_Up;
        private System.Windows.Forms.Button btn_Move_Down;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView Tree_View_Task;
        private System.Windows.Forms.Button btn_Delete_Node;
        private System.Windows.Forms.Button btn_Add_Node;
        private System.Windows.Forms.ComboBox ddl_Sub_Task;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label lbl_Question_Reason;
        private System.Windows.Forms.TextBox txt_Message;
        private System.Windows.Forms.Label lbl_Option;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Options;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Reason;
        private System.Windows.Forms.DataGridView Datagrid_Options;
        private System.Windows.Forms.DataGridView datagrid_Reasons;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ddl_Yes_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewLinkColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}