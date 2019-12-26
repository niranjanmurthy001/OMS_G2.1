using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
namespace Ordermanagement_01.Masters
{
    public partial class Client_State_County_Type_Import : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Client_id;
        int State_id, County_id,County_Type_Id;
        string state, county,countytype;
        System.Data.DataTable dtnonadded = new System.Data.DataTable();
        DataRow dr;
        DataSet ds = new DataSet();
        int Check_value;
        int User_Id;
        decimal orderCost;
        int County_Check;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string Client_Name;
        int Client_ID;
        public Client_State_County_Type_Import(int CLIENT_ID)
        {
            
            InitializeComponent();
            Client_ID = CLIENT_ID;
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //cProbar.startProgress();
            grd_Client_cost.Rows.Clear();
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
           // cProbar.stopProgress();
        }
        private void Import(string txtFileName)
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
                    int value = 0; int newrow = 0;
                    sda.Fill(data);
                    dtnonadded.Clear();
                    dtnonadded.Columns.Clear();
                    dtnonadded.Columns.Add("State", typeof(string));
                    dtnonadded.Columns.Add("County", typeof(string));
                    dtnonadded.Columns.Add("CountyType", typeof(string));


                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        if (data.Rows[i]["State"].ToString() != "" || data.Rows[i]["County"].ToString() != "" || data.Rows[i]["State"].ToString() != null || data.Rows[i]["County"].ToString() != null && data.Rows[i]["CountyType"].ToString() != "" && data.Rows[i]["CountyType"].ToString() != null)
                        {


                            grd_Client_cost.Rows.Add();
                            grd_Client_cost.Rows[i].Cells[0].Value = data.Rows[i]["State"].ToString();
                            grd_Client_cost.Rows[i].Cells[1].Value = data.Rows[i]["County"].ToString();
                            grd_Client_cost.Rows[i].Cells[2].Value = data.Rows[i]["CountyType"].ToString();



                            //Error State
                            state = data.Rows[i]["State"].ToString();
                            county = data.Rows[i]["County"].ToString();
                            countytype = data.Rows[i]["CountyType"].ToString();
                            Hashtable htstate = new Hashtable();
                            System.Data.DataTable dtstate = new System.Data.DataTable();
                            htstate.Add("@Trans", "STATE_ID");
                            htstate.Add("@State", state);
                            dtstate = dataaccess.ExecuteSP("Sp_County", htstate);
                            if (dtstate.Rows.Count != 0)
                            {
                                State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                                grd_Client_cost.Rows[i].Cells[3].Value = State_id;
                            }
                            else
                            {
                                // MessageBox.Show(state + " does not exist in State Info");
                                State_id = 0;
                                grd_Client_cost.Rows[i].Cells[3].Value = State_id;
                                grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }

                            //Error County
                            Hashtable htcounty = new Hashtable();
                            System.Data.DataTable dtcounty = new System.Data.DataTable();
                            htcounty.Add("@Trans", "COUNTY_ID");
                            htcounty.Add("@State_Id", State_id);
                            htcounty.Add("@County", county);
                            dtcounty = dataaccess.ExecuteSP("Sp_County", htcounty);
                            if (dtcounty.Rows.Count != 0)
                            {
                                County_id = int.Parse(dtcounty.Rows[0]["County_ID"].ToString());
                                grd_Client_cost.Rows[i].Cells[4].Value = County_id;
                            }
                            else
                            {
                                County_id = 0;
                                //  MessageBox.Show(county + " does not exist in County Info");

                                grd_Client_cost.Rows[i].Cells[4].Value = County_id;
                               // grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }

                            //error_County_Type

                            Hashtable htcountyType = new Hashtable();
                            System.Data.DataTable dtcountyType = new System.Data.DataTable();
                            htcountyType.Add("@Trans", "CHECK_COUNTY_TYPE");
                            htcountyType.Add("@State_County_Type", countytype);

                            dtcountyType = dataaccess.ExecuteSP("Sp_External_Client_Sate_County", htcountyType);
                            if (dtcounty.Rows.Count != 0)
                            {
                                County_Type_Id = int.Parse(dtcountyType.Rows[0]["Order_Assign_Type_Id"].ToString());
                                grd_Client_cost.Rows[i].Cells[6].Value = County_Type_Id;
                            }
                            else
                            {
                                County_Type_Id = 0;
                                //  MessageBox.Show(county + " does not exist in County Info");

                                grd_Client_cost.Rows[i].Cells[6].Value = County_Type_Id;
                                grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }

                            //Check Exist State and county




                            if (State_id == 0)
                            {

                                grd_Client_cost.Rows[i].Cells[5].Value = "Error";
                                dr = dtnonadded.NewRow();

                                dr["State"] = grd_Client_cost.Rows[i].Cells[0].Value.ToString();
                                dr["County"] = grd_Client_cost.Rows[i].Cells[1].Value.ToString();

                                dr["CountyType"] = grd_Client_cost.Rows[i].Cells[2].Value.ToString();
                                dtnonadded.Rows.Add(dr);
                                grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                            }

                            else
                            {
                                grd_Client_cost.Rows[i].Cells[5].Value = "NoError";

                            }

                            if (State_id != 0)
                            {


                                Hashtable htchek = new Hashtable();
                                System.Data.DataTable dtcheck = new System.Data.DataTable();

                                htchek.Add("@Trans", "CHECK_COUNTY_BY_NAME");
                                htchek.Add("@State_Id", State_id);
                                htchek.Add("@County", county);
                                dtcheck = dataaccess.ExecuteSP("Sp_County", htchek);

                                if (dtcheck.Rows.Count > 0)
                                {

                                    County_Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    County_Check = 0;
                                }



                            }
                            if (County_Check > 0)
                            {


                                grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;

                            }


                            if (County_Type_Id == 0)
                            {

                                grd_Client_cost.Rows[i].Cells[5].Value = "Error";
                                dr = dtnonadded.NewRow();

                                dr["State"] = grd_Client_cost.Rows[i].Cells[0].Value.ToString();
                                dr["County"] = grd_Client_cost.Rows[i].Cells[1].Value.ToString();

                                dr["CountyType"] = grd_Client_cost.Rows[i].Cells[2].Value.ToString();
                                dtnonadded.Rows.Add(dr);
                                grd_Client_cost.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }
                            else
                            {

                                grd_Client_cost.Rows[i].Cells[5].Value = "NoError";
                            }




                        }
                        else
                        {
                            grd_Client_cost.Rows.Clear();
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

        private void btn_Import_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //cProbar.startProgress();

            for (int i = 0; i < grd_Client_cost.Rows.Count; i++)
            {

                string StateName = grd_Client_cost.Rows[i].Cells[0].Value.ToString();
                string County_Name = grd_Client_cost.Rows[i].Cells[1].Value.ToString();
                string County_Type = grd_Client_cost.Rows[i].Cells[2].Value.ToString();
                string State = grd_Client_cost.Rows[i].Cells[3].Value.ToString();
                string County = grd_Client_cost.Rows[i].Cells[4].Value.ToString();
                string Countytype = grd_Client_cost.Rows[i].Cells[6].Value.ToString();
                string error = grd_Client_cost.Rows[i].Cells[5].Value.ToString();

                if (error != "Error")
                {


                    string county_type = grd_Client_cost.Rows[i].Cells[2].Value.ToString();




                    Hashtable htcheck = new Hashtable();
                    System.Data.DataTable dtcheck = new System.Data.DataTable();
                    htcheck.Add("@Trans", "CHECK");
                    htcheck.Add("@Client_Id", Client_ID);
                    htcheck.Add("@State_Id", State);
                    htcheck.Add("@County_Id", County);
                    dtcheck = dataaccess.ExecuteSP("Sp_External_Client_Sate_County", htcheck);
                    if (dtcheck.Rows.Count > 0)
                    {
                        Check_value = int.Parse(dtcheck.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Check_value = 0;
                    }

                    if (Check_value == 0)
                    {

                        Hashtable htabsinsert = new Hashtable();
                        System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                        htabsinsert.Add("@Trans", "INSERT");
                        htabsinsert.Add("@Client_Id", Client_ID);
                        htabsinsert.Add("@State_Id", State);
                        htabsinsert.Add("@County_Id", County);
                        htabsinsert.Add("@County_Type", Countytype);
                        dtabsinsert = dataaccess.ExecuteSP("Sp_External_Client_Sate_County", htabsinsert);

                    }
                    else if (Check_value > 0)
                    {
                        Hashtable htabsinsert = new Hashtable();
                        System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                      
                        htabsinsert.Add("@Trans", "UPDATE");
                        htabsinsert.Add("@Client_Id", Client_ID);
                        htabsinsert.Add("@State_Id", State);
                        htabsinsert.Add("@County_Id", County);
                        htabsinsert.Add("@County_Type", Countytype);
                        dtabsinsert = dataaccess.ExecuteSP("Sp_External_Client_Sate_County", htabsinsert);

                    }


                }
            }
            string title = "Successfull";
            MessageBox.Show("Record Imported Successfully",title);
        }

        private void btn_SampleFormat_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\Temp\OMS_Import\");
            string temppath = @"c:\Temp\OMS_Import\CountyImport.xlsx";
            File.Copy(@"\\192.168.12.33\OMS-Import_Excels\CountyImport.xlsx", temppath, true);
            
            Process.Start(temppath);
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            DataSet dsexport = new DataSet();

            ds.Clear();
            ds.Tables.Add(dtnonadded);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Convert_Dataset_to_Excel();
            }
            ds.Clear();
            dtnonadded.Clear();
        }
        private void Convert_Dataset_to_Excel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = true;
            Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            DataTableCollection collection = ds.Tables;

            for (int i = collection.Count; i > 0; i--)
            {
                Sheets xlSheets = null;
                Worksheet xlWorksheet = null;
                //Create Excel Sheets
                xlSheets = ExcelApp.Worksheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                               Type.Missing, Type.Missing, Type.Missing);

                System.Data.DataTable table = collection[i - 1];
                xlWorksheet.Name = table.TableName;

                for (int j = 1; j < table.Columns.Count + 1; j++)
                {
                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                }

                // Storing Each row and column value to excel sheet
                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 0; l < table.Columns.Count; l++)
                    {
                        ExcelApp.Cells[k + 2, l + 1] =
                        table.Rows[k].ItemArray[l].ToString();
                    }
                }
                ExcelApp.Columns.AutoFit();
            }
            ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
            ExcelApp.Visible = true;

        }

        private void Client_State_County_Type_Import_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
