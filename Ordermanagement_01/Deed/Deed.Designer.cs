namespace Ordermanagement_01.Deed
{
    partial class Deed
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
            this.lbl_Deed = new System.Windows.Forms.Label();
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.tree_Deed = new System.Windows.Forms.TreeView();
            this.btn_treeview = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txt_DeedInfo = new System.Windows.Forms.TextBox();
            this.lbl_Info = new System.Windows.Forms.Label();
            this.lbl_Marker = new System.Windows.Forms.Label();
            this.comb_Deed = new System.Windows.Forms.ComboBox();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Deed
            // 
            this.lbl_Deed.AutoSize = true;
            this.lbl_Deed.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Deed.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_Deed.Location = new System.Drawing.Point(345, 45);
            this.lbl_Deed.Name = "lbl_Deed";
            this.lbl_Deed.Size = new System.Drawing.Size(64, 22);
            this.lbl_Deed.TabIndex = 86;
            this.lbl_Deed.Text = "DEED";
            this.lbl_Deed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.tree_Deed);
            this.pnlSideTree.Location = new System.Drawing.Point(0, 0);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(165, 309);
            this.pnlSideTree.TabIndex = 88;
            // 
            // tree_Deed
            // 
            this.tree_Deed.Location = new System.Drawing.Point(0, 0);
            this.tree_Deed.Name = "tree_Deed";
            this.tree_Deed.Size = new System.Drawing.Size(165, 309);
            this.tree_Deed.TabIndex = 85;
            this.tree_Deed.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_Deed_AfterSelect);
            // 
            // btn_treeview
            // 
            this.btn_treeview.BackColor = System.Drawing.Color.Transparent;
            this.btn_treeview.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_treeview.Image = global::Ordermanagement_01.Properties.Resources.left;
            this.btn_treeview.Location = new System.Drawing.Point(165, 0);
            this.btn_treeview.Name = "btn_treeview";
            this.btn_treeview.Size = new System.Drawing.Size(36, 26);
            this.btn_treeview.TabIndex = 89;
            this.btn_treeview.UseVisualStyleBackColor = false;
            this.btn_treeview.Click += new System.EventHandler(this.btn_treeview_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Save.Location = new System.Drawing.Point(225, 240);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(83, 30);
            this.btn_Save.TabIndex = 90;
            this.btn_Save.Text = "Insert";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Delete.Location = new System.Drawing.Point(335, 240);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(89, 30);
            this.btn_Delete.TabIndex = 91;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Cancel.Location = new System.Drawing.Point(440, 240);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(81, 30);
            this.btn_Cancel.TabIndex = 92;
            this.btn_Cancel.Text = "Clear";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // txt_DeedInfo
            // 
            this.txt_DeedInfo.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DeedInfo.Location = new System.Drawing.Point(410, 160);
            this.txt_DeedInfo.Name = "txt_DeedInfo";
            this.txt_DeedInfo.Size = new System.Drawing.Size(164, 26);
            this.txt_DeedInfo.TabIndex = 94;
            // 
            // lbl_Info
            // 
            this.lbl_Info.AutoSize = true;
            this.lbl_Info.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Info.Location = new System.Drawing.Point(190, 160);
            this.lbl_Info.Name = "lbl_Info";
            this.lbl_Info.Size = new System.Drawing.Size(81, 19);
            this.lbl_Info.TabIndex = 96;
            this.lbl_Info.Text = "Information:";
            // 
            // lbl_Marker
            // 
            this.lbl_Marker.AutoSize = true;
            this.lbl_Marker.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Marker.Location = new System.Drawing.Point(190, 105);
            this.lbl_Marker.Name = "lbl_Marker";
            this.lbl_Marker.Size = new System.Drawing.Size(105, 19);
            this.lbl_Marker.TabIndex = 95;
            this.lbl_Marker.Text = "Marker Master:";
            // 
            // comb_Deed
            // 
            this.comb_Deed.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comb_Deed.FormattingEnabled = true;
            this.comb_Deed.Items.AddRange(new object[] {
            "Deed",
            "Mortgage",
            "Judgment",
            "Tax",
            "Legal Description",
            "Order Information",
            "Additional Information"});
            this.comb_Deed.Location = new System.Drawing.Point(410, 100);
            this.comb_Deed.Name = "comb_Deed";
            this.comb_Deed.Size = new System.Drawing.Size(164, 27);
            this.comb_Deed.TabIndex = 97;
            this.comb_Deed.SelectedIndexChanged += new System.EventHandler(this.comb_Deed_SelectedIndexChanged);
            // 
            // Deed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 307);
            this.Controls.Add(this.comb_Deed);
            this.Controls.Add(this.txt_DeedInfo);
            this.Controls.Add(this.lbl_Info);
            this.Controls.Add(this.lbl_Marker);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_treeview);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.lbl_Deed);
            this.MaximizeBox = false;
            this.Name = "Deed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Deed";
            this.Load += new System.EventHandler(this.Deed_Load);
            this.pnlSideTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Deed;
        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.TreeView tree_Deed;
        private System.Windows.Forms.Button btn_treeview;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txt_DeedInfo;
        private System.Windows.Forms.Label lbl_Info;
        private System.Windows.Forms.Label lbl_Marker;
        private System.Windows.Forms.ComboBox comb_Deed;
    }
}