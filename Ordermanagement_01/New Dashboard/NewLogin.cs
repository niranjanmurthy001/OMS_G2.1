using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using static Ordermanagement_01.New_Dashboard.New_Dashboard;

namespace Ordermanagement_01.New_Dashboard
{
    public partial class NewLogin : XtraForm
    {



        int _Application_Login_Type, _User_Id, _User_Role_Id;
        string _User_Name, _Employee_Name;
        int _Result;
        string _Result_Message;
        string _Password;
        string URL = "";
        Models.Users _User_det = new Models.Users();

        private bool IsClicked = false;
        public NewLogin()
        {
            InitializeComponent();

        }

        private void pictureEditClose_Click(object sender, EventArgs e)
        {
            this.Close();

            // Test
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateCredentials())
            {
                IsClicked = true;
                try
                {
                    btnLogin.Enabled = false;
                    SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                    Validate_User();
                    SplashScreenManager.CloseForm(false);
                }
                catch (Exception ex)
                {
                    btnLogin.Enabled = true;
                    IsClicked = false;
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show(ex.ToString());
                    XtraMessageBox.Show("Error Occured Please Check With Administrator");
                }
            }
        }

        private async void Validate_User()
        {
            string _Emp_Code = textEditUsername.Text.ToString().ToUpper();
            _Password = textEditPassword.Text.ToString();

            _User_det.DRN_Emp_Code = _Emp_Code;
            _User_det.Password = _Password;

            using (var Client = new HttpClient())
            {

                var serializedUser = JsonConvert.SerializeObject(_User_det);
                var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var result = await Client.PostAsync(Base_Url.Url+"/Login/Validate_User", content);

                if (result.IsSuccessStatusCode)
                {
                    var UserJsonString = await result.Content.ReadAsStringAsync();
                    var objResultData = JsonConvert.DeserializeObject<Result_Data>(UserJsonString);
                    if (objResultData.Users != null && objResultData.Users.Count > 0)
                    {
                        List<Models.Users> _Rlist_data = objResultData.Users.ToList();
                        //if (_Result == 0)
                        //{
                        _Application_Login_Type = _Rlist_data[0].Application_Login_Type;
                        _User_Id = _Rlist_data[0].User_id;
                        _User_Role_Id = _Rlist_data[0].User_RoleId;
                        _User_Name = _Rlist_data[0].DRN_Emp_Code;
                        _Employee_Name = _Rlist_data[0].Employee_Name;

                        if (_Application_Login_Type == 1)
                        {
                            if (_User_Role_Id == 2)
                            {
                                Employee.Dashboard dashboard = new Employee.Dashboard(_User_Id, _User_Role_Id);
                                Invoke(new MethodInvoker(delegate { dashboard.Show(); }));
                            }
                            else
                            {
                                AdminDashboard mainmenu = new AdminDashboard(_User_Role_Id.ToString(), _User_Id.ToString(), _User_Name, _Password);
                                mainmenu.Show();
                            }
                        }
                        else if (_Application_Login_Type == 2)
                        {
                            Tax.Tax_New_Dashboard taxdashboard = new Tax.Tax_New_Dashboard(_User_Role_Id.ToString(), _User_Id.ToString(), _Employee_Name);
                            taxdashboard.Show();
                        }

                        btnLogin.Enabled = true;
                        SplashScreenManager.CloseForm(false);

                        NewLogin _Form_New_Login = new NewLogin();
                        _Form_New_Login.Close();
                        this.Hide();
                        //}
                        //else
                        //{
                        //    btnLogin.Enabled = true;
                        //    IsClicked = false;
                        //    SplashScreenManager.CloseForm(false);
                        //    XtraMessageBox.Show("something went wrong");
                        //}
                    }
                    else
                    {
                        btnLogin.Enabled = true;
                        IsClicked = false;
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Wrong User Name and Password");
                    }
                }
                else
                {
                    btnLogin.Enabled = true;
                    IsClicked = false;
                    SplashScreenManager.CloseForm(false);
                }
            }
        }

        private bool ValidateCredentials()
        {
            if (string.IsNullOrEmpty(textEditUsername.Text.Trim()))
            {
                XtraMessageBox.Show("Username should not be empty");
                return false;
            }
            if (string.IsNullOrEmpty(textEditPassword.Text.Trim()))
            {
                XtraMessageBox.Show("Password should not be empty");
                return false;
            }
            return true;
        }

        private void btnViewPassword_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && e.Clicks == 1)
            //{
            //    textEditPassword.Properties.UseSystemPasswordChar = false;
            //    //  textEditPassword.Focus();
            //}
        }

        private void btnViewPassword_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && e.Clicks == 1)
            //{
            //    if (textEditPassword.Properties.UseSystemPasswordChar == true)
            //    {
            //        textEditPassword.Properties.UseSystemPasswordChar = false;
            //    }
            //    else
            //    {
            //        textEditPassword.Properties.UseSystemPasswordChar = true;

            //    }
            //}
        }

        private void btnViewPassword_Click(object sender, EventArgs e)
        {
            if (textEditPassword.Properties.UseSystemPasswordChar == false)
            {
                textEditPassword.Properties.UseSystemPasswordChar = true;
            }
            else
            {
                textEditPassword.Properties.UseSystemPasswordChar = false;

            }

        }

        private void NewLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                if (!IsClicked)
                {
                    btnLogin.Focus();
                    btnLogin_Click(sender, e);
                }
            }
        }

        private void textEditPassword_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditPassword = sender as TextEdit;
            if (!string.IsNullOrEmpty(textEditPassword.Text.Trim()))
            {
                btnViewPassword.Visible = true;
            }
            else
            {
                btnViewPassword.Visible = false;
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            Init();
            SplashScreenManager.CloseForm(false);
        }

        private void Init()
        {
            lblCopyright.Text = $"© {DateTime.Now.Year} DRN All Rights Reserved.";
        }
    }



}
