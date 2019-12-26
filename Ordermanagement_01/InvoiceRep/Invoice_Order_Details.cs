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
using System.Globalization;

namespace Ordermanagement_01.Invoice
{
    public partial class Invoice_Order_Details : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
         

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
        int Autoinvoice_No;
        int Invoice_No;
     
        decimal invoice_Search_Cost, Invoice_Copy_Cost,Invoice_CGI_Land_Cost,Invoice_Addional_fee;
        string Invoice_Number;
        string Operation;
        string Inv_Num;
        string User_Role;
        public Invoice_Order_Details(int Orderid, int User_Id,string OPERATION,string INV_NUM,string USER_ROLE)
        {
            InitializeComponent();
            userid = User_Id;
            Operation=OPERATION;
            Order_Id = Orderid;
            User_Role = USER_ROLE;
            if (User_Role == "1" || userid == 260 || userid == 179)
            {
                dbc.BindClientName(ddl_Client_Search);
            }
            else
            {
                dbc.BindClientNo_for_Report(ddl_Client_Search);

            }

            txt_Production_Date.Text = DateTime.Now.ToString();

            if (Order_Id != 0)
            {
                if (Operation == "Insert")
                {
                    load_order_masters();
                    Order_Load();
                    txt_Invoice_Date.Text = DateTime.Now.ToString("mm/dd/yyyy");
                    txt_Invoice_Order_Number.Visible = true;
                    lbl_Enter_Order.Visible = true;
                    Group_Order_Numbers.Enabled = true;
                    lbl_Invoice.Visible = false;
                    lbl_Invoice_Number.Visible = false;
                 

                    txt_Production_Date.Text = DateTime.Now.ToString();
                }
                else if (Operation == "Update")
                {
                    txt_Invoice_Order_Number.Visible = false;
                    lbl_Enter_Order.Visible = false;
                    lbl_Invoice.Visible = true;
                    lbl_Invoice_Number.Visible = true;
                    btn_Save.Text = "Edit Invoice";
                    Group_Order_Numbers.Enabled = false;
                    Inv_Num = INV_NUM.ToString();
                    load_order_masters();
                    Order_Load();
                    Load_Invoice_Details();
                    if (client_Id == 4 && ddl_State.SelectedValue.ToString() == "31")
                    {
                        txt_CGI_Title_Land_Amount.Visible = false;
                    }
                    lbl_Invoice_Number.Text = Inv_Num.ToString();
                }

             
            }
        }

