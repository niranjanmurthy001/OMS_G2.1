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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using ClosedXML.Excel;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using DevExpress.XtraGrid;
using DevExpress.Utils;

namespace Ordermanagement_01.Dashboard
{
    public partial class ScoreBoard : DevExpress.XtraEditors.XtraForm
    {
        private DropDownistBindClass dbc;
        private DataAccess dataaccess;
        private readonly int userId;
        private readonly int userRoleId;
        public ScoreBoard(int userId, int userRoleId)
        {
            InitializeComponent();
            dbc = new DropDownistBindClass();
            this.userId = userId;
            this.userRoleId = userRoleId;
            dataaccess = new DataAccess();
        }
        private void ScoreBoard_Load(object sender, EventArgs e)
        {
            dbc.BindMonth(lookUpEditMonth);
            dbc.BindYear(lookUpEditYear);
            var htMonth = new Hashtable();

            htMonth.Add("@Trans", "GET_CURRENT_MONTH");
            var dtMonth = new DataAccess().ExecuteSP("Sp_Score_Board", htMonth);
            if (dtMonth.Rows.Count > 0)
            {
                lookUpEditMonth.EditValue = dtMonth.Rows[0]["C_Month"];
            }
            var htYear = new Hashtable();
            htYear.Add("@Trans", "CURRENT_YEAR");
            var dtYear = dataaccess.ExecuteSP("Sp_Score_Board", htYear);
            if (dtYear != null && dtYear.Rows.Count > 0)
            {
                lookUpEditYear.EditValue = dtYear.Rows[0]["Year"];
            }
            else
            {
                lookUpEditYear.EditValue = null;
            }
            this.WindowState = FormWindowState.Maximized;
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Updated_Score_Board();
            }
        }    
        private void Updated_Score_Board()
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                gridControlScoreBoard.DataSource = null;
                gridViewScoreBoard.Columns.Clear();
                DateTime d1 = DateTime.Now;
                d1 = d1.AddDays(-1);
                Hashtable ht_Score1 = new Hashtable();

                System.Data.DataTable dt_Score1 = new System.Data.DataTable();
                dt_Score1.Clear();

                Hashtable ht_Get_User_Orders = new Hashtable();
                System.Data.DataTable dt_Get_User_Orders = new System.Data.DataTable();

                if (userRoleId == 2)
                {
                    ht_Get_User_Orders.Add("@Trans", "CALCUATE_MONTHLY_WISE_USER_EFFECINECY_BY_USER_ID_SCORE_BOARD2");
                }
                else
                {
                    ht_Get_User_Orders.Add("@Trans", "CALCUATE_MONTHLY_WISE_USER_EFFECINECY_FOR_SCORE_BOARD2");
                }

                ht_Get_User_Orders.Add("@User_Id", userId);
                ht_Get_User_Orders.Add("@Month", lookUpEditMonth.EditValue.ToString()); //ddl_Month.SelectedValue.ToString()
                ht_Get_User_Orders.Add("@Years", lookUpEditYear.EditValue.ToString()); //ddl_Year.SelectedValue.ToString()
                dt_Get_User_Orders = dataaccess.ExecuteSP("Sp_Score_Board", ht_Get_User_Orders);


                Hashtable ht_Score = new Hashtable();

                System.Data.DataTable dt_Score = new System.Data.DataTable();
                System.Data.DataTable dt_Score2 = new System.Data.DataTable();
                dt_Score2.Clear();




                if (userRoleId == 2)
                {
                    ht_Score.Add("@Trans", "GET_USER_NEWLY_UPDATED_ORDER_EFFECINECY_USER_ID_WISE");
                }
                else
                {

                    // Insert to temp table data
                    Hashtable ht_Temp_Score = new Hashtable();

                    System.Data.DataTable dt_Temp_Score = new System.Data.DataTable();
                    ht_Temp_Score.Add("@Trans", "INSERT_EFF_TO_TEMP");
                    dt_Temp_Score = dataaccess.ExecuteSP("Sp_Score_Board", ht_Temp_Score);

                    if (checkEditProductionTimeWise.Checked == true)
                    {
                        ht_Score.Add("@Trans", "GET_USER_UPDATED_NEW_ORDER_EFFECINECY");
                    }
                    else if (checkEditTargetWise.Checked == true)
                    {
                        ht_Score.Add("@Trans", "GET_USER_UPDATED_TARGET_WISE_ORDER_EFFECINECY");
                    }
                }

