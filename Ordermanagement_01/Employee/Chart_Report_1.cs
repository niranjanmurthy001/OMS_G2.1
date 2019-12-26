using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using System.Collections;
using System.Globalization;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
//using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraPrinting;
using DevExpress.Utils;

namespace Ordermanagement_01.Masters
{
    public partial class Chart_Report_1 : Form
    {
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        DataTable dt_Bind_ErrTab = new DataTable();
        DataTable dt_Bind_Err_Desc = new DataTable();

        DataTable dt_Errror_Tab = new DataTable();
        DataTable dt_Errror_Desc = new DataTable();
        DataTable dt_Errror_On_User = new DataTable();

        DataTable dt_BarChart_Error_Desc = new DataTable();
        DataTable dt_LineChart_Error_Desc = new DataTable();
        int User_ID, User_Role;
        string Er_Tab_argument, Er_Desc_argument, Error_On_UserName_argument, Error_OnClient, Error_OnState;
        string ertab_errorcount, erDesc_errorcount, eruser_errorcount, Error_Client_Count, Error_State_Count, values;
        string ProductionDate, Error_Tab_Page;
      


        public Chart_Report_1(int User_Id, int Role, string Produ_Date)
        {
            InitializeComponent();
            User_ID = User_Id;
            User_Role = Role;
            chartControl1.CrosshairEnabled = DefaultBoolean.False;
            chartControl1.RuntimeHitTesting = true;
            ProductionDate = Produ_Date;

            chartControl2.CrosshairEnabled = DefaultBoolean.False;
            chartControl2.RuntimeHitTesting = true;

            chartControl3.CrosshairEnabled = DefaultBoolean.False;
            chartControl3.RuntimeHitTesting = true;
        }

        private void Chart_Report_1_Load(object sender, EventArgs e)
        {
             Bind_ErrorTab_Task(ddl_Errors_Tab_Task);
             Bind_ErrorDesc_Task(ddl_Error_Desc_Task);
            Bind_ErrorOnUser_Task(ddl_Error_On_User_Task);
           
            //dbc.Bind_UserName_In_ErrorDashboard(ddl_ErrorOnUser);
            Bind_UserName_In_ErrorDashboard(ddl_ErrorOnUser);
            Bind_ErrUser_Error_Status(dll_Error_Status);
            Bind_ErrDesc_Error_Status(ddl_ErrDesc_ErrorStatus);
            Bind_ErrTab_Error_Status(ddl_ErrTab_Error_Status);

            Bind_Error_Field(ddl_Error_Field);
            Bind_Error_Tab(ddl_Error_Tab);

            string D1 = DateTime.Now.ToString("MM/dd/yyyy");
            string D2 = DateTime.Now.ToString("MM/dd/yyyy");

            txt_Error_Tab_From_Date.Format = DateTimePickerFormat.Custom;
            txt_Error_Tab_To_Date.Format = DateTimePickerFormat.Custom;

            txt_Error_Tab_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_Error_Tab_To_Date.CustomFormat ="MM/dd/yyyy";

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
        }


        private void Bind_ErrorTab_Task(ComboBox ddl_Errors_Tab_Task)
        {
            Hashtable ht_bind = new Hashtable();
            DataTable dt_bind = new DataTable();
            ht_bind.Add("@Trans", "BIND_ERROR_TASK");
            dt_bind = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_bind);
            DataRow dr = dt_bind.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_bind.Rows.InsertAt(dr, 0);
            ddl_Errors_Tab_Task.DataSource = dt_bind;
            ddl_Errors_Tab_Task.DisplayMember = "Order_Status";
            ddl_Errors_Tab_Task.ValueMember = "Order_Status_ID";

        }

