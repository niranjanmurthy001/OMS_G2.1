using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace Ordermanagement_01.Tax.Tax_Invoice_Reports
{
    public partial class Tax_Monthly_Invoice_rpt : Form
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
        int Invoice_Id, ClientId, commonid;
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        Tables CrTables;
        public Tax_Monthly_Invoice_rpt(int invoiceid,int clientid)
        {
            InitializeComponent();
            Invoice_Id = invoiceid;
            ClientId = clientid;
            commonid = 0;
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

        private void Tax_Monthly_Invoice_rpt_Load(object sender, EventArgs e)
        {

            if (Invoice_Id != 0)
            {
                rptDoc = new Tax.Tax_Invoice_Reports.Tax_Invoice_Monthly_rpt();
            }
            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", Invoice_Id);
            rptDoc.SetParameterValue("@Trans", "SELECT_Client_ID");
            rptDoc.SetParameterValue("@Client_Id", ClientId);
            rptDoc.SetParameterValue("@Branch_ID", commonid);
            rptDoc.SetParameterValue("@Client_Number", commonid);
            rptDoc.SetParameterValue("@Client_Name", "");
            rptDoc.SetParameterValue("@Client_Code", "");
            rptDoc.SetParameterValue("@Client_Photo", "");
            rptDoc.SetParameterValue("@Client_Country", commonid);
            rptDoc.SetParameterValue("@Client_State", commonid);
            rptDoc.SetParameterValue("@Client_District", commonid);
            rptDoc.SetParameterValue("@Client_City", "");
            rptDoc.SetParameterValue("@Client_Pin", "");
            rptDoc.SetParameterValue("@Client_Address", "");
            rptDoc.SetParameterValue("@Client_Phone", "");
            rptDoc.SetParameterValue("@Client_Fax", "");
            rptDoc.SetParameterValue("@Client_Email", "");
            rptDoc.SetParameterValue("@Client_Web", "");
            rptDoc.SetParameterValue("@Inserted_By", commonid);
            rptDoc.SetParameterValue("@Inserted_date", DateTime.Now);
            rptDoc.SetParameterValue("@Modified_By", commonid);
            rptDoc.SetParameterValue("@Modified_Date", DateTime.Now);
            rptDoc.SetParameterValue("@status", "True");
            rptDoc.SetParameterValue("@External_Client_Id", commonid);
            crViewer.ReportSource = rptDoc;
        }
    }
}
