using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;
using Newtonsoft.Json;
using System.Net.Http;
using Ordermanagement_01.Models;
using System.Net;

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class OrderEntry : XtraForm
    {
        public OrderEntry()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            
        }

        private void OrderEntry_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            Bindclients();
            BindSubClients();
            //BindState();
            //BindCounty();
            SplashScreenManager.CloseForm(false);
        }
        private async void Bindclients()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans", "SELECT_CLIENTS" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Client/BindClients", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    ddl_ClientNames.Properties.Items.Add(row["Client_Name"]);
                                    ddl_Clientname.Properties.Items.Add(row["Client_Name"]);
                                }
                            }
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
        private async void BindSubClients()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans", "SELECT_SUB_CLIENTS" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/SubClient/BindSubClients", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    ddl_SubClients.Properties.Items.Add(row["Order_Sub_Clients"]);
                                    ddl_Subclient.Properties.Items.Add(row["Order_Sub_Clients"]);
                                }
                            }
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
        //private async void BindState()
        //{
        //    try
        //    {
        //        SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
        //        var dictionary = new Dictionary<string, object>
        //        {
        //            {"@Trans", "SELECT_STATE" }
        //        };
        //        var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
        //        using (var httpClient = new HttpClient())
        //        {
        //            var response = await httpClient.PostAsync(Base_Url.Url + "/SubClient/BindSubClients", data);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                if (response.StatusCode == HttpStatusCode.OK)
        //                {
        //                    var result = await response.Content.ReadAsStringAsync();
        //                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
        //                    if (dt != null && dt.Rows.Count > 0)
        //                    {
        //                        foreach (DataRow row in dt.Rows)
        //                        {
        //                            ddl_State.Properties.Items.Add(row["State"]);
        //                            ddl_State1.Properties.Items.Add(row["State"]);
        //                            ddl_state2.Properties.Items.Add(row["State"]);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SplashScreenManager.CloseForm(false);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        SplashScreenManager.CloseForm(false);
        //    }
        //}

        //private async void BindCounty()
        //{
        //    try
        //    {
        //        SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
        //        var dictionary = new Dictionary<string, object>
        //        {
        //            {"@Trans", "SELECT_COUNTY" }
        //        };
        //        var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
        //        using (var httpClient = new HttpClient())
        //        {
        //            var response = await httpClient.PostAsync(Base_Url.Url + "/SubClient/BindSubClients", data);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                if (response.StatusCode == HttpStatusCode.OK)
        //                {
        //                    var result = await response.Content.ReadAsStringAsync();
        //                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
        //                    if (dt != null && dt.Rows.Count > 0)
        //                    {
        //                        foreach (DataRow row in dt.Rows)
        //                        {
        //                            ddl_County.Properties.Items.Add(row["County"]);
        //                            ddl_County1.Properties.Items.Add(row["County"]);
        //                            ddl_County2.Properties.Items.Add(row["County"]);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SplashScreenManager.CloseForm(false);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        SplashScreenManager.CloseForm(false);
        //    }
        //}
    }
}