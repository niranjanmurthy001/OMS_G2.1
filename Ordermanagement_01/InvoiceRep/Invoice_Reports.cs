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
using System.Drawing.Drawing2D;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;

namespace Ordermanagement_01.InvoiceRep
{
    public partial class Invoice_Reports : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();

        InfiniteProgressBar.frmProgress form = new InfiniteProgressBar.frmProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        Hashtable ht = new Hashtable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtuserexport = new System.Data.DataTable();

        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        string server = "192.168.12.33";
        string database = "TITLELOGY_NEW_OMS";
        string UserID = "sa";
        string password = "password1$";
        int Order_Id;

        string Client_Order_no;
        int Order_Type;
        int User_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        public Invoice_Reports()
        {
            InitializeComponent();
        }

        private void Invoice_Reports_Load(object sender, EventArgs e)
        {
            AddParent();
            dbc.BindClientName_rpt(ddl_Client_Name);
            txt_Fromdate.Value = DateTime.Now;
            txt_Todate.Value = DateTime.Now;
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
        private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Client_Name.SelectedIndex > 0)
            {

                int clientid = int.Parse(ddl_Client_Name.SelectedValue.ToString());
                dbc.BindSubProcessName_rpt(ddl_Subprocess, clientid);
                ddl_Subprocess.Focus();
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


            tvwRightSide.Nodes[0].Nodes.Add("Client Wise Invoice Report");
           
        }

        private void btn_Report_Click(object sender, EventArgs e)
        {

        }
    }
}
