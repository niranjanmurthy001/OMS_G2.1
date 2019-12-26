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


namespace Ordermanagement_01
{
    public partial class Order_Entry : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        private Point pt, pt1, ordertask_lbl, grp_pt, grp_pt1, grd_ordertask, grd_ordertask1, grd_orderprogress, grd_orderprogress1, grd_ordererr, grd_ordererr1, grd_ordercmt, grd_ordercmt1, form_pt, form1_pt;
        private Point ordertask_lbl1, orderprog_lbl, orderprog_lbl1, ordererror, ordererror1, ordercomment, ordercomment1;
        string Order_Target, Time_Zone, Recived_Time;
        int Order_Id=0;
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
        int DateCustom = 0;
        public Order_Entry(int Orderid,int User_Id)
        {
            
            InitializeComponent();
           // Clear();
            pnl_visible();
           // Order_Entry.ActiveForm.Width = 1045;
            dbc.BindClientName(ddl_ClientName);
            dbc.BindOrderType(ddl_ordertype);
            userid = User_Id;
            dbc.BindState(ddl_State);
            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            dbc.BindSubProcessName(ddl_SubProcess, clientid);
            ddl_ordertask.Visible = true;
            ddl_ordertask.Items.Insert(0, "Search");
            ddl_ordertask.Items.Insert(1, "Typing");
            ddl_ordertask.Items.Insert(2, "Upload");

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
            Order_Id=Orderid;
           
           // SetMyCustomFormat();
            
            
            if (ddl_ClientName.SelectedIndex != 0)
            {
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
        }
        protected void Get_Order_Search_Cost_Details()
        {

            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT");
            ht_Select_Order_Details.Add("@Order_Id", Order_Id);

            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                ddl_Order_Source.Text = dt_Select_Order_Details.Rows[0]["Source"].ToString();
                txt_Search_cost.Text = dt_Select_Order_Details.Rows[0]["Search_Cost"].ToString();
                txt_Copy_cost.Text = dt_Select_Order_Details.Rows[0]["Copy_Cost"].ToString();
                txt_Abstractor_Cost.Text = dt_Select_Order_Details.Rows[0]["Abstractor_Cost"].ToString();
                txt_noofpage.Text = dt_Select_Order_Details.Rows[0]["No_Of_pages"].ToString();
            }
        }
        //public void SetMyCustomFormat()
        //{
        //    // Set the Format type and the CustomFormat string.
        //    ddl_Hour.Format = DateTimePickerFormat.Custom;
        //    ddl_Hour.CustomFormat = "hh";
        //    ddl_Minute.Format = DateTimePickerFormat.Custom;
        //    ddl_Minute.CustomFormat = "mm";
        //    ddl_Sec.Format = DateTimePickerFormat.Custom;
        //    ddl_Sec.CustomFormat = "ss";

        //}
        private void Order_Controls_Load()
        {
            if (btn_Save.Text == "Edit Order" && txt_OrderNumber.Text != "")
            {
              //  btn_Save.Text = "Submit";
                Control_Enable_false();
            }
            else if (btn_Save.Text != "Edit Order" && txt_OrderNumber.Text == "")
            {
                btn_Save.Text = "Add New Order";
                Control_Enable_false();
            }
            //else if (btn_Save.Text == "Add New Order")
            //{

            //    Control_Enable_false();
            //}
        }

