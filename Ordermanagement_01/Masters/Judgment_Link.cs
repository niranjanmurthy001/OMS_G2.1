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
using System.IO;
using ClosedXML.Excel;
using System.Data.OleDb;



namespace Ordermanagement_01
{
    public partial class Judgment_Link : Form
    {
        System.Data.DataTable dtnonadded = new System.Data.DataTable();
        System.Data.DataTable dtnonadded1 = new System.Data.DataTable();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        System.Data.DataTable dtselect = new System.Data.DataTable();
        System.Data.DataTable dtsort = new System.Data.DataTable();
        System.Data.DataTable dtcounty = new System.Data.DataTable();
        InfiniteProgressBar.clsProgress progBar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int UserId, JudgmentID = 0, count = 0, count_non = 0, value=0;
       // static int CurrentpageIndex = 0;
        private int currentPageIndex = 1;
        private int pageSize = 50;
      //  int Pagesize = 10;
        string username;
        string state;
        string county;
        string duplicate, StateAbr;
        DateTime ResearchDate;
        int userid = 0, state_id, stateid, countyid, insert = 0, state_insert, county_Id, State_id;

        string statename, countyname;

        int state_ID, county_ID;


        public Judgment_Link(int User_Id,string UserName)
        {
            InitializeComponent();
            UserId = User_Id;
            username = UserName;
            dbc.BindState(ddl_State);
            dbc.BindState(cbo_State);
            dbc.BindCounty(ddl_County,int.Parse(ddl_State.SelectedValue.ToString()));
            dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
        }

        private void Btn_Upload_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            label15.Visible = false;
            label16.Visible = false;

            grp_JudgmentInfo.Visible = true;
            grp_JugmentReg.Visible = false;

            Judgment_view.Visible = false;
            Judgment_delete.Visible = false;

