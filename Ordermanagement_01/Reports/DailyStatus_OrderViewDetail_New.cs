using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.CommentCard;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using Ordermanagement_01.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class DailyStatus_OrderViewDetail_New : XtraForm
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt_Order_Status = new DataTable();
        //System.Data.DataRow row ;
        string User_Role_Id;
        string Header;
        //  string Path1;
        DataTable dt;
        //int User_Id;
        string Production_Date; string date;
        DataTable dt_Section;
        Hashtable httargetorder = new Hashtable();
        DataTable dttargetorder = new DataTable();
        DataTable dt_Order_Details = new DataTable();
        int Order_Id_value;
        int Work_Type_Id = 1;
        int Differnce_Time, Order_ID, User_id, External_Client_Order_Id, External_Client_Order_Task_Id, Check_External_Production, Max_Time_Id;
        int Userid_value = 0, OrderStatusId = 0, Order_StatusId_Value = 0, Selected_Row_Count, Order_StatusId = 0, User_id_value = 0;
        string User_Name_value, Order_Status_Value;
        int Record_Count = 0, error_Count_1 = 0, error_Count_2 = 0, error_Count_3 = 0, error_Count_4 = 0, error_Count_5 = 0, error_Count_6 = 0, Check_Cont = 0; int Order_Allocate_Count = 0; int Error_Count = 0;
        int Sub_Process_ID, Order_Status_Id, Client_Id, Order_Type_Abs_Id, ClientId, Sub_Process_Id, Order_Task_Id, Order_Satatus_Id;
        int Emp_Job_role_Id, Emp_Sal_Cat_Id, Eff_Client_Id, Eff_Order_Type_Abs_Id, Eff_Order_Task_Id, Eff_Order_Source_Type_Id, Eff_State_Id, Eff_County_Id, Eff_Sub_Process_Id;
        string Clint, userroleid, Operation, Operation_Count, From_date, To_Date, Path1, errormessage = "", error_status = "", error_value = "", vendor_validation_msg = "", Order_Number;
        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string caption = e.Column.Caption;
            if (caption == "Comments")
            {
                DataRowView view = gridView2.GetRow(e.RowHandle) as DataRowView;
                if (view.Row["Order_Comments"] != null)
                {
                    string Comments = view.Row["Order_Comments"].ToString();
                    if (Comments != "")
                    {
                        e.Appearance.BackColor = Color.Orange;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.FromArgb(217, 217, 217);
                    }
                }
            }
            if (Tab_Type_Name == "Open Order Wise" || Tab_Type_Name == "Pending Order Wise")
            {
                if (caption == "Tax Status")
                {
                    DataRowView view = gridView2.GetRow(e.RowHandle) as DataRowView;
                    if (view.Row["Tax_Status"] != null)
                    {
                        string Task = view.Row["Tax_Status"].ToString();
                        if (Task == "COMPLETED")
                        {
                            e.Appearance.BackColor = Color.Green;
                            e.Appearance.ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        private void gridView2_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            //if (e.Column.FieldName == "Order_Production_Date" && e.CellValue.ToString() == "01/01/1900")
            //{


            //    gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "Order_Production_Date", DBNull.Value);
            //}
        }

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            BindOrdersByOperation();
        }

        private void gridView2_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = gridView2.CalcHitInfo(e.Location);
            if (info.InRowCell == true)
            {
                string caption = info.Column.Caption;
                {
                    if (caption == "Comments")
                    {
                        DataRowView vie = gridView2.GetRow(info.RowHandle) as DataRowView;
                        StringBuilder bs = new StringBuilder();
                        if (vie.Row["Order_Comments"] != null || vie.Row["Status_Comments"] != null || vie.Row["Permission_Comments"] != null || vie.Row["Tax_Task"] != null || vie.Row["Tax_Task_Id"] != null || vie.Row["Assigned_Date"] != null)
                        {
                            string Comments = vie.Row["Order_Comments"].ToString();
                            string Order_Number = vie.Row["Client_Order_Number"].ToString();
                            bs.Append(Order_Number.ToString());
                            if (Comments != "")
                            {
                                bs.AppendLine();
                                bs.AppendLine();
                                (bs.Append(Comments.ToString().TrimStart('@')).AppendLine()).ToString();
                            }
                            //else
                            //{
                            //    string message = "No Comments";
                            //    toolTipController1.ShowHint(message);
                            //}
                            if (Tab_Type_Name == "Open Order Wise" || Tab_Type_Name == "Pending Order Wise")
                            {
                                string StatusComments = vie.Row["Status_Comments"].ToString();
                                string PermissionComments = vie.Row["Permission_Comments"].ToString();
                                string Taxtask = vie.Row["Tax_Task"].ToString();
                                string Taxstatus = vie.Row["Tax_Status"].ToString();
                                string AssignedDate = vie.Row["Assigned_Date"].ToString();
                                string Task1 = vie.Row["Tax_Status"].ToString();
                                if (StatusComments != "")
                                {
                                    bs.AppendLine();
                                    bs.Append(StatusComments.ToString());
                                    bs.AppendLine();
                                }
                                if (PermissionComments != "")
                                {
                                    bs.AppendLine();
                                    bs.Append(PermissionComments.ToString());
                                    bs.AppendLine();
                                }
                                if (Task1 != "COMPLETED" && Task1 != "")
                                {
                                    bs.AppendLine();
                                    bs.Append("Tax---");
                                    bs.Append(Taxtask.ToString());
                                    bs.Append("--".ToString());
                                    bs.Append(Taxstatus.ToString());
                                    bs.Append("--".ToString());
                                    bs.Append("Assigned On--");
                                    bs.Append(AssignedDate.ToString());
                                }
                                string _str = bs.Replace("@", Environment.NewLine + Environment.NewLine).ToString();
                                toolTipController1.ShowHint(_str);
                            }

                        }
                    }
                }
            }
        }
        private void gridView2_MouseUp(object sender, MouseEventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                string caption = info.Column.Caption;
                if (caption == "Comments")
                {
                    Order_Passing_Params obj_Order_Details_List = new Order_Passing_Params();
                    DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                    int Order_ID = int.Parse(row["Order_Id"].ToString());
                    obj_Order_Details_List.Order_Id = Order_ID;
                    obj_Order_Details_List.Work_Type_Id = Work_Type_Id;
                    Comment_Card cmd = new Comment_Card(obj_Order_Details_List);
                    cmd.Show();
                }
            }
        }
        private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        int Order_Status;
        string Client_Number, Tab_Type_Name = "";
        decimal Emp_Sal, Emp_cat_Value, Emp_Eff_Allocated_Order_Count, Eff_Order_User_Effecncy;
        public DailyStatus_OrderViewDetail_New(DataTable dtt, string USER_ROLE_ID, int USER_ID, string PRODUCTION_DATE, string Client_Num, int Order_Status_Id, string OPERATION, string FROM_DATE, string TO_DATE, string Date, string Tab_Name, string headerText)
        {
            InitializeComponent();
            Tab_Type_Name = Tab_Name;
            date = Date;
            From_date = FROM_DATE;
            To_Date = TO_DATE;
            Client_Number = Client_Num;
            Operation = OPERATION;
            Order_Status = Order_Status_Id;
            gridView2.IndicatorWidth = 50;
            if (!string.IsNullOrEmpty(headerText))
            {
                Header = headerText;
            }
            else
            {
                Header = "Order Details";
            }
            groupControl1.Text = Header;
            dt = dtt;
            User_Role_Id = USER_ROLE_ID;
            User_id = USER_ID;
            Production_Date = PRODUCTION_DATE;

            pivotGridControl1.DataSource = dt;



            repositoryItemPopupContainerEdit1.QueryPopUp += repositoryItemPopupContainerEdit1_QueryPopUp;
            repositoryItemPopupContainerEdit1.CloseUp += repositoryItemPopupContainerEdit1_CloseUp;
            repositoryItemPopupContainerEdit1.PopupControl = CreatePopupControl();


            if (Tab_Type_Name == "Shift Wise")
            {
                panel7.Visible = true;
                panel2.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    grd_Targetorder.DataSource = dt;
                }

                if (User_Role_Id == "1")
                {

                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;

                }
                else
                {
                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;

                }
            }
            if (Tab_Type_Name == "Daily Wise" || Tab_Type_Name == "Open Order Wise" || Tab_Type_Name == "Pending Order Wise")
            {
                panel7.Visible = true;
                panel2.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    grd_Targetorder.DataSource = dt;
                }
                if (User_Role_Id == "1")
                {

                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;

                }
                else
                {
                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;

                }
                //Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
            }
            else
            {


                if (Tab_Type_Name == "Shift Wise")
                {
                    panel7.Visible = true;
                    panel2.Visible = true;
                }
                else
                {
                    panel7.Visible = false;
                    panel2.Visible = false;
                }
                if (dt.Rows.Count > 0)
                {

                    grd_Targetorder.DataSource = dt;



                    if (User_Role_Id == "1")
                    {

                        gridColumn14.Visible = false;
                        gridColumn15.Visible = false;

                        gridColumn37.Visible = true;
                        gridColumn38.Visible = true;

                        gridView2.Columns["Client_Number"].Visible = true;
                        gridView2.Columns["Subprocess_Number"].Visible = true;

                    }
                    else
                    {
                        gridColumn14.Visible = false;
                        gridColumn15.Visible = false;

                        gridColumn37.Visible = true;
                        gridColumn38.Visible = true;

                        gridView2.Columns["Client_Number"].Visible = true;
                        gridView2.Columns["Subprocess_Number"].Visible = true;

                    }
                }
            }
        }
        DataTable CreatePivotTable()
        {
            Random rnd = new Random();
            // DataTable dt = new DataTable();



            return dt;
        }
        private void DailyStatus_OrderViewDetail_New_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            try
            {
                SetupLookup();
                Bind_Order_Status_For_Reallocate();
                // Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
                this.Text = Header.ToString();
                this.WindowState = FormWindowState.Maximized;
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
        protected void BindOrdersByOperation()
        {
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            try
            {
                grd_Targetorder.DataSource = null;
                pivotGridControl1.DataSource = null;
                string Fromdate = From_date.ToString();
                string Todate = To_Date.ToString();

                string spName = "";
                httargetorder.Clear();
                dttargetorder.Clear();

                httargetorder.Add("@Trans", Operation);
                httargetorder.Add("@Client_Number", Client_Number);    //Clint
                                                                       //httargetorder.Add("@Sub_Client", Sub_Process_ID);  // Sub_Process_ID 
                httargetorder.Add("@Order_Status", Order_Status);
                httargetorder.Add("@Fromdate", Fromdate);
                httargetorder.Add("@Todate", Todate);
                httargetorder.Add("@date", date);
                // httargetorder.Add("@date", s_Date);
                if (Operation == "AGENT_OPEN_ORDER_DETAILS" || Operation == "AGENT_OPEN_ORDER_ROW_TOTAL_CLIENT_DATE_WISE" || Operation == "AGENT_OPEN_ORDER_COLUMN_GRANT_TOTAL_WISE" ||
                     Operation == "AGENT_OPEN_ORDER_COLUMN_GRANT_TOTAL_WISE" || Operation == "AGENT_OPEN_ORDER_ALL_CLIENT_AND_ORDER_STATUS_WISE" || Operation == "AGENT_OPEN_ORDER_CLIENT_AND_DATE_WISE" ||
                     Operation == "AGENT_OPEN_ORDER_CLIENT_AND_ALL_TASK_WISE" || Operation == "AGENT_OPEN_ORDER_DATE_WISE" || Operation == "AGENT_OPEN_ORDER_CLIENT_AND_ORDER_STATUS_WISE")
                {
                    spName = "Sp_Daily_Status_Report_Open";
                }
                else if (Operation == "AGENT_PENDING_ORDER_DETAILS" || Operation == "AGENT_OPEN_ORDER_ROW_TOTAL_CLIENT_DATE_WISE" ||
                    Operation == "AGENT_PENDING_ORDER_CLIENT_AND_STATUS" || Operation == "AGENT_PENDING_ORDER_ALL_CLIENT_STATUS_WISE" ||
                    Operation == "AGENT_PENDING_ORDER_CLIENT_DATE_WISE" || Operation == "AGENT_PENDING_ORDER_CLIENT_AND_ALL_STATUS_WISE"
                    || Operation == "AGING_PENDING_ORDER_DATE_WISE")
                {
                    spName = "Sp_Daily_Status_Report_Pending";
                }
                else
                {
                    spName = "Sp_Daily_Status_Report";
                }
                dttargetorder = dataaccess.ExecuteSP(spName, httargetorder);
                dt_Order_Details = dttargetorder;
                if (dttargetorder.Rows.Count > 0)
                {
                    grd_Targetorder.DataSource = dttargetorder;
                    pivotGridControl1.DataSource = dttargetorder;
                    if (User_Role_Id == "1")
                    {
                        gridColumn14.Visible = true;
                        gridColumn15.Visible = true;

                        gridColumn37.Visible = false;
                        gridColumn38.Visible = false;

                        gridView2.Columns["Client_Name"].Visible = true;
                        gridView2.Columns["Sub_ProcessName"].Visible = true;

                    }
                    else
                    {
                        gridColumn14.Visible = false;
                        gridColumn15.Visible = false;

                        gridColumn37.Visible = true;
                        gridColumn38.Visible = true;

                        gridView2.Columns["Client_Number"].Visible = true;
                        gridView2.Columns["Subprocess_Number"].Visible = true;
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
        public void SetupLookup()
        {
            //DevExpress.XtraEditors.Controls.LookUpColumnInfo col1;
            //col1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);

            Hashtable htParam = new Hashtable();
            DataTable dtParam = new DataTable();
            htParam.Clear();
            dtParam.Clear();
            lookUpEdit1.Properties.DataSource = null;

            htParam.Add("@Trans", "SELECT");
            dtParam = dataaccess.ExecuteSP("Sp_User", htParam);

            //repsItemCmbx_1 = new RepositoryItemComboBox();
            //l = new List<string>(dt.Rows.Count);
            //foreach (DataRow row in dt.Rows)
            //    repsItemCmbx_1.Items.Add((string)row["User_Name"]);


            DataRow dr = dtParam.NewRow();
            dr[0] = 0;
            dr[4] = "SELECT";
            dtParam.Rows.InsertAt(dr, 0);
            lookUpEdit1.Properties.DataSource = dtParam;
            lookUpEdit1.Properties.DisplayMember = "User_Name";
            lookUpEdit1.Properties.ValueMember = "User_id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            //col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            lookUpEdit1.Properties.Columns.Add(col);

        }
        public void Bind_Order_Status_For_Reallocate()
        {
            Hashtable ht_OrderStatus = new Hashtable();
            DataTable dt_OrderStatus = new DataTable();
            ht_OrderStatus.Add("@Trans", "BIND_FOR_ORDER_STATUS_VIEW");
            dt_OrderStatus = dataaccess.ExecuteSP("Sp_Order_Status", ht_OrderStatus);

            //repsItemCmbx_2 = new RepositoryItemComboBox();
            //l = new List<string>(dt_Order_Status.Rows.Count);
            //foreach (DataRow row in dt_Order_Status.Rows)
            //    repsItemCmbx_2.Items.Add((string)row["Order_Status"]);


            DataRow dr = dt_OrderStatus.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_OrderStatus.Rows.InsertAt(dr, 0);
            lookUpEdit2.Properties.DataSource = dt_OrderStatus;
            lookUpEdit2.Properties.DisplayMember = "Order_Status";
            lookUpEdit2.Properties.ValueMember = "Order_Status_ID";

            LookUpColumnInfo col_1;

            col_1 = new LookUpColumnInfo("Order_Status", 100);
            //   col_1.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            lookUpEdit2.Properties.Columns.Add(col_1);

        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            //  load_Progressbar.Start_progres();
            if (dt.Rows.Count > 0)
            {
                Export_ReportData();
            }
            else
            {
                XtraMessageBox.Show("No Records were found to export", "Message", MessageBoxButtons.OK);
            }
        }
        void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "OrderView-Details";
            }
            else if (e.Index == 1)
            {
                e.SheetName = "Custom-OrderView-Details";
            }
        }
        private void Export_ReportData()
        {
            pivotGridControl1.OptionsView.ShowFilterHeaders = false;
            pivotGridControl1.OptionsView.ShowDataHeaders = false;
            pivotGridControl1.OptionsView.ShowColumnHeaders = false;

            gridView2.OptionsView.ShowFooter = false;
            gridView2.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.None;
            gridView2.VisibleColumns[0].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_1 = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
                DevExpress.XtraPrinting.PrintableComponentLink link_1 = new DevExpress.XtraPrinting.PrintableComponentLink();
                DevExpress.XtraPrinting.PrintableComponentLink link_2 = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_1.PrintingSystem = ps;

                link_1.Component = grd_Targetorder;
                link_2.Component = pivotGridControl1;

                compositeLink_1.Links.AddRange(new object[] { link_1, link_2 });

                string ReportName = "Order-View-Details";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_1.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps.XlSheetCreated += PrintingSystem_XlSheetCreated;

                compositeLink_1.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Value, IgnoreErrors = XlIgnoreErrors.None });
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
                pivotGridControl1.OptionsView.ShowFilterHeaders = true;
                pivotGridControl1.OptionsView.ShowDataHeaders = true;
                pivotGridControl1.OptionsView.ShowColumnHeaders = true;
                gridView2.OptionsView.ShowFooter = true;
                gridView2.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            }












            //-----------------------------
            //try
            //{
            //    //  string Export_Title_Name = Group_Header.Text;
            //    //Exporting to Excel
            //    string folderPath = "C:\\Temp\\";
            //    Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Order_Details" + ".xlsx";
            //    if (!Directory.Exists(folderPath))
            //    {
            //        Directory.CreateDirectory(folderPath);
            //    }
            //    // Grd_Purchase_Items.OptionsPrint.AutoWidth = false;
            //    gridView9.ExportToXlsx(Path1);

            //    System.Diagnostics.Process.Start(Path1);
            //}
            //catch (Exception ex)
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show("Problem While Exporting Please Check with Administrator", "Message", MessageBoxButtons.OK);

            //}

        }
        //private void grd_Targetorder_Click(object sender, EventArgs e)
        //{
        //    var columnIndex = gridView9.FocusedColumn.VisibleIndex;

        //    if (columnIndex == 0)
        //    {
        //        System.Data.DataRow row = gridView9.GetDataRow(gridView9.FocusedRowHandle);
        //      int  Order_ID = int.Parse(row[0].ToString());

        //        Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID,User_Id, User_Role_Id, Production_Date);
        //        orderentry.Show();
        //    }


        //}
        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var columnIndex = gridView2.FocusedColumn.VisibleIndex;

            if (columnIndex == 1)
            {
                System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                int Order_ID = int.Parse(row["Order_Id"].ToString());

                Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID, User_id, User_Role_Id, Production_Date);
                orderentry.Show();
            }

            else if (columnIndex == 21)
            {

                if (error_Count_1 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    // styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn31;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "This Order is Processing by Tax Team", "Warning", MessageBoxButtons.OK);
                }
                if (error_Count_2 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    //styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn31;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "This Order is Assigned To Vendor and It will Not Reallocate", "Warning", MessageBoxButtons.OK);
                }
                if (error_Count_3 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    //styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn31;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "This Order is in Work in Progress you can't Reallocate", "Warning", MessageBoxButtons.OK);

                }
                if (error_Count_4 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    // styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn31;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order", "Warning", MessageBoxButtons.OK);


                }
                if (error_Count_5 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    // styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn31;


                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order Is in Work in Progress in Tax Team", "Warning", MessageBoxButtons.OK);


                }
                if (error_Count_6 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    //  styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn31;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Abstractor Order Cannot be Reallocate", "Warning", MessageBoxButtons.OK);
                }

                label4.Text = gridView2.SelectedRowsCount.ToString();
            }

        }
        private void gridView2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            //Initialization.
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            { }
            //Calculation.
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            { }
            //Finalization.
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                e.TotalValue = string.Format("{0} of {1}", gridView2.SelectedRowsCount, gridView2.RowCount);
            label4.Text = e.TotalValue.ToString();
        }
        struct GroupRowHash
        {
            public object GroupValue;
            public int GroupRowHandle;
        };
        Dictionary<int, Dictionary<GroupRowHash, int>> commonCache = new Dictionary<int, Dictionary<GroupRowHash, int>>();
        public Rectangle TheRequiredArea { get; private set; }
        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.GroupRowHandle != GridControl.InvalidRowHandle)
            {
                GridView view = sender as GridView;
                e.DisplayText += string.Format("[{0}/{1}]", GetcheckedChildRowsCount(e, view, GetCheckedChildRowsCache(e)), GetFullChildRowsCount(view, e.GroupRowHandle));


            }
        }
        private Dictionary<GroupRowHash, int> GetCheckedChildRowsCache(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            Dictionary<GroupRowHash, int> checkedChildRowsCache;
            if (commonCache.ContainsKey(e.Column.GroupIndex))
                checkedChildRowsCache = commonCache[e.Column.GroupIndex];
            else
            {
                checkedChildRowsCache = new Dictionary<GroupRowHash, int>();
                commonCache.Add(e.Column.GroupIndex, checkedChildRowsCache);
            }
            return checkedChildRowsCache;
        }
        private int GetcheckedChildRowsCount(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e, GridView view, Dictionary<GroupRowHash, int> checkedChildRowsCache)
        {
            int checkedChildRowsCount;
            GroupRowHash key = new GroupRowHash { GroupRowHandle = e.GroupRowHandle, GroupValue = e.Value };
            if (!checkedChildRowsCache.ContainsKey(key))
            {
                checkedChildRowsCount = CalcCheckedRowsCount(view, e.GroupRowHandle, view.GetChildRowCount(e.GroupRowHandle));
                if (checkedChildRowsCount != 0)
                    checkedChildRowsCache[key] = checkedChildRowsCount;
            }
            else
                checkedChildRowsCount = checkedChildRowsCache[key];
            return checkedChildRowsCount;
        }
        private int GetFullChildRowsCount(GridView view, int groupRowHandle)
        {
            int childRowCount = view.GetChildRowCount(groupRowHandle);
            int childGroupRowCount = 0;
            int nextChildHandle;
            for (int i = 0; i < childRowCount; i++)
            {
                nextChildHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (!view.IsGroupRow(nextChildHandle))
                    return childRowCount;
                else
                    childGroupRowCount += GetFullChildRowsCount(view, nextChildHandle);
            }
            return childGroupRowCount;
        }
        private int CalcCheckedRowsCount(GridView view, int groupRowHandle, int childRowsCount)
        {
            int nextChildHandle;
            int checkedChildCount = 0;
            int[] selectedRows = view.GetSelectedRows();
            for (int i = 0; i < childRowsCount; i++)
            {
                nextChildHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (view.IsGroupRow(nextChildHandle))
                    checkedChildCount += CalcCheckedRowsCount(view, nextChildHandle, view.GetChildRowCount(nextChildHandle));
                else if (selectedRows.Contains(nextChildHandle))
                    checkedChildCount++;
            }
            return checkedChildCount;
        }
        private void ClearCache()
        {
            commonCache.Clear();
        }
        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption == "SI.NO")
            {
                string value = e.RowHandle.ToString();

                if (value != "")
                {
                    int value1 = int.Parse(value.ToString()) + 1;

                    e.DisplayText = value1.ToString();

                }
            }

            if (e.Column.FieldName == "Order_Production_Date")
            {
                string v = e.CellValue.ToString();
                if (v == "01/01/1900")
                {
                    e.DisplayText = null;
                }
            }
        }
        private void gridView2_EndGrouping(object sender, EventArgs e)
        {
            ClearCache();
        }
        private void pivotGridControl1_CellClick(object sender, PivotCellEventArgs e)
        {
            //SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                // Create a new form. 
                //   Form form = new Form();
                //  form.Text = "Records";
                // Place a DataGrid control on the form. 
                //DataGrid grid = new DataGrid();
                //grid.Parent = form;
                //grid.Dock = DockStyle.Fill;
                //// Get the recrd set associated with the current cell and bind it to the grid. 
                //grid.DataSource = e.CreateDrillDownDataSource();


                //SimpleButton btnExport_New = new SimpleButton();
                //btnExport_New.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                //btnExport_New.Appearance.BackColor = System.Drawing.Color.White;
                //btnExport_New.Appearance.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //btnExport_New.Appearance.Options.UseBackColor = true;
                //btnExport_New.Appearance.Options.UseFont = true;
                //btnExport_New.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
                //btnExport_New.AppearanceHovered.Options.UseBackColor = true;
                //btnExport_New.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                //btnExport_New.Location = new System.Drawing.Point(944, 3);
                //btnExport_New.Name = "btnExport_New";
                //btnExport_New.Size = new System.Drawing.Size(150, 32);
                //btnExport_New.TabIndex = 559;
                //btnExport_New.Text = "Export";
                //btnExport_New.Click += new System.EventHandler(this.btnExport_New_Click);


                //btnExport_New.Parent = form;

                //  gridControl1.Visible = true;
                //gridControl1.DataSource = dt;
                //    gridControl1.RefreshDataSource();
                // gridControl1.DataSource = dt;
                //  gridControl1.Parent = form;

                // gridControl1.Dock = DockStyle.Fill;
                gridControl1.DataSource = e.CreateDrillDownDataSource();
                dt_Section = GetDataTable(gridView5);
                //  gridControl1.DataSource = dt_Section;
                Record record = new Record(dt_Section, Order_ID, User_id, User_Role_Id, Production_Date, Client_Number, Operation, From_date, To_Date, Order_Status, date);
                record.Show();
                //form.Bounds = new Rectangle(100, 100, 600, 450);
                // form.WindowState = FormWindowState.Maximized;

                //// Display the form. 
                //  form.ShowDialog(this);
                // form.Dispose();
                //  form.Refresh();
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            //finally
            //{
            //    //Close Wait Form
            //    //SplashScreenManager.CloseForm(false);
            //}

        }
        DataTable GetDataTable(GridView view)
        {
            DataTable dt_1 = new DataTable();
            foreach (GridColumn c in view.Columns)
                dt_1.Columns.Add(c.FieldName, c.ColumnType);
            for (int r = 0; r < view.RowCount; r++)
            {
                object[] rowValues = new object[dt_1.Columns.Count];
                for (int c = 0; c < dt_1.Columns.Count; c++)
                    rowValues[c] = view.GetRowCellValue(r, dt_1.Columns[c].ColumnName);
                dt_1.Rows.Add(rowValues);
            }
            return dt_1;
        }
        private PopupContainerControl CreatePopupControl()
        {
            PopupContainerControl result = new PopupContainerControl();
            DrillDownControl ddc = new DrillDownControl();
            ddc.Name = "DrillDownControl";
            ddc.Dock = DockStyle.Fill;
            // ddc.DataSource = dt;
            result.Size = ddc.Size;
            result.Controls.Add(ddc);
            return result;
        }
        private void repositoryItemPopupContainerEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {
            PopupContainerEdit editor = (PopupContainerEdit)sender;
            DrillDownControl ddc =
                (DrillDownControl)editor.Properties.PopupControl.Controls["DrillDownControl"];
            ddc.DataSource = pivotGridControl1.Cells.GetFocusedCellInfo().CreateDrillDownDataSource();

        }
        private void repositoryItemPopupContainerEdit1_CloseUp(object sender, CloseUpEventArgs e)
        {
            PopupContainerEdit editor = (PopupContainerEdit)sender;
            DrillDownControl ddc =
                (DrillDownControl)editor.Properties.PopupControl.Controls["DrillDownControl"];
            if (ddc.DataSource != null)
            {
                ((IDisposable)ddc.DataSource).Dispose();
                ddc.DataSource = null;
            }
        }
        private void gridView5_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var columnIndex = gridView5.FocusedColumn.VisibleIndex;

            if (columnIndex == 1)
            {

                //int Order_ID ;
                // gridControl1.DataSource = dt;
                System.Data.DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
                int Order_ID = int.Parse(row["Order_ID"].ToString());

                Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID, User_id, User_Role_Id, Production_Date);
                orderentry.Show();
            }

        }
        private void btnExport_New_Click(object sender, EventArgs e)
        {

            //  load_Progressbar.Start_progres();
            if (dt_Section.Rows.Count > 0)
            {
                Export_ReportData();

            }
            else
            {

                DevExpress.XtraEditors.XtraMessageBox.Show("No Records were found to export", "Message", MessageBoxButtons.OK);
            }


        }
        private void gridView5_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption == "SI.NO")
            {
                string value = e.RowHandle.ToString();

                if (value != "")
                {
                    int value1 = int.Parse(value.ToString()) + 1;

                    e.DisplayText = value1.ToString();

                }
            }
        }
        // -------------------10-apr-2019
        private void Get_Effecncy_Category()
        {
            if (Emp_Job_role_Id != 0 && Emp_Sal != 0)
            {

                Hashtable htget_Category = new Hashtable();
                DataTable dtget_Category = new DataTable();
                if (Emp_Job_role_Id == 1)
                {
                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_SEARCHER");
                }
                else if (Emp_Job_role_Id == 2)
                {

                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_TYPER");
                }
                htget_Category.Add("@Salary", Emp_Sal);
                htget_Category.Add("@Job_Role_Id", Emp_Job_role_Id);

                dtget_Category = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Category);


                if (dtget_Category.Rows.Count > 0)
                {
                    Emp_Sal_Cat_Id = int.Parse(dtget_Category.Rows[0]["Category_ID"].ToString());
                    Emp_cat_Value = decimal.Parse(dtget_Category.Rows[0]["Category_Name"].ToString());
                }
                else
                {
                    Emp_Sal_Cat_Id = 0;
                    Emp_cat_Value = 0;
                }

            }
            else
            {
                MessageBox.Show("Please Setup Employee job Role");
            }

        }
        private void Get_Employee_Details()
        {

            object obj = lookUpEdit1.EditValue;
            string Username = lookUpEdit1.Text;
            if (obj.ToString() != "0")
            {
                Userid_value = (int)obj;
            }
            else
            {
                Userid_value = 0;
            }
            Hashtable htget_empdet = new Hashtable();
            DataTable dtget_empdet = new DataTable();

            htget_empdet.Add("@Trans", "GET_EMP_DETAILS");
            htget_empdet.Add("@User_Id", Userid_value);
            dtget_empdet = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_empdet);
            if (dtget_empdet.Rows.Count > 0)
            {
                if (dtget_empdet.Rows[0]["Job_Role_Id"].ToString() != "" && dtget_empdet.Rows[0]["Job_Role_Id"].ToString() != null)
                {
                    Emp_Job_role_Id = int.Parse(dtget_empdet.Rows[0]["Job_Role_Id"].ToString());
                    Emp_Sal = decimal.Parse(dtget_empdet.Rows[0]["Salary"].ToString());
                }
                else
                {
                    Emp_Job_role_Id = 0;
                    Emp_Sal = 0;
                }

            }
            else
            {

                Emp_Job_role_Id = 0;
                Emp_Sal = 0;
            }

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
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
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
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

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
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
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
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
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
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
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
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

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
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

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
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
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
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

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
                htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                if (dtget_Effeciency_Value.Rows.Count > 0)
                {
                    Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                }
                else
                {
                    Emp_Eff_Allocated_Order_Count = 0;
                    Eff_Order_User_Effecncy = 0;
                }
            }
        }
        private bool Validate()
        {
            object obj = lookUpEdit1.EditValue;
            string Username = lookUpEdit1.Text;
            if (obj.ToString() != "0")
            {
                User_id_value = (int)obj;
            }
            else
            {
                User_id_value = 0;
            }

            // order status
            object obj_OrderStatusId = lookUpEdit2.EditValue;
            string Order_Status = lookUpEdit2.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                Order_StatusId_Value = (int)obj_OrderStatusId;
            }
            else
            {
                Order_StatusId_Value = 0;
            }

            if (User_id_value == null || User_id_value == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select User Name.", "Warning", MessageBoxButtons.OK);
                return false;
            }

            //
            else if (Order_StatusId_Value == null || Order_StatusId_Value == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Task.", "Warning", MessageBoxButtons.OK);
                return false;
            }


            Selected_Row_Count = gridView2.SelectedRowsCount;
            if (Selected_Row_Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Records to Reallocate.", "Warning", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        private bool Validate1()
        {
            // order status
            object obj_OrderStatusId = lookUpEdit2.EditValue;
            string Order_Status = lookUpEdit2.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                Order_StatusId_Value = (int)obj_OrderStatusId;
            }
            else
            {
                Order_StatusId_Value = 0;
            }
            if (Order_StatusId_Value == null || Order_StatusId_Value == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Task.", "Warning", MessageBoxButtons.OK);
                return false;
            }

            Selected_Row_Count = gridView2.SelectedRowsCount;
            if (Selected_Row_Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Records to Reallocate.", "Warning", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }
        private void btn_Reallocate_Click(object sender, EventArgs e)
        {
            object obj = lookUpEdit1.EditValue;
            string Username = lookUpEdit1.Text;
            if (obj.ToString() != "0")
            {
                User_id_value = (int)obj;
            }

            int Selected_Row_Count = gridView2.SelectedRowsCount;

            // order status
            object obj_OrderStatusId = lookUpEdit2.EditValue;
            string Order_Status = lookUpEdit2.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                Order_StatusId_Value = (int)obj_OrderStatusId;
            }
            Get_Employee_Details();
            Get_Effecncy_Category();

            if (Validate() != false)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    try
                    {
                        try
                        {
                            List<int> gridViewSelectedRows = gridView2.GetSelectedRows().ToList();

                            for (int i = 0; i < gridViewSelectedRows.Count; i++)
                            {
                                //int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
                                DataRow row = gridView2.GetDataRow(gridViewSelectedRows[i]);

                                //System.Data.DataRow row1 = gridView2.GetDataRow(gridView2.FocusedRowHandle);

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

                                Order_Id_value = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                                ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                                Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                                Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                                Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                                int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                                int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                                Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                                DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                                htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                                htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id_value);
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
                                    htcheck_Order_In_tax.Add("@Order_Id", Order_Id_value);
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
                                        ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id_value);
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

                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn31;

                                    error_status = "True";
                                    // errormessage = "This Order is Processing by Tax Team";
                                    gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
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
                                                htupdateTaxStatus.Add("@Modified_By", User_id);
                                                htupdateTaxStatus.Add("@Order_Id", Order_Id_value);
                                                dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                                if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                                {

                                                    Hashtable htupassin = new Hashtable();
                                                    DataTable dtupassign = new DataTable();
                                                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                    htupassin.Add("@Order_Id", Order_Id_value);

                                                    dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                                    Hashtable htinsert_Assign = new Hashtable();
                                                    DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                                    htinsert_Assign.Add("@Trans", "INSERT");
                                                    htinsert_Assign.Add("@Order_Id", Order_Id_value);
                                                    htinsert_Assign.Add("@User_Id", User_id_value);
                                                    htinsert_Assign.Add("@Order_Status_Id", Order_StatusId_Value);
                                                    htinsert_Assign.Add("@Order_Progress_Id", 6);
                                                    htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                                    htinsert_Assign.Add("@Assigned_By", User_id);
                                                    htinsert_Assign.Add("@Modified_By", User_id);
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
                                                    htorderStatus.Add("@Order_ID", Order_Id_value);
                                                    htorderStatus.Add("@Order_Status", Order_StatusId_Value);
                                                    htorderStatus.Add("@Modified_By", User_id);
                                                    htorderStatus.Add("@Modified_Date", date);
                                                    dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                    Hashtable htupdate_Prog = new Hashtable();
                                                    DataTable dtupdate_Prog = new System.Data.DataTable();
                                                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                    htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                                    htupdate_Prog.Add("@Order_Progress", 6);
                                                    htupdate_Prog.Add("@Modified_By", User_id);
                                                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                    //OrderHistory
                                                    Hashtable ht_Order_History = new Hashtable();
                                                    DataTable dt_Order_History = new DataTable();
                                                    ht_Order_History.Add("@Trans", "INSERT");
                                                    ht_Order_History.Add("@Order_Id", Order_Id_value);
                                                    ht_Order_History.Add("@User_Id", User_id_value);
                                                    ht_Order_History.Add("@Status_Id", Order_StatusId_Value);
                                                    ht_Order_History.Add("@Progress_Id", 6);
                                                    ht_Order_History.Add("@Work_Type", 1);
                                                    ht_Order_History.Add("@Assigned_By", User_id);
                                                    ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                    //OrderHistory
                                                    Hashtable ht_Order_History1 = new Hashtable();
                                                    DataTable dt_Order_History1 = new DataTable();
                                                    ht_Order_History1.Add("@Trans", "INSERT");
                                                    ht_Order_History1.Add("@Order_Id", Order_Id_value);
                                                    ht_Order_History1.Add("@User_Id", User_id_value);
                                                    ht_Order_History1.Add("@Status_Id", Order_StatusId_Value);
                                                    ht_Order_History1.Add("@Progress_Id", 6);
                                                    ht_Order_History1.Add("@Work_Type", 1);
                                                    ht_Order_History1.Add("@Assigned_By", User_id);
                                                    ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                                    dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                                    //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                                    Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                    DataTable dt_Order_InTitleLogy = new DataTable();
                                                    htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                    htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id_value);
                                                    dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                    if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                    {

                                                        External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                        External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                                        // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                                        if (External_Client_Order_Task_Id != 18)
                                                        {
                                                            if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                            {
                                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                                DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                            }
                                                            else
                                                            {
                                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                                DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
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
                                                    htorderStatus.Add("@Order_ID", Order_Id_value);
                                                    htorderStatus.Add("@Order_Status", Order_Satatus_Id);
                                                    htorderStatus.Add("@Modified_By", User_id);
                                                    htorderStatus.Add("@Modified_Date", date);
                                                    dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                    htupdate_Prog.Clear();
                                                    dtupdate_Prog.Clear();
                                                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                    htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                                    htupdate_Prog.Add("@Order_Progress", 6);
                                                    htupdate_Prog.Add("@Modified_By", User_id);
                                                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                                    styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                                    styleFormatForOffer.Column = this.gridColumn31;

                                                    error_status = "False";
                                                    gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                                    Record_Count = 1;
                                                    Error_Count = 0;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                                {
                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn31;

                                    //errormessage = "This Order is Assigned To Vendor and It will Not Reallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
                                    error_status = "True";
                                    gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                    error_Count_2 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }

                                else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                                {
                                    if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 60)
                                    {
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Red;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn31;

                                        error_status = "True";
                                        gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);

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
                                            htupassin.Add("@Order_Id", Order_Id_value);
                                            dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                            Hashtable htinsert_Assign = new Hashtable();
                                            DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                            htinsert_Assign.Add("@Trans", "INSERT");
                                            htinsert_Assign.Add("@Order_Id", Order_Id_value);
                                            htinsert_Assign.Add("@User_Id", User_id_value);
                                            htinsert_Assign.Add("@Order_Status_Id", Order_StatusId_Value);
                                            htinsert_Assign.Add("@Order_Progress_Id", 6);
                                            htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                            htinsert_Assign.Add("@Assigned_By", User_id);
                                            htinsert_Assign.Add("@Modified_By", User_id);
                                            htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                            htinsert_Assign.Add("@status", "True");
                                            htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                            dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);


                                            Hashtable htinsertrec = new Hashtable();
                                            DataTable dtinsertrec = new System.Data.DataTable();
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            Hashtable htorderStatus = new Hashtable();
                                            DataTable dtorderStatus = new DataTable();
                                            htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                            htorderStatus.Add("@Order_ID", Order_Id_value);
                                            htorderStatus.Add("@Order_Status", Order_StatusId_Value);
                                            htorderStatus.Add("@Modified_By", User_id);
                                            htorderStatus.Add("@Modified_Date", date);
                                            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                            Hashtable htupdate_Prog = new Hashtable();
                                            DataTable dtupdate_Prog = new System.Data.DataTable();
                                            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                            htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                            htupdate_Prog.Add("@Order_Progress", 6);
                                            htupdate_Prog.Add("@Modified_By", User_id);
                                            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            DataTable dt_Order_History = new DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", Order_Id_value);
                                            ht_Order_History.Add("@User_Id", User_id_value);
                                            ht_Order_History.Add("@Status_Id", Order_StatusId_Value);
                                            ht_Order_History.Add("@Progress_Id", 6);
                                            ht_Order_History.Add("@Work_Type", 1);
                                            ht_Order_History.Add("@Assigned_By", User_id);
                                            ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                            //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                            DataTable dt_Order_InTitleLogy = new DataTable();
                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id_value);
                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                            {

                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());
                                                // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                                if (External_Client_Order_Task_Id != 18)
                                                {
                                                    if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                    }
                                                    else
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
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
                                            htorderStatus.Add("@Order_ID", Order_Id_value);
                                            htorderStatus.Add("@Order_Status", Order_StatusId_Value);
                                            htorderStatus.Add("@Modified_By", User_id);
                                            htorderStatus.Add("@Modified_Date", date);
                                            dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                            htupdate_Prog.Clear();
                                            dtupdate_Prog.Clear();
                                            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                            htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                            htupdate_Prog.Add("@Order_Progress", 6);
                                            htupdate_Prog.Add("@Modified_By", User_id);
                                            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                            dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                            // txt_Order_number_TextChanged();
                                            DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                            styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                            styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                            styleFormatForOffer.Column = this.gridColumn31;

                                            error_status = "False";
                                            gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                            Record_Count = 1;
                                            Error_Count = 0;

                                        }
                                        else
                                        {
                                            //errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
                                            DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                            styleFormatForOffer.Appearance.BackColor = Color.Red;
                                            styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                            styleFormatForOffer.Column = this.gridColumn31;

                                            error_status = "True";
                                            // errormessage = "This Order is Processing by Tax Team";
                                            gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                            error_Count_4 = 1;
                                            Error_Count = 1;
                                            Record_Count = 0;
                                        }
                                    }
                                    else
                                    {
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Red;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn31;

                                        error_status = "True";
                                        // errormessage = "This Order is Processing by Tax Team";
                                        gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                        error_Count_5 = 1;
                                        Error_Count = 1;
                                        Record_Count = 0;
                                    }
                                }
                                else
                                {
                                    error_status = "True";
                                    //errormessage = "Abstractor Order Cannot be Reallocate";
                                    gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                    error_Count_6 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }   // for loop close

                        }
                        catch (Exception ex)
                        {
                            //Close Wait Form
                            SplashScreenManager.CloseForm(false);

                            MessageBox.Show("Error Occured Please Check With Administrator");
                        }
                        //finally
                        //{
                        //    //gridView2.EndUpdate();
                        //}

                        //
                        if (Record_Count > 0)
                        {
                            DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                            styleFormatForOffer.Appearance.BackColor = Color.Blue;
                            styleFormatForOffer.Appearance.Options.UseBackColor = true;
                            styleFormatForOffer.Column = this.gridColumn31;
                            SplashScreenManager.CloseForm(false);

                            DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order ReAllocated Sucessfully.", "Success", MessageBoxButtons.OK);
                            BindOrdersByOperation();
                            btn_Clear_Click(sender, e);
                        }
                        if (Error_Count > 0)
                        {
                            SplashScreenManager.CloseForm(false);

                            DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Few Orders are not ReAllocated Please check in Error Status Column.", "Warning", MessageBoxButtons.OK);
                            DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                            styleFormatForOffer.Appearance.BackColor = Color.Red;

                            // styleFormatForOffer.Column.AppearanceCell.ForeColor = Color.Red;
                            styleFormatForOffer.Appearance.Options.UseBackColor = true;
                            styleFormatForOffer.Column = this.gridColumn31;
                            // Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
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


            }


        }
        //private void btn_Reallocate_Submit_Click(object sender, EventArgs e)
        //{
        //    // int a=0;
        //    if (Validate_1() != false)
        //    {

        //        if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
        //        {

        //            try
        //            {

        //                for (int i = 0; i < gridView2.SelectedRowsCount; i++)
        //                {
        //                    int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
        //                    DataRow row = gridView2.GetDataRow(a);

        //                    System.Data.DataRow row1 = gridView2.GetDataRow(gridView2.FocusedRowHandle);

        //                    User_Name_value = row1["User_Name"].ToString();
        //                    Order_Status_Value = row1["Current_Task"].ToString();

        //                    Hashtable ht_get_UserID = new Hashtable();
        //                    DataTable dt_get_UserID = new System.Data.DataTable();
        //                    ht_get_UserID.Add("@Trans", "GET_USER_ID_BY_USER_NAME");
        //                    ht_get_UserID.Add("@User_Name", User_Name_value);
        //                    dt_get_UserID = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_get_UserID);
        //                    if (dt_get_UserID.Rows.Count > 0)
        //                    {
        //                        User_id_value = int.Parse(dt_get_UserID.Rows[0]["User_id"].ToString());
        //                    }


        //                    Hashtable ht_get_Order_StatusId = new Hashtable();
        //                    DataTable dt_get_Order_StatusId = new System.Data.DataTable();
        //                    ht_get_Order_StatusId.Add("@Trans", "GET_ORDERSTATUSID_BY_ORDERSTATUS");
        //                    ht_get_Order_StatusId.Add("@Order_Status", Order_Status_Value);
        //                    dt_get_Order_StatusId = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_get_Order_StatusId);
        //                    if (dt_get_Order_StatusId.Rows.Count > 0)
        //                    {
        //                        Order_StatusId_Value = int.Parse(dt_get_Order_StatusId.Rows[0]["Order_Status_ID"].ToString());
        //                    }



        //                    Order_Number = row["Order_Number"].ToString();
        //                    Eff_Client_Id = int.Parse(row["Client_Id"].ToString());
        //                    Eff_Sub_Process_Id = int.Parse(row["SubProcess_Id"].ToString());
        //                    Eff_State_Id = int.Parse(row["State_ID"].ToString());
        //                    Eff_County_Id = int.Parse(row["County_ID"].ToString());
        //                    Eff_Order_Type_Abs_Id = int.Parse(row["OrderType_ABS_Id"].ToString());


        //                    Hashtable htselect_Orderid = new Hashtable();
        //                    DataTable dtselect_Orderid = new System.Data.DataTable();
        //                    htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
        //                    htselect_Orderid.Add("@Client_Order_Number", Order_Number);
        //                    dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);

        //                    Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
        //                    ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
        //                    Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
        //                    Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
        //                    Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

        //                    int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
        //                    int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

        //                    Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
        //                    DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

        //                    htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
        //                    htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id);
        //                    dtget_User_Order_Last_Time_Updated = dataaccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

        //                    if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
        //                    {
        //                        Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());
        //                    }
        //                    else
        //                    {
        //                        Max_Time_Id = 0;
        //                    }

        //                    if (Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Abs_Staus_Id != 17)
        //                    {
        //                        Get_Employee_Details();
        //                        Get_Effecncy_Category();
        //                        if (Max_Time_Id != 0)
        //                        {
        //                            Hashtable htget_User_Order_Differnce_Time = new Hashtable();
        //                            DataTable dtget_User_Order_Differnce_Time = new DataTable();
        //                            htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
        //                            htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
        //                            dtget_User_Order_Differnce_Time = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

        //                            if (dtget_User_Order_Differnce_Time.Rows.Count > 0)
        //                            {
        //                                Differnce_Time = int.Parse(dtget_User_Order_Differnce_Time.Rows[0]["Diff"].ToString());
        //                            }
        //                            else
        //                            {
        //                                Differnce_Time = 0;
        //                            }
        //                            //htget_User_Order_Differnce_Time.Add("","");
        //                        }

        //                        Get_Order_Source_Type_For_Effeciency();
        //                        //========= Effecincy Cal End=========================================
        //                    }
        //                    //========= Effecincy Cal End=========================================

        //                    // This is for Tax Order Status check
        //                    int Check_Order_In_Tax = 0;
        //                    int Tax_User_Order_Diff_Time = 0;
        //                    if (Abs_Staus_Id == 26)
        //                    {

        //                        Hashtable htcheck_Order_In_tax = new Hashtable();
        //                        DataTable dt_check_Order_In_tax = new DataTable();

        //                        htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
        //                        htcheck_Order_In_tax.Add("@Order_Id", Order_Id);
        //                        dt_check_Order_In_tax = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

        //                        if (dt_check_Order_In_tax.Rows.Count > 0)
        //                        {

        //                            Check_Order_In_Tax = int.Parse(dt_check_Order_In_tax.Rows[0]["count"].ToString());
        //                        }
        //                        else
        //                        {
        //                            Check_Order_In_Tax = 0;
        //                        }
        //                        if (Check_Order_In_Tax > 0)
        //                        {
        //                            Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
        //                            DataTable dt_Get_Tax_Diff_Time = new DataTable();

        //                            ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
        //                            ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id);
        //                            dt_Get_Tax_Diff_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

        //                            if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
        //                            {
        //                                Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());
        //                            }
        //                            else
        //                            {
        //                                Tax_User_Order_Diff_Time = 0;
        //                            }
        //                        }
        //                    }
        //                    if (Tax_User_Order_Diff_Time < 30 && Tax_User_Order_Diff_Time != 0)
        //                    {
        //                        //This Is For Highligghting the Error based on the input

        //                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                        styleFormatForOffer.Appearance.BackColor = Color.Red;
        //                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                        styleFormatForOffer.Column = this.gridColumn34;

        //                        error_status = "True";
        //                        // errormessage = "This Order is Processing by Tax Team";
        //                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
        //                        error_Count_1 = 1;
        //                        Error_Count = 1;
        //                        Record_Count = 0;
        //                    }
        //                    else
        //                    {
        //                        // Updating Tax Order Status
        //                        // Updating Tax Order Status
        //                        if (Abs_Staus_Id == 26)
        //                        {
        //                            if (Check_Order_In_Tax > 0)
        //                            {
        //                                if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
        //                                {
        //                                    // Cancelling the Order in Tax
        //                                    Hashtable htupdateOrderTaxStatus = new Hashtable();
        //                                    System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
        //                                    Hashtable htupdateTaxStatus = new Hashtable();
        //                                    System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();

        //                                    htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
        //                                    htupdateTaxStatus.Add("@Tax_Status", 4);
        //                                    htupdateTaxStatus.Add("@Modified_By", User_id);
        //                                    htupdateTaxStatus.Add("@Order_Id", Order_Id);
        //                                    dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

        //                                    if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
        //                                    {

        //                                        Hashtable htupassin = new Hashtable();
        //                                        DataTable dtupassign = new DataTable();
        //                                        htupassin.Add("@Trans", "DELET_BY_ORDER");
        //                                        htupassin.Add("@Order_Id", Order_Id);

        //                                        dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
        //                                        Hashtable htinsert_Assign = new Hashtable();
        //                                        DataTable dtinsertrec_Assign = new System.Data.DataTable();
        //                                        htinsert_Assign.Add("@Trans", "INSERT");
        //                                        htinsert_Assign.Add("@Order_Id", Order_Id);
        //                                        htinsert_Assign.Add("@User_Id", User_id_value);
        //                                        htinsert_Assign.Add("@Order_Status_Id", Order_StatusId_Value);
        //                                        htinsert_Assign.Add("@Order_Progress_Id", 6);
        //                                        htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
        //                                        htinsert_Assign.Add("@Assigned_By", User_id);
        //                                        htinsert_Assign.Add("@Modified_By", User_id);
        //                                        htinsert_Assign.Add("@Modified_Date", DateTime.Now);
        //                                        htinsert_Assign.Add("@status", "True");
        //                                        htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
        //                                        dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);

        //                                        //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

        //                                        Hashtable htinsertrec = new Hashtable();
        //                                        DataTable dtinsertrec = new System.Data.DataTable();
        //                                        DateTime date = new DateTime();
        //                                        date = DateTime.Now;
        //                                        string dateeval = date.ToString("dd/MM/yyyy");
        //                                        string time = date.ToString("hh:mm tt");

        //                                        Hashtable htorderStatus = new Hashtable();
        //                                        DataTable dtorderStatus = new DataTable();
        //                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
        //                                        htorderStatus.Add("@Order_ID", Order_Id);
        //                                        htorderStatus.Add("@Order_Status", Order_StatusId_Value);
        //                                        htorderStatus.Add("@Modified_By", User_id);
        //                                        htorderStatus.Add("@Modified_Date", date);
        //                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

        //                                        Hashtable htupdate_Prog = new Hashtable();
        //                                        DataTable dtupdate_Prog = new System.Data.DataTable();
        //                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
        //                                        htupdate_Prog.Add("@Order_ID", Order_Id);
        //                                        htupdate_Prog.Add("@Order_Progress", 6);
        //                                        htupdate_Prog.Add("@Modified_By", User_id);
        //                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
        //                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


        //                                        //OrderHistory
        //                                        Hashtable ht_Order_History = new Hashtable();
        //                                        DataTable dt_Order_History = new DataTable();
        //                                        ht_Order_History.Add("@Trans", "INSERT");
        //                                        ht_Order_History.Add("@Order_Id", Order_Id);
        //                                        ht_Order_History.Add("@User_Id", User_id_value);
        //                                        ht_Order_History.Add("@Status_Id", Order_StatusId_Value);
        //                                        ht_Order_History.Add("@Progress_Id", 6);
        //                                        ht_Order_History.Add("@Work_Type", 1);
        //                                        ht_Order_History.Add("@Assigned_By", User_id);
        //                                        ht_Order_History.Add("@Modification_Type", "Order Reallocate");
        //                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


        //                                        //OrderHistory
        //                                        Hashtable ht_Order_History1 = new Hashtable();
        //                                        DataTable dt_Order_History1 = new DataTable();
        //                                        ht_Order_History1.Add("@Trans", "INSERT");
        //                                        ht_Order_History1.Add("@Order_Id", Order_Id);
        //                                        ht_Order_History1.Add("@User_Id", User_id_value);
        //                                        ht_Order_History1.Add("@Status_Id", Order_StatusId_Value);
        //                                        ht_Order_History1.Add("@Progress_Id", 6);
        //                                        ht_Order_History1.Add("@Work_Type", 1);
        //                                        ht_Order_History1.Add("@Assigned_By", User_id);
        //                                        ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
        //                                        dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

        //                                        //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

        //                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
        //                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
        //                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
        //                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
        //                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

        //                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
        //                                        {

        //                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
        //                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

        //                                            // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
        //                                            if (External_Client_Order_Task_Id != 18)
        //                                            {
        //                                                if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
        //                                                {
        //                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
        //                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

        //                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
        //                                                }
        //                                                else
        //                                                {
        //                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
        //                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
        //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

        //                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
        //                                                }
        //                                            }
        //                                        }

        //                                        //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

        //                                        htinsertrec.Clear();
        //                                        dtinsertrec.Clear();

        //                                        htorderStatus.Clear();
        //                                        dtorderStatus.Clear();
        //                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
        //                                        htorderStatus.Add("@Order_ID", Order_Id);
        //                                        htorderStatus.Add("@Order_Status", Order_Satatus_Id);
        //                                        htorderStatus.Add("@Modified_By", User_id);
        //                                        htorderStatus.Add("@Modified_Date", date);
        //                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

        //                                        htupdate_Prog.Clear();
        //                                        dtupdate_Prog.Clear();
        //                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
        //                                        htupdate_Prog.Add("@Order_ID", Order_Id);
        //                                        htupdate_Prog.Add("@Order_Progress", 6);
        //                                        htupdate_Prog.Add("@Modified_By", User_id);
        //                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
        //                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

        //                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
        //                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                                        styleFormatForOffer.Column = this.gridColumn34;

        //                                        error_status = "False";
        //                                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
        //                                        Record_Count = 1;
        //                                        Error_Count = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }

        //                    if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
        //                    {
        //                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                        styleFormatForOffer.Appearance.BackColor = Color.Red;
        //                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                        styleFormatForOffer.Column = this.gridColumn34;

        //                        //errormessage = "This Order is Assigned To Vendor and It will Not Reallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
        //                        error_status = "True";
        //                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
        //                        error_Count_2 = 1;
        //                        Error_Count = 1;
        //                        Record_Count = 0;
        //                    }

        //                    else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
        //                    {
        //                        if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 5)
        //                        {
        //                            DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                            styleFormatForOffer.Appearance.BackColor = Color.Red;
        //                            styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                            styleFormatForOffer.Column = this.gridColumn34;

        //                            error_status = "True";
        //                            gridView2.SetRowCellValue(a, "Error_Status", error_status);

        //                            error_Count_3 = 1;
        //                            Error_Count = 1;
        //                            Record_Count = 0;
        //                        }
        //                    }
        //                    else if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20)
        //                    {
        //                        if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
        //                        {
        //                            if (Differnce_Time > 5 || Differnce_Time == 0)
        //                            {
        //                                Hashtable htupassin = new Hashtable();
        //                                DataTable dtupassign = new DataTable();

        //                                htupassin.Add("@Trans", "DELET_BY_ORDER");
        //                                htupassin.Add("@Order_Id", Order_Id);
        //                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

        //                                Hashtable htinsert_Assign = new Hashtable();
        //                                DataTable dtinsertrec_Assign = new System.Data.DataTable();
        //                                htinsert_Assign.Add("@Trans", "INSERT");
        //                                htinsert_Assign.Add("@Order_Id", Order_Id);
        //                                htinsert_Assign.Add("@User_Id", User_id_value);
        //                                htinsert_Assign.Add("@Order_Status_Id", Order_StatusId_Value);
        //                                htinsert_Assign.Add("@Order_Progress_Id", 6);
        //                                htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
        //                                htinsert_Assign.Add("@Assigned_By", User_id);
        //                                htinsert_Assign.Add("@Modified_By", User_id);
        //                                htinsert_Assign.Add("@Modified_Date", DateTime.Now);
        //                                htinsert_Assign.Add("@status", "True");
        //                                htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
        //                                dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);


        //                                Hashtable htinsertrec = new Hashtable();
        //                                DataTable dtinsertrec = new System.Data.DataTable();
        //                                DateTime date = new DateTime();
        //                                date = DateTime.Now;
        //                                string dateeval = date.ToString("dd/MM/yyyy");
        //                                string time = date.ToString("hh:mm tt");

        //                                Hashtable htorderStatus = new Hashtable();
        //                                DataTable dtorderStatus = new DataTable();
        //                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
        //                                htorderStatus.Add("@Order_ID", Order_Id);
        //                                htorderStatus.Add("@Order_Status", Order_StatusId_Value);
        //                                htorderStatus.Add("@Modified_By", User_id);
        //                                htorderStatus.Add("@Modified_Date", date);
        //                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

        //                                Hashtable htupdate_Prog = new Hashtable();
        //                                DataTable dtupdate_Prog = new System.Data.DataTable();
        //                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
        //                                htupdate_Prog.Add("@Order_ID", Order_Id);
        //                                htupdate_Prog.Add("@Order_Progress", 6);
        //                                htupdate_Prog.Add("@Modified_By", User_id);
        //                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
        //                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


        //                                //OrderHistory
        //                                Hashtable ht_Order_History = new Hashtable();
        //                                DataTable dt_Order_History = new DataTable();
        //                                ht_Order_History.Add("@Trans", "INSERT");
        //                                ht_Order_History.Add("@Order_Id", Order_Id);
        //                                ht_Order_History.Add("@User_Id", User_id_value);
        //                                ht_Order_History.Add("@Status_Id", Order_StatusId_Value);
        //                                ht_Order_History.Add("@Progress_Id", 6);
        //                                ht_Order_History.Add("@Work_Type", 1);
        //                                ht_Order_History.Add("@Assigned_By", User_id);
        //                                ht_Order_History.Add("@Modification_Type", "Order Reallocate");
        //                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

        //                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

        //                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
        //                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
        //                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
        //                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
        //                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

        //                                if (dt_Order_InTitleLogy.Rows.Count > 0)
        //                                {

        //                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
        //                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());
        //                                    // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
        //                                    if (External_Client_Order_Task_Id != 18)
        //                                    {
        //                                        if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
        //                                        {
        //                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
        //                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

        //                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
        //                                        }
        //                                        else
        //                                        {
        //                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
        //                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
        //                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

        //                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
        //                                        }
        //                                    }
        //                                }

        //                                htinsertrec.Clear();
        //                                dtinsertrec.Clear();

        //                                htorderStatus.Clear();
        //                                dtorderStatus.Clear();
        //                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
        //                                htorderStatus.Add("@Order_ID", Order_Id);
        //                                htorderStatus.Add("@Order_Status", Order_StatusId_Value);
        //                                htorderStatus.Add("@Modified_By", User_id);
        //                                htorderStatus.Add("@Modified_Date", date);
        //                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

        //                                htupdate_Prog.Clear();
        //                                dtupdate_Prog.Clear();
        //                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
        //                                htupdate_Prog.Add("@Order_ID", Order_Id);
        //                                htupdate_Prog.Add("@Order_Progress", 6);
        //                                htupdate_Prog.Add("@Modified_By", User_id);
        //                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
        //                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

        //                                // txt_Order_number_TextChanged();
        //                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                                styleFormatForOffer.Appearance.BackColor = Color.Blue;
        //                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                                styleFormatForOffer.Column = this.gridColumn34;

        //                                error_status = "False";
        //                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
        //                                Record_Count = 1;
        //                                Error_Count = 0;

        //                            }
        //                            else
        //                            {
        //                                //errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
        //                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                                styleFormatForOffer.Appearance.BackColor = Color.Red;
        //                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                                styleFormatForOffer.Column = this.gridColumn34;

        //                                error_status = "True";
        //                                // errormessage = "This Order is Processing by Tax Team";
        //                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
        //                                error_Count_4 = 1;
        //                                Error_Count = 1;
        //                                Record_Count = 0;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                            styleFormatForOffer.Appearance.BackColor = Color.Red;
        //                            styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                            styleFormatForOffer.Column = this.gridColumn34;

        //                            error_status = "True";
        //                            // errormessage = "This Order is Processing by Tax Team";
        //                            gridView2.SetRowCellValue(a, "Error_Status", error_status);
        //                            error_Count_5 = 1;
        //                            Error_Count = 1;
        //                            Record_Count = 0;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        error_status = "True";
        //                        //errormessage = "Abstractor Order Cannot be Reallocate";
        //                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
        //                        error_Count_6 = 1;
        //                        Error_Count = 1;
        //                        Record_Count = 0;
        //                    }
        //                }   // for loop close

        //            }
        //            catch (Exception ex)
        //            {

        //            }


        //            finally
        //            {
        //                //gridView2.EndUpdate();
        //            }

        //            //
        //            if (Record_Count > 0)
        //            {
        //                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                styleFormatForOffer.Appearance.BackColor = Color.Blue;
        //                styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                styleFormatForOffer.Column = this.gridColumn34;


        //                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order ReAllocated Sucessfully.", "Success", MessageBoxButtons.OK);
        //                Get_Client_Wise_Production_Count_Orders_To_GridviewBind();


        //            }
        //            if (Error_Count > 0)
        //            {
        //                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Few Orders are not ReAllocated Please check in Error Status Column.", "Warning", MessageBoxButtons.OK);
        //                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
        //                styleFormatForOffer.Appearance.BackColor = Color.Red;

        //                // styleFormatForOffer.Column.AppearanceCell.ForeColor = Color.Red;
        //                styleFormatForOffer.Appearance.Options.UseBackColor = true;
        //                styleFormatForOffer.Column = this.gridColumn34;
        //                Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
        //            }

        //        }

        //    }
        //}
        private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            GridView view = sender as GridView;
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                case CollectionChangeAction.Remove:
                    ClearCache();

                    break;
                case CollectionChangeAction.Refresh:
                    ClearCache();
                    break;
            }
            view.LayoutChanged();

            gridView2.UpdateSummary();

            label4.Text = gridView2.SelectedRowsCount.ToString();
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {

            try
            {
                gridView2.ClearSelection();

                lookUpEdit1.EditValue = 0;
                lookUpEdit2.EditValue = 0;

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Something Went Wrong Please Check .", "Warning", MessageBoxButtons.OK);
            }
        }
        private void btn_Deallocate_Click(object sender, EventArgs e)
        {

            object obj = lookUpEdit1.EditValue;
            string Username = lookUpEdit1.Text;
            if (obj.ToString() != "0")
            {
                Userid_value = (int)obj;
            }
            int Selected_Row_Count = gridView2.SelectedRowsCount;

            // order status
            object obj_OrderStatusId = lookUpEdit2.EditValue;
            string Order_Status = lookUpEdit2.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                OrderStatusId = (int)obj_OrderStatusId;
            }

            if (Validate1() != false)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                    try
                    {
                        List<int> gridViewSelectedRows = gridView2.GetSelectedRows().ToList();
                        for (int i = 0; i < gridViewSelectedRows.Count; i++)
                        {
                            //int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
                            DataRow row = gridView2.GetDataRow(gridViewSelectedRows[i]);

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

                            Order_Id_value = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                            ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                            Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                            Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                            int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());



                            Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                            DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                            htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                            htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id_value);
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
                                htcheck_Order_In_tax.Add("@Order_Id", Order_Id_value);
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
                                    ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id_value);
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

                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn31;


                                error_status = "True";
                                // errormessage = "This Order is Processing by Tax Team";
                                gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
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
                                            htupdateTaxStatus.Add("@Modified_By", User_id);
                                            htupdateTaxStatus.Add("@Order_Id", Order_Id_value);
                                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                            {

                                                Hashtable htupassin = new Hashtable();
                                                DataTable dtupassign = new DataTable();
                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                htupassin.Add("@Order_Id", Order_Id_value);

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
                                                htorderStatus.Add("@Order_ID", Order_Id_value);
                                                htorderStatus.Add("@Order_Status", OrderStatusId);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                Hashtable htupdate_Prog = new Hashtable();
                                                DataTable dtupdate_Prog = new DataTable();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                                htupdate_Prog.Add("@Order_Progress", 8);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id_value);
                                                ht_Order_History.Add("@User_Id", Userid_value);
                                                ht_Order_History.Add("@Status_Id", OrderStatusId);
                                                ht_Order_History.Add("@Progress_Id", 8);
                                                ht_Order_History.Add("@Work_Type", 1);
                                                ht_Order_History.Add("@Assigned_By", User_id);
                                                ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                //OrderHistory
                                                Hashtable ht_Order_History1 = new Hashtable();
                                                DataTable dt_Order_History1 = new DataTable();
                                                ht_Order_History1.Add("@Trans", "INSERT");
                                                ht_Order_History1.Add("@Order_Id", Order_Id_value);
                                                ht_Order_History1.Add("@User_Id", Userid_value);
                                                ht_Order_History1.Add("@Status_Id", OrderStatusId);
                                                ht_Order_History1.Add("@Progress_Id", 8);
                                                ht_Order_History1.Add("@Work_Type", 1);
                                                ht_Order_History1.Add("@Assigned_By", User_id);
                                                ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                                dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                DataTable dt_Order_InTitleLogy = new DataTable();
                                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id_value);
                                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                {

                                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                                    // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title

                                                    if (External_Client_Order_Task_Id != 18)
                                                    {

                                                        if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }
                                                        else
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
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
                                                htorderStatus.Add("@Order_ID", Order_Id_value);
                                                htorderStatus.Add("@Order_Status", Order_Satatus_Id);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                htupdate_Prog.Clear();
                                                dtupdate_Prog.Clear();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                                htupdate_Prog.Add("@Order_Progress", 8);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                                styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                                styleFormatForOffer.Column = this.gridColumn31;

                                                error_status = "False";
                                                gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                                Record_Count = 1;
                                                Error_Count = 0;

                                            }

                                        }

                                    }

                                }
                            }


                            if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                            {
                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn31;

                                errormessage = "This Order is Assigned To Vendor and It will Not Reallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
                                error_status = "True";
                                gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                error_Count_2 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }

                            else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                            {
                                if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 5)
                                {

                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn31;

                                    error_status = "True";
                                    errormessage = "This Order is in Work in Progress you can't Reallocate";
                                    gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
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
                                        htupassin.Add("@Order_Id", Order_Id_value);
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
                                        htorderStatus.Add("@Order_ID", Order_Id_value);
                                        htorderStatus.Add("@Order_Status", OrderStatusId);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                        Hashtable htupdate_Prog = new Hashtable();
                                        DataTable dtupdate_Prog = new System.Data.DataTable();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                        htupdate_Prog.Add("@Order_Progress", 8);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                        //OrderHistory
                                        Hashtable ht_Order_History = new Hashtable();
                                        DataTable dt_Order_History = new DataTable();
                                        ht_Order_History.Add("@Trans", "INSERT");
                                        ht_Order_History.Add("@Order_Id", Order_Id_value);
                                        ht_Order_History.Add("@User_Id", Userid_value);
                                        ht_Order_History.Add("@Status_Id", OrderStatusId);
                                        ht_Order_History.Add("@Progress_Id", 8);
                                        ht_Order_History.Add("@Work_Type", 1);
                                        ht_Order_History.Add("@Assigned_By", User_id);
                                        ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                        //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                        DataTable dt_Order_InTitleLogy = new DataTable();
                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id_value);
                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
                                        {

                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                            // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                            if (External_Client_Order_Task_Id != 18)
                                            {

                                                if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                                else
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    DataTable dt_TitleLogy_Order_Task_Status = new DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
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
                                        htorderStatus.Add("@Order_ID", Order_Id_value);
                                        htorderStatus.Add("@Order_Status", OrderStatusId);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                        htupdate_Prog.Clear();
                                        dtupdate_Prog.Clear();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id_value);
                                        htupdate_Prog.Add("@Order_Progress", 8);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);



                                        // txt_Order_number_TextChanged();
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn31;

                                        error_status = "False";
                                        gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                        Record_Count = 1;
                                        Error_Count = 0;
                                    }
                                    else
                                    {
                                        errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Red;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn31;


                                        error_status = "True";
                                        // errormessage = "This Order is Processing by Tax Team";
                                        gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                        error_Count_1 = 1;
                                        Error_Count = 1;
                                        Record_Count = 0;
                                    }
                                }
                                else
                                {

                                    errormessage = "Order Is in Work in Progress in Tax Team";
                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn31;

                                    error_status = "True";
                                    // errormessage = "This Order is Processing by Tax Team";
                                    gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                    error_Count_1 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }
                            else
                            {
                                error_status = "True";
                                errormessage = "Abstractor Order Cannot be Reallocate";
                                gridView2.SetRowCellValue(gridViewSelectedRows[i], "Error_Status", error_status);
                                error_Count_1 = 1;
                                Error_Count = 1;
                                Record_Count = 0;

                            }

                        }   // for loop close
                    }
                    catch (Exception ex)
                    {

                    }

                    if (Record_Count > 0)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn31;

                        DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order DeAllocated Sucessfully.", "Success", MessageBoxButtons.OK);
                        BindOrdersByOperation();
                        btn_Clear_Click(sender, e);

                    }

                    if (Error_Count > 0)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Red;

                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn31;
                        XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Few Orders are not DeAllocated Please check in Error Status Column.", "Warning", MessageBoxButtons.OK);
                        BindOrdersByOperation();
                    }


                }

            }


        }
    }
}