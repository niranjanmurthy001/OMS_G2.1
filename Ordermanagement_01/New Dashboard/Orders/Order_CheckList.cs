using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DevExpress.XtraEditors;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using CrystalDecisions.Shared;
using Ordermanagement_01.Models;


namespace Ordermanagement_01.New_Dashboard.Orders
{
    public partial class Order_CheckList : XtraForm
    {
        ReportDocument RD = new ReportDocument();
        Commonclass Comclass = new Commonclass();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        DataSet ds = new DataSet();
        int Order_Id, Work_Type_Id, User_Role, User_ID, Order_Task, Selected_Order_Task;
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex > 0)
            {
                if (radioGroup1.SelectedIndex == 0)
                {
                    Selected_Order_Task = 2;
                }
                else if (radioGroup1.SelectedIndex == 1)
                {
                    Selected_Order_Task = 3;
                }
                else if (radioGroup1.SelectedIndex == 2)
                {
                    Selected_Order_Task = 4;
                }
                else if (radioGroup1.SelectedIndex == 3)
                {
                    Selected_Order_Task = 7;
                }
                else if (radioGroup1.SelectedIndex == 4)
                {
                    Selected_Order_Task = 23;
                }
                else if (radioGroup1.SelectedIndex == 5)
                {
                    Selected_Order_Task = 24;
                }
            }
            CR_Report(Order_Id, Selected_Order_Task);
        }
        public Order_CheckList(Order_Passing_Params obj_Order_Details_List)
        {
            InitializeComponent();
            Order_Id = obj_Order_Details_List.Order_Id;
            this.Order_Task = obj_Order_Details_List.Order_Status_ID;
            Work_Type_Id = obj_Order_Details_List.Work_Type_Id;
            User_Role = obj_Order_Details_List.User_Role_Id;
            User_ID = obj_Order_Details_List.User_Id;
        }
        private void Order_CheckList_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            Load_Sample_Report_For_Faster();
            RD = new Reports.CrystalReport.Order_CheckList();
            if (Order_Task == 2)
            {
                radioGroup1.SelectedIndex = 0;
            }
            else if (Order_Task == 3)
            {
                radioGroup1.SelectedIndex = 1;
            }
            else if (Order_Task == 4)
            {
                radioGroup1.SelectedIndex = 2;
            }
            else if (Order_Task == 7)
            {
                radioGroup1.SelectedIndex = 3;
            }
            else if (Order_Task == 23)
            {
                radioGroup1.SelectedIndex = 4;
            }
            else if (Order_Task == 24)
            {
                radioGroup1.SelectedIndex = 5;
            }
            else if (Order_Task == 12)
            {
                radioGroup1.SelectedIndex = 6;
            }
            radioGroup1_SelectedIndexChanged(sender, e);
            SplashScreenManager.CloseForm(false);
        }
        private async void CR_Report(int Order_Id, int Status_ID)
        {
            int chk_count = 0;
            DataTable dt = new DataTable();
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                    {"@Trans","CHECK_COUNT" },
                    {"@Order_Id",Order_Id },
                    {"@Work_Type_Id",Work_Type_Id },
                    {"@Order_Task", Status_ID}
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Order_Check_List/OrderCheckList", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (int.Parse(dt.Rows[0]["COUNT"].ToString()) > 0)
                            {
                                chk_count = 1;
                            }
                            else
                            {
                                chk_count = 0;
                            }
                            if (chk_count == 0)
                            {
                                RD = new Reports.CrystalReport.Order_CheckList();
                                Logon_Cr();
                                if (Work_Type_Id == 1)
                                {
                                    RD.SetParameterValue("@Trans", "SELECT_USER_TASK_WISE");
                                }
                                else if (Work_Type_Id == 2)
                                {
                                    RD.SetParameterValue("@Trans", "SELECT_USER_REWORK_TASK_WISE");
                                }
                                else if (Work_Type_Id == 3)
                                {
                                    RD.SetParameterValue("@Trans", "SELECT_USER_SUPER_QC_TASK_WISE");
                                }
                            }
                            else if (chk_count == 1)
                            {
                                RD = new Reports.CrystalReport.Order_CheckList();
                                Logon_Cr();
                                if (Work_Type_Id == 1)
                                {
                                    RD.SetParameterValue("@Trans", "SELECT_USER_TASK_WISE_NEW");
                                }
                                else if (Work_Type_Id == 2)
                                {
                                    RD.SetParameterValue("@Trans", "SELECT_USER_REWORK_TASK_WISE_NEW");
                                }
                                else if (Work_Type_Id == 3)
                                {
                                    RD.SetParameterValue("@Trans", "SELECT_USER_SUPER_QC_TASK_WISE_NEW");
                                }
                            }
                            RD.SetParameterValue("@Order_Id", Order_Id);
                            RD.SetParameterValue("@Order_Task", Status_ID);
                            RD.SetParameterValue("@Work_Type_Id", Work_Type_Id);
                            crystalReportViewer1.ReportSource = RD;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Something Went Wrong,Please Contact Admin");
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void Load_Sample_Report_For_Faster()
        {
            RD = new Reports.CrystalReport.Order_CheckList();
            Logon_Cr();
            //crystalReportViewer1.ReportSource = RD;
        }
        private void Logon_Cr()
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            List<string> cl_Lgoin = Comclass.Crystal_report_Login();
            crConnectionInfo.ServerName = cl_Lgoin[0].ToString();
            crConnectionInfo.DatabaseName = cl_Lgoin[1].ToString();
            crConnectionInfo.UserID = cl_Lgoin[2].ToString();
            crConnectionInfo.Password = cl_Lgoin[3].ToString();
            CrTables = RD.Database.Tables;
            foreach (Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            SplashScreenManager.CloseForm(false);
        }
    }
}