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
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
namespace Ordermanagement_01.Masters
{  
    public partial class Client_CostTat : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Client_id;
        int State_id, County_id;
        string state, county;
        System.Data.DataTable dtnonadded = new System.Data.DataTable();
        DataRow dr;
        DataSet ds = new DataSet();
        int Check_value;
        int User_Id;
        decimal orderCost;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string Client_Name;
        int Sub_Client_Id;
        public Client_CostTat(int CLIENT_ID,int USER_ID,string CLIENT_NAME)
        {
            InitializeComponent();
            Client_id = CLIENT_ID;
            Sub_Client_Id = 27;
            User_Id = USER_ID;
            Client_Name = CLIENT_NAME;
        }

        private void grd_Client_cost_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //cProbar.startProgress();
            grd_Client_cost.Rows.Clear();
            //Grid Load Excel
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select Excel file";
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
            //cProbar.stopProgress();
        }
        private void Import(string txtFileName)
        {
            if (Client_id != 0)
            {
                if (txtFileName != string.Empty)
                {
                    try
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
                        int value = 0; int newrow = 0;
                        sda.Fill(data);

                        dtnonadded.Columns.Add("State",typeof(string));
                        dtnonadded.Columns.Add("County", typeof(string));
                        dtnonadded.Columns.Add("COS", typeof(string));
                        dtnonadded.Columns.Add("2WS", typeof(string));
                        dtnonadded.Columns.Add("FS", typeof(string));
                        dtnonadded.Columns.Add("30YS", typeof(string));
                        dtnonadded.Columns.Add("40YS", typeof(string));
                        dtnonadded.Columns.Add("UPS", typeof(string));
                        dtnonadded.Columns.Add("DOCR", typeof(string));
                        dtnonadded.Columns.Add("COMMENTS", typeof(string));

                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            if (data.Rows[i]["State"].ToString() != "" || data.Rows[i]["County"].ToString() != "" || data.Rows[i]["State"].ToString() != null || data.Rows[i]["County"].ToString() != null)
                            {
                             

                                grd_Client_cost.Rows.Add();
                                grd_Client_cost.Rows[i].Cells[0].Value = data.Rows[i]["State"].ToString();
                                grd_Client_cost.Rows[i].Cells[1].Value = data.Rows[i]["County"].ToString();
                                grd_Client_cost.Rows[i].Cells[2].Value = data.Rows[i]["COS"].ToString();

                                grd_Client_cost.Rows[i].Cells[3].Value = data.Rows[i]["2WS"].ToString();
                                grd_Client_cost.Rows[i].Cells[4].Value = data.Rows[i]["FS"].ToString();
                                grd_Client_cost.Rows[i].Cells[5].Value = data.Rows[i]["30YS"].ToString();
                                grd_Client_cost.Rows[i].Cells[6].Value = data.Rows[i]["40YS"].ToString();
                                grd_Client_cost.Rows[i].Cells[7].Value = data.Rows[i]["UPS"].ToString();
                                grd_Client_cost.Rows[i].Cells[8].Value = data.Rows[i]["DOCR"].ToString();
                                grd_Client_cost.Rows[i].Cells[9].Value = data.Rows[i]["COMMENTS"].ToString();
                           
                                grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.White;







                                //Error State
                                state = data.Rows[i]["State"].ToString();
                                county = data.Rows[i]["County"].ToString();
                                Hashtable htstate = new Hashtable();
                                System.Data.DataTable dtstate = new System.Data.DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", state);
                                dtstate = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                                    grd_Client_cost.Rows[i].Cells[10].Value = State_id;
                                }
                                else
                                {
                                    // MessageBox.Show(state + " does not exist in State Info");
                                    State_id = 0;
                                    grd_Client_cost.Rows[i].Cells[10].Value = State_id;
                                    grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }

                                //Error County
                                Hashtable htcounty = new Hashtable();
                                System.Data.DataTable dtcounty = new System.Data.DataTable();
                                htcounty.Add("@Trans", "COUNTYID");
                                htcounty.Add("@County_Name", county);
                                htcounty.Add("@State_Id", State_id);
                                dtcounty = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcounty);
                                if (dtcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtcounty.Rows[0]["County_ID"].ToString());
                                    grd_Client_cost.Rows[i].Cells[11].Value = County_id;
                                }
                                else
                                {
                                    County_id = 0;
                                    //  MessageBox.Show(county + " does not exist in County Info");
                                    grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    grd_Client_cost.Rows[i].Cells[11].Value = County_id;
                                }

                                //Mismatch State and County
                                Hashtable htstcounty = new Hashtable();
                                System.Data.DataTable dtstcounty = new System.Data.DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", county);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                                }
                                else
                                {
                                    County_id = 0;
                                    grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }

                                if (State_id == 0 || County_id == 0)
                                {

                                    grd_Client_cost.Rows[i].Cells[12].Value = "Error";
                                    dr = dtnonadded.NewRow();

                                    dr["State"] = grd_Client_cost.Rows[i].Cells[0].Value.ToString();
                                    dr["County"] = grd_Client_cost.Rows[i].Cells[1].Value.ToString();
                                    dr["COS"] = grd_Client_cost.Rows[i].Cells[2].Value.ToString();
                                    dr["2WS"] = grd_Client_cost.Rows[i].Cells[3].Value.ToString();
                                    dr["FS"] = grd_Client_cost.Rows[i].Cells[4].Value.ToString();
                                    dr["30YS"] = grd_Client_cost.Rows[i].Cells[5].Value.ToString();
                                    dr["40YS"] = grd_Client_cost.Rows[i].Cells[6].Value.ToString();
                                    dr["UPS"] = grd_Client_cost.Rows[i].Cells[7].Value.ToString();
                                    dr["DOCR"] = grd_Client_cost.Rows[i].Cells[8].Value.ToString();
                                    dr["COMMENTS"] = grd_Client_cost.Rows[i].Cells[9].Value.ToString();
                                    dtnonadded.Rows.Add(dr);

                                }
                                else
                                {
                                    grd_Client_cost.Rows[i].Cells[12].Value = "NoError";

                                }

                               
                            

                             
                                //Record Already exists
                                //if ( State_id != 0 && County_id != 0 && OrderType_id != 0)
                                //{
                                //Hashtable htnewrows = new Hashtable();
                                //DataTable dtnewrows = new DataTable();
                                //htnewrows.Add("@Trans", "SELNEWROWS");
                                //htnewrows.Add("@Abstractor_Id", Abstractor_id);
                                //htnewrows.Add("@State", State_id);
                                //htnewrows.Add("@County", County_id);
                                //htnewrows.Add("@Order_Type_Id", OrderType_id);
                                //dtnewrows = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htnewrows);
                                ////  int Newrowvalue = 0;
                                //if (dtnewrows.Rows.Count > 0)
                                //{
                                //    int abstractorid = int.Parse(dtnewrows.Rows[0]["Abstractor_Id"].ToString());
                                //    int stateid = int.Parse(dtnewrows.Rows[0]["State"].ToString());
                                //    int countyid = int.Parse(dtnewrows.Rows[0]["County"].ToString());
                                //    int ordertypeid = int.Parse(dtnewrows.Rows[0]["Order_Type_Id"].ToString());
                                //    if (Abstractor_id == abstractorid && OrderType_id == ordertypeid && State_id == stateid && County_id == countyid)
                                //    {
                                //        //Newrowvalue = 1;
                                //        // grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                //        //newrow = 0;
                                //        newrow = 1;
                                //    }
                                //    else
                                //    {
                                //        newrow = 0;
                                //    }
                                //}


                                //if (value == 1)
                                //{
                                //    //grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                //}
                                //if (newrow == 1)
                                //{
                                //    grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                //}

                            }
                            else
                            {
                                grd_Client_cost.Rows.Clear();
                                MessageBox.Show("Check Empty Cells in Excel");
                            }

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void Client_CostTat_Load(object sender, EventArgs e)
        {
            lbl_CLient_Name.Text = Client_Name.ToString();
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //cProbar.startProgress();

            for (int i = 0; i < grd_Client_cost.Rows.Count;i++ )
            {


                string State = grd_Client_cost.Rows[i].Cells[10].Value.ToString();
                string County = grd_Client_cost.Rows[i].Cells[11].Value.ToString();
                string error = grd_Client_cost.Rows[i].Cells[12].Value.ToString();

                if (error != "Error")
                {

                    if (grd_Client_cost.Columns[2].HeaderText == "COS")
                    {
                        string Value = grd_Client_cost.Rows[i].Cells[2].Value.ToString();
                        if (Value != "" || Value != null)
                        {
                            orderCost =Convert.ToDecimal(grd_Client_cost.Rows[i].Cells[2].Value.ToString());
                        }
                        else
                        {

                            orderCost = 0;
                        }
                        int Order_Type_Id = 1;
                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Client_Id",Client_id);
                        htcheck.Add("@Sub_Process_Id", Sub_Client_Id);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County_Id", County);
                        htcheck.Add("@State_Id", State);
                        dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_value = 0;
                        }

                        if (Check_value == 0)
                        {
                          
                            Hashtable htabsinsert = new Hashtable();
                            System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Client_Id",Client_id);
                            htabsinsert.Add("@Sub_Process_Id", Sub_Client_Id);
                            htabsinsert.Add("@State_Id", State);
                            htabsinsert.Add("@County_Id", County);
                            htabsinsert.Add("@Order_Type_Id",Order_Type_Id);
                            htabsinsert.Add("@Order_Cost",orderCost);
                            htabsinsert.Add("@Inserted_By", User_Id);
                            htabsinsert.Add("@Inserted_date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);

                        }


                    }
                    if (grd_Client_cost.Columns[3].HeaderText == "2WS")
                    { 
                        string Value = grd_Client_cost.Rows[i].Cells[3].Value.ToString();
                        if (Value != "" || Value != null)
                        {
                       
                            orderCost = Convert.ToDecimal(grd_Client_cost.Rows[i].Cells[3].Value.ToString());
                        }
                        else
                        {

                            orderCost = 0;
                        }
                       
                        int Order_Type_Id = 29;
                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Client_Id", Client_id);
                        htcheck.Add("@Sub_Process_Id", Sub_Client_Id);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County_Id", County);
                        htcheck.Add("@State_Id", State);
                        dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_value = 0;
                        }

                        if (Check_value == 0)
                        {

                            Hashtable htabsinsert = new Hashtable();
                            System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Client_Id", Client_id);
                            htabsinsert.Add("@Sub_Process_Id", Sub_Client_Id);
                            htabsinsert.Add("@State_Id", State);
                            htabsinsert.Add("@County_Id", County);
                            htabsinsert.Add("@Order_Type_Id", Order_Type_Id);
                            htabsinsert.Add("@Order_Cost", orderCost);
                            htabsinsert.Add("@Inserted_By", User_Id);
                            htabsinsert.Add("@Inserted_date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);

                        }


                    }
                    if (grd_Client_cost.Columns[4].HeaderText == "FS")
                    {
                        string Value = grd_Client_cost.Rows[i].Cells[4].Value.ToString();
                        if (grd_Client_cost.Rows[i].Cells[4].Value.ToString()!=string.Empty || Value != null)
                        {
                        
                            orderCost = Convert.ToDecimal(grd_Client_cost.Rows[i].Cells[4].Value.ToString());
                        }
                        else
                        {

                            orderCost = 0;
                        }
                       
                      
                        int Order_Type_Id = 36;
                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Client_Id", Client_id);
                        htcheck.Add("@Sub_Process_Id", Sub_Client_Id);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County_Id", County);
                        htcheck.Add("@State_Id", State);
                        dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_value = 0;
                        }

                        if (Check_value == 0)
                        {

                            Hashtable htabsinsert = new Hashtable();
                            System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Client_Id", Client_id);
                            htabsinsert.Add("@Sub_Process_Id", Sub_Client_Id);
                            htabsinsert.Add("@State_Id", State);
                            htabsinsert.Add("@County_Id", County);
                            htabsinsert.Add("@Order_Type_Id", Order_Type_Id);
                            htabsinsert.Add("@Order_Cost", orderCost);
                            htabsinsert.Add("@Inserted_By", User_Id);
                            htabsinsert.Add("@Inserted_date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);

                        }


                    }
                    if (grd_Client_cost.Columns[5].HeaderText == "30YS")
                    {string Value = grd_Client_cost.Rows[i].Cells[5].Value.ToString();
                        if (Value != "" || Value != null)
                        {
                        
                            orderCost = Convert.ToDecimal(grd_Client_cost.Rows[i].Cells[5].Value.ToString());
                        }
                        else
                        {

                            orderCost = 0;
                        }
                       
                        int Order_Type_Id = 30;
                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Client_Id", Client_id);
                        htcheck.Add("@Sub_Process_Id", Sub_Client_Id);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County_Id", County);
                        htcheck.Add("@State_Id", State);
                        dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_value = 0;
                        }

                        if (Check_value == 0)
                        {

                            Hashtable htabsinsert = new Hashtable();
                            System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Client_Id", Client_id);
                            htabsinsert.Add("@Sub_Process_Id", Sub_Client_Id);
                            htabsinsert.Add("@State_Id", State);
                            htabsinsert.Add("@County_Id", County);
                            htabsinsert.Add("@Order_Type_Id", Order_Type_Id);
                            htabsinsert.Add("@Order_Cost", orderCost);
                            htabsinsert.Add("@Inserted_By", User_Id);
                            htabsinsert.Add("@Inserted_date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);

                        }


                    }
                    if (grd_Client_cost.Columns[6].HeaderText == "40YS")
                    {
                        string Value = grd_Client_cost.Rows[i].Cells[6].Value.ToString();
                        if (Value != "" || Value != null)
                        {
                        
                            orderCost = Convert.ToDecimal(grd_Client_cost.Rows[i].Cells[6].Value.ToString());
                        }
                        else
                        {

                            orderCost = 0;
                        }
                       
                        
                        int Order_Type_Id = 38;
                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Client_Id", Client_id);
                        htcheck.Add("@Sub_Process_Id", Sub_Client_Id);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County_Id", County);
                        htcheck.Add("@State_Id", State);
                        dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_value = 0;
                        }


                        if (Check_value == 0)
                        {

                            Hashtable htabsinsert = new Hashtable();
                            System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Client_Id", Client_id);
                            htabsinsert.Add("@Sub_Process_Id", Sub_Client_Id);
                            htabsinsert.Add("@State_Id", State);
                            htabsinsert.Add("@County_Id", County);
                            htabsinsert.Add("@Order_Type_Id", Order_Type_Id);
                            htabsinsert.Add("@Order_Cost", orderCost);
                            htabsinsert.Add("@Inserted_By", User_Id);
                            htabsinsert.Add("@Inserted_date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);

                        }


                    }
                    if (grd_Client_cost.Columns[7].HeaderText == "UPS")
                    { string Value = grd_Client_cost.Rows[i].Cells[7].Value.ToString();
                        if (Value != "" || Value != null)
                        {
                        

                            orderCost = Convert.ToDecimal(grd_Client_cost.Rows[i].Cells[7].Value.ToString());
                        }
                        else {
                            orderCost = 0;
                        }
                        int Order_Type_Id = 7;
                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Client_Id", Client_id);
                        htcheck.Add("@Sub_Process_Id", Sub_Client_Id);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County_Id", County);
                        htcheck.Add("@State_Id", State);
                        dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_value = 0;
                        }


                        if (Check_value == 0)
                        {

                            Hashtable htabsinsert = new Hashtable();
                            System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Client_Id", Client_id);
                            htabsinsert.Add("@Sub_Process_Id", Sub_Client_Id);
                            htabsinsert.Add("@State_Id", State);
                            htabsinsert.Add("@County_Id", County);
                            htabsinsert.Add("@Order_Type_Id", Order_Type_Id);
                            htabsinsert.Add("@Order_Cost", orderCost);
                            htabsinsert.Add("@Inserted_By", User_Id);
                            htabsinsert.Add("@Inserted_date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);

                        }


                    }
                    if (grd_Client_cost.Columns[8].HeaderText == "DOCR")
                    {
                        string Value = grd_Client_cost.Rows[i].Cells[8].Value.ToString();
                        if (Value != "" || Value != null)
                        { 
                       
                            orderCost = Convert.ToDecimal(grd_Client_cost.Rows[i].Cells[8].Value.ToString());
                        }
                        else
                        {
                            orderCost = 0;
                        }
                        int Order_Type_Id = 21;
                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Client_Id", Client_id);
                        htcheck.Add("@Sub_Process_Id", Sub_Client_Id);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County_Id", County);
                        htcheck.Add("@State_Id", State);
                        dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_value = 0;
                        }


                        if (Check_value == 0)
                        {

                            Hashtable htabsinsert = new Hashtable();
                            System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Client_Id", Client_id);
                            htabsinsert.Add("@Sub_Process_Id", Sub_Client_Id);
                            htabsinsert.Add("@State_Id", State);
                            htabsinsert.Add("@County_Id", County);
                            htabsinsert.Add("@Order_Type_Id", Order_Type_Id);
                            htabsinsert.Add("@Order_Cost", orderCost);
                            htabsinsert.Add("@Inserted_By", User_Id);
                            htabsinsert.Add("@Inserted_date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);

                        }


                    }

                   
                }

            }
            //cProbar.stopProgress();
            MessageBox.Show("Record Imported Sucessfully");
        }

        private void btn_NonAddedRows_Click(object sender, EventArgs e)
        {

        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            DataSet dsexport = new DataSet();

            ds.Clear();
            ds.Tables.Add(dtnonadded);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Convert_Dataset_to_Excel();
            }
            ds.Clear();
            dtnonadded.Clear();

        }
        private void Convert_Dataset_to_Excel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = true;
            Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            DataTableCollection collection = ds.Tables;

            for (int i = collection.Count; i > 0; i--)
            {
                Sheets xlSheets = null;
                Worksheet xlWorksheet = null;
                //Create Excel Sheets
                xlSheets = ExcelApp.Worksheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                               Type.Missing, Type.Missing, Type.Missing);

                System.Data.DataTable table = collection[i - 1];
                xlWorksheet.Name = table.TableName;

                for (int j = 1; j < table.Columns.Count + 1; j++)
                {
                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                }

                // Storing Each row and column value to excel sheet
                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 0; l < table.Columns.Count; l++)
                    {
                        ExcelApp.Cells[k + 2, l + 1] =
                        table.Rows[k].ItemArray[l].ToString();
                    }
                }
                ExcelApp.Columns.AutoFit();
            }
            ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
            ExcelApp.Visible = true;

        }

        private void btn_SampleFormat_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\Temp\OMS_Import\");
            string temppath = @"c:\Temp\OMS_Import\Client_Order_Cost.xlsx";
            File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Client_Order_Cost.xlsx", temppath, true);
           // File.Copy(Environment.CurrentDirectory + "\\Client_Order_Cost.xlsx", temppath, true);
            Process.Start(temppath);
        }
    }
}
