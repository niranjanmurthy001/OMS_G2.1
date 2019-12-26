using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Order_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        private Point pt, pt1, ordertask_lbl, grp_pt, grp_pt1, grd_ordertask, grd_ordertask1, grd_orderprogress, grd_orderprogress1, grd_ordererr, grd_ordererr1, grd_ordercmt, grd_ordercmt1, form_pt, form1_pt;
        private Point ordertask_lbl1, orderprog_lbl, orderprog_lbl1, ordererror, ordererror1, ordercomment, ordercomment1;
        string Order_Target, Time_Zone, Recived_Time;
        int Order_Id = 0;
        int userid;
        string Empname;
        int Count;
        int BRANCH_ID;
        int No_Of_Orders;
        int client_Id, Subprocess_id;
        
        string OrderStatus;
        int Chk_Userid;
        string MAX_ORDER_NUMBER;
        int chk_Order_no;
        double zipcode;
        decimal SearchCost, Copy_Cost, Abstractor_Cost;
        int stateid, county, ordertype, Vendor_id,User_Role_Id;

        public Vendor_Order_View(int Orderid, int User_Id,int USER_ROLE)
        {
            InitializeComponent();

            User_Role_Id = USER_ROLE;
            if (User_Role_Id == 1)
            {
                dbc.BindClientName(ddl_ClientName);
            } 
            else 
            {

                dbc.BindClientNo(ddl_ClientName);
            }
           
            dbc.BindOrderType(ddl_ordertype);
            userid = User_Id;
            dbc.BindState(ddl_State);
            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            if (User_Role_Id == 1)
            {
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
            }
            else 
            {
                dbc.BindSubProcessNumber(ddl_SubProcess, clientid);

            }
            ddl_ordertask.Visible = true;
            ddl_ordertask.Items.Insert(0, "Search");
            ddl_ordertask.Items.Insert(1, "Typing");
            ddl_ordertask.Items.Insert(2, "Upload");
            ddl_ordertask.Items.Insert(3, "Upload Completed");
            ddl_ordertask.Items.Insert(4, "Abstractor");
            ddl_ordertask.Items.Insert(5, "Vendor");
            //   ddl_Search_Type.Visible = true;
            // ddl_Search_Type.Items.Insert(0, "SELECT");
            //ddl_Search_Type.Items.Insert(0, "TIER 1");
            //ddl_Search_Type.Items.Insert(1, "TIER 2");
            //ddl_Search_Type.Items.Insert(2, "TIER 2-In house");

            //   ddl_Order_Source.Items.Insert(0, "SELECT");
            ddl_Order_Source.Items.Insert(0, "Online");
            ddl_Order_Source.Items.Insert(1, "Subscription");
            ddl_Order_Source.Items.Insert(2, "Plant");
            ddl_Order_Source.Items.Insert(3, "Abstractor");
            ddl_Order_Source.Items.Insert(4, "Online/Abstractor");
            //Order_Controls_Load();
            Order_Id = Orderid;

            // SetMyCustomFormat();


            if (ddl_ClientName.SelectedIndex != 0)
            {
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
        }

        private void Vendor_Order_View_Load(object sender, EventArgs e)
        {
            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            if (User_Role_Id == 1)
            {
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
            }
            else 
            {

                dbc.BindSubProcessNumber(ddl_SubProcess, client_Id);
            }
            if (ddl_ClientName.SelectedIndex != 0)
            {
                //  int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
              
                if (User_Role_Id == 1)
                {
                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                }
                else 
                {

                    dbc.BindSubProcessNumber(ddl_SubProcess, client_Id);
                }
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
            //  AddParent();
            Order_Load();
            dbc.Bind_Vendors(ddl_Vendor_Name);
            dbc.Bind_Vendor_Task(ddl_Vendor_Task);
            dbc.Bind_Vendor_Status(ddl_Vendor_Status);
           

            load_Vendor_Name_Name();
            Vendor_Order_Details();
           
        }
        private void Order_Load()
        {
            try
            {
                if (Order_Id != 0)
                {
                    Control_Enable_false();

                    Hashtable ht_Select_Order_Details = new Hashtable();
                    DataTable dt_Select_Order_Details = new DataTable();

                    ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_WISE");
                    ht_Select_Order_Details.Add("@Order_ID", Order_Id);
                    dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);

                    if (dt_Select_Order_Details.Rows.Count > 0)
                    {
                        //ViewState["Orderid"] = order_Id.ToString();
                        //Session["order_id"] = order_Id.ToString();
                        txt_OrderNumber.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                        //string date = dt_Select_Order_Details.Rows[0]["Date"].ToString();
                        System.DateTime firstDate = DateTime.ParseExact(dt_Select_Order_Details.Rows[0]["Date"].ToString(), "MM/dd/yyyy", null);
                        txt_Date.Text = firstDate.ToString();

                        ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                        ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                        if (ddl_ClientName.SelectedIndex > 0)
                        {

                            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());


                            if (User_Role_Id == 1)
                            {
                                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                            }
                            else
                            {

                                dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                            }
                        }

                        ddl_SubProcess.SelectedValue = dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString();

                        txt_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                        txt_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                        txt_City.Text = dt_Select_Order_Details.Rows[0]["City"].ToString();
                        txt_Zip.Text = dt_Select_Order_Details.Rows[0]["Zip"].ToString();
                        ddl_State.SelectedValue = dt_Select_Order_Details.Rows[0]["stateid"].ToString();
                        //ddl_Search_Type.SelectedItem = lblSearch_Type.Text;
                        //  ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                        //  ddl_Search_Type.Text = dt_Select_Order_Details.Rows[0]["Search_Type"].ToString();
                        txt_Client_order_ref.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Ref"].ToString();
                        lbl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();
                        ddl_Hour.Text = dt_Select_Order_Details.Rows[0]["HH"].ToString();
                        ddl_Minute.Text = dt_Select_Order_Details.Rows[0]["MM"].ToString();
                        ddl_Sec.Text = dt_Select_Order_Details.Rows[0]["SS"].ToString();
                        if (ddl_State.SelectedIndex > 0)
                        {
                            int stateid = int.Parse(ddl_State.SelectedValue.ToString());
                            dbc.BindCounty(ddl_County, stateid);

                        }
                        if (dt_Select_Order_Details.Rows[0]["CountyId"].ToString() != "0")
                        {
                            ddl_County.SelectedValue = dt_Select_Order_Details.Rows[0]["CountyId"].ToString();
                        }
                        else
                        {
                            ddl_County.SelectedValue = "SELECT";
                        }
                        ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                        txt_Borrowername.Text = dt_Select_Order_Details.Rows[0]["Borrower_Name"].ToString();

                        txt_Notes.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();

                        txt_Vendor_Order_Notes.Text = dt_Select_Order_Details.Rows[0]["Vendor_Instructions"].ToString();
                        // chk_Expidate.Checked = Convert.ToBoolean(dt_Select_Order_Details.Rows[0]["Expidate"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in loading Order check with administrator");
            }

        }

        public void load_Vendor_Name_Name()
        {
            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();


            ht_Select_Order_Details.Add("@Trans", "GET_VENDOR_ID");

            ht_Select_Order_Details.Add("@Order_Id", Order_Id);

            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {


                ddl_Vendor_Name.SelectedValue = dt_Select_Order_Details.Rows[0]["Venodor_Id"].ToString();
            }


        }
        public void Vendor_Order_Details()
        {

            Hashtable ht_Vendor_Order_Details = new Hashtable();
            DataTable dt_Vendor_Order_Details = new DataTable();




            ht_Vendor_Order_Details.Add("@Trans", "GET_VENDOR_ORDER_DETAILS_BY_ORDERID");
            ht_Vendor_Order_Details.Add("@Order_Id", Order_Id);

            dt_Vendor_Order_Details = dataaccess.ExecuteSP("Sp_Vendor_Order_Count", ht_Vendor_Order_Details);

            if (dt_Vendor_Order_Details.Rows.Count > 0)
            {
                ddl_Vendor_Name.SelectedValue=dt_Vendor_Order_Details.Rows[0]["Vendor_Id"].ToString();
                ddl_Vendor_Task.SelectedValue = dt_Vendor_Order_Details.Rows[0]["Order_Status_ID"].ToString();
                ddl_Vendor_Status.SelectedValue = dt_Vendor_Order_Details.Rows[0]["Order_Progress_Id"].ToString();

                lbl_Assigned_Date.Text = dt_Vendor_Order_Details.Rows[0]["Assigned_Date_Time"].ToString();

                lbl_Completed_Date.Text = dt_Vendor_Order_Details.Rows[0]["Completed_Date"].ToString();

                lbl_Rejected_Date.Text = dt_Vendor_Order_Details.Rows[0]["Rejected_Date_Time"].ToString();

                txt_Rejected_Reason.Text = dt_Vendor_Order_Details.Rows[0]["Rejected_Reason"].ToString();

                txt_Vendor_Comments.Text = dt_Vendor_Order_Details.Rows[0]["Comments"].ToString();

                

                lbl_Search_Date.Text = dt_Vendor_Order_Details.Rows[0]["Search_Date"].ToString(); ;
                lbl_Effective_Date.Text = dt_Vendor_Order_Details.Rows[0]["Effective_Date"].ToString();

                    
           
            }


            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_NOTES");
            htselect.Add("@Order_Id", Order_Id);

            dtselect = dataaccess.ExecuteSP("Sp_Vendor_Order_Notes", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Grid_Comments.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {

                    Grid_Comments.Rows.Add();
                    Grid_Comments.Rows[i].Cells[0].Value = dtselect.Rows[i]["Vendor_Notes"].ToString();
                    Grid_Comments.Rows[i].Cells[1].Value = dtselect.Rows[i]["Inserted_by"].ToString();


                }
            }
            else
            {
                Grid_Comments.Rows.Clear();
           


            }

          


        }
        private void Control_Enable_false()
        {
            ddl_ClientName.Enabled = false;
            ddl_SubProcess.Enabled = false;
            ddl_Hour.Enabled = false;
            ddl_Minute.Enabled = false;
            ddl_Sec.Enabled = false;
            txt_Date.Enabled = false;
            ddl_ordertype.Enabled = false;
            txt_OrderNumber.ReadOnly = true;
            txt_APN.ReadOnly = true;
            txt_Client_order_ref.ReadOnly = true;
            txt_Borrowername.ReadOnly = true;
            txt_Address.ReadOnly = true;
            ddl_State.Enabled = false;
            ddl_County.Enabled = false;
            txt_City.ReadOnly = true;
            txt_Zip.ReadOnly = true;
            ddl_ordertask.Enabled = false;
            //  ddl_Search_Type.Enabled = false;
            ddl_Order_Source.Enabled = false;
            txt_Search_cost.ReadOnly = true;
            txt_Copy_cost.ReadOnly = true;
            txt_Abstractor_Cost.ReadOnly = true;
            txt_noofpage.ReadOnly = true;
            txt_Notes.ReadOnly = true;
        }

        private void btn_View_Doc_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id,userid, txt_OrderNumber.Text, ddl_ClientName.SelectedValue.ToString(), ddl_SubProcess.SelectedValue.ToString());
            Orderuploads.Show();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {


            if (txt_Vendor_Order_Notes.Text != "")
            {


                Hashtable htupdate_Vendor_Instruction = new Hashtable();
                DataTable dtupdate_Vendor_Instruction = new DataTable();

                
                htupdate_Vendor_Instruction.Add("@Trans", "UPDATE_VENDOR_NOTES");
                htupdate_Vendor_Instruction.Add("@Vendor_Instructions",txt_Vendor_Order_Notes.Text.ToString());
                htupdate_Vendor_Instruction.Add("@Order_ID", Order_Id);
                dtupdate_Vendor_Instruction = dataaccess.ExecuteSP("Sp_Order",htupdate_Vendor_Instruction);
                MessageBox.Show("Note Updated Sucessfully");





            }
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

      
    }
}
