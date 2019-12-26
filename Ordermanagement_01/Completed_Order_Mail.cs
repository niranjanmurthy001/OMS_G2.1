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
using System.DirectoryServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

namespace Ordermanagement_01
{
    public partial class Completed_Order_Mail : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        NetworkCredential NetworkCred;
        string Directory_Path;
        int Client_Id,Sub_Client_Id, Order_Id, Document_Id, user_id, Email_Sent_Count;
        string Order_Number, Email, Subject;
        string Email_Order_Place_Id;
        string Email_Order_Placer_Name;
        string Email_Order_Placer_Password;
        int External_Client_Id;
        string Package = "";
        string[] FName;
        string P1, P2;
        string Inv_Status;
        int Index;
        int External_Clinet_Id, External_Sub_client_Id, External_Order_Id;
        int Inhouse_Client_Id, Inhosue_Sub_Client_id;
        string External_Client_Order_Number;
        string File_size;
        int External_Vendor_Id;
        string Search_Path, Report_Path,Final_Path,Invoice_Path;
        int Order_Type_Id;
        public Completed_Order_Mail(int CLIENT_ID,string ORDER_NUMBER,int ORDER_ID,int USER_ID,int SUB_CLIENT_ID,int ORDER_TYPE_ID)
        {
            InitializeComponent();
            Client_Id = CLIENT_ID;
            Order_Number = ORDER_NUMBER;
            Order_Id = ORDER_ID;
            user_id = USER_ID;
            Sub_Client_Id = SUB_CLIENT_ID;
            Order_Type_Id = ORDER_TYPE_ID;

            Load_External_Client_Order_Client_Details();

            // this is for Titlelogy Vendor Db title Solutions
            if (Client_Id == 33)
            {
                if (Sub_Client_Id == 300)// This is for Peak Title
                {






                }
                else if (Sub_Client_Id != 361)
                {

                    Creat_Final_Title_Search_package();
                }
            }
         
                Get_Vendor_Detials();
                Send_Html_Email_Body();
            
            this.Close();
        }


        private void Load_External_Client_Order_Client_Details()
        {

            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            ht.Add("@Trans", "GET_EXTERNAL_CLIENT_SUB_CLIENT_ORDER_ID");
            ht.Add("@Order_Id", Order_Id);
            dt = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", ht);
            if (dt.Rows.Count > 0)
            {



                External_Clinet_Id = int.Parse(dt.Rows[0]["Clinet_Id"].ToString());
                External_Sub_client_Id = int.Parse(dt.Rows[0]["Sub_Client_Id"].ToString());
                External_Order_Id = int.Parse(dt.Rows[0]["External_Order_Id"].ToString());
                External_Client_Order_Number = dt.Rows[0]["Order_Number"].ToString();
                Inhouse_Client_Id = int.Parse(dt.Rows[0]["Client_Id"].ToString());
                Inhosue_Sub_Client_id = int.Parse(dt.Rows[0]["Subprocess_Id"].ToString());
            }
            else
            {
                External_Clinet_Id = 0;
            }
           
        }

