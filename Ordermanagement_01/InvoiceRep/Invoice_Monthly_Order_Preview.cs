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
using System.DirectoryServices;
using System.IO;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;

namespace Ordermanagement_01.InvoiceRep
{
    public partial class Invoice_Monthly_Order_Preview : Form
    {
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string server = "192.168.12.33";
        string database = "TITLELOGY_NEW_OMS";
        string UserID = "sa";
        string password = "password1$";
        int Invoice_Id;

        string Client_Order_no;
        int Order_Type;
        int abstarctor_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        int Sub_Process_Id;
        string User_Role;
        int Client_Id;
        public Invoice_Monthly_Order_Preview(int INVOICE_ID,int SUBPROCESS_ID,string USER_ROLE,int CLIENT_ID)
        {
         
            InitializeComponent();
            Invoice_Id = INVOICE_ID;
            Sub_Process_Id = SUBPROCESS_ID;
            Client_Id = CLIENT_ID;
            User_Role=USER_ROLE;
           
        }
        public void Logon_To_Crystal()
        {

            crConnectionInfo.ServerName = server;
            crConnectionInfo.DatabaseName = database;
            crConnectionInfo.UserID = UserID;
            crConnectionInfo.Password = password;
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
       

        private void Invoice_Monthly_Order_Preview_Load(object sender, EventArgs e)
        {

            if (Client_Id==4)
            {

                rptDoc = new InvoiceRep.InvReport.Invoice_Monthly_Report_Rdc();
            }

            else if (Sub_Process_Id == 102)
            {

                rptDoc = new InvoiceRep.InvReport.Invoice_Monthly_Report_WCARP();
            }
            else
            {


                rptDoc = new InvoiceRep.InvReport.Invoice_Monthly_Report();
            }

            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", Invoice_Id);
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", Invoice_Id, "Individual");
            rptDoc.SetParameterValue("User_Role", User_Role);
            crViewer.ReportSource = rptDoc;
        }

    }
}