                ht_Score.Add("@User_Id", userId);
                dt_Score2 = dataaccess.ExecuteSP("Sp_Score_Board", ht_Score);

                Hashtable htget_Avg_Total_eff = new Hashtable();
                System.Data.DataTable dtget_Avg_Total_Eff = new System.Data.DataTable();

                if (userRoleId == 2)
                {
                    htget_Avg_Total_eff.Add("@Trans", "GET_NEWLY_UPDATED_AVG_EFF_BY_USER_ID");
                }
                else
                {

                    if (checkEditProductionTimeWise.Checked == true)
                    {
                        htget_Avg_Total_eff.Add("@Trans", "GET_NEWLY_UPDATED_AVG_EFF");
                    }
                    else if (checkEditTargetWise.Checked == true)
                    {

                        htget_Avg_Total_eff.Add("@Trans", "GET_NEWLY_TARGETED_UPDATED_AVG_EFF");
                    }

                }
                htget_Avg_Total_eff.Add("@User_Id", userId);
                dtget_Avg_Total_Eff = dataaccess.ExecuteSP("Sp_Score_Board", htget_Avg_Total_eff);

                System.Data.DataTable dt_Final_Score1 = new System.Data.DataTable();

                var collection = from t1 in dt_Score2.AsEnumerable()
                                 join t2 in dtget_Avg_Total_Eff.AsEnumerable()
                                   on t1["User_Id"] equals t2["User_Id"]
                                 select new
                                 {
                                     User_Id = t1["User_Id"],
                                     User_Name = t1["User_Name"],
                                     Branch_Name = t1["Branch_Name"],
                                     DRN_Emp_Code = t1["DRN_Emp_Code"],
                                     Emp_Job_Role = t1["Emp_Job_Role"],
                                     Operation_Type = t1["Operation_Type"],
                                     Shift_Type_Name = t1["Shift_Type_Name"],
                                     Reporting_To_1 = t1["Reporting_To_1"],
                                     Reporting_To_2 = t1["Reporting_To_2"],
                                     Avg_Eff = t2["Avg_Eff"],
                                     D1 = t1["1"],
                                     D2 = t1["2"],
                                     D3 = t1["3"],
                                     D4 = t1["4"],
                                     D5 = t1["5"],
                                     D6 = t1["6"],
                                     D7 = t1["7"],
                                     D8 = t1["8"],
                                     D9 = t1["9"],
                                     D10 = t1["10"],
                                     D11 = t1["11"],
                                     D12 = t1["12"],
                                     D13 = t1["13"],
                                     D14 = t1["14"],
                                     D15 = t1["15"],
                                     D16 = t1["16"],
                                     D17 = t1["17"],
                                     D18 = t1["18"],
                                     D19 = t1["19"],
                                     D20 = t1["20"],
                                     D21 = t1["21"],
                                     D22 = t1["22"],
                                     D23 = t1["23"],
                                     D24 = t1["24"],
                                     D25 = t1["25"],
                                     D26 = t1["26"],
                                     D27 = t1["27"],
                                     D28 = t1["28"],
                                     D29 = t1["29"],
                                     D30 = t1["30"],
                                     D31 = t1["31"],
                                 };
                System.Data.DataTable result = new System.Data.DataTable("Final_Data");
                result.Columns.Add("User_Id", typeof(string));
                result.Columns.Add("User_Name", typeof(string));
                result.Columns.Add("Branch_Name", typeof(string));
                result.Columns.Add("DRN_Emp_Code", typeof(string));
                result.Columns.Add("Emp_Job_Role", typeof(string));
                result.Columns.Add("Operation_Type", typeof(string));
                result.Columns.Add("Shift_Type_Name", typeof(string));
                result.Columns.Add("Reporting_To_1", typeof(string));
                result.Columns.Add("Reporting_To_2", typeof(string));
                result.Columns.Add("Avg_Eff", typeof(string));

