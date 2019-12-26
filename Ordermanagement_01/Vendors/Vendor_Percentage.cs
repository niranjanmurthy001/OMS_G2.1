using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Percentage : Form
    {
        Commonclass cc = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Temp_val = 0, User_Id,   Insert_val = 0, Cell_Value = 0;
        double tot_val = 0, vendor_per;
        DataTable dtTax = new System.Data.DataTable();
        public Vendor_Percentage(int userid)
        {
            InitializeComponent();
            User_Id = userid;
        }

        private void Vendor_Percentage_Load(object sender, EventArgs e)
        {
            Bind_Vendor_Percentage();
        }
        private void Bind_Vendor_Percentage()
        {
            Hashtable htTax = new Hashtable();
            

            htTax.Add("@Trans", "SELECT_VENDOR_PERCENT");

            dtTax = dataaccess.ExecuteSP("Sp_Vendor", htTax);
            if (dtTax.Rows.Count > 0)
            {
                GridVendor_Percentage.Rows.Clear();
                for (int i = 0; i < dtTax.Rows.Count; i++)
                {
                    GridVendor_Percentage.Rows.Add();
                    GridVendor_Percentage.Rows[i].Cells[0].Value = i + 1;
                    GridVendor_Percentage.Rows[i].Cells[1].Value = dtTax.Rows[i]["Vendor_Name"].ToString();
                    if (dtTax.Rows[i]["Percentage"].ToString() != "" && dtTax.Rows[i]["Percentage"].ToString() != null)
                    {
                        GridVendor_Percentage.Rows[i].Cells[2].Value = dtTax.Rows[i]["Percentage"].ToString();
                    }
                    else
                    {
                        GridVendor_Percentage.Rows[i].Cells[2].Value = "0";
                    }
                    GridVendor_Percentage.Rows[i].Cells[3].Value = dtTax.Rows[i]["Vendor_Id"].ToString();
                    GridVendor_Percentage.Rows[i].Cells[4].Value = dtTax.Rows[i]["Vendor_Percentage_Id"].ToString();

                }
                
            }
            else
            {
                GridVendor_Percentage.Rows.Clear();
                GridVendor_Percentage.DataSource = null;
            }
            tot_val = 0;
            for (int i = 0; i < GridVendor_Percentage.Rows.Count; i++)
            {
                vendor_per =  Convert.ToDouble (GridVendor_Percentage.Rows[i].Cells[2].Value.ToString());
                tot_val = vendor_per + tot_val;
                vendor_per = 0;
                if (tot_val > 100)
                {
                    break;
                }
                else
                {
                    Cell_Value = 1;
                }
                lbl_Total.Text = tot_val.ToString();
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Bind_Vendor_Percentage();
            
        }

        private void btn_SaveAll_Click(object sender, EventArgs e)
        {
            
            if (GridVendor_Percentage.Rows.Count > 0)
            {
                tot_val = 0;
                for (int i = 0; i < GridVendor_Percentage.Rows.Count; i++)
                {
                    vendor_per = double.Parse(GridVendor_Percentage.Rows[i].Cells[2].Value.ToString());
                    tot_val = vendor_per + tot_val;
                    vendor_per = 0;
                    if (tot_val > 100)
                    {
                        break;
                    }
                    else
                    {
                        Cell_Value = 1;
                    }
                    lbl_Total.Text = tot_val.ToString();
                }
                if (tot_val > 100)
                {
                    MessageBox.Show("Total Percentage of vendor exceeded over 100.. Kindly Give lesser value");
                    lbl_Total.Text = "100";
                }
                else
                {
                    Insert_val = 1;
                }

                if (Insert_val == 1)
                {
                    for (int i = 0; i < GridVendor_Percentage.Rows.Count; i++)
                    {
                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "SELECT_VENDOR_PERCENT_VENDOR");
                        htcheck.Add("@Vendor_Id", int.Parse(GridVendor_Percentage.Rows[i].Cells[3].Value.ToString()));
                        dtcheck = dataaccess.ExecuteSP("Sp_Vendor", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {

                            Hashtable htupdpate = new Hashtable();
                            DataTable dtupdpate = new DataTable();
                            htupdpate.Add("@Trans", "UPDATE_VENDOR_PERCENT");
                            htupdpate.Add("@Vendor_Id", int.Parse(GridVendor_Percentage.Rows[i].Cells[3].Value.ToString()));
                            htupdpate.Add("@Percentage", double.Parse(GridVendor_Percentage.Rows[i].Cells[2].Value.ToString()));
                            htupdpate.Add("@Modified_By", User_Id);

                            dtupdpate = dataaccess.ExecuteSP("Sp_Vendor", htupdpate);
                            Temp_val = 1;

                        }
                        else
                        {

                            Hashtable htinsert = new Hashtable();
                            DataTable dtinsert = new DataTable();
                            htinsert.Add("@Trans", "INSERT_VENDOR_PERCENT");
                            htinsert.Add("@Vendor_Id", int.Parse(GridVendor_Percentage.Rows[i].Cells[3].Value.ToString()));
                            htinsert.Add("@Percentage", double.Parse(GridVendor_Percentage.Rows[i].Cells[2].Value.ToString()));
                            htinsert.Add("@Inserted_By", User_Id);

                            dtinsert = dataaccess.ExecuteSP("Sp_Vendor", htinsert);
                            Temp_val = 1;

                        }
                    }
                    Insert_val = 0;

                }
                

            }
            if (Temp_val == 1)
            {
                MessageBox.Show("Record Updated Successfully");
                Temp_val = 0;
            }
        }

        private void GridVendor_Percentage_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
                
        }

        private void GridVendor_Percentage_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void GridVendor_Percentage_KeyDown(object sender, KeyEventArgs e)
        {
           
           
        }

        private void GridVendor_Percentage_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
           
        }

        private void GridVendor_Percentage_Enter(object sender, EventArgs e)
        {
           
        }

        private void GridVendor_Percentage_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            tot_val = 0;
            for (int i = 0; i < GridVendor_Percentage.Rows.Count; i++)
            {
                vendor_per = double.Parse(GridVendor_Percentage.Rows[i].Cells[2].Value.ToString());
                tot_val = vendor_per + tot_val;
                vendor_per = 0.00;
                if (tot_val > 100)
                {
                    break;
                }
                else
                {
                    Cell_Value = 1;
                }
                lbl_Total.Text = tot_val.ToString();
            }
            if (tot_val > 100)
            {
                MessageBox.Show("Total Percentage of vendor exceeded over 100.. Kindly Give lesser value");
                lbl_Total.Text = "100";
            }
        }
    }
}
