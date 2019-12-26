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

namespace Ordermanagement_01.Masters
{
    public partial class Chart_Report : Form
    {
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        DataTable dt_Bind_ErrTab = new DataTable();
        DataTable dt_Bind_Err_Desc= new DataTable();

        DataTable dt_Errror_Tab = new DataTable();
        DataTable dt_Errror_Desc = new DataTable();
        DataTable dt_Errror_On_User = new DataTable();

        DataTable dt_BarChart_Error_Desc = new DataTable();
        DataTable dt_LineChart_Error_Desc = new DataTable();


        public Chart_Report()
        {
            InitializeComponent();
        }

        private void Chart_Report_Load(object sender, EventArgs e)
        {
            Bind_ErrorTab_Task(ddl_Errors_Tab_Task);
            Bind_ErrorDesc_Task(ddl_Error_Desc_Task);
            Bind_ErrorOnUser_Task(ddl_Error_On_User_Task);
           
            //dbc.Bind_UserName_In_ErrorDashboard(ddl_ErrorOnUser);
            //Bind_UserName_In_ErrorDashboard(ddl_ErrorOnUser);

            string D1 = DateTime.Now.ToString("MM/dd/yyyy");
            string D2 = DateTime.Now.ToString("MM/dd/yyyy");

          
            txt_Error_Tab_From_Date.Format = DateTimePickerFormat.Custom;
            txt_Error_Tab_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_Error_Tab_From_Date.Text = D1;

            txt_Error_Tab_To_Date.Format = DateTimePickerFormat.Custom;
            txt_Error_Tab_To_Date.CustomFormat = "MM/dd/yyyy";
            txt_Error_Tab_To_Date.Text = D2;





            txt_Error_Desc_From_Date.Format = DateTimePickerFormat.Custom;
            txt_Error_Desc_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_Error_Desc_From_Date.Text = D1;

            txt_Error_Desc_To_Date.Format = DateTimePickerFormat.Custom;
            txt_Error_Desc_To_Date.CustomFormat = "MM/dd/yyyy";
            txt_Error_Desc_To_Date.Text = D2;



            txt_ErrorOnUser_From_Date.Format = DateTimePickerFormat.Custom;
            txt_ErrorOnUser_From_Date.CustomFormat = "MM/dd/yyyy";
            txt_ErrorOnUser_From_Date.Text = D1;

            txt_ErrorOnUser_To_Date.Format = DateTimePickerFormat.Custom;
            txt_ErrorOnUser_To_Date.CustomFormat = "MM/dd/yyyy";
            txt_ErrorOnUser_To_Date.Text = D2;



            this.WindowState = FormWindowState.Maximized;
        
           
        }

        private void Bind_ErrorTab_Task(ComboBox ddl_Errors_Tab_Task)
        {
            Hashtable ht_bind = new Hashtable();
            DataTable dt_bind = new DataTable();
            ht_bind.Add("@Trans", "BIND_ERROR_TASK");
            dt_bind = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_bind);
            DataRow dr = dt_bind.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_bind.Rows.InsertAt(dr, 0);
            ddl_Errors_Tab_Task.DataSource = dt_bind;
            ddl_Errors_Tab_Task.DisplayMember = "Order_Status";
            ddl_Errors_Tab_Task.ValueMember = "Order_Status_ID";

        }

        private void Bind_ErrorDesc_Task(ComboBox ddl_Error_Desc_Task)
        {
            Hashtable ht_bind = new Hashtable();
            DataTable dt_bind = new DataTable();
            ht_bind.Add("@Trans","BIND_ERROR_TASK");
            dt_bind = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_bind);
            DataRow dr = dt_bind.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
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
            dr[1] = "SELECT";
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


