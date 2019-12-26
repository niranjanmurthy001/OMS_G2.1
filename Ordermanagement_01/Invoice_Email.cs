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

namespace Ordermanagement_01.InvoiceRep
{
    public partial class Invoice_Email : Form
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
        int Order_Id;
        string[] FName;
        string Document_Name;
        string Client_Order_no ;
        int Order_Type;
        int abstarctor_id,User_Id;
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
        public Invoice_Email( string ORDER_NUMBER, int USER_ID, int ORDER_ID, int INVOICE_ID, string INVOICE_STATUS, string FORM, int SUB_PROCESS_ID)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            User_Id = USER_ID;
          
            Forms = FORM.ToString();
            Order_Number = ORDER_NUMBER.ToString();
          
            Invoice_Id = INVOICE_ID;
            Invoice_Status = INVOICE_STATUS;
            Sub_Process_ID=SUB_PROCESS_ID;
            if (Forms == "Order_Invoice")
            {
                Export_Report();
                Merge_Document();
              
            }
            else if (Forms == "Monthly_Invoice")
            {

                Export_Monthly_Invoice_Report();
                Get_From_To_Date();
                Send_Html_Email_Body();
            }

            if (Forms == "Order_Invoice")
            {
                Hashtable htsearch = new Hashtable();
                DataTable dtsearch = new DataTable();
                htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
                htsearch.Add("@Order_ID", Order_Id);
                dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
                if (dtsearch.Rows.Count > 0)
                {
                    Send_Html_Email_Body();
                }
                else
                {
                    MessageBox.Show("SearchPackage is Not Added Please Check it");

                }
            }

        }

        public void Merge_Document()
        {
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_ID", Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
            if (dtsearch.Rows.Count > 0)
            {
                P2 = dtsearch.Rows[0]["Document_Path"].ToString();
            }
            Hashtable htinvoice = new Hashtable();
            DataTable dtinvoice = new DataTable();
            htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
            htinvoice.Add("@Order_ID", Order_Id);
            dtinvoice = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htinvoice);
            if (dtinvoice.Rows.Count > 0)
            {
                P1 = dtinvoice.Rows[0]["Document_Path"].ToString();
            }
            DataSet ds = new DataSet();
            ds.Clear();
            if (Invoice_Status == "True")
            {
                //ds.Tables.Add(dtinvoice);
                //ds.Merge(dtsearch);
            }
            else if (Invoice_Status == "False")
            {

                ////ds.Tables.Add(dtsearch);
            }


            if (Invoice_Status == "True")
            {
                if (dtsearch.Rows.Count > 0)
                {



                        //Define a new output document and its size, type

                        Package = "InvoiceAndSearch";
                        Merge_Invoice_Search();
                       
                    

                }
                else
                {

                    MessageBox.Show("SearchPackage is Not Added Please Check it");
                    
                }
            }
            else if (Invoice_Status == "False")
            {
                if (dtsearch.Rows.Count > 0)
                {

                    Package = "Search";
                    Merge_Invoice_Search();
                  
                }
                else
                {

                    MessageBox.Show("Search package is not uploaded check it");
                }

            }
           

        }
        public void Merge_Invoice_Search()
        {


            //lstFiles[0] = @"C:/Users/DRNASM0001/Desktop/15-59989-Search Package.pdf";
            //lstFiles[1] = @"C:/Users/DRNASM0001/Desktop/Invoice.pdf";
            if (Invoice_Status == "True" && Package == "InvoiceAndSearch")
            {
                Index = 3;

            }
            else if (Invoice_Status == "False" && Package == "Search")
            {

                Index = 2;
            }
            string[] lstFiles = new string[Index];
            if (Invoice_Status == "True" && Package == "InvoiceAndSearch")
            {

                lstFiles[0] = P1;

                lstFiles[1] = P2;
            }
            else if (Invoice_Status == "False" && Package == "Search")
            {

                lstFiles[0] = P2;


            }
            //lstFiles[2] = @"C:/pdf/3.pdf";

            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            string outputPdfPath = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf";


            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

            //Open the output file
            sourceDocument.Open();

            try
            {
                //Loop through the files list
                for (int f = 0; f < lstFiles.Length - 1; f++)
                {
                    int pages = get_pageCcount(lstFiles[f]);

                    reader = new PdfReader(lstFiles[f]);
                    //Add pages of current file
                    for (int i = 1; i <= pages; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }

                    reader.Close();
                }
                //At the end save the output file
                sourceDocument.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Get_From_To_Date()
        { 
        
            Hashtable htdate = new Hashtable();
            DataTable dtdate = new DataTable();
            htdate.Add("@Trans", "GET_FROM_TO_DATE");
            htdate.Add("@MonthlyInvoice_Id", Invoice_Id);
            dtdate = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htdate);
            if (dtdate.Rows.Count > 0)
            {


                From_Date = dtdate.Rows[0]["Invoice_From_date"].ToString();
                To_date = dtdate.Rows[0]["Invoice_To_Date"].ToString();
                Invoice_Month_Name = dtdate.Rows[0]["Month_Name"].ToString();
            }
        }

        private int get_pageCcount(string file)
        {
            PdfReader pdfReader = new PdfReader(File.OpenRead(file));
            int numberOfPages = pdfReader.NumberOfPages;
            //return matches.Count;

            return numberOfPages;
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

        public void Export_Report()
        {

            rptDoc = new InvoiceRep.InvReport.Invoice_Report();

            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Order_ID", Order_Id);
            ExportOptions CrExportOptions;
            string Invoice_Order_Number =Order_Number.ToString();
            string Source = @"\\192.168.12.33\Invoice-Reports\Invoice.pdf";

            string File_Name = ""+Order_Number+".pdf";
            string dest_path1 = @"\\192.168.12.33\Invoice-Reports\" + Invoice_Order_Number + @"\" + File_Name;
            DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
            de.Username = "administrator";
            de.Password = "password1$";
            Directory.CreateDirectory(@"\\192.168.12.33\Invoice-Reports\" + Invoice_Order_Number);
         
            File.Copy(Source, dest_path1, true);
         

            Hashtable htpath = new Hashtable();
            DataTable dtpath = new DataTable();

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id",Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htcheck);
            int check;
            if (dtcheck.Rows.Count > 0)
            {
                 check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                check=0;
            }
            if (check == 0)
            {
                htpath.Add("@Trans", "INSERT");
                htpath.Add("@Order_Id", Order_Id);
                htpath.Add("@Invoice_Id", Invoice_Id);
                htpath.Add("@Document_Path", dest_path1);
                dtpath = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htpath);



            }

            Hashtable htgetpath = new Hashtable();
            DataTable dtgetpath = new DataTable();
            htgetpath.Add("@Trans", "GET_PATH");
            htgetpath.Add("@Order_Id", Order_Id);
            dtgetpath = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htgetpath);
          
            if (dtgetpath.Rows.Count > 0)
            {
                View_File_Path = dtgetpath.Rows[0]["Document_Path"].ToString();
            }
            FileInfo newFile = new FileInfo(View_File_Path);

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

        public void Export_Monthly_Invoice_Report()
        {


            rptDoc = new InvoiceRep.InvReport.Invoice_Monthly_Report();

            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", Invoice_Id);
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", Invoice_Id, "Individual");
            
          
            ExportOptions CrExportOptions;
            FileInfo newFile = new FileInfo(@"\\192.168.12.33\Invoice-Reports\InvoiceMonthly.pdf");
        
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



               

                 
                    //LinkedResource imagelink = new LinkedResource(Environment.CurrentDirectory + @"\Drn_Email_Logo.png", "image/png");
                    //imagelink.ContentId = "imageId";
                    //imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                    mm.IsBodyHtml = true;
                    string body = this.PopulateBody();
                if (Forms == "Order_Invoice")
            {
                SendHtmlFormattedEmail("neworders@abstractshop.com", "Sample", body);
            }
            else if (Forms == "Monthly_Invoice")
            {

                SendHtmlFormattedEmail("accounts@abstractshop.com", "Sample", body);
            }
                    

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
           
            if (Forms == "Order_Invoice")
            {
                Directory_Path = @"\Invoice_Email.htm";
            }
            else if (Forms == "Monthly_Invoice")
            {

                Directory_Path = @"\Monthly_Invoice_Email.htm";
            }
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + Directory_Path))
            {

                body = reader.ReadToEnd();
            }

            //Hashtable htorder = new Hashtable();
            //DataTable dtorder = new DataTable();
            //htorder.Add("@Trans", "GET_INVOICE_ORDER_DETAILS_FOR_EMAIL");
            //htorder.Add("@Order_ID", Order_Id);
            //dtorder = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htorder);
            //if (dtorder.Rows.Count > 0)
            //{


            //}
            if (Forms == "Order_Invoice")
            {
                if (Invoice_Status == "True")
                {
                    body = body.Replace("{Text}", "Please find the attached search report along with invoice copy for your review.");

                }
                else if (Invoice_Status == "False")
                {
                    body = body.Replace("{Text}", "Please find the attached search report.");

                }
            }
            else if (Forms == "Monthly_Invoice")
            {
                body = body.Replace("{From_Date}",From_Date.ToString());
                body = body.Replace("{To_Date}",  To_date.ToString());

            }
            //body = body.Replace("{OrderType}", dtorder.Rows[0]["Order_Type"].ToString());
            //body = body.Replace("{OwnerName}", dtorder.Rows[0]["Borrower_Name"].ToString());
            //body = body.Replace("{Property_Address}", dtorder.Rows[0]["Address"].ToString());
            //body = body.Replace("{County}", dtorder.Rows[0]["County"].ToString());
           
            return body;
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
               

                if (Forms == "Order_Invoice")
                {
                    mailMessage.From = new MailAddress("neworders@abstractshop.com");
                    Path1 = @"\\192.168.12.33\Invoice-Reports\Invoicemerge.pdf";
                    Attachment_Name = Order_Number.ToString() + ".pdf";
                }
                else if (Forms == "Monthly_Invoice")
                {
                    mailMessage.From = new MailAddress("accounts@abstractshop.com");
                    Path1 = @"\\192.168.12.33\Invoice-Reports\InvoiceMonthly.pdf";
                    Attachment_Name = "MonthlyInvoice.pdf";
                }

                var maxsize = 15 * 1024 * 1000;
                var fileName = Path1;
                FileInfo fi = new FileInfo(fileName);
                var size = fi.Length;
                if (size <= maxsize)
                {
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));



                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));

                    Hashtable htdate = new Hashtable();
                    System.Data.DataTable dtdate = new System.Data.DataTable();
                    htdate.Add("@Trans", "SELECT");
                    htdate.Add("@Sub_Process_Id", Sub_Process_ID);
                    dtdate = dataaccess.ExecuteSP("Sp_Client_Mail", htdate);
                    if (dtdate.Rows.Count > 0)
                    {

                        Email = "Avilable";
                        Alternative_Email = "Avilable";


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

                        if (Forms == "Monthly_Invoice")
                        {

                            mailMessage.CC.Add("accounts@abstractshop.com");
                           
                        }
                        else
                        {

                            mailMessage.CC.Add("neworders@abstractshop.com");
                           
                        }

                        if (Forms == "Order_Invoice")
                        {
                            Hashtable htorder = new Hashtable();
                            DataTable dtorder = new DataTable();
                            htorder.Add("@Trans", "GET_INVOICE_ORDER_DETAILS_FOR_EMAIL");
                            htorder.Add("@Order_ID", Order_Id);
                            dtorder = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htorder);
                            if (dtorder.Rows.Count > 0)
                            {


                            }

                            string Title = dtorder.Rows[0]["Order_Type"].ToString();
                            string Subject = "" + Order_Number + "-" + Title.ToString();
                            mailMessage.Subject = Subject.ToString();

                            StringBuilder sb = new StringBuilder();
                            sb.Append("Subject: " + Subject.ToString() + "" + Environment.NewLine);

                        }
                        else if (Forms == "Monthly_Invoice")
                        {
                            string Subject = "Invoice - " + Invoice_Month_Name.ToString(); 
                            mailMessage.Subject = Subject.ToString();

                        }


                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;



                        SmtpClient smtp = new SmtpClient();

                        smtp.Host = "smtpout.secureserver.net";
                       
                        if (Forms == "Order_Invoice")
                        {
                             NetworkCred = new NetworkCredential("neworders@abstractshop.com", "123abs");
                        }
                        else if (Forms == "Monthly_Invoice")
                        {

                            NetworkCred = new NetworkCredential("accounts@abstractshop.com", "Steve@1234");
                        }
                        smtp.UseDefaultCredentials = false;
                        // smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
                        smtp.Timeout = (60 * 5 * 1000);
                        smtp.Credentials = NetworkCred;
                        // smtp.EnableSsl = true;
                        smtp.Port = 80;
                        //string userState = "test message1";
                        smtp.Send(mailMessage);
                        smtp.Dispose();

                        if (Forms == "Order_Invoice")
                        {
                            Update_Invoice_Email_Status();
                        }
                        else if (Forms == "Monthly_Invoice")
                        {
                            Update_Monthly_Invoice_Email_Status();
                        }
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
            htupdate.Add("@Order_ID", Order_Id);
            dtupdate = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htupdate);

        }
        public void Update_Monthly_Invoice_Email_Status()
        {

            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new DataTable();
            htupdate.Add("@Trans", "UPDATE_EMAIL_STATUS");
            htupdate.Add("@MonthlyInvoice_Id",Invoice_Id);
            dtupdate = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htupdate);

        }
     
    }
}
