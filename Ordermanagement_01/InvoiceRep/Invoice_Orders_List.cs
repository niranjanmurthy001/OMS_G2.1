using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Globalization;
using ClosedXML.Excel;
using DevExpress.XtraSplashScreen;
using System.Net;

//using PdfSharp;
//using PdfSharp.Pdf;
namespace Ordermanagement_01.Invoice
{
    public partial class Invoice_Orders_List : Form
    {


        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        CheckBox chkbox = new CheckBox();
        int Order_Id = 0;
        int userid;
        string Empname;
        int Count;
        string Gender, PaymentType, Employee_Type;
        decimal Abstractor_Cost;
        int Abstractor_Tat;
        int Order_Type_Id;
        string User_Role;
        int Sub_Process_ID;
        string Email, Alternative_Email;
        string Inv_Status;
        string[] FName;
        static int currentpageindex = 0;
        int pagesize = 50;
        static int currentmonthlyindex = 0;
        int page = 15;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int Invoice_Id;
        string View_File_Path;
        string Invoice_Status;
        DialogResult dialogResult;
        PdfCopy pdfCopyProvider = null;
        PdfImportedPage importedPage;
        string Package = "";
        string invoiceFile, searchPackage;
        int Index;
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtuser = new DataTable();
        DataTable dtMonthlyuser = new DataTable();
        string localPath = "";
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        string server = "192.168.12.33";
        string database = "TITLELOGY_NEW_OMS";
        string UserID = "sa";
        string password = "password1$";


        string Client_Order_no;

        int User_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        string Production_Date;
        private string year;
        private string month;
        private string Ftp_Domain_Name;
        private string Ftp_User_Name;
        private string Ftp_Password;
        private string Ftp_Path;
        private string mainPath;
        private string ftpfullpath;
        private string homeFolder;
        private int clientId;
        private string invoiceLocalFile;
        private string searchPackageLocalFile;
        public Invoice_Orders_List(int User_Id, string Role_id, string PRODUCTION_DATE)
        {
            userid = User_Id;
            User_Role = Role_id;
            Production_Date = PRODUCTION_DATE;
            InitializeComponent();
        }

        private void btn_New_Invoice_Click(object sender, EventArgs e)
        {

            Invoice_Order_Details invid = new Invoice_Order_Details(0, userid, "Insert", "0", User_Role);
            invid.Show();
        }
        private void GetDataRow(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtuser.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void GetDataRow_Monthly(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtMonthlyuser.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void Geridview_Bind_Invoice_Orders()
        {
            Hashtable htuser = new Hashtable();
            htuser.Add("@Trans", "GET_INVOICE_ORDER_DETAILS");
            if (rbtn_Invoice_NotSended.Checked == true)
            {
                htuser.Add("@Email_Status", "False");
            }
            else if (rbtn_Invoice_Sended.Checked == true)
            {
                htuser.Add("@Email_Status", "True");
            }
            dtuser = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
            //grd_order.Rows[0].Height= 15;
            grd_order.Columns[0].Width = 25;
            grd_order.Columns[1].Width = 36;
            grd_order.Columns[2].Width = 90;
            grd_order.Columns[3].Width = 90;
            grd_order.Columns[4].Width = 100;
            grd_order.Columns[5].Width = 100;
            grd_order.Columns[6].Width = 100;
            grd_order.Columns[7].Width = 100;
            grd_order.Columns[8].Width = 100;
            grd_order.Columns[9].Width = 70;
            grd_order.Columns[10].Width = 70;
            grd_order.Columns[11].Width = 75;
            grd_order.Columns[12].Width = 111;
            grd_order.Columns[13].Width = 50;
            grd_order.Columns[14].Width = 111;
            grd_order.Columns[15].Width = 45;
            grd_order.Columns[16].Width = 45;
            grd_order.Columns[17].Width = 50;

            System.Data.DataTable temptable = dtuser.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtuser.Rows.Count)
            {
                endindex = dtuser.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow(ref row, dtuser.Rows[i]);
                temptable.Rows.Add(row);
            }
            if (temptable.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = temptable.Rows[i]["Order_Invoice_No"].ToString();
                    if (User_Role == "1" || userid == 260 || userid == 179)
                    {
                        grd_order.Rows[i].Cells[4].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[5].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {

                        grd_order.Rows[i].Cells[4].Value = temptable.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[5].Value = temptable.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_order.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = temptable.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[8].Value = temptable.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[9].Value = temptable.Rows[i]["Search_Cost"].ToString();
                    grd_order.Rows[i].Cells[10].Value = temptable.Rows[i]["Copy_Cost"].ToString();

                    grd_order.Rows[i].Cells[11].Value = temptable.Rows[i]["CGI_Title_Land_Amount"].ToString();
                    grd_order.Rows[i].Cells[12].Value = temptable.Rows[i]["Additional_Fees"].ToString();

                    grd_order.Rows[i].Cells[13].Value = temptable.Rows[i]["Total"].ToString();
                    grd_order.Rows[i].Cells[14].Value = temptable.Rows[i]["Invoice_Date"].ToString();
                    grd_order.Rows[i].Cells[19].Value = temptable.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[20].Value = temptable.Rows[i]["Client_Id"].ToString();
                    grd_order.Rows[i].Cells[21].Value = temptable.Rows[i]["Sub_ProcessId"].ToString();
                    grd_order.Rows[i].Cells[22].Value = temptable.Rows[i]["Invoice_Id"].ToString();
                    grd_order.Rows[i].Cells[23].Value = temptable.Rows[i]["stateid"].ToString();

                }
            }
            else
            {
                grd_order.Visible = true;
                grd_order.Rows.Clear();
                grd_order.DataSource = null;
            }
            lbl_Total_Orders.Text = dtuser.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtuser.Rows.Count) / pagesize);

            // hiding the PDF View for the User Role
            if (User_Role == "2")
            {
                grd_order.Columns[15].Visible = false;
            }



        }
        private void Geridview_Bind_Monthly_Invoice_Orders()
        {
            Hashtable htuser = new Hashtable();

            htuser.Add("@Trans", "GET_ALL_MONTHLY_INVOICE_DETAILS");
            if (rbtn_Monthly_inv_Not_Sent.Checked == true)
            {
                htuser.Add("@Email_Status", "False");
            }
            else if (rbtn_Monthly_Invoice_Sent.Checked == true)
            {
                htuser.Add("@Email_Status", "True");

            }
            dtMonthlyuser = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htuser);
            grid_Monthly_Invoice.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grid_Monthly_Invoice.EnableHeadersVisualStyles = false;
            grid_Monthly_Invoice.Columns[0].Width = 36;
            grid_Monthly_Invoice.Columns[1].Width = 35;
            grid_Monthly_Invoice.Columns[2].Width = 130;
            grid_Monthly_Invoice.Columns[3].Width = 120;
            grid_Monthly_Invoice.Columns[4].Width = 150;
            grid_Monthly_Invoice.Columns[5].Width = 126;
            grid_Monthly_Invoice.Columns[6].Width = 132;
            grid_Monthly_Invoice.Columns[7].Width = 120;
            grid_Monthly_Invoice.Columns[8].Width = 100;
            grid_Monthly_Invoice.Columns[9].Width = 100;
            grid_Monthly_Invoice.Columns[10].Width = 100;
            grid_Monthly_Invoice.Columns[11].Width = 100;
            grid_Monthly_Invoice.Columns[12].Width = 100;
            grid_Monthly_Invoice.Columns[14].Width = 150;

