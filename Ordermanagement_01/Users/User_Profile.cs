using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace Ordermanagement_01.Users
{
    public partial class User_Profile : Form
    {
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_id;
        byte[] bimage;
        public User_Profile(int userid)
        {
            InitializeComponent();
            User_id = userid;
            Hashtable htuser = new Hashtable();
            DataTable dtuser = new DataTable();
            htuser.Add("@Trans", "USER_IDWISE");
            htuser.Add("@User_id", User_id);
            dtuser = dataaccess.ExecuteSP("Sp_User", htuser);
            for (int i = 0; i < dtuser.Rows.Count ;i++)
            {
                txt_company.Text = dtuser.Rows[i]["Company_Name"].ToString();
                txt_branch.Text = dtuser.Rows[i]["Branch_Name"].ToString();
                lbl_User_Profile_Name.Text = dtuser.Rows[i]["Employee_Name"].ToString();
                txt_user.Text = dtuser.Rows[i]["User_Name"].ToString();
                lbl_User_Role.Text = dtuser.Rows[i]["Role_Name"].ToString();
                txt_mob_no.Text = dtuser.Rows[i]["Mobileno"].ToString();
                txt_email.Text = dtuser.Rows[i]["Email"].ToString();
                if (dtuser.Rows[i]["User_Photo"].ToString() != "")
                {
                    bimage = (Byte[])(dtuser.Rows[i]["User_Photo"]);
                    MemoryStream ms = new MemoryStream(bimage, 0, bimage.Length);
                    ms.Write(bimage, 0, bimage.Length);
                    //emp_img.Image = Image.FromStream(ms, true);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_mob_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void User_Profile_Load(object sender, EventArgs e)
        {
            Hashtable htuser = new Hashtable();
            DataTable dtuser = new DataTable();
            htuser.Add("@Trans", "USER_IDWISE");
            htuser.Add("@User_id", User_id);
            dtuser = dataaccess.ExecuteSP("Sp_User", htuser);
            for (int i = 0; i < dtuser.Rows.Count; i++)
            {
                txt_company.Text = dtuser.Rows[i]["Company_Name"].ToString();
                txt_branch.Text = dtuser.Rows[i]["Branch_Name"].ToString();
                lbl_User_Profile_Name.Text = dtuser.Rows[i]["Employee_Name"].ToString();
                txt_user.Text = dtuser.Rows[i]["User_Name"].ToString();
                lbl_User_Role.Text = dtuser.Rows[i]["Role_Name"].ToString();
                txt_mob_no.Text = dtuser.Rows[i]["Mobileno"].ToString();
                txt_email.Text = dtuser.Rows[i]["Email"].ToString();
                //if (dtuser.Rows[i]["User_Photo"].ToString() != "" || dtuser.Rows[i]["User_Photo"].ToString() != null)
                //{
                //    bimage = (Byte[])(dtuser.Rows[i]["User_Photo"]);
                //    MemoryStream ms = new MemoryStream(bimage, 0, bimage.Length);
                //    ms.Write(bimage, 0, bimage.Length);
                //    emp_img.Image = Image.FromStream(ms, true);
                //}
            }
            Size company_size = TextRenderer.MeasureText(txt_company.Text, txt_company.Font);
            txt_company.Width = company_size.Width;
            txt_company.Height = company_size.Height;
            Size branch_size = TextRenderer.MeasureText(txt_branch.Text, txt_branch.Font);
            txt_branch.Width = branch_size.Width;
            txt_branch.Height = branch_size.Height;
            Size user_size = TextRenderer.MeasureText(txt_user.Text, txt_user.Font);
            txt_user.Width = user_size.Width;
            txt_user.Height = user_size.Height;
            Size mob_size = TextRenderer.MeasureText(txt_mob_no.Text, txt_mob_no.Font);
            txt_mob_no.Width = mob_size.Width;
            txt_mob_no.Height = mob_size.Height;
            Size email_size = TextRenderer.MeasureText(txt_email.Text, txt_email.Font);
            txt_email.Width = email_size.Width;
            txt_email.Height = email_size.Height;
            Size profile_size = TextRenderer.MeasureText(lbl_User_Profile_Name.Text, lbl_User_Profile_Name.Font);
            lbl_User_Profile_Name.Width = profile_size.Width;
            lbl_User_Profile_Name.Height = profile_size.Height;
            Size role_size = TextRenderer.MeasureText(lbl_User_Role.Text, lbl_User_Role.Font);
            lbl_User_Role.Width = role_size.Width;
            lbl_User_Role.Height = role_size.Height;
        }

        private void txt_mob_no_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_User_Profile_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_mob_no_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txt_user_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_branch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_company_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void emp_img_Click(object sender, EventArgs e)
        {

        }

        private void lbl_User_Role_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            string oldpwd = txt_OldPwd.Text;
            string newpwd = txt_NewPwd.Text;
            string reentrpwd = txt_Re_enterpwd.Text;
            if (Validation() != false)
            {
                Hashtable htold = new Hashtable();
                DataTable dtold = new DataTable();
                htold.Add("@Trans", "OLDPWD");
                htold.Add("@User_id", User_id);
                htold.Add("@Password", oldpwd);
                dtold = dataaccess.ExecuteSP("Sp_User", htold);
                if (dtold.Rows.Count > 0)
                {
                    string pwd = dtold.Rows[0]["Password"].ToString();
                    if (newpwd == reentrpwd)
                    {
                        if (newpwd != pwd)
                        {
                            Hashtable htnew = new Hashtable();
                            DataTable dtnew = new DataTable();
                            htnew.Add("@Trans", "CHANGEPWD");
                            htnew.Add("@Password", newpwd);
                            htnew.Add("@User_id", User_id);
                            dtnew = dataaccess.ExecuteSP("Sp_User", htnew);
                            MessageBox.Show("New Password Updated Successfully");
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("New Password matched to old password... Give New one..");
                            txt_NewPwd.Text = "";
                            txt_Re_enterpwd.Text = "";
                            txt_NewPwd.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Passwords mismatched... Enter Correct Password");
                        txt_NewPwd.Text = "";
                        txt_Re_enterpwd.Text = "";
                        txt_NewPwd.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter the correct Password");
                    clear();
                }
            }
           
        }
        private bool Validation()
        {
            if (txt_OldPwd.Text == "")
            {
                MessageBox.Show("Please Enter your Old Password");
                txt_OldPwd.Focus();
                return false;
            }
            else if (txt_NewPwd.Text == "")
            {
                MessageBox.Show("Please Enter your New Password");
                txt_NewPwd.Focus();
                return false;
            }
            else if (txt_Re_enterpwd.Text == "")
            {
                MessageBox.Show("Re-Enter Password field is required");
                txt_Re_enterpwd.Focus();
                return false;
            }
            return true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            txt_OldPwd.Text = "";
            txt_NewPwd.Text = "";
            txt_Re_enterpwd.Text = "";
        }

        private void txt_OldPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_NewPwd.Focus();
            }
        }

        private void txt_NewPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Re_enterpwd.Focus();
            }
        }

        private void txt_Re_enterpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Submit.Focus();
            }
        }

        private void btn_Submit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Cancel.Focus();
            }
        }
    }
}
