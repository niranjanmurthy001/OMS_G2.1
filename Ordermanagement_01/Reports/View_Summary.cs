using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.Collections;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPivotGrid;

using DevExpress.XtraReports.UI;

using System.Diagnostics;
using DevExpress.Xpf.Printing;
using DevExpress.XtraGrid.Columns;
using System.Threading;
using ProgressBarExample;
using DevExpress.LookAndFeel;
using DevExpress.XtraCharts;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Office.OpenXml;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using ClosedXML.Excel;

namespace Ordermanagement_01.Reports
{
    public partial class View_Summary : DevExpress.XtraEditors.XtraForm
    {
        System.Data.DataTable dt_get_Shift_wise = new System.Data.DataTable();
        System.Data.DataTable dt_get_singale_date_wise = new System.Data.DataTable();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        string Production_Date;
        int branch_Id,shift_Type_Id;
        public View_Summary(DataTable dt_get_Shift, DataTable dt_singale_date_wise,int branch,int Shift)
        {
            InitializeComponent();
            //Production_Date = PRODUCTION_DATE;
            dt_get_Shift_wise = dt_get_Shift;
            pivotGridControl2.Visible = true;
            branch_Id = branch;
            shift_Type_Id = Shift;
            dt_get_singale_date_wise = dt_singale_date_wise;
        }

        private void View_Summary_Load(object sender, EventArgs e)
        {
            User_Summary();
            Order_Summary();
        }

        // Shift summary
        private void Order_Summary()
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                pivotGridControl2.DataSource = dt_get_Shift_wise;

            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }
        // User  summary
        private void User_Summary()
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                Grid_User_Summary.DataSource = dt_get_singale_date_wise;

            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }



        //private void pivotGridControl2_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        //{
        //    if (e.Field.FieldName == "Order_Status")
        //    {
        //        object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_ID"),
        //             orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_ID");
        //        e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
        //        e.Handled = true;

        //    }
        //}

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption == "SI.NO")
            {
                string value = e.RowHandle.ToString();

                if (value != "")
                {
                    int value1 = int.Parse(value.ToString()) + 1;

                    e.DisplayText = value1.ToString();

                }
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                Export_Grid();
            }
            finally
            {

                SplashScreenManager.CloseForm(false);
            }
        }


        private void Export_Grid()
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_1 = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_1 = new DevExpress.XtraPrintingLinks.CompositeLink(ps_1);

                DevExpress.XtraPrinting.PrintableComponentLink link_1 = new DevExpress.XtraPrinting.PrintableComponentLink();
                DevExpress.XtraPrinting.PrintableComponentLink link_2 = new DevExpress.XtraPrinting.PrintableComponentLink();


                // Show the Document Map toolbar button and menu item.
                ps_1.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_1.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_1.PrintingSystem = ps_1;


                link_1.Component = Grid_User_Summary;
                link_1.PaperName = "Niranjan";
                link_2.Component = pivotGridControl2;
                compositeLink_1.Links.AddRange(new object[] { link_1, link_2});


                string ReportName = "Summary-Report";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_1.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_1.XlSheetCreated += PrintingSystem_XlSheetCreated_1;

                compositeLink_1.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Text });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }



        }


        void PrintingSystem_XlSheetCreated_1(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "User Summary ";

            }
            if (e.Index == 1)
            {
                e.SheetName = "Order Summary";

            }
        }

        private void pivotGridControl2_CustomFieldSort(object sender, DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "Order_Status")
            {

                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_ID"),
                     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_ID");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;
            }
        }

        private void Grid_User_Summary_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                 var columnIndex = gridView2.FocusedColumn.VisibleIndex;

                 if (columnIndex == 2)
                 {
                     //System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                     //int Order_ID = int.Parse(row["Order_Id"].ToString());



                     Hashtable ht_get = new Hashtable();
                     System.Data.DataTable dt_get = new System.Data.DataTable();
                     ht_get.Clear();
                     dt_get.Clear();

                     string date = dt_get_singale_date_wise.Rows[0]["Date"].ToString();
                     if (branch_Id == 0 && shift_Type_Id==0)
                     {
                         ht_get.Add("@Trans", "LOGGED_IN_USER");
                     }
                     else if(branch_Id!=0 && shift_Type_Id==0)
                     {
                         ht_get.Add("@Trans", "LOGGED_IN_USER_BRANCH_WISE");
                     }
                     else if (branch_Id != 0 && shift_Type_Id != 0)
                     {

                         ht_get.Add("@Trans", "LOGGED_IN_USER_BRANCH_SHIFT_WISE");
                     }
                     ht_get.Add("@date", date);
                     ht_get.Add("@Branch", branch_Id);
                     ht_get.Add("@Shift_Type_Id", shift_Type_Id);
                     dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get);

                     User_Details_View view = new User_Details_View(dt_get);
                     view.Show();
                 }
                 else if (columnIndex == 4)
                 {
                     //System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                     //int Order_ID = int.Parse(row["Order_Id"].ToString());



                     Hashtable ht_get_1 = new Hashtable();
                     System.Data.DataTable dt_get_1 = new System.Data.DataTable();
                     ht_get_1.Clear();
                     dt_get_1.Clear();

                     string date = dt_get_singale_date_wise.Rows[0]["Date"].ToString();
                     if (branch_Id == 0 && shift_Type_Id == 0)
                     {
                         ht_get_1.Add("@Trans", "PRODUCTION_USER");
                     }
                     else if (branch_Id != 0 && shift_Type_Id == 0)
                     {

                         ht_get_1.Add("@Trans", "PRODUCTION_USER_BRANCH_WISE");
                     }
                     else if (branch_Id != 0 && shift_Type_Id != 0)
                     {

                         ht_get_1.Add("@Trans", "PRODUCTION_USER_BRANCH_SHIFT_WISE");
                     }
                     ht_get_1.Add("@Branch", branch_Id);
                     ht_get_1.Add("@Shift_Type_Id", shift_Type_Id);
                    // ht_get_1.Add("@date", date);
                     dt_get_1 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_1);

                     User_Details_View view = new User_Details_View(dt_get_1);
                     view.Show();
                 }
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }






    }
}