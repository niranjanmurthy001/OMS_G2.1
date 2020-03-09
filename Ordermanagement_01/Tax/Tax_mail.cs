using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_mail : Form
    {
        DataAccess dataAccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable htorder = new Hashtable();
        System.Data.DataTable dtorder = new System.Data.DataTable();
        NetworkCredential NetworkCred;
        int Order_id; string userid, user_role, path, Ordernumber, OPERATION, SUBPROCESSID, EMAILID;
        string Tax_Content_Path, Tax_Header_Path;
        string Tax_Certificate_Path;
        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }
        System.Data.DataTable dtother = new System.Data.DataTable();
        int clientId;
        System.Data.DataTable dt_Email_Details = new System.Data.DataTable();
        private string directoryPath;
        private string year;
        private string month;
        private string Ftp_Domain_Name;
        private string Ftp_User_Name;
        private string Ftp_Password;
        private string Ftp_Path;
        private string mainPath;
        private string ftpfullpath;
        private string Upload_Directory;


        public Tax_mail(int orderid, string User_Id, string User_Role, string orderno, string operation, string subprocessid)
        {
            InitializeComponent();
            Order_id = orderid;
            userid = User_Id;
            user_role = User_Role;
            Ordernumber = orderno;
            OPERATION = operation;
            SUBPROCESSID = subprocessid;

            var dt = dbc.Get_Month_Year();
            if (dt != null && dt.Rows.Count > 0)
            {
                year = dt.Rows[0]["Year"].ToString();
                month = dt.Rows[0]["Month"].ToString();
            }
            var ht = new Hashtable();
            ht.Add("@Trans", "GET_CLIENT_DETAILS");
            ht.Add("@Order_Id", Order_id);
            var dt_client = dataAccess.ExecuteSP("Sp_Tax_Orders_Documents", ht);
            if (dt_client != null && dt_client.Rows.Count > 0)
            {
                clientId = Convert.ToInt32(dt_client.Rows[0]["Client_Id"].ToString());
            }
            System.Data.DataTable dt_ftp_Details = dbc.Get_Ftp_Details();
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
            else
            {
                throw new Exception("FTP Credentials not found");
            }
            directoryPath = year + "/" + month + "/" + clientId + "/" + Order_id + "";
            CreateDirectory(directoryPath);
            if (SUBPROCESSID == "349")
            {
                Merge_Word_Convert_Pdf_Files(orderid, int.Parse(subprocessid), Ordernumber);
                Get_Uploaded_Attached_Document();
                Send_Html_Email_Body();
                Close();

            }
            else if (SUBPROCESSID == "344")
            {

                Merge_Word_Convert_Files(orderid, int.Parse(subprocessid), Ordernumber);
                Get_Uploaded_Attached_Document();
                Send_Html_Email_Body();
                this.Close();
            }
            else
            {

                //  MessageBox.Show("Pdf Template is not Configured for this client");

            }

        }
        private void CreateDirectory(string directoryPath)
        {
            try
            {
                Upload_Directory = directoryPath;
                string Ftp_Host_Name = Ftp_Domain_Name;
                Ftp_Path = Ftp_Host_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents";
                string[] folderArray = Upload_Directory.Split('/');
                string folderName = "";
                for (int i = 0; i < folderArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(folderArray[i]))
                    {
                        try
                        {
                            folderName = string.IsNullOrEmpty(folderName) ? folderArray[i] : folderName + "/" + folderArray[i];
                            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create("ftp://" + Ftp_Path + "/" + folderName);
                            ftp.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
                            FtpWebResponse CreateForderResponse = (FtpWebResponse)ftp.GetResponse();
                            if (CreateForderResponse.StatusCode == FtpStatusCode.PathnameCreated)
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        private void Get_Email_Id()
        {
            Hashtable htmail = new Hashtable();
            System.Data.DataTable dtmail = new System.Data.DataTable();
            htmail.Add("@Trans", "GET_EMAIL_ID");
            htmail.Add("@Tax_Order_Id", SUBPROCESSID);
            dtmail = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htmail);
            if (dtmail.Rows.Count > 0)
            {
                EMAILID = dtmail.Rows[0]["Email_ID"].ToString();
            }
        }
        private void Get_Uploaded_Attached_Document()
        {

            // Get Tax Certificate path here

            Hashtable httax = new Hashtable();
            System.Data.DataTable dttax = new System.Data.DataTable();
            httax.Add("@Trans", "GET_TAX_CERTIFICATE");
            httax.Add("@Order_Id", Order_id);
            dttax = dataAccess.ExecuteSP("Sp_Tax_Orders_Documents", httax);

            if (dttax.Rows.Count > 0)
            {
                Tax_Certificate_Path = dttax.Rows[0]["New_Document_Path"].ToString();
            }
            else
            {
                Tax_Certificate_Path = "";
            }
            // get Other Attachments which are all checked

            Hashtable htother = new Hashtable();
            dtother.Clear();
            htother.Add("@Trans", "GET_TAX_OTHER_DOCUMENTS");
            htother.Add("@Order_Id", Order_id);
            dtother = dataAccess.ExecuteSP("Sp_Tax_Orders_Documents", htother);
        }
        private string Populate_Body()
        {
            string body = string.Empty;
            string templatePath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/Oms Email Templates/Tax_Completed_Mail_4100_4200_Client.htm";
            FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(templatePath));
            ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpReqFile.UseBinary = true;
            ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
            Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();

            using (StreamReader reader = new StreamReader(ftpStream))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }
        private async void SendHtmlFormattedEmail(string mail, string subject, string body)
        {
            if (OPERATION == "Bulk")
            {
                using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                {
                    Get_Email_Id();
                    mailMessage.To.Add(EMAILID);


                    if (EMAILID != "")
                    {
                        if (int.Parse(SUBPROCESSID) == 344)
                        {
                            mailMessage.From = new MailAddress("Taxes@drnds.com");
                        }
                        else if (int.Parse(SUBPROCESSID) == 349)
                        {
                            mailMessage.From = new MailAddress("Taxes@drnds.com");
                        }

                        string Attachment = Ordernumber.ToString() + ".pdf";
                        // this is for Tax Certificate
                        if (Tax_Certificate_Path != "")
                        {
                            FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Tax_Certificate_Path));
                            ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                            ftpReqFile.UseBinary = true;
                            ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                            Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                            mailMessage.Attachments.Add(new Attachment(ftpStream, Attachment.ToString()));//mail attachments
                        }
                        if (dtother.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtother.Rows.Count; i++)
                            {
                                string Other_Path = dtother.Rows[i]["New_Document_Path"].ToString();
                                string Attach_File_Name = "";
                                Attach_File_Name = Path.GetFileName(Other_Path);

                                FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Other_Path));
                                ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                                ftpReqFile.UseBinary = true;
                                ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                                Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                                mailMessage.Attachments.Add(new Attachment(ftpStream, Attach_File_Name.ToString()));//mail attachments
                            }
                        }

                        mailMessage.CC.Add("Taxes@drnds.com");//mail sending cc

                        string Subject = Ordernumber.ToString();
                        mailMessage.Subject = Subject.ToString();//mail subject
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        // smtp.Host = "smtpout.secureserver.net";
                        if (int.Parse(SUBPROCESSID) == 344)
                        {
                            await Get_Email_Details("Taxes@drnds.com");
                            if (dt_Email_Details.Rows.Count > 0)
                            {
                                smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                smtp.EnableSsl = false;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());
                                smtp.Send(mailMessage);
                                smtp.Dispose();

                                Update_Email_Status();
                            }
                            //NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");
                        }
                        else if (int.Parse(SUBPROCESSID) == 349)
                        {
                            await Get_Email_Details("Taxes@drnds.com");
                            if (dt_Email_Details.Rows.Count > 0)
                            {
                                smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                smtp.EnableSsl = false;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());
                                smtp.Send(mailMessage);
                                smtp.Dispose();

                                Update_Email_Status();
                            }
                            // NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");


                        }

                        //// smtp.UseDefaultCredentials = false;
                        //// smtp.EnableSsl = true;

                        //smtp.Timeout = (60 * 5 * 1000);
                        //smtp.Credentials = NetworkCred;
                        //smtp.Port = 25;

                    }
                    else
                    {
                        MessageBox.Show("To Email Address not Found ; Please setup in Client Subprocess");
                    }
                }
            }
            else
            {
                using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                {

                    if (int.Parse(SUBPROCESSID) == 344)
                    {
                        mailMessage.From = new MailAddress("Taxes@drnds.com");
                    }
                    else if (int.Parse(SUBPROCESSID) == 349)
                    {
                        mailMessage.From = new MailAddress("Taxes@drnds.com");


                    }

                    string Attachment = Ordernumber.ToString() + ".pdf";

                    // this is for Tax Certificate
                    if (Tax_Certificate_Path != "")
                    {
                        FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Tax_Certificate_Path));
                        ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                        ftpReqFile.UseBinary = true;
                        ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                        Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                        //ms = new MemoryStream(File.ReadAllBytes(Tax_Certificate_Path));
                        mailMessage.Attachments.Add(new Attachment(ftpStream, Attachment.ToString()));//mail attachments
                    }
                    if (dtother.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtother.Rows.Count; i++)
                        {
                            string Other_Path = dtother.Rows[i]["New_Document_Path"].ToString();
                            string Attach_File_Name = "";
                            Attach_File_Name = Path.GetFileName(Other_Path);

                            FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Other_Path));
                            ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                            ftpReqFile.UseBinary = true;
                            ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                            Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                            mailMessage.Attachments.Add(new Attachment(ftpStream, Attach_File_Name.ToString()));//mail attachments
                        }
                    }

                    Get_Email_Id();

                    mailMessage.To.Add(EMAILID);



                    string Subject = Ordernumber.ToString();
                    mailMessage.Subject = Subject.ToString();//mail subject
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();

                    // smtp.Host = "smtpout.secureserver.net";
                    if (int.Parse(SUBPROCESSID) == 344)
                    {
                        await Get_Email_Details("Taxes@drnds.com");
                        if (dt_Email_Details.Rows.Count > 0)
                        {
                            smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                            NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                            smtp.EnableSsl = false;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());

                            smtp.Send(mailMessage);
                            smtp.Dispose();

                            Update_Email_Status();
                        }
                        else
                        {
                            MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                        }
                        // NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");
                    }
                    else if (int.Parse(SUBPROCESSID) == 349)
                    {
                        await Get_Email_Details("Taxes@drnds.com");
                        if (dt_Email_Details.Rows.Count > 0)
                        {
                            smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                            NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                            smtp.EnableSsl = false;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());
                            smtp.Send(mailMessage);
                            smtp.Dispose();

                            Update_Email_Status();
                        }
                        else
                        {
                            MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                        }
                        // NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");

                    }
                    //// smtp.EnableSsl = true;
                    //smtp.Timeout = (60 * 5 * 1000);
                    //smtp.Credentials = NetworkCred;
                    //smtp.Port = 25;

                    this.Close();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("File is too long, Restrict file size within 20 MB");
                    //    this.Close();
                    //}
                }
            }
        }

        private async void SendHtmlFormattedEmail_Word(string mail, string subject, string body)
        {
            try
            {
                if (OPERATION == "Bulk")
                {
                    using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                    {
                        Get_Email_Id();

                        mailMessage.To.Add(EMAILID);
                        mailMessage.CC.Add("Taxes@drnds.com");

                        if (EMAILID != "")
                        {
                            if (int.Parse(SUBPROCESSID) == 344)
                            {
                                mailMessage.From = new MailAddress("Taxes@drnds.com");
                            }
                            else if (int.Parse(SUBPROCESSID) == 349)
                            {
                                mailMessage.From = new MailAddress("Taxes@drnds.com");
                            }


                            string Attachment = Ordernumber.ToString() + ".docx";

                            // this is for Tax Certificate
                            if (Tax_Certificate_Path != "")
                            {
                                FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Tax_Certificate_Path));
                                ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                                ftpReqFile.UseBinary = true;
                                ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                                Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                                mailMessage.Attachments.Add(new Attachment(ftpStream, Attachment.ToString()));//mail attachments
                            }
                            if (dtother.Rows.Count > 0)
                            {

                                for (int i = 0; i < dtother.Rows.Count; i++)
                                {
                                    string Other_Path = dtother.Rows[i]["New_Document_Path"].ToString();
                                    string Attach_File_Name = "";
                                    Attach_File_Name = Path.GetFileName(Other_Path);

                                    FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Other_Path));
                                    ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                                    ftpReqFile.UseBinary = true;
                                    ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                                    Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                                    mailMessage.Attachments.Add(new Attachment(ftpStream, Attach_File_Name.ToString()));//mail attachments
                                }
                            }

                            mailMessage.CC.Add("Taxes@drnds.com");//mail sending cc
                            string Subject = Ordernumber.ToString();
                            mailMessage.Subject = Subject.ToString();//mail subject
                            mailMessage.Body = body;
                            mailMessage.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            // smtp.Host = "smtpout.secureserver.net";
                            if (int.Parse(SUBPROCESSID) == 344)
                            {
                                await Get_Email_Details("Taxes@drnds.com");
                                if (dt_Email_Details.Rows.Count > 0)
                                {
                                    smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                    NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                    smtp.EnableSsl = false;
                                    smtp.Credentials = NetworkCred;
                                    smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());

                                    smtp.Send(mailMessage);
                                    smtp.Dispose();
                                    Update_Email_Status();
                                }
                                else
                                {
                                    MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                                }
                                // NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");
                            }
                            else if (int.Parse(SUBPROCESSID) == 349)
                            {
                                await Get_Email_Details("Taxes@drnds.com");
                                if (dt_Email_Details.Rows.Count > 0)
                                {
                                    smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                    NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                    smtp.EnableSsl = false;
                                    smtp.Credentials = NetworkCred;
                                    smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());

                                    smtp.Send(mailMessage);
                                    smtp.Dispose();
                                    Update_Email_Status();
                                }
                                else
                                {
                                    MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                                }
                                // NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");
                            }

                            //// smtp.UseDefaultCredentials = false;
                            //// smtp.EnableSsl = true;

                            //smtp.UseDefaultCredentials = true;
                            //smtp.Timeout = (60 * 5 * 1000);
                            //smtp.Credentials = NetworkCred;
                            //smtp.Port = 25;

                        }
                        else
                        {
                            MessageBox.Show("To Email Address not Found ; Please setup in Client Subprocess");
                        }
                    }
                }
                else
                {
                    using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                    {
                        if (int.Parse(SUBPROCESSID) == 344)
                        {
                            mailMessage.From = new MailAddress("Taxes@drnds.com");
                        }
                        else if (int.Parse(SUBPROCESSID) == 349)
                        {
                            mailMessage.From = new MailAddress("Taxes@drnds.com");
                        }

                        string Attachment = Ordernumber.ToString() + ".docx";

                        // this is for Tax Certificate
                        if (Tax_Certificate_Path != "")
                        {
                            FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Tax_Certificate_Path));
                            ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                            ftpReqFile.UseBinary = true;
                            ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                            Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                            mailMessage.Attachments.Add(new Attachment(ftpStream, Attachment.ToString()));//mail attachments
                        }
                        if (dtother.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtother.Rows.Count; i++)
                            {
                                string Other_Path = dtother.Rows[i]["New_Document_Path"].ToString();
                                string Attach_File_Name = "";
                                Attach_File_Name = Path.GetFileName(Other_Path);

                                FtpWebRequest ftpReqFile = (FtpWebRequest)WebRequest.Create(new Uri(Tax_Certificate_Path));
                                ftpReqFile.Method = WebRequestMethods.Ftp.DownloadFile;
                                ftpReqFile.UseBinary = true;
                                ftpReqFile.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                                Stream ftpStream = ftpReqFile.GetResponse().GetResponseStream();
                                mailMessage.Attachments.Add(new Attachment(ftpStream, Attach_File_Name.ToString()));//mail attachments
                            }
                        }

                        Get_Email_Id();

                        mailMessage.To.Add(EMAILID);
                        mailMessage.CC.Add("Taxes@drnds.com");



                        string Subject = Ordernumber.ToString();
                        mailMessage.Subject = Subject.ToString();//mail subject
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();

                        //   smtp.Host = "smtpout.secureserver.net";
                        if (int.Parse(SUBPROCESSID) == 344)
                        {
                            await Get_Email_Details("Taxes@drnds.com");
                            if (dt_Email_Details.Rows.Count > 0)
                            {
                                smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                smtp.EnableSsl = false;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());

                                smtp.Send(mailMessage);
                                smtp.Dispose();

                                Update_Email_Status();
                            }
                            else
                            {
                                MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                            }
                            //  NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");

                        }
                        else if (int.Parse(SUBPROCESSID) == 349)
                        {
                            await Get_Email_Details("Taxes@drnds.com");
                            if (dt_Email_Details.Rows.Count > 0)
                            {
                                smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                smtp.EnableSsl = false;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());

                                smtp.Send(mailMessage);
                                smtp.Dispose();

                                Update_Email_Status();
                            }
                            else
                            {
                                MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                            }

                            // NetworkCred = new NetworkCredential("Taxes@drnds.com", "ffP2mx=EoM");
                        }

                        //// smtp.EnableSsl = true;

                        //smtp.Timeout = (60 * 5 * 1000);
                        //smtp.Credentials = NetworkCred;
                        //smtp.Port = 25;

                        Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Update_Email_Status()
        {
            htorder.Clear(); dtorder.Clear();
            htorder.Add("@Trans", "UPDATE_EMAIL_STATUS");
            htorder.Add("@Order_Id", Order_id);
            dtorder = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htorder);
            MessageBox.Show("Mail Sent Successfully");
        }
        private void Send_Html_Email_Body()
        {
            using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
            {
                try
                {
                    mail.IsBodyHtml = true;
                    string body = this.Populate_Body();

                    if (SUBPROCESSID == "349")
                    {
                        SendHtmlFormattedEmail("Taxes@drnds.com", "Sample", body);
                        //this is commented beacuse of No Pdf Files were sending
                    }

                    if (SUBPROCESSID == "344")
                    {

                        SendHtmlFormattedEmail_Word("Taxes@drnds.com", "Sample", body);

                    }
                    this.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Tax_mail_Load(object sender, EventArgs e)
        {

        }



        private void Merge_Word_Convert_Pdf_Files(int Order_Id, int Subprocess_Id, string Order_Number)
        {

            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_WORD_TAX_CERTIFICATE");
            ht.Add("@Order_Id", Order_Id);
            dt = dataAccess.ExecuteSP("Sp_Tax_Orders_Documents", ht);

            if (Subprocess_Id == 349)
            {
                if (dt.Rows.Count == 1)
                {
                    Tax_Content_Path = dt.Rows[0]["New_Document_Path"].ToString();
                    string Extension = Path.GetExtension(Tax_Content_Path);
                    if (Extension == ".doc" || Extension == ".docx")
                    {
                        string dirTemp = "C:\\OMS\\Temp";
                        if (!Directory.Exists(dirTemp))
                        {
                            var dirInfo = Directory.CreateDirectory(dirTemp);
                        }
                        string sourceFileName = Order_Number + "-FinalCertSource.docx";
                        string fileSource = dirTemp + "\\" + sourceFileName;

                        Download_Ftp_File(sourceFileName, Tax_Content_Path);

                        object oMissing = System.Reflection.Missing.Value;
                        var wordApp = new Word.Application();
                        var contentSource = wordApp.Documents.Open(fileSource);
                        contentSource.ActiveWindow.Selection.WholeStory();
                        contentSource.ActiveWindow.Selection.Copy();
                        contentSource.Close();

                        string destFileName = Order_Number + "-FinalCertDest.docx";
                        string fileDest = dirTemp + "\\" + destFileName;
                        string pdfFileName = dirTemp + "\\" + Order_Number + "-FinalCert.pdf";

                        string sourcePathHeader = string.Empty;
                        if (Subprocess_Id == 344)
                        {
                            sourcePathHeader = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/42001_Tax_Header_Document.docx";
                        }
                        if (Subprocess_Id == 349)
                        {
                            sourcePathHeader = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/43001_Header_Tax_Document.docx";
                        }
                        Download_Ftp_File(destFileName, sourcePathHeader);
                        var tempDoc = wordApp.Documents.Open(fileDest);
                        tempDoc.ActiveWindow.Selection.WholeStory();
                        tempDoc.ActiveWindow.Selection.PasteAndFormat(WdRecoveryType.wdUseDestinationStylesRecovery);
                        tempDoc.SaveAs(fileDest);
                        tempDoc.Save();
                        tempDoc.Close();

                        Marshal.ReleaseComObject(wordApp);
                        Marshal.ReleaseComObject(contentSource);
                        Marshal.ReleaseComObject(tempDoc);
                        GC.Collect();

                        RichEditDocumentServer server = new RichEditDocumentServer();
                        server.LoadDocument(fileDest, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);
                        PdfExportOptions options = new PdfExportOptions();
                        options.ImageQuality = PdfJpegImageQuality.Highest;
                        options.Compressed = false;
                        FileStream stream = new FileStream(pdfFileName, FileMode.Create);
                        server.ExportToPdf(stream, options);
                        server.Dispose();
                        stream.Dispose();

                        string current_time = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss");

                        Stream fileStream = new FileStream(pdfFileName, FileMode.Open);
                        string File_Name = "Tax_Certifcate_" + Order_Number + "-" + current_time + ".pdf";
                        ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/" + directoryPath + "";
                        string ftpUploadFullPath = "" + ftpfullpath + "/" + File_Name + "";

                        FtpWebRequest reqUploadCheckfile = (FtpWebRequest)WebRequest.Create(new Uri(ftpfullpath));
                        reqUploadCheckfile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                        reqUploadCheckfile.Method = WebRequestMethods.Ftp.ListDirectory;
                        StreamReader reader = new StreamReader(reqUploadCheckfile.GetResponse().GetResponseStream());

                        HashSet<string> directories = new HashSet<string>(); // create list to store directories.   
                        string line = reader.ReadLine();
                        while (!string.IsNullOrEmpty(line))
                        {
                            directories.Add(line); // Add Each Directory to the List.  
                            line = reader.ReadLine();
                        }
                        if (directories.Contains(File_Name))
                        {
                            FtpWebRequest delRequest = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                            delRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            delRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                            FtpWebResponse responseDelete = (FtpWebResponse)delRequest.GetResponse();
                            if (responseDelete.StatusCode == FtpStatusCode.FileActionOK)
                            {
                                FtpWebRequest uploadReq = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                                uploadReq.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                uploadReq.Method = WebRequestMethods.Ftp.UploadFile;
                                Stream ftpUploadStream = uploadReq.GetRequestStream();
                                fileStream.CopyTo(ftpUploadStream);
                                fileStream.Close();
                                ftpUploadStream.Close();
                            }
                        }
                        else
                        {
                            FtpWebRequest uploadReq = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                            uploadReq.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            uploadReq.Method = WebRequestMethods.Ftp.UploadFile;
                            Stream ftpUploadStream = uploadReq.GetRequestStream();
                            fileStream.CopyTo(ftpUploadStream);
                            fileStream.Close();
                            ftpUploadStream.Close();
                        }

                        string Pdf_Extension = Path.GetExtension(pdfFileName);

                        Hashtable htorderkb = new Hashtable();
                        System.Data.DataTable dtorderkb = new System.Data.DataTable();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Order_Id", Order_Id);
                        htorderkb.Add("@Document_Type", 15);
                        htorderkb.Add("@Instuction", "Final Tax Certificate");
                        htorderkb.Add("@Document_Path", ftpUploadFullPath);
                        htorderkb.Add("@File_Extension", Pdf_Extension);
                        htorderkb.Add("@Tax_Task", 2);
                        htorderkb.Add("@Inserted_By", userid);
                        htorderkb.Add("@status", "True");
                        dtorderkb = dataAccess.ExecuteSP("Sp_Tax_Orders_Documents", htorderkb);

                    }
                }
                else
                {
                    MessageBox.Show("More than 1 tax Certificate were Checked ; Please check only one Tax Certificate");
                }
            }
        }

        private void Download_Ftp_File(string p, string Source_Path)
        {
            try
            {
                FtpWebRequest reqFTP;
                FileStream outputStream = new FileStream("C:\\OMS\\Temp" + "\\" + p, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Source_Path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.CopyTo(outputStream);
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Merge_Word_Convert_Files(int Order_Id, int Subprocess_Id, string Order_Number)
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_WORD_TAX_CERTIFICATE");
            ht.Add("@Order_Id", Order_Id);
            dt = dataAccess.ExecuteSP("Sp_Tax_Orders_Documents", ht);

            if (Subprocess_Id == 344)
            {
                if (dt.Rows.Count == 1)
                {
                    Tax_Content_Path = dt.Rows[0]["New_Document_Path"].ToString();
                    string Extension = Path.GetExtension(Tax_Content_Path);
                    if (Extension == ".doc" || Extension == ".docx")
                    {
                        string dirTemp = "C:\\OMS\\Temp";
                        if (!Directory.Exists(dirTemp))
                        {
                            var dirInfo = Directory.CreateDirectory(dirTemp);
                        }
                        string sourceFileName = Order_Number + "-FinalCertSource.docx";
                        string fileSource = dirTemp + "\\" + sourceFileName;

                        Download_Ftp_File(sourceFileName, Tax_Content_Path);

                        object oMissing = System.Reflection.Missing.Value;
                        var wordApp = new Word.Application();
                        var contentSource = wordApp.Documents.Open(fileSource);
                        contentSource.ActiveWindow.Selection.WholeStory();
                        contentSource.ActiveWindow.Selection.Copy();
                        contentSource.Close();

                        string destFileName = Order_Number + "-FinalCertDest.docx";
                        string fileDest = dirTemp + "\\" + destFileName;

                        string sourcePathHeader = string.Empty;
                        if (Subprocess_Id == 344)
                        {
                            sourcePathHeader = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/42001_Tax_Header_Document.docx";
                        }
                        if (Subprocess_Id == 349)
                        {
                            sourcePathHeader = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/43001_Header_Tax_Document.docx";
                        }
                        Download_Ftp_File(destFileName, sourcePathHeader);

                        var tempDoc = wordApp.Documents.Open(fileDest);
                        tempDoc.ActiveWindow.Selection.WholeStory();
                        tempDoc.ActiveWindow.Selection.PasteAndFormat(WdRecoveryType.wdUseDestinationStylesRecovery);
                        tempDoc.SaveAs(fileDest);
                        tempDoc.Save();
                        tempDoc.Close();

                        Marshal.ReleaseComObject(wordApp);
                        Marshal.ReleaseComObject(contentSource);
                        Marshal.ReleaseComObject(tempDoc);
                        GC.Collect();

                        Stream fileStream = new FileStream(dirTemp + "\\" + destFileName, FileMode.Open);
                        string File_Name = "Tax_CERTIFICATE_" + Order_Number + ".docx";
                        ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/" + directoryPath + "";
                        string ftpUploadFullPath = "" + ftpfullpath + "/" + File_Name + "";

                        FtpWebRequest reqUploadCheckfile = (FtpWebRequest)WebRequest.Create(new Uri(ftpfullpath));
                        reqUploadCheckfile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                        reqUploadCheckfile.Method = WebRequestMethods.Ftp.ListDirectory;
                        StreamReader reader = new StreamReader(reqUploadCheckfile.GetResponse().GetResponseStream());

                        HashSet<string> directories = new HashSet<string>(); // create list to store directories.   
                        string line = reader.ReadLine();
                        while (!string.IsNullOrEmpty(line))
                        {
                            directories.Add(line); // Add Each Directory to the List.  
                            line = reader.ReadLine();
                        }
                        if (directories.Contains(File_Name))
                        {
                            FtpWebRequest delRequest = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                            delRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            delRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                            FtpWebResponse responseDelete = (FtpWebResponse)delRequest.GetResponse();
                            if (responseDelete.StatusCode == FtpStatusCode.FileActionOK)
                            {
                                FtpWebRequest uploadReq = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                                uploadReq.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                uploadReq.Method = WebRequestMethods.Ftp.UploadFile;
                                Stream ftpUploadStream = uploadReq.GetRequestStream();
                                fileStream.CopyTo(ftpUploadStream);
                                fileStream.Close();
                                ftpUploadStream.Close();
                            }
                        }
                        else
                        {
                            FtpWebRequest uploadReq = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                            uploadReq.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            uploadReq.Method = WebRequestMethods.Ftp.UploadFile;
                            Stream ftpUploadStream = uploadReq.GetRequestStream();
                            fileStream.CopyTo(ftpUploadStream);
                            fileStream.Close();
                            ftpUploadStream.Close();
                        }
                        Hashtable htorderkb = new Hashtable();
                        System.Data.DataTable dtorderkb = new System.Data.DataTable();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Order_Id", Order_Id);
                        htorderkb.Add("@Document_Type", 15);
                        htorderkb.Add("@Instuction", "Final Tax Certificate");
                        htorderkb.Add("@Document_Path", ftpUploadFullPath);
                        htorderkb.Add("@File_Extension", ".docx");
                        htorderkb.Add("@Tax_Task", 2);
                        htorderkb.Add("@Inserted_By", userid);
                        htorderkb.Add("@status", "True");
                        dtorderkb = dataAccess.ExecuteSP("Sp_Tax_Orders_Documents", htorderkb);
                    }
                }
                else
                {
                    MessageBox.Show("More than 1 tax Certificate were Checked ; Please check only one Tax Certificate");
                }
            }
        }
        private async Task<System.Data.DataTable> Get_Email_Details(string Email)
        {

            // Call api
            dt_Email_Details.Clear();
            try
            {

                var dictionary = new Dictionary<string, object>()
                                            {
                                               { "@Trans", "SELECT_BY_EMAIL"},
                                               { "@Email_Address",Email},
                                          };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient2 = new HttpClient())
                {
                    var response2 = await httpClient2.PostAsync(Base_Url.Url + "/EmailSettings/BindData", data);
                    if (response2.IsSuccessStatusCode)
                    {
                        if (response2.StatusCode == HttpStatusCode.OK)
                        {
                            var result2 = await response2.Content.ReadAsStringAsync();
                            dt_Email_Details = JsonConvert.DeserializeObject<System.Data.DataTable>(result2);
                        }
                    }
                }
                // get data
                return dt_Email_Details;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
