using System;
using DevExpress.XtraEditors;
using System.Windows.Forms;

using DevExpress.XtraSplashScreen;
using DevExpress.Data.Controls;
using DevExpress.XtraCharts;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Linq;
using Ordermanagement_01.Models;
using System.Threading.Tasks;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class Accuracy_Summary : XtraForm
    {
        private DateTime firstDateOfMonth, lastDateOfMonth;
        private readonly int userId, userRoleId;
        private readonly string productionDate;
        private readonly DropDownistBindClass dbc;
        private readonly DataAccess dataaccess;
        public Accuracy_Summary(int userId, int userRoleId, string productionDate)
        {
            this.userId = userId;
            this.userRoleId = userRoleId;
            this.productionDate = productionDate;
            dbc = new DropDownistBindClass();
            dataaccess = new DataAccess();
            InitializeComponent();
        }
        private void checkEditCustomize_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditCustomize.Checked)
            {
                lookUpEditMonth.Enabled = true;
                lookUpEditYear.Enabled = true;
                btnSubmit.Enabled = true;
            }
            else
            {
                lookUpEditMonth.Enabled = false;
                lookUpEditYear.Enabled = false;
                btnSubmit.Enabled = false;
            }
        }
        private void windowsUIButtonPanelType_ButtonChecked(object sender, ButtonEventArgs e)
        {
            foreach (WindowsUIButton btn in windowsUIButtonPanelType.Buttons)
            {
                if (btn != e.Button) btn.Checked = false;
            }
            WindowsUIButton button = e.Button as WindowsUIButton;
            string summaryType = string.Empty;

            switch (button.Tag.ToString())
            {
                case "Day":
                    firstDateOfMonth = DateTime.Parse(productionDate);
                    lastDateOfMonth = DateTime.Parse(productionDate);
                    break;
                case "Month":
                    firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    lastDateOfMonth = DateTime.Now;
                    break;
                case "Year":
                    firstDateOfMonth = new DateTime(DateTime.Now.Year, 1, 1);
                    lastDateOfMonth = DateTime.Now;
                    break;
            }
            var tasks = Task.WhenAll(
                BindAcccuracyAsync(),
                UpdateTempTableAsync(),
                BindErrorsAsync()
            );
        }
        private async Task UpdateTempTableAsync()
        {
            var dictUpdate = new Dictionary<string, object>() {
                { "@Trans", "INSERT_INTO_TEMP_USER_NEW_USER_WISE_DATE_RANGE" },
                { "@User_Id", userId },
                { "@From_Date", firstDateOfMonth },
                { "@To_Date", lastDateOfMonth }
            };
            var data = new StringContent(JsonConvert.SerializeObject(dictUpdate), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(Base_Url.Url + "/Accuracy/UpdateTempTable", data);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                       await BindAcccuracyDetailsAsync();
                    }
                }
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            firstDateOfMonth = new DateTime(Convert.ToInt32(lookUpEditYear.EditValue), Convert.ToInt32(lookUpEditMonth.EditValue), 1);
            lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            if (lastDateOfMonth > DateTime.Now) lastDateOfMonth = DateTime.Now;
            var tasks = Task.WhenAll(
               BindAcccuracyAsync(),
               UpdateTempTableAsync(),
               BindErrorsAsync()
            );
        }
        private void gridViewAccuracyDetails_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewAccuracy_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewAccuracyDetails_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                int orderId = Convert.ToInt32(gridViewAccuracyDetails.GetRowCellValue(e.RowHandle, "Order_ID"));
                Order_Entry order = new Order_Entry(orderId, userId, userRoleId.ToString(), "");
                Invoke(new MethodInvoker(delegate { order.Show(); }));
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (gridViewAccuracy.RowCount > 0)
            {
                ExportSummary();
            }
            else
            {
                XtraMessageBox.Show("Data not found");
            }
        }
        private void ExportSummary()
        {
            try
            {
                gridViewAccuracyDetails.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = SummaryItemType.None;
                gridViewErrors.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = SummaryItemType.None;
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                PrintingSystem ps = new PrintingSystem();
                CompositeLink compositeLink = new CompositeLink(ps);
                PrintableComponentLink linkAccuracy = new PrintableComponentLink();
                linkAccuracy.Component = gridControlAccuracy;
                PrintableComponentLink linkChart = new PrintableComponentLink();
                linkChart.Component = chartControlAccuracy;
                PrintableComponentLink linkAccuracyDetails = new PrintableComponentLink();
                linkAccuracyDetails.Component = gridControlAccuracyDetails;
                PrintableComponentLink linkErrors = new PrintableComponentLink();
                linkErrors.Component = gridControlErrors;
                ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);
                ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt,
                PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink.Links.AddRange(new object[]
                {
                     linkAccuracy,linkChart,linkAccuracyDetails,linkErrors
                });
                string ReportName = "Accuracy Summary";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                compositeLink.CreatePageForEachLink();
                ps.XlSheetCreated += PrintingSystem_XlSheetCreated;
                compositeLink.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                gridViewAccuracyDetails.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = SummaryItemType.Count;
                gridViewErrors.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = SummaryItemType.Count;
                SplashScreenManager.CloseForm(false);
            }
        }
        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Accuracy Summary";
            }
            else if (e.Index == 1)
            {
                e.SheetName = "Summary Chart";
            }
            else if (e.Index == 2)
            {
                e.SheetName = "Accuracy Details";
            }
            else if (e.Index == 3)
            {
                e.SheetName = "Errors";
            }
        }
        private void gridViewErrors_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void Accuracy_Summary_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                WindowState = FormWindowState.Maximized;
                checkEditCustomize.Checked = false;
                dbc.BindMonth(lookUpEditMonth);
                dbc.BindYear(lookUpEditYear);
                lookUpEditMonth.EditValue = DateTime.Now.Month;
                lookUpEditYear.EditValue = DateTime.Now.Year;
                //firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //lastDateOfMonth = DateTime.Now;               
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void gridViewErrors_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                int orderId = Convert.ToInt32(gridViewErrors.GetRowCellValue(e.RowHandle, "Order_Id"));
                Order_Entry order = new Order_Entry(orderId, userId, userRoleId.ToString(), "");
                Invoke(new MethodInvoker(delegate { order.Show(); }));
            }
        }
        private async Task BindAcccuracyAsync()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                gridControlAccuracy.DataSource = null;
                chartControlAccuracy.DataSource = null;
                var dictAccuracy = new Dictionary<string, object>()
                {
                { "@Trans","CALCULATE_MONTHLY_ACCURACY_USER_WISE" },
                { "@From_date",firstDateOfMonth },
                { "@To_Date",lastDateOfMonth },
                { "@Error_On_User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictAccuracy), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Accuracy/AccuracySummary", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            var listAccuracy = JsonConvert.DeserializeObject<List<Models.Employee.AccuracySummary>>(dtResult);
                            if (listAccuracy != null && listAccuracy.Count > 0)
                            {
                                gridControlAccuracy.DataSource = listAccuracy;
                                chartControlAccuracy.DataSource = listAccuracy;
                                chartControlAccuracy.Series[0].ArgumentDataMember = "Date";
                                chartControlAccuracy.Series[0].ValueDataMembers.AddRange("Accuracy");
                                chartControlAccuracy.Series[0].ArgumentScaleType = ScaleType.Auto;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private async Task BindAcccuracyDetailsAsync()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                gridControlAccuracyDetails.DataSource = null;
                var dictOrderDetails = new Dictionary<string, object>();
                dictOrderDetails.Add("@Trans", "GET_COMPLETED_FROMDATE_AND_TODATE_WISE");
                dictOrderDetails.Add("@From_Date", firstDateOfMonth);
                dictOrderDetails.Add("@To_Date", lastDateOfMonth);
                var data = new StringContent(JsonConvert.SerializeObject(dictOrderDetails), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Accuracy/CompletedOrders", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            var listCompletedOrders = JsonConvert.DeserializeObject<List<Order_Details>>(dtResult);
                            if (listCompletedOrders != null && listCompletedOrders.Count > 0)
                            {
                                gridControlAccuracyDetails.DataSource = listCompletedOrders;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private async Task BindErrorsAsync()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                gridControlErrors.DataSource = null;
                var dictErros = new Dictionary<string, object>();
                dictErros.Add("@Trans", "GET_USER_WISE_ERROR_DETAILS_BY_FROMDATE_TODATE_WISE");
                dictErros.Add("@From_Date", firstDateOfMonth);
                dictErros.Add("@To_Date", lastDateOfMonth);
                dictErros.Add("@Error_On_User_Id", userId);
                var data = new StringContent(JsonConvert.SerializeObject(dictErros), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Accuracy/Errors", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            var listErrors = JsonConvert.DeserializeObject<List<Models.Employee.Errors>>(dtResult);
                            if (listErrors != null && listErrors.Count > 0)
                            {
                                gridControlErrors.DataSource = listErrors;
                                gridViewErrors.BestFitColumns();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
    }
}
