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
    public partial class Import_Orders : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string MAX_ORDER_NUMBER;
        int GET_CLIENT, GET_SUBPROCESS, GET_ORDER_STATUS, GET_ORDER_TYPE, GET_CLIENT_STATE, GET_BARROWER_STATE, GET_CLIENT_COUNTY, GET_BARROWER_COUNTY, CHECK_ORDER;
        int BRANCH_ID;
        int client_Id, Subprocess_id, Assign_County_Type_ID;
        int Count;
        int userid;
        string Assign_County_Type;
        DateTime prior_date;
        string date_prior;
        object Entered_OrderId;
        int value = 0;
        int Vendor_Total_No_Of_Order_Recived, Vendor_No_Of_Order_For_each_Vendor, Vendor_Order_capacity;
        decimal Vendor_Order_Percentage, Vendor_Balance_Percentage, Total_Vendor_Balance_Percentage, Total_Vendor_Alloacated_Percentage;
        int No_Of_Order_Assignd_for_Vendor, Vendor_Id;
        string Vendors_State_County, Vendors_Order_Type, Vendors_Client_Sub_Client;
        int Vendor_Order_Allocation_Count, Vendor_Id_For_Assign;
        string lblOrder_Type_For_Vendor;
        string Vend_date;
        int Assigning_To_Vendor;
        public Import_Orders(int User_id)
        {
            InitializeComponent();
            userid = User_id;
            Hashtable httruncate = new Hashtable();
            DataTable dttruncate = new System.Data.DataTable();
            httruncate.Add("@Trans", "TRUNCATE");
            dttruncate = dataaccess.ExecuteSP("Sp_Temp_Order", httruncate);
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"d:\";
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
            Gridview_Bind_Ordes();
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
                  //  grd_order.DataSource = data;
                    Hashtable httruncate = new Hashtable();
                    DataTable dttruncate = new System.Data.DataTable();
                    httruncate.Add("@Trans", "TRUNCATE");
                    dttruncate = dataaccess.ExecuteSP("Sp_Temp_Order", httruncate);
                    //for (int i = 0; i < data.Rows.Count; i++)
                    //{
                    //    if (data.Rows[i]["Order Type"].ToString() == "Update" || data.Rows[i]["Order Type"].ToString() == "UPDATE")
                    //    {
                    //        if (data.Rows[i]["Prior Effective Date"].ToString() == "")
                    //        {
                    //            value = 1;
                    //            MessageBox.Show("Enter Order Prior Date");
                    //        }
                    //        else
                    //        {
                    //            value = 0;
                    //        }
                    //    }

                        
                    //}
                    value = 0;
                    if (value != 1)
                    {
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            if (data.Rows[i]["Task"].ToString() != "" || data.Rows[i]["Task"].ToString() != null)
                            {

                                Hashtable htinsertrec = new Hashtable();
                                DataTable dtinsertrec = new System.Data.DataTable();
                                htinsertrec.Add("@Trans", "INSERT");
                                htinsertrec.Add("@Client_Order_Number", data.Rows[i]["Order Number"].ToString());
                                htinsertrec.Add("@Order_Type", data.Rows[i]["Order Type"].ToString());
                                htinsertrec.Add("@Client", data.Rows[i]["Client"].ToString());
                                htinsertrec.Add("@Order_Input", data.Rows[i]["Task"].ToString());
                                //htinsertrec.Add("@Search_Type", dt.Rows[i]["Search Type"].ToString());
                                htinsertrec.Add("@Client_Order_Ref", data.Rows[i]["Client Order Ref"].ToString());
                                htinsertrec.Add("@Sub_Client", data.Rows[i]["Sub process"].ToString());
                                htinsertrec.Add("@Borrower_Name", data.Rows[i]["Borrower_First"].ToString());
                                htinsertrec.Add("@Borrower_Name2", data.Rows[i]["Borrower_Last"].ToString());
                                htinsertrec.Add("@City", data.Rows[i]["City"].ToString());
                                htinsertrec.Add("@Zip", data.Rows[i]["ZIP_Code"].ToString());
                                htinsertrec.Add("@County", data.Rows[i]["County"].ToString());
                                htinsertrec.Add("@State", data.Rows[i]["State"].ToString());
                                htinsertrec.Add("@Received_Date", data.Rows[i]["Date"].ToString());
                                htinsertrec.Add("@Time", data.Rows[i]["Time"].ToString());
                                htinsertrec.Add("@Address", data.Rows[i]["Property Address"].ToString());
                                htinsertrec.Add("@Notes", data.Rows[i]["Comments"].ToString());

                                htinsertrec.Add("@APN", data.Rows[i]["APN"].ToString());

                                if (data.Rows[i]["Order Type"].ToString() == "Update" || data.Rows[i]["Order Type"].ToString() == "UPDATE")
                                {
                                    htinsertrec.Add("@Order_Prior_Date", data.Rows[i]["Prior Effective Date"].ToString());
                                }
                                else
                                {

                                }
                                dtinsertrec = dataaccess.ExecuteSP("Sp_Temp_Order", htinsertrec);


                            }
                            else
                            {
                                MessageBox.Show("Enter Task Properly");
                            }
                            // lbl_totalrows.Text = Convert.ToString(i);
                        }
                        value = 0;
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        protected void Gridview_Bind_Ordes()
        {

            Hashtable ht_Delete = new Hashtable();
            DataTable dt_delete = new System.Data.DataTable();
            ht_Delete.Add("@Trans", "DELETE_Duplicate");
            dt_delete = dataaccess.ExecuteSP("Sp_Temp_Order", ht_Delete);
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new System.Data.DataTable();
            htselect.Add("@Trans", "SELECT");
            dtselect = dataaccess.ExecuteSP("Sp_Temp_Order", htselect);
            //lbl_totalrows.Text = Convert.ToString(dtselect.Rows.Count);

            if (dtselect.Rows.Count > 0)
            {
              grd_order.Rows.Clear();
              for (int i = 0; i < dtselect.Rows.Count; i++)
              {
                  grd_order.Rows.Add();
                  grd_order.Rows[i].Cells[0].Value = i + 1;
                  grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                  grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Order_Type"].ToString();
                  grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Client"].ToString();
                  grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Sub_Client"].ToString();
                  grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();
                  grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Order_Input"].ToString();
                  grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["Borrower_Name"].ToString();
                  grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["Borrower_Name2"].ToString();
                  grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Address"].ToString();
                  grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["City"].ToString();
                  grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["County"].ToString();
                  grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["State"].ToString();
                  grd_order.Rows[i].Cells[13].Value = dtselect.Rows[i]["Zip"].ToString();
                  grd_order.Rows[i].Cells[14].Value = dtselect.Rows[i]["Received_Date"].ToString();
                  grd_order.Rows[i].Cells[15].Value = dtselect.Rows[i]["Time"].ToString();
                  grd_order.Rows[i].Cells[16].Value = dtselect.Rows[i]["Notes"].ToString();
                  grd_order.Rows[i].Cells[17].Value = dtselect.Rows[i]["Order_Prior_Date"].ToString();
                  grd_order.Rows[i].Cells[18].Value = dtselect.Rows[i]["Apn"].ToString();
              }
            }
            else
            {
            }
            //DuplicateRecords();
            for (int i = 0; i < grd_order.Rows.Count; i++)
            {
                string lbl_Clent_OrderNumber = grd_order.Rows[i].Cells[1].Value.ToString();
                Hashtable htDuplicate = new Hashtable();
                DataTable dtDuplicate = new System.Data.DataTable();
                htDuplicate.Add("@Trans", "REMOVE_DUPLICATE");
                dtDuplicate = dataaccess.ExecuteSP("Sp_Temp_Order", htDuplicate);
                for (int j = 0; j < dtDuplicate.Rows.Count; j++)
                {
                    if (lbl_Clent_OrderNumber == dtDuplicate.Rows[j]["Client_Order_Number"].ToString())
                    {
                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Tomato;
                    }
                }
                string lblSub_Client = grd_order.Rows[i].Cells[4].Value.ToString();
                string lblClient = grd_order.Rows[i].Cells[3].Value.ToString();
                string lblOrder_Type = grd_order.Rows[i].Cells[2].Value.ToString();
                Hashtable ht_Subprocess = new Hashtable();
                DataTable dt_Subprocess = new System.Data.DataTable();
                ht_Subprocess.Add("@Trans", "SELECTSUBPROCESSNAME");
                ht_Subprocess.Add("@Sub_ProcessName", lblSub_Client);
                dt_Subprocess = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht_Subprocess);
                if (dt_Subprocess.Rows.Count > 0)
                {
                    if (lblSub_Client.ToUpper() != dt_Subprocess.Rows[0]["Sub_ProcessName"].ToString() || lblClient.ToUpper() != dt_Subprocess.Rows[0]["Client_Name"].ToString())
                    {
                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                }

                Hashtable ht_Ordertype = new Hashtable();
                DataTable dt_Ordertype = new System.Data.DataTable();
                ht_Ordertype.Add("@Trans", "BIND_ORDERTYPE");
                ht_Ordertype.Add("@Order_Type", lblOrder_Type);
                dt_Ordertype = dataaccess.ExecuteSP("Sp_Order_Type", ht_Ordertype);
                if (dt_Ordertype.Rows.Count > 0)
                {
                    if (lblOrder_Type.ToUpper() != dt_Ordertype.Rows[0]["Order_Type"].ToString())
                    {
                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void btn_Duplicate_Click(object sender, EventArgs e)
        {
            Hashtable htdelrec = new Hashtable();
            DataTable dtdelrec = new System.Data.DataTable();

            htdelrec.Add("@Trans", "DELETE");
            dtdelrec = dataaccess.ExecuteSP("Sp_Temp_Order", htdelrec);
            Gridview_Bind_Ordes();
            DuplicateRecords();
        }
        protected void DuplicateRecords()
        {

            foreach (DataGridViewRow row in this.grd_order.Rows)
            {
                string lbl_clientordernumber = row.Cells[1].Value.ToString();

                string DuplicateClient_Number;
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new System.Data.DataTable();
                htselect.Add("@Trans", "GetDuplicate");
                dtselect = dataaccess.ExecuteSP("Sp_Temp_Order", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        Count = int.Parse(dtselect.Rows[i]["count"].ToString());

                        if (Count > 1)
                        {

                         //   btn_Duplicate.CssClass = "Windowbutton";
                            DuplicateClient_Number = dtselect.Rows[i]["Client_Order_Number"].ToString();

                            if (lbl_clientordernumber == DuplicateClient_Number)
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.Pink;

                            }
                        }
                        else
                        {

                            //btn_Duplicate.CssClass = "DropDown";
                        }

                    }


                }
                else
                {

                   // btn_Duplicate.CssClass = "DropDown";
                }

            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            int CountOrderInsert = 0;
            foreach (DataGridViewRow row in this.grd_order.Rows)
            {


                string lbl_Clent_OrderNumber = row.Cells[1].Value.ToString();
                string lblOrder_Type = row.Cells[2].Value.ToString();
                string lblClient = row.Cells[3].Value.ToString();
                string lblSub_Client = row.Cells[4].Value.ToString();
              
                string lblClient_Order_Ref = row.Cells[5].Value.ToString();
                string lblOrder_Input = row.Cells[6].Value.ToString();
                string lblBorrower_Name = row.Cells[7].Value.ToString();
                string lblBorrower2_Name = row.Cells[8].Value.ToString();
                string lblBarrower_Address = row.Cells[9].Value.ToString();
                string lblCity = row.Cells[10].Value.ToString();
                string lblCounty = row.Cells[11].Value.ToString();
                string lblState = row.Cells[12].Value.ToString();
                string lblZip = row.Cells[13].Value.ToString();
                string lblReceived_Date = row.Cells[14].Value.ToString();
                string lblTime = row.Cells[15].Value.ToString();
                string lblNotes = row.Cells[16].Value.ToString();
                string lblPriorDate = row.Cells[17].Value.ToString();
                string lblApn = row.Cells[18].Value.ToString();
               
             
         
              

                lblOrder_Type_For_Vendor = lblOrder_Type.ToString();
                Hashtable htType = new Hashtable();
                DataTable dtType = new System.Data.DataTable();
                htType.Add("@Trans", "GETType");
                htType.Add("@Order_Type", lblOrder_Type);
                dtType = dataaccess.ExecuteSP("Sp_Order_Get_Details", htType);
                if (dtType.Rows.Count <= 0)
                {
                    Hashtable htInsertType = new Hashtable();
                    DataTable dtInsertType = new System.Data.DataTable();
                    htInsertType.Add("@Trans", "INSERT");
                    htInsertType.Add("@Order_Type", lblOrder_Type);
                    htInsertType.Add("@Status", "True");
                    htInsertType.Add("@Inserted_By", userid);
                    htInsertType.Add("@Inserted_Date", DateTime.Now);

                    dtInsertType = dataaccess.ExecuteSP("Sp_Order_Type", htInsertType);
                }


                // Subprocess_id = int.Parse(dtSubprocess.Rows[0]["Subprocess_Id"].ToString());
                //Get Barrower State

                Hashtable htbarowerstate = new Hashtable();
                DataTable dtbarrowerstate = new System.Data.DataTable();
                htbarowerstate.Add("@Trans", "GETSTATE_BY_ABR");
                htbarowerstate.Add("@state_name", lblState);
                dtbarrowerstate = dataaccess.ExecuteSP("Sp_Order_Get_Details", htbarowerstate);
                if (dtbarrowerstate.Rows.Count > 0)
                {

                    GET_BARROWER_STATE = int.Parse(dtbarrowerstate.Rows[0]["State_ID"].ToString());
                }
                else
                {

                    GET_BARROWER_STATE = 0;
                }


                //get Barrower County


                //get County
                Hashtable htBarcounty = new Hashtable();
                DataTable dtbarcounty = new System.Data.DataTable();
                htBarcounty.Add("@Trans", "GETCOUNTY");
                htBarcounty.Add("@State", GET_BARROWER_STATE);
                htBarcounty.Add("@County_Name", lblCounty);
                dtbarcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty);
                if (dtbarcounty.Rows.Count > 0)
                {

                    GET_BARROWER_COUNTY = int.Parse(dtbarcounty.Rows[0]["County_ID"].ToString());
                }
                else
                {
                    Hashtable htcounty = new Hashtable();
                    DataTable dtcounty = new System.Data.DataTable();

                    htcounty.Add("@Trans", "ADDCOUNTY");
                    htcounty.Add("@state_Id", GET_BARROWER_STATE);
                    htcounty.Add("@County", lblCounty);
                    dtcounty = dataaccess.ExecuteSP("Sp_Order_Get_Details", htcounty);
                    Hashtable htBarcounty1 = new Hashtable();
                    DataTable dtbarcounty1 = new System.Data.DataTable();
                    htBarcounty1.Add("@Trans", "GETCOUNTY");
                    htBarcounty1.Add("@State", GET_BARROWER_STATE);
                    htBarcounty1.Add("@County_Name", lblCounty);
                    dtbarcounty1 = dataaccess.ExecuteSP("Sp_Order_Get_Details", htBarcounty1);
                    GET_BARROWER_COUNTY = int.Parse(dtbarcounty1.Rows[0]["County_ID"].ToString());

                }

                Hashtable htcountyid = new Hashtable();
                DataTable dtcountyid = new DataTable();
                htcountyid.Add("@Trans", "GET_COUNTY_TYPE");
                htcountyid.Add("@County", GET_BARROWER_COUNTY);
                dtcountyid = dataaccess.ExecuteSP("Sp_Order", htcountyid);
                if (dtcountyid.Rows.Count > 0)
                {

                    Assign_County_Type = dtcountyid.Rows[0]["County_Type"].ToString();

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


               
                //get_Order_Type
                Hashtable htorderType = new Hashtable();
                DataTable dtordertype = new System.Data.DataTable();
                htorderType.Add("@Trans", "GET_ORDER_TYPE");
                htorderType.Add("@Order_Type", lblOrder_Type);
                dtordertype = dataaccess.ExecuteSP("Sp_Order_Get_Details", htorderType);
                if (dtordertype.Rows.Count > 0)
                {

                    GET_ORDER_TYPE = int.Parse(dtordertype.Rows[0]["Order_Type_ID"].ToString());
                }
                else
                {
                    //  row.Focus();
                    MessageBox.Show("Order Type Does Not Match");
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Type Does Not Match')</script>", false);
                    break;
                }

                //get_Order_Status
                Hashtable htorderstatus = new Hashtable();
                DataTable dtorderstatus = new System.Data.DataTable();
                htorderstatus.Add("@Trans", "GET_ORDER_STATUS");
                htorderstatus.Add("@Order_Status", lblOrder_Input);
                dtorderstatus = dataaccess.ExecuteSP("Sp_Order_Get_Details", htorderstatus);
                if (dtorderstatus.Rows.Count > 0)
                {

                    GET_ORDER_STATUS = int.Parse(dtorderstatus.Rows[0]["Order_Status_ID"].ToString());
                }
                else
                {

                    GET_ORDER_STATUS = 0;
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
                //get_Client
                int Client_ID = 0;
                Hashtable ht_Client = new Hashtable();
                DataTable dt_Client = new System.Data.DataTable();
                ht_Client.Add("@Trans", "SELECT_Client_ID_Name");
                ht_Client.Add("@Client_Name", lblClient);
                dt_Client = dataaccess.ExecuteSP("Sp_Client", ht_Client);
                if (dt_Client.Rows.Count > 0)
                {
                    Client_ID = int.Parse(dt_Client.Rows[0]["Client_Id"].ToString());
                }
                GET_CLIENT = Client_ID;
              
                //get_Subprocess
                Int64 Subprocesnum;
                Int64 Subprocessno = 0;
                Int64 Client_Number;
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "MAXSUBPROCESSNUMBER");
                htselect.Add("@Client_Id", Client_ID);
                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    Subprocesnum = Convert.ToInt64(dtselect.Rows[0]["Subprocess_Number"].ToString());
                    Client_Number = Convert.ToInt64(dt_Client.Rows[0]["Client_Number"].ToString());
                    if (Subprocesnum == 1)
                    {
                        Int64 maxsubno;
                        maxsubno = Client_Number + Subprocesnum;
                        Subprocessno = maxsubno;
                    }
                    else
                    {
                        Subprocessno = Subprocesnum;
                    }
                }
                Hashtable ht_subprocess = new Hashtable();
                DataTable dt_subprocess = new System.Data.DataTable();
                ht_subprocess.Add("@Trans", "SELECTSUBPROCESSID");
                ht_subprocess.Add("@Sub_ProcessName", lblSub_Client);
                dt_subprocess = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht_subprocess);
                if (dt_subprocess.Rows.Count <= 0)
                {
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new DataTable();
                    DataTable dt = new DataTable();

                    //DateTime date = new DateTime();
                    //date = DateTime.Now;
                    //string dateeval = date.ToString("dd/MM/yyyy");


                    //23-01-2018
                    DateTime date = new DateTime();
                    DateTime time;
                  date= DateTime.Now;
                    string dateeval = date.ToString("MM/dd/yyyy");


                    htinsert.Add("@Trans", "INSERT");
                    htinsert.Add("@Client_Id", Client_ID);
                    htinsert.Add("@Sub_ProcessName", lblSub_Client);
                    htinsert.Add("@Subprocess_Number", Subprocessno);
                    htinsert.Add("@Inserted_By", userid);
                    htinsert.Add("@Inserted_date", date);
                    htinsert.Add("@status", "True");
                    dtinsert = dataaccess.ExecuteSP("Sp_Client_SubProcess", htinsert);
                    Hashtable ht_subprocess_Again = new Hashtable();
                    DataTable dt_subprocess_Again = new System.Data.DataTable();
                    ht_subprocess_Again.Add("@Trans", "SELECTSUBPROCESSID");
                    ht_subprocess_Again.Add("@Sub_ProcessName", lblSub_Client);
                    dt_subprocess_Again = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht_subprocess_Again);
                    GET_SUBPROCESS = int.Parse(dt_subprocess_Again.Rows[0]["Subprocess_Id"].ToString());
                }
                else
                {
                    GET_SUBPROCESS = int.Parse(dt_subprocess.Rows[0]["Subprocess_Id"].ToString());
                }

                //check order number exist
                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new System.Data.DataTable();

                htcheck.Add("@Trans", "CHECK_ORDER_NUMBER");
                htcheck.Add("@Client_Order_Number", lbl_Clent_OrderNumber);
                htcheck.Add("@State", GET_BARROWER_STATE);
                htcheck.Add("@Borrower_Name", lblBorrower_Name);
               
                htcheck.Add("@county", GET_BARROWER_COUNTY);
                htcheck.Add("@Address", lblBarrower_Address);
                dtcheck = dataaccess.ExecuteSP("Sp_Order", htcheck);


                if (dtcheck.Rows.Count > 0)
                {
                    CHECK_ORDER = int.Parse(dtcheck.Rows[0]["count"].ToString());

                }
                else
                {
                    CHECK_ORDER = 0;
                }
                DateTime Received_date;
                string date_received;

                if (lblPriorDate != "")
                {
                    prior_date = Convert.ToDateTime(lblPriorDate.ToString());
                    date_prior = prior_date.ToString("MM/dd/yyyy");
                }
                else
                {

                }
                if (lblReceived_Date != "")
                {
                    Received_date = Convert.ToDateTime(lblReceived_Date.ToString());
                    date_received = Received_date.ToString("MM/dd/yyyy");
                }
                else
                {
                    Received_date = Convert.ToDateTime("01/01/1990");
                    date_received = Received_date.ToString("MM/dd/yyyy");
                }



                CountOrderInsert = CountOrderInsert + 1;
                if (CHECK_ORDER == 0)
                {
                    Hashtable htinsertrec = new Hashtable();
                    DataTable dtinsertrec = new System.Data.DataTable();


                    htinsertrec.Add("@Trans", "INSERT");
                    htinsertrec.Add("@Sub_ProcessId", GET_SUBPROCESS);

                    htinsertrec.Add("@Placed_By", userid);
                    htinsertrec.Add("@Order_Type", GET_ORDER_TYPE);
                    htinsertrec.Add("@Order_Number", MAX_ORDER_NUMBER);
                    htinsertrec.Add("@Client_Order_Number", lbl_Clent_OrderNumber);


                    // This is for Tax Internal Client Orders Allocation

                    // This is commented because of no Directly moved to Tax Queue
                    ////==================================================
                    //Hashtable ht_check = new Hashtable();
                    //DataTable dt_check = new System.Data.DataTable();
                    //ht_check.Add("@Trans", "CHECK");
                    //ht_check.Add("@Client_Id", Client_ID);
                    //ht_check.Add("@Order_Type_Id", GET_ORDER_TYPE);
                    //ht_check.Add("@flag", "False");
                    //dt_check = dataaccess.ExecuteSP("Sp_Tax_Order_Movement_Client_Product_Type", ht_check);

                    //int Check_Count = 0;
                    //if (dt_check.Rows.Count > 0)
                    //{

                    //    Check_Count = int.Parse(dt_check.Rows[0]["COUNT"].ToString());
                    //}
                    //else
                    //{

                    //    Check_Count = 0;
                    //}


                    // For Non Tax Orders
                    if (GET_ORDER_TYPE != 70 && GET_ORDER_TYPE != 110)
                    {
                        if (Client_ID == 28 || Client_ID == 31)//  //Moving ABC & SPC Client Orders Into Re-Search Oreder Allocation Order Queue
                        {

                            htinsertrec.Add("@Order_Status", 25);

                        }
                        else
                        {

                            if (Assign_County_Type_ID == 2)
                            {
                                //moving abstrctshop client and abstractor order moving to research order queue
                                if (Assign_County_Type_ID == 2 && Client_ID == 31)
                                {

                                    htinsertrec.Add("@Order_Status", 25);
                                }
                                else
                                {
                                    htinsertrec.Add("@Order_Status", 17);

                                }
                            }
                            else
                            {


                                htinsertrec.Add("@Order_Status", GET_ORDER_STATUS);


                            }
                        }
                    }
                    else if (GET_ORDER_TYPE == 70 || GET_ORDER_TYPE == 110)
                    {
                      
                            htinsertrec.Add("@Order_Status", 21);

                        
                    }
                   



                    htinsertrec.Add("@Client_Order_Ref", lblClient_Order_Ref);
                    //htinsertrec.Add("@Search_Type", lblSearch_Type.Text);
                    htinsertrec.Add("@Order_Progress", 8);


                    htinsertrec.Add("@Zip", lblZip);
                    htinsertrec.Add("@City", lblCity);
                    htinsertrec.Add("@Date", date_received);
                    htinsertrec.Add("@Borrower_Name", lblBorrower_Name);
                    htinsertrec.Add("@Borrower_Name2", lblBorrower2_Name);
                    htinsertrec.Add("@Address", lblBarrower_Address);

                    htinsertrec.Add("@APN", lblApn);

                    htinsertrec.Add("@County", GET_BARROWER_COUNTY);
                    htinsertrec.Add("@State", GET_BARROWER_STATE);
                    htinsertrec.Add("@Recived_Date", date_received);
                    htinsertrec.Add("@Order_Prior_Date", date_prior);
                    htinsertrec.Add("@Recived_Time", lblTime);
                    htinsertrec.Add("@Notes", lblNotes);
                    htinsertrec.Add("@Inserted_By", userid);
                    htinsertrec.Add("@Inserted_date", DateTime.Now);
                    htinsertrec.Add("@status", "True");
                    htinsertrec.Add("@Order_Assign_Type", Assign_County_Type_ID);
                    //if (CHECK_ORDER == 0)
                    //{
                    Entered_OrderId = dataaccess.ExecuteSPForScalar("Sp_Order", htinsertrec);

                  

                    Hashtable ht_Order_History = new Hashtable();
                    DataTable dt_Order_History = new DataTable();
                    ht_Order_History.Add("@Trans", "INSERT");

                    ht_Order_History.Add("@Order_Id", Entered_OrderId);

                    // ht_Order_History.Add("@User_Id", );
                    //For Tax Orders
                    //if (Check_Count == 0)
                    //{
                        if (GET_ORDER_TYPE != 70 || GET_ORDER_TYPE != 110)
                        {
                            if (Client_ID == 28)
                            {
                                ht_Order_History.Add("@Status_Id", 25);
                            }
                            else
                            {
                                ht_Order_History.Add("@Status_Id", GET_ORDER_STATUS);

                            }
                        }
                    //}
                    //else if (Check_Count > 0)
                    //{ 
                    
                    //}

                    ht_Order_History.Add("@Progress_Id", 8);
                    ht_Order_History.Add("@Assigned_By", userid);

                    ht_Order_History.Add("@Modification_Type", "Order Create");

                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                    //if (Check_Count > 0)
                    //{
                    //    // This is for Tax Internal Client Orders Allocation

                    //    //==================================================
                    //    Insert_Internal_Tax_Order_Status();


                    //    Hashtable htupdate = new Hashtable();
                    //    System.Data.DataTable dtupdate = new System.Data.DataTable();
                    //    htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                    //    htupdate.Add("@Order_ID", Entered_OrderId);
                    //    htupdate.Add("@Search_Tax_Request", "True");

                    //    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                    //    Hashtable httaxupdate = new Hashtable();
                    //    System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                    //    httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
                    //    httaxupdate.Add("@Order_ID", Entered_OrderId);
                    //    httaxupdate.Add("@Search_Tax_Request_Progress", 8);

                    //    dttaxupdate = dataaccess.ExecuteSP("Sp_Order", httaxupdate);



                    //    //OrderHistory
                    //    Hashtable ht_Order_History1 = new Hashtable();
                    //    DataTable dt_Order_History1 = new DataTable();
                    //    ht_Order_History1.Add("@Trans", "INSERT");
                    //    ht_Order_History1.Add("@Order_Id", Entered_OrderId);
                    //    ht_Order_History1.Add("@User_Id", userid);
                    //    ht_Order_History1.Add("@Status_Id", 26);
                    //    ht_Order_History1.Add("@Progress_Id", 8);
                    //    ht_Order_History1.Add("@Work_Type", 1);
                    //    ht_Order_History1.Add("@Assigned_By", userid);
                    //    ht_Order_History1.Add("@Modification_Type", "Order Moved Tax Queue");
                    //    dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);
                    //    // Order_History();
                    //}

                    //Assiging the VA Sate and FAIRFAX county will assign to the Rajani  User
                    if (GET_BARROWER_STATE == 47 && GET_ORDER_TYPE != 70 && GET_ORDER_TYPE != 110 )
                    {
                        if (Client_ID != 28)
                        {

                            if (GET_BARROWER_COUNTY == 2857 || GET_BARROWER_COUNTY == 2858)
                            {


                                Assign_Order_For_User();

                            }
                        }
                    }


                    if (Assign_County_Type_ID != 2 && GET_ORDER_TYPE != 70 && GET_ORDER_TYPE != 110 && Client_ID != 28)
                    {
                        if (GET_BARROWER_COUNTY != 2857 || GET_BARROWER_COUNTY != 2858)
                        {

                            Vendor_Order_Allocate_New();

                        }
                    }


                    //Assigning the order to The Tax Allocation
                   
                        if (GET_ORDER_TYPE == 70 || GET_ORDER_TYPE == 110)
                        {

                            Insert_Tax_Order_Status();

                        }
                    

                    Get_Maximum_OrderNumber();
                    //}

                    grd_order.DataSource = null;
                }
                // grd_order.EmptyDataText = "No Orders to Import";
                //   grd_order.DataBind();
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Orders successfully Imported')</script>", false);
            }
            // }
            if (CountOrderInsert > 0)
            {

                MessageBox.Show(CountOrderInsert + " Orders successfully Imported");
                grd_order.Rows.Clear();
            }
            //model1.Hide();
        }

        private void Insert_Internal_Tax_Order_Status()
        {

            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", Entered_OrderId);
            httax.Add("@Order_Task", 26);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", 1);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);



        }
        protected void Assign_Order_For_User()
        {
            //For the order is VA STATE FAIRFOX COUNTY then IT Will Assign to The User Rajiniganth ORDER WILL ASSIGN

            string lbl_Allocated_Userid = "7";
            Hashtable htchk_Assign = new Hashtable();
            DataTable dtchk_Assign = new System.Data.DataTable();
            htchk_Assign.Add("@Trans", "ORDER_ASSIGN_VERIFY");
            htchk_Assign.Add("@Order_Id", Entered_OrderId);
            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            if (dtchk_Assign.Rows.Count <= 0)
            {
                Hashtable htupassin = new Hashtable();
                DataTable dtupassign = new DataTable();

                htupassin.Add("@Trans", "DELET_BY_ORDER");
                htupassin.Add("@Order_Id", Entered_OrderId);
                //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));

                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);


                Hashtable htinsert_Assign = new Hashtable();
                DataTable dtinsertrec_Assign = new System.Data.DataTable();
                htinsert_Assign.Add("@Trans", "INSERT");
                htinsert_Assign.Add("@Order_Id", Entered_OrderId);
                //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));
                htinsert_Assign.Add("@Assigned_By", userid);
                htinsert_Assign.Add("@Modified_By", userid);
                htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                htinsert_Assign.Add("@status", "True");
                dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);
            }
            //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

            Hashtable htinsertrec = new Hashtable();
            DataTable dtinsertrec = new System.Data.DataTable();

            //DateTime date = new DateTime();
            //date = DateTime.Now;
            //string dateeval = date.ToString("dd/MM/yyyy");
            //string time = date.ToString("hh:mm tt");


            //23-01-2018
 
            DateTime date = new DateTime();
            date = DateTime.Now;
            DateTime time;
          date= DateTime.Now;
            string dateeval = date.ToString("MM/dd/yyyy");
           

            htinsertrec.Add("@Trans", "UPDATE_REALLOCATE");
            htinsertrec.Add("@Order_Id", Entered_OrderId);
            htinsertrec.Add("@User_Id", 7);
            htinsertrec.Add("@Order_Status_Id", 2);
            htinsertrec.Add("@Order_Progress_Id", 6);
            htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
            htinsertrec.Add("@Assigned_By", userid);
            htinsertrec.Add("@Modified_By", userid);
            htinsertrec.Add("@Modified_Date", DateTime.Now);
            htinsertrec.Add("@status", "True");
            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);


            Hashtable htorderStatus = new Hashtable();
            DataTable dtorderStatus = new DataTable();
            htorderStatus.Add("@Trans", "UPDATE_STATUS");
            htorderStatus.Add("@Order_ID", Entered_OrderId);
            htorderStatus.Add("@Order_Status", 2);
            htorderStatus.Add("@Modified_By", userid);
            htorderStatus.Add("@Modified_Date", date);
            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);
            Hashtable htorderStatus_Allocate = new Hashtable();
            DataTable dtorderStatus_Allocate = new DataTable();
            htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
            htorderStatus_Allocate.Add("@Order_ID", Entered_OrderId);
            htorderStatus_Allocate.Add("@Order_Status_Id", 2);
            htorderStatus_Allocate.Add("@Modified_By", userid);
            htorderStatus_Allocate.Add("@Assigned_Date", Convert.ToString(dateeval));
            htorderStatus_Allocate.Add("@Assigned_By", userid);
            //For this User VA STATE FAIRFOX COUNTY ORDER WILL ASSIGN
            htorderStatus_Allocate.Add("@User_Id", 7);
            htorderStatus_Allocate.Add("@Modified_Date", date);
            dtorderStatus_Allocate = dataaccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);


            Hashtable htupdate_Prog = new Hashtable();
            DataTable dtupdate_Prog = new System.Data.DataTable();
            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
            htupdate_Prog.Add("@Order_ID", Entered_OrderId);
            htupdate_Prog.Add("@Order_Progress", 6);
            htupdate_Prog.Add("@Modified_By", userid);
            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


            //OrderHistory
            Hashtable ht_Order_History = new Hashtable();
            DataTable dt_Order_History = new DataTable();
            ht_Order_History.Add("@Trans", "INSERT");
            ht_Order_History.Add("@Order_Id", Entered_OrderId);
            ht_Order_History.Add("@User_Id", 7);
            ht_Order_History.Add("@Status_Id", 2);
            ht_Order_History.Add("@Progress_Id", 6);
            ht_Order_History.Add("@Work_Type", 1);
            ht_Order_History.Add("@Assigned_By", userid);
            ht_Order_History.Add("@Modification_Type", "Order Assigned Automatically");
            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);



        }

        private void Insert_Tax_Order_Status()
        {

            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", Entered_OrderId);
            httax.Add("@Order_Task", 21);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", 1);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);





        }

        private void Vendor_Order_Allocate_New()
        {

            int Order_Type_ABS;
            Vendor_Order_Allocation_Count = 0;
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABBR");
            ht_BIND.Add("@Order_Type", lblOrder_Type_For_Vendor.ToString());
            dt_BIND = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_BIND);
            if (dt_BIND.Rows.Count > 0)
            {
                Order_Type_ABS = int.Parse(dt_BIND.Rows[0]["OrderType_ABS_Id"].ToString());
            }
            else
            {

                Order_Type_ABS = 0;

            }
            if (Order_Type_ABS != 0)
            {
                int State_Id = GET_BARROWER_STATE;

                int County_Id = GET_BARROWER_COUNTY;
                Hashtable htvendorname = new Hashtable();
                DataTable dtvendorname = new DataTable();
                htvendorname.Add("@Trans", "GET_VENDORS_STATE_COUNTY_WISE");
                htvendorname.Add("@State_Id", State_Id);
                htvendorname.Add("@County_Id", County_Id);
                dtvendorname = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendorname);
                Vendors_State_County = string.Empty;
                if (dtvendorname.Rows.Count > 0)
                {
                    for (int i = 0; i < dtvendorname.Rows.Count; i++)
                    {
                        Vendors_State_County = Vendors_State_County + dtvendorname.Rows[i]["Vendor_Id"].ToString();
                        Vendors_State_County += (i < dtvendorname.Rows.Count) ? "," : string.Empty;

                    }

                    Hashtable htcheck_Vendor_Order_Type_Abs = new Hashtable();
                    DataTable dtcheck_Vendor_Order_Type_Abs = new DataTable();
                    htcheck_Vendor_Order_Type_Abs.Add("@Trans", "GET_VENDOR_ORDER_TYPE_COVERAGE");
                    htcheck_Vendor_Order_Type_Abs.Add("@Vendors_Id", Vendors_State_County);
                    htcheck_Vendor_Order_Type_Abs.Add("@Order_Type_Abs_Id", Order_Type_ABS);
                    dtcheck_Vendor_Order_Type_Abs = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", htcheck_Vendor_Order_Type_Abs);

                    Vendors_Order_Type = string.Empty;
                    if (dtcheck_Vendor_Order_Type_Abs.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtcheck_Vendor_Order_Type_Abs.Rows.Count; j++)
                        {

                            Vendors_Order_Type = Vendors_Order_Type + dtcheck_Vendor_Order_Type_Abs.Rows[j]["Vendor_Id"].ToString();
                            Vendors_Order_Type += (j < dtcheck_Vendor_Order_Type_Abs.Rows.Count) ? "," : string.Empty;

                        }

                        //Hashtable htget_Client_Id= new Hashtable();
                        //DataTable dtget_Client_Id = new DataTable();

                        //htget_Client_Id.Add("@Trans", "GET_CLIENT_ID");
                        //htget_Client_Id.Add("@Subprocess_Id", GET_SUBPROCESS);
                        //dtget_Client_Id=dataaccess.ExecuteSP("",htget_Client_Id);


                        Hashtable htget_vendor_Client_And_Sub_Client = new Hashtable();
                        DataTable dtget_Vendor_Client_And_Sub_Client = new DataTable();

                        htget_vendor_Client_And_Sub_Client.Add("@Trans", "GET_VENDOR_ON_CLIENT_AND_SUB_CLIENT");
                        htget_vendor_Client_And_Sub_Client.Add("@Client_Id", GET_CLIENT);
                        htget_vendor_Client_And_Sub_Client.Add("@Sub_Client_Id", GET_SUBPROCESS);
                        htget_vendor_Client_And_Sub_Client.Add("@Vendors_Id", Vendors_Order_Type);
                        dtget_Vendor_Client_And_Sub_Client = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htget_vendor_Client_And_Sub_Client);
                        Vendors_Client_Sub_Client = string.Empty;
                        if (dtget_Vendor_Client_And_Sub_Client.Rows.Count > 0)
                        {


                            //Getting the Vendors Satisfied All the Conditions

                            DataTable dt_Temp_Vendors = new DataTable();

                            dt_Temp_Vendors.Columns.Add("Vendor_Id");
                            dt_Temp_Vendors.Columns.Add("Capcity");
                            dt_Temp_Vendors.Columns.Add("Percentage");

                            for (int ven = 0; ven < dtget_Vendor_Client_And_Sub_Client.Rows.Count; ven++)
                            {

                                //Getting the Vendor Order Capacity
                                Vendor_Id = int.Parse(dtget_Vendor_Client_And_Sub_Client.Rows[ven]["Vendor_Id"].ToString());
                                Hashtable htvenncapacity = new Hashtable();
                                System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                                htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                                htvenncapacity.Add("@Venodor_Id", dtget_Vendor_Client_And_Sub_Client.Rows[ven]["Vendor_Id"].ToString());
                                dtvencapacity = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                                if (dtvencapacity.Rows.Count > 0)
                                {
                                    Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());

                                    Hashtable htetcdate = new Hashtable();
                                    System.Data.DataTable dtetcdate = new System.Data.DataTable();
                                    htetcdate.Add("@Trans", "GET_DATE");
                                    dtetcdate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);

                                    Vend_date = dtetcdate.Rows[0]["Date"].ToString();



                                    if (Vendor_Order_capacity != 0)
                                    {

                                        //Getting the Vendor Client Wise Percentage

                                        Hashtable htvendor_Percngate = new Hashtable();
                                        System.Data.DataTable dtvendor_percentage = new System.Data.DataTable();

                                        htvendor_Percngate.Add("@Trans", "GET_VENDOR_PERCENTAGE_OF_ORDERS");
                                        htvendor_Percngate.Add("@Venodor_Id", Vendor_Id);
                                        htvendor_Percngate.Add("@Client_Id", GET_CLIENT);
                                        htvendor_Percngate.Add("@Date", Vend_date);
                                        dtvendor_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendor_Percngate);

                                        if (dtvendor_percentage.Rows.Count > 0)
                                        {

                                            Vendor_Order_Percentage = Convert.ToDecimal(dtvendor_percentage.Rows[0]["Percentage"].ToString());

                                            if (Vendor_Order_Percentage != 0)
                                            {

                                             
                                                Hashtable htvendor_Bal_Percngate = new Hashtable();
                                                System.Data.DataTable dtvendor_bal_percentage = new System.Data.DataTable();
                                                htvendor_Bal_Percngate.Add("@Trans", "GET_BALANCE_PERCENTAGE");
                                                htvendor_Bal_Percngate.Add("@Venodor_Id", Vendor_Id);
                                                htvendor_Bal_Percngate.Add("@Client_Id", GET_CLIENT);
                                                htvendor_Bal_Percngate.Add("@Date", Vend_date);
                                                dtvendor_bal_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendor_Bal_Percngate);
                                                if (dtvendor_bal_percentage.Rows.Count > 0)
                                                {

                                                    Vendor_Balance_Percentage = Convert.ToDecimal(dtvendor_bal_percentage.Rows[0]["Vendor_Balance_Perntage"].ToString());

                                                }
                                                else
                                                {
                                                    Vendor_Balance_Percentage = 0;

                                                }

                                                Total_Vendor_Balance_Percentage = Vendor_Order_Percentage + Vendor_Balance_Percentage;


                                                Hashtable htVendor_No_Of_Order_Assigned = new Hashtable();
                                                System.Data.DataTable dtVendor_No_Of_Order_Assigned = new System.Data.DataTable();
                                                htVendor_No_Of_Order_Assigned.Add("@Trans", "COUNT_NO_OF_ORDER_ASSIGNED_TO_VENDOR_DATE_WISE");
                                                htVendor_No_Of_Order_Assigned.Add("@Venodor_Id", Vendor_Id);
                                                htVendor_No_Of_Order_Assigned.Add("@Date", Vend_date);

                                                dtVendor_No_Of_Order_Assigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htVendor_No_Of_Order_Assigned);

                                                if (dtVendor_No_Of_Order_Assigned.Rows.Count > 0)
                                                {

                                                    No_Of_Order_Assignd_for_Vendor = int.Parse(dtVendor_No_Of_Order_Assigned.Rows[0]["count"].ToString());
                                                }
                                                else
                                                {

                                                    No_Of_Order_Assignd_for_Vendor = 0;
                                                }

                                                Hashtable htcheck_Percentage = new Hashtable();
                                                DataTable dtcheck_percentage = new DataTable();

                                                htcheck_Percentage.Add("@Trans", "CEHCK");
                                                htcheck_Percentage.Add("@Client_Id", GET_CLIENT);
                                                htcheck_Percentage.Add("@Venodor_Id", Vendor_Id);
                                                htcheck_Percentage.Add("@Date", Vend_date);
                                                dtcheck_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htcheck_Percentage);

                                                int Check_Count;

                                                if (dtcheck_percentage.Rows.Count > 0)
                                                {

                                                    Check_Count = int.Parse(dtcheck_percentage.Rows[0]["count"].ToString());
                                                }
                                                else
                                                {

                                                    Check_Count = 0;
                                                }

                                                if (No_Of_Order_Assignd_for_Vendor < Vendor_Order_capacity)
                                                {
                                                    Assigning_To_Vendor = 1;


                                                    if (Check_Count == 0)
                                                    {


                                                        Hashtable ht_Insert_Temp = new Hashtable();
                                                        DataTable dt_Insert_Temp = new DataTable();

                                                        ht_Insert_Temp.Add("@Trans", "INSERT_TEMP_VENDOR_ORDER_CAPACITY");
                                                        ht_Insert_Temp.Add("@Venodor_Id", Vendor_Id);
                                                        ht_Insert_Temp.Add("@Client_Id", GET_CLIENT);
                                                        ht_Insert_Temp.Add("@Vendor_Capcity", Vendor_Order_capacity);
                                                        ht_Insert_Temp.Add("@Vendor_Balance_Perntage", Total_Vendor_Balance_Percentage);
                                                        ht_Insert_Temp.Add("@No_Of_Order_Assigned", No_Of_Order_Assignd_for_Vendor);
                                                        dt_Insert_Temp = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Insert_Temp);
                                                    }
                                                    else
                                                    {

                                                        Hashtable ht_Update_Perentage = new Hashtable();
                                                        DataTable dt_Update_Perentage = new DataTable();

                                                        ht_Update_Perentage.Add("@Trans", "UPDATE_PERCENTAGE");
                                                        ht_Update_Perentage.Add("@Venodor_Id", Vendor_Id);
                                                        ht_Update_Perentage.Add("@Date", Vend_date);
                                                        ht_Update_Perentage.Add("@Client_Id", GET_CLIENT);
                                                        ht_Update_Perentage.Add("@Vendor_Capcity", Vendor_Order_capacity);
                                                        ht_Update_Perentage.Add("@Vendor_Balance_Perntage", Total_Vendor_Balance_Percentage);
                                                        ht_Update_Perentage.Add("@No_Of_Order_Assigned", No_Of_Order_Assignd_for_Vendor);
                                                        dt_Update_Perentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Update_Perentage);

                                                    }
                                                }

                                            }
                                        }
                                    }
                                }

                            }


                            if (Assigning_To_Vendor == 1)
                            {
                                Hashtable ht_Get_Max_Vendor = new Hashtable();
                                DataTable dt_Get_Max_Vendor = new DataTable();
                                ht_Get_Max_Vendor.Add("@Trans", "GET_MAX_VENDOR_BALANCE_PERCENTAGE");
                                ht_Get_Max_Vendor.Add("@Client_Id", GET_CLIENT);
                                ht_Get_Max_Vendor.Add("@Date", Vend_date);
                                dt_Get_Max_Vendor = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Get_Max_Vendor);

                                if (dt_Get_Max_Vendor.Rows.Count > 0)
                                {
                                    Vendor_Id_For_Assign = int.Parse(dt_Get_Max_Vendor.Rows[0]["Vendor_Id"].ToString());


                                    if (dt_Get_Max_Vendor.Rows.Count > 0)
                                    {
                                        Vendor_Order_Allocation_Count = 1;

                                        Hashtable htVendor_No_Of_Order_Assigned = new Hashtable();
                                        System.Data.DataTable dtVendor_No_Of_Order_Assigned = new System.Data.DataTable();
                                        htVendor_No_Of_Order_Assigned.Add("@Trans", "COUNT_NO_OF_ORDER_ASSIGNED_TO_VENDOR_DATE_WISE");
                                        htVendor_No_Of_Order_Assigned.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                        htVendor_No_Of_Order_Assigned.Add("@Date", Vend_date);

                                        dtVendor_No_Of_Order_Assigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htVendor_No_Of_Order_Assigned);

                                        if (dtVendor_No_Of_Order_Assigned.Rows.Count > 0)
                                        {

                                            No_Of_Order_Assignd_for_Vendor = int.Parse(dtVendor_No_Of_Order_Assigned.Rows[0]["count"].ToString());
                                        }
                                        else
                                        {

                                            No_Of_Order_Assignd_for_Vendor = 0;
                                        }

                                        Hashtable htvenncapacity = new Hashtable();
                                        System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                                        htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                                        htvenncapacity.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                        dtvencapacity = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                                        if (dtvencapacity.Rows.Count > 0)
                                        {
                                            Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());
                                        }


                                        Total_Vendor_Alloacated_Percentage = Total_Vendor_Balance_Percentage - Vendor_Order_Percentage;


                                        Hashtable htetcdate = new Hashtable();
                                        System.Data.DataTable dtetcdate = new System.Data.DataTable();
                                        htetcdate.Add("@Trans", "GET_DATE");
                                        dtetcdate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);

                                        Vend_date = dtetcdate.Rows[0]["Date"].ToString();

                                        if (No_Of_Order_Assignd_for_Vendor <= Vendor_Order_capacity)
                                        {



                                            Hashtable htCheckOrderAssigned = new Hashtable();
                                            System.Data.DataTable dtcheckorderassigned = new System.Data.DataTable();

                                            htCheckOrderAssigned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
                                            htCheckOrderAssigned.Add("@Order_Id", Entered_OrderId);
                                            dtcheckorderassigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htCheckOrderAssigned);

                                            int CheckCount = int.Parse(dtcheckorderassigned.Rows[0]["count"].ToString());


                                            if (CheckCount <= 0)
                                            {

                                                Hashtable htupdatestatus = new Hashtable();
                                                System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                                                htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                                                htupdatestatus.Add("@Order_Status", 20);
                                                htupdatestatus.Add("@Modified_By", userid);
                                                htupdatestatus.Add("@Order_ID", Entered_OrderId);
                                                dtupdatestatus = dataaccess.ExecuteSP("Sp_Order", htupdatestatus);


                                                Hashtable htupdateprogress = new Hashtable();
                                                System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                                                htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdateprogress.Add("@Order_Progress", 6);
                                                htupdateprogress.Add("@Modified_By", userid);
                                                htupdateprogress.Add("@Order_ID", Entered_OrderId);
                                                dtupdateprogress = dataaccess.ExecuteSP("Sp_Order", htupdateprogress);


                                                Hashtable htinsert = new Hashtable();
                                                System.Data.DataTable dtinert = new System.Data.DataTable();

                                                htinsert.Add("@Trans", "INSERT");
                                                htinsert.Add("@Order_Id", Entered_OrderId);
                                                htinsert.Add("@Order_Task_Id", 2);
                                                htinsert.Add("@Order_Status_Id", 13);
                                                htinsert.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                                htinsert.Add("@Assigned_Date_Time", dtetcdate.Rows[0]["Date_time"]);
                                                htinsert.Add("@Assigned_By", userid);
                                                htinsert.Add("@Inserted_By", userid);
                                                htinsert.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                                htinsert.Add("@Status", "True");
                                                dtinert = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htinsert);

                                                Hashtable htinsertstatus = new Hashtable();
                                                System.Data.DataTable dtinsertstatus = new System.Data.DataTable();
                                                htinsertstatus.Add("@Trans", "INSERT");
                                                htinsertstatus.Add("@Vendor_Id", Vendor_Id_For_Assign);
                                                htinsertstatus.Add("@Order_Id", Entered_OrderId);
                                                htinsertstatus.Add("@Order_Task", 2);
                                                htinsertstatus.Add("@Order_Status", 13);
                                                htinsertstatus.Add("@Assigen_Date", dtetcdate.Rows[0]["Date"]);
                                                htinsertstatus.Add("@Inserted_By", userid);
                                                htinsertstatus.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                                htinsertstatus.Add("@Status", "True");
                                                dtinsertstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htinsertstatus);

                                                Hashtable ht_Order_History = new Hashtable();
                                                System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Entered_OrderId);
                                                ht_Order_History.Add("@User_Id", userid);
                                                ht_Order_History.Add("@Status_Id", 20);
                                                ht_Order_History.Add("@Progress_Id", 6);
                                                ht_Order_History.Add("@Assigned_By", userid);
                                                ht_Order_History.Add("@Modification_Type", "Vendor Order Auto Assigned");
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);



                                                Hashtable ht_Update_Perentage = new Hashtable();
                                                DataTable dt_Update_Perentage = new DataTable();

                                                ht_Update_Perentage.Add("@Trans", "UPDATE_PERCENTAGE");
                                                ht_Update_Perentage.Add("@Date", Vend_date);
                                                ht_Update_Perentage.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                                ht_Update_Perentage.Add("@Client_Id", GET_CLIENT);
                                                ht_Update_Perentage.Add("@Vendor_Capcity", Vendor_Order_capacity);
                                                ht_Update_Perentage.Add("@Vendor_Balance_Perntage", Total_Vendor_Alloacated_Percentage);
                                                ht_Update_Perentage.Add("@No_Of_Order_Assigned", No_Of_Order_Assignd_for_Vendor);
                                                dt_Update_Perentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Update_Perentage);


                                                Assigning_To_Vendor = 0;




                                            }




                                        }


                                    }
                                }







                            }



                        }


                    }

                }



            }





        }
        private void Order_History()
        {
            Hashtable ht_Order_History = new Hashtable();
            DataTable dt_Order_History = new DataTable();
            ht_Order_History.Add("@Trans", "INSERT");

            ht_Order_History.Add("@Order_Id", Entered_OrderId);

            // ht_Order_History.Add("@User_Id", );
            ht_Order_History.Add("@Status_Id", GET_ORDER_STATUS);
            ht_Order_History.Add("@Progress_Id", 8);
            ht_Order_History.Add("@Assigned_By", userid);

            ht_Order_History.Add("@Modification_Type", "Order Create");

            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

        }
        protected void Get_Maximum_OrderNumber()
        {
            Hashtable htmax = new Hashtable();
            DataTable dtmax = new System.Data.DataTable();
            htmax.Add("@Trans", "MAX_ORDER_NO");
            dtmax = dataaccess.ExecuteSP("Sp_Order", htmax);
            if (dtmax.Rows.Count > 0)
            {
                MAX_ORDER_NUMBER = "DRN" + "-" + dtmax.Rows[0]["ORDER_NUMBER"].ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Import_Orders_Load(object sender, EventArgs e)
        {
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
            grd_order.EnableHeadersVisualStyles = false;
            grd_order.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
        }

        private void btn_Sample_Format_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"c:\OMS_Import\Order Import.xlsx";
            File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Order Import.xlsx", temppath, true);

            Process.Start(temppath);  
        }
    }
}
