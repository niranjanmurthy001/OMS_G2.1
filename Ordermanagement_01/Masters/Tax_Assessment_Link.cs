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

namespace Ordermanagement_01.Masters
{
    public partial class Tax_Assessment_Link : Form
    {
        System.Data.DataTable dtnonadded = new System.Data.DataTable();
        System.Data.DataTable dtnonadded1 = new System.Data.DataTable();
        DataRow dr;
        DataRow dr1;

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        
        System.Data.DataTable dtselect = new System.Data.DataTable();
        System.Data.DataTable dtcounty = new System.Data.DataTable();
        System.Data.DataTable dtsort = new System.Data.DataTable();
        InfiniteProgressBar.clsProgress progBar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();

        int User_ID, CountyTaxId, count, count_non = 0, value;
        static int CurrentpageIndex = 0;
        private int pagesize = 50;
        string username;
       //string state ;
       //string county;
       string duplicate;
       string State_Abr, StateAbr;
       int state_ID, county_ID;
       string statename, countyname;
       int userid = 0, state_id, stateid, countyid, insert = 0, state_insert, county_Id, State_id;
     //  int state, county;
       int cntr = 0; //used for custom sort toggle
        public Tax_Assessment_Link(int User_id,string UserName)
        {
            InitializeComponent();
            User_ID = User_id;
            username = UserName;
            dbc.BindState(ddl_State);
            dbc.BindState(cbo_State);
            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
        }

        private void Btn_Upload_Click(object sender, EventArgs e)
        {

            grp_TaxAssessInfo.Visible = true;
            grp_TaxAssessReg.Visible = false;

            TaxAssessment_view.Visible = false;
            TaxAssessment_delete.Visible = false;

            Grd_CountyTaxLink.Rows.Clear();

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
                ////  grd_order.DataSource = data;
                //Hashtable httruncate = new Hashtable();
                //DataTable dttruncate = new System.Data.DataTable();
                //httruncate.Add("@Trans", "TRUNCATE");
                //dttruncate = dataaccess.ExecuteSP("Sp_Temp_Order", httruncate);


                //dtnonadded.Clear();
                //dtnonadded.Columns.Clear();
                //dtnonadded.Columns.Add("State", typeof(string));
                //dtnonadded.Columns.Add("County", typeof(string));

                Grd_CountyTaxLink.Rows.Clear();
                if (data.Rows.Count > 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        string State = data.Rows[i]["State"].ToString();
                        string county = data.Rows[i]["County"].ToString();

                        Grd_CountyTaxLink.Rows.Add();
                        Grd_CountyTaxLink.Rows[i].Cells[0].Value = i + 1;
                        Grd_CountyTaxLink.Rows[i].Cells[1].Value = data.Rows[i]["State"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[2].Value = data.Rows[i]["County"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[3].Value = data.Rows[i]["Tax_PhoneNo"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[4].Value = data.Rows[i]["Assessor_PhoneNo"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[5].Value = data.Rows[i]["CountyTax_Link"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[6].Value = data.Rows[i]["Assessor_Link"].ToString();

                        Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                        Grd_CountyTaxLink.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_CountyTaxLink.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_CountyTaxLink.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                        //Check State Exist  -- State is unique
                        //Hashtable htorder = new Hashtable();
                        //DataTable dtorder = new DataTable();
                        //htorder.Add("@Trans", "SEARCH_STATE");
                        //htorder.Add("@State", data.Rows[i]["State"].ToString());

                        //dtorder = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htorder);
                        //if (dtorder.Rows.Count > 0)
                        //{
                        //    stateid = int.Parse(dtorder.Rows[0]["State_ID"].ToString());
                        //    State_Abr = dtorder.Rows[0]["Abbreviation"].ToString();

                        //    Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                        //    dr = dtnonadded.NewRow();

                        //    dr["State"] = Grd_CountyTaxLink.Rows[i].Cells[1].Value.ToString();
                        //    dr["County"] = Grd_CountyTaxLink.Rows[i].Cells[2].Value.ToString();

                        //    dtnonadded.Rows.Add(dr);
                        //}

                        ////Check County Exist  -- County is unique
                        Hashtable ht_order = new Hashtable();
                        DataTable dt_order = new DataTable();
                        ht_order.Add("@Trans", "SEARCH_COUNTY");
                        //ht_order.Add("@Abbreviation", data.Rows[i]["State"].ToString());

                        ht_order.Add("@StateName", data.Rows[i]["State"].ToString());
                        ht_order.Add("@county_Name", data.Rows[i]["County"].ToString());

                        dt_order = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", ht_order);
                        if (dt_order.Rows.Count > 0)
                        {
                            State_id = int.Parse(dt_order.Rows[0]["State_ID"].ToString());
                            county_Id = int.Parse(dt_order.Rows[0]["County_ID"].ToString());
                            StateAbr = dt_order.Rows[0]["Abbreviation"].ToString();

                            Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                            //dr = dtnonadded.NewRow();

                            //dr["State"] = Grd_CountyTaxLink.Rows[i].Cells[1].Value.ToString();
                            //dr["County"] = Grd_CountyTaxLink.Rows[i].Cells[2].Value.ToString();

                            //  dtnonadded.Rows.Add(dr);
                        }

                        //error in County or state

                        Hashtable htord = new Hashtable();
                        DataTable dtord = new DataTable();
                        htord.Add("@Trans", "COUNTY_SEARCH");
                        // htord.Add("@Abbreviation", State_Abr);
                        htord.Add("@StateName", State);
                        htord.Add("@county_Name", county);

                        dtord = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htord);
                        if (dtord.Rows.Count != 0)
                        {
                            county_Id = int.Parse(dtord.Rows[0]["County_ID"].ToString());
                        }
                        else
                        {
                            //abbrid = 0;
                            Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                        }

                        //Duplicate of records
                        for (int j = 0; j < i; j++)
                        {

                            string state = data.Rows[j]["State"].ToString();
                            string countyname = data.Rows[j]["County"].ToString();

                            if (state == State_Abr && countyname == county)
                            {

                                Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                            }
                            else
                            {
                                value = 0;
                            }

                        }

                        if (State == "" || county == "")
                        {
                            Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }


                    }
                    btn_Import.Visible = true;
                    TaxAssessment_view.Visible = false;
                    TaxAssessment_delete.Visible = false;
                    lbl_count.Text = data.Rows.Count.ToString();
                    lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(data.Rows.Count) / pagesize);
                }
                //btn_Import.Visible = true;
                //TaxAssessment_view.Visible = false;
                //TaxAssessment_delete.Visible = false;
                //lbl_count.Text = data.Rows.Count.ToString();
                //lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(data.Rows.Count) / pagesize);

               }
                else
                {
                    string title = "Empty!";
                    MessageBox.Show("Check the Excel is empty", title);
                }

          //  }
         }
        private bool validate()
        {
            int error = 0, dupl = 0, exist = 0, empty = 0;
            for (int i = 0; i <= Grd_CountyTaxLink.Rows.Count - 1; i++)
            {
                if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                {

                    return true;
                }
                else
                {
                    if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
                    {
                        error = error + 1;
                        //return false;
                    }

                    else if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Cyan)
                    {
                        dupl = dupl + 1;
                        // return false;
                    }
                    else if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Yellow)
                    {
                        exist = exist + 1;

                    }
                    else if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.SystemColors.Control)
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
                //MessageBox.Show(" ' " + empty + " ' " + "  No of Row is Empty and No empty row can be import", title);
                return false;
            }


