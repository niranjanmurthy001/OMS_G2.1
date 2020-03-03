using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Ordermanagement_01
{
    public partial class Create_Client : Form
    {
        int Client_id;
        int userid = 0;
        int companyid, branchid;
        byte[] bimage;
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, client_lbl, client_lbl1, create_cli1, del_cli1, create_cli, del_cli, clear_btn, clear_btn1;

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dtse = new DataTable();
        string Email_ID;
        object Client_insert_Id;
        Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
        int Client_Mail_Id;
        string username;
        public Regex pinCode = new Regex(@"^\d{6,}$", RegexOptions.Compiled);
        public Regex FaxNum = new Regex(@"^\d{15,}$", RegexOptions.Compiled);
        public Regex phoneno = new Regex(@"^\d{10,}$", RegexOptions.Compiled);
        public Regex CompName = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);
        public Regex CompSlogan = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);
        public Regex City = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);

        public Create_Client(int user_id, string UserName)
        {
            InitializeComponent();

            dbc.BindCompany(ddl_Company);
            dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));
            dbc.BindCountry(Ddl_Client_Country);
            if (Ddl_Client_Country.SelectedIndex > 0)
            {
                dbc.BindState1(Ddl_Client_State, int.Parse(Ddl_Client_Country.ToString()));
            }
            if (Ddl_Client_State.SelectedIndex > 0)
            {
                dbc.BindDistrict(ddl_Client_district, int.Parse(Ddl_Client_State.SelectedValue.ToString()));
            }
            userid = user_id;
            username = UserName;
        }

        private void Create_Client_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                //btn_treeview.Left = Width - 60;
                lbl_ClientRefNo.Visible = false;
                ListofClientNumbers.Visible = false;
                ListofClientNumbers.Enabled = false;
                pnlSideTree.Visible = true;
                txt_ClientNumber.Enabled = true;
                AddParent();
                lbl_Record_Addedby.Text = "";
                lbl_Record_AddedDate.Text = " ";
                dbc.BindCompany(ddl_Company);
                dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));
                dbc.BindCountry(Ddl_Client_Country);
                GetMaximumClientNumber();
                rbtn_Enable.Checked = true;
                BindClientNumbers();
                textBoximage.Enabled = true;

                lbl_Record_Addedby.Text = username;
                lbl_Record_AddedDate.Text = DateTime.Now.ToString();
                ddl_Company.Select();

                textBoximage.Enabled = false;
                txt_CostTATExcel.Enabled = false;
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
        private bool BindClientNumbers()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                ht.Add("@Trans", "ClientRefValues");
                ht.Add("@Client_Number", txt_ClientNumber.Text.ToString());
                dt = dataaccess.ExecuteSP("Sp_Client", ht);
                if (dt.Rows.Count > 0)
                {
                    ListofClientNumbers.DisplayMember = "Number";
                    ListofClientNumbers.ValueMember = "Number";
                    ListofClientNumbers.DataSource = dt;

                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    // MessageBox.Show("Available ClientNumber's Not Found For These Particular Client Number");
                    //ListofClientNumbers.Visible = false;
                    //lbl_ClientRefNo.Visible = false;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                // MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void AddParent()
        {
            string sKeyTemp = "";
            tree_Client.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
            string companyid;
            ht.Add("@Trans", "SELECTGRID");
            sKeyTemp = "Branches";
            dt = dataaccess.ExecuteSP("Sp_Branch", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                companyid = dt.Rows[i]["Branch_ID"].ToString();
                sKeyTemp = dt.Rows[i]["Branch_Name"].ToString();
                parentnode = tree_Client.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, companyid);
            }

        }

        private void AddChilds(TreeNode parentnode, string sKey)
        {
            string sKeyTemp1 = "";
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            //  TreeNode parentnode1;
            string clientid;
            ht.Add("@Trans", "Branch_cli");
            ht.Add("@Branch_ID", sKey);
            // sKeyTemp1 = "Client";
            dt = dataaccess.ExecuteSP("Sp_Tree_Orders", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp1 = dt.Rows[i]["Client_Name"].ToString();
                clientid = dt.Rows[i]["Client_Id"].ToString();
                parentnode.Nodes.Add(clientid, sKeyTemp1);
                //NextChild(parentnode1, sKeyTemp1);
            }
        }

        //private void NextChild(TreeNode parentnode1, string sKey)
        //{
        //    string sKeyTemp2 = "";
        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    string clientid;
        //    ht.Add("@Trans", "Branch_cli");
        //  //  ht.Add("@Client_Id", sKey);
        //    dt = dataaccess.ExecuteSP("Sp_Tree_Orders", ht);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sKeyTemp2 = dt.Rows[i]["Client_Name"].ToString();
        //        clientid = dt.Rows[i]["Client_Id"].ToString();
        //     //   string ckey= dt.Rows[i]["Client_Id"].ToString();
        //        parentnode1.Nodes.Add(clientid, sKeyTemp2);
        //    }
        //}

        private void txt_company_fax_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 6)
            {
                MessageBox.Show("Enter 5 or 6 digit Number");
                // SendKeys.Send("{Tab}");
            }
        }

        private void txt_company_phono_TextChanged(object sender, EventArgs e)
        {

        }

        private void ddl_Company_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_branchname.Focus();
            }
        }

        private void ddl_branchname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_ClientName.Focus();
            }
        }

        private void txt_ClientNumber_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txt_ClientName.Focus();
            }

            // else if((txt_ClientNumber.Text.Length >4 || txt_ClientNumber.Text.Length<5) && (e.KeyValue&& e.KeyValue && (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete))
        }

        private void txt_ClientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_Code.Focus();
            }
        }

        private void txt_Client_address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_city.Focus();
            }
        }

        private void Ddl_Client_Country_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ddl_Client_State.Focus();
            }
        }

        private void Ddl_Client_State_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Client_district.Focus();
            }
        }

        private void ddl_Client_district_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_Pincode.Focus();
            }
        }

        private void txt_Client_city_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ddl_Client_Country.Focus();
            }
        }

        private void txt_Client_Pincode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_phono.Focus();
            }
        }

        private void txt_Client_phono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_fax.Focus();
            }
        }

        private void txt_Client_fax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_email.Focus();
            }
        }

        private void txt_Client_email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_website.Focus();
            }
        }

        private void txt_Client_website_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void Ddl_Client_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ddl_Client_Country.SelectedIndex > 0)
            {
                dbc.BindState1(Ddl_Client_State, int.Parse(Ddl_Client_Country.SelectedValue.ToString()));
            }
        }

        private void Ddl_Client_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ddl_Client_State.SelectedIndex > 0)
            {
                dbc.BindDistrict(ddl_Client_district, int.Parse(Ddl_Client_State.SelectedValue.ToString()));
            }
        }

        private bool Validation()
        {
            string title = "Validation!";
            if (txt_ClientName.Text == "")
            {
                MessageBox.Show("Enter Client Name", title);
                txt_ClientName.Focus();
                //    txt_ClientName.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (Ddl_Client_Country.Text == "SELECT")
            {
                MessageBox.Show("Select Country", title);
                Ddl_Client_Country.Focus();
                //  Ddl_Client_Country.BackColor = System.Drawing.Color.Red;
                return false;
            }

            else if (Ddl_Client_State.Text == "SELECT" || Ddl_Client_State.Text == "")
            {
                MessageBox.Show("Select  State", title);
                Ddl_Client_State.Focus();
                Ddl_Client_State.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (ddl_Client_district.Text == "SELECT" || ddl_Client_district.Text == "")
            {
                MessageBox.Show("Select  District", title);
                ddl_Client_district.Focus();
                ddl_Client_district.BackColor = System.Drawing.Color.Red;
                return false;
            }


            else if (txt_Client_phono.Text == "")
            {
                MessageBox.Show("Enter Phone number", title);
                ddl_Client_district.Focus();
                ddl_Client_district.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (txt_Client_address.Text == "")
            {
                MessageBox.Show("Enter  Email", title);
                //ddl_Client_district.Focus();
                //ddl_Client_district.BackColor = System.Drawing.Color.Red;
                txt_Client_address.Focus();
                return false;
            }
            else if (txt_Client_email.Text == "")
            {
                MessageBox.Show("Enter  Email", title);
                //ddl_Client_district.Focus();
                //ddl_Client_district.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (txt_ClientNumber.Text.Length == 0 && txt_ClientNumber.Text.Length <= 4)
            {
                MessageBox.Show(" Enter a Valid Client Number", title);
                txt_ClientNumber.Focus();
                return false;
            }

            return true;
        }

        private bool Validate_Client_Name()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "CLIENTNAME");
            dt = dataaccess.ExecuteSP("Sp_Client", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (txt_ClientName.Text == dt.Rows[i]["Client_Name"].ToString())
                {
                    string title = "Exist!";
                    MessageBox.Show("Client Name Already Exist", title);
                    txt_ClientName.Focus();
                    return false;
                }
            }
            return true;
        }

        protected void clear()
        {
            Client_id = 0;
            Client_Mail_Id = 0;
            txt_Client_address.Text = "";
            txt_Client_Code.Text = "";
            txt_Client_city.Text = "";
            txt_Client_email.Text = "";
            txt_Client_fax.Text = "";
            txt_Client_phono.Text = "";
            txt_Client_Pincode.Text = "";
            txt_Client_website.Text = "";
            txt_ClientName.Text = "";
            txt_ClientNumber.Text = "";
            txt_ClientName.BackColor = System.Drawing.Color.White;
            //  ddl_branchname.SelectedIndex = 0;
            // dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));

            Ddl_Client_Country.SelectedValue = 0;
            Ddl_Client_Country.BackColor = System.Drawing.Color.White;
            Ddl_Client_State.SelectedValue = 0;
            Ddl_Client_State.BackColor = System.Drawing.Color.White;
            ddl_Client_district.DataSource = null;
            ddl_Client_district.BackColor = System.Drawing.Color.White;
            btn_Save.Text = "Add";
            Client_image.Image = null;
            lbl_Record_Addedby.Text = username;
            textBoximage.Text = "";
            //  textBoximage.Enabled=false;
            lbl_Record_AddedDate.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            if (rbtn_Enable.Checked)
            {
                rbtn_Enable.Checked = false;
            }
            if (rbtn_Disable.Checked)
            {
                rbtn_Disable.Checked = false;
            }
            GetMaximumClientNumber();
            grd_Email.Rows.Clear();
            ddl_Company.SelectedIndex = 0;
            dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));

            GetMaximumClientNumber();

            AddParent();
            Grd_Mail_Bind();
            ddl_Company.Select();
        }

        private bool duplic_Email()
        {
            string emailid;

            for (int k = 0; k < grd_Email.Rows.Count - 1; k++)
            {
                Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();

                for (int j = 0; j < k; j++)
                {
                    // emailid = dt_Check.Rows[j]["Email-ID"].ToString();
                    emailid = grd_Email.Rows[j].Cells[1].Value.ToString();
                    if (Email_ID == emailid)
                    {
                        string title = "Exist!";
                        MessageBox.Show("Alternative Email Already Exist", title);
                        grd_Email.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                        return false;

                    }
                }
            }
            for (int k = 0; k < grd_Email.Rows.Count - 1; k++)
            {
                Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();
                Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                if (myRegularExpression.IsMatch(Email_ID))
                {
                    //valid e-mail
                }
                else
                {
                    MessageBox.Show("Alternative Email Address Not Valid");
                    grd_Email.Rows[k].Cells[1].Selected = CanSelect;
                    return false;
                }
                for (int j = 0; j < k; j++)
                {
                    emailid = grd_Email.Rows[j].Cells[1].Value.ToString();
                    Regex myRegularExpression1 = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                    if (myRegularExpression1.IsMatch(emailid))
                    {
                        //valid e-mail
                    }
                    else
                    {
                        MessageBox.Show("Alternative Email Address Not Valid");
                        grd_Email.Rows[j].Cells[1].Selected = CanSelect;
                        return false;
                    }
                }
            }
            return true;
        }

        private void Grd_Mail_Insert()
        {
            //  Client_Mail_Id = int.Parse(grd_Email.Rows[i].Cells[0].Value.ToString());
            for (int i = 0; i < grd_Email.Rows.Count; i++)
            {
                if (grd_Email.Rows[i].Cells[1].Value != null)
                {
                    if (grd_Email.Rows[i].Cells[1].Value.ToString() != "")
                    {
                        if (Client_Mail_Id == 0 && btn_Save.Text == "Add")
                        {
                            Hashtable ht_Mail = new Hashtable();
                            DataTable dt_Mail = new DataTable();
                            ht_Mail.Add("@Trans", "INSERT");
                            ht_Mail.Add("@Client_Id", Client_insert_Id); // Client_insert_Id
                            ht_Mail.Add("@Email_ID", grd_Email.Rows[i].Cells[1].Value.ToString());
                            ht_Mail.Add("@Inserted_By", userid);
                            // ht_Mail.Add("@Inserted_By", userid);
                            dt_Mail = dataaccess.ExecuteSP("Sp_Client_Wise_Email", ht_Mail);
                            Client_Mail_Id = 0;
                        }
                        //else if (Client_Mail_Id != 0)
                        //{
                        //    Hashtable ht_Update_Mail = new Hashtable();
                        //    DataTable dt_Update_Mail = new DataTable();
                        //    ht_Update_Mail.Add("@Trans", "INSERT");
                        //    ht_Update_Mail.Add("@Client_Mail_Id", Client_Mail_Id);
                        //    ht_Update_Mail.Add("@Client_Id", Client_id);
                        //    ht_Update_Mail.Add("@Email_ID", grd_Email.Rows[i].Cells[1].Value.ToString());
                        //    ht_Update_Mail.Add("@Modified_By", userid);
                        //    dt_Update_Mail = dataaccess.ExecuteSP("Sp_Client_Wise_Email", ht_Update_Mail);
                        //}
                    }
                }

            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //if (Ddl_Client_Country.SelectedValue.ToString() != "SELECT" && Ddl_Client_State.SelectedValue.ToString() != "SELECT" && ddl_Client_district.SelectedValue.ToString() != "SELECT")
            //{

            if (Client_id == 0 && Validation() != false && Validate_Client_Name() != false && btn_Save.Text == "Add" && duplic_Email() != false)
            {


                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new DataTable();
                DataTable dt = new DataTable();
                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                htinsert.Add("@Trans", "INSERT");
                //htinsert.Add("@Company_Id", int.Parse(ddl_Company.SelectedValue.ToString()));
                //htinsert.Add("@Branch_ID", branchid);ddl_branchname
                htinsert.Add("@Branch_ID", ddl_branchname.SelectedValue);
                htinsert.Add("@Client_Number", txt_ClientNumber.Text);
                htinsert.Add("@Client_Name", txt_ClientName.Text);
                htinsert.Add("@Client_Photo", bimage);
                htinsert.Add("@Client_Code", txt_Client_Code.Text);
                htinsert.Add("@Client_Country", int.Parse(Ddl_Client_Country.SelectedValue.ToString()));
                htinsert.Add("@Client_State", int.Parse(Ddl_Client_State.SelectedValue.ToString()));
                htinsert.Add("@Client_District", int.Parse(ddl_Client_district.SelectedValue.ToString()));
                htinsert.Add("@Client_City", txt_Client_city.Text);
                htinsert.Add("@Client_Address", txt_Client_address.Text);
                htinsert.Add("@Client_Phone", txt_Client_phono.Text);
                htinsert.Add("@Client_Pin", txt_Client_Pincode.Text);
                htinsert.Add("@Client_Fax", txt_Client_fax.Text);
                htinsert.Add("@Client_Email", txt_Client_email.Text);
                htinsert.Add("@Client_Web", txt_Client_website.Text);
                htinsert.Add("@Inserted_By", userid);
                htinsert.Add("@Inserted_date", date);
                htinsert.Add("@status", "True");

                Client_insert_Id = dataaccess.ExecuteSPForScalar("Sp_Client", htinsert);

                Grd_Mail_Insert();
                string title = "Insert";
                MessageBox.Show("Client Created Sucessfully", title);
                clear();
                GetMaximumClientNumber();
                Client_id = 0;
                AddParent();
            }
            else if (Client_id != 0 && Validation() != false && btn_Save.Text == "Edit" && duplic_Email() != false)
            {
                //Update

                Hashtable htupdate = new Hashtable();
                DataTable dtupdate = new DataTable();
                DataTable dt = new DataTable();
                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                htupdate.Add("@Trans", "UPDATE");
                htupdate.Add("@Client_Id", Client_id);
                //htupdate.Add("@Company_Id", int.Parse(ddl_Company.SelectedValue.ToString()));
                htupdate.Add("@Branch_ID", branchid);
                htupdate.Add("@Client_Number", txt_ClientNumber.Text);
                htupdate.Add("@Client_Name", txt_ClientName.Text);
                htupdate.Add("@Client_Code", txt_Client_Code.Text);
                htupdate.Add("@Client_Photo", bimage);
                htupdate.Add("@Client_Country", int.Parse(Ddl_Client_Country.SelectedValue.ToString()));
                htupdate.Add("@Client_State", int.Parse(Ddl_Client_State.SelectedValue.ToString()));
                htupdate.Add("@Client_District", int.Parse(ddl_Client_district.SelectedValue.ToString()));
                htupdate.Add("@Client_City", txt_Client_city.Text);
                htupdate.Add("@Client_Address", txt_Client_address.Text);
                htupdate.Add("@Client_Phone", txt_Client_phono.Text);
                htupdate.Add("@Client_Pin", txt_Client_Pincode.Text.ToString());
                htupdate.Add("@Client_Fax", txt_Client_fax.Text);
                htupdate.Add("@Client_Email", txt_Client_email.Text);
                htupdate.Add("@Client_Web", txt_Client_website.Text);
                htupdate.Add("@Modified_By", userid);
                htupdate.Add("@Modified_Date", date);
                htupdate.Add("@status", "True");


                if (rbtn_Enable.Checked == true)
                {
                    htupdate.Add("@Client_Status", "True");
                }
                else if (rbtn_Disable.Checked == true)
                {

                    htupdate.Add("@Client_Status", "False");
                }
                dtupdate = dataaccess.ExecuteSP("Sp_Client", htupdate);
                Grd_Mail_Update();
                string title = "Update";
                MessageBox.Show("Client Updated Sucessfully", title);
                clear();
                GetMaximumClientNumber();

                AddParent();
                Grd_Mail_Bind();
                Client_id = 0;
                GetMaximumClientNumber();
                grd_Email.Rows.Clear();
            }
        }

        protected void GetMaximumClientNumber()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();

            htselect.Add("@Trans", "MAXCLIENTNUMBER");
            dtselect = dataaccess.ExecuteSP("Sp_Client", htselect);
            if (dtselect.Rows.Count > 0)
            {
                txt_ClientNumber.Text = dtselect.Rows[0]["CLIENTNUMBER"].ToString();
            }

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
            grd_Email.Rows.Clear();
        }

        private void ddl_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Company.SelectedIndex > 0)
            {
                dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));
            }

        }

        private void txt_ClientNumber_TextChanged(object sender, EventArgs e)
        {

            string Client_Numbers = txt_ClientNumber.Text.Trim().ToString();

            if (Client_Numbers.Length > 0 && Client_Numbers.Length >= 4)
            {
                string value = txt_ClientNumber.Text.Trim().ToString();
                bool Validate_Result = Validate_Client_Number_Length(Client_Numbers);
                if (Validate_Result == false)
                {


                    txt_ClientNumber.Text = value;

                }
                else
                {
                    BindClientNumbers();

                }

            }

            else
            {
                //MessageBox.Show("Please Enter A Valid Client Number");
                txt_ClientNumber.Focus();
            }
            if (txt_ClientNumber.Text.Length == 0)
            {
                ListofClientNumbers.Visible = false;
                lbl_ClientRefNo.Visible = false;


            }





        }

        private void btn_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBoximage.Enabled = true;
                textBoximage.Text = open.FileName;
                string image = textBoximage.Text;
                Bitmap bmp = new Bitmap(image);
                if (textBoximage.Text != "")
                {
                    FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read);
                    bimage = new byte[fs.Length];
                    fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    Client_image.Image = GetDataToImage((byte[])bimage);
                }
            }
            else
            {
                textBoximage.Enabled = false;
            }

        }

        public Image GetDataToImage(byte[] bimage)
        {
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                return imgConverter.ConvertFrom(bimage) as Image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image not uploaded");
                return null;
            }
        }

        private void btn_Slide_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 186; pt1.Y = 0;
            comp_pt.X = 8; comp_pt.Y = 30;
            add_pt.X = 8; add_pt.Y = 455;
            comp_pt1.X = 200; comp_pt1.Y = 30;
            add_pt1.X = 200; add_pt1.Y = 455;
            client_lbl.X = 285; client_lbl.Y = 10;
            client_lbl1.X = 470; client_lbl1.Y = 10;
            create_cli.X = 150; create_cli.Y = 585;
            create_cli1.X = 300; create_cli1.Y = 585;
            del_cli.X = 295; del_cli.Y = 585;
            del_cli1.X = 445; del_cli1.Y = 585;
            clear_btn.X = 440; clear_btn.Y = 585;
            clear_btn1.X = 590; clear_btn1.Y = 585;
            form_pt.X = 350; form_pt.Y = 0;
            form1_pt.X = 200; form1_pt.Y = 0;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                //btn_treeview.Location = pt;

                lbl_Client.Location = client_lbl;
                btn_Save.Location = create_cli;
                btn_Delete.Location = del_cli;
                btn_Cancel.Location = clear_btn;
                grp_Client_det.Location = comp_pt;
                grp_Add_det.Location = add_pt;
                Create_Company.ActiveForm.Width = 1057;
                Create_Company.ActiveForm.Location = form_pt;
                // btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                // btn_treeview.Location = pt1;

                lbl_Client.Location = client_lbl1;
                btn_Save.Location = create_cli1;
                btn_Delete.Location = del_cli1;
                btn_Cancel.Location = clear_btn1;
                grp_Client_det.Location = comp_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Company.ActiveForm.Width = 1247;
                Create_Company.ActiveForm.Location = form1_pt;
                //  btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\left.png");
            }
            AddParent();
        }

        private void tree_Client_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lbl_ClientRefNo.Visible = false;
            ListofClientNumbers.Visible = false;
            BindClientNumbers();

            string Checked;

            string logo;
            dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));
            // Company_Id =int.Parse(tvwRightSide.SelectedNode.Text.Substring(0, 4));
            //Client_id = int.Parse(tree_Client.SelectedNode.Text.Substring(0, 4));

            txt_CostTATExcel.Enabled = false;

            if (txt_CostTATExcel.Enabled == false)
            {
                txt_CostTATExcel.Text = "";

            }


            bool isNum = Int32.TryParse(tree_Client.SelectedNode.Name, out Client_id);
            if (isNum)
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                ht.Add("@Trans", "SELECT_Client_ID");
                ht.Add("@Client_Id", Client_id);
                dt = dataaccess.ExecuteSP("Sp_Client", ht);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Client_id = int.Parse(dt.Rows[0]["Client_Id"].ToString());

                    ddl_Company.SelectedValue = int.Parse(dt.Rows[0]["Company_Id"].ToString());
                    companyid = int.Parse(dt.Rows[0]["Company_Id"].ToString());

                    ddl_branchname.SelectedValue = dt.Rows[0]["Branch_ID"].ToString();
                    branchid = int.Parse(dt.Rows[0]["Branch_ID"].ToString());

                    txt_ClientName.Text = dt.Rows[0]["Client_Name"].ToString();
                    txt_ClientNumber.Text = dt.Rows[0]["Client_Number"].ToString();
                    txt_Client_Code.Text = dt.Rows[0]["Client_Code"].ToString();
                    Ddl_Client_Country.SelectedValue = dt.Rows[0]["Client_Country"].ToString();
                    Ddl_Client_State.SelectedValue = dt.Rows[0]["Client_State"].ToString();
                    ddl_Client_district.SelectedValue = dt.Rows[0]["Client_District"].ToString();
                    txt_Client_city.Text = dt.Rows[0]["Client_City"].ToString();
                    txt_Client_Pincode.Text = dt.Rows[0]["Client_Pin"].ToString();
                    txt_Client_address.Text = dt.Rows[0]["Client_Address"].ToString();
                    txt_Client_phono.Text = dt.Rows[0]["Client_Phone"].ToString();
                    txt_Client_fax.Text = dt.Rows[0]["Client_Fax"].ToString();
                    txt_Client_email.Text = dt.Rows[0]["Client_Email"].ToString();
                    txt_Client_website.Text = dt.Rows[0]["Client_Web"].ToString();
                    if (dt.Rows[0]["Client_Photo"].ToString() != "")
                    {
                        bimage = (Byte[])(dt.Rows[0]["Client_Photo"]);
                        MemoryStream ms = new MemoryStream(bimage, 0, bimage.Length);
                        ms.Write(bimage, 0, bimage.Length);
                        Client_image.Image = Image.FromStream(ms, true);
                        textBoximage.Enabled = true;
                    }
                    else
                    {
                        Client_image.Image = null;
                        textBoximage.Text = "";
                        textBoximage.Enabled = false;
                    }
                    if (dt.Rows[0]["Modifiedby"].ToString() != "")
                    {
                        lbl_Record_Addedby.Text = dt.Rows[0]["Modifiedby"].ToString();
                        lbl_Record_AddedDate.Text = dt.Rows[0]["Modified_Date"].ToString();
                    }
                    else if (dt.Rows[0]["Modifiedby"].ToString() == "")
                    {
                        lbl_Record_Addedby.Text = dt.Rows[0]["Insertedby"].ToString();
                        lbl_Record_AddedDate.Text = dt.Rows[0]["Inserted_date"].ToString();
                    }

                    string Client_Status = dt.Rows[0]["Client_Status"].ToString();

                    if (Client_Status == "True")
                    {

                        rbtn_Enable.Checked = true;
                    }
                    else
                    {

                        rbtn_Disable.Checked = true;
                    }
                }

                if (Client_id != 0)
                {
                    btn_Save.Text = "Edit";
                }
                else
                {
                    btn_Save.Text = "Add";
                }

                Grd_Mail_Bind();
                //  Grd_Mail_Insert();
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //Client_id = int.Parse(tree_Client.SelectedNode.Text.Substring(0, 4));
            DialogResult dialog = MessageBox.Show("Do you want to Delete Client", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (Client_id != 0)
                {
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@Client_Id", Client_id);
                    dtdelete = dataaccess.ExecuteSP("Sp_Client", htdelete);
                    int count = dtdelete.Rows.Count;
                    MessageBox.Show("Client Deleted Successfully ");
                    clear();
                    AddParent();
                }
                else
                {
                    string title = "Select!";
                    MessageBox.Show("Please Select Client to delete", title);
                    txt_ClientName.Focus();
                }
            }
            clear();
            grd_Email.Rows.Clear();
        }

        private void txt_Client_phono_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 20)
            {
                MessageBox.Show("Enter Maximum 20 digit Number");
                txt_Client_phono.Select();

            }
        }

        private void txt_Client_fax_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 20)
            {
                MessageBox.Show("Enter Maximum 20 digit Number");

            }
        }


        private void txt_Client_Pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_Pincode.SelectedText.Length == 0) //for block first whitespace 
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

            if (pinCode.IsMatch(txt_Client_Pincode.Text) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("PinCode  Number Maximum 5 or 6 digits.");
            }

            txt_Client_Pincode.Select();
        }

        private void txt_Client_phono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_phono.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

            var countChar = txt_Client_phono.Text;
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

            txt_Client_phono.Select();
        }

        private void txt_Client_fax_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_fax.SelectedText.Length == 0) //for block first whitespace 
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

            if (txt_Client_fax.Text.Length == 20 && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Fax Number Maximum 20 digits");
            }
            txt_Client_fax.Select();
        }

        private void txt_Client_email_Leave(object sender, EventArgs e)
        {
            //Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            //if (myRegularExpression.IsMatch(txt_Client_email.Text))
            //{
            //    //valid e-mail
            //}
            //else
            //{
            //    MessageBox.Show("Email Address Not Valid");
            //}


            Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            if (myRegularExpression.IsMatch(txt_Client_email.Text))
            {
                //valid e-mail
            }
            else
            {
                MessageBox.Show("Email Address Not Valid");
            }
        }

        private void txt_Client_website_Leave(object sender, EventArgs e)
        {

        }

        private void txt_ClientNumber_Validating(object sender, CancelEventArgs e)
        {

            if (txt_ClientNumber.Text.Length < 4)
            {
                e.Cancel = true;
                MessageBox.Show("please enter Client Number It Shouldn't be Less than 4 Digit");
            }

        }

        private void btn_UploadExcel_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Masters.Client_CostTat CostTAT = new Ordermanagement_01.Masters.Client_CostTat(Client_id, userid, txt_ClientName.Text);
            CostTAT.Show();
        }

        private void grd_Email_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    duplic_Email();
                }


                Hashtable ht_Mail = new Hashtable();
                DataTable dt_Mail = new DataTable();

                if (e.ColumnIndex == 2)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        for (int i = 0; i < grd_Email.Rows.Count - 1; i++)
                        {

                            if (grd_Email.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                            {
                                grd_Email.Rows.RemoveAt(i);
                                i = i - 1;
                            }
                            Grd_Mail_Bind();
                            Client_Mail_Id = 0;
                        }
                        if (grd_Email.Rows[e.RowIndex].Cells[0].Value != null && grd_Email.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.White)
                        {
                            string Email = grd_Email.Rows[e.RowIndex].Cells[1].Value.ToString();
                            int Client_Mail_Id = int.Parse(grd_Email.Rows[e.RowIndex].Cells[0].Value.ToString());
                            ht_Mail.Add("@Trans", "DELETE");
                            ht_Mail.Add("@Client_Mail_Id", Client_Mail_Id);
                            dt_Mail = dataaccess.ExecuteSP("Sp_Client_Wise_Email", ht_Mail);
                            MessageBox.Show(" ' " + Email + " ' " + " Deleted Successfully");
                            Grd_Mail_Bind();
                            Client_Mail_Id = 0;
                        }
                    }
                }
            }
        }

        private void ListofClientNumbers_Click(object sender, EventArgs e)
        {
            string text = ListofClientNumbers.GetItemText(ListofClientNumbers.SelectedItem);
            txt_ClientNumber.Text = text;
            ListofClientNumbers.Visible = false;
            lbl_ClientRefNo.Visible = false;


        }

        private void txt_ClientNumber_Leave_1(object sender, EventArgs e)
        {

            if (txt_ClientNumber.Text.Length >= 4 && txt_ClientNumber.Text.Length <= 7)
            {
                ClientNoCheck();

            }
            else
            {
                ListofClientNumbers.Visible = false;
                lbl_ClientRefNo.Visible = false;
                MessageBox.Show("please enter Client Number Should  be Greater than 4 Digit And max 7 digit");
                txt_ClientNumber.Focus();
            }

        }
        private void txt_ClientNumber_TabIndexChanged(object sender, EventArgs e)
        {
            lbl_ClientRefNo.Visible = false;
            ListofClientNumbers.Visible = false;
            MessageBox.Show("Tab Index changed ");
        }
        private void ListofClientNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            this.ListofClientNumbers.Focus();
            this.ListofClientNumbers.Select();

            if (e.KeyCode == Keys.Up && this.ListofClientNumbers.SelectedIndex - 1 > -1)
            {
                ListofClientNumbers.SelectedIndex--;
            }
            if (e.KeyCode == Keys.Down && this.ListofClientNumbers.SelectedIndex + 1 < this.ListofClientNumbers.Items.Count)
            {
                ListofClientNumbers.SelectedIndex++;
            }
            if (e.KeyCode == Keys.Enter)
            {
                string value = ListofClientNumbers.GetItemText(ListofClientNumbers.SelectedItem);
                txt_ClientNumber.Text = value;
                //ListofClientNumbers.Visible = false;
                //lbl_ClientRefNo.Visible = false;

            }

        }

        private void Grd_Mail_Bind()
        {
            Hashtable ht_Mail_Bind = new Hashtable();
            DataTable dt_Mail_Bind = new DataTable();
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            ht_Mail_Bind.Add("@Trans", "SELECT");
            ht_Mail_Bind.Add("@Client_Id", Client_id);
            dt_Mail_Bind = dataaccess.ExecuteSP("Sp_Client_Wise_Email", ht_Mail_Bind);


            dtse = dt_Mail_Bind;

            if (dt_Mail_Bind.Rows.Count > 0)
            {
                grd_Email.Rows.Clear();
                for (int i = 0; i < dt_Mail_Bind.Rows.Count; i++)
                {
                    grd_Email.AutoGenerateColumns = false;
                    grd_Email.ColumnCount = 2;
                    grd_Email.Rows.Add();
                    grd_Email.Rows[i].Cells[0].Value = dt_Mail_Bind.Rows[i]["Client_Mail_Id"].ToString();
                    grd_Email.Rows[i].Cells[1].Value = dt_Mail_Bind.Rows[i]["Email-ID"].ToString();


                    grd_Email.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    grd_Email.Columns[0].Visible = false;

                }
            }
            else
            {
                grd_Email.DataSource = null;
                grd_Email.Rows.Clear();
            }
            if (grd_Email.Rows.Count > 1)
            {
                for (int i = 0; i < grd_Email.Rows.Count; i++)
                {
                    grd_Email.Rows[i].Cells[2].Value = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                }
            }
        }

        private void Grd_Mail_Update()
        {
            string Email_ID;
            Hashtable ht_Mail = new Hashtable();
            DataTable dt_Mail = new DataTable();

            for (int i = 0; i < grd_Email.Rows.Count; i++)
            {
                if (grd_Email.Rows[i].Cells[0].Value == null || grd_Email.Rows[i].Cells[0].Value == "" || grd_Email.Rows[i].DefaultCellStyle.BackColor == Color.White)
                {
                    if (grd_Email.Rows[i].Cells[1].Value != null)
                    {
                        if (grd_Email.Rows[i].Cells[1].Value.ToString() != "")
                        {
                            ht_Mail.Clear();
                            dt_Mail.Clear();
                            ht_Mail.Add("@Trans", "INSERT");
                            ht_Mail.Add("@Client_Id", Client_id);
                            ht_Mail.Add("@Email_ID", grd_Email.Rows[i].Cells[1].Value.ToString());
                            ht_Mail.Add("@Inserted_By", userid);
                            dt_Mail = dataaccess.ExecuteSP("Sp_Client_Wise_Email", ht_Mail);
                        }
                    }
                }
                else
                {
                    if (grd_Email.Rows[i].Cells[1].Value != null || grd_Email.Rows[i].DefaultCellStyle.BackColor == Color.White)
                    {
                        if (grd_Email.Rows[i].Cells[1].Value.ToString() != "")
                        {

                            ht_Mail.Clear();
                            dt_Mail.Clear();

                            ht_Mail.Add("@Trans", "UPDATE");
                            ht_Mail.Add("@Client_Mail_Id", grd_Email.Rows[i].Cells[0].Value.ToString());
                            ht_Mail.Add("@Email_ID", grd_Email.Rows[i].Cells[1].Value.ToString());
                            ht_Mail.Add("@Modified_By", userid);
                            dt_Mail = dataaccess.ExecuteSP("Sp_Client_Wise_Email", ht_Mail);
                        }
                    }
                }
            }
            //if (grd_Email.Rows.Count > 1)
            //{
            //    for (int i = 0; i < grd_Email.Rows.Count-1; i++)
            //    {
            //        grd_Email.Rows[i].Cells[2].Value = "Delete";
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {



            Ordermanagement_01.Masters.Client_State_County_Type_Import ci = new Masters.Client_State_County_Type_Import(Client_id);
            ci.Show();


        }

        private void txt_Client_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_address.Focus();
            }
        }

        private void txt_Client_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //}

            //if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_city.SelectedText.Length == 0) //for block first whitespace 
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("White Space not allowed for First Charcter");
            //    }
            //}

            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                MessageBox.Show("Invalid");
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_city.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
            txt_Client_city.Select();

        }

        private void txt_ClientName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }

            //if ((char.IsWhiteSpace(e.KeyChar)) && txt_ClientName.SelectedText.Length == 0) //for block first whitespace 
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("White Space not allowed for First Charcter");
            //    }
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_ClientName.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");

                }
            }
            txt_ClientName.Select();
        }

        private void txt_Client_Pincode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
                (sender as TextBox).Paste();
        }

        private void txt_Client_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_email.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_Client_email.Select();
                }
            }

            if ((txt_Client_email.Text.Length == 50) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Email is Maximum 50 Charcters");
                txt_Client_email.Select();
            }
        }

        private void txt_Client_website_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_website.SelectedText.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_Client_website.Select();
                }
            }


        }

        private void txt_Client_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_address.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_Client_address.Select();
                }
            }
        }

        private void txt_Client_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_Code.Text.Length == 0) //for block first whitespace 
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("White Space not allowed for First Charcter");
            //    }
            //}


            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Client_Code.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_Client_Code.Select();
                }
            }
        }


        private void txt_ClientNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            lbl_ClientRefNo.Visible = true;
            ListofClientNumbers.Visible = true;
            ListofClientNumbers.Enabled = true;
            lbl_ClientRefNo.Visible = true;

            BindClientNumbers();
            e.Handled = !char.IsDigit(e.KeyChar);
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar == (char)Keys.Back)
                e.Handled = false;

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_ClientNumber.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_ClientNumber.Select();
                }
            }


        }


        private bool Validate_Client_Number_Length(string Client_Number)
        {
            ArrayList ar = new ArrayList();
            //string[] strings = (string[])ar.ToArray(typeof(string));

            foreach (char item in Client_Number)
            {

                ar.Add(item);

            }
            if (Client_Number.Length == 4)
            {
                ar.RemoveAt(0);
            }
            else if (Client_Number.Length == 5)
            {
                ar.RemoveRange(0, 2);
            }
            else if (Client_Number.Length == 6)
            {
                ar.RemoveRange(0, 3);
            }
            else if (Client_Number.Length == 7)
            {
                ar.RemoveRange(0, 4);
            }

            bool Check_Value = false;
            foreach (var value in ar)
            {
                if (int.Parse(value.ToString()) != 0)
                {
                    Check_Value = false;

                    break;

                }
                else
                {

                    Check_Value = true;


                }
            }
            return Check_Value;

        }
        private void txt_Client_Pincode_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 6)
            {
                MessageBox.Show("Enter 5 or 6 digit Number");
                txt_Client_Pincode.Select();
            }
        }

        private void grd_Email_KeyPress(object sender, KeyPressEventArgs e)
        {

            duplic_Email();

            //string emailid;
            //for (int k = 0; k < grd_Email.Rows.Count - 1; k++)
            //{
            //    Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();
            //    Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            //    if (myRegularExpression.IsMatch(Email_ID))
            //    {
            //        //valid e-mail
            //    }
            //    else
            //    {
            //        MessageBox.Show("Alternative Email Address Not Valid");
            //        //Grd_Mail_Bind();

            //    }
            //    for (int j = 0; j < k; j++)
            //    {
            //        emailid = grd_Email.Rows[j].Cells[1].Value.ToString();
            //        Regex myRegularExpression1 = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            //        if (myRegularExpression1.IsMatch(emailid))
            //        {
            //            //valid e-mail
            //        }
            //        else
            //        {
            //            MessageBox.Show("Alternative Email Address Not Valid");
            //            //return false;
            //           // Grd_Mail_Bind();
            //        }
            //    }
            //}
        }
        public bool ClientNoCheck()
        {
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            try
            {

                if (txt_ClientNumber.Text != "")
                {
                    ht.Add("@Trans", "ClientNOCheck");
                    ht.Add("@Client_Number", txt_ClientNumber.Text);
                    dt = dataaccess.ExecuteSP("Sp_Client", ht);
                    int count = Convert.ToInt32(dt.Rows[0]["count"].ToString());
                    if (count > 0)
                    {
                        MessageBox.Show("Its an Already Existing ClientNumber ");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


}


