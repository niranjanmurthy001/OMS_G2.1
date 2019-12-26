namespace Ordermanagement_01
{
    partial class Rework_Search_Order
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Order_number = new System.Windows.Forms.TextBox();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.OrderNumber = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.rbtn_Completed_Order = new System.Windows.Forms.RadioButton();
            this.rbn_Rework_Order_Search = new System.Windows.Forms.RadioButton();
            this.ddl_Order_Status_Reallocate = new System.Windows.Forms.ComboBox();
            this.ddl_UserName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.link_Search_Order_Allocation = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Order Number :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txt_Order_number
            // 
            this.txt_Order_number.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Order_number.Location = new System.Drawing.Point(137, 80);
            this.txt_Order_number.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Order_number.Name = "txt_Order_number";
            this.txt_Order_number.Size = new System.Drawing.Size(214, 25);
            this.txt_Order_number.TabIndex = 3;
            this.txt_Order_number.TextChanged += new System.EventHandler(this.txt_Order_number_TextChanged);
            // 
            // grd_order
            // 
            this.grd_order.AllowUserToAddRows = false;
            this.grd_order.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.grd_order.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_order.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_order.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.grd_order.ColumnHeadersHeight = 36;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderNumber,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column12,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle11;
            this.grd_order.Location = new System.Drawing.Point(1, 133);
            this.grd_order.Name = "grd_order";
            this.grd_order.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.grd_order.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grd_order.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_order.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.grd_order.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_order.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_order.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_order.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.RowTemplate.Height = 25;
            this.grd_order.Size = new System.Drawing.Size(1200, 198);
            this.grd_order.TabIndex = 10;
            this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            // 
            // OrderNumber
            // 
            this.OrderNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderNumber.DefaultCellStyle = dataGridViewCellStyle10;
            this.OrderNumber.HeaderText = "ORDER NUMBER";
            this.OrderNumber.MinimumWidth = 2;
            this.OrderNumber.Name = "OrderNumber";
            this.OrderNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OrderNumber.Text = "";
            this.OrderNumber.Width = 135;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CLIENT";
            this.Column1.Name = "Column1";
            this.Column1.Width = 97;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "SUB CLIENT";
            this.Column2.Name = "Column2";
            this.Column2.Width = 130;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "RECEIVED DATE";
            this.Column3.Name = "Column3";
            this.Column3.Width = 127;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ORDER TYPE";
            this.Column4.Name = "Column4";
            this.Column4.Width = 96;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 130F;
            this.Column5.HeaderText = "ORDER REF. NO";
            this.Column5.Name = "Column5";
            this.Column5.Width = 124;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "SEARCH TYPE";
            this.Column6.Name = "Column6";
            this.Column6.Width = 117;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "COUNTY";
            this.Column7.Name = "Column7";
            this.Column7.Width = 97;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "STATE";
            this.Column8.Name = "Column8";
            this.Column8.Width = 97;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "TASK";
            this.Column9.Name = "Column9";
            this.Column9.Width = 135;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "STATUS";
            this.Column12.Name = "Column12";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "USER";
            this.Column10.Name = "Column10";
            this.Column10.Width = 97;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "ORDER ID";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(506, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 31);
            this.label4.TabIndex = 11;
            this.label4.Text = "REWORK SEARCH ORDER";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rbtn_Completed_Order
            // 
            this.rbtn_Completed_Order.AutoSize = true;
            this.rbtn_Completed_Order.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Completed_Order.Location = new System.Drawing.Point(632, 40);
            this.rbtn_Completed_Order.Name = "rbtn_Completed_Order";
            this.rbtn_Completed_Order.Size = new System.Drawing.Size(189, 20);
            this.rbtn_Completed_Order.TabIndex = 200;
            this.rbtn_Completed_Order.Text = "Completed Order Search";
            this.rbtn_Completed_Order.UseVisualStyleBackColor = true;
            this.rbtn_Completed_Order.CheckedChanged += new System.EventHandler(this.rbtn_Completed_Order_CheckedChanged);
            // 
            // rbn_Rework_Order_Search
            // 
            this.rbn_Rework_Order_Search.AutoSize = true;
            this.rbn_Rework_Order_Search.Checked = true;
            this.rbn_Rework_Order_Search.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_Rework_Order_Search.Location = new System.Drawing.Point(462, 40);
            this.rbn_Rework_Order_Search.Name = "rbn_Rework_Order_Search";
            this.rbn_Rework_Order_Search.Size = new System.Drawing.Size(164, 20);
            this.rbn_Rework_Order_Search.TabIndex = 199;
            this.rbn_Rework_Order_Search.TabStop = true;
            this.rbn_Rework_Order_Search.Text = "Rework Order Search";
            this.rbn_Rework_Order_Search.UseVisualStyleBackColor = true;
            this.rbn_Rework_Order_Search.CheckedChanged += new System.EventHandler(this.rbn_Rework_Order_Search_CheckedChanged);
            // 
            // ddl_Order_Status_Reallocate
            // 
            this.ddl_Order_Status_Reallocate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Order_Status_Reallocate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Order_Status_Reallocate.FormattingEnabled = true;
            this.ddl_Order_Status_Reallocate.Location = new System.Drawing.Point(734, 82);
            this.ddl_Order_Status_Reallocate.Margin = new System.Windows.Forms.Padding(4);
            this.ddl_Order_Status_Reallocate.Name = "ddl_Order_Status_Reallocate";
            this.ddl_Order_Status_Reallocate.Size = new System.Drawing.Size(192, 28);
            this.ddl_Order_Status_Reallocate.TabIndex = 204;
            this.ddl_Order_Status_Reallocate.SelectedIndexChanged += new System.EventHandler(this.ddl_Order_Status_Reallocate_SelectedIndexChanged);
            // 
            // ddl_UserName
            // 
            this.ddl_UserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_UserName.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_UserName.FormattingEnabled = true;
            this.ddl_UserName.Location = new System.Drawing.Point(447, 81);
            this.ddl_UserName.Margin = new System.Windows.Forms.Padding(4);
            this.ddl_UserName.Name = "ddl_UserName";
            this.ddl_UserName.Size = new System.Drawing.Size(239, 28);
            this.ddl_UserName.TabIndex = 203;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(693, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 19);
            this.label5.TabIndex = 202;
            this.label5.Text = "Task :";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(367, 85);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 19);
            this.label6.TabIndex = 201;
            this.label6.Text = "User Name :";
            // 
            // link_Search_Order_Allocation
            // 
            this.link_Search_Order_Allocation.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_Search_Order_Allocation.Location = new System.Drawing.Point(938, 81);
            this.link_Search_Order_Allocation.Name = "link_Search_Order_Allocation";
            this.link_Search_Order_Allocation.Size = new System.Drawing.Size(124, 30);
            this.link_Search_Order_Allocation.TabIndex = 205;
            this.link_Search_Order_Allocation.Text = "Reallocate";
            this.link_Search_Order_Allocation.UseVisualStyleBackColor = true;
            this.link_Search_Order_Allocation.Click += new System.EventHandler(this.link_Search_Order_Allocation_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1070, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 30);
            this.button1.TabIndex = 206;
            this.button1.Text = "Deallocate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Rework_Search_Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 384);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.link_Search_Order_Allocation);
            this.Controls.Add(this.ddl_Order_Status_Reallocate);
            this.Controls.Add(this.ddl_UserName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rbtn_Completed_Order);
            this.Controls.Add(this.rbn_Rework_Order_Search);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grd_order);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Order_number);
            this.Name = "Rework_Search_Order";
            this.Text = "Rework_Search_Order";
            this.Load += new System.EventHandler(this.Rework_Search_Order_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Order_number;
        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.DataGridViewButtonColumn OrderNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtn_Completed_Order;
        private System.Windows.Forms.RadioButton rbn_Rework_Order_Search;
        private System.Windows.Forms.ComboBox ddl_Order_Status_Reallocate;
        private System.Windows.Forms.ComboBox ddl_UserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button link_Search_Order_Allocation;
        private System.Windows.Forms.Button button1;
    }
}