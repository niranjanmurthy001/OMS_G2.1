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
            this.btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.grid_notification = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.Message = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_Message = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.updatedon = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_order_by_date = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.Readstatus = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.Message_id = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_layoutViewColumn1_1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_notification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Message)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_order_by_date)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.btn_Export);
            this.groupControl1.Controls.Add(this.grid_notification);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(737, 511);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "General Notification";
            // 
            // btn_Export
            // 
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Appearance.Options.UseFont = true;
            this.btn_Export.Location = new System.Drawing.Point(643, 24);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(75, 26);
            this.btn_Export.TabIndex = 1;
            this.btn_Export.Text = "Export";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
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
            this.grid_notification.Size = new System.Drawing.Size(733, 488);
            this.grid_notification.TabIndex = 0;
            this.grid_notification.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.FieldCaption.Options.UseTextOptions = true;
            this.layoutView1.Appearance.FieldCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutView1.CardHorzInterval = 0;
            this.layoutView1.CardMinSize = new System.Drawing.Size(710, 48);
            this.layoutView1.CardVertInterval = 4;
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.Message,
            this.updatedon,
            this.Readstatus,
            this.Message_id});
            this.layoutView1.GridControl = this.grid_notification;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_layoutViewColumn1,
            this.layoutViewField_layoutViewColumn1_1});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsItemText.AlignMode = DevExpress.XtraGrid.Views.Layout.FieldTextAlignMode.AlignGlobal;
            this.layoutView1.OptionsItemText.TextToControlDistance = 0;
            this.layoutView1.OptionsView.CardArrangeRule = DevExpress.XtraGrid.Views.Layout.LayoutCardArrangeRule.AllowPartialCards;
            this.layoutView1.OptionsView.ShowCardCaption = false;
            this.layoutView1.OptionsView.ShowFieldHints = false;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Column;
            this.layoutView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.updatedon, DevExpress.Data.ColumnSortOrder.Descending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.Message, DevExpress.Data.ColumnSortOrder.Descending)});
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            this.layoutView1.CustomDrawCardFieldValue += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.layoutView1_CustomDrawCardFieldValue);
            this.layoutView1.FieldValueClick += new DevExpress.XtraGrid.Views.Layout.Events.FieldValueClickEventHandler(this.layoutView1_FieldValueClick);
            // 
            // Message
            // 
            this.Message.AppearanceCell.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.Message.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.Message.AppearanceCell.Options.UseFont = true;
            this.Message.AppearanceCell.Options.UseForeColor = true;
            this.Message.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Message.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.Message.AppearanceHeader.Options.UseFont = true;
            this.Message.AppearanceHeader.Options.UseForeColor = true;
            this.Message.Caption = "Message";
            this.Message.FieldName = "Message";
            this.Message.LayoutViewField = this.layoutViewField_Message;
            this.Message.Name = "Message";
            this.Message.OptionsColumn.AllowEdit = false;
            this.Message.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.Message.Width = 542;
            // 
            // layoutViewField_Message
            // 
            this.layoutViewField_Message.EditorPreferredWidth = 706;
            this.layoutViewField_Message.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_Message.Name = "layoutViewField_Message";
            this.layoutViewField_Message.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewField_Message.Size = new System.Drawing.Size(710, 24);
            this.layoutViewField_Message.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutViewField_Message.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_Message.TextVisible = false;
            // 
            // updatedon
            // 
            this.updatedon.AppearanceCell.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            this.updatedon.AppearanceCell.ForeColor = System.Drawing.Color.Black;
            this.updatedon.AppearanceCell.Options.UseFont = true;
            this.updatedon.AppearanceCell.Options.UseForeColor = true;
            this.updatedon.AppearanceCell.Options.UseTextOptions = true;
            this.updatedon.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.updatedon.AppearanceHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.updatedon.AppearanceHeader.ForeColor = System.Drawing.Color.Black;
            this.updatedon.AppearanceHeader.Options.UseFont = true;
            this.updatedon.AppearanceHeader.Options.UseForeColor = true;
            this.updatedon.AppearanceHeader.Options.UseTextOptions = true;
            this.updatedon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.updatedon.Caption = "                                                                                 " +
    "                                                                          Update" +
    "d On";
            this.updatedon.CustomizationCaption = "Updated On";
            this.updatedon.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.updatedon.FieldName = "Modified_Date";
            this.updatedon.LayoutViewField = this.layoutViewField_order_by_date;
            this.updatedon.Name = "updatedon";
            this.updatedon.OptionsColumn.AllowEdit = false;
            this.updatedon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.updatedon.Width = 589;
            // 
            // layoutViewField_order_by_date
            // 
            this.layoutViewField_order_by_date.EditorPreferredWidth = 179;
            this.layoutViewField_order_by_date.Location = new System.Drawing.Point(0, 24);
            this.layoutViewField_order_by_date.Name = "layoutViewField_order_by_date";
            this.layoutViewField_order_by_date.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewField_order_by_date.Size = new System.Drawing.Size(710, 24);
            this.layoutViewField_order_by_date.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutViewField_order_by_date.TextSize = new System.Drawing.Size(527, 13);
            // 
            // Readstatus
            // 
            this.Readstatus.Caption = "Read Status";
            this.Readstatus.FieldName = "Read_Staus";
            this.Readstatus.LayoutViewField = this.layoutViewField_layoutViewColumn1;
            this.Readstatus.Name = "Readstatus";
            // 
            // layoutViewField_layoutViewColumn1
            // 
            this.layoutViewField_layoutViewColumn1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1.Name = "layoutViewField_layoutViewColumn1";
            this.layoutViewField_layoutViewColumn1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewField_layoutViewColumn1.Size = new System.Drawing.Size(710, 48);
            this.layoutViewField_layoutViewColumn1.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutViewField_layoutViewColumn1.TextSize = new System.Drawing.Size(461, 20);
            // 
            // Message_id
            // 
            this.Message_id.Caption = "Message ID";
            this.Message_id.FieldName = "Gen_Update_ID";
            this.Message_id.LayoutViewField = this.layoutViewField_layoutViewColumn1_1;
            this.Message_id.Name = "Message_id";
            // 
            // layoutViewField_layoutViewColumn1_1
            // 
            this.layoutViewField_layoutViewColumn1_1.EditorPreferredWidth = 10;
            this.layoutViewField_layoutViewColumn1_1.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_layoutViewColumn1_1.Name = "layoutViewField_layoutViewColumn1_1";
            this.layoutViewField_layoutViewColumn1_1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewField_layoutViewColumn1_1.Size = new System.Drawing.Size(710, 48);
            this.layoutViewField_layoutViewColumn1_1.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutViewField_layoutViewColumn1_1.TextSize = new System.Drawing.Size(461, 20);
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.GroupBordersVisible = false;
            this.layoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_Message,
            this.layoutViewField_order_by_date});
            this.layoutViewCard1.Name = "layoutViewTemplateCard";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 0;
            this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2013";
            // 
            // General_Notification
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 511);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(715, 549);
            this.Name = "General_Notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Notification";
            this.Load += new System.EventHandler(this.General_Notification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_notification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_Message)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_order_by_date)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_layoutViewColumn1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grid_notification;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Message;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn updatedon;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Read_Status;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Readstatus;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn Message_id;
        private DevExpress.XtraEditors.SimpleButton btn_Export;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_Message;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_order_by_date;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_layoutViewColumn1_1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
    }
}