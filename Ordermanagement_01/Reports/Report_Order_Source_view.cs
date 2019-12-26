using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ClosedXML.Excel;
using System.IO;

namespace Ordermanagement_01.Reports
{
    public partial class Report_Order_Source_view : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_Id,Employeesourceid, Clientid, Order_sourceid;
        string Userroleid, Order_source, Header_Text, Fromdate, Todate, Path1, Export_Title_Name,Client_Name,user_roleid;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Report_Order_Source_view(int userid, string userroleid, int clientid, string headertext, int ordersourceid, string order_source, string fromdate, string todate, string clientname, string Userroleid, string PRODUCTION_DATE)
        {
            InitializeComponent();
            User_Id = userid;
            Userroleid = userroleid;
            Clientid = clientid;
            Client_Name = clientname;
            Header_Text = headertext;
            
            Order_sourceid = ordersourceid;
            Order_source = order_source;
            Fromdate = fromdate;
            Todate = todate;
            user_roleid = Userroleid;
        }

        private void Report_Order_Source_view_Load(object sender, EventArgs e)
        {
            Lbl_Title.Text = Header_Text;

            //Bind_Order_Details();


            if (Order_source == "" && Order_sourceid == 0)
            {
                Lbl_Title.Text = Header_Text.ToString() + " 's Ordes";
                Bind_Received_Order_detail();
            }
            else if (Header_Text != "No of Hits" && Header_Text != "No of Documents")
            {
                Lbl_Title.Text = Client_Name.ToString() +" 's Client Order ";
                Bind_Order_Details();
            }
           
            else 
            {
                Bind_Order_Details();
            }
            
            
        }
        private void Bind_Received_Order_detail()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "BIND_ORDER_INFO");
            ht.Add("@Client_Id", Clientid);
            ht.Add("@Date", Header_Text);
            
            dt = dataaccess.ExecuteSP("Sp_Order_Received_Report", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Rpt_order_source.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Rpt_order_source.Rows.Add();
                    grd_Rpt_order_source.Rows[i].Cells[0].Value = i + 1;
                    grd_Rpt_order_source.Rows[i].Cells[1].Value = dt.Rows[i]["Client_Order_Number"].ToString();

                    grd_Rpt_order_source.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Name"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[3].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[4].Value = dt.Rows[i]["State"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[5].Value = dt.Rows[i]["County"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[6].Value = dt.Rows[i]["Order_Type"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[7].Value = dt.Rows[i]["Order_Type_Abbreviation"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[8].Value = dt.Rows[i]["Borrower_Name"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[9].Value = dt.Rows[i]["Production_date"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[10].Value = dt.Rows[i]["Order_ID"].ToString();
                    Column12.Visible = false;
                    Column13.Visible = false;
                }
            }
            lbl_Total_orders.Text = "Total Orders : " + dt.Rows.Count;
        }
        private void Bind_Order_Details()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            if (Header_Text == "No of Hits")
            {
                htselect.Add("@Trans", "OPEN_CLIENT_ORDER_SOURCE_NO_HITS_INFO_DETAILS");
                htselect.Add("@Client", Clientid);
                htselect.Add("@From_Date", Fromdate);
                htselect.Add("@To_Date", Todate);
                htselect.Add("@Employee_Source_id", Order_sourceid); 
            }
            else if (Header_Text == "No of Documents")
            {
                htselect.Add("@Trans", "OPEN_CLIENT_ORDER_SOURCE_NO_DOC_INFO_DETAILS");
                htselect.Add("@Client", Clientid);
                htselect.Add("@From_Date", Fromdate);
                htselect.Add("@To_Date", Todate);
                htselect.Add("@Employee_Source_id", Order_sourceid);
            }
            else
            {
                htselect.Add("@Trans", "OPEN_CLIENT_ORDER_SOURCE_NO_HITS_DOC_INFO_DETAILS");
                htselect.Add("@Client", Clientid);
                htselect.Add("@From_Date", Fromdate);
                htselect.Add("@To_Date", Todate);
                htselect.Add("@Employee_Source_id", Order_sourceid);
            }

            dtselect = dataaccess.ExecuteSP("Sp_Order_Source", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_Rpt_order_source.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Rpt_order_source.Rows.Add();
                    grd_Rpt_order_source.Rows[i].Cells[0].Value = i + 1;
                    grd_Rpt_order_source.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                    
                    grd_Rpt_order_source.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[4].Value = dtselect.Rows[i]["State"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[5].Value = dtselect.Rows[i]["County"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[6].Value = dtselect.Rows[i]["Order_Type"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[7].Value = dtselect.Rows[i]["Order_Type_Abbreviation"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[8].Value = dtselect.Rows[i]["Borrower_Name"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[9].Value = dtselect.Rows[i]["Production_date"].ToString();
                    grd_Rpt_order_source.Rows[i].Cells[10].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    Column12.Visible = true;
                    Column13.Visible = true;
                    grd_Rpt_order_source.Rows[i].Cells[11].Value = int.Parse(dtselect.Rows[i]["No_of_Hits"].ToString());
                    grd_Rpt_order_source.Rows[i].Cells[12].Value = int.Parse(dtselect.Rows[i]["No_Of_Documents"].ToString());
                }
            }
            lbl_Total_orders.Text = "Total Orders : " + dtselect.Rows.Count;
        }

        private void grd_Rpt_order_source_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                int orderid = int.Parse(grd_Rpt_order_source.Rows[e.RowIndex].Cells[10].Value.ToString());
                Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(orderid, User_Id, Userroleid,"");
                orderentry.Show();
            }
                 
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn column in grd_Rpt_order_source.Columns)
            {
                if (column.HeaderText != "")
                {
                    if (column.ValueType == null  )
                    {
                        if (column.HeaderText == "No of Hits" || column.HeaderText == "No of Documents" || column.HeaderText == "Order Number" )
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));
                        }
                       
                        else if(column.HeaderText != "Orderid")
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        

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

                    if (cell.Value != null && cell.Value.ToString() != "")
                    {
                        if (cell.ColumnIndex == 11 )
                        {
                            dt.Rows[dt.Rows.Count - 1][10] = int.Parse(cell.Value.ToString());
                        }
                        else if (cell.ColumnIndex == 12)
                        {
                            dt.Rows[dt.Rows.Count - 1][11] = int.Parse(cell.Value.ToString());
                        }

                        else if (cell.ColumnIndex == 12)
                        {

                        }
                        else
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                        
                    }
                }
            }
            if (Header_Text == "No of Hits" || Header_Text == "No of Documents")
            {
                Export_Title_Name = "Order Source Report";
            }
            else
            {
                Export_Title_Name = "Order Received Report";
            }
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
    }
}
