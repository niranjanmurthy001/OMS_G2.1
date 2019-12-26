using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using System.Collections;
using System.IO;
using RTF;
using System.Net.Mime;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.DirectoryServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Payment_Mail : Form
    {
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id;
        string[] FName;
        string Document_Name;
       
        string Path1;
        string Attachment_Name;
        string Directory_Path;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        string Order_Number;
        Tables CrTables;
        string Email, Alternative_Email;
        int Invoice_Id;
        string View_File_Path;
        string Invoice_Status;
        DialogResult dialogResult;
        string Forms;
        string Package = "";
        string P1, P2;
        int Index;
        int Sub_Process_ID;
        NetworkCredential NetworkCred;
        string From_Date, To_date;
        string Invoice_Month_Name;
        int Monthly_Invoice_Id;
        string Month, Year, Emial_Contents,Emial_Add;
        
        public Abstractor_Payment_Mail(int MONTHLY_INVOICE_ID,string MONTH,string YEAR,string EMAIL_CONTENTS,string EMAIL_ADD)
        {
            InitializeComponent();
            Monthly_Invoice_Id = MONTHLY_INVOICE_ID;
            Month = MONTH;
            Year = YEAR;
            Emial_Contents = EMAIL_CONTENTS;
            Emial_Add = EMAIL_ADD;
                Export_Report();

                Send_Html_Email_Body();
                this.Close();
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
           

        }

        public void Export_Report()
        {

            rptDoc = new Abstractor.Abstractor_Reports.Abstractor_Payment();
            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Abstractor_Monthly_Invoice_ID", Monthly_Invoice_Id);
         
            ExportOptions CrExportOptions;
            FileInfo newFile = new FileInfo(@"\\192.168.12.33\ABSTRACTOR FILES\ABSTRACTOR MONTHLY PAYMENT\Abstractor_Monthly_Payement.pdf");

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

      

        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {



                    mm.IsBodyHtml = true;
                    string body = this.PopulateBody();

                    SendHtmlFormattedEmail("apinvoice@drnds.com", "Sample", body);
                    
                 
                    this.Close();






                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }

        }

        public string PopulateBody()
        {
            string body = string.Empty;


            Directory_Path = @"\\192.168.12.33\Oms Email Templates\Abstractor_Payment_Email.htm";




                using (StreamReader reader = new StreamReader(Directory_Path))
                {

                    body = reader.ReadToEnd();



                    body = body.Replace("{Month}",""+Month+" - "+Year+"");
                  
                        body = body.Replace("{Text}",Emial_Contents.ToString());
                   

                }

            return body;
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {



                mailMessage.From = new MailAddress("apinvoice@drnds.com");
                Path1 = @"\\192.168.12.33\ABSTRACTOR FILES\ABSTRACTOR MONTHLY PAYMENT\Abstractor_Monthly_Payement.pdf";
                Attachment_Name = ""+Month+"-"+Year+"Payment.pdf";
               

                var maxsize = 15 * 1024 * 1000;
                var fileName = Path1;
                FileInfo fi = new FileInfo(fileName);
                var size = fi.Length;
                if (size <= maxsize)
                {
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));



                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));

                  

                    if (Emial_Add != "")
                    {


   
                        mailMessage.To.Add(Emial_Add.ToString());


                        mailMessage.CC.Add("apinvoice@drnds.com");

                        
                      
                        
                            string Subject = "Payment Statement - "+Month+"-"+Year+"";
                            mailMessage.Subject = Subject.ToString();

                       
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;



                        SmtpClient smtp = new SmtpClient();

                        smtp.Host = "smtpout.secureserver.net";



                        NetworkCred = new NetworkCredential("apinvoice@drnds.com", "AecXmC9T07DcP$");
                        
                        smtp.UseDefaultCredentials = true;
                    
                        smtp.Timeout = (60 * 5 * 1000);
                        smtp.Credentials = NetworkCred;
                        // smtp.EnableSsl = true;
                        smtp.Port = 25;
                        //string userState = "test message1";
                        smtp.Send(mailMessage);
                        smtp.Dispose();

                       
                            Update_Invoice_Email_Status();
                        
                        
                    }
                    else
                    {

                        MessageBox.Show("Email is Not Added Kindly Check It");
                    }
                }
                else
                {

                    MessageBox.Show("Attachment Size should less than 10 mb ");
                }
            }

        }

        public void Update_Invoice_Email_Status()
        {

            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new DataTable();
            htupdate.Add("@Trans", "UPDATE_EMAIL_STATUS");
            htupdate.Add("@Abs_Monthl_Invoice_Id", Monthly_Invoice_Id);
            dtupdate = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htupdate);

        }
    }
}
