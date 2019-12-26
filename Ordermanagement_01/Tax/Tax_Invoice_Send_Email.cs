using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net;
using System.Net.Mail;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Invoice_Send_Email : Form
    {
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        NetworkCredential NetworkCred;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int invoiceid, clientid, subprocessid, User_Id,  Client_Id, commonid;
        string invoiceno, invoicedt, invoicecmt, invoicemon, Invoice_Month_Name, From_Date, To_date; string Directory_Path, Path1, Attachment_Name, Email, Alternative_Email;
        public Tax_Invoice_Send_Email(int invoice_id,int client_id,int subprocess_id,int userid,string invoice_no,string invoice_dt,string invoice_cmt,string invoice_mon)
        {
            InitializeComponent();
            invoiceid = invoice_id;
            clientid = client_id;
            subprocessid = subprocess_id;
            User_Id = userid;
            invoiceno = invoice_no;
            invoicedt = invoice_dt;
            invoicecmt = invoice_cmt;
            invoicemon = invoice_mon;
            commonid = 0;

            //form_loader.Start_progres();
            Export_Monthly_Invoice_Report();
            Get_From_To_Date();
            Send_Html_Email_Body();
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
        public void Export_Monthly_Invoice_Report()
        {


            rptDoc = new Tax.Tax_Invoice_Reports.Tax_Invoice_Monthly_rpt();

            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", invoiceid);
            rptDoc.SetParameterValue("@Trans", "SELECT_Client_ID");
            rptDoc.SetParameterValue("@Client_Id", clientid);
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

            rptDoc.SetParameterValue("@Monthly_Invoice_Id", invoiceid, "Individual");



            ExportOptions CrExportOptions;
            FileInfo newFile = new FileInfo(@"\\192.168.12.33\Invoice-Reports\Tax_Invoice_Monthly.pdf");

            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();

            PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
            CrExportOptions = rptDoc.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            rptDoc.Export();
        }
        public void Get_From_To_Date()
        {

            Hashtable htdate = new Hashtable();
            DataTable dtdate = new DataTable();
            htdate.Add("@Trans", "GET_FROM_TO_DATE");
            htdate.Add("@Tax_MonthlyInvoice_Id", invoiceid);
            dtdate = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htdate);
            if (dtdate.Rows.Count > 0)
            {


                From_Date = dtdate.Rows[0]["Invoice_From_date"].ToString();
                To_date = dtdate.Rows[0]["Invoice_To_Date"].ToString();
                Invoice_Month_Name = dtdate.Rows[0]["Month_Name"].ToString();
            }
        }
        public string PopulateBody()
        {
            
            string body = string.Empty;

            
                    Directory_Path = @"\Monthly_Invoice_Email.htm";
               
            
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + Directory_Path))
            {

                body = reader.ReadToEnd();
            }
            body = body.Replace("{From_Date}", From_Date.ToString());
            body = body.Replace("{To_Date}", To_date.ToString());
            
            return body;
        }
        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("accounts@abstractshop.com");
               // mailMessage.From = new MailAddress("techteam@drnds.com");
                Path1 = @"\\192.168.12.33\Invoice-Reports\Tax_Invoice_Monthly.pdf";
                Attachment_Name = "Tax_MonthlyInvoice.pdf";
                 var maxsize = 15 * 1024 * 1000;
                var fileName = Path1;
                FileInfo fi = new FileInfo(fileName);
                var size = fi.Length;
                MemoryStream ms = new MemoryStream();
                MemoryStream ms1 = new MemoryStream();
                if (size <= maxsize)
                {
                    ms = new MemoryStream(File.ReadAllBytes(Path1));
                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));
                    Hashtable htdate = new Hashtable();
                    System.Data.DataTable dtdate = new System.Data.DataTable();
                    htdate.Add("@Trans", "SELECT");
                    htdate.Add("@Sub_Process_Id", subprocessid);
                    dtdate = dataaccess.ExecuteSP("Sp_Client_Mail", htdate);
                    if (dtdate.Rows.Count > 0)
                    {

                        Email = "Avilable";
                        Alternative_Email = "Avilable";
                        Client_Id = int.Parse(dtdate.Rows[0]["Client_Id"].ToString());

                    }
                    else
                    {

                        Email = "";
                        Alternative_Email = "";
                    }
                    if (Email != "")
                    {


                        for (int j = 0; j < dtdate.Rows.Count; j++)
                        {
                            mailMessage.To.Add(dtdate.Rows[j]["Email-ID"].ToString());
                            
                        }
                        //mailMessage.To.Add("techteam@drnds.com");
                        //mailMessage.CC.Add("techteam@drnds.com");
                        mailMessage.CC.Add("accounts@abstractshop.com");
                        string Subject = "Tax-Invoice - " + Invoice_Month_Name.ToString();
                        mailMessage.Subject = Subject.ToString();
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();

                        smtp.Host = "smtpout.secureserver.net";
                        NetworkCred = new NetworkCredential("accounts@abstractshop.com", "Penn@567");
                        //NetworkCredential NetworkCred = new NetworkCredential("techteam@drnds.com", "nop539");
                        smtp.UseDefaultCredentials = false;
                        //  smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
                        smtp.Timeout = (60 * 5 * 1000);
                        smtp.Credentials = NetworkCred;
                        // smtp.EnableSsl = true;
                        smtp.Port = 25;
                        //string userState = "test message1";
                        smtp.Send(mailMessage);
                        smtp.Dispose();
                        Update_Monthly_Invoice_Email_Status();
                    }
                    else
                    {

                        MessageBox.Show("Email is Not Added Kindly Check It");
                    }
                }
            }
            //accounts@abstractshop.com 

        }
        public void Update_Monthly_Invoice_Email_Status()
        {

            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new DataTable();
            htupdate.Add("@Trans", "UPDATE_TAX_INVOICE_EMAIL_STATUS");
            htupdate.Add("@Tax_MonthlyInvoice_Id", invoiceid);
            dtupdate = dataaccess.ExecuteSP("Sp_Tax_Monthly_Invoice_Entry", htupdate);
          

        }
        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {

                    mm.IsBodyHtml = true;
                    string body = this.PopulateBody();

                   // SendHtmlFormattedEmail("neworders@abstractshop.com", "Sample", body);


                     SendHtmlFormattedEmail("accounts@abstractshop.com", "Sample", body);



                    this.Close();






                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }

        }
       

        private void Tax_Invoice_Send_Email_Load(object sender, EventArgs e)
        {

        }
    }
}
