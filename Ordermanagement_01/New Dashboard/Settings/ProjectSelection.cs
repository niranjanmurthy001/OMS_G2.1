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
using System.Net.Http;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System.Net;
using Ordermanagement_01.Masters;

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class ProjectSelection : DevExpress.XtraEditors.XtraForm
    {
        public ProjectSelection()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OrderEntry OE = new OrderEntry();
            OE.Show();
        }

        private void ProjectSelection_Load(object sender, EventArgs e)
        {
            BindProjectType();
            BindDepartmentType();

        }
        private async void BindProjectType()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans","SELECT_PROJECT_TYPE" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/ProjectType/BindProjectType", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    checkBox_ProjectType.Items.Add(dt.Rows[i]["Project_Type"].ToString());
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

        private async void BindDepartmentType()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans","SELECT_DEPARTMENT_TYPE" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/DepartmentType/BindDeptType", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    checkBox_DepartmentType.Items.Add(dt.Rows[i]["Order_Department"].ToString());
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
    }
}