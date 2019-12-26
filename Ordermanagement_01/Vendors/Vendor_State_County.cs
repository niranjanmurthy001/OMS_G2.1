using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_State_County : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int County,  Check, Delvalue = 0;
        int Vendor_Id, User_Id, insertval = 0, Vendor_State_Id,checkstate;
        DataTable dtsel = new DataTable();
        DataTable dtselect = new DataTable();        
        DataTable dtnew = new DataTable();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtAdd_State = new DataTable();
        DataTable dt_grid_Add_State_County = new DataTable();
        static int currentpageindex = 0;
        int pagesize=15;

        string duplicate;

        public Vendor_State_County(int vendor_id,int userid,string vendorname)
        {
            InitializeComponent();
            Vendor_Id = vendor_id;
            User_Id = userid;
            lbl_VendorName.Text = vendorname;
        }

        private void ddl_Add_State_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_Add_State.SelectedIndex > 0)
            {
                Bind_County_State_Wise();
                Bind_Db_Grid();
                ddl_Vendor_State.SelectedIndex = 0;
            }
            else if (ddl_Add_State.SelectedIndex == 0)
            {
                Bind_All_State_County_For_Vendor();
                Bind_AllCountyVendor();
                //ddl_Vendor_State.SelectedIndex = 0;
                
            }
            else { }
            chk_All.Checked = false;
            chk_Db_county.Checked = false;
            //chk_All_CheckedChanged(sender, e);
        }

        private void Bind_AllCountyVendor()
        {
            if (ddl_Add_State.SelectedIndex == 0)
            {

                Hashtable ht_AllCounty = new Hashtable();
                DataTable dt_AllCounty = new DataTable();
                ht_AllCounty.Add("@Trans", "SELECT_STATE_COUNTY");
                ht_AllCounty.Add("@Vendor_Id", Vendor_Id);

                dt_AllCounty = dataaccess.ExecuteSP("Sp_Vendor_State_County", ht_AllCounty);

                if (dt_AllCounty.Rows.Count > 0)
                {
                    grd_County.Rows.Clear();
                    for (int i = 0; i < dt_AllCounty.Rows.Count; i++)
                    {
                        grd_County.Rows.Add();

                        grd_County.Rows[i].Cells[1].Value = dt_AllCounty.Rows[i]["County"].ToString();
                        grd_County.Rows[i].Cells[2].Value = dt_AllCounty.Rows[i]["County_ID"].ToString();
                    }

                }
                lbl_County.Text = dt_AllCounty.Rows.Count.ToString();
            }
        }

        private void Bind_County_State_Wise()
        {
            Hashtable ht_County = new Hashtable();
            DataTable dt_County = new DataTable();

            if (ddl_Add_State.SelectedIndex > 0)
            {
               
                ht_County.Add("@Trans", "BIND_COUNTY_FOR_STATE_WISE");
                ht_County.Add("@Vendor_Id", Vendor_Id);
                ht_County.Add("@State_ID", int.Parse(ddl_Add_State.SelectedValue.ToString()));

                dt_County = dataaccess.ExecuteSP("Sp_Vendor_State_County", ht_County);

                if (dt_County.Rows.Count > 0)
                {
                    grd_County.Rows.Clear();
                    for (int i = 0; i < dt_County.Rows.Count; i++)
                    {
                        grd_County.Rows.Add();

                        grd_County.Rows[i].Cells[1].Value = dt_County.Rows[i]["County"].ToString();
                        grd_County.Rows[i].Cells[2].Value = dt_County.Rows[i]["County_ID"].ToString();
                    }

                }
                else
                {
                    grd_County.Rows.Clear();

                }
            }
            lbl_County.Text = dt_County.Rows.Count.ToString();
        }

        private void Bind_County_for_State()
        {
            if (ddl_Add_State.SelectedIndex > 0)
            {
                
                Hashtable htParam = new Hashtable();
                DataTable dt = new DataTable();
                htParam.Add("@Trans", "SELECT COUNTY");
                htParam.Add("@State_ID", int.Parse(ddl_Add_State.SelectedValue.ToString()));

                dt = dataaccess.ExecuteSP("Sp_Genral", htParam);

                if (dt.Rows.Count > 0)
                {
                    grd_County.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_County.Rows.Add();

                        grd_County.Rows[i].Cells[1].Value = dt.Rows[i]["County"].ToString();
                        grd_County.Rows[i].Cells[2].Value = dt.Rows[i]["County_ID"].ToString();
                    }

                }
                Bind_Db_Grid();
            }
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {

                for (int i = 0; i < grd_County.Rows.Count; i++)
                {

                    grd_County[0, i].Value = true;
                }
            }
            else if (chk_All.Checked == false)
            {

                for (int i = 0; i < grd_County.Rows.Count; i++)
                {

                    grd_County[0, i].Value = false;
                }
            }
        }

        private void Bind_Db_Grid()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            if (ddl_Add_State.SelectedIndex > 0)
            {
                
                htsel.Add("@Trans", "SEL_VENDOR_STATE");
                htsel.Add("@Vendor_Id", Vendor_Id);
                htsel.Add("@State", int.Parse(ddl_Add_State.SelectedValue.ToString()));
                dtsel = dataaccess.ExecuteSP("Sp_Vendor_State_County", htsel);
              
                if (dtsel.Rows.Count > 0)
                {
                    grd_State_county.Rows.Clear();
                    for (int i = 0; i < dtsel.Rows.Count; i++)
                    {
                        grd_State_county.Rows.Add();
                        grd_State_county.Rows[i].Cells[1].Value = i + 1;
                        grd_State_county.Rows[i].Cells[2].Value = dtsel.Rows[i]["State"].ToString();
                        grd_State_county.Rows[i].Cells[3].Value = dtsel.Rows[i]["County"].ToString();
                        grd_State_county.Rows[i].Cells[4].Value = dtsel.Rows[i]["Vendor_Id"].ToString();
                        grd_State_county.Rows[i].Cells[5].Value = dtsel.Rows[i]["Vendor_State_Id"].ToString();
                    }
                }
                else
                {
                    grd_State_county.Rows.Clear();
                }
              
            }
            lbl_State_County.Text = dtsel.Rows.Count.ToString();

        }

        private void Bind_All_State_County_For_Vendor()
        {
            Hashtable htsel_sc_vend = new Hashtable();
            DataTable dtsel_sc_vend = new DataTable();
            if (ddl_Add_State.SelectedIndex == 0)
            {           
                htsel_sc_vend.Add("@Trans", "SELECT_ALL_STATE_COUNTY_FOR_VENDOR");
                htsel_sc_vend.Add("@Vendor_Id", Vendor_Id);
                dtsel_sc_vend = dataaccess.ExecuteSP("Sp_Vendor_State_County", htsel_sc_vend);
                dtAdd_State = dtsel_sc_vend;
                if (dtsel_sc_vend.Rows.Count > 0)
                {
                    grd_State_county.Rows.Clear();
                    for (int i = 0; i < dtsel_sc_vend.Rows.Count; i++)
                    {
                        grd_State_county.Rows.Add();
                        grd_State_county.Rows[i].Cells[1].Value = i + 1;
                        grd_State_county.Rows[i].Cells[2].Value = dtsel_sc_vend.Rows[i]["State"].ToString();
                        grd_State_county.Rows[i].Cells[3].Value = dtsel_sc_vend.Rows[i]["County"].ToString();
                        grd_State_county.Rows[i].Cells[4].Value = dtsel_sc_vend.Rows[i]["Vendor_Id"].ToString();
                        grd_State_county.Rows[i].Cells[5].Value = dtsel_sc_vend.Rows[i]["Vendor_State_Id"].ToString();
                    }
                    lbl_State_County.Text = dtsel_sc_vend.Rows.Count.ToString();
                }
                else
                {
                    grd_State_county.Rows.Clear();
                }
               
            }
            lbl_Total_Orders.Text = dtsel_sc_vend.Rows.Count.ToString();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (Vendor_Id != 0 || lbl_VendorName.Text != "")
            {
                
                for (int i = 0; i < grd_County.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_County[0, i].FormattedValue;
                    if (isChecked == true)
                    {
                        County = int.Parse(grd_County.Rows[i].Cells[2].Value.ToString());

                        Hashtable hscheck = new Hashtable();
                        DataTable dtcheck = new System.Data.DataTable();


                        hscheck.Add("@Trans", "CHECK_STATE");
                        hscheck.Add("@Vendor_Id", Vendor_Id);
                        hscheck.Add("@County", County);
                        hscheck.Add("@State", int.Parse(ddl_Add_State.SelectedValue.ToString()));
                        dtcheck = dataaccess.ExecuteSP("Sp_Vendor_State_County", hscheck);

                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                        if (Check == 0)
                        {

                            Hashtable hsforSP = new Hashtable();
                            DataTable dt = new System.Data.DataTable();

                            //Insert
                            hsforSP.Add("@Trans", "INSERT_STATE_COUNTY");
                            hsforSP.Add("@Vendor_Id", Vendor_Id);
                            hsforSP.Add("@State", int.Parse(ddl_Add_State.SelectedValue.ToString()));
                            hsforSP.Add("@County", County);

                            hsforSP.Add("@Inserted_By", User_Id);
                            hsforSP.Add("@Inserted_Date", DateTime.Now);
                            hsforSP.Add("@Status", "True");
                            dt = dataaccess.ExecuteSP("Sp_Vendor_State_County", hsforSP);
                            insertval = 1;
                            isChecked = false;
                        }
                        
                    }
                }
                if (insertval == 1)
                {
                    MessageBox.Show("State county Record inserted successfully");
                    insertval = 0;
                    Bind_Db_Grid();
                    Bind_County_State_Wise();

                    chk_All.Checked = false;
                    Get_Vendor_Added_Sate();

              //    Bind_All_State_Info();
                  //  Bind_Filter_State();

                    dbc.Bind_Added_State_For_Vendor(ddl_Vendor_State, Vendor_Id);

                    chk_All_CheckedChanged( sender,  e);
                    Bind_All_State();
                 
                }
                else
                {
                    insertval = 0;
                }
            }
            else
            {
                MessageBox.Show("Select Vendor Name");
            }
        }


        private void Get_Vendor_Added_Sate()
        {

            Hashtable htParam = new Hashtable();

            htParam.Add("@Trans", "SELECT_ADDED_STATE");
            htParam.Add("@Vendor_Id", Vendor_Id);
            dt = dataaccess.ExecuteSP("Sp_Vendor_State_County", htParam);

            dtAdd_State = dt;
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to delete Vendor client Subclient", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                for (int client = 0; client < grd_State_county.Rows.Count; client++)
                {
                    bool isclient = (bool)grd_State_county[0, client].FormattedValue;
                    if (isclient)
                    {
                        Hashtable htdel = new Hashtable();
                        DataTable dtdel = new DataTable();
                        htdel.Add("@Trans", "DELETE");
                        htdel.Add("@Vendor_State_Id", int.Parse(grd_State_county.Rows[client].Cells[5].Value.ToString()));
                        dtdel = dataaccess.ExecuteSP("Sp_Vendor_State_County", htdel);
                        
                    }
                }

                MessageBox.Show("Vendor client/subclient info Deleted Successfully");
                Bind_County_State_Wise();
                Bind_Db_Grid();
                chk_Db_county.Checked = false;
                chk_Db_county_CheckedChanged(sender, e);
                dbc.Bind_Added_State_For_Vendor(ddl_Vendor_State, Vendor_Id);
            }
            else
            {

            }


            //for (int i = 0; i < grd_State_county.Rows.Count; i++)
            //{
            //    bool isChecked = (bool)grd_State_county[0, i].FormattedValue;
            //    if (isChecked == true)
            //    {
            //        Vendor_State_Id = int.Parse(grd_State_county.Rows[i].Cells[5].Value.ToString());
            //        Hashtable htdel = new Hashtable();
            //        DataTable dtdel = new DataTable();
            //        htdel.Add("@Trans", "DELETE");
            //        htdel.Add("@Vendor_State_Id", Vendor_State_Id);

            //        dbc.Bind_Added_State_For_Vendor(ddl_Vendor_State, Vendor_Id);
            //        dtdel = dataaccess.ExecuteSP("Sp_Vendor_State_County", htdel);
            //        Delvalue = 1;
            //    }
            //}
            //if (Delvalue == 1)
            //{
            //    MessageBox.Show("Record Deleted Successfully");
                
            //    Bind_County_State_Wise();
            //    Bind_Db_Grid();

            //    chk_Db_county.Checked = false;

            //    chk_Db_county_CheckedChanged( sender,  e);

            //    Get_Vendor_Added_Sate();

            //    Bind_All_State_Info();

            //    Delvalue = 0;
            //    dbc.Bind_Added_State_For_Vendor(ddl_Vendor_State, Vendor_Id);
            //}
            //else
            //{
            //    MessageBox.Show("Kindly Select the record to delete");
            //    Delvalue = 0;
            //}
           
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
           Bind_All_State_Info();
           Bind_AllCountyVendor();
          
        }

        private void Bind_Filter_data()
        {
            DataView dtsearch = new DataView(dtselect);

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
                    if (temptable.Rows[i]["Availability"].ToString() == "True")
                    {
                        //Column3.ThreeState = true;
                        gridstate.Rows[i].Cells[3].Value = bool.Parse(temptable.Rows[i]["Availability"].ToString());
                    }
                    else
                    {
                        gridstate.Rows[i].Cells[3].Value = bool.Parse(temptable.Rows[i]["Availability"].ToString());
                    }
                    gridstate.Rows[i].Cells[4].Value = temptable.Rows[i]["Vendor_Id"].ToString();
                    gridstate.Rows[i].Cells[5].Value = temptable.Rows[i]["Vendor_State_Id"].ToString();
                }
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
            }
            else
            {
                gridstate.Rows.Clear();
                MessageBox.Show("No Records Found");
                Bind_All_State();
                txt_State_County.Text = "";
            }
            //lbl_Total_Orders.Text = dt.Rows.Count.ToString();

            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
        }

        private void txt_State_County_TextChanged(object sender, EventArgs e)
        {
            if (txt_State_County.Text != "" && txt_State_County.Text != "Search by State County...")
            {
                Bind_Filter_data();
            }
            else
            {
                Bind_All_State();
            }
        }

        private void Get_Row_Table_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void Get_Row_Table_Search_1(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt1.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void Bind_All_State()
        {

            DataView dtview = new DataView(dtselect);
            dt = dtview.ToTable();
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
                    if (temptable.Rows[i]["Availability"].ToString() == "True")
                    {
                        //Column3.ThreeState = true;
                        gridstate.Rows[i].Cells[3].Value = bool.Parse(temptable.Rows[i]["Availability"].ToString());
                    }
                    else
                    {
                        //Column3.ThreeState = false;
                        gridstate.Rows[i].Cells[3].Value = bool.Parse(temptable.Rows[i]["Availability"].ToString());
                    }
                    gridstate.Rows[i].Cells[4].Value = temptable.Rows[i]["Vendor_Id"].ToString();
                    gridstate.Rows[i].Cells[5].Value = temptable.Rows[i]["Vendor_State_Id"].ToString();
                }
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
            }
            else
            {
                gridstate.Rows.Clear();

            }
            //lbl_Total_Orders.Text = dt.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
        }
             

        private void Bind_All_State_Info()
        {
            Hashtable htsel = new Hashtable();
           Hashtable htnew=new Hashtable();
            htsel.Add("@Trans", "SELECT_STATE_COUNTY");
            htsel.Add("@Vendor_Id", Vendor_Id);
            dtselect.Rows.Clear();
            dtselect = dataaccess.ExecuteSP("Sp_Vendor_State_County", htsel);

            htnew.Add("@Trans", "SELECT_AVAIL_TRUE");
            htnew.Add("@Vendor_Id", Vendor_Id);

            dtnew = dataaccess.ExecuteSP("Sp_Vendor_State_County", htnew);

            dtAdd_State = dtnew;

            dt_grid_Add_State_County = dtselect;

            if (dtnew.Rows.Count > 0)
            {
                grd_State_county.Rows.Clear();
                
                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    grd_State_county.Rows.Add();
                    grd_State_county.Rows[i].Cells[1].Value = i + 1;
                    grd_State_county.Rows[i].Cells[2].Value = dtnew.Rows[i]["State"].ToString();
                    grd_State_county.Rows[i].Cells[3].Value = dtnew.Rows[i]["County"].ToString();
                    grd_State_county.Rows[i].Cells[4].Value = dtnew.Rows[i]["Vendor_Id"].ToString();
           
                    grd_State_county.Rows[i].Cells[5].Value = dtnew.Rows[i]["Vendor_State_Id"].ToString();
                }
            }
            else
            {
                grd_State_county.Rows.Clear();
            }

            lbl_State_County.Text = dtselect.Rows.Count.ToString();
            lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();

            Bind_All_State();
            

        }

        private void Vendor_State_County_Load(object sender, EventArgs e)
        {
            dbc.BindState_For_Vendor(ddl_Add_State,Vendor_Id);
            dbc.Bind_Added_State_For_Vendor(ddl_Vendor_State, Vendor_Id);

            Get_Vendor_Added_Sate();

            Bind_All_State_Info();
            Bind_All_State();

            txt_State_County.Select();

            btnFirst_Click(sender, e);
            
        }

        private void btn_Import_State_County_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Import_State_County Import_State = new Ordermanagement_01.Vendors.Import_State_County(Vendor_Id,User_Id,lbl_VendorName.Text);
            Import_State.Show();
        }

        private void chk_Db_county_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Db_county.Checked == true)
            {
                for (int i = 0; i < grd_State_county.Rows.Count; i++)
                {

                    grd_State_county[0, i].Value = true;
                }
            }
            else if (chk_Db_county.Checked == false)
            {
                for (int i = 0; i < grd_State_county.Rows.Count; i++)
                {

                    grd_State_county[0, i].Value = false;
                }
            }
            chk_All.Checked = false;
          
        }

        private void txt_State_County_Enter(object sender, EventArgs e)
        {
            txt_State_County.Text = "";
        }

        private void gridstate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (gridstate.Rows[e.RowIndex].Cells[5].Value != "" && gridstate.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    Vendor_State_Id = int.Parse(gridstate.Rows[e.RowIndex].Cells[5].Value.ToString());

                    if (Vendor_State_Id != 0)
                    {
                        Hashtable htchk = new Hashtable();
                        DataTable dtchk = new DataTable();
                        htchk.Add("@Trans", "CHK_AVAILABLE");
                        htchk.Add("@Vendor_State_Id", Vendor_State_Id);
                        dtchk = dataaccess.ExecuteSP("Sp_Vendor_State_County", htchk);
                        if (dtchk.Rows.Count > 0)
                        {
                            checkstate = int.Parse(dtchk.Rows[0]["Count"].ToString());
                            if (checkstate != 0)
                            {
                                Hashtable htavail = new Hashtable();
                                DataTable dtavail = new DataTable();
                                htavail.Add("@Trans", "AVAILIBLE_FALSE");
                                htavail.Add("@Vendor_State_Id", Vendor_State_Id);
                                dtavail = dataaccess.ExecuteSP("Sp_Vendor_State_County", htavail);
                                MessageBox.Show("State County Removed Successfully");
                            }
                            else if (checkstate == 0)
                            {
                                Hashtable htavail = new Hashtable();
                                DataTable dtavail = new DataTable();
                                htavail.Add("@Trans", "AVAILIBLE_TRUE");
                                htavail.Add("@Vendor_State_Id", Vendor_State_Id);
                                dtavail = dataaccess.ExecuteSP("Sp_Vendor_State_County", htavail);
                                MessageBox.Show("State County Added Successfully");
                            }
                        }
                        
                       
                   }
                }
            }
            Bind_All_State_Info();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentpageindex++;
            if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;
                //Bind_Filter_data();
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                btnPrevious.Enabled = true; 
                btnFirst.Enabled = true;
                if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
                {
                    Bind_Filter_data();
                }
            }
            if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
            {
                Bind_Filter_data();
            }
            else
            {
                Bind_All_State();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text != "")
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
                Bind_Filter_data();
            }
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
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
            if (txt_State_County.Text != "Search by State County..." && txt_State_County.Text !="")
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  ddl_Add_State.Focus();
            //ddl_Add_State.SelectedIndex = 0;
           // Bind_All_State_County_For_Vendor();
            //Bind_All_State();
           // Bind_All_State_Info();

            if (tabControl1.SelectedIndex == 0)
            {
                Bind_All_State();
                txt_State_County.Select();
                ddl_Add_State.SelectedIndex = 0;
                ddl_Vendor_State.SelectedIndex = 0;
            }

            else
            {
                ddl_Add_State.Focus();
                Bind_All_State_County_For_Vendor();
                Bind_All_State_Info();
                txt_State_County.Text = "";

            }

        }

        private void ddl_Vendor_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Vendor_State.SelectedIndex > 0)
            {

                Bind_Filter_State();
              //  ddl_Add_State.SelectedIndex = 0;
                Bind_AllCountyVendor();
            }
            else
            {

                Bind_All_State_County_For_Vendor();
               // Bind_AllCountyVendor();
               
            }
            chk_All.Checked = false;
            chk_Db_county.Checked = false;
           
        }


        private void Bind_Filter_State()
        {

            Hashtable ht_Bind_Filter_State_County = new Hashtable();
            DataTable dt_Bind_Filter_StateCounty = new DataTable();

            ht_Bind_Filter_State_County.Add("@Trans", "SELECT_STATE_COUNTY");
            ht_Bind_Filter_State_County.Add("@Vendor_Id", Vendor_Id);

            dt_Bind_Filter_StateCounty = dataaccess.ExecuteSP("Sp_Vendor_State_County", ht_Bind_Filter_State_County);




          //  DataView dtsearch = new DataView(dt_grid_Add_State_County);


            DataView dtsearch = new DataView(dt_Bind_Filter_StateCounty);

            var search = ddl_Vendor_State.SelectedValue.ToString();
            dtsearch.RowFilter = "State_ID ="+search.ToString()+" ";
            dt = dtsearch.ToTable();

            if (dt.Rows.Count > 0)
            {
                grd_State_county.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_State_county.Rows.Add();

                    grd_State_county.Rows[i].Cells[1].Value = i + 1;
                    grd_State_county.Rows[i].Cells[2].Value = dt.Rows[i]["State"].ToString();
                    grd_State_county.Rows[i].Cells[3].Value = dt.Rows[i]["County"].ToString();
                    grd_State_county.Rows[i].Cells[4].Value = dt.Rows[i]["Vendor_Id"].ToString();
                    grd_State_county.Rows[i].Cells[5].Value = dt.Rows[i]["Vendor_State_Id"].ToString();
                }
                lbl_State_County.Text = dt.Rows.Count.ToString();
            }
            //else
            //{
            //    //grd_State_county.Rows.Clear();
            //    //MessageBox.Show("No Records Found");
            //    //Bind_All_State_Info();
            //    //ddl_Vendor_State.SelectedIndex = 0;
            //}
            //lbl_State_County.Text = dt.Rows.Count.ToString();
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            dbc.BindState_For_Vendor(ddl_Add_State, Vendor_Id);
            dbc.Bind_Added_State_For_Vendor(ddl_Vendor_State, Vendor_Id);

            chk_All_CheckedChanged(sender,e);

            chk_Db_county_CheckedChanged(sender, e);

            Bind_All_State_Info();

            chk_All.Checked = false;

            chk_Db_county.Checked = false;
           
        }
   

    }
}
