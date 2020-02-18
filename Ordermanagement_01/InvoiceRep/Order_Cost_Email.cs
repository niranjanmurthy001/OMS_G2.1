using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Ordermanagement_01.Models;

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
        DataTable dt_Email_Details = new DataTable();
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
            else
            {
                body = body.Replace("{Comments}", string.Empty);
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
                throw ex;
            }
        }

        private void Order_Cost_Email_Load(object sender, EventArgs e)
        {

        }

        private async void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            try
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
                        //FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Source_Path));
                        //reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                        //reqFTP.UseBinary = true;
                        //reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                        //FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        //Stream ftpStream = response.GetResponseStream();
                        string Folder_Path = "C:\\OMS\\Temp\\";
                        if (!Directory.Exists(Folder_Path))
                        {
                            Directory.CreateDirectory(Folder_Path);
                        }
                        Download_Ftp_File(Order_Number.ToString() + ".pdf", Source_Path);
                        var maxsize = 20 * 1024 * 1000;
                        //var fileName = Path1;
                        //FileInfo fi = new FileInfo(fileName);
                        var size = Ftp_File_Size(Source_Path);
                        if (size <= maxsize)
                        {
                            MemoryStream ms = new MemoryStream(File.ReadAllBytes(Folder_Path + Order_Number.ToString() + ".pdf"));
                            Attachment_Name = Order_Number.ToString() + ".pdf";
                            mailMessage.Attachments.Add(new Attachment(ms, Attachment_Name.ToString()));
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

                                await Get_Email_Details("netco@drnds.com");

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


                                //smtp.Host = "smtpout.secureserver.net";

                                //NetworkCredential NetworkCred = new NetworkCredential("netco@drnds.com", "P2DGo5fi-c");
                                //smtp.UseDefaultCredentials = true;
                                //// smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
                                //smtp.Timeout = (60 * 5 * 1000);
                                //smtp.Credentials = NetworkCred;
                                ////smtp.EnableSsl = true;
                                //smtp.Port = 25;

                                //smtp.Send(mailMessage);
                                //smtp.Dispose();


                                //Update_Email_Status();

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
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
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
        private async Task<DataTable> Get_Email_Details(string Email)
        {

            // Call api
            dt_Email_Details.Clear();
            try {

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
