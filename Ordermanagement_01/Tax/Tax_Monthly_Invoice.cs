using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Monthly_Invoice : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid, invoice_id, client_id, subprocess_id, Insert_clientid, Insert_subprocessid, Autoinvoice_No ;
        string operation, invoice_no, invoice_date, invoice_cmt, invoice_mon, gen_Invoice_Number;
        decimal total_amount_to_pay;
        public Tax_Monthly_Invoice(int USER_ID, string OPERATION, string INV_NUM, int INVOICE_ID, int CLIENT_ID, int SUB_PROCESS_ID, string INVOICE_DATE, string INVOICE_COMMENTS, string INVOICE_MONTH)
        {
            InitializeComponent();
            userid = USER_ID;
            operation = OPERATION;
            invoice_no = INV_NUM;
            invoice_id = INVOICE_ID;
            client_id = CLIENT_ID;
            subprocess_id = SUB_PROCESS_ID;
            invoice_date = INVOICE_DATE;
            invoice_cmt = INVOICE_COMMENTS;
            invoice_mon = INVOICE_MONTH;
        }

        private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Client_Name.SelectedIndex > 0)
            {

                dbc.BindSubProcess_ForEntry(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            btn_Get_Click(sender, e);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Validate_Monthly_Invoice() != false && btn_Save.Text == "Genrate Invoice")
            {
                //generating invoice auto number
                Insert_clientid = int.Parse(ddl_Client_Name.SelectedValue.ToString());
                Insert_subprocessid = int.Parse(ddl_Client_SubProcess.SelectedValue.ToString());
                Hashtable htmax = new Hashtable();
                DataTable dtmax = new DataTable();
                htmax.Add("@Trans", "GET_MAX_INVOICE_AUTO_NUMBER");
                htmax.Add("@Client_Id", Insert_clientid);

                dtmax = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htmax);
                if (dtmax.Rows.Count > 0)
                {
                    Autoinvoice_No = int.Parse(dtmax.Rows[0]["Invoice_Auto_No"].ToString());
                }

                //generating invoice number
                Hashtable htmax_Invoice_No = new Hashtable();
                DataTable dtmax_invoice_No = new DataTable();
                htmax_Invoice_No.Add("@Trans", "GET_MAX_INVOICE_NUMBER");
                htmax_Invoice_No.Add("@Client_Id", Insert_clientid);
                dtmax_invoice_No = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htmax_Invoice_No);

                if (dtmax_invoice_No.Rows.Count > 0)
                {
                    gen_Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
                }

                
                //Inserting Monthly invoice record 
                decimal Inv_paying = Convert.ToDecimal(txt_Invoice_Paying_Amount.Text);
                decimal Total_Old_Balance = Convert.ToDecimal(txt_Old_balance.Text);
                decimal total_amount_to_pay = Convert.ToDecimal(txt_Total_Amount_To_Pay.Text);
                decimal Total_Of_Current_Invoice_to_Pay = Convert.ToDecimal(txt_Total.Text);

                decimal Total_Invoice_Paying = Inv_paying;

                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "INSERT");

                ht.Add("@Client_Id", Insert_clientid);
                ht.Add("@Subprocess_ID", Insert_subprocessid);
                ht.Add("@Invoice_Auto_No", Autoinvoice_No);
                ht.Add("@Invoice_No", gen_Invoice_Number);
                ht.Add("@Invoice_From_date", txt_From_date.Text);
                ht.Add("@Invoice_To_Date", txt_To_date.Text);
                ht.Add("@Total_Invoice_Amount", Total_Of_Current_Invoice_to_Pay);
                ht.Add("@Total_Inv_Paid_Amount", Total_Invoice_Paying);
                ht.Add("@Invoice_Date", txt_Invoice_Date.Text);
                ht.Add("@Invoice_Paid_Status", int.Parse(ddl_Payment_Status.SelectedValue.ToString()));
                ht.Add("@Comments", txt_Invoice_comments.Text);
                ht.Add("@Email_Status", "False");
                ht.Add("@Revised", "False");
                ht.Add("@Status", "True");
                ht.Add("@Inserted_By", userid);

                object Invoice_Id = dataaccess.ExecuteSPForScalar("Sp_Tax_Monthly_Invoice_Entry", ht);

                int Inv_Id = int.Parse(Invoice_Id.ToString());

                decimal Total_Amount_To_Clear_Balance = Total_Old_Balance;
                for (int i = 0; i < Grid_Invoice_Details.Rows.Count; i++)
                {
                    //Inserting Monthly invoice record orders
                    //string nnn = no.ToString();
                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    int Order_Id = int.Parse(Grid_Invoice_Details.Rows[i].Cells[6].Value.ToString());
                    htcheck.Add("@Trans", "CHECK");
                    htcheck.Add("@Order_ID", Order_Id);
                    htcheck.Add("@Client_Id", Insert_clientid);
                    htcheck.Add("@Subprocess_ID", Insert_subprocessid);
                    dtcheck = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Order", htcheck);
                    int check = int.Parse(dtcheck.Rows[0]["Count"].ToString());
                    if (check == 0)
                    {
                        Hashtable htin = new Hashtable();
                        DataTable dtin = new DataTable();

                        htin.Add("@Trans", "INSERT_MON_INVOICE");
                        htin.Add("@Tax_MonthlyInvoice_Id", Inv_Id);
                        htin.Add("@Client_Id", Insert_clientid);
                        htin.Add("@Subprocess_ID", Insert_subprocessid);
                        htin.Add("@Order_Id", Order_Id);

                        dtin = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Order", htin);


                    }
                }

                MessageBox.Show("Monthly Invoice Genrated Sucessfully");
                Clear();
                btn_Get_Click(sender, e);
                //
            }
            else if (btn_Save.Text == "Edit Invoice")
            {
                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();
                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@MonthlyInvoice_Id", invoice_id);
                dtcheck = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htcheck);
                if (dtcheck.Rows.Count > 0)
                {
                    int count = int.Parse(dtcheck.Rows[0]["Count"].ToString());
                    if (count > 0)
                    {
                        decimal Inv_paying = Convert.ToDecimal(txt_Invoice_Paying_Amount.Text);
                        decimal Total_Old_Balance = Convert.ToDecimal(txt_Old_balance.Text);
                        decimal total_amount_to_pay = Convert.ToDecimal(txt_Total_Amount_To_Pay.Text);
                        decimal Total_Of_Current_Invoice_to_Pay = Convert.ToDecimal(txt_Total.Text);
                        //update
                        Hashtable htup = new Hashtable();
                        DataTable dtup = new DataTable();
                        htup.Add("@Trans", "UPDATE");
                        htup.Add("@MonthlyInvoice_Id", invoice_id);
                        htup.Add("@Total_Inv_Paid_Amount", Inv_paying);
                        htup.Add("@Invoice_Date", txt_Invoice_Date.Text);
                        htup.Add("@Invoice_Paid_Status", int.Parse(ddl_Payment_Status.SelectedValue.ToString()));
                        htup.Add("@Comments", txt_Invoice_comments.Text);

                        htup.Add("@Modified_By", userid);
                        dtup = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htup);
                        MessageBox.Show("Monthly Invoice Updated Sucessfully");
                        Clear();
                        this.Close();
                    }
                }
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
        private void Grid_Old_Balance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void Tax_Monthly_Invoice_Load(object sender, EventArgs e)
        {
            if (invoice_id == 0)
            {
                //insert
                dbc.BindClientName(ddl_Client_Name);
                txt_From_date.Text = DateTime.Now.ToString();
                txt_To_date.Text = DateTime.Now.ToString();
                txt_Invoice_Date.Text = DateTime.Now.ToString();
                btn_Save.Text = "Genrate Invoice";
                dbc.BindPayment_Status(ddl_Payment_Status);
                Control_Enable_True();
                txt_Old_balance.Text = "0.00";
                txt_Invoice_Paying_Amount.Text = "0.00";
                ddl_Payment_Status.SelectedValue = "1";
            }
            else
            {
                //update
                dbc.BindClientName(ddl_Client_Name);
                ddl_Client_Name.SelectedValue = client_id;
                ddl_Client_Name_SelectedIndexChanged(sender, e);
                dbc.BindPayment_Status(ddl_Payment_Status);
                ddl_Client_SubProcess.SelectedValue = subprocess_id;
                txt_From_date.Text = DateTime.Now.ToString();
                txt_To_date.Text = DateTime.Now.ToString();
                txt_Invoice_Date.Text = DateTime.Now.ToString();
                btn_Save.Text = "Edit Invoice";

                lbl_Invoice_Number.Text = invoice_no.ToString();
                lbl_Inv_Month.Text = invoice_mon.ToString();
                //txt_Invoice_Date.Text = Invoice_Date.ToString();
                //txt_Invoice_comments.Text = Invoice_Comments.ToString();
                Control_EnableFalse();
                Load_Invoice_Entred_Order_Details();

            }
            dbc.BindClientName(ddl_Client_Name);
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
        public void Load_Invoice_Entred_Order_Details()
        {


            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_ENTERED_TAX_ORDER_INVOICE_COST_DETAILS");
            htsearch.Add("@MonthlyInvoice_Id", invoice_id);

            dtsearch = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htsearch);

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
                    Grid_Invoice_Details.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Base_Cost"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[3].Value = dtsearch.Rows[i]["Maily_way_Cost"].ToString();
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

            Hashtable hts = new Hashtable();
            DataTable dts = new DataTable();
            hts.Add("@Trans", "GET_ENTERED_TAX_INVOICE_COST_DETAILS");
            hts.Add("@MonthlyInvoice_Id", invoice_id);

            dts = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", hts);

            if (dts.Rows.Count > 0)
            {

                txt_No_Of_orders.Text = dts.Rows[0]["No_Of_Orders"].ToString();
                txt_Basic_Cost.Text = dts.Rows[0]["Base_Cost"].ToString();
                txt_Mail_away_cost.Text = dts.Rows[0]["Maily_way_Cost"].ToString();
                txt_Total.Text = dts.Rows[0]["Total"].ToString();


            }
            else
            {

                txt_No_Of_orders.Text = "0";
                txt_Basic_Cost.Text = "0.00";
                txt_Mail_away_cost.Text = "0.00";
                txt_Total.Text = "0.00";

            }

            //Summary of Invoice on Order Type Wise
            Hashtable htsummary1 = new Hashtable();
            DataTable dtsummary1 = new DataTable();
            htsummary1.Add("@Trans", "GET_ENTRED_SUMMARY_TAX_ORDER_TYPE_WISE_COST");
            htsummary1.Add("@MonthlyInvoice_Id", invoice_id);

            dtsummary1 = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htsummary1);
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


            //Hashtable htsummary2 = new Hashtable();
            //DataTable dtsummary2 = new DataTable();
            //htsummary2.Add("@Trans", "GET_ENTERED_OLD_BALANCE");
            //htsummary2.Add("@Subprocess_ID", subprocess_id);
            //dtsummary2 = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary2);
            //Grid_Old_Balance.EnableHeadersVisualStyles = false;
            //Grid_Old_Balance.Columns[0].Width = 36;
            //Grid_Old_Balance.Columns[1].Width = 36;
            //Grid_Old_Balance.Columns[2].Width = 120;
            //Grid_Old_Balance.Columns[3].Width = 120;
            //Grid_Old_Balance.Columns[4].Width = 100;
            //Grid_Old_Balance.Columns[5].Width = 126;



            //if (dtsearch.Rows.Count > 0)
            //{
            //    //ex2.Visible = true;
            //    Grid_Old_Balance.Rows.Clear();
            //    for (int i = 0; i < dtsummary2.Rows.Count; i++)
            //    {
            //        Grid_Old_Balance.Rows.Add();
            //        Grid_Old_Balance.Rows[i].Cells[1].Value = i + 1;
            //        Grid_Old_Balance.Rows[i].Cells[2].Value = dtsummary2.Rows[i]["Invoice_No"].ToString();
            //        Grid_Old_Balance.Rows[i].Cells[3].Value = dtsummary2.Rows[i]["Month_Name"].ToString();
            //        Grid_Old_Balance.Rows[i].Cells[4].Value = dtsummary2.Rows[i]["Total_Invoice_Amount"].ToString();
            //        Grid_Old_Balance.Rows[i].Cells[5].Value = dtsummary2.Rows[i]["Total_Inv_Paid_Amount"].ToString();
            //        Grid_Old_Balance.Rows[i].Cells[6].Value = dtsummary2.Rows[i]["Balance_Amount"].ToString();
            //        Grid_Old_Balance.Rows[i].Cells[7].Value = dtsummary2.Rows[i]["MonthlyInvoice_Id"].ToString();
            //        Grid_Old_Balance.Rows[i].Cells[8].Value = dtsummary2.Rows[i]["Invoice_Date"].ToString();
            //        Grid_Old_Balance.Rows[i].Cells[9].Value = dtsummary2.Rows[i]["Comments"].ToString();
            //    }
            //    // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
            //}
            //else
            //{
            //    Grid_Old_Balance.Rows.Clear();
            //    Grid_Old_Balance.DataSource = null;
            //    // lbl_Total_Orders.Text = "0";
            //    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
            //    //grd_Admin_orders.DataBind();
            //}






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
            htinv.Add("@Trans", "SELECT_TAX_MONTHLY_INVOICE_DETAILS");
            htinv.Add("@MonthlyInvoice_Id", invoice_id);
            dtinv = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htinv);
            if (dtinv.Rows.Count > 0)
            {

                txt_Invoice_Paying_Amount.Text = dtinv.Rows[0]["Inv_Paying_Amount"].ToString();
                //txt_Invoice_Paying_Amount.Enabled = false;
                txt_Invoice_Date.Enabled = false;
                //txt_Invoice_Paying_Amount.ReadOnly = true;
                //ddl_Payment_Status.Enabled = false;

                ddl_Payment_Status.SelectedValue = dtinv.Rows[0]["Invoice_Paid_Status"].ToString();
                txt_Invoice_comments.Text = dtinv.Rows[0]["Comments"].ToString();
                txt_Invoice_Date.Text = dtinv.Rows[0]["Invoice_Date"].ToString();

                //ddl_Client_Name.SelectedValue = int.Parse(dtinv.Rows[0]["Client_Id"].ToString());
                //ddl_Client_SubProcess.SelectedValue = int.Parse(dtinv.Rows[0]["Subprocess_Id"].ToString());
            }

        }

        private void btn_Get_Click(object sender, EventArgs e)
        {
            Insert_subprocessid = int.Parse(ddl_Client_SubProcess.SelectedValue.ToString());
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            if (txt_From_date.Text != "" && txt_To_date.Text != "")
            {
                DateTime fromdate = Convert.ToDateTime(txt_From_date.Text, usDtfi);
                DateTime Todate = Convert.ToDateTime(txt_To_date.Text, usDtfi);
                Hashtable htsearch = new Hashtable();
                DataTable dtsearch = new DataTable();
                htsearch.Add("@Trans", "SELECT_DATE_RANGE_CLIENT_ID");
                htsearch.Add("@Subprocess_ID", Insert_subprocessid);
                htsearch.Add("@From_Date", fromdate);
                htsearch.Add("@To_Date", Todate);
                dtsearch = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htsearch);

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
                        Grid_Invoice_Details.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Base_Cost"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[3].Value = dtsearch.Rows[i]["Maily_way_Cost"].ToString();
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
                htsummary.Add("@Trans", "GET_SUMMARY_OF_TAX_INVOICE_CLIENT_WISE");
                htsummary.Add("@Subprocess_ID", Insert_subprocessid);
                htsummary.Add("@From_Date", fromdate);
                htsummary.Add("@To_Date", Todate);
                dtsummary = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htsummary);

                if (dtsummary.Rows.Count > 0)
                {

                    txt_No_Of_orders.Text = dtsummary.Rows[0]["No_Of_Orders"].ToString();
                    txt_Basic_Cost.Text = dtsummary.Rows[0]["Base_Cost"].ToString();
                    txt_Mail_away_cost.Text = dtsummary.Rows[0]["Maily_way_Cost"].ToString();
                    txt_Total.Text = dtsummary.Rows[0]["Total"].ToString();
                    txt_Total_Amount_To_Pay.Text = dtsummary.Rows[0]["Total"].ToString();

                }
                else
                {

                    txt_No_Of_orders.Text = "0";
                    txt_Basic_Cost.Text = "0.00";
                    txt_Mail_away_cost.Text = "0.00";
                    txt_Total.Text = "0.00";
                    txt_Total_Amount_To_Pay.Text = "0.00";
                }

                //Summary of Invoice on Order Type Wise
                Hashtable htsummary1 = new Hashtable();
                DataTable dtsummary1 = new DataTable();
                htsummary1.Add("@Trans", "SELECT_DATE_RANGE_PRODUCT_TYPE_ID");
                htsummary1.Add("@Subprocess_ID", Insert_subprocessid);
                htsummary1.Add("@From_Date", fromdate);
                htsummary1.Add("@To_Date", Todate);
                dtsummary1 = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htsummary1);
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


                //Hashtable htsummary2 = new Hashtable();
                //DataTable dtsummary2 = new DataTable();
                //htsummary2.Add("@Trans", "GET_OLD_BALANCE");
                //htsummary2.Add("@Subprocess_ID", Sub_Process_ID);
                //dtsummary2 = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsummary2);
                //Grid_Old_Balance.EnableHeadersVisualStyles = false;
                //Grid_Old_Balance.Columns[0].Width = 36;
                //Grid_Old_Balance.Columns[1].Width = 36;
                //Grid_Old_Balance.Columns[2].Width = 120;
                //Grid_Old_Balance.Columns[3].Width = 120;
                //Grid_Old_Balance.Columns[4].Width = 100;
                //Grid_Old_Balance.Columns[5].Width = 126;
                //Grid_Old_Balance.Columns[6].Width = 100;

                //if (dtsearch.Rows.Count > 0)
                //{
                //    //ex2.Visible = true;
                //    Grid_Old_Balance.Rows.Clear();
                //    for (int i = 0; i < dtsummary2.Rows.Count; i++)
                //    {
                //        Grid_Old_Balance.Rows.Add();
                //        Grid_Old_Balance.Rows[i].Cells[1].Value = i + 1;
                //        Grid_Old_Balance.Rows[i].Cells[2].Value = dtsummary2.Rows[i]["Invoice_No"].ToString();
                //        Grid_Old_Balance.Rows[i].Cells[3].Value = dtsummary2.Rows[i]["Month_Name"].ToString();
                //        Grid_Old_Balance.Rows[i].Cells[4].Value = dtsummary2.Rows[i]["Total_Invoice_Amount"].ToString();
                //        Grid_Old_Balance.Rows[i].Cells[5].Value = dtsummary2.Rows[i]["Total_Inv_Paid_Amount"].ToString();
                //        Grid_Old_Balance.Rows[i].Cells[6].Value = dtsummary2.Rows[i]["Balance_Amount"].ToString();
                //        Grid_Old_Balance.Rows[i].Cells[7].Value = dtsummary2.Rows[i]["MonthlyInvoice_Id"].ToString();
                //        Grid_Old_Balance.Rows[i].Cells[8].Value = dtsummary2.Rows[i]["Invoice_Date"].ToString();
                //        Grid_Old_Balance.Rows[i].Cells[9].Value = dtsummary2.Rows[i]["Comments"].ToString();

                //    }
                //    // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
                //}
                //else
                //{
                //    Grid_Old_Balance.Rows.Clear();
                //    Grid_Old_Balance.DataSource = null;
                //    // lbl_Total_Orders.Text = "0";
                //    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //    //grd_Admin_orders.DataBind();
                //}


               // Load_Old_Balance_Amount();
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

        private void txt_Total_Amount_To_Pay_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txt_Old_balance_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Old_Balance_Click(object sender, EventArgs e)
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
    }
}
