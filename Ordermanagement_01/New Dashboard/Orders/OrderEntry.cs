using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordermanagement_01.New_Dashboard.Orders
{
    public partial class OrderEntry : XtraForm
    {
        private Dictionary<LayoutControlItem, object> dictionaryLayoutItems;
        private Dictionary<LayoutControlGroup, object> dictionaryLayoutGroups;
        private DataTable dtProcessSettings;
        private System.Threading.Timer timer;
        public OrderEntry()
        {
            InitializeComponent();
            dictionaryLayoutItems = new Dictionary<LayoutControlItem, object>()
            {
                { layoutControlItemPriorDate,1 },
                { layoutControlItemDeedChain,1 },
                { layoutControlItemLoanNo, 2 },
                { layoutControlItemReqType,2 }
            };
            dictionaryLayoutGroups = new Dictionary<LayoutControlGroup, object>()
            {
                { layoutControlGroupOrder,1 },
                { layoutControlGroupOthers,1 },
                { layoutControlGroupAdditional,1 },
                { layoutControlGroupLereta,2 },
                { layoutControlGroupTitle,3 },
                { layoutControlGroupTaxCode,4 }
            };
        }
        private void OrderEntry_Resize(object sender, EventArgs e)
        {
            // layoutControlGroupAdditional.Expanded = WindowState == FormWindowState.Maximized ? true : false;
        }
        private async void OrderEntry_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                DevExpress.UserSkins.BonusSkins.Register();
                WindowState = FormWindowState.Maximized;
                await BindClients();
                await BindProjectType();
                await BindDepartmentType();
                BindStates();
                timer = new System.Threading.Timer(a =>
                {
                    GetProcessSettings();
                }, null, TimeSpan.FromSeconds(0), TimeSpan.FromMinutes(1));

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
        private void BindStates()
        {
            try
            {
                lookUpEditState.Properties.DataSource = null;
                lookUpEditState.Properties.Columns.Clear();
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(Base_Url.Url + "/Master/States").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(response.Content.ReadAsStringAsync().Result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = 0;
                                dr[1] = "SELECT";
                                dt.Rows.InsertAt(dr, 0);
                                lookUpEditState.Properties.DataSource = dt;
                                lookUpEditState.Properties.DisplayMember = "State";
                                lookUpEditState.Properties.ValueMember = "State_ID";
                                lookUpEditState.Properties.Columns.Add(new LookUpColumnInfo("State"));
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
        private void BindCounties(object stateId)
        {
            try
            {
                lookUpEditCounty.Properties.DataSource = null;
                lookUpEditCounty.Properties.Columns.Clear();
                lookUpEditCounty.EditValue = 0;
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync($"{Base_Url.Url}/Master/Counties/{stateId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(response.Content.ReadAsStringAsync().Result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr[1] = 0;
                                dr[0] = "SELECT";
                                dt.Rows.InsertAt(dr, 0);
                                lookUpEditCounty.Properties.DataSource = dt;
                                lookUpEditCounty.Properties.DisplayMember = "County";
                                lookUpEditCounty.Properties.ValueMember = "County_ID";
                                lookUpEditCounty.Properties.Columns.Add(new LookUpColumnInfo("County"));
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
        private void GetProcessSettings()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetAsync(Base_Url.Url + "/Master/ProcessSettings").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(response.Content.ReadAsStringAsync().Result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                dtProcessSettings = dt;
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
        private async Task BindDepartmentType()
        {
            try
            {
                lookUpEditDeptType.Properties.DataSource = null;
                lookUpEditDeptType.Properties.Columns.Clear();
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans","SELECT_DEPARTMENT_TYPE" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Master/BindDeptType", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr[1] = 0;
                                dr[0] = "SELECT";
                                dt.Rows.InsertAt(dr, 0);
                                lookUpEditDeptType.Properties.DataSource = dt;
                                lookUpEditDeptType.Properties.DisplayMember = "Order_Department";
                                lookUpEditDeptType.Properties.ValueMember = "Order_Department_Id";
                                lookUpEditDeptType.Properties.Columns.Add(new LookUpColumnInfo("Order_Department", "Department"));
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
        private async Task BindClients()
        {
            try
            {
                lookUpEditClient.Properties.DataSource = null;
                lookUpEditClient.Properties.Columns.Clear();
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans", "SELECT_CLIENTS" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Master/BindClients", data);
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
                                dr[1] = "SELECT";
                                dt.Rows.InsertAt(dr, 0);
                                lookUpEditClient.Properties.DataSource = dt;
                                lookUpEditClient.Properties.DisplayMember = "Client_Name";
                                lookUpEditClient.Properties.ValueMember = "Client_Id";
                                lookUpEditClient.Properties.Columns.Add(new LookUpColumnInfo("Client_Name", "Client"));
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
        private async Task BindProjectType()
        {
            try
            {
                lookUpEditProjectType.Properties.DataSource = null;
                lookUpEditProjectType.Properties.Columns.Clear();
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans","SELECT_PROJECT_TYPE" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Master/BindProjectType", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr[1] = 0;
                                dr[0] = "SELECT";
                                dt.Rows.InsertAt(dr, 0);
                                lookUpEditProjectType.Properties.DataSource = dt;
                                lookUpEditProjectType.Properties.DisplayMember = "Project_Type";
                                lookUpEditProjectType.Properties.ValueMember = "Project_Type_Id";
                                lookUpEditProjectType.Properties.Columns.Add(new LookUpColumnInfo("Project_Type", "Project Type"));
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
        private class OrderInfo
        {
            public string orderNumber { get; set; }
            public string client { get; set; }
            public string subClient { get; set; }
            public string Date { get; set; }
            public string Task { get; set; }
            public string user { get; set; }
        }
        private void lookUpEditProjectType_EditValueChanged(object sender, EventArgs e)
        {
            int projectType = GetInt(lookUpEditProjectType.EditValue);
            if (projectType > 0)
            {
                if (projectType == 4)
                {
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 2).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                }
                else
                {
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 2).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                }
                if (projectType == 1)
                {
                    dictionaryLayoutItems.Where(kv => GetInt(kv.Value) == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutItems.Where(kv => GetInt(kv.Value) != 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 3).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 4).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                }
                if (projectType == 2 || projectType == 3)
                {
                    dictionaryLayoutItems.Where(kv => GetInt(kv.Value) == 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                    dictionaryLayoutItems.Where(kv => GetInt(kv.Value) != 1).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 3).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Never);
                    dictionaryLayoutGroups.Where(kv => GetInt(kv.Value) == 4).ToList().ForEach(kv => kv.Key.Visibility = LayoutVisibility.Always);
                }
            }
            else
            {
                dictionaryLayoutGroups.Keys.ToList().ForEach(group => group.Visibility = LayoutVisibility.Never);
            }
        }
        private void lookUpEditClient_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditClient.EditValue) > 0)
            {
                BindSubClients(lookUpEditClient.EditValue);
            }
            else
            {
                lookUpEditSubClient.Properties.DataSource = null;
                lookUpEditSubClient.Properties.Columns.Clear();
            }
        }
        private void BindSubClients(object subClientId)
        {
            lookUpEditSubClient.Properties.DataSource = null;
            lookUpEditSubClient.Properties.Columns.Clear();
            lookUpEditSubClient.EditValue = 0;
            var dictionary = new Dictionary<string, object>
                {
                    {"@Trans", "SELECT_SUB_CLIENTS" },
                    {"@Client_Id",subClientId }
                };
            var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(Base_Url.Url + "/Master/BindSubClients", data).Result;

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr[1] = 0;
                            dr[0] = "SELECT";
                            dt.Rows.InsertAt(dr, 0);
                            lookUpEditSubClient.Properties.DataSource = dt;
                            lookUpEditSubClient.Properties.DisplayMember = "Sub_ProcessName";
                            lookUpEditSubClient.Properties.ValueMember = "Subprocess_Id";
                            lookUpEditSubClient.Properties.Columns.Add(new LookUpColumnInfo("Sub_ProcessName", "Sub Client"));

                        }
                    }
                }
            }
        }
        private void lookUpEditSubClient_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditSubClient.EditValue) > 0)
            {
                lookUpEditProjectType.EditValue = lookUpEditProjectType.Properties.GetKeyValueByDisplayText("SELECT");
                lookUpEditDeptType.EditValue = lookUpEditDeptType.Properties.GetKeyValueByDisplayText("SELECT");
                try
                {
                    var settingsRow = dtProcessSettings
                                        .AsEnumerable()
                                        .FirstOrDefault(row => row.Field<long>("Client_Id") == Convert.ToInt64(lookUpEditClient.EditValue)
                                         && row.Field<long>("Subprocess_Id") == Convert.ToInt64(lookUpEditSubClient.EditValue));
                    if (settingsRow == null)
                    {
                        XtraMessageBox.Show("Project Type not set for the selected subclient");
                        return;
                    }
                    lookUpEditProjectType.EditValue = settingsRow["Project_Type_Id"];
                    lookUpEditDeptType.EditValue = settingsRow["Department_Type"];
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Something went wrong");
                }
            }
        }
        private void lookUpEditState_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditState.EditValue) > 0)
            {
                BindCounties(lookUpEditState.EditValue);
            }
        }
        Func<object, int> GetInt = a => Convert.ToInt32(a);
    }
}