namespace Ordermanagement_01.MarkerMaker
{
    partial class MarkerMaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerMaker));
            this.pnlSideTree = new System.Windows.Forms.Panel();
            this.tvwRightSide = new System.Windows.Forms.TreeView();
            this.pdfViewerControl1 = new Bytescout.PDFViewer.PDFViewerControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ddl_Marker = new System.Windows.Forms.ComboBox();
            this.Txt_Extract = new System.Windows.Forms.TextBox();
            this.rb_Selectionmode = new System.Windows.Forms.RadioButton();
            this.rb_View_Mode = new System.Windows.Forms.RadioButton();
            this.btn_preview = new System.Windows.Forms.Button();
            this.lbl_Value = new System.Windows.Forms.Label();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.btn_Add_Node = new System.Windows.Forms.Button();
            this.btn_Delete_Node = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.pnlSideTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSideTree
            // 
            this.pnlSideTree.Controls.Add(this.tvwRightSide);
            this.pnlSideTree.Location = new System.Drawing.Point(12, 31);
            this.pnlSideTree.Name = "pnlSideTree";
            this.pnlSideTree.Size = new System.Drawing.Size(231, 609);
            this.pnlSideTree.TabIndex = 89;
            // 
            // tvwRightSide
            // 
            this.tvwRightSide.HideSelection = false;
            this.tvwRightSide.LineColor = System.Drawing.Color.White;
            this.tvwRightSide.Location = new System.Drawing.Point(5, 1);
            this.tvwRightSide.Name = "tvwRightSide";
            this.tvwRightSide.ShowNodeToolTips = true;
            this.tvwRightSide.Size = new System.Drawing.Size(223, 608);
            this.tvwRightSide.TabIndex = 0;
            this.tvwRightSide.UseWaitCursor = true;
            this.tvwRightSide.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwRightSide_AfterSelect);
            this.tvwRightSide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwRightSide_KeyDown);
            // 
            // pdfViewerControl1
            // 
            this.pdfViewerControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pdfViewerControl1.Location = new System.Drawing.Point(249, 31);
            this.pdfViewerControl1.MouseMode = Bytescout.PDFViewer.MouseMode.Hand;
            this.pdfViewerControl1.Name = "pdfViewerControl1";
            this.pdfViewerControl1.RegistrationKey = null;
            this.pdfViewerControl1.RegistrationName = null;
            this.pdfViewerControl1.Size = new System.Drawing.Size(915, 428);
            this.pdfViewerControl1.TabIndex = 91;
            this.pdfViewerControl1.SelectionRectangleChanged += new System.EventHandler(this.pdfViewerControl1_SelectionRectangleChanged);
            this.pdfViewerControl1.Load += new System.EventHandler(this.pdfViewerControl1_Load);
            this.pdfViewerControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pdfViewerControl1_DragDrop);
            this.pdfViewerControl1.DragLeave += new System.EventHandler(this.pdfViewerControl1_DragLeave);
            this.pdfViewerControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pdfViewerControl1_KeyDown);
            this.pdfViewerControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pdfViewerControl1_MouseClick);
            this.pdfViewerControl1.MouseLeave += new System.EventHandler(this.pdfViewerControl1_MouseLeave);
            this.pdfViewerControl1.MouseHover += new System.EventHandler(this.pdfViewerControl1_MouseHover);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ddl_Marker
            // 
            this.ddl_Marker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Marker.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Marker.FormattingEnabled = true;
            this.ddl_Marker.Items.AddRange(new object[] {
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
            this.ddl_Marker.Location = new System.Drawing.Point(12, 3);
            this.ddl_Marker.Name = "ddl_Marker";
            this.ddl_Marker.Size = new System.Drawing.Size(177, 27);
            this.ddl_Marker.TabIndex = 96;
            this.ddl_Marker.SelectedIndexChanged += new System.EventHandler(this.ddl_Marker_SelectedIndexChanged);
            // 
            // Txt_Extract
            // 
            this.Txt_Extract.Enabled = false;
            this.Txt_Extract.Location = new System.Drawing.Point(249, 465);
            this.Txt_Extract.Multiline = true;
            this.Txt_Extract.Name = "Txt_Extract";
            this.Txt_Extract.Size = new System.Drawing.Size(838, 37);
            this.Txt_Extract.TabIndex = 97;
            // 
            // rb_Selectionmode
            // 
            this.rb_Selectionmode.AutoSize = true;
            this.rb_Selectionmode.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Selectionmode.Location = new System.Drawing.Point(397, 2);
            this.rb_Selectionmode.Name = "rb_Selectionmode";
            this.rb_Selectionmode.Size = new System.Drawing.Size(132, 23);
            this.rb_Selectionmode.TabIndex = 99;
            this.rb_Selectionmode.TabStop = true;
            this.rb_Selectionmode.Text = "Selection Mode";
            this.rb_Selectionmode.UseVisualStyleBackColor = true;
            this.rb_Selectionmode.CheckedChanged += new System.EventHandler(this.rb_Selectionmode_CheckedChanged);
            // 
            // rb_View_Mode
            // 
            this.rb_View_Mode.AutoSize = true;
            this.rb_View_Mode.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_View_Mode.Location = new System.Drawing.Point(544, 2);
            this.rb_View_Mode.Name = "rb_View_Mode";
            this.rb_View_Mode.Size = new System.Drawing.Size(103, 23);
            this.rb_View_Mode.TabIndex = 100;
            this.rb_View_Mode.TabStop = true;
            this.rb_View_Mode.Text = "View Mode";
            this.rb_View_Mode.UseVisualStyleBackColor = true;
            this.rb_View_Mode.CheckedChanged += new System.EventHandler(this.rb_View_Mode_CheckedChanged);
            // 
            // btn_preview
            // 
            this.btn_preview.Location = new System.Drawing.Point(983, 2);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(104, 28);
            this.btn_preview.TabIndex = 102;
            this.btn_preview.Text = "Preview";
            this.btn_preview.UseVisualStyleBackColor = true;
            this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
            // 
            // lbl_Value
            // 
            this.lbl_Value.AutoSize = true;
            this.lbl_Value.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Value.Location = new System.Drawing.Point(252, 519);
            this.lbl_Value.Name = "lbl_Value";
            this.lbl_Value.Size = new System.Drawing.Size(49, 19);
            this.lbl_Value.TabIndex = 103;
            this.lbl_Value.Text = "label1";
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(249, 54);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(915, 405);
            this.axAcroPDF1.TabIndex = 101;
            // 
            // btn_Add_Node
            // 
            this.btn_Add_Node.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Add_Node.BackgroundImage")));
            this.btn_Add_Node.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Add_Node.Location = new System.Drawing.Point(192, 3);
            this.btn_Add_Node.Name = "btn_Add_Node";
            this.btn_Add_Node.Size = new System.Drawing.Size(32, 27);
            this.btn_Add_Node.TabIndex = 104;
            this.btn_Add_Node.UseVisualStyleBackColor = true;
            this.btn_Add_Node.Click += new System.EventHandler(this.btn_Add_Node_Click);
            // 
            // btn_Delete_Node
            // 
            this.btn_Delete_Node.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Delete_Node.BackgroundImage")));
            this.btn_Delete_Node.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Delete_Node.Location = new System.Drawing.Point(229, 3);
            this.btn_Delete_Node.Name = "btn_Delete_Node";
            this.btn_Delete_Node.Size = new System.Drawing.Size(36, 27);
            this.btn_Delete_Node.TabIndex = 105;
            this.btn_Delete_Node.UseVisualStyleBackColor = true;
            this.btn_Delete_Node.Click += new System.EventHandler(this.btn_Delete_Node_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(1089, 465);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 32);
            this.btn_Delete.TabIndex = 106;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // MarkerMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 645);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Delete_Node);
            this.Controls.Add(this.btn_Add_Node);
            this.Controls.Add(this.lbl_Value);
            this.Controls.Add(this.btn_preview);
            this.Controls.Add(this.axAcroPDF1);
            this.Controls.Add(this.rb_View_Mode);
            this.Controls.Add(this.rb_Selectionmode);
            this.Controls.Add(this.Txt_Extract);
            this.Controls.Add(this.ddl_Marker);
            this.Controls.Add(this.pdfViewerControl1);
            this.Controls.Add(this.pnlSideTree);
            this.MaximizeBox = false;
            this.Name = "MarkerMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MarkerMaker";
            this.Load += new System.EventHandler(this.MarkerMaker_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MarkerMaker_KeyDown);
            this.pnlSideTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideTree;
        private System.Windows.Forms.TreeView tvwRightSide;
        private Bytescout.PDFViewer.PDFViewerControl pdfViewerControl1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox ddl_Marker;
        private System.Windows.Forms.TextBox Txt_Extract;
        private System.Windows.Forms.RadioButton rb_Selectionmode;
        private System.Windows.Forms.RadioButton rb_View_Mode;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        private System.Windows.Forms.Button btn_preview;
        private System.Windows.Forms.Label lbl_Value;
        private System.Windows.Forms.Button btn_Add_Node;
        private System.Windows.Forms.Button btn_Delete_Node;
        private System.Windows.Forms.Button btn_Delete;
    }
}