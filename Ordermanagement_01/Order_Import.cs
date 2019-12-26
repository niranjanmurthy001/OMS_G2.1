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



namespace Ordermanagement_01
{
    public partial class Order_Import : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_id, Stateid, Countyid, Ordertypeid, Taskid, Clientid, Subprocessid;
        int State_id, County_id, Ordertype_id, Task_id, Checkorder, Client_id;
        int checkvalue;
        int Check_order;
        int value; int newrow,Assign_County_Type_ID;
        string Date, Orderno, Client, Subprocess, Ordertype, State, County, Task, MAX_ORDER_NUMBER, Borrower_Firstname, Borrower_Lastname, Address;
        string Assign_County_Type;
        public Order_Import(int userid)
        {
            InitializeComponent();
            User_id=userid;
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
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
           // Gridview_Bind_Ordes();
        }
        private void Import(string txtFileName)
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

                    sda.Fill(data);
                    ////  grd_order.DataSource = data;
                    //Hashtable httruncate = new Hashtable();
                    //DataTable dttruncate = new System.Data.DataTable();
                    //httruncate.Add("@Trans", "TRUNCATE");
                    //dttruncate = dataaccess.ExecuteSP("Sp_Temp_Order", httruncate);
                    grd_OrderEntry.Rows.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        if (data.Rows[i]["Date"].ToString() != "" &&
                            data.Rows[i]["Time"].ToString() != "" &&
                            data.Rows[i]["Order Number"].ToString() != "" &&
                            data.Rows[i]["Client"].ToString() != "" &&
                            data.Rows[i]["Sub process"].ToString() != "" &&
             
                            data.Rows[i]["Order Type"].ToString() != "" &&
                            data.Rows[i]["Property Address"].ToString() != "" &&
                            data.Rows[i]["State"].ToString() != "" &&
                            data.Rows[i]["County"].ToString() != "" &&
                            data.Rows[i]["Borrower_First"].ToString() != "" &&
                            data.Rows[i]["Borrower_Last"].ToString() != "" &&
                            data.Rows[i]["Task"].ToString() != ""
                            )
                        {
                            Date = data.Rows[i]["Date"].ToString();
                            Orderno = data.Rows[i]["Order Number"].ToString();
                            Client = data.Rows[i]["Client"].ToString();
                            Subprocess = data.Rows[i]["Sub process"].ToString();
                            Ordertype = data.Rows[i]["Order Type"].ToString();
                            State = data.Rows[i]["State"].ToString();
                            County = data.Rows[i]["County"].ToString();
                            Task = data.Rows[i]["Task"].ToString();
                            Borrower_Firstname = data.Rows[i]["Borrower_First"].ToString();
                            Borrower_Lastname = data.Rows[i]["Borrower_Last"].ToString();
                            Address = data.Rows[i]["Property Address"].ToString();
                            value = 0;

                            grd_OrderEntry.Rows.Add();
                            grd_OrderEntry.Rows[i].Cells[0].Value = i+1;
                            grd_OrderEntry.Rows[i].Cells[1].Value = data.Rows[i]["Order Number"].ToString();
                            grd_OrderEntry.Rows[i].Cells[2].Value = data.Rows[i]["APN"].ToString();
                            grd_OrderEntry.Rows[i].Cells[3].Value = data.Rows[i]["Order Type"].ToString();
                            grd_OrderEntry.Rows[i].Cells[4].Value = data.Rows[i]["Client"].ToString();
                            grd_OrderEntry.Rows[i].Cells[5].Value = data.Rows[i]["Sub process"].ToString();

                            grd_OrderEntry.Rows[i].Cells[6].Value = data.Rows[i]["Client Order Ref"].ToString();
                            grd_OrderEntry.Rows[i].Cells[7].Value = data.Rows[i]["Task"].ToString();
                            grd_OrderEntry.Rows[i].Cells[8].Value = data.Rows[i]["Borrower_First"].ToString();
                            grd_OrderEntry.Rows[i].Cells[9].Value = data.Rows[i]["Borrower_Last"].ToString();

                            grd_OrderEntry.Rows[i].Cells[10].Value = data.Rows[i]["Property Address"].ToString();
                            grd_OrderEntry.Rows[i].Cells[11].Value = data.Rows[i]["County"].ToString();
                            grd_OrderEntry.Rows[i].Cells[12].Value = data.Rows[i]["State"].ToString();

                            grd_OrderEntry.Rows[i].Cells[13].Value = data.Rows[i]["Date"].ToString();
                            grd_OrderEntry.Rows[i].Cells[14].Value = data.Rows[i]["Time"].ToString();
                            grd_OrderEntry.Rows[i].Cells[15].Value = data.Rows[i]["Comments"].ToString();

                            grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.White;

                            //Change Date into DateTime
                            DateTime Received_date;
                            string date_received;
                           
                            if (Date != "")
                            {
                                try
                                {
                                    Received_date = Convert.ToDateTime(Date.ToString());
                                    date_received = Received_date.ToString("MM/dd/yyyy");
                                }
                                catch
                                {
                                    grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    grd_OrderEntry.Rows[i].Cells[13].Style.ForeColor = Color.White;
                                }
                                
                            }
                            else
                            {
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_OrderEntry.Rows[i].Cells[13].Style.ForeColor = Color.White;
                            }


                            //Duplication of Records
                            for (int j = 0; j < i; j++)
                            {
                                string Order_no = data.Rows[j]["Order Number"].ToString();
                                if (Orderno == Order_no)
                                {
                                    value = 1;
                                    break;
                                }
                                else
                                {
                                    value = 0;
                                }

                            }
                            if (value == 1)
                            {
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_OrderEntry.Rows[i].Cells[1].Style.ForeColor = Color.White;
                            }


                            //OrderType Taken and Add
                            Hashtable htType = new Hashtable();
                            DataTable dtType = new System.Data.DataTable();
                            htType.Add("@Trans", "GETType");
                            htType.Add("@Order_Type", Ordertype);
                            dtType = dataaccess.ExecuteSP("Sp_Order_Get_Details", htType);
                            if (dtType.Rows.Count > 0)
                            {
                                Ordertypeid = int.Parse(dtType.Rows[0]["Order_Type_ID"].ToString());
                            }
                            else
                            {
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_OrderEntry.Rows[i].Cells[3].Style.ForeColor = Color.White;
                            }

                            //Get State
                            Hashtable htbarowerstate = new Hashtable();
                            DataTable dtbarrowerstate = new System.Data.DataTable();
                            htbarowerstate.Add("@Trans", "GETSTATE_BY_ABR");
                            htbarowerstate.Add("@state_name", State);
                            dtbarrowerstate = dataaccess.ExecuteSP("Sp_Order_Get_Details", htbarowerstate);
                            if (dtbarrowerstate.Rows.Count > 0)
                            {

                                Stateid = int.Parse(dtbarrowerstate.Rows[0]["State_ID"].ToString());
                            }
                            else
                            {
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_OrderEntry.Rows[i].Cells[11].Style.ForeColor = Color.White;
                            }


                            //get County
                            Hashtable htBarcounty = new Hashtable();
                            DataTable dtbarcounty = new System.Data.DataTable();
                            htBarcounty.Add("@Trans", "GET_COUNTY");
                            htBarcounty.Add("@state_Id", Stateid);
                            htBarcounty.Add("@County_Name", County);
                            dtbarcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty);
                            if (dtbarcounty.Rows.Count > 0)
                            {
                                Countyid = int.Parse(dtbarcounty.Rows[0]["County_ID"].ToString());
                            }
                            else
                            {
                                Hashtable htcounty = new Hashtable();
                                DataTable dtcounty = new System.Data.DataTable();

                                htcounty.Add("@Trans", "ADDCOUNTY");
                                htcounty.Add("@state_Id", Stateid);
                                htcounty.Add("@County", County);
                                dtcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htcounty);