            System.Data.DataTable temptable = dtMonthlyuser.Clone();
            int startindex = currentmonthlyindex * page;
            int endindex = currentmonthlyindex * page + page;
            if (endindex > dtMonthlyuser.Rows.Count)
            {
                endindex = dtMonthlyuser.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Monthly(ref row, dtMonthlyuser.Rows[i]);
                temptable.Rows.Add(row);
            }
            if (temptable.Rows.Count > 0)
            {
                grid_Monthly_Invoice.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grid_Monthly_Invoice.Rows.Add();
                    grid_Monthly_Invoice.Rows[i].Cells[0].Value = i + 1;

                    grid_Monthly_Invoice.Rows[i].Cells[2].Value = temptable.Rows[i]["Monthly_Invoice_No"].ToString();
                    if (User_Role == "1" || userid == 260 || userid == 179)
                    {
                        grid_Monthly_Invoice.Rows[i].Cells[3].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[4].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grid_Monthly_Invoice.Rows[i].Cells[3].Value = temptable.Rows[i]["Client_Number"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[4].Value = temptable.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grid_Monthly_Invoice.Rows[i].Cells[5].Value = temptable.Rows[i]["No_Of_Orders"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[6].Value = temptable.Rows[i]["TOTAL_PAYABLE_AMOUNT"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[7].Value = temptable.Rows[i]["Total_Invoice_Amount"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[8].Value = temptable.Rows[i]["Total_Inv_Paid_Amount"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[9].Value = temptable.Rows[i]["Balance_Invoice_Amount"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[10].Value = temptable.Rows[i]["Invoice_Date"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[13].Value = temptable.Rows[i]["MonthlyInvoice_Id"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[14].Value = temptable.Rows[i]["Client_Id"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[15].Value = temptable.Rows[i]["Subprocess_ID"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[16].Value = temptable.Rows[i]["Comments"].ToString();
                    grid_Monthly_Invoice.Rows[i].Cells[17].Value = temptable.Rows[i]["Month_Name"].ToString();
                }
            }
            else
            {
                grid_Monthly_Invoice.Visible = true;
                grid_Monthly_Invoice.Rows.Clear();
                grid_Monthly_Invoice.DataSource = null;
            }
            lbl_Montly_Total_orders.Text = dtMonthlyuser.Rows.Count.ToString();
            lbl_MonthlyRecord_status.Text = (currentmonthlyindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtMonthlyuser.Rows.Count) / page);






            //if (dtuser.Rows.Count > 0)
            //{
            //    //ex2.Visible = true;
            //    grid_Monthly_Invoice.Rows.Clear();
            //    for (int i = 0; i < dtuser.Rows.Count; i++)
            //    {
            //grid_Monthly_Invoice.Rows.Add();
            //grid_Monthly_Invoice.Rows[i].Cells[0].Value = i + 1;
            //grid_Monthly_Invoice.Rows[i].Cells[1].Value = dtuser.Rows[i]["Monthly_Invoice_No"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Name"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[3].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[4].Value = dtuser.Rows[i]["No_Of_Orders"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[5].Value = dtuser.Rows[i]["TOTAL_PAYABLE_AMOUNT"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[6].Value = dtuser.Rows[i]["Total_Invoice_Amount"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[7].Value = dtuser.Rows[i]["Total_Inv_Paid_Amount"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[8].Value = dtuser.Rows[i]["Balance_Invoice_Amount"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[9].Value = dtuser.Rows[i]["Invoice_Date"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[12].Value = dtuser.Rows[i]["MonthlyInvoice_Id"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[13].Value = dtuser.Rows[i]["Client_Id"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[14].Value = dtuser.Rows[i]["Subprocess_ID"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[15].Value = dtuser.Rows[i]["Comments"].ToString();
            //grid_Monthly_Invoice.Rows[i].Cells[16].Value = dtuser.Rows[i]["Month_Name"].ToString();


            //    }
            //    // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
            //}
            //else
            //{
            //    grid_Monthly_Invoice.Rows.Clear();
            //    grid_Monthly_Invoice.DataSource = null;
            //    // lbl_Total_Orders.Text = "0";
            //    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
            //    //grd_Admin_orders.DataBind();
            //}



        }
        private void Invoice_Orders_List_Load(object sender, EventArgs e)
        {
            rbtn_Invoice_NotSended_CheckedChanged(sender, e);
            rbtn_Monthly_inv_Not_Sent_CheckedChanged(sender, e);
            AddParent();
            if (User_Role == "1" || userid == 260 || userid == 179)
            {
                dbc.BindClientName_rpt(ddl_Client_Name);
                dbc.BindClientName(ddlClient);
            }
            else
            {
                dbc.BindClientNo_for_Report(ddl_Client_Name);
                dbc.BindClientNo_for_Report(ddlClient);
            }
            btnFirst_Click(sender, e);
            txt_From_date.Value = txt_Fromdate.Value = DateTime.Now;
            txt_To_date.Value = txt_Todate.Value = DateTime.Now;

            //if (User_Role == "2")
            //{
            Tab_Control.TabPages.Remove(tabPage1);
            //}
            var dt = dbc.Get_Month_Year();
            if (dt != null && dt.Rows.Count > 0)
            {
                year = dt.Rows[0]["Year"].ToString();
                month = dt.Rows[0]["Month"].ToString();
            }

            DataTable dt_ftp_Details = dbc.Get_Ftp_Details();

            if (dt_ftp_Details.Rows.Count > 0)
            {
                Ftp_Domain_Name = dt_ftp_Details.Rows[0]["Ftp_Host_Name"].ToString();

                Ftp_User_Name = dt_ftp_Details.Rows[0]["Ftp_User_Name"].ToString();

                string Ftp_pass = dt_ftp_Details.Rows[0]["Ftp_Password"].ToString();

                if (Ftp_pass != "")
                {
                    Ftp_Password = dbc.Decrypt(Ftp_pass);
                }
            }
            else
            {
                MessageBox.Show("Ftp File Path was not found; You cannot upload the documents please check with administrator");
            }

        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 15)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString());
                int client_id = int.Parse(grd_order.Rows[e.RowIndex].Cells[20].Value.ToString());
                int State = int.Parse(grd_order.Rows[e.RowIndex].Cells[23].Value.ToString());

                InvoiceRep.Invoice_Order_Preview OrderEntry = new InvoiceRep.Invoice_Order_Preview(Order_Id, client_id, 0, User_Role, State);
                OrderEntry.Show();
                // cProbar.stopProgress();
            }
            else if (e.ColumnIndex == 2)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString());
                string Invoice_No = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                Invoice_Order_Details inv = new Invoice_Order_Details(Order_Id, userid, "Update", Invoice_No, User_Role);
                inv.Show();
                //cProbar.stopProgress();
            }
            else if (e.ColumnIndex == 17)
            {

                dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    Sub_Process_ID = int.Parse(grd_order.Rows[e.RowIndex].Cells[21].Value.ToString());
                    int invoice_ID = int.Parse(grd_order.Rows[e.RowIndex].Cells[22].Value.ToString());
                    string Order_Number = grd_order.Rows[e.RowIndex].Cells[2].Value.ToString();
                    int order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString());
                    // Get_Email();

                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();
                    htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
                    htin.Add("@Subprocess_ID", Sub_Process_ID);
                    dtin = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htin);
                    if (dtin.Rows.Count > 0)
                    {
                        Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

                    }
                    if (Inv_Status == "False")
                    {

                        dialogResult = MessageBox.Show("Invoice is Disabled, Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);


                        if (dialogResult == DialogResult.Yes)
                        {
                            form_loader.Start_progres();
                            //cProbar.startProgress();
                            InvoiceRep.Invoice_Send_Email inv = new InvoiceRep.Invoice_Send_Email(Order_Number, userid, order_Id, invoice_ID, Inv_Status, "Order_Invoice", Sub_Process_ID);
                            // inv.Show();
                            Geridview_Bind_Invoice_Orders();
                            //cProbar.stopProgress();
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do something else
                        }
                    }
                    else if (Inv_Status == "True")
                    {
                        InvoiceRep.Invoice_Send_Email inv = new InvoiceRep.Invoice_Send_Email(Order_Number, userid, order_Id, invoice_ID, Inv_Status, "Order_Invoice", Sub_Process_ID);
                        //inv.Show();
                        Geridview_Bind_Invoice_Orders();


                    }

                    //cProbar.stopProgress();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }

            }
            else if (e.ColumnIndex == 16)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString());
                Sub_Process_ID = int.Parse(grd_order.Rows[e.RowIndex].Cells[21].Value.ToString());
                Invoice_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[22].Value.ToString());
                clientId = Convert.ToInt32(grd_order.Rows[e.RowIndex].Cells[20].Value);
                if (clientId == 11)
                {
                    Export_Report();
                    Merge_Document_2();
                }
                else
                {
                    string dirTemp = "C:\\OMS\\Temp";
                    if (!Directory.Exists(dirTemp))
                    {
                        var dirInfo = Directory.CreateDirectory(dirTemp);
                    }

                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();
                    htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
                    htsearch.Add("@Order_ID", Order_Id);
                    dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
                    if (dtsearch.Rows.Count > 0)
                    {
                        searchPackage = dtsearch.Rows[0]["New_Document_Path"].ToString();
                        searchPackageLocalFile = dirTemp + "\\" + Order_Id + "-Search-Package.pdf";
                        Download_Ftp_File(Order_Id + "-Search-Package.pdf", searchPackage);

                        if (localPath != "")
                        {
                            System.Diagnostics.Process.Start(localPath);
                        }
                    }


                    //MessageBox.Show("Invoice Will Not Merge for this client");


                }
                // Merge_Document();
                //Merge_Document_2();

                // cProbar.stopProgress();
            }
            else if (e.ColumnIndex == 18)
            {
                dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString());
                    Sub_Process_ID = int.Parse(grd_order.Rows[e.RowIndex].Cells[21].Value.ToString());
                    Hashtable htupdate = new Hashtable();
                    DataTable dtupdate = new DataTable();
                    htupdate.Add("@Trans", "DELETE");
                    htupdate.Add("@Order_ID", Order_Id);
                    dtupdate = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htupdate);

