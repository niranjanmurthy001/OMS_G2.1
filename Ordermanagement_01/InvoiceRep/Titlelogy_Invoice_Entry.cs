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

namespace Ordermanagement_01.InvoiceRep
{
    public partial class Titlelogy_Invoice_Entry : Form
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

        int Chk_Order_Search_Cost;
        string OPERATE_SEARCH_COST;
        int SubprocessId, ClientId;
        decimal Totalcost;
        int Autoinvoice_No;
        int Invoice_No;


      
        string Operation;
        string Inv_Num;
        
        decimal invoice_Search_Cost, Invoice_Copy_Cost, Invoice_Order_Cost;
        decimal Inhouse_Search_Cost, Inhouse_Copy_Cost, Inhouse_Order_Cost,Inhouse_Drn_cost,Inhouse_Db_Cost;
        int No_Of_Pages, Inhouse_No_Of_Pages;
        string Invoice_Number;
        decimal Search_Cost, Total_Cost;
        string Record_View_Type;
        int External_Order_Id, External_Client_Id, External_Sub_Client_Id;
        int Inhouse_Client_Id, Inhouse_SubClient_Id, Inhouse_Order_Type_Id,Inhouse_State_Id,Inhouse_County_Id;
        decimal Titelogy_Order_Type_Wise_Invoice_Amount, Title_Logy_Probate_Cost, Title_Logy_Platmap_Cost, Total_Titlelogy_Order_Cost;
        int Title_Peak_Inv_No_Of_Pages, Title_Peak_Inv_No_Probate_Pages, Title_Peak_Inv_No_Plat_Map_Pages, Title_Peak_Inv_Total_No_Probate_And_Plat_Map_Pages;
        int Order_Type_Id;
        public Titlelogy_Invoice_Entry(int Orderid, int User_Id,int ORDER_TYPE_ID)
        {
            InitializeComponent();

            userid = User_Id;
          
            Order_Id = Orderid;

            Order_Type_Id = ORDER_TYPE_ID;

            txt_Invoice_Date.Text = DateTime.Now.ToString();

            if (Order_Id != 0)
            {
               
                  
                    load_order_masters();
                    Order_Load();
                    Load_Invoice_Details();

                    if (client_Id == 33 && Subprocess_id == 300)
                    {

                        if (Order_Type_Id == 113 || Order_Type_Id == 117 || Order_Type_Id == 115)
                        {

                            lbl_Probate.Visible = true;
                            lbl_Plat.Visible = true;
                            txt_Probate_Pages.Visible = true;
                            txt_Plat_Pages.Visible = true;
                            txt_Total_Cost.Visible = true;
                            lbl_Total.Visible = true;

                        }
                        else
                        {
                            lbl_Probate.Visible = false;
                            lbl_Plat.Visible = false;
                            txt_Probate_Pages.Visible = false;
                            txt_Plat_Pages.Visible = false;
                            txt_Total_Cost.Visible = false;
                            lbl_Total.Visible = false;

                        }
                    }
                    //lbl_Invoice_Number.Text = Inv_Num.ToString();
                }


            
        }

