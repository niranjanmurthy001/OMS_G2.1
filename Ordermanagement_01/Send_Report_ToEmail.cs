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
using System.Net;
using System.Net.Mail;


namespace Ordermanagement_01
{
    public partial class Send_Report_ToEmail : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        System.Data.DataTable dt = new System.Data.DataTable();
        DataSet ds = new DataSet();
        Hashtable ht_Status = new Hashtable();
        System.Data.DataTable dt_Status = new System.Data.DataTable();
        string From_date, To_date;
        string createingpath;
        static System.Globalization.CultureInfo oldCI;
        Hashtable htto = new Hashtable();
        System.Data.DataTable dtto = new System.Data.DataTable();
        string From_Email;
        int Time_Interval;
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        string server = "192.168.12.33";
        string database = "TITLELOGY_NEW_OMS";
        string UserID = "sa";
        string password = "password1$";
        int Order_Id;
        int user_id;
        string Client_Order_no;
        int Order_Type;

        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        public Send_Report_ToEmail(int User_ID)
        {
            InitializeComponent();
            user_id = User_ID;
            Get_Day_OfThisMonth();
           
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
        public void Get_Day_OfThisMonth()
        {
            Hashtable httimezone = new Hashtable();
            System.Data.DataTable dttimezone = new System.Data.DataTable();

            
                    httimezone.Add("@Trans", "GET_EST_CURRENT_TIME");
                    dttimezone = dataaccess.ExecuteSP("Sp_Time_Zone", httimezone);
                    if (dttimezone.Rows.Count > 0)
                    {


                        To_date = dttimezone.Rows[0]["date"].ToString();

                    }

                  
            Hashtable htdate = new Hashtable();
            System.Data.DataTable dtdate = new System.Data.DataTable();
            htdate.Add("@Trans", "GET_DATE");
            dtdate = dataaccess.ExecuteSP("Sp_Get_Day_For_Report", htdate);
            if (dtdate.Rows.Count > 0)
            {

                From_date = dtdate.Rows[1][0].ToString();
            

            
            }
          

        }
        public void Load_Emails()

        {

            Hashtable htfrom = new Hashtable();
            System.Data.DataTable dtfrom = new System.Data.DataTable();
            htfrom.Add("@Trans", "GET_FROM_EMAIL");
            dtfrom = dataaccess.ExecuteSP("Sp_Automail", htfrom);
            if (dtfrom.Rows.Count > 0)
            {

                From_Email = dtfrom.Rows[0]["Sent_Email"].ToString();
                Time_Interval = int.Parse(dtfrom.Rows[0]["Email_Interval_Time"].ToString());


            }

            
            htto.Add("@Trans", "GET_TO_EMAIL");
            dtto = dataaccess.ExecuteSP("Sp_Automail", htto);
            if (dtto.Rows.Count > 0)
            {

              

            }



        }


        private void button1_Click(object sender, EventArgs e)
        {
            Export_Production_Report();

            Load_User_Production_Rep();
        }




        public void Export_Production_Report()
        {



            Get_Day_OfThisMonth();
           
                DateTime Fromdate = Convert.ToDateTime(From_date.ToString());
                DateTime Todate = Convert.ToDateTime(To_date.ToString());


                
                Hashtable ht_Status1 = new Hashtable();
                System.Data.DataTable dt_Status1 = new System.Data.DataTable();


                ht_Status1.Add("@Trans", "Order_Status_Report_All_ClientWise");
                
              
                ht_Status1.Add("@Fromdate", Fromdate);
                ht_Status1.Add("@Todate", Todate);
                ht_Status1.Add("@Log_In_Userid", user_id);
                
                dt_Status1 = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status1);




                Hashtable ht_Status = new Hashtable();
                System.Data.DataTable dt_Status = new System.Data.DataTable();






                ht_Status.Add("@Trans", "ALL_CLIENT_WISE_PRODUCTION_COUNT");
                
             
                ht_Status.Add("@Fromdate", Fromdate);
                ht_Status.Add("@Todate", Todate);
                ht_Status.Add("@Log_In_Userid", user_id);
                dt_Status = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Status);
                ds.Tables.Add(dt_Status);

                // ds.Tables.Add(dt_Status1);
                ds.Merge(dt_Status1);
               // ExportDataSetToExcel( ds);