                                Hashtable htBarcounty1 = new Hashtable();
                                DataTable dtbarcounty1 = new System.Data.DataTable();
                                htBarcounty1.Add("@Trans", "GETCOUNTY");
                                htBarcounty1.Add("@State", Stateid);
                                htBarcounty1.Add("@County_Name", County);
                                dtbarcounty1 = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty1);

                                Countyid = int.Parse(dtbarcounty1.Rows[0]["County_ID"].ToString());
                            }


                            //get_Order_Status
                            Hashtable htorderstatus = new Hashtable();
                            DataTable dtorderstatus = new System.Data.DataTable();
                            htorderstatus.Add("@Trans", "GET_ORDER_STATUS");
                            htorderstatus.Add("@Order_Status", Task);
                            dtorderstatus = dataaccess.ExecuteSP("Sp_Order_Get_Details", htorderstatus);
                            if (dtorderstatus.Rows.Count > 0)
                            {
                                Taskid = int.Parse(dtorderstatus.Rows[0]["Order_Status_ID"].ToString());
                            }
                            else
                            {
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_OrderEntry.Rows[i].Cells[7].Style.ForeColor = Color.White;
                            }



                            //get_Client
                            Clientid = 0; 
                            Hashtable ht_Client = new Hashtable();
                            DataTable dt_Client = new System.Data.DataTable();
                            ht_Client.Add("@Trans", "SELECT_Client_ID_Name");
                            ht_Client.Add("@Client_Name", Client);
                            dt_Client = dataaccess.ExecuteSP("Sp_Client", ht_Client);
                            if (dt_Client.Rows.Count > 0)
                            {
                                Clientid = int.Parse(dt_Client.Rows[0]["Client_Id"].ToString());
                            }
                            else
                            {
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_OrderEntry.Rows[i].Cells[4].Style.ForeColor = Color.White;
                            }

                            //get_Subprocess
                            Hashtable ht_subprocess = new Hashtable();
                            DataTable dt_subprocess = new System.Data.DataTable();
                            ht_subprocess.Add("@Trans", "SUBPROCESSID");
                            ht_subprocess.Add("@Client_Id", Clientid);
                            ht_subprocess.Add("@Sub_ProcessName", Subprocess);
                            dt_subprocess = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht_subprocess);
                            if (dt_subprocess.Rows.Count > 0)
                            {
                                Subprocessid = int.Parse(dt_subprocess.Rows[0]["Subprocess_Id"].ToString());
                            }
                            else
                            {
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_OrderEntry.Rows[i].Cells[5].Style.ForeColor = Color.White;
                            }

                            //check order number exist
                            Hashtable htcheck = new Hashtable();
                            DataTable dtcheck = new System.Data.DataTable();
                            htcheck.Add("@Trans", "ORDER_NUMBER");
                            htcheck.Add("@Client_Order_Number", Orderno);
                            dtcheck = dataaccess.ExecuteSP("Sp_Order", htcheck);
                            //Check_order = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            if (dtcheck.Rows.Count > 0)
                            {
                                int Orderid = int.Parse(dtcheck.Rows[0]["Order_ID"].ToString());
                                grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void btn_SampleFormat_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"c:\OMS_Import\Order Import.xlsx";
            File.Copy(Environment.CurrentDirectory + "\\Order Import.xlsx", temppath, true);

            Process.Start(temppath);  
        }

        private void btn_ErrorRows_Click(object sender, EventArgs e)
        {
            string sno,date, time, ordernumber, client, subpro, clientref, ordertype, address, state, county, borrower, comments, task,apn;
            for (int i = 0; i < grd_OrderEntry.Rows.Count; i++)
            {


                if (grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {

                    sno=grd_OrderEntry.Rows[i].Cells[0].Value.ToString();
                    ordernumber = grd_OrderEntry.Rows[i].Cells[1].Value.ToString();
                    apn = grd_OrderEntry.Rows[i].Cells[2].Value.ToString();
                    ordertype = grd_OrderEntry.Rows[i].Cells[3].Value.ToString();
                    client = grd_OrderEntry.Rows[i].Cells[4].Value.ToString();

                    subpro = grd_OrderEntry.Rows[i].Cells[5].Value.ToString();
                    clientref = grd_OrderEntry.Rows[i].Cells[6].Value.ToString();
                    task = grd_OrderEntry.Rows[i].Cells[7].Value.ToString();

                    borrower = grd_OrderEntry.Rows[i].Cells[8].Value.ToString();
                    address = grd_OrderEntry.Rows[i].Cells[9].Value.ToString();
                    county = grd_OrderEntry.Rows[i].Cells[10].Value.ToString();

                    state = grd_OrderEntry.Rows[i].Cells[11].Value.ToString();
                    date = grd_OrderEntry.Rows[i].Cells[12].Value.ToString();
                    time = grd_OrderEntry.Rows[i].Cells[13].Value.ToString();
                    comments = grd_OrderEntry.Rows[i].Cells[14].Value.ToString();

                 
                    grd_DupOrderEntry.Rows.Add();
                    int j = grd_DupOrderEntry.Rows.Count - 1;
                    grd_DupOrderEntry.Rows[j].Cells[0].Value = "Submit";
                    grd_DupOrderEntry.Rows[j].Cells[1].Value = "Delete";
                    grd_DupOrderEntry.Rows[j].Cells[2].Value = sno;
                    grd_DupOrderEntry.Rows[j].Cells[3].Value = ordernumber;
                    if (grd_OrderEntry.Rows[i].Cells[1].Style.ForeColor == Color.White)
                    {
                        grd_DupOrderEntry.Rows[j].Cells[3].Style.ForeColor = Color.White;
                    }
                    grd_DupOrderEntry.Rows[j].Cells[4].Value = apn;
                    grd_DupOrderEntry.Rows[j].Cells[5].Value = ordertype;
                    if (grd_OrderEntry.Rows[i].Cells[3].Style.ForeColor == Color.White)
                    {
                        grd_DupOrderEntry.Rows[j].Cells[5].Style.ForeColor = Color.White;
                    }
                    grd_DupOrderEntry.Rows[j].Cells[6].Value = client;
                    if (grd_OrderEntry.Rows[i].Cells[4].Style.ForeColor == Color.White)
                    {
                        grd_DupOrderEntry.Rows[j].Cells[6].Style.ForeColor = Color.White;
                    }
                    grd_DupOrderEntry.Rows[j].Cells[7].Value = subpro;
                    if (grd_OrderEntry.Rows[i].Cells[5].Style.ForeColor == Color.White)
                    {
                        grd_DupOrderEntry.Rows[j].Cells[7].Style.ForeColor = Color.White;
                    }
                    grd_DupOrderEntry.Rows[j].Cells[8].Value = clientref;
                    grd_DupOrderEntry.Rows[j].Cells[9].Value = task;
                    if (grd_OrderEntry.Rows[i].Cells[7].Style.ForeColor == Color.White)
                    {
                        grd_DupOrderEntry.Rows[j].Cells[9].Style.ForeColor = Color.White;
                    }
                    grd_DupOrderEntry.Rows[j].Cells[10].Value = borrower;
                    grd_DupOrderEntry.Rows[j].Cells[11].Value = address;
                    grd_DupOrderEntry.Rows[j].Cells[12].Value = county;
                    grd_DupOrderEntry.Rows[j].Cells[13].Value = state;
                    if (grd_OrderEntry.Rows[i].Cells[11].Style.ForeColor == Color.White)
                    {
                        grd_DupOrderEntry.Rows[j].Cells[13].Style.ForeColor = Color.White;
                    }
                    grd_DupOrderEntry.Rows[j].Cells[14].Value = date;
                    if (grd_OrderEntry.Rows[i].Cells[12].Style.ForeColor == Color.White)
                    {
                        grd_DupOrderEntry.Rows[j].Cells[14].Style.ForeColor = Color.White;
                    }
                    grd_DupOrderEntry.Rows[j].Cells[15].Value = time;
                    grd_DupOrderEntry.Rows[j].Cells[16].Value = comments;

                    grd_DupOrderEntry.Rows[j].DefaultCellStyle.BackColor = Color.Red;

                    grd_OrderEntry.Rows.RemoveAt(i);

                    i = i - 1;
                    

                    

                }
                else
                {
                    grd_OrderEntry.DefaultCellStyle.BackColor = Color.White;
                }


            }
        }

        private void btn_NonAddedRows_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_OrderEntry.Rows.Count; i++)
            {
                if (grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {

                    //grd_Abstracter.Rows.Add(i);
                    grd_OrderEntry.Rows.RemoveAt(i);

                    i = i - 1;
                }
                else if (grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    grd_OrderEntry.Rows.RemoveAt(i);
                    i = i - 1;
                }

                else
                {
                    grd_OrderEntry.DefaultCellStyle.BackColor = Color.White;
                }


            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            
            checkvalue = 0;
            int Entervalue = 0;
            int OrderInsert = 0;
            for (int i = 0; i < grd_OrderEntry.Rows.Count; i++)
            {
                if (grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor != Color.White)
                {
                    Entervalue = 1;
                }
            }
            if (Entervalue == 1)
            {
                MessageBox.Show("Check the Errors and Already Existed Items in Excel");
            }
            if (Entervalue != 1)
            {
                for (int i = 0; i < grd_OrderEntry.Rows.Count; i++)
                {
                    if (grd_OrderEntry.Rows[i].DefaultCellStyle.BackColor != System.Drawing.Color.Cyan)
                    {
                        //grd_OrderEntry.Rows.Add();
                        //grd_OrderEntry.Rows[i].Cells[0].Value = i + 1;
                        string Ordernumber = grd_OrderEntry.Rows[i].Cells[1].Value.ToString();
                        string APN = grd_OrderEntry.Rows[i].Cells[2].Value.ToString();
                        string OrderType = grd_OrderEntry.Rows[i].Cells[3].Value.ToString();
                        string Client = grd_OrderEntry.Rows[i].Cells[4].Value.ToString();

                        string SubProcess = grd_OrderEntry.Rows[i].Cells[5].Value.ToString();
                        string clientref = grd_OrderEntry.Rows[i].Cells[6].Value.ToString();
                        string Task = grd_OrderEntry.Rows[i].Cells[7].Value.ToString();
                        string Borrower = grd_OrderEntry.Rows[i].Cells[8].Value.ToString();

                        string Address = grd_OrderEntry.Rows[i].Cells[9].Value.ToString();
                        string County = grd_OrderEntry.Rows[i].Cells[10].Value.ToString();
                        string State = grd_OrderEntry.Rows[i].Cells[11].Value.ToString();

                        string Date = grd_OrderEntry.Rows[i].Cells[12].Value.ToString();
                        string Time = grd_OrderEntry.Rows[i].Cells[13].Value.ToString();
                        string Comments = grd_OrderEntry.Rows[i].Cells[14].Value.ToString();


                        //Change Date into DateTime
                        DateTime Received_date;
                        string date_received;
                        if (Date != "")
                        {
                            Received_date = Convert.ToDateTime(Date.ToString());
                            date_received = Received_date.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            Received_date = Convert.ToDateTime("01/01/1990");
                            date_received = Received_date.ToString("MM/dd/yyyy");
                        }

                        //get_max order number
                        Hashtable htmax = new Hashtable();
                        DataTable dtmax = new System.Data.DataTable();
                        htmax.Add("@Trans", "MAX_ORDER_NO");
                        dtmax = dataaccess.ExecuteSP("Sp_Order", htmax);
                        if (dtmax.Rows.Count > 0)
                        {
                            MAX_ORDER_NUMBER = "DRN" + "-" + dtmax.Rows[0]["ORDER_NUMBER"].ToString();
                        }


                        //OrderType Taken and Add
                        Hashtable htType = new Hashtable();
                        DataTable dtType = new System.Data.DataTable();
                        htType.Add("@Trans", "GETType");
                        htType.Add("@Order_Type", Ordertype);
                        dtType = dataaccess.ExecuteSP("Sp_Order_Get_Details", htType);
                        if (dtType.Rows.Count > 0)
                        {
                            Ordertype_id = int.Parse(dtType.Rows[0]["Order_Type_ID"].ToString());
                        }

                        //Get State name by ABR
                        Hashtable htbarowerstate = new Hashtable();
                        DataTable dtbarrowerstate = new System.Data.DataTable();
                        htbarowerstate.Add("@Trans", "GETSTATE_BY_ABR");
                        htbarowerstate.Add("@state_name", State);
                        dtbarrowerstate = dataaccess.ExecuteSP("Sp_Order_Get_Details", htbarowerstate);
                        if (dtbarrowerstate.Rows.Count > 0)
                        {
                            State_id = int.Parse(dtbarrowerstate.Rows[0]["State_ID"].ToString());
                        }

                        //get County
                        Hashtable htBarcounty = new Hashtable();
                        DataTable dtbarcounty = new System.Data.DataTable();
                        htBarcounty.Add("@Trans", "GET_COUNTY");
                        htBarcounty.Add("@state_Id", Stateid);
                        htBarcounty.Add("@County_Name", County);
                        dtbarcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty);
                        if (dtbarcounty.Rows.Count > 0)
                        {
                            County_id = int.Parse(dtbarcounty.Rows[0]["County_ID"].ToString());
                        }

                        Hashtable htcounty = new Hashtable();
                        DataTable dtcounty = new DataTable();
                        htcounty.Add("@Trans", "GET_COUNTY_TYPE");
                        htcounty.Add("@County", County_id);
                        dtcounty = dataaccess.ExecuteSP("Sp_Order", htcounty);
                        if (dtcounty.Rows.Count > 0)
                        {

                            Assign_County_Type = dtcounty.Rows[0]["County_Type"].ToString();

                        }
                        else
                        {


                        }
                        if (Assign_County_Type == "TIER 1")
                        {

                            Assign_County_Type_ID = 1;

                        }
                        else if (Assign_County_Type == "TIER 2")
                        {

                            Assign_County_Type_ID = 2;
                        }

                        //get_Order_Status
                        Hashtable htorderstatus = new Hashtable();
                        DataTable dtorderstatus = new System.Data.DataTable();
                        htorderstatus.Add("@Trans", "GET_ORDER_STATUS");
                        htorderstatus.Add("@Order_Status", Task);
                        dtorderstatus = dataaccess.ExecuteSP("Sp_Order_Get_Details", htorderstatus);
                        if (dtorderstatus.Rows.Count > 0)
                        {
                            Task_id = int.Parse(dtorderstatus.Rows[0]["Order_Status_ID"].ToString());
                        }


                        //get_Client
                        //Client_id = 0; 
                        Hashtable ht_Client = new Hashtable();
                        DataTable dt_Client = new System.Data.DataTable();
                        ht_Client.Add("@Trans", "SELECT_Client_ID_Name");
                        ht_Client.Add("@Client_Name", Client);
                        dt_Client = dataaccess.ExecuteSP("Sp_Client", ht_Client);
                        if (dt_Client.Rows.Count > 0)
                        {
                            Client_id = int.Parse(dt_Client.Rows[0]["Client_Id"].ToString());
                        }


                        //get_Subprocess
                        Hashtable ht_subprocess = new Hashtable();
                        DataTable dt_subprocess = new System.Data.DataTable();
                        ht_subprocess.Add("@Trans", "SUBPROCESSID");
                        ht_subprocess.Add("@Client_Id", Client_id);
                        ht_subprocess.Add("@Sub_ProcessName", Subprocess);
                        dt_subprocess = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht_subprocess);
                        if (dt_subprocess.Rows.Count > 0)
                        {
                            Subprocessid = int.Parse(dt_subprocess.Rows[0]["Subprocess_Id"].ToString());
                        }

                        //check order number exist
                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new System.Data.DataTable();

                        htcheck.Add("@Trans", "CHECK_ORDER_NUMBER");
                        htcheck.Add("@Client_Order_Number", Ordernumber);
                        //htcheck.Add("@State", State_id);
                        // htcheck.Add("@Borrower_Name", Borrower);
                        // htcheck.Add("@county", County_id);
                        // htcheck.Add("@Address", Address);
                        dtcheck = dataaccess.ExecuteSP("Sp_Order", htcheck);
                        Checkorder = int.Parse(dtcheck.Rows[0]["count"].ToString());
                        if (Checkorder <= 0)
                        {
                            OrderInsert = 1;
                            Hashtable htinsertrec = new Hashtable();
                            DataTable dtinsertrec = new System.Data.DataTable();


                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Sub_ProcessId", Subprocessid);

                            htinsertrec.Add("@Placed_By", User_id);
                            htinsertrec.Add("@Order_Type", Ordertype_id);
                            htinsertrec.Add("@Order_Number", MAX_ORDER_NUMBER);
                            htinsertrec.Add("@APN", APN);
                            htinsertrec.Add("@Client_Order_Number", Ordernumber);
                            htinsertrec.Add("@Order_Status", Taskid);
                            htinsertrec.Add("@Client_Order_Ref", clientref);
                            //htinsertrec.Add("@Search_Type", lblSearch_Type.Text);
                            htinsertrec.Add("@Order_Progress", 8);


                            htinsertrec.Add("@Zip", 0);
                            htinsertrec.Add("@Date", date_received);
                            htinsertrec.Add("@Borrower_Name", Borrower);
                            htinsertrec.Add("@Address", Address);

                            htinsertrec.Add("@County", County_id);
                            htinsertrec.Add("@State", State_id);
                            htinsertrec.Add("@Recived_Date", date_received);
                            htinsertrec.Add("@Recived_Time", Time);
                            htinsertrec.Add("@Notes", Comments);
                            htinsertrec.Add("@Order_Assign_Type", Assign_County_Type_ID);

                            htinsertrec.Add("@Inserted_By", User_id);
                            htinsertrec.Add("@Inserted_date", DateTime.Now);
                            htinsertrec.Add("@status", "True");

                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order", htinsertrec);
                        }
                    }
                    if (OrderInsert == 1)
                    {
                        MessageBox.Show("Order Imported Successfully");
                        grd_OrderEntry.Rows.Clear();
                    }
                }




            }
        }

        private void Order_Import_Load(object sender, EventArgs e)
        {
            Hashtable htcheckorder = new Hashtable();
            DataTable dtcheckorder=new DataTable();
            htcheckorder.Add("@Trans","SELECT_ORDERS");
            dtcheckorder = dataaccess.ExecuteSP("Sp_Order", htcheckorder);
            if (dtcheckorder.Rows.Count > 0)
            {
                btn_NonAddedRows.Enabled = true;
            }
            else
            {
                btn_NonAddedRows.Enabled = false;
            }

        }

        private void grd_DupOrderEntry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int datevalue, statevalue, countyvalue, clientvalue, taskvalue, ordertypevalue, subprovalue;
            datevalue = 0; statevalue = 0; countyvalue = 0; clientvalue = 0; taskvalue = 0; ordertypevalue = 0; subprovalue = 0;
            if (e.ColumnIndex == 0)
            {
                int i=e.RowIndex;
                    //Ordernumber duplication
                string sno = grd_DupOrderEntry.Rows[i].Cells[2].Value.ToString();
                    string orderno = grd_DupOrderEntry.Rows[i].Cells[3].Value.ToString();
                    string apn = grd_DupOrderEntry.Rows[i].Cells[4].Value.ToString();
                    string ordertype = grd_DupOrderEntry.Rows[i].Cells[5].Value.ToString();
                    string client = grd_DupOrderEntry.Rows[i].Cells[6].Value.ToString();
                    string subpro = grd_DupOrderEntry.Rows[i].Cells[7].Value.ToString();

                    string clientref = grd_DupOrderEntry.Rows[i].Cells[8].Value.ToString();
                    string task = grd_DupOrderEntry.Rows[i].Cells[9].Value.ToString();
                    string Borrower = grd_DupOrderEntry.Rows[i].Cells[10].Value.ToString();
                    string Address = grd_DupOrderEntry.Rows[i].Cells[11].Value.ToString();

                    string county = grd_DupOrderEntry.Rows[i].Cells[12].Value.ToString();
                    string state = grd_DupOrderEntry.Rows[i].Cells[13].Value.ToString();
                    string date = grd_DupOrderEntry.Rows[i].Cells[14].Value.ToString();

                    string time = grd_DupOrderEntry.Rows[i].Cells[15].Value.ToString();
                    string comments = grd_DupOrderEntry.Rows[i].Cells[16].Value.ToString();
                   
                   
                    for (int j = 0; j < grd_OrderEntry.Rows.Count; j++)
                    {
                        string Order_no = grd_OrderEntry.Rows[i].Cells[1].Value.ToString();
                        if (Orderno == Order_no)
                        {
                            value = 1;
                            break;
                        }
                        else
                        {
                            value = 0;
                        }
                    }
                    if (value == 1)
                    {
                        MessageBox.Show("*" + orderno + "*" +"Not updated");
                    }

                    //Change Date into DateTime
                    if (date != "")
                    {
                        try
                        {
                            DateTime Received_date = Convert.ToDateTime(date.ToString());
                            string date_received = Received_date.ToString("MM/dd/yyyy");
                            datevalue = 1;
                        }
                        catch
                        {
                            MessageBox.Show("*" + date + "*" + "Date Format Incorrect");
                        }
                    }
                    else
                    {
                        MessageBox.Show("*" + date + "*" + "Not updated");
                    }
                    

                    //OrderType Taken and Add
                    Hashtable htType = new Hashtable();
                    DataTable dtType = new System.Data.DataTable();
                    htType.Add("@Trans", "GETType");
                    htType.Add("@Order_Type", ordertype);
                    dtType = dataaccess.ExecuteSP("Sp_Order_Get_Details", htType);
                    if (dtType.Rows.Count <= 0)
                    {
                        MessageBox.Show("*" + ordertype + "*" + "Not updated");
                    }
                    else
                    {
                        ordertypevalue = 1;
                    }

                    //Get State name by ABR
                    Hashtable htbarowerstate = new Hashtable();
                    DataTable dtbarrowerstate = new System.Data.DataTable();
                    htbarowerstate.Add("@Trans", "GETSTATE_BY_ABR");
                    htbarowerstate.Add("@state_name", state);
                    dtbarrowerstate = dataaccess.ExecuteSP("Sp_Order_Get_Details", htbarowerstate);
                    if (dtbarrowerstate.Rows.Count <= 0)
                    {
                        MessageBox.Show("*" + state + "*" + "Not updated");

                    }
                    else
                    {
                        Stateid = int.Parse(dtbarrowerstate.Rows[0]["State_ID"].ToString());
                        statevalue = 1;
                    }

                    //get County
                    Hashtable htBarcounty = new Hashtable();
                    DataTable dtbarcounty = new System.Data.DataTable();
                    htBarcounty.Add("@Trans", "GETCOUNTY");
                    htBarcounty.Add("@State", Stateid);
                    htBarcounty.Add("@County_Name", county);
                    dtbarcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty);
                    if (dtbarcounty.Rows.Count <= 0)
                    {
                        MessageBox.Show("*" + county + "*" + "Not updated");
                    }
                    else
                    {
                        countyvalue = 1;
                    }
                    


                    //get_Order_Status
                    Hashtable htorderstatus = new Hashtable();
                    DataTable dtorderstatus = new System.Data.DataTable();
                    htorderstatus.Add("@Trans", "GET_ORDER_STATUS");
                    htorderstatus.Add("@Order_Status", task);
                    dtorderstatus = dataaccess.ExecuteSP("Sp_Order_Get_Details", htorderstatus);
                    if (dtorderstatus.Rows.Count <= 0)
                    {
                        MessageBox.Show("*" + task + "*" + "Not updated");
                    }
                    else
                    {
                        taskvalue = 1;
                    }



                    //get_Client
                    //Clientid = 0;
                    Hashtable ht_Client = new Hashtable();
                    DataTable dt_Client = new System.Data.DataTable();
                    ht_Client.Add("@Trans", "SELECT_Client_ID_Name");
                    ht_Client.Add("@Client_Name", client);
                    dt_Client = dataaccess.ExecuteSP("Sp_Client", ht_Client);
                    if (dt_Client.Rows.Count <= 0)
                    {
                        MessageBox.Show("*" + client + "*" + "Not updated");
                    }
                    else
                    {
                        Clientid = int.Parse(dt_Client.Rows[0]["Client_Id"].ToString());
                        clientvalue = 1;
                    }
                    

                    //get_Subprocess
                    Hashtable ht_subprocess = new Hashtable();
                    DataTable dt_subprocess = new System.Data.DataTable();
                    ht_subprocess.Add("@Trans", "SUBPROCESSID");
                    ht_subprocess.Add("@Client_Id", Clientid);
                    ht_subprocess.Add("@Sub_ProcessName", subpro);
                    dt_subprocess = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht_subprocess);
                    if (dt_subprocess.Rows.Count <= 0)
                    {
                        MessageBox.Show("*" + subpro + "*" + "Not updated");
                    }
                    else
                    {
                        subprovalue = 1;
                    }
                    if(datevalue==1 && value==0 && ordertypevalue==1 &&  statevalue==1 && countyvalue==1 && clientvalue==1 && subprovalue==1 && taskvalue==1)
                    {
                        grd_OrderEntry.Rows.Add();
                        int j = grd_OrderEntry.Rows.Count - 1;
                        grd_OrderEntry.Rows[j].Cells[0].Value = sno;
                        grd_OrderEntry.Rows[j].Cells[1].Value = orderno;
                        grd_OrderEntry.Rows[j].Cells[2].Value = apn;
                        grd_OrderEntry.Rows[j].Cells[3].Value = ordertype;
                        grd_OrderEntry.Rows[j].Cells[4].Value = client;
                        grd_OrderEntry.Rows[j].Cells[5].Value = subpro;

                        grd_OrderEntry.Rows[j].Cells[6].Value = clientref;
                        grd_OrderEntry.Rows[j].Cells[7].Value = task;
                        grd_OrderEntry.Rows[j].Cells[8].Value = Borrower;
                        grd_OrderEntry.Rows[j].Cells[9].Value = Address;

                        grd_OrderEntry.Rows[j].Cells[10].Value = county;
                        grd_OrderEntry.Rows[j].Cells[11].Value = state;
                        grd_OrderEntry.Rows[j].Cells[12].Value = date;

                        grd_OrderEntry.Rows[j].Cells[13].Value = time;
                        grd_OrderEntry.Rows[j].Cells[14].Value = comments;
                        grd_OrderEntry.Rows[j].DefaultCellStyle.BackColor = Color.White;
                        MessageBox.Show("*" + orderno + "*" + " Corrected Data Updated Successfully");
                        grd_DupOrderEntry.Rows.RemoveAt(e.RowIndex);
                        
                    }
               
            }
            else if (e.ColumnIndex == 1)
            {
                grd_DupOrderEntry.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
