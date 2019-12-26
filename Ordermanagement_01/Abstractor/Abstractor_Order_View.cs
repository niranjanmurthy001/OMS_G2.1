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
namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Order_View : Form
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
        string User_Role_Id;
        string OrderStatus;
        int Chk_Userid;
        string MAX_ORDER_NUMBER;
        int chk_Order_no;
        double zipcode;
        decimal SearchCost, Copy_Cost, Abstractor_Cost;
        int No_Of_Pages;
        int Chk_Order_Search_Cost;
        string OPERATE_SEARCH_COST;
        int SubprocessId, ClientId;
        decimal Totalcost;
        int stateid, county, ordertype, abstractor_id;
        int Abstractor_Tat;
        int abstractor_Task, abstractor_Status;
        int DateCustom = 0;
        int abs_no_of_pages;
        decimal abs_pages_cost, abs_actual_cost;

        int clientid;
        public Abstractor_Order_View(int Orderid, int User_Id,string USER_ROLE_ID)
        {
            InitializeComponent();

            // Clear();
            pnl_visible();
            User_Role_Id = USER_ROLE_ID;
            // Order_Entry.ActiveForm.Width = 1045;
            if (User_Role_Id == "1")
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
            
            if (ddl_ClientName.SelectedIndex > 0)
            {
                 clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            }
            if (User_Role_Id == "1")
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

        private void Abstractor_Order_View_Load(object sender, EventArgs e)
        {
            //Order_Controls_Load();
           // pnlSideTree.Visible = false;
            //Order_Entry.ActiveForm.Width=1045;
            //  Order_Entry.ActiveForm.Height=731;
            //treeView1.Visible = false;
            pnl_visible();

            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            if (User_Role_Id == "1")
            {
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
            }
            else
            {

                dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
            }

            if (ddl_ClientName.SelectedIndex != 0)
            {
                //  int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                if (User_Role_Id == "1")
                {
                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                }
                else
                {

                    dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                }
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
            dbc.Bind_Abstractor_Name(ddl_Abstractor_Name);
            dbc.Bind_Abstractor_Task(ddl_Abstractor_Task);
            dbc.Bind_Abstractor_Status(ddl_Abstractor_Status);
            txt_Abstractor_Returned_Date.Text = "";
          //  AddParent();
            Order_Load();
           
         

            load_Abstractor_Name();
            Abstractor_Order_Details();
            Load_Abstractor_Order_Details();
        }
        private void pnl_visible()
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            grp_pt.X = 38; grp_pt.Y = 15;
            grp_pt1.X = 230; grp_pt1.Y = 12;
            grd_ordertask.X = 38; grd_ordertask.Y = 402;
            grd_ordertask1.X = 228; grd_ordertask1.Y = 402;
            grd_orderprogress.X = 530; grd_orderprogress.Y = 402;
            grd_orderprogress1.X = 720; grd_orderprogress1.Y = 402;
            grd_ordererr.X = 38; grd_ordererr.Y = 526;
            grd_ordererr1.X = 228; grd_ordererr1.Y = 526;
            grd_ordercmt.X = 530; grd_ordercmt.Y = 526;
            grd_ordercmt1.X = 720; grd_ordercmt1.Y = 526;
            ordertask_lbl.X = 35; ordertask_lbl.Y = 382;
            ordertask_lbl1.X = 223; ordertask_lbl1.Y = 382;
            orderprog_lbl.X = 525; orderprog_lbl.Y = 382;
            orderprog_lbl1.X = 715; orderprog_lbl1.Y = 382;
            ordererror.X = 35; ordererror.Y = 507;
            ordererror1.X = 223; ordererror1.Y = 507;
            ordercomment.X = 525; ordercomment.Y = 507;
            ordercomment1.X = 715; ordercomment1.Y = 507;
            //   Order_Entry.ActiveForm.Width = 1045;
            //  Form.ActiveForm.Size = new System.Drawing.Size(1045,730);
            //form_pt.X = 350; form_pt.Y = 20;
            //form1_pt.X = 1005; form1_pt.Y = 20;

            //if (pnlSideTree.Visible != true)
            //{
            //    //hide panel
            //    // pnlSideTree.Visible = false;
            //    btn_treeview.Location = pt;
            //    lbl_OrderTask.Location = ordertask_lbl;
            //    lbl_OrderProgress.Location = orderprog_lbl;
            //    lbl_OrderError.Location = ordererror;
            //    lbl_OrderComment.Location = ordercomment;
            //    grid_OrderTask.Location = grd_ordertask;
            //    grid_OrderProgress.Location = grd_orderprogress;
            //    grd_Error.Location = grd_ordererr;
            //    Grid_Comments.Location = grd_ordercmt;
            //    grp_OrderEntry.Location = grp_pt;
            //    //    Order_Entry.ActiveForm.Width = 1045;
            //    //Order_Entry.ActiveForm.Location = form_pt;
            //    btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            //}
            //show panel
            // pnlSideTree.Visible = true;



        }
        private void Order_Load()
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
                    txt_Date.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();

                    ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                    ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                    if (ddl_ClientName.SelectedIndex > 0)
                    {

                        int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                        if (User_Role_Id == "1")
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
                   
                }
                
            }
          

        }

        public void Load_Abstractor_Order_Details()
        {


            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();


            ht_Select_Order_Details.Add("@Trans", "GET_ABSTRACTOR_ORDER_DETAILS");

            ht_Select_Order_Details.Add("@Order_Id", Order_Id);
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Abstractor_Order_Status", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                string value = dt_Select_Order_Details.Rows[0]["Progress_Status"].ToString();
                ddl_Abstractor_Status.Text = value.ToString();

            }


        }


        public void load_Abstractor_Name()
        {
            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();


            ht_Select_Order_Details.Add("@Trans", "GET_ABSTRACTOR_ID");

            ht_Select_Order_Details.Add("@Order_ID", Order_Id);

            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {


                ddl_Abstractor_Name.SelectedValue = dt_Select_Order_Details.Rows[0]["Abstractor_Id"].ToString();
            }


        }
        public void Abstractor_Order_Details()
        { 
        
                Hashtable ht_Abstractor_Order_Details = new Hashtable();
                DataTable dt_Abstractor_Order_Details = new DataTable();


              
                if (ddl_State.SelectedIndex > 0)
                {
                     stateid = int.Parse(ddl_State.SelectedValue.ToString());
                    

                }
                if (ddl_County.SelectedIndex > 0)
                {

                     county = int.Parse(ddl_County.SelectedValue.ToString());
                }
                if (ddl_ordertype.SelectedIndex > 0)
                {

                     ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString() );
                }
                if (ddl_Abstractor_Name.SelectedIndex > 0)
                {

                     abstractor_id = int.Parse(ddl_Abstractor_Name.SelectedValue.ToString());
                }

                ht_Abstractor_Order_Details.Add("@Trans", "SELECT_ABSTRACTOR_COST_TAT");
                ht_Abstractor_Order_Details.Add("@order_Id", Order_Id);
                ht_Abstractor_Order_Details.Add("@Abstractor_Id", abstractor_id);
                ht_Abstractor_Order_Details.Add("@State", stateid);
                ht_Abstractor_Order_Details.Add("@County", county);
                ht_Abstractor_Order_Details.Add("@Order_Type_Id", ordertype);

                dt_Abstractor_Order_Details = dataaccess.ExecuteSP("Sp_Abstractor_Cost", ht_Abstractor_Order_Details);

                if (dt_Abstractor_Order_Details.Rows.Count > 0)
                {


                    txt_Abstract_Cost.Text = dt_Abstractor_Order_Details.Rows[0]["Cost"].ToString();
                    txt_Abstract_Tat.Text = dt_Abstractor_Order_Details.Rows[0]["Tat"].ToString();
                    txt_Abstarct_Assigned_date.Text = dt_Abstractor_Order_Details.Rows[0]["Assigned_Date"].ToString();
                    ddl_Abstractor_Task.SelectedValue = dt_Abstractor_Order_Details.Rows[0]["Abstractor_Status"].ToString();
                    ddl_Abstractor_Status.SelectedValue = dt_Abstractor_Order_Details.Rows[0]["Abstractor_Progress"].ToString();
                }

                Hashtable ht_Abstractor_Returndate = new Hashtable();
                DataTable dt_Abstractor_Returndate = new DataTable();
                ht_Abstractor_Returndate.Add("@Trans", "GET_RETURN_DATE");
                ht_Abstractor_Returndate.Add("@order_Id", Order_Id);

                dt_Abstractor_Returndate = dataaccess.ExecuteSP("Sp_Abstractor_Order_Status", ht_Abstractor_Returndate);

                if (dt_Abstractor_Returndate.Rows.Count > 0 && dt_Abstractor_Returndate.Rows[0]["Return_date"] != "" && dt_Abstractor_Returndate.Rows[0]["Return_date"] != null)
                {

                    txt_Abstractor_Returned_Date.Text = dt_Abstractor_Returndate.Rows[0]["Return_date"].ToString();


                }
                else
                {
                    DateCustom = 0;
                  
                    txt_Abstractor_Returned_Date.Text = "";

                }

                Hashtable ht_Abstractor_Cost = new Hashtable();
                DataTable dt_Abstractor_cost = new DataTable();
                ht_Abstractor_Cost.Add("@Trans", "SELECT");
                ht_Abstractor_Cost.Add("@order_Id", Order_Id);

                dt_Abstractor_cost = dataaccess.ExecuteSP("Sp_Abstractor_Order_Cost", ht_Abstractor_Cost);

                if (dt_Abstractor_cost.Rows.Count > 0)
                {

                    txt_abs_no_of_pages.Text = dt_Abstractor_cost.Rows[0]["No_Of_Pages"].ToString();
                    txt_abs_pages_cost.Text = dt_Abstractor_cost.Rows[0]["Pages_Cost"].ToString();
                    txt_abs_actual_cost.Text = dt_Abstractor_cost.Rows[0]["Actual_Cost"].ToString();
                    txt_abs_comments.Text = dt_Abstractor_cost.Rows[0]["Comments"].ToString();
                }
                else
                { 
                txt_abs_no_of_pages.Text="";
                txt_abs_pages_cost.Text ="";
                txt_abs_actual_cost.Text = "";
                txt_abs_comments.Text = "";

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

        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ClientName.SelectedIndex != 0)
            {
                int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                if (User_Role_Id == "1")
                {
                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                }
                else
                {

                    dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                }
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                // ddl_SubProcess.Focus();

            }
            else
            {
                if (User_Role_Id == "1")
                {
                    dbc.BindSubProcessName(ddl_SubProcess, 0);
                }
                else
                {

                    dbc.BindSubProcessNumber(ddl_SubProcess, 0);
                }
            
            
            }
        }

        private void ddl_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_County.SelectedIndex != 0)
            //{
            //    int stateid = int.Parse(ddl_County.SelectedValue.ToString());
            //    dbc.BindState1(ddl_State, stateid);
            //    ddl_County.Focus();

            //}
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_State.SelectedIndex != 0)
            {
                int stateid = int.Parse(ddl_State.SelectedValue.ToString());
                dbc.BindCounty(ddl_County, stateid);
                //   ddl_County.Focus();

            }
        }

        private void ddl_ClientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_SubProcess.Focus();
            }
        }

        private void ddl_SubProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_ordertype.Focus();
            }
        }

        private void ddl_ordertype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_OrderNumber.Focus();
            }
        }

        private void txt_OrderNumber_KeyDown(object sender, KeyEventArgs e)
        {
            Hashtable htorder = new Hashtable();
            DataTable dtorder = new DataTable();
            string Orderchk;
            Orderchk = txt_OrderNumber.Text;
            htorder.Add("@Trans", "CHECK_ORDER_NUMBER");
            htorder.Add("@Client_Order_Number", Orderchk);
            dtorder = dataaccess.ExecuteSP("Sp_Order", htorder);
            //User_Chk_Img.Image = null;
            if (int.Parse(dtorder.Rows[0]["count"].ToString()) > 0)
            {
                //User_Chk_Img.Image = Image.FromFile(Environment.CurrentDirectory + @"\Delete1.png");


            }
            else if (int.Parse(dtorder.Rows[0]["count"].ToString()) <= 0)
            {
                //User_Chk_Img.Image = Image.FromFile(Environment.CurrentDirectory + @"\Sucess.png");

                if (e.KeyCode == Keys.Enter)
                {
                    txt_APN.Focus();
                }
            }


        }

        private void txt_APN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Client_order_ref.Focus();
            }
        }

        private void txt_Client_order_ref_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Borrowername.Focus();
            }
        }

        private void txt_Borrowername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Address.Focus();
            }
        }

        private void txt_Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_State.Focus();
            }
        }

        private void ddl_State_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_County.Focus();
            }
        }

        private void ddl_County_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_City.Focus();
            }
        }

        private void txt_City_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Zip.Focus();
            }
        }

        private void txt_Zip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_ordertask.Focus();
            }
        }

        private void ddl_ordertask_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ddl_Search_Type_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Hour.Focus();
            }
        }

        private void ddl_Hour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Minute.Focus();
            }
        }

        private void ddl_Minute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Sec.Focus();
            }
        }

        private void ddl_Sec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Order_Source.Focus();
            }
        }

        private void ddl_Order_Source_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Search_cost.Focus();
            }
        }

        private void txt_Search_cost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Copy_cost.Focus();
            }
        }

        private void txt_Copy_cost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Abstractor_Cost.Focus();
            }
        }

        private void txt_Abstractor_Cost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_noofpage.Focus();
            }
        }

        private void txt_noofpage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Notes.Focus();
            }
        }

        private void txt_Notes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void AddParent()
        {

            string sKeyTemp = "";
            treeView1.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();

            TreeNode parentnode;
            ht.Add("@Trans", "SELECT");

            dt = dataaccess.ExecuteSP("Sp_Client", ht);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //  {
            sKeyTemp = "Orders";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp = dt.Rows[i]["Client_Name"].ToString();

                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);

                AddChilds(parentnode, sKeyTemp);
            }
        }
        private void AddChilds(TreeNode parentnode, string sKey)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode childnode;
            string sKeyTemp1 = "";

            ht.Add("@Trans", "Subprocess_Name");
            ht.Add("@Client_Name", sKey);
            dt = dataaccess.ExecuteSP("Sp_Abstractor_Tree_Orders", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp1 = dt.Rows[i]["Sub_ProcessName"].ToString();
                childnode = parentnode.Nodes.Add(sKeyTemp1, sKeyTemp1);
                AddChilds1(childnode, sKeyTemp1);
            }
        }
        private void AddChilds1(TreeNode childnode, string sKey1)
        {
            Hashtable ht1 = new Hashtable();
            DataTable dt1 = new System.Data.DataTable();
            TreeNode childnode1;
            string sKeyTemp2 = "";
            ht1.Add("@Trans", "Order_Month");
            ht1.Add("@Sub_ProcessName", sKey1);
            dt1 = dataaccess.ExecuteSP("Sp_Abstractor_Tree_Orders", ht1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                sKeyTemp2 = dt1.Rows[i]["Date"].ToString();
                childnode1 = childnode.Nodes.Add(sKeyTemp2, sKeyTemp2);
                AddChilds2(childnode1, sKeyTemp2, sKey1);
            }
        }
        private void AddChilds2(TreeNode childnode1, string sKey2, string Subprocess)
        {
            Hashtable ht2 = new Hashtable();
            DataTable dt2 = new System.Data.DataTable();
            TreeNode childnode2;
            string sKeyTemp3 = sKey2;
            ht2.Add("@Trans", "Order_Date");
            ht2.Add("@Sub_ProcessName", Subprocess);
            ht2.Add("@Month", sKeyTemp3);
            dt2 = dataaccess.ExecuteSP("Sp_Abstractor_Tree_Orders", ht2);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                sKeyTemp3 = dt2.Rows[i]["Date"].ToString();
                childnode2 = childnode1.Nodes.Add(sKeyTemp3, sKeyTemp3);
                AddChilds3(childnode2, sKeyTemp3, Subprocess);
            }
        }
        private void AddChilds3(TreeNode childnode2, string sKey3, string Subprocess)
        {
            Hashtable ht2 = new Hashtable();
            DataTable dt2 = new System.Data.DataTable();
            TreeNode childnode3;
            string sKeyTemp4 = sKey3;
            string Order_Id_tree;
            ht2.Add("@Trans", "Order_Id");
            ht2.Add("@Sub_ProcessName", Subprocess);
            ht2.Add("@Date", sKeyTemp4);
            dt2 = dataaccess.ExecuteSP("Sp_Abstractor_Tree_Orders", ht2);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                sKeyTemp4 = dt2.Rows[i]["Client_Order_Number"].ToString();
                Order_Id_tree = dt2.Rows[i]["Order_ID"].ToString();
                childnode3 = childnode2.Nodes.Add(Order_Id_tree, sKeyTemp4);
                // AddChilds3(childnode2, sKeyTemp3);

            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Clear();
            // if (treeView1.SelectedNode.Name.ToString() !=)
            bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out Order_Id);
            if (isNum)
            {
                Order_Load();
                load_Abstractor_Name();
                Abstractor_Order_Details();
                // Order_Controls_Load();
            }

        }
        private void Clear()
        {
            txt_Date.Value = DateTime.Now;
            ddl_ClientName.SelectedIndex = 0;
            ddl_SubProcess.SelectedIndex = 0;
            ddl_ordertype.SelectedIndex = 0;
            txt_OrderNumber.Text = "";
            txt_APN.Text = "";
            ddl_Hour.Text = "0";
            ddl_Minute.Text = "00";
            ddl_Sec.Text = "00";
            txt_Client_order_ref.Text = "";
            txt_Borrowername.Text = "";
            txt_Address.Text = "";
            ddl_State.SelectedIndex = 0;
            //ddl_County.SelectedIndex = ;
            txt_City.Text = "";
            txt_Zip.Text = "";
            ddl_ordertask.SelectedIndex = 0;
            // ddl_Search_Type.SelectedIndex = 0;
            ddl_Order_Source.SelectedIndex = 0;
            txt_Search_cost.Text = "";
            txt_Copy_cost.Text = "";
            txt_Abstractor_Cost.Text = "";
            txt_noofpage.Text = "";
            txt_Notes.Text = "";
            lbl_County_Type.Text = "";
            ddl_Abstractor_Name.SelectedIndex = 0;
            txt_Abstract_Cost.Text = "";
            txt_Abstract_Tat.Text = "";
            txt_Abstarct_Assigned_date.Text = "";
            txt_Abstractor_Returned_Date.Text = "";
            ddl_Abstractor_Task.SelectedIndex = 0;
            ddl_Abstractor_Status.SelectedIndex = 0;


        }
        private void ddl_County_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ddl_State_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string message = "Are You Proceed?";
            string title = "Submitting";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {



                if (validate() != false)
                {
                    try
                    {
                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        Hashtable htasbupdate = new Hashtable();
                        DataTable dtabsupdate = new DataTable();
                        if (txt_Abstract_Cost.Text != "")
                        {
                            Abstractor_Cost = Convert.ToDecimal(txt_Abstract_Cost.Text);


                        }

                        else
                        {

                            Abstractor_Cost = 0;
                        }
                        if (txt_Abstract_Tat.Text != "")
                        {

                            Abstractor_Tat = int.Parse(txt_Abstract_Tat.Text);
                        }
                        else
                        {
                            Abstractor_Tat = 0;

                        }
                        htasbupdate.Add("@Trans", "UPDATE");
                        htasbupdate.Add("@Cost", Abstractor_Cost);
                        htasbupdate.Add("@Tat", Abstractor_Tat);
                        htasbupdate.Add("@Modified_By", userid);
                        htasbupdate.Add("@Modified_Date", date);
                        dtabsupdate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htasbupdate);





                        Hashtable htasbtask = new Hashtable();
                        DataTable dtabstask = new DataTable();

                        Hashtable htabsassign = new Hashtable();
                        DataTable dtabsassign = new DataTable();
                        if (ddl_Abstractor_Task.SelectedIndex > 0)
                        {


                            abstractor_Task = int.Parse(ddl_Abstractor_Task.SelectedValue.ToString());

                        }

                        else
                        {

                            abstractor_Task = 0;
                        }
                        if (ddl_Abstractor_Status.SelectedIndex > 0)
                        {

                            abstractor_Status = int.Parse(ddl_Abstractor_Status.SelectedValue.ToString());

                        }
                        else
                        {
                            abstractor_Status = 0;

                        }


                        htasbtask.Add("@Trans", "UPDATE");

                        htasbtask.Add("@Order_Id", Order_Id);
                        htasbtask.Add("@Abstractor_Status", abstractor_Task);
                        htasbtask.Add("@Abstractor_Progress", abstractor_Status);
                        htasbtask.Add("@Modified_By", userid);
                        htasbtask.Add("@Modified_Date", date);
                        dtabstask = dataaccess.ExecuteSP("Sp_Abstractor_Order_Status", htasbtask);

                        htabsassign.Add("@Trans", "UPDATE_STATUS_WISE");
                        htabsassign.Add("@Order_Id", Order_Id);
                        htabsassign.Add("@Order_Status_Id", abstractor_Task);
                        htabsassign.Add("@Order_Progress_Id", abstractor_Status);
                        htabsassign.Add("@Modified_By", userid);
                        htabsassign.Add("@Modified_Date", date);
                        dtabsassign = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", htabsassign);



                        if (abstractor_Status != 6)
                        {
                            Hashtable htreturndate = new Hashtable();
                            DataTable dtreturndate = new DataTable();

                            htreturndate.Add("@Trans", "UPDATE_RETURN_DATE");
                            htreturndate.Add("@Order_Id", Order_Id);
                            htreturndate.Add("@Return_Date", txt_Abstractor_Returned_Date.Text);
                            htreturndate.Add("@Modified_By", userid);
                            htreturndate.Add("@Modified_Date", date);
                            dtreturndate = dataaccess.ExecuteSP("Sp_Abstractor_Order_Status", htreturndate);




                        }


                        Hashtable htabscostcheck = new Hashtable();
                        DataTable dtabscostcheck = new DataTable();
                        htabscostcheck.Add("@Trans", "CHECK");
                        htabscostcheck.Add("@Order_Id", Order_Id);

                        dtabscostcheck = dataaccess.ExecuteSP("Sp_Abstractor_Order_Cost", htabscostcheck);
                        int abschekcost = int.Parse(dtabscostcheck.Rows[0]["count"].ToString());

                        Hashtable htabscost = new Hashtable();
                        DataTable dtabscost = new DataTable();
                        if (abschekcost == 0)
                        {
                            if (txt_abs_no_of_pages.Text != "")
                            {


                                abs_no_of_pages = int.Parse(txt_abs_no_of_pages.Text.ToString());

                            }

                            else
                            {

                                abs_no_of_pages = 0;
                            }
                            if (txt_abs_pages_cost.Text != "")
                            {

                                abs_pages_cost = Convert.ToDecimal(txt_abs_pages_cost.Text.ToString());

                            }
                            else
                            {
                                abs_pages_cost = 0;

                            }

                            if (txt_abs_actual_cost.Text != "")
                            {

                                abs_actual_cost = Convert.ToDecimal((txt_abs_actual_cost.Text.ToString()));

                            }
                            else
                            {
                                abs_actual_cost = 0;

                            }



                            htabscost.Add("@Trans", "INSERT");
                            htabscost.Add("@Order_Id", Order_Id);
                            htabscost.Add("@Abstractor_Id", abstractor_id);
                            htabscost.Add("@No_Of_Pages", abs_no_of_pages);
                            htabscost.Add("@Pages_Cost", abs_pages_cost);
                            htabscost.Add("@Actual_Cost", abs_actual_cost);
                            htabscost.Add("@Status","True");
                            htabscost.Add("@Inserted_By", userid);
                            htabscost.Add("@Instered_Date", date);
                            dtabscost = dataaccess.ExecuteSP("Sp_Abstractor_Order_Cost", htabscost);
                        }
                        else if (abschekcost > 0)
                        {
                            if (txt_abs_no_of_pages.Text != "")
                            {


                                abs_no_of_pages = int.Parse(txt_abs_no_of_pages.Text.ToString());

                            }

                            else
                            {

                                abs_no_of_pages = 0;
                            }
                            if (txt_abs_pages_cost.Text != "")
                            {

                                 abs_pages_cost =  Convert.ToDecimal( txt_abs_pages_cost.Text.ToString());
                                
                            }
                            else
                            {
                                abs_pages_cost = 0;

                            }

                            if (txt_abs_actual_cost.Text != "")
                            {

                                abs_actual_cost = Convert.ToDecimal( txt_abs_actual_cost.Text.ToString());

                            }
                            else
                            {
                                abs_actual_cost = 0;

                            }



                            htabscost.Add("@Trans", "UPDATE");
                            htabscost.Add("@Order_Id", Order_Id);
                            htabscost.Add("@Abstractor_Id", abstractor_id);
                            htabscost.Add("@No_Of_Pages", abs_no_of_pages);
                            htabscost.Add("@Pages_Cost", abs_pages_cost);
                            htabscost.Add("@Actual_Cost", abs_actual_cost);
                            htabscost.Add("@Modified_By", userid);
                            htabscost.Add("@Modified_Date", date);
                            dtabscost = dataaccess.ExecuteSP("Sp_Abstractor_Order_Cost", htabscost);

                        }

                   

                        /*--------------------------Order history--------------*/
                        Hashtable hthistroy = new Hashtable();
                        DataTable dthistroy = new DataTable();
                        hthistroy.Add("@Trans", "INSERT");
                        hthistroy.Add("@Order_Id", Order_Id);
                        //hthistroy.Add("@User_Id", Tree_View_UserId);
                        hthistroy.Add("@Status_Id", ddl_Abstractor_Task.SelectedValue);
                        hthistroy.Add("@Progress_Id", ddl_Abstractor_Status.SelectedValue);
                        hthistroy.Add("@Assigned_By", userid);
                        hthistroy.Add("@Modification_Type", "Abstractor order updated");
                        hthistroy.Add("@Work_Type", 1);
                        dthistroy = dataaccess.ExecuteSP("Sp_Order_History", hthistroy);

                   
                        MessageBox.Show("Record Updated Sucessfully");
                        Abstractor_Order_View av = new Abstractor_Order_View(0, 0,User_Role_Id);
                        av.Close();
                        this.Close();
                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }
                }


            }
            else
            {
                // Do something
            }
        }

        public bool validate()
        {

            if (txt_Abstractor_Returned_Date.Text == "" || txt_Abstractor_Returned_Date.Text==" ")
            {

                MessageBox.Show("Please enter Returned date");
                txt_Abstractor_Returned_Date.Focus();
                return false;
            }
           
            if(txt_abs_no_of_pages.Text=="")
            {

                MessageBox.Show("Please enter no of pages");
                txt_Abstractor_Returned_Date.Focus();
                return false;
            }

            if (txt_abs_pages_cost.Text == "")
            {
                MessageBox.Show("Please enter pages cost");
                txt_Abstractor_Returned_Date.Focus();
                return false;
            

            }
            if (txt_abs_actual_cost.Text == "")
            {
                MessageBox.Show("Please enter actual cost");
                txt_Abstractor_Returned_Date.Focus();
                return false;

            }
            if (int.Parse(ddl_Abstractor_Status.SelectedValue.ToString()) == 6)
            {
                MessageBox.Show("Status Should not be in open state");
                txt_Abstractor_Returned_Date.Focus();
                return false;

            }
            return true;
        }

        private void txt_Abstractor_Returned_Date_ValueChanged(object sender, EventArgs e)
        {
            if (DateCustom != 0)
            {
                txt_Abstractor_Returned_Date.CustomFormat = "MM/dd/yyyy";
            }
            DateCustom = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.SelectedValue.ToString(), ddl_SubProcess.SelectedValue.ToString());
            Orderuploads.Show();

        }

        private void btn_Abstractor_Compose_Email_Click(object sender, EventArgs e)
        {
            Abstractor_Compose_Email abst_comp_Email = new Abstractor_Compose_Email(Order_Id, int.Parse(ddl_Abstractor_Name.SelectedValue.ToString()),userid);
            abst_comp_Email.Show();
        }

    
      

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    Send_Email sendemail = new Send_Email();
        //    sendemail.Show();
        //}

   
      
      
        


   
    }
}
