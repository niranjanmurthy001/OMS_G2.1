using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Ordermanagement_01.Models;
using Newtonsoft.Json;
using System.Text;

namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Payment_Mail : Form
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
        int Monthly_Invoice_Id;
        string Month, Year, Emial_Contents, Emial_Add;
        string Ftp_Domain_Name, Ftp_User_Name, Ftp_Password;
        DataTable dt_Email_Details = new DataTable();
        public Abstractor_Payment_Mail(int MONTHLY_INVOICE_ID, string MONTH, string YEAR, string EMAIL_CONTENTS, string EMAIL_ADD)
        {
            InitializeComponent();
            Monthly_Invoice_Id = MONTHLY_INVOICE_ID;
            Month = MONTH;
            Year = YEAR;
            Emial_Contents = EMAIL_CONTENTS;
            Emial_Add = EMAIL_ADD;
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
                Export_Report();
                Send_Html_Email_Body();
                Close();
            }
            else
            {
                MessageBox.Show("Ftp File Path was not found; You cannot upload the documents please check with administrator");
                Close();
            }
        }

        public void Logon_To_Crystal()
        {
            List<string> cl_Lgoin = Comclass.Crystal_report_Login();
            crConnectionInfo.ServerName = cl_Lgoin[0].ToString();
            crConnectionInfo.DatabaseName = cl_Lgoin[1].ToString();
            crConnectionInfo.UserID = cl_Lgoin[2].ToString();
            crConnectionInfo.Password = cl_Lgoin[3].ToString();
            CrTables = rptDoc.Database.Tables;

            foreach (Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
        }

        public void Export_Report()
        {
            rptDoc = new Abstractor_Reports.Abstractor_Payment();
            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Abstractor_Monthly_Invoice_ID", Monthly_Invoice_Id);
            ExportOptions CrExportOptions;
            Download_Ftp_File("AbstractorReport.pdf", "ftp://titlelogy.com/Ftp_Application_Files/OMS/Abstractor_Files/Abstractor_Monthly_Payement.pdf");
            FileInfo newFile = new FileInfo("C:\\OMS\\Temp" + "\\" + "Abstractor_Monthly_Payement.pdf");
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

        private void Download_Ftp_File(string File_Name, string File_path)
        {
            try
            {
                FtpWebRequest reqFTP;
                FileStream outputStream = new FileStream("C:\\OMS\\Temp" + "\\" + File_Name, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(File_path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.CopyTo(outputStream);
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in Downloading Files please Check with Administrator");
                Close();
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
                    SendHtmlFormattedEmail("apinvoice@drnds.com", "Sample", body);
                    Close();
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
            body = Read_Body_From_Url("https://titlelogy.com/Ftp_Application_Files/OMS/Oms_Email_Templates/Abstractor_Payment_Email.htm");
            body = body.Replace("{Month}", "" + Month + " - " + Year + "");
            body = body.Replace("{Text}", Emial_Contents.ToString());
            return body;
        }

        private async void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("apinvoice@drnds.com");
                    string Path1 = "C:\\OMS\\Temp" + "\\" + "Abstractor_Monthly_Payement.pdf";
                    string Attachment_Name = "" + Month + "-" + Year + "Payment.pdf";
                    var maxsize = 15 * 1024 * 1000;
                    var fileName = Path1;
                    FileInfo fi = new FileInfo(fileName);
                    var size = fi.Length;
                    if (size <= maxsize)
                    {
                        MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));
                        mailMessage.Attachments.Add(new Attachment(ms, Attachment_Name.ToString()));
                        if (Emial_Add != "")
                        {
                            mailMessage.To.Add(Emial_Add.ToString());
                            mailMessage.CC.Add("apinvoice@drnds.com");
                         
                            string Subject = "Payment Statement - " + Month + "-" + Year + "";
                            mailMessage.Subject = Subject.ToString();
                            mailMessage.Body = body;
                            mailMessage.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                           

                            await Get_Email_Details("apinvoice@drnds.com");

                            if (dt_Email_Details.Rows.Count > 0)
                            {
                                smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                smtp.EnableSsl = false;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());
                                smtp.Send(mailMessage);
                                smtp.Dispose();

                            }
                            else
                            {
                                MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                            }

                            //smtp.Host = "mail.drnds.com";
                            //NetworkCred = new NetworkCredential("apinvoice@drnds.com", "AecXmC9T07DcP$");
                            //smtp.UseDefaultCredentials = false;
                            //smtp.Timeout = (60 * 5 * 1000);
                            //smtp.Credentials = NetworkCred;
                            //// smtp.EnableSsl = true;
                            //smtp.Port = 80;
                            ////string userState = "test message1";
                            //smtp.Send(mailMessage);
                            //smtp.Dispose();
                            //// Update_Invoice_Email_Status();
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
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
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

        public string Read_Body_From_Url(string Url)
        {
            WebResponse Result = null;
            WebRequest req = WebRequest.Create(Url);
            Result = req.GetResponse();
            Stream RStream = Result.GetResponseStream();
            StreamReader sr = new StreamReader(RStream);
            string body = sr.ReadToEnd().ToString();
            sr.Dispose();
            return body;
        }
        private async Task<DataTable> Get_Email_Details(string Email)
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
                            dt_Email_Details = JsonConvert.DeserializeObject<DataTable>(result2);
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

