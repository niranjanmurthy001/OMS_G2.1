using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class EmailSetting : XtraForm
    {
        DataAccess dataccess = new DataAccess();
        Hashtable ht = new Hashtable();
        private int emailId;
        int Email_Type;

        public EmailSetting()
        {
            InitializeComponent();
        }

        private void EmailSetting_Load(object sender, EventArgs e)
        {
            grid_Email_Address_list();
            txt_IS.Enabled = true;
            txt_OS.Enabled = true;
            this.ActiveControl = txt_name;

        }
        private async void grid_Email_Address_list()
        {

            try
            {
                var dictionary = new Dictionary<string, object>()
            {
                    { "@Trans", "Select"}
            };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/EmailSettings/Select", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt.Rows.Count > 0)
                            {
                                gridControl1_Email_Address.DataSource = dt;
                                gridControl1_Email_Address.ForceInitialize();
                            }
                            else
                            {
                                gridControl1_Email_Address.DataSource = null;
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

        private bool Validation()
        {
            if (txt_name.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter Your_Name");
                txt_yourname.Focus();
                return false;
            }
            else if (txt_Email_address.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter Email_Address");
                txt_Email_address.Focus();
                return false;
            }
            else if (txt_Incoming_server.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter Incoming Server details");
                txt_Incoming_server.Focus();
                return false;
            }
            else if (txt_Outgoing_server.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter Outgoing server details");
                txt_Outgoing_server.Focus();
                return false;
            }
            else if (txt_User_Name.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter User Name");
                txt_User_Name.Focus();
                return false;
            }
            else if (txt_password.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter Password");
                txt_password.Focus();
                return false;
            }
            //else if (txt_IS.Text == "")
            //{
            //    MessageBox.Show("please enter the Incoming Port");
            //    txt_IS.Focus();
            //    return false;

            //}
            else if (((txt_IS.Text != "" && (Convert.ToInt32(txt_IS.Text.Length) > 4)) || txt_IS.Text == ""))
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("incoming port must be less than 4....And please enter Incoming port");
                txt_IS.Focus();
                return false;
            }
            //else if (txt_OS.Text == "")
            //{
            //    MessageBox.Show("Please enter the Outgoing Server Port");
            //    txt_OS.Focus();
            //    return false;
            //}
            else if (((txt_OS.Text != "" && (Convert.ToInt32(txt_OS.Text.Length) > 4) || txt_OS.Text == "")))
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Outgoing port must be less than 4.....And please enter Outgoing port");
                txt_OS.Focus();
                return false;
            }
            Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (myRegularExpression.IsMatch(txt_Email_address.Text))
            {

            }
            else
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Email Address Not Valid");
                txt_Email_address.Focus();
                return false;
            }

            return true;
        }
        public async Task<bool> Usercheck()
        {
            DataTable dt = new DataTable();
            try
            {

                if (txt_Email_address.Text != "")
                {
                    var dictionary = new Dictionary<string, object>()
                {
                    { "@Trans", "EmailCheck" },
                    { "@Email_Address", txt_Email_address.Text.Trim()}
                };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/EmailSettings/Check", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(result);
                                int count = Convert.ToInt32(dt1.Rows[0]["count"].ToString());
                                if (count > 0)
                                {
                                    SplashScreenManager.CloseForm(false);
                                    XtraMessageBox.Show("E-mail Already Exists");
                                    return false;
                                }

                            }


                        }

                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async void btn_save_Click(object sender, EventArgs e)
        {
            try
            {

                if (Validation() != false)
                {
                    if (emailId == 0 && btn_save.Text == "Save" && (await Usercheck()) != false)
                    {
                        SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                        txt_IS.Enabled = true;
                        txt_OS.Enabled = true;
                        var dictionary = new Dictionary<string, object>();
                        {
                            dictionary.Add("@Trans", "INSERT");
                            dictionary.Add("@Your_Name", txt_name.Text);
                            dictionary.Add("@Email_Address", txt_Email_address.Text);
                            dictionary.Add("@Incoming_Mail_Server", txt_Incoming_server.Text);
                            dictionary.Add("@Outgoing_Mail_Server", txt_Outgoing_server.Text);
                            dictionary.Add("@Incoming_Server_Port", txt_IS.Text);
                            dictionary.Add("@Outgoing_Server_Port", txt_OS.Text);
                            dictionary.Add("@User_Name", txt_User_Name.Text);
                            dictionary.Add("@Password", txt_password.Text);
                            dictionary.Add("@Connection_SSL", check_connection_SSL.Checked);
                        };
                        var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.PostAsync(Base_Url.Url + "/EmailSettings/Insert", data);
                            if (response.IsSuccessStatusCode)
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var result = await response.Content.ReadAsStringAsync();
                                    SplashScreenManager.CloseForm(false);
                                    XtraMessageBox.Show(txt_User_Name.Text + " Created Successfully ");
                                    grid_Email_Address_list();
                                    Clear();
                                }
                            }
                        }


                    }
                    if (btn_save.Text == "Edit")
                    {
                        var dictionary1 = new Dictionary<string, object>();
                        {
                            dictionary1.Add("@Trans", "UPDATE");
                            dictionary1.Add("@ID", emailId);
                            dictionary1.Add("@Your_Name", txt_name.Text);
                            dictionary1.Add("@Email_Address", txt_Email_address.Text);
                            dictionary1.Add("@Incoming_Mail_Server", txt_Incoming_server.Text);
                            dictionary1.Add("@Outgoing_Mail_Server", txt_Outgoing_server.Text);
                            dictionary1.Add("@Connection_SSL", check_connection_SSL.Checked);
                            dictionary1.Add("@Incoming_Server_Port", txt_IS.Text);
                            dictionary1.Add("@Outgoing_Server_Port", txt_OS.Text);
                            dictionary1.Add("@User_Name", txt_User_Name.Text);
                            dictionary1.Add("@Password", txt_password.Text);
                        };

                        var data = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.PutAsync(Base_Url.Url + "/EmailSettings/Update", data);
                            if (response.IsSuccessStatusCode)
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var result = await response.Content.ReadAsStringAsync();
                                    SplashScreenManager.CloseForm(false);
                                    XtraMessageBox.Show(txt_User_Name.Text + " Updated Successfully ");
                                    grid_Email_Address_list();
                                    Clear();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }


        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "Email_Address")
                {
                    btn_save.Text = "Edit";
                    DataRow row = gridView1.GetDataRow(e.RowHandle);
                    emailId = Convert.ToInt32(row["ID"]);
                    txt_name.Text = row["Your_Name"].ToString();
                    Txt_Name();
                    txt_Email_address.Text = row["Email_Address"].ToString();
                    Txt_EmailAddress();
                    txt_Incoming_server.Text = row["Incoming_Mail_Server"].ToString();
                    Txt_incoming_server();
                    txt_Outgoing_server.Text = row["Outgoing_Mail_Server"].ToString();
                    Txt_Outgoingserver();
                    bool con = Convert.ToBoolean(Convert.ToInt32(row["Connection_SSL"]));
                    check_connection_SSL.Checked = con;
                    txt_User_Name.Text = row["User_Name"].ToString();
                    TXt_username();
                    txt_password.Text = row["Password"].ToString();
                    Txt_Password();
                    txt_IS.Text = row["Incoming_Server_Port"].ToString();
                    Txt_Incomingserverport();
                    txt_OS.Text = row["Outgoing_Server_Port"].ToString();
                    Txt_Outgoingport();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }
        public void Clear()
        {
            txt_name.Text = "";
            txt_Email_address.Text = "";
            txt_Incoming_server.Text = "";
            txt_Outgoing_server.Text = "";
            txt_User_Name.Text = "";
            txt_password.Text = "";
            txt_IS.Text = "";
            txt_OS.Text = "";
            check_ShowPassword.Checked = false;
            check_connection_SSL.Checked = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            btn_save.Text = "Submit";
            Clear();
        }

        private void check_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (check_ShowPassword.Checked)
            {
                txt_password.Properties.PasswordChar = default(char);
            }
            else
            {
                txt_password.Properties.PasswordChar = '*';
            }
        }

        private async void simpleButton2_Click(object sender, EventArgs e)
        {

            string Email_Address = txt_Email_address.Text;
            try
            {
                if (txt_Email_address.Text != "")
                {
                    var dictionary = new Dictionary<string, object>()
                {
                    { "@Trans", "DELETE" },
                  { "@Email_Address", Email_Address }
                };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/EmailSettings/Delete", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                                gridControl1_Email_Address.DataSource = dt;
                                int count = dt.Rows.Count;
                                grid_Email_Address_list();
                                SplashScreenManager.CloseForm(false);
                                XtraMessageBox.Show("Record Deleted Successfully");
                                Clear();
                            }
                        }
                    }
                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Please Enter the Email Address");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn_testconnection_Click(object sender, EventArgs e)
        {

            if (Validation() != false)
            {
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    testEmailadrdress();

                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
        }
        private void testEmailadrdress()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    SendHtmlFormattedEmail();

                }
                catch (Exception error)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show(error.Message.ToString());
                    return;

                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }

        }

        private void SendHtmlFormattedEmail()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(txt_Email_address.Text.ToString());

                    if (txt_Email_address.Text != "")
                    {

                        mailMessage.To.Add(txt_Email_address.Text);

                        SplashScreenManager.CloseForm(false);
                        string Subject = " " + txt_User_Name.Text + " " + "Test Email - OMS";
                        mailMessage.Subject = Subject.ToString();

                        StringBuilder sb = new StringBuilder();
                        sb.Append("Subject: " + Subject.ToString() + "" + Environment.NewLine);

                        SmtpClient smtp = new SmtpClient();

                        smtp.Host = txt_Outgoing_server.Text;

                        //NetworkCredential NetworkCred = new NetworkCredential("netco@drnds.com", "P2DGo5fi-c");
                        NetworkCredential NetworkCred = new NetworkCredential(txt_Email_address.Text, txt_password.Text);
                        smtp.UseDefaultCredentials = true;
                        // smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
                        smtp.Timeout = (60 * 5 * 1000);
                        smtp.Credentials = NetworkCred;
                        //smtp.EnableSsl = true;
                        smtp.Port = Convert.ToInt32(txt_OS.Text);
                        smtp.Send(mailMessage);
                        smtp.Dispose();
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Email Account Tested Succesfully");
                    }
                    else
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Email is Not Added Kindly Check It");
                    }
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(e.Message.ToString());
                return;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void txt_name_Properties_Leave(object sender, EventArgs e)
        {
            //if (txt_name.Text == "")
            //{
            //    XtraMessageBox.Show("Enter Your_Name");
            //    txt_yourname.Focus();

            //}
        }
        private void txt_Email_address_Properties_Leave(object sender, EventArgs e)
        {
            Regex RegExForEmail = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");

            if (txt_Email_address.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                Txt_EmailAddress();
                XtraMessageBox.Show("Enter Email_Address");
                txt_Email_address.Focus();
                return;
            }
            else if (RegExForEmail.IsMatch(txt_Email_address.Text))
            {

            }
            else
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Email Address Not Valid");
                txt_Email_address.Focus();
            }
        }

        private void txt_Incoming_server_Properties_Leave(object sender, EventArgs e)
        {
            //Regex RegExForIncomingServer = new Regex("^[0-9]{3}$");
            //if (txt_Incoming_server.Text == "")
            //{
            //    XtraMessageBox.Show("Enter Incoming Server details");
            //    txt_Incoming_server.Focus();
            //    return;
            //}
            //else if (RegExForIncomingServer.IsMatch(txt_Incoming_server.Text))
            //{

            //}
            //else
            //{
            //    XtraMessageBox.Show("Incoming Server Details Is Having less than 3....And please enter a Valid 3 Digit Incoming port No" );
            //    txt_Incoming_server.Focus();

            //}

        }

        private void txt_Outgoing_server_Properties_Leave(object sender, EventArgs e)
        {
            //Regex RegExForOutgoingServer = new Regex("^[0-9]{3}$");
            //if (txt_Outgoing_server.Text == "")
            //{
            //    XtraMessageBox.Show("Enter OutGoing Server details");
            //    txt_Outgoing_server.Focus();
            //    return;
            //}
            //else if (RegExForOutgoingServer.IsMatch(txt_Outgoing_server.Text))
            //{

            //}
            //else
            //{
            //    XtraMessageBox.Show("Outgoing Server Details Is Having less than 3.....And please enter a Valid 3 Digit Outgoing port No");
            //    txt_Outgoing_server.Focus();
            //}
        }

        private void txt_IS_Properties_Leave(object sender, EventArgs e)
        {
            //Regex RegExForIsPort = new Regex("^[0-9]$");
            if (txt_IS.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                Txt_Incomingserverport();
                XtraMessageBox.Show("Enter Incoming Server Port details");
                txt_IS.Focus();
                return;
            }
            //else if (RegExForIsPort.IsMatch(txt_IS.Text))
            //{

            //}
            //else
            //{
            //    XtraMessageBox.Show("Incoming Server Port Is Having less than 3....And please enter a Valid Incoming port No");
            //    txt_IS.Focus();
            //}
        }
        private void txt_OS_Properties_Leave(object sender, EventArgs e)
        {
            //Regex RegExForOsPort= new Regex("^[0-9]$");
            if (txt_OS.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                Txt_Outgoingport();
                XtraMessageBox.Show("Enter OutGoing Server Port details");
                txt_OS.Focus();
                return;
            }
            //else if (RegExForOsPort.IsMatch(txt_OS.Text))
            //{

            ////}
            //else
            //{
            //    XtraMessageBox.Show("Outgoing port Is Having less than 3.....And please enter a Valid Outgoing port No");
            //    txt_OS.Focus();
            //}
        }

        private void txt_password_Properties_Leave(object sender, EventArgs e)
        {
            ////Regex RegExForPassword = new Regex("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[!*@#$%^&+=]).*$");
            if (txt_password.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter Password");
                txt_password.Focus();
            }
            //if (RegExForPassword.IsMatch(txt_password.Text))
            //{

            //}
            //else
            //{
            //    XtraMessageBox.Show("Password Should Be Having  Alpha-numeric & OneSpecialCharacter.");
            //    txt_password.Focus();
            //}
        }

        private void txt_User_Name_Leave(object sender, EventArgs e)
        {
            if (txt_User_Name.Text == "")
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Enter UserName");
                txt_User_Name.Focus();

            }
        }

        private void txt_User_Name_TextChanged(object sender, EventArgs e)
        {
            txt_User_Name.Text = txt_Email_address.Text.ToString();
        }

        private void txt_Email_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txt_Incoming_server_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txt_Outgoing_server_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txt_IS_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txt_OS_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txt_Email_address_Leave(object sender, EventArgs e)
        {
            txt_User_Name.Text = txt_Email_address.Text;
        }

        private void txt_User_Name_Enter(object sender, EventArgs e)
        {
            txt_User_Name.Text = txt_Email_address.Text;
        }

        private void txt_Email_address_TextChanged(object sender, EventArgs e)
        {
            txt_Email_address.Text = txt_Email_address.Text.Replace(" ", string.Empty);
        }

        private void txt_Incoming_server_TextChanged(object sender, EventArgs e)
        {
            txt_Incoming_server.Text = txt_Incoming_server.Text.Replace(" ", string.Empty);
        }

        private void txt_Outgoing_server_TextChanged(object sender, EventArgs e)
        {
            txt_Outgoing_server.Text = txt_Outgoing_server.Text.Replace(" ", string.Empty);
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            txt_password.Text = txt_password.Text.Replace(" ", string.Empty);
        }

        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        public bool Txt_Name()
        {
            bool bStatus = true;
            if (txt_name.Text == "")
            {
                dxErrorProvider1.SetError(txt_name, "Please Enter Name");
                bStatus = false;
            }
            else
                dxErrorProvider1.SetError(txt_name, "");
            return bStatus;
        }
        private void txt_name_Validating(object sender, CancelEventArgs e)
        {
            Txt_Name();
        }

        public bool Txt_EmailAddress()
        {
            bool bStatus = true;
            Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (myRegularExpression.IsMatch(txt_Email_address.Text))
            {
                dxErrorProvider1.SetError(txt_Email_address, "");
            }
            else
            {
                dxErrorProvider1.SetError(txt_Email_address, "Please Enter Valid Email-Id");
                bStatus = false;
            }
            return bStatus;
        }
        private void txt_Email_address_Validating(object sender, CancelEventArgs e)
        {
            Txt_EmailAddress();
        }

        public bool Txt_incoming_server()
        {
            bool bStatus = true;
            if (txt_Incoming_server.Text == "")
            {
                dxErrorProvider1.SetError(txt_Incoming_server, "Please Enter incoming mail server");
                bStatus = false;
            }
            else
                dxErrorProvider1.SetError(txt_Incoming_server, "");
            return bStatus;
        }
        private void txt_Incoming_server_Validating(object sender, CancelEventArgs e)
        {
            Txt_incoming_server();
        }

        public bool Txt_Outgoingserver()
        {
            bool bStatus = true;
            if (txt_Outgoing_server.Text == "")
            {
                dxErrorProvider1.SetError(txt_Outgoing_server, "Please Enter Outgoing mail server");
                bStatus = false;
            }
            else
                dxErrorProvider1.SetError(txt_Outgoing_server, "");
            return bStatus;
        }
        private void txt_Outgoing_server_Validating(object sender, CancelEventArgs e)
        {
            Txt_Outgoingserver();
        }

        public bool Txt_Incomingserverport()
        {
            bool bStatus = true;
            if (txt_IS.Text == "")
            {
                dxErrorProvider1.SetError(txt_IS, "Please enter the incoming server port number");
                bStatus = false;
            }
            else
                dxErrorProvider1.SetError(txt_IS, "");
            return bStatus;
        }
        private void txt_IS_Validating(object sender, CancelEventArgs e)
        {
            Txt_Incomingserverport();
        }

        public bool Txt_Outgoingport()
        {
            bool bStatus = true;
            if (txt_OS.Text == "")
            {
                dxErrorProvider1.SetError(txt_OS, "Please nter outgoing server port number");
                bStatus = false;
            }
            else
                dxErrorProvider1.SetError(txt_OS, "");
            return bStatus;
        }
        private void txt_OS_Validating(object sender, CancelEventArgs e)
        {
            Txt_Outgoingport();
        }

        public bool TXt_username()
        {
            bool bstatus = true;
            if (txt_User_Name.Text == "")
            {
                dxErrorProvider1.SetError(txt_User_Name, "Please enter the username");
                bstatus = false;
            }
            else
                dxErrorProvider1.SetError(txt_User_Name, "");
            return bstatus;
        }
        private void txt_User_Name_Validating(object sender, CancelEventArgs e)
        {
            TXt_username();
        }

        public bool Txt_Password()
        {
            bool bstatus = true;
            if (txt_password.Text == "")
            {
                dxErrorProvider1.SetError(txt_password, "Please enter the password");
                bstatus = false;
            }
            else
                dxErrorProvider1.SetError(txt_password, "");
            return bstatus;
        }
        private void txt_password_Validating(object sender, CancelEventArgs e)
        {
            Txt_Password();
        }
    }
}