        public void load_order_masters()
        {
            if (User_Role == "1"|| userid == 260 || userid == 179)
            {
                dbc.BindClientName(ddl_ClientName);
                dbc.BindClientName(ddl_Client_Search);
            }
            else 
            {

                dbc.BindClientNo_for_Report(ddl_ClientName);
                dbc.BindClientNo_for_Report(ddl_Client_Search);
            }
            dbc.BindOrderType(ddl_ordertype);
          
           
            dbc.BindState(ddl_State);
            client_Id = int.Parse(ddl_ClientName.SelectedValue.ToString());
          
            ddl_ordertask.Visible = true;
            ddl_ordertask.Items.Insert(0, "Search");
            ddl_ordertask.Items.Insert(1, "Typing");
            ddl_ordertask.Items.Insert(2, "Upload");
            ddl_ordertask.Items.Insert(3, "Upload Completed");
            ddl_ordertask.Items.Insert(4, "Abstractor");
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
         

            // SetMyCustomFormat();


            if (ddl_ClientName.SelectedIndex != 0)
            {
                if (User_Role == "1" || userid == 260 || userid == 179)
                {
                    dbc.BindSubProcessName(ddl_SubProcess, client_Id);
                }
                else 
                {
                    dbc.BindSubProcessNumber(ddl_SubProcess, client_Id);

                }
             
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
        }

        public void Load_Invoice_Details()
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT");
            ht.Add("@Order_ID",Order_Id);
            dt = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", ht);
            if (dt.Rows.Count > 0)
            {

                txt_Invoice_Search_Cost.Text = dt.Rows[0]["Search_Cost"].ToString();
                txt_Invoice_Copy_Cost.Text = dt.Rows[0]["Copy_Cost"].ToString();
                if(dt.Rows[0]["Invoice_Date"].ToString()!="")
                {
                  
                    txt_Invoice_Date.Text = dt.Rows[0]["Invoice_Date"].ToString();
                }
                else
                {
                
                      
                {
                    
                  
                    txt_Invoice_Date.Text = "";

                }
                }
                txt_Invoice_comments.Text = dt.Rows[0]["Comments"].ToString();

                if (client_Id == 4 && ddl_State.SelectedValue.ToString() == "31")
                {

                    txt_CGI_Title_Land_Amount.Text = dt.Rows[0]["CGI_Title_Land_Amount"].ToString();
                }
                txt_AdditionalFees.Text = dt.Rows[0]["Additional_Fees"].ToString();
                txt_AdditionalComments.Text = dt.Rows[0]["AdditionalFees_Comments"].ToString();
            }


        }
        private void Invoice_Order_Details_Load(object sender, EventArgs e)
        {
            string D1 = DateTime.Now.ToString("MM/dd/yyyy");
            txt_Date.Value = DateTime.Now;
            txt_Date.Format = DateTimePickerFormat.Custom;
            txt_Date.CustomFormat = "MM/dd/yyyy";
            txt_Date.Text = D1;


            txt_Invoice_Date.Value = DateTime.Now;
            txt_Invoice_Date.Format = DateTimePickerFormat.Custom;
            txt_Invoice_Date.CustomFormat = "MM/dd/yyyy";
            txt_Invoice_Date.Text = D1;

            
            if (Order_Id != 0)
            {

                int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                if (User_Role == "1")
                {
                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                }
                else 
                {
                    dbc.BindSubProcessNumber(ddl_SubProcess, clientid);

                }
                ddl_Client_Search_SelectedIndexChanged( sender,  e);
                if (ddl_ClientName.SelectedIndex != 0)
                {
                    //  int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                    if (User_Role == "1"  || userid == 260 || userid == 179)
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

                Order_Load();
                if (client_Id == 4 && ddl_State.SelectedValue.ToString() == "31")
                {
                    lbl_CGI_Title_Amt.Visible = true;
                    txt_CGI_Title_Land_Amount.Visible = true;
                }
                else
                {
                    lbl_CGI_Title_Amt.Visible = false;
                    txt_CGI_Title_Land_Amount.Visible = false;
                    label29.Visible = false;
                }
                Load_No_Of_pages();
            }
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
                 

                    ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                    ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                    if (ddl_ClientName.SelectedIndex > 0)
                    {

                        client_Id = int.Parse(ddl_ClientName.SelectedValue.ToString());
                        if (User_Role == "1" || userid == 260 || userid == 179)
                        {
                            dbc.BindSubProcessName(ddl_SubProcess, client_Id);
                        }
                        else 
                        {
                            dbc.BindSubProcessNumber(ddl_SubProcess, client_Id);

                        }
                
                    }

                    ddl_SubProcess.SelectedValue = dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString();

                    Subprocess_id = int.Parse(ddl_SubProcess.SelectedValue.ToString());

                    Subprocess_id = int.Parse(ddl_SubProcess.SelectedValue.ToString());
                    txt_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                    txt_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                    txt_City.Text = dt_Select_Order_Details.Rows[0]["City"].ToString();
                    txt_Zip.Text = dt_Select_Order_Details.Rows[0]["Zip"].ToString();
                    ddl_State.SelectedValue = dt_Select_Order_Details.Rows[0]["stateid"].ToString();
                    txt_Date.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();
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
                    if (Operation != "Update")
                    {
                        txt_Invoice_Date.Text = DateTime.Now.ToString();
                    }
                }

            }

        }

        private void Load_No_Of_pages()
        {

            Hashtable htget_No_Of_Pages = new Hashtable();
            DataTable dtget_No_Of_Pages = new DataTable();
            htget_No_Of_Pages.Add("@Trans", "GET_NO_OF_PAGES");
            htget_No_Of_Pages.Add("@Order_ID",Order_Id);
            dtget_No_Of_Pages = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htget_No_Of_Pages);

            if (dtget_No_Of_Pages.Rows.Count > 0)
            {

                txt_No_Of_Pages.Text = dtget_No_Of_Pages.Rows[0]["No_Of_pages"].ToString();

            }
            else
            {

                txt_No_Of_Pages.Text = "";

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
        private void label34_Click(object sender, EventArgs e)
        {

        }

        

        private void txt_Invoice_Order_Number_TextChanged(object sender, EventArgs e)
        {
            string Order_Number = txt_Invoice_Order_Number.Text;
            if(Order_Number!="")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "GET_ORDER_ID");
                ht.Add("@Client_Order_Number", Order_Number);
                dt = dataaccess.ExecuteSP("Sp_Order", ht);

                if (dt.Rows.Count > 0)
                {
                    Order_Id = int.Parse(dt.Rows[0]["Order_ID"].ToString());
                    load_order_masters();
                    Order_Load();
                    if (client_Id == 4 && ddl_State.SelectedValue.ToString() == "31")
                    {
                        lbl_CGI_Title_Amt.Visible = true;
                        txt_CGI_Title_Land_Amount.Visible = true;
                    }
                    else
                    {
                        lbl_CGI_Title_Amt.Visible = false;
                        txt_CGI_Title_Land_Amount.Visible = false;
                        label29.Visible = false;
                    }
                }

                //else
                //{

                //    MessageBox.Show("This Order Number is not avilable with us");
                //    txt_Invoice_Order_Number.Focus();
                //}

            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Validate_Invoice() != false && btn_Save.Text == "Genrate Invoice")
            {
                Hashtable htmax = new Hashtable();
                DataTable dtmax = new DataTable();
                htmax.Add("@Trans", "GET_MAX_INVOICE_AUTO_NUMBER");
                htmax.Add("@Client_Id", client_Id);
                //txt_Invoice_Date.Text = Convert.ToDateTime("01/01/2018").ToString();
                
                dtmax = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htmax);
                if (dtmax.Rows.Count > 0)
                {
                    Autoinvoice_No = int.Parse(dtmax.Rows[0]["Invoice_Auto_No"].ToString());
                }
               
                 if (txt_Invoice_Search_Cost.Text != "")
                            {

                                invoice_Search_Cost = Convert.ToDecimal(txt_Invoice_Search_Cost.Text.ToString());
                            }
                            else
                            {
                                invoice_Search_Cost = 0;

                            }

                            if (txt_Invoice_Copy_Cost.Text != "")
                            {

                                Invoice_Copy_Cost = Convert.ToDecimal((txt_Invoice_Copy_Cost.Text.ToString()));

                            }
                            else
                            {
                                Invoice_Copy_Cost = 0;

                            }
                            if (txt_CGI_Title_Land_Amount.Text != "")
                            {

                                Invoice_CGI_Land_Cost = Convert.ToDecimal((txt_CGI_Title_Land_Amount.Text.ToString()));

                            }
                            else
                            {
                                Invoice_CGI_Land_Cost = 0;

                            }

                            if (txt_AdditionalFees.Text != "")
                            {

                                Invoice_Addional_fee = Convert.ToDecimal((txt_AdditionalFees.Text.ToString()));

                            }
                            else
                            {
                                Invoice_Addional_fee = 0;

                            }


                            Hashtable htmax_Invoice_No = new Hashtable();
                            DataTable dtmax_invoice_No = new DataTable();
                            htmax_Invoice_No.Add("@Trans", "GET_MAX_INVOICE_NUMBER");
                            htmax_Invoice_No.Add("@Client_Id", client_Id);
                            dtmax_invoice_No = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htmax_Invoice_No);

                            if (dtmax_invoice_No.Rows.Count > 0)
                            {

                                Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
                            }


                           //string nnn = no.ToString();
                            Hashtable htcheck = new Hashtable();
                            DataTable dtcheck=new DataTable ();

                            htcheck.Add("@Trans", "CHECK");
                            htcheck.Add("@Order_ID",Order_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htcheck);
                            int check = int.Parse(dtcheck.Rows[0]["Count"].ToString());
                            if (check == 0)
                            {

                                Hashtable ht = new Hashtable();
                                DataTable dt = new DataTable();

                                ht.Add("@Trans", "INSERT");
                                ht.Add("@Order_ID", Order_Id);
                                ht.Add("@Client_Id", client_Id);
                                ht.Add("@Subprocess_ID", Subprocess_id);
                                ht.Add("@Invoice_Auto_No", Autoinvoice_No);
                                ht.Add("@Invoice_No", Invoice_Number);
                                ht.Add("@Search_Cost", invoice_Search_Cost);
                                ht.Add("@Copy_Cost", Invoice_Copy_Cost);
                                ht.Add("@Invoice_Date", txt_Invoice_Date.Text);
                                ht.Add("@Comments", txt_Invoice_comments.Text);
                                ht.Add("@Email_Status","False");
                                ht.Add("@Revised", "False");
                                ht.Add("@Status", "True");
                                ht.Add("@Inserted_By", userid);
                                ht.Add("@CGI_Title_Land_Amount",Invoice_CGI_Land_Cost);
                                ht.Add("@Additional_Fees",Invoice_Addional_fee);
                                ht.Add("@AdditionalFees_Comments", txt_AdditionalComments.Text);
                                dt = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", ht);
                                MessageBox.Show("Invoice Genrated Sucessfully");

                                //Update No of Pages 

                                Hashtable htupdate_No_Of_Pages = new Hashtable();
                                DataTable dtupdate_No_Of_Pages = new DataTable();
                                htupdate_No_Of_Pages.Add("@Trans", "UPDATE_No_OF_PAGES");
                                htupdate_No_Of_Pages.Add("@Order_Id",Order_Id);
                                htupdate_No_Of_Pages.Add("@No_Of_pages", int.Parse(txt_No_Of_Pages.Text));
                                dtupdate_No_Of_Pages = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htupdate_No_Of_Pages);


                                Clear();
                            }
                            else
                            {


                                MessageBox.Show("Invoice already Genrated Please Check");


                            }
            }
            else if (Validate_Invoice() != false && btn_Save.Text == "Edit Invoice")

            {

                if (txt_Invoice_Search_Cost.Text != "")
                {

                    invoice_Search_Cost = Convert.ToDecimal(txt_Invoice_Search_Cost.Text.ToString());

                }
                else
                {
                    invoice_Search_Cost = 0;

                }

                if (txt_Invoice_Copy_Cost.Text != "")
                {

                    Invoice_Copy_Cost = Convert.ToDecimal((txt_Invoice_Copy_Cost.Text.ToString()));

                }
                else
                {
                    Invoice_Copy_Cost = 0;

                }

                if (txt_CGI_Title_Land_Amount.Text != "")
                {

                    Invoice_CGI_Land_Cost = Convert.ToDecimal((txt_CGI_Title_Land_Amount.Text.ToString()));

                }
                else
                {
                    Invoice_CGI_Land_Cost = 0;

                }


                if (txt_AdditionalFees.Text != "")
                {

                    Invoice_Addional_fee = Convert.ToDecimal((txt_AdditionalFees.Text.ToString()));

                }
                else
                {
                    Invoice_Addional_fee = 0;

                }





                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "UPDATE");
                ht.Add("@Order_ID", Order_Id);
                ht.Add("@Search_Cost", invoice_Search_Cost);
                ht.Add("@Copy_Cost", Invoice_Copy_Cost);
                ht.Add("@CGI_Title_Land_Amount", Invoice_CGI_Land_Cost);
                ht.Add("@Additional_Fees", Invoice_Addional_fee);
                ht.Add("@AdditionalFees_Comments", txt_AdditionalComments.Text);
                ht.Add("@Invoice_Date", txt_Invoice_Date.Text);
                ht.Add("@Comments", txt_Invoice_comments.Text);
                ht.Add("@Status", "True");
                ht.Add("@Modified_By", userid);

                dt = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", ht);
                //Update No of Pages 

                Hashtable htupdate_No_Of_Pages = new Hashtable();
                DataTable dtupdate_No_Of_Pages = new DataTable();
                htupdate_No_Of_Pages.Add("@Trans", "UPDATE_No_OF_PAGES");
                htupdate_No_Of_Pages.Add("@Order_Id", Order_Id);
                htupdate_No_Of_Pages.Add("@No_Of_pages", int.Parse(txt_No_Of_Pages.Text));
                dtupdate_No_Of_Pages = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htupdate_No_Of_Pages);

                MessageBox.Show("Invoice Updated Sucessfully");
                this.Close();
                Clear();

            }
        }

        public void Clear()
        {
            txt_Invoice_Order_Number.Text = "";
            txt_Invoice_comments.Text = "";
            txt_Invoice_Search_Cost.Text = "";
            txt_Invoice_Copy_Cost.Text = "";
            txt_Invoice_comments.Text = "";
            btn_Save.Text = "Genrate Invoice";
            txt_CGI_Title_Land_Amount.Text = "";
            txt_AdditionalFees.Text = "";
            txt_AdditionalComments.Text = "";
            txt_No_Of_Pages.Text = "";
            txt_Invoice_Order_Number.Visible = true;
            lbl_Enter_Order.Visible = true;

            lbl_Invoice.Visible = false;
            lbl_Invoice_Number.Visible = false;
            bind_OrderList();
            Order_Id = 0;
            //load_order_masters();
            //Order_Load();
            txt_Invoice_Order_Number.Focus();


        }


        public bool Validate_Invoice()
        {
            if (txt_Invoice_Search_Cost.Text == "")
            {
                MessageBox.Show("Please Enter Search Cost");
                txt_Invoice_Search_Cost.Focus();
                return false;
            }
            if (txt_Invoice_Copy_Cost.Text == "")
            {

                MessageBox.Show("Please enter Copy Cost");
                txt_Invoice_Copy_Cost.Focus();
                return false;
            }
            if (txt_Invoice_Date.Text == "")
            {

                MessageBox.Show("Please enter Invoice Date");
                txt_Invoice_Date.Focus();
                return false;
            }
            if (txt_No_Of_Pages.Text == "")
            {

                MessageBox.Show("Please enter Number Of Pages");
                txt_No_Of_Pages.Focus();
                return false;
            }
            if (txt_CGI_Title_Land_Amount.Text == "" && client_Id == 4 && ddl_State.SelectedValue.ToString()=="31")
            {

                MessageBox.Show("Please enter CGI title Land Amount");
                txt_CGI_Title_Land_Amount.Focus();
                return false;
            }
            if (txt_AdditionalFees.Text == "")
            {
                MessageBox.Show("Please enter Additional Fees");
                txt_AdditionalFees.Focus();
                return false;
            }
            return true;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            this.Close();
           
           
        }

        

        private void txt_Invoice_Date_ValueChanged(object sender, EventArgs e)
        {
           
      
        }

        private void txt_Production_Date_ValueChanged(object sender, EventArgs e)
        {
            bind_OrderList();

        }
        private void bind_OrderList()
            {

                if (ddl_Client_Search.SelectedIndex > 0 && txt_Production_Date.Text != "")
                {
                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                    DateTime fromdate = Convert.ToDateTime(txt_Production_Date.Text, usDtfi);
                    ht.Add("@Trans", "GET_ORDERS_LIST");
                    ht.Add("@Client_Id", int.Parse(ddl_Client_Search.SelectedValue.ToString()));
                    ht.Add("@Production_Date", fromdate);


                    dt = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", ht);

                    if (dt.Rows.Count > 0)
                    {
                        //ex2.Visible = true;
                        Grid_Invoice_Details.Rows.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Grid_Invoice_Details.Rows.Add();

                            Grid_Invoice_Details.Rows[i].Cells[0].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                            Grid_Invoice_Details.Rows[i].Cells[1].Value = dt.Rows[i]["Order_ID"].ToString();

                        }
                    }
                    else
                    {
                        Grid_Invoice_Details.Rows.Clear();
                        Grid_Invoice_Details.DataSource = null;
                        // lbl_Total_Orders.Text = "0";
                        //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                        //grd_Admin_orders.DataBind();
                    }
                }
        }

        private void ddl_Client_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_OrderList();
        }

        private void Grid_Invoice_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                 Order_Id = int.Parse(Grid_Invoice_Details.Rows[e.RowIndex].Cells[1].Value.ToString());
                load_order_masters();
                Order_Load();

                if (client_Id == 4 && ddl_State.SelectedValue == "31")
                {
                    lbl_CGI_Title_Amt.Visible = true;
                    txt_CGI_Title_Land_Amount.Visible = true;
                }
                else
                {
                    lbl_CGI_Title_Amt.Visible = false;
                    txt_CGI_Title_Land_Amount.Visible = false;
                    label29.Visible = false;
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.SelectedValue.ToString(), ddl_SubProcess.SelectedValue.ToString());
            Orderuploads.Show();
        }

        private void lbl_CGI_Title_Amt_Click(object sender, EventArgs e)
        {

        }

        private void txt_Invoice_Search_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_Invoice_Copy_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                  (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_No_Of_Pages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_CGI_Title_Land_Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_AdditionalFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
