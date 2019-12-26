using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using ClosedXML.Excel;
namespace Ordermanagement_01.Reports
{
    public partial class Report_Subscription_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Client_ID, userpassword;
        string Client_Name;
        string Path1;
        string Export_Title_Name,type_sub,web;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Report_Subscription_View(int CLIENT_ID, string CLIENT_NAME, string Type_Sub, string website_Name)
        {
            InitializeComponent();
            Client_ID = CLIENT_ID;
            Client_Name = CLIENT_NAME.ToString();
            web = website_Name;
          //  userpassword = Userpassword;
            type_sub = Type_Sub;
            this.Text = "" + Client_Name.ToString() + "- Subscripition Report View";
            lbl_Header.Text = this.Text;
            Export_Title_Name = "Date Wise Subscription Report";

        }

        private void Report_Subscription_View_Load(object sender, EventArgs e)
        {
            if (type_sub == "CLIENT WISE")
            {
                Hashtable htuser = new Hashtable();
                DataTable dtuser = new DataTable();
                htuser.Add("@Trans", "GET_USERPASSWORD_CLIENT");
                htuser.Add("@client_Id", Client_ID);
                htuser.Add("@webSite_Name", web);
                dtuser = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htuser);
                if (dtuser.Rows.Count > 0)
                {
                    userpassword = int.Parse(dtuser.Rows[0]["User_Password_Id"].ToString());

                    Hashtable htselect = new Hashtable();
                    DataTable dtselect = new System.Data.DataTable();
                    htselect.Add("@Trans", "SELECT_DATE_WISE_CLIENT_WISE");
                    htselect.Add("@client_Id", Client_ID);
                    htselect.Add("@User_Passwordid", Convert.ToString(userpassword));
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
                        Grd_OrderTime.Columns[0].Visible = false;



                        ArrangeGrid(Grd_OrderTime);
                    }
                    else
                    {
                        Grd_OrderTime.Visible = false;
                        Grd_OrderTime.DataSource = null;

                    }
                }
                
            }
            else if (type_sub == "USER WISE")
            {
                Hashtable htuser = new Hashtable();
                DataTable dtuser = new DataTable();
                htuser.Add("@Trans", "GET_USERPASSWORD_CLIENT");
                htuser.Add("@client_Id", Client_ID);
                htuser.Add("@webSite_Name", web);
                dtuser = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htuser);
                {
                    userpassword = int.Parse(dtuser.Rows[0]["User_Password_Id"].ToString());

                    Hashtable htselect = new Hashtable();
                    DataTable dtselect = new System.Data.DataTable();
                    htselect.Add("@Trans", "SELECT_DATE_WISE_USER_WISE");
                    htselect.Add("@userid", Client_ID);
                    htselect.Add("@User_Passwordid", Convert.ToString(userpassword));
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
                        Grd_OrderTime.Columns[0].Visible = false;



                        ArrangeGrid(Grd_OrderTime);
                    }
                    else
                    {
                        Grd_OrderTime.Visible = false;
                        Grd_OrderTime.DataSource = null;

                    }
                }
                
            }
        
           



            
        }

        public static void ArrangeGrid(DataGridView Grid)
        {
            int twidth = 0;
            if (Grid.Rows.Count > 0)
            {
                twidth = (Grid.Width * Grid.Columns.Count) / 50;
                for (int i = 0; i < Grid.Columns.Count; i++)
                {
                    Grid.Columns[i].Width = twidth;
                }

            }
        }

        private void Grd_OrderTime_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 1)
            {
               
                    int Indexvalue = e.ColumnIndex;
                    int Website_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());
                    string Website = Grd_OrderTime.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string To_Date = Grd_OrderTime.Columns[Indexvalue].HeaderText;

                    Subcription_Report_Detailed_View sv = new Subcription_Report_Detailed_View(Client_ID, Client_Name, Website_Id, Website, To_Date,type_sub);
                    sv.Show();
                
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
