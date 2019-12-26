namespace Ordermanagement_01.Employee
{
    partial class Search_NotePad
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
            this.components = new System.ComponentModel.Container();
            this.grp_Header_Text = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_rich_Note_Details = new DevExpress.XtraRichEdit.RichEditControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.Default_Look_Confirmation = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grp_Header_Text)).BeginInit();
            this.grp_Header_Text.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Header_Text
            // 
            this.grp_Header_Text.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grp_Header_Text.Appearance.Options.UseFont = true;
            this.grp_Header_Text.Controls.Add(this.tableLayoutPanel1);
            this.grp_Header_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Header_Text.Location = new System.Drawing.Point(0, 0);
            this.grp_Header_Text.Name = "grp_Header_Text";
            this.grp_Header_Text.Size = new System.Drawing.Size(1042, 632);
            this.grp_Header_Text.TabIndex = 0;
            this.grp_Header_Text.Text = "Search Note Pad";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.txt_rich_Note_Details, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1038, 610);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txt_rich_Note_Details
            // 
            this.txt_rich_Note_Details.Appearance.Text.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_rich_Note_Details.Appearance.Text.Options.UseFont = true;
            this.txt_rich_Note_Details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_rich_Note_Details.Location = new System.Drawing.Point(3, 3);
            this.txt_rich_Note_Details.LookAndFeel.SkinName = "Office 2010 Blue";
            this.txt_rich_Note_Details.Name = "txt_rich_Note_Details";
            this.txt_rich_Note_Details.Size = new System.Drawing.Size(1032, 554);
            this.txt_rich_Note_Details.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 563);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 44);
            this.panel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSubmit);
            this.flowLayoutPanel1.Controls.Add(this.btnClear);
            this.flowLayoutPanel1.Controls.Add(this.btn_Export);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(676, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(356, 44);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Appearance.BackColor = System.Drawing.Color.White;
            this.btnSubmit.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Appearance.Options.UseBackColor = true;
            this.btnSubmit.Appearance.Options.UseFont = true;
            this.btnSubmit.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.AppearanceHovered.Options.UseBackColor = true;
            this.btnSubmit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnSubmit.Location = new System.Drawing.Point(3, 3);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(114, 35);
            this.btnSubmit.TabIndex = 557;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.BackColor = System.Drawing.Color.White;
            this.btnClear.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Appearance.Options.UseBackColor = true;
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.AppearanceHovered.Options.UseBackColor = true;
            this.btnClear.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnClear.Location = new System.Drawing.Point(123, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 35);
            this.btnClear.TabIndex = 558;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Appearance.BackColor = System.Drawing.Color.White;
            this.btn_Export.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Appearance.Options.UseBackColor = true;
            this.btn_Export.Appearance.Options.UseFont = true;
            this.btn_Export.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.btn_Export.AppearanceHovered.Options.UseBackColor = true;
            this.btn_Export.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btn_Export.Location = new System.Drawing.Point(238, 3);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(114, 35);
            this.btn_Export.TabIndex = 558;
            this.btn_Export.Text = "Export";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // Default_Look_Confirmation
            // 
            this.Default_Look_Confirmation.EnableBonusSkins = true;
            this.Default_Look_Confirmation.LookAndFeel.SkinName = "Office 2013";
            // 
            // Search_NotePad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 632);
            this.Controls.Add(this.grp_Header_Text);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Search_NotePad";
            this.Text = "Search_NotePad";
            this.Load += new System.EventHandler(this.Search_NotePad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grp_Header_Text)).EndInit();
            this.grp_Header_Text.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grp_Header_Text;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraRichEdit.RichEditControl txt_rich_Note_Details;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.LookAndFeel.DefaultLookAndFeel Default_Look_Confirmation;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
    }
}