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

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Inhouse_Order_View : Form
    {
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        static int currentpageindex = 0;
        int pagesize = 10;
        Hashtable htselect = new Hashtable();
        DataTable dtselect = new DataTable();
        DataTable dt = new System.Data.DataTable();
        DataTable dtuser = new System.Data.DataTable();
        string Tax_Order_Porgress;
        int User_Id;
        string User_Role_Id;
        public Tax_Inhouse_Order_View(string TAX_ORDER_PROCESS,int USER_ID,string USER_ROLE_ID)
        {
            InitializeComponent();
            Tax_Order_Porgress = TAX_ORDER_PROCESS;
            User_Id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
          
             if (Tax_Order_Porgress == "Cancelled")
            {
                lbl_Header.Text = "TAX CANCELLED ORDERS";

            }
            else if (Tax_Order_Porgress == "Processing")
            {
                lbl_Header.Text = "TAX PROCESSING ORDERS";
            }
            else if (Tax_Order_Porgress == "Hold")
            {
                lbl_Header.Text = "TAX HOLD ORDERS";
            }

            


        }

        private void GetRowTable(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtuser.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        protected void Gridview_Bind_Assigned_Orders()
        {
            Hashtable htuser = new Hashtable();

            if (Tax_Order_Porgress == "Completed")
            {
                htuser.Add("@Trans", "GET_TAX_COMPLETED_ORDERS");
            }
            else if (Tax_Order_Porgress == "Cancelled")
            {
                htuser.Add("@Trans", "GET_TAX_CANCELLED_ORDERS");
            
            }
            else if (Tax_Order_Porgress == "Processing")
            {
                htuser.Add("@Trans", "GET_TAX_PROCESSING_ORDERS");
            }
            else if (Tax_Order_Porgress == "Hold")
            {
                htuser.Add("@Trans", "GET_TAX_PENDING_HOLD_ORDERS");
            }



            dtuser = dataaccess.ExecuteSP("Sp_Tax_Orders", htuser);
            grd_Admin_orders.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_Admin_orders.EnableHeadersVisualStyles = false;
            grd_Admin_orders.Columns[0].Width = 50;
            grd_Admin_orders.Columns[1].Width = 110;
            grd_Admin_orders.Columns[2].Width = 150;
            grd_Admin_orders.Columns[3].Width = 180;
            grd_Admin_orders.Columns[4].Width = 150;
            grd_Admin_orders.Columns[5].Width = 100;
            grd_Admin_orders.Columns[6].Width = 120;
            grd_Admin_orders.Columns[7].Width = 100;
            grd_Admin_orders.Columns[8].Width = 100;
            grd_Admin_orders.Columns[9].Width = 100;
          

            System.Data.DataTable temptable = dtuser;

         
            if (temptable.Rows.Count > 0)
            {
                //ex2.Visible = true;

                //ex2.Visible = true;
                grd_Admin_orders.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_Admin_orders.Rows.Add();
                    grd_Admin_orders.Rows[i].Cells[0].Value = i + 1;
                    if (User_Role_Id == "1")
                    {
                        grd_Admin_orders.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                    }
                    else 
                    {
                        grd_Admin_orders.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Number"].ToString();
                    }
                    if (User_Role_Id == "1")
                    {
                        grd_Admin_orders.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else 
                    {
                        grd_Admin_orders.Rows[i].Cells[2].Value = temptable.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grd_Admin_orders.Rows[i].Cells[3].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                    grd_Admin_orders.Rows[i].Cells[4].Value = temptable.Rows[i]["Order_Type"].ToString();
                    grd_Admin_orders.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Asigned_Type"].ToString();
                    grd_Admin_orders.Rows[i].Cells[6].Value = temptable.Rows[i]["STATE_COUNTY"].ToString();
                    grd_Admin_orders.Rows[i].Cells[7].Value = temptable.Rows[i]["Assigned_Date"].ToString();
                    grd_Admin_orders.Rows[i].Cells[8].Value = temptable.Rows[i]["Progress_Status"].ToString();
                    grd_Admin_orders.Rows[i].Cells[9].Value = temptable.Rows[i]["Order_ID"].ToString();
                 
                }
            }
            else
            {
                grd_Admin_orders.Rows.Clear();
                grd_Admin_orders.DataSource = null;
            }
         

        }


      

      

        private void Bind_Filter_Data()
        {
            DataView dtsearch = new DataView(dtuser);
            dtsearch.RowFilter = "Client_Order_Number like '%" + txt_Order_Number.Text.ToString().ToString() + "%' ";
            dt = dtsearch.ToTable();

            System.Data.DataTable temptable = dt;


           

            if (temptable.Rows.Count > 0)
            {
                grd_Admin_orders.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_Admin_orders.Rows.Add();
                    grd_Admin_orders.Rows[i].Cells[0].Value = i + 1;
                    if (User_Role_Id == "1")
                    {
                        grd_Admin_orders.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {
                        grd_Admin_orders.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Number"].ToString();

                    }

                    if (User_Role_Id == "1")
                    {
                        grd_Admin_orders.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else 
                    {
                        grd_Admin_orders.Rows[i].Cells[2].Value = temptable.Rows[i]["Subprocess_Number"].ToString();

                    }


                    grd_Admin_orders.Rows[i].Cells[3].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                    grd_Admin_orders.Rows[i].Cells[4].Value = temptable.Rows[i]["Order_Type"].ToString();
                    grd_Admin_orders.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Asigned_Type"].ToString();
                    grd_Admin_orders.Rows[i].Cells[6].Value = temptable.Rows[i]["STATE_COUNTY"].ToString();
                    grd_Admin_orders.Rows[i].Cells[7].Value = temptable.Rows[i]["Assigned_Date"].ToString();
                    grd_Admin_orders.Rows[i].Cells[8].Value = temptable.Rows[i]["Progress_Status"].ToString();
                    grd_Admin_orders.Rows[i].Cells[9].Value = temptable.Rows[i]["Order_ID"].ToString();

                }
            }
            else
            {
                grd_Admin_orders.Rows.Clear();
                grd_Admin_orders.Visible = true;
                grd_Admin_orders.DataSource = null;
            }
         

        }

    
   

        

        private void txt_Order_Number_TextChanged(object sender, EventArgs e)
        {
            if (txt_Order_Number.Text != "")
            {
                Bind_Filter_Data();
            }
            else
            {
                Gridview_Bind_Assigned_Orders();

            }
        }

        private void txt_Order_Number_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Order_Number.Text == "Search Order number.....")
            {
                txt_Order_Number.Text = "";
                txt_Order_Number.ForeColor = Color.Black;
            }
        }

        private void Tax_Inhouse_Order_View_Load(object sender, EventArgs e)
        {
            Gridview_Bind_Assigned_Orders();
           
        }

        private void grd_Admin_orders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {

                    Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(grd_Admin_orders.Rows[e.RowIndex].Cells[9].Value.ToString()), User_Id, User_Role_Id,"");
                    OrderEntry.Show();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
