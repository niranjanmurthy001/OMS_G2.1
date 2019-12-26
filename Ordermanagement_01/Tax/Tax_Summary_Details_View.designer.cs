namespace Ordermanagement_01.Tax
{
    partial class Tax_Summary_Details_View
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
            this.groupControlDetails = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlTaxDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewTaxDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Client_Order_Number = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.Client_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Client_Number = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Sub_ProcessName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Subprocess_Number = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Order_Type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Assigned_Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Borrower_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.State = new DevExpress.XtraGrid.Columns.GridColumn();
            this.County = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Address = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tax_Task = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tax_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grd_Si_No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Order_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetails)).BeginInit();
            this.groupControlDetails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTaxDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTaxDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlDetails
            // 
            this.groupControlDetails.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.groupControlDetails.Appearance.Options.UseFont = true;
            this.groupControlDetails.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControlDetails.AppearanceCaption.Options.UseFont = true;
            this.groupControlDetails.AutoSize = true;
            this.groupControlDetails.Controls.Add(this.tableLayoutPanel1);
            this.groupControlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlDetails.Location = new System.Drawing.Point(0, 0);
            this.groupControlDetails.LookAndFeel.SkinName = "Office 2010 Blue";
            this.groupControlDetails.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControlDetails.Name = "groupControlDetails";
            this.groupControlDetails.Size = new System.Drawing.Size(1223, 484);
            this.groupControlDetails.TabIndex = 1;
            this.groupControlDetails.Text = "Tax Summary";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gridControlTaxDetails, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1219, 461);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Appearance.BackColor = System.Drawing.Color.White;
            this.btnExport.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseBackColor = true;
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.AppearanceHovered.Options.UseBackColor = true;
            this.btnExport.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExport.Location = new System.Drawing.Point(1066, 424);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 34);
            this.btnExport.TabIndex = 559;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // gridControlTaxDetails
            // 
            this.gridControlTaxDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTaxDetails.Location = new System.Drawing.Point(3, 3);
            this.gridControlTaxDetails.MainView = this.gridViewTaxDetails;
            this.gridControlTaxDetails.Name = "gridControlTaxDetails";
            this.gridControlTaxDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.gridControlTaxDetails.Size = new System.Drawing.Size(1213, 415);
            this.gridControlTaxDetails.TabIndex = 1;
            this.gridControlTaxDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTaxDetails});
            // 
            // gridViewTaxDetails
            // 
            this.gridViewTaxDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Client_Order_Number,
            this.Client_Name,
            this.Client_Number,
            this.Sub_ProcessName,
            this.Subprocess_Number,
            this.Order_Type,
            this.Assigned_Date,
            this.Borrower_Name,
            this.State,
            this.County,
            this.Address,
            this.Tax_Task,
            this.Tax_Status,
            this.grd_Si_No,
            this.Order_ID});
            this.gridViewTaxDetails.GridControl = this.gridControlTaxDetails;
            this.gridViewTaxDetails.Name = "gridViewTaxDetails";
            this.gridViewTaxDetails.OptionsBehavior.Editable = false;
            this.gridViewTaxDetails.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridViewTaxDetails.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewTaxDetails_RowCellClick);
            // 
            // Client_Order_Number
            // 
            this.Client_Order_Number.Caption = "Order Number";
            this.Client_Order_Number.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.Client_Order_Number.FieldName = "Client_Order_Number";
            this.Client_Order_Number.Name = "Client_Order_Number";
            this.Client_Order_Number.Visible = true;
            this.Client_Order_Number.VisibleIndex = 1;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // Client_Name
            // 
            this.Client_Name.Caption = "Client Name";
            this.Client_Name.FieldName = "Client_Name";
            this.Client_Name.Name = "Client_Name";
            this.Client_Name.Visible = true;
            this.Client_Name.VisibleIndex = 2;
            // 
            // Client_Number
            // 
            this.Client_Number.Caption = "Client Number";
            this.Client_Number.FieldName = "Client_Number";
            this.Client_Number.Name = "Client_Number";
            this.Client_Number.Visible = true;
            this.Client_Number.VisibleIndex = 3;
            // 
            // Sub_ProcessName
            // 
            this.Sub_ProcessName.Caption = "Sub Client";
            this.Sub_ProcessName.FieldName = "Sub_ProcessName";
            this.Sub_ProcessName.Name = "Sub_ProcessName";
            this.Sub_ProcessName.Visible = true;
            this.Sub_ProcessName.VisibleIndex = 4;
            // 
            // Subprocess_Number
            // 
            this.Subprocess_Number.Caption = "Sub Client No";
            this.Subprocess_Number.FieldName = "Subprocess_Number";
            this.Subprocess_Number.Name = "Subprocess_Number";
            this.Subprocess_Number.Visible = true;
            this.Subprocess_Number.VisibleIndex = 5;
            // 
            // Order_Type
            // 
            this.Order_Type.Caption = "Order Type";
            this.Order_Type.FieldName = "Order_Type";
            this.Order_Type.Name = "Order_Type";
            this.Order_Type.Visible = true;
            this.Order_Type.VisibleIndex = 6;
            // 
            // Assigned_Date
            // 
            this.Assigned_Date.Caption = "Assigned Date";
            this.Assigned_Date.FieldName = "Assigned_Date";
            this.Assigned_Date.Name = "Assigned_Date";
            this.Assigned_Date.Visible = true;
            this.Assigned_Date.VisibleIndex = 8;
            // 
            // Borrower_Name
            // 
            this.Borrower_Name.Caption = "Borrower Name";
            this.Borrower_Name.FieldName = "Borrower_Name";
            this.Borrower_Name.Name = "Borrower_Name";
            this.Borrower_Name.Visible = true;
            this.Borrower_Name.VisibleIndex = 7;
            // 
            // State
            // 
            this.State.Caption = "State";
            this.State.FieldName = "State";
            this.State.Name = "State";
            this.State.Visible = true;
            this.State.VisibleIndex = 9;
            // 
            // County
            // 
            this.County.Caption = "County";
            this.County.FieldName = "County";
            this.County.Name = "County";
            this.County.Visible = true;
            this.County.VisibleIndex = 10;
            // 
            // Address
            // 
            this.Address.Caption = "Address";
            this.Address.FieldName = "Address";
            this.Address.Name = "Address";
            this.Address.Visible = true;
            this.Address.VisibleIndex = 11;
            // 
            // Tax_Task
            // 
            this.Tax_Task.Caption = "Tax Task";
            this.Tax_Task.FieldName = "Tax_Task";
            this.Tax_Task.Name = "Tax_Task";
            this.Tax_Task.Visible = true;
            this.Tax_Task.VisibleIndex = 12;
            // 
            // Tax_Status
            // 
            this.Tax_Status.Caption = "Tax Status";
            this.Tax_Status.FieldName = "Tax_Status";
            this.Tax_Status.Name = "Tax_Status";
            this.Tax_Status.Visible = true;
            this.Tax_Status.VisibleIndex = 13;
            // 
            // grd_Si_No
            // 
            this.grd_Si_No.Caption = "SI NO";
            this.grd_Si_No.FieldName = "RowNum";
            this.grd_Si_No.Name = "grd_Si_No";
            this.grd_Si_No.Visible = true;
            this.grd_Si_No.VisibleIndex = 0;
            // 
            // Order_ID
            // 
            this.Order_ID.Caption = "gridColumn1";
            this.Order_ID.FieldName = "Order_ID";
            this.Order_ID.Name = "Order_ID";
            // 
            // Tax_Summary_Details_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 484);
            this.Controls.Add(this.groupControlDetails);
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Tax_Summary_Details_View";
            this.Text = "Tax_Summary_Details_View";
            this.Load += new System.EventHandler(this.Tax_Summary_Details_View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlDetails)).EndInit();
            this.groupControlDetails.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTaxDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTaxDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlDetails;
        private DevExpress.XtraGrid.GridControl gridControlTaxDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTaxDetails;
        private DevExpress.XtraGrid.Columns.GridColumn Client_Order_Number;
        private DevExpress.XtraGrid.Columns.GridColumn Client_Name;
        private DevExpress.XtraGrid.Columns.GridColumn Client_Number;
        private DevExpress.XtraGrid.Columns.GridColumn Sub_ProcessName;
        private DevExpress.XtraGrid.Columns.GridColumn Subprocess_Number;
        private DevExpress.XtraGrid.Columns.GridColumn Order_Type;
        private DevExpress.XtraGrid.Columns.GridColumn Assigned_Date;
        private DevExpress.XtraGrid.Columns.GridColumn Borrower_Name;
        private DevExpress.XtraGrid.Columns.GridColumn State;
        private DevExpress.XtraGrid.Columns.GridColumn County;
        private DevExpress.XtraGrid.Columns.GridColumn Address;
        private DevExpress.XtraGrid.Columns.GridColumn Tax_Task;
        private DevExpress.XtraGrid.Columns.GridColumn Tax_Status;
        private DevExpress.XtraGrid.Columns.GridColumn grd_Si_No;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraGrid.Columns.GridColumn Order_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
    }
}