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
using ClosedXML.Excel;

namespace Ordermanagement_01.Reports
{
    public partial class Order_Report_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_status, Order_Progress,Userid;
        DateTime From_Date, To_Date; string userroleid, Export_Title_Name, Path1;
        string Production_Date;
        public Order_Report_View(int orderstatus, int progress, DateTime fromdate, DateTime todate, int userid, string User_roleid, string PRODUCTION_DATE)
        {
            InitializeComponent();
            Order_status = orderstatus;
            Order_Progress = progress;
            From_Date = fromdate;
            To_Date = todate;
            Userid = userid;
            userroleid = User_roleid;
            Production_Date = PRODUCTION_DATE;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in grd_Rpt_order_source.Columns)
            {
                if (column.HeaderText != "" && column.HeaderText != "Orderid")
                {
                    if (column.ValueType == null )
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
            foreach (DataGridViewRow row in grd_Rpt_order_source.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();
                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {

                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex-1] = cell.Value.ToString();
                        }
                    }
                }
            }


            Export_Title_Name = "Client_Production";
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

            System.Diagnostics.Process.Start(Path1);

        }

        private void Order_Report_View_Load(object sender, EventArgs e)
        {
            Hashtable htinfo = new Hashtable();
            DataTable dtinfo = new DataTable();
            htinfo.Add("@Trans", "EXPORT_BIND_STATUS_WISE_PROGRESS_WISE");
            htinfo.Add("@From_Date", From_Date);
            htinfo.Add("@To_Date", To_Date);
            htinfo.Add("@Order_Progress", Order_Progress);
            htinfo.Add("@Order_Status", Order_status);
            dtinfo = dataaccess.ExecuteSP("Sp_Order_Task_wise_Report", htinfo);
            if (dtinfo.Rows.Count > 0)
            {
                grd_Rpt_order_source.Rows.Clear();
                for (int i = 0; i < dtinfo.Rows.Count; i++)
                {
                    grd_Rpt_order_source.Rows.Add();
                    grd_Rpt_order_source.Rows[i].Cells[1].Value = i+1;
                    grd_Rpt_order_source.Rows[i].Cells[2].Value = dtinfo.Rows[i]["Client_Order_Number"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[3].Value = dtinfo.Rows[i]["Client Name"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[4].Value = dtinfo.Rows[i]["Sub_ProcessName"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[5].Value = dtinfo.Rows[i]["State"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[6].Value = dtinfo.Rows[i]["County"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[7].Value = dtinfo.Rows[i]["Order_Type"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[8].Value = dtinfo.Rows[i]["Order_Type_Abbreviation"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[9].Value = dtinfo.Rows[i]["Borrower_Name"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[10].Value = dtinfo.Rows[i]["User_Name"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[0].Value = dtinfo.Rows[i]["Order_ID"].ToString();
                }
            }
            lbl_Total_orders.Text = "Total Orders : " + dtinfo.Rows.Count;
        }

        private void grd_Rpt_order_source_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                //order view order id 10
                Ordermanagement_01.Order_Entry entry = new Ordermanagement_01.Order_Entry(int.Parse(grd_Rpt_order_source.Rows[e.RowIndex].Cells[0].Value.ToString()), Userid, userroleid,"");
                entry.Show();
            }
        }
    }
}
