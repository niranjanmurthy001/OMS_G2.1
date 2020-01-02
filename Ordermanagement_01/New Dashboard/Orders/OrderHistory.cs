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
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System.Net.Http;
using System.Net;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;

namespace Ordermanagement_01
{
    public partial class OrderHistory : DevExpress.XtraEditors.XtraForm
    {
        private readonly int userId, orderId,roleId;
        private readonly string orderNumber, client, subProcess, state, county;        
        private DataAccess dataaccess = new DataAccess();

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

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
            BindGridHistory();
            BindStatusHistory();
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