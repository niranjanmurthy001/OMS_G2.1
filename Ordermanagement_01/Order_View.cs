using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;
namespace Ordermanagement_01
{
    public partial class Order_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        System.Data.DataTable dttargetorder = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();

        

        string Client_Id, Operation,Operation_Count,From_date,To_Date;
        int User_id;
       // InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        DataSet ds = new DataSet();
        //InfiniteProgressBar.frmProgress form = new InfiniteProgressBar.frmProgress();
        System.Data.DataTable dtexport = new System.Data.DataTable();

        static int currentpageindex = 0;
        int pagesize = 15;
        int Sub_Process_ID;
        string Path1,userroleid;
        string Production_Date;
        public Order_View(string CLIENT_ID, string OPERATION, string OERATION_COUNT, string FROM_DATE, string TO_DATE, int USER_ID, int SUB_PROCESS_D, string User_roleid, string PRODUCTION_DATE)
        {
            User_id = USER_ID;
            Client_Id = CLIENT_ID;
            Operation = OPERATION;
            Operation_Count = OERATION_COUNT;
            From_date = FROM_DATE;
            To_Date = TO_DATE;
            Sub_Process_ID = SUB_PROCESS_D;
            userroleid = User_roleid;
            Production_Date = PRODUCTION_DATE;
            InitializeComponent();
        }
        private void Get_Table_Row_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dttargetorder.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        protected void Get_Client_Wise_Production_Count_Orders_To_GridviewBind()
        {


            DateTime Fromdate = Convert.ToDateTime(From_date.ToString());
            DateTime Todate = Convert.ToDateTime(To_Date.ToString());

            Hashtable httargetorder = new Hashtable();
            //System.Data.DataTable dttargetorder = new System.Data.DataTable();
            dtexport.Clear();
            httargetorder.Add("@Trans",Operation);
            httargetorder.Add("@Clint", Client_Id);
            httargetorder.Add("@Sub_Client", Sub_Process_ID);
            httargetorder.Add("@Fromdate", Fromdate);
            httargetorder.Add("@Todate", Todate);
            dttargetorder = dataaccess.ExecuteSP("Sp_Order_Status_Report", httargetorder);
            dtexport = dttargetorder;

            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                Get_Table_Row_Search(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            if (temptable.Rows.Count > 0)
            {
                grd_Targetorder.DataSource = null;

                grd_Targetorder.AutoGenerateColumns = false;
                grd_Targetorder.ColumnCount = 18;

                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;

                //grd_Targetorder.Columns[1].Name = "Order Number";
                //grd_Targetorder.Columns[1].HeaderText = "CLIENT_ORDER_NUMBER";
                //grd_Targetorder.Columns[1].DataPropertyName = "Order_Number";
                //grd_Targetorder.Columns[1].Width = 200;
                //grd_Targetorder.Columns[1].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "CLIENT ORDER NUMBER";
                link.DataPropertyName = "Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;

                

                grd_Targetorder.Columns[1].Name = "Customer";
                grd_Targetorder.Columns[1].HeaderText = "CLIENT NAME";
                if (userroleid == "1")
                {
                    grd_Targetorder.Columns[1].DataPropertyName = "Client_name";
                }
                else 
                {
                    grd_Targetorder.Columns[1].DataPropertyName = "Client_Number";
                }
                grd_Targetorder.Columns[1].Width = 140;

                grd_Targetorder.Columns[2].Name = "SubProcess";
                grd_Targetorder.Columns[2].HeaderText = "SUB PROCESS";
                if (userroleid == "1")
                {
                    grd_Targetorder.Columns[2].DataPropertyName = "Sub_client";
                }
                else 
                {
                    grd_Targetorder.Columns[2].DataPropertyName = "Subprocess_Number";
                }
                grd_Targetorder.Columns[2].Width = 220;


                grd_Targetorder.Columns[3].Name = "Submited";
                grd_Targetorder.Columns[3].HeaderText = "SUBMITTED DATE";
                grd_Targetorder.Columns[3].DataPropertyName = "Date";
                grd_Targetorder.Columns[3].Width = 120;

                
                grd_Targetorder.Columns[4].Name = "OrderType";
                grd_Targetorder.Columns[4].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[4].DataPropertyName = "Order_type";
                grd_Targetorder.Columns[4].Width = 160;

                grd_Targetorder.Columns[5].Name = "Borrower_name";
                grd_Targetorder.Columns[5].HeaderText = "Borrower Name";
                grd_Targetorder.Columns[5].DataPropertyName = "Borrower_Name";
                grd_Targetorder.Columns[5].Width = 120;

                grd_Targetorder.Columns[6].Name = "Property_Address";
                grd_Targetorder.Columns[6].HeaderText = "Property Address";
                grd_Targetorder.Columns[6].DataPropertyName = "Address";
                grd_Targetorder.Columns[6].Width = 120;

                grd_Targetorder.Columns[7].Name = "ClientOrderRef";
                grd_Targetorder.Columns[7].HeaderText = "CLIENT ORDER REF. NO";
                grd_Targetorder.Columns[7].DataPropertyName = "Ref_number";
                grd_Targetorder.Columns[7].Width = 170;


                grd_Targetorder.Columns[8].Name = "SearchType";
                grd_Targetorder.Columns[8].HeaderText = "SEARCH TYPE";
                grd_Targetorder.Columns[8].DataPropertyName = "County_type";
                grd_Targetorder.Columns[8].Width = 160;

                grd_Targetorder.Columns[9].Name = "State";
                grd_Targetorder.Columns[9].HeaderText = "STATE";
                grd_Targetorder.Columns[9].DataPropertyName = "State";
                grd_Targetorder.Columns[9].Width = 120;

                grd_Targetorder.Columns[10].Name = "County";
                grd_Targetorder.Columns[10].HeaderText = "COUNTY";
                grd_Targetorder.Columns[10].DataPropertyName = "County";
                grd_Targetorder.Columns[10].Width = 140;
               


                grd_Targetorder.Columns[11].Name = "Task";
                grd_Targetorder.Columns[11].HeaderText = "TASK";
                grd_Targetorder.Columns[11].DataPropertyName = "Current_Task";
                grd_Targetorder.Columns[11].Width = 120;


                grd_Targetorder.Columns[12].Name = "STATUS";
                grd_Targetorder.Columns[12].HeaderText = "STATUS";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[12].Width = 100;


                grd_Targetorder.Columns[13].Name = "Order_ID";
                grd_Targetorder.Columns[13].HeaderText = "Order_ID";
                grd_Targetorder.Columns[13].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[13].Width = 100;
                grd_Targetorder.Columns[13].Visible = false;

                grd_Targetorder.Columns[14].Name = "User";
                grd_Targetorder.Columns[14].HeaderText = "User Name";
                grd_Targetorder.Columns[14].DataPropertyName = "UserName";
                grd_Targetorder.Columns[14].Width = 100;

                grd_Targetorder.Columns[15].Name = "Comment";
                grd_Targetorder.Columns[15].HeaderText = "Comments";
                grd_Targetorder.Columns[15].DataPropertyName = "Comment";
                grd_Targetorder.Columns[15].Width = 100;

                grd_Targetorder.Columns[16].Name = "Notes";
                grd_Targetorder.Columns[16].HeaderText = "Notes";
                grd_Targetorder.Columns[16].DataPropertyName = "Notes";
                grd_Targetorder.Columns[16].Width = 100;

                grd_Targetorder.Columns[17].Name = "Production_Date";
                grd_Targetorder.Columns[17].HeaderText = "Production Date";
                grd_Targetorder.Columns[17].DataPropertyName = "Production_Date";
                grd_Targetorder.Columns[17].Width = 100;



                //grd_Targetorder.Columns[18].Name = "Recived_Order_Time";
                //grd_Targetorder.Columns[18].HeaderText = "Recived Order Date";
                //grd_Targetorder.Columns[18].DataPropertyName = "Recived_Order_Time";
                //grd_Targetorder.Columns[18].Width = 100;




                //grd_Targetorder.Columns[19].Name = "Completed_Date";
                //grd_Targetorder.Columns[19].HeaderText = "Completed Date";
                //grd_Targetorder.Columns[19].DataPropertyName = "Completed_Date";
                //grd_Targetorder.Columns[19].Width = 100;
           

                //  grd_Targetorder.Columns[12].Visible = false;
                grd_Targetorder.DataSource = temptable;


                lbl_Total.Text = dttargetorder.Rows.Count.ToString();
                
            }
            else
            {

                grd_Targetorder.DataSource = null;
                lbl_Total.Text = "0";
                //grd_Targetorder.DataBind();
            }



            lbl_Total_Orders.Text = dttargetorder.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }

        }

