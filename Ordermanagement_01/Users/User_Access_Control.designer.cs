namespace Ordermanagement_01
{
    partial class User_Access_Control
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.grd_UserAccess = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.tree_UserAccess = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_UserRole = new System.Windows.Forms.ComboBox();
            this.btn_ImportExcel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ddl_EmployeeName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grd_UserAccess)).BeginInit();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(242, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 24);
            this.label4.TabIndex = 27;
            this.label4.Text = "User Name :";
            // 
            // grd_UserAccess
            // 
            this.grd_UserAccess.AllowUserToAddRows = false;
            this.grd_UserAccess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_UserAccess.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_UserAccess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_UserAccess.ColumnHeadersHeight = 35;
            this.grd_UserAccess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.grd_UserAccess.Location = new System.Drawing.Point(208, 137);
            this.grd_UserAccess.Name = "grd_UserAccess";
            this.grd_UserAccess.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_UserAccess.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_UserAccess.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grd_UserAccess.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_UserAccess.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.grd_UserAccess.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_UserAccess.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_UserAccess.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_UserAccess.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_UserAccess.RowTemplate.Height = 24;
            this.grd_UserAccess.Size = new System.Drawing.Size(435, 544);
            this.grd_UserAccess.TabIndex = 29;
            this.grd_UserAccess.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_UserAccess_CellContentClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.NullValue = false;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FillWeight = 5.076157F;
            this.Column1.HeaderText = "SELECT";
            this.Column1.Name = "Column1";
            this.Column1.Width = 122;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.FillWeight = 194.9238F;
            this.Column2.HeaderText = "USER CONTROLS";
            this.Column2.Name = "Column2";
            this.Column2.Width = 311;
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.txt_UserName);
            this.pnlSideTree.Controls.Add(this.tree_UserAccess);
            this.pnlSideTree.Location = new System.Drawing.Point(0, 0);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(190, 681);
            this.pnlSideTree.TabIndex = 75;
            // 
            // txt_UserName
            // 
            this.txt_UserName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_UserName.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_UserName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txt_UserName.Location = new System.Drawing.Point(1, 1);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(188, 25);
            this.txt_UserName.TabIndex = 70;
            this.txt_UserName.Text = "Search User name...";
            this.txt_UserName.TextChanged += new System.EventHandler(this.txt_employeeName_TextChanged);
            this.txt_UserName.Enter += new System.EventHandler(this.txt_UserName_Enter);
            this.txt_UserName.MouseEnter += new System.EventHandler(this.txt_UserName_MouseEnter);
            // 
            // tree_UserAccess
            // 
            this.tree_UserAccess.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.tree_UserAccess.Location = new System.Drawing.Point(0, 26);
            this.tree_UserAccess.Name = "tree_UserAccess";
            this.tree_UserAccess.Size = new System.Drawing.Size(190, 651);
            this.tree_UserAccess.TabIndex = 69;
            this.tree_UserAccess.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_UserAccess_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(242, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 24);
            this.label1.TabIndex = 79;
            this.label1.Text = "User Role :";
            // 
            // cbo_UserRole
            // 
            this.cbo_UserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_UserRole.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_UserRole.FormattingEnabled = true;
            this.cbo_UserRole.Items.AddRange(new object[] {
            "SELECT",
            "SEARCHER",
            "TYPER",
            "UPLOADER",
            "IMPORTER",
            "ASSIGNER",
            "ADMIN"});
            this.cbo_UserRole.Location = new System.Drawing.Point(370, 97);
            this.cbo_UserRole.Name = "cbo_UserRole";
            this.cbo_UserRole.Size = new System.Drawing.Size(195, 28);
            this.cbo_UserRole.TabIndex = 2;
            this.cbo_UserRole.SelectedIndexChanged += new System.EventHandler(this.cbo_UserRole_SelectedIndexChanged);
            // 
            // btn_ImportExcel
            // 
            this.btn_ImportExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_ImportExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ImportExcel.BackgroundImage = global::Ordermanagement_01.Properties.Resources.MS_Office_2007_Beta_2_Excel;
            this.btn_ImportExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ImportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ImportExcel.Location = new System.Drawing.Point(592, 7);
            this.btn_ImportExcel.Name = "btn_ImportExcel";
            this.btn_ImportExcel.Size = new System.Drawing.Size(40, 40);
            this.btn_ImportExcel.TabIndex = 3;
            this.btn_ImportExcel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_ImportExcel.UseVisualStyleBackColor = false;
            this.btn_ImportExcel.Click += new System.EventHandler(this.btn_ImportExcel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.label3.Location = new System.Drawing.Point(307, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 31);
            this.label3.TabIndex = 82;
            this.label3.Text = "USER ACCESS CONTROL";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ddl_EmployeeName
            // 
            this.ddl_EmployeeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_EmployeeName.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_EmployeeName.FormattingEnabled = true;
            this.ddl_EmployeeName.Location = new System.Drawing.Point(369, 58);
            this.ddl_EmployeeName.Name = "ddl_EmployeeName";
            this.ddl_EmployeeName.Size = new System.Drawing.Size(196, 28);
            this.ddl_EmployeeName.TabIndex = 83;
            this.ddl_EmployeeName.SelectedIndexChanged += new System.EventHandler(this.ddl_EmployeeName_SelectedIndexChanged);
            // 
            // User_Access_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 679);
            this.Controls.Add(this.ddl_EmployeeName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_ImportExcel);
            this.Controls.Add(this.cbo_UserRole);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.grd_UserAccess);
            this.Controls.Add(this.label4);
            this.Name = "User_Access_Control";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User_Access_Control";
            this.Load += new System.EventHandler(this.User_Access_Control_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_UserAccess)).EndInit();
            this.pnlSideTree.ResumeLayout(false);
            this.pnlSideTree.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grd_UserAccess;
        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.TreeView tree_UserAccess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_UserRole;
        private System.Windows.Forms.Button btn_ImportExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddl_EmployeeName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TextBox txt_UserName;
    }
}