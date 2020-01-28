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
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class Client_Process : XtraForm
    {
        int Client;
        string subclient;
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
                                foreach (DataRow rows in dt.Rows)
                                {
                                    //DataRow dr = dt.NewRow();
                                    //rows[0] = 0;
                                    //rows[1] = "select client";
                                    //dt.Rows.InsertAt(rows, 0);

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
            //    subclient = item.ToString();
            //}
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[4]
            //{
            //    new DataColumn("Client",typeof(int)),
            //    new DataColumn("Sub_Client",typeof(int)),
            //    new DataColumn("Project_Type",typeof(int)),
            //    new DataColumn("Department_Type",typeof(int))
            //});
            //int sub_client = int.Parse(checkedListBox_Subclients.GetDisplayItemValue(checkedListBox_Subclients.FindString("Order_Department_Id")).ToString());
            //int subclient = int.Parse(checkedListBox_Subclients.SelectedItem.ToString());
            //int Project_Type = int.Parse(checkedListBox_ProjectType.CheckedItems.ToString());
            //int Dept_Type = int.Parse(checkedListBox_DeptType.CheckedItems.ToString());
            
            try
            {                            
                DataRowView r = checkedListBox_Subclients.GetItem(checkedListBox_Subclients.SelectedIndex) as DataRowView;
                var SubClient = r["Subprocess_Id"];
                DataRowView r1=checkedListBox_ProjectType.GetItem(checkedListBox_ProjectType.SelectedIndex) as DataRowView;
                var Project_Type = r1["Project_Type_Id"];
                DataRowView r2 = checkedListBox_DeptType.GetItem(checkedListBox_DeptType.SelectedIndex) as DataRowView;
                var Dept_Type = r2["Order_Department_Id"];
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans","INSERT" },
                    {"@Client_Id",Client },
                    {"@Sub_Client",SubClient },
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
            try
            {
                ddl_Client_Names.EditValue = 0;
                checkedListBox_Subclients.UnCheckAll();
                checkedListBox_ProjectType.UnCheckAll();
                checkedListBox_DeptType.UnCheckAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
            try
            {
                Clear();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }              
    }
}