namespace Ordermanagement_01.Masters
{
    partial class Order_Document_List
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_ErrorInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_DocumentList = new System.Windows.Forms.TextBox();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.Grd_DocumentList = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_SearchDocument = new System.Windows.Forms.TextBox();
            this.lbl_DocumentListID = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Grd_DocumentList)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_ErrorInfo
            // 
            this.lbl_ErrorInfo.AutoSize = true;
            this.lbl_ErrorInfo.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ErrorInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_ErrorInfo.Location = new System.Drawing.Point(213, 7);
            this.lbl_ErrorInfo.Name = "lbl_ErrorInfo";
            this.lbl_ErrorInfo.Size = new System.Drawing.Size(165, 31);
            this.lbl_ErrorInfo.TabIndex = 100;
            this.lbl_ErrorInfo.Text = "DOCUMENT LIST";
            this.lbl_ErrorInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 103;
            this.label1.Text = "Document List Name :";
            // 
            // txt_DocumentList
            // 
            this.txt_DocumentList.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DocumentList.Location = new System.Drawing.Point(157, 51);
            this.txt_DocumentList.Name = "txt_DocumentList";
            this.txt_DocumentList.Size = new System.Drawing.Size(189, 25);
            this.txt_DocumentList.TabIndex = 1;
            this.txt_DocumentList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DocumentList_KeyPress);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Submit.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Submit.Location = new System.Drawing.Point(401, 49);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(78, 30);
            this.btn_Submit.TabIndex = 2;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(499, 49);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(62, 30);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Clear";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // Grd_DocumentList
            // 
            this.Grd_DocumentList.AllowUserToAddRows = false;
            this.Grd_DocumentList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Grd_DocumentList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grd_DocumentList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_DocumentList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grd_DocumentList.ColumnHeadersHeight = 35;
            this.Grd_DocumentList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grd_DocumentList.DefaultCellStyle = dataGridViewCellStyle2;
            this.Grd_DocumentList.Location = new System.Drawing.Point(12, 132);
            this.Grd_DocumentList.Name = "Grd_DocumentList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_DocumentList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Grd_DocumentList.RowHeadersVisible = false;
            this.Grd_DocumentList.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Grd_DocumentList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.Grd_DocumentList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grd_DocumentList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Grd_DocumentList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.Grd_DocumentList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.Grd_DocumentList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grd_DocumentList.RowTemplate.Height = 25;
            this.Grd_DocumentList.ShowCellToolTips = false;
            this.Grd_DocumentList.Size = new System.Drawing.Size(672, 347);
            this.Grd_DocumentList.TabIndex = 6;
            this.Grd_DocumentList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grd_DocumentList_CellClick);
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Document_ListID";
            this.Column5.Name = "Column5";
            this.Column5.Visible = false;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 30.45686F;
            this.Column1.HeaderText = "S.No";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 271.6155F;
            this.Column2.HeaderText = "DOCUMENT LIST";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 55.27132F;
            this.Column3.HeaderText = "VIEW/EDIT";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.Text = "";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 42.65634F;
            this.Column4.HeaderText = "DELETE";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(259, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 108;
            this.label2.Text = "Search By Name:";
            // 
            // txt_SearchDocument
            // 
            this.txt_SearchDocument.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SearchDocument.Location = new System.Drawing.Point(382, 92);
            this.txt_SearchDocument.Name = "txt_SearchDocument";
            this.txt_SearchDocument.Size = new System.Drawing.Size(302, 25);
            this.txt_SearchDocument.TabIndex = 4;
            this.txt_SearchDocument.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_SearchDocument_MouseClick);
            this.txt_SearchDocument.TextChanged += new System.EventHandler(this.txt_SearchDocument_TextChanged);
            this.txt_SearchDocument.Enter += new System.EventHandler(this.txt_SearchDocument_Enter);
            this.txt_SearchDocument.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_SearchDocument_KeyPress);
            this.txt_SearchDocument.Leave += new System.EventHandler(this.txt_SearchDocument_Leave);
            // 
            // lbl_DocumentListID
            // 
            this.lbl_DocumentListID.AutoSize = true;
            this.lbl_DocumentListID.Location = new System.Drawing.Point(384, 56);
            this.lbl_DocumentListID.Name = "lbl_DocumentListID";
            this.lbl_DocumentListID.Size = new System.Drawing.Size(0, 13);
            this.lbl_DocumentListID.TabIndex = 110;
            this.lbl_DocumentListID.Visible = false;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(12, 12);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(37, 36);
            this.btn_Refresh.TabIndex = 5;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(12, 96);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(200, 19);
            this.label45.TabIndex = 234;
            this.label45.Text = "(Fields with * Mark are Mandatory)";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(354, 54);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 20);
            this.label21.TabIndex = 235;
            this.label21.Text = "*";
            // 
            // Order_Document_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 491);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.lbl_DocumentListID);
            this.Controls.Add(this.txt_SearchDocument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Grd_DocumentList);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txt_DocumentList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_ErrorInfo);
            this.MaximizeBox = false;
            this.Name = "Order_Document_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order_Document_List";
            this.Load += new System.EventHandler(this.Order_Document_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grd_DocumentList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_ErrorInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_DocumentList;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.DataGridView Grd_DocumentList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_SearchDocument;
        private System.Windows.Forms.Label lbl_DocumentListID;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
    }
}