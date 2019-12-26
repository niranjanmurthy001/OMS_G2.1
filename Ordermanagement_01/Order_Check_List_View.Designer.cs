namespace Ordermanagement_01
{
    partial class Order_Check_List_View
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rbtn_Search = new System.Windows.Forms.RadioButton();
            this.rbt_Search_Qc = new System.Windows.Forms.RadioButton();
            this.rbtn_Typing = new System.Windows.Forms.RadioButton();
            this.rbtn_Typing_Qc = new System.Windows.Forms.RadioButton();
            this.rbtn_Upload = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ddl_Work_Type = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Doc_Export = new System.Windows.Forms.Button();
            this.Gridview_Document_List = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbtn_Document_Search_Qc = new System.Windows.Forms.RadioButton();
            this.rbtn_Document_Search = new System.Windows.Forms.RadioButton();
            this.rbtn_Final_Qc = new System.Windows.Forms.RadioButton();
            this.rbtn_Exception = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gridview_Document_List)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtn_Search
            // 
            this.rbtn_Search.AutoSize = true;
            this.rbtn_Search.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Search.Location = new System.Drawing.Point(320, 6);
            this.rbtn_Search.Name = "rbtn_Search";
            this.rbtn_Search.Size = new System.Drawing.Size(67, 24);
            this.rbtn_Search.TabIndex = 88;
            this.rbtn_Search.Text = "Search";
            this.rbtn_Search.UseVisualStyleBackColor = true;
            this.rbtn_Search.CheckedChanged += new System.EventHandler(this.rbtn_Search_CheckedChanged);
            // 
            // rbt_Search_Qc
            // 
            this.rbt_Search_Qc.AutoSize = true;
            this.rbt_Search_Qc.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbt_Search_Qc.Location = new System.Drawing.Point(403, 6);
            this.rbt_Search_Qc.Name = "rbt_Search_Qc";
            this.rbt_Search_Qc.Size = new System.Drawing.Size(89, 24);
            this.rbt_Search_Qc.TabIndex = 89;
            this.rbt_Search_Qc.Text = "Search QC";
            this.rbt_Search_Qc.UseVisualStyleBackColor = true;
            this.rbt_Search_Qc.CheckedChanged += new System.EventHandler(this.rbt_Search_Qc_CheckedChanged);
            // 
            // rbtn_Typing
            // 
            this.rbtn_Typing.AutoSize = true;
            this.rbtn_Typing.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Typing.Location = new System.Drawing.Point(510, 6);
            this.rbtn_Typing.Name = "rbtn_Typing";
            this.rbtn_Typing.Size = new System.Drawing.Size(70, 24);
            this.rbtn_Typing.TabIndex = 90;
            this.rbtn_Typing.Text = "Typing";
            this.rbtn_Typing.UseVisualStyleBackColor = true;
            this.rbtn_Typing.CheckedChanged += new System.EventHandler(this.rbtn_Typing_CheckedChanged);
            // 
            // rbtn_Typing_Qc
            // 
            this.rbtn_Typing_Qc.AutoSize = true;
            this.rbtn_Typing_Qc.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Typing_Qc.Location = new System.Drawing.Point(605, 6);
            this.rbtn_Typing_Qc.Name = "rbtn_Typing_Qc";
            this.rbtn_Typing_Qc.Size = new System.Drawing.Size(90, 24);
            this.rbtn_Typing_Qc.TabIndex = 91;
            this.rbtn_Typing_Qc.Text = "Typing Qc";
            this.rbtn_Typing_Qc.UseVisualStyleBackColor = true;
            this.rbtn_Typing_Qc.CheckedChanged += new System.EventHandler(this.rbtn_Typing_Qc_CheckedChanged);
            // 
            // rbtn_Upload
            // 
            this.rbtn_Upload.AutoSize = true;
            this.rbtn_Upload.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Upload.Location = new System.Drawing.Point(892, 6);
            this.rbtn_Upload.Name = "rbtn_Upload";
            this.rbtn_Upload.Size = new System.Drawing.Size(71, 24);
            this.rbtn_Upload.TabIndex = 92;
            this.rbtn_Upload.Text = "Upload";
            this.rbtn_Upload.UseVisualStyleBackColor = true;
            this.rbtn_Upload.CheckedChanged += new System.EventHandler(this.rbtn_Upload_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1258, 642);
            this.tabControl1.TabIndex = 94;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1250, 606);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Check List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ddl_Work_Type
            // 
            this.ddl_Work_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Work_Type.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Work_Type.FormattingEnabled = true;
            this.ddl_Work_Type.ItemHeight = 20;
            this.ddl_Work_Type.Items.AddRange(new object[] {
            "Live",
            "Rework",
            "SuperQc "});
            this.ddl_Work_Type.Location = new System.Drawing.Point(127, 3);
            this.ddl_Work_Type.Name = "ddl_Work_Type";
            this.ddl_Work_Type.Size = new System.Drawing.Size(161, 28);
            this.ddl_Work_Type.TabIndex = 130;
            this.ddl_Work_Type.SelectedIndexChanged += new System.EventHandler(this.ddl_Work_Type_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 20);
            this.label15.TabIndex = 129;
            this.label15.Text = "Select Work Type:";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1238, 544);
            this.crystalReportViewer1.TabIndex = 93;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_Doc_Export);
            this.tabPage2.Controls.Add(this.Gridview_Document_List);
            this.tabPage2.Controls.Add(this.rbtn_Document_Search_Qc);
            this.tabPage2.Controls.Add(this.rbtn_Document_Search);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1120, 533);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Document List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_Doc_Export
            // 
            this.btn_Doc_Export.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Doc_Export.Location = new System.Drawing.Point(220, 9);
            this.btn_Doc_Export.Name = "btn_Doc_Export";
            this.btn_Doc_Export.Size = new System.Drawing.Size(95, 35);
            this.btn_Doc_Export.TabIndex = 95;
            this.btn_Doc_Export.Text = "Export";
            this.btn_Doc_Export.UseVisualStyleBackColor = true;
            this.btn_Doc_Export.Click += new System.EventHandler(this.btn_Doc_Export_Click);
            // 
            // Gridview_Document_List
            // 
            this.Gridview_Document_List.AllowUserToAddRows = false;
            this.Gridview_Document_List.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.Gridview_Document_List.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Gridview_Document_List.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Gridview_Document_List.ColumnHeadersHeight = 29;
            this.Gridview_Document_List.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.Gridview_Document_List.Location = new System.Drawing.Point(30, 50);
            this.Gridview_Document_List.Name = "Gridview_Document_List";
            this.Gridview_Document_List.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Gridview_Document_List.RowHeadersVisible = false;
            this.Gridview_Document_List.Size = new System.Drawing.Size(762, 407);
            this.Gridview_Document_List.TabIndex = 94;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 29.68796F;
            this.dataGridViewTextBoxColumn1.HeaderText = "S. No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 55;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Date";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "User";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Type of Docs Found";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 300;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "No. of docs found";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // rbtn_Document_Search_Qc
            // 
            this.rbtn_Document_Search_Qc.AutoSize = true;
            this.rbtn_Document_Search_Qc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Document_Search_Qc.Location = new System.Drawing.Point(113, 20);
            this.rbtn_Document_Search_Qc.Name = "rbtn_Document_Search_Qc";
            this.rbtn_Document_Search_Qc.Size = new System.Drawing.Size(86, 17);
            this.rbtn_Document_Search_Qc.TabIndex = 91;
            this.rbtn_Document_Search_Qc.Text = "Search QC";
            this.rbtn_Document_Search_Qc.UseVisualStyleBackColor = true;
            this.rbtn_Document_Search_Qc.CheckedChanged += new System.EventHandler(this.rbtn_Document_Search_Qc_CheckedChanged);
            // 
            // rbtn_Document_Search
            // 
            this.rbtn_Document_Search.AutoSize = true;
            this.rbtn_Document_Search.Checked = true;
            this.rbtn_Document_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Document_Search.Location = new System.Drawing.Point(30, 20);
            this.rbtn_Document_Search.Name = "rbtn_Document_Search";
            this.rbtn_Document_Search.Size = new System.Drawing.Size(65, 17);
            this.rbtn_Document_Search.TabIndex = 90;
            this.rbtn_Document_Search.TabStop = true;
            this.rbtn_Document_Search.Text = "Search";
            this.rbtn_Document_Search.UseVisualStyleBackColor = true;
            this.rbtn_Document_Search.CheckedChanged += new System.EventHandler(this.rbtn_Document_Search_CheckedChanged);
            // 
            // rbtn_Final_Qc
            // 
            this.rbtn_Final_Qc.AutoSize = true;
            this.rbtn_Final_Qc.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Final_Qc.Location = new System.Drawing.Point(701, 6);
            this.rbtn_Final_Qc.Name = "rbtn_Final_Qc";
            this.rbtn_Final_Qc.Size = new System.Drawing.Size(77, 24);
            this.rbtn_Final_Qc.TabIndex = 131;
            this.rbtn_Final_Qc.Text = "Final Qc";
            this.rbtn_Final_Qc.UseVisualStyleBackColor = true;
            this.rbtn_Final_Qc.CheckedChanged += new System.EventHandler(this.rbtn_Final_Qc_CheckedChanged);
            // 
            // rbtn_Exception
            // 
            this.rbtn_Exception.AutoSize = true;
            this.rbtn_Exception.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Exception.Location = new System.Drawing.Point(787, 6);
            this.rbtn_Exception.Name = "rbtn_Exception";
            this.rbtn_Exception.Size = new System.Drawing.Size(87, 24);
            this.rbtn_Exception.TabIndex = 132;
            this.rbtn_Exception.Text = "Exception";
            this.rbtn_Exception.UseVisualStyleBackColor = true;
            this.rbtn_Exception.CheckedChanged += new System.EventHandler(this.rbtn_Exception_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1244, 600);
            this.tableLayoutPanel1.TabIndex = 133;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ddl_Work_Type);
            this.panel1.Controls.Add(this.rbtn_Exception);
            this.panel1.Controls.Add(this.rbtn_Typing_Qc);
            this.panel1.Controls.Add(this.rbtn_Final_Qc);
            this.panel1.Controls.Add(this.rbtn_Typing);
            this.panel1.Controls.Add(this.rbtn_Upload);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.rbtn_Search);
            this.panel1.Controls.Add(this.rbt_Search_Qc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1238, 44);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.crystalReportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1238, 544);
            this.panel2.TabIndex = 1;
            // 
            // Order_Check_List_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 642);
            this.Controls.Add(this.tabControl1);
            this.Name = "Order_Check_List_View";
            this.Text = "Order_Check_List_View";
            this.Load += new System.EventHandler(this.Order_Check_List_View_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gridview_Document_List)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtn_Search;
        private System.Windows.Forms.RadioButton rbt_Search_Qc;
        private System.Windows.Forms.RadioButton rbtn_Typing;
        private System.Windows.Forms.RadioButton rbtn_Typing_Qc;
        private System.Windows.Forms.RadioButton rbtn_Upload;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton rbtn_Document_Search_Qc;
        private System.Windows.Forms.RadioButton rbtn_Document_Search;
        private System.Windows.Forms.DataGridView Gridview_Document_List;
        private System.Windows.Forms.Button btn_Doc_Export;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox ddl_Work_Type;
        private System.Windows.Forms.RadioButton rbtn_Exception;
        private System.Windows.Forms.RadioButton rbtn_Final_Qc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}