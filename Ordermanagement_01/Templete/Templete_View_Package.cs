using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Ordermanagement_01.Templete
{
    public partial class Templete_View_Package : Form
    {
        
        ReportDocument rptDoc = new ReportDocument();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        string server;
        string Db;
        string Username,Order_No;
        string Password, Assignor;
        int Orderid;
        int TotalTax, Deed_Value, Mortgage_Value;
        string Client_no, Subprocess_no;
        InfiniteProgressBar.clsProgress proloader = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Templete_View_Package(int Order_Id, string OrderNO, string client_no, string subprocess_no)
        {
            InitializeComponent();
            Orderid = Order_Id;
            Order_No = OrderNO;
            Client_no = client_no;
            Subprocess_no = subprocess_no;
        }

        
        private void Templete_View_Load(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //proloader.startProgress();

            SqlConnectionStringBuilder decoder1 = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            server = decoder1.DataSource;
            Db = decoder1.InitialCatalog;
            Username = decoder1.UserID;
            Password = decoder1.Password;

            rptDoc = new Empty_For_Login();
            Logon_Cr();
            if (Client_no == "11000")
            {
                rptDoc = new Crystal_Report111000();
            }
            else if (Client_no == "7000")
            {
                rptDoc = new Crystal_Report17000();
            }
            else if (Client_no == "10000")
            {
                rptDoc = new Crystal_Report110000();
            }
            else if (Client_no == "13000")
            {
              //  rptDoc = new Crystal_Report113000();
            }
            else if (Client_no == "14000")
            {
                rptDoc = new Crystal_Report114000_C();
            }
            else if (Client_no == "1000")
            {
                rptDoc = new Crystal_Report11000();
            }
            else if (Client_no == "8000")
            {
                rptDoc = new Crystal_Report110000();
            }
            Logon_Cr();

            //Order
            //  rptDoc.SetParameterValue("@Trans_Order", "ORDER_INFROMATION");
            //  rptDoc.SetParameterValue("@Order_ID_Order", Orderid);
            //  rptDoc.SetParameterValue("@Client_Order_Number", Orderid);
            ////  rptDoc.SetParameterValue("@TotalTax", TotalTax);

            // //Deed
            //  rptDoc.SetParameterValue("@Trans_Deed", "DEED_INFROMATION", "DEED");
            //  rptDoc.SetParameterValue("@Order_ID_Deed", Orderid, "DEED");

            // // Mortgage
            //  rptDoc.SetParameterValue("@Trans_Mortgage", "Mortgage_INFROMATION", "Mortgage");
            //  rptDoc.SetParameterValue("@Order_ID_Mortgage", Orderid, "Mortgage");
            //  rptDoc.SetParameterValue("@Trans_Mortgage_Additional_INFROMATION", "Mortgage_Additional_INFROMATION", "Mortgage");
            //  rptDoc.SetParameterValue("@Order_ID_Mortgage_Additional_INFROMATION", Orderid, "Mortgage");
            //  rptDoc.SetParameterValue("@Trans_Mortgage_Additional_INFROMATION_Details", "Mortgage_Additional_INFROMATION_Details", "Mortgage");
            //  rptDoc.SetParameterValue("@Order_ID_Mortgage_Additional_INFROMATION_Details", Orderid, "Mortgage");

            //  if (Assignor != null)
            //  {
            //      rptDoc.SetParameterValue("@Assignor", Assignor, "Mortgage");
            //  }
            //  else
            //  {
            //      rptDoc.SetParameterValue("@Assignor", "", "Mortgage");
            //  }
            //  //Mortgage Additional_Information
            // // rptDoc.SetParameterValue("@Trans_Mortgage_Additional_INFROMATION", "Mortgage_Additional_INFROMATION", "Mortgage_Additional_Information");
            // // rptDoc.SetParameterValue("@Order_ID_Mortgage_Additional_INFROMATION", Orderid, "Mortgage_Additional_Information");
            //  ////Total Tax
            //  //rptDoc.SetParameterValue("@Trans_Total_Tax", "Total_Tax_INFROMATION", "Total_Tax");
            //  //rptDoc.SetParameterValue("@Order_ID_Total_Tax", Orderid, "Total_Tax");

            // //Tax
            //  rptDoc.SetParameterValue("@Trans_Tax", "TAX_INFROMATION", "TAX");
            //  rptDoc.SetParameterValue("@Order_ID_Tax", Orderid, "TAX");

            //  //Judgment
            //  rptDoc.SetParameterValue("@Trans_Judgment", "Judgment_INFROMATION", "JUDGMENT");
            //  rptDoc.SetParameterValue("@Order_ID_Judgment", Orderid, "JUDGMENT");
            //  rptDoc.SetParameterValue("@Trans_Judgement_Additional_INFROMATION", "Judgement_Additional_INFROMATION", "JUDGMENT");
            //  rptDoc.SetParameterValue("@Order_ID_Judgement_Additional_INFROMATION", Orderid, "JUDGMENT");

            // // //Assessment
            // // rptDoc.SetParameterValue("@Trans_Assessment", "Assessment_INFROMATION", "Assessment");
            // // rptDoc.SetParameterValue("@Order_ID_Assessment", Orderid, "Assessment");

            //  //LEGAL DESCRIPTION
            //  rptDoc.SetParameterValue("@Trans_Legal_Description", "Legal_Description_INFROMATION", "LEGAL DESCRIPTION");
            //  rptDoc.SetParameterValue("@Order_ID_Legal_Description", Orderid, "LEGAL DESCRIPTION");

            //  //Additional_Information
            //  rptDoc.SetParameterValue("@Trans_Additional_Information", "Additional_Information_INFROMATION", "Additional_Information");
            //  rptDoc.SetParameterValue("@Order_ID_Additional_Information", Orderid, "Additional_Information");

            rptDoc.SetParameterValue("@Trans_Order", "ORDER_INFROMATION");
            rptDoc.SetParameterValue("@Order_ID_Order", Orderid);
            rptDoc.SetParameterValue("@Client_Order_Number", Order_No);
            //  rptDoc.SetParameterValue("@TotalTax", TotalTax);

            //Deed
            rptDoc.SetParameterValue("@Trans_Deed", "DEED_INFROMATION", "DEED");
            rptDoc.SetParameterValue("@Order_ID_Deed", Orderid, "DEED");

            // Mortgage
            rptDoc.SetParameterValue("@Trans_Mortgage", "Mortgage_INFROMATION", "Mortgage");
            rptDoc.SetParameterValue("@Order_ID_Mortgage", Orderid, "Mortgage");



            //Mortgage Additional_Information
            // rptDoc.SetParameterValue("@Trans_Mortgage_Additional_INFROMATION", "Mortgage_Additional_INFROMATION", "Mortgage_Additional_Information");
            // rptDoc.SetParameterValue("@Order_ID_Mortgage_Additional_INFROMATION", Orderid, "Mortgage_Additional_Information");
            ////Total Tax
            //rptDoc.SetParameterValue("@Trans_Total_Tax", "Total_Tax_INFROMATION", "Total_Tax");
            //rptDoc.SetParameterValue("@Order_ID_Total_Tax", Orderid, "Total_Tax");

            //Tax
            rptDoc.SetParameterValue("@Trans_Tax", "TAX_INFROMATION", "TAX");
            rptDoc.SetParameterValue("@Order_ID_Tax", Orderid, "TAX");

            //Judgment
            rptDoc.SetParameterValue("@Trans_Judgment", "Judgment_INFROMATION", "JUDGMENT");
            rptDoc.SetParameterValue("@Order_ID_Judgment", Orderid, "JUDGMENT");


            //Assessment
            if (Client_no != "11000")
            {
                rptDoc.SetParameterValue("@Trans_Assessment", "Assessment_INFROMATION", "Assessment");
                rptDoc.SetParameterValue("@Order_ID_Assessment", Orderid, "Assessment");
            }
            

            //LEGAL DESCRIPTION
            rptDoc.SetParameterValue("@Trans_Legal_Description", "Legal_Description_INFROMATION", "LEGAL DESCRIPTION");
            rptDoc.SetParameterValue("@Order_ID_Legal_Description", Orderid, "LEGAL DESCRIPTION");

            //Additional_Information
            rptDoc.SetParameterValue("@Trans_Additional_Information", "Additional_Information_INFROMATION", "Additional_Information");
            rptDoc.SetParameterValue("@Order_ID_Additional_Information", Orderid, "Additional_Information");


            
            crystalReportViewer1.ReportSource = rptDoc;

           //proloader.stopProgress();
        }
        private void Logon_Cr()
        {
            crConnectionInfo.ServerName = server;
            crConnectionInfo.DatabaseName = Db;
            crConnectionInfo.UserID = Username;
            crConnectionInfo.Password = Password;
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
    }
}
