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
using System.Web.Mail;
namespace Ordermanagement_01
{
    public partial class Email_Send : Form
    {
        public Email_Send()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (MailMessage mailMessage = new MailMessage())
            //    {
            //        MailAddress ma;
            //        ma = new MailAddress("vendors@drnds.com", "new Orders");
            //        mailMessage.From = ma;
            //        mailMessage.To.Add("nirnajanmurthy@drnds.com");
                 
            //        mailMessage.Subject = "Test";
            //        mailMessage.Body = "Test";

            //        SmtpClient smtp = new SmtpClient();

            //        smtp.Host = "mail.drnds.com";
            //        smtp.Port = 465;
            //        //smtp.UseDefaultCredentials = false;
            //        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            //        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay;
            //        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;
            //        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.None;


            //        //mailMessage.Headers.Add("Disposition-Notification-To", "niranjanmurthy@drnds.com");

            //        //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //        //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
            //        smtp.EnableSsl = true;
            //        NetworkCredential NetworkCred = new NetworkCredential("vendors@drnds.com", "Dandin@123");
            //        smtp.Credentials = NetworkCred;
            //        // smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
            //        smtp.Timeout = (60 * 5 * 1000);

            //        //string userState = "test message1";

            //        // this is Commented for development and testing purpose

            //        smtp.Send(mailMessage);
            //        smtp.Dispose();

            //        MessageBox.Show("mail Sent Sucessfully");
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString());
            //}

            Send_Email_1();
        }

        private void Email_Send_Load(object sender, EventArgs e)
        {

        }
        private void Send_Email_1()
        {

            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtpout.secureserver.net");

                mail.From = new MailAddress("protitletaxsale@drnds.com");
                mail.To.Add("niranjanmurthy@drnds.com");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail";

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("protitletaxsale@drnds.com", "AecXmC9T07DcP$");
               // SmtpServer.EnableSsl = true;

               
                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            
            {

                MessageBox.Show(ex.ToString());

            }
        
        }
     
    }
}
