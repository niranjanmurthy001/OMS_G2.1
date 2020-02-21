using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ordermanagement_01.Vendors
{
    public partial class Keywords : XtraForm
    {
        private readonly int userId;
        private object id;
        public Keywords(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        private async void Keywords_Load(object sender, EventArgs e)
        {
            try
            {
                await BindKeywords();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Something went wrong");
            }
        }
        private async Task BindKeywords()
        {
            gridControlKeywords.DataSource = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{Base_Url.Url}/Keywords/GetKeywords");
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            DataTable dtKeywords = JsonConvert.DeserializeObject<DataTable>(await response.Content.ReadAsStringAsync());
                            if (dtKeywords != null && dtKeywords.Rows.Count > 0)
                            {
                                gridControlKeywords.DataSource = dtKeywords;
                            }
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void btnAddKeywords_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddKeywords.Text == "Add")
                {
                    if (string.IsNullOrWhiteSpace(textEditKeyword.Text.Trim()))
                    {
                        XtraMessageBox.Show("Enter keyword");
                        textEditKeyword.Focus();
                        return;
                    }
                    var dictionary = new Dictionary<string, object>()
                 {
                    { "@Trans", "INSERT" },
                    { "@Keyword",textEditKeyword.Text.Trim() },
                    { "@InsertedBy",userId },
                    { "@Status",true }
                 };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/Keywords/Add", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                XtraMessageBox.Show("Keyword added successfully");
                                textEditKeyword.Text = string.Empty;
                                await BindKeywords();
                            }
                            else
                            {
                                XtraMessageBox.Show("Unable to add keyword");
                            }
                        }
                    }
                }
                if (btnAddKeywords.Text == "Update")
                {
                    if (string.IsNullOrWhiteSpace(textEditKeyword.Text.Trim()))
                    {
                        XtraMessageBox.Show("Enter keyword");
                        textEditKeyword.Focus();
                        return;
                    }
                    var dictionary = new Dictionary<string, object>()
                 {
                    { "@Trans", "UPDATE" },
                    { "@Keyword",textEditKeyword.Text.Trim() },
                    { "@ModifiedBy",userId },
                    { "@KeywordId",id }
                 };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync(Base_Url.Url + "/Keywords/Update", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                XtraMessageBox.Show("Keyword updated successfully");
                                Clear();
                                await BindKeywords();
                            }
                            else
                            {
                                Clear();
                                XtraMessageBox.Show("Unable to update keyword");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Something went wrong");
            }
        }
        private void Clear()
        {
            textEditKeyword.Text = string.Empty;
            btnAddKeywords.Text = "Add";
            id = 0;
        }
        private void gridViewKeywords_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private async void gridViewKeywords_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                id = gridViewKeywords.GetRowCellValue(e.RowHandle, "KeywordId");
                textEditKeyword.Text = gridViewKeywords.GetRowCellValue(e.RowHandle, "Keyword").ToString();
                btnAddKeywords.Text = "Update";
            }
            if (e.Column.FieldName == "Delete")
            {
                try
                {
                    id = gridViewKeywords.GetRowCellValue(e.RowHandle, "KeywordId");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.DeleteAsync($"{Base_Url.Url}/Keywords/Delete/{id}");
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                XtraMessageBox.Show("Keyword deleted successfully");
                                await BindKeywords();
                            }
                            else
                            {
                                XtraMessageBox.Show("Unable to delete keyword");
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    XtraMessageBox.Show("Something went wrong");
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}