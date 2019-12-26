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
    public partial class Vendor_Percntage_New : Form
    {
        Commonclass cc = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Temp_val = 0, User_Id, Insert_val = 0, Cell_Value = 0;
        double tot_val = 0, vendor_per;
        decimal Vendor_Percentage_Value;
        
        DataTable dtven_Percentage = new System.Data.DataTable();
        int Row_Number,Validate_Record_Count,Current_Index;
        int Vendor_Id, Client_Id;
        string User_Role;
        public Vendor_Percntage_New(int userid,string USER_ROLE)
        {
            InitializeComponent();
            User_Id = userid;
            User_Role = USER_ROLE;
        }

        private void Vendor_Percntage_New_Load(object sender, EventArgs e)
        {
            Bind_Vendor_Percentage();
            Vendor_Client_Cell_Highlight();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Bind_Vendor_Percentage()
        {
            Hashtable ht_Vendor_Perntage = new Hashtable();

            if (User_Role == "1")
            {
                ht_Vendor_Perntage.Add("@Trans", "SELECT");
            }
            else
            {

                ht_Vendor_Perntage.Add("@Trans", "SELECT_FOR_EMP_ROLE");
            }

            dtven_Percentage = dataaccess.ExecuteSP("Sp_Vendor_Percentage", ht_Vendor_Perntage);
            if (dtven_Percentage.Rows.Count > 0)
            {
                GridVendor_Percentage.DataSource = dtven_Percentage;
              
            }
            else
            {
                GridVendor_Percentage.Rows.Clear();
                GridVendor_Percentage.DataSource = null;
            }

            // Resize the master DataGridView columns to fit the newly loaded data.
            GridVendor_Percentage.AutoResizeColumns();

            // Configure the details DataGridView so that its columns automatically
            // adjust their widths when the data changes.
            GridVendor_Percentage.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
          
           
        }

        private void btn_SaveAll_Click(object sender, EventArgs e)
        {

            if (GridVendor_Percentage.Rows.Count > 0)
            {

                for (int i = 0; i < GridVendor_Percentage.Rows.Count; i++)
                {

                    for (int j = 0; j < GridVendor_Percentage.Columns.Count; j++)
                    {

                        string Row_Value = GridVendor_Percentage.Rows[i].Cells[0].Value.ToString();

                        string Column_Header = GridVendor_Percentage.Columns[j].HeaderText;



                        if (Column_Header != "Vendor_Name")
                        {
                            string Percentage = GridVendor_Percentage.Rows[i].Cells[j].Value.ToString();

                            if (Percentage != "" && Percentage != null)
                            {

                                Vendor_Percentage_Value = Convert.ToDecimal(Percentage.ToString());
                            }
                            else
                            {
                                Vendor_Percentage_Value = 0;

                            }

                            Hashtable htcheck = new Hashtable();
                            DataTable dtcheck = new DataTable();

                            Hashtable htget_Vendor = new Hashtable();
                            DataTable dtget_Vendor = new DataTable();

                            htget_Vendor.Add("@Trans", "GET_VENDOR_ID");
                            htget_Vendor.Add("@Vendor_Name", Row_Value);
                            dtget_Vendor = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htget_Vendor);
                            if (dtget_Vendor.Rows.Count > 0)
                            {
                                Vendor_Id = int.Parse(dtget_Vendor.Rows[0]["Vendor_Id"].ToString());

                            }
                            Hashtable htget_Client = new Hashtable();
                            DataTable dtget_Client = new DataTable();

                            htget_Client.Add("@Trans", "GET_CLIENT_ID");
                            htget_Client.Add("@Client_Name", Column_Header);
                            dtget_Client = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htget_Client);
                            if (dtget_Client.Rows.Count > 0)
                            {
                                Client_Id = int.Parse(dtget_Client.Rows[0]["Client_Id"].ToString());

                            }

                            htcheck.Add("@Trans", "CEHCK");
                            htcheck.Add("@Vendor_Id", Vendor_Id);
                            htcheck.Add("@Client_Id", Client_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htcheck);

                            int Check = 0;

                            if (dtcheck.Rows.Count > 0)
                            {
                                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                            }
                            else
                            {

                                Check = 0;
                            }

                            if (Check == 0)
                            {

                                Hashtable htinsert = new Hashtable();
                                DataTable dtinsert = new DataTable();

                                htinsert.Add("@Trans", "INSERT_VENDOR_PERCENT");
                                htinsert.Add("@Vendor_Id", Vendor_Id);
                                htinsert.Add("@Client_Id", Client_Id);
                                htinsert.Add("@Percentage", Vendor_Percentage_Value);
                                htinsert.Add("@Inserted_By", User_Id);

                                dtinsert = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htinsert);


                            }
                            else if (Check > 0)
                            {

                                Hashtable htinsert = new Hashtable();
                                DataTable dtinsert = new DataTable();

                                htinsert.Add("@Trans", "UPDATE_VENDOR_PERCENT");
                                htinsert.Add("@Vendor_Id", Vendor_Id);
                                htinsert.Add("@Client_Id", Client_Id);
                                htinsert.Add("@Percentage", Vendor_Percentage_Value);
                                htinsert.Add("@Modified_By", User_Id);
                                dtinsert = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htinsert);

                            }




                        }
                    }







                }


                MessageBox.Show("Percentage Updated Sucessfully");
                Bind_Vendor_Percentage();
                Vendor_Client_Cell_Highlight();
            }

        }

        private void GridVendor_Percentage_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex != 0)
                {

                    decimal sum = 0;
                    for (int i = 0; i < GridVendor_Percentage.Rows.Count; ++i)
                    {
                        int Coumn_Index = e.ColumnIndex;
                        string Cell_Value = GridVendor_Percentage.Rows[i].Cells[Coumn_Index].Value.ToString();
                        Row_Number = e.RowIndex;
                        if (Cell_Value != null && Cell_Value != "")
                        {
                            Current_Index = e.ColumnIndex;
                            sum += Convert.ToDecimal(Cell_Value.ToString());
                        }
                    }

                    decimal Total = sum;



                    if (Total > 100)
                    {

                        // MessageBox.Show("Total Value of Each Vendor Should not Cross More Than 100");
                        GridVendor_Percentage.Rows[Row_Number].Cells[Current_Index].Value = 0;
                        // Validate_Record_Count = 1;
                    }
                }
                
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Bind_Vendor_Percentage();
            Vendor_Client_Cell_Highlight();
        }

        private void Vendor_Client_Cell_Highlight()
        {

            if (GridVendor_Percentage.Rows.Count > 0)
            {

                for (int i = 0; i < GridVendor_Percentage.Rows.Count; i++)
                {

                    for (int j = 0; j < GridVendor_Percentage.Columns.Count; j++)
                    {

                        string Row_Value = GridVendor_Percentage.Rows[i].Cells[0].Value.ToString();

                        string Column_Header = GridVendor_Percentage.Columns[j].HeaderText;



                        if (Column_Header != "Vendor_Name")
                        {


                            Hashtable htcheck = new Hashtable();
                            DataTable dtcheck = new DataTable();

                            Hashtable htget_Vendor = new Hashtable();
                            DataTable dtget_Vendor = new DataTable();

                            htget_Vendor.Add("@Trans", "GET_VENDOR_ID");
                            htget_Vendor.Add("@Vendor_Name", Row_Value);
                            dtget_Vendor = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htget_Vendor);
                            if (dtget_Vendor.Rows.Count > 0)
                            {
                                Vendor_Id = int.Parse(dtget_Vendor.Rows[0]["Vendor_Id"].ToString());

                            }
                            Hashtable htget_Client = new Hashtable();
                            DataTable dtget_Client = new DataTable();

                            htget_Client.Add("@Trans", "GET_CLIENT_ID");
                            htget_Client.Add("@Client_Name", Column_Header);
                            dtget_Client = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htget_Client);
                            if (dtget_Client.Rows.Count > 0)
                            {
                                Client_Id = int.Parse(dtget_Client.Rows[0]["Client_Id"].ToString());

                            }



                            htcheck.Add("@Trans", "CHECK_VENDOR_CLEINT");
                            htcheck.Add("@Vendor_Id", Vendor_Id);
                            htcheck.Add("@Client_Id", Client_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Vendor_Percentage", htcheck);

                            int Check = 0;

                            if (dtcheck.Rows.Count > 0)
                            {
                                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                            }
                            else
                            {

                                Check = 0;
                            }





                            if (Check == 0)
                            {

                              //  GridVendor_Percentage.Rows[i].Cells[j].Style.BackColor = Color.Red;

                                GridVendor_Percentage.Rows[i].Cells[j].ReadOnly = true;
                            }
                            else
                            {

                                GridVendor_Percentage.Rows[i].Cells[j].Style.BackColor = Color.LightYellow;

                              
                            }


                        }

                    }

                }
            }









        }
    }
}
