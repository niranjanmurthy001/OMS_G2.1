namespace Ordermanagement_01.Abstractor
{
    partial class Abstract_Order_Move
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
            this.label1 = new System.Windows.Forms.Label();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddl_Order_Status_Reallocate = new System.Windows.Forms.ComboBox();
            this.btn_Allocate = new System.Windows.Forms.Button();
            this.lbl_allocated_user = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rb_Users = new System.Windows.Forms.RadioButton();
            this.Rb_Task = new System.Windows.Forms.RadioButton();
            this.txt_SearchOrdernumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Number = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Client_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATECOUNTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(162)))), ((int)(((byte)(176)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(1010, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "USER NAME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TreeView1
            // 
            this.TreeView1.AllowDrop = true;
            this.TreeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(162)))), ((int)(((byte)(176)))));
            this.TreeView1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TreeView1.Indent = 15;
            this.TreeView1.ItemHeight = 20;
            this.TreeView1.Location = new System.Drawing.Point(1010, 106);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.PathSeparator = "";
            this.TreeView1.Size = new System.Drawing.Size(243, 464);
            this.TreeView1.TabIndex = 2;
            this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // grd_order
            // 
            this.grd_order.AllowDrop = true;
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grd_order.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_order.ColumnHeadersHeight = 30;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Chk,
            this.SNo,
            this.Order_Number,
            this.Client_Name,
            this.Sub_ProcessName,
            this.Order_Type,
            this.STATECOUNTY,
            this.Date,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column1,
            this.Column2,
            this.Column3});
            this.grd_order.Location = new System.Drawing.Point(8, 86);
            this.grd_order.Name = "grd_order";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.grd_order.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_order.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.grd_order.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_order.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_order.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_order.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.RowTemplate.Height = 25;
            this.grd_order.Size = new System.Drawing.Size(943, 482);
            this.grd_order.TabIndex = 4;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.label7.Location = new System.Drawing.Point(445, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(418, 31);
            this.label7.TabIndex = 25;
            this.label7.Text = "MOVE TO OMS ALLOCATION /PROCEESS QUE";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_Header
            // 
            this.lbl_Header.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_Header.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.Black;
            this.lbl_Header.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_Header.Location = new System.Drawing.Point(12, 9);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(266, 23);
            this.lbl_Header.TabIndex = 26;
            this.lbl_Header.Text = "ABSTRACTOR RETURNED QUEUE:";
            this.lbl_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label2.Location = new System.Drawing.Point(4, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 23);
            this.label2.TabIndex = 27;
            this.label2.Text = "TASK :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ddl_Order_Status_Reallocate
            // 
            this.ddl_Order_Status_Reallocate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Order_Status_Reallocate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Order_Status_Reallocate.FormattingEnabled = true;
            this.ddl_Order_Status_Reallocate.Location = new System.Drawing.Point(74, 46);
            this.ddl_Order_Status_Reallocate.Margin = new System.Windows.Forms.Padding(4);
            this.ddl_Order_Status_Reallocate.Name = "ddl_Order_Status_Reallocate";
            this.ddl_Order_Status_Reallocate.Size = new System.Drawing.Size(172, 28);
            this.ddl_Order_Status_Reallocate.TabIndex = 28;
            // 
            // btn_Allocate
            // 
            this.btn_Allocate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Allocate.BackgroundImage = global::Ordermanagement_01.Properties.Resources.left_arrow;
            this.btn_Allocate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Allocate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Allocate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Allocate.Location = new System.Drawing.Point(957, 268);
            this.btn_Allocate.Name = "btn_Allocate";
            this.btn_Allocate.Size = new System.Drawing.Size(47, 31);
            this.btn_Allocate.TabIndex = 128;
            this.btn_Allocate.UseVisualStyleBackColor = false;
            this.btn_Allocate.Click += new System.EventHandler(this.btn_Allocate_Click);
            // 
            // lbl_allocated_user
            // 
            this.lbl_allocated_user.AutoSize = true;
            this.lbl_allocated_user.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbl_allocated_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_allocated_user.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_allocated_user.ForeColor = System.Drawing.Color.Black;
            this.lbl_allocated_user.Location = new System.Drawing.Point(1099, 43);
            this.lbl_allocated_user.Name = "lbl_allocated_user";
            this.lbl_allocated_user.Size = new System.Drawing.Size(54, 26);
            this.lbl_allocated_user.TabIndex = 130;
            this.lbl_allocated_user.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(1006, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 24);
            this.label4.TabIndex = 129;
            this.label4.Text = "Allocate To :";
            // 
            // rb_Users
            // 
            this.rb_Users.AutoSize = true;
            this.rb_Users.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Users.Location = new System.Drawing.Point(761, 40);
            this.rb_Users.Name = "rb_Users";
            this.rb_Users.Size = new System.Drawing.Size(132, 28);
            this.rb_Users.TabIndex = 132;
            this.rb_Users.TabStop = true;
            this.rb_Users.Text = "Move To Users";
            this.rb_Users.UseVisualStyleBackColor = true;
            this.rb_Users.CheckedChanged += new System.EventHandler(this.rb_Users_CheckedChanged);
            // 
            // Rb_Task
            // 
            this.Rb_Task.AutoSize = true;
            this.Rb_Task.Checked = true;
            this.Rb_Task.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rb_Task.Location = new System.Drawing.Point(630, 40);
            this.Rb_Task.Name = "Rb_Task";
            this.Rb_Task.Size = new System.Drawing.Size(125, 28);
            this.Rb_Task.TabIndex = 131;
            this.Rb_Task.TabStop = true;
            this.Rb_Task.Text = "Move To Task";
            this.Rb_Task.UseVisualStyleBackColor = true;
            this.Rb_Task.CheckedChanged += new System.EventHandler(this.Rb_Task_CheckedChanged);
            // 
            // txt_SearchOrdernumber
            // 
            this.txt_SearchOrdernumber.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SearchOrdernumber.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.txt_SearchOrdernumber.Location = new System.Drawing.Point(266, 46);
            this.txt_SearchOrdernumber.Name = "txt_SearchOrdernumber";
            this.txt_SearchOrdernumber.Size = new System.Drawing.Size(275, 28);
            this.txt_SearchOrdernumber.TabIndex = 148;
            this.txt_SearchOrdernumber.Text = "Search by order number...";
            this.txt_SearchOrdernumber.Click += new System.EventHandler(this.txt_SearchOrdernumber_Click);
            this.txt_SearchOrdernumber.TextChanged += new System.EventHandler(this.txt_SearchOrdernumber_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1016, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(237, 17);
            this.label5.TabIndex = 157;
            this.label5.Text = "Record are belongs to NA State 11000 Client";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.YellowGreen;
            this.pictureBox1.Location = new System.Drawing.Point(983, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 13);
            this.pictureBox1.TabIndex = 156;
            this.pictureBox1.TabStop = false;
            // 
            // Chk
            // 
            this.Chk.FillWeight = 20.10912F;
            this.Chk.HeaderText = "";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // SNo
            // 
            this.SNo.FillWeight = 48.73096F;
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            // 
            // Order_Number
            // 
            this.Order_Number.FillWeight = 121.86F;
            this.Order_Number.HeaderText = "ORDER NUMBER";
            this.Order_Number.Name = "Order_Number";
            this.Order_Number.ReadOnly = true;
            this.Order_Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Order_Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Client_Name
            // 
            this.Client_Name.FillWeight = 121.86F;
            this.Client_Name.HeaderText = "CLIENT";
            this.Client_Name.Name = "Client_Name";
            this.Client_Name.ReadOnly = true;
            // 
            // Sub_ProcessName
            // 
            this.Sub_ProcessName.FillWeight = 121.86F;
            this.Sub_ProcessName.HeaderText = "SUB CLIENT";
            this.Sub_ProcessName.Name = "Sub_ProcessName";
            this.Sub_ProcessName.ReadOnly = true;
            // 
            // Order_Type
            // 
            this.Order_Type.FillWeight = 121.86F;
            this.Order_Type.HeaderText = "ORDER TYPE";
            this.Order_Type.Name = "Order_Type";
            this.Order_Type.ReadOnly = true;
            // 
            // STATECOUNTY
            // 
            this.STATECOUNTY.FillWeight = 121.86F;
            this.STATECOUNTY.HeaderText = "STATE & COUNTY";
            this.STATECOUNTY.Name = "STATECOUNTY";
            this.STATECOUNTY.ReadOnly = true;
            // 
            // Date
            // 
            dataGridViewCellStyle2.Format = "MM/dd/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.Date.DefaultCellStyle = dataGridViewCellStyle2;
            this.Date.FillWeight = 121.86F;
            this.Date.HeaderText = "RECEIVED DATE";
            this.Date.Name = "Date";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Order_Progress_ID";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Order_Statusid";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Order_id";
            this.Column9.Name = "Column9";
            this.Column9.Visible = false;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Order_Type_ID";
            this.Column10.Name = "Column10";
            this.Column10.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "State_ID";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "County_Id";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Sub_Process_Id";
            this.Column3.Name = "Column3";
            this.Column3.Visible = false;
            // 
            // Abstract_Order_Move
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 580);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_SearchOrdernumber);
            this.Controls.Add(this.rb_Users);
            this.Controls.Add(this.Rb_Task);
            this.Controls.Add(this.lbl_allocated_user);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Allocate);
            this.Controls.Add(this.ddl_Order_Status_Reallocate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_Header);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.grd_order);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TreeView1);
            this.MaximizeBox = false;
            this.Name = "Abstract_Order_Move";
            this.Text = "Abstract_Order_Move";
            this.Load += new System.EventHandler(this.Abstract_Order_Move_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView TreeView1;
        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddl_Order_Status_Reallocate;
        private System.Windows.Forms.Button btn_Allocate;
        private System.Windows.Forms.Label lbl_allocated_user;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb_Users;
        private System.Windows.Forms.RadioButton Rb_Task;
        private System.Windows.Forms.TextBox txt_SearchOrdernumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewButtonColumn Order_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATECOUNTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}