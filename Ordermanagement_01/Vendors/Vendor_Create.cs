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
using System.Globalization;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Create : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, County, Vendor_Id, Check, Delvalue = 0, Vendor_count;
        System.Data.DataTable dtsel = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtselect = new System.Data.DataTable();
        System.Data.DataTable dtnew = new System.Data.DataTable();
        System.Data.DataTable dt_grid_serach_State_County = new System.Data.DataTable();
        System.Data.DataTable dt_Search = new System.Data.DataTable();

        string duplicate, vendor_number, vendor_number1;
        static int currentpageindex = 0;
        int pagesize = 15;
        public Regex pinCode = new Regex(@"^\d{6,}$", RegexOptions.Compiled);
        public Regex FaxNum = new Regex(@"^\d{15,}$", RegexOptions.Compiled);
        public Regex phoneno = new Regex(@"^\d{10,}$", RegexOptions.Compiled);
        string User_Role;
        public Vendor_Create(int vendorid,int UserID,string USER_ROLE)
        {
            InitializeComponent();
            Userid = UserID;
            User_Role = USER_ROLE;
            Vendor_Id = vendorid;

            
        }

        private void clear()
        {
            txt_Vendor.Text = "";
            txt_Vendor_no.Text = "";
           
            txt_Vendor_city.Text = "";
            txt_Vendor_address.Text = "";
            txt_Vendor_Pincode.Text = "";
            txt_Vendor_phono.Text = ""; 
            txt_Vendor_fax.Text = "";
            txt_Vendor_email.Text = "";
            txt_Vendor_website.Text = "";
            AutoGenerateNumber();
        }

        private void AutoGenerateNumber()
        {
            Hashtable htauto = new Hashtable();
            DataTable dtauto = new DataTable();
            htauto.Add("@Trans", "MAX_VENDOR_NUMBER");
            dtauto = dataaccess.ExecuteSP("Sp_Vendor", htauto);
            if (dtauto.Rows.Count > 0)
            {
                vendor_number = dtauto.Rows[0]["Vendor_Number"].ToString();
                vendor_number1 = "VEN-" + dtauto.Rows[0]["Vendor_Number"].ToString();
                txt_Vendor_no.Text = vendor_number1;
            }
        }

        private bool Validation()
        {

            if (txt_Vendor.Text == "")
            {
                MessageBox.Show("Enter Vendor Name.");
                txt_Vendor.Focus();
                return false;
            }
            //else if (txt_Vendor_city.Text == "")
            //{
            //    MessageBox.Show("Enter Vendor City Name.");
            //    txt_Vendor_city.Focus();
            //    return false;
            //}
            //else if (txt_Vendor_Pincode.Text == "")
            //{
            //    MessageBox.Show("Enter Vendor Pincode.");
            //    txt_Vendor_Pincode.Focus();
            //    return false;
            //}
            //else if (txt_Vendor_address.Text == "")
            //{
            //    MessageBox.Show("Enter Vendor Address.");
            //    txt_Vendor_address.Focus();
            //    return false;
            //}
            //else if (txt_Vendor_phono.Text == "")
            //{
            //    MessageBox.Show("Enter Vendor Phone Number.");
            //    txt_Vendor_phono.Focus();
            //    return false;
            //}
            //else if (txt_Vendor_fax.Text == "")
            //{
            //    MessageBox.Show("Enter Vendor Fax Number.");
            //    txt_Vendor_fax.Focus();
            //    return false;
            //}
            //else if (txt_Vendor_email.Text == "")
            //{
            //    MessageBox.Show("Enter Vendor Email Id.");
            //    txt_Vendor_email.Focus();
            //    return false;
            //}
            //else if (txt_Vendor_website.Text == "")
            //{
            //    MessageBox.Show("Enter Vendor Web site Name.");
            //    txt_Vendor_website.Focus();
            //    return false;
            //}
            return true;
        }

        protected bool Validation1()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Vendor", ht);
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string DtOrderType = (dt.Rows[i]["Vendor_Name"].ToString()).ToLower();

                string OrderType = (txt_Vendor.Text).ToLower();
                if (DtOrderType == OrderType && btn_Add_New.Text != "Edit Vendor")
                {
                    duplicate = "Duplicate Data";
                    MessageBox.Show("Vendor Name Already Exists.");
                    return false;
                }
            }
            return true;
        }
       
        private void btn_Add_New_Click(object sender, EventArgs e)
        {

            if (Validation() != false && Vendor_Id == 0 && duplicate != "Duplicate Data")
            {
                if (Validation1() != false)
                {                   
                    string clientname = txt_Vendor.Text.ToUpper().ToString();

                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new DataTable();
                    DataTable dt = new DataTable();
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    htinsert.Add("@Trans", "INSERT");
                    htinsert.Add("@Branch_ID", int.Parse(ddl_BranchNamee.SelectedValue.ToString()));
                    htinsert.Add("@Vendor_Number", vendor_number);
                    htinsert.Add("@Vendor_Name", clientname);
                    
                    htinsert.Add("@Vendor_City", txt_Vendor_city.Text);
                    htinsert.Add("@Vendor_Address", txt_Vendor_address.Text);
                    htinsert.Add("@Vendor_Phone", txt_Vendor_phono.Text);
                    htinsert.Add("@Vendor_Pin", txt_Vendor_Pincode.Text.ToString());
                    htinsert.Add("@Vendor_Fax", txt_Vendor_fax.Text);
                    htinsert.Add("@Vendor_Email", txt_Vendor_email.Text);
                    htinsert.Add("@Vendor_Web", txt_Vendor_website.Text);
                    htinsert.Add("@Inserted_By", Userid);
                    dtinsert = dataaccess.ExecuteSP("Sp_Vendor", htinsert);
                                       
                    AutoGenerateNumber();
                    MessageBox.Show("Vendor Created Successfully");
                    clear();
                 
                    foreach (Form f1 in Application.OpenForms)
                    {
                        if (f1.Name == "Vendor_Create")
                        {

                            f1.Close();
                            break;

                        }
                    }
                    
                
                }
              
            }
            else if (Vendor_Id!=0)          
            {

                string clientname = txt_Vendor.Text.ToUpper().ToString();

                Hashtable htupdate = new Hashtable();
                DataTable dtupdate = new DataTable();
                DataTable dt = new DataTable();
                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                
                htupdate.Add("@Trans", "UPDATE");
                htupdate.Add("@Vendor_Id", Vendor_Id);
                htupdate.Add("@Branch_ID", int.Parse(ddl_BranchNamee.SelectedValue.ToString()));
                htupdate.Add("@Vendor_Number", vendor_number);
                htupdate.Add("@Vendor_Name", clientname);              
                htupdate.Add("@Vendor_City", txt_Vendor_city.Text);
                htupdate.Add("@Vendor_Address", txt_Vendor_address.Text);
                htupdate.Add("@Vendor_Phone", txt_Vendor_phono.Text.ToString());
                htupdate.Add("@Vendor_Pin", txt_Vendor_Pincode.Text.ToString());
                htupdate.Add("@Vendor_Fax", txt_Vendor_fax.Text);
                htupdate.Add("@Vendor_Email", txt_Vendor_email.Text);
                htupdate.Add("@Vendor_Web", txt_Vendor_website.Text);
                htupdate.Add("@Modified_By", Userid);
                htupdate.Add("@Modified_Date", date);
                htupdate.Add("@status", "True");
                dtupdate = dataaccess.ExecuteSP("Sp_Vendor", htupdate);
                
                btn_Add_New.Text = "Add New Vendor";
            
                MessageBox.Show("Vendor Updated Successfully");
                clear();

                this.Close();
                //foreach (Form f1 in Application.OpenForms)
                //{
                //    if (f1.Name == "Vendor_Create")
                //    {

                //        f1.Close();
                //        break;

                //    }
                //}
                //Ordermanagement_01.Vendors.Vendor_View vendorview = new Ordermanagement_01.Vendors.Vendor_View(Userid);

                //vendorview.Show();
                ////  vendorview.Close();
                ////this.Close();
                //vendorview.Refresh();
                //  vendorview.Show();
            

            }
          
            
           
         
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        


        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Ddl_Vendor_State.SelectedIndex > 0)
            //{
            //    dbc.BindCounty(Ddl_Vendor_Country, int.Parse(Ddl_Vendor_State.SelectedValue.ToString()));
            //}
        }

        private void Vendor_Create_Load(object sender, EventArgs e)
        {
            AutoGenerateNumber();
            dbc.BindCompany(ddl_CompanyName);
            dbc.BindBranch(ddl_BranchNamee, int.Parse(ddl_CompanyName.SelectedValue.ToString()));
            txt_Vendor.Select();
            
            if (Vendor_Id == 0)
            {
                btn_Add_New.Text = "Add New Vendor";
                tabControl1.TabPages.Remove(grid_State_County);

            }
            else
            {
                Bind_Vendor_Info();
                Bind_All_State_Info();
                btn_Add_New.Text = "Edit Vendor";
             
            }


            Ordermanagement_01.Vendors.Vendor_View vendorview = new Ordermanagement_01.Vendors.Vendor_View(Userid, User_Role);

            vendorview.Hide();
        }


        private void Bind_Vendor_Info()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            htsel.Add("@Trans", "SELECT_VENDOR");
            htsel.Add("@Vendor_Id", Vendor_Id);
            dtsel = dataaccess.ExecuteSP("Sp_Vendor", htsel);
            if (dtsel.Rows.Count > 0)
            {
                txt_Vendor.Text = dtsel.Rows[0]["Vendor_Name"].ToString();
                txt_Vendor_no.Text = "VEN - " + dtsel.Rows[0]["Vendor_Number"].ToString();
                txt_Vendor_city.Text = dtsel.Rows[0]["Vendor_City"].ToString();
                txt_Vendor_Pincode.Text = dtsel.Rows[0]["Vendor_Pin"].ToString();
                txt_Vendor_address.Text = dtsel.Rows[0]["Vendor_Address"].ToString();
                txt_Vendor_phono.Text = dtsel.Rows[0]["Vendor_Phone"].ToString();
                txt_Vendor_fax.Text = dtsel.Rows[0]["Vendor_Fax"].ToString();
                txt_Vendor_email.Text = dtsel.Rows[0]["Vendor_Email"].ToString();
                txt_Vendor_website.Text = dtsel.Rows[0]["Vendor_Web"].ToString();
            }
        }
        
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Bind_All_State_Info();
            txt_State_County.Text = "";
            txt_State_County.Select();
        }
        

        private void txt_Vendor_phono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsNumber(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (phoneno.IsMatch(txt_Vendor_phono.Text) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Phone Number must be 10 digits");
            }
        }

        private void txt_Vendor_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txt_Vendor_no_KeyPress(object sender, KeyPressEventArgs e)
        {
        //    if (!char.IsNumber(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
            if (pinCode.IsMatch(txt_Vendor_no.Text))
            {
                e.Handled = true;
            }
        }

        private void txt_Vendor_Pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            if (pinCode.IsMatch(txt_Vendor_Pincode.Text) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("PinCode Should be 6 digits.");
            }

        }

        private void txt_Vendor_fax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))  && e.KeyChar != (char)Keys.Back &&  !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }


            //if (!(char.IsDigit(e.KeyChar)) && !(char.IsPunctuation(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            //{
            //    e.Handled = true;
            //}

            if (FaxNum.IsMatch(txt_Vendor_fax.Text) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Fax Number less then 20 digits.");
            }
        }

        private void txt_Vendor_email_Leave(object sender, EventArgs e)
        {
            Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            if (myRegularExpression.IsMatch(txt_Vendor_email.Text))
            {
                //valid e-mail
            }
            else
            {
                MessageBox.Show("Email Address Not Valid");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        //private void Bind_All_State_Info()
        //{
        //    Hashtable htsearch = new Hashtable();
        //    htsearch.Add("@Trans", "SELECT_AVAIL_TRUE");
        //    htsearch.Add("@Vendor_Id", Vendor_Id);

        //    dtsel = dataaccess.ExecuteSP("Sp_Vendor_State_County", htsearch);
        //    if (dtsel.Rows.Count > 0)
        //    {
        //        gridstate.Rows.Clear();
        //        for (int i = 0; i < dtsel.Rows.Count; i++)
        //        {
        //            gridstate.Rows.Add();
        //            gridstate.Rows[i].Cells[0].Value = i + 1;
        //            gridstate.Rows[i].Cells[1].Value = dtsel.Rows[i]["State"].ToString();
        //            gridstate.Rows[i].Cells[2].Value = dtsel.Rows[i]["County"].ToString();
        //            gridstate.Rows[i].Cells[3].Value = dtsel.Rows[i]["Vendor_Id"].ToString();
        //        }
        //    }
        //}

        private void gridstate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Get_Row_Table_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void txt_State_County_TextChanged(object sender, EventArgs e)
       {
           //if (gridstate.Rows.Count > 0)
           //{
               //  DataView dtsearch = new DataView(dtsel);   
           if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
           {
               DataView dtsearch = new DataView(dtselect); 

              //  DataView dtsearch = new DataView(dt_Search);


               var search = txt_State_County.Text.ToString();
               dtsearch.RowFilter = "State like '%" + search.ToString() + "%' or County like '%" + search.ToString() + "%' ";

               dt = dtsearch.ToTable();
               System.Data.DataTable temptable1 = dt.Clone();
               int startindex = currentpageindex * pagesize;
               int endindex = (currentpageindex * pagesize) + pagesize;
               if (endindex > dt.Rows.Count)
               {
                   endindex = dt.Rows.Count;
               }
               for (int i = startindex; i < endindex; i++)
               {
                   DataRow row = temptable1.NewRow();
                   Get_Row_Table_Search(ref row, dt.Rows[i]);
                   temptable1.Rows.Add(row);
               }

               if (temptable1.Rows.Count > 0)
               {
                   gridstate.Rows.Clear();
                   for (int i = 0; i < temptable1.Rows.Count; i++)
                   {
                       gridstate.Rows.Add();
                       gridstate.Rows[i].Cells[0].Value = i + 1;
                       gridstate.Rows[i].Cells[1].Value = temptable1.Rows[i]["State"].ToString();
                       gridstate.Rows[i].Cells[2].Value = temptable1.Rows[i]["County"].ToString();

                       gridstate.Rows[i].Cells[3].Value = temptable1.Rows[i]["Vendor_Id"].ToString();
                       gridstate.Rows[i].Cells[4].Value = temptable1.Rows[i]["Vendor_State_Id"].ToString();
                   }
                   lbl_Total_State_County.Text = dt.Rows.Count.ToString();
               }
               else
               {
                   gridstate.Rows.Clear();
                   MessageBox.Show("No Records Found");
                  // Bind_All_State_Info();
                   txt_State_County.Text = "";
               }
               lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);

           }
           else
           {
               Bind_All_State_Info();


           }
        }

        private void txt_State_County_Enter(object sender, EventArgs e)
        {
            txt_State_County.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
          //  txt_State_County.Text = "Search by State County...";

                currentpageindex++;
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;
                  
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;
                    txt_State_County_TextChanged(sender, e);
                    //Bind_All_State_Info();
                    //txt_State_County.Select();
                    //if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
                    //{
                    //    Bind_Filter_data();
                    //}
                    //else
                    //{
                    //    Bind_All_State_Info();
                    //    // Bind_All_State();


                    //}

                }
                if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
                {
                    Bind_Filter_data();
                }
                else
                {
                    Bind_All_State_Info();
                   // Bind_All_State();

                  }
            
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
                Bind_Filter_data();
            }
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
                Bind_All_State();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentpageindex--;
            if (currentpageindex == 0)
            {
                btnPrevious.Enabled = false;
                btnFirst.Enabled = false;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;

            }
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
            {

                Bind_Filter_data();

            }
            else
            {
                Bind_All_State();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
            {
                Bind_Filter_data();

            }
            else
            {
                Bind_All_State();
            }
        }

        private void txt_Vendor_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (char.IsWhiteSpace(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(tabControl1.SelectedIndex == 1)
            {
                grid_State_County.Focus();
                Bind_All_State_Info();
                txt_State_County.Select();

                //foreach (Form f1 in Application.OpenForms)
                //{
                //    if (f1.Name == "Vendor_Create")
                //    {

                //        f1.Close();
                       
                //            break;
                        

                //    }


                //    Ordermanagement_01.Vendors.Vendor_View vendorview = new Ordermanagement_01.Vendors.Vendor_View(Userid);

                //    vendorview.Show();
                //    //  vendorview.Close();
                //    //this.Close();
                //    vendorview.Refresh();
                //}
               
            }
            else{
            
            }
            
        }

        private void Bind_Filter_data()
        {
            DataView dtsearch = new DataView(dtselect);
           // DataView dtsearch = new DataView(dt_Search);
            var search = txt_State_County.Text.ToString();
            dtsearch.RowFilter = "State like '%" + search.ToString() + "%' or County like '%" + search.ToString() + "%' ";
            dt = dtsearch.ToTable();
            System.Data.DataTable temptable = dt.Clone();

            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dt.Rows.Count)
            {
                endindex = dt.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                Get_Row_Table_Search(ref row, dt.Rows[i]);
                temptable.Rows.Add(row);
            }

            if (temptable.Rows.Count > 0)
            {
                gridstate.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    gridstate.Rows.Add();
                    gridstate.Rows[i].Cells[0].Value = i + 1;
                    gridstate.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                    gridstate.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                   
                    gridstate.Rows[i].Cells[3].Value = temptable.Rows[i]["Vendor_Id"].ToString();
                    gridstate.Rows[i].Cells[4].Value = temptable.Rows[i]["Vendor_State_Id"].ToString();
                }
            }
            lbl_Total_State_County.Text = dt.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
        }


        private void Bind_All_State()
        {

            //DataView dtview = new DataView(dt_grid_serach_State_County);

            DataView dtview = new DataView(dtselect);

            dt = dtview.ToTable();

            System.Data.DataTable temptable = dt.Clone();
          //  currentpageindex++;
            int startindex = currentpageindex * pagesize;
            int endindex = (currentpageindex * pagesize) + pagesize;
            if (endindex > dt.Rows.Count)
            {
                endindex = dt.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                Get_Row_Table_Search(ref row, dt.Rows[i]);
                temptable.Rows.Add(row);
            }

            dt_Search = temptable;
            if (temptable.Rows.Count > 0)
            {

                gridstate.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {

                    gridstate.Rows.Add();
                    gridstate.Rows[i].Cells[0].Value = i + 1;
                    gridstate.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                    gridstate.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                  
                    gridstate.Rows[i].Cells[3].Value = temptable.Rows[i]["Vendor_Id"].ToString();
                    gridstate.Rows[i].Cells[4].Value = temptable.Rows[i]["Vendor_State_Id"].ToString();
                }
                lbl_Total_State_County.Text = temptable.Rows.Count.ToString();
            }
            else
            {
                //gridstate.Rows.Clear();
           
                //MessageBox.Show("No Records Found");
                //Bind_All_State();
                //txt_State_County.Text = "";
            }
            lbl_Total_State_County.Text = dt_Search.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
        }


        private void Bind_All_State_Info()
        {
            Hashtable htsel = new Hashtable();
            Hashtable htnew = new Hashtable();
            htsel.Add("@Trans", "SELECT_STATE_COUNTY");
            htsel.Add("@Vendor_Id", Vendor_Id);
            dtselect.Rows.Clear();
            dtselect = dataaccess.ExecuteSP("Sp_Vendor_State_County", htsel);

            htnew.Add("@Trans", "SELECT_AVAIL_TRUE");
            htnew.Add("@Vendor_Id", Vendor_Id);

            dtnew = dataaccess.ExecuteSP("Sp_Vendor_State_County", htnew);


            dt_grid_serach_State_County = dtselect;

            if (dtnew.Rows.Count > 0)
            {
                gridstate.Rows.Clear();

                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    gridstate.Rows.Add();
                    gridstate.Rows[i].Cells[0].Value = i + 1;
                    gridstate.Rows[i].Cells[1].Value = dtnew.Rows[i]["State"].ToString();
                    gridstate.Rows[i].Cells[2].Value = dtnew.Rows[i]["County"].ToString();
                    gridstate.Rows[i].Cells[3].Value = dtnew.Rows[i]["Vendor_Id"].ToString();
                    gridstate.Rows[i].Cells[4].Value = dtnew.Rows[i]["Vendor_State_Id"].ToString();
                }
                lbl_Total_State_County.Text = dtnew.Rows.Count.ToString();
            }
            else
            {
               // gridstate.Rows.Clear();
             
               // MessageBox.Show("No Records Found");
                //Bind_All_State();
               // txt_State_County.Text = "";
              
            }

            Bind_All_State();


        }

        private void grid_State_County_Click(object sender, EventArgs e)
        {

        }

        private void Vendor_Create_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_View vendorview = new Ordermanagement_01.Vendors.Vendor_View(Userid, User_Role);

            vendorview.Show();
         
            vendorview.Refresh();
        }

    }
}