        // Error Tab All Task Wise
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
                Hashtable ht_ErrTab_Line = new Hashtable();
                DataTable dt_ErrTab_Line = new DataTable();
                ht_ErrTab_Line.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_ALL_TASK");
                ht_ErrTab_Line.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Line.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                dt_ErrTab_Line = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Line);
               // dt_Errror_Tab = dt_ErrTab;

                //chartControl1.DataSource = null;
                chartControl1.DataSource = dt_ErrTab_Line;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //  Error Tab Indivsual  Task Wise
        private void Bind_Bar_Chart_Error_Tab_Task_Wise()
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

                Bind_Line_Chart_Error_Tab_Task_Wise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Bind_Line_Chart_Error_Tab_Task_Wise()
        {
            try
            {

                //Bar Chart
                Hashtable ht_ErrTab_Linechart = new Hashtable();
                DataTable dt_ErrTab_Linechart = new DataTable();
                ht_ErrTab_Linechart.Add("@Trans", "LINE_CHART_ERROR_TAB_DATE_WISE_TASK_WISE");
                ht_ErrTab_Linechart.Add("@Error_From_Date", txt_Error_Tab_From_Date.Text);
                ht_ErrTab_Linechart.Add("@Error_To_Date", txt_Error_Tab_To_Date.Text);
                ht_ErrTab_Linechart.Add("@Error_Task", int.Parse(ddl_Errors_Tab_Task.SelectedValue.ToString()));
                dt_ErrTab_Linechart = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErrTab_Linechart);
                // dt_Errror_Tab = dt_ErrTab;

                chartControl1.DataSource = dt_ErrTab_Linechart;
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.Series["%"].ArgumentDataMember = "ErrorType";
                chartControl1.Series["%"].ValueDataMembers[0] = "Error_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Error Tab Sumbit
        private void btn_Error_Tab_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_Errors_Tab_Task.SelectedIndex == 0)
            {
                chartControl1.DataSource = null;
                Bind_Bar_Error_Tab_All_Task();
            }
            else if (ddl_Errors_Tab_Task.SelectedIndex > 0)
            {
                chartControl1.DataSource = null;
                Bind_Bar_Chart_Error_Tab_Task_Wise();
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

            cl.PaperKind = System.Drawing.Printing.PaperKind.A4;
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


        private void Bind_Bar_Error_Desc_All_Task()
        {
            try
            {
               
                //Bar Chart
                Hashtable ht_Select = new Hashtable();
                DataTable dt_Select = new DataTable();
               // ht_Select.Add("@Trans", "BAR_CHART_ERROR_DESCRIPTION_DATE_WISE_ALL_TASK");
                ht_Select.Add("@Trans", "LINE_CHART_ERROR_DESCRIPTION_DATE_WISE_ALL_TASK");
                ht_Select.Add("@Error_From_Date", txt_Error_Desc_From_Date.Text);
                ht_Select.Add("@Error_To_Date", txt_Error_Desc_To_Date.Text);
                dt_Select = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_Select);
                //dt = dt_Select;

                chartControl2.DataSource = dt_Select;

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

                Bind_Line_Error_Desc_All_Task();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btn_Error_Desc_Sumbit_Click(object sender, EventArgs e)
        {
            if (ddl_Error_Desc_Task.SelectedIndex == 0)
            {
                Bind_Bar_Error_Desc_All_Task();
            }
            else if (ddl_Error_Desc_Task.SelectedIndex > 0)
            {
                Bind_Bar_Error_Desc_All_Task_Wise();
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

            cl_1.PaperKind = System.Drawing.Printing.PaperKind.A3;
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

        // Error On User All Task Wise
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
                chartControl3.Series["%"].ArgumentDataMember ="Error_On_UserName";
                chartControl3.Series["%"].ValueDataMembers[0] ="Error_OnUser_Percent";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_ErrorOnUser_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_Error_On_User_Task.SelectedIndex == 0)
            {
                Bind_Bar_Chart_Error_On_User_All_Task();
            }
            else if (ddl_Error_On_User_Task.SelectedIndex > 0)
            {
                Bind_Bar_Chart_Error_On_User_Task_Wise();
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

            cl_2.PaperKind = System.Drawing.Printing.PaperKind.A3;
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






    }
}
