using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;
using System.Net.Http;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System.Net;

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class Client_Process : XtraForm
    {
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataAccess da = new DataAccess();
        DataTable dt = new DataTable();
        public Client_Process()
        {
            InitializeComponent();
        }

        private void Client_Process_Load(object sender, EventArgs e)
        {
            Bindclients();
        }

        private async void Bindclients()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans", "SELECT" }
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
                                DataRow dr = dt.NewRow();
                                dr[0] = 0;
                                dr[3] = "Select Client";
                                dt.Rows.InsertAt(dr, 0);
                                ddl_Client_Names.DisplayMember = "Client_Name";
                                ddl_Client_Names.ValueMember = "Client_Id";
                                ddl_Client_Names.DataSource = dt;
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

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            OrderEntry OR = new OrderEntry();
            OR.Show();
        }
    }
}