using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using ClosedXML.Excel;
namespace Ordermanagement_01.Reports
{

    public partial class Reports_Master : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();

        InfiniteProgressBar.frmProgress form = new InfiniteProgressBar.frmProgress();

        Hashtable ht = new Hashtable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtuserexport = new System.Data.DataTable();
        System.Data.DataTable dtordererror = new System.Data.DataTable();
        DataSet ds = new DataSet();
        Hashtable ht_Status = new Hashtable();
        System.Data.DataTable dt_Status = new System.Data.DataTable();
        private System.Drawing.Point progrbar;
        private System.Drawing.Point pt, pt1, from_lbl, from_lbl1, to_lbl, to_lbl1, form_pt, form1_pt, report_lbl, report_lbl1, from_date, from_date1, to_date,
           to_date1, report_grp, report_grp1, clear_btn, clear_btn1, refresh_btn, refresh_btn1, export_btn, export_btn1, report_pnl, report_pnl1
           , client_lbl, subpro_lbl, client_ddl, subpor_ddl, task_lbl, task_ddl, OrderTypr_Abr_ddl, OrderTypr_Abr_lbl;
        int Client, SubProcess;

        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        int Order_Id;
        int Pass_Sub_Process_Id;
        string Client_Order_no;
        int Order_Type;
        int User_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        string Path1, websiteName, sub_clientid;
        Tables CrTables;
        int Loged_In_User_Id;
        string Export_Title_Name, userroleid;

        int Ordertask_Id, Ordertype_Abr_Id; string Order_Task;

        System.Data.DataTable dtclientcount = new System.Data.DataTable();
        string Production_Date;
        public Reports_Master(int user_id, string User_Roleid, string PRODUCTION_DATE)
        {

            InitializeComponent();
            User_id = user_id;
            userroleid = User_Roleid;
            Loged_In_User_Id = user_id;
            dbc.BindOrderStatusRpt(ddl_Task);
            dbc.BindOrderStatusRpt_For_Check_list(ddl_Check_List_Task);
            dbc.Bind_Order_Progress_rpt(ddl_Status);
            dbc.BindUserName(ddl_EmployeeName);
            Production_Date = PRODUCTION_DATE;
            if (userroleid == "1")
            {
                dbc.BindClientName(ddl_ClientName);
                dbc.BindClientName(ddl_Client_Status);

                dbc.BindSubProcessName_rpt1(ddl_SubProcess);

            }
            else 
            {
                dbc.BindClientNo_for_Report(ddl_ClientName);
                dbc.BindClientNo_for_Report(ddl_Client_Status);

              //  dbc.BindSubProcessNo_rpt(ddl_SubProcess);

            }
            dbc.BindUserName(ddl_Check_List_UserName);

        }


