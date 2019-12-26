namespace Ordermanagement_01
{
    partial class Employee_Error_Entry_Old
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
            this.txt_ErrorCmt = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cbo_ErrorDes = new System.Windows.Forms.ComboBox();
            this.cbo_ErrorCatogery = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btn_ErrorSub = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.grd_Error = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ERROR_Task = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ERRORUSER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Cbo_Task = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Lbl_User = new System.Windows.Forms.Label();
            this.ddl_User = new System.Windows.Forms.ComboBox();
            this.chk_Username = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Error)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_ErrorCmt
            // 
            this.txt_ErrorCmt.BackColor = System.Drawing.SystemColors.Window;
            this.txt_ErrorCmt.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_ErrorCmt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_ErrorCmt.Location = new System.Drawing.Point(462, 217);
            this.txt_ErrorCmt.Multiline = true;
            this.txt_ErrorCmt.Name = "txt_ErrorCmt";
            this.txt_ErrorCmt.Size = new System.Drawing.Size(381, 50);
            this.txt_ErrorCmt.TabIndex = 3;
            this.txt_ErrorCmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ErrorCmt_KeyDown);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label27.Location = new System.Drawing.Point(231, 217);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(78, 20);
            this.label27.TabIndex = 77;
            this.label27.Text = "Comments :";
            // 
            // cbo_ErrorDes
            // 
            this.cbo_ErrorDes.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_ErrorDes.FormattingEnabled = true;
            this.cbo_ErrorDes.Location = new System.Drawing.Point(463, 96);
            this.cbo_ErrorDes.Name = "cbo_ErrorDes";
            this.cbo_ErrorDes.Size = new System.Drawing.Size(287, 28);
            this.cbo_ErrorDes.TabIndex = 2;
            this.cbo_ErrorDes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_ErrorDes_KeyDown);
            // 
            // cbo_ErrorCatogery
            // 
            this.cbo_ErrorCatogery.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_ErrorCatogery.FormattingEnabled = true;
            this.cbo_ErrorCatogery.Location = new System.Drawing.Point(463, 55);
            this.cbo_ErrorCatogery.Name = "cbo_ErrorCatogery";
            this.cbo_ErrorCatogery.Size = new System.Drawing.Size(287, 28);
            this.cbo_ErrorCatogery.TabIndex = 1;
            this.cbo_ErrorCatogery.SelectedIndexChanged += new System.EventHandler(this.cbo_ErrorCatogery_SelectedIndexChanged);
            this.cbo_ErrorCatogery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_ErrorCatogery_KeyDown);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label26.Location = new System.Drawing.Point(231, 100);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(116, 20);
            this.label26.TabIndex = 75;
            this.label26.Text = "Error Description :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(231, 59);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(103, 20);
            this.label25.TabIndex = 74;
            this.label25.Text = "Error Catogery :";
            // 
            // btn_ErrorSub
            // 
            this.btn_ErrorSub.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ErrorSub.Location = new System.Drawing.Point(360, 294);
            this.btn_ErrorSub.Name = "btn_ErrorSub";
            this.btn_ErrorSub.Size = new System.Drawing.Size(95, 30);
            this.btn_ErrorSub.TabIndex = 4;
            this.btn_ErrorSub.Text = "Submit";
            this.btn_ErrorSub.UseVisualStyleBackColor = true;
            this.btn_ErrorSub.Visible = false;
            this.btn_ErrorSub.Click += new System.EventHandler(this.btn_ErrorSub_Click);
            this.btn_ErrorSub.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_ErrorSub_KeyDown);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Location = new System.Drawing.Point(488, 294);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(95, 30);
            this.btn_Clear.TabIndex = 5;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Visible = false;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // grd_Error
            // 
            this.grd_Error.AllowUserToAddRows = false;
            this.grd_Error.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Error.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Error.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_Error.ColumnHeadersHeight = 29;
            this.grd_Error.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.User_Name,
            this.Column8,
            this.Column5,
            this.Column6,
            this.Column7,
            this.ERROR_Task,
            this.ERRORUSER});
            this.grd_Error.Location = new System.Drawing.Point(2, 339);
            this.grd_Error.Name = "grd_Error";
            this.grd_Error.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_Error.RowHeadersVisible = false;
            this.grd_Error.Size = new System.Drawing.Size(1026, 173);
            this.grd_Error.TabIndex = 82;
            this.grd_Error.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Error_CellClick);
            // 
            // SNo
            // 
            this.SNo.FillWeight = 29.68796F;
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.Width = 50;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 67.40617F;
            this.Column1.HeaderText = "ERROR TYPE";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 67.40617F;
            this.Column2.HeaderText = "DESCRIPTION";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 217.0294F;
            this.Column3.HeaderText = "COMMENTS";
            this.Column3.Name = "Column3";
            this.Column3.Width = 300;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 33.58048F;
            this.Column4.HeaderText = "TASK";
            this.Column4.Name = "Column4";
            this.Column4.Width = 75;
            // 
            // User_Name
            // 
            this.User_Name.FillWeight = 133.8527F;
            this.User_Name.HeaderText = "USER NAME";
            this.User_Name.Name = "User_Name";
            // 
            // Column8
            // 
            this.Column8.FillWeight = 37.01456F;
            this.Column8.HeaderText = "REMOVE";
            this.Column8.Name = "Column8";
            this.Column8.Width = 75;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Userid";
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Taskid";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "ErrorInfo_ID";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            // 
            // ERROR_Task
            // 
            this.ERROR_Task.HeaderText = "ERROR TASK";
            this.ERROR_Task.Name = "ERROR_Task";
            // 
            // ERRORUSER
            // 
            this.ERRORUSER.HeaderText = "UPDATED BY";
            this.ERRORUSER.Name = "ERRORUSER";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(354, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 31);
            this.label1.TabIndex = 83;
            this.label1.Text = "EMPLOYEE ERROR ENTRY";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label2.Location = new System.Drawing.Point(231, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 84;
            this.label2.Text = "Task :";
            // 
            // Cbo_Task
            // 
            this.Cbo_Task.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_Task.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Cbo_Task.FormattingEnabled = true;
            this.Cbo_Task.Location = new System.Drawing.Point(463, 140);
            this.Cbo_Task.Name = "Cbo_Task";
            this.Cbo_Task.Size = new System.Drawing.Size(155, 28);
            this.Cbo_Task.TabIndex = 85;
            this.Cbo_Task.SelectedIndexChanged += new System.EventHandler(this.Cbo_Task_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label3.Location = new System.Drawing.Point(231, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 86;
            this.label3.Text = "User :";
            // 
            // Lbl_User
            // 
            this.Lbl_User.AutoSize = true;
            this.Lbl_User.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_User.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Lbl_User.Location = new System.Drawing.Point(463, 180);
            this.Lbl_User.Name = "Lbl_User";
            this.Lbl_User.Size = new System.Drawing.Size(2, 22);
            this.Lbl_User.TabIndex = 87;
            this.Lbl_User.UseMnemonic = false;
            // 
            // ddl_User
            // 
            this.ddl_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_User.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.ddl_User.FormattingEnabled = true;
            this.ddl_User.Location = new System.Drawing.Point(682, 174);
            this.ddl_User.Name = "ddl_User";
            this.ddl_User.Size = new System.Drawing.Size(214, 28);
            this.ddl_User.TabIndex = 88;
            this.ddl_User.SelectedIndexChanged += new System.EventHandler(this.ddl_User_SelectedIndexChanged);
            // 
            // chk_Username
            // 
            this.chk_Username.AutoSize = true;
            this.chk_Username.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Username.Location = new System.Drawing.Point(682, 142);
            this.chk_Username.Name = "chk_Username";
            this.chk_Username.Size = new System.Drawing.Size(181, 23);
            this.chk_Username.TabIndex = 89;
            this.chk_Username.Text = "Check for User/Vendor Name";
            this.chk_Username.UseVisualStyleBackColor = true;
            this.chk_Username.Visible = false;
            this.chk_Username.CheckedChanged += new System.EventHandler(this.chk_Username_CheckedChanged);
            // 
            // Employee_Error_Entry_Old
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 509);
            this.Controls.Add(this.chk_Username);
            this.Controls.Add(this.ddl_User);
            this.Controls.Add(this.Lbl_User);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cbo_Task);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grd_Error);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_ErrorSub);
            this.Controls.Add(this.txt_ErrorCmt);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.cbo_ErrorDes);
            this.Controls.Add(this.cbo_ErrorCatogery);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Name = "Employee_Error_Entry_Old";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee ERROR Entry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Employee_Error_Entry_FormClosed);
            this.Load += new System.EventHandler(this.Employee_Error_Entry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Error)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ErrorCmt;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cbo_ErrorDes;
        private System.Windows.Forms.ComboBox cbo_ErrorCatogery;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btn_ErrorSub;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.DataGridView grd_Error;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Cbo_Task;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Lbl_User;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_Name;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERROR_Task;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERRORUSER;
        private System.Windows.Forms.ComboBox ddl_User;
        private System.Windows.Forms.CheckBox chk_Username;
    }
}