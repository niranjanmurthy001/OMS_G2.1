namespace Ordermanagement_01.New_Dashboard.Employee
{
    partial class General_Notification
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grid_notification = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.layoutViewColumn1 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewColumn2 = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.Message = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.order_by_date = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.layoutViewField_Message = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField_order_by_date = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_notification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Message)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_order_by_date)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.grid_notification);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(699, 511);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "General Notification";
            // 
            // grid_notification
            // 
            this.grid_notification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_notification.Location = new System.Drawing.Point(2, 21);
            this.grid_notification.MainView = this.layoutView1;
            this.grid_notification.Name = "grid_notification";
            this.grid_notification.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2});
            this.grid_notification.Size = new System.Drawing.Size(695, 488);
            this.grid_notification.TabIndex = 0;
            this.grid_notification.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1,
            this.advBandedGridView1});
            // 
            // layoutView1
            // 
            this.layoutView1.CardMinSize = new System.Drawing.Size(689, 119);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.layoutViewColumn1,
            this.layoutViewColumn2});
            this.layoutView1.GridControl = this.grid_notification;
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsItemText.AlignMode = DevExpress.XtraGrid.Views.Layout.FieldTextAlignMode.AlignGlobal;
            this.layoutView1.OptionsItemText.TextToControlDistance = 8;
            this.layoutView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.layoutViewColumn2, DevExpress.Data.ColumnSortOrder.Descending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.layoutViewColumn1, DevExpress.Data.ColumnSortOrder.Descending)});
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            // 
            // layoutViewColumn1
            // 
            this.layoutViewColumn1.AppearanceCell.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutViewColumn1.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.layoutViewColumn1.AppearanceCell.Options.UseFont = true;
            this.layoutViewColumn1.AppearanceCell.Options.UseForeColor = true;
            this.layoutViewColumn1.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.layoutViewColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.layoutViewColumn1.AppearanceHeader.Options.UseFont = true;
            this.layoutViewColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.layoutViewColumn1.Caption = "Message";
            this.layoutViewColumn1.FieldName = "Message";
            this.layoutViewColumn1.LayoutViewField = this.layoutViewField_Message;
            this.layoutViewColumn1.Name = "layoutViewColumn1";
            this.layoutViewColumn1.OptionsColumn.AllowEdit = false;
            this.layoutViewColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.layoutViewColumn1.Width = 542;
            // 
            // layoutViewColumn2
            // 
            this.layoutViewColumn2.AppearanceCell.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutViewColumn2.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.layoutViewColumn2.AppearanceCell.Options.UseFont = true;
            this.layoutViewColumn2.AppearanceCell.Options.UseForeColor = true;
            this.layoutViewColumn2.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.layoutViewColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.layoutViewColumn2.AppearanceHeader.Options.UseFont = true;
            this.layoutViewColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.layoutViewColumn2.Caption = "Order By Date";
            this.layoutViewColumn2.FieldName = "Modified_Date";
            this.layoutViewColumn2.LayoutViewField = this.layoutViewField_order_by_date;
            this.layoutViewColumn2.Name = "layoutViewColumn2";
            this.layoutViewColumn2.OptionsColumn.AllowEdit = false;
            this.layoutViewColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.layoutViewColumn2.Width = 589;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.Message,
            this.order_by_date});
            this.advBandedGridView1.GridControl = this.grid_notification;
            this.advBandedGridView1.Name = "advBandedGridView1";
            this.advBandedGridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.order_by_date, DevExpress.Data.ColumnSortOrder.Descending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.Message, DevExpress.Data.ColumnSortOrder.Descending)});
            this.advBandedGridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.advBandedGridView1_RowCellClick);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Columns.Add(this.Message);
            this.gridBand1.Columns.Add(this.order_by_date);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.OptionsBand.ShowCaption = false;
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 1131;
            // 
            // Message
            // 
            this.Message.AppearanceCell.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.Message.AppearanceCell.Options.UseFont = true;
            this.Message.AppearanceCell.Options.UseForeColor = true;
            this.Message.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.Message.AppearanceHeader.Options.UseFont = true;
            this.Message.AppearanceHeader.Options.UseForeColor = true;
            this.Message.Caption = "Message";
            this.Message.FieldName = "Message";
            this.Message.Name = "Message";
            this.Message.OptionsColumn.AllowEdit = false;
            this.Message.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.Message.Visible = true;
            this.Message.Width = 542;
            // 
            // order_by_date
            // 
            this.order_by_date.AppearanceCell.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.order_by_date.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.order_by_date.AppearanceCell.Options.UseFont = true;
            this.order_by_date.AppearanceCell.Options.UseForeColor = true;
            this.order_by_date.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.order_by_date.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.order_by_date.AppearanceHeader.Options.UseFont = true;
            this.order_by_date.AppearanceHeader.Options.UseForeColor = true;
            this.order_by_date.Caption = "Order By Date";
            this.order_by_date.FieldName = "Modified_Date";
            this.order_by_date.Name = "order_by_date";
            this.order_by_date.OptionsColumn.AllowEdit = false;
            this.order_by_date.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.order_by_date.Visible = true;
            this.order_by_date.Width = 589;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2013";
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_Message,
            this.layoutViewField_order_by_date});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 8;
            this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // layoutViewField_Message
            // 
            this.layoutViewField_Message.EditorPreferredWidth = 677;
            this.layoutViewField_Message.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_Message.Name = "layoutViewField_Message";
            this.layoutViewField_Message.Size = new System.Drawing.Size(683, 47);
            this.layoutViewField_Message.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField_Message.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.layoutViewField_Message.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutViewField_order_by_date
            // 
            this.layoutViewField_order_by_date.EditorPreferredWidth = 677;
            this.layoutViewField_order_by_date.Location = new System.Drawing.Point(0, 47);
            this.layoutViewField_order_by_date.Name = "layoutViewField_order_by_date";
            this.layoutViewField_order_by_date.Size = new System.Drawing.Size(683, 47);
            this.layoutViewField_order_by_date.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField_order_by_date.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.layoutViewField_order_by_date.TextSize = new System.Drawing.Size(73, 13);
            // 
            // General_Notification
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 511);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(715, 1080);
            this.MinimumSize = new System.Drawing.Size(715, 549);
            this.Name = "General_Notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Notification";
            this.Load += new System.EventHandler(this.General_Notification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_notification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Message)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_order_by_date)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grid_notification;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn Message;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn order_by_date;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn layoutViewColumn2;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_Message;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_order_by_date;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
    }
}