            return true;

        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            int error = 0, dupl = 0, exist = 0;
            form_loader.Start_progres();
            //progBar.startProgress();
            count = 0;
            int count_grd = Grd_CountyTaxLink.Rows.Count;
            int count_ex=0;
            if (validate() != false)
            {
                for (int i = 0; i < Grd_CountyTaxLink.Rows.Count - 1; i++)
                {
                    if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                    {
                        int Stateid;
                        int Countyid;

                        Hashtable htbarowerstate = new Hashtable();
                        DataTable dtbarrowerstate = new System.Data.DataTable();
                        // htbarowerstate.Add("@Trans", "GETSTATE_BY_ABR");
                        htbarowerstate.Add("@Trans", "GETSTATE_BY_STATENAME");
                        htbarowerstate.Add("@state_name", Grd_CountyTaxLink.Rows[i].Cells[1].Value.ToString());
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
                        htBarcounty.Add("@County_Name", Grd_CountyTaxLink.Rows[i].Cells[2].Value.ToString());
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
                            dt = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", ht);
                            if (dt.Rows.Count == 0)
                            {
                                //if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.White)
                                //{
                                Hashtable ht_INSERT = new Hashtable();
                                DataTable dt_INSERT = new System.Data.DataTable();
                                ht_INSERT.Add("@Trans", "INSERT");
                                ht_INSERT.Add("@State", Stateid);
                                ht_INSERT.Add("@County", Countyid);
                                ht_INSERT.Add("@Tax_PhoneNo", Grd_CountyTaxLink.Rows[i].Cells[3].Value.ToString());
                                ht_INSERT.Add("@Assessor_PhoneNo", Grd_CountyTaxLink.Rows[i].Cells[4].Value.ToString());
                                ht_INSERT.Add("@CountyTax_Link", Grd_CountyTaxLink.Rows[i].Cells[5].Value.ToString());
                                ht_INSERT.Add("@Assessor_Link", Grd_CountyTaxLink.Rows[i].Cells[6].Value.ToString());
                                ht_INSERT.Add("@Inserted_By", User_ID);
                                ht_INSERT.Add("@Status", "True");
                                dt_INSERT = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", ht_INSERT);
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
                    //    if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Red)
                    //    {
                    //        error = error + 1;
                    //    }

                    //    else if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Cyan)
                    //    {
                    //        dupl = dupl + 1;
                    //    }
                    //    else if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == System.Drawing.Color.Yellow)
                    //    {
                    //        exist = exist + 1;
                    //    }
                    //}
                }

                form_loader.Start_progres();
                //progBar.startProgress();
                if (count > 0)
                {
                    string title = "Successfull";
                    MessageBox.Show(count + " No of Tax Assessment Link Records Imported successfully", title);
                    count = 0;
                    BindTaxAssessmentGrid();
                    btn_Import.Visible = true;
                    TaxAssessment_view.Visible = true;
                    TaxAssessment_delete.Visible = true;
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
            

            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (btn_Add.Text == "Add New")
            {
                TaxAssessmentClear();
                grp_TaxAssessReg.Visible = true;
                grp_TaxAssessInfo.Visible = true;
              
               
                btn_Import.Visible = false;
                btn_Export.Visible = false;

                pictureBox3.Visible = false;
                label20.Visible = false;
                pictureBox1.Visible = false;
                label19.Visible = false;
                pictureBox2.Visible = false;
                label16.Visible = false;


                  btn_Remove_Duplic.Visible = false;
                  btn_removedup.Visible = false;
                 btn_Remove_Error_row.Visible = false;



              
                btn_Add.Text = "Back";
                btn_AddTaxCounty.Text = "Add";
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
                Btn_Upload.Visible = false;
                btn_GetImportExcel.Visible = false;
                lbl_Record_Addedby.Text = username;
                lbl_Record_AddedDate.Text =DateTime.Now.ToString();

                label4.Visible = false;
                label13.Visible = true;
                label15.Visible = false;
                btn_Refresh.Visible = false;
                ddl_State.Focus();
                BindTaxAssessmentGrid();
               // First_Page();
               
            }
            else
            {
                grp_TaxAssessInfo.Visible = true;
                grp_TaxAssessReg.Visible = false;
                //grp_TaxAssessInfo.Visible = true;
               
                TaxAssessmentClear();
                btn_Add.Text = "Add New";
                dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
                Btn_Upload.Visible = true;
                btn_GetImportExcel.Visible = true;
                btn_Import.Visible = true;
                btn_Export.Visible = true;

                label4.Visible = true;
                label13.Visible = false;
                label15.Visible = false;
                btn_Refresh.Visible = true;
                btn_Refresh_Click(sender,e);
                //First_Page();
              //  cbo_State_SelectedIndexChanged(sender, e);
                BindTaxAssessmentGrid();

                pictureBox3.Visible = true;
                label20.Visible = true;
                pictureBox1.Visible = true;
                label19.Visible = true;
                pictureBox2.Visible = true;
                label16.Visible = true;


                btn_Remove_Duplic.Visible = true;
                btn_removedup.Visible = true;
                btn_Remove_Error_row.Visible = true;
            }
        }

        private bool Validation()
        {
            string title = "Validation!";
            if (ddl_State.SelectedIndex==0)
            {
                
                MessageBox.Show("Select State Name",title);
                return false;
            }
            else if (ddl_County.SelectedIndex==0)
            {
               
                MessageBox.Show("Select County Name", title);
                return false;
            }
            else if (txt_Tax_PhoneNo.Text=="")
            {
              
                MessageBox.Show("Select Tax Phone Number", title);
                return false;
            }

         

            return true;
        }

        private bool Duplicate_Record()
        {
            string state, county;
            Hashtable ht_checkDuplicate = new Hashtable();
            DataTable dt_checkDuplicate = new DataTable();

            ht_checkDuplicate.Add("@Trans", "SELECT_BY_STATE_COUNTY");
            ht_checkDuplicate.Add("@State", ddl_State.SelectedValue.ToString());
            ht_checkDuplicate.Add("@County", ddl_County.SelectedValue.ToString());
            dt_checkDuplicate = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", ht_checkDuplicate);
          
            for (int i = 0; i <= dt_checkDuplicate.Rows.Count; i++)
            {
                state = dt_checkDuplicate.Rows[0]["State_ID"].ToString();
                county = dt_checkDuplicate.Rows[0]["County_ID"].ToString();

                string selected_state = ddl_State.SelectedValue.ToString();
                string selected_County = ddl_County.SelectedValue.ToString();

                if (state == selected_state && county == selected_County && btn_AddTaxCounty.Text != "Edit")
                {
                    duplicate = "Duplicate Data";
                    string title = "Duplicate Record!";
                    MessageBox.Show("Record Already Existed", title);
                 
                    return false;
                }
            }

            return true;
        }

        private bool Edit_Duplicate_Record()
        {
                //int stateid, countyid;
                Hashtable ht_checkDuplicate = new Hashtable();
                DataTable dt_checkDuplicate = new DataTable();
            
                ht_checkDuplicate.Add("@Trans", "EDIT_CHECK_DUPLICATE");
                ht_checkDuplicate.Add("@StateName", statename);
                ht_checkDuplicate.Add("@county_Name", countyname);
                dt_checkDuplicate = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", ht_checkDuplicate);
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
                dt_check = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", ht_check);
                if (dt_check.Rows.Count>0)
                {
                    state_ID = int.Parse(dt_check.Rows[0]["State_ID"].ToString());
                    county_ID = int.Parse(dt_check.Rows[0]["County_ID"].ToString());
                }
                if (state_ID == stateid && county_ID == countyid && btn_AddTaxCounty.Text == "Edit")
                {
                    return true;
                }
                else
                {
                    if (state_ID != stateid && btn_AddTaxCounty.Text == "Edit")
                    {
                        duplicate = "Duplicate Data";
                        string title = "Duplicate Record!";
                        MessageBox.Show("Record Already Existed", title);
                        TaxAssessmentClear();
                        return false;
                    }
                    else if (county_ID != countyid && btn_AddTaxCounty.Text == "Edit")
                    {
                        duplicate = "Duplicate Data";
                        string title = "Duplicate Record!";
                        MessageBox.Show("Record Already Existed", title);
                        TaxAssessmentClear();
                        return false;
                    }
                }
               return true;
        }


        private void btn_AddTaxCounty_Click(object sender, EventArgs e)
        {
            if (Validation() != false )
            {
                if (CountyTaxId == 0 && btn_AddTaxCounty.Text == "Add" && Duplicate_Record()!=false)
                {
                    
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new DataTable();
                    htinsert.Add("@Trans", "INSERT");
                    htinsert.Add("@State", int.Parse(ddl_State.SelectedValue.ToString()));
                    htinsert.Add("@County", int.Parse(ddl_County.SelectedValue.ToString()));
                    htinsert.Add("@Tax_PhoneNo", txt_Tax_PhoneNo.Text);
                    htinsert.Add("@Assessor_PhoneNo", txt_Assessor_PhoneNo.Text);
                    htinsert.Add("@CountyTax_Link", txt_CountyTax_Link.Text);
                    htinsert.Add("@Assessor_Link", txt_Assessor_Link.Text);
                    htinsert.Add("@Inserted_By", User_ID);
                    htinsert.Add("@Inserted_date", DateTime.Now);
                    htinsert.Add("@Status", "True");
                    dtinsert = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htinsert);

                    string title = "Insert Window";
                    MessageBox.Show("Tax Assessment Link Inserted Successfully",title);
                    //Btn_Upload.Visible = false;
                    //btn_GetImportExcel.Visible = false;
                    BindTaxAssessmentGrid();
                    CountyTaxId = 0;
                    //cbo_State.SelectedIndex = 0;
                    //cbo_County.SelectedIndex = 0;
                    TaxAssessmentClear();

                    label4.Visible = false;
                    label13.Visible = true;
                    label15.Visible = false;
                   
                }
                else if (CountyTaxId != 0 && Edit_Duplicate_Record()!=false)
                {
                  
                    Grd_CountyTaxLink.Rows.Clear();
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

                   // CountyTaxId = int.Parse(Grd_CountyTaxLink.Rows[e.RowIndex].Cells[11].Value.ToString());

                   // CountyTaxId =int.Parse(ddl_County.SelectedValue.ToString());

                    Hashtable htupdate = new Hashtable();
                    DataTable dtupdate = new DataTable();
                    htupdate.Add("@Trans", "UPDATE");
                    htupdate.Add("@County_Assement_Link_Id", CountyTaxId);
                    htupdate.Add("@State", Stateid);
                    htupdate.Add("@County", Countyid);

                    htupdate.Add("@Tax_PhoneNo", txt_Tax_PhoneNo.Text);
                    htupdate.Add("@Assessor_PhoneNo", txt_Assessor_PhoneNo.Text);
                    htupdate.Add("@CountyTax_Link", txt_CountyTax_Link.Text);
                    htupdate.Add("@Assessor_Link", txt_Assessor_Link.Text);
                    htupdate.Add("@Inserted_By", User_ID);
                    htupdate.Add("@Inserted_date", DateTime.Now);
                    htupdate.Add("@Status", "True");
                    dtupdate = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htupdate);

                    string title = "Updated";
                    MessageBox.Show("Tax Assessment Link Updated Successfully",title);
                    BindTaxAssessmentGrid();
                    CountyTaxId = 0;
                    TaxAssessmentClear();
                    label4.Visible = true;
                    label13.Visible = false;
                    label15.Visible = false;
                }
               // TaxAssessmentClear();


                //grp_TaxAssessReg.Visible = true;
                //grp_TaxAssessInfo.Visible = false;
                //BindTaxAssessmentGrid();
                btn_Add.Text = "Back";

                cbo_State.SelectedIndex = 0;
                cbo_County.SelectedIndex = 0;

                btn_AddTaxCounty.Text = "Add";

                //label4.Visible = false;
                //label13.Visible = true;
                //label15.Visible = false;

                ddl_State.SelectedIndex = 0;
                ddl_County.SelectedIndex = 0;
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            TaxAssessmentClear();
            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
        }

        private void TaxAssessmentClear()
        {

            ddl_State.SelectedIndex = 0;
            ddl_County.SelectedIndex = -1;
         
            txt_CountyTax_Link.Text = "";
            txt_Tax_PhoneNo.Text = "";
            txt_Assessor_PhoneNo.Text = "";
            txt_Assessor_Link.Text = "";
            //lbl_Record_Addedby.Text = "";
            //lbl_Record_AddedDate.Text = "";

            btn_AddTaxCounty.Text = "Add";

            label15.Visible = false;
            label4.Visible = false;
            label13.Visible = true;
            CountyTaxId = 0;

            ddl_State.Focus();
        }

        private void Grd_CountyTaxLink_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                
                if (e.ColumnIndex == 9)
                {
                    //label4.Visible = true;
                    //label13.Visible = false;
                    //label15.Visible = false;

                    ddl_State.Focus();
                    label15.Visible = true;
                    label4.Visible = false;
                    label13.Visible = false;

                    btn_Export.Visible = false;
                    btn_Import.Visible = false;
                    btn_Refresh.Visible = false;

                    grp_TaxAssessInfo.Visible = true;
                    grp_TaxAssessReg.Visible = true;
                    Btn_Upload.Visible = false;
                    btn_GetImportExcel.Visible = false;

                    pictureBox3.Visible = false;
                    label20.Visible = false;
                    pictureBox1.Visible = false;
                    label19.Visible = false;
                    pictureBox2.Visible = false;
                    label16.Visible = false;

                    btn_Remove_Duplic.Visible = false;
                    btn_removedup.Visible = false;
                    btn_Remove_Error_row.Visible = false;



                    //View code
                    CountyTaxId = int.Parse(Grd_CountyTaxLink.Rows[e.RowIndex].Cells[11].Value.ToString());
                    Hashtable htselect = new Hashtable();
                    DataTable dtselect = new DataTable();
                    htselect.Add("@Trans", "SELECT");
                    htselect.Add("@County_Assement_Link_Id", CountyTaxId);
                    dtselect = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htselect);
                    if (dtselect.Rows.Count > 0)
                    {
                        ddl_State.Text = dtselect.Rows[0]["State"].ToString();

                        statename = ddl_State.Text.ToString();
                        ddl_County.Text = dtselect.Rows[0]["County"].ToString();
                        countyname = ddl_County.Text.ToString();

                        txt_Tax_PhoneNo.Text = dtselect.Rows[0]["Tax_PhoneNo"].ToString();
                        txt_Assessor_PhoneNo.Text = dtselect.Rows[0]["Assessor_PhoneNo"].ToString();
                        txt_CountyTax_Link.Text = dtselect.Rows[0]["CountyTax_Link"].ToString();
                        txt_Assessor_Link.Text = dtselect.Rows[0]["Assessor_Link"].ToString();
                        lbl_Record_Addedby.Text = dtselect.Rows[0]["User_Name"].ToString();
                        lbl_Record_AddedDate.Text = dtselect.Rows[0]["Inserted_date"].ToString();
                    }
                    btn_AddTaxCounty.Text = "Edit";
                    btn_Add.Text = "Back";
                    
                }
                //Delete code
                else if (e.ColumnIndex == 10)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        //if (CountyTaxId!=0)
                        //{
                        CountyTaxId = int.Parse(Grd_CountyTaxLink.Rows[e.RowIndex].Cells[11].Value.ToString());
                        Hashtable htdelete = new Hashtable();
                        DataTable dtdelete = new DataTable();
                        htdelete.Add("@Trans", "DELETE");
                        htdelete.Add("@County_Assement_Link_Id", CountyTaxId);
                        dtdelete = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htdelete);

