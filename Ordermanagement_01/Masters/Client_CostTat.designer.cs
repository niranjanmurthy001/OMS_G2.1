namespace Ordermanagement_01.Masters
{
    partial class Client_CostTat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Abstitle = new System.Windows.Forms.Label();
            this.lbl_title_Abs = new System.Windows.Forms.Label();
            this.lbl_CLient_Name = new System.Windows.Forms.Label();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_SampleFormat = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.grd_Client_cost = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Export = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Client_cost)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Abstitle
            // 
            this.lbl_Abstitle.AutoSize = true;
            this.lbl_Abstitle.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Abstitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Abstitle.Location = new System.Drawing.Point(380, 20);
            this.lbl_Abstitle.Name = "lbl_Abstitle";
            this.lbl_Abstitle.Size = new System.Drawing.Size(331, 31);
            this.lbl_Abstitle.TabIndex = 16;
            this.lbl_Abstitle.Text = "IMPORT CLIENT ORDER COST  INFO";
            this.lbl_Abstitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_title_Abs
            // 
            this.lbl_title_Abs.AutoSize = true;
            this.lbl_title_Abs.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title_Abs.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_title_Abs.Location = new System.Drawing.Point(12, 51);
            this.lbl_title_Abs.Name = "lbl_title_Abs";
            this.lbl_title_Abs.Size = new System.Drawing.Size(114, 24);
            this.lbl_title_Abs.TabIndex = 36;
            this.lbl_title_Abs.Text = "CLIENT NAME:";
            this.lbl_title_Abs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_title_Abs.Visible = false;
            // 
            // lbl_CLient_Name
            // 
            this.lbl_CLient_Name.AutoSize = true;
            this.lbl_CLient_Name.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CLient_Name.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_CLient_Name.Location = new System.Drawing.Point(146, 51);
            this.lbl_CLient_Name.Name = "lbl_CLient_Name";
            this.lbl_CLient_Name.Size = new System.Drawing.Size(86, 24);
            this.lbl_CLient_Name.TabIndex = 37;
            this.lbl_CLient_Name.Text = "LBL_CLIENT";
            this.lbl_CLient_Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_CLient_Name.Visible = false;
            // 
            // btn_upload
            // 
            this.btn_upload.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_upload.Location = new System.Drawing.Point(16, 78);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(119, 32);
            this.btn_upload.TabIndex = 1;
            this.btn_upload.Text = "Upload Excel";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // btn_SampleFormat
            // 
            this.btn_SampleFormat.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_SampleFormat.Location = new System.Drawing.Point(672, 75);
            this.btn_SampleFormat.Name = "btn_SampleFormat";
            this.btn_SampleFormat.Size = new System.Drawing.Size(124, 35);
            this.btn_SampleFormat.TabIndex = 2;
            this.btn_SampleFormat.Text = "Sample Format";
            this.btn_SampleFormat.UseVisualStyleBackColor = true;
            this.btn_SampleFormat.Click += new System.EventHandler(this.btn_SampleFormat_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Import.Location = new System.Drawing.Point(1031, 75);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(90, 35);
            this.btn_Import.TabIndex = 4;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // grd_Client_cost
            // 
            this.grd_Client_cost.AllowUserToAddRows = false;
            this.grd_Client_cost.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Client_cost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Client_cost.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grd_Client_cost.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Client_cost.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_Client_cost.ColumnHeadersHeight = 30;
            this.grd_Client_cost.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Client_cost.DefaultCellStyle = dataGridViewCellStyle2;
            this.grd_Client_cost.Location = new System.Drawing.Point(12, 125);
            this.grd_Client_cost.Name = "grd_Client_cost";
            this.grd_Client_cost.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Client_cost.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grd_Client_cost.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Client_cost.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_Client_cost.RowTemplate.Height = 25;
            this.grd_Client_cost.Size = new System.Drawing.Size(1116, 525);
            this.grd_Client_cost.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "State";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "County";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "COS";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "2WS";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "FS";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "30YS";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "40YS";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "UPS";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "DOCR";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "COMMENTS";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "State_id";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "County_id";
            this.Column12.Name = "Column12";
            this.Column12.Visible = false;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Error_Row";
            this.Column13.Name = "Column13";
            this.Column13.Visible = false;
            // 
            // btn_Export
            // 
            this.btn_Export.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Export.Location = new System.Drawing.Point(802, 75);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(223, 35);
            this.btn_Export.TabIndex = 3;
            this.btn_Export.Text = "Export Error Rows";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // Client_CostTat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 662);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.grd_Client_cost);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.btn_SampleFormat);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.lbl_CLient_Name);
            this.Controls.Add(this.lbl_title_Abs);
            this.Controls.Add(this.lbl_Abstitle);
            this.Name = "Client_CostTat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client_CostTat";
            this.Load += new System.EventHandler(this.Client_CostTat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Client_cost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Abstitle;
        private System.Windows.Forms.Label lbl_title_Abs;
        private System.Windows.Forms.Label lbl_CLient_Name;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_SampleFormat;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.DataGridView grd_Client_cost;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    }
}