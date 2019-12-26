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
    public partial class Sample : Form
    {
        string Directory_Path;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        int userid, Proposal_clientid, Proposal_type; string clientname, clientid, emailid, Errormsg, Bulk;
        public Sample(string Clientid, string Clientname, int Userid, string Emailid, int proposal_clientid,string bulk,int proposal_Type)
        {
            InitializeComponent();
            Bulk = bulk;
            clientid = Clientid;
            userid = Userid;
            clientname = Clientname;
            emailid = Emailid;
            Proposal_clientid = proposal_clientid;
            Proposal_type = proposal_Type;
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
                    SendHtmlFormattedEmail("netco@drnds.com", "Sample",body);

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




            if (Proposal_type == 1)
            {


                Directory_Path = @"\\192.168.12.33\Oms Email Templates\Abs_New_Proposal_Template_1.html";
            
            }
            else if (Proposal_type == 2)
            {

   

                Directory_Path = @"\\192.168.12.33\Oms Email Templates\PR247Final.htm";
               // Directory_Path = @"\prnew.html";
            }
            using (StreamReader reader = new StreamReader(Directory_Path))
            {

                body = reader.ReadToEnd();
            }



            body = body.Replace("{Client-Name}", clientname);

            return body;
        }


        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
                try
                {
                    Hashtable htemail = new Hashtable();
                    DataTable dtemail = new DataTable();
                    htemail.Add("@Trans", "SELECT_EMAILPASS");
                    htemail.Add("@Proposal_From_Id",Proposal_type );
                    dtemail = dataaccess.ExecuteSP("Sp_Proposal_Client", htemail);


                    Hashtable htpath = new Hashtable();
                    DataTable dtpath = new DataTable();
                    htpath.Add("@Trans", "SELECT_TRUE_ATTACH");
                    htpath.Add("@Proposal_From_Id", Proposal_type);


                    dtpath = dataaccess.ExecuteSP("Sp_Proposal_Client", htpath);


                    mailMessage.From = new MailAddress(dtemail.Rows[0]["Email_Id"].ToString());
                    if (dtpath.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtpath.Rows.Count; i++)
                        {
                          
                            // mailMessage.From = new MailAddress("techteam@drnds.com");
                            string Path1 = dtpath.Rows[i]["File_Path"].ToString();

                            MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));

                            string Attachment_Name = dtpath.Rows[i]["File_Name"].ToString();
                            mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));

                        }
                    }
                        mailMessage.To.Add(emailid);

                 
                        if (Proposal_type == 1)
                        {


                           // mailMessage.CC.Add("sales@abstractshop.com");
                         
                        }
                        else if (Proposal_type == 2)
                        {

                         //   mailMessage.CC.Add("sales@abstractshop.com");
                        }


                        if (Proposal_type == 1)
                        {
                            mailMessage.Subject = "ABSTRACT SHOP - Vendor Sign up - Title Search and Tax Certificate - All 50 States";
                        }
                        else if (Proposal_type == 2)
                        {
                            mailMessage.Subject = "Order Online Nationwide Title Search - Propertyreport247.com";
                        }

                   
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;



                        SmtpClient smtp = new SmtpClient();

                    

                        smtp.Host = "smtpout.secureserver.net";
                        NetworkCredential NetworkCred = new NetworkCredential(dtemail.Rows[0]["Email_Id"].ToString(), dtemail.Rows[0]["Password"].ToString());
                      //  NetworkCredential NetworkCred = new NetworkCredential("info@propertyreport247.com", "info247");
                        // NetworkCredential NetworkCred = new NetworkCredential("techteam@drnds.com","nop539");k
                        smtp.UseDefaultCredentials = true;
                      //  smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                        // smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000
                        smtp.Timeout = (60 * 5 * 1000);
                        smtp.Credentials = NetworkCred;
                     //   smtp.EnableSsl = false;
                        smtp.Port = 3535;
                        //string userState = "test message1";
                        smtp.Send(mailMessage);
                        smtp.Dispose();


                        Hashtable htchk = new Hashtable();
                        DataTable dtchk = new DataTable();
                        htchk.Add("@Trans", "CHECK_PROPOSAL_SEND");
                        htchk.Add("@Proposal_Client_Id", Proposal_clientid);
                        htchk.Add("@Proposal_From_Id", Proposal_type);
                        dtchk = dataaccess.ExecuteSP("Sp_Proposal_Client", htchk);
                        if (dtchk.Rows.Count > 0)
                        {
                            //update
                            Hashtable htup = new Hashtable();
                            DataTable dtup = new DataTable();
                            htup.Add("@Trans", "UPDATE_PROPOSAL_SEND");
                            htup.Add("@Proposal_Client_Id", Proposal_clientid);
                            htup.Add("@Proposal_From_Id", Proposal_type);
                            htup.Add("@Modified_by", userid);
                            dtup = dataaccess.ExecuteSP("Sp_Proposal_Client", htup);
                        }
                        else
                        {
                            //insert
                            Hashtable htin = new Hashtable();
                            DataTable dtin = new DataTable();
                            htin.Add("@Trans", "INSERT_PROPOSAL_SEND");
                            htin.Add("@Proposal_Client_Id", Proposal_clientid);
                            htin.Add("@Proposal_From_Id", Proposal_type);
                            htin.Add("@Inserted_by", userid);
                            dtin = dataaccess.ExecuteSP("Sp_Proposal_Client", htin);
                        }


                        //Propoasal Email Send History
                        Hashtable hthistory = new Hashtable();
                        DataTable dthistory = new DataTable();
                        hthistory.Add("@Trans", "INSERT_SEND_HISTORY");
                        hthistory.Add("@Proposal_Client_Id", Proposal_clientid);
                        hthistory.Add("@Proposal_From_Id", Proposal_type);
                        hthistory.Add("@Last_Send_By", userid);
                        dthistory = dataaccess.ExecuteSP("Sp_Proposal_Client", hthistory);

                        ////updating record in transaction table
                        //Hashtable htup = new Hashtable();
                        //DataTable dtup = new DataTable();
                        //htup.Add("@Trans", "INSERT_PROPOSAL_SEND");
                        //htup.Add("@Proposal_Client_Id", Proposal_clientid);

                        //htup.Add("@Inserted_by", userid);
                        //dtup = dataaccess.ExecuteSP("Sp_Proposal_Client", htup);

                        //  MessageBox.Show("Mail Sent Successfully");

                        // }

                        //   }

                    
                }
                catch (Exception ex)
                {

                    return;
                   
                }

            }
        }

        


      
        
        private void button1_Click(object sender, EventArgs e)
        {
            Send_Html_Email_Body();
        }

        private void Sample_Load(object sender, EventArgs e)
        {
        
        }
    }
}
