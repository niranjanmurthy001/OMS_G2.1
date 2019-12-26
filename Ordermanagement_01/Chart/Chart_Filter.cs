using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.Utils;
using DevExpress.XtraCharts;

namespace Ordermanagement_01.Chart
{
    public partial class Chart_Filter : Form
    {
        int Err_Userid, Error_Desc_User_Id, ErrUserid, Error_User_Id;
        int ErrorTab_User_Id, ErrorDesc_User_Id, ErrorClient_User_Id, ErrorState_User_Id;
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_ID, User_Role, error_Task, errortask_id, error_Status_id, error_tab_id, error_Desc_id, Error_On_User_id, clientId, Subprocess_id, stateId, County_Id, State_Id;
        string Er_Tab_argument, Er_Desc_argument, Error_On_UserName_argument, Error_OnClient, Error_OnState;
        string ertab_errorcount , erDesc_errorcount, eruser_errorcount, Error_Client_Count, Error_State_Count, values;
        string ProductionDate, Error_Tab_Page, errortask, error_Status, error_Tab;
        string State; string Type_Name, Error_Type_Name, ErrorTypeName;
        string Order_Task = "", Order_Status = "", Error_Tab = "", Error_Desc = "", Error_On_User = "",Error_Client="", Error_Subprocess="",Error_State="",Error_County="";
        string Single_Order_Task = "", Single_Order_Status = "", Single_Error_Tab = "";
        int Order_Task_Count, Order_Status_Count, Error_Tab_Count, Error_Desc_Count, Error_OnUser_Count, Error_OnClient_Count, Error_OnState_Count,Error_County_Count, State_Count,County_Count,
            Error_Subprocess_Count,SubclientCount,ClientCount;
        string SingleOrderTask ;
        string multiple_errortask;
        int Client_Count; string Single_Client = "", Client = "";
        int Clientid, subprocessid, Error_Type_Id, Error_description_Id;
        string ClientName, State_Name;
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        Hashtable htlivecount = new Hashtable();
        DataTable dtlivecount = new DataTable();
        public Chart_Filter(int User_Id, int Role, string Produ_Date)
        {
            InitializeComponent();
            User_ID = User_Id;
            User_Role = Role;
            ProductionDate = Produ_Date;

            chartControl1.CrosshairEnabled = DefaultBoolean.False;
            chartControl1.RuntimeHitTesting = true;

            chartControl2.CrosshairEnabled = DefaultBoolean.False;
            chartControl2.RuntimeHitTesting = true;

            chartControl3.CrosshairEnabled = DefaultBoolean.False;
            chartControl3.RuntimeHitTesting = true;

            chartControl4.CrosshairEnabled = DefaultBoolean.False;
            chartControl4.RuntimeHitTesting = true;

            chartControl5.CrosshairEnabled = DefaultBoolean.False;
            chartControl5.RuntimeHitTesting = true;
        }

        private void Chart_Filter_Load(object sender, EventArgs e)
        {
            Grid_Bind_Error_Task();
            Grid_Bind_Error_Status();
            Grid_Bind_Error_Tab();
            Bind_UserName_ErrorTab(ddl_ErrorOnUser);

            Bind_UserName_ErrorDesc(ddl_Err_desc_ErrorOnUser);
            Grid_Bind_Error_Description();        //--------- error_Status desc
            Grid_Bind_Error_Desc_Error_Task();
            Grid_Bind_Error_Desc_Error_Status();

            Grid_BindError_Task();    //--------- Error On uSer
            Grid_BindError_Status();
            Grid_Bind_Error_On_User();

            Bind_Grid_Error_Task();      //--------- Error On Client
            Bind_Grid_Error_Status();
            Bind_Grid_Client();

            Bind_UserName_ErrorClient(ddl_ErrClient_ErrorOnUser);
           
            //Bind_AllSubc();

            Bind_Grid_State_Error_Task();        //--------- Error On State
            Bind_Grid_State_Error_Status();
            Bind_Grid_State();
            Bind_UserName_ErrorState(ddl_State_ErrorOnUser);

            string D1 = DateTime.Now.ToString("MM/dd/yyyy");
            string D2 = DateTime.Now.ToString("MM/dd/yyyy");

            txt_Error_Tab_From_Date.Format = DateTimePickerFormat.Custom;
            txt_Error_Tab_To_Date.Format = DateTimePickerFormat.Custom;

            txt_Error_Tab_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_Error_Tab_To_Date.CustomFormat = "MM/dd/yyyy";

            txt_Error_Tab_From_Date.Text = D1;
            txt_Error_Tab_To_Date.Text = D2;


            txt_Error_Desc_From_Date.Format = DateTimePickerFormat.Custom;
            txt_Error_Desc_To_Date.Format = DateTimePickerFormat.Custom;

            txt_Error_Desc_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_Error_Desc_To_Date.CustomFormat = "MM/dd/yyyy";

            txt_Error_Desc_From_Date.Text = D1;
            txt_Error_Desc_To_Date.Text = D2;

            txt_ErrorOnUser_From_Date.Format = DateTimePickerFormat.Custom;
            txt_ErrorOnUser_To_Date.Format = DateTimePickerFormat.Custom;

            txt_ErrorOnUser_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_ErrorOnUser_To_Date.CustomFormat = "MM/dd/yyyy";

            txt_ErrorOnUser_From_Date.Text = D1;
            txt_ErrorOnUser_To_Date.Text = D2;



            txt_Client_From_Date.Format = DateTimePickerFormat.Custom;
            txt_Client_To_Date.Format = DateTimePickerFormat.Custom;

            txt_Client_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_Client_To_Date.CustomFormat = "MM/dd/yyyy";

            txt_Client_From_Date.Text = D1;
            txt_Client_To_Date.Text = D2;

            txt_State_From_Date.Format = DateTimePickerFormat.Custom;
            txt_State_To_Date.Format = DateTimePickerFormat.Custom;

            txt_State_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_State_To_Date.CustomFormat = "MM/dd/yyyy";

            txt_State_From_Date.Text = D1;
            txt_State_To_Date.Text = D2;
           
        }

        private void Grid_Bind_Error_Task()
        {
            Hashtable ht_getErrorTask = new Hashtable();
            DataTable dt_getErrorTask = new DataTable();

            ht_getErrorTask.Add("@Trans", "BIND_ERROR_TASK");
            dt_getErrorTask = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getErrorTask);
            if (dt_getErrorTask.Rows.Count > 0)
            {
                grd_Error_Task.Rows.Clear();
                for (int i = 0; i < dt_getErrorTask.Rows.Count; i++)
                {
                    grd_Error_Task.Rows.Add();
                    grd_Error_Task.Rows[i].Cells[1].Value = dt_getErrorTask.Rows[i]["Order_Status"].ToString();
                    grd_Error_Task.Rows[i].Cells[2].Value = dt_getErrorTask.Rows[i]["Order_Status_Id"].ToString();
                }
            }
            else
            {
                grd_Error_Task.Rows.Clear();

            }
        }

