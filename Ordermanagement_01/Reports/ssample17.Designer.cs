namespace Ordermanagement_01.Reports
{
    partial class ssample17
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
            this.panel12 = new System.Windows.Forms.Panel();
            this.pivotGridControl7_Shift_Datewise = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pivotGridField38 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField39 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField40 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField41 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField43 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl7_Shift_Datewise)).BeginInit();
            this.SuspendLayout();
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.pivotGridControl7_Shift_Datewise);
            this.panel12.Location = new System.Drawing.Point(31, 23);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(553, 252);
            this.panel12.TabIndex = 12;
            // 
            // pivotGridControl7_Shift_Datewise
            // 
            this.pivotGridControl7_Shift_Datewise.ActiveFilterString = "";
            this.pivotGridControl7_Shift_Datewise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.pivotGridControl7_Shift_Datewise.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField38,
            this.pivotGridField39,
            this.pivotGridField40,
            this.pivotGridField41,
            this.pivotGridField43});
            this.pivotGridControl7_Shift_Datewise.Location = new System.Drawing.Point(3, 3);
            this.pivotGridControl7_Shift_Datewise.LookAndFeel.SkinName = "Office 2010 Blue";
            this.pivotGridControl7_Shift_Datewise.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pivotGridControl7_Shift_Datewise.Name = "pivotGridControl7_Shift_Datewise";
            this.pivotGridControl7_Shift_Datewise.OptionsView.RowTreeWidth = 240;
            this.pivotGridControl7_Shift_Datewise.OptionsView.ShowColumnTotals = false;
            this.pivotGridControl7_Shift_Datewise.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.pivotGridControl7_Shift_Datewise.Size = new System.Drawing.Size(492, 221);
            this.pivotGridControl7_Shift_Datewise.TabIndex = 10;
            // 
            // pivotGridField38
            // 
            this.pivotGridField38.AreaIndex = 0;
            this.pivotGridField38.Caption = "Shift Type Name";
            this.pivotGridField38.FieldName = "Shift_Type_Name";
            this.pivotGridField38.Name = "pivotGridField38";
            // 
            // pivotGridField39
            // 
            this.pivotGridField39.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField39.AreaIndex = 0;
            this.pivotGridField39.Caption = "Shift Type Name";
            this.pivotGridField39.FieldName = "Shift_Type_Name";
            this.pivotGridField39.Name = "pivotGridField39";
            this.pivotGridField39.Options.ShowSummaryTypeName = true;
            this.pivotGridField39.TopValueMode = DevExpress.XtraPivotGrid.TopValueMode.AllValues;
            this.pivotGridField39.UnboundFieldName = "pivotGridField7";
            // 
            // pivotGridField40
            // 
            this.pivotGridField40.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField40.AreaIndex = 0;
            this.pivotGridField40.Caption = "Order Production Date";
            this.pivotGridField40.FieldName = "Order_Production_Date";
            this.pivotGridField40.Name = "pivotGridField40";
            // 
            // pivotGridField41
            // 
            this.pivotGridField41.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField41.AreaIndex = 0;
            this.pivotGridField41.Caption = "No Of Orders";
            this.pivotGridField41.FieldName = "No_Of_Orders";
            this.pivotGridField41.Name = "pivotGridField41";
            // 
            // pivotGridField43
            // 
            this.pivotGridField43.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField43.AreaIndex = 1;
            this.pivotGridField43.Caption = "Order Status";
            this.pivotGridField43.FieldName = "Order_Status";
            this.pivotGridField43.Name = "pivotGridField43";
            // 
            // ssample17
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 408);
            this.Controls.Add(this.panel12);
            this.Name = "ssample17";
            this.Text = "ssample17";
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl7_Shift_Datewise)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel12;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl7_Shift_Datewise;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField38;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField39;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField40;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField41;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField43;
    }
}