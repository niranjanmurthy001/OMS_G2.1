using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace Ordermanagement_01.Vendors
{
    public partial class Import_State_County : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Vendor_Id, User_Id, State_id, County_id, checkvalue, ChkValue, validate_val;
        string vendor_name, state, county, vendor; DialogResult dialogresult,dialog1;
        public Import_State_County(int Vendorid,int Userid,string Vendor)
        {
            InitializeComponent();
            Vendor_Id = Vendorid;
            User_Id = Userid;
            vendor = Vendor;
        }
        
        private void btn_upload_Click(object sender, EventArgs e)
        {
            
            if (Vendor_Id !=0)
            {
                grd_Vendor_Statecounty.Rows.Clear();
                //Grid Load Excel
                OpenFileDialog fdlg = new OpenFileDialog();

                fdlg.Title = "Select Excel file";
                fdlg.InitialDirectory = @"c:\";
                var txtFileName = fdlg.FileName;
                fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtFileName = fdlg.FileName;
                    Import(txtFileName);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else
            {

                //OpenFileDialog fdlg = new OpenFileDialog();

                //fdlg.Title = "Select Excel file";
                //fdlg.InitialDirectory = @"c:\";
                //var txtFileName = fdlg.FileName;
                //fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
                //fdlg.FilterIndex = 1;
                //fdlg.RestoreDirectory = true;
                //if (fdlg.ShowDialog() == DialogResult.OK)
                //{
                //    txtFileName = fdlg.FileName;
                //    Import(txtFileName);
                //    System.Windows.Forms.Application.DoEvents();
                //}
            }
        }
        private void Import(string txtFileName)
        {
            if (Vendor_Id != 0)
            {
                if (txtFileName != string.Empty)
                {
                    try
                    {
                        String name = "Sheet1";    // default Sheet1 
                        String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                   txtFileName +
                                    ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                        OleDbConnection con = new OleDbConnection(constr);
                        OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                        con.Open();

                        OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                        System.Data.DataTable data = new System.Data.DataTable();
                        int value = 0;
                        sda.Fill(data);

                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            if (data.Rows[i]["State"].ToString() != "" || data.Rows[i]["County"].ToString() != "" ||
                                 data.Rows[i]["State"].ToString() != null || data.Rows[i]["County"].ToString() != null)
                            {
                                //  vendor_name = data.Rows[i]["Vendor Name"].ToString();
                                state = data.Rows[i]["State"].ToString();
                                county = data.Rows[i]["County"].ToString();


                                grd_Vendor_Statecounty.Rows.Add();
                                grd_Vendor_Statecounty.Columns[0].Visible = false;
                                grd_Vendor_Statecounty.Rows[i].Cells[1].Value = data.Rows[i]["State"].ToString();
                                grd_Vendor_Statecounty.Rows[i].Cells[2].Value = data.Rows[i]["County"].ToString();

                                grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor = Color.White;

                                for (int j = 0; j < i; j++)
                                {
                                    string state1 = data.Rows[j]["State"].ToString();
                                    string county1 = data.Rows[j]["County"].ToString();
                                    if (state == state1 && county == county1)
                                    {
                                        value = 1;
                                        break;
                                    }
                                    else
                                    {
                                        value = 0;
                                    }

                                }


                                Hashtable htabsid = new Hashtable();
                                DataTable dtabsid = new DataTable();
                                htabsid.Add("@Trans", "SELECT_VENDORID");
                                htabsid.Add("@Vendor_Id", Vendor_Id);
                                dtabsid = dataaccess.ExecuteSP("Sp_Vendor_State_County", htabsid);
                                if (dtabsid.Rows.Count != 0)
                                {
                                    vendor_name = dtabsid.Rows[0]["Vendor_Name"].ToString();
                                }

                                //Error State
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", state);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                                }
                                else
                                {
                                    grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }

                                //Mismatch State and County
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", county);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                                }
                                else
                                {
                                    grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }

                                ////Record Already exists
                                //Hashtable htnewrows = new Hashtable();
                                //DataTable dtnewrows = new DataTable();
                                //htnewrows.Add("@Trans", "SELNEWROWS");
                                //htnewrows.Add("@Vendor_Id", Vendor_Id);
                                //htnewrows.Add("@State", State_id);
                                //htnewrows.Add("@County", County_id);
                                //dtnewrows = dataaccess.ExecuteSP("Sp_Vendor_State_County", htnewrows);
                                //if (dtnewrows.Rows.Count > 0)
                                //{
                                //    int vendorid = int.Parse(dtnewrows.Rows[0]["Vendor_Id"].ToString());
                                //    int stateid = int.Parse(dtnewrows.Rows[0]["State"].ToString());
                                //    int countyid = int.Parse(dtnewrows.Rows[0]["County"].ToString());
                                    
                                //    if (Vendor_Id == vendorid && State_id == stateid && County_id == countyid)
                                //    {
                                //        newrow = 1;
                                //    }
                                //    else
                                //    {
                                //        newrow = 0;
                                //    }
                                //}


                                if (value == 1)
                                {
                                    grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }
                                //if (newrow == 1)
                                //{
                                //    grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                //}

                            }
                            else
                            {
                                grd_Vendor_Statecounty.Rows.Clear();
                                MessageBox.Show("Check Empty Cells in Excel");
                            }

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }


                }

            }
           
           
        }
        private void btn_SampleFormat_Click(object sender, EventArgs e)
        {
            if (Vendor_Id != 0)
            {
                Directory.CreateDirectory(@"c:\Temp\OMS_Import\");
                string temppath = @"c:\Temp\OMS_Import\Inv_Vendor_State_County.xlsx";
                File.Copy(Environment.CurrentDirectory + "\\Inv_Vendor_State_County.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Directory.CreateDirectory(@"c:\Temp\OMS_Import\");
                string temppath = @"c:\Temp\OMS_Import\Vendor_State_County.xlsx";
                File.Copy(Environment.CurrentDirectory + "\\Vendor_State_County.xlsx", temppath, true);
                Process.Start(temppath);
            }
        }

        private void btn_ErrorRows_Click(object sender, EventArgs e)
        {
            if (Vendor_Id != 0)
            {
                for (int i = 0; i < grd_Vendor_Statecounty.Rows.Count; i++)
                {
                    if (grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    {

                        // grd_CpAbstractor.Rows.Add();
                        string Statename = grd_Vendor_Statecounty.Rows[i].Cells[1].Value.ToString();
                        string Countyname = grd_Vendor_Statecounty.Rows[i].Cells[2].Value.ToString();

                        grd_Vendor_Statecounty.Rows.RemoveAt(i);

                        i = i - 1;
                        grd_CpVendor.Rows.Add();
                        int j = grd_CpVendor.Rows.Count - 1;
                        grd_CpVendor.Rows[j].Cells[0].Value = "Submit";
                        grd_CpVendor.Rows[j].Cells[1].Value = "Delete";
                        grd_CpVendor.Rows[j].Cells[2].Value = Statename;
                        grd_CpVendor.Rows[j].Cells[3].Value = Countyname;

                        grd_CpVendor.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        grd_CpVendor.DefaultCellStyle.BackColor = Color.White;
                    }


                }
            }
        }

        private bool Validation_Statecounty()
        {
            for (int i = 0; i < grd_Vendor_Statecounty.Rows.Count; i++)
            {
                string State_name = grd_Vendor_Statecounty.Rows[i].Cells[1].Value.ToString();
                string County_name = grd_Vendor_Statecounty.Rows[i].Cells[2].Value.ToString();

                Hashtable htstate = new Hashtable();
                DataTable dtstate = new DataTable();
                htstate.Add("@Trans", "STATE_ID");
                htstate.Add("@State_name", State_name);
                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                if (dtstate.Rows.Count != 0)
                {
                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                }

                //Mismatch State and County
                Hashtable htstcounty = new Hashtable();
                DataTable dtstcounty = new DataTable();
                htstcounty.Add("@Trans", "STATE_COUNTY");
                htstcounty.Add("@State_ID", State_id);
                htstcounty.Add("@County_Name", County_name);
                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                if (dtstcounty.Rows.Count != 0)
                {
                    County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                }
                else
                {
                    validate_val = 1;
                }
            }
            if (validate_val == 1)
            {
                validate_val = 0;
                return false;
            }
            else
            {
                return true;
            }
            
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            if (Vendor_Id != 0)
            {
                int Entervalue = 0;
                checkvalue = 0;
                for (int i = 0; i < grd_Vendor_Statecounty.Rows.Count; i++)
                {
                    if (grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor != Color.White)
                    {
                        Entervalue = 1;
                    }
                }
                if (Entervalue != 1)
                {
                    dialogresult = MessageBox.Show("Do you want to Overwrite State county of current abstractor (YES-OVERWRITE/NO-RECREATE)", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogresult == DialogResult.Yes && Validation_Statecounty() != false)
                    {
                        checkvalue = 1;
                        for (int i = 0; i < grd_Vendor_Statecounty.Rows.Count; i++)
                        {
                            // string Vendor_name = grd_Vendor_Statecounty.Rows[i].Cells[0].Value.ToString();
                            string State_name = grd_Vendor_Statecounty.Rows[i].Cells[1].Value.ToString();
                            string County_name = grd_Vendor_Statecounty.Rows[i].Cells[2].Value.ToString();

                            Hashtable htstate = new Hashtable();
                            DataTable dtstate = new DataTable();
                            htstate.Add("@Trans", "STATE_ID");
                            htstate.Add("@State_name", State_name);
                            dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                            if (dtstate.Rows.Count != 0)
                            {
                                State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                            }



                            //Mismatch State and County
                            Hashtable htstcounty = new Hashtable();
                            DataTable dtstcounty = new DataTable();
                            htstcounty.Add("@Trans", "STATE_COUNTY");
                            htstcounty.Add("@State_ID", State_id);
                            htstcounty.Add("@County_Name", County_name);
                            dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                            if (dtstcounty.Rows.Count != 0)
                            {
                                County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                            }

                            Hashtable htnewrows = new Hashtable();
                            DataTable dtnewrows = new DataTable();
                            htnewrows.Add("@Trans", "SELNEWROWS");
                            htnewrows.Add("@Vendor_Id", Vendor_Id);
                            htnewrows.Add("@State", State_id);
                            htnewrows.Add("@County", County_id);
                            dtnewrows = dataaccess.ExecuteSP("Sp_Vendor_State_County", htnewrows);
                            if (dtnewrows.Rows.Count > 0)
                            {
                                int vendorid = int.Parse(dtnewrows.Rows[0]["Vendor_Id"].ToString());
                                int stateid = int.Parse(dtnewrows.Rows[0]["State"].ToString());
                                int countyid = int.Parse(dtnewrows.Rows[0]["County"].ToString());
                                if (Vendor_Id == vendorid && State_id == stateid && County_id == countyid)
                                {
                                    ChkValue = 1;
                                }
                                else
                                {
                                    ChkValue = 0;
                                }
                            }


                            if (ChkValue == 0)
                            {
                                
                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "INSERT_STATE_COUNTY");
                                htabsinsert.Add("@Vendor_Id", Vendor_Id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);

                                htabsinsert.Add("@Inserted_By", User_Id);
                                htabsinsert.Add("@Inserted_Date ", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Vendor_State_County", htabsinsert);

                            }
                            else if (ChkValue > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Vendor_Id", Vendor_Id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);

                                htabsinsert.Add("@Modified_By", User_Id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Vendor_State_County", htabsinsert);
                            }
                            
                        }

                        
                    }
                    else if (Validation_Statecounty() != false)//for recreate state county of abstractor
                    {
                        checkvalue = 0;
                        //delete all previouse record of particular vendor
                        Hashtable htdelall = new Hashtable();
                        DataTable dtdelall = new DataTable();
                        htdelall.Add("@Trans", "DELETE_ALL");
                        htdelall.Add("@Vendor_Id", Vendor_Id);
                        dtdelall = dataaccess.ExecuteSP("Sp_Vendor_State_County", htdelall);
                        for (int i = 0; i < grd_Vendor_Statecounty.Rows.Count; i++)
                        {
                            // string Vendor_name = grd_Vendor_Statecounty.Rows[i].Cells[0].Value.ToString();
                            string State_name = grd_Vendor_Statecounty.Rows[i].Cells[1].Value.ToString();
                            string County_name = grd_Vendor_Statecounty.Rows[i].Cells[2].Value.ToString();

                            Hashtable htstate = new Hashtable();
                            DataTable dtstate = new DataTable();
                            htstate.Add("@Trans", "STATE_ID");
                            htstate.Add("@State_name", State_name);
                            dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                            if (dtstate.Rows.Count != 0)
                            {
                                State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                            }



                            //Mismatch State and County
                            Hashtable htstcounty = new Hashtable();
                            DataTable dtstcounty = new DataTable();
                            htstcounty.Add("@Trans", "STATE_COUNTY");
                            htstcounty.Add("@State_ID", State_id);
                            htstcounty.Add("@County_Name", County_name);
                            dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                            if (dtstcounty.Rows.Count != 0)
                            {
                                County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                            }

                            


                            //Insert freshly to database
                            Hashtable htabsinsert = new Hashtable();
                            DataTable dtabsinsert = new DataTable();
                            htabsinsert.Add("@Trans", "INSERT_STATE_COUNTY");
                            htabsinsert.Add("@Vendor_Id", Vendor_Id);
                            htabsinsert.Add("@State", State_id);
                            htabsinsert.Add("@County", County_id);

                            htabsinsert.Add("@Inserted_By", User_Id);
                            
                            
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Vendor_State_County", htabsinsert);


                            
                        }



                    }
                    grd_Vendor_Statecounty.Rows.Clear();
                    if (checkvalue == 1)
                    {

                        MessageBox.Show("Vendor State county Info Overwrited Successfully");
                        checkvalue = 0;
                    }
                    else if (checkvalue == 0)
                    {
                        MessageBox.Show("Vendor State county Info Newly Inserted Successfully");
                    }

                }

                else
                {
                    MessageBox.Show("Check the Errors in Excel");
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            grd_Vendor_Statecounty.Rows.Clear();
            grd_CpVendor.Rows.Clear();
            checkvalue = 0; ChkValue = 0; validate_val = 0;
        }

        private void grd_CpVendor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int error_val = 0;
                for (int i = 0; i < grd_CpVendor.Rows.Count; i++)
                {
                    string state = grd_CpVendor.Rows[i].Cells[2].Value.ToString();
                    string county = grd_CpVendor.Rows[i].Cells[3].Value.ToString();
                    for (int j = 0; j < grd_Vendor_Statecounty.Rows.Count; j++)
                    {
                        string state_cp = grd_Vendor_Statecounty.Rows[j].Cells[1].Value.ToString();
                        string county_cp = grd_Vendor_Statecounty.Rows[j].Cells[2].Value.ToString();
                        if (state == state_cp && county == county_cp)
                        {
                            error_val = 1;
                        }
                        else
                        {

                            error_val = 0;
                        }
                    }

                }
                if (error_val == 1)
                {
                    MessageBox.Show("Kindly change state county and update it");
                    error_val = 0;
                }
                else if (error_val == 0)
                {
                    for (int i = grd_Vendor_Statecounty.Rows.Count, j = 0; j < grd_CpVendor.Rows.Count; i++)
                    {

                        grd_Vendor_Statecounty.Rows.Add();
                        grd_Vendor_Statecounty.Rows[i].Cells[1].Value = grd_CpVendor.Rows[j].Cells[2].Value;
                        grd_Vendor_Statecounty.Rows[i].Cells[2].Value = grd_CpVendor.Rows[j].Cells[3].Value;
                        grd_CpVendor.Rows.RemoveAt(j);
                        grd_Vendor_Statecounty.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }

                    MessageBox.Show("State county Values updated successfully.. Import it");
                }
            }
            else if (e.ColumnIndex == 1)
            {
                grd_CpVendor.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void Import_State_County_Load(object sender, EventArgs e)
        {
            lbl_Vendor_Name.Text = vendor;
        }

        
    }
}
