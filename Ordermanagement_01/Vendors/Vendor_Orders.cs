using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using ClosedXML.Excel;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Orders : Form
    {
        int Order_Progress,Vendor_Id,User_Id;
        string Operation;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        string Path1;
        int User_Role_Id;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Vendor_Orders(int ORDER_PROGRESS,string OPERATION,int VENDOR_ID,int USER_ID,int USER_ROLE_ID)
        {
            InitializeComponent();

            Vendor_Id = VENDOR_ID;
            User_Id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
            Order_Progress = ORDER_PROGRESS;
            Operation = OPERATION;


            if (Order_Progress == 13)
            {

                Lbl_Title.Text = "VENDOR WAITING FOR ACCEPTANCE ORDERS";
              


            }
            else if (Order_Progress == 6)
            {

                Lbl_Title.Text = "VENDOR WORK IN PROGRESS ORDERS";



            }
            else if (Order_Progress == 3)
            {

                Lbl_Title.Text = "VENDOR RETURNED ORDERS";


            }
            else if (Order_Progress == 16)
            {

                Lbl_Title.Text = "VENDOR REJECTED ORDERS";


            }
            this.Text = Lbl_Title.Text;
        }

        private void Vendor_Orders_Load(object sender, EventArgs e)
        {
            Load_Vendor_Production_Report();
            this.WindowState = FormWindowState.Maximized;
        }


        private void Load_Vendor_Production_Report()
        {
         


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();
            System.Data.DataTable dt_orders = new System.Data.DataTable();

            dt_Status.Rows.Clear();
           
            ht_Status.Clear();
            dt_Status.Clear();
            string Client, SubProcess;


            ht_Status.Add("@Trans", Operation);
           ht_Status.Add("@vendor_Id", Vendor_Id);
           

            dt_Status = dataaccess.ExecuteSP("Sp_Vendor_Order_Report", ht_Status);




            dt_orders.Clear();
            dt_orders = dt_Status;


            if (dt_orders.Rows.Count > 0)
            {


                //    Grd_OrderTime.Rows.Clear();
                //Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;

                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.AutoGenerateColumns = false;

                Grd_OrderTime.ColumnCount = 16;
                //Grd_OrderTime.Rows.Add();
                Grd_OrderTime.Columns[0].Name = "Orderid";
                Grd_OrderTime.Columns[0].HeaderText = "Order Id";
                Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                Grd_OrderTime.Columns[0].Width = 50;
                Grd_OrderTime.Columns[0].Visible = false;



                Grd_OrderTime.Columns[1].Name = "Assigned Date";
                Grd_OrderTime.Columns[1].HeaderText = "Assigned Date";
                Grd_OrderTime.Columns[1].DataPropertyName = "Assigned Date";
                Grd_OrderTime.Columns[1].Width = 140;

                Grd_OrderTime.Columns[2].Name = "Completed Date";
                Grd_OrderTime.Columns[2].HeaderText = "Completed Date";
                Grd_OrderTime.Columns[2].DataPropertyName = "Completed Date";
                Grd_OrderTime.Columns[2].Width = 140;



                Grd_OrderTime.Columns[3].Name = "Client_Order_Number";
                Grd_OrderTime.Columns[3].HeaderText = "Client_Order_Number";
                Grd_OrderTime.Columns[3].DataPropertyName = "Client_Order_Number";
                Grd_OrderTime.Columns[3].Width = 120;


                Grd_OrderTime.Columns[4].Name = "Client_Name";
                Grd_OrderTime.Columns[4].HeaderText = "Client Name";
                Grd_OrderTime.Columns[4].DataPropertyName = "Client_Name";
                Grd_OrderTime.Columns[4].Width = 120;

                Grd_OrderTime.Columns[5].Name = "Sub_ProcessName";
                Grd_OrderTime.Columns[5].HeaderText = "Sub process";
                Grd_OrderTime.Columns[5].DataPropertyName = "Sub_ProcessName";
                Grd_OrderTime.Columns[5].Width = 120;

                Grd_OrderTime.Columns[6].Name = "Order_Number";
                Grd_OrderTime.Columns[6].HeaderText = "Order_Number";
                Grd_OrderTime.Columns[6].DataPropertyName = "Order_Number";
                Grd_OrderTime.Columns[6].Width = 120;


                Grd_OrderTime.Columns[7].Name = "Order_Type";
                Grd_OrderTime.Columns[7].HeaderText = "Order Type";
                Grd_OrderTime.Columns[7].DataPropertyName = "Order_Type";
                Grd_OrderTime.Columns[7].Width = 120;


                Grd_OrderTime.Columns[8].Name = "Property Address";
                Grd_OrderTime.Columns[8].HeaderText = "Property Address";
                Grd_OrderTime.Columns[8].DataPropertyName = "Property Address";
                Grd_OrderTime.Columns[8].Width = 120;

                Grd_OrderTime.Columns[9].Name = "Abbreviation";
                Grd_OrderTime.Columns[9].HeaderText = "State";
                Grd_OrderTime.Columns[9].DataPropertyName = "Abbreviation";
                Grd_OrderTime.Columns[9].Width = 200;

                Grd_OrderTime.Columns[10].Name = "County";
                Grd_OrderTime.Columns[10].HeaderText = "County";
                Grd_OrderTime.Columns[10].DataPropertyName = "County";
                Grd_OrderTime.Columns[10].Width = 200;

                Grd_OrderTime.Columns[11].Name = "Borrower_Name";
                Grd_OrderTime.Columns[11].HeaderText = "Borrower Name";
                Grd_OrderTime.Columns[11].DataPropertyName = "Borrower_Name";
                Grd_OrderTime.Columns[11].Width = 300;

                Grd_OrderTime.Columns[12].Name = "Progress_Status";
                Grd_OrderTime.Columns[12].HeaderText = "Status";
                Grd_OrderTime.Columns[12].DataPropertyName = "Progress_Status";
                Grd_OrderTime.Columns[12].Width = 300;

                Grd_OrderTime.Columns[13].Name = "Vendor_Name";
                Grd_OrderTime.Columns[13].HeaderText = "Vendor Name";
                Grd_OrderTime.Columns[13].DataPropertyName = "Vendor_Name";
                Grd_OrderTime.Columns[13].Width = 150;



                Grd_OrderTime.Columns[14].Name = "Comments";
                Grd_OrderTime.Columns[14].HeaderText = "Comments";
                Grd_OrderTime.Columns[14].DataPropertyName = "Comments";
                Grd_OrderTime.Columns[14].Width = 150;

                Grd_OrderTime.Columns[15].Name = "TAT";
                Grd_OrderTime.Columns[15].HeaderText = "TAT";
                Grd_OrderTime.Columns[15].DataPropertyName = "TAT";
                Grd_OrderTime.Columns[15].Width = 150;



                Grd_OrderTime.DataSource = dt_Status;





            }
            else
            {
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (Grd_OrderTime.Rows.Count > 0)
            {
                form_loader.Start_progres();
                Export_ReportData();
            }
            else
            {

                MessageBox.Show("Refresh The Report and Export");
            }
        }

        private void Export_ReportData()
        {



            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
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
            foreach (DataGridViewRow row in Grd_OrderTime.Rows)
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
            string Export_Title_Name = "Vendor_Production";
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, Export_Title_Name.ToString());


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

        private void Grd_OrderTime_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex ==3 || e.ColumnIndex == 6)
                {

                    int Order_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());

                    Vendor_Order_View view = new Vendor_Order_View(Order_Id, User_Id, User_Role_Id);
                    view.Show();



                }

            }
        }
    }
}
