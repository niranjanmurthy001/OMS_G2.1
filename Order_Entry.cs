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
        public int EmpError = 0;
        object Entered_OrderId;
        Hashtable htuser = new Hashtable();
        DataTable dtuser = new System.Data.DataTable();
        string Order_Target, Time_Zone, Recived_Time;
        int Order_Id=0;
        int userid;
        string Empname, Client_no;
        int Count;
        int BRANCH_ID;
        int No_Of_Orders;
        int client_Id, Subprocess_id;
        string User_Role_Id;
        string OrderStatus;
        int Chk_Userid;
        string Assign_County_Type;
        string MAX_ORDER_NUMBER;
        int chk_Order_no;
        double zipcode;
        decimal SearchCost, Copy_Cost, Abstractor_Cost, Order_Cost;
        int No_Of_Pages;
        object Order_Inserted_Id;
        int Chk_Order_Search_Cost;
        string OPERATE_SEARCH_COST;
        int SubprocessId, clientid;
        decimal Totalcost;
        DialogResult dialogResult;
        int DateCustom = 0;
        
        string btn_text;
        int Session_Order_Task;
        string Vend_date,userroleid;

        int Vendor_Total_No_Of_Order_Recived, Vendor_No_Of_Order_For_each_Vendor, Vendor_Order_capacity;
        decimal Vendor_Order_Percentage;
        int No_Of_Order_Assignd_for_Vendor,Vendor_Id;

        int External_Client_Order_Task_Id, External_Client_Order_Id;
        public Order_Entry(int Orderid,int User_Id,string User_Roleid)
        {
            
            InitializeComponent();
           // Clear();
          //  pnl_visible();
           // Order_Entry.ActiveForm.Width = 1045;
            
            
            dbc.BindOrderType(ddl_ordertype);
            userid = User_Id;
            userroleid = User_Roleid;
            dbc.BindState(ddl_State);
            //clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            //dbc.BindSubProcessName(ddl_SubProcess, clientid);
            dbc.BindAbstractor_Order_Serarh_Type(ddl_Abstractor_Search_Type);
            ddl_ordertask.Visible = true;
            ddl_ordertask.Items.Insert(0, "Search");
            ddl_ordertask.Items.Insert(1, "Searh QC");
            ddl_ordertask.Items.Insert(2, "Typing");
            ddl_ordertask.Items.Insert(3, "Typing QC");
            ddl_ordertask.Items.Insert(4, "Upload");
            ddl_ordertask.Items.Insert(5, "Upload Completed");
            ddl_ordertask.Items.Insert(6, "Abstractor");
            //dbc.BindOrderStatus(ddl_ordertask);

            //ddl_ordertask.SelectedIndex = 1;
         //   ddl_Search_Type.Visible = true;
            // ddl_Search_Type.Items.Insert(0, "SELECT");
            //ddl_Search_Type.Items.Insert(0, "TIER 1");
            //ddl_Search_Type.Items.Insert(1, "TIER 2");
            //ddl_Search_Type.Items.Insert(2, "TIER 2-In house");

            //   ddl_Order_Source.Items.Insert(0, "SELECT");
            ddl_Order_Source.Items.Insert(0, "Online");
            ddl_Order_Source.Items.Insert(1, "Subscription");
            ddl_Order_Source.Items.Insert(2, "Abstractor");
            ddl_Order_Source.Items.Insert(3, "Online/Abstractor");
            ddl_Order_Source.Items.Insert(4, "Online/Data Tree");
            ddl_Order_Source.Items.Insert(5, "Data Trace");
            ddl_Order_Source.Items.Insert(6, "Title Point");
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
        //Ordermanagement_01.Employee_Error_Entry EmployeeError = new Ordermanagement_01.Employee_Error_Entry(User_id, "", Order_Id, 3);
       
        
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
                Hashtable htchek = new Hashtable();
                DataTable dtcheck = new DataTable();
                htchek.Add("@Trans", "CHECK");
                htchek.Add("@Order_ID", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htchek);
                if (dtcheck.Rows.Count > 0)
                {

                    Hashtable ht_Select = new Hashtable();
                    DataTable dt_Select = new DataTable();

                    ht_Select.Add("@Trans", "GET_ORDER_COST");
                    ht_Select.Add("@Order_ID", Order_Id);

                    dt_Select = dataaccess.ExecuteSP("Sp_Order", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {

                        txt_Order_Cost.Text = dt_Select.Rows[0]["Order_Cost"].ToString();
                    }
                    else
                    {
                        txt_Order_Cost.Text = dt_Select_Order_Details.Rows[0]["Order_Cost"].ToString();
                    }

                }
                

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
                ddl_Copy_Type.SelectedIndex = 2;
                btn_Save.Text = "Add New Order";
                this.Text = "Add New Order";
            }
            else
            {
                //Control_Enable_false();
                ddl_ordertask.Enabled = false;
                btn_Save.Text = "Edit";
              
            }
            //Order_Controls_Load();
         
           lbl_Order_Prior_Date.Visible = false;
           txt_Order_Prior_Date.Visible = false;
           lbl_order_Prior_mark.Visible = false;
           
           //Order_Entry.ActiveForm.Width=1045;
         //  Order_Entry.ActiveForm.Height=731;
           //treeView1.Visible = false;
         
            if (userroleid == "1")
            {
                dbc.BindClientName(ddl_ClientName);
                clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
               // dbc.BindSubProcessName(ddl_SubProcess, clientid);
            }
            else if (userroleid == "2")
            {
                dbc.BindClientNo(ddl_ClientName);
                clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
               // dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
            }
            
            dbc.Bind_Client_Email(ddl_Client_Email, clientid);
            dbc.Bind_Order_Assign_Type(ddl_County_Type);
            dbc.BindAbstractor_Order_Serarh_Type(ddl_Abstractor_Search_Type);
            if (ddl_ClientName.SelectedIndex != 0)
            {
              //  int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
            Order_Load();
            if (Order_Id != 0)
            {
                this.Text = "Order No -" + txt_OrderNumber.Text.ToString();

            }
            Get_Order_Search_Cost_Details();
            GridviewbindUser_Task_Orderdata();
            GridviewbindUser_Task_Status_Orderdata();
            Geydview_Bind_Comments();
            //grd_Error_Bind();
            if (Order_Id == 0)
            {
                Clear();
            }
            Size search = TextRenderer.MeasureText(lbl_search.Text, lbl_search.Font);
            lbl_search.Width = search.Width;
            lbl_search.Height = search.Height;
            Size searchqc = TextRenderer.MeasureText(lbl_SearchQC.Text, lbl_SearchQC.Font);
            lbl_SearchQC.Width = searchqc.Width;
            lbl_SearchQC.Height = searchqc.Height;
            Size typing = TextRenderer.MeasureText(lbl_Typing.Text, lbl_Typing.Font);
            lbl_Typing.Width = typing.Width;
            lbl_Typing.Height = typing.Height;
            Size typingqc = TextRenderer.MeasureText(lbl_TypingQC.Text, lbl_TypingQC.Font);
            lbl_TypingQC.Width = typingqc.Width;
            lbl_TypingQC.Height = typingqc.Height;
            Size upload = TextRenderer.MeasureText(lbl_Upload.Text, lbl_Upload.Font);
            lbl_Upload.Width = upload.Width;
            lbl_Upload.Height = upload.Height;


            Size searcht = TextRenderer.MeasureText(lbl_STask.Text, lbl_STask.Font);
            lbl_STask.Width = searcht.Width;
            lbl_STask.Height = searcht.Height;
            Size searchqct = TextRenderer.MeasureText(lbl_ScTask.Text, lbl_ScTask.Font);
            lbl_ScTask.Width = searchqct.Width;
            lbl_ScTask.Height = searchqct.Height;
            Size typingt = TextRenderer.MeasureText(lbl_Ttask.Text, lbl_Ttask.Font);
            lbl_Ttask.Width = typingt.Width;
            lbl_Ttask.Height = typingt.Height;
            Size typingqct = TextRenderer.MeasureText(lbl_TcTask.Text, lbl_TcTask.Font);
            lbl_TcTask.Width = typingqct.Width;
            lbl_TcTask.Height = typingqct.Height;
            Size uploadt = TextRenderer.MeasureText(lbl_UTask.Text, lbl_UTask.Font);
            lbl_UTask.Width = uploadt.Width;
            lbl_UTask.Height = uploadt.Height;

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Client_ID");
            htselect.Add("@Client_Id", clientid);
            dtselect = dataaccess.ExecuteSP("Sp_Client", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Client_no = dtselect.Rows[0]["Client_Number"].ToString();
            }

        }
       
        private void Order_Load()
        {
            if (Order_Id != 0)
            {
                Control_Enable_false();

                if (userid == 1 || userid == 4 || userid == 99 || userid==33|| userid==38)
                {

                }
                else
                {

                    txt_Order_Cost.Enabled = false;
                    txt_Order_Cost.PasswordChar = '*';
                    txt_Search_cost.PasswordChar = '*';
                    txt_Copy_cost.PasswordChar='*';
                    txt_Abstractor_Cost.PasswordChar = '*';
                    txt_noofpage.PasswordChar = '*';



                }
                



                btn_Save.Text = "Edit Order";
                Hashtable ht_Select_Order_Details = new Hashtable();
                DataTable dt_Select_Order_Details = new DataTable();

                ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_WISE");
                ht_Select_Order_Details.Add("@Order_ID", Order_Id);
                dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);
                
                if (dt_Select_Order_Details.Rows.Count > 0)
                {
                    if (dt_Select_Order_Details.Rows[0]["Deed_Chain"].ToString() != "" && dt_Select_Order_Details.Rows[0]["Deed_Chain"].ToString() == "True")
                    {
                        rdo_Deedchain.Checked = true;
                    }
                    else if (dt_Select_Order_Details.Rows[0]["Deed_Chain"].ToString() == "False" || dt_Select_Order_Details.Rows[0]["Deed_Chain"].ToString() =="")
                    {
                        rdo_Deedchain.Checked = false;
                    }
                    //ViewState["Orderid"] = order_Id.ToString();
                    //Session["order_id"] = order_Id.ToString();
                    txt_OrderNumber.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                    txt_Date.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();

                    ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                     int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                    

                     if (ordertype == 7 && txt_Order_Prior_Date.Text == " " && dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString()=="")
                     {
                         lbl_Order_Prior_Date.Visible = true;
                         txt_Order_Prior_Date.Visible = true;
                         lbl_order_Prior_mark.Visible = true;

                     }
                     else if (ordertype == 7 && dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString() != "")
                     {
                         DateCustom = 1;
                         lbl_Order_Prior_Date.Visible = true;
                         txt_Order_Prior_Date.Visible = true;
                         lbl_order_Prior_mark.Visible = true;
                         
                         txt_Order_Prior_Date.Text = dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString();

                     }
                     else
                     {

                         lbl_Order_Prior_Date.Visible = false;
                         txt_Order_Prior_Date.Visible = false;
                         lbl_order_Prior_mark.Visible = false;
                     }


                     if (userroleid == "1")
                     {
                         ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                         if (ddl_ClientName.SelectedIndex > 0)
                         {

                             clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                             dbc.BindSubProcessName(ddl_SubProcess, clientid);
                         }
                     }
                     else if (userroleid == "2")
                     {
                         ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                         if (ddl_ClientName.SelectedIndex > 0)
                         {

                             clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                             dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                         }
                     }
                    

                    ddl_SubProcess.SelectedValue = dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString();
                    dbc.Bind_Order_Assign_Type(ddl_County_Type);
                    txt_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                    txt_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                    txt_City.Text = dt_Select_Order_Details.Rows[0]["City"].ToString();
                    txt_Zip.Text = dt_Select_Order_Details.Rows[0]["Zip"].ToString();
                    ddl_State.SelectedValue = dt_Select_Order_Details.Rows[0]["stateid"].ToString();
                    txt_Abstractor_Notes.Text = dt_Select_Order_Details.Rows[0]["Abstractor_Note"].ToString();
                    ddl_Abstractor_Search_Type.SelectedValue = dt_Select_Order_Details.Rows[0]["Abstractor_Search_Type_ID"].ToString();
                    ddl_Client_Email.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Email_Id"].ToString();
                    //ddl_Search_Type.SelectedItem = lblSearch_Type.Text;
                    //  ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                    //  ddl_Search_Type.Text = dt_Select_Order_Details.Rows[0]["Search_Type"].ToString();
                    txt_Client_order_ref.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Ref"].ToString();
                    lbl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();
                   // Assign_County_Type = dt_Select_Order_Details.Rows[0]["Order_Assign_Type"].ToString();
                    ddl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();
                    ddl_Hour.Text = dt_Select_Order_Details.Rows[0]["HH"].ToString();
                    ddl_Minute.Text = dt_Select_Order_Details.Rows[0]["MM"].ToString();
                    ddl_Sec.Text = dt_Select_Order_Details.Rows[0]["SS"].ToString();
                    //txt_Order_Prior_Date.Text = dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString();
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

                    if (dt_Select_Order_Details.Rows[0]["Order_Status"].ToString() == "15" || dt_Select_Order_Details.Rows[0]["Order_Status"].ToString() == "16")
                    {
                        ddl_ordertask.SelectedValue = "3";
                        
                    }
                    else
                    {
                        ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                      
                    }
                    Session_Order_Task = int.Parse(dt_Select_Order_Details.Rows[0]["Order_Status_Id"].ToString());
                    txt_Borrowername.Text = dt_Select_Order_Details.Rows[0]["Borrower_Name"].ToString();

                    txt_Notes.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();
                   
                    ddl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();

                    if (dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() != "" && dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() != null)
                    {

                        ddl_Copy_Type.Text = dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString();

                        if (dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() == "Full")
                        {

                            ddl_Copy_Type.SelectedIndex = 1;
                        }
                        else if (dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() == "Pertinent")
                        {

                            ddl_Copy_Type.SelectedIndex = 2;
                        }

                    }
                    else
                    {

                        ddl_Copy_Type.SelectedIndex = 0;
                    }
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
                clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                dbc.Bind_Client_Email(ddl_Client_Email, clientid);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
               // ddl_SubProcess.Focus();

            }
        }

        private void ddl_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_County.SelectedIndex != 0)
            {
                int county_id = int.Parse(ddl_County.SelectedValue.ToString());
               
                Hashtable htcounty = new Hashtable();
                DataTable dtcounty = new DataTable();
                htcounty.Add("@Trans", "GET_COUNTY_TYPE");
                htcounty.Add("@County", county_id);
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

                    ddl_County_Type.SelectedValue = 1;
                }
                else if (Assign_County_Type == "TIER 2")
                
                {

                    ddl_County_Type.SelectedValue = 2;
                }


                ddl_County.Focus();

            }


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

            //int length = Orderchk.Length;

            //if (length > 3)
            //{
            //    string Substring = Orderchk.Substring(0, 4);

            //    if (Substring.ToUpper() == "FCPF")
            //    {

            //        ddl_Copy_Type.SelectedIndex = 1;

            //    }
            //    else
            //    {

            //        ddl_Copy_Type.SelectedIndex = 2;
            //    }

            //}
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
            if (ddl_Abstractor_Search_Type.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Abstractor Search Type");

                if (Order_Id == 0)
                {

                    btn_Save.Text = "Add New Order";
                }
                else
                {
                    btn_Save.Text = "Submit";

                }
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select Order Type')</script>", false);
                ddl_Abstractor_Search_Type.Focus();
                //   ddl_ordertype.BorderColor = System.Drawing.Color.Red;
                return false;

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
            if (ddl_County_Type.SelectedIndex <= 0)
            {

                MessageBox.Show("Select County Type");
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select County')</script>", false);
                ddl_County_Type.Focus();
                // ddl_County.BorderColor = System.Drawing.Color.Red;
                return false;
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
            btn_text = btn_Save.Text;
            if (ddl_Client_Email.SelectedIndex <= 0)
            {


                dialogResult = MessageBox.Show("You are not Selected Client Email,Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);


                if (dialogResult == DialogResult.Yes)
                {

                    return true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (btn_text == "Submit")
                    {
                        btn_Save.Text = "Submit";


                    }
                    return false;
                }
            }




            return true;

        }

        private bool Validation_ControlEnable()
        {


            if (ddl_ClientName.SelectedIndex <= 0)
            {
                
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
                
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select County')</script>", false);
                ddl_County.Focus();
                // ddl_County.BorderColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {

                // ddl_County.BorderColor = System.Drawing.Color.DarkBlue;
            }
            if (ddl_County_Type.SelectedIndex <= 0)
            {

              
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select County')</script>", false);
                ddl_County_Type.Focus();
                // ddl_County.BorderColor = System.Drawing.Color.Red;
                return false;
            }

            if (txt_Borrowername.Text == "")
            {
              
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

            if (ddl_ordertask.SelectedItem != null)
            {

                htuser.Clear();
                dtuser.Clear();
               
                htuser.Add("@Trans", "SELECT_STATUSID");
                htuser.Add("@Order_Status", ddl_ordertask.SelectedItem);
                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);


                if (btn_Save.Text == "Add New Order" && Validate_OrderNo() != false && Validation() != false && validate_Update_Search()!=false)
                {
                    
                    Get_Maximum_OrderNumber();


                    string Order_Trim = txt_OrderNumber.Text.ToString();

                    string Order_Trim1 = Order_Trim.TrimStart();
                    Order_Trim1 = Order_Trim.TrimEnd();
                    string ordernumber = Order_Trim1.Trim().ToString();



                    int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                    clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());

                    int subprocessid = int.Parse(ddl_SubProcess.SelectedValue.ToString());

                    string address = txt_Address.Text.ToUpper().ToString();

                    int state = int.Parse(ddl_State.SelectedValue.ToString());
                    int county = int.Parse(ddl_County.SelectedValue.ToString());

                    string Barrowername = txt_Borrowername.Text.ToUpper().ToString();
                    //    string Search_type = ddl_Search_Type.SelectedItem.ToString();
                    string Client_Order_Ref = txt_Client_order_ref.Text;
                    string Notes = txt_Notes.Text.ToUpper().ToString();
                    string AbstractorNotes = txt_Abstractor_Notes.Text.ToUpper().ToString();
                    string City = txt_City.Text.ToString();

                    int County_Type = int.Parse(ddl_County_Type.SelectedValue.ToString());
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

                    if (ordertype != 70)
                    {

                        if (County_Type == 2)
                        {
                            htorder.Add("@Order_Status", 17);
                        }
                        else if (County_Type != 2)
                        {

                            htorder.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                        }
                    }
                    else if(ordertype==70)
                    {

                        htorder.Add("@Order_Status",21);



                    }
                
                    htorder.Add("@Client_Order_Ref", Client_Order_Ref);
                    // htorder.Add("@Search_Type", Search_type);
                    htorder.Add("@Order_Prior_Date", txt_Order_Prior_Date.Text);
                    htorder.Add("@Order_Progress", 8);
                    htorder.Add("@Address", address);
                    htorder.Add("@State", state);
                    htorder.Add("@County", county);
                    htorder.Add("@APN", txt_APN.Text);
                    htorder.Add("@City", City.ToString());
                    htorder.Add("@Zip", zipcode.ToString());
                    htorder.Add("@Borrower_Name", Barrowername);
                    htorder.Add("@Notes", Notes);
                    if (rdo_Deedchain.Checked == true)
                    {
                        htorder.Add("@Deed_Chain", "True");
                    }
                    else
                    {
                        htorder.Add("@Deed_Chain", "False");
                    }
                    htorder.Add("@Order_Assign_Type",County_Type);
                    htorder.Add("@Abstractor_Note", AbstractorNotes);
                    htorder.Add("@Total_Cost", Totalcost);
                    htorder.Add("@Paid", "");
                    htorder.Add("@Id", 0);
                    htorder.Add("@Opened", date);
                    htorder.Add("@Action_Date", date);
                    htorder.Add("@Turn_Time", "");
                    htorder.Add("@Inserted_By", userid);
                    htorder.Add("@Inserted_date", date);
                    htorder.Add("@Recived_Date", txt_Date.Text);
                    htorder.Add("@Client_Email_Id",ddl_Client_Email.SelectedValue.ToString());
                    htorder.Add("@Copy_Type",ddl_Copy_Type.SelectedItem.ToString());
                    htorder.Add("@Abstractor_Search_Type",ddl_Abstractor_Search_Type.SelectedValue.ToString());
                    htorder.Add("@status", true);

                   // dtorder = dataaccess.ExecuteSP("Sp_Order", htorder);

                    //dtorder = dataaccess.ExecuteSP("Sp_Order", htorder);
                     Entered_OrderId = dataaccess.ExecuteSPForScalar("Sp_Order", htorder);
                     Order_History();
                     Vendor_Order_Allocate();

                     if (ordertype == 70)
                     {

                         Insert_Tax_Order_Status();
      
                     }
                   
                    //OPERATE_SEARCH_COST = "INSERT";
                    //Insert_Order_Search_Cost(sender, e);

                    Hashtable htget_order = new Hashtable();
                    DataTable dtget_order = new DataTable();
                    htget_order.Add("@Trans", "GET_ORDER_ID");
                    htget_order.Add("@Client_Order_Number", ordernumber.ToString());
                    dtget_order = dataaccess.ExecuteSP("Sp_Order", htget_order);

                    if (dtget_order.Rows.Count > 0)
                    {
                        Order_Id = int.Parse(dtget_order.Rows[0]["Order_ID"].ToString());



                    }


                    Hashtable htdoc = new Hashtable();
                    DataTable dtdoc = new DataTable();
                    htdoc.Add("@Trans", "SELECT");
                    htdoc.Add("@Order_Number", ordernumber.ToString());
                    dtdoc = dataaccess.ExecuteSP("Sp__Temp_Document_Upload", htdoc);

                    if (dtdoc.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtdoc.Rows.Count;i++ )
                        {
                            Hashtable htorderkb = new Hashtable();
                            DataTable dtorderkb = new System.Data.DataTable();

                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", dtdoc.Rows[i]["Instuction"].ToString());
                            htorderkb.Add("@Order_ID", Order_Id);
                            htorderkb.Add("@Document_Name", dtdoc.Rows[i]["Document_Name"].ToString());
                            //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                            // htorderkb.Add("@Extension", extension);
                            htorderkb.Add("@Document_Path", dtdoc.Rows[i]["Document_Path"].ToString());
                            htorderkb.Add("@Inserted_By", userid);
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);


                        }

                        Hashtable htdel = new Hashtable();
                        DataTable dtdel = new DataTable();
                        htdel.Add("@Trans", "DELETE_RECORD");
                        htdel.Add("@Order_Number", ordernumber.ToString());
                        dtdel = dataaccess.ExecuteSP("Sp__Temp_Document_Upload", htdel);
                    }



                    OPERATE_SEARCH_COST = "INSERT";
                    Insert_Order_Search_Cost(sender, e);


                    Get_Maximum_OrderNumber();

                    Order_Uploads ops = new Order_Uploads("Insert",Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
                   
                    ops.Hide();

                    //Order_Uploads.ActiveForm.Disposed += new EventHandler(close_Upload_Form);
                    
                 
                
                    MessageBox.Show("New Order Added Sucessfully");
                    Clear();
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('New Order Added Sucessfully')</script>", false);
                    //   GridviewbindOrderdata();
                    Get_Maximum_OrderNumber();

                    Control_Enable();
                  
                }
                else if (btn_Save.Text == "Submit"  && Validation() != false && validate_Update_Search() != false)
                {

                  //  string ordernumber = txt_OrderNumber.Text.Trim().ToString();
                   
                    string ordernumber = txt_OrderNumber.Text.ToString();

                    int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                    clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                    //        string Search_type = ddl_Search_Type.SelectedItem.ToString();
                    string Client_Order_Ref = txt_Client_order_ref.Text;
                    int subprocessid = int.Parse(ddl_SubProcess.SelectedValue.ToString());


                    string address = txt_Address.Text.ToUpper().ToString();


                    int state = int.Parse(ddl_State.SelectedValue.ToString());
                    int county = int.Parse(ddl_County.SelectedValue.ToString());

                    string Barrowername = txt_Borrowername.Text.ToUpper().ToString();

                    string AbstractorNotes = txt_Abstractor_Notes.Text.ToUpper().ToString();
                    int County_Type = int.Parse(ddl_County_Type.SelectedValue.ToString());
                    string Notes = txt_Notes.Text.ToUpper().ToString();

                    string City = txt_City.Text.ToString();
                    if (txt_Zip.Text != "")
                    {
                        zipcode = Convert.ToDouble(txt_Zip.Text);
                    }

                    string minute, sec,Hour;
                    if (ddl_Hour.Text == "")
                    {
                        Hour = "0";
                    }
                    else
                    {
                        Hour = ddl_Hour.Text;

                    }
                    if (ddl_Minute.Text == "")
                    {
                        minute = "0";

                    }
                    else
                    {

                        minute = ddl_Minute.Text;
                    }
                    if (ddl_Sec.Text == "")
                    {

                        sec = "0";
                    }
                    else
                    {

                        sec = "0";
                    }

                    string Recived_Time = Hour + ":" + minute + ":" + sec;

                    int orderid = Order_Id;

                    Hashtable htorder = new Hashtable();
                    DataTable dtorder = new DataTable();


                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    Order_History();
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
                    htorder.Add("@Order_Prior_Date", txt_Order_Prior_Date.Text);
                    htorder.Add("@County", county);
                    htorder.Add("@Borrower_Name", Barrowername);
                    if (rdo_Deedchain.Checked == true)
                    {
                        htorder.Add("@Deed_Chain", "True");
                    }
                    else
                    {
                        htorder.Add("@Deed_Chain", "False");
                    }
                    htorder.Add("@Notes", Notes);
                    htorder.Add("@Order_Assign_Type", County_Type);
                    htorder.Add("@Abstractor_Note", AbstractorNotes);
                    htorder.Add("@Copy_Type", ddl_Copy_Type.SelectedItem.ToString());
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
                    htorder.Add("@Abstractor_Search_Type", ddl_Abstractor_Search_Type.SelectedValue.ToString());
                    htorder.Add("@Client_Email_Id", ddl_Client_Email.SelectedValue.ToString());
                    htorder.Add("@status", true);
                    dtorder = dataaccess.ExecuteSP("Sp_Order", htorder);

                    //Hashtable htorderStatus = new Hashtable();
                    //DataTable dtorderStatus = new DataTable();
                    //htorderStatus.Add("@Trans", "UPDATE_STATUS");
                    //htorderStatus.Add("@Order_ID", orderid);
                    //htorderStatus.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                    //htorderStatus.Add("@Modified_By", userid);
                    //htorderStatus.Add("@Modified_Date", date);
                    //dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                    //Hashtable htorderProgress = new Hashtable();
                    //DataTable dtorderProgress = new DataTable();
                    //htorderProgress.Add("@Trans", "UPDATE_PROGRESS");
                    //htorderProgress.Add("@Order_ID", orderid);
                    //htorderProgress.Add("@Order_Progress", 8);
                    //htorderProgress.Add("@Modified_By", userid);
                    //htorderProgress.Add("@Modified_Date", date);
                    //dtorderProgress = dataaccess.ExecuteSP("Sp_Order", htorderProgress);


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

                    
                    MessageBox.Show("Order Updated Sucessfully");
                    Clear();
                    this.Close();
                    
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Updated Sucessfully')</script>", false);
                    btn_Save.Text = "Add New Order";
                    ddl_ordertask.Enabled = true;
                    //  GridviewbindOrderdata();
                }

                if (btn_Save.Text == "Edit Order")
                {
                  
                    btn_Save.Text = "Submit";
                    Control_Enable();
                    ddl_ordertask.Enabled = false;
                }
                else
                {
                    
                    if (btn_Save.Text == "Submit" && Validation_ControlEnable() != false && validate_Update_Search_Control_Enable() != false)
                    {
                        if (dialogResult == DialogResult.No && btn_text == "Submit" && dialogResult != DialogResult.None)
                        {
                           
                            btn_Save.Text = "Submit";

                        }
                        else
                        {
                            btn_Save.Text = "Add New Order";
                            Control_Enable();
                        }
                    }
                    else if (btn_Save.Text == "Add New Order" && Validation_ControlEnable() != false && validate_Update_Search_Control_Enable() != false)
                    {
                        if (dialogResult != DialogResult.No && dialogResult!=DialogResult.None)
                        {
                            btn_Save.Text = "Submit";
                            Control_Enable();
                        }


                    }
                }
                //  Clear();
                Get_Maximum_OrderNumber();
                
                
            }
            else
            {
                if (btn_Save.Text == "Edit Order")
                {
                    btn_Save.Text = "Submit";
                    Control_Enable();
                }
                else
                {
                    btn_Save.Text = "Add New Order";
                    Control_Enable();
                }
                MessageBox.Show("Select Order Task");
              
            }
            // Clear();
            //AddParent();
         
        }



        private void Insert_Tax_Order_Status()
        {

            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", Entered_OrderId);
            httax.Add("@Order_Task",21);
            httax.Add("@Order_Status",8);
            httax.Add("@Tax_Task",1);
            httax.Add("@Tax_Status",6);
            httax.Add("@Inserted_By",userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);



        }
       

        private void close_Upload_Form()
        {

            Order_Uploads.ActiveForm.Dispose();
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

        public bool validate_Update_Search_Control_Enable()
        {
            int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());

            if (ordertype == 7 && txt_Order_Prior_Date.Text == " ")
            {
                lbl_Order_Prior_Date.Visible = true;
                txt_Order_Prior_Date.Visible = true;
                lbl_order_Prior_mark.Visible = true;
              
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
            if (txt_Order_Cost.Text != "") { Order_Cost = Convert.ToDecimal(txt_Order_Cost.Text.ToString()); } else { Order_Cost = 0; }
            if (txt_Abstractor_Cost.Text != "") { Abstractor_Cost = Convert.ToDecimal(txt_Abstractor_Cost.Text.ToString()); } else { Abstractor_Cost = 0; }
            if (txt_noofpage.Text != "") { No_Of_Pages = Convert.ToInt32(txt_noofpage.Text.ToString()); } else { No_Of_Pages = 0; }
            Hashtable ht_Orderid = new Hashtable();
            DataTable dt_Orderid = new System.Data.DataTable();
            ht_Orderid.Add("@Trans", "SELECT_ORDER_ID");
            ht_Orderid.Add("@Client_Order_Number", txt_OrderNumber.Text.Trim().ToString());
            dt_Orderid = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Orderid);
            Order_Id = int.Parse(dt_Orderid.Rows[0]["Order_ID"].ToString());
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new System.Data.DataTable();
            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            string time = date.ToString("hh:mm tt");
            if (OPERATE_SEARCH_COST == "INSERT")
            {
                htsearch.Add("@Trans", "INSERT");
                htsearch.Add("@Order_Id", Order_Id);
                if (ddl_Order_Source.SelectedItem != null)
                {
                    htsearch.Add("@Source", ddl_Order_Source.SelectedItem.ToString());
                }

                htsearch.Add("@Search_Cost", SearchCost);
                htsearch.Add("@Copy_Cost", Copy_Cost);
                htsearch.Add("@Order_Cost", Order_Cost);
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
                htsearch.Add("@Order_Cost", Order_Cost);
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
            txt_Abstractor_Notes.Text = "";
            ddl_State.SelectedIndex = 0;
            //ddl_County.SelectedIndex = ;
            txt_City.Text = "";
            txt_Zip.Text="";
            Order_Id = 0;
            ddl_ordertask.SelectedIndex = 0;
           // ddl_Search_Type.SelectedIndex = 0;
            ddl_Order_Source.SelectedIndex = 0;
            txt_Search_cost.Text = "";
            txt_Copy_cost.Text = "";
            txt_Abstractor_Cost.Text="";
            txt_noofpage.Text = "";
            txt_Notes.Text = "";
            lbl_County_Type.Text = "";
            txt_Order_Cost.Text = "";
            ddl_Client_Email.SelectedIndex = 0;
        }
        private void Control_Enable()
        {
            ddl_ClientName.Enabled = true;
            ddl_SubProcess.Enabled = true;
            ddl_Hour.Enabled = true;
            ddl_Minute.Enabled = true;
            ddl_Sec.Enabled = true;
            txt_Date.Enabled = true;
            txt_Order_Cost.Enabled = true;
            ddl_County_Type.Enabled = true;
            txt_Order_Prior_Date.Enabled = true;
           
            ddl_ordertype.Enabled = true;
            ddl_Abstractor_Search_Type.Enabled = true;
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
            txt_Order_Prior_Date.Enabled = true;
            ddl_Copy_Type.Enabled = true;
            txt_Abstractor_Notes.Enabled = true;

            txt_Search_cost.ReadOnly = false;
            txt_Copy_cost.ReadOnly = false;
            txt_Order_Cost.ReadOnly = false;
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
            txt_Abstractor_Notes.ReadOnly = false;
            rdo_Deedchain.Enabled = true;
            DateCustom = 1;
        }
        private void Control_Enable_false()
        {
            ddl_ClientName.Enabled = false;
            ddl_SubProcess.Enabled = false;
            ddl_County_Type.Enabled = false;
            ddl_Hour.Enabled = false;
            ddl_Minute.Enabled = false;
            ddl_Sec.Enabled = false;
            txt_Date.Enabled = false;
            ddl_ordertype.Enabled = false;
            txt_OrderNumber.ReadOnly = true;
            txt_APN.ReadOnly = true;
            txt_Order_Prior_Date.Enabled = false;
            ddl_Copy_Type.Enabled = false;
            txt_Client_order_ref.ReadOnly = true;
            txt_Borrowername.ReadOnly = true;
            txt_Address.ReadOnly = true;
            ddl_State.Enabled = false;
            ddl_County.Enabled = false;
            txt_City.ReadOnly = true;
            txt_Zip.ReadOnly = true;
            ddl_ordertask.Enabled = false;
            txt_Order_Cost.Enabled = false;
            txt_Abstractor_Notes.Enabled = false;
            
         //  ddl_Search_Type.Enabled = false;
           
            ddl_Order_Source.Enabled = false;
            ddl_Abstractor_Search_Type.Enabled = false;
            txt_Search_cost.ReadOnly = true;
            txt_Copy_cost.ReadOnly = true;
            txt_Abstractor_Cost.ReadOnly = true;
            txt_noofpage.ReadOnly = true;
            txt_Notes.ReadOnly = true;
            txt_Order_Cost.ReadOnly = true;
            txt_Abstractor_Notes.ReadOnly = true;
            rdo_Deedchain.Enabled = false;
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
            //grid_OrderTask.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.OliveDrab;
            //grid_OrderTask.EnableHeadersVisualStyles = false;
            //grid_OrderTask.Columns[0].Width=90;
            //grid_OrderTask.Columns[1].Width=105;
            //grid_OrderTask.Columns[2].Width=90;
            //grid_OrderTask.Columns[3].Width=110;
            //grid_OrderTask.Columns[4].Width=90;
            if (dtselect.Rows.Count > 0)
            {
                //grid_OrderTask.Rows.Clear();
                // for (int i = 0; i < dtselect.Rows.Count; i++)
                // {
                    // grid_OrderTask.Rows.Add();
                     lbl_search.Text = dtselect.Rows[0]["search"].ToString();
                     lbl_SearchQC.Text = dtselect.Rows[0]["Search Qc"].ToString();
                     lbl_Typing.Text = dtselect.Rows[0]["Typing"].ToString();
                     lbl_TypingQC.Text = dtselect.Rows[0]["Typing Qc"].ToString();
                     lbl_Upload.Text = dtselect.Rows[0]["Upload"].ToString();
                // }
            }
            else
            {
                //grid_OrderTask.Visible = true;
                //grid_OrderTask.DataSource = null;
                lbl_search.Text = "";
                lbl_SearchQC.Text = "";
                lbl_Typing.Text = "";
                lbl_TypingQC.Text = "";
                lbl_Upload.Text = "";

            }
        }
        protected void GridviewbindUser_Task_Status_Orderdata()
        {

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();

            htselect.Add("@Trans", "GET_TASK_STATUS");
            htselect.Add("@Order_ID", Order_Id);
            dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
            //grid_OrderProgress.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.OliveDrab;
            //grid_OrderProgress.EnableHeadersVisualStyles = false;
            //grid_OrderProgress.Columns[0].Width = 140;
            //grid_OrderProgress.Columns[1].Width = 105;
            //grid_OrderProgress.Columns[2].Width = 140;
            //grid_OrderProgress.Columns[3].Width = 105;
            //grid_OrderProgress.Columns[4].Width = 90;
            if (dtselect.Rows.Count > 0)
            {
                //grid_OrderProgress.Rows.Clear();
                //for (int i = 0; i < dtselect.Rows.Count; i++)
                //{
                   // grid_OrderProgress.Rows.Add();
                    lbl_STask.Text = dtselect.Rows[0]["search"].ToString();
                    lbl_ScTask.Text = dtselect.Rows[0]["Searh QC"].ToString();
                    lbl_Ttask.Text = dtselect.Rows[0]["Typing"].ToString();
                    lbl_TcTask.Text = dtselect.Rows[0]["Typing Qc"].ToString();
                    lbl_UTask.Text = dtselect.Rows[0]["Upload"].ToString();

             //   }
             

            }
            else
            {
               // grid_OrderProgress.Visible = true;
           //     grid_OrderProgress.DataSource = null;
                lbl_STask.Text = "";
                lbl_ScTask.Text = "";
                lbl_Ttask.Text = "";
                lbl_TcTask.Text = "";
                lbl_UTask.Text = "";

            }
        }
        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT");
            htComments.Add("@Order_Id",Order_Id);
            htComments.Add("@Work_Type",1);

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
        //protected void grd_Error_Bind()
        //{
        //    Hashtable htNotes = new Hashtable();
        //    DataTable dtNotes = new System.Data.DataTable();
        //    // int Order_id = int.Parse(Session["order_id"].ToString());
        //    htNotes.Add("@Trans", "BIND");
        //    htNotes.Add("@User_id", userid);
        //    dtNotes = dataaccess.ExecuteSP("Sp_Error_Info", htNotes);
        //    grd_Error.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Brown;
        //    grd_Error.EnableHeadersVisualStyles = false;
        //    grd_Error.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
        //    grd_Error.Columns[0].Width = 150;
        //    grd_Error.Columns[1].Width = 250;
        //    grd_Error.Columns[2].Width = 120;
        //    grd_Error.Columns[3].Width = 120;
        //    grd_Error.Columns[4].Width = 95;
        //    if (dtNotes.Rows.Count > 0)
        //    {
        //        grd_Error.Rows.Clear();

        //        for (int i = 0; i < dtNotes.Rows.Count; i++)
        //        {
        //            grd_Error.Rows.Add();

        //            grd_Error.Rows[i].Cells[0].Value = dtNotes.Rows[i]["Error_Type"].ToString();
        //            grd_Error.Rows[i].Cells[1].Value = dtNotes.Rows[i]["Error_Description"].ToString();
        //            grd_Error.Rows[i].Cells[2].Value = dtNotes.Rows[i]["Comments"].ToString();
        //            grd_Error.Rows[i].Cells[3].Value = dtNotes.Rows[i]["Order_Status"].ToString();
        //            grd_Error.Rows[i].Cells[4].Value = dtNotes.Rows[i]["User_name"].ToString();
        //        }
        //    }
        //    else
        //    {
        //    }


        //}

    

        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {
          
        }



      //  private void AddParent()
      //  {

      //      string sKeyTemp = "";
           
      //      Hashtable ht = new Hashtable();
      //      DataTable dt = new System.Data.DataTable();

      //      TreeNode parentnode;
      //      ht.Add("@Trans", "SELECT");

      //      dt = dataaccess.ExecuteSP("Sp_Client", ht);
      //      //for (int i = 0; i < dt.Rows.Count; i++)
      //      //  {
      //      sKeyTemp = "Orders";
      //      for (int i = 0; i < dt.Rows.Count; i++)
      //      {
      //          sKeyTemp = dt.Rows[i]["Client_Name"].ToString();

      //        parentnode=  treeView1.Nodes.Add(sKeyTemp, sKeyTemp);

      //        AddChilds(parentnode, sKeyTemp);
      //       }
      //  }
      //  private void AddChilds(TreeNode parentnode, string sKey)
      //  {
      //      Hashtable ht = new Hashtable();
      //      DataTable dt = new System.Data.DataTable();
      //      TreeNode childnode;
      //      string sKeyTemp1 = "";

      //      ht.Add("@Trans", "Subprocess_Name");
      //      ht.Add("@Client_Name", sKey);
      //      dt = dataaccess.ExecuteSP("Sp_Tree_Orders", ht);
      //      for (int i = 0; i < dt.Rows.Count; i++)
      //      {
      //          sKeyTemp1 = dt.Rows[i]["Sub_ProcessName"].ToString();
      //         childnode= parentnode.Nodes.Add(sKeyTemp1, sKeyTemp1);
      //         AddChilds1(childnode,sKeyTemp1);
      //      }
      //  }
      //  private void AddChilds1(TreeNode childnode, string sKey1)
      //{
      //    Hashtable ht1 = new Hashtable();
      //    DataTable dt1 = new System.Data.DataTable();
      //    TreeNode childnode1;
      //    string sKeyTemp2 = "";
      //    ht1.Add("@Trans", "Order_Month");
      //    ht1.Add("@Sub_ProcessName", sKey1);
      //    dt1 = dataaccess.ExecuteSP("Sp_Tree_Orders", ht1);
      //    for (int i = 0; i < dt1.Rows.Count; i++)
      //    {

      //        sKeyTemp2 = dt1.Rows[i]["Date"].ToString();
      //        childnode1 = childnode.Nodes.Add(sKeyTemp2, sKeyTemp2);
      //        AddChilds2(childnode1, sKeyTemp2, sKey1);
      //    }
      //}
      //  private void AddChilds2(TreeNode childnode1, string sKey2,string Subprocess)
      //  {
      //      Hashtable ht2 = new Hashtable();
      //      DataTable dt2 = new System.Data.DataTable();
      //      TreeNode childnode2;
      //      string sKeyTemp3 = sKey2;
      //      ht2.Add("@Trans", "Order_Date");
      //      ht2.Add("@Sub_ProcessName", Subprocess);
      //      ht2.Add("@Month", sKeyTemp3);
      //      dt2 = dataaccess.ExecuteSP("Sp_Tree_Orders", ht2);
      //      for (int i = 0; i < dt2.Rows.Count; i++)
      //      {
      //          sKeyTemp3 = dt2.Rows[i]["Date"].ToString();
      //          childnode2 = childnode1.Nodes.Add(sKeyTemp3, sKeyTemp3);
      //          AddChilds3(childnode2, sKeyTemp3, Subprocess);
      //      }
      //  }
      //  private void AddChilds3(TreeNode childnode2, string sKey3, string Subprocess)
      //  {
      //      Hashtable ht2 = new Hashtable();
      //      DataTable dt2 = new System.Data.DataTable();
      //      TreeNode childnode3;
      //      string sKeyTemp4 = sKey3;
      //      string Order_Id_tree;
      //      ht2.Add("@Trans", "Order_Id");
      //      ht2.Add("@Sub_ProcessName", Subprocess);
      //      ht2.Add("@Date", sKeyTemp4);
      //      dt2 = dataaccess.ExecuteSP("Sp_Tree_Orders", ht2);
      //      for (int i = 0; i < dt2.Rows.Count; i++)
      //      {

      //          sKeyTemp4 = dt2.Rows[i]["Client_Order_Number"].ToString();
      //          Order_Id_tree = dt2.Rows[i]["Order_ID"].ToString();
      //          childnode3 = childnode2.Nodes.Add(Order_Id_tree, sKeyTemp4);
      //         // AddChilds3(childnode2, sKeyTemp3);
               
      //      }
      //  }

        //private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    Clear();
        //   // if (treeView1.SelectedNode.Name.ToString() !=)
        //    bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out Order_Id);
        //    if (isNum)
        //    {
        //        Order_Load();
        //        Get_Order_Search_Cost_Details();
        //        GridviewbindUser_Task_Orderdata();
        //        GridviewbindUser_Task_Status_Orderdata();
        //        Geydview_Bind_Comments();
        //       // Order_Controls_Load();
        //    }
           
        //}

        private void ddl_County_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ddl_State_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            if (Order_Id == 0)
            {
                int Orderno = 0;
                Hashtable ht_Maxid = new Hashtable();
                DataTable dt_Maxid = new DataTable();
                ht_Maxid.Add("@Trans", "MAX_ORDER_NO");
                dt_Maxid = dataaccess.ExecuteSP("Sp_Order", ht_Maxid);
                if (dt_Maxid.Rows.Count > 0)
                {
                    Orderno = int.Parse(dt_Maxid.Rows[0]["ORDER_NUMBER"].ToString());

                    Order_Uploads Orderuploads = new Order_Uploads("Insert",Orderno, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
                    Orderuploads.Show();
                }
            }
            else
            {

                Order_Uploads Orderuploads = new Order_Uploads("Update",Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
                Orderuploads.Show();
            }
        }

        private void txt_Order_Prior_Date_ValueChanged(object sender, EventArgs e)
        {
            if (DateCustom != 0)
            {
                txt_Order_Prior_Date.CustomFormat = "MM/dd/yyyy";
            }
            DateCustom = 1;
        }

        private void Order_Entry_FormClosing(object sender, FormClosingEventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
            Orderuploads.Close();
            Orderuploads.Hide();

            Ordermanagement_01.Employee_Error_Entry Error = new Ordermanagement_01.Employee_Error_Entry(userid, "", Order_Id, 3,1);
            Error.Close();
            Error.Hide();
            
        }

        private void ddl_ordertype_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_ordertype.SelectedIndex > 0)
            {

                ddl_Abstractor_Search_Type.SelectedValue = int.Parse(ddl_ordertype.SelectedValue.ToString());

                int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                if (ordertype==7)
                {

                    lbl_Order_Prior_Date.Visible = true;
                    txt_Order_Prior_Date.Visible = true;
                    lbl_order_Prior_mark.Visible = true;
                }
                else

                {
                    lbl_Order_Prior_Date.Visible = false;
                    txt_Order_Prior_Date.Visible = false;
                    lbl_order_Prior_mark.Visible = false;

                }
            }



        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Typing_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void btn_Order_Error_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_Error_Entry EmployeeError = new Ordermanagement_01.Employee_Error_Entry(userid, "", Order_Id, 3,1);
            EmployeeError.Show();
            
        }

        private void Order_Entry_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
            //Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
            //Orderuploads.Close();
            //Orderuploads.Hide();
            //this.Parent.Hide();


        }

        private void btn_Preview_Check_List_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Order_Check_List_View check_List_View = new Ordermanagement_01.Order_Check_List_View(userid, Order_Id, 0, "AdminWise");
            check_List_View.Show();
        }



        private void Order_History()
        {
            Hashtable ht_Order_History = new Hashtable();
            DataTable dt_Order_History = new DataTable();
            ht_Order_History.Add("@Trans", "INSERT");
            if (btn_Save.Text == "Submit")
            {
                ht_Order_History.Add("@Order_Id", Order_Id);
            }
            else
            {
                ht_Order_History.Add("@Order_Id", Entered_OrderId);
            }
           // ht_Order_History.Add("@User_Id", );
            ht_Order_History.Add("@Status_Id", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
            ht_Order_History.Add("@Progress_Id",8);
            ht_Order_History.Add("@Assigned_By",userid);
            ht_Order_History.Add("@Work_Type", 1);
            if(btn_Save.Text == "Submit")
            {
                 ht_Order_History.Add("@Modification_Type","Order Update");
            }
            else
            {
            ht_Order_History.Add("@Modification_Type","Order Create");
            }
            dt_Order_History=dataaccess.ExecuteSP("Sp_Order_History",ht_Order_History);

        }

        private void btn_OrderHistory_Click(object sender, EventArgs e)
        {
            
            Ordermanagement_01.Order_History_List Order_History = new Ordermanagement_01.Order_History_List(userid,Order_Id, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text, ddl_State.Text, ddl_County.Text);
            Order_History.Show();
        }

        private void txt_OrderNumber_TextChanged(object sender, EventArgs e)
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
                //txt_OrderNumber.Text = "";


            }
            else if (int.Parse(dtorder.Rows[0]["count"].ToString()) <= 0)
            {
                User_Chk_Img.Image = Image.FromFile(Environment.CurrentDirectory + @"\Sucess.png");

               
            }

        }

        private void ddl_SubProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_SubProcess.SelectedIndex > 0)
            { 
            
                Subprocess_id=int.Parse(ddl_SubProcess.SelectedValue.ToString());

                
            }
            if (Subprocess_id == 6)
            {

                ddl_Copy_Type.SelectedIndex = 1;
            }
            else
            {
                ddl_Copy_Type.SelectedIndex = 2;

            }

        }

        private void Btn_Marker_Maker_Click(object sender, EventArgs e)
        {
         
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "PACKAGE_VALIDATE");
            ht.Add("@Order_Id", Order_Id);
            dt = dataaccess.ExecuteSP("Sp_Document_Upload", ht);
            if (dt.Rows.Count > 0)
            {
                Ordermanagement_01.MarkerMaker.Image_Marker_Maker Markermaker = new Ordermanagement_01.MarkerMaker.Image_Marker_Maker(Order_Id, Session_Order_Task, txt_OrderNumber.Text, ddl_ordertask.Text.ToString(), Client_no, "", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                Markermaker.Show();
            }
            else
            {
                MessageBox.Show("Please select search Package in uploaddocuments");
            }
        }

        private void btn_Judgement_Click(object sender, EventArgs e)
        {

            if (ddl_State.SelectedIndex > 0)
            {

                string Stat_Id = ddl_State.SelectedValue.ToString();

                Ordermanagement_01.Masters.Judgement_Period_Create_View js = new Ordermanagement_01.Masters.Judgement_Period_Create_View(userid,Stat_Id);
                js.Show();
            }


        }

      
        //===================================================Its an Vendor Area=================================================

        private void Vendor_Order_Allocate()
        {
           

            int State_Id = int.Parse(ddl_State.SelectedValue.ToString());

            int County_Id = int.Parse(ddl_County.SelectedValue.ToString());
            Hashtable htvendorname = new Hashtable();
            DataTable dtvendorname = new DataTable();
            htvendorname.Add("@Trans", "GET_VENDORS_STATE_COUNTY_WISE");

            htvendorname.Add("@State_Id", State_Id);
            htvendorname.Add("@County_Id", County_Id);
            dtvendorname = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendorname);
            if (dtvendorname.Rows.Count > 0)
            {


                for (int i = 0; i < dtvendorname.Rows.Count; i++)
                {

                    Vendor_Id = int.Parse(dtvendorname.Rows[i]["Vendor_Id"].ToString());
                 

                    Hashtable htvenncapacity = new Hashtable();
                    System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                    htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                    htvenncapacity.Add("@Venodor_Id", Vendor_Id);
                    dtvencapacity = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                    if (dtvencapacity.Rows.Count > 0)
                    {

                        Hashtable htetcdate = new Hashtable();
                        System.Data.DataTable dtetcdate = new System.Data.DataTable();
                        htetcdate.Add("@Trans", "GET_DATE");
                        dtetcdate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);

                         Vend_date =dtetcdate.Rows[0]["Date"].ToString();


                        Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());

                        Hashtable htvendor_Percngate = new Hashtable();
                        System.Data.DataTable dtvendor_percentage = new System.Data.DataTable();

                        htvendor_Percngate.Add("@Trans", "GET_VENDOR_PERCENTAGE_OF_ORDERS");
                        htvendor_Percngate.Add("@Venodor_Id", Vendor_Id);
                        dtvendor_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendor_Percngate);

                       

                        if (dtvendor_percentage.Rows.Count > 0)
                        {



                            Vendor_Order_Percentage = Convert.ToDecimal(dtvendor_percentage.Rows[0]["Percentage"].ToString());

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





                            //   if((Vendor_Order_Percentage/Vendor_Total_No_Of_Order_Recived)==)


                            //Formula 

                            //   Vendor%
                            //------------------- * Total_No_Of_Order_recived  <= Vendor_Capacity
                            // 100



                            var Vendor_Allocation_Output = (Vendor_Order_Percentage / 100) * Vendor_Total_No_Of_Order_Recived;




                            decimal Vendor_Order_Allocation_Count = Convert.ToDecimal(Vendor_Allocation_Output.ToString());

                            if (Vendor_Order_Allocation_Count <= Vendor_Order_capacity && No_Of_Order_Assignd_for_Vendor <= Vendor_Order_capacity)
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
                                    htinsert.Add("@Venodor_Id", Vendor_Id);
                                    htinsert.Add("@Assigned_Date_Time", dtetcdate.Rows[0]["Date_time"]);
                                    htinsert.Add("@Assigned_By", userid);
                                    htinsert.Add("@Inserted_By", userid);
                                    htinsert.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                    htinsert.Add("@Status", "True");
                                    dtinert = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htinsert);




                                    Hashtable htinsertstatus = new Hashtable();
                                    System.Data.DataTable dtinsertstatus = new System.Data.DataTable();
                                    htinsertstatus.Add("@Trans", "INSERT");
                                    htinsertstatus.Add("@Vendor_Id", Vendor_Id);
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


                                    //==================================External Client_Vendor_Orders=====================================================


                                    Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                    System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                    htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                    htCheck_Order_InTitlelogy.Add("@Order_ID", Entered_OrderId);
                                    dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                    if (dt_Order_InTitleLogy.Rows.Count > 0)
                                    {

                                        External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                        External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                        if (External_Client_Order_Task_Id != 18)
                                        {
                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 2);
                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                        }




                                    }


                                }



                            }






                            //OrderHistory
                          
                        }
                        else
                        {


                        }





               

                    }
                }

                MessageBox.Show("Order Assigned Sucessfully");
            }


            }

        private void grp_OrderEntry_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {

                string Stat_Id = ddl_State.SelectedValue.ToString();

                Ordermanagement_01.Employee.State_Wise_Tax_Due_Date tax = new Ordermanagement_01.Employee.State_Wise_Tax_Due_Date(userid, Stat_Id);
                tax.Show();
            }
        }

                    
        
     

        
        

    }
}
