using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;
using DevExpress.XtraGrid.Columns;
using InfiniteProgressBar;

namespace Ordermanagement_01.Masters
{
    public partial class CountyJudgementsLinks : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dataTableExcelData, dataTableWithNoErrors, dataTableErrors;
        private SqlConnection con;
        private clsProgress progress = new clsProgress();
        public CountyJudgementsLinks()
        {
            InitializeComponent();
            dataTableExcelData = new DataTable();
            dataTableWithNoErrors = new DataTable();
            dataTableErrors = new DataTable();
        }

        private void CountyJudgementsLinks_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.buttonRemoveErrors.Enabled = false;
            this.buttonImport.Enabled = false;
            FileDialogConfig();
            AddColumnsToDataTable();
        }

        private void FileDialogConfig()
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Title = "Browse files to import";
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Select File to import";
            openFileDialog1.FileName = "select";
            openFileDialog1.InitialDirectory = @"c:\";
        }

        private void AddColumnsToDataTable()
        {
            dataTableExcelData.Columns.AddRange(new DataColumn[]{
            new DataColumn("State", typeof(String)),
            new DataColumn("County",typeof(String)),
            new DataColumn("Online_Index",typeof(String)),
            new DataColumn("Website_Name",typeof(String)),
            new DataColumn("Subscription_Type",typeof(String)),
            new DataColumn("Subscription_Cost",typeof(String)),
            new DataColumn("Recorder_Weblink",typeof(String)),
            new DataColumn("Image_Subscription",typeof(String)),
            new DataColumn("Image_Cost",typeof(String)),
            new DataColumn("Images_Free",typeof(String)),
            new DataColumn("Images_From_Technically",typeof(String)),
            new DataColumn("Index_Data_Starts_From",typeof(String)),
            new DataColumn("Images_Starts_From",typeof(String)),
            new DataColumn("Index_User_Id",typeof(String)),
            new DataColumn("Index_Password",typeof(String)),
            new DataColumn("CCR_S",typeof(String)),
            new DataColumn("Assessor_Map",typeof(String)),
            new DataColumn("Plat_Map",typeof(String)),
            new DataColumn("Judgement_OR_Lien",typeof(String)),
            new DataColumn("Judgement_OR_Lien_Images",typeof(String)),
            new DataColumn("Judgement_OR_Lien_Web_Link_Prothonotary",typeof(String)),
            new DataColumn("Judgement_OR_Lien_Web_Link_Muncipal_Orphan",typeof(String)),
            new DataColumn("Judgement_OR_Lien_Web_Link_Superior_Court",typeof(String)),
            new DataColumn("JG_User_Id",typeof(String)),
            new DataColumn("JG_Password",typeof(String)),
            new DataColumn("Data_Tree_Images",typeof(String)),
            new DataColumn("Comments",typeof(String))            
            });
            dataTableErrors = dataTableExcelData.Clone();
            dataTableWithNoErrors = dataTableExcelData.Clone();            
            dataTableWithNoErrors.Columns.Add(new DataColumn("State_ID", typeof(Int32)));
            dataTableWithNoErrors.Columns.Add(new DataColumn("County_ID", typeof(Int32)));
            dataTableErrors.Columns.Add(new DataColumn("Error", typeof(String)));
        }

        private void buttonUploadExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                if (fileName != string.Empty)
                {
                    ProcessImport(fileName);
                }
                else
                {
                    XtraMessageBox.Show("Error in opening file");
                }
                BindGridCountyInfo();
            }
        }

        private void ProcessImport(string fileName)
        {
            dataTableExcelData.Rows.Clear();
            try
            {
                string xlConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                 fileName +
                                 ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection conn = new OleDbConnection(xlConn);
                OleDbCommand cmd = new OleDbCommand("select * from[" + "Sheet1" + "$]", conn);
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(dataTableExcelData);
                var ht1 = new Hashtable();
                ht1.Add("@Trans", "TRUNCATE_TEMP_TABLE");
                var d = new DataAccess().ExecuteSP("SP_County_Judgememts_Links", ht1);

                //Bulk insert to db temp table 
                using (con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString))
                {
                    con.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        bulkCopy.ColumnMappings.Add("STATE", "State");
                        bulkCopy.ColumnMappings.Add("COUNTY", "County");
                        bulkCopy.ColumnMappings.Add("Online_Index", "Online_Index");
                        bulkCopy.ColumnMappings.Add("Website_Name", "Website_Name");
                        bulkCopy.ColumnMappings.Add("Subscription_Type", "Subscription_Type");
                        bulkCopy.ColumnMappings.Add("Subscription_Cost", "Subscription_Cost");
                        bulkCopy.ColumnMappings.Add("Recorder_Weblink", "Recorder_Weblink");
                        bulkCopy.ColumnMappings.Add("Image_Subscription", "Image_Subscription");
                        bulkCopy.ColumnMappings.Add("Image_Cost", "Image_Cost");
                        bulkCopy.ColumnMappings.Add("Images_Free", "Images_Free");
                        bulkCopy.ColumnMappings.Add("Images_From_Technically", "Images_From_Technically");
                        bulkCopy.ColumnMappings.Add("Index_Data_Starts_From", "Index_Data_Starts_From");
                        bulkCopy.ColumnMappings.Add("Images_Starts_From", "Images_Starts_From");
                        bulkCopy.ColumnMappings.Add("Index_User_Id", "Index_User_Id");
                        bulkCopy.ColumnMappings.Add("Index_Password", "Index_Password");
                        bulkCopy.ColumnMappings.Add("CCR_S", "CCR_S");
                        bulkCopy.ColumnMappings.Add("Assessor_Map", "Assessor_Map");
                        bulkCopy.ColumnMappings.Add("Plat_Map", "Plat_Map");
                        bulkCopy.ColumnMappings.Add("Judgement_OR_Lien", "Judgement_OR_Lien");
                        bulkCopy.ColumnMappings.Add("Judgement_OR_Lien_Images", "Judgement_OR_Lien_Images");
                        bulkCopy.ColumnMappings.Add("Judgement_OR_Lien_Web_Link_Prothonotary", "Judgement_OR_Lien_Web_Link_Prothonotary");
                        bulkCopy.ColumnMappings.Add("Judgement_OR_Lien_Web_Link_Muncipal_Orphan", "Judgement_OR_Lien_Web_Link_Muncipal_Orphan");
                        bulkCopy.ColumnMappings.Add("Judgement_OR_Lien_Web_Link_Superior_Court", "Judgement_OR_Lien_Web_Link_Superior_Court");
                        bulkCopy.ColumnMappings.Add("JG_User_Id", "JG_User_Id");
                        bulkCopy.ColumnMappings.Add("JG_Password", "JG_Password");
                        bulkCopy.ColumnMappings.Add("Data_Tree_Images", "Data_Tree_Images");
                        bulkCopy.ColumnMappings.Add("Comments", "Comments");
                        bulkCopy.BulkCopyTimeout = 3000;
                        bulkCopy.BatchSize = 10000;
                        bulkCopy.DestinationTableName = "Tbl_Temp_County_Judgements_Links";
                        bulkCopy.WriteToServer(dataTableExcelData.CreateDataReader());
                        XtraMessageBox.Show("County Records Info uploaded successfully");
                    }
                    conn.Close();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Error in Uploading Excel");
            }
        }

        private void BindGridCountyInfo()
        {
            gridViewCounty_Info.Columns.ColumnByFieldName("State_ID").Visible = false;
            gridViewCounty_Info.Columns.ColumnByFieldName("County_ID").Visible = false;
            gridControlCounty_Info.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_TEMP_DATA");
            var dt = new DataAccess().ExecuteSP("SP_County_Judgememts_Links", ht);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlCounty_Info.DataSource = dt;
                this.gridViewCounty_Info.BestFitColumns();
                this.buttonRemoveErrors.Enabled = true;
                this.buttonImport.Enabled = true;               
                XtraMessageBox.Show("Total Records : " + gridViewCounty_Info.RowCount);
            }
            else
            {
                XtraMessageBox.Show("Failed to get the County Info");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            dataTableExcelData.Rows.Clear();
            dataTableWithNoErrors.Rows.Clear();
            dataTableErrors.Rows.Clear();
            gridControlCounty_Info.DataSource = null;          
            this.buttonRemoveErrors.Enabled = false;
            this.buttonImport.Enabled = false;
        }

        private void buttonRemoveErrors_Click(object sender, EventArgs e)
        {
            dataTableErrors.Rows.Clear();
            dataTableWithNoErrors.Rows.Clear();
            for (int i = 0; i < gridViewCounty_Info.DataRowCount; i++)
            {
                GridColumn columnState = gridViewCounty_Info.Columns.ColumnByFieldName("State_ID");
                GridColumn columnCounty = gridViewCounty_Info.Columns.ColumnByFieldName("County_ID");
                var state = gridViewCounty_Info.GetRowCellValue(i, columnState);
                var county = gridViewCounty_Info.GetRowCellValue(i, columnCounty);
                if (String.IsNullOrWhiteSpace(state.ToString()) || String.IsNullOrWhiteSpace(county.ToString()))
                {
                    dataTableErrors.ImportRow(gridViewCounty_Info.GetDataRow(i));
                    dataTableErrors.Rows[i]["Error"] = "Error in state or county names";
                }
                else
                {
                    dataTableWithNoErrors.ImportRow(gridViewCounty_Info.GetDataRow(i));
                }
            }

            XtraMessageBox.Show("Errors Found: " + dataTableErrors.Rows.Count + " Records to import : " + dataTableWithNoErrors.Rows.Count);
            if (dataTableWithNoErrors.Rows.Count > 0)
            {
                gridControlCounty_Info.DataSource = null;
                gridControlCounty_Info.DataSource = dataTableWithNoErrors;
                gridViewCounty_Info.BestFitColumns();
            }
            if (dataTableErrors.Rows.Count > 0)
            {
                ImportErrorsToExcel(dataTableErrors);
            }
            else {
                XtraMessageBox.Show("No Errors found in the file");
            }
            buttonRemoveErrors.Enabled = false;
        }

        private void ImportErrorsToExcel(DataTable dataTableErrors)
        {
            try
            {
                string filePath = @"C:\County Judgement Links\";
                string fileName = filePath + "County Judgement Links Errors -" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dataTableErrors, "Errors");
                        wb.SaveAs(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Failed to export errors" + ex);
            }
        }  
        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (dataTableWithNoErrors.Rows.Count < 1)
            {
                XtraMessageBox.Show("No records found to import");
                return;
            }
            try
            {
                progress.startProgress();
                // var watch = System.Diagnostics.Stopwatch.StartNew();
                foreach (DataRow row in dataTableWithNoErrors.Rows)
                {
                    var ht = new Hashtable();
                    ht.Add("@Trans", "MERGE");
                    ht.Add("@State_Id", Convert.ToInt32(row["State_ID"]));
                    ht.Add("@County_Id", Convert.ToInt32(row["County_ID"]));
                    ht.Add("@Online_Index", row["Online_Index"].ToString());
                    ht.Add("@Website_Name", row["Website_Name"].ToString());
                    ht.Add("@Subscription_Type", row["Subscription_Type"].ToString());
                    ht.Add("@Subscription_Cost", row["Subscription_Cost"].ToString());
                    ht.Add("@Recorder_Weblink", row["Recorder_Weblink"].ToString());
                    ht.Add("@Image_Subscription", row["Image_Subscription"].ToString());
                    ht.Add("@Image_Cost", row["Image_Cost"].ToString());
                    ht.Add("@Images_Free", row["Images_Free"].ToString());
                    ht.Add("@Images_From_Technically", row["Images_From_Technically"].ToString());
                    ht.Add("@Index_Data_Starts_From", row["Index_Data_Starts_From"].ToString());
                    ht.Add("@Images_Starts_From", row["Images_Starts_From"].ToString());
                    ht.Add("@Index_User_Id", row["Index_User_Id"].ToString());
                    ht.Add("@Index_Password", row["Index_Password"].ToString());
                    ht.Add("@CCR_S", row["CCR_S"].ToString());
                    ht.Add("@Assessor_Map", row["Assessor_Map"].ToString());
                    ht.Add("@Plat_Map", row["Plat_Map"].ToString());
                    ht.Add("@Judgement_OR_Lien", row["Judgement_OR_Lien"].ToString());
                    ht.Add("@Judgement_OR_Lien_Images", row["Judgement_OR_Lien_Images"].ToString());
                    ht.Add("@Judgement_OR_Lien_Web_Link_Prothonotary", row["Judgement_OR_Lien_Web_Link_Prothonotary"].ToString());
                    ht.Add("@Judgement_OR_Lien_Web_Link_Muncipal_Orphan", row["Judgement_OR_Lien_Web_Link_Muncipal_Orphan"].ToString());
                    ht.Add("@Judgement_OR_Lien_Web_Link_Superior_Court", row["Judgement_OR_Lien_Web_Link_Superior_Court"].ToString());
                    ht.Add("@JG_User_Id", row["JG_User_Id"].ToString());
                    ht.Add("@JG_Password", row["JG_Password"].ToString());
                    ht.Add("@Data_Tree_Images", row["Data_Tree_Images"].ToString());
                    ht.Add("@Comments", row["Comments"].ToString());
                    int count = new DataAccess().ExecuteSPForCRUD("SP_County_Judgememts_Links", ht);
                }
                //   watch.Stop();
                // XtraMessageBox.Show("time taken : " + (watch.ElapsedMilliseconds) / 1000);
                XtraMessageBox.Show("County Judgements links Records imported successfully");
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Failed to import county judgements links");
            }
            finally {
                progress.stopProgress();
            }
        }

        private void buttonSample_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(@"C:\County Judgement Links\");
                string temppath = @"C:\County Judgement Links\County_Judgement_Links.xlsx";
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\County_Judgements_Links.xlsx", temppath, true);
                System.Diagnostics.Process.Start(temppath);
            }
            catch (Exception) {
                XtraMessageBox.Show("Error in opening file");
            }
        }
    }
}