using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class Process_Settings : XtraForm
    {
        int Client;
        int Project_Type;
        int Department_Type;
        int Selected_Sub_Client;
        int _subclient;
        int _Projecttype;
        int _departmenttype;
        public Process_Settings()
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
                                dr[1] = "Select Client";
                                dt.Rows.InsertAt(dr, 0);
                            }
                            ddl_Client_Names.Properties.DataSource = dt;
                            ddl_Client_Names.Properties.DisplayMember = "Client_Name";
                            ddl_Client_Names.Properties.ValueMember = "Client_Id";
                            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
                            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Client_Name");
                            ddl_Client_Names.Properties.Columns.Add(col);
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
        private async Task Bind_Sub_Clients1(int Client_Id)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>
                {
                    {"@Trans", "SELECT_SUB_CLIENTS" },
                    {"@Client_Id",Client_Id }
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
                                checkedListBox_Subclients.DataSource = dt;
                                checkedListBox_Subclients.DisplayMember = "Sub_ProcessName";
                                checkedListBox_Subclients.ValueMember = "Subprocess_Id";
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
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
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
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
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
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Master/BindDeptType", data);
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
            Client = Convert.ToInt32(ddl_Client_Names.EditValue);
            if (btn_Submit.Text == "Submit" && validate() != false)
            {
                try
                {
                    DataRowView r1 = checkedListBox_ProjectType.GetItem(checkedListBox_ProjectType.SelectedIndex) as DataRowView;
                    Project_Type = Convert.ToInt32(r1["Project_Type_Id"]);
                    DataRowView r2 = checkedListBox_DeptType.GetItem(checkedListBox_DeptType.SelectedIndex) as DataRowView;
                    Department_Type = Convert.ToInt32(r2["Order_Department_Id"]);
                    DataTable dtmulti = new DataTable();
                    dtmulti.Columns.AddRange(new DataColumn[4]
                    {
                        new DataColumn("Client",typeof(int)),
                        new DataColumn("Sub_Client",typeof(int)),
                        new DataColumn("Project_Type",typeof(int)),
                        new DataColumn("Department_Type",typeof(int))
                    });
                    foreach (object itemChecked in checkedListBox_Subclients.CheckedItems)
                    {
                        DataRowView castedItem = itemChecked as DataRowView;
                        string sub = castedItem["Sub_ProcessName"].ToString();
                        int subclient = Convert.ToInt32(castedItem["Subprocess_Id"]);
                        int projecttype = Project_Type;
                        int departmenttype = Department_Type;
                        dtmulti.Rows.Add(Client, subclient, projecttype, departmenttype);
                    }
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    var data = new StringContent(JsonConvert.SerializeObject(dtmulti), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/Process_settings/Insert", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                SplashScreenManager.CloseForm(false);
                                XtraMessageBox.Show("Client is Submitted");
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
            else if (btn_Submit.Text == "Update" && validate() != false)
            {
                try
                {
                    DataRowView r1 = checkedListBox_ProjectType.GetItem(checkedListBox_ProjectType.SelectedIndex) as DataRowView;
                    int _Project_Type = Convert.ToInt32(r1["Project_Type_Id"]);
                    DataRowView r2 = checkedListBox_DeptType.GetItem(checkedListBox_DeptType.SelectedIndex) as DataRowView;
                    int _Department_Type = Convert.ToInt32(r2["Order_Department_Id"]);
                    DataRowView r3 = checkedListBox_Subclients.GetItem(checkedListBox_Subclients.SelectedIndex) as DataRowView;
                    _subclient = Convert.ToInt32(r3["Subprocess_Id"]);
                    var dictionary = new Dictionary<string, object>
                    {
                        {"@Trans","Update" },
                        {"@Client_Id",Client},
                        {"@Sub_Client", _subclient},
                        {"@Project_Type" ,_Project_Type},
                        {"@Department_Type",_Department_Type }
                    };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/Process_settings/Update", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                SplashScreenManager.CloseForm(false);
                                XtraMessageBox.Show("Client Updated Successfully");
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
        }
        private void Clear()
        {
            ddl_Client_Names.ItemIndex = 0;
            checkedListBox_Subclients.UnCheckAll();
            checkedListBox_Subclients.DataSource = null;
            checkedListBox_ProjectType.UnCheckAll();
            checkedListBox_ProjectType.SelectedIndex = 0;
            checkedListBox_DeptType.UnCheckAll();
            checkedListBox_DeptType.SelectedIndex = 0;
            ddl_Client_Names.Enabled = true;
            checkedListBox_Subclients.Enabled = true;
            btn_Submit.Text = "Submit";
        }
        private async void grid_Client_Details()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                    {
                        { "@Trans", "SELECT"}
                    };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Master/BindData", data);
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
                int Client_Id = Convert.ToInt32(ddl_Client_Names.EditValue);
                Bind_Sub_Clients1(Client_Id);
            }
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void checkedListBox_ProjectType_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked && checkedListBox_ProjectType.CheckedItems.Count > 1)
            {
                checkedListBox_ProjectType.ItemCheck -= checkedListBox_ProjectType_ItemCheck;
                checkedListBox_ProjectType.SetItemChecked(checkedListBox_ProjectType.CheckedIndices[0], false);
                checkedListBox_ProjectType.ItemCheck += checkedListBox_ProjectType_ItemCheck;
            }
        }
        private void checkedListBox_DeptType_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (e.State == CheckState.Checked && checkedListBox_DeptType.CheckedItems.Count > 1)
            {
                checkedListBox_DeptType.ItemCheck -= checkedListBox_DeptType_ItemCheck;
                checkedListBox_DeptType.SetItemChecked(checkedListBox_DeptType.CheckedIndices[0], false);
                checkedListBox_DeptType.ItemCheck += checkedListBox_DeptType_ItemCheck;
            }
        }
        private bool validate()
        {
            if (Convert.ToInt32(ddl_Client_Names.EditValue) == 0)
            {
                XtraMessageBox.Show("Select Client");
                return false;
            }
            if (checkedListBox_Subclients.CheckedItems.Count == 0)
            {
                XtraMessageBox.Show("Select Sub-Clients");
                return false;
            }
            if (checkedListBox_ProjectType.CheckedItems.Count == 0)
            {
                XtraMessageBox.Show("Select Project_Type");
                return false;
            }
            if (checkedListBox_DeptType.CheckedItems.Count == 0)
            {
                XtraMessageBox.Show("Select Department Type");
                return false;
            }
            return true;
        }
        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                if (e.Column.FieldName == "Sub_ProcessName")
                {
                    btn_Submit.Text = "Update";
                    ddl_Client_Names.Enabled = false;
                    checkedListBox_Subclients.Enabled = false;
                    GridView view = gridControl_client_details.MainView as GridView;
                    var index = view.GetDataRow(view.GetSelectedRows()[0]);
                    ddl_Client_Names.EditValue = index.ItemArray[4];
                    int _client = Convert.ToInt32(ddl_Client_Names.EditValue);
                    Bind_Sub_Clients1(_client);
                    Selected_Sub_Client = Convert.ToInt32(index.ItemArray[5]);
                    int PT = Convert.ToInt32(index.ItemArray[6]);
                    int DT = Convert.ToInt32(index.ItemArray[7]);
                    checkedListBox_Subclients.SelectedValue = Selected_Sub_Client;
                    checkedListBox_ProjectType.SelectedValue = PT;
                    checkedListBox_DeptType.SelectedValue = DT;
                }
                _subclient = checkedListBox_Subclients.SelectedIndex;
                _Projecttype = checkedListBox_ProjectType.SelectedIndex;
                _departmenttype = checkedListBox_DeptType.SelectedIndex;
                checkedListBox_Subclients.SetItemChecked(_subclient, true);
                checkedListBox_ProjectType.SetItemChecked(_Projecttype, true);
                checkedListBox_DeptType.SetItemChecked(_departmenttype, true);
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
        private bool ValidateList()
        {
            bool bStatus = true;
            Client = Convert.ToInt32(ddl_Client_Names.EditValue);
            if (Client == 0)
            {
                dxErrorProvider1.SetError(ddl_Client_Names, "Please Select Client");
                bStatus = false;
            }
            else
                dxErrorProvider1.SetError(ddl_Client_Names, "");
            return bStatus;
        }
        private void ddl_Client_Names_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateList();
        }
    }
}