namespace Ordermanagement_01.Users
{
    partial class ActiveInactiveNew
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlActiveInactive = new DevExpress.XtraGrid.GridControl();
            this.gridViewActiveInactive = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.EmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Role = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Branch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmpCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.User_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Edit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlActiveInactive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActiveInactive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.gridControlActiveInactive);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1034, 490);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Users";
            // 
            // gridControlActiveInactive
            // 
            this.gridControlActiveInactive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlActiveInactive.Location = new System.Drawing.Point(2, 23);
            this.gridControlActiveInactive.MainView = this.gridViewActiveInactive;
            this.gridControlActiveInactive.Name = "gridControlActiveInactive";
            this.gridControlActiveInactive.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.gridControlActiveInactive.Size = new System.Drawing.Size(1030, 465);
            this.gridControlActiveInactive.TabIndex = 0;
            this.gridControlActiveInactive.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewActiveInactive});
            // 
            // gridViewActiveInactive
            // 
            this.gridViewActiveInactive.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.EmployeeName,
            this.Role,
            this.Branch,
            this.EmpCode,
            this.Status,
            this.User_id,
            this.Edit});
            this.gridViewActiveInactive.GridControl = this.gridControlActiveInactive;
            this.gridViewActiveInactive.Name = "gridViewActiveInactive";
            this.gridViewActiveInactive.OptionsBehavior.Editable = false;
            this.gridViewActiveInactive.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewActiveInactive_RowCellClick);
            this.gridViewActiveInactive.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewActiveInactive_CustomDrawRowIndicator);
            this.gridViewActiveInactive.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewActiveInactive_RowStyle);
            // 
            // EmployeeName
            // 
            this.EmployeeName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeName.AppearanceHeader.Options.UseFont = true;
            this.EmployeeName.Caption = "Employee Name";
            this.EmployeeName.FieldName = "Employee_Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.Visible = true;
            this.EmployeeName.VisibleIndex = 1;
            // 
            // Role
            // 
            this.Role.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Role.AppearanceHeader.Options.UseFont = true;
            this.Role.Caption = "Role";
            this.Role.FieldName = "Role_Name";
            this.Role.Name = "Role";
            this.Role.Visible = true;
            this.Role.VisibleIndex = 3;
            // 
            // Branch
            // 
            this.Branch.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Branch.AppearanceHeader.Options.UseFont = true;
            this.Branch.Caption = "Branch";
            this.Branch.FieldName = "Branch_Name";
            this.Branch.Name = "Branch";
            this.Branch.Visible = true;
            this.Branch.VisibleIndex = 0;
            // 
            // EmpCode
            // 
            this.EmpCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmpCode.AppearanceHeader.Options.UseFont = true;
            this.EmpCode.Caption = "Emp Code";
            this.EmpCode.FieldName = "DRN_Emp_Code";
            this.EmpCode.Name = "EmpCode";
            this.EmpCode.Visible = true;
            this.EmpCode.VisibleIndex = 2;
            // 
            // Status
            // 
            this.Status.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.AppearanceHeader.Options.UseFont = true;
            this.Status.Caption = "Status";
            this.Status.FieldName = "Status";
            this.Status.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.Status.Name = "Status";
            this.Status.Visible = true;
            this.Status.VisibleIndex = 4;
            // 
            // User_id
            // 
            this.User_id.Caption = "User_id";
            this.User_id.FieldName = "User_id";
            this.User_id.Name = "User_id";
            // 
            // Edit
            // 
            this.Edit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edit.AppearanceHeader.Options.UseFont = true;
            this.Edit.Caption = "Change Status";
            this.Edit.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.Edit.FieldName = "Edit";
            this.Edit.Name = "Edit";
            this.Edit.Visible = true;
            this.Edit.VisibleIndex = 5;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.repositoryItemHyperLinkEdit1.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
            this.repositoryItemHyperLinkEdit1.Appearance.Options.UseFont = true;
            this.repositoryItemHyperLinkEdit1.Appearance.Options.UseForeColor = true;
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.NullText = "Change";
            // 
            // ActiveInactiveNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 490);
            this.Controls.Add(this.groupControl1);
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "ActiveInactiveNew";
            this.Text = "Active Inactive Users";
            this.Load += new System.EventHandler(this.ActiveInactiveNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlActiveInactive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActiveInactive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControlActiveInactive;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewActiveInactive;
        private DevExpress.XtraGrid.Columns.GridColumn EmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn Role;
        private DevExpress.XtraGrid.Columns.GridColumn Branch;
        private DevExpress.XtraGrid.Columns.GridColumn EmpCode;
        private DevExpress.XtraGrid.Columns.GridColumn Status;
        private DevExpress.XtraGrid.Columns.GridColumn User_id;
        private DevExpress.XtraGrid.Columns.GridColumn Edit;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
    }
}