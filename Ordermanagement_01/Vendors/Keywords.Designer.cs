namespace Ordermanagement_01.Vendors
{
    partial class Keywords
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
            this.textEditKeyword = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddKeywords = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlKeywords = new DevExpress.XtraGrid.GridControl();
            this.gridViewKeywords = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KeywordId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.textEditKeyword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlKeywords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKeywords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textEditKeyword
            // 
            this.textEditKeyword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textEditKeyword.Location = new System.Drawing.Point(83, 9);
            this.textEditKeyword.Name = "textEditKeyword";
            this.textEditKeyword.Size = new System.Drawing.Size(230, 44);
            this.textEditKeyword.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(14, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Keyword";
            // 
            // btnAddKeywords
            // 
            this.btnAddKeywords.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddKeywords.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddKeywords.Appearance.Options.UseFont = true;
            this.btnAddKeywords.Location = new System.Drawing.Point(157, 59);
            this.btnAddKeywords.Name = "btnAddKeywords";
            this.btnAddKeywords.Size = new System.Drawing.Size(75, 23);
            this.btnAddKeywords.TabIndex = 2;
            this.btnAddKeywords.Text = "Add";
            this.btnAddKeywords.Click += new System.EventHandler(this.btnAddKeywords_Click);
            // 
            // gridControlKeywords
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gridControlKeywords, 3);
            this.gridControlKeywords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlKeywords.Location = new System.Drawing.Point(3, 99);
            this.gridControlKeywords.MainView = this.gridViewKeywords;
            this.gridControlKeywords.Name = "gridControlKeywords";
            this.gridControlKeywords.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemHyperLinkEdit2});
            this.gridControlKeywords.Size = new System.Drawing.Size(478, 220);
            this.gridControlKeywords.TabIndex = 3;
            this.gridControlKeywords.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewKeywords});
            // 
            // gridViewKeywords
            // 
            this.gridViewKeywords.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewKeywords.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewKeywords.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewKeywords.Appearance.Row.Options.UseFont = true;
            this.gridViewKeywords.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.KeywordId,
            this.gridColumn3,
            this.gridColumn2});
            this.gridViewKeywords.GridControl = this.gridControlKeywords;
            this.gridViewKeywords.IndicatorWidth = 30;
            this.gridViewKeywords.Name = "gridViewKeywords";
            this.gridViewKeywords.OptionsBehavior.Editable = false;
            this.gridViewKeywords.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewKeywords_RowCellClick);
            this.gridViewKeywords.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewKeywords_CustomDrawRowIndicator);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Keyword";
            this.gridColumn1.FieldName = "Keyword";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 242;
            // 
            // KeywordId
            // 
            this.KeywordId.Caption = "KeywordId";
            this.KeywordId.FieldName = "KeywordId";
            this.KeywordId.Name = "KeywordId";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Edit";
            this.gridColumn3.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.gridColumn3.FieldName = "Edit";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 80;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.repositoryItemHyperLinkEdit1.Appearance.Options.UseForeColor = true;
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.NullText = "Edit";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Delete";
            this.gridColumn2.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.gridColumn2.FieldName = "Delete";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 80;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.repositoryItemHyperLinkEdit2.Appearance.Options.UseForeColor = true;
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            this.repositoryItemHyperLinkEdit2.NullText = "Delete";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.textEditKeyword);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnAddKeywords);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(75, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(332, 90);
            this.panelControl1.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(238, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.02537F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.94926F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.02538F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridControlKeywords, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 322);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // Keywords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 322);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.MaximumSize = new System.Drawing.Size(700, 360);
            this.MinimumSize = new System.Drawing.Size(500, 360);
            this.Name = "Keywords";
            this.Text = "Keywords";
            this.Load += new System.EventHandler(this.Keywords_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEditKeyword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlKeywords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKeywords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit textEditKeyword;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAddKeywords;
        private DevExpress.XtraGrid.GridControl gridControlKeywords;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKeywords;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn KeywordId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}