                    Geridview_Bind_Invoice_Orders();
                    txt_orderserach_Number.Text = "";
                    //cProbar.stopProgress();
                }
            }
        }


        public void Get_Email()
        {


            Hashtable htdate = new Hashtable();
            System.Data.DataTable dtdate = new System.Data.DataTable();
            htdate.Add("@Trans", "GET_EMAIL_OF_SUB_CLIENT");
            htdate.Add("@Subprocess_ID", Sub_Process_ID);
            dtdate = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htdate);
            if (dtdate.Rows.Count > 0)
            {

                Email = dtdate.Rows[0]["Email"].ToString();
                Alternative_Email = dtdate.Rows[0]["Alternative_Email"].ToString();


            }
            else
            {

                Email = "";
                Alternative_Email = "";
            }


        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Geridview_Bind_Invoice_Orders();
            txt_orderserach_Number.Text = "";
        }
        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            this.Cursor = currentCursor;
        }
        private void txt_orderserach_Number_TextChanged(object sender, EventArgs e)
        {
            Search_Record();
            First_Page();
        }
        private void GetDataRow_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void GetDataRow_Monthly_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt1.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void Search_Record()
        {

            DataView dtsearch = new DataView(dtuser);

            if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search...")
            {
                string search = txt_orderserach_Number.Text;

                //dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%' or Client_Name like '%" + search.ToString() + "%' or  Sub_ProcessName like '%" + search.ToString() + "%' or Order_Type like '%" + search.ToString() + "%'  or STATECOUNTY like '%" + search.ToString() + "%' or Date like '%" + search.ToString() + "%' or Order_Cost like '%" + search.ToString() + "%' or Order_Cost_Date like '%" + search.ToString() + "%' or  Order_ID like '%" + search.ToString() +"%' or  Sub_ProcessId like '%" + search.ToString() +"%' or  Order_Cost_Id like '%" + search.ToString() + "%'";
                //dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%' or Client_Name like '%" + search.ToString() + "%' or convert(Client_Number,'System.String') like '%" + search.ToString() + "%' or  Sub_ProcessName like '%" + search.ToString() + "%' or  Convert(Subprocess_Number,'System.string') like '%" + search.ToString() + "%' or Order_Type like '%"
                //    + search.ToString() + "%' or Order_Invoice_No like '%" + search.ToString() +
                //"%' or Invoice_Date like '%" + search.ToString() +
                //"%' or Date like '%" + search.ToString() + "%'";

                dtsearch.RowFilter = " Client_Order_Number like '%" + search.ToString() + "%' or Client_Name like '%" + search.ToString() + "%' or Convert(Client_Number, 'System.String') like '%" + search.ToString() + "%' or  Sub_ProcessName like '%" + search.ToString() + "%' or Convert(Subprocess_Number, 'System.String') like '%" + search.ToString() + "%'  or Order_Type like '%"
                    + search.ToString() + "%' or Order_Invoice_No like '%" + search.ToString() +
                "%' or Invoice_Date like '%" + search.ToString() +
                "%' or Date like '%" + search.ToString() + "%' ";


                dt1 = dtsearch.ToTable();

                System.Data.DataTable temptable = dt1.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dt1.Rows.Count)
                {
                    endindex = dt1.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetDataRow_Monthly_Search(ref row, dt1.Rows[i]);
                    temptable.Rows.Add(row);
                }
                if (temptable.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = temptable.Rows[i]["Order_Invoice_No"].ToString();
                        if (User_Role == "1" || userid == 260 || userid == 179)
                        {
                            grd_order.Rows[i].Cells[4].Value = temptable.Rows[i]["Client_Name"].ToString();
                            grd_order.Rows[i].Cells[5].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                        }

                        else
                        {
                            grd_order.Rows[i].Cells[4].Value = temptable.Rows[i]["Client_Number"].ToString();
                            grd_order.Rows[i].Cells[5].Value = temptable.Rows[i]["Subprocess_Number"].ToString();

                        }
                        grd_order.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[7].Value = temptable.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[8].Value = temptable.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[9].Value = temptable.Rows[i]["Search_Cost"].ToString();
                        grd_order.Rows[i].Cells[10].Value = temptable.Rows[i]["Copy_Cost"].ToString();
                        grd_order.Rows[i].Cells[11].Value = temptable.Rows[i]["CGI_Title_Land_Amount"].ToString();
                        grd_order.Rows[i].Cells[12].Value = temptable.Rows[i]["Additional_Fees"].ToString();
                        grd_order.Rows[i].Cells[13].Value = temptable.Rows[i]["Total"].ToString();
                        grd_order.Rows[i].Cells[14].Value = temptable.Rows[i]["Invoice_Date"].ToString();
                        grd_order.Rows[i].Cells[19].Value = temptable.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[20].Value = temptable.Rows[i]["Client_Id"].ToString();
                        grd_order.Rows[i].Cells[21].Value = temptable.Rows[i]["Sub_ProcessId"].ToString();
                        grd_order.Rows[i].Cells[22].Value = temptable.Rows[i]["Invoice_Id"].ToString();
                        grd_order.Rows[i].Cells[23].Value = temptable.Rows[i]["stateid"].ToString();

                    }
                }
                else
                {
                    grd_order.Visible = true;
                    grd_order.Rows.Clear();
                    grd_order.DataSource = null;
                }
                lbl_Total_Orders.Text = dt1.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt1.Rows.Count) / pagesize);
            }
            //foreach (DataGridViewRow row in grd_order.Rows)
            //{
            //    if (txt_orderserach_Number.Text != "")
            //    {
            //        if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search..." && cbo_colmn.Text == "Client" && row.Cells[3].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
            //        {
            //            row.Visible = true;
            //        }
            //        else if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search..." && cbo_colmn.Text == "Sub Client" && row.Cells[4].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
            //        {
            //            row.Visible = true;
            //        }
            //        else if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search..." && cbo_colmn.Text == "Received Date" && row.Cells[6].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
            //        {
            //            row.Visible = true;
            //        }
            //        else if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search..." && cbo_colmn.Text == "Order Type" && row.Cells[5].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
            //        {
            //            row.Visible = true;
            //        }
            //        else if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search..." && cbo_colmn.Text == "Invoice Date" && row.Cells[11].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
            //        {
            //            row.Visible = true;
            //        }

            //        else if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search..." && cbo_colmn.Text == "Order Number" && row.Cells[1].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
            //        {
            //            row.Visible = true;
            //        }
            //        else if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search..." && cbo_colmn.Text == "Invoice Number" && row.Cells[2].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
            //        {
            //            row.Visible = true;
            //        }

            //        else
            //        {
            //            row.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        row.Visible = true;

            //    }
            //}
        }

        private void rbtn_Invoice_NotSended_CheckedChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Invoice_Orders();
        }

        private void rbtn_Invoice_Sended_CheckedChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Invoice_Orders();
        }
        private void Download_Ftp_File(string p, string Source_Path)
        {
            try
            {
                localPath = "";
                   //string File_Name = p;
                   //File_Name = File_Name.Replace("%20"," ");
                   FtpWebRequest reqFTP;
                string Folder_Path = "C:\\OMS\\Temp";
                if (!Directory.Exists(Folder_Path))
                {
                    Directory.CreateDirectory(Folder_Path);
                }
                //  string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + p;
                 localPath = "C:\\OMS\\Temp" + "\\" + p;
                FileStream outputStream = new FileStream(localPath, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Source_Path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.CopyTo(outputStream);
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                //   System.Diagnostics.Process.Start(localPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Problem in Downloading Files please Check with Administrator");
            }
        }
        private void CreateDirectory(string mainPath, string directoryPath)
        {
            try
            {
                string Ftp_Host_Name = Ftp_Domain_Name;
                Ftp_Path = Ftp_Host_Name + "/Ftp_Application_Files/OMS/" + mainPath + "";

                string[] folderArray = directoryPath.Split('/');
                string folderName = "";
                for (int i = 0; i < folderArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(folderArray[i]))
                    {

                        try
                        {
                            folderName = string.IsNullOrEmpty(folderName) ? folderArray[i] : folderName + "/" + folderArray[i];
                            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create("ftp://" + Ftp_Path + "/" + folderName);
                            ftp.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                            FtpWebResponse CreateForderResponse = (FtpWebResponse)ftp.GetResponse();

                            if (CreateForderResponse.StatusCode == FtpStatusCode.PathnameCreated)
                            {

                            }
                        }
                        catch (WebException ex)
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void Export_Report()
        {
            string dirTemp = "C:\\OMS\\Temp";
            if (!Directory.Exists(dirTemp))
            {
                var dirInfo = Directory.CreateDirectory(dirTemp);
            }


            if (clientId == 11)
            {
                rptDoc = new InvoiceRep.InvReport.Invoice_Report();


                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_ID", Order_Id);
                ExportOptions CrExportOptions;
                string Invoice_Order_Number = Order_Number.ToString();
                string File_Name = "" + Order_Number + ".pdf";


                Hashtable htpath = new Hashtable();
                DataTable dtpath = new DataTable();
                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();
                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Order_Id", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htcheck);
                int check;
                if (dtcheck.Rows.Count > 0)
                {
                    check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {
                    check = 0;
                }
                if (check == 0)
                {
                    string sourcePath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Invoice_Reports/Invoice.pdf";

                    Download_Ftp_File("Invoice.pdf", sourcePath);
                    FileInfo newFile = new FileInfo(dirTemp + "\\Invoice.pdf");

                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();

                    PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
                    CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
                    CrExportOptions = rptDoc.ExportOptions;
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    rptDoc.Export();


                    homeFolder = year + "/" + month + "/" + clientId + "/" + Order_Id + "";
                    mainPath = "Invoice_Reports";
                    ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/" + mainPath + "/" + homeFolder + "";
                    CreateDirectory(mainPath, homeFolder);

                    string ftpUploadFullPath = ftpfullpath + "/" + File_Name;

                    FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP 
                    ftpReq.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password); // Credentials  
                    ftpReq.Method = WebRequestMethods.Ftp.ListDirectory;
                    FtpWebResponse response = (FtpWebResponse)ftpReq.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    HashSet<string> directories = new HashSet<string>(); // create list to store directories.   
                    string line = streamReader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        directories.Add(line); // Add Each Directory to the List.  
                        line = streamReader.ReadLine();
                    }
                    if (!directories.Contains(File_Name))
                    {
                        FtpWebRequest ftpUpLoadFile = (FtpWebRequest)WebRequest.Create(ftpUploadFullPath);
                        ftpUpLoadFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                        ftpUpLoadFile.KeepAlive = true;
                        ftpUpLoadFile.UseBinary = true;
                        ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;
                        Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                        FileStream stream = new FileStream(dirTemp + "\\Invoice.pdf", FileMode.Open);
                        stream.CopyTo(ftpstream);
                        ftpstream.Dispose();
                        stream.Dispose();
                        htpath.Add("@Trans", "INSERT");
                        htpath.Add("@Order_Id", Order_Id);
                        htpath.Add("@Invoice_Id", Invoice_Id);
                        htpath.Add("@Document_Path", ftpUploadFullPath);
                        dtpath = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htpath);
                    }
                    else
                    {
                        FtpWebRequest ftpDeleteFile = (FtpWebRequest)WebRequest.Create(ftpUploadFullPath);
                        ftpDeleteFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                        ftpDeleteFile.KeepAlive = true;
                        ftpDeleteFile.UseBinary = true;
                        ftpDeleteFile.Method = WebRequestMethods.Ftp.DeleteFile;
                        FtpWebResponse deleteResponse = (FtpWebResponse)ftpDeleteFile.GetResponse();
                        if (deleteResponse.StatusCode == FtpStatusCode.FileActionOK)
                        {
                            FtpWebRequest ftpUpLoadFile = (FtpWebRequest)WebRequest.Create(ftpUploadFullPath);
                            ftpUpLoadFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                            ftpUpLoadFile.KeepAlive = true;
                            ftpUpLoadFile.UseBinary = true;
                            ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;
                            Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                            FileStream stream = new FileStream(dirTemp + "\\Invoice.pdf", FileMode.Open);
                            stream.CopyTo(ftpstream);
                            ftpstream.Dispose();
                            stream.Dispose();
                        }
                    }
                }
                else
                {
                    Hashtable htgetpath = new Hashtable();
                    DataTable dtgetpath = new DataTable();
                    htgetpath.Add("@Trans", "GET_PATH");
                    htgetpath.Add("@Order_Id", Order_Id);
                    dtgetpath = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htgetpath);

                    if (dtgetpath.Rows.Count > 0)
                    {
                        View_File_Path = dtgetpath.Rows[0]["New_Document_Path"].ToString();
                    }
                    string existingFile = Order_Number + "-Invoice.pdf";
                    Download_Ftp_File(existingFile, View_File_Path);
                    FileInfo newFile = new FileInfo(dirTemp + "\\" + existingFile);

                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();

                    PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
                    CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
                    CrExportOptions = rptDoc.ExportOptions;
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    rptDoc.Export();

                    FtpWebRequest ftpUpLoadFile = (FtpWebRequest)WebRequest.Create(View_File_Path);
                    ftpUpLoadFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                    ftpUpLoadFile.KeepAlive = true;
                    ftpUpLoadFile.UseBinary = true;
                    ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;
                    Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                    FileStream stream = new FileStream(dirTemp + "\\" + existingFile, FileMode.Open);
                    stream.CopyTo(ftpstream);
                    ftpstream.Dispose();
                    stream.Dispose();
                }
            }

        }

        //public void Merge_Document()
        //{
        //    Hashtable htsearch = new Hashtable();
        //    DataTable dtsearch = new DataTable();
        //    htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
        //    htsearch.Add("@Order_ID", Order_Id);
        //    dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);

        //    Hashtable htinvoice = new Hashtable();
        //    DataTable dtinvoice = new DataTable();
        //    htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
        //    htinvoice.Add("@Order_ID", Order_Id);
        //    dtinvoice = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htinvoice);
        //    Hashtable htin = new Hashtable();
        //    DataTable dtin = new DataTable();
        //    htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
        //    htin.Add("@Subprocess_ID", Sub_Process_ID);
        //    dtin = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htin);
        //    if (dtin.Rows.Count > 0)
        //    {
        //        Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

        //    }
        //    DataSet ds = new DataSet();
        //    ds.Clear();

        //    if (Inv_Status == "True")
        //    {
        //        ds.Tables.Add(dtinvoice);
        //        ds.Merge(dtsearch);
        //    }
        //    else if (Inv_Status == "False")
        //    {

        //        ds.Tables.Add(dtsearch);
        //    }


        //    if (dtsearch.Rows.Count < 0)
        //    {


        //        dialogResult = MessageBox.Show("Search Package is Not added,Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);

        //        if (dialogResult == DialogResult.Yes)
        //        {
        //            List<PdfReader> readerList = new List<PdfReader>();
        //            foreach (DataTable table in ds.Tables)
        //            {


        //                foreach (DataRow gvrow1 in table.Rows)
        //                {


        //                    string path = gvrow1["New_Document_Path"].ToString();
        //                    FileStream fs = new FileStream(path, FileMode.Open);
        //                    PdfReader pf = new PdfReader(fs);
        //                    readerList.Add(pf);

        //                }
        //            }


        //            //Define a new output document and its size, type
        //            Document document = new Document();

        //            //Get instance response output stream to write output file.
        //            string outPutFilePath = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf";

        //            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.Create));


        //            document.Open();

        //            foreach (PdfReader reader in readerList)
        //            {
        //                for (int i = 1; i <= reader.NumberOfPages; i++)
        //                {
        //                    PdfImportedPage page = writer.GetImportedPage(reader, i);

        //                    document.Add(iTextSharp.text.Image.GetInstance(page));


        //                }
        //            }
        //            document.Close();

        //            FName = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf".Split('\\');
        //            string Source_Path = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf";
        //            System.IO.Directory.CreateDirectory(@"C:\temp");

        //            File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
        //            System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);
        //        }
        //        else if (dialogResult == DialogResult.No)
        //        {
        //            this.Close();
        //        }

        //    }
        //    else if (dtsearch.Rows.Count > 0)
        //    {

        //        List<PdfReader> readerList = new List<PdfReader>();
        //        foreach (DataTable table in ds.Tables)
        //        {


        //            foreach (DataRow gvrow1 in table.Rows)
        //            {


        //                string path = gvrow1["Document_Path"].ToString();
        //                FileStream fs = new FileStream(path, FileMode.Open);
        //                PdfReader pf = new PdfReader(fs);
        //                readerList.Add(pf);

        //            }
        //        }


        //        //Define a new output document and its size, type
        //        Document document = new Document(PageSize.A4, 0, 0, 0, 0);
        //        //Get instance response output stream to write output file.
        //        string outPutFilePath = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf";

        //        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.Create));


        //        document.Open();

        //        foreach (PdfReader reader in readerList)
        //        {
        //            for (int i = 1; i <= reader.NumberOfPages; i++)
        //            {
        //                PdfImportedPage page = writer.GetImportedPage(reader, i);
        //                document.Add(iTextSharp.text.Image.GetInstance(page));
        //            }
        //        }
        //        document.Close();
        //        FName = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf".Split('\\');
        //        string Source_Path = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf";
        //        System.IO.Directory.CreateDirectory(@"C:\temp");
        //        File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
        //        System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);
        //    }
        //    else
        //    {

        //        MessageBox.Show("Search package is not uploaded check it");
        //    }


        //}
        public void Merge_Document_2()
        {
            string dirTemp = "C:\\OMS\\Temp";
            if (!Directory.Exists(dirTemp))
            {
                var dirInfo = Directory.CreateDirectory(dirTemp);
            }
            string sourcePath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Invoice_Reports/Invoicemerge.pdf";

            Download_Ftp_File("Invoicemerge.pdf", sourcePath);

            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_ID", Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
            if (dtsearch.Rows.Count > 0)
            {
                searchPackage = dtsearch.Rows[0]["New_Document_Path"].ToString();
                searchPackageLocalFile = dirTemp + "\\" + Order_Id + "-Search-Package.pdf";
                Download_Ftp_File(Order_Id + "-Search-Package.pdf", searchPackage);
            }
            Hashtable htinvoice = new Hashtable();
            DataTable dtinvoice = new DataTable();
            htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
            htinvoice.Add("@Order_ID", Order_Id);
            dtinvoice = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htinvoice);
            if (dtinvoice.Rows.Count > 0)
            {
                invoiceFile = dtinvoice.Rows[0]["New_Document_Path"].ToString();
                invoiceLocalFile = dirTemp + "\\" + Order_Id + "-Invoice.pdf";
                Download_Ftp_File(Order_Id + "-Invoice.pdf", invoiceFile);
            }

            Hashtable htin = new Hashtable();
            DataTable dtin = new DataTable();
            htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
            htin.Add("@Subprocess_ID", Sub_Process_ID);
            dtin = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htin);
            if (dtin.Rows.Count > 0)
            {
                Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();
            }
            if (Inv_Status == "True")
            {
                if (dtsearch.Rows.Count > 0)
                {
                    Package = "InvoiceAndSearch";
                    Merge_Invoice_Search();
                    System.Diagnostics.Process.Start(dirTemp + "\\Invoicemerge.pdf");
                }
                else
                {
                    MessageBox.Show("SearchPackage is Not Added Please Check it");
                }
            }
            else if (Inv_Status == "False")
            {
                if (dtsearch.Rows.Count > 0)
                {
                    Package = "Search";
                    Merge_Invoice_Search();
                    System.Diagnostics.Process.Start(dirTemp + "\\Invoicemerge.pdf");
                }
                else
                {
                    MessageBox.Show("Search package is not uploaded check it");
                }
            }
        }

        public void Merge_Invoice_Search()
        {
            if (Inv_Status == "True" && Package == "InvoiceAndSearch")
            {
                Index = 3;
            }
            else if (Inv_Status == "False" && Package == "Search")
            {
                Index = 2;
            }
            string[] lstFiles = new string[Index];
            if (Inv_Status == "True" && Package == "InvoiceAndSearch")
            {
                lstFiles[0] = invoiceLocalFile;
                lstFiles[1] = searchPackageLocalFile;
            }
            else if (Inv_Status == "False" && Package == "Search")
            {
                lstFiles[0] = searchPackageLocalFile;
            }
            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            string outputPdfPath = "C:\\OMS\\Temp\\Invoicemerge.pdf";


            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new FileStream(outputPdfPath, FileMode.Create));

            //Open the output file
            sourceDocument.Open();

            try
            {
                //Loop through the files list
                for (int f = 0; f < lstFiles.Length - 1; f++)
                {
                    int pages = get_pageCcount(lstFiles[f]);
                    reader = new PdfReader(lstFiles[f]);
                    //Add pages of current file
                    for (int i = 1; i <= pages; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }
                    reader.Close();
                }
                //At the end save the output file
                sourceDocument.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int get_pageCcount(string file)
        {
            PdfReader pdfReader = new PdfReader(File.OpenRead(file));
            int numberOfPages = pdfReader.NumberOfPages;
            return numberOfPages;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ddl_Month_Order_Category_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Search_MonthlyInvoice_Record()
        {
            //grid_Monthly_Invoice.Rows.Add();
            //        grid_Monthly_Invoice.Rows[i].Cells[0].Value = i + 1;
            //        grid_Monthly_Invoice.Rows[i].Cells[1].Value = temptable.Rows[i]["Monthly_Invoice_No"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Name"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[3].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[4].Value = temptable.Rows[i]["No_Of_Orders"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[5].Value = temptable.Rows[i]["TOTAL_PAYABLE_AMOUNT"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[6].Value = temptable.Rows[i]["Total_Invoice_Amount"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[7].Value = temptable.Rows[i]["Total_Inv_Paid_Amount"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[8].Value = temptable.Rows[i]["Balance_Invoice_Amount"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[9].Value = temptable.Rows[i]["Invoice_Date"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[12].Value = temptable.Rows[i]["MonthlyInvoice_Id"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[13].Value = temptable.Rows[i]["Client_Id"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[14].Value = temptable.Rows[i]["Subprocess_ID"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[15].Value = temptable.Rows[i]["Comments"].ToString();
            //        grid_Monthly_Invoice.Rows[i].Cells[16].Value = temptable.Rows[i]["Month_Name"].ToString();


            DataView dtsearch = new DataView(dtMonthlyuser);
            if (txt_Monthly_Order_Search.Text != "" && txt_Monthly_Order_Search.Text != "Search..")
            {
                string search = txt_Monthly_Order_Search.Text.ToString();
                dtsearch.RowFilter = "Monthly_Invoice_No like '%" + search.ToString() + "%' or Client_Name like '%" + search.ToString() + "%' or Sub_ProcessName like '%" + search.ToString() + "%' or Invoice_Date like '%" + search.ToString() + "%'";

                dt = dtsearch.ToTable();

                System.Data.DataTable temptable = dt.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dt.Rows.Count)
                {
                    endindex = dt.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetDataRow_Search(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }
                if (temptable.Rows.Count > 0)
                {
                    grid_Monthly_Invoice.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grid_Monthly_Invoice.Rows.Add();
                        grid_Monthly_Invoice.Rows[i].Cells[0].Value = i + 1;
                        grid_Monthly_Invoice.Rows[i].Cells[2].Value = temptable.Rows[i]["Monthly_Invoice_No"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[3].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[4].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[5].Value = temptable.Rows[i]["No_Of_Orders"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[6].Value = temptable.Rows[i]["TOTAL_PAYABLE_AMOUNT"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[7].Value = temptable.Rows[i]["Total_Invoice_Amount"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[8].Value = temptable.Rows[i]["Total_Inv_Paid_Amount"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[9].Value = temptable.Rows[i]["Balance_Invoice_Amount"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[10].Value = temptable.Rows[i]["Invoice_Date"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[13].Value = temptable.Rows[i]["MonthlyInvoice_Id"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[14].Value = temptable.Rows[i]["Client_Id"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[15].Value = temptable.Rows[i]["Subprocess_ID"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[16].Value = temptable.Rows[i]["Comments"].ToString();
                        grid_Monthly_Invoice.Rows[i].Cells[17].Value = temptable.Rows[i]["Month_Name"].ToString();
                    }
                }
                else
                {
                    grid_Monthly_Invoice.Visible = true;
                    grid_Monthly_Invoice.Rows.Clear();
                    grid_Monthly_Invoice.DataSource = null;
                }
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
            }
        }


        private void txt_Monthly_Order_Search_TextChanged(object sender, EventArgs e)
        {
            Search_MonthlyInvoice_Record();
            First_Page();
            //foreach (DataGridViewRow row in grid_Monthly_Invoice.Rows)
            //{
            //    if (txt_Monthly_Order_Search.Text != "")
            //    {

            //        if (txt_Monthly_Order_Search.Text != "" && ddl_Month_Order_Category.Text == "Invoice Number" && row.Cells[1].Value.ToString().StartsWith(txt_Monthly_Order_Search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;

            //        }
            //        else if (txt_Monthly_Order_Search.Text != "" && ddl_Month_Order_Category.Text == "Client" && row.Cells[2].Value.ToString().StartsWith(txt_Monthly_Order_Search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;

            //        }
            //        else if (txt_Monthly_Order_Search.Text != "" && ddl_Month_Order_Category.Text == "Sub Client" && row.Cells[3].Value.ToString().StartsWith(txt_Monthly_Order_Search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;

            //        }
            //        else if (txt_Monthly_Order_Search.Text != "" && ddl_Month_Order_Category.Text == "Invoice Date" && row.Cells[9].Value.ToString().StartsWith(txt_Monthly_Order_Search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;

            //        }
            //        else
            //        {
            //            row.Visible = false;

            //        }



            //    }
            //    else
            //    {

            //        row.Visible = true;
            //    }
            //}
        }

        private void btn_Monthly_Invoice_Click(object sender, EventArgs e)
        {
            InvoiceRep.Invoice_Monthly mon = new InvoiceRep.Invoice_Monthly(userid, "Insert", "0000", 0, 0, 0, "", "", "", User_Role);
            mon.Show();
        }

        private void rbtn_Monthly_inv_Not_Sent_CheckedChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Monthly_Invoice_Orders();
        }

        private void rbtn_Monthly_Invoice_Sent_CheckedChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Monthly_Invoice_Orders();
        }

        private void grid_Monthly_Invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 11)
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    int Client_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[14].Value.ToString());
                    Sub_Process_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[15].Value.ToString());
                    int Invoice_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[13].Value.ToString());
                    Ordermanagement_01.InvoiceRep.Invoice_Monthly_Order_Preview OrderEntry = new Ordermanagement_01.InvoiceRep.Invoice_Monthly_Order_Preview(Invoice_ID, Sub_Process_ID, User_Role, Client_ID);
                    OrderEntry.Show();
                    //cProbar.stopProgress();
                }
                else if (e.ColumnIndex == 2)
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    int Invoice_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[13].Value.ToString());
                    int Client_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[14].Value.ToString());
                    Sub_Process_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[15].Value.ToString());
                    string Invoice_No = grid_Monthly_Invoice.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string Invoice_Date = grid_Monthly_Invoice.Rows[e.RowIndex].Cells[8].Value.ToString();
                    string Invoice_Comments = grid_Monthly_Invoice.Rows[e.RowIndex].Cells[16].Value.ToString();
                    InvoiceRep.Invoice_Monthly inv = new InvoiceRep.Invoice_Monthly(userid, "Update", Invoice_No, Invoice_ID, Client_ID, Sub_Process_ID, Invoice_Date, Invoice_Comments, grid_Monthly_Invoice.Rows[e.RowIndex].Cells[17].Value.ToString(), User_Role);
                    inv.Show();
                    //cProbar.stopProgress();
                }
                else if (e.ColumnIndex == 12)
                {

                    dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        form_loader.Start_progres();
                        //cProbar.startProgress();

                        int invoice_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[13].Value.ToString());
                        Sub_Process_ID = int.Parse(grid_Monthly_Invoice.Rows[e.RowIndex].Cells[15].Value.ToString());

                        Get_Email();

                        Hashtable htin = new Hashtable();
                        DataTable dtin = new DataTable();
                        htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
                        htin.Add("@Subprocess_ID", Sub_Process_ID);
                        dtin = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htin);
                        if (dtin.Rows.Count > 0)
                        {
                            Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

                        }

                        InvoiceRep.Invoice_Send_Email inv = new InvoiceRep.Invoice_Send_Email("00", userid, 0, invoice_ID, "", "Monthly_Invoice", Sub_Process_ID);

                        Geridview_Bind_Monthly_Invoice_Orders();



                        // cProbar.stopProgress();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }

                }
            }
        }

        private void btn_Monthly_Refresh_Click(object sender, EventArgs e)
        {
            Geridview_Bind_Monthly_Invoice_Orders();
            txt_Monthly_Order_Search.Text = "";
        }

        private void txt_orderserach_Number_Enter(object sender, EventArgs e)
        {
            Search_Record();
        }

        private void tvwRightSide_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }



        public void Logon_To_Crystal()
        {

            crConnectionInfo.ServerName = server;
            crConnectionInfo.DatabaseName = database;
            crConnectionInfo.UserID = UserID;
            crConnectionInfo.Password = password;
            CrTables = rptDoc.Database.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            foreach (ReportDocument sr in rptDoc.Subreports)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in sr.Database.Tables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);

                }
            }


        }
        private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void AddParent()
        {

            string sKeyTemp = "";
            tvwRightSide.Nodes.Clear();
            //  Hashtable ht = new Hashtable();
            // DataTable dt = new System.Data.DataTable();

            ht.Clear();
            dt.Clear();

            //for (int i = 0; i < dt.Rows.Count; i++)
            //  {
            sKeyTemp = "Reports";
            // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
            tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds(sKeyTemp);
            // }


        }
        private void AddChilds(string sKey)
        {
            ht.Clear();
            dt.Clear();
            //Hashtable ht = new Hashtable();
            // DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;


            tvwRightSide.Nodes[0].Nodes.Add("Client Wise Invoice Report");

        }

        private void btn_Report_Click(object sender, EventArgs e)
        {
            if (ddl_Client_Name.SelectedIndex > 0)
            {
                string Client = ddl_Client_Name.SelectedValue.ToString();
                string Sub_Client = ddl_Subprocess.Text.ToString();
                if (ddl_Client_Name.SelectedIndex <= 0)
                {

                    Client = "SELECT";
                }
                if (ddl_Subprocess.SelectedIndex <= 0)
                {

                    Sub_Client = "SELECT";
                }



                if (Sub_Client == "SELECT")
                {


                    rptDoc = new InvoiceRep.InvReport.Monthly_Invoice_Client_Wise_New_Report();

                    string From_date = txt_Fromdate.Text;
                    DateTime dtfrom = Convert.ToDateTime(txt_Fromdate.Text);
                    DateTime dtto = Convert.ToDateTime(txt_Todate.Text);
                    rptDoc.SetParameterValue("@Trans", "SELECT_ALL");
                    rptDoc.SetParameterValue("@Client", Client);
                    rptDoc.SetParameterValue("@Sub_Client", ddl_Subprocess.SelectedValue);
                    rptDoc.SetParameterValue("@From_Date", dtfrom.ToString("MM/dd/yyyy"));
                    rptDoc.SetParameterValue("@To_Date", dtto.ToString("MM/dd/yyyy"));


                    rptDoc.SetParameterValue("@Client", Client, "Individual");
                    rptDoc.SetParameterValue("@Subprocess_ID", ddl_Subprocess.SelectedValue, "Individual");
                    rptDoc.SetParameterValue("@From_Date", dtfrom.ToString("MM/dd/yyyy"), "Individual");
                    rptDoc.SetParameterValue("@To_Date", dtto.ToString("MM/dd/yyyy"), "Individual");


                    Logon_To_Crystal();
                    crViewer.ReportSource = rptDoc;

                }
                else if (Sub_Client != "SELECT")
                {

                    rptDoc = new InvoiceRep.InvReport.Monthly_Invoice_Client_Wise_New_Report();

                    string From_date = txt_Fromdate.Text;
                    DateTime dtfrom = Convert.ToDateTime(txt_Fromdate.Text);
                    DateTime dtto = Convert.ToDateTime(txt_Todate.Text);
                    rptDoc.SetParameterValue("@Trans", "SELECT_CLIENT_WISE");
                    rptDoc.SetParameterValue("@Client", Client);
                    rptDoc.SetParameterValue("@Sub_Client", ddl_Subprocess.SelectedValue);
                    rptDoc.SetParameterValue("@From_Date", dtfrom.ToString("MM/dd/yyyy"));
                    rptDoc.SetParameterValue("@To_Date", dtto.ToString("MM/dd/yyyy"));


                    rptDoc.SetParameterValue("@Client", Client, "Individual");
                    rptDoc.SetParameterValue("@Subprocess_ID", ddl_Subprocess.SelectedValue, "Individual");
                    rptDoc.SetParameterValue("@From_Date", dtfrom.ToString("MM/dd/yyyy"), "Individual");
                    rptDoc.SetParameterValue("@To_Date", dtto.ToString("MM/dd/yyyy"), "Individual");




                    Logon_To_Crystal();
                    crViewer.ReportSource = rptDoc;
                }


            }


        }

        private void ddl_Client_Name_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ddl_Client_Name.SelectedIndex > 0)
            {
                int clientid = int.Parse(ddl_Client_Name.SelectedValue.ToString());
                if (User_Role == "1" || userid == 260 || userid == 179)
                {
                    dbc.BindSubProcessName_rpt(ddl_Subprocess, clientid);
                }
                else
                {
                    dbc.BindSubProcessNo_rpt(ddl_Subprocess, clientid);
                }
                ddl_Subprocess.Focus();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex++;
            if (txt_orderserach_Number.Text != "")
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dt1.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                Search_Record();
            }
            else
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtuser.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                Geridview_Bind_Invoice_Orders();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            this.Cursor = currentCursor;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txt_orderserach_Number.Text != "")
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt1.Rows.Count) / pagesize) - 1;
                Search_Record();
            }
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtuser.Rows.Count) / pagesize) - 1;
                Geridview_Bind_Invoice_Orders();
            }

            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;


            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
            currentpageindex--;
            if (currentpageindex == 0)
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
            if (txt_orderserach_Number.Text != "")
            {
                Search_Record();
            }
            else
            {
                Geridview_Bind_Invoice_Orders();
            }
            this.Cursor = currentCursor;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (txt_orderserach_Number.Text != "")
            {
                Search_Record();
            }
            else
            {
                Geridview_Bind_Invoice_Orders();
            }
            this.Cursor = currentCursor;
        }

        private void btn_MonthlyNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentmonthlyindex++;
            if (txt_Monthly_Order_Search.Text != "")
            {
                if (currentmonthlyindex == (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / page) - 1)
                {
                    btn_MonthlyNext.Enabled = false;
                    btn_MonthlyLast.Enabled = false;
                }
                else
                {
                    btn_MonthlyNext.Enabled = true;
                    btn_MonthlyLast.Enabled = true;

                }
                Search_MonthlyInvoice_Record();
            }
            else
            {
                if (currentmonthlyindex == (int)Math.Ceiling(Convert.ToDecimal(dtMonthlyuser.Rows.Count) / page) - 1)
                {
                    btn_MonthlyNext.Enabled = false;
                    btn_MonthlyLast.Enabled = false;
                }
                else
                {
                    btn_MonthlyNext.Enabled = true;
                    btn_MonthlyLast.Enabled = true;

                }
                Geridview_Bind_Monthly_Invoice_Orders();
            }
            btn_Monthlyfirst.Enabled = true;
            btn_Monthly_Previous.Enabled = true;

            this.Cursor = currentCursor;
        }

        private void btn_MonthlyLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txt_Monthly_Order_Search.Text != "")
            {
                currentmonthlyindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / page) - 1;
                Search_MonthlyInvoice_Record();
            }
            else
            {
                currentmonthlyindex = (int)Math.Ceiling(Convert.ToDecimal(dtMonthlyuser.Rows.Count) / page) - 1;
                Geridview_Bind_Monthly_Invoice_Orders();
            }
            btn_Monthlyfirst.Enabled = true;
            btn_Monthly_Previous.Enabled = true;
            btn_MonthlyNext.Enabled = false;
            btn_MonthlyLast.Enabled = false;

            this.Cursor = currentCursor;
        }

        private void btn_Monthly_Previous_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
            currentmonthlyindex--;
            if (currentmonthlyindex == 0)
            {
                btn_Monthly_Previous.Enabled = false;
                btn_Monthlyfirst.Enabled = false;
            }
            else
            {
                btn_Monthly_Previous.Enabled = true;
                btn_Monthlyfirst.Enabled = true;

            }
            btn_MonthlyNext.Enabled = true;
            btn_MonthlyLast.Enabled = true;
            if (txt_Monthly_Order_Search.Text != "")
            {
                Search_MonthlyInvoice_Record();
            }
            else
            {
                Geridview_Bind_Monthly_Invoice_Orders();
            }
            this.Cursor = currentCursor;
        }

        private void btn_Monthlyfirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentmonthlyindex = 0;
            btn_Monthly_Previous.Enabled = false;
            btn_MonthlyNext.Enabled = true;
            btn_MonthlyLast.Enabled = true;
            btn_Monthlyfirst.Enabled = false;
            if (txt_Monthly_Order_Search.Text != "")
            {
                Search_MonthlyInvoice_Record();
            }
            else
            {
                Geridview_Bind_Monthly_Invoice_Orders();
            }
            this.Cursor = currentCursor;
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

        private void btn_Send_All_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);


            if (dialogResult == DialogResult.Yes)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;


                    if (isChecked == true)
                    {
                        Sub_Process_ID = int.Parse(grd_order.Rows[i].Cells[21].Value.ToString());
                        int invoice_ID = int.Parse(grd_order.Rows[i].Cells[22].Value.ToString());
                        string Order_Number = grd_order.Rows[i].Cells[2].Value.ToString();
                        int order_Id = int.Parse(grd_order.Rows[i].Cells[19].Value.ToString());

                        // Get_Email();

                        Hashtable htin = new Hashtable();
                        DataTable dtin = new DataTable();
                        htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
                        htin.Add("@Subprocess_ID", Sub_Process_ID);
                        dtin = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htin);
                        if (dtin.Rows.Count > 0)
                        {
                            Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

                        }
                        if (Inv_Status == "False")
                        {
                            InvoiceRep.Invoice_Send_Email inv = new InvoiceRep.Invoice_Send_Email(Order_Number, userid, order_Id, invoice_ID, Inv_Status, "Order_Invoice", Sub_Process_ID);
                            // inv.Show();
                        }
                        else if (Inv_Status == "True")
                        {
                            InvoiceRep.Invoice_Send_Email inv = new InvoiceRep.Invoice_Send_Email(Order_Number, userid, order_Id, invoice_ID, Inv_Status, "Order_Invoice", Sub_Process_ID);
                            //inv.Show();
                        }
                    }

                }
                Geridview_Bind_Invoice_Orders();
                // cProbar.stopProgress();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlSubProcess.SelectedIndex > 0)
            {
                Sub_Process_ID = int.Parse(ddlSubProcess.SelectedValue.ToString());
            }
            else
            {
                Sub_Process_ID = 0;
            }
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            if (txt_From_date.Text != "" && txt_To_date.Text != "")
            {
                DateTime fromdate = Convert.ToDateTime(txt_From_date.Text, usDtfi);
                DateTime Todate = Convert.ToDateTime(txt_To_date.Text, usDtfi);
                Hashtable htsearch = new Hashtable();
                DataTable dtsearch = new DataTable();
                htsearch.Add("@Trans", "GET_CLIENT_WISE_ORDER_COST_DETAILS_EXPORT");
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
                Grid_Invoice_Details.Columns[7].Width = 100;
                if (dtsearch.Rows.Count > 0)
                {
                    Grid_Invoice_Details.Rows.Clear();
                    for (int i = 0; i < dtsearch.Rows.Count; i++)
                    {
                        Grid_Invoice_Details.Rows.Add();
                        Grid_Invoice_Details.Rows[i].Cells[0].Value = i + 1;
                        Grid_Invoice_Details.Rows[i].Cells[1].Value = dtsearch.Rows[i]["Client_Order_Number"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Search_Cost"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[3].Value = dtsearch.Rows[i]["Copy_Cost"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[4].Value = dtsearch.Rows[i]["Additional_Fees"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[5].Value = dtsearch.Rows[i]["Total"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[6].Value = dtsearch.Rows[i]["Invoice_Date"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[7].Value = dtsearch.Rows[i]["Order_ID"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[8].Value = dtsearch.Rows[i]["Order_Invoice_No"].ToString();
                    }
                }
                else
                {
                    Grid_Invoice_Details.Rows.Clear();
                    Grid_Invoice_Details.DataSource = null;
                }
            }
        }
        private void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClient.SelectedIndex > 0)
            {
                int clientid = int.Parse(ddlClient.SelectedValue.ToString());
                if (User_Role == "1" || userid == 260 || userid == 179)
                {
                    dbc.BindSubProcessName_rpt(ddlSubProcess, clientid);
                }
                else
                {
                    dbc.BindSubProcessNo_rpt(ddlSubProcess, clientid);
                }
                ddlSubProcess.Focus();
            }
        }

        private void Grid_Invoice_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                form_loader.Start_progres();
                int Order_Id = int.Parse(Grid_Invoice_Details.Rows[e.RowIndex].Cells[7].Value.ToString());
                string Invoice_No = Grid_Invoice_Details.Rows[e.RowIndex].Cells[8].Value.ToString();
                Invoice_Order_Details inv = new Invoice_Order_Details(Order_Id, userid, "Update", Invoice_No, User_Role);
                inv.Show();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                DateTime fromdate = Convert.ToDateTime(txt_From_date.Text, usDtfi);
                DateTime Todate = Convert.ToDateTime(txt_To_date.Text, usDtfi);
                Hashtable htsearch = new Hashtable();
                DataTable dtsearch = new DataTable();
                htsearch.Add("@Trans", "GET_CLIENT_WISE_ORDER_COST_DETAILS_EXPORT");
                htsearch.Add("@Subprocess_ID", Sub_Process_ID);
                htsearch.Add("@From_Date", fromdate);
                htsearch.Add("@To_Date", Todate);
                dtsearch = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htsearch);
                if (dtsearch != null && dtsearch.Rows.Count > 0)
                {
                    dtsearch.Columns.Remove("Order_ID");
                    dtsearch.Columns.Remove("Invoice_Id");
                    dtsearch.Columns.Remove("Invoice_Auto_No");
                    dtsearch.Columns.Remove("Client_Id");
                    dtsearch.Columns.Remove("Subprocess_Id");
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        string filePath = @"C:\Invoice Reports\";
                        string fileName = filePath + "Invoice Reports - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        workbook.Worksheets.Add(dtsearch, "Invoice Reports");
                        workbook.SaveAs(fileName);
                        SplashScreenManager.CloseForm(false);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Found");
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_Complete_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);


            if (dialogResult == DialogResult.Yes)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;


                    if (isChecked == true)
                    {
                        int order_Id = int.Parse(grd_order.Rows[i].Cells[19].Value.ToString());


                        Hashtable htupdate = new Hashtable();
                        DataTable dtupdate = new DataTable();
                        htupdate.Add("@Trans", "UPDATE_EMAIL_STATUS");
                        htupdate.Add("@Order_ID", order_Id);
                        dtupdate = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htupdate);



                    }
                }
                Geridview_Bind_Invoice_Orders();
                txt_orderserach_Number.Text = "";
                //cProbar.stopProgress();
            }
        }

        private void btn_Monthl_Invoice_Send_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);


            if (dialogResult == DialogResult.Yes)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                for (int i = 0; i < grid_Monthly_Invoice.Rows.Count; i++)
                {
                    bool isChecked = (bool)grid_Monthly_Invoice[1, i].FormattedValue;


                    if (isChecked == true)
                    {


                        int invoice_ID = int.Parse(grid_Monthly_Invoice.Rows[i].Cells[13].Value.ToString());
                        Sub_Process_ID = int.Parse(grid_Monthly_Invoice.Rows[i].Cells[15].Value.ToString());


                        Get_Email();

                        Hashtable htin = new Hashtable();
                        DataTable dtin = new DataTable();
                        htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
                        htin.Add("@Subprocess_ID", Sub_Process_ID);
                        dtin = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htin);
                        if (dtin.Rows.Count > 0)
                        {
                            Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

                        }

                        InvoiceRep.Invoice_Send_Email inv = new InvoiceRep.Invoice_Send_Email("00", userid, 0, invoice_ID, "", "Monthly_Invoice", Sub_Process_ID);



                    }
                }
                Geridview_Bind_Monthly_Invoice_Orders();
                txt_Monthly_Order_Search.Text = "";
                // cProbar.stopProgress();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                for (int i = 0; i < grid_Monthly_Invoice.Rows.Count; i++)
                {



                    grid_Monthly_Invoice[1, i].Value = true;
                }
            }
            else if (checkBox1.Checked == false)
            {

                for (int i = 0; i < grid_Monthly_Invoice.Rows.Count; i++)
                {

                    grid_Monthly_Invoice[1, i].Value = false;
                }
            }
        }

        private void grd_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }





    }
}
