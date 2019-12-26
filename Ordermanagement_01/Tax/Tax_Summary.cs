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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.IO;
using ClosedXML.Excel;
using DevExpress.XtraSplashScreen;
namespace Ordermanagement_01.Tax
{
    public partial class Tax_Summary : DevExpress.XtraEditors.XtraForm
    {
        private DateTime startOfMonth, endOfMonth;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        private readonly string  User_Role_Id;
        private readonly int userId;
        public Tax_Summary(int userId,string USER_ROLE_ID)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                InitializeComponent();
                User_Role_Id = USER_ROLE_ID;
                this.userId = userId;
                string D2 = DateTime.Now.ToString("MM/dd/yyyy");
                dateEditToDate.Text = D2;

            }
            finally
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

            }

        }

        private void Tax_Summary_Load(object sender, EventArgs e)
        {
            var today = DateTime.Now;
            startOfMonth = new DateTime(today.Year, today.Month, 1);
            dateEditFromDate.Text = startOfMonth.ToShortDateString();
            this.WindowState = FormWindowState.Maximized;
        }
        private void dateEditFromDate_EditValueChanged(object sender, EventArgs e)
        {
            endOfMonth = Convert.ToDateTime(dateEditFromDate.Text).AddMonths(1).AddDays(-1);
            if (endOfMonth > DateTime.Now) endOfMonth = DateTime.Now;
            dateEditToDate.Text = endOfMonth.ToShortDateString();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {


            if (String.IsNullOrWhiteSpace(dateEditFromDate.Text) || String.IsNullOrWhiteSpace(dateEditToDate.Text))
            {
                XtraMessageBox.Show("From date and To date should not be empty");
                return;
            }
            bool isValid = false;
            if (Convert.ToDateTime(dateEditFromDate.Text) > DateTime.Now)
            {
                isValid = false;
                XtraMessageBox.Show("Select a valid from date");
                return;
            }
            else
            {
                isValid = true;
            }

            if (isValid)
            {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                try
                {
                    Bind_Tax_Summary();
                    Bind_Tax_Summary_Details();

                }
                catch (Exception ex)
                {

                    //Close Wait Form
                    XtraMessageBox.Show("Problem with loading Files Please Check with Administrator");
                    SplashScreenManager.CloseForm(false);
                }
                finally
                {
                    //Close Wait Form
                    SplashScreenManager.CloseForm(false);
                }

            }
        }

        private void Bind_Tax_Summary()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_SUMMARY");
            htselect.Add("@From_Date", dateEditFromDate.Text.ToString());
            htselect.Add("@To_Date", dateEditToDate.Text.ToString());

            dtselect = dataaccess.ExecuteSP("Sp_Tax_Summary", htselect);
            //@Company_Log

            if (dtselect.Rows.Count > 0)
            {
                Grd_Summary.DataSource = dtselect;

            }
            else
            {

                Grd_Summary.DataSource = null;
                Grd_Summary.Text = "No Records";
            }
            gridViewSummary.BestFitColumns();
        }

        private void Bind_Tax_Summary_Details()
        {

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_DETAIL");
            htselect.Add("@From_Date", dateEditFromDate.Text.ToString());
            htselect.Add("@To_Date", dateEditToDate.Text.ToString());
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Summary", htselect);
            //@Company_Log

            if (dtselect.Rows.Count > 0)
            {
                Grd_Details.DataSource = dtselect;

            }
            else
            {

                Grd_Details.DataSource = null;
                Grd_Details.Text = "No Records";
            }


            //if (User_Role_Id == "1")
            //{


            //    gridView2.Columns["Client_Name"].Visible = true;
            //    gridView2.Columns["Sub_ProcessName"].Visible = true;


            //    gridView2.Columns["Client_Number"].Visible = false;
            //    gridView2.Columns["Subprocess_Number"].Visible = false;
            //}
            //else if (User_Role_Id != "1")
            //{


            gridViewTaxOrders.Columns["Client_Name"].Visible = false;
            gridViewTaxOrders.Columns["Sub_ProcessName"].Visible = false;


            gridViewTaxOrders.Columns["Client_Number"].Visible = true;
            gridViewTaxOrders.Columns["Subprocess_Number"].Visible = true;
            //}
            // gridView2.BestFitColumns();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                DataTable dtSummary, dtDetails;
                dtSummary = GetDataTable(gridViewSummary);
                dtDetails = GetDataTable(gridViewTaxOrders);

                //if (User_Role_Id != "1")
                //{

                //    dtDetails.Columns.Remove("Client_Number");
                //    dtDetails.Columns.Remove("Subprocess_Number");

                //}
                //else if (User_Role_Id == "1")
                //{

                dtDetails.Columns.Remove("Client_Name");
                dtDetails.Columns.Remove("Sub_ProcessName");
                // }

                if ((dtSummary != null && dtSummary.Rows.Count > 0) && (dtDetails != null && dtDetails.Rows.Count > 0))
                {
                    string filePath = @"C:\Tax Summary\";
                    string fileName = filePath + "Tax Summary - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    else
                    {
                        try
                        {
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dtSummary, "Summary");
                                wb.Worksheets.Add(dtDetails, "Details");
                                wb.SaveAs(fileName);
                            }

                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Exported Successfully ");
                            System.Diagnostics.Process.Start(fileName);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Something went wrong while exporting summary");
                        }
                    }
                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("No Records found to Export");
                }

            }
            catch (Exception ex)
            {

                SplashScreenManager.CloseForm(false);
            }
            finally
            {

                SplashScreenManager.CloseForm(false);
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

        private void gridViewTaxOrders_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                int orderId = Convert.ToInt32(gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Order_ID"));
                Order_Entry order = new Order_Entry(orderId, userId, User_Role_Id, "");
                order.Show();
            }
        }

        private void gridViewSummary_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string Date;
            if (e.Column.FieldName == "Total_Orders")
            {
                Date = gridViewSummary.GetRowCellValue(e.RowHandle, "Date").ToString();
                var ht = new Hashtable();
                ht.Add("@Trans", "SELECT_DETAILS_STATUS_TOTAL");
                ht.Add("@Date", Date);
                var dt = new DataAccess().ExecuteSP("Sp_Tax_Summary", ht);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Tax_Summary_Details_View tax = new Tax_Summary_Details_View(dt, "Total Orders On : " + Date, userId,User_Role_Id);
                    tax.Show();
                }
                else
                {
                    XtraMessageBox.Show("Data Not Found");
                }

            }
            if (e.Column.FieldName == "Completed")
            {
                Date = gridViewSummary.GetRowCellValue(e.RowHandle, "Date").ToString();
                var ht = new Hashtable();
                ht.Add("@Trans", "SELECT_DETAILS_STATUS_WISE");
                ht.Add("@Date", Date);
                ht.Add("@Tax_Status", 1);
                var dt = new DataAccess().ExecuteSP("Sp_Tax_Summary", ht);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Tax_Summary_Details_View tax = new Tax_Summary_Details_View(dt, "Completed Orders On : " + Date, userId, User_Role_Id);
                    tax.Show();
                }
                else
                {
                    XtraMessageBox.Show("Data Not Found");
                }

            }
            if (e.Column.FieldName == "Pending")
            {
                Date = gridViewSummary.GetRowCellValue(e.RowHandle, "Date").ToString();
                var ht = new Hashtable();
                ht.Add("@Trans", "SELECT_DETAILS_STATUS_PENDING");
                ht.Add("@Date", Date);
                var dt = new DataAccess().ExecuteSP("Sp_Tax_Summary", ht);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Tax_Summary_Details_View tax = new Tax_Summary_Details_View(dt, "Pending Orders On : " + Date, userId, User_Role_Id);
                    tax.Show();
                }
                else
                {
                    XtraMessageBox.Show("Data Not Found");
                }

            }
            if (e.Column.FieldName == "Cancelled")
            {
                Date = gridViewSummary.GetRowCellValue(e.RowHandle, "Date").ToString();
                var ht = new Hashtable();
                ht.Add("@Trans", "SELECT_DETAILS_STATUS_WISE");
                ht.Add("@Date", Date);
                ht.Add("@Tax_Status", 4);
                var dt = new DataAccess().ExecuteSP("Sp_Tax_Summary", ht);

                if (dt != null && dt.Rows.Count > 0)
                {
                    Tax_Summary_Details_View tax = new Tax_Summary_Details_View(dt, "Cancelled Orders On : " + Date, userId, User_Role_Id);
                    tax.Show();
                }
                else
                {
                    XtraMessageBox.Show("Data Not Found");
                }
            }
        }
    }
}