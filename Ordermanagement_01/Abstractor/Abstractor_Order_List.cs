using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Order_List : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Month, Year, Abstractor_Id,Abstractor_Name;
        public Abstractor_Order_List(string MONTH,string YEAR,string ABSTRACTOR_ID,string ABS_NAME)
        {
            InitializeComponent();
            Month = MONTH;
            Year = YEAR;
            Abstractor_Id = ABSTRACTOR_ID;
            Abstractor_Name = ABS_NAME.ToString();
            lbl_Abstractor_Name.Text = Abstractor_Name.ToString();


        }
        private void Gridview_Bind_OrderList()
        {



            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();


            htuser.Add("@Trans", "GET_ABSTRACTOR_ORDERS_COST_LIST");
            htuser.Add("@Month",Month);
            htuser.Add("@Year", Year);
            htuser.Add("@Abstractor_Id",Abstractor_Id);
            dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
            grd_order.Columns[0].Width = 36;

            grd_order.Columns[1].Width = 150;
            grd_order.Columns[2].Width = 100;
            grd_order.Columns[3].Width = 100;
            grd_order.Columns[4].Width = 100;
            grd_order.Columns[5].Width = 120;
            grd_order.Columns[6].Width = 100;
            grd_order.Columns[7].Width = 50;
    

            if (dtuser.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_order.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[0].Value = i + 1;
                    grd_order.Rows[i].Cells[1].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Month_Name"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Actual_Cost"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Pages_Cost"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Return_Date"].ToString();
                    grd_order.Rows[i].Cells[6].Value = "due";
                    grd_order.Rows[i].Cells[7].Value = dtuser.Rows[i]["Order_ID"].ToString();
                  


                }
                // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.DataSource = null;
                // lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }

        }

        private void Bind_Total_Orders_Cost()
        {
            Hashtable httotal = new Hashtable();
            DataTable dttotal = new System.Data.DataTable();


            httotal.Add("@Trans", "GET_TOTAL_COST");
            httotal.Add("@Month", Month);
            httotal.Add("@Year", Year);
            httotal.Add("@Abstractor_Id",Abstractor_Id);
            dttotal = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", httotal);
            if (dttotal.Rows.Count > 0)
            {
                txt_No_Of_orders.Text = grd_order.Rows.Count.ToString();
                txt_Actual_Cost.Text = dttotal.Rows[0]["Actual_Cost"].ToString();
                txt_Pages_Cost.Text = dttotal.Rows[0]["Pages_Cost"].ToString();
                txt_Total.Text = dttotal.Rows[0]["Total_Cost"].ToString();


            }
            else
            {
                txt_No_Of_orders.Text = "0";
                txt_Actual_Cost.Text = "0";
                txt_Pages_Cost.Text = "0";
                txt_Total.Text = "0";

            }

        }

        private void Abstractor_Order_List_Load(object sender, EventArgs e)
        {
            Gridview_Bind_OrderList();
            Bind_Total_Orders_Cost();

        }

    }
}
