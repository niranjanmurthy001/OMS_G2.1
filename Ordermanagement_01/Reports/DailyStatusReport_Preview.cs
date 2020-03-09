using ClosedXML.Excel;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraSplashScreen;
//using DevExpress.Spreadsheet;
using ProgressBarExample;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class DailyStatusReport_Preview : XtraForm
    {
        DataTable dt_get_Shift_wise = new DataTable();
        DataTable dt_get = new DataTable();
        DataTable dt_get_eff = new DataTable();
        SplashScreenManager splashScreenManager1 = new SplashScreenManager();
        ProgressDialog pd = new ProgressDialog();
        DataTable dtuserexport = new DataTable();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        string My_Clients;
        string MY_Client;
        int User_id, Aging_User_id_value = 0, AgingUser_id_value = 0;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        ProgressBarControl progressBar = new ProgressBarControl();
        InfiniteProgressBar.clsProgress clsLoader = new InfiniteProgressBar.clsProgress();
        Hashtable httargetorder = new Hashtable();
        DataTable dttargetorder = new DataTable();
        DataTable dt_Order_Details = new DataTable();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        PivotGridControl pivotGrid = new PivotGridControl();
        DataTable dtclientexport = new DataTable();
        string userroleid;
        string Production_date;
        string Val_Branch_Name_1 = "";
        DataTable dt_Capacity_Utilization = new DataTable();
        private int userRoleId;
        private int Editvalue;
        int Client;
        int SubProcess;
        string Shift_Current_Date_Status = "False";
        DataTable dtclientReport = new DataTable();

        private string HeaderText { get; set; }
        public DailyStatusReport_Preview(int USER_ID, string User_roleid, string PRODUCTION_DATE)
        {
            InitializeComponent();
            User_id = USER_ID;
            userroleid = User_roleid;
            userRoleId = Convert.ToInt32(User_roleid);
            Production_date = PRODUCTION_DATE;



            //BindLookUpEdit_ShiftWise_Manager();
            //BindLookUpEdit_Product_Manager();
            //BindLookUpEdit_Product_Supervisor();
            //BindLookUpEdit_Product_ShiftType();

            //BindLookUpEdit_AgingPreview_OpenOrders_Manager();        // Aging Open Orders
            //BindLookUpEdit_AgingPreview_Pending_Orders_Manager();   // Aging Pending Orders

            // this.pivotGridControl1.ShowPivotTableFieldList = true;


        }

        //UserLookAndFeel lookAndFeel;
        //protected override DevExpress.LookAndFeel.UserLookAndFeel TargetLookAndFeel
        //{
        //    get
        //    {
        //        if (lookAndFeel == null)
        //        {
        //            lookAndFeel = new UserLookAndFeel(this);
        //            lookAndFeel.UseDefaultLookAndFeel = false;
        //            lookAndFeel.SkinName = "VS2010";
        //        }
        //        return lookAndFeel;

        //    }
        //}

        private void DailyStatusReport_Preview_Load(object sender, EventArgs e)
        {


            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                Bind_Admin_User_Clients();

                if (userRoleId == 1)
                {
                    BindClientName(lookUpEditAllEmpProdClientName);
                    BindClientName(lookUpEditAllClientName);
                    BindMyClientName(lookUpEditMyClientName, User_id);
                    BindMyClientName(lookUpEditMyEmpProdClientName, User_id);
                }
                else
                {
                    BindClientNumber(lookUpEditAllClientName);
                    BindClientNumber(lookUpEditAllEmpProdClientName);
                    BindMyClientnumber(lookUpEditMyClientName, User_id);
                    BindMyClientnumber(lookUpEditMyEmpProdClientName, User_id);
                }
                Text = "Daily Status Report";
                string D1 = DateTime.Now.ToString("M/d/yyyy");
                string D2 = DateTime.Now.ToString("M/d/yyyy");
                string D3 = DateTime.Now.ToString("MM/dd/yyyy");
                dateEditAllClientFromDate.Text = D1;
                dateEditAllClientToDate.Text = D1;
                dateEditAllEmpProdFromDate.Text = D1;
                dateEditAllEmpProdToDate.Text = D1;
                dateEditMyClientEmpProdFromDate.Text = D1;
                dateEditMyClientEmpProdToDate.Text = D1;
                dateEditMyClientsFromDate.Text = D1;
                dateEditMyClientsToDate.Text = D1;

                dateEdit_To_Date.Text = D2;
                dateEditShiftWiseCurrent.Text = D3;
                dateEdit1_Current_Date_Top_Eff.Text = D3;

                //var today = DateTime.Now;
                //startOfMonth = new DateTime(today.Year, 1, today.Month);
                //dateEdit_From_date.Text = startOfMonth.ToShortDateString();


                dateEdit_From_date.Text = D1;
                dateEditToAll.Text = D1;



                //  startOfMonth = new DateTime(today.Year, 1, today.Month);
                // dateEdit_From_date.Text = today.ToString("MM/01/yyyy");

                lookUpEdit_Branch.EditValue = 0;
                dbc.Bindbranch_Capacity(ddl_Capacity_Branch);
                BindLookUpEdit_ShiftWise_Manager();
                BindLookUpEdit_Product_Manager();
                BindLookUpEdit_Product_Supervisor();
                BindLookUpEdit_Product_ShiftType();

                BindLookUpEdit_AgingPreview_OpenOrders_Manager();        // Aging Open Orders
                BindLookUpEdit_AgingPreview_Pending_Orders_Manager();   // Aging Pending Orders
                Bind_Shift_Type_Master();
                //tableLayoutPanel4.RowStyles.RemoveAt(0);
                //tableLayoutPanel8.RowStyles.RemoveAt(0);
                //tableLayoutPanel4.Dock = DockStyle.Fill;

                this.WindowState = FormWindowState.Maximized;

                for (int i = 0; i < Chk_List_Report.ItemCount; i++)
                {
                    Chk_List_Report.SetItemChecked(i, true);

                }

                for (int i = 0; i < checkedLstBxCntrl_Branch_Wise.ItemCount; i++)
                {
                    checkedLstBxCntrl_Branch_Wise.SetItemChecked(i, true);

                }


                //panel10.Visible = false;
                //pivotGridControl2.Visible = false;

                //panel12.Visible = true;
                //pivotGridControl7_Shift_Datewise.Visible = true;

                InitTabScoreBoard();
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }




        }
        private void Bind_Admin_User_Clients()
        {

            try
            {
                Hashtable ht_Admin_User_Clients = new Hashtable();
                DataTable dt_Admin_User_Clients = new DataTable();

                ht_Admin_User_Clients.Add("@Trans", "GET_ADMIN_USER_CLIENTS");
                ht_Admin_User_Clients.Add("@Log_In_Userid", User_id);
                dt_Admin_User_Clients = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Admin_User_Clients);

                if (dt_Admin_User_Clients.Rows.Count > 0)
                {


                    for (int i = 0; i < dt_Admin_User_Clients.Rows.Count; i++)
                    {
                        MY_Client = MY_Client + dt_Admin_User_Clients.Rows[i]["Client_ID"].ToString();
                        MY_Client += (i < dt_Admin_User_Clients.Rows.Count) ? "," : string.Empty;

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void InitTabScoreBoard()
        {
            dbc.BindMonth(lookUpEditMonth);
            dbc.BindYear(lookUpEditYear);
            var htMonth = new Hashtable();

            htMonth.Add("@Trans", "GET_CURRENT_MONTH");
            var dtMonth = new DataAccess().ExecuteSP("Sp_Score_Board", htMonth);
            if (dtMonth.Rows.Count > 0)
            {
                lookUpEditMonth.EditValue = dtMonth.Rows[0]["C_Month"];
            }
            var htYear = new Hashtable();
            htYear.Add("@Trans", "CURRENT_YEAR");
            var dtYear = dataaccess.ExecuteSP("Sp_Score_Board", htYear);
            if (dtYear != null && dtYear.Rows.Count > 0)
            {
                lookUpEditYear.EditValue = dtYear.Rows[0]["Year"];
            }
            else
            {
                lookUpEditYear.EditValue = null;
            }
        }

        //private void pivotGridControl2_CustomFieldValueCells(object sender, EventArgs e)
        //{
        //    for (int i = e.GetCellCount(false) - 1; i >= 0; i--)
        //    {
        //        var cell = e.GetCell(false, i);
        //        if (cell != null)
        //            e.Remove(cell);
        //    }
        //}

        public void BindLookUpEdit_ShiftWise_Manager()
        {
            Hashtable ht_Shiftwise = new Hashtable();
            DataTable dt_Shiftwise = new DataTable();

            lookUpEdit_Branch.Properties.DataSource = null;

            ht_Shiftwise.Add("@Trans", "BIND_MANAGER");
            dt_Shiftwise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Shiftwise);

            DataRow dr = dt_Shiftwise.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_Shiftwise.Rows.InsertAt(dr, 0);

            lookUpEdit_Branch.Properties.DataSource = dt_Shiftwise;
            lookUpEdit_Branch.Properties.DisplayMember = "User_Name";
            lookUpEdit_Branch.Properties.ValueMember = "User_ID";

            // lookUpEdit_Shift_Manager.Properties.ForceInitialize();

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col_shift;

            col_shift = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            lookUpEdit_Branch.Properties.Columns.Add(col_shift);



        }


        public void BindLookUpEdit_Product_Manager()
        {


            //Hashtable ht_Pmang = new Hashtable();
            //System.Data.DataTable dt_Pmang = new System.Data.DataTable();

            //lookUpEdit_Product_Manager.Properties.DataSource = null;


            //ht_Pmang.Add("@Trans", "BIND_MANAGER");
            //dt_Pmang = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Pmang);

            //DataRow dr = dt_Pmang.NewRow();
            //dr[0] = 0;
            //dr[1] = "SELECT";
            //dt_Pmang.Rows.InsertAt(dr, 0);
            //lookUpEdit_Product_Manager.Properties.DataSource = dt_Pmang;
            //lookUpEdit_Product_Manager.Properties.DisplayMember = "User_Name";
            //lookUpEdit_Product_Manager.Properties.ValueMember = "User_ID";

            //DevExpress.XtraEditors.Controls.LookUpColumnInfo col_1;
            //col_1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            //lookUpEdit_Product_Manager.Properties.Columns.Add(col_1);



        }

        public void BindLookUpEdit_Product_Supervisor()
        {

            //Hashtable ht_bind_Supervisor = new Hashtable();
            //System.Data.DataTable dt_bind_Supervisor = new System.Data.DataTable();

            //lookUpEdit_Product_SuperVisor.Properties.DataSource = null;


            //ht_bind_Supervisor.Add("@Trans", "BIND_SUPERVISOR");
            //dt_bind_Supervisor = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_bind_Supervisor);

            //DataRow dr = dt_bind_Supervisor.NewRow();
            //dr[0] = 0;
            //dr[5] = "SELECT";
            //dt_bind_Supervisor.Rows.InsertAt(dr, 0);
            //lookUpEdit_Product_SuperVisor.Properties.DataSource = dt_bind_Supervisor;
            //lookUpEdit_Product_SuperVisor.Properties.DisplayMember = "User_Name";
            //lookUpEdit_Product_SuperVisor.Properties.ValueMember = "User_id";

            //DevExpress.XtraEditors.Controls.LookUpColumnInfo col_2;
            //col_2 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            //lookUpEdit_Product_SuperVisor.Properties.Columns.Add(col_2);

        }

        public void BindLookUpEdit_Product_ShiftType()
        {

            //Hashtable ht_bind_SHift = new Hashtable();
            //System.Data.DataTable dt_bind_SHift = new System.Data.DataTable();
            //lookUpEdit_Product_ShiftType.Properties.DataSource = null;

            //ht_bind_SHift.Add("@Trans", "BIND_SHIFT_MASTER_TYPE");
            //dt_bind_SHift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_bind_SHift);

            //DataRow dr = dt_bind_SHift.NewRow();
            //dr[0] = 0;
            //dr[1] = "SELECT";
            //dt_bind_SHift.Rows.InsertAt(dr, 0);
            //lookUpEdit_Product_ShiftType.Properties.DataSource = dt_bind_SHift;
            //lookUpEdit_Product_ShiftType.Properties.DisplayMember = "Shift_Type_Name";
            //lookUpEdit_Product_ShiftType.Properties.ValueMember = "Shift_Type_Id";

            //DevExpress.XtraEditors.Controls.LookUpColumnInfo col_3;
            //col_3 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Shift_Type_Name", 100);
            //lookUpEdit_Product_ShiftType.Properties.Columns.Add(col_3);

        }

        public void BindLookUpEdit_AgingPreview_OpenOrders_Manager()
        {
            Hashtable ht_AgingOPen = new Hashtable();
            DataTable dt_AgingOPen = new DataTable();

            lookUpEdit_AgingPreview_Open_Orders.Properties.DataSource = null;

            ht_AgingOPen.Add("@Trans", "BIND_MANAGER");
            dt_AgingOPen = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_AgingOPen);

            DataRow dr = dt_AgingOPen.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_AgingOPen.Rows.InsertAt(dr, 0);
            lookUpEdit_AgingPreview_Open_Orders.Properties.DataSource = dt_AgingOPen;
            lookUpEdit_AgingPreview_Open_Orders.Properties.DisplayMember = "User_Name";
            lookUpEdit_AgingPreview_Open_Orders.Properties.ValueMember = "User_ID";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col_4;
            col_4 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            lookUpEdit_AgingPreview_Open_Orders.Properties.Columns.Add(col_4);

        }

        //07/11/2018

        public void BindLookUpEdit_AgingPreview_Pending_Orders_Manager()
        {
            Hashtable ht_AgingPending = new Hashtable();
            DataTable dt_AgingPending = new DataTable();

            lookUpEdit_Aging_Pending_Orders_Manager.Properties.DataSource = null;

            ht_AgingPending.Add("@Trans", "BIND_MANAGER");
            dt_AgingPending = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_AgingPending);

            DataRow dr = dt_AgingPending.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_AgingPending.Rows.InsertAt(dr, 0);
            lookUpEdit_Aging_Pending_Orders_Manager.Properties.DataSource = dt_AgingPending;
            lookUpEdit_Aging_Pending_Orders_Manager.Properties.DisplayMember = "User_Name";
            lookUpEdit_Aging_Pending_Orders_Manager.Properties.ValueMember = "User_ID";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            lookUpEdit_Aging_Pending_Orders_Manager.Properties.Columns.Add(col);

        }

        protected void GetDateWiseInto_PivotGrid()
        {
            //load_Progressbar.Start_progres();

            try
            {
                httargetorder.Clear();
                dttargetorder.Clear();

                httargetorder.Add("@Trans", "SELECT");
                dttargetorder = dataaccess.ExecuteSP("Sp_Daily_Status_Report", httargetorder);
                dt_Order_Details = dttargetorder;

                // Assign the data source to the PivotGrid control.
                pivotGridControlDailyWise.DataSource = dttargetorder;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        protected void Get_Date_Wise_Into_PivotGrid()
        {
            //load_Progressbar.Start_progres();

            try
            {
                httargetorder.Clear();
                dttargetorder.Clear();

                httargetorder.Add("@Trans", "SELECT");
                dttargetorder = dataaccess.ExecuteSP("Sp_Daily_Status_Report", httargetorder);
                dt_Order_Details = dttargetorder;


                // Assign the data source to the PivotGrid control.
                pivotGridControlDailyWise.DataSource = dttargetorder;

                // Create a row Pivot Grid Control field bound to the Country datasource field.
                PivotGridField fieldDate = new PivotGridField("Date", PivotArea.RowArea);

                //// Create a row Pivot Grid Control field bound to the Sales Person datasource field.
                //PivotGridField fieldClient = new PivotGridField("Client_Number", PivotArea.ColumnArea);
                //fieldClient.Caption = "Client";


                // Create a row Pivot Grid Control field bound to the Sales Person datasource field.
                PivotGridField fieldClient = new PivotGridField("Client_NUmber", PivotArea.ColumnArea);
                fieldClient.Caption = "ClientNumber";

                //// Create a filter Pivot Grid Control field bound to the ProductName datasource field.
                //PivotGridField fieldProductName = new PivotGridField("Client_Name", PivotArea.FilterArea);
                //fieldProductName.Caption = "Client Name";


                // Create a filter Pivot Grid Control field bound to the ProductName datasource field.
                PivotGridField fieldProductName = new PivotGridField("Client_NUmber", PivotArea.FilterArea);
                fieldProductName.Caption = "Client Number";


                // Create a data Pivot Grid Control field bound to the 'Extended Price' datasource field.
                PivotGridField fieldExtendedPrice = new PivotGridField("Received", PivotArea.DataArea);
                fieldExtendedPrice.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                // Specify the formatting setting to format summary values as integer currency amount.
                // fieldExtendedPrice.CellFormat.FormatString = "c0";


                // Create a data Pivot Grid Control field bound to the 'Extended Price' datasource field.
                PivotGridField field_ExtendedPrice = new PivotGridField("Completed", PivotArea.DataArea);
                field_ExtendedPrice.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                // Add the fields to the control's field collection.         
                pivotGridControlDailyWise.Fields.AddRange(new PivotGridField[] {fieldDate,fieldClient,
                                            fieldProductName, fieldExtendedPrice, field_ExtendedPrice});


                // Arrange the row fields within the Row Header Area.
                fieldDate.AreaIndex = 0;
                // fieldCustomer.AreaIndex = 1;

                // Arrange the column fields within the Column Header Area.
                fieldClient.AreaIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void gettaskwise()
        {
            httargetorder.Clear();
            dttargetorder.Clear();

            httargetorder.Add("@Trans", "SELECT_TASK_WISE");
            dttargetorder = dataaccess.ExecuteSP("Sp_Daily_Status_Report", httargetorder);
            dt_Order_Details = dttargetorder;


            // Assign the data source to the PivotGrid control.
            pivotGridControlShiftWise1.DataSource = dttargetorder;
        }

        private void ShowPivotGridPreview_1(PivotGridControl pivotGrid)
        {

            // Verify that the Pivot Grid Control can be printed.
            if (!pivotGrid.IsPrintingAvailable)
            {
                MessageBox.Show("Missing DevExpress.XtraPrinting library", "Error");
                return;
            }
            //pivotGrid.ShowPrintPreview();

            pivotGrid.ShowRibbonPrintPreview();
        }

        // preview
        private void ShowPivotGridPreview()
        {

            // Verify that the Pivot Grid Control can be printed.
            if (!pivotGrid.IsPrintingAvailable)
            {
                MessageBox.Show("Missing DevExpress.XtraPrinting library", "Error");
                return;
            }
            // pivotGrid.ShowPrintPreview();

            pivotGrid.ShowRibbonPrintPreview();
        }

        // print
        private void PrintPivotGrid(PivotGridControl pivotGrid)
        {
            // Verify that the Pivot Grid Control can be printed.
            if (!pivotGrid.IsPrintingAvailable)
            {
                MessageBox.Show("Missing DevExpress.XtraPrinting library", "Error");
                return;
            }
            pivotGrid.Print();
        }



        // clear
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            //dateEdit_From_date.Text="";
            //dateEdit_To_Date.Text="";

            try
            {
                for (int i = 0; i < Chk_List_Report.ItemCount; i++)
                {
                    Chk_List_Report.SetItemChecked(i, false);
                }

                for (int i = 0; i < checkedLstBxCntrl_Branch_Wise.ItemCount; i++)
                {
                    checkedLstBxCntrl_Branch_Wise.SetItemChecked(i, false);
                }


                string D2 = DateTime.Now.ToString("M/d/yyyy");
                string D3 = DateTime.Now.ToString("MM/dd/yyyy");
                dateEdit_From_date.Text = D2;
                dateEdit_To_Date.Text = D2;
                dateEditShiftWiseCurrent.Text = D3;
                dateEdit1_Current_Date_Top_Eff.Text = D3;




            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Something Went Wrong Please Check with Administrator.", "Warning", MessageBoxButtons.OK);
            }

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                string checkAll = Chk_List_Report.Items[0].CheckState.ToString();
                string checkDaily = Chk_List_Report.Items[1].CheckState.ToString();
                string checkShift = Chk_List_Report.Items[2].CheckState.ToString();
                string checkProductType = Chk_List_Report.Items[3].CheckState.ToString();
                string checkOpen = Chk_List_Report.Items[4].CheckState.ToString();
                string checkPending = Chk_List_Report.Items[5].CheckState.ToString();
                string checkEfficiency = Chk_List_Report.Items[6].CheckState.ToString();
                string checkCapacity = Chk_List_Report.Items[7].CheckState.ToString();
                Shift_Current_Date_Status = "False";
                if (checkAll == "Checked")
                {
                    DateWise();
                    ShiftWise();
                    ProductTypeDateWise();
                    AgingOpenOrdersDateWise();
                    AgingPendingOrdersDateWise();
                    TopEffieciency(Production_date.ToString());
                    CapacityUtilization();
                }
                else
                {
                    if (checkDaily == "Checked")
                    {
                        DateWise();
                    }
                    if (checkShift == "Checked")
                    {
                        ShiftWise();
                    }
                    if (checkProductType == "Checked")
                    {
                        ProductTypeDateWise();
                    }
                    if (checkOpen == "Checked")
                    {
                        AgingOpenOrdersDateWise();
                    }
                    if (checkPending == "Checked")
                    {
                        AgingPendingOrdersDateWise();
                    }
                    if (checkEfficiency == "Checked")
                    {
                        TopEffieciency(Production_date.ToString());
                    }
                    if (checkCapacity == "Checked")
                    {
                        CapacityUtilization();
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }



        private void DateWise()
        {
            string fdate = dateEdit_From_date.Text.ToString();
            string todate = dateEditToAll.Text.ToString();
            if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "")
            {
                httargetorder.Clear();
                dttargetorder.Clear();
                httargetorder.Add("@Trans", "SELECT_DATE_WISE");
                httargetorder.Add("@Fromdate", fdate);
                httargetorder.Add("@Todate", todate);
                dttargetorder = dataaccess.ExecuteSP("Sp_Daily_Status_Report", httargetorder);
                dt_Order_Details = dttargetorder;
                pivotGridControlDailyWise.DataSource = dttargetorder;
            }
            else
            {
                pivotGridControlDailyWise.DataSource = null;
            }
        }

        private void ShiftWise()
        {
            string Check_Branch_All = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
            string Check_Banglore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
            string Check_Hosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();

            pivotGridControlShiftWise1.Visible = false;
            pivotGridControlShiftWise2.Visible = false;
            pivotGridControlShiftWise.Visible = true;
            pivotGridControlShiftWise.Dock = DockStyle.Fill;

            //new 10-july-2019
            if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
            {
                Hashtable ht_get = new Hashtable();
                DataTable dt_get = new DataTable();
                ht_get.Clear();
                dt_get.Clear();
                ht_get.Add("@Trans", "SHIFT_WISE_FROM_AND_TO_DATE");
                ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get.Add("@Todate", dateEditToAll.Text);
                dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get);
                pivotGridControlShiftWise.DataSource = dt_get;

            }

            // both bangl and hosur
            else if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
            {
                Hashtable ht_get = new Hashtable();
                DataTable dt_get = new DataTable();
                ht_get.Clear();
                dt_get.Clear();
                ht_get.Add("@Trans", "SHIFT_WISE_FROM_AND_TO_DATE");
                ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get.Add("@Todate", dateEditToAll.Text);
                dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get);
                pivotGridControlShiftWise.DataSource = dt_get;
            }

            // banglore branch wise
            else if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Checked" && Check_Hosur == "Unchecked")
            {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                try
                {

                    Hashtable ht_getShift = new Hashtable();
                    DataTable dt_getShift = new DataTable();

                    ht_getShift.Add("@Trans", "SHIFT_FROM_AND_TO_DATE_AND_BRANCH_WISE");
                    ht_getShift.Add("@Fromdate", dateEdit_From_date.Text);
                    ht_getShift.Add("@Todate", dateEditToAll.Text);
                    ht_getShift.Add("@Branch_ID", 3);
                    dt_getShift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getShift);

                    if (dt_getShift.Rows.Count > 0)
                    {
                        pivotGridControlShiftWise.DataSource = dt_getShift;
                    }
                    else
                    {
                        pivotGridControlShiftWise.DataSource = null;
                    }
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
            // hosur branch wise
            else if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Checked")
            {

                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                try
                {

                    Hashtable ht_Shift_hosur = new Hashtable();
                    DataTable dt_Shift_hosur = new DataTable();

                    ht_Shift_hosur.Add("@Trans", "SHIFT_FROM_AND_TO_DATE_AND_BRANCH_WISE");
                    ht_Shift_hosur.Add("@Fromdate", dateEdit_From_date.Text);
                    ht_Shift_hosur.Add("@Todate", dateEditToAll.Text);
                    ht_Shift_hosur.Add("@Branch_ID", 5);
                    dt_Shift_hosur = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Shift_hosur);

                    if (dt_Shift_hosur.Rows.Count > 0)
                    {
                        pivotGridControlShiftWise.DataSource = dt_Shift_hosur;
                    }
                    else
                    {
                        pivotGridControlShiftWise.DataSource = null;
                    }
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
        }

        private void ProductTypeDateWise()
        {
            if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "")
            {
                Hashtable ht_get_ProductType = new Hashtable();
                DataTable dt_get_ProductType = new DataTable();

                ht_get_ProductType.Add("@Trans", "PRODUCT_TYPE_DATE_WISE");
                ht_get_ProductType.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_ProductType.Add("@Todate", dateEditToAll.Text);
                dt_get_ProductType = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_ProductType);
                pivotGridControlProductTypeWise.DataSource = dt_get_ProductType;
            }
            else
            {
                pivotGridControlProductTypeWise.DataSource = null;
            }

            //// all Branch Wise
            //if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
            //{
            //    Hashtable ht_get_ProductType = new Hashtable();
            //    System.Data.DataTable dt_get_ProductType = new System.Data.DataTable();

            //    ht_get_ProductType.Add("@Trans", "PRODUCT_TYPE_DATE_WISE");
            //    ht_get_ProductType.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_ProductType.Add("@Todate", dateEdit_To_Date.Text);
            //    dt_get_ProductType = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_ProductType);

            //    pivotGridControlProductTypeWise.DataSource = dt_get_ProductType;
            //}

            //// banglore branch wise
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Checked" && Check_Hosur == "Unchecked")
            //{
            //    Hashtable ht_get_ProductType = new Hashtable();
            //    System.Data.DataTable dt_get_ProductType = new System.Data.DataTable();

            //    ht_get_ProductType.Add("@Trans", "PRODUCT_TYPE_DATE_WISE_AND_BANGLORE_BRANCH_WISE");
            //    ht_get_ProductType.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_ProductType.Add("@Todate", dateEdit_To_Date.Text);
            //    ht_get_ProductType.Add("@Branch_ID", 3);
            //    dt_get_ProductType = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_ProductType);

            //    pivotGridControlProductTypeWise.DataSource = dt_get_ProductType;

            //}
            ////hosur branch wise
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Checked")
            //{
            //    Hashtable ht_get_ProductType = new Hashtable();
            //    System.Data.DataTable dt_get_ProductType = new System.Data.DataTable();

            //    ht_get_ProductType.Add("@Trans", "PRODUCT_TYPE_DATE_WISE_AND_HOSUR_BRANCH_WISE");
            //    ht_get_ProductType.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_ProductType.Add("@Todate", dateEdit_To_Date.Text);
            //    ht_get_ProductType.Add("@Branch_ID", 5);
            //    dt_get_ProductType = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_ProductType);

            //    pivotGridControlProductTypeWise.DataSource = dt_get_ProductType;

            //}
            ////none 
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
            //{
            //    pivotGridControlProductTypeWise.DataSource = null;
            //}

        }

        // Shift Wise

        private void ShiftTypeCurrentDate()
        {
            pivotGridControlShiftWise.DataSource = null;
            pivotGridControlShiftWise.Visible = true;
            pivotGridControlShiftWise2.Visible = false;

            Hashtable ht_get_Shift = new Hashtable();
            DataTable dt_get_Shift = new DataTable();

            ht_get_Shift.Add("@Trans", "SHIFT_DATE_WISE");
            ht_get_Shift.Add("@date", dateEditShiftWiseCurrent.Text.ToString());
            dt_get_Shift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift);
            dt_get_Shift_wise = dt_get_Shift;
            pivotGridControlShiftWise.DataSource = dt_get_Shift;
        }


        // 02/july-2019 shift banglore branch wise

        private void ShiftTypeWiseCurrentDateBranchWise(int branchId)
        {
            pivotGridControlShiftWise.DataSource = null;
            pivotGridControlShiftWise.Visible = true;
            pivotGridControlShiftWise2.Visible = false;

            Hashtable ht_get_Shift = new Hashtable();
            DataTable dt_get_Shift = new DataTable();
            ht_get_Shift.Add("@Trans", "SHIFT_DATE_WISE_BRANCH_WISE");
            ht_get_Shift.Add("@date", dateEditShiftWiseCurrent.Text.ToString());
            ht_get_Shift.Add("@Branch_ID", branchId);
            dt_get_Shift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift);
            dt_get_Shift_wise = dt_get_Shift;
            pivotGridControlShiftWise.DataSource = dt_get_Shift;

        }
        //private void ShiftType_MANGER_And_Current_Date_Wise(int Userid_value)
        //{
        //    // load_Progressbar.Start_progres();
        //    if (dateEdit_Current_Date_ShiftWise.Text != "")
        //    {
        //        Hashtable ht_Admin_User_Clients = new Hashtable();
        //        System.Data.DataTable dt_Admin_User_Clients = new System.Data.DataTable();

        //        ht_Admin_User_Clients.Add("@Trans", "GET_ADMIN_USER_CLIENTS");
        //        ht_Admin_User_Clients.Add("@Log_In_Userid", Userid_value);
        //        dt_Admin_User_Clients = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Admin_User_Clients);
        //        My_Clients = "";
        //        StringBuilder sb = new StringBuilder();
        //        if (dt_Admin_User_Clients.Rows.Count > 0)
        //        {


        //            for (int i = 0; i < dt_Admin_User_Clients.Rows.Count; i++)
        //            {
        //                // My_Clients = My_Clients + dt_Admin_User_Clients.Rows[i]["Client_ID"].ToString();
        //                //My_Clients += (i < dt_Admin_User_Clients.Rows.Count) ? "," : string.Empty;


        //                My_Clients = dt_Admin_User_Clients.Rows[i]["Client_ID"].ToString();
        //                // My_Clients = Single_Client;
        //                sb = sb.Append("," + My_Clients);
        //                My_Clients = sb.ToString();
        //            }

        //        }

        //        Hashtable ht_getShift = new Hashtable();
        //        System.Data.DataTable dt_getShift = new System.Data.DataTable();

        //        ht_getShift.Clear();
        //        dt_getShift.Clear();

        //        ht_getShift.Add("@Trans", "MANGER_AND_CURRENT_DATE_WISE");
        //        ht_getShift.Add("@date", dateEdit_Current_Date_ShiftWise.Text);
        //        ht_getShift.Add("@My_Clients", My_Clients);

        //        dt_getShift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getShift);

        //        pivotGridControlShiftWise1.Visible = true;
        //        pivotGridControlShiftWise2.Visible = false;
        //        pivotGridControlShiftWise1.DataSource = dt_getShift;

        //    }
        //    else
        //    {
        //        pivotGridControlShiftWise1.DataSource = null;
        //    }

        //}

        private void btn_Shiftwise_Filter_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                Shift_Current_Date_Status = "True";

                string branchAll = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
                string branchBangalore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
                string branchHosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();

                if (dateEditShiftWiseCurrent.Text == string.Empty)
                {
                    XtraMessageBox.Show("select date");
                    dateEditShiftWiseCurrent.Focus();
                    return;
                }
                if (branchAll == "Checked")
                {
                    ShiftTypeCurrentDate();
                }
                else
                {
                    int branch = 0;
                    if (branchBangalore == "Checked")
                    {
                        branch = 3;
                    }
                    if (branchHosur == "Checked")
                    {
                        branch = 5;
                    }
                    ShiftTypeWiseCurrentDateBranchWise(branch);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Something went wrong contact admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }



        private void lookUpEdit_Shift_Manager_Click(object sender, EventArgs e)
        {
            lookUpEdit_Branch.EditValue = null;
            lookUpEdit_Branch.EditValue = "SELECT";

            // lookUpEdit_Shift_Manager.EditValue = 0;

        }

        private void lookUpEdit_Shift_Manager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Clear)
            {
                (sender as LookUpEdit).EditValue = null;
            }
        }

        private void btn_ShiftWiseType_Clear_Click(object sender, EventArgs e)
        {
            //lookUpEdit_Shift_Manager.Text = "SELECT";
            lookUpEdit_Branch.EditValue = 0;

            string D2 = DateTime.Now.ToString("M/d/yyyy");
            string D3 = DateTime.Now.ToString("MM/dd/yyyy");
            dateEdit_From_date.Text = D2;
            dateEdit_To_Date.Text = D2;
            dateEditShiftWiseCurrent.Text = D3;

        }

        private void Filter_Manager_Wise(int Userid_value)
        {
            //load_Progressbar.Start_progres();

            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {
                Hashtable ht_get_Mangerwise = new Hashtable();
                DataTable dt_get_Mangerwise = new DataTable();

                ht_get_Mangerwise.Add("@Trans", "PRODUCT_TYPE_MANAGER_WISE");
                ht_get_Mangerwise.Add("@Reporting_To_Id_2", Userid_value);
                ht_get_Mangerwise.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_Mangerwise.Add("@Todate", dateEdit_To_Date.Text);
                dt_get_Mangerwise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Mangerwise);

                pivotGridControlProductTypeWise.DataSource = dt_get_Mangerwise;


            }
            else
            {
                pivotGridControlProductTypeWise.DataSource = null;
            }
        }

        private void Filter_Supervisor_Wise(int Supervisor_User_Id)
        {
            // load_Progressbar.Start_progres();
            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {
                Hashtable ht_get_Supervisorwise = new Hashtable();
                DataTable dt_get_Supervisorwise = new DataTable();

                ht_get_Supervisorwise.Add("@Trans", "PRODUCT_TYPE_SUPERVISOR_WISE");
                ht_get_Supervisorwise.Add("@Reporting_To_Id_1", Supervisor_User_Id);
                ht_get_Supervisorwise.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_Supervisorwise.Add("@Todate", dateEdit_To_Date.Text);
                dt_get_Supervisorwise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Supervisorwise);

                pivotGridControlProductTypeWise.DataSource = dt_get_Supervisorwise;
            }
            else
            {
                pivotGridControlProductTypeWise.DataSource = null;

            }
        }

        private void Filter_Shift_Wise(int Shift_Type_Id)
        {
            //load_Progressbar.Start_progres();
            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {
                Hashtable ht_get_ShiftType_wise = new Hashtable();
                DataTable dt_get_ShiftType_wise = new DataTable();

                ht_get_ShiftType_wise.Add("@Trans", "PRODUCT_TYPE_SHIFTTYPE_WISE");
                ht_get_ShiftType_wise.Add("@Shift_Type_Id", Shift_Type_Id);
                ht_get_ShiftType_wise.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_ShiftType_wise.Add("@Todate", dateEdit_To_Date.Text);
                dt_get_ShiftType_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_ShiftType_wise);

                pivotGridControlProductTypeWise.DataSource = dt_get_ShiftType_wise;
            }
            else
            {
                pivotGridControlProductTypeWise.DataSource = null;
            }
        }

        private void Filter_Superviosr_Shift_Wise(int Supervisor_User_Id, int ShiftType_Id)
        {
            //load_Progressbar.Start_progres();
            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {
                Hashtable ht_Superviosr_Shift_Wise = new Hashtable();
                DataTable dt_Superviosr_Shift_Wise = new DataTable();

                ht_Superviosr_Shift_Wise.Add("@Trans", "PRODUCT_TYPE_SUPERVIOSR_SHIFT_WISE");
                ht_Superviosr_Shift_Wise.Add("@Reporting_To_Id_1", Supervisor_User_Id);
                ht_Superviosr_Shift_Wise.Add("@Shift_Type_Id", ShiftType_Id);
                ht_Superviosr_Shift_Wise.Add("@Fromdate", dateEdit_From_date.Text);
                ht_Superviosr_Shift_Wise.Add("@Todate", dateEdit_To_Date.Text);

                dt_Superviosr_Shift_Wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Superviosr_Shift_Wise);

                pivotGridControlProductTypeWise.DataSource = dt_Superviosr_Shift_Wise;
            }

            else
            {
                pivotGridControlProductTypeWise.DataSource = null;
            }
        }

        private void Filter_Manager_Shift_Wise(int Userid_value, int ShiftType_Id)
        {
            //load_Progressbar.Start_progres();
            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {
                Hashtable ht_Manager_Shift = new Hashtable();
                DataTable dt_Manager_Shift = new DataTable();

                ht_Manager_Shift.Add("@Trans", "PRODUCT_TYPE_MANAGER_SHIFT_WISE");
                ht_Manager_Shift.Add("@Reporting_To_Id_2", Userid_value);
                ht_Manager_Shift.Add("@Shift_Type_Id", ShiftType_Id);
                ht_Manager_Shift.Add("@Fromdate", dateEdit_From_date.Text);
                ht_Manager_Shift.Add("@Todate", dateEdit_To_Date.Text);
                dt_Manager_Shift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Manager_Shift);

                pivotGridControlProductTypeWise.DataSource = dt_Manager_Shift;
            }
            else
            {
                pivotGridControlProductTypeWise.DataSource = null;
            }
        }

        private void Filter_Manager_Supervisor_Wise(int Userid_value, int Supervisor_User_Id)
        {
            // load_Progressbar.Start_progres();
            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {
                Hashtable ht_Manager_Superviosr_Wise = new Hashtable();
                DataTable dt_Manager_Superviosr_Wise = new DataTable();

                ht_Manager_Superviosr_Wise.Add("@Trans", "PRODUCT_TYPE_MANAGER_SUPERVIOSR_WISE");
                ht_Manager_Superviosr_Wise.Add("@Reporting_To_Id_2", Userid_value);
                ht_Manager_Superviosr_Wise.Add("@Reporting_To_Id_1", Supervisor_User_Id);
                ht_Manager_Superviosr_Wise.Add("@Fromdate", dateEdit_From_date.Text);
                ht_Manager_Superviosr_Wise.Add("@Todate", dateEdit_To_Date.Text);
                dt_Manager_Superviosr_Wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Manager_Superviosr_Wise);

                pivotGridControlProductTypeWise.DataSource = dt_Manager_Superviosr_Wise;
            }
            else
            {
                pivotGridControlProductTypeWise.DataSource = null;
            }
        }

        private void Filter_Manager_Supervisor_Shift_Wise(int Userid_value, int Supervisor_User_Id, int ShiftType_Id)
        {
            // load_Progressbar.Start_progres();
            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {

                Hashtable ht_Mang_Superv_Shift = new Hashtable();
                DataTable dt_Mang_Superv_Shift = new DataTable();

                ht_Mang_Superv_Shift.Add("@Trans", "PRODUCT_TYPE_MANAGER_SUPERVIOSR_SHIFT_WISE");
                ht_Mang_Superv_Shift.Add("@Reporting_To_Id_2", Userid_value);
                ht_Mang_Superv_Shift.Add("@Reporting_To_Id_1", Supervisor_User_Id);
                ht_Mang_Superv_Shift.Add("@Shift_Type_Id", ShiftType_Id);
                ht_Mang_Superv_Shift.Add("@Fromdate", dateEdit_From_date.Text);
                ht_Mang_Superv_Shift.Add("@Todate", dateEdit_To_Date.Text);
                dt_Mang_Superv_Shift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Mang_Superv_Shift);

                pivotGridControlProductTypeWise.DataSource = dt_Mang_Superv_Shift;
            }
            else
            {
                pivotGridControlProductTypeWise.DataSource = null;
            }
        }

        private void lookUpEdit_Manager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Clear)
            {
                (sender as LookUpEdit).EditValue = null;
            }
        }

        private void lookUpEdit_SuperVisor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Clear)
            {
                (sender as LookUpEdit).EditValue = null;
            }
        }

        private void lookUpEdit_ShiftType_Click(object sender, EventArgs e)
        {
            //lookUpEdit_ShiftType.EditValue = null;
            //lookUpEdit_ShiftType.EditValue = "SELECT";
        }

        private void lookUpEdit_ShiftType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Clear)
            {
                (sender as LookUpEdit).EditValue = null;
            }
        }

        private void btn_Product_Type_Wise_Clear_Click(object sender, EventArgs e)
        {
            //lookUpEdit_Manager.Text = "SELECT";
            //lookUpEdit_SuperVisor.Text = "SELECT";
            //lookUpEdit_ShiftType.Text = "SELECT";

            load_Progressbar.Start_progres();


        }

        // Aging open Orders

        private void btn_Aging_Preview_Open_Ordes_Sumbit_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                My_Clients = "";
                Aging_User_id_value = 0;
                object obj_4 = lookUpEdit_AgingPreview_Open_Orders.EditValue;
                string User_name = lookUpEdit_AgingPreview_Open_Orders.Text;
                if (obj_4.ToString() != "0")
                {
                    Aging_User_id_value = (int)obj_4;
                }

                load_Progressbar.Start_progres();

                if (Aging_User_id_value != 0)
                {
                    // Bind_Admin_User_Clients(Aging_User_id_value);

                    Filter_Aging_Preview_OpenOrders_Manager_Wise(Aging_User_id_value);
                }

                else
                {
                    AgingOpenOrdersDateWise();
                }
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        //Aging Opend Orders Date Wise

        private void AgingOpenOrdersDateWise()
        {

            if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "")
            {
                Hashtable ht_get_AginOpenOrders = new Hashtable();
                DataTable dt_get_AginOpenOrders = new DataTable();

                ht_get_AginOpenOrders.Add("@Trans", "AGING_PREVIEW_OPEN_ORDER_DATE_WISE");
                ht_get_AginOpenOrders.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_AginOpenOrders.Add("@Todate", dateEditToAll.Text);
                dt_get_AginOpenOrders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_AginOpenOrders);

                pivotGridControlAgingOpenOrders.DataSource = dt_get_AginOpenOrders;
            }
            else
            {
                //  pivotGridControl6.DataSource = null;
                pivotGridControlAgingOpenOrders.DataSource = null;
            }



            //// all Branch Wise
            //if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
            //{
            //        Hashtable ht_get_AginOpenOrders = new Hashtable();
            //    System.Data.DataTable dt_get_AginOpenOrders = new System.Data.DataTable();

            //    ht_get_AginOpenOrders.Add("@Trans", "AGING_PREVIEW_OPEN_ORDER_DATE_WISE");
            //    ht_get_AginOpenOrders.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_AginOpenOrders.Add("@Todate", dateEdit_To_Date.Text);
            //    dt_get_AginOpenOrders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_AginOpenOrders);

            //    pivotGridControlAgingOpenOrders.DataSource = dt_get_AginOpenOrders;
            //}
            //// banglore branch wise
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Checked" && Check_Hosur == "Unchecked")
            //{
            //    Hashtable ht_get_AginOpen_Orders = new Hashtable();
            //    System.Data.DataTable dt_get_AginOpen_Orders = new System.Data.DataTable();

            //    ht_get_AginOpen_Orders.Add("@Trans", "AGING_PREVIEW_OPEN_ORDER_DATE_AND_BANGLORE_BRANCH_WISE");
            //    ht_get_AginOpen_Orders.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_AginOpen_Orders.Add("@Todate", dateEdit_To_Date.Text);
            //    ht_get_AginOpen_Orders.Add("@Branch_ID",3);
            //    dt_get_AginOpen_Orders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_AginOpen_Orders);

            //    //pivotGridControl6.DataSource = dt_get_AginOpenOrders;

            //    pivotGridControlAgingOpenOrders.DataSource = dt_get_AginOpen_Orders;
            //}
            //// hosur branch wise
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Checked")
            //{
            //    Hashtable ht_get_Agin_Open_Orders = new Hashtable();
            //    System.Data.DataTable dt_get_Agin_Open_Orders = new System.Data.DataTable();

            //    ht_get_Agin_Open_Orders.Add("@Trans", "AGING_PREVIEW_OPEN_ORDER_DATE_AND_HOSUR_BRANCH_WISE");
            //    ht_get_Agin_Open_Orders.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_Agin_Open_Orders.Add("@Todate", dateEdit_To_Date.Text);
            //    ht_get_Agin_Open_Orders.Add("@Branch_ID", 5);
            //    dt_get_Agin_Open_Orders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Agin_Open_Orders);

            //    //pivotGridControl6.DataSource = dt_get_AginOpenOrders;

            //    pivotGridControlAgingOpenOrders.DataSource = dt_get_Agin_Open_Orders;
            //}
            ////none
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
            //{
            //    pivotGridControlAgingOpenOrders.DataSource = null;
            //}

        }

        // manager wise
        private void Filter_Aging_Preview_OpenOrders_Manager_Wise(int Aging_User_id_value)
        {
            //load_Progressbar.Start_progres();
            StringBuilder sb = new StringBuilder();
            Hashtable ht_Admin_User_Clients = new Hashtable();
            DataTable dt_Admin_User_Clients = new DataTable();

            ht_Admin_User_Clients.Add("@Trans", "GET_ADMIN_USER_CLIENTS");
            ht_Admin_User_Clients.Add("@Log_In_Userid", Aging_User_id_value);
            dt_Admin_User_Clients = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Admin_User_Clients);

            if (dt_Admin_User_Clients.Rows.Count > 0)
            {


                for (int i = 0; i < dt_Admin_User_Clients.Rows.Count; i++)
                {
                    // My_Clients = My_Clients + dt_Admin_User_Clients.Rows[i]["Client_ID"].ToString();
                    //My_Clients += (i < dt_Admin_User_Clients.Rows.Count) ? "," : string.Empty;


                    My_Clients = dt_Admin_User_Clients.Rows[i]["Client_ID"].ToString();
                    // My_Clients = My_Client;
                    sb = sb.Append("," + My_Clients);
                    My_Clients = sb.ToString();
                }

            }


            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
            {
                Hashtable ht_get_Aging_OPendOrders_Manager = new Hashtable();
                DataTable dt_get_Aging_OPendOrders_Manager = new DataTable();

                //ht_get_Aging_OPendOrders_Manager.Add("@Trans", "AGING_PREVIEW_OPEN_ORDER_MANAGER_WISE");
                ht_get_Aging_OPendOrders_Manager.Add("@Trans", "GET_AGING_OPEN_ORDER_MANAGER_WISE_GRID_DETAILS");
                ht_get_Aging_OPendOrders_Manager.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_Aging_OPendOrders_Manager.Add("@Todate", dateEdit_To_Date.Text);
                //ht_get_Aging_OPendOrders_Manager.Add("@Reporting_To_Id_2", Aging_User_id_value);
                ht_get_Aging_OPendOrders_Manager.Add("@My_Clients", My_Clients);
                dt_get_Aging_OPendOrders_Manager = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Aging_OPendOrders_Manager);

                pivotGridControlAgingOpenOrders.DataSource = dt_get_Aging_OPendOrders_Manager;
            }
            else
            {
                pivotGridControlAgingOpenOrders.DataSource = null;
            }
        }

        private void lookUpEdit_AgingPreview_Open_Orders_Click(object sender, EventArgs e)
        {
            lookUpEdit_AgingPreview_Open_Orders.EditValue = null;
            lookUpEdit_AgingPreview_Open_Orders.EditValue = "SELECT";
            lookUpEdit_AgingPreview_Open_Orders.EditValue = 0;
        }

        private void lookUpEdit_AgingPreview_Open_Orders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Clear)
            {
                (sender as LookUpEdit).EditValue = null;
            }
        }

        // 07/11/2018

        //Aging PENDING Orders Date Wise

        private void btn_Aging_Preview_Pending_Ordes_Sumbit_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                AgingUser_id_value = 0;
                My_Clients = "";
                object obj_5 = lookUpEdit_Aging_Pending_Orders_Manager.EditValue;
                string AgingPedning_User_name = lookUpEdit_Aging_Pending_Orders_Manager.Text;
                if (obj_5.ToString() != "0")
                {
                    AgingUser_id_value = (int)obj_5;
                }

                load_Progressbar.Start_progres();
                if (AgingUser_id_value != 0)
                {
                    // Bind_Admin_User_Clients(AgingUser_id_value);

                    Filter_AgingPreview_PendingOrders_Manager_Wise(AgingUser_id_value);
                }

                else
                {
                    AgingPendingOrdersDateWise();
                }
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }


        private void AgingPendingOrdersDateWise()
        {

            if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "")
            {
                Hashtable ht_get_AginOpenOrders = new Hashtable();
                DataTable dt_get_AginOpenOrders = new DataTable();

                ht_get_AginOpenOrders.Add("@Trans", "AGING_PREVIEW_PENDING_ORDER_DATE_WISE");
                ht_get_AginOpenOrders.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_AginOpenOrders.Add("@Todate", dateEditToAll.Text);
                dt_get_AginOpenOrders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_AginOpenOrders);

                pivotGridControlAginfPendingOrders.DataSource = dt_get_AginOpenOrders;

            }
            else
            {
                pivotGridControlAginfPendingOrders.DataSource = null;

            }



            // all brabnch wise

            //if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
            //{
            //        Hashtable ht_get_AginOpenOrders = new Hashtable();
            //    System.Data.DataTable dt_get_AginOpenOrders = new System.Data.DataTable();

            //    ht_get_AginOpenOrders.Add("@Trans", "AGING_PREVIEW_PENDING_ORDER_DATE_WISE");
            //    ht_get_AginOpenOrders.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_AginOpenOrders.Add("@Todate", dateEdit_To_Date.Text);
            //    dt_get_AginOpenOrders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_AginOpenOrders);

            //    pivotGridControlAginfPendingOrders.DataSource = dt_get_AginOpenOrders;
            //    //pivotGridControl7.DataSource = dt_get_AginOpenOrders;
            //}
            //// banglore branch wise
            //if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Checked" && Check_Hosur == "Unchecked")
            //{
            //    Hashtable ht_get_AginOpen_Orders = new Hashtable();
            //    System.Data.DataTable dt_get_AginOpen_Orders = new System.Data.DataTable();

            //    ht_get_AginOpen_Orders.Add("@Trans", "AGING_PREVIEW_PENDING_ORDER_DATE_BANGLORE_WISE");
            //    ht_get_AginOpen_Orders.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_AginOpen_Orders.Add("@Todate", dateEdit_To_Date.Text);
            //    ht_get_AginOpen_Orders.Add("@Branch_ID", 3);
            //    dt_get_AginOpen_Orders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_AginOpen_Orders);

            //    pivotGridControlAginfPendingOrders.DataSource = dt_get_AginOpen_Orders;

            //}
            //// hosur branch wise
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Checked")
            //{
            //    Hashtable ht_get_Agin_Open_Orders = new Hashtable();
            //    System.Data.DataTable dt_get_Agin_Open_Orders = new System.Data.DataTable();

            //    ht_get_Agin_Open_Orders.Add("@Trans", "AGING_PREVIEW_PENDING_ORDER_DATE_AND_HOSUR_WISE");
            //    ht_get_Agin_Open_Orders.Add("@Fromdate", dateEdit_From_date.Text);
            //    ht_get_Agin_Open_Orders.Add("@Todate", dateEdit_To_Date.Text);
            //    ht_get_Agin_Open_Orders.Add("@Branch_ID", 5);
            //    dt_get_Agin_Open_Orders = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Agin_Open_Orders);

            //    pivotGridControlAginfPendingOrders.DataSource = dt_get_Agin_Open_Orders;

            //}
            ////none
            //else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
            //{
            //    pivotGridControlAginfPendingOrders.DataSource = null;

            //}

        }

        // manager wise
        private void Filter_AgingPreview_PendingOrders_Manager_Wise(int AgingUser_id_value)
        {
            //  load_Progressbar.Start_progres();
            StringBuilder sb_aging = new StringBuilder();
            Hashtable ht_AdminUserClients = new Hashtable();
            DataTable dt_AdminUserClients = new DataTable();

            ht_AdminUserClients.Add("@Trans", "GET_ADMIN_USER_CLIENTS");
            ht_AdminUserClients.Add("@Log_In_Userid", AgingUser_id_value);
            dt_AdminUserClients = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_AdminUserClients);

            if (dt_AdminUserClients.Rows.Count > 0)
            {
                for (int i = 0; i < dt_AdminUserClients.Rows.Count; i++)
                {
                    // My_Clients = My_Clients + dt_Admin_User_Clients.Rows[i]["Client_ID"].ToString();
                    //My_Clients += (i < dt_Admin_User_Clients.Rows.Count) ? "," : string.Empty;


                    My_Clients = dt_AdminUserClients.Rows[i]["Client_ID"].ToString();
                    // My_Clients = My_Client;
                    sb_aging = sb_aging.Append("," + My_Clients);
                    My_Clients = sb_aging.ToString();
                }

            }


            Hashtable ht_get_Aging_OPendOrders_Manager = new Hashtable();
            DataTable dt_get_Aging_OPendOrders_Manager = new DataTable();

            ht_get_Aging_OPendOrders_Manager.Add("@Trans", "AGING_PREVIEW_PENDING_ORDER_MANAGER_WISE");
            ht_get_Aging_OPendOrders_Manager.Add("@Fromdate", dateEdit_From_date.Text);
            ht_get_Aging_OPendOrders_Manager.Add("@Todate", dateEdit_To_Date.Text);
            //ht_get_Aging_OPendOrders_Manager.Add("@Reporting_To_Id_2", AgingUser_id_value);
            ht_get_Aging_OPendOrders_Manager.Add("@My_Clients", My_Clients);
            dt_get_Aging_OPendOrders_Manager = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Aging_OPendOrders_Manager);

            pivotGridControlAginfPendingOrders.DataSource = dt_get_Aging_OPendOrders_Manager;
        }



        private void lookUpEdit_Aging_Pending_Orders_Manager_Click(object sender, EventArgs e)
        {
            lookUpEdit_Aging_Pending_Orders_Manager.EditValue = null;
            lookUpEdit_Aging_Pending_Orders_Manager.EditValue = "SELECT";
            lookUpEdit_Aging_Pending_Orders_Manager.EditValue = 0;
        }

        private void lookUpEdit_Aging_Pending_Orders_Manager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Clear)
            {
                (sender as LookUpEdit).EditValue = null;
            }
        }

        // export
        private void btn_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                Export_All_PivotGrid();
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }
        // Export


        private void Export_All_PivotGrid()
        {
            try
            {

                //   PivotGridControl[] grids = new PivotGridControl[] { pivotGridControlDailyWise, pivotGridControlShiftWise, pivotGridControlProductTypeWise, pivotGridControlAgingOpenOrders, pivotGridControlAginfPendingOrders, pivotGridControlShiftWise2, pivotGridControlTopEfficiency };
                PrintingSystem ps = new PrintingSystem();
                CompositeLink compositeLink = new CompositeLink(ps);

                PrintableComponentLink link_1 = new PrintableComponentLink();
                PrintableComponentLink link_2 = new PrintableComponentLink();
                PrintableComponentLink link_3 = new PrintableComponentLink();
                PrintableComponentLink link_4 = new PrintableComponentLink();
                PrintableComponentLink link_5 = new PrintableComponentLink();
                PrintableComponentLink link_6 = new PrintableComponentLink();
                PrintableComponentLink link_7 = new PrintableComponentLink();
                PrintableComponentLink link_8 = new PrintableComponentLink();
                PrintableComponentLink link_capac = new PrintableComponentLink();
                PrintableComponentLink link_grid = new PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                chartControl1.Visible = true;
                link_1.Component = pivotGridControlDailyWise;
                link_1.PaperName = "Daily Status Report";
                link_3.Component = pivotGridControlProductTypeWise;
                link_4.Component = pivotGridControlAgingOpenOrders;
                link_5.Component = pivotGridControlAginfPendingOrders;
                //   link_6.Component = pivotGridControl6;
                link_8.Component = pivotGridControlTopEfficiency;
                link_capac.Component = chartControl1;
                link_grid.Component = Grd_Capcity_Utilization;

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);


                if (pivotGridControlShiftWise.Visible == true)
                {
                    pivotGridControlShiftWise.Visible = true;
                    pivotGridControlShiftWise2.Visible = false;
                    link_2.Component = pivotGridControlShiftWise;
                    compositeLink.Links.AddRange(new object[] { link_1, link_2, link_3, link_4, link_5, link_8, link_grid, link_capac });
                }
                else
                {
                    pivotGridControlShiftWise2.Visible = true;
                    pivotGridControlShiftWise.Visible = false;
                    link_7.Component = pivotGridControlShiftWise1;
                    compositeLink.Links.AddRange(new object[] { link_1, link_7, link_3, link_4, link_5, link_8, link_grid, link_capac });
                }
                compositeLink.PrintingSystem = ps;

                string ReportName = "Daily-Status-Report_For_All";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                // compositeLink.ShowPreview();
                compositeLink.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps.XlSheetCreated += PrintingSystem_XlSheetCreated;
                //  compositeLink.CreatePageForEachLink();

                compositeLink.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurs Please Check with Administrator");
            }
        }

        void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Daily-Wise-Order-Count";
            }
            else if (e.Index == 1)
            {
                e.SheetName = "Shift-Wise-Count";
            }
            else if (e.Index == 2)
            {
                e.SheetName = "Product-Type-Count";
            }
            else if (e.Index == 3)
            {
                e.SheetName = "Open-Orders";
            }
            else if (e.Index == 4)
            {
                e.SheetName = "Pending-Orders";
            }
            else if (e.Index == 5)
            {
                e.SheetName = "Top Efficiency";

            }
            else if (e.Index == 6)
            {
                e.SheetName = "Capacity-Utilzation-Count";

            }
            else if (e.Index == 7)
            {
                e.SheetName = "Capacity-Utilzation-Chart";
            }

        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //PivotGridControl[] grids = new PivotGridControl[] { pivotGridControl1, pivotGridControl2, pivotGridControl3, pivotGridControl6, pivotGridControl5, pivotGridControl7, pivotGridControl8 };
            //DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
            //DevExpress.XtraPrintingLinks.CompositeLink compositeLink = new DevExpress.XtraPrintingLinks.CompositeLink(ps);


            DevExpress.XtraPrinting.PrintingSystem ps1 = new DevExpress.XtraPrinting.PrintingSystem();

            ps1.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

            chartControl1.Visible = true;

            PrintableComponentLink link1 = new PrintableComponentLink(ps1);
            link1.Component = pivotGridControlDailyWise;

            PrintableComponentLink link2 = new PrintableComponentLink(ps1);
            link2.Component = pivotGridControlShiftWise1;

            PrintableComponentLink link7 = new PrintableComponentLink(ps1);
            link7.Component = pivotGridControlShiftWise2;

            PrintableComponentLink link3 = new PrintableComponentLink(ps1);
            link3.Component = pivotGridControlProductTypeWise;

            PrintableComponentLink link4 = new PrintableComponentLink(ps1);
            link4.Component = pivotGridControlAgingOpenOrders;

            PrintableComponentLink link5 = new PrintableComponentLink(ps1);
            link5.Component = pivotGridControlAginfPendingOrders;

            //PrintableComponentLink link6 = new PrintableComponentLink(ps1);
            //link6.Component = pivotGridControl6;

            PrintableComponentLink link8 = new PrintableComponentLink(ps1);
            link8.Component = pivotGridControlTopEfficiency;

            PrintableComponentLink link_chart = new PrintableComponentLink(ps1);
            link_chart.Component = chartControl1;

            CompositeLink compositeLink = new CompositeLink(ps1);

            // Make the "Export to Csv" and "Export to Txt" commands visible.
            ps1.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
            compositeLink.PrintingSystem = ps1;

            string ReportName = "Daily_Status_Report";
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";


            if (pivotGridControlShiftWise1.Visible == true)
            {
                pivotGridControlShiftWise1.Visible = true;
                pivotGridControlShiftWise2.Visible = false;
                compositeLink.Links.AddRange(new object[] { link1, link2, link3, link4, link5, link8, link_chart });
            }
            else
            {
                pivotGridControlShiftWise1.Visible = false;
                pivotGridControlShiftWise2.Visible = true;
                compositeLink.Links.AddRange(new object[] { link1, link7, link3, link4, link5, link8, link_chart });

            }

            compositeLink.CreatePageForEachLink();

            compositeLink.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
            System.Diagnostics.Process.Start(Path1);

            // var tempworkbook = new Workbook();
            // tempworkbook.LoadDocument(Path1, DocumentFormat.xlsx);
            //  WorksheetCollection worksheets = tempworkbook.Worksheets;
            //tempworkbook.Worksheets[0].Name = "RenamedSheet";
            //tempworkbook.Worksheets[1].Name = "RenamedSheet";



        }

        private void Export_PivotGrid_DailyWise()
        {
            DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart = new DevExpress.XtraPrinting.PrintableComponentLink();

            pivotGridControlDailyWise.Visible = true;
            pclChart.Component = pivotGridControlDailyWise;

            cl.PaperKind = System.Drawing.Printing.PaperKind.A3;
            cl.Landscape = true;
            cl.Margins.Right = 40;
            cl.Margins.Left = 40;
            cl.Margins.Top = 40;
            cl.Margins.Bottom = 40;
            cl.Links.AddRange(new object[] { pclChart });
            cl.ShowPreviewDialog();
            //  cl.ShowRibbonPreviewDialog(this);

        }

        private void Export_PivotGrid_ShiftWise()
        {
            DevExpress.XtraPrinting.PrintingSystem ps2 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl2 = new DevExpress.XtraPrintingLinks.CompositeLink(ps2);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart2 = new DevExpress.XtraPrinting.PrintableComponentLink();

            pivotGridControlShiftWise1.Visible = true;
            pclChart2.Component = pivotGridControlShiftWise1;

            cl2.PaperKind = System.Drawing.Printing.PaperKind.A3;
            cl2.Landscape = true;
            cl2.Margins.Right = 40;
            cl2.Margins.Left = 40;
            cl2.Margins.Top = 40;
            cl2.Margins.Bottom = 40;
            cl2.Links.AddRange(new object[] { pclChart2 });
            cl2.ShowPreviewDialog();
            //  cl.ShowRibbonPreviewDialog(this);

        }

        private void Export_PivotGrid_ProductTypeWise()
        {
            DevExpress.XtraPrinting.PrintingSystem ps3 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl3 = new DevExpress.XtraPrintingLinks.CompositeLink(ps3);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart3 = new DevExpress.XtraPrinting.PrintableComponentLink();

            pivotGridControlProductTypeWise.Visible = true;
            pclChart3.Component = pivotGridControlProductTypeWise;

            cl3.PaperKind = System.Drawing.Printing.PaperKind.A3;
            cl3.Landscape = true;
            cl3.Margins.Right = 40;
            cl3.Margins.Left = 40;
            cl3.Margins.Top = 40;
            cl3.Margins.Bottom = 40;
            cl3.Links.AddRange(new object[] { pclChart3 });
            cl3.ShowPreviewDialog();
            //  cl.ShowRibbonPreviewDialog(this);

        }

        private void Export_PivotGrid_AgingOpenOrders()
        {
            DevExpress.XtraPrinting.PrintingSystem ps4 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl4 = new DevExpress.XtraPrintingLinks.CompositeLink(ps4);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart4 = new DevExpress.XtraPrinting.PrintableComponentLink();

            //pivotGridControl6.Visible = true;
            // pclChart4.Component = pivotGridControl6;

            cl4.PaperKind = System.Drawing.Printing.PaperKind.A3;
            cl4.Landscape = true;
            cl4.Margins.Right = 40;
            cl4.Margins.Left = 40;
            cl4.Margins.Top = 40;
            cl4.Margins.Bottom = 40;
            cl4.Links.AddRange(new object[] { pclChart4 });
            cl4.ShowPreviewDialog();

        }

        private void Export_PivotGrid_Aging_Pending_Orders()
        {
            DevExpress.XtraPrinting.PrintingSystem ps5 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl5 = new DevExpress.XtraPrintingLinks.CompositeLink(ps5);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart5 = new DevExpress.XtraPrinting.PrintableComponentLink();

            pivotGridControlAginfPendingOrders.Visible = true;
            pclChart5.Component = pivotGridControlAginfPendingOrders;

            cl5.PaperKind = System.Drawing.Printing.PaperKind.A3;
            cl5.Landscape = true;
            cl5.Margins.Right = 40;
            cl5.Margins.Left = 40;
            cl5.Margins.Top = 40;
            cl5.Margins.Bottom = 40;
            cl5.Links.AddRange(new object[] { pclChart5 });
            cl5.ShowPreviewDialog();


        }

        private void btn_Aging_Pending_Orders_Click(object sender, EventArgs e)
        {
            lookUpEdit_Aging_Pending_Orders_Manager.EditValue = 0;
            // lookUpEdit_AgingPreview_Open_Orders.EditValue = "SELECT";
        }

        private void btn_AgingOpen_Orders_Click(object sender, EventArgs e)
        {
            lookUpEdit_AgingPreview_Open_Orders.EditValue = 0;
        }

        private bool IsNullValue(PivotGridField field)
        {
            if (field == null)
            {

                return false;
            }
            else
            {

                return true;
            }
        }

        private void pivotGridControl1_CellClick(object sender, PivotCellEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                DateTime date;
                string s_Date = "";
                DateTime dat;
                string Tab_Name = "";
                Tab_Name = "Daily Wise";
                string Operation = "";

                string value_Cda = "";
                string value_ClientNumber = "";
                string Client_Number = "";
                string Date = "";
                string Column_Name = "";

                string V_Data = "";
                string Row_Value_Type = "";
                string Column_Value_Type = "";
                PivotGridHitInfo hi = pivotGridControlDailyWise.CalcHitInfo(pivotGridControlDailyWise.PointToClient(MousePosition));
                if (hi.HitTest == PivotGridHitTest.Cell)
                {
                    // string ss = hi.HeadersAreaInfo.ToString();
                    Column_Name = hi.CellInfo.DataField.FieldName.ToString();
                    Row_Value_Type = hi.CellInfo.RowValueType.ToString();
                    Column_Value_Type = hi.CellInfo.ColumnValueType.ToString();

                    foreach (var field in pivotGridControlDailyWise.GetFieldsByArea(PivotArea.ColumnArea))
                    {
                        if (Column_Name == "Received")
                        {
                            V_Data = e.GetFieldValue(pivotGridField3).ToString();
                        }
                        if (Column_Name == "Completed")
                        {
                            V_Data = e.GetFieldValue(pivotGridField4).ToString();
                        }
                    }
                    // this is for Individual cell Click

                    foreach (var field in pivotGridControlDailyWise.GetFieldsByArea(PivotArea.RowArea))
                    {
                        if (Row_Value_Type != "GrandTotal") // This is Grand_Row_Count
                        {
                            string value1 = e.GetFieldValue(pivotGridField5).ToString();
                            value_Cda = value1;



                        }
                    }

                    foreach (var field in pivotGridControlDailyWise.GetFieldsByArea(PivotArea.ColumnArea))
                    {
                        if (Column_Value_Type != "GrandTotal")// this is Reading When Grand Column Count
                        {
                            string value1 = e.GetFieldValue(pivotGridField2).ToString();
                            value_ClientNumber = value1;
                        }
                    }
                    Client_Number = value_ClientNumber.ToString();
                    Date = value_Cda;


                    //input date string as dd-MM-yyyy HH:mm:ss format
                    // string date = "17-10-2016 20:00:00";
                    //dt will be DateTime type

                    if (Date != "")
                    {

                        DateTime date_new = Convert.ToDateTime(Date.ToString());


                        //date = DateTime.ParseExact(Date, "M/d/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        //s will be MM/dd/yyyy format string
                        s_Date = date_new.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        s_Date = "";
                    }


                    // d =Convert.ToDateTime(Date.ToString());


                    //string D1 = DateTime.Now.ToString("MM/dd/yyyy");
                    //dateTimePicker1.Value = DateTime.Now;


                    //txt_Fromdate.Format = DateTimePickerFormat.Custom;
                    //txt_Fromdate.CustomFormat = "MM/dd/yyyy";
                    //txt_Fromdate.Text = D1;


                    //Date. =Convert.ToString(DateTimePickerFormat.Custom);

                    //Date= D1;

                }

                // Get Completed Order Details

                //1
                if (Row_Value_Type == "Value" && Column_Value_Type == "Value")// This is for Non Summary Click Event
                {

                    if (V_Data != "" && V_Data != "0")
                    {
                        Hashtable ht_get_grid = new Hashtable();
                        DataTable dt_get_grid = new DataTable();
                        ht_get_grid.Clear();
                        dt_get_grid.Clear();
                        if (Column_Name == "Received")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_RECIVED_DETAILS");
                            Operation = "ORDER_VIEW_RECIVED_DETAILS";
                        }
                        else if (Column_Name == "Completed")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_COMPLETED_DETAILS");
                            Operation = "ORDER_VIEW_COMPLETED_DETAILS";

                        }
                        ht_get_grid.Add("@Client_Number", Client_Number);
                        ht_get_grid.Add("@date", s_Date);
                        ht_get_grid.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                        ht_get_grid.Add("@Todate", dateEditToAll.Text.ToString());
                        dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_grid);


                        HeaderText = $"{Tab_Name} - {Client_Number} - {Column_Name} Orders on {Convert.ToDateTime(Date).ToShortDateString()}";

                        //Daily_Status_Order_View_Detail form1 = new Daily_Status_Order_View_Detail(dt_get_grid, userroleid, User_id, Production_date);
                        //form1.Show();

                        DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_grid, userroleid, User_id, Production_date, dt_get_grid.Rows[0]["Client_Number"].ToString(), 0, Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, s_Date, Tab_Name, HeaderText);
                        form1.Show();
                    }
                    else
                    {

                        SplashScreenManager.CloseForm(false);
                    }
                }
                //2
                else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")// this is For Row Grand Total // this is for Single Client & All Date
                {
                    if (V_Data != "" && V_Data != "0")
                    {
                        Hashtable ht_get_grid = new Hashtable();
                        DataTable dt_get_grid = new DataTable();
                        ht_get_grid.Clear();
                        dt_get_grid.Clear();
                        if (Column_Name == "Received")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_RECIVED_GRAND_ROW_TOTAL_CLIENTWISE_ALL_DATE");
                            Operation = "ORDER_VIEW_RECIVED_GRAND_ROW_TOTAL_CLIENTWISE_ALL_DATE";
                        }
                        else if (Column_Name == "Completed")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_COMPLETED_GRAND_TOTAL_CLIENT_WISE_DATEWISE");
                            Operation = "ORDER_VIEW_COMPLETED_GRAND_TOTAL_CLIENT_WISE_DATEWISE";

                        }
                        ht_get_grid.Add("@Client_Number", Client_Number);
                        ht_get_grid.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                        ht_get_grid.Add("@Todate", dateEditToAll.Text.ToString());
                        dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_grid);


                        //Daily_Status_Order_View_Detail form1 = new Daily_Status_Order_View_Detail(dt_get_grid, userroleid, User_id, Production_date);
                        //form1.Show();
                        HeaderText = $"{Tab_Name} - {Client_Number} - All {Column_Name} Orders";
                        //  DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_grid, userroleid, User_id, Production_date);
                        DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_grid, userroleid, User_id, Production_date, dt_get_grid.Rows[0]["Client_Number"].ToString(), 0, Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                        form1.Show();
                    }
                    else
                    {
                        SplashScreenManager.CloseForm(false);
                    }

                }
                //3
                else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")// Single Date All Client Grand Total
                {
                    if (V_Data != "" && V_Data != "0")
                    {
                        Hashtable ht_get_grid = new Hashtable();
                        DataTable dt_get_grid = new DataTable();
                        ht_get_grid.Clear();
                        dt_get_grid.Clear();
                        if (Column_Name == "Received")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_RECIVED_DETAILS_GRAND_TOTAL_DATE_WISE");
                            Operation = "ORDER_VIEW_RECIVED_DETAILS_GRAND_TOTAL_DATE_WISE";
                        }
                        else if (Column_Name == "Completed")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_COMPLETED_GRAND_TOTAL_DATE_WISE");
                            Operation = "ORDER_VIEW_COMPLETED_GRAND_TOTAL_DATE_WISE";

                        }
                        //ht_get_grid.Add("@Client_Number", Client_Number);
                        ht_get_grid.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                        ht_get_grid.Add("@Todate", dateEditToAll.Text.ToString());
                        ht_get_grid.Add("@date", s_Date);
                        dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_grid);


                        //Daily_Status_Order_View_Detail form1 = new Daily_Status_Order_View_Detail(dt_get_grid, userroleid, User_id, Production_date);
                        //form1.Show();

                        HeaderText = $"{Tab_Name} - {Client_Number} All {Column_Name} Orders on {Convert.ToDateTime(Date).ToShortDateString()}";

                        //DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_grid, userroleid, User_id, Production_date);
                        DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_grid, userroleid, User_id, Production_date, "", 0, Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, Date, Tab_Name, HeaderText);

                        form1.Show();
                    }
                    else
                    {

                        SplashScreenManager.CloseForm(false);
                    }

                }
                //4
                else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal")// Single Date All Client Grand Total
                {

                    if (V_Data != "" && V_Data != "0")
                    {
                        Hashtable ht_get_grid = new Hashtable();
                        DataTable dt_get_grid = new DataTable();
                        ht_get_grid.Clear();
                        dt_get_grid.Clear();
                        if (Column_Name == "Received")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_RECIVED_DETAILS_GRAND_TOTAL_ALL");
                            Operation = "ORDER_VIEW_RECIVED_DETAILS_GRAND_TOTAL_ALL";
                        }
                        else if (Column_Name == "Completed")
                        {
                            ht_get_grid.Add("@Trans", "ORDER_VIEW_COMPLETED_GRAND_TOTAL_ALL");
                            Operation = "ORDER_VIEW_COMPLETED_GRAND_TOTAL_ALL";

                        }
                        //ht_get_grid.Add("@Client_Number", Client_Number);
                        ht_get_grid.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                        ht_get_grid.Add("@Todate", dateEditToAll.Text.ToString());
                        //ht_get_grid.Add("@date", Date);
                        dt_get_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_grid);


                        //Daily_Status_Order_View_Detail form1 = new Daily_Status_Order_View_Detail(dt_get_grid, userroleid, User_id, Production_date);
                        //form1.Show();

                        HeaderText = $"{Tab_Name} - All {Column_Name} Orders";
                        //DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_grid, userroleid, User_id, Production_date);
                        DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_grid, userroleid, User_id, Production_date, "", 0, Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                        form1.Show();
                    }
                    else
                    {

                        SplashScreenManager.CloseForm(false);
                    }

                }

            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }

        private void pivotGridControl3_CellClick(object sender, PivotCellEventArgs e)
        {
            // load_Progressbar.Start_progres();
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                string Tab_Name = "";
                Tab_Name = "Product Type Wise";
                string Operation = "";
                string val_order_Status = ""; string val_orderType_Abbr = ""; string val_Cda = "";
                string areaindex;
                string OrderType_Abbrevation = "";
                string Date = "";
                string Order_Status = "";
                string Column_Name = "";
                string V_Data = "";
                string Row_Value_Type = "";
                string Column_Value_Type = "";
                PivotGridHitInfo hit = pivotGridControlProductTypeWise.CalcHitInfo(pivotGridControlProductTypeWise.PointToClient(MousePosition));
                if (hit.HitTest == PivotGridHitTest.Cell)
                {
                    Column_Name = hit.CellInfo.DataField.FieldName.ToString();
                    Row_Value_Type = hit.CellInfo.RowValueType.ToString();
                    Column_Value_Type = hit.CellInfo.ColumnValueType.ToString();
                    areaindex = hit.CellInfo.ColumnIndex.ToString();

                    //1
                    if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
                    {
                        //
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField13).ToString();
                            val_Cda = value1;
                        }
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField16).ToString();   //Order_Type_Abbreviation
                            val_orderType_Abbr = value_2;
                            V_Data = e.GetFieldValue(pivotGridField14).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value3 = e.GetFieldValue(pivotGridField15).ToString();
                            val_order_Status = value3;
                        }

                        OrderType_Abbrevation = val_orderType_Abbr.ToString();
                        Order_Status = val_order_Status.ToString();
                        Date = val_Cda.ToString();

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report(OrderType_Abbrevation, Order_Status);

                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            OrderType_Abbrevation = dt_Get_Details.Rows[0][0].ToString();
                            // Order_Status = dt_Get_Details.Rows[1][0].ToString();
                            Order_Status = dt_Get_Details.Rows[1][0].ToString();
                        }
                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_details = new Hashtable();
                            DataTable dt_get_details = new DataTable();
                            ht_get_details.Clear();
                            dt_get_details.Clear();
                            ht_get_details.Add("@Trans", "ORDER_VIEW_PRODUT_WISE_TASK_WISE_DETAILS");
                            ht_get_details.Add("@Order_Type_Abrivation", OrderType_Abbrevation);
                            ht_get_details.Add("@Order_Status", Order_Status);
                            ht_get_details.Add("@date", Date);
                            dt_get_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_details);

                            Operation = "ORDER_VIEW_PRODUT_WISE_TASK_WISE_DETAILS";

                            //    DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date);

                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date, dt_get_details.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_details.Rows[0]["Subprocess_Id"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //2
                    else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")  // Row Wise Count for Product_Type Wise
                    {
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField16).ToString();
                            val_orderType_Abbr = value_2;
                            V_Data = e.GetFieldValue(pivotGridField14).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value3 = e.GetFieldValue(pivotGridField15).ToString();
                            val_order_Status = value3;
                        }

                        OrderType_Abbrevation = val_orderType_Abbr.ToString();
                        Order_Status = val_order_Status.ToString();
                        Date = val_Cda.ToString();

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report(OrderType_Abbrevation, Order_Status);

                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            OrderType_Abbrevation = dt_Get_Details.Rows[0][0].ToString();
                            // Order_Status = dt_Get_Details.Rows[1][0].ToString();
                            Order_Status = dt_Get_Details.Rows[1][0].ToString();
                        }

                        if (V_Data != "" && V_Data != "0")
                        {

                            Hashtable ht_get_details = new Hashtable();
                            DataTable dt_get_details = new DataTable();
                            ht_get_details.Clear();
                            dt_get_details.Clear();
                            //ht_get_grid_details.Add("@Trans", "GET_GRID_PRODUCT_TYPE_DETAILS"); 
                            ht_get_details.Add("@Trans", "ORDER_VIEW_PRODUT_GRAND_TOTAL_PRODUCT_WISE");
                            ht_get_details.Add("@Order_Type_Abrivation", OrderType_Abbrevation);
                            ht_get_details.Add("@Order_Status", Order_Status);
                            //ht_get_details.Add("@date", Date);
                            ht_get_details.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                            ht_get_details.Add("@Todate", dateEdit_To_Date.Text.ToString());
                            dt_get_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_details);

                            //Daily_Status_Order_View_Detail dailystatus = new Daily_Status_Order_View_Detail(dt_get_details, userroleid, User_id, Production_date);
                            //dailystatus.Show();

                            //  DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date, dt_get_details.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }


                    }
                    //3   task and date wise total for each task total
                    else if (Row_Value_Type == "Value" && Column_Value_Type == "Total")  // Row Wise Count for Task Wise & Date Wise
                    {
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField13).ToString();
                            val_Cda = value1;
                        }
                        V_Data = e.GetFieldValue(pivotGridField14).ToString().Trim();

                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value3 = e.GetFieldValue(pivotGridField15).ToString();
                            val_order_Status = value3;
                        }
                        // Order_Status = val_order_Status.ToString();
                        Date = val_Cda.ToString();


                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);

                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }


                        if (V_Data != "" && V_Data != "0")
                        {

                            Hashtable ht_get_details = new Hashtable();
                            DataTable dt_get_details = new DataTable();
                            ht_get_details.Clear();
                            dt_get_details.Clear();

                            ht_get_details.Add("@Trans", "ORDER_VIEW_PRODUT_WISE_TASK_WISE_DATE_WISE");
                            ht_get_details.Add("@Order_Status", Order_Status);
                            ht_get_details.Add("@date", Date);
                            //ht_get_details.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                            //ht_get_details.Add("@Todate", dateEdit_To_Date.Text.ToString());
                            dt_get_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_details);


                            // DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date, dt_get_details.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }

                    //4       
                    else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Total") // Row Wise - All Prouct Count
                    {

                        V_Data = e.GetFieldValue(pivotGridField14).ToString().Trim();
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value3 = e.GetFieldValue(pivotGridField15).ToString();
                            val_order_Status = value3;
                        }
                        //Order_Status = val_order_Status.ToString();
                        //Date = val_Cda.ToString();

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);

                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }
                        if (V_Data != "" && V_Data != "0")
                        {

                            Hashtable ht_get_details = new Hashtable();
                            DataTable dt_get_details = new DataTable();
                            ht_get_details.Clear();
                            dt_get_details.Clear();

                            ht_get_details.Add("@Trans", "ORDER_VIEW_PRODUT_GRAND_TOTAL_TASK_ALL_DATE_WISE_PRODUCT_WISE");
                            ht_get_details.Add("@Order_Status", Order_Status);
                            //ht_get_details.Add("@date", Date);
                            ht_get_details.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                            ht_get_details.Add("@Todate", dateEdit_To_Date.Text.ToString());
                            dt_get_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_details);


                            //DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date, dt_get_details.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //5
                    else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal") // Row Wise - All Prouct Count
                    {
                        V_Data = e.GetFieldValue(pivotGridField14).ToString().Trim();

                        if (V_Data != "" && V_Data != "0")
                        {

                            Hashtable ht_get_details = new Hashtable();
                            DataTable dt_get_details = new DataTable();
                            ht_get_details.Clear();
                            dt_get_details.Clear();
                            ht_get_details.Add("@Trans", "ORDER_VIEW_PRODUT_ALL_DATE_WISE");

                            //ht_get_details.Add("@date", Date);
                            ht_get_details.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                            ht_get_details.Add("@Todate", dateEdit_To_Date.Text.ToString());
                            dt_get_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_details);


                            // DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date, dt_get_details.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //6
                    else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal") // Row Wise - All Prouct Count
                    {
                        V_Data = e.GetFieldValue(pivotGridField14).ToString().Trim();
                        foreach (var field in pivotGridControlProductTypeWise.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField13).ToString();
                            val_Cda = value1;
                        }
                        if (V_Data != "" && V_Data != "0")
                        {

                            Hashtable ht_get_details = new Hashtable();
                            DataTable dt_get_details = new DataTable();
                            ht_get_details.Clear();
                            dt_get_details.Clear();
                            ht_get_details.Add("@Trans", "ORDER_VIEW_PRODUCT_TYPE_DATE_WISE");
                            ht_get_details.Add("@date", val_Cda);
                            //ht_get_details.Add("@Fromdate", dateEdit_From_date.Text.ToString());
                            //ht_get_details.Add("@Todate", dateEdit_To_Date.Text.ToString());
                            dt_get_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_details);


                            //DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_details, userroleid, User_id, Production_date, dt_get_details.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }



                }// if close
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }

        // Aging open order
        //private void pivotGridControl4_CellClick(object sender, PivotCellEventArgs e)
        //{
        //    string Client_Number = "";
        //    string Date = "";
        //    string Order_Status = "";
        //    PivotGridHitInfo hit = pivotGridControl4.CalcHitInfo(pivotGridControl4.PointToClient(MousePosition));

        //    //string order_Status = e.GetFieldValue(pivotGridControl4.Fields["Order_Status"], e.ColumnIndex).ToString();
        //    //string date = e.GetFieldValue(pivotGridControl4.Fields["Received_Date"], e.ColumnIndex).ToString();
        //    //string ClientNumber = e.GetFieldValue(pivotGridControl4.Fields["Client_Number"], e.ColumnIndex).ToString();


        //    //string order_Status = pivotGridControl4.GetFieldList["Order_Status"].ToString();

        //    if (hit.HitTest == PivotGridHitTest.Cell)
        //    {
        //        var order_Status = hit.CellInfo.GetFieldValue(pivotGridField21);
        //        var ClientNumber = hit.CellInfo.GetFieldValue(pivotGridField19);
        //        var date = hit.CellInfo.GetFieldValue(pivotGridField18);

        //        Client_Number = ClientNumber.ToString();
        //        Order_Status = order_Status.ToString();
        //        Date = date.ToString();
        //    }

        //    Hashtable ht_getgriddetails = new Hashtable();
        //    System.Data.DataTable dt_getgriddetails = new System.Data.DataTable();

        //    ht_getgriddetails.Add("@Trans", "GET_AGING_OPEN_ORDER_DETAILS_DATE_WISE_GRID_DETAILS");
        //    ht_getgriddetails.Add("@Client_Number", Client_Number);
        //    ht_getgriddetails.Add("@Order_Status", Order_Status);
        //    ht_getgriddetails.Add("@date", Date);
        //    dt_getgriddetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getgriddetails);

        //    Daily_Status_Order_View_Detail dailystatus = new Daily_Status_Order_View_Detail(dt_getgriddetails, userroleid);

        //    dailystatus.Show();
        //}

        private void pivotGridControl4_CellClick(object sender, PivotCellEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                string Tab_Name = "";
                Tab_Name = "Open Order Wise";
                string Operation = "";
                string Row_Value_Type = "";
                string Column_Value_Type = "";
                string value_Cda = "";
                string value_ClientNumber = "";
                string val_Order_Status = "";
                string Client_Number = "";
                string Date = "";
                string Order_Status = "";
                string V_Data = "";
                string Column_Name = "";

                PivotGridHitInfo hi = pivotGridControlAgingOpenOrders.CalcHitInfo(pivotGridControlAgingOpenOrders.PointToClient(MousePosition));
                if (hi.HitTest == PivotGridHitTest.Cell)
                {
                    Column_Name = hi.CellInfo.DataField.FieldName.ToString();
                    Row_Value_Type = hi.CellInfo.RowValueType.ToString();
                    Column_Value_Type = hi.CellInfo.ColumnValueType.ToString();

                    //1
                    Hashtable ht_Temp_Comment_Details = new Hashtable();
                    DataTable dt_Temp_Comment_details = new DataTable();

                    ht_Temp_Comment_Details.Clear();
                    ht_Temp_Comment_Details.Add("@Trans", "CREATE_TEMP_COMMENT_TABLE");
                    dt_Temp_Comment_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Temp_Comment_Details);



                    if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
                    {
                        // MessageBox.Show("Each Cell WIse");
                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField28).ToString();
                            value_Cda = value1;

                            V_Data = e.GetFieldValue(pivotGridField20).ToString().Trim();
                        }

                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField18).ToString();
                            value_ClientNumber = value_2;

                        }
                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField21).ToString();
                            val_Order_Status = value_2;
                        }


                        Client_Number = value_ClientNumber.ToString();
                        Date = value_Cda.ToString();


                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_Order_Status);

                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_getgrid_details = new Hashtable();
                            DataTable dt_getgrid_details = new DataTable();
                            ht_getgrid_details.Clear();
                            ht_getgrid_details.Clear();

                            ht_getgrid_details.Add("@Trans", "AGENT_OPEN_ORDER_DETAILS");
                            ht_getgrid_details.Add("@Client_Number", Client_Number);
                            ht_getgrid_details.Add("@Order_Status", Order_Status);
                            ht_getgrid_details.Add("@date", Date);
                            dt_getgrid_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Open", ht_getgrid_details);

                            HeaderText = $"{Tab_Name} - {Client_Number} - {val_Order_Status} Orders on {Date}";

                            Operation = "AGENT_OPEN_ORDER_DETAILS";

                            //DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_getgrid_details, userroleid, User_id, Production_date, dt_getgrid_details.Rows[0]["Client_Number"].ToString(), int.Parse(dt_getgrid_details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");

                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_getgrid_details, userroleid, User_id, Production_date, dt_getgrid_details.Rows[0]["Client_Number"].ToString(), int.Parse(dt_getgrid_details.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, Date, Tab_Name, HeaderText);


                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //2
                    else if (Row_Value_Type == "Total" && Column_Value_Type == "Value")// Client Wise -Task Wise Total
                    {

                        // MessageBox.Show("Client wise & Task Wise");

                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField18).ToString();
                            value_ClientNumber = value_2;

                        }
                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField21).ToString();
                            val_Order_Status = value_2;
                        }
                        V_Data = e.GetFieldValue(pivotGridField20).ToString().Trim();
                        Client_Number = value_ClientNumber.ToString();

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_Order_Status);

                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_getgriddetails = new Hashtable();
                            DataTable dt_getgriddetails = new DataTable();
                            ht_getgriddetails.Clear();
                            dt_getgriddetails.Clear();
                            ht_getgriddetails.Add("@Trans", "AGENT_OPEN_ORDER_CLIENT_AND_ORDER_STATUS_WISE");
                            ht_getgriddetails.Add("@Client_Number", Client_Number);
                            ht_getgriddetails.Add("@Order_Status", Order_Status);
                            dt_getgriddetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Open", ht_getgriddetails);

                            HeaderText = $"{Tab_Name} - {Client_Number} - All {val_Order_Status} Orders";

                            //DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_getgriddetails, userroleid, User_id, Production_date, dt_getgriddetails.Rows[0]["Client_Number"].ToString(), int.Parse(dt_getgriddetails.Rows[0]["Order_Status"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");
                            Operation = "AGENT_OPEN_ORDER_CLIENT_AND_ORDER_STATUS_WISE";
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_getgriddetails, userroleid, User_id, Production_date, dt_getgriddetails.Rows[0]["Client_Number"].ToString(), int.Parse(dt_getgriddetails.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //3
                    else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")// All Client and Task Wise
                    {


                        // MessageBox.Show("All Client TaskWise");
                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField21).ToString();
                            val_Order_Status = value_2;
                        }
                        V_Data = e.GetFieldValue(pivotGridField20).ToString().Trim();
                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_Order_Status);

                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_grid_details = new Hashtable();
                            DataTable dt_grid_details = new DataTable();
                            ht_grid_details.Clear();
                            dt_grid_details.Clear();
                            ht_grid_details.Add("@Trans", "AGENT_OPEN_ORDER_ALL_CLIENT_AND_ORDER_STATUS_WISE");
                            // ht_getgriddetails.Add("@Client_Number", Client_Number);
                            ht_grid_details.Add("@Order_Status", Order_Status);
                            dt_grid_details = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Open", ht_grid_details);



                            // DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_grid_details, userroleid, User_id, Production_date, dt_grid_details.Rows[0]["Client_Number"].ToString(), int.Parse(dt_grid_details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");
                            HeaderText = $"{Tab_Name} - All {val_Order_Status} Orders";

                            Operation = "AGENT_OPEN_ORDER_ALL_CLIENT_AND_ORDER_STATUS_WISE";
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_grid_details, userroleid, User_id, Production_date, dt_grid_details.Rows[0]["Client_Number"].ToString(), int.Parse(dt_grid_details.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);


                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //4
                    else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")// Client and Date Wise
                    {
                        // MessageBox.Show("Client and Date Wise Total");

                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField28).ToString();
                            value_Cda = value1;

                            V_Data = e.GetFieldValue(pivotGridField20).ToString().Trim();
                        }

                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField18).ToString();
                            value_ClientNumber = value_2;

                        }

                        V_Data = e.GetFieldValue(pivotGridField20).ToString().Trim();
                        Client_Number = value_ClientNumber.ToString();
                        Date = value_Cda.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Client_DateWise = new Hashtable();
                            DataTable dt_get_Client_DateWise = new DataTable();
                            ht_get_Client_DateWise.Clear();
                            dt_get_Client_DateWise.Clear();
                            ht_get_Client_DateWise.Add("@Trans", "AGENT_OPEN_ORDER_CLIENT_AND_DATE_WISE");
                            ht_get_Client_DateWise.Add("@Client_Number", Client_Number);
                            ht_get_Client_DateWise.Add("@date", Date);
                            dt_get_Client_DateWise = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Open", ht_get_Client_DateWise);



                            // DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_Client_DateWise, userroleid, User_id, Production_date, dt_get_Client_DateWise.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Client_DateWise.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");

                            HeaderText = $"{Tab_Name} - {Client_Number} - All Orders on {Date}";

                            Operation = "AGENT_OPEN_ORDER_CLIENT_AND_DATE_WISE";
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_Client_DateWise, userroleid, User_id, Production_date, dt_get_Client_DateWise.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Client_DateWise.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, Date, Tab_Name, HeaderText);

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //5
                    else if (Row_Value_Type == "Total" && Column_Value_Type == "GrandTotal")// Date Wise - Client Wise and All Task
                    {

                        // MessageBox.Show("Client Wise and All Task");


                        V_Data = e.GetFieldValue(pivotGridField20).ToString().Trim();
                        foreach (var field in pivotGridControlAgingOpenOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField18).ToString();
                            value_ClientNumber = value_2;

                        }
                        Client_Number = value_ClientNumber.ToString();
                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Date_All_Task_Wise = new Hashtable();
                            DataTable dt_get_Date_All_Task_Wise = new DataTable();
                            ht_get_Date_All_Task_Wise.Clear();
                            dt_get_Date_All_Task_Wise.Clear();
                            ht_get_Date_All_Task_Wise.Add("@Trans", "AGENT_OPEN_ORDER_CLIENT_AND_ALL_TASK_WISE");
                            ht_get_Date_All_Task_Wise.Add("@Client_Number", Client_Number);

                            dt_get_Date_All_Task_Wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Open", ht_get_Date_All_Task_Wise);



                            //DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_Date_All_Task_Wise, userroleid, User_id, Production_date, dt_get_Date_All_Task_Wise.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Date_All_Task_Wise.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");

                            HeaderText = $"{Tab_Name} - {Client_Number} - All Orders";

                            Operation = "AGENT_OPEN_ORDER_CLIENT_AND_ALL_TASK_WISE";
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_get_Date_All_Task_Wise, userroleid, User_id, Production_date, dt_get_Date_All_Task_Wise.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Date_All_Task_Wise.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    //7
                    else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal")// Date Wise - Client Wise All Task
                    {

                        //  MessageBox.Show("DATE WISE");
                        V_Data = e.GetFieldValue(pivotGridField20).ToString().Trim();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_AgingPending_detail = new Hashtable();
                            DataTable dt_AgingPending_detail = new DataTable();

                            ht_AgingPending_detail.Clear();
                            dt_AgingPending_detail.Clear();

                            ht_AgingPending_detail.Add("@Trans", "AGENT_OPEN_ORDER_DATE_WISE");
                            ht_AgingPending_detail.Add("@Fromdate", dateEdit_From_date.Text);
                            ht_AgingPending_detail.Add("@Todate", dateEdit_To_Date.Text);

                            dt_AgingPending_detail = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Open", ht_AgingPending_detail);

                            HeaderText = $"{Tab_Name} - All Orders";

                            // DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_AgingPending_detail, userroleid, User_id, Production_date, dt_AgingPending_detail.Rows[0]["Client_Number"].ToString(), int.Parse(dt_AgingPending_detail.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");



                            Operation = "AGENT_OPEN_ORDER_DATE_WISE";
                            DailyStatus_OrderViewDetail_New form1 = new DailyStatus_OrderViewDetail_New(dt_AgingPending_detail, userroleid, User_id, Production_date, dt_AgingPending_detail.Rows[0]["Client_Number"].ToString(), int.Parse(dt_AgingPending_detail.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                            form1.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }

        private void pivotGridControl5_CellClick(object sender, PivotCellEventArgs e)
        {
            //load_Progressbar.Start_progres();
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                string Tab_Name = "";
                Tab_Name = "Pending Order Wise";
                string Operation = "";
                string Date = "";
                string value_date_1 = ""; string value_Client_Num = ""; string value_Order_Status_1 = "";
                string Aging_Client_Number = "";
                string Aging_Date = "";
                string Aging_Order_Status = "";
                string V_Data = "";
                string Row_Value_Type = "";
                string Column_Value_Type = "";
                string Column_Name = "";
                // string Row_Name = "";
                PivotGridHitInfo hit = pivotGridControlAginfPendingOrders.CalcHitInfo(pivotGridControlAginfPendingOrders.PointToClient(MousePosition));

                if (hit.HitTest == PivotGridHitTest.Cell)
                {

                    Column_Name = hit.CellInfo.DataField.FieldName.ToString();
                    Row_Value_Type = hit.CellInfo.RowValueType.ToString();
                    Column_Value_Type = hit.CellInfo.ColumnValueType.ToString();

                    if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
                    {

                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField23).ToString();
                            value_date_1 = value_2;
                        }
                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField29).ToString();
                            value_Client_Num = value_3;
                            V_Data = e.GetFieldValue(pivotGridField25).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_4 = e.GetFieldValue(pivotGridField26).ToString();
                            value_Order_Status_1 = value_4;
                        }

                        DataTable dt_Get_Details = new DataTable();

                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
                        string Order_Status = "";
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }


                        Aging_Client_Number = value_Client_Num.ToString();
                        Aging_Order_Status = Order_Status.ToString();
                        Aging_Date = value_date_1;

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Aging_Pending_griddetails = new Hashtable();
                            DataTable dt_get_Aging_Pending_griddetails = new DataTable();
                            ht_get_Aging_Pending_griddetails.Clear();
                            dt_get_Aging_Pending_griddetails.Clear();

                            ht_get_Aging_Pending_griddetails.Add("@Trans", "AGENT_PENDING_ORDER_DETAILS");
                            ht_get_Aging_Pending_griddetails.Add("@Client_Number", Aging_Client_Number);
                            ht_get_Aging_Pending_griddetails.Add("@Order_Status", Aging_Order_Status);
                            ht_get_Aging_Pending_griddetails.Add("@date", Aging_Date);
                            dt_get_Aging_Pending_griddetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Pending", ht_get_Aging_Pending_griddetails);

                            // DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_get_Aging_Pending_griddetails, userroleid, User_id, Production_date);

                            HeaderText = $"{Tab_Name} - {Aging_Client_Number} - {value_Order_Status_1} Orders on {Aging_Date}";

                            Operation = "AGENT_PENDING_ORDER_DETAILS";
                            DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_get_Aging_Pending_griddetails, userroleid, User_id, Production_date, dt_get_Aging_Pending_griddetails.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Aging_Pending_griddetails.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, Aging_Date, Tab_Name, HeaderText);

                            dailystatus_Aging_pending.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //2
                    else if (Row_Value_Type == "Total" && Column_Value_Type == "Value")// Client Wise -Task Wise Total
                    {

                        //MessageBox.Show("Client Wise & Task Wise");

                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField29).ToString();
                            value_Client_Num = value_3;
                            V_Data = e.GetFieldValue(pivotGridField25).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_4 = e.GetFieldValue(pivotGridField26).ToString();
                            value_Order_Status_1 = value_4;
                        }

                        DataTable dt_Get_Details = new DataTable();

                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
                        string Order_Status = "";
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }


                        Aging_Client_Number = value_Client_Num.ToString();
                        Aging_Order_Status = Order_Status.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Aging_Pending_grid = new Hashtable();
                            DataTable dt_get_Aging_Pending_grid = new DataTable();

                            ht_get_Aging_Pending_grid.Clear();
                            dt_get_Aging_Pending_grid.Clear();

                            ht_get_Aging_Pending_grid.Add("@Trans", "AGENT_PENDING_ORDER_CLIENT_AND_STATUS");
                            ht_get_Aging_Pending_grid.Add("@Client_Number", Aging_Client_Number);
                            ht_get_Aging_Pending_grid.Add("@Order_Status", Aging_Order_Status);
                            //ht_get_Aging_Pending_grid.Add("@date", Aging_Date);
                            dt_get_Aging_Pending_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Pending", ht_get_Aging_Pending_grid);


                            //DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_get_Aging_Pending_grid, userroleid, User_id, Production_date, dt_get_Aging_Pending_grid.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Aging_Pending_grid.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");
                            HeaderText = $"{Tab_Name} - {Aging_Client_Number} - All {value_Order_Status_1} Orders";

                            Operation = "AGENT_PENDING_ORDER_CLIENT_AND_STATUS";
                            DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_get_Aging_Pending_grid, userroleid, User_id, Production_date, dt_get_Aging_Pending_grid.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Aging_Pending_grid.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                            dailystatus_Aging_pending.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //3
                    else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")// All Client and Task Wise
                    {
                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value_4 = e.GetFieldValue(pivotGridField26).ToString();
                            value_Order_Status_1 = value_4;
                        }
                        V_Data = e.GetFieldValue(pivotGridField25).ToString().Trim();
                        DataTable dt_Get_Details = new DataTable();

                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
                        string Order_Status = "";
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Aging_Order_Status = Order_Status.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Aging_Pending = new Hashtable();
                            DataTable dt_get_Aging_Pending = new DataTable();

                            ht_get_Aging_Pending.Clear();
                            dt_get_Aging_Pending.Clear();

                            ht_get_Aging_Pending.Add("@Trans", "AGENT_PENDING_ORDER_ALL_CLIENT_STATUS_WISE");
                            ht_get_Aging_Pending.Add("@Order_Status", Aging_Order_Status);
                            dt_get_Aging_Pending = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Pending", ht_get_Aging_Pending);

                            //DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_get_Aging_Pending, userroleid, User_id, Production_date, dt_get_Aging_Pending.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Aging_Pending.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");

                            HeaderText = $"{Tab_Name} - All {value_Order_Status_1} Orders";

                            Operation = "AGENT_PENDING_ORDER_ALL_CLIENT_STATUS_WISE";
                            DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_get_Aging_Pending, userroleid, User_id, Production_date, dt_get_Aging_Pending.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Aging_Pending.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                            dailystatus_Aging_pending.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //4
                    else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")// Client and Date Wise
                    {
                        //MessageBox.Show("Client and Date Wise Total");
                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_2 = e.GetFieldValue(pivotGridField23).ToString();
                            value_date_1 = value_2;
                        }
                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField29).ToString();
                            value_Client_Num = value_3;
                            V_Data = e.GetFieldValue(pivotGridField25).ToString().Trim();
                        }

                        DataTable dt_Get_Details = new DataTable();

                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
                        string Order_Status = "";
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }


                        Aging_Client_Number = value_Client_Num.ToString();
                        Aging_Order_Status = Order_Status.ToString();
                        Aging_Date = value_date_1;

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_Aging_Pending_Details = new Hashtable();
                            DataTable dt_Aging_Pending_Details = new DataTable();

                            ht_Aging_Pending_Details.Clear();
                            dt_Aging_Pending_Details.Clear();

                            ht_Aging_Pending_Details.Add("@Trans", "AGENT_PENDING_ORDER_CLIENT_DATE_WISE");
                            ht_Aging_Pending_Details.Add("@Client_Number", Aging_Client_Number);
                            ht_Aging_Pending_Details.Add("@date", Aging_Date);
                            dt_Aging_Pending_Details = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Pending", ht_Aging_Pending_Details);

                            HeaderText = $"{Tab_Name} - {Aging_Client_Number} - All Orders on {Aging_Date}";

                            // DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_Aging_Pending_Details, userroleid, User_id, Production_date, dt_Aging_Pending_Details.Rows[0]["Client_Number"].ToString(), int.Parse(dt_Aging_Pending_Details.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");

                            Operation = "AGENT_PENDING_ORDER_CLIENT_DATE_WISE";
                            DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_Aging_Pending_Details, userroleid, User_id, Production_date, dt_Aging_Pending_Details.Rows[0]["Client_Number"].ToString(), int.Parse(dt_Aging_Pending_Details.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, Aging_Date, Tab_Name, HeaderText);

                            dailystatus_Aging_pending.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    //5 correct
                    else if (Row_Value_Type == "Total" && Column_Value_Type == "GrandTotal")// Date Wise - Client Wise All Task
                    {

                        foreach (var field in pivotGridControlAginfPendingOrders.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField29).ToString();
                            value_Client_Num = value_3;
                            V_Data = e.GetFieldValue(pivotGridField25).ToString().Trim();
                        }

                        Aging_Client_Number = value_Client_Num.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_AgingPending = new Hashtable();
                            DataTable dt_AgingPending = new DataTable();

                            ht_AgingPending.Clear();
                            dt_AgingPending.Clear();

                            ht_AgingPending.Add("@Trans", "AGENT_PENDING_ORDER_CLIENT_AND_ALL_STATUS_WISE");
                            ht_AgingPending.Add("@Client_Number", Aging_Client_Number);
                            dt_AgingPending = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Pending", ht_AgingPending);


                            //  DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_AgingPending, userroleid, User_id, Production_date, dt_AgingPending.Rows[0]["Client_Number"].ToString(), int.Parse(dt_AgingPending.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");

                            HeaderText = $"{Tab_Name} - {Aging_Client_Number} - All Orders";
                            Operation = "AGENT_PENDING_ORDER_CLIENT_AND_ALL_STATUS_WISE";
                            DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_AgingPending, userroleid, User_id, Production_date, dt_AgingPending.Rows[0]["Client_Number"].ToString(), int.Parse(dt_AgingPending.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                            dailystatus_Aging_pending.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //6
                    //else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")// Date Wise - Client Wise All Task
                    //{

                    //    MessageBox.Show("Client Wise and All Task");
                    //    foreach (var field in pivotGridControl5.GetFieldsByArea(PivotArea.RowArea))
                    //    {
                    //        string value_2 = e.GetFieldValue(pivotGridField23).ToString();
                    //        value_date_1 = value_2;
                    //    }
                    //    foreach (var field in pivotGridControl5.GetFieldsByArea(PivotArea.RowArea))
                    //    {
                    //        string value_3 = e.GetFieldValue(pivotGridField29).ToString();
                    //        value_Client_Num = value_3;
                    //        V_Data = e.GetFieldValue(pivotGridField25).ToString().Trim();
                    //    }
                    //    //foreach (var field in pivotGridControl5.GetFieldsByArea(PivotArea.ColumnArea))
                    //    //{
                    //    //    string value_4 = e.GetFieldValue(pivotGridField26).ToString();
                    //    //    value_Order_Status_1 = value_4;
                    //    //}
                    //    //System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

                    //    //dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
                    //    //string Order_Status = "";
                    //    //if (dt_Get_Details.Rows.Count > 0)
                    //    //{
                    //    //    Order_Status = dt_Get_Details.Rows[0][0].ToString();
                    //    //}


                    //    Aging_Client_Number = value_Client_Num.ToString();
                    //    //Aging_Order_Status = Order_Status.ToString();
                    //    Aging_Date = value_date_1;

                    //    if (V_Data != "" && V_Data != "0")
                    //    {
                    //        Hashtable ht_AgingPending_detail = new Hashtable();
                    //        System.Data.DataTable dt_AgingPending_detail = new System.Data.DataTable();

                    //        ht_AgingPending_detail.Clear();
                    //        dt_AgingPending_detail.Clear();

                    //        ht_AgingPending_detail.Add("@Trans", "AGENT_PENDING_ORDER_CLIENT_DATE_WISE");
                    //        ht_AgingPending_detail.Add("@Client_Number", Aging_Client_Number);
                    //        ht_AgingPending_detail.Add("@date", Aging_Date);


                    //        //ht_AgingPending_detail.Add("@Order_Status", Aging_Order_Status);

                    //        dt_AgingPending_detail = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_AgingPending_detail);

                    //        Daily_Status_Order_View_Detail dailystatus_Aging_pending = new Daily_Status_Order_View_Detail(dt_AgingPending_detail, userroleid, User_id, Production_date);

                    //        dailystatus_Aging_pending.Show();
                    //    }
                    //    else
                    //    {
                    //        SplashScreenManager.CloseForm(false);
                    //    }
                    //}

                    // 
                    //7
                    else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal")// Date Wise - Client Wise All Task
                    {


                        V_Data = e.GetFieldValue(pivotGridField25).ToString().Trim();


                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_AgingPending_detail = new Hashtable();
                            DataTable dt_AgingPending_detail = new DataTable();

                            ht_AgingPending_detail.Clear();
                            dt_AgingPending_detail.Clear();




                            ht_AgingPending_detail.Add("@Trans", "AGING_PENDING_ORDER_DATE_WISE");
                            ht_AgingPending_detail.Add("@Fromdate", dateEdit_From_date.Text);
                            ht_AgingPending_detail.Add("@Todate", dateEdit_To_Date.Text);
                            dt_AgingPending_detail = dataaccess.ExecuteSP("Sp_Daily_Status_Report_Pending", ht_AgingPending_detail);


                            //DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_AgingPending_detail, userroleid, User_id, Production_date, dt_AgingPending_detail.Rows[0]["Client_Number"].ToString(), int.Parse(dt_AgingPending_detail.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text,"");

                            HeaderText = $"{Tab_Name} - All Orders";

                            Operation = "AGING_PENDING_ORDER_DATE_WISE";
                            DailyStatus_OrderViewDetail_New dailystatus_Aging_pending = new DailyStatus_OrderViewDetail_New(dt_AgingPending_detail, userroleid, User_id, Production_date, dt_AgingPending_detail.Rows[0]["Client_Number"].ToString(), int.Parse(dt_AgingPending_detail.Rows[0]["Order_Status_ID"].ToString()), Operation, dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);

                            dailystatus_Aging_pending.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //Filter_AgingPreview_PendingOrders_Date_Wise();
                }
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        public bool isProcessRunning { get; set; }

        private void pivotGridControl2_CellClick(object sender, PivotCellEventArgs e)
        {
            // load_Progressbar.Start_progres();
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                string Tab_Name = "";
                Tab_Name = "Shift Wise";
                string Column_Name = "";
                string val_order_Status = ""; string val_Cda = ""; string val_Shift_Type_Name = ""; string val_Emp_Job_Role = ""; string Val_Branch_Name = "";
                string Row_Value_Type = ""; string Column_Value_Type = "";
                string Shift_Type_Name = "";
                string Emp_Job_Role = "";
                string Date = "";
                string Order_Status = "";
                string V_Data = "";
                //string value_empjobrole = "";
                PivotGridHitInfo hit = pivotGridControlShiftWise1.CalcHitInfo(pivotGridControlShiftWise1.PointToClient(MousePosition));
                if (hit.HitTest == PivotGridHitTest.Cell)
                {
                    //string jobrole_count = Convert.ToString(hit.ValueInfo.Value);

                    //if (jobrole_count!="")
                    //{

                    //}

                    //
                    Column_Name = hit.CellInfo.DataField.FieldName.ToString();

                    Row_Value_Type = hit.CellInfo.RowValueType.ToString();
                    Column_Value_Type = hit.CellInfo.ColumnValueType.ToString();


                    if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
                    {

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField8).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField9).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value2 = e.GetFieldValue(pivotGridField11).ToString();
                            val_order_Status = value2;
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField7).ToString();
                            val_Shift_Type_Name = value_3;
                        }

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_4 = e.GetFieldValue(pivotGridField17).ToString();
                            Val_Branch_Name = value_4;
                        }

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField10) != null)
                            {
                                var value_4 = e.GetFieldValue(pivotGridField10).ToString();
                                val_Emp_Job_Role = value_4;
                            }
                            //var value_4 = e.GetFieldValue(pivotGridField10).ToString();
                            else
                            {
                                val_Emp_Job_Role = null;
                            }
                        }
                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            // OrderType_Abbrevation = dt_Get_Details.Rows[0][0].ToString();
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();


                        if (V_Data != "" && V_Data != "0" && val_Emp_Job_Role != null)
                        {
                            Emp_Job_Role = val_Emp_Job_Role.ToString();
                            Hashtable ht_get_Shiftdetails = new Hashtable();
                            DataTable dt_get_Shiftdetails = new DataTable();
                            ht_get_Shiftdetails.Clear();
                            dt_get_Shiftdetails.Clear();

                            ht_get_Shiftdetails.Add("@Trans", "SHIFT_WISE_BY_ORDER_DETIALS_VIEW_SINGLE_DAY");
                            ht_get_Shiftdetails.Add("@Order_Status", Order_Status);
                            ht_get_Shiftdetails.Add("@date", Date);
                            ht_get_Shiftdetails.Add("@Branch", Val_Branch_Name);
                            ht_get_Shiftdetails.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shiftdetails.Add("@Emp_Job_Role", Emp_Job_Role);
                            dt_get_Shiftdetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shiftdetails);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shiftdetails, userroleid, User_id, Production_date);

                            //shift_deatils.Show();


                            //  DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date, dt_get_Shiftdetails.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shiftdetails.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");


                            shift_deatils.Show();


                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }




                    }
                    //2
                    if (Row_Value_Type == "Total" && Column_Value_Type == "Value")// Shift & Task Wise  
                    {
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField8).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField9).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value2 = e.GetFieldValue(pivotGridField11).ToString();
                            val_order_Status = value2;
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField7).ToString();
                            val_Shift_Type_Name = value_3;
                        }

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField10) != null)
                            {
                                var value_4 = e.GetFieldValue(pivotGridField10).ToString();
                                val_Emp_Job_Role = value_4;
                            }
                            //var value_4 = e.GetFieldValue(pivotGridField10).ToString();
                            else
                            {
                                //val_Emp_Job_Role = null;
                            }

                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            Val_Branch_Name = "";
                            string value_4 = e.GetFieldValue(pivotGridField17).ToString();
                            Val_Branch_Name = value_4;
                        }

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            // OrderType_Abbrevation = dt_Get_Details.Rows[0][0].ToString();
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shiftdetails = new Hashtable();
                            DataTable dt_get_Shiftdetails = new DataTable();
                            ht_get_Shiftdetails.Clear();
                            dt_get_Shiftdetails.Clear();
                            ht_get_Shiftdetails.Add("@Trans", "SHIFT_WISE_BY_ORDER_DETIALS_VIEW_SHIFT_WISE_SINGLE_DAY");
                            ht_get_Shiftdetails.Add("@Order_Status", Order_Status);
                            ht_get_Shiftdetails.Add("@date", Date);
                            ht_get_Shiftdetails.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shiftdetails.Add("@Branch", Val_Branch_Name);
                            dt_get_Shiftdetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shiftdetails);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shiftdetails, userroleid, User_id, Production_date);
                            //shift_deatils.Show();


                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date, dt_get_Shiftdetails.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shiftdetails.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");


                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }




                        //    MessageBox.Show("Shiftwise");

                    }
                    //3
                    if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")//All Shift and Task Wise  
                    {

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField8).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField9).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value2 = e.GetFieldValue(pivotGridField11).ToString();
                            val_order_Status = value2;
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField7) != null)
                            {
                                string value_3 = e.GetFieldValue(pivotGridField7).ToString();
                                val_Shift_Type_Name = value_3;
                            }
                        }

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            Val_Branch_Name = "";
                            string value_4 = e.GetFieldValue(pivotGridField17).ToString();
                            Val_Branch_Name = value_4;
                        }

                        //foreach (var field in pivotGridControl2.GetFieldsByArea(PivotArea.RowArea))
                        //{
                        //    if (e.GetFieldValue(pivotGridField10) != null)
                        //    {
                        //        var value_4 = e.GetFieldValue(pivotGridField10).ToString();
                        //        val_Emp_Job_Role = value_4;
                        //    }
                        //    //var value_4 = e.GetFieldValue(pivotGridField10).ToString();
                        //    else
                        //    {
                        //        //val_Emp_Job_Role = null;
                        //    }

                        //}
                        //foreach (var field in pivotGridControl2.GetFieldsByArea(PivotArea.RowArea))
                        //{
                        //    Val_Branch_Name = "";
                        //    string value_4 = e.GetFieldValue(pivotGridField17).ToString();
                        //    Val_Branch_Name = value_4;
                        //}

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            // OrderType_Abbrevation = dt_Get_Details.Rows[0][0].ToString();
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shiftdetails = new Hashtable();
                            DataTable dt_get_Shiftdetails = new DataTable();
                            ht_get_Shiftdetails.Clear();
                            dt_get_Shiftdetails.Clear();

                            ht_get_Shiftdetails.Add("@Trans", "ALL_SHIFT_WISE_SINGLE_DAY");
                            ht_get_Shiftdetails.Add("@Order_Status", Order_Status);
                            ht_get_Shiftdetails.Add("@date", Date);
                            ht_get_Shiftdetails.Add("@Branch", Val_Branch_Name);
                            dt_get_Shiftdetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shiftdetails);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shiftdetails, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date, dt_get_Shiftdetails.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shiftdetails.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }

                    }

                    //4
                    if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal")// All Date and All Task and All Shift Wise Total
                    {

                        //// MessageBox.Show("All Total");    // pass current date only parameter


                        V_Data = e.GetFieldValue(pivotGridField9).ToString().Trim();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift_Current_date_wise = new Hashtable();
                            DataTable dt_get_Shift_Current_date_wise = new DataTable();
                            ht_get_Shift_Current_date_wise.Clear();
                            dt_get_Shift_Current_date_wise.Clear();

                            ht_get_Shift_Current_date_wise.Add("@Trans", "SHIFT_CURRENTDATE_WISE");
                            ht_get_Shift_Current_date_wise.Add("@date", dateEditShiftWiseCurrent.Text);

                            dt_get_Shift_Current_date_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift_Current_date_wise);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date, dt_get_Shift_Current_date_wise.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift_Current_date_wise.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }


                    }

                    //5
                    if (Row_Value_Type == "Total" && Column_Value_Type == "GrandTotal")// shift and Date Wise All Task Total
                    {

                        //MessageBox.Show("Shift Wise and Date Wise All Task Total");

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField7).ToString();
                            val_Shift_Type_Name = value_3;
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            Val_Branch_Name = "";
                            string value_4 = e.GetFieldValue(pivotGridField17).ToString();
                            Val_Branch_Name = value_4;
                        }

                        V_Data = e.GetFieldValue(pivotGridField9).ToString().Trim();


                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift_Current_date_wise = new Hashtable();
                            DataTable dt_get_Shift_Current_date_wise = new DataTable();
                            ht_get_Shift_Current_date_wise.Clear();
                            dt_get_Shift_Current_date_wise.Clear();

                            ht_get_Shift_Current_date_wise.Add("@Trans", "CURRENT_DATE_AND_SHIFT_TYPE_WISE");
                            ht_get_Shift_Current_date_wise.Add("@date", dateEditShiftWiseCurrent.Text);
                            ht_get_Shift_Current_date_wise.Add("@Shift_Type_Name", val_Shift_Type_Name);
                            ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                            dt_get_Shift_Current_date_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift_Current_date_wise);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            //DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date, dt_get_Shift_Current_date_wise.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift_Current_date_wise.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //6
                    if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")  // shift and Date Wise ,Emp_Job_Role - Client Wise All Task
                    {
                        V_Data = e.GetFieldValue(pivotGridField9).ToString().Trim();

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField10) != null)
                            {
                                string value_3 = e.GetFieldValue(pivotGridField7).ToString();
                                val_Shift_Type_Name = value_3;
                            }
                        }

                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField10) != null)
                            {
                                var value_4 = e.GetFieldValue(pivotGridField10).ToString();
                                val_Emp_Job_Role = value_4;
                            }
                        }
                        foreach (var field in pivotGridControlShiftWise1.GetFieldsByArea(PivotArea.RowArea))
                        {
                            Val_Branch_Name = "";
                            var Branch_value = e.GetFieldValue(pivotGridField17).ToString();
                            Val_Branch_Name = Branch_value;
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_getdetail = new Hashtable();
                            DataTable dt_getdetail = new DataTable();
                            //ht_get.Clear();
                            //dt_get.Clear();

                            ht_getdetail.Add("@Trans", "CURRENT_DATE_AND_SHIFT_TYPE_AND_EMP_JOB_ROLE_WISE");
                            ht_getdetail.Add("@date", dateEditShiftWiseCurrent.Text);
                            ht_getdetail.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_getdetail.Add("@Emp_Job_Role", Emp_Job_Role);

                            ht_getdetail.Add("@Branch", Val_Branch_Name);
                            dt_getdetail = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getdetail);


                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_getdetail, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_getdetail, userroleid, User_id, Production_date, dt_getdetail.Rows[0]["Client_Number"].ToString(), int.Parse(dt_getdetail.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }


                }

            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }

        private void pivotGridControl7_CellClick(object sender, PivotCellEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                string Check_Branch_All = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
                string Check_Banglore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
                string Check_Hosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();
                string Val_Branch_Name_1 = "";
                string checked_Item = "";

                if (Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[0].Description.ToString();
                }
                else if (Check_Banglore == "Checked" && Check_Branch_All == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[1].Description.ToString();
                }
                else if (Check_Hosur == "Checked" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[2].Description.ToString();
                }

                Val_Branch_Name_1 = checked_Item;



                string Tab_Name = "";
                Tab_Name = "Shift Wise";
                string Column_Name = "";
                string val_order_Status = ""; string val_Cda = ""; string val_Shift_Type_Name = ""; string val_Emp_Job_Role = "";
                string Row_Value_Type = ""; string Column_Value_Type = "";
                string Shift_Type_Name = "";
                string Emp_Job_Role = "";
                string Date = "";
                string Order_Status = "";
                string V_Data = "";
                //string value_empjobrole = "";
                PivotGridHitInfo hit = pivotGridControlShiftWise2.CalcHitInfo(pivotGridControlShiftWise2.PointToClient(MousePosition));
                if (hit.HitTest == PivotGridHitTest.Cell)
                {
                    Column_Name = hit.CellInfo.DataField.FieldName.ToString();

                    Row_Value_Type = hit.CellInfo.RowValueType.ToString();
                    Column_Value_Type = hit.CellInfo.ColumnValueType.ToString();

                    //1
                    if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
                    {

                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField39).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value2 = e.GetFieldValue(pivotGridField38).ToString();
                            val_order_Status = value2;
                        }
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField40).ToString();
                            val_Shift_Type_Name = value_3;
                        }

                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField41) != null)
                            {
                                var value_4 = e.GetFieldValue(pivotGridField41).ToString();
                                val_Emp_Job_Role = value_4;
                            }
                            else
                            {
                                val_Emp_Job_Role = null;
                            }

                        }
                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift1 = new Hashtable();
                            DataTable dt_get_Shift1 = new DataTable();
                            ht_get_Shift1.Clear();
                            dt_get_Shift1.Clear();

                            ht_get_Shift1.Add("@Trans", "SHIFT_TYPE_STATUS_DATE_EMPJOBTOLE_SHIFT_WISE");
                            ht_get_Shift1.Add("@Order_Status", Order_Status);
                            ht_get_Shift1.Add("@date", Date);
                            ht_get_Shift1.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shift1.Add("@Emp_Job_Role", Emp_Job_Role);
                            dt_get_Shift1 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift1);


                            //DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift1, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift1, userroleid, User_id, Production_date, dt_get_Shift1.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift1.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();

                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }

                    //2 pass shift , empjobrole and date and all order status
                    if (Row_Value_Type == "Value" && Column_Value_Type == "Total")
                    {
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField39).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        }

                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField40).ToString();
                            val_Shift_Type_Name = value_3;
                        }

                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField41) != null)
                            {
                                var value_4 = e.GetFieldValue(pivotGridField41).ToString();
                                val_Emp_Job_Role = value_4;
                            }
                            else
                            {
                                val_Emp_Job_Role = null;
                            }

                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift2 = new Hashtable();
                            DataTable dt_get_Shift2 = new DataTable();
                            ht_get_Shift2.Clear();
                            dt_get_Shift2.Clear();

                            ht_get_Shift2.Add("@Trans", "SHIFT_TYPE_ALL_ORDER_STATUS_WISE");
                            ht_get_Shift2.Add("@date", Date);
                            ht_get_Shift2.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shift2.Add("@Emp_Job_Role", Emp_Job_Role);
                            dt_get_Shift2 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift2);


                            //DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift2, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift2, userroleid, User_id, Production_date, dt_get_Shift2.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift2.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    //3 pass shift ,  date and single order status
                    if (Row_Value_Type == "Total" && Column_Value_Type == "Value")
                    {
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField39).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value2 = e.GetFieldValue(pivotGridField38).ToString();
                            val_order_Status = value2;
                        }
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField40).ToString();
                            val_Shift_Type_Name = value_3;
                        }

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift3 = new Hashtable();
                            DataTable dt_get_Shift3 = new DataTable();
                            ht_get_Shift3.Clear();
                            dt_get_Shift3.Clear();

                            ht_get_Shift3.Add("@Trans", "SHIFT_TYPE_SINGLE_ORDER_STATUS_WISE");
                            ht_get_Shift3.Add("@date", Date);
                            ht_get_Shift3.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shift3.Add("@Order_Status", Order_Status);
                            dt_get_Shift3 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift3);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift3, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift3, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift3, userroleid, User_id, Production_date, dt_get_Shift3.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift3.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    //4 pass shift ,  date and ALL order status
                    if (Row_Value_Type == "Total" && Column_Value_Type == "Total")
                    {
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField39).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField40).ToString();
                            val_Shift_Type_Name = value_3;
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift4 = new Hashtable();
                            DataTable dt_get_Shift4 = new DataTable();
                            ht_get_Shift4.Clear();
                            dt_get_Shift4.Clear();

                            ht_get_Shift4.Add("@Trans", "SHIFT_TYPE_SINGLE_DATE_AND_SHIFT_WISE");
                            ht_get_Shift4.Add("@date", Date);
                            ht_get_Shift4.Add("@Shift_Type_Name", Shift_Type_Name);
                            dt_get_Shift4 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift4);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift4, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift4, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift4, userroleid, User_id, Production_date, dt_get_Shift4.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift4.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    //5 pass  date and single order status
                    if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")
                    {
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField39).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        }
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value2 = e.GetFieldValue(pivotGridField38).ToString();
                            val_order_Status = value2;
                        }
                        Date = val_Cda.ToString();
                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift5 = new Hashtable();
                            DataTable dt_get_Shift5 = new DataTable();
                            ht_get_Shift5.Clear();
                            dt_get_Shift5.Clear();

                            if (checked_Item == "ALL")
                            {
                                ht_get_Shift5.Add("@Trans", "SHIFT_TYPE_SINGLE_DATE_AND_STATUS_WISE");

                            }
                            else if (checked_Item == "BANGALORE")
                            {
                                ht_get_Shift5.Add("@Trans", "SHIFT_TYPE_SINGLE_DATE_AND_STATUS_WISE");
                            }
                            else if (checked_Item == "HOSUR")
                            {
                                ht_get_Shift5.Add("@Trans", "SHIFT_TYPE_SINGLE_DATE_AND_STATUS_WISE");
                            }


                            //ht_get_Shift5.Add("@Trans", "SHIFT_TYPE_SINGLE_DATE_AND_STATUS_WISE");
                            ht_get_Shift5.Add("@date", Date);
                            ht_get_Shift5.Add("@Order_Status", Order_Status);
                            ht_get_Shift5.Add("@Fromdate", dateEdit_From_date.Text);
                            ht_get_Shift5.Add("@Todate", dateEdit_To_Date.Text);
                            dt_get_Shift5 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift5);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift5, userroleid, User_id, Production_date);
                            //shift_deatils.Show();


                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift5, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift5, userroleid, User_id, Production_date, dt_get_Shift5.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift5.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }


                    //6 pass  date and All order status
                    if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Total")
                    {
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.ColumnArea))
                        {
                            string value1 = e.GetFieldValue(pivotGridField39).ToString();
                            val_Cda = value1;
                            V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        }
                        Date = val_Cda.ToString();


                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift6 = new Hashtable();
                            DataTable dt_get_Shift6 = new DataTable();
                            ht_get_Shift6.Clear();
                            dt_get_Shift6.Clear();

                            ht_get_Shift6.Add("@Trans", "SHIFT_TYPE_SINGLE_DATE_WISE_GRAND_TOTAL");
                            ht_get_Shift6.Add("@date", Date);
                            dt_get_Shift6 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift6);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift6, userroleid, User_id, Production_date);
                            //shift_deatils.Show();


                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift6, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift6, userroleid, User_id, Production_date, dt_get_Shift6.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift6.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    //7 pass  from date and todate
                    if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal")
                    {
                        V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift7 = new Hashtable();
                            DataTable dt_get_Shift7 = new DataTable();
                            ht_get_Shift7.Clear();
                            dt_get_Shift7.Clear();

                            ht_get_Shift7.Add("@Trans", "ALL_DATE_SHIFTWISE_DATA");
                            ht_get_Shift7.Add("@Fromdate", dateEdit_From_date.Text);
                            ht_get_Shift7.Add("@Todate", dateEdit_To_Date.Text);
                            dt_get_Shift7 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift7);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift7, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift7, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift7, userroleid, User_id, Production_date, dt_get_Shift7.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift7.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }

                    //8 pass  from date and todate
                    if (Row_Value_Type == "Total" && Column_Value_Type == "GrandTotal")
                    {
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField40).ToString();
                            val_Shift_Type_Name = value_3;
                        }
                        V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift8 = new Hashtable();
                            DataTable dt_get_Shift8 = new DataTable();
                            ht_get_Shift8.Clear();
                            dt_get_Shift8.Clear();
                            ht_get_Shift8.Add("@Trans", "SHIFT_ALL_DATE_AND_ALL_ORDER_STATUS_WISE");
                            ht_get_Shift8.Add("@Fromdate", dateEdit_From_date.Text);
                            ht_get_Shift8.Add("@Todate", dateEdit_To_Date.Text);
                            ht_get_Shift8.Add("@Shift_Type_Name", Shift_Type_Name);
                            dt_get_Shift8 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift8);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift8, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift8, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift8, userroleid, User_id, Production_date, dt_get_Shift8.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift8.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");

                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }
                    //int Count=0;
                    //StringBuilder sb = new StringBuilder();
                    //9 pass  from date and todate
                    if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")
                    {
                        //foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
                        //{
                        //    Count = Count + 1;
                        //    string value1 = e.GetFieldValue(pivotGridField39).ToString();

                        //    if (Count==1)
                        //    {

                        //        val_Cda = value1;
                        //        sb = sb.Append(val_Cda);
                        //    }
                        //    else
                        //    {
                        //        sb = sb.Append("," + val_Cda);
                        //        val_Cda = sb.ToString();


                        //       Count++;
                        //    }

                        //}
                        Date = val_Cda.ToString();


                        V_Data = e.GetFieldValue(pivotGridField42).ToString().Trim();
                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            string value_3 = e.GetFieldValue(pivotGridField40).ToString();
                            val_Shift_Type_Name = value_3;
                        }

                        foreach (var field in pivotGridControlShiftWise2.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField41) != null)
                            {
                                var value_4 = e.GetFieldValue(pivotGridField41).ToString();
                                val_Emp_Job_Role = value_4;
                            }
                            else
                            {
                                val_Emp_Job_Role = null;
                            }

                        }
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift9 = new Hashtable();
                            DataTable dt_get_Shift9 = new DataTable();
                            ht_get_Shift9.Clear();
                            dt_get_Shift9.Clear();

                            ht_get_Shift9.Add("@Trans", "SHIFT_EMPJOBROLE_ALLDATE_AND_ALL_STATUS_WISE");
                            ht_get_Shift9.Add("@Fromdate", dateEdit_From_date.Text);
                            ht_get_Shift9.Add("@Todate", dateEdit_To_Date.Text);
                            ht_get_Shift9.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shift9.Add("@Emp_Job_Role", Emp_Job_Role);
                            dt_get_Shift9 = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift9);

                            //Daily_Status_Order_View_Detail shift_deatils = new Daily_Status_Order_View_Detail(dt_get_Shift9, userroleid, User_id, Production_date);
                            //shift_deatils.Show();

                            // DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift9, userroleid, User_id, Production_date);
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift9, userroleid, User_id, Production_date, dt_get_Shift9.Rows[0]["Client_Number"].ToString(), int.Parse(dt_get_Shift9.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, "");


                            shift_deatils.Show();
                        }
                        else
                        {

                            SplashScreenManager.CloseForm(false);
                        }
                    }

                }

            }

            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        //private void pivotGridControl6_CellClick(object sender, PivotCellEventArgs e)
        //{
        //    SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
        //    try
        //    {
        //        string Row_Value_Type = "";
        //        string Column_Value_Type = "";
        //        string value_Cda = ""; string value_ClientNumber = ""; string val_Order_Status = "";
        //        string Client_Number = "";
        //        string Date = "";
        //        string Order_Status = "";
        //        string V_Data = "";
        //        string Column_Name = "";
        //        string Row_Name = "";

        //        PivotGridHitInfo hi = pivotGridControl6.CalcHitInfo(pivotGridControl6.PointToClient(MousePosition));
        //        if (hi.HitTest == PivotGridHitTest.Cell)
        //        {

        //            Column_Name = hi.CellInfo.DataField.FieldName.ToString();
        //            Row_Name = hi.CellInfo.RowField.FieldName.ToString();
        //            Row_Value_Type = hi.CellInfo.RowValueType.ToString();
        //            Column_Value_Type = hi.CellInfo.ColumnValueType.ToString();

        //            //Row_Value_Type= hi.HeaderField.GrandTotalText.ToString();

        //            //

        //            foreach (var field in pivotGridControl6.GetFieldsByArea(PivotArea.RowArea))
        //            {
        //                if (Column_Value_Type != "GrandTotal")       // Date not Requried while Taking All date Wise
        //                {
        //                    string value1 = e.GetFieldValue(pivotGridField37).ToString();
        //                    value_Cda = value1;
        //                }
        //                V_Data = e.GetFieldValue(pivotGridField17).ToString().Trim();
        //            }

        //            foreach (var field in pivotGridControl6.GetFieldsByArea(PivotArea.RowArea))
        //            {
        //                if (Row_Value_Type != "GrandTotal" && Column_Value_Type != "GrandTotal")
        //                {
        //                    string value_2 = e.GetFieldValue(pivotGridField34).ToString();
        //                    value_ClientNumber = value_2;

        //                }
        //            }
        //            foreach (var field in pivotGridControl6.GetFieldsByArea(PivotArea.ColumnArea))
        //            {
        //                if (Row_Value_Type != "GrandTotal" && Column_Value_Type != "GrandTotal")
        //                {
        //                    string value_2 = e.GetFieldValue(pivotGridField36).ToString();
        //                    val_Order_Status = value_2;
        //                }


        //            }



        //            System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

        //            dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_Order_Status);

        //            if (dt_Get_Details.Rows.Count > 0)
        //            {
        //                Order_Status = dt_Get_Details.Rows[0][0].ToString();
        //            }

        //            Client_Number = value_ClientNumber.ToString();
        //            Date = value_Cda;
        //        }

        //        if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
        //        {

        //            if (V_Data != "" && V_Data != "0")
        //            {
        //                Hashtable ht_getgriddetails = new Hashtable();
        //                System.Data.DataTable dt_getgriddetails = new System.Data.DataTable();
        //                ht_getgriddetails.Clear();
        //                dt_getgriddetails.Clear();
        //                ht_getgriddetails.Add("@Trans", "AGENT_OPEN_ORDER_DETAILS");
        //                ht_getgriddetails.Add("@Client_Number", Client_Number);
        //                ht_getgriddetails.Add("@Order_Status", Order_Status);
        //                ht_getgriddetails.Add("@date", Date);
        //                dt_getgriddetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getgriddetails);

        //                Daily_Status_Order_View_Detail dailystatus = new Daily_Status_Order_View_Detail(dt_getgriddetails, userroleid, User_id, Production_date);

        //                dailystatus.Show();
        //            }
        //            else
        //            {
        //                SplashScreenManager.CloseForm(false);
        //            }
        //        }
        //        else if (Row_Value_Type == "Value" && Column_Value_Type == "Total")  // Row Wise Count for Product_Type Wise
        //        {

        //            if (V_Data != "" && V_Data != "0")
        //            {
        //                Hashtable ht_getgrid_detail = new Hashtable();
        //                System.Data.DataTable dt_getgrid_detail = new System.Data.DataTable();
        //                ht_getgrid_detail.Clear();
        //                dt_getgrid_detail.Clear();
        //                ht_getgrid_detail.Add("@Trans", "AGENT_OPEN_ORDER_ROW_TOTAL_CLIENT_DATE_WISE");
        //                ht_getgrid_detail.Add("@Client_Number", Client_Number);
        //                ht_getgrid_detail.Add("@date", Date);
        //                ht_getgrid_detail.Add("@Order_Status", Order_Status);

        //                dt_getgrid_detail = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getgrid_detail);

        //                Daily_Status_Order_View_Detail dailystatus = new Daily_Status_Order_View_Detail(dt_getgrid_detail, userroleid, User_id, Production_date);

        //                dailystatus.Show();
        //            }
        //            else
        //            {
        //                SplashScreenManager.CloseForm(false);
        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        //Close Wait Form
        //        SplashScreenManager.CloseForm(false);

        //        MessageBox.Show("Error Occured Please Check With Administrator");
        //    }
        //    finally
        //    {
        //        //Close Wait Form
        //        SplashScreenManager.CloseForm(false);
        //    }

        //}

        private void pivotGridControlTopEfficiency_CellClick(object sender, PivotCellEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                if (e.DataField.FieldName == "User_Effeciency")
                {
                    PivotDrillDownDataSource source = e.CreateDrillDownDataSource();
                    if (source.RowCount > 0)
                    {
                        PivotDrillDownDataRow row = source[0];
                        object userId = row["User_Id"];
                        object userRole = row["User_RoleId"];
                        var ht = new Hashtable()
                        {
                            {"@Trans", "INSERT_INTO_TEMP_USER" },
                            { "@Production_Date", dateEdit1_Current_Date_Top_Eff.Text },
                            {"@User_Id",userId }
                        };
                        var val = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", ht);
                        Dashboard.Emp_Production_Score_Board TargeDashboard = new Dashboard.Emp_Production_Score_Board(Convert.ToInt32(userId), userRole.ToString(), dateEdit1_Current_Date_Top_Eff.Text, "");
                        TargeDashboard.Show();
                    }
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

        private void btn_Submit_Top_Eff_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                // dateEdit_From_date.Text = ""; dateEdit_To_Date.Text = "";

                //if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
                //{
                //    Top_Effieciency(dateEdit_From_date.Text);
                //}


                if (dateEdit1_Current_Date_Top_Eff.Text != "")
                {
                    TopEffieciency(dateEdit1_Current_Date_Top_Eff.Text);
                }


            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }
        private void TopEffieciency(string Date)
        {
            string Check_Branch_All = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
            string Check_Banglore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
            string Check_Hosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();
            string checked_Item = ""; int branch_id = 0;
            if (Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
            {
                checked_Item = checkedLstBxCntrl_Branch_Wise.Items[0].Description.ToString();

            }
            else if (Check_Banglore == "Checked" && Check_Branch_All == "Unchecked" && Check_Hosur == "Unchecked")
            {
                checked_Item = checkedLstBxCntrl_Branch_Wise.Items[1].Description.ToString();
                branch_id = 3;
            }
            else if (Check_Hosur == "Checked" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked")
            {
                checked_Item = checkedLstBxCntrl_Branch_Wise.Items[2].Description.ToString();
                branch_id = 5;
            }
            else if (Check_Hosur == "Unchecked" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked")
            {
                checked_Item = "";

            }


            //if (Date != "" )
            //{
            //    Hashtable ht_get_eff = new Hashtable();
            //    ht_get_eff.Add("@Trans", "CALCULATE_DAILY_TOP_EFF");
            //    ht_get_eff.Add("@date", dateEdit1_Current_Date_Top_Eff.Text);

            //    dt_get_eff = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_eff);
            //    pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
            //    pivotGridControlTopEfficiency.DataSource = dt_get_eff;

            //}
            //else
            //{
            //    pivotGridControlTopEfficiency.DataSource = null;
            //}


            //new 
            // both branch wise
            if (Date != "" && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
            {
                Hashtable ht_get_eff = new Hashtable();
                ht_get_eff.Add("@Trans", "CALCULATE_DAILY_TOP_EFF");
                ht_get_eff.Add("@date", dateEdit1_Current_Date_Top_Eff.Text);

                dt_get_eff = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_eff);
                pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
                pivotGridControlTopEfficiency.DataSource = dt_get_eff;

            }


            //date wise both branch wise
            else if (Date != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
            {

                pivotGridControlTopEfficiency.DataSource = null;
                MessageBox.Show("Select any one of Branch");
            }


            // banglore branch wise
            else if (Date != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Checked" && Check_Hosur == "Unchecked")
            {
                Hashtable ht_get_eff_Bangl = new Hashtable();
                DataTable dt_get_eff_Bangl = new DataTable();

                ht_get_eff_Bangl.Add("@Trans", "CALCULATE_DAILY_TOP_EFF_BRANCH_WISE_1");
                ht_get_eff_Bangl.Add("@date", dateEdit1_Current_Date_Top_Eff.Text);
                ht_get_eff_Bangl.Add("@Branch_ID", branch_id);
                dt_get_eff_Bangl = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_eff_Bangl);

                pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
                pivotGridControlTopEfficiency.DataSource = dt_get_eff_Bangl;
            }
            //hosur branch wise
            else if (Date != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Checked")
            {
                Hashtable ht_get_eff_hosur = new Hashtable();
                DataTable dt_get_eff_hosur = new DataTable();

                ht_get_eff_hosur.Add("@Trans", "CALCULATE_DAILY_TOP_EFF_BRANCH_WISE_1");
                ht_get_eff_hosur.Add("@date", dateEdit1_Current_Date_Top_Eff.Text);
                ht_get_eff_hosur.Add("@Branch_ID", branch_id);
                dt_get_eff_hosur = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_eff_hosur);

                pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
                pivotGridControlTopEfficiency.DataSource = dt_get_eff_hosur;
            }


            //unchecked all branch  checkbox current date wise 16 july 2019
            //else if (Date != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
            //{
            //    Hashtable ht_get_eff = new Hashtable();
            //    ht_get_eff.Add("@Trans", "CALCULATE_DAILY_TOP_EFF");
            //    ht_get_eff.Add("@date", dateEdit1_Current_Date_Top_Eff.Text);

            //    dt_get_eff = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_get_eff);
            //    pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
            //    pivotGridControlTopEfficiency.DataSource = dt_get_eff;


            //}
            //else
            //{
            //    pivotGridControlTopEfficiency.DataSource = null;
            //}




        }



        // july 19 2019

        private void Top_Effieciency(string Fromdate, string Todate)
        {
            string Check_Branch_All = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
            string Check_Banglore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
            string Check_Hosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();
            dateEdit1_Current_Date_Top_Eff.Text = "";

            //new 
            // both branch wise
            if (Fromdate != "" && Todate != "" && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
            {
                Hashtable ht_geteff = new Hashtable();
                DataTable dt_geteff = new DataTable();
                ht_geteff.Add("@Trans", "CALCULATE_DAILY_TOP_EFF_FROMDATE_TODATE_BOTH_BRANCH_WISE");
                ht_geteff.Add("@Fromdate", Fromdate);
                ht_geteff.Add("@Todate", Todate);


                dt_get_eff = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_geteff);
                pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
                pivotGridControlTopEfficiency.DataSource = dt_get_eff;

            }

            // banglore branch wise
            else if (Fromdate != "" && Todate != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Checked" && Check_Hosur == "Unchecked")
            {
                Hashtable ht_geteffBangl = new Hashtable();
                DataTable dt_geteffBangl = new DataTable();

                ht_geteffBangl.Add("@Trans", "CALCULATE_DAILY_TOP_EFF_FROMDATE_TODATE_SINGLE_BRANCH_WISE");
                ht_geteffBangl.Add("@Fromdate", Fromdate);
                ht_geteffBangl.Add("@Todate", Todate);
                ht_geteffBangl.Add("@Branch_ID", 3);
                dt_geteffBangl = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_geteffBangl);

                pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
                pivotGridControlTopEfficiency.DataSource = dt_geteffBangl;
            }
            //hosur branch wise
            else if (Fromdate != "" && Todate != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Checked")
            {
                Hashtable ht_geteffhosur = new Hashtable();
                DataTable dt_geteffhosur = new DataTable();

                ht_geteffhosur.Add("@Trans", "CALCULATE_DAILY_TOP_EFF_FROMDATE_TODATE_SINGLE_BRANCH_WISE");
                ht_geteffhosur.Add("@Fromate", Fromdate);
                ht_geteffhosur.Add("@Todate", Todate);
                ht_geteffhosur.Add("@Branch_ID", 5);
                dt_geteffhosur = dataaccess.ExecuteSP("Sp_Daily_Status_Top_Efficiency_Calculation", ht_geteffhosur);

                pivotGridField47.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                pivotGridField47.ValueFormat.FormatString = "{0:d2} %";
                pivotGridControlTopEfficiency.DataSource = dt_geteffhosur;
            }
            //Fromdate and todate wise 
            else if (Fromdate != "" && Todate != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
            {

                pivotGridControlTopEfficiency.DataSource = null;
                MessageBox.Show("Select any one of Branch");
            }




        }


        // july 16 2019 top eff from date and todate



        // A custom formatter object.
        public class BaseFormatter : IFormatProvider, ICustomFormatter
        {
            // The GetFormat method of the IFormatProvider interface.
            // This must return an object that provides formatting services for the specified type.
            public object GetFormat(Type format)
            {
                if (format == typeof(ICustomFormatter)) return this;
                else return null;
            }
            // The Format method of the ICustomFormatter interface.
            // This must format the specified value according to the specified format settings.
            public string Format(string format, object arg, IFormatProvider provider)
            {
                if (format == null)
                {
                    if (arg is IFormattable)
                        return ((IFormattable)arg).ToString(format, provider);
                    else
                        return arg.ToString();
                }
                if (format == "B")
                    return Convert.ToString(Convert.ToString(arg));

                else
                    return arg.ToString();
            }
        }



        private void CapacityUtilization()
        {
            try
            {
                string Check_Branch_All = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
                string Check_Banglore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
                string Check_Hosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();
                string checked_Item = ""; int branch_id = 0;
                if (Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[0].Description.ToString();

                }
                else if (Check_Banglore == "Checked" && Check_Branch_All == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[1].Description.ToString();
                    branch_id = 3;
                }
                else if (Check_Hosur == "Checked" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[2].Description.ToString();
                    branch_id = 5;
                }
                else if (Check_Hosur == "Unchecked" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked")
                {
                    checked_Item = "";

                }


                ////-------------
                //if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "")
                //{
                //    Hashtable ht_get = new Hashtable();
                //    //System.Data.DataTable dt_get = new System.Data.DataTable();
                //    ht_get.Clear();
                //    dt_get.Clear();
                //    ht_get.Add("@Trans", "CAPACITY_UTILIZATION");
                //    ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                //    ht_get.Add("@Todate", dateEdit_To_Date.Text);
                //    dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get);

                //    Grd_Capcity_Utilization.DataSource = dt_get;
                //    dt_Capacity_Utilization.Clear();
                //    dt_Capacity_Utilization = dt_get;
                //    // ---------------Bind Chart here------------------
                //    chartControl1.DataSource = dt_get;
                //    chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                //    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                //    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";
                //}
                //else
                //{
                //    Grd_Capcity_Utilization.DataSource = null;
                //}



                // new 
                // All branch wise
                if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
                {
                    Hashtable ht_get = new Hashtable();
                    //System.Data.DataTable dt_get = new System.Data.DataTable();
                    ht_get.Clear();
                    dt_get.Clear();
                    ht_get.Add("@Trans", "CAPACITY_UTILIZATION");
                    ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                    ht_get.Add("@Todate", dateEditToAll.Text);
                    dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get);

                    Grd_Capcity_Utilization.DataSource = dt_get;
                    dt_Capacity_Utilization.Clear();
                    dt_Capacity_Utilization = dt_get;
                    // ---------------Bind Chart here------------------
                    chartControl1.DataSource = dt_get;
                    chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
                    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";
                }




                // banglore branch wise
                else if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Checked" && Check_Hosur == "Unchecked")
                {
                    Hashtable ht_get = new Hashtable();
                    //System.Data.DataTable dt_get = new System.Data.DataTable();
                    ht_get.Clear();
                    dt_get.Clear();
                    ht_get.Add("@Trans", "CAPACITY_UTILIZATION_BRANCH_WISE");
                    ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                    ht_get.Add("@Todate", dateEditToAll.Text);
                    ht_get.Add("@Branch_Id", branch_id);
                    dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get);

                    Grd_Capcity_Utilization.DataSource = dt_get;
                    dt_Capacity_Utilization.Clear();
                    dt_Capacity_Utilization = dt_get;
                    // ---------------Bind Chart here------------------
                    chartControl1.DataSource = dt_get;
                    chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";
                }

                // Hosur branch wise
                else if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Checked")
                {
                    Hashtable ht_get = new Hashtable();
                    //System.Data.DataTable dt_get = new System.Data.DataTable();
                    ht_get.Clear();
                    dt_get.Clear();
                    ht_get.Add("@Trans", "CAPACITY_UTILIZATION_BRANCH_WISE");
                    ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                    ht_get.Add("@Todate", dateEditToAll.Text);
                    ht_get.Add("@Branch_Id", branch_id);
                    dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get);

                    Grd_Capcity_Utilization.DataSource = dt_get;
                    dt_Capacity_Utilization.Clear();
                    dt_Capacity_Utilization = dt_get;
                    // ---------------Bind Chart here------------------
                    chartControl1.DataSource = dt_get;
                    chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";
                }

                else if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    Hashtable ht_get = new Hashtable();
                    //System.Data.DataTable dt_get = new System.Data.DataTable();
                    ht_get.Clear();
                    dt_get.Clear();
                    ht_get.Add("@Trans", "CAPACITY_UTILIZATION");
                    ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                    ht_get.Add("@Todate", dateEditToAll.Text);
                    dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get);

                    Grd_Capcity_Utilization.DataSource = dt_get;
                    dt_Capacity_Utilization.Clear();
                    dt_Capacity_Utilization = dt_get;
                    // ---------------Bind Chart here------------------
                    chartControl1.DataSource = dt_get;
                    chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";
                }
                else
                {
                    Grd_Capcity_Utilization.DataSource = null;
                    chartControl1.DataSource = null;
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void Bind_Capacity_Utilization_Branch_Wise(int Branch_Id)
        {
            try
            {
                if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "")
                {
                    Hashtable ht_get = new Hashtable();
                    //System.Data.DataTable dt_get = new System.Data.DataTable();
                    ht_get.Clear();
                    dt_get.Clear();
                    ht_get.Add("@Trans", "CAPACITY_UTILIZATION_BRANCH_WISE");
                    ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                    ht_get.Add("@Todate", dateEditToAll.Text);
                    ht_get.Add("@Branch_Id", Branch_Id);
                    dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get);


                    Grd_Capcity_Utilization.DataSource = dt_get;

                    dt_Capacity_Utilization.Clear();

                    dt_Capacity_Utilization = dt_get;
                    // ---------------Bind Chart here------------------


                    chartControl1.DataSource = dt_get;
                    chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";

                }
                else
                {
                    Grd_Capcity_Utilization.DataSource = null;
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void Bind_Capacity_Utilization_Branch_Wise(int Branch_Id, int Shift_Type_Id)
        {

            if (dateEdit_From_date.Text != "" && dateEditToAll.Text != "")
            {
                Hashtable ht_get = new Hashtable();
                //System.Data.DataTable dt_get = new System.Data.DataTable();
                ht_get.Clear();
                dt_get.Clear();
                ht_get.Add("@Trans", "CAPACITY_UTILIZATION_BRANCH_SHIFT_WISE");
                ht_get.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get.Add("@Todate", dateEditToAll.Text);
                ht_get.Add("@Branch_Id", Branch_Id);
                ht_get.Add("@Shift_Type_Id", Shift_Type_Id);
                dt_get = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get);


                Grd_Capcity_Utilization.DataSource = dt_get;

                dt_Capacity_Utilization.Clear();

                dt_Capacity_Utilization = dt_get;
                // ---------------Bind Chart here------------------


                chartControl1.DataSource = dt_get;
                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";

            }
            else
            {
                Grd_Capcity_Utilization.DataSource = null;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabNavigationPage8_Paint(object sender, PaintEventArgs e)
        {

        }


        private void Chk_List_Report_MouseClick(object sender, MouseEventArgs e)
        {


            string check_Status = Chk_List_Report.Items[0].CheckState.ToString();

            for (int i = 0; i < Chk_List_Report.CheckedItems.Count; i++)
            {

                string Checked_Item_Name = Chk_List_Report.CheckedItems[i].ToString();

            }

            int Items_Count = 0;
            int Selcetd_Items_Count = 0;

            if (check_Status == "Checked")
            {
                Items_Count = 1;
                Selcetd_Items_Count = Chk_List_Report.CheckedItems.Count;
            }
            else
            {
                check_Status = "Unchecked";

                Selcetd_Items_Count = Chk_List_Report.CheckedItems.Count;
                Items_Count = 0;
            }

            //string Check_Items = Chk_List_Report.SelectedItem.ToString();


            if (check_Status == "Checked")
            {

                for (int i = 1; i < Chk_List_Report.Items.Count; i++)
                {

                    Chk_List_Report.SetItemChecked(i, true);
                }



            }
            else if (check_Status == "Unchecked" && Selcetd_Items_Count == 7)
            {


                for (int i = 1; i < Chk_List_Report.Items.Count; i++)
                {

                    Chk_List_Report.SetItemChecked(i, false);
                }

            }
        }



        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl1.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;

            if (point != null)
            {
                string Date, count, values;

                Date = point.Argument.ToString();

                count = Convert.ToInt32(point.Values[0]).ToString();
                values = "Value(s): " + count;


            }
        }

        private void tabPane1_Click(object sender, EventArgs e)
        {

            if (tabNavigationPage9.Caption == "All Client Status")
            {
                tabNavigationPage11.PageVisible = true;
            }
            if (tabNavigationPage10.Caption == "My Client Status")
            {
                tabNavigationPage13.PageVisible = true;
            }

        }

        private void pivotGridControl2_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {

            if (e.Field.FieldName == "Order_Status" && e.Field.FieldName != null)
            {
                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_ID"),
                     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_ID");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;

            }


        }

        private void pivotGridControl3_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "Order_Status")
            {

                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_Id"),
                     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_Id");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;
            }
        }

        private void pivotGridControl4_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "Order_Status")
            {

                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_ID"),
                     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_ID");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;
            }
        }

        private void pivotGridControl5_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "Order_Status")
            {

                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_ID"),
                     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_ID");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;
            }
        }

        private void pivotGridControl8_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "Order_Status")
            {
                //int Index2 = e.ListSourceRowIndex1 + 1;
                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_Id"),
                     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_Id");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);

                e.Handled = true;

            }
        }



        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                //string Tab_Name = "";
                //Tab_Name = "Capacity Utilization Wise";
                object obj_4 = ddl_Capacity_Branch.EditValue;
                int Branch_Id = 0;
                string branch_Name = ddl_Capacity_Branch.Text;
                if (obj_4.ToString() != "0")
                {
                    Branch_Id = (int)obj_4;
                }


                object obj_5 = ddl_Shift.EditValue;
                int Shift_Type_Id = 0;
                string Shift_Name = ddl_Shift.Text;
                if (obj_5.ToString() != "0")
                {
                    Shift_Type_Id = (int)obj_5;
                }

                string Check_Branch_All = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
                string Check_Banglore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
                string Check_Hosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();


                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                var columnIndex = gridView1.FocusedColumn.VisibleIndex;
                string Production_Date_1 = row["Date"].ToString();

                Hashtable htgetShift = new Hashtable();
                DataTable dtgetShift = new DataTable();

                if (Branch_Id == 0 && Shift_Type_Id == 0 && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_WISE");
                }
                else if (Branch_Id == 0 && Shift_Type_Id == 0 && Check_Banglore == "Checked" && Check_Branch_All == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_AND_BRANCH_WISE");
                    htgetShift.Add("@Branch_ID", 3);
                }
                else if (Branch_Id == 0 && Shift_Type_Id == 0 && Check_Hosur == "Checked" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_AND_BRANCH_WISE");
                    htgetShift.Add("@Branch_ID", 5);
                }


                // july 16 ------if all branch checkbox checked then

                if (Branch_Id != 0 && Shift_Type_Id == 0 && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_AND_BRANCH_WISE");
                    htgetShift.Add("@Branch_ID", Branch_Id);
                }
                else if (Branch_Id != 0 && Shift_Type_Id != 0 && Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_AND_BRANCH_AND_SHIFT_WISE");
                    htgetShift.Add("@Branch_ID", Branch_Id);
                    htgetShift.Add("@Shift_Type_Id", Shift_Type_Id);
                }


                // uchecked all branche check box it uses from date and todate
                else if (Branch_Id == 0 && Shift_Type_Id == 0 && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_WISE");
                }
                else if (Branch_Id != 0 && Shift_Type_Id == 0 && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_AND_BRANCH_WISE");
                    htgetShift.Add("@Branch_ID", Branch_Id);
                }
                else if (Branch_Id != 0 && Shift_Type_Id != 0 && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    htgetShift.Add("@Trans", "SHIFT_DATE_AND_BRANCH_AND_SHIFT_WISE");
                    htgetShift.Add("@Branch_ID", Branch_Id);
                    htgetShift.Add("@Shift_Type_Id", Shift_Type_Id);
                }

                htgetShift.Add("@date", Production_Date_1.ToString());
                dtgetShift = dataaccess.ExecuteSP("Sp_Daily_Status_Report", htgetShift);



                //string date=dt_Capacity_Utilization.Rows[0]["Date"].ToString();

                Hashtable ht_get_singale_date_wise = new Hashtable();
                DataTable dt_get_singale_date_wise = new DataTable();

                ht_get_singale_date_wise.Clear();
                dt_get_singale_date_wise.Clear();

                if (Branch_Id == 0 && Shift_Type_Id == 0)
                {
                    ht_get_singale_date_wise.Add("@Trans", "CAPACITY_UTILIZATION_SINGALE_DATE_WISE");
                }
                else if (Branch_Id != 0 && Shift_Type_Id != 0)
                {

                    ht_get_singale_date_wise.Add("@Trans", "CAPACITY_UTILIZATION_BRANCH_SHIFT_WISE_SINGLE_DAY");
                }
                else if (Branch_Id != 0 && Shift_Type_Id == 0)
                {

                    ht_get_singale_date_wise.Add("@Trans", "CAPACITY_UTILIZATION_SINGALE_DATE_WISE_BRANCH_WISE");
                }
                ht_get_singale_date_wise.Add("@Fromdate", dateEdit_From_date.Text);
                ht_get_singale_date_wise.Add("@Todate", dateEdit_To_Date.Text);
                ht_get_singale_date_wise.Add("@date", Production_Date_1);
                ht_get_singale_date_wise.Add("@Branch_Id", Branch_Id);
                ht_get_singale_date_wise.Add("@Shift_Type_Id", Shift_Type_Id);
                dt_get_singale_date_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get_singale_date_wise);

                if (columnIndex == 1)
                {
                    chartControl1.DataSource = dt_Capacity_Utilization;
                    chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
                    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Cap_Utilization";

                    //string Production_Date_1 = dt_Capacity_Utilization.Rows[0]["Date"].ToString();

                    Ordermanagement_01.Reports.View_Summary view = new Ordermanagement_01.Reports.View_Summary(dtgetShift, dt_get_singale_date_wise, Branch_Id, Shift_Type_Id);
                    view.Show();
                }

                if (columnIndex == 2)
                {
                    chartControl1.DataSource = dt_Capacity_Utilization;
                    chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                    chartControl1.Series["Cap_Utilization"].ArgumentDataMember = "Date";
                    chartControl1.Series["Cap_Utilization"].ValueDataMembers[0] = "Prod_Cap_Utilization";

                    Ordermanagement_01.Reports.View_Summary view = new Reports.View_Summary(dtgetShift, dt_get_singale_date_wise, Branch_Id, Shift_Type_Id);
                    view.Show();
                }


            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Problem While binding Data");


            }

            finally
            {

                SplashScreenManager.CloseForm(false);
            }

        }




        private void btn_Top_Eff_Export_Click(object sender, EventArgs e)
        {


            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                Export_Top_Efficiency_Grid();
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        //void PrintingSystem_XlsxDocumentCreated(object sender, XlsxDocumentCreatedEventArgs e)
        //{
        //    e.SheetNames[0] = " MySheet 1";
        //    e.SheetNames[1] = " MySheet 2";
        //}

        //private void Chk_List_Report_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string Selected_Index = Chk_List_Report.SelectedIndex.ToString();

        //   // string Selected_Index = Chk_List_Report..ToString();


        //    //btn_Submit_Click(sender, e);

        //    //for (int i = 0; i < Chk_List_Report.ItemCount; i++)
        //    //{
        //    //    string Client_Check = Chk_List_Report.GetItemCheckState(i).ToString();
        //    //    if (Client_Check == "Checked")
        //    //    {


        //    //    }

        //    //}


        //}
        //private void pivotGridControl7_CellClick(object sender, PivotCellEventArgs e)
        //{

        //    try
        //    {
        //            string value_date_1 = ""; string value_Client_Num = ""; string value_Order_Status_1 = "";
        //            string Aging_Client_Number = "";
        //            string Aging_Date = "";
        //            string Aging_Order_Status = "";
        //            string V_Data = "";
        //            string Row_Value_Type = "";
        //            string Column_Value_Type = "";
        //            string Column_Name = "";
        //            string Row_Name = "";

        //            PivotGridHitInfo hi = pivotGridControl7.CalcHitInfo(pivotGridControl7.PointToClient(MousePosition));


        //            if (hi.HitTest == PivotGridHitTest.Cell)
        //            {


        //                Column_Name = hi.CellInfo.DataField.FieldName.ToString();
        //                //  Row_Name = hi.CellInfo.RowField.FieldName.ToString();
        //                Row_Value_Type = hi.CellInfo.RowValueType.ToString();
        //                Column_Value_Type = hi.CellInfo.ColumnValueType.ToString();


        //                if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
        //                {

        //                    //MessageBox.Show("Each Cell Wise");
        //                    MessageBox.Show("Each Cell WIse");

        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.RowArea))
        //                    {
        //                        string value_2 = e.GetFieldValue(pivotGridField41).ToString();
        //                        value_date_1 = value_2;
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_3 = e.GetFieldValue(pivotGridField38).ToString();
        //                        value_Client_Num = value_3;
        //                        V_Data = e.GetFieldValue(pivotGridField39).ToString().Trim();
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_4 = e.GetFieldValue(pivotGridField40).ToString();
        //                        value_Order_Status_1 = value_4;
        //                    }

        //                    System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

        //                    dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
        //                    string Order_Status = "";
        //                    if (dt_Get_Details.Rows.Count > 0)
        //                    {
        //                        Order_Status = dt_Get_Details.Rows[0][0].ToString();
        //                    }


        //                    Aging_Client_Number = value_Client_Num.ToString();
        //                    Aging_Order_Status = Order_Status.ToString();
        //                    Aging_Date = value_date_1;

        //                    if (V_Data != "" && V_Data != "0")
        //                    {
        //                        Hashtable ht_get_Aging_Pending_griddetails = new Hashtable();
        //                        System.Data.DataTable dt_get_Aging_Pending_griddetails = new System.Data.DataTable();
        //                        ht_get_Aging_Pending_griddetails.Clear();
        //                        dt_get_Aging_Pending_griddetails.Clear();

        //                        ht_get_Aging_Pending_griddetails.Add("@Trans", "AGENT_PENDING_ORDER_DETAILS");
        //                        ht_get_Aging_Pending_griddetails.Add("@Client_Number", Aging_Client_Number);
        //                        ht_get_Aging_Pending_griddetails.Add("@Order_Status", Aging_Order_Status);
        //                        ht_get_Aging_Pending_griddetails.Add("@date", Aging_Date);
        //                        dt_get_Aging_Pending_griddetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Aging_Pending_griddetails);

        //                        Daily_Status_Order_View_Detail dailystatus_Aging_pending = new Daily_Status_Order_View_Detail(dt_get_Aging_Pending_griddetails, userroleid, User_id, Production_date);

        //                        dailystatus_Aging_pending.Show();
        //                    }
        //                    else
        //                    {
        //                        SplashScreenManager.CloseForm(false);
        //                    }
        //                }
        //                else if (Row_Value_Type == "Total" && Column_Value_Type == "Value")// Client Wise -Task Wise Total
        //                {

        //                    MessageBox.Show("Client Wise & Task Wise");
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.RowArea))
        //                    {
        //                        string value_2 = e.GetFieldValue(pivotGridField41).ToString();
        //                        value_date_1 = value_2;
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_3 = e.GetFieldValue(pivotGridField38).ToString();
        //                        value_Client_Num = value_3;
        //                        V_Data = e.GetFieldValue(pivotGridField39).ToString().Trim();
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_4 = e.GetFieldValue(pivotGridField40).ToString();
        //                        value_Order_Status_1 = value_4;
        //                    }

        //                    System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

        //                    dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
        //                    string Order_Status = "";
        //                    if (dt_Get_Details.Rows.Count > 0)
        //                    {
        //                        Order_Status = dt_Get_Details.Rows[0][0].ToString();
        //                    }


        //                    Aging_Client_Number = value_Client_Num.ToString();
        //                    Aging_Order_Status = Order_Status.ToString();
        //                    Aging_Date = value_date_1;

        //                    if (V_Data != "" && V_Data != "0")
        //                    {
        //                        Hashtable ht_get_Aging_Pending_grid = new Hashtable();
        //                        System.Data.DataTable dt_get_Aging_Pending_grid = new System.Data.DataTable();

        //                        ht_get_Aging_Pending_grid.Clear();
        //                        dt_get_Aging_Pending_grid.Clear();

        //                        ht_get_Aging_Pending_grid.Add("@Trans", "AGENT_PENDING_ORDER_DETAILS");
        //                        ht_get_Aging_Pending_grid.Add("@Client_Number", Aging_Client_Number);
        //                        ht_get_Aging_Pending_grid.Add("@Order_Status", Aging_Order_Status);

        //                        ht_get_Aging_Pending_grid.Add("@date", Aging_Date);
        //                        dt_get_Aging_Pending_grid = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Aging_Pending_grid);

        //                        Daily_Status_Order_View_Detail dailystatus_Aging_pending = new Daily_Status_Order_View_Detail(dt_get_Aging_Pending_grid, userroleid, User_id, Production_date);

        //                        dailystatus_Aging_pending.Show();
        //                    }
        //                    else
        //                    {
        //                        SplashScreenManager.CloseForm(false);
        //                    }

        //                }
        //                else if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")// All Client and Task Wise
        //                {


        //                    MessageBox.Show("All Client Task Wise");
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.RowArea))
        //                    {
        //                        string value_2 = e.GetFieldValue(pivotGridField41).ToString();
        //                        value_date_1 = value_2;
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_3 = e.GetFieldValue(pivotGridField38).ToString();
        //                        value_Client_Num = value_3;
        //                        V_Data = e.GetFieldValue(pivotGridField39).ToString().Trim();
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_4 = e.GetFieldValue(pivotGridField40).ToString();
        //                        value_Order_Status_1 = value_4;
        //                    }

        //                    System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

        //                    dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
        //                    string Order_Status = "";
        //                    if (dt_Get_Details.Rows.Count > 0)
        //                    {
        //                        Order_Status = dt_Get_Details.Rows[0][0].ToString();
        //                    }


        //                    Aging_Client_Number = value_Client_Num.ToString();
        //                    Aging_Order_Status = Order_Status.ToString();
        //                    Aging_Date = value_date_1;

        //                    if (V_Data != "" && V_Data != "0")
        //                    {
        //                        Hashtable ht_get_Aging_Pending = new Hashtable();
        //                        System.Data.DataTable dt_get_Aging_Pending = new System.Data.DataTable();

        //                        ht_get_Aging_Pending.Clear();
        //                        dt_get_Aging_Pending.Clear();

        //                        ht_get_Aging_Pending.Add("@Trans", "AGENT_PENDING_ORDER_ALL_CLIENT_AND_ALL_STATUS_WISE");

        //                        ht_get_Aging_Pending.Add("@Client_Number", Aging_Client_Number);
        //                        ht_get_Aging_Pending.Add("@Order_Status", Aging_Order_Status);
        //                        ht_get_Aging_Pending.Add("@date", Aging_Date);
        //                        dt_get_Aging_Pending = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Aging_Pending);

        //                        Daily_Status_Order_View_Detail dailystatus_Aging_pending = new Daily_Status_Order_View_Detail(dt_get_Aging_Pending, userroleid, User_id, Production_date);

        //                        dailystatus_Aging_pending.Show();
        //                    }
        //                    else
        //                    {
        //                        SplashScreenManager.CloseForm(false);
        //                    }
        //                }

        //                else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")// Client and Date Wise
        //                {
        //                    MessageBox.Show("Client and Date Wise Total");
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.RowArea))
        //                    {
        //                        string value_2 = e.GetFieldValue(pivotGridField41).ToString();
        //                        value_date_1 = value_2;
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_3 = e.GetFieldValue(pivotGridField38).ToString();
        //                        value_Client_Num = value_3;
        //                        V_Data = e.GetFieldValue(pivotGridField39).ToString().Trim();
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_4 = e.GetFieldValue(pivotGridField40).ToString();
        //                        value_Order_Status_1 = value_4;
        //                    }

        //                    System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

        //                    dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
        //                    string Order_Status = "";
        //                    if (dt_Get_Details.Rows.Count > 0)
        //                    {
        //                        Order_Status = dt_Get_Details.Rows[0][0].ToString();
        //                    }


        //                    Aging_Client_Number = value_Client_Num.ToString();
        //                    Aging_Order_Status = Order_Status.ToString();
        //                    Aging_Date = value_date_1;

        //                    if (V_Data != "" && V_Data != "0")
        //                    {
        //                        Hashtable ht_Aging_Pending_Details = new Hashtable();
        //                        System.Data.DataTable dt_Aging_Pending_Details = new System.Data.DataTable();

        //                        ht_Aging_Pending_Details.Clear();
        //                        dt_Aging_Pending_Details.Clear();

        //                        ht_Aging_Pending_Details.Add("@Trans", "AGENT_PENDING_ORDER_CLIENT_DATE_WISE");
        //                        ht_Aging_Pending_Details.Add("@Client_Number", Aging_Client_Number);
        //                        ht_Aging_Pending_Details.Add("@date", Aging_Date);

        //                        ht_Aging_Pending_Details.Add("@Order_Status", Aging_Order_Status);

        //                        dt_Aging_Pending_Details = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_Aging_Pending_Details);

        //                        Daily_Status_Order_View_Detail dailystatus_Aging_pending = new Daily_Status_Order_View_Detail(dt_Aging_Pending_Details, userroleid, User_id, Production_date);

        //                        dailystatus_Aging_pending.Show();
        //                    }
        //                    else
        //                    {
        //                        SplashScreenManager.CloseForm(false);
        //                    }
        //                }
        //                else if (Row_Value_Type == "Total" && Column_Value_Type == "GrandTotal")// Date Wise - Client Wise All Task
        //                {

        //                    MessageBox.Show("Client Wise and All Task");
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.RowArea))
        //                    {
        //                        string value_2 = e.GetFieldValue(pivotGridField41).ToString();
        //                        value_date_1 = value_2;
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_3 = e.GetFieldValue(pivotGridField38).ToString();
        //                        value_Client_Num = value_3;
        //                        V_Data = e.GetFieldValue(pivotGridField39).ToString().Trim();
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_4 = e.GetFieldValue(pivotGridField40).ToString();
        //                        value_Order_Status_1 = value_4;
        //                    }

        //                    System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

        //                    dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
        //                    string Order_Status = "";
        //                    if (dt_Get_Details.Rows.Count > 0)
        //                    {
        //                        Order_Status = dt_Get_Details.Rows[0][0].ToString();
        //                    }


        //                    Aging_Client_Number = value_Client_Num.ToString();
        //                    Aging_Order_Status = Order_Status.ToString();
        //                    Aging_Date = value_date_1;

        //                    if (V_Data != "" && V_Data != "0")
        //                    {
        //                        Hashtable ht_AgingPending = new Hashtable();
        //                        System.Data.DataTable dt_AgingPending = new System.Data.DataTable();

        //                        ht_AgingPending.Clear();
        //                        dt_AgingPending.Clear();

        //                        ht_AgingPending.Add("@Trans", "AGENT_PENDING_ORDER_CLIENT_AND_ALL_STATUS_WISE");
        //                        ht_AgingPending.Add("@Client_Number", Aging_Client_Number);
        //                        ht_AgingPending.Add("@date", Aging_Date);

        //                        ht_AgingPending.Add("@Order_Status", Aging_Order_Status);
        //                        dt_AgingPending = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_AgingPending);

        //                        Daily_Status_Order_View_Detail dailystatus_Aging_pending = new Daily_Status_Order_View_Detail(dt_AgingPending, userroleid, User_id, Production_date);

        //                        dailystatus_Aging_pending.Show();
        //                    }
        //                    else
        //                    {
        //                        SplashScreenManager.CloseForm(false);
        //                    }

        //                }
        //                else if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")// Date Wise - Client Wise All Task
        //                {

        //                    MessageBox.Show("Client Wise and All Task");
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.RowArea))
        //                    {
        //                        string value_2 = e.GetFieldValue(pivotGridField41).ToString();
        //                        value_date_1 = value_2;
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_3 = e.GetFieldValue(pivotGridField38).ToString();
        //                        value_Client_Num = value_3;
        //                        V_Data = e.GetFieldValue(pivotGridField39).ToString().Trim();
        //                    }
        //                    foreach (var field in pivotGridControl7.GetFieldsByArea(PivotArea.ColumnArea))
        //                    {
        //                        string value_4 = e.GetFieldValue(pivotGridField40).ToString();
        //                        value_Order_Status_1 = value_4;
        //                    }

        //                    System.Data.DataTable dt_Get_Details = new System.Data.DataTable();

        //                    dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", value_Order_Status_1);
        //                    string Order_Status = "";
        //                    if (dt_Get_Details.Rows.Count > 0)
        //                    {
        //                        Order_Status = dt_Get_Details.Rows[0][0].ToString();
        //                    }


        //                    Aging_Client_Number = value_Client_Num.ToString();
        //                    Aging_Order_Status = Order_Status.ToString();
        //                    Aging_Date = value_date_1;

        //                    if (V_Data != "" && V_Data != "0")
        //                    {
        //                        Hashtable ht_AgingPending_detail = new Hashtable();
        //                        System.Data.DataTable dt_AgingPending_detail = new System.Data.DataTable();

        //                        ht_AgingPending_detail.Clear();
        //                        dt_AgingPending_detail.Clear();

        //                        ht_AgingPending_detail.Add("@Trans", "AGENT_PENDING_ORDER_CLIENT_AND_ALL_STATUS_WISE");
        //                        ht_AgingPending_detail.Add("@Client_Number", Aging_Client_Number);
        //                        ht_AgingPending_detail.Add("@date", Aging_Date);

        //                        ht_AgingPending_detail.Add("@Order_Status", Aging_Order_Status);

        //                        dt_AgingPending_detail = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_AgingPending_detail);

        //                        Daily_Status_Order_View_Detail dailystatus_Aging_pending = new Daily_Status_Order_View_Detail(dt_AgingPending_detail, userroleid, User_id, Production_date);

        //                        dailystatus_Aging_pending.Show();
        //                    }
        //                    else
        //                    {
        //                        SplashScreenManager.CloseForm(false);
        //                    }
        //                }


        //         }

        //    }
        //    catch (Exception ex)
        //    {

        //        //Close Wait Form
        //        SplashScreenManager.CloseForm(false);

        //        MessageBox.Show("Error Occured Please Check With Administrator");
        //    }
        //    finally
        //    {
        //        //Close Wait Form
        //        SplashScreenManager.CloseForm(false);
        //    }
        //}

        private void Export_Top_Efficiency_Grid()
        {
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_Top_Eff = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_Top_Eff = new DevExpress.XtraPrintingLinks.CompositeLink(ps_Top_Eff);

                DevExpress.XtraPrinting.PrintableComponentLink link_8_Top_Eff = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_Top_Eff.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                link_8_Top_Eff.Component = pivotGridControlTopEfficiency;


                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_Top_Eff.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_Top_Eff.PrintingSystem = ps_Top_Eff;


                compositeLink_Top_Eff.Links.AddRange(new object[] { link_8_Top_Eff });


                string ReportName = "Top-Efficiency-Status-Report";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_Top_Eff.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_Top_Eff.XlSheetCreated += PrintingSystem_XlSheetCreated_8;


                compositeLink_Top_Eff.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurs Check with Administrator");
            }

        }

        private void btn_Aging_PendingOrders_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);

            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_Pending_Orders = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_Pending_Orders = new DevExpress.XtraPrintingLinks.CompositeLink(ps_Pending_Orders);

                DevExpress.XtraPrinting.PrintableComponentLink link_5_Pending_Orders = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_Pending_Orders.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                link_5_Pending_Orders.Component = pivotGridControlAginfPendingOrders;


                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_Pending_Orders.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_Pending_Orders.PrintingSystem = ps_Pending_Orders;


                compositeLink_Pending_Orders.Links.AddRange(new object[] { link_5_Pending_Orders });


                string ReportName = "Daily_Status_Report_Pending_Orders";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_Pending_Orders.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_Pending_Orders.XlSheetCreated += PrintingSystem_XlSheetCreated_Pending_Orders;


                compositeLink_Pending_Orders.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_Aging_Open_Orders_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_openorders = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_openorders = new DevExpress.XtraPrintingLinks.CompositeLink(ps_openorders);

                DevExpress.XtraPrinting.PrintableComponentLink link_4_openorders = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_openorders.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                link_4_openorders.Component = pivotGridControlAgingOpenOrders;


                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_openorders.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_openorders.PrintingSystem = ps_openorders;


                compositeLink_openorders.Links.AddRange(new object[] { link_4_openorders });


                string ReportName = "Open_Orders_Status_Report";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_openorders.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_openorders.XlSheetCreated += PrintingSystem_XlSheetCreated_Open_Orders;


                compositeLink_openorders.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_Shift_Wise_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                PivotGridControl[] grids_shift = new PivotGridControl[] { pivotGridControlShiftWise, pivotGridControlShiftWise2 };
                DevExpress.XtraPrinting.PrintingSystem ps_Shift = new DevExpress.XtraPrinting.PrintingSystem();

                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_Shift = new DevExpress.XtraPrintingLinks.CompositeLink(ps_Shift);

                DevExpress.XtraPrinting.PrintableComponentLink link_2_Shift = new DevExpress.XtraPrinting.PrintableComponentLink();
                DevExpress.XtraPrinting.PrintableComponentLink link_7_Shift = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_Shift.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_Shift.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_Shift.PrintingSystem = ps_Shift;


                //compositeLink.Links.AddRange(new object[] { link_4 });

                if (pivotGridControlShiftWise.Visible == true)
                {
                    pivotGridControlShiftWise.Visible = true;
                    pivotGridControlShiftWise2.Visible = false;
                    link_2_Shift.Component = pivotGridControlShiftWise;
                    compositeLink_Shift.Links.AddRange(new object[] { link_2_Shift });
                }
                else
                {
                    pivotGridControlShiftWise2.Visible = true;
                    pivotGridControlShiftWise.Visible = false;
                    link_7_Shift.Component = pivotGridControlShiftWise2;
                    compositeLink_Shift.Links.AddRange(new object[] { link_7_Shift });
                }

                string ReportName = "Shift-Wise-Status-Report";
                string folderPath = "C:\\Temp\\";
                //string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                string Path2 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";

                //pivotGridControl2.CustomExportFieldValue += pivotGridControl2_CustomExportFieldValue;

                // compositeLink_Shift.x

                compositeLink_Shift.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_Shift.XlSheetCreated += PrintingSystem_XlSheetCreated_Shift_Wise;




                compositeLink_Shift.PrintingSystem.ExportToXlsx(Path2, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path2);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_ProductTypeWise_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_Product_type = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_Product_type = new DevExpress.XtraPrintingLinks.CompositeLink(ps_Product_type);

                DevExpress.XtraPrinting.PrintableComponentLink link_3_Product_type = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_Product_type.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_Product_type.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_Product_type.PrintingSystem = ps_Product_type;


                link_3_Product_type.Component = pivotGridControlProductTypeWise;
                compositeLink_Product_type.Links.AddRange(new object[] { link_3_Product_type });


                string ReportName = "Product-Type-Wise-Status-Report";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_Product_type.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_Product_type.XlSheetCreated += PrintingSystem_XlSheetCreated_Product_Type;

                compositeLink_Product_type.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_Capacity_Utilization_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_capacity = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_capacity = new DevExpress.XtraPrintingLinks.CompositeLink(ps_capacity);
                DevExpress.XtraPrinting.PrintableComponentLink link_grid_2 = new DevExpress.XtraPrinting.PrintableComponentLink();
                DevExpress.XtraPrinting.PrintableComponentLink link_capac_1 = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_capacity.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_capacity.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_capacity.PrintingSystem = ps_capacity;

                link_grid_2.Component = Grd_Capcity_Utilization;
                link_capac_1.Component = chartControl1;
                compositeLink_capacity.Links.AddRange(new object[] { link_grid_2, link_capac_1 });

                string ReportName = "Capacity-Utilization-Status-Report";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_capacity.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_capacity.XlSheetCreated += PrintingSystem_XlSheetCreated_Capcity_Utilization;

                compositeLink_capacity.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_Daily_Wise_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_daily = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_Daily = new DevExpress.XtraPrintingLinks.CompositeLink(ps_daily);

                DevExpress.XtraPrinting.PrintableComponentLink link_Daily = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_daily.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_daily.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_Daily.PrintingSystem = ps_daily;


                link_Daily.Component = pivotGridControlDailyWise;
                link_Daily.PaperName = "Niranjan";
                compositeLink_Daily.Links.AddRange(new object[] { link_Daily });


                string ReportName = "Daily-Wise-Status-Report";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_Daily.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_daily.XlSheetCreated += PrintingSystem_XlSheetCreated_Daily_Wise;

                compositeLink_Daily.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        void PrintingSystem_XlSheetCreated_Capcity_Utilization(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Capacity-Utilzation-Count";

            }
            else if (e.Index == 1)
            {
                e.SheetName = "Capacity-Utilzation-Chart";

            }

        }

        void PrintingSystem_XlSheetCreated_8(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Top-Efficiency";
            }


        }

        void PrintingSystem_XlSheetCreated_Pending_Orders(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Pending-Orders";

            }


        }


        void PrintingSystem_XlSheetCreated_Client_reports(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Client Wise Summary";

            }

            if (e.Index == 1)
            {
                e.SheetName = "Orders Details";

            }


        }

        void PrintingSystem_XlSheetCreated_Open_Orders(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Open-Orders";

            }


        }

        void PrintingSystem_XlSheetCreated_Product_Type(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Product-Type";

            }


        }

        void PrintingSystem_XlSheetCreated_Shift_Wise(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Shift-Wise-Count";

            }


        }

        void PrintingSystem_XlSheetCreated_Daily_Wise(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "Daily-Wise-Order-Count";

            }

        }



        //private void pivotGridControl2_Click(object sender, EventArgs e)
        //{
        //    // get date and shift type name  and emp job role


        //    string val_Shift_Type_Name=""; string val_Emp_Job_Role=""; string date="";



        //    int row_Value_Lin_Count = pivotGridField7.RowValueLineCount;
        //    int row_Value_Lin_Count1 = pivotGridField7.ColumnValueLineCount; 

        //    foreach (var field1 in pivotGridControl2.GetFieldsByArea(PivotArea.RowArea))
        //    {
        //        string value_3 = pivotGridControl2.GetFieldValue(pivotGridField7,0).ToString();
        //        val_Shift_Type_Name = value_3;


        //        string value_4 = pivotGridControl2.GetFieldValue(pivotGridField10, 0).ToString();
        //        val_Emp_Job_Role = value_4;

        //      //  string val = pivotGridControl2.GetColumnIndex();
        //    }

        //    foreach (var field1 in pivotGridControl2.GetFieldsByArea(PivotArea.ColumnArea))
        //    {
        //        string Date = pivotGridControl2.GetFieldValue(pivotGridField8, 1).ToString();
        //        date = Date;

        //    }





        //}


        private void pivotGridControl2_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {

                    PivotGridHitInfo hitInfo = pivotGridControlShiftWise1.CalcHitInfo(e.Location);

                    if (hitInfo.ValueInfo != null)
                    {
                        // int index = hitInfo.CellInfo.RowFieldIndex;

                        //string Shift_Type = hitInfo.CellInfo.RowField.Name;

                        object value = hitInfo.ValueInfo.Value;

                        int Max_Row_Index = hitInfo.ValueInfo.MaxIndex;
                        string val_Shift_Type_Name = ""; string val_Emp_Job_Role = ""; string date = ""; string Baranch_Name = "";

                        string Shift_Type = pivotGridControlShiftWise1.GetFieldValue(pivotGridField7, Max_Row_Index).ToString();
                        val_Shift_Type_Name = Shift_Type;


                        string value_4 = pivotGridControlShiftWise1.GetFieldValue(pivotGridField10, Max_Row_Index).ToString();
                        val_Emp_Job_Role = value_4;

                        string Date = pivotGridControlShiftWise1.GetFieldValue(pivotGridField8, 0).ToString();
                        date = Date;

                        string branch_n = pivotGridControlShiftWise1.GetFieldValue(pivotGridField17, Max_Row_Index).ToString();
                        Baranch_Name = branch_n;


                        Hashtable ht_get_jobrole_wise = new Hashtable();
                        DataTable dt_get_jobrole_wise = new DataTable();

                        ht_get_jobrole_wise.Add("@Trans", "EMP_JOB_ROLE_WISE_DETAIL");
                        ht_get_jobrole_wise.Add("@date", date);
                        ht_get_jobrole_wise.Add("@Shift_Type_Name", val_Shift_Type_Name);
                        ht_get_jobrole_wise.Add("@Emp_Job_Role", val_Emp_Job_Role);
                        ht_get_jobrole_wise.Add("@Branch_Name", Baranch_Name);
                        dt_get_jobrole_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get_jobrole_wise);

                        Reports.User_Details_View view_1 = new Reports.User_Details_View(dt_get_jobrole_wise);
                        view_1.Show();

                    }
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occured Please Check With Administrator");
            }

            //PivotGridControl pivot = sender as PivotGridControl;
            // PivotGridHitInfo info = pivot.CalcHitInfo(e.Location);
            //if (info.HitTest == PivotGridHitTest.Value) 
            //// you can process item click here
            //{

            //PivotGridHitInfo hit = pivotGridControl2.CalcHitInfo(pivotGridControl2.PointToClient(MousePosition));
            //if (hit.HitTest == PivotGridHitTest.Cell)
            //{

            // PivotGridHitInfo hit = pivotGridControl2.CalcHitInfo(pivotGridControl2.PointToClient(MousePosition));
            // if (hit.HitTest == PivotGridHitTest.Cell)
            // {

            //  this.Text = Convert.ToString(hit.ValueInfo.Value);

            // string val_Shift_Type_Name; string val_Emp_Job_Role;

            //foreach (var field in pivotGridControl2.GetFieldsByArea(PivotArea.RowArea))
            //{
            //    string value_3 = e.GetFieldValue(pivotGridField7).ToString();
            //    val_Shift_Type_Name = value_3;
            //}

            //foreach (var field in pivotGridControl2.GetFieldsByArea(PivotArea.RowArea))
            //{
            //    if (e.GetFieldValue(pivotGridField10) != null)
            //    {
            //        var value_4 = e.GetFieldValue(pivotGridField10).ToString();
            //        val_Emp_Job_Role = value_4;
            //    }
            //    //var value_4 = e.GetFieldValue(pivotGridField10).ToString();
            //    else
            //    {
            //        val_Emp_Job_Role = null;
            //    }

            // }

            //Hashtable ht_get_jobrole_wise = new Hashtable();
            //System.Data.DataTable dt_get_jobrole_wise = new System.Data.DataTable();

            //ht_get_jobrole_wise.Add("@Trans", "EMP_JOB_ROLE_WISE_DETAIL");
            //ht_get_jobrole_wise.Add("@date", date);
            //ht_get_jobrole_wise.Add("@Shift_Type_Name", val_Shift_Type_Name);
            //ht_get_jobrole_wise.Add("@Emp_Job_Role", val_Emp_Job_Role);
            //dt_get_jobrole_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_jobrole_wise);

            //  Daily_Status_Order_View_Detail shift_deatils = new Order_View_Detail(dt_get_jobrole_wise);

            //  shift_deatils.Show();


            //}


        }

        private void btn_Branch_Cap_City_Submit_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                object obj_4 = ddl_Capacity_Branch.EditValue;
                int Branch_Id = 0;
                string branch_Name = ddl_Capacity_Branch.Text;
                if (obj_4.ToString() != "0")
                {
                    Branch_Id = (int)obj_4;
                }

                object obj_5 = ddl_Shift.EditValue;
                int Shift_Type_Id = 0;

                if (obj_5.ToString() != "0")
                {
                    Shift_Type_Id = (int)obj_5;

                }



                if (Branch_Id != 0 && Shift_Type_Id == 0)
                {
                    // Bind_branch_Wise

                    Bind_Capacity_Utilization_Branch_Wise(Branch_Id);
                }
                else if (Branch_Id != 0 && Shift_Type_Id != 0)
                {

                    Bind_Capacity_Utilization_Branch_Wise(Branch_Id, Shift_Type_Id);
                    // Branch and Shift Wise
                }

                else if (Branch_Id == 0 && Shift_Type_Id == 0)
                {

                    // Bind All branch

                    // Capacity Utilization

                    CapacityUtilization();

                }
            }

            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Updated_Score_Board();
            }
        }

        private bool Validation()
        {
            if (lookUpEditMonth.EditValue.ToString() == "0")
            {
                XtraMessageBox.Show("Select month");
                lookUpEditMonth.Focus();
                return false;
            }
            if (lookUpEditYear.EditValue == null)
            {
                XtraMessageBox.Show("Select year");
                lookUpEditYear.Focus();
                return false;
            }
            return true;
        }
        private void Updated_Score_Board()
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                gridControlScoreBoard.DataSource = null;
                gridViewScoreBoard.Columns.Clear();
                DateTime d1 = DateTime.Now;
                d1 = d1.AddDays(-1);
                Hashtable ht_Score1 = new Hashtable();

                DataTable dt_Score1 = new DataTable();
                dt_Score1.Clear();

                Hashtable ht_Get_User_Orders = new Hashtable();
                DataTable dt_Get_User_Orders = new DataTable();

                if (userRoleId == 2)
                {
                    ht_Get_User_Orders.Add("@Trans", "CALCUATE_MONTHLY_WISE_USER_EFFECINECY_BY_USER_ID_SCORE_BOARD2");
                }
                else
                {
                    ht_Get_User_Orders.Add("@Trans", "CALCUATE_MONTHLY_WISE_USER_EFFECINECY_FOR_SCORE_BOARD2");
                }

                ht_Get_User_Orders.Add("@User_Id", User_id);
                ht_Get_User_Orders.Add("@Month", lookUpEditMonth.EditValue.ToString()); //ddl_Month.SelectedValue.ToString()
                ht_Get_User_Orders.Add("@Years", lookUpEditYear.EditValue.ToString()); //ddl_Year.SelectedValue.ToString()
                dt_Get_User_Orders = dataaccess.ExecuteSP("Sp_Score_Board", ht_Get_User_Orders);


                Hashtable ht_Score = new Hashtable();

                DataTable dt_Score = new DataTable();
                DataTable dt_Score2 = new DataTable();
                dt_Score2.Clear();




                if (userRoleId == 2)
                {
                    ht_Score.Add("@Trans", "GET_USER_NEWLY_UPDATED_ORDER_EFFECINECY_USER_ID_WISE");
                }
                else
                {

                    // Insert to temp table data
                    Hashtable ht_Temp_Score = new Hashtable();

                    DataTable dt_Temp_Score = new DataTable();
                    ht_Temp_Score.Add("@Trans", "INSERT_EFF_TO_TEMP");
                    dt_Temp_Score = dataaccess.ExecuteSP("Sp_Score_Board", ht_Temp_Score);

                    if (checkEditProductionTimeWise.Checked == true)
                    {
                        ht_Score.Add("@Trans", "GET_USER_UPDATED_NEW_ORDER_EFFECINECY");
                    }
                    else if (checkEditTargetWise.Checked == true)
                    {
                        ht_Score.Add("@Trans", "GET_USER_UPDATED_TARGET_WISE_ORDER_EFFECINECY");
                    }
                }

                ht_Score.Add("@User_Id", User_id);
                dt_Score2 = dataaccess.ExecuteSP("Sp_Score_Board", ht_Score);

                Hashtable htget_Avg_Total_eff = new Hashtable();
                DataTable dtget_Avg_Total_Eff = new DataTable();

                if (userRoleId == 2)
                {
                    htget_Avg_Total_eff.Add("@Trans", "GET_NEWLY_UPDATED_AVG_EFF_BY_USER_ID");
                }
                else
                {

                    if (checkEditProductionTimeWise.Checked == true)
                    {
                        htget_Avg_Total_eff.Add("@Trans", "GET_NEWLY_UPDATED_AVG_EFF");
                    }
                    else if (checkEditTargetWise.Checked == true)
                    {

                        htget_Avg_Total_eff.Add("@Trans", "GET_NEWLY_TARGETED_UPDATED_AVG_EFF");
                    }

                }
                htget_Avg_Total_eff.Add("@User_Id", User_id);
                dtget_Avg_Total_Eff = dataaccess.ExecuteSP("Sp_Score_Board", htget_Avg_Total_eff);

                DataTable dt_Final_Score1 = new DataTable();

                var collection = from t1 in dt_Score2.AsEnumerable()
                                 join t2 in dtget_Avg_Total_Eff.AsEnumerable()
                                   on t1["User_Id"] equals t2["User_Id"]
                                 select new
                                 {
                                     User_Id = t1["User_Id"],
                                     User_Name = t1["User_Name"],
                                     Branch_Name = t1["Branch_Name"],
                                     DRN_Emp_Code = t1["DRN_Emp_Code"],
                                     Emp_Job_Role = t1["Emp_Job_Role"],
                                     Operation_Type = t1["Operation_Type"],
                                     Shift_Type_Name = t1["Shift_Type_Name"],
                                     Reporting_To_1 = t1["Reporting_To_1"],
                                     Reporting_To_2 = t1["Reporting_To_2"],
                                     Avg_Eff = t2["Avg_Eff"],
                                     D1 = t1["1"],
                                     D2 = t1["2"],
                                     D3 = t1["3"],
                                     D4 = t1["4"],
                                     D5 = t1["5"],
                                     D6 = t1["6"],
                                     D7 = t1["7"],
                                     D8 = t1["8"],
                                     D9 = t1["9"],
                                     D10 = t1["10"],
                                     D11 = t1["11"],
                                     D12 = t1["12"],
                                     D13 = t1["13"],
                                     D14 = t1["14"],
                                     D15 = t1["15"],
                                     D16 = t1["16"],
                                     D17 = t1["17"],
                                     D18 = t1["18"],
                                     D19 = t1["19"],
                                     D20 = t1["20"],
                                     D21 = t1["21"],
                                     D22 = t1["22"],
                                     D23 = t1["23"],
                                     D24 = t1["24"],
                                     D25 = t1["25"],
                                     D26 = t1["26"],
                                     D27 = t1["27"],
                                     D28 = t1["28"],
                                     D29 = t1["29"],
                                     D30 = t1["30"],
                                     D31 = t1["31"],
                                 };
                DataTable result = new DataTable("Final_Data");
                result.Columns.Add("User_Id", typeof(string));
                result.Columns.Add("User_Name", typeof(string));
                result.Columns.Add("Branch_Name", typeof(string));
                result.Columns.Add("DRN_Emp_Code", typeof(string));
                result.Columns.Add("Emp_Job_Role", typeof(string));
                result.Columns.Add("Operation_Type", typeof(string));
                result.Columns.Add("Shift_Type_Name", typeof(string));
                result.Columns.Add("Reporting_To_1", typeof(string));
                result.Columns.Add("Reporting_To_2", typeof(string));
                result.Columns.Add("Avg_Eff", typeof(int));

                DataTable dt_Final_Score = new DataTable();
                Hashtable htgetfirst_Last_Dates = new Hashtable();
                DataTable dtgetfirst_last_Dates = new DataTable();

                htgetfirst_Last_Dates.Add("@Trans", "GET_FIRST_LAST_DATE");
                htgetfirst_Last_Dates.Add("@Month", int.Parse(lookUpEditMonth.EditValue.ToString()));
                htgetfirst_Last_Dates.Add("@Years", int.Parse(lookUpEditYear.EditValue.ToString()));
                dtgetfirst_last_Dates = dataaccess.ExecuteSP("Sp_Score_Board", htgetfirst_Last_Dates);

                Hashtable htdatecolumn = new Hashtable();

                DataTable dtdatecolumn = new DataTable();
                htdatecolumn.Add("@Trans", "GET_DATES");
                htdatecolumn.Add("@date_from1", dtgetfirst_last_Dates.Rows[0]["First_Date"].ToString());
                htdatecolumn.Add("@date_to1", dtgetfirst_last_Dates.Rows[0]["Last_Date"].ToString());
                dtdatecolumn = dataaccess.ExecuteSP("Sp_Score_Board", htdatecolumn);
                foreach (DataRow row in dtdatecolumn.Rows)
                {
                    result.Columns.Add(row["dts"].ToString(), typeof(object));
                }
                if (lookUpEditMonth.EditValue.ToString() == "2")
                {
                    if (result.Columns.Contains("29/02"))
                    {
                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28, item.D29);
                        }
                    }
                    else
                    {
                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28);
                        }
                    }
                }
                else
                {
                    if (result.Columns.Contains("31/" + GetDigitAppended(lookUpEditMonth.EditValue.ToString())))
                    {
                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28, item.D29, item.D30, item.D31);
                        }
                    }
                    else
                    {

                        foreach (var item in collection)
                        {
                            result.Rows.Add(item.User_Id, item.User_Name, item.Branch_Name, item.DRN_Emp_Code, item.Emp_Job_Role, item.Operation_Type, item.Shift_Type_Name, item.Reporting_To_1, item.Reporting_To_2, item.Avg_Eff, item.D1, item.D2, item.D3, item.D4, item.D5, item.D6, item.D7, item.D8, item.D9, item.D10,
                                item.D11, item.D12, item.D13, item.D14, item.D15, item.D16, item.D17, item.D18, item.D19, item.D20, item.D21, item.D22, item.D23, item.D24, item.D25, item.D26, item.D27, item.D28, item.D29, item.D30);
                        }
                    }


                }

                if (result.Rows.Count > 0)
                {
                    gridControlScoreBoard.DataSource = result;
                    gridViewScoreBoard.Columns.ColumnByFieldName("User_Id").Visible = false;
                    gridViewScoreBoard.Columns.ColumnByFieldName("User_Name").OptionsColumn.AllowFocus = false;
                    gridViewScoreBoard.BestFitColumns();
                    //  gridViewScoreBoard.ShowFindPanel();
                    foreach (GridColumn column in ((DevExpress.XtraGrid.Views.Base.ColumnView)gridControlScoreBoard.Views[0]).Columns)
                    {
                        if (column.FieldName == "User_Name") column.Fixed = FixedStyle.Left;
                        if (column.FieldName == "Avg_Eff") column.Fixed = FixedStyle.Left;
                        {

                            column.FilterMode = ColumnFilterMode.Value;
                            column.SortMode = ColumnSortMode.Value;

                            column.OptionsColumn.AllowSort = DefaultBoolean.True;
                            column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

                        }
                        if (column.FieldName != "User_Name" && column.FieldName != "Branch_Name" && column.FieldName != "DRN_Emp_Code"
                            && column.FieldName != "Emp_Job_Role" && column.FieldName != "Shift_Type_Name" && column.FieldName != "Reporting_To_1" &&
                            column.FieldName != "Reporting_To_2" && column.FieldName != "Avg_Eff" && column.FieldName != "Operation_Type")
                        {
                            RepositoryItemHyperLinkEdit edit = new RepositoryItemHyperLinkEdit();
                            edit.Appearance.ForeColor = System.Drawing.Color.Blue;
                            edit.Appearance.Options.UseForeColor = true;
                            column.ColumnEdit = edit;
                            column.FilterMode = ColumnFilterMode.Value;
                            column.SortMode = ColumnSortMode.Value;
                            column.OptionsColumn.AllowSort = DefaultBoolean.True;
                            column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        }
                    }
                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Data not found");
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong check with admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private string GetDigitAppended(string p)
        {
            int count = (int)Math.Log10((Convert.ToInt32(p))) + 1;
            if (count > 1) { return p.ToString(); }
            else
            {
                return "0" + p;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gridControlScoreBoard.DataSource = null;
            gridViewScoreBoard.Columns.Clear();
            checkEditProductionTimeWise.Checked = false;
            checkEditTargetWise.Checked = false;
            lookUpEditMonth.EditValue = 0;
            lookUpEditYear.EditValue = null;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (gridViewScoreBoard.RowCount > 0)
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                DataTable dtRows = GetDataTable(gridViewScoreBoard);
                dtRows.Columns.Remove("User_Id");
                try
                {
                    string fileName;

                    string filePath = @"C:\Efficiency\";
                    if (checkEditProductionTimeWise.Checked == true)
                    {
                        fileName = filePath + "Efficiency - " + checkEditProductionTimeWise.Text.ToString() + " " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    }
                    else if (checkEditTargetWise.Checked == true)
                    {
                        fileName = filePath + "Efficiency - " + checkEditTargetWise.Text.ToString() + " " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    }
                    else
                    {
                        fileName = filePath + "Efficiency - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    }
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    else
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dtRows, "Efficiency");
                            wb.SaveAs(fileName);
                        }
                        SplashScreenManager.CloseForm(false);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
                catch (Exception)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Somthing went wrong while exporting data");
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
            else
            {
                XtraMessageBox.Show("Data not found");
            }
        }

        private DataTable GetDataTable(GridView view)
        {
            DataTable dt = new DataTable();
            foreach (GridColumn c in view.Columns)
                dt.Columns.Add(c.FieldName, c.ColumnType);
            for (int r = 0; r < view.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                    rowValues[c] = view.GetRowCellValue(r, dt.Columns[c].ColumnName);
                dt.Rows.Add(rowValues);
            }
            return dt;
        }

        private void gridViewScoreBoard_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName != "User_Name" && e.Column.FieldName != "Branch_Name" && e.Column.FieldName != "Branch_Name" && e.Column.FieldName != "DRN_Emp_Code"
                          && e.Column.FieldName != "Emp_Job_Role" && e.Column.FieldName != "Shift_Type_Name" && e.Column.FieldName != "Reporting_To_1" &&
                          e.Column.FieldName != "Reporting_To_2" && e.Column.FieldName != "Avg_Eff" && e.Column.FieldName != "Operation_Type")
                {
                    if (String.IsNullOrEmpty(gridViewScoreBoard.GetRowCellValue(e.RowHandle, gridViewScoreBoard.Columns.ColumnByFieldName(e.Column.FieldName)).ToString()))
                    {
                        return;
                    }
                    int userId = Convert.ToInt32(gridViewScoreBoard.GetRowCellValue(e.RowHandle, "User_Id"));
                    string date;
                    string avgEfficiency = gridViewScoreBoard.GetRowCellValue(e.RowHandle, gridViewScoreBoard.Columns.ColumnByFieldName(e.Column.FieldName)).ToString();
                    if (lookUpEditYear.EditValue != null)
                    {
                        date = DateTime.ParseExact(e.Column.FieldName + "/" + lookUpEditYear.EditValue.ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        XtraMessageBox.Show("year not selected");
                        return;
                    }
                    var ht = new Hashtable();
                    ht.Add("@Trans", "INSERT_INTO_TEMP_USER");
                    ht.Add("@Production_Date", date);
                    ht.Add("@User_Id", userId);
                    var dt = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", ht);
                    Ordermanagement_01.Dashboard.Emp_Production_Score_Board TargeDashboard = new Ordermanagement_01.Dashboard.Emp_Production_Score_Board(userId, userRoleId.ToString(), date.ToString(), avgEfficiency);
                    TargeDashboard.Show();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Something went wrong");
            }
        }

        private void gridViewScoreBoard_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void checkEditProductionTimeWise_CheckedChanged(object sender, EventArgs e)
        {
            checkEditTargetWise.Checked = false;
        }

        private void checkEditTargetWise_CheckedChanged(object sender, EventArgs e)
        {
            checkEditProductionTimeWise.Checked = false;
        }

        private void checkedLstBxCntrl_Branch_Wise_MouseClick(object sender, MouseEventArgs e)
        {
            string check_Status = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();

            for (int i = 0; i < checkedLstBxCntrl_Branch_Wise.CheckedItems.Count; i++)
            {

                string Checked_Item_Name = checkedLstBxCntrl_Branch_Wise.CheckedItems[i].ToString();

            }

            int Items_Count = 0;
            int Selcetd_Items_Count = 0;

            if (check_Status == "Checked")
            {
                Items_Count = 1;
                Selcetd_Items_Count = checkedLstBxCntrl_Branch_Wise.CheckedItems.Count;
            }
            else
            {
                check_Status = "Unchecked";

                Selcetd_Items_Count = checkedLstBxCntrl_Branch_Wise.CheckedItems.Count;
                Items_Count = 0;
            }

            //string Check_Items = checkedLstBxCntrl_Branch_Wise.SelectedItem.ToString();


            if (check_Status == "Checked")
            {

                for (int i = 1; i < checkedLstBxCntrl_Branch_Wise.Items.Count; i++)
                {

                    checkedLstBxCntrl_Branch_Wise.SetItemChecked(i, true);
                }



            }
            else if (check_Status == "Unchecked" && Selcetd_Items_Count == 2)
            {


                for (int i = 1; i < checkedLstBxCntrl_Branch_Wise.Items.Count; i++)
                {

                    checkedLstBxCntrl_Branch_Wise.SetItemChecked(i, false);
                }

            }
        }

        private void btn_Clear_Top_Eff_Click(object sender, EventArgs e)
        {
            string D2 = DateTime.Now.ToString("M/d/yyyy");
            string D3 = DateTime.Now.ToString("MM/dd/yyyy");
            dateEdit_From_date.Text = D2;
            dateEdit_To_Date.Text = D2;
            // dateEdit_Current_Date_ShiftWise.Text = D3;
            dateEdit1_Current_Date_Top_Eff.Text = D3;

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pivotGridControlDailyWise_Click(object sender, EventArgs e)
        {


        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 1)
            {
                string D3 = DateTime.Now.ToString("MM/dd/yyyy");
                dateEdit1_Current_Date_Top_Eff.Text = D3;
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        //private void pivotGridControl2_CustomExportFieldValue(object sender, CustomExportFieldValueEventArgs e)
        //{
        //    if (e.Field.FieldName == "No_Of_Orders")
        //    {
        //        // e.Cell.Style["mso-number-format"] = "{0}";
        //        e.Brick.Text = e.Text;
        //        e.Brick.TextValue = e.Text;
        //    }
        //    else
        //    {
        //        e.Brick.Text = e.Text;
        //        e.Brick.TextValue = e.Text;
        //    }
        //}


        public void Bind_Shift_Type_Master()
        {
            Hashtable ht_shift = new Hashtable();
            DataTable dt_shift = new DataTable();



            ht_shift.Add("@Trans", "SELECT_SHIFT_TYPE_MASTER");
            dt_shift = dataaccess.ExecuteSP("Sp_User", ht_shift);
            DataRow dr = dt_shift.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_shift.Rows.InsertAt(dr, 0);

            ddl_Shift.Properties.DataSource = dt_shift;
            ddl_Shift.Properties.DisplayMember = "Shift_Type_Name";
            ddl_Shift.Properties.ValueMember = "Shift_Type_Id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Shift_Type_Name", 100);
            ddl_Shift.Properties.Columns.Add(col);
        }

        private void btn_Capacity_Clear_Click(object sender, EventArgs e)
        {
            ddl_Capacity_Branch.EditValue = 0;
            ddl_Shift.EditValue = 0;

        }




        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
        //    try
        //    {
        //        Userid_value = 0;

        //        object obj_shift = lookUpEdit_Branch.EditValue;
        //        string Username_shiftwise = lookUpEdit_Branch.Text;
        //        if (obj_shift != null)
        //        {
        //            if (obj_shift.ToString() != "0")
        //            {
        //                Userid_value = (int)obj_shift;
        //            }
        //        }
        //        if (Userid_value != 0 && dateEdit_Current_Date_ShiftWise.Text != "")
        //        {
        //            ShiftType_MANGER_And_Current_Date_Wise(Userid_value);
        //        }
        //        else if (Userid_value == 0 && dateEdit_Current_Date_ShiftWise.Text != "")
        //        {
        //            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
        //            try
        //            {
        //                //ShiftType_Wise_Current_Date1();
        //            }
        //            finally
        //            {
        //                //Close Wait Form
        //                SplashScreenManager.CloseForm(false);

        //            }
        //        }

        //    }
        //    finally
        //    {
        //        //Close Wait Form
        //        SplashScreenManager.CloseForm(false);
        //    }
        //}

        private void lookUpEditAllEmpProdClientName_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditAllEmpProdClientName.EditValue) > 0)
            {
                if (userRoleId == 1)
                {
                    Editvalue = int.Parse(lookUpEditAllEmpProdClientName.EditValue.ToString());
                    BindSubProcessName(lookUpEditAllEmpProdSubProcess, Editvalue);
                }
                else
                {
                    Editvalue = int.Parse(lookUpEditAllEmpProdClientName.EditValue.ToString());
                    BindSubProcessNumber(lookUpEditAllEmpProdSubProcess, Editvalue);
                }
            }
            else
            {
                lookUpEditAllEmpProdSubProcess.Properties.DataSource = null;
            }
        }

        private void lookUpEditAllClientName_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditAllClientName.EditValue) > 0)
            {
                if (userRoleId == 1)
                {
                    Editvalue = int.Parse(lookUpEditAllClientName.EditValue.ToString());
                    BindSubProcessName(lookUpEditAllClientSubProcess, Editvalue);
                }
                else
                {
                    Editvalue = int.Parse(lookUpEditAllClientName.EditValue.ToString());
                    BindSubProcessNumber(lookUpEditAllClientSubProcess, Editvalue);
                }
            }
            else
            {
                lookUpEditAllClientSubProcess.Properties.DataSource = null;
            }
        }

        private void dateEdit_From_date_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateEdit_To_Date_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pivotGridControl9_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "Order_Status" && e.Field.FieldName != null)
            {
                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "Order_Status_ID"),
                     orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "Order_Status_ID");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
                e.Handled = true;
            }
        }

        private void pivotGridControl9_CellClick(object sender, PivotCellEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                string Tab_Name = "";
                Tab_Name = "Shift Wise";
                string Column_Name = "";
                string Row_Value_Type = "";
                string Column_Value_Type = "";
                string Shift_Type_Name = "";
                string Emp_Job_Role = "";
                string Date = "";
                string Order_Status = "";
                string V_Data = "";
                string val_order_Status = "";
                string val_Cda = "";
                string val_Shift_Type_Name = "";
                string val_Emp_Job_Role = "";
                string Val_Branch_Name = "";
                //string Val_Branch_Name_1 = "";

                string Check_Branch_All = checkedLstBxCntrl_Branch_Wise.Items[0].CheckState.ToString();
                string Check_Banglore = checkedLstBxCntrl_Branch_Wise.Items[1].CheckState.ToString();
                string Check_Hosur = checkedLstBxCntrl_Branch_Wise.Items[2].CheckState.ToString();
                string checked_Item = "";

                if (Check_Branch_All == "Checked" && Check_Banglore == "Checked" && Check_Hosur == "Checked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[0].Description.ToString();
                }
                else if (Check_Banglore == "Checked" && Check_Branch_All == "Unchecked" && Check_Hosur == "Unchecked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[1].Description.ToString();
                }
                else if (Check_Hosur == "Checked" && Check_Branch_All == "Unchecked" && Check_Banglore == "Unchecked")
                {
                    checked_Item = checkedLstBxCntrl_Branch_Wise.Items[2].Description.ToString();
                }

                Val_Branch_Name_1 = checked_Item;


                PivotGridHitInfo hit = pivotGridControlShiftWise.CalcHitInfo(pivotGridControlShiftWise.PointToClient(MousePosition));
                if (hit.HitTest == PivotGridHitTest.Cell)
                {

                    Column_Name = hit.CellInfo.DataField.FieldName.ToString();

                    Row_Value_Type = hit.CellInfo.RowValueType.ToString();
                    Column_Value_Type = hit.CellInfo.ColumnValueType.ToString();

                    if (e.GetFieldValue(pivotGridField67) != null)
                    {
                        V_Data = e.GetFieldValue(pivotGridField67).ToString().Trim();
                    }
                    else
                    {

                        V_Data = "0";
                    }

                    foreach (var field in pivotGridControlShiftWise.GetFieldsByArea(PivotArea.ColumnArea))
                    {
                        if (e.GetFieldValue(pivotGridField64) != null)
                        {
                            string value1 = e.GetFieldValue(pivotGridField64).ToString();
                            val_Cda = value1;
                        }


                    }
                    foreach (var field in pivotGridControlShiftWise.GetFieldsByArea(PivotArea.ColumnArea))
                    {
                        if (e.GetFieldValue(pivotGridField63) != null)
                        {
                            string value2 = e.GetFieldValue(pivotGridField63).ToString();
                            val_order_Status = value2;
                        }
                        else
                        {

                            val_order_Status = "";
                        }

                    }
                    foreach (var field in pivotGridControlShiftWise.GetFieldsByArea(PivotArea.RowArea))
                    {
                        if (e.GetFieldValue(pivotGridField65) != null)
                        {
                            string value_3 = e.GetFieldValue(pivotGridField65).ToString();
                            val_Shift_Type_Name = value_3;
                        }
                        else
                        {
                            val_Shift_Type_Name = "";
                        }
                    }

                    foreach (var field in pivotGridControlShiftWise.GetFieldsByArea(PivotArea.RowArea))
                    {
                        if (e.GetFieldValue(pivotGridField70) != null)
                        {
                            string value_4 = e.GetFieldValue(pivotGridField70).ToString();
                            Val_Branch_Name = value_4;
                        }
                    }

                    foreach (var field in pivotGridControlShiftWise.GetFieldsByArea(PivotArea.RowArea))
                    {
                        if (e.GetFieldValue(pivotGridField66) != null)
                        {
                            var value_4 = e.GetFieldValue(pivotGridField66).ToString();
                            val_Emp_Job_Role = value_4;
                        }
                        else
                        {
                            val_Emp_Job_Role = "";
                        }
                    }
                    //1



                    if (Row_Value_Type == "Value" && Column_Value_Type == "Value")
                    {

                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();


                        if (V_Data != "" && V_Data != "0" && val_Emp_Job_Role != null)
                        {
                            Emp_Job_Role = val_Emp_Job_Role.ToString();
                            Hashtable ht_get_Shiftdetails = new Hashtable();
                            DataTable dt_get_Shiftdetails = new DataTable();
                            ht_get_Shiftdetails.Clear();
                            dt_get_Shiftdetails.Clear();

                            ht_get_Shiftdetails.Add("@Trans", "SHIFT_WISE_BY_ORDER_DETIALS_VIEW_SINGLE_DAY");
                            ht_get_Shiftdetails.Add("@Order_Status", Order_Status);
                            ht_get_Shiftdetails.Add("@date", Date);
                            ht_get_Shiftdetails.Add("@Branch", Val_Branch_Name);
                            ht_get_Shiftdetails.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shiftdetails.Add("@Emp_Job_Role", Emp_Job_Role);
                            dt_get_Shiftdetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shiftdetails);


                            HeaderText = $"{Tab_Name} - {Val_Branch_Name} - {Shift_Type_Name} - {val_order_Status} Orders on {Date}";

                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date, dt_get_Shiftdetails.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_Shiftdetails.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, Date, Tab_Name, HeaderText);

                            shift_deatils.Show();

                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //2
                    if (Row_Value_Type == "Total" && Column_Value_Type == "Value")// Shift & Task Wise  
                    {


                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }

                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();


                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shiftdetails = new Hashtable();
                            DataTable dt_get_Shiftdetails = new DataTable();
                            ht_get_Shiftdetails.Clear();
                            dt_get_Shiftdetails.Clear();

                            //---------------------new
                            // branch and order status
                            if (Shift_Type_Name == "" && Emp_Job_Role == "" && Val_Branch_Name != "" && Val_Branch_Name_1 == "ALL" && Order_Status != "")
                            {
                                ht_get_Shiftdetails.Add("@Trans", "SHIFT_WISE_BY_ORDER_DETIALS_VIEW_SHIFT_WISE_SINGLE_DAY_BRANCH_WISE_1");

                            }
                            else if (Shift_Type_Name == "" && Emp_Job_Role == "" && Val_Branch_Name != "" && Val_Branch_Name_1 != "ALL" && Order_Status != "")
                            {
                                ht_get_Shiftdetails.Add("@Trans", "SHIFT_WISE_BY_ORDER_DETIALS_VIEW_SHIFT_WISE_SINGLE_DAY_BRANCH_WISE_1");

                            }

                            // Branch and shift type and order status
                            else if (Shift_Type_Name != "" && Emp_Job_Role == "" && Val_Branch_Name != "" && Val_Branch_Name_1 != "ALL" && Order_Status != "")
                            {
                                ht_get_Shiftdetails.Add("@Trans", "SHIFT_WISE_BY_ORDER_DETIALS_VIEW_SHIFT_WISE_SINGLE_DAY_BRANCH_WISE_2");

                            }
                            else if (Shift_Type_Name != "" && Emp_Job_Role == "" && Val_Branch_Name != "" && Val_Branch_Name_1 == "ALL" && Order_Status != "")
                            {
                                ht_get_Shiftdetails.Add("@Trans", "SHIFT_WISE_BY_ORDER_DETIALS_VIEW_SHIFT_WISE_SINGLE_DAY_BRANCH_WISE_2");

                            }

                            ht_get_Shiftdetails.Add("@Order_Status", Order_Status);
                            ht_get_Shiftdetails.Add("@date", Date);
                            ht_get_Shiftdetails.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_get_Shiftdetails.Add("@Emp_Job_Role", Emp_Job_Role);
                            ht_get_Shiftdetails.Add("@Branch", Val_Branch_Name);


                            dt_get_Shiftdetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shiftdetails);

                            HeaderText = $"{Tab_Name} - {Val_Branch_Name} - {Shift_Type_Name} - ALL {val_order_Status} Orders on {Date}";
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date, dt_get_Shiftdetails.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_Shiftdetails.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);
                            shift_deatils.Show();

                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //3
                    if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "Value")//All Shift and Task Wise  
                    {
                        DataTable dt_Get_Details = new DataTable();
                        dt_Get_Details.Clear();
                        dt_Get_Details = dbc.dt_Get_Details_For_Order_Status_Report("", val_order_Status);
                        if (dt_Get_Details.Rows.Count > 0)
                        {
                            Order_Status = dt_Get_Details.Rows[0][0].ToString();
                        }
                        Date = val_Cda.ToString();
                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shiftdetails = new Hashtable();
                            DataTable dt_get_Shiftdetails = new DataTable();
                            ht_get_Shiftdetails.Clear();
                            dt_get_Shiftdetails.Clear();
                            // new 11-july-2019

                            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Shift_Current_Date_Status == "False" && Val_Branch_Name_1 == "ALL" && Order_Status != "" && Val_Branch_Name == "")
                            {
                                ht_get_Shiftdetails.Add("@Trans", "SHIFT_FROM_TODATE_BOTH_BRANCH_WISE");
                                ht_get_Shiftdetails.Add("@date", Date);
                            }
                            // 16th july 2019
                            else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Shift_Current_Date_Status == "False" && Val_Branch_Name_1 != "ALL" && Order_Status != "" && Val_Branch_Name == "")
                            {

                                ht_get_Shiftdetails.Add("@Trans", "SHIFT_FROM_TODATE_SINGLE_BRANCH_WISE");
                                ht_get_Shiftdetails.Add("@Branch", Val_Branch_Name_1);
                                ht_get_Shiftdetails.Add("@date", Date);
                            }


                            // new current date wise
                            else if (dateEditShiftWiseCurrent.Text != "" && Val_Branch_Name == "" && Order_Status != "" && Val_Branch_Name_1 != "ALL" && Shift_Current_Date_Status == "True")
                            {
                                ht_get_Shiftdetails.Add("@Trans", "ALL_SHIFT_WISE_SINGLE_DAY_BRANCH_WISE");
                                ht_get_Shiftdetails.Add("@Branch", Val_Branch_Name_1);
                                ht_get_Shiftdetails.Add("@date", dateEditShiftWiseCurrent.Text);
                            }
                            else if (dateEditShiftWiseCurrent.Text != "" && Val_Branch_Name == "" && Order_Status != "" && Val_Branch_Name_1 == "ALL" && Shift_Current_Date_Status == "True")
                            {
                                ht_get_Shiftdetails.Add("@Trans", "ALL_SHIFT_TASK_WISE_SINGLE_DAY_ALL_BRANCH_WISE");
                                ht_get_Shiftdetails.Add("@date", dateEditShiftWiseCurrent.Text);

                            }
                            ht_get_Shiftdetails.Add("@Order_Status", Order_Status);
                            //ht_get_Shiftdetails.Add("@date", dateEdit_Current_Date_ShiftWise.Text);

                            //ht_get_Shiftdetails.Add("@Fromdate", dateEdit_From_date.Text);
                            //ht_get_Shiftdetails.Add("@Todate", dateEdit_To_Date.Text);
                            dt_get_Shiftdetails = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shiftdetails);

                            HeaderText = $"{Tab_Name} - ALL - {val_order_Status} Orders on {Date}";
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shiftdetails, userroleid, User_id, Production_date, dt_get_Shiftdetails.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_Shiftdetails.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);
                            shift_deatils.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }

                    //4
                    if (Row_Value_Type == "GrandTotal" && Column_Value_Type == "GrandTotal")// All Date and All Task and All Shift Wise Total
                    {
                        V_Data = e.GetFieldValue(pivotGridField67).ToString().Trim();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift_Current_date_wise = new Hashtable();
                            DataTable dt_get_Shift_Current_date_wise = new DataTable();
                            ht_get_Shift_Current_date_wise.Clear();
                            dt_get_Shift_Current_date_wise.Clear();

                            // 16th july
                            if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Val_Branch_Name_1 == "ALL" && Shift_Current_Date_Status == "False")
                            {

                                ht_get_Shift_Current_date_wise.Add("@Trans", "SHIFT_FROM_TODATE_AND_ALL_BRANCH_WISE");
                                ht_get_Shift_Current_date_wise.Add("@Fromdate", dateEdit_From_date.Text);
                                ht_get_Shift_Current_date_wise.Add("@Todate", dateEdit_To_Date.Text);
                            }
                            else if (dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Val_Branch_Name_1 != "ALL" && Shift_Current_Date_Status == "False")
                            {

                                ht_get_Shift_Current_date_wise.Add("@Trans", "SHIFT_FROM_TODATE_AND_BRANCH_WISE");
                                ht_get_Shift_Current_date_wise.Add("@Fromdate", dateEdit_From_date.Text);
                                ht_get_Shift_Current_date_wise.Add("@Todate", dateEdit_To_Date.Text);
                                ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name_1);
                            }



                            // old current date wise
                            else if (Val_Branch_Name_1 == "ALL" && dateEditShiftWiseCurrent.Text != "" && Shift_Current_Date_Status == "True")
                            {
                                ht_get_Shift_Current_date_wise.Add("@Trans", "SHIFT_CURRENTDATE_WISE_ALL_BRANCH_WISE");

                            }
                            else if (Val_Branch_Name_1 != "ALL" && dateEditShiftWiseCurrent.Text != "" && Shift_Current_Date_Status == "True")
                            {

                                ht_get_Shift_Current_date_wise.Add("@Trans", "SHIFT_CURRENTDATE_WISE_BRANCH_WISE");
                                ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name_1);

                            }

                            ht_get_Shift_Current_date_wise.Add("@date", dateEditShiftWiseCurrent.Text);
                            //ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                            dt_get_Shift_Current_date_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift_Current_date_wise);


                            HeaderText = $"{Tab_Name} - ALL Orders";
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date, dt_get_Shift_Current_date_wise.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_Shift_Current_date_wise.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);
                            shift_deatils.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }

                    //5
                    if (Row_Value_Type == "Total" && Column_Value_Type == "GrandTotal")// shift and Date Wise All Task Total
                    {

                        V_Data = e.GetFieldValue(pivotGridField67).ToString().Trim();

                        foreach (var field in pivotGridControlShiftWise.GetFieldsByArea(PivotArea.RowArea))
                        {
                            if (e.GetFieldValue(pivotGridField65) != null)
                            {
                                val_Shift_Type_Name = "";
                                string value_6 = e.GetFieldValue(pivotGridField65).ToString();
                                val_Shift_Type_Name = value_6;
                            }
                            else
                            {
                                val_Shift_Type_Name = "";
                            }

                        }

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_get_Shift_Current_date_wise = new Hashtable();
                            DataTable dt_get_Shift_Current_date_wise = new DataTable();
                            ht_get_Shift_Current_date_wise.Clear();
                            dt_get_Shift_Current_date_wise.Clear();

                            string s_Date1 = "";
                            string date1 = dateEditShiftWiseCurrent.Text;

                            if (date1 != "")
                            {
                                DateTime currentdate = DateTime.ParseExact(date1, "M/d/yyyy", CultureInfo.InvariantCulture);
                                s_Date1 = currentdate.ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                s_Date1 = "";
                            }

                            // new july 15 from date and todate wise

                            if (val_Shift_Type_Name == "" && Val_Branch_Name != "" && dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && s_Date1 == "")
                            {
                                ht_get_Shift_Current_date_wise.Add("@Trans", "FROM_AND_TO_DATE_AND_BRANCH_WISE");
                                ht_get_Shift_Current_date_wise.Add("@Fromdate", dateEdit_From_date.Text);
                                ht_get_Shift_Current_date_wise.Add("@Todate", dateEdit_To_Date.Text);
                                ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                            }

                            if (val_Shift_Type_Name != "" && Val_Branch_Name != "" && dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && s_Date1 == "")
                            {
                                ht_get_Shift_Current_date_wise.Add("@Trans", "FROM_AND_TO_DATE_AND_BRANCH_AND_SHIFT_WISE");
                                ht_get_Shift_Current_date_wise.Add("@Fromdate", dateEdit_From_date.Text);
                                ht_get_Shift_Current_date_wise.Add("@Todate", dateEdit_To_Date.Text);
                                ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                            }

                            // old onr current date wise
                            else if (val_Shift_Type_Name != "" && s_Date1 != "" && Shift_Current_Date_Status == "True")
                            {
                                ht_get_Shift_Current_date_wise.Add("@Trans", "CURRENT_DATE_AND_SHIFT_TYPE_WISE");
                                ht_get_Shift_Current_date_wise.Add("@date", s_Date1);
                                ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                            }
                            else if (Shift_Type_Name == "" && Val_Branch_Name != "" && s_Date1 != "" && Shift_Current_Date_Status == "True")
                            {

                                ht_get_Shift_Current_date_wise.Add("@Trans", "ALL_SHIFT_WISE_BRANCH_WISE_SINGLE_DAY_TOTAL");
                                ht_get_Shift_Current_date_wise.Add("@date", s_Date1);
                                ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                            }

                            //ht_get_Shift_Current_date_wise.Add("@date", s_Date1);
                            ht_get_Shift_Current_date_wise.Add("@Shift_Type_Name", val_Shift_Type_Name);
                            //ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                            dt_get_Shift_Current_date_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift_Current_date_wise);

                            HeaderText = $"{Tab_Name} - {Val_Branch_Name} - {val_Shift_Type_Name} ALL Orders";
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date, dt_get_Shift_Current_date_wise.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_Shift_Current_date_wise.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);
                            shift_deatils.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }

                    }
                    //6
                    if (Row_Value_Type == "Value" && Column_Value_Type == "GrandTotal")  // shift and Date Wise ,Emp_Job_Role - Client Wise All Task
                    {
                        V_Data = e.GetFieldValue(pivotGridField67).ToString().Trim();

                        Shift_Type_Name = val_Shift_Type_Name.ToString();
                        Emp_Job_Role = val_Emp_Job_Role.ToString();

                        if (V_Data != "" && V_Data != "0")
                        {
                            Hashtable ht_getdetail = new Hashtable();
                            DataTable dt_getdetail = new DataTable();
                            //ht_get.Clear();
                            //dt_get.Clear();
                            string s_Date1 = "";
                            string date1 = dateEditShiftWiseCurrent.Text;

                            if (date1 != "")
                            {
                                DateTime currentdate = DateTime.ParseExact(date1, "M/d/yyyy", CultureInfo.InvariantCulture);
                                s_Date1 = currentdate.ToString("MM/dd/yyyy");
                            }
                            else
                            {
                                s_Date1 = "";

                            }

                            // new 15 july
                            if (Shift_Current_Date_Status == "False" && dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Val_Branch_Name != "" && Val_Branch_Name_1 == "ALL")
                            {
                                ht_getdetail.Add("@Trans", "FROM_AND_TO_DATE_AND_SHIFT_TYPE_AND_EMP_JOB_ROLE_AND_BRANCH_WISE");
                                ht_getdetail.Add("@Fromdate", dateEdit_From_date.Text);
                                ht_getdetail.Add("@Todate", dateEdit_To_Date.Text);
                                //ht_getdetail.Add("@Branch", Val_Branch_Name);

                            }
                            else if (Shift_Current_Date_Status == "False" && dateEdit_From_date.Text != "" && dateEdit_To_Date.Text != "" && Val_Branch_Name != "" && Val_Branch_Name_1 != "ALL")
                            {
                                ht_getdetail.Add("@Trans", "FROM_AND_TO_DATE_AND_SHIFT_TYPE_AND_EMP_JOB_ROLE_AND_BRANCH_WISE");
                                ht_getdetail.Add("@Fromdate", dateEdit_From_date.Text);
                                ht_getdetail.Add("@Todate", dateEdit_To_Date.Text);
                                //ht_getdetail.Add("@Branch", Val_Branch_Name_1);
                            }

                            //old
                            else if (s_Date1 != "" && Val_Branch_Name != "" && Shift_Current_Date_Status == "True" && Val_Branch_Name_1 != "")
                            {
                                ht_getdetail.Add("@Trans", "CURRENT_DATE_AND_SHIFT_TYPE_AND_EMP_JOB_ROLE_WISE");
                                ht_getdetail.Add("@date", s_Date1);
                                // ht_getdetail.Add("@Branch", Val_Branch_Name);
                            }

                            ht_getdetail.Add("@Shift_Type_Name", Shift_Type_Name);
                            ht_getdetail.Add("@Emp_Job_Role", Emp_Job_Role);
                            ht_getdetail.Add("@Branch", Val_Branch_Name);
                            dt_getdetail = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getdetail);

                            HeaderText = $"{Tab_Name} - {Val_Branch_Name} - {Shift_Type_Name} - All {Emp_Job_Role}  Orders";
                            DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_getdetail, userroleid, User_id, Production_date, dt_getdetail.Rows[0]["Client_Id"].ToString(), int.Parse(dt_getdetail.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);
                            shift_deatils.Show();
                        }
                        else
                        {
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                }
                //7
                if (Row_Value_Type == "Value" && Column_Value_Type == "Total")  // Branch- Shift - Job Role - Row Total
                {
                    V_Data = e.GetFieldValue(pivotGridField67).ToString().Trim();

                    Shift_Type_Name = val_Shift_Type_Name.ToString();
                    Emp_Job_Role = val_Emp_Job_Role.ToString();

                    if (V_Data != "" && V_Data != "0")
                    {
                        Hashtable ht_getdetail = new Hashtable();
                        DataTable dt_getdetail = new DataTable();

                        ht_getdetail.Add("@Trans", "CURRENT_DATE_AND_SHIFT_TYPE_AND_EMP_JOB_ROLE_WISE");
                        ht_getdetail.Add("@date", dateEditShiftWiseCurrent.Text);
                        ht_getdetail.Add("@Shift_Type_Name", Shift_Type_Name);
                        ht_getdetail.Add("@Emp_Job_Role", Emp_Job_Role);

                        ht_getdetail.Add("@Branch", Val_Branch_Name);
                        dt_getdetail = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_getdetail);

                        HeaderText = $"{Tab_Name} - {Val_Branch_Name} - {Shift_Type_Name} - All {Emp_Job_Role}  Orders";
                        DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_getdetail, userroleid, User_id, Production_date, dt_getdetail.Rows[0]["Client_Id"].ToString(), int.Parse(dt_getdetail.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);
                        shift_deatils.Show();
                    }
                    else
                    {

                        SplashScreenManager.CloseForm(false);
                    }
                }
                //8
                if (Row_Value_Type == "Total" && Column_Value_Type == "Total") // Brnach- Shift Wise Total
                {

                    V_Data = e.GetFieldValue(pivotGridField67).ToString().Trim();

                    if (V_Data != "" && V_Data != "0")
                    {
                        Hashtable ht_get_Shift_Current_date_wise = new Hashtable();
                        DataTable dt_get_Shift_Current_date_wise = new DataTable();
                        ht_get_Shift_Current_date_wise.Clear();
                        dt_get_Shift_Current_date_wise.Clear();

                        ht_get_Shift_Current_date_wise.Add("@Trans", "CURRENT_DATE_AND_SHIFT_TYPE_WISE");
                        ht_get_Shift_Current_date_wise.Add("@date", dateEditShiftWiseCurrent.Text.ToString());
                        ht_get_Shift_Current_date_wise.Add("@Shift_Type_Name", val_Shift_Type_Name);
                        ht_get_Shift_Current_date_wise.Add("@Branch", Val_Branch_Name);
                        dt_get_Shift_Current_date_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Report", ht_get_Shift_Current_date_wise);

                        HeaderText = $"{Tab_Name} - {Val_Branch_Name} - {val_Shift_Type_Name} - All Orders";
                        DailyStatus_OrderViewDetail_New shift_deatils = new DailyStatus_OrderViewDetail_New(dt_get_Shift_Current_date_wise, userroleid, User_id, Production_date, dt_get_Shift_Current_date_wise.Rows[0]["Client_Id"].ToString(), int.Parse(dt_get_Shift_Current_date_wise.Rows[0]["Subprocess_Id"].ToString()), "", dateEdit_From_date.Text, dateEdit_To_Date.Text, "", Tab_Name, HeaderText);
                        shift_deatils.Show();
                    }
                    else
                    {
                        SplashScreenManager.CloseForm(false);
                    }
                }
            }
            catch (Exception ex)
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }

        private void lookUpEditMyClientName_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditMyClientName.EditValue) > 0)
            {
                Editvalue = int.Parse(lookUpEditMyClientName.EditValue.ToString());
                if (userRoleId == 1)
                {
                    BindMyClientSubProcessName(lookUpEditMyClienSubprocess, Editvalue);
                }
                else
                {
                    BindMyClientSubProcessNumber(lookUpEditMyClienSubprocess, Editvalue);
                }
            }
            else
            {
                lookUpEditMyClienSubprocess.Properties.DataSource = null;
            }
        }

        private void pivotGridControl9_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    PivotGridHitInfo hitInfo = pivotGridControlShiftWise.CalcHitInfo(e.Location);

                    if (hitInfo.ValueInfo != null)
                    {
                        // int index = hitInfo.CellInfo.RowFieldIndex;
                        //string Shift_Type = hitInfo.CellInfo.RowField.Name;

                        object value = hitInfo.ValueInfo.Value;

                        int Max_Row_Index = hitInfo.ValueInfo.MaxIndex;
                        string val_Shift_Type_Name = "";
                        string val_Emp_Job_Role = "";
                        string date = "";
                        string Baranch_Name = "";

                        string Shift_Type = pivotGridControlShiftWise.GetFieldValue(pivotGridField65, Max_Row_Index).ToString();
                        val_Shift_Type_Name = Shift_Type;

                        string value_4 = pivotGridControlShiftWise.GetFieldValue(pivotGridField66, Max_Row_Index).ToString();
                        val_Emp_Job_Role = value_4;

                        string Date = pivotGridControlShiftWise.GetFieldValue(pivotGridField64, 0).ToString();
                        date = Date;

                        string branch_n = pivotGridControlShiftWise.GetFieldValue(pivotGridField70, Max_Row_Index).ToString();
                        Baranch_Name = branch_n;

                        Hashtable ht_get_jobrole_wise = new Hashtable();
                        DataTable dt_get_jobrole_wise = new DataTable();

                        ht_get_jobrole_wise.Add("@Trans", "EMP_JOB_ROLE_WISE_DETAIL");
                        ht_get_jobrole_wise.Add("@date", date);
                        ht_get_jobrole_wise.Add("@Shift_Type_Name", val_Shift_Type_Name);
                        ht_get_jobrole_wise.Add("@Emp_Job_Role", val_Emp_Job_Role);
                        ht_get_jobrole_wise.Add("@Branch_Name", Baranch_Name);
                        dt_get_jobrole_wise = dataaccess.ExecuteSP("Sp_Daily_Status_Capcity_Utilization", ht_get_jobrole_wise);
                        Reports.User_Details_View view_1 = new Reports.User_Details_View(dt_get_jobrole_wise);
                        view_1.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
        }
        private void BindClientName(LookUpEdit lookUpEditClientName)
        {

            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Client", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[3] = "ALL";
            dt.Rows.InsertAt(dr, 0);
            lookUpEditClientName.Properties.DataSource = dt;
            lookUpEditClientName.Properties.DisplayMember = "Client_Name";
            lookUpEditClientName.Properties.ValueMember = "Client_Id";
            lookUpEditClientName.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Client_Name", 100);
            lookUpEditClientName.Properties.Columns.Add(col);
        }

        private void BindClientNumber(LookUpEdit lookUpEditClientName)
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Client", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[2] = "Client Number";
            dt.Rows.InsertAt(dr, 0);
            lookUpEditClientName.Properties.DataSource = dt;
            lookUpEditClientName.Properties.DisplayMember = "Client_Number";
            lookUpEditClientName.Properties.ValueMember = "Client_Id";
            lookUpEditClientName.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Client_Number", 100);
            lookUpEditClientName.Properties.Columns.Add(col);
        }

        private void lookUpEditMyEmpProdClientName_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditMyEmpProdClientName.EditValue) > 0)
            {
                Editvalue = int.Parse(lookUpEditMyEmpProdClientName.EditValue.ToString());
                if (userRoleId == 1)
                {
                    BindMyClientSubProcessName(lookUpEditMyEmpProdSubprocess, Editvalue);
                }
                else
                {
                    BindMyClientSubProcessNumber(lookUpEditMyEmpProdSubprocess, Editvalue);
                }
            }
            else
            {
                lookUpEditMyEmpProdSubprocess.Properties.DataSource = null;
            }
        }

        private void simple_Btn_Submit_Click(object sender, EventArgs e)
        {

            //if (Validations())
            //{
            Bind_Grid_Control();
            //}
        }
        //private bool Validations()
        //{
        //    if (date_Edit_From_Date.Text == string.Empty)
        //    {
        //        XtraMessageBox.Show("Select the From Date");
        //        date_Edit_From_Date.Focus();
        //        return false;
        //    }

        //    if (date_Edit_To_Date.Text == string.Empty)
        //    {
        //        XtraMessageBox.Show("Select the To Date");
        //        date_Edit_To_Date.Focus();
        //        return false;
        //    }
        //    if (look_UpEdit_Client_Name.EditValue.ToString() == "0")
        //    {
        //        XtraMessageBox.Show("Select Client Name");
        //        look_UpEdit_Client_Name.Focus();
        //        return false;
        //    }
        //    if (look_UpEdit_SubProcess_Name.EditValue.ToString() == "0")
        //    {
        //        XtraMessageBox.Show("Select SubProcessName Name");
        //        look_UpEdit_SubProcess_Name.Focus();
        //        return false;
        //    }

        //    return true;

        //}
        private void Bind_Grid_Control()
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                if (Convert.ToInt32(lookUpEditAllClientName.EditValue) > 0)
                {
                    Client = int.Parse(lookUpEditAllClientName.EditValue.ToString());
                }
                if (Convert.ToInt32(lookUpEditAllClientSubProcess.EditValue) > 0)
                {
                    SubProcess = int.Parse(lookUpEditAllClientSubProcess.EditValue.ToString());

                }
                else
                {
                    SubProcess = 0;
                }

                gridControlAllClientProduction.DataSource = null;
                DateTime Fromdate = Convert.ToDateTime(dateEditAllClientFromDate.Text.ToString());
                DateTime Todate = Convert.ToDateTime(dateEditAllClientToDate.Text.ToString());
                String dy = Todate.Day.ToString();
                String mn = Todate.Month.ToString();
                String yy = Todate.Year.ToString();

                if (userRoleId == 1)
                {
                    gridViewAllClientProduction.Columns[0].FieldName = "Client";
                }
                else
                {
                    gridViewAllClientProduction.Columns[0].FieldName = "Client_Number";
                }
                gridViewAllClientProduction.Columns.ColumnByFieldName("R_Current_Day").Caption = "R-" + Todate.ToShortDateString();
                gridViewAllClientProduction.Columns.ColumnByFieldName("C_Current_Day").Caption = "C-" + Todate.ToShortDateString();
                gridViewAllClientProduction.Columns.ColumnByFieldName("R_MTD").Caption = "R(MTD)" + "-" + mn + "/" + yy + "";
                gridViewAllClientProduction.Columns.ColumnByFieldName("C_MTD").Caption = "C(MTD)" + "-" + mn + "/" + yy + "";

                //   gridView2.Columns.Clear();

                gridViewAllClientProduction.BestFitColumns();



                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;

                Hashtable ht_All_Clients = new Hashtable();

                DataTable dt_All_Clients = new DataTable();
                dt_All_Clients.Clear();
                if (userRoleId != 2)
                {
                    if (Convert.ToInt32(lookUpEditAllClientName.EditValue) > 0 && Convert.ToInt32(lookUpEditAllClientSubProcess.EditValue) <= 0)
                    {
                        ht_All_Clients.Add("@Trans", "CLIENT_WISE_PRODUCTION_COUNT");
                    }
                    else if (Convert.ToInt32(lookUpEditAllClientName.EditValue) > 0 && Convert.ToInt32(lookUpEditAllClientSubProcess.EditValue) > 0)
                    {
                        ht_All_Clients.Add("@Trans", "CLEINT_SUPROCESS_COUNT");
                    }
                    else
                    {
                        ht_All_Clients.Add("@Trans", "ALL_CLIENT_WISE_PRODUCTION_COUNT");
                    }

                }
                else if (userRoleId == 2)
                {

                    if (Convert.ToInt32(lookUpEditAllClientName.EditValue) > 0 && Convert.ToInt32(lookUpEditAllClientSubProcess.EditValue) <= 0)
                    {

                        ht_All_Clients.Add("@Trans", "CLIENT_WISE_PRODUCTION_COUNT");
                    }

                    else if (Convert.ToInt32(lookUpEditAllClientName.EditValue) > 0 && Convert.ToInt32(lookUpEditAllClientSubProcess.EditValue) > 0)
                    {
                        ht_All_Clients.Add("@Trans", "CLEINT_SUPROCESS_COUNT");
                    }
                    else
                    {
                        ht_All_Clients.Add("@Trans", "ALL_CLIENT_WISE_PRODUCTION_COUNT");
                    }

                }
                ht_All_Clients.Add("@F_Date", dateEditAllClientFromDate.Text.ToString());
                ht_All_Clients.Add("@T_date", dateEditAllClientToDate.Text.ToString());
                ht_All_Clients.Add("@Clint", Client);
                ht_All_Clients.Add("@Log_In_Userid", User_id);
                ht_All_Clients.Add("@Subprocess_Id", SubProcess);
                dt_All_Clients = dataaccess.ExecuteSP("usp_Order_Status_Report_Updated", ht_All_Clients);
                dtclientexport = dt_All_Clients;

                gridControlAllClientProduction.DataSource = dt_All_Clients;




            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something Went Wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void simple_Button_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                if (dtclientexport.Rows.Count > 0)
                {
                    DataSet dsexport = new DataSet();
                    if (Convert.ToInt32(lookUpEditAllClientName.EditValue) > 0)
                    {
                        Client = int.Parse(lookUpEditAllClientName.EditValue.ToString());
                    }
                    else
                    {
                        Client = 0;
                    }
                    if (Convert.ToInt32(lookUpEditAllClientSubProcess.EditValue) > 0)
                    {
                        SubProcess = int.Parse(lookUpEditAllClientSubProcess.EditValue.ToString());
                    }
                    else
                    {
                        SubProcess = 0;
                    }


                    DateTime Fromdate = Convert.ToDateTime(dateEditAllClientFromDate.Text.ToString());
                    DateTime Todate = Convert.ToDateTime(dateEditAllClientToDate.Text.ToString());

                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                    DateTime seldate = Convert.ToDateTime(dateEditAllClientToDate.Text, usDtfi);

                    Hashtable ht_Status1 = new Hashtable();
                    DataTable dt_Status1 = new DataTable();

                    if (userRoleId == 1)
                    {
                        if (Client != 0 && SubProcess == 0)
                        {
                            ht_Status1.Add("@Filter_Type", "Client_Wise");
                            ht_Status1.Add("@Trans", "Order_Status_Report__ClientWise");
                        }
                        else if (Client != 0 && SubProcess != 0)
                        {
                            ht_Status1.Add("@Filter_Type", "Sub_Client_Wise");
                            ht_Status1.Add("@Trans", "Order_Status_Report__Client_SubprocessWise");
                        }
                        else
                        {
                            ht_Status1.Add("@Filter_Type", "All");
                            ht_Status1.Add("@Trans", "Order_Status_Report_All_ClientWise");
                        }
                    }
                    else
                    {
                        if (Client != 0 && SubProcess == 0)
                        {
                            ht_Status1.Add("@Filter_Type", "Client_Wise");
                            ht_Status1.Add("@Trans", "Order_Status_Report_ClientWise_Employee_User_Role");
                        }
                        else if (Client != 0 && SubProcess != 0)
                        {
                            ht_Status1.Add("@Filter_Type", "Sub_Client_Wise");
                            ht_Status1.Add("@Trans", "Order_Status_Report__Client_SubprocessWise_Employee_User_Role");
                        }
                        else
                        {
                            ht_Status1.Add("@Filter_Type", "All");
                            ht_Status1.Add("@Trans", "Order_Status_Report_All_ClientWise_Employee_User_Role");
                        }
                    }
                    ht_Status1.Add("@F_Date", dateEditAllClientFromDate.Text);
                    ht_Status1.Add("@T_date", dateEditAllClientToDate.Text);
                    ht_Status1.Add("@Clint", Client);
                    ht_Status1.Add("@Log_In_Userid", User_id);
                    ht_Status1.Add("@Subprocess_Id", SubProcess);
                    dt_Status1 = dataaccess.ExecuteSP("usp_Order_Status_Report_Details_Updated", ht_Status1);

                    gridControl5.DataSource = dt_Status1;
                    gridView6.BestFitColumns();

                    Export_ReportClient_ProductionData();

                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("No data avilabe to Export");
                }
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
        private void Export_ReportClient_ProductionData()
        {
            PrintingSystem ps = new PrintingSystem();
            CompositeLink compositeLink = new CompositeLink(ps);
            PrintableComponentLink link_1 = new PrintableComponentLink();
            PrintableComponentLink link_2 = new PrintableComponentLink();
            ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);
            link_1.Component = gridControlAllClientProduction;
            link_1.PaperName = "Client_Production";
            link_2.Component = gridControl5;


            ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
            compositeLink.Links.AddRange(new object[] { link_1, link_2 });

            compositeLink.PrintingSystem = ps;

            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + "Client_Production" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";

            // compositeLink.ShowPreview();
            compositeLink.CreatePageForEachLink();

            // this is for Creating excel sheet name
            ps.XlSheetCreated += PrintingSystem_XlSheetCreated_Client_reports;
            //  compositeLink.CreatePageForEachLink();

            compositeLink.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
            System.Diagnostics.Process.Start(Path1);


        }

        private void simple_Button_Clear_Click(object sender, EventArgs e)
        {
            lookUpEditAllClientName.EditValue = 0;
            lookUpEditAllClientSubProcess.EditValue = 0;
            gridControlAllClientProduction.DataSource = null;
            string D1 = DateTime.Now.ToString("M/d/yyyy");
            dateEditAllClientFromDate.Text = D1;
            dateEditAllClientToDate.Text = D1;

        }

        private void date_Edit_From_Date_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simple_Button_Refresh_Click(object sender, EventArgs e)
        {

            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);

            try
            {
                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                //DateTime fromdate = DateTime.ParseExact(txt_Employee_From_Date.Text, "MM/DD/yyyy", null);
                //DateTime fromdate = Convert.ToDateTime(txt_Employee_From_Date.Text, usDtfi);
                //  DateTime Todate = Convert.ToDateTime(txt_Employee_Todate.Text, usDtfi);
                //  DateTime Todate = DateTime.ParseExact(txt_Employee_Todate.Text, "MM/DD/yyyy", null);
                gridControlAllClientProduction.DataSource = null;
                DateTime Fromdate = Convert.ToDateTime(dateEditAllClientFromDate.Text.ToString());
                DateTime Todate = Convert.ToDateTime(dateEditAllClientToDate.Text.ToString());
                String dy = Todate.Day.ToString();
                String mn = Todate.Month.ToString();
                String yy = Todate.Year.ToString();
                if (userRoleId == 1)
                {
                    gridViewAllClientEmpProduction.Columns.ColumnByFieldName("Client_Number").Visible = false;
                    gridViewAllClientEmpProduction.Columns.ColumnByFieldName("Subprocess_Number").Visible = false;
                }
                else
                {
                    gridViewAllClientEmpProduction.Columns.ColumnByFieldName("Client_Name").Visible = false;
                    gridViewAllClientEmpProduction.Columns.ColumnByFieldName("Sub_ProcessName").Visible = false;
                }

                if (Fromdate <= Todate)
                {
                    int Clientid = 0;
                    int SubProcessid = 0;
                    string orderid = null;
                    string ProgressId = null;
                    string Status = null;
                    string Userid = null;


                    // dbc.BindOrder1(ddl_OrderNumber);

                    if (Convert.ToInt32(lookUpEditAllEmpProdClientName.EditValue) > 0)
                    {
                        Clientid = int.Parse(lookUpEditAllEmpProdClientName.EditValue.ToString());
                    }
                    if (Convert.ToInt32(lookUpEditAllEmpProdSubProcess.EditValue) > 0)
                    {
                        SubProcessid = int.Parse(lookUpEditAllEmpProdSubProcess.EditValue.ToString());
                    }

                    ht.Clear();
                    dt.Clear();
                    dtclientexport.Clear();

                    if (Clientid == 0 && SubProcessid == 0)
                    {


                        ht.Add("@Trans", "All");
                        ht.Add("@Order_Id", orderid);
                        ht.Add("@Client_Id", Clientid);
                        ht.Add("@Subprocess_Id", SubProcessid);
                        ht.Add("@Order_Progress_Id", ProgressId);
                        ht.Add("@Order_Status_Id", Status);
                        ht.Add("@From_date", dateEditAllEmpProdFromDate.Text);
                        ht.Add("@To_date", dateEditAllEmpProdToDate.Text);
                        ht.Add("@User_Id", Userid);
                        dt = dataaccess.ExecuteSP("Sp_Rpt_User_Order_TimeManagement", ht);
                        gridControlAllClientEmpProduction.DataSource = dt;
                        dtclientexport = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something Went Wrong");
            }
            finally

            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void simple_Button_Exprt_Click(object sender, EventArgs e)
        {

            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);

            gridViewAllClientEmpProduction.Columns.ColumnByFieldName("Client_Order_Number").ColumnEdit = null;
            try
            {
                if (gridControlAllClientEmpProduction.DataSource == null)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Data Not Found");
                }

                else
                {
                    //    string folderPath = "C:\\Temp\\";
                    //    string Path1 = folderPath + "User_Production" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    //    XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                    //    options.AllowHyperLinks = DevExpress.Utils.DefaultBoolean.False;
                    //    options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    //    options.TextExportMode = TextExportMode.Value;
                    //    options.IgnoreErrors = XlIgnoreErrors.NumberStoredAsText;
                    //    gridControl2.ExportToXlsx(Path1, options);
                    //    System.Diagnostics.Process.Start(Path1);



                    string filePath = "C:\\Temp\\";
                    string fileName = filePath + "User_Production" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                        gridControlAllClientEmpProduction.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    else
                    {
                        gridControlAllClientEmpProduction.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }


                }

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Data Not found");
            }


            finally
            {

                SplashScreenManager.CloseForm(false);
                gridViewAllClientEmpProduction.Columns.ColumnByFieldName("Client_Order_Number").ColumnEdit = repositoryItemHyperLinkEdit34;
            }





        }

        private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridView3_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void simple_Button_Expt_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);

            gridViewMyClientEmpProduction.Columns.ColumnByFieldName("Client_Order_Number").ColumnEdit = null;
            try
            {
                if (gridControlMyClientEmpProduction.DataSource == null)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Data Not Found");
                }
                else
                {
                    //    string folderPath = "C:\\Temp\\";
                    //    string Path1 = folderPath + "User_Production" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    //    XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                    //    options.AllowHyperLinks = DevExpress.Utils.DefaultBoolean.False;
                    //    options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    //    options.TextExportMode = TextExportMode.Value;
                    //    options.IgnoreErrors = XlIgnoreErrors.NumberStoredAsText;
                    //    gridControl4.ExportToXlsx(Path1, options);
                    //    System.Diagnostics.Process.Start(Path1);

                    string filePath = "C:\\Temp\\";
                    string fileName = filePath + "User_Production" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                        gridControlMyClientEmpProduction.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                    else
                    {
                        gridControlMyClientEmpProduction.ExportToXlsx(fileName);
                        System.Diagnostics.Process.Start(fileName);
                    }
                }

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Data Not found");
            }


            finally
            {
                SplashScreenManager.CloseForm(false);
                gridViewMyClientEmpProduction.Columns.ColumnByFieldName("Client_Order_Number").ColumnEdit = repositoryItemHyperLinkEdit35;
            }
        }

        private void simpleButton_Submit_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                if (Convert.ToInt32(lookUpEditMyClientName.EditValue) != 0 && Convert.ToInt32(lookUpEditMyClientName.EditValue) > 0)
                {
                    Client = int.Parse(lookUpEditMyClientName.EditValue.ToString());
                }
                if (Convert.ToInt32(lookUpEditMyClienSubprocess.EditValue) > 0)
                {
                    SubProcess = int.Parse(lookUpEditMyClienSubprocess.EditValue.ToString());

                }
                else
                {

                    SubProcess = 0;
                }


                DateTime Fromdate = Convert.ToDateTime(dateEditMyClientsFromDate.Text.ToString());
                DateTime Todate = Convert.ToDateTime(dateEditMyClientsToDate.Text.ToString());
                String dy = Todate.Day.ToString();
                String mn = Todate.Month.ToString();
                String yy = Todate.Year.ToString();


                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


                Hashtable ht_Status = new Hashtable();
                DataTable dt_Status = new DataTable();

                if (userRoleId == 1)
                {
                    gridViewMyClientProduction.Columns[0].FieldName = "Client";
                }
                else
                {
                    gridViewMyClientProduction.Columns[0].FieldName = "Client_Number";
                }
                gridViewMyClientProduction.Columns.ColumnByFieldName("R_Current_Day").Caption = "R-" + Todate.ToShortDateString();
                gridViewMyClientProduction.Columns.ColumnByFieldName("C_Current_Day").Caption = "C-" + Todate.ToShortDateString();
                gridViewMyClientProduction.Columns.ColumnByFieldName("R_MTD").Caption = "R(MTD)" + "-" + mn + "/" + yy + "";
                gridViewMyClientProduction.Columns.ColumnByFieldName("C_MTD").Caption = "C(MTD)" + "-" + mn + "/" + yy + "";

                //   gridView2.Columns.Clear();

                gridViewMyClientProduction.BestFitColumns();



                Hashtable ht_All_Clients = new Hashtable();

                DataTable dt_All_Clients = new DataTable();

                if (userRoleId != 2)
                {
                    if (Convert.ToInt32(lookUpEditMyClientName.EditValue) != 0 && Convert.ToInt32(lookUpEditMyClienSubprocess.EditValue) == 0)
                    {
                        ht_Status.Add("@Trans", "CLIENT_WISE_PRODUCTION_COUNT");
                    }
                    else if (Convert.ToInt32(lookUpEditMyClientName.EditValue) != 0 && Convert.ToInt32(lookUpEditMyClienSubprocess.EditValue) != 0 && lookUpEditMyClientName.Text != "" && lookUpEditMyClienSubprocess.Text != "")
                    {
                        ht_Status.Add("@Trans", "CLIENT_SUBPROCESS_WISE_PRODUCTION_COUNT");
                    }

                    else
                    {
                        ht_Status.Add("@Trans", "MY_ALL_CLIENT_WISE_PRODUCTION_COUNT");
                        ht_Status.Add("@My_Clients", MY_Client);

                    }

                }


                ht_Status.Add("@Fromdate", dateEditMyClientsFromDate.Text);
                ht_Status.Add("@Todate", dateEditMyClientsToDate.Text);
                ht_Status.Add("@Clint", Client);
                ht_Status.Add("@Log_In_Userid", User_id);
                ht_Status.Add("@user_id", User_id);
                ht_Status.Add("@Subprocess_Id", SubProcess);
                dt_Status = dataaccess.ExecuteSP("usp_Order_Status_Report_Updated", ht_Status);
                gridControlMyClientProduction.DataSource = dt_Status;
                dtclientexport = dt_Status;

            }


            catch (Exception ex)
            {

                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something Went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void gridControl6_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                DataSet dsexport = new DataSet();

                if (Convert.ToInt32(lookUpEditMyClientName.EditValue) != 0 && Convert.ToInt32(lookUpEditMyClientName.EditValue) > 0)
                {
                    Client = int.Parse(lookUpEditMyClientName.EditValue.ToString());
                }
                else
                {
                    Client = 0;
                }
                if (Convert.ToInt32(lookUpEditMyClienSubprocess.EditValue) > 0)
                {
                    SubProcess = int.Parse(lookUpEditMyClienSubprocess.EditValue.ToString());

                }
                else
                {

                    SubProcess = 0;
                }

                DateTime Fromdate = Convert.ToDateTime(dateEditMyClientsFromDate.Text.ToString());
                DateTime Todate = Convert.ToDateTime(dateEditMyClientsToDate.Text.ToString());


                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                DateTime seldate = Convert.ToDateTime(dateEditMyClientsToDate.Text, usDtfi);

                Hashtable ht_Status1 = new Hashtable();
                DataTable dt_Status1 = new DataTable();

                if (userRoleId == 1)
                {
                    if (Client != 0 && SubProcess == 0)
                    {
                        ht_Status1.Add("@Trans", "Order_Status_Report__ClientWise");
                    }
                    else if (Client != 0 && SubProcess != 0)
                    {
                        ht_Status1.Add("@Trans", "Order_Status_Report__Client_SubprocessWise");
                    }
                    else
                    {
                        ht_Status1.Add("@Filter_Type", "My_Client_Wise");
                        ht_Status1.Add("@Trans", "Order_Status_Report_All_My_ClientWise");
                        ht_Status1.Add("@My_Clients", MY_Client);
                    }
                }


                if (userRoleId != 1)
                {

                    if (Client != 0 && SubProcess == 0)
                    {
                        ht_Status1.Add("@Trans", "Order_Status_Report_ClientWise_Employee_User_Role");
                    }
                    else if (Client != 0 && SubProcess != 0)
                    {
                        ht_Status1.Add("@Trans", "Order_Status_Report__Client_SubprocessWise_Employee_User_Role");
                    }
                    else
                    {
                        ht_Status1.Add("@Trans", "Order_Status_Report_All_My_ClientWise_Employe_User_Role");
                        ht_Status1.Add("@Filter_Type", "My_Client_Wise");
                        ht_Status1.Add("@My_Clients", MY_Client);
                    }
                }


                ht_Status1.Add("@F_Date", dateEditMyClientsFromDate.Text.ToString());
                ht_Status1.Add("@T_date", dateEditMyClientsToDate.Text.ToString());
                ht_Status1.Add("@Clint", Client);
                ht_Status1.Add("@Subprocess_Id", SubProcess);
                ht_Status1.Add("@Log_In_Userid", User_id);

                dt_Status1 = dataaccess.ExecuteSP("usp_Order_Status_Report_Details_Updated", ht_Status1);
                dtclientReport = dt_Status1;
                gridControl6.DataSource = dt_Status1;
                gridView6.BestFitColumns();
                Export_MyClient_ProductionData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Something Went Wrong");
                SplashScreenManager.CloseForm(false);
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Export_MyClient_ProductionData()
        {

            PrintingSystem ps = new PrintingSystem();
            CompositeLink compositeLink = new CompositeLink(ps);
            PrintableComponentLink link_1 = new PrintableComponentLink();
            PrintableComponentLink link_2 = new PrintableComponentLink();
            ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);
            link_1.Component = gridControlMyClientProduction;
            link_1.PaperName = "Client_Production";
            link_2.Component = gridControl6;


            ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
            compositeLink.Links.AddRange(new object[] { link_1, link_2 });

            compositeLink.PrintingSystem = ps;

            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + "Client_Production" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
            // compositeLink.ShowPreview();
            compositeLink.CreatePageForEachLink();

            // this is for Creating excel sheet name
            ps.XlSheetCreated += PrintingSystem_XlSheetCreated_Client_reports;
            //  compositeLink.CreatePageForEachLink();

            compositeLink.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.NumberStoredAsText });
            System.Diagnostics.Process.Start(Path1);
        }

        private void gridView4_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridView5_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void simpleButton_Clear_Click(object sender, EventArgs e)
        {
            lookUpEditMyClientName.EditValue = 0;
            lookUpEditMyClienSubprocess.EditValue = 0;
            gridControlMyClientProduction.DataSource = null;
            string D1 = DateTime.Now.ToString("M/d/yyyy");
            dateEditMyClientsFromDate.Text = D1;
            dateEditMyClientsToDate.Text = D1;
        }

        private void simpleButton_Refresh_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                DateTime fromdate = Convert.ToDateTime(dateEditMyClientEmpProdFromDate.Text, usDtfi);
                DateTime Todate = Convert.ToDateTime(dateEditMyClientEmpProdToDate.Text, usDtfi);
                if (userRoleId == 1)
                {
                    gridViewMyClientEmpProduction.Columns.ColumnByFieldName("Client_Number").Visible = false;
                    gridViewMyClientEmpProduction.Columns.ColumnByFieldName("Subprocess_Number").Visible = false;
                }
                else
                {
                    gridViewMyClientEmpProduction.Columns.ColumnByFieldName("Client_Name").Visible = false;
                    gridViewMyClientEmpProduction.Columns.ColumnByFieldName("Sub_ProcessName").Visible = false;
                }



                if (fromdate <= Todate)
                {
                    string orderid = null;
                    int Clientid = 0;
                    int SubProcessid = 0;
                    string Status = null;
                    string ProgressId = null;

                    if (Convert.ToInt32(lookUpEditMyEmpProdClientName.EditValue) != 0)
                    {
                        Clientid = int.Parse(lookUpEditMyEmpProdClientName.EditValue.ToString());
                    }
                    if (Convert.ToInt32(lookUpEditMyEmpProdSubprocess.EditValue) != -1)
                    {
                        SubProcessid = int.Parse(lookUpEditMyEmpProdSubprocess.EditValue.ToString());
                    }


                    ht.Clear();
                    dt.Clear();
                    dtuserexport.Clear();



                    if (SubProcessid == 0 && Clientid == 0)
                    {
                        ht.Add("@Trans", "All_MyClient");
                        ht.Add("@Order_Id", orderid);
                        ht.Add("@Client_Id", Clientid);
                        ht.Add("@Subprocess_Id", SubProcessid);
                        ht.Add("@Order_Progress_Id", ProgressId);
                        ht.Add("@Order_Status_Id", Status);
                        ht.Add("@From_date", fromdate);
                        ht.Add("@To_date", Todate);
                        ht.Add("@User_Id", User_id);
                        dt = dataaccess.ExecuteSP("Sp_Rpt_User_Order_TimeManagement", ht);
                        gridControlMyClientEmpProduction.DataSource = dt;
                        dtuserexport = dt;

                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something Went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void gridViewAllClientEmpProduction_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                Order_Entry orderentry = new Order_Entry(Convert.ToInt32(gridViewAllClientEmpProduction.GetRowCellValue(e.RowHandle, "Order_ID")), User_id, userroleid, Production_date);
                orderentry.Show();
            }
        }

        private void gridViewMyClientEmpProduction_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                Order_Entry orderentry = new Order_Entry(Convert.ToInt32(gridViewMyClientEmpProduction.GetRowCellValue(e.RowHandle, "Order_ID")), User_id, userroleid, Production_date);
                orderentry.Show();
            }
        }

        private void gridViewAllClientProduction_MouseDown(object sender, MouseEventArgs e)
        {
            GridView grid = sender as GridView;
            GridHitInfo gridHit = grid.CalcHitInfo(e.Location);
            GridFooterCellInfoArgs info = gridHit.FooterCell;
            if (gridHit.HitTest == GridHitTest.RowCell && info == null)
            {
                Order_View_Details orderView;
                int clientId = Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Client_Id"));
                int subProcessId = Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Subprocess_Id"));
                if (gridHit.Column.FieldName == "R_Current_Day")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "R_Current_Day")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_DATE_WISE", "GET_RECIVED_ORDER_DATEWISE_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_DATE_WISE_SUB_CLIENT", "GET_RECIVED_ORDER_DATEWISE_SUB_CLIENT_WISE_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "R_MTD")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "R_MTD")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_MTD_WISE", "GET_RECIVED_MTD_ORDER_MISE_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_MTD_WISE_SUB_CLIENT_WISE", "GET_RECIVED_MTD_ORDER_MISE_COUNT_SUB_CLIENT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "C_Current_Day")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "C_Current_Day")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_DATE_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_DATE_SUB_CLIENT_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT_SUB_CLIENT_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "C_MTD")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "C_MTD")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_MTD_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_MTD_WISE_SUB_PROCESS_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Research")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Research")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RESEARCH_ORDER", "GET_RESEARCH_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RESEARCH_ORDER_SUB_PROCESS_WISE", "GET_RESEARCH_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Tax")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Tax")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_ORDER", "GET_TAX_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_ORDER_SUB_PROCESS_WISE", "GET_TAX_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Search")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Search")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_ORDER", "GET_SEARCH_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_ORDER_SUB_PROCESS_WISE", "GET_SEARCH_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }

                if (gridHit.Column.FieldName == "Image_Request")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Image_Request")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_IMAGE_REQ_ORDER", "GET_IMAGE_REQ_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_IMAGE_REQ_ORDER_SUB_PROCESS_WISE", "GET_IMAGE_REQ_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }

                if (gridHit.Column.FieldName == "Data_Depth_Request")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Data_Depth_Request")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_DATA_DEPTH_REQ_ORDER", "GET_DATA_DEPTH_REQ_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_DATA_DEPTH_REQ_ORDER_SUB_PROCESS_WISE", "GET_DATA_DEPTH_REQ_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }

                if (gridHit.Column.FieldName == "Tax_Cert_request")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Tax_Cert_request")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_CERT_REQ_ORDER", "GET_TAX_CERT_REQ_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_CERT_REQ_ORDER_SUB_PROCESS_WISE", "GET_TAX_CERT_REQ_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }

                if (gridHit.Column.FieldName == "Search_Qc")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Search_Qc")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_QC_ORDER", "GET_SEARCH_QC_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_QC_ORDER_SUB_CLIENT_WISE", "GET_SEARCH_QC_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Typing")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Typing")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_ORDER", "GET_TYPING_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_ORDER_SUB_PROCESS_WISE", "GET_TYPING_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Typing_QC")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Typing_QC")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_QC_ORDER", "GET_TYPING_QC_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_QC_ORDER_SUB_PROCESS_WIESE", "GET_TYPING_QC_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Final_QC")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Final_QC")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_FINAL_QC_ORDER", "GET_FINAL_QC_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_FINAL_QC_ORDER_SUB_PROCESS_WIESE", "GET_FINAL_QC_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Upload")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Upload")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_UPLOAD_ORDER", "UPLOAD_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_UPLOAD_ORDER_SUB_PROCESS_WISE", "UPLOAD_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Exception")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Exception")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_EXCEPTION_ORDER", "GET_EXCEPTION_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_EXCEPTION_ORDER_SUB_PROCESS_WIESE", "GET_EXCEPTION_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Abstractor")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Abstractor")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_ABSTRACTOR_ORDER", "GET_ABSTRATCOR_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_ABSTRACTOR_ORDER_SUB_PROCESS_WISE", "GET_ABSTRATCOR_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Vendor")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Vendor")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_VENDOR_ORDER", "GET_VENDOR_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_VENDOR_ORDER_SUB_PROCESS_WISE", "GET_VENDOR_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Clarification")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Clarification")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Clarification_ORDER ", "GET_CLARIFICATION_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Clarification_ORDER_SUB_PROCESS_WISE ", "GET_CLARIFICATION_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Hold")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Hold")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Hold_ORDER", "GET_HOLDER_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Hold_ORDER_SUB_PROCESS_WISE", "GET_HOLDER_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Cancelled")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Cancelled")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Cancelled_ORDER", "GET_CANCEELED_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Cancelled_ORDER_SUB_PROCESS_WISE", "GET_CANCEELED_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "WFT")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "WFT")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_WFT_ORDER", "WFT_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_WFT_ORDER_SUB_PROCESS_WISE", "WFT_ORDER_COUNT_SUB_RPOCESS_WIESE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Search_Tax")
                {
                    if (Convert.ToInt32(gridViewAllClientProduction.GetRowCellValue(gridHit.RowHandle, "Search_Tax")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_INTERNAL_ORDER", "TAX_INTERNAL_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_INTERNAL_ORDER_SUB_PROCESS_WISE", "TAX_INTERNAL_ORDER_COUNT_SUB_RPOCESS_WIESE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
            }

            if (info != null && info.SummaryItem != null)
            {
                if (Convert.ToInt32(info.DisplayText) == 0) { return; }
                Order_View_Details orderView;
                if (info.Column.FieldName == "R_Current_Day")
                {
                    orderView = new Order_View_Details(null, "GET_RECIVD_ORDER_DATE_WISE_ALL", "GET_RECIVED_ORDER_DATEWISE_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "R_MTD")
                {
                    orderView = new Order_View_Details(null, "GET_RECIVD_ORDER_MTD_WISE_ALL", "GET_RECIVED_MTD_ORDER_MISE_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "C_Current_Day")
                {
                    orderView = new Order_View_Details(null, "GET_COMPLETED_ORDER_DATE_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "C_MTD")
                {
                    orderView = new Order_View_Details(null, "GET_COMPLETED_ORDER_MTD_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Research")
                {
                    orderView = new Order_View_Details(null, "GET_RESEARCH_ORDER", "GET_RESEARCH_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Tax")
                {
                    orderView = new Order_View_Details(null, "GET_TAX_ORDER_ALL", "GET_TAX_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Search")
                {
                    orderView = new Order_View_Details(null, "GET_SEARCH_ORDER_ALL", "GET_SEARCH_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Image_Request")
                {
                    orderView = new Order_View_Details(null, "GET_IMAGE_REQUEST_ORDER_ALL", "GET_IMAGE_REQUEST_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Data_Depth_Request")
                {
                    orderView = new Order_View_Details(null, "GET_DATA_DEPTH_REQ_ORDER_ALL", "GET_DATA_DEPTH_REQ_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Tax_Cert_request")
                {
                    orderView = new Order_View_Details(null, "GET_TAX_CERT_REQ_ORDER_ALL", "GET_TAX_CERT_REQ_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }

                if (info.Column.FieldName == "Search_Qc")
                {
                    orderView = new Order_View_Details(null, "GET_SEARCH_QC_ORDER_ALL", "GET_SEARCH_QC_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Typing")
                {
                    orderView = new Order_View_Details(null, "GET_TYPING_ORDER_ALL", "GET_TYPING_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Typing_QC")
                {
                    orderView = new Order_View_Details(null, "GET_TYPING_QC_ORDER_ALL", "GET_TYPING_QC_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Final_QC")
                {
                    orderView = new Order_View_Details(null, "GET_FINAL_QC_ORDER_ALL", "GET_FINAL_QC_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Upload")
                {
                    orderView = new Order_View_Details(null, "GET_UPLOAD_ORDER", "UPLOAD_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Exception")
                {
                    orderView = new Order_View_Details(null, "GET_EXCEPTION_ORDER_ALL", "GET_EXCEPTION_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Abstractor")
                {
                    orderView = new Order_View_Details(null, "GET_ABSTRACTOR_ORDER", "GET_ABSTRATCOR_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Vendor")
                {
                    orderView = new Order_View_Details(null, "GET_VENDOR_ORDER", "GET_VENDOR_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Clarification")
                {
                    orderView = new Order_View_Details(null, "GET_Clarification_ORDER_ALL ", "GET_CLARIFICATION_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Hold")
                {
                    orderView = new Order_View_Details(null, "GET_Hold_ORDER", "GET_HOLDER_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Cancelled")
                {
                    orderView = new Order_View_Details(null, "GET_Cancelled_ORDER_ALL", "GET_CANCEELED_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "WFT")
                {
                    orderView = new Order_View_Details(null, "GET_WFT_ORDER", "WFT_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Search_Tax")
                {
                    orderView = new Order_View_Details(null, "GET_TAX_INTERNAL_ORDER", "TAX_INTERNAL_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
            }
        }

        private void gridViewMyClientProduction_MouseDown(object sender, MouseEventArgs e)
        {
            GridView grid = sender as GridView;
            GridHitInfo gridHit = grid.CalcHitInfo(e.Location);
            GridFooterCellInfoArgs info = gridHit.FooterCell;
            if (gridHit.HitTest == GridHitTest.RowCell && info == null)
            {
                Order_View_Details orderView;
                int clientId = Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Client_Id"));
                int subProcessId = Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Subprocess_Id"));
                if (gridHit.Column.FieldName == "R_Current_Day")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "R_Current_Day")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_DATE_WISE", "GET_RECIVED_ORDER_DATEWISE_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_DATE_WISE_SUB_CLIENT", "GET_RECIVED_ORDER_DATEWISE_SUB_CLIENT_WISE_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "R_MTD")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "R_MTD")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_MTD_WISE", "GET_RECIVED_MTD_ORDER_MISE_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RECIVD_ORDER_MTD_WISE_SUB_CLIENT_WISE", "GET_RECIVED_MTD_ORDER_MISE_COUNT_SUB_CLIENT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "C_Current_Day")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "C_Current_Day")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_DATE_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_DATE_SUB_CLIENT_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT_SUB_CLIENT_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "C_MTD")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "C_MTD")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_MTD_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_COMPLETED_ORDER_MTD_WISE_SUB_PROCESS_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Research")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Research")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RESEARCH_ORDER", "GET_RESEARCH_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_RESEARCH_ORDER_SUB_PROCESS_WISE", "GET_RESEARCH_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Tax")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Tax")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_ORDER", "GET_TAX_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_ORDER_SUB_PROCESS_WISE", "GET_TAX_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Search")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Search")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_ORDER", "GET_SEARCH_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_ORDER_SUB_PROCESS_WISE", "GET_SEARCH_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Image_Request")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Image_Request")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_IMAGE_REQ_ORDER", "GET_IMAGE_REQ_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_IMAGE_REQ_ORDER_SUB_PROCESS_WISE", "GET_IMAGE_REQ_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }

                if (gridHit.Column.FieldName == "Data_Depth_Request")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Data_Depth_Request")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_DATA_DEPTH_REQ_ORDER", "GET_DATA_DEPTH_REQ_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_DATA_DEPTH_REQ_ORDER_SUB_PROCESS_WISE", "GET_DATA_DEPTH_REQ_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }

                if (gridHit.Column.FieldName == "Tax_Cert_request")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Tax_Cert_request")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_CERT_REQ_ORDER", "GET_TAX_CERT_REQ_ORDER_COUNT", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_CERT_REQ_ORDER_SUB_PROCESS_WISE", "GET_TAX_CERT_REQ_ORDER_COUNT_SUB_PROCESS_WISE", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Search_Qc")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Search_Qc")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_QC_ORDER", "GET_SEARCH_QC_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_SEARCH_QC_ORDER_SUB_CLIENT_WISE", "GET_SEARCH_QC_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Typing")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Typing")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_ORDER", "GET_TYPING_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_ORDER_SUB_PROCESS_WISE", "GET_TYPING_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Typing_QC")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Typing_QC")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_QC_ORDER", "GET_TYPING_QC_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TYPING_QC_ORDER_SUB_PROCESS_WIESE", "GET_TYPING_QC_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Final_QC")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Final_QC")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_FINAL_QC_ORDER", "GET_FINAL_QC_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_FINAL_QC_ORDER_SUB_PROCESS_WIESE", "GET_FINAL_QC_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Upload")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Upload")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_UPLOAD_ORDER", "UPLOAD_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_UPLOAD_ORDER_SUB_PROCESS_WISE", "UPLOAD_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Exception")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Exception")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_EXCEPTION_ORDER", "GET_EXCEPTION_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_EXCEPTION_ORDER_SUB_PROCESS_WIESE", "GET_EXCEPTION_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Abstractor")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Abstractor")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_ABSTRACTOR_ORDER", "GET_ABSTRATCOR_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_ABSTRACTOR_ORDER_SUB_PROCESS_WISE", "GET_ABSTRATCOR_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Vendor")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Vendor")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_VENDOR_ORDER", "GET_VENDOR_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_VENDOR_ORDER_SUB_PROCESS_WISE", "GET_VENDOR_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Clarification")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Clarification")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Clarification_ORDER ", "GET_CLARIFICATION_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Clarification_ORDER_SUB_PROCESS_WISE ", "GET_CLARIFICATION_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Hold")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Hold")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Hold_ORDER", "GET_HOLDER_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Hold_ORDER_SUB_PROCESS_WISE", "GET_HOLDER_ORDER_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Cancelled")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Cancelled")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Cancelled_ORDER", "GET_CANCEELED_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_Cancelled_ORDER_SUB_PROCESS_WISE", "GET_CANCEELED_COUNT_SUB_PROCESS_WISE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "WFT")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "WFT")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_WFT_ORDER", "WFT_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_WFT_ORDER_SUB_PROCESS_WISE", "WFT_ORDER_COUNT_SUB_RPOCESS_WIESE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
                if (gridHit.Column.FieldName == "Search_Tax")
                {
                    if (Convert.ToInt32(gridViewMyClientProduction.GetRowCellValue(gridHit.RowHandle, "Search_Tax")) == 0) { return; }
                    if (subProcessId == 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_INTERNAL_ORDER", "TAX_INTERNAL_ORDER_COUNT", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                        orderView.Show();
                        return;
                    }
                    if (subProcessId > 0)
                    {
                        orderView = new Order_View_Details(clientId.ToString(), "GET_TAX_INTERNAL_ORDER_SUB_PROCESS_WISE", "TAX_INTERNAL_ORDER_COUNT_SUB_RPOCESS_WIESE", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, subProcessId, userroleid, "");
                        orderView.Show();
                        return;
                    }
                }
            }

            if (info != null && info.SummaryItem != null)
            {
                if (Convert.ToInt32(info.DisplayText) == 0) { return; }
                Order_View_Details orderView;
                if (info.Column.FieldName == "R_Current_Day")
                {
                    orderView = new Order_View_Details(null, "GET_RECIVD_ORDER_DATE_WISE_ALL", "GET_RECIVED_ORDER_DATEWISE_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "R_MTD")
                {
                    orderView = new Order_View_Details(null, "GET_RECIVD_ORDER_MTD_WISE_ALL", "GET_RECIVED_MTD_ORDER_MISE_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "C_Current_Day")
                {
                    orderView = new Order_View_Details(null, "GET_COMPLETED_ORDER_DATE_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "C_MTD")
                {
                    orderView = new Order_View_Details(null, "GET_COMPLETED_ORDER_MTD_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Research")
                {
                    orderView = new Order_View_Details(null, "GET_RESEARCH_ORDER", "GET_RESEARCH_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Tax")
                {
                    orderView = new Order_View_Details(null, "GET_TAX_ORDER_ALL", "GET_TAX_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Search")
                {
                    orderView = new Order_View_Details(null, "GET_SEARCH_ORDER_ALL", "GET_SEARCH_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Image_Request")
                {
                    orderView = new Order_View_Details(null, "GET_IMAGE_REQUEST_ORDER_ALL", "GET_IMAGE_REQUEST_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Data_Depth_Request")
                {
                    orderView = new Order_View_Details(null, "GET_DATA_DEPTH_REQ_ORDER_ALL", "GET_DATA_DEPTH_REQ_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Tax_Cert_request")
                {
                    orderView = new Order_View_Details(null, "GET_TAX_CERT_REQ_ORDER_ALL", "GET_TAX_CERT_REQ_ORDER_COUNT_ALL", dateEditAllClientFromDate.Text, dateEditAllClientToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Search_Qc")
                {
                    orderView = new Order_View_Details(null, "GET_SEARCH_QC_ORDER_ALL", "GET_SEARCH_QC_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Typing")
                {
                    orderView = new Order_View_Details(null, "GET_TYPING_ORDER_ALL", "GET_TYPING_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Typing_QC")
                {
                    orderView = new Order_View_Details(null, "GET_TYPING_QC_ORDER_ALL", "GET_TYPING_QC_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Final_QC")
                {
                    orderView = new Order_View_Details(null, "GET_FINAL_QC_ORDER_ALL", "GET_FINAL_QC_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Upload")
                {
                    orderView = new Order_View_Details(null, "GET_UPLOAD_ORDER", "UPLOAD_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Exception")
                {
                    orderView = new Order_View_Details(null, "GET_EXCEPTION_ORDER_ALL", "GET_EXCEPTION_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Abstractor")
                {
                    orderView = new Order_View_Details(null, "GET_ABSTRACTOR_ORDER", "GET_ABSTRATCOR_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Vendor")
                {
                    orderView = new Order_View_Details(null, "GET_VENDOR_ORDER", "GET_VENDOR_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Clarification")
                {
                    orderView = new Order_View_Details(null, "GET_Clarification_ORDER_ALL ", "GET_CLARIFICATION_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Hold")
                {
                    orderView = new Order_View_Details(null, "GET_Hold_ORDER", "GET_HOLDER_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Cancelled")
                {
                    orderView = new Order_View_Details(null, "GET_Cancelled_ORDER_ALL", "GET_CANCEELED_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "WFT")
                {
                    orderView = new Order_View_Details(null, "GET_WFT_ORDER", "WFT_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
                if (info.Column.FieldName == "Search_Tax")
                {
                    orderView = new Order_View_Details(null, "GET_TAX_INTERNAL_ORDER", "TAX_INTERNAL_ORDER_COUNT_ALL", dateEditMyClientsFromDate.Text, dateEditMyClientsToDate.Text, User_id, 0, userroleid, "");
                    orderView.Show();
                    return;
                }
            }
        }
        private void BindSubProcessName(LookUpEdit SubProcessName, int editvalue)
        {

            Hashtable htSubProcessName = new Hashtable();
            DataTable dtSubProcessName = new DataTable();

            htSubProcessName.Add("@Trans", "SELECTCLIENTWISE");
            htSubProcessName.Add("@Client_Id", editvalue);
            dtSubProcessName = dataaccess.ExecuteSP("Sp_Client_SubProcess", htSubProcessName);
            DataRow dr = dtSubProcessName.NewRow();
            dr[0] = 0;
            dr[6] = "ALL";
            dtSubProcessName.Rows.InsertAt(dr, 0);

            SubProcessName.Properties.DataSource = dtSubProcessName;
            SubProcessName.Properties.DisplayMember = "Sub_ProcessName";
            SubProcessName.Properties.ValueMember = "Subprocess_Id";
            SubProcessName.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sub_ProcessName", 100);
            SubProcessName.Properties.Columns.Add(col);


        }
        private void BindSubProcessNumber(LookUpEdit SubProcessNumber, int editvalue)
        {

            Hashtable htSubProcessNumbe = new Hashtable();
            DataTable dtSubProcessNumbe = new DataTable();
            htSubProcessNumbe.Add("@Trans", "SELECTSUBPROCESSNUMBERCLIENTWISE");
            htSubProcessNumbe.Add("@Client_Id", editvalue);
            dtSubProcessNumbe = dataaccess.ExecuteSP("Sp_Client_SubProcess", htSubProcessNumbe);
            DataRow dr = dtSubProcessNumbe.NewRow();
            dr[0] = 0;
            dr[3] = "ALL";
            // dr[3] = "0";
            dtSubProcessNumbe.Rows.InsertAt(dr, 0);
            SubProcessNumber.Properties.DataSource = dtSubProcessNumbe;
            SubProcessNumber.Properties.DisplayMember = "Subprocess_Number";
            SubProcessNumber.Properties.ValueMember = "Subprocess_Id";
            SubProcessNumber.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Subprocess_Number", 100);
            SubProcessNumber.Properties.Columns.Add(col);

        }
        private void BindMyClientName(LookUpEdit MyClientName, int User_id)
        {

            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();

            htParam.Add("@Trans", "BIND_USERCLIENT");
            htParam.Add("@Userid", User_id);
            dt = dataaccess.ExecuteSP("Sp_UserClient_Reports", htParam);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[2] = "ALL";
                dr[3] = 0;

                dt.Rows.InsertAt(dr, 0);


            }


            MyClientName.Properties.DataSource = dt;
            MyClientName.Properties.DisplayMember = "Client_Name";
            MyClientName.Properties.ValueMember = "Client_Id";

            MyClientName.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Client_Name", 100);
            MyClientName.Properties.Columns.Add(col);

        }
        private void BindMyClientnumber(LookUpEdit MyClientNumber, int User_Id)
        {

            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();

            htParam.Add("@Trans", "BIND_USERCLIENT");
            htParam.Add("@Userid", User_Id);
            dt = dataaccess.ExecuteSP("Sp_UserClient_Reports", htParam);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[3] = 0;
                dr[1] = "ALL";
                dt.Rows.InsertAt(dr, 0);
            }
            MyClientNumber.Properties.DataSource = dt;
            MyClientNumber.Properties.DisplayMember = "Client_Number";
            MyClientNumber.Properties.ValueMember = "Client_Id";
            MyClientNumber.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Client_Number", 100);
            MyClientNumber.Properties.Columns.Add(col);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BindMyClientSubProcessName(LookUpEdit MyClientSubProcessName, int editvalue)
        {

            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "SELECTCLIENTWISE");
            htParam.Add("@Client_Id", editvalue);
            dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[6] = "Sub_ProcessName";
            dt.Rows.InsertAt(dr, 0);
            MyClientSubProcessName.Properties.DataSource = dt;
            MyClientSubProcessName.Properties.DisplayMember = "Sub_ProcessName";
            MyClientSubProcessName.Properties.ValueMember = "Subprocess_Id";
            MyClientSubProcessName.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sub_ProcessName", 100);
            MyClientSubProcessName.Properties.Columns.Add(col);

        }
        private void BindMyClientSubProcessNumber(LookUpEdit MyClientSubProcessNumber, int editvalue)
        {

            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();

            htParam.Add("@Trans", "SELECTSUBPROCESSNUMBERCLIENTWISE");
            htParam.Add("@Client_Id", editvalue);
            dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[3] = "ALL";
            // dr[3] = "0";
            dt.Rows.InsertAt(dr, 0);
            MyClientSubProcessNumber.Properties.DataSource = dt;
            MyClientSubProcessNumber.Properties.DisplayMember = "Subprocess_Number";
            MyClientSubProcessNumber.Properties.ValueMember = "Subprocess_Id";
            MyClientSubProcessNumber.Properties.Columns.Clear();
            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Subprocess_Number", 100);
            MyClientSubProcessNumber.Properties.Columns.Add(col);


        }
    }
}