            Grd_Judgment_Link.Rows.Clear();

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
                BindJugmentGrid();
            }
            
        }

        private void Import(string txtFileName)
        {
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

                    if (data.Rows.Count > 0)
                    {
                        Grd_Judgment_Link.Rows.Clear();
                        for (int i = 0; i < data.Rows.Count; i++)
                        {

                            string State_name = data.Rows[i]["State"].ToString();
                            string County_name = data.Rows[i]["County"].ToString();

                            Grd_Judgment_Link.Rows.Add();
                            Grd_Judgment_Link.Rows[i].Cells[0].Value = i + 1;
                            Grd_Judgment_Link.Rows[i].Cells[1].Value = data.Rows[i]["State"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[2].Value = data.Rows[i]["County"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[3].Value = data.Rows[i]["Research_Date"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[4].Value = data.Rows[i]["Judgment_Link"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[5].Value = data.Rows[i]["Lien_Link"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[6].Value = data.Rows[i]["Criminal"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[7].Value = data.Rows[i]["Subscription"].ToString();

                            //Grd_Judgment_Link.Rows[i].Cells[10].Value = "View";
                            //Grd_Judgment_Link.Rows[i].Cells[11].Value = "Delete";

                            Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                            Grd_Judgment_Link.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            Grd_Judgment_Link.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            Grd_Judgment_Link.Rows[i].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                            ////Check County Exist  -- County is unique
                            Hashtable ht_order = new Hashtable();
                            DataTable dt_order = new DataTable();
                            ht_order.Add("@Trans", "SEARCH_COUNTY");
                            ht_order.Add("@StateName", State_name);
                            ht_order.Add("@county_Name", County_name);

                            dt_order = dataaccess.ExecuteSP("Sp_Judgment_Link", ht_order);
                            if (dt_order.Rows.Count > 0)
                            {
                                State_id = int.Parse(dt_order.Rows[0]["State_ID"].ToString());
                                county_Id = int.Parse(dt_order.Rows[0]["County_ID"].ToString());
                                StateAbr = dt_order.Rows[0]["Abbreviation"].ToString();

                                Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            }
                            //else
                            //{
                            //    Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            //}

                            //Duplicate of records
                            for (int j = 0; j < i; j++)
                            {

                                string state = data.Rows[j]["State"].ToString();
                                string county = data.Rows[j]["County"].ToString();

                                if (state == State_name && county == County_name)
                                {

                                    Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                                }
                                else
                                {
                                    value = 0;
                                }

                            }


                            //error in County or state

                            Hashtable htord = new Hashtable();
                            DataTable dtord = new DataTable();
                            htord.Add("@Trans", "COUNTY_SEARCH");
                            htord.Add("@StateName", Grd_Judgment_Link.Rows[i].Cells[1].Value);
                            htord.Add("@county_Name", Grd_Judgment_Link.Rows[i].Cells[2].Value);

                            dtord = dataaccess.ExecuteSP("Sp_Judgment_Link", htord);
                            if (dtord.Rows.Count > 0)
                            {

                                county_Id = int.Parse(dtord.Rows[0]["County_ID"].ToString());
                            }
                            else
                            {
                                //abbrid = 0;
                                Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                            }

                            if (State_name == "" || County_name == "")
                            {
                                Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }

                        }
                        btn_Import.Visible = true;
                        Judgment_view.Visible = false;
                        Judgment_delete.Visible = false;

                        lbl_count.Text = data.Rows.Count.ToString();
                        lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(data.Rows.Count) / pageSize);
                    }
            }
                    else
                    {
                        string title = "Empty!";
                        MessageBox.Show("Check the Excel is empty",title);
                    }
                
              //  }
            
        }


        private bool validate()
        {
            int error = 0, dupl = 0, exist = 0, empty = 0;
            for (int i = 0; i <=Grd_Judgment_Link.Rows.Count - 1; i++)
            {
                if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                {

                    return true;
                }
                else
                {
                    if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
                    {
                        error = error + 1;
                        //return false;
                    }

                    else if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Cyan)
                    {
                        dupl = dupl + 1;
                       // return false;
                    }
                    else if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Yellow)
                    {
                        exist = exist + 1;
                        
                    }
                    else if(Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.SystemColors.Control)
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
            int error = 0, dupl = 0, exist = 0,empty=0;
            form_loader.Start_progres();
            //progBar.startProgress();
            count = 0; count_non = 0;
            int count_grd = Grd_Judgment_Link.Rows.Count;
            int count_ex = 0;
            if (validate()!=false)
            {
            for (int i = 0; i < Grd_Judgment_Link.Rows.Count - 1; i++)
            {
                if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                {
                    int Stateid;
                    int Countyid;
                    Hashtable htbarowerstate = new Hashtable();
                    DataTable dtbarrowerstate = new System.Data.DataTable();
                    // htbarowerstate.Add("@Trans", "GETSTATE_BY_ABR");
                    htbarowerstate.Add("@Trans", "GETSTATE_BY_STATENAME");

                    //htbarowerstate.Add("@state_name", Grd_Judgment_Link.Rows[i].Cells[1].Value.ToString());
                    htbarowerstate.Add("@state_name", Grd_Judgment_Link.Rows[i].Cells[1].Value.ToString());
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
                    htBarcounty.Add("@County_Name", Grd_Judgment_Link.Rows[i].Cells[2].Value.ToString());
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
                        ht.Add("@Trans", "SORT_COUNTY");
                        ht.Add("@State_Id", Stateid);
                        ht.Add("@County_Id", Countyid);
                        dt = dataaccess.ExecuteSP("Sp_Judgment_Link", ht);
                        if (dt.Rows.Count == 0)
                        {
                            //if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                            //{
                            Hashtable ht_INSERT = new Hashtable();
                            DataTable dt_INSERT = new System.Data.DataTable();
                            ht_INSERT.Add("@Trans", "INSERT");
                            ht_INSERT.Add("@State_Id", Stateid);
                            ht_INSERT.Add("@County_Id", Countyid);
                            ht_INSERT.Add("@Research_Date", Grd_Judgment_Link.Rows[i].Cells[3].Value.ToString());
                            ht_INSERT.Add("@Judgment_Link", Grd_Judgment_Link.Rows[i].Cells[4].Value.ToString());
                            ht_INSERT.Add("@Lien_Link", Grd_Judgment_Link.Rows[i].Cells[5].Value.ToString());
                            ht_INSERT.Add("@Criminal", Grd_Judgment_Link.Rows[i].Cells[6].Value.ToString());
                            ht_INSERT.Add("@Subscription", Grd_Judgment_Link.Rows[i].Cells[7].Value.ToString());
                            ht_INSERT.Add("@Inserted_By", UserId);
                            ht_INSERT.Add("@Status", "True");
                            dt_INSERT = dataaccess.ExecuteSP("Sp_Judgment_Link", ht_INSERT);
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
                //    if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
                //    {
                //        error = error + 1;
                //    }

                //    else if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Cyan)
                //    {
                //        dupl = dupl + 1;
                //    }
                //    else if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Yellow)
                //    {
                //        exist = exist + 1;
                //    }
                //    //else if(Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Gray)
                //    //{
                //    //    empty = empty+1;
                //    //}
                    
                //}
              
            }
         
                if (count > 0)
                {
                    string title = "Successfull";
                    MessageBox.Show(count + " No of Judgment Link Records Imported successfully", title);
                    BindJugmentGrid();
                    btn_Import.Visible = true;
                    Judgment_view.Visible = true;
                    Judgment_delete.Visible = true;
                }
                
                //if (error > 0)
                //{

                //    string title1 = "Error!";
                //    MessageBox.Show(" ' " + error + " ' " + " Invalid!, Check the Incorrect Values in Excel", title1);
                //    // error = 0;
                //    //break;
                //}

                //if (dupl > 0)
                //{
                //    string title1 = "Duplicate Data!";
                //    MessageBox.Show(" ' " + dupl + " ' " + "Duplicate data in Excel", title1);
                //}

                //if (exist > 0)
                //{
                //    string title = "Existed!";
                //    MessageBox.Show(" ' " + exist + " ' " + "  No of County Link Records Already Exists", title);
                //}
                //if(empty>0)
                //{
                //  string title = "Empty!";
                //    MessageBox.Show(" ' " + exist + " ' " + "  No of Row is Empty", title);
                // }

                //BindJugmentGrid();
                //btn_Import.Visible = true;
                //Judgment_view.Visible = true;
                //Judgment_delete.Visible = true;

        }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (btn_Add.Text == "Add New")
            {
                JudgmentClear();
                grp_JugmentReg.Visible = true;
                grp_JudgmentInfo.Visible = true;

                btn_Import.Visible = false;
                btn_Export.Visible = false;

                label4.Visible = false;
                label15.Visible = true;
                label16.Visible = false;
                btn_Refresh.Visible = false;
                btn_Import.Visible = false;

                Btn_Upload.Visible = false;
                btn_GetImportExcel.Visible = false;

                pictureBox3.Visible = false;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                label20.Visible = false;

                btn_Remove_Duplic.Visible = false;
                btn_removedup.Visible = false;
                btn_Remove_Error_row.Visible = false;
                btn_Add.Text = "Back";
                btn_AddJugment.Text = "Add";

              
               
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
               
                lbl_Record_Addedby.Text = username;
                lbl_Record_AddedDate.Text = DateTime.Now.ToString();
               // btn_Refresh_Click(sender, e);
            
            }
            else
            {
                label4.Visible = true;
                label15.Visible = false;
                label16.Visible = false;

                btn_Import.Visible = true;
                btn_Export.Visible = true;
                Btn_Upload.Visible = true;
                btn_GetImportExcel.Visible = true;
                btn_Refresh.Visible = true;

                btn_Remove_Duplic.Visible = true;
                btn_removedup.Visible = true;
                btn_Remove_Error_row.Visible = true;

                grp_JudgmentInfo.Visible = true;
                grp_JugmentReg.Visible = false;

                pictureBox3.Visible = true;
                label20.Visible = true;
                pictureBox1.Visible = true;
                label19.Visible = true;
                pictureBox2.Visible = true;
                label18.Visible = true;
                btn_Add.Text = "Add New";

                //grp_JudgmentInfo.Visible = true;
                //grp_JugmentReg.Visible = true;
                JudgmentClear();
               
                cbo_State.SelectedIndex = 0;
                dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
               
                BindJugmentGrid();
             
            }
            
        }

        private void JudgmentClear()
        {
            Grd_Judgment_Link.Refresh();
            ddl_State.SelectedIndex = 0;
            ddl_County.SelectedIndex = -1;
            txt_ResearchDate.Text = "";
            txt_Criminallink.Text = "";
            txt_Judgmentlink.Text = "";
            txt_Lienlink.Text = "";
            txt_Subscriptionlink.Text = "";
            ////lbl_Record_Addedby.Text = "";
            ////lbl_Record_AddedDate.Text = "";

            label4.Visible = false;
            label15.Visible = true;
            label16.Visible = false;
            btn_AddJugment.Text = "Add";
            dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
           // BindJugmentGrid();

        }


        private void Judgment_Link_Load(object sender, EventArgs e)
        {
            btn_Import.Visible = true;
            btn_Export.Visible = true;
            grp_JugmentReg.Visible = false;
            grp_JudgmentInfo.Visible = true;    
           
            First_Page();
            dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
            label4.Visible = true;
            label15.Visible = false;
            label16.Visible=false;
            BindJugmentGrid();
            Btn_Upload.Select();
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

        private void BindJugmentGrid()
        {
            form_loader.Start_progres();
            //progBar.startProgress();

            Judgment_view.Visible = true;
            Judgment_delete.Visible = true;
            Grd_Judgment_Link.Rows.Clear();
            Hashtable htselect = new Hashtable();
            // System.Data.DataTable dtselect = new System.Data.DataTable();



            if (cbo_State.SelectedIndex > 0)
            {
                htselect.Add("@Trans", "SORT_STATE");
                htselect.Add("@State_Id", int.Parse(cbo_State.SelectedValue.ToString()));
                if (cbo_County.SelectedIndex > 0)
                {
                    htselect.Clear();
                    htselect.Add("@Trans", "SORT_COUNTY");

                    htselect.Add("@State_Id", int.Parse(cbo_State.SelectedValue.ToString()));
                    htselect.Add("@County_Id", int.Parse(cbo_County.SelectedValue.ToString()));
                }
            }
            else
            {

                htselect.Add("@Trans", "SELECTGRID");
            }

           // Grd_Judgment_Link.Rows.Clear();
            dtselect = dataaccess.ExecuteSP("Sp_Judgment_Link", htselect);

            System.Data.DataTable temptable = dtselect.Clone();
            //Grd_Judgment_Link.DataSource = temptable;
            int startIndex = currentPageIndex * pageSize;
            int endIndex = currentPageIndex * pageSize + pageSize;
            if (endIndex > dtselect.Rows.Count)
            {
                endIndex = dtselect.Rows.Count;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                DataRow Row = temptable.NewRow();
                //DataColumn col=temptable.
                Get_New_Row_Column(ref Row, dtselect.Rows[i]);
                temptable.Rows.Add(Row);
            }
         //   Grd_Judgment_Link.Rows.Clear();
            //Grd_Judgment_Link.Columns.Add();
            if (temptable.Rows.Count > 0)
            {
                //Grd_Judgment_Link.Rows.Clear();
           
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                   // Grd_Judgment_Link.Columns.Add();
                    //Grd_Judgment_Link.ColumnCount = 13;
                    //Grd_Judgment_Link.Columns[0].Name = "Sl.NO";
                    //Grd_Judgment_Link.Columns[1].Name = "State";
                    //Grd_Judgment_Link.Columns[2].Name = "County";
                    //Grd_Judgment_Link.Columns[3].Name = "Research Date";
                    //Grd_Judgment_Link.Columns[4].Name = "Judgment_Link";
                    //Grd_Judgment_Link.Columns[5].Name = "Lien_Link";
                    //Grd_Judgment_Link.Columns[6].Name = "Criminal";
                    //Grd_Judgment_Link.Columns[7].Name = "Subscription";
                    //Grd_Judgment_Link.Columns[8].Name = "State_ID";
                    //Grd_Judgment_Link.Columns[9].Name = "County_ID";
                    //Grd_Judgment_Link.Columns[12].Name = "Judgment_Links_Id";
                    //Grd_Judgment_Link.Columns[8].Visible = false;
                    //Grd_Judgment_Link.Columns[9].Visible = false;
                    //Grd_Judgment_Link.Columns[12].Visible = false;

                    //Grd_Judgment_Link.Columns[10].Name = "View";
                    //Grd_Judgment_Link.Columns[11].Name = "Delete";

                    Grd_Judgment_Link.Rows.Add();
                    Grd_Judgment_Link.Rows[i].Cells[0].Value = i + 1;
                    Grd_Judgment_Link.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString(); ;
                    Grd_Judgment_Link.Rows[i].Cells[3].Value = temptable.Rows[i]["Research_Date"].ToString().Trim();
                    Grd_Judgment_Link.Rows[i].Cells[4].Value = temptable.Rows[i]["Judgment_Link"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[5].Value = temptable.Rows[i]["Lien_Link"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[6].Value = temptable.Rows[i]["Criminal"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[7].Value = temptable.Rows[i]["Subscription"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[8].Value = temptable.Rows[i]["State_ID"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[9].Value = temptable.Rows[i]["County_ID"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[12].Value = temptable.Rows[i]["Judgment_Links_Id"].ToString();

                    Grd_Judgment_Link.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                    Grd_Judgment_Link.Rows[i].Cells[5].Style.WrapMode = DataGridViewTriState.True;

                   
                    Grd_Judgment_Link.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Judgment_Link.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Judgment_Link.Rows[i].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                 
                }
            }
            else
            {
                Grd_Judgment_Link.Rows.Clear();
                Grd_Judgment_Link.Visible = true;
                Grd_Judgment_Link.DataSource = null;
            }
            lbl_count.Text =" Total Records " + dtselect.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize);

           // progBar.stopProgress();


        }

        private void Get_New_Row_Column(ref DataRow Row,DataRow Source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                Row[col.ColumnName] = Source[col.ColumnName];
            }
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            JudgmentClear();
            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
        }

        private bool Duplicate_Record()
        {
            string state, county;
            Hashtable ht_checkDuplicate = new Hashtable();
            DataTable dt_checkDuplicate = new DataTable();

            ht_checkDuplicate.Add("@Trans", "CHECK_DUPLICATE");
            ht_checkDuplicate.Add("@State_Id", ddl_State.SelectedValue.ToString());
            ht_checkDuplicate.Add("@County_Id", ddl_County.SelectedValue.ToString());
            dt_checkDuplicate = dataaccess.ExecuteSP("Sp_Judgment_Link", ht_checkDuplicate);

            if (dt_checkDuplicate.Rows.Count > 0)
            {

                for (int i = 0; i <= dt_checkDuplicate.Rows.Count; i++)
                {
                    state = dt_checkDuplicate.Rows[0]["State_ID"].ToString();
                    county = dt_checkDuplicate.Rows[0]["County_ID"].ToString();

                    string selected_state = ddl_State.SelectedValue.ToString();
                    string selected_County = ddl_County.SelectedValue.ToString();

                    if (state == selected_state && county == selected_County && btn_AddJugment.Text != "Edit")
                    {
                        duplicate = "Duplicate Data";
                        string title = "Duplicate Record!";
                        MessageBox.Show("Record Already Existed", title);
                        JudgmentClear();
                        return false;
                    }
                }
            }
            return true;
        }

        private bool Edit_Duplicate_Record()
        {
           
           
            Hashtable ht_checkDuplicate = new Hashtable();
            DataTable dt_checkDuplicate = new DataTable();

            ht_checkDuplicate.Add("@Trans", "EDIT_CHECK_DUPLICATE");
            ht_checkDuplicate.Add("@StateName", statename);
            ht_checkDuplicate.Add("@county_Name", countyname);
            dt_checkDuplicate = dataaccess.ExecuteSP("Sp_Judgment_Link", ht_checkDuplicate);
            if (dt_checkDuplicate.Rows.Count > 0)
            {
                stateid = int.Parse(dt_checkDuplicate.Rows[0]["State_ID"].ToString());
                countyid = int.Parse(dt_checkDuplicate.Rows[0]["County_ID"].ToString());
            }

            Hashtable ht_check = new Hashtable();
            DataTable dt_check = new DataTable();
            ht_check.Add("@Trans", "SORT_COUNTY");
            ht_check.Add("@State_Id", ddl_State.SelectedValue.ToString());
            ht_check.Add("@County_Id", ddl_County.SelectedValue.ToString());
            dt_check = dataaccess.ExecuteSP("Sp_Judgment_Link", ht_check);
            if (dt_check.Rows.Count > 0)
            {
                state_ID = int.Parse(dt_check.Rows[0]["State_ID"].ToString());
                county_ID = int.Parse(dt_check.Rows[0]["County_ID"].ToString());
            }

            if (state_ID == stateid && county_ID == countyid && btn_AddJugment.Text == "Edit")
            {
                return true;
            }
            else
            {
                if (state_ID != stateid && btn_AddJugment.Text == "Edit")
                {
                    duplicate = "Duplicate Data";
                    string title = "Duplicate Record!";
                    MessageBox.Show("Record Already Existed", title);
                    JudgmentClear();
                    return false;
                }
                else if (county_ID != countyid && btn_AddJugment.Text == "Edit")
                {
                    duplicate = "Duplicate Data";
                    string title = "Duplicate Record!";
                    MessageBox.Show("Record Already Existed", title);
                    JudgmentClear();
                    return false;
                }
            }
            return true;
           // Hashtable ht_check_Duplicate = new Hashtable();
           // DataTable dt_check_Duplicate = new DataTable();

           // ht_check_Duplicate.Add("@Trans", "CHECK_DUPLICATE");
           // ht_check_Duplicate.Add("@State_Id", ddl_State.SelectedValue.ToString());
           // ht_check_Duplicate.Add("@County_Id", ddl_County.SelectedValue.ToString());
           // dt_check_Duplicate = dataaccess.ExecuteSP("Sp_Judgment_Link", ht_check_Duplicate);

           //if (dt_check_Duplicate.Rows.Count == 0)
           // {
           //     return true;
           // }
           // else
           // {
           //     duplicate = "Duplicate Data";
           //     string title = "Duplicate Record!";
           //     MessageBox.Show("Record Already Existed", title);
           //     JudgmentClear();
           //     return false;
           // }
           
        }

        private void btn_AddJugment_Click(object sender, EventArgs e)
        {
            if (Validation() != false)
            {
                //if (Duplicate_Record() != false)
                //{
                    if (JudgmentID == 0 && Duplicate_Record() != false)
                    {
                        label4.Visible = false;
                        label15.Visible = true;
                        label16.Visible = false;
                        Hashtable htinsert = new Hashtable();
                        DataTable dtinsert = new DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@State_Id", int.Parse(ddl_State.SelectedValue.ToString()));
                        htinsert.Add("@County_Id", int.Parse(ddl_County.SelectedValue.ToString()));
                        htinsert.Add("@Research_Date", txt_ResearchDate.Text.Trim());
                        htinsert.Add("@Judgment_Link", txt_Judgmentlink.Text);
                        htinsert.Add("@Lien_Link", txt_Lienlink.Text);
                        htinsert.Add("@Criminal", txt_Criminallink.Text);
                        htinsert.Add("@Subscription", txt_Subscriptionlink.Text);
                        htinsert.Add("@Inserted_By", UserId);
                        htinsert.Add("@Instered_Date", DateTime.Now);
                        htinsert.Add("@Status", "True");
                        dtinsert = dataaccess.ExecuteSP("Sp_Judgment_Link", htinsert);
                        string title = "Insert";
                        MessageBox.Show("Judgment Link Inserted Successfully", title);
                        JudgmentClear();
                        //grp_JugmentReg.Visible = true;
                        //grp_JudgmentInfo.Visible = true;
                        //BindJugmentGrid();
                        btn_Add.Text = "Back";

                        label4.Visible = false;
                        label15.Visible = true;
                        label16.Visible = false;
                        JudgmentID = 0;
                        grp_JugmentReg.Visible = true;
                        grp_JudgmentInfo.Visible = true;
                        ddl_State.SelectedIndex = 0;
                        ddl_County.SelectedIndex = 0;
                        dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
                    }
                    //}
                    else if (JudgmentID != 0 && Edit_Duplicate_Record()!=false)
                    {
                        label4.Visible = false;
                        label15.Visible = false;
                        label16.Visible = true;

                        Grd_Judgment_Link.Rows.Clear();
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
                        htupdate.Add("@Trans", "UPDATE");
                        htupdate.Add("@Judgment_Links_Id", JudgmentID);
                        htupdate.Add("@State_Id", Stateid);
                        htupdate.Add("@County_Id", Countyid);
                        htupdate.Add("@Research_Date", txt_ResearchDate.Text.Trim());
                        htupdate.Add("@Judgment_Link", txt_Judgmentlink.Text);
                        htupdate.Add("@Lien_Link", txt_Lienlink.Text);
                        htupdate.Add("@Criminal", txt_Criminallink.Text);
                        htupdate.Add("@Subscription", txt_Subscriptionlink.Text);
                        htupdate.Add("@Inserted_By", UserId);
                        htupdate.Add("@Instered_Date", DateTime.Now);
                        htupdate.Add("@Status", "True");
                        dtupdate = dataaccess.ExecuteSP("Sp_Judgment_Link", htupdate);
                        string title = "Update";
                        MessageBox.Show("Judgment Link Updated Successfully", title);

                        JudgmentClear();
                        //grp_JugmentReg.Visible = true;
                        //grp_JudgmentInfo.Visible = false;
                        //BindJugmentGrid();
                        btn_Add.Text = "Back";
                        //btn_AddJugment.Text = "Add";

                        //label4.Visible = false;
                        //label15.Visible = true;
                        //label16.Visible = false;

                        label4.Visible = false;
                        label15.Visible = true;
                        label16.Visible = false;

                        grp_JudgmentInfo.Visible = true;
                        grp_JugmentReg.Visible = true;
                        btn_AddJugment.Text = "Add";

                        JudgmentID = 0;
                        ddl_State.SelectedIndex = 0;
                        ddl_County.SelectedIndex = 0;
                        dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
                    }
                    //ddl_State.SelectedIndex = 0;
                   // ddl_County.SelectedIndex = 0;
                    txt_Judgmentlink.Text = "";
               // }
            }
        }

        private bool Validation()
        {
            string title = "Validation!";
            if (ddl_State.SelectedIndex==0)
            {

                MessageBox.Show("Select State Name", title);
                ddl_State.Focus();
                return false;
            }
            else if (ddl_County.SelectedIndex==0)
            {

                MessageBox.Show("Select County Name", title);
                ddl_County.Focus();
                return false;
            }
            else if (txt_ResearchDate.Text == "")
            {
                MessageBox.Show("Select Research date", title);
                txt_ResearchDate.Focus();
                return false;
            }
            //else if (txt_Judgmentlink.Text == "")
            //{
            //    MessageBox.Show("Enter Judgement link", title);
            //    txt_Judgmentlink.Focus();
            //    return false;
            //}
            return true;
        }

        private void Grd_Judgment_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
               
                //View code
                if (e.ColumnIndex == 10)
                {
                    label4.Visible = false;
                    label15.Visible = false;
                    label16.Visible = true;
                    btn_Refresh.Visible = false;
                    btn_Import.Visible = false;
                    btn_Export.Visible = false;
                    grp_JudgmentInfo.Visible = true;
                    grp_JugmentReg.Visible = true;
                    Btn_Upload.Visible = false;
                    btn_GetImportExcel.Visible = false;

                    pictureBox3.Visible = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = false;
                    label18.Visible = false;
                    label19.Visible = false;
                    label20.Visible = false;

                    btn_Remove_Duplic.Visible = false;
                    btn_removedup.Visible = false;
                    btn_Remove_Error_row.Visible = false;


                    JudgmentID = int.Parse(Grd_Judgment_Link.Rows[e.RowIndex].Cells[12].Value.ToString());
                    Hashtable htselect = new Hashtable();
                    DataTable dtselect = new DataTable();
                    htselect.Add("@Trans", "SELECT");
                    htselect.Add("@Judgment_Links_Id", JudgmentID);
                    dtselect = dataaccess.ExecuteSP("Sp_Judgment_Link", htselect);
                    if (dtselect.Rows.Count > 0)
                    {
                        ddl_State.Text = dtselect.Rows[0]["State"].ToString();
                        statename = ddl_State.Text.ToString();

                        ddl_County.Text = dtselect.Rows[0]["County"].ToString();
                        countyname = ddl_County.Text.ToString();

                        txt_ResearchDate.Text = dtselect.Rows[0]["Research_Date"].ToString().Trim();
                        //ResearchDate = Convert.ToDateTime(txt_ResearchDate.Text);
                        txt_Criminallink.Text = dtselect.Rows[0]["Criminal"].ToString();
                        txt_Judgmentlink.Text = dtselect.Rows[0]["Judgment_Link"].ToString();
                        txt_Lienlink.Text = dtselect.Rows[0]["Lien_Link"].ToString();
                        txt_Subscriptionlink.Text = dtselect.Rows[0]["Subscription"].ToString();
                        lbl_Record_Addedby.Text = dtselect.Rows[0]["User_Name"].ToString();
                        lbl_Record_AddedDate.Text = dtselect.Rows[0]["Instered_Date"].ToString();
                    }
                    btn_AddJugment.Text = "Edit";
                    btn_Add.Text = "Back";
                }
                //Delete code
                else if (e.ColumnIndex == 11)
                {
                    JudgmentID = int.Parse(Grd_Judgment_Link.Rows[e.RowIndex].Cells[12].Value.ToString());
                       var op = MessageBox.Show("Do You Want to Delete Record", "Delete confirmation", MessageBoxButtons.YesNo);
                       if (op == DialogResult.Yes)
                       {
                           Hashtable htdelete = new Hashtable();
                           DataTable dtdelete = new DataTable();
                           htdelete.Add("@Trans", "DELETE");
                           htdelete.Add("@Judgment_Links_Id", JudgmentID);
                           dtdelete = dataaccess.ExecuteSP("Sp_Judgment_Link", htdelete);

                           string Title = "Successfull";
                           MessageBox.Show("Record Deleted Successfully",Title);
                         
                           JudgmentID = 0;
                           cbo_State.SelectedIndex = 0;
                           dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
                           BindJugmentGrid();
                       }
                       else
                       {
                           BindJugmentGrid();
                       }
                }
            }
        }

        private void ddl_County_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                Grd_Judgment_Link.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    Grd_Judgment_Link.Rows.Add();
                    Grd_Judgment_Link.Rows[i].Cells[0].Value = i + 1;
                    Grd_Judgment_Link.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[3].Value = temptable.Rows[i]["Research_Date"].ToString().Trim();
                    Grd_Judgment_Link.Rows[i].Cells[4].Value = temptable.Rows[i]["Judgment_Link"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[5].Value = temptable.Rows[i]["Lien_Link"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[6].Value = temptable.Rows[i]["Criminal"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[7].Value = temptable.Rows[i]["Subscription"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[8].Value = temptable.Rows[i]["State_ID"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[9].Value = temptable.Rows[i]["County_ID"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[12].Value = temptable.Rows[i]["Judgment_Links_Id"].ToString();

   
                    Grd_Judgment_Link.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Judgment_Link.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Judgment_Link.Rows[i].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Judgment_Link.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                    Grd_Judgment_Link.Rows[i].Cells[5].Style.WrapMode = DataGridViewTriState.True;

                }
            }
            else
            {
                Grd_Judgment_Link.Rows.Clear();
                Grd_Judgment_Link.Visible = true;
                Grd_Judgment_Link.DataSource = null;
            }

            lbl_count.Text = "Total Records: " + dtsort.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pageSize);
        }

        private void cbo_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_State.SelectedIndex > 0)
            {
                dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
                Grd_Judgment_Link.Rows.Clear();

                form_loader.Start_progres();
                //progBar.startProgress();
                Hashtable htsort = new Hashtable();
                
                htsort.Add("@Trans", "SORT_STATE");
                htsort.Add("@State_Id", int.Parse(cbo_State.SelectedValue.ToString()));
                dtsort = dataaccess.ExecuteSP("Sp_Judgment_Link", htsort);

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
                    Grd_Judgment_Link.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        Grd_Judgment_Link.Rows.Add();
                        Grd_Judgment_Link.Rows[i].Cells[0].Value = i + 1;
                        Grd_Judgment_Link.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[3].Value = temptable.Rows[i]["Research_Date"].ToString().Trim();
                        Grd_Judgment_Link.Rows[i].Cells[4].Value = temptable.Rows[i]["Judgment_Link"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[5].Value = temptable.Rows[i]["Lien_Link"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[6].Value = temptable.Rows[i]["Criminal"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[7].Value = temptable.Rows[i]["Subscription"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[8].Value = temptable.Rows[i]["State_ID"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[9].Value = temptable.Rows[i]["County_ID"].ToString();
                        Grd_Judgment_Link.Rows[i].Cells[12].Value = temptable.Rows[i]["Judgment_Links_Id"].ToString();

                        Grd_Judgment_Link.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_Judgment_Link.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                        Grd_Judgment_Link.Rows[i].Cells[5].Style.WrapMode = DataGridViewTriState.True;
                        Grd_Judgment_Link.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_Judgment_Link.Rows[i].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                      

                    }
                    lbl_count.Text = "Total Records: " + dtsort.Rows.Count.ToString();
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
                    Grd_Judgment_Link.Rows.Clear();
                    Grd_Judgment_Link.Visible = true;
                    Grd_Judgment_Link.DataSource = null;
                }

             
               // First_Page();
              
            }
            
        }

        private void GetDataRow_County(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtcounty.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
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
                Grd_Judgment_Link.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    Grd_Judgment_Link.Rows.Add();
                    Grd_Judgment_Link.Rows[i].Cells[0].Value = i + 1;
                    Grd_Judgment_Link.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[3].Value = temptable.Rows[i]["Research_Date"].ToString().Trim();
                    Grd_Judgment_Link.Rows[i].Cells[4].Value = temptable.Rows[i]["Judgment_Link"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[5].Value = temptable.Rows[i]["Lien_Link"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[6].Value = temptable.Rows[i]["Criminal"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[7].Value = temptable.Rows[i]["Subscription"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[8].Value = temptable.Rows[i]["State_ID"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[9].Value = temptable.Rows[i]["County_ID"].ToString();
                    Grd_Judgment_Link.Rows[i].Cells[12].Value = temptable.Rows[i]["Judgment_Links_Id"].ToString();

                   
                    Grd_Judgment_Link.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Judgment_Link.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Judgment_Link.Rows[i].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    Grd_Judgment_Link.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                    Grd_Judgment_Link.Rows[i].Cells[5].Style.WrapMode = DataGridViewTriState.True;
                }
            }
            else
            {
                Grd_Judgment_Link.Rows.Clear();
                Grd_Judgment_Link.Visible = true;
                Grd_Judgment_Link.DataSource = null;
            }

            lbl_count.Text = "Total Orders: " + dtcounty.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize);
          
        }

        private void cbo_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_State.SelectedIndex > 0)
            {
                if (cbo_County.SelectedIndex > 0)
                {
                    Grd_Judgment_Link.Rows.Clear();
                    form_loader.Start_progres();
                    //progBar.startProgress();
                    Hashtable ht = new Hashtable();
                   
                    ht.Add("@Trans", "SORT_COUNTY");
                    ht.Add("@State_Id", int.Parse(cbo_State.SelectedValue.ToString()));
                    ht.Add("@County_Id", int.Parse(cbo_County.SelectedValue.ToString()));
                    dtcounty = dataaccess.ExecuteSP("Sp_Judgment_Link", ht);

                    System.Data.DataTable temptable = dtcounty.Clone();
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
                        Grd_Judgment_Link.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            Grd_Judgment_Link.Rows.Add();
                            Grd_Judgment_Link.Rows[i].Cells[0].Value = i + 1;
                            Grd_Judgment_Link.Rows[i].Cells[1].Value = temptable.Rows[i]["State"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[2].Value = temptable.Rows[i]["County"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[3].Value = temptable.Rows[i]["Research_Date"].ToString().Trim();
                            Grd_Judgment_Link.Rows[i].Cells[4].Value = temptable.Rows[i]["Judgment_Link"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[5].Value = temptable.Rows[i]["Lien_Link"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[6].Value = temptable.Rows[i]["Criminal"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[7].Value = temptable.Rows[i]["Subscription"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[8].Value = temptable.Rows[i]["State_ID"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[9].Value = temptable.Rows[i]["County_ID"].ToString();
                            Grd_Judgment_Link.Rows[i].Cells[12].Value = temptable.Rows[i]["Judgment_Links_Id"].ToString();

                            Grd_Judgment_Link.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                            Grd_Judgment_Link.Rows[i].Cells[5].Style.WrapMode = DataGridViewTriState.True;
                            Grd_Judgment_Link.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            Grd_Judgment_Link.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            Grd_Judgment_Link.Rows[i].Cells[11].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                           


                        }
                        lbl_count.Text = "Total Orders: " + dtcounty.Rows.Count.ToString();
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
                        Grd_Judgment_Link.Rows.Clear();
                        Grd_Judgment_Link.Visible = true;
                        Grd_Judgment_Link.DataSource = null;
                        MessageBox.Show("Record Not Found");
                        lbl_count.Text = "Total Orders: " + dtcounty.Rows.Count.ToString();
                        lblRecordsStatus.Text = (currentPageIndex + 0) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pageSize);

                        btn_Refresh_Click(sender,e);
                    }

                   
                 
                  //  First_Page();
                    
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Grd_Judgment_Link.Rows.Clear();
          //  Grd_Judgment_Link.Columns.Clear();
           // Grd_Judgment_Link.DataSource = null;
           // Grd_Judgment_Link.DataSource = dtselect;

            Grd_Judgment_Link.Refresh();

            //Grd_Judgment_Link.Visible = true;

            cbo_State.SelectedIndex = 0;
            cbo_County.SelectedIndex = -1;
            //btn_Import.Visible = true;
            First_Page();
            BindJugmentGrid();
            dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
            JudgmentClear();
         //   btn_Cancel_Click(sender,e);

            //label4.Visible = true;
            //label15.Visible = false;
            //label16.Visible = false;
         
            //cbo_State_SelectedIndexChanged(sender,e);
           // First_Page();

            //Grd_Judgment_Link.DataSource = dtselect;
        }

        private void btn_GetImportExcel_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"c:\OMS_Import\Judgment_Import.xlsx";
            if (!(Directory.Exists(temppath)))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Judgment_Import.xlsx", temppath, true);
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
            if (cbo_State.SelectedIndex > 0)
            {
                Filter_State_Data();
            }
            else if (cbo_County.SelectedIndex > 0)
            {
                Filter_County_Data();
            }
            else
            {
                BindJugmentGrid();
            }
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
            if (cbo_State.SelectedIndex > 0)
            {
                Filter_State_Data();
            }
            else if (cbo_County.SelectedIndex > 0)
            {
                Filter_County_Data();
            }
            else
            {
                BindJugmentGrid();
            }

            this.Cursor = currentCursor;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentPageIndex++;
            if (cbo_State.SelectedIndex > 0)
            {
                if (currentPageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pageSize) - 1)
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
            else if (cbo_State.SelectedIndex > 0)
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
            
            }
            else
            {
                if (currentPageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize) - 1)
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

            
            }
            BindJugmentGrid();
            this.Cursor = currentCursor; 
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (cbo_State.SelectedIndex > 0)
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
                BindJugmentGrid();
            }
            
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            
            this.Cursor = currentCursor;
        
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Grid_Export_Data();
        }

        private void Grid_Export_Data()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in Grd_Judgment_Link.Columns)
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
            foreach (DataGridViewRow row in Grd_Judgment_Link.Rows)
            {
                
                dt.Rows.Add();
              
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count-1][cell.ColumnIndex - 1] = cell.Value.ToString();
                           
                        }
                    }
                }
            }
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Judgment_Link" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Judgment_Link");
                try
                {
                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it",title);
                }
            }
            System.Diagnostics.Process.Start(Path1);
         }

        private void txt_Lienlink_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Lienlink.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Judgmentlink_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Judgmentlink.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Criminallink_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Criminallink.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_ResearchDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_ResearchDate.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Subscriptionlink_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Subscriptionlink.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void btn_Remove_Duplic_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_Judgment_Link.Rows.Count - 1; i++)
            {

                if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {
                    Grd_Judgment_Link.Rows.RemoveAt(i);
                    i = i - 1;
                }
                lbl_count.Text = (Grd_Judgment_Link.Rows.Count-1).ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(Grd_Judgment_Link.Rows.Count) / pageSize);
            }
        }

        private void btn_removedup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_Judgment_Link.Rows.Count - 1; i++)
            {

                if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    Grd_Judgment_Link.Rows.RemoveAt(i);
                    i = i - 1;
                }
                lbl_count.Text = (Grd_Judgment_Link.Rows.Count - 1).ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(Grd_Judgment_Link.Rows.Count) / pageSize);
            }
        }

        private void btn_Remove_Error_row_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_Judgment_Link.Rows.Count - 1; i++)
            {
                if (Grd_Judgment_Link.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    Grd_Judgment_Link.Rows.RemoveAt(i);
                    i = i - 1;
                }
                lbl_count.Text = (Grd_Judgment_Link.Rows.Count - 1).ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(Grd_Judgment_Link.Rows.Count) / pageSize);
            }
        }

       
        
       

      

    

     
        
    }
}
