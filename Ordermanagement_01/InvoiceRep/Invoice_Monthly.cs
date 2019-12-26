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
using System.Globalization;
using System.Text.RegularExpressions;
using Ordermanagement_01.Invoice;
namespace Ordermanagement_01.InvoiceRep
{
    public partial class Invoice_Monthly : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Client_Id, Sub_Process_ID;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int user_id;
        int Autoinvoice_No;
        
        int Monthly_Invoice_Id;
        string Invoice_Comments;
        string Invoice_Number,Invoice_Month;
        int Invoice_Id;
        string Invoice_Date;
        decimal total_amount_to_pay;
        decimal Old_Total_Amount, Old_Paid_Amount, balance_amount_paid;
        decimal Total_Invoice_Balance;
        string User_Role;
        public Invoice_Monthly(int USER_ID, string OPERATION, string INV_NUM,int INVOICE_ID,int CLIENT_ID,int SUB_PROCESS_ID,string INVOICE_DATE,string INVOICE_COMMENTS,string INVOICE_MONTH,string USER_ROLE)
        {
            user_id = USER_ID;
            Invoice_Number = INV_NUM.ToString();
            Invoice_Id = INVOICE_ID;
            Client_Id = CLIENT_ID;
            Sub_Process_ID = SUB_PROCESS_ID;
            Invoice_Date = INVOICE_DATE.ToString();
            Invoice_Comments = INVOICE_COMMENTS.ToString();
            Invoice_Month = INVOICE_MONTH.ToString();
            User_Role = USER_ROLE;
            InitializeComponent();
        }

      
        private void Invoice_Monthly_Load(object sender, EventArgs e)
        {
            if (Invoice_Id == 0)
            {

                if (User_Role == "1")
                {
                    dbc.BindClientName(ddl_Client_Name);
                }
                else 
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Name);
                }
                txt_From_date.Text = DateTime.Now.ToString();
                txt_To_date.Text = DateTime.Now.ToString();
                txt_Invoice_Date.Text = DateTime.Now.ToString();
                btn_Save.Text = "Genrate Invoice";
                dbc.BindPayment_Status(ddl_Payment_Status);
                Control_Enable_True();
                txt_Old_balance.Text = "0.00";
                txt_Invoice_Paying_Amount.Text = "0.00";
                ddl_Payment_Status.SelectedValue= "1";
               
            }
            else if (Invoice_Id != 0)
            {
                if (User_Role == "1")
                {
                    dbc.BindClientName(ddl_Client_Name);
                }
                else
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Name);
                }
              
                ddl_Client_Name.SelectedValue = Client_Id;
                ddl_Client_Name_SelectedIndexChanged( sender,  e);
                dbc.BindPayment_Status(ddl_Payment_Status);
                ddl_Client_SubProcess.SelectedValue =Sub_Process_ID;
                txt_From_date.Text = DateTime.Now.ToString();
                txt_To_date.Text = DateTime.Now.ToString();
                txt_Invoice_Date.Text = DateTime.Now.ToString();
                btn_Save.Text = "Edit Invoice";
                
                lbl_Invoice_Number.Text = Invoice_Number.ToString();
                lbl_Inv_Month.Text = Invoice_Month.ToString();
                //txt_Invoice_Date.Text = Invoice_Date.ToString();
                //txt_Invoice_comments.Text = Invoice_Comments.ToString();
                Control_EnableFalse();
                Load_Invoice_Entred_Order_Details();


            }

        }
        public void Control_EnableFalse()
        {
            ddl_Client_Name.Enabled = false;
            ddl_Client_SubProcess.Enabled = false;
            lbl_From.Visible = false;
            lbl_To.Visible = false;
            txt_From_date.Visible = false;
            txt_To_date.Visible = false;
            btn_Get.Visible = false;

            lbl_Inv_Header.Visible = true;
            lbl_Invoice_Number.Visible = true;



        }
        public void Control_Enable_True()
        {

            ddl_Client_Name.Enabled = true;
            ddl_Client_SubProcess.Enabled = true;
            lbl_From.Visible = true;
            lbl_To.Visible = true;
            txt_From_date.Visible = true;
            txt_To_date.Visible = true;
            btn_Get.Visible = true;
            lbl_Inv_Header.Visible = false;
            lbl_Invoice_Number.Visible = false;
        }


        private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Client_Name.SelectedIndex > 0)
            {
                if (User_Role == "1")
                {
                    dbc.BindSubProcess_ForEntry(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                }
                else 
                
                {

                    dbc.BindSubProcessNo_rpt(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                }
            }
        }

        public void Load_Old_Balance_Amount()
            {
             
               
                decimal totoldnew;
             
                if (txt_Old_balance.Text != "" && txt_Total.Text != "0.00")
                {
                    totoldnew = Convert.ToDecimal(txt_Total.Text) + Convert.ToDecimal(txt_Old_balance.Text);

                    txt_Total_Amount_To_Pay.Text = totoldnew.ToString();

                }
                else 
                {

                    totoldnew = Convert.ToDecimal(txt_Total.Text);
                    txt_Old_balance.Text = "0.00";
                    txt_Total_Amount_To_Pay.Text = totoldnew.ToString();
                }
            


        }



        public void Load_Invoice_Entred_Order_Details()
        {
          
            
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_ENTERED_ORDER_INVOICE_COST_DETAILS");
            htsearch.Add("@MonthlyInvoice_Id", Invoice_Id);
          
            dtsearch = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsearch);

            Grid_Invoice_Details.EnableHeadersVisualStyles = false;
            Grid_Invoice_Details.Columns[0].Width = 40;
            Grid_Invoice_Details.Columns[1].Width = 120;
            Grid_Invoice_Details.Columns[2].Width = 120;
            Grid_Invoice_Details.Columns[3].Width = 100;
            Grid_Invoice_Details.Columns[4].Width = 126;
            Grid_Invoice_Details.Columns[5].Width = 132;


            if (dtsearch.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Invoice_Details.Rows.Clear();
                for (int i = 0; i < dtsearch.Rows.Count; i++)
                {
                    Grid_Invoice_Details.Rows.Add();
                    Grid_Invoice_Details.Rows[i].Cells[0].Value = i + 1;
                    Grid_Invoice_Details.Rows[i].Cells[1].Value = dtsearch.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Search_Cost"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[3].Value = dtsearch.Rows[i]["Copy_Cost"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[4].Value = dtsearch.Rows[i]["Total"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[5].Value = dtsearch.Rows[i]["Invoice_Date"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[6].Value = dtsearch.Rows[i]["Order_ID"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[7].Value = dtsearch.Rows[i]["Order_Invoice_No"].ToString();
                    
                }
                // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
            }
            else
            {
                Grid_Invoice_Details.Rows.Clear();
                Grid_Invoice_Details.DataSource = null;
                // lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }


            //Load Summary of Order Invoice Details

            Hashtable htsummary = new Hashtable();
            DataTable dtsummary = new DataTable();
            htsummary.Add("@Trans", "GET_ENTERED_SUMMARY_OF_INVOICE_ORDER_COST_DETAILS");
            htsummary.Add("@MonthlyInvoice_Id", Invoice_Id);
            
            dtsummary = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary);

            if (dtsummary.Rows.Count > 0)
            {

                txt_No_Of_orders.Text = dtsummary.Rows[0]["No_Of_Orders"].ToString();
                txt_Search_Cost.Text = dtsummary.Rows[0]["Search_Cost"].ToString();
                txt_Copy_Cost.Text = dtsummary.Rows[0]["Copy_Cost"].ToString();
                txt_Total.Text = dtsummary.Rows[0]["Total"].ToString();


            }
            else
            {

                txt_No_Of_orders.Text = "0";
                txt_Search_Cost.Text = "0.00";
                txt_Copy_Cost.Text = "0.00";
                txt_Total.Text = "0.00";

            }

            //Summary of Invoice on Order Type Wise
            Hashtable htsummary1 = new Hashtable();
            DataTable dtsummary1 = new DataTable();
            htsummary1.Add("@Trans", "GET_ENTRED_SUMMARY_ORDER_TYPE_WISE_COST");
            htsummary1.Add("@MonthlyInvoice_Id", Invoice_Id);
          
            dtsummary1 = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary1);
            Grid_Invoice_Summary.EnableHeadersVisualStyles = false;
            Grid_Invoice_Summary.Columns[0].Width = 36;
            Grid_Invoice_Summary.Columns[1].Width = 120;
            Grid_Invoice_Summary.Columns[2].Width = 120;
            Grid_Invoice_Summary.Columns[3].Width = 100;
            Grid_Invoice_Summary.Columns[4].Width = 126;



            if (dtsearch.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Invoice_Summary.Rows.Clear();
                for (int i = 0; i < dtsummary1.Rows.Count; i++)
                {
                    Grid_Invoice_Summary.Rows.Add();
                    Grid_Invoice_Summary.Rows[i].Cells[0].Value = i + 1;
                    Grid_Invoice_Summary.Rows[i].Cells[1].Value = dtsummary1.Rows[i]["Order_Type"].ToString();
                    Grid_Invoice_Summary.Rows[i].Cells[2].Value = dtsummary1.Rows[i]["No_Of_Orders"].ToString();
                    Grid_Invoice_Summary.Rows[i].Cells[3].Value = dtsummary1.Rows[i]["Total"].ToString();
                    Grid_Invoice_Summary.Rows[i].Cells[4].Value = dtsummary1.Rows[i]["Total"].ToString();
                    Grid_Invoice_Summary.Rows[i].Cells[5].Value = dtsummary1.Rows[i]["Order_Type_ID"].ToString();

                }
                // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
            }
            else
            {
                Grid_Invoice_Summary.Rows.Clear();
                Grid_Invoice_Summary.DataSource = null;
                // lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }


            Hashtable htsummary2 = new Hashtable();
            DataTable dtsummary2 = new DataTable();
            htsummary2.Add("@Trans", "GET_ENTERED_OLD_BALANCE");
            htsummary2.Add("@Subprocess_ID", Sub_Process_ID);
            dtsummary2 = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary2);
            Grid_Old_Balance.EnableHeadersVisualStyles = false;
            Grid_Old_Balance.Columns[0].Width = 36;
            Grid_Old_Balance.Columns[1].Width = 36;
            Grid_Old_Balance.Columns[2].Width = 120;
            Grid_Old_Balance.Columns[3].Width = 120;
            Grid_Old_Balance.Columns[4].Width = 100;
            Grid_Old_Balance.Columns[5].Width = 126;



            if (dtsearch.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Old_Balance.Rows.Clear();
                for (int i = 0; i < dtsummary2.Rows.Count; i++)
                {
                    Grid_Old_Balance.Rows.Add();
                    Grid_Old_Balance.Rows[i].Cells[1].Value = i + 1;
                    Grid_Old_Balance.Rows[i].Cells[2].Value = dtsummary2.Rows[i]["Invoice_No"].ToString();
                    Grid_Old_Balance.Rows[i].Cells[3].Value = dtsummary2.Rows[i]["Month_Name"].ToString();
                    Grid_Old_Balance.Rows[i].Cells[4].Value = dtsummary2.Rows[i]["Total_Invoice_Amount"].ToString();
                    Grid_Old_Balance.Rows[i].Cells[5].Value = dtsummary2.Rows[i]["Total_Inv_Paid_Amount"].ToString();
                    Grid_Old_Balance.Rows[i].Cells[6].Value = dtsummary2.Rows[i]["Balance_Amount"].ToString();
                    Grid_Old_Balance.Rows[i].Cells[7].Value = dtsummary2.Rows[i]["MonthlyInvoice_Id"].ToString();
                    Grid_Old_Balance.Rows[i].Cells[8].Value = dtsummary2.Rows[i]["Invoice_Date"].ToString();
                    Grid_Old_Balance.Rows[i].Cells[9].Value = dtsummary2.Rows[i]["Comments"].ToString();
                }
                // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
            }
            else
            {
                Grid_Old_Balance.Rows.Clear();
                Grid_Old_Balance.DataSource = null;
                // lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }


           


   
            decimal totoldnew;
          
            if (txt_Old_balance.Text != "" && txt_Total.Text != "0.00")
            {
                totoldnew = Convert.ToDecimal(txt_Total.Text);

                txt_Total_Amount_To_Pay.Text = totoldnew.ToString();

            }
            else
            {

                totoldnew = Convert.ToDecimal(txt_Total.Text);
                txt_Old_balance.Text = "0.00";
                txt_Total_Amount_To_Pay.Text = totoldnew.ToString();
            }


            //Get _Invoice_Details=====================================

            Hashtable htinv = new Hashtable();
            DataTable dtinv = new DataTable();
            htinv.Add("@Trans", "SELECT_INVOICE_DETAILS");
            htinv.Add("@MonthlyInvoice_Id", Invoice_Id);
            dtinv = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htinv);
            if (dtinv.Rows.Count > 0)
            {

                txt_Invoice_Paying_Amount.Text = dtinv.Rows[0]["Inv_Paying_AMount"].ToString();
                //txt_Invoice_Paying_Amount.Enabled = false;
                txt_Invoice_Date.Enabled = false;
                //txt_Invoice_Paying_Amount.ReadOnly = true;
                //ddl_Payment_Status.Enabled = false;

                ddl_Payment_Status.SelectedValue = dtinv.Rows[0]["Invoice_Paid_Status"].ToString();
                txt_Invoice_comments.Text = dtinv.Rows[0]["Comments"].ToString();
                txt_Invoice_Date.Text = dtinv.Rows[0]["Invoice_Date"].ToString();
            

            }

        }

       

        private void btn_Get_Click(object sender, EventArgs e)
        {

            //Load All Order Details
            Sub_Process_ID = int.Parse(ddl_Client_SubProcess.SelectedValue.ToString());
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            if (txt_From_date.Text != "" && txt_To_date.Text != "")
            {
                DateTime fromdate = Convert.ToDateTime(txt_From_date.Text, usDtfi);
                DateTime Todate = Convert.ToDateTime(txt_To_date.Text, usDtfi);
                Hashtable htsearch = new Hashtable();
                DataTable dtsearch = new DataTable();
                htsearch.Add("@Trans", "GET_CLIENT_WISE_ORDER_COST_DETAILS");
                htsearch.Add("@Subprocess_ID", Sub_Process_ID);
                htsearch.Add("@From_Date", fromdate);
                htsearch.Add("@To_Date", Todate);
                dtsearch = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsearch);

                Grid_Invoice_Details.EnableHeadersVisualStyles = false;
                Grid_Invoice_Details.Columns[0].Width = 40;
                Grid_Invoice_Details.Columns[1].Width = 120;
                Grid_Invoice_Details.Columns[2].Width = 120;
                Grid_Invoice_Details.Columns[3].Width = 100;
                Grid_Invoice_Details.Columns[4].Width = 126;
                Grid_Invoice_Details.Columns[5].Width = 132;
                Grid_Invoice_Details.Columns[6].Width = 100;
                if (dtsearch.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Invoice_Details.Rows.Clear();
                    for (int i = 0; i < dtsearch.Rows.Count; i++)
                    {
                        Grid_Invoice_Details.Rows.Add();
                        Grid_Invoice_Details.Rows[i].Cells[0].Value = i + 1;
                        Grid_Invoice_Details.Rows[i].Cells[1].Value = dtsearch.Rows[i]["Client_Order_Number"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Search_Cost"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[3].Value = dtsearch.Rows[i]["Copy_Cost"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[4].Value = dtsearch.Rows[i]["Total"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[5].Value = dtsearch.Rows[i]["Invoice_Date"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[6].Value = dtsearch.Rows[i]["Order_ID"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[7].Value = dtsearch.Rows[i]["Order_Invoice_No"].ToString();
                    }
                    // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
                }
                else
                {
                    Grid_Invoice_Details.Rows.Clear();
                    Grid_Invoice_Details.DataSource = null;
                    // lbl_Total_Orders.Text = "0";
                    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                    //grd_Admin_orders.DataBind();
                }


                //Load Summary of Order Invoice Details

                Hashtable htsummary = new Hashtable();
                DataTable dtsummary = new DataTable();
                htsummary.Add("@Trans", "GET_SUMMARY_OF_INVOICE_CLIENT_WISE");
                htsummary.Add("@Subprocess_ID", Sub_Process_ID);
                htsummary.Add("@From_Date", fromdate);
                htsummary.Add("@To_Date", Todate);
                dtsummary = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary);

                if (dtsummary.Rows.Count > 0)
                {

                    txt_No_Of_orders.Text = dtsummary.Rows[0]["No_Of_Orders"].ToString();
                    txt_Search_Cost.Text = dtsummary.Rows[0]["Search_Cost"].ToString();
                    txt_Copy_Cost.Text = dtsummary.Rows[0]["Copy_Cost"].ToString();
                    txt_Total.Text = dtsummary.Rows[0]["Total"].ToString();


                }
                else
                {

                    txt_No_Of_orders.Text = "0";
                    txt_Search_Cost.Text = "0.00";
                    txt_Copy_Cost.Text = "0.00";
                    txt_Total.Text = "0.00";

                }

                //Summary of Invoice on Order Type Wise
                Hashtable htsummary1 = new Hashtable();
                DataTable dtsummary1 = new DataTable();
                htsummary1.Add("@Trans", "GET_SUMMARY_OF_INVOICE__ORDER_TYPE_CLIENT_WISE");
                htsummary1.Add("@Subprocess_ID", Sub_Process_ID);
                htsummary1.Add("@From_Date", fromdate);
                htsummary1.Add("@To_Date", Todate);
                dtsummary1 = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary1);
                Grid_Invoice_Summary.EnableHeadersVisualStyles = false;
                Grid_Invoice_Summary.Columns[0].Width = 36;
                Grid_Invoice_Summary.Columns[1].Width = 120;
                Grid_Invoice_Summary.Columns[2].Width = 120;
                Grid_Invoice_Summary.Columns[3].Width = 100;
                Grid_Invoice_Summary.Columns[4].Width = 126;



                if (dtsearch.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Invoice_Summary.Rows.Clear();
                    for (int i = 0; i < dtsummary1.Rows.Count; i++)
                    {
                        Grid_Invoice_Summary.Rows.Add();
                        Grid_Invoice_Summary.Rows[i].Cells[0].Value = i + 1;
                        Grid_Invoice_Summary.Rows[i].Cells[1].Value = dtsummary1.Rows[i]["Order_Type"].ToString();
                        Grid_Invoice_Summary.Rows[i].Cells[2].Value = dtsummary1.Rows[i]["No_Of_Orders"].ToString();
                        Grid_Invoice_Summary.Rows[i].Cells[3].Value = dtsummary1.Rows[i]["Total"].ToString();
                        Grid_Invoice_Summary.Rows[i].Cells[4].Value = dtsummary1.Rows[i]["Total"].ToString();
                        Grid_Invoice_Summary.Rows[i].Cells[5].Value = dtsummary1.Rows[i]["Order_Type_ID"].ToString();

                    }
                    // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
                }
                else
                {
                    Grid_Invoice_Summary.Rows.Clear();
                    Grid_Invoice_Summary.DataSource = null;
                    // lbl_Total_Orders.Text = "0";
                    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                    //grd_Admin_orders.DataBind();
                }


                Hashtable htsummary2 = new Hashtable();
                DataTable dtsummary2 = new DataTable();
                htsummary2.Add("@Trans", "GET_OLD_BALANCE");
                htsummary2.Add("@Subprocess_ID", Sub_Process_ID);
                dtsummary2 = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary2);
                Grid_Old_Balance.EnableHeadersVisualStyles = false;
                Grid_Old_Balance.Columns[0].Width = 36;
                Grid_Old_Balance.Columns[1].Width = 36;
                Grid_Old_Balance.Columns[2].Width = 120;
                Grid_Old_Balance.Columns[3].Width = 120;
                Grid_Old_Balance.Columns[4].Width = 100;
                Grid_Old_Balance.Columns[5].Width = 126;
                Grid_Old_Balance.Columns[6].Width = 100;

                if (dtsearch.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Old_Balance.Rows.Clear();
                    for (int i = 0; i < dtsummary2.Rows.Count; i++)
                    {
                        Grid_Old_Balance.Rows.Add();
                        Grid_Old_Balance.Rows[i].Cells[1].Value = i + 1;
                        Grid_Old_Balance.Rows[i].Cells[2].Value = dtsummary2.Rows[i]["Invoice_No"].ToString();
                        Grid_Old_Balance.Rows[i].Cells[3].Value = dtsummary2.Rows[i]["Month_Name"].ToString();
                        Grid_Old_Balance.Rows[i].Cells[4].Value = dtsummary2.Rows[i]["Total_Invoice_Amount"].ToString();
                        Grid_Old_Balance.Rows[i].Cells[5].Value = dtsummary2.Rows[i]["Total_Inv_Paid_Amount"].ToString();
                        Grid_Old_Balance.Rows[i].Cells[6].Value = dtsummary2.Rows[i]["Balance_Amount"].ToString();
                        Grid_Old_Balance.Rows[i].Cells[7].Value = dtsummary2.Rows[i]["MonthlyInvoice_Id"].ToString();
                        Grid_Old_Balance.Rows[i].Cells[8].Value = dtsummary2.Rows[i]["Invoice_Date"].ToString();
                        Grid_Old_Balance.Rows[i].Cells[9].Value = dtsummary2.Rows[i]["Comments"].ToString();

                    }
                    // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
                }
                else
                {
                    Grid_Old_Balance.Rows.Clear();
                    Grid_Old_Balance.DataSource = null;
                    // lbl_Total_Orders.Text = "0";
                    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                    //grd_Admin_orders.DataBind();
                }


                Load_Old_Balance_Amount();
            }
        }

        public bool Validate_Selection()
        {

            if (ddl_Client_Name.SelectedIndex <= 0)
            {

                MessageBox.Show("Please Select Client Name");
                ddl_Client_Name.Focus();
                return false;

            }
            if (txt_From_date.Text == "" || txt_To_date.Text=="")
            {

                MessageBox.Show("Please Enter Date Range");
                txt_From_date.Focus();
                txt_To_date.Focus();
                return false;
            }
            return true;

        }

        private void Grid_Invoice_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                int Order_Id = int.Parse(Grid_Invoice_Details.Rows[e.RowIndex].Cells[6].Value.ToString());
                string Invoice_No = Grid_Invoice_Details.Rows[e.RowIndex].Cells[7].Value.ToString();
                Invoice_Order_Details inv = new Invoice_Order_Details(Order_Id, user_id, "Update", Invoice_No,User_Role);
                inv.Show();
                //cProbar.stopProgress();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (Validate_Monthly_Invoice() != false && btn_Save.Text=="Genrate Invoice")
            
            
            { 

                Client_Id = int.Parse(ddl_Client_Name.SelectedValue.ToString());
                Sub_Process_ID = int.Parse(ddl_Client_SubProcess.SelectedValue.ToString());
                Hashtable htmax = new Hashtable();
                DataTable dtmax = new DataTable();
                htmax.Add("@Trans", "GET_MAX_INVOICE_AUTO_NUMBER");
                htmax.Add("@Client_Id", Client_Id);

                dtmax = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htmax);
                if (dtmax.Rows.Count > 0)
                {
                    Autoinvoice_No = int.Parse(dtmax.Rows[0]["Invoice_Auto_No"].ToString());


                }

                Hashtable htmax_Invoice_No = new Hashtable();
                DataTable dtmax_invoice_No = new DataTable();
                htmax_Invoice_No.Add("@Trans", "GET_MAX_INVOICE_NUMBER");
                htmax_Invoice_No.Add("@Client_Id", Client_Id);
                dtmax_invoice_No = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htmax_Invoice_No);

                if (dtmax_invoice_No.Rows.Count > 0)
                {

                    Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
                }





                decimal Inv_paying = Convert.ToDecimal(txt_Invoice_Paying_Amount.Text);
                decimal Total_Old_Balance=Convert.ToDecimal(txt_Old_balance.Text);
                decimal total_amount_to_pay = Convert.ToDecimal(txt_Total_Amount_To_Pay.Text);
                decimal Total_Of_Current_Invoice_to_Pay = Convert.ToDecimal(txt_Total.Text);

                
                //decimal Total_Invoice_Paying = Inv_paying - Total_Old_Balance;

                decimal Total_Invoice_Paying = Inv_paying;

                //if (Total_Invoice_Paying > 0)
                //{ 
                

                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();

                    ht.Add("@Trans", "INSERT");
                  
                    ht.Add("@Client_Id", Client_Id);
                    ht.Add("@Subprocess_ID", Sub_Process_ID);
                    ht.Add("@Invoice_Auto_No", Autoinvoice_No);
                    ht.Add("@Invoice_No", Invoice_Number);
                    ht.Add("@Invoice_From_date",txt_From_date.Text);
                    ht.Add("@Invoice_To_Date",txt_To_date.Text);
                    ht.Add("@Total_Invoice_Amount", Total_Of_Current_Invoice_to_Pay);
                    ht.Add("@Total_Inv_Paid_Amount", Total_Invoice_Paying);
                    ht.Add("@Invoice_Date", txt_Invoice_Date.Text);
                    ht.Add("@Invoice_Paid_Status",int.Parse(ddl_Payment_Status.SelectedValue.ToString()));
                    ht.Add("@Comments", txt_Invoice_comments.Text);
                    ht.Add("@Email_Status", "False");
                    ht.Add("@Revised", "False");
                    ht.Add("@Status", "True");
                    ht.Add("@Inserted_By",user_id);

                   object Invoice_Id = dataaccess.ExecuteSPForScalar("Sp_Monthly_Invoice", ht);
                  
                   int Inv_Id=int.Parse(Invoice_Id.ToString());

                   decimal Total_Amount_To_Clear_Balance = Total_Old_Balance;

                  
                    for (int i = 0; i < Grid_Invoice_Details.Rows.Count; i++)
                    {

                        //string nnn = no.ToString();
                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        int Order_Id=int.Parse(Grid_Invoice_Details.Rows[i].Cells[6].Value.ToString());
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Order_ID", Order_Id);
                        htcheck.Add("@Client_Id",Client_Id);
                        htcheck.Add("@Sub_Process_Id", Sub_Process_ID);
                        dtcheck = dataaccess.ExecuteSP("Sp_Order_Monthly_Invoice_Order", htcheck);
                        int check = int.Parse(dtcheck.Rows[0]["Count"].ToString());
                        if (check == 0)
                        {
                              Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();

                    htin.Add("@Trans", "INSERT");
                               htin.Add("@Monthly_Invoice_Id",Inv_Id);
                    htin.Add("@Client_Id", Client_Id);
                    htin.Add("@Sub_Process_Id", Sub_Process_ID);
                    htin.Add("@Order_Id", Order_Id);
                  
                            dtin=dataaccess.ExecuteSP("Sp_Order_Monthly_Invoice_Order",htin);


                        }



                    }
                    //if (Grid_Old_Balance.Rows.Count > 0)
                    //{

                    //    for (int j = 0; j < Grid_Old_Balance.Rows.Count; j++)
                    //    {
                    //        bool isChecked = (bool)Grid_Old_Balance[0, j].FormattedValue;


                    //        if (isChecked == true)
                    //        {

                    //            string balance = Grid_Old_Balance.Rows[j].Cells[6].Value.ToString();
                    //            int Monhly_invoice_Id = int.Parse(Grid_Old_Balance.Rows[j].Cells[7].Value.ToString());

                    //            Hashtable htin = new Hashtable();
                    //            DataTable dtin = new DataTable();

                    //            decimal total_balnce = Convert.ToDecimal(balance.ToString());

                    //            if (Inv_paying >= total_balnce)
                    //            {

                    //                balance_amount_paid = total_balnce;
                    //            }
                    //            else if (Inv_paying < total_balnce)
                    //            {

                    //                balance_amount_paid = total_balnce - Inv_paying;

                    //            }

                    //            htin.Add("@Trans", "INSERT");
                    //            htin.Add("@Monthly_Invoice_Id", Inv_Id);
                    //            htin.Add("@Balance_Monthly_Invoice_Id", Monhly_invoice_Id);
                    //            decimal diff = total_amount_to_pay - Inv_paying;
                    //            if (diff > 0)
                    //            {
                    //                htin.Add("@Balance_Invoice_Amount", diff);
                    //            }
                    //            else
                    //            {

                    //                htin.Add("@Balance_Invoice_Amount", 0);
                    //            }
                    //            htin.Add("@Old_Balanc_Amount", balance_amount_paid);
                    //            htin.Add("@Balance_Invoice_Amount_Paid", balance_amount_paid);
                    //            htin.Add("@Sub_Process_Id", Sub_Process_ID);
                    //            htin.Add("@Status", "True");
                    //            htin.Add("@Inserted_By", user_id);
                    //            dtin = dataaccess.ExecuteSP("Sp_Old_Monthly_Invoice_Entry", htin);
                    //        }
                    //    }
                    //}
                    ////else
                    ////{
                    //if (Grid_Old_Balance.Rows.Count >=0  && txt_Invoice_Paying_Amount.Text == "0.00" && txt_Invoice_Paying_Amount.Text!="")
                    //    {

                    //        decimal diff = total_amount_to_pay - Inv_paying;
                    //        if (diff > 0)
                    //        {
                    //            Hashtable htin = new Hashtable();
                    //            DataTable dtin = new DataTable();

                    //            htin.Add("@Trans", "INSERT");
                    //            htin.Add("@Monthly_Invoice_Id", Inv_Id);
                    //            htin.Add("@Balance_Monthly_Invoice_Id", Inv_Id);
                    //            htin.Add("@Sub_Process_Id", Sub_Process_ID);
                    //            htin.Add("@Old_Balanc_Amount",0);
                    //            htin.Add("@Balance_Invoice_Amount", diff);
                    //            htin.Add("@Balance_Invoice_Amount_Paid", 0);
                    //            htin.Add("@Status", "True");
                    //            htin.Add("@Inserted_By", user_id);
                    //            dtin = dataaccess.ExecuteSP("Sp_Old_Monthly_Invoice_Entry", htin);

                    //        }
                    //    }

                    //    //if()
                    //}

                
                    MessageBox.Show("Invoice Genrated Sucessfully");
                    Clear();
                    btn_Get_Click(sender, e);
                }
                //else
                ////{
                
                //    MessageBox.Show("Need to Clear Old Balance Amount");

                //}
                //}

            else if (Validate_Monthly_Invoice() != false && btn_Save.Text == "Edit Invoice")
            {

                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                decimal Inv_paying = Convert.ToDecimal(txt_Invoice_Paying_Amount.Text);
                decimal Total_Old_Balance = Convert.ToDecimal(txt_Old_balance.Text);
                decimal total_amount_to_pay = Convert.ToDecimal(txt_Total_Amount_To_Pay.Text);
                decimal Total_Of_Current_Invoice_to_Pay = Convert.ToDecimal(txt_Total.Text);
                decimal Total_Invoice_Paying = Inv_paying;

                ht.Add("@Trans", "UPDATE_INV_DETAILS");
                ht.Add("@MonthlyInvoice_Id", Invoice_Id);
                ht.Add("@Invoice_Date", txt_Invoice_Date.Text);
                ht.Add("@Invoice_Paid_Status",int.Parse(ddl_Payment_Status.SelectedValue.ToString()));
                ht.Add("@Total_Inv_Paid_Amount", Inv_paying);
                ht.Add("@Comments", txt_Invoice_comments.Text);
                ht.Add("@Status", "True");
                ht.Add("@Modified_By", user_id);

                dt = dataaccess.ExecuteSP("Sp_Monthly_Invoice", ht);

                MessageBox.Show("Invoice Updated Sucessfully");
                this.Close();
                Clear();
                btn_Get_Click(sender, e);


            }



        }
        
        public void Clear()
        {

            txt_Invoice_comments.Text = "";
         
            txt_Invoice_Paying_Amount.Text = "";
            txt_Invoice_Paying_Amount.Enabled = true;
            ddl_Payment_Status.Enabled = true;
         
        }

        private bool Validate_Monthly_Invoice()
        {

            if (txt_Invoice_Paying_Amount.Text == "")
            {


                MessageBox.Show("Please Enter Invoice Amount");
                txt_Invoice_Paying_Amount.Focus();
                return false;
            }
            if (txt_Invoice_Date.Text == "")
            {

                MessageBox.Show("Please Enter Invoice Date");
                txt_Invoice_Date.Focus();
                return false;
            }
            if (ddl_Payment_Status.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select Invoice Status");
                ddl_Payment_Status.Focus();
                return false;
            

            }
            return true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            btn_Get_Click(sender, e);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txt_Invoice_Paying_Amount_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

       

        private void txt_Invoice_Paying_Amount_TextChanged(object sender, EventArgs e)
        {
            if (txt_Invoice_Paying_Amount.Text != "")
            {

                decimal Inv_paying = Convert.ToDecimal(txt_Invoice_Paying_Amount.Text);
                if (txt_Total_Amount_To_Pay.Text != "")
                {
                     total_amount_to_pay = Convert.ToDecimal(txt_Total_Amount_To_Pay.Text);

                }
                if (Inv_paying <= total_amount_to_pay)
                {


                    decimal diff = total_amount_to_pay - Inv_paying;
                    txt_Inv_Balance_Amount.Text = diff.ToString();
                }
                else if (Inv_paying > total_amount_to_pay)
                {

                    MessageBox.Show("Paying Amount Should not be greater than Total Amount");
                    txt_Invoice_Paying_Amount.Text = "";
                    txt_Invoice_Paying_Amount.Focus();

                }



            }
        }

       

        private void Populate_Invoice_Prievious_balance_Amount()
        {
            Total_Invoice_Balance = 0;
            for (int i = 0; i < Grid_Old_Balance.Rows.Count; i++)
            {
                bool isChecked = (bool)Grid_Old_Balance[0, i].EditedFormattedValue;


                if (isChecked == true)
                {

                    decimal Invoice_Balance = Convert.ToDecimal(Grid_Old_Balance.Rows[i].Cells[6].Value.ToString());



                    Total_Invoice_Balance = Total_Invoice_Balance + Invoice_Balance;
                }


            }
            txt_Old_balance.Text = Total_Invoice_Balance.ToString();
        }

        private void Grid_Old_Balance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Populate_Invoice_Prievious_balance_Amount();
                Load_Old_Balance_Amount();
            }
            if(e.ColumnIndex==2)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                int Invoice_ID = int.Parse(Grid_Old_Balance.Rows[e.RowIndex].Cells[7].Value.ToString());
                int Client_ID = int.Parse(ddl_Client_Name.SelectedValue.ToString());
                Sub_Process_ID = int.Parse(ddl_Client_SubProcess.SelectedValue.ToString());
                string Invoice_No = Grid_Old_Balance.Rows[e.RowIndex].Cells[2].Value.ToString();
                string Invoice_Date = Grid_Old_Balance.Rows[e.RowIndex].Cells[8].Value.ToString();
                string Invoice_Comments = Grid_Old_Balance.Rows[e.RowIndex].Cells[9].Value.ToString();
                string Invoice_Month= Grid_Old_Balance.Rows[e.RowIndex].Cells[3].Value.ToString();
                InvoiceRep.Invoice_Monthly inv = new InvoiceRep.Invoice_Monthly(user_id, "Update", Invoice_No, Invoice_ID, Client_ID, Sub_Process_ID, Invoice_Date, Invoice_Comments, Invoice_Month,User_Role);
                inv.Show();
                //cProbar.stopProgress();
            

            }

        }

      

    
      

     
       
    }
}