        public void Logon_To_Crystal()
        {
            List<string> cl_Lgoin = Comclass.Crystal_report_Login();
            crConnectionInfo.ServerName = cl_Lgoin[0].ToString();
            crConnectionInfo.DatabaseName = cl_Lgoin[1].ToString();
            crConnectionInfo.UserID = cl_Lgoin[2].ToString();
            crConnectionInfo.Password = cl_Lgoin[3].ToString();
            CrTables = rptDoc.Database.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }


        }

        private void AddParent()
        {

            string sKeyTemp = "";
            tvwRightSide.Nodes.Clear();
            //  Hashtable ht = new Hashtable();
            // DataTable dt = new System.Data.DataTable();

            ht.Clear();
            dt.Clear();

            //for (int i = 0; i < dt.Rows.Count; i++)
            //  {
            sKeyTemp = "Reports";
            // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
            tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds(sKeyTemp);
            // }


        }


        private void AddChilds(string sKey)
        {
            ht.Clear();
            dt.Clear();
            //Hashtable ht = new Hashtable();
            // DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;


            tvwRightSide.Nodes[0].Nodes.Add("User Production Report");
            tvwRightSide.Nodes[0].Nodes.Add("User Production Count");
            tvwRightSide.Nodes[0].Nodes.Add("User Production Summary");

            //tvwRightSide.Nodes[0].Nodes.Add("Productivity Report");
            tvwRightSide.Nodes[0].Nodes.Add("Billing Report");
           // tvwRightSide.Nodes[0].Nodes.Add("Client Wise Production Report");
           // tvwRightSide.Nodes[0].Nodes.Add("Client Wise Production Count");
            tvwRightSide.Nodes[0].Nodes.Add("Orders Document List Report");
            tvwRightSide.Nodes[0].Nodes.Add("Orders Check List Report");
            tvwRightSide.Nodes[0].Nodes.Add("Orders Error Info Report");
            tvwRightSide.Nodes[0].Nodes.Add("Client wise Subscription Report");
            //tvwRightSide.Nodes[0].Nodes.Add("Employee wise Subscription Report");
            tvwRightSide.Nodes[0].Nodes.Add("Order Source Report");
            tvwRightSide.Nodes[0].Nodes.Add("Order Received Date Report");
            tvwRightSide.Nodes[0].Nodes.Add("Order Task wise Report");
            tvwRightSide.Nodes[0].Nodes.Add("User Break Report");

            tvwRightSide.ExpandAll();
        }

        private void Reports_Master_Load(object sender, EventArgs e)
        {
            //btn_treeview.Left = Width - 50;
            txt_Fromdate.Value = DateTime.Now;
            txt_Todate.Value = DateTime.Now;

            tvwRightSide.Visible = true;
            crViewer.Visible = false;

            AddParent();
            this.Text = "Report Master";

            dbc.Bind_OrderTask_Rept_ForCheck_list(ddl_Check_List_Task);
            dbc.Bind_Order_Type_Abbr_Rept_ForCheck_list(ddl_OrderTYpe_Abr);

            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;
        }

        protected void Productivity_Calculation()
        {
            int count_Date = 0;
            DateTime From_date = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime To_date = Convert.ToDateTime(txt_Todate.Text.ToString());
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            DateTime fromdate = Convert.ToDateTime(txt_Fromdate.Text, usDtfi);
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text, usDtfi);
            Hashtable ht_Produ = new Hashtable();
            System.Data.DataTable dt_Produ = new System.Data.DataTable();
            Hashtable ht_Produ1 = new Hashtable();
            System.Data.DataTable dt_Produ1 = new System.Data.DataTable();
            Hashtable ht_Count = new Hashtable();
            System.Data.DataTable dt_Count = new System.Data.DataTable();
            ht_Count.Add("@Trans", "Count_Production_date_All");
            ht_Count.Add("@From_date", fromdate);
            ht_Count.Add("@To_date", Todate);
            dt_Count = dataaccess.ExecuteSP("Sp_Productivity_Report", ht_Count);
            if (dt_Count.Rows.Count > 0)
            {
                count_Date = dt_Count.Rows.Count;
            }


            ht_Produ.Add("@From_date", fromdate);
            ht_Produ.Add("@To_date", Todate);
            dt_Produ = dataaccess.ExecuteSP("Sp_Productivity_Calc_Table", ht_Produ);
            dt_Produ = dataaccess.ExecuteSP("Sp_Rpt_Prodctivity_Calc", ht_Produ);
            ht_Produ1.Add("@Count_Date", Convert.ToString(count_Date));
            dt_Produ1 = dataaccess.ExecuteSP("Sp_Rpt_Productivity_Pivot", ht_Produ1);
            if (dt_Produ1.Rows.Count > 0)
            {
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.DataSource = dt_Produ1;
                // Grd_OrderTime.DataBind();


            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }

        }

        protected void Load_Grd_Master_Report()
        {
            if (Lbl_Title.Text == "")
            {
                // MessageBox.Show("Enter From and To date Properly");

            }
            else if (Lbl_Title.Text != "")
            {

                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                DateTime fromdate = Convert.ToDateTime(txt_Fromdate.Text, usDtfi);
                DateTime Todate = Convert.ToDateTime(txt_Todate.Text, usDtfi);


                if (fromdate <= Todate)
                {
                    int orderid = 0;
                    int Clientid = 0;
                    int SubProcessid = 0;
                    int Userid = 0;
                    int Status = 0;
                    int ProgressId = 0;
                    // dbc.BindOrder1(ddl_OrderNumber);
                    if (ddl_OrderNumber.SelectedIndex != 0 & ddl_OrderNumber.SelectedIndex != -1)
                    {
                        orderid = int.Parse(ddl_OrderNumber.SelectedValue.ToString());
                    }
                    if (ddl_ClientName.SelectedIndex != 0)
                    {
                        Clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                    }
                    if (ddl_SubProcess.SelectedIndex != -1)
                    {
                        SubProcessid = int.Parse(ddl_SubProcess.SelectedValue.ToString());
                    }
                    if (ddl_EmployeeName.SelectedIndex != 0)
                    {
                        Userid = int.Parse(ddl_EmployeeName.SelectedValue.ToString());
                    }
                    if (ddl_Status.SelectedIndex != 0)
                    {
                        ProgressId = int.Parse(ddl_Status.SelectedValue.ToString());
                    }
                    if (ddl_Task.SelectedIndex != 0)
                    {
                        Status = int.Parse(ddl_Task.SelectedValue.ToString());
                    }

                    ht.Clear();
                    dt.Clear();
                    dtuserexport.Clear();
                    if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId == 0 & Userid == 0)
                    {

                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "All");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);

                            ht.Add("@User_Id", Userid);
                            ht.Add("@Log_In_Userid", Loged_In_User_Id);

                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {


                            //rptDoc = new Reports.CrystalReport.User_Production_ReportNew();
                            rptDoc = new Reports.CrystalReport.CrystalReportUser_Prod();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "All");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", 0);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }





                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {

                            ht.Add("@Trans", "User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);

                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {


                            rptDoc = new Reports.CrystalReport.CrystalReportUser_Prod();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SELECT_USER_WISE");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }

                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {


                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Progress_ID_And_Used_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Progress_ID_And_Used_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }

                    }

                    else if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Status_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Status_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }

                    else if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Status_ID_and_User_Id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Status_ID_and_User_Id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Status_ID_and_Progress_Id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Status_ID_and_Progress_Id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }

                    else if (orderid == 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Status_ID_and_Progress_Id_And_UserID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Status_ID_and_Progress_Id_And_UserID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status == 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status == 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID_And_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID_And_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status == 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID_And_Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID_And_Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status == 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID_And_Progress_ID_And_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID_And_Progress_ID_And_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status != 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID_And_Status_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                            dt = dataaccess.ExecuteSP("Sp_Rpt_User_Order_TimeManagement", ht);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID_And_Status_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status != 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID_And_Status_ID_And_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID_And_Status_ID_And_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status != 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID_And_Status_ID_And_Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID_And_Status_ID_And_Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid == 0 & SubProcessid != 0 & Status != 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "SubProcess_ID_And_Status_ID_And_Progress_ID_and_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "SubProcess_ID_And_Status_ID_And_Progress_ID_and_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status == 0 & ProgressId == 0 & Userid == 0)
                    {

                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status == 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status == 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status == 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Progress_ID_and_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Progress_ID_and_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status != 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Status_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Status_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status != 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Status_ID_and_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Status_ID_and_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status != 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Status_ID_and_Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Status_ID_and_Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid == 0 & Status != 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Status_ID_and_Progress_ID_and_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Status_ID_and_Progress_ID_and_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID_and_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID_and_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID_and_Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID_and_Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID_and_Progress_ID_and_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID_and_Progress_ID_and_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID_and_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID_and_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID_and_Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID_and_Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid == 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID_and_Progress_ID_And_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Client_ID_And_Subprocess_ID_and_Status_ID_and_Progress_ID_And_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_User_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_User_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Progress_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Progress_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status == 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Progress_ID_And_User_Id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Progress_ID_And_User_Id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Status_ID");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Status_ID");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Status_ID_And_User_Id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Status_ID_And_User_Id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Status_ID_And_Progress_Id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Status_ID_And_Progress_Id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid == 0 & SubProcessid == 0 & Status != 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Status_ID_And_Progress_Id_and_User_Id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Status_ID_And_Progress_Id_and_User_Id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }



                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_User_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_User_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Progress_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Progress_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status == 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Progress_id_and_User_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Progress_id_and_User_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId == 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId == 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id_And_User_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id_And_User_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId != 0 & Userid == 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id_And_Progress_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id_And_Progress_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }
                    else if (orderid != 0 & Clientid != 0 & SubProcessid != 0 & Status != 0 & ProgressId != 0 & Userid != 0)
                    {
                        if (tvwRightSide.SelectedNode.Text == "User Production Report")
                        {
                            ht.Add("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id_And_Progress_id_and_user_id");
                            ht.Add("@Order_Id", orderid);
                            ht.Add("@Client_Id", Clientid);
                            ht.Add("@Subprocess_Id", SubProcessid);
                            ht.Add("@Order_Progress_Id", ProgressId);
                            ht.Add("@Order_Status_Id", Status);
                            ht.Add("@From_date", fromdate);
                            ht.Add("@To_date", Todate);
                            ht.Add("@User_Id", Userid);
                        }
                        else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                        {
                            rptDoc = new Reports.CrystalReport.User_Production_Count();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Trans", "Order_ID_And_Client_id_And_Subrpocess_id_And_Status_id_And_Progress_id_and_user_id");
                            rptDoc.SetParameterValue("@From_date", fromdate);
                            rptDoc.SetParameterValue("@To_date", Todate);
                            rptDoc.SetParameterValue("@Order_Id", orderid);
                            rptDoc.SetParameterValue("@Client_Id", Clientid);
                            rptDoc.SetParameterValue("@Subprocess_Id", SubProcessid);
                            rptDoc.SetParameterValue("@Order_Progress_Id", ProgressId);
                            rptDoc.SetParameterValue("@Order_Status_Id", Status);
                            rptDoc.SetParameterValue("@User_Id", Userid);
                            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
                        }
                    }

                    if (tvwRightSide.SelectedNode.Text == "User Production Report")
                    {

                        dt = dataaccess.ExecuteSP("Sp_Rpt_User_Order_TimeManagement", ht);
                        dtuserexport = dt;
                    }
                    else if (tvwRightSide.SelectedNode.Text == "User Production Count")
                    {



                        crViewer.ReportSource = rptDoc;


                    }
                    //Grd_OrderTime.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkSlateGray;
                    //Grd_OrderTime.EnableHeadersVisualStyles = false;
                    //Grd_OrderTime.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
                    if (dt.Rows.Count > 0)
                    {

                        lbl_Error.Visible = false;
                        Grd_OrderTime.DataSource = null;
                        Grd_OrderTime.AutoGenerateColumns = false;

                        Grd_OrderTime.ColumnCount = 19;
                        Grd_OrderTime.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 7.75F, FontStyle.Bold);
                        Grd_OrderTime.ColumnHeadersHeight = 40;

                        //Grd_OrderTime.Rows.Add();
                        Grd_OrderTime.Columns[0].Name = "SNo";
                        Grd_OrderTime.Columns[0].HeaderText = "S. No";
                        Grd_OrderTime.Columns[0].Width = 50;

                        Grd_OrderTime.Columns[1].Name = "ProductionDate";
                        Grd_OrderTime.Columns[1].HeaderText = "PRODUCTION DATE";
                        Grd_OrderTime.Columns[1].DataPropertyName = "Production_Date";
                        Grd_OrderTime.Columns[1].Width = 140;

                     
                        //visible code
                        //Grd_OrderTime.Columns[3].Name = "OrderNumber";
                        //Grd_OrderTime.Columns[3].HeaderText = "ORDER_NUMBER";
                        //Grd_OrderTime.Columns[3].DataPropertyName = "Client_Order_Number";
                        //Grd_OrderTime.Columns[3].Width = 195;
                        //Grd_OrderTime.Columns[3].Visible = false;
                        //original code
                        DataGridViewLinkColumn Order_link = new DataGridViewLinkColumn();
                        Grd_OrderTime.Columns.Add(Order_link);
                        Order_link.Name = "OrderNumber";
                        Order_link.HeaderText = "ORDER NUMBER";
                        Order_link.DataPropertyName = "Client_Order_Number";
                        Order_link.Width = 195;
                        Order_link.DisplayIndex = 2;

                        if (userroleid == "1")
                        {
                            Grd_OrderTime.Columns[2].Name = "Client";
                            Grd_OrderTime.Columns[2].HeaderText = "CLIENT NAME";
                            Grd_OrderTime.Columns[2].DataPropertyName = "Client_Name";
                            Grd_OrderTime.Columns[2].Width = 125;

                            Grd_OrderTime.Columns[3].Name = "SubProcess";
                            Grd_OrderTime.Columns[3].HeaderText = "SUB PROCESS";
                            Grd_OrderTime.Columns[3].DataPropertyName = "Sub_ProcessName";
                            Grd_OrderTime.Columns[3].Width = 250;
                        }
                        else 
                        {

                            Grd_OrderTime.Columns[2].Name = "Client";
                            Grd_OrderTime.Columns[2].HeaderText = "CLIENT NAME";
                            Grd_OrderTime.Columns[2].DataPropertyName = "Client_Number";
                            Grd_OrderTime.Columns[2].Width = 125;

                            Grd_OrderTime.Columns[3].Name = "SubProcess";
                            Grd_OrderTime.Columns[3].HeaderText = "SUB PROCESS";
                            Grd_OrderTime.Columns[3].DataPropertyName = "Subprocess_Number";
                            Grd_OrderTime.Columns[3].Width = 250;
                        }

                        Grd_OrderTime.Columns[4].Name = "OrderType";
                        Grd_OrderTime.Columns[4].HeaderText = "ORDER TYPE";
                        Grd_OrderTime.Columns[4].DataPropertyName = "Order_Type";
                        Grd_OrderTime.Columns[4].Width = 180;

                        Grd_OrderTime.Columns[5].Name = "Target Category";
                        Grd_OrderTime.Columns[5].HeaderText = "Target Category";
                        Grd_OrderTime.Columns[5].DataPropertyName = "Order_Source_Type_Name";

                        Grd_OrderTime.Columns[6].Name = "OrderStatus";
                        Grd_OrderTime.Columns[6].HeaderText = "ORDER STATUS";
                        Grd_OrderTime.Columns[6].DataPropertyName = "Task";
                        Grd_OrderTime.Columns[6].Width = 150;

                        Grd_OrderTime.Columns[7].Name = "Status";
                        Grd_OrderTime.Columns[7].HeaderText = "PROGRESS STATUS";
                        Grd_OrderTime.Columns[7].DataPropertyName = "Order_Status";
                        Grd_OrderTime.Columns[7].Width = 150;

                        Grd_OrderTime.Columns[8].Name = "StartTime";
                        Grd_OrderTime.Columns[8].HeaderText = "START TIME";
                        Grd_OrderTime.Columns[8].DataPropertyName = "Start_Time";
                        Grd_OrderTime.Columns[8].Width = 120;

                        Grd_OrderTime.Columns[9].Name = "EndTime";
                        Grd_OrderTime.Columns[9].HeaderText = "END TIME";
                        Grd_OrderTime.Columns[9].DataPropertyName = "End_Time";
                        Grd_OrderTime.Columns[9].Width = 120;

                        Grd_OrderTime.Columns[10].Name = "TotalTime";
                        Grd_OrderTime.Columns[10].HeaderText = "TOTAL TIME";
                        Grd_OrderTime.Columns[10].DataPropertyName = "Total_Time";
                        Grd_OrderTime.Columns[10].Width = 100;

                        Grd_OrderTime.Columns[11].Name = "OrderId";
                        Grd_OrderTime.Columns[11].HeaderText = "Order Id";
                        Grd_OrderTime.Columns[11].DataPropertyName = "Order_ID";
                        Grd_OrderTime.Columns[11].Visible = false;


                        //Grd_OrderTime.Columns[11].Name = "UserName";
                        //Grd_OrderTime.Columns[11].HeaderText = "USER NAME";
                        //Grd_OrderTime.Columns[11].DataPropertyName = "User_Name";
                        //Grd_OrderTime.Columns[11].Width = 125;

                        Grd_OrderTime.Columns[12].Name = "User_Name";
                        Grd_OrderTime.Columns[12].HeaderText = "EMPLOYEE NAME";
                        Grd_OrderTime.Columns[12].DataPropertyName = "User_Name";
                        Grd_OrderTime.Columns[12].Width = 125;

                        Grd_OrderTime.Columns[13].Name = "empcode";
                        Grd_OrderTime.Columns[13].HeaderText = "EMP CODE";
                        Grd_OrderTime.Columns[13].DataPropertyName = "DRN_Emp_Code";
                        Grd_OrderTime.Columns[13].Width = 100;

                        Grd_OrderTime.Columns[14].Name = "jobrole";
                        Grd_OrderTime.Columns[14].HeaderText = "JOB ROLE";
                        Grd_OrderTime.Columns[14].DataPropertyName = "Emp_Job_Role";
                        Grd_OrderTime.Columns[14].Width = 120;

                        Grd_OrderTime.Columns[15].Name = "shift";
                        Grd_OrderTime.Columns[15].HeaderText = "SHIFT";
                        Grd_OrderTime.Columns[15].DataPropertyName = "Shift_Type_Name";
                        Grd_OrderTime.Columns[15].Width = 100;

                        Grd_OrderTime.Columns[16].Name = "Reportingto1";
                        Grd_OrderTime.Columns[16].HeaderText = "REPORTING TO 1";
                        Grd_OrderTime.Columns[16].DataPropertyName = "Reporting_To_1";
                        Grd_OrderTime.Columns[16].Width = 140;

                        Grd_OrderTime.Columns[17].Name = "Reportingto1";
                        Grd_OrderTime.Columns[17].HeaderText = "REPORTING_TO_2";
                        Grd_OrderTime.Columns[17].DataPropertyName = "Reporting_To_2";
                        Grd_OrderTime.Columns[17].Width = 140;

                        Grd_OrderTime.Columns[18].Name = "Branch Name";
                        Grd_OrderTime.Columns[18].HeaderText = "BRANCH NAME";
                        Grd_OrderTime.Columns[18].DataPropertyName = "Branch_Name";
                        Grd_OrderTime.Columns[18].Width = 140;

                       
                        Grd_OrderTime.DataSource = dt;


                    }
                    else
                    {
                        Grd_OrderTime.Visible = true;
                        Grd_OrderTime.DataSource = null;

                    }

                }
                else
                {
                    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter date Properly')</script>", false);
                }
                for (int i = 0; i < Grd_OrderTime.Rows.Count; i++)
                {
                    Grd_OrderTime.Rows[i].Cells[0].Value = i + 1;
                }
            }


        }

        protected void Load_Rep_user_Production_Count()
        {
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            rptDoc = new Reports.CrystalReport.User_Production_Count();
            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Trans", "SELECT");
            rptDoc.SetParameterValue("@Fromdate", Fromdate);
            rptDoc.SetParameterValue("@Todate", Todate);
            crViewer.ReportSource = rptDoc;





        }

        public void Load_Order_Task_Status()
        {


            if (ddl_Client_Status.SelectedIndex != 0)
            {
                Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            }

            DateTime From_date = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime To_date = Convert.ToDateTime(txt_Todate.Text.ToString());
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            DateTime fromdate = Convert.ToDateTime(txt_Fromdate.Text, usDtfi);
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text, usDtfi);
            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();



            if (ddl_Client_Status.Text != "ALL")
            {
                ht_Status.Add("@Trans", "CLIENT_WISE");
            }
            else
            {
                ht_Status.Add("@Trans", "ALL_CLIENT_WISE");
            }
            ht_Status.Add("@Fromdate", fromdate);
            ht_Status.Add("@Todate", Todate);
            ht_Status.Add("@Clint", Client);

            ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
            dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);

            if (dt_Status.Rows.Count > 0)
            {
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.DataSource = dt_Status;
                // Grd_OrderTime.DataBind();


            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }



            if (ddl_Client_Status.SelectedIndex != 0)
            {
                Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            }






        }


        public void Load_User_Production_Summary_Report()
        {

            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();

            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");

            ht_Status.Clear();
            dt_Status.Clear();
            string Client, SubProcess;

            //Update User Order Task Comments 

            //Hashtable htupcomment = new Hashtable();
            //System.Data.DataTable dtupcomment = new System.Data.DataTable();

            //htupcomment.Add("@Trans", "INSERT");
            //dtupcomment = dataaccess.ExecuteSP("Sp_Temp_User_Order_Comments", htupcomment);



            if (ddl_Client_Status.SelectedIndex > 0)
            {

                Client = ddl_Client_Status.SelectedValue.ToString();
            }
            else
            {

                Client = "ALL";
            }

            if (ddl_Subprocess_Status.SelectedIndex > 0)
            {

                SubProcess = ddl_SubProcess.SelectedValue.ToString();
            }
            else
            {

                SubProcess = "ALL";
            }


            if (Client == "ALL")
            {
                ht_Status.Add("@Trans", "SELECT_DATE_RANGE");
            }
            else if (Client != "ALL" && SubProcess == "ALL")
            {
                ht_Status.Add("@Trans", "CLIENT_WISE");
            }
            else if (Client != "ALL" && SubProcess != "ALL")
            {
                ht_Status.Add("@Trans", "CLIENT_SUB_PROCESS_WISE");
            }


            ht_Status.Add("@Production_From_Date", Fromdate);
            ht_Status.Add("@Production_To_Date", Todate);
            ht_Status.Add("@Client", Client);
            ht_Status.Add("@Sub_Client", SubProcess);
            ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
            dt_Status = dataaccess.ExecuteSP("Sp_User_Production_Summary_Report_New", ht_Status);
            dtuserexport.Clear();
            dtuserexport = dt_Status;
            if (dt_Status.Rows.Count > 0)
            {



                lbl_Error.Visible = false;


                Grd_OrderTime.Rows.Clear();

                Grd_OrderTime.Visible = true;

                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.AutoGenerateColumns = false;

                Grd_OrderTime.ColumnCount = 98;
                //Grd_OrderTime.Rows.Add();

                //Grd_OrderTime.Columns[0].Name = "SNo";
                //Grd_OrderTime.Columns[0].HeaderText = "S. No";
                //Grd_OrderTime.Columns[0].Width = 50;
                //Grd_OrderTime.Columns[0].Visible = false;

                Grd_OrderTime.Columns[0].Name = "OrderId";
                Grd_OrderTime.Columns[0].HeaderText = "Order Id";
                Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                Grd_OrderTime.Columns[0].Width = 50;
                Grd_OrderTime.Columns[0].Visible = false;



                Grd_OrderTime.Columns[1].Name = "Client_Order_Number";
                Grd_OrderTime.Columns[1].HeaderText = "ORDER NUMBER";
                Grd_OrderTime.Columns[1].DataPropertyName = "Client_Order_Number";
                Grd_OrderTime.Columns[1].Width = 140;
                Grd_OrderTime.Columns[1].Visible = true;

                //DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                //Grd_OrderTime.Columns.Add(link);
                //link.HeaderText = "ORDER NUMBER";
                //link.DataPropertyName = "Client_Order_Number";
                //link.Width = 140;
                //link.DisplayIndex = 1;

                Grd_OrderTime.Columns[2].Name = "Recived_Date";
                Grd_OrderTime.Columns[2].HeaderText = "RECIVED DATE";
                Grd_OrderTime.Columns[2].DataPropertyName = "Recived_Date";
                Grd_OrderTime.Columns[2].Width = 140;

                if (userroleid == "1")
                {
                    Grd_OrderTime.Columns[3].Name = "Client_Name";
                    Grd_OrderTime.Columns[3].HeaderText = "CLIENT NAME";
                    Grd_OrderTime.Columns[3].DataPropertyName = "Client_Name";
                    Grd_OrderTime.Columns[3].Width = 140;




                    Grd_OrderTime.Columns[4].Name = "Sub_ProcessName";
                    Grd_OrderTime.Columns[4].HeaderText = "SUB CLIENT";
                    Grd_OrderTime.Columns[4].DataPropertyName = "Sub_ProcessName";
                    Grd_OrderTime.Columns[4].Width = 140;

                }
                else 
                {
                    Grd_OrderTime.Columns[3].Name = "Client_Name";
                    Grd_OrderTime.Columns[3].HeaderText = "CLIENT NUMBER";
                    Grd_OrderTime.Columns[3].DataPropertyName = "Client_Number";
                    Grd_OrderTime.Columns[3].Width = 140;




                    Grd_OrderTime.Columns[4].Name = "Sub_ProcessName";
                    Grd_OrderTime.Columns[4].HeaderText = "SUB CLIENT NUMBER";
                    Grd_OrderTime.Columns[4].DataPropertyName = "Subprocess_Number";
                    Grd_OrderTime.Columns[4].Width = 140;

                }


                Grd_OrderTime.Columns[5].Name = "Order_Type";
                Grd_OrderTime.Columns[5].HeaderText = "ORDER TYPE";
                Grd_OrderTime.Columns[5].DataPropertyName = "Order_Type";
                Grd_OrderTime.Columns[5].Width = 140;

                Grd_OrderTime.Columns[6].DataPropertyName = "Order_Source_Type_Name";
                Grd_OrderTime.Columns[6].Name = "Order_Source_Type_Name";
                Grd_OrderTime.Columns[6].HeaderText = "Target Category";
                Grd_OrderTime.Columns[6].Width = 140;

                Grd_OrderTime.Columns[7].Name = "Borrower_Name";
                Grd_OrderTime.Columns[7].HeaderText = "BORROWER NAME";
                Grd_OrderTime.Columns[7].DataPropertyName = "Borrower_Name";
                Grd_OrderTime.Columns[7].Width = 140;

                Grd_OrderTime.Columns[8].Name = "Abbreviation";
                Grd_OrderTime.Columns[8].HeaderText = "STATE";
                Grd_OrderTime.Columns[8].DataPropertyName = "Abbreviation";
                Grd_OrderTime.Columns[8].Width = 140;

                Grd_OrderTime.Columns[9].Name = "County";
                Grd_OrderTime.Columns[9].HeaderText = "COUNTY";
                Grd_OrderTime.Columns[9].DataPropertyName = "County";
                Grd_OrderTime.Columns[9].Width = 140;



                Grd_OrderTime.Columns[10].Name = "Current_Task";
                Grd_OrderTime.Columns[10].HeaderText = "CURRENT TASK";
                Grd_OrderTime.Columns[10].DataPropertyName = "Current_Task";
                Grd_OrderTime.Columns[10].Width = 140;

                Grd_OrderTime.Columns[11].Name = "Current_Status";
                Grd_OrderTime.Columns[11].HeaderText = "CURRENT STATUS";
                Grd_OrderTime.Columns[11].DataPropertyName = "Current_Status";
                Grd_OrderTime.Columns[11].Width = 140;

                Grd_OrderTime.Columns[12].Name = "Production_Date";
                Grd_OrderTime.Columns[12].HeaderText = "PRODUCTION DATE";
                Grd_OrderTime.Columns[12].DataPropertyName = "Production_Date";
                Grd_OrderTime.Columns[12].Width = 140;


                Grd_OrderTime.Columns[13].Name = "Address";
                Grd_OrderTime.Columns[13].HeaderText = "ADDRESS";
                Grd_OrderTime.Columns[13].DataPropertyName = "Address";
                Grd_OrderTime.Columns[13].Width = 140;

                Grd_OrderTime.Columns[14].Name = "Search_UserName";
                Grd_OrderTime.Columns[14].HeaderText = "SEARCH USERNAME";
                Grd_OrderTime.Columns[14].DataPropertyName = "SEARCH_USERNAME";
                Grd_OrderTime.Columns[14].Width = 125;
                //
                Grd_OrderTime.Columns[15].Name = "Searchempcode";
                Grd_OrderTime.Columns[15].HeaderText = "SEARCH USER EMPCODE";
                Grd_OrderTime.Columns[15].DataPropertyName = "Search_User_EmpCode";
                Grd_OrderTime.Columns[15].Width = 90;

                Grd_OrderTime.Columns[16].Name = "Searchjobrole";
                Grd_OrderTime.Columns[16].HeaderText = "SEARCH USER JOB ROLE";
                Grd_OrderTime.Columns[16].DataPropertyName = "Search_User_Job_Role";
                Grd_OrderTime.Columns[16].Width = 80;


                Grd_OrderTime.Columns[17].Name = "Searchshift";
                Grd_OrderTime.Columns[17].HeaderText = "SEARCH USER SHIFT";
                Grd_OrderTime.Columns[17].DataPropertyName = "Search_User_Shift";
                Grd_OrderTime.Columns[17].Width = 80;


                Grd_OrderTime.Columns[18].Name = "Searcreporting_to_1";
                Grd_OrderTime.Columns[18].HeaderText = "SEARCH USER REPORTING TO 1";
                Grd_OrderTime.Columns[18].DataPropertyName = "Search_User_Reporting_To_1";
                Grd_OrderTime.Columns[18].Width = 140;

                Grd_OrderTime.Columns[19].Name = "Searcreporting_to_2";
                Grd_OrderTime.Columns[19].HeaderText = "SEARCH USER REPORTING TO 2";
                Grd_OrderTime.Columns[19].DataPropertyName = "Search_User_Reporting_To_2";
                Grd_OrderTime.Columns[19].Width = 140;

                //

                Grd_OrderTime.Columns[20].Name = "SEARCH_STATUS";
                Grd_OrderTime.Columns[20].HeaderText = "SEARCH_STATUS";
                Grd_OrderTime.Columns[20].DataPropertyName = "SEARCH_STATUS";
                Grd_OrderTime.Columns[20].Width = 195;

                Grd_OrderTime.Columns[21].Name = "SEARCH_PRODUCTION_DATE";
                Grd_OrderTime.Columns[21].HeaderText = "SEARCH PRODUCTION DATE";
                Grd_OrderTime.Columns[21].DataPropertyName = "SEARCH_PRODUCTION_DATE";
                Grd_OrderTime.Columns[21].Width = 125;

                Grd_OrderTime.Columns[22].Name = "SEARCH_START_TIME";
                Grd_OrderTime.Columns[22].HeaderText = "SEARCH START TIME";
                Grd_OrderTime.Columns[22].DataPropertyName = "SEARCH_START_TIME";
                Grd_OrderTime.Columns[22].Width = 250;

                Grd_OrderTime.Columns[23].Name = "SEARCH_END_TIME";
                Grd_OrderTime.Columns[23].HeaderText = "SEARCH END TIME";
                Grd_OrderTime.Columns[23].DataPropertyName = "SEARCH_END_TIME";
                Grd_OrderTime.Columns[23].Width = 180;

                Grd_OrderTime.Columns[24].Name = "TOTAL_SEARCH_TIME";
                Grd_OrderTime.Columns[24].HeaderText = "TOTAL SEARCH TIME";
                Grd_OrderTime.Columns[24].DataPropertyName = "TOTAL_SEARCH_TIME";
                Grd_OrderTime.Columns[24].Width = 150;

                Grd_OrderTime.Columns[25].Name = "SEARCH_QC_USERNAME";
                Grd_OrderTime.Columns[25].HeaderText = "SEARCH_QC USERNAME";
                Grd_OrderTime.Columns[25].DataPropertyName = "SEARCH_QC_USERNAME";
                Grd_OrderTime.Columns[25].Width = 150;

                //
                Grd_OrderTime.Columns[26].Name = "SearchQCempcode";
                Grd_OrderTime.Columns[26].HeaderText = "SEARCH_QC USER EMPCODE";
                Grd_OrderTime.Columns[26].DataPropertyName = "SEARCH_QC_USER_EMP_CODE";
                Grd_OrderTime.Columns[26].Width = 100;

                Grd_OrderTime.Columns[27].Name = "SearchQCjobrole";
                Grd_OrderTime.Columns[27].HeaderText = "SEARCH_QC USER JOB ROLE";
                Grd_OrderTime.Columns[27].DataPropertyName = "SEARCH_QC_USER_JOB_ROLE";
                Grd_OrderTime.Columns[27].Width = 140;


                Grd_OrderTime.Columns[28].Name = "SearchQCshift";
                Grd_OrderTime.Columns[28].HeaderText = "SEARCH_QC USER SHIFT";
                Grd_OrderTime.Columns[28].DataPropertyName = "SEARCH_QC_USER_SHIFT";
                Grd_OrderTime.Columns[28].Width = 100;


                Grd_OrderTime.Columns[29].Name = "SearcQCreporting_to_1";
                Grd_OrderTime.Columns[29].HeaderText = "SEARCH_QC USER REPORTING TO 1";
                Grd_OrderTime.Columns[29].DataPropertyName = "Search_QC_User_Reporting_To_1";
                Grd_OrderTime.Columns[29].Width = 140;

                Grd_OrderTime.Columns[30].Name = "Searcreporting_to_2";
                Grd_OrderTime.Columns[30].HeaderText = "SEARCH_QC USER REPORTING TO 2";
                Grd_OrderTime.Columns[30].DataPropertyName = "Search_QC_User_Reporting_To_2";
                Grd_OrderTime.Columns[30].Width = 140;

                //

                Grd_OrderTime.Columns[31].Name = "SEARCH_QC_STATUS";
                Grd_OrderTime.Columns[31].HeaderText = "SEARCH_QC STATUS";
                Grd_OrderTime.Columns[31].DataPropertyName = "SEARCH_QC_STATUS";
                Grd_OrderTime.Columns[31].Width = 120;

                Grd_OrderTime.Columns[32].Name = "SEARCH_QC_PRODUCTION_DATE";
                Grd_OrderTime.Columns[32].HeaderText = "SEARCH_QC PRODUCTION_DATE";
                Grd_OrderTime.Columns[32].DataPropertyName = "SEARCH_QC_PRODUCTION_DATE";
                Grd_OrderTime.Columns[32].Width = 120;

                Grd_OrderTime.Columns[33].Name = "SEARCH_QC_START_TIME";
                Grd_OrderTime.Columns[33].HeaderText = "SEARCH_QC START_TIME";
                Grd_OrderTime.Columns[33].DataPropertyName = "SEARCH_QC_START_TIME";
                Grd_OrderTime.Columns[33].Width = 100;

                Grd_OrderTime.Columns[34].Name = "SEARCH_QC_END_TIME";
                Grd_OrderTime.Columns[34].HeaderText = "SEARCH_QC END TIME";
                Grd_OrderTime.Columns[34].DataPropertyName = "SEARCH_QC_END_TIME";
                Grd_OrderTime.Columns[34].Width = 100;

                Grd_OrderTime.Columns[35].Name = "TOTAL_SEARCH_QC_TIME";
                Grd_OrderTime.Columns[35].HeaderText = "TOTAL SEARCH_QC TIME";
                Grd_OrderTime.Columns[35].DataPropertyName = "TOTAL_SEARCH_QC_TIME";
                Grd_OrderTime.Columns[35].Width = 100;

                //
                Grd_OrderTime.Columns[36].Name = "TYPING_USERNAME";
                Grd_OrderTime.Columns[36].HeaderText = "TYPING USERNAME";
                Grd_OrderTime.Columns[36].DataPropertyName = "TYPING_USERNAME";
                Grd_OrderTime.Columns[36].Width = 100;

                Grd_OrderTime.Columns[37].Name = "typinguserempcode";
                Grd_OrderTime.Columns[37].HeaderText = "TYPING USER EMPCODE";
                Grd_OrderTime.Columns[37].DataPropertyName = "Typing_User_Emp_Code";
                Grd_OrderTime.Columns[37].Width = 100;


                Grd_OrderTime.Columns[38].Name = "typinguserempjobrole";
                Grd_OrderTime.Columns[38].HeaderText = "TYPING USER JOB ROLE";
                Grd_OrderTime.Columns[38].DataPropertyName = "Typing_User_Job_Role";
                Grd_OrderTime.Columns[38].Width = 100;

                Grd_OrderTime.Columns[39].Name = "typingusereshift";
                Grd_OrderTime.Columns[39].HeaderText = "TYPING USER SHIFT";
                Grd_OrderTime.Columns[39].DataPropertyName = "Typing_User_Shift";
                Grd_OrderTime.Columns[39].Width = 100;


                Grd_OrderTime.Columns[40].Name = "typinguserereportingto_1";
                Grd_OrderTime.Columns[40].HeaderText = "TYPING USER REPORTING TO 1";
                Grd_OrderTime.Columns[40].DataPropertyName = "Typing_User_Reporting_To_1";
                Grd_OrderTime.Columns[40].Width = 140;

                Grd_OrderTime.Columns[41].Name = "typinguserereportingto_2";
                Grd_OrderTime.Columns[41].HeaderText = "TYPING USER REPORTING TO 2";
                Grd_OrderTime.Columns[41].DataPropertyName = "Typing_User_Reporting_To_2";
                Grd_OrderTime.Columns[41].Width = 140;

                //
                Grd_OrderTime.Columns[42].Name = "TYPING_STATUS";
                Grd_OrderTime.Columns[42].HeaderText = "TYPING STATUS";
                Grd_OrderTime.Columns[42].DataPropertyName = "TYPING_STATUS";
                Grd_OrderTime.Columns[42].Width = 100;

                Grd_OrderTime.Columns[43].Name = "TYPING_PRODUCTION_DATE";
                Grd_OrderTime.Columns[43].HeaderText = "TYPING PRODUCTION DATE";
                Grd_OrderTime.Columns[43].DataPropertyName = "TYPING_PRODUCTION_DATE";
                Grd_OrderTime.Columns[43].Width = 100;


                Grd_OrderTime.Columns[44].Name = "TYPING_START_TIME";
                Grd_OrderTime.Columns[44].HeaderText = "TYPING START TIME";
                Grd_OrderTime.Columns[44].DataPropertyName = "TYPING_START_TIME";
                Grd_OrderTime.Columns[44].Width = 100;

                Grd_OrderTime.Columns[45].Name = "TYPING_END_TIME";
                Grd_OrderTime.Columns[45].HeaderText = "TYPING END TIME";
                Grd_OrderTime.Columns[45].DataPropertyName = "TYPING_END_TIME";
                Grd_OrderTime.Columns[45].Width = 100;

                Grd_OrderTime.Columns[46].Name = "TYPING_TOTAL_TIME";
                Grd_OrderTime.Columns[46].HeaderText = "TYPING TOTAL TIME";
                Grd_OrderTime.Columns[46].DataPropertyName = "TYPING_TOTAL_TIME";
                Grd_OrderTime.Columns[46].Width = 100;

                //

                Grd_OrderTime.Columns[47].Name = "TYPIG_QC_USERNAME";
                Grd_OrderTime.Columns[47].HeaderText = "TYPIG_QC USERNAME";
                Grd_OrderTime.Columns[47].DataPropertyName = "TYPIG_QC_USERNAME";
                Grd_OrderTime.Columns[47].Width = 100;

                Grd_OrderTime.Columns[48].Name = "typingqcempcode";
                Grd_OrderTime.Columns[48].HeaderText = "TYPIG_QC EMPCODE";
                Grd_OrderTime.Columns[48].DataPropertyName = "TYPING_QC_EMP_CODE";
                Grd_OrderTime.Columns[48].Width = 100;

                Grd_OrderTime.Columns[49].Name = "typingqcjobrole";
                Grd_OrderTime.Columns[49].HeaderText = "TYPIG_QC JOBROLE";
                Grd_OrderTime.Columns[49].DataPropertyName = "TYPING_QC_JOB_ROLE";
                Grd_OrderTime.Columns[49].Width = 100;

                Grd_OrderTime.Columns[50].Name = "typingqcshift";
                Grd_OrderTime.Columns[50].HeaderText = "TYPIG_QC SHIFT";
                Grd_OrderTime.Columns[50].DataPropertyName = "TYPING_QC_SHIFT";
                Grd_OrderTime.Columns[50].Width = 100;

                Grd_OrderTime.Columns[51].Name = "typingqcreportingto1";
                Grd_OrderTime.Columns[51].HeaderText = "TYPING_QC USER REPORTING TO 1";
                Grd_OrderTime.Columns[51].DataPropertyName = "TYPING_QC_User_Reporting_To_1";
                Grd_OrderTime.Columns[51].Width = 140;


                Grd_OrderTime.Columns[52].Name = "typingqcreportingto2";
                Grd_OrderTime.Columns[52].HeaderText = "TYPING_QC USER REPORTING TO 2";
                Grd_OrderTime.Columns[52].DataPropertyName = "TYPING_QC_User_Reporting_To_2";
                Grd_OrderTime.Columns[52].Width = 140;
                //

                Grd_OrderTime.Columns[53].Name = "TYPING_QC_STATUS";
                Grd_OrderTime.Columns[53].HeaderText = "TYPING_QC STATUS";
                Grd_OrderTime.Columns[53].DataPropertyName = "TYPING_QC_STATUS";
                Grd_OrderTime.Columns[53].Width = 100;

                Grd_OrderTime.Columns[54].Name = "TYPING_QC_PRODUCTION_DATE";
                Grd_OrderTime.Columns[54].HeaderText = "TYPING_QC PRODUCTION DATE";
                Grd_OrderTime.Columns[54].DataPropertyName = "TYPING_QC_PRODUCTION_DATE";
                Grd_OrderTime.Columns[54].Width = 100;


                Grd_OrderTime.Columns[55].Name = "TYPING_QC_START_TIME";
                Grd_OrderTime.Columns[55].HeaderText = "TYPING_QC START TIME";
                Grd_OrderTime.Columns[55].DataPropertyName = "TYPING_QC_START_TIME";
                Grd_OrderTime.Columns[55].Width = 100;

                Grd_OrderTime.Columns[56].Name = "TYPING_QC_END_TIME";
                Grd_OrderTime.Columns[56].HeaderText = "TYPING_QC END TIME";
                Grd_OrderTime.Columns[56].DataPropertyName = "TYPING_QC_END_TIME";
                Grd_OrderTime.Columns[56].Width = 100;

                Grd_OrderTime.Columns[57].Name = "TYPING_QC_TOTAL_TIME";
                Grd_OrderTime.Columns[57].HeaderText = "TYPING_QC TOTAL TIME";
                Grd_OrderTime.Columns[57].DataPropertyName = "TYPING_QC_TOTAL_TIME";
                Grd_OrderTime.Columns[57].Width = 100;

                //

                Grd_OrderTime.Columns[58].Name = "Final_Qc_UserName";
                Grd_OrderTime.Columns[58].HeaderText = "FINAL_QC USERNAME";
                Grd_OrderTime.Columns[58].DataPropertyName = "Final_Qc_UserName";
                Grd_OrderTime.Columns[58].Width = 100;

                Grd_OrderTime.Columns[59].Name = "FINAL_QC_EMP_CODE";
                Grd_OrderTime.Columns[59].HeaderText = "FINAL_QC EMP_CODE";
                Grd_OrderTime.Columns[59].DataPropertyName = "FINAL_QC_EMP_CODE";
                Grd_OrderTime.Columns[59].Width = 100;

                Grd_OrderTime.Columns[60].Name = "FINAL_QC_JOB_ROLE";
                Grd_OrderTime.Columns[60].HeaderText = "FINAL_QC JOB ROLE";
                Grd_OrderTime.Columns[60].DataPropertyName = "FINAL_QC_JOB_ROLE";
                Grd_OrderTime.Columns[60].Width = 100;

                Grd_OrderTime.Columns[61].Name = "FINAL_QC_USER_SHIFT";
                Grd_OrderTime.Columns[61].HeaderText = "FINAL_QC USER SHIFT";
                Grd_OrderTime.Columns[61].DataPropertyName = "FINAL_QC_USER_SHIFT";
                Grd_OrderTime.Columns[61].Width = 100;


                Grd_OrderTime.Columns[62].Name = "FINAL_QC_USER_REPORTING_TO_1";
                Grd_OrderTime.Columns[62].HeaderText = "FINAL_QC USER REPORTING TO 1";
                Grd_OrderTime.Columns[62].DataPropertyName = "FINAL_QC_USER_REPORTING_TO_1";
                Grd_OrderTime.Columns[62].Width = 140;


                Grd_OrderTime.Columns[63].Name = "FINAL_QC_REPORTING_TO_2";
                Grd_OrderTime.Columns[63].HeaderText = "FINAL_QC REPORTING TO 2";
                Grd_OrderTime.Columns[63].DataPropertyName = "FINAL_QC_REPORTING_TO_2";
                Grd_OrderTime.Columns[63].Width = 140;

                //

                Grd_OrderTime.Columns[64].Name = "Final_Qc_Progress";
                Grd_OrderTime.Columns[64].HeaderText = "FINAL_QC STATUS";
                Grd_OrderTime.Columns[64].DataPropertyName = "Final_Qc_Progress";
                Grd_OrderTime.Columns[64].Width = 100;

                Grd_OrderTime.Columns[65].Name = "Final_Qc_Production_Date";
                Grd_OrderTime.Columns[65].HeaderText = "FINAL_QC PRODUCTION DATE";
                Grd_OrderTime.Columns[65].DataPropertyName = "Final_Qc_Production_Date";
                Grd_OrderTime.Columns[65].Width = 100;


                Grd_OrderTime.Columns[66].Name = "Final_Qc_Start_Time";
                Grd_OrderTime.Columns[66].HeaderText = "FINAL_QC START TIME";
                Grd_OrderTime.Columns[66].DataPropertyName = "Final_Qc_Start_Time";
                Grd_OrderTime.Columns[66].Width = 100;

                Grd_OrderTime.Columns[67].Name = "Final_Qc_End_Time";
                Grd_OrderTime.Columns[67].HeaderText = "FINAL_QC END TIME";
                Grd_OrderTime.Columns[67].DataPropertyName = "Final_Qc_End_Time";
                Grd_OrderTime.Columns[67].Width = 100;

                Grd_OrderTime.Columns[68].Name = "TOTAL_FINAL_QC_TIME";
                Grd_OrderTime.Columns[68].HeaderText = "FINAL_QC TOTAL TIME";
                Grd_OrderTime.Columns[68].DataPropertyName = "TOTAL_FINAL_QC_TIME";
                Grd_OrderTime.Columns[68].Width = 100;

                //

                Grd_OrderTime.Columns[69].Name = "Exception_UserName";
                Grd_OrderTime.Columns[69].HeaderText = "EXCEPTION USERNAME";
                Grd_OrderTime.Columns[69].DataPropertyName = "Exception_UserName";
                Grd_OrderTime.Columns[69].Width = 100;

                Grd_OrderTime.Columns[70].Name = "Exception_User_Emp_Code";
                Grd_OrderTime.Columns[70].HeaderText = "EXCEPTION USER EMP CODE";
                Grd_OrderTime.Columns[70].DataPropertyName = "Exception_User_Emp_Code";
                Grd_OrderTime.Columns[70].Width = 100;

                Grd_OrderTime.Columns[71].Name = "Exception_User_Job_Role";
                Grd_OrderTime.Columns[71].HeaderText = "EXCEPTION USER JOB ROLE";
                Grd_OrderTime.Columns[71].DataPropertyName = "Exception_User_Job_Role";
                Grd_OrderTime.Columns[71].Width = 100;

                Grd_OrderTime.Columns[72].Name = "Exception_User_Shift";
                Grd_OrderTime.Columns[72].HeaderText = "EXCEPTION USER SHIFT";
                Grd_OrderTime.Columns[72].DataPropertyName = "Exception_User_Shift";
                Grd_OrderTime.Columns[72].Width = 100;

                Grd_OrderTime.Columns[73].Name = "EXception_User_Reporting_To_1";
                Grd_OrderTime.Columns[73].HeaderText = "EXCEPTION USER REPORTING TO 1";
                Grd_OrderTime.Columns[73].DataPropertyName = "EXception_User_Reporting_To_1";
                Grd_OrderTime.Columns[73].Width = 140;

                Grd_OrderTime.Columns[74].Name = "EXception_User_Reporting_To_2";
                Grd_OrderTime.Columns[74].HeaderText = "EXCEPTION USER REPORTING TO 2";
                Grd_OrderTime.Columns[74].DataPropertyName = "EXception_User_Reporting_To_2";
                Grd_OrderTime.Columns[74].Width = 140;
                //

                Grd_OrderTime.Columns[75].Name = "Exception_Progress";
                Grd_OrderTime.Columns[75].HeaderText = "EXCEPTION STATUS";
                Grd_OrderTime.Columns[75].DataPropertyName = "Exception_Progress";
                Grd_OrderTime.Columns[75].Width = 100;

                Grd_OrderTime.Columns[76].Name = "Exception_Production_Date";
                Grd_OrderTime.Columns[76].HeaderText = "EXCEPTION PRODUCTION DATE";
                Grd_OrderTime.Columns[76].DataPropertyName = "Exception_Production_Date";
                Grd_OrderTime.Columns[76].Width = 100;


                Grd_OrderTime.Columns[77].Name = "Exception_Start_Time";
                Grd_OrderTime.Columns[77].HeaderText = "EXCEPTION START TIME";
                Grd_OrderTime.Columns[77].DataPropertyName = "Exception_Start_Time";
                Grd_OrderTime.Columns[77].Width = 100;

                Grd_OrderTime.Columns[78].Name = "Exception_End_Time";
                Grd_OrderTime.Columns[78].HeaderText = "EXCEPTION END TIME";
                Grd_OrderTime.Columns[78].DataPropertyName = "Exception_End_Time";
                Grd_OrderTime.Columns[78].Width = 100;

                Grd_OrderTime.Columns[79].Name = "TOTAL_EXCEPTION_TIME";
                Grd_OrderTime.Columns[79].HeaderText = "EXCEPTION TOTAL TIME";
                Grd_OrderTime.Columns[79].DataPropertyName = "TOTAL_EXCEPTION_TIME";
                Grd_OrderTime.Columns[79].Width = 100;

                //
                Grd_OrderTime.Columns[80].Name = "UPLOAD_USERNAME";
                Grd_OrderTime.Columns[80].HeaderText = "UPLOAD USERNAME";
                Grd_OrderTime.Columns[80].DataPropertyName = "UPLOAD_USERNAME";
                Grd_OrderTime.Columns[80].Width = 100;

                Grd_OrderTime.Columns[81].Name = "uploaduserempcode";
                Grd_OrderTime.Columns[81].HeaderText = "UPLOAD USER EMPCODE";
                Grd_OrderTime.Columns[81].DataPropertyName = "UPLOAD_USER_EMP_CODE";
                Grd_OrderTime.Columns[81].Width = 100;

                Grd_OrderTime.Columns[82].Name = "uploadjobrole";
                Grd_OrderTime.Columns[82].HeaderText = "UPLOAD USER JOB ROLE";
                Grd_OrderTime.Columns[82].DataPropertyName = "UPLOAD_USER_JOB_ROLE";
                Grd_OrderTime.Columns[82].Width = 120;

                Grd_OrderTime.Columns[83].Name = "uploadusershift";
                Grd_OrderTime.Columns[83].HeaderText = "UPLOAD USER SHIFT";
                Grd_OrderTime.Columns[83].DataPropertyName = "UPLOAD_USER_SHIFT";
                Grd_OrderTime.Columns[83].Width = 100;

                Grd_OrderTime.Columns[84].Name = "uploaduserReportingto1";
                Grd_OrderTime.Columns[84].HeaderText = "UPLOAD_USER REPORTING TO 1";
                Grd_OrderTime.Columns[84].DataPropertyName = "UPLOAD_USER_REPORTING_TO_1";
                Grd_OrderTime.Columns[84].Width = 140;

                Grd_OrderTime.Columns[85].Name = "uploaduserReportingto2";
                Grd_OrderTime.Columns[85].HeaderText = "UPLOAD_USER REPORTING TO 2";
                Grd_OrderTime.Columns[85].DataPropertyName = "UPLOAD_USER_REPORTING_TO_2";
                Grd_OrderTime.Columns[85].Width = 140;

                //
                Grd_OrderTime.Columns[86].Name = "UPLOAD_STATUS";
                Grd_OrderTime.Columns[86].HeaderText = "UPLOAD STATUS";
                Grd_OrderTime.Columns[86].DataPropertyName = "UPLOAD_STATUS";
                Grd_OrderTime.Columns[86].Width = 100;

                Grd_OrderTime.Columns[87].Name = "UPLOAD_PRODUCTION_DATE";
                Grd_OrderTime.Columns[87].HeaderText = "UPLOAD PRODUCTION DATE";
                Grd_OrderTime.Columns[87].DataPropertyName = "UPLOAD_PRODUCTION_DATE";
                Grd_OrderTime.Columns[87].Width = 100;

                Grd_OrderTime.Columns[88].Name = "UPLOAD_START_TIME";
                Grd_OrderTime.Columns[88].HeaderText = "UPLOAD START TIME";
                Grd_OrderTime.Columns[88].DataPropertyName = "UPLOAD_START_TIME";
                Grd_OrderTime.Columns[88].Width = 100;

                Grd_OrderTime.Columns[89].Name = "UPLOAD_END_TIME";
                Grd_OrderTime.Columns[89].HeaderText = "UPLOAD END TIME";
                Grd_OrderTime.Columns[89].DataPropertyName = "UPLOAD_END_TIME";
                Grd_OrderTime.Columns[89].Width = 100;

                Grd_OrderTime.Columns[90].Name = "TOTAL_UPLAODING_TIME";
                Grd_OrderTime.Columns[90].HeaderText = "TOTAL UPLAODING TIME";
                Grd_OrderTime.Columns[90].DataPropertyName = "TOTAL_UPLAODING_TIME";
                Grd_OrderTime.Columns[90].Width = 100;

                Grd_OrderTime.Columns[91].Name = "Search";
                Grd_OrderTime.Columns[91].HeaderText = "SEARCH COMMENTS";
                Grd_OrderTime.Columns[91].DataPropertyName = "Search";
                Grd_OrderTime.Columns[91].Width = 100;


                Grd_OrderTime.Columns[92].Name = "Search_Qc";
                Grd_OrderTime.Columns[92].HeaderText = "SEARCH_QC COMMENTS";
                Grd_OrderTime.Columns[92].DataPropertyName = "Search_Qc";
                Grd_OrderTime.Columns[92].Width = 100;


                Grd_OrderTime.Columns[93].Name = "Typing";
                Grd_OrderTime.Columns[93].HeaderText = "TYPING COMMENTS";
                Grd_OrderTime.Columns[93].DataPropertyName = "Typing";
                Grd_OrderTime.Columns[93].Width = 100;


                Grd_OrderTime.Columns[94].Name = "Typing_Qc";
                Grd_OrderTime.Columns[94].HeaderText = "TYPING_QC COMMENTS";
                Grd_OrderTime.Columns[94].DataPropertyName = "Typing_Qc";
                Grd_OrderTime.Columns[94].Width = 100;

                Grd_OrderTime.Columns[95].Name = "Final_Qc";
                Grd_OrderTime.Columns[95].HeaderText = "FINAL QC COMMENTS";
                Grd_OrderTime.Columns[95].DataPropertyName = "Final_Qc";
                Grd_OrderTime.Columns[95].Width = 100;


                Grd_OrderTime.Columns[96].Name = "Upload";
                Grd_OrderTime.Columns[96].HeaderText = "UPLOAD COMMENTS";
                Grd_OrderTime.Columns[96].DataPropertyName = "Upload";
                Grd_OrderTime.Columns[96].Width = 100;

                Grd_OrderTime.Columns[97].Name = "Exception";
                Grd_OrderTime.Columns[97].HeaderText = "EXCEPTION COMMENTS";
                Grd_OrderTime.Columns[97].DataPropertyName = "Exception";
                Grd_OrderTime.Columns[97].Width = 100;



                Grd_OrderTime.DataSource = dt_Status;





            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }
        }


        public void Load_Date_Wise_Order_Status_Report()
        {

            if (ddl_Client_Status.SelectedIndex != 0)
            {
                Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            }

            if (ddl_Subprocess_Status.SelectedIndex > 0)
            {
                SubProcess = int.Parse(ddl_Subprocess_Status.SelectedValue.ToString());

            }
            else
            {

                SubProcess = 0;
            }
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());

            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();


            ht_Status.Clear();
            dt_Status.Clear();
            if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text == "ALL")
            {
                ht_Status.Add("@Trans", "Order_Status_Report__ClientWise");
            }
            else if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text != "ALL")
            {
                ht_Status.Add("@Trans", "Order_Status_Report__Client_SubprocessWise");
            }
            else
            {
                ht_Status.Add("@Trans", "Order_Status_Report_All_My_ClientWise");
            }

            ht_Status.Add("@Fromdate", Fromdate);
            ht_Status.Add("@Todate", Todate);
            ht_Status.Add("@Clint", Client);
            ht_Status.Add("@Subprocess_Id", SubProcess);
            ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
            dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);

            if (dt_Status.Rows.Count > 0)
            {




                lbl_Error.Visible = false;
                Grd_OrderTime.Visible = true;

                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.AutoGenerateColumns = false;

                Grd_OrderTime.ColumnCount = 26;
                //Grd_OrderTime.Rows.Add();
                Grd_OrderTime.Columns[0].Name = "OrderId";
                Grd_OrderTime.Columns[0].HeaderText = "Order ID";
                Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                Grd_OrderTime.Columns[0].Visible = false;
                Grd_OrderTime.Columns[0].Width = 50;


                Grd_OrderTime.Columns[1].Name = "Order_Number";
                Grd_OrderTime.Columns[1].HeaderText = "ORDER NUMBER";
                Grd_OrderTime.Columns[1].DataPropertyName = "Order_Number";
                Grd_OrderTime.Columns[1].Width = 140;
                Grd_OrderTime.Columns[1].Visible = false;

                DataGridViewLinkColumn order_number = new DataGridViewLinkColumn();
                Grd_OrderTime.Columns.Add(order_number);
                order_number.Name = "Order_Number";
                order_number.HeaderText = "ORDER NUMBER";
                order_number.DataPropertyName = "Order_Number";
                order_number.Width = 140;
                order_number.DisplayIndex = 1;

                Grd_OrderTime.Columns[2].Name = "Ref_number";
                Grd_OrderTime.Columns[2].HeaderText = "Ref number";
                Grd_OrderTime.Columns[2].DataPropertyName = "Ref_number";
                Grd_OrderTime.Columns[2].Width = 125;

                Grd_OrderTime.Columns[3].Name = "Date";
                Grd_OrderTime.Columns[3].HeaderText = "RECIVED DATE";
                Grd_OrderTime.Columns[3].DataPropertyName = "Date";
                Grd_OrderTime.Columns[3].Width = 195;

                Grd_OrderTime.Columns[4].Name = "Client_name";
                Grd_OrderTime.Columns[4].HeaderText = "CLIENT NAME";
                Grd_OrderTime.Columns[4].DataPropertyName = "Client_name";
                Grd_OrderTime.Columns[4].Width = 125;

                Grd_OrderTime.Columns[5].Name = "Sub_client";
                Grd_OrderTime.Columns[5].HeaderText = "SUB CLIENT";
                Grd_OrderTime.Columns[5].DataPropertyName = "Sub_client";
                Grd_OrderTime.Columns[5].Width = 250;

                Grd_OrderTime.Columns[6].Name = "Order_type";
                Grd_OrderTime.Columns[6].HeaderText = "ORDER TYPE";
                Grd_OrderTime.Columns[6].DataPropertyName = "Order_type";
                Grd_OrderTime.Columns[6].Width = 180;

                Grd_OrderTime.Columns[7].Name = "Borrower_Name";
                Grd_OrderTime.Columns[7].HeaderText = "BORROWER NAME";
                Grd_OrderTime.Columns[7].DataPropertyName = "Borrower_Name";
                Grd_OrderTime.Columns[7].Width = 150;

                Grd_OrderTime.Columns[8].Name = "Address";
                Grd_OrderTime.Columns[8].HeaderText = "BORROWER ADDRESS";
                Grd_OrderTime.Columns[8].DataPropertyName = "Address";
                Grd_OrderTime.Columns[8].Width = 150;

                Grd_OrderTime.Columns[9].Name = "Abbreviation";
                Grd_OrderTime.Columns[9].HeaderText = "STATE";
                Grd_OrderTime.Columns[9].DataPropertyName = "Abbreviation";
                Grd_OrderTime.Columns[9].Width = 120;

                Grd_OrderTime.Columns[10].Name = "County";
                Grd_OrderTime.Columns[10].HeaderText = "COUNTY";
                Grd_OrderTime.Columns[10].DataPropertyName = "County";
                Grd_OrderTime.Columns[10].Width = 120;

                Grd_OrderTime.Columns[11].Name = "Current_Task";
                Grd_OrderTime.Columns[11].HeaderText = "CURRENT TASK Task";
                Grd_OrderTime.Columns[11].DataPropertyName = "Current_Task";
                Grd_OrderTime.Columns[11].Width = 100;

                Grd_OrderTime.Columns[12].Name = "Order_Status";
                Grd_OrderTime.Columns[12].HeaderText = "ORDER STATUS";
                Grd_OrderTime.Columns[12].DataPropertyName = "Order_Status";
                Grd_OrderTime.Columns[12].Width = 100;

                Grd_OrderTime.Columns[13].Name = "Abstarctor_Status";
                Grd_OrderTime.Columns[13].HeaderText = "ABSTRACTOR STATUS";
                Grd_OrderTime.Columns[13].DataPropertyName = "Abstarctor_Status";
                Grd_OrderTime.Columns[13].Width = 100;

                Grd_OrderTime.Columns[14].Name = "Production_Date";
                Grd_OrderTime.Columns[14].HeaderText = "PRODUCTION DATE";
                Grd_OrderTime.Columns[14].DataPropertyName = "Production_Date";
                Grd_OrderTime.Columns[14].Width = 100;

                Grd_OrderTime.Columns[15].Name = "County_type";
                Grd_OrderTime.Columns[15].HeaderText = "COUNTY TYPE";
                Grd_OrderTime.Columns[15].DataPropertyName = "County_type";
                Grd_OrderTime.Columns[15].Width = 100;

                Grd_OrderTime.Columns[16].Name = "Source";
                Grd_OrderTime.Columns[16].HeaderText = "SOURCE";
                Grd_OrderTime.Columns[16].DataPropertyName = "Source";
                Grd_OrderTime.Columns[16].Width = 100;


                Grd_OrderTime.Columns[17].Name = "Search_Cost";
                Grd_OrderTime.Columns[17].HeaderText = "SEARCH COST";
                Grd_OrderTime.Columns[17].DataPropertyName = "Search_Cost";
                Grd_OrderTime.Columns[17].Width = 100;

                Grd_OrderTime.Columns[18].Name = "Copy_Cost";
                Grd_OrderTime.Columns[18].HeaderText = "COPY COST";
                Grd_OrderTime.Columns[18].DataPropertyName = "Copy_Cost";
                Grd_OrderTime.Columns[18].Width = 100;

                Grd_OrderTime.Columns[19].Name = "Abstractor Cost";
                Grd_OrderTime.Columns[19].HeaderText = "ABSTRACTOR COST";
                Grd_OrderTime.Columns[19].DataPropertyName = "Abstractor_Cost";
                Grd_OrderTime.Columns[19].Width = 100;

                Grd_OrderTime.Columns[20].Name = "No_Of_pages";
                Grd_OrderTime.Columns[20].HeaderText = "NO OF PAGES";
                Grd_OrderTime.Columns[20].DataPropertyName = "No_Of_pages";
                Grd_OrderTime.Columns[20].Width = 100;

                Grd_OrderTime.Columns[21].Name = "No_of_Hits";
                Grd_OrderTime.Columns[21].HeaderText = "No Of Hits";
                Grd_OrderTime.Columns[21].DataPropertyName = "No_of_Hits";
                Grd_OrderTime.Columns[21].Width = 100;

                Grd_OrderTime.Columns[22].Name = "No_Of_Documents";
                Grd_OrderTime.Columns[22].HeaderText = "NO OF DOCUMENTS";
                Grd_OrderTime.Columns[22].DataPropertyName = "No_Of_Documents";
                Grd_OrderTime.Columns[22].Width = 100;

                Grd_OrderTime.Columns[23].Name = "Website_Name";
                Grd_OrderTime.Columns[23].HeaderText = "Website Name";
                Grd_OrderTime.Columns[23].DataPropertyName = "Website_Name";
                Grd_OrderTime.Columns[23].Width = 100;

                Grd_OrderTime.Columns[24].Name = "Effectivedate";
                Grd_OrderTime.Columns[24].HeaderText = "EFFECTIVE DATE";
                Grd_OrderTime.Columns[24].DataPropertyName = "Effectivedate";
                Grd_OrderTime.Columns[24].Width = 100;

                Grd_OrderTime.Columns[25].Name = "UserName";
                Grd_OrderTime.Columns[25].HeaderText = "USER NAME";
                Grd_OrderTime.Columns[25].DataPropertyName = "UserName";
                Grd_OrderTime.Columns[25].Width = 100;

                Grd_OrderTime.DataSource = dt_Status;



            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;


            }



            Hashtable ht_Status1 = new Hashtable();
            System.Data.DataTable dt_Status1 = new System.Data.DataTable();



            if (ddl_Client_Status.Text != "ALL")
            {
                ht_Status1.Add("@Trans", "CLIENT_WISE_PRODUCTION_COUNT");
            }
            else if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text != "ALL")
            {
                ht_Status1.Add("@Trans", "CLIENT_SUBPROCESS_WISE_PRODUCTION_COUNT");
            }

            else
            {
                ht_Status1.Add("@Trans", "MY_ALL_CLIENT_WISE_PRODUCTION_COUNT");
                ht_Status1.Add("@Log_In_Userid", Loged_In_User_Id);
            }

            ht_Status1.Add("@Fromdate", Fromdate);
            ht_Status1.Add("@Todate", Todate);
            ht_Status1.Add("@Clint", Client);
            ht_Status1.Add("@Subprocess_Id", SubProcess);
            dt_Status1 = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status1);
            dtclientcount = dt_Status1;




        }
        public void Load_Date_Wise_Order_Status_Report_Count()
        {

            if (ddl_Client_Status.SelectedIndex != 0)
            {
                Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            }

            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());

            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();


            ht_Status.Clear();
            dt_Status.Clear();
            if (ddl_Client_Status.Text != "ALL")
            {
                ht_Status.Add("@Trans", "GET_ORDER_STATUS_REP_COUNT_CLIENT_WISE");
            }
            else
            {
                ht_Status.Add("@Trans", "GET_ORDER_STATUS_REP_COUNT_ALL_CLIENT_WISE");
            }

            ht_Status.Add("@Fromdate", Fromdate);
            ht_Status.Add("@Todate", Todate);
            ht_Status.Add("@Clint", Client);

            dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);

            if (dt_Status.Rows.Count > 0)
            {
                // grid_Count.Visible = true;
                // grid_Count.DataSource = dt_Status;
                // Grd_OrderTime.DataBind();


            }
            else
            {
                // grid_Count.Visible = false;
                //grid_Count.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }
        }

        public void Load_Client_Production_Count()
        {
            if (ddl_Client_Status.SelectedIndex != 0)
            {
                Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            }
            if (ddl_Subprocess_Status.SelectedIndex > 0)
            {
                SubProcess = int.Parse(ddl_Subprocess_Status.SelectedValue.ToString());

            }
            else
            {

                SubProcess = 0;
            }

            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();

            dt_Status.Rows.Clear();

            if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text == "ALL")
            {
                ht_Status.Add("@Trans", "CLIENT_WISE_PRODUCTION_COUNT");
            }
            else if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text != "ALL")
            {
                ht_Status.Add("@Trans", "CLIENT_SUBPROCESS_WISE_PRODUCTION_COUNT");
            }

            else
            {
                ht_Status.Add("@Trans", "MY_ALL_CLIENT_WISE_PRODUCTION_COUNT");
            }
            ht_Status.Add("@Fromdate", Fromdate);
            ht_Status.Add("@Todate", Todate);
            ht_Status.Add("@Clint", Client);
            ht_Status.Add("@Subprocess_Id", SubProcess);

            ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
            dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);

            if (dt_Status.Rows.Count > 0)
            {
                lbl_Error.Visible = false;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.DataSource = null;

                //Grd_OrderTime.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                //Grd_OrderTime.EnableHeadersVisualStyles = false;
                //Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.ColumnCount = 17;

                Grd_OrderTime.Columns[0].Name = "Client";
                Grd_OrderTime.Columns[0].HeaderText = "Client name";
                Grd_OrderTime.Columns[0].DataPropertyName = "Client";


                Grd_OrderTime.Columns[1].Name = "R_Current_Day";
                Grd_OrderTime.Columns[1].HeaderText = "R-" + txt_Todate.Text;
                Grd_OrderTime.Columns[1].DataPropertyName = "R_Current_Day";


                String dy = Todate.Day.ToString();
                String mn = Todate.Month.ToString();
                String yy = Todate.Year.ToString();
                Grd_OrderTime.Columns[2].Name = "R_MTD";
                Grd_OrderTime.Columns[2].HeaderText = "R(MTD)" + "-" + mn + "/" + yy + "";
                Grd_OrderTime.Columns[2].DataPropertyName = "R_MTD";


                Grd_OrderTime.Columns[3].Name = "C_Current_Day";
                Grd_OrderTime.Columns[3].HeaderText = "C-" + txt_Todate.Text;
                Grd_OrderTime.Columns[3].DataPropertyName = "C_Current_Day";


                Grd_OrderTime.Columns[4].Name = "C_MTD";
                Grd_OrderTime.Columns[4].HeaderText = "C(MTD)" + "-" + mn + "/" + yy + "";
                Grd_OrderTime.Columns[4].DataPropertyName = "C_MTD";

                Grd_OrderTime.Columns[5].Name = "Search";
                Grd_OrderTime.Columns[5].HeaderText = "Search";
                Grd_OrderTime.Columns[5].DataPropertyName = "Search";



                Grd_OrderTime.Columns[6].Name = "Search_Qc";
                Grd_OrderTime.Columns[6].HeaderText = "Search Qc";
                Grd_OrderTime.Columns[6].DataPropertyName = "Search_Qc";

                Grd_OrderTime.Columns[7].Name = "Typing";
                Grd_OrderTime.Columns[7].HeaderText = "Typing";
                Grd_OrderTime.Columns[7].DataPropertyName = "Typing";


                Grd_OrderTime.Columns[8].Name = "Typing_QC";
                Grd_OrderTime.Columns[8].HeaderText = "Typing_QC";
                Grd_OrderTime.Columns[8].DataPropertyName = "Typing_QC";


                Grd_OrderTime.Columns[9].Name = "Upload";
                Grd_OrderTime.Columns[9].HeaderText = "Upload";
                Grd_OrderTime.Columns[9].DataPropertyName = "Upload";

                Grd_OrderTime.Columns[10].Name = "Abstractor";
                Grd_OrderTime.Columns[10].HeaderText = "Abstractor";
                Grd_OrderTime.Columns[10].DataPropertyName = "Abstractor";


                Grd_OrderTime.Columns[11].Name = "AFA";
                Grd_OrderTime.Columns[11].HeaderText = "AFA";
                Grd_OrderTime.Columns[11].DataPropertyName = "AFA";


                Grd_OrderTime.Columns[12].Name = "WFA";
                Grd_OrderTime.Columns[12].HeaderText = "WFA";
                Grd_OrderTime.Columns[12].DataPropertyName = "WFA";



                Grd_OrderTime.Columns[13].Name = "Clarification";
                Grd_OrderTime.Columns[13].HeaderText = "Clarification";
                Grd_OrderTime.Columns[13].DataPropertyName = "Clarification";


                Grd_OrderTime.Columns[14].Name = "Hold";
                Grd_OrderTime.Columns[14].HeaderText = "Hold";
                Grd_OrderTime.Columns[14].DataPropertyName = "Hold";


                Grd_OrderTime.Columns[15].Name = "Cancelled";
                Grd_OrderTime.Columns[15].HeaderText = "Cancelled";
                Grd_OrderTime.Columns[15].DataPropertyName = "Cancelled";

                Grd_OrderTime.Columns[16].Name = "Client_Id";
                Grd_OrderTime.Columns[16].HeaderText = "Client_Id";
                Grd_OrderTime.Columns[16].DataPropertyName = "Client_Id";
                Grd_OrderTime.Columns[16].Visible = false;


                if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text != "ALL")
                {

                    Grd_OrderTime.Columns[17].Name = "Subprocess_Id";
                    Grd_OrderTime.Columns[17].HeaderText = "Subprocess_Id";
                    Grd_OrderTime.Columns[17].DataPropertyName = "Subprocess_Id";
                    Grd_OrderTime.Columns[17].Visible = false;

                    Pass_Sub_Process_Id = 1;
                }
                else
                {

                    Pass_Sub_Process_Id = 0;
                    //Grd_OrderTime.Columns[17].Name = "Subprocess_Id";
                    //Grd_OrderTime.Columns[17].HeaderText = "Subprocess_Id";
                    //Grd_OrderTime.Columns[17].DataPropertyName = "Subprocess_Id";
                    //Grd_OrderTime.Columns[17].Visible = false;

                }





                Grd_OrderTime.Visible = true;
                Grd_OrderTime.DataSource = dt_Status;



            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;

            }



        }


        public void Load_Billing_Report()
        {
            if (ddl_Client_Status.SelectedIndex != 0)
            {
                Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            }

            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();

            dt_Status.Rows.Clear();

            if (ddl_Client_Status.Text != "SELECT" && ddl_Client_Status.Text!="0")
            {
                ht_Status.Add("@Trans", "Client");
            }             
            else
            {
                ht_Status.Add("@Trans", "ALL");
            }
            ht_Status.Add("@From_Date", Fromdate);
            ht_Status.Add("@To_Date", Todate);
            ht_Status.Add("@Client_Id", Client);

            ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
            dt_Status = dataaccess.ExecuteSP("Sp_Billing_Report", ht_Status);

            if (dt_Status.Rows.Count > 0)
            {
                lbl_Error.Visible = false;
                Grd_OrderTime.Visible = true;

                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.AutoGenerateColumns = false;

                Grd_OrderTime.ColumnCount = 33;

                Grd_OrderTime.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 7.75F, FontStyle.Bold);
                Grd_OrderTime.ColumnHeadersHeight = 40;

                //Grd_OrderTime.Rows.Add();
                Grd_OrderTime.Columns[0].Name = "Order_ID";
                Grd_OrderTime.Columns[0].HeaderText = "Order_ID";
                Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                Grd_OrderTime.Columns[0].Width = 50;
                Grd_OrderTime.Columns[0].Visible = false;

                Grd_OrderTime.Columns[1].Name = "Order_Number";
                Grd_OrderTime.Columns[1].HeaderText = "Order_Number";
                Grd_OrderTime.Columns[1].DataPropertyName = "Client_Order_Number";
                Grd_OrderTime.Columns[1].Width = 140;
                Grd_OrderTime.Columns[1].Visible = false;

                DataGridViewLinkColumn client_order = new DataGridViewLinkColumn();
                Grd_OrderTime.Columns.Add(client_order);
                client_order.DisplayIndex = 1;
                client_order.Name = "Order_Number";
                client_order.HeaderText = "Order Number";
                client_order.DataPropertyName = "Client_Order_Number";
                client_order.Width = 140;


                Grd_OrderTime.Columns[2].Name = "Ref_number";
                Grd_OrderTime.Columns[2].HeaderText = "Ref number";
                Grd_OrderTime.Columns[2].DataPropertyName = "Client_Order_Ref";
                Grd_OrderTime.Columns[2].Width = 125;

                Grd_OrderTime.Columns[3].Name = "Date";
                Grd_OrderTime.Columns[3].HeaderText = "Recived Date";
                Grd_OrderTime.Columns[3].DataPropertyName = "Received_Date";
                Grd_OrderTime.Columns[3].Width = 195;

                if (userroleid == "1")
                {
                    Grd_OrderTime.Columns[4].Name = "Client_name";
                    Grd_OrderTime.Columns[4].HeaderText = "CLIENT NAME";
                    Grd_OrderTime.Columns[4].DataPropertyName = "Client_name";
                    Grd_OrderTime.Columns[4].Width = 125;

                    Grd_OrderTime.Columns[5].Name = "Sub_client";
                    Grd_OrderTime.Columns[5].HeaderText = "SUB PROCESS";
                    Grd_OrderTime.Columns[5].DataPropertyName = "Sub_ProcessName";
                    Grd_OrderTime.Columns[5].Width = 250;
                }
                else 
                {

                    Grd_OrderTime.Columns[4].Name = "Client_name";
                    Grd_OrderTime.Columns[4].HeaderText = "CLIENT NAME";
                    Grd_OrderTime.Columns[4].DataPropertyName = "Client_Number";
                    Grd_OrderTime.Columns[4].Width = 125;

                    Grd_OrderTime.Columns[5].Name = "Sub_client";
                    Grd_OrderTime.Columns[5].HeaderText = "SUB PROCESS";
                    Grd_OrderTime.Columns[5].DataPropertyName = "Subprocess_Number";
                    Grd_OrderTime.Columns[5].Width = 250;
                }
            

                Grd_OrderTime.Columns[6].Name = "Order_type";
                Grd_OrderTime.Columns[6].HeaderText = "ORDER TYPE";
                Grd_OrderTime.Columns[6].DataPropertyName = "Order_Type";
                Grd_OrderTime.Columns[6].Width = 180;

                Grd_OrderTime.Columns[7].Name = "Target Category";
                Grd_OrderTime.Columns[7].HeaderText = "Target Category";
                Grd_OrderTime.Columns[7].DataPropertyName = "Order_Source_Type_Name";
                Grd_OrderTime.Columns[7].Width = 140;


                Grd_OrderTime.Columns[8].Name = "Borrower_Name";
                Grd_OrderTime.Columns[8].HeaderText = "Borrower Name";
                Grd_OrderTime.Columns[8].DataPropertyName = "Borrower_Name";
                Grd_OrderTime.Columns[8].Width = 150;

                Grd_OrderTime.Columns[9].Name = "Address";
                Grd_OrderTime.Columns[9].HeaderText = "Barrower Address";
                Grd_OrderTime.Columns[9].DataPropertyName = "Address";
                Grd_OrderTime.Columns[9].Width = 150;

                Grd_OrderTime.Columns[10].Name = "Abbreviation";
                Grd_OrderTime.Columns[10].HeaderText = "STATE";
                Grd_OrderTime.Columns[10].DataPropertyName = "Abbreviation";
                Grd_OrderTime.Columns[10].Width = 120;

                Grd_OrderTime.Columns[11].Name = "County";
                Grd_OrderTime.Columns[11].HeaderText = "COUNTY";
                Grd_OrderTime.Columns[11].DataPropertyName = "County";
                Grd_OrderTime.Columns[11].Width = 120;

                Grd_OrderTime.Columns[12].Name = "Current_Task";
                Grd_OrderTime.Columns[12].HeaderText = "Current Task";
                Grd_OrderTime.Columns[12].DataPropertyName = "Current_Task";
                Grd_OrderTime.Columns[12].Width = 100;

                Grd_OrderTime.Columns[13].Name = "Order_Status";
                Grd_OrderTime.Columns[13].HeaderText = "Order Status";
                Grd_OrderTime.Columns[13].DataPropertyName = "Order_Status";
                Grd_OrderTime.Columns[13].Width = 100;

                Grd_OrderTime.Columns[14].Name = "Completed_Date";
                Grd_OrderTime.Columns[14].HeaderText = "Completed Date";
                Grd_OrderTime.Columns[14].DataPropertyName = "Completed_Date";
                Grd_OrderTime.Columns[14].Width = 100;

                Grd_OrderTime.Columns[15].Name = "County_type";
                Grd_OrderTime.Columns[15].HeaderText = "County type";
                Grd_OrderTime.Columns[15].DataPropertyName = "County_Type";
                Grd_OrderTime.Columns[15].Width = 100;

                Grd_OrderTime.Columns[16].Name = "Source";
                Grd_OrderTime.Columns[16].HeaderText = "Source";
                Grd_OrderTime.Columns[16].DataPropertyName = "Source";
                Grd_OrderTime.Columns[16].Width = 100;

                Grd_OrderTime.Columns[17].Name = "Order_Cost";
                Grd_OrderTime.Columns[17].HeaderText = "Order Cost";
                Grd_OrderTime.Columns[17].DataPropertyName = "Order_Cost";
                Grd_OrderTime.Columns[17].Width = 100;

                Grd_OrderTime.Columns[18].Name = "Search_Cost";
                Grd_OrderTime.Columns[18].HeaderText = "Search Cost";
                Grd_OrderTime.Columns[18].DataPropertyName = "Search_Cost";
                Grd_OrderTime.Columns[18].Width = 100;

                Grd_OrderTime.Columns[19].Name = "Copy_Cost";
                Grd_OrderTime.Columns[19].HeaderText = "Copy Cost";
                Grd_OrderTime.Columns[19].DataPropertyName = "Copy_Cost";
                Grd_OrderTime.Columns[19].Width = 100;

                Grd_OrderTime.Columns[20].Name = "Abstractor Cost";
                Grd_OrderTime.Columns[20].HeaderText = "Abstractor_Cost";
                Grd_OrderTime.Columns[20].DataPropertyName = "Abstractor_Cost";
                Grd_OrderTime.Columns[20].Width = 100;

                Grd_OrderTime.Columns[21].Name = "No_Of_pages";
                Grd_OrderTime.Columns[21].HeaderText = "No Of pages";
                Grd_OrderTime.Columns[21].DataPropertyName = "No_Of_pages";
                Grd_OrderTime.Columns[21].Width = 100;

                Grd_OrderTime.Columns[22].Name = "No_of_Hits";
                Grd_OrderTime.Columns[22].HeaderText = "No Of Hits";
                Grd_OrderTime.Columns[22].DataPropertyName = "No_of_Hits";
                Grd_OrderTime.Columns[22].Width = 100;

                Grd_OrderTime.Columns[23].Name = "No_Of_Documents";
                Grd_OrderTime.Columns[23].HeaderText = "No Of Documents";
                Grd_OrderTime.Columns[23].DataPropertyName = "No_Of_Documents";
                Grd_OrderTime.Columns[23].Width = 100;

                Grd_OrderTime.Columns[24].Name = "Website_Name";
                Grd_OrderTime.Columns[24].HeaderText = "Website Name";
                Grd_OrderTime.Columns[24].DataPropertyName = "Website_Name";
                Grd_OrderTime.Columns[24].Width = 100;

                Grd_OrderTime.Columns[25].Name = "Effectivedate";
                Grd_OrderTime.Columns[25].HeaderText = "Effective Date";
                Grd_OrderTime.Columns[25].DataPropertyName = "Effective_date";
                Grd_OrderTime.Columns[25].Width = 100;

                //Grd_OrderTime.Columns[25].Name = "UserName";
                //Grd_OrderTime.Columns[25].HeaderText = "User Name";
                //Grd_OrderTime.Columns[25].DataPropertyName = "User_Name";
                //Grd_OrderTime.Columns[25].Width = 100;

                Grd_OrderTime.Columns[26].Name = "User_Name";
                Grd_OrderTime.Columns[26].HeaderText = "Employee Name";
                Grd_OrderTime.Columns[26].DataPropertyName = "User_Name";
                Grd_OrderTime.Columns[26].Width = 110;



                Grd_OrderTime.Columns[27].Name = "DRN_Emp_Code";
                Grd_OrderTime.Columns[27].HeaderText = "Emp Code";
                Grd_OrderTime.Columns[27].DataPropertyName = "DRN_Emp_Code";
                Grd_OrderTime.Columns[27].Width = 100;


                Grd_OrderTime.Columns[28].Name = "Emp_Job_Role";
                Grd_OrderTime.Columns[28].HeaderText = "Emp Job Role";
                Grd_OrderTime.Columns[28].DataPropertyName = "Emp_Job_Role";
                Grd_OrderTime.Columns[28].Width = 100;

                Grd_OrderTime.Columns[29].Name = "Shift_Type_Name";
                Grd_OrderTime.Columns[29].HeaderText = "SHIFT";
                Grd_OrderTime.Columns[29].DataPropertyName = "Shift_Type_Name";
                Grd_OrderTime.Columns[29].Width = 100;

                Grd_OrderTime.Columns[30].Name = "Reporting_To_1";
                Grd_OrderTime.Columns[30].HeaderText = "Reporting Level 1";
                Grd_OrderTime.Columns[30].DataPropertyName = "Reporting_To_1";
                Grd_OrderTime.Columns[30].Width = 125;

                Grd_OrderTime.Columns[31].Name = "Reporting_To_2";
                Grd_OrderTime.Columns[31].HeaderText = "Reporting Level 2";
                Grd_OrderTime.Columns[31].DataPropertyName = "Reporting_To_2";
                Grd_OrderTime.Columns[31].Width = 125;

                Grd_OrderTime.Columns[32].Name = "Branch Name";
                Grd_OrderTime.Columns[32].HeaderText = "Branch Name";
                Grd_OrderTime.Columns[32].DataPropertyName = "Branch_Name";
                Grd_OrderTime.Columns[32].Width = 110;


                Grd_OrderTime.Visible = true;
                Grd_OrderTime.DataSource = dt_Status;



            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;

            }


        }

        public void Load_Order_Document_List_Report()
        {


            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();

            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");
            if (ddl_Client_Status.Text == "ALL" || ddl_Client_Status.SelectedIndex==0)
            {
                ht_Status.Add("@Trans", "SELECT");
            }
            else
            {

                ht_Status.Add("@Trans", "SELECT_CLIENT_WISE");
                ht_Status.Add("@Client_id", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
            }
            ht_Status.Add("@From_date", From_Date);
            ht_Status.Add("@To_Date", To_Date);

            ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
            ht_Status.Add("@Work_Type_Id", 1);

            dt_Status = dataaccess.ExecuteSP("Sp_rpt_Document_List", ht_Status);
            dtuserexport = dt_Status;
            if (dt_Status.Rows.Count > 0)
            {
                lbl_Error.Visible = false;
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.AutoGenerateColumns = true;
                Grd_OrderTime.Refresh();

                
                //Grd_OrderTime.Columns[1].Visible = false;

                Grd_OrderTime.ColumnCount = 21;
                Grd_OrderTime.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 7.75F, FontStyle.Bold);
                Grd_OrderTime.ColumnHeadersHeight = 40;

                //Grd_OrderTime.Rows.Add();
                Grd_OrderTime.Columns[0].Name = "Order_ID";
                Grd_OrderTime.Columns[0].HeaderText = "Order_ID";
                Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                Grd_OrderTime.Columns[0].Width = 50;
                Grd_OrderTime.Columns[0].Visible = false;

                Grd_OrderTime.Columns[1].Name = "Order_Number";
                Grd_OrderTime.Columns[1].HeaderText = "Order Number";
                Grd_OrderTime.Columns[1].DataPropertyName = "Client_Order_Number";
                Grd_OrderTime.Columns[1].Width = 140;


                Grd_OrderTime.Columns[2].Name = "Ref_number";
                Grd_OrderTime.Columns[2].HeaderText = "Ref number";
                Grd_OrderTime.Columns[2].DataPropertyName = "Client_Order_Ref";
                Grd_OrderTime.Columns[2].Width = 125;

                Grd_OrderTime.Columns[3].Name = "Date";
                Grd_OrderTime.Columns[3].HeaderText = "Recived Date";
                Grd_OrderTime.Columns[3].DataPropertyName = "Received_Date";
                Grd_OrderTime.Columns[3].Width = 195;

                if (userroleid == "1")
                {
                    Grd_OrderTime.Columns[4].Name = "Client_name";
                    Grd_OrderTime.Columns[4].HeaderText = "CLIENT NAME";
                    Grd_OrderTime.Columns[4].DataPropertyName = "Client_name";
                    Grd_OrderTime.Columns[4].Width = 125;

                    Grd_OrderTime.Columns[5].Name = "Sub_client";
                    Grd_OrderTime.Columns[5].HeaderText = "SUB PROCESS";
                    Grd_OrderTime.Columns[5].DataPropertyName = "Sub_ProcessName";
                    Grd_OrderTime.Columns[5].Width = 250;
                }
                else 
                {
                    Grd_OrderTime.Columns[4].Name = "Client_Number";
                    Grd_OrderTime.Columns[4].HeaderText = "CLIENT NAME";
                    Grd_OrderTime.Columns[4].DataPropertyName = "Client_Number";
                    Grd_OrderTime.Columns[4].Width = 130;

                    Grd_OrderTime.Columns[5].Name = "Subprocess_Number";
                    Grd_OrderTime.Columns[5].HeaderText = "SUB PROCESS";
                    Grd_OrderTime.Columns[5].DataPropertyName = "Subprocess_Number";
                    Grd_OrderTime.Columns[5].Width = 130;

                }

              

                Grd_OrderTime.Columns[6].Name = "Order_type";
                Grd_OrderTime.Columns[6].HeaderText = "ORDER TYPE";
                Grd_OrderTime.Columns[6].DataPropertyName = "Order_Type";
                Grd_OrderTime.Columns[6].Width = 180;

                Grd_OrderTime.Columns[7].Name = "Borrower_Name";
                Grd_OrderTime.Columns[7].HeaderText = "Borrower Name";
                Grd_OrderTime.Columns[7].DataPropertyName = "Borrower_Name";
                Grd_OrderTime.Columns[7].Width = 150;

                Grd_OrderTime.Columns[8].Name = "Address";
                Grd_OrderTime.Columns[8].HeaderText = "Barrower Address";
                Grd_OrderTime.Columns[8].DataPropertyName = "Address";
                Grd_OrderTime.Columns[8].Width = 150;

                Grd_OrderTime.Columns[9].Name = "Abbreviation";
                Grd_OrderTime.Columns[9].HeaderText = "STATE";
                Grd_OrderTime.Columns[9].DataPropertyName = "Abbreviation";
                Grd_OrderTime.Columns[9].Width = 120;

                Grd_OrderTime.Columns[10].Name = "County";
                Grd_OrderTime.Columns[10].HeaderText = "COUNTY";
                Grd_OrderTime.Columns[10].DataPropertyName = "County";
                Grd_OrderTime.Columns[10].Width = 120;

                Grd_OrderTime.Columns[11].Name = "City";
                Grd_OrderTime.Columns[11].HeaderText = "City";
                Grd_OrderTime.Columns[11].DataPropertyName = "City";
                Grd_OrderTime.Columns[11].Width = 100;

                Grd_OrderTime.Columns[12].Name = "Order_Status";
                Grd_OrderTime.Columns[12].HeaderText = "Order Status";
                Grd_OrderTime.Columns[12].DataPropertyName = "Order_Status";
                Grd_OrderTime.Columns[12].Width = 100;

                //Grd_OrderTime.Columns[13].Name = "User_Name";
                //Grd_OrderTime.Columns[13].HeaderText = "User Name";
                //Grd_OrderTime.Columns[13].DataPropertyName = "User_Name";
                //Grd_OrderTime.Columns[13].Width = 100;

                Grd_OrderTime.Columns[13].Name = "Employee_Name";
                Grd_OrderTime.Columns[13].HeaderText = "Employee Name";
                Grd_OrderTime.Columns[13].DataPropertyName = "Employee_Name";
                Grd_OrderTime.Columns[13].Width = 100;

                Grd_OrderTime.Columns[14].Name = "DRN_Emp_Code";
                Grd_OrderTime.Columns[14].HeaderText = "DRN_Emp_Code";
                Grd_OrderTime.Columns[14].DataPropertyName = "DRN_Emp_Code";
                Grd_OrderTime.Columns[14].Width = 100;

                Grd_OrderTime.Columns[15].Name = "Emp_Job_Role";
                Grd_OrderTime.Columns[15].HeaderText = "Emp_Job_Role";
                Grd_OrderTime.Columns[15].DataPropertyName = "Emp_Job_Role";
                Grd_OrderTime.Columns[15].Width = 100;

                Grd_OrderTime.Columns[16].Name = "Shift_Type_Name";
                Grd_OrderTime.Columns[16].HeaderText = "Shift_Type_Name";
                Grd_OrderTime.Columns[16].DataPropertyName = "Shift_Type_Name";
                Grd_OrderTime.Columns[16].Width = 100;

                Grd_OrderTime.Columns[17].Name = "Reporting_To_1";
                Grd_OrderTime.Columns[17].HeaderText = "Reporting_To_1";
                Grd_OrderTime.Columns[17].DataPropertyName = "Reporting_To_1";
                Grd_OrderTime.Columns[17].Width = 100;

                Grd_OrderTime.Columns[18].Name = "Reporting_To_2";
                Grd_OrderTime.Columns[18].HeaderText = "Reporting_To_2";
                Grd_OrderTime.Columns[18].DataPropertyName = "Reporting_To_2";
                Grd_OrderTime.Columns[18].Width = 100;

                Grd_OrderTime.Columns[19].Name = "Order_Production_Date";
                Grd_OrderTime.Columns[19].HeaderText = "Order Production Date";
                Grd_OrderTime.Columns[19].DataPropertyName = "Order_Production_Date";
                Grd_OrderTime.Columns[19].Width = 100;

                Grd_OrderTime.Columns[20].Name = "Branch Name";
                Grd_OrderTime.Columns[20].HeaderText = "Branch Name";
                Grd_OrderTime.Columns[20].DataPropertyName = "Branch_Name";
                Grd_OrderTime.Columns[20].Width = 100;

                Grd_OrderTime.Visible = true;
                Grd_OrderTime.DataSource = dt_Status;

                Grd_OrderTime.Columns[0].Visible = false;

            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;

            }


        }

        public void Load_Order_Subscription_List_Report()
        {

            if (Lbl_Title.Text == "Client wise Subscription Report")
            {
                DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
                string From_Date = Fromdate.ToString("MM/dd/yyyy");
                string To_Date = Todate.ToString("MM/dd/yyyy");
                Hashtable htinsert = new Hashtable();
                if (ddl_Client_Status.Text == "ALL" || ddl_Client_Status.Text=="0")
                {
                    System.Data.DataTable dtinsert = new System.Data.DataTable();
                    htinsert.Add("@Trans", "INSERT_NEW");
                    htinsert.Add("@From_Date", From_Date);
                    htinsert.Add("@To_Date", To_Date);
                    dtinsert = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htinsert);
                }
                else
                {

                    System.Data.DataTable dtinsert = new System.Data.DataTable();
                    htinsert.Add("@Trans", "INSERT_CLIENT_WISE");
                    htinsert.Add("@From_Date", From_Date);
                    htinsert.Add("@To_Date", To_Date);
                    htinsert.Add("@client_Id", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                    dtinsert = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htinsert);
                }



                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


                Hashtable ht_Status = new Hashtable();
                System.Data.DataTable dt_Status = new System.Data.DataTable();

                dt_Status.Rows.Clear();


                ht_Status.Add("@Trans", "SELECT");


                ht_Status.Add("@From_Date", From_Date);
                ht_Status.Add("@To_Date", To_Date);

                //ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
                //ht_Status.Add("@Work_Type_Id", 1);

                dt_Status = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", ht_Status);
                dtuserexport = dt_Status;
               
                if (dt_Status.Rows.Count > 0)
                {
                    lbl_Error.Visible = false;
                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.Rows.Clear();
                    Grd_OrderTime.Columns.Clear();
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.AutoGenerateColumns = true;
                    Grd_OrderTime.Refresh();




                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.DataSource = dt_Status;
                    if (userroleid == "1")
                    {
                        Grd_OrderTime.Columns[2].Visible = false;


                    }
                    else 
                    {

                        Grd_OrderTime.Columns[1].Visible = false;
                    }
                    Grd_OrderTime.Columns[0].Visible = false;
                    //Grd_OrderTime.Columns[1].Visible = false;

                    ArrangeGrid(Grd_OrderTime);
                }
                else
                {
                    Grd_OrderTime.Visible = false;
                    Grd_OrderTime.DataSource = null;

                }

                if (dtuserexport.Rows.Count > 0)
                {
                    if (userroleid == "1")
                    {
                        dtuserexport.Columns.Remove("Client_Number");
                    }
                    else 
                    {
                        dtuserexport.Columns.Remove("Client_Name");

                    }
                }

                //insert temp users





                //    for (int j = 0; j < Grd_OrderTime.Rows.Count; j++)
                //    {
                //        htnew.Clear();
                //        htnew.Add("@Trans", "ALL_CLIENT_WISE_SUBSCRIPTION_ORDER_INFORMATION");
                //        htnew.Add("@webSite_Name", dt_Status.Columns[i].ColumnName.ToString());
                //        htnew.Add("@Client", int.Parse(Grd_OrderTime.Rows[j].Cells[0].Value.ToString()));
                //        dtnew = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htnew);
                //        if (dtnew.Rows.Count > 0)
                //        {
                //            gridclient.DataSource = null;
                //            gridclient.DataSource = dtnew;
                //        }
                //        else
                //        {
                //            gridclient.DataSource = null;
                //        }
                //    }
                //}
            }
            else if (Lbl_Title.Text == "Employee wise Subscription Report")
            {
                DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
                string From_Date = Fromdate.ToString("MM/dd/yyyy");
                string To_Date = Todate.ToString("MM/dd/yyyy");
                Hashtable htinsert = new Hashtable();
                Hashtable ht_Status = new Hashtable();
                System.Data.DataTable dt_Status = new System.Data.DataTable();
                if (ddl_Client_Status.Text == "ALL")
                {
                    System.Data.DataTable dtinsert = new System.Data.DataTable();
                    htinsert.Add("@Trans", "INSERT_EMPLOYEE_NEW");
                    htinsert.Add("@From_Date", From_Date);
                    htinsert.Add("@To_Date", To_Date);

                    dtinsert = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htinsert);
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


                    //Hashtable ht_Status = new Hashtable();
                    //System.Data.DataTable dt_Status = new System.Data.DataTable();

                    dt_Status.Rows.Clear();


                    ht_Status.Add("@Trans", "SELECT_USER");


                    ht_Status.Add("@From_Date", From_Date);
                    ht_Status.Add("@To_Date", To_Date);

                    //ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
                    //ht_Status.Add("@Work_Type_Id", 1);

                    //dt_Status = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", ht_Status);
                    //dtuserexport = dt_Status;
                }
                else
                {

                    System.Data.DataTable dtinsert = new System.Data.DataTable();
                    htinsert.Add("@Trans", "INSERT_USER_WISE");
                    htinsert.Add("@From_Date", From_Date);
                    htinsert.Add("@To_Date", To_Date);
                    htinsert.Add("@User_Id", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                    dtinsert = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htinsert);
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;




                    dt_Status.Rows.Clear();


                    ht_Status.Add("@Trans", "SELECT_USER_WISE_RECORD");


                    ht_Status.Add("@From_Date", From_Date);
                    ht_Status.Add("@To_Date", To_Date);
                    ht_Status.Add("@userid", Convert.ToString(ddl_Client_Status.SelectedValue));
                    //ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
                    //ht_Status.Add("@Work_Type_Id", 1);


                }
                dt_Status = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", ht_Status);
                dtuserexport = dt_Status;



                if (dt_Status.Rows.Count > 0)
                {
                    lbl_Error.Visible = false;
                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.Rows.Clear();
                    Grd_OrderTime.Columns.Clear();
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.AutoGenerateColumns = true;
                    Grd_OrderTime.Refresh();




                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.DataSource = dt_Status;
                    Grd_OrderTime.Columns[0].Visible = false;


                    ArrangeGrid(Grd_OrderTime);
                }
                else
                {
                    Grd_OrderTime.Visible = false;
                    Grd_OrderTime.DataSource = null;

                }

                Hashtable htnew = new Hashtable();

                System.Data.DataTable dtnew = new System.Data.DataTable();

                htnew.Clear(); dtnew.Clear();
                htnew.Add("@Trans", "ALL_USER_WISE_SUBSCRIPTION_ORDER_INFORMATION");
                //htnew.Add("@webSite_Name", websiteName);
                //htnew.Add("@Client", sub_clientid);
                htnew.Add("@To_Date", To_Date);
                htnew.Add("@From_Date", From_Date);
                dtnew = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htnew);
                if (dtnew.Rows.Count > 0)
                {
                    gridclient.DataSource = dtnew;

                }
                else
                {
                    gridclient.DataSource = null;
                }
            }
        }


        public static void ArrangeGrid(DataGridView Grid)
        {
            int twidth = 0;
            if (Grid.Rows.Count > 0)
            {
                twidth = (Grid.Width * Grid.Columns.Count) / 200;
                for (int i = 0; i < Grid.Columns.Count; i++)
                {
                    Grid.Columns[i].Width = twidth;
                }

            }
        }

        // 19-09-2017
        public bool Validation()
        {
            if (txt_Fromdate.Text == "")
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Select Proper fields in the above filters";
                return false;
            }
            else if (txt_Todate.Text == "")
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "Select Proper fields in the above filters";
                return false;
            }


            else if (ddl_Check_List_Task.Visible == true)
            {
                if (ddl_Check_List_Task.Text == "")
                {
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "Select Proper fields in the above filters";
                    return false;
                }
            }
            return true;

        }

        private void LoadCheckListdata()
        {
            if (Validation() != false)
            {
                DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


                string From_Date = Fromdate.ToString("MM/dd/yyyy");
                string To_Date = Todate.ToString("MM/dd/yyyy");
             //   rptDoc = new Reports.CrystalReport.Checklist_Final_Trans_Report();
                rptDoc = new Reports.CrystalReport.Chklist();
                Logon_To_Crystal();

                if (ddl_Check_List_Task.SelectedIndex > 0 && ddl_OrderTYpe_Abr.SelectedIndex > 0)
                {
                    Ordertask_Id = int.Parse(ddl_Check_List_Task.SelectedValue.ToString());
                    Ordertype_Abr_Id = int.Parse(ddl_OrderTYpe_Abr.SelectedValue.ToString());
                }

                string Sub_Client;
                string Client;

                int Client_Id, Sub_Client_Id;
                // int User_Id;
                if (ddl_Client_Status.SelectedIndex > 0)
                {
                    Client_Id = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                    Client = ddl_Client_Status.SelectedValue.ToString();
                }
                else
                {
                    Client_Id = 0;

                    Client = "ALL";

                }
                if (ddl_SubProcess.SelectedIndex > 0)
                {
                    Sub_Client_Id = int.Parse(ddl_Subprocess_Status.SelectedValue.ToString());
                    Sub_Client = ddl_Subprocess_Status.SelectedValue.ToString();
                }
                else
                {
                    Sub_Client_Id = 0;
                    Sub_Client = "ALL";

                }


                if (Client != "ALL" && Sub_Client == "ALL")
                {

                    rptDoc.SetParameterValue("@Trans", "CLEINT_WISE");
                }
                else if (Client != "ALL" && Sub_Client != "ALL")
                {

                    rptDoc.SetParameterValue("@Trans", "CLEINT_SUB_CLIENT_WISE");
                }
                else if (Client == "ALL" && Sub_Client == "ALL")
                {

                    rptDoc.SetParameterValue("@Trans", "SELECT_DATE_RANGE_WISE");
                }
               
                //if (From_Date != "" && From_Date != "")
                //{
                //    rptDoc.SetParameterValue("@Trans", "SELECT_USER_TASK_WISE");
                //}

                int Order_TypeAbsId=int.Parse(ddl_OrderTYpe_Abr.SelectedValue.ToString());

                rptDoc.SetParameterValue("@Order_Task", int.Parse(ddl_Check_List_Task.SelectedValue.ToString()));
                rptDoc.SetParameterValue("@Order_Type_Abs_Id", Order_TypeAbsId);
                rptDoc.SetParameterValue("@From_date", From_Date);
                rptDoc.SetParameterValue("@To_date", To_Date);
                rptDoc.SetParameterValue("@Client", Client);
                rptDoc.SetParameterValue("@Sub_Client", Sub_Client);
                rptDoc.SetParameterValue("@Work_Type_Id", 1);
               // rptDoc.SetParameterValue("@Login_User_Id", Loged_In_User_Id);

                crViewer.ReportSource = rptDoc;

            }
        }

     

        private void btn_Report_Click(object sender, EventArgs e)
        {
            
            form_loader.Start_progres();
            //cProbar.startProgress();
            if (Lbl_Title.Text == "User Production Report")
            {
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "User_Production_Report";
                Load_Grd_Master_Report();
                crViewer.Visible = false;
            }
            else if (Lbl_Title.Text == "User Production Count")
            {
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "User_Production_Count";
                Load_Grd_Master_Report();
                Grd_OrderTime.Visible = false;
                crViewer.Visible = true;

            }
            else if (Lbl_Title.Text == "User Production Summary")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                Load_User_Production_Summary_Report();
                Export_Title_Name = "User_Production_Summary";
                crViewer.Visible = false;

            }

            else if (Lbl_Title.Text == "Client Wise Production Report")
            {
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "Client Wise Production Report";
                Load_Date_Wise_Order_Status_Report();

            }

            else if (Lbl_Title.Text == "Client Wise Production Count")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "Client_Production_Count";
                Load_Client_Production_Count();
            }
            else if (Lbl_Title.Text == "Billing Report")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "Billing_Report";
                Load_Billing_Report();
            }
            else if (Lbl_Title.Text == "Client wise Subscription Report")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "Client_wise_Subscription_Report";
                Load_Order_Subscription_List_Report();
            }
            else if (Lbl_Title.Text == "Employee wise Subscription Report")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "Employee_wise_Subscription_Report";
                Load_Order_Subscription_List_Report();
            }
            else if (Lbl_Title.Text == "Orders Document List Report")
            {
                Grd_OrderTime.AutoGenerateColumns = true;
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "Document_List_Report";
                Load_Order_Document_List_Report();

            }
            else if (Lbl_Title.Text == "Orders Check List Report")
            {
                Grd_OrderTime.AutoGenerateColumns = true;
                Grd_OrderTime.DataSource = null;
               // Load_Check_List_data();

                LoadCheckListdata();

            }
            else if (Lbl_Title.Text == "Orders Error Info Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                Export_Title_Name = "Error_Info_Report";
                Load_Orders_Error_Info_Report();

            }
            else if (Lbl_Title.Text == "Order Source Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                Export_Title_Name = "Order_Source_Report";
                Load_Order_Source_Report();
            }
            else if (Lbl_Title.Text == "Order Received Date Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                Export_Title_Name = "Order_Recived_Date_Report";
                Load_Order_Received_Date_Report();
            }
            else if (Lbl_Title.Text == "User Break Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                Export_Title_Name = "User_Break_Report";
                Load_User_Break_Report();
            }
            else if (Lbl_Title.Text == "Order Task wise Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                Export_Title_Name = "Order_Recived_Date_Report";
                Load_Order_Task_Wise_Report();
            }
            else if (Lbl_Title.Text == "Productivity Report")
            {//not used
                Grd_OrderTime.DataSource = null;
                Export_Title_Name = "User_Productivity";
                Productivity_Calculation();
            }
            else if (Lbl_Title.Text == "Progress Wise Counts")
            {//not used
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Rows.Clear();
                Order_Received_Count();
            }
            else if (Lbl_Title.Text == "Client Status Report")
            {//not used
                Grd_OrderTime.DataSource = null;

                Load_Order_Task_Status();
            }
            else
            {
                lbl_Error.Visible = true;
            }
            //cProbar.stopProgress();
        }

        private void Load_Order_Task_Wise_Report()
        {
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;

            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");

            ht_Status.Clear();
            dt_Status.Clear();
            if (ddl_Check_List_Task.Text == "ALL" && ddl_Client_Status.SelectedIndex > 0)
            {
                ht_Status.Add("@Trans", "BIND_ALL_STATUS_PROGRESS_WISE");
                ht_Status.Add("@From_Date", Fromdate);
                ht_Status.Add("@To_Date", Todate);
                ht_Status.Add("@Order_Progress", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                dt_Status = dataaccess.ExecuteSP("Sp_Order_Task_wise_Report", ht_Status);

                Hashtable htselect = new Hashtable();
                htselect.Add("@Trans", "EXPORT_BIND_ALL_ORDER_STATUS_PROGRESS_WISE");
                htselect.Add("@From_Date", Fromdate);
                htselect.Add("@To_Date", Todate);
                htselect.Add("@Order_Progress", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                dtselect = dataaccess.ExecuteSP("Sp_Order_Task_wise_Report", htselect);
            }
            else if (ddl_Check_List_Task.Text != "ALL" && ddl_Client_Status.SelectedIndex > 0)
            {
                ht_Status.Add("@Trans", "BIND_STATUS_WISE_PROGRESS_WISE");
                ht_Status.Add("@From_Date", Fromdate);
                ht_Status.Add("@To_Date", Todate);
                ht_Status.Add("@Order_Progress", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                ht_Status.Add("@Order_Status", int.Parse(ddl_Check_List_Task.SelectedValue.ToString()));
                dt_Status = dataaccess.ExecuteSP("Sp_Order_Task_wise_Report", ht_Status);

                Hashtable htselect = new Hashtable();
                htselect.Add("@Trans", "EXPORT_BIND_STATUS_WISE_PROGRESS_WISE");
                htselect.Add("@From_Date", Fromdate);
                htselect.Add("@To_Date", Todate);
                htselect.Add("@Order_Progress", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                htselect.Add("@Order_Status", int.Parse(ddl_Check_List_Task.SelectedValue.ToString()));
                dtselect = dataaccess.ExecuteSP("Sp_Order_Task_wise_Report", htselect);
            }
            if (dt_Status.Rows.Count > 0)
            {
                lbl_Error.Visible = false;

                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Rows.Clear();
              

                Grd_OrderTime.DataSource = dt_Status;
                Grd_OrderTime.Columns[0].ValueType = typeof(string);


            }
            else
            {
                lbl_Error.Visible = true;
                Grd_OrderTime.DataSource = null;
            }


            if (dtselect.Rows.Count > 0)
            {
                lbl_Error.Visible = false;

                gridclient.DataSource = null;
                gridclient.Rows.Clear();

                gridclient.DataSource = dtselect;
            }
            else
            {
                lbl_Error.Visible = true;
                gridclient.DataSource = null;
            }
        }

        private void Load_Order_Received_Date_Report()
        {
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;

            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");

            ht_Status.Clear();
            dt_Status.Clear();

            if (ddl_Client_Status.Text == "ALL")
            {

                ht_Status.Add("@Trans", "INSERT_TEMP_DATE_RANGE");
                ht_Status.Add("@From_Date", txt_Fromdate.Value);
                ht_Status.Add("@To_Date", txt_Todate.Value);
                dt_Status = dataaccess.ExecuteSP("Sp_Order_Received_Report", ht_Status);
            }
            else
            {
                ht_Status.Add("@Trans", "INSERT_TEMP_CLIENT_DATE_RANGE");
                ht_Status.Add("@From_Date", txt_Fromdate.Value);
                ht_Status.Add("@To_Date", txt_Todate.Value);
                ht_Status.Add("@Client_Id", ddl_Client_Status.SelectedValue);
                dt_Status = dataaccess.ExecuteSP("Sp_Order_Received_Report", ht_Status);
            }




            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "ALL_SELECT_DATE_RANGE");
            dt = dataaccess.ExecuteSP("Sp_Order_Received_Report", ht);



            if (dt.Rows.Count > 0)
            {
                lbl_Error.Visible = false;
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.DataSource = dt;
                //Grd_OrderTime.AllowUserToAddRows = false;

                Grd_OrderTime.Columns[0].Visible = false;
            }
            else
            {
                Grd_OrderTime.DataSource = null;
            }



            Hashtable htorder = new Hashtable();
            System.Data.DataTable dtorder = new System.Data.DataTable();
            htorder.Add("@Trans", "ALL_CLIENT_SUBPROCESS_DATERANGE");
            dtorder = dataaccess.ExecuteSP("Sp_Order_Received_Report", htorder);

            if (dtorder.Rows.Count > 0)
            {
                gridclient.DataSource = null;
                gridclient.DataSource = dtorder;
            }
            else
            {
                gridclient.DataSource = null;
            }

        }

        private void Load_User_Break_Report()
        {
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;

            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");

            ht_Status.Clear();
            dt_Status.Clear();





            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();




            ht.Add("@Trans", "GET_BREAK_DETAILS_ALL_USERWISE");
            ht.Add("@Firstdate", From_Date);
            ht.Add("@Second_Date", To_Date);
            dt = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", ht);

            if (dt.Rows.Count > 0)
            {
                lbl_Error.Visible = false;
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.DataSource = dt;
                //Grd_OrderTime.AllowUserToAddRows = false;

                // Grd_OrderTime.Columns[0].Visible = false;
            }
            else
            {
                Grd_OrderTime.DataSource = null;
            }
        }

        private void Load_Order_Source_Report()
        {
            
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");

            ht_Status.Clear();
            dt_Status.Clear();
            int Employee_source = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            string Employee_src = ddl_Client_Status.SelectedValue.ToString();


            Hashtable htselect = new Hashtable();
            if (int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 5 || int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 9 || int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 12)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "";
                lbl_Subprocess_Status.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                htselect.Clear(); dtselect.Clear();
                htselect.Add("@Trans", "NO_OF_HITS_DATE_RANGE");
                htselect.Add("@From_Date", From_Date);
                htselect.Add("@To_Date", To_Date);
                htselect.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
                dtselect = dataaccess.ExecuteSP("Sp_Order_Source", htselect);

                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    System.Data.DataTable dt_source = new System.Data.DataTable();
                    ht_Status.Clear(); dt_Status.Clear();
                    ht_Status.Add("@Trans", "BIND_ORDER_SOURCE_NO_HITS_INFO_DETAILS");
                    // ht_Status.Add("@Client", int.Parse(dtselect.Rows[i]["Client_Id"].ToString()));
                    ht_Status.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
                    ht_Status.Add("@From_Date", From_Date);
                    ht_Status.Add("@To_Date", To_Date);
                    dt_Status = dataaccess.ExecuteSP("Sp_Order_Source", ht_Status);
                }
            }
            else if (int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 6 || int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 11 || int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 13)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "";
                lbl_Subprocess_Status.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                htselect.Clear(); dtselect.Clear();
                htselect.Add("@Trans", "NO_OF_DOC_DATE_RANGE");
                htselect.Add("@From_Date", From_Date);
                htselect.Add("@To_Date", To_Date);
                htselect.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
                dtselect = dataaccess.ExecuteSP("Sp_Order_Source", htselect);

                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    System.Data.DataTable dt_source = new System.Data.DataTable();
                    ht_Status.Clear(); dt_Status.Clear();
                    ht_Status.Add("@Trans", "BIND_ORDER_SOURCE_NO_DOC_INFO_DETAILS");
                    // ht_Status.Add("@Client", int.Parse(dtselect.Rows[i]["Client_Id"].ToString()));
                    ht_Status.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
                    ht_Status.Add("@From_Date", From_Date);
                    ht_Status.Add("@To_Date", To_Date);
                    dt_Status = dataaccess.ExecuteSP("Sp_Order_Source", ht_Status);



                }
            }
            else if (int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 10 || int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 7 || int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 14 || int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 15)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "";
                lbl_Subprocess_Status.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                htselect.Clear(); dtselect.Clear();
                htselect.Add("@Trans", "NO_OF_HITS_DOC_DATE_RANGE");
                htselect.Add("@From_Date", From_Date);
                htselect.Add("@To_Date", To_Date);
                htselect.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
                dtselect = dataaccess.ExecuteSP("Sp_Order_Source", htselect);

                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    System.Data.DataTable dt_source = new System.Data.DataTable();
                    ht_Status.Clear(); dt_Status.Clear();
                    ht_Status.Add("@Trans", "BIND_ORDER_SOURCE_NO_HITS_DOC_INFO_DETAILS");
                    // ht_Status.Add("@Client", int.Parse(dtselect.Rows[i]["Client_Id"].ToString()));
                    ht_Status.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
                    ht_Status.Add("@From_Date", From_Date);
                    ht_Status.Add("@To_Date", To_Date);
                    dt_Status = dataaccess.ExecuteSP("Sp_Order_Source", ht_Status);
                }
            }
            else if (int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 2)
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "";
                

                dt_Status.Clear(); ht_Status.Clear();
                Hashtable htinsert = new Hashtable();
                if (ddl_Subprocess_Status.Text == "ALL")
                {
                    System.Data.DataTable dtinsert = new System.Data.DataTable();
                    htinsert.Add("@Trans", "INSERT_NEW");
                    htinsert.Add("@From_Date", From_Date);
                    htinsert.Add("@To_Date", To_Date);
                    dtinsert = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htinsert);
                }
                else
                {

                    System.Data.DataTable dtinsert = new System.Data.DataTable();
                    htinsert.Add("@Trans", "INSERT_CLIENT_WISE");
                    htinsert.Add("@From_Date", From_Date);
                    htinsert.Add("@To_Date", To_Date);
                    htinsert.Add("@client_Id", int.Parse(ddl_Subprocess_Status.SelectedValue.ToString()));
                    dtinsert = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htinsert);
                }


                ht_Status.Add("@Trans", "SELECT");
                ht_Status.Add("@From_Date", From_Date);
                ht_Status.Add("@To_Date", To_Date);

                //ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
                //ht_Status.Add("@Work_Type_Id", 1);

                dt_Status = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", ht_Status);
                dtuserexport = dt_Status;
                
            }
            //else if ()
            //{
            //    lbl_Error.Visible = true;
            //    lbl_Error.Text = "";
            //    htselect.Clear(); dtselect.Clear();
            //    htselect.Add("@Trans", "NO_OF_HITS_DATE_RANGE");
            //    htselect.Add("@From_Date", From_Date);
            //    htselect.Add("@To_Date", To_Date);
            //    htselect.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
            //    dtselect = dataaccess.ExecuteSP("Sp_Order_Source", htselect);

            //    for (int i = 0; i < dtselect.Rows.Count; i++)
            //    {
            //        System.Data.DataTable dt_source = new System.Data.DataTable();
            //        ht_Status.Clear(); dt_Status.Clear();
            //        ht_Status.Add("@Trans", "BIND_ORDER_SOURCE_NO_HITS_INFO_DETAILS");
            //        // ht_Status.Add("@Client", int.Parse(dtselect.Rows[i]["Client_Id"].ToString()));
            //        ht_Status.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
            //        ht_Status.Add("@From_Date", From_Date);
            //        ht_Status.Add("@To_Date", To_Date);
            //        dt_Status = dataaccess.ExecuteSP("Sp_Order_Source", ht_Status);
            //    }
            //}
            else
            {
                lbl_Error.Visible = true;
                lbl_Error.Text = "";
                lbl_Subprocess_Status.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                //@Employee_Source_id
                htselect.Clear(); dtselect.Clear();
                htselect.Add("@Trans", "BIND_ORDER_SOURCE_ONLINE");
                htselect.Add("@From_Date", From_Date);
                htselect.Add("@To_Date", To_Date);
                htselect.Add("@Employee_Source_id", ddl_Client_Status.SelectedValue);
                dtselect = dataaccess.ExecuteSP("Sp_Order_Source", htselect);

            }
            //}
            if (ddl_Subprocess_Status.Visible == true)
            {
                if (dt_Status.Rows.Count > 0)
                {
                    lbl_Error.Visible = false;
                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.Rows.Clear();
                    Grd_OrderTime.Columns.Clear();
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.AutoGenerateColumns = true;
                    Grd_OrderTime.Refresh();




                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.DataSource = dt_Status;
                    Grd_OrderTime.Columns[0].Visible = false;
                    //Grd_OrderTime.Columns[1].Visible = false;

                    ArrangeGrid(Grd_OrderTime);
                }
                else
                {
                    Grd_OrderTime.Visible = false;
                    Grd_OrderTime.DataSource = null;

                }
            }
            else
            {
                if (dtselect.Rows.Count > 0)
                {
                    lbl_Error.Visible = false;
                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.Rows.Clear();
                    Grd_OrderTime.Columns.Clear();
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.AutoGenerateColumns = true;
                    Grd_OrderTime.Refresh();
                    Grd_OrderTime.AllowUserToAddRows = false;



                    Grd_OrderTime.Visible = true;

                    Grd_OrderTime.DataSource = dtselect;
                    Grd_OrderTime.Columns[0].Visible = false;

                    //Grd_OrderTime.DataSource = null;
                    //Grd_OrderTime.AutoGenerateColumns = false;

                    //Grd_OrderTime.ColumnCount = 4;
                    ////Grd_OrderTime.Rows.Add();
                    //Grd_OrderTime.Columns[0].Name = "Clientid";
                    //Grd_OrderTime.Columns[0].HeaderText = "Client Id";
                    //Grd_OrderTime.Columns[0].DataPropertyName = "Client_Id";
                    //Grd_OrderTime.Columns[0].Width = 140;
                    //Grd_OrderTime.Columns[0].Visible = false;


                    //Grd_OrderTime.Columns[1].Name = "Client";
                    //Grd_OrderTime.Columns[1].HeaderText = "Client Name";
                    //Grd_OrderTime.Columns[1].DataPropertyName = "Client_Name";
                    //Grd_OrderTime.Columns[1].Width = 140;

                    //Grd_OrderTime.Columns[2].Name = "No_of_hits";
                    //Grd_OrderTime.Columns[2].HeaderText = "No Of Hits";
                    //Grd_OrderTime.Columns[2].DataPropertyName = "No_of_Hits";
                    //Grd_OrderTime.Columns[2].Width = 140;

                    //Grd_OrderTime.Columns[3].Name = "No_of_documents";
                    //Grd_OrderTime.Columns[3].HeaderText = "No Of Documents";
                    //Grd_OrderTime.Columns[3].DataPropertyName = "No_of_Documents";
                    //Grd_OrderTime.Columns[3].Width = 140;
                    //Grd_OrderTime.DataSource = dtselect;

                    //  ArrangeGrid(Grd_OrderTime);

                }
                else
                {
                    //Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.DataSource = null;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "No Records Found";
                }
                if (dt_Status.Rows.Count > 0)
                {

                    gridclient.DataSource = null;

                    gridclient.DataSource = dt_Status;


                }
                else
                {
                    gridclient.DataSource = null;
                    gridclient.Rows.Clear();
                }
            }
            //}
            //else
            //{

            //}

        }







        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ClientName.SelectedIndex > 0)
            {
                int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                if (userroleid == "1")
                {
                    dbc.BindSubProcessName_rpt(ddl_SubProcess, clientid);
                }
                else 
                {
                    dbc.BindSubProcessNo_rpt(ddl_SubProcess, clientid);

                }
                ddl_SubProcess.Focus();
            }
            else
            {
                //dbc.BindSubProcessName_rpt1(ddl_SubProcess);
            }
            //Load_Grd_Master_Report();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void First_report_Design()
        {
            //user production report as well as user production count
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;


            grp_Report.Visible = true;
            label1.Visible = false;
            ddl_Client_Status.Visible = false;
            crViewer.Visible = false;
            ddl_Subprocess_Status.Visible = false;
            lbl_Subprocess_Status.Visible = false;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;
            label2.Visible = false;
            ddl_Check_List_UserName.Visible = false;


            from_lbl.X = 459; from_lbl.Y = 64;
            pt1.X = 555; pt1.Y = 61;
            to_lbl.X = 774; to_lbl.Y = 64;
            pt.X = 857; pt.Y = 61;
            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            refresh_btn.X = 643; refresh_btn.Y = 237;
            export_btn.X = 751; export_btn.Y = 238;
            clear_btn1.X = 856; clear_btn1.Y = 237;
            clear_btn.X = 239; clear_btn.Y = 271;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            btn_Clear_All.Location = clear_btn1;
            pnl_report.Height = 422;
            crViewer.Height = 422;
            Grd_OrderTime.Height = 422;


        }

        private void Second_Report_Design()
        {
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            grp_Report.Visible = true;
            ddl_Status.Visible = false;
            ddl_Task.Visible = false;
            ddl_OrderNumber.Visible = false;
            ddl_EmployeeName.Visible = false;
            label7.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
            label8.Visible = false;
            label1.Visible = false;
            ddl_Client_Status.Visible = false;
            crViewer.Visible = false;
            ddl_Subprocess_Status.Visible = false;
            lbl_Subprocess_Status.Visible = false;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;


            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 857; pt.Y = 42;
            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            refresh_btn.X = 643; refresh_btn.Y = 237;
            export_btn.X = 751; export_btn.Y = 238;
            clear_btn.X = 239; clear_btn.Y = 271;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            pnl_report.Height = 422;
            crViewer.Height = 422;
            Grd_OrderTime.Height = 422;
        }
        private void Billing_report_Design()
        {
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            grp_Report.Visible = false;
            lbl_from.Visible = true;
            txt_Fromdate.Visible = true;
            txt_Todate.Visible = true;
            lbl_to.Visible = true;
            label1.Visible = true;
            crViewer.Visible = false;
            label1.Visible = true;
            ddl_Client_Status.Visible = true;
            ddl_Subprocess_Status.Visible = true;
            lbl_Subprocess_Status.Visible = true;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;


            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 901; pt.Y = 42;

            client_lbl.X = 459; client_lbl.Y = 82;
            client_ddl.X = 555; client_ddl.Y = 82;
            subpro_lbl.X = 774; subpro_lbl.Y = 84;
            subpor_ddl.X = 901; subpor_ddl.Y = 81;


            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            pnl_report.Height = 522;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;
        }
        private void User_Production_Summary_design()
        {
            //user production summary

            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            grp_Report.Visible = false;
            lbl_from.Visible = true;
            txt_Fromdate.Visible = true;
            txt_Todate.Visible = true;
            lbl_to.Visible = true;
            label1.Visible = true;
            crViewer.Visible = false;
            label1.Visible = true;
            ddl_Client_Status.Visible = true;
            ddl_Subprocess_Status.Visible = true;
            lbl_Subprocess_Status.Visible = true;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;
            label2.Visible = false;
            ddl_Check_List_UserName.Visible = false;

            label1.Visible = true;
            label1.Text = "Client Name :";
            ddl_Client_Status.Visible = true;
          

            if (userroleid == "1")
            {
                dbc.BindClient(ddl_Client_Status);
            }
            else 
            {

                dbc.BindClientNo_for_Report(ddl_Client_Status);
            }
            lbl_Subprocess_Status.Text = "SubProcessName :";

            int clientid = int.Parse(ddl_Client_Status.SelectedValue.ToString());
            if (userroleid == "1")
            {
                dbc.BindSubProcessName_rpt(ddl_Subprocess_Status, clientid);
            }
            else 
            {

                dbc.BindSubProcessNo_rpt(ddl_Subprocess_Status, clientid);
            }
            ddl_Subprocess_Status.Focus();

            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 901; pt.Y = 42;

            client_lbl.X = 459; client_lbl.Y = 82;
            client_ddl.X = 555; client_ddl.Y = 82;
            subpro_lbl.X = 774; subpro_lbl.Y = 84;
            subpor_ddl.X = 901; subpor_ddl.Y = 81;

            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn1.X = 856; clear_btn1.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            pnl_report.Height = 522;
            btn_Clear_All.Location = clear_btn1;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;

        }
        private void Clientwise_Prod_Rpt_Design()
        {
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            grp_Report.Visible = false;
            lbl_from.Visible = true;
            txt_Fromdate.Visible = true;
            txt_Todate.Visible = true;
            lbl_to.Visible = true;
            label1.Visible = true;
            crViewer.Visible = false;
            label1.Visible = true;
            ddl_Client_Status.Visible = true;
            ddl_Subprocess_Status.Visible = true;
            lbl_Subprocess_Status.Visible = true;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;


            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 901; pt.Y = 42;

            client_lbl.X = 459; client_lbl.Y = 82;
            client_ddl.X = 555; client_ddl.Y = 82;
            subpro_lbl.X = 774; subpro_lbl.Y = 84;
            subpor_ddl.X = 901; subpor_ddl.Y = 81;


            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn1.X = 856; clear_btn1.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            btn_Clear_All.Location = clear_btn1;
            pnl_report.Height = 522;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;
        }
        private void Clientwise_Pro_count_Rpt_Design()
        {
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            grp_Report.Visible = false;
            lbl_from.Visible = true;
            txt_Fromdate.Visible = true;
            txt_Todate.Visible = true;
            lbl_to.Visible = true;
            label1.Visible = true;
            crViewer.Visible = false;
            label1.Visible = true;
            ddl_Client_Status.Visible = true;
            ddl_Subprocess_Status.Visible = true;
            lbl_Subprocess_Status.Visible = true;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;

            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 901; pt.Y = 42;

            client_lbl.X = 459; client_lbl.Y = 82;
            client_ddl.X = 555; client_ddl.Y = 82;
            subpro_lbl.X = 774; subpro_lbl.Y = 84;
            subpor_ddl.X = 901; subpor_ddl.Y = 81;


            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn1.X = 856; clear_btn1.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            btn_Clear_All.Location = clear_btn1;
            pnl_report.Height = 522;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;
        }

        private void Order_Chklist_Design()
        {
            lbl_OrderTypr_Abr.Visible = true;
            ddl_OrderTYpe_Abr.Visible = true;


            grp_Report.Visible = false;
            label1.Visible = true;
            ddl_Client_Status.Visible = true;
            crViewer.Visible = false;


            ddl_Subprocess_Status.Visible = true;
            lbl_Subprocess_Status.Visible = true;
            lbl_Chk_Task.Visible = true;
            ddl_Check_List_Task.Visible = true;

            from_lbl.X = 226; from_lbl.Y = 49;
            pt1.X = 310; pt1.Y = 47;

            to_lbl.X = 497; to_lbl.Y = 50;
            pt.X = 565; pt.Y = 48;

            client_lbl.X = 227; client_lbl.Y = 93;
            client_ddl.X = 418; client_ddl.Y = 82;

            subpro_lbl.X = 619; subpro_lbl.Y = 84;
            subpor_ddl.X = 743; subpor_ddl.Y = 81;


            //task_lbl.X = 938; task_lbl.Y = 44;
            //task_ddl.X = 1021; task_ddl.Y = 38;

            task_lbl.X = 775; task_lbl.Y = 49;
            task_ddl.X = 845; task_ddl.Y = 46;

            OrderTypr_Abr_lbl.X = 1041; OrderTypr_Abr_lbl.Y = 47;
            OrderTypr_Abr_ddl.X = 1169; OrderTypr_Abr_ddl.Y = 43;

            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            lbl_Chk_Task.Location = task_lbl;
            ddl_Check_List_Task.Location = task_ddl;

            lbl_OrderTypr_Abr.Location = OrderTypr_Abr_lbl;
            ddl_OrderTYpe_Abr.Location = OrderTypr_Abr_ddl;


            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn1.X = 856; clear_btn1.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Clear_All.Location = clear_btn1;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            pnl_report.Height = 522;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;
        }


        
        private void Order_Document_List_Design()
        {
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            crViewer.Visible = false;
            grp_Report.Visible = false;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;
            label2.Visible = false;
            ddl_Check_List_UserName.Visible = false;

            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 857; pt.Y = 42;

            client_lbl.X = 459; client_lbl.Y = 82;
            client_ddl.X = 555; client_ddl.Y = 82;
            subpro_lbl.X = 774; subpro_lbl.Y = 84;
            subpor_ddl.X = 901; subpor_ddl.Y = 81;


            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn1.X = 856; clear_btn1.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            btn_Clear_All.Location = clear_btn1;
            //Grd_OrderTime
            pnl_report.Height = 522;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;
        }
        private void Order_Erro_Info_Design()
        {
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            crViewer.Visible = false;
            grp_Report.Visible = false;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;
            label2.Visible = false;
            ddl_Check_List_UserName.Visible = false;

            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 857; pt.Y = 42;

            client_lbl.X = 459; client_lbl.Y = 82;
            client_ddl.X = 555; client_ddl.Y = 82;
            subpro_lbl.X = 774; subpro_lbl.Y = 84;
            subpor_ddl.X = 901; subpor_ddl.Y = 81;


            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn1.X = 856; clear_btn1.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            btn_Clear_All.Location = clear_btn1;
            //Grd_OrderTime
            pnl_report.Height = 522;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;
        }

        private void User_Break_Design()
        {
            lbl_OrderTypr_Abr.Visible = false;
            ddl_OrderTYpe_Abr.Visible = false;

            crViewer.Visible = false;
            grp_Report.Visible = false;
            lbl_Chk_Task.Visible = false;
            ddl_Check_List_Task.Visible = false;
            label2.Visible = false;
            ddl_Check_List_UserName.Visible = false;

            from_lbl.X = 459; from_lbl.Y = 43;
            pt1.X = 555; pt1.Y = 42;
            to_lbl.X = 774; to_lbl.Y = 45;
            pt.X = 857; pt.Y = 42;

            client_lbl.X = 459; client_lbl.Y = 82;
            client_ddl.X = 555; client_ddl.Y = 82;
            subpro_lbl.X = 774; subpro_lbl.Y = 84;
            subpor_ddl.X = 901; subpor_ddl.Y = 81;


            lbl_from.Location = from_lbl;
            lbl_to.Location = to_lbl;
            txt_Fromdate.Location = pt1;
            txt_Todate.Location = pt;

            label1.Location = client_lbl;
            ddl_Client_Status.Location = client_ddl;
            lbl_Subprocess_Status.Location = subpro_lbl;
            ddl_Subprocess_Status.Location = subpor_ddl;

            refresh_btn.X = 643; refresh_btn.Y = 133;
            export_btn.X = 751; export_btn.Y = 133;
            clear_btn1.X = 856; clear_btn1.Y = 133;
            clear_btn.X = 239; clear_btn.Y = 165;

            btn_Report.Location = refresh_btn;
            btn_Export.Location = export_btn;
            pnl_report.Location = clear_btn;
            btn_Clear_All.Location = clear_btn1;
            //Grd_OrderTime
            pnl_report.Height = 522;
            crViewer.Height = 522;
            Grd_OrderTime.Height = 522;
        }

        private void tvwRightSide_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Lbl_Title.Text = tvwRightSide.SelectedNode.Text;

            Grd_OrderTime.DataSource = null;

            Grd_OrderTime.Refresh();

            lbl_User_summary.Visible = false;
            //dbc.Bind_UserClient_rpt(ddl_Client_Status, Loged_In_User_Id);
            //dbc.Bind_UserClient_rpt(ddl_ClientName, Loged_In_User_Id);
            if (ddl_Client_Status.Text == "")
            {
                lbl_User_summary.Visible = true;
            }
            if (ddl_ClientName.Text == "")
            {
                lbl_User_summary.Visible = true;
            }
            if (tvwRightSide.SelectedNode.Text == "User Production Report" || tvwRightSide.SelectedNode.Text == "Progress Wise Counts" || tvwRightSide.SelectedNode.Text == "Open Status Reports" || tvwRightSide.SelectedNode.Text == "Production Reports" || tvwRightSide.SelectedNode.Text == "Datewise Pending counts")
            {
                First_report_Design();
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                

             

                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "User Production Count")
            {
                First_report_Design();
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "User Production Summary")
            {

                User_Production_Summary_design();
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "Billing Report")
            {
                User_Production_Summary_design();
                grp_Report.Visible = false;
                lbl_from.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;
                lbl_to.Visible = true;
                label1.Text = "Client Name :";
                crViewer.Visible = false;
                label1.Visible = true;
                ddl_Client_Status.Visible = true;
                ddl_Subprocess_Status.Visible = false;
                lbl_Subprocess_Status.Visible = false;
                lbl_Chk_Task.Visible = false;
                ddl_Check_List_Task.Visible = false;
                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else 
                
                {
                   
                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "Client Wise Production Report")
            {
                Clientwise_Prod_Rpt_Design();
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                ddl_Client_Status.Visible = true;
                label1.Visible = true;
                label1.Text = "Client Name :";
                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                lbl_Subprocess_Status.Text = "SubProcessName :";
                
                int clientid = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                dbc.BindSubProcessName_rpt(ddl_Subprocess_Status, clientid);
                ddl_Subprocess_Status.Focus();
                dbc.BindClient(ddl_Client_Status);
            }
            else if (tvwRightSide.SelectedNode.Text == "Client Wise Production Count")
            {
                Clientwise_Pro_count_Rpt_Design();
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                ddl_Client_Status.Visible = true;
                label1.Visible = true;
                label1.Text = "Client Name :";
                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else 
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                lbl_Subprocess_Status.Text = "SubProcessName :";
                int clientid = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                if (userroleid == "1")
                {
                    dbc.BindSubProcessName_rpt(ddl_Subprocess_Status, clientid);
                }
                else
                {

                    dbc.BindSubProcessNo_rpt(ddl_Subprocess_Status, clientid);
                }

                ddl_Subprocess_Status.Focus();
                
            }

            else if (tvwRightSide.SelectedNode.Text == "Orders Document List Report")
            {
                Order_Document_List_Design();
                grp_Report.Visible = false;
                lbl_from.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;
                lbl_to.Visible = true;
                crViewer.Visible = false;
                label1.Visible = true;
                label1.Text = "Client Name :";
                ddl_Client_Status.Visible = true;
                ddl_Subprocess_Status.Visible = false;
                lbl_Subprocess_Status.Visible = false;
                lbl_Chk_Task.Visible = false;
                ddl_Check_List_Task.Visible = false;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "Orders Check List Report")
            {
                Order_Chklist_Design();
                crViewer.Visible = true;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                Grd_OrderTime.Visible = false;
                ddl_Client_Status.Visible = true;
                label1.Visible = true;
                label1.Text = "Client Name :";

                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else 
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                lbl_Subprocess_Status.Text = "SubProcessName :";
                int clientid = int.Parse(ddl_Client_Status.SelectedValue.ToString());

                if (userroleid == "1")
                {
                    dbc.BindSubProcessName_rpt(ddl_Subprocess_Status, clientid);
                }
                else 
                {

                    dbc.BindSubProcessNo_rpt(ddl_Subprocess_Status, clientid);
                }

                ddl_Subprocess_Status.Focus();
         
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "Orders Error Info Report")
            {
                Order_Erro_Info_Design();
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                ddl_Client_Status.Visible = true;
                label1.Visible = true;
                label1.Text = "Client Name :";

                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else 
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                lbl_Subprocess_Status.Text = "SubProcessName :";
                int clientid = int.Parse(ddl_Client_Status.SelectedValue.ToString());

                if (userroleid == "1")
                {
                    dbc.BindSubProcessName_rpt(ddl_Subprocess_Status, clientid);
                }
                else 
                {

                    dbc.BindSubProcessNo_rpt(ddl_Subprocess_Status, clientid);
                }
                ddl_Subprocess_Status.Focus();
              
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "Client wise Subscription Report")
            {
                User_Production_Summary_design();
                grp_Report.Visible = false;
                lbl_from.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;
                lbl_to.Visible = true;

                label1.Text = "Client Name :";
                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                ddl_Client_Status.Visible = true;

                crViewer.Visible = false;
                label1.Visible = true;
                ddl_Subprocess_Status.Visible = false;
                lbl_Subprocess_Status.Visible = false;
                lbl_Chk_Task.Visible = false;
                ddl_Check_List_Task.Visible = false;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }

            }
            else if (tvwRightSide.SelectedNode.Text == "Employee wise Subscription Report")
            {
                User_Production_Summary_design();
                grp_Report.Visible = false;
                lbl_from.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;
                lbl_to.Visible = true;
                label1.Text = "User Name :";
                crViewer.Visible = false;
                label1.Visible = true;
                ddl_Client_Status.Visible = true;
                ddl_Subprocess_Status.Visible = false;
                lbl_Subprocess_Status.Visible = false;
                lbl_Chk_Task.Visible = false;
                ddl_Check_List_Task.Visible = false;

                dbc.BindUserName(ddl_Client_Status);

                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }

            else if (tvwRightSide.SelectedNode.Text == "Order Source Report")
            {
                User_Production_Summary_design();
                grp_Report.Visible = false;
                lbl_from.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;
                lbl_to.Visible = true;
                crViewer.Visible = false;
                label1.Visible = true;
                ddl_Client_Status.Visible = true;
                ddl_Subprocess_Status.Visible = false;
                lbl_Subprocess_Status.Visible = false;
                lbl_Chk_Task.Visible = false;
                ddl_Check_List_Task.Visible = false;

                dbc.Bind_Employee_Order_source(ddl_Client_Status);

                label1.Text = "Order Source:";
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "Order Received Date Report")
            {
                Order_Erro_Info_Design();
                ddl_Client_Status.Visible = true;
                label1.Visible = true;
                label1.Text = "Client Name :";

                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else 
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                lbl_Subprocess_Status.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "Order Task wise Report")
            {
                Order_Chklist_Design();
                lbl_OrderTypr_Abr.Visible = false;
                ddl_OrderTYpe_Abr.Visible = false;

                lbl_Chk_Task.Visible = true;
                ddl_Check_List_Task.Visible = true;
                label1.Visible = true;
                label1.Text = "Order Progress :";
                dbc.Bind_Order_Progress(ddl_Client_Status);
                dbc.Bind_Order_Status_all(ddl_Check_List_Task);
                ddl_Client_Status.Visible = true;
                lbl_Subprocess_Status.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }
            else if (tvwRightSide.SelectedNode.Text == "User Break Report")
            {

                User_Break_Design();
                ddl_Client_Status.Visible = false;
                label1.Visible = false;
                label1.Text = "Client Name :";
               
                lbl_Subprocess_Status.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }

            }

            else if (tvwRightSide.SelectedNode.Text == "Progress Wise Counts" || tvwRightSide.SelectedNode.Text == "Open Status Reports" || tvwRightSide.SelectedNode.Text == "Production Reports" || tvwRightSide.SelectedNode.Text == "Datewise Pending counts")
            {//not used
                Second_Report_Design();
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
                if (gridclient.Rows.Count > 0)
                {
                    gridclient.DataSource = null;
                }
            }

            else if (tvwRightSide.SelectedNode.Text == "Client Status Report")
            {//not used
                grp_Report.Visible = true;
                lbl_from.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;
                lbl_to.Visible = true;
                label1.Visible = true;
                label1.Text = "Client Name :";
                if (userroleid == "1")
                {
                    dbc.BindClient(ddl_Client_Status);
                }
                else 
                {

                    dbc.BindClientNo_for_Report(ddl_Client_Status);
                }
                ddl_Client_Status.Visible = true;
                crViewer.Visible = false;
                ddl_Subprocess_Status.Visible = false;
                lbl_Subprocess_Status.Visible = false;
                lbl_Chk_Task.Visible = false;
                ddl_Check_List_Task.Visible = false;
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Columns.Clear();
            }



            else
            {
                ddl_Status.Visible = true;
                ddl_Task.Visible = true;
                ddl_OrderNumber.Visible = true;
                ddl_EmployeeName.Visible = true;
                label7.Visible = true;
                label4.Visible = true;
                label6.Visible = true;
                label8.Visible = true;
                lbl_Chk_Task.Visible = false;
                ddl_Check_List_Task.Visible = false;
            }
        }

        private void txt_Todate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_Fromdate_ValueChanged(object sender, EventArgs e)
        {

        }


        protected void Order_Received_Count()
        {
            int count_Date = 0;
            DateTime From_date = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime To_date = Convert.ToDateTime(txt_Todate.Text.ToString());
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            DateTime fromdate = Convert.ToDateTime(txt_Fromdate.Text, usDtfi);
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text, usDtfi);
            Hashtable ht_Received = new Hashtable();
            System.Data.DataTable dt_Received = new System.Data.DataTable();
            if (ddl_ClientName.SelectedValue == "ALL" && ddl_SubProcess.SelectedValue == "ALL")
            {
                ht_Received.Add("@Trans", "Client_Details");
                //  ht_Received.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
                // ht_Received.Add("@Sup_Process_Id", ddl_SubProcess.SelectedValue.ToString());
            }
            else if (ddl_ClientName.SelectedValue == "ALL" && ddl_SubProcess.SelectedIndex != -1)
            {
                ht_Received.Add("@Trans", "Sub_Process");
                //  ht_Received.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
                ht_Received.Add("@Sup_Process_Id", ddl_SubProcess.SelectedValue.ToString());
            }
            else if (ddl_ClientName.SelectedValue != "ALL" && ddl_SubProcess.SelectedValue == "ALL")
            {
                ht_Received.Add("@Trans", "Client");
                ht_Received.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
                //  ht_Received.Add("@Sup_Process_Id", ddl_SubProcess.SelectedValue.ToString());
            }
            else if (ddl_ClientName.SelectedValue != "ALL" && ddl_SubProcess.SelectedIndex != -1)
            {
                ht_Received.Add("@Trans", "Client_Subprocess");
                ht_Received.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
                ht_Received.Add("@Sup_Process_Id", ddl_SubProcess.SelectedValue.ToString());
            }

            ht_Received.Add("@From_Date", fromdate);
            ht_Received.Add("@To_date", Todate);
            dt_Received = dataaccess.ExecuteSP("Sp_Progress_Wise_Count", ht_Received);
            if (dt_Received.Rows.Count > 0)
            {
                Grd_OrderTime.Visible = true;
                Grd_OrderTime.DataSource = dt_Received;
                //    Grd_OrderTime.DataBind();


            }
            else
            {
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }
            //  Grid_Total();
            // Grid_Completed_Date();
            //  Grid_Total_Received();
            //  Client_Wise_Count();
            // Grid_Grand_Tot();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //cProbar.startProgress();


            if (Lbl_Title.Text == "Client Status Report")
            {

                //Hashtable htexp = new Hashtable();
                //System.Data.DataTable dtexp = new System.Data.DataTable();
                //DateTime From_date = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                //DateTime To_date = Convert.ToDateTime(txt_Todate.Text.ToString());
                //DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                //DateTime fromdate = Convert.ToDateTime(txt_Fromdate.Text, usDtfi);
                //DateTime Todate = Convert.ToDateTime(txt_Todate.Text, usDtfi);
                //if (ddl_Client_Status.Text != "ALL")
                //{
                //    htexp.Add("@Trans", "CLIENT_WISE");
                //}
                //else
                //{
                //    htexp.Add("@Trans", "ALL_CLIENT_WISE");
                //}
                //htexp.Add("@Fromdate", fromdate);
                //htexp.Add("@Todate", Todate);
                //htexp.Add("@Clint", Client);
                //dtexp = dataaccess.ExecuteSP("Sp_Order_Status_Report", htexp);
                //ds.Tables.Add(dtexp);

                //Convert_Dataset_to_Excel();
                //ds.Clear();

                Export_ReportData();
            }
            else if (Lbl_Title.Text == "User Production Report")
            {

                if (dtuserexport.Rows.Count > 0)
                {
                    //ds.Tables.Add(dtuserexport);

                    //Convert_Dataset_to_Excel();
                    //ds.Clear();

                    Export_ReportData();
                }
            }
            else if (Lbl_Title.Text == "User Production Summary")
            {

                if (dtuserexport.Rows.Count > 0)
                {
                    //ds.Tables.Add(dtuserexport);

                    //Convert_Dataset_to_Excel();
                    //ds.Clear();
                    //ds.Tables.Clear();
                    //dtuserexport.Clear();

                    Export_ReportData();
                }
            }
            else if (Lbl_Title.Text == "Client Wise Production Report")
            {

                Export_ReportData();
                //if (ddl_Client_Status.SelectedIndex != 0)
                //{
                //    Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                //}

                //if (ddl_Subprocess_Status.SelectedIndex > 0)
                //{
                //    SubProcess = int.Parse(ddl_Subprocess_Status.SelectedValue.ToString());

                //}
                //else
                //{

                //    SubProcess = 0;
                //}
                //DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                //DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


                //DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                //DateTime seldate = Convert.ToDateTime(txt_Todate.Text, usDtfi);

                //Hashtable ht_Status1 = new Hashtable();
                //System.Data.DataTable dt_Status1 = new System.Data.DataTable();


                //if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text == "ALL")
                //{
                //    ht_Status1.Add("@Trans", "Order_Status_Report__ClientWise");
                //}
                //else if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text != "ALL")
                //{
                //    ht_Status1.Add("@Trans", "Order_Status_Report__Client_SubprocessWise");

                //}
                //else
                //{
                //    ht_Status1.Add("@Trans", "Order_Status_Report_All_My_ClientWise");
                //    ht_Status1.Add("@Log_In_Userid", Loged_In_User_Id);
                //}

                //ht_Status1.Add("@Fromdate", Fromdate);
                //ht_Status1.Add("@Todate", Todate);
                //ht_Status1.Add("@Clint", Client);
                //ht_Status1.Add("@Subprocess_Id", SubProcess);
                //dt_Status1 = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status1);





                //if (ddl_Client_Status.SelectedIndex != 0)
                //{
                //    Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                //}





                //Hashtable ht_Status = new Hashtable();
                //System.Data.DataTable dt_Status = new System.Data.DataTable();



                //if (ddl_Client_Status.Text != "ALL")
                //{
                //    ht_Status.Add("@Trans", "CLIENT_WISE_PRODUCTION_COUNT");
                //}
                //else if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text != "ALL")
                //{
                //    ht_Status.Add("@Trans", "CLIENT_SUBPROCESS_WISE_PRODUCTION_COUNT");
                //}

                //else
                //{
                //    ht_Status.Add("@Trans", "MY_ALL_CLIENT_WISE_PRODUCTION_COUNT");
                //    ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
                //}

                //ht_Status.Add("@Fromdate", Fromdate);
                //ht_Status.Add("@Todate", Todate);
                //ht_Status.Add("@Clint", Client);
                //ht_Status.Add("@Subprocess_Id", SubProcess);
                //dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);
                //ds.Tables.Add(dt_Status);

                //// ds.Tables.Add(dt_Status1);
                //ds.Merge(dt_Status1);
                //Convert_Dataset_to_Excel();
                //ds.Clear();
                //ds.Tables.Clear();
            }
            else if (Lbl_Title.Text == "Client Wise Production Count")
            {

                //if (ddl_Client_Status.SelectedIndex != 0)
                //{
                //    Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                //}

                //DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                //DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());

                //DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                //DateTime seldate = Convert.ToDateTime(txt_Todate.Text, usDtfi);

                //Hashtable ht_Status = new Hashtable();
                //System.Data.DataTable dt_Status = new System.Data.DataTable();



                //if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text == "ALL")
                //{
                //    ht_Status.Add("@Trans", "CLIENT_WISE_PRODUCTION_COUNT");
                //}
                //else if (ddl_Client_Status.Text != "ALL" && ddl_Subprocess_Status.Text != "ALL")
                //{
                //    ht_Status.Add("@Trans", "CLIENT_SUBPROCESS_WISE_PRODUCTION_COUNT");
                //}

                //else
                //{
                //    ht_Status.Add("@Trans", "ALL_CLIENT_WISE_PRODUCTION_COUNT");
                //}

                //ht_Status.Add("@Fromdate", Fromdate);
                //ht_Status.Add("@Todate", Todate);
                //ht_Status.Add("@Clint", Client);
                //ht_Status.Add("@Subprocess_Id", SubProcess);
                //dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);

                //ds.Tables.Add(dt_Status);

                //Convert_Dataset_to_Excel();
                //ds.Clear();
                //ds.Tables.Clear();

                Export_ReportData();

            }
            else if (Lbl_Title.Text == "Billing Report")
            {

                //if (ddl_Client_Status.SelectedIndex != 0)
                //{
                //    Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                //}

                //DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                //DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


                //DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


                //Hashtable ht_Status = new Hashtable();
                //System.Data.DataTable dt_Status = new System.Data.DataTable();

                //dt_Status.Rows.Clear();

                //if (ddl_Client_Status.Text != "ALL")
                //{
                //    ht_Status.Add("@Trans", "Client");
                //}
                //else
                //{
                //    ht_Status.Add("@Trans", "ALL");
                //}
                //ht_Status.Add("@From_Date", Fromdate);
                //ht_Status.Add("@To_Date", Todate);
                //ht_Status.Add("@Client_Id", Client);

                //dt_Status = dataaccess.ExecuteSP("Sp_Billing_Report", ht_Status);

                //ds.Tables.Add(dt_Status);

                //Convert_Dataset_to_Excel();
                //ds.Clear();
                //ds.Tables.Clear();

                Export_ReportData();

            }
            else if (Lbl_Title.Text == "Client wise Subscription Report")
            {
                // Export_ReportData();
                Export_Report_Subscription_Data();

            }
            else if (Lbl_Title.Text == "User Break Report")
            {
            
                Export_ReportData();

            }
            else if (Lbl_Title.Text == "Employee wise Subscription Report")
            {
                Export_Report_Employee_Subscription_Data();
            }
            else if (Lbl_Title.Text == "Orders Document List Report")
            {





                if (dtuserexport.Rows.Count > 0)
                {
                    //ds.Tables.Add(dtuserexport);

                    //Convert_Dataset_to_Excel();
                    //ds.Clear();
                    //ds.Tables.Clear();
                    //dtuserexport.Clear();

                    Export_ReportData();
                }


            }
            else if (Lbl_Title.Text == "Orders Error Info Report")
            {


                if (dtordererror.Rows.Count > 0)
                {
                    //ds.Clear();
                    //ds.Tables.Clear();

                    ////ds.Tables.Remove();
                    //ds.Tables.Add(dtordererror);

                    //Convert_Dataset_to_Excel();
                    //ds.Tables.Clear();

                    Export_ReportData();

                }
            }
            else if (Lbl_Title.Text == "Order Source Report")
            {
                //Export_ReportData();
                if (ddl_Client_Status.Text == "Subscription")
                {
                    Export_Report_Subscription_Data();
                }
                else
                {
                    Export_ReportOrder_Source_Data();
                }
            }
            else if (Lbl_Title.Text == "Order Received Date Report")
            {
                Export_ReportOrder_Source_Data();
            }
            else if (Lbl_Title.Text == "Order Task wise Report")
            {
                Export_Report_Order_Task_wise_Report();
            }
            else
            {
                // ds.Tables[0] = Grd_OrderTime.DataSource;
                ds.Tables.Add(dt_Status);
                Convert_Dataset_to_Excel();
                ds.Tables.Clear();
            }

            //cProbar.stopProgress();

        }

        private void Export_Report_Order_Task_wise_Report()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
            {
                if (column.HeaderText != "" )
                {
                    if (column.ValueType == null)
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in Grd_OrderTime.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();
                    
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {

                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                    
                }
            }



            foreach (DataGridViewColumn column in gridclient.Columns)
            {
                if (column.HeaderText != "" && column.HeaderText != "Order_ID")
                {
                    dt1.Columns.Add(column.HeaderText, column.ValueType);
                }
            }

            //Adding the Rows
            foreach (DataGridViewRow row in gridclient.Rows)
            {
                dt1.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt1.Rows[dt1.Rows.Count - 1][cell.ColumnIndex-1] = cell.Value.ToString();
                        }
                    }
                }
            }

            //Export_Title_Name = "Client_Production";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Count");
                wb.Worksheets.Add(dt1, "Report");

                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
            System.Diagnostics.Process.Start(Path1);
        }

        private void Export_Report_Employee_Subscription_Data()
        {



            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
            {
                if (column.HeaderText != "" && column.HeaderText != "User_Id")
                {
                    if (column.ValueType == null)
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in Grd_OrderTime.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();
                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {

                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex - 1] = cell.Value.ToString();
                        }
                    }
                }
            }



            foreach (DataGridViewColumn column in gridclient.Columns)
            {
                if (column.HeaderText != "")
                {
                    dt1.Columns.Add(column.HeaderText, column.ValueType);
                }
            }

            //Adding the Rows
            foreach (DataGridViewRow row in gridclient.Rows)
            {
                dt1.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() != "")
                    {
                        dt1.Rows[dt1.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }

            //Export_Title_Name = "Client_Production";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Count");
                wb.Worksheets.Add(dt1, "Report");

                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
            System.Diagnostics.Process.Start(Path1);
        }

        private void Export_Report_Subscription_Data()
        {



            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
            {
                if (column.HeaderText != "" && column.HeaderText!="client_Id")
                {
                    if (column.ValueType == null)
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in Grd_OrderTime.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();
                    if (cell.ColumnIndex != 0)
                    {
                        //if (cell.ColumnIndex == 0)
                        //{
                        //    dt.Rows[dt.Rows.Count - 1][1] = int.Parse(cell.Value.ToString());
                        //}

                        if (cell.Value != null && cell.Value.ToString() != "")
                        {

                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex-1] = cell.Value.ToString();
                        }
                    }
                }
            }

            DateTime Fdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Tdate = Convert.ToDateTime(txt_Todate.Text.ToString());
            string From_Date = Fdate.ToString("MM/dd/yyyy");
            string To_Date = Tdate.ToString("MM/dd/yyyy");
            Hashtable httemp = new Hashtable();
            System.Data.DataTable dttemp = new System.Data.DataTable();
            httemp.Add("@Trans", "INSERT_TEMP_USER");
            httemp.Add("@From_Date", From_Date);
            httemp.Add("@To_Date", To_Date);

            dttemp = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", httemp);



            Hashtable htnew = new Hashtable();

            System.Data.DataTable dtnew = new System.Data.DataTable();

            htnew.Clear();
            if (Lbl_Title.Text != "Order Source Report")
            {
                if (ddl_Client_Status.Text == "ALL")
                {
                    htnew.Add("@Trans", "GET_ALL_CLIENT_ORDERS");
                }
                else if (ddl_Client_Status.SelectedIndex > 0)
                {
                    htnew.Add("@Trans", "GET_CLIENT_WISE_ORDERS");
                    htnew.Add("@client_Id", int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                }
            }
            else if (Lbl_Title.Text == "Order Source Report")
            {
                if (ddl_Subprocess_Status.Text == "ALL")
                {
                    htnew.Add("@Trans", "GET_ALL_CLIENT_ORDERS");
                }
                else if (ddl_Subprocess_Status.SelectedIndex > 0)
                {
                    htnew.Add("@Trans", "GET_CLIENT_WISE_ORDERS");
                    htnew.Add("@client_Id", int.Parse(ddl_Subprocess_Status.SelectedValue.ToString()));
                }
            }
            //htnew.Add("@webSite_Name", websiteName);
            //htnew.Add("@Client", sub_clientid);

            htnew.Add("@To_Date", To_Date);
            htnew.Add("@From_Date", From_Date);
            dtnew = dataaccess.ExecuteSP("Sp_Orders_Subscription_Report", htnew);
            if (dtnew.Rows.Count > 0)
            {
                gridclient.DataSource = dtnew;

            }
            else
            {
                gridclient.DataSource = null;
            }


            foreach (DataGridViewColumn column in gridclient.Columns)
            {
                if (column.HeaderText != "" && column.HeaderText != "Order_ID") 
                {
                    dt1.Columns.Add(column.HeaderText, column.ValueType);
                }
            }

            //Adding the Rows
            foreach (DataGridViewRow row in gridclient.Rows)
            {
                dt1.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt1.Rows[dt1.Rows.Count - 1][cell.ColumnIndex - 1] = cell.Value.ToString();
                        }
                    }
                }
            }

            //Export_Title_Name = "Client_Production";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Count");
                wb.Worksheets.Add(dt1, "Report");

                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);

        }

        private void Export_ReportData()
        {


      
            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
            {
                if (column.HeaderText != "")
                {
                    if (column.ValueType == null)
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in Grd_OrderTime.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();


                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }

                }
            }


            Export_Title_Name = "Report";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Report");


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

          //  System.Diagnostics.Process.Start(Path1);




            System.Diagnostics.Process.Start(Path1);
        }

        private void Export_ReportOrder_Source_Data()
        {

            if (ddl_Client_Status.Text == "Online")
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
                //Adding the Columns
                foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
                {
                    if (column.HeaderText != "" && column.HeaderText !="Order_ID")
                    {
                        if (column.ValueType == null)
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));

                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));

                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));

                            }
                            else if (column.ValueType == typeof(DateTime))
                            {

                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }
                    }

                }

                //Adding the Rows
                foreach (DataGridViewRow row in Grd_OrderTime.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //string Value1 = cell.Value.ToString();
                        //string m = Value1.Trim().ToString();
                        if (cell.ColumnIndex != 0)
                        {
                            if (cell.ColumnIndex == 0)
                            {
                                dt.Rows[dt.Rows.Count - 1][1] = cell.Value.ToString();
                            }
                            else if (cell.Value != null && cell.Value.ToString() != "")
                            {
                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex-1] = cell.Value.ToString();
                            }
                        }
                    }
                }



                
                //Exporting to Excel
                string folderPath = "C:\\Temp\\";
                Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);


                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Count");

                    try
                    {

                        wb.SaveAs(Path1);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                       // MessageBox.Show("File is Opened, Please Close and Export it");
                    }



                }

                System.Diagnostics.Process.Start(Path1);
            }
           
            else
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.DataTable dt1 = new System.Data.DataTable();
                DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
                //Adding the Columns
                foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
                {
                    if (column.HeaderText != "" && column.HeaderText != "Client_Id")
                    {
                        if (column.ValueType == null)
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));

                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));

                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));

                            }
                            else if (column.ValueType == typeof(DateTime))
                            {

                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }
                    }

                }

                //Adding the Rows
                foreach (DataGridViewRow row in Grd_OrderTime.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //string Value1 = cell.Value.ToString();
                        //string m = Value1.Trim().ToString();
                        if (cell.ColumnIndex != 0)
                        {
                            if (cell.Value != null && cell.Value.ToString() != "")
                            {

                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex - 1] = cell.Value.ToString();
                            }
                        }
                    }
                }



                foreach (DataGridViewColumn column in gridclient.Columns)
                {
                    if (column.HeaderText != "")
                    {
                        dt1.Columns.Add(column.HeaderText, column.ValueType);
                    }
                }

                //Adding the Rows
                foreach (DataGridViewRow row in gridclient.Rows)
                {
                    dt1.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt1.Rows[dt1.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                    }
                }


                //Exporting to Excel
                string folderPath = "C:\\Temp\\";
                Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);


                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Count");
                    wb.Worksheets.Add(dt1, "Report");

                    try
                    {

                        wb.SaveAs(Path1);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("File is Opened, Please Close and Export it");
                    }



                }

                System.Diagnostics.Process.Start(Path1);
            }

            
        }

        private void Export_ReportClient_ProductionData()
        {



            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
            {
                if (column.HeaderText != "")
                {
                    if (column.ValueType == null )
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in Grd_OrderTime.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();
                    if (cell.ColumnIndex != 0)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {

                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                    }
                }
            }



            foreach (DataGridViewColumn column in gridclient.Columns)
            {
                if (column.HeaderText != "")
                {
                    dt1.Columns.Add(column.HeaderText, column.ValueType);
                }
            }

            //Adding the Rows
            foreach (DataGridViewRow row in gridclient.Rows)
            {
                dt1.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() != "")
                    {
                        dt1.Rows[dt1.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }

            Export_Title_Name = "Client_Production";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Count");
                wb.Worksheets.Add(dt1, "Report");

                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
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


        private void Convert_Dataset_to_Excel_Multiple()
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
                //xlSheets = ExcelApp.Worksheets;
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


        private void ddl_SubProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 189; pt1.Y = 0;
            report_pnl.X = 10; report_pnl.Y = 280;
            report_pnl1.X = 206; report_pnl1.Y = 280;
            from_lbl.X = 195; from_lbl.Y = 60;
            from_lbl1.X = 385; from_lbl1.Y = 60;
            to_lbl.X = 565; to_lbl.Y = 60;
            to_lbl1.X = 765; to_lbl1.Y = 60;
            from_date.X = 315; from_date.Y = 58;
            from_date1.X = 505; from_date1.Y = 58;
            to_date.X = 685; to_date.Y = 58;
            to_date1.X = 875; to_date1.Y = 58;
            report_lbl.X = 440; report_lbl.Y = 10;
            report_lbl1.X = 630; report_lbl1.Y = 10;
            report_grp.X = 10; report_grp.Y = 105;
            report_grp1.X = 205; report_grp1.Y = 105;
            refresh_btn.X = 395; refresh_btn.Y = 235;
            refresh_btn1.X = 585; refresh_btn1.Y = 235;
            export_btn.X = 510; export_btn.Y = 235;
            export_btn1.X = 745; export_btn1.Y = 235;
            clear_btn.X = 430; clear_btn.Y = 565;
            clear_btn1.X = 620; clear_btn1.Y = 565;
            form_pt.X = 150; form_pt.Y = 10;
            form1_pt.X = 60; form1_pt.Y = 10;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                Lbl_Title.Location = report_lbl;
                lbl_from.Location = from_lbl;
                lbl_to.Location = to_lbl;
                txt_Fromdate.Location = from_date;
                txt_Todate.Location = to_date;
                grp_Report.Location = report_grp;
                btn_Report.Location = refresh_btn;
                btn_Export.Location = export_btn;
                pnl_report.Location = report_pnl;
                Create_Branch.ActiveForm.Width = 1040;
                Create_Branch.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                Lbl_Title.Location = report_lbl1;
                lbl_from.Location = from_lbl1;
                lbl_to.Location = to_lbl1;
                txt_Fromdate.Location = from_date1;
                txt_Todate.Location = to_date1;
                grp_Report.Location = report_grp1;
                btn_Report.Location = refresh_btn1;
                btn_Export.Location = export_btn1;
                pnl_report.Location = report_pnl1;
                Create_Branch.ActiveForm.Width = 1240;
                Create_Branch.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

        private void ddl_Task_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ddl_OrderNumber_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Lbl_Title.Text == "Client Wise Production Report")
            {
                if (ddl_Client_Status.SelectedIndex != 0)
                {
                    Client = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                }


                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


                Hashtable ht_Status = new Hashtable();
                System.Data.DataTable dt_Status = new System.Data.DataTable();



                if (ddl_Client_Status.Text != "ALL")
                {
                    ht_Status.Add("@Trans", "GET_ORDER_STATUS_REP_COUNT_CLIENT_WISE");
                }
                else
                {
                    ht_Status.Add("@Trans", "GET_ORDER_STATUS_REP_COUNT_ALL_CLIENT_WISE");
                }


                ht_Status.Add("@Clint", Client);
                dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);
                ds.Tables.Add(dt_Status);

                Convert_Dataset_to_Excel();
                ds.Clear();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "export.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //ToCsV(dataGridView1, @"c:\export.xls");
                ToCsV(Grd_OrderTime, sfd.FileName); // Here dataGridview1 is your grid view name 
            }
        }
        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void ddl_ClientName_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void crViewer_Load(object sender, EventArgs e)
        {

        }

        private void Grd_OrderTime_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (tvwRightSide.SelectedNode.Text == "Billing Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex == 26)
                    {

                        Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString()), User_id, userroleid,Production_Date);
                        OrderEntry.Show();
                    }
                    //cProbar.stopProgress();
                }
                else if (tvwRightSide.SelectedNode.Text == "Client wise Subscription Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex >= 1)
                    {
                        int Client_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string Client_Name = Grd_OrderTime.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string website_Name = Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString();

                        Report_Subscription_View rv = new Report_Subscription_View(Client_Id, Client_Name, "CLIENT WISE", website_Name);
                        rv.Show();

                    }
                    //cProbar.stopProgress();
                }
                else if (tvwRightSide.SelectedNode.Text == "Employee wise Subscription Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex >= 1)
                    {
                        int User_id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string User_Name = Grd_OrderTime.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string website_Name = Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString();

                        Report_Subscription_View rv = new Report_Subscription_View(User_id, User_Name, "USER WISE", website_Name);
                        rv.Show();

                    }
                }

                else if (tvwRightSide.SelectedNode.Text == "Orders Document List Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex == 1)
                    {

                        Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString()), User_id, userroleid,Production_Date);
                        OrderEntry.Show();
                    }
                    //cProbar.stopProgress();
                }

                else if (tvwRightSide.SelectedNode.Text == "User Production Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex == 12)
                    {

                        Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[11].Value.ToString()), User_id, userroleid,Production_Date);
                        OrderEntry.Show();
                    }
                    //cProbar.stopProgress();
                }
                else if (tvwRightSide.SelectedNode.Text == "User Production Summary")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex == 45)
                    {

                        Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString()), User_id, userroleid,Production_Date);
                        OrderEntry.Show();
                    }
                    //cProbar.stopProgress();
                }
                else if (tvwRightSide.SelectedNode.Text == "Client Wise Production Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex == 23)
                    {

                        Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString()), User_id, userroleid,Production_Date);
                        OrderEntry.Show();
                    }
                    //cProbar.stopProgress();
                }

                else if (tvwRightSide.SelectedNode.Text == "Orders Check List Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex == 1)
                    {

                        Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString()), User_id, userroleid,Production_Date);
                        OrderEntry.Show();
                    }
                    //cProbar.stopProgress();
                }

                else if (tvwRightSide.SelectedNode.Text == "Orders Error Info Report")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    if (e.ColumnIndex == 1)
                    {

                        Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString()), User_id, userroleid,Production_Date);
                        OrderEntry.Show();
                    }
                    //cProbar.stopProgress();
                }
                else if (tvwRightSide.SelectedNode.Text == "Order Source Report")
                {
                    form_loader.Start_progres();
                    if (e.ColumnIndex == 1)
                    {
                        int clientid = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string headertext = Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString();
                        int ordersourceid = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                        string clientname = Grd_OrderTime.Rows[e.RowIndex].Cells[1].Value.ToString();
                        if (headertext == "Client Order Number")
                        {
                            //Clientid
                            Ordermanagement_01.Order_Entry view = new Ordermanagement_01.Order_Entry(clientid, User_id, userroleid,Production_Date);
                            view.Show();
                        }

                        else if (ordersourceid != 1 && ordersourceid != 2 && ordersourceid != 3 && ordersourceid != 4)
                        {
                            Ordermanagement_01.Reports.Report_Order_Source_view rpt_ordersource = new Ordermanagement_01.Reports.Report_Order_Source_view(User_id, userroleid, clientid, headertext, ordersourceid, ddl_Client_Status.Text, txt_Fromdate.Text, txt_Todate.Text, clientname, userroleid,Production_Date);
                            rpt_ordersource.Show();
                        }
                        //Ordermanagement_01.Reports.Report_Order_Source_view rpt_ordersource = new Ordermanagement_01.Reports.Report_Order_Source_view(UserID,userroleid,int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString()),Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString(),int.Parse(ddl_Client_Status.SelectedValue.ToString()));
                    }
                    else if (e.ColumnIndex >= 1)
                    {
                        int Client_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string Client_Name = Grd_OrderTime.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string website_Name = Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString();

                        int clientid = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string headertext = Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString();
                        int ordersourceid = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                        string clientname = Grd_OrderTime.Rows[e.RowIndex].Cells[1].Value.ToString();

                        if (ordersourceid != 1 && ordersourceid != 2 && ordersourceid != 3 && ordersourceid != 4)
                        {
                            Ordermanagement_01.Reports.Report_Order_Source_view rpt_ordersource = new Ordermanagement_01.Reports.Report_Order_Source_view(User_id, userroleid, clientid, headertext, ordersourceid, ddl_Client_Status.Text, txt_Fromdate.Text, txt_Todate.Text, clientname, userroleid,Production_Date);
                            rpt_ordersource.Show();
                        }
                        else
                        {

                            Report_Subscription_View rv = new Report_Subscription_View(Client_Id, Client_Name, "CLIENT WISE", website_Name);
                            rv.Show();
                        }

                    }
                }
                else if (tvwRightSide.SelectedNode.Text == "Order Received Date Report")
                {
                    form_loader.Start_progres();
                    if (e.ColumnIndex != 0 && e.ColumnIndex != 1)
                    {
                        int clientid = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string headertext = Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString();
                        string date = Grd_OrderTime.Columns[e.ColumnIndex].HeaderText.ToString();
                        Ordermanagement_01.Reports.Report_Order_Source_view rpt_ordersource = new Ordermanagement_01.Reports.Report_Order_Source_view(User_id, userroleid, clientid, headertext, 0, "", "", "", "",userroleid,Production_Date);
                        rpt_ordersource.Show();
                    }
                }
                else if (tvwRightSide.SelectedNode.Text == "Order Task wise Report")
                {
                    form_loader.Start_progres();
                    if (e.ColumnIndex == 0)
                    {
                        DateTime From_date = Convert.ToDateTime(txt_Fromdate.Text.ToString());
                        DateTime To_date = Convert.ToDateTime(txt_Todate.Text.ToString());
                        //no of orders

                        if (Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString() == "SEARCH")
                        {
                            //SEARCH ORDER SHOW
                            Ordermanagement_01.Reports.Order_Report_View search = new Ordermanagement_01.Reports.Order_Report_View(2, int.Parse(ddl_Client_Status.SelectedValue.ToString()), From_date, To_date, User_id, userroleid,Production_Date);
                            search.Show();
                        }
                        else if (Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString() == "SEARCH QC")
                        {
                            //SEARCH QC ORDER SHOW
                            Ordermanagement_01.Reports.Order_Report_View search = new Ordermanagement_01.Reports.Order_Report_View(3, int.Parse(ddl_Client_Status.SelectedValue.ToString()), From_date, To_date,User_id, userroleid,Production_Date);
                            search.Show();
                        }
                        else if (Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString() == "TYPING")
                        {
                            //TYPING ORDER SHOW
                            Ordermanagement_01.Reports.Order_Report_View search = new Ordermanagement_01.Reports.Order_Report_View(4, int.Parse(ddl_Client_Status.SelectedValue.ToString()), From_date, To_date, User_id, userroleid,Production_Date);
                            search.Show();
                        }
                        else if (Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString() == "TYPING QC")
                        {
                            //TYPING QC ORDER SHOW
                            Ordermanagement_01.Reports.Order_Report_View search = new Ordermanagement_01.Reports.Order_Report_View(7, int.Parse(ddl_Client_Status.SelectedValue.ToString()), From_date, To_date, User_id, userroleid,Production_Date);
                            search.Show();
                        }
                        else if (Grd_OrderTime.Rows[e.RowIndex].Cells[0].Value.ToString() == "UPLOAD")
                        {
                            //UPLOAD ORDER SHOW
                            Ordermanagement_01.Reports.Order_Report_View search = new Ordermanagement_01.Reports.Order_Report_View(12, int.Parse(ddl_Client_Status.SelectedValue.ToString()), From_date, To_date, User_id, userroleid,Production_Date);
                            search.Show();
                        }

                    }
                    else if (e.ColumnIndex == 2)
                    {
                        //no of users
                    }
                }
                else if (tvwRightSide.SelectedNode.Text == "Client Wise Production Count")
                {
                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    int Sub_Process_Id;
                    if (e.ColumnIndex == 1)
                    {

                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_RECIVD_ORDER_DATE_WISE_SUB_CLIENT", "GET_RECIVED_ORDER_DATEWISE_SUB_CLIENT_WISE_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_RECIVD_ORDER_DATE_WISE", "GET_RECIVED_ORDER_DATEWISE_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid, Production_Date);
                            OrderEntry.Show();
                        }


                    }

                    else if (e.ColumnIndex == 2)
                    {

                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_RECIVD_ORDER_MTD_WISE_SUB_CLIENT_WISE", "GET_RECIVED_MTD_ORDER_MISE_COUNT_SUB_CLIENT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid, Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_RECIVD_ORDER_MTD_WISE", "GET_RECIVED_MTD_ORDER_MISE_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid, Production_Date);
                            OrderEntry.Show();
                        }


                    }
                    else if (e.ColumnIndex == 3)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_COMPLETED_ORDER_DATE_SUB_CLIENT_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT_SUB_CLIENT_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_COMPLETED_ORDER_DATE_WISE", "GET_COMPLETED_ORDER_DATE_WISE_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 4)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_COMPLETED_ORDER_MTD_WISE_SUB_PROCESS_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_COMPLETED_ORDER_MTD_WISE", "GET_COMPLETED_ORDER_MTD_WISE_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 5)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_SEARCH_ORDER_SUB_PROCESS_WISE", "GET_SEARCH_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_SEARCH_ORDER", "GET_SEARCH_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }




                    }
                    else if (e.ColumnIndex == 6)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_SEARCH_QC_ORDER_SUB_CLIENT_WISE", "GET_SEARCH_QC_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_SEARCH_QC_ORDER", "GET_SEARCH_QC_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 7)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_TYPING_ORDER_SUB_PROCESS_WISE", "GET_TYPING_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_TYPING_ORDER", "GET_TYPING_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 8)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_TYPING_QC_ORDER_SUB_PROCESS_WIESE", "GET_TYPING_QC_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_TYPING_QC_ORDER", "GET_TYPING_QC_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 9)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_UPLOAD_ORDER_SUB_PROCESS_WISE", "UPLOAD_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_UPLOAD_ORDER", "UPLOAD_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 10)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_ABSTRACTOR_ORDER_SUB_PROCESS_WISE", "GET_ABSTRATCOR_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_ABSTRACTOR_ORDER", "GET_ABSTRATCOR_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 11)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_AFA_ORDER_SUB_PROCESS_WISE", "AFA_ORDER_COUNT_SUB_RPOCESS_WIESE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_AFA_ORDER", "AFA_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }


                    }
                    else if (e.ColumnIndex == 12)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_WFA_ORDER_SUB_PROCESS_WISE", "WFA_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_WFA_ORDER", "WFA_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    else if (e.ColumnIndex == 13)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_Clarification_ORDER_SUB_PROCESS_WISE ", "GET_CLARIFICATION_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_Clarification_ORDER ", "GET_CLARIFICATION_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }




                    }
                    else if (e.ColumnIndex == 14)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_Hold_ORDER_SUB_PROCESS_WISE", "GET_HOLDER_ORDER_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_Hold_ORDER", "GET_HOLDER_ORDER_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }


                    }
                    else if (e.ColumnIndex == 15)
                    {
                        if (Pass_Sub_Process_Id != 0)
                        {

                            Sub_Process_Id = int.Parse(Grd_OrderTime.Rows[e.RowIndex].Cells[17].Value.ToString());
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_Cancelled_ORDER_SUB_PROCESS_WISE", "GET_CANCEELED_COUNT_SUB_PROCESS_WISE", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }
                        else
                        {

                            Sub_Process_Id = 0;
                            Ordermanagement_01.Order_View OrderEntry = new Ordermanagement_01.Order_View(Grd_OrderTime.Rows[e.RowIndex].Cells[16].Value.ToString(), "GET_Cancelled_ORDER", "GET_CANCEELED_COUNT", txt_Fromdate.Text, txt_Todate.Text, User_id, Sub_Process_Id, userroleid,Production_Date);
                            OrderEntry.Show();
                        }



                    }
                    //cProbar.stopProgress();
                }
            }
        }

        private void Grd_OrderTime_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ddl_Client_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Client_Status.SelectedIndex > 0)
            {


                TreeNode tn = tvwRightSide.SelectedNode;


                if (tn != null && tvwRightSide.SelectedNode.Text != "User Production Summary")
                {
                    if (Lbl_Title.Text == "Order Source Report")
                    {
                        if (int.Parse(ddl_Client_Status.SelectedValue.ToString()) == 2)
                        {
                            lbl_Subprocess_Status.Visible = true;
                            lbl_Subprocess_Status.Text = "Client Name :";
                            ddl_Subprocess_Status.Visible = true;
                            if (userroleid == "1")
                            {
                                dbc.BindClientName(ddl_Subprocess_Status);
                            }
                            else if (userroleid == "2")
                            {
                                dbc.BindClientNo_for_Report(ddl_Subprocess_Status);

                            } 


                        }
                        else
                        {
                            lbl_Subprocess_Status.Visible = false;
                            lbl_Subprocess_Status.Text = "SubProcess Name :";
                            ddl_Subprocess_Status.Visible = false;
                        }

                    }
                    else
                    {
                        int clientid = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                        if (userroleid == "1")
                        {
                            dbc.BindSubProcessName_rpt(ddl_Subprocess_Status, clientid);
                        }
                        else if (userroleid == "2")
                        {

                            dbc.BindSubProcessNo_rpt(ddl_Subprocess_Status, clientid);
                        }
                        ddl_Subprocess_Status.Focus();
                    }
                }
                
                else
                {
                    int clientid = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                  
                    if (userroleid == "1")
                    {
                        dbc.Bind_UserClientSubprocess_rpt(ddl_Subprocess_Status, clientid);
                    }
                    else if (userroleid == "2")
                    {

                        dbc.BindSubProcessNo_rpt(ddl_Subprocess_Status, clientid);
                    }
                    ddl_Subprocess_Status.Focus();
                }
            }

            else
            {
                //dbc.BindSubProcessName_rpt1(ddl_SubProcess);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //Hashtable ht_Status = new Hashtable();
            //System.Data.DataTable dt_Status = new System.Data.DataTable();

            //dt_Status = dataaccess.ExecuteSP("Sp_Report_Export", ht_Status);

            //if (dt_Status.Rows.Count > 0)
            //{

            //    ds.Tables.Add(dt_Status);

            //    Convert_Dataset_to_Excel();
            //}
        }


        private void Load_Check_List_data()
        {
            //if (ddl_Check_List_Task.SelectedIndex > 0)
            //{
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();

            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");
            rptDoc = new Reports.CrystalReport.Order_Check_List_Report();

            Logon_To_Crystal();
            string Sub_Client;
            string Client;
            string Username;
            int Client_Id, Sub_Client_Id, User_Id;
            if (ddl_Client_Status.SelectedIndex > 0)
            {
                Client_Id = int.Parse(ddl_Client_Status.SelectedValue.ToString());
                Client = ddl_Client_Status.SelectedValue.ToString();
            }
            else
            {
                Client_Id = 0;

                Client = "ALL";

            }
            if (ddl_Subprocess_Status.SelectedIndex > 0)
            {
                Sub_Client_Id = int.Parse(ddl_Subprocess_Status.SelectedValue.ToString());
                Sub_Client = ddl_Subprocess_Status.SelectedValue.ToString();
            }
            else
            {
                Sub_Client_Id = 0;
                Sub_Client = "ALL";

            }
            if (ddl_Check_List_UserName.SelectedIndex > 0)
            {
                User_Id = int.Parse(ddl_Check_List_UserName.SelectedValue.ToString());
                Username = ddl_Check_List_UserName.SelectedValue.ToString();
            }
            else
            {
                User_Id = 0;
                Username = "ALL";
            }


            if (Client == "ALL" && Sub_Client == "ALL" && Username == "ALL")
            {

                rptDoc.SetParameterValue("@Trans", "SELECT_DATE_RANGE_WISE");
            }
            else if (Client != "ALL" && Sub_Client == "ALL" && Username == "ALL")
            {

                rptDoc.SetParameterValue("@Trans", "CLIENT_WISE");
            }
            else if (Client != "ALL" && Sub_Client != "ALL" && Username == "ALL")
            {

                rptDoc.SetParameterValue("@Trans", "CLEINT_SUB_CLIENT_WISE");
            }
            else if (Client != "ALL" && Sub_Client != "ALL" && Username != "ALL")
            {

                rptDoc.SetParameterValue("@Trans", "CLEINT_SUB_CLIENT_USER_WISE");
            }

            else if (Client != "ALL" && Username != "ALL")
            {
                rptDoc.SetParameterValue("@Trans", "CLEINT_USER_WISE");

            }
            else if (Client == "ALL" && Username != "ALL")
            {
                rptDoc.SetParameterValue("@Trans", "USER_WISE");

            }

            rptDoc.SetParameterValue("@Task", int.Parse(ddl_Check_List_Task.SelectedValue.ToString()));
            rptDoc.SetParameterValue("@From_date", From_Date);
            rptDoc.SetParameterValue("@To_date", To_Date);
            rptDoc.SetParameterValue("@Order_Id", 0);
            rptDoc.SetParameterValue("@Client_Id", Client_Id);
            rptDoc.SetParameterValue("@Sub_Client_Id", Sub_Client_Id);
            rptDoc.SetParameterValue("@User_Id", User_Id);
            rptDoc.SetParameterValue("@Work_Type_Id", 1);
            rptDoc.SetParameterValue("@Log_In_Userid", Loged_In_User_Id);
            crViewer.ReportSource = rptDoc;
            //}
            //    else
            //  {

            //      MessageBox.Show("Please Select Task");
            //  }
        }

        private void Load_Orders_Error_Info_Report()
        {
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());


            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;


            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();

            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");

            ht_Status.Clear();
            dt_Status.Clear();
            string Client, SubProcess;

            if (ddl_Client_Status.SelectedIndex > 0)
            {

                Client = ddl_Client_Status.SelectedValue.ToString();
            }
            else
            {

                Client = "ALL";
            }

            if (ddl_Subprocess_Status.SelectedIndex > 0)
            {

                SubProcess = ddl_Subprocess_Status.SelectedValue.ToString();
            }
            else
            {

                SubProcess = "ALL";
            }


            if (Client == "ALL")
            {
                ht_Status.Add("@Trans", "SELECT_ERROR_DATE_RANGE");
                ht_Status.Add("@Fromdate", From_Date);
                ht_Status.Add("@Todate", To_Date);
                ht_Status.Add("@Log_In_Userid", Loged_In_User_Id);
                ht_Status.Add("@Work_Type", 1);

                dt_Status = dataaccess.ExecuteSP("Sp_Rpt_Order_ErrorReport", ht_Status);

            }
            else if (Client != "ALL" && SubProcess == "ALL")
            {
                ht_Status.Add("@Trans", "CLIENT_WISE");
                ht_Status.Add("@Fromdate", From_Date);
                ht_Status.Add("@Todate", To_Date);
                ht_Status.Add("@Client_Id", Client);
                ht_Status.Add("@Work_Type", 1);
                dt_Status = dataaccess.ExecuteSP("Sp_Rpt_Order_ErrorReport", ht_Status);

            }
            else if (Client != "ALL" && SubProcess != "ALL")
            {
                ht_Status.Add("@Trans", "CLIENT_SUB_PROCESS_WISE");

                ht_Status.Add("@Fromdate", From_Date);
                ht_Status.Add("@Todate", To_Date);
                ht_Status.Add("@Client_Id", Client);
                ht_Status.Add("@Sub_Process_Id", SubProcess);
                ht_Status.Add("@Work_Type", 1);
                dt_Status = dataaccess.ExecuteSP("Sp_Rpt_Order_ErrorReport", ht_Status);

            }
            dtordererror.Clear();
            dtordererror = dt_Status;
            if (dtordererror.Rows.Count > 0)
            {
                lbl_Error.Visible = false;

                Grd_OrderTime.Rows.Clear();
                //Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;

                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.AutoGenerateColumns = false;

                Grd_OrderTime.ColumnCount = 15;
                //Grd_OrderTime.Rows.Add();
                Grd_OrderTime.Columns[0].Name = "Orderid";
                Grd_OrderTime.Columns[0].HeaderText = "Order Id";
                Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                Grd_OrderTime.Columns[0].Width = 50;
                Grd_OrderTime.Columns[0].Visible = false;

                Grd_OrderTime.Columns[1].Name = "Client_Order_Number";
                Grd_OrderTime.Columns[1].HeaderText = "Order Number";
                Grd_OrderTime.Columns[1].DataPropertyName = "Client Order Number";
                Grd_OrderTime.Columns[1].Width = 140;

                if (userroleid == "1")
                {
                    Grd_OrderTime.Columns[2].Name = "Client_Name";
                    Grd_OrderTime.Columns[2].HeaderText = "Client Name";
                    Grd_OrderTime.Columns[2].DataPropertyName = "Client";
                    Grd_OrderTime.Columns[2].Width = 120;

                    Grd_OrderTime.Columns[3].Name = "Sub_ProcessName";
                    Grd_OrderTime.Columns[3].HeaderText = "Subprocess Name";
                    Grd_OrderTime.Columns[3].DataPropertyName = "Subprocess";
                    Grd_OrderTime.Columns[3].Width = 300;
                }
                else 
                {
                    Grd_OrderTime.Columns[2].Name = "Client_Name";
                    Grd_OrderTime.Columns[2].HeaderText = "Client Name";
                    Grd_OrderTime.Columns[2].DataPropertyName = "Client_Number";
                    Grd_OrderTime.Columns[2].Width = 120;

                    Grd_OrderTime.Columns[3].Name = "Sub_ProcessName";
                    Grd_OrderTime.Columns[3].HeaderText = "Subprocess Name";
                    Grd_OrderTime.Columns[3].DataPropertyName = "Subprocess_Number";
                    Grd_OrderTime.Columns[3].Width = 300;

                }

                Grd_OrderTime.Columns[4].Name = "Order_Type";
                Grd_OrderTime.Columns[4].HeaderText = "Order Type";
                Grd_OrderTime.Columns[4].DataPropertyName = "Order Type";
                Grd_OrderTime.Columns[4].Width = 300;

                Grd_OrderTime.Columns[5].Name = "Error_Type";
                Grd_OrderTime.Columns[5].HeaderText = "Error Type";
                Grd_OrderTime.Columns[5].DataPropertyName = "Error Type";
                Grd_OrderTime.Columns[5].Width = 150;

                Grd_OrderTime.Columns[6].Name = "Error_description";
                Grd_OrderTime.Columns[6].HeaderText = "Error Description";
                Grd_OrderTime.Columns[6].DataPropertyName = "Error description";
                Grd_OrderTime.Columns[6].Width = 200;

                Grd_OrderTime.Columns[7].Name = "Comments";
                Grd_OrderTime.Columns[7].HeaderText = "Comments";
                Grd_OrderTime.Columns[7].DataPropertyName = "Comments";
                Grd_OrderTime.Columns[7].Width = 200;

                Grd_OrderTime.Columns[8].Name = "Error_On_Status";
                Grd_OrderTime.Columns[8].HeaderText = "Error Status";
                Grd_OrderTime.Columns[8].DataPropertyName = "Error On Status";
                Grd_OrderTime.Columns[8].Width = 150;

                Grd_OrderTime.Columns[9].Name = "Error_On_User_Name";
                Grd_OrderTime.Columns[9].HeaderText = "Error User Name";
                Grd_OrderTime.Columns[9].DataPropertyName = "Error On User Name";
                Grd_OrderTime.Columns[9].Width = 150;

                Grd_OrderTime.Columns[10].Name = "Error On DRN Emp Code";
                Grd_OrderTime.Columns[10].HeaderText = "Error Emp Code";
                Grd_OrderTime.Columns[10].DataPropertyName = "DRN_Emp_Code";
                Grd_OrderTime.Columns[10].Width = 150;


                Grd_OrderTime.Columns[11].Name = "Error_From_Status";
                Grd_OrderTime.Columns[11].HeaderText = "QC Status";
                Grd_OrderTime.Columns[11].DataPropertyName = "Error From Status";
                Grd_OrderTime.Columns[11].Width = 150;

                Grd_OrderTime.Columns[12].Name = "Error_Enter_By_Username";
                Grd_OrderTime.Columns[12].HeaderText = "QC Username";
                Grd_OrderTime.Columns[12].DataPropertyName = "Error From Username";
                Grd_OrderTime.Columns[12].Width = 160;

                Grd_OrderTime.Columns[13].Name = "Error Entered from emp_code";
                Grd_OrderTime.Columns[13].HeaderText = "QC Emp Code";
                Grd_OrderTime.Columns[13].DataPropertyName = "drn_emp_code";
                Grd_OrderTime.Columns[13].Width = 160;

                Grd_OrderTime.Columns[14].Name = "Order_Production_Date";
                Grd_OrderTime.Columns[14].HeaderText = "Production Date";
                Grd_OrderTime.Columns[14].DataPropertyName = "Order Production Date";
                Grd_OrderTime.Columns[14].Width = 120;

                Grd_OrderTime.DataSource = dt_Status;





            }
            else
            {
                Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }
        }

        private void lbl_Subprocess_Status_Click(object sender, EventArgs e)
        {

        }

        private void btn_Clear_All_Click(object sender, EventArgs e)
        {
            txt_Fromdate.Value = DateTime.Now;
            txt_Todate.Value = DateTime.Now;
            ddl_Check_List_Task.SelectedIndex = 0;
            ddl_Client_Status.SelectedIndex = 0;
            ddl_Subprocess_Status.Text = "";
            //dbc.BindSubProcess(ddl_Subprocess_Status, int.Parse(ddl_Client_Status.SelectedValue.ToString()));
            // ddl_Subprocess_Status.SelectedIndex = 0;
            ddl_Check_List_UserName.SelectedIndex = 0;
            ddl_ClientName.SelectedIndex = 0;
            ddl_SubProcess.Text = "";
            //dbc.BindSubProcess(ddl_SubProcess, int.Parse(ddl_ClientName.SelectedValue.ToString()));
            //ddl_OrderNumber.SelectedIndex = 0;
            ddl_EmployeeName.SelectedIndex = 0;
            ddl_Status.SelectedIndex = 0;
            ddl_Task.SelectedIndex = 0;
            if (Lbl_Title.Text == "User Production Report")
            {
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                crViewer.Visible = false;
            }
            else if (Lbl_Title.Text == "User Production Count")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = false;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                crViewer.Visible = false;

            }
            else if (Lbl_Title.Text == "User Production Summary")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Export_Title_Name = "User_Production_Summary";
                crViewer.Visible = false;
            }
            else if (Lbl_Title.Text == "Productivity Report")
            {
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Export_Title_Name = "User_Productivity";
            }
            else if (Lbl_Title.Text == "Progress Wise Counts")
            {
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Grd_OrderTime.Rows.Clear();
            }
            else if (Lbl_Title.Text == "Client Status Report")
            {
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
            }
            else if (Lbl_Title.Text == "Client Wise Production Report")
            {
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
            }
            else if (Lbl_Title.Text == "Client Wise Production Count")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Export_Title_Name = "Client_Production_Count";
            }
            else if (Lbl_Title.Text == "Billing Report")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Export_Title_Name = "Billing_Report";
            }
            else if (Lbl_Title.Text == "User Break Report")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Export_Title_Name = "User_Break_Report";
            }
            else if (Lbl_Title.Text == "Client wise Subscription Report" || Lbl_Title.Text == "Employee wise Subscription Report")
            {
                Grd_OrderTime.AutoGenerateColumns = false;
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;

            }
            else if (Lbl_Title.Text == "Orders Document List Report")
            {
                Grd_OrderTime.AutoGenerateColumns = true;
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Export_Title_Name = "Document_List_Report";
            }
            else if (Lbl_Title.Text == "Orders Check List Report")
            {
                Grd_OrderTime.AutoGenerateColumns = true;
                Grd_OrderTime.DataSource = null;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
            }
            else if (Lbl_Title.Text == "Orders Error Info Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
                Export_Title_Name = "Error_Info_Report";
            }
            else if (Lbl_Title.Text == "Order Source Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;

                if (ddl_Subprocess_Status.Visible == true)
                {
                    ddl_Subprocess_Status.Visible = false;
                    lbl_Subprocess_Status.Visible = false;
                }
                Export_Title_Name = "Order_Source_Reprot";
            }
            else if (Lbl_Title.Text == "Order Received Date Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;

                Export_Title_Name = "Order Received Date Report";
            }
            else if (Lbl_Title.Text == "Order Task wise Report")
            {
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.Visible = true;
                lbl_Error.Text = "Select Proper fileds in the left side treeview";
                lbl_Error.Visible = false;
            }
            else
            {
                lbl_Error.Visible = true;
            }
        }

        private void Grd_OrderTime_DataSourceChanged(object sender, EventArgs e)
        {
            //if (Lbl_Title.Text == "Client wise Subscription Report")
            //{
            //    for (int i = 0; i < Grd_OrderTime.Rows.Count; i++)
            //    {

            //    }
            //}
        }



    }
}


