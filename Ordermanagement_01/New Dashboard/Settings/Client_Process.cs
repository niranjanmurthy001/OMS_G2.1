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
        int Client;
        public Client_Process()
        {
            InitializeComponent();
        }

        private void Client_Process_Load(object sender, EventArgs e)
        {
            Bindclients();
            BindProjectType();
            BindDepartmentType();
            grid_Client_Details();
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
                                    //DataRow dr = dt.NewRow();
                                    //dr[0] = 0;
                                    //dr[1] = "Select Client";
                                    //dt.Rows.InsertAt(dr, 0);
                                    
                                    ddl_Client_Names.Properties.DataSource = dt;
                                    ddl_Client_Names.Properties.DisplayMember = "Client_Name";
                                    ddl_Client_Names.Properties.ValueMember = "Client_Id";
                                    ddl_Client_Names.Properties.Columns.Clear();
                                    DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
                                    col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Client_Name", 100);
                                    ddl_Client_Names.Properties.Columns.Add(col);                                   
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
            Client = int.Parse(ddl_Client_Names.EditValue.ToString());
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans", "SELECT_SUB_CLIENTS" },
                    {"@Client_Id",Client }
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
                                for (int i = 0; i < dt.Rows.Count; i++)
                                { 
                                    checkedListBox_Subclients.DataSource = dt;
                                    checkedListBox_Subclients.DisplayMember = "Sub_ProcessName";
                                    checkedListBox_Subclients.ValueMember = "Order_Sub_Client_Id";
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

        private async void BindProjectType()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans","SELECT_PROJECT_TYPE" }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/ProjectType/BindProjectType", data);
                    if(response.IsSuccessStatusCode)
                    {
                        if(response.StatusCode==HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    //checkedListBox_ProjectType.Items.Add(dt.Rows[i]["Project_Type"].ToString());
                                    checkedListBox_ProjectType.DataSource = dt;
                                    checkedListBox_ProjectType.DisplayMember = "Project_Type";
                                    checkedListBox_ProjectType.ValueMember = "Project_Type_Id";
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
                                    checkedListBox_DeptType.DataSource = dt;
                                    checkedListBox_DeptType.DisplayMember = "Order_Department";
                                    checkedListBox_DeptType.ValueMember = "Order_Department_Id";
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

        private async void btn_Submit_Click(object sender, EventArgs e)
        {

            //var items = checkedListBox_Subclients.CheckedItems;
            //foreach (var item in items)
            //{
            //    string s = item.ToString();
            //}
            int sub_client = int.Parse(checkedListBox_Subclients.SelectedIndex.ToString());
            int Project_Type = int.Parse(checkedListBox_ProjectType.SelectedIndex.ToString());
            int Dept_Type = int.Parse(checkedListBox_DeptType.SelectedIndex.ToString());
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans","INSERT" },
                    {"@Client_Id",Client },
                    {"@Sub_Client",sub_client },
                    {"@Project_Type",Project_Type },
                    {"@Department_Type",Dept_Type }

                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Client_Process/Insert", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show(ddl_Client_Names.EditValue.ToString() +" "+ "is Submitted");
                            grid_Client_Details();
                            Clear();                           
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
        private void Clear()
        {
            ddl_Client_Names.EditValue=null;
            checkedListBox_Subclients.UnCheckAll();
            checkedListBox_ProjectType.UnCheckAll();
            checkedListBox_DeptType.UnCheckAll();
        }
        private async void grid_Client_Details()
        {

            try
            {
                SplashScreenManager.ShowForm(this,typeof(Masters.WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
            {
                    { "@Trans", "SELECT"}
            };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Client_Process/BindData", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt.Rows.Count > 0)
                            {
                                gridControl_client_details.DataSource = dt;
                                gridView1.BestFitColumns();
                            }
                            else
                            {
                                gridControl_client_details.DataSource = null;
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

        private void ddl_Client_Names_EditValueChanged(object sender, EventArgs e)
        {
            if (ddl_Client_Names.ItemIndex > 0)
            {
                BindSubClients();
            }
            else
            {
                BindSubClients();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}