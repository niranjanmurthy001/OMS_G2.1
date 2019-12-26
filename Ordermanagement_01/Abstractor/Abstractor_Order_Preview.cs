using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;

namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Order_Preview : Form
    { 
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();


        int Order_Id;
           
        string Client_Order_no,DEED_CHAIN,mailid; 
        int Order_Type,ClientId;
        int abstarctor_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Commonclass Comclass = new Commonclass();
        Tables CrTables;
        public Abstractor_Order_Preview(int ORDER_ID,int ABSTRACTOR_ID,string CLIENT_ORDERNO,int ORDER_TYPE_ID,string deedchain,string Email_Id,int clientid)
        {
            Order_Id = ORDER_ID;
            abstarctor_id = ABSTRACTOR_ID;
            Client_Order_no = CLIENT_ORDERNO.ToString();
            Order_Type = ORDER_TYPE_ID;
            DEED_CHAIN = deedchain;
            mailid = Email_Id;
            ClientId = clientid;
            InitializeComponent();
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

            foreach (ReportDocument sr in rptDoc.Subreports)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in sr.Database.Tables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);

                }
            }
        }

        private void Abstractor_Order_Preview_Load(object sender, EventArgs e)
        {

            if (DEED_CHAIN == "Yes")
            {
                rptDoc = new Abstractor_Reports.Abstract_Order_Search_Deed_Chain();
                Logon_To_Crystal();
                if (mailid == "neworders@abstractshop.com")
                {
                    rptDoc.SetParameterValue("@Trans", "ABS");
                 
                }
                else if (mailid == "vendors@drnds.com")
                {
                    rptDoc.SetParameterValue("@Trans", "DRN");
                }
                rptDoc.SetParameterValue("@Order_ID", Order_Id);
                rptDoc.SetParameterValue("@Abstractor_Id", abstarctor_id);
                rptDoc.SetParameterValue("@Abstractor_Cost", "True");
                crViewer.ReportSource = rptDoc;
            }
            else if (DEED_CHAIN == "No")
            {

                if (Order_Type == 1 || Order_Type == 5)
                {

                    rptDoc = new Abstractor_Reports.Current_Owner_Search();
                    Logon_To_Crystal();
                   



                }
                else if (Order_Type == 29)
                {
                    rptDoc = new Abstractor_Reports.Two_Owner_Search();
                    Logon_To_Crystal();
                  

                }
                else if (Order_Type == 30)
                {
                    rptDoc = new Abstractor_Reports.ThirtyYears_Search();
                    Logon_To_Crystal();
                    

                }
                else if (Order_Type == 36)
                {
                    rptDoc = new Abstractor_Reports.Full_Search();
                    Logon_To_Crystal();
                   
                  

                }
                else if (Order_Type == 7)
                {
                    rptDoc = new Abstractor_Reports.Update_Search();
                    Logon_To_Crystal();
                   

                }
                else if (Order_Type == 21)
                {
                    rptDoc = new Abstractor_Reports.DocumentRetrival();
                    Logon_To_Crystal();
                  

                }
                else if (Order_Type == 43)
                {
                    rptDoc = new Abstractor_Reports.Twenty_Year_Search();
                    Logon_To_Crystal();
                   

                }
                else if (Order_Type == 49)
                {
                    rptDoc = new Abstractor_Reports.Fifty_Year_Search();
                    Logon_To_Crystal();
                  

                }
                else if (Order_Type == 70)
                {
                    rptDoc = new Abstractor_Reports.Current_Owner_Deed_Chain();
                    Logon_To_Crystal();
                   
                }
                else if (Order_Type == 71)
                {
                    rptDoc = new Abstractor_Reports.Two_Owner_DeadChain();
                    Logon_To_Crystal();
                   

                }


                else if (Order_Type == 38)
                {
                    rptDoc = new Abstractor_Reports.FourtyYearSearch();
                    Logon_To_Crystal();
                   

                }
                else if (Order_Type == 72)
                {
                    rptDoc = new Abstractor_Reports.HundredYearSearch();
                    Logon_To_Crystal();
                    

                }
                else if (Order_Type == 32)
                {
                    rptDoc = new Abstractor_Reports.Mortgage_Assessment_Search();
                    Logon_To_Crystal();
                   
                }
                else if (Order_Type == 73)
                {
                    rptDoc = new Abstractor_Reports.Full_Owner_Deed_Chain();
                    Logon_To_Crystal();
                  

                }
                else if (Order_Type == 76)
                {


                    rptDoc = new  Abstractor_Reports.Current_Owner_Search_Report();
                    Logon_To_Crystal();

                }
              
                rptDoc.SetParameterValue("@Order_ID", Order_Id);
                rptDoc.SetParameterValue("@Abstractor_Id", abstarctor_id);
                rptDoc.SetParameterValue("@Abstractor_Cost", "True");
                if (mailid == "neworders@abstractshop.com")
                {
                    rptDoc.SetParameterValue("@Trans", "ABS");
                }
                else if (mailid == "vendors@drnds.com")
                {
                    rptDoc.SetParameterValue("@Trans", "DRN");
                }
                crViewer.ReportSource = rptDoc;

            }
          

        }

      

   
    }
   
}
