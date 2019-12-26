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
    public partial class Abstractor_Payment_Preview : Form
    {
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        string server = "192.168.12.33";
        string database = "TITLELOGY_NEW_OMS";
        string UserID = "sa";
        string password = "password1$";
        int Order_Id;

        string Client_Order_no;
        int Order_Type;
        int abstarctor_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        int Abs_Invoice_ID;

        public Abstractor_Payment_Preview(int ABS_ID,int ABS_INVOICE_ID)
        {
            InitializeComponent();
            Abs_Invoice_ID = ABS_INVOICE_ID;
            abstarctor_id = ABS_ID;
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

        private void Abstractor_Payment_Preview_Load(object sender, EventArgs e)
        {
            rptDoc = new Abstractor.Abstractor_Reports.Abstractor_Payment();
            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Abstractor_Monthly_Invoice_ID", Abs_Invoice_ID);
            crViewer.ReportSource = rptDoc;
        }

    }
}
