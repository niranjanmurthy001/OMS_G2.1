namespace Ordermanagement_01.Abstractor
{
    partial class Abstractor_Invoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label25 = new System.Windows.Forms.Label();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATECOUNTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbtn_Invoice_Sended = new System.Windows.Forms.RadioButton();
            this.rbtn_Invoice_NotSended = new System.Windows.Forms.RadioButton();
            this.cbo_colmn = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_orderserach_Number = new System.Windows.Forms.TextBox();
            this.btn_New_Invoice = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btn_Refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            this.SuspendLayout();
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.MediumBlue;
            this.label25.Location = new System.Drawing.Point(441, 9);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(389, 31);
            this.label25.TabIndex = 191;
            this.label25.Text = "ABSTRACTOR ORDERS PAYMENT DETAILS";
            this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grd_order
            // 
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.AllowUserToResizeRows = false;
            this.grd_order.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_order.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grd_order.ColumnHeadersHeight = 40;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.Column7,
            this.Column10,
            this.Column9,
            this.Column4,
            this.Client_Name,
            this.Sub_ProcessName,
            this.Order_Type,
            this.Column3,
            this.STATECOUNTY,
            this.Date,
            this.Column1,
            this.Column2,
            this.Column13,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column11});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle9;
            this.grd_order.Location = new System.Drawing.Point(12, 124);
            this.grd_order.Name = "grd_order";
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.Size = new System.Drawing.Size(1249, 429);
            this.grd_order.TabIndex = 192;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            // 
            // SNo
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SNo.DefaultCellStyle = dataGridViewCellStyle8;
            this.SNo.FillWeight = 91.37055F;
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "PAYMENT NO";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Month";
            this.Column10.Name = "Column10";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Year";
            this.Column9.Name = "Column9";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ABS NAME";
            this.Column4.Name = "Column4";
            // 
            // Client_Name
            // 
            this.Client_Name.FillWeight = 61.35016F;
            this.Client_Name.HeaderText = "ABS  PHONE";
            this.Client_Name.Name = "Client_Name";
            this.Client_Name.ReadOnly = true;
            // 
            // Sub_ProcessName
            // 
            this.Sub_ProcessName.FillWeight = 124.9346F;
            this.Sub_ProcessName.HeaderText = "PAYEE NAME";
            this.Sub_ProcessName.Name = "Sub_ProcessName";
            this.Sub_ProcessName.ReadOnly = true;
            // 
            // Order_Type
            // 
            this.Order_Type.FillWeight = 130.2978F;
            this.Order_Type.HeaderText = "Mailing Address";
            this.Order_Type.Name = "Order_Type";
            this.Order_Type.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "AMOUNT";
            this.Column3.Name = "Column3";
            // 
            // STATECOUNTY
            // 
            this.STATECOUNTY.FillWeight = 147.2111F;
            this.STATECOUNTY.HeaderText = "PAYMENT STATUS";
            this.STATECOUNTY.Name = "STATECOUNTY";
            this.STATECOUNTY.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.FillWeight = 132.6245F;
            this.Date.HeaderText = "PROCESSED DATE";
            this.Date.Name = "Date";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "REF NUM";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "CHECK SCANNED";
            this.Column2.Name = "Column2";
            // 
            // Column13
            // 
            this.Column13.FillWeight = 61.35016F;
            this.Column13.HeaderText = "VIEW";
            this.Column13.Image = global::Ordermanagement_01.Properties.Resources.Preview;
            this.Column13.Name = "Column13";
            this.Column13.ToolTipText = "View";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "EMAIL";
            this.Column5.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            this.Column8.Visible = false;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Email_Contents";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            // 
            // rbtn_Invoice_Sended
            // 
            this.rbtn_Invoice_Sended.AutoSize = true;
            this.rbtn_Invoice_Sended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_Sended.Location = new System.Drawing.Point(657, 52);
            this.rbtn_Invoice_Sended.Name = "rbtn_Invoice_Sended";
            this.rbtn_Invoice_Sended.Size = new System.Drawing.Size(93, 20);
            this.rbtn_Invoice_Sended.TabIndex = 200;
            this.rbtn_Invoice_Sended.Text = "Email Sent";
            this.rbtn_Invoice_Sended.UseVisualStyleBackColor = true;
            this.rbtn_Invoice_Sended.CheckedChanged += new System.EventHandler(this.rbtn_Invoice_Sended_CheckedChanged);
            // 
            // rbtn_Invoice_NotSended
            // 
            this.rbtn_Invoice_NotSended.AutoSize = true;
            this.rbtn_Invoice_NotSended.Checked = true;
            this.rbtn_Invoice_NotSended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_NotSended.Location = new System.Drawing.Point(532, 52);
            this.rbtn_Invoice_NotSended.Name = "rbtn_Invoice_NotSended";
            this.rbtn_Invoice_NotSended.Size = new System.Drawing.Size(119, 20);
            this.rbtn_Invoice_NotSended.TabIndex = 199;
            this.rbtn_Invoice_NotSended.TabStop = true;
            this.rbtn_Invoice_NotSended.Text = "Email Not Sent";
            this.rbtn_Invoice_NotSended.UseVisualStyleBackColor = true;
            this.rbtn_Invoice_NotSended.CheckedChanged += new System.EventHandler(this.rbtn_Invoice_NotSended_CheckedChanged);
            // 
            // cbo_colmn
            // 
            this.cbo_colmn.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_colmn.FormattingEnabled = true;
            this.cbo_colmn.Items.AddRange(new object[] {
            "Abstractor Name",
            "Payment Date",
            "Payment Status",
            "Email"});
            this.cbo_colmn.Location = new System.Drawing.Point(96, 90);
            this.cbo_colmn.Name = "cbo_colmn";
            this.cbo_colmn.Size = new System.Drawing.Size(222, 28);
            this.cbo_colmn.TabIndex = 202;
            this.cbo_colmn.SelectedIndexChanged += new System.EventHandler(this.cbo_colmn_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 201;
            this.label2.Text = "Search By:\r\n";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txt_orderserach_Number
            // 
            this.txt_orderserach_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_orderserach_Number.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_orderserach_Number.Location = new System.Drawing.Point(324, 90);
            this.txt_orderserach_Number.Multiline = true;
            this.txt_orderserach_Number.Name = "txt_orderserach_Number";
            this.txt_orderserach_Number.Size = new System.Drawing.Size(301, 27);
            this.txt_orderserach_Number.TabIndex = 203;
            this.txt_orderserach_Number.TextChanged += new System.EventHandler(this.txt_orderserach_Number_TextChanged);
            // 
            // btn_New_Invoice
            // 
            this.btn_New_Invoice.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_New_Invoice.Location = new System.Drawing.Point(647, 86);
            this.btn_New_Invoice.Name = "btn_New_Invoice";
            this.btn_New_Invoice.Size = new System.Drawing.Size(233, 33);
            this.btn_New_Invoice.TabIndex = 208;
            this.btn_New_Invoice.Text = "Genrate New Payment";
            this.btn_New_Invoice.UseVisualStyleBackColor = true;
            this.btn_New_Invoice.Click += new System.EventHandler(this.btn_New_Invoice_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 61.35016F;
            this.dataGridViewImageColumn1.HeaderText = "VIEW";
            this.dataGridViewImageColumn1.Image = global::Ordermanagement_01.Properties.Resources.Preview;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ToolTipText = "View";
            this.dataGridViewImageColumn1.Width = 51;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "EMAIL";
            this.dataGridViewImageColumn2.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 83;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(12, 32);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(44, 40);
            this.btn_Refresh.TabIndex = 209;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // Abstractor_Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 565);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.btn_New_Invoice);
            this.Controls.Add(this.cbo_colmn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_orderserach_Number);
            this.Controls.Add(this.rbtn_Invoice_Sended);
            this.Controls.Add(this.rbtn_Invoice_NotSended);
            this.Controls.Add(this.grd_order);
            this.Controls.Add(this.label25);
            this.Name = "Abstractor_Invoice";
            this.Text = "Abstractor_Invoice";
            this.Load += new System.EventHandler(this.Abstractor_Invoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.RadioButton rbtn_Invoice_Sended;
        private System.Windows.Forms.RadioButton rbtn_Invoice_NotSended;
        private System.Windows.Forms.ComboBox cbo_colmn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_orderserach_Number;
        private System.Windows.Forms.Button btn_New_Invoice;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewLinkColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATECOUNTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn Column13;
        private System.Windows.Forms.DataGridViewImageColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}