        private void Bind_ErrorDesc_Task(ComboBox ddl_Error_Desc_Task)
        {
            Hashtable ht_bind = new Hashtable();
            DataTable dt_bind = new DataTable();
            ht_bind.Add("@Trans", "BIND_ERROR_TASK");
            dt_bind = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_bind);
            DataRow dr = dt_bind.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_bind.Rows.InsertAt(dr, 0);
            ddl_Error_Desc_Task.DataSource = dt_bind;
            ddl_Error_Desc_Task.DisplayMember = "Order_Status";
            ddl_Error_Desc_Task.ValueMember = "Order_Status_ID";
        }

        private void Bind_ErrorOnUser_Task(ComboBox ddl_Error_On_User_Task)
        {
            Hashtable htbind = new Hashtable();
            DataTable dtbind = new DataTable();
            htbind.Add("@Trans", "BIND_ERROR_TASK");
            dtbind = dataaccess.ExecuteSP("Sp_Error_Dashboard", htbind);
            DataRow dr = dtbind.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dtbind.Rows.InsertAt(dr, 0);
            ddl_Error_On_User_Task.DataSource = dtbind;
            ddl_Error_On_User_Task.DisplayMember = "Order_Status";
            ddl_Error_On_User_Task.ValueMember = "Order_Status_ID";
        }

        private void Bind_UserName_In_ErrorDashboard(ComboBox ddl_ErrorOnUser)
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

        private void Bind_ErrUser_Error_Status(ComboBox dll_Error_Status)
        {
            Hashtable ht_Par = new Hashtable();
            DataTable dt_Par = new DataTable();

            ht_Par.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_Par = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Par);
            DataRow dr = dt_Par.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_Par.Rows.InsertAt(dr, 0);
            dll_Error_Status.DataSource = dt_Par;
            dll_Error_Status.DisplayMember = "Error_Status";
            dll_Error_Status.ValueMember = "Error_Status_Id";
        }

        private void Bind_ErrDesc_Error_Status(ComboBox ddl_ErrDesc_ErrorStatus)
        {
            Hashtable ht_Pa = new Hashtable();
            DataTable dt_Pa = new DataTable();

            ht_Pa.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_Pa = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Pa);
            DataRow dr = dt_Pa.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_Pa.Rows.InsertAt(dr, 0);
            ddl_ErrDesc_ErrorStatus.DataSource = dt_Pa;
            ddl_ErrDesc_ErrorStatus.DisplayMember = "Error_Status";
            ddl_ErrDesc_ErrorStatus.ValueMember = "Error_Status_Id";
        }

        private void Bind_ErrTab_Error_Status(ComboBox ddl_ErrTab_Error_Status)
        {
            Hashtable ht_etab = new Hashtable();
            DataTable dt_etab = new DataTable();

            ht_etab.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_etab = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_etab);
            DataRow dr = dt_etab.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_etab.Rows.InsertAt(dr, 0);
            ddl_ErrTab_Error_Status.DataSource = dt_etab;
            ddl_ErrTab_Error_Status.DisplayMember = "Error_Status";
            ddl_ErrTab_Error_Status.ValueMember = "Error_Status_Id";
        }

        private void Bind_Error_Field(ComboBox ddl_Error_Field)
        {
            Hashtable ht_ErField = new Hashtable();
            DataTable dt_ErField = new DataTable();
            ht_ErField.Add("@Trans", "SELECT_ERROR_FIELD");
            dt_ErField = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErField);
            DataRow dr = dt_ErField.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_ErField.Rows.InsertAt(dr, 0);
            ddl_Error_Field.DataSource = dt_ErField;
            ddl_Error_Field.DisplayMember = "Error_description";
            ddl_Error_Field.ValueMember = "Error_description_Id";

        }

        private void Bind_Error_Tab(ComboBox ddl_Error_Tab)
        {
            Hashtable ht_ErTab = new Hashtable();
            DataTable dt_ErTab = new DataTable();
            ht_ErTab.Add("@Trans", "SELECT_ERROR_TAB");
            dt_ErTab = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErTab);
            DataRow dr = dt_ErTab.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_ErTab.Rows.InsertAt(dr, 0);
            ddl_Error_Tab.DataSource = dt_ErTab;
            ddl_Error_Tab.DisplayMember = "Error_Type";
            ddl_Error_Tab.ValueMember = "Error_Type_Id";
        }

        // CLIENT TAB

        private void Bind_Error_Client(ComboBox ddl_Error_On_Client)
        {
            Hashtable ht_ErClient = new Hashtable();
            DataTable dt_ErClient = new DataTable();
            ht_ErClient.Add("@Trans", "SELECT_CLIENT");
            dt_ErClient = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErClient);
            DataRow dr = dt_ErClient.NewRow();
            dr[0] = 0;
            dr[3] = "ALL";
            dt_ErClient.Rows.InsertAt(dr, 0);
            ddl_Error_On_Client.DataSource = dt_ErClient;
            ddl_Error_On_Client.DisplayMember = "Client_Name";
            ddl_Error_On_Client.ValueMember = "Client_Id";

        }

        private void Bind_Error_SubClient(ComboBox ddl_Error_SubClient, int Clientid)
        {
            Hashtable ht_ErCl_Sub = new Hashtable();
            DataTable dt_ErCl_Sub = new DataTable();
            ht_ErCl_Sub.Add("@Trans", "SELECTCLIENTWISE");
            ht_ErCl_Sub.Add("@Client_Id", Clientid);
            dt_ErCl_Sub = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErCl_Sub);
            DataRow dr = dt_ErCl_Sub.NewRow();
            dr[0] = 0;
            dr[6] = "ALL";
            dt_ErCl_Sub.Rows.InsertAt(dr, 0);
            ddl_Error_SubClient.DataSource = dt_ErCl_Sub;
            ddl_Error_SubClient.DisplayMember = "Sub_ProcessName";
            ddl_Error_SubClient.ValueMember = "Subprocess_Id";





        }

        private void Bind_Error_Client_Task(ComboBox ddl_Client_Error_Task)
        {
            Hashtable ht_Cl_Task = new Hashtable();
            DataTable dt_Cl_Task = new DataTable();
            ht_Cl_Task.Add("@Trans", "BIND_ERROR_TASK");
            dt_Cl_Task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Cl_Task);
            DataRow dr = dt_Cl_Task.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_Cl_Task.Rows.InsertAt(dr, 0);
            ddl_Client_Error_Task.DataSource = dt_Cl_Task;
            ddl_Client_Error_Task.DisplayMember = "Order_Status";
            ddl_Client_Error_Task.ValueMember = "Order_Status_ID";
        }

        private void Bind_Error_Client_ErrorStatus(ComboBox ddl_Client_Error_Status)
        {
            Hashtable ht_Cl_Status = new Hashtable();
            DataTable dt_Cl_Status = new DataTable();

            ht_Cl_Status.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_Cl_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Cl_Status);
            DataRow dr = dt_Cl_Status.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_Cl_Status.Rows.InsertAt(dr, 0);
            ddl_Client_Error_Status.DataSource = dt_Cl_Status;
            ddl_Client_Error_Status.DisplayMember = "Error_Status";
            ddl_Client_Error_Status.ValueMember = "Error_Status_Id";
        }

        //  STATE TAB

        private void Bind_Error_State(ComboBox ddl_Error_On_State)
        {
            Hashtable ht_ErState = new Hashtable();
            DataTable dt_ErState = new DataTable();
            ht_ErState.Add("@Trans", "SELECT_STATE");
            dt_ErState = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErState);
            DataRow dr = dt_ErState.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_ErState.Rows.InsertAt(dr, 0);
            ddl_Error_On_State.DataSource = dt_ErState;
            ddl_Error_On_State.DisplayMember = "State";
            ddl_Error_On_State.ValueMember = "State_ID";

        }

        private void Bind_Error_State_Task(ComboBox ddl_State_Error_Task)
        {
            Hashtable ht_ErState_task = new Hashtable();
            DataTable dt_ErState_task = new DataTable();
            ht_ErState_task.Add("@Trans", "BIND_ERROR_TASK");
            dt_ErState_task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErState_task);
            DataRow dr = dt_ErState_task.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_ErState_task.Rows.InsertAt(dr, 0);
            ddl_State_Error_Task.DataSource = dt_ErState_task;
            ddl_State_Error_Task.DisplayMember = "Order_Status";
            ddl_State_Error_Task.ValueMember = "Order_Status_ID";

        }

        private void Bind_Error_State_ErrStatus(ComboBox ddl_State_Error_Status)
        {
            Hashtable ht_State_Status = new Hashtable();
            DataTable dt_State_Status = new DataTable();

            ht_State_Status.Add("@Trans", "SELECT_ERROR_STATUS");
            dt_State_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_State_Status);
            DataRow dr = dt_State_Status.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_State_Status.Rows.InsertAt(dr, 0);
            ddl_State_Error_Status.DataSource = dt_State_Status;
            ddl_State_Error_Status.DisplayMember = "Error_Status";
            ddl_State_Error_Status.ValueMember = "Error_Status_Id";
        }

        public void Bind_County(ComboBox ddl_Error_On_State_County, int Id)
        {
            Hashtable ht_county = new Hashtable();
            DataTable dt_county = new DataTable();

            ht_county.Add("@Trans", "SELECT COUNTY");
            ht_county.Add("@State_ID", Id);
            dt_county = dataaccess.ExecuteSP("Sp_Genral", ht_county);
            DataRow dr = dt_county.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt_county.Rows.InsertAt(dr, 0);
            ddl_Error_On_State_County.DataSource = dt_county;
            ddl_Error_On_State_County.DisplayMember = "County";
            ddl_Error_On_State_County.ValueMember = "County_ID";
            //   ddlName.DataBind();
            //  ddlName.Items.Insert(0, "SELECT");
        }
      

        // Error Tab Sumbit
        private void btn_Error_Tab_Submit_Click(object sender, EventArgs e)
        {
            // All Task Wise
            if (ddl_Errors_Tab_Task.SelectedIndex == 0 && ddl_ErrTab_Error_Status.SelectedIndex == 0 && ddl_Error_Tab.SelectedIndex == 0)
            {
                
                Bind_Bar_Error_Tab_All_Task();
            }
                // Indivisual task wise
            else if (ddl_Errors_Tab_Task.SelectedIndex > 0 && ddl_ErrTab_Error_Status.SelectedIndex == 0 && ddl_Error_Tab.SelectedIndex == 0)
            {

                Bind_Bar_Chart_Error_Task_Wise();
            }
                //Status Wise
            else if (ddl_Errors_Tab_Task.SelectedIndex == 0 && ddl_ErrTab_Error_Status.SelectedIndex > 0 && ddl_Error_Tab.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_Status_Wise();
            }
                // Error Tab wise
            else if (ddl_Errors_Tab_Task.SelectedIndex == 0 && ddl_ErrTab_Error_Status.SelectedIndex == 0 && ddl_Error_Tab.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_Tab_Wise();
            }
                // error tab and task wise
            else if (ddl_Errors_Tab_Task.SelectedIndex > 0 && ddl_ErrTab_Error_Status.SelectedIndex == 0 && ddl_Error_Tab.SelectedIndex > 0)
            {
               Bind_Bar_Chart_Error_Task_AND_Tab_Wise();
            }
            //error tab and   STtaus wise 
            else if (ddl_Errors_Tab_Task.SelectedIndex > 0 && ddl_ErrTab_Error_Status.SelectedIndex == 0 && ddl_Error_Tab.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_Tab_AND_Status_Wise();
            }

              //error task  and   STtaus wise 
            else if (ddl_Errors_Tab_Task.SelectedIndex > 0 && ddl_ErrTab_Error_Status.SelectedIndex > 0 && ddl_Error_Tab.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_Task_AND_Status_Wise();
            }

             //ERROR TAB ,TASK AND STATUS WISE 
            else if (ddl_Errors_Tab_Task.SelectedIndex > 0 && ddl_ErrTab_Error_Status.SelectedIndex > 0 && ddl_Error_Tab.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_Tab_AND_Task_AND_Status_Wise();
            }

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

                //Bar Chart
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

        //  Bar chart Indivsual  Task Wise
        private void Bind_Bar_Chart_Error_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_task = new Hashtable();
                DataTable dt_ErrTab_task = new DataTable();
                // ht_ErrTab_task.Add("@Trans", "BAR_CHART_ERROR_TAB_DATE_WISE_TASK_WISE");
                ht_ErrTab_task.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_WISE");
                ht_ErrTab_task.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_task.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_task.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                dt_ErrTab_task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_task);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_task;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Line = new Hashtable();
                DataTable dt_ErrTab_Task_Line = new DataTable();
                ht_ErrTab_Task_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_WISE");
                ht_ErrTab_Task_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Line.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                dt_ErrTab_Task_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Task_Line);
                // dt_Errror_Tab = dt_ErrTab;

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


        // Bar chart  Error  Status Wise
        private void Bind_Bar_Chart_Error_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Status = new Hashtable();
                DataTable dt_ErrTab_Status = new DataTable();

                ht_ErrTab_Status.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_WISE");
                ht_ErrTab_Status.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Status.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Status.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                dt_ErrTab_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Status);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Status;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Err_Status_Line = new Hashtable();
                DataTable dt_Err_Status_Line= new DataTable();
                ht_Err_Status_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_WISE");
                ht_Err_Status_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Err_Status_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Err_Status_Line.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                dt_Err_Status_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Err_Status_Line);
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

        private void Bind_Bar_Chart_Error_Tab_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Wise = new Hashtable();
                DataTable dt_ErrTab_Wise = new DataTable();

                ht_ErrTab_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_WISE");
                ht_ErrTab_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Wise.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                dt_ErrTab_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Tab_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Tab_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Wise_Line = new Hashtable();
                DataTable dt_ErrTab_Wise_Line = new DataTable();
                ht_ErrTab_Wise_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_WISE");
                ht_ErrTab_Wise_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Wise_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Wise_Line.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                dt_ErrTab_Wise_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Wise_Line);
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

        // Bar chart tab and task wise

        private void Bind_Bar_Chart_Error_Task_AND_Tab_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Wise= new Hashtable();
                DataTable dt_ErrTab_Task_Wise = new DataTable();

                ht_ErrTab_Task_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_AND_TASK_WISE");
                ht_ErrTab_Task_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Wise.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                ht_ErrTab_Task_Wise.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                dt_ErrTab_Task_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Task_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Task_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_AND_Tab_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_AND_Tab_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_wise_Line = new Hashtable();
                DataTable dt_ErrTab_Task_wise_Line = new DataTable();
                ht_ErrTab_Task_wise_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_AND_TASK_WISE");
                ht_ErrTab_Task_wise_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_wise_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_wise_Line.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                ht_ErrTab_Task_wise_Line.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                dt_ErrTab_Task_wise_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Task_wise_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Task_wise_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // Bar chart tab and Status wise

        private void Bind_Bar_Chart_Error_Tab_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Status_wise = new Hashtable();
                DataTable dt_ErrTab_Status_wise = new DataTable();

                ht_ErrTab_Status_wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_AND_STATUS_WISE");
                ht_ErrTab_Status_wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Status_wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Status_wise.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                ht_ErrTab_Status_wise.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                dt_ErrTab_Status_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Status_wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Status_wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Tab_AND_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Tab_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Status_Wise_Line = new Hashtable();
                DataTable dt_ErrTab_Status_Wise_Line = new DataTable();
                ht_ErrTab_Status_Wise_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TAB_AND_STATUS_WISE");
                ht_ErrTab_Status_Wise_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Status_Wise_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Status_Wise_Line.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                ht_ErrTab_Status_Wise_Line.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                dt_ErrTab_Status_Wise_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Status_Wise_Line);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Status_Wise_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // status and task wise

        private void Bind_Bar_Chart_Error_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                ht_Errtask_Status_Wise.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errtask_Status_Wise);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_Errtask_Status_Wise;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Task_AND_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTask_Status_Line = new Hashtable();
                DataTable dt_ErrTask_Status_Line = new DataTable();
                ht_ErrTask_Status_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_WISE");
                ht_ErrTask_Status_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTask_Status_Line.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                ht_ErrTask_Status_Line.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                dt_ErrTask_Status_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTask_Status_Line);
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

        // Error Tab And Task And Status wise
        private void Bind_Bar_Chart_Error_Tab_AND_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Status = new Hashtable();
                DataTable dt_ErrTab_Task_Status = new DataTable();

                ht_ErrTab_Task_Status.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_TAB_WISE");
                ht_ErrTab_Task_Status.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Status.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Status.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                ht_ErrTab_Task_Status.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                ht_ErrTab_Task_Status.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                dt_ErrTab_Task_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Task_Status);
                //dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Task_Status;

                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["Count"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_Tab_AND_Task_AND_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Tab_AND_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Task_Status_Linechart = new Hashtable();
                DataTable dt_ErrTab_Task_Status_Linechart = new DataTable();
                ht_ErrTab_Task_Status_Linechart.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_STATUS_AND_TASK_TAB_WISE");
                ht_ErrTab_Task_Status_Linechart.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Status", int.Parse(ddl_ErrTab_Error_Status.SelectedValue.ToString()));
                ht_ErrTab_Task_Status_Linechart.Add("@Error_Type", int.Parse(ddl_Error_Tab.SelectedValue.ToString()));
                dt_ErrTab_Task_Status_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Task_Status_Linechart);
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

        private void btn_Error_Tab_Export_Click(object sender, EventArgs e)
        {
            Export_Error_Tab();
        }




        //----------Error Desc---------------------


        // Error Desc ALL Task Wise


       


        private void btn_Error_Desc_Sumbit_Click(object sender, EventArgs e)
        {
            if (ddl_Error_Desc_Task.SelectedIndex == 0 && ddl_ErrDesc_ErrorStatus.SelectedIndex == 0 && ddl_Error_Field.SelectedIndex == 0)
            {
                Bind_Bar_Error_Desc_All_Task();
            }
            else if (ddl_Error_Desc_Task.SelectedIndex > 0  && ddl_ErrDesc_ErrorStatus.SelectedIndex == 0 && ddl_Error_Field.SelectedIndex == 0)
            {
                Bind_Bar_Error_Desc_All_Task_Wise();
            }
            else if (ddl_Error_Desc_Task.SelectedIndex == 0 && ddl_ErrDesc_ErrorStatus.SelectedIndex == 0 && ddl_Error_Field.SelectedIndex > 0)
            {
                Bind_Bar_Error_Field_Wise();
            }
            else if (ddl_Error_Desc_Task.SelectedIndex == 0 && ddl_ErrDesc_ErrorStatus.SelectedIndex > 0 && ddl_Error_Field.SelectedIndex > 0)
            {
                Bind_Bar_ErrorDesc_Field_AND_Status_Wise();
            }
            else if (ddl_Error_Desc_Task.SelectedIndex > 0 && ddl_ErrDesc_ErrorStatus.SelectedIndex == 0 && ddl_Error_Field.SelectedIndex > 0)
            {
                Bind_Bar_ErrorDesc_Field_AND_Task_Wise();
            }
            else if (ddl_Error_Desc_Task.SelectedIndex == 0 && ddl_ErrDesc_ErrorStatus.SelectedIndex > 0 && ddl_Error_Field.SelectedIndex == 0)
            {
                Bind_Bar_ErrorDesc_Status_Wise();
            }
            else if (ddl_Error_Desc_Task.SelectedIndex > 0 && ddl_ErrDesc_ErrorStatus.SelectedIndex > 0 && ddl_Error_Field.SelectedIndex == 0)
            {
                Bind_Bar_ErrorDesc_Task_AND_Status_Wise();
            }

            else if (ddl_Error_Desc_Task.SelectedIndex > 0 && ddl_ErrDesc_ErrorStatus.SelectedIndex > 0 && ddl_Error_Field.SelectedIndex > 0)
            {
                Bind_Bar_ErrorDesc_Field_AND_Status_AND_Task_Wise();
            }

        }

        // ALL Task Wise
        private void Bind_Bar_Error_Desc_All_Task()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errdesc = new Hashtable();
                DataTable dt_Errdesc = new DataTable();

                ht_Errdesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_ALL_TASK");
                ht_Errdesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Errdesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                dt_Errdesc = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errdesc);
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

                htSel_Line_ErrorDesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_ALL_TASK");
                htSel_Line_ErrorDesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                htSel_Line_ErrorDesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);

                dtSel_Line_ErrorDesc = dataaccess.ExecuteSP("Sp_Error_Dashboard", htSel_Line_ErrorDesc);

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

        // Error Desc Indivusal Task Wise
        private void Bind_Bar_Error_Desc_All_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDesc = new Hashtable();
                DataTable dt_Select_ErrDesc = new DataTable();
                //ht_Select_ErrDesc.Add("@Trans", "BAR_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_WISE");
                ht_Select_ErrDesc.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_WISE");
                ht_Select_ErrDesc.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDesc.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDesc.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Select_ErrDesc = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Select_ErrDesc);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDesc;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";





                Bind_Line_Error_Desc_All_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_Error_Desc_All_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sele = new Hashtable();
                DataTable dt_Sele = new DataTable();
                ht_Sele.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_WISE");
                ht_Sele.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sele.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sele.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Sele = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sele);
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
     
        // Error Desc Filed Wise
        private void Bind_Bar_Error_Field_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDe = new Hashtable();
                DataTable dt_Select_ErrDe = new DataTable();
               
                ht_Select_ErrDe.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_WISE");
                ht_Select_ErrDe.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDe.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDe.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                dt_Select_ErrDe = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Select_ErrDe);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDe;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_Error_Field_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_Error_Field_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel = new Hashtable();
                DataTable dt_Sel = new DataTable();
                ht_Sel.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_WISE");
                ht_Sel.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                dt_Sel = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel);
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
      
        private void Bind_Bar_ErrorDesc_Field_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDe = new Hashtable();
                DataTable dt_Select_ErrDe = new DataTable();

                ht_Select_ErrDe.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_STATUS_WISE");
                ht_Select_ErrDe.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDe.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDe.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                ht_Select_ErrDe.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                dt_Select_ErrDe = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Select_ErrDe);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDe;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel = new Hashtable();
                DataTable dt_Sel = new DataTable();
                ht_Sel.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_STATUS_WISE");
                ht_Sel.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                ht_Sel.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                dt_Sel = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel);
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

        //BARHCART Field And TASK Wise
        private void Bind_Bar_ErrorDesc_Field_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_ErrD = new Hashtable();
                DataTable dt_Sel_ErrD = new DataTable();

                ht_Sel_ErrD.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_WISE");
                ht_Sel_ErrD.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_ErrD.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_ErrD.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                ht_Sel_ErrD.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Sel_ErrD = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel_ErrD);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Sel_ErrD;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_Line = new Hashtable();
                DataTable dt_Sel_Line = new DataTable();
                ht_Sel_Line.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_WISE");
                ht_Sel_Line.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_Line.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_Line.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                ht_Sel_Line.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Sel_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel_Line);
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

        //BARHCART  TASK AND Status  Wise
        private void Bind_Bar_ErrorDesc_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_ErrD = new Hashtable();
                DataTable dt_Sel_ErrD = new DataTable();

                ht_Sel_ErrD.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_AND_STATUS_WISE");
                ht_Sel_ErrD.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_ErrD.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_ErrD.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                ht_Sel_ErrD.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Sel_ErrD = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel_ErrD);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Sel_ErrD;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Task_AND_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_Line = new Hashtable();
                DataTable dt_Sel_Line = new DataTable();
                ht_Sel_Line.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_TASK_AND_STATUS_WISE");
                ht_Sel_Line.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_Line.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_Line.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                ht_Sel_Line.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Sel_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel_Line);
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



        // Error Status Wise
        private void Bind_Bar_ErrorDesc_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Select_ErrDe = new Hashtable();
                DataTable dt_Select_ErrDe = new DataTable();

                ht_Select_ErrDe.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_STATUS_WISE");
                ht_Select_ErrDe.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select_ErrDe.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Select_ErrDe.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                dt_Select_ErrDe = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Select_ErrDe);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Select_ErrDe;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel = new Hashtable();
                DataTable dt_Sel = new DataTable();
                ht_Sel.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_STATUS_WISE");
                ht_Sel.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                dt_Sel = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel);
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


        // BARHCART Field And TASK AND Status Wise

        private void Bind_Bar_ErrorDesc_Field_AND_Status_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Sel_ErrD = new Hashtable();
                DataTable dt_Sel_ErrD = new DataTable();

                ht_Sel_ErrD.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_AND_STATUS_WISE");
                ht_Sel_ErrD.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Sel_ErrD.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Sel_ErrD.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                ht_Sel_ErrD.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                ht_Sel_ErrD.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Sel_ErrD = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Sel_ErrD);
                //dt = dt_Select_ErrDesc;

                chartControl2.DataSource = dt_Sel_ErrD;

                chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl2.Series["Count"].ArgumentDataMember = "Error_description";
                chartControl2.Series["Count"].ValueDataMembers[0] = "Error_Desc_Count";


                Bind_Line_ErrorDesc_Field_AND_Status_AND_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bind_Line_ErrorDesc_Field_AND_Status_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Se = new Hashtable();
                DataTable dt_Se = new DataTable();
                ht_Se.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_FIELD_AND_TASK_AND_STATUS_WISE");
                ht_Se.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Se.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                ht_Se.Add("@Error_description_Id", int.Parse(ddl_Error_Field.SelectedValue.ToString()));
                ht_Se.Add("@Error_Status", int.Parse(ddl_ErrDesc_ErrorStatus.SelectedValue.ToString()));
                ht_Se.Add("@Error_Task", int.Parse(ddl_Error_Desc_Task.SelectedValue.ToString()));
                dt_Se = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Se);
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






        // Export Error Desc Chart
        private void Export_Error_Desc()
        {
            load_Progressbar.Start_progres();
            DevExpress.XtraPrinting.PrintingSystem ps1 = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrintingLinks.CompositeLink cl_1 = new DevExpress.XtraPrintingLinks.CompositeLink(ps1);
            DevExpress.XtraPrinting.PrintableComponentLink pclChart1 = new DevExpress.XtraPrinting.PrintableComponentLink();

            chartControl2.Visible = true;
            pclChart1.Component = chartControl2;

            cl_1.PaperKind = System.Drawing.Printing.PaperKind.ESheet;
            cl_1.Landscape = true;
            cl_1.Margins.Right = 40;
            cl_1.Margins.Left = 40;

            cl_1.Links.AddRange(new object[] { pclChart1 });
            cl_1.ShowPreviewDialog();


        }

        private void btn_Error_Desc_Export_Click(object sender, EventArgs e)
        {
            Export_Error_Desc();
        }


        //--------------------- Error On User-------------------------------------


        private void btn_ErrorOnUser_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_Error_On_User_Task.SelectedIndex == 0 && ddl_ErrorOnUser.SelectedIndex == 0 && dll_Error_Status.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_User_All_Task();
                
            }
                // Task Wise
            else if (ddl_Error_On_User_Task.SelectedIndex > 0 && ddl_ErrorOnUser.SelectedIndex == 0 && dll_Error_Status.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_User_Task_Wise();
            }

                // user wise
            else if (ddl_ErrorOnUser.SelectedIndex > 0 && ddl_Error_On_User_Task.SelectedIndex == 0 &&  dll_Error_Status.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_User_Wise();
            }
             // User and Task Wise
            else if (ddl_ErrorOnUser.SelectedIndex > 0 && ddl_Error_On_User_Task.SelectedIndex > 0 && dll_Error_Status.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_User_AND_Task_Wise();
            }
            // Error Status Wise
            else if (ddl_ErrorOnUser.SelectedIndex == 0 && ddl_Error_On_User_Task.SelectedIndex == 0 && dll_Error_Status.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Status_Wise();
            }
                // Task And Status Wise
            else if (ddl_ErrorOnUser.SelectedIndex == 0 && ddl_Error_On_User_Task.SelectedIndex > 0 && dll_Error_Status.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Status_AND_Task_Wise();
            }
                //status and user wise
            else if (ddl_ErrorOnUser.SelectedIndex > 0 && ddl_Error_On_User_Task.SelectedIndex == 0 && dll_Error_Status.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Status_AND_User_Wise();
            }
            else if (ddl_ErrorOnUser.SelectedIndex > 0 && ddl_Error_On_User_Task.SelectedIndex > 0 && dll_Error_Status.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_User_AND_Task_AND_Status_Wise();
            }
        }

        // Export Error Desc Chart
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
                dt_ErrOnUser_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnUser_Barchart);
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
                dt_Erruser_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Erruser_Barchart);
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

        // BAR CHART individusal Task wise
        private void Bind_Bar_Chart_Error_On_User_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUser = new Hashtable();
                DataTable dt_ErrOnUser = new DataTable();
                ht_ErrOnUser.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_TASK_WISE");
                ht_ErrOnUser.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUser.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOnUser.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                dt_ErrOnUser = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnUser);
                // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUser;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_User_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_User_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUser_Line = new Hashtable();
                DataTable dt_ErrOnUser_Line = new DataTable();
                ht_ErrOnUser_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_TASK_WISE");
                ht_ErrOnUser_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUser_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOnUser_Line.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                dt_ErrOnUser_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnUser_Line);
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
        private void Bind_Bar_Chart_Error_On_User_Wise()
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
                ht_ErrOnUse_Barchart.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                dt_ErrOnUse_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnUse_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUse_Barchart;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_User_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_User_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Erruse_Linechart = new Hashtable();
                DataTable dt_Erruse_Linechart = new DataTable();
                ht_Erruse_Linechart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_USER_WISE");
                ht_Erruse_Linechart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Erruse_Linechart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_Erruse_Linechart.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                dt_Erruse_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Erruse_Linechart);
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
        private void Bind_Bar_Chart_Error_On_User_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnUs_Barchart = new Hashtable();
                DataTable dt_ErrOnUs_Barchart = new DataTable();

                ht_ErrOnUs_Barchart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_USER_AND_TASK_WISE");
                ht_ErrOnUs_Barchart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOnUs_Barchart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOnUs_Barchart.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                ht_ErrOnUs_Barchart.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                dt_ErrOnUs_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnUs_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOnUs_Barchart;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_User_AND_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        private void Bind_Line_Chart_Error_On_User_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Erru_Linechart = new Hashtable();
                DataTable dt_Erru_Linechart = new DataTable();
                ht_Erru_Linechart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_USER_AND_TASK_WISE");
                ht_Erru_Linechart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Erru_Linechart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_Erru_Linechart.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                ht_Erru_Linechart.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                dt_Erru_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Erru_Linechart);
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
        private void Bind_Bar_Chart_Error_On_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Errsta_Barchart = new Hashtable();
                DataTable dt_Errsta_Barchart = new DataTable();

                ht_Errsta_Barchart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_WISE");
                ht_Errsta_Barchart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Errsta_Barchart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_Errsta_Barchart.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                dt_Errsta_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errsta_Barchart);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_Errsta_Barchart;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrSta_Linechart = new Hashtable();
                DataTable dt_ErrSta_Linechart = new DataTable();
                ht_ErrSta_Linechart.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_WISE");
                ht_ErrSta_Linechart.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrSta_Linechart.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrSta_Linechart.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                dt_ErrSta_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrSta_Linechart);
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
        private void Bind_Bar_Chart_Error_On_Status_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOn_Bar = new Hashtable();
                DataTable dt_ErrOn_Bar = new DataTable();

                ht_ErrOn_Bar.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_WISE");
                ht_ErrOn_Bar.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrOn_Bar.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrOn_Bar.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                ht_ErrOn_Bar.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                dt_ErrOn_Bar = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOn_Bar);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrOn_Bar;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_AND_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_AND_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrS_Line = new Hashtable();
                DataTable dt_ErrS_Line = new DataTable();
                ht_ErrS_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_WISE");
                ht_ErrS_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrS_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrS_Line.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                ht_ErrS_Line.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                dt_ErrS_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrS_Line);
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

        private void Bind_Bar_Chart_Error_On_Status_AND_User_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrStat_Bar = new Hashtable();
                DataTable dt_ErrStat_Bar = new DataTable();

                ht_ErrStat_Bar.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_USER_WISE");
                ht_ErrStat_Bar.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrStat_Bar.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrStat_Bar.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                ht_ErrStat_Bar.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                dt_ErrStat_Bar = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrStat_Bar);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrStat_Bar;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_AND_User_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_AND_User_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrSt_Line = new Hashtable();
                DataTable dt_ErrSt_Line = new DataTable();
                ht_ErrSt_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_USER_WISE");
                ht_ErrSt_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrSt_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrSt_Line.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                ht_ErrSt_Line.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                dt_ErrSt_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrSt_Line);
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

        private void Bind_Bar_Chart_Error_On_User_AND_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrStat_Bar = new Hashtable();
                DataTable dt_ErrStat_Bar = new DataTable();

                ht_ErrStat_Bar.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_AND_USER_WISE");
                ht_ErrStat_Bar.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_ErrStat_Bar.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_ErrStat_Bar.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                ht_ErrStat_Bar.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                ht_ErrStat_Bar.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                dt_ErrStat_Bar = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrStat_Bar);
                //dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl3.DataSource = dt_ErrStat_Bar;
                chartControl3.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl3.Series["Count"].ArgumentDataMember = "Error_On_UserName";
                chartControl3.Series["Count"].ValueDataMembers[0] = "Error_Count";

                Bind_Line_Chart_Error_On_Status_AND_Task_AND_User_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Status_AND_Task_AND_User_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Err_Line = new Hashtable();
                DataTable dt_Err_Line = new DataTable();
                ht_Err_Line.Add("@Trans", "LINE_CHART_ERROR_ON_USER_DATE_WISE_STATUS_AND_TASK_AND_USER_WISE");
                ht_Err_Line.Add("@Error_From_Date", txt_ErrorOnUser_From_Date.Text);
                ht_Err_Line.Add("@Error_To_Date", txt_ErrorOnUser_To_Date.Text);
                ht_Err_Line.Add("@Error_Status", int.Parse(dll_Error_Status.SelectedValue.ToString()));
                ht_Err_Line.Add("@Error_User", int.Parse(ddl_ErrorOnUser.SelectedValue.ToString()));
                ht_Err_Line.Add("@Error_Task", int.Parse(ddl_Error_On_User_Task.SelectedValue.ToString()));
                dt_Err_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Err_Line);
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


        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //string argument;
            //int errorcount,  values;
            // Obtain hit information under the test point.
            ChartHitInfo hi = chartControl1.CalcHitInfo(e.X, e.Y);

            // Obtain the series point under the test point.
            SeriesPoint point = hi.SeriesPoint;

            // Check whether the series point was clicked or not.
            if (point != null)
            {
                // Obtain the series point argument.
                // Er_Tab_argument = "Argument: " + point.Argument.ToString();

                Er_Tab_argument = point.Argument.ToString();

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
                // Show the tooltip.
                // toolTipController1.ShowHint(argument + "\n" + values, "SeriesPoint Data");
            }
            else
            {
                // Hide the tooltip.
                // toolTipController1.HideHint();
            }

            Error_Tab_Page = "Error_Tab";


            if (tabControl1.TabIndex == 0)
            {
               
               // Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Er_Tab_argument,State, Convert.ToInt32(ertab_errorcount), txt_Error_Tab_From_Date.Text, txt_Error_Tab_To_Date.Text, ProductionDate, Error_Tab_Page);
               // errordetails.Show();
            }


        }

        private void chartControl2_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl2.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;

            if (point != null)
            {

                //Er_Desc_argument = "Argument: " + point.Argument.ToString();

                Er_Desc_argument = point.Argument.ToString();
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


            if (tabControl1.TabIndex == 0)
            {

               // Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Er_Desc_argument, Convert.ToInt32(erDesc_errorcount), txt_Error_Desc_From_Date.Text, txt_Error_Desc_To_Date.Text, ProductionDate, Error_Tab_Page);
             //   errordetails.Show();
            }
        }


        private void chartControl3_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl3.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;

            if (point != null)
            {

                //Error_On_UserName_argument = "Argument: " + point.Argument.ToString();
                Error_On_UserName_argument = point.Argument.ToString();
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


            if (tabControl1.TabIndex == 0)
            {

               // Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_On_UserName_argument, Convert.ToInt32(eruser_errorcount), txt_ErrorOnUser_From_Date.Text, txt_ErrorOnUser_To_Date.Text, ProductionDate, Error_Tab_Page);
              //  errordetails.Show();
            }
        }

        // CLIENT

        private void btn_Client_Submit_Click(object sender, EventArgs e)
        {
            // all task wise
            if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_All_Task();

            }
            // task wise
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_Task_Wise();

            }
            // task and status wise
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Wise();

            }

             // task and status and Client wise
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_Wise();

            }

             // task and status and Client and subclient wise
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise();

            }
            // task and Client and subclient wise
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_Task_AND_Client_Wise();

            }

            // status  wise
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_Status_Wise();

            }
            // status  and CLient wise
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_Status_AND_Client_Wise();

            }

             // status  and CLient and subclient wise
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise();

            }

             // status  and subclient wise
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Client_Status_AND_SubClient_Wise();

            }
            // client wise
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Client_Wise();

            }
            // Client  and subclient wise
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Client_AND_Subclient_Wise();

            }
            // subclient wise
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex > 0)
            {
                Bind_Bar_Chart_ErrorOn_Subclient_Wise();

            }



        }


        // BAR CHART Error On CLIENT All Task Wise
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

        // BAR CHART ERRROR ON CLIENT INDIVIDUSAL TASK WISE
        private void Bind_Bar_Chart_Error_On_Client_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnClient_taskwise = new Hashtable();
                DataTable dt_ErrOnClient_taskwise = new DataTable();
                ht_ErrOnClient_taskwise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_WISE");
                ht_ErrOnClient_taskwise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                dt_ErrOnClient_taskwise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnClient_taskwise);
                // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl4.DataSource = dt_ErrOnClient_taskwise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnCl_Line_task = new Hashtable();
                DataTable dt_ErrOnCl_Line_task = new DataTable();
                ht_ErrOnCl_Line_task.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_WISE");
                ht_ErrOnCl_Line_task.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                dt_ErrOnCl_Line_task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnCl_Line_task);

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

        // BAR CHART Task and Status wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnClient_taskwise = new Hashtable();
                DataTable dt_ErrOnClient_taskwise = new DataTable();
                ht_ErrOnClient_taskwise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_WISE");
                ht_ErrOnClient_taskwise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnClient_taskwise.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_ErrOnClient_taskwise.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                dt_ErrOnClient_taskwise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnClient_taskwise);

                chartControl4.DataSource = dt_ErrOnClient_taskwise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnCl_Line_task = new Hashtable();
                DataTable dt_ErrOnCl_Line_task = new DataTable();
                ht_ErrOnCl_Line_task.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_WISE");
                ht_ErrOnCl_Line_task.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnCl_Line_task.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_ErrOnCl_Line_task.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                dt_ErrOnCl_Line_task = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnCl_Line_task);

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


        // BAR CHART Task and Status and CLIENT wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_status_cl_wise = new Hashtable();
                DataTable dt_task_status_cl_wise = new DataTable();
                ht_task_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_WISE");
                ht_task_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_status_cl_wise.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_task_status_cl_wise.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_task_status_cl_wise.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                dt_task_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_status_cl_wise);

                chartControl4.DataSource = dt_task_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_taskStatus_Client = new Hashtable();
                DataTable dt_Line_taskStatus_Client = new DataTable();
                ht_Line_taskStatus_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_WISE");
                ht_Line_taskStatus_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_Line_taskStatus_Client.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_Line_taskStatus_Client.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                dt_Line_taskStatus_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_taskStatus_Client);

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

        // BAR CHART Task and Status and CLIENT and SubClient wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_status_cl_wise = new Hashtable();
                DataTable dt_task_status_cl_wise = new DataTable();
                ht_task_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_SUBCLIENT_WISE");
                ht_task_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_status_cl_wise.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_task_status_cl_wise.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_task_status_cl_wise.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                ht_task_status_cl_wise.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_task_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_status_cl_wise);

                chartControl4.DataSource = dt_task_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Status_Client_SubClient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_taskStatus_Client = new Hashtable();
                DataTable dt_Line_taskStatus_Client = new DataTable();
                ht_Line_taskStatus_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_STATUS_CLIENT_SUBCLIENT_WISE");
                ht_Line_taskStatus_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_Line_taskStatus_Client.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_Line_taskStatus_Client.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                ht_Line_taskStatus_Client.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_Line_taskStatus_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_taskStatus_Client);

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


        // BAR CHART Task and CLIENT  wise
        private void Bind_Bar_Chart_Error_On_Client_Task_AND_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_status_cl_wise = new Hashtable();
                DataTable dt_task_status_cl_wise = new DataTable();
                ht_task_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_WISE");
                ht_task_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_task_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_task_status_cl_wise.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_task_status_cl_wise.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));

                dt_task_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_status_cl_wise);

                chartControl4.DataSource = dt_task_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Task_AND_Client_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Task_AND_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_taskStatus_Client = new Hashtable();
                DataTable dt_Line_taskStatus_Client = new DataTable();
                ht_Line_taskStatus_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_TASK_AND_CLIENT_WISE");
                ht_Line_taskStatus_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_taskStatus_Client.Add("@Error_Task", int.Parse(ddl_Client_Error_Task.SelectedValue.ToString()));
                ht_Line_taskStatus_Client.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));

                dt_Line_taskStatus_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_taskStatus_Client);

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




        // BAR CHART Status wise
        private void Bind_Bar_Chart_Error_On_Client_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnClient_statuswise = new Hashtable();
                DataTable dt_ErrOnClient_statuswise = new DataTable();
                ht_ErrOnClient_statuswise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_WISE");
                ht_ErrOnClient_statuswise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnClient_statuswise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnClient_statuswise.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                dt_ErrOnClient_statuswise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnClient_statuswise);

                chartControl4.DataSource = dt_ErrOnClient_statuswise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrOnCl_Line_status = new Hashtable();
                DataTable dt_ErrOnCl_Line_status = new DataTable();
                ht_ErrOnCl_Line_status.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_WISE");
                ht_ErrOnCl_Line_status.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_ErrOnCl_Line_status.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_ErrOnCl_Line_status.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                dt_ErrOnCl_Line_status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOnCl_Line_status);

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

        // BAR CHART Status and CLIENT wise
        private void Bind_Bar_Chart_Error_On_Client_Status_AND_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_status_cl_wise = new Hashtable();
                DataTable dt_status_cl_wise = new DataTable();
                ht_status_cl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_WISE");
                ht_status_cl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_status_cl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_status_cl_wise.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_status_cl_wise.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                dt_status_cl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_status_cl_wise);

                chartControl4.DataSource = dt_status_cl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Status_AND_Client_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Status_AND_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Status_Client = new Hashtable();
                DataTable dt_Line_Status_Client = new DataTable();
                ht_Line_Status_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_WISE");
                ht_Line_Status_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Status_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Status_Client.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_Line_Status_Client.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                dt_Line_Status_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_Status_Client);

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


        // BAR CHART Status and CLIENT and subclient wise
        private void Bind_Bar_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_status_cl_SubCl_wise = new Hashtable();
                DataTable dt_status_cl_SubCl_wise = new DataTable();
                ht_status_cl_SubCl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_status_cl_SubCl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_status_cl_SubCl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_status_cl_SubCl_wise.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_status_cl_SubCl_wise.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                ht_status_cl_SubCl_wise.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_status_cl_SubCl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_status_cl_SubCl_wise);

                chartControl4.DataSource = dt_status_cl_SubCl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Status_AND_Client_AND_SubClient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Stat_Cl_SubCl = new Hashtable();
                DataTable dt_Line_Stat_Cl_SubCl = new DataTable();
                ht_Line_Stat_Cl_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_Line_Stat_Cl_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Stat_Cl_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Stat_Cl_SubCl.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_Line_Stat_Cl_SubCl.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                ht_Line_Stat_Cl_SubCl.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_Line_Stat_Cl_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_Stat_Cl_SubCl);

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


        // BAR CHART Status and subclient wise
        private void Bind_Bar_Chart_Error_On_Client_Status_AND_SubClient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_status_SubCl_wise = new Hashtable();
                DataTable dt_status_SubCl_wise = new DataTable();
                ht_status_SubCl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_SUBCLIENT_WISE");
                ht_status_SubCl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_status_SubCl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_status_SubCl_wise.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_status_SubCl_wise.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_status_SubCl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_status_SubCl_wise);

                chartControl4.DataSource = dt_status_SubCl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Status_AND_SubClient_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Status_AND_SubClient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Stat_SubCl = new Hashtable();
                DataTable dt_Line_Stat_SubCl = new DataTable();
                ht_Line_Stat_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_STATUS_AND_SUBCLIENT_WISE");
                ht_Line_Stat_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Stat_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Stat_SubCl.Add("@Error_Status", int.Parse(ddl_Client_Error_Status.SelectedValue.ToString()));
                ht_Line_Stat_SubCl.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_Line_Stat_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_Stat_SubCl);

                chartControl4.DataSource = dt_Line_Stat_SubCl;
                chartControl4.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["%"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["%"].ValueDataMembers[0] = "Error_Client_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // BAR CHART CLIENT wise
        private void Bind_Bar_Chart_Error_On_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_client_wise = new Hashtable();
                DataTable dt_client_wise = new DataTable();
                ht_client_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_CLIENT_WISE");
                ht_client_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_wise.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                dt_client_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_client_wise);

                chartControl4.DataSource = dt_client_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client = new Hashtable();
                DataTable dt_Line_Client = new DataTable();
                ht_Line_Client.Add("@Trans", "LINE_CHART_CLIENT_DATE_CLIENT_WISE");
                ht_Line_Client.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                dt_Line_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_Client);

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


        // BAR CHART CLIENT and Subclient wise
        private void Bind_Bar_Chart_Error_On_Client_AND_Subclient_Wise()
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
                ht_client_Subcl_wise.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                ht_client_Subcl_wise.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_client_Subcl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_client_Subcl_wise);

                chartControl4.DataSource = dt_client_Subcl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_Error_On_Client_AND_Subclient_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_On_Client_AND_Subclient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client_SubCl = new Hashtable();
                DataTable dt_Line_Client_SubCl = new DataTable();
                ht_Line_Client_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_AND_CLIENT_AND_SUBCLIENT_WISE");
                ht_Line_Client_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client_SubCl.Add("@Client_Id", int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                ht_Line_Client_SubCl.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_Line_Client_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_Client_SubCl);

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

        // BAR CHART Subclient wise
        private void Bind_Bar_Chart_ErrorOn_Subclient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_client_Subcl_wise = new Hashtable();
                DataTable dt_client_Subcl_wise = new DataTable();
                ht_client_Subcl_wise.Add("@Trans", "LINE_CHART_CLIENT_DATE_SUBCLIENT_WISE");
                ht_client_Subcl_wise.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_client_Subcl_wise.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_client_Subcl_wise.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_client_Subcl_wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_client_Subcl_wise);

                chartControl4.DataSource = dt_client_Subcl_wise;
                chartControl4.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl4.Series["Count"].ArgumentDataMember = "Error_Client_Name";
                chartControl4.Series["Count"].ValueDataMembers[0] = "Error_Client_Count";

                Bind_Line_Chart_ErrorOn_Subclient_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_ErrorOn_Subclient_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_Line_Client_SubCl = new Hashtable();
                DataTable dt_Line_Client_SubCl = new DataTable();
                ht_Line_Client_SubCl.Add("@Trans", "LINE_CHART_CLIENT_DATE_SUBCLIENT_WISE");
                ht_Line_Client_SubCl.Add("@Error_From_Date", txt_Client_From_Date.Text);
                ht_Line_Client_SubCl.Add("@Error_To_Date", txt_Client_To_Date.Text);
                ht_Line_Client_SubCl.Add("@Subprocess_Id", int.Parse(ddl_Error_SubClient.SelectedValue.ToString()));
                dt_Line_Client_SubCl = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Line_Client_SubCl);

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



        private void btn_Client_Export_Click(object sender, EventArgs e)
        {
            Export_Error_On_Client();
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

        private void chartControl4_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl4.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;

            if (point != null)
            {

                //Error_On_UserName_argument = "Argument: " + point.Argument.ToString();
                Error_OnClient = point.Argument.ToString();
                Error_Client_Count = Convert.ToInt32(point.Values[0]).ToString();
                values = "Value(s): " + Error_Client_Count;
                if (Error_Client_Count.Length >= 1)
                {
                    for (int i = 0; i <= Error_Client_Count.Length - 1; i++)
                    {
                        values = values + ", " + Error_Client_Count[i].ToString();
                    }
                }
            }

            if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Client";
            }
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Client";
            }
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Client";
            }
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex == 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Client";
            }
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Subclient";
            }
            else if (ddl_Client_Error_Task.SelectedIndex > 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Subclient";
            }
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex > 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Subclient";
            }

            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_Subclient";
            }
            else if (ddl_Client_Error_Task.SelectedIndex == 0 && ddl_Client_Error_Status.SelectedIndex == 0 && ddl_Error_On_Client.SelectedIndex > 0 && ddl_Error_SubClient.SelectedIndex > 0)
            {
                Error_Tab_Page = "Error_On_Subclient";
            }




            if (tabControl1.TabIndex == 0)
            {

               // Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_OnClient, Convert.ToInt32(Error_Client_Count), txt_Client_From_Date.Text, txt_Client_To_Date.Text, ProductionDate, Error_Tab_Page);
               // errordetails.Show();
            }
        }

        private void ddl_Error_On_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Error_On_Client.SelectedIndex > 0)
            {
                if (User_Role == 1)
                {
                    Bind_Error_SubClient(ddl_Error_SubClient, int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));

                    // dbc.BindSubProcessName(ddl_Error_SubClient, int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                }
                else
                {
                    dbc.BindSubProcessNumber(ddl_Error_SubClient, int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
                }

            }
            else
            {

                Bind_Error_SubClient(ddl_Error_SubClient, int.Parse(ddl_Error_On_Client.SelectedValue.ToString()));
            }
        }





        // --------------- State TAB-----------------------------
        private void ddl_Error_On_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Error_On_State.SelectedIndex > 0)
            {
                Bind_County(ddl_Error_On_State_County, int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
            }
            else if (ddl_Error_On_State.SelectedIndex == 0)
            {
                //ddl_Error_On_State.SelectedIndex=0;
                //ddl_Error_On_State.SelectedValue = 0;
                // Bind_County(ddl_Error_On_State_County, int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
            }
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

        private void chartControl5_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hi = chartControl5.CalcHitInfo(e.X, e.Y);
            SeriesPoint point = hi.SeriesPoint;

            if (point != null)
            {

                //Error_On_UserName_argument = "Argument: " + point.Argument.ToString();
                Error_OnState = point.Argument.ToString();
                Error_State_Count = Convert.ToInt32(point.Values[0]).ToString();
                values = "Value(s): " + Error_State_Count;
                if (Error_State_Count.Length >= 1)
                {
                    for (int i = 0; i <= Error_State_Count.Length - 1; i++)
                    {
                        values = values + ", " + Error_State_Count[i].ToString();
                    }
                }
            }
            if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_State";
            }
            else if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_State";
            }
            else if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_State";
            }
            else if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_State";
            }
            else if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_County";
            }
            else if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_County";
            }
            else if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_County";
            }

            else if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Error_Tab_Page = "Error_On_County";
            }
            else if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex > 0)
            {
                Error_Tab_Page = "Error_On_County";
            }
            //Error_Tab_Page = "Error_On_State";

            if (tabControl1.TabIndex == 0)
            {

               // Ordermanagement_01.Employee.Error_Details errordetails = new Ordermanagement_01.Employee.Error_Details(User_ID, User_Role, Error_OnState, Convert.ToInt32(Error_State_Count), txt_State_From_Date.Text, txt_State_To_Date.Text, ProductionDate, Error_Tab_Page);
               // errordetails.Show();
            }

        }

        private void btn_State_Submit_Click(object sender, EventArgs e)
        {
            // all task wise
            if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_State_All_Task();

            }
            // indivisual task wise
            if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_State_Task_Wise();

            }

            //  Status wise
            if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_State_Status_Wise();

            }

            //  STATE wise
            if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_State_Wise();

            }
            //  TASK AND STATUS wise
            if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex == 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_State_Task_AND_Status_Wise();

            }
            //  TASK AND STATE wise
            if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_State_Task_AND_State_Wise();

            }
            //  TASK AND Status AND STATE wise
            if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Task_Status_State_Wise();

            }

            //   Status AND STATE wise
            if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_Status_AND_State_Wise();

            }
            //   Status AND STATE and County wise
            if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Status_AND_State_AND_County_Wise();

            }




            //   STATE and County wise
            if (ddl_State_Error_Task.SelectedIndex == 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_State_AND_County_Wise();

            }
            //  TASK AND County AND STATE wise
            if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex == 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Task_State_County_Wise();

            }

            // TASK AND Status AND STATEand county  wise
            if (ddl_State_Error_Task.SelectedIndex > 0 && ddl_State_Error_Status.SelectedIndex > 0 && ddl_Error_On_State.SelectedIndex > 0 && ddl_Error_On_State_County.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_Task_State_County_Wise();

            }

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
        private void Bind_Bar_Chart_Error_On_State_Task_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrOn_state_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_Barchart = new DataTable();

                ht_ErrOn_state_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_WISE");
                ht_ErrOn_state_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                dt_ErrOn_state_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOn_state_Barchart);

                chartControl5.DataSource = dt_ErrOn_state_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_Barchart = new Hashtable();
                DataTable dt_ErrState_Barchart = new DataTable();
                ht_ErrState_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_WISE");
                ht_ErrState_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_Barchart.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                dt_ErrState_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrState_Barchart);

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
        private void Bind_Bar_Chart_Error_On_State_Status_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrOn_state_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_Barchart = new DataTable();

                ht_ErrOn_state_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_WISE");
                ht_ErrOn_state_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                dt_ErrOn_state_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOn_state_Barchart);

                chartControl5.DataSource = dt_ErrOn_state_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_Barchart = new Hashtable();
                DataTable dt_ErrState_Barchart = new DataTable();
                ht_ErrState_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_WISE");
                ht_ErrState_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_Barchart.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                dt_ErrState_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrState_Barchart);

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
        private void Bind_Bar_Chart_Error_On_State_Wise()
        {
            try
            {

                //chartControl5.Series.Remove(chartControl5.Series["Error_State_Name"]);
                //chartControl5.RefreshData();

                // chartControl5.ClearCache(chartControl5.d);
                //chartControl5.Series[0].DataSource=null;
                //chartControl5.Series[1].DataSource = null;
                //ht_ErrStateCounty_Wise.Clear();
                //dt_ErrStateCounty_Wise.Clear();

                //this.chartControl5.Series[0].Points.Clear();
                //chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                //chartControl5.ClearCache();

                //chartControl5.SeriesTemplate.ArgumentDataMember = "";
                // chartControl5.Series.Clear();
                //chartControl5.RuntimeHitTesting = true;
                //chartControl5.ClearSelection();
                //chartControl5.Series.BeginUpdate();

                //Bar Chart
                // chartControl5.DataSource = null;

                Hashtable ht_ErrOn_state_Barchart = new Hashtable();
                DataTable dt_ErrOn_state_Barchart = new DataTable();

                //ht_ErrOn_state_Barchart.Clear();
                //dt_ErrOn_state_Barchart.Clear();
                //dt_ErrStateCounty_Wise.Clear();
                //chartControl5.DataSource = null;

                ht_ErrOn_state_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_WISE");
                ht_ErrOn_state_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrOn_state_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrOn_state_Barchart.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                // ht_ErrOn_state_Barchart.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_ErrOn_state_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrOn_state_Barchart);



                chartControl5.DataSource = dt_ErrOn_state_Barchart;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Wise();

                // chartControl5.Series.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrState_Barchart = new Hashtable();
                DataTable dt_ErrState_Barchart = new DataTable();

                ht_ErrState_Barchart.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_WISE");
                ht_ErrState_Barchart.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrState_Barchart.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrState_Barchart.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                // ht_ErrState_Barchart.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_ErrState_Barchart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrState_Barchart);

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
        private void Bind_Bar_Chart_Error_On_State_Task_AND_Status_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_STATUS_WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_Errtask_Status_Wise.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errtask_Status_Wise);

                chartControl5.DataSource = dt_Errtask_Status_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_AND_Status_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_AND_Status_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status = new Hashtable();
                DataTable dt_task_Status = new DataTable();
                ht_task_Status.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_STATUS_WISE");
                ht_task_Status.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_task_Status.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                dt_task_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_Status);

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
        private void Bind_Bar_Chart_Error_On_State_Task_AND_State_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_Wise = new Hashtable();
                DataTable dt_Errtask_Status_Wise = new DataTable();

                ht_Errtask_Status_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE__WISE");
                ht_Errtask_Status_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_Wise.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_Errtask_Status_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                dt_Errtask_Status_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errtask_Status_Wise);

                chartControl5.DataSource = dt_Errtask_Status_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_AND_State_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_AND_State_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status = new Hashtable();
                DataTable dt_task_Status = new DataTable();
                ht_task_Status.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE__WISE");
                ht_task_Status.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_task_Status.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                dt_task_Status = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_Status);

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
        private void Bind_Bar_Chart_Error_On_Task_Status_State_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_Status_State_Wise = new Hashtable();
                DataTable dt_Errtask_Status_State_Wise = new DataTable();

                ht_Errtask_Status_State_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_WISE");
                ht_Errtask_Status_State_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_Status_State_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_Status_State_Wise.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_Errtask_Status_State_Wise.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_Errtask_Status_State_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                dt_Errtask_Status_State_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errtask_Status_State_Wise);

                chartControl5.DataSource = dt_Errtask_Status_State_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_Status_State_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_Status_State_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State = new Hashtable();
                DataTable dt_task_Status_State = new DataTable();
                ht_task_Status_State.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_WISE");
                ht_task_Status_State.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_task_Status_State.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_task_Status_State.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                dt_task_Status_State = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_Status_State);

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
        private void Bind_Bar_Chart_Error_On_Status_AND_State_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatus_State_Wise = new Hashtable();
                DataTable dt_ErrStatus_State_Wise = new DataTable();

                ht_ErrStatus_State_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_WISE");
                ht_ErrStatus_State_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatus_State_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatus_State_Wise.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_ErrStatus_State_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                dt_ErrStatus_State_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrStatus_State_Wise);

                chartControl5.DataSource = dt_ErrStatus_State_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Status_AND_State_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Status_AND_State_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State = new Hashtable();
                DataTable dt_task_Status_State = new DataTable();
                ht_task_Status_State.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_WISE");
                ht_task_Status_State.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_task_Status_State.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                dt_task_Status_State = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_Status_State);

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
        private void Bind_Bar_Chart_Error_On_State_AND_County_Wise()
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
                ht_ErrStateCounty_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_ErrStateCounty_Wise.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_ErrStateCounty_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrStateCounty_Wise);

                chartControl5.DataSource = dt_ErrStateCounty_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_State_Task_AND_County_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_State_Task_AND_County_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_StateCounty = new Hashtable();
                DataTable dt_StateCounty = new DataTable();
                ht_StateCounty.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATE_COUNTY_WISE");
                ht_StateCounty.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_StateCounty.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_StateCounty.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_StateCounty.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_StateCounty = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_StateCounty);

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
        private void Bind_Bar_Chart_Error_On_Task_Status_State_And_County_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_StatusState_County_Wise = new Hashtable();
                DataTable dt_Errtask_StatusState_County_Wise = new DataTable();

                ht_Errtask_StatusState_County_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_COUNTY_WISE");
                ht_Errtask_StatusState_County_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_StatusState_County_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_StatusState_County_Wise.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_Errtask_StatusState_County_Wise.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_Errtask_StatusState_County_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_Errtask_StatusState_County_Wise.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_Errtask_StatusState_County_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errtask_StatusState_County_Wise);

                chartControl5.DataSource = dt_Errtask_StatusState_County_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_Status_State_And_County_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_Status_State_And_County_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_Status_State_County = new Hashtable();
                DataTable dt_task_Status_State_County = new DataTable();
                ht_task_Status_State_County.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATUS_AND_STATE_COUNTY_WISE");
                ht_task_Status_State_County.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_Status_State_County.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_Status_State_County.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_task_Status_State_County.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_task_Status_State_County.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_task_Status_State_County.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_task_Status_State_County = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_Status_State_County);

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
        private void Bind_Bar_Chart_Error_On_Task_State_County_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_Errtask_State_County_Wise = new Hashtable();
                DataTable dt_Errtask_State_County_Wise = new DataTable();

                ht_Errtask_State_County_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_AND_COUNTY_WISE");
                ht_Errtask_State_County_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_Errtask_State_County_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_Errtask_State_County_Wise.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_Errtask_State_County_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_Errtask_State_County_Wise.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_Errtask_State_County_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Errtask_State_County_Wise);

                chartControl5.DataSource = dt_Errtask_State_County_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Task_State_County_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Task_State_County_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_task_State_County = new Hashtable();
                DataTable dt_task_State_County = new DataTable();
                ht_task_State_County.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_TASK_AND_STATE_AND_COUNTY_WISE");
                ht_task_State_County.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_task_State_County.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_task_State_County.Add("@Error_Task", int.Parse(ddl_State_Error_Task.SelectedValue.ToString()));
                ht_task_State_County.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_task_State_County.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_task_State_County = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_task_State_County);

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
        private void Bind_Bar_Chart_Error_On_Status_AND_State_AND_County_Wise()
        {
            try
            {
                //Bar Chart
                Hashtable ht_ErrStatus_State_County_Wise = new Hashtable();
                DataTable dt_ErrStatus_State_County_Wise = new DataTable();

                ht_ErrStatus_State_County_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_COUNTY_WISE");
                ht_ErrStatus_State_County_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatus_State_County_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatus_State_County_Wise.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_ErrStatus_State_County_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_ErrStatus_State_County_Wise.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_ErrStatus_State_County_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrStatus_State_County_Wise);

                chartControl5.DataSource = dt_ErrStatus_State_County_Wise;
                chartControl5.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl5.Series["Count"].ArgumentDataMember = "Error_State_Name";
                chartControl5.Series["Count"].ValueDataMembers[0] = "Error_State_Count";

                Bind_Line_Chart_Error_On_Status_AND_State_AND_County_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Bind_Line_Chart_Error_On_Status_AND_State_AND_County_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrStatusStateCounty_Wise = new Hashtable();
                DataTable dt_ErrStatusStateCounty_Wise = new DataTable();
                ht_ErrStatusStateCounty_Wise.Add("@Trans", "LINE_CHART_STATE_DATE_WISE_STATUS_AND_STATE_COUNTY_WISE");
                ht_ErrStatusStateCounty_Wise.Add("@Error_From_Date", txt_State_From_Date.Text);
                ht_ErrStatusStateCounty_Wise.Add("@Error_To_Date", txt_State_To_Date.Text);
                ht_ErrStatusStateCounty_Wise.Add("@Error_Status", int.Parse(ddl_State_Error_Status.SelectedValue.ToString()));
                ht_ErrStatusStateCounty_Wise.Add("@State_Id", int.Parse(ddl_Error_On_State.SelectedValue.ToString()));
                ht_ErrStatusStateCounty_Wise.Add("@County_Id", int.Parse(ddl_Error_On_State_County.SelectedValue.ToString()));
                dt_ErrStatusStateCounty_Wise = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrStatusStateCounty_Wise);

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



        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           // Chat_User newMDIChild = new Chat_User(1);
            // Set the Parent Form of the Child window.
            //newMDIChild.MdiParent = this;
            //// Display the new form.
            //newMDIChild.Show();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedIndex == 0)
            //{
            //    Error_Tab_Page = "Error_Tab";
            //    Ordermanagement_01.Chart.Error_Tab_Entry errordetails = new Ordermanagement_01.Chart.Error_Tab_Entry(Error_Tab_Page);
            //    errordetails.Show();

            //}
            //else if (tabControl1.SelectedIndex == 1)
            //{
            //    Error_Tab_Page = "Error_Description";
            //    Ordermanagement_01.Chart.Error_Desc_Entry errordetails = new Ordermanagement_01.Chart.Error_Desc_Entry(Error_Tab_Page);
            //    errordetails.Show();

            //}
            //else if (tabControl1.SelectedIndex == 2)
            //{
            //    Error_Tab_Page = "Error_On_User";
            //    Ordermanagement_01.Chart.Error_On_User_Entry errordetails = new Ordermanagement_01.Chart.Error_On_User_Entry(Error_Tab_Page);
            //    errordetails.Show();
            //}


        }












    }
}
