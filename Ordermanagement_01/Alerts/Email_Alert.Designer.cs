namespace Ordermanagement_01.Alerts
{
    partial class Email_Alert
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
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.rbtn_Invoice_Sended = new System.Windows.Forms.RadioButton();
            this.rbtn_Invoice_NotSended = new System.Windows.Forms.RadioButton();
            this.txt_orderserach_Number = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            this.SuspendLayout();
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
            this.grd_order.ColumnHeadersHeight = 30;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.Order_Number,
            this.Column7,
            this.Column1,
            this.Column5,
            this.Column15,
            this.Column6});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle9;
            this.grd_order.Location = new System.Drawing.Point(12, 84);
            this.grd_order.Name = "grd_order";
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.Size = new System.Drawing.Size(824, 349);
            this.grd_order.TabIndex = 193;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            // 
            // rbtn_Invoice_Sended
            // 
            this.rbtn_Invoice_Sended.AutoSize = true;
            this.rbtn_Invoice_Sended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_Sended.Location = new System.Drawing.Point(432, 38);
            this.rbtn_Invoice_Sended.Name = "rbtn_Invoice_Sended";
            this.rbtn_Invoice_Sended.Size = new System.Drawing.Size(93, 20);
            this.rbtn_Invoice_Sended.TabIndex = 206;
            this.rbtn_Invoice_Sended.Text = "Email Sent";
            this.rbtn_Invoice_Sended.UseVisualStyleBackColor = true;
            this.rbtn_Invoice_Sended.CheckedChanged += new System.EventHandler(this.rbtn_Invoice_Sended_CheckedChanged);
            // 
            // rbtn_Invoice_NotSended
            // 
            this.rbtn_Invoice_NotSended.AutoSize = true;
            this.rbtn_Invoice_NotSended.Checked = true;
            this.rbtn_Invoice_NotSended.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Invoice_NotSended.Location = new System.Drawing.Point(307, 38);
            this.rbtn_Invoice_NotSended.Name = "rbtn_Invoice_NotSended";
            this.rbtn_Invoice_NotSended.Size = new System.Drawing.Size(119, 20);
            this.rbtn_Invoice_NotSended.TabIndex = 205;
            this.rbtn_Invoice_NotSended.TabStop = true;
            this.rbtn_Invoice_NotSended.Text = "Email Not Sent";
            this.rbtn_Invoice_NotSended.UseVisualStyleBackColor = true;
            this.rbtn_Invoice_NotSended.CheckedChanged += new System.EventHandler(this.rbtn_Invoice_NotSended_CheckedChanged);
            // 
            // txt_orderserach_Number
            // 
            this.txt_orderserach_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_orderserach_Number.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_orderserach_Number.Location = new System.Drawing.Point(85, 48);
            this.txt_orderserach_Number.Multiline = true;
            this.txt_orderserach_Number.Name = "txt_orderserach_Number";
            this.txt_orderserach_Number.Size = new System.Drawing.Size(202, 27);
            this.txt_orderserach_Number.TabIndex = 207;
            this.txt_orderserach_Number.TextChanged += new System.EventHandler(this.txt_orderserach_Number_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 208;
            this.label2.Text = "Search By:\r\n";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.MediumBlue;
            this.label25.Location = new System.Drawing.Point(323, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(212, 31);
            this.label25.TabIndex = 209;
            this.label25.Text = "EMAIL ALERT DETAILS";
            this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // Order_Number
            // 
            this.Order_Number.FillWeight = 153.6957F;
            this.Order_Number.HeaderText = "ORDER NUMBER";
            this.Order_Number.Name = "Order_Number";
            this.Order_Number.ReadOnly = true;
            this.Order_Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "EMAIL_DATE";
            this.Column7.Name = "Column7";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "EMAIL SENT BY";
            this.Column1.Name = "Column1";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "EMAIL";
            this.Column5.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Column15";
            this.Column15.Name = "Column15";
            this.Column15.Visible = false;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Email_Alert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 464);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_orderserach_Number);
            this.Controls.Add(this.rbtn_Invoice_Sended);
            this.Controls.Add(this.rbtn_Invoice_NotSended);
            this.Controls.Add(this.grd_order);
            this.Name = "Email_Alert";
            this.Text = "Email_Alert";
            this.Load += new System.EventHandler(this.Email_Alert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.RadioButton rbtn_Invoice_Sended;
        private System.Windows.Forms.RadioButton rbtn_Invoice_NotSended;
        private System.Windows.Forms.TextBox txt_orderserach_Number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}