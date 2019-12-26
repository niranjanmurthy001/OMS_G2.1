namespace Ordermanagement_01.Abstractor
{
    partial class Import_Absractor_Cost_TAT
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
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.lbl_Abstitle = new System.Windows.Forms.Label();
            this.grd_Abstracter_CostTAT = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_NonAddedRows = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_ErrorRows = new System.Windows.Forms.Button();
            this.grd_CpAbstracter_CostTAT = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_ErrorRows = new System.Windows.Forms.Label();
            this.btn_SampleFormat = new System.Windows.Forms.Button();
            this.lbl_Abs_Name = new System.Windows.Forms.Label();
            this.lbl_title_Abs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Abstracter_CostTAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_CpAbstracter_CostTAT)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Import
            // 
            this.btn_Import.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_Import.Location = new System.Drawing.Point(1033, 51);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(90, 35);
            this.btn_Import.TabIndex = 17;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_upload.Location = new System.Drawing.Point(12, 55);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(119, 32);
            this.btn_upload.TabIndex = 16;
            this.btn_upload.Text = "Upload Excel";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // lbl_Abstitle
            // 
            this.lbl_Abstitle.AutoSize = true;
            this.lbl_Abstitle.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Abstitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Abstitle.Location = new System.Drawing.Point(387, 8);
            this.lbl_Abstitle.Name = "lbl_Abstitle";
            this.lbl_Abstitle.Size = new System.Drawing.Size(403, 31);
            this.lbl_Abstitle.TabIndex = 15;
            this.lbl_Abstitle.Text = "IMPORT ABSTRACTOR COST AND TAT INFO";
            this.lbl_Abstitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grd_Abstracter_CostTAT
            // 
            this.grd_Abstracter_CostTAT.AllowUserToAddRows = false;
            this.grd_Abstracter_CostTAT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Abstracter_CostTAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Abstracter_CostTAT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Abstracter_CostTAT.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Abstracter_CostTAT.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_Abstracter_CostTAT.ColumnHeadersHeight = 30;
            this.grd_Abstracter_CostTAT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column5,
            this.Column2,
            this.Column3,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Abstracter_CostTAT.DefaultCellStyle = dataGridViewCellStyle2;
            this.grd_Abstracter_CostTAT.Location = new System.Drawing.Point(12, 96);
            this.grd_Abstracter_CostTAT.Name = "grd_Abstracter_CostTAT";
            this.grd_Abstracter_CostTAT.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Abstracter_CostTAT.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grd_Abstracter_CostTAT.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Abstracter_CostTAT.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_Abstracter_CostTAT.RowTemplate.Height = 25;
            this.grd_Abstracter_CostTAT.Size = new System.Drawing.Size(1231, 316);
            this.grd_Abstracter_CostTAT.TabIndex = 18;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ABSTRACTOR NAME";
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
            // Column2
            // 
            this.Column2.HeaderText = "ORDER TYPE";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "COST";
            this.Column3.Name = "Column3";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "TAT";
            this.Column6.Name = "Column6";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_NonAddedRows
            // 
            this.btn_NonAddedRows.Enabled = false;
            this.btn_NonAddedRows.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_NonAddedRows.Location = new System.Drawing.Point(878, 51);
            this.btn_NonAddedRows.Name = "btn_NonAddedRows";
            this.btn_NonAddedRows.Size = new System.Drawing.Size(140, 35);
            this.btn_NonAddedRows.TabIndex = 19;
            this.btn_NonAddedRows.Text = "Non-Added Rows";
            this.btn_NonAddedRows.UseVisualStyleBackColor = true;
            this.btn_NonAddedRows.Click += new System.EventHandler(this.btn_NonAddedRows_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(181, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Record Already Exist";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(360, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Error in Excel Record";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Red;
            this.pictureBox2.Location = new System.Drawing.Point(313, 66);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 13);
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Cyan;
            this.pictureBox1.Location = new System.Drawing.Point(139, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 13);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // btn_ErrorRows
            // 
            this.btn_ErrorRows.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_ErrorRows.Location = new System.Drawing.Point(765, 51);
            this.btn_ErrorRows.Name = "btn_ErrorRows";
            this.btn_ErrorRows.Size = new System.Drawing.Size(98, 35);
            this.btn_ErrorRows.TabIndex = 24;
            this.btn_ErrorRows.Text = "Error Rows";
            this.btn_ErrorRows.UseVisualStyleBackColor = true;
            this.btn_ErrorRows.Click += new System.EventHandler(this.btn_ErrorRows_Click);
            // 
            // grd_CpAbstracter_CostTAT
            // 
            this.grd_CpAbstracter_CostTAT.AllowUserToAddRows = false;
            this.grd_CpAbstracter_CostTAT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_CpAbstracter_CostTAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_CpAbstracter_CostTAT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_CpAbstracter_CostTAT.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_CpAbstracter_CostTAT.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grd_CpAbstracter_CostTAT.ColumnHeadersHeight = 30;
            this.grd_CpAbstracter_CostTAT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_CpAbstracter_CostTAT.DefaultCellStyle = dataGridViewCellStyle6;
            this.grd_CpAbstracter_CostTAT.Location = new System.Drawing.Point(12, 442);
            this.grd_CpAbstracter_CostTAT.Name = "grd_CpAbstracter_CostTAT";
            this.grd_CpAbstracter_CostTAT.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_CpAbstracter_CostTAT.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grd_CpAbstracter_CostTAT.RowHeadersVisible = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Ebrima", 9.75F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_CpAbstracter_CostTAT.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.grd_CpAbstracter_CostTAT.RowTemplate.Height = 25;
            this.grd_CpAbstracter_CostTAT.Size = new System.Drawing.Size(1231, 203);
            this.grd_CpAbstracter_CostTAT.TabIndex = 25;
            this.grd_CpAbstracter_CostTAT.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CpAbstracter_CostTAT_CellClick);
            // 
            // Column7
            // 
            this.Column7.FillWeight = 85.27919F;
            this.Column7.HeaderText = "ADD";
            this.Column7.Name = "Column7";
            this.Column7.Text = "Submit";
            // 
            // Column8
            // 
            this.Column8.FillWeight = 85.27919F;
            this.Column8.HeaderText = "REMOVE";
            this.Column8.Name = "Column8";
            this.Column8.Text = "Delete";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 203.0457F;
            this.dataGridViewTextBoxColumn1.HeaderText = "ABSTRACTOR NAME";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 85.27919F;
            this.dataGridViewTextBoxColumn2.HeaderText = "STATE";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 85.27919F;
            this.dataGridViewTextBoxColumn3.HeaderText = "COUNTY";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 85.27919F;
            this.dataGridViewTextBoxColumn4.HeaderText = "ORDER TYPE";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 85.27919F;
            this.dataGridViewTextBoxColumn5.HeaderText = "COST";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 85.27919F;
            this.dataGridViewTextBoxColumn6.HeaderText = "TAT";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // lbl_ErrorRows
            // 
            this.lbl_ErrorRows.AutoSize = true;
            this.lbl_ErrorRows.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold);
            this.lbl_ErrorRows.Location = new System.Drawing.Point(9, 415);
            this.lbl_ErrorRows.Name = "lbl_ErrorRows";
            this.lbl_ErrorRows.Size = new System.Drawing.Size(111, 24);
            this.lbl_ErrorRows.TabIndex = 32;
            this.lbl_ErrorRows.Text = "ERROR ROWS:";
            // 
            // btn_SampleFormat
            // 
            this.btn_SampleFormat.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_SampleFormat.Location = new System.Drawing.Point(621, 51);
            this.btn_SampleFormat.Name = "btn_SampleFormat";
            this.btn_SampleFormat.Size = new System.Drawing.Size(124, 35);
            this.btn_SampleFormat.TabIndex = 33;
            this.btn_SampleFormat.Text = "Sample Format";
            this.btn_SampleFormat.UseVisualStyleBackColor = true;
            this.btn_SampleFormat.Click += new System.EventHandler(this.btn_SampleFormat_Click);
            // 
            // lbl_Abs_Name
            // 
            this.lbl_Abs_Name.AutoSize = true;
            this.lbl_Abs_Name.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Abs_Name.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_Abs_Name.Location = new System.Drawing.Point(194, 26);
            this.lbl_Abs_Name.Name = "lbl_Abs_Name";
            this.lbl_Abs_Name.Size = new System.Drawing.Size(0, 24);
            this.lbl_Abs_Name.TabIndex = 34;
            this.lbl_Abs_Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_Abs_Name.Visible = false;
            // 
            // lbl_title_Abs
            // 
            this.lbl_title_Abs.AutoSize = true;
            this.lbl_title_Abs.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title_Abs.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_title_Abs.Location = new System.Drawing.Point(12, 24);
            this.lbl_title_Abs.Name = "lbl_title_Abs";
            this.lbl_title_Abs.Size = new System.Drawing.Size(161, 24);
            this.lbl_title_Abs.TabIndex = 35;
            this.lbl_title_Abs.Text = "ABSTRACTOR NAME:";
            this.lbl_title_Abs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_title_Abs.Visible = false;
            // 
            // Import_Absractor_Cost_TAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 662);
            this.Controls.Add(this.lbl_title_Abs);
            this.Controls.Add(this.lbl_Abs_Name);
            this.Controls.Add(this.btn_SampleFormat);
            this.Controls.Add(this.lbl_ErrorRows);
            this.Controls.Add(this.grd_CpAbstracter_CostTAT);
            this.Controls.Add(this.btn_ErrorRows);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_NonAddedRows);
            this.Controls.Add(this.grd_Abstracter_CostTAT);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.lbl_Abstitle);
            this.Name = "Import_Absractor_Cost_TAT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import_Absractor_Cost_TAT";
            this.Load += new System.EventHandler(this.Import_Absractor_Cost_TAT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Abstracter_CostTAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_CpAbstracter_CostTAT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Label lbl_Abstitle;
        private System.Windows.Forms.DataGridView grd_Abstracter_CostTAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_NonAddedRows;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_ErrorRows;
        private System.Windows.Forms.DataGridView grd_CpAbstracter_CostTAT;
        private System.Windows.Forms.Label lbl_ErrorRows;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button btn_SampleFormat;
        private System.Windows.Forms.Label lbl_Abs_Name;
        private System.Windows.Forms.Label lbl_title_Abs;
    }
}