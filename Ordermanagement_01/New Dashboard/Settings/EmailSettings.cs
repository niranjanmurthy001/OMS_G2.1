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
    public partial class EmailSettings :XtraForm
    {

        DataAccess dataccess = new DataAccess();
        Hashtable ht = new Hashtable();
        private int emailId;
        int Email_Type;

        public EmailSettings()
        {
            InitializeComponent();
        }

        private void EmailSettings_Load(object sender, EventArgs e)
        {
            grid_Email_Address_list();
            Bind_Incoming_Ports();
            Bind_Outgoing_Ports();
        }
        private bool Validation()
        {
            if (txt_yourname.Text == "")
            {
                MessageBox.Show("Enter Your_Name");
                txt_yourname.Focus();
                return false;
            }
            else if (txt_Email_address.Text == "")
            {
                MessageBox.Show("Enter Email_Address");
                txt_Email_address.Focus();
                return false;
            }
            else if (txt_Incoming_server.Text == "")
            {
                MessageBox.Show("Enter Incoming Server details");
                txt_Incoming_server.Focus();
                return false;
            }
            else if (txt_Outgoing_server.Text == "")
            {
                MessageBox.Show("Enter Outgoing server details");
                txt_Outgoing_server.Focus();
                return false;
            }
            else if (txt_User_Name.Text == "")
            {
                MessageBox.Show("Enter User Name");
                txt_User_Name.Focus();
                return false;
            }
            else if (txt_password.Text == "")
            {
                MessageBox.Show("Enter Password");
                txt_password.Focus();
                return false;
            }
            else if (ddl_server_ISP.EditValue == null)
            {
                MessageBox.Show("Please select Incoming Server Port");
                return false;
            }
            else if (ddl_server_OSP.EditValue == null)
            {
                MessageBox.Show("Please select Outgoing Server Port");
                return false;
            }
            Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (myRegularExpression.IsMatch(txt_Email_address.Text))
            {

            }
            else
            {
                MessageBox.Show("Email Address Not Valid");
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
                    MessageBox.Show("E-mail Already Exists");
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
            if (Validation() != false && Usercheck() != false)
            {
                if (emailId == 0 && btn_save.Text == "Save")
                {
                    hsforSP.Add("@Trans", "INSERT");
                    hsforSP.Add("@Your_Name", txt_yourname.Text);
                    hsforSP.Add("@Email_Address", txt_Email_address.Text);
                    hsforSP.Add("@Incoming_Mail_Server", txt_Incoming_server.Text);
                    hsforSP.Add("@Outgoing_Mail_Server", txt_Outgoing_server.Text);
                    hsforSP.Add("@Incoming_Server_Port", ddl_server_ISP.EditValue);
                    hsforSP.Add("@Outgoing_Server_Port", ddl_server_OSP.EditValue);
                    hsforSP.Add("@User_Name", txt_User_Name.Text);
                    hsforSP.Add("@Password", txt_password.Text);
                    dt = dataccess.ExecuteSP("Sp_EmailVerify", hsforSP);
                    MessageBox.Show(txt_User_Name.Text + " Created Successfully ");
                    grid_Email_Address_list();
                    Clear();
                }
                if (btn_save.Text == "Edit")
                {
                    hsforSP.Add("@Trans", "UPDATE");
                    hsforSP.Add("@ID", emailId);
                    hsforSP.Add("@Your_Name", txt_yourname.Text);
                    hsforSP.Add("@Email_Address", txt_Email_address.Text);
                    hsforSP.Add("@Incoming_Mail_Server", txt_Incoming_server.Text);
                    hsforSP.Add("@Outgoing_Mail_Server", txt_Outgoing_server.Text);
                    hsforSP.Add("@Incoming_Server_Port", ddl_server_ISP.EditValue);
                    hsforSP.Add("@Outgoing_Server_Port", ddl_server_OSP.EditValue);
                    hsforSP.Add("@User_Name", txt_User_Name.Text);
                    hsforSP.Add("@Password", txt_password.Text);
                    dt = dataccess.ExecuteSP("Sp_EmailVerify", hsforSP);
                    MessageBox.Show(txt_User_Name.Text + " Updated Successfully ");
                    grid_Email_Address_list();
                    Clear();
                }
            }
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
        public void Bind_Incoming_Ports()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Clear();
            dt.Clear();
            ht.Add("@Trans", "BIND");
            dt = dataccess.ExecuteSP("Sp_Incoming_Port", ht);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = 0;
                dr[1] = "SELECT";
                dt.Rows.InsertAt(dr, 0);
            }
            ddl_server_ISP.Properties.DataSource = dt;
            ddl_server_ISP.Properties.DisplayMember = "Incoming_Server_Port";
            ddl_server_ISP.Properties.ValueMember = "In_Port_Id";
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Incoming_Server_Port");
            ddl_server_ISP.Properties.Columns.Add(col);
        }
        public void Bind_Outgoing_Ports()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Clear();
            dt.Clear();
            ht.Add("@Trans", "BIND");
            dt = dataccess.ExecuteSP("Sp_Outgoing_Port", ht);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = 0;
                dr[1] = "SELECT";
                dt.Rows.InsertAt(dr, 0);
            }
            ddl_server_OSP.Properties.DataSource = dt;
            ddl_server_OSP.Properties.DisplayMember = "Outgoing_Server_Port";
            ddl_server_OSP.Properties.ValueMember = "OutPort_Id";
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Outgoing_Server_Port");
            ddl_server_OSP.Properties.Columns.Add(col);
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
                    txt_yourname.Text = row["Your_Name"].ToString();
                    txt_Email_address.Text = row["Email_Address"].ToString();
                    txt_Incoming_server.Text = row["Incoming_Mail_Server"].ToString();
                    txt_Outgoing_server.Text = row["Outgoing_Mail_Server"].ToString();
                    txt_User_Name.Text = row["User_Name"].ToString();
                    txt_password.Text = row["Password"].ToString();
                    ddl_server_ISP.EditValue = int.Parse(row["Incoming_Server_Port"].ToString());
                    ddl_server_OSP.EditValue = int.Parse(row["Outgoing_Server_Port"].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex");
            }
        }
        public void Clear()
        {
            txt_yourname.Text = "";
            txt_Email_address.Text = "";
            txt_Incoming_server.Text = "";
            txt_Outgoing_server.Text = "";
            txt_User_Name.Text = "";
            txt_password.Text = "";
            ddl_server_ISP.EditValue = 0;
            ddl_server_OSP.EditValue = 0;
        }
    }
}
