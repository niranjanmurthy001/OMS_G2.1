namespace Ordermanagement_01.Masters
{
    partial class Employee_Search_Order_Source
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
            this.label11 = new System.Windows.Forms.Label();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.txt_Order_Source = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.txt_Serach_source = new System.Windows.Forms.TextBox();
            this.tree_order_source = new System.Windows.Forms.TreeView();
            this.lbl_OrderType = new System.Windows.Forms.Label();
            this.lbl_RecordAddedOn = new System.Windows.Forms.Label();
            this.lbl_RecordAddedBy = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_Recorded_Added_By = new System.Windows.Forms.Label();
            this.lbl_Recorded_Date = new System.Windows.Forms.Label();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(542, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 20);
            this.label11.TabIndex = 235;
            this.label11.Text = "*";
            // 
            // btn_clear
            // 
            this.btn_clear.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_clear.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_clear.Location = new System.Drawing.Point(450, 255);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(63, 30);
            this.btn_clear.TabIndex = 4;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Submit.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Submit.Location = new System.Drawing.Point(257, 255);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(80, 30);
            this.btn_Submit.TabIndex = 2;
            this.btn_Submit.Text = "Add";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // txt_Order_Source
            // 
            this.txt_Order_Source.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Order_Source.Location = new System.Drawing.Point(350, 101);
            this.txt_Order_Source.Name = "txt_Order_Source";
            this.txt_Order_Source.Size = new System.Drawing.Size(187, 25);
            this.txt_Order_Source.TabIndex = 1;
            this.txt_Order_Source.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Order_Source_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(214, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 20);
            this.label10.TabIndex = 231;
            this.label10.Text = "Order Source:";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Delete.Location = new System.Drawing.Point(353, 255);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(80, 30);
            this.btn_Delete.TabIndex = 3;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.txt_Serach_source);
            this.pnlSideTree.Controls.Add(this.tree_order_source);
            this.pnlSideTree.Location = new System.Drawing.Point(2, 1);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(189, 372);
            this.pnlSideTree.TabIndex = 237;
            // 
            // txt_Serach_source
            // 
            this.txt_Serach_source.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_Serach_source.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Serach_source.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txt_Serach_source.Location = new System.Drawing.Point(0, 1);
            this.txt_Serach_source.Name = "txt_Serach_source";
            this.txt_Serach_source.Size = new System.Drawing.Size(187, 25);
            this.txt_Serach_source.TabIndex = 6;
            this.txt_Serach_source.Text = "Search Order source...";
            this.txt_Serach_source.TextChanged += new System.EventHandler(this.txt_Serach_source_TextChanged);
            this.txt_Serach_source.MouseEnter += new System.EventHandler(this.txt_Serach_source_MouseEnter);
            // 
            // tree_order_source
            // 
            this.tree_order_source.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tree_order_source.Location = new System.Drawing.Point(-1, 28);
            this.tree_order_source.Name = "tree_order_source";
            this.tree_order_source.Size = new System.Drawing.Size(188, 341);
            this.tree_order_source.TabIndex = 5;
            this.tree_order_source.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_order_source_AfterSelect);
            // 
            // lbl_OrderType
            // 
            this.lbl_OrderType.AutoSize = true;
            this.lbl_OrderType.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OrderType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_OrderType.Location = new System.Drawing.Point(248, 9);
            this.lbl_OrderType.Name = "lbl_OrderType";
            this.lbl_OrderType.Size = new System.Drawing.Size(255, 31);
            this.lbl_OrderType.TabIndex = 238;
            this.lbl_OrderType.Text = "EMPLOYEE ORDER SOURCE";
            this.lbl_OrderType.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_RecordAddedOn
            // 
            this.lbl_RecordAddedOn.AutoSize = true;
            this.lbl_RecordAddedOn.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedOn.Location = new System.Drawing.Point(350, 188);
            this.lbl_RecordAddedOn.Name = "lbl_RecordAddedOn";
            this.lbl_RecordAddedOn.Size = new System.Drawing.Size(0, 20);
            this.lbl_RecordAddedOn.TabIndex = 242;
            // 
            // lbl_RecordAddedBy
            // 
            this.lbl_RecordAddedBy.AutoSize = true;
            this.lbl_RecordAddedBy.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedBy.Location = new System.Drawing.Point(350, 154);
            this.lbl_RecordAddedBy.Name = "lbl_RecordAddedBy";
            this.lbl_RecordAddedBy.Size = new System.Drawing.Size(0, 20);
            this.lbl_RecordAddedBy.TabIndex = 241;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label13.Location = new System.Drawing.Point(214, 188);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 20);
            this.label13.TabIndex = 240;
            this.label13.Text = "Record Added On";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label12.Location = new System.Drawing.Point(214, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 20);
            this.label12.TabIndex = 239;
            this.label12.Text = "Record Added By";
            // 
            // lbl_Recorded_Added_By
            // 
            this.lbl_Recorded_Added_By.AutoSize = true;
            this.lbl_Recorded_Added_By.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_Recorded_Added_By.Location = new System.Drawing.Point(346, 154);
            this.lbl_Recorded_Added_By.Name = "lbl_Recorded_Added_By";
            this.lbl_Recorded_Added_By.Size = new System.Drawing.Size(65, 20);
            this.lbl_Recorded_Added_By.TabIndex = 243;
            this.lbl_Recorded_Added_By.Text = "Added By";
            // 
            // lbl_Recorded_Date
            // 
            this.lbl_Recorded_Date.AutoSize = true;
            this.lbl_Recorded_Date.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_Recorded_Date.Location = new System.Drawing.Point(346, 189);
            this.lbl_Recorded_Date.Name = "lbl_Recorded_Date";
            this.lbl_Recorded_Date.Size = new System.Drawing.Size(79, 20);
            this.lbl_Recorded_Date.TabIndex = 244;
            this.lbl_Recorded_Date.Text = "Added Date";
            // 
            // Employee_Search_Order_Source
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 374);
            this.Controls.Add(this.lbl_Recorded_Date);
            this.Controls.Add(this.lbl_Recorded_Added_By);
            this.Controls.Add(this.lbl_RecordAddedOn);
            this.Controls.Add(this.lbl_RecordAddedBy);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbl_OrderType);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txt_Order_Source);
            this.Controls.Add(this.label10);
            this.MaximizeBox = false;
            this.Name = "Employee_Search_Order_Source";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee_Search_Order_Source";
            this.Load += new System.EventHandler(this.Employee_Search_Order_Source_Load);
            this.pnlSideTree.ResumeLayout(false);
            this.pnlSideTree.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.TextBox txt_Order_Source;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.TextBox txt_Serach_source;
        private System.Windows.Forms.TreeView tree_order_source;
        private System.Windows.Forms.Label lbl_OrderType;
        private System.Windows.Forms.Label lbl_RecordAddedOn;
        private System.Windows.Forms.Label lbl_RecordAddedBy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_Recorded_Date;
        public System.Windows.Forms.Label lbl_Recorded_Added_By;
    }
}