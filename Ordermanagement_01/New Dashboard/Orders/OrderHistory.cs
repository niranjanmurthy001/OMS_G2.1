using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System.Net.Http;
using System.Net;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace Ordermanagement_01
{
    public partial class OrderHistory : XtraForm
    {
        private readonly int userId, orderId,roleId;
        private readonly string orderNumber, client, subProcess, state, county;        
        private DataAccess dataaccess = new DataAccess();
       
        public OrderHistory(int userid,string roleId, int Orderid, string OrderNo, string Clientname, string Subprocessname, string State, string County)
        {
            InitializeComponent();
            this.userId = userid;
            this.orderId = Orderid;
            this.orderNumber = OrderNo;
            this.client = Clientname;
            this.subProcess = Subprocessname;
            this.state = State;
            this.county = County;
            this.roleId = Convert.ToInt32(roleId);
        }

        private void OrderHistory_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            groupControlOrderHistory.Text = orderNumber + "'s History";
            lbl_Clientname.Text = client;
            lbl_Subprocess.Text = subProcess;
            lbl_State.Text = state;
            lbl_County.Text = county;
            DataTable datatable = new DataTable();
            BindGridHistory();
            BindStatusHistory();
            BindStatusPermissionHistory();


            if (roleId == 2)
            {
                splitContainerControl1.Panel2.Visible = false;
            }
        }
        private async void BindStatusHistory()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                    { "@Trans", "SELECT" },
                    { "@Order_Id", orderId }
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictionary ), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response1 = await httpClient.PostAsync(Base_Url.Url + "/OrderHistory/BindOrderStatus", data);
                    if (response1.IsSuccessStatusCode)
                    {
                        if (response1.StatusCode == HttpStatusCode.OK)
                        {
                            var result1 = await response1.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result1);
                            if (dt.Rows.Count > 0)
                            {
                                gridControlOrderStatusHistory.DataSource = dt;
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
        private async void BindStatusPermissionHistory()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                    { "@Trans", "SELECT" },
                    { "@Order_Id", orderId }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response2 = await httpClient.PostAsync(Base_Url.Url + "/OrderHistory/BindOrderStatusPermission", data);
                    if (response2.IsSuccessStatusCode)
                    {
                        if (response2.StatusCode == HttpStatusCode.OK)
                        {
                            var result1 = await response2.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result1);
                            if (dt.Rows.Count > 0)
                            {
                                gridControlOrderStatusPermissionHistory.DataSource = dt;
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
        private async void BindGridHistory()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);    
               var dictionary = new Dictionary<string, object>()
                {
                    { "@Trans", "SELECT" },
                   { "@Order_Id", orderId }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/OrderHistory/LoadOrderHistory", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result= await response.Content.ReadAsStringAsync();
                            DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt1.Rows.Count > 0)
                            {
                                gridControlOrderHistory.DataSource = dt1;
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
        private void btn_Export_Click(object sender, EventArgs e)
        {
            
            if (gridViewOrderHistory.RowCount> 0)
            {
                ExportOrderHistory();
            }
            else
            {
                XtraMessageBox.Show("Data not found");
            }


        }
        private void ExportOrderHistory()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                PrintingSystem ps = new PrintingSystem();
                CompositeLink compositeLink = new CompositeLink(ps);
                PrintableComponentLink LinkOrderhistory = new PrintableComponentLink();
                LinkOrderhistory.Component = gridControlOrderHistory;
                PrintableComponentLink LinkOrderStatusHistory = new PrintableComponentLink();
                LinkOrderStatusHistory.Component = gridControlOrderStatusHistory;

                ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);
                ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt,
                PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink.Links.AddRange(new object[]
                {
                    LinkOrderhistory,LinkOrderStatusHistory
                });
                string ReportName = "Order History";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                compositeLink.CreatePageForEachLink();
                ps.XlSheetCreated += PrintingSystem_XlSheetCreated;
                compositeLink.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);
            }
            catch(Exception e)
            {
                throw e;
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
                e.SheetName = "Orders History";
            }
            else if (e.Index == 1)
            {
                e.SheetName = "Orders Status History";
            }
        }

        private void gridViewOrderHistory_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewOrderStatusHistory_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}