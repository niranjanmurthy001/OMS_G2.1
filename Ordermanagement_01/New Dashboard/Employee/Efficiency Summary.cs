using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraCharts;
using DevExpress.XtraBars.Docking2010;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Ordermanagement_01.Models;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class Efficiency_Summary : XtraForm
    {
        private readonly int userId;
        private readonly DropDownistBindClass dbc;
        private readonly DataAccess dataaccess;
        private readonly int userRoleId;
        private readonly DateTime productionDate;        

        public Efficiency_Summary(int userId, int userRoleId, string productionDate)
        {
            this.userId = userId;
            this.userRoleId = userRoleId;           
            this.productionDate = Convert.ToDateTime(productionDate ?? DateTime.Now.ToString("MM/dd/yyyy"));
            dbc = new DropDownistBindClass();
            dataaccess = new DataAccess();
            InitializeComponent();
        }
        private void Efficiency_Summary_Load(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong check with admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void checkEditCustomize_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditCustomize.Checked)
            {
                lookUpEditYear.Enabled = true;
                lookUpEditMonth.Enabled = true;
                btnSubmit.Enabled = true;
            }
            else
            {
                lookUpEditYear.Enabled = false;
                lookUpEditMonth.Enabled = false;
                btnSubmit.Enabled = false;
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lookUpEditMonth.EditValue.ToString() == "0")
            {
                XtraMessageBox.Show("Select month");
                lookUpEditMonth.Focus();
                return;
            }
            if (lookUpEditYear.EditValue == null)
            {
                XtraMessageBox.Show("Select year");
                lookUpEditYear.Focus();
                return;
            }
            DateTime fromDate = new DateTime(Convert.ToInt32(lookUpEditYear.EditValue), Convert.ToInt32(lookUpEditMonth.EditValue), 1);
            DateTime toDate = fromDate.AddMonths(1).AddDays(-1);
            BindEfficiencySummary(fromDate, toDate);
        }
        private void BindEfficiencySummary(DateTime fromDate, DateTime toDate)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Task t = System.Threading.Tasks.Task.WhenAll(BindEfficiencyAsync(fromDate, toDate),
                 BindOrdersAsync(fromDate, toDate));
               // t.Wait(2000);
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
                    BindEfficiencySummary(productionDate, productionDate);
                    break;
                case "Month":
                    BindEfficiencySummary(new DateTime(productionDate.Year, productionDate.Month, 1), productionDate);
                    break;
                case "Year":
                    break;
            }
        }
        private async Task BindEfficiencyAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                chartControlEfficiency.DataSource = null;
                gridControlEfficiency.DataSource = null;
                var dictAccuracy = new Dictionary<string, object>()
                {
                  { "@Trans","USER_EFF_DATE_RANGE" },
                  { "@From_Date",fromDate },
                  { "@ToDate",toDate },
                  { "@User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictAccuracy), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Efficiency/EfficiencySummary", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            DataTable dtEfficiency = JsonConvert.DeserializeObject<DataTable>(dtResult);
                            if (dtEfficiency != null && dtEfficiency.Rows.Count > 0)
                            {
                                gridControlEfficiency.DataSource = dtEfficiency;
                                chartControlEfficiency.DataSource = dtEfficiency;
                                chartControlEfficiency.Series[0].ArgumentDataMember = "Date";
                                chartControlEfficiency.Series[0].ValueDataMembers.AddRange("Effecinecy");
                                chartControlEfficiency.Series[0].ArgumentScaleType = ScaleType.Auto;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task BindOrdersAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                gridControlEfficiencyDetails.DataSource = null;
                var dictCompletedOrders = new Dictionary<string, object>()
                {
                  { "@Trans","GET_COMPLETED_USER_ORDERS_DATE_RANGE" },
                  { "@From_Date",fromDate },
                  { "@To_Date",toDate },
                  { "@User_Id",userId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictCompletedOrders), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Efficiency/CompletedOrders", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var dtResult = await response.Content.ReadAsStringAsync();
                            DataTable dtCompletedOrders = JsonConvert.DeserializeObject<DataTable>(dtResult);
                            if (dtCompletedOrders != null && dtCompletedOrders.Rows.Count > 0)
                            {
                                gridControlEfficiencyDetails.DataSource = dtCompletedOrders;
                                gridViewEfficiencyDetails.BestFitColumns();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void gridViewEfficiencyDetails_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewEfficiency_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewEfficiencyDetails_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                int orderId = Convert.ToInt32(gridViewEfficiencyDetails.GetRowCellValue(e.RowHandle, "Order_ID"));
                Order_Entry order = new Order_Entry(orderId, userId, userRoleId.ToString(), "");
                Invoke(new MethodInvoker(delegate { order.Show(); }));
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (gridViewEfficiency.RowCount > 0)
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
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                PrintingSystem ps = new PrintingSystem();
                CompositeLink compositeLink = new CompositeLink(ps);
                PrintableComponentLink linkEfficiency = new PrintableComponentLink();
                linkEfficiency.Component = gridControlEfficiency;
                PrintableComponentLink linkChart = new PrintableComponentLink();
                linkChart.Component = chartControlEfficiency;
                PrintableComponentLink linkAccuracyDetails = new PrintableComponentLink();
                linkAccuracyDetails.Component = gridControlEfficiencyDetails;

                ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);
                ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt,
                PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink.Links.AddRange(new object[]
                {
                     linkEfficiency,linkChart,linkAccuracyDetails
                });
                string ReportName = "Efficiency Summary";
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
                SplashScreenManager.CloseForm(false);
            }
        }
        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Efficiency Summary";
            }
            else if (e.Index == 1)
            {
                e.SheetName = "Summary Chart";
            }
            else if (e.Index == 2)
            {
                e.SheetName = "Efficiency Details";
            }
        }
    }
}