using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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
        int Order_Id = 0;
        int userid;
        string Empname, Client_no;
        int Count;
        int BRANCH_ID;
        int No_Of_Orders;
        int client_Id, Subprocess_id, Order_Type_Id;
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
        int Check_Order;
        static string Orignal_Order_Number;
        string btn_text;
        int Session_Order_Task;
        string Vend_date, userroleid;

        int Vendor_Total_No_Of_Order_Recived, Vendor_No_Of_Order_For_each_Vendor, Vendor_Order_capacity;
        decimal Vendor_Order_Percentage, Vendor_Balance_Percentage, Total_Vendor_Balance_Percentage, Total_Vendor_Alloacated_Percentage;
        int No_Of_Order_Assignd_for_Vendor, Vendor_Id;

        int External_Client_Order_Task_Id, External_Client_Order_Id;
        string Vendors_State_County, Vendors_Order_Type, Vendors_Client_Sub_Client;
        int Vendor_Order_Allocation_Count, Vendor_Id_For_Assign;

        int Assigning_To_Vendor;
        string Production_Date;
        private bool IsAddressConfirmed;

        public Order_Entry(int Orderid, int User_Id, string User_Roleid, string PRODUCTION_DATE)
        {

            InitializeComponent();
            // Clear();
            //  pnl_visible();
            // Order_Entry.ActiveForm.Width = 1045;


            dbc.BindOrderType(ddl_ordertype);
            userid = User_Id;
            userroleid = User_Roleid;
            dbc.BindState(ddl_State);
            dbc.BindProjectType(ddlCategoryType);
            //clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            //dbc.BindSubProcessName(ddl_SubProcess, clientid);
            dbc.BindAbstractor_Order_Serarh_Type(ddl_Abstractor_Search_Type);
            ddl_ordertask.Visible = true;
            ddl_ordertask.Items.Insert(0, "Search");
            ddl_ordertask.Items.Insert(1, "Search QC");
            ddl_ordertask.Items.Insert(2, "Typing");
            ddl_ordertask.Items.Insert(3, "Typing QC");
            ddl_ordertask.Items.Insert(4, "Upload");
            ddl_ordertask.Items.Insert(5, "Final QC");
            ddl_ordertask.Items.Insert(6, "Exception");
            ddl_ordertask.Items.Insert(7, "Research");
            ddl_ordertask.Items.Insert(8, "Upload Completed");
            ddl_ordertask.Items.Insert(9, "Abstractor");
            ddl_ordertask.Items.Insert(10, "Vendor");
            ddl_ordertask.Items.Insert(11, "Tax Request");
            ddl_ordertask.Items.Insert(12, "Search Tax Request");
            ddl_ordertask.Items.Insert(13, "Tax");
            //dbc.BindOrderStatus(ddl_ordertask);

            //ddl_ordertask.SelectedIndex = 1;
            //   ddl_Search_Type.Visible = true;
            // ddl_Search_Type.Items.Insert(0, "SELECT");
            //ddl_Search_Type.Items.Insert(0, "TIER 1");
            //ddl_Search_Type.Items.Insert(1, "TIER 2");
            //ddl_Search_Type.Items.Insert(2, "TIER 2-In house");
            dbc.Bind_Employee_Order_source(ddl_Order_Source);
            //   ddl_Order_Source.Items.Insert(0, "SELECT");
            //ddl_Order_Source.Items.Insert(0, "Online");
            //ddl_Order_Source.Items.Insert(1, "Subscription");
            //ddl_Order_Source.Items.Insert(2, "Abstractor");
            //ddl_Order_Source.Items.Insert(3, "Online/Abstractor");
            //ddl_Order_Source.Items.Insert(4, "Online/Data Tree");
            //ddl_Order_Source.Items.Insert(5, "Data Trace");
            //ddl_Order_Source.Items.Insert(6, "Title Point");
            //Order_Controls_Load();
            Order_Id = Orderid;
            Production_Date = PRODUCTION_DATE;
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
                txt_No_of_hits.Text = dt_Select_Order_Details.Rows[0]["No_of_Hits"].ToString();
                txt_No_of_documents.Text = dt_Select_Order_Details.Rows[0]["No_of_Documents"].ToString();
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
            this.WindowState = FormWindowState.Maximized;


            if (Order_Id == 0)
            {
                ddl_Copy_Type.SelectedIndex = 2;
                btn_Save.Text = "Add New Order";
                this.Text = "Add New Order";
                ddl_ordertask.Enabled = true;
                ddl_County_Type.Enabled = true;
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

            if (userroleid == "1" || userid == 179 || userid == 260)
            {
                dbc.BindClientName(ddl_ClientName);
                clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());


                // dbc.BindSubProcessName(ddl_SubProcess, clientid);
            }
            else
            {
                dbc.BindClientNo(ddl_ClientName);
                clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());

                // dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
            }



            if (userroleid == "1" || userroleid == "5" || userroleid == "6" || userroleid == "4")
            {
                pnl_butns.Visible = true;
                grp_Order_Task.Visible = true;


            }
            else
            {

                pnl_butns.Visible = false;
                grp_Order_Task.Visible = false;

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
            Size finalqc = TextRenderer.MeasureText(lbl_Final_Qc.Text, lbl_Final_Qc.Font);
            lbl_Final_Qc.Width = finalqc.Width;
            lbl_Final_Qc.Height = finalqc.Height;

            Size exception = TextRenderer.MeasureText(lbl_Exception.Text, lbl_Exception.Font);
            lbl_Exception.Width = exception.Width;
            lbl_Exception.Height = exception.Height;


            Size upload = TextRenderer.MeasureText(lbl_Upload.Text, lbl_Upload.Font);
            lbl_Upload.Width = upload.Width;
            lbl_Upload.Height = upload.Height;



            Size Tax_Internal = TextRenderer.MeasureText(lbl_Tax.Text, lbl_Tax.Font);
            lbl_Tax.Width = Tax_Internal.Width;
            lbl_Tax.Height = Tax_Internal.Height;



            Size Order_Completed = TextRenderer.MeasureText(lbl_Comp.Text, lbl_Comp.Font);
            lbl_Comp.Width = Order_Completed.Width;
            lbl_Comp.Height = Order_Completed.Height;





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
            Size final_Qcqct = TextRenderer.MeasureText(lbl_Final_Qc_Task.Text, lbl_Final_Qc_Task.Font);
            lbl_Final_Qc_Task.Width = final_Qcqct.Width;
            lbl_Final_Qc_Task.Height = final_Qcqct.Height;

            Size Exception_qct = TextRenderer.MeasureText(lbl_Exception_Task.Text, lbl_Exception_Task.Font);
            lbl_Exception_Task.Width = Exception_qct.Width;
            lbl_Exception_Task.Height = Exception_qct.Height;

            Size Tax_Internal_Tax_Task = TextRenderer.MeasureText(lbl_tax_Task.Text, lbl_tax_Task.Font);
            lbl_tax_Task.Width = Tax_Internal_Tax_Task.Width;
            lbl_tax_Task.Height = Tax_Internal_Tax_Task.Height;


            Size Order_Completed_Task = TextRenderer.MeasureText(lbl_Comp_task.Text, lbl_Comp_task.Font);
            lbl_Comp_task.Width = Order_Completed_Task.Width;
            lbl_Comp_task.Height = Order_Completed_Task.Height;


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
                try
                {
                    Control_Enable_false();
                    if (userroleid == "1" || userroleid == "6")
                    {

                    }
                    else
                    {
                        txt_Order_Cost.Enabled = false;
                        txt_Order_Cost.PasswordChar = '*';
                        //txt_Search_cost.PasswordChar = '*';
                        //txt_Copy_cost.PasswordChar='*';
                        //txt_Abstractor_Cost.PasswordChar = '*';
                        //txt_noofpage.PasswordChar = '*';                        
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
                        else if (dt_Select_Order_Details.Rows[0]["Deed_Chain"].ToString() == "False" || dt_Select_Order_Details.Rows[0]["Deed_Chain"].ToString() == "")
                        {
                            rdo_Deedchain.Checked = false;
                        }
                        //ViewState["Orderid"] = order_Id.ToString();
                        //Session["order_id"] = order_Id.ToString();
                        Orignal_Order_Number = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                        txt_OrderNumber.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                        DateTime Order_Recive_Date;
                        if (!(dt_Select_Order_Details.Rows[0]["Date"] is DBNull))
                        {
                            Order_Recive_Date = DateTime.ParseExact(dt_Select_Order_Details.Rows[0]["Date"].ToString(), "MM/dd/yyyy", null);
                            txt_Date.Text = Order_Recive_Date.ToString();
                        }
                        else
                        {
                            txt_Date.Text = "";
                        }

                        // txt_Date.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();

                        ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                        int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                        Order_Type_Id = int.Parse(ddl_ordertype.SelectedValue.ToString());

                        if (ordertype == 7 && txt_Order_Prior_Date.Text == " " && dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString() == "")
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
                        if (userroleid == "1" || userid == 179 || userid == 260)
                        {
                            try
                            {
                                ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                                if (ddl_ClientName.SelectedIndex > 0)
                                {
                                    clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                                }
                            }
                            catch (Exception ex)
                            {
                                ddl_ClientName.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            try
                            {
                                ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                                if (ddl_ClientName.SelectedIndex > 0)
                                {
                                    clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                                    dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                                }
                            }
                            catch (Exception ex)
                            {
                                ddl_ClientName.SelectedIndex = 0;
                            }
                        }
                        chk_Expidate.Checked = Convert.ToBoolean(dt_Select_Order_Details.Rows[0]["Expidate"].ToString());
                        try
                        {
                            ddl_SubProcess.SelectedValue = dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            ddl_SubProcess.SelectedIndex = 0;
                        }

                        dbc.Bind_Order_Assign_Type(ddl_County_Type);
                        txt_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                        txt_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                        txt_City.Text = dt_Select_Order_Details.Rows[0]["City"].ToString();
                        txt_Zip.Text = dt_Select_Order_Details.Rows[0]["Zip"].ToString();
                        ddl_State.SelectedValue = dt_Select_Order_Details.Rows[0]["stateid"].ToString();

                        txt_Abstractor_Notes.Text = dt_Select_Order_Details.Rows[0]["Abstractor_Note"].ToString();
                        try
                        {
                            ddl_Abstractor_Search_Type.SelectedValue = dt_Select_Order_Details.Rows[0]["Abstractor_Search_Type_ID"].ToString();
                        }
                        catch (Exception ex)
                        {
                            ddl_Abstractor_Search_Type.SelectedIndex = 0;
                        }
                        ddl_Client_Email.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Email_Id"].ToString();

                        DateTime Effective_date;
                        if (!(dt_Select_Order_Details.Rows[0]["Effective_date"] is DBNull))
                        {
                            Effective_date = DateTime.ParseExact(dt_Select_Order_Details.Rows[0]["Effective_date"].ToString(), "MM/dd/yyyy", null);
                            dtp_Effective_date.Text = Effective_date.ToString();
                        }
                        else
                        {
                            dtp_Effective_date.Text = "";
                        }

                        if (!string.IsNullOrEmpty(dt_Select_Order_Details.Rows[0]["Category_Type_Id"].ToString()))
                        {
                            ddlCategoryType.SelectedValue = dt_Select_Order_Details.Rows[0]["Category_Type_Id"];
                        }
                        else
                        {
                            ddlCategoryType.SelectedIndex = 0;
                        }
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
                            try
                            {
                                ddl_County.SelectedValue = dt_Select_Order_Details.Rows[0]["CountyId"].ToString();
                            }
                            catch (Exception ex)
                            {
                                ddl_County.SelectedIndex = 0;
                            }
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
                        txt_Vendor_Instructions.Text = dt_Select_Order_Details.Rows[0]["Vendor_Instructions"].ToString();

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
                        if (!string.IsNullOrEmpty(dt_Select_Order_Details.Rows[0]["Delq_Status"].ToString()) && dt_Select_Order_Details.Rows[0]["Delq_Status"].ToString() == "1")
                        {
                            labelDelinquent.Visible = true;
                        }
                    }
                    Order_Controls_Load();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }

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
                if (userroleid == "1" || userid == 179 || userid == 260)
                {
                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                }
                else
                {
                    dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                    //   dbc.BindSubProcessNumber(ddl_SubProcess, clientid);

                }
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
                User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\Delete1.png");



            }
            else if (int.Parse(dtorder.Rows[0]["count"].ToString()) <= 0)
            {

                User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\Sucess.png");

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

            if (ddl_SubProcess.SelectedIndex <= 0)
            {
                MessageBox.Show("Select SubProcess");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Select SubProcess')</script>", false);
                ddl_SubProcess.Focus();
                //   ddl_SubProcess.BorderColor = System.Drawing.Color.Red;
                return false;

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
            if (btn_Save.Text == "Add New Order")
            {
                if (ddlCategoryType.SelectedIndex < 1)
                {
                    MessageBox.Show("Select Target Category Type");
                    ddlCategoryType.Focus();
                    return false;
                }
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
            //if (ddl_Client_Email.SelectedIndex <= 0)
            //{
            //    dialogResult = MessageBox.Show("You are not Selected Client Email,Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {

            //        return true;
            //    }
            //    else if (dialogResult == DialogResult.No)
            //    {
            //        if (btn_text == "Submit")
            //        {
            //            btn_Save.Text = "Submit";
            //        }
            //        return false;
            //    }
            //}




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

        private bool ValidateAddress()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "CHECK_ORDER_ADDRESS");
            htselect.Add("@Address", txt_Address.Text.Trim().ToUpper());
            dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
            if (Convert.ToInt32(dtselect.Rows[0]["count"]) > 0)
            {
                if (DialogResult.Yes == XtraMessageBox.Show("This property Address already exists, Do you want to proceed?", "Confirm", MessageBoxButtons.YesNo))
                {
                    IsAddressConfirmed = true;
                    return true;
                }
                else
                {
                    IsAddressConfirmed = false;
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_ordertask.SelectedItem != null)
                {

                    htuser.Clear();
                    dtuser.Clear();

                    htuser.Add("@Trans", "SELECT_STATUSID");
                    htuser.Add("@Order_Status", ddl_ordertask.SelectedItem);
                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);


                    if (btn_Save.Text == "Add New Order" && Validate_OrderNo() != false && Validation() != false && validate_Update_Search() != false && ValidateAddress() != false)
                    {
                        Get_Maximum_OrderNumber();

                        string Order_Trim = txt_OrderNumber.Text.ToString();
                        Orignal_Order_Number = Order_Trim;
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
                        string Vendor_Instruction = txt_Vendor_Instructions.Text.ToUpper().ToString();
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

                        // This is for Tax Internal Client Orders Allocation

                        // This is commented because Directly moving to tax Option is disabled

                        ////==================================================

                        Hashtable ht_check = new Hashtable();
                        DataTable dt_check = new System.Data.DataTable();
                        ht_check.Add("@Trans", "CHECK");
                        ht_check.Add("@Client_Id", clientid);
                        ht_check.Add("@Order_Type_Id", ordertype);
                        ht_check.Add("@flag", "False");
                        dt_check = dataaccess.ExecuteSP("Sp_Tax_Order_Movement_Client_Product_Type", ht_check);

                        int Check_Count = 0;
                        if (dt_check.Rows.Count > 0)
                        {

                            Check_Count = int.Parse(dt_check.Rows[0]["COUNT"].ToString());
                        }
                        else
                        {

                            Check_Count = 0;
                        }


                        if (ordertype != 70 && ordertype != 110)
                        {
                            // this tax Order movement

                            //if (Check_Count != 1)
                            //{

                            if (subprocessid == 210 && state == 34)
                            {
                                htorder.Add("@Order_Status", 17);  // adding NC STATE AND SHOP_PNTG CLIENT ORDER TO ABSTRATCOR QUEUE

                            }
                            else
                            {
                                //Moving ABC & SPC Client Orders Into Re-Search Oreder Allocation Order Queue
                                if (clientid == 28 || clientid == 31)
                                {


                                    htorder.Add("@Order_Status", 25);

                                }
                                else
                                {

                                    if (County_Type == 2)
                                    {
                                        //moving abstrctshop client and abstractor order moving to research order queue
                                        if (County_Type == 2 && client_Id == 11)
                                        {
                                            htorder.Add("@Order_Status", 25);
                                        }
                                        else
                                        {
                                            htorder.Add("@Order_Status", 17);
                                        }
                                    }

                                    else if (County_Type != 2)
                                    {
                                        // Moving Search Order Allocation Queue
                                        htorder.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));

                                    }
                                }
                            }
                            //}
                            //else

                            //{
                            //    // This is for Tax Internal Client Orders Allocation

                            //    //==================================================


                            //    if (Check_Count > 0)
                            //    {
                            //        htorder.Add("@Order_Status", 26);

                            //    }
                            //}
                        }

                        else if (ordertype == 70 || ordertype == 110)
                        {
                            if (Check_Count == 0)
                            {

                                htorder.Add("@Order_Status", 21);
                            }
                        }


                        //======================================

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
                        htorder.Add("@Vendor_Instructions", Vendor_Instruction);

                        if (rdo_Deedchain.Checked == true)
                        {
                            htorder.Add("@Deed_Chain", "True");
                        }
                        else
                        {
                            htorder.Add("@Deed_Chain", "False");
                        }

                        if (chk_Expidate.Checked == true)
                        {
                            htorder.Add("@Expidate", "True");
                        }
                        else
                        {
                            htorder.Add("@Expidate", "False");
                        }
                        htorder.Add("@Order_Assign_Type", County_Type);
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
                        htorder.Add("@Client_Email_Id", ddl_Client_Email.SelectedValue.ToString());
                        htorder.Add("@Copy_Type", ddl_Copy_Type.SelectedItem.ToString());
                        htorder.Add("@Abstractor_Search_Type", ddl_Abstractor_Search_Type.SelectedValue.ToString());
                        if (ddlCategoryType.SelectedIndex > 0)
                        {
                            htorder.Add("@Category_Type_Id", ddlCategoryType.SelectedValue);
                        }
                        htorder.Add("@status", true);


                        Entered_OrderId = dataaccess.ExecuteSPForScalar("Sp_Order", htorder);


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


                        if (clientid == 28)//If Client ABC means Passing into the Reserach Order Queue
                        {
                            ht_Order_History.Add("@Status_Id", 25);
                        }
                        else
                        {
                            ht_Order_History.Add("@Status_Id", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));

                        }
                        ht_Order_History.Add("@Progress_Id", 8);
                        ht_Order_History.Add("@Assigned_By", userid);
                        ht_Order_History.Add("@Work_Type", 1);
                        if (btn_Save.Text == "Submit")
                        {
                            ht_Order_History.Add("@Modification_Type", "Order Update");
                        }
                        else
                        {
                            ht_Order_History.Add("@Modification_Type", "Order Create");
                        }
                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                        ////Assiging the VA Sate and FAIRFAX county will assign to the Rajani  User
                        // if (state == 47 && ordertype != 70)
                        // {
                        //     if (clientid != 28)
                        //     {

                        //         if (county == 2857 || county == 2858)
                        //         {


                        //             Assign_Order_For_User();

                        //         }
                        //     }
                        // }


                        //Assigning the Order to the Vendor Order Allocation

                        if (County_Type != 2 && ordertype != 70 && clientid != 28)
                        {
                            if (county != 2857 || county != 2858)
                            {
                                Vendor_Order_Allocate_New();
                            }

                        }


                        // This is Commented Because of Direct Tax Movement is not Using


                        if (Check_Count > 0)
                        {
                            // This is for Tax Internal Client Orders Allocation

                            //==================================================

                            Insert_Internal_Tax_Order_Status();


                            Hashtable htupdate = new Hashtable();
                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                            htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                            htupdate.Add("@Order_ID", Entered_OrderId);
                            htupdate.Add("@Search_Tax_Request", "True");

                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                            Hashtable httaxupdate = new Hashtable();
                            System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                            httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
                            httaxupdate.Add("@Order_ID", Entered_OrderId);
                            httaxupdate.Add("@Search_Tax_Request_Progress", 8);

                            dttaxupdate = dataaccess.ExecuteSP("Sp_Order", httaxupdate);



                            //OrderHistory
                            Hashtable ht_Order_History1 = new Hashtable();
                            DataTable dt_Order_History1 = new DataTable();
                            ht_Order_History1.Add("@Trans", "INSERT");
                            ht_Order_History1.Add("@Order_Id", Entered_OrderId);
                            ht_Order_History1.Add("@User_Id", userid);
                            ht_Order_History1.Add("@Status_Id", 26);
                            ht_Order_History1.Add("@Progress_Id", 8);
                            ht_Order_History1.Add("@Work_Type", 1);
                            ht_Order_History1.Add("@Assigned_By", userid);
                            ht_Order_History1.Add("@Modification_Type", "Order Moved Tax Queue");
                            dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);
                        }

                        //Assigning the order to The Tax Allocation

                        // this is for Tax External Client Orders
                        if (ordertype == 70 || ordertype == 110)
                        {

                            Insert_Tax_Order_Status();

                            Hashtable ht_Order_History1 = new Hashtable();
                            DataTable dt_Order_History1 = new DataTable();
                            ht_Order_History1.Add("@Trans", "INSERT");
                            ht_Order_History1.Add("@Order_Id", Entered_OrderId);
                            ht_Order_History1.Add("@User_Id", userid);
                            ht_Order_History1.Add("@Status_Id", 26);
                            ht_Order_History1.Add("@Progress_Id", 8);
                            ht_Order_History1.Add("@Work_Type", 1);
                            ht_Order_History1.Add("@Assigned_By", userid);
                            ht_Order_History1.Add("@Modification_Type", "Order Moved External Tax Queue");
                            dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

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

                            for (int i = 0; i < dtdoc.Rows.Count; i++)
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

                        Order_Uploads ops = new Order_Uploads("Insert", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);

                        ops.Hide();

                        //Order_Uploads.ActiveForm.Disposed += new EventHandler(close_Upload_Form);



                        MessageBox.Show("New Order Added Sucessfully");
                        Clear();
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('New Order Added Sucessfully')</script>", false);
                        //   GridviewbindOrderdata();
                        Get_Maximum_OrderNumber();

                        Control_Enable();

                    }
                    else if (btn_Save.Text == "Submit" && Validation() != false && validate_Update_Search() != false)
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
                        string Vendor_Instruction = txt_Vendor_Instructions.Text.ToUpper().ToString();
                        string City = txt_City.Text.ToString();
                        if (txt_Zip.Text != "")
                        {
                            zipcode = Convert.ToDouble(txt_Zip.Text);
                        }

                        string minute, sec, Hour;
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
                        htorder.Add("@Effective_Date", dtp_Effective_date.Text);
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
                        if (chk_Expidate.Checked == true)
                        {
                            htorder.Add("@Expidate", "True");
                        }
                        else
                        {
                            htorder.Add("@Expidate", "False");
                        }
                        htorder.Add("@Notes", Notes);
                        htorder.Add("@Vendor_Instructions", Vendor_Instruction);
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
                        if (ddlCategoryType.SelectedIndex > 0)
                        {
                            htorder.Add("@Category_Type_Id", ddlCategoryType.SelectedValue);
                        }
                        htorder.Add("@status", true);
                        dtorder = dataaccess.ExecuteSP("Sp_Order", htorder);

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
                        ddl_Order_Source.Enabled = true;
                        //  GridviewbindOrderdata();
                    }

                    if (btn_Save.Text == "Edit Order")
                    {

                        btn_Save.Text = "Submit";
                        Control_Enable();
                        ddl_ordertask.Enabled = false;
                        ddl_Order_Source.Enabled = false;
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
                                // ddl_ordertask.Enabled = true;
                            }
                        }
                        else if (btn_Save.Text == "Add New Order" && Validation_ControlEnable() != false && validate_Update_Search_Control_Enable() != false)
                        {
                            if (dialogResult != DialogResult.No && dialogResult != DialogResult.None && IsAddressConfirmed)
                            {
                                btn_Save.Text = "Submit";
                                Control_Enable();
                            }
                            else
                            {
                                btn_Save.Text = "Add New Order";
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
                        //  ddl_ordertask.Enabled = true;
                    }
                    MessageBox.Show("Select Order Task");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            httax.Add("@Order_Task", 21);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", 1);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);



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
                //if (ddl_Order_Source.SelectedItem != null)
                //{
                htsearch.Add("@Source", ddl_Order_Source.Text);
                htsearch.Add("@Order_source", ddl_Order_Source.SelectedValue);
                //}
                htsearch.Add("@No_Of_Hits", txt_No_of_hits.Text);
                htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
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
                //if (ddl_Order_Source.SelectedItem != null)
                //{
                htsearch.Add("@Source", ddl_Order_Source.Text);

                //}
                htsearch.Add("@No_Of_Hits", txt_No_of_hits.Text);
                htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                htsearch.Add("@Order_source", ddl_Order_Source.SelectedValue);
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
            // ddl_SubProcess.SelectedIndex = 0;
            ddlCategoryType.SelectedIndex = 0;
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
            txt_Zip.Text = "";
            Order_Id = 0;
            ddl_ordertask.SelectedIndex = 0;
            // ddl_Search_Type.SelectedIndex = 0;
            ddl_Order_Source.SelectedIndex = 0;
            txt_Search_cost.Text = "";
            txt_Copy_cost.Text = "";
            txt_Abstractor_Cost.Text = "";
            txt_noofpage.Text = "";
            txt_Notes.Text = "";
            lbl_County_Type.Text = "";
            txt_Order_Cost.Text = "";
            ddl_Client_Email.SelectedIndex = 0;
            chk_Expidate.Checked = false;
            rdo_Deedchain.Checked = false;
        }
        private void Control_Enable()
        {
            ddl_ClientName.Enabled = true;
            ddl_SubProcess.Enabled = true;
            ddlCategoryType.Enabled = true;
            ddl_Hour.Enabled = true;
            ddl_Minute.Enabled = true;
            ddl_Sec.Enabled = true;
            txt_Date.Enabled = true;
            txt_Order_Cost.Enabled = true;
            if (userroleid == "1")
            {
                ddl_County_Type.Enabled = true;
            }
            else
            {
                ddl_County_Type.Enabled = false;
            }
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

            ddl_ordertask.Enabled = false;
            //  ddl_Search_Type.Enabled = true;

            ddl_Order_Source.Enabled = true;
            txt_Search_cost.Enabled = true;
            txt_Copy_cost.Enabled = true;
            txt_No_of_documents.Enabled = true;
            txt_No_of_hits.Enabled = true;
            txt_Abstractor_Cost.Enabled = true;
            txt_noofpage.Enabled = true;
            txt_Notes.Enabled = true;
            lbl_County_Type.Text = "";
            txt_Order_Prior_Date.Enabled = true;
            ddl_Copy_Type.Enabled = true;
            txt_Abstractor_Notes.Enabled = true;
            txt_Vendor_Instructions.Enabled = true;

            txt_Search_cost.ReadOnly = false;
            txt_Copy_cost.ReadOnly = false;
            txt_No_of_hits.ReadOnly = false;
            txt_No_of_documents.ReadOnly = false;
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

            txt_Vendor_Instructions.ReadOnly = false;
            dtp_Effective_date.Enabled = true;


            rdo_Deedchain.Enabled = true;
            chk_Expidate.Enabled = true;
            DateCustom = 1;
        }
        private void Control_Enable_false()
        {
            ddl_ClientName.Enabled = false;
            ddl_SubProcess.Enabled = false;
            ddlCategoryType.Enabled = false;
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
            txt_No_of_hits.Enabled = false;
            txt_No_of_hits.ReadOnly = true;
            txt_No_of_documents.Enabled = false;
            txt_No_of_documents.ReadOnly = true;
            dtp_Effective_date.Enabled = false;

            txt_Vendor_Instructions.Enabled = false;
            txt_Vendor_Instructions.ReadOnly = true;
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
            chk_Expidate.Enabled = false;
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
                lbl_Final_Qc.Text = dtselect.Rows[0]["Final QC"].ToString();
                lbl_Upload.Text = dtselect.Rows[0]["Upload"].ToString();
                lbl_Exception.Text = dtselect.Rows[0]["Exception"].ToString();
                lbl_Comp.Text = dtselect.Rows[0]["Upload Completed"].ToString();


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
                lbl_Comp.Text = "";
            }

            Bind_Tax_Internal(order_id);
        }

        private void Bind_Tax_Internal(int Order_Id)
        {


            Hashtable ht_Get_tax = new Hashtable();
            DataTable dt_get_tax = new DataTable();


            ht_Get_tax.Add("@Trans", "GET_USER_TASK_DETAILS_FOR_TAX_INTERNAL_ORDER");
            ht_Get_tax.Add("@Order_ID", Order_Id);
            dt_get_tax = dataaccess.ExecuteSP("Sp_Order", ht_Get_tax);
            if (dt_get_tax.Rows.Count > 0)
            {

                lbl_Tax.Text = dt_get_tax.Rows[0]["User_Name"].ToString();
                lbl_tax_Task.Text = dt_get_tax.Rows[0]["Progress_Status"].ToString();

            }
            else
            {

                lbl_Tax.Text = "";
                lbl_tax_Task.Text = "";

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
                lbl_ScTask.Text = dtselect.Rows[0]["Search QC"].ToString();
                lbl_Ttask.Text = dtselect.Rows[0]["Typing"].ToString();
                lbl_TcTask.Text = dtselect.Rows[0]["Typing Qc"].ToString();
                lbl_Final_Qc_Task.Text = dtselect.Rows[0]["Final QC"].ToString();

                lbl_UTask.Text = dtselect.Rows[0]["Upload"].ToString();

                lbl_Exception_Task.Text = dtselect.Rows[0]["Exception"].ToString();


                lbl_Comp_task.Text = dtselect.Rows[0]["Upload Completed"].ToString();
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
                lbl_Comp_task.Text = "";

            }
        }
        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT");
            htComments.Add("@Order_Id", Order_Id);
            htComments.Add("@Work_Type", 1);

            dtComments = dataaccess.ExecuteSP("Sp_Order_Comments", htComments);
            Grid_Comments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.OliveDrab;
            Grid_Comments.EnableHeadersVisualStyles = false;
            Grid_Comments.Columns[0].Width = 850;
            Grid_Comments.Columns[1].Width = 150;
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

                if (userroleid == "1" || userroleid == "6" || userroleid == "4" || userroleid == "5")
                {

                    Grid_Comments.Columns[1].Visible = true;
                }
                else
                {

                    Grid_Comments.Columns[1].Visible = false;
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
            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            string time = date.ToString("hh:mm tt");

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

        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                Masters.New_County_Links County_links = new Masters.New_County_Links(int.Parse(ddl_State.SelectedValue.ToString()), int.Parse(ddl_County.SelectedValue.ToString()));
                County_links.Show();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something Went Wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
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

                    Order_Uploads Orderuploads = new Order_Uploads("Insert", Orderno, userid, txt_OrderNumber.Text, clientid.ToString(), Subprocess_id.ToString());
                    Orderuploads.Show();
                }
            }
            else
            {

                Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, clientid.ToString(), Subprocess_id.ToString());
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


        }

        private void ddl_ordertype_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddl_ordertype.SelectedIndex > 0)
            {
                string Order_Type_value = ddl_ordertype.Text.ToString();

                if (Order_Type_value != "")
                {
                    try

                    {
                        ddl_Abstractor_Search_Type.SelectedIndex = 0;
                        ddl_Abstractor_Search_Type.Text = ddl_ordertype.Text.ToString();
                    }
                    catch (Exception ex)
                    {
                        ddl_Abstractor_Search_Type.SelectedIndex = 0;
                    }
                }


                // ddl_Abstractor_Search_Type.SelectedValue = int.Parse(ddl_ordertype.SelectedValue.ToString());
                int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());
                //string abordertype = ddl_Abstractor_Search_Type.Text.ToString();
                //if (abordertype.ToString() == "")
                //{
                //    ddl_Abstractor_Search_Type.SelectedItem = ddl_ordertype.Text.ToString();
                //}


                if (ordertype == 7)
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
            Ordermanagement_01.Employee_Error_Entry EmployeeError = new Ordermanagement_01.Employee_Error_Entry(userid, userroleid, "", Order_Id, 3, 1, txt_OrderNumber.Text, Production_Date, 0, clientid);
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
            Ordermanagement_01.Order_Check_List_View check_List_View = new Ordermanagement_01.Order_Check_List_View(userid, Order_Id, 0, "AdminWise", 1, userroleid);
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

            ht_Order_History.Add("@Status_Id", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
            ht_Order_History.Add("@Progress_Id", 8);
            ht_Order_History.Add("@Assigned_By", userid);
            ht_Order_History.Add("@Work_Type", 1);
            if (btn_Save.Text == "Submit")
            {
                ht_Order_History.Add("@Modification_Type", "Order Update");
            }
            else
            {
                ht_Order_History.Add("@Modification_Type", "Order Create");
            }
            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

        }

        private void btn_OrderHistory_Click(object sender, EventArgs e)
        {

            Ordermanagement_01.OrderHistory Order_History = new Ordermanagement_01.OrderHistory(userid, userroleid, Order_Id, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text, ddl_State.Text, ddl_County.Text);
            Order_History.Show();
        }

        private void txt_OrderNumber_TextChanged(object sender, EventArgs e)
        {


            string Actual_Order_Number = txt_OrderNumber.Text;

            string CHnaging_Order_Number;

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
                User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\Delete1.png");
                //txt_OrderNumber.Text = "";
                Check_Order = 1;

            }
            else if (int.Parse(dtorder.Rows[0]["count"].ToString()) <= 0)
            {
                User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\Sucess.png");

                Check_Order = 0;

            }

            if (Order_Id != 0)
            {

                if (Orignal_Order_Number.ToString() != txt_OrderNumber.Text)
                {

                    Hashtable htorder1 = new Hashtable();
                    DataTable dtorder1 = new DataTable();


                    htorder1.Add("@Trans", "CHECK_ORDER_NUMBER");
                    htorder1.Add("@Client_Order_Number", txt_OrderNumber.Text);
                    dtorder1 = dataaccess.ExecuteSP("Sp_Order", htorder1);
                    User_Chk_Img.Image = null;
                    if (int.Parse(dtorder1.Rows[0]["count"].ToString()) > 0)
                    {

                        MessageBox.Show("This Order Number is Already Exist");

                        txt_OrderNumber.Clear();

                    }


                }



            }
        }

        private void ddl_SubProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_SubProcess.SelectedIndex > 0)
            {

                Subprocess_id = int.Parse(ddl_SubProcess.SelectedValue.ToString());


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
                //Ordermanagement_01.MarkerMaker.Image_Marker_Maker Markermaker = new Ordermanagement_01.MarkerMaker.Image_Marker_Maker(Order_Id, Session_Order_Task, txt_OrderNumber.Text, ddl_ordertask.Text.ToString(), Client_no, "", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                //Markermaker.Show();
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

                Ordermanagement_01.Masters.Judgement_Period_Create_View js = new Ordermanagement_01.Masters.Judgement_Period_Create_View(userid, Stat_Id, userroleid);
                js.Show();
            }


        }


        //===================================================Its an Vendor Area=================================================

        private void Vendor_Order_Allocate()
        {
            int Order_Type_ABS;
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABBR");
            ht_BIND.Add("@Order_Type", ddl_ordertype.Text.ToString());
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

                        Hashtable htcheck_Vendor_Order_Type_Abs = new Hashtable();
                        DataTable dtcheck_Vendor_Order_Type_Abs = new DataTable();
                        htcheck_Vendor_Order_Type_Abs.Add("@Trans", "GET_VENDOR_ORDER_TYPE_COVERAGE");
                        htcheck_Vendor_Order_Type_Abs.Add("@Vendor_Id", Vendor_Id);
                        htcheck_Vendor_Order_Type_Abs.Add("@Order_Type_Abs_Id", Order_Type_ABS);
                        dtcheck_Vendor_Order_Type_Abs = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", htcheck_Vendor_Order_Type_Abs);
                        int Ven_Order_Type_Abs_Count = 0;
                        if (dtcheck_Vendor_Order_Type_Abs.Rows.Count > 0)
                        {

                            Ven_Order_Type_Abs_Count = int.Parse(dtcheck_Vendor_Order_Type_Abs.Rows[0]["count"].ToString());
                        }
                        else
                        {
                            Ven_Order_Type_Abs_Count = 0;

                        }


                        if (Ven_Order_Type_Abs_Count != 0)
                        {

                            int Ven_Client_Sun_Client_Count;

                            Hashtable htget_vendor_Client_And_Sub_Client = new Hashtable();
                            DataTable dtget_Vendor_Client_And_Sub_Client = new DataTable();

                            htget_vendor_Client_And_Sub_Client.Add("@Trans", "GET_VENDOR_ON_CLIENT_AND_SUB_CLIENT");
                            htget_vendor_Client_And_Sub_Client.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                            htget_vendor_Client_And_Sub_Client.Add("@Sub_Client_Id", int.Parse(ddl_SubProcess.SelectedValue.ToString()));
                            htget_vendor_Client_And_Sub_Client.Add("@Venodor_Id", Vendor_Id);
                            dtget_Vendor_Client_And_Sub_Client = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htget_vendor_Client_And_Sub_Client);

                            if (dtget_Vendor_Client_And_Sub_Client.Rows.Count > 0)
                            {

                                Ven_Client_Sun_Client_Count = int.Parse(dtget_Vendor_Client_And_Sub_Client.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                Ven_Client_Sun_Client_Count = 0;
                            }


                            if (Ven_Client_Sun_Client_Count != 0)
                            {




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

                                    Vend_date = dtetcdate.Rows[0]["Date"].ToString();


                                    Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());

                                    Hashtable htvendor_Percngate = new Hashtable();
                                    System.Data.DataTable dtvendor_percentage = new System.Data.DataTable();

                                    htvendor_Percngate.Add("@Trans", "GET_VENDOR_PERCENTAGE_OF_ORDERS");
                                    htvendor_Percngate.Add("@Venodor_Id", Vendor_Id);
                                    htvendor_Percngate.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                                    dtvendor_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendor_Percngate);

                                    if (Vendor_Order_capacity != 0)
                                    {

                                        if (dtvendor_percentage.Rows.Count > 0)
                                        {
                                            Vendor_Order_Percentage = Convert.ToDecimal(dtvendor_percentage.Rows[0]["Percentage"].ToString());

                                            if (Vendor_Order_Percentage != 0)
                                            {

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


                                                Vendor_Total_No_Of_Order_Recived = No_Of_Order_Assignd_for_Vendor;


                                                //   if((Vendor_Order_Percentage/Vendor_Total_No_Of_Order_Recived)==)


                                                //Formula 

                                                //   Vendor%
                                                //------------------- * Total_No_Of_Order_recived  <= Vendor_Capacity
                                                // 100



                                                var Vendor_Allocation_Output = (Vendor_Order_Percentage / 100) * Vendor_Total_No_Of_Order_Recived;




                                                decimal Vendor_Order_Allocation_Count = Convert.ToDecimal(Vendor_Allocation_Output.ToString());

                                                if (Vendor_Order_Allocation_Count < Vendor_Order_capacity && No_Of_Order_Assignd_for_Vendor < Vendor_Order_capacity)
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
                                            }






                                            //OrderHistory

                                        }

                                    }
                                    else
                                    {


                                    }
                                }







                            }
                        }

                        //MessageBox.Show("Order Assigned Sucessfully");
                    }
                }

            }
        }

        private void Vendor_Order_Allocate_New()
        {

            int Order_Type_ABS;
            Vendor_Order_Allocation_Count = 0;
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABBR");
            ht_BIND.Add("@Order_Type", ddl_ordertype.Text.ToString());
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
                int State_Id = int.Parse(ddl_State.SelectedValue.ToString());

                int County_Id = int.Parse(ddl_County.SelectedValue.ToString());
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

                        Hashtable htget_vendor_Client_And_Sub_Client = new Hashtable();
                        DataTable dtget_Vendor_Client_And_Sub_Client = new DataTable();

                        htget_vendor_Client_And_Sub_Client.Add("@Trans", "GET_VENDOR_ON_CLIENT_AND_SUB_CLIENT");
                        htget_vendor_Client_And_Sub_Client.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                        htget_vendor_Client_And_Sub_Client.Add("@Sub_Client_Id", int.Parse(ddl_SubProcess.SelectedValue.ToString()));
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
                                        htvendor_Percngate.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
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
                                                htvendor_Bal_Percngate.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
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
                                                htcheck_Percentage.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
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
                                                        ht_Insert_Temp.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
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
                                                        ht_Update_Perentage.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
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
                                ht_Get_Max_Vendor.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
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
                                                ht_Update_Perentage.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                                                ht_Update_Perentage.Add("@Vendor_Capcity", Vendor_Order_capacity);
                                                ht_Update_Perentage.Add("@Vendor_Balance_Perntage", Total_Vendor_Alloacated_Percentage);
                                                ht_Update_Perentage.Add("@No_Of_Order_Assigned", No_Of_Order_Assignd_for_Vendor);
                                                dt_Update_Perentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Update_Perentage);



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



        private void grp_OrderEntry_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {

                string Stat_Id = ddl_State.SelectedValue.ToString();

                Ordermanagement_01.Employee.State_Wise_Tax_Due_Date tax = new Ordermanagement_01.Employee.State_Wise_Tax_Due_Date(userid, Stat_Id, userroleid);
                tax.Show();
            }
        }

        private void txt_Notes_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Order_Notes_Click(object sender, EventArgs e)
        {
            string State_ID = Convert.ToString(ddl_State.SelectedValue);
            Ordermanagement_01.Employee.Employee_Order_Information emp = new Ordermanagement_01.Employee.Employee_Order_Information(userid, State_ID, Order_Id, userroleid, 0, Session_Order_Task);
            emp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Research_Order_History oe = new Research_Order_History(Order_Id, userid, 25, int.Parse(ddl_County.SelectedValue.ToString()));
            oe.Show();
        }

        private void btn_vendorComments_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Order_View vr = new Vendors.Vendor_Order_View(Order_Id, userid, int.Parse(userroleid));
            vr.Show();
        }

        private void btn_Titlelogy_Invoice_Click(object sender, EventArgs e)
        {
            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            if (dt_Order_InTitleLogy.Rows.Count > 0)
            {
                Ordermanagement_01.InvoiceRep.Titlelogy_Invoice_Entry tinv = new InvoiceRep.Titlelogy_Invoice_Entry(Order_Id, userid, Order_Type_Id);
                tinv.Show();
            }
            else
            {

                MessageBox.Show("This Order is Not Imported From Titlelogy");
            }

        }

        private void btn_Search_Links_Click(object sender, EventArgs e)
        {
            if (ddl_County.SelectedIndex > 0)
            {
                Ordermanagement_01.Employee.Searcher_New_Link_history Search_LinkHistory = new Ordermanagement_01.Employee.Searcher_New_Link_history(Order_Id, Session_Order_Task, userid, int.Parse(userroleid.ToString()), txt_OrderNumber.Text, 0);
                Search_LinkHistory.Show();
            }

        }

        private void txt_Zip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Order_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Search_cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Copy_cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Abstractor_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_noofpage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar == 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_No_of_hits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_No_of_documents_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_No_of_documents_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
    e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void btn_Typing_Entry_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("https://titlelogy.com/Typing/Orders/EntryTyping_Document.aspx?Order_Id=" + Order_Id.ToString() + "&User_Id=" + userid.ToString() + "");
            Order_Entry_Type_Document typingDoc = new Order_Entry_Type_Document(Order_Id, userid);
            typingDoc.Show();
        }
    }
}