        protected void Get_Count_Of_Order_Type_Wise()
        { 
        
            DateTime Fromdate = Convert.ToDateTime(From_date.ToString());
            DateTime Todate = Convert.ToDateTime(To_Date.ToString());
            string F_Date = Fromdate.ToString("MM/dd/yyyy");
            string T_Date = Todate.ToString("MM/dd/yyyy");
            Hashtable httargetorder = new Hashtable();
            System.Data.DataTable dttargetorder = new System.Data.DataTable();
            dtexport.Clear();
            httargetorder.Add("@Trans", Operation_Count);
            httargetorder.Add("@Clint", Client_Id);
            httargetorder.Add("@Sub_Client", Sub_Process_ID);
            httargetorder.Add("@F_Date", F_Date);
            httargetorder.Add("@T_date", T_Date);
            dttargetorder = dataaccess.ExecuteSP("Sp_Order_Status_Report", httargetorder);

            if (dttargetorder.Rows.Count > 0)
            {

                Grid_Count.DataSource = dttargetorder;
                

            }
            else
            {
                Grid_Count.DataSource = null;


            }



        }

        private void Order_View_Load(object sender, EventArgs e)
        {

            Get_Count_Of_Order_Type_Wise();
            Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
            btnFirst_Click(sender, e);
        }

        private void grd_Targetorder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 18)
                {
                    //cProbar.startProgress();
                    form_loader.Start_progres();
                    Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(grd_Targetorder.Rows[e.RowIndex].Cells[13].Value.ToString()), User_id, userroleid, "");
                    OrderEntry.Show();
                    //cProbar.stopProgress();
                }
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            //cProbar.startProgress();
            form_loader.Start_progres();
            ds.Tables.Add(dtexport);

            Export_ReportData();
           // Convert_Dataset_to_Excel();
            ds.Clear();
            dtexport.Clear();
            Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
            //cProbar.stopProgress();
        }
        private void Export_ReportData()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            if (dttargetorder.Rows.Count > 0)
            {
                grd_export.DataSource = dttargetorder;
                for (int i = 0; i < dttargetorder.Rows.Count; i++)
                {
                    grd_export.DataSource = null;

                    grd_export.AutoGenerateColumns = false;
                    grd_export.ColumnCount = 18;

                    grd_export.Columns[0].Name = "SNo";
                    grd_export.Columns[0].HeaderText = "S. No";
                    grd_export.Columns[0].Width = 65;


                    grd_export.Columns[1].Name = "Order Number";
                    grd_export.Columns[1].HeaderText = "ORDER_NUMBER";
                    grd_export.Columns[1].DataPropertyName = "Order_Number";
                    grd_export.Columns[1].Width = 200;
                    grd_export.Columns[1].Visible = false;

                    //DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                    //grd_export.Columns.Add(link);
                    //link.Name = "Order Number";
                    //link.HeaderText = "CLIENT ORDER NUMBER";
                    //link.DataPropertyName = "Order_Number";
                    //link.Width = 200;
                    //link.DisplayIndex = 1;



                    grd_export.Columns[2].Name = "Customer";
                    grd_export.Columns[2].HeaderText = "CLIENT NAME";
                    if (userroleid == "1")
                    {
                        grd_export.Columns[2].DataPropertyName = "Client_name";
                    }
                    else 
                    {
                        grd_export.Columns[2].DataPropertyName = "Client_Number";

                    }
                    grd_export.Columns[2].Width = 140;

                    grd_export.Columns[3].Name = "SubProcess";
                    grd_export.Columns[3].HeaderText = "SUB PROCESS";
                    if (userroleid == "1")
                    {
                        grd_export.Columns[3].DataPropertyName = "Sub_client";
                    }
                    else
                    {
                        grd_export.Columns[3].DataPropertyName = "Subprocess_Number";

                    }
                    grd_export.Columns[3].Width = 220;


                    grd_export.Columns[4].Name = "Submited";
                    grd_export.Columns[4].HeaderText = "SUBMITTED DATE";
                    grd_export.Columns[4].DataPropertyName = "Date";
                    grd_export.Columns[4].Width = 120;


                    grd_export.Columns[5].Name = "OrderType";
                    grd_export.Columns[5].HeaderText = "ORDER TYPE";
                    grd_export.Columns[5].DataPropertyName = "Order_type";
                    grd_export.Columns[5].Width = 160;

                    grd_export.Columns[6].Name = "Borrower_name";
                    grd_export.Columns[6].HeaderText = "Borrower Name";
                    grd_export.Columns[6].DataPropertyName = "Borrower_Name";
                    grd_export.Columns[6].Width = 120;

                    grd_export.Columns[7].Name = "Property_Address";
                    grd_export.Columns[7].HeaderText = "Property Address";
                    grd_export.Columns[7].DataPropertyName = "Address";
                    grd_export.Columns[7].Width = 120;

                    grd_export.Columns[8].Name = "ClientOrderRef";
                    grd_export.Columns[8].HeaderText = "CLIENT ORDER REF. NO";
                    grd_export.Columns[8].DataPropertyName = "Ref_number";
                    grd_export.Columns[8].Width = 170;


                    grd_export.Columns[9].Name = "SearchType";
                    grd_export.Columns[9].HeaderText = "SEARCH TYPE";
                    grd_export.Columns[9].DataPropertyName = "County_type";
                    grd_export.Columns[9].Width = 160;

                    grd_export.Columns[10].Name = "State";
                    grd_export.Columns[10].HeaderText = "STATE";
                    grd_export.Columns[10].DataPropertyName = "State";
                    grd_export.Columns[10].Width = 120;

                    grd_export.Columns[11].Name = "County";
                    grd_export.Columns[11].HeaderText = "COUNTY";
                    grd_export.Columns[11].DataPropertyName = "County";
                    grd_export.Columns[11].Width = 140;


                    grd_export.Columns[12].Name = "Task";
                    grd_export.Columns[12].HeaderText = "TASK";
                    grd_export.Columns[12].DataPropertyName = "Current_Task";
                    grd_export.Columns[12].Width = 120;


                    grd_export.Columns[13].Name = "STATUS";
                    grd_export.Columns[13].HeaderText = "STATUS";
                    grd_export.Columns[13].DataPropertyName = "Order_Status";
                    grd_export.Columns[13].Width = 100;

                    
                    grd_export.Columns[14].Name = "User";
                    grd_export.Columns[14].HeaderText = "User Name";
                    grd_export.Columns[14].DataPropertyName = "UserName";
                    grd_export.Columns[14].Width = 100;

                    grd_export.Columns[15].Name = "Comment";
                    grd_export.Columns[15].HeaderText = "Comments";
                    grd_export.Columns[15].DataPropertyName = "Comment";
                    grd_export.Columns[15].Width = 100;

                    grd_export.Columns[16].Name = "Notes";
                    grd_export.Columns[16].HeaderText = "Notes";
                    grd_export.Columns[16].DataPropertyName = "Notes";
                    grd_export.Columns[16].Width = 100;

                    grd_export.Columns[17].Name = "Production_Date";
                    grd_export.Columns[17].HeaderText = "Production Date";
                    grd_export.Columns[17].DataPropertyName = "Production_Date";
                    grd_export.Columns[17].Width = 100;


                    //  grd_Targetorder.Columns[12].Visible = false;
                    grd_export.DataSource = dttargetorder;


                    // lbl_Total.Text = dttargetorder.Rows.Count.ToString();
                }
            }
            else
            {

                grd_export.DataSource = null;
                lbl_Total.Text = "0";
                //grd_Targetorder.DataBind();
            }
            for (int i = 0; i < grd_export.Rows.Count; i++)
            {
                grd_export.Rows[i].Cells[0].Value = i + 1;
            }
            //Adding the Columns
            foreach (DataGridViewColumn column in grd_export.Columns)
            {
                if (column.HeaderText != "")
                {
                    
                    if (column.ValueType == null)
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in grd_export.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();


                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }

            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Order_Details"  + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Order_Details");


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
        }
        private void Convert_Dataset_to_Excel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = true;
            Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            DataTableCollection collection = ds.Tables;

            for (int i = collection.Count; i > 0; i--)
            {
                Sheets xlSheets = null;
                Worksheet xlWorksheet = null;
                //Create Excel Sheets
                xlSheets = ExcelApp.Worksheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                               Type.Missing, Type.Missing, Type.Missing);

                System.Data.DataTable table = collection[i - 1];
                xlWorksheet.Name = table.TableName;

                for (int j = 1; j < table.Columns.Count + 1; j++)
                {
                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                }

                // Storing Each row and column value to excel sheet
                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 0; l < table.Columns.Count; l++)
                    {
                        ExcelApp.Cells[k + 2, l + 1] =
                        table.Rows[k].ItemArray[l].ToString();
                    }
                }
                ExcelApp.Columns.AutoFit();
            }
            ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
            ExcelApp.Visible = true;

        }
        private void Get_Table_Row_Search_Number(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            DataView dtsearch = new DataView(dtexport);
            if (txt_SearchOrdernumber.Text != "")
            {
                string search = txt_SearchOrdernumber.Text.ToString();
                dtsearch.RowFilter = "Order_Number like '%" + search.ToString() + "%'";
                //System.Data.DataTable dt = new System.Data.DataTable();
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
                    Get_Table_Row_Search_Number(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }


                if (temptable.Rows.Count > 0)
                {

                    grd_Targetorder.DataSource = null;

                    grd_Targetorder.AutoGenerateColumns = false;
                    grd_Targetorder.ColumnCount = 18;

                    grd_Targetorder.Columns[0].Name = "SNo";
                    grd_Targetorder.Columns[0].HeaderText = "S. No";
                    grd_Targetorder.Columns[0].Width = 65;

                    //grd_Targetorder.Columns[1].Name = "Order Number";
                    //grd_Targetorder.Columns[1].HeaderText = "CLIENT_ORDER_NUMBER";
                    //grd_Targetorder.Columns[1].DataPropertyName = "Order_Number";
                    //grd_Targetorder.Columns[1].Width = 200;
                    //grd_Targetorder.Columns[1].Visible = false;

                    DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                    grd_Targetorder.Columns.Add(link);
                    link.Name = "Order Number";
                    link.HeaderText = "CLIENT ORDER NUMBER";
                    link.DataPropertyName = "Order_Number";
                    link.Width = 200;
                    link.DisplayIndex = 1;



                    grd_Targetorder.Columns[1].Name = "Customer";
                    grd_Targetorder.Columns[1].HeaderText = "CLIENT NAME";
                    if (userroleid == "1")
                    {
                        grd_Targetorder.Columns[1].DataPropertyName = "Client_name";
                    }
                    else 
                    {

                        grd_Targetorder.Columns[1].DataPropertyName = "Client_Number";
                    }
                    grd_Targetorder.Columns[1].Width = 140;

                    grd_Targetorder.Columns[2].Name = "SubProcess";
                    grd_Targetorder.Columns[2].HeaderText = "SUB PROCESS";
                    if (userroleid == "1")
                    {
                        grd_Targetorder.Columns[2].DataPropertyName = "Sub_client";
                    }
                    else 
                    {
                        grd_Targetorder.Columns[2].DataPropertyName = "Subprocess_Number";

                    }
                    grd_Targetorder.Columns[2].Width = 220;


                    grd_Targetorder.Columns[3].Name = "Submited";
                    grd_Targetorder.Columns[3].HeaderText = "SUBMITTED DATE";
                    grd_Targetorder.Columns[3].DataPropertyName = "Date";
                    grd_Targetorder.Columns[3].Width = 120;


                    grd_Targetorder.Columns[4].Name = "OrderType";
                    grd_Targetorder.Columns[4].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[4].DataPropertyName = "Order_type";
                    grd_Targetorder.Columns[4].Width = 160;

                    grd_Targetorder.Columns[5].Name = "Borrower_name";
                    grd_Targetorder.Columns[5].HeaderText = "Borrower Name";
                    grd_Targetorder.Columns[5].DataPropertyName = "Borrower_Name";
                    grd_Targetorder.Columns[5].Width = 120;

                    grd_Targetorder.Columns[6].Name = "Property_Address";
                    grd_Targetorder.Columns[6].HeaderText = "Property Address";
                    grd_Targetorder.Columns[6].DataPropertyName = "Address";
                    grd_Targetorder.Columns[6].Width = 120;

                    grd_Targetorder.Columns[7].Name = "ClientOrderRef";
                    grd_Targetorder.Columns[7].HeaderText = "CLIENT ORDER REF. NO";
                    grd_Targetorder.Columns[7].DataPropertyName = "Ref_number";
                    grd_Targetorder.Columns[7].Width = 170;


                    grd_Targetorder.Columns[8].Name = "SearchType";
                    grd_Targetorder.Columns[8].HeaderText = "SEARCH TYPE";
                    grd_Targetorder.Columns[8].DataPropertyName = "County_type";
                    grd_Targetorder.Columns[8].Width = 160;


                    grd_Targetorder.Columns[9].Name = "County";
                    grd_Targetorder.Columns[9].HeaderText = "COUNTY";
                    grd_Targetorder.Columns[9].DataPropertyName = "County";
                    grd_Targetorder.Columns[9].Width = 140;


                    grd_Targetorder.Columns[10].Name = "State";
                    grd_Targetorder.Columns[10].HeaderText = "STATE";
                    grd_Targetorder.Columns[10].DataPropertyName = "State";
                    grd_Targetorder.Columns[10].Width = 120;


                    grd_Targetorder.Columns[11].Name = "Task";
                    grd_Targetorder.Columns[11].HeaderText = "TASK";
                    grd_Targetorder.Columns[11].DataPropertyName = "Current_Task";
                    grd_Targetorder.Columns[11].Width = 120;


                    grd_Targetorder.Columns[12].Name = "STATUS";
                    grd_Targetorder.Columns[12].HeaderText = "STATUS";
                    grd_Targetorder.Columns[12].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[12].Width = 100;


                    grd_Targetorder.Columns[13].Name = "Order_ID";
                    grd_Targetorder.Columns[13].HeaderText = "Order_ID";
                    grd_Targetorder.Columns[13].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[13].Width = 100;
                    grd_Targetorder.Columns[13].Visible = false;

                    grd_Targetorder.Columns[14].Name = "User";
                    grd_Targetorder.Columns[14].HeaderText = "User Name";
                    grd_Targetorder.Columns[14].DataPropertyName = "UserName";
                    grd_Targetorder.Columns[14].Width = 100;

                    grd_Targetorder.Columns[15].Name = "Comment";
                    grd_Targetorder.Columns[15].HeaderText = "Comments";
                    grd_Targetorder.Columns[15].DataPropertyName = "Comment";
                    grd_Targetorder.Columns[15].Width = 100;

                    grd_Targetorder.Columns[16].Name = "Notes";
                    grd_Targetorder.Columns[16].HeaderText = "Notes";
                    grd_Targetorder.Columns[16].DataPropertyName = "Notes";
                    grd_Targetorder.Columns[16].Width = 100;

                    grd_Targetorder.Columns[17].Name = "Production_Date";
                    grd_Targetorder.Columns[17].HeaderText = "Production Date";
                    grd_Targetorder.Columns[17].DataPropertyName = "Production_Date";
                    grd_Targetorder.Columns[17].Width = 100;


                    //  grd_Targetorder.Columns[12].Visible = false;
                    grd_Targetorder.DataSource = temptable;
                }
                First_Page();

            }
            else
            {
                First_Page();
                Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
            }
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
        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }
        private void Get_Client_Wise_Production_Count_Order_Next()
        {
            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                Get_Table_Row_Search(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            if (temptable.Rows.Count > 0)
            {
                grd_Targetorder.DataSource = null;

                grd_Targetorder.AutoGenerateColumns = false;
                grd_Targetorder.ColumnCount = 18;

                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;

                //grd_Targetorder.Columns[1].Name = "Order Number";
                //grd_Targetorder.Columns[1].HeaderText = "CLIENT_ORDER_NUMBER";
                //grd_Targetorder.Columns[1].DataPropertyName = "Order_Number";
                //grd_Targetorder.Columns[1].Width = 200;
                //grd_Targetorder.Columns[1].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "CLIENT ORDER NUMBER";
                link.DataPropertyName = "Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;



                grd_Targetorder.Columns[1].Name = "Customer";
                grd_Targetorder.Columns[1].HeaderText = "CLIENT NAME";
                if (userroleid == "1")
                {
                    grd_Targetorder.Columns[1].DataPropertyName = "Client_name";
                }
                else 
                {

                    grd_Targetorder.Columns[1].DataPropertyName = "Client_Number";
                }
                grd_Targetorder.Columns[1].Width = 140;

                grd_Targetorder.Columns[2].Name = "SubProcess";
                grd_Targetorder.Columns[2].HeaderText = "SUB PROCESS";
                if (userroleid == "1")
                {
                    grd_Targetorder.Columns[2].DataPropertyName = "Sub_client";
                }
                else 
                {
                    grd_Targetorder.Columns[2].DataPropertyName = "Subprocess_Number";

                }
                grd_Targetorder.Columns[2].Width = 220;


                grd_Targetorder.Columns[3].Name = "Submited";
                grd_Targetorder.Columns[3].HeaderText = "SUBMITTED DATE";
                grd_Targetorder.Columns[3].DataPropertyName = "Date";
                grd_Targetorder.Columns[3].Width = 120;


                grd_Targetorder.Columns[4].Name = "OrderType";
                grd_Targetorder.Columns[4].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[4].DataPropertyName = "Order_type";
                grd_Targetorder.Columns[4].Width = 160;

                grd_Targetorder.Columns[5].Name = "Borrower_name";
                grd_Targetorder.Columns[5].HeaderText = "Borrower Name";
                grd_Targetorder.Columns[5].DataPropertyName = "Borrower_Name";
                grd_Targetorder.Columns[5].Width = 120;

                grd_Targetorder.Columns[6].Name = "Property_Address";
                grd_Targetorder.Columns[6].HeaderText = "Property Address";
                grd_Targetorder.Columns[6].DataPropertyName = "Address";
                grd_Targetorder.Columns[6].Width = 120;

                grd_Targetorder.Columns[7].Name = "ClientOrderRef";
                grd_Targetorder.Columns[7].HeaderText = "CLIENT ORDER REF. NO";
                grd_Targetorder.Columns[7].DataPropertyName = "Ref_number";
                grd_Targetorder.Columns[7].Width = 170;


                grd_Targetorder.Columns[8].Name = "SearchType";
                grd_Targetorder.Columns[8].HeaderText = "SEARCH TYPE";
                grd_Targetorder.Columns[8].DataPropertyName = "County_type";
                grd_Targetorder.Columns[8].Width = 160;


                grd_Targetorder.Columns[9].Name = "County";
                grd_Targetorder.Columns[9].HeaderText = "COUNTY";
                grd_Targetorder.Columns[9].DataPropertyName = "County";
                grd_Targetorder.Columns[9].Width = 140;


                grd_Targetorder.Columns[10].Name = "State";
                grd_Targetorder.Columns[10].HeaderText = "STATE";
                grd_Targetorder.Columns[10].DataPropertyName = "State";
                grd_Targetorder.Columns[10].Width = 120;


                grd_Targetorder.Columns[11].Name = "Task";
                grd_Targetorder.Columns[11].HeaderText = "TASK";
                grd_Targetorder.Columns[11].DataPropertyName = "Current_Task";
                grd_Targetorder.Columns[11].Width = 120;


                grd_Targetorder.Columns[12].Name = "STATUS";
                grd_Targetorder.Columns[12].HeaderText = "STATUS";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[12].Width = 100;


                grd_Targetorder.Columns[13].Name = "Order_ID";
                grd_Targetorder.Columns[13].HeaderText = "Order_ID";
                grd_Targetorder.Columns[13].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[13].Width = 100;
                grd_Targetorder.Columns[13].Visible = false;

                grd_Targetorder.Columns[14].Name = "User";
                grd_Targetorder.Columns[14].HeaderText = "User Name";
                grd_Targetorder.Columns[14].DataPropertyName = "UserName";
                grd_Targetorder.Columns[14].Width = 100;

                grd_Targetorder.Columns[15].Name = "Comment";
                grd_Targetorder.Columns[15].HeaderText = "Comments";
                grd_Targetorder.Columns[15].DataPropertyName = "Comment";
                grd_Targetorder.Columns[15].Width = 100;

                grd_Targetorder.Columns[16].Name = "Notes";
                grd_Targetorder.Columns[16].HeaderText = "Notes";
                grd_Targetorder.Columns[16].DataPropertyName = "Notes";
                grd_Targetorder.Columns[16].Width = 100;

                grd_Targetorder.Columns[17].Name = "Production_Date";
                grd_Targetorder.Columns[17].HeaderText = "Production Date";
                grd_Targetorder.Columns[17].DataPropertyName = "Production_Date";
                grd_Targetorder.Columns[17].Width = 100;


                //  grd_Targetorder.Columns[12].Visible = false;
                grd_Targetorder.DataSource = temptable;


                lbl_Total.Text = dttargetorder.Rows.Count.ToString();

            }
            else
            {

                grd_Targetorder.DataSource = null;
                lbl_Total.Text = "0";
                //grd_Targetorder.DataBind();
            }
            lbl_Total_Orders.Text = dttargetorder.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentpageindex + 1 <= (dttargetorder.Rows.Count / pagesize))
            {
                Cursor currentCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;

                currentpageindex++;
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                Get_Client_Wise_Production_Count_Order_Next();

                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;

                this.Cursor = currentCursor;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize) - 1;
            Get_Client_Wise_Production_Count_Order_Next();
          
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;

            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentpageindex >= 1)
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
                Get_Client_Wise_Production_Count_Order_Next();
                this.Cursor = currentCursor;
            }
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
            Get_Client_Wise_Production_Count_Order_Next();
            this.Cursor = currentCursor;
        }
    }
}
