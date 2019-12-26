using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
namespace Ordermanagement_01
{
    public partial class Todays_Orders : Form
    {
          Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DataSet ds = new DataSet();
        string date_report;
        Microsoft.Office.Interop.Excel.DataTable Excel_Data;
        DropDownistBindClass dbc = new DropDownistBindClass();
        public Todays_Orders(string Date_Orders)
        {
            InitializeComponent();
            date_report = Date_Orders;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Grid();
        }
        private void Load_Grid()
        {
             Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            htselect.Add("@Trans", "Todays_Orders");
            htselect.Add("@Date_Orders", date_report);
                dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
                grd_order.Columns[12].Visible = true;
      
            DataSet ds = new DataSet();
              ds.Tables.Add(dtselect);

          //  Excel_Data = ds;
            if (dtselect.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {

                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[0].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Name"].ToString();
                    grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["County_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["County"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["User_Name"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[12].Value ="Delete";
                    grd_order.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.PowderBlue;
                  //  grd_order.Rows[i].Cells[12].Style.BackColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                grd_order.Visible = true;
                grd_order.DataSource = null;
            }
            lbl_Total_orders.Text = dtselect.Rows.Count.ToString();
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            Load_Grid();
        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_order.Rows)
            {
                if (txt_SearchOrdernumber.Text != "")
                {
                    if (row.Cells[0].Value.ToString().StartsWith(txt_SearchOrdernumber.Text.ToString(), true, CultureInfo.InvariantCulture))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
                else
                {
                    row.Visible = true;
                }
            }
        }
    }
}