        private void Order_Entry_Load(object sender, EventArgs e)
        {
           
            if (Order_Id == 0)
            {

                btn_Save.Text = "Add New Order";
            }
            else
            {
                //Control_Enable_false();
                btn_Save.Text = "Edit";
            }
            //Order_Controls_Load();
           pnlSideTree.Visible = false;

           lbl_Order_Prior_Date.Visible = false;
           txt_Order_Prior_Date.Visible = false;
           lbl_order_Prior_mark.Visible = false;
           //Order_Entry.ActiveForm.Width=1045;
         //  Order_Entry.ActiveForm.Height=731;
           //treeView1.Visible = false;
            pnl_visible();
            
            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            dbc.BindSubProcessName(ddl_SubProcess, clientid);
            if (ddl_ClientName.SelectedIndex != 0)
            {
              //  int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
            Order_Load();
            Get_Order_Search_Cost_Details();
            GridviewbindUser_Task_Orderdata();
            GridviewbindUser_Task_Status_Orderdata();
            Geydview_Bind_Comments();
            grd_Error_Bind();
            if (Order_Id == 0)
            {
                Clear();
            }
            
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

            if (pnlSideTree.Visible != true)
            {
                //hide panel
                // pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_OrderTask.Location = ordertask_lbl;
                lbl_OrderProgress.Location = orderprog_lbl;
                lbl_OrderError.Location = ordererror;
                lbl_OrderComment.Location = ordercomment;
                grid_OrderTask.Location = grd_ordertask;
                grid_OrderProgress.Location = grd_orderprogress;
                grd_Error.Location = grd_ordererr;
                Grid_Comments.Location = grd_ordercmt;
                grp_OrderEntry.Location = grp_pt;
            //    Order_Entry.ActiveForm.Width = 1045;
                //Order_Entry.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            //show panel
               // pnlSideTree.Visible = true;
            
                
           
        }
        private void Order_Load()
        {
            if (Order_Id != 0)
            {
                Control_Enable_false();
                btn_Save.Text = "Edit Order";
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
                        dbc.BindSubProcessName(ddl_SubProcess, clientid);
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
                    txt_Order_Prior_Date.Text = dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString();
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
                Order_Controls_Load();
            }
            else
            {
                btn_Save.Text = "Add New Order";
            }

        }
        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ClientName.SelectedIndex != 0)
            {
                int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
               // ddl_SubProcess.Focus();

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
            User_Chk_Img.Image = null;
            if (int.Parse(dtorder.Rows[0]["count"].ToString()) > 0)
            {
                User_Chk_Img.Image = Image.FromFile(Environment.CurrentDirectory + @"\Delete1.png");


            }
            else if (int.Parse(dtorder.Rows[0]["count"].ToString()) <= 0)
            {
                User_Chk_Img.Image = Image.FromFile(Environment.CurrentDirectory + @"\Sucess.png");

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
            if (e.KeyCode == Keys.Enter)
            {
                txt_Date.Focus();
            }
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
        private bool Validation()
        {


            if (ddl_ClientName.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Customer Name");
             //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select Customer Name')</script>", false);
                ddl_ClientName.Focus();
              //  ddl_ClientName.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

             //   ddl_ClientName.BorderColor = System.Drawing.Color.DarkBlue;
            }
            if (ddl_SubProcess.SelectedIndex <= 0)
            {
                MessageBox.Show("Select SubProcess");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select SubProcess')</script>", false);
                ddl_SubProcess.Focus();
             //   ddl_SubProcess.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

             //   ddl_SubProcess.BorderColor = System.Drawing.Color.DarkBlue;
            }
            //if (ddl_Search_Type.SelectedIndex <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select Search Type')</script>", false);
            //    ddl_Search_Type.Focus();
            //    ddl_Search_Type.BorderColor = System.Drawing.Color.Red;
            //    return false;

            //}
            //else
            //{

            //    ddl_Search_Type.BorderColor = System.Drawing.Color.DarkBlue;
            //}

            if (ddl_ordertype.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Order Type");
               // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select Order Type')</script>", false);
                ddl_ordertype.Focus();
             //   ddl_ordertype.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

               // ddl_ordertype.BorderColor = System.Drawing.Color.DarkBlue;
            }
            if (txt_OrderNumber.Text == "")
            {
                MessageBox.Show("Enter Order Number");
             //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter Order Number')</script>", false);
                txt_OrderNumber.Focus();
              //  txt_OrderNumber.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

                //txt_OrderNumber.BorderColor = System.Drawing.Color.DarkBlue;
            }
            if (ddl_State.SelectedIndex <= 0)
            {
                MessageBox.Show("Select State");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select State')</script>", false);
                ddl_State.Focus();
                //ddl_State.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

              //  ddl_State.BorderColor = System.Drawing.Color.DarkBlue;
            }
            if (ddl_County.SelectedIndex <= 0)
            {
                MessageBox.Show("Select County");
               // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select County')</script>", false);
                ddl_County.Focus();
               // ddl_County.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

               // ddl_County.BorderColor = System.Drawing.Color.DarkBlue;
            }

            if (txt_Borrowername.Text == "")
            {
                MessageBox.Show("Enter Borrower Name");
             //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('')</script>", false);
                txt_Borrowername.Focus();
             //   txt_Borrowername.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

               // txt_Borrowername.BorderColor = System.Drawing.Color.DarkBlue;
            }

     
            if (txt_Date.Text == "")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter Date Name')</script>", false);
                MessageBox.Show("Enter Date Name");
                txt_Date.Focus();
              //  txt_Date.BorderColor = System.Drawing.Color.Red;
                return false;
            }
            else
            {
              // txt_Date.BorderColor = System.Drawing.Color.DarkBlue;
            }

            return true;

        }
        private bool Validate_OrderNo()
        {   
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "CHECK_ORDER_NUMBER");
            htselect.Add("@Client_Order_Number", txt_OrderNumber.Text);
            dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
            if (dtselect.Rows.Count > 0)
            {

                chk_Order_no = int.Parse(dtselect.Rows[0]["count"].ToString());

            }
            else
            {

                chk_Order_no = 0;

            }
            if (chk_Order_no > 0)
            {
                MessageBox.Show("The Order Number " + txt_OrderNumber.Text + " Alredy Exist");
              //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('The Order Number " + txt_OrderNumber.Text + " Alredy Exist' )</script>", false);
                return false;
            }
            else
            {

                return true;
            }


        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
           



                if (btn_Save.Text == "Edit Order")
                {
                    btn_Save.Text = "Submit";
                    Control_Enable();
                }
                else if (btn_Save.Text != "Submit" && btn_Save.Text != "Edit Order")
                {
                    btn_Save.Text = "Add New Order";
                    Control_Enable();
                }
                string message = "Are You Proceed?";
                string title = "Submitting";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

               
                if (ddl_ordertask.SelectedItem != null)
                {
                    Hashtable htuser = new Hashtable();
                    DataTable dtuser = new System.Data.DataTable();
                    htuser.Add("@Trans", "SELECT_STATUSID");
                    htuser.Add("@Order_Status", ddl_ordertask.SelectedItem);
                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);

                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.Yes)
                    {
                
                        if (btn_Save.Text == "Add New Order" && Validate_OrderNo() != false && Validation() != false && validate_Update_Search() != false)
                        {
                            Get_Maximum_OrderNumber();

                            string ordernumber = txt_OrderNumber.Text.ToString();

                            int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());

                            int subprocessid = int.Parse(ddl_SubProcess.SelectedValue.ToString());

                            string address = txt_Address.Text.ToUpper().ToString();

                            int state = int.Parse(ddl_State.SelectedValue.ToString());
                            int county = int.Parse(ddl_County.SelectedValue.ToString());

                            string Barrowername = txt_Borrowername.Text.ToUpper().ToString();
                            //    string Search_type = ddl_Search_Type.SelectedItem.ToString();
                            string Client_Order_Ref = txt_Client_order_ref.Text;
                            string Notes = txt_Notes.Text.ToUpper().ToString();
                            string City = txt_City.Text.ToString();
                            if (txt_Zip.Text != "")
                            {
                                zipcode = Convert.ToDouble(txt_Zip.Text);
                            }

                            if (chk_est_Time.Checked == true)
                            {
                                //string Recived_Time = ddl_Hour.SelectedItem.ToString() + ":" + ddl_Minute.SelectedItem.ToString() + ":" + ddl_Sec.SelectedItem.ToString();
                                DateTime time1 = new DateTime(2008, 12, 11, 6, 0, 0);  // your DataTimeVariable
                                //  TimeZoneInfo timeZone1 = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                TimeZoneInfo timeZone2 = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                                // DateTime newTime = TimeZoneInfo.ConvertTime(time1, timeZone1, timeZone2);
                            }

                            Recived_Time = ddl_Hour.Text + ":" + ddl_Minute.Text + ":" + ddl_Sec.Text;


                            Hashtable htorder = new Hashtable();
                            DataTable dtorder = new DataTable();


                            DateTime date = new DateTime();
                            date = DateTime.Now;
                            string dateeval = date.ToString("dd/MM/yyyy");
                            htorder.Add("@Trans", "INSERT");
                            if (chk_est_Time.Checked != true)
                            {

                                htorder.Add("@Recived_Time", Recived_Time);
                            }
                            else if (chk_est_Time.Checked == true)
                            {
                                string Cst_Time = txt_Date.Text + " " + ddl_Hour.Text + ":" + ddl_Minute.Text + ":" + ddl_Sec.Text;
                                DateTime EST_TIME = Convert.ToDateTime(Cst_Time);
                                TimeZoneInfo timeZone1 = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                                TimeZoneInfo IndianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                                DateTime Indiandate = TimeZoneInfo.ConvertTime(EST_TIME, timeZone1, IndianZone);
                                string IndianTime = Indiandate.ToString("HH:mm:ss");
                                htorder.Add("@Recived_Time", IndianTime);
                            }
                            htorder.Add("@Sub_ProcessId", subprocessid);
                            htorder.Add("@Date", txt_Date.Text);
                            htorder.Add("@Order_Type", ordertype);
                            htorder.Add("@Order_Number", MAX_ORDER_NUMBER.ToString());
                            htorder.Add("@Client_Order_Number", ordernumber.ToString());
                            htorder.Add("@Documents", "");
                            htorder.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                            htorder.Add("@Client_Order_Ref", Client_Order_Ref);
                            // htorder.Add("@Search_Type", Search_type);
                            htorder.Add("@Order_Progress", 8);
                            htorder.Add("@Address", address);
                            htorder.Add("@State", state);
                            htorder.Add("@County", county);
                            htorder.Add("@APN", txt_APN.Text);
                            htorder.Add("@City", City.ToString());
                            htorder.Add("@Zip", zipcode.ToString());
                            htorder.Add("@Borrower_Name", Barrowername);
                            htorder.Add("@Notes", Notes);
                            htorder.Add("@Total_Cost", Totalcost);
                            htorder.Add("@Order_Prior_Date", txt_Order_Prior_Date.Text);
                            htorder.Add("@Paid", "");
                            htorder.Add("@Id", 0);
                            htorder.Add("@Opened", date);
                            htorder.Add("@Action_Date", date);
                            htorder.Add("@Turn_Time", "");
                            htorder.Add("@Inserted_By", userid);
                            htorder.Add("@Inserted_date", date);
                            htorder.Add("@Recived_Date", txt_Date.Text);


                            htorder.Add("@status", true);

                            dtorder = dataaccess.ExecuteSP("Sp_Order", htorder);
                            Get_Maximum_OrderNumber();
                            MessageBox.Show("New Order Added Sucessfully");
                            Clear();
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('New Order Added Sucessfully')</script>", false);
                            //   GridviewbindOrderdata();
                            Get_Maximum_OrderNumber();

                            Control_Enable();
                        }
                        else
                        {


                        }
                             }
             

