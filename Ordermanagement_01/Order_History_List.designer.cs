namespace Ordermanagement_01
{
    partial class Order_History_List
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
            this.lbl_Order_Number = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Clientname = new System.Windows.Forms.Label();
            this.lbl_Subprocess = new System.Windows.Forms.Label();
            this.lbl_State = new System.Windows.Forms.Label();
            this.lbl_County = new System.Windows.Forms.Label();
            this.Grid_History = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_History)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Order_Number
            // 
            this.lbl_Order_Number.AutoSize = true;
            this.lbl_Order_Number.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Order_Number.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_Order_Number.Location = new System.Drawing.Point(379, 12);
            this.lbl_Order_Number.Name = "lbl_Order_Number";
            this.lbl_Order_Number.Size = new System.Drawing.Size(138, 31);
            this.lbl_Order_Number.TabIndex = 5;
            this.lbl_Order_Number.Text = "HISTORY LIST";
            this.lbl_Order_Number.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label3.Location = new System.Drawing.Point(7, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "CLIENT NAME:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label4.Location = new System.Drawing.Point(7, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "SUBPROCESS NAME:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label5.Location = new System.Drawing.Point(731, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 26);
            this.label5.TabIndex = 9;
            this.label5.Text = "STATE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label6.Location = new System.Drawing.Point(730, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 26);
            this.label6.TabIndex = 10;
            this.label6.Text = "COUNTY:";
            // 
            // lbl_Clientname
            // 
            this.lbl_Clientname.AutoSize = true;
            this.lbl_Clientname.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Clientname.Location = new System.Drawing.Point(183, 41);
            this.lbl_Clientname.Name = "lbl_Clientname";
            this.lbl_Clientname.Size = new System.Drawing.Size(73, 20);
            this.lbl_Clientname.TabIndex = 12;
            this.lbl_Clientname.Text = "Clientname";
            // 
            // lbl_Subprocess
            // 
            this.lbl_Subprocess.AutoSize = true;
            this.lbl_Subprocess.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Subprocess.Location = new System.Drawing.Point(183, 79);
            this.lbl_Subprocess.Name = "lbl_Subprocess";
            this.lbl_Subprocess.Size = new System.Drawing.Size(77, 20);
            this.lbl_Subprocess.TabIndex = 13;
            this.lbl_Subprocess.Text = "Subprocess";
            // 
            // lbl_State
            // 
            this.lbl_State.AutoSize = true;
            this.lbl_State.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_State.Location = new System.Drawing.Point(844, 45);
            this.lbl_State.Name = "lbl_State";
            this.lbl_State.Size = new System.Drawing.Size(40, 20);
            this.lbl_State.TabIndex = 14;
            this.lbl_State.Text = "State";
            // 
            // lbl_County
            // 
            this.lbl_County.AutoSize = true;
            this.lbl_County.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_County.Location = new System.Drawing.Point(844, 83);
            this.lbl_County.Name = "lbl_County";
            this.lbl_County.Size = new System.Drawing.Size(53, 20);
            this.lbl_County.TabIndex = 15;
            this.lbl_County.Text = "County";
            // 
            // Grid_History
            // 
            this.Grid_History.AllowUserToAddRows = false;
            this.Grid_History.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grid_History.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.Grid_History.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_History.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid_History.ColumnHeadersHeight = 30;
            this.Grid_History.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column5,
            this.Column2,
            this.Column3,
            this.Column8,
            this.Column6,
            this.Column7});
            this.Grid_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_History.Location = new System.Drawing.Point(3, 153);
            this.Grid_History.Name = "Grid_History";
            this.Grid_History.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_History.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Grid_History.RowHeadersVisible = false;
            this.Grid_History.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Grid_History.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.Grid_History.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Grid_History.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Grid_History.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.Grid_History.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.Grid_History.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid_History.RowTemplate.Height = 25;
            this.Grid_History.Size = new System.Drawing.Size(1149, 456);
            this.Grid_History.TabIndex = 16;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 35.533F;
            this.Column1.HeaderText = "S. No";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 91.53642F;
            this.Column4.HeaderText = "ASSIGNEE";
            this.Column4.Name = "Column4";
            this.Column4.Width = 117;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 109.8005F;
            this.Column5.HeaderText = "ASSIGNER";
            this.Column5.Name = "Column5";
            this.Column5.Width = 141;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 81.26336F;
            this.Column2.HeaderText = "ORDER TASK";
            this.Column2.Name = "Column2";
            this.Column2.Width = 104;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 141.3822F;
            this.Column3.HeaderText = "PROGRESS TYPE";
            this.Column3.Name = "Column3";
            this.Column3.Width = 181;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "ORDER TYPE";
            this.Column8.Name = "Column8";
            this.Column8.Width = 127;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 146.0468F;
            this.Column6.HeaderText = "STAGE";
            this.Column6.Name = "Column6";
            this.Column6.Width = 187;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 94.4379F;
            this.Column7.HeaderText = "DATE";
            this.Column7.Name = "Column7";
            this.Column7.Width = 121;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Grid_History, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1155, 612);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Clientname);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_Order_Number);
            this.panel1.Controls.Add(this.lbl_County);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbl_State);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lbl_Subprocess);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 144);
            this.panel1.TabIndex = 0;
            // 
            // Order_History_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 612);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Order_History_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order_History_List";
            this.Load += new System.EventHandler(this.Order_History_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_History)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Order_Number;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Clientname;
        private System.Windows.Forms.Label lbl_Subprocess;
        private System.Windows.Forms.Label lbl_State;
        private System.Windows.Forms.Label lbl_County;
        private System.Windows.Forms.DataGridView Grid_History;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}