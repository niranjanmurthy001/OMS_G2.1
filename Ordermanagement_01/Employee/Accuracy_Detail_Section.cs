using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Ordermanagement_01.Reports
{
    public partial class Accuracy_Detail_Section : DevExpress.XtraEditors.XtraForm
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int Emp_User_Id, Branch_ID;
        string User_Role_Id, Production_Date;
        string Header, Efficiency;
        System.Data.DataTable dtexport = new System.Data.DataTable();
        System.Data.DataTable dttargetorder = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        TimeSpan prod_t, Ideal_t, Break_t, Total_t, Allocte_t;
        int Production_Time, Ideal_Time, Break_Time, Total_Time, Allocated_In_Sec;
        string From_date, To_date;
        public Accuracy_Detail_Section(int EMP_USER_ID, string USER_ROLE, string PRODUCTION_DATE, int branchid, string firstdate, string lastdate)
        {
            InitializeComponent();

            Emp_User_Id = EMP_USER_ID;
            User_Role_Id = USER_ROLE;
            Production_Date = PRODUCTION_DATE;
            Branch_ID = branchid;
            From_date = firstdate;
            To_date = lastdate;
        }

        private void Accuracy_Detail_Section_Load(object sender, EventArgs e)
        {
            
            Bind_Emp_Production_Dataview();
            Grid_Errors_Details();
            Bind_Employee_effeciency();
        }

        private void Bind_Employee_effeciency()
        {
            DataTable dtEfficiency = new DataTable();
            dtEfficiency.Columns.AddRange(new DataColumn[8]{
            new DataColumn("T.Orders",typeof(string)),
            new DataColumn("Total Hr",typeof(string)),
            new DataColumn("P.Hour",typeof(string)),
            new DataColumn("Allocated Hour",typeof(string)),
            new DataColumn("B.Hour",typeof(string)),
            new DataColumn("Efficiency",typeof(string)),
            new DataColumn("User_Id",typeof(string)),
            new DataColumn("I.Hour",typeof(string))
            });

            DataRow row = dtEfficiency.NewRow();


            try
            {
                Header = "Order Details";
                Hashtable htemp = new Hashtable();
                System.Data.DataTable dtemp = new System.Data.DataTable();


                //Grd_Dash_Emp_efficency.Rows.Clear();
                //Grd_Dash_Emp_efficency.Rows.Add();
                Hashtable htuser_Order_Details = new Hashtable();
                System.Data.DataTable dtOrder_Details = new System.Data.DataTable();

                Hashtable htget_Emp_Eff = new Hashtable();
                System.Data.DataTable dtget_Emp_Eff = new System.Data.DataTable();
                htget_Emp_Eff.Clear();
                dtget_Emp_Eff.Clear();


                htget_Emp_Eff.Add("@Trans", "DAILY_USER_NEW_UPDATED_EFF");
                htget_Emp_Eff.Add("@User_Id", Emp_User_Id);
                DateTime Prd_Date = Convert.ToDateTime(Production_Date.ToString());
                string Prd_Date1 = Prd_Date.ToString("MM/dd/yyyy");
                htget_Emp_Eff.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Eff = dataaccess.ExecuteSP("Sp_Score_Board_Updated", htget_Emp_Eff);

                Efficiency = dtget_Emp_Eff.Rows[0]["Effecinecy"].ToString();




                Hashtable htget_Emp_Prod_Idel_Time = new Hashtable();
                System.Data.DataTable dtget_Emp_Prod_Idel_Time = new System.Data.DataTable();

                htget_Emp_Prod_Idel_Time.Add("@Trans", "GET_BREAK_HOURS");
                htget_Emp_Prod_Idel_Time.Add("@User_Id", Emp_User_Id);
                htget_Emp_Prod_Idel_Time.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Prod_Idel_Time = dataaccess.ExecuteSP("Sp_Score_Board", htget_Emp_Prod_Idel_Time);

                if (dtget_Emp_Prod_Idel_Time.Rows.Count > 0)
                {
                    Break_Time = int.Parse(dtget_Emp_Prod_Idel_Time.Rows[0]["Total_Break_Time"].ToString());
                }
                else
                {
                    Break_Time = 0;
                }

                Hashtable htget_Emp_Prod_Idel_Time1 = new Hashtable();
                System.Data.DataTable dtget_Emp_Prod_Idel_Time1 = new System.Data.DataTable();

                htget_Emp_Prod_Idel_Time1.Add("@Trans", "GET_IDEAL_HOURS");
                htget_Emp_Prod_Idel_Time1.Add("@User_Id", Emp_User_Id);
                htget_Emp_Prod_Idel_Time1.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Prod_Idel_Time1 = dataaccess.ExecuteSP("Sp_Score_Board", htget_Emp_Prod_Idel_Time1);

                if (dtget_Emp_Prod_Idel_Time1.Rows.Count > 0)
                {
                    Ideal_Time = int.Parse(dtget_Emp_Prod_Idel_Time1.Rows[0]["Total_Ideal_Time"].ToString());
                }
                else
                {
                    Ideal_Time = 0;
                }

                Hashtable htget_Emp_Prod_Idel_Time2 = new Hashtable();
                System.Data.DataTable dtget_Emp_Prod_Idel_Time2 = new System.Data.DataTable();

                htget_Emp_Prod_Idel_Time2.Add("@Trans", "GET_PRODUCTION_HOURS");
                htget_Emp_Prod_Idel_Time2.Add("@User_Id", Emp_User_Id);
                htget_Emp_Prod_Idel_Time2.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Prod_Idel_Time2 = dataaccess.ExecuteSP("Sp_Score_Board", htget_Emp_Prod_Idel_Time2);

                if (dtget_Emp_Prod_Idel_Time2.Rows.Count > 0)
                {
                    Production_Time = int.Parse(dtget_Emp_Prod_Idel_Time2.Rows[0]["Total_Production_Time"].ToString());
                }
                else
                {
                    Production_Time = 0;
                }


                if (dtget_Emp_Eff.Rows.Count > 0)
                {
                    Allocated_In_Sec = int.Parse(dtget_Emp_Eff.Rows[0]["Allocated_In_Sec"].ToString());
                }
                else
                {
                    Allocated_In_Sec = 0;
                }



                Total_Time = Production_Time + Break_Time + Ideal_Time;

                Total_t = TimeSpan.FromSeconds(Total_Time);
                string Total_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                Total_t.Hours,
                Total_t.Minutes,
                Total_t.Seconds);
                
                prod_t = TimeSpan.FromSeconds(Production_Time);
                string Prd_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                prod_t.Hours,
                prod_t.Minutes,
                prod_t.Seconds);
               
                Allocte_t = TimeSpan.FromSeconds(Allocated_In_Sec);
                string Allocate_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                Allocte_t.Hours,
                Allocte_t.Minutes,
                Allocte_t.Seconds);
                    
                Ideal_t = TimeSpan.FromSeconds(Ideal_Time);
                string idl_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                   Ideal_t.Hours,
                   Ideal_t.Minutes,
                   Ideal_t.Seconds);

                Break_t = TimeSpan.FromSeconds(Break_Time);
                string brk_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                Break_t.Hours,
                Break_t.Minutes,
                Break_t.Seconds);

                row["Total Hr"] = Total_formatedTime;
                row["P.Hour"] = Prd_formatedTime;
                row["Allocated Hour"] = Allocate_formatedTime;      
                row["I.Hour"] = idl_formatedTime;
                row["B.Hour"] = brk_formatedTime;
                row["User_Id"] = Emp_User_Id.ToString();
                row["Efficiency"] = Efficiency;
                row["T.Orders"] = gridView2.RowCount.ToString();
                dtEfficiency.Rows.Add(row);
                if (dtEfficiency.Rows.Count > 0)
                {
                    Grd_Dash_Emp_efficency.DataSource = dtEfficiency;
                }             
            }
            catch (Exception ex)
            {


            }




        }

        private void Bind_Emp_Production_Dataview()
        {
            Header = "Order Details";
            Hashtable ht_Comp_Orders = new Hashtable();
            System.Data.DataTable dt_Comp_Orders = new System.Data.DataTable();
            ht_Comp_Orders.Clear();
            dt_Comp_Orders.Clear();

            //1 user wise
            if (Emp_User_Id != 0 && Branch_ID != 0 && Production_Date != "" && From_date == "" && To_date == "")
            {
                ht_Comp_Orders.Add("@Trans", "GET_COMPLETED_ORDERS_NEW");
                // ht_Comp_Orders.Add("@filter", "USER_AND_PRODUCTION_DATE_WISE");
            }
            //2 branch and date wise  -- row value type is 'TOTAl' and column value type is 'VALUE'
            else if (Branch_ID != 0 && Emp_User_Id == 0 && Production_Date != "" && From_date == "" && To_date == "")
            {

                ht_Comp_Orders.Add("@Trans", "GET_COMPLETED_BRANCH_DATE_WISE");
                // ht_Comp_Orders.Add("@filter", "BRANCH_AND_PRODUCTION_DATE_WISE");
            }
                //3 production date wise   (rowvaluetype is Grandtotal and columnvalutype is value)
            else if (Production_Date != "" && Branch_ID == 0 && Emp_User_Id == 0 && From_date == "" && To_date == "")
            {
                ht_Comp_Orders.Add("@Trans", "GET_COMPLETED_PRODUCTION_DATE_WISE");
                //ht_Comp_Orders.Add("@filter", "PRODUCTION_DATE_WISE");
            }
            //4 from date and to date (rowvaluetype is Grandtotal and columnvalutype is Grandtotal)
            else if (From_date != "" && To_date != "" && Branch_ID == 0 && Emp_User_Id == 0 && Production_Date == "")
            {
                ht_Comp_Orders.Add("@Trans", "GET_COMPLETED_FROMDATE_AND_TODATE_WISE");
                //ht_Comp_Orders.Add("@filter", "FROM_DATE_AND_TO_DATE_WISE");
            }

            // 5 (rowvaluetype is TOTAl and columnvalutype is Grandtotal)
            else if (Emp_User_Id == 0 && From_date != "" && To_date != "" && Branch_ID != 0 && Production_Date == "")
            {
                ht_Comp_Orders.Add("@Trans", "GET_COMPLETED_BRANCH_AND_FROMDATE_AND_TODATE_WISE");
                // ht_Comp_Orders.Add("@filter", "BRANCH_AND_FROM_DATE_AND_TO_DATE_WISE");
            }
            // 6 (rowvaluetype is value and columnvalutype is Grandtotal)
            else if (Emp_User_Id != 0 && From_date != "" && To_date != "" && Branch_ID != 0 && Production_Date == "")
            {
                ht_Comp_Orders.Add("@Trans", "GET_COMPLETED_USER_BRANCH_FROMDATE_TODATE_WISE");
                // ht_Comp_Orders.Add("@filter", "USER_AND_FROM_DATE_AND_TO_DATE_WISE");
            }


            //ht_Orders.Add("@Trans", "GET_COMPLETED_ORDERS_NEW");
            ht_Comp_Orders.Add("@Production_Date", Production_Date);
            ht_Comp_Orders.Add("@User_Id", Emp_User_Id);
            ht_Comp_Orders.Add("@Branch_ID", Branch_ID);
            ht_Comp_Orders.Add("@From_Date", From_date);
            ht_Comp_Orders.Add("@To_Date", To_date);
            dt_Comp_Orders = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", ht_Comp_Orders);

            if (dt_Comp_Orders.Rows.Count > 0)
            {
                //lbl_total.Text = dttargetorder.Rows.Count.ToString();
                //if (dttargetorder.Rows.Count > 0)
                //{
                //    lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
                //}

                //grd_Targetorder.DataSource = dt;

                if (User_Role_Id == "1")
                {
                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;
                }
                else
                {
                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;

                }
                grd_Targetorder.DataSource = dt_Comp_Orders;
            }
            else
            {
                grd_Targetorder.Visible = true;
                grd_Targetorder.DataSource = null;
            }

        }

        private void Grid_Errors_Details()
        {
            Header = "Order Details";
            try
            {
                Hashtable htget_New_Erros = new Hashtable();
                DataTable dtget_New_Errors = new DataTable();

                if (Emp_User_Id != 0 && Branch_ID != 0 && Production_Date != "" && From_date == "" && To_date == "")
                {
                    htget_New_Erros.Add("@Trans", "GET_USER_ERROR_DETAILS");
                }
                else if (Branch_ID != 0 && Emp_User_Id == 0 && Production_Date != "" && From_date == "" && To_date == "")
                {

                    htget_New_Erros.Add("@Trans", "GET_USER_ERROR_DETAILS_BY_BRANCH_AND_SINGLE_DATE_WISE");

                }
                else if (Production_Date != "" && Emp_User_Id == 0 && Branch_ID == 0 && From_date == "" & To_date == "")
                {
                    htget_New_Erros.Add("@Trans", "GET_USER_ERROR_DETAILS_BY_SINGLE_DATE_WISE");
                }
                else if (From_date != "" && To_date != "" && Emp_User_Id == 0 && Branch_ID == 0 && Production_Date == "")
                {
                    htget_New_Erros.Add("@Trans", "GET_USER_ERROR_DETAILS_BY_FROMDATE_TODATE_WISE");
                }

                else if (Emp_User_Id != 0 && From_date != "" && To_date != "" && Branch_ID != 0 && Production_Date == "")
                {
                    htget_New_Erros.Add("@Trans", "GET_USER_ERROR_DETAILS_BY_COLUMN_GRANDTOTAL_WISE");
                }

                else if (Emp_User_Id == 0 && From_date != "" && To_date != "" && Branch_ID != 0 && Production_Date == "")
                {
                    htget_New_Erros.Add("@Trans", "GET_USER_ERROR_DETAILS_BY_BRANCH_TOTAL_WISE");
                }


                //htget_New_Erros.Add("@Trans", "GET_USER_ERROR_DETAILS");


                htget_New_Erros.Add("@Error_On_User_Id", Emp_User_Id);
                htget_New_Erros.Add("@Production_Date", Production_Date);
                htget_New_Erros.Add("@Branch_Id", Branch_ID);
                htget_New_Erros.Add("@From_date", From_date);
                htget_New_Erros.Add("@To_date", To_date);
                dtget_New_Errors = dataaccess.ExecuteSP("Sp_Accuracy_Details", htget_New_Erros);

                if (dtget_New_Errors.Rows.Count > 0)
                {
                    Grid_Error_Details.DataSource = dtget_New_Errors;

                    if (User_Role_Id == "2")
                    {
                        gridView6.Columns["Error_Entered_From"].Visible = false;
                    }
                }
                else
                {

                    Grid_Error_Details.DataSource = null;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in With Binding Data;Please Contact Administrator");
            }

        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                Export_All_PivotGrid();
            }
            finally
            {

                SplashScreenManager.CloseForm(false);
            }
        }

        private void Export_All_PivotGrid()
        {
            GridControl[] grids = new GridControl[] { Grd_Dash_Emp_efficency, grd_Targetorder, Grid_Error_Details };
            DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink(ps);

            // Show the Document Map toolbar button and menu item.
            ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

            // Make the "Export to Csv" and "Export to Txt" commands visible.
            ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
            compositeLink.PrintingSystem = ps;
            foreach (GridControl grid in grids)
            {
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = grid;
                //compositeLink.Links.Add(link);
                compositeLink.Links.AddRange(new object[] { link });
            }
           

            string ReportName = "Accuracy_Deatils";
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";

            //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //string myPath = @"15-02-2019-Accuracy_Deatils.xlsx";
            //excelApp.Workbooks.Open(myPath);

            //// Get Worksheet
            //Microsoft.Office.Interop.Excel.Worksheet worksheet = excelApp.Worksheets[1];
            //int rowIndex = 2; int colIndex = 2;
            //for (int i = 0; i < 10; i++)
            //{
            //    excelApp.Cells[rowIndex, colIndex] = "\r123";
            //}

            //excelApp.Visible = false;


            compositeLink.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage });
            compositeLink.ShowPreview();
            compositeLink.CreatePageForEachLink();





        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption =="SI.NO")
            {
                string value = e.RowHandle.ToString();

                if (value != "")
                {
                    int value1 = int.Parse(value.ToString()) + 1;

                    e.DisplayText = value1.ToString();

                }
            }
        }

        private void gridView6_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (e.Column.Caption == "SI.NO")
            {
                string value_3 = e.RowHandle.ToString();

                if (value_3 != "")
                {
                    int value_4 = int.Parse(value_3.ToString()) + 1;

                    e.DisplayText = value_4.ToString();

                }
            }
        }

        private void gridView6_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var columnIndex = gridView6.FocusedColumn.VisibleIndex;

            if (columnIndex == 1)
            {
                System.Data.DataRow row = gridView6.GetDataRow(gridView6.FocusedRowHandle);
                int Order_ID = int.Parse(row["Order_Id"].ToString());

                Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID, Emp_User_Id, User_Role_Id, Production_Date);
                orderentry.Show();
            }
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var columnIndex = gridView2.FocusedColumn.VisibleIndex;

            if (columnIndex == 1)
            {
                System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                int Order_ID = int.Parse(row["Order_Id"].ToString());

                Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID, Emp_User_Id, User_Role_Id, Production_Date);
                orderentry.Show();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            var columnIndex = gridView1.FocusedColumn.VisibleIndex;
           
            if (columnIndex == 2)
            {
                Ordermanagement_01.Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(Production_Date, Emp_User_Id, "Production");
                emb.Show();

            }
            else if (columnIndex == 3)
            {
                Ordermanagement_01.Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(Production_Date, Emp_User_Id, "Ideal");
                emb.Show();
            }
            else if (columnIndex == 4)
            {
                 Ordermanagement_01.Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(Production_Date, Emp_User_Id, "Break");
                 emb.Show();
            }
           
        }

        

       



    }
}