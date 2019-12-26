namespace Ordermanagement_01.Gen_Forms
{
    partial class StateWise_UserNameAndPassword
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
            this.tree_StateName = new System.Windows.Forms.TreeView();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.grp_State = new System.Windows.Forms.GroupBox();
            this.cmb_State = new System.Windows.Forms.ComboBox();
            this.txt_Comments = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_link = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_treeview = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grp_State.SuspendLayout();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // tree_StateName
            // 
            this.tree_StateName.Location = new System.Drawing.Point(0, 0);
            this.tree_StateName.Name = "tree_StateName";
            this.tree_StateName.Size = new System.Drawing.Size(190, 470);
            this.tree_StateName.TabIndex = 96;
            this.tree_StateName.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_StateName_AfterSelect);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(322, 332);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(113, 33);
            this.btn_Delete.TabIndex = 7;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(492, 332);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(113, 33);
            this.btn_Cancel.TabIndex = 8;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_Submit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Submit.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Submit.Location = new System.Drawing.Point(152, 332);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(113, 33);
            this.btn_Submit.TabIndex = 6;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = false;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // grp_State
            // 
            this.grp_State.Controls.Add(this.label8);
            this.grp_State.Controls.Add(this.label7);
            this.grp_State.Controls.Add(this.label2);
            this.grp_State.Controls.Add(this.label14);
            this.grp_State.Controls.Add(this.label45);
            this.grp_State.Controls.Add(this.btn_Delete);
            this.grp_State.Controls.Add(this.cmb_State);
            this.grp_State.Controls.Add(this.txt_Comments);
            this.grp_State.Controls.Add(this.label6);
            this.grp_State.Controls.Add(this.btn_Cancel);
            this.grp_State.Controls.Add(this.txt_link);
            this.grp_State.Controls.Add(this.label5);
            this.grp_State.Controls.Add(this.btn_Submit);
            this.grp_State.Controls.Add(this.txt_Password);
            this.grp_State.Controls.Add(this.label4);
            this.grp_State.Controls.Add(this.txt_UserName);
            this.grp_State.Controls.Add(this.label3);
            this.grp_State.Controls.Add(this.label1);
            this.grp_State.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.grp_State.Location = new System.Drawing.Point(208, 58);
            this.grp_State.Name = "grp_State";
            this.grp_State.Size = new System.Drawing.Size(748, 393);
            this.grp_State.TabIndex = 92;
            this.grp_State.TabStop = false;
            this.grp_State.Text = "STATE COUNTY DETAILS";
            // 
            // cmb_State
            // 
            this.cmb_State.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_State.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.cmb_State.FormattingEnabled = true;
            this.cmb_State.Location = new System.Drawing.Point(283, 30);
            this.cmb_State.Name = "cmb_State";
            this.cmb_State.Size = new System.Drawing.Size(164, 28);
            this.cmb_State.TabIndex = 1;
            // 
            // txt_Comments
            // 
            this.txt_Comments.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_Comments.Location = new System.Drawing.Point(283, 205);
            this.txt_Comments.Multiline = true;
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.Size = new System.Drawing.Size(438, 75);
            this.txt_Comments.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(71, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.TabIndex = 88;
            this.label6.Text = "Comments:";
            // 
            // txt_link
            // 
            this.txt_link.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_link.Location = new System.Drawing.Point(283, 151);
            this.txt_link.Multiline = true;
            this.txt_link.Name = "txt_link";
            this.txt_link.Size = new System.Drawing.Size(438, 43);
            this.txt_link.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(71, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 20);
            this.label5.TabIndex = 86;
            this.label5.Text = "Link:";
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_Password.Location = new System.Drawing.Point(283, 115);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(164, 25);
            this.txt_Password.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(71, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 84;
            this.label4.Text = "Password:";
            // 
            // txt_UserName
            // 
            this.txt_UserName.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_UserName.Location = new System.Drawing.Point(283, 75);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(164, 25);
            this.txt_UserName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 83;
            this.label3.Text = "User Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 80;
            this.label1.Text = "State:";
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.tree_StateName);
            this.pnlSideTree.Location = new System.Drawing.Point(0, 0);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(190, 469);
            this.pnlSideTree.TabIndex = 99;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_title.Location = new System.Drawing.Point(485, 13);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(186, 31);
            this.lbl_title.TabIndex = 100;
            this.lbl_title.Text = "STATE WISE ENTRY";
            // 
            // btn_treeview
            // 
            this.btn_treeview.BackColor = System.Drawing.Color.Transparent;
            this.btn_treeview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_treeview.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_treeview.ForeColor = System.Drawing.Color.White;
            this.btn_treeview.Image = global::Ordermanagement_01.Properties.Resources.left;
            this.btn_treeview.Location = new System.Drawing.Point(190, 0);
            this.btn_treeview.Name = "btn_treeview";
            this.btn_treeview.Size = new System.Drawing.Size(36, 26);
            this.btn_treeview.TabIndex = 101;
            this.btn_treeview.UseVisualStyleBackColor = false;
            this.btn_treeview.Click += new System.EventHandler(this.btn_treeview_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(542, 295);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(200, 19);
            this.label45.TabIndex = 234;
            this.label45.Text = "(Fields with * Mark are Mandatory)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(449, 33);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 20);
            this.label14.TabIndex = 235;
            this.label14.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(449, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 20);
            this.label2.TabIndex = 236;
            this.label2.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(447, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 20);
            this.label7.TabIndex = 237;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(724, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 20);
            this.label8.TabIndex = 238;
            this.label8.Text = "*";
            // 
            // StateWise_UserNameAndPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 463);
            this.Controls.Add(this.btn_treeview);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.grp_State);
            this.MaximizeBox = false;
            this.Name = "StateWise_UserNameAndPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StateWise_UserNameAndPassword";
            this.Load += new System.EventHandler(this.StateWise_UserNameAndPassword_Load);
            this.grp_State.ResumeLayout(false);
            this.grp_State.PerformLayout();
            this.pnlSideTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tree_StateName;
        private System.Windows.Forms.GroupBox grp_State;
        private System.Windows.Forms.ComboBox cmb_State;
        private System.Windows.Forms.TextBox txt_Comments;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_link;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_treeview;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;

    }
}