        private void Get_Vendor_Detials()
        {

            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            ht.Add("@Trans", "GET_VENDOR_ID");
            ht.Add("@Order_Id", External_Order_Id);
            dt = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", ht);
            if (dt.Rows.Count > 0)
            {

                External_Vendor_Id = int.Parse(dt.Rows[0]["Assigned_To_Vendor_Id"].ToString());

            }
        }
        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {



                    mm.IsBodyHtml = true;
                    string body = this.PopulateBody();

                    SendHtmlFormattedEmail("novare@drnds.com", "Sample", body);
                 

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


            Hashtable ht_Doc = new Hashtable();
            DataTable dt_Doc = new DataTable();
            ht_Doc.Add("@Trans", "GET_EXTERNAL_DOCUMENT_ID");
            ht_Doc.Add("@Order_Id", External_Order_Id);
            dt_Doc = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", ht_Doc);
            if (dt_Doc.Rows.Count > 0)
            {

                Document_Id = int.Parse(dt_Doc.Rows[0]["Order_Document_Id"].ToString());
            }

            if (Sub_Client_Id == 300 || Sub_Client_Id == 286)
            {
                Directory_Path = @"\\192.168.12.33\Oms Email Templates\Completed_Orders_Db.htm";
            }
            else
            {
                Directory_Path = @"\\192.168.12.33\Oms Email Templates\Completed_Orders.htm";
            }
            
            using (StreamReader reader = new StreamReader(Directory_Path))
            {

                body = reader.ReadToEnd();

              
            }

            if (Sub_Client_Id == 300 || Sub_Client_Id == 286)
            {

                body = body.Replace("{Order_Num}", Order_Number.ToString());

            }
            else
            {
                if (External_Vendor_Id == 1)
                {
                    body = body.Replace("{Team}", "DRN");
                }
                else if (External_Vendor_Id == 2)
                {

                    body = body.Replace("{Team}", "Titlelogy");
                }

                body = body.Replace("{Url}", "https://titlelogy.com/Orders/Completed_Order.aspx?Document_Id=" + Document_Id.ToString() + "&Order_Number=" + Order_Number.ToString() + "");

            }

           
       



            return body;
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {


                Hashtable htdate = new Hashtable();
                System.Data.DataTable dtdate = new System.Data.DataTable();
                htdate.Add("@Trans", "SELECT");
                htdate.Add("@Client_Id", External_Clinet_Id);
                dtdate = dataaccess.ExecuteSP("Sp_External_Clients", htdate);
                if (dtdate.Rows.Count > 0)
                {

                    Email = dtdate.Rows[0]["Company_Email"].ToString();
                    External_Clinet_Id = int.Parse(dtdate.Rows[0]["Client_Id"].ToString());

                }
                else
                {

                    Email = "";

                }


                if (Email != "")
                {
                    
               Hashtable htcount = new Hashtable();
               DataTable dtcount = new DataTable();
               htcount.Add("@Trans", "CHECK_EMAIL_SENT");
               htcount.Add("@Order_Id", Order_Id);
               dtcount = dataaccess.ExecuteSP("Sp_Order_Email_Notification", htcount);

               if (dtcount.Rows.Count > 0)
               {
                   Email_Sent_Count = int.Parse(dtcount.Rows[0]["count"].ToString());

               }
               else {
                   Email_Sent_Count = 0;
               }

               if (Email_Sent_Count == 0)
               {
                   Hashtable htinsrt = new Hashtable();
                   DataTable dtinsert = new DataTable();


                   htinsrt.Add("@Trans", "INSERT");
                   htinsrt.Add("@Order_Id", Order_Id);
                   htinsrt.Add("@Sent_By", user_id);
                   dtinsert = dataaccess.ExecuteSP("Sp_Order_Email_Notification", htinsrt);
               }
               else if( Email_Sent_Count>0)
               {
                   Hashtable htinsrt = new Hashtable();
                   DataTable dtinsert = new DataTable();


                   htinsrt.Add("@Trans", "UPDATE_FALSE");
                   htinsrt.Add("@Order_Id", Order_Id);
                   htinsrt.Add("@Sent_By", user_id);
                   dtinsert = dataaccess.ExecuteSP("Sp_Order_Email_Notification", htinsrt);

               }


                   MailAddress ma;
                   // MailAddress ma = new MailAddress("niranjanmurthy@drnds.com", "niranjan");

                    Hashtable htget_Order_Placeemail = new Hashtable();

                    DataTable dt_get_Order_PalceEmail = new DataTable();
                    htget_Order_Placeemail.Add("@Trans", "GET_EMAIL_FOR_ORDER_PLACE");
                    htget_Order_Placeemail.Add("@Client_Id", External_Clinet_Id);
                    dt_get_Order_PalceEmail = dataaccess.ExecuteSP("Sp_External_Client_Send_Recive_Email", htget_Order_Placeemail);

                    if (dt_get_Order_PalceEmail.Rows.Count > 0)
                    {
                        Email_Order_Place_Id = dt_get_Order_PalceEmail.Rows[0]["Email_Id"].ToString();
                        Email_Order_Placer_Name = dt_get_Order_PalceEmail.Rows[0]["Reciver_Name"].ToString();
                        Email_Order_Placer_Password = dt_get_Order_PalceEmail.Rows[0]["Password"].ToString();
                        ma = new MailAddress(Email_Order_Place_Id, Email_Order_Placer_Name);
                        mailMessage.From = ma;
                       
                    }


                    Hashtable htget_CompletedOrder_email = new Hashtable();

                    DataTable dt_get_CompletedOrder_Email = new DataTable();
                    htget_CompletedOrder_email.Add("@Trans", "GET_EMAIL_FOR_SENDING_COMPLETED_ORDER");
                    htget_CompletedOrder_email.Add("@Client_Id", External_Clinet_Id);
                    dt_get_CompletedOrder_Email = dataaccess.ExecuteSP("Sp_External_Client_Send_Recive_Email", htget_CompletedOrder_email);

                    if (dt_get_CompletedOrder_Email.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt_get_CompletedOrder_Email.Rows.Count; i++)
                        {

                            mailMessage.To.Add(dt_get_CompletedOrder_Email.Rows[i]["Email_Id"].ToString());


                        }
                    }

                    if (External_Vendor_Id == 1)
                    {

                        mailMessage.CC.Add("info@drnds.com");
                    }
                  
                    mailMessage.CC.Add(Email_Order_Place_Id);

                    if (Client_Id == 33)// this is for Db Title Vendor Clients
                    {

                        mailMessage.CC.Add("titleabstracting@dbtitlesolutions.com");
                        mailMessage.CC.Add("info@dbtitlesolutions.com");

                        // mailMessage.CC.Add("techteam@drnds.com");
                    }


                    // This is for Adding Attachment to Email itself -->for Clinet 33 / subclient 300

                    if (Client_Id == 33)
                    {
                        if (Sub_Client_Id == 300)
                        {

                            // Get the Serarch Package

                            Hashtable htget_Serach_Package = new Hashtable();
                            DataTable dtget_Search_Package = new DataTable();

                            htget_Serach_Package.Add("@Trans", "GET_DOCUMENT_PATH_BY_ID");
                            htget_Serach_Package.Add("@Order_Id", External_Order_Id);
                            htget_Serach_Package.Add("@Document_Type_Id", 1);
                            dtget_Search_Package = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htget_Serach_Package);
                            if (dtget_Search_Package.Rows.Count > 0)
                            {

                                Search_Path = dtget_Search_Package.Rows[0]["Document_Path"].ToString();
                            }
                            else
                            {

                                Search_Path = "";
                            }

                            Hashtable htget_Report_Package = new Hashtable();
                            DataTable dtget_Report_Package = new DataTable();

                            htget_Report_Package.Add("@Trans", "GET_DOCUMENT_PATH_BY_ID");
                            htget_Report_Package.Add("@Order_Id", External_Order_Id);
                            htget_Report_Package.Add("@Document_Type_Id", 10);
                            dtget_Report_Package = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htget_Report_Package);

                            if (dtget_Report_Package.Rows.Count > 0)
                            {

                                Report_Path = dtget_Report_Package.Rows[0]["Document_Path"].ToString();
                            }
                            else
                            {

                                Report_Path = "";
                            }





                            string Path1 = Search_Path;

                            string path2 = Report_Path;


                            

                          



                                Hashtable htget_Invoice_file = new Hashtable();
                                DataTable dtget_Invoice_file = new DataTable();

                                htget_Invoice_file.Add("@Trans", "GET_DOCUMENT_PATH_BY_ID");
                                htget_Invoice_file.Add("@Order_Id", External_Order_Id);
                                htget_Invoice_file.Add("@Document_Type_Id", 12);
                                dtget_Invoice_file = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htget_Invoice_file);

                                if (dtget_Invoice_file.Rows.Count > 0)
                                {

                                    Invoice_Path = dtget_Invoice_file.Rows[0]["Document_Path"].ToString();
                                }
                                else
                                {

                                    Invoice_Path = "";
                                }


                                string path3 = Invoice_Path;


                                if (Path1 != "" && path2 != "" && path3!="")
                                {

                                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));
                                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Path.GetFileName(Path1)));
                                    MemoryStream ms1 = new MemoryStream(File.ReadAllBytes(path2));
                                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms1, Path.GetFileName(path2)));
                                    MemoryStream ms2 = new MemoryStream(File.ReadAllBytes(path3));
                                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms2, Path.GetFileName(path3)));

                                }


                            
                            else
                            {

                                if (Path1 != "" && path2 != "")
                                {

                                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));
                                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Path.GetFileName(Path1)));
                                    MemoryStream ms1 = new MemoryStream(File.ReadAllBytes(path2));
                                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms1, Path.GetFileName(path2)));

                                }
                            }


                        }
                        else 
                        {

                            // Get the Serarch Package

                            Hashtable htget_Final_Package = new Hashtable();
                            DataTable dtget_Final_Package = new DataTable();

                            htget_Final_Package.Add("@Trans", "GET_DOCUMENT_PATH_BY_ID");
                            htget_Final_Package.Add("@Order_Id", External_Order_Id);
                            htget_Final_Package.Add("@Document_Type_Id", 13);
                            dtget_Final_Package = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htget_Final_Package);
                            if (dtget_Final_Package.Rows.Count > 0)
                            {

                                Final_Path = dtget_Final_Package.Rows[0]["Document_Path"].ToString();
                            }
                            else
                            {

                                Final_Path = "";
                            }


                            string Path1 = Final_Path;

                            if (Path1 != "")
                            {
                                MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));


                                mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Path.GetFileName(Path1)));
                            }

                        }

                    }


                 
                 
                    Subject = "OrderNo."+ Order_Number.ToString()+" - "+"Completed";
                 
                    
                    mailMessage.Subject = Subject.ToString();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Subject: " + Subject.ToString() + "" + Environment.NewLine);





                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;



                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "mail.titlelogy.com";

                    
                   
                   
                  //  NetworkCredential NetworkCred = new NetworkCredential("techteam@drnds.com", "nop539");

                   // NetworkCred = new NetworkCredential("niranjanmurthy@drnds.com", "123nir");
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay;
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.None;


                    //mailMessage.Headers.Add("Disposition-Notification-To", "niranjanmurthy@drnds.com");
                  
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                    
                    //smtp.EnableSsl = true;
                   
                    smtp.Port = 25;
                    smtp.UseDefaultCredentials = false;
                    NetworkCred = new NetworkCredential("neworders@titlelogy.com", "Dandin@12345");
                    smtp.Credentials = NetworkCred;
                    // smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
                    smtp.Timeout = (60 * 5 * 1000);
                  
                    //string userState = "test message1";

                    // this is Commented for development and testing purpose

                   smtp.Send(mailMessage);
                   smtp.Dispose();


                    Hashtable htupdate = new Hashtable();
                    DataTable dtupdate = new DataTable();

                    htupdate.Add("@Trans", "UPDATE");
                    htupdate.Add("@Order_Id", Order_Id);
                    htupdate.Add("@Sent_By", user_id);
                    dtupdate = dataaccess.ExecuteSP("Sp_Order_Email_Notification", htupdate);



                }
            }

        }

        


        // This is For Titlelogy Db Title Vendor Purpose
        private void Creat_Final_Title_Search_package()
        {

          
            Merge_Document_2();

        }


     

   

        public void Merge_Document_2()
        {
            Hashtable htsearch = new Hashtable();
            System.Data.DataTable dtsearch = new System.Data.DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_Id", External_Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htsearch);
            if (dtsearch.Rows.Count > 0)
            {
                P2 = dtsearch.Rows[0]["Document_Path"].ToString();
            }
            Hashtable htinvoice = new Hashtable();
            System.Data.DataTable dtinvoice = new System.Data.DataTable();
            htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
            htinvoice.Add("@Order_Id", External_Order_Id);
            dtinvoice = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htinvoice);
            if (dtinvoice.Rows.Count > 0)
            {
                P1 = dtinvoice.Rows[0]["Document_Path"].ToString();
            }
            Hashtable htin = new Hashtable();
            System.Data.DataTable dtin = new System.Data.DataTable();
            htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
            htin.Add("@Sub_Process_Id", Inhosue_Sub_Client_id);
            dtin = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htin);
            if (dtin.Rows.Count > 0)
            {
                Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

            }
            DataSet ds = new DataSet();
            ds.Clear();



            if (Inv_Status == "True")
            {
                //ds.Tables.Add(dtinvoice);
                //ds.Merge(dtsearch);
            }
            else if (Inv_Status == "False")
            {

                //  ds.Tables.Add(dtsearch);
            }

            if (Inv_Status == "True")
            {
                if (dtsearch.Rows.Count > 0)
                {



                    //Define a new output document and its size, type

                    Package = "InvoiceAndSearch";
                    Merge_Invoice_Search();
                    //FName = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf".Split('\\');
                    //string Source_Path = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf";
                    //System.IO.Directory.CreateDirectory(@"C:\temp");

                    //File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
                    //System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);


                }
                else
                {

                    MessageBox.Show("SearchPackage is Not Added Please Check it");

                }
            }
            else if (Inv_Status == "False")
            {
                if (dtsearch.Rows.Count > 0)
                {

                    Package = "Search";
                    Merge_Invoice_Search();
                    //FName = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf".Split('\\');
                    //string Source_Path = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf";
                    //System.IO.Directory.CreateDirectory(@"C:\temp");
                    //File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
                    //System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);
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
            if (Inv_Status == "True" && Package == "InvoiceAndSearch")
            {
                Index = 3;

            }
            else if (Inv_Status == "False" && Package == "Search")
            {

                Index = 2;
            }
            string[] lstFiles = new string[Index];
            if (Inv_Status == "True" && Package == "InvoiceAndSearch")
            {

                lstFiles[0] = P1;

                lstFiles[1] = P2;
            }
            else if (Inv_Status == "False" && Package == "Search")
            {

                lstFiles[0] = P2;


            }
            //lstFiles[2] = @"C:/pdf/3.pdf";

            PdfReader reader = null;
            iTextSharp.text.Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            string outputPdfPath = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf";



            sourceDocument = new iTextSharp.text.Document();
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


            string Invoice_Order_Number = External_Client_Order_Number.ToString();
            string Source = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf";

            string File_Name = "" + External_Client_Order_Number + ".pdf";
            //string Docname = FName[FName.Length - 1].ToString();
            string dest_path1 = @"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + External_Order_Id + @"\" + File_Name;
            DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
            de.Username = "administrator";
            de.Password = "password1$";


            Directory.CreateDirectory(@"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + External_Order_Id);
            string extension1 = Path.GetExtension(File_Name);
            File.Copy(Source, dest_path1, true);


            Hashtable htpath = new Hashtable();
            System.Data.DataTable dtpath = new System.Data.DataTable();

            Hashtable htcheck = new Hashtable();
            System.Data.DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK_FINAL_PACKAGE");
            htcheck.Add("@Order_Id", External_Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htcheck);
            int check;
            if (dtcheck.Rows.Count > 0)
            {
                check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                check = 0;
            }
            if (check == 0)
            {


                htpath.Add("@Trans", "INSERT");
                htpath.Add("@Document_Type_Id", 13);
                htpath.Add("@Order_Id", External_Order_Id);
                htpath.Add("@Document_From", 2);
                htpath.Add("@Document_File_Type", extension1.ToString());
                htpath.Add("@Description", "Search Packgae");
                htpath.Add("@Document_Path", dest_path1);
                htpath.Add("@File_Size", File_size);

                htpath.Add("@Inserted_date", DateTime.Now);
                htpath.Add("@status", "True");
                dtpath = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htpath);

            }


        }

        private int get_pageCcount(string file)
        {

            //Regex regex = new Regex(@"/Type\s*/Page[^s]");
            //MatchCollection matches = regex.Matches(sr.ReadToEnd());
            PdfReader pdfReader = new PdfReader(File.OpenRead(file));
            int numberOfPages = pdfReader.NumberOfPages;
            //return matches.Count;

            return numberOfPages;

        }
        private void Completed_Order_Mail_Load(object sender, EventArgs e)
        {

        }

        
    }
}
