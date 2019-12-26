using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace Ordermanagement_01
{
    public partial class Create_Branch : Form
    {
        int Branch_id;
        int Companyid;
        int userid = 0, compid;
        string username;
        string branchname;
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, branch_lbl, branch_lbl1, create_bran, create_bran1, del_bran,
            del_bran1, clear_btn, clear_btn1;

        public Regex pinCode = new Regex(@"^\d{6,}$", RegexOptions.Compiled);
        public Regex FaxNum = new Regex(@"^\d{15,}$", RegexOptions.Compiled);
        public Regex phoneno = new Regex(@"^\d{10,}$", RegexOptions.Compiled);
        public Regex CompName = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);
        public Regex CompSlogan = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);
        public Regex City = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        public Create_Branch(int user_id,string Username)
        {
            InitializeComponent();
           // dbc.BindCompany(ddl_Company);
            dbc.BindCountry(ddl_Branch_country);
            username = Username;
            //if (ddl_Branch_country.SelectedIndex > 0)
            //{
            //    dbc.BindState1(ddl_Branch_state, int.Parse(ddl_Branch_country.SelectedValue.ToString()));
            //}
            //if (ddl_Branch_state.SelectedIndex > 0)
            //{
            //    dbc.BindDistrict(ddl_Branch_district, int.Parse(ddl_Branch_state.SelectedValue.ToString()));
            //}
            userid = user_id;
        }

        private void ddl_Company_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branchname.Focus();
            }
        }

        private void txt_Branchname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branch_Code.Focus();
            }
        }

        private void txt_Branch_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branch_address.Focus();
            }
        }

        private void txt_Branch_address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Branch_country.Focus();
            }
        }

        private void ddl_Branch_country_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Branch_state.Focus();
            }
        }

        private void ddl_Branch_state_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Branch_district.Focus();
            }
        }

        private void ddl_Branch_district_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branch_city.Focus();
            }
        }

        private void txt_Branch_city_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_branch_Pincode.Focus();
            }
        }

        private void txt_branch_Pincode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branch_phono.Focus();
            }
        }

        private void txt_Branch_phono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branch_fax.Focus();
            }
        }

        private void txt_Branch_fax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branch_email.Focus();
            }
        }

        private void txt_Branch_email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Branch_website.Focus();
            }
        }

        private void txt_Branch_website_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChkDefault.Focus();
            }
        }

        private void ChkDefault_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void Create_Branch_Load(object sender, EventArgs e)
        {
            Branch_id = 0;
            dbc.BindCompany(ddl_Company);
            pnlSideTree.Visible = true;
            AddParent();
            lbl_RecordAddedBy.Text = username;
            lbl_RecordAddedOn.Text=DateTime.Now.ToString();
          //  ddl_Company.SelectedIndex = 0;
            ddl_Company.Select();

            dbc.BindState1(ddl_Branch_state, int.Parse(ddl_Branch_country.SelectedValue.ToString()));

            bindDistrict();
        }

        private void bindDistrict()
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "SELECT");
            htParam.Add("@StateID", ddl_Branch_state.SelectedValue.ToString());
            dt = dataaccess.ExecuteSP("sp_District", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            ddl_Branch_district.DataSource = dt;
            ddl_Branch_district.DisplayMember = "District_Name";
            ddl_Branch_district.ValueMember = "District_Id";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Companyid = ddl_Company.SelectedIndex;
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (ChkDefault.Checked == true)
            {
                hsforSP.Add("@Trans", "ChkDefault");
                dt = dataaccess.ExecuteSP("Sp_Branch", hsforSP);
                hsforSP.Clear();
            }
            if (Validation() != false)
            {
                if (Branch_id == 0 && Duplicate_Branch_Name() != false && btn_Save.Text!="Edit")
                {

                    hsforSP.Add("@Trans", "INSERT");
                    //hsforSP.Add("@Company_Id", txt_companyname.Text);
                    hsforSP.Add("@Company_Id", ddl_Company.SelectedValue);
                    hsforSP.Add("@Branch_Name", txt_Branchname.Text.ToUpper());
                    hsforSP.Add("@Branch_Code", txt_Branch_Code.Text);
                    hsforSP.Add("@Branch_Address", txt_Branch_address.Text);
                    hsforSP.Add("@Branch_Country", ddl_Branch_country.SelectedValue);
                    hsforSP.Add("@Branch_State", ddl_Branch_state.SelectedValue);
                    hsforSP.Add("@Branch_District", ddl_Branch_district.SelectedValue);
                    hsforSP.Add("@Branch_City", txt_Branch_city.Text);
                    hsforSP.Add("@Branch_Pincode", txt_branch_Pincode.Text);
                    hsforSP.Add("@Branch_Phone", txt_Branch_phono.Text);
                    hsforSP.Add("@Branch_Fax", txt_Branch_fax.Text);
                    hsforSP.Add("@Branch_Email", txt_Branch_email.Text);
                    hsforSP.Add("@Branch_Web", txt_Branch_website.Text);
                   // hsforSP.Add("@Branch_Chk_Default", ChkDefault.Checked);
                    hsforSP.Add("@Status", "TRUE");
                    hsforSP.Add("@Inserted_By", userid);
                    hsforSP.Add("@Inserted_date", DateTime.Now);
                  
                    //hsforSP.Add("@status", endofSupportLife);
                    dt = dataaccess.ExecuteSP("Sp_Branch", hsforSP);
                    string title = "Insert";
                    MessageBox.Show("Branch Created Sucessfully",title);
                    clear();
                    Branch_id = 0;
                    AddParent();
                }
                else if (Branch_id != 0 && Edit_duplic_Branch_Name() != false)
                {
                    //Update

                    hsforSP.Add("@Trans", "UPDATE");
                    hsforSP.Add("@Branch_ID", Branch_id);
                    hsforSP.Add("@Company_Id", ddl_Company.SelectedValue);
                    //hsforSP.Add("@Branch_ID", Branch_id);
                    hsforSP.Add("@Branch_Name", txt_Branchname.Text.ToUpper());
                    hsforSP.Add("@Branch_Code", txt_Branch_Code.Text);
                    hsforSP.Add("@Branch_Address", txt_Branch_address.Text);
                    hsforSP.Add("@Branch_Country", ddl_Branch_country.SelectedValue);
                    hsforSP.Add("@Branch_State", ddl_Branch_state.SelectedValue);
                    hsforSP.Add("@Branch_District", ddl_Branch_district.SelectedValue);
                    hsforSP.Add("@Branch_City", txt_Branch_city.Text);
                    hsforSP.Add("@Branch_Pincode", txt_branch_Pincode.Text);
                    hsforSP.Add("@Branch_Phone", txt_Branch_phono.Text);
                    hsforSP.Add("@Branch_Fax", txt_Branch_fax.Text);
                    hsforSP.Add("@Branch_Email", txt_Branch_email.Text);
                    hsforSP.Add("@Branch_Web", txt_Branch_website.Text);
                  //  hsforSP.Add("@Branch_Chk_Default", ChkDefault.Checked);
                    hsforSP.Add("@Status", "TRUE");
                    hsforSP.Add("@Modified_By", userid);
                    hsforSP.Add("@Modified_Date", DateTime.Now);
                    //hsforSP.Add("@status", endofSupportLife);
                    dt = dataaccess.ExecuteSP("Sp_Branch", hsforSP);
                    string title = "Update";
                    MessageBox.Show("Branch Updated Sucessfully",title);
                    clear();
                    AddParent();
                }
            }
               
            

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Checked;
            string logo;
            bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out Branch_id);
            if (isNum)
            {
            //    lbl_Branch.Text = "EDIT BRANCH";
                //pt.X = 450; pt.Y = 20;
                //lbl_Branch.Location = pt;
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new DataTable();
                hsforSP.Add("@Trans", "SELECT");
                hsforSP.Add("@Branch_ID", Branch_id);
                dt = dataaccess.ExecuteSP("Sp_Branch", hsforSP);
                int sample = int.Parse(dt.Rows[0]["State_Address_ID"].ToString());
                ddl_Company.SelectedValue = dt.Rows[0]["Company_Id"].ToString();

                compid = int.Parse(dt.Rows[0]["Company_Id"].ToString()); 

                txt_Branchname.Text = dt.Rows[0]["Branch_Name"].ToString();
                branchname = dt.Rows[0]["Branch_Name"].ToString();

                txt_Branch_Code.Text = dt.Rows[0]["Branch_Code"].ToString();
                ddl_Branch_country.SelectedValue = dt.Rows[0]["Country_ID"].ToString();
                ddl_Branch_state.SelectedValue = dt.Rows[0]["State_Address_ID"].ToString();
                ddl_Branch_district.SelectedValue = dt.Rows[0]["District_Id"].ToString();
                txt_Branch_address.Text = dt.Rows[0]["Branch_Address"].ToString();
                txt_Branch_city.Text = dt.Rows[0]["Branch_City"].ToString();
                txt_Branch_email.Text = dt.Rows[0]["Branch_Email"].ToString();
                txt_Branch_fax.Text = dt.Rows[0]["Branch_Fax"].ToString();
                txt_Branch_phono.Text = dt.Rows[0]["Branch_Phone"].ToString();
                txt_branch_Pincode.Text = dt.Rows[0]["Branch_Pincode"].ToString();
                txt_Branch_website.Text = dt.Rows[0]["Branch_Web"].ToString();
                if (dt.Rows[0]["Modifiedby"].ToString() != "")
                {
                    lbl_RecordAddedBy.Text = dt.Rows[0]["Modifiedby"].ToString();
                    lbl_RecordAddedOn.Text = dt.Rows[0]["Modified_Date"].ToString();
                }
                else if (dt.Rows[0]["Modifiedby"].ToString() == "")
                {
                    lbl_RecordAddedBy.Text = dt.Rows[0]["Insertedby"].ToString();
                    lbl_RecordAddedOn.Text = dt.Rows[0]["Instered_Date"].ToString();
                }
                Checked = dt.Rows[0]["SetDefault"].ToString();
                if (Checked == "True")
                {
                    ChkDefault.Checked = true;
                }
                else if (Checked == "False")
                {
                    ChkDefault.Checked = false;
                }
                if (Branch_id != 0)
                {
                    btn_Save.Text = "Edit";
                }
                else
                {
                    btn_Save.Text = "Add";
                }
            }
        }

        private bool Validation()
        {
            string title = "Validation!";

            if (ddl_Company.SelectedIndex==-1)
            {
                MessageBox.Show("Please kindly Select Company Name", title);
                txt_Branchname.Focus();
                return false;
            }

            if (txt_Branchname.Text == "")
            {
                MessageBox.Show("Enter Branch Name",title);
                txt_Branchname.Focus();
                return false;
            }
            else if (txt_Branch_Code.Text == "")
            {
                MessageBox.Show("Enter Branch Code",title);
                txt_Branch_Code.Focus();
                return false;
            }
            else if (txt_Branch_address.Text == "")
            {
                MessageBox.Show("Enter Branch Address",title);
                txt_Branch_address.Focus();
                return false;
            }
            else if (ddl_Branch_country.Text == "SELECT")
            {
                MessageBox.Show("Select Branch Country",title);
                ddl_Branch_country.Focus();
                return false;
            }
            else if (ddl_Branch_state.Text == "SELECT" || ddl_Branch_state.Text=="")
            {
                MessageBox.Show("Select Branch State",title);
                ddl_Branch_state.Focus();
                return false;
            }
            else if (ddl_Branch_district.Text=="SELECT" || ddl_Branch_district.Text=="")
            {
                MessageBox.Show("Select Branch District",title);
                ddl_Branch_district.Focus();
                return false;
            }
            else if (txt_Branch_city.Text == "")
            {
                MessageBox.Show("Enter Branch City",title);
                txt_Branch_city.Focus();
                return false;
            }
            else if (txt_branch_Pincode.Text == "")
            {
                MessageBox.Show("Enter Branch Pincode",title);
                txt_branch_Pincode.Focus();
                return false;
            }
            else if (txt_Branch_phono.Text == "")
            {
                MessageBox.Show("Enter Branch Phone number",title);
                txt_Branch_phono.Focus();
                return false;
            }
            else if (txt_Branch_fax.Text == "")
            {
                MessageBox.Show("Enter Branch Fax number",title);
                txt_Branch_fax.Focus();
                //txt_Branch_fax.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (txt_Branch_email.Text == "")
            {
                MessageBox.Show("Enter Branch Email Address",title);
                txt_Branch_email.Focus();
                return false;
            }
            else if (txt_Branch_website.Text == "")
            {
                MessageBox.Show("Enter Branch Website Name",title);
                txt_Branch_website.Focus();
                return false;
            }
            
            //else if (ChkDefault.Checked==false)
            //{
            //    MessageBox.Show("Set As Default Check Box Should Be Marked",title);
            //    ChkDefault.Focus();
            //    return false;
            //}
            //Hashtable ht = new Hashtable();
            //DataTable dt = new DataTable();
            //ht.Add("@Trans", "BRANCHNAME");
           
            //dt = dataaccess.ExecuteSP("Sp_Branch", ht);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (txt_Branchname.Text == dt.Rows[i]["Branch_Name"].ToString())
            //    {
            //        MessageBox.Show("Branch Name Already Exist");
            //        return false;
                    
            //    }
            //}
            return true;
        }

        private bool Duplicate_Branch_Name()
        {
            //Hashtable ht = new Hashtable();
            //DataTable dt = new System.Data.DataTable();
            //ht.Add("@Trans", "SELECTGRID");
            //dt = dataaccess.ExecuteSP("Sp_Branch", ht);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    //treeView1.Nodes[0].Nodes.Add(dt.Rows[i]["Branch_ID"].ToString(), dt.Rows[i]["Branch_Name"].ToString());

            //    Branch_id=int.Parse(dt.Rows[i]["Branch_ID"].ToString());
            //}

             Companyid = int.Parse(ddl_Company.SelectedValue.ToString());

                Hashtable ht_Val = new Hashtable();
                DataTable dt_Val = new DataTable();
                ht_Val.Add("@Trans", "BRANCHNAME");
                ht_Val.Add("@Company_Id", Companyid);
                ht_Val.Add("@Branch_Name", txt_Branchname.Text);

                dt_Val = dataaccess.ExecuteSP("Sp_Branch", ht_Val);
                for (int i = 0; i < dt_Val.Rows.Count; i++)
                {
                    string brancname = dt_Val.Rows[i]["Branch_Name"].ToString();
                    int companyid=int.Parse(dt_Val.Rows[i]["Company_Id"].ToString());
                    string companyname = dt_Val.Rows[i]["Company_Name"].ToString();

                    if (txt_Branchname.Text.ToUpper() == brancname.ToUpper() && Companyid==companyid)
                    {
                        string title = "Duplicate Record!";
                      //  MessageBox.Show("Branch Name Already Exist",title);

                        MessageBox.Show(" * " + txt_Branchname.Text + " * " + " Branch Name Already Exist", title);
                        txt_Branchname.Text = "";
                        txt_Branchname.Select();
                        return false;

                    }
                }
            
                return true;
            
        }

        private bool Edit_duplic_Branch_Name()
        {
            Hashtable ht_Val = new Hashtable();
            DataTable dt_Val = new DataTable();
            ht_Val.Add("@Trans", "BRANCHNAME");
            ht_Val.Add("@Company_Id", ddl_Company.SelectedValue);
            ht_Val.Add("@Branch_Name", txt_Branchname.Text.ToUpper());

            dt_Val = dataaccess.ExecuteSP("Sp_Branch", ht_Val);
          
          //  if (dt_Val.Rows.Count > 0 && branchname.ToUpper() != txt_Branchname.Text.ToUpper() && compid==int.Parse(ddl_Company.SelectedValue.ToString()))

            if (dt_Val.Rows.Count > 0 && branchname.ToUpper() != txt_Branchname.Text.ToUpper())
            {

                //if (dt_Val.Rows.Count > 0 && branchname.ToUpper() != txt_Branchname.Text.ToUpper())
                //{

                    string title = "Duplicate Record!";
                    MessageBox.Show(" * " + txt_Branchname.Text + " * " + " Branch Name Already Exist", title);
                    txt_Branchname.Text = "";
                    txt_Branchname.Select();
                    return false;
               // }
            }
            else
            {
                if (dt_Val.Rows.Count > 0 && branchname.ToUpper() == txt_Branchname.Text.ToUpper() && compid != int.Parse(ddl_Company.SelectedValue.ToString()))
                {

                    string title = "Duplicate Record!";
                    MessageBox.Show(" * " + txt_Branchname.Text + " * " + " Branch Name Already Existed for this selected Company", title);
                    txt_Branchname.Text = "";
                    txt_Branchname.Select();
                    return false;
                }
            }
          
            return true;
    }
        protected void clear()
        {
           // lbl_Branch.Text = "BRANCH";
           // pt.X=475;pt.Y=20;
           // lbl_Branch.Location=pt;
             Branch_id = 0;
            txt_Branchname.Text = "";
            txt_Branch_Code.Text = "";
            txt_Branch_address.Text = "";
            txt_Branch_city.Text = "";
            txt_Branch_Code.Text = "";
            txt_Branch_Code.Enabled = true;
            txt_Branch_email.Text = "";
            txt_Branch_fax.Text = "";
            txt_Branch_phono.Text = "";
            txt_branch_Pincode.Text = "";
            txt_Branch_website.Text = "";
            btn_Save.Text = "Add";
          //  ChkDefault.Checked = false;
            txt_Branchname.BackColor = System.Drawing.Color.White;
            ddl_Branch_district.BackColor = System.Drawing.Color.White;
            ddl_Branch_country.BackColor = System.Drawing.Color.White;
            ddl_Branch_state.BackColor = System.Drawing.Color.White;
            ddl_Branch_district.DataSource = null;   
            ddl_Branch_country.SelectedValue = 0;
            ddl_Branch_state.SelectedValue = 0;
            lbl_RecordAddedBy.Text = username;
            lbl_RecordAddedOn.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            dbc.BindState1(ddl_Branch_state, int.Parse(ddl_Branch_country.SelectedValue.ToString()));
            bindDistrict();
            ddl_Company.SelectedIndex = 0;
            AddParent();
           
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
           // treeView1_AfterSelect(sender,e);
        }

        
        private void AddParent()
        {

            string sKeyTemp = "";
            treeView1.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
            ht.Add("@Trans", "SELECTGRID");
           // sKeyTemp = "Branch";
            sKeyTemp = "Branches";
            dt = dataaccess.ExecuteSP("Sp_Branch", ht);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
                
               // sKeyTemp = dt.Rows[i]["Branch_Name"].ToString();
                //parentnode=treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
               // AddChilds(parentnode,sKeyTemp);
                //sKeyTemp = "Branches";
                treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(sKeyTemp);
            //}
        }

        private void AddChilds(string sKey)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "SELECTGRID");
            dt = dataaccess.ExecuteSP("Sp_Branch", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                treeView1.Nodes[0].Nodes.Add(dt.Rows[i]["Branch_ID"].ToString(), dt.Rows[i]["Branch_Name"].ToString());
            }
        }

        private void AddChilds(TreeNode parentnode,string sKey)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "SELECTGRID");
            dt = dataaccess.ExecuteSP("Sp_Branch", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                treeView1.Nodes[0].Nodes.Add(dt.Rows[i]["Branch_ID"].ToString() , dt.Rows[i]["Branch_Name"].ToString());
            }
        }

        private void ddl_Branch_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_Branch_country.SelectedIndex > 0)
            //{
            //    dbc.BindState1(ddl_Branch_state, int.Parse(ddl_Branch_country.SelectedValue.ToString()));
            //}
            //else
            //{
            //   // ddl_Branch_state.SelectedIndex = 0;
            //}

            if (ddl_Branch_country.SelectedIndex > 0)
            {
                int val = int.Parse(ddl_Branch_country.SelectedValue.ToString());
                if (val == 99)
                {
                    dbc.BindState1(ddl_Branch_state, int.Parse(ddl_Branch_country.SelectedValue.ToString()));
                }
                else if (val == 228)
                {
                    dbc.Bind_State_BY_Country(ddl_Branch_state, int.Parse(ddl_Branch_country.SelectedValue.ToString()));
                }
            }


        }

        private void ddl_Branch_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_Branch_state.SelectedIndex > 0)
            //{
            //    dbc.BindDistrict(ddl_Branch_district, int.Parse(ddl_Branch_state.SelectedValue.ToString()));
            //}
            //else
            //{
            //    //ddl_Branch_district.SelectedIndex = 0;
            //    //dbc.BindDistrict(ddl_Branch_district, int.Parse(ddl_Branch_state.SelectedValue.ToString()));
            //}

            if (ddl_Branch_state.SelectedIndex > 0)
            {
                int value = int.Parse(ddl_Branch_country.SelectedValue.ToString());
                if (value == 99)
                {
                    dbc.BindDistrict(ddl_Branch_district, int.Parse(ddl_Branch_state.SelectedValue.ToString()));
                }
                else
                {
                    if (value == 228)
                    {
                        dbc.BindCounty_StateWise(ddl_Branch_district, int.Parse(ddl_Branch_state.SelectedValue.ToString()));
                    }
                }

            }



        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
           
                DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (Branch_id != 0)
                    {
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    //  Branch_id = int.Parse(treeView1.SelectedNode.Text.Substring(0, 4).ToString());
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@Branch_ID", Branch_id);
                    dtdelete = dataaccess.ExecuteSP("Sp_Branch", htdelete);
                    MessageBox.Show("Branch Successfully Deleted");
                    int count = dtdelete.Rows.Count;
                    clear();
                    AddParent();
                    }
                    else
                    {
                        string title = "Select!";
                        MessageBox.Show("Please Select Valid Branch Name",title);
                        treeView1.Focus();
                    }
                }
                clear();
        
        }

        

        private void txt_Branch_email_Leave(object sender, EventArgs e)
        {
            //Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            //if (myRegularExpression.IsMatch(txt_Branch_email.Text))
            //{
            //    //valid e-mail
            //}
            //else
            //{
            //    string title = "Validation!";
            //    MessageBox.Show("Email Address Not Valid",title);
            //}

            Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

            // Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (myRegularExpression.IsMatch(txt_Branch_email.Text))
            {
                //valid e-mail
            }
            else
            {
                MessageBox.Show("Email Address Not Valid");
            }

        }

        private void txt_branch_Pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
          //  e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_branch_Pincode.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }

            if (pinCode.IsMatch(txt_branch_Pincode.Text) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("PinCode  Number Maximum 5 or 6 digits.");
            }
        }

        private void txt_Branch_phono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branch_phono.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

            var countChar = txt_Branch_phono.Text;
            if (countChar.Length == 20 && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Phone Number Maximum 20 digits");
            }

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }
        }

        private void txt_Branch_fax_KeyPress(object sender, KeyPressEventArgs e)
        {
          //  e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branch_fax.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }

            if (txt_Branch_fax.Text.Length == 20 && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Fax Number Maximum 20 digits");
            }

        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 189; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 50;
            add_pt.X = 5; add_pt.Y = 445;
            comp_pt1.X = 200; comp_pt1.Y = 60;
            add_pt1.X = 200; add_pt1.Y = 450;
            branch_lbl.X = 275; branch_lbl.Y = 20;
            branch_lbl1.X = 475; branch_lbl1.Y = 20;
            create_bran.X = 160; create_bran.Y = 565;
            create_bran1.X = 350; create_bran1.Y = 565;
            del_bran.X = 295; del_bran.Y = 565;
            del_bran1.X = 485; del_bran1.Y = 565;
            clear_btn.X = 430; clear_btn.Y = 565;
            clear_btn1.X = 620; clear_btn1.Y = 565;
            form_pt.X = 350; form_pt.Y = 20;
            form1_pt.X = 180; form1_pt.Y = 20;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_Branch.Location = branch_lbl;
                btn_Save.Location = create_bran;
                btn_Delete.Location = del_bran;
                btn_Cancel.Location = clear_btn;
                grp_Branch_det.Location = comp_pt;
                grp_Add_det.Location = add_pt;
                Create_Branch.ActiveForm.Width = 690;
                Create_Branch.ActiveForm.Location = form_pt;
               // btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_Branch.Location = branch_lbl1;
                btn_Save.Location = create_bran1;
                btn_Delete.Location = del_bran1;
                btn_Cancel.Location = clear_btn1;
                grp_Branch_det.Location = comp_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Branch.ActiveForm.Width = 900;
                Create_Branch.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

        private void txt_Branchname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branchname.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
            if ((char.IsNumber(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Numbers are not allowed");
                }
            }
         
        }

        private void txt_Branch_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branch_city.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Branch_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branch_Code.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Branch_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branch_address.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void ddl_Branch_country_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((char.IsWhiteSpace(e.KeyChar)) && ddl_Branch_country.SelectedText.Length == 0) //for block first whitespace 
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("White Space not allowed for First Charcter");
            //    }
            //}
        }

        private void ddl_Branch_state_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((char.IsWhiteSpace(e.KeyChar)) && ddl_Branch_state.SelectedText.Length == 0) //for block first whitespace 
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("White Space not allowed for First Charcter");
            //    }
            //}
        }

        private void ddl_Branch_district_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && ddl_Branch_district.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Branch_email_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branch_email.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

            if ((txt_Branch_email.Text.Length == 50) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Email is Maximum 50 Charcters");
            }
        }

        private void txt_Branch_website_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Branch_website.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
           
        }

        private void txt_Branch_email_MouseClick(object sender, MouseEventArgs e)
        {
            //Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            //if (myRegularExpression.IsMatch(txt_Branch_email.Text))
            //{
            //    //valid e-mail
            //}
            //else
            //{
            //    string title = "Validation!";
            //    MessageBox.Show("Email Address Not Valid", title);
            //}
        }

        private void ddl_Branch_country_MouseClick(object sender, MouseEventArgs e)
        {

            //if (!(char.IsLetter(ddl_Branch_country.sele)))
            //{

            //}

        }

        private void txt_Branch_website_TabIndexChanged(object sender, EventArgs e)
        {
            btn_Save.Select();
        }

        private void txt_branch_Pincode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
                (sender as TextBox).Paste();
        }

        private void txt_branch_Pincode_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 6)
            {
                MessageBox.Show("Enter 5 or 6 digit Number");
                // SendKeys.Send("{Tab}");
            }
        }

        private void txt_Branch_fax_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 20)
            {
                MessageBox.Show("Enter Maximum 20 digit Number");
                // SendKeys.Send("{Tab}");
            }
        }

        private void txt_Branch_phono_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 20)
            {
                MessageBox.Show("Enter Maximum 20 digit Number");
                // SendKeys.Send("{Tab}");
            }
        }

        private void txt_Branch_website_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Branch_email_TextChanged(object sender, EventArgs e)
        {

        }

   

    

   
        
    }
}
