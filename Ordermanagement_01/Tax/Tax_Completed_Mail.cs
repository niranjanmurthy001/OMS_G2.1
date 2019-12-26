using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.DirectoryServices;
using System.Diagnostics;


namespace Ordermanagement_01.Tax
{
    public partial class Tax_Completed_Mail : Form
    {
        DataAccess dataAccess = new DataAccess();
        Hashtable htorder = new Hashtable();
        System.Data.DataTable dtorder = new System.Data.DataTable();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        System.Data.DataTable temp = new System.Data.DataTable();
        string userid, user_role; int COUNT;
        static int Currentpageindex = 0; int pagesize = 30;
        public Tax_Completed_Mail(string User_id,string User_role)
        {
            InitializeComponent();
            userid = User_id;
            user_role = User_role;
        }
        private void GetrowTable(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtorder.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void Bind_All_tax_completed()
        {
            //grd_All_Tax_Completed_orders
            if (rbtn_TaxInvoice_NotSended.Checked == true)
            {
                htorder.Clear(); dtorder.Clear();
                htorder.Add("@Trans", "SELECT_ALL_EMAIL_NOT_SEND");
                dtorder = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htorder);
                System.Data.DataTable temptable = dtorder.Clone();
                int startindex = Currentpageindex * pagesize;
                int endindex = Currentpageindex * pagesize + pagesize;
                if (endindex > dtorder.Rows.Count)
                {
                    endindex = dtorder.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetrowTable(ref row, dtorder.Rows[i]);
                    temptable.Rows.Add(row);
                }
                if (temptable.Rows.Count > 0)
                {
                    grd_All_Tax_Completed_orders.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_All_Tax_Completed_orders.Rows.Add();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[1].Value = i + 1;
                        grd_All_Tax_Completed_orders.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[3].Value = temptable.Rows[i]["Order_Type"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[4].Value = temptable.Rows[i]["Borrower_Name"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[5].Value = temptable.Rows[i]["Address"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[6].Value = temptable.Rows[i]["Assigned_Date"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[7].Value = temptable.Rows[i]["STATE_COUNTY"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[8].Value = temptable.Rows[i]["APN"].ToString();
                        Column5.Visible = false;
                        grd_All_Tax_Completed_orders.Rows[i].Cells[12].Value = temptable.Rows[i]["Order_ID"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[13].Value = temptable.Rows[i]["Sub_ProcessId"].ToString();
                    }
                    
                }
                else
                {
                    grd_All_Tax_Completed_orders.Rows.Clear();
                }
                
            }//SELECT_ALL_EMAIL_SEND
            else if (rbtn_TaxInvoice_Sended.Checked == true)
            {
                htorder.Clear(); dtorder.Clear();
                htorder.Add("@Trans", "SELECT_ALL_EMAIL_SEND");
                dtorder = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htorder);
                System.Data.DataTable temptable = dtorder.Clone();
                int startindex = Currentpageindex * pagesize;
                int endindex = Currentpageindex * pagesize + pagesize;
                if (endindex > dtorder.Rows.Count)
                {
                    endindex = dtorder.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetrowTable(ref row, dtorder.Rows[i]);
                    temptable.Rows.Add(row);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_All_Tax_Completed_orders.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_All_Tax_Completed_orders.Rows.Add();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[1].Value = i + 1;
                        grd_All_Tax_Completed_orders.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[3].Value = temptable.Rows[i]["Order_Type"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[4].Value = temptable.Rows[i]["Borrower_Name"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[5].Value = temptable.Rows[i]["Address"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[6].Value = temptable.Rows[i]["Assigned_Date"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[7].Value = temptable.Rows[i]["STATE_COUNTY"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[8].Value = temptable.Rows[i]["APN"].ToString();
                        Column5.Visible = true;
                        grd_All_Tax_Completed_orders.Rows[i].Cells[9].Value = temptable.Rows[i]["Sending_Date"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[12].Value = temptable.Rows[i]["Order_ID"].ToString();
                        grd_All_Tax_Completed_orders.Rows[i].Cells[13].Value = temptable.Rows[i]["Sub_ProcessId"].ToString();
                    }
                }
                else
                {
                    grd_All_Tax_Completed_orders.Rows.Clear();
                }
            }
            lbl_Total_Orders.Text = dtorder.Rows.Count.ToString();
            lblRecordsStatus.Text = (Currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtorder.Rows.Count) / pagesize);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            txt_Search_Order_no.Text = "";
            Bind_All_tax_completed();
        }
        private void GetrowTable_Client(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in temp.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void Bind_filter_data()
        {
            System.Data.DataTable temptable = temp.Clone();
            int startindex = Currentpageindex * pagesize;
            int endindex = Currentpageindex * pagesize + pagesize;
            if (endindex > temp.Rows.Count)
            {
                endindex = temp.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetrowTable_Client(ref row, temp.Rows[i]);
                temptable.Rows.Add(row);
            }
            if (temptable.Rows.Count > 0)
            {
                grd_All_Tax_Completed_orders.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_All_Tax_Completed_orders.Rows.Add();

                    grd_All_Tax_Completed_orders.Rows[i].Cells[1].Value = i + 1;
                    grd_All_Tax_Completed_orders.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                    grd_All_Tax_Completed_orders.Rows[i].Cells[3].Value = temptable.Rows[i]["Order_Type"].ToString();
                    grd_All_Tax_Completed_orders.Rows[i].Cells[4].Value = temptable.Rows[i]["Borrower_Name"].ToString();
                    grd_All_Tax_Completed_orders.Rows[i].Cells[5].Value = temptable.Rows[i]["Address"].ToString();
                    grd_All_Tax_Completed_orders.Rows[i].Cells[6].Value = temptable.Rows[i]["Assigned_Date"].ToString();
                    grd_All_Tax_Completed_orders.Rows[i].Cells[7].Value = temptable.Rows[i]["STATE_COUNTY"].ToString();
                    grd_All_Tax_Completed_orders.Rows[i].Cells[8].Value = temptable.Rows[i]["APN"].ToString();
                    Column5.Visible = true;
                    if (temptable.Rows[i]["Sending_Date"].ToString() == "")
                    {
                        grd_All_Tax_Completed_orders.Rows[i].Cells[9].Value = "N/A";
                    }
                    else
                    {
                        grd_All_Tax_Completed_orders.Rows[i].Cells[9].Value = temptable.Rows[i]["Sending_Date"].ToString();
                    }
                    grd_All_Tax_Completed_orders.Rows[i].Cells[12].Value = temptable.Rows[i]["Order_ID"].ToString();
                    grd_All_Tax_Completed_orders.Rows[i].Cells[13].Value = temptable.Rows[i]["Sub_ProcessId"].ToString();

                }
            }
            lbl_Total_Orders.Text = temp.Rows.Count.ToString();
            lblRecordsStatus.Text = (Currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(temp.Rows.Count) / pagesize);
        }
        private void txt_Search_Order_no_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Order_no.Text != "")
            {
                DataView dtsearch = new DataView(dtorder);
                string search=txt_Search_Order_no.Text.ToString();
                dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%'";
                
                temp = dtsearch.ToTable();
                Bind_filter_data();
                btnFirst_Click(sender, e);
            }
            else
            {
                Bind_All_tax_completed();
            }
        }

        private void btn_Send_All_Click(object sender, EventArgs e)
        {
            if (grd_All_Tax_Completed_orders.Rows.Count > 0)
            {
                DialogResult send_all = MessageBox.Show("Do you want to send all Tax certificate mails", "Mail Confirmation", MessageBoxButtons.YesNo);
                if (send_all == DialogResult.Yes)
                {
                    form_loader.Start_progres();
                    btn_Send_All.Enabled = false;
                    //COUNT_SEND
                    Hashtable ht_ntsend = new Hashtable();
                    DataTable dt_ntsend = new DataTable();
                    ht_ntsend.Add("@Trans", "COUNT_NOT_SEND");
                    dt_ntsend = dataAccess.ExecuteSP("Sp_Tax_Order_Status", ht_ntsend);
                    if (dt_ntsend.Rows.Count > 0)
                    {
                        COUNT = int.Parse(dt_ntsend.Rows[0]["count_not_send"].ToString());
                    }
                    else
                    {
                        COUNT = 0;
                    }
                    for (int i = 0; i < grd_All_Tax_Completed_orders.Rows.Count; i++)
                    {
                        bool ischeck = (bool)grd_All_Tax_Completed_orders[0, i].FormattedValue;
                        if (ischeck)
                        {
                            try
                            {
                                int orderid = int.Parse(grd_All_Tax_Completed_orders.Rows[i].Cells[12].Value.ToString());
                                string orderno = grd_All_Tax_Completed_orders.Rows[i].Cells[2].Value.ToString();
                                string emailid = grd_All_Tax_Completed_orders.Rows[i].Cells[13].Value.ToString();
                                Ordermanagement_01.Tax.Tax_mail mail = new Ordermanagement_01.Tax.Tax_mail(orderid, userid, user_role, orderno, "Bulk", emailid);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }


                        }
                    }
                    Bind_All_tax_completed();
                    if (COUNT > grd_All_Tax_Completed_orders.Rows.Count)
                    {
                        MessageBox.Show("Mail Sent Successfully");
                    }
                    btn_Send_All.Enabled = true;
                }
                else
                {

                }
            }
        }

        private void grd_All_Tax_Completed_orders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex  != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    //view the order information
                    string Order_Id = grd_All_Tax_Completed_orders.Rows[e.RowIndex].Cells[12].Value.ToString();
                    string Order_Number = grd_All_Tax_Completed_orders.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Tax_Order_View txview = new Tax_Order_View(Order_Id, userid, Order_Number, user_role);
                    txview.Show();
                }
                if (e.ColumnIndex == 10)
                {
                    //view pdf
                    //htorder.Clear(); dtorder.Clear();
                    //htorder.Add("@Trans", "SELECT_MAX_UPLOAD_DOCUMENTS");
                    //htorder.Add("@Order_Id", int.Parse(grd_All_Tax_Completed_orders.Rows[e.RowIndex].Cells[12].Value.ToString()));
                    //dtorder = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htorder);
                    //if (dtorder.Rows.Count > 0)
                    //{
                    //    string path = dtorder.Rows[0]["Document_Path"].ToString();
                    //    System.Diagnostics.Process.Start(path);
                    //}
                    //SELECT_MAX_UPLOAD_DOCUMENTS
                }
                else if (e.ColumnIndex == 11)
                {
                    //send email
                    DialogResult dialog = MessageBox.Show("Do you want to send Tax certificate mail", "Mail Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        form_loader.Start_progres();
                        int orderid = int.Parse(grd_All_Tax_Completed_orders.Rows[e.RowIndex].Cells[12].Value.ToString());
                        string orderno = grd_All_Tax_Completed_orders.Rows[e.RowIndex].Cells[2].Value.ToString();
                        string emailid = grd_All_Tax_Completed_orders.Rows[e.RowIndex].Cells[13].Value.ToString();
                        Ordermanagement_01.Tax.Tax_mail mail = new Ordermanagement_01.Tax.Tax_mail(orderid, userid, user_role, orderno, "", emailid);

                    }
                    else
                    {

                    }
                    Bind_All_tax_completed();
                }
            }
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {
                for (int i = 0; i < grd_All_Tax_Completed_orders.Rows.Count; i++)
                {
                    grd_All_Tax_Completed_orders[0, i].Value = true;
                }
            }
            else if (chk_All.Checked == false)
            {
                for (int i = 0; i < grd_All_Tax_Completed_orders.Rows.Count; i++)
                {
                    grd_All_Tax_Completed_orders[0, i].Value = false;
                }
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
           //this.Cursor = Cursors.WaitCursor;
            if (txt_Search_Order_no.Text == "Search by Order Number.." || txt_Search_Order_no.Text == "")
            {
                Currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtorder.Rows.Count) / pagesize) - 1;
                Bind_All_tax_completed(); 
            }
            else
            {
                Currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(temp.Rows.Count) / pagesize) - 1;
                Bind_filter_data();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;

            this.Cursor = currentCursor;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;

            Currentpageindex++;
            if (txt_Search_Order_no.Text == "Search by Order Number.." || txt_Search_Order_no.Text == "")
            {
                if (Currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtorder.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;
                }
                Bind_All_tax_completed();
            }
            else
            {
                if (Currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtorder.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;
                }
                Bind_filter_data();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
           // this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
            Currentpageindex--;
            if (Currentpageindex == 0)
            {
                btnPrevious.Enabled = false;
                btnFirst.Enabled = false;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;

            }
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            if (txt_Search_Order_no.Text == "Search by Order Number.." || txt_Search_Order_no.Text == "")
            {
                Bind_All_tax_completed();
            }
            else
            {
                Bind_filter_data();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;

            Currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (txt_Search_Order_no.Text == "Search by Order Number.." || txt_Search_Order_no.Text == "")
            {
                Bind_All_tax_completed();

            }
            else
            {
                Bind_filter_data();
            }
            this.Cursor = currentCursor;
        }

        private void rbtn_TaxInvoice_NotSended_CheckedChanged(object sender, EventArgs e)
        {
            Bind_All_tax_completed();
            btnFirst_Click(sender, e);
        }

        private void rbtn_TaxInvoice_Sended_CheckedChanged(object sender, EventArgs e)
        {

            Bind_All_tax_completed();
            btnFirst_Click(sender, e);
        }

        private void Tax_Completed_Mail_Load(object sender, EventArgs e)
        {
            Bind_All_tax_completed();
        }

        private void txt_Search_Order_no_MouseEnter(object sender, EventArgs e)
        {
           

        }

    
    }
}
