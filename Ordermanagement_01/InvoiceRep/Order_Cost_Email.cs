﻿using System;
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
    public partial class Order_Cost_Email : Form
    {

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        int Order_Id;
        string[] FName;
        string Document_Name;
        string Client_Order_no;
        int Order_Type;
        int abstarctor_id, User_Id;
        string Path1;
        string Attachment_Name;
        string Directory_Path;

        string Order_Number;

        string Email, Alternative_Email;
        int Order_Cost_Id;
        string View_File_Path;
        string Invoice_Status;
        DialogResult dialogResult;
        string Forms;
        string Package = "";
        string P1, P2;
        int Index;
        int Sub_Process_ID;
        string Order_Costs;
        private string Ftp_Domain_Name;
        private string Ftp_User_Name;
        private string Ftp_Password;
        public Order_Cost_Email(string ORDER_NUMBER, int USER_ID, int ORDER_ID, int ORDER_COST_ID, int SUB_PROCESS_ID, string ORDERCOST)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            User_Id = USER_ID;


            Order_Number = ORDER_NUMBER.ToString();
            Order_Cost_Id = ORDER_COST_ID;
            Sub_Process_ID = SUB_PROCESS_ID;
            Order_Costs = ORDERCOST;


            DataTable dt_ftp_Details = dbc.Get_Ftp_Details();
            if (dt_ftp_Details.Rows.Count > 0)
            {
                Ftp_Domain_Name = dt_ftp_Details.Rows[0]["Ftp_Host_Name"].ToString();

                Ftp_User_Name = dt_ftp_Details.Rows[0]["Ftp_User_Name"].ToString();

                string Ftp_pass = dt_ftp_Details.Rows[0]["Ftp_Password"].ToString();

                if (Ftp_pass != "")
                {
                    Ftp_Password = dbc.Decrypt(Ftp_pass);
                }
            }
            Send_Html_Email_Body();
        }

        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {





                    mm.IsBodyHtml = true;



                    string body = this.PopulateBody();
                    SendHtmlFormattedEmail("netco@drnds.com", "Sample", body);

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




            Hashtable htorder = new Hashtable();
            DataTable dtorder = new DataTable();
            htorder.Add("@Trans", "GET_ORDER_COST_DETAILS_FOR_EMAIL");
            htorder.Add("@Order_ID", Order_Id);
            dtorder = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htorder);
            if (dtorder.Rows.Count > 0)
            {


            }

            string Title = dtorder.Rows[0]["Order_Type"].ToString();
            string Comments = dtorder.Rows[0]["Comments"].ToString();


            // using (StreamReader reader = new StreamReader(Directory_Path))
            // {

            body = Read_Body_From_Url("https://titlelogy.com/Ftp_Application_Files/OMS/Oms_Email_Templates/order_Cost.htm");
            // }
            body = body.Replace("{Text}", "The fee for this order is $" + Order_Costs.ToString() + "");

            if (Comments != "")
            {
                body = body.Replace("{Comments}", "Comments:" + Comments.ToString() + "");
            }

            return body;
        }

        public string Read_Body_From_Url(string Url)
        {
            WebResponse Result = null;
            string Page_Source_Code;
            WebRequest req = WebRequest.Create(Url);
            Result = req.GetResponse();
            Stream RStream = Result.GetResponseStream();
            StreamReader sr = new StreamReader(RStream);
            new StreamReader(RStream);
            Page_Source_Code = sr.ReadToEnd();
            sr.Dispose();
            string body = Page_Source_Code.ToString();
            return body;
        }
        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("netco@drnds.com");

                Hashtable htsearch = new Hashtable();
                DataTable dtsearch = new DataTable();
                htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
                htsearch.Add("@Order_ID", Order_Id);
                dtsearch = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htsearch);


                if (dtsearch.Rows.Count > 0)
                {
                    FName = dtsearch.Rows[0]["Document_Name"].ToString().Split('\\');
                    string Source_Path = dtsearch.Rows[0]["New_Document_Path"].ToString();

                    //  Path1 = Source_Path;
                    FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Source_Path));
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    Stream ftpStream = response.GetResponseStream();

                    var maxsize = 20 * 1024 * 1000;
                    //var fileName = Path1;
                    //FileInfo fi = new FileInfo(fileName);
                    var size = Ftp_File_Size(Source_Path);
                    if (size <= maxsize)
                    {
                        Attachment_Name = Order_Number.ToString() + ".pdf";
                        mailMessage.Attachments.Add(new Attachment(ftpStream, Attachment_Name.ToString()));
                        Hashtable htdate = new Hashtable();
                        DataTable dtdate = new DataTable();
                        htdate.Add("@Trans", "SELECT_CLIENT_EMAIL");
                        htdate.Add("@Order_ID", Order_Id);
                        dtdate = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htdate);
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
                                mailMessage.To.Add(dtdate.Rows[j]["Email_ID"].ToString());
                            }
                            mailMessage.Bcc.Add("jegadeesh@drnds.com");

                            //  mailMessage.To.Add("techteam@drnds.com");
                            Hashtable htorder = new Hashtable();
                            DataTable dtorder = new DataTable();
                            htorder.Add("@Trans", "GET_ORDER_COST_DETAILS_FOR_EMAIL");
                            htorder.Add("@Order_ID", Order_Id);
                            dtorder = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htorder);
                            if (dtorder.Rows.Count > 0)
                            {


                            }

                            string Title = dtorder.Rows[0]["Order_Type"].ToString();
                            string Comments = dtorder.Rows[0]["Comments"].ToString();
                            string Subject = " " + Order_Number + "  -  " + Title.ToString();
                            mailMessage.Subject = Subject.ToString();

                            StringBuilder sb = new StringBuilder();
                            sb.Append("Subject: " + Subject.ToString() + "" + Environment.NewLine);


                            mailMessage.Body = body;
                            mailMessage.IsBodyHtml = true;



                            SmtpClient smtp = new SmtpClient();

                            smtp.Host = "smtpout.secureserver.net";

                            NetworkCredential NetworkCred = new NetworkCredential("netco@drnds.com", "Capitalcity2020$");
                            smtp.UseDefaultCredentials = true;
                            // smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
                            smtp.Timeout = (60 * 5 * 1000);
                            smtp.Credentials = NetworkCred;
                            //smtp.EnableSsl = true;
                            smtp.Port = 25;

                            smtp.Send(mailMessage);
                            smtp.Dispose();


                            Update_Email_Status();

                        }
                        else
                        {

                            MessageBox.Show("Email is Not Added Kindly Check It");
                        }
                    }
                    else
                    {

                        MessageBox.Show("Attachment Should Not be greater than 20 mb");
                    }
                }
                else
                {

                    MessageBox.Show("Search Package not added check it");
                }
            }
        }

        public void Update_Email_Status()
        {

            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new DataTable();
            htupdate.Add("@Trans", "UPDATE_EMAIL_STATUS");
            htupdate.Add("@Order_ID", Order_Id);
            dtupdate = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htupdate);

        }
        public long Ftp_File_Size(string Ftp_File_Path)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(Ftp_File_Path));
            request.Proxy = null;
            request.Credentials = new NetworkCredential(@"clone\ftpuser", "Qwerty@12345");
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            long size = response.ContentLength;
            response.Close();
            return size;
        }
    }

}
