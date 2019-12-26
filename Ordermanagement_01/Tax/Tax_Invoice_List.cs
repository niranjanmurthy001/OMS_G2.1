using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Invoice_List : Form
    {
        Commonclass cc = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid,mailsend=0; string user_roleid, search;
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();

        DataTable dtMonthlyuser = new DataTable();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Tax_Invoice_List(int Userid,string User_roleid)
        {
            InitializeComponent();
            userid = Userid;
            user_roleid = User_roleid;
        }

        private void btn_New_Invoice_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Tax.Tax_Invoice_Generation invoice_gen = new Ordermanagement_01.Tax.Tax_Invoice_Generation(0, userid, "Insert", "0",user_roleid);
            invoice_gen.Show();
        }

        private void Tax_Invoice_List_Load(object sender, EventArgs e)
        {
            if (rbtn_TaxInvoice_NotSended.Checked == true)
            {
                Bind_Tax_Invoice_data();
            }
            else if (rbtn_TaxInvoice_Sended.Checked == true)
            {
                Bind_Tax_Invoice_data();
            }
            Bind_Monthly_Invoice_data();

        }
        private void Bind_Tax_Invoice_data()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_NOT_SENT_INVOICE");
            if (rbtn_TaxInvoice_NotSended.Checked == true)
            {
                ht.Add("@Email_Status", "False");
            }
            else
            {
                ht.Add("@Email_Status", "True");
            }

            dt = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", ht);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
            if (dt.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Invoice_No"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Client_Name"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["State_county"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Recived_Date"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["Base_Cost"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dt.Rows[i]["Maily_way_Cost"].ToString();
                    grd_order.Rows[i].Cells[11].Value = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i]["Base_Cost"].ToString()) + Convert.ToDecimal(dt.Rows[i]["Maily_way_Cost"].ToString()));
                    grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["Invoice_Date"].ToString();
                    grd_order.Rows[i].Cells[13].Value = dt.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[16].Value = dt.Rows[i]["Tax_Invoice_Id"].ToString();
                    grd_order.Rows[i].Cells[17].Value = dt.Rows[i]["Sub_ProcessId"].ToString();
                    grd_order.Rows[i].Cells[18].Value = dt.Rows[i]["Client_Id"].ToString();
                }
            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.DataSource = null;
            }

        }
       

        private void rbtn_TaxInvoice_NotSended_CheckedChanged(object sender, EventArgs e)
        {
            Bind_Tax_Invoice_data();
            cbo_colmn.Text = "";
            txt_orderserach_Number.Text = "";
        }

        private void rbtn_TaxInvoice_Sended_CheckedChanged(object sender, EventArgs e)
        {
            Bind_Tax_Invoice_data();
            cbo_colmn.Text = "";
            txt_orderserach_Number.Text = "";
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            if (rbtn_TaxInvoice_NotSended.Checked == true)
            {
                Bind_Tax_Invoice_data();
            }
            else if (rbtn_TaxInvoice_Sended.Checked == true)
            {
                Bind_Tax_Invoice_data();
            }
            cbo_colmn.Text = "";
            txt_orderserach_Number.Text = "";
        }

        private void check_All_CheckedChanged(object sender, EventArgs e)
        {
            if (check_All.Checked == true)
            {
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    grd_order[0, i].Value = true;
                }
            }
            else if (check_All.Checked == false)
            {
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    grd_order[0, i].Value = false;
                }
            }
        }

        private void txt_orderserach_Number_TextChanged(object sender, EventArgs e)
        {
            if(cbo_colmn.Text!="")
            {
                DataView dtsearch = new DataView(dt);
                search = txt_orderserach_Number.Text.ToString();
                if (cbo_colmn.Text == "Order Number")
                {
                    dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%'";
                }
                else if (cbo_colmn.Text == "Invoice Number")
                {
                    dtsearch.RowFilter = "Invoice_No like '%" + search.ToString() + "%'";
                }
                else if (cbo_colmn.Text == "Invoice Date")
                {
                    dtsearch.RowFilter = "Invoice_Date like '%" + search.ToString() + "%'";
                }
                else if (cbo_colmn.Text == "Client")
                {
                    dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%'";
                }
                else if (cbo_colmn.Text == "Sub Client")
                {
                    dtsearch.RowFilter = "Sub_ProcessName like '%" + search.ToString() + "%'";
                }
                else if (cbo_colmn.Text == "Received Date")
                {
                    dtsearch.RowFilter = "Recived_Date like '%" + search.ToString() + "%'";
                }
                else if (cbo_colmn.Text == "Order Type")
                {
                    dtsearch.RowFilter = "Order_Type like '%" + search.ToString() + "%'";
                }
                DataTable dtnew = new DataTable();
                dtnew = dtsearch.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dtnew.Rows[i]["Client_Order_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dtnew.Rows[i]["Invoice_No"].ToString();
                        grd_order.Rows[i].Cells[4].Value = dtnew.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[5].Value = dtnew.Rows[i]["Sub_ProcessName"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dtnew.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dtnew.Rows[i]["State_county"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dtnew.Rows[i]["Recived_Date"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dtnew.Rows[i]["Base_Cost"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dtnew.Rows[i]["Maily_way_Cost"].ToString();
                        grd_order.Rows[i].Cells[11].Value = Convert.ToDecimal(Convert.ToDecimal(dtnew.Rows[i]["Base_Cost"].ToString()) + Convert.ToDecimal(dtnew.Rows[i]["Maily_way_Cost"].ToString()));
                        grd_order.Rows[i].Cells[12].Value = dtnew.Rows[i]["Invoice_Date"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dtnew.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[17].Value = dtnew.Rows[i]["Tax_Invoice_Id"].ToString();
                        grd_order.Rows[i].Cells[18].Value = dtnew.Rows[i]["Sub_ProcessId"].ToString();
                        grd_order.Rows[i].Cells[19].Value = dtnew.Rows[i]["Client_Id"].ToString();
                    }
                }
                else
                {
                    if (rbtn_TaxInvoice_NotSended.Checked == true)
                    {
                        Bind_Tax_Invoice_data();
                    }
                    else if (rbtn_TaxInvoice_Sended.Checked == true)
                    {
                        Bind_Tax_Invoice_data();
                    }
                }
            }
            
        }

        private void grd_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    //order viewing
                    int invoiceid=int.Parse(grd_order.Rows[e.RowIndex].Cells[17].Value.ToString());
                    string invoice_no = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                    Ordermanagement_01.Tax.Tax_Invoice_Generation invoice_view = new Ordermanagement_01.Tax.Tax_Invoice_Generation(invoiceid, userid, "VIEW", invoice_no, user_roleid);
                    invoice_view.Show();
                }
                else if (e.ColumnIndex == 14)
                {
                    //report viewing
                    int invoiceid = int.Parse(grd_order.Rows[e.RowIndex].Cells[17].Value.ToString());
                    string invoice_no = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                    Ordermanagement_01.Tax.Tax_Invoice_Reports.Tax_Invoice_rpt invoice_rpt = new Ordermanagement_01.Tax.Tax_Invoice_Reports.Tax_Invoice_rpt(invoiceid);
                    invoice_rpt.Show();
                }
                else if (e.ColumnIndex == 15)
                {
                    //invoice email sending
                }
                else if (e.ColumnIndex == 19)
                {
                    //invoice delete 
                    int invoiceid = int.Parse(grd_order.Rows[e.RowIndex].Cells[17].Value.ToString());
                    DialogResult dialog = MessageBox.Show("Are you sure want to delete this Monthly invoice", "Monthly Invoice Delete confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        //delete this record
                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "DELETE__INVOICE");
                        ht.Add("@Tax_Invoice_Id", invoiceid);
                        dt = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", ht);
                        MessageBox.Show("Invoice Deleted Successfully");
                       
                    }
                    Bind_Tax_Invoice_data();
                }

                     
            }
        }

        private void btn_Send_All_Click(object sender, EventArgs e)
        {

        }

        private void btn_Monthly_Refresh_Click(object sender, EventArgs e)
        {
            Bind_Monthly_Invoice_data();
        }

        private void rbtn_Monthly_Not_Sent_CheckedChanged(object sender, EventArgs e)
        {
            Bind_Monthly_Invoice_data();
        }

        private void rbtn_Invoice_Sent_CheckedChanged(object sender, EventArgs e)
        {
            Bind_Monthly_Invoice_data();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void chk_Monthly_Invoice_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Monthly_Invoice.Checked == true)
            {
                for (int i = 0; i < grd_Monthly_Invoice.Rows.Count; i++)
                {
                    grd_Monthly_Invoice[0, i].Value = true;
                }
            }
            else if (chk_Monthly_Invoice.Checked == false)
            {
                for (int i = 0; i < grd_Monthly_Invoice.Rows.Count; i++)
                {
                    grd_Monthly_Invoice[0, i].Value = false;
                }
            }
        }

        private void grd_Monthly_Invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    //viewing the information
                    string invoiceno = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[2].Value.ToString();
                    int invoiceid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[13].Value.ToString());
                    int clientid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[14].Value.ToString());
                    int subprocessid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[15].Value.ToString());
                    string invoicedate = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[10].Value.ToString();
                    string invoice_cmt = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[16].Value.ToString();
                    string invoice_mon = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[17].Value.ToString();
                    Ordermanagement_01.Tax.Tax_Monthly_Invoice monthly = new Ordermanagement_01.Tax.Tax_Monthly_Invoice(userid, "VIEW", invoiceno, invoiceid, clientid, subprocessid, invoicedate, invoice_cmt, invoice_mon);
                    monthly.Show();
                }

                else if (e.ColumnIndex == 11)
                {
                    //view the reports
                    int invoiceid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[13].Value.ToString());
                    int clientid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[14].Value.ToString());
                    Ordermanagement_01.Tax.Tax_Invoice_Reports.Tax_Monthly_Invoice_rpt monthly11 = new Ordermanagement_01.Tax.Tax_Invoice_Reports.Tax_Monthly_Invoice_rpt(invoiceid, clientid);
                    monthly11.Show();
                }
                else if (e.ColumnIndex == 12)
                {
                    //email sending
                    form_loader.Start_progres();
                    DialogResult dialog = MessageBox.Show("Do you want to send Tax Monthly Invoice","Send mail Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        int invoiceid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[13].Value.ToString());
                        int clientid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[14].Value.ToString());
                        int subprocessid = int.Parse(grd_Monthly_Invoice.Rows[e.RowIndex].Cells[15].Value.ToString());
                        string invoiceno = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[2].Value.ToString();
                        string invoicedate = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[10].Value.ToString();
                        string invoice_cmt = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[16].Value.ToString();
                        string invoice_mon = grd_Monthly_Invoice.Rows[e.RowIndex].Cells[17].Value.ToString();
                        //int USER_ID, string OPERATION, string INV_NUM,int INVOICE_ID,int CLIENT_ID,int SUB_PROCESS_ID,string INVOICE_DATE,string INVOICE_COMMENTS,string INVOICE_MONTH
                        Ordermanagement_01.Tax.Tax_Invoice_Send_Email email = new Ordermanagement_01.Tax.Tax_Invoice_Send_Email(invoiceid, clientid, subprocessid, userid, invoiceno, invoicedate, invoice_cmt, invoice_mon);
                        // email.Show();
                        Hashtable htck = new Hashtable();
                        DataTable dtck = new DataTable();
                        htck.Add("@Trans", "CHECK_TAX_INVOICE_EMAIL_STATUS");
                        htck.Add("@Tax_MonthlyInvoice_Id", invoiceid);
                        dtck = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htck);
                        if (dtck.Rows.Count > 0)
                        {
                            if (dtck.Rows[0]["Email_Status"].ToString() == "True")
                            {

                                MessageBox.Show("Tax Monthly Invoice Sent successfully");
                            }
                            else
                            {
                                //MessageBox.Show("Tax Monthly Invoice ");
                            }
                        }
                        
                    }
                    Bind_Monthly_Invoice_data();
                }
            }
            
        }
        private void Bind_Monthly_Invoice_data()
        {
            Hashtable htuser = new Hashtable();

            htuser.Add("@Trans", "GET_ALL_MONTHLY_INVOICE_DETAILS");
            if (rbtn_Monthly_Not_Sent.Checked == true)
            {
                htuser.Add("@Email_Status", "False");
            }
            else if (rbtn_Invoice_Sent.Checked == true)
            {
                htuser.Add("@Email_Status", "True");
            }
            dtMonthlyuser = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htuser);
            grd_Monthly_Invoice.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_Monthly_Invoice.EnableHeadersVisualStyles = false;
            grd_Monthly_Invoice.Columns[0].Width = 36;
            grd_Monthly_Invoice.Columns[1].Width = 36;
            grd_Monthly_Invoice.Columns[2].Width = 120;
            grd_Monthly_Invoice.Columns[3].Width = 120;
            grd_Monthly_Invoice.Columns[4].Width = 150;
            grd_Monthly_Invoice.Columns[5].Width = 126;
            grd_Monthly_Invoice.Columns[6].Width = 132;
            grd_Monthly_Invoice.Columns[7].Width = 120;
            grd_Monthly_Invoice.Columns[8].Width = 100;
            grd_Monthly_Invoice.Columns[9].Width = 100;
            grd_Monthly_Invoice.Columns[10].Width = 70;

            if (dtMonthlyuser.Rows.Count > 0)
            {
                grd_Monthly_Invoice.Rows.Clear();
                for (int i = 0; i < dtMonthlyuser.Rows.Count; i++)
                {
                    grd_Monthly_Invoice.Rows.Add();
                    grd_Monthly_Invoice.Rows[i].Cells[1].Value = i + 1;

                    grd_Monthly_Invoice.Rows[i].Cells[2].Value = dtMonthlyuser.Rows[i]["Monthly_Invoice_No"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[3].Value = dtMonthlyuser.Rows[i]["Client_Name"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[4].Value = dtMonthlyuser.Rows[i]["Sub_ProcessName"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[5].Value = dtMonthlyuser.Rows[i]["No_Of_Orders"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[6].Value = dtMonthlyuser.Rows[i]["TOTAL_PAYABLE_AMOUNT"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[7].Value = dtMonthlyuser.Rows[i]["Total_Invoice_Amount"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[8].Value = dtMonthlyuser.Rows[i]["Total_Inv_Paid_Amount"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[9].Value = dtMonthlyuser.Rows[i]["Balance_Invoice_Amount"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[10].Value = dtMonthlyuser.Rows[i]["Invoice_Date"].ToString();


                    grd_Monthly_Invoice.Rows[i].Cells[13].Value = dtMonthlyuser.Rows[i]["MonthlyInvoice_Id"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[14].Value = dtMonthlyuser.Rows[i]["Client_Id"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[15].Value = dtMonthlyuser.Rows[i]["Subprocess_ID"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[16].Value = dtMonthlyuser.Rows[i]["Comments"].ToString();
                    grd_Monthly_Invoice.Rows[i].Cells[17].Value = dtMonthlyuser.Rows[i]["Month_Name"].ToString();
                }
            }
            else
            {
                grd_Monthly_Invoice.Rows.Clear();
                grd_Monthly_Invoice.DataSource = null;
            }
        }
        private void btn_Send_Monthly_Invoice_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do You want to send all Monthly Invoices", "Send Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                for (int i = 0; i < grd_Monthly_Invoice.Rows.Count; i++)
                {
                    bool ischeck = (bool)grd_Monthly_Invoice[0, i].FormattedValue;
                    if (ischeck)
                    {

                        form_loader.Start_progres();
                        int invoiceid = int.Parse(grd_Monthly_Invoice.Rows[i].Cells[13].Value.ToString());
                        int clientid = int.Parse(grd_Monthly_Invoice.Rows[i].Cells[14].Value.ToString());
                        int subprocessid = int.Parse(grd_Monthly_Invoice.Rows[i].Cells[15].Value.ToString());
                        string invoiceno = grd_Monthly_Invoice.Rows[i].Cells[2].Value.ToString();
                        string invoicedate = grd_Monthly_Invoice.Rows[i].Cells[10].Value.ToString();
                        string invoice_cmt = grd_Monthly_Invoice.Rows[i].Cells[16].Value.ToString();
                        string invoice_mon = grd_Monthly_Invoice.Rows[i].Cells[17].Value.ToString();
                        //int USER_ID, string OPERATION, string INV_NUM,int INVOICE_ID,int CLIENT_ID,int SUB_PROCESS_ID,string INVOICE_DATE,string INVOICE_COMMENTS,string INVOICE_MONTH
                        Ordermanagement_01.Tax.Tax_Invoice_Send_Email email = new Ordermanagement_01.Tax.Tax_Invoice_Send_Email(invoiceid, clientid, subprocessid, userid, invoiceno, invoicedate, invoice_cmt, invoice_mon);
                        Hashtable htck = new Hashtable();
                        DataTable dtck = new DataTable();
                        htck.Add("@Trans", "CHECK_TAX_INVOICE_EMAIL_STATUS");
                        htck.Add("@Tax_MonthlyInvoice_Id", invoiceid);
                        dtck = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htck);
                        if (dtck.Rows.Count > 0)
                        {
                            if (dtck.Rows[0]["Email_Status"].ToString() == "True")
                            {

                                mailsend = 1;
                            }
                            else
                            {
                                //MessageBox.Show("Tax Monthly Invoice ");
                            }
                        }
                    }
                }

            }
            if (mailsend == 1)
            {
                MessageBox.Show("Tax Monthly Invoice Sent successfully");
               
                mailsend = 0;
            }
            Bind_Monthly_Invoice_data();
        }

        private void btn_New_Monthlyinvoice_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Tax.Tax_Monthly_Invoice monthly_invoice = new Ordermanagement_01.Tax.Tax_Monthly_Invoice(userid, "Insert", "0000", 0, 0, 0, "", "", "");
            monthly_invoice.Show();
        }

        private void txt_Searchby_Monthly_TextChanged(object sender, EventArgs e)
        {
            if (cbo_Searchby_monthly.Text != "")
            {
                DataView dtsearch = new DataView(dtMonthlyuser);
                search = txt_Searchby_Monthly.Text.ToString();
                if (cbo_Searchby_monthly.Text == "Order Number")
                {
                    dtsearch.RowFilter = "Monthly_Invoice_No like '%" + search.ToString() + "%'";
                }
                else if (cbo_Searchby_monthly.Text == "Invoice Number")
                {
                    dtsearch.RowFilter = "Invoice_No like '%" + search.ToString() + "%'";
                }
                else if (cbo_Searchby_monthly.Text == "Invoice Date")
                {
                    dtsearch.RowFilter = "Invoice_Date like '%" + search.ToString() + "%'";
                }
                else if (cbo_Searchby_monthly.Text == "Client")
                {
                    dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%'";
                }
                else if (cbo_Searchby_monthly.Text == "Sub Client")
                {
                    dtsearch.RowFilter = "Sub_ProcessName like '%" + search.ToString() + "%'";
                }
                else if (cbo_Searchby_monthly.Text == "Received Date")
                {
                    dtsearch.RowFilter = "Recived_Date like '%" + search.ToString() + "%'";
                }
                else if (cbo_Searchby_monthly.Text == "Order Type")
                {
                    dtsearch.RowFilter = "Order_Type like '%" + search.ToString() + "%'";
                }
                DataTable dtnew = new DataTable();
                dtnew = dtsearch.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    grd_Monthly_Invoice.Rows.Clear();
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        grd_Monthly_Invoice.Rows.Add();
                        grd_Monthly_Invoice.Rows[i].Cells[1].Value = i + 1;

                        grd_Monthly_Invoice.Rows[i].Cells[2].Value = dtnew.Rows[i]["Monthly_Invoice_No"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[3].Value = dtnew.Rows[i]["Client_Name"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[4].Value = dtnew.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[5].Value = dtnew.Rows[i]["No_Of_Orders"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[6].Value = dtnew.Rows[i]["TOTAL_PAYABLE_AMOUNT"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[7].Value = dtnew.Rows[i]["Total_Invoice_Amount"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[8].Value = dtnew.Rows[i]["Total_Inv_Paid_Amount"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[9].Value = dtnew.Rows[i]["Balance_Invoice_Amount"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[10].Value = dtnew.Rows[i]["Invoice_Date"].ToString();


                        grd_Monthly_Invoice.Rows[i].Cells[13].Value = dtnew.Rows[i]["MonthlyInvoice_Id"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[14].Value = dtnew.Rows[i]["Client_Id"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[15].Value = dtnew.Rows[i]["Subprocess_ID"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[16].Value = dtnew.Rows[i]["Comments"].ToString();
                        grd_Monthly_Invoice.Rows[i].Cells[17].Value = dtnew.Rows[i]["Month_Name"].ToString();
                    }
                }
                else
                {
                    if (rbtn_TaxInvoice_NotSended.Checked == true)
                    {
                        Bind_Tax_Invoice_data();
                    }
                    else if (rbtn_TaxInvoice_Sended.Checked == true)
                    {
                        Bind_Tax_Invoice_data();
                    }
                }
            }
        }
    }
}