        private void Grid_Bind_Error_Status()
        {
            Hashtable ht_getErrorStatus = new Hashtable();
            DataTable dt_getErrorStatus = new DataTable();

            ht_getErrorStatus.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_getErrorStatus = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getErrorStatus);
            if (dt_getErrorStatus.Rows.Count > 0)
            {
                grd_Error_Status.Rows.Clear();
                for (int i = 0; i < dt_getErrorStatus.Rows.Count; i++)
                {
                    grd_Error_Status.Rows.Add();
                    grd_Error_Status.Rows[i].Cells[1].Value = dt_getErrorStatus.Rows[i]["Error_Status"].ToString();
                    grd_Error_Status.Rows[i].Cells[2].Value = dt_getErrorStatus.Rows[i]["Error_Status_Id"].ToString();
                }
            }
            else
            {
                grd_Error_Status.Rows.Clear();

            }
        }

        private void Grid_Bind_Error_Tab()
        {
            Hashtable ht_getErrorTab = new Hashtable();
            DataTable dt_getErrorTab = new DataTable();

            ht_getErrorTab.Add("@Trans", "SELECT_ERROR_TAB");
            dt_getErrorTab = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getErrorTab);
            if (dt_getErrorTab.Rows.Count > 0)
            {
                grd_Error_Tab.Rows.Clear();
                for (int i = 0; i < dt_getErrorTab.Rows.Count; i++)
                {
                    grd_Error_Tab.Rows.Add();
                    grd_Error_Tab.Rows[i].Cells[1].Value = dt_getErrorTab.Rows[i]["Error_Type"].ToString();
                    grd_Error_Tab.Rows[i].Cells[2].Value = dt_getErrorTab.Rows[i]["Error_Type_Id"].ToString();
                }
            }
            else
            {
                grd_Error_Tab.Rows.Clear();

            }
        }


        private void btn_Error_Tab_Clear_Click(object sender, EventArgs e)
        {
            Chk_All_Order_Status.Checked = false;
            chk_All_Error_Status.Checked = false;
            Chk_All_Error_Tab.Checked = false;
            chartControl1.DataSource = null;
            Grid__All_Order_Status_Count.DataSource = null;
            Grid_Bind_Error_Task();
            Grid_Bind_Error_Status();
            Grid_Bind_Error_Tab();

            ddl_ErrorOnUser.SelectedIndex = 0;
            Grid_All_Task_Wise_Count();
            Bind_Bar_Error_Tab_All_Task();
        }

        private void btn_Error_Tab_Export_Click(object sender, EventArgs e)
        {
            Export_Error_Tab();
        }

        private void Export_Error_Tab()
        {
            load_Progressbar.Start_progres();
            DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl = new DevExpress.XtraPrintingLinks.CompositeLink(ps);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart = new DevExpress.XtraPrinting.PrintableComponentLink();

            chartControl1.Visible = true;
            pclChart.Component = chartControl1;

            cl.PaperKind = System.Drawing.Printing.PaperKind.A3;
            cl.Landscape = true;
            cl.Margins.Right = 40;
            cl.Margins.Left = 40;
            cl.Margins.Top = 40;
            cl.Margins.Bottom = 40;
            cl.Links.AddRange(new object[] { pclChart });
            cl.ShowPreviewDialog();


        }


        //  BAr Chart Error Tab All Task Wise
        private void Bind_Bar_Error_Tab_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Barchart = new Hashtable();
                DataTable dt_ErrTab_BarChart = new DataTable();
                //ht_ErrTab_Barchart.Add("@Trans", "BAR_CHART_ERROR_TAB_DATE_WISE_ALL_TASK");
                ht_ErrTab_Barchart.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ALL_TASK");
                ht_ErrTab_Barchart.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Barchart.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                dt_ErrTab_BarChart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl1.DataSource = dt_ErrTab_BarChart;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Error_Tab_All_Task();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Error_Tab_All_Task()
        {
            try
            {

                //Line Chart
                Hashtable ht_ErrTab_LineChart = new Hashtable();
                DataTable dt_ErrTab_LineChart = new DataTable();
                ht_ErrTab_LineChart.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ALL_TASK");
                ht_ErrTab_LineChart.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_LineChart.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                dt_ErrTab_LineChart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_LineChart);
                // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl1.DataSource = dt_ErrTab_LineChart;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        ////  Bar chart Indivsual  Task Wise
        //private void Bind_Bar_Chart_Error_Task_Wise(string SingleOrderTask)
        //{
        //    try
        //    {
             
        //        //Bar Chart
        //        Hashtable ht_ErrTab_task = new Hashtable();
        //        DataTable dt_ErrTab_task = new DataTable();
                
        //        ht_ErrTab_task.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_WISE");
        //        ht_ErrTab_task.Add("@Error_Task", SingleOrderTask);
        //        ht_ErrTab_task.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
        //        ht_ErrTab_task.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
        //        dt_ErrTab_task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_task);
        //        chartControl1.DataSource = dt_ErrTab_task;

        //        chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        //        chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
        //        chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

        //        Bind_Line_Chart_Error_Task_Wise(SingleOrderTask);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        //private void Bind_Line_Chart_Error_Task_Wise(string SingleOrderTask)
        //{
        //    try
        //    {
              
        //        //Bar Chart
        //        Hashtable ht_ErrTab_Task_Line = new Hashtable();
        //        DataTable dt_ErrTab_Task_Line = new DataTable();
        //        ht_ErrTab_Task_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_WISE");
        //        ht_ErrTab_Task_Line.Add("@Error_Task", SingleOrderTask);
        //        ht_ErrTab_Task_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
        //        ht_ErrTab_Task_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
              
        //        dt_ErrTab_Task_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Task_Line);
               
        //        chartControl1.DataSource = dt_ErrTab_Task_Line;
        //        chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        //        chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
        //        chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        //  Bar chart Indivsual  Multiple task  Wise 09/10/2018  

        private void Bind_Bar_Chart_Error_MultipleTask_Wise(string multiple_errortask)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrTab_task = new Hashtable();
                DataTable dt_ErrTab_task = new DataTable();
               
                ht_ErrTab_task.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_MULTIPLE_TASK_WISE");
                ht_ErrTab_task.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_task.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_task.Add("@Error_Task", multiple_errortask);

                dt_ErrTab_task = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_task);
               
                chartControl1.DataSource = dt_ErrTab_task;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_MultipleTask_Wise(multiple_errortask);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_MultipleTask_Wise(string multiple_errortask)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrTab_Task_Line = new Hashtable();
                DataTable dt_ErrTab_Task_Line = new DataTable();
              
                ht_ErrTab_Task_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_MULTIPLE_TASK_WISE");
                ht_ErrTab_Task_Line.Add("@Error_Task", multiple_errortask);
               
                ht_ErrTab_Task_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                dt_ErrTab_Task_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Task_Line);
               
                chartControl1.DataSource = dt_ErrTab_Task_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Bar chart  Error  Status Wise  10 july 2018
        private void Bind_Bar_Chart_Error_Status_Wise(string error_Status)
        {
            try
            {
                //errorstatus = error_Status;
                //Bar Chart
                Hashtable ht_ErrTab_Status = new Hashtable();
                DataTable dt_ErrTab_Status = new DataTable();

                ht_ErrTab_Status.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ERRROR_STATUS_WISE");
                ht_ErrTab_Status.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Status.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Status.Add("@Error_Status", error_Status);
                dt_ErrTab_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Status);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Status;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Status_Wise(error_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Status_Wise(string error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Err_Status_Line = new Hashtable();
                DataTable dt_Err_Status_Line = new DataTable();
                ht_Err_Status_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ERRROR_STATUS_WISE");
                ht_Err_Status_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Err_Status_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Err_Status_Line.Add("@Error_Status", error_Status);
                dt_Err_Status_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Err_Status_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Err_Status_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Bar chart Tab Wise

        private void Bind_Bar_Chart_Error_Tab_Wise(string error_Tab_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Wise = new Hashtable();
                DataTable dt_ErrTab_Wise = new DataTable();

                ht_ErrTab_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ERROR_TAB_WISE");
                ht_ErrTab_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Wise.Add("@Error_Type", error_Tab_id);
                dt_ErrTab_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Tab_Wise(error_Tab_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Tab_Wise(string error_Tab_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Wise_Line = new Hashtable();
                DataTable dt_ErrTab_Wise_Line = new DataTable();
                ht_ErrTab_Wise_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ERROR_TAB_WISE");
                ht_ErrTab_Wise_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Wise_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Wise_Line.Add("@Error_Type", error_Tab_id);
                dt_ErrTab_Wise_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Wise_Line);
                // dt_Errror_Tab = dt_ErrTab_Wise_Line;

                chartControl1.DataSource = dt_ErrTab_Wise_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // task and status wise 09/10/2018  

        private void Bind_Bar_Chart_Error_Task_AND_Status_Wise(string errortask, string errorstatus)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_MULTIPLE_WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Task", errortask);
                ht_Errtask_Status_Wise.Add("@Error_Status", errorstatus);
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_Status_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_AND_Status_Wise(errortask, errorstatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_AND_Status_Wise(string errortask, string errorstatus)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_Status_Line = new Hashtable();
                DataTable dt_ErrTask_Status_Line = new DataTable();
                ht_ErrTask_Status_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_MULTIPLE_WISE");
                ht_ErrTask_Status_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_Task", errortask);
                ht_ErrTask_Status_Line.Add("@Error_Status", errorstatus);
                dt_ErrTask_Status_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTask_Status_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTask_Status_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // task and Error tab wise   10-07-2018

        private void Bind_Bar_Chart_Error_Task_AND_ErrorTab_Wise(string error_task, string error_tab)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_AND_ERRORTAB_MULTIPLE_WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Task", error_task);
                ht_Errtask_Status_Wise.Add("@Error_Type", error_tab);
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_Status_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_AND_ErrorTab_Wise(error_task, error_tab);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_AND_ErrorTab_Wise(string error_task, string error_tab)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_Status_Line = new Hashtable();
                DataTable dt_ErrTask_Status_Line = new DataTable();
                ht_ErrTask_Status_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_AND_ERRORTAB_MULTIPLE_WISE");
                ht_ErrTask_Status_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_Task", error_task);
                ht_ErrTask_Status_Line.Add("@Error_Type", error_tab);
                dt_ErrTask_Status_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTask_Status_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTask_Status_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Error tab and Status wise   10-07-2018

        private void Bind_Bar_Chart_ErrorTab_AND_Status_Wise(string error_Status,string error_tab)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_AND_STATUS_MULTIPLE_WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Status", error_Status);
                ht_Errtask_Status_Wise.Add("@Error_Type", error_tab);
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_Status_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_ErrorTab_AND_Status_Wise(error_Status,error_tab);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_ErrorTab_AND_Status_Wise(string error_Status,string error_tab)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_Status_Line = new Hashtable();
                DataTable dt_ErrTask_Status_Line = new DataTable();
                ht_ErrTask_Status_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_AND_STATUS_MULTIPLE_WISE");
                ht_ErrTask_Status_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_Status", error_Status);
                ht_ErrTask_Status_Line.Add("@Error_Type", error_tab);
                dt_ErrTask_Status_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTask_Status_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTask_Status_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //  Task And Status and  Error Tab  wise
        private void Bind_Bar_Chart_Error_Tab_AND_Task_AND_Status_Wise(string errortask, string error_Status, string error_Tab)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Status = new Hashtable();
                DataTable dt_ErrTab_Task_Status = new DataTable();

                ht_ErrTab_Task_Status.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_TAB_WISE");
                ht_ErrTab_Task_Status.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Status.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Status.Add("@Error_Task", errortask);
                ht_ErrTab_Task_Status.Add("@Error_Status", error_Status);
                ht_ErrTab_Task_Status.Add("@Error_Type", error_Tab);
                dt_ErrTab_Task_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Task_Status);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Task_Status;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Tab_AND_Task_AND_Status_Wise(errortask, error_Status, error_Tab);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Tab_AND_Task_AND_Status_Wise(string errortask, string error_Status, string error_Tab)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Status_Linechart = new Hashtable();
                DataTable dt_ErrTab_Task_Status_Linechart = new DataTable();
                ht_ErrTab_Task_Status_Linechart.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_TAB_WISE");
                ht_ErrTab_Task_Status_Linechart.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Task", errortask);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Status", error_Status);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Type", error_Tab);
                dt_ErrTab_Task_Status_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Task_Status_Linechart);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Task_Status_Linechart;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //  Task And Status and  Error Tab and user  wise
        private void Bind_Bar_Chart_Error_Tab_AND_Task_AND_Status_Wise(string errortask, string error_Status, string error_Tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Status_User = new Hashtable();
                DataTable dt_ErrTab_Task_Status_User = new DataTable();

                ht_ErrTab_Task_Status_User.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_TAB_AND_USER_WISE");
                ht_ErrTab_Task_Status_User.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Status_User.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Status_User.Add("@Error_Task", errortask);
                ht_ErrTab_Task_Status_User.Add("@Error_Status", error_Status);
                ht_ErrTab_Task_Status_User.Add("@Error_Type", error_Tab);
                ht_ErrTab_Task_Status_User.Add("@User_Id", Err_Userid);
                dt_ErrTab_Task_Status_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Task_Status_User);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Task_Status_User;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Tab_AND_Task_AND_Status_Wise(errortask, error_Status, error_Tab, Err_Userid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Tab_AND_Task_AND_Status_Wise(string errortask, string error_Status, string error_Tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Status_Linechart = new Hashtable();
                DataTable dt_ErrTab_Task_Status_Linechart = new DataTable();
                ht_ErrTab_Task_Status_Linechart.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_TAB_AND_USER_WISE");
                ht_ErrTab_Task_Status_Linechart.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Task", errortask);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Status", error_Status);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Type", error_Tab);
                ht_ErrTab_Task_Status_Linechart.Add("@User_Id", Err_Userid);
                dt_ErrTab_Task_Status_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Task_Status_Linechart);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Task_Status_Linechart;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // task and User wise   17-07-2018

        private void Bind_Bar_Chart_Error_Task_AND_User_Wise(string error_task, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_User_Wise = new Hashtable();
                DataTable dt_Errtask_User_Wise = new DataTable();

                ht_Errtask_User_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_AND_USER_MULTIPLE_WISE");
                ht_Errtask_User_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_User_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_User_Wise.Add("@Error_Task", error_task);
                ht_Errtask_User_Wise.Add("@User_Id", Err_Userid);
                dt_Errtask_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_User_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_User_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_AND_User_Wise(error_task, Err_Userid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_AND_User_Wise(string error_task, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_User_Line = new Hashtable();
                DataTable dt_ErrTask_User_Line = new DataTable();
                ht_ErrTask_User_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_AND_USER_MULTIPLE_WISE");
                ht_ErrTask_User_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_User_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_User_Line.Add("@Error_Task", error_task);
                ht_ErrTask_User_Line.Add("@User_Id", Err_Userid);
                dt_ErrTask_User_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTask_User_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTask_User_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // status and User wise   17-07-2018

        private void Bind_Bar_Chart_Error_Status_AND_User_Wise(string error_Status, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrStatus_UserWise = new Hashtable();
                DataTable dt_ErrStatus_UserWise = new DataTable();

                ht_ErrStatus_UserWise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_USER_MULTIPLE_WISE");
                ht_ErrStatus_UserWise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrStatus_UserWise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrStatus_UserWise.Add("@Error_Status", error_Status);
                ht_ErrStatus_UserWise.Add("@User_Id", Err_Userid);
                dt_ErrStatus_UserWise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatus_UserWise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrStatus_UserWise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Status_AND_User_Wise(error_Status, Err_Userid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Status_AND_User_Wise(string error_Status, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrStatus_UserLine = new Hashtable();
                DataTable dt_ErrStatus_UserLine = new DataTable();
                ht_ErrStatus_UserLine.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_USER_MULTIPLE_WISE");
                ht_ErrStatus_UserLine.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrStatus_UserLine.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrStatus_UserLine.Add("@Error_Status", error_Status);
                ht_ErrStatus_UserLine.Add("@User_Id", Err_Userid);
                dt_ErrStatus_UserLine = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatus_UserLine);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrStatus_UserLine;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Error tab and USER wise   

        private void Bind_Bar_Chart_ErrorTab_AND_User_Wise(string error_tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_user_Wise = new Hashtable();
                DataTable dt_Errtask_user_Wise = new DataTable();

                ht_Errtask_user_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ERRORTAB_AND_USER_MULTIPLE_WISE");
                ht_Errtask_user_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_user_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_user_Wise.Add("@User_Id", Err_Userid);
                ht_Errtask_user_Wise.Add("@Error_Type", error_tab);
                dt_Errtask_user_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_user_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_user_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_ErrorTab_AND_User_Wise(error_tab, Err_Userid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_ErrorTab_AND_User_Wise(string error_tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_User_Line = new Hashtable();
                DataTable dt_ErrTask_User_Line = new DataTable();
                ht_ErrTask_User_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ERRORTAB_AND_USER_MULTIPLE_WISE");
                ht_ErrTask_User_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_User_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_User_Line.Add("@User_Id", Err_Userid);
                ht_ErrTask_User_Line.Add("@Error_Type", error_tab);
                dt_ErrTask_User_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTask_User_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTask_User_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // task and status and User wise 09/10/2018  

        private void Bind_Bar_Chart_Error_Task_AND_Status_And_User_Wise(string errortask, string errorstatus, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_Status_User_Wise = new Hashtable();
                DataTable dt_Errtask_Status_User_Wise = new DataTable();

                ht_Errtask_Status_User_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_STATUS_AND_USER_MULTIPLE_WISE");
                ht_Errtask_Status_User_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_Status_User_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_Status_User_Wise.Add("@Error_Task", errortask);
                ht_Errtask_Status_User_Wise.Add("@Error_Status", errorstatus);
                ht_Errtask_Status_User_Wise.Add("@User_Id", Err_Userid);
                dt_Errtask_Status_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_User_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_Status_User_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_AND_Status_And_User_Wise(errortask, errorstatus, Err_Userid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_AND_Status_And_User_Wise(string errortask, string errorstatus, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_StatusUser_Line = new Hashtable();
                DataTable dt_ErrTask_StatusUser_Line = new DataTable();
                ht_ErrTask_StatusUser_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_STATUS_AND_USER_MULTIPLE_WISE");
                ht_ErrTask_StatusUser_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_StatusUser_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_StatusUser_Line.Add("@Error_Task", errortask);
                ht_ErrTask_StatusUser_Line.Add("@Error_Status", errorstatus);
                ht_ErrTask_StatusUser_Line.Add("@User_Id", Err_Userid);
                dt_ErrTask_StatusUser_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTask_StatusUser_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTask_StatusUser_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // task and Errror Tab and User wise 09/10/2018  

        private void Bind_Bar_Chart_Error_Task_AND_ErrorTab_And_User_Wise(string errortask, string error_tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_Status_User_Wise = new Hashtable();
                DataTable dt_Errtask_Status_User_Wise = new DataTable();

                ht_Errtask_Status_User_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_ERRORTAB_AND_USER_MULTIPLE_WISE");
                ht_Errtask_Status_User_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_Status_User_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_Status_User_Wise.Add("@Error_Task", errortask);
                ht_Errtask_Status_User_Wise.Add("@Error_Type", error_tab);
                ht_Errtask_Status_User_Wise.Add("@User_Id", Err_Userid);
                dt_Errtask_Status_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_User_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_Status_User_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_AND_ErrorTab_And_User_Wise(errortask, error_tab, Err_Userid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_AND_ErrorTab_And_User_Wise(string errortask, string error_tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_StatusUser_Line = new Hashtable();
                DataTable dt_ErrTask_StatusUser_Line = new DataTable();
                ht_ErrTask_StatusUser_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_ERRORTAB_AND_USER_MULTIPLE_WISE");
                ht_ErrTask_StatusUser_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_StatusUser_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_StatusUser_Line.Add("@Error_Task", errortask);
                ht_ErrTask_StatusUser_Line.Add("@Error_Type", error_tab);
                ht_ErrTask_StatusUser_Line.Add("@User_Id", Err_Userid);
                dt_ErrTask_StatusUser_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTask_StatusUser_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTask_StatusUser_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // Error tab and Status and User wise   17-07-2018

        private void Bind_Bar_Chart_ErrorTab_AND_Staus_User_Wise(string error_Status, string error_tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtab_StatusUser_Wise = new Hashtable();
                DataTable dt_Errtab_StatusUser_Wise = new DataTable();

                ht_Errtab_StatusUser_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_ERRORTAB_AND_USER_MULTIPLE_WISE");
                ht_Errtab_StatusUser_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtab_StatusUser_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtab_StatusUser_Wise.Add("@Error_Status", error_Status);
                ht_Errtab_StatusUser_Wise.Add("@Error_Type", error_tab);
                ht_Errtab_StatusUser_Wise.Add("@User_Id", Err_Userid);
                dt_Errtab_StatusUser_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtab_StatusUser_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtab_StatusUser_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_ErrorTab_AND_Staus_User_Wise(error_Status, error_tab, Err_Userid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_ErrorTab_AND_Staus_User_Wise(string error_Status, string error_tab, int Err_Userid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_StatusUser_Line = new Hashtable();
                DataTable dt_ErrTab_StatusUser_Line = new DataTable();
                ht_ErrTab_StatusUser_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_ERRORTAB_AND_USER_MULTIPLE_WISE");
                ht_ErrTab_StatusUser_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_StatusUser_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_StatusUser_Line.Add("@Error_Status", error_Status);
                ht_ErrTab_StatusUser_Line.Add("@Error_Type", error_tab);
                ht_ErrTab_StatusUser_Line.Add("@User_Id", Err_Userid);
                dt_ErrTab_StatusUser_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_StatusUser_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_StatusUser_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // User wise  
        private void Bind_Bar_Error_Tab_All_Task_User_Wise(int user_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Barchart_user = new Hashtable();
                DataTable dt_ErrTab_BarChart_user = new DataTable();
                //ht_ErrTab_Barchart.Add("@Trans", "BAR_CHART_ERROR_TAB_DATE_WISE_ALL_TASK");
                ht_ErrTab_Barchart_user.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ALL_TASK_USER_WISE");
                ht_ErrTab_Barchart_user.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Barchart_user.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Barchart_user.Add("@User_Id", user_id);
                dt_ErrTab_BarChart_user = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_Barchart_user);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl1.DataSource = dt_ErrTab_BarChart_user;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Error_Tab_All_Task_User_Wise(user_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Error_Tab_All_Task_User_Wise(int user_id)
        {
            try
            {

                //Line Chart
                Hashtable ht_ErrTab_LineChart_user = new Hashtable();
                DataTable dt_ErrTab_LineChart_user = new DataTable();
                ht_ErrTab_LineChart_user.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ALL_TASK_USER_WISE");
                ht_ErrTab_LineChart_user.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_LineChart_user.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_LineChart_user.Add("@User_Id", user_id);
                dt_ErrTab_LineChart_user = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrTab_LineChart_user);
                // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl1.DataSource = dt_ErrTab_LineChart_user;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void btn_Errtab_Submit_Click(object sender, EventArgs e)
        {
                load_Progressbar.Start_progres();
                Err_Userid = int.Parse(ddl_ErrorOnUser.SelectedValue.ToString());
                if (txt_Error_Tab_From_Date.Text != "" && txt_Error_Tab_To_Date.Text != "")
                {
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    Order_Task_Count = 0; Order_Status_Count = 0; Error_Tab_Count = 0;
                    for (int error_task = 0; error_task < grd_Error_Task.Rows.Count; error_task++)
                    {

                        bool is_task = (bool)grd_Error_Task[0, error_task].FormattedValue;
                        if (is_task == true)
                        {
                            Order_Task_Count = Order_Task_Count + 1;
                            errortask_id = int.Parse(grd_Error_Task.Rows[error_task].Cells[2].Value.ToString());

                            if (Order_Task_Count == 1)
                            {
                                Single_Order_Task = errortask_id.ToString();

                                Single_Order_Task = errortask_id.ToString();
                                Order_Task = Single_Order_Task;
                                sb = sb.Append(Order_Task);

                            }
                            else
                            {

                                sb = sb.Append("," + errortask_id);
                                Order_Task = sb.ToString();

                                //  Order_Task = Single_Order_Task + "," + errortask_id + "," ;
                                Order_Task_Count++;
                            }
                        }
                    }
                    for (int error_Status = 0; error_Status < grd_Error_Status.Rows.Count; error_Status++)
                    {
                        bool is_Status = (bool)grd_Error_Status[0, error_Status].FormattedValue;
                        if (is_Status == true)
                        {
                            Order_Status_Count = Order_Status_Count + 1;
                            error_Status_id = int.Parse(grd_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                            if (Order_Status_Count == 1)
                            {
                                //Single_Order_Status = error_Status_id.ToString();
                                Order_Status = error_Status_id.ToString();
                                sb1 = sb1.Append(Order_Status);
                            }
                            else
                            {
                                //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                                sb1 = sb1.Append("," + error_Status_id);
                                Order_Status = sb1.ToString();
                                Order_Status_Count++;
                            }
                        }
                    }
                    for (int error_tab = 0; error_tab < grd_Error_Tab.Rows.Count; error_tab++)
                    {

                        bool is_errortab = (bool)grd_Error_Tab[0, error_tab].FormattedValue;
                        if (is_errortab == true)
                        {
                            Error_Tab_Count = Error_Tab_Count + 1;
                            error_tab_id = int.Parse(grd_Error_Tab.Rows[error_tab].Cells[2].Value.ToString());
                            if (Error_Tab_Count == 1)
                            {

                                // Single_Error_Tab = error_tab_id.ToString();
                                Error_Tab = error_tab_id.ToString();
                                sb2 = sb2.Append(Error_Tab);
                            }
                            else
                            {
                                //Error_Tab = Single_Order_Status + "," + error_tab_id + ",";
                                //Error_Tab_Count++;

                                sb2 = sb2.Append("," + error_tab_id);
                                Error_Tab = sb2.ToString();
                                Error_Tab_Count++;
                            }


                        }

                    }
              
                     Hashtable ht_ErrTab_Barchart = new Hashtable();
                     DataTable dt_ErrTab_BarChart = new DataTable();

                     if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_Tab_Count == 0 && Err_Userid==0)
                     {
                         Bind_Bar_Error_Tab_All_Task();
                         Grid_All_Task_Wise_Count();
                     }
                     //else if (Order_Task_Count == 1)
                     //{
                     //    Bind_Bar_Chart_Error_Task_Wise(Single_Order_Task);
                     //}

                        // task wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_Tab_Count == 0 && Err_Userid == 0)
                     {
                         Bind_Bar_Chart_Error_MultipleTask_Wise(Order_Task);
                         Grid_Task_Wise_Count(Order_Task);
                        
                     }
                     // status wise
                     else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_Tab_Count == 0 && Err_Userid == 0)
                     {
                         Bind_Bar_Chart_Error_Status_Wise(Order_Status);
                         Grid_Status_Wise_Count(Order_Status);
                     }
                     // Error Tab wise
                     else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_Tab_Count >= 1 && Err_Userid == 0)
                     {
                         Bind_Bar_Chart_Error_Tab_Wise(Error_Tab);
                         Grid_ErrorTab_Wise_Count(Error_Tab);
                     
                     }
                     // user wise
                     else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_Tab_Count == 0 && Err_Userid > 0)
                     {
                         Bind_Bar_Error_Tab_All_Task_User_Wise(Err_Userid);
                         Grid_User_Wise_Count(Err_Userid);
                     }

                         // task and status wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_Tab_Count == 0 && Err_Userid == 0)
                     {
                         Bind_Bar_Chart_Error_Task_AND_Status_Wise(Order_Task, Order_Status);
                         Grid_Task_Status_Wise_Count(Order_Task, Order_Status);
                     }
                     // task and error tab wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_Tab_Count >= 1 && Err_Userid == 0)
                     {
                         Bind_Bar_Chart_Error_Task_AND_ErrorTab_Wise(Order_Task, Error_Tab);
                         Grid_Task_ErrorTab_Wise_Count(Order_Task, Error_Tab);
                     }
                     // task and User  wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_Tab_Count == 0 && Err_Userid > 0)
                     {
                         Bind_Bar_Chart_Error_Task_AND_User_Wise(Order_Task, Err_Userid);
                         Grid_Task_User_Wise_Count(Order_Task,Err_Userid);
                     }
                        // task and status and User wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_Tab_Count == 0 && Err_Userid > 0)
                     {
                         Bind_Bar_Chart_Error_Task_AND_Status_And_User_Wise(Order_Task, Order_Status, Err_Userid);
                         Grid_Task_Status_User_Wise_Count(Order_Task,Order_Status, Err_Userid);
                     }

                    // task and Error Tab and User wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count ==0 && Error_Tab_Count >=1 && Err_Userid > 0)
                     {
                         Bind_Bar_Chart_Error_Task_AND_ErrorTab_And_User_Wise(Order_Task, Error_Tab, Err_Userid);
                         Grid_Task_ErrorTab_User_Wise_Count(Order_Task, Error_Tab, Err_Userid);
                     }
                     // Status and error tab wise
                     else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_Tab_Count >= 1 && Err_Userid == 0)
                     {
                         Bind_Bar_Chart_ErrorTab_AND_Status_Wise(Order_Status, Error_Tab);
                         Grid_Staus_ErrorTab_Wise_Count(Order_Status, Error_Tab);
                     }

                    // Status and error tab and User wise
                     else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_Tab_Count >= 1 && Err_Userid > 0)
                     {
                         Bind_Bar_Chart_ErrorTab_AND_Staus_User_Wise(Order_Status, Error_Tab, Err_Userid);
                         Grid_Status_ErrorTab_User_Wise_Count(Order_Status, Error_Tab,Err_Userid);
                     }
                    
                     // Status and User wise
                     else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_Tab_Count == 0 && Err_Userid > 0)
                     {
                         Bind_Bar_Chart_Error_Status_AND_User_Wise(Order_Status, Err_Userid);
                         Grid_Status_User_Wise_Count(Order_Status,  Err_Userid);
                     }

                        //  error tab and User wise
                     else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_Tab_Count >= 1 && Err_Userid > 0)
                     {
                         Bind_Bar_Chart_ErrorTab_AND_User_Wise(Error_Tab, Err_Userid);
                         Grid_ErrorTab_User_Wise_Count(Error_Tab, Err_Userid);
                     }


                     // TASK , STATUS , ERROR TAB  wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count >= 0 && Error_Tab_Count >= 1 && Err_Userid == 0)
                     {
                         Bind_Bar_Chart_Error_Tab_AND_Task_AND_Status_Wise(Order_Task, Order_Status, Error_Tab);
                         Grid_Task_Status_ErrorTab_Wise_Count(Order_Task, Order_Status,Error_Tab);
                     }

                     // TASK , STATUS , ERROR TAB and USER wise
                     else if (Order_Task_Count >= 1 && Order_Status_Count >= 0 && Error_Tab_Count >= 1 && Err_Userid > 0)
                     {
                         Bind_Bar_Chart_Error_Tab_AND_Task_AND_Status_Wise(Order_Task, Order_Status, Error_Tab, Err_Userid);
                         Grid_Task_Status_ErrorTab_User_Wise_Count(Order_Task, Order_Status, Error_Tab, Err_Userid);
                     }

                    

                }
                else
                {
                    MessageBox.Show("Select Date");
                    txt_Error_Tab_From_Date.Select();
                }
            
        }


        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            Error_Type_Name = ""; Type_Name = "";
            Order_Task = ""; Order_Status = ""; Error_Tab = "";
            Order_Task_Count = 0; Order_Status_Count = 0; Error_Tab_Count = 0;
            StringBuilder sb_ErrorStatus = new StringBuilder();
            // Obtain hit information under the test point.
            ChartHitInfo hi = chartControl1.CalcHitInfo(e.X, e.Y);
            // Obtain the series point under the test point.
            SeriesPoint point = hi.SeriesPoint;
            // Check whether the series point wa-s clicked or not.
            if (point != null)
            {
                // Obtain the series point argument.
                // Er_Tab_argument = "Argument: " + point.Argument.ToString();
                ErrorTab_User_Id = int.Parse(ddl_ErrorOnUser.SelectedValue.ToString());
                Er_Tab_argument = point.Argument.ToString();
              //  Error_Type_Name = Er_Tab_argument;
                // Obtain series point values.
                ertab_errorcount = Convert.ToInt32(point.Values[0]).ToString();
                values = "Value(s): " + ertab_errorcount;
                if (ertab_errorcount.Length >= 1)
                {
                    for (int i = 0; i <= ertab_errorcount.Length - 1; i++)
                    {
                        values = values + ", " + ertab_errorcount[i].ToString();
                    }
                }
           
            }

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();

            ht.Add("@Trans", "GET_ERROR_TYPE_ID");
            ht.Add("@Error_Type_Name", Er_Tab_argument);
            dt = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht);
            if (dt.Rows.Count > 0)
            {
                Error_Type_Id = int.Parse(dt.Rows[0]["Error_Type_Id"].ToString());
            }

            Error_Type_Name = Error_Type_Id.ToString();
            Error_Tab_Page = "Error_Tab";
            StringBuilder sb = new StringBuilder();
            StringBuilder sb_1 = new StringBuilder();
            StringBuilder sb_2 = new StringBuilder();
           
            for (int error_task = 0; error_task < grd_Error_Task.Rows.Count; error_task++)
            {
                bool is_task = (bool)grd_Error_Task[0, error_task].FormattedValue;
                if (is_task == true)
                {
                    Order_Task_Count = Order_Task_Count + 1;
                    errortask_id = int.Parse(grd_Error_Task.Rows[error_task].Cells[2].Value.ToString());

                    if (Order_Task_Count == 1) { Single_Order_Task = errortask_id.ToString(); Order_Task = Single_Order_Task; sb = sb.Append(Order_Task); }

                    else { sb = sb.Append("," + errortask_id); Order_Task = sb.ToString(); Order_Task_Count++; }
                }
            }
            for (int error_Status = 0; error_Status < grd_Error_Status.Rows.Count; error_Status++)
            {
                bool is_Status = (bool)grd_Error_Status[0, error_Status].FormattedValue;
                if (is_Status == true)
                {
                    Order_Status_Count = Order_Status_Count + 1;
                    error_Status_id = int.Parse(grd_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                    if (Order_Status_Count == 1) { Order_Status = error_Status_id.ToString(); sb_1 = sb_1.Append(Order_Status); }

                    else { sb_1 = sb_1.Append("," + error_Status_id); Order_Status = sb_1.ToString(); Order_Status_Count++; }
                }
            }
        

            if (tabControl1.SelectedIndex==0)
            {
                
                Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(ertab_errorcount), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page, ErrorTab_User_Id, Order_Task, Order_Status);
                errordetails.Show();
            }
        }

        //--------------
        private void Grid__All_Order_Status_Count_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Error_Tab_Page = "Error_Tab";
                Err_Userid = int.Parse(ddl_ErrorOnUser.SelectedValue.ToString());
                Error_User_Id = Err_Userid;
                Error_Type_Name = ""; Type_Name = "";
                StringBuilder sb = new StringBuilder();
                StringBuilder sb_1 = new StringBuilder();
                StringBuilder sb_2 = new StringBuilder();
                Order_Task = ""; Order_Status = ""; Error_Tab = "";
                Order_Task_Count = 0; Order_Status_Count = 0; Error_Tab_Count = 0;
                //for (int error_task = 0; error_task < grd_Error_Task.Rows.Count; error_task++)
                //{
                //    bool is_task = (bool)grd_Error_Task[0, error_task].FormattedValue;
                //    if (is_task == true)
                //    {
                //        Order_Task_Count = Order_Task_Count + 1;
                //        errortask_id = int.Parse(grd_Error_Task.Rows[error_task].Cells[2].Value.ToString());

                //        if (Order_Task_Count == 1) { Single_Order_Task = errortask_id.ToString(); Order_Task = Single_Order_Task; sb = sb.Append(Order_Task); }

                //        else { sb = sb.Append("," + errortask_id); Order_Task = sb.ToString(); Order_Task_Count++; }
                //    }
                //}
                for (int error_Status = 0; error_Status < grd_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Status = (bool)grd_Error_Status[0, error_Status].FormattedValue;
                    if (is_Status == true)
                    {
                        Order_Status_Count = Order_Status_Count + 1;
                        error_Status_id = int.Parse(grd_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1) 
                        { 
                            Order_Status = error_Status_id.ToString();
                            sb_1 = sb_1.Append(Order_Status); }

                        else 
                        {
                            sb_1 = sb_1.Append("," + error_Status_id); 
                            Order_Status = sb_1.ToString(); 
                            Order_Status_Count++; 
                        }
                    }
                }

                for (int error_tab = 0; error_tab < grd_Error_Tab.Rows.Count; error_tab++)
                {

                    bool is_errortab = (bool)grd_Error_Tab[0, error_tab].FormattedValue;
                    if (is_errortab == true)
                    {
                        Error_Tab_Count = Error_Tab_Count + 1;
                        error_tab_id = int.Parse(grd_Error_Tab.Rows[error_tab].Cells[2].Value.ToString());
                        if (Error_Tab_Count == 1) 
                        {
                            Error_Type_Name = error_tab_id.ToString();
                            sb_2 = sb_2.Append(Error_Type_Name);
                        }

                        else 
                        { 
                            sb_2 = sb_2.Append("," + error_tab_id); 
                            Error_Type_Name = sb_2.ToString();
                            Error_Tab_Count++; 
                        }
                    }

                }

                if (e.ColumnIndex == 0)
                {
                    Order_Task = "2";
                    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                    errordetails.Show();
                }
                
                if (e.ColumnIndex == 1)
                {
                    Order_Task = "3";
                    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                    errordetails.Show();
                }
                if (e.ColumnIndex == 2)
                {
                    Order_Task = "4";
                    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                    errordetails.Show();
                }
                if (e.ColumnIndex == 3)
                {
                    Order_Task = "7";
                    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                    errordetails.Show();
                }
                if (e.ColumnIndex == 4)
                {
                    Order_Task = "23";
                    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                    errordetails.Show();
                }
                if (e.ColumnIndex == 5)
                {
                    Order_Task = "25";
                    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                    errordetails.Show();
                }

            }
        }

       

        //  Error TAB COUNT
        public static void ArrangeGrid(DataGridView Grid)
        {
            int twidth = 0;
            if (Grid.Rows.Count > 0)
            {
                twidth = (Grid.Width * Grid.Columns.Count) / 75;
                for (int i = 0; i < Grid.Columns.Count; i++)
                {
                    Grid.Columns[i].Width = 75;
                }

            }
        }

        private void Grid_All_Task_Wise_Count()
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "ALL_TASK_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                // Grid__All_Order_Status_Count.DataSource = dtlivecount;
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;

                //DataGridViewLinkColumn link1 = new DataGridViewLinkColumn();
                //Grid__All_Order_Status_Count.Columns.Add(link1);
                //link1.Name = "SEARCH";
                //link1.HeaderText = "SEARCH";
                //link1.DataPropertyName = "Search";
                //link1.Width = 80;
                //link1.DisplayIndex = 0;

                //DataGridViewLinkColumn link2 = new DataGridViewLinkColumn();
                //Grid__All_Order_Status_Count.Columns.Add(link2);
                //link2.Name = "SEARCH QC";
                //link2.HeaderText = "SEARCH QC";
                //link2.DataPropertyName = "Search QC";
                //link2.Width = 80;
                //link2.DisplayIndex = 1;

                //DataGridViewLinkColumn link3 = new DataGridViewLinkColumn();
                //Grid__All_Order_Status_Count.Columns.Add(link3);
                //link3.Name = "TYPING";
                //link3.HeaderText = "TYPING";
                //link3.DataPropertyName = "Typing";
                //link3.Width = 80;
                //link3.DisplayIndex = 2;

                //DataGridViewLinkColumn link4 = new DataGridViewLinkColumn();
                //Grid__All_Order_Status_Count.Columns.Add(link4);
                //link4.Name = "TYPING QC";
                //link4.HeaderText = "TYPING QC";
                //link4.DataPropertyName = "Typing QC";
                //link4.Width = 80;
                //link4.DisplayIndex = 3;

                //DataGridViewLinkColumn link5 = new DataGridViewLinkColumn();
                //Grid__All_Order_Status_Count.Columns.Add(link5);
                //link5.Name = "FINAL QC";
                //link5.HeaderText = "FINAL QC";
                //link5.DataPropertyName = "Final QC";
                //link5.Width = 80;
                //link5.DisplayIndex = 4;


                //DataGridViewLinkColumn link6 = new DataGridViewLinkColumn();
                //Grid__All_Order_Status_Count.Columns.Add(link6);
                //link6.Name = "EXCEPTION";
                //link6.HeaderText = "EXCEPTION";
                //link6.DataPropertyName = "Exception";
                //link6.Width = 80;
                //link6.DisplayIndex = 5;



                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;
                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;
                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;
                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;
                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;
                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;

                Grid__All_Order_Status_Count.DataSource = dtlivecount;

            }
            else
            {
                //Grid__All_Order_Status_Count.DataSource = null;
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_User_Wise_Count(int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "USER_WISE_COUNT");
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            //htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {

                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;


                Grid__All_Order_Status_Count.DataSource = dtlivecount;

            }
            else
            {
                //Grid__All_Order_Status_Count.DataSource = null;
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_Wise_Count(string Order_Task)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_WISE_COUNT");

            //htlivecount.Add("@User_Id", Err_Userid);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                // Grid__All_Order_Status_Count.DataSource = null;
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Status_Wise_Count(string Order_Status)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "STATUS_WISE_COUNT");
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_ErrorTab_Wise_Count(string ErrorTab)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TAB_WISE_COUNT");
            htlivecount.Add("@Error_Type", ErrorTab);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_User_Wise_Count(string Order_Task, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();


            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_USER_WISE_COUNT");

            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid__All_Order_Status_Count.DataSource = null;
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_Status_Wise_Count(string Order_Task, string Order_Status)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_STATUS_WISE_COUNT");
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_Status_User_Wise_Count(string Order_Task, string Order_Status, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_STATUS_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Chart_User_Id", Err_Userid);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_ErrorTab_Wise_Count(string Order_Task, string ErrorTab)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_ERROR_TAB_WISE_COUNT");
            htlivecount.Add("@Error_Type", ErrorTab);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_ErrorTab_User_Wise_Count(string Order_Task, string ErrorTab, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_ERROR_TAB_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            htlivecount.Add("@Error_Type", ErrorTab);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Chart_User_Id", Err_Userid);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_Status_ErrorTab_Wise_Count(string Order_Task, string Order_Status, string ErrorTab)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_STATUS_ERROR_TAB_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            htlivecount.Add("@Error_Type", ErrorTab);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);
            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Task_Status_ErrorTab_User_Wise_Count(string Order_Task, string Order_Status, string ErrorTab, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "TASK_STATUS_ERROR_TAB_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            htlivecount.Add("@Error_Type", ErrorTab);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Chart_User_Id", Err_Userid);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Status_User_Wise_Count(string Order_Status, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "STATUS_USER_WISE_COUNT");

            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid__All_Order_Status_Count.DataSource = null;
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Grid_Staus_ErrorTab_Wise_Count(string Order_Status, string ErrorTab)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "STATUS_ERROR_TAB_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_Type", ErrorTab);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);

        }

        private void Grid_Status_ErrorTab_User_Wise_Count(string Order_Status, string ErrorTab, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "STATUS_ERROR_TAB_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_Type", ErrorTab);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);

        }

        private void Grid_ErrorTab_User_Wise_Count(string ErrorTab, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid__All_Order_Status_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TAB_USER_WISE_COUNT");

            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_Type", ErrorTab);
            htlivecount.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid__All_Order_Status_Count.AutoGenerateColumns = false;
                Grid__All_Order_Status_Count.ColumnCount = 6;


                Grid__All_Order_Status_Count.Columns[0].Name = "SEARCH";
                Grid__All_Order_Status_Count.Columns[0].HeaderText = "Search";
                Grid__All_Order_Status_Count.Columns[0].DataPropertyName = "Search";
                Grid__All_Order_Status_Count.Columns[0].Width = 80;

                Grid__All_Order_Status_Count.Columns[1].Name = "SEARCH QC";
                Grid__All_Order_Status_Count.Columns[1].HeaderText = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].DataPropertyName = "Search QC";
                Grid__All_Order_Status_Count.Columns[1].Width = 80;

                Grid__All_Order_Status_Count.Columns[2].Name = "TYPING";
                Grid__All_Order_Status_Count.Columns[2].HeaderText = "Typing";
                Grid__All_Order_Status_Count.Columns[2].DataPropertyName = "Typing";
                Grid__All_Order_Status_Count.Columns[2].Width = 80;

                Grid__All_Order_Status_Count.Columns[3].Name = "TYPING QC ";
                Grid__All_Order_Status_Count.Columns[3].HeaderText = "Typing QC ";
                Grid__All_Order_Status_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid__All_Order_Status_Count.Columns[3].Width = 80;

                Grid__All_Order_Status_Count.Columns[4].Name = "FINAL QC";
                Grid__All_Order_Status_Count.Columns[4].HeaderText = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].DataPropertyName = "Final QC";
                Grid__All_Order_Status_Count.Columns[4].Width = 80;

                Grid__All_Order_Status_Count.Columns[5].Name = "EXCEPTION";
                Grid__All_Order_Status_Count.Columns[5].HeaderText = "Exception";
                Grid__All_Order_Status_Count.Columns[5].DataPropertyName = "Exception";
                Grid__All_Order_Status_Count.Columns[5].Width = 80;
                Grid__All_Order_Status_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid__All_Order_Status_Count.Rows.Clear();
            }
            ArrangeGrid(Grid__All_Order_Status_Count);


        }

        private void Chk_All_Order_Status_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Error_Task.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Error_Task.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["ChkAll_OrderStatus"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_Order_Status.Checked;
               
            }
        }

        private void grd_Error_Task_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Error_Task.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["ChkAll_OrderStatus"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt = 0;
                    }
                }
            }
            if (unchk_cnt == 1)
            {
                Chk_All_Order_Status.Checked = false;
            }
            else
            {
                Chk_All_Order_Status.Checked = true;
            }
        
        }

        private void chk_All_Error_Status_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Error_Status.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Error_Status.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["ChkAll_Error_Status"] as DataGridViewCheckBoxCell);
                checkBox.Value = chk_All_Error_Status.Checked;

            }
        }

        private void grd_Error_Status_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Error_Status.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["ChkAll_Error_Status"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt = 0;
                    }
                }
            }
            if (unchk_cnt == 1)
            {
                chk_All_Error_Status.Checked = false;
            }
            else
            {
                chk_All_Error_Status.Checked = true;
            }
        }

        private void Chk_All_Error_Tab_Click(object sender, EventArgs e)
        {
                //Necessary to end the edit mode of the Cell.
            grd_Error_Tab.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Error_Tab.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["ChkAll_Error_Type"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_Error_Tab.Checked;

            }
        }

        private void grd_Error_Tab_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Error_Tab.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["ChkAll_Error_Type"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt = 0;
                    }
                }
            }
            if (unchk_cnt == 1)
            {
                Chk_All_Error_Tab.Checked = false;
            }
            else
            {
                Chk_All_Error_Tab.Checked = true;
            }
        }

      
       

        ///--------------------- Error_Desc_Entry DESC---------
        ///


        private void Grid_Bind_Error_Desc_Error_Task()
        {
            Hashtable ht_getError_Task = new Hashtable();
            DataTable dt_getError_Task = new DataTable();

            ht_getError_Task.Add("@Trans", "BIND_ERROR_TASK");
            dt_getError_Task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getError_Task);
            if (dt_getError_Task.Rows.Count > 0)
            {
                grd_ErrorDesc_Order_Status.Rows.Clear();
                for (int i = 0; i < dt_getError_Task.Rows.Count; i++)
                {
                    grd_ErrorDesc_Order_Status.Rows.Add();
                    grd_ErrorDesc_Order_Status.Rows[i].Cells[1].Value = dt_getError_Task.Rows[i]["Order_Status"].ToString();
                    grd_ErrorDesc_Order_Status.Rows[i].Cells[2].Value = dt_getError_Task.Rows[i]["Order_Status_Id"].ToString();
                }
            }
            else
            {
                grd_ErrorDesc_Order_Status.Rows.Clear();

            }
        }

        private void Grid_Bind_Error_Desc_Error_Status()
        {
            Hashtable ht_getError_Status = new Hashtable();
            DataTable dt_getError_Status = new DataTable();

            ht_getError_Status.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_getError_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getError_Status);
            if (dt_getError_Status.Rows.Count > 0)
            {
                grd_Error_Desc_Error_Status.Rows.Clear();
                for (int i = 0; i < dt_getError_Status.Rows.Count; i++)
                {
                    grd_Error_Desc_Error_Status.Rows.Add();
                    grd_Error_Desc_Error_Status.Rows[i].Cells[1].Value = dt_getError_Status.Rows[i]["Error_Status"].ToString();
                    grd_Error_Desc_Error_Status.Rows[i].Cells[2].Value = dt_getError_Status.Rows[i]["Error_Status_Id"].ToString();
                }
            }
            else
            {
                grd_Error_Desc_Error_Status.Rows.Clear();

            }
        }

        private void Grid_Bind_Error_Description()
        {
            Hashtable ht_getError_Field = new Hashtable();
            DataTable dt_getError_Field = new DataTable();

            ht_getError_Field.Add("@Trans", "SELECT_ERROR_FIELD");
            dt_getError_Field = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getError_Field);
            if (dt_getError_Field.Rows.Count > 0)
            {
                grd_Error_Field.Rows.Clear();
                for (int i = 0; i < dt_getError_Field.Rows.Count; i++)
                {
                    grd_Error_Field.Rows.Add();
                    grd_Error_Field.Rows[i].Cells[1].Value = dt_getError_Field.Rows[i]["Error_description"].ToString();
                  //  grd_Error_Field.Rows[i].Cells[2].Value = dt_getError_Field.Rows[i]["Error_description_Id"].ToString();
                }
            }
            else
            {
                grd_Error_Field.Rows.Clear();

            }
        }

        private void Chk_All_Error_Task_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_ErrorDesc_Order_Status.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_ErrorDesc_Order_Status.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["ChkAll_Errror_Task"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_Error_Task.Checked;

            }
        }

        private void grd_ErrorDesc_Order_Status_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_1 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_ErrorDesc_Order_Status.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["ChkAll_Errror_Task"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_1 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_1 = 0;
                    }
                }
            }
            if (unchk_cnt_1 == 1)
            {
                Chk_All_Error_Task.Checked = false;
            }
            else
            {
                Chk_All_Error_Task.Checked = true;
            }
        }

        private void Chk_All_ErrorStatus_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Error_Desc_Error_Status.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Error_Desc_Error_Status.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["ChkAll_Errror_Status"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_ErrorStatus.Checked;

            }
        }

        private void grd_Error_Desc_Error_Status_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_2 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Error_Desc_Error_Status.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["ChkAll_Errror_Status"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_2 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_2 = 0;
                    }
                }
            }
            if (unchk_cnt_2 == 1)
            {
                Chk_All_ErrorStatus.Checked = false;
            }
            else
            {
                Chk_All_ErrorStatus.Checked = true;
            }
        }

        private void ChkAll_Error_Field_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Error_Field.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Error_Field.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_Error_Desc"] as DataGridViewCheckBoxCell);
                checkBox.Value = ChkAll_Error_Field.Checked;

            }
        }

        private void grd_Error_Field_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_3 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Error_Field.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_Error_Desc"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_3 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_3 = 0;
                    }
                }
            }
            if (unchk_cnt_3 == 1)
            {
                ChkAll_Error_Field.Checked = false;
            }
            else
            {
                ChkAll_Error_Field.Checked = true;
            }
        }

        private void btn_Error_Desc_Export_Click(object sender, EventArgs e)
        {
            Export_Error_Desc();
        }

        private void Export_Error_Desc()
        {
            load_Progressbar.Start_progres();
            DevExpress.XtraPrinting.PrintingSystem ps1 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl_1 = new DevExpress.XtraPrintingLinks.CompositeLink(ps1);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart1 = new  DevExpress.XtraPrinting.PrintableComponentLink();

            chartControl2.Visible = true;
            pclChart1.Component = chartControl2;

            cl_1.PaperKind = System.Drawing.Printing.PaperKind.ESheet;
            cl_1.Landscape = true;
            cl_1.Margins.Right = 40;
            cl_1.Margins.Left = 40;

            cl_1.Links.AddRange(new object[] { pclChart1 });
            cl_1.ShowPreviewDialog();


        }

        private void btn_Error_Desc_Clear_Click(object sender, EventArgs e)
        {
            Chk_All_Error_Task.Checked = false;
            Chk_All_ErrorStatus.Checked = false;
            ChkAll_Error_Field.Checked=false;

            Grid_Bind_Error_Description();
            Grid_Bind_Error_Desc_Error_Task();
            Grid_Bind_Error_Desc_Error_Status();

            ddl_Err_desc_ErrorOnUser.SelectedIndex = 0;
            Grid_ErrorField_All_Task_Wise_Count();
            Bind_Bar_Error_Desc_All_Task();
        }

        // ALL Task Wise
        private void Bind_Bar_Error_Desc_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errdesc = new Hashtable();
                DataTable dt_Errdesc = new DataTable();

                ht_Errdesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATEWISE_ALL_TASK_WISE");
                ht_Errdesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Errdesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                dt_Errdesc = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errdesc);
                //dt = dt_Select;

                chartControl2.DataSource = dt_Errdesc;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";

                Bind_Line_Error_Desc_All_Task();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_Error_Desc_All_Task()
        {
            try
            {
                // Line CHart
                Hashtable htSel_Line_ErrorDesc = new Hashtable();
                DataTable dtSel_Line_ErrorDesc = new DataTable();

                htSel_Line_ErrorDesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATEWISE_ALL_TASK_WISE");
                htSel_Line_ErrorDesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                htSel_Line_ErrorDesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);

                dtSel_Line_ErrorDesc = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htSel_Line_ErrorDesc);

                // dt_LineChart_Error_Desc = dtSel_Line_ErrorDesc;

                chartControl2.DataSource = dtSel_Line_ErrorDesc;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //   Task Wise
        private void Bind_Bar_Error_Desc_All_Task_Wise(string Order_Task)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDesc = new Hashtable();
                DataTable dt_Select_ErrDesc = new DataTable();
               
                ht_Select_ErrDesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_WISE");
                ht_Select_ErrDesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDesc.Add("@Error_Task", Order_Task);
                dt_Select_ErrDesc = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Select_ErrDesc);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDesc;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";





                Bind_Line_Error_Desc_All_Task_Wise(Order_Task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_Error_Desc_All_Task_Wise(string Order_Task)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sele = new Hashtable();
                DataTable dt_Sele = new DataTable();
                ht_Sele.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_WISE");
                ht_Sele.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sele.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sele.Add("@Error_Task", Order_Task);
                dt_Sele = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sele);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sele;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";

                //  Bind_Line_Error_Desc_All_Task();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Error Status Wise
        private void Bind_Bar_ErrorDesc_Status_Wise(string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDe = new Hashtable();
                DataTable dt_Select_ErrDe = new DataTable();

                ht_Select_ErrDe.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_STATUS_WISE");
                ht_Select_ErrDe.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDe.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDe.Add("@Error_Status", Error_Status);
                dt_Select_ErrDe = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Select_ErrDe);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDe;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Status_Wise(Error_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Status_Wise(string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel = new Hashtable();
                DataTable dt_Sel = new DataTable();
                ht_Sel.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_STATUS_WISE");
                ht_Sel.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel.Add("@Error_Status", Error_Status);
                dt_Sel = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sel;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Error Desc Filed Wise
        private void Bind_Bar_Error_Field_Wise(string Error_Field)
        {
            try
            {
                chartControl2.DataSource = null;

                //Bar Chart
                Hashtable ht_Select_ErrDe = new Hashtable();
                DataTable dt_Select_ErrDe = new DataTable();

                ht_Select_ErrDe.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_WISE");
                ht_Select_ErrDe.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDe.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDe.Add("@Error_description",Error_Field);
                dt_Select_ErrDe = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Select_ErrDe);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDe;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_Error_Field_Wise(Error_Field);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_Error_Field_Wise(string Error_Field)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel = new Hashtable();
                DataTable dt_Sel = new DataTable();
                ht_Sel.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_WISE");
                ht_Sel.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel.Add("@Error_description", Error_Field);
                dt_Sel = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sel;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // BARHCART Field And Status Wise

        private void Bind_Bar_ErrorDesc_Field_AND_Status_Wise(string Error_Field,string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDe = new Hashtable();
                DataTable dt_Select_ErrDe = new DataTable();

                ht_Select_ErrDe.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_STATUS_WISE");
                ht_Select_ErrDe.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDe.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDe.Add("@Error_description", Error_Field);
                ht_Select_ErrDe.Add("@Error_Status", Error_Status);
                dt_Select_ErrDe = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Select_ErrDe);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDe;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_Status_Wise(Error_Field,Error_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_Status_Wise(string Error_Field, string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel = new Hashtable();
                DataTable dt_Sel = new DataTable();
                ht_Sel.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_STATUS_WISE");
                ht_Sel.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel.Add("@Error_description", Error_Field);
                ht_Sel.Add("@Error_Status", Error_Status);
                dt_Sel = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sel;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Field And TASK Wise
        private void Bind_Bar_ErrorDesc_Field_AND_Task_Wise(string Error_task,string Error_Field)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_ErrD = new Hashtable();
                DataTable dt_Sel_ErrD = new DataTable();

                ht_Sel_ErrD.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_WISE");
                ht_Sel_ErrD.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_ErrD.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_ErrD.Add("@Error_description",Error_Field );
                ht_Sel_ErrD.Add("@Error_Task", Error_task);
                dt_Sel_ErrD = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel_ErrD);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Sel_ErrD;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_Task_Wise(Error_task,Error_Field);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_Task_Wise(string Error_task, string Error_Field)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_Line = new Hashtable();
                DataTable dt_Sel_Line = new DataTable();
                ht_Sel_Line.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_WISE");
                ht_Sel_Line.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_Line.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_Line.Add("@Error_description",Error_Field );
                ht_Sel_Line.Add("@Error_Task", Error_task);
                dt_Sel_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel_Line);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sel_Line;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //  TASK AND Status  Wise
        private void Bind_Bar_ErrorDesc_Task_AND_Status_Wise(string Order_Task, string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_ErrD = new Hashtable();
                DataTable dt_Sel_ErrD = new DataTable();

                ht_Sel_ErrD.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_AND_STATUS_WISE");
                ht_Sel_ErrD.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_ErrD.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_ErrD.Add("@Error_Status", Order_Status);
                ht_Sel_ErrD.Add("@Error_Task", Order_Task);
                dt_Sel_ErrD = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel_ErrD);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Sel_ErrD;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Task_AND_Status_Wise(Order_Task, Order_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Task_AND_Status_Wise(string Order_Task, string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_Line = new Hashtable();
                DataTable dt_Sel_Line = new DataTable();
                ht_Sel_Line.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_AND_STATUS_WISE");
                ht_Sel_Line.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_Line.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_Line.Add("@Error_Status", Order_Status);
                ht_Sel_Line.Add("@Error_Task", Order_Task);
                dt_Sel_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel_Line);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sel_Line;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //  Field And TASK AND Status Wise

        private void Bind_Bar_ErrorDesc_Field_AND_Status_AND_Task_Wise(string Order_Task,string Error_Status,string Error_description)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_ErrD = new Hashtable();
                DataTable dt_Sel_ErrD = new DataTable();

                ht_Sel_ErrD.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_AND_STATUS_WISE");
                ht_Sel_ErrD.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_ErrD.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_ErrD.Add("@Error_Task", Order_Task);
                ht_Sel_ErrD.Add("@Error_Status",Error_Status);
                ht_Sel_ErrD.Add("@Error_description", Error_description);
                dt_Sel_ErrD = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sel_ErrD);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Sel_ErrD;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_Status_AND_Task_Wise(Order_Task,Error_Status,Error_description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_Status_AND_Task_Wise(string Order_Task, string Error_Status, string Error_description)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Se = new Hashtable();
                DataTable dt_Se = new DataTable();
                ht_Se.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_AND_STATUS_WISE");
                ht_Se.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Se.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Se.Add("@Error_description", Error_description);
                ht_Se.Add("@Error_Status", Error_Status);
                ht_Se.Add("@Error_Task", Order_Task);
                dt_Se = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Se);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Se;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        //   User Wise  ---------------17-07-2018------------------

        private void Bind_Bar_Error_Desc_User_Wise(int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDesc = new Hashtable();
                DataTable dt_Select_ErrDesc = new DataTable();

                ht_Select_ErrDesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATEWISE_USER_WISE");
                ht_Select_ErrDesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDesc.Add("@User_Id", ErrorUser_Id);
               // ht_Select_ErrDesc.Add("@Work_Type", 1);
                dt_Select_ErrDesc = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Select_ErrDesc);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDesc;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";





                Bind_Line_Error_Desc_User_Wise(ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_Error_Desc_User_Wise(int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sele = new Hashtable();
                DataTable dt_Sele = new DataTable();
                ht_Sele.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATEWISE_USER_WISE");
                ht_Sele.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sele.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sele.Add("@User_Id", ErrorUser_Id);
                dt_Sele = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sele);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sele;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";

                //  Bind_Line_Error_Desc_All_Task();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //   Task and User Wise
        private void Bind_Bar_Error_Desc_Task_And_User_Wise(string Order_Task, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDesc = new Hashtable();
                DataTable dt_Select_ErrDesc = new DataTable();

                ht_Select_ErrDesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_TASK_AND_USER_WISE");
                ht_Select_ErrDesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDesc.Add("@Error_Task", Order_Task);
                ht_Select_ErrDesc.Add("@User_Id", ErrorUser_Id);
                dt_Select_ErrDesc = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Select_ErrDesc);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDesc;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";

                Bind_Line_Error_Desc_Task_And_User_Wise(Order_Task, ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_Error_Desc_Task_And_User_Wise(string Order_Task, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sele_task_User = new Hashtable();
                DataTable dt_Sele_task_User = new DataTable();
                ht_Sele_task_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_TASK_AND_USER_WISE");
                ht_Sele_task_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sele_task_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sele_task_User.Add("@Error_Task", Order_Task);
                ht_Sele_task_User.Add("@User_Id", ErrorUser_Id);
                dt_Sele_task_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Sele_task_User);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Sele_task_User;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //  Status and User  Wise
        private void Bind_Bar_ErrorDesc_Status_And_User_Wise(string Error_Status, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDes_Status_User = new Hashtable();
                DataTable dt_Select_ErrDes_Status_User = new DataTable();

                ht_Select_ErrDes_Status_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_STATUS_AND_USER_WISE");
                ht_Select_ErrDes_Status_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDes_Status_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDes_Status_User.Add("@Error_Status", Error_Status);
                ht_Select_ErrDes_Status_User.Add("@User_Id", ErrorUser_Id);
                dt_Select_ErrDes_Status_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Select_ErrDes_Status_User);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDes_Status_User;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Status_And_User_Wise(Error_Status, ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Status_And_User_Wise(string Error_Status, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Status_User = new Hashtable();
                DataTable dt_Line_Status_User = new DataTable();

                ht_Line_Status_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_STATUS_AND_USER_WISE");
                ht_Line_Status_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Line_Status_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Line_Status_User.Add("@Error_Status", Error_Status);
                ht_Line_Status_User.Add("@User_Id", ErrorUser_Id);
                dt_Line_Status_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Status_User);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Line_Status_User;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //  Field And USER Wise

        private void Bind_Bar_ErrorDesc_Field_AND_User_Wise(string Error_Field, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrDesc_Field_User = new Hashtable();
                DataTable dt_ErrDesc_Field_User = new DataTable();

                ht_ErrDesc_Field_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_FIELD_AND_USER_WISE");
                ht_ErrDesc_Field_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_ErrDesc_Field_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_ErrDesc_Field_User.Add("@Error_description", Error_Field);
                ht_ErrDesc_Field_User.Add("@User_Id", ErrorUser_Id);
                dt_ErrDesc_Field_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrDesc_Field_User);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_ErrDesc_Field_User;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_User_Wise(Error_Field, ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_User_Wise(string Error_Field, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_ErrDesc_Field_User = new Hashtable();
                DataTable dt_Line_ErrDesc_Field_User = new DataTable();
                ht_Line_ErrDesc_Field_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_FIELD_AND_USER_WISE");
                ht_Line_ErrDesc_Field_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Line_ErrDesc_Field_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Line_ErrDesc_Field_User.Add("@Error_description", Error_Field);
                ht_Line_ErrDesc_Field_User.Add("@User_Id", ErrorUser_Id);
                dt_Line_ErrDesc_Field_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_ErrDesc_Field_User);
                //dt = dt_Sele;

                chartControl2.DataSource = dt_Line_ErrDesc_Field_User;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //  TASK AND Status AND USER Wise
        private void Bind_Bar_ErrorDesc_Task_Status_User_Wise(string Order_Task, string Error_Status, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_TaskStatusUser = new Hashtable();
                DataTable dt_TaskStatusUser = new DataTable();

                ht_TaskStatusUser.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_TASK_STATUS_USER_WISE");
                ht_TaskStatusUser.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_TaskStatusUser.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_TaskStatusUser.Add("@Error_Status", Order_Status);
                ht_TaskStatusUser.Add("@Error_Task", Order_Task);
                ht_TaskStatusUser.Add("@User_Id", ErrorUser_Id);
                dt_TaskStatusUser = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_TaskStatusUser);
               
                chartControl2.DataSource = dt_TaskStatusUser;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Task_Status_User_Wise(Order_Task, Order_Status, ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Task_Status_User_Wise(string Order_Task, string Error_Status, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_TaskStatusUser = new Hashtable();
                DataTable dt_Line_TaskStatusUser = new DataTable();
                ht_Line_TaskStatusUser.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_TASK_STATUS_USER_WISE");
                ht_Line_TaskStatusUser.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Line_TaskStatusUser.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Line_TaskStatusUser.Add("@Error_Status", Order_Status);
                ht_Line_TaskStatusUser.Add("@Error_Task", Order_Task);
                ht_Line_TaskStatusUser.Add("@User_Id", ErrorUser_Id);
                dt_Line_TaskStatusUser = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_TaskStatusUser);

                chartControl2.DataSource = dt_Line_TaskStatusUser;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Field And TASK and User Wise
        private void Bind_Bar_ErrorDesc_Field_Task_User_Wise(string Order_Task, string Error_Field, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Field_Task_User = new Hashtable();
                DataTable dt_Field_Task_User = new DataTable();

                ht_Field_Task_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_TASK_USER_WISE");
                ht_Field_Task_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Field_Task_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Field_Task_User.Add("@Error_description", Error_Field);
                ht_Field_Task_User.Add("@Error_Task", Order_Task);
                ht_Field_Task_User.Add("@User_Id", ErrorUser_Id);
                dt_Field_Task_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Field_Task_User);
               
                chartControl2.DataSource = dt_Field_Task_User;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_Task_User_Wise(Order_Task, Error_Field, ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_Task_User_Wise(string Order_Task, string Error_Field, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Field_Task_User = new Hashtable();
                DataTable dt_Line_Field_Task_User = new DataTable();
                ht_Line_Field_Task_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_TASK_USER_WISE");
                ht_Line_Field_Task_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Line_Field_Task_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Line_Field_Task_User.Add("@Error_description", Error_Field);
                ht_Line_Field_Task_User.Add("@Error_Task", Order_Task);
                ht_Line_Field_Task_User.Add("@User_Id", ErrorUser_Id);
                dt_Line_Field_Task_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Field_Task_User);
               
                chartControl2.DataSource = dt_Line_Field_Task_User;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //  Field And Status AND USER Wise

        private void Bind_Bar_ErrorDesc_Field_AND_Status_User_Wise(string Error_Status, string Error_Field, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Bar_Field_Status_User = new Hashtable();
                DataTable dt_Bar_Field_Status_User = new DataTable();

                ht_Bar_Field_Status_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_FIELD_STATUS_USER_WISE");
                ht_Bar_Field_Status_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Bar_Field_Status_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Bar_Field_Status_User.Add("@Error_description", Error_Field);
                ht_Bar_Field_Status_User.Add("@Error_Status", Error_Status);
                ht_Bar_Field_Status_User.Add("@User_Id", ErrorUser_Id);
                dt_Bar_Field_Status_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Bar_Field_Status_User);
             
                chartControl2.DataSource = dt_Bar_Field_Status_User;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_Status_User_Wise(Error_Field, Error_Status, ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_Status_User_Wise(string Error_Status, string Error_Field, int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Field_Status_User = new Hashtable();
                DataTable dt_Line_Field_Status_User = new DataTable();
                ht_Line_Field_Status_User.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_FIELD_STATUS_USER_WISE");
                ht_Line_Field_Status_User.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Line_Field_Status_User.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Line_Field_Status_User.Add("@Error_description", Error_Field);
                ht_Line_Field_Status_User.Add("@Error_Status", Error_Status);
                ht_Line_Field_Status_User.Add("@User_Id", ErrorUser_Id);
                dt_Line_Field_Status_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Field_Status_User);
              
                chartControl2.DataSource = dt_Line_Field_Status_User;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //  TASK , Status , Field And USER Wise

        private void Bind_Bar_ErrorDesc_Field_Status_Task_User_Wise(string Order_Task,string Error_Status,string Error_description,int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_All= new Hashtable();
                DataTable dt_All = new DataTable();

                ht_All.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_FIELD_TASK_STATUS_USER_WISE");
                ht_All.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_All.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_All.Add("@Error_Task", Order_Task);
                ht_All.Add("@Error_Status", Error_Status);
                ht_All.Add("@Error_description", Error_description);
                ht_All.Add("@User_Id", ErrorUser_Id);
                dt_All = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_All);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_All;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_Status_Task_User_Wise(Order_Task, Error_Status, Error_description, ErrorUser_Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_Status_Task_User_Wise(string Order_Task,string Error_Status,string Error_description,int ErrorUser_Id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_All = new Hashtable();
                DataTable dt_Line_All = new DataTable();

                ht_Line_All.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_AND_FIELD_TASK_STATUS_USER_WISE");
                ht_Line_All.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Line_All.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Line_All.Add("@Error_description", Error_description);
                ht_Line_All.Add("@Error_Status", Error_Status);
                ht_Line_All.Add("@Error_Task", Order_Task);
                ht_Line_All.Add("@User_Id", ErrorUser_Id);
                dt_Line_All = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_All);
               
                chartControl2.DataSource = dt_Line_All;

                chartControl2.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["%"].ArgumentDataMember = "Error_description";
                chartControl2.Series["%"].ValueDataMembers[0] = "Error_Desc_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btn_Error_Desc_Submit_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            Error_Desc_User_Id = int.Parse(ddl_Err_desc_ErrorOnUser.SelectedValue.ToString());
            if (txt_Error_Desc_From_Date.Text != "" && txt_Error_Desc_To_Date.Text != "")
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                Order_Task_Count = 0; Order_Status_Count = 0; Error_Desc_Count = 0;
                for (int error_task = 0; error_task < grd_ErrorDesc_Order_Status.Rows.Count; error_task++)
                {
                    bool is_task = (bool)grd_ErrorDesc_Order_Status[0, error_task].FormattedValue;
                    if (is_task == true)
                    {
                        Order_Task_Count = Order_Task_Count + 1;
                        errortask_id = int.Parse(grd_ErrorDesc_Order_Status.Rows[error_task].Cells[2].Value.ToString());
                        if (Order_Task_Count == 1)
                        {
                            //Single_Order_Task = errortask_id.ToString();

                            Single_Order_Task = errortask_id.ToString();
                            Order_Task = Single_Order_Task;
                            sb = sb.Append(Order_Task);
                        }
                        else
                        {
                            sb = sb.Append("," + errortask_id);
                            Order_Task = sb.ToString();
                            Order_Task_Count++;
                        }
                    }
                }
                for (int error_Status = 0; error_Status < grd_Error_Desc_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Order_Status = (bool)grd_Error_Desc_Error_Status[0, error_Status].FormattedValue;
                    if (is_Order_Status == true)
                    {
                        Order_Status_Count = Order_Status_Count + 1;
                        error_Status_id = int.Parse(grd_Error_Desc_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1)
                        {
                            //Single_Order_Status = error_Status_id.ToString();
                            Order_Status = error_Status_id.ToString();
                            sb1 = sb1.Append(Order_Status);
                        }
                        else
                        {
                            //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                            sb1 = sb1.Append("," + error_Status_id);
                            Order_Status = sb1.ToString();
                            Order_Status_Count++;
                        }
                    }
                }
                for (int error_Desc = 0; error_Desc < grd_Error_Field.Rows.Count; error_Desc++)
                {
                    bool is_error_desc = (bool)grd_Error_Field[0, error_Desc].FormattedValue;
                    if (is_error_desc == true)
                    {
                        Error_Desc_Count = Error_Desc_Count + 1;
                        //error_Desc_id = int.Parse(grd_Error_Field.Rows[error_Desc].Cells[2].Value.ToString());
                         string errorDesc = grd_Error_Field.Rows[error_Desc].Cells[1].Value.ToString();
                        if (Error_Desc_Count == 1)
                        {
                            
                            //Error_Desc = error_Desc_id.ToString();
                            Error_Desc = errorDesc.ToString();
                            sb2 = sb2.Append(Error_Desc);
                        }
                        else
                        {
                            
                           // sb2 = sb2.Append("," + error_Desc_id);
                            sb2 = sb2.Append("," + errorDesc);
                            Error_Desc = sb2.ToString();
                            Error_Desc_Count++;
                        }
                    }
                }

                // Date wise 
                if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_Desc_Count == 0 && Error_Desc_User_Id==0)
                {
                    Bind_Bar_Error_Desc_All_Task();
                    Grid_ErrorField_All_Task_Wise_Count();
                }
                // task wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_Desc_Count == 0 && Error_Desc_User_Id == 0)
                {
                    Bind_Bar_Error_Desc_All_Task_Wise(Order_Task);
                    Grid_Error_Task_Wise_Count(Order_Task);
                  
                }
                // status wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_Desc_Count == 0 && Error_Desc_User_Id == 0)
                {
                    Bind_Bar_ErrorDesc_Status_Wise(Order_Status);
                    Grid_Error_Status_Wise_Count(Order_Status);
                }
                // Error FILED wise
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_Desc_Count >= 1 && Error_Desc_User_Id == 0)
                {
                    Bind_Bar_Error_Field_Wise(Error_Desc);
                    Grid_Error_Field_Wise_Count(Error_Desc);
                }
                    // task and status wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_Desc_Count == 0 && Error_Desc_User_Id == 0)
                {
                    Bind_Bar_ErrorDesc_Task_AND_Status_Wise(Order_Task, Order_Status);
                    Grid_ErrorTask_AND_ErrorStatus_Wise_Count(Order_Task, Order_Status);
                }
                // task and error Field wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_Desc_Count >= 1 && Error_Desc_User_Id == 0)
                {
                    Bind_Bar_ErrorDesc_Field_AND_Task_Wise(Order_Task, Error_Desc);
                    Grid_ErrorTask_AND_ErrorField_Wise_Count(Order_Task, Error_Desc);
                }
                // Status and error Field wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_Desc_Count >= 1 && Error_Desc_User_Id == 0)
                {
                    Bind_Bar_ErrorDesc_Field_AND_Status_Wise(Error_Desc ,Order_Status);
                    Grid_ErrorStaus_AND_ErrorField_Wise_Count(Order_Status, Error_Desc);
                }

                // TASK , STATUS , ERROR Field wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 0 && Error_Desc_Count >= 1 && Error_Desc_User_Id == 0)
                {
                    Bind_Bar_ErrorDesc_Field_AND_Status_AND_Task_Wise(Order_Task, Order_Status, Error_Desc);
                    Grid_ErrorTask_AND_ErrorStatus_AND_ErrorField_Wise_Count(Order_Task,Order_Status, Error_Desc);
                }

                 // User wise
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_Desc_Count == 0 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_Error_Desc_User_Wise(Error_Desc_User_Id);
                    Grid_Error_User_Wise_Count(Error_Desc_User_Id);
                }
                // User and Task wise
                else if (Order_Task_Count >=1 && Order_Status_Count == 0 && Error_Desc_Count == 0 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_Error_Desc_Task_And_User_Wise(Order_Task, Error_Desc_User_Id);
                    Grid_ErrorTask_AND_ErrorUser_Wise_Count(Order_Task, Error_Desc_User_Id);
                }
                // User and Status wise
                else if (Order_Task_Count ==0 && Order_Status_Count >=1 && Error_Desc_Count == 0 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_ErrorDesc_Status_And_User_Wise(Order_Status, Error_Desc_User_Id);
                    Grid_ErrorStatus_AND_ErrorUser_Wise_Count(Order_Status, Error_Desc_User_Id);
                }

                 // User and Field wise
                else if (Order_Task_Count == 0 && Order_Status_Count ==0 && Error_Desc_Count >=1 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_ErrorDesc_Field_AND_User_Wise(Error_Desc, Error_Desc_User_Id);
                    Grid_ErrorFiled_AND_ErrorUser_Wise_Count(Error_Desc, Error_Desc_User_Id);
                }

                // Task ,Field and User wise
                else if (Order_Task_Count >=1 && Order_Status_Count == 0 && Error_Desc_Count >= 1 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_ErrorDesc_Field_Task_User_Wise(Order_Task, Error_Desc, Error_Desc_User_Id);
                    Grid_ErrorTask_ErrorField_ErrorUser_Wise_Count(Order_Task, Error_Desc, Error_Desc_User_Id);
                }

                  //Field, Status and User wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_Desc_Count >=1 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_ErrorDesc_Field_AND_Status_User_Wise(Order_Status, Error_Desc, Error_Desc_User_Id);
                    Grid_ErrorStatus_AND_ErrorField_User_Wise_Count(Error_Desc,Order_Status, Error_Desc_User_Id);
                }

                // TASK , Status and User wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >=1 && Error_Desc_Count ==0 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_ErrorDesc_Task_Status_User_Wise(Order_Task, Order_Status, Error_Desc_User_Id);
                    Grid_ErrorTask_AND_ErrorStatus_ErrorUser_Wise_Count(Order_Task, Order_Status, Error_Desc_User_Id);
                }


                 // TASK , STATUS ,  Field and User wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_Desc_Count >= 1 && Error_Desc_User_Id > 0)
                {
                    Bind_Bar_ErrorDesc_Field_Status_Task_User_Wise(Order_Task, Order_Status, Error_Desc, Error_Desc_User_Id);
                    Grid_ErrorTask_AND_Error_Status_AND_ErrorField_User_Wise_Count(Order_Task, Order_Status, Error_Desc, Error_Desc_User_Id);
                }

            }
            else
            {
                MessageBox.Show("Select Date");
                txt_Error_Tab_From_Date.Select();
            }
        }

        private void Grid_Error_field_All_Task_Count_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Error_Tab_Page = "Error_Description";
                Err_Userid = int.Parse(ddl_Err_desc_ErrorOnUser.SelectedValue.ToString());
                Error_User_Id = Err_Userid;
                Error_Type_Name = "";
                Type_Name = "";
                StringBuilder sb = new StringBuilder();
                StringBuilder sb_1 = new StringBuilder();
                StringBuilder sb_2 = new StringBuilder();
                Order_Task = ""; Order_Status = ""; Error_Desc = "";
                Order_Task_Count = 0; Order_Status_Count = 0; Error_Desc_Count = 0;

                for (int error_Status = 0; error_Status < grd_Error_Desc_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Status = (bool)grd_Error_Desc_Error_Status[0, error_Status].FormattedValue;
                    if (is_Status == true)
                    {
                        Order_Status_Count = Order_Status_Count + 1;
                        error_Status_id = int.Parse(grd_Error_Desc_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1) { Order_Status = error_Status_id.ToString(); sb_1 = sb_1.Append(Order_Status); }

                        else { sb_1 = sb_1.Append("," + error_Status_id); Order_Status = sb_1.ToString(); Order_Status_Count++; }
                    }
                }
                for (int error_Desc = 0; error_Desc < grd_Error_Field.Rows.Count; error_Desc++)
                {
                    bool is_error_desc = (bool)grd_Error_Field[0, error_Desc].FormattedValue;
                    if (is_error_desc == true)
                    {
                        Error_Desc_Count = Error_Desc_Count + 1; 
                       // error_Desc_id = int.Parse(grd_Error_Field.Rows[error_Desc].Cells[2].Value.ToString());
                        string errorDesc = grd_Error_Field.Rows[error_Desc].Cells[1].Value.ToString();
                        if (Error_Desc_Count == 1) 
                        { 
                           // Error_Type_Name = error_Desc_id.ToString(); 
                            Error_Type_Name = errorDesc.ToString();
                            sb_2 = sb_2.Append(Error_Type_Name);
                        }

                        else 
                        { 
                           // sb_2 = sb_2.Append("," + error_Desc_id);
                            sb_2 = sb_2.Append("," + errorDesc);
                            Error_Type_Name = sb_2.ToString();
                            Error_Desc_Count++; 
                        }
                    }
                }

               
                if (e.ColumnIndex == 0)
                {
                    if (Grid_Error_field_All_Task_Count.Rows[0].Cells[0].Value.ToString() != "0")
                    {
                        Order_Task = "2";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }

                }

                if (e.ColumnIndex == 1)
                {
                    if (Grid_Error_field_All_Task_Count.Rows[0].Cells[1].Value.ToString() != "0")
                    {
                        Order_Task = "3";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (Grid_Error_field_All_Task_Count.Rows[0].Cells[2].Value.ToString() != "0")
                    {
                        Order_Task = "4";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    if (Grid_Error_field_All_Task_Count.Rows[0].Cells[3].Value.ToString() != "0")
                    {
                        Order_Task = "7";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (Grid_Error_field_All_Task_Count.Rows[0].Cells[4].Value.ToString() != "0")
                    {
                        Order_Task = "23";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    if (Grid_Error_field_All_Task_Count.Rows[0].Cells[5].Value.ToString() != "0")
                    {
                        Order_Task = "24";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }

            }
        }

        private void chartControl2_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl2.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;
            Order_Task = ""; Order_Status = ""; Error_Type_Name = "";
            if (point != null)
            {

                //Er_Desc_argument = "Argument: " + point.Argument.ToString();
                Error_User_Id = int.Parse(ddl_Err_desc_ErrorOnUser.SelectedValue.ToString());
                Er_Desc_argument = point.Argument.ToString();
                Error_Type_Name = Er_Desc_argument;
                erDesc_errorcount = Convert.ToInt32(point.Values[0]).ToString();
                values = "Value(s): " + erDesc_errorcount;
                if (erDesc_errorcount.Length >= 1)
                {
                    for (int i = 0; i <= erDesc_errorcount.Length - 1; i++)
                    {
                        values = values + ", " + erDesc_errorcount[i].ToString();
                    }
                }

            }

            Error_Tab_Page = "Error_Description";


            //Hashtable ht = new Hashtable();
            //DataTable dt = new DataTable();

            //ht.Add("@Trans", "GET_ERROR_FIELD_ID");
            //ht.Add("@Error_Type_Name", Er_Desc_argument);
            //dt = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht);
            //if (dt.Rows.Count > 0)
            //{
            //    Error_description_Id = int.Parse(dt.Rows[0]["Error_description_Id"].ToString());
            //    Error_Type_Id = int.Parse(dt.Rows[0]["Error_Type_Id"].ToString());
            //}

            //Error_Type_Name = Error_description_Id.ToString();
           

            StringBuilder sb = new StringBuilder();
            StringBuilder sb_1 = new StringBuilder();
            StringBuilder sb_2 = new StringBuilder();

            for (int error_task = 0; error_task < grd_ErrorDesc_Order_Status.Rows.Count; error_task++)
            {
                bool is_task = (bool)grd_ErrorDesc_Order_Status[0, error_task].FormattedValue;
                if (is_task == true)
                {
                    Order_Task_Count = Order_Task_Count + 1;
                    errortask_id = int.Parse(grd_ErrorDesc_Order_Status.Rows[error_task].Cells[2].Value.ToString());

                    if (Order_Task_Count == 1) { Single_Order_Task = errortask_id.ToString(); Order_Task = Single_Order_Task; sb = sb.Append(Order_Task); }

                    else { sb = sb.Append("," + errortask_id); Order_Task = sb.ToString(); Order_Task_Count++; }
                }
            }
            for (int error_Status = 0; error_Status < grd_Error_Desc_Error_Status.Rows.Count; error_Status++)
            {
                bool is_Status = (bool)grd_Error_Desc_Error_Status[0, error_Status].FormattedValue;
                if (is_Status == true)
                {
                    Order_Status_Count = Order_Status_Count + 1;
                    error_Status_id = int.Parse(grd_Error_Desc_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                    if (Order_Status_Count == 1) { Order_Status = error_Status_id.ToString(); sb_1 = sb_1.Append(Order_Status); }

                    else { sb_1 = sb_1.Append("," + error_Status_id); Order_Status = sb_1.ToString(); Order_Status_Count++; }
                }
            }
        

            if (tabControl1.SelectedIndex == 1)
            {

                Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(erDesc_errorcount), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id,Order_Task,Order_Status);
                errordetails.Show();
            }
        }


        //------------------------- Error  On USer--------------------

        private void Grid_BindError_Task()
        {
            Hashtable ht_getErrorTask = new Hashtable();
            DataTable dt_getErrorTask = new DataTable();

            ht_getErrorTask.Add("@Trans", "BIND_ERROR_TASK");
            dt_getErrorTask = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getErrorTask);
            if (dt_getErrorTask.Rows.Count > 0)
            {
                grd__OnUser_Errortask.Rows.Clear();
                for (int i = 0; i < dt_getErrorTask.Rows.Count; i++)
                {
                    grd__OnUser_Errortask.Rows.Add();
                    grd__OnUser_Errortask.Rows[i].Cells[1].Value = dt_getErrorTask.Rows[i]["Order_Status"].ToString();
                    grd__OnUser_Errortask.Rows[i].Cells[2].Value = dt_getErrorTask.Rows[i]["Order_Status_Id"].ToString();
                }
            }
            else
            {
                grd__OnUser_Errortask.Rows.Clear();

            }
        }

        private void Grid_BindError_Status()
        {
            Hashtable ht_getErrorStatus = new Hashtable();
            DataTable dt_getErrorStatus = new DataTable();

            ht_getErrorStatus.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_getErrorStatus = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getErrorStatus);
            if (dt_getErrorStatus.Rows.Count > 0)
            {
                grd_OnUser_Error_Status.Rows.Clear();
                for (int i = 0; i < dt_getErrorStatus.Rows.Count; i++)
                {
                    grd_OnUser_Error_Status.Rows.Add();
                    grd_OnUser_Error_Status.Rows[i].Cells[1].Value = dt_getErrorStatus.Rows[i]["Error_Status"].ToString();
                    grd_OnUser_Error_Status.Rows[i].Cells[2].Value = dt_getErrorStatus.Rows[i]["Error_Status_Id"].ToString();
                }
            }
            else
            {
                grd_OnUser_Error_Status.Rows.Clear();

            }
        }

        private void Grid_Bind_Error_On_User()
        {
            Hashtable ht_getErrorOnUser = new Hashtable();
            DataTable dt_getErrorOnUser = new DataTable();

            ht_getErrorOnUser.Add("@Trans", "SELECT");
            dt_getErrorOnUser = dataaccess.ExecuteSP("Sp_User", ht_getErrorOnUser);
            if (dt_getErrorOnUser.Rows.Count > 0)
            {
                grd_Error_On_User.Rows.Clear();
                for (int i = 0; i < dt_getErrorOnUser.Rows.Count; i++)
                {
                    grd_Error_On_User.Rows.Add();
                    grd_Error_On_User.Rows[i].Cells[1].Value = dt_getErrorOnUser.Rows[i]["User_Name"].ToString();
                    grd_Error_On_User.Rows[i].Cells[2].Value = dt_getErrorOnUser.Rows[i]["User_id"].ToString();
                }
            }
            else
            {
                grd_Error_On_User.Rows.Clear();

            }
        }

        // BAR CHART Error On User All Task Wise
        private void Bind_Bar_Chart_Error_On_User_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUser_Barchart = new Hashtable();
                DataTable dt_ErrOnUser_Barchart = new DataTable();

                ht_ErrOnUser_Barchart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_ALL_TASK");
                ht_ErrOnUser_Barchart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUser_Barchart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                dt_ErrOnUser_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnUser_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUser_Barchart;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_User_All_Task();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_User_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Erruser_Barchart = new Hashtable();
                DataTable dt_Erruser_Barchart = new DataTable();
                ht_Erruser_Barchart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_ALL_TASK");
                ht_Erruser_Barchart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Erruser_Barchart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                dt_Erruser_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Erruser_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_Erruser_Barchart;

                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART  Task wise
        private void Bind_Bar_Chart_Error_On_User_Task_Wise(string Error_Task)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUser = new Hashtable();
                DataTable dt_ErrOnUser = new DataTable();
                ht_ErrOnUser.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_TASK_WISE");
                ht_ErrOnUser.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUser.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOnUser.Add("@Error_Task", Error_Task);
                dt_ErrOnUser = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnUser);
                // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUser;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_User_Task_Wise(Error_Task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_User_Task_Wise(string Error_Task)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUser_Line = new Hashtable();
                DataTable dt_ErrOnUser_Line = new DataTable();
                ht_ErrOnUser_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_TASK_WISE");
                ht_ErrOnUser_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUser_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOnUser_Line.Add("@Error_Task", Error_Task);
                dt_ErrOnUser_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnUser_Line);
                // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUser_Line;
                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART user wise
        private void Bind_Bar_Chart_Error_On_User_Wise(string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUse_Barchart = new Hashtable();
                DataTable dt_ErrOnUse_Barchart = new DataTable();

                ht_ErrOnUse_Barchart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_USER_WISE");
                ht_ErrOnUse_Barchart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUse_Barchart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                // ht_ErrOnUse_Barchart.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                ht_ErrOnUse_Barchart.Add("@Error_User", Error_OnUser);
                dt_ErrOnUse_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnUse_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUse_Barchart;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_User_Wise(Error_OnUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_User_Wise(string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Erruse_Linechart = new Hashtable();
                DataTable dt_Erruse_Linechart = new DataTable();
                ht_Erruse_Linechart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_USER_WISE");
                ht_Erruse_Linechart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Erruse_Linechart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_Erruse_Linechart.Add("@Error_User", Error_OnUser);
                dt_Erruse_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Erruse_Linechart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_Erruse_Linechart;

                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //BAR CHART user and task wise
        private void Bind_Bar_Chart_Error_On_User_AND_Task_Wise(string Error_Task, string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUs_Barchart = new Hashtable();
                DataTable dt_ErrOnUs_Barchart = new DataTable();

                ht_ErrOnUs_Barchart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_USER_AND_TASK_WISE");
                ht_ErrOnUs_Barchart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUs_Barchart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOnUs_Barchart.Add("@Error_Task", Error_Task);
                ht_ErrOnUs_Barchart.Add("@Error_User", Error_OnUser);
                dt_ErrOnUs_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnUs_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUs_Barchart;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_User_AND_Task_Wise(Error_Task,Error_OnUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_User_AND_Task_Wise(string Error_Task, string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Erru_Linechart = new Hashtable();
                DataTable dt_Erru_Linechart = new DataTable();
                ht_Erru_Linechart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_USER_AND_TASK_WISE");
                ht_Erru_Linechart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Erru_Linechart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_Erru_Linechart.Add("@Error_Task", Error_Task);
                ht_Erru_Linechart.Add("@Error_User", Error_OnUser);
                dt_Erru_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Erru_Linechart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_Erru_Linechart;

                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //BAR CHART Status wise
        private void Bind_Bar_Chart_Error_On_Status_Wise(string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errsta_Barchart = new Hashtable();
                DataTable dt_Errsta_Barchart = new DataTable();

                ht_Errsta_Barchart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_WISE");
                ht_Errsta_Barchart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Errsta_Barchart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_Errsta_Barchart.Add("@Error_Status", Error_Status);
                dt_Errsta_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errsta_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_Errsta_Barchart;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_Wise(Error_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_Wise(string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrSta_Linechart = new Hashtable();
                DataTable dt_ErrSta_Linechart = new DataTable();
                ht_ErrSta_Linechart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_WISE");
                ht_ErrSta_Linechart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrSta_Linechart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrSta_Linechart.Add("@Error_Status", Error_Status);
                dt_ErrSta_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrSta_Linechart);
                //dt_Errror_Tab = dt_ErrStatus_Linechart;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrSta_Linechart;

                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART STATUS AND TASK WISE
        private void Bind_Bar_Chart_Error_On_Status_AND_Task_Wise(string Error_Task,string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOn_Bar = new Hashtable();
                DataTable dt_ErrOn_Bar = new DataTable();

                ht_ErrOn_Bar.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_WISE");
                ht_ErrOn_Bar.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOn_Bar.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOn_Bar.Add("@Error_Status", Error_Status);
                ht_ErrOn_Bar.Add("@Error_Task",Error_Task);
                dt_ErrOn_Bar = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_Bar);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOn_Bar;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_AND_Task_Wise(Error_Task,Error_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_AND_Task_Wise(string Error_Task, string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrS_Line = new Hashtable();
                DataTable dt_ErrS_Line = new DataTable();
                ht_ErrS_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_WISE");
                ht_ErrS_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrS_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrS_Line.Add("@Error_Status", Error_Status);
                ht_ErrS_Line.Add("@Error_Task",Error_Task);
                dt_ErrS_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrS_Line);
                //dt_Errror_Tab = dt_ErrStatus_Linechart;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrS_Line;

                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // Bar Charts Status AND USer Wise

        private void Bind_Bar_Chart_Error_On_Status_AND_User_Wise(string Error_Status, string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrStat_Bar = new Hashtable();
                DataTable dt_ErrStat_Bar = new DataTable();

                ht_ErrStat_Bar.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_USER_WISE");
                ht_ErrStat_Bar.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrStat_Bar.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrStat_Bar.Add("@Error_Status", Error_Status);
                ht_ErrStat_Bar.Add("@Error_User", Error_OnUser);
                dt_ErrStat_Bar = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStat_Bar);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrStat_Bar;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_AND_User_Wise(Error_Status,Error_OnUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_AND_User_Wise(string Error_Status, string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrSt_Line = new Hashtable();
                DataTable dt_ErrSt_Line = new DataTable();
                ht_ErrSt_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_USER_WISE");
                ht_ErrSt_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrSt_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrSt_Line.Add("@Error_Status", Error_Status);
                ht_ErrSt_Line.Add("@Error_User", Error_OnUser);
                dt_ErrSt_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrSt_Line);
                //dt_Errror_Tab = dt_ErrStatus_Linechart;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrSt_Line;

                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Bar Charts Status AND TASK AND USer Wise

        private void Bind_Bar_Chart_Error_On_User_AND_Task_AND_Status_User_Wise(string Error_Task, string Error_Status, string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrStat_Bar = new Hashtable();
                DataTable dt_ErrStat_Bar = new DataTable();

                ht_ErrStat_Bar.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_AND_USER_WISE");
                ht_ErrStat_Bar.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrStat_Bar.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrStat_Bar.Add("@Error_Task", Error_Task);
                ht_ErrStat_Bar.Add("@Error_Status", Error_Status);
                ht_ErrStat_Bar.Add("@Error_User", Error_OnUser);
                dt_ErrStat_Bar = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStat_Bar);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrStat_Bar;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_AND_Task_AND_User_Wise(Error_Task,Error_Status,Error_OnUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_AND_Task_AND_User_Wise(string Error_Task, string Error_Status, string Error_OnUser)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Err_Line = new Hashtable();
                DataTable dt_Err_Line = new DataTable();
                ht_Err_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_AND_USER_WISE");
                ht_Err_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Err_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);

                ht_Err_Line.Add("@Error_Task", Error_Task);
                ht_Err_Line.Add("@Error_Status", Error_Status);
                ht_Err_Line.Add("@Error_User", Error_OnUser);
                dt_Err_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Err_Line);
                //dt_Errror_Tab = dt_ErrStatus_Linechart;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_Err_Line;

                chartControl3.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["%"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] = "Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        private void btn_OnUser_Clear_Click(object sender, EventArgs e)
        {
            Chk_All_OnUser_Error_Task.Checked = false;
            Chk_All_OnUser_Error_Status.Checked = false;
            Chk_All_Error_OnUser.Checked = false;

            Grid_BindError_Task();
            Grid_BindError_Status();
            Grid_Bind_Error_On_User();

            GridErrorOnUser_All_Task_Wise_Count();
            Bind_Bar_Chart_Error_On_User_All_Task();

        }
        // Export Error User Chart
        private void Export_Error_On_User()
        {
            load_Progressbar.Start_progres();
            DevExpress.XtraPrinting.PrintingSystem ps2 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl_2 = new DevExpress.XtraPrintingLinks.CompositeLink(ps2);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart2 = new DevExpress.XtraPrinting.PrintableComponentLink();

            chartControl3.Visible = true;
            pclChart2.Component = chartControl3;

            cl_2.PaperKind = System.Drawing.Printing.PaperKind.ESheet;
            cl_2.Landscape = true;
            cl_2.Margins.Right = 40;
            cl_2.Margins.Left = 40;

            cl_2.Links.AddRange(new object[] { pclChart2 });
            cl_2.ShowPreviewDialog();


        }

        private void btn_ErrorOnUser_Export_Click(object sender, EventArgs e)
        {
            Export_Error_On_User();
        }

        private void Chk_All_OnUser_Error_Task_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd__OnUser_Errortask.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd__OnUser_Errortask.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_Error_Task"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_OnUser_Error_Task.Checked;

            }
        }

        private void grd__OnUser_Errortask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchkcnt1 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd__OnUser_Errortask.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_Error_Task"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchkcnt1 = 1;
                        break;
                    }
                    else
                    {
                        unchkcnt1 = 0;
                    }
                }
            }
            if (unchkcnt1 == 1)
            {
                Chk_All_OnUser_Error_Task.Checked = false;
            }
            else
            {
                Chk_All_OnUser_Error_Task.Checked = true;
            }
        }

        private void Chk_All_OnUser_Error_Status_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_OnUser_Error_Status.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_OnUser_Error_Status.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_Error_Status"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_OnUser_Error_Status.Checked;

            }
        }

        private void grd_OnUser_Error_Status_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchkcnt2 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_OnUser_Error_Status.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_Error_Status"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchkcnt2 = 1;
                        break;
                    }
                    else
                    {
                        unchkcnt2 = 0;
                    }
                }
            }
            if (unchkcnt2 == 1)
            {
                Chk_All_OnUser_Error_Status.Checked = false;
            }
            else
            {
                Chk_All_OnUser_Error_Status.Checked = true;
            }
        }

        private void Chk_All_Error_OnUser_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Error_On_User.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Error_On_User.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_All_ErrorOn_User"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_Error_OnUser.Checked;

            }
        }

        private void grd_Error_On_User_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchkcnt3 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Error_On_User.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_All_ErrorOn_User"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchkcnt3 = 1;
                        break;
                    }
                    else
                    {
                        unchkcnt3 = 0;
                    }
                }
            }
            if (unchkcnt3 == 1)
            {
                Chk_All_Error_OnUser.Checked = false;
            }
            else
            {
                Chk_All_Error_OnUser.Checked = true;
            }
        }

       
        private void btn_OnUser_Submit_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (txt_ErrorOnUser_From_Date.Text != "" && txt_ErrorOnUser_To_Date.Text != "")
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                Order_Task_Count = 0; Order_Status_Count = 0; Error_OnUser_Count = 0;
                for (int error_task = 0; error_task < grd__OnUser_Errortask.Rows.Count; error_task++)
                {

                    bool is_task = (bool)grd__OnUser_Errortask[0, error_task].FormattedValue;
                    if (is_task == true)
                    {
                        Order_Task_Count = error_task + 1;
                        errortask_id = int.Parse(grd__OnUser_Errortask.Rows[error_task].Cells[2].Value.ToString());

                        if (Order_Task_Count == 1)
                        {
                            //Single_Order_Task = errortask_id.ToString();

                            Single_Order_Task = errortask_id.ToString();
                            Order_Task = Single_Order_Task;
                            sb = sb.Append(Order_Task);
                        }
                        else
                        {
                            sb = sb.Append("," + errortask_id);
                            Order_Task = sb.ToString();
                            Order_Task_Count++;
                        }
                    }
                }
                for (int error_Status = 0; error_Status < grd_OnUser_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Order_Status = (bool)grd_OnUser_Error_Status[0, error_Status].FormattedValue;
                    if (is_Order_Status == true)
                    {
                        Order_Status_Count = error_Status + 1;
                        error_Status_id = int.Parse(grd_OnUser_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1)
                        {
                            Order_Status = error_Status_id.ToString();
                            sb1 = sb1.Append(Order_Status);
                        }
                        else
                        {
                            //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                            sb1 = sb1.Append("," + error_Status_id);
                            Order_Status = sb1.ToString();
                            Order_Status_Count++;
                        }
                    }
                }
                for (int error_OnUser = 0; error_OnUser < grd_Error_On_User.Rows.Count; error_OnUser++)
                {

                    bool is_error_Onuser = (bool)grd_Error_On_User[0, error_OnUser].FormattedValue;
                    if (is_error_Onuser == true)
                    {
                        Error_OnUser_Count = error_OnUser + 1;
                        Error_On_User_id = int.Parse(grd_Error_On_User.Rows[error_OnUser].Cells[2].Value.ToString());
                        if (Error_OnUser_Count == 1)
                        {
                            Error_On_User = Error_On_User_id.ToString();
                            sb2 = sb2.Append(Error_On_User);
                        }
                        else
                        {
                            sb2 = sb2.Append("," + Error_On_User_id);
                            Error_On_User = sb2.ToString();
                            Error_OnUser_Count++;
                        }
                    }

                }


                if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnUser_Count == 0)
                {
                    Bind_Bar_Chart_Error_On_User_All_Task();
                    GridErrorOnUser_All_Task_Wise_Count();
                }

                // task wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnUser_Count == 0)
                {
                    Bind_Bar_Chart_Error_On_User_Task_Wise(Order_Task);
                    GridErrorOnUser_ErrorTask_Wise_Count(Order_Task);
                }
                // status wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnUser_Count == 0)
                {
                    Bind_Bar_Chart_Error_On_Status_Wise(Order_Status);
                    GridErrorOnUser_ErrorStatus_Wise_Count(Order_Status);
                }
                // Error_On_User wise
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnUser_Count >= 1)
                {
                    Bind_Bar_Chart_Error_On_User_Wise(Error_On_User);
                    GridErrorOnUser_User_Wise_Count(Error_On_User);
                }

                    // task and status wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnUser_Count == 0)
                {
                    Bind_Bar_Chart_Error_On_Status_AND_Task_Wise(Order_Task, Order_Status);
                    GridErrorOnUser_ErrorTask_AND_ErrorStatus_Wise_Count(Order_Task, Order_Status);
                }
                // task and  Error_On_User wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnUser_Count >= 1)
                {
                    Bind_Bar_Chart_Error_On_User_AND_Task_Wise(Order_Task, Error_On_User);
                    GridErrorOnUser_ErrorTask_AND_ErrorUser_Wise_Count(Order_Task, Error_On_User);
                }
                // Status and Error_On_User wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnUser_Count >= 1)
                {
                    Bind_Bar_Chart_Error_On_Status_AND_User_Wise(Order_Status, Error_On_User);
                    GridErrorOnUser_ErrorStatus_AND_Error_On_User_Wise_Count(Order_Status, Error_On_User);
                }

                // TASK , STATUS ,Error_On_User  wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 0 && Error_OnUser_Count >= 1)
                {
                    Bind_Bar_Chart_Error_On_User_AND_Task_AND_Status_User_Wise(Order_Task, Order_Status, Error_On_User);
                    GridErrorOnUser_ErrorTask_AND_ErrorStatus_ErrorUser_Wise_Count(Order_Task, Order_Status, Error_On_User);
                }

            }
            else
            {
                MessageBox.Show("Select Date");
                txt_Error_Tab_From_Date.Select();
            }
        }

        private void chartControl3_MouseClick_1(object sender, MouseEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();

            ChartHitInfo hi_123 = chartControl3.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi_123.SeriesPoint;
            Error_User_Id = 0;
            Order_Task = ""; Order_Status = ""; Error_Tab = ""; Error_Type_Name = "";
            Order_Task_Count = 0; Order_Status_Count = 0; Error_OnUser_Count = 0; Error_On_UserName_argument = "";
            if (point != null)
            {
                //Error_User_Id = 0;
                //Error_Type_Name = "";
                //Error_On_UserName_argument = "Argument: " + point.Argument.ToString();

                Error_On_UserName_argument = point.Argument.ToString();
                // Error_Type_Name = Error_On_UserName_argument;


                //  ErrorTypeName = Error_On_UserName_argument;
                eruser_errorcount = Convert.ToInt32(point.Values[0]).ToString();
                values = "Value(s): " + eruser_errorcount;
                if (eruser_errorcount.Length >= 1)
                {
                    for (int i = 0; i <= eruser_errorcount.Length - 1; i++)
                    {
                        values = values + ", " + eruser_errorcount[i].ToString();
                    }
                }
            }

            Error_Tab_Page = "Error_On_User";

            Hashtable ht_Err = new Hashtable();
            DataTable dt_Err = new DataTable();
            ht_Err.Add("@Trans", "GET_USERID_BY_ERROR_TYPE_NAME");
            //ht_Err.Add("@Error_Type_Name", Error_Type_Name);
            ht_Err.Add("@Error_Type_Name", Error_On_UserName_argument);
            dt_Err = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Err);
            if (dt_Err.Rows.Count > 0)
            {
                // Error_Type_Name = dt.Rows[0]["User_Name"].ToString();
                Error_User_Id = int.Parse(dt_Err.Rows[0]["User_Id"].ToString());
            }
            Error_Type_Name = "";   /// when chart click user name(Error_Type_Name) will pass empty and only will pass user id(Error_User_Id) single or multiple

            /// 
            for (int error_task = 0; error_task < grd__OnUser_Errortask.Rows.Count; error_task++)
            {

                bool is_task = (bool)grd__OnUser_Errortask[0, error_task].FormattedValue;
                if (is_task == true)
                {
                    Order_Task_Count = error_task + 1;
                    errortask_id = int.Parse(grd__OnUser_Errortask.Rows[error_task].Cells[2].Value.ToString());

                    if (Order_Task_Count == 1)
                    {
                        //Single_Order_Task = errortask_id.ToString();

                        Single_Order_Task = errortask_id.ToString();
                        Order_Task = Single_Order_Task;
                        sb = sb.Append(Order_Task);
                    }
                    else
                    {
                        sb = sb.Append("," + errortask_id);
                        Order_Task = sb.ToString();
                        Order_Task_Count++;
                    }
                }
            }
            for (int error_Status = 0; error_Status < grd_OnUser_Error_Status.Rows.Count; error_Status++)
            {
                bool is_Order_Status = (bool)grd_OnUser_Error_Status[0, error_Status].FormattedValue;
                if (is_Order_Status == true)
                {
                    Order_Status_Count = error_Status + 1;
                    error_Status_id = int.Parse(grd_OnUser_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                    if (Order_Status_Count == 1)
                    {
                        Order_Status = error_Status_id.ToString();
                        sb1 = sb1.Append(Order_Status);
                    }
                    else
                    {
                        //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                        sb1 = sb1.Append("," + error_Status_id);
                        Order_Status = sb1.ToString();
                        Order_Status_Count++;
                    }
                }
            }

            if (tabControl1.SelectedIndex == 2)
            {

                Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(eruser_errorcount), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                errordetails.Show();
            }
        }

        private void chartControl3_MouseClick(object sender, MouseEventArgs e)
        {
            //StringBuilder sb = new StringBuilder();
            //StringBuilder sb1 = new StringBuilder();  

            //ChartHitInfo hi_123 = chartControl3.CalcHitInfo(e.X, e.Y);
            //SeriesPoint point = hi_123.SeriesPoint;
            //Error_User_Id = 0;
            //Order_Task = ""; Order_Status = ""; Error_Tab = ""; Error_Type_Name = "";
            //Order_Task_Count = 0; Order_Status_Count = 0; Error_OnUser_Count = 0; Error_On_UserName_argument = "";
            //if (point != null)
            //{
            //    //Error_User_Id = 0;
            //    //Error_Type_Name = "";
            //    //Error_On_UserName_argument = "Argument: " + point.Argument.ToString();

            //    Error_On_UserName_argument = point.Argument.ToString();
            //   // Error_Type_Name = Error_On_UserName_argument;


            //  //  ErrorTypeName = Error_On_UserName_argument;
            //    eruser_errorcount = Convert.ToInt32(point.Values[0]).ToString();
            //    values = "Value(s): " + eruser_errorcount;
            //    if (eruser_errorcount.Length >= 1)
            //    {
            //        for (int i = 0; i <= eruser_errorcount.Length - 1; i++)
            //        {
            //            values = values + ", " + eruser_errorcount[i].ToString();
            //        }
            //    }
            //}

            //Error_Tab_Page = "Error_On_User";
           
            //Hashtable ht_Err = new Hashtable();
            //DataTable dt_Err = new DataTable();
            //ht_Err.Add("@Trans", "GET_USERID_BY_ERROR_TYPE_NAME");
            ////ht_Err.Add("@Error_Type_Name", Error_Type_Name);
            //ht_Err.Add("@Error_Type_Name", Error_On_UserName_argument);
            //dt_Err = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Err);
            //if (dt_Err.Rows.Count > 0)
            //{
            //   // Error_Type_Name = dt.Rows[0]["User_Name"].ToString();
            //    Error_User_Id = int.Parse(dt_Err.Rows[0]["User_Id"].ToString());
            //}
            //Error_Type_Name = "";   /// when chart click user name(Error_Type_Name) will pass empty and only will pass user id(Error_User_Id) single or multiple
           
            //                  /// 
            //for (int error_task = 0; error_task < grd__OnUser_Errortask.Rows.Count; error_task++)
            //{

            //    bool is_task = (bool)grd__OnUser_Errortask[0, error_task].FormattedValue;
            //    if (is_task == true)
            //    {
            //        Order_Task_Count = error_task + 1;
            //        errortask_id = int.Parse(grd__OnUser_Errortask.Rows[error_task].Cells[2].Value.ToString());

            //        if (Order_Task_Count == 1)
            //        {
            //            //Single_Order_Task = errortask_id.ToString();

            //            Single_Order_Task = errortask_id.ToString();
            //            Order_Task = Single_Order_Task;
            //            sb = sb.Append(Order_Task);
            //        }
            //        else
            //        {
            //            sb = sb.Append("," + errortask_id);
            //            Order_Task = sb.ToString();
            //            Order_Task_Count++;
            //        }
            //    }
            //}
            //for (int error_Status = 0; error_Status < grd_OnUser_Error_Status.Rows.Count; error_Status++)
            //{
            //    bool is_Order_Status = (bool)grd_OnUser_Error_Status[0, error_Status].FormattedValue;
            //    if (is_Order_Status == true)
            //    {
            //        Order_Status_Count = error_Status + 1;
            //        error_Status_id = int.Parse(grd_OnUser_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
            //        if (Order_Status_Count == 1)
            //        {
            //            Order_Status = error_Status_id.ToString();
            //            sb1 = sb1.Append(Order_Status);
            //        }
            //        else
            //        {
            //            //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
            //            sb1 = sb1.Append("," + error_Status_id);
            //            Order_Status = sb1.ToString();
            //            Order_Status_Count++;
            //        }
            //    }
            //}




            //if (tabControl1.SelectedIndex == 2)
            //{

            //    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(eruser_errorcount), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id,Order_Task,Order_Status);
            //    errordetails.Show();
            //}
        }


        private void Grid_ErrorOnUser_All_Task_Count_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Error_Tab_Page = "Error_On_User";
                //Err_Userid = int.Parse(ddl_ErrorOnUser.SelectedValue.ToString());
                //Error_User_Id = Err_Userid;
                //Error_Type_Name = "";
               
                Order_Task = ""; Order_Status = ""; Error_Tab = "";
                StringBuilder sb_task = new StringBuilder();
                StringBuilder sb_status = new StringBuilder();
                StringBuilder sb_user = new StringBuilder();
                Order_Task_Count = 0; Order_Status_Count = 0; Error_OnUser_Count = 0;
                for (int error_task = 0; error_task < grd__OnUser_Errortask.Rows.Count; error_task++)
                {

                    bool is_task = (bool)grd__OnUser_Errortask[0, error_task].FormattedValue;
                    if (is_task == true)
                    {
                        Order_Task_Count = error_task + 1;
                        errortask_id = int.Parse(grd__OnUser_Errortask.Rows[error_task].Cells[2].Value.ToString());
                        if (Order_Task_Count == 1) { Single_Order_Task = errortask_id.ToString();Order_Task = Single_Order_Task;   sb_task = sb_task.Append(Order_Task);}
                    
                        else { sb_task = sb_task.Append("," + errortask_id); Order_Task = sb_task.ToString(); Order_Task_Count++; }
                    }
                }
                for (int error_Status = 0; error_Status < grd_OnUser_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Order_Status = (bool)grd_OnUser_Error_Status[0, error_Status].FormattedValue;
                    if (is_Order_Status == true)
                    {
                        Order_Status_Count = error_Status + 1;
                        error_Status_id = int.Parse(grd_OnUser_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1) { Order_Status = error_Status_id.ToString(); sb_status = sb_status.Append(Order_Status); }
                     
                        else { sb_status = sb_status.Append("," + error_Status_id); Order_Status = sb_status.ToString(); Order_Status_Count++; }
                    }
                }
                for (int error_OnUser = 0; error_OnUser < grd_Error_On_User.Rows.Count; error_OnUser++)
                {
                    bool is_error_Onuser = (bool)grd_Error_On_User[0, error_OnUser].FormattedValue;
                    if (is_error_Onuser == true)
                    {
                        Error_OnUser_Count = error_OnUser + 1;
                        Error_On_User_id = int.Parse(grd_Error_On_User.Rows[error_OnUser].Cells[2].Value.ToString());
                        if (Error_OnUser_Count == 1)
                        {
                            Error_On_User = Error_On_User_id.ToString();
                            sb_user = sb_user.Append(Error_On_User);
                        }
                        else
                        {
                            sb_user = sb_user.Append("," + Error_On_User_id);
                            Error_On_User = sb_user.ToString();
                            Error_OnUser_Count++;
                        }

                    }
                }
                Error_Type_Name = Error_On_User;  /// singl or multple user id only
                Error_User_Id = 0;     // when grid click integer user id is zero will pass
               

                if (e.ColumnIndex == 0)
                {

                    if (Grid_ErrorOnUser_All_Task_Count.Rows[0].Cells[0].Value.ToString() != "0")
                    {
                        Order_Task = "2";

                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    if (Grid_ErrorOnUser_All_Task_Count.Rows[0].Cells[1].Value.ToString() != "0")
                    {
                        Order_Task = "3";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (Grid_ErrorOnUser_All_Task_Count.Rows[0].Cells[2].Value.ToString() != "0")
                    {
                        Order_Task = "4";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    if (Grid_ErrorOnUser_All_Task_Count.Rows[0].Cells[3].Value.ToString() != "0")
                    {
                    Order_Task = "7";
                    Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                    errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (Grid_ErrorOnUser_All_Task_Count.Rows[0].Cells[4].Value.ToString() != "0")
                    {
                        Order_Task = "23";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    if (Grid_ErrorOnUser_All_Task_Count.Rows[0].Cells[5].Value.ToString() != "0")
                    {
                        Order_Task = "24";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }


            }
        }



        //----------- Client WIse

        private void Bind_Grid_Error_Task()
        {
            Hashtable ht_getErr_Task = new Hashtable();
            DataTable dt_getErr_Task = new DataTable();

            ht_getErr_Task.Add("@Trans", "BIND_ERROR_TASK");
            dt_getErr_Task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getErr_Task);
            if (dt_getErr_Task.Rows.Count > 0)
            {
                grd_Client_Error_task.Rows.Clear();
                for (int i = 0; i < dt_getErr_Task.Rows.Count; i++)
                {
                    grd_Client_Error_task.Rows.Add();
                    grd_Client_Error_task.Rows[i].Cells[1].Value = dt_getErr_Task.Rows[i]["Order_Status"].ToString();
                    grd_Client_Error_task.Rows[i].Cells[2].Value = dt_getErr_Task.Rows[i]["Order_Status_Id"].ToString();
                }
            }
            else
            {
                grd_Client_Error_task.Rows.Clear();

            }
        }

        private void Bind_Grid_Error_Status()
        {
            Hashtable ht_getErr_Status = new Hashtable();
            DataTable dt_getErr_Status = new DataTable();

            ht_getErr_Status.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_getErr_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getErr_Status);
            if (dt_getErr_Status.Rows.Count > 0)
            {
                grd_Client_Error_Status.Rows.Clear();
                for (int i = 0; i < dt_getErr_Status.Rows.Count; i++)
                {
                    grd_Client_Error_Status.Rows.Add();
                    grd_Client_Error_Status.Rows[i].Cells[1].Value = dt_getErr_Status.Rows[i]["Error_Status"].ToString();
                    grd_Client_Error_Status.Rows[i].Cells[2].Value = dt_getErr_Status.Rows[i]["Error_Status_Id"].ToString();
                }
            }
            else
            {
                grd_Client_Error_Status.Rows.Clear();

            }
        }

        private void Bind_Grid_Client()
        {
            Hashtable ht_getError_Client = new Hashtable();
            DataTable dt_getError_Client = new DataTable();

            ht_getError_Client.Add("@Trans", "SELECT_CLIENT");
            dt_getError_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getError_Client);
            if (dt_getError_Client.Rows.Count > 0)
            {
                grd_Client.Rows.Clear();
                for (int i = 0; i < dt_getError_Client.Rows.Count; i++)
                {
                    grd_Client.Rows.Add();
                    grd_Client.Rows[i].Cells[1].Value = dt_getError_Client.Rows[i]["Client_Id"].ToString();
                    grd_Client.Rows[i].Cells[2].Value = dt_getError_Client.Rows[i]["Client_Name"].ToString();
                }
            }
            else
            {
                grd_Client.Rows.Clear();

            }

          
        }

        private void Bind_Grid_SubClient()
        {
            Hashtable ht_getError_SubClient = new Hashtable();
            DataTable dt_getError_SubClient = new DataTable();

            ht_getError_SubClient.Add("@Trans", "SELECTCLIENTWISE");
            ht_getError_SubClient.Add("@Client_Id", clientId);
            dt_getError_SubClient = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getError_SubClient);
            if (dt_getError_SubClient.Rows.Count > 0)
            {
                grd_SubClient.Rows.Clear();
                for (int i = 0; i < dt_getError_SubClient.Rows.Count; i++)
                {
                    grd_SubClient.Rows.Add();
                    grd_SubClient.Rows[i].Cells[1].Value = dt_getError_SubClient.Rows[i]["Subprocess_Id"].ToString();
                    grd_SubClient.Rows[i].Cells[2].Value = dt_getError_SubClient.Rows[i]["Sub_ProcessName"].ToString();
                }
            }
            else
            {
                grd_SubClient.Rows.Clear();

            }


        }

        private void Bind_SubClient_ClientWise()
        {
            for (int client = 0; client < grd_Client.Rows.Count; client++)
            {
                bool isclient = (bool)grd_Client[0, client].FormattedValue;
                if (isclient)
                {

                    Hashtable ht_SubClient = new Hashtable();
                    DataTable dt_SubClient = new DataTable();
                    ht_SubClient.Add("@Trans", "SELECTCLIENTWISE");
                    ht_SubClient.Add("@Client_Id", int.Parse(grd_Client.Rows[client].Cells[1].Value.ToString()));
                    dt_SubClient = dataaccess.ExecuteSP("Sp_Error_DashBoard", ht_SubClient);
                    if (dt_SubClient.Rows.Count > 0)
                    {
                        grd_SubClient.Rows.Clear();
                        for (int i = 0; i < dt_SubClient.Rows.Count; i++)
                        {
                            grd_SubClient.Rows.Add();
                            grd_SubClient.Rows[i].Cells[1].Value = dt_SubClient.Rows[i]["Subprocess_Id"].ToString();
                            grd_SubClient.Rows[i].Cells[2].Value = dt_SubClient.Rows[i]["Sub_ProcessName"].ToString();

                        }
                    }
                    else
                    {
                       // grd_SubClient.Rows.Clear();
                    }
                }
            }
        }

        private void Bind_AllSubc()
        {


            Hashtable ht_All_Subclient = new Hashtable();
            DataTable dt_All_Subclient = new DataTable();
            ht_All_Subclient.Add("@Trans", "SELECT_SUBCLIENTS_FOR_ALL_CLIENTS");
            dt_All_Subclient = dataaccess.ExecuteSP("Sp_Error_DashBoard", ht_All_Subclient);
            if (dt_All_Subclient.Rows.Count > 0)
            {

                int row = grd_SubClient.Rows.Count;
                for (int i = 0; i < dt_All_Subclient.Rows.Count; i++, row++)
                {
                    grd_SubClient.Rows.Add();

                    grd_SubClient.Rows[row].Cells[1].Value = dt_All_Subclient.Rows[i]["Subprocess_Id"].ToString();
                    grd_SubClient.Rows[row].Cells[2].Value = dt_All_Subclient.Rows[i]["Sub_ProcessName"].ToString();
                    grd_SubClient[0, row].Value = true;

                }

            }

            else
            {
                for (int j = 0; j < dt_All_Subclient.Rows.Count; j++)
                {
                    for (int s = 0; s < grd_SubClient.Rows.Count; s++)
                    {
                        if (grd_SubClient.Rows[s].Cells[1].Value.ToString() == dt_All_Subclient.Rows[j]["Subprocess_Id"].ToString())
                        {
                            grd_SubClient.Rows.RemoveAt(s);

                        }
                    }
                }
            }



        }

        private void ChkAll_Client_Error_Task_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Client_Error_task.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Client_Error_task.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_All_Client_ErrorTask"] as DataGridViewCheckBoxCell);
                checkBox.Value = ChkAll_Client_Error_Task.Checked;

            }
        }

        private void grd_Client_Error_task_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_1 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Client_Error_task.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_All_Client_ErrorTask"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_1 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_1 = 0;
                    }
                }
            }
            if (unchk_cnt_1 == 1)
            {
                ChkAll_Client_Error_Task.Checked = false;
            }
            else
            {
                ChkAll_Client_Error_Task.Checked = true;
            }
        }

        private void Chk_Client_Error_Status_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Client_Error_Status.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Client_Error_Status.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_All_Client_Error_Status"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_Client_Error_Status.Checked;

            }
        }

        private void grd_Client_Error_Status_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_2 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_Client_Error_Status.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_All_Client_Error_Status"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_2 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_2 = 0;
                    }
                }
            }
            if (unchk_cnt_2 == 1)
            {
                Chk_Client_Error_Status.Checked = false;
            }
            else
            {
                Chk_Client_Error_Status.Checked = true;
            }
        }

        private void Chk_All_Client_Name_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_Client.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_Client.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_all_Client"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_Client_Name.Checked;

            }
        }

        private void Chk_All_Sub_Process_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_SubClient.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_SubClient.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_All_SubClient"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_Sub_Process.Checked;

            }
        }

        private void grd_SubClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_3 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_SubClient.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_All_SubClient"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_3 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_3 = 0;
                    }
                }
            }
            if (unchk_cnt_3 == 1)
            {
                Chk_All_Sub_Process.Checked = false;
            }
            else
            {
                Chk_All_Sub_Process.Checked = true;
            }
        }

        private void btn_Client_Clear_Click(object sender, EventArgs e)
        {
            ChkAll_Client_Error_Task.Checked = false;
            Chk_Client_Error_Status.Checked = false;
            Chk_All_Client_Name.Checked = false;
            Chk_All_Sub_Process.Checked = false;


            Chk_All_Client_Name_Click(sender, e);
            Chk_All_Sub_Process_Click(sender, e);
            Chk_Client_Error_Status_Click(sender, e);
            ChkAll_Client_Error_Task_Click(sender, e);


            Bind_Grid_Error_Task();
            Bind_Grid_Error_Status();
            Bind_Grid_Client();

            ddl_ErrClient_ErrorOnUser.SelectedIndex = 0;
            Bind_Bar_Chart_Error_On_Client_All_Task();
            Grid_ErrorOnClient_All_Task_Wise_Count();

        }

        // Export Error Client Chart
        private void Export_Error_On_Client()
        {
            load_Progressbar.Start_progres();
            DevExpress.XtraPrinting.PrintingSystem ps4 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl_4 = new DevExpress.XtraPrintingLinks.CompositeLink(ps4);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart4 = new DevExpress.XtraPrinting.PrintableComponentLink();

            chartControl4.Visible = true;
            pclChart4.Component = chartControl4;

            cl_4.PaperKind = System.Drawing.Printing.PaperKind.ESheet;
            cl_4.Landscape = true;
            cl_4.Margins.Right = 40;
            cl_4.Margins.Left = 40;

            cl_4.Links.AddRange(new object[] { pclChart4 });
            cl_4.ShowPreviewDialog();


        }

        private void btn_Client_Export_Click(object sender, EventArgs e)
        {
            Export_Error_On_Client();
        }

        //---------- BAR CHART----------------

        // 1) Date wise (Error On CLIENT All Task Wise)
        private void Bind_Bar_Chart_Error_On_Client_All_Task()
        {
            try
            {
                //chartControl4.Visible = true;
                //chartControl6.Visible = false;

                //Bar Chart
                Hashtable ht_ErrOnClient_Barchart = new Hashtable();
                DataTable dt_ErrOnClient_Barchart = new DataTable();

                ht_ErrOnClient_Barchart.Add("@Trans", "LINE_CHART_CLIENT_DATE_WISE_ALL_TASK");
                ht_ErrOnClient_Barchart.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnClient_Barchart.Add("@Error_To_Date", txt_Client_To_Date.Text);
                dt_ErrOnClient_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnClient_Barchart);

                chartControl4.DataSource = dt_ErrOnClient_Barchart;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_All_Task();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Client_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrClient_Barchart = new Hashtable();
                DataTable dt_ErrClient_Barchart = new DataTable();
                ht_ErrClient_Barchart.Add("@Trans", "LINE_CHART_CLIENT_DATE_WISE_ALL_TASK");
                ht_ErrClient_Barchart.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrClient_Barchart.Add("@Error_To_Date", txt_Client_To_Date.Text);
                dt_ErrClient_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrClient_Barchart);

                chartControl4.DataSource = dt_ErrClient_Barchart;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 2) USER wise
        private void Bind_Bar_Chart_Error_On_User_Wise(int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_client_User_wise = new Hashtable();
                DataTable dt_client_User_wise = new DataTable();
                ht_client_User_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_USER_WISE");
                ht_client_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_User_wise.Add("@User_Id", Err_User_id);
                dt_client_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_client_User_wise);

                chartControl4.DataSource = dt_client_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_User_Wise(Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_User_Wise(int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client_User = new Hashtable();
                DataTable dt_Line_Client_User = new DataTable();
                ht_Line_Client_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_USER_WISE");
                ht_Line_Client_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client_User.Add("@User_Id", Err_User_id);
                dt_Line_Client_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Client_User);

                chartControl4.DataSource = dt_Line_Client_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //  3) TASK WISE
        private void Bind_Bar_Chart_Error_On_Client_Task_Wise(string Order_Task)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnClient_taskwise = new Hashtable();
                DataTable dt_ErrOnClient_taskwise = new DataTable();
                ht_ErrOnClient_taskwise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_WISE");
                ht_ErrOnClient_taskwise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_Task", Order_Task);
                dt_ErrOnClient_taskwise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnClient_taskwise);
                // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl4.DataSource = dt_ErrOnClient_taskwise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_Wise(Order_Task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_Wise(string Order_Task)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnCl_Line_task = new Hashtable();
                DataTable dt_ErrOnCl_Line_task = new DataTable();
                ht_ErrOnCl_Line_task.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_WISE");
                ht_ErrOnCl_Line_task.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_Task", Order_Task);
                dt_ErrOnCl_Line_task = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnCl_Line_task);

                chartControl4.DataSource = dt_ErrOnCl_Line_task;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 4) BAR CHART Status wise
        private void Bind_Bar_Chart_Error_On_Client_Status_Wise(string Order_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnClient_statuswise = new Hashtable();
                DataTable dt_ErrOnClient_statuswise = new DataTable();
                ht_ErrOnClient_statuswise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_WISE");
                ht_ErrOnClient_statuswise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnClient_statuswise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnClient_statuswise.Add("@Error_Status", Order_Status);
                dt_ErrOnClient_statuswise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnClient_statuswise);

                chartControl4.DataSource = dt_ErrOnClient_statuswise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Status_Wise(Order_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Status_Wise(string Order_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnCl_Line_status = new Hashtable();
                DataTable dt_ErrOnCl_Line_status = new DataTable();
                ht_ErrOnCl_Line_status.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_WISE");
                ht_ErrOnCl_Line_status.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnCl_Line_status.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnCl_Line_status.Add("@Error_Status", Order_Status);
                dt_ErrOnCl_Line_status = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnCl_Line_status);

                chartControl4.DataSource = dt_ErrOnCl_Line_status;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 5) BAR CHART CLIENT and Subclient wise
        private void Bind_Bar_Chart_Error_On_Client_AND_Subclient_Wise(string Client, string SubClient)
        {
            try
            {
                //chartControl6.Visible = true;
                //chartControl4.Visible = false;
                //Bar Chart
                Hashtable ht_client_Subcl_wise = new Hashtable();
                DataTable dt_client_Subcl_wise = new DataTable();
                ht_client_Subcl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_client_Subcl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_Subcl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_Subcl_wise.Add("@Client", Client);
                ht_client_Subcl_wise.Add("@Subclient", SubClient);
                dt_client_Subcl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_client_Subcl_wise);

                chartControl4.DataSource = dt_client_Subcl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_AND_Subclient_Wise(Client, SubClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_AND_Subclient_Wise(string Client, string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client_SubCl = new Hashtable();
                DataTable dt_Line_Client_SubCl = new DataTable();
                ht_Line_Client_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_Line_Client_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client_SubCl.Add("@Client", Client);
                ht_Line_Client_SubCl.Add("@Subclient", SubClient);
                dt_Line_Client_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Client_SubCl);

                chartControl4.DataSource = dt_Line_Client_SubCl;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // 6) BAR CHART CLIENT wise
        private void Bind_Bar_Chart_Error_On_Client_Wise(string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_client_wise = new Hashtable();
                DataTable dt_client_wise = new DataTable();
                ht_client_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_CLIENT_WISE");
                ht_client_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_wise.Add("@Client", Client);
                dt_client_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_client_wise);

                chartControl4.DataSource = dt_client_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Wise(Client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Wise(string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client = new Hashtable();
                DataTable dt_Line_Client = new DataTable();
                ht_Line_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_CLIENT_WISE");
                ht_Line_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client.Add("@Client", Client);
                dt_Line_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Client);

                chartControl4.DataSource = dt_Line_Client;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 7)  USER and Task wise
        private void Bind_Bar_Chart_Error_On_Task_User_Wise(string Order_Task, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_User_wise = new Hashtable();
                DataTable dt_task_User_wise = new DataTable();
                ht_task_User_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_USER_WISE");
                ht_task_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_User_wise.Add("@Error_Task", Order_Task);
                ht_task_User_wise.Add("@User_Id", Err_User_id);
                dt_task_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_User_wise);

                chartControl4.DataSource = dt_task_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Task_User_Wise(Order_Task, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Task_User_Wise(string Order_Task, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_task_User = new Hashtable();
                DataTable dt_Line_task_User = new DataTable();
                ht_Line_task_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_USER_WISE");
                ht_Line_task_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_task_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_task_User.Add("@Error_Task", Order_Task);
                ht_Line_task_User.Add("@User_Id", Err_User_id);
                dt_Line_task_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_task_User);

                chartControl4.DataSource = dt_Line_task_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //  8) USER and Statuis wise
        private void Bind_Bar_Chart_Error_On_Status_User_Wise(string Order_Status, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_User_wise = new Hashtable();
                DataTable dt_task_User_wise = new DataTable();
                ht_task_User_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_USER_WISE");
                ht_task_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_User_wise.Add("@Error_Status", Order_Status);
                ht_task_User_wise.Add("@User_Id", Err_User_id);
                dt_task_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_User_wise);

                chartControl4.DataSource = dt_task_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Status_User_Wise(Order_Status, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_User_Wise(string Order_Status, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_task_User = new Hashtable();
                DataTable dt_Line_task_User = new DataTable();
                ht_Line_task_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_USER_WISE");
                ht_Line_task_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_task_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_task_User.Add("@Error_Status", Order_Status);
                ht_Line_task_User.Add("@User_Id", Err_User_id);
                dt_Line_task_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_task_User);

                chartControl4.DataSource = dt_Line_task_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 9) User and  BAR CHART CLIENT and Subclient  wise
        private void Bind_Bar_Chart_Error_On_Client_AND_Subclient_User_Wise(string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_client_Subcl_user_wise = new Hashtable();
                DataTable dt_client_Subcl_user_wise = new DataTable();
                ht_client_Subcl_user_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_client_Subcl_user_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_Subcl_user_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_Subcl_user_wise.Add("@Client", Client);
                ht_client_Subcl_user_wise.Add("@Subclient", SubClient);
                ht_client_Subcl_user_wise.Add("@User_Id", Err_User_id);
                dt_client_Subcl_user_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_client_Subcl_user_wise);

                chartControl4.DataSource = dt_client_Subcl_user_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_AND_Subclient_User_Wise(Client, SubClient, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_AND_Subclient_User_Wise(string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client_SubCl_user = new Hashtable();
                DataTable dt_Line_Client_SubCl_user = new DataTable();
                ht_Line_Client_SubCl_user.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_Line_Client_SubCl_user.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client_SubCl_user.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client_SubCl_user.Add("@Client", Client);
                ht_Line_Client_SubCl_user.Add("@Subclient", SubClient);
                ht_Line_Client_SubCl_user.Add("@User_Id", Err_User_id);
                dt_Line_Client_SubCl_user = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Client_SubCl_user);

                chartControl4.DataSource = dt_Line_Client_SubCl_user;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 10) User and more then one client --. CLIENT  wise 
        private void Bind_Bar_Chart_Error_On_Client_User_Wise(string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_client_Userwise = new Hashtable();
                DataTable dt_client_Userwise = new DataTable();
                ht_client_Userwise.Add("@Trans", "LINE_CHART_CLIENT_DATE_CLIENT_USER_WISE");
                ht_client_Userwise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_Userwise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_Userwise.Add("@Client", Client);
                ht_client_Userwise.Add("@User_Id", Err_User_id);
                dt_client_Userwise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_client_Userwise);

                chartControl4.DataSource = dt_client_Userwise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_User_Wise(Client, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_User_Wise(string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client_User = new Hashtable();
                DataTable dt_Line_Client_User = new DataTable();
                ht_Line_Client_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_CLIENT_USER_WISE");
                ht_Line_Client_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client_User.Add("@Client", Client);
                ht_Line_Client_User.Add("@User_Id", Err_User_id);
                dt_Line_Client_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Client_User);

                chartControl4.DataSource = dt_Line_Client_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //  11) USER and Task and Status wise
        private void Bind_Bar_Chart_Error_On_Task_AND_Status_AND_User_Wise(string Order_Task, string Order_Status, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Stat_User_wise = new Hashtable();
                DataTable dt_task_Stat_User_wise = new DataTable();
                ht_task_Stat_User_wise.Add("@Trans", "LINECHARTCLIENT_DATE_AND_TASK_AND_STATUS_USER_WISE");
                ht_task_Stat_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_Stat_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_Stat_User_wise.Add("@Error_Task", Order_Task);
                ht_task_Stat_User_wise.Add("@Error_Status", Order_Status);
                ht_task_Stat_User_wise.Add("@User_Id", Err_User_id);
                dt_task_Stat_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Stat_User_wise);

                chartControl4.DataSource = dt_task_Stat_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Task_AND_Status_AND_User_Wise(Order_Task, Order_Status, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Task_AND_Status_AND_User_Wise(string Order_Task, string Order_Status, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_task_Stat_User = new Hashtable();
                DataTable dt_Line_task_Stat_User = new DataTable();
                ht_Line_task_Stat_User.Add("@Trans", "LINECHARTCLIENT_DATE_AND_TASK_AND_STATUS_USER_WISE");
                ht_Line_task_Stat_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_task_Stat_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_task_Stat_User.Add("@Error_Task", Order_Task);
                ht_Line_task_Stat_User.Add("@Error_Status", Order_Status);
                ht_Line_task_Stat_User.Add("@User_Id", Err_User_id);
                dt_Line_task_Stat_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_task_Stat_User);

                chartControl4.DataSource = dt_Line_task_Stat_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // 12) user and  Task and CLIENT and subclient wise   iwse
        private void Bind_Bar_Chart_Error_On_Task_AND_Client_AND_SubClient_User_Wise(string Order_Task, string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_cl_SubCl_User_wise = new Hashtable();
                DataTable dt_task_cl_SubCl_User_wise = new DataTable();
                ht_task_cl_SubCl_User_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_task_cl_SubCl_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_cl_SubCl_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_cl_SubCl_User_wise.Add("@Error_Task", Order_Task);
                ht_task_cl_SubCl_User_wise.Add("@Client", Client);
                ht_task_cl_SubCl_User_wise.Add("@SubClient", SubClient);
                ht_task_cl_SubCl_User_wise.Add("@User_Id", Err_User_id);
                dt_task_cl_SubCl_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_cl_SubCl_User_wise);

                chartControl4.DataSource = dt_task_cl_SubCl_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Task_AND_Client_AND_SubClient_User_Wise(Order_Task, Client, SubClient, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Task_AND_Client_AND_SubClient_User_Wise(string Order_Task, string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_task_Cl_SubCl_User = new Hashtable();
                DataTable dt_Line_task_Cl_SubCl_User = new DataTable();
                ht_Line_task_Cl_SubCl_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_Line_task_Cl_SubCl_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_task_Cl_SubCl_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_task_Cl_SubCl_User.Add("@Error_Task", Order_Task);
                ht_Line_task_Cl_SubCl_User.Add("@Client", Client);
                ht_Line_task_Cl_SubCl_User.Add("@SubClient", SubClient);
                ht_Line_task_Cl_SubCl_User.Add("@User_Id", Err_User_id);
                dt_Line_task_Cl_SubCl_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_task_Cl_SubCl_User);

                chartControl4.DataSource = dt_Line_task_Cl_SubCl_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 13)  Status and CLIENT and subclient and User wise
        private void Bind_Bar_Chart_Error_On_Status_Client_SubClient_User_Wise(string Order_Status, string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_status_cl_SubCl_User_wise = new Hashtable();
                DataTable dt_status_cl_SubCl_User_wise = new DataTable();
                ht_status_cl_SubCl_User_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_status_cl_SubCl_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_status_cl_SubCl_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_status_cl_SubCl_User_wise.Add("@Error_Status", Order_Status);
                ht_status_cl_SubCl_User_wise.Add("@Client", Client);
                ht_status_cl_SubCl_User_wise.Add("@SubClient", SubClient);
                ht_status_cl_SubCl_User_wise.Add("@User_Id", Err_User_id);
                dt_status_cl_SubCl_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_status_cl_SubCl_User_wise);

                chartControl4.DataSource = dt_status_cl_SubCl_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Status_Client_SubClient_User_Wise(Order_Status, Client, SubClient, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_Client_SubClient_User_Wise(string Order_Status, string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Stat_Cl_SubCl_User = new Hashtable();
                DataTable dt_Line_Stat_Cl_SubCl_User = new DataTable();
                ht_Line_Stat_Cl_SubCl_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_Line_Stat_Cl_SubCl_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Stat_Cl_SubCl_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Stat_Cl_SubCl_User.Add("@Error_Status", Order_Status);
                ht_Line_Stat_Cl_SubCl_User.Add("@Client", Client);
                ht_Line_Stat_Cl_SubCl_User.Add("@SubClient", SubClient);
                ht_Line_Stat_Cl_SubCl_User.Add("@User_Id", Err_User_id);
                dt_Line_Stat_Cl_SubCl_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Stat_Cl_SubCl_User);

                chartControl4.DataSource = dt_Line_Stat_Cl_SubCl_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 14) User and  Task and Status and CLIENT and subclient wise 
        private void Bind_Bar_Chart_Error_On_Task_STATUS_AND_Client_AND_SubClient_User_Wise(string Order_Task, string Order_Status, string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_cl_SubCl_User_wise = new Hashtable();
                DataTable dt_task_cl_SubCl_User_wise = new DataTable();
                ht_task_cl_SubCl_User_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_STATUS_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_task_cl_SubCl_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_cl_SubCl_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_cl_SubCl_User_wise.Add("@Error_Task", Order_Task);
                ht_task_cl_SubCl_User_wise.Add("@Error_Status", Order_Status);
                ht_task_cl_SubCl_User_wise.Add("@Client", Client);
                ht_task_cl_SubCl_User_wise.Add("@SubClient", SubClient);
                ht_task_cl_SubCl_User_wise.Add("@User_Id", Err_User_id);

                dt_task_cl_SubCl_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_cl_SubCl_User_wise);

                chartControl4.DataSource = dt_task_cl_SubCl_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Task_STATUS_AND_Client_AND_SubClient_User_Wise(Order_Task, Order_Status, Client, SubClient, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Task_STATUS_AND_Client_AND_SubClient_User_Wise(string Order_Task, string Order_Status, string Client, string SubClient, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_task_Cl_SubCl_User = new Hashtable();
                DataTable dt_Line_task_Cl_SubCl_User = new DataTable();
                ht_Line_task_Cl_SubCl_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_STATUS_AND_CLIENT_AND_SUBCLIENT_USER_WISE");
                ht_Line_task_Cl_SubCl_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_task_Cl_SubCl_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_task_Cl_SubCl_User.Add("@Error_Task", Order_Task);
                ht_Line_task_Cl_SubCl_User.Add("@Error_Status", Order_Status);
                ht_Line_task_Cl_SubCl_User.Add("@Client", Client);
                ht_Line_task_Cl_SubCl_User.Add("@SubClient", SubClient);
                ht_Line_task_Cl_SubCl_User.Add("@User_Id", Err_User_id);

                dt_Line_task_Cl_SubCl_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_task_Cl_SubCl_User);

                chartControl4.DataSource = dt_Line_task_Cl_SubCl_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 15)User and  Task and  CLIENT and  wise
        private void Bind_Bar_Chart_Error_On_Task_Client_User_Wise(string Order_Task, string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_client_Userwise = new Hashtable();
                DataTable dt_task_client_Userwise = new DataTable();
                ht_task_client_Userwise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_USER_WISE");
                ht_task_client_Userwise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_client_Userwise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_client_Userwise.Add("@Error_Task", Order_Task);
                ht_task_client_Userwise.Add("@Client", Client);
                ht_task_client_Userwise.Add("@User_Id", Err_User_id);
                dt_task_client_Userwise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_client_Userwise);

                chartControl4.DataSource = dt_task_client_Userwise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Task_Client_User_Wise(Order_Task, Client, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Task_Client_User_Wise(string Order_Task, string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_task_Client_User = new Hashtable();
                DataTable dt_Line_task_Client_User = new DataTable();
                ht_Line_task_Client_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_USER_WISE");
                ht_Line_task_Client_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_task_Client_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_task_Client_User.Add("@Error_Task", Order_Task);
                ht_Line_task_Client_User.Add("@Client", Client);
                ht_Line_task_Client_User.Add("@User_Id", Err_User_id);
                dt_Line_task_Client_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_task_Client_User);

                chartControl4.DataSource = dt_Line_task_Client_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //  16) User and STtaus and  CLIENT  wise
        private void Bind_Bar_Chart_Error_On_Status_Client_User_Wise(string Order_Status, string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Status_client_Userwise = new Hashtable();
                DataTable dt_Status_client_Userwise = new DataTable();
                ht_Status_client_Userwise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_USER_WISE");
                ht_Status_client_Userwise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Status_client_Userwise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Status_client_Userwise.Add("@Error_Status", Order_Status);
                ht_Status_client_Userwise.Add("@Client", Client);
                ht_Status_client_Userwise.Add("@User_Id", Err_User_id);
                dt_Status_client_Userwise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Status_client_Userwise);

                chartControl4.DataSource = dt_Status_client_Userwise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Status_Client_User_Wise(Order_Status, Client, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_Client_User_Wise(string Order_Status, string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_status_ClientUser = new Hashtable();
                DataTable dt_Line_status_ClientUser = new DataTable();
                ht_Line_status_ClientUser.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_USER_WISE");
                ht_Line_status_ClientUser.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_status_ClientUser.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_status_ClientUser.Add("@Error_Status", Order_Status);
                ht_Line_status_ClientUser.Add("@Client", Client);
                ht_Line_status_ClientUser.Add("@User_Id", Err_User_id);
                dt_Line_status_ClientUser = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_status_ClientUser);

                chartControl4.DataSource = dt_Line_status_ClientUser;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //17)Task and Status and CLIENT  and User wise
        private void Bind_Bar_Chart_Error_On_Task_AND_Status_Client_User_Wise(string Order_Task, string Order_Status, string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_status_cl_User_wise = new Hashtable();
                DataTable dt_task_status_cl_User_wise = new DataTable();
                ht_task_status_cl_User_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_USER_WISE");
                ht_task_status_cl_User_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_status_cl_User_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_status_cl_User_wise.Add("@Error_Task", Order_Task);
                ht_task_status_cl_User_wise.Add("@Error_Status", Order_Status);
                ht_task_status_cl_User_wise.Add("@Client", Client);
                ht_task_status_cl_User_wise.Add("@User_Id", Err_User_id);
                dt_task_status_cl_User_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_status_cl_User_wise);

                chartControl4.DataSource = dt_task_status_cl_User_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Task_AND_Status_Client_User_Wise(Order_Task, Order_Status, Client, Err_User_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Task_AND_Status_Client_User_Wise(string Order_Task, string Order_Status, string Client, int Err_User_id)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_taskStatus_Client_User = new Hashtable();
                DataTable dt_Line_taskStatus_Client_User = new DataTable();
                ht_Line_taskStatus_Client_User.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_USER_WISE");
                ht_Line_taskStatus_Client_User.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_taskStatus_Client_User.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_taskStatus_Client_User.Add("@Error_Task", Order_Task);
                ht_Line_taskStatus_Client_User.Add("@Error_Status", Order_Status);
                ht_Line_taskStatus_Client_User.Add("@Client", Client);
                ht_Line_taskStatus_Client_User.Add("@User_Id", Err_User_id);
                dt_Line_taskStatus_Client_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_taskStatus_Client_User);

                chartControl4.DataSource = dt_Line_taskStatus_Client_User;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 18) Task and Status wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Wise(string Order_Task,string Order_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnClient_taskwise = new Hashtable();
                DataTable dt_ErrOnClient_taskwise = new DataTable();
                ht_ErrOnClient_taskwise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_WISE");
                ht_ErrOnClient_taskwise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_Task", Order_Task);
                ht_ErrOnClient_taskwise.Add("@Error_Status", Order_Status);
                dt_ErrOnClient_taskwise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnClient_taskwise);

                chartControl4.DataSource = dt_ErrOnClient_taskwise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Status_Wise(Order_Task,Order_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Status_Wise(string Order_Task, string Order_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnCl_Line_task = new Hashtable();
                DataTable dt_ErrOnCl_Line_task = new DataTable();
                ht_ErrOnCl_Line_task.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_WISE");
                ht_ErrOnCl_Line_task.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_Task", Order_Task);
                ht_ErrOnCl_Line_task.Add("@Error_Status", Order_Status);
                dt_ErrOnCl_Line_task = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOnCl_Line_task);

                chartControl4.DataSource = dt_ErrOnCl_Line_task;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 19) BAR CHART TASK  and CLIENT and subclient wise
        private void Bind_Bar_Chart_Error_On_Task_AND_Client_AND_SubClient_Wise(string Order_Task, string Client, string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_status_cl_SubCl_wise = new Hashtable();
                DataTable dt_status_cl_SubCl_wise = new DataTable();
                ht_status_cl_SubCl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_status_cl_SubCl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_status_cl_SubCl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_status_cl_SubCl_wise.Add("@Error_Task", Order_Task);
                ht_status_cl_SubCl_wise.Add("@Client", Client);
                ht_status_cl_SubCl_wise.Add("@SubClient", SubClient);
                dt_status_cl_SubCl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_status_cl_SubCl_wise);

                chartControl4.DataSource = dt_status_cl_SubCl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Task_AND_Client_AND_SubClient_Wise(Order_Task, Client, SubClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Task_AND_Client_AND_SubClient_Wise(string Order_Task, string Client, string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Stat_Cl_SubCl = new Hashtable();
                DataTable dt_Line_Stat_Cl_SubCl = new DataTable();
                ht_Line_Stat_Cl_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_Line_Stat_Cl_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Stat_Cl_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Stat_Cl_SubCl.Add("@Error_Task", Order_Task);
                ht_Line_Stat_Cl_SubCl.Add("@Client", Client);
                ht_Line_Stat_Cl_SubCl.Add("@SubClient", SubClient);
                dt_Line_Stat_Cl_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Stat_Cl_SubCl);

                chartControl4.DataSource = dt_Line_Stat_Cl_SubCl;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 20)  Status and CLIENT and subclient wise
        private void Bind_Bar_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise(string Order_Status, string Client, string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_status_cl_SubCl_wise = new Hashtable();
                DataTable dt_status_cl_SubCl_wise = new DataTable();
                ht_status_cl_SubCl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_status_cl_SubCl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_status_cl_SubCl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_status_cl_SubCl_wise.Add("@Error_Status", Order_Status);
                ht_status_cl_SubCl_wise.Add("@Client", Client);
                ht_status_cl_SubCl_wise.Add("@SubClient", SubClient);
                dt_status_cl_SubCl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_status_cl_SubCl_wise);

                chartControl4.DataSource = dt_status_cl_SubCl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise(Order_Status, Client, SubClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise(string Order_Status, string Client, string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Stat_Cl_SubCl = new Hashtable();
                DataTable dt_Line_Stat_Cl_SubCl = new DataTable();
                ht_Line_Stat_Cl_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_Line_Stat_Cl_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Stat_Cl_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Stat_Cl_SubCl.Add("@Error_Status", Order_Status);
                ht_Line_Stat_Cl_SubCl.Add("@Client", Client);
                ht_Line_Stat_Cl_SubCl.Add("@SubClient", SubClient);
                dt_Line_Stat_Cl_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Stat_Cl_SubCl);

                chartControl4.DataSource = dt_Line_Stat_Cl_SubCl;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 21) Task and Status and CLIENT and SubClient wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise(string Order_Task, string Order_Status, string Client, string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_status_cl_wise = new Hashtable();
                DataTable dt_task_status_cl_wise = new DataTable();
                ht_task_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_SUBCLIENT_WISE");
                ht_task_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_status_cl_wise.Add("@Error_Task", Order_Task);
                ht_task_status_cl_wise.Add("@Error_Status", Order_Status);
                ht_task_status_cl_wise.Add("@Client", Client);
                ht_task_status_cl_wise.Add("@SubClient", SubClient);
                dt_task_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_status_cl_wise);

                chartControl4.DataSource = dt_task_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise(Order_Task, Order_Status, Client, SubClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise(string Order_Task, string Order_Status, string Client, string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_taskStatus_Client = new Hashtable();
                DataTable dt_Line_taskStatus_Client = new DataTable();
                ht_Line_taskStatus_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_SUBCLIENT_WISE");
                ht_Line_taskStatus_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_Task", Order_Task);
                ht_Line_taskStatus_Client.Add("@Error_Status", Order_Status);
                ht_Line_taskStatus_Client.Add("@Client", Client);
                ht_Line_taskStatus_Client.Add("@SubClient", SubClient);
                dt_Line_taskStatus_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_taskStatus_Client);

                chartControl4.DataSource = dt_Line_taskStatus_Client;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 22) Task and CLIENT  wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Client_Wise(string Order_Task,string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_status_cl_wise = new Hashtable();
                DataTable dt_task_status_cl_wise = new DataTable();
                ht_task_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_WISE");
                ht_task_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_status_cl_wise.Add("@Error_Task", Order_Task);
                ht_task_status_cl_wise.Add("@Client", Client);

                dt_task_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_status_cl_wise);

                chartControl4.DataSource = dt_task_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Client_Wise(Order_Task, Client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Client_Wise(string Order_Task,string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_taskStatus_Client = new Hashtable();
                DataTable dt_Line_taskStatus_Client = new DataTable();
                ht_Line_taskStatus_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_WISE");
                ht_Line_taskStatus_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_Task", Order_Task);
                ht_Line_taskStatus_Client.Add("@Client", Client);

                dt_Line_taskStatus_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_taskStatus_Client);

                chartControl4.DataSource = dt_Line_taskStatus_Client;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        //  23) Status and CLIENT wise
        private void Bind_Bar_Chart_Error_On_Client_Status_AND_Client_Wise(string Order_Status, string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_status_cl_wise = new Hashtable();
                DataTable dt_status_cl_wise = new DataTable();
                ht_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_WISE");
                ht_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_status_cl_wise.Add("@Error_Status", Order_Status);
                ht_status_cl_wise.Add("@Client", Client);
                dt_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_status_cl_wise);

                chartControl4.DataSource = dt_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Status_AND_Client_Wise(Order_Status,Client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Status_AND_Client_Wise(string Order_Status, string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Status_Client = new Hashtable();
                DataTable dt_Line_Status_Client = new DataTable();
                ht_Line_Status_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_WISE");
                ht_Line_Status_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Status_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Status_Client.Add("@Error_Status", Order_Status);
                ht_Line_Status_Client.Add("@Client", Client);
                dt_Line_Status_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Status_Client);

                chartControl4.DataSource = dt_Line_Status_Client;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 24) Task and Status and CLIENT wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_Wise(string Order_Task, string Order_Status, string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_status_cl_wise = new Hashtable();
                DataTable dt_task_status_cl_wise = new DataTable();
                ht_task_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_WISE");
                ht_task_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_status_cl_wise.Add("@Error_Task", Order_Task);
                ht_task_status_cl_wise.Add("@Error_Status", Order_Status);
                ht_task_status_cl_wise.Add("@Client", Client);
                dt_task_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_status_cl_wise);

                chartControl4.DataSource = dt_task_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_Wise(Order_Task, Order_Status, Client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_Wise(string Order_Task, string Order_Status, string Client)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_taskStatus_Client = new Hashtable();
                DataTable dt_Line_taskStatus_Client = new DataTable();
                ht_Line_taskStatus_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_WISE");
                ht_Line_taskStatus_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_Task", Order_Task);
                ht_Line_taskStatus_Client.Add("@Error_Status", Order_Status);
                ht_Line_taskStatus_Client.Add("@Client", Client);
                dt_Line_taskStatus_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_taskStatus_Client);

                chartControl4.DataSource = dt_Line_taskStatus_Client;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Subclient wise
        private void Bind_Bar_Chart_ErrorOn_Subclient_Wise(string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_client_Subcl_wise = new Hashtable();
                DataTable dt_client_Subcl_wise = new DataTable();
                ht_client_Subcl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_SUBCLIENT_WISE");
                ht_client_Subcl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_Subcl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_Subcl_wise.Add("@Subclient", SubClient);
                dt_client_Subcl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_client_Subcl_wise);

                chartControl4.DataSource = dt_client_Subcl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_ErrorOn_Subclient_Wise(SubClient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_ErrorOn_Subclient_Wise(string SubClient)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client_SubCl = new Hashtable();
                DataTable dt_Line_Client_SubCl = new DataTable();
                ht_Line_Client_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_SUBCLIENT_WISE");
                ht_Line_Client_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client_SubCl.Add("@Subclient", SubClient);
                dt_Line_Client_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_Client_SubCl);

                chartControl4.DataSource = dt_Line_Client_SubCl;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void btn_ClientSubmit_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();

            ErrUserid = int.Parse(ddl_ErrClient_ErrorOnUser.SelectedValue.ToString());
            if (txt_Client_From_Date.Text != "" && txt_Client_To_Date.Text != "")
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();

                Order_Task_Count = 0; Order_Status_Count = 0; Error_OnClient_Count = 0; Error_Subprocess_Count = 0;
                for (int error_task = 0; error_task < grd_Client_Error_task.Rows.Count; error_task++)
                {

                    bool is_task = (bool)grd_Client_Error_task[0, error_task].FormattedValue;
                    if (is_task == true)
                    {
                        Order_Task_Count++; ;
                        errortask_id = int.Parse(grd_Client_Error_task.Rows[error_task].Cells[2].Value.ToString());

                        if (Order_Task_Count == 1)
                        {
                            //Single_Order_Task = errortask_id.ToString();

                            Single_Order_Task = errortask_id.ToString();
                            Order_Task = Single_Order_Task;
                            sb = sb.Append(Order_Task);
                        }
                        else
                        {
                            sb = sb.Append("," + errortask_id);
                            Order_Task = sb.ToString();
                            Order_Task_Count++;
                        }
                    }
                }
                for (int error_Status = 0; error_Status < grd_Client_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Order_Status = (bool)grd_Client_Error_Status[0, error_Status].FormattedValue;
                    if (is_Order_Status == true)
                    {
                        Order_Status_Count++;
            
                        error_Status_id = int.Parse(grd_Client_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1)
                        {
                            Order_Status = error_Status_id.ToString();
                            sb1 = sb1.Append(Order_Status);
                        }
                        else
                        {
                            //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                            sb1 = sb1.Append("," + error_Status_id);
                            Order_Status = sb1.ToString();
                            Order_Status_Count++;
                        }
                    }
                }
                for (int errorClient = 0; errorClient < grd_Client.Rows.Count; errorClient++)
                {

                    bool is_errorClient = (bool)grd_Client[0, errorClient].FormattedValue;
                    if (is_errorClient == true)
                    {
                        Error_OnClient_Count++;

                        clientId = int.Parse(grd_Client.Rows[errorClient].Cells[1].Value.ToString());
                        if (Error_OnClient_Count == 1)
                        {
                            Error_Client = clientId.ToString();
                            sb2 = sb2.Append(Error_Client);
                        }
                        else
                        {
                            sb2 = sb2.Append("," + clientId);
                            Error_Client = sb2.ToString();
                            Error_OnClient_Count++;
                        }
                     
                    }

                }



                if (Error_OnClient_Count == 1) 
                {

                    // client wise
                    for (int error_SubClient = 0; error_SubClient < grd_SubClient.Rows.Count; error_SubClient++)
                    {

                        bool is_error_SubClient = (bool)grd_SubClient[0, error_SubClient].FormattedValue;
                        if (is_error_SubClient == true)
                        {

                            Error_Subprocess_Count++;
                             Subprocess_id = int.Parse(grd_SubClient.Rows[error_SubClient].Cells[1].Value.ToString());

                           
                            if (Error_Subprocess_Count == 0)
                            {
                                Error_Subprocess = Subprocess_id.ToString();
                                Error_Subprocess_Count = 0;
                            }

                            if (Error_Subprocess_Count == 1)
                            {
                                Error_Subprocess = Subprocess_id.ToString();
                                sb3 = sb3.Append(Error_Subprocess);
                            }
                            else if (Error_Subprocess_Count >= 1)
                            {
                                sb3 = sb3.Append("," + Subprocess_id);
                                Error_Subprocess = sb3.ToString();
                                Error_Subprocess_Count++;
                            }
                        }

                    }
                }
                else
                {
                    Error_Subprocess_Count=0;
                }

                // 1)Date Wise
                if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid==0)
                {
                    Bind_Bar_Chart_Error_On_Client_All_Task();
                    Grid_ErrorOnClient_All_Task_Wise_Count();
                }
                // 2)User Wise
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_User_Wise(ErrUserid);
                    Grid_ErrorOnClient_ErrorOnUser_Wise_Count(ErrUserid);
                }
                // 3)task wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Task_Wise(Order_Task);
                    Grid_ErrorOnClient_Task_Wise_Count(Order_Task);
                }
                // 4)status wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Status_Wise(Order_Status);
                    Grid_ErrorOnClient_Status_Wise_Count(Order_Status);

                }
                // 5)client and subclient  (one client and more then one sub client --> subclient  Wise )
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnClient_Count == 1 && Error_Subprocess_Count >= 1 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_AND_Subclient_Wise(Error_Client, Error_Subprocess);
                    Grid_ErrorOnClient_Client_AND_SubClient_Wise_Count(Error_Client, Error_Subprocess);
                }
                //6) Client wise  (more then one client Client Wise )
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnClient_Count > 1 && Error_Subprocess_Count == 0  && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Wise(Error_Client);
                    Grid_ErrorOnClient_Client_Wise_Count(Error_Client);
                }


                //7) User  and Task Wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Task_User_Wise(Order_Task, ErrUserid);
                    Grid_ErrorOnClient_Task_AND_User_Wise_Count(Order_Task, ErrUserid);
                }
                // 8)User  and Status Wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Status_User_Wise(Order_Status, ErrUserid);
                    Grid_ErrorOnClient_Status_AND_User_Wise_Count(Order_Status, ErrUserid);
                }
                // 9)client and subclient and User wise
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnClient_Count == 1 && Error_Subprocess_Count >= 1 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Client_AND_Subclient_User_Wise(Error_Client, Error_Subprocess, ErrUserid);
                    Grid_ErrorOnClient_Client_AND_SubClient_User_Wise_Count(Error_Client, Error_Subprocess, ErrUserid);
                }
                // 10) User  and Client Wise
                else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnClient_Count >= 1 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Client_User_Wise(Error_Client, ErrUserid);
                    Grid_ErrorOnClient_User_AND_Client_Wise_Count(Error_Client, ErrUserid);
                }
                // 11)TASK , STATUS ,and user wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Task_AND_Status_AND_User_Wise(Order_Task, Order_Status, ErrUserid);
                    Grid_ErrorOnClient_Task_AND_Status_AND_User_Wise_Count(Order_Task, Order_Status, ErrUserid);

                }
                // 12)Task, single client and subclient and User wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnClient_Count == 1 && Error_Subprocess_Count >= 1 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Task_AND_Client_AND_SubClient_User_Wise(Order_Task, Error_Client, Error_Subprocess, ErrUserid);
                    Grid_ErrorOnClient_Task_AND_Client_AND_SubClient_User_Wise_Count(Order_Task, Error_Client, Error_Subprocess, ErrUserid);
                }
                // 13)user and status client and subclient  wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnClient_Count == 1 && Error_Subprocess_Count >= 1 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Status_Client_SubClient_User_Wise(Order_Status, Error_Client, Error_Subprocess, ErrUserid);
                    Grid_ErrorOnClient_Status_AND_Client_AND_SubClient_User_Wise_Count(Order_Status, Error_Client, Error_Subprocess, ErrUserid);
                }
                // 14) User and Task,STATUS and  single client and subclient 
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnClient_Count == 1 && Error_Subprocess_Count >= 1 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Task_STATUS_AND_Client_AND_SubClient_User_Wise(Order_Task, Order_Status, Error_Client, Error_Subprocess, ErrUserid);

                    Grid_ErrorOnClient_Task_STATUS_AND_Client_AND_SubClient_User_Wise_Count(Order_Task, Order_Status, Error_Client, Error_Subprocess, ErrUserid);
                }

                // 15) user and  task ,more then one client -- Client   Wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnClient_Count >= 1 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Task_Client_User_Wise(Order_Task, Error_Client, ErrUserid);
                    Grid_ErrorOnClient_Task_AND_Client_AND_User_Wise_Count(Order_Task, Error_Client, ErrUserid);
                }


                //16)  user and Status ,Client   Wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnClient_Count >= 1 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Status_Client_User_Wise(Order_Status, Error_Client, ErrUserid);
                    Grid_ErrorOnClient_Status_AND_Client_AND_User_Wise_Count(Order_Status, Error_Client, ErrUserid);
                }

                 // 17)  user and TASK , STATUS ,Client   wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnClient_Count >= 1 && Error_Subprocess_Count == 0 && ErrUserid > 0)
                {
                    Bind_Bar_Chart_Error_On_Task_AND_Status_Client_User_Wise(Order_Task, Order_Status, Error_Client, ErrUserid);
                    Grid_ErrorOnClient_Task_Status_AND_Client_AND_User_Wise_Count(Order_Task, Order_Status, Error_Client, ErrUserid);
                }

                // 18) task and status wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnClient_Count == 0 && Error_Subprocess_Count == 0 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Wise(Order_Task, Order_Status);
                    Grid_ErrorOnClient_Task_AND_Status_Wise_Count(Order_Task, Order_Status);
                }
                // 19) Task, single client and subclient wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnClient_Count == 1 && Error_Subprocess_Count >= 1 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Task_AND_Client_AND_SubClient_Wise(Order_Task, Error_Client, Error_Subprocess);
                    Grid_ErrorOnClient_Task_AND_Client_AND_SubClient_Wise_Count(Order_Task, Error_Client, Error_Subprocess);
                }

                // 20) status and  client and subclient
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnClient_Count >= 1 && Error_Subprocess_Count >= 1 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise(Order_Status, Error_Client, Error_Subprocess);
                    Grid_ErrorOnClient_Status_AND_Client_AND_SubClient_Wise_Count(Order_Status, Error_Client, Error_Subprocess);
                }
                // 21) Task,STATUS and  single client and subclient 
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnClient_Count == 1 && Error_Subprocess_Count >= 1 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise(Order_Task, Order_Status, Error_Client, Error_Subprocess);

                    Grid_ErrorOnClient_Task_AND_Client_AND_SubClient_Wise_Count(Order_Task, Order_Status, Error_Client, Error_Subprocess);
                }
                
                   
                // 22) task and  Client wise
                else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnClient_Count >= 1 && Error_Subprocess_Count == 0 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Task_AND_Client_Wise(Order_Task, Error_Client);
                    Grid_ErrorOnClient_Task_AND_Client_Wise(Order_Task, Error_Client);
                }
                // 23 ) Status and Client wise
                else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnClient_Count >= 1 &&  Error_Subprocess_Count == 0  && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Status_AND_Client_Wise(Order_Status, Error_Client);
                    Grid_ErrorOnClient_Status_AND_Client_Wise(Order_Status, Error_Client);
                }

                //  24) TASK , STATUS ,Client  wise
                else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnClient_Count >= 1 && Error_Subprocess_Count == 0 && ErrUserid == 0)
                {
                    Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_Wise(Order_Task, Order_Status, Error_Client);
                    Grid_ErrorOnClient_Task_AND_Status_AND_Client_Wise_Count(Order_Task,Order_Status, Error_Client);
                }


            }
            else
            {
                MessageBox.Show("Select Date");
                txt_Client_From_Date.Select();
            }
        }

        private void grd_Client_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grd_Client.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void grd_Client_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int client_Id=0;
                if (e.ColumnIndex == 0)
                {
                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    //int Client_Count; string Single_Client = "", Client="";
                    StringBuilder client_Sb = new StringBuilder();
                    Client_Count = 0;
                   
                    for (int clie = 0; clie < grd_Client.Rows.Count; clie++)
                    {
                        bool is_Clien = (bool)grd_Client[0, clie].FormattedValue;
                        if (is_Clien == true)
                        {
                            Client_Count = Client_Count + 1;
                            client_Id = int.Parse(grd_Client.Rows[clie].Cells[1].Value.ToString());

                            if (Client_Count == 1)
                            {

                                Single_Client = client_Id.ToString();
                                Client = Single_Client;
                                client_Sb = client_Sb.Append(Client);
                            }
                            else
                            {
                                client_Sb = client_Sb.Append("," + client_Id);
                                Client = client_Sb.ToString();
                                Client_Count++;
                            }
                        }
                    }
                    if (Client_Count == 1)
                    {
                        //clientId = int.Parse(grd_Client.Rows[e.RowIndex].Cells[1].Value.ToString());
                        ht.Add("@Trans", "SELECTCLIENTWISE");
                        ht.Add("@Client_Id", client_Id);
                        dt = dataaccess.ExecuteSP("Sp_Error_DashBoard", ht);
                        grd_SubClient.Rows.Clear();
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(grd_Client.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            {
                                int row = grd_SubClient.Rows.Count;
                                for (int j = 0; j < dt.Rows.Count; j++, row++)
                                {
                                    grd_SubClient.Rows.Add();

                                    grd_SubClient.Rows[row].Cells[1].Value = dt.Rows[j]["Subprocess_Id"].ToString();
                                    grd_SubClient.Rows[row].Cells[2].Value = dt.Rows[j]["Sub_ProcessName"].ToString();

                                    grd_SubClient[0, row].Value = true;
                                    Chk_All_Sub_Process.Checked = true;
                                }

                            }
                            else
                            {
                                            int row = grd_SubClient.Rows.Count;
                                            for (int m = 0; m < dt.Rows.Count; m++, row++)
                                            {
                                                grd_SubClient.Rows.Add();

                                                grd_SubClient.Rows[row].Cells[1].Value = dt.Rows[m]["Subprocess_Id"].ToString();
                                                grd_SubClient.Rows[row].Cells[2].Value = dt.Rows[m]["Sub_ProcessName"].ToString();

                                                grd_SubClient[0, row].Value = true;
                                                Chk_All_Sub_Process.Checked = true;
                                            }
                                }

                            }
                        
                    }
                    else if (Client_Count > 1)
                    {
                        ht.Clear();
                        dt.Clear();
                         grd_SubClient.Rows.Clear();
                         Chk_All_Sub_Process.Checked = true;
                        ht.Add("@Trans", "SELECT_CLIENT_WISE");
                        ht.Add("@Client", Client);
                        dt = dataaccess.ExecuteSP("Sp_Error_DashBoard_1", ht);
                        if (dt.Rows.Count > 0)
                        {
                            //if (Convert.ToBoolean(grd_Client.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            //{
                                for (int clie = 0; clie < grd_Client.Rows.Count; clie++)
                                {
                                    bool is_Clien = (bool)grd_Client[0, clie].FormattedValue;
                                    if (is_Clien == true)
                                    {


                                        DataRow dr = dt.NewRow();
                                        dr[0] = 0;
                                        dr[6] = "ALL";
                                        dt.Rows.InsertAt(dr, 0);

                                        //grd_SubClient.DataSource = dr[6];


                                        //grd_SubClient.Rows[0].Cells[1].Value = dt.Rows[0]["ALL"].ToString();


                                        int row = grd_SubClient.Rows.Count;
                                        grd_SubClient.Rows.Clear();
                                        for (int k = 0; k < dt.Rows.Count; k++, row++)
                                        {
                                            grd_SubClient.Rows.Add();
                                            grd_SubClient.Rows[0].Cells[2].Value = dt.Rows[0][6].ToString();

                                            grd_SubClient[0, 0].Value = true;
                                            // grd_SubClient[0, row].Value = "true";
                                            Chk_All_Sub_Process.Checked = true;
                                            break;
                                        }
                                    }

                            }
                        }
                        else
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                for (int s = 0; s < grd_SubClient.Rows.Count; s++)
                                {
                                    if (grd_SubClient.Rows[s].Cells[1].Value.ToString() == dt.Rows[j]["Subprocess_Id"].ToString())
                                    {
                                        grd_SubClient.Rows.RemoveAt(s);
                                        Chk_All_Sub_Process.Checked = false;
                                    }
                                }
                            }

                        }
                    }

                    if (Client_Count == 0)
                    {
                        grd_SubClient.Rows.Clear();
                        Chk_All_Sub_Process.Checked = false;
                    }
                }

            }
        }


        private void chartControl4_MouseClick(object sender, MouseEventArgs e)
        {
            string Error_subprocess = ""; Type_Name = ""; Error_Type_Name = "";
            Order_Task = ""; Order_Status = ""; Client_Count = 0;
            load_Progressbar.Start_progres();
            int unchk_count = 0; 
            
            ChartHitInfo hi = chartControl4.CalcHitInfo(e.X, e.Y);
            SeriesPoint point_5 = hi.SeriesPoint;

            if (point_5 != null)
            {
                Error_User_Id = int.Parse(ddl_ErrClient_ErrorOnUser.SelectedValue.ToString());
                //Error_On_UserName_argument = "Argument: " + point.Argument.ToString();
                Error_OnClient = point_5.Argument.ToString();
                //Error_Type_Name = Error_OnClient;
                Error_Client_Count = Convert.ToInt32(point_5.Values[0]).ToString();
                values = "Value(s): " + Error_Client_Count;
                if (Error_Client_Count.Length >= 1)
                {
                    for (int i = 0; i <= Error_Client_Count.Length - 1; i++)
                    {
                        values = values + ", " + Error_Client_Count[i].ToString();
                    }
                }
            }
            Error_OnClient_Count = 0;
        
            StringBuilder sb_1 = new StringBuilder();
            StringBuilder sb_2 = new StringBuilder();
            StringBuilder sb_3 = new StringBuilder();

            for (int errorClient = 0; errorClient < grd_Client.Rows.Count; errorClient++)
            {

                bool is_errorClient = (bool)grd_Client[0, errorClient].FormattedValue;
                if (is_errorClient == true)
                {
                    Error_OnClient_Count++;
                    clientId = int.Parse(grd_Client.Rows[errorClient].Cells[1].Value.ToString());
                    if (Error_OnClient_Count == 1)
                    {
                       // Error_Type_Name = clientId.ToString();
                       // sb_2 = sb_2.Append(Error_Type_Name);
                        //sb_2 = sb_2.Append(Error_Client);
                    }
                    else
                    {
                        //sb_2 = sb_2.Append("," + clientId);
                       // Error_Type_Name = sb_2.ToString();
                        Error_OnClient_Count++;
                    }

                }

            }

            Error_Tab_Page = "Error_On_Client";

            Hashtable ht_client = new Hashtable();
            DataTable dt_client = new DataTable();

            ht_client.Add("@Trans", "GET_ERROR_CLIENT_NAME");
            ht_client.Add("@Client_Name", Error_OnClient);
            dt_client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_client);
            if (dt_client.Rows.Count > 0)
            {
                Error_Type_Name = dt_client.Rows[0]["Client_Id"].ToString();

            }
            Type_Name = "";

            if (Error_OnClient_Count > 1)
            {
                Error_Tab_Page = "Error_On_Client";
                Type_Name = "";
            }
            else if (Error_OnClient_Count==1)
            {

                

                Error_Tab_Page = "Error_On_Subclient";

                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "GET_ERROR_SUBPROCESS_NAME");
                ht.Add("@SuProcess_Name", Error_OnClient);
                dt = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht);
                if (dt.Rows.Count > 0)
                {
                    //State_Id = int.Parse(dt.Rows[0]["State_ID"].ToString());
                    //County_Id =int.Parse(dt.Rows[0]["County_ID"].ToString());
                    Error_Type_Name = dt.Rows[0]["Client_Id"].ToString();
                    Type_Name = dt.Rows[0]["Subprocess_id"].ToString();
                }


                // Client and Subclient wise
                //for (int error_SubClient = 0; error_SubClient < grd_SubClient.Rows.Count; error_SubClient++)
                //{

                //    bool is_error_SubClient = (bool)grd_SubClient[0, error_SubClient].FormattedValue;
                //    if (is_error_SubClient == true)
                //    {

                //        Error_Subprocess_Count++;
                //        Subprocess_id = int.Parse(grd_SubClient.Rows[error_SubClient].Cells[1].Value.ToString());


                //        if (Error_Subprocess_Count == 0)
                //        {
                //            Type_Name = Subprocess_id.ToString();
                //            Error_Subprocess_Count = 0;
                //        }

                //        if (Error_Subprocess_Count == 1)
                //        {
                //            Type_Name = Subprocess_id.ToString();
                //            sb_3 = sb_3.Append(Type_Name);
                //        }
                //        else if (Error_Subprocess_Count >= 1)
                //        {
                //            sb_3 = sb_3.Append("," + Subprocess_id);
                //            Type_Name = sb_3.ToString();
                //            Error_Subprocess_Count++;
                //        }
                //    }
                //}
            }



            //Error_Tab_Page = "Error_On_Client";

            //Hashtable ht_client = new Hashtable();
            //DataTable dt_client = new DataTable();

            //ht_client.Add("@Trans", "GET_ERROR_CLIENT_NAME");
            //ht_client.Add("@Client_Name", Error_OnClient);
            //dt_client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_client);
            //if (dt_client.Rows.Count > 0)
            //{
            //    Error_Type_Name = dt_client.Rows[0]["Client_Id"].ToString();
                
            //}
            //Type_Name = "";
            
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            Order_Task_Count = 0; Order_Status_Count = 0; Error_OnClient_Count = 0; Error_Subprocess_Count = 0;
            for (int error_task = 0; error_task < grd_Client_Error_task.Rows.Count; error_task++)
            {

                bool is_task = (bool)grd_Client_Error_task[0, error_task].FormattedValue;
                if (is_task == true)
                {
                    Order_Task_Count = error_task + 1;
                    errortask_id = int.Parse(grd_Client_Error_task.Rows[error_task].Cells[2].Value.ToString());

                    if (Order_Task_Count == 1)
                    {
                        //Single_Order_Task = errortask_id.ToString();

                        Single_Order_Task = errortask_id.ToString();
                        Order_Task = Single_Order_Task;
                        sb = sb.Append(Order_Task);
                    }
                    else
                    {
                        sb = sb.Append("," + errortask_id);
                        Order_Task = sb.ToString();
                        Order_Task_Count++;
                    }
                }
            }
            for (int error_Status = 0; error_Status < grd_Client_Error_Status.Rows.Count; error_Status++)
            {
                bool is_Order_Status = (bool)grd_Client_Error_Status[0, error_Status].FormattedValue;
                if (is_Order_Status == true)
                {
                    Order_Status_Count = error_Status + 1;
                    error_Status_id = int.Parse(grd_Client_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                    if (Order_Status_Count == 1)
                    {
                        Order_Status = error_Status_id.ToString();
                        sb1 = sb1.Append(Order_Status);
                    }
                    else
                    {
                        //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                        sb1 = sb1.Append("," + error_Status_id);
                        Order_Status = sb1.ToString();
                        Order_Status_Count++;
                    }
                }
            }


            if (tabControl1.SelectedIndex == 3)
            {

                // Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_OnClient,State, Convert.ToInt32(Error_Client_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page);

                Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_Client_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                errordetails.Show();
            }
        }


        private void Grid_ErrorClient_All_Task_Count_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Error_Tab_Page = "Error_On_Client";
                Err_Userid = int.Parse(ddl_ErrClient_ErrorOnUser.SelectedValue.ToString());
                Error_User_Id = Err_Userid;
                Error_Type_Name = ""; Type_Name = "";
                StringBuilder sb = new StringBuilder();
                StringBuilder sb_1 = new StringBuilder();
                StringBuilder sb_2 = new StringBuilder();
                StringBuilder sb_3 = new StringBuilder();
                Order_Task = ""; Order_Status = ""; Error_Tab = "";
                Order_Task_Count = 0; Order_Status_Count = 0; Error_OnClient_Count = 0; Error_Subprocess_Count = 0;

                for (int error_Status = 0; error_Status < grd_Client_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Order_Status = (bool)grd_Client_Error_Status[0, error_Status].FormattedValue;
                    if (is_Order_Status == true)
                    {
                        Order_Status_Count ++;
                        error_Status_id = int.Parse(grd_Client_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1)
                        {
                            Order_Status = error_Status_id.ToString();
                            sb_1 = sb_1.Append(Order_Status);
                        }
                        else
                        {
                            //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                            sb_1 = sb_1.Append("," + error_Status_id);
                            Order_Status = sb_1.ToString();
                            Order_Status_Count++;
                        }
                    }
                }
                for (int errorClient = 0; errorClient < grd_Client.Rows.Count; errorClient++)
                {
                    bool is_errorClient = (bool)grd_Client[0, errorClient].FormattedValue;
                    if (is_errorClient == true)
                    {
                        Error_OnClient_Count ++;
                        clientId = int.Parse(grd_Client.Rows[errorClient].Cells[1].Value.ToString());
                        if (Error_OnClient_Count == 1)
                        {
                            Error_Type_Name = clientId.ToString();
                            sb_2 = sb_2.Append(Error_Client);
                        }
                        else
                        {
                            sb_2 = sb_2.Append("," + clientId);
                            Error_Type_Name = sb_2.ToString();
                            Error_OnClient_Count++;
                        }
                    }
                }
                 if (Error_OnClient_Count == 1)
                 {
                     Error_Tab_Page = "Error_On_Subclient";

                     // Client and Subclient wise
                     for (int error_SubClient = 0; error_SubClient < grd_SubClient.Rows.Count; error_SubClient++)
                     {

                         bool is_error_SubClient = (bool)grd_SubClient[0, error_SubClient].FormattedValue;
                         if (is_error_SubClient == true)
                         {

                             Error_Subprocess_Count ++;
                             Subprocess_id = int.Parse(grd_SubClient.Rows[error_SubClient].Cells[1].Value.ToString());
                             if (Error_Subprocess_Count == 0)
                             {
                                 Type_Name = Subprocess_id.ToString();
                                 Error_Subprocess_Count = 0;
                             }

                             if (Error_Subprocess_Count == 1)
                             {
                                 Type_Name = Subprocess_id.ToString();
                                 sb_3 = sb_3.Append(Type_Name);
                             }
                             else if (Error_Subprocess_Count >= 1)
                             {
                                 sb_3 = sb_3.Append("," + Subprocess_id);
                                 Type_Name = sb_3.ToString();
                                 Error_Subprocess_Count++;
                             }
                         }

                     }

                 }

                if (e.ColumnIndex == 0)
                {
                    if (Grid_ErrorClient_All_Task_Count.Rows[0].Cells[0].Value.ToString() != "0")
                    {
                        Order_Task = "2";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    if (Grid_ErrorClient_All_Task_Count.Rows[0].Cells[1].Value.ToString() != "0")
                    {
                        Order_Task = "3";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (Grid_ErrorClient_All_Task_Count.Rows[0].Cells[2].Value.ToString() != "0")
                    {
                        Order_Task = "4";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    if (Grid_ErrorClient_All_Task_Count.Rows[0].Cells[3].Value.ToString() != "0")
                    {
                        Order_Task = "7";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (Grid_ErrorClient_All_Task_Count.Rows[0].Cells[4].Value.ToString() != "0")
                    {
                        Order_Task = "23";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    if (Grid_ErrorClient_All_Task_Count.Rows[0].Cells[5].Value.ToString() != "0")
                    {
                        Order_Task = "24";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }

            }
        }

        // =-------------------CLIETN WISE GRID COUNT-----------

        // 1) Date wise  // Error On CLIENT All Task Wise

        private void Grid_ErrorOnClient_All_Task_Wise_Count()
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORCLIENT_ALLTASK_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        //2) user wise count
        private void Grid_ErrorOnClient_ErrorOnUser_Wise_Count(int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 3)  Task Wise
        private void Grid_ErrorOnClient_Task_Wise_Count(string Order_Task)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 4)  Status Wise
        private void Grid_ErrorOnClient_Status_Wise_Count(string Order_Status)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_STATUS_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 5)client and Sublcient
        private void Grid_ErrorOnClient_Client_AND_SubClient_Wise_Count(string Client, string Subclient)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_CLIENT_AND_SUBCLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        //6) Client Wise
        private void Grid_ErrorOnClient_Client_Wise_Count(string Error_Client)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_CLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Client", Error_Client);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 7) task and user wise count
        private void Grid_ErrorOnClient_Task_AND_User_Wise_Count(string Order_Task, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 8) Status and user wise count
        private void Grid_ErrorOnClient_Status_AND_User_Wise_Count(string Order_Status, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_STATUS_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 9) user and client and subclient 
        private void Grid_ErrorOnClient_Client_AND_SubClient_User_Wise_Count(string Client, string Subclient, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_CLIENT_AND_ERROR_SUBCLIENT_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 10) User and Client  Wise
        private void Grid_ErrorOnClient_User_AND_Client_Wise_Count(string Client, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_CLIENT_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 11) ( Task,Status,ErrUserid);
        private void Grid_ErrorOnClient_Task_AND_Status_AND_User_Wise_Count(string Order_Task, string Order_Status, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONCLIENT_TASK_AND_STATUS_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 12) Task, Client,Error_Subprocess, ErrUserid
        private void Grid_ErrorOnClient_Task_AND_Client_AND_SubClient_User_Wise_Count(string Order_Task, string Client, string Subclient, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_AND_ERROR_CLIENT_AND_ERROR_SUBCLIENT_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {

                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 13) Status, Error_Client, Error_Subprocess, ErrUserid
        private void Grid_ErrorOnClient_Status_AND_Client_AND_SubClient_User_Wise_Count(string Order_Status, string Client, string Subclient, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_STATUS_AND_ERROR_CLIENT_AND_ERROR_SUBCLIENT_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 14 ) user and  Task,Status and  Client,Error_Subprocess
        private void Grid_ErrorOnClient_Task_STATUS_AND_Client_AND_SubClient_User_Wise_Count(string Order_Task, string Order_Status, string Client, string Subclient, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_STATUS_AND_CLIENT_AND_SUBCLIENT_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {

                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }


        // 15) user and task ,client 
        private void Grid_ErrorOnClient_Task_AND_Client_AND_User_Wise_Count(string Order_Task, string Client, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_AND_ERROR_CLIENT_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        //16) User and status client wise
        private void Grid_ErrorOnClient_Status_AND_Client_AND_User_Wise_Count(string Order_Status, string Client, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_STATUS_AND_ERROR_CLIENT_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 17) user and Task,Status and Client 
        private void Grid_ErrorOnClient_Task_Status_AND_Client_AND_User_Wise_Count(string Order_Task, string Order_Status, string Client, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_AND_ERROR_STATUS_AND_ERROR_CLIENT_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 18) Task And Status Wise
        private void Grid_ErrorOnClient_Task_AND_Status_Wise_Count(string Order_Task, string Order_Status)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_AND_STATUS_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        //  19) Task, Client,Subclient
        private void Grid_ErrorOnClient_Task_AND_Client_AND_SubClient_Wise_Count(string Order_Task, string Client, string Subclient)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_AND_ERROR_CLIENT_AND_ERROR_SUBCLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);
            //htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {

                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 20) Status and Client subcleint Wise
        private void Grid_ErrorOnClient_Status_AND_Client_AND_SubClient_Wise_Count(string Order_Status, string Client, string Subclient)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERRORSTATUS_AND_CLIENT_AND_SUBCLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 21) Task,Status and  Client,Subclient
        private void Grid_ErrorOnClient_Task_AND_Client_AND_SubClient_Wise_Count(string Order_Task, string Order_Status, string Client, string Subclient)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_STATUS_AND_CLIENT_AND_SUBCLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            htlivecount.Add("@Subclient", Subclient);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {

                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 22) Task And Client Wise
        private void Grid_ErrorOnClient_Task_AND_Client_Wise(string Order_Task, string Client)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_TASK_AND_CLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Client", Client);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 23) Status and Client Wise
        private void Grid_ErrorOnClient_Status_AND_Client_Wise(string Order_Status, string Client)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERROR_STATUS_AND_CLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }

        // 24) Task and Status and Client Wise
        private void Grid_ErrorOnClient_Task_AND_Status_AND_Client_Wise_Count(string Order_Task, string Order_Status, string Client)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorClient_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_CLIENT_ERRORTASK_AND_ERRORSTATUS_AND_CLIENT_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Client_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Client_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Client", Client);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorClient_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorClient_All_Task_Count.ColumnCount = 6;

                Grid_ErrorClient_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorClient_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorClient_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorClient_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorClient_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorClient_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorClient_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorClient_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorClient_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorClient_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorClient_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorClient_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorClient_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorClient_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorClient_All_Task_Count);

        }


        // --------------------- STATE TAB -------------

        private void Bind_Grid_State_Error_Task()
        {
            Hashtable ht_StateErr_Task = new Hashtable();
            DataTable dt_StateErr_Task = new DataTable();

            ht_StateErr_Task.Add("@Trans", "BIND_ERROR_TASK");
            dt_StateErr_Task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_StateErr_Task);
            if (dt_StateErr_Task.Rows.Count > 0)
            {
                grd_StateWise_Error_Task.Rows.Clear();
                for (int i = 0; i < dt_StateErr_Task.Rows.Count; i++)
                {
                    grd_StateWise_Error_Task.Rows.Add();
                    grd_StateWise_Error_Task.Rows[i].Cells[1].Value = dt_StateErr_Task.Rows[i]["Order_Status"].ToString();
                    grd_StateWise_Error_Task.Rows[i].Cells[2].Value = dt_StateErr_Task.Rows[i]["Order_Status_Id"].ToString();
                }
            }
            else
            {
                grd_StateWise_Error_Task.Rows.Clear();

            }
        }

        private void Bind_Grid_State_Error_Status()
        {
            Hashtable ht_StateErr_Status = new Hashtable();
            DataTable dt_StateErr_Status = new DataTable();

            ht_StateErr_Status.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_StateErr_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_StateErr_Status);
            if (dt_StateErr_Status.Rows.Count > 0)
            {
                grd_StateWise_Error_Status.Rows.Clear();
                for (int i = 0; i < dt_StateErr_Status.Rows.Count; i++)
                {
                    grd_StateWise_Error_Status.Rows.Add();
                    grd_StateWise_Error_Status.Rows[i].Cells[1].Value = dt_StateErr_Status.Rows[i]["Error_Status"].ToString();
                    grd_StateWise_Error_Status.Rows[i].Cells[2].Value = dt_StateErr_Status.Rows[i]["Error_Status_Id"].ToString();
                }
            }
            else
            {
                grd_StateWise_Error_Status.Rows.Clear();

            }
        }

        private void Bind_Grid_State()
        {
            Hashtable ht_getError_State = new Hashtable();
            DataTable dt_getError_State = new DataTable();

            ht_getError_State.Add("@Trans", "SELECT_STATE");
            dt_getError_State = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getError_State);
            if (dt_getError_State.Rows.Count > 0)
            {
                grd_State.Rows.Clear();
                for (int i = 0; i < dt_getError_State.Rows.Count; i++)
                {
                    grd_State.Rows.Add();
                    grd_State.Rows[i].Cells[1].Value = dt_getError_State.Rows[i]["State_ID"].ToString();
                    grd_State.Rows[i].Cells[2].Value = dt_getError_State.Rows[i]["State"].ToString();
                }
            }
            else
            {
                grd_State.Rows.Clear();

            }


        }


        private void btn_StateWise_Clear_Click(object sender, EventArgs e)
        {
            chk_All_StateWise_ErrorTask.Checked = false;
            chk_All_StateWise_Error_Status.Checked = false;
            chk_All_State.Checked = false;
            Chk_All_County.Checked = false;

            Chk_All_County_Click(sender, e);
            chk_All_State_Click(sender, e);
            chk_All_StateWise_Error_Status_Click(sender, e);
            chk_All_StateWise_ErrorTask_Click(sender, e);


            Bind_Grid_State_Error_Task();
            Bind_Grid_State_Error_Status();
            Bind_Grid_State();

            ddl_State_ErrorOnUser.SelectedIndex = 0;
            Bind_Bar_Chart_Error_On_State_All_Task();
            Grid_ErrorOnState_All_Task_Wise_Count();


        }

        private void btn_State_Export_Click(object sender, EventArgs e)
        {
            Export_Error_On_State();
        }

        private void Export_Error_On_State()
        {
            load_Progressbar.Start_progres();
            DevExpress.XtraPrinting.PrintingSystem ps5 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl_5 = new DevExpress.XtraPrintingLinks.CompositeLink(ps5);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart5 = new DevExpress.XtraPrinting.PrintableComponentLink();

            chartControl5.Visible = true;
            pclChart5.Component = chartControl5;

            cl_5.PaperKind = System.Drawing.Printing.PaperKind.ESheet;
            cl_5.Landscape = true;
            cl_5.Margins.Right = 40;
            cl_5.Margins.Left = 40;

            cl_5.Links.AddRange(new object[] { pclChart5 });
            cl_5.ShowPreviewDialog();


        }

        // BAR CHART  All Task Wise
        private void Bind_Bar_Chart_Error_On_State_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnState_Barchart = new Hashtable();
                DataTable dt_ErrOnState_Barchart = new DataTable();

                ht_ErrOnState_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_ALL_TASK");
                ht_ErrOnState_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOnState_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                dt_ErrOnState_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnState_Barchart);



                chartControl5.DataSource = dt_ErrOnState_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;


                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_All_Task();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Err_State_Barchart = new Hashtable();
                DataTable dt_Err_State_Barchart = new DataTable();
                ht_Err_State_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_ALL_TASK");
                ht_Err_State_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Err_State_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                dt_Err_State_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Err_State_Barchart);


                chartControl5.DataSource = dt_Err_State_Barchart;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART  Indivisual Task Wise
        private void Bind_Bar_Chart_Error_On_State_Task_Wise(string Error_Task)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrOn_state_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_Barchart = new DataTable();

                ht_ErrOn_state_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_WISE");
                ht_ErrOn_state_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_Task", Error_Task);
                dt_ErrOn_state_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_state_Barchart);

                chartControl5.DataSource = dt_ErrOn_state_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_Wise(Error_Task);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_Wise(string Error_Task)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_Barchart = new Hashtable();
                DataTable dt_ErrState_Barchart = new DataTable();
                ht_ErrState_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_WISE");
                ht_ErrState_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_Barchart.Add("@Error_Task", Error_Task);
                dt_ErrState_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrState_Barchart);

                chartControl5.DataSource = dt_ErrState_Barchart;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On Status Wise
        private void Bind_Bar_Chart_Error_On_State_Status_Wise(string Error_Status)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrOn_state_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_Barchart = new DataTable();

                ht_ErrOn_state_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_WISE");
                ht_ErrOn_state_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_Status", Error_Status);
                dt_ErrOn_state_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_state_Barchart);

                chartControl5.DataSource = dt_ErrOn_state_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Status_Wise(Error_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Status_Wise(string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_Barchart = new Hashtable();
                DataTable dt_ErrState_Barchart = new DataTable();
                ht_ErrState_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_WISE");
                ht_ErrState_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_Barchart.Add("@Error_Status", Error_Status);
                dt_ErrState_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrState_Barchart);

                chartControl5.DataSource = dt_ErrState_Barchart;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  STATE Wise
        private void Bind_Bar_Chart_Error_On_State_Wise(string State)
        {
            try
            {
                Hashtable ht_ErrOn_state_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_Barchart = new DataTable();

                ht_ErrOn_state_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_WISE");
                ht_ErrOn_state_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_Barchart.Add("@State", State);
                // ht_ErrOn_state_Barchart.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_ErrOn_state_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_state_Barchart);

                chartControl5.DataSource = dt_ErrOn_state_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Wise(State);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Wise(string State)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_Barchart = new Hashtable();
                DataTable dt_ErrState_Barchart = new DataTable();

                ht_ErrState_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_WISE");
                ht_ErrState_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_Barchart.Add("@State", State);
                dt_ErrState_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrState_Barchart);

                chartControl5.DataSource = dt_ErrState_Barchart;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  TASK AND Status Wise
        private void Bind_Bar_Chart_Error_On_State_Task_AND_Status_Wise(string Error_Task, string Error_Status)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_STATUS_WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_Status_Wise.Add("@Error_Status", Error_Status);
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_Wise);

                chartControl5.DataSource = dt_Errtask_Status_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_AND_Status_Wise(Error_Task, Error_Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_AND_Status_Wise(string Error_Task, string Error_Status)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status = new Hashtable();
                DataTable dt_task_Status = new DataTable();
                ht_task_Status.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_STATUS_WISE");
                ht_task_Status.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status.Add("@Error_Task", Error_Task);
                ht_task_Status.Add("@Error_Status", Error_Status);
                dt_task_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status);

                chartControl5.DataSource = dt_task_Status;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  TASK AND STATE Wise
        private void Bind_Bar_Chart_Error_On_State_Task_AND_State_Wise(string Error_Task, string State)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE__WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_Status_Wise.Add("@State", State);
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_Wise);

                chartControl5.DataSource = dt_Errtask_Status_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_AND_State_Wise(Error_Task, State);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_AND_State_Wise(string Error_Task, string State)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status = new Hashtable();
                DataTable dt_task_Status = new DataTable();
                ht_task_Status.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE__WISE");
                ht_task_Status.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status.Add("@Error_Task", Error_Task);
                ht_task_Status.Add("@State", State);
                dt_task_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status);

                chartControl5.DataSource = dt_task_Status;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  TASK ,STATUS AND STATE Wise
        private void Bind_Bar_Chart_Error_On_Task_Status_State_Wise(string Error_Task,string Error_Status, string State)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_State_Wise = new Hashtable();
                DataTable dt_Errtask_Status_State_Wise = new DataTable();

                ht_Errtask_Status_State_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_WISE");
                ht_Errtask_Status_State_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_State_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_State_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_Status_State_Wise.Add("@Error_Status", Error_Status);
                ht_Errtask_Status_State_Wise.Add("@State", State);
                dt_Errtask_Status_State_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_State_Wise);

                chartControl5.DataSource = dt_Errtask_Status_State_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_Status_State_Wise(Error_Task,Error_Status,State);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_Status_State_Wise(string Error_Task, string Error_Status, string State)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State = new Hashtable();
                DataTable dt_task_Status_State = new DataTable();
                ht_task_Status_State.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_WISE");
                ht_task_Status_State.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State.Add("@Error_Task", Error_Task);
                ht_task_Status_State.Add("@Error_Status", Error_Status);
                ht_task_Status_State.Add("@State", State);
                dt_task_Status_State = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_State);

                chartControl5.DataSource = dt_task_Status_State;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  STATUS AND STATE Wise
        private void Bind_Bar_Chart_Error_On_Status_AND_State_Wise(string Error_Status,string State)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatus_State_Wise = new Hashtable();
                DataTable dt_ErrStatus_State_Wise = new DataTable();

                ht_ErrStatus_State_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_WISE");
                ht_ErrStatus_State_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatus_State_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatus_State_Wise.Add("@Error_Status", Error_Status);
                ht_ErrStatus_State_Wise.Add("@State", State);
                dt_ErrStatus_State_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatus_State_Wise);

                chartControl5.DataSource = dt_ErrStatus_State_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Status_AND_State_Wise(Error_Status, State);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Status_AND_State_Wise(string Error_Status, string State)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State = new Hashtable();
                DataTable dt_task_Status_State = new DataTable();
                ht_task_Status_State.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_WISE");
                ht_task_Status_State.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State.Add("@Error_Status", Error_Status);
                ht_task_Status_State.Add("@State", State);
                dt_task_Status_State = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_State);

                chartControl5.DataSource = dt_task_Status_State;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  STATE and County Wise
        private void Bind_Bar_Chart_Error_On_State_AND_County_Wise(string State, string County)
        {
            try
            {
                //Bar Chart
                chartControl5.DataSource = null;

                Hashtable ht_ErrStateCounty_Wise = new Hashtable();
                DataTable dt_ErrStateCounty_Wise = new DataTable();

                ht_ErrStateCounty_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_COUNTY_WISE");
                ht_ErrStateCounty_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStateCounty_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStateCounty_Wise.Add("@State", State);
                ht_ErrStateCounty_Wise.Add("@County", County);
                dt_ErrStateCounty_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStateCounty_Wise);

                chartControl5.DataSource = dt_ErrStateCounty_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_AND_County_Wise(State, County);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_AND_County_Wise(string State, string County)
        {
            try
            {

                //Bar Chart
                Hashtable ht_StateCounty = new Hashtable();
                DataTable dt_StateCounty = new DataTable();
                ht_StateCounty.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_COUNTY_WISE");
                ht_StateCounty.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_StateCounty.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_StateCounty.Add("@State", State);
                ht_StateCounty.Add("@County", County);
                dt_StateCounty = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_StateCounty);

                chartControl5.DataSource = dt_StateCounty;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  TASK ,STATUS AND STATEand county  Wise
        private void Bind_Bar_Chart_Error_On_Task_Status_State_And_County_Wise(string Error_Task, string Error_Status, string State, string County)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_StatusState_County_Wise = new Hashtable();
                DataTable dt_Errtask_StatusState_County_Wise = new DataTable();

                ht_Errtask_StatusState_County_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_COUNTY_WISE");
                ht_Errtask_StatusState_County_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_StatusState_County_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_StatusState_County_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_StatusState_County_Wise.Add("@Error_Status", Error_Status);
                ht_Errtask_StatusState_County_Wise.Add("@State", State);
                ht_Errtask_StatusState_County_Wise.Add("@County",County);
                dt_Errtask_StatusState_County_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_StatusState_County_Wise);

                chartControl5.DataSource = dt_Errtask_StatusState_County_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_Status_State_And_County_Wise(Error_Task, Error_Status, State, County);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_Status_State_And_County_Wise(string Error_Task, string Error_Status, string State, string County)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State_County = new Hashtable();
                DataTable dt_task_Status_State_County = new DataTable();
                ht_task_Status_State_County.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_COUNTY_WISE");
                ht_task_Status_State_County.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State_County.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State_County.Add("@Error_Task", Error_Task);
                ht_task_Status_State_County.Add("@Error_Status", Error_Status);
                ht_task_Status_State_County.Add("@State", State);
                ht_task_Status_State_County.Add("@County", County);
                dt_task_Status_State_County = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_State_County);

                chartControl5.DataSource = dt_task_Status_State_County;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  TASK , STATE AND COUNTY Wise
        private void Bind_Bar_Chart_Error_On_Task_State_County_Wise(string Error_Task, string State, string County)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_State_County_Wise = new Hashtable();
                DataTable dt_Errtask_State_County_Wise = new DataTable();

                ht_Errtask_State_County_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_AND_COUNTY_WISE");
                ht_Errtask_State_County_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_State_County_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_State_County_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_State_County_Wise.Add("@State", State);
                ht_Errtask_State_County_Wise.Add("@County", County);
                dt_Errtask_State_County_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_State_County_Wise);

                chartControl5.DataSource = dt_Errtask_State_County_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_State_County_Wise(Error_Task,State, County);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_State_County_Wise(string Error_Task, string State, string County)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_State_County = new Hashtable();
                DataTable dt_task_State_County = new DataTable();
                ht_task_State_County.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_AND_COUNTY_WISE");
                ht_task_State_County.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_State_County.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_State_County.Add("@Error_Task", Error_Task);
                ht_task_State_County.Add("@State", State);
                ht_task_State_County.Add("@County", County);
                dt_task_State_County = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_State_County);

                chartControl5.DataSource = dt_task_State_County;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  Status and STATE AND COUNTY Wise
        private void Bind_Bar_Chart_Error_On_Status_AND_State_AND_County_Wise(string Error_Status, string State, string County)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatus_State_County_Wise = new Hashtable();
                DataTable dt_ErrStatus_State_County_Wise = new DataTable();

                ht_ErrStatus_State_County_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_COUNTY_WISE");
                ht_ErrStatus_State_County_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatus_State_County_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatus_State_County_Wise.Add("@Error_Status", Error_Status);
                ht_ErrStatus_State_County_Wise.Add("@State", State);
                ht_ErrStatus_State_County_Wise.Add("@County", County);
                dt_ErrStatus_State_County_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatus_State_County_Wise);

                chartControl5.DataSource = dt_ErrStatus_State_County_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Status_AND_State_AND_County_Wise(Error_Status,State,County);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Status_AND_State_AND_County_Wise(string Error_Status, string State, string County)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatusStateCounty_Wise = new Hashtable();
                DataTable dt_ErrStatusStateCounty_Wise = new DataTable();
                ht_ErrStatusStateCounty_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_COUNTY_WISE");
                ht_ErrStatusStateCounty_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatusStateCounty_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatusStateCounty_Wise.Add("@Error_Status", Error_Status);
                ht_ErrStatusStateCounty_Wise.Add("@State", State);
                ht_ErrStatusStateCounty_Wise.Add("@County", County);
                dt_ErrStatusStateCounty_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatusStateCounty_Wise);

                chartControl5.DataSource = dt_ErrStatusStateCounty_Wise;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART  County Wise
        private void Bind_Bar_Chart_Error_On_County_Wise(string County)
        {
            try
            {
                //Bar Chart
                chartControl5.DataSource = null;

                Hashtable ht_ErrCounty_Wise = new Hashtable();
                DataTable dt_ErrCounty_Wise = new DataTable();

                ht_ErrCounty_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_COUNTY_WISE");
                ht_ErrCounty_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrCounty_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                //ht_EteCounty_Wise.Add("@State", State);
                ht_ErrCounty_Wise.Add("@County", County);
                dt_ErrCounty_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrCounty_Wise);

                chartControl5.DataSource = dt_ErrCounty_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_County_Wise(County);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_County_Wise(string County)
        {
            try
            {

                //Bar Chart
                Hashtable ht_County = new Hashtable();
                DataTable dt_County = new DataTable();
                ht_County.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_COUNTY_WISE");
                ht_County.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_County.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_County.Add("@County", County);
                dt_County = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_County);

                chartControl5.DataSource = dt_County;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // 17-07-2018 //  User Wise
        
        private void Bind_Bar_Chart_ErrorOnUser_Wise(int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrOn_state_User = new Hashtable();
                DataTable dt_ErrOn_state_User = new DataTable();

                ht_ErrOn_state_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_USER_WISE");
                ht_ErrOn_state_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_User.Add("@User_Id", ErrUserid);
                dt_ErrOn_state_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_state_User);

                chartControl5.DataSource = dt_ErrOn_state_User;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_ErrorOnUser_Wise(ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_ErrorOnUser_Wise(int ErrUserid)
        {
            try
            {

                //Line Chart
                Hashtable ht_Line_ErrState_User = new Hashtable();
                DataTable dt_Line_ErrState_User = new DataTable();
                ht_Line_ErrState_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_USER_WISE");
                ht_Line_ErrState_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Line_ErrState_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Line_ErrState_User.Add("@User_Id", ErrUserid);
                dt_Line_ErrState_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_ErrState_User);

                chartControl5.DataSource = dt_Line_ErrState_User;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // Task and User Wise
        private void Bind_Bar_Chart_ErrorOnTask_AND_User_Wise(string Error_Task, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrOn_state_User = new Hashtable();
                DataTable dt_ErrOn_state_User = new DataTable();

                ht_ErrOn_state_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_USER_WISE");
                ht_ErrOn_state_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_User.Add("@Error_Task", Error_Task);
                ht_ErrOn_state_User.Add("@User_Id", ErrUserid);
                dt_ErrOn_state_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_state_User);

                chartControl5.DataSource = dt_ErrOn_state_User;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_ErrorOnTask_AND_User_Wise(Error_Task, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_ErrorOnTask_AND_User_Wise(string Error_Task,int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_ErrState_User = new Hashtable();
                DataTable dt_Line_ErrState_User = new DataTable();
                ht_Line_ErrState_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_USER_WISE");
                ht_Line_ErrState_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Line_ErrState_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Line_ErrState_User.Add("@Error_Task", Error_Task);
                ht_Line_ErrState_User.Add("@User_Id", ErrUserid);
                dt_Line_ErrState_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Line_ErrState_User);

                chartControl5.DataSource = dt_Line_ErrState_User;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On Status and User  Wise
        private void Bind_Bar_Chart_ErrorOnStatus_AND_User_Wise(string Error_Status, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrOn_state_User_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_User_Barchart = new DataTable();

                ht_ErrOn_state_User_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_USER_WISE");
                ht_ErrOn_state_User_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_User_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_User_Barchart.Add("@Error_Status", Error_Status);
                ht_ErrOn_state_User_Barchart.Add("@User_Id", ErrUserid);
                dt_ErrOn_state_User_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_state_User_Barchart);

                chartControl5.DataSource = dt_ErrOn_state_User_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_ErrorOnStatus_AND_User_Wise(Error_Status, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_ErrorOnStatus_AND_User_Wise(string Error_Status, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_User_Barchart = new Hashtable();
                DataTable dt_ErrState_User_Barchart = new DataTable();
                ht_ErrState_User_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_USER_WISE");
                ht_ErrState_User_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_User_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_User_Barchart.Add("@Error_Status", Error_Status);
                ht_ErrState_User_Barchart.Add("@User_Id", ErrUserid);
                dt_ErrState_User_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrState_User_Barchart);

                chartControl5.DataSource = dt_ErrState_User_Barchart;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //18-07-2018


        // BAR CHART Error On  STATE and User Wise
        private void Bind_Bar_Chart_Error_On_State_AND_User_Wise(string Error_State, int ErrUserid)
        {
            try
            {
                Hashtable ht_ErrOn_state_User_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_User_Barchart = new DataTable();

                ht_ErrOn_state_User_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_USER_WISE");
                ht_ErrOn_state_User_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_User_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_User_Barchart.Add("@State", Error_State);
                ht_ErrOn_state_User_Barchart.Add("@User_Id", ErrUserid);
                dt_ErrOn_state_User_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrOn_state_User_Barchart);

                chartControl5.DataSource = dt_ErrOn_state_User_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_AND_User_Wise(Error_State, ErrUserid);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_AND_User_Wise(string Error_State, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_User_Line = new Hashtable();
                DataTable dt_ErrState_User_Line = new DataTable();

                ht_ErrState_User_Line.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_USER_WISE");
                ht_ErrState_User_Line.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_User_Line.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_User_Line.Add("@State", Error_State);
                ht_ErrState_User_Line.Add("@User_Id", ErrUserid);
                dt_ErrState_User_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrState_User_Line);

                chartControl5.DataSource = dt_ErrState_User_Line;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  TASK AND Status and USER Wise
        private void Bind_Bar_Chart_Error_On_State_Task_AND_Status_AND_User_Wise(string Error_Task, string Error_Status, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_User_Wise = new Hashtable();
                DataTable dt_Errtask_Status_User_Wise = new DataTable();

                ht_Errtask_Status_User_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_STATUS_USER_WISE");
                ht_Errtask_Status_User_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_User_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_User_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_Status_User_Wise.Add("@Error_Status", Error_Status);
                ht_Errtask_Status_User_Wise.Add("@User_Id", ErrUserid);
                dt_Errtask_Status_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_User_Wise);

                chartControl5.DataSource = dt_Errtask_Status_User_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_AND_Status_AND_UserWise(Error_Task, Error_Status, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_AND_Status_AND_UserWise(string Error_Task, string Error_Status, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_User = new Hashtable();
                DataTable dt_task_Status_User = new DataTable();
                ht_task_Status_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_STATUS_USER_WISE");
                ht_task_Status_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_User.Add("@Error_Task", Error_Task);
                ht_task_Status_User.Add("@Error_Status", Error_Status);
                ht_task_Status_User.Add("@User_Id", ErrUserid);
                dt_task_Status_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_User);

                chartControl5.DataSource = dt_task_Status_User;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  TASK AND STATE and User Wise
        private void Bind_Bar_Chart_Error_On_Task_AND_State_AND_User_Wise(string Error_Task, string Error_State, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Err_task_Status_User_Wise = new Hashtable();
                DataTable dt_Err_task_Status_User_Wise = new DataTable();

                ht_Err_task_Status_User_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_USER_WISE");
                ht_Err_task_Status_User_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Err_task_Status_User_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Err_task_Status_User_Wise.Add("@Error_Task", Error_Task);
                ht_Err_task_Status_User_Wise.Add("@State", Error_State);
                ht_Err_task_Status_User_Wise.Add("@User_Id", ErrUserid);
                dt_Err_task_Status_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Err_task_Status_User_Wise);

                chartControl5.DataSource = dt_Err_task_Status_User_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_AND_State_AND_User_Wise(Error_Task, Error_State, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_AND_State_AND_User_Wise(string Error_Task, string Error_State, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_User_Line = new Hashtable();
                DataTable dt_task_Status_User_Line = new DataTable();
                ht_task_Status_User_Line.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_USER_WISE");
                ht_task_Status_User_Line.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_User_Line.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_User_Line.Add("@Error_Task", Error_Task);
                ht_task_Status_User_Line.Add("@State", Error_State);
                ht_task_Status_User_Line.Add("@User_Id", ErrUserid);
                dt_task_Status_User_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_User_Line);

                chartControl5.DataSource = dt_task_Status_User_Line;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  STATUS AND STATE and User Wise
        private void Bind_Bar_Chart_Error_On_Status_AND_State_AND_User_Wise(string Error_Status, string Error_State, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatus_State_User_Wise = new Hashtable();
                DataTable dt_ErrStatus_State_User_Wise = new DataTable();

                ht_ErrStatus_State_User_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_USER_WISE");
                ht_ErrStatus_State_User_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatus_State_User_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatus_State_User_Wise.Add("@Error_Status", Error_Status);
                ht_ErrStatus_State_User_Wise.Add("@State", Error_State);
                ht_ErrStatus_State_User_Wise.Add("@User_Id", ErrUserid);
                dt_ErrStatus_State_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatus_State_User_Wise);

                chartControl5.DataSource = dt_ErrStatus_State_User_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Status_AND_State_AND_User_Wise(Error_Status, Error_State, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_AND_State_AND_User_Wise(string Error_Status, string Error_State, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State_User_Line = new Hashtable();
                DataTable dt_task_Status_State_User_Line = new DataTable();
                ht_task_Status_State_User_Line.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_USER_WISE");
                ht_task_Status_State_User_Line.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State_User_Line.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State_User_Line.Add("@Error_Status", Error_Status);
                ht_task_Status_State_User_Line.Add("@State", Error_State);
                ht_task_Status_State_User_Line.Add("@User_Id", ErrUserid);
                dt_task_Status_State_User_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_State_User_Line);
                                                                                                 
                chartControl5.DataSource = dt_task_Status_State_User_Line;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  TASK ,STATUS AND STATE and User  Wise
        private void Bind_Bar_Chart_Error_On_Task_Status_State_AND_User_Wise(string Error_Task, string Error_Status, string Error_State, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_State_User_Wise = new Hashtable();
                DataTable dt_Errtask_Status_State_User_Wise = new DataTable();

                ht_Errtask_Status_State_User_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_USER_WISE");
                ht_Errtask_Status_State_User_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_State_User_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_State_User_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_Status_State_User_Wise.Add("@Error_Status", Error_Status);
                ht_Errtask_Status_State_User_Wise.Add("@State", Error_State);
                ht_Errtask_Status_State_User_Wise.Add("@User_Id", ErrUserid);
                dt_Errtask_Status_State_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_State_User_Wise);

                chartControl5.DataSource = dt_Errtask_Status_State_User_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_Status_State_AND_User_Wise(Error_Task, Error_Status, Error_State, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_Status_State_AND_User_Wise(string Error_Task, string Error_Status, string Error_State, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State_User_Line = new Hashtable();
                DataTable dt_task_Status_State_User_Line = new DataTable();
                ht_task_Status_State_User_Line.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_USER_WISE");
                ht_task_Status_State_User_Line.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State_User_Line.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State_User_Line.Add("@Error_Task", Error_Task);
                ht_task_Status_State_User_Line.Add("@Error_Status", Error_Status);
                ht_task_Status_State_User_Line.Add("@State", Error_State);
                ht_task_Status_State_User_Line.Add("@User_Id", ErrUserid);
                dt_task_Status_State_User_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_State_User_Line);

                chartControl5.DataSource = dt_task_Status_State_User_Line;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  TASK , STATE AND COUNTY and User Wise
        private void Bind_Bar_Chart_Error_On_Task_State_County_AND_User_Wise(string Error_Task, string Error_State, string County, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_State_County_User_Wise = new Hashtable();
                DataTable dt_Errtask_State_County_User_Wise = new DataTable();

                ht_Errtask_State_County_User_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_AND_COUNTY_USER_WISE");
                ht_Errtask_State_County_User_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_State_County_User_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_State_County_User_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_State_County_User_Wise.Add("@State", Error_State);
                ht_Errtask_State_County_User_Wise.Add("@County", County);
                ht_Errtask_State_County_User_Wise.Add("@User_Id", ErrUserid);
                dt_Errtask_State_County_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_State_County_User_Wise);


                chartControl5.DataSource = dt_Errtask_State_County_User_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_State_County_AND_User_Wise(Error_Task, Error_State, County, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_State_County_AND_User_Wise(string Error_Task, string Error_State, string County, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_State_County_User = new Hashtable();
                DataTable dt_task_State_County_User = new DataTable();
                ht_task_State_County_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_AND_COUNTY_USER_WISE");
                ht_task_State_County_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_State_County_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_State_County_User.Add("@Error_Task", Error_Task);
                ht_task_State_County_User.Add("@State", Error_State);
                ht_task_State_County_User.Add("@County", County);
                ht_task_State_County_User.Add("@User_Id", ErrUserid);
                dt_task_State_County_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_State_County_User);
                                                                                              
                chartControl5.DataSource = dt_task_State_County_User;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  Status and STATE AND COUNTY and USER Wise
        private void Bind_Bar_Chart_Error_On_Status_AND_State_AND_County_AND_User_Wise(string Error_Status, string Error_State, string County, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatus_State_County_User_Wise = new Hashtable();
                DataTable dt_ErrStatus_State_County_User_Wise = new DataTable();

                ht_ErrStatus_State_County_User_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_COUNTY_USER_WISE");
                ht_ErrStatus_State_County_User_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatus_State_County_User_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatus_State_County_User_Wise.Add("@Error_Status", Error_Status);
                ht_ErrStatus_State_County_User_Wise.Add("@State", Error_State);
                ht_ErrStatus_State_County_User_Wise.Add("@County", County);
                ht_ErrStatus_State_County_User_Wise.Add("@User_Id", ErrUserid);
                dt_ErrStatus_State_County_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatus_State_County_User_Wise);

                chartControl5.DataSource = dt_ErrStatus_State_County_User_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Status_AND_State_AND_County_AND_UserWise(Error_Status, Error_State, County, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Status_AND_State_AND_County_AND_UserWise(string Error_Status, string Error_State, string County, int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatusStateCounty_Wise = new Hashtable();
                DataTable dt_ErrStatusStateCounty_Wise = new DataTable();
                ht_ErrStatusStateCounty_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_COUNTY_USER_WISE");
                ht_ErrStatusStateCounty_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatusStateCounty_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatusStateCounty_Wise.Add("@Error_Status", Error_Status);
                ht_ErrStatusStateCounty_Wise.Add("@State", Error_State);
                ht_ErrStatusStateCounty_Wise.Add("@County", County);
                ht_ErrStatusStateCounty_Wise.Add("@User_Id", ErrUserid);
                dt_ErrStatusStateCounty_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStatusStateCounty_Wise);

                chartControl5.DataSource = dt_ErrStatusStateCounty_Wise;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // BAR CHART Error On  STATE and County and User Wise
        private void Bind_Bar_Chart_Error_On_State_AND_County_AND_User_Wise(string Error_State, string County, int ErrUserid)
        {
            try
            {
                //Bar Chart
                chartControl5.DataSource = null;

                Hashtable ht_ErrStateCounty_Wise = new Hashtable();
                DataTable dt_ErrStateCounty_Wise = new DataTable();

                ht_ErrStateCounty_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_COUNTY_USER_WISE");
                ht_ErrStateCounty_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStateCounty_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStateCounty_Wise.Add("@State", Error_State);
                ht_ErrStateCounty_Wise.Add("@County", County);
                ht_ErrStateCounty_Wise.Add("@User_Id", ErrUserid);
                dt_ErrStateCounty_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_ErrStateCounty_Wise);

                chartControl5.DataSource = dt_ErrStateCounty_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_AND_County_AND_User_Wise(Error_State, County, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_AND_County_AND_User_Wise(string Error_State, string County, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_StateCounty_User = new Hashtable();
                DataTable dt_StateCounty_user = new DataTable();
                ht_StateCounty_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_COUNTY_USER_WISE");
                ht_StateCounty_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_StateCounty_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_StateCounty_User.Add("@State", Error_State);
                ht_StateCounty_User.Add("@County", County);
                ht_StateCounty_User.Add("@User_Id", ErrUserid);
                dt_StateCounty_user = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_StateCounty_User);

                chartControl5.DataSource = dt_StateCounty_user;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  TASK ,STATUS AND STATE and county and User  Wise
        private void Bind_Bar_Chart_Error_On_Task_Status_State_AND_County_User_Wise(string Error_Task, string Error_Status, string Error_State, string Error_County,int ErrUserid)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_State_User_Wise = new Hashtable();
                DataTable dt_Errtask_Status_State_User_Wise = new DataTable();

                ht_Errtask_Status_State_User_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_AND_COUNTY_USER_WISE");
                ht_Errtask_Status_State_User_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_State_User_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_State_User_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_Status_State_User_Wise.Add("@Error_Status", Error_Status);
                ht_Errtask_Status_State_User_Wise.Add("@State", Error_State);
                ht_Errtask_Status_State_User_Wise.Add("@County", Error_County);
                ht_Errtask_Status_State_User_Wise.Add("@User_Id", ErrUserid);
                dt_Errtask_Status_State_User_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_State_User_Wise);

                chartControl5.DataSource = dt_Errtask_Status_State_User_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_Status_State_AND_County_User_Wise(Error_Task, Error_Status, Error_State,Error_County, ErrUserid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_Status_State_AND_County_User_Wise(string Error_Task, string Error_Status, string Error_State,string Error_County, int ErrUserid)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State_User_Line = new Hashtable();
                DataTable dt_task_Status_State_User_Line = new DataTable();
                ht_task_Status_State_User_Line.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_AND_COUNTY_USER_WISE");
                ht_task_Status_State_User_Line.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State_User_Line.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State_User_Line.Add("@Error_Task", Error_Task);
                ht_task_Status_State_User_Line.Add("@Error_Status", Error_Status);
                ht_task_Status_State_User_Line.Add("@State", Error_State);
                ht_task_Status_State_User_Line.Add("@County", Error_County);
                ht_task_Status_State_User_Line.Add("@User_Id", ErrUserid);
                dt_task_Status_State_User_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_State_User_Line);

                chartControl5.DataSource = dt_task_Status_State_User_Line;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART Error On  TASK ,STATUS and  STATE AND COUNTY 
        private void Bind_Bar_Chart_Error_On_Task_AND_Status_AND_State_AND_County_Wise(string Error_Task, string Error_Status, string Error_State, string County)
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_State_County_Wise = new Hashtable();
                DataTable dt_Errtask_Status_State_County_Wise = new DataTable();

                ht_Errtask_Status_State_County_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_AND_COUNTY_WISE");
                ht_Errtask_Status_State_County_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_State_County_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_State_County_Wise.Add("@Error_Task", Error_Task);
                ht_Errtask_Status_State_County_Wise.Add("@Error_Status", Error_Status);
                ht_Errtask_Status_State_County_Wise.Add("@State", Error_State);
                ht_Errtask_Status_State_County_Wise.Add("@County", County);
                
                dt_Errtask_Status_State_County_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_Errtask_Status_State_County_Wise);


                chartControl5.DataSource = dt_Errtask_Status_State_County_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_AND_Status_State_County_Wise(Error_Task, Error_Status,Error_State, County);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_AND_Status_State_County_Wise(string Error_Task, string Error_Status, string Error_State, string County)
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State_County_User = new Hashtable();
                DataTable dt_task_Status_State_County_User = new DataTable();
                ht_task_Status_State_County_User.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_AND_COUNTY_WISE");
                ht_task_Status_State_County_User.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State_County_User.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State_County_User.Add("@Error_Task", Error_Task);
                ht_task_Status_State_County_User.Add("@Error_Status", Error_Status);
                ht_task_Status_State_County_User.Add("@State", Error_State);
                ht_task_Status_State_County_User.Add("@County", County);
                
                dt_task_Status_State_County_User = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", ht_task_Status_State_County_User);

                chartControl5.DataSource = dt_task_Status_State_County_User;
                chartControl5.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["%"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["%"].ValueDataMembers[0] = "Error_State_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void chk_All_StateWise_ErrorTask_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_StateWise_Error_Task.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_StateWise_Error_Task.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_All_State_ErrorTask"] as DataGridViewCheckBoxCell);
                checkBox.Value = chk_All_StateWise_ErrorTask.Checked;

            }
        }

        private void grd_StateWise_Error_Task_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_1 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_StateWise_Error_Task.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_All_State_ErrorTask"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_1 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_1 = 0;
                    }
                }
            }
            if (unchk_cnt_1 == 1)
            {
                chk_All_StateWise_ErrorTask.Checked = false;
            }
            else
            {
                chk_All_StateWise_ErrorTask.Checked = true;
            }
        }

        private void chk_All_StateWise_Error_Status_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_StateWise_Error_Status.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_StateWise_Error_Status.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_All_State_ErrorStatus"] as DataGridViewCheckBoxCell);
                checkBox.Value = chk_All_StateWise_Error_Status.Checked;

            }
        }

        private void grd_StateWise_Error_Status_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_2 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_StateWise_Error_Status.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_All_State_ErrorStatus"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_2 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_2 = 0;
                    }
                }
            }
            if (unchk_cnt_2 == 1)
            {
                chk_All_StateWise_Error_Status.Checked = false;
            }
            else
            {
                chk_All_StateWise_Error_Status.Checked = true;
            }
        }

        private void Chk_All_County_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_County.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_County.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_AllCounty"] as DataGridViewCheckBoxCell);
                checkBox.Value = Chk_All_County.Checked;

            }
        }

        private void grd_County_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt_3 = 0;
            //08-02-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_County.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_AllCounty"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt_3 = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt_3 = 0;
                    }
                }
            }
            if (unchk_cnt_3 == 1)
            {
                Chk_All_County.Checked = false;
            }
            else
            {
                Chk_All_County.Checked = true;
            }
        }

        private void chk_All_State_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_State.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_State.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_AllState"] as DataGridViewCheckBoxCell);
                checkBox.Value = chk_All_State.Checked;

            }
        }


        private void grd_State_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grd_State.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void grd_State_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int state_Id = 0;
                if (e.ColumnIndex == 0)
                {
                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    int State_Count; string Single_State = "", State = "";
                    StringBuilder State_Sb = new StringBuilder();
                    State_Count = 0;

                    for (int state = 0; state < grd_State.Rows.Count; state++)
                    {
                        bool is_state = (bool)grd_State[0, state].FormattedValue;
                        if (is_state == true)
                        {
                            State_Count = State_Count + 1;
                            state_Id = int.Parse(grd_State.Rows[state].Cells[1].Value.ToString());

                            if (State_Count == 1)
                            {

                                Single_State = state_Id.ToString();
                                State = Single_State;
                                State_Sb = State_Sb.Append(State);
                            }
                            else
                            {
                                State_Sb = State_Sb.Append("," + state_Id);
                                State = State_Sb.ToString();
                                State_Count++;
                            }
                        }
                    }

                    if (State_Count == 1)
                    {
                        ht.Clear();
                        dt.Clear();
                        ht.Add("@Trans", "SELECT_COUNTY");
                        ht.Add("@State_ID", state_Id);
                        dt = dataaccess.ExecuteSP("Sp_Error_DashBoard_1", ht);
                        grd_County.Rows.Clear();
                        if (dt.Rows.Count > 0)
                        {
                          
                            if (Convert.ToBoolean(grd_State.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            {
                                int row = grd_County.Rows.Count;
                                for (int j = 0; j < dt.Rows.Count; j++, row++)
                                {
                                    grd_County.Rows.Add();
                                    grd_County.Rows[row].Cells[1].Value = dt.Rows[j]["County_ID"].ToString();
                                    grd_County.Rows[row].Cells[2].Value = dt.Rows[j]["County"].ToString();

                                    grd_County[0, row].Value = true;
                                    Chk_All_County.Checked = true;
                                }

                            }
                            else
                            {


                                int row = grd_County.Rows.Count;
                                for (int m = 0; m < dt.Rows.Count; m++, row++)
                                {
                                    grd_County.Rows.Add();

                                    grd_County.Rows[row].Cells[1].Value = dt.Rows[m]["County_ID"].ToString();
                                    grd_County.Rows[row].Cells[2].Value = dt.Rows[m]["County"].ToString();

                                    grd_County[0, row].Value = true;
                                    Chk_All_County.Checked = true;
                                }


                            }

                        }

                    }
                    else if (State_Count > 1)
                    {
                        ht.Clear();
                        dt.Clear();
                        grd_County.Rows.Clear();
                        Chk_All_County.Checked = true;
                        ht.Add("@Trans", "SELECT_STATE_WISE_COUNTY");
                        ht.Add("@State", State);
                        dt = dataaccess.ExecuteSP("Sp_Error_DashBoard_1", ht);
                        if (dt.Rows.Count > 0)
                        {
                            //if (Convert.ToBoolean(grd_State.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            //{

                            for (int state = 0; state < grd_State.Rows.Count; state++)
                            {
                                bool is_state = (bool)grd_State[0, state].FormattedValue;
                                if (is_state == true)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr[0] = 0;
                                    dr[1] = "ALL";
                                    dt.Rows.InsertAt(dr, 0);

                                    int row = grd_County.Rows.Count;
                                    grd_County.Rows.Clear();
                                    for (int k = 0; k < dt.Rows.Count; k++)
                                    {
                                        grd_County.Rows.Add();
                                        grd_County.Rows[0].Cells[2].Value = dt.Rows[k][1].ToString();

                                        grd_County[0, 0].Value = true;
                                        Chk_All_County.Checked = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                for (int s = 0; s < grd_County.Rows.Count; s++)
                                {
                                    if (grd_County.Rows[s].Cells[1].Value.ToString() == dt.Rows[j]["County_ID"].ToString())
                                    {
                                        grd_County.Rows.RemoveAt(s);
                                        Chk_All_County.Checked = false;
                                    }
                                }
                            }

                        }
                    }

                    if (State_Count == 0)
                    {
                        grd_County.Rows.Clear();
                        Chk_All_County.Checked = false;
                    }

                }
            }
        }

        private void btn_StateWise_Submit_Click(object sender, EventArgs e)
        {
                load_Progressbar.Start_progres();

                ErrUserid = int.Parse(ddl_State_ErrorOnUser.SelectedValue.ToString());
                if (txt_State_From_Date.Text != "" && txt_State_To_Date.Text != "")
                {
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    StringBuilder sb3 = new StringBuilder();

                    Order_Task_Count = 0; Order_Status_Count = 0; Error_OnState_Count = 0; Error_County_Count = 0;
                    for (int error_task = 0; error_task < grd_StateWise_Error_Task.Rows.Count; error_task++)
                    {

                        bool is_task = (bool)grd_StateWise_Error_Task[0, error_task].FormattedValue;
                        if (is_task == true)
                        {
                            Order_Task_Count = Order_Task_Count + 1;
                            errortask_id = int.Parse(grd_StateWise_Error_Task.Rows[error_task].Cells[2].Value.ToString());

                            if (Order_Task_Count == 1)
                            {
                               
                                Single_Order_Task = errortask_id.ToString();
                                Order_Task = Single_Order_Task;
                                sb = sb.Append(Order_Task);
                            }
                            else
                            {
                                sb = sb.Append("," + errortask_id);
                                Order_Task = sb.ToString();
                                Order_Task_Count++;
                            }
                        }
                    }
                    for (int error_Status = 0; error_Status < grd_StateWise_Error_Status.Rows.Count; error_Status++)
                    {
                        bool is_Order_Status = (bool)grd_StateWise_Error_Status[0, error_Status].FormattedValue;
                        if (is_Order_Status == true)
                        {
                            Order_Status_Count = Order_Status_Count + 1;
                            error_Status_id = int.Parse(grd_StateWise_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                            if (Order_Status_Count == 1)
                            {
                                Order_Status = error_Status_id.ToString();
                                sb1 = sb1.Append(Order_Status);
                            }
                            else
                            {
                                //Order_Status = Single_Order_Status + "," + error_Status_id + ",";
                                sb1 = sb1.Append("," + error_Status_id);
                                Order_Status = sb1.ToString();
                                Order_Status_Count++;
                            }
                        }
                    }
                    for (int errorState = 0; errorState < grd_State.Rows.Count; errorState++)
                    {

                        bool is_errorState = (bool)grd_State[0, errorState].FormattedValue;
                        if (is_errorState == true)
                        {
                            //Error_OnState_Count = errorState + 1;
                            Error_OnState_Count = Error_OnState_Count + 1;
                            stateId = int.Parse(grd_State.Rows[errorState].Cells[1].Value.ToString());
                            if (Error_OnState_Count == 1)
                            {
                                Error_State = stateId.ToString();
                                sb2 = sb2.Append(Error_State);
                            }
                            else
                            {
                                sb2 = sb2.Append("," + stateId);
                                Error_State = sb2.ToString();
                                Error_OnState_Count++;
                            }
                        }

                    }
                    if (Error_OnState_Count == 1)
                    {
                        for (int error_County = 0; error_County < grd_County.Rows.Count; error_County++)
                        {

                            bool is_error_County = (bool)grd_County[0, error_County].FormattedValue;
                            if (is_error_County == true)
                            {
                                //Error_County_Count = error_County + 1;
                                Error_County_Count = Error_County_Count + 1;
                                if (grd_County.Rows[error_County].Cells[1].Value == null)
                                {
                                    County_Id = 0;
                                }
                                else
                                {
                                    County_Id = int.Parse(grd_County.Rows[error_County].Cells[1].Value.ToString());
                                }

                                if (Error_County_Count == 0)
                                {
                                    Error_County = County_Id.ToString();
                                    Error_County_Count = 0;
                                }
                                else if (Error_County_Count == 1)
                                {
                                    Error_County = County_Id.ToString();
                                    sb3 = sb3.Append(Error_County);
                                }
                                else if (Error_County_Count >= 1)
                                {
                                    sb3 = sb3.Append("," + County_Id);
                                    Error_County = sb3.ToString();
                                    Error_County_Count++;
                                }
                            }

                        }
                    }
                    else
                    {
                      
                        Error_County_Count = 0;
                    }


                    if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnState_Count == 0 && Error_County_Count == 0 && ErrUserid==0)
                    {
                        Bind_Bar_Chart_Error_On_State_All_Task();
                        Grid_ErrorOnState_All_Task_Wise_Count();
                    }

                    // task wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnState_Count == 0 && Error_County_Count == 0 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_State_Task_Wise(Order_Task);
                        Grid_ErrorOnState_Task_Wise_Count(Order_Task);
                    }
                    // status wise
                    else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnState_Count == 0 && Error_County_Count == 0 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_State_Status_Wise(Order_Status);
                        Grid_ErrorOnState_Status_Wise_Count(Order_Status);
                    }
                    // State wise
                    else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnState_Count > 1 && Error_County_Count == 0 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_State_Wise(Error_State);
                        Grid_ErrorOnState_State_Wise_Count(Error_State);
                    }

                    // task and status wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnState_Count == 0 && Error_County_Count==0 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_State_Task_AND_Status_Wise(Order_Task,Order_Status);
                        Grid_ErrorOnState_Task_AND_Status_Wise_Count(Order_Task, Order_Status);
                    }
                    // task and  State wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnState_Count >= 1 && Error_County_Count == 0 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_State_Task_AND_State_Wise(Order_Task, Error_State);
                        Grid_ErrorOnState_Task_AND_State_Wise_Count(Order_Task, Error_State);
                    }
                    // Status and State wise
                    else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnState_Count >= 1 && Error_County_Count == 0 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_Status_AND_State_Wise(Order_Status, Error_State);
                        Grid_ErrorOnState_Status_AND_State_Wise_Count(Order_Status, Error_State);
                        
                    }

                    // TASK , STATUS ,State  wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnState_Count >= 1 && Error_County_Count == 0 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_Task_Status_State_Wise(Order_Task, Order_Status, Error_State);
                        Grid_ErrorOnState_Task_Status_AND_State_Wise_Count(Order_Task,Order_Status, Error_State);
                        
                        
                    }
                    // Task, single State and  County
                    else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnState_Count >= 1 && Error_County_Count >= 1 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_Task_State_County_Wise(Order_Task, Error_State, Error_County);
                        Grid_ErrorOnState_Task_AND_State_AND_County_Wise_Count(Order_Task, Error_State, Error_County);
                       
                    }

                        // status, State and County
                    else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnState_Count >= 1 && Error_County_Count >= 1 && ErrUserid == 0)
                    {

                        Bind_Bar_Chart_Error_On_Status_AND_State_AND_County_Wise(Order_Status, Error_State, Error_County);
                        Grid_ErrorOnState_Status_AND_State_AND_County_Wise_Count(Order_Status, Error_State, Error_County);
                        
                    }
                    // State and County  (County wise)
                    else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnState_Count == 1 && Error_County_Count >= 1 && ErrUserid == 0)
                    {
                        Bind_Bar_Chart_Error_On_State_AND_County_Wise(Error_State, Error_County);
                        Grid_ErrorOnState_State_AND_County_Wise_Count(Error_State, Error_County);
                       
                    }

                    // USER wise
                    else if (Order_Task_Count ==0 && Order_Status_Count == 0 && Error_OnState_Count == 0 && Error_County_Count == 0 && ErrUserid > 0)
                    {
                        Bind_Bar_Chart_ErrorOnUser_Wise(ErrUserid);
                        Grid_ErrorOnState_User_Wise_Count(ErrUserid);
                    }

                    // task and User wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnState_Count == 0 && Error_County_Count == 0 && ErrUserid > 0)
                    {
                        Bind_Bar_Chart_ErrorOnTask_AND_User_Wise(Order_Task, ErrUserid);
                        Grid_ErrorOnState_Task_AND_User_Wise_Count(Order_Task,ErrUserid);
                    }

                    // status and User wise
                    else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnState_Count == 0 && Error_County_Count == 0 && ErrUserid > 0)
                    {
                         Bind_Bar_Chart_ErrorOnStatus_AND_User_Wise(Order_Status,ErrUserid);
                         Grid_ErrorOnState_Status_AND_User_Wise_Count(Order_Status, ErrUserid);
                    }

                     // State and User wise
                    else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnState_Count >= 1 && Error_County_Count == 0 && ErrUserid > 0)
                    {
                        Bind_Bar_Chart_Error_On_State_AND_User_Wise(Error_State, ErrUserid);
                        Grid_ErrorOnState_State_AND_User_Wise_Count(Error_State, ErrUserid);
                    }
                    // task and status  and USer wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnState_Count == 0 && Error_County_Count == 0 && ErrUserid > 0)
                    {
                        Bind_Bar_Chart_Error_On_State_Task_AND_Status_AND_User_Wise(Order_Task, Order_Status, ErrUserid);
                        Grid_ErrorOnState_Task_AND_Status_AND_User_Wise_Count(Order_Task, Order_Status, ErrUserid);
                    }
                    // task and  State  and USer wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnState_Count >= 1 && Error_County_Count == 0 && ErrUserid > 0)
                    {
                        Bind_Bar_Chart_Error_On_Task_AND_State_AND_User_Wise(Order_Task, Error_State, ErrUserid);

                        Grid_ErrorOnState_Task_AND_State_AND_User_Wise_Count(Order_Task, Error_State, ErrUserid);
                    }

                     // Status and State and USer wise
                    else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnState_Count >= 1 && Error_County_Count == 0 && ErrUserid > 0)
                    {

                        Bind_Bar_Chart_Error_On_Status_AND_State_AND_User_Wise(Order_Status, Error_State, ErrUserid);
                        Grid_ErrorOnState_Status_AND_State_AND_User_Wise_Count(Order_Status, Error_State, ErrUserid);
                    }
                    // TASK , STATUS ,State  and USer wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count >= 1 && Error_OnState_Count >= 1 && Error_County_Count == 0 && ErrUserid > 0)
                    {
                         Bind_Bar_Chart_Error_On_Task_Status_State_AND_User_Wise(Order_Task, Order_Status, Error_State, ErrUserid);
                         Grid_ErrorOnState_Task_AND_Status_AND_State_AND_User_Wise_Count(Order_Task,Order_Status, Error_State, ErrUserid);

                    }
                    // Task, single State and  County and User wise
                    else if (Order_Task_Count >= 1 && Order_Status_Count == 0 && Error_OnState_Count >= 1 && Error_County_Count >= 1 && ErrUserid > 0)
                    {
                        Bind_Bar_Chart_Error_On_Task_State_County_AND_User_Wise(Order_Task, Error_State, Error_County, ErrUserid);
                        Grid_ErrorOnState_Task_AND_State_AND_County_AND_User_Wise_Count(Order_Task,Error_State,Error_County, ErrUserid);
                    }

                    // status, State and County and User wise
                    else if (Order_Task_Count == 0 && Order_Status_Count >= 1 && Error_OnState_Count == 1 && Error_County_Count >= 1 && ErrUserid > 0)
                    {

                        Bind_Bar_Chart_Error_On_Status_AND_State_AND_County_AND_User_Wise(Order_Status, Error_State, Error_County, ErrUserid);
                        Grid_ErrorOnState_Status_AND_State_AND_County_AND_User_Wise_Count(Order_Status, Error_State, Error_County, ErrUserid);

                    }
                    // TASK AND status, State and County 
                    else if (Order_Task_Count >= 0 && Order_Status_Count >= 1 && Error_OnState_Count == 1 && Error_County_Count >= 1 && ErrUserid == 0)
                    {

                        Bind_Bar_Chart_Error_On_Task_AND_Status_AND_State_AND_County_Wise(Order_Task, Order_Status, Error_State, Error_County);
                        Grid_ErrorOnState_Task_AND_Status_AND_State_AND_County_Wise_Count(Order_Task,Order_Status, Error_State, Error_County);

                    }


                    // State and County and User wise (County wise)
                    else if (Order_Task_Count == 0 && Order_Status_Count == 0 && Error_OnState_Count == 1 && Error_County_Count >= 1 && ErrUserid > 0)
                    {
                         Bind_Bar_Chart_Error_On_State_AND_County_AND_User_Wise(Error_State, Error_County,ErrUserid);
                         Grid_ErrorOnState_State_AND_County_AND_User_Wise_Count(Error_State, Error_County,ErrUserid);

                    }

                     // Task,status, single State and  County and User wise  (County Wise)
                    else if (Order_Task_Count >= 1 && Order_Status_Count >=1 && Error_OnState_Count == 1 && Error_County_Count >= 1 && ErrUserid > 0)
                    {
                        Bind_Bar_Chart_Error_On_Task_Status_State_AND_County_User_Wise(Order_Task, Order_Status, Error_State, Error_County, ErrUserid);
                        Grid_ErrorOnState_Task_AND_Status_AND_State_AND_County_AND_User_Wise_Count(Order_Task,Order_Status,Error_State, Error_County, ErrUserid);
                    }


                    

                   
                }
                else
                {
                    MessageBox.Show("Select Date");
                    txt_State_From_Date.Select();
                }

        }

        private void chartControl5_MouseClick(object sender, MouseEventArgs e)
        {
            Type_Name = ""; Error_Type_Name = ""; Order_Task = ""; Order_Status="";
            Order_Task_Count = 0; Order_Status_Count = 0; Error_OnState_Count = 0; Error_County_Count = 0;
            load_Progressbar.Start_progres();
            //Type_Name = "";
            StringBuilder sb = new StringBuilder();
            StringBuilder sb_1 = new StringBuilder();
            StringBuilder sb_2 = new StringBuilder();
            StringBuilder sb_3 = new StringBuilder();

            ChartHitInfo hi = chartControl5.CalcHitInfo(e.X, e.Y);
            SeriesPoint point_1 = hi.SeriesPoint;
            if (point_1 != null)
            {
                Error_User_Id = int.Parse(ddl_State_ErrorOnUser.SelectedValue.ToString());
                //Error_On_UserName_argument = "Argument: " + point.Argument.ToString();
               // Error_OnState = point.Argument.ToString();
               // Error_Type_Name = point.Argument.ToString();

                Error_OnState = point_1.Argument.ToString();
                Error_State_Count = Convert.ToInt32(point_1.Values[0]).ToString();
                values = "Value(s): " + Error_State_Count;
                if (Error_State_Count.Length >= 1)
                {
                    for (int i = 0; i <= Error_State_Count.Length - 1; i++)
                    {
                        values = values + ", " + Error_State_Count[i].ToString();
                    }
                }
            }
          
           
            StringBuilder sb_ErrorClient = new StringBuilder();
            for (int errorState = 0; errorState < grd_State.Rows.Count; errorState++)
            {
                bool is_errorState = (bool)grd_State[0, errorState].FormattedValue;
                if (is_errorState == true)
                {
                    Error_OnState_Count ++;
                    stateId = int.Parse(grd_State.Rows[errorState].Cells[1].Value.ToString());
                    if (Error_OnState_Count == 1)
                    {
                        Error_Type_Name = stateId.ToString();
                        sb_ErrorClient = sb_ErrorClient.Append(Error_Type_Name);
                    }
                    else
                    {
                        sb_ErrorClient = sb_ErrorClient.Append("," + stateId);
                        Error_Type_Name = sb_ErrorClient.ToString();
                        Error_OnState_Count++;
                    }
                    //stateId= Error_Type_Name;
                }

            }

            Hashtable htclient = new Hashtable();
            DataTable dtclient = new DataTable();

            htclient.Add("@Trans", "GET_ERROR_STATE_NAME");
            htclient.Add("@State_Name", Error_OnState);
            dtclient = dataaccess.ExecuteSP("Sp_Error_Dashboard", htclient);
            if (dtclient.Rows.Count > 0)
            {
                Error_Type_Name = dtclient.Rows[0]["State_ID"].ToString();

            }
            Error_Tab_Page = "Error_On_State";
            Type_Name = "";

            if (Error_OnState_Count > 1)
            {
                Error_Tab_Page = "Error_On_State";
                Type_Name = "";

              
            }
            else if (Error_OnState_Count == 1)
            {
                Error_Tab_Page = "Error_On_County";
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "GET_ERROR_COUNTY_NAME");
                ht.Add("@State_ID", stateId);
                ht.Add("@County_Name", Error_OnState);

                dt = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht);
                if (dt.Rows.Count > 0)
                {
                   // Error_Type_Name = dt.Rows[0]["State_ID"].ToString();
                    State_Name= dt.Rows[0]["State_ID"].ToString();
                    Error_Type_Name = State_Name;
                    Type_Name = dt.Rows[0]["County_ID"].ToString();
                }
               
            }

          
           

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            //Order_Task_Count = 0; Order_Status_Count = 0; 
            for (int error_task = 0; error_task < grd_StateWise_Error_Task.Rows.Count; error_task++)
            {

                bool is_task = (bool)grd_StateWise_Error_Task[0, error_task].FormattedValue;
                if (is_task == true)
                {
                    Order_Task_Count = Order_Task_Count + 1;
                    errortask_id = int.Parse(grd_StateWise_Error_Task.Rows[error_task].Cells[2].Value.ToString());

                    if (Order_Task_Count == 1)
                    {
                        
                        Single_Order_Task = errortask_id.ToString();
                        Order_Task = Single_Order_Task;
                        sb = sb.Append(Order_Task);
                    }
                    else
                    {
                        sb = sb.Append("," + errortask_id);
                        Order_Task = sb.ToString();
                        Order_Task_Count++;
                    }
                }
            }
            for (int error_Status = 0; error_Status < grd_StateWise_Error_Status.Rows.Count; error_Status++)
            {
                bool is_Order_Status = (bool)grd_StateWise_Error_Status[0, error_Status].FormattedValue;
                if (is_Order_Status == true)
                {
                    Order_Status_Count = Order_Status_Count + 1;
                    error_Status_id = int.Parse(grd_StateWise_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                    if (Order_Status_Count == 1)
                    {
                        Order_Status = error_Status_id.ToString();
                        sb1 = sb1.Append(Order_Status);
                    }
                    else
                    {
                        sb1 = sb1.Append("," + error_Status_id);
                        Order_Status = sb1.ToString();
                        Order_Status_Count++;
                    }
                }
            }


            if (tabControl1.SelectedIndex == 4)
            {
                Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                errordetails.Show();
            }

        }


        private void Grid_ErrorOnState_All_Task_Count_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Error_Tab_Page = "Error_On_State";
                Err_Userid = int.Parse(ddl_State_ErrorOnUser.SelectedValue.ToString());
                Error_User_Id = Err_Userid;
                Error_Type_Name = ""; Type_Name = "";
                Order_Task = ""; Order_Status = "";
                StringBuilder sb = new StringBuilder();
                StringBuilder sb_1 = new StringBuilder();
                StringBuilder sb_2 = new StringBuilder();
                StringBuilder sb_3 = new StringBuilder();
                Order_Task = ""; Order_Status = ""; Error_Tab = "";
                Order_Task_Count = 0; Order_Status_Count = 0; Error_OnState_Count = 0; Error_County_Count = 0;
          
                for (int error_Status = 0; error_Status < grd_StateWise_Error_Status.Rows.Count; error_Status++)
                {
                    bool is_Order_Status = (bool)grd_StateWise_Error_Status[0, error_Status].FormattedValue;
                    if (is_Order_Status == true)
                    {
                        Order_Status_Count++;
                        error_Status_id = int.Parse(grd_StateWise_Error_Status.Rows[error_Status].Cells[2].Value.ToString());
                        if (Order_Status_Count == 1)
                        {
                            Order_Status = error_Status_id.ToString();
                            sb_1 = sb_1.Append(Order_Status);
                        }
                        else
                        {
                            sb_1 = sb_1.Append("," + error_Status_id);
                            Order_Status = sb_1.ToString();
                            Order_Status_Count++;
                        }
                    }
                }

                for (int errorState = 0; errorState < grd_State.Rows.Count; errorState++)
                {

                    bool is_errorState = (bool)grd_State[0, errorState].FormattedValue;
                    if (is_errorState == true)
                    {
                        Error_OnState_Count++;
                        stateId = int.Parse(grd_State.Rows[errorState].Cells[1].Value.ToString());
                        if (Error_OnState_Count == 1)
                        {
                            Error_Type_Name = stateId.ToString();
                            sb_2 = sb_2.Append(Error_Type_Name);
                        }
                        else
                        {
                            sb_2 = sb_2.Append("," + stateId);
                            Error_Type_Name = sb_2.ToString();
                            Error_OnState_Count++;
                        }

                    }

                }

                if (Error_OnState_Count == 1)
                {
                    Error_Tab_Page = "Error_On_County";

                    // State and County wise
                    for (int error_county = 0; error_county < grd_County.Rows.Count; error_county++)
                    {

                        bool is_error_county = (bool)grd_County[0, error_county].FormattedValue;
                        if (is_error_county == true)
                        {

                            Error_County_Count++;
                            County_Id = int.Parse(grd_County.Rows[error_county].Cells[1].Value.ToString());

                            if (Error_County_Count == 0)
                            {
                                Type_Name = County_Id.ToString();
                                Error_County_Count = 0;
                            }
                            if (Error_County_Count == 1)
                            {
                                Type_Name = County_Id.ToString();
                                sb_3 = sb_3.Append(Type_Name);
                            }
                            else if (Error_County_Count >= 1)
                            {
                                sb_3 = sb_3.Append("," + County_Id);
                                Type_Name = sb_3.ToString();
                                Error_County_Count++;
                            }
                        }

                    }

                }


                if (e.ColumnIndex == 0)
                {
                    if (Grid_ErrorOnState_All_Task_Count.Rows[0].Cells[0].Value.ToString() != "0")
                    {
                        Order_Task = "2";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    if (Grid_ErrorOnState_All_Task_Count.Rows[0].Cells[1].Value.ToString() != "0")
                    {
                        Order_Task = "3";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if (Grid_ErrorOnState_All_Task_Count.Rows[0].Cells[2].Value.ToString() != "0")
                    {
                        Order_Task = "4";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    if (Grid_ErrorOnState_All_Task_Count.Rows[0].Cells[3].Value.ToString() != "0")
                    {
                        Order_Task = "7";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (Grid_ErrorOnState_All_Task_Count.Rows[0].Cells[4].Value.ToString() != "0")
                    {
                        Order_Task = "23";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    if (Grid_ErrorOnState_All_Task_Count.Rows[0].Cells[5].Value.ToString() != "0")
                    {
                        Order_Task = "24";
                        Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_Type_Name, Type_Name, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page, Error_User_Id, Order_Task, Order_Status);
                        errordetails.Show();
                    }
                    else
                    {
                        MessageBox.Show("No record found");
                    }
                }

            }
        }




       //-------------16 juyl-----------------
        
        private void Bind_UserName_ErrorTab(ComboBox ddl_ErrorOnUser)
        {
            Hashtable ht_Param = new Hashtable();
            DataTable dt_Param = new DataTable();

            ht_Param.Add("@Trans", "SELECT");
            dt_Param = dataaccess.ExecuteSP("Sp_User", ht_Param);
            DataRow dr = dt_Param.NewRow();
            dr[0] = 0;
            dr[4] = "ALL";
            dt_Param.Rows.InsertAt(dr, 0);
            ddl_ErrorOnUser.DataSource = dt_Param;
            ddl_ErrorOnUser.DisplayMember = "User_Name";
            ddl_ErrorOnUser.ValueMember = "User_id";
        }

        private void Bind_UserName_ErrorDesc(ComboBox ddl_Err_desc_ErrorOnUser)
        {
            Hashtable ht_Param = new Hashtable();
            DataTable dt_Param = new DataTable();

            ht_Param.Add("@Trans", "SELECT");
            dt_Param = dataaccess.ExecuteSP("Sp_User", ht_Param);
            DataRow dr = dt_Param.NewRow();
            dr[0] = 0;
            dr[4] = "ALL";
            dt_Param.Rows.InsertAt(dr, 0);
            ddl_Err_desc_ErrorOnUser.DataSource = dt_Param;
            ddl_Err_desc_ErrorOnUser.DisplayMember = "User_Name";
            ddl_Err_desc_ErrorOnUser.ValueMember = "User_id";
        }
        private void Bind_UserName_ErrorClient(ComboBox ddl_ErrClient_ErrorOnUser)
        {
            Hashtable ht_Param = new Hashtable();
            DataTable dt_Param = new DataTable();

            ht_Param.Add("@Trans", "SELECT");
            dt_Param = dataaccess.ExecuteSP("Sp_User", ht_Param);
            DataRow dr = dt_Param.NewRow();
            dr[0] = 0;
            dr[4] = "ALL";
            dt_Param.Rows.InsertAt(dr, 0);
            ddl_ErrClient_ErrorOnUser.DataSource = dt_Param;
            ddl_ErrClient_ErrorOnUser.DisplayMember = "User_Name";
            ddl_ErrClient_ErrorOnUser.ValueMember = "User_id";
        }
        private void Bind_UserName_ErrorState(ComboBox ddl_State_ErrorOnUser)
        {
            Hashtable ht_Param = new Hashtable();
            DataTable dt_Param = new DataTable();

            ht_Param.Add("@Trans", "SELECT");
            dt_Param = dataaccess.ExecuteSP("Sp_User", ht_Param);
            DataRow dr = dt_Param.NewRow();
            dr[0] = 0;
            dr[4] = "ALL";
            dt_Param.Rows.InsertAt(dr, 0);
            ddl_State_ErrorOnUser.DataSource = dt_Param;
            ddl_State_ErrorOnUser.DisplayMember = "User_Name";
            ddl_State_ErrorOnUser.ValueMember = "User_id";
        }




        // --------Error Filed Count------------------

        private void Grid_ErrorField_All_Task_Wise_Count()
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ALLTASK_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid_Error_field_All_Task_Count.DataSource = null;
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);
        }

        private void Grid_Error_User_Wise_Count(int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORUSER_WISE_COUNT");
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid_Error_field_All_Task_Count.DataSource = null;
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_Error_Task_Wise_Count(string Order_Task)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TASK_WISE_COUNT");
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                // Grid_Error_field_All_Task_Count.DataSource = null;
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_Error_Status_Wise_Count(string Order_Status)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_STATUS_WISE_COUNT");
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_Error_Field_Wise_Count(string ErrorField)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_FIELD_WISE_COUNT");
            htlivecount.Add("@Error_description", ErrorField);
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorTask_AND_ErrorUser_Wise_Count(string Order_Task, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();


            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORTASK_AND_USER_WISE_COUNT");
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid_Error_field_All_Task_Count.DataSource = null;
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorTask_AND_ErrorStatus_Wise_Count(string Order_Task, string Order_Status)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERORRTASK_ERRORSTATUS_WISE_COUNT");
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorTask_AND_ErrorStatus_ErrorUser_Wise_Count(string Order_Task, string Order_Status, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORTASK_ERRORSTATUS_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Chart_User_Id", Err_Userid);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;

                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorTask_AND_ErrorField_Wise_Count(string Order_Task, string ErrorField)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TASK_ERROR_FIELD_WISE_COUNT");
            htlivecount.Add("@Error_description", ErrorField);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorTask_ErrorField_ErrorUser_Wise_Count(string Order_Task, string ErrorField, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TASK_ERROR_FIELD_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_description", ErrorField);
            htlivecount.Add("@Chart_User_Id", Err_Userid);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorTask_AND_ErrorStatus_AND_ErrorField_Wise_Count(string Order_Task, string Order_Status, string ErrorField)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TASK_ERROR_STATUS_ERROR_FILED_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Error_description", ErrorField);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);
            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorTask_AND_Error_Status_AND_ErrorField_User_Wise_Count(string Order_Task, string Order_Status, string ErrorField, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORTASK_ERRORSTATUS_ERROR_FIELD_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_description", ErrorField);
            htlivecount.Add("@Chart_User_Id", Err_Userid);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorStatus_AND_ErrorUser_Wise_Count(string Order_Status, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORSTATUS_AND_ERROR_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_Status", Order_Status);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid_Error_field_All_Task_Count.DataSource = null;
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }

        private void Grid_ErrorStaus_AND_ErrorField_Wise_Count(string Order_Status, string ErrorField)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_STATUS_ERROR_FIELD_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_description", ErrorField);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);

        }

        private void Grid_ErrorStatus_AND_ErrorField_User_Wise_Count(string Order_Status, string ErrorField, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_STATUS_ERROR_FIELD_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_description", ErrorField);
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);

        }

        private void Grid_ErrorFiled_AND_ErrorUser_Wise_Count(string ErrorField, int Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_Error_field_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_FILED_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
            htlivecount.Add("@Error_description", ErrorField);
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_Error_field_All_Task_Count.AutoGenerateColumns = false;
                Grid_Error_field_All_Task_Count.ColumnCount = 6;


                Grid_Error_field_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_Error_field_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_Error_field_All_Task_Count.Columns[0].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_Error_field_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_Error_field_All_Task_Count.Columns[1].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_Error_field_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_Error_field_All_Task_Count.Columns[2].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_Error_field_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_Error_field_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_Error_field_All_Task_Count.Columns[3].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_Error_field_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_Error_field_All_Task_Count.Columns[4].Width = 80;

                Grid_Error_field_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_Error_field_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_Error_field_All_Task_Count.Columns[5].Width = 80;
                Grid_Error_field_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }


        // --------Error On User Count------------------

        private void GridErrorOnUser_All_Task_Wise_Count()
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ALL_ERROR_TASK_WISECOUNT");
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid_ErrorOnUser_All_Task_Count.DataSource = null;
                Grid_ErrorOnUser_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnUser_All_Task_Count);
        }

        private void GridErrorOnUser_User_Wise_Count(string Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONUSER_WISECOUNT");
            htlivecount.Add("@Chart_User_Id", Err_Userid);
           // htlivecount.Add("@Error_On_User_Id", Err_Userid);
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {

                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid_ErrorOnUser_All_Task_Count.DataSource = null;
                Grid_ErrorOnUser_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnUser_All_Task_Count);


        }

        private void GridErrorOnUser_ErrorTask_Wise_Count(string Order_Task)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TASK_WISECOUNT");
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                // Grid_ErrorOnUser_All_Task_Count.DataSource = null;
                Grid_ErrorOnUser_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnUser_All_Task_Count);


        }

        private void GridErrorOnUser_ErrorStatus_Wise_Count(string Order_Status)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_STATUS_WISECOUNT");
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnUser_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnUser_All_Task_Count);


        }

        private void GridErrorOnUser_ErrorTask_AND_ErrorUser_Wise_Count(string Order_Task, string Err_Userid)
        {
          
            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_TASK_AND_ERROR_ON_USER_WISECOUNT");
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            htlivecount.Add("@Error_Task", Order_Task);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                //Grid_ErrorOnUser_All_Task_Count.DataSource = null;
                Grid_ErrorOnUser_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnUser_All_Task_Count);


        }

        private void GridErrorOnUser_ErrorTask_AND_ErrorStatus_Wise_Count(string Order_Task, string Order_Status)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERORR_TASK_AND_ERROR_STATUS_WISECOUNT");
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnUser_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnUser_All_Task_Count);


        }

        private void GridErrorOnUser_ErrorStatus_AND_Error_On_User_Wise_Count(string Order_Status, string Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_STATUS_AND_ERROR_ON_USER_WISECOUNT");
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Chart_User_Id", Err_Userid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnUser_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnUser_All_Task_Count);


        }

        private void GridErrorOnUser_ErrorTask_AND_ErrorStatus_ErrorUser_Wise_Count(string Order_Task, string Order_Status, string Err_Userid)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            htlivecount.Clear();
            dtlivecount.Clear();
            Grid_ErrorOnUser_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERORR_TASK_AND_ERROR_STATUS_AND_ERROR_ON_USER_WISECOUNT");
            htlivecount.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Chart_User_Id", Err_Userid);

            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnUser_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnUser_All_Task_Count.ColumnCount = 6;


                Grid_ErrorOnUser_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnUser_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnUser_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnUser_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnUser_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnUser_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnUser_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_Error_field_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_Error_field_All_Task_Count);


        }


     



       /// -------------------  State WISE COUNT--------

         // Error On State All Task Wise
        private void Grid_ErrorOnState_All_Task_Wise_Count()
        {
         
                htlivecount.Clear();
                dtlivecount.Clear();

                Grid_ErrorOnState_All_Task_Count.DataSource = null;
                htlivecount.Add("@Trans", "ERROR_ON_STATE_ALLTASK_WISE_COUNT");
                htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
                htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
                dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

                if (dtlivecount.Rows.Count > 0)
                {
                    Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                    Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                    Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                    Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                    Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                    Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                    Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                    Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                    Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                    Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                    Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                    Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                    Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                    Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                    Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                    Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                    Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                    Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                    Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                    Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                    Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                    Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                    Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                    Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                    Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                    Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                    Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
                }
                else
                {
                    Grid_ErrorOnState_All_Task_Count.Rows.Clear();
                }
                ArrangeGrid(Grid_ErrorOnState_All_Task_Count);
           
        }

        //  Task Wise
        private void Grid_ErrorOnState_Task_Wise_Count(string Order_Task)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_ERROR_TASK_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        //  Status Wise
        private void Grid_ErrorOnState_Status_Wise_Count(string Order_Status)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_ERROR_STATUS_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        //  STate Wise
        private void Grid_ErrorOnState_State_Wise_Count(string Error_State)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_ERROR_STATE_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@State", Error_State);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }


        //  task and Status Wise
        private void Grid_ErrorOnState_Task_AND_Status_Wise_Count(string Order_Task,string Order_Status)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_ERRORTASK_AND_ERRORSTATUS_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }


        //  task and State Wise
        private void Grid_ErrorOnState_Task_AND_State_Wise_Count(string Order_Task, string Error_State)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_ERROR_TASK_AND_STATE_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@State", Error_State);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        // Status  and  State a Wise
        private void Grid_ErrorOnState_Status_AND_State_Wise_Count(string Order_Status, string Error_State)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_ERRORS_TATUS_AND_ERROR_STATE_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

         // Task and Status  and  State a Wise
        private void Grid_ErrorOnState_Task_Status_AND_State_Wise_Count(string Order_Task,string Order_Status, string Error_State)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_ERROR_TASK_AND_STATUS_AND_ERROR_STATE_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }
        
        // Task and  State and County Wise

        private void Grid_ErrorOnState_Task_AND_State_AND_County_Wise_Count(string Order_Task,string Error_State, string Error_County)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_ERROR_TASK_AND_ERROR_STATE_AND_COUNTY_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        //  (Order_Status, Error_State, Error_County);

        private void Grid_ErrorOnState_Status_AND_State_AND_County_Wise_Count(string Order_Status, string Error_State, string Error_County)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_ERROR_STATUS_AND_ERROR_STATE_AND_COUNTY_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        //(Error_State, Error_County);

        private void Grid_ErrorOnState_State_AND_County_Wise_Count(string Error_State, string Error_County)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_ERROR_STATE_AND_COUNTY_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        // Err User  wise
         private void Grid_ErrorOnState_User_Wise_Count(int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }
          

         // task and Err User  wise
        private void Grid_ErrorOnState_Task_AND_User_Wise_Count(string Order_Task,int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_TASK_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        // STATUS  and Err User  wise
        private void Grid_ErrorOnState_Status_AND_User_Wise_Count(string Order_Status, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_STATUS_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        // STATE  and Err User  wise
        private void Grid_ErrorOnState_State_AND_User_Wise_Count(string Error_State, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_STATE_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        //task and  STATUS  and Err User  wise
        private void Grid_ErrorOnState_Task_AND_Status_AND_User_Wise_Count(string Order_Task, string Order_Status, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERROR_ON_STATE_TASK_AND_STATUS_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        //task and  STATE  and Err User  wise
        private void Grid_ErrorOnState_Task_AND_State_AND_User_Wise_Count(string Order_Task, string Error_State, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_TASK_AND_STATE_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

       
        // STATUS ,  Error_State and Err User  wise
        private void Grid_ErrorOnState_Status_AND_State_AND_User_Wise_Count(string Order_Status, string Error_State, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_STATUS_AND_STATE_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        // TASK AND STATUS ,  Error_State and Err User  wise
        private void Grid_ErrorOnState_Task_AND_Status_AND_State_AND_User_Wise_Count(string Order_Task, string Order_Status, string Error_State, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_TASK_AND_STATUS_AND_STATE_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        // TASK AND Error_State and COUNTY and  Err User  wise
        private void Grid_ErrorOnState_Task_AND_State_AND_County_AND_User_Wise_Count(string Order_Task, string Error_State,string Error_County, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_TASK_AND_STATE_AND_COUNTY_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

      
        // STATUS AND Error_State and COUNTY and  Err User  wise
        private void Grid_ErrorOnState_Status_AND_State_AND_County_AND_User_Wise_Count(string Order_Status, string Error_State, string Error_County, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_STATUS_AND_STATE_AND_COUNTY_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }
      

        // Error_State and COUNTY and  Err User  wise
        private void Grid_ErrorOnState_State_AND_County_AND_User_Wise_Count(string Error_State, string Error_County, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_STATE_AND_COUNTY_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        // STATUS AND Error_State and COUNTY and  Err User  wise
        private void Grid_ErrorOnState_Task_AND_Status_AND_State_AND_County_AND_User_Wise_Count(string Order_Task,string Order_Status, string Error_State, string Error_County, int ErrUserid)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_TASK_AND_STATUS_AND_STATE_AND_COUNTY_AND_USER_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            htlivecount.Add("@Chart_User_Id", ErrUserid);
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        private void Grid_ErrorClient_All_Task_Count_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        // Task and STATUS AND Error_State and COUNTY 
        private void Grid_ErrorOnState_Task_AND_Status_AND_State_AND_County_Wise_Count(string Order_Task, string Order_Status, string Error_State, string Error_County)
        {

            htlivecount.Clear();
            dtlivecount.Clear();

            Grid_ErrorOnState_All_Task_Count.DataSource = null;
            htlivecount.Add("@Trans", "ERRORONSTATE_TASK_AND_STATUS_AND_STATE_AND_COUNTY_WISE_COUNT");
            htlivecount.Add("@Error_From_Date", txt_State_From_Date.Text);
            htlivecount.Add("@Error_To_Date", txt_State_To_Date.Text);
            htlivecount.Add("@Error_Task", Order_Task);
            htlivecount.Add("@Error_Status", Order_Status);
            htlivecount.Add("@State", Error_State);
            htlivecount.Add("@County", Error_County);
            
            dtlivecount = dataaccess.ExecuteSP("Sp_Error_Dashboard_1", htlivecount);

            if (dtlivecount.Rows.Count > 0)
            {
                Grid_ErrorOnState_All_Task_Count.AutoGenerateColumns = false;
                Grid_ErrorOnState_All_Task_Count.ColumnCount = 6;

                Grid_ErrorOnState_All_Task_Count.Columns[0].Name = "SEARCH";
                Grid_ErrorOnState_All_Task_Count.Columns[0].HeaderText = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].DataPropertyName = "Search";
                Grid_ErrorOnState_All_Task_Count.Columns[0].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[1].Name = "SEARCH QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].HeaderText = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].DataPropertyName = "Search QC";
                Grid_ErrorOnState_All_Task_Count.Columns[1].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[2].Name = "TYPING";
                Grid_ErrorOnState_All_Task_Count.Columns[2].HeaderText = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].DataPropertyName = "Typing";
                Grid_ErrorOnState_All_Task_Count.Columns[2].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[3].Name = "TYPING QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].HeaderText = "Typing QC ";
                Grid_ErrorOnState_All_Task_Count.Columns[3].DataPropertyName = "Typing QC";
                Grid_ErrorOnState_All_Task_Count.Columns[3].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[4].Name = "FINAL QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].HeaderText = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].DataPropertyName = "Final QC";
                Grid_ErrorOnState_All_Task_Count.Columns[4].Width = 80;

                Grid_ErrorOnState_All_Task_Count.Columns[5].Name = "EXCEPTION";
                Grid_ErrorOnState_All_Task_Count.Columns[5].HeaderText = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].DataPropertyName = "Exception";
                Grid_ErrorOnState_All_Task_Count.Columns[5].Width = 80;
                Grid_ErrorOnState_All_Task_Count.DataSource = dtlivecount;
            }
            else
            {
                Grid_ErrorOnState_All_Task_Count.Rows.Clear();
            }
            ArrangeGrid(Grid_ErrorOnState_All_Task_Count);

        }

        private void btn_Error_Tab_Submit_Click(object sender, EventArgs e)
        {

        }

      

       
     


      

      
   
       
      




    }
}
