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
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using System.Globalization;

namespace Ordermanagement_01
{

    public partial class Accuracy : DevExpress.XtraEditors.XtraForm
    {
        DateTime first;
        DateTime last ;
        System.Data.DataTable dt_get = new System.Data.DataTable();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        int userId, userid; string userName;
        string User_Role_Id;
        string Employee_24_7_Production_user_id, Employee_24_7_Hour;

        string Production_Date;

        public Accuracy(int User_ID,string Role_ID,string PRODUCTION_DATE)
        {
            InitializeComponent();

            userid = User_ID;

            User_Role_Id = Role_ID;

            Production_Date = PRODUCTION_DATE;

            //userid = int.Parse(user_id.ToString());
        }

        private void Accuracy_Load(object sender, EventArgs e)
        {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                try
                {
                    BindLookUpEdit_Accuracy_Month();
                    BindLookUpEdit_Accuracy_Year();

                    Hashtable htget_Current_Month = new Hashtable();
                    System.Data.DataTable dtget_Current_Month = new System.Data.DataTable();

                    htget_Current_Month.Add("@Trans", "GET_CURRENT_MONTH");
                    dtget_Current_Month = dataaccess.ExecuteSP("Sp_Score_Board", htget_Current_Month);
                    if (dtget_Current_Month.Rows.Count > 0)
                    {
                        lookUpEdit_Accuracy_Month.EditValue = int.Parse(dtget_Current_Month.Rows[0]["C_Month"].ToString());
                    }

                    DateTime dt = DateTime.Now;

                    int year = dt.Year;

                    lookUpEdit_Accuracy_Year.EditValue = year;
                    this.WindowState = FormWindowState.Maximized;

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


        public void BindLookUpEdit_Accuracy_Month()
        {
            Hashtable ht_month = new Hashtable();
            System.Data.DataTable dt_month = new System.Data.DataTable();

            lookUpEdit_Accuracy_Month.Properties.DataSource = null;

            ht_month.Add("@Trans", "GET_MONTHS");
            dt_month = dataaccess.ExecuteSP("Sp_Score_Board", ht_month);

            DataRow dr = dt_month.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_month.Rows.InsertAt(dr, 0);

            lookUpEdit_Accuracy_Month.Properties.DataSource = dt_month;
            lookUpEdit_Accuracy_Month.Properties.DisplayMember = "monthname";
            lookUpEdit_Accuracy_Month.Properties.ValueMember = "mth";
           
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col_month;
            col_month = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("monthname", 100);
            lookUpEdit_Accuracy_Month.Properties.Columns.Add(col_month);



        }

        public void BindLookUpEdit_Accuracy_Year()
        {
            Hashtable ht_Year = new Hashtable();
            System.Data.DataTable dt_Year = new System.Data.DataTable();

            lookUpEdit_Accuracy_Year.Properties.DataSource = null;

            ht_Year.Add("@Trans", "GET_YEARS");
            dt_Year = dataaccess.ExecuteSP("Sp_Score_Board", ht_Year);

            //DataRow dr = dt_Year.NewRow();
            //dr[0] = "SELECT";
            //dr[1] = "SELECT";
            //dt_Year.Rows.InsertAt(dr, 0);

            lookUpEdit_Accuracy_Year.Properties.DataSource = dt_Year;
            lookUpEdit_Accuracy_Year.Properties.DisplayMember = "year";
            lookUpEdit_Accuracy_Year.Properties.ValueMember = "year";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col_year;
            col_year = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("year", 100);
            lookUpEdit_Accuracy_Year.Properties.Columns.Add(col_year);



        }

        private void lookUpEdit_Accuracy_Month_Click(object sender, EventArgs e)
        {
            lookUpEdit_Accuracy_Month.EditValue = null;
            lookUpEdit_Accuracy_Month.EditValue = "SELECT";
            lookUpEdit_Accuracy_Month.EditValue = 0;
        }

        private void lookUpEdit_Accuracy_Year_Click(object sender, EventArgs e)
        {
            lookUpEdit_Accuracy_Year.EditValue = null;
            lookUpEdit_Accuracy_Year.EditValue = "SELECT";
            lookUpEdit_Accuracy_Year.EditValue = 0;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
              SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
              try
              {

                  SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                  try
                  {
                      
                      DevExpress.XtraPrinting.PrintingSystem ps_daily = new DevExpress.XtraPrinting.PrintingSystem();
                      DevExpress.XtraPrintingLinks.CompositeLink compositeLink_Daily = new DevExpress.XtraPrintingLinks.CompositeLink(ps_daily);

                      DevExpress.XtraPrinting.PrintableComponentLink link_Daily = new DevExpress.XtraPrinting.PrintableComponentLink();

                      // Show the Document Map toolbar button and menu item.
                      ps_daily.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                      // Make the "Export to Csv" and "Export to Txt" commands visible.
                      ps_daily.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                      compositeLink_Daily.PrintingSystem = ps_daily;


                      link_Daily.Component = pivotGridControl1;
                      link_Daily.PaperName = "Niranjan";
                      compositeLink_Daily.Links.AddRange(new object[] { link_Daily });


                      string ReportName = "Daily-Wise-Status-Report";
                      string folderPath = "C:\\Temp\\";
                      string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                      //  compositeLink.ShowPreview();
                      compositeLink_Daily.CreatePageForEachLink();

                      // this is for Creating excel sheet name
                  //    ps_daily.XlSheetCreated += PrintingSystem_XlSheetCreated_Daily_Wise;

                      compositeLink_Daily.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
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


                 // Export_All_PivotGrid();
              }
              finally
              {

                  SplashScreenManager.CloseForm(false);
              }

        }

        private void btn_Accuracy_Clear_Click(object sender, EventArgs e)
        {
            lookUpEdit_Accuracy_Month.EditValue = 0;
            lookUpEdit_Accuracy_Year.EditValue = 0;

          
        }

        private void btn_Accuracy_Submit_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                int month_id = 0; int year_id = 0;
                object obj = lookUpEdit_Accuracy_Month.EditValue;
                string month_name = lookUpEdit_Accuracy_Month.Text;
                if (obj.ToString() != "0")
                {
                    month_id = (int)obj;
                }

                object obj_year = lookUpEdit_Accuracy_Year.EditValue;
                string year = lookUpEdit_Accuracy_Year.Text;
                if (obj_year.ToString() != "0")
                {
                    year_id = (int)obj_year;
                }
                // get First and last day of the Month and Year

                first = new DateTime(year_id, month_id, 1);
                last = first.AddMonths(1).AddSeconds(-1);
                Hashtable ht_get_accuracy = new Hashtable();
                System.Data.DataTable dt_get_accuracy = new System.Data.DataTable();
                if (User_Role_Id != "2")
                {
                    ht_get_accuracy.Add("@Trans", "CALCULATE_MONTHLY_WISE_ACCURACY");
                }
                else if (User_Role_Id == "2")
                {

                    ht_get_accuracy.Add("@Trans", "CALCULATE_MONTHLY_ACCURACY_USER_WISE");
                }
                ht_get_accuracy.Add("@From_date", first);
                ht_get_accuracy.Add("@To_Date", last);
                ht_get_accuracy.Add("@Error_On_User_Id",userid);
                dt_get_accuracy = dataaccess.ExecuteSP("Sp_Accuracy_Details", ht_get_accuracy);

                dt_get = dt_get_accuracy;
                if (dt_get_accuracy.Rows.Count > 0)
                {
                    pivotGridControl1.DataSource = dt_get_accuracy;

                    pivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    pivotGridField4.CellFormat.Format = new CultureInfo("de");
                    pivotGridField4.CellFormat.FormatString = "{0:N0}%";

                }
                else
                {
                    pivotGridControl1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem With loading records Please check it");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

       
        private void Export_All_PivotGrid()
        {
            PivotGridControl[] grids = new PivotGridControl[] { pivotGridControl1 };
            DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink(ps);

            // Show the Document Map toolbar button and menu item.
            ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

            // Make the "Export to Csv" and "Export to Txt" commands visible.
            ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
            compositeLink.PrintingSystem = ps;
            foreach (PivotGridControl grid in grids)
            {
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = grid;
                //compositeLink.Links.Add(link);
                compositeLink.Links.AddRange(new object[] { link });
            }

            string ReportName = "Daily_Status_Report";
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";

            compositeLink.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage });
            compositeLink.ShowPreview();
            compositeLink.CreatePageForEachLink();

          
        }

        private void pivotGridControl1_CellClick(object sender, PivotCellEventArgs e)
        {
            //Employee_24_7_Production_user_id = userid.ToString();
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
               PivotGridHitInfo hi = pivotGridControl1.CalcHitInfo(pivotGridControl1.PointToClient(MousePosition));
               if (hi.HitTest == PivotGridHitTest.Cell)
               {
                     string V_Data = "";
                     string Column_Name = "";
                     string Row_Value_Type = ""; string Column_Value_Type = "";
                     string User_Name = ""; string Branch_Name = "";
                     string V_UserName = ""; string V_Date = "";
                     string V_BranchName = "";

                     Row_Value_Type = hi.CellInfo.RowValueType.ToString();
                     Column_Value_Type = hi.CellInfo.ColumnValueType.ToString();

                    // MessageBox.Show("Row_value_Type"+Row_Value_Type + " Column_Value_Type:" + Column_Value_Type);
                     Column_Name = hi.CellInfo.DataField.FieldName.ToString();
                     //Row_Name = hi.CellInfo.RowField.FieldName.ToString();


                     foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.ColumnArea))
                     {
                         if (Column_Name == "No_Of_Errors")
                         {
                             V_Data = e.GetFieldValue(pivotGridField2).ToString();
                         }
                         if (Column_Name == "No_of_Completed_orders")
                         {
                             V_Data = e.GetFieldValue(pivotGridField3).ToString();
                         }
                         if (Column_Name == "Accuracy")
                         {
                             V_Data = e.GetFieldValue(pivotGridField4).ToString();
                         }
                     }

                     int month_id = 0; int year_id = 0;
                     object obj = lookUpEdit_Accuracy_Month.EditValue;
                     string month_name = lookUpEdit_Accuracy_Month.Text;
                     if (obj.ToString() != "0")
                     {
                         month_id = (int)obj;
                     }

                     object obj_year = lookUpEdit_Accuracy_Year.EditValue;
                     string year = lookUpEdit_Accuracy_Year.Text;
                     if (obj_year.ToString() != "0")
                     {
                         year_id = (int)obj_year;
                     }
                     // get First and last day of the Month and Year

                     first = new DateTime(year_id, month_id, 1);
                     last = first.AddMonths(1).AddSeconds(-1);

                     string Fromdate = first.ToString();
                     string Todate = last.ToString();
                    //foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.RowArea))
                    //{

                    //    V_UserName = e.GetFieldValue(pivotGridField5).ToString();
                    //    User_Name = V_UserName;
                    //}
                    //foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.ColumnArea))
                    //{

                    //    V_Date = e.GetFieldValue(pivotGridField1).ToString();

                    //    //Production_Date = V_Date;
                    //}
                    //foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.RowArea))
                    //{

                    //    V_BranchName = e.GetFieldValue(pivotGridField12).ToString();

                    //    Branch_Name = V_BranchName;
                    //}


               

                    if (V_Data != "" && V_Data != "0")
                 {
                     if (Row_Value_Type == "Value" && Column_Value_Type == "Value")// This is for Non Summary Click Event
                     {
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.ColumnArea))
                         {
                             V_Date = e.GetFieldValue(pivotGridField1).ToString();
                         }
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.RowArea))
                         {
                             V_UserName = e.GetFieldValue(pivotGridField5).ToString();
                             User_Name = V_UserName;
                         }
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.RowArea))
                         {
                             V_BranchName = e.GetFieldValue(pivotGridField12).ToString();
                             Branch_Name = V_BranchName;
                         }

                         //if (V_Data != "" && V_Data != "0")
                         //{
                             
                             Hashtable ht_get_grid = new Hashtable();
                             System.Data.DataTable dt_get_grid = new System.Data.DataTable();
                             ht_get_grid.Clear();
                             dt_get_grid.Clear();
                             ht_get_grid.Add("@Trans", "GET_USER_ID");
                             ht_get_grid.Add("@Emp_Name", User_Name);
                             dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_grid);

                            //
                            Hashtable ht_insert = new Hashtable();
                            System.Data.DataTable dt_insert = new System.Data.DataTable();
                            //ht_insert.Clear();
                            //dt_insert.Clear();

                            ht_insert.Add("@Trans", "INSERT_INTO_TEMP_USER_NEW_USER_WISE");
                            ht_insert.Add("@User_Id", dt_get_grid.Rows[0]["User_id"].ToString());
                            ht_insert.Add("@Production_Date", V_Date.ToString());
                            dt_insert = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", ht_insert);

                            Ordermanagement_01.Reports.Accuracy_Detail_Section TargeDashboard
                            = new Ordermanagement_01.Reports.Accuracy_Detail_Section(int.Parse(dt_get_grid.Rows[0]["User_id"].ToString()), User_Role_Id, V_Date.ToString(), int.Parse(dt_get_grid.Rows[0]["Branch_ID"].ToString()),"","");
                             TargeDashboard.Show();

                         //}
                         //else
                         //{
                         //    SplashScreenManager.CloseForm(false);
                         //}
                     }
                     //2

                     else if (Row_Value_Type == "Value" && Column_Value_Type == "Total")// this is For Column Grand Total // this is for Single user & All Date
                     {
                         //if (V_Data != "" && V_Data != "0")
                         //{

                             Hashtable ht_get_grid = new Hashtable();
                             System.Data.DataTable dt_get_grid = new System.Data.DataTable();
                             ht_get_grid.Clear();
                             dt_get_grid.Clear();
                             ht_get_grid.Add("@Trans", "GET_USER_ID");
                             ht_get_grid.Add("@Emp_Name", User_Name);
                             // ht_get_grid.Add("@Branch_Name", Branch_Name);
                             dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_grid);

                             //Hashtable htinsert = new Hashtable();
                             //System.Data.DataTable dtinsert = new System.Data.DataTable();

                             //htinsert.Add("@Trans", "INSERT_INTO_TEMP_USER");
                             //htinsert.Add("@Production_Date", V_Date.ToString());
                             //htinsert.Add("@User_Id", int.Parse(dt_get_grid.Rows[0]["User_id"].ToString()));
                             //dtinsert = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htinsert);

                             //Hashtable htinsert = new Hashtable();
                             //System.Data.DataTable dtinsert = new System.Data.DataTable();

                             //htinsert.Add("@Trans", "INSERT_INTO_TEMP_USER_1");
                             //htinsert.Add("@From_Date", Fromdate.ToString());
                             //htinsert.Add("@To_Date", Todate.ToString());
                             //dtinsert = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htinsert);


                             Ordermanagement_01.Reports.Accuracy_Detail_Section TargeDashboard
                            = new Ordermanagement_01.Reports.Accuracy_Detail_Section(int.Parse(dt_get_grid.Rows[0]["User_id"].ToString()), User_Role_Id, V_Date.ToString(), int.Parse(dt_get_grid.Rows[0]["Branch_ID"].ToString()),"","");
                             TargeDashboard.Show();

                         //}
                         //else
                         //{
                         //    SplashScreenManager.CloseForm(false);
                         //}
                     }
                    //3
                     else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")// this is For Column Grand Total // this is for Single user & All Date
                     {
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.RowArea))
                         {
                             V_UserName = e.GetFieldValue(pivotGridField5).ToString();
                             User_Name = V_UserName;
                         }
                       

                         //if (V_Data != "" && V_Data != "0")
                         //{
                             Hashtable ht_get_grid = new Hashtable();
                             System.Data.DataTable dt_get_grid = new System.Data.DataTable();
                             ht_get_grid.Clear();
                             dt_get_grid.Clear();
                             ht_get_grid.Add("@Trans", "GET_USER_ID");
                             ht_get_grid.Add("@Emp_Name", User_Name);
                             // ht_get_grid.Add("@Branch_Name", Branch_Name);
                             dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_grid);

                             //Hashtable htinsert = new Hashtable();
                             //System.Data.DataTable dtinsert = new System.Data.DataTable();

                             //htinsert.Add("@Trans", "INSERT_INTO_TEMP_USER");
                             //htinsert.Add("@Production_Date", V_Date.ToString());
                             //htinsert.Add("@User_Id", int.Parse(dt_get_grid.Rows[0]["User_id"].ToString()));
                             //dtinsert = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htinsert);


                             Ordermanagement_01.Reports.Accuracy_Detail_Section TargeDashboard
                            = new Ordermanagement_01.Reports.Accuracy_Detail_Section(int.Parse(dt_get_grid.Rows[0]["User_id"].ToString()), User_Role_Id, V_Date.ToString(), int.Parse(dt_get_grid.Rows[0]["Branch_ID"].ToString()), Fromdate, Todate);
                             TargeDashboard.Show();
                         //}
                         //else
                         //{
                         //    SplashScreenManager.CloseForm(false);
                         //}
                     }
                    //4
                     else if (Row_Value_Type == "Total" && Column_Value_Type == "Value")// this is For Row Total // this is for Single user & All Date
                     {
                       
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.ColumnArea))
                         {
                             V_Date = e.GetFieldValue(pivotGridField1).ToString().Trim(); 
                             
                         }
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.RowArea))
                         {
                             string val2 = e.GetFieldValue(pivotGridField12).ToString();
                             Branch_Name = val2;
                         }
                         
                         //if (V_Data != "" && V_Data != "0")
                         //{
                             Hashtable ht_get_grid = new Hashtable();
                             System.Data.DataTable dt_get_grid = new System.Data.DataTable();
                             ht_get_grid.Clear();
                             dt_get_grid.Clear();
                             ht_get_grid.Add("@Trans", "GET_BRANCH_ID");
                             ht_get_grid.Add("@Branch_Name", Branch_Name);
                             dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_grid);
                          
                             Ordermanagement_01.Reports.Accuracy_Detail_Section TargeDashboard = new Ordermanagement_01.Reports.Accuracy_Detail_Section(0, User_Role_Id, V_Date.ToString(), int.Parse(dt_get_grid.Rows[0]["Branch_ID"].ToString()),"","");
                             TargeDashboard.Show();
                         //}
                         //else
                         //{
                         //    SplashScreenManager.CloseForm(false);
                         //}
                     }

                       //5
                     else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")// this is For row Grand Total // this is for Single date & All Date
                     {
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.ColumnArea))
                         {
                             V_Date = e.GetFieldValue(pivotGridField1).ToString().Trim();
                             
                         }

                         //if (V_Data != "" && V_Data != "0")
                         //{

                             Ordermanagement_01.Reports.Accuracy_Detail_Section TargeDashboard = new Ordermanagement_01.Reports.Accuracy_Detail_Section(0, User_Role_Id, V_Date.ToString(), 0,"","");
                             TargeDashboard.Show();
                         //}
                         //else
                         //{
                         //    SplashScreenManager.CloseForm(false);
                         //}
                     }
                     //6
                     else if (Row_Value_Type == "Total" && Column_Value_Type == "GrandTotal")// this is For Row Total // this is for Single user & All Date
                     {
                         foreach (var field in pivotGridControl1.GetFieldsByArea(PivotArea.RowArea))
                         {
                             string val2 = e.GetFieldValue(pivotGridField12).ToString();
                             Branch_Name = val2;
                         }
                         
                         //if (V_Data != "" && V_Data != "0")
                         //{

                             Hashtable ht_get_grid = new Hashtable();
                             System.Data.DataTable dt_get_grid = new System.Data.DataTable();
                             ht_get_grid.Clear();
                             dt_get_grid.Clear();
                             ht_get_grid.Add("@Trans", "GET_BRANCH_ID");
                             ht_get_grid.Add("@Branch_Name", Branch_Name);
                             dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_grid);


                                 Ordermanagement_01.Reports.Accuracy_Detail_Section TargeDashboard = new Ordermanagement_01.Reports.Accuracy_Detail_Section(0, User_Role_Id, V_Date.ToString(), int.Parse(dt_get_grid.Rows[0]["Branch_ID"].ToString()), Fromdate, Todate);
                                 TargeDashboard.Show();
                          //}
                          //else
                          //{
                          //       SplashScreenManager.CloseForm(false);
                          //}

                        
                     }

                     //7
                     else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal")// this is For Row Total // this is for Single user & All Date
                     {
                         //if (V_Data != "" && V_Data != "0")
                         //{

                             Ordermanagement_01.Reports.Accuracy_Detail_Section TargeDashboard = new Ordermanagement_01.Reports.Accuracy_Detail_Section(0, User_Role_Id, "", 0, Fromdate, Todate);
                             TargeDashboard.Show();

                         //}
                         //else
                         //{
                         //    SplashScreenManager.CloseForm(false);
                         //}
                     }

                   }
                   else
                   {
                       SplashScreenManager.CloseForm(false);
                       
                   }
               }
               else
               {
                   SplashScreenManager.CloseForm(false);
                   MessageBox.Show("Error Occured Please Check With Administrator");
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

        private void pivotGridControl1_CustomCellValue(object sender, PivotCellValueEventArgs e)
        {
            try
            {

                // row wise GrandTotal only 
                if (e.DataField == pivotGridField4 && e.RowValueType == PivotGridValueType.GrandTotal && e.ColumnValueType == PivotGridValueType.Value)
                {
                    decimal summaryValue1 = Convert.ToDecimal(e.GetFieldValue(pivotGridField2));
                    decimal summaryValue2 = Convert.ToDecimal(e.GetFieldValue(pivotGridField3));
                    //decimal value=summaryValue1 / summaryValue2;

                    if (summaryValue1 != 0)
                    {
                        if (summaryValue1 != 0 && summaryValue2 != 0)
                        {
                            decimal acc = summaryValue1 / summaryValue2;
                            e.Value = 100 * (1 - acc);
                        }
                    }
                    else
                    {
                        e.Value = 0;
                    }
                }

                // Column wise grand total
                if (e.DataField == pivotGridField4 && e.RowValueType == PivotGridValueType.GrandTotal && e.ColumnValueType == PivotGridValueType.GrandTotal)
                {
                    decimal summaryValue1 = Convert.ToDecimal(e.GetFieldValue(pivotGridField2));
                    decimal summaryValue2 = Convert.ToDecimal(e.GetFieldValue(pivotGridField3));
                    //decimal value=summaryValue1 / summaryValue2;

                    if (summaryValue1 != 0)
                    {
                        if (summaryValue1 != 0 && summaryValue2 != 0)
                        {
                            decimal acc = summaryValue1 / summaryValue2;
                            e.Value = 100 * (1 - acc);
                        }
                    }
                    else
                    {
                        e.Value = 0;
                    }
                }

                // row wise Total
                if (e.DataField == pivotGridField4 && e.RowValueType == PivotGridValueType.Total && e.ColumnValueType == PivotGridValueType.GrandTotal)
                {
                    decimal summaryValue1 = Convert.ToDecimal(e.GetFieldValue(pivotGridField2));
                    decimal summaryValue2 = Convert.ToDecimal(e.GetFieldValue(pivotGridField3));
                    if (summaryValue1 != 0)
                    {
                        if (summaryValue1 != 0 && summaryValue2 != 0)
                        {
                            decimal acc = summaryValue1 / summaryValue2;
                            e.Value = 100 * (1 - acc);
                        }
                    }
                    else
                    {
                        e.Value = 0;
                    }
                }

                // row wise Total and coulmn wise total
                if (e.DataField == pivotGridField4 && e.RowValueType == PivotGridValueType.Total && e.ColumnValueType == PivotGridValueType.Value)
                {
                    decimal summaryValue1 = Convert.ToDecimal(e.GetFieldValue(pivotGridField2));
                    decimal summaryValue2 = Convert.ToDecimal(e.GetFieldValue(pivotGridField3));
                    //decimal value=summaryValue1 / summaryValue2;

                    if (summaryValue1 != 0)
                    {
                        if (summaryValue1 != 0 && summaryValue2 != 0)
                        {
                            decimal acc = summaryValue1 / summaryValue2;
                            e.Value = 100 * (1 - acc);
                        }
                    }
                    else
                    {
                        e.Value = 0;
                    }
                }

                // row wise value and coulmn wise Grand Total
                if (e.DataField == pivotGridField4 && e.RowValueType == PivotGridValueType.Value && e.ColumnValueType == PivotGridValueType.GrandTotal)
                {
                    decimal summaryValue1 = Convert.ToDecimal(e.GetFieldValue(pivotGridField2));
                    decimal summaryValue2 = Convert.ToDecimal(e.GetFieldValue(pivotGridField3));
                    //decimal value=summaryValue1 / summaryValue2;

                    if (summaryValue1 != 0)
                    {
                        if (summaryValue1 != 0 && summaryValue2 != 0)
                        {
                            decimal acc = summaryValue1 / summaryValue2;
                            e.Value = 100 * (1 - acc);
                        }
                    }
                    else
                    {
                        e.Value = 0;
                    }

                    //
                    if (summaryValue1 == 0 && summaryValue1 != null)
                    {
                        if (summaryValue2 != 0)
                        {
                            e.Value = 100;
                        }
                        else if (summaryValue2 == 0)
                        {
                            e.Value = 0;
                        }
                    }
                    
                }

            }
            catch
            {
                MessageBox.Show("Problem With loading records Please check it");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);

            }
        }

        
       

        

    }
}