                        // string message = "Close Window";
                        string title = "Successfull";
                        MessageBox.Show("Record Deleted Successfully",title);
                        BindTaxAssessmentGrid();
                        CountyTaxId = 0;
                        //}
                        //else
                        //{
                        //    string title = "Select!";
                        //    MessageBox.Show("Please Select the Record",title);
                        //}
                    }
                    else
                    {
                        BindTaxAssessmentGrid();
                    }

                }
            }
        }

        private void Filter_State_Data()
        {
            System.Data.DataTable tempTable = dtsort.Clone();
            int startindex = CurrentpageIndex * pagesize;
            int endindex = CurrentpageIndex * pagesize + pagesize;
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
                Grd_CountyTaxLink.Rows.Clear();
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    Grd_CountyTaxLink.Rows.Add();
                    Grd_CountyTaxLink.Rows[i].Cells[0].Value = i + 1;
                    Grd_CountyTaxLink.Rows[i].Cells[1].Value = tempTable.Rows[i]["State"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[2].Value = tempTable.Rows[i]["County"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[3].Value = tempTable.Rows[i]["Tax_PhoneNo"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[4].Value = tempTable.Rows[i]["Assessor_PhoneNo"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[5].Value = tempTable.Rows[i]["CountyTax_Link"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[6].Value = tempTable.Rows[i]["Assessor_Link"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[7].Value = tempTable.Rows[i]["State_ID"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[8].Value = tempTable.Rows[i]["County_ID"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[11].Value = tempTable.Rows[i]["County_Assement_Link_Id"].ToString();

                    Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    Grd_CountyTaxLink.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_CountyTaxLink.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_CountyTaxLink.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            else
            {
                Grd_CountyTaxLink.Rows.Clear();
                Grd_CountyTaxLink.Visible = true;
                Grd_CountyTaxLink.DataSource = null;
            }
            lbl_count.Text = "Total Orders: " + dtsort.Rows.Count.ToString();
            lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize);
        }


        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            CurrentpageIndex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            this.Cursor = currentCursor;
        }

        private void cbo_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_State.SelectedIndex > 0)
            {
                dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
                Grd_CountyTaxLink.Rows.Clear();
                form_loader.Start_progres();
                //progBar.startProgress();
                Hashtable htsort = new Hashtable();
               
                htsort.Add("@Trans", "SELECT_BY_STATE");
                htsort.Add("@State", int.Parse(cbo_State.SelectedValue.ToString()));
                //Bind_Grid_Tax_PageIndex();

                dtsort = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htsort);

                System.Data.DataTable tempTable = dtsort.Clone();
                int startindex = CurrentpageIndex * pagesize;
                int endindex = CurrentpageIndex * pagesize + pagesize;
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
                    Grd_CountyTaxLink.Rows.Clear();
                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        Grd_CountyTaxLink.Rows.Add();
                        Grd_CountyTaxLink.Rows[i].Cells[0].Value = i + 1;
                        Grd_CountyTaxLink.Rows[i].Cells[1].Value = tempTable.Rows[i]["State"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[2].Value = tempTable.Rows[i]["County"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[3].Value = tempTable.Rows[i]["Tax_PhoneNo"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[4].Value = tempTable.Rows[i]["Assessor_PhoneNo"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[5].Value = tempTable.Rows[i]["CountyTax_Link"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[6].Value = tempTable.Rows[i]["Assessor_Link"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[7].Value = tempTable.Rows[i]["State_ID"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[8].Value = tempTable.Rows[i]["County_ID"].ToString();
                        Grd_CountyTaxLink.Rows[i].Cells[11].Value = tempTable.Rows[i]["County_Assement_Link_Id"].ToString();
                    }
                    lbl_count.Text = "Total Records: " + dtsort.Rows.Count.ToString();
                    lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize);
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
                    Grd_CountyTaxLink.Rows.Clear();
                    Grd_CountyTaxLink.Visible = true;
                    Grd_CountyTaxLink.DataSource = null;
                    string empty = "Empty!";
                    MessageBox.Show("Record not Found",empty);
                     btn_Refresh_Click( sender,  e);
                }
                //lbl_count.Text = "Total Orders: " + dtsort.Rows.Count.ToString();
                //lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize);
                //First_Page();
               
            }
          
        }

        private void Filter_County_Data()
        {
            System.Data.DataTable tempTable = dtcounty.Clone();
            int startindex = CurrentpageIndex * pagesize;
            int endindex = CurrentpageIndex * pagesize + pagesize;
            if (endindex > dtcounty.Rows.Count)
            {
                endindex = dtcounty.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow newrow = tempTable.NewRow();
                GetNewRow_County(ref newrow, dtcounty.Rows[i]);
                tempTable.Rows.Add(newrow);
            }

            if (tempTable.Rows.Count > 0)
            {
                Grd_CountyTaxLink.Rows.Clear();
                for (int i = 0; i < tempTable.Rows.Count; i++)
                {
                    Grd_CountyTaxLink.Rows.Add();
                    Grd_CountyTaxLink.Rows[i].Cells[0].Value = i + 1;
                    Grd_CountyTaxLink.Rows[i].Cells[1].Value = tempTable.Rows[i]["State"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[2].Value = tempTable.Rows[i]["County"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[3].Value = tempTable.Rows[i]["Tax_PhoneNo"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[4].Value = tempTable.Rows[i]["Assessor_PhoneNo"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[5].Value = tempTable.Rows[i]["CountyTax_Link"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[6].Value = tempTable.Rows[i]["Assessor_Link"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[7].Value = tempTable.Rows[i]["State_ID"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[8].Value = tempTable.Rows[i]["County_ID"].ToString();
                    Grd_CountyTaxLink.Rows[i].Cells[11].Value = tempTable.Rows[i]["County_Assement_Link_Id"].ToString();

                    Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    Grd_CountyTaxLink.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_CountyTaxLink.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_CountyTaxLink.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            else
            {
                Grd_CountyTaxLink.Rows.Clear();
                Grd_CountyTaxLink.Visible = true;
                Grd_CountyTaxLink.DataSource = null;
            }
            lbl_count.Text = "Total Orders: " + dtcounty.Rows.Count.ToString();
            lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
        }

        private void cbo_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_State.SelectedIndex > 0)
            {
                if (cbo_County.SelectedIndex > 0)
                {
                    Grd_CountyTaxLink.Rows.Clear();
                    form_loader.Start_progres();
                    //progBar.startProgress();
                    Hashtable ht = new Hashtable();
                    
                    ht.Add("@Trans", "SELECT_BY_STATE_COUNTY");
                    ht.Add("@State", int.Parse(cbo_State.SelectedValue.ToString()));
                    ht.Add("@County", int.Parse(cbo_County.SelectedValue.ToString()));

                    dtcounty = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", ht);

                    System.Data.DataTable tempTable = dtcounty.Clone();
                    int startindex = CurrentpageIndex * pagesize;
                    int endindex = CurrentpageIndex * pagesize + pagesize;
                    if (endindex > dtcounty.Rows.Count)
                    {
                        endindex = dtcounty.Rows.Count;
                    }
                    for (int i = startindex; i < endindex; i++)
                    {
                        DataRow newrow = tempTable.NewRow();
                        GetNewRow_County(ref newrow, dtcounty.Rows[i]);
                        tempTable.Rows.Add(newrow);
                    }

                    if (tempTable.Rows.Count > 0)
                    {
                        Grd_CountyTaxLink.Rows.Clear();
                        for (int i = 0; i < tempTable.Rows.Count; i++)
                        {
                            Grd_CountyTaxLink.Rows.Add();
                            Grd_CountyTaxLink.Rows[i].Cells[0].Value = i + 1;
                            Grd_CountyTaxLink.Rows[i].Cells[1].Value = tempTable.Rows[i]["State"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[2].Value = tempTable.Rows[i]["County"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[3].Value = tempTable.Rows[i]["Tax_PhoneNo"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[4].Value = tempTable.Rows[i]["Assessor_PhoneNo"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[5].Value = tempTable.Rows[i]["CountyTax_Link"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[6].Value = tempTable.Rows[i]["Assessor_Link"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[7].Value = tempTable.Rows[i]["State_ID"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[8].Value = tempTable.Rows[i]["County_ID"].ToString();
                            Grd_CountyTaxLink.Rows[i].Cells[11].Value = tempTable.Rows[i]["County_Assement_Link_Id"].ToString();
                        }
                        lbl_count.Text = "Total Records: " + dtcounty.Rows.Count.ToString();
                        lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);

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
                        Grd_CountyTaxLink.Rows.Clear();
                        Grd_CountyTaxLink.Visible = true;
                        Grd_CountyTaxLink.DataSource = null;

                        string Empty = "Empty!";
                        MessageBox.Show("Record Not Found", Empty);

                        lbl_count.Text = "Total Orders: " + dtcounty.Rows.Count.ToString();
                        lblRecordsStatus.Text = (CurrentpageIndex + 0) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
                        btn_Refresh_Click(sender, e);
                    }
                    //lbl_count.Text = "Total Orders: " + dtcounty.Rows.Count.ToString();
                    //lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
                    
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
          // First_Page();
        }

        private void GetNewRow(ref DataRow newrow, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                newrow[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void GetNewRow_State(ref DataRow newrow, DataRow source)
        {
            foreach (DataColumn col in dtsort.Columns)
            {
                newrow[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void GetNewRow_County(ref DataRow newrow, DataRow source)
        {
            foreach (DataColumn col in dtcounty.Columns)
            {
                newrow[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            cbo_State.SelectedIndex = 0;
            cbo_County.SelectedIndex = -1;
            BindTaxAssessmentGrid();
            First_Page();

            dbc.BindCounty(cbo_County, int.Parse(cbo_State.SelectedValue.ToString()));
            
        }
     
        private void BindTaxAssessmentGrid()
        {
            form_loader.Start_progres();

            //progBar.startProgress();

            TaxAssessment_view.Visible = true;
            TaxAssessment_delete.Visible = true;

            Grd_CountyTaxLink.Rows.Clear();

            Hashtable htselect = new Hashtable();
            

            if (cbo_State.SelectedIndex > 0)
            {
                htselect.Add("@Trans", "SELECT_BY_STATE");
                htselect.Add("@State", int.Parse(cbo_State.SelectedValue.ToString()));
                if (cbo_County.SelectedIndex > 0)
                {
                    htselect.Clear();
                    htselect.Add("@Trans", "SELECT_BY_STATE_COUNTY");

                    htselect.Add("@State", int.Parse(cbo_State.SelectedValue.ToString()));
                    htselect.Add("@County", int.Parse(cbo_County.SelectedValue.ToString()));
                }
            }
            else
            {
                htselect.Add("@Trans", "SELECT_ALL");
                
            }
           dtselect = dataaccess.ExecuteSP("Sp_County_Tax_Assesment_Link", htselect);

           System.Data.DataTable tempTable = dtselect.Clone();
           int startindex = CurrentpageIndex * pagesize;
           int endindex = CurrentpageIndex * pagesize + pagesize;
           if (endindex > dtselect.Rows.Count)
           {
               endindex = dtselect.Rows.Count;
           }
           for (int i = startindex; i < endindex; i++)
           {
               DataRow newrow = tempTable.NewRow();
               GetNewRow(ref newrow, dtselect.Rows[i]);
               tempTable.Rows.Add(newrow);
           }

           if (tempTable.Rows.Count > 0)
           {
               Grd_CountyTaxLink.Rows.Clear();
               for (int i = 0; i < tempTable.Rows.Count; i++)
               {
                   Grd_CountyTaxLink.Rows.Add();
                   Grd_CountyTaxLink.Rows[i].Cells[0].Value = i + 1;
                   Grd_CountyTaxLink.Rows[i].Cells[1].Value = tempTable.Rows[i]["State"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[2].Value = tempTable.Rows[i]["County"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[3].Value = tempTable.Rows[i]["Tax_PhoneNo"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[4].Value = tempTable.Rows[i]["Assessor_PhoneNo"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[5].Value = tempTable.Rows[i]["CountyTax_Link"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[6].Value = tempTable.Rows[i]["Assessor_Link"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[7].Value = tempTable.Rows[i]["State_ID"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[8].Value = tempTable.Rows[i]["County_ID"].ToString();
                   Grd_CountyTaxLink.Rows[i].Cells[11].Value = tempTable.Rows[i]["County_Assement_Link_Id"].ToString();

                   Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                   Grd_CountyTaxLink.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                   Grd_CountyTaxLink.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                   Grd_CountyTaxLink.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
               }

           }



           else
           {
               Grd_CountyTaxLink.Rows.Clear();
               Grd_CountyTaxLink.Visible = true;
               Grd_CountyTaxLink.DataSource = null;
           }
           lbl_count.Text = "Total Records: " + dtselect.Rows.Count.ToString();
           lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);

            
           
        }


        private void Tax_Assessment_Link_Load(object sender, EventArgs e)
        {
            grp_TaxAssessReg.Visible = false;
            grp_TaxAssessInfo.Visible = true;
         


            btn_Import.Visible = true;
            //grp_TaxAssessReg.Visible = false;
            //grp_TaxAssessInfo.Visible = true;
            if (cbo_State.SelectedIndex == 0 && cbo_County.SelectedIndex==0)
            {
            BindTaxAssessmentGrid();
            }
            First_Page();

            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));


            btn_Import.Visible = true;
            btn_Export.Visible = true;

          
            label4.Visible = true;
            label13.Visible = false;
            label15.Visible = false;

           
        }
        

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            }
        }

        private void btn_GetImportExcel_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"c:\OMS_Import\Tax_Assesment_Import.xlsx";
            if (!Directory.Exists(temppath))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Tax_Assesment_Import.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
        }

        private void txt_Tax_PhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("Invalid!");
            //    }
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Tax_PhoneNo.Text.Length == 0) //for block first whitespace 
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

        }

        private void txt_Assessor_PhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("Invalid!");
            //    }
            //}

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Assessor_PhoneNo.Text.Length == 0) //for block first whitespace 
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

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            CurrentpageIndex++;
            if (cbo_State.SelectedIndex > 0)
            {
                if (CurrentpageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize) - 1)
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
            else if (cbo_County.SelectedIndex > 0)
            {
                if (CurrentpageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1)
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
                btnNext.Enabled = false;
                //btnLast.Enabled = false;
                //btnPrevious.Enabled = true;
                Filter_County_Data();
            }
            else
            {
                if (CurrentpageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
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
                //btnNext.Enabled = false;
                //btnLast.Enabled = false;

              //  BindTaxAssessmentGrid();
            }
            
            BindTaxAssessmentGrid();

           
            this.Cursor = currentCursor;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (cbo_State.SelectedIndex > 0)
            {
                CurrentpageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize) - 1;
                Filter_State_Data();
            }
            else if (cbo_County.SelectedIndex > 0)
            {
                CurrentpageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1;
                Filter_County_Data();
            }
            else
            {
                CurrentpageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
                BindTaxAssessmentGrid();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            
            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
        
            CurrentpageIndex--;
            if (CurrentpageIndex == 0)
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
                BindTaxAssessmentGrid();
            }
                

           
            this.Cursor = currentCursor;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            CurrentpageIndex = 0;
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
                BindTaxAssessmentGrid();
            }

            

            this.Cursor = currentCursor;
        }

        private void Grd_CountyTaxLink_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (cntr % 2 == 0) //This condition applied for toggeling the Ascending and Descending sort
                Grd_CountyTaxLink.Sort(Grd_CountyTaxLink.Columns[e.ColumnIndex], ListSortDirection.Ascending);
            else
                Grd_CountyTaxLink.Sort(Grd_CountyTaxLink.Columns[e.ColumnIndex], ListSortDirection.Descending);
            cntr++;

            //DataGridViewColumn newColumn = Grd_CountyTaxLink.Columns[e.ColumnIndex];
            //DataGridViewColumn oldColumn = Grd_CountyTaxLink.SortedColumn;
            //ListSortDirection direction;

            //// If oldColumn is null, then the DataGridView is not sorted.
            //if (oldColumn != null)
            //{
            //    // Sort the same column again, reversing the SortOrder.
            //    if (oldColumn == newColumn &&
            //        Grd_CountyTaxLink.SortOrder == SortOrder.Ascending)
            //    {
            //        direction = ListSortDirection.Descending;
            //    }
            //    else
            //    {
            //        // Sort a new column and remove the old SortGlyph.
            //        direction = ListSortDirection.Ascending;
            //        oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            //    }
            //}
            //else
            //{
            //    direction = ListSortDirection.Ascending;
            //}

            //// Sort the selected column.
            //Grd_CountyTaxLink.Sort(newColumn, direction);
            //newColumn.HeaderCell.SortGlyphDirection =
            //    direction == ListSortDirection.Ascending ?
            //    SortOrder.Ascending : SortOrder.Descending;

         
        
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
            foreach (DataGridViewColumn column in Grd_CountyTaxLink.Columns)
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
            foreach (DataGridViewRow row in Grd_CountyTaxLink.Rows)
            {

                dt.Rows.Add();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex - 1] = cell.Value.ToString();

                        }
                    }
                }
            }
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Tax_Assessment_Link" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Tax_Assessment_Link");
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

        private void txt_CountyTax_Link_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_CountyTax_Link.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Assessor_Link_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid!");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Assessor_Link.Text.Length == 0) //for block first whitespace 
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
            for (int i = 0; i < Grd_CountyTaxLink.Rows.Count - 1; i++)
            {

                if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {
                    Grd_CountyTaxLink.Rows.RemoveAt(i);
                    i = i - 1;
                }

                lbl_count.Text = (Grd_CountyTaxLink.Rows.Count - 1).ToString();
                lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(Grd_CountyTaxLink.Rows.Count) / pagesize);
            }

       }

        private void btn_removedup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_CountyTaxLink.Rows.Count-1; i++)
            {

                if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    Grd_CountyTaxLink.Rows.RemoveAt(i);
                    i = i - 1;
                }

                lbl_count.Text = (Grd_CountyTaxLink.Rows.Count - 1).ToString();
                lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(Grd_CountyTaxLink.Rows.Count) / pagesize);
            }

        }

        private void btn_Remove_Error_row_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_CountyTaxLink.Rows.Count - 1; i++)
            {
                if (Grd_CountyTaxLink.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    Grd_CountyTaxLink.Rows.RemoveAt(i);
                    i = i - 1;
                }

                lbl_count.Text = (Grd_CountyTaxLink.Rows.Count - 1).ToString();
                lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(Grd_CountyTaxLink.Rows.Count) / pagesize);
            }
        }

    }
}
