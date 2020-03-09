using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace Ordermanagement_01.Reports
{
    public partial class Record : XtraForm
    {
        private int order_ID;
        private string production_Date;
        private int user_id;
        private string user_Role_Id;
        private DataTable dtOrders;
        private readonly DataAccess dataaccess;
        private Func<int> userId;
        private int empJobRoleId;
        private decimal empSalary;
        private int empSalCatId;
        private decimal empCatValue;
        private int Max_Time_Id;
        private int Differnce_Time;
        private int External_Client_Order_Id;
        private int External_Client_Order_Task_Id;
        private int ClientId;
        private int userIdToAllocate;
        private int orderStatus;
        private int Eff_Order_Source_Type_Id;
        private int Eff_Order_User_Effecncy;
        private int Eff_Order_Task_Id;
        private string Order_Number;
        private int Eff_Client_Id;
        private int Eff_Sub_Process_Id;
        private int Eff_County_Id;
        private int Eff_State_Id;
        private int Eff_Order_Type_Abs_Id;
        private int orderId;
        private int Sub_Process_Id;
        private int Order_Task_Id;
        private int Order_Satatus_Id;
        private int Abs_Staus_Id;
        private int Abs_Progress_Id;
        private int error_Count_1;
        private int Error_Count;
        private int Record_Count;
        private int error_Count_2;
        private int error_Count_3;
        private int error_Count_4;
        private int error_Count_5;
        private int error_Count_6;
        private StyleFormatCondition styleFormatBlue, styleFormatRed;
        public int Emp_Eff_Allocated_Order_Count { get; private set; }
        private string operation;
        private string fromDate;
        private string toDate;
        private string client;
        private int orderStatusId;
        private string date;

        public Record(DataTable dt, int order_ID, int user_id, string user_Role_Id, string production_Date, string client, string operation, string fromDate, string toDate, int orderStatusId, string date)
        {
            InitializeComponent();
            dataaccess = new DataAccess();
            this.order_ID = order_ID;
            this.user_id = user_id;
            this.user_Role_Id = user_Role_Id;
            this.production_Date = production_Date;
            this.client = client;
            this.operation = operation;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.date = date;
            dtOrders = dt;
            styleFormatBlue = new StyleFormatCondition();
            styleFormatBlue.Appearance.BackColor = Color.Blue;
            styleFormatBlue.Appearance.Options.UseBackColor = true;

            styleFormatRed = new StyleFormatCondition();
            styleFormatRed.Appearance.BackColor = Color.Blue;
            styleFormatRed.Appearance.Options.UseBackColor = true;
        }
        private void Record_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                gridControlOrders.DataSource = dtOrders;
                gridControlOrders.Visible = true;
                WindowState = FormWindowState.Maximized;
                gridViewOrders.IndicatorWidth = 50;
                BindUsers();
                BindTasks();
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

        private void BindTasks()
        {
            try
            {
                var htTasks = new Hashtable {
                    { "@Trans", "BIND_FOR_ORDER_STATUS_VIEW" }
                };
                var dtTasks = dataaccess.ExecuteSP("Sp_Order_Status", htTasks);
                DataRow dr = dtTasks.NewRow();
                dr[0] = 0;
                dr[1] = "SELECT";
                dtTasks.Rows.InsertAt(dr, 0);
                lookUpEditTask.Properties.DataSource = dtTasks;
                lookUpEditTask.Properties.DisplayMember = "Order_Status";
                lookUpEditTask.Properties.ValueMember = "Order_Status_ID";
                lookUpEditTask.Properties.Columns.Add(new LookUpColumnInfo("Order_Status", "Task"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BindOrdersByOperation()
        {
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            try
            {
                gridControlOrders.DataSource = null;



                string spName = "";
                var httargetorder = new Hashtable();

                httargetorder.Add("@Trans", operation);
                httargetorder.Add("@Client_Number", client);    //Clint
                                                                //httargetorder.Add("@Sub_Client", Sub_Process_ID);  // Sub_Process_ID 
                httargetorder.Add("@Order_Status", orderStatusId);
                httargetorder.Add("@Fromdate", fromDate);
                httargetorder.Add("@Todate", toDate);
                httargetorder.Add("@date", date);
                // httargetorder.Add("@date", s_Date);
                if (operation == "AGENT_OPEN_ORDER_DETAILS" || operation == "AGENT_OPEN_ORDER_ROW_TOTAL_CLIENT_DATE_WISE" || operation == "AGENT_OPEN_ORDER_COLUMN_GRANT_TOTAL_WISE" ||
                     operation == "AGENT_OPEN_ORDER_COLUMN_GRANT_TOTAL_WISE" || operation == "AGENT_OPEN_ORDER_ALL_CLIENT_AND_ORDER_STATUS_WISE" || operation == "AGENT_OPEN_ORDER_CLIENT_AND_DATE_WISE" ||
                     operation == "AGENT_OPEN_ORDER_CLIENT_AND_ALL_TASK_WISE" || operation == "AGENT_OPEN_ORDER_DATE_WISE" || operation == "AGENT_OPEN_ORDER_CLIENT_AND_ORDER_STATUS_WISE")
                {
                    spName = "Sp_Daily_Status_Report_Open";
                }
                else if (operation == "AGENT_PENDING_ORDER_DETAILS" || operation == "AGENT_OPEN_ORDER_ROW_TOTAL_CLIENT_DATE_WISE" ||
                    operation == "AGENT_PENDING_ORDER_CLIENT_AND_STATUS" || operation == "AGENT_PENDING_ORDER_ALL_CLIENT_STATUS_WISE" ||
                    operation == "AGENT_PENDING_ORDER_CLIENT_DATE_WISE" || operation == "AGENT_PENDING_ORDER_CLIENT_AND_ALL_STATUS_WISE"
                    || operation == "AGING_PENDING_ORDER_DATE_WISE")
                {
                    spName = "Sp_Daily_Status_Report_Pending";
                }
                else
                {
                    spName = "Sp_Daily_Status_Report";
                }
                var dtOrders = dataaccess.ExecuteSP(spName, httargetorder);

                if (dtOrders.Rows.Count > 0)
                {
                    gridControlOrders.DataSource = dtOrders;
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void BindUsers()
        {
            try
            {
                lookUpEditUsers.Properties.DataSource = null;
                var ht = new Hashtable {
                    { "@Trans", "SELECT" }
                };
                var dtUser = dataaccess.ExecuteSP("Sp_User", ht);
                DataRow dr = dtUser.NewRow();
                dr[0] = 0;
                dr[4] = "SELECT";
                dtUser.Rows.InsertAt(dr, 0);
                lookUpEditUsers.Properties.DataSource = dtUser;
                lookUpEditUsers.Properties.DisplayMember = "User_Name";
                lookUpEditUsers.Properties.ValueMember = "User_id";
                lookUpEditUsers.Properties.Columns.Add(new LookUpColumnInfo("User_Name", "Username"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void gridView5_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                DataRow row = gridViewOrders.GetDataRow(e.RowHandle);
                int Order_ID = int.Parse(row["Order_ID"].ToString());
                Order_Entry orderentry = new Order_Entry(Order_ID, user_id, user_Role_Id, production_Date);
                orderentry.Show();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridViewOrders.OptionsView.ShowFooter = false;
            gridViewOrders.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.None;
            try
            {
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + "Orders Record " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                options.AllowHyperLinks = DevExpress.Utils.DefaultBoolean.False;
                options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                options.TextExportMode = TextExportMode.Value;
                options.IgnoreErrors = XlIgnoreErrors.NumberStoredAsText;
                gridControlOrders.ExportToXlsx(Path1, options);
                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("something went wrong");
            }
            finally
            {
                gridViewOrders.OptionsView.ShowFooter = true;
                gridViewOrders.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            }

        }

        private void gridView5_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }


        private void btnReAllocate_Click(object sender, EventArgs e)
        {
            if (ValidateSelection())
            {
                userIdToAllocate = Convert.ToInt32(lookUpEditUsers.EditValue);
                orderStatus = Convert.ToInt32(lookUpEditTask.EditValue);

                Get_Employee_Details();
                Get_Effecncy_Category();

                if (Validate() != false)
                {
                    if (XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                    {
                        try
                        {
                            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                            List<int> gridViewSelectedRowHandles = gridViewOrders.GetSelectedRows().ToList();

                            for (int i = 0; i < gridViewSelectedRowHandles.Count; i++)
                            {
                                DataRow row = gridViewOrders.GetDataRow(gridViewSelectedRowHandles[i]);
                                Order_Number = row["Client_Order_Number"].ToString();
                                Eff_Client_Id = int.Parse(row["Client_Id"].ToString());
                                Eff_Sub_Process_Id = int.Parse(row["Subprocess_Id"].ToString());
                                Eff_State_Id = int.Parse(row["State_ID"].ToString());
                                Eff_County_Id = int.Parse(row["County_ID"].ToString());
                                Eff_Order_Type_Abs_Id = int.Parse(row["OrderType_Abs_Id"].ToString());

                                Hashtable htselect_Orderid = new Hashtable();
                                DataTable dtselect_Orderid = new System.Data.DataTable();
                                htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
                                htselect_Orderid.Add("@Client_Order_Number", Order_Number);
                                dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);

                                orderId = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                                ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                                Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                                Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                                Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                                Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                                Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                                Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                                DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                                htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                                htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", orderId);
                                dtget_User_Order_Last_Time_Updated = dataaccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

                                if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
                                {
                                    Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());
                                }
                                else
                                {
                                    Max_Time_Id = 0;
                                }

                                if (Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Abs_Staus_Id != 17)
                                {
                                    Get_Employee_Details();
                                    Get_Effecncy_Category();
                                    if (Max_Time_Id != 0)
                                    {
                                        Hashtable htget_User_Order_Differnce_Time = new Hashtable();
                                        DataTable dtget_User_Order_Differnce_Time = new DataTable();
                                        htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                                        htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
                                        dtget_User_Order_Differnce_Time = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

                                        if (dtget_User_Order_Differnce_Time.Rows.Count > 0)
                                        {
                                            Differnce_Time = int.Parse(dtget_User_Order_Differnce_Time.Rows[0]["Diff"].ToString());
                                        }
                                        else
                                        {
                                            Differnce_Time = 0;
                                        }
                                        //htget_User_Order_Differnce_Time.Add("","");
                                    }

                                    Get_Order_Source_Type_For_Effeciency();
                                    //========= Effecincy Cal End=========================================
                                }
                                //========= Effecincy Cal End=========================================

                                // This is for Tax Order Status check
                                int Check_Order_In_Tax = 0;
                                int Tax_User_Order_Diff_Time = 0;
                                if (Abs_Staus_Id == 26)
                                {

                                    Hashtable htcheck_Order_In_tax = new Hashtable();
                                    DataTable dt_check_Order_In_tax = new DataTable();

                                    htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
                                    htcheck_Order_In_tax.Add("@Order_Id", orderId);
                                    dt_check_Order_In_tax = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

                                    if (dt_check_Order_In_tax.Rows.Count > 0)
                                    {

                                        Check_Order_In_Tax = int.Parse(dt_check_Order_In_tax.Rows[0]["count"].ToString());
                                    }
                                    else
                                    {
                                        Check_Order_In_Tax = 0;
                                    }
                                    if (Check_Order_In_Tax > 0)
                                    {
                                        Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                                        DataTable dt_Get_Tax_Diff_Time = new DataTable();

                                        ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                                        ht_Get_Tax_Diff_Time.Add("@Order_Id", orderId);
                                        dt_Get_Tax_Diff_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

                                        if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
                                        {
                                            Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());
                                        }
                                        else
                                        {
                                            Tax_User_Order_Diff_Time = 0;
                                        }
                                    }
                                }
                                if (Tax_User_Order_Diff_Time < 30 && Tax_User_Order_Diff_Time != 0)
                                {
                                    //This Is For Highligghting the Error based on the input                                   
                                    error_Count_1 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                                else
                                {
                                    // Updating Tax Order Status
                                    // Updating Tax Order Status
                                    if (Abs_Staus_Id == 26)
                                    {
                                        if (Check_Order_In_Tax > 0)
                                        {
                                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                            {
                                                // Cancelling the Order in Tax
                                                Hashtable htupdateOrderTaxStatus = new Hashtable();
                                                DataTable dtupdateOrderTaxStatus = new DataTable();
                                                Hashtable htupdateTaxStatus = new Hashtable();
                                                DataTable dtupdateTaxStatus = new DataTable();

                                                htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                                htupdateTaxStatus.Add("@Tax_Status", 4);
                                                htupdateTaxStatus.Add("@Modified_By", userId);
                                                htupdateTaxStatus.Add("@Order_Id", orderId);
                                                dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                                if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                                {

                                                    Hashtable htupassin = new Hashtable();
                                                    DataTable dtupassign = new DataTable();
                                                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                    htupassin.Add("@Order_Id", orderId);

                                                    dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                                    Hashtable htinsert_Assign = new Hashtable();
                                                    DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                                    htinsert_Assign.Add("@Trans", "INSERT");
                                                    htinsert_Assign.Add("@Order_Id", orderId);
                                                    htinsert_Assign.Add("@User_Id", userIdToAllocate);
                                                    htinsert_Assign.Add("@Order_Status_Id", orderStatus);
                                                    htinsert_Assign.Add("@Order_Progress_Id", 6);
                                                    htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                                    htinsert_Assign.Add("@Assigned_By", userIdToAllocate);
                                                    htinsert_Assign.Add("@Modified_By", userIdToAllocate);
                                                    htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                                    htinsert_Assign.Add("@status", "True");
                                                    htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                                    dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);

                                                    //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                                                    Hashtable htinsertrec = new Hashtable();
                                                    DataTable dtinsertrec = new System.Data.DataTable();
                                                    DateTime date = new DateTime();
                                                    date = DateTime.Now;
                                                    string dateeval = date.ToString("dd/MM/yyyy");
                                                    string time = date.ToString("hh:mm tt");

                                                    Hashtable htorderStatus = new Hashtable();
                                                    DataTable dtorderStatus = new DataTable();
                                                    htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                    htorderStatus.Add("@Order_ID", orderId);
                                                    htorderStatus.Add("@Order_Status", orderStatus);
                                                    htorderStatus.Add("@Modified_By", userId);
                                                    htorderStatus.Add("@Modified_Date", date);
                                                    dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                    Hashtable htupdate_Prog = new Hashtable();
                                                    DataTable dtupdate_Prog = new System.Data.DataTable();
                                                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                    htupdate_Prog.Add("@Order_ID", orderId);
                                                    htupdate_Prog.Add("@Order_Progress", 6);
                                                    htupdate_Prog.Add("@Modified_By", userId);
                                                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                    //OrderHistory
                                                    Hashtable ht_Order_History = new Hashtable();
                                                    DataTable dt_Order_History = new DataTable();
                                                    ht_Order_History.Add("@Trans", "INSERT");
                                                    ht_Order_History.Add("@Order_Id", orderId);
                                                    ht_Order_History.Add("@User_Id", userIdToAllocate);
                                                    ht_Order_History.Add("@Status_Id", orderStatus);
                                                    ht_Order_History.Add("@Progress_Id", 6);
                                                    ht_Order_History.Add("@Work_Type", 1);
                                                    ht_Order_History.Add("@Assigned_By", userId);
                                                    ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                    //OrderHistory
                                                    Hashtable ht_Order_History1 = new Hashtable();
                                                    DataTable dt_Order_History1 = new DataTable();
                                                    ht_Order_History1.Add("@Trans", "INSERT");
                                                    ht_Order_History1.Add("@Order_Id", orderId);
                                                    ht_Order_History1.Add("@User_Id", userIdToAllocate);
                                                    ht_Order_History1.Add("@Status_Id", orderStatus);
                                                    ht_Order_History1.Add("@Progress_Id", 6);
                                                    ht_Order_History1.Add("@Work_Type", 1);
                                                    ht_Order_History1.Add("@Assigned_By", userId);
                                                    ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                                    dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                                    //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                                    Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                    DataTable dt_Order_InTitleLogy = new DataTable();
                                                    htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                    htCheck_Order_InTitlelogy.Add("@Order_ID", orderId);
                                                    dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                    if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                    {

                                                        External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                        External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                                        // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                                        if (External_Client_Order_Task_Id != 18)
                                                        {
                                                            if (ClientId == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                            {
                                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                                DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                            }
                                                            else
                                                            {
                                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                                DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                            }
                                                        }
                                                    }

                                                    //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                                                    htinsertrec.Clear();
                                                    dtinsertrec.Clear();

                                                    htorderStatus.Clear();
                                                    dtorderStatus.Clear();
                                                    htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                    htorderStatus.Add("@Order_ID", orderId);
                                                    htorderStatus.Add("@Order_Status", Order_Satatus_Id);
                                                    htorderStatus.Add("@Modified_By", userIdToAllocate);
                                                    htorderStatus.Add("@Modified_Date", date);
                                                    dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                    htupdate_Prog.Clear();
                                                    dtupdate_Prog.Clear();
                                                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                    htupdate_Prog.Add("@Order_ID", orderId);
                                                    htupdate_Prog.Add("@Order_Progress", 6);
                                                    htupdate_Prog.Add("@Modified_By", userIdToAllocate);
                                                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);
                                                    Record_Count = 1;
                                                    Error_Count = 0;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                                {
                                    error_Count_2 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }

                                else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                                {
                                    if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 60)
                                    {
                                        error_Count_3 = 1;
                                        Error_Count = 1;
                                        Record_Count = 0;
                                    }
                                }
                                else if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20)
                                {
                                    if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                    {
                                        if (Differnce_Time > 5 || Differnce_Time == 0)
                                        {
                                            Hashtable htupassin = new Hashtable();
                                            DataTable dtupassign = new DataTable();

                                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                                            htupassin.Add("@Order_Id", orderId);
                                            dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                            Hashtable htinsert_Assign = new Hashtable();
                                            DataTable dtinsertrec_Assign = new DataTable();
                                            htinsert_Assign.Add("@Trans", "INSERT");
                                            htinsert_Assign.Add("@Order_Id", orderId);
                                            htinsert_Assign.Add("@User_Id", userIdToAllocate);
                                            htinsert_Assign.Add("@Order_Status_Id", orderStatus);
                                            htinsert_Assign.Add("@Order_Progress_Id", 6);
                                            htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                            htinsert_Assign.Add("@Assigned_By", userId);
                                            htinsert_Assign.Add("@Modified_By", userId);
                                            htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                            htinsert_Assign.Add("@status", "True");
                                            htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                            dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);


                                            Hashtable htinsertrec = new Hashtable();
                                            DataTable dtinsertrec = new DataTable();
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            Hashtable htorderStatus = new Hashtable();
                                            DataTable dtorderStatus = new DataTable();
                                            htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                            htorderStatus.Add("@Order_ID", orderId);
                                            htorderStatus.Add("@Order_Status", orderStatus);
                                            htorderStatus.Add("@Modified_By", userId);
                                            htorderStatus.Add("@Modified_Date", date);
                                            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                            Hashtable htupdate_Prog = new Hashtable();
                                            DataTable dtupdate_Prog = new DataTable();
                                            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                            htupdate_Prog.Add("@Order_ID", orderId);
                                            htupdate_Prog.Add("@Order_Progress", 6);
                                            htupdate_Prog.Add("@Modified_By", userId);
                                            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            DataTable dt_Order_History = new DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", orderId);
                                            ht_Order_History.Add("@User_Id", userIdToAllocate);
                                            ht_Order_History.Add("@Status_Id", orderStatus);
                                            ht_Order_History.Add("@Progress_Id", 6);
                                            ht_Order_History.Add("@Work_Type", 1);
                                            ht_Order_History.Add("@Assigned_By", userId);
                                            ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                            //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                            DataTable dt_Order_InTitleLogy = new DataTable();
                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                            htCheck_Order_InTitlelogy.Add("@Order_ID", orderId);
                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                            {

                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());
                                                // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                                if (External_Client_Order_Task_Id != 18)
                                                {
                                                    if (ClientId == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                    }
                                                    else
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                    }
                                                }
                                            }

                                            htinsertrec.Clear();
                                            dtinsertrec.Clear();

                                            htorderStatus.Clear();
                                            dtorderStatus.Clear();
                                            htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                            htorderStatus.Add("@Order_ID", orderId);
                                            htorderStatus.Add("@Order_Status", orderStatus);
                                            htorderStatus.Add("@Modified_By", userId);
                                            htorderStatus.Add("@Modified_Date", date);
                                            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                            htupdate_Prog.Clear();
                                            dtupdate_Prog.Clear();
                                            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                            htupdate_Prog.Add("@Order_ID", orderId);
                                            htupdate_Prog.Add("@Order_Progress", 6);
                                            htupdate_Prog.Add("@Modified_By", userId);
                                            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                            // txt_Order_number_TextChanged();                                         
                                            Record_Count = 1;
                                            Error_Count = 0;

                                        }
                                        else
                                        {
                                            error_Count_4 = 1;
                                            Error_Count = 1;
                                            Record_Count = 0;
                                        }
                                    }
                                    else
                                    {
                                        error_Count_5 = 1;
                                        Error_Count = 1;
                                        Record_Count = 0;
                                    }
                                }
                                else
                                {

                                    error_Count_6 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }// for loop close
                            if (Record_Count > 0)
                            {
                                SplashScreenManager.CloseForm(false);
                                XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order ReAllocated Sucessfully.", "Success", MessageBoxButtons.OK);
                                //BindOrdersByOperation();
                                Clear();
                            }
                            if (Error_Count > 0)
                            {
                                SplashScreenManager.CloseForm(false);
                            }
                        }
                        catch (Exception ex)
                        {
                            SplashScreenManager.CloseForm(false);
                            MessageBox.Show("Error Occured Please Check With Administrator");
                        }
                        finally
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                }
            }
        }

        private void Clear()
        {
            lookUpEditUsers.EditValue = 0;
            lookUpEditTask.EditValue = 0;
            gridViewOrders.ClearSelection();
        }

        private void Get_Order_Source_Type_For_Effeciency()
        {

            // Check for the Search Task
            //Check its Plant  or Technical For Searcher


            Emp_Eff_Allocated_Order_Count = 0; Eff_Order_Source_Type_Id = 0;
            Eff_Order_User_Effecncy = 0;

            if (Eff_Order_Task_Id == 2 || Eff_Order_Task_Id == 3)
            {
                Hashtable htcheckplant_Technical = new Hashtable();
                DataTable dtcheckplant_Technical = new DataTable();
                htcheckplant_Technical.Add("@Trans", "GET_ORDER_SOURCE_TYPE_ID");
                htcheckplant_Technical.Add("@State_Id", Eff_State_Id);
                htcheckplant_Technical.Add("@County", Eff_County_Id);
                dtcheckplant_Technical = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheckplant_Technical);

                if (dtcheckplant_Technical.Rows.Count > 0)
                {
                    Eff_Order_Source_Type_Id = int.Parse(dtcheckplant_Technical.Rows[0]["Order_Source_Type_ID"].ToString());
                }
                else
                {
                    Eff_Order_Source_Type_Id = 0;
                }
                // If its an Technical or Plant
                if (Eff_Order_Source_Type_Id != 0)
                {
                    //Get the Allocated Count in the Efffecincy Matrix
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);
                    htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }
                }
                else if (Emp_Eff_Allocated_Order_Count != 0 && Eff_Order_Source_Type_Id != 0)
                {
                    //Get the Allocated Count in the Efffecincy Matrix for Online
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                    htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }
                }
                else
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                        Eff_Order_User_Effecncy = 0;
                    }
                }
            }
            else if (Eff_Order_Task_Id == 4 || Eff_Order_Task_Id == 7)
            {
                // this is for Deed Chain Order and Typing 
                Hashtable htcheck_Deed_Chain = new Hashtable();
                DataTable dtcheck_Deed_Chain = new DataTable();
                htcheck_Deed_Chain.Add("@Trans", "GET_ORDER_SOURCE_TYPE_ID_BY_SUB_CLIENT");
                htcheck_Deed_Chain.Add("@Subprocess_Id", Eff_Sub_Process_Id);
                dtcheck_Deed_Chain = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheck_Deed_Chain);

                if (dtcheck_Deed_Chain.Rows.Count > 0)
                {
                    Eff_Order_Source_Type_Id = int.Parse(dtcheck_Deed_Chain.Rows[0]["Order_Source_Type_ID"].ToString());
                }
                else
                {
                    Eff_Order_Source_Type_Id = 0;
                }
                if (Eff_Order_Source_Type_Id != 0)
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);
                    htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    }
                    else
                    {
                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }
                }
                else if (Eff_Order_Source_Type_Id != 0 && Emp_Eff_Allocated_Order_Count != 0)
                {

                    //Get the Allocated Count in the Efffecincy Matrix for Online
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }

                    Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                }

                else
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                        Eff_Order_User_Effecncy = 0;
                    }
                }

            }
            else  // this is for not Search and Typing Qc
            {

                Hashtable htget_Effecicy_Value = new Hashtable();
                DataTable dtget_Effeciency_Value = new DataTable();

                htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                htget_Effecicy_Value.Add("@Category_Id", empSalCatId);
                dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                if (dtget_Effeciency_Value.Rows.Count > 0)
                {
                    Emp_Eff_Allocated_Order_Count = Convert.ToInt32(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                }
                else
                {
                    Emp_Eff_Allocated_Order_Count = 0;
                    Eff_Order_User_Effecncy = 0;
                }
            }
        }

        private void Get_Effecncy_Category()
        {
            if (empJobRoleId != 0 && empSalary != 0)
            {
                Hashtable htget_Category = new Hashtable();
                DataTable dtget_Category = new DataTable();
                if (empJobRoleId == 1)
                {
                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_SEARCHER");
                }
                else if (empJobRoleId == 2)
                {

                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_TYPER");
                }
                htget_Category.Add("@Salary", empSalary);
                htget_Category.Add("@Job_Role_Id", empJobRoleId);
                dtget_Category = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Category);
                if (dtget_Category.Rows.Count > 0)
                {
                    empSalCatId = int.Parse(dtget_Category.Rows[0]["Category_ID"].ToString());
                    empCatValue = decimal.Parse(dtget_Category.Rows[0]["Category_Name"].ToString());
                }
                else
                {
                    empSalCatId = 0;
                    empCatValue = 0;
                }
            }
            else
            {
                XtraMessageBox.Show("Please Setup Employee job Role");
            }
        }

        private void Get_Employee_Details()
        {

            Hashtable htget_empdet = new Hashtable();
            DataTable dtget_empdet = new DataTable();

            htget_empdet.Add("@Trans", "GET_EMP_DETAILS");
            htget_empdet.Add("@User_Id", lookUpEditUsers.EditValue);
            dtget_empdet = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_empdet);
            if (dtget_empdet.Rows.Count > 0)
            {
                if (dtget_empdet.Rows[0]["Job_Role_Id"].ToString() != "" && dtget_empdet.Rows[0]["Job_Role_Id"].ToString() != null)
                {
                    empJobRoleId = int.Parse(dtget_empdet.Rows[0]["Job_Role_Id"].ToString());
                    empSalary = decimal.Parse(dtget_empdet.Rows[0]["Salary"].ToString());
                }
                else
                {
                    empJobRoleId = 0;
                    empSalary = 0;
                }
            }
        }

        private bool ValidateSelection()
        {
            if (Convert.ToInt32(lookUpEditUsers.EditValue) < 1)
            {
                XtraMessageBox.Show("Select User");
                lookUpEditUsers.Focus();
                return false;
            }
            if (Convert.ToInt32(lookUpEditTask.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Task");
                lookUpEditTask.Focus();
                return false;
            }
            if (gridViewOrders.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Select Orders");
                return false;
            }
            return true;
        }

        private void groupControlRecords_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //BindOrdersByOperation();
        }

        private void btnDeAllocate_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditTask.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Task");
                lookUpEditTask.Focus();
                return;
            }
            if (gridViewOrders.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Select Orders");
                return;
            }
            if (XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
            {
                userIdToAllocate = Convert.ToInt32(lookUpEditUsers.EditValue);
                orderStatus = Convert.ToInt32(lookUpEditTask.EditValue);
                try
                {
                    List<int> gridViewSelectedRows = gridViewOrders.GetSelectedRows().ToList();
                    for (int i = 0; i < gridViewSelectedRows.Count; i++)
                    {
                        DataRow row = gridViewOrders.GetDataRow(gridViewSelectedRows[i]);

                        Order_Number = row["Client_Order_Number"].ToString();
                        Eff_Client_Id = int.Parse(row["Client_Id"].ToString());
                        Eff_Sub_Process_Id = int.Parse(row["Subprocess_Id"].ToString());
                        Eff_State_Id = int.Parse(row["State_ID"].ToString());
                        Eff_County_Id = int.Parse(row["County_ID"].ToString());
                        Eff_Order_Type_Abs_Id = int.Parse(row["OrderType_ABS_Id"].ToString());


                        Hashtable htselect_Orderid = new Hashtable();
                        DataTable dtselect_Orderid = new System.Data.DataTable();
                        htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
                        htselect_Orderid.Add("@Client_Order_Number", Order_Number);
                        dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);

                        orderId = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                        ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                        Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                        Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                        Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                        int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                        int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());



                        Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                        DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                        htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                        htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", orderId);
                        dtget_User_Order_Last_Time_Updated = dataaccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

                        if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
                        {
                            Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());
                        }
                        else
                        {
                            Max_Time_Id = 0;
                        }


                        if (Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Abs_Staus_Id != 17)
                        {
                            if (Max_Time_Id != 0)
                            {

                                Hashtable htget_User_Order_Differnce_Time = new Hashtable();
                                DataTable dtget_User_Order_Differnce_Time = new DataTable();
                                htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                                htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
                                dtget_User_Order_Differnce_Time = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

                                if (dtget_User_Order_Differnce_Time.Rows.Count > 0)
                                {
                                    Differnce_Time = int.Parse(dtget_User_Order_Differnce_Time.Rows[0]["Diff"].ToString());

                                }
                                else
                                {
                                    Differnce_Time = 0;

                                }
                            }

                        }

                        // This is for Tax Order Status check
                        int Check_Order_In_Tax = 0;
                        int Tax_User_Order_Diff_Time = 0;
                        if (Abs_Staus_Id == 26)
                        {

                            Hashtable htcheck_Order_In_tax = new Hashtable();
                            DataTable dt_check_Order_In_tax = new DataTable();

                            htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
                            htcheck_Order_In_tax.Add("@Order_Id", orderId);
                            dt_check_Order_In_tax = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

                            if (dt_check_Order_In_tax.Rows.Count > 0)
                            {

                                Check_Order_In_Tax = int.Parse(dt_check_Order_In_tax.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                Check_Order_In_Tax = 0;
                            }

                            if (Check_Order_In_Tax > 0)
                            {

                                Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                                DataTable dt_Get_Tax_Diff_Time = new DataTable();

                                ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                                ht_Get_Tax_Diff_Time.Add("@Order_Id", orderId);
                                dt_Get_Tax_Diff_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

                                if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
                                {

                                    Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());

                                }
                                else
                                {

                                    Tax_User_Order_Diff_Time = 0;
                                }

                            }
                        }
                        if (Tax_User_Order_Diff_Time < 30 && Tax_User_Order_Diff_Time != 0)
                        {
                            //This Is For Highligghting the Error based on the input
                            gridViewOrders.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", "True");
                            error_Count_1 = 1;
                            Error_Count = 1;
                            Record_Count = 0;
                        }
                        else
                        {
                            // Updating Tax Order Status
                            if (Abs_Staus_Id == 26)
                            {
                                if (Check_Order_In_Tax > 0)
                                {

                                    if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                    {

                                        // Cancelling the Order in Tax

                                        Hashtable htupdateOrderTaxStatus = new Hashtable();
                                        DataTable dtupdateOrderTaxStatus = new DataTable();
                                        Hashtable htupdateTaxStatus = new Hashtable();
                                        DataTable dtupdateTaxStatus = new DataTable();

                                        htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                        htupdateTaxStatus.Add("@Tax_Status", 4);
                                        htupdateTaxStatus.Add("@Modified_By", userId);
                                        htupdateTaxStatus.Add("@Order_Id", orderId);
                                        dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                        if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                        {

                                            Hashtable htupassin = new Hashtable();
                                            DataTable dtupassign = new DataTable();
                                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                                            htupassin.Add("@Order_Id", orderId);

                                            dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                            Hashtable htinsertrec = new Hashtable();
                                            DataTable dtinsertrec = new DataTable();
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            Hashtable htorderStatus = new Hashtable();
                                            DataTable dtorderStatus = new DataTable();
                                            htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                            htorderStatus.Add("@Order_ID", orderId);
                                            htorderStatus.Add("@Order_Status", orderStatus);
                                            htorderStatus.Add("@Modified_By", userId);
                                            htorderStatus.Add("@Modified_Date", date);
                                            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                            Hashtable htupdate_Prog = new Hashtable();
                                            DataTable dtupdate_Prog = new DataTable();
                                            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                            htupdate_Prog.Add("@Order_ID", orderId);
                                            htupdate_Prog.Add("@Order_Progress", 8);
                                            htupdate_Prog.Add("@Modified_By", userId);
                                            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            DataTable dt_Order_History = new DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", orderId);
                                            ht_Order_History.Add("@User_Id", userIdToAllocate);
                                            ht_Order_History.Add("@Status_Id", orderStatus);
                                            ht_Order_History.Add("@Progress_Id", 8);
                                            ht_Order_History.Add("@Work_Type", 1);
                                            ht_Order_History.Add("@Assigned_By", userId);
                                            ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                            //OrderHistory
                                            Hashtable ht_Order_History1 = new Hashtable();
                                            DataTable dt_Order_History1 = new DataTable();
                                            ht_Order_History1.Add("@Trans", "INSERT");
                                            ht_Order_History1.Add("@Order_Id", orderId);
                                            ht_Order_History1.Add("@User_Id", userIdToAllocate);
                                            ht_Order_History1.Add("@Status_Id", orderStatus);
                                            ht_Order_History1.Add("@Progress_Id", 8);
                                            ht_Order_History1.Add("@Work_Type", 1);
                                            ht_Order_History1.Add("@Assigned_By", userId);
                                            ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                            dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                            //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                            DataTable dt_Order_InTitleLogy = new DataTable();
                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                            htCheck_Order_InTitlelogy.Add("@Order_ID", orderId);
                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                            {

                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                                // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title

                                                if (External_Client_Order_Task_Id != 18)
                                                {

                                                    if (ClientId == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                    }
                                                    else
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                    }

                                                }

                                            }

                                            htinsertrec.Clear();
                                            dtinsertrec.Clear();

                                            htorderStatus.Clear();
                                            dtorderStatus.Clear();
                                            htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                            htorderStatus.Add("@Order_ID", orderId);
                                            htorderStatus.Add("@Order_Status", orderStatus);
                                            htorderStatus.Add("@Modified_By", userId);
                                            htorderStatus.Add("@Modified_Date", date);
                                            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                            htupdate_Prog.Clear();
                                            dtupdate_Prog.Clear();
                                            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                            htupdate_Prog.Add("@Order_ID", orderId);
                                            htupdate_Prog.Add("@Order_Progress", 8);
                                            htupdate_Prog.Add("@Modified_By", userId);
                                            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);
                                            Record_Count = 1;
                                            Error_Count = 0;

                                        }

                                    }

                                }

                            }
                        }


                        if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                        {
                            error_Count_2 = 1;
                            Error_Count = 1;
                            Record_Count = 0;
                        }

                        else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                        {
                            if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 5)
                            {
                                error_Count_3 = 1;
                                Error_Count = 1;
                                Record_Count = 0;

                            }
                        }
                        else if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20)
                        {
                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                            {

                                if (Differnce_Time > 5 || Differnce_Time == 0)
                                {


                                    Hashtable htupassin = new Hashtable();
                                    DataTable dtupassign = new DataTable();

                                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                                    htupassin.Add("@Order_Id", orderId);
                                    dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                    Hashtable htinsertrec = new Hashtable();
                                    DataTable dtinsertrec = new System.Data.DataTable();
                                    DateTime date = new DateTime();
                                    date = DateTime.Now;
                                    string dateeval = date.ToString("dd/MM/yyyy");
                                    string time = date.ToString("hh:mm tt");

                                    Hashtable htorderStatus = new Hashtable();
                                    DataTable dtorderStatus = new DataTable();
                                    htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                    htorderStatus.Add("@Order_ID", orderId);
                                    htorderStatus.Add("@Order_Status", orderStatus);
                                    htorderStatus.Add("@Modified_By", userId);
                                    htorderStatus.Add("@Modified_Date", date);
                                    dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                    Hashtable htupdate_Prog = new Hashtable();
                                    DataTable dtupdate_Prog = new System.Data.DataTable();
                                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                    htupdate_Prog.Add("@Order_ID", orderId);
                                    htupdate_Prog.Add("@Order_Progress", 8);
                                    htupdate_Prog.Add("@Modified_By", userId);
                                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                    //OrderHistory
                                    Hashtable ht_Order_History = new Hashtable();
                                    DataTable dt_Order_History = new DataTable();
                                    ht_Order_History.Add("@Trans", "INSERT");
                                    ht_Order_History.Add("@Order_Id", orderId);
                                    ht_Order_History.Add("@User_Id", userIdToAllocate);
                                    ht_Order_History.Add("@Status_Id", orderStatus);
                                    ht_Order_History.Add("@Progress_Id", 8);
                                    ht_Order_History.Add("@Work_Type", 1);
                                    ht_Order_History.Add("@Assigned_By", userId);
                                    ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                    //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                    Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                    DataTable dt_Order_InTitleLogy = new DataTable();
                                    htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                    htCheck_Order_InTitlelogy.Add("@Order_ID", orderId);
                                    dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                    if (dt_Order_InTitleLogy.Rows.Count > 0)
                                    {

                                        External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                        External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                        // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                        if (External_Client_Order_Task_Id != 18)
                                        {

                                            if (ClientId == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                            {
                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                            }
                                            else
                                            {
                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", orderStatus);
                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                            }
                                        }
                                    }

                                    htinsertrec.Clear();
                                    dtinsertrec.Clear();

                                    htorderStatus.Clear();
                                    dtorderStatus.Clear();
                                    htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                    htorderStatus.Add("@Order_ID", orderId);
                                    htorderStatus.Add("@Order_Status", orderStatus);
                                    htorderStatus.Add("@Modified_By", userId);
                                    htorderStatus.Add("@Modified_Date", date);
                                    dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                    htupdate_Prog.Clear();
                                    dtupdate_Prog.Clear();
                                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                    htupdate_Prog.Add("@Order_ID", orderId);
                                    htupdate_Prog.Add("@Order_Progress", 8);
                                    htupdate_Prog.Add("@Modified_By", userId);
                                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);



                                    // txt_Order_number_TextChanged();
                                    Record_Count = 1;
                                    Error_Count = 0;
                                }
                                else
                                {

                                    error_Count_1 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }
                            else
                            {


                                error_Count_1 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }
                        }
                        else
                        {

                            error_Count_1 = 1;
                            Error_Count = 1;
                            Record_Count = 0;

                        }

                    }   // for loop close
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Something went wrong");
                }

                if (Record_Count > 0)
                {
                    XtraMessageBox.Show("Orders DeAllocated Successfully");
                    //BindOrdersByOperation();
                    Clear();
                }

                if (Error_Count > 0)
                {
                    XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Few Orders are not DeAllocated Please check in Error Status Column.", "Warning", MessageBoxButtons.OK);
                }
            }

        }
    }
}
