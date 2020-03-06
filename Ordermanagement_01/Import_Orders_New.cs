using ClosedXML.Excel;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class Import_Orders_New : XtraForm
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        Hashtable ht_Subprocess = new Hashtable();
        DataTable dt_Subprocess = new DataTable();
        DataTable dt = new DataTable();
        Hashtable ht_Ordertype = new Hashtable();
        DataTable dt_Ordertype = new DataTable();
        DropDownistBindClass dbc = new DropDownistBindClass();
        private DataAccess da;
        SqlConnection con;
        Hashtable htDuplicate = new Hashtable();
        DataTable dtDuplicate = new DataTable();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string MAX_ORDER_NUMBER;
        int clientId, subProcessId, orderStatusId, orderTypeId, stateId, countyId, categoryType, CHECK_ORDER;
        int value = 0, Count, userid, Assign_County_Type_ID;
        DateTime prior_date, Received_date;
        string date_prior, date_received, priorEffectiveDate, receivedDate, clientOrderRef, zip, orderTime, comments, APN, city;
        int Assigning_To_Vendor, Vendor_Order_capacity;
        decimal Vendor_Order_Percentage, Vendor_Balance_Percentage, Total_Vendor_Balance_Percentage, Total_Vendor_Alloacated_Percentage;
        int No_Of_Order_Assignd_for_Vendor, Vendor_Id;
        string Vendors_State_County, Vendors_Order_Type, Vendors_Client_Sub_Client;
        int Vendor_Id_For_Assign;
        string lblOrder_Type_For_Vendor, Vend_date, clientOrderNumber, orderType, orderTask, client, subClient, state, county,
            borrowerFirstName, borrowerLastName, address, copyType;
        DataTable dataTableErrors = new DataTable();
        DataTable dataTableExportToDb = new DataTable();
        DataTable dataTableImportExcel = new DataTable();

        private static int duplicateCount = 0, ErrorCount = 0, existingCount = 0, tempErrors = 0;
        private object EnteredOrderId;
        private int Vendor_Order_Allocation_Count;

        public Import_Orders_New(int User_id)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            openFileDialogImport_Orders.CheckFileExists = true;
            openFileDialogImport_Orders.CheckPathExists = true;
            openFileDialogImport_Orders.Title = "Browse files to import";
            openFileDialogImport_Orders.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialogImport_Orders.FilterIndex = 1;
            openFileDialogImport_Orders.Title = "Select File to import";
            openFileDialogImport_Orders.FileName = "select";
            openFileDialogImport_Orders.InitialDirectory = @"c:\";
            openFileDialogImport_Orders.RestoreDirectory = true;
            userid = User_id;
            grd_order_View.ShowFindPanel();
            grd_order_View.IndicatorWidth = 30;
        }

        private object GetOrderTypeId(string prod_type)
        {
            var ht = new Hashtable();
            ht.Add("@Trans", "GET_PRODUCT_TYPE_ID");
            ht.Add("@Product_Type", prod_type);
            var dt = new DataAccess().ExecuteSP("SP_Import_Tax_Order", ht);
            var obj = 0;
            foreach (DataRow row in dt.Rows)
            {
                obj = Convert.ToInt32(row["Order_Type_ID"].ToString());
            }
            return obj;
        }

        private void Clear()
        {
            dataTableImportExcel.Rows.Clear();
            labelDuplicatesCount.Text = "00";
            labelErrors.Text = "00";
            labelExistingOrdersCount.Text = "00";
            labelOrdersCount.Text = "00";
            btn_Export_Errors.Enabled = false;
            grd_order_Control.DataSource = null;
            dataTableExportToDb.Rows.Clear();
            btn_Import_Orders.Enabled = false;
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"d:\";
            var txtFileName = fdlg.FileName;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName = fdlg.FileName;
                ProcessImport(txtFileName);
                Application.DoEvents();
            }
        }

        private void ProcessImport(string fileName)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            dataTableImportExcel.Columns.Clear();
            dataTableImportExcel.Rows.Clear();
            try
            {
                using (XLWorkbook workBook = new XLWorkbook(fileName))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable dt = new DataTable();

                    //Loop through the Worksheet rows.
                    bool firstRow = true;

                    foreach (IXLRow row in workSheet.Rows())
                    {


                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dataTableImportExcel.Columns.Add(cell.Value.ToString(), typeof(string));
                            }
                            firstRow = false;
                        }
                        else
                        {
                            if (row.IsEmpty())
                                continue;
                            //Add rows to DataTable.
                            dataTableImportExcel.Rows.Add();
                            int i = 0;
                            try
                            {
                                foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                                {
                                    dataTableImportExcel.Rows[dataTableImportExcel.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                            catch (Exception ex)
                            {
                                dataTableImportExcel.Rows.RemoveAt(dataTableImportExcel.Rows.Count - 1);
                                break;
                            }
                        }
                    }
                }
                dataTableErrors.Columns.Clear();
                dataTableExportToDb.Columns.Clear();
                dataTableErrors = dataTableImportExcel.Clone();
                dataTableErrors.Columns.AddRange(new DataColumn[]{
                new DataColumn("Error_Status",typeof(string)),
                new DataColumn("State_ID",typeof(int)),
                new DataColumn("County_ID",typeof(int)),
                new DataColumn("Order_Type_ID",typeof(int)),
                new DataColumn("Order_Status_ID",typeof(int)),
                new DataColumn("Client_Id",typeof(int)),
                new DataColumn("Subprocess_Id",typeof(int)),
                new DataColumn("Exist_count",typeof(int)),
                new DataColumn("Duplicate_Count",typeof(int)),
                 new DataColumn("Order_Source_Type_ID",typeof(int))
                });
                dataTableExportToDb = dataTableErrors.Clone();
                var ht1 = new Hashtable();
                ht1.Add("@Trans", "TRUNCATE");
                da = new DataAccess();
                var d = da.ExecuteSP("Sp_Temp_Order", ht1);
                using (con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString))
                {
                    con.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        bulkCopy.ColumnMappings.Add("Received_Date", "Received_Date");
                        bulkCopy.ColumnMappings.Add("Time", "Time");
                        bulkCopy.ColumnMappings.Add("Client_Order_Number", "Client_Order_Number");
                        bulkCopy.ColumnMappings.Add("Order_Type", "Order_Type");
                        bulkCopy.ColumnMappings.Add("Task", "Task");
                        bulkCopy.ColumnMappings.Add("Client", "Client");
                        bulkCopy.ColumnMappings.Add("Sub_Client", "Sub_Client");
                        bulkCopy.ColumnMappings.Add("Client_Order_Ref", "Client_Order_Ref");
                        bulkCopy.ColumnMappings.Add("Address", "Address");
                        bulkCopy.ColumnMappings.Add("State", "State");
                        bulkCopy.ColumnMappings.Add("County", "County");
                        bulkCopy.ColumnMappings.Add("City", "City");
                        bulkCopy.ColumnMappings.Add("Zip", "Zip");
                        bulkCopy.ColumnMappings.Add("Borrower_First_Name", "Borrower_First_Name");
                        bulkCopy.ColumnMappings.Add("Borrower_Last_Name", "Borrower_Last_Name");
                        bulkCopy.ColumnMappings.Add("Comments", "Comments");
                        bulkCopy.ColumnMappings.Add("Prior_Effective_Date", "Prior_Effective_Date");
                        bulkCopy.ColumnMappings.Add("APN", "APN");
                        bulkCopy.ColumnMappings.Add("Target_Category", "Category_Type");
                        bulkCopy.ColumnMappings.Add("Copy_Type", "Copy_Type");
                        // bulkCopy.ColumnMappings.Add("Temp_Order_Id", "Temp_Order_Id");
                        bulkCopy.BulkCopyTimeout = 3000;
                        bulkCopy.BatchSize = 10000;
                        bulkCopy.DestinationTableName = "Tbl_Temp_Orders";
                        bulkCopy.WriteToServer(dataTableImportExcel.CreateDataReader());
                        SplashScreenManager.CloseForm(false);
                        //   XtraMessageBox.Show("Orders uploaded successfully");
                    }

                    //conn.Close();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(e.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

            BindOrders();
            ValidateOrders();
        }

        protected void BindOrders()
        {
            da = new DataAccess();
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT");
            dt = da.ExecuteSP("Sp_Temp_Order", ht);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("Error_Status", typeof(string));
                grd_order_Control.DataSource = dt;
                labelOrdersCount.Text = "";
                labelOrdersCount.Text = grd_order_View.DataRowCount.ToString();
                grd_order_View.BestFitColumns();
                grd_order_View.OptionsSelection.EnableAppearanceFocusedCell = false;
                grd_order_View.OptionsSelection.EnableAppearanceFocusedRow = false;
                grd_order_View.OptionsSelection.EnableAppearanceHideSelection = false;
            }
            else
            {
                XtraMessageBox.Show("Data not found");
            }
        }

        private void ValidateOrders()
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                labelDuplicatesCount.Text = "";
                labelErrors.Text = "";
                labelExistingOrdersCount.Text = "";
                duplicateCount = 0;
                tempErrors = 0;
                existingCount = 0;
                for (int i = 0; i < grd_order_View.DataRowCount; i++)
                {
                    // null or empty value in columns 
                    if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Client_Order_Number")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Order Number not found");
                    }
                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Task")).ToString())
                        || string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Order_Status_ID")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Order_Task Missing");
                    }
                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Order_Type")).ToString())
                        || string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Order_Type_ID")).ToString()))
                    {

                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Order Type missing or invalid order type");
                    }
                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Received_Date")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Date not found");
                    }
                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Time")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Time not found");
                    }

                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Client")).ToString())
                        || string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Client_Id")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Client not found");
                    }

                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Sub_Client")).ToString())
                        || string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Subprocess_Id")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Sub Client not found");
                    }

                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("State")).ToString())
                        || string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("State_ID")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "State not found");
                    }
                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("County")).ToString())
                        || string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("County_ID")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "County not found");
                    }
                    else if (Convert.ToInt32(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Exist_count"))) > 0)
                    {
                        existingCount += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Existing");
                    }
                    else if (Convert.ToInt32(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Duplicate_Count"))) > 0)
                    {
                        duplicateCount += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Duplicate");
                    }
                    else if (Convert.ToInt32(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("ad_duplicate_count"))) > 0)
                    {
                        duplicateCount += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Duplicate Address");
                    }
                    else if (string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Category_Type")).ToString())
                                           || string.IsNullOrWhiteSpace(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Order_Source_Type_ID")).ToString()))
                    {
                        tempErrors += 1;
                        grd_order_View.SetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status"), "Target Category Not Found");
                    }


                }   //End for
                RemoveErrors();

                ErrorCount = tempErrors + duplicateCount + existingCount;
                labelDuplicatesCount.Text = duplicateCount.ToString();
                labelExistingOrdersCount.Text = existingCount.ToString();
                int valid = grd_order_View.RowCount - ErrorCount;
                labelErrors.Text = tempErrors.ToString();
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Errors : " + ErrorCount);

                if (tempErrors > 0 || existingCount > 0 || duplicateCount > 0)
                {
                    btn_Export_Errors.Enabled = true;
                }
                if (dataTableExportToDb.Rows.Count > 0)
                {
                    btn_Import_Orders.Enabled = true;
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void RemoveErrors()
        {

            dataTableErrors.Rows.Clear();
            dataTableExportToDb.Rows.Clear();
            for (int i = 0; i < grd_order_View.DataRowCount; i++)
            {
                if (string.IsNullOrEmpty(grd_order_View.GetRowCellValue(i, grd_order_View.Columns.ColumnByFieldName("Error_Status")).ToString()))
                {
                    dataTableExportToDb.ImportRow(grd_order_View.GetDataRow(i));
                }
                else
                {
                    DataRow row = grd_order_View.GetDataRow(i);
                    DataRow row1 = dataTableErrors.NewRow();
                    row1["Received_Date"] = row["Received_Date"];
                    row1["Time"] = row["Time"];
                    row1["Client_Order_Number"] = row["Client_Order_Number"];
                    row1["Order_Type"] = row["Order_Type"];
                    row1["Task"] = row["Task"];
                    row1["Client"] = row["Client"];
                    row1["Sub_Client"] = row["Sub_Client"];
                    row1["Client_Order_Ref"] = row["Client_Order_Ref"];
                    row1["Address"] = row["Address"];
                    row1["State"] = row["State"];
                    row1["County"] = row["County"];
                    row1["City"] = row["City"];
                    row1["Zip"] = row["Zip"];
                    row1["Borrower_First_Name"] = row["Borrower_First_Name"];
                    row1["Borrower_Last_Name"] = row["Borrower_Last_Name"];
                    row1["Comments"] = row["Comments"];
                    row1["Prior_Effective_Date"] = row["Prior_Effective_Date"];
                    row1["APN"] = row["APN"];
                    row1["Target_Category"] = row["Category_Type"];
                    row1["Copy_Type"] = row["Copy_Type"];
                    row1["Error_Status"] = row["Error_Status"];
                    row1["State_ID"] = row["State_ID"];
                    row1["County_ID"] = row["County_ID"];
                    row1["Order_Type_ID"] = row["Order_Type_ID"];
                    row1["Order_Status_ID"] = row["Order_Status_ID"];
                    row1["Client_Id"] = row["Client_Id"];
                    row1["Subprocess_Id"] = row["Subprocess_Id"];
                    row1["Exist_count"] = row["Exist_count"];
                    row1["Duplicate_Count"] = row["Duplicate_Count"];
                    row1["Order_Source_Type_ID"] = row["Order_Source_Type_ID"];
                    dataTableErrors.Rows.Add(row1);
                }
            }
        }

        private void btn_Export_Errors_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                string filePath = @"C:\Order Import\";
                string fileName = filePath + "Order Import-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        dataTableErrors.Columns.Remove("State_ID");
                        dataTableErrors.Columns.Remove("County_ID");
                        dataTableErrors.Columns.Remove("Order_Type_ID");
                        dataTableErrors.Columns.Remove("Order_Status_ID");
                        dataTableErrors.Columns.Remove("Client_Id");
                        dataTableErrors.Columns.Remove("Subprocess_Id");
                        dataTableErrors.Columns.Remove("Exist_count");
                        dataTableErrors.Columns.Remove("Duplicate_Count");
                        dataTableErrors.Columns.Remove("Order_Source_Type_ID");

                        wb.Worksheets.Add(dataTableErrors, "Errors_In_Order");
                        try
                        {
                            wb.SaveAs(fileName);
                            Process.Start(fileName);
                        }
                        catch (Exception ex)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Failed to export errors in orders");
                        }
                    }
                }
                BindOrdersToExport(dataTableExportToDb);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void BindOrdersToExport(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                grd_order_Control.DataSource = null;
                grd_order_Control.DataSource = dt;
                btn_Export_Errors.Enabled = false;
                labelDuplicatesCount.Text = "00";
                labelErrors.Text = "00";
                labelExistingOrdersCount.Text = "00";
                labelOrdersCount.Text = grd_order_View.RowCount.ToString();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_Import_Orders_Click(object sender, EventArgs e)
        {
            ImportOrdersToDB();
        }

        private void ImportOrdersToDB()
        {
            int CountOrderInsert = 0;
            bool isSuccess = false;
            int count = 0;
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                if (dataTableExportToDb.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTableExportToDb.Rows)
                    {
                        clientOrderNumber = row["Client_Order_Number"].ToString();
                        orderType = row["Order_Type"].ToString();
                        orderTask = row["Task"].ToString();
                        client = row["Client"].ToString();
                        subClient = row["Sub_Client"].ToString();
                        receivedDate = row["Received_Date"].ToString();
                        state = row["State"].ToString();
                        county = row["County"].ToString();
                        borrowerFirstName = row["Borrower_First_Name"].ToString();
                        borrowerLastName = row["Borrower_Last_Name"].ToString();
                        address = row["Address"].ToString();
                        city = row["City"].ToString();
                        zip = row["Zip"].ToString();
                        orderTime = row["Time"].ToString();
                        comments = row["Comments"].ToString();
                        APN = row["APN"].ToString();
                        priorEffectiveDate = row["Prior_Effective_Date"].ToString();
                        clientOrderRef = row["Client_Order_Ref"].ToString();
                        copyType = row["Copy_Type"].ToString();

                        lblOrder_Type_For_Vendor = orderType;
                        stateId = Convert.ToInt32(row["State_ID"].ToString());
                        countyId = Convert.ToInt32(row["County_ID"].ToString());
                        orderTypeId = Convert.ToInt32(row["Order_Type_ID"].ToString());
                        orderStatusId = Convert.ToInt32(row["Order_Status_ID"].ToString());
                        clientId = Convert.ToInt32(row["Client_Id"].ToString());
                        subProcessId = Convert.ToInt32(row["Subprocess_Id"].ToString());
                        categoryType = Convert.ToInt32(row["Order_Source_Type_ID"].ToString());

                        Assign_County_Type_ID = GetCountyType(countyId);

                        // get_max order number
                        Hashtable htmax = new Hashtable();
                        DataTable dtmax = new DataTable();
                        htmax.Add("@Trans", "MAX_ORDER_NO");
                        dtmax = dataaccess.ExecuteSP("Sp_Order", htmax);
                        if (dtmax.Rows.Count > 0)
                        {
                            MAX_ORDER_NUMBER = "DRN" + "-" + dtmax.Rows[0]["ORDER_NUMBER"].ToString();
                        }

                        //check order number exist
                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();

                        htcheck.Add("@Trans", "CHECK_ORDER_NUMBER");
                        htcheck.Add("@Client_Order_Number", clientOrderNumber);
                        dtcheck = dataaccess.ExecuteSP("Sp_Order", htcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            CHECK_ORDER = int.Parse(dtcheck.Rows[0]["count"].ToString());
                        }
                        else { CHECK_ORDER = 0; }

                        //DateTime Received_date;
                        //string date_received;

                        if (priorEffectiveDate != "")
                        {
                            prior_date = Convert.ToDateTime(priorEffectiveDate);
                            date_prior = prior_date.ToString("MM/dd/yyyy");
                        }
                        else { }

                        if (receivedDate != "")
                        {
                            Received_date = Convert.ToDateTime(receivedDate);
                            date_received = Received_date.ToString("MM/dd/yyyy");
                        }
                        else
                        {

                        }

                        CountOrderInsert = CountOrderInsert + 1;
                        if (CHECK_ORDER == 0)
                        {
                            Hashtable htinsertrec = new Hashtable();
                            DataTable dtinsertrec = new DataTable();

                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Sub_ProcessId", subProcessId);
                            htinsertrec.Add("@Placed_By", userid);
                            htinsertrec.Add("@Order_Type", orderTypeId);
                            htinsertrec.Add("@Order_Number", MAX_ORDER_NUMBER);
                            htinsertrec.Add("@Client_Order_Number", clientOrderNumber);

                            // This is for Tax Internal Client Orders Allocation

                            //==================================================

                            Hashtable ht_check = new Hashtable();
                            DataTable dt_check = new System.Data.DataTable();
                            ht_check.Add("@Trans", "CHECK");
                            ht_check.Add("@Client_Id", clientId);
                            ht_check.Add("@Order_Type_Id", orderTypeId);
                            ht_check.Add("@flag", "False");
                            dt_check = dataaccess.ExecuteSP("Sp_Tax_Order_Movement_Client_Product_Type", ht_check);

                            int Check_Count = 0;
                            if (dt_check.Rows.Count > 0)
                            {
                                Check_Count = int.Parse(dt_check.Rows[0]["COUNT"].ToString());
                            }
                            else { Check_Count = 0; }

                            // For Non Tax Orders
                            if (orderTypeId != 70 && orderTypeId != 110)
                            {
                                if (clientId == 28 || clientId == 31)//  //Moving ABC & SPC Client Orders Into Re-Search Oreder Allocation Order Queue
                                {
                                    htinsertrec.Add("@Order_Status", 25);
                                }
                                else
                                {
                                    if (Assign_County_Type_ID == 2)
                                    {
                                        //moving abstrctshop client and abstractor order moving to research order queue
                                        if (Assign_County_Type_ID == 2 && clientId == 31)
                                        {
                                            htinsertrec.Add("@Order_Status", 25);
                                        }
                                        else { htinsertrec.Add("@Order_Status", 17); }
                                    }
                                    else
                                    {
                                        htinsertrec.Add("@Order_Status", orderStatusId);
                                    }
                                }
                            }
                            else if (orderTypeId == 70 || orderTypeId == 110)
                            {

                                htinsertrec.Add("@Order_Status", 21);

                            }


                            htinsertrec.Add("@Client_Order_Ref", clientOrderRef);
                            htinsertrec.Add("@Order_Progress", 8);
                            htinsertrec.Add("@Zip", zip);
                            htinsertrec.Add("@City", city);
                            htinsertrec.Add("@Date", date_received);
                            htinsertrec.Add("@Borrower_Name", borrowerFirstName);
                            htinsertrec.Add("@Borrower_Name2", borrowerLastName);
                            htinsertrec.Add("@Address", address);
                            htinsertrec.Add("@APN", APN);
                            htinsertrec.Add("@County", countyId);
                            htinsertrec.Add("@State", stateId);
                            htinsertrec.Add("@Recived_Date", date_received);
                            htinsertrec.Add("@Order_Prior_Date", date_prior);
                            htinsertrec.Add("@Recived_Time", orderTime);
                            htinsertrec.Add("@Notes", comments);
                            htinsertrec.Add("@Inserted_By", userid);
                            htinsertrec.Add("@Inserted_date", DateTime.Now);
                            htinsertrec.Add("@Category_Type_Id", categoryType);
                            htinsertrec.Add("@Copy_Type", copyType);
                            htinsertrec.Add("@status", "True");
                            htinsertrec.Add("@Order_Assign_Type", Assign_County_Type_ID);
                            //Entered_OrderId = dataaccess.ExecuteSPForScalar("Sp_Order", htinsertrec);
                            EnteredOrderId = da.ExecuteSPForScalar("Sp_Order", htinsertrec);
                            int Entered_OrderId = Convert.ToInt32(EnteredOrderId);

                            if (Entered_OrderId > 0)
                            {
                                isSuccess = true;
                                count++;
                            }
                            else
                            {
                                isSuccess = false;
                            }

                            Hashtable ht_Order_History = new Hashtable();
                            DataTable dt_Order_History = new DataTable();
                            ht_Order_History.Add("@Trans", "INSERT");
                            ht_Order_History.Add("@Order_Id", EnteredOrderId);
                            // For Tax Orders
                            if (Check_Count == 0)
                            {
                                if (orderTypeId != 70 || orderTypeId != 110)
                                {
                                    if (clientId == 28)
                                    {
                                        ht_Order_History.Add("@Status_Id", 25);
                                    }
                                    else
                                    {
                                        ht_Order_History.Add("@Status_Id", orderStatusId);

                                    }
                                }
                            }
                            else
                            {

                            }

                            ht_Order_History.Add("@Progress_Id", 8);
                            ht_Order_History.Add("@Assigned_By", userid);

                            ht_Order_History.Add("@Modification_Type", "Order Create");

                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                            if (Check_Count > 0)
                            {
                                // This is for Tax Internal Client Orders Allocation

                                //==================================================

                                InsertInternalTaxOrderStatus();


                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                                htupdate.Add("@Order_ID", EnteredOrderId);
                                htupdate.Add("@Search_Tax_Request", "True");

                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                                Hashtable httaxupdate = new Hashtable();
                                System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                                httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
                                httaxupdate.Add("@Order_ID", EnteredOrderId);
                                httaxupdate.Add("@Search_Tax_Request_Progress", 8);

                                dttaxupdate = dataaccess.ExecuteSP("Sp_Order", httaxupdate);

                                //OrderHistory
                                Hashtable ht_Order_History1 = new Hashtable();
                                DataTable dt_Order_History1 = new DataTable();
                                ht_Order_History1.Add("@Trans", "INSERT");
                                ht_Order_History1.Add("@Order_Id", EnteredOrderId);
                                ht_Order_History1.Add("@User_Id", userid);
                                ht_Order_History1.Add("@Status_Id", 26);
                                ht_Order_History1.Add("@Progress_Id", 8);
                                ht_Order_History1.Add("@Work_Type", 1);
                                ht_Order_History1.Add("@Assigned_By", userid);
                                ht_Order_History1.Add("@Modification_Type", "Order Moved Tax Queue");
                                dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                            }

                            //Assiging the VA Sate and FAIRFAX county will assign to the Rajani  User
                            if (stateId == 47 && orderTypeId != 70 && orderTypeId != 110)
                            {
                                if (clientId != 28)
                                {
                                    if (countyId == 2857 || countyId == 2858)
                                    {
                                        AssignOrderForUser();
                                    }
                                }
                            }
                            if (Assign_County_Type_ID != 2 && orderTypeId != 70 && orderTypeId != 110 && clientId != 28)
                            {
                                if (countyId != 2857 || countyId != 2858)
                                {
                                    VendorOrderAllocateNew();
                                }
                            }


                            //Assigning the order to The Tax Allocation
                            //if (Check_Count == 0)
                            //{
                            if (orderTypeId == 70 || orderTypeId == 110)
                            {
                                InsertTaxOrderStatus();
                            }
                            //}

                            GetMaximumOrderNumber();
                            //}                            
                        }
                    }// for each close                   
                }
                if (isSuccess == true)
                {
                    SplashScreenManager.CloseForm(false);
                    string msg = (count == 1) ? " Order Imported Successfully" : " Orders Imported Successfully";
                    XtraMessageBox.Show(count + msg);
                    Clear();
                }
                else if (isSuccess == false)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Failed to export orders");
                    Clear();
                }
            }// try close
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Failed to export Orders");
                Clear();
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private int GetCountyType(int countyId)
        {
            int id = 0;
            string assignType = "";
            var ht = new Hashtable();

            ht.Add("@Trans", "GET_COUNTY_TYPE");
            ht.Add("@County", countyId);
            var dt = dataaccess.ExecuteSP("Sp_Order", ht);

            if (dt != null && dt.Rows.Count > 0)
            {
                assignType = dt.Rows[0]["County_Type"].ToString();

                if (assignType == "TIER 1")
                {
                    id = 1;
                }
                else if (assignType == "TIER 1")
                {
                    id = 2;
                }
            }
            return id;
        }

        private void InsertInternalTaxOrderStatus()
        {

            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", EnteredOrderId);
            httax.Add("@Order_Task", 26);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", 1);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);
        }

        protected void AssignOrderForUser()
        {
            //For the order is VA STATE FAIRFOX COUNTY then IT Will Assign to The User Rajiniganth ORDER WILL ASSIGN

            string lbl_Allocated_Userid = "7";
            Hashtable htchk_Assign = new Hashtable();
            DataTable dtchk_Assign = new System.Data.DataTable();
            htchk_Assign.Add("@Trans", "ORDER_ASSIGN_VERIFY");
            htchk_Assign.Add("@Order_Id", EnteredOrderId);
            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            if (dtchk_Assign.Rows.Count <= 0)
            {
                Hashtable htupassin = new Hashtable();
                DataTable dtupassign = new DataTable();

                htupassin.Add("@Trans", "DELET_BY_ORDER");
                htupassin.Add("@Order_Id", EnteredOrderId);
                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                Hashtable htinsert_Assign = new Hashtable();
                DataTable dtinsertrec_Assign = new System.Data.DataTable();
                htinsert_Assign.Add("@Trans", "INSERT");
                htinsert_Assign.Add("@Order_Id", EnteredOrderId);
                htinsert_Assign.Add("@Assigned_By", userid);
                htinsert_Assign.Add("@Modified_By", userid);
                htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                htinsert_Assign.Add("@status", "True");
                dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);
            }

            Hashtable htinsertrec = new Hashtable();
            DataTable dtinsertrec = new System.Data.DataTable();

            //23-01-2018

            DateTime date = new DateTime();
            date = DateTime.Now;
            DateTime time;
            date = DateTime.Now;
            string dateeval = date.ToString("MM/dd/yyyy");


            htinsertrec.Add("@Trans", "UPDATE_REALLOCATE");
            htinsertrec.Add("@Order_Id", EnteredOrderId);
            htinsertrec.Add("@User_Id", 7);
            htinsertrec.Add("@Order_Status_Id", 2);
            htinsertrec.Add("@Order_Progress_Id", 6);
            htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
            htinsertrec.Add("@Assigned_By", userid);
            htinsertrec.Add("@Modified_By", userid);
            htinsertrec.Add("@Modified_Date", DateTime.Now);
            htinsertrec.Add("@status", "True");
            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);


            Hashtable htorderStatus = new Hashtable();
            DataTable dtorderStatus = new DataTable();
            htorderStatus.Add("@Trans", "UPDATE_STATUS");
            htorderStatus.Add("@Order_ID", EnteredOrderId);
            htorderStatus.Add("@Order_Status", 2);
            htorderStatus.Add("@Modified_By", userid);
            htorderStatus.Add("@Modified_Date", date);
            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);
            Hashtable htorderStatus_Allocate = new Hashtable();
            DataTable dtorderStatus_Allocate = new DataTable();
            htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
            htorderStatus_Allocate.Add("@Order_ID", EnteredOrderId);
            htorderStatus_Allocate.Add("@Order_Status_Id", 2);
            htorderStatus_Allocate.Add("@Modified_By", userid);
            htorderStatus_Allocate.Add("@Assigned_Date", Convert.ToString(dateeval));
            htorderStatus_Allocate.Add("@Assigned_By", userid);
            //For this User VA STATE FAIRFOX COUNTY ORDER WILL ASSIGN
            htorderStatus_Allocate.Add("@User_Id", 7);
            htorderStatus_Allocate.Add("@Modified_Date", date);
            dtorderStatus_Allocate = dataaccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);


            Hashtable htupdate_Prog = new Hashtable();
            DataTable dtupdate_Prog = new System.Data.DataTable();
            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
            htupdate_Prog.Add("@Order_ID", EnteredOrderId);
            htupdate_Prog.Add("@Order_Progress", 6);
            htupdate_Prog.Add("@Modified_By", userid);
            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


            //OrderHistory
            Hashtable ht_Order_History = new Hashtable();
            DataTable dt_Order_History = new DataTable();
            ht_Order_History.Add("@Trans", "INSERT");
            ht_Order_History.Add("@Order_Id", EnteredOrderId);
            ht_Order_History.Add("@User_Id", 7);
            ht_Order_History.Add("@Status_Id", 2);
            ht_Order_History.Add("@Progress_Id", 6);
            ht_Order_History.Add("@Work_Type", 1);
            ht_Order_History.Add("@Assigned_By", userid);
            ht_Order_History.Add("@Modification_Type", "Order Assigned Automatically");
            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

        }

        private void InsertTaxOrderStatus()
        {

            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", EnteredOrderId);
            httax.Add("@Order_Task", 21);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", 1);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);
        }

        private void VendorOrderAllocateNew()
        {
            int Order_Type_ABS;
            Vendor_Order_Allocation_Count = 0;
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABBR");
            ht_BIND.Add("@Order_Type", lblOrder_Type_For_Vendor);
            dt_BIND = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_BIND);
            if (dt_BIND.Rows.Count > 0)
            {
                Order_Type_ABS = int.Parse(dt_BIND.Rows[0]["OrderType_ABS_Id"].ToString());
            }
            else
            {
                Order_Type_ABS = 0;
            }
            if (Order_Type_ABS != 0)
            {
                int State_Id = stateId;

                int County_Id = countyId;
                Hashtable htvendorname = new Hashtable();
                DataTable dtvendorname = new DataTable();
                htvendorname.Add("@Trans", "GET_VENDORS_STATE_COUNTY_WISE");
                htvendorname.Add("@State_Id", State_Id);
                htvendorname.Add("@County_Id", County_Id);
                dtvendorname = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendorname);
                Vendors_State_County = string.Empty;
                if (dtvendorname.Rows.Count > 0)
                {
                    for (int i = 0; i < dtvendorname.Rows.Count; i++)
                    {
                        Vendors_State_County = Vendors_State_County + dtvendorname.Rows[i]["Vendor_Id"].ToString();
                        Vendors_State_County += (i < dtvendorname.Rows.Count) ? "," : string.Empty;

                    }

                    Hashtable htcheck_Vendor_Order_Type_Abs = new Hashtable();
                    DataTable dtcheck_Vendor_Order_Type_Abs = new DataTable();
                    htcheck_Vendor_Order_Type_Abs.Add("@Trans", "GET_VENDOR_ORDER_TYPE_COVERAGE");
                    htcheck_Vendor_Order_Type_Abs.Add("@Vendors_Id", Vendors_State_County);
                    htcheck_Vendor_Order_Type_Abs.Add("@Order_Type_Abs_Id", Order_Type_ABS);
                    dtcheck_Vendor_Order_Type_Abs = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", htcheck_Vendor_Order_Type_Abs);

                    Vendors_Order_Type = string.Empty;
                    if (dtcheck_Vendor_Order_Type_Abs.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtcheck_Vendor_Order_Type_Abs.Rows.Count; j++)
                        {

                            Vendors_Order_Type = Vendors_Order_Type + dtcheck_Vendor_Order_Type_Abs.Rows[j]["Vendor_Id"].ToString();
                            Vendors_Order_Type += (j < dtcheck_Vendor_Order_Type_Abs.Rows.Count) ? "," : string.Empty;

                        }

                        Hashtable htget_vendor_Client_And_Sub_Client = new Hashtable();
                        DataTable dtget_Vendor_Client_And_Sub_Client = new DataTable();

                        htget_vendor_Client_And_Sub_Client.Add("@Trans", "GET_VENDOR_ON_CLIENT_AND_SUB_CLIENT");
                        htget_vendor_Client_And_Sub_Client.Add("@Client_Id", clientId);
                        htget_vendor_Client_And_Sub_Client.Add("@Sub_Client_Id", subProcessId);
                        htget_vendor_Client_And_Sub_Client.Add("@Vendors_Id", Vendors_Order_Type);
                        dtget_Vendor_Client_And_Sub_Client = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htget_vendor_Client_And_Sub_Client);
                        Vendors_Client_Sub_Client = string.Empty;
                        if (dtget_Vendor_Client_And_Sub_Client.Rows.Count > 0)
                        {
                            //Getting the Vendors Satisfied All the Conditions
                            DataTable dt_Temp_Vendors = new DataTable();
                            dt_Temp_Vendors.Columns.Add("Vendor_Id");
                            dt_Temp_Vendors.Columns.Add("Capcity");
                            dt_Temp_Vendors.Columns.Add("Percentage");

                            for (int ven = 0; ven < dtget_Vendor_Client_And_Sub_Client.Rows.Count; ven++)
                            {
                                //Getting the Vendor Order Capacity
                                Vendor_Id = int.Parse(dtget_Vendor_Client_And_Sub_Client.Rows[ven]["Vendor_Id"].ToString());
                                Hashtable htvenncapacity = new Hashtable();
                                System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                                htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                                htvenncapacity.Add("@Venodor_Id", dtget_Vendor_Client_And_Sub_Client.Rows[ven]["Vendor_Id"].ToString());
                                dtvencapacity = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                                if (dtvencapacity.Rows.Count > 0)
                                {
                                    Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());

                                    Hashtable htetcdate = new Hashtable();
                                    System.Data.DataTable dtetcdate = new DataTable();
                                    htetcdate.Add("@Trans", "GET_DATE");
                                    dtetcdate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);

                                    Vend_date = dtetcdate.Rows[0]["Date"].ToString();
                                    if (Vendor_Order_capacity != 0)
                                    {
                                        //Getting the Vendor Client Wise Percentage
                                        Hashtable htvendor_Percngate = new Hashtable();
                                        System.Data.DataTable dtvendor_percentage = new System.Data.DataTable();
                                        htvendor_Percngate.Add("@Trans", "GET_VENDOR_PERCENTAGE_OF_ORDERS");
                                        htvendor_Percngate.Add("@Venodor_Id", Vendor_Id);
                                        htvendor_Percngate.Add("@Client_Id", clientId);
                                        htvendor_Percngate.Add("@Date", Vend_date);
                                        dtvendor_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendor_Percngate);

                                        if (dtvendor_percentage.Rows.Count > 0)
                                        {

                                            Vendor_Order_Percentage = Convert.ToDecimal(dtvendor_percentage.Rows[0]["Percentage"].ToString());

                                            if (Vendor_Order_Percentage != 0)
                                            {
                                                Hashtable htvendor_Bal_Percngate = new Hashtable();
                                                System.Data.DataTable dtvendor_bal_percentage = new System.Data.DataTable();
                                                htvendor_Bal_Percngate.Add("@Trans", "GET_BALANCE_PERCENTAGE");
                                                htvendor_Bal_Percngate.Add("@Venodor_Id", Vendor_Id);
                                                htvendor_Bal_Percngate.Add("@Client_Id", clientId);
                                                htvendor_Bal_Percngate.Add("@Date", Vend_date);
                                                dtvendor_bal_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvendor_Bal_Percngate);
                                                if (dtvendor_bal_percentage.Rows.Count > 0)
                                                {
                                                    Vendor_Balance_Percentage = Convert.ToDecimal(dtvendor_bal_percentage.Rows[0]["Vendor_Balance_Perntage"].ToString());
                                                }
                                                else
                                                {
                                                    Vendor_Balance_Percentage = 0;
                                                }

                                                Total_Vendor_Balance_Percentage = Vendor_Order_Percentage + Vendor_Balance_Percentage;

                                                Hashtable htVendor_No_Of_Order_Assigned = new Hashtable();
                                                System.Data.DataTable dtVendor_No_Of_Order_Assigned = new System.Data.DataTable();
                                                htVendor_No_Of_Order_Assigned.Add("@Trans", "COUNT_NO_OF_ORDER_ASSIGNED_TO_VENDOR_DATE_WISE");
                                                htVendor_No_Of_Order_Assigned.Add("@Venodor_Id", Vendor_Id);
                                                htVendor_No_Of_Order_Assigned.Add("@Date", Vend_date);

                                                dtVendor_No_Of_Order_Assigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htVendor_No_Of_Order_Assigned);

                                                if (dtVendor_No_Of_Order_Assigned.Rows.Count > 0)
                                                {

                                                    No_Of_Order_Assignd_for_Vendor = int.Parse(dtVendor_No_Of_Order_Assigned.Rows[0]["count"].ToString());
                                                }
                                                else
                                                {

                                                    No_Of_Order_Assignd_for_Vendor = 0;
                                                }

                                                Hashtable htcheck_Percentage = new Hashtable();
                                                DataTable dtcheck_percentage = new DataTable();

                                                htcheck_Percentage.Add("@Trans", "CEHCK");
                                                htcheck_Percentage.Add("@Client_Id", clientId);
                                                htcheck_Percentage.Add("@Venodor_Id", Vendor_Id);
                                                htcheck_Percentage.Add("@Date", Vend_date);
                                                dtcheck_percentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htcheck_Percentage);

                                                int Check_Count;

                                                if (dtcheck_percentage.Rows.Count > 0)
                                                {
                                                    Check_Count = int.Parse(dtcheck_percentage.Rows[0]["count"].ToString());
                                                }
                                                else
                                                {
                                                    Check_Count = 0;
                                                }

                                                if (No_Of_Order_Assignd_for_Vendor < Vendor_Order_capacity)
                                                {
                                                    Assigning_To_Vendor = 1;
                                                    if (Check_Count == 0)
                                                    {
                                                        Hashtable ht_Insert_Temp = new Hashtable();
                                                        DataTable dt_Insert_Temp = new DataTable();

                                                        ht_Insert_Temp.Add("@Trans", "INSERT_TEMP_VENDOR_ORDER_CAPACITY");
                                                        ht_Insert_Temp.Add("@Venodor_Id", Vendor_Id);
                                                        ht_Insert_Temp.Add("@Client_Id", clientId);
                                                        ht_Insert_Temp.Add("@Vendor_Capcity", Vendor_Order_capacity);
                                                        ht_Insert_Temp.Add("@Vendor_Balance_Perntage", Total_Vendor_Balance_Percentage);
                                                        ht_Insert_Temp.Add("@No_Of_Order_Assigned", No_Of_Order_Assignd_for_Vendor);
                                                        dt_Insert_Temp = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Insert_Temp);
                                                    }
                                                    else
                                                    {

                                                        Hashtable ht_Update_Perentage = new Hashtable();
                                                        DataTable dt_Update_Perentage = new DataTable();

                                                        ht_Update_Perentage.Add("@Trans", "UPDATE_PERCENTAGE");
                                                        ht_Update_Perentage.Add("@Venodor_Id", Vendor_Id);
                                                        ht_Update_Perentage.Add("@Date", Vend_date);
                                                        ht_Update_Perentage.Add("@Client_Id", clientId);
                                                        ht_Update_Perentage.Add("@Vendor_Capcity", Vendor_Order_capacity);
                                                        ht_Update_Perentage.Add("@Vendor_Balance_Perntage", Total_Vendor_Balance_Percentage);
                                                        ht_Update_Perentage.Add("@No_Of_Order_Assigned", No_Of_Order_Assignd_for_Vendor);
                                                        dt_Update_Perentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Update_Perentage);

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }


                            if (Assigning_To_Vendor == 1)
                            {
                                Hashtable ht_Get_Max_Vendor = new Hashtable();
                                DataTable dt_Get_Max_Vendor = new DataTable();
                                ht_Get_Max_Vendor.Add("@Trans", "GET_MAX_VENDOR_BALANCE_PERCENTAGE");
                                ht_Get_Max_Vendor.Add("@Client_Id", clientId);
                                ht_Get_Max_Vendor.Add("@Date", Vend_date);
                                dt_Get_Max_Vendor = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Get_Max_Vendor);

                                if (dt_Get_Max_Vendor.Rows.Count > 0)
                                {
                                    Vendor_Id_For_Assign = int.Parse(dt_Get_Max_Vendor.Rows[0]["Vendor_Id"].ToString());


                                    if (dt_Get_Max_Vendor.Rows.Count > 0)
                                    {
                                        Vendor_Order_Allocation_Count = 1;

                                        Hashtable htVendor_No_Of_Order_Assigned = new Hashtable();
                                        System.Data.DataTable dtVendor_No_Of_Order_Assigned = new System.Data.DataTable();
                                        htVendor_No_Of_Order_Assigned.Add("@Trans", "COUNT_NO_OF_ORDER_ASSIGNED_TO_VENDOR_DATE_WISE");
                                        htVendor_No_Of_Order_Assigned.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                        htVendor_No_Of_Order_Assigned.Add("@Date", Vend_date);

                                        dtVendor_No_Of_Order_Assigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htVendor_No_Of_Order_Assigned);

                                        if (dtVendor_No_Of_Order_Assigned.Rows.Count > 0)
                                        {

                                            No_Of_Order_Assignd_for_Vendor = int.Parse(dtVendor_No_Of_Order_Assigned.Rows[0]["count"].ToString());
                                        }
                                        else { No_Of_Order_Assignd_for_Vendor = 0; }

                                        Hashtable htvenncapacity = new Hashtable();
                                        System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                                        htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                                        htvenncapacity.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                        dtvencapacity = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                                        if (dtvencapacity.Rows.Count > 0)
                                        {
                                            Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());
                                        }

                                        Total_Vendor_Alloacated_Percentage = Total_Vendor_Balance_Percentage - Vendor_Order_Percentage;

                                        Hashtable htetcdate = new Hashtable();
                                        System.Data.DataTable dtetcdate = new System.Data.DataTable();
                                        htetcdate.Add("@Trans", "GET_DATE");
                                        dtetcdate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);

                                        Vend_date = dtetcdate.Rows[0]["Date"].ToString();

                                        if (No_Of_Order_Assignd_for_Vendor <= Vendor_Order_capacity)
                                        {
                                            Hashtable htCheckOrderAssigned = new Hashtable();
                                            System.Data.DataTable dtcheckorderassigned = new System.Data.DataTable();

                                            htCheckOrderAssigned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
                                            htCheckOrderAssigned.Add("@Order_Id", EnteredOrderId);
                                            dtcheckorderassigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htCheckOrderAssigned);

                                            int CheckCount = int.Parse(dtcheckorderassigned.Rows[0]["count"].ToString());
                                            if (CheckCount <= 0)
                                            {

                                                Hashtable htupdatestatus = new Hashtable();
                                                System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                                                htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                                                htupdatestatus.Add("@Order_Status", 20);
                                                htupdatestatus.Add("@Modified_By", userid);
                                                htupdatestatus.Add("@Order_ID", EnteredOrderId);
                                                dtupdatestatus = dataaccess.ExecuteSP("Sp_Order", htupdatestatus);


                                                Hashtable htupdateprogress = new Hashtable();
                                                System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                                                htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdateprogress.Add("@Order_Progress", 6);
                                                htupdateprogress.Add("@Modified_By", userid);
                                                htupdateprogress.Add("@Order_ID", EnteredOrderId);
                                                dtupdateprogress = dataaccess.ExecuteSP("Sp_Order", htupdateprogress);


                                                Hashtable htinsert = new Hashtable();
                                                System.Data.DataTable dtinert = new System.Data.DataTable();

                                                htinsert.Add("@Trans", "INSERT");
                                                htinsert.Add("@Order_Id", EnteredOrderId);
                                                htinsert.Add("@Order_Task_Id", 2);
                                                htinsert.Add("@Order_Status_Id", 13);
                                                htinsert.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                                htinsert.Add("@Assigned_Date_Time", dtetcdate.Rows[0]["Date_time"]);
                                                htinsert.Add("@Assigned_By", userid);
                                                htinsert.Add("@Inserted_By", userid);
                                                htinsert.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                                htinsert.Add("@Status", "True");
                                                dtinert = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htinsert);

                                                Hashtable htinsertstatus = new Hashtable();
                                                System.Data.DataTable dtinsertstatus = new System.Data.DataTable();
                                                htinsertstatus.Add("@Trans", "INSERT");
                                                htinsertstatus.Add("@Vendor_Id", Vendor_Id_For_Assign);
                                                htinsertstatus.Add("@Order_Id", EnteredOrderId);
                                                htinsertstatus.Add("@Order_Task", 2);
                                                htinsertstatus.Add("@Order_Status", 13);
                                                htinsertstatus.Add("@Assigen_Date", dtetcdate.Rows[0]["Date"]);
                                                htinsertstatus.Add("@Inserted_By", userid);
                                                htinsertstatus.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                                htinsertstatus.Add("@Status", "True");
                                                dtinsertstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htinsertstatus);

                                                Hashtable ht_Order_History = new Hashtable();
                                                System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", EnteredOrderId);
                                                ht_Order_History.Add("@User_Id", userid);
                                                ht_Order_History.Add("@Status_Id", 20);
                                                ht_Order_History.Add("@Progress_Id", 6);
                                                ht_Order_History.Add("@Assigned_By", userid);
                                                ht_Order_History.Add("@Modification_Type", "Vendor Order Auto Assigned");
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);



                                                Hashtable ht_Update_Perentage = new Hashtable();
                                                DataTable dt_Update_Perentage = new DataTable();

                                                ht_Update_Perentage.Add("@Trans", "UPDATE_PERCENTAGE");
                                                ht_Update_Perentage.Add("@Date", Vend_date);
                                                ht_Update_Perentage.Add("@Venodor_Id", Vendor_Id_For_Assign);
                                                ht_Update_Perentage.Add("@Client_Id", clientId);
                                                ht_Update_Perentage.Add("@Vendor_Capcity", Vendor_Order_capacity);
                                                ht_Update_Perentage.Add("@Vendor_Balance_Perntage", Total_Vendor_Alloacated_Percentage);
                                                ht_Update_Perentage.Add("@No_Of_Order_Assigned", No_Of_Order_Assignd_for_Vendor);
                                                dt_Update_Perentage = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", ht_Update_Perentage);


                                                Assigning_To_Vendor = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void GetMaximumOrderNumber()
        {
            Hashtable htmax = new Hashtable();
            DataTable dtmax = new System.Data.DataTable();
            htmax.Add("@Trans", "MAX_ORDER_NO");
            dtmax = dataaccess.ExecuteSP("Sp_Order", htmax);
            if (dtmax.Rows.Count > 0)
            {
                MAX_ORDER_NUMBER = "DRN" + "-" + dtmax.Rows[0]["ORDER_NUMBER"].ToString();
            }
        }

        private void grd_order_View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e.RowHandle >= 0)
            {
                string error = view.GetRowCellValue(e.RowHandle, view.Columns["Error_Status"]).ToString();
                if (!String.IsNullOrEmpty(error))
                {
                    if (error == "Duplicate")
                    {
                        e.Appearance.BackColor = Color.FromArgb(250, 50, 26);
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (error == "Existing")
                    {
                        //e.Appearance.ForeColor = Color.LightYellow;
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 192);
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.FromArgb(250, 106, 4);
                        e.Appearance.ForeColor = Color.Black;
                    }
                }

            }
        }

        private void btn_Sample_Excel_Fromat_Click(object sender, EventArgs e)
        {
            try
            {
                string temppath = @"C:\OMS\Order Import New.xlsx";
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Order Import New.xlsx", temppath, true);
                Process.Start(temppath);
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Error in opening file");
            }
        }

        private void grd_order_View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}