        public void load_order_masters()
        {

            dbc.BindClientName(ddl_ClientName);
      
            dbc.BindOrderType(ddl_ordertype);

            dbc.BindState(ddl_State);
            dbc.BindProduction_Unit_Type(ddl_Production_Unit);
            dbc.Bind_Employee_Order_source(ddl_Inhouse_Order_Source);
            dbc.Bind_Invoice_Ordered_For(ddl_Inhouse_Ordered_For);
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
                dbc.BindSubProcessName(ddl_SubProcess, client_Id);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

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
                    txt_Date.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();

                    ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                    Inhouse_Order_Type_Id = int.Parse(dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString());
                    ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                    if (ddl_ClientName.SelectedIndex > 0)
                    {

                        client_Id = int.Parse(ddl_ClientName.SelectedValue.ToString());
                        Inhouse_Client_Id = client_Id;
                        dbc.BindSubProcessName(ddl_SubProcess, client_Id);
                    }

                    ddl_SubProcess.SelectedValue = dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString();

                    Subprocess_id = int.Parse(ddl_SubProcess.SelectedValue.ToString());


                    Subprocess_id = int.Parse(ddl_SubProcess.SelectedValue.ToString());

                    Inhouse_SubClient_Id = Subprocess_id;
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
                        Inhouse_State_Id = stateid;
                        dbc.BindCounty(ddl_County, stateid);

                    }
                    if (dt_Select_Order_Details.Rows[0]["CountyId"].ToString() != "0")
                    {
                        ddl_County.SelectedValue = dt_Select_Order_Details.Rows[0]["CountyId"].ToString();
                        Inhouse_County_Id = int.Parse(dt_Select_Order_Details.Rows[0]["CountyId"].ToString());
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
                    if (dt_Select_Order_Details.Rows[0]["External_Order_Id"] != "" && dt_Select_Order_Details.Rows[0]["External_Order_Id"] != null)
                    {
                        External_Order_Id = int.Parse(dt_Select_Order_Details.Rows[0]["External_Order_Id"].ToString());
                        External_Client_Id = int.Parse(dt_Select_Order_Details.Rows[0]["External_Client_Id"].ToString());
                        External_Sub_Client_Id = int.Parse(dt_Select_Order_Details.Rows[0]["External_Sub_Client_Id"].ToString());
                    }
                    else
                    {
                        txt_Invoice_Search_Cost.Text = "";
                        txt_No_Of_Pages.Text = "";
                        txt_Invoice_Copy_Cost.Text = "";
                        txt_Inhouse_Search_Cost.Text = "";
                        txt_Inhouse_Copy_Cost.Text = "";
                        txt_Inhouse_No_of_page.Text = "";
                        txt_Inhouse_Drn_Cost.Text = "";
                        txt_Inhouse_Db_Cost.Text = "";
                        ddl_Production_Unit.SelectedIndex = 1;

                    }


                }

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



