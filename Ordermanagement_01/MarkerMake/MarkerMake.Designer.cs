namespace Ordermanagement_01.MarkerMake
{
    partial class MarkerMake
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
            this.tvwRightSide = new System.Windows.Forms.TreeView();
            this.btn_treeview = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlSideTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.tvwRightSide);
            this.pnlSideTree.Location = new System.Drawing.Point(10, 11);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(177, 646);
            this.pnlSideTree.TabIndex = 90;
            // 
            // tvwRightSide
            // 
            this.tvwRightSide.HideSelection = false;
            this.tvwRightSide.LineColor = System.Drawing.Color.White;
            this.tvwRightSide.Location = new System.Drawing.Point(5, 3);
            this.tvwRightSide.Name = "tvwRightSide";
            this.tvwRightSide.ShowNodeToolTips = true;
            this.tvwRightSide.Size = new System.Drawing.Size(167, 640);
            this.tvwRightSide.TabIndex = 0;
            this.tvwRightSide.UseWaitCursor = true;
            // 
            // btn_treeview
            // 
            this.btn_treeview.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_treeview.Location = new System.Drawing.Point(497, -67);
            this.btn_treeview.Name = "btn_treeview";
            this.btn_treeview.Size = new System.Drawing.Size(52, 33);
            this.btn_treeview.TabIndex = 89;
            this.btn_treeview.Text = "<<";
            this.btn_treeview.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(188, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 33);
            this.button1.TabIndex = 91;
            this.button1.Text = "Deed";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MarkerMake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 697);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlSideTree);
            this.Controls.Add(this.btn_treeview);
            this.Name = "MarkerMake";
            this.Text = "MarkerMake";
            this.Load += new System.EventHandler(this.MarkerMake_Load);
            this.pnlSideTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.TreeView tvwRightSide;
        private System.Windows.Forms.Button btn_treeview;
        private System.Windows.Forms.Button button1;
    }
}