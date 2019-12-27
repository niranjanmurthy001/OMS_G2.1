using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.Net.Http;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using System.Net;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class General_Notification : DevExpress.XtraEditors.XtraForm
    {
        public General_Notification()
        {
            InitializeComponent();
        }
        private void General_Notification_Load(object sender, EventArgs e)
        {
            notification();
        }
        private async void notification()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                {"@Trans","SELECT_GRID" },
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Notification/OrderNotification", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                grid_notification.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong,please contact admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void advBandedGridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Message")
            {
                string msg = advBandedGridView1.GetRowCellValue(advBandedGridView1.FocusedRowHandle, "Message").ToString();
                Ordermanagement_01.Employee.Genral_Message_View alertmesgview = new Ordermanagement_01.Employee.Genral_Message_View(null, msg);
                alertmesgview.Show();
            }
        }        
    }
}
