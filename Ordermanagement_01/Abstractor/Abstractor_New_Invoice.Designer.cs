namespace Ordermanagement_01.Abstractor
{
    partial class Abstractor_New_Invoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ddl_Year = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddl_Month = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Number = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label25 = new System.Windows.Forms.Label();
            this.Ab = new System.Windows.Forms.GroupBox();
            this.txt_Email_Body_Content = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.ddl_Payment_Status = new System.Windows.Forms.ComboBox();
            this.txt_Reference_Number = new System.Windows.Forms.TextBox();
            this.txt_Invoice_Date = new System.Windows.Forms.DateTimePicker();
            this.label45 = new System.Windows.Forms.Label();
            this.txt_order_comments = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Abstractor_Name = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            this.Ab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ddl_Year
            // 
            this.ddl_Year.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Year.FormattingEnabled = true;
            this.ddl_Year.Items.AddRange(new object[] {
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"});
            this.ddl_Year.Location = new System.Drawing.Point(432, 47);
            this.ddl_Year.Name = "ddl_Year";
            this.ddl_Year.Size = new System.Drawing.Size(134, 28);
            this.ddl_Year.TabIndex = 211;
            this.ddl_Year.SelectedIndexChanged += new System.EventHandler(this.ddl_Year_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(319, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 19);
            this.label3.TabIndex = 210;
            this.label3.Text = "Year of Payment:\r\n";
            // 
            // ddl_Month
            // 
            this.ddl_Month.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Month.FormattingEnabled = true;
            this.ddl_Month.Location = new System.Drawing.Point(134, 47);
            this.ddl_Month.Name = "ddl_Month";
            this.ddl_Month.Size = new System.Drawing.Size(178, 28);
            this.ddl_Month.TabIndex = 209;
            this.ddl_Month.SelectedIndexChanged += new System.EventHandler(this.ddl_Month_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 19);
            this.label1.TabIndex = 208;
            this.label1.Text = "Month of Payment:\r\n";
            // 
            // grd_order
            // 
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.AllowUserToDeleteRows = false;
            this.grd_order.AllowUserToResizeRows = false;
            this.grd_order.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_order.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.grd_order.ColumnHeadersHeight = 40;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.SNo,
            this.Column10,
            this.Order_Number,
            this.Column4,
            this.Client_Name,
            this.Sub_ProcessName,
            this.Order_Type,
            this.Column3,
            this.Column1,
            this.Column6});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle12;
            this.grd_order.Location = new System.Drawing.Point(12, 81);
            this.grd_order.Name = "grd_order";
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.Size = new System.Drawing.Size(1249, 322);
            this.grd_order.TabIndex = 212;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            this.grd_order.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellContentClick);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "CHK";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // SNo
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SNo.DefaultCellStyle = dataGridViewCellStyle11;
            this.SNo.FillWeight = 91.37055F;
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Month";
            this.Column10.Name = "Column10";
            // 
            // Order_Number
            // 
            this.Order_Number.FillWeight = 153.6957F;
            this.Order_Number.HeaderText = "ORDERS";
            this.Order_Number.Name = "Order_Number";
            this.Order_Number.ReadOnly = true;
            this.Order_Number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Order_Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            // Column1
            // 
            this.Column1.HeaderText = "PAYMENT STATUS";
            this.Column1.Name = "Column1";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.MediumBlue;
            this.label25.Location = new System.Drawing.Point(493, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(363, 31);
            this.label25.TabIndex = 213;
            this.label25.Text = "ABSTRACTOR NEW ORDERS PAYMENT ";
            this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Ab
            // 
            this.Ab.Controls.Add(this.txt_Email_Body_Content);
            this.Ab.Controls.Add(this.label10);
            this.Ab.Controls.Add(this.label8);
            this.Ab.Controls.Add(this.button1);
            this.Ab.Controls.Add(this.btn_Save);
            this.Ab.Controls.Add(this.ddl_Payment_Status);
            this.Ab.Controls.Add(this.txt_Reference_Number);
            this.Ab.Controls.Add(this.txt_Invoice_Date);
            this.Ab.Controls.Add(this.label45);
            this.Ab.Controls.Add(this.txt_order_comments);
            this.Ab.Controls.Add(this.label44);
            this.Ab.Controls.Add(this.label40);
            this.Ab.Controls.Add(this.label34);
            this.Ab.Controls.Add(this.label22);
            this.Ab.Controls.Add(this.label24);
            this.Ab.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ab.Location = new System.Drawing.Point(14, 409);
            this.Ab.Name = "Ab";
            this.Ab.Size = new System.Drawing.Size(1247, 186);
            this.Ab.TabIndex = 214;
            this.Ab.TabStop = false;
            this.Ab.Text = "PAYMENT DETAILS";
            // 
            // txt_Email_Body_Content
            // 
            this.txt_Email_Body_Content.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Email_Body_Content.Location = new System.Drawing.Point(806, 74);
            this.txt_Email_Body_Content.Multiline = true;
            this.txt_Email_Body_Content.Name = "txt_Email_Body_Content";
            this.txt_Email_Body_Content.Size = new System.Drawing.Size(419, 74);
            this.txt_Email_Body_Content.TabIndex = 238;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(712, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 20);
            this.label10.TabIndex = 237;
            this.label10.Text = "Email Content:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(621, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(157, 20);
            this.label8.TabIndex = 202;
            this.label8.Text = "Check Reference Number";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(712, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(551, 143);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(155, 32);
            this.btn_Save.TabIndex = 5;
            this.btn_Save.Text = "Genrate Payment";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // ddl_Payment_Status
            // 
            this.ddl_Payment_Status.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Payment_Status.FormattingEnabled = true;
            this.ddl_Payment_Status.Location = new System.Drawing.Point(176, 28);
            this.ddl_Payment_Status.Name = "ddl_Payment_Status";
            this.ddl_Payment_Status.Size = new System.Drawing.Size(183, 28);
            this.ddl_Payment_Status.TabIndex = 1;
            // 
            // txt_Reference_Number
            // 
            this.txt_Reference_Number.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Reference_Number.Location = new System.Drawing.Point(776, 28);
            this.txt_Reference_Number.Name = "txt_Reference_Number";
            this.txt_Reference_Number.Size = new System.Drawing.Size(184, 25);
            this.txt_Reference_Number.TabIndex = 3;
            // 
            // txt_Invoice_Date
            // 
            this.txt_Invoice_Date.CalendarMonthBackground = System.Drawing.Color.LightGray;
            this.txt_Invoice_Date.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Invoice_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_Invoice_Date.Location = new System.Drawing.Point(477, 31);
            this.txt_Invoice_Date.Name = "txt_Invoice_Date";
            this.txt_Invoice_Date.Size = new System.Drawing.Size(115, 25);
            this.txt_Invoice_Date.TabIndex = 2;
            this.txt_Invoice_Date.Value = new System.DateTime(2014, 10, 31, 0, 0, 0, 0);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(1025, 151);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(189, 17);
            this.label45.TabIndex = 199;
            this.label45.Text = "(Fields with * Mark are mandatory)";
            // 
            // txt_order_comments
            // 
            this.txt_order_comments.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_order_comments.Location = new System.Drawing.Point(176, 74);
            this.txt_order_comments.Multiline = true;
            this.txt_order_comments.Name = "txt_order_comments";
            this.txt_order_comments.Size = new System.Drawing.Size(530, 65);
            this.txt_order_comments.TabIndex = 4;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(11, 96);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(74, 20);
            this.label44.TabIndex = 197;
            this.label44.Text = "Comments:";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.ForeColor = System.Drawing.Color.Red;
            this.label40.Location = new System.Drawing.Point(365, 38);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(13, 17);
            this.label40.TabIndex = 193;
            this.label40.Text = "*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Red;
            this.label34.Location = new System.Drawing.Point(602, 35);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(13, 17);
            this.label34.TabIndex = 187;
            this.label34.Text = "*";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(386, 33);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 20);
            this.label22.TabIndex = 181;
            this.label22.Text = "Process Date:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(13, 32);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(100, 20);
            this.label24.TabIndex = 165;
            this.label24.Text = "Payment Status:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(591, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 19);
            this.label2.TabIndex = 215;
            this.label2.Text = "Search Abstractor:\r\n";
            // 
            // txt_Abstractor_Name
            // 
            this.txt_Abstractor_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Abstractor_Name.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.txt_Abstractor_Name.Location = new System.Drawing.Point(704, 47);
            this.txt_Abstractor_Name.Name = "txt_Abstractor_Name";
            this.txt_Abstractor_Name.Size = new System.Drawing.Size(235, 25);
            this.txt_Abstractor_Name.TabIndex = 216;
            this.txt_Abstractor_Name.TextChanged += new System.EventHandler(this.txt_Abstractor_Name_TextChanged);
            // 
            // Abstractor_New_Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 593);
            this.Controls.Add(this.txt_Abstractor_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Ab);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.grd_order);
            this.Controls.Add(this.ddl_Year);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ddl_Month);
            this.Controls.Add(this.label1);
            this.Name = "Abstractor_New_Invoice";
            this.Text = "Abstractor_New_Invoice";
            this.Load += new System.EventHandler(this.Abstractor_New_Invoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            this.Ab.ResumeLayout(false);
            this.Ab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddl_Year;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddl_Month;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox Ab;
        private System.Windows.Forms.DateTimePicker txt_Invoice_Date;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txt_order_comments;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_Reference_Number;
        private System.Windows.Forms.ComboBox ddl_Payment_Status;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewButtonColumn Order_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.TextBox txt_Email_Body_Content;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Abstractor_Name;
    }
}