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
using System.Drawing;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Base;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class General_Notification : DevExpress.XtraEditors.XtraForm
    {
        public readonly int User_Id;
        string status;
        public General_Notification(int User_ID)
        {
            InitializeComponent();
            User_Id = User_ID;
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
                {"@View_Type","Details" },
                { "@User_Id",User_Id}
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

        private void layoutView1_CustomDrawCardFieldValue(object sender, RowCellCustomDrawEventArgs e)
        {
            LayoutView View = sender as LayoutView;
            if (e.RowHandle >= 0)
            {
                status = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Read_Staus"]);
                if (status == "UnRead")
                {                    
                    e.Appearance.ForeColor = Color.Blue;                  
                }
            }
        }
        private async void layoutView1_Click(object sender, EventArgs e)
        {
            string Readstatus = (layoutView1.GetRowCellValue(layoutView1.FocusedRowHandle, "Read_Staus")).ToString();
            int messageid = Convert.ToInt32(layoutView1.GetRowCellValue(layoutView1.FocusedRowHandle, "Message_Id"));
            if (Readstatus == "UnRead")
            {
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    var dictionary = new Dictionary<string, object>()
                    {
                    {"@Trans","Insert"},
                    {"@Message_Id",messageid},
                    {"@User_Id",User_Id }
                    };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/Notification/Create", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                layoutView1.Appearance.FieldValue.ForeColor = Color.Black;
                            }
                        }
                    }
                }


                catch (Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    throw ex;
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }          
        }
    }
}
