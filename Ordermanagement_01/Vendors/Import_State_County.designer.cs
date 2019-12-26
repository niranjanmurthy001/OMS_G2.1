namespace Ordermanagement_01.Vendors
{
    partial class Import_State_County
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_title_Abs = new System.Windows.Forms.Label();
            this.btn_SampleFormat = new System.Windows.Forms.Button();
            this.lbl_ErrorRows = new System.Windows.Forms.Label();
            this.lbl_Vendor_Name = new System.Windows.Forms.Label();
            this.grd_CpVendor = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_ErrorRows = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.grd_Vendor_Statecounty = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.lbl_Abstitle = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd_CpVendor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Vendor_Statecounty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_title_Abs
            // 
            this.lbl_title_Abs.AutoSize = true;
            this.lbl_title_Abs.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title_Abs.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_title_Abs.Location = new System.Drawing.Point(16, 25);
            this.lbl_title_Abs.Name = "lbl_title_Abs";
            this.lbl_title_Abs.Size = new System.Drawing.Size(125, 24);
            this.lbl_title_Abs.TabIndex = 50;
            this.lbl_title_Abs.Text = "VENDOR NAME:";
            this.lbl_title_Abs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_SampleFormat
            // 
            this.btn_SampleFormat.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_SampleFormat.Location = new System.Drawing.Point(803, 55);
            this.btn_SampleFormat.Name = "btn_SampleFormat";
            this.btn_SampleFormat.Size = new System.Drawing.Size(124, 35);
            this.btn_SampleFormat.TabIndex = 48;
            this.btn_SampleFormat.Text = "Sample Format";
            this.btn_SampleFormat.UseVisualStyleBackColor = true;
            this.btn_SampleFormat.Click += new System.EventHandler(this.btn_SampleFormat_Click);
            // 
            // lbl_ErrorRows
            // 
            this.lbl_ErrorRows.AutoSize = true;
            this.lbl_ErrorRows.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_ErrorRows.Location = new System.Drawing.Point(13, 419);
            this.lbl_ErrorRows.Name = "lbl_ErrorRows";
            this.lbl_ErrorRows.Size = new System.Drawing.Size(111, 24);
            this.lbl_ErrorRows.TabIndex = 47;
            this.lbl_ErrorRows.Text = "ERROR ROWS:";
            // 
            // lbl_Vendor_Name
            // 
            this.lbl_Vendor_Name.AutoSize = true;
            this.lbl_Vendor_Name.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Vendor_Name.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_Vendor_Name.Location = new System.Drawing.Point(147, 25);
            this.lbl_Vendor_Name.Name = "lbl_Vendor_Name";
            this.lbl_Vendor_Name.Size = new System.Drawing.Size(0, 24);
            this.lbl_Vendor_Name.TabIndex = 49;
            this.lbl_Vendor_Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grd_CpVendor
            // 
            this.grd_CpVendor.AllowUserToAddRows = false;
            this.grd_CpVendor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_CpVendor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_CpVendor.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_CpVendor.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_CpVendor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_CpVendor.ColumnHeadersHeight = 30;
            this.grd_CpVendor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_CpVendor.DefaultCellStyle = dataGridViewCellStyle2;
            this.grd_CpVendor.Location = new System.Drawing.Point(16, 446);
            this.grd_CpVendor.Name = "grd_CpVendor";
            this.grd_CpVendor.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_CpVendor.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grd_CpVendor.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_CpVendor.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_CpVendor.RowTemplate.Height = 25;
            this.grd_CpVendor.Size = new System.Drawing.Size(1116, 203);
            this.grd_CpVendor.TabIndex = 46;
            this.grd_CpVendor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CpVendor_CellClick);
            // 
            // Column7
            // 
            this.Column7.FillWeight = 33.72091F;
            this.Column7.HeaderText = "ADD";
            this.Column7.Name = "Column7";
            this.Column7.Text = "Submit";
            // 
            // Column8
            // 
            this.Column8.FillWeight = 34.63114F;
            this.Column8.HeaderText = "REMOVE";
            this.Column8.Name = "Column8";
            this.Column8.Text = "Delete";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 136.3824F;
            this.dataGridViewTextBoxColumn2.HeaderText = "STATE";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 136.3824F;
            this.dataGridViewTextBoxColumn3.HeaderText = "COUNTY";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // btn_ErrorRows
            // 
            this.btn_ErrorRows.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_ErrorRows.Location = new System.Drawing.Point(933, 55);
            this.btn_ErrorRows.Name = "btn_ErrorRows";
            this.btn_ErrorRows.Size = new System.Drawing.Size(98, 35);
            this.btn_ErrorRows.TabIndex = 45;
            this.btn_ErrorRows.Text = "Error Rows";
            this.btn_ErrorRows.UseVisualStyleBackColor = true;
            this.btn_ErrorRows.Click += new System.EventHandler(this.btn_ErrorRows_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(199, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 44;
            this.label3.Text = "Error in Excel Record";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // grd_Vendor_Statecounty
            // 
            this.grd_Vendor_Statecounty.AllowUserToAddRows = false;
            this.grd_Vendor_Statecounty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Vendor_Statecounty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Vendor_Statecounty.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Vendor_Statecounty.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Vendor_Statecounty.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grd_Vendor_Statecounty.ColumnHeadersHeight = 30;
            this.grd_Vendor_Statecounty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Vendor_Statecounty.DefaultCellStyle = dataGridViewCellStyle6;
            this.grd_Vendor_Statecounty.Location = new System.Drawing.Point(16, 100);
            this.grd_Vendor_Statecounty.Name = "grd_Vendor_Statecounty";
            this.grd_Vendor_Statecounty.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Vendor_Statecounty.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grd_Vendor_Statecounty.RowHeadersVisible = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Vendor_Statecounty.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.grd_Vendor_Statecounty.RowTemplate.Height = 25;
            this.grd_Vendor_Statecounty.Size = new System.Drawing.Size(1116, 316);
            this.grd_Vendor_Statecounty.TabIndex = 39;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "VENDOR NAME";
            this.Column1.Name = "Column1";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "STATE";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "COUNTY";
            this.Column5.Name = "Column5";
            // 
            // btn_Import
            // 
            this.btn_Import.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Import.Location = new System.Drawing.Point(1037, 55);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(90, 35);
            this.btn_Import.TabIndex = 38;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_upload.Location = new System.Drawing.Point(16, 59);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(119, 32);
            this.btn_upload.TabIndex = 37;
            this.btn_upload.Text = "Upload Excel";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // lbl_Abstitle
            // 
            this.lbl_Abstitle.AutoSize = true;
            this.lbl_Abstitle.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Abstitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Abstitle.Location = new System.Drawing.Point(410, 9);
            this.lbl_Abstitle.Name = "lbl_Abstitle";
            this.lbl_Abstitle.Size = new System.Drawing.Size(314, 31);
            this.lbl_Abstitle.TabIndex = 36;
            this.lbl_Abstitle.Text = "IMPORT VENDOR STATE COUNTY";
            this.lbl_Abstitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Red;
            this.pictureBox2.Location = new System.Drawing.Point(152, 67);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 13);
            this.pictureBox2.TabIndex = 43;
            this.pictureBox2.TabStop = false;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(1091, 12);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(36, 37);
            this.btn_Refresh.TabIndex = 130;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // Import_State_County
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 662);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.lbl_title_Abs);
            this.Controls.Add(this.btn_SampleFormat);
            this.Controls.Add(this.lbl_ErrorRows);
            this.Controls.Add(this.lbl_Vendor_Name);
            this.Controls.Add(this.grd_CpVendor);
            this.Controls.Add(this.btn_ErrorRows);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.grd_Vendor_Statecounty);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.lbl_Abstitle);
            this.MaximizeBox = false;
            this.Name = "Import_State_County";
            this.Text = "Import_State_County";
            this.Load += new System.EventHandler(this.Import_State_County_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_CpVendor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Vendor_Statecounty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_title_Abs;
        private System.Windows.Forms.Button btn_SampleFormat;
        private System.Windows.Forms.Label lbl_ErrorRows;
        private System.Windows.Forms.Label lbl_Vendor_Name;
        private System.Windows.Forms.DataGridView grd_CpVendor;
        private System.Windows.Forms.Button btn_ErrorRows;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView grd_Vendor_Statecounty;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Label lbl_Abstitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}