                        else if (btn_Save.Text == "Submit" && Validation() != false && validate_Update_Search() != false)
                        {

                            string ordernumber = txt_OrderNumber.Text.ToString();

                            int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                            int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                            //        string Search_type = ddl_Search_Type.SelectedItem.ToString();
                            string Client_Order_Ref = txt_Client_order_ref.Text;
                            int subprocessid = int.Parse(ddl_SubProcess.SelectedValue.ToString());


                            string address = txt_Address.Text.ToUpper().ToString();


                            int state = int.Parse(ddl_State.SelectedValue.ToString());
                            int county = int.Parse(ddl_County.SelectedValue.ToString());

                            string Barrowername = txt_Borrowername.Text.ToUpper().ToString();


                            string Notes = txt_Notes.Text.ToUpper().ToString();

                            string City = txt_City.Text.ToString();
                            if (txt_Zip.Text != "")
                            {
                                zipcode = Convert.ToDouble(txt_Zip.Text);
                            }


                            string Recived_Time = ddl_Hour.Text + ":" + ddl_Minute.Text + ":" + ddl_Sec.Text;

                            int orderid = Order_Id;

                            Hashtable htorder = new Hashtable();
                            DataTable dtorder = new DataTable();


                            DateTime date = new DateTime();
                            date = DateTime.Now;
                            string dateeval = date.ToString("dd/MM/yyyy");
                            htorder.Add("@Trans", "UPDATE");
                            htorder.Add("@Order_ID", orderid);
                            htorder.Add("@Date", txt_Date.Text);
                            htorder.Add("@Sub_ProcessId", subprocessid);

                            htorder.Add("@Order_Type", ordertype);
                            htorder.Add("@Client_Order_Number", ordernumber);
                            htorder.Add("@Client_Order_Ref", Client_Order_Ref);
                            // htorder.Add("@Search_Type", Search_type);
                            // htorder.Add("@Order_Status", OrderStatus);
                            htorder.Add("@Documents", "");

                            htorder.Add("@Address", address);
                            htorder.Add("@APN", txt_APN.Text);
                            htorder.Add("@City", City.ToString());
                            htorder.Add("@Zip", zipcode.ToString());
                            htorder.Add("@State", state);

                            htorder.Add("@County", county);
                            htorder.Add("@Borrower_Name", Barrowername);

                            htorder.Add("@Notes", Notes);
                            htorder.Add("@Order_Prior_Date", txt_Order_Prior_Date.Text);
                            htorder.Add("@Total_Cost", Totalcost);
                            htorder.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                            htorder.Add("@Order_Progress", 8);
                            htorder.Add("@Paid", "");
                            htorder.Add("@Id", 0);
                            // htorder.Add("@Opened", date);
                            htorder.Add("@Action_Date", date);
                            //   htorder.Add("@Turn_Time", "");
                            htorder.Add("@Inserted_By", userid);
                            htorder.Add("@Inserted_date", date);
                            htorder.Add("@Recived_Date", txt_Date.Text);
                            htorder.Add("@Recived_Time", Recived_Time);
                            htorder.Add("@status", true);
                            dtorder = dataaccess.ExecuteSP("Sp_Order", htorder);

                            Hashtable htorderStatus = new Hashtable();
                            DataTable dtorderStatus = new DataTable();
                            htorderStatus.Add("@Trans", "UPDATE_STATUS");
                            htorderStatus.Add("@Order_ID", orderid);
                            htorderStatus.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                            htorderStatus.Add("@Modified_By", userid);
                            htorderStatus.Add("@Modified_Date", date);
                            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                            Hashtable htorderProgress = new Hashtable();
                            DataTable dtorderProgress = new DataTable();
                            htorderProgress.Add("@Trans", "UPDATE_PROGRESS");
                            htorderProgress.Add("@Order_ID", orderid);
                            htorderProgress.Add("@Order_Progress", 8);
                            htorderProgress.Add("@Modified_By", userid);
                            htorderProgress.Add("@Modified_Date", date);
                            dtorderProgress = dataaccess.ExecuteSP("Sp_Order", htorderProgress);
                            MessageBox.Show("Order Updated Sucessfully");
                            Clear();
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Updated Sucessfully')</script>", false);
                            btn_Save.Text = "Add New Order";
                            //  GridviewbindOrderdata();
                        }
                        Hashtable ht_Select_Order_Details = new Hashtable();
                        DataTable dt_Select_Order_Details = new DataTable();

                        ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                        ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                        dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                        if (dt_Select_Order_Details.Rows.Count > 0)
                        {

                            Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                        }
                        else
                        {

                            Chk_Order_Search_Cost = 0;
                        }

