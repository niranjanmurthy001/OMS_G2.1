using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.New_Dashboard.Orders
{
    public partial class Order_Instruction : XtraForm
    {
        private bool IsButton { get; set; }
        public readonly int Order_Id, State_Id, County_Id, Order_Type_Id, Order_Type_Abs_Id,
                            Client_Id, Sub_Client_Id, Work_Type_Id, User_Id, Order_Task_Id, Order_Status_Id;
        public readonly string Client_Order_Number, Form_View_Type;
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        const int MF_BYCOMMAND = 0;
        const int MF_DISABLED = 2;
        const int SC_CLOSE = 0xF060;
        public Order_Instruction(Order_Passing_Params obj_Order_Details_List)
        {
            InitializeComponent();
            buttonNext.Select();
            Order_Id = obj_Order_Details_List.Order_Id;
            State_Id = obj_Order_Details_List.State_Id;
            County_Id = obj_Order_Details_List.County_Id;
            Order_Type_Id = obj_Order_Details_List.Order_Type_Id;
            Order_Type_Abs_Id = obj_Order_Details_List.Order_Type_Abs_Id;
            Client_Order_Number = obj_Order_Details_List.Client_Order_Number;
            Client_Id = obj_Order_Details_List.Client_Id;
            Sub_Client_Id = obj_Order_Details_List.Sub_Client_Id;
            Work_Type_Id = obj_Order_Details_List.Work_Type_Id;
            User_Id = obj_Order_Details_List.User_Id;
            Order_Task_Id = obj_Order_Details_List.Order_Task_Id;
            Form_View_Type = obj_Order_Details_List.Form_View_Type;
        }
        private void Order_Instruction_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                if (Form_View_Type == "View")
                {
                    tabPane.Pages.Remove(tabConfirmation);
                    buttonFinish.Enabled = true;
                    buttonFinish.Text = "Close";
                }
                else
                {
                    var close = GetSystemMenu(Handle, false);
                    EnableMenuItem(close, SC_CLOSE, MF_BYCOMMAND | MF_DISABLED);
                }
                IsButton = false;
                buttonPrevious.Visible = false;
                Statue_Of_Limitations();
                Special_Instructions();
                US_Tax();
                Check_All_Client_Sub();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong contact admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            IsButton = true;
            tabPane.SelectedPageIndex += 1;
            buttonPrevious.Enabled = true;
            if (Form_View_Type == "View")
            {
                buttonFinish.Enabled = true;
            }
            else
            {
                buttonFinish.Enabled = false;
            }
        }
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            IsButton = true;
            tabPane.SelectedPageIndex -= 1;
            buttonFinish.Visible = true;
            buttonFinish.Enabled = false;
            checkConfirmation_Properties.Checked = false;
        }
        private async void buttonFinish_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                if (Form_View_Type != "View")
                {
                    var dictionary = new Dictionary<string, object>()
                    {
                                { "@Trans","INSERT"            },
                                { "@Order_Id",Order_Id         },
                                { "@User_Id",User_Id           },
                                { "@Task_Id", Order_Task_Id    },
                                { "@Work_Type_Id",Work_Type_Id },
                                { "@Form_State",true           }
                    };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/OrderInstruction/Create", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
                Close();
            }
        }
        private void tabSpecial_Instructions_VisibleChanged(object sender, EventArgs e)
        {
            buttonPrevious.Enabled = false;
            buttonNext.Enabled = true;
            editingBar1.Visible = true;
            buttonPrevious.Visible = false;
            buttonFinish.Visible = true;
            buttonNext.Visible = true;
            buttonFinish.Enabled = false;
        }
        private void checkConfirmation_Properties_CheckedChanged(object sender, EventArgs e)
        {
            if (checkConfirmation_Properties.Checked == true)
            {
                this.buttonFinish.Enabled = true;
            }
            else
            {
                this.buttonFinish.Enabled = false;
            }
        }
        private void Order_Instruction_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (buttonFinish.Text != "Close")
            {
                Form form = Application.OpenForms["Employee_Order_Entry"];
                if (form != null) { form.Enabled = true; }
            }
        }
        private void tabUs_DueDates_VisibleChanged(object sender, EventArgs e)
        {
            editingBar1.Visible = true;
            buttonFinish.Enabled = false;
            buttonPrevious.Visible = true;
            buttonFinish.Visible = true;
        }
        private void tabAlert_VisibleChanged(object sender, EventArgs e)
        {
            buttonFinish.Enabled = false;
            buttonPrevious.Enabled = true;
            buttonPrevious.Visible = true;
            editingBar1.Visible = true;
            buttonFinish.Visible = true;
        }
        private void tabConfirmation_VisibleChanged(object sender, EventArgs e)
        {
            buttonPrevious.Enabled = true;
            buttonNext.Enabled = true;
            editingBar1.Visible = true;
            buttonFinish.Enabled = false;
            buttonFinish.Visible = true;
        }
        private void tabStatue_Of_Limitation_VisibleChanged(object sender, EventArgs e)
        {
            buttonFinish.Enabled = false;
            buttonPrevious.Visible = true;
            buttonFinish.Visible = true;
            editingBar1.Visible = true;
        }
        private async void US_Tax()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                    {"@Trans","SELECT_TAX"},
                    { "@State_Id",State_Id}
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url.ToString() + "/OrderInstruction/USTAX", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                grid_Us_Tax.DataSource = dt;
                            }
                            else
                            {
                                // XtraMessageBox.Show("Tax Not Found");
                                // this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void tabPane_SelectedPageChanging(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangingEventArgs e)
        {
            if (IsButton) e.Cancel = false;
            if (!IsButton)
            {
                e.Cancel = true;
            }
            IsButton = false;
        }
        private async void Special_Instructions()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                {"@Trans","SELECT_INSTRUCTIONS" },
                {"@Order_ID",Order_Id           }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/OrderInstruction/SpecialInstruction", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                richEditControl1.Text = (dt.Rows[0]["Notes"].ToString());
                            }
                            else
                            {
                                richEditControl1.Text = "";
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private async void Statue_Of_Limitations()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                richEditControl2.Text = "";
                var dictionary = new Dictionary<string, object>()
                    {
                    {"@Trans","SELECT_STATUE" },
                    { "@State_Id",State_Id    },
                    };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/OrderInstruction/Statue", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                richEditControl2.Text = dt.Rows[0]["Statute of limitation"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private async void Check_All_Client_Sub()
        {
            StringBuilder appendtext = new StringBuilder();
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                     {"@Trans","CHECK_ALL_CLIENT_SUB" },
                    {"@Client_Id",Client_Id           }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/OrderAlert/Check-Clients", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                appendtext.AppendLine(dt.Rows[0]["Instructions"].ToString());
                                appendtext.AppendLine("");
                            }

                        }
                    }
                }
                var dictionaryClientInstructions = new Dictionary<string, object>()
                {
                   {"@Trans","CHECK_ALL_ORDER_ST_COUNTY"    },
                    {"@Order_Type_ABS_Id",Order_Type_Abs_Id },
                    {"@State_Id",State_Id                   },
                    {"@County_Id",County_Id                 }
                };
                var dataClientInstructions = new StringContent(JsonConvert.SerializeObject(dictionaryClientInstructions), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/OrderAlert/Check Clients", dataClientInstructions);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                appendtext.AppendLine(dt.Rows[0]["Instructions"].ToString());
                                appendtext.AppendLine("");
                            }
                        }
                    }
                }
                var dictionaryInstructions = new Dictionary<string, object>()
                {
                    {"@Trans","CHECK_ALL_TRUE_ORDER_ST_COUNTY" },
                    {"@Order_Type_ABS_Id",Order_Type_Abs_Id    },
                    {"@State_Id",State_Id                      },
                    {"@County_Id",County_Id                    }
                };
                var dataInstructions = new StringContent(JsonConvert.SerializeObject(dictionaryInstructions), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/OrderAlert/Check Clients", dataInstructions);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                appendtext.AppendLine(dt.Rows[0]["Instructions"].ToString());
                                appendtext.AppendLine("");
                            }
                        }
                    }
                }
                richEditControl3.Text = appendtext.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
    }
}