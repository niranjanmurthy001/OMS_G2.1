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
namespace Ordermanagement_01.Client_Proposal
{
    public partial class Email_Test : Form
    {
        public Email_Test()
        {
            InitializeComponent();
        }

        private void button1_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send_Html_Email_Body();
        }

        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {

                    mm.IsBodyHtml = true;



                    string body = "Text";
                    SendHtmlFormattedEmail("netco@drnds.com", "Sample", "");

                    this.Close();

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }

        }


        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {



               mailMessage.From = new MailAddress("info@propertyreport247.com");
              //  mailMessage.From = new MailAddress("nirnajanmurthy@drnds.com");

                mailMessage.To.Add("niranjanmurthy@drnds.com");



                mailMessage.Subject = "Order Online Nationwide Title Search - Propertyreport247.com";




                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;



                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtpout.secureserver.net";




                NetworkCredential NetworkCred1 = new NetworkCredential("info@propertyreport247.com", "property247");
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = false;

                smtp.Credentials = NetworkCred1;
                smtp.Port = 25;

               
                smtp.Send(mailMessage);


            }
        }
    }
}
