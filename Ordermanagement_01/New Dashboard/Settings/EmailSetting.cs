using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private void grid_Email_Address_list()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "Select");
            dt = dataccess.ExecuteSP("Sp_EmailVerify", ht);
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

        private bool Validation()
        {
            if (txt_name.Text == "")
            {
                XtraMessageBox.Show("Enter Your_Name");
                txt_yourname.Focus();
                return false;
            }
            else if (txt_Email_address.Text == "")
            {
                XtraMessageBox.Show("Enter Email_Address");
                txt_Email_address.Focus();
                return false;
            }
          else if (txt_Incoming_server.Text == "")
            {
                XtraMessageBox.Show("Enter Incoming Server details");
                txt_Incoming_server.Focus();
                return false;
            }
            else if (txt_Outgoing_server.Text == "")
            {
                XtraMessageBox.Show("Enter Outgoing server details");
                txt_Outgoing_server.Focus();
                return false;
            }
            else if (txt_User_Name.Text == "")
            {
                XtraMessageBox.Show("Enter User Name");
                txt_User_Name.Focus();
                return false;
            }
            else if (txt_password.Text == "")
            {
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
            else if (((txt_IS.Text != "" && (Convert.ToInt32(txt_IS.Text.Length) > 3)) || txt_IS.Text == ""))
            {

                XtraMessageBox.Show("incoming port must be less than 3....And please enter Incoming port");
                txt_IS.Focus();
                return false;
            }
            //else if (txt_OS.Text == "")
            //{
            //    MessageBox.Show("Please enter the Outgoing Server Port");
            //    txt_OS.Focus();
            //    return false;
            //}
            else if (((txt_OS.Text != "" && (Convert.ToInt32(txt_OS.Text.Length) > 3) || txt_OS.Text == "")))
            {

               XtraMessageBox.Show("Outgoing port must be less than 3.....And please enter Outgoing port");
                txt_OS.Focus();
                return false;
            }
            Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (myRegularExpression.IsMatch(txt_Email_address.Text))
            {

            }
            else
            {
                XtraMessageBox.Show("Email Address Not Valid");
                txt_Email_address.Focus();
                return false;
            }

            return true;
        }
        public bool Usercheck()
        {

            DataTable dt = new DataTable();
            if (txt_Email_address.Text != "")
            {
                Hashtable htmail = new Hashtable();
                htmail.Add("@Trans", "EmailCheck");
                htmail.Add("@Email_Address", txt_Email_address.Text.Trim());
                dt = dataccess.ExecuteSP("Sp_EmailVerify", htmail);
                int count = Convert.ToInt32(dt.Rows[0]["count"].ToString());
                if (count > 0)
                {
                    XtraMessageBox.Show("E-mail Already Exists");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (Validation() != false)
            {
                if (emailId == 0 && btn_save.Text == "Save" && Usercheck() != false)
                {
                    txt_IS.Enabled = true;
                    txt_OS.Enabled = true;
                    hsforSP.Add("@Trans", "INSERT");
                    hsforSP.Add("@Your_Name", txt_name.Text);
                    hsforSP.Add("@Email_Address", txt_Email_address.Text);
                    hsforSP.Add("@Incoming_Mail_Server", txt_Incoming_server.Text);
                    hsforSP.Add("@Outgoing_Mail_Server", txt_Outgoing_server.Text);
                    hsforSP.Add("@Incoming_Server_Port", txt_IS.Text);
                    hsforSP.Add("@Outgoing_Server_Port", txt_OS.Text);
                    hsforSP.Add("@User_Name", txt_User_Name.Text);
                    hsforSP.Add("@Password", txt_password.Text);
                    hsforSP.Add("@Connection_SSL", check_connection_SSL.Checked);
                    dt = dataccess.ExecuteSP("Sp_EmailVerify", hsforSP);
                    XtraMessageBox.Show(txt_User_Name.Text + " Created Successfully ");
                    grid_Email_Address_list();
                    Clear();
                }
                if (btn_save.Text == "Edit")
                {

                    hsforSP.Add("@Trans", "UPDATE");
                    hsforSP.Add("@ID", emailId);
                    hsforSP.Add("@Your_Name", txt_name.Text);
                    hsforSP.Add("@Email_Address", txt_Email_address.Text);
                    hsforSP.Add("@Incoming_Mail_Server", txt_Incoming_server.Text);
                    hsforSP.Add("@Outgoing_Mail_Server", txt_Outgoing_server.Text);
                    hsforSP.Add("@Connection_SSL", check_connection_SSL.Checked);
                    hsforSP.Add("@Incoming_Server_Port", txt_IS.Text);
                    hsforSP.Add("@Outgoing_Server_Port", txt_OS.Text);
                    hsforSP.Add("@User_Name", txt_User_Name.Text);
                    hsforSP.Add("@Password", txt_password.Text);
                    dt = dataccess.ExecuteSP("Sp_EmailVerify", hsforSP);
                    XtraMessageBox.Show(txt_User_Name.Text + " Updated Successfully ");
                    grid_Email_Address_list();
                    Clear();
                }
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
                    txt_Email_address.Text = row["Email_Address"].ToString();
                    txt_Incoming_server.Text = row["Incoming_Mail_Server"].ToString();
                    txt_Outgoing_server.Text = row["Outgoing_Mail_Server"].ToString();
                    bool con = Convert.ToBoolean(Convert.ToInt32(row["Connection_SSL"]));
                    check_connection_SSL.Checked = con;
                    txt_User_Name.Text = row["User_Name"].ToString();
                    txt_password.Text = row["Password"].ToString();
                    txt_IS.Text = row["Incoming_Server_Port"].ToString();
                    txt_OS.Text = row["Outgoing_Server_Port"].ToString();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Ex");
             
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            string Email_Address = txt_Email_address.Text;
            if (txt_Email_address.Text != "")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Email_Address", Email_Address);
                dt = dataccess.ExecuteSP("Sp_EmailVerify", ht);
                gridControl1_Email_Address.DataSource = dt;
                int count = dt.Rows.Count;
                grid_Email_Address_list();
                XtraMessageBox.Show("Record Deleted Successfully");
                Clear();
            }
            else
            {
                XtraMessageBox.Show("Please Enter the Email Address");
            }

        }

        private void btn_testconnection_Click(object sender, EventArgs e)
        {      

            XtraMessageBox.Show( txt_name.Text + "  " + "Account Testing Finished");
        }
        private void txt_name_Properties_Leave(object sender, EventArgs e)
        {
            if (txt_name.Text == "")
            {
                XtraMessageBox.Show("Enter Your_Name");
                txt_yourname.Focus();
               
            }
        }
        private void txt_Email_address_Properties_Leave(object sender, EventArgs e)
        {
            Regex RegExForEmail = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");

            if (txt_Email_address.Text == "")
            {
                XtraMessageBox.Show("Enter Email_Address");
                txt_Email_address.Focus();
                return;
            }
            else if (RegExForEmail.IsMatch(txt_Email_address.Text))
            {

            }
            else
            {
                XtraMessageBox.Show("Email Address Not Valid");
                txt_Email_address.Focus();
            }
        }

        private void txt_Incoming_server_Properties_Leave(object sender, EventArgs e)
           {
            Regex RegExForIncomingServer = new Regex("^[0-9]{3}$");
            if (txt_Incoming_server.Text == "")
            {
                XtraMessageBox.Show("Enter Incoming Server details");
                txt_Incoming_server.Focus();
                return;
            }
            else if (RegExForIncomingServer.IsMatch(txt_Incoming_server.Text))
            {

            }
            else
            {
                XtraMessageBox.Show("Incoming Server Details Is Having less than 3....And please enter a Valid 3 Digit Incoming port No" );
                txt_Incoming_server.Focus();

            }
        
        }

        private void txt_Outgoing_server_Properties_Leave(object sender, EventArgs e)
        {
            Regex RegExForOutgoingServer = new Regex("^[0-9]{3}$");
            if (txt_Outgoing_server.Text == "")
            {
                XtraMessageBox.Show("Enter OutGoing Server details");
                txt_Outgoing_server.Focus();
                return;
            }
            else if (RegExForOutgoingServer.IsMatch(txt_Outgoing_server.Text))
            {

            }
            else
            {
                XtraMessageBox.Show("Outgoing Server Details Is Having less than 3.....And please enter a Valid 3 Digit Outgoing port No");
                txt_Outgoing_server.Focus();
            }
        }

        private void txt_IS_Properties_Leave(object sender, EventArgs e)
            {
            Regex RegExForIsPort = new Regex("^[0-9]{3}$");
            if (txt_IS.Text == "")
            {
                XtraMessageBox.Show("Enter Incoming Server Port details");
                txt_IS.Focus();
                return;
            }
            else if (RegExForIsPort.IsMatch(txt_IS.Text))
            {

            }
            else
            {
                XtraMessageBox.Show("Incoming Server Port Is Having less than 3....And please enter a Valid 3 Digit Incoming port No");
                txt_IS.Focus();
            }
        }
        private void txt_OS_Properties_Leave(object sender, EventArgs e)
        {
            Regex RegExForOsPort= new Regex("^[0-9]{3}$");
            if (txt_OS.Text == "")
            {
                XtraMessageBox.Show("Enter OutGoing Server Port details");
                txt_OS.Focus();
                return;
            }
            else if (RegExForOsPort.IsMatch(txt_OS.Text))
            {

            }
            else
            {
                XtraMessageBox.Show("Outgoing port Is Having less than 3.....And please enter a Valid 3 Digit Outgoing port No");
                txt_OS.Focus();
            }
        }

        private void txt_password_Properties_Leave(object sender, EventArgs e)
            {
            Regex RegExForPassword = new Regex("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[!*@#$%^&+=]).*$");
            if (txt_password.Text == "")
            {
                XtraMessageBox.Show("Enter Password");
                txt_password.Focus();
            }
            if (RegExForPassword.IsMatch(txt_password.Text))
            {

            }
            else
            {
                XtraMessageBox.Show("Password Should Be Having  Alpha-numeric & OneSpecialCharacter.");
                txt_password.Focus();
            }
        }

        private void txt_User_Name_Leave(object sender, EventArgs e)
        {
            if (txt_User_Name.Text == "")
            {
                XtraMessageBox.Show("Enter UserName");
                txt_User_Name.Focus();
                
            }
        }
    }
}
