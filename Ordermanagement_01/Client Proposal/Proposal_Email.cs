using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Net.Mime;

namespace Ordermanagement_01.Client_Proposal
{
    public partial class Proposal_Email : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid,Proposal_clientid; string clientname,clientid,emailid,Errormsg;
        public Proposal_Email(string Clientid, string Clientname, int Userid, string Emailid, int proposal_clientid)
        {
            InitializeComponent();
            clientid = Clientid;
            userid = Userid;
            clientname = Clientname;
            emailid = Emailid;
            Proposal_clientid = proposal_clientid;
            HtmlBody();
            this.Close();
            
        }
        public string PopulateBody()
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\Proposal_Email.htm"))
            {
                body = reader.ReadToEnd();
            }

            
            body = body.Replace("{Client-Name}", clientname);

            return body;
        }
        private void HtmlBody()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {

                    mm.IsBodyHtml = true;
                    string body = this.PopulateBody();
                    SendHtmlFormattedEmail("techteam@drnds.com", "Sample", body);



                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Errormsg = ex.Message;
                }
            }
        }
        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("techteam@drnds.com");

                Hashtable htemail = new Hashtable();
                DataTable dtemail = new DataTable();
                htemail.Add("@Trans", "SELECT_EMAILPASS");
                dtemail = dataaccess.ExecuteSP("Sp_Proposal_Client", htemail);

                Hashtable htpath = new Hashtable();
                DataTable dtpath = new DataTable();
                htpath.Add("@Trans", "SELECT_TRUE_ATTACH");

                dtpath = dataaccess.ExecuteSP("Sp_Proposal_Client", htpath);
                if (dtpath.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpath.Rows.Count; i++)
                    {
                        string Path1 = dtpath.Rows[i]["File_Path"].ToString();
                        MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));

                        string Attachment_Name = dtpath.Rows[i]["File_Name"].ToString();
                       

                        mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));

                        mailMessage.To.Add(emailid);

                        mailMessage.To.Add("techteam@drnds.com");

                        StringBuilder sb = new StringBuilder();
                        sb.Append("Subject: " + "Regarding Client Proposal" + "" + Environment.NewLine);
                        mailMessage.Body = body;
                      
                          

                      

                        SmtpClient smtp = new SmtpClient();

                        smtp.Host = "smtpout.secureserver.net";

                        NetworkCredential NetworkCred = new NetworkCredential("techteam@drnds.com", "nop539");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 80;
                        smtp.Send(mailMessage);
                        smtp.Dispose();

                        //updating record in transaction table
                        //Hashtable htup = new Hashtable();
                        //DataTable dtup = new DataTable();
                        //htup.Add("@Trans", "INSERT_PROPOSAL_SEND");
                        //htup.Add("@Proposal_Client_Id", Proposal_clientid);
                        //htup.Add("@Inserted_by", userid);
                        //dtup = dataaccess.ExecuteSP("Sp_Proposal_Client", htup);
                        
                        MessageBox.Show("Mail Sended Successfully");
                    }
                }
            }
        }

        private void Proposal_Email_Load(object sender, EventArgs e)
        {

        }
    }
}