                Exp();
                ds.Clear();
            


        }


        public void Load_User_Production_Rep()
   
        {

     
        rptDoc = new Reports.CrystalReport.CrystalReportUser_Prod();
        Logon_To_Crystal();
      DateTime Fromdate = Convert.ToDateTime(From_date.ToString());
                DateTime Todate = Convert.ToDateTime(To_date.ToString());

        rptDoc.SetParameterValue("@Trans", "All");
        rptDoc.SetParameterValue("@Order_Id", 0);
        rptDoc.SetParameterValue("@Client_Id", 0);
        rptDoc.SetParameterValue("@Subprocess_Id", 0);
        rptDoc.SetParameterValue("@Order_Progress_Id", 0);
        rptDoc.SetParameterValue("@Order_Status_Id", 0);
        rptDoc.SetParameterValue("@From_date", Fromdate);
        rptDoc.SetParameterValue("@To_date", Todate);
        rptDoc.SetParameterValue("@User_Id", 0);
        rptDoc.SetParameterValue("@Log_In_Userid",user_id);
        crViewer.ReportSource = rptDoc;
        Export_User_Production_Report();

    }
        public void Export_User_Production_Report()
        {
            DateTime Fromdate = Convert.ToDateTime(From_date.ToString());
            DateTime Todate = Convert.ToDateTime(To_date.ToString());

            Logon_To_Crystal();
          rptDoc.SetParameterValue("@Trans", "All");
        rptDoc.SetParameterValue("@Order_Id", 0);
        rptDoc.SetParameterValue("@Client_Id", 0);
        rptDoc.SetParameterValue("@Subprocess_Id", 0);
        rptDoc.SetParameterValue("@Order_Progress_Id", 0);
        rptDoc.SetParameterValue("@Order_Status_Id", 0);
        rptDoc.SetParameterValue("@From_date", Fromdate);
        rptDoc.SetParameterValue("@To_date", Todate);
        rptDoc.SetParameterValue("@User_Id", 0);
        rptDoc.SetParameterValue("@Log_In_Userid", user_id);
            ExportOptions CrExportOptions;
         
            FileInfo newFile = new FileInfo(@"\\192.168.12.33\oms-reports\User_ProductionRep.xls");

           

            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
            CrExportOptions = rptDoc.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.ExcelWorkbook;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            rptDoc.Export();





        }


        private void Exp()
        {
            FileInfo newFile = new FileInfo(@"\\192.168.12.33\oms-reports\ProductionRep.xlsx");

       
         

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
          //  ExcelApp.Visible = true;
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
               // xlWorksheet.Cells[0, 0].Style.IsTextWrapped = true;
                ExcelApp.Columns.AutoFit();
            }
            ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
            //ExcelApp.Visible = true;
           // xlWorkbook.SaveCopyAs(@"C:\Niranjan.xlsx");
           // xlWorkbook.SaveCopyAs(@"D:\Export\ProductionRep.xlsx");
            xlWorkbook.SaveCopyAs(newFile);
           
            ExcelApp.DisplayAlerts = false;
            xlWorkbook.Close();
            ExcelApp.Quit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            Send_Production_Rport();
            Send_User_Production_Rport();
        }


        public void Send_Production_Rport()
        { 

        using (MailMessage mm = new MailMessage())
            {
                try


                {
                  
                    mm.From = new MailAddress(From_Email.ToString());

                    for (int i = 0; i < dtto.Rows.Count; i++)
                    {

                        mm.To.Add(dtto.Rows[i]["To_Email_Id"].ToString());
                    }

                    txt_Subject.Text = "All Client Production  Report - " + "From -" + From_date.ToString() + " To - " + To_date.ToString() + "";
                    mm.Subject = "All Client Production  Report - " + "From -" + From_date.ToString() + " To - " + To_date.ToString() + "";

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Subject: " + txt_Subject.ToString() + "" + Environment.NewLine);
                  


                    string Path1 = @"\\192.168.12.33\oms-reports\ProductionRep.xlsx";
                   
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));
                    mm.Attachments.Add(new System.Net.Mail.Attachment(ms, "ProductionRep.xlsx"));
                   
                   
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                  
                    smtp.Host = "smtpout.secureserver.net";

                    NetworkCredential NetworkCred = new NetworkCredential("neworders@drnds.com", "bbt456");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                  
                    smtp.Port = 80;
                    smtp.Send(mm);
                    
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;
                }
            }
        }

        public void Send_User_Production_Rport()
        {

            using (MailMessage mm = new MailMessage())
            {
                try
                {

                    mm.From = new MailAddress(From_Email.ToString());

                    for (int i = 0; i < dtto.Rows.Count; i++)
                    {

                        mm.To.Add(dtto.Rows[i]["To_Email_Id"].ToString());
                    }

                    txt_Subject.Text = "All Users Production Report - " + "From -" + From_date.ToString() + " To - " + To_date.ToString() + "";
                    mm.Subject = "All Users Production Report -  " + "From -" + From_date.ToString() + " To - " + To_date.ToString() + "";

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Subject: " + "Auto Email" + "" + Environment.NewLine);


                    //String str = sb.ToString();
                    //string Message_Body = str.ToString();
                    //mm.Body = Message_Body;
                    string Path1 = @"\\192.168.12.33\oms-reports\User_ProductionRep.xls";
                    
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));
                    mm.Attachments.Add(new System.Net.Mail.Attachment(ms, "User_ProductionRep.xls"));
                    

                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    // smtp.Host = "smtp.gmail.com";
                    smtp.Host = "smtpout.secureserver.net";
                    // smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("neworders@drnds.com", "123new.com");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    //smtp.Port = 587;
                    smtp.Port = 80;
                    smtp.Send(mm);
                    //MessageBox.Show("Email Sent");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;
                }
            }
        }


        private void Send_Report_ToEmail_Load(object sender, EventArgs e)
        {
            Get_Day_OfThisMonth();
            Load_Emails();
            button1_Click(sender, e);
            button2_Click(sender, e);
            Send_Report_ToEmail email = new Send_Report_ToEmail(user_id);
            email.Close();
           //this.Close();
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Send_Production_Rport();
            button1_Click( sender,  e);
            button2_Click( sender,  e);
            Send_Report_ToEmail email = new Send_Report_ToEmail(user_id);
            email.Close();



        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_Attachment.Text = dlg.FileName.ToString();
            }
        }

        }

    }

