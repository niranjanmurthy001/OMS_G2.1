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
    public partial class Client_Proposal_Auto_Send : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        InfiniteProgressBar.clsProgress loader = new InfiniteProgressBar.clsProgress();
        string Path1, Export_Title;

        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        System.Data.DataTable dtuser = new System.Data.DataTable();
        int User_id, check = 0;
        static int currentpageindex = 0;
        int pagesize = 300;
        int Index;
        int Proposal_type;
        string Directory_Path;
        public Client_Proposal_Auto_Send()
        {
            InitializeComponent();
        }
        private void Bind_Proposal_Not_send()
        {
        
                ht.Clear(); ht.Clear();
                ht.Add("@Trans", "EMAIL_NOT_SEND_AUTO");
                ht.Add("@Proposal_From_Id",1);

                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);






                if (dt.Rows.Count > 0)
                {
                    grd_Proposal_Email.Rows.Clear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Proposal_Email.Rows.Add();
                        grd_Proposal_Email.Rows[i].Cells[1].Value = dt.Rows[i]["Proposal_Client_Id"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[2].Value = i + 1;
                        grd_Proposal_Email.Rows[i].Cells[3].Value = dt.Rows[i]["Proposal_From"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[4].Value = dt.Rows[i]["Client_Name"].ToString();
                        grd_Proposal_Email.Rows[i].Cells[5].Value = dt.Rows[i]["Email_Id"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[5].Value = dt.Rows[i]["State"].ToString();
                        //grd_Proposal_Email.Rows[i].Cells[6].Value = dt.Rows[i]["County"].ToString();

                        //grd_Proposal_Email.Rows[i].Cells[5].Value = "N/A";
                        grd_Proposal_Email.Rows[i].Cells[8].Value = "View";


                        if (dt.Rows[i]["Modified_by"].ToString() == null || dt.Rows[i]["Modified_by"].ToString() == "")
                        {
                            grd_Proposal_Email.Rows[i].Cells[9].Value = dt.Rows[i]["Inserted_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[10].Value = dt.Rows[i]["Inserted_Date"].ToString();
                        }
                        else
                        {
                            grd_Proposal_Email.Rows[i].Cells[11].Value = dt.Rows[i]["Modified_by"].ToString();
                            grd_Proposal_Email.Rows[i].Cells[12].Value = dt.Rows[i]["Modified_Date"].ToString();
                        }
                        Column12.Visible = false;
                        Column13.Visible = false;
               
                }
                
            }
           
        }
        private void btn_Send_All_Click(object sender, EventArgs e)
        {
            loader.startProgress();
            btn_Send_All.Enabled = false;

            if (grd_Proposal_Email.Rows.Count > 0)
            {

                for (int i = 0; i < grd_Proposal_Email.Rows.Count; i++)
                {
                    bool ischeck = (bool)grd_Proposal_Email[0, i].FormattedValue;
                    if (ischeck == true)
                    {
                        //sending mail
                        string client_name = grd_Proposal_Email.Rows[i].Cells[4].Value.ToString();
                        string client_id = grd_Proposal_Email.Rows[i].Cells[1].Value.ToString();
                        string emailid = grd_Proposal_Email.Rows[i].Cells[5].Value.ToString();
                        int proposal_clientid = int.Parse(grd_Proposal_Email.Rows[i].Cells[1].Value.ToString());

                        Send_Html_Email_Body(emailid, proposal_clientid,client_name);
                       // Ordermanagement_01.Client_Proposal.Sample email = new Ordermanagement_01.Client_Proposal.Sample(client_id, client_name, 1, emailid, proposal_clientid, "bulk", 2);

                        ////updating record in transaction table
                        //Hashtable htup = new Hashtable();
                        //DataTable dtup = new DataTable();
                        //htup.Add("@Trans", "INSERT_PROPOSAL_SEND");
                        //htup.Add("@Proposal_Client_Id", int.Parse(grd_Proposal_Email.Rows[i].Cells[1].Value.ToString()));
                        //htup.Add("@Inserted_by", User_id);
                        //dtup = dataaccess.ExecuteSP("Sp_Proposal_Client", htup);
                        //updating record in transaction table






                        //MessageBox.Show("Mail Sended Successfully");
                    }

                }

            }
            this.Close();

            System.Environment.Exit(1);


           
            
        }

        private void Client_Proposal_Auto_Send_Load(object sender, EventArgs e)
        {
            Proposal_type = 1;
            Bind_Proposal_Not_send();

            for (int i = 0; i < grd_Proposal_Email.Rows.Count; i++)
            {
                grd_Proposal_Email[0, i].Value = true;
            }

            //btn_Send_All_Click(sender, e);
        }



        public void Send_Html_Email_Body(string Email_Id, int Proposal_clientid,string Client_Name)
        {
            using (MailMessage mm = new MailMessage())
            {
                //try
                //{

                    mm.IsBodyHtml = true;



                    string body = this.PopulateBody(Client_Name);
                    SendHtmlFormattedEmail("netco@drnds.com", "Sample", body, Email_Id, Proposal_clientid,Client_Name);

                  

                //}
                //catch (Exception error)
                //{
                //    MessageBox.Show(error.Message);
                //    return;

                //}
            }

        }

        public string PopulateBody(string Client_Name)
        {
            string body = string.Empty;




            if (Proposal_type == 1)
            {


                Directory_Path = @"\\192.168.12.33\Oms Email Templates\Proposal_Email.htm";

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


            body = body.Replace("{Client-Name}", Client_Name);

            return body;
        }


        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body,string EMAIL_ID,int PROPOSAL_CLIENT_ID,string CLIENT_NAME)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
                try
                {
                    Hashtable htemail = new Hashtable();
                    DataTable dtemail = new DataTable();
                    htemail.Add("@Trans", "SELECT_EMAILPASS");
                    htemail.Add("@Proposal_From_Id", 1);
                    dtemail = dataaccess.ExecuteSP("Sp_Proposal_Client", htemail);


                    Hashtable htpath = new Hashtable();
                    DataTable dtpath = new DataTable();
                    htpath.Add("@Trans", "SELECT_TRUE_ATTACH");
                    htpath.Add("@Proposal_From_Id", 1);


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
                    mailMessage.To.Add(EMAIL_ID);

                    if (Proposal_type == 1)
                    {
                        mailMessage.CC.Add("sales@abstractshop.com");
                    }
                    else if (Proposal_type == 2)
                    {

                        mailMessage.Bcc.Add("sales@abstractshop.com");
                    }

                    // mailMessage.CC.Add("niranjanmurthy@drnds.com");
                    //mailMessage.CC.Add("Dineshkumar.b@drnds.com");
                    // mailMessage.CC.Add("naveen@drnds.com");

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
                    smtp.Port = 80;
                    //string userState = "test message1";
                    smtp.Send(mailMessage);
                    smtp.Dispose();


                    Hashtable htchk = new Hashtable();
                    DataTable dtchk = new DataTable();
                    htchk.Add("@Trans", "CHECK_PROPOSAL_SEND");
                    htchk.Add("@Proposal_Client_Id", PROPOSAL_CLIENT_ID);
                    htchk.Add("@Proposal_From_Id", 1);
                    dtchk = dataaccess.ExecuteSP("Sp_Proposal_Client", htchk);
                    if (dtchk.Rows.Count > 0)
                    {
                        //update
                        Hashtable htup = new Hashtable();
                        DataTable dtup = new DataTable();
                        htup.Add("@Trans", "UPDATE_PROPOSAL_SEND");
                        htup.Add("@Proposal_Client_Id", PROPOSAL_CLIENT_ID);
                        htup.Add("@Proposal_From_Id", 1);
                        htup.Add("@Modified_by", 1);
                        dtup = dataaccess.ExecuteSP("Sp_Proposal_Client", htup);
                    }
                    else
                    {
                        //insert
                        Hashtable htin = new Hashtable();
                        DataTable dtin = new DataTable();
                        htin.Add("@Trans", "INSERT_PROPOSAL_SEND");
                        htin.Add("@Proposal_Client_Id", PROPOSAL_CLIENT_ID);
                        htin.Add("@Proposal_From_Id", 1);
                        htin.Add("@Inserted_by", 1);
                        dtin = dataaccess.ExecuteSP("Sp_Proposal_Client", htin);
                    }


                    //Propoasal Email Send History
                    Hashtable hthistory = new Hashtable();
                    DataTable dthistory = new DataTable();
                    hthistory.Add("@Trans", "INSERT_SEND_HISTORY");
                    hthistory.Add("@Proposal_Client_Id", PROPOSAL_CLIENT_ID);
                    hthistory.Add("@Proposal_From_Id", 1);
                    hthistory.Add("@Last_Send_By", 1);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hashtable htgethour = new Hashtable();
            DataTable dtgethour = new DataTable();
            htgethour.Add("@Trans", "GET_HOUR");
            int hour;
            dtgethour = dataaccess.ExecuteSP("Sp_Proposal_Client",htgethour);
            if (dtgethour.Rows.Count > 0)
            {
                hour = int.Parse(dtgethour.Rows[0]["hour"].ToString());

            }
            else
            {

                hour = 0;
            }

            if (hour == 18)
            {
                Proposal_type = 1;
                Bind_Proposal_Not_send();


                for (int i = 0; i < grd_Proposal_Email.Rows.Count; i++)
                {
                    grd_Proposal_Email[0, i].Value = true;
                }

                btn_Send_All_Click(sender, e);
            }
        }
    }
}
