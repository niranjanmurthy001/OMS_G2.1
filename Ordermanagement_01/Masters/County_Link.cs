using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Data.OleDb;

using DocumentFormat.OpenXml.Spreadsheet;

namespace Ordermanagement_01
{
    public partial class County_Link : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable tmptable = new System.Data.DataTable();
        System.Data.DataTable dtselect = new System.Data.DataTable();
        System.Data.DataTable dtsort = new System.Data.DataTable();
        System.Data.DataTable dtcounty = new System.Data.DataTable();
        InfiniteProgressBar.clsProgress probar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int User_Id, CountyLinkId, count, count_non, inset;
      //  static int currentPageIndex = 0;
        private int currentPageIndex = 1;
         string state;
         string county;
        private int pageSize = 50;
        string username, StateAbr;
        string duplicate;
        string statename, countyname;
      //  int state, county;
        int userid = 0, state_id, stateid, countyid, insert = 0, state_insert, county_Id, State_id;
        int state_ID, county_ID;
        public County_Link(int userid, string Username)
        {
            InitializeComponent();
            User_Id = userid;
            username = Username;
        }
        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentPageIndex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            this.Cursor = currentCursor;
        }

        private void County_Link_Load(object sender, EventArgs e)
        {
           
            dbc.BindState(ddl_State_Wise);
            dbc.BindState(ddl_State);
          

            grp_CountyReg.Visible = true;
            btn_Import.Visible = true;
           // grp_CountyReg.Visible = false;
            grp_CountyInfo.Visible = true;
            First_Page();
            dbc.BindCounty(cbo_County, int.Parse(ddl_State_Wise.SelectedValue.ToString()));
            //dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
           
           //lbl_Record_Addedby.Text = username;
           //lbl_Record_AddedDate.Text = DateTime.Now.ToString();
            
           // btn_Import.

            label4.Visible = true;
            label25.Visible = false;
            label26.Visible = false;

            Grdiview_Bind_Tax_County_Link();
        }

        private void GetNewRow(ref DataRow newRow, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                newRow[col.ColumnName] = source[col.ColumnName];
            }
        }

        protected void Grdiview_Bind_Tax_County_Link()
        {
            form_loader.Start_progres();
            //probar.startProgress();

            County_View.Visible = true;
            County_Delete.Visible = true;

            grd_CountyImport.Rows.Clear();
            Hashtable htselect = new Hashtable();
            
            //System.Data.DataTable dtselect=
            if (ddl_State_Wise.SelectedIndex > 0)
            {
                htselect.Add("@Trans", "SELECT_BY_STATE_WISE");
                htselect.Add("@State", ddl_State_Wise.SelectedValue.ToString());
                if (cbo_County.SelectedIndex > 0)
                {
                    htselect.Clear();
                    htselect.Add("@Trans", "SELECT_BY_STATE_COUNTY");
                    htselect.Add("@State", int.Parse(ddl_State_Wise.SelectedValue.ToString()));
                    htselect.Add("@County", int.Parse(cbo_County.SelectedValue.ToString()));
                }

            }
            else
            {

                htselect.Add("@Trans", "SELECT_ALL");
            }

            dtselect = dataaccess.ExecuteSP("Sp_County_Link", htselect);

            dtcounty=dtselect;
            if (dtselect.Rows.Count > 0)
            {
                System.Data.DataTable tmptable = dtselect.Clone();
                int startIndex = currentPageIndex * pageSize;
                int endIndex = currentPageIndex * pageSize + pageSize;
                if (endIndex > dtselect.Rows.Count)
                {
                    endIndex = dtselect.Rows.Count;
                }
                for (int i = startIndex; i < endIndex; i++)
                {
                    DataRow newrow = tmptable.NewRow();
                    GetNewRow(ref newrow, dtselect.Rows[i]);
                    tmptable.Rows.Add(newrow);
                }

                //Page index format

                if (tmptable.Rows.Count > 0)
                {
                    grd_CountyImport.Rows.Clear();
                    for (int i = 0; i < tmptable.Rows.Count; i++)
                    {
                        // Grd_County_Link.DataSource = dtselect;
                        grd_CountyImport.Rows.Add();
                        grd_CountyImport.Rows[i].Cells[0].Value = i + 1;
                        grd_CountyImport.Rows[i].Cells[1].Value = tmptable.Rows[i]["State"].ToString();
                        grd_CountyImport.Rows[i].Cells[2].Value = tmptable.Rows[i]["County"].ToString();
                        grd_CountyImport.Rows[i].Cells[3].Value = tmptable.Rows[i]["Index_Availability"].ToString();
                        grd_CountyImport.Rows[i].Cells[4].Value = tmptable.Rows[i]["Index_date_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[5].Value = tmptable.Rows[i]["Back_deed_search"].ToString();
                        grd_CountyImport.Rows[i].Cells[6].Value = tmptable.Rows[i]["Back_deed_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[7].Value = tmptable.Rows[i]["Images"].ToString();
                        grd_CountyImport.Rows[i].Cells[8].Value = tmptable.Rows[i]["Images_date_of_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[9].Value = tmptable.Rows[i]["Land_Records_Link"].ToString();
                        grd_CountyImport.Rows[i].Cells[10].Value = tmptable.Rows[i]["Subscription_Link"].ToString();
                        grd_CountyImport.Rows[i].Cells[11].Value = tmptable.Rows[i]["Plant_availability"].ToString();
                        grd_CountyImport.Rows[i].Cells[14].Value = tmptable.Rows[i]["County_Link_Id"].ToString();


                        grd_CountyImport.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_CountyImport.Rows[i].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_CountyImport.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                else
                {
                    grd_CountyImport.Rows.Clear();
                    grd_CountyImport.Visible = true;
                    grd_CountyImport.DataSource = null;
                }
                lbl_count.Text = dtselect.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize);

                // probar.stopProgress();
            }
        }

        private void Grd_Tax_County_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9 || e.ColumnIndex == 8)
            {
                string url = grd_CountyImport.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (url != "" && url != "N/A")
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
                //Update
            else if (e.ColumnIndex == 11)
            {

            }
                //Delete
            else if (e.ColumnIndex == 12)
            {
                if (grd_CountyImport.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@County_Link_Id", grd_CountyImport.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    dtdelete = dataaccess.ExecuteSP("Sp_County_Link", htdelete);
                    Grdiview_Bind_Tax_County_Link();
                }
            }
        }

        private void lbl_customer_No_TextChanged(object sender, EventArgs e)
        {

        }

        private void CountyClear()
        {
            ddl_State.SelectedIndex = 0;
        //    ddl_County.SelectedIndex = -1;
            txt_Index_Availability.Text = ""; 
            txt_Index_date_range.Text = ""; 
            txt_Back_deed_search.Text = "";
            txt_Back_deed_range.Text="";  
            txt_Images.Text="";  
            txt_Images_date_of_range.Text="";  
            txt_Land_Records_Link.Text="";  
            txt_Subscription_Link.Text="";  
            txt_Plant_availability.Text="";  
            //lbl_Record_Addedby.Text="";
            //lbl_Record_AddedDate.Text = "";

            CountyLinkId = 0;
            btn_Add_New_Tax.Text = "Add";
            //label26.Visible = false;
            //label25.Visible = false;
            //label4.Visible = true;

            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
        }

        private void btn_Add_New_Click(object sender, EventArgs e)
        {
            if (btn_Add_New.Text == "Add New")
            {
                CountyClear();
                label25.Visible = true;
                label4.Visible = false;
                label26.Visible = false;
                btn_Import.Visible = false;
                grp_CountyInfo.Visible = false;
                grp_CountyReg.Visible = true;
               
                btn_Add_New.Text = "Back";
                btn_Add_New_Tax.Text = "Add";
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
                     lbl_Record_Addedby.Text = username;
                     lbl_Record_AddedDate.Text = DateTime.Now.ToString();
                     Btn_Upload.Visible = false;
                     btn_GetImportExcel.Visible = false;
                     btn_Export_County.Visible = false;
                     ddl_State.Select();
                     btn_Refresh.Visible = false;
                     


                     pictureBox3.Visible = false;
                     pictureBox1.Visible = false;
                     pictureBox2.Visible = false;
                     label27.Visible = false;
                     label28.Visible = false;
                     label29.Visible = false;

                     btn_Remove_Duplic.Visible = false;
                     btn_removedup.Visible = false;
                     btn_Remove_Error_row.Visible = false;
            }
            else
            {
                label4.Visible = true;
                label25.Visible = false;
                label26.Visible = false;
                grp_CountyInfo.Visible = true;
                grp_CountyReg.Visible = true;
                CountyClear();
                btn_Add_New.Text = "Add New";
                btn_Export_County.Visible = true;
                btn_Import.Visible = true;
                Btn_Upload.Visible = true;
                btn_GetImportExcel.Visible = true;

                btn_Refresh_Click(sender,e);
                btn_Refresh.Visible = true;

                pictureBox3.Visible = true;
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                label27.Visible = true;
                label28.Visible = true;
                label29.Visible = true;

                btn_Remove_Duplic.Visible = true;
                btn_removedup.Visible = true;
                btn_Remove_Error_row.Visible = true;
            }
           
        }
        private bool validate()
        {
            int error = 0, dupl = 0, exist = 0, empty = 0;
            for (int i = 0; i <= grd_CountyImport.Rows.Count - 1; i++)
            {
                if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                {

                    return true;
                }
                else
                {
                    if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
                    {
                        error = error + 1;
                        //return false;
                    }

                    else if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Cyan)
                    {
                        dupl = dupl + 1;
                        // return false;
                    }
                    else if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Yellow)
                    {
                        exist = exist + 1;

                    }
                    else if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.SystemColors.Control)
                    {
                        empty = empty + 1;
                    }
                }
            }
            if (error > 0)
            {

                string title1 = "Error!";
                MessageBox.Show(" ' " + error + " ' " + " Error!, Check the Incorrect Values in Excel so Remove Errors from the Excel", title1);
                return false;

            }

            if (dupl > 0)
            {
                string title1 = "Duplicate Data!";
                MessageBox.Show(" ' " + dupl + " ' " + "Duplicate data in Excel so Remove the Duplicate Data from Excel", title1);
                return false;
            }

            if (exist > 0)
            {
                string title = "Existed!";
                MessageBox.Show(" ' " + exist + " ' " + "  No of County Link Records Already Exists so Remove the Existed Data from the Excel", title);
                return false;
            }
            if (empty > 0)
            {
                string title = "Empty!";
                MessageBox.Show("Upload a Excel file to import", title);
                return false;
            }


            return true;

        }
        private void btn_Import_Click(object sender, EventArgs e)
        {
            int error = 0, dupl = 0, exist = 0;
            form_loader.Start_progres();
            //probar.startProgress();
            count = 0; count_non = 0;
            int count_grd = grd_CountyImport.Rows.Count;
            int count_ex;
            if (validate() != false)
            {
                for (int i = 0; i < grd_CountyImport.Rows.Count - 1; i++)
                {
                    if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                    {
                        int Stateid;
                        int Countyid;
                        Hashtable htbarowerstate = new Hashtable();
                        DataTable dtbarrowerstate = new System.Data.DataTable();
                        //htbarowerstate.Add("@Trans", "GETSTATE_BY_ABR");
                        htbarowerstate.Add("@Trans", "GETSTATE_BY_STATENAME");
                        htbarowerstate.Add("@state_name", grd_CountyImport.Rows[i].Cells[1].Value.ToString());
                        dtbarrowerstate = dataaccess.ExecuteSP("Sp_Order_Get_Details", htbarowerstate);
                        if (dtbarrowerstate.Rows.Count > 0)
                        {

                            Stateid = int.Parse(dtbarrowerstate.Rows[0]["State_ID"].ToString());
                        }
                        else
                        {
                            Stateid = 0;
                        }


                        //get County
                        Hashtable htBarcounty = new Hashtable();
                        DataTable dtbarcounty = new System.Data.DataTable();
                        htBarcounty.Add("@Trans", "GETCOUNTY");
                        htBarcounty.Add("@State", Stateid);
                        htBarcounty.Add("@County_Name", grd_CountyImport.Rows[i].Cells[2].Value.ToString());
                        dtbarcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty);
                        if (dtbarcounty.Rows.Count > 0)
                        {
                            Countyid = int.Parse(dtbarcounty.Rows[0]["County_ID"].ToString());
                        }
                        else
                        {
                            Countyid = 0;
                        }

                        if (Stateid != 0 && Countyid != 0)
                        {
                            //Record already exists
                            Hashtable ht = new Hashtable();
                            DataTable dt = new System.Data.DataTable();
                            ht.Add("@Trans", "SELECT_BY_STATE_COUNTY");
                            ht.Add("@State", Stateid);
                            ht.Add("@County", Countyid);
                            dt = dataaccess.ExecuteSP("Sp_County_Link", ht);
                            if (dt.Rows.Count == 0)
                            {
                                //if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                                //{
                                Hashtable ht_INSERT = new Hashtable();
                                DataTable dt_INSERT = new System.Data.DataTable();
                                ht_INSERT.Add("@Trans", "INSERT");
                                ht_INSERT.Add("@State", Stateid);
                                ht_INSERT.Add("@County", Countyid);
                                ht_INSERT.Add("@Index_Availability", grd_CountyImport.Rows[i].Cells[3].Value.ToString());
                                ht_INSERT.Add("@Index_date_range", grd_CountyImport.Rows[i].Cells[4].Value.ToString());
                                ht_INSERT.Add("@Back_deed_search", grd_CountyImport.Rows[i].Cells[5].Value.ToString());
                                ht_INSERT.Add("@Back_deed_range", grd_CountyImport.Rows[i].Cells[6].Value.ToString());
                                ht_INSERT.Add("@Images", grd_CountyImport.Rows[i].Cells[7].Value.ToString());
                                ht_INSERT.Add("@Images_date_of_range", grd_CountyImport.Rows[i].Cells[8].Value.ToString());
                                ht_INSERT.Add("@Land_Records_Link", grd_CountyImport.Rows[i].Cells[9].Value.ToString());
                                ht_INSERT.Add("@Subscription_Link", grd_CountyImport.Rows[i].Cells[10].Value.ToString());
                                ht_INSERT.Add("@Plant_availability", grd_CountyImport.Rows[i].Cells[11].Value.ToString());
                                ht_INSERT.Add("@Inserted_By", User_Id);
                                ht_INSERT.Add("@Instered_Date", DateTime.Now);
                                ht_INSERT.Add("@Status", "True");
                                dt_INSERT = dataaccess.ExecuteSP("Sp_County_Link", ht_INSERT);
                                count = count + 1;
                                //}
                                //else
                                //{
                                //    string title1 = "Check!";
                                //    MessageBox.Show("Invalid!, Check the Incorrect Values in Excel", title1);
                                //    count = 0;
                                //    break;
                                //}
                            }

                        }
                        else
                        {
                            count_non = count_non + 1;

                        }
                    }

                    //else
                    //{
                    //    if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
                    //    {
                    //        error = error + 1;
                    //    }

                    //    else if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Cyan)
                    //    {
                    //        dupl = dupl + 1;
                    //    }
                    //    else if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Yellow)
                    //    {
                    //        exist = exist + 1;
                    //    }
                    //}

                }

                // probar.stopProgress();
                if (count > 0)
                {
                    string title = "Successfull";
                    MessageBox.Show(" ' " + count + " ' " + " No of County Link Records Imported successfully", title);
                    Grdiview_Bind_Tax_County_Link();
                    btn_Import.Visible = true;
                    County_View.Visible = true;
                    County_Delete.Visible = true;
                }
               
                //if (error > 0)
                //{

                //    string title1 = "Check!";
                //    MessageBox.Show(" ' " + error + " ' " + " Invalid!, Check the Incorrect Values in Excel", title1);
                //    // error = 0;
                //    //break;
                //}

                //if (dupl > 0)
                //{
                //    string title1 = "Check!";
                //    MessageBox.Show(" ' " + dupl + " ' " + "Duplicate data in Excel", title1);
                //}

                //if (exist > 0)
                //{
                //    string title = "Existed!";
                //    MessageBox.Show(" ' " + exist + " ' " + "  No of County Link Records Already Exists", title);
                //}

                //Grdiview_Bind_Tax_County_Link();
                //btn_Import.Visible = true;
                //County_View.Visible = true;
                //County_Delete.Visible = true;

            }
        }

        private void GetDataRow_State(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtsort.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void Filter_State_Data()
        {
            System.Data.DataTable temptable = dtsort.Clone();
            int startindex = currentPageIndex * pageSize;
            int endindex = currentPageIndex * pageSize + pageSize;
            if (endindex > dtsort.Rows.Count)
            {
                endindex = dtsort.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow newrow = temptable.NewRow();
                GetDataRow_State(ref newrow, dtsort.Rows[i]);
                temptable.Rows.Add(newrow);
            }

            if (temptable.Rows.Count > 0)
            {
                grd_CountyImport.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_CountyImport.Rows.Add();
                    grd_CountyImport.Rows[i].Cells[0].Value = i + 1;
                    grd_CountyImport.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                    grd_CountyImport.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                    grd_CountyImport.Rows[i].Cells[3].Value = temptable.Rows[i]["Index_Availability"].ToString();
                    grd_CountyImport.Rows[i].Cells[4].Value = temptable.Rows[i]["Index_date_range"].ToString();
                    grd_CountyImport.Rows[i].Cells[5].Value = temptable.Rows[i]["Back_deed_search"].ToString();
                    grd_CountyImport.Rows[i].Cells[6].Value = temptable.Rows[i]["Back_deed_range"].ToString();
                    grd_CountyImport.Rows[i].Cells[7].Value = temptable.Rows[i]["Images"].ToString();
                    grd_CountyImport.Rows[i].Cells[8].Value = temptable.Rows[i]["Images_date_of_range"].ToString();
                    grd_CountyImport.Rows[i].Cells[9].Value = temptable.Rows[i]["Land_Records_Link"].ToString();
                    grd_CountyImport.Rows[i].Cells[10].Value = temptable.Rows[i]["Subscription_Link"].ToString();
                    grd_CountyImport.Rows[i].Cells[11].Value = temptable.Rows[i]["Plant_availability"].ToString();
                    grd_CountyImport.Rows[i].Cells[14].Value = temptable.Rows[i]["County_Link_Id"].ToString();

                    grd_CountyImport.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_CountyImport.Rows[i].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_CountyImport.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_CountyImport.Rows.Clear();
                grd_CountyImport.Visible = true;
                grd_CountyImport.DataSource = null;
            }

            lbl_count.Text = dtsort.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pageSize);
        }

        private void ddl_State_Wise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State_Wise.SelectedIndex > 0)
            {
                dbc.BindCounty(cbo_County, int.Parse(ddl_State_Wise.SelectedValue.ToString()));
                grd_CountyImport.Rows.Clear();

                form_loader.Start_progres();
                //probar.startProgress();
                Hashtable htsort = new Hashtable();
                
                htsort.Add("@Trans", "SELECT_BY_STATE_WISE");
                htsort.Add("@State", int.Parse(ddl_State_Wise.SelectedValue.ToString()));
                dtsort = dataaccess.ExecuteSP("Sp_County_Link", htsort);


                System.Data.DataTable temptable = dtsort.Clone();
                int startindex = currentPageIndex * pageSize;
                int endindex = currentPageIndex * pageSize + pageSize;
                if (endindex > dtsort.Rows.Count)
                {
                    endindex = dtsort.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow newrow = temptable.NewRow();
                    GetDataRow_State(ref newrow,dtsort.Rows[i]);
                    temptable.Rows.Add(newrow);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_CountyImport.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_CountyImport.Rows.Add();
                        grd_CountyImport.Rows[i].Cells[0].Value = i + 1;
                        grd_CountyImport.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                        grd_CountyImport.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                        grd_CountyImport.Rows[i].Cells[3].Value = temptable.Rows[i]["Index_Availability"].ToString();
                        grd_CountyImport.Rows[i].Cells[4].Value = temptable.Rows[i]["Index_date_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[5].Value = temptable.Rows[i]["Back_deed_search"].ToString();
                        grd_CountyImport.Rows[i].Cells[6].Value = temptable.Rows[i]["Back_deed_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[7].Value = temptable.Rows[i]["Images"].ToString();
                        grd_CountyImport.Rows[i].Cells[8].Value = temptable.Rows[i]["Images_date_of_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[9].Value = temptable.Rows[i]["Land_Records_Link"].ToString();
                        grd_CountyImport.Rows[i].Cells[10].Value = temptable.Rows[i]["Subscription_Link"].ToString();
                        grd_CountyImport.Rows[i].Cells[11].Value = temptable.Rows[i]["Plant_availability"].ToString();
                        grd_CountyImport.Rows[i].Cells[14].Value = temptable.Rows[i]["County_Link_Id"].ToString();

                        grd_CountyImport.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_CountyImport.Rows[i].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_CountyImport.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    lbl_count.Text = dtsort.Rows.Count.ToString();
                    lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pageSize);
                    if (lblRecordsStatus.Text == "1 / 1")
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnFirst.Enabled = true;
                    }
                    else
                    {
                        First_Page();
                    }  
              
                }
                else
                {
                    grd_CountyImport.Rows.Clear();
                    grd_CountyImport.Visible = true;
                    grd_CountyImport.DataSource = null;

                   
                }

                //lbl_count.Text = dtsort.Rows.Count.ToString();
                //lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pageSize);
                
              
             //   First_Page();
              
            }
            
        }

        private void GetDataRow_County(ref DataRow dest, DataRow source)
        {
            foreach(DataColumn col in dtcounty.Columns)
            {
                dest[col.ColumnName]=source[col.ColumnName];
            }
        }

        private void Filter_County_Data()
        {
            System.Data.DataTable temptable = dtsort.Clone();
            int startindex = currentPageIndex * pageSize;
            int endindex = currentPageIndex * pageSize + pageSize;
            if (endindex > dtcounty.Rows.Count)
            {
                endindex = dtcounty.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow newrow = temptable.NewRow();
                GetDataRow_County(ref newrow, dtcounty.Rows[i]);
                temptable.Rows.Add(newrow);
            }

            if (temptable.Rows.Count > 0)
            {
                grd_CountyImport.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_CountyImport.Rows.Add();
                    grd_CountyImport.Rows[i].Cells[0].Value = i + 1;
                    grd_CountyImport.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                    grd_CountyImport.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                    grd_CountyImport.Rows[i].Cells[3].Value = temptable.Rows[i]["Index_Availability"].ToString();
                    grd_CountyImport.Rows[i].Cells[4].Value = temptable.Rows[i]["Index_date_range"].ToString();
                    grd_CountyImport.Rows[i].Cells[5].Value = temptable.Rows[i]["Back_deed_search"].ToString();
                    grd_CountyImport.Rows[i].Cells[6].Value = temptable.Rows[i]["Back_deed_range"].ToString();
                    grd_CountyImport.Rows[i].Cells[7].Value = temptable.Rows[i]["Images"].ToString();
                    grd_CountyImport.Rows[i].Cells[8].Value = temptable.Rows[i]["Images_date_of_range"].ToString();
                    grd_CountyImport.Rows[i].Cells[9].Value = temptable.Rows[i]["Land_Records_Link"].ToString();
                    grd_CountyImport.Rows[i].Cells[10].Value = temptable.Rows[i]["Subscription_Link"].ToString();
                    grd_CountyImport.Rows[i].Cells[11].Value = temptable.Rows[i]["Plant_availability"].ToString();
                    grd_CountyImport.Rows[i].Cells[14].Value = temptable.Rows[i]["County_Link_Id"].ToString();

                    grd_CountyImport.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_CountyImport.Rows[i].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_CountyImport.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_CountyImport.Rows.Clear();
                grd_CountyImport.Visible = true;
                grd_CountyImport.DataSource = null;
            }

            lbl_count.Text =  dtcounty.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize);
        }

        private void cbo_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State_Wise.SelectedIndex > 0)
            {
                if (cbo_County.SelectedIndex > 0)
                {
                    grd_CountyImport.Rows.Clear();

                    form_loader.Start_progres();
                    //probar.startProgress();
                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    ht.Add("@Trans", "SELECT_BY_STATE_COUNTY");
                    ht.Add("@State", int.Parse(ddl_State_Wise.SelectedValue.ToString()));
                    ht.Add("@County", int.Parse(cbo_County.SelectedValue.ToString()));
                    dtcounty = dataaccess.ExecuteSP("Sp_County_Link", ht);

                    System.Data.DataTable temptable = dtsort.Clone();
                    int startindex = currentPageIndex * pageSize;
                    int endindex = currentPageIndex * pageSize + pageSize;
                    if (endindex > dtcounty.Rows.Count)
                    {
                        endindex = dtcounty.Rows.Count;
                    }
                    for (int i = startindex; i < endindex; i++)
                    {
                        DataRow newrow = temptable.NewRow();
                        GetDataRow_County(ref newrow, dtcounty.Rows[i]);
                        temptable.Rows.Add(newrow);
                    }

                    if (temptable.Rows.Count > 0)
                    {
                        grd_CountyImport.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            grd_CountyImport.Rows.Add();
                            grd_CountyImport.Rows[i].Cells[0].Value = i + 1;
                            grd_CountyImport.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                            grd_CountyImport.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                            grd_CountyImport.Rows[i].Cells[3].Value = temptable.Rows[i]["Index_Availability"].ToString();
                            grd_CountyImport.Rows[i].Cells[4].Value = temptable.Rows[i]["Index_date_range"].ToString();
                            grd_CountyImport.Rows[i].Cells[5].Value = temptable.Rows[i]["Back_deed_search"].ToString();
                            grd_CountyImport.Rows[i].Cells[6].Value = temptable.Rows[i]["Back_deed_range"].ToString();
                            grd_CountyImport.Rows[i].Cells[7].Value = temptable.Rows[i]["Images"].ToString();
                            grd_CountyImport.Rows[i].Cells[8].Value = temptable.Rows[i]["Images_date_of_range"].ToString();
                            grd_CountyImport.Rows[i].Cells[9].Value = temptable.Rows[i]["Land_Records_Link"].ToString();
                            grd_CountyImport.Rows[i].Cells[10].Value = temptable.Rows[i]["Subscription_Link"].ToString();
                            grd_CountyImport.Rows[i].Cells[11].Value = temptable.Rows[i]["Plant_availability"].ToString();
                            grd_CountyImport.Rows[i].Cells[14].Value = temptable.Rows[i]["County_Link_Id"].ToString();

                            grd_CountyImport.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_CountyImport.Rows[i].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_CountyImport.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        }
                        lbl_count.Text = dtcounty.Rows.Count.ToString();
                        lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize);
                        if (lblRecordsStatus.Text == "1 / 1")
                        {
                            btnNext.Enabled = false;
                            btnLast.Enabled = false;
                            btnFirst.Enabled = true;
                        }
                        else
                        {
                            First_Page();
                        }  
                    }
                      
                    else
                    {
                        grd_CountyImport.Rows.Clear();
                        grd_CountyImport.Visible = true;
                       
                   
                        grd_CountyImport.DataSource = null;
                        MessageBox.Show("Records not Found");
                        //Grdiview_Bind_Tax_County_Link();

                        //btn_Refresh_Click(sender, e);
                        //lbl_count.Text = dtselect.Rows.Count.ToString();
                        //lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize);


                        lbl_count.Text = dtcounty.Rows.Count.ToString();
                        lblRecordsStatus.Text = (currentPageIndex + 0) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize);

                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnFirst.Enabled = true;
                       btn_Refresh_Click(sender, e);
                    }

                    //lbl_count.Text = dtcounty.Rows.Count.ToString();
                    //lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize);

                    //if (lblRecordsStatus.Text == "1 / 1")
                    //{
                    //    btnNext.Enabled = false;
                    //    btnLast.Enabled = false;
                    //    btnFirst.Enabled = true;
                    //}
                    //else
                    //{
                    //    First_Page();
                    //}  
                
                }
            }
           
        }

        private void Btn_Upload_Click(object sender, EventArgs e)
        {
            grp_CountyInfo.Visible = true;
            grp_CountyReg.Visible = true;

            County_View.Visible = false;
            County_Delete.Visible = false;

            label4.Visible = true;
            label25.Visible = false;
            label26.Visible = false;

            grd_CountyImport.Rows.Clear();

            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"c:\";
            var txtFileName = fdlg.FileName;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName = fdlg.FileName;
                Import(txtFileName);
                System.Windows.Forms.Application.DoEvents();
            }
            else
            {
                Grdiview_Bind_Tax_County_Link();
            }
            
        }


        private void Import(string txtFileName)
        {
            form_loader.Start_progres();
            //probar.startProgress();
            if (txtFileName != string.Empty)
            {

                String name = "Sheet1";    // default Sheet1 
                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           txtFileName +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                System.Data.DataTable data = new System.Data.DataTable();

                sda.Fill(data);
              
                grd_CountyImport.Rows.Clear();
                if (data.Rows.Count > 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        string State_name = data.Rows[i]["State"].ToString();
                        string County_name = data.Rows[i]["County"].ToString();

                        grd_CountyImport.Rows.Add();
                        grd_CountyImport.Rows[i].Cells[0].Value = i + 1;
                        grd_CountyImport.Rows[i].Cells[1].Value = data.Rows[i]["State"].ToString();
                        grd_CountyImport.Rows[i].Cells[2].Value = data.Rows[i]["County"].ToString();

                        grd_CountyImport.Rows[i].Cells[3].Value = data.Rows[i]["Index_Availability"].ToString();
                        grd_CountyImport.Rows[i].Cells[4].Value = data.Rows[i]["Index_date_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[5].Value = data.Rows[i]["Back_deed_search"].ToString();
                        grd_CountyImport.Rows[i].Cells[6].Value = data.Rows[i]["Back_deed_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[7].Value = data.Rows[i]["Images"].ToString();
                        grd_CountyImport.Rows[i].Cells[8].Value = data.Rows[i]["Images_date_of_range"].ToString();
                        grd_CountyImport.Rows[i].Cells[9].Value = data.Rows[i]["Land_Records_Link"].ToString();
                        grd_CountyImport.Rows[i].Cells[10].Value = data.Rows[i]["Subscription_Link"].ToString();
                        grd_CountyImport.Rows[i].Cells[11].Value = data.Rows[i]["Plant_availability"].ToString();

                        grd_CountyImport.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;

                        grd_CountyImport.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_CountyImport.Rows[i].Cells[12].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_CountyImport.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



                        ////Check County Exist  -- County is unique
                        Hashtable ht_order = new Hashtable();
                        DataTable dt_order = new DataTable();
                        ht_order.Add("@Trans", "SEARCH_COUNTY");
                        ht_order.Add("@StateName", State_name);
                        ht_order.Add("@county_Name", County_name);

                        dt_order = dataaccess.ExecuteSP("Sp_County_Link", ht_order);
                        if (dt_order.Rows.Count > 0)
                        {
                            State_id = int.Parse(dt_order.Rows[0]["State_ID"].ToString());
                            county_Id = int.Parse(dt_order.Rows[0]["County_ID"].ToString());
                            StateAbr = dt_order.Rows[0]["Abbreviation"].ToString();

                            grd_CountyImport.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                        }

                        //error in County or state

                        Hashtable htord = new Hashtable();
                        DataTable dtord = new DataTable();
                        htord.Add("@Trans", "COUNTY_SEARCH");
                        htord.Add("@StateName", grd_CountyImport.Rows[i].Cells[1].Value);
                        htord.Add("@county_Name", grd_CountyImport.Rows[i].Cells[2].Value);

                        dtord = dataaccess.ExecuteSP("Sp_County_Link", htord);
                        if (dtord.Rows.Count > 0)
                        {

                            county_Id = int.Parse(dtord.Rows[0]["County_ID"].ToString());
                        }
                        else
                        {
                            //abbrid = 0;
                            grd_CountyImport.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;

                        }


                        //Duplicate of records
                        for (int j = 0; j < i; j++)
                        {

                            string state = data.Rows[j]["State"].ToString();
                            string county = data.Rows[j]["County"].ToString();

                            if (state == State_name && county == County_name)
                            {

                                grd_CountyImport.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Cyan;

                            }
                            else
                            {
                             //   value = 0;
                            }

                        }

                        if (State_name == "" || County_name == "")
                        {
                            grd_CountyImport.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                        }

                    }

                    btn_Import.Visible = true;
                    County_View.Visible = false;
                    County_Delete.Visible = false;

                    lbl_count.Text = data.Rows.Count.ToString();
                    lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(data.Rows.Count) / pageSize);

                }
                else
                {
                    string title = "Empty!";

                    MessageBox.Show("Check the Excel is empty", title);
                }

            }
            //probar.stopProgress();
        }


        private bool Duplicate_Record()
        {
            Hashtable ht_checkDuplicate = new Hashtable();
            DataTable dt_checkDuplicate = new DataTable();

            ht_checkDuplicate.Add("@Trans", "CHECK_DUPLICATE");
            ht_checkDuplicate.Add("@State", ddl_State.SelectedValue.ToString());
            ht_checkDuplicate.Add("@County", ddl_County.SelectedValue.ToString());
            dt_checkDuplicate = dataaccess.ExecuteSP("Sp_County_Link", ht_checkDuplicate);

            for (int i = 0; i <= dt_checkDuplicate.Rows.Count - 1; i++)
            {
                state = dt_checkDuplicate.Rows[0]["State_ID"].ToString();
                county = dt_checkDuplicate.Rows[0]["County_ID"].ToString();

                string selected_state = ddl_State.SelectedValue.ToString();
                string selected_County = ddl_County.SelectedValue.ToString();

                if (state == selected_state && county == selected_County  && btn_Add_New_Tax.Text != "Edit")
                {
                //if (state == selected_state && county == selected_County )
                //{
                    duplicate = "Duplicate Data";
                    string title = "Duplicate Record!";
                    MessageBox.Show("Record Already Existed", title);
                    CountyClear();
                    return false;
                }
            //    else if (state == selected_state && county == selected_County && btn_Add_New_Tax.Text == "Edit")
            //    {
            //        //if (state == selected_state && county == selected_County )
            //        //{
            //        duplicate = "Duplicate Data";
            //        string title = "Duplicate Record!";
            //        MessageBox.Show("Record Already Existed", title);
            //        CountyClear();
            //        return false;
            //    }
            }

            return true;
        }

        //private bool Edit_Duplicate_Record()
        //{
        //    int countylinkid;
        //    Hashtable ht_checkDuplicate = new Hashtable();
        //    DataTable dt_checkDuplicate = new DataTable();

        //    ht_checkDuplicate.Add("@Trans", "CHECK_EDIT_DUPLICATE");
        //    ht_checkDuplicate.Add("@Statename", statename);
        //    ht_checkDuplicate.Add("@county_Name", countyname);
          
        //    dt_checkDuplicate = dataaccess.ExecuteSP("Sp_County_Link", ht_checkDuplicate);

         
        //  if (dt_checkDuplicate.Rows.Count>0)
        //  {
        //        state = dt_checkDuplicate.Rows[0]["State_ID"].ToString();
        //        county = dt_checkDuplicate.Rows[0]["County_ID"].ToString();
        //       // countylinkid =int.Parse(dt_checkDuplicate.Rows[0]["CountyLinkId"].ToString());

        //        string selected_state = ddl_State.SelectedValue.ToString();
        //        string selected_County = ddl_County.SelectedValue.ToString();

        //        if (state == selected_state && county == selected_County && btn_Add_New_Tax.Text == "Edit")
        //        {
        //            return true;
                   
        //        }
        //        else
        //        {
        //            if (state != selected_state  && btn_Add_New_Tax.Text == "Edit")
        //            {
                        
        //                duplicate = "Duplicate Data";
        //                string title = "Duplicate Record!";
        //                MessageBox.Show("Record Already Existed", title);
        //                CountyClear();
        //                return false;
        //            }
        //            else if (county != selected_County && btn_Add_New_Tax.Text == "Edit")
        //            {
        //                duplicate = "Duplicate Data";
        //                string title = "Duplicate Record!";
        //                MessageBox.Show("Record Already Existed", title);
        //                CountyClear();
        //                return false;

        //            }
        //        }
           
        //  }
        //    return true;
        //}


        private bool Edit_Duplicate_Record()
        {
            //int stateid, countyid;
            Hashtable ht_checkDuplicate = new Hashtable();
            DataTable dt_checkDuplicate = new DataTable();

            ht_checkDuplicate.Add("@Trans", "CHECK_EDIT_DUPLICATE");
            ht_checkDuplicate.Add("@StateName", statename);
            ht_checkDuplicate.Add("@county_Name", countyname);
            dt_checkDuplicate = dataaccess.ExecuteSP("Sp_County_Link", ht_checkDuplicate);
            if (dt_checkDuplicate.Rows.Count > 0)
            {
                stateid = int.Parse(dt_checkDuplicate.Rows[0]["State_ID"].ToString());
                countyid = int.Parse(dt_checkDuplicate.Rows[0]["County_ID"].ToString());
            }

            Hashtable ht_check = new Hashtable();
            DataTable dt_check = new DataTable();
            ht_check.Add("@Trans", "SELECT_BY_STATE_COUNTY");
            ht_check.Add("@State", ddl_State.SelectedValue.ToString());
            ht_check.Add("@County", ddl_County.SelectedValue.ToString());
            dt_check = dataaccess.ExecuteSP("Sp_County_Link", ht_check);
            if (dt_check.Rows.Count > 0)
            {
                state_ID = int.Parse(dt_check.Rows[0]["State_ID"].ToString());
                county_ID = int.Parse(dt_check.Rows[0]["County_ID"].ToString());
            }
            if (state_ID == stateid && county_ID == countyid && btn_Add_New_Tax.Text == "Edit")
            {
                return true;
            }
            else
            {
                if (state_ID != stateid && btn_Add_New_Tax.Text == "Edit")
                {
                    duplicate = "Duplicate Data";
                    string title = "Duplicate Record!";
                    MessageBox.Show("Record Already Existed", title);
                    CountyClear();
                    return false;
                }
                else if (county_ID != countyid && btn_Add_New_Tax.Text == "Edit")
                {
                    duplicate = "Duplicate Data";
                    string title = "Duplicate Record!";
                    MessageBox.Show("Record Already Existed", title);
                    CountyClear();
                    return false;
                }
            }
            return true;
        }

        private void btn_Add_New_Tax_Click(object sender, EventArgs e)
        {
           
            if (Validation() != false && CountyLinkId == 0 && Duplicate_Record() != false)
            {
                //Insertion Code
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new DataTable();
                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@State", int.Parse(ddl_State.SelectedValue.ToString()));
                htinsert.Add("@County", int.Parse(ddl_County.SelectedValue.ToString()));
                htinsert.Add("@Index_Availability", txt_Index_Availability.Text);
                htinsert.Add("@Index_date_range", txt_Index_date_range.Text);
                htinsert.Add("@Back_deed_search", txt_Back_deed_search.Text);
                htinsert.Add("@Back_deed_range", txt_Back_deed_range.Text);
                htinsert.Add("@Images", txt_Images.Text);
                htinsert.Add("@Images_date_of_range", txt_Images_date_of_range.Text);
                htinsert.Add("@Land_Records_Link", txt_Land_Records_Link.Text);
                htinsert.Add("@Subscription_Link", txt_Subscription_Link.Text);
                htinsert.Add("@Plant_availability", txt_Plant_availability.Text);
                htinsert.Add("@Inserted_By", User_Id);
                htinsert.Add("@Instered_Date", DateTime.Now);
                htinsert.Add("@Status", "True");
                dtinsert = dataaccess.ExecuteSP("Sp_County_Link", htinsert);
                string title = "Insert";
                MessageBox.Show("County Link Inserted Successfully",title);
                //grp_CountyReg.Visible = false;
                //grp_CountyInfo.Visible = true;
                btn_Add_New.Text = "Back";

                label4.Visible = false;
                label26.Visible = false;
                label25.Visible = true;
                btn_Cancel_Click(sender,e);
                btn_GetImportExcel.Visible = false;
                btn_Export_County.Visible = false;
                Btn_Upload.Visible = false;
                btn_Import.Visible = false;
                ddl_State.Select();
               // Grdiview_Bind_Tax_County_Link();
            
                grp_CountyReg.Visible = true;
                grp_CountyInfo.Visible = false;
                
            }
            else if (CountyLinkId != 0 && Validation() != false && Edit_Duplicate_Record()!=false)
            {
                //Updation Code
                grd_CountyImport.Rows.Clear();
                int Stateid;
                int Countyid;
                Hashtable htbarowerstate = new Hashtable();
                DataTable dtbarrowerstate = new System.Data.DataTable();
                htbarowerstate.Add("@Trans", "GETSTATE");
                htbarowerstate.Add("@state_name", ddl_State.Text);
                dtbarrowerstate = dataaccess.ExecuteSP("Sp_Order_Get_Details", htbarowerstate);
                if (dtbarrowerstate.Rows.Count > 0)
                {

                    Stateid = int.Parse(dtbarrowerstate.Rows[0]["State_ID"].ToString());
                }
                else
                {
                    Stateid = 0;
                }
                //get County
                Hashtable htBarcounty = new Hashtable();
                DataTable dtbarcounty = new System.Data.DataTable();
                htBarcounty.Add("@Trans", "GETCOUNTY");
                htBarcounty.Add("@State", Stateid);
                htBarcounty.Add("@County_Name", ddl_County.Text);
                dtbarcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty);
                if (dtbarcounty.Rows.Count > 0)
                {
                    Countyid = int.Parse(dtbarcounty.Rows[0]["County_ID"].ToString());
                }
                else
                {
                    Countyid = 0;
                }


                Hashtable htupdate = new Hashtable();
                DataTable dtupdate = new DataTable();
                htupdate.Add("@Trans","UPDATE");
                htupdate.Add("@County_Link_Id", CountyLinkId);
                htupdate.Add("@State", Stateid);
                htupdate.Add("@County", Countyid);

                htupdate.Add("@Index_Availability", txt_Index_Availability.Text);
                htupdate.Add("@Index_date_range", txt_Index_date_range.Text);
                htupdate.Add("@Back_deed_search", txt_Back_deed_search.Text);
                htupdate.Add("@Back_deed_range", txt_Back_deed_range.Text);
                htupdate.Add("@Images", txt_Images.Text);
                htupdate.Add("@Images_date_of_range", txt_Images_date_of_range.Text);
                htupdate.Add("@Land_Records_Link", txt_Land_Records_Link.Text);
                htupdate.Add("@Subscription_Link", txt_Subscription_Link.Text);
                htupdate.Add("@Plant_availability", txt_Plant_availability.Text);
                htupdate.Add("@Modified_By", User_Id);
                htupdate.Add("@Modified_Date", DateTime.Now);
                dtupdate = dataaccess.ExecuteSP("Sp_County_Link", htupdate);


                string title = "Update";
                MessageBox.Show("County Link Updated Successfully", title);
               // Grdiview_Bind_Tax_County_Link();
                grp_CountyReg.Visible = true;
                grp_CountyInfo.Visible = false;
                btn_Add_New.Text = "Back";
               
                btn_Add_New_Tax.Text = "Add";
               

                btn_GetImportExcel.Visible = false;
                btn_Export_County.Visible = false;
                Btn_Upload.Visible = false;
                btn_Import.Visible = false;
                ddl_State.Select();
               btn_Cancel_Click(sender, e);
              //  btn_Refresh.Visible = false;
               label4.Visible = false;
               label26.Visible = false;
               label25.Visible = true;
            }
           
        }

        private bool Validation()
        {
            string title = "Validation!";
            if (ddl_State.SelectedIndex == 0)
            {
                MessageBox.Show("Select State Name", title);
                ddl_State.Focus();
                return false;
            }
            else if (ddl_County.SelectedIndex == 0 )
            {
                MessageBox.Show("Select County Name", title);
                ddl_County.Focus();
                return false;
            }
            else if (txt_Index_Availability.Text == "")
            {
                MessageBox.Show("Please Enter Index availability of county link", title);
                txt_Index_Availability.Focus();
                return false;
            }
            else if (txt_Land_Records_Link.Text=="")
            {
                MessageBox.Show("Please Enter Land Records link", title);
                txt_Land_Records_Link.Focus();
                return false;
            }

            else if (txt_Subscription_Link.Text == "")
            {
                MessageBox.Show("Please Enter Subscription link", title);
                txt_Subscription_Link.Focus();
                return false;
            }


            return true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            CountyClear();
            label4.Visible = false;
            label26.Visible = false;
            label25.Visible = true;
            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
        }

        private void grd_CountyImport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex != -1)
            {
            //if (e.ColumnIndex != -1)
            //{
                
                //View code
                if (e.ColumnIndex == 12)
                {
                    label4.Visible = false;
                    label26.Visible = true;
                    label25.Visible = false;
                    Btn_Upload.Visible = false;
                    btn_GetImportExcel.Visible = false;

                    btn_Export_County.Visible = false;
                    btn_Import.Visible = false;

                    pictureBox3.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    label27.Visible = false;
                    label28.Visible = false;
                    label29.Visible = false;

                    btn_Remove_Duplic.Visible = false;
                    btn_removedup.Visible = false;
                    btn_Remove_Error_row.Visible = false;
                    btn_Refresh.Visible = false;
                    grp_CountyInfo.Visible = false;
                    grp_CountyReg.Visible = true;
                    CountyLinkId = int.Parse(grd_CountyImport.Rows[e.RowIndex].Cells[14].Value.ToString());

                    Hashtable htselect = new Hashtable();
                    DataTable dtselect = new DataTable();
                    htselect.Add("@Trans", "SELECT");
                    htselect.Add("@County_Link_Id", CountyLinkId);
                    dtselect = dataaccess.ExecuteSP("Sp_County_Link", htselect);
                    if (dtselect.Rows.Count > 0)
                    {
                        ddl_State.Text = dtselect.Rows[0]["State"].ToString();
                        statename = ddl_State.Text.ToString();

                        ddl_County.Text = dtselect.Rows[0]["County"].ToString();
                        countyname = ddl_County.Text.ToString();

                        txt_Index_Availability.Text = dtselect.Rows[0]["Index_Availability"].ToString();
                        txt_Index_date_range.Text = dtselect.Rows[0]["Index_date_range"].ToString();
                        txt_Back_deed_search.Text = dtselect.Rows[0]["Back_deed_search"].ToString();
                        txt_Back_deed_range.Text = dtselect.Rows[0]["Back_deed_range"].ToString();
                        txt_Images.Text = dtselect.Rows[0]["Images"].ToString();
                        txt_Images_date_of_range.Text = dtselect.Rows[0]["Images_date_of_range"].ToString();
                        txt_Land_Records_Link.Text = dtselect.Rows[0]["Land_Records_Link"].ToString();
                        txt_Subscription_Link.Text = dtselect.Rows[0]["Subscription_Link"].ToString();
                        txt_Plant_availability.Text = dtselect.Rows[0]["Plant_availability"].ToString();  

                        lbl_Record_Addedby.Text = dtselect.Rows[0]["User_Name"].ToString();
                        lbl_Record_AddedDate.Text = dtselect.Rows[0]["Instered_Date"].ToString();
                        btn_Add_New_Tax.Text = "Edit";
                        btn_Add_New.Text = "Back";
                    }
                  //  Grdiview_Bind_Tax_County_Link();
                }
                //Delete code
                else if (e.ColumnIndex == 13)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        CountyLinkId = int.Parse(grd_CountyImport.Rows[e.RowIndex].Cells[14].Value.ToString());
                        Hashtable htdelete = new Hashtable();
                        DataTable dtdelete = new DataTable();
                        htdelete.Add("@Trans", "DELETE");
                        htdelete.Add("@County_Link_Id", CountyLinkId);
                        dtdelete = dataaccess.ExecuteSP("Sp_County_Link", htdelete);
                      //  string title = "Delete!";
                        inset = 1;
                       
                       //// MessageBox.Show("Record Deleted Successfully");
                       // CountyLinkId = 0;
                       // btn_Refresh_Click(sender,e);
                    }

                    if(inset ==1)
                    {
                        MessageBox.Show("Record Deleted Successfully");
                        CountyLinkId = 0;
                        btn_Refresh_Click(sender, e);
                        Grdiview_Bind_Tax_County_Link();
                    }
                    
                }
            }
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            grd_CountyImport.Rows.Clear();
            
            ddl_State_Wise.SelectedIndex = 0;
            cbo_County.SelectedIndex = -1;
            Grdiview_Bind_Tax_County_Link();
            dbc.BindCounty(cbo_County, int.Parse(ddl_State_Wise.SelectedValue.ToString()));
           
        }

        private void btn_GetImportExcel_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"c:\OMS_Import\County_Link_Import.xlsx";
            if (!Directory.Exists(temppath))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\County_Link_Import.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
            currentPageIndex--;
            if (currentPageIndex == 0)
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
            if (ddl_State.SelectedIndex > 0 )
            {
                Filter_State_Data();
            }
            else if (cbo_County.SelectedIndex > 0)
            {
                Filter_County_Data();
            }
            else
            {
                Grdiview_Bind_Tax_County_Link();
            }
            Grdiview_Bind_Tax_County_Link();
            this.Cursor = currentCursor;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentPageIndex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (ddl_State.SelectedIndex > 0)
            {
                Filter_State_Data();
            }
            else if (cbo_County.SelectedIndex > 0)
            {
                Filter_County_Data();
            }
            else
            {
                Grdiview_Bind_Tax_County_Link();
            }
            Grdiview_Bind_Tax_County_Link();
            this.Cursor = currentCursor;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentPageIndex++;
            if (ddl_State.SelectedIndex > 0 )
            {

                if (this.currentPageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pageSize) - 1)
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
                this.Cursor = currentCursor;
            }
            else if (cbo_County.SelectedIndex > 0)
            {
                if (currentPageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize) - 1)
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
                this.Cursor = currentCursor;
            }
            
            else
            {
               
                if (currentPageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                   // Grdiview_Bind_Tax_County_Link();
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                   // Grdiview_Bind_Tax_County_Link();
                }
               
                Grdiview_Bind_Tax_County_Link();
                this.Cursor = currentCursor;
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;

           // Grdiview_Bind_Tax_County_Link();
            this.Cursor = currentCursor;
        }

       

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (ddl_State.SelectedIndex > 0 )
            {
                currentPageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pageSize) - 1;
                Filter_State_Data();
            }
            else if (cbo_County.SelectedIndex > 0)
            {
                currentPageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize) - 1;
                Filter_County_Data();
            }
            else
            {
                currentPageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize) - 1;
                Grdiview_Bind_Tax_County_Link();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
          //  Grdiview_Bind_Tax_County_Link();
            this.Cursor = currentCursor;
        }

        private void grd_CountyImport_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = grd_CountyImport.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = grd_CountyImport.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    grd_CountyImport.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                  
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                    
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
               
            }

            // Sort the selected column.
            grd_CountyImport.Sort(newColumn, direction);
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;


            //grd_CountyImport.datasource=
        }

        private void grd_CountyImport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Put each of the columns into programmatic sort mode.
            foreach (DataGridViewColumn column in grd_CountyImport.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void grd_CountyImport_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            // Try to sort based on the cells in the current column.
            e.SortResult = System.String.Compare(
                e.CellValue1.ToString(), e.CellValue2.ToString());

            // If the cells are equal, sort based on the ID column.
            if (e.SortResult == 0 && e.Column.Name != "County_Link_Id")
            {
                e.SortResult = System.String.Compare(
                    grd_CountyImport.Rows[e.RowIndex1].Cells["County_Link_Id"].Value.ToString(),
                    grd_CountyImport.Rows[e.RowIndex2].Cells["County_Link_Id"].Value.ToString());
            }
            e.Handled = true;
        }

        private void txt_Index_Availability_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("Invalid!");
            //    }
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Index_Availability.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Index_date_range_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("Invalid!");
            //    }
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Index_date_range.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void txt_Back_deed_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Back_deed_search.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void txt_Back_deed_range_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("Invalid!");
            //    }
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Back_deed_range.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void txt_Images_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Images.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void txt_Images_date_of_range_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("Invalid!");
            //    }
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Images_date_of_range.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void txt_Land_Records_Link_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Land_Records_Link.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void txt_Subscription_Link_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Subscription_Link.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void txt_Plant_availability_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Plant_availability.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

        }

        private void btn_Export_County_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
           
           Grid_Export_Data();

            //Microsoft.Office.Interop.Excel.Application objexcelapp = new Microsoft.Office.Interop.Excel.Application();
            //objexcelapp.Application.Workbooks.Add(Type.Missing);
            //objexcelapp.Columns.ColumnWidth = 25;
            //for (int i = 1; i < grd_CountyImport.Columns.Count + 1; i++)
            //{
            //    objexcelapp.Cells[1, i] = grd_CountyImport.Columns[i - 1].HeaderText;
            //}


            ///*For storing Each row and column value to excel sheet*/
            //for (int i = 0; i < grd_CountyImport.Rows.Count; i++)
            //{
            //    for (int j = 0; j < grd_CountyImport.Columns.Count; j++)
            //    {
            //        if (grd_CountyImport.Rows[i].Cells[j].Value != null)
            //        {
            //            objexcelapp.Cells[i + 2, j + 1] = grd_CountyImport.Rows[i].Cells[j].Value.ToString();
            //        }
            //    }
            //}
            //MessageBox.Show("Your excel file exported successfully at C:\\Temp\\" + "County_Link" + ".xlsx");
            //objexcelapp.ActiveWorkbook.SaveCopyAs("C:\\Temp\\" + "County_Link" + ".xlsx");
            //objexcelapp.ActiveWorkbook.Saved = true;

           

                
        }
  

        private void Grid_Export_Data()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in grd_CountyImport.Columns)
            {
                if (column.Index != 0)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }

                    }
                }
            }
            //Adding rows in Excel
            foreach (DataGridViewRow row in grd_CountyImport.Rows)
            {
                if (count < grd_CountyImport.Rows.Count-1)
                {

                dt.Rows.Add();

                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count-1][cell.ColumnIndex-1] = cell.Value.ToString();

                        }
                    }
                    }
                }
                count++;
            }
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "County_Link" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "County_Link");
                try
                {
                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it", title);
                }
            }
            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_Remove_Duplic_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_CountyImport.Rows.Count - 1; i++)
            {

                if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Cyan)
                {
                    grd_CountyImport.Rows.RemoveAt(i);
                    i = i - 1;
                   
                }
                lbl_count.Text = (grd_CountyImport.Rows.Count-1).ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(grd_CountyImport.Rows.Count) / pageSize);
            }
            //lbl_count.Text = grd_CountyImport.Rows.Count.ToString();
            //lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(grd_CountyImport.Rows.Count) / pageSize);
        } 

        private void btn_removedup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_CountyImport.Rows.Count - 1; i++)
            {

                if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Yellow)
                {
                    grd_CountyImport.Rows.RemoveAt(i);
                    i = i - 1;
                    
                }
                lbl_count.Text = (grd_CountyImport.Rows.Count-1).ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(grd_CountyImport.Rows.Count) / pageSize);
            }
           
        } 

   

        private void btn_Remove_Error_row_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_CountyImport.Rows.Count - 1; i++)
            {
                if (grd_CountyImport.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
                {
                    grd_CountyImport.Rows.RemoveAt(i);
                    i = i - 1;
                   
                }
                lbl_count.Text = (grd_CountyImport.Rows.Count-1).ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(grd_CountyImport.Rows.Count) / pageSize);
            }
            //lbl_count.Text = grd_CountyImport.Rows.Count.ToString();
           // lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(grd_CountyImport.Rows.Count) / pageSize);
        }

        private void btn_Add_New_TabIndexChanged(object sender, EventArgs e)
        {
            ddl_State_Wise.Select();
        }

      
     
    }
}
