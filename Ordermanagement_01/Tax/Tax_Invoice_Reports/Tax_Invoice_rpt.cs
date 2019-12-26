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
using System.Collections;
using CrystalDecisions.Shared;


namespace Ordermanagement_01.Tax.Tax_Invoice_Reports
{
    public partial class Tax_Invoice_rpt : Form
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
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        Tables CrTables;
        public Tax_Invoice_rpt(int invoice_id)
        {
            InitializeComponent();
            Invoice_Id = invoice_id;
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


        }
        private void Tax_Invoice_rpt_Load(object sender, EventArgs e)
        {
            if (Invoice_Id != 0)
            {
                rptDoc = new Tax.Tax_Invoice_Reports.Tax_Invoice_report();
            }
            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Tax_Invoice_Id", Invoice_Id);
            crViewer.ReportSource = rptDoc;
        }
    }
}
