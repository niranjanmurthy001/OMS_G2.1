using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using ClosedXML.Excel;
//using ClosedXML.Excel;

namespace Ordermanagement_01.Client_Proposal
{
    public partial class Client_Proposal_Email : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        InfiniteProgressBar.clsProgress loader = new InfiniteProgressBar.clsProgress();
        string Path1, Export_Title;

        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        System.Data.DataTable dtuser = new System.Data.DataTable();
        int User_id, check = 0;
        static int currentpageindex = 0;
        int pagesize = 200;
        int Index;
        public Client_Proposal_Email(int userid)
        {
            InitializeComponent();
            User_id = userid;
        }

        private void Client_Proposal_Email_Load(object sender, EventArgs e)
        {
            dbc.Bind_Proposal_From(ddl_Proposal_From);
            ddl_Proposal_From.SelectedIndex = 1;
            if (ddl_Proposal_From.SelectedIndex > 0)
            {
                if (rbtn_Proposal_NotSended.Checked == true)
                {
                    Bind_Proposal_Not_send();
                }
                else
                {
                    Bind_Proposal_send();
                }
                btn_First_Click(sender, e);

            }
            else
            {

                MessageBox.Show("Select Proposal Type");
                ddl_Proposal_From.Focus();
            }
           
        }
        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btn_Previous.Enabled = false;
            btn_Next.Enabled = true;
            btn_Last.Enabled = true;
            btn_First.Enabled = false;
            this.Cursor = currentCursor;
        }
        private void Bind_Proposal_send()
        {
            if (ddl_Proposal_From.SelectedIndex > 0)
            {
                ht.Clear(); ht.Clear();
                ht.Add("@Trans", "EMAIL_SEND");
                ht.Add("@Proposal_From_Id", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client_1", ht);

                dtuser = dt;
            
                System.Data.DataTable temptable = dtuser.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dtuser.Rows.Count)
                {
                    endindex = dtuser.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetDataRow(ref row, dtuser.Rows[i]);
                    temptable.Rows.Add(row);
                }


                if (temptable.Rows.Count > 0)
                {
                    grd_Proposal_Email.Rows.Clear();
                    grd_Export.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Proposal_Email.Rows.Add();
                        grd_Proposal_Email.Rows[i].Cells[1].Value = temptable.Rows[i]["Proposal_Client_Id"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[2].Value = i + 1;
                        grd_Proposal_Email.Rows[i].Cells[3].Value = temptable.Rows[i]["Proposal_From"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[4].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[5].Value = temptable.Rows[i]["Email_Id"].ToString();
                       
                        //grd_Proposal_Email.Rows[i].Cells[5].Value = dt.Rows[i]["State"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[6].Value = dt.Rows[i]["County"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[5].Value = dt.Rows[i]["Date"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[8].Value = "View";


                        if (dt.Rows[i]["Modified_by"].ToString() == null || temptable.Rows[i]["Modified_by"].ToString() == "")
                        {
                            grd_Proposal_Email.Rows[i].Cells[9].Value = temptable.Rows[i]["Inserted_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[10].Value = temptable.Rows[i]["Inserted_Date"].ToString();
                        }
                        else
                        {
                            grd_Proposal_Email.Rows[i].Cells[11].Value = temptable.Rows[i]["Modified_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[12].Value = temptable.Rows[i]["Modified_Date"].ToString();
                        }
                        Column12.Visible = true;
                        Column13.Visible = true;
                       

                        grd_Export.Rows.Add();
                        grd_Export.Rows[i].Cells[0].Value = i + 1;
                        grd_Export.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Export.Rows[i].Cells[2].Value = temptable.Rows[i]["Email_Id"].ToString();
                        grd_Export.Rows[i].Cells[3].Value = temptable.Rows[i]["Date"].ToString();
                       
                    }
                    //Hashtable htcmt = new Hashtable();
                    //DataTable dtcmt = new DataTable();
                    //for (int j = 0; j < grd_Proposal_Email.Rows.Count; j++)
                    //{

                    //    htcmt.Clear(); dtcmt.Clear();
                    //    htcmt.Add("@Trans", "EMAIL_FOLLOW");
                    //    htcmt.Add("@Proposal_Client_Id", int.Parse(grd_Proposal_Email.Rows[j].Cells[1].Value.ToString()));
                    //    htcmt.Add("@Proposal_From_Id", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                    //    //htcmt.Add("@Proposal_From_Id",);
                    //    dtcmt = dataaccess.ExecuteSP("Sp_Proposal_Client_1", htcmt);

                    //    if (dtcmt.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < dtcmt.Rows.Count; i++)
                    //        {
                    //            if (dtcmt.Rows[i]["Follow_up_Date"].ToString() != "")
                    //            {
                    //                grd_Proposal_Email.Rows[j].Cells[7].Value = dtcmt.Rows[i]["Follow_up_Date"].ToString();
                    //            }
                    //        }
                    //    }
                    //}

                    lbl_Total_orders.Text = dtuser.Rows.Count.ToString();
                    lbl_Record_status.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtuser.Rows.Count) / pagesize);
                }
                else
                {
                    grd_Export.Rows.Clear();
                    grd_Export.DataSource = null;
                    grd_Proposal_Email.Rows.Clear();
                    grd_Proposal_Email.DataSource = null;
                }
            }
            else
            {

                MessageBox.Show("select Proposal Type");
                ddl_Proposal_From.Focus();
            }
        }
        private void Bind_Proposal_Not_send()
        {
            if (ddl_Proposal_From.SelectedIndex > 0)
            {
                ht.Clear(); ht.Clear();
                ht.Add("@Trans", "EMAIL_NOT_SEND");
                ht.Add("@Proposal_From_Id", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));

                dt = dataaccess.ExecuteSP("Sp_Proposal_Client_1", ht);
                dtuser = dt;
                //Hashtable htcmt=new Hashtable();
                //DataTable dtcmt=new DataTable();
                //htcmt.Add("@Trans", "EMAIL_FOLLOW");
                //dtcmt = dataaccess.ExecuteSP("Sp_Proposal_Client_1", htcmt);
                System.Data.DataTable temptable = dtuser.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dtuser.Rows.Count)
                {
                    endindex = dtuser.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetDataRow(ref row, dtuser.Rows[i]);
                    temptable.Rows.Add(row);
                }


                if (temptable.Rows.Count > 0)
                {
                    grd_Proposal_Email.Rows.Clear();
                    grd_Export.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Proposal_Email.Rows.Add();
                        grd_Proposal_Email.Rows[i].Cells[1].Value = temptable.Rows[i]["Proposal_Client_Id"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[2].Value = i + 1;
                        grd_Proposal_Email.Rows[i].Cells[3].Value = temptable.Rows[i]["Proposal_From"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[4].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[5].Value = temptable.Rows[i]["Email_Id"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[5].Value = dt.Rows[i]["State"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[6].Value = dt.Rows[i]["County"].ToString();

                        //grd_Proposal_Email.Rows[i].Cells[5].Value = "N/A";
                        grd_Proposal_Email.Rows[i].Cells[8].Value = "View";


                        if (temptable.Rows[i]["Modified_by"].ToString() == null || temptable.Rows[i]["Modified_by"].ToString() == "")
                        {
                            grd_Proposal_Email.Rows[i].Cells[9].Value = temptable.Rows[i]["Inserted_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[10].Value = temptable.Rows[i]["Inserted_Date"].ToString();
                        }
                        else
                        {
                            grd_Proposal_Email.Rows[i].Cells[11].Value = temptable.Rows[i]["Modified_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[12].Value = temptable.Rows[i]["Modified_Date"].ToString();
                        }
                        Column12.Visible = false;
                        Column13.Visible = false;
                        //if (dt.Rows[i]["Last_Sent_By"].ToString() == null || dt.Rows[i]["Last_Sent_By"].ToString() == "")
                        //{
                        //    grd_Proposal_Email.Rows[i].Cells[11].Value = dt.Rows[i]["Sent_By"].ToString();
                        //    grd_Proposal_Email.Rows[i].Cells[12].Value = dt.Rows[i]["Send_Date"].ToString();
                        //}
                        //else
                        //{
                        //    grd_Proposal_Email.Rows[i].Cells[11].Value = dt.Rows[i]["Last_Sent_By"].ToString();
                        //    grd_Proposal_Email.Rows[i].Cells[12].Value = dt.Rows[i]["Last_Send_Date"].ToString();
                        //}
                        grd_Export.Rows.Add();
                        grd_Export.Rows[i].Cells[0].Value = i + 1;
                        grd_Export.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Export.Rows[i].Cells[2].Value = temptable.Rows[i]["Email_Id"].ToString();
                        grd_Export.Rows[i].Cells[3].Value = "N/A";



                    }

                    lbl_Total_orders.Text = dtuser.Rows.Count.ToString();
                    lbl_Record_status.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtuser.Rows.Count) / pagesize);
                    Hashtable htcmt = new Hashtable();
                    DataTable dtcmt = new DataTable();

                    //for (int j = 0; j < grd_Proposal_Email.Rows.Count; j++)
                    //{
                    //    htcmt.Clear(); dtcmt.Clear();
                    //    htcmt.Add("@Trans", "EMAIL_FOLLOW");
                    //    htcmt.Add("@Proposal_Client_Id", int.Parse(grd_Proposal_Email.Rows[j].Cells[1].Value.ToString()));
                    //    htcmt.Add("@Proposal_From_Id", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                    //    dtcmt = dataaccess.ExecuteSP("Sp_Proposal_Client_1", htcmt);

                    //    if (dtcmt.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < dtcmt.Rows.Count; i++)
                    //        {
                    //            if (dtcmt.Rows[i]["Follow_up_Date"].ToString() != "")
                    //            {
                    //                grd_Proposal_Email.Rows[j].Cells[7].Value = dtcmt.Rows[i]["Follow_up_Date"].ToString();
                    //            }
                    //        }
                    //    }
                    //}
                }
                else
                {
                    grd_Export.Rows.Clear();
                    grd_Export.DataSource = null;
                    grd_Proposal_Email.Rows.Clear();
                    grd_Proposal_Email.DataSource = null;
                }
            }
            else
            {

                MessageBox.Show("select Proposal Type");
                ddl_Proposal_From.Focus();
            }
        }

        private void GetDataRow(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtuser.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void btn_Send_All_Click(object sender, EventArgs e)
        {
            //form_loader.Start_progres();
            loader.startProgress();
            btn_Send_All.Enabled = false;
            for (int i = 0; i < grd_Proposal_Email.Rows.Count; i++)
            {
                bool ischeck = (bool)grd_Proposal_Email[0, i].FormattedValue;
                if (ischeck == true)
                {
                    //sending mail
                    string client_name = grd_Proposal_Email.Rows[i].Cells[4].Value.ToString();
                    string client_id = grd_Proposal_Email.Rows[i].Cells[1].Value.ToString();
                    string emailid = grd_Proposal_Email.Rows[i].Cells[5].Value.ToString();
                    int proposal_clientid = int.Parse(grd_Proposal_Email.Rows[i].Cells[1].Value.ToString());


                    Ordermanagement_01.Client_Proposal.Sample email = new Ordermanagement_01.Client_Proposal.Sample(client_id, client_name, User_id, emailid, proposal_clientid, "bulk",int.Parse(ddl_Proposal_From.SelectedValue.ToString()));

                 


                    


                    //MessageBox.Show("Mail Sended Successfully");
                }
                else
                {
                    check = check + 1;
                }
            }
            if (check == grd_Proposal_Email.Rows.Count)
            {
                btn_Send_All.Enabled = true;
                loader.stopProgress();
                MessageBox.Show("Kindly select, whichever Email want to be send");
                check = 0;
                Bind_Proposal_Not_send();
            }
            else
            {
                btn_Send_All.Enabled = true;
                Bind_Proposal_Not_send();
                loader.stopProgress();
                MessageBox.Show("Mail Sent Successfully");
                check = 0;
                check_All.Checked = false;
            }
            
                
        }

      

        private void txt_search_proposal_TextChanged(object sender, EventArgs e)
        {
            if (txt_search_proposal.Text != "" && txt_search_proposal.Text != "Search Proposal Client...")
            {
                Filter_Data();



            }
            else
            {
                if (rbtn_Proposal_NotSended.Checked == true)
                {
                    Bind_Proposal_Not_send();
                }
                else if (rbtn_Proposal_Sended.Checked == true)
                {
                    Bind_Proposal_send();
                }
            }
        }

        private void rbtn_Proposal_NotSended_CheckedChanged(object sender, EventArgs e)
        {
            loader.startProgress();
            Bind_Proposal_Not_send();
            check_All.Checked = false;
            loader.stopProgress();
        }

        private void rbtn_Proposal_Sended_CheckedChanged(object sender, EventArgs e)
        {
            loader.startProgress();
            Bind_Proposal_send();
            check_All.Checked = false;
            loader.stopProgress();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            loader.startProgress();
            if (rbtn_Proposal_NotSended.Checked == true)
            {
                Bind_Proposal_Not_send();
            }
            else
            {
                Bind_Proposal_send();
            }
            check_All.Checked = false;
            txt_search_proposal.Text = "";
            loader.stopProgress();
        }

        private void check_All_CheckedChanged(object sender, EventArgs e)
        {
            if (check_All.Checked == true)
            {
                for (int i = 0; i < grd_Proposal_Email.Rows.Count; i++)
                {
                    grd_Proposal_Email[0, i].Value = true;
                }
            }
            else if (check_All.Checked == false)
            {
                for (int i = 0; i < grd_Proposal_Email.Rows.Count; i++)
                {
                    grd_Proposal_Email[0, i].Value = false;
                }
            }
        }

        private void grd_Proposal_Email_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    
        private void txt_search_proposal_MouseEnter(object sender, EventArgs e)
        {
            if (txt_search_proposal.Text == "Search Proposal Client...")
            {
                txt_search_proposal.Text = "";
            }
        }

        private void Export_Proposal_Sent()
        {
            System.Data.DataTable dt1 = new System.Data.DataTable();

            
            foreach(DataGridViewColumn col in grd_Export.Columns)
            {
                //if (col.Index != 6)
                //{
                    if (col.HeaderText != "")
                    {
                        if (col.ValueType == null)
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (col.ValueType == typeof(int))
                            {
                                dt1.Columns.Add(col.HeaderText, typeof(int));
                            }
                            else if (col.ValueType == typeof(decimal))
                            {
                                dt1.Columns.Add(col.HeaderText, typeof(decimal));
                            }
                            else if (col.ValueType == typeof(DateTime))
                            {
                                dt1.Columns.Add(col.HeaderText, typeof(string));
                            }
                            else if (col.ValueType == typeof(CheckBox))
                            {
                                dt1.Columns.Add(col.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt1.Columns.Add(col.HeaderText, col.ValueType);
                            }
                        }
                    //}
                }
            }
              //Adding the Rows
            foreach (DataGridViewRow row in grd_Export.Rows)
            {
                dt1.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() != "" )
                    {
                        
                            dt1.Rows[dt1.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                      
                    }
                }

            }
            string folderpath = @"C:\\temp\\";
            Path1 = folderpath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title + ".xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt1, Export_Title.ToString());
                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (rbtn_Proposal_NotSended.Checked == true)
            {
                form_loader.Start_progres();
                Export_Title = "Proposal_Not_Send";
                //Export Proposal Not Sent data
                Export_Proposal_Sent();
            }
            else if (rbtn_Proposal_Sended.Checked == true)
            {
                form_loader.Start_progres();
                Export_Title = "Proposal_Send";
                //Export Proposal Sent data
                Export_Proposal_Sent();
            }

        }

        private void ddl_Proposal_From_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddl_Proposal_From.SelectedIndex > 0)
            
            {
                if (rbtn_Proposal_NotSended.Checked == true)
                {
                    Bind_Proposal_Not_send();
                }
                else
                {
                    Bind_Proposal_send();
                }

            }
        }

    


        private void btn_First_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btn_Previous.Enabled = false;
            btn_Next.Enabled = true;
            btn_Last.Enabled = true;
            btn_First.Enabled = false;
            if (txt_search_proposal.Text != "" && txt_search_proposal.Text != "Search Proposal Client...")
            {
                Filter_Data();
            }
            else
            {
                if (ddl_Proposal_From.SelectedIndex > 0)
                {
                    if (rbtn_Proposal_NotSended.Checked == true)
                    {
                        Bind_Proposal_Not_send();
                    }
                    else
                    {
                        Bind_Proposal_send();
                    }

                }
            }
            this.Cursor = currentCursor;
        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
            currentpageindex--;
            if (currentpageindex == 0)
            {
                btn_Previous.Enabled = false;
                btn_First.Enabled = false;
            }
            else
            {
                btn_Previous.Enabled = true;
                btn_First.Enabled = true;

            }
            btn_Next.Enabled = true;
            btn_Last.Enabled = true;
            if (txt_search_proposal.Text != "" && txt_search_proposal.Text != "Search Proposal Client...")
            {
                Filter_Data();
            }
            else
            {
                if (ddl_Proposal_From.SelectedIndex > 0)
                {
                    if (rbtn_Proposal_NotSended.Checked == true)
                    {
                        Bind_Proposal_Not_send();
                    }
                    else
                    {
                        Bind_Proposal_send();
                    }

                }
            }
            this.Cursor = currentCursor;
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex++;
            if (txt_search_proposal.Text != "" && txt_search_proposal.Text != "Search Proposal Client...")
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1)
                {
                    btn_Next.Enabled = false;
                    btn_Last.Enabled = false;
                }
                else
                {
                    btn_Next.Enabled = true;
                    btn_Last.Enabled = true;

                }
                Filter_Data();
            }
            else
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtuser.Rows.Count) / pagesize) - 1)
                {
                    btn_Next.Enabled = false;
                    btn_Last.Enabled = false;
                }
                else
                {
                    btn_Next.Enabled = true;
                    btn_Last.Enabled = true;

                }
                if (ddl_Proposal_From.SelectedIndex > 0)
                {
                    if (rbtn_Proposal_NotSended.Checked == true)
                    {
                        Bind_Proposal_Not_send();
                    }
                    else
                    {
                        Bind_Proposal_send();
                    }

                }
            }
            btn_First.Enabled = true;
            btn_Previous.Enabled = true;
            this.Cursor = currentCursor;
        }

        private void btn_Last_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txt_search_proposal.Text != "")
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
                Filter_Data();
            }
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtuser.Rows.Count) / pagesize) - 1;
                if (ddl_Proposal_From.SelectedIndex > 0)
                {
                    if (rbtn_Proposal_NotSended.Checked == true)
                    {
                        Bind_Proposal_Not_send();
                    }
                    else
                    {
                        Bind_Proposal_send();
                    }

                }
            }
            btn_First.Enabled = true;
            btn_Previous.Enabled = true;
            btn_Next.Enabled = false;
            btn_Last.Enabled = false;

            this.Cursor = currentCursor;
        }

        private void Filter_Data()
        {


            if (txt_search_proposal.Text != "" && txt_search_proposal.Text != "Search Proposal Client...")
            {
              
                DataView dtsearch = new DataView(dtuser);
                string search = txt_search_proposal.Text.ToString();
                dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' or Email_Id like '%" + search.ToString() +  "%'";

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
                    GetDataRow_Search(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }
                if (temptable.Rows.Count > 0)
                {
                    grd_Proposal_Email.Rows.Clear();
                    grd_Export.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Proposal_Email.Rows.Add();
                        grd_Proposal_Email.Rows[i].Cells[1].Value = temptable.Rows[i]["Proposal_Client_Id"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[2].Value = i + 1;
                        grd_Proposal_Email.Rows[i].Cells[3].Value = temptable.Rows[i]["Proposal_From"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[4].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[5].Value = temptable.Rows[i]["Email_Id"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[5].Value = dt.Rows[i]["State"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[6].Value = dt.Rows[i]["County"].ToString();



                        grd_Export.Rows.Add();
                        grd_Export.Rows[i].Cells[0].Value = i + 1;
                        grd_Export.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Export.Rows[i].Cells[2].Value = temptable.Rows[i]["Email_Id"].ToString();
                        if (rbtn_Proposal_Sended.Checked == true)
                        {
                            grd_Proposal_Email.Rows[i].Cells[7].Value = temptable.Rows[i]["Date"].ToString();
                            grd_Export.Rows[i].Cells[3].Value = temptable.Rows[i]["Date"].ToString();
                        }
                        else if (rbtn_Proposal_NotSended.Checked == true)
                        {
                            grd_Proposal_Email.Rows[i].Cells[7].Value = "N/A";
                            grd_Export.Rows[i].Cells[3].Value = "N/A";
                        }
                        grd_Proposal_Email.Rows[i].Cells[8].Value = "View";
                        // grd_Proposal_Email.Rows[i].Cells[6].Value = dt.Rows[i]["Follow_up_Date"].ToString();
                        if (dt.Rows[i]["Modified_by"].ToString() == null || temptable.Rows[i]["Modified_by"].ToString() == "")
                        {
                            grd_Proposal_Email.Rows[i].Cells[9].Value = temptable.Rows[i]["Inserted_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[10].Value = temptable.Rows[i]["Inserted_Date"].ToString();
                        }
                        else
                        {
                            grd_Proposal_Email.Rows[i].Cells[9].Value = temptable.Rows[i]["Modified_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[10].Value = temptable.Rows[i]["Modified_Date"].ToString();
                        }
                       

                    }
                    Hashtable htcmt = new Hashtable();
                    DataTable dtcmt = new DataTable();

                    for (int j = 0; j < grd_Proposal_Email.Rows.Count; j++)
                    {
                        htcmt.Clear(); dtcmt.Clear();
                        htcmt.Add("@Trans", "EMAIL_FOLLOW");
                        htcmt.Add("@Proposal_Client_Id", int.Parse(grd_Proposal_Email.Rows[j].Cells[1].Value.ToString()));
                        dtcmt = dataaccess.ExecuteSP("Sp_Proposal_Client_1", htcmt);

                        if (dtcmt.Rows.Count > 0)
                        {
                            if (dtcmt.Rows[0]["Follow_up_Date"].ToString() != "")
                            {
                                grd_Proposal_Email.Rows[j].Cells[7].Value = dtcmt.Rows[0]["Follow_up_Date"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    grd_Proposal_Email.Rows.Clear();
                    grd_Export.Rows.Clear();
                }
                lbl_Total_orders.Text = dt.Rows.Count.ToString();
                lbl_Record_status.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
            }
            else
            {
                if (rbtn_Proposal_NotSended.Checked == true)
                {
                    Bind_Proposal_Not_send();
                }
                else if (rbtn_Proposal_Sended.Checked == true)
                {
                    Bind_Proposal_send();
                }
                
            }
        }


        private void GetDataRow_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void grd_Proposal_Email_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 13)
                {
                    //mail sending code
                    form_loader.Start_progres();

                    string Clientname = grd_Proposal_Email.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string clientid = grd_Proposal_Email.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string emailid = grd_Proposal_Email.Rows[e.RowIndex].Cells[5].Value.ToString();
                    int proposal_clientid = int.Parse(grd_Proposal_Email.Rows[e.RowIndex].Cells[1].Value.ToString());
                    //Ordermanagement_01.Client_Proposal.Proposal_Email email = new Ordermanagement_01.Client_Proposal.Proposal_Email(clientid, Clientname, User_id, emailid, proposal_clientid);

                    Ordermanagement_01.Client_Proposal.Sample email = new Ordermanagement_01.Client_Proposal.Sample(clientid, Clientname, User_id, emailid, proposal_clientid, "", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));

                    //email.Show();
                    if (rbtn_Proposal_NotSended.Checked == true)
                    {
                        Bind_Proposal_Not_send();
                    }
                    else
                    {
                        Bind_Proposal_send();
                    }

                    //Bind_Proposal_Not_send();
                }
                else if (e.ColumnIndex == 8)
                {
                    form_loader.Start_progres();

                    string Clientname = grd_Proposal_Email.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string clientid = grd_Proposal_Email.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string emailid = grd_Proposal_Email.Rows[e.RowIndex].Cells[5].Value.ToString();
                    int proposal_clientid = int.Parse(grd_Proposal_Email.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string inserted_by = grd_Proposal_Email.Rows[e.RowIndex].Cells[9].Value.ToString();
                    string modified_by = grd_Proposal_Email.Rows[e.RowIndex].Cells[10].Value.ToString();

                    Ordermanagement_01.Client_Proposal.Client_Proposal_History history = new Ordermanagement_01.Client_Proposal.Client_Proposal_History(clientid, Clientname, User_id, emailid, proposal_clientid, inserted_by, modified_by, int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                    history.Show();

                }

                
            }


        }

        private void ddl_Proposal_From_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


}
