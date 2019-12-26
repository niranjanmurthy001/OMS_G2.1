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
    public partial class Subcription_Report_Detailed_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Client_ID,Website_id;
        string Client_Name,Website_Name,To_date;
        string Path1;
        string Export_Title_Name,typesub;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Subcription_Report_Detailed_View(int CLIENT_ID, string CLIENT_NAME,int WEBSITE_ID,string WEBSITE_NAME,string TO_DATE,string Type_Sub)
        {
            InitializeComponent();
            Client_ID = CLIENT_ID;
            Website_id = WEBSITE_ID;
            Client_Name = CLIENT_NAME.ToString();
            Website_Name = WEBSITE_NAME.ToString();
            typesub = Type_Sub;
            To_date = TO_DATE;
            this.Text = "" + Client_Name.ToString() + "-" + Website_Name + "-"+To_date+"- Subscripition Report View";
            lbl_Header.Text = this.Text;
            Export_Title_Name = "Orders Subscription Report";
        }

        private void Subcription_Report_Detailed_View_Load(object sender, EventArgs e)
        {
            if (typesub == "USER WISE")
            {
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new System.Data.DataTable();
                htselect.Add("@Trans", "GET_SUBSCRIPTION_ORDERS_DATE_WISE");
                htselect.Add("@client_Id", Client_ID);
                htselect.Add("@To_Date", To_date);
                htselect.Add("@User_Password_Id", Website_id);
                dtselect = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htselect);
                if (dtselect.Rows.Count > 0)
                {

                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.Rows.Clear();
                    Grd_OrderTime.Columns.Clear();
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.AutoGenerateColumns = true;
                    Grd_OrderTime.Refresh();



                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.DataSource = dtselect;


                    ArrangeGrid(Grd_OrderTime);
                }
                else
                {
                    Grd_OrderTime.Visible = false;
                    Grd_OrderTime.DataSource = null;

                }

            }
            else if (typesub == "CLIENT WISE")
            {
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new System.Data.DataTable();
                if (Website_id != 43)
                {
                    htselect.Add("@Trans", "GET_SUBSCRIPTION_ORDERS_DATE_WISE");
                }
                else if (Website_id==43)
                {

                    htselect.Add("@Trans", "GET_SUBSCRIPTION_ORDERS_DATE_WISE_FOR_OTHER_WEBSITE");
                }

                htselect.Add("@client_Id", Client_ID);
                htselect.Add("@To_Date", To_date);
                htselect.Add("@User_Password_Id", Website_id);
                dtselect = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htselect);
                if (dtselect.Rows.Count > 0)
                {

                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.Rows.Clear();
                    Grd_OrderTime.Columns.Clear();
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.AutoGenerateColumns = true;
                    Grd_OrderTime.Refresh();



                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.DataSource = dtselect;


                    ArrangeGrid(Grd_OrderTime);
                }
                else
                {
                    Grd_OrderTime.Visible = false;
                    Grd_OrderTime.DataSource = null;

                }
            }
        }
        public static void ArrangeGrid(DataGridView Grid)
        {
            int twidth = 0;
            if (Grid.Rows.Count > 0)
            {
                twidth = (Grid.Width * Grid.Columns.Count) / 25;
                for (int i = 0; i < Grid.Columns.Count; i++)
                {
                    Grid.Columns[i].Width = twidth;
                }

            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Export_ReportData();
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
    }
}
