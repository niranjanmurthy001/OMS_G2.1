using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace Ordermanagement_01.Client_Proposal
{
    public partial class Proposal_Email_Settings : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        Hashtable ht = new Hashtable();
        DataTable dt = new System.Data.DataTable();

        int Userid,Proposal_Client_emailid;string User_Roleid;
        int Proposal_From_Id;
        public Proposal_Email_Settings( int userid,string user_roleid)
        {
            InitializeComponent();
            Userid = userid;
            User_Roleid = user_roleid;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void grp_Branch_det_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            txt_Email_Id.Text = "";
            txt_Password.Text = "";
            lbl_RecordAddedBy.Text = "";
            lbl_RecordAddedOn.Text = "";
            ddl_Proposal_From.SelectedIndex = 0;
            btn_Save.Text = "Submit";

        }
       

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (Proposal_Client_emailid != 0)
            {
                Hashtable htdel = new Hashtable();
                DataTable dtdel = new DataTable();
                htdel.Add("@Trans", "DELETE_EMAILPASS");
                htdel.Add("@Proposal_Email_Settings_id",Proposal_Client_emailid);
                dtdel = dataaccess.ExecuteSP("Sp_Proposal_Client", htdel);
                MessageBox.Show("Proposal Email Record Deleted Successfully");
            }
        }
        private bool Validation()
        {
            if (ddl_Proposal_From.SelectedIndex <= 0)
            {

                MessageBox.Show("kindly Select Proposal For");
                ddl_Proposal_From.Focus();

                return false;
            }
            if (txt_Email_Id.Text == "")
            {
                MessageBox.Show("Kindly Enter Email Id");
                return false;
            }
            else if (txt_Email_Id.Text != "")
            {
                Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
                if (myRegularExpression.IsMatch(txt_Email_Id.Text))
                {
                    //valid e-mail
                }
                else
                {
                    MessageBox.Show("Email Address Not Valid");
                }
            }
            else if (txt_Password.Text == "")
            {
                MessageBox.Show("Kindly Enter Password");
                return false;
            }
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Validation() != false && Proposal_Client_emailid != 0)
            {
                //Update
                Hashtable htup = new Hashtable();
                DataTable dtup = new DataTable();
                htup.Add("@Trans", "UPDAE_EMAILPASS");
                htup.Add("@Proposal_Email_Settings_id", Proposal_Client_emailid);
                htup.Add("@Proposal_From_Id", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                htup.Add("@Email_Id", txt_Email_Id.Text);
                htup.Add("@Password", txt_Password.Text);
                htup.Add("@Modified_by", Userid);
                dtup = dataaccess.ExecuteSP("Sp_Proposal_Client", htup);
                MessageBox.Show("Proposal Email Record Updated Successfully");
                clear();
                AddParent();
            }
            else if (Validation() != false && Proposal_Client_emailid == 0)
            {
                //Insert
                Hashtable htin = new Hashtable();
                DataTable dtin = new DataTable();
                htin.Add("@Trans", "INSERT_EMAILPASS");
                htin.Add("@Proposal_From_Id",int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                htin.Add("@Email_Id", txt_Email_Id.Text);
                htin.Add("@Password", txt_Password.Text);
                htin.Add("@Inserted_by", Userid);
                dtin = dataaccess.ExecuteSP("Sp_Proposal_Client", htin);
                MessageBox.Show("Proposal Email Record Inserted Successfully");
                clear();
                AddParent();
            }
        }

        private void tvw_Proposal_Email_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool isnum = Int32.TryParse(tvw_Proposal_Email.SelectedNode.Name, out Proposal_Client_emailid);
            if (isnum)
            {
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "SELECT_EMAILPASSID");
                ht.Add("@Proposal_Email_Settings_id", Proposal_Client_emailid);
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                if (dt.Rows.Count > 0)
                {
                    ddl_Proposal_From.SelectedValue = dt.Rows[0]["Proposal_From_Id"].ToString();
                    txt_Email_Id.Text = dt.Rows[0]["Email_Id"].ToString();
                    txt_Password.Text = dt.Rows[0]["Password"].ToString();
                    if (dt.Rows[0]["Modified_by"].ToString() != "")
                    {
                        lbl_RecordAddedBy.Text = dt.Rows[0]["Modified_by"].ToString();
                        lbl_RecordAddedOn.Text = dt.Rows[0]["Modified_Date"].ToString();
                    }
                    else
                    {
                        lbl_RecordAddedBy.Text = dt.Rows[0]["Inserted_by"].ToString();
                        lbl_RecordAddedOn.Text = dt.Rows[0]["Inserted_Date"].ToString();
                    }
                    btn_Save.Text = "Edit";
                }
            }
        }

        private void txt_Search_Client_TextChanged(object sender, EventArgs e)
        {

        }

        private void Proposal_Email_Settings_Load(object sender, EventArgs e)
        {
            AddParent();
            dbc.Bind_Proposal_From(ddl_Proposal_From);
        }
        private void AddParent()
        {
         
         
            string sKeyTemp = "";
            tvw_Proposal_Email.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
          
            ht.Add("@Trans", "SELECT_PROPOSAL_FROM");
            sKeyTemp = "Proposal For";
            dt = dataaccess.ExecuteSP("Sp_Proposal_Master", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Proposal_From_Id =int.Parse(dt.Rows[i]["Proposal_From_Id"].ToString());
                sKeyTemp = dt.Rows[i]["Proposal_From"].ToString();
                parentnode = tvw_Proposal_Email.Nodes.Add(Proposal_From_Id.ToString(), sKeyTemp);
                AddChilds(parentnode, Proposal_From_Id.ToString());
            }

            tvw_Proposal_Email.ExpandAll();

        }
        private void AddChilds(TreeNode parentnode, string sKey)
        {
         
            string sKeyTemp2 = "";
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "SELECT_EMAILPASS");
            ht.Add("@Proposal_From_Id", sKey);
            dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp2 = dt.Rows[i]["Email_Id"].ToString();
                string ckey = dt.Rows[i]["Proposal_Email_Settings_id"].ToString();
                parentnode.Nodes.Add(ckey, sKeyTemp2);
            }
        }

        private void chk_Password_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Password.Checked == false)
            {
                txt_Password.PasswordChar = '*';
                chk_Password.Checked = false;
                chk_Password.Text = "Show Password";
            }
            else
            {
                txt_Password.PasswordChar = '\0';
                chk_Password.Checked = true;
                chk_Password.Text = "Hide Password";
            }
        }
    }
}