                System.Data.DataTable dt_Final_Score = new System.Data.DataTable();
                Hashtable htgetfirst_Last_Dates = new Hashtable();
                System.Data.DataTable dtgetfirst_last_Dates = new System.Data.DataTable();

                htgetfirst_Last_Dates.Add("@Trans", "GET_FIRST_LAST_DATE");
                htgetfirst_Last_Dates.Add("@Month", int.Parse(lookUpEditMonth.EditValue.ToString()));
                htgetfirst_Last_Dates.Add("@Years", int.Parse(lookUpEditYear.EditValue.ToString()));
                dtgetfirst_last_Dates = dataaccess.ExecuteSP("Sp_Score_Board", htgetfirst_Last_Dates);

                Hashtable htdatecolumn = new Hashtable();

                System.Data.DataTable dtdatecolumn = new System.Data.DataTable();
                htdatecolumn.Add("@Trans", "GET_DATES");
                htdatecolumn.Add("@date_from1", dtgetfirst_last_Dates.Rows[0]["First_Date"].ToString());
                htdatecolumn.Add("@date_to1", dtgetfirst_last_Dates.Rows[0]["Last_Date"].ToString());
                dtdatecolumn = dataaccess.ExecuteSP("Sp_Score_Board", htdatecolumn);
                foreach (DataRow row in dtdatecolumn.Rows)
                {
                    result.Columns.Add(row["dts"].ToString(), typeof(object));
                }
                if (lookUpEditMonth.EditValue.ToString() == "2")
                {
                    if (result.Columns.Contains("29/02"))
                    {
                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28, item.D29);
                        }
                    }
                    else
                    {
                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28);
                        }
                    }
                }
                else
                {
                    if (result.Columns.Contains("31/" + GetDigitAppended(lookUpEditMonth.EditValue.ToString())))
                    {
                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28, item.D29, item.D30, item.D31);
                        }
                    }
                    else
                    {

                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28, item.D29, item.D30);
                        }
                    }


                }

                if (result.Rows.Count > 0)
                {
                    gridControlScoreBoard.DataSource = result;
                    gridViewScoreBoard.Columns.ColumnByFieldName("User_Id").Visible = false;
                    gridViewScoreBoard.Columns.ColumnByFieldName("User_Name").OptionsColumn.AllowFocus = false;
                    gridViewScoreBoard.BestFitColumns();
                    gridViewScoreBoard.ShowFindPanel();
                    foreach (GridColumn column in ((DevExpress.XtraGrid.Views.Base.ColumnView)gridControlScoreBoard.Views[0]).Columns)
                    {
                        if (column.FieldName == "User_Name") column.Fixed = FixedStyle.Left;
                        if (column.FieldName == "Avg_Eff")
                        {
                            column.FilterMode = ColumnFilterMode.DisplayText;
                            column.SortMode = ColumnSortMode.DisplayText;
                            column.OptionsColumn.AllowSort = DefaultBoolean.True;
                        }
                        if (column.FieldName != "User_Name" && column.FieldName != "Branch_Name" && column.FieldName != "DRN_Emp_Code"
                            && column.FieldName != "Emp_Job_Role" && column.FieldName != "Shift_Type_Name" && column.FieldName != "Reporting_To_1" &&
                            column.FieldName != "Reporting_To_2" && column.FieldName != "Avg_Eff" && column.FieldName != "Operation_Type")
                        {
                            RepositoryItemHyperLinkEdit edit = new RepositoryItemHyperLinkEdit();
                            edit.Appearance.ForeColor = Color.Blue;
                            edit.Appearance.Options.UseForeColor = true;
                            column.ColumnEdit = edit;
                            column.FilterMode = ColumnFilterMode.DisplayText;
                            column.SortMode = ColumnSortMode.DisplayText;
                            column.OptionsColumn.AllowSort = DefaultBoolean.True;
                        }
                    }
                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Data not found");
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong check with admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private string GetDigitAppended(string p)
        {
            int count = (int)Math.Log10((Convert.ToInt32(p))) + 1;
            if (count > 1) { return p.ToString(); }
            else
            {
                return "0" + p;
            }
        }
        private bool Validation()
        {
            if (lookUpEditMonth.EditValue.ToString() == "0")
            {
                XtraMessageBox.Show("Select month");
                lookUpEditMonth.Focus();
                return false;
            }
            if (lookUpEditYear.EditValue == null)
            {
                XtraMessageBox.Show("Select year");
                lookUpEditYear.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            gridControlScoreBoard.DataSource = null;
            gridViewScoreBoard.Columns.Clear();
            checkEditProductionTimeWise.Checked = false;
            checkEditTargetWise.Checked = false;
            lookUpEditMonth.EditValue = 0;
            lookUpEditYear.EditValue = null;
        }
        private void gridViewScoreBoard_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void checkEditProductionTimeWise_CheckedChanged(object sender, EventArgs e)
        {
            checkEditTargetWise.Checked = false;
        }
        private void checkEditTargetWise_CheckedChanged(object sender, EventArgs e)
        {
            checkEditProductionTimeWise.Checked = false;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (gridViewScoreBoard.RowCount > 0)
            {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                DataTable dtRows = GetDataTable(gridViewScoreBoard);
                dtRows.Columns.Remove("User_Id");
                try
                {
                    string fileName;

                    string filePath = @"C:\Efficiency\";
                    if (checkEditProductionTimeWise.Checked == true)
                    {
                        fileName = filePath + "Efficiency - " + checkEditProductionTimeWise.Text.ToString() + " " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    }
                    else if (checkEditTargetWise.Checked == true)
                    {
                        fileName = filePath + "Efficiency - " + checkEditTargetWise.Text.ToString() + " " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    }
                    else
                    {
                        fileName = filePath + "Efficiency - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    }
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    else
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dtRows, "Efficiency");
                            wb.SaveAs(fileName);
                        }
                        SplashScreenManager.CloseForm(false);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
                catch (Exception)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Somthing went wrong while exporting data");
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
            else
            {
                XtraMessageBox.Show("Data not found");
            }
        }
        private DataTable GetDataTable(GridView view)
        {
            DataTable dt = new DataTable();
            foreach (GridColumn c in view.Columns)
                dt.Columns.Add(c.FieldName, c.ColumnType);
            for (int r = 0; r < view.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                    rowValues[c] = view.GetRowCellValue(r, dt.Columns[c].ColumnName);
                dt.Rows.Add(rowValues);
            }
            return dt;
        }
        private void gridViewScoreBoard_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {            
            try
            {
                if (e.Column.FieldName != "User_Name" && e.Column.FieldName != "Branch_Name" && e.Column.FieldName != "Branch_Name" && e.Column.FieldName != "DRN_Emp_Code"
                          && e.Column.FieldName != "Emp_Job_Role" && e.Column.FieldName != "Shift_Type_Name" && e.Column.FieldName != "Reporting_To_1" &&
                          e.Column.FieldName != "Reporting_To_2" && e.Column.FieldName != "Avg_Eff" && e.Column.FieldName != "Operation_Type")
                {
                    if (String.IsNullOrEmpty(gridViewScoreBoard.GetRowCellValue(e.RowHandle, gridViewScoreBoard.Columns.ColumnByFieldName(e.Column.FieldName)).ToString()))
                    {
                        return;
                    }
                    string avgEfficiency=gridViewScoreBoard.GetRowCellValue(e.RowHandle, gridViewScoreBoard.Columns.ColumnByFieldName(e.Column.FieldName)).ToString();
                    int userId = Convert.ToInt32(gridViewScoreBoard.GetRowCellValue(e.RowHandle, "User_Id"));
                    string date;
                    if (lookUpEditYear.EditValue != null)
                    {
                        date = DateTime.ParseExact(e.Column.FieldName + "/" + lookUpEditYear.EditValue.ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        XtraMessageBox.Show("year not selected");
                        return;
                    }
                    var ht = new Hashtable();
                    ht.Add("@Trans", "INSERT_INTO_TEMP_USER");
                    ht.Add("@Production_Date", date);
                    ht.Add("@User_Id", userId);
                    var dt = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", ht);
                    Ordermanagement_01.Dashboard.Emp_Production_Score_Board TargeDashboard = new Ordermanagement_01.Dashboard.Emp_Production_Score_Board(userId, userRoleId.ToString(), date.ToString(), avgEfficiency);
                    TargeDashboard.Show();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Something went wrong");
            }
        }
    }
}