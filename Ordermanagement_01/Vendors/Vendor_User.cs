using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_User : Form
    {
        Commonclass cc = new Commonclass();
        DataAccess dbc = new DataAccess();
        DropDownistBindClass drp = new DropDownistBindClass();
        int Vendor_User_Id,vendorid,User_Id;
        TreeNode parentnode;
        string sKeyTemp = "";
        string vendorstatus,vendorname;
        public Vendor_User(int Vendorid,int userid,string Vendor_status,string Vendorname)
        {
            InitializeComponent();
            
            vendorid = Vendorid;
            User_Id = userid;
            vendorstatus = Vendor_status;
            vendorname = Vendorname;
            AddParent();

        
        }

        private void tree_VendorName_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txt_Vendoruser.Focus();
          
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            bool ISNUM = Int32.TryParse(tree_VendorName.SelectedNode.Name, out Vendor_User_Id);
            if (ISNUM)
            {
               
                htselect.Add("@Trans", "SELECT");
                htselect.Add("@Vendor_User_Id", Vendor_User_Id);
                dtselect = dbc.ExecuteSP("Sp_Vendor_User", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    if (vendorstatus == "Overall")
                    {
                        ddl_Vendorname.Visible = true;
                        txt_Vendor_name.Visible = false;
                        ddl_Vendorname.SelectedValue = int.Parse(dtselect.Rows[0]["Vendor_Id"].ToString());
                    }
                    else if (vendorstatus == "Individual")
                    {
                        ddl_Vendorname.Visible = false;
                        txt_Vendor_name.Visible = true;
                        txt_Vendor_name.Text = tree_VendorName.Nodes[0].Text;
                    }
                    txt_Vendoruser.Text = dtselect.Rows[0]["User_Name"].ToString();
                    txt_Password.Text = dtselect.Rows[0]["Password"].ToString();
                    txt_confirmpwd.Text = dtselect.Rows[0]["Password"].ToString();
                    //txt_confirmpwd.Enabled = false;
                    //txt_confirmpwd.ReadOnly = true;
                }
                btn_Submit.Text = "Update";
                txt_Vendoruser.Select();
            }
            else
            {

                
            }

            
        }

        private void AddParent()
        {
            if (vendorstatus == "Overall")
            {
                
                tree_VendorName.Nodes.Clear();

                Hashtable ht = new Hashtable();
                DataTable dt = new System.Data.DataTable();
                
                string Vendor_Id;
                ht.Add("@Trans", "BIND_VENDOR_ALL");

                dt = dbc.ExecuteSP("Sp_Vendor_User", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Vendor_Id = dt.Rows[i]["Vendor_Id"].ToString();
                    sKeyTemp = dt.Rows[i]["Vendor_Name"].ToString();
                    parentnode = tree_VendorName.Nodes.Add(sKeyTemp, sKeyTemp);
                    AddChilds(parentnode, Vendor_Id);
                }
            }
            else if (vendorstatus == "Individual")
            {
                tree_VendorName.Nodes.Clear();

                Hashtable ht = new Hashtable();
                DataTable dt = new System.Data.DataTable();
                TreeNode parentnode;
                string Vendor_Id;
                ht.Add("@Trans", "BIND_VENDOR");
                ht.Add("@Vendor_id", vendorid);
                dt = dbc.ExecuteSP("Sp_Vendor_User", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Vendor_Id = dt.Rows[i]["Vendor_Id"].ToString();
                    sKeyTemp = dt.Rows[i]["Vendor_Name"].ToString();
                    parentnode = tree_VendorName.Nodes.Add(sKeyTemp, sKeyTemp);
                    AddChilds(parentnode, Vendor_Id);
                }
            }
            tree_VendorName.ExpandAll();
        }

        private void AddChilds(TreeNode parentnode, string venodrid)
        {
            string sKeyTemp2 = "";
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "BIND_TREE");
            ht.Add("@Vendor_id", venodrid);
            dt = dbc.ExecuteSP("Sp_Vendor_User", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp2 = dt.Rows[i]["User_Name"].ToString();
                string ckey = dt.Rows[i]["Vendor_User_Id"].ToString();

                parentnode.Nodes.Add(ckey, sKeyTemp2);
            }
        }

        private void Vendor_User_Load(object sender, EventArgs e)
        {
                   
            if (vendorid == 0 && vendorname == "")
            {
                ddl_Vendorname.Visible = true;
                txt_Vendor_name.Visible = false;
                drp.Bind_Vendors(ddl_Vendorname);
                ddl_Vendorname.Select();
               
            }
            else
            {
                ddl_Vendorname.Visible = false;
                txt_Vendor_name.Visible = true;
                txt_Vendor_name.Text = vendorname;
              //  txt_Vendoruser.Focus();
                txt_Vendoruser.Select();
                
            }

            //txt_Vendoruser.Focus();
            
        }

        private bool validation_User()
        {

            if (ddl_Vendorname.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly Select Vendor Name");
                return false;
            }
            else if (txt_Vendoruser.Text == "")
            {
                MessageBox.Show("Kindly Enter UserName");
                return false;
            }
            else if (txt_Password.Text == "")
            {
                MessageBox.Show("Kindly Enter Password");
                return false;
            }
            else if (txt_confirmpwd.Text == "")
            {
                MessageBox.Show("Kindly Enter Confirm Password");
                return false;
            }
            return true;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (validation_User() != false)
            {
                if (btn_Submit.Text == "Update")
                {
                    if (Vendor_User_Id != 0)
                    {
                       // vendorid = int.Parse(ddl_Vendorname.SelectedValue.ToString());
                        if (vendorid == 0 && vendorname == "")
                        {
                            if (txt_Password.Text == txt_confirmpwd.Text)
                            {
                                Hashtable htupdate = new Hashtable();
                                DataTable dtupdate = new DataTable();
                                htupdate.Add("@Trans", "UPDATE");
                                htupdate.Add("@Vendor_id", ddl_Vendorname.SelectedValue);
                                htupdate.Add("@Vendor_User_Id", Vendor_User_Id);
                                htupdate.Add("@User_Name", txt_Vendoruser.Text);
                                htupdate.Add("@Password", txt_Password.Text);

                                htupdate.Add("@Modified_By", User_Id);
                                dtupdate = dbc.ExecuteSP("Sp_Vendor_User", htupdate);
                                MessageBox.Show("Vendor User Updated successfuly");
                                ddl_Vendorname.Select();
                                AddParent();
                                Clear();
                                btn_Submit.Text = "Submit";
                            }
                            else
                            {
                                MessageBox.Show("Password not matching.. kindly update it");
                                txt_Password.Focus();
                            }
                        }
                        else
                        {

                            if (txt_Password.Text == txt_confirmpwd.Text)
                            {

                                Hashtable htupdate = new Hashtable();
                                DataTable dtupdate = new DataTable();
                                htupdate.Add("@Trans", "UPDATE");
                                htupdate.Add("@Vendor_id", vendorid);
                                htupdate.Add("@Vendor_User_Id", Vendor_User_Id);
                                htupdate.Add("@User_Name", txt_Vendoruser.Text);
                                htupdate.Add("@Password", txt_Password.Text);
                                htupdate.Add("@Modified_By", User_Id);
                                dtupdate = dbc.ExecuteSP("Sp_Vendor_User", htupdate);
                                MessageBox.Show("Vendor User Updated successfuly");
                                AddParent();
                                Clear();
                                btn_Submit.Text = "Submit";
                            }
                            else
                            {
                                MessageBox.Show("Password not matching.. kindly update it");
                                txt_Password.Focus();
                            }
                        }
                        
                        //update

                        //AddParent();
                        //Clear();
                        //btn_Submit.Text = "Submit";
                        txt_Vendoruser.Select();
                    }
                }
                else 
                {
                    if (btn_Submit.Text =="Submit")
                    {
                        if (vendorid == 0 && vendorname == "")
                        {
                            if (txt_Password.Text == txt_confirmpwd.Text)
                            {
                                //insert
                                Hashtable htinsert = new Hashtable();
                                DataTable dtinsert = new DataTable();
                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Vendor_id", ddl_Vendorname.SelectedValue);
                                htinsert.Add("@User_Name", txt_Vendoruser.Text);
                                htinsert.Add("@Password", txt_Password.Text);
                              
                                dtinsert = dbc.ExecuteSP("Sp_Vendor_User", htinsert);
                                MessageBox.Show("Vendor User Inserted successfuly");
                                AddParent();
                                Clear();
                            }
                            else
                            {
                                MessageBox.Show("Password not matching.. kindly update it");
                                txt_Password.Focus();
                            }
                        }
                        else
                        {
                            if (txt_Password.Text == txt_confirmpwd.Text)
                            {
                                //insert
                                Hashtable htinsert = new Hashtable();
                                DataTable dtinsert = new DataTable();
                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Vendor_id", vendorid);
                                htinsert.Add("@User_Name", txt_Vendoruser.Text);
                                htinsert.Add("@Password", txt_Password.Text);

                                dtinsert = dbc.ExecuteSP("Sp_Vendor_User", htinsert);
                                MessageBox.Show("Vendor User Inserted successfuly");
                                AddParent();
                                Clear();
                            }
                            else
                            {
                                MessageBox.Show("Password not matching.. kindly update it");
                                txt_Password.Focus();
                            }

                        }
                    }
                }

            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (Vendor_User_Id != 0)
            {
                if (vendorstatus == "Overall")
                {
                    Hashtable htdel = new Hashtable();
                    DataTable dtdel = new DataTable();
                    htdel.Add("@Trans", "DELETE");
                    htdel.Add("@Vendor_User_Id", Vendor_User_Id);
                    dtdel = dbc.ExecuteSP("Sp_Vendor_User", htdel);
                    MessageBox.Show("Vendor User Deleted Successfully");
                }
                else if (vendorstatus == "Individual")
                {
                    Hashtable htdel = new Hashtable();
                    DataTable dtdel = new DataTable();
                    htdel.Add("@Trans", "DELETE");
                    htdel.Add("@Vendor_User_Id", Vendor_User_Id);
                    dtdel = dbc.ExecuteSP("Sp_Vendor_User", htdel);
                    MessageBox.Show("Vendor User Deleted Successfully");
                }
            }
            else
            {
                MessageBox.Show("Select Vendor properly");
            }
            Clear();
            btn_Submit.Text = "Submit";
            AddParent();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Clear();
            AddParent();
            ddl_Vendorname.Select();
            if (txt_Vendoruser.Enabled==true)
            {
                txt_Vendoruser.Select();
            }
        }

        private void Clear()
        {
            if (vendorstatus == "Overall")
            {
                txt_Vendoruser.Text = "";
                txt_Vendor_name.Text = "";
                txt_Password.Text = "";
                txt_confirmpwd.Enabled = true;
                txt_confirmpwd.ReadOnly = false;
                txt_confirmpwd.Text = "";
                ddl_Vendorname.SelectedValue = 0;
              //  ddl_Vendorname.SelectedIndex = 0;
                chk_Password.Checked = false;
                chk_ConfPwd.Checked = false;
            }
            else if (vendorstatus == "Individual")
            {
                txt_Vendoruser.Text = "";
                //txt_Vendor_name.Text = "";
                txt_Password.Text = "";
                //txt_confirmpwd.Enabled = false;
                //txt_confirmpwd.ReadOnly = true;
                txt_confirmpwd.Text = "";
                ddl_Vendorname.SelectedValue = 0;
                //ddl_Vendorname.SelectedIndex = 0;
                chk_Password.Checked = false;
                chk_ConfPwd.Checked = false;
                
            }
            Vendor_User_Id = 0;
            btn_Submit.Text = "Submit";
            txt_Vendoruser.Select();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            
            AddParent();
            Clear();
            btn_Submit.Text = "Submit";
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

        private void chk_ConfPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ConfPwd.Checked == false)
            {
                txt_confirmpwd.PasswordChar = '*';
                chk_ConfPwd.Checked = false;
                chk_ConfPwd.Text = "Show Password";
            }
            else
            {
                txt_confirmpwd.PasswordChar = '\0';
                chk_ConfPwd.Checked = true;
                chk_ConfPwd.Text = "Hide Password";
            }
        }

        private void txt_Vendoruser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void ddl_Vendorname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_Vendorname.SelectedIndex>0)
            {
                txt_Vendoruser.Select();
                txt_confirmpwd.Text = "";
                txt_Password.Text = "";
                txt_Vendoruser.Text = "";
                txt_Vendor_name.Text = "";
                chk_ConfPwd.Checked = false;
                chk_Password.Checked = false;
                btn_Submit.Text = "Submit";
                
            }
        }

       

    }
}
