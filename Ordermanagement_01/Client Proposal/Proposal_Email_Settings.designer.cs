namespace Ordermanagement_01.Client_Proposal
{
    partial class Proposal_Email_Settings
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
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.tvw_Proposal_Email = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_RecordAddedOn = new System.Windows.Forms.Label();
            this.lbl_RecordAddedBy = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grp_Branch_det = new System.Windows.Forms.GroupBox();
            this.chk_Password = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Email_Id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ddl_Proposal_From = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.grp_Branch_det.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Branch.Location = new System.Drawing.Point(304, 9);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(268, 31);
            this.lbl_Branch.TabIndex = 75;
            this.lbl_Branch.Text = "PROPOSAL EMAIL SETTINGS";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(418, 375);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(106, 33);
            this.btn_Delete.TabIndex = 4;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(556, 375);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 33);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Clear";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Save.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(281, 375);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(111, 33);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "Submit";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // tvw_Proposal_Email
            // 
            this.tvw_Proposal_Email.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvw_Proposal_Email.Location = new System.Drawing.Point(2, 4);
            this.tvw_Proposal_Email.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvw_Proposal_Email.Name = "tvw_Proposal_Email";
            this.tvw_Proposal_Email.Size = new System.Drawing.Size(194, 466);
            this.tvw_Proposal_Email.TabIndex = 237;
            this.tvw_Proposal_Email.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_Proposal_Email_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_RecordAddedOn);
            this.groupBox1.Controls.Add(this.lbl_RecordAddedBy);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(205, 245);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(504, 99);
            this.groupBox1.TabIndex = 236;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ADDITIONAL INFO";
            // 
            // lbl_RecordAddedOn
            // 
            this.lbl_RecordAddedOn.AutoSize = true;
            this.lbl_RecordAddedOn.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedOn.Location = new System.Drawing.Point(178, 64);
            this.lbl_RecordAddedOn.Name = "lbl_RecordAddedOn";
            this.lbl_RecordAddedOn.Size = new System.Drawing.Size(0, 20);
            this.lbl_RecordAddedOn.TabIndex = 74;
            // 
            // lbl_RecordAddedBy
            // 
            this.lbl_RecordAddedBy.AutoSize = true;
            this.lbl_RecordAddedBy.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedBy.Location = new System.Drawing.Point(178, 25);
            this.lbl_RecordAddedBy.Name = "lbl_RecordAddedBy";
            this.lbl_RecordAddedBy.Size = new System.Drawing.Size(0, 20);
            this.lbl_RecordAddedBy.TabIndex = 73;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label13.Location = new System.Drawing.Point(11, 65);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 20);
            this.label13.TabIndex = 72;
            this.label13.Text = "Record Added On";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label12.Location = new System.Drawing.Point(11, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 20);
            this.label12.TabIndex = 71;
            this.label12.Text = "Record Added By";
            // 
            // grp_Branch_det
            // 
            this.grp_Branch_det.Controls.Add(this.ddl_Proposal_From);
            this.grp_Branch_det.Controls.Add(this.label1);
            this.grp_Branch_det.Controls.Add(this.chk_Password);
            this.grp_Branch_det.Controls.Add(this.label5);
            this.grp_Branch_det.Controls.Add(this.label4);
            this.grp_Branch_det.Controls.Add(this.txt_Password);
            this.grp_Branch_det.Controls.Add(this.label3);
            this.grp_Branch_det.Controls.Add(this.txt_Email_Id);
            this.grp_Branch_det.Controls.Add(this.label2);
            this.grp_Branch_det.Controls.Add(this.label45);
            this.grp_Branch_det.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Branch_det.Location = new System.Drawing.Point(205, 61);
            this.grp_Branch_det.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grp_Branch_det.Name = "grp_Branch_det";
            this.grp_Branch_det.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grp_Branch_det.Size = new System.Drawing.Size(504, 180);
            this.grp_Branch_det.TabIndex = 235;
            this.grp_Branch_det.TabStop = false;
            this.grp_Branch_det.Text = "EMAIL SETTING DETAILS";
            // 
            // chk_Password
            // 
            this.chk_Password.AutoSize = true;
            this.chk_Password.Location = new System.Drawing.Point(269, 145);
            this.chk_Password.Name = "chk_Password";
            this.chk_Password.Size = new System.Drawing.Size(119, 24);
            this.chk_Password.TabIndex = 254;
            this.chk_Password.Text = "Show Password";
            this.chk_Password.UseVisualStyleBackColor = true;
            this.chk_Password.CheckedChanged += new System.EventHandler(this.chk_Password_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(374, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 20);
            this.label5.TabIndex = 253;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(373, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 252;
            this.label4.Text = "*";
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Password.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Password.Location = new System.Drawing.Point(157, 102);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(210, 25);
            this.txt_Password.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 249;
            this.label3.Text = "Password:";
            // 
            // txt_Email_Id
            // 
            this.txt_Email_Id.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Email_Id.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Email_Id.Location = new System.Drawing.Point(157, 71);
            this.txt_Email_Id.Name = "txt_Email_Id";
            this.txt_Email_Id.Size = new System.Drawing.Size(210, 25);
            this.txt_Email_Id.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 246;
            this.label2.Text = "Email Id:";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(298, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(200, 19);
            this.label45.TabIndex = 244;
            this.label45.Text = "(Fields with * Mark are Mandatory)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tvw_Proposal_Email);
            this.panel1.Location = new System.Drawing.Point(3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 474);
            this.panel1.TabIndex = 254;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 255;
            this.label1.Text = "Proposal For:";
            // 
            // ddl_Proposal_From
            // 
            this.ddl_Proposal_From.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Proposal_From.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.ddl_Proposal_From.FormattingEnabled = true;
            this.ddl_Proposal_From.Location = new System.Drawing.Point(157, 34);
            this.ddl_Proposal_From.Name = "ddl_Proposal_From";
            this.ddl_Proposal_From.Size = new System.Drawing.Size(210, 28);
            this.ddl_Proposal_From.TabIndex = 256;
            // 
            // Proposal_Email_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 474);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grp_Branch_det);
            this.Controls.Add(this.lbl_Branch);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Proposal_Email_Settings";
            this.Text = "Proposal_Email_Settings";
            this.Load += new System.EventHandler(this.Proposal_Email_Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grp_Branch_det.ResumeLayout(false);
            this.grp_Branch_det.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TreeView tvw_Proposal_Email;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_RecordAddedOn;
        private System.Windows.Forms.Label lbl_RecordAddedBy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grp_Branch_det;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Email_Id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chk_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddl_Proposal_From;
    }
}