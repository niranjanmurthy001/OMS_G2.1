using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraSplashScreen;


namespace Ordermanagement_01.Gen_Forms
{
    public partial class Login : Form
    {
        SplashScreenManager splashScreenManager1 = new SplashScreenManager();
        Commonclass Comclass = new Commonclass();
        DataAccess Dataaccess = new DataAccess();
        int SubprocessId, ClientId;
        string Confirmusername, ConfirmPassword, Userid, RollName;
        string EmployeeId;
        static string userid,Application_Login_Id;
        string Username, password;
        string Empname;
        string USerRoleid;
        
        int Branch_id;
        string last_login_datetime;
        string strIpAddress;
        protected string msUserID = "";
        protected string msSessionCacheValue = "";
   
        public Login()
        {
            InitializeComponent();

            this.ShowInTaskbar = true;
            this.Text = "Titlelogy-Login";
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("favicon.icon")));
            //this.Icon = new Icon("Resources/favicon.ico");
        }

        private void btn_login_Click(object sender, EventArgs e)





        {
           
            Hashtable htmastuser = new Hashtable();
            DataTable dtmstuser = new DataTable();
            string empCode = txt_Emp_Code.Text.ToString().ToUpper();
            string password = txt_password.Text.ToString();

            htmastuser.Add("@Trans", "GET_USER_BY_EMP_CODE");
            htmastuser.Add("@DRN_Emp_Code", empCode);
            dtmstuser = Dataaccess.ExecuteSP("Sp_User", htmastuser);
            //empCode = txt_Emp_Code.Text.ToString();
            //password = txt_password.Text.ToString();
            if (dtmstuser.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dtmstuser.Rows[0]["DRN_Emp_Code"].ToString()))
                {
                    txt_Emp_Code.Text = "";
                    txt_Emp_Code.Focus();
                    MessageBox.Show("User Does Not Exist");
                    return;
                }
                else {
                    Confirmusername = dtmstuser.Rows[0]["DRN_Emp_Code"].ToString().ToUpper();
                }               
                ConfirmPassword = dtmstuser.Rows[0]["Password"].ToString();
                Empname = dtmstuser.Rows[0]["Employee_Name"].ToString();
                userid = dtmstuser.Rows[0]["User_id"].ToString();
                USerRoleid = dtmstuser.Rows[0]["User_RoleId"].ToString();
                Branch_id = int.Parse(dtmstuser.Rows[0]["Branch_ID"].ToString());
                Application_Login_Id = dtmstuser.Rows[0]["Application_Login_Type"].ToString();
             //   last_login_datetime = dtmstuser.Rows[0]["Last_login"].ToString();
            }
            else
            {
                txt_Emp_Code.Text = "";
                txt_Emp_Code.Focus();
               // ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('Username Does Not Exist');", true);
                MessageBox.Show("Username Does Not Exist");
            }

            if (Confirmusername == empCode && ConfirmPassword == password)
            {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                
                try
                {

                    string userID = userid;

                    DataTable dt_Employee_Status = new DataTable();
                    Hashtable ht_Employee_Status = new Hashtable();
                    ht_Employee_Status.Add("@Trans", "Log_In_Present");
                    ht_Employee_Status.Add("@Presents", "True");
                    ht_Employee_Status.Add("@Employee_Id", userid);

                    dt_Employee_Status = Dataaccess.ExecuteSP("Sp_Employee_Status", ht_Employee_Status);
                    if (Application_Login_Id == "1")
                    {


                        AdminDashboard mainmenu = new AdminDashboard(USerRoleid, userid, Empname,password);

                        mainmenu.Show();
                    }
                    else if (Application_Login_Id == "2")
                    {

                        Tax.Tax_New_Dashboard taxdashboard = new Tax.Tax_New_Dashboard(USerRoleid, userid, Empname);
                        taxdashboard.Show();
                    }

                    Ordermanagement_01.Gen_Forms.Login loginfrm = new Ordermanagement_01.Gen_Forms.Login();

                    loginfrm.Close();
                    this.Hide();
                }
                catch (Exception ex)
                {

                    //Close Wait Form
                    SplashScreenManager.CloseForm(false);

                    MessageBox.Show("Error Occured Please Check With Administrator");
                }
                finally
                {
                    //Close Wait Form
                    SplashScreenManager.CloseForm(false);
                }
            }
            else
            {
               // ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('Wrong User Name or Password');", true);
                MessageBox.Show("Wrong User Name or Password");
            }


          //  Hashtable htuser = new Hashtable();
          //  DataTable dtuser = new DataTable();
          //  Username = txt_username.Text.ToString();
          //  password = txt_password.Text.ToString();
          //  htuser.Add("@Trans", "SELUSER");
          //  htuser.Add("@User_Name", Username);
          //  dtuser = Dataaccess.ExecuteSP("Sp_User", htuser);
          //  if (dtuser.Rows.Count > 0)
          //  {
          //      Confirmusername = dtuser.Rows[0]["User_Name"].ToString();
          //      userid = dtuser.Rows[0]["User_id"].ToString();
          //  }
          //  else
          //  {
          //      txt_username.Text = "";
          //      txt_username.Focus();
          //   //   ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('Username Does Not Exist');", true);
          //      MessageBox.Show("Username Does Not Exist");

          //  }



          //  Hashtable htpassword = new Hashtable();
          //  DataTable dtpassword = new DataTable();
          //  htpassword.Add("@Trans", "SELPASS");
          //  htpassword.Add("@User_Name", Username);
          //  htpassword.Add("@Password", password);
          //  htpassword.Add("@User_id", userid);

          //  dtpassword = Dataaccess.ExecuteSP("Sp_User", htpassword);
          //  if (dtpassword.Rows.Count > 0)
          //  {

          //      ConfirmPassword = dtpassword.Rows[0]["Password"].ToString();
          //      Userid = dtpassword.Rows[0]["User_id"].ToString();

          //  }
          //  else
          //  {


          //      txt_password.Text = "";
          //      txt_password.Focus();
          //   //   ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('Invalid Password');", true);
          //      MessageBox.Show("Invalid Password");
          //      return;
          //  }
          //  DataTable dt = new DataTable();
          //  Hashtable ht = new Hashtable();
          //  ht.Add("@Trans", "USERWISE");
          //  ht.Add("@User_id", Userid);
          ////  Session["userid"] = userid.ToString();
          //  dt = Dataaccess.ExecuteSP("Sp_User", ht);
          //  if (dt.Rows.Count > 0)
          //  {
          //      RollName = dt.Rows[0]["Role_Name"].ToString();
          //     // Session["Employee_Id"] = dt.Rows[0]["Employee_Id"].ToString();

          //  }

        }

        private void txt_Cancel_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Gen_Forms.Login login1 = new Ordermanagement_01.Gen_Forms.Login();
            login1.Close();
            System.Environment.Exit(1);
        }

   

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txt_username_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Enter)
            {
            txt_password.Focus();
            }
        }

        private void txt_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login.Focus();
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

       
      

    }
}
