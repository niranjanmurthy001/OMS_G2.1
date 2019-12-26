namespace Ordermanagement_01.AutoAllocation
{
    partial class Auto_Allocation_Order_Type_Priority
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
            this.Grid_Order_Type_Abs = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Order_Type_Down = new System.Windows.Forms.Button();
            this.btn_Ordertype_Up = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Order_Type_Abs)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid_Order_Type_Abs
            // 
            this.Grid_Order_Type_Abs.AllowUserToAddRows = false;
            this.Grid_Order_Type_Abs.AllowUserToDeleteRows = false;
            this.Grid_Order_Type_Abs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Grid_Order_Type_Abs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Grid_Order_Type_Abs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grid_Order_Type_Abs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.Grid_Order_Type_Abs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Order_Type_Abs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid_Order_Type_Abs.ColumnHeadersHeight = 30;
            this.Grid_Order_Type_Abs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Column4});
            this.Grid_Order_Type_Abs.Location = new System.Drawing.Point(123, 87);
            this.Grid_Order_Type_Abs.Name = "Grid_Order_Type_Abs";
            this.Grid_Order_Type_Abs.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Ebrima", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Order_Type_Abs.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Grid_Order_Type_Abs.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_Order_Type_Abs.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.Grid_Order_Type_Abs.RowTemplate.Height = 25;
            this.Grid_Order_Type_Abs.Size = new System.Drawing.Size(231, 192);
            this.Grid_Order_Type_Abs.TabIndex = 299;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "Chk";
            this.dataGridViewCheckBoxColumn3.FillWeight = 28.80222F;
            this.dataGridViewCheckBoxColumn3.HeaderText = "";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 196.2088F;
            this.dataGridViewTextBoxColumn3.HeaderText = "ORDER TYPE ABR";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "OrderType_ABS_Id";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "P_Id";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // btn_Order_Type_Down
            // 
            this.btn_Order_Type_Down.BackgroundImage = global::Ordermanagement_01.Properties.Resources.Down;
            this.btn_Order_Type_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Order_Type_Down.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.btn_Order_Type_Down.Location = new System.Drawing.Point(95, 169);
            this.btn_Order_Type_Down.Name = "btn_Order_Type_Down";
            this.btn_Order_Type_Down.Size = new System.Drawing.Size(24, 22);
            this.btn_Order_Type_Down.TabIndex = 301;
            this.btn_Order_Type_Down.UseVisualStyleBackColor = true;
            // 
            // btn_Ordertype_Up
            // 
            this.btn_Ordertype_Up.BackgroundImage = global::Ordermanagement_01.Properties.Resources.Up;
            this.btn_Ordertype_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Ordertype_Up.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.btn_Ordertype_Up.Location = new System.Drawing.Point(95, 137);
            this.btn_Ordertype_Up.Name = "btn_Ordertype_Up";
            this.btn_Ordertype_Up.Size = new System.Drawing.Size(24, 22);
            this.btn_Ordertype_Up.TabIndex = 300;
            this.btn_Ordertype_Up.UseVisualStyleBackColor = true;
            // 
            // Auto_Allocation_Order_Type_Priority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 431);
            this.Controls.Add(this.btn_Order_Type_Down);
            this.Controls.Add(this.btn_Ordertype_Up);
            this.Controls.Add(this.Grid_Order_Type_Abs);
            this.Name = "Auto_Allocation_Order_Type_Priority";
            this.Text = "Auto_Allocation_Order_Type_Priority";
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Order_Type_Abs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Order_Type_Down;
        private System.Windows.Forms.Button btn_Ordertype_Up;
        private System.Windows.Forms.DataGridView Grid_Order_Type_Abs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}