        private void Load_Invoice_Details()
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT");
            ht.Add("@Order_ID", External_Order_Id);
            dt = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht);
            if (dt.Rows.Count > 0)
            {

                txt_Invoice_Search_Cost.Text = dt.Rows[0]["Search_Cost"].ToString();
                txt_Invoice_Copy_Cost.Text = dt.Rows[0]["Copy_Cost"].ToString();
                txt_No_Of_Pages.Text = dt.Rows[0]["No_Of_Pages"].ToString();
                 

                if (dt.Rows[0]["Invoice_Date"].ToString() != "")
                {
                    txt_Invoice_Date.Text = dt.Rows[0]["Invoice_Date"].ToString();
                }
                else
                {
                    txt_Invoice_Date.Text = "";
                }



                txt_Inhouse_Search_Cost.Text = dt.Rows[0]["Inhouse_Search_Cost"].ToString();
                txt_Inhouse_Copy_Cost.Text = dt.Rows[0]["Inhouse_Copy_Cost"].ToString();
                txt_Inhouse_No_of_page.Text = dt.Rows[0]["Inhouse_No_Pages"].ToString();
                txt_Inhouse_Drn_Cost.Text = dt.Rows[0]["DRN_Cost"].ToString();
                txt_Inhouse_Db_Cost.Text = dt.Rows[0]["DB_Cost"].ToString();

                txt_Plat_Pages.Text = dt.Rows[0]["Plat_Map_Pages"].ToString();
                txt_Probate_Pages.Text = dt.Rows[0]["Probate_Pages"].ToString();
                txt_Total_Cost.Text = dt.Rows[0]["Order_Cost"].ToString();

                string Production_Type = dt.Rows[0]["Production_Unit_Type"].ToString();
                if (Production_Type != "" && Production_Type != null)
                {
                    ddl_Production_Unit.SelectedValue = Production_Type.ToString();
                }
                else
                {

                    ddl_Production_Unit.SelectedIndex = 0;
                }

                
                string Inhouse_Ordered_For = dt.Rows[0]["Ordered_For"].ToString();
                if (Inhouse_Ordered_For != "" && Inhouse_Ordered_For != null)
                {
                    ddl_Inhouse_Ordered_For.SelectedValue= Inhouse_Ordered_For.ToString();
                }
                else {
                    ddl_Inhouse_Ordered_For.SelectedIndex = 0;
                }

                string Inhouse_Order_Source = dt.Rows[0]["Order_Source"].ToString();
                if (Inhouse_Order_Source != "" && Inhouse_Order_Source != null)
                {
                    ddl_Inhouse_Order_Source.SelectedValue = Inhouse_Order_Source.ToString();
                }
                else
                {
                    ddl_Inhouse_Order_Source.SelectedIndex = 0;
                }
            }
                else
            {
                        invoice_Search_Cost = 0;
                        txt_Invoice_Search_Cost.Text = "";
                        txt_No_Of_Pages.Text = "";
                        txt_Invoice_Copy_Cost.Text = "";
                        txt_Inhouse_Search_Cost.Text = "";
                        txt_Inhouse_Copy_Cost.Text = "";
                        txt_Inhouse_No_of_page.Text = "";
                        txt_Inhouse_Drn_Cost.Text = "";
                        txt_Inhouse_Db_Cost.Text = "";

                        ddl_Production_Unit.SelectedIndex = 1;

            }


            // This is commented for For Invoice Not Using by Database wise 
            //else


            //{

            //    // Check Client Order_Type_Wise Cost


            //    Hashtable htcheck = new Hashtable();
            //    DataTable dtcheck = new DataTable();

            //    htcheck.Add("@Trans", "GET_ORDER_COST_BY_CLIENT_ORDER_TYPE_WISE");
            //    htcheck.Add("@Client_Id", Inhouse_Client_Id);
            //    htcheck.Add("@Subprocess_ID", Inhouse_SubClient_Id);
            //    htcheck.Add("@Order_Type_Id", Inhouse_Order_Type_Id);
            //    dtcheck = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htcheck);
            //    if (dtcheck.Rows.Count > 0)
            //    {


            //        invoice_Search_Cost = Convert.ToDecimal(dtcheck.Rows[0]["Order_Cost"].ToString());
            //        txt_Invoice_Search_Cost.Text = invoice_Search_Cost.ToString();
            //        txt_Invoice_Copy_Cost.Text = "0";
            //        txt_No_Of_Pages.Text = "0";
            //        ddl_Production_Unit.SelectedIndex = 1;



            //    }
            //    else
            //    {




            //        // Check Client - sublclient state and county and order Type wise

            //        Hashtable htget_Order_cost = new Hashtable();
            //        DataTable dtget_Order_Cost = new DataTable();

            //        htget_Order_cost.Add("@Trans", "GET_CLIENT_ORDER_COST");
            //        htget_Order_cost.Add("@Client_Id", Inhouse_Client_Id);
            //        htget_Order_cost.Add("@Subprocess_ID", Inhouse_SubClient_Id);
            //        htget_Order_cost.Add("@state_Id", Inhouse_State_Id);
            //        htget_Order_cost.Add("@County_Id", Inhouse_County_Id);
            //        htget_Order_cost.Add("@Order_Type_Id", Inhouse_Order_Type_Id);

            //        dtget_Order_Cost = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htget_Order_cost);

            //        if (dtget_Order_Cost.Rows.Count > 0)
            //        {

            //            invoice_Search_Cost = Convert.ToDecimal(dtget_Order_Cost.Rows[0]["Order_Cost"].ToString());
            //            txt_Invoice_Search_Cost.Text = invoice_Search_Cost.ToString();
            //            txt_Invoice_Copy_Cost.Text = "0";
            //            txt_No_Of_Pages.Text = "0";
            //            ddl_Production_Unit.SelectedIndex = 1;
            //        }
            //        else
            //        {

            //            invoice_Search_Cost = 0;
            //            txt_Invoice_Search_Cost.Text = "";
            //            txt_No_Of_Pages.Text = "";
            //            txt_Invoice_Copy_Cost.Text = "";
            //            txt_Inhouse_Search_Cost.Text = "";
            //            txt_Inhouse_Copy_Cost.Text = "";
            //            txt_Inhouse_No_of_page.Text = "";
            //            ddl_Production_Unit.SelectedIndex = 1;

            //        }
            //    }
                
                
            
        }

        private void txt_Invoice_Order_Number_TextChanged(object sender, EventArgs e)
        {

            string Order_Number = txt_Invoice_Order_Number.Text;
            if (Order_Number != "")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "GET_ORDER_ID");
                ht.Add("@Client_Order_Number", Order_Number);
                dt = dataaccess.ExecuteSP("Sp_Order", ht);

                if (dt.Rows.Count > 0)
                {
                    Order_Id = int.Parse(dt.Rows[0]["Order_ID"].ToString());
                    if (dt.Rows[0]["Order_ID"].ToString() != "" && dt.Rows[0]["Order_ID"].ToString() != null)
                    {

                        External_Order_Id = int.Parse(dt.Rows[0]["Order_ID"].ToString());
                    }
                    else
                    {

                        External_Order_Id = 0;
                    }

                    if (External_Order_Id != 0)
                    {
                        load_order_masters();

                        Order_Load();

                        if (User_Role_Id == "1")
                        {

                        }
                        else
                        {


                        }

                    }
                    else
                    {
                        txt_Invoice_Search_Cost.Text = "";
                        txt_No_Of_Pages.Text = "";
                        txt_Invoice_Copy_Cost.Text = "";
                        txt_Inhouse_Search_Cost.Text = "";
                        txt_Inhouse_Copy_Cost.Text = "";
                        txt_Inhouse_No_of_page.Text = "";
                        txt_Inhouse_Drn_Cost.Text = "";
                        txt_Inhouse_Db_Cost.Text = "";

                        ddl_Production_Unit.SelectedIndex = 1;
                        MessageBox.Show("This Order Not Belongs to Titlelogy");
                    }


                }



            }
        }

        private void Titlelogy_Invoice_Entry_Load(object sender, EventArgs e)
        {
            if (Order_Id != 0)
            {
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

                Load_Invoice_Details();

                
            }
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
            return true;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Validate_Invoice() != false)
            {
                Order_Invoice_Entry();
            }
        }


        private void Order_Invoice_Entry()
        {

            Hashtable ht_max = new Hashtable();
            DataTable dt_max = new DataTable();
            ht_max.Add("@Trans", "GET_MAX_EXTERNAL_INVOICE_AUTO_NUMBER");
            ht_max.Add("@Client_Id", External_Client_Id);
            dt_max = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_max);

            if (dt_max.Rows.Count > 0)
            {
                Autoinvoice_No = int.Parse(dt_max.Rows[0]["Invoice_Auto_No"].ToString());
            }

            Hashtable htmax_Invoice_No = new Hashtable();
            DataTable dtmax_invoice_No = new DataTable();
            htmax_Invoice_No.Add("@Trans", "GET_MAX_EXTERNAL_INVOICE_NUMBER");
            htmax_Invoice_No.Add("@Client_Id", External_Client_Id);
            dtmax_invoice_No = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htmax_Invoice_No);

            if (dtmax_invoice_No.Rows.Count > 0)
            {
                Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
            }



            if (txt_Invoice_Search_Cost.Text != "")
            {
                invoice_Search_Cost = Convert.ToDecimal(txt_Invoice_Search_Cost.Text.ToString());
            }
            else { invoice_Search_Cost = 0; }

            if (txt_Invoice_Copy_Cost.Text != "")
            {
                Invoice_Copy_Cost = Convert.ToDecimal((txt_Invoice_Copy_Cost.Text.ToString()));
            }
            else { Invoice_Copy_Cost = 0; }

            if (txt_No_Of_Pages.Text != "")
            {
                No_Of_Pages = int.Parse(txt_No_Of_Pages.Text.ToString());
            }
            else { No_Of_Pages = 0; }

            


            
         

            if (txt_Inhouse_Search_Cost.Text != "")
            {
                Inhouse_Search_Cost = Convert.ToDecimal(txt_Inhouse_Search_Cost.Text.ToString());
            }
            else { Inhouse_Search_Cost = 0; }

            if (txt_Inhouse_Copy_Cost.Text != "")
            {
                Inhouse_Copy_Cost = Convert.ToDecimal((txt_Inhouse_Copy_Cost.Text.ToString()));
            }
            else { Inhouse_Copy_Cost = 0; }

            if (txt_Inhouse_No_of_page.Text != "")
            {
                Inhouse_No_Of_Pages = int.Parse(txt_Inhouse_No_of_page.Text.ToString());
            } else { 
                Inhouse_No_Of_Pages = 0; 
            }
            if (txt_Inhouse_Drn_Cost.Text != "")
            {
                Inhouse_Drn_cost = Convert.ToDecimal(txt_Inhouse_Drn_Cost.Text);
            }
            else {
                Inhouse_Drn_cost = 0;
            }
            if (txt_Inhouse_Db_Cost.Text != "")
            {
                Inhouse_Db_Cost = Convert.ToDecimal(txt_Inhouse_Db_Cost.Text);
            }
            else
            {
                Inhouse_Db_Cost = 0;
            }

            Hashtable ht_check = new Hashtable();
            DataTable dt_check = new DataTable();


            if (client_Id == 33 && Subprocess_id == 300)
            {


                if (Order_Type_Id == 116 || Order_Type_Id == 113 || Order_Type_Id == 117 || Order_Type_Id == 7)
                {

                    // this is for DB title-Peak Title Order Type Wise Amount 


                    



                    if (txt_No_Of_Pages.Text != "")
                    {
                        Title_Peak_Inv_No_Of_Pages = int.Parse(txt_No_Of_Pages.Text);

                    }
                    else
                    {
                        Title_Peak_Inv_No_Of_Pages = 0;


                    }

                    if (txt_Plat_Pages.Text != "")
                    {

                        Title_Peak_Inv_No_Plat_Map_Pages = int.Parse(txt_Plat_Pages.Text);
                    }
                    else
                    {
                        Title_Peak_Inv_No_Plat_Map_Pages = 0;

                    }

                    if (txt_Probate_Pages.Text != "")
                    {

                        Title_Peak_Inv_No_Probate_Pages = int.Parse(txt_Probate_Pages.Text);
                    }
                    else
                    {
                        Title_Peak_Inv_No_Probate_Pages = 0;

                    }

                    // this is the Requiremnet above page 15 means *** No. of copies: 46 (46-15 = 31 copies will be billed).  Per copy cost $0.50 *31 =  $15.50 ***
                    if (Title_Peak_Inv_No_Of_Pages > 15)
                    {

                        Invoice_Copy_Cost = Convert.ToDecimal((Title_Peak_Inv_No_Of_Pages - 15) * (0.50));


                    }
                    else
                    {

                        Invoice_Copy_Cost = 0;
                    }


                    // Plat map And Probate Price Are 1$

                    Title_Logy_Probate_Cost = Title_Peak_Inv_No_Probate_Pages * 1;

                    Title_Logy_Platmap_Cost = Title_Peak_Inv_No_Plat_Map_Pages * 1;




                    // Total Order Cost= As per the Require Ment ----Search Cost + Copycost + sum(Probate+Plat Map) = $187.50


                    Total_Titlelogy_Order_Cost = (invoice_Search_Cost + Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost);

                    Invoice_Copy_Cost = Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost;

                    Invoice_Order_Cost = Total_Titlelogy_Order_Cost;


                }
                else
                {
                    Title_Logy_Probate_Cost = 0;
                    Title_Logy_Platmap_Cost = 0;
                    Title_Peak_Inv_No_Plat_Map_Pages = 0;
                    Title_Peak_Inv_No_Probate_Pages = 0;
                }

            }


            ht_check.Add("@Trans", "CHECK");
            ht_check.Add("@Order_ID", External_Order_Id);
            dt_check = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_check);
            int check = int.Parse(dt_check.Rows[0]["Count"].ToString());
            if (check == 0)
            {
                Hashtable ht_insert = new Hashtable();
                DataTable dt_insert = new DataTable();

               

                ht_insert.Add("@Trans", "INSERT");
                ht_insert.Add("@Client_Id", External_Client_Id);
                ht_insert.Add("@Order_ID", External_Order_Id);
                ht_insert.Add("@Subprocess_ID",External_Sub_Client_Id);
                ht_insert.Add("@Invoice_Auto_No", Autoinvoice_No);
                ht_insert.Add("@Invoice_No", Invoice_Number);
                ht_insert.Add("@Order_Cost", Invoice_Order_Cost);
                ht_insert.Add("@Search_Cost", invoice_Search_Cost);
                ht_insert.Add("@Copy_Cost", Invoice_Copy_Cost);
                ht_insert.Add("@No_Of_Pages", No_Of_Pages);
                ht_insert.Add("@Invoice_Date", txt_Invoice_Date.Text);
                ht_insert.Add("@Inhouse_Search_Cost", Inhouse_Search_Cost);
                ht_insert.Add("@Inhouse_Copy_Cost", Inhouse_Copy_Cost);
                ht_insert.Add("@Production_Unit_Type", int.Parse(ddl_Production_Unit.SelectedValue.ToString()));
                ht_insert.Add("@Inhouse_No_Pages", Inhouse_No_Of_Pages);
                ht_insert.Add("@Probate_Pages", Title_Peak_Inv_No_Probate_Pages);
                ht_insert.Add("@Plat_Map_Pages", Title_Peak_Inv_No_Plat_Map_Pages);
                ht_insert.Add("@Probate_Cost", Title_Logy_Probate_Cost);
                ht_insert.Add("@Plat_Map_Cost", Title_Logy_Platmap_Cost);
                ht_insert.Add("@Ordered_For", int.Parse(ddl_Inhouse_Ordered_For.SelectedValue.ToString()));
                ht_insert.Add("@Order_Source", int.Parse(ddl_Inhouse_Order_Source.SelectedValue.ToString()));
                ht_insert.Add("@DRN_Cost", Inhouse_Drn_cost);
                ht_insert.Add("@DB_Cost", Inhouse_Db_Cost);

                ht_insert.Add("@Status", "True");

                ht_insert.Add("@Inserted_By", userid);

                dt_insert = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_insert);
                MessageBox.Show("Invoice Genrated Sucessfully");
            }
            else
            {

                Hashtable ht_Update = new Hashtable();
                DataTable dt_Update = new DataTable();

                ht_Update.Add("@Trans", "UPDATE");
                ht_Update.Add("@Client_Id", External_Client_Id);
                ht_Update.Add("@Order_ID", External_Order_Id);
                ht_Update.Add("@Subprocess_ID", External_Sub_Client_Id);
                ht_Update.Add("@Invoice_Auto_No", Autoinvoice_No);
                ht_Update.Add("@Invoice_No", Invoice_Number);
                ht_Update.Add("@Order_Cost", Invoice_Order_Cost);
                ht_Update.Add("@Search_Cost", invoice_Search_Cost);
                ht_Update.Add("@Copy_Cost", Invoice_Copy_Cost);
                ht_Update.Add("@No_Of_Pages", No_Of_Pages);
                ht_Update.Add("@Invoice_Date", txt_Invoice_Date.Text);
                ht_Update.Add("@Inhouse_Search_Cost", Inhouse_Search_Cost);
                ht_Update.Add("@Inhouse_Copy_Cost", Inhouse_Copy_Cost);
                ht_Update.Add("@Production_Unit_Type", int.Parse(ddl_Production_Unit.SelectedValue.ToString()));
                ht_Update.Add("@Inhouse_No_Pages", Inhouse_No_Of_Pages);
                ht_Update.Add("@Ordered_For", int.Parse(ddl_Inhouse_Ordered_For.SelectedValue.ToString()));
                ht_Update.Add("@Order_Source", int.Parse(ddl_Inhouse_Order_Source.SelectedValue.ToString()));
                ht_Update.Add("@DRN_Cost", Inhouse_Drn_cost);
                ht_Update.Add("@DB_Cost", Inhouse_Db_Cost);
                ht_Update.Add("@Status", "True");
                ht_Update.Add("@Modified_By", userid);

                dt_Update = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_Update);

                // updateing Invoice Page details for Peak title client 320007
                if (client_Id == 33 && Subprocess_id == 300)
                {


                    if (Order_Type_Id == 116 || Order_Type_Id == 113 || Order_Type_Id == 117 || Order_Type_Id == 7)
                    {
                        Hashtable ht_Update1 = new Hashtable();
                        DataTable dt_Update1 = new DataTable();

                        ht_Update1.Add("@Trans", "UPDATE_INVOICE_PAGE_COST_DETAILS");
                        ht_Update1.Add("@Order_ID", External_Order_Id);
                        ht_Update1.Add("@No_Of_Pages", No_Of_Pages);
                        ht_Update1.Add("@Probate_Pages", Title_Peak_Inv_No_Probate_Pages);
                        ht_Update1.Add("@Plat_Map_Pages", Title_Peak_Inv_No_Plat_Map_Pages);
                        ht_Update1.Add("@Probate_Cost", Title_Logy_Probate_Cost);
                        ht_Update1.Add("@Plat_Map_Cost", Title_Logy_Platmap_Cost);
                        dt_Update1 = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_Update1);

                    }
                }

                MessageBox.Show("Invoice Updated Sucessfully");
                
            }
        }

        private void btn_Invoice_Preview_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.InvoiceRep.Invoice_Order_Preview OrderEntry = new Ordermanagement_01.InvoiceRep.Invoice_Order_Preview(Order_Id, client_Id, External_Order_Id,User_Role_Id,0);
            OrderEntry.Show();
        }

        private void btn_upload_Documents_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.SelectedValue.ToString(), ddl_SubProcess.SelectedValue.ToString());
            Orderuploads.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_Invoice_Search_Cost.Text = "";
            txt_No_Of_Pages.Text = "";
            txt_Invoice_Copy_Cost.Text = "";
            txt_Inhouse_Search_Cost.Text = "";
            txt_Inhouse_Copy_Cost.Text = "";
            txt_Inhouse_No_of_page.Text = "";
            txt_Inhouse_Drn_Cost.Text = "";
            txt_Inhouse_Db_Cost.Text = "";

            ddl_Production_Unit.SelectedIndex = 1;
            this.Close();

        }

        private void txt_Invoice_Search_Cost_TextChanged(object sender, EventArgs e)
        {
            Calculate_Total_Inovice_Cost_For_DB_Title_peak_Title_Client();
        }

        private void txt_Invoice_Search_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Invoice_Search_Cost.Text))
            {
                txt_Invoice_Search_Cost.Text = "";
            }

        }

        private void txt_Invoice_Copy_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Invoice_Copy_Cost.Text))
            {
                 txt_Invoice_Copy_Cost.Text = "";
            }
        }

        private void txt_No_Of_Pages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
                e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

      

        private void txt_Probate_Pages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
              e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

       
        private void txt_Plat_Pages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
                e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Total_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Total_Cost.Text))
            {
               txt_Total_Cost.Text = "";
            }
        }




        private void Calculate_Total_Inovice_Cost_For_DB_Title_peak_Title_Client()
        
        {
            if (client_Id == 33 && Subprocess_id == 300)
            {

                if ( Order_Type_Id == 113 || Order_Type_Id == 115 || Order_Type_Id == 117)
                {

                    if (txt_Invoice_Search_Cost.Text != "")
                    {
                        invoice_Search_Cost = Convert.ToDecimal(txt_Invoice_Search_Cost.Text);
                    }
                    else
                    {
                        invoice_Search_Cost = 0;
                    }

                    if (txt_No_Of_Pages.Text != "")
                    {
                        No_Of_Pages = int.Parse(txt_No_Of_Pages.Text);
                    }
                    else
                    {
                        No_Of_Pages = 0;
                    }

                    if (No_Of_Pages > 15)
                    {
                        Invoice_Copy_Cost = Convert.ToDecimal((No_Of_Pages - 15) * (0.50));
                    }
                    else
                    {
                        Invoice_Copy_Cost = 0;
                    }

                    if (txt_Plat_Pages.Text != "")
                    {

                        Title_Peak_Inv_No_Plat_Map_Pages = int.Parse(txt_Plat_Pages.Text);
                    }
                    else
                    {
                        Title_Peak_Inv_No_Plat_Map_Pages = 0;

                    }

                    if (txt_Probate_Pages.Text != "")
                    {

                        Title_Peak_Inv_No_Probate_Pages = int.Parse(txt_Probate_Pages.Text);
                    }
                    else
                    {
                        Title_Peak_Inv_No_Probate_Pages = 0;
                    }


                    Title_Logy_Probate_Cost = Title_Peak_Inv_No_Probate_Pages * 1;

                    Title_Logy_Platmap_Cost = Title_Peak_Inv_No_Plat_Map_Pages * 1;




                    // Total Order Cost= As per the Require Ment ----Search Cost + Copycost + sum(Probate+Plat Map) = $187.50
                 

                    Total_Titlelogy_Order_Cost = (invoice_Search_Cost + Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost);

                    Invoice_Copy_Cost = Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost;

                    txt_Invoice_Copy_Cost.Text = Invoice_Copy_Cost.ToString();

                    txt_Total_Cost.Text = Total_Titlelogy_Order_Cost.ToString();


                }
            }
        

        }

        private void txt_No_Of_Pages_TextChanged(object sender, EventArgs e)
        {
            if (txt_No_Of_Pages.Text != "")
            {
                Calculate_Total_Inovice_Cost_For_DB_Title_peak_Title_Client();
            }
        }

        private void txt_Probate_Pages_TextChanged(object sender, EventArgs e)
        {
            if (txt_Probate_Pages.Text != "")
            {
                Calculate_Total_Inovice_Cost_For_DB_Title_peak_Title_Client();
            }
        }

        private void txt_Plat_Pages_TextChanged(object sender, EventArgs e)
        {
            if (txt_Plat_Pages.Text != "")
            {
                Calculate_Total_Inovice_Cost_For_DB_Title_peak_Title_Client();
            }
        }

        private void txt_Inhouse_Search_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Inhouse_Search_Cost.Text))
            {
                txt_Inhouse_Search_Cost.Text = "";
            }
        }

        private void txt_Inhouse_Copy_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Inhouse_Copy_Cost.Text))
            {
                txt_Inhouse_Copy_Cost.Text = "";
            }

        }

        private void txt_Inhouse_No_of_page_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Inhouse_No_of_page_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
              e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Invoice_Copy_Cost_TextChanged(object sender, EventArgs e)
        {
            Calculate_Total_Inovice_Cost_For_DB_Title_peak_Title_Client();
        }

       
       
    
    }
}
