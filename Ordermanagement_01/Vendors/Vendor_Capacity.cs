using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Capacity : Form
    {
        Commonclass cc = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_ID, Temp_val = 0;

        public Vendor_Capacity(int userid)
        {
            InitializeComponent();
            User_ID = userid;
        }

        private void Vendor_Capacity_Load(object sender, EventArgs e)
        {
            Bind_Vendor_Capacity();
        }
        private void Bind_Vendor_Capacity()
        {
            Hashtable htTax = new Hashtable();
            DataTable dtTax = new System.Data.DataTable();

            htTax.Add("@Trans", "SELECT");

            dtTax = dataaccess.ExecuteSP("Sp_Vendor_Order_Capacity", htTax);
            if (dtTax.Rows.Count > 0)
            {
                GridVendor_Capacity.Rows.Clear();
                for (int i = 0; i < dtTax.Rows.Count; i++)
                {
                    GridVendor_Capacity.Rows.Add();
                    GridVendor_Capacity.Rows[i].Cells[0].Value = i+1;
                    GridVendor_Capacity.Rows[i].Cells[1].Value = dtTax.Rows[i]["Vendor_Name"].ToString();
                    if (dtTax.Rows[i]["Capacity"].ToString() != "" && dtTax.Rows[i]["Capacity"].ToString() != null)
                    {
                        GridVendor_Capacity.Rows[i].Cells[2].Value = dtTax.Rows[i]["Capacity"].ToString();
                    }
                    GridVendor_Capacity.Rows[i].Cells[3].Value = dtTax.Rows[i]["Vendor_Id"].ToString();
                    GridVendor_Capacity.Rows[i].Cells[4].Value = dtTax.Rows[i]["Vendor_Capacity_Id"].ToString();
                
                }
            }
            else
            {
                GridVendor_Capacity.Rows.Clear();
                GridVendor_Capacity.DataSource = null;
            }
        }

      
        private void btn_SaveAll_Click(object sender, EventArgs e)
        {
            if (GridVendor_Capacity.Rows.Count > 0)
            {
                for (int i = 0; i < GridVendor_Capacity.Rows.Count; i++)
                {
                    Hashtable htselect = new Hashtable();
                    DataTable dtselect = new DataTable();
                    htselect.Add("@Trans", "GET_ProcessMaster_DETAILS");
                    htselect.Add("@Vendor_Id", int.Parse(GridVendor_Capacity.Rows[i].Cells[3].Value.ToString()));
                    dtselect = dataaccess.ExecuteSP("Sp_Vendor_Order_Capacity", htselect);
                    if (dtselect.Rows.Count > 0)
                    {
                        Hashtable htupdpate = new Hashtable();
                        DataTable dtupdpate = new DataTable();
                        htupdpate.Add("@Trans", "UPDATE");
                        htupdpate.Add("@Vendor_Id", int.Parse(GridVendor_Capacity.Rows[i].Cells[3].Value.ToString()));

                        if (GridVendor_Capacity.Rows[i].Cells[2].Value != null)
                        {
                            htupdpate.Add("@Capacity", int.Parse(GridVendor_Capacity.Rows[i].Cells[2].Value.ToString()));
                        }
                      //  htupdpate.Add("@Capacity", int.Parse(GridVendor_Capacity.Rows[i].Cells[2].Value.ToString()));
                        htupdpate.Add("@Modified_By", User_ID);

                        dtupdpate = dataaccess.ExecuteSP("Sp_Vendor_Order_Capacity", htupdpate);
                        Temp_val = 1;
                    }
                    else
                    {
                        Hashtable htinsert = new Hashtable();
                        DataTable dtinsert = new DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@Vendor_Id", int.Parse(GridVendor_Capacity.Rows[i].Cells[3].Value.ToString()));
                        if (GridVendor_Capacity.Rows[i].Cells[2].Value != null)
                        {

                            htinsert.Add("@Capacity", int.Parse(GridVendor_Capacity.Rows[i].Cells[2].Value.ToString()));
                        }
                       // htinsert.Add("@Capacity", int.Parse(GridVendor_Capacity.Rows[i].Cells[2].Value.ToString()));
                        htinsert.Add("@Inserted_By", User_ID);
                        htinsert.Add("@Instered_Date", DateTime.Now);

                        dtinsert = dataaccess.ExecuteSP("Sp_Vendor_Order_Capacity", htinsert);
                        Temp_val = 1;
                    }

                }
            }
            if (Temp_val == 1)
            {
                MessageBox.Show("Record Updated Successfully");
            }

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Bind_Vendor_Capacity();
        }

        private void GridVendor_Capacity_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
             GridVendor_Capacity.Columns[e.ColumnIndex].HeaderText;
            int newInteger;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("No of Orders (Capacity)")) return;

            if(string.IsNullOrEmpty(e.FormattedValue.ToString()) )   
            {
                GridVendor_Capacity.Rows[e.RowIndex].ErrorText =
                    "Number of Order Capacity must not be empty";
                MessageBox.Show("Number of Order Capacity must not be empty");
                e.Cancel = true;
            }
            else if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0)
            {
                //GridVendor_Capacity.Rows[e.RowIndex].ErrorText = "The Value Must be Positive Integer";

                MessageBox.Show("The Value Must be Positive Integer");
                e.Cancel = true;

            }

            else
            {

            }
           
            

        }

     
    }
}
