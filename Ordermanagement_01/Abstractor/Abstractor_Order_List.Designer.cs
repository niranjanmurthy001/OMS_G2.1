namespace Ordermanagement_01.Abstractor
{
    partial class Abstractor_Order_List
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label25 = new System.Windows.Forms.Label();
            this.grd_order = new System.Windows.Forms.DataGridView();
            this.lbl_Abstractor_Name = new System.Windows.Forms.Label();
            this.txt_Total = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Pages_Cost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Actual_Cost = new System.Windows.Forms.TextBox();
            this.txt_No_Of_orders = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sub_ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).BeginInit();
            this.SuspendLayout();
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.MediumBlue;
            this.label25.Location = new System.Drawing.Point(320, -1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(356, 31);
            this.label25.TabIndex = 214;
            this.label25.Text = "ABSTRACTOR  ORDERS COST DETAILS ";
            this.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_order.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_order.ColumnHeadersHeight = 40;
            this.grd_order.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.Column1,
            this.Column10,
            this.Client_Name,
            this.Sub_ProcessName,
            this.Order_Type,
            this.Column3,
            this.Column2});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_order.DefaultCellStyle = dataGridViewCellStyle3;
            this.grd_order.Location = new System.Drawing.Point(12, 114);
            this.grd_order.Name = "grd_order";
            this.grd_order.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_order.RowHeadersVisible = false;
            this.grd_order.Size = new System.Drawing.Size(934, 387);
            this.grd_order.TabIndex = 215;
            // 
            // lbl_Abstractor_Name
            // 
            this.lbl_Abstractor_Name.AutoSize = true;
            this.lbl_Abstractor_Name.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Abstractor_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Abstractor_Name.Location = new System.Drawing.Point(484, 30);
            this.lbl_Abstractor_Name.Name = "lbl_Abstractor_Name";
            this.lbl_Abstractor_Name.Size = new System.Drawing.Size(134, 20);
            this.lbl_Abstractor_Name.TabIndex = 216;
            this.lbl_Abstractor_Name.Text = "ABSTRACTOR NAME";
            this.lbl_Abstractor_Name.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txt_Total
            // 
            this.txt_Total.Enabled = false;
            this.txt_Total.ForeColor = System.Drawing.Color.MediumBlue;
            this.txt_Total.Location = new System.Drawing.Point(718, 71);
            this.txt_Total.Name = "txt_Total";
            this.txt_Total.Size = new System.Drawing.Size(159, 20);
            this.txt_Total.TabIndex = 224;
            this.txt_Total.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(674, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 17);
            this.label5.TabIndex = 223;
            this.label5.Text = "Total:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Visible = false;
            // 
            // txt_Pages_Cost
            // 
            this.txt_Pages_Cost.Enabled = false;
            this.txt_Pages_Cost.ForeColor = System.Drawing.Color.MediumBlue;
            this.txt_Pages_Cost.Location = new System.Drawing.Point(544, 73);
            this.txt_Pages_Cost.Name = "txt_Pages_Cost";
            this.txt_Pages_Cost.Size = new System.Drawing.Size(124, 20);
            this.txt_Pages_Cost.TabIndex = 222;
            this.txt_Pages_Cost.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(446, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 221;
            this.label3.Text = "Total Pages Cost:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Visible = false;
            // 
            // txt_Actual_Cost
            // 
            this.txt_Actual_Cost.Enabled = false;
            this.txt_Actual_Cost.ForeColor = System.Drawing.Color.MediumBlue;
            this.txt_Actual_Cost.Location = new System.Drawing.Point(321, 73);
            this.txt_Actual_Cost.Name = "txt_Actual_Cost";
            this.txt_Actual_Cost.Size = new System.Drawing.Size(124, 20);
            this.txt_Actual_Cost.TabIndex = 220;
            // 
            // txt_No_Of_orders
            // 
            this.txt_No_Of_orders.Enabled = false;
            this.txt_No_Of_orders.ForeColor = System.Drawing.Color.OrangeRed;
            this.txt_No_Of_orders.Location = new System.Drawing.Point(123, 73);
            this.txt_No_Of_orders.Name = "txt_No_Of_orders";
            this.txt_No_Of_orders.Size = new System.Drawing.Size(96, 20);
            this.txt_No_Of_orders.TabIndex = 219;
            this.txt_No_Of_orders.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(220, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 218;
            this.label4.Text = "Total Actual Cost:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 217;
            this.label2.Text = "Total No of Orders:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(348, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 225;
            this.label1.Text = "ABSTRACTOR NAME:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.SNo.FillWeight = 91.37055F;
            this.SNo.HeaderText = "S. No";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Order Number";
            this.Column1.Name = "Column1";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Month";
            this.Column10.Name = "Column10";
            // 
            // Client_Name
            // 
            this.Client_Name.FillWeight = 61.35016F;
            this.Client_Name.HeaderText = "Actual Cost";
            this.Client_Name.Name = "Client_Name";
            this.Client_Name.ReadOnly = true;
            // 
            // Sub_ProcessName
            // 
            this.Sub_ProcessName.FillWeight = 124.9346F;
            this.Sub_ProcessName.HeaderText = "Pages Cost";
            this.Sub_ProcessName.Name = "Sub_ProcessName";
            this.Sub_ProcessName.ReadOnly = true;
            this.Sub_ProcessName.Visible = false;
            // 
            // Order_Type
            // 
            this.Order_Type.FillWeight = 130.2978F;
            this.Order_Type.HeaderText = "Completed_Date";
            this.Order_Type.Name = "Order_Type";
            this.Order_Type.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Payment_Status";
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // Abstractor_Order_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 518);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Total);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Pages_Cost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Actual_Cost);
            this.Controls.Add(this.txt_No_Of_orders);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_Abstractor_Name);
            this.Controls.Add(this.grd_order);
            this.Controls.Add(this.label25);
            this.Name = "Abstractor_Order_List";
            this.Text = "Abstractor_Order_List";
            this.Load += new System.EventHandler(this.Abstractor_Order_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_order)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.DataGridView grd_order;
        private System.Windows.Forms.Label lbl_Abstractor_Name;
        private System.Windows.Forms.TextBox txt_Total;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Pages_Cost;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Actual_Cost;
        private System.Windows.Forms.TextBox txt_No_Of_orders;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sub_ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}