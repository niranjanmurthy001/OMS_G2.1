namespace Ordermanagement_01.Abstractor
{
    partial class Abstractor_Serach
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
            this.btn_treeview = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.lbl_OrderType = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.grp_Add_det = new System.Windows.Forms.GroupBox();
            this.lbl_RecordAddedOn = new System.Windows.Forms.Label();
            this.lbl_RecordAddedBy = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grp_OrderType = new System.Windows.Forms.GroupBox();
            this.Chk_Status = new System.Windows.Forms.CheckBox();
            this.txt_Order_No = new System.Windows.Forms.TextBox();
            this.txt_Order_Type = new System.Windows.Forms.TextBox();
            this.txt_Order_Type_Abbrivation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.tree_OrderType = new System.Windows.Forms.TreeView();
            this.grp_Add_det.SuspendLayout();
            this.grp_OrderType.SuspendLayout();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_treeview
            // 
            this.btn_treeview.BackColor = System.Drawing.Color.Transparent;
            this.btn_treeview.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_treeview.Image = global::Ordermanagement_01.Properties.Resources.left;
            this.btn_treeview.Location = new System.Drawing.Point(189, 0);
            this.btn_treeview.Name = "btn_treeview";
            this.btn_treeview.Size = new System.Drawing.Size(36, 26);
            this.btn_treeview.TabIndex = 78;
            this.btn_treeview.UseVisualStyleBackColor = false;
            this.btn_treeview.Click += new System.EventHandler(this.btn_treeview_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(399, 400);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(153, 35);
            this.btn_Delete.TabIndex = 84;
            this.btn_Delete.Text = "Delete Order Type";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // lbl_OrderType
            // 
            this.lbl_OrderType.AutoSize = true;
            this.lbl_OrderType.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OrderType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_OrderType.Location = new System.Drawing.Point(345, 10);
            this.lbl_OrderType.Name = "lbl_OrderType";
            this.lbl_OrderType.Size = new System.Drawing.Size(252, 31);
            this.lbl_OrderType.TabIndex = 83;
            this.lbl_OrderType.Text = "ABSTRACTOR ORDER TYPE";
            this.lbl_OrderType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(562, 400);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(77, 35);
            this.btn_Cancel.TabIndex = 80;
            this.btn_Cancel.Text = "Clear";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // grp_Add_det
            // 
            this.grp_Add_det.Controls.Add(this.lbl_RecordAddedOn);
            this.grp_Add_det.Controls.Add(this.lbl_RecordAddedBy);
            this.grp_Add_det.Controls.Add(this.label13);
            this.grp_Add_det.Controls.Add(this.label12);
            this.grp_Add_det.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.grp_Add_det.Location = new System.Drawing.Point(201, 275);
            this.grp_Add_det.Name = "grp_Add_det";
            this.grp_Add_det.Size = new System.Drawing.Size(538, 104);
            this.grp_Add_det.TabIndex = 82;
            this.grp_Add_det.TabStop = false;
            this.grp_Add_det.Text = "ADDITIONAL INFORMATION";
            // 
            // lbl_RecordAddedOn
            // 
            this.lbl_RecordAddedOn.AutoSize = true;
            this.lbl_RecordAddedOn.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedOn.Location = new System.Drawing.Point(279, 64);
            this.lbl_RecordAddedOn.Name = "lbl_RecordAddedOn";
            this.lbl_RecordAddedOn.Size = new System.Drawing.Size(51, 20);
            this.lbl_RecordAddedOn.TabIndex = 70;
            this.lbl_RecordAddedOn.Text = "label19";
            // 
            // lbl_RecordAddedBy
            // 
            this.lbl_RecordAddedBy.AutoSize = true;
            this.lbl_RecordAddedBy.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedBy.Location = new System.Drawing.Point(279, 30);
            this.lbl_RecordAddedBy.Name = "lbl_RecordAddedBy";
            this.lbl_RecordAddedBy.Size = new System.Drawing.Size(51, 20);
            this.lbl_RecordAddedBy.TabIndex = 69;
            this.lbl_RecordAddedBy.Text = "label14";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label13.Location = new System.Drawing.Point(71, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 20);
            this.label13.TabIndex = 68;
            this.label13.Text = "Record Added On";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label12.Location = new System.Drawing.Point(71, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 20);
            this.label12.TabIndex = 67;
            this.label12.Text = "Record Added By";
            // 
            // grp_OrderType
            // 
            this.grp_OrderType.Controls.Add(this.Chk_Status);
            this.grp_OrderType.Controls.Add(this.txt_Order_No);
            this.grp_OrderType.Controls.Add(this.txt_Order_Type);
            this.grp_OrderType.Controls.Add(this.txt_Order_Type_Abbrivation);
            this.grp_OrderType.Controls.Add(this.label4);
            this.grp_OrderType.Controls.Add(this.label3);
            this.grp_OrderType.Controls.Add(this.label2);
            this.grp_OrderType.Controls.Add(this.label1);
            this.grp_OrderType.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.grp_OrderType.Location = new System.Drawing.Point(201, 55);
            this.grp_OrderType.Name = "grp_OrderType";
            this.grp_OrderType.Size = new System.Drawing.Size(538, 209);
            this.grp_OrderType.TabIndex = 81;
            this.grp_OrderType.TabStop = false;
            this.grp_OrderType.Text = "ORDER TYPE DETAILS";
            // 
            // Chk_Status
            // 
            this.Chk_Status.AutoSize = true;
            this.Chk_Status.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Chk_Status.Location = new System.Drawing.Point(283, 163);
            this.Chk_Status.Name = "Chk_Status";
            this.Chk_Status.Size = new System.Drawing.Size(98, 24);
            this.Chk_Status.TabIndex = 4;
            this.Chk_Status.Text = "[ChkDefault]";
            this.Chk_Status.UseVisualStyleBackColor = true;
            // 
            // txt_Order_No
            // 
            this.txt_Order_No.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_Order_No.Location = new System.Drawing.Point(283, 36);
            this.txt_Order_No.Name = "txt_Order_No";
            this.txt_Order_No.Size = new System.Drawing.Size(164, 25);
            this.txt_Order_No.TabIndex = 1;
            // 
            // txt_Order_Type
            // 
            this.txt_Order_Type.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_Order_Type.Location = new System.Drawing.Point(283, 74);
            this.txt_Order_Type.Name = "txt_Order_Type";
            this.txt_Order_Type.Size = new System.Drawing.Size(164, 25);
            this.txt_Order_Type.TabIndex = 2;
            // 
            // txt_Order_Type_Abbrivation
            // 
            this.txt_Order_Type_Abbrivation.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_Order_Type_Abbrivation.Location = new System.Drawing.Point(283, 114);
            this.txt_Order_Type_Abbrivation.Name = "txt_Order_Type_Abbrivation";
            this.txt_Order_Type_Abbrivation.Size = new System.Drawing.Size(164, 25);
            this.txt_Order_Type_Abbrivation.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label4.Location = new System.Drawing.Point(71, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 83;
            this.label4.Text = "Status:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label3.Location = new System.Drawing.Point(71, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 20);
            this.label3.TabIndex = 82;
            this.label3.Text = "Order Type Abbreviation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label2.Location = new System.Drawing.Point(71, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 81;
            this.label2.Text = "Order Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label1.Location = new System.Drawing.Point(71, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 80;
            this.label1.Text = "Order Type No:";
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Save.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(255, 400);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(134, 35);
            this.btn_Save.TabIndex = 79;
            this.btn_Save.Text = "Add Order Type";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.tree_OrderType);
            this.pnlSideTree.Location = new System.Drawing.Point(-1, 0);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(190, 450);
            this.pnlSideTree.TabIndex = 85;
            // 
            // tree_OrderType
            // 
            this.tree_OrderType.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.tree_OrderType.Location = new System.Drawing.Point(0, 0);
            this.tree_OrderType.Name = "tree_OrderType";
            this.tree_OrderType.Size = new System.Drawing.Size(190, 450);
            this.tree_OrderType.TabIndex = 72;
            this.tree_OrderType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_OrderType_AfterSelect);
            // 
            // Abstractor_Serach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 451);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.lbl_OrderType);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.grp_Add_det);
            this.Controls.Add(this.grp_OrderType);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_treeview);
            this.Name = "Abstractor_Serach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abstractor Search Type";
            this.Load += new System.EventHandler(this.Abstractor_Serach_Load);
            this.grp_Add_det.ResumeLayout(false);
            this.grp_Add_det.PerformLayout();
            this.grp_OrderType.ResumeLayout(false);
            this.grp_OrderType.PerformLayout();
            this.pnlSideTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_treeview;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Label lbl_OrderType;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox grp_Add_det;
        private System.Windows.Forms.Label lbl_RecordAddedOn;
        private System.Windows.Forms.Label lbl_RecordAddedBy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grp_OrderType;
        private System.Windows.Forms.CheckBox Chk_Status;
        private System.Windows.Forms.TextBox txt_Order_No;
        private System.Windows.Forms.TextBox txt_Order_Type;
        private System.Windows.Forms.TextBox txt_Order_Type_Abbrivation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.TreeView tree_OrderType;
    }
}