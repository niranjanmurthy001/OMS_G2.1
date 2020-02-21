using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.Reports
{
    public partial class BreakIdleReportsNew : DevExpress.XtraEditors.XtraForm
    {
        private DateTime fromDate;
        private readonly int userId, roleId;
        public BreakIdleReportsNew(int userId, int roleId)
        {
            InitializeComponent();
            this.userId = userId;
            this.roleId = roleId;
        }
        private void BreakIdleReportsNew_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            this.WindowState = FormWindowState.Maximized;
            fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateEditFromDate.Text = fromDate.ToShortDateString();
            dateEditToDate.Text = DateTime.Now.ToShortDateString();
            tabPaneReports.SelectedPage = tabNavigationPagesummary;
            SplashScreenManager.CloseForm(false);
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEditFromDate.Text))
            {
                XtraMessageBox.Show("select from date");
                dateEditFromDate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dateEditToDate.Text))
            {
                XtraMessageBox.Show("select to date");
                dateEditToDate.Focus();
                return;
            }
            if (Convert.ToDateTime(dateEditToDate.Text) < Convert.ToDateTime(dateEditFromDate.Text))
            {
                XtraMessageBox.Show("To Date Cannot be less than from date");
                return;
            }
            BindSummary();
            BindIdleHours();
            BindBreakHours();
        }
        private async void BindBreakHours()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>();
                {
                    dictionary.Add("@Trans", "BREAK_HOURS");
                    dictionary.Add("@From_Date", dateEditFromDate.Text);
                    dictionary.Add("@To_Date", dateEditToDate.Text);
                    if (roleId != 1 && roleId != 4 && roleId != 6)
                    {
                        dictionary.Add("@User_Id", userId);
                    }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url.ToString() + "/BreakIdleReports/BindBreakIdleReports", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                pivotGridControlBreakHours.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private async void BindIdleHours()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>();
                {
                    dictionary.Add("@Trans", "IDLE_HOURS");
                    dictionary.Add("@From_Date", dateEditFromDate.Text);
                    dictionary.Add("@To_Date", dateEditToDate.Text);
                    if (roleId != 1 && roleId != 4 && roleId != 6)
                    {
                        dictionary.Add("@User_Id", userId);
                    }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url.ToString() + "/BreakIdleReports/BindBreakIdleReports", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                pivotGridControlIdleHours.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private async void BindSummary()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>();
                {
                    dictionary.Add("@Trans", "SUMMARY");
                    dictionary.Add("@From_Date", dateEditFromDate.Text);
                    dictionary.Add("@To_Date", dateEditToDate.Text);
                    if (roleId != 1 && roleId != 4 && roleId != 6)
                    {
                        dictionary.Add("@User_Id", userId);
                    }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url.ToString() + "/BreakIdleReports/BindBreakIdleReports", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                pivotGridControlSummary.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            dateEditFromDate.Text = fromDate.ToShortDateString();
            dateEditToDate.Text = DateTime.Now.ToShortDateString();
            pivotGridControlIdleHours.DataSource = null;
            pivotGridControlBreakHours.DataSource = null;
            pivotGridControlSummary.DataSource = null;
        }
        private void pivotGridControlBreakHours_CustomSummary(object sender, DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs e)
        {
            string field = e.DataField.FieldName;
            IList list = e.CreateDrillDownDataSource();
            if (field == "break_hours")
            {
                List<TimeSpan> listHours = new List<TimeSpan>();
                foreach (PivotDrillDownDataRow row in list)
                {
                    listHours.Add(TimeSpan.Parse(row[field].ToString()));
                }
                var ts = new TimeSpan(listHours.Sum(t => t.Duration().Ticks));
                string total = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                    (int)ts.TotalHours,
                    ts.Minutes,
                    ts.Seconds
                );
                e.CustomValue = total;
            }
        }
        private void pivotGridControlIdleHours_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
        {
            string field = e.DataField.FieldName;
            IList list = e.CreateDrillDownDataSource();
            if (field == "Idle_Hours")
            {
                List<TimeSpan> listHours = new List<TimeSpan>();
                foreach (PivotDrillDownDataRow row in list)
                {
                    object a = row[field];
                    CultureInfo culture = CultureInfo.InvariantCulture;
                    listHours.Add(TimeSpan.Parse(row[field].ToString(), culture));
                }
                var ts = new TimeSpan(listHours.Sum(t => t.Duration().Ticks));
                string total = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                    (int)ts.TotalHours,
                    ts.Minutes,
                    ts.Seconds
                );
                e.CustomValue = total;
            }
        }
        private void pivotGridControlSummary_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
        {
            string field = e.DataField.FieldName;
            IList list = e.CreateDrillDownDataSource();
            if (field == "Production Hours")
            {
                List<TimeSpan> listHours = new List<TimeSpan>();
                foreach (PivotDrillDownDataRow row in list)
                {
                    object a = row[field];
                    listHours.Add(TimeSpan.Parse(row[field].ToString()));
                }
                var ts = new TimeSpan(listHours.Sum(t => t.Duration().Ticks));
                string total = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                    (int)ts.TotalHours,
                    ts.Minutes,
                    ts.Seconds
                );
                e.CustomValue = total;
            }
            if (field == "Break Hours")
            {
                List<TimeSpan> listHours = new List<TimeSpan>();
                foreach (PivotDrillDownDataRow row in list)
                {
                    listHours.Add(TimeSpan.Parse(row[field].ToString()));
                }
                var ts = new TimeSpan(listHours.Sum(t => t.Duration().Ticks));
                string total = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                    (int)ts.TotalHours,
                    ts.Minutes,
                    ts.Seconds
                );
                e.CustomValue = total;
            }
            if (field == "Ideal Hours")
            {
                List<TimeSpan> listHours = new List<TimeSpan>();
                foreach (PivotDrillDownDataRow row in list)
                {
                    object a = row[field];
                    listHours.Add(TimeSpan.Parse(row[field].ToString()));
                }
                var ts = new TimeSpan(listHours.Sum(t => t.Duration().Ticks));
                string total = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                    (int)ts.TotalHours,
                    ts.Minutes,
                    ts.Seconds
                );
                e.CustomValue = total;
            }
            if (field == "Total Hours")
            {
                List<TimeSpan> listHours = new List<TimeSpan>();
                foreach (PivotDrillDownDataRow row in list)
                {
                    object a = row[field];
                    listHours.Add(TimeSpan.Parse(row[field].ToString()));
                }
                var ts = new TimeSpan(listHours.Sum(t => t.Duration().Ticks));
                string total = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                    (int)ts.TotalHours,
                    ts.Minutes,
                    ts.Seconds
                );
                e.CustomValue = total;
            }
        }
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                if (pivotGridControlBreakHours.DataSource == null && pivotGridControlSummary.DataSource == null && pivotGridControlIdleHours.DataSource == null)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Data not found, cannot export");
                    return;
                }
                PivotGridControl[] pivotGrids = new PivotGridControl[] { pivotGridControlSummary, pivotGridControlIdleHours, pivotGridControlBreakHours };
                PrintingSystem ps = new PrintingSystem();
                ps.SetCommandVisibility(new PrintingSystemCommand[] {
                    PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx,
                    PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls, PrintingSystemCommand.Open
                }, CommandVisibility.All);
                ps.XlSheetCreated += PrintingSystem_XlSheetCreated;
                CompositeLink cl = new CompositeLink(ps);

                PrintableComponentLink linkSummary = new PrintableComponentLink();
                linkSummary.Component = pivotGridControlSummary;
                linkSummary.PaperName = "Summary";

                PrintableComponentLink linkIdle = new PrintableComponentLink();
                linkIdle.Component = pivotGridControlIdleHours;
                linkIdle.PaperName = "Idle Hours";

                PrintableComponentLink linkBreak = new PrintableComponentLink();
                linkBreak.Component = pivotGridControlBreakHours;
                linkBreak.PaperName = "Break Hours";

                cl.Links.AddRange(new object[] { linkSummary, linkIdle, linkBreak });
                cl.CreatePageForEachLink();

                string filePath = @"C:\Break Idle Reports\";
                string fileName = filePath + "Break Idle Reports All-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Export(cl, fileName);
                }
                else
                {
                    Export(cl, fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private static void Export(CompositeLink cl, string fileName)
        {
            cl.ExportToXlsx(fileName, new XlsxExportOptions()
            {
                ExportMode = XlsxExportMode.SingleFilePageByPage,
                ExportHyperlinks = false,
                TextExportMode = TextExportMode.Value,
                IgnoreErrors = XlIgnoreErrors.NumberStoredAsText
            });
            System.Diagnostics.Process.Start(fileName);
        }
        private void pivotGridControlSummary_CellClick(object sender, PivotCellEventArgs e)
        {
            try
            {
                int user = Convert.ToInt32(e.CreateDrillDownDataSource().Cast<PivotDrillDownDataRow>().Select(row => row["User_Id"]).Single());
                string pdate = e.CreateDrillDownDataSource().Cast<PivotDrillDownDataRow>().Select(row => row["Date"]).Single().ToString();
                if (e.DataField.FieldName == pivotGridFieldPHours.FieldName)
                {
                    Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(pdate, user, "Production");
                    emb.Show();
                }
                if (e.DataField.FieldName == pivotGridFieldBHours.FieldName)
                {
                    Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(pdate, user, "Break");
                    emb.Show();
                }
                if (e.DataField.FieldName == pivotGridFieldIHours.FieldName)
                {
                    Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(pdate, user, "Ideal");
                    emb.Show();
                }
            }
            catch
            {
                return;
            }
        }
        void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Summary";
            }
            if (e.Index == 1)
            {
                e.SheetName = "Idle Hours";
            }
            if (e.Index == 2)
            {
                e.SheetName = "Break Hours";
            }
        }
    }
}