namespace Ordermanagement_01.InvoiceRep
{
    partial class Invoice_Reports
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
            this.tvwRightSide = new System.Windows.Forms.TreeView();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.ddl_Subprocess = new System.Windows.Forms.ComboBox();
            this.lbl_Subprocess_Status = new System.Windows.Forms.Label();
            this.ddl_Client_Name = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Todate = new System.Windows.Forms.DateTimePicker();
            this.txt_Fromdate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to = new System.Windows.Forms.Label();
            this.lbl_from = new System.Windows.Forms.Label();
            this.crViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btn_Report = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tvwRightSide
            // 
            this.tvwRightSide.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwRightSide.Location = new System.Drawing.Point(13, 49);
            this.tvwRightSide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tvwRightSide.Name = "tvwRightSide";
            this.tvwRightSide.Size = new System.Drawing.Size(190, 623);
            this.tvwRightSide.TabIndex = 69;
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.AutoSize = true;
            this.Lbl_Title.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Title.ForeColor = System.Drawing.Color.SteelBlue;
            this.Lbl_Title.Location = new System.Drawing.Point(511, 5);
            this.Lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(177, 31);
            this.Lbl_Title.TabIndex = 75;
            this.Lbl_Title.Text = "INVOICE REPORTS";
            this.Lbl_Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ddl_Subprocess
            // 
            this.ddl_Subprocess.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Subprocess.FormattingEnabled = true;
            this.ddl_Subprocess.Location = new System.Drawing.Point(772, 92);
            this.ddl_Subprocess.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Subprocess.Name = "ddl_Subprocess";
            this.ddl_Subprocess.Size = new System.Drawing.Size(169, 28);
            this.ddl_Subprocess.TabIndex = 91;
            // 
            // lbl_Subprocess_Status
            // 
            this.lbl_Subprocess_Status.AutoSize = true;
            this.lbl_Subprocess_Status.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Subprocess_Status.Location = new System.Drawing.Point(626, 95);
            this.lbl_Subprocess_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Subprocess_Status.Name = "lbl_Subprocess_Status";
            this.lbl_Subprocess_Status.Size = new System.Drawing.Size(118, 20);
            this.lbl_Subprocess_Status.TabIndex = 90;
            this.lbl_Subprocess_Status.Text = "SubProcessName :";
            // 
            // ddl_Client_Name
            // 
            this.ddl_Client_Name.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Client_Name.FormattingEnabled = true;
            this.ddl_Client_Name.Location = new System.Drawing.Point(406, 91);
            this.ddl_Client_Name.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ddl_Client_Name.Name = "ddl_Client_Name";
            this.ddl_Client_Name.Size = new System.Drawing.Size(173, 28);
            this.ddl_Client_Name.TabIndex = 89;
            this.ddl_Client_Name.SelectedIndexChanged += new System.EventHandler(this.ddl_Client_Name_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(286, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 88;
            this.label1.Text = "Client Name :";
            // 
            // txt_Todate
            // 
            this.txt_Todate.CustomFormat = "MM/DD/YYYY";
            this.txt_Todate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Todate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_Todate.Location = new System.Drawing.Point(772, 51);
            this.txt_Todate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Todate.Name = "txt_Todate";
            this.txt_Todate.Size = new System.Drawing.Size(103, 25);
            this.txt_Todate.TabIndex = 87;
            this.txt_Todate.Value = new System.DateTime(2014, 11, 11, 0, 0, 0, 0);
            // 
            // txt_Fromdate
            // 
            this.txt_Fromdate.CustomFormat = "MM/DD/YYYY";
            this.txt_Fromdate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_Fromdate.Location = new System.Drawing.Point(406, 49);
            this.txt_Fromdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_Fromdate.Name = "txt_Fromdate";
            this.txt_Fromdate.Size = new System.Drawing.Size(106, 25);
            this.txt_Fromdate.TabIndex = 86;
            this.txt_Fromdate.Value = new System.DateTime(2014, 11, 11, 0, 0, 0, 0);
            // 
            // lbl_to
            // 
            this.lbl_to.AutoSize = true;
            this.lbl_to.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_to.Location = new System.Drawing.Point(626, 51);
            this.lbl_to.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_to.Name = "lbl_to";
            this.lbl_to.Size = new System.Drawing.Size(62, 20);
            this.lbl_to.TabIndex = 85;
            this.lbl_to.Text = "To Date :";
            // 
            // lbl_from
            // 
            this.lbl_from.AutoSize = true;
            this.lbl_from.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_from.Location = new System.Drawing.Point(286, 51);
            this.lbl_from.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_from.Name = "lbl_from";
            this.lbl_from.Size = new System.Drawing.Size(77, 20);
            this.lbl_from.TabIndex = 84;
            this.lbl_from.Text = "From Date :";
            // 
            // crViewer
            // 
            this.crViewer.ActiveViewIndex = -1;
            this.crViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crViewer.Location = new System.Drawing.Point(202, 197);
            this.crViewer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.crViewer.Name = "crViewer";
            this.crViewer.ShowGroupTreeButton = false;
            this.crViewer.Size = new System.Drawing.Size(1063, 475);
            this.crViewer.TabIndex = 92;
            // 
            // btn_Report
            // 
            this.btn_Report.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Report.Location = new System.Drawing.Point(604, 156);
            this.btn_Report.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Report.Name = "btn_Report";
            this.btn_Report.Size = new System.Drawing.Size(94, 35);
            this.btn_Report.TabIndex = 93;
            this.btn_Report.Text = "Refresh";
            this.btn_Report.UseVisualStyleBackColor = true;
            this.btn_Report.Click += new System.EventHandler(this.btn_Report_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(717, 156);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 35);
            this.button1.TabIndex = 94;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Invoice_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 673);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Report);
            this.Controls.Add(this.crViewer);
            this.Controls.Add(this.ddl_Subprocess);
            this.Controls.Add(this.lbl_Subprocess_Status);
            this.Controls.Add(this.ddl_Client_Name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Todate);
            this.Controls.Add(this.txt_Fromdate);
            this.Controls.Add(this.lbl_to);
            this.Controls.Add(this.lbl_from);
            this.Controls.Add(this.Lbl_Title);
            this.Controls.Add(this.tvwRightSide);
            this.Name = "Invoice_Reports";
            this.Text = "Invoice_Reports";
            this.Load += new System.EventHandler(this.Invoice_Reports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvwRightSide;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.ComboBox ddl_Subprocess;
        private System.Windows.Forms.Label lbl_Subprocess_Status;
        private System.Windows.Forms.ComboBox ddl_Client_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txt_Todate;
        private System.Windows.Forms.DateTimePicker txt_Fromdate;
        private System.Windows.Forms.Label lbl_to;
        private System.Windows.Forms.Label lbl_from;
        internal CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer;
        private System.Windows.Forms.Button btn_Report;
        private System.Windows.Forms.Button button1;
    }
}