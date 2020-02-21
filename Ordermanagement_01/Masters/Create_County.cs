using ClosedXML.Excel;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Ordermanagement_01.Masters
{
    public partial class Create_County : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_Id, County_Id;
        DataTable dtselect = new DataTable();
        DataTable dt = new DataTable();
        System.Data.DataTable dtcounty = new System.Data.DataTable();
        System.Data.DataTable dtsort = new System.Data.DataTable();
        static int currentpageindex = 0;
        int pagesize = 50, State_ID;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string path1;
        public Create_County(int USER_ID)
        {
            InitializeComponent();
            User_Id = USER_ID;
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Masters.County_Import cr = new Masters.County_Import(User_Id);
            cr.Show();
        }

        private void Create_County_Load(object sender, EventArgs e)
        {
            dbc.BindState(ddl_State);

            BindGridCounty();
            ddl_State.Focus();
            clear();
            dbc.BindState(ddl_SearchbyState);
            dbc.BindCounty(ddl_searchCounty, int.Parse(ddl_SearchbyState.SelectedValue.ToString()));

        }

        private void GetrowTable(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void Grid_County_Bind()
        {
            ddl_SearchbyState.SelectedIndex = 0;
            //  ddl_searchCounty.SelectedIndex = 0;
            form_loader.Start_progres();
            grd_County.Rows.Clear();
            Hashtable htselect = new Hashtable();


            if (ddl_SearchbyState.SelectedIndex > 0)
            {
                htselect.Add("@Trans", "SEARCH_BYSTATE");
                htselect.Add("@State_Id", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                if (ddl_searchCounty.SelectedIndex > 0)
                {
                    htselect.Clear();
                    htselect.Add("@Trans", "SEARCH_BYCOUNTY");

                    htselect.Add("@State_Id", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                    htselect.Add("@County_ID", int.Parse(ddl_searchCounty.SelectedValue.ToString()));
                }
            }
            else
            {
                htselect.Add("@Trans", "SELECT_All");

            }
            dtselect = dataaccess.ExecuteSP("Sp_County", htselect);


            System.Data.DataTable temptable = dtselect.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtselect.Rows.Count)
            {
                endindex = dtselect.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetrowTable(ref row, dtselect.Rows[i]);
                temptable.Rows.Add(row);
            }


            if (temptable.Rows.Count > 0)
            {
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_County.Rows.Add();
                    grd_County.Rows[i].Cells[0].Value = i + 1;
                    grd_County.Rows[i].Cells[1].Value = temptable.Rows[i]["State_Name"].ToString();
                    grd_County.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                    grd_County.Rows[i].Cells[3].Value = temptable.Rows[i]["County_Type"].ToString();
                    grd_County.Rows[i].Cells[4].Value = temptable.Rows[i]["State_ID"].ToString();
                    grd_County.Rows[i].Cells[5].Value = temptable.Rows[i]["County_ID"].ToString();
                    grd_County.Rows[i].Cells[6].Value = "View";
                    grd_County.Rows[i].Cells[7].Value = "Delete";

                    grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_County.Rows.Clear();
                grd_County.Visible = true;
                grd_County.DataSource = null;
            }
            lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);
        }

        private void BindGridCounty()
        {
            //grd_County.Rows.Clear();
            //Hashtable htselect = new Hashtable();

            //htselect.Add("@Trans", "SELECT_All");
            //dtselect = dataaccess.ExecuteSP("Sp_County_Master", htselect);

            form_loader.Start_progres();
            grd_County.Rows.Clear();
            Hashtable htselect = new Hashtable();


            if (ddl_SearchbyState.SelectedIndex > 0)
            {
                htselect.Add("@Trans", "SEARCH_BYSTATE");
                htselect.Add("@State_ID", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                if (ddl_searchCounty.SelectedIndex > 0)
                {
                    htselect.Clear();
                    htselect.Add("@Trans", "SEARCH_BYCOUNTY");

                    htselect.Add("@State_ID", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                    htselect.Add("@County_ID", int.Parse(ddl_searchCounty.SelectedValue.ToString()));
                }
            }
            else
            {
                htselect.Add("@Trans", "SELECT_All");

            }
            dtselect = dataaccess.ExecuteSP("Sp_County", htselect);


            System.Data.DataTable temptable = dtselect.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtselect.Rows.Count)
            {
                endindex = dtselect.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetrowTable(ref row, dtselect.Rows[i]);
                temptable.Rows.Add(row);
            }


            if (temptable.Rows.Count > 0)
            {
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_County.Rows.Add();
                    grd_County.Rows[i].Cells[0].Value = i + 1;
                    grd_County.Rows[i].Cells[1].Value = temptable.Rows[i]["State_Name"].ToString();
                    grd_County.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                    grd_County.Rows[i].Cells[3].Value = temptable.Rows[i]["County_Type"].ToString();
                    grd_County.Rows[i].Cells[4].Value = temptable.Rows[i]["State_ID"].ToString();
                    grd_County.Rows[i].Cells[5].Value = temptable.Rows[i]["County_ID"].ToString();
                    grd_County.Rows[i].Cells[6].Value = "View";
                    grd_County.Rows[i].Cells[7].Value = "Delete";

                    grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);
            }
            else
            {
                grd_County.Rows.Clear();
                grd_County.Visible = true;
                grd_County.DataSource = null;
            }
            //lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
            //lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
            btn_searchClear_Click(sender, e);
        }

        private void clear()
        {
            ddl_State.SelectedIndex = 0;
            ddl_CountyType.SelectedIndex = 0;
            txt_County.Text = "";
            County_Id = 0;
            btn_Submit.Text = "Add";
        }

        private bool Validation()
        {
            string title = "Validation!";
            if (ddl_State.SelectedIndex == 0)
            {
                MessageBox.Show("Select State name", title);
                ddl_State.Focus();
                return false;
            }
            else if (ddl_CountyType.SelectedIndex == 0)
            {
                MessageBox.Show("Select County Type", title);
                ddl_CountyType.Focus();
                return false;
            }
            else if (txt_County.Text == "")
            {
                MessageBox.Show("Enter County name", title);
                txt_County.Focus();
                return false;
            }
            return true;
        }

        private bool Validate_Duplicate()
        {
            State_ID = int.Parse(ddl_State.SelectedValue.ToString());

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "DUPLICATE_COUNTY");
            ht.Add("@State_Id", State_ID);
            dt = dataaccess.ExecuteSP("Sp_County", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (txt_County.Text == dt.Rows[i]["County"].ToString())
                {
                    string title1 = "Exist!";
                    MessageBox.Show("County Name Already Exist", title1);
                    return false;

                }

            }
            return true;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (County_Id == 0 && Validation() != false && Validate_Duplicate() != false)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new DataTable();
                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@State_ID", ddl_State.SelectedValue);
                htinsert.Add("@County_Type", ddl_CountyType.Text);
                htinsert.Add("@County_Name", txt_County.Text);
                htinsert.Add("@Inserted_by", User_Id);
                htinsert.Add("@Inserted_Date", DateTime.Now);
                htinsert.Add("@Status", "True");
                dtinsert = dataaccess.ExecuteSP("Sp_County_Master", htinsert);
                string title = "Insert";

                MessageBox.Show("County Added Successfully", title);
                County_Id = 0;
                BindGridCounty();
                clear();
            }
            else if (Validation() != false)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new DataTable();
                htinsert.Add("@Trans", "UPDATE");
                htinsert.Add("@County_ID", County_Id);
                htinsert.Add("@State_ID", ddl_State.SelectedValue);
                htinsert.Add("@County_Type", ddl_CountyType.Text);
                htinsert.Add("@County_Name", txt_County.Text);
                htinsert.Add("@Modified_by", User_Id);
                htinsert.Add("@Modified_Date", DateTime.Now);
                htinsert.Add("@Status", "True");
                dtinsert = dataaccess.ExecuteSP("Sp_County_Master", htinsert);
                string title = "Update";
                MessageBox.Show("County Updated Successfully", title);
                County_Id = 0;

                BindGridCounty();
                clear();
            }

        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //BindGridCounty();
            Grid_County_Bind();
            clear();

            ddl_SearchbyState.SelectedIndex = 0;
            btn_searchClear_Click(sender, e);

            dbc.BindState(ddl_SearchbyState);
            dbc.BindCounty(ddl_searchCounty, int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
        }

        private void grd_County_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 6)
                {
                    //view code
                    int Stateid = int.Parse(grd_County.Rows[e.RowIndex].Cells[4].Value.ToString());
                    County_Id = int.Parse(grd_County.Rows[e.RowIndex].Cells[5].Value.ToString());
                    if (Stateid != 0 && County_Id != 0)
                    {
                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new DataTable();
                        htselect.Add("@Trans", "SELECT");
                        htselect.Add("@State_ID", Stateid);
                        htselect.Add("@County_ID", County_Id);
                        dtselect = dataaccess.ExecuteSP("Sp_County_Master", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            ddl_State.SelectedValue = dtselect.Rows[0]["State_ID"].ToString();
                            ddl_CountyType.Text = dtselect.Rows[0]["County_Type"].ToString();
                            txt_County.Text = dtselect.Rows[0]["County"].ToString();
                            btn_Submit.Text = "Edit";
                        }
                    }
                }
                else if (e.ColumnIndex == 7)
                {
                    //Delete code

                    DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        int Stateid = int.Parse(grd_County.Rows[e.RowIndex].Cells[4].Value.ToString());
                        County_Id = int.Parse(grd_County.Rows[e.RowIndex].Cells[5].Value.ToString());
                        if (Stateid != 0 && County_Id != 0)
                        {
                            string County_name = grd_County.Rows[e.RowIndex].Cells[2].Value.ToString();
                            Hashtable htdel = new Hashtable();
                            DataTable dtdel = new DataTable();
                            htdel.Add("@Trans", "DELETE");
                            htdel.Add("@State_ID", Stateid);
                            htdel.Add("@County_ID", County_Id);
                            dtdel = dataaccess.ExecuteSP("Sp_County_Master", htdel);
                            MessageBox.Show(" ' " + County_name + " ' " + " Record Deleted Successfully");
                            clear();
                            BindGridCounty();
                            County_Id = 0;
                            Stateid = 0;
                            //ddl_State.SelectedIndex = 0;
                            //ddl_CountyType.SelectedIndex = 0;

                            btn_searchClear_Click(sender, e);
                        }
                        //BindGridCounty();
                        // clear();
                    }
                    //clear();
                }
            }
        }

        private void ddl_SearchbyState_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_SearchbyState.SelectedIndex != 0)
            {
                dbc.BindCounty(ddl_searchCounty, int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                //DataView dtsearch = new DataView(dtselect);
                //dtsearch.RowFilter = "State like '%" + ddl_SearchbyState.Text.ToString().ToString() + "%' ";
                //dt = dtsearch.ToTable();


                Hashtable htsort = new Hashtable();

                htsort.Add("@Trans", "SEARCH_BYSTATE");
                //htsort.Add("@State", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                htsort.Add("@State_Id", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                //Bind_Grid_Tax_PageIndex();

                dtsort = dataaccess.ExecuteSP("Sp_County", htsort);

                System.Data.DataTable temptable = dtsort.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dtsort.Rows.Count)
                {
                    endindex = dtsort.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetrowTable_Client(ref row, dtsort.Rows[i]);
                    temptable.Rows.Add(row);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_County.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_County.Rows.Add();
                        grd_County.Rows[i].Cells[0].Value = i + 1;
                        grd_County.Rows[i].Cells[1].Value = temptable.Rows[i]["State_Name"].ToString();
                        grd_County.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                        grd_County.Rows[i].Cells[3].Value = temptable.Rows[i]["County_Type"].ToString();
                        grd_County.Rows[i].Cells[4].Value = temptable.Rows[i]["State_ID"].ToString();
                        grd_County.Rows[i].Cells[5].Value = temptable.Rows[i]["County_ID"].ToString();
                        grd_County.Rows[i].Cells[6].Value = "View";
                        grd_County.Rows[i].Cells[7].Value = "Delete";

                        grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                else
                {
                    // grd_County.Rows.Clear();
                    grd_County.Visible = true;
                    grd_County.DataSource = null;
                    MessageBox.Show("No Records Found");
                    //BindGridCounty();
                }

                lbl_Total_Orders.Text = dtsort.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize);

                //System.Data.DataTable temptable = dt.Clone();
                //int startindex = currentpageindex * pagesize;
                //int endindex = currentpageindex * pagesize + pagesize;
                //if (endindex > dt.Rows.Count)
                //{
                //    endindex = dt.Rows.Count;
                //}
                //for (int i = startindex; i < endindex; i++)
                //{
                //    DataRow row = temptable.NewRow();
                //    GetrowTable_Client(ref row, dt.Rows[i]);
                //    temptable.Rows.Add(row);
                //}

                //if (temptable.Rows.Count > 0)
                //{
                //    grd_County.Rows.Clear();
                //    for (int i = 0; i < temptable.Rows.Count; i++)
                //    {
                //        grd_County.Rows.Add();
                //        grd_County.Rows[i].Cells[0].Value = i + 1;
                //        grd_County.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                //        grd_County.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                //        grd_County.Rows[i].Cells[3].Value = temptable.Rows[i]["County_Type"].ToString();
                //        grd_County.Rows[i].Cells[4].Value = temptable.Rows[i]["State_ID"].ToString();
                //        grd_County.Rows[i].Cells[5].Value = temptable.Rows[i]["County_ID"].ToString();
                //        grd_County.Rows[i].Cells[6].Value = "View";
                //        grd_County.Rows[i].Cells[7].Value = "Delete";
                //    }

                //}
                //else
                //{
                //    grd_County.Visible = true;
                //    grd_County.DataSource = null;

                //}
                ////lbl_Total_Orders.Text = "Total Orders: " + dt.Rows.Count.ToString();
                //lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                //lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);


                // First_Page();
            }
            //First_Page();
        }

        private void GetrowTable_Client(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtsort.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void GetrowTableOfCounty_Client(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtcounty.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void btn_searchSub_Click(object sender, EventArgs e)
        {
            if (ddl_SearchbyState.Text != "Select" || ddl_SearchbyState.Text != "" && ddl_searchCounty.Text != "Select" || ddl_searchCounty.Text != "")
            {
                DataView dtsearch = new DataView(dtselect);

                dtsearch.RowFilter = "State like '%" + ddl_SearchbyState.Text.ToString().ToString() + "%' and County like '%" + ddl_SearchbyState.Text.ToString().ToString() + "%' ";


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
                    GetrowTable_Client(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_County.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_County.Rows.Add();
                        grd_County.Rows[i].Cells[0].Value = i + 1;
                        grd_County.Rows[i].Cells[1].Value = temptable.Rows[i]["State_Name"].ToString();
                        grd_County.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                        grd_County.Rows[i].Cells[3].Value = temptable.Rows[i]["County_Type"].ToString();
                        grd_County.Rows[i].Cells[4].Value = temptable.Rows[i]["State_ID"].ToString();
                        grd_County.Rows[i].Cells[5].Value = temptable.Rows[i]["County_ID"].ToString();
                        grd_County.Rows[i].Cells[6].Value = "View";
                        grd_County.Rows[i].Cells[7].Value = "Delete";

                        grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                else
                {
                    grd_County.Visible = true;
                    grd_County.DataSource = null;

                }
                //lbl_Total_Orders.Text = "Total Orders: " + dt.Rows.Count.ToString();
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);



                //Hashtable ht = new Hashtable();

                //ht.Add("@Trans", "SEARCH_BYCOUNTY");
                //ht.Add("@State_ID", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                //ht.Add("@County_ID", int.Parse(ddl_searchCounty.SelectedValue.ToString()));
                //dt = dataaccess.ExecuteSP("Sp_County_Master", ht);
                //if (dt.Rows.Count > 0)
                //{
                //    grd_County.Rows.Clear();
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        grd_County.Rows.Add();
                //        grd_County.Rows[i].Cells[0].Value = i + 1;
                //        grd_County.Rows[i].Cells[1].Value = dt.Rows[i]["State"].ToString();
                //        grd_County.Rows[i].Cells[2].Value = dt.Rows[i]["County"].ToString();
                //        grd_County.Rows[i].Cells[3].Value = dt.Rows[i]["County_Type"].ToString();
                //        grd_County.Rows[i].Cells[4].Value = dt.Rows[i]["State_ID"].ToString();
                //        grd_County.Rows[i].Cells[5].Value = dt.Rows[i]["County_ID"].ToString();
                //        grd_County.Rows[i].Cells[6].Value = "View";
                //        grd_County.Rows[i].Cells[7].Value = "Delete";
                //    }
                //}
            }
        }

        private void btn_searchClear_Click(object sender, EventArgs e)
        {
            ddl_SearchbyState.SelectedIndex = 0;
            if (ddl_searchCounty.SelectedIndex != -1)
            {
                ddl_searchCounty.SelectedIndex = 0;
            }
            BindGridCounty();

            dbc.BindState(ddl_SearchbyState);
            dbc.BindCounty(ddl_searchCounty, int.Parse(ddl_SearchbyState.SelectedValue.ToString()));

            clear();
        }

        private void ddl_searchCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_SearchbyState.Text != "Select" || ddl_SearchbyState.Text != "" && ddl_searchCounty.Text != "Select" || ddl_searchCounty.Text != "")
            //{
            //    DataView dtsearch = new DataView(dtselect);

            //    dtsearch.RowFilter = "State like '%" + ddl_SearchbyState.Text.ToString().ToString() + "%' and County like '%" + ddl_searchCounty.Text.ToString().ToString() + "%' ";


            //    dt = dtsearch.ToTable();
            //    System.Data.DataTable temptable = dt.Clone();

            if (ddl_SearchbyState.SelectedIndex > 0)
            {
                if (ddl_searchCounty.SelectedIndex > 0)
                {
                    //grd_County.Rows.Clear();
                    form_loader.Start_progres();
                    //progBar.startProgress();
                    Hashtable ht_County = new Hashtable();

                    ht_County.Add("@Trans", "SEARCH_BYCOUNTY");
                    //ht_County.Add("@State", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                    //ht_County.Add("@County", int.Parse(ddl_searchCounty.SelectedValue.ToString()));

                    ht_County.Add("@State_ID", int.Parse(ddl_SearchbyState.SelectedValue.ToString()));
                    ht_County.Add("@County_ID", int.Parse(ddl_searchCounty.SelectedValue.ToString()));

                    dtcounty = dataaccess.ExecuteSP("Sp_County", ht_County);

                    System.Data.DataTable temptable = dtcounty.Clone();
                    int startindex = currentpageindex * pagesize;
                    int endindex = currentpageindex * pagesize + pagesize;
                    if (endindex > dtcounty.Rows.Count)
                    {
                        endindex = dtcounty.Rows.Count;
                    }
                    for (int i = startindex; i < endindex; i++)
                    {
                        DataRow row = temptable.NewRow();
                        GetrowTableOfCounty_Client(ref row, dtcounty.Rows[i]);
                        temptable.Rows.Add(row);
                    }

                    if (temptable.Rows.Count > 0)
                    {
                        grd_County.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            grd_County.Rows.Add();
                            grd_County.Rows[i].Cells[0].Value = i + 1;
                            grd_County.Rows[i].Cells[1].Value = temptable.Rows[i]["State_Name"].ToString();
                            grd_County.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                            grd_County.Rows[i].Cells[3].Value = temptable.Rows[i]["County_Type"].ToString();
                            grd_County.Rows[i].Cells[4].Value = temptable.Rows[i]["State_ID"].ToString();
                            grd_County.Rows[i].Cells[5].Value = temptable.Rows[i]["County_ID"].ToString();
                            grd_County.Rows[i].Cells[6].Value = "View";
                            grd_County.Rows[i].Cells[7].Value = "Delete";

                            grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }

                    }
                    else
                    {
                        grd_County.Visible = true;
                        grd_County.DataSource = null;
                        grd_County.Rows.Clear();
                        MessageBox.Show("No Records Found");
                        // BindGridCounty();
                        currentpageindex = 0;
                        btnPrevious.Enabled = false;
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnFirst.Enabled = true;
                        // this.Cursor = currentCursor;
                    }

                    lbl_Total_Orders.Text = dtcounty.Rows.Count.ToString();
                    lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
                    First_Page();
                }

            }
            First_Page();
        }

        private void BindFilterdata()
        {
            if (ddl_SearchbyState.Text != "Select" || ddl_SearchbyState.Text != "" && ddl_searchCounty.Text != "Select" || ddl_searchCounty.Text != "")
            {
                DataView dtsearch = new DataView(dtselect);

                dtsearch.RowFilter = "State like '%" + ddl_SearchbyState.Text.ToString().ToString() + "%' and County like '%" + ddl_searchCounty.Text.ToString().ToString() + "%' ";


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
                    GetrowTable_Client(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_County.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_County.Rows.Add();
                        grd_County.Rows[i].Cells[0].Value = i + 1;
                        grd_County.Rows[i].Cells[1].Value = temptable.Rows[i]["State_Name"].ToString();
                        grd_County.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                        grd_County.Rows[i].Cells[3].Value = temptable.Rows[i]["County_Type"].ToString();
                        grd_County.Rows[i].Cells[4].Value = temptable.Rows[i]["State_ID"].ToString();
                        grd_County.Rows[i].Cells[5].Value = temptable.Rows[i]["County_ID"].ToString();
                        grd_County.Rows[i].Cells[6].Value = "View";
                        grd_County.Rows[i].Cells[7].Value = "Delete";

                        grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                else
                {
                    grd_County.Visible = true;
                    grd_County.DataSource = null;

                }

                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);

            }
        }

        private void GetNewRow_State(ref DataRow newrow, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                newrow[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;


            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;

            this.Cursor = currentCursor;
        }

        private void Filter_State_Data()
        {
            System.Data.DataTable tempTable = dtsort.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtsort.Rows.Count)
            {
                endindex = dtsort.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow newrow = tempTable.NewRow();
                GetNewRow_State(ref newrow, dtsort.Rows[i]);
                tempTable.Rows.Add(newrow);
            }

            if (tempTable.Rows.Count > 0)
            {
                grd_County.Rows.Clear();
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    grd_County.Rows.Add();
                    grd_County.Rows[i].Cells[0].Value = i + 1;
                    grd_County.Rows[i].Cells[1].Value = tempTable.Rows[i]["State_Name"].ToString();
                    grd_County.Rows[i].Cells[2].Value = tempTable.Rows[i]["County"].ToString();
                    grd_County.Rows[i].Cells[3].Value = tempTable.Rows[i]["County_Type"].ToString();
                    grd_County.Rows[i].Cells[4].Value = tempTable.Rows[i]["State_ID"].ToString();
                    grd_County.Rows[i].Cells[5].Value = tempTable.Rows[i]["County_ID"].ToString();
                    grd_County.Rows[i].Cells[6].Value = "View";
                    grd_County.Rows[i].Cells[7].Value = "Delete";

                    grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }

            else
            {
                grd_County.Rows.Clear();
                grd_County.Visible = true;
                grd_County.DataSource = null;
                string title = "Empty!";
                MessageBox.Show("No Records Found", title);
            }
            lbl_Total_Orders.Text = dtsort.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize);
        }

        private void GetNewRow_County(ref DataRow newrow, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                newrow[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void Filter_County_Data()
        {
            System.Data.DataTable tempTable = dtcounty.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dt.Rows.Count)
            {
                endindex = dt.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow newrow = tempTable.NewRow();
                GetNewRow_County(ref newrow, dt.Rows[i]);
                tempTable.Rows.Add(newrow);
            }

            if (tempTable.Rows.Count > 0)
            {
                grd_County.Rows.Clear();
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    grd_County.Rows.Add();
                    grd_County.Rows[i].Cells[0].Value = i + 1;
                    grd_County.Rows[i].Cells[1].Value = tempTable.Rows[i]["State_Name"].ToString();
                    grd_County.Rows[i].Cells[2].Value = tempTable.Rows[i]["County"].ToString();
                    grd_County.Rows[i].Cells[3].Value = tempTable.Rows[i]["County_Type"].ToString();
                    grd_County.Rows[i].Cells[4].Value = tempTable.Rows[i]["State_ID"].ToString();
                    grd_County.Rows[i].Cells[5].Value = tempTable.Rows[i]["County_ID"].ToString();
                    grd_County.Rows[i].Cells[6].Value = "View";
                    grd_County.Rows[i].Cells[7].Value = "Delete";

                    grd_County.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_County.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            else
            {
                grd_County.Rows.Clear();
                grd_County.Visible = true;
                grd_County.DataSource = null;
                string title = "Empty!";
                MessageBox.Show("No Records Found", title);
            }
            lbl_Total_Orders.Text = dtcounty.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex++;
            //if (ddl_SearchbyState.Text != "Select" && ddl_searchCounty.Text != "Select")
            if (ddl_SearchbyState.SelectedIndex > 0)
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                Filter_State_Data();

            }
            else if (ddl_searchCounty.SelectedIndex > 0)
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                Filter_County_Data();
                //BindFilterdata();
            }

            else
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                BindGridCounty();
            }




            //   BindFilterdata();

            BindGridCounty();

            //btnFirst.Enabled = true;
            //btnPrevious.Enabled = true;

            this.Cursor = currentCursor;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            //if (ddl_SearchbyState.Text != "Select" && ddl_searchCounty.Text != "Select")
            //{
            if (ddl_SearchbyState.SelectedIndex > 0)
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize) - 1;
                // BindFilterdata();
                Filter_State_Data();
            }
            else if (ddl_searchCounty.SelectedIndex > 0)
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1;
                Filter_County_Data();
            }
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
                BindGridCounty();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            BindGridCounty();
            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
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
            if (ddl_SearchbyState.SelectedIndex > 0)
            {
                Filter_State_Data();
            }
            else if (ddl_searchCounty.SelectedIndex > 0)
            {
                Filter_County_Data();
            }
            else
            {
                BindGridCounty();
            }
            //if (ddl_SearchbyState.Text != "Select" && ddl_searchCounty.Text != "Select")
            //{

            //    BindFilterdata();

            //}
            //else
            //{
            //    BindGridCounty();
            //}
            BindGridCounty();
            this.Cursor = currentCursor;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;

            if (ddl_SearchbyState.SelectedIndex > 0)
            {
                Filter_State_Data();
            }
            else if (ddl_searchCounty.SelectedIndex > 0)
            {
                Filter_County_Data();
            }
            else
            {
                BindGridCounty();
            }
            BindGridCounty();
            this.Cursor = currentCursor;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            Export_CountyData();
        }
        private void Export_CountyData()
        {
            //Exporting to Excel
            dtselect.Columns.Remove("State_Name");
            dtselect.Columns.Remove("State_ID");
            dtselect.Columns.Remove("County_ID");
            string Export_Title_Name = "County_Details";
            string folderPath = "C:\\Temp\\";
            path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtselect, Export_Title_Name.ToString());

                try
                {

                    wb.SaveAs(path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(path1);
        }

        private void txt_County_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                string title = "Validation!";
                txt_County.Text = "";
                MessageBox.Show("Numbers Not Allowed", title);
            }

            if (txt_County.Text.Length == 0)
            {
                if (e.Handled = (e.KeyChar == (char)Keys.Space))
                {
                    MessageBox.Show("Space not allowed!");
                }
            }

            //if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            //{

            //    e.Handled = true;

            //    MessageBox.Show("Numbers not allowed");
            //}

        }

        private void lbl_ErrorInfo_Click(object sender, EventArgs e)
        {

        }


    }
}
