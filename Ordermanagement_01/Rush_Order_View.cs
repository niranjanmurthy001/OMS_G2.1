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
using ClosedXML.Excel;
using System.IO;
namespace Ordermanagement_01
{
    public partial class Rush_Order_View : Form
    {
        System.Data.DataTable dtexport = new System.Data.DataTable();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        
        string Operation;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        //InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        DataSet ds = new DataSet();
        int Userid; string userroleid;
        string Production_Date;
        string Path1;
        public Rush_Order_View(string OPERATION, int userid, string User_Roleid, string PRODUCTION_DATE)
        {
            InitializeComponent();
            Operation = OPERATION.ToString();
            Userid = userid;
            userroleid = User_Roleid;
            Production_Date = PRODUCTION_DATE;
        }

        private void Rush_Order_View_Load(object sender, EventArgs e)
        {
            if (Operation == "GET_RUSH_ORDERS")
            {


                lbl_Header.Text = "RUSH ORDERS LIST";

            }
            else if (Operation == "GET_OVER_DUE_ORDER")
            {
                lbl_Header.Text = "OVER DUE ORDERS";

            }
            Get_Count_Of_Order_Type_Wise();
        }
        protected void Get_Count_Of_Order_Type_Wise()
        {

         
            Hashtable httargetorder = new Hashtable();
            System.Data.DataTable dttargetorder = new System.Data.DataTable();
            dtexport.Clear();
            httargetorder.Add("@Trans", Operation);
         
            dttargetorder = dataaccess.ExecuteSP("Sp_Order_Status_Report", httargetorder);
            
            if (dttargetorder.Rows.Count > 0)
            {

                dtexport = dttargetorder;

                if (dttargetorder.Rows.Count > 0)
                {
                    grd_Targetorder.DataSource = null;

                    grd_Targetorder.AutoGenerateColumns = false;
                    grd_Targetorder.ColumnCount = 17;

                    grd_Targetorder.Columns[0].Name = "SNo";
                    grd_Targetorder.Columns[0].HeaderText = "S. No";
                    grd_Targetorder.Columns[0].Width = 65;

                    grd_Targetorder.Columns[1].Name = "Order Number";
                    grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                    grd_Targetorder.Columns[1].DataPropertyName = "Order_Number";
                    grd_Targetorder.Columns[1].Width = 200;
                    grd_Targetorder.Columns[1].Visible = false;

                    DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                    grd_Targetorder.Columns.Add(link);
                    link.Name = "Order Number";
                    link.HeaderText = "ORDER NUMBER";
                    link.DataPropertyName = "Order_Number";
                    link.Width = 200;
                    link.DisplayIndex = 1;

                    if (userroleid == "1")
                    {
                        grd_Targetorder.Columns[2].Name = "Customer";
                        grd_Targetorder.Columns[2].HeaderText = "CLIENT NAME";
                        grd_Targetorder.Columns[2].DataPropertyName = "Client_name";
                        grd_Targetorder.Columns[2].Width = 140;

                        grd_Targetorder.Columns[3].Name = "SubProcess";
                        grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                        grd_Targetorder.Columns[3].DataPropertyName = "Sub_client";
                        grd_Targetorder.Columns[3].Width = 220;
                    }
                    else
                    {
                        grd_Targetorder.Columns[2].Name = "Client_Number";
                        grd_Targetorder.Columns[2].HeaderText = "CLIENT NAME";
                        grd_Targetorder.Columns[2].DataPropertyName = "Client_Number";
                        grd_Targetorder.Columns[2].Width = 140;

                        grd_Targetorder.Columns[3].Name = "Subprocess_Number";
                        grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                        grd_Targetorder.Columns[3].DataPropertyName = "Subprocess_Number";
                        grd_Targetorder.Columns[3].Width = 220;
                    
                    }


                    grd_Targetorder.Columns[4].Name = "Submited";
                    grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                    grd_Targetorder.Columns[4].DataPropertyName = "Date";
                    grd_Targetorder.Columns[4].Width = 120;
                    grd_Targetorder.Columns[4].DefaultCellStyle.Format = "MM/dd/yyyy";


                    grd_Targetorder.Columns[5].Name = "OrderType";
                    grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[5].DataPropertyName = "Order_type";
                    grd_Targetorder.Columns[5].Width = 160;


                    grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                    grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                    grd_Targetorder.Columns[6].DataPropertyName = "Ref_number";
                    grd_Targetorder.Columns[6].Width = 170;


                    grd_Targetorder.Columns[7].Name = "SearchType";
                    grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                    grd_Targetorder.Columns[7].DataPropertyName = "County_type";
                    grd_Targetorder.Columns[7].Width = 160;


                    grd_Targetorder.Columns[8].Name = "County";
                    grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                    grd_Targetorder.Columns[8].DataPropertyName = "County";
                    grd_Targetorder.Columns[8].Width = 140;


                    grd_Targetorder.Columns[9].Name = "State";
                    grd_Targetorder.Columns[9].HeaderText = "STATE";
                    grd_Targetorder.Columns[9].DataPropertyName = "State";
                    grd_Targetorder.Columns[9].Width = 120;


                    grd_Targetorder.Columns[10].Name = "Task";
                    grd_Targetorder.Columns[10].HeaderText = "TASK";
                    grd_Targetorder.Columns[10].DataPropertyName = "Current_Task";
                    grd_Targetorder.Columns[10].Width = 120;


                    grd_Targetorder.Columns[11].Name = "STATUS";
                    grd_Targetorder.Columns[11].HeaderText = "STATUS";
                    grd_Targetorder.Columns[11].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[11].Width = 100;


                    grd_Targetorder.Columns[12].Name = "Order_ID";
                    grd_Targetorder.Columns[12].HeaderText = "Order_ID";
                    grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[12].Width = 100;
                    grd_Targetorder.Columns[12].Visible = false;

                    grd_Targetorder.Columns[13].Name = "User";
                    grd_Targetorder.Columns[13].HeaderText = "User Name";
                    grd_Targetorder.Columns[13].DataPropertyName = "UserName";
                    grd_Targetorder.Columns[13].Width = 100;


                    grd_Targetorder.Columns[14].Name = "Tat_Start_Date_Time";
                    grd_Targetorder.Columns[14].HeaderText = "ORDER START TIME";
                    grd_Targetorder.Columns[14].DataPropertyName = "Tat_Start_Date_Time";
                    grd_Targetorder.Columns[14].Width = 100;


                    grd_Targetorder.Columns[15].Name = "ORDER END TIME";
                    grd_Targetorder.Columns[15].HeaderText = "ORDER END TIME";
                    grd_Targetorder.Columns[15].DataPropertyName = "Tat_End_Date_Time";
                    grd_Targetorder.Columns[15].Width = 100;


                    grd_Targetorder.Columns[16].Name = "DIFFERENCE HOURS";
                    grd_Targetorder.Columns[16].HeaderText = "DIFFERENCE HOURS";
                    grd_Targetorder.Columns[16].DataPropertyName = "DIFF";
                    grd_Targetorder.Columns[16].Width = 100;

                 
                    //  grd_Targetorder.Columns[12].Visible = false;
                    grd_Targetorder.DataSource = dttargetorder;


                    //lbl_Total.Text = dttargetorder.Rows.Count.ToString();

                }
                else
                {

                    grd_Targetorder.DataSource = null;
                    grd_Targetorder.Rows.Clear();
                    //lbl_Total.Text = "0";
                    //grd_Targetorder.DataBind();
                }


                for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
                {
                    grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
                }
                


            }
            else
            {
                grd_Targetorder.DataSource = null;


            }



        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            //cProbar.startProgress();
            form_loader.Start_progres();
            //ds.Tables.Clear();
            //ds.Clear();
            //ds.Tables.Add(dtexport);

            //Convert_Dataset_to_Excel();

            Export_ReportData();
           
           
            //cProbar.stopProgress();
        }

