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
    
    public partial class Create_Company : Form
    {

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, comp_lbl, comp_lbl1, create_comp, create_comp1, del_comp,
            del_comp1, clear_btn, clear_btn1;
        byte[] bimage;
        int userid = 0;
        int Company_Id,edit_companyid;
        string edit_Company_name;
        string username;
        string compnname;
        public Regex pinCode = new Regex(@"^\d{6,}$", RegexOptions.Compiled);
        public Regex FaxNum = new Regex(@"^\d{15,}$", RegexOptions.Compiled);
        public Regex phoneno = new Regex(@"^\d{10,}$", RegexOptions.Compiled);
        public Regex CompName = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);
        public Regex CompSlogan = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);
        public Regex City = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);

        public Create_Company(int user_id,string Username)
        {
            InitializeComponent();
            username = Username;
            dbc.BindCountry(ddl_company_country);
            if (ddl_company_country.SelectedIndex > 0)
            {
                dbc.BindState1(ddl_company_state, int.Parse(ddl_company_country.SelectedValue.ToString()));
            }
            if (ddl_company_state.SelectedIndex > 0)
            {
                dbc.BindDistrict(ddl_company_district, int.Parse(ddl_company_state.SelectedValue.ToString()));
            }
            userid = user_id;
            //txt_companyname.MinimumSize = new Size(32, 0);
        }
        //private void txt_companyname_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox tb = sender as TextBox;
        //    if (tb != null)
        //    {
        //        tb.Width = TextRenderer.MeasureText(tb.Text, tb.Font, Size.Empty,
        //                                TextFormatFlags.TextBoxControl).Width + 20;
        //    }
        //}
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txt_companyname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_slogan.Focus();
            }
        }

        private void txt_company_slogan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_registrationno.Focus();
            }
        }

        private void txt_company_registrationno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_address.Focus();
            }
        }

        private void txt_company_address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_company_country.Focus();
            }
        }

        private void ddl_company_country_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_company_state.Focus();
                
            }
        }

        private void ddl_company_state_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_company_district.Focus();
            }
        }

        private void ddl_company_district_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_city.Focus();
            }
        }

        private void txt_company_city_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_Pincode.Focus();
            }
        }

        private void txt_company_Pincode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_phono.Focus();
            }
        }

        private void txt_company_phono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_fax.Focus();
            }
        }

        private void txt_company_fax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_email.Focus();
            }
        }

        private void txt_company_email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_company_website.Focus();
            }
        }

        private void txt_company_website_KeyDown(object sender, KeyEventArgs e)
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

     

        private void ChkDefault_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Create_Company_Load(object sender, EventArgs e)
        {
           // btn_treeview.Left = Width - 50;
            Company_Id = 0;
          pnlSideTree.Visible = true;
           AddParent();
           lbl_RecordAddedBy.Text = username;
           lbl_RecordAddedOn.Text = DateTime.Now.ToString();
            txt_companyname.Select();
          // textBoximage.Enabled = false;
       
            dbc.BindState1(ddl_company_state, int.Parse(ddl_company_country.SelectedValue.ToString()));

            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "SELECT");
            htParam.Add("@StateID", ddl_company_state.SelectedValue.ToString());
            dt = dataaccess.ExecuteSP("sp_District", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            ddl_company_district.DataSource = dt;
            ddl_company_district.DisplayMember = "District_Name";
            ddl_company_district.ValueMember = "District_Id";
        }
        private void AddParent()
         {
       
            string sKeyTemp ="";
            tvwRightSide.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            
            ht.Add("@Trans", "SELECT");
              sKeyTemp = "Company";
            dt = dataaccess.ExecuteSP("Sp_Company", ht);
               //for (int i = 0; i < dt.Rows.Count; i++)
               //  {
                    sKeyTemp = "Companies";
                   // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
                    tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
                   AddChilds(sKeyTemp);
              // }
       
    
         }
        private void AddChilds(string sKey)
           {
           Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
               
           
           ht.Add("@Trans", "SELECT");
         
            dt = dataaccess.ExecuteSP("Sp_Company", ht);
               for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     tvwRightSide.Nodes[0].Nodes.Add(dt.Rows[i]["Company_Id"].ToString() , dt.Rows[i]["Company_Name"].ToString());
          
               }
           }
        private bool Validation()
           {
               if (txt_companyname.Text == "")
               {
                   MessageBox.Show("Enter Company Name"); 
                   txt_companyname.Focus();
                   
                   return false;
               }
               else if (txt_company_address.Text == "")
               {
                   MessageBox.Show("Enter Company Address");
                   txt_company_address.Focus();
                   return false;
               }
               else if (ddl_company_country.Text == "SELECT")
               {
                   MessageBox.Show("Select Company Country");
                   ddl_company_country.Focus();
                   return false;
               }
               else if (ddl_company_state.Text == "SELECT" || ddl_company_state.Text == "")
               {
                   MessageBox.Show("Select Company State");
                   ddl_company_state.Focus();
                   return false;
               }
               else if (ddl_company_district.Text == "SELECT" || ddl_company_district.Text == "")
               {
                   MessageBox.Show("Select Company District");
                   ddl_company_district.Focus();
                   return false;
               }
               else if (txt_company_city.Text == "")
               {
                   MessageBox.Show("Enter Company City");
                   txt_company_city.Focus();
                   return false;
               }
               else if (txt_company_Pincode.Text == "")
               {
                   MessageBox.Show("Enter Company Pincode");
                   txt_company_Pincode.Focus();
                   return false;
               }
               else if (txt_company_phono.Text == "")
               {
                   MessageBox.Show("Enter Company Phone no");
                   txt_company_phono.Focus();
                   return false;
               }
               else if (txt_company_fax.Text == "")
               {
                   MessageBox.Show("Enter Company Fax");
                   txt_company_fax.Focus();
                   return false;
               }
               else if (txt_company_email.Text == "" )
               {
                 
                   MessageBox.Show("Enter Company Email");
                   txt_company_email.Focus();
                   return false;
               }
              
               else if (txt_company_website.Text == "" )
               {
                   MessageBox.Show("Enter Company Website");
                   txt_company_website.Focus();
                   return false;
               }

               Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
               if (myRegularExpression.IsMatch(txt_company_email.Text))
               {
                   //valid e-mail
               }
               else
               {
                   MessageBox.Show("Email Address Not Valid");
                   txt_company_email.Focus();
                   return false;
               }

               //else if (ChkDefault.Checked == false)
               //{
               //    MessageBox.Show("Check the Check Default");
               //    ChkDefault.Focus();
               //    return false;
               //}
               //Hashtable ht = new Hashtable();
               //DataTable dt = new DataTable();
               //ht.Add("@Trans", "COMPNAME");
               //dt = dataaccess.ExecuteSP("Sp_Company", ht);
               //for (int i = 0; i < dt.Rows.Count; i++)
               //{
               //    if (txt_companyname.Text == dt.Rows[i]["Company_Name"].ToString())
               //    {
               //        MessageBox.Show(" * " + txt_companyname.Text + " * " + " Company Name Already Exist");
               //        return false;
               //        //break;
               //    }

               //}
               return true;

           }

        private bool Val_Existed_Record()
           {
               
               
               Hashtable ht = new Hashtable();
               DataTable dt = new DataTable();
               ht.Add("@Trans", "COMPNAME");
               ht.Add("@Company_Name", txt_companyname.Text);
               //ht.Add("@Company_Id", edit_companyid);
               dt = dataaccess.ExecuteSP("Sp_Company", ht);
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   string compnname = dt.Rows[i]["Company_Name"].ToString();

                   if (txt_companyname.Text.ToUpper() == compnname.ToUpper())
                   {
                       string title = "Duplicate Record!";
                       MessageBox.Show(" * " + txt_companyname.Text + " * " + " Company Name Already Exist", title);
                       txt_companyname.Text = "";
                       txt_companyname.Select();
                       return false;
                       //break;
                   }

               }
               return true;
           }

        private bool Edit_Val()
           {
               //int Check;
               Hashtable ht = new Hashtable();
               DataTable dt = new DataTable();
               ht.Add("@Trans", "COMPNAME");
               ht.Add("@Company_Name", txt_companyname.Text);
               dt = dataaccess.ExecuteSP("Sp_Company", ht);
               if (dt.Rows.Count > 0 && edit_Company_name != txt_companyname.Text)
               {
                  
               
                       string title = "Duplicate Record!";
                       MessageBox.Show(" * " + txt_companyname.Text + " * " + " Company Name Already Exist", title);
                       txt_companyname.Text = "";
                       txt_companyname.Select();
                       return false;
               }
              
              

               return true;
           }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (ChkDefault.Checked == true)
            {
                hsforSP.Add("@Trans", "ChkDefault");
                dt = dataaccess.ExecuteSP("Sp_Company", hsforSP);
                hsforSP.Clear();
            }
            if (Validation() != false)
            {
                if (btn_Save.Text == "Add")
                {
                    if (Company_Id == 0 && Val_Existed_Record() != false)
                    {

                        //Insert
                        //img = (byte[])Session["imgempphoto"];
                        hsforSP.Add("@Trans", "INSERT");
                        //hsforSP.Add("@Company_Id", txt_companyname.Text);
                        hsforSP.Add("@Company_Name", txt_companyname.Text.ToUpper());
                        hsforSP.Add("@Comp_slogan", txt_company_slogan.Text);
                        hsforSP.Add("@Comp_RegistrationNo", txt_company_registrationno.Text);
                        hsforSP.Add("@Comp_Address", txt_company_address.Text);
                        hsforSP.Add("@Comp_Country", ddl_company_country.SelectedValue);
                        hsforSP.Add("@Comp_State", ddl_company_state.SelectedValue);
                        hsforSP.Add("@Comp_District", ddl_company_district.SelectedValue);
                        hsforSP.Add("@Comp_City", txt_company_city.Text);
                        hsforSP.Add("@Comp_Pincode", txt_company_Pincode.Text);
                        hsforSP.Add("@Comp_Phone", txt_company_phono.Text);
                        hsforSP.Add("@Comp_Fax", txt_company_fax.Text);
                        hsforSP.Add("@Comp_Email", txt_company_email.Text);
                        hsforSP.Add("@Comp_Web", txt_company_website.Text);
                        hsforSP.Add("@Comp_Logo", bimage);
                        //  hsforSP.Add("@Com_SetDefault", ChkDefault.Checked);
                        hsforSP.Add("@Status", "TRUE");
                        hsforSP.Add("@Inserted_By", userid);
                        hsforSP.Add("@Inserted_date", DateTime.Now);
                        ////hsforSP.Add("@Modified_By", supportContractStartDate);
                        ////hsforSP.Add("@Modified_Date", supportContractEndDate);
                        ////hsforSP.Add("@status", endofSupportLife);
                        dt = dataaccess.ExecuteSP("Sp_Company", hsforSP);
                        string title = "Insert";
                        MessageBox.Show(" * " + txt_companyname.Text + " * " + " Company Name Created Sucessfully", title);
                        clear();
                        AddParent();
                        Company_Id = 0;
                    }
                }
                else if (btn_Save.Text == "Edit" && Edit_Val()!=false)
                {
                    if (Company_Id != 0)
                    {
                        //Update
                        hsforSP.Add("@Trans", "UPDATE");
                        hsforSP.Add("@Company_Id", Company_Id);
                        hsforSP.Add("@Company_Name", txt_companyname.Text.ToUpper());
                        hsforSP.Add("@Comp_slogan", txt_company_slogan.Text);
                        hsforSP.Add("@Comp_RegistrationNo", txt_company_registrationno.Text);
                        hsforSP.Add("@Comp_Address", txt_company_address.Text);
                        hsforSP.Add("@Comp_Country", ddl_company_country.SelectedValue);
                        hsforSP.Add("@Comp_State", ddl_company_state.SelectedValue);
                        hsforSP.Add("@Comp_District", ddl_company_district.SelectedValue);
                        hsforSP.Add("@Comp_City", txt_company_city.Text);
                        hsforSP.Add("@Comp_Pincode", txt_company_Pincode.Text);
                        hsforSP.Add("@Comp_Phone", txt_company_phono.Text);
                        hsforSP.Add("@Comp_Fax", txt_company_fax.Text);
                        hsforSP.Add("@Comp_Email", txt_company_email.Text);
                        hsforSP.Add("@Comp_Web", txt_company_website.Text);
                        hsforSP.Add("@Comp_Logo", bimage);
                        //  hsforSP.Add("@Com_SetDefault", ChkDefault.Checked);
                        hsforSP.Add("@Status", "TRUE");
                        // hsforSP.Add("@Inserted_By", Empname);
                        //  hsforSP.Add("@Inserted_date", DateTime.Now);
                        hsforSP.Add("@Modified_By", userid);
                        hsforSP.Add("@Modified_Date", DateTime.Now);
                        ////hsforSP.Add("@status", endofSupportLife);
                        dt = dataaccess.ExecuteSP("Sp_Company", hsforSP);
                        // model1.Hide();
                        string title = "Update";
                        MessageBox.Show(" * " + txt_companyname.Text + " * " + " Company Name Updated Sucessfully", title);
                        clear();
                        AddParent();
                    }
                }
                
            }
            //}
            //else
            //{
            //    MessageBox.Show("Select Country , State  And District");
            //}
            
            }

            

        private void clear()
        {
           //lbl_Company.Text = "Company";
            //pt.X = 485; pt.Y = 4;
            //lbl_Company.Location = pt;
            txt_companyname.Text = "";
            Company_Id = 0;
            txt_company_slogan.Text = "";
            txt_company_registrationno.Text = "";
            txt_company_address.Text = "";
            ddl_company_state.DataSource = null;
            ddl_company_district.DataSource = null;
            ddl_company_country.SelectedIndex = 0;
            txt_company_city.Text = "";
            txt_company_Pincode.Text = "";
            txt_company_phono.Text = "";
            txt_company_fax.Text = "";
            txt_company_email.Text = "";
            txt_company_website.Text = "";
            btn_Save.Text = "Add";
            textBoximage.Text = "";
            cmp_Image.Image = null;
            txt_companyname.BackColor = System.Drawing.Color.White;
            ddl_company_district.BackColor = System.Drawing.Color.White;
            ddl_company_state.BackColor = System.Drawing.Color.White;
            ddl_company_country.BackColor = System.Drawing.Color.White;
            //lbl_RecordAddedBy.Text = "";
            ChkDefault.Checked = false;
            lbl_RecordAddedBy.Text = username;
            lbl_RecordAddedOn.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            textBoximage.Enabled = false;

           // cmp_Image.InitialImage = null;


           // clear();
            AddParent();


           // tvwRightSide.Focus();

            //if (cmp_Image.Image != null)
            //{
            //    cmp_Image.Image.Dispose();
            //    cmp_Image.Image = null;
            //}
            //Graphics graphic = Graphics.FromImage(cmp_Image.Image);
            //graphic.Clear(Color.Red);//Color to fill the background and reset the box
        }
        

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {


        }

       

        private void tvwRightSide_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Checked;
            string logo;
          //  clear();
            cmp_Image.Update();
            bool isNum = Int32.TryParse(tvwRightSide.SelectedNode.Name, out Company_Id);
            if (isNum)
            {

                edit_companyid = Company_Id;
               // lbl_Company.Text = "Edit";
                //pt.X=465;pt.Y=4; 
                //lbl_Company.Location=pt;
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new DataTable();
                hsforSP.Add("@Trans", "SELECTGRID");
                hsforSP.Add("@Company_Id", Company_Id);
                dt = dataaccess.ExecuteSP("Sp_Company", hsforSP);
                txt_companyname.Text = dt.Rows[0]["Company_Name"].ToString();
                edit_Company_name = dt.Rows[0]["Company_Name"].ToString();
                txt_company_slogan.Text = dt.Rows[0]["Comp_slogan"].ToString();
                txt_company_registrationno.Text = dt.Rows[0]["Comp_RegistrationNo"].ToString();
                txt_company_address.Text = dt.Rows[0]["Comp_Address"].ToString();
                txt_company_Pincode.Text = dt.Rows[0]["Comp_Pincode"].ToString();
                txt_company_phono.Text = dt.Rows[0]["Comp_Phone"].ToString();
                txt_company_fax.Text = dt.Rows[0]["Comp_Fax"].ToString();
                txt_company_email.Text = dt.Rows[0]["Comp_Email"].ToString();
                txt_company_website.Text = dt.Rows[0]["Comp_Web"].ToString();
                txt_company_city.Text = dt.Rows[0]["Comp_City"].ToString();
                ddl_company_country.SelectedValue = dt.Rows[0]["Comp_Country"].ToString();
                ddl_company_state.SelectedValue = dt.Rows[0]["Comp_State"].ToString();
                ddl_company_district.SelectedValue = dt.Rows[0]["Comp_District"].ToString();
                // byte[] imageBytes = Convert.FromBase64String(dt.Rows[0]["Comp_Logo"].ToString()); 
                

               if (dt.Rows[0]["Comp_Logo"].ToString() != "")
               {

                        bimage = (Byte[])(dt.Rows[0]["Comp_Logo"]);
                        MemoryStream ms = new MemoryStream(bimage, 0, bimage.Length);
                        ms.Write(bimage, 0, bimage.Length);
                        cmp_Image.Image = Image.FromStream(ms, true);
                        textBoximage.Enabled = false;
                }
               
                else
                {
                    cmp_Image.Image = null;
                    textBoximage.Text = "";
                    textBoximage.Enabled = false;
                }

                //if (cmp_Image.Image != null)
                //{
                //    cmp_Image.Image.Dispose();
                //    cmp_Image.Image = null;
                //}

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


               // Checked = dt.Rows[0]["SetDefault"].ToString();
                //if (Checked == "True")
                //{
                //    ChkDefault.Checked = true;

                //}
                //else if (Checked == "False")
                //{
                //    ChkDefault.Checked = false;
                //}
                if (Company_Id != 0)
                {
                    btn_Save.Text = "Edit";
                }
                else
                {
                    btn_Save.Text = "Add";
                }
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
                    cmp_Image.Image = GetDataToImage((byte[])bimage);
                    textBoximage.Enabled = true;
                   // textBoximage.Visible = true;
                }
              //  textBoximage.Enabled = true;
            }
            else
            {
                textBoximage.Enabled = true;
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

        private void ddl_company_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_company_country.SelectedIndex > 0)
            {
                int val = int.Parse(ddl_company_country.SelectedValue.ToString());
                if (val == 99)
                {
                    dbc.BindState1(ddl_company_state, int.Parse(ddl_company_country.SelectedValue.ToString()));
                }
                else if (val == 228)
                {
                    dbc.Bind_State_BY_Country(ddl_company_state, int.Parse(ddl_company_country.SelectedValue.ToString()));
                }
            }
            //else
            //{
            //    if (ddl_company_country.SelectedItem.ToString() == "SELECT")
            //    {
            //        ddl_company_state.SelectedItem = "Select";



            //    }
            //}

        }

        private void ddl_company_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_company_state.SelectedIndex > 0)
            {
                int value = int.Parse(ddl_company_country.SelectedValue.ToString());
                if(value==99)
                {
                       dbc.BindDistrict(ddl_company_district, int.Parse(ddl_company_state.SelectedValue.ToString()));
                }
                else{
                    if (value == 228)
                   {
                            dbc.BindCounty_StateWise(ddl_company_district, int.Parse(ddl_company_state.SelectedValue.ToString()));
                   }
                }
                
            }
        }

        private void tvwRightSide_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
              DialogResult dialog = MessageBox.Show("Do you want to Delete a Company", "Delete Confirmation", MessageBoxButtons.YesNo);
              if (dialog == DialogResult.Yes)
              {
                  if (Company_Id != 0)
                  {
                      Hashtable hsforSP = new Hashtable();
                      DataTable dt = new DataTable();
                      //   Company_Id = int.Parse(tvwRightSide.SelectedNode.Text.Substring(0, 4).ToString());
                    //  string Companyname = tvwRightSide.SelectedNode.Text.Substring(0, 4).ToString();
                      hsforSP.Add("@Trans", "DELETE");
                      hsforSP.Add("@Company_Id", Company_Id);
                      dt = dataaccess.ExecuteSP("Sp_Company", hsforSP);
                      int count = dt.Rows.Count;

                      MessageBox.Show(" * " + txt_companyname.Text +   " * "  + " Company Successfully Deleted");
                      clear();
                      AddParent();
                  }
                  else
                  {
                      string title = "Select!";
                      MessageBox.Show("Please Select Valid Company Name",title);
                      tvwRightSide.Focus();
                  }
              }
       }
        private void txt_company_email_Leave(object sender, EventArgs e)
        {
            Regex myRegularExpression =new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

           // Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (myRegularExpression.IsMatch(txt_company_email.Text))
            {
                //valid e-mail
            }
            else
            {
                MessageBox.Show("Email Address Not Valid");
            }
        }

        private void txt_company_email_CursorChanged(object sender, EventArgs e)
        {
            //Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            //if (myRegularExpression.IsMatch(txt_company_email.Text))
            //{
            //    //valid e-mail
            //}
            //else
            //{
            //    MessageBox.Show("Email Address Not Valid");
            //}
        }

        private void txt_company_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_email.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_company_email.Select();
                }
            }
            if ((txt_company_email.Text.Length ==50) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Email is Maximum 50 Charcters");
            }
           


            //Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            //if (myRegularExpression.IsMatch(txt_company_email.Text))
            //{
            //    //valid e-mail
            //}
            //else
            //{
            //    MessageBox.Show("Email Address Not Valid");
            //}
        }


        private void txt_company_Pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

           

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }
            if (pinCode.IsMatch(txt_company_Pincode.Text) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("PinCode Number Maximum 6 digits.");
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_Pincode.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
          
        }

        private void txt_company_phono_KeyPress(object sender, KeyPressEventArgs e)
        {
         //   e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            //if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Invalid!,Kindly Enter Numbers"); 
            //}

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }

           

            //if (char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back &&  txt_company_phono.Text.Length > 15)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Phone Number must be 15 digits");
            //}

            var countChar = txt_company_phono.Text;
            if (countChar.Length == 20 && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Phone Number Maximum 20 digits");
            }
           
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_phono.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
            
        }

        private void txt_company_fax_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }

            //if (FaxNum.IsMatch(txt_company_fax.Text) && e.KeyChar != (char)Keys.Back)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Fax Number less then 10 digits.");
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_fax.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

            if (txt_company_fax.Text.Length == 20 && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Fax Number Maximum 20 digits");
            }




            //Regex myRegularExpression = new Regex("^[+-0-9 ]");
            //if (myRegularExpression.IsMatch(txt_company_fax.Text))
            //{
            //    //valid e-mail
            //}
            //else
            //{
            //    MessageBox.Show("Invalid!, Enter Correct Fax");
            //}
            const char hyphen = (char)0x2D;
            const char delete = (char)0x08;
            if (char.IsNumber(e.KeyChar) && e.KeyChar == hyphen)
            {
                e.Handled = true;
                MessageBox.Show("Invalid!, Enter Correct Fax");
            }

            //if (txt_company_fax.Text.Length == 0)
            //{
            //    if (e.Handled = (e.KeyChar == (char)Keys.Space))
            //    {
            //        MessageBox.Show("space not allowed!");
            //    }
            //}
        }

      

        private void txt_companyname_Leave(object sender, EventArgs e)
        {
            

        }

        

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 50;
            add_pt.X = 5; add_pt.Y = 485;
            comp_pt1.X = 200; comp_pt1.Y = 50;
            add_pt1.X = 200; add_pt1.Y = 485;
            comp_lbl.X = 315; comp_lbl.Y = 20;
            comp_lbl1.X = 513; comp_lbl1.Y = 20;
            create_comp.X = 175; create_comp.Y = 601;
            create_comp1.X = 365; create_comp1.Y = 601;
            del_comp.X = 335; del_comp.Y = 601;
            del_comp1.X = 525; del_comp1.Y = 601;
            clear_btn.X = 495; clear_btn.Y = 601;
            clear_btn1.X = 685; clear_btn1.Y = 601;
            form_pt.X = 350; form_pt.Y = 20;
            form1_pt.X = 200; form1_pt.Y = 20;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_Company.Location = comp_lbl;
                btn_Save.Location = create_comp;
                btn_Delete.Location = del_comp;
                btn_Cancel.Location = clear_btn;
                grp_Comp_det.Location = comp_pt;
                grp_Add_det.Location = add_pt;
                Create_Company.ActiveForm.Width = 750;
                Create_Company.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_Company.Location = comp_lbl1;
                btn_Save.Location = create_comp1;
                btn_Delete.Location = del_comp1;
                btn_Cancel.Location = clear_btn1;
                grp_Comp_det.Location = comp_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Company.ActiveForm.Width = 950;
                Create_Company.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");

            }
            AddParent();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txt_companyname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txt_companyname.Text.Length == 0)
            //{
            //    if (e.Handled = (e.KeyChar == (char)Keys.Space))
            //   {
            //       MessageBox.Show("space not allowed!");
            //   }  
            //}
            //if (CompName.IsMatch(txt_companyname.Text) && e.KeyChar != (char)Keys.Back)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("Numbers not allowed");
            //}

            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_companyname.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

          


        }

        private void txt_company_slogan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_slogan.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_company_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                MessageBox.Show("Invalid");
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_city.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void ddl_company_country_KeyPress(object sender, KeyPressEventArgs e)
        {
           //ddl_company_country_SelectedIndexChanged(sender, e);
        }

        private void txt_company_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_address.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_company_website_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_website.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_company_registrationno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_company_registrationno.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void ddl_company_state_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && ddl_company_state.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void ddl_company_district_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && ddl_company_district.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void textBoximage_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_companyname_TextChanged(object sender, EventArgs e)
        {
         //  compnname = txt_companyname.Text.ToUpper();
            
          //  Val_Existed_Record();
        }

        private void txt_company_email_TextChanged(object sender, EventArgs e)
        {
           //Regex myRegularExpression =new Regex("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")

           //// Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
           // if (myRegularExpression.IsMatch(txt_company_email.Text))
           // {
           //     //valid e-mail
           // }
           // else
           // {
           //     MessageBox.Show("Email Address Not Valid");
           //     txt_company_email.Focus();
           //     // return false;
           // }
        }

        private void txt_company_Pincode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V))
                (sender as TextBox).Paste();
        }

      
        private void txt_company_Pincode_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 6)
            {
                MessageBox.Show("Enter 5 or 6 digit Number");
             
            }
        }

        private void txt_company_phono_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 20)
            {
                MessageBox.Show("Enter Maximum 20 digit Number");
               
            }
        }

        private void txt_company_fax_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 20)
            {
                MessageBox.Show("Enter Maximum 20 digit Number");
               
            }
        }

       

       
       
    }
}
