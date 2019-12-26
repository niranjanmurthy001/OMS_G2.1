namespace Ordermanagement_01
{
    partial class Create_UserRole
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
            this.grp_UserRole = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_Role_No = new System.Windows.Forms.TextBox();
            this.txt_Role_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grp_Add_det = new System.Windows.Forms.GroupBox();
            this.lbl_RecordAddedOn = new System.Windows.Forms.Label();
            this.lbl_RecordAddedBy = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.lbl_UserRole = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_treeview = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.grp_UserRole.SuspendLayout();
            this.grp_Add_det.SuspendLayout();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_UserRole
            // 
            this.grp_UserRole.Controls.Add(this.label3);
            this.grp_UserRole.Controls.Add(this.label21);
            this.grp_UserRole.Controls.Add(this.txt_Role_No);
            this.grp_UserRole.Controls.Add(this.txt_Role_Name);
            this.grp_UserRole.Controls.Add(this.label2);
            this.grp_UserRole.Controls.Add(this.label1);
            this.grp_UserRole.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_UserRole.Location = new System.Drawing.Point(224, 75);
            this.grp_UserRole.Name = "grp_UserRole";
            this.grp_UserRole.Size = new System.Drawing.Size(551, 125);
            this.grp_UserRole.TabIndex = 74;
            this.grp_UserRole.TabStop = false;
            this.grp_UserRole.Text = "USER ROLE DETAILS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(453, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 227;
            this.label3.Text = "*";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(451, 36);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 20);
            this.label21.TabIndex = 226;
            this.label21.Text = "*";
            // 
            // txt_Role_No
            // 
            this.txt_Role_No.Enabled = false;
            this.txt_Role_No.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Role_No.Location = new System.Drawing.Point(224, 36);
            this.txt_Role_No.Name = "txt_Role_No";
            this.txt_Role_No.Size = new System.Drawing.Size(223, 25);
            this.txt_Role_No.TabIndex = 1;
            this.txt_Role_No.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Role_ID_KeyDown);
            this.txt_Role_No.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Role_No_KeyPress);
            // 
            // txt_Role_Name
            // 
            this.txt_Role_Name.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Role_Name.Location = new System.Drawing.Point(224, 81);
            this.txt_Role_Name.Name = "txt_Role_Name";
            this.txt_Role_Name.Size = new System.Drawing.Size(223, 25);
            this.txt_Role_Name.TabIndex = 2;
            this.txt_Role_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Role_KeyDown);
            this.txt_Role_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Role_Name_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(71, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 81;
            this.label2.Text = "Role Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 80;
            this.label1.Text = "Role No:";
            // 
            // grp_Add_det
            // 
            this.grp_Add_det.Controls.Add(this.lbl_RecordAddedOn);
            this.grp_Add_det.Controls.Add(this.lbl_RecordAddedBy);
            this.grp_Add_det.Controls.Add(this.label13);
            this.grp_Add_det.Controls.Add(this.label12);
            this.grp_Add_det.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Add_det.Location = new System.Drawing.Point(224, 215);
            this.grp_Add_det.Name = "grp_Add_det";
            this.grp_Add_det.Size = new System.Drawing.Size(551, 104);
            this.grp_Add_det.TabIndex = 84;
            this.grp_Add_det.TabStop = false;
            this.grp_Add_det.Text = "ADDITIONAL INFORMATION";
            // 
            // lbl_RecordAddedOn
            // 
            this.lbl_RecordAddedOn.AutoSize = true;
            this.lbl_RecordAddedOn.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RecordAddedOn.Location = new System.Drawing.Point(279, 64);
            this.lbl_RecordAddedOn.Name = "lbl_RecordAddedOn";
            this.lbl_RecordAddedOn.Size = new System.Drawing.Size(51, 20);
            this.lbl_RecordAddedOn.TabIndex = 70;
            this.lbl_RecordAddedOn.Text = "label19";
            // 
            // lbl_RecordAddedBy
            // 
            this.lbl_RecordAddedBy.AutoSize = true;
            this.lbl_RecordAddedBy.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RecordAddedBy.Location = new System.Drawing.Point(279, 30);
            this.lbl_RecordAddedBy.Name = "lbl_RecordAddedBy";
            this.lbl_RecordAddedBy.Size = new System.Drawing.Size(51, 20);
            this.lbl_RecordAddedBy.TabIndex = 69;
            this.lbl_RecordAddedBy.Text = "label14";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(71, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 20);
            this.label13.TabIndex = 68;
            this.label13.Text = "Record Added On";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(71, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 20);
            this.label12.TabIndex = 67;
            this.label12.Text = "Record Added By";
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(213, 398);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // lbl_UserRole
            // 
            this.lbl_UserRole.AutoSize = true;
            this.lbl_UserRole.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserRole.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_UserRole.Location = new System.Drawing.Point(463, 0);
            this.lbl_UserRole.Name = "lbl_UserRole";
            this.lbl_UserRole.Size = new System.Drawing.Size(112, 31);
            this.lbl_UserRole.TabIndex = 84;
            this.lbl_UserRole.Text = "USER ROLE";
            this.lbl_UserRole.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Cancel.Location = new System.Drawing.Point(600, 345);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(92, 35);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Clear";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Save.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Save.Location = new System.Drawing.Point(343, 345);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(97, 35);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "Add ";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.treeView1);
            this.pnlSideTree.Location = new System.Drawing.Point(0, 0);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(216, 401);
            this.pnlSideTree.TabIndex = 86;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Delete.Location = new System.Drawing.Point(472, 345);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(99, 35);
            this.btn_Delete.TabIndex = 4;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_treeview
            // 
            this.btn_treeview.BackColor = System.Drawing.Color.Transparent;
            this.btn_treeview.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_treeview.Image = global::Ordermanagement_01.Properties.Resources.left;
            this.btn_treeview.Location = new System.Drawing.Point(218, 0);
            this.btn_treeview.Name = "btn_treeview";
            this.btn_treeview.Size = new System.Drawing.Size(36, 26);
            this.btn_treeview.TabIndex = 7;
            this.btn_treeview.UseVisualStyleBackColor = false;
            this.btn_treeview.Visible = false;
            this.btn_treeview.Click += new System.EventHandler(this.btn_treeview_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(577, 53);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(200, 19);
            this.label45.TabIndex = 237;
            this.label45.Text = "(Fields with * Mark are Mandatory)";
            // 
            // Create_UserRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 397);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_treeview);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.lbl_UserRole);
            this.Controls.Add(this.grp_Add_det);
            this.Controls.Add(this.grp_UserRole);
            this.MaximizeBox = false;
            this.Name = "Create_UserRole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create_UserRole";
            this.Load += new System.EventHandler(this.Create_UserRole_Load);
            this.grp_UserRole.ResumeLayout(false);
            this.grp_UserRole.PerformLayout();
            this.grp_Add_det.ResumeLayout(false);
            this.grp_Add_det.PerformLayout();
            this.pnlSideTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_UserRole;
        private System.Windows.Forms.TextBox txt_Role_No;
        private System.Windows.Forms.TextBox txt_Role_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grp_Add_det;
        private System.Windows.Forms.Label lbl_RecordAddedOn;
        private System.Windows.Forms.Label lbl_RecordAddedBy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lbl_UserRole;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.Button btn_treeview;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label45;
    }
}