        private void Export_ReportData()
        {



            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in grd_Targetorder.Columns)
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
            foreach (DataGridViewRow row in grd_Targetorder.Rows)
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


           string Export_Title_Name = "Report";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Report");


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            //  System.Diagnostics.Process.Start(Path1);




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

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
            
        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            DataView dtsearch = new DataView(dtexport);
            if (txt_SearchOrdernumber.Text != "")
            {
                string search = txt_SearchOrdernumber.Text.ToString();
                dtsearch.RowFilter = "Order_Number like '%" + search.ToString() + "%'";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = dtsearch.ToTable();
                if (dt.Rows.Count > 0)
                {
                    grd_Targetorder.DataSource = null;

                    grd_Targetorder.AutoGenerateColumns = false;
                    grd_Targetorder.ColumnCount = 17;

                    grd_Targetorder.Columns[0].Name = "SNo";
                    grd_Targetorder.Columns[0].HeaderText = "S. No";
                    grd_Targetorder.Columns[0].Width = 65;

                    grd_Targetorder.Columns[1].Name = "Order Number";
                    grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                    grd_Targetorder.Columns[1].DataPropertyName = "Order_Number";
                    grd_Targetorder.Columns[1].Width = 200;
                    grd_Targetorder.Columns[1].Visible = false;

                    DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                    grd_Targetorder.Columns.Add(link);
                    link.Name = "Order Number";
                    link.HeaderText = "ORDER NUMBER";
                    link.DataPropertyName = "Order_Number";
                    link.Width = 200;
                    link.DisplayIndex = 1;

                    grd_Targetorder.Columns[2].Name = "Customer";
                    grd_Targetorder.Columns[2].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "Client_name";
                    grd_Targetorder.Columns[2].Width = 140;

                    grd_Targetorder.Columns[3].Name = "SubProcess";
                    grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                    grd_Targetorder.Columns[3].DataPropertyName = "Sub_client";
                    grd_Targetorder.Columns[3].Width = 220;


                    grd_Targetorder.Columns[4].Name = "Submited";
                    grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                    grd_Targetorder.Columns[4].DataPropertyName = "Date";
                    grd_Targetorder.Columns[4].Width = 120;


                    grd_Targetorder.Columns[5].Name = "OrderType";
                    grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[5].DataPropertyName = "Order_type";
                    grd_Targetorder.Columns[5].Width = 160;


                    grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                    grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                    grd_Targetorder.Columns[6].DataPropertyName = "Ref_number";
                    grd_Targetorder.Columns[6].Width = 170;


                    grd_Targetorder.Columns[7].Name = "SearchType";
                    grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                    grd_Targetorder.Columns[7].DataPropertyName = "County_type";
                    grd_Targetorder.Columns[7].Width = 160;


                    grd_Targetorder.Columns[8].Name = "County";
                    grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                    grd_Targetorder.Columns[8].DataPropertyName = "County";
                    grd_Targetorder.Columns[8].Width = 140;


                    grd_Targetorder.Columns[9].Name = "State";
                    grd_Targetorder.Columns[9].HeaderText = "STATE";
                    grd_Targetorder.Columns[9].DataPropertyName = "State";
                    grd_Targetorder.Columns[9].Width = 120;


                    grd_Targetorder.Columns[10].Name = "Task";
                    grd_Targetorder.Columns[10].HeaderText = "TASK";
                    grd_Targetorder.Columns[10].DataPropertyName = "Current_Task";
                    grd_Targetorder.Columns[10].Width = 120;


                    grd_Targetorder.Columns[11].Name = "STATUS";
                    grd_Targetorder.Columns[11].HeaderText = "STATUS";
                    grd_Targetorder.Columns[11].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[11].Width = 100;


                    grd_Targetorder.Columns[12].Name = "Order_ID";
                    grd_Targetorder.Columns[12].HeaderText = "Order_ID";
                    grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[12].Width = 100;
                    grd_Targetorder.Columns[12].Visible = false;

                    grd_Targetorder.Columns[13].Name = "User";
                    grd_Targetorder.Columns[13].HeaderText = "User Name";
                    grd_Targetorder.Columns[13].DataPropertyName = "UserName";
                    grd_Targetorder.Columns[13].Width = 100;


                    grd_Targetorder.Columns[14].Name = "Tat_Start_Date_Time";
                    grd_Targetorder.Columns[14].HeaderText = "ORDER START TIME";
                    grd_Targetorder.Columns[14].DataPropertyName = "Tat_Start_Date_Time";
                    grd_Targetorder.Columns[14].Width = 100;


                    grd_Targetorder.Columns[15].Name = "ORDER END TIME";
                    grd_Targetorder.Columns[15].HeaderText = "ORDER END TIME";
                    grd_Targetorder.Columns[15].DataPropertyName = "Tat_End_Date_Time";
                    grd_Targetorder.Columns[15].Width = 100;


                    grd_Targetorder.Columns[16].Name = "DIFFERENCE HOURS";
                    grd_Targetorder.Columns[16].HeaderText = "DIFFERENCE HOURS";
                    grd_Targetorder.Columns[16].DataPropertyName = "DIFF";
                    grd_Targetorder.Columns[16].Width = 100;


                    //  grd_Targetorder.Columns[12].Visible = false;
                    grd_Targetorder.DataSource = dt;
                }
            }
        }

        private void grd_Targetorder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 17)
            {
                //cProbar.startProgress();
                form_loader.Start_progres();

                Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(grd_Targetorder.Rows[e.RowIndex].Cells[12].Value.ToString()), Userid, userroleid,"");
                OrderEntry.Show();
                //cProbar.stopProgress();
            }
        }

    }
}