                        if (Chk_Order_Search_Cost > 0 && btn_Save.Text != "Edit")
                        {

                            OPERATE_SEARCH_COST = "UPDATE";
                            Insert_Order_Search_Cost(sender, e);

                        }
                        else if (Chk_Order_Search_Cost == 0 && btn_Save.Text != "Edit")
                        {
                            OPERATE_SEARCH_COST = "INSERT";
                            Insert_Order_Search_Cost(sender, e);
                        }

                        //     Clear();
                        // Get_Maximum_OrderNumber();

                        else
                        {
                            MessageBox.Show("Select Order Task");
                        }
                }
               
        }

        public bool validate_Update_Search()
        {
            int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());

            if (ordertype == 7 && txt_Order_Prior_Date.Text == " ")
            {
                lbl_Order_Prior_Date.Visible = true;
                txt_Order_Prior_Date.Visible = true;
                lbl_order_Prior_mark.Visible = true;
                MessageBox.Show("Please Enter Order Prior Date");
                return false;

            }
            else
            {
                lbl_Order_Prior_Date.Visible = false;
                txt_Order_Prior_Date.Visible = false;
                lbl_order_Prior_mark.Visible = false;
                return true;
            }

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
        protected void Insert_Order_Search_Cost(object sender, EventArgs e)
        {
          

            if (txt_Search_cost.Text != "") { SearchCost = Convert.ToDecimal(txt_Search_cost.Text.ToString()); } else { SearchCost = 0; }
            if (txt_Copy_cost.Text != "") { Copy_Cost = Convert.ToDecimal(txt_Copy_cost.Text.ToString()); } else { Copy_Cost = 0; }
            if (txt_Abstractor_Cost.Text != "") { Abstractor_Cost = Convert.ToDecimal(txt_Abstractor_Cost.Text.ToString()); } else { Abstractor_Cost = 0; }

            if (txt_noofpage.Text != "") { No_Of_Pages = Convert.ToInt32(txt_noofpage.Text.ToString()); } else { No_Of_Pages = 0; }
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new System.Data.DataTable();

            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            string time = date.ToString("hh:mm tt");
            if (OPERATE_SEARCH_COST == "INSERT")
            {
                htsearch.Add("@Trans", "INSERT");
                htsearch.Add("@Order_Id",Order_Id);
                if (ddl_Order_Source.SelectedItem != null)
                {
                    htsearch.Add("@Source",  ddl_Order_Source.SelectedItem.ToString());
                }
              
                htsearch.Add("@Search_Cost", SearchCost);
                htsearch.Add("@Copy_Cost", Copy_Cost);
                htsearch.Add("@Abstractor_Cost", Abstractor_Cost);
                htsearch.Add("@No_Of_pages", No_Of_Pages);
                htsearch.Add("@Inserted_By", userid);
                htsearch.Add("@Inserted_date", date);

                htsearch.Add("@status", "True");
                dtsearch = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htsearch);
            }
            else if (OPERATE_SEARCH_COST == "UPDATE")
            {

                htsearch.Add("@Trans", "UPDATE");
                htsearch.Add("@Order_Id", Order_Id);
                if (ddl_Order_Source.SelectedItem != null)
                {
                    htsearch.Add("@Source", ddl_Order_Source.SelectedItem.ToString());
                }
                htsearch.Add("@Search_Cost", SearchCost);
                htsearch.Add("@Copy_Cost", Copy_Cost);
                htsearch.Add("@Abstractor_Cost", Abstractor_Cost);
                htsearch.Add("@No_Of_pages", No_Of_Pages);

                htsearch.Add("@Modified_By", userid);
                htsearch.Add("@Modified_Date", date);
                htsearch.Add("@status", "True");
                dtsearch = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htsearch);
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
            txt_Zip.Text="";
            ddl_ordertask.SelectedIndex = 0;
           // ddl_Search_Type.SelectedIndex = 0;
            ddl_Order_Source.SelectedIndex = 0;
            txt_Search_cost.Text = "";
            txt_Copy_cost.Text = "";
            txt_Abstractor_Cost.Text="";
            txt_noofpage.Text = "";
            txt_Notes.Text = "";
            lbl_County_Type.Text = "";
        }
        private void Control_Enable()
        {
            ddl_ClientName.Enabled = true;
            ddl_SubProcess.Enabled = true;
            ddl_Hour.Enabled = true;
            ddl_Minute.Enabled = true;
            ddl_Sec.Enabled = true;
            txt_Date.Enabled = true;
            ddl_ordertype.Enabled = true;
            txt_OrderNumber.Enabled = true;
            txt_APN.Enabled = true;
            txt_Client_order_ref.Enabled = true;
            txt_Borrowername.Enabled = true;
            txt_Address.Enabled = true;
            ddl_State.Enabled = true;
            ddl_County.Enabled = true;
            txt_City.Enabled = true;
            txt_Zip.Enabled = true;
            ddl_ordertask.Enabled = true;
          //  ddl_Search_Type.Enabled = true;
            ddl_Order_Source.Enabled = true;
            txt_Search_cost.Enabled = true;
            txt_Copy_cost.Enabled = true;
            txt_Abstractor_Cost.Enabled = true;
            txt_noofpage.Enabled = true;
            txt_Notes.Enabled = true;
            lbl_County_Type.Text = "";

            txt_Search_cost.ReadOnly = false;
            txt_Copy_cost.ReadOnly = false;
            txt_Abstractor_Cost.ReadOnly = false;
            txt_noofpage.ReadOnly = false;
            txt_Notes.ReadOnly = false;
            txt_OrderNumber.ReadOnly = false;
            txt_APN.ReadOnly = false;
            txt_Client_order_ref.ReadOnly = false;
            txt_Borrowername.ReadOnly = false;
            txt_Address.ReadOnly = false;
            txt_City.ReadOnly = false;
            txt_Zip.ReadOnly = false;
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Control_Enable();
        }
        protected void GridviewbindUser_Task_Orderdata()
        {
            int order_id = Order_Id;
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();

            htselect.Add("@Trans", "GET_USER_TASK_DETAILS_FOR_ORDER");
            htselect.Add("@Order_ID", order_id);
            dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
            grid_OrderTask.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.OliveDrab;
            grid_OrderTask.EnableHeadersVisualStyles = false;
            grid_OrderTask.Columns[0].Width=90;
            grid_OrderTask.Columns[1].Width=105;
            grid_OrderTask.Columns[2].Width=90;
            grid_OrderTask.Columns[3].Width=110;
            grid_OrderTask.Columns[4].Width=90;
            if (dtselect.Rows.Count > 0)
            {
                grid_OrderTask.Rows.Clear();
                 for (int i = 0; i < dtselect.Rows.Count; i++)
                 {
                     grid_OrderTask.Rows.Add();
                     grid_OrderTask.Rows[i].Cells[0].Value = dtselect.Rows[i]["search"].ToString();
                     grid_OrderTask.Rows[i].Cells[1].Value = dtselect.Rows[i]["Search Qc"].ToString();
                     grid_OrderTask.Rows[i].Cells[2].Value = dtselect.Rows[i]["Typing"].ToString();
                     grid_OrderTask.Rows[i].Cells[3].Value = dtselect.Rows[i]["Typing Qc"].ToString();
                     grid_OrderTask.Rows[i].Cells[4].Value = dtselect.Rows[i]["Upload"].ToString();
                     
                    
                 }
            }
            else
            {
                grid_OrderTask.Visible = true;
                grid_OrderTask.DataSource = null;
              

            }
        }
        protected void GridviewbindUser_Task_Status_Orderdata()
        {

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();

            htselect.Add("@Trans", "GET_TASK_STATUS");
            htselect.Add("@Order_ID", Order_Id);
            dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
            grid_OrderProgress.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.OliveDrab;
            grid_OrderProgress.EnableHeadersVisualStyles = false;
            grid_OrderProgress.Columns[0].Width = 140;
            grid_OrderProgress.Columns[1].Width = 105;
            grid_OrderProgress.Columns[2].Width = 140;
            grid_OrderProgress.Columns[3].Width = 105;
            grid_OrderProgress.Columns[4].Width = 90;
            if (dtselect.Rows.Count > 0)
            {
                grid_OrderProgress.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grid_OrderProgress.Rows.Add();
                    grid_OrderProgress.Rows[i].Cells[0].Value = dtselect.Rows[i]["search"].ToString();
                    grid_OrderProgress.Rows[i].Cells[1].Value = dtselect.Rows[i]["Searh QC"].ToString();
                    grid_OrderProgress.Rows[i].Cells[2].Value = dtselect.Rows[i]["Typing"].ToString();
                    grid_OrderProgress.Rows[i].Cells[3].Value = dtselect.Rows[i]["Typing Qc"].ToString();
                    grid_OrderProgress.Rows[i].Cells[4].Value = dtselect.Rows[i]["Upload"].ToString();

                }
             

            }
            else
            {
                grid_OrderProgress.Visible = true;
                grid_OrderProgress.DataSource = null;
               

            }
        }
        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT");
            htComments.Add("@Order_Id",Order_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_Comments", htComments);
            Grid_Comments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.OliveDrab;
            Grid_Comments.EnableHeadersVisualStyles = false;
            Grid_Comments.Columns[0].Width = 350;
            Grid_Comments.Columns[1].Width = 125;
            if (dtComments.Rows.Count > 0)
            {
                Grid_Comments.Rows.Clear();
               
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    Grid_Comments.Rows.Add();
                  //  Grid_Comments.Rows[i].Cells[0].Value = dtComments.Rows[i]["Comment_Id"].ToString();
                    Grid_Comments.Rows[i].Cells[0].Value = dtComments.Rows[i]["Comment"].ToString();
                    Grid_Comments.Rows[i].Cells[1].Value = dtComments.Rows[i]["User_Name"].ToString();
                }
            }
            else
            {
               



            }


        }
        protected void grd_Error_Bind()
        {
            Hashtable htNotes = new Hashtable();
            DataTable dtNotes = new System.Data.DataTable();
           // int Order_id = int.Parse(Session["order_id"].ToString());
            htNotes.Add("@Trans", "BIND");
            htNotes.Add("@Order_Id", Order_Id);
            dtNotes = dataaccess.ExecuteSP("Sp_Order_Notes", htNotes);
            grd_Error.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Brown;
            grd_Error.EnableHeadersVisualStyles = false;
            grd_Error.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            grd_Error.Columns[0].Width = 150;
            grd_Error.Columns[1].Width = 250;
            grd_Error.Columns[2].Width = 120;
            grd_Error.Columns[3].Width = 120;
            grd_Error.Columns[4].Width = 95;
            if (dtNotes.Rows.Count > 0)
            {
                grd_Error.Rows.Clear();
                
                for (int i = 0; i < dtNotes.Rows.Count; i++)
                {
                    grd_Error.Rows.Add();

                    grd_Error.Rows[i].Cells[0].Value = dtNotes.Rows[i]["Error_Description"].ToString();
                    grd_Error.Rows[i].Cells[1].Value = dtNotes.Rows[i]["Error_Type"].ToString();
                    grd_Error.Rows[i].Cells[2].Value = dtNotes.Rows[i]["description_Type"].ToString();
                    grd_Error.Rows[i].Cells[3].Value = dtNotes.Rows[i]["Order_Status"].ToString();
                  //  grd_Error.Rows[i].Cells[4].Value = dtNotes.Rows[i]["Upload"].ToString();
                }
            }
            else
            {
            }

        }

        private void btn_treeview_Click(object sender, EventArgs e)
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
            form_pt.X = 150; form_pt.Y = 0;
            form1_pt.X = 25; form1_pt.Y = 0;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_OrderTask.Location = ordertask_lbl;
                lbl_OrderProgress.Location = orderprog_lbl;
                lbl_OrderError.Location = ordererror;
                lbl_OrderComment.Location = ordercomment;
                grid_OrderTask.Location = grd_ordertask;
                grid_OrderProgress.Location = grd_orderprogress;
                grd_Error.Location = grd_ordererr;
                Grid_Comments.Location = grd_ordercmt;
                grp_OrderEntry.Location = grp_pt;
                Order_Entry.ActiveForm.Width = 1045;
                Order_Entry.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_OrderTask.Location = ordertask_lbl1;
                lbl_OrderProgress.Location = orderprog_lbl1;
                lbl_OrderError.Location = ordererror1;
                lbl_OrderComment.Location = ordercomment1;
                grid_OrderTask.Location = grd_ordertask1;
                grid_OrderProgress.Location = grd_orderprogress1;
                grd_Error.Location = grd_ordererr1;
                Grid_Comments.Location = grd_ordercmt1;
                grp_OrderEntry.Location = grp_pt1;
                Order_Entry.ActiveForm.Width = 1235;
                Order_Entry.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\left.png");
            }
            AddParent();
        }

        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 189; pt1.Y = 0;
            grp_pt.X = 5; grp_pt.Y = 50;
            grd_ordertask.X = 5; grd_ordertask.Y = 445;
            grp_pt1.X = 200; grp_pt1.Y = 60;
            grd_ordertask1.X = 200; grd_ordertask1.Y = 450;
            ordertask_lbl.X = 275; ordertask_lbl.Y = 20;
            ordertask_lbl1.X = 475; ordertask_lbl1.Y = 20;
            grd_orderprogress.X = 160; grd_orderprogress.Y = 565;
            grd_orderprogress1.X = 350; grd_orderprogress1.Y = 565;
            grd_ordererr.X = 295; grd_ordererr.Y = 565;
            grd_ordererr1.X = 485; grd_ordererr1.Y = 565;
            grd_ordercmt.X = 430; grd_ordercmt.Y = 565;
            grd_ordercmt1.X = 620; grd_ordercmt1.Y = 565;
            form_pt.X = 350; form_pt.Y = 20;
            form1_pt.X = 180; form1_pt.Y = 20;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
               
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_OrderTask.Location = ordertask_lbl;
                lbl_OrderProgress.Location = orderprog_lbl;
                lbl_OrderError.Location = ordererror;
                lbl_OrderComment.Location = ordercomment;
                grid_OrderTask.Location = grd_ordertask;
                grid_OrderProgress.Location = grd_orderprogress;
                grd_Error.Location = grd_ordererr;
                Grid_Comments.Location = grd_ordercmt;
                grp_OrderEntry.Location = grp_pt;
                Order_Entry.ActiveForm.Width = 690;
                Order_Entry.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                
                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_OrderTask.Location = ordertask_lbl1;
                lbl_OrderProgress.Location = orderprog_lbl1;
                lbl_OrderError.Location = ordererror1;
                lbl_OrderComment.Location = ordercomment1;
                grid_OrderTask.Location = grd_ordertask1;
                grid_OrderProgress.Location = grd_orderprogress1;
                grd_Error.Location = grd_ordererr1;
                Grid_Comments.Location = grd_ordercmt1;
                grp_OrderEntry.Location = grp_pt1;
                Order_Entry.ActiveForm.Width = 900;
                //Order_Entry.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\left.png");
            }
            AddParent();
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

              parentnode=  treeView1.Nodes.Add(sKeyTemp, sKeyTemp);

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
            dt = dataaccess.ExecuteSP("Sp_Tree_Orders", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp1 = dt.Rows[i]["Sub_ProcessName"].ToString();
               childnode= parentnode.Nodes.Add(sKeyTemp1, sKeyTemp1);
               AddChilds1(childnode,sKeyTemp1);
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
          dt1 = dataaccess.ExecuteSP("Sp_Tree_Orders", ht1);
          for (int i = 0; i < dt1.Rows.Count; i++)
          {

              sKeyTemp2 = dt1.Rows[i]["Date"].ToString();
              childnode1 = childnode.Nodes.Add(sKeyTemp2, sKeyTemp2);
              AddChilds2(childnode1, sKeyTemp2, sKey1);
          }
      }
        private void AddChilds2(TreeNode childnode1, string sKey2,string Subprocess)
        {
            Hashtable ht2 = new Hashtable();
            DataTable dt2 = new System.Data.DataTable();
            TreeNode childnode2;
            string sKeyTemp3 = sKey2;
            ht2.Add("@Trans", "Order_Date");
            ht2.Add("@Sub_ProcessName", Subprocess);
            ht2.Add("@Month", sKeyTemp3);
            dt2 = dataaccess.ExecuteSP("Sp_Tree_Orders", ht2);
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
            dt2 = dataaccess.ExecuteSP("Sp_Tree_Orders", ht2);
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
                Get_Order_Search_Cost_Details();
                GridviewbindUser_Task_Orderdata();
                GridviewbindUser_Task_Status_Orderdata();
                Geydview_Bind_Comments();
               // Order_Controls_Load();
            }
           
        }

        private void ddl_County_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ddl_State_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads(Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
            Orderuploads.Show();
        }

        private void txt_Order_Prior_Date_ValueChanged(object sender, EventArgs e)
        {
            if (DateCustom != 0)
            {
                txt_Order_Prior_Date.CustomFormat = "MM/dd/yyyy";
            }
            DateCustom = 1;
        }

       

        
        

    }
}
