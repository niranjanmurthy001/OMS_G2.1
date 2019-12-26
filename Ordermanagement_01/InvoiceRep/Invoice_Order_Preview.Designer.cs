namespace Ordermanagement_01.InvoiceRep
{
    partial class Invoice_Order_Preview
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
            this.crViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btn_New_Invoice = new System.Windows.Forms.Button();
            this.btn_Merge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crViewer
            // 
            this.crViewer.ActiveViewIndex = -1;
            this.crViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer.Location = new System.Drawing.Point(4, 44);
            this.crViewer.Name = "crViewer";
            this.crViewer.Size = new System.Drawing.Size(1089, 548);
            this.crViewer.TabIndex = 5;
            // 
            // btn_New_Invoice
            // 
            this.btn_New_Invoice.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_New_Invoice.Location = new System.Drawing.Point(994, 5);
            this.btn_New_Invoice.Name = "btn_New_Invoice";
            this.btn_New_Invoice.Size = new System.Drawing.Size(92, 33);
            this.btn_New_Invoice.TabIndex = 196;
            this.btn_New_Invoice.Text = "Export";
            this.btn_New_Invoice.UseVisualStyleBackColor = true;
            this.btn_New_Invoice.Visible = false;
            this.btn_New_Invoice.Click += new System.EventHandler(this.btn_New_Invoice_Click);
            // 
            // btn_Merge
            // 
            this.btn_Merge.Font = new System.Drawing.Font("Ebrima", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Merge.Location = new System.Drawing.Point(4, 5);
            this.btn_Merge.Name = "btn_Merge";
            this.btn_Merge.Size = new System.Drawing.Size(85, 33);
            this.btn_Merge.TabIndex = 197;
            this.btn_Merge.Text = "Merge";
            this.btn_Merge.UseVisualStyleBackColor = true;
            this.btn_Merge.Visible = false;
            this.btn_Merge.Click += new System.EventHandler(this.btn_Merge_Click);
            // 
            // Invoice_Order_Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 596);
            this.Controls.Add(this.btn_Merge);
            this.Controls.Add(this.btn_New_Invoice);
            this.Controls.Add(this.crViewer);
            this.Name = "Invoice_Order_Preview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice_Order_Preview";
            this.Load += new System.EventHandler(this.Invoice_Order_Preview_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer;
        private System.Windows.Forms.Button btn_New_Invoice;
        private System.Windows.Forms.Button btn_Merge;
    }
}