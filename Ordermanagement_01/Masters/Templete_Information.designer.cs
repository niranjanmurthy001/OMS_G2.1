namespace Ordermanagement_01.Masters
{
    partial class Templete_Information
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
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btn_treeview = new System.Windows.Forms.Button();
            this.lbl_Master = new System.Windows.Forms.Label();
            this.lbl_Fields = new System.Windows.Forms.Label();
            this.Cbo_Master = new System.Windows.Forms.ComboBox();
            this.txt_Fields = new System.Windows.Forms.TextBox();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lbl_Template = new System.Windows.Forms.Label();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.treeView1);
            this.pnlSideTree.Location = new System.Drawing.Point(0, 0);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(190, 404);
            this.pnlSideTree.TabIndex = 76;
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(190, 404);
            this.treeView1.TabIndex = 67;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // btn_treeview
            // 
            this.btn_treeview.BackColor = System.Drawing.Color.Transparent;
            this.btn_treeview.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_treeview.Image = global::Ordermanagement_01.Properties.Resources.left;
            this.btn_treeview.Location = new System.Drawing.Point(189, 0);
            this.btn_treeview.Name = "btn_treeview";
            this.btn_treeview.Size = new System.Drawing.Size(36, 26);
            this.btn_treeview.TabIndex = 75;
            this.btn_treeview.UseVisualStyleBackColor = false;
            this.btn_treeview.Click += new System.EventHandler(this.btn_treeview_Click);
            this.btn_treeview.MouseEnter += new System.EventHandler(this.btn_treeview_MouseEnter);
            // 
            // lbl_Master
            // 
            this.lbl_Master.AutoSize = true;
            this.lbl_Master.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Master.Location = new System.Drawing.Point(265, 100);
            this.lbl_Master.Name = "lbl_Master";
            this.lbl_Master.Size = new System.Drawing.Size(53, 20);
            this.lbl_Master.TabIndex = 77;
            this.lbl_Master.Text = "Master:";
            // 
            // lbl_Fields
            // 
            this.lbl_Fields.AutoSize = true;
            this.lbl_Fields.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Fields.Location = new System.Drawing.Point(265, 175);
            this.lbl_Fields.Name = "lbl_Fields";
            this.lbl_Fields.Size = new System.Drawing.Size(45, 20);
            this.lbl_Fields.TabIndex = 78;
            this.lbl_Fields.Text = "Fields:";
            // 
            // Cbo_Master
            // 
            this.Cbo_Master.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbo_Master.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cbo_Master.FormattingEnabled = true;
            this.Cbo_Master.Items.AddRange(new object[] {
            "SELECT",
            "Deed",
            "Mortgage",
            "Mortgage Sub Document",
            "Judgment",
            "Judgment Sub Document",
            "Total Tax",
            "Tax",
            "Assessment",
            "Legal Description",
            "Order Information"});
            this.Cbo_Master.Location = new System.Drawing.Point(465, 95);
            this.Cbo_Master.Name = "Cbo_Master";
            this.Cbo_Master.Size = new System.Drawing.Size(184, 28);
            this.Cbo_Master.TabIndex = 79;
            // 
            // txt_Fields
            // 
            this.txt_Fields.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Fields.Location = new System.Drawing.Point(465, 172);
            this.txt_Fields.Name = "txt_Fields";
            this.txt_Fields.Size = new System.Drawing.Size(184, 25);
            this.txt_Fields.TabIndex = 80;
            // 
            // btn_Submit
            // 
            this.btn_Submit.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Submit.Location = new System.Drawing.Point(260, 290);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(110, 35);
            this.btn_Submit.TabIndex = 81;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(420, 290);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(104, 35);
            this.btn_Delete.TabIndex = 82;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(580, 290);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(105, 35);
            this.btn_Cancel.TabIndex = 83;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbl_Template
            // 
            this.lbl_Template.AutoSize = true;
            this.lbl_Template.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Template.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_Template.Location = new System.Drawing.Point(341, 23);
            this.lbl_Template.Name = "lbl_Template";
            this.lbl_Template.Size = new System.Drawing.Size(248, 31);
            this.lbl_Template.TabIndex = 84;
            this.lbl_Template.Text = "TEMPLATE INFORMATION";
            // 
            // Templete_Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 402);
            this.Controls.Add(this.lbl_Template);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txt_Fields);
            this.Controls.Add(this.Cbo_Master);
            this.Controls.Add(this.lbl_Fields);
            this.Controls.Add(this.lbl_Master);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.btn_treeview);
            this.MaximizeBox = false;
            this.Name = "Templete_Information";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Templete_Information";
            this.Load += new System.EventHandler(this.Templete_Information_Load);
            this.pnlSideTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btn_treeview;
        private System.Windows.Forms.Label lbl_Master;
        private System.Windows.Forms.Label lbl_Fields;
        private System.Windows.Forms.ComboBox Cbo_Master;
        private System.Windows.Forms.TextBox txt_Fields;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label lbl_Template;
    }
}