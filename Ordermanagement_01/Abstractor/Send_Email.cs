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
using System.Configuration;
namespace Ordermanagement_01.Abstractor
{
    public partial class Send_Email : Form
    {
    
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();


        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        int Order_Id;
        string Email, Alternative_Email;
        string Client_Order_no, DEED_CHAIN,Order_number;
        int Order_Type;
        int abstarctor_id;
        int User_Id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        string Report_Name;
        string Instructions,Emailid;
        string Copy_Type, DocRetrivalNotes,Order_Prior_Date;
        int Ab_Order_Email_Number;
        public Send_Email(int ORDER_ID, int ABSTRACTOR_ID, string CLIENT_ORDERNO, int ORDER_TYPE_ID, string EMAIL, string ALTERNATIVE_EMAIL, int USER_ID, string deedchain, string emailid)
        {
            Order_Id = ORDER_ID;
            abstarctor_id = ABSTRACTOR_ID;
            User_Id = USER_ID;
            Emailid = emailid;
            DEED_CHAIN = deedchain;
            //Client_Order_no = CLIENT_ORDERNO.ToString();
            Order_Type = ORDER_TYPE_ID;
            Email = EMAIL.ToString();
            Alternative_Email = ALTERNATIVE_EMAIL.ToString();
            InitializeComponent();
            Export_Report();
         
            Send_Html_Email_Body();
            
            //Send_Email email = new Send_Email(Order_Id, abstarctor_id, Client_Order_no, Order_Type, Email);
            //email.Close();
        }

        public void get_Order_Info()
        {


            Hashtable htorder = new Hashtable();
            DataTable dtorder = new DataTable();
            htorder.Add("@Order_ID", Order_Id);
            htorder.Add("@Abstractor_Id", abstarctor_id);
            dtorder = dataaccess.ExecuteSP("Sp_Rpt_Abstractor_Order",htorder);
            if (dtorder.Rows.Count > 0)
            {

                Copy_Type = dtorder.Rows[0]["Copy_Type"].ToString();
                DocRetrivalNotes = dtorder.Rows[0]["Abstractor_Note"].ToString();
                Order_Prior_Date = dtorder.Rows[0]["Order_Prior_Date"].ToString();
                if (DEED_CHAIN == "Yes")
                {
                    Report_Name = dtorder.Rows[0]["Order_Type"].ToString() + " Deed Chain Request Form";
                }
               
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

            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }

            foreach (ReportDocument sr in rptDoc.Subreports)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in sr.Database.Tables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);

                }
            }
        }
        private void btn_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox_Attachment.Text = dlg.FileName.ToString();
            }
        }

        public void Export_Report()
        {
            if (DEED_CHAIN == "Yes")
            {
                rptDoc = new Abstractor_Reports.Abstract_Order_Search_Deed_Chain();
                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_ID", Order_Id);
                rptDoc.SetParameterValue("@Abstractor_Id", abstarctor_id);
                rptDoc.SetParameterValue("@Abstractor_Cost", "False");
                if (Emailid == "vendors@drnds.com")
                {
                    rptDoc.SetParameterValue("@Trans", "DRN");
                }
                else if (Emailid == "neworders@abstractshop.com")
                {
                    rptDoc.SetParameterValue("@Trans", "ABS");
                }
                crViewer.ReportSource = rptDoc;

          
            }
            else if (DEED_CHAIN == "No")
            {
        


                if (Order_Type == 1 || Order_Type == 5)
                {

                    rptDoc = new Abstractor_Reports.Current_Owner_Search();
                    Report_Name = "Current Owner Search";



                }
                else if (Order_Type == 29)
                {
                    rptDoc = new Abstractor_Reports.Two_Owner_Search();

                    Report_Name = "Two Owner Search";
                }
                else if (Order_Type == 30)
                {
                    rptDoc = new Abstractor_Reports.ThirtyYears_Search();

                    Report_Name = "Thirty Years Search";
                }
                else if (Order_Type == 36)
                {
                    rptDoc = new Abstractor_Reports.Full_Search();

                    Report_Name = "Full Search";
                }
                else if (Order_Type == 7)
                {
                    rptDoc = new Abstractor_Reports.Update_Search();
                    Report_Name = "Update Search";
                }
                else if (Order_Type == 21)
                {
                    rptDoc = new Abstractor_Reports.DocumentRetrival();
                    Report_Name = "Document Retrival";
                }
                else if (Order_Type == 43)
                {
                    rptDoc = new Abstractor_Reports.Twenty_Year_Search();
                    Report_Name = "Twenty Years Search";
                }
                else if (Order_Type == 49)
                {
                    rptDoc = new Abstractor_Reports.Fifty_Year_Search();
                    Report_Name = "Fifty Years Search";
                }
                else if (Order_Type == 70)
                {
                    rptDoc = new Abstractor_Reports.Current_Owner_Deed_Chain();
                    Report_Name = "Current Owner Deed Chain";

                }
                else if (Order_Type == 71)
                {
                    rptDoc = new Abstractor_Reports.Two_Owner_DeadChain();
                    Report_Name = "Two Owner Deed Chain";

                }
                else if (Order_Type == 38)
                {
                    rptDoc = new Abstractor_Reports.FourtyYearSearch();
                    Report_Name = "40 Years Search";

                }
                else if (Order_Type == 72)
                {
                    rptDoc = new Abstractor_Reports.HundredYearSearch();
                    Report_Name = "100 Years Search";

                }
                else if (Order_Type == 32)
                {
                    rptDoc = new Abstractor_Reports.Mortgage_Assessment_Search();
                    Report_Name = "Mortgage and Assignment";

                }
                else if (Order_Type == 73)
                {
                    rptDoc = new Abstractor_Reports.Full_Owner_Deed_Chain();
                    Report_Name = "Full Owner Deed Chain";

                }
                else if (Order_Type == 76)
                {
                    rptDoc = new Abstractor_Reports.Current_Owner_Search_Report();
                    Report_Name = "Current Owner Report";

                }
                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_ID", Order_Id);
                rptDoc.SetParameterValue("@Abstractor_Id", abstarctor_id);
                rptDoc.SetParameterValue("@Abstractor_Cost", "False");
                if (Emailid == "vendors@drnds.com")
                {
                    rptDoc.SetParameterValue("@Trans", "DRN");
                }
                else if (Emailid == "neworders@abstractshop.com")
                {
                    rptDoc.SetParameterValue("@Trans", "ABS");
                }
                
            }

            
            

          
            ExportOptions CrExportOptions;
            FileInfo newFile = new FileInfo(@"\\192.168.12.33\oms-reports\AbstractorReport.pdf");
            

       

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


        

     

      

      


        public void Search_Instructions()
        {
            get_Order_Info();

            if (DEED_CHAIN == "No")
            {

                if (Order_Type == 1 || Order_Type == 5)//Current Owner Search
                {
                    Instructions = "<p><font size=3 face='Century Gothic'><b>Search Instructions:</b></font></p>" +
                    "<ol> <font size=2.5 face='Century Gothic'><li>Please provide the all Deeds chain (The Oldest Deed in chain must be Full-Value Deed (100% conveying) Please Note:</li>" +
                    "<ol style='list-style:none'><li>Foreclosure transactions and apparent family transactions such as gift deeds, quit claim deeds, interfamily transfers, and probate transfers do not meet the standard of a FVD. Transactions where no value or partial value was paid for the property do not meet the FVD definition.</li></ol>" +
                        "<li>The copies of all Taxes and Assessment, Open DOT/Mortgages (<b>" + Copy_Type + " Copies including Notary and all Riders</b>), Deeds, Judgments and liens, Plat Map.</li>" +
                        "<li>If no DOT/Mortgage, provide us the last DOT satisfaction copies for our review along with the DOT.</li>" +
                        "<li>If property is foreclosed please provide the foreclosed documents.</li>" +
                    "<li>We require at least a 24 Month Chain of Title included</li> </font></ol>";
                }
                else if (Order_Type == 21)//Document Retrieval
                {
                    Instructions = "<p><font size=3 face='Century Gothic'><b>Please note:<b></font></p>" +
                    "<p><font size=2.5 face='Century Gothic'>We need the below document Retrieved. Kindly provide us only the <b> " + Copy_Type + " Copies </b> Only.</font></p>" +
                    "<p><font size=2.5 face='Century Gothic'><b>" + DocRetrivalNotes + "</b></font></p>";
                }
                else if (Order_Type == 7)//Update Search
                {
                    Instructions = "<font size=3 face='Century Gothic'><b>Search Instructions:</b></font>" +
                        "<ol><font size=2.5 face='Century Gothic'><li>Please complete update/bring down from to <b>" + Order_Prior_Date + " </b> current.</li>" +
                        "<li>Please provide copies of any new documents recorded.</li><li>If any open mortgages found please provide the <b> " + Copy_Type + " copies </b> (with notary and rider).</li></font></ol>";
                }
                else if (Order_Type == 76)//Current Owner Search Report
                {
                    Instructions = "<font size=3 face='Century Gothic'><b>Search Instructions:</b></font>" +
                        "<font size=3 face='Century Gothic'>KINDLY OPEN THE WORD DOCUMENT FOR SEARCH REQUIREMENTS</font>" +
                        "<font size=3 face='Century Gothic'>REACH US IF YOU NEED ANY FURTHER INFORMATION.</font>";
                       
                }
                else if (Order_Type == 29)
                {
                    Instructions = "";
                }
                else if (Order_Type == 30)
                {
                    Instructions = "";
                }
                else if (Order_Type == 36)
                {
                    Instructions = "";
                }
            }
                else if(DEED_CHAIN=="Yes")
            {
           
                Instructions = "<p><font size=3 face='Century Gothic'><b>Search Instructions:</b></font></p>" +
                "<ol> <font size=2.5 face='Century Gothic'><li>Please provide the all Deeds chain (The Oldest Deed in chain must be Full-Value Deed (100% conveying) Please Note:</li>" +
                "<ol style='list-style:none'><li>Foreclosure transactions and apparent family transactions such as gift deeds, quit claim deeds, interfamily transfers, and probate transfers do not meet the standard of a FVD. Transactions where no value or partial value was paid for the property do not meet the FVD definition.</li></ol>";
          
                
                }
        }


        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {

                  

                    Hashtable htorder = new Hashtable();
                    DataTable dtorder = new DataTable();
                    htorder.Add("@Order_ID", Order_Id);
                    htorder.Add("@Abstractor_Id", abstarctor_id);
                    dtorder = dataaccess.ExecuteSP("Sp_Rpt_Abstractor_Order", htorder);
                    if (dtorder.Rows.Count > 0)
                    {


                    }
                    Search_Instructions();
                    

               
                    mm.IsBodyHtml = true;
                    if (Emailid == "vendors@drnds.com")
                    {
                        Order_number = "DRN-" + Convert.ToString(dtorder.Rows[0]["Order_Id"].ToString());
                        string body = this.PopulateBody(Order_number, dtorder.Rows[0]["Order_Type_New"].ToString(), dtorder.Rows[0]["Borrower_Name"].ToString(), dtorder.Rows[0]["Address"].ToString(), dtorder.Rows[0]["County"].ToString(), Instructions);
                        Client_Order_no = Order_number;
                        SendHtmlFormattedEmail("vendors@drnds.com", "Sample", body);
                        
                    }
                    else if (Emailid == "neworders@abstractshop.com")
                    {
                        Order_number = "ABS-" + Convert.ToString(dtorder.Rows[0]["Order_Id"].ToString());
                        string body = this.PopulateBody(Order_number, dtorder.Rows[0]["Order_Type_New"].ToString(), dtorder.Rows[0]["Borrower_Name"].ToString(), dtorder.Rows[0]["Address"].ToString(), dtorder.Rows[0]["County"].ToString(), Instructions);
                        Client_Order_no = Order_number;
                        SendHtmlFormattedEmail("vendors@drnds.com", "Sample", body);
                        
                    }
                    
                    
                    




                    Assign_Orders_ToAbstractor();
                    Update_Abstractor_Order_Status();



                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }

        }
        public string PopulateBody(string Order_Number, string OrderType, string OwnerName, string Property_Address, string County, string Instructions)
        {
            string body = string.Empty;
            
            if (Emailid == "vendors@drnds.com")
            {
                StreamReader reader = new StreamReader(@"\\192.168.12.33\Oms Email Templates\SendEmail.htm");

                body = reader.ReadToEnd();
            }
            else if(Emailid=="neworders@abstractshop.com")
            {
             
                StreamReader reader1 = new StreamReader(@"\\192.168.12.33\Oms Email Templates\SendEmail_Abs.htm");
                body = reader1.ReadToEnd();
            }
            
          
           
                body = body.Replace("{Order_Number}", Order_Number);
           
         
            if (DEED_CHAIN == "No")
            {
                body = body.Replace("{OrderType}", OrderType);
            }
            else if (DEED_CHAIN == "Yes")
            {
                body = body.Replace("{OrderType}", Report_Name);


            }
            body = body.Replace("{OwnerName}", OwnerName);
            body = body.Replace("{Property_Address}", Property_Address);
            body = body.Replace("{County}", County);
            body = body.Replace("{Instructions}", Instructions);
           
            return body;
        }
        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {

                if (Emailid == "vendors@drnds.com")
                {
                    mailMessage.From = new MailAddress("vendors@drnds.com");
                    //mailMessage.From = new MailAddress("vendors@drnds.net");
                }
                else if (Emailid == "neworders@abstractshop.com")
                {
                    mailMessage.From = new MailAddress("neworders@abstractshop.com");
                }
                

                string Path1 = @"\\192.168.12.33\oms-reports\AbstractorReport.pdf";

                string Typing_Template = @"\\192.168.12.33\abstractor-documents\Abstractor Order Typing Format.docx";

                MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));
             
                string Attachment_Name = Report_Name.ToString() + '-' + Client_Order_no.ToString() + ".pdf";

                mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));

                //Current Owener Search Report Adding Abstractor Orer Typing Format
                MemoryStream ms1 = new MemoryStream();
                if (Order_Type == 76)
                {

                    ms1 = new MemoryStream(File.ReadAllBytes(Typing_Template));
                    string Attachment_Typeing_Name = "Abstractor Order Typing Format.docx";
                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms1, Attachment_Typeing_Name.ToString()));
                }


                mailMessage.To.Add(Email);
                if (Emailid == "vendors@drnds.com")
                {
                    mailMessage.To.Add("vendors@drnds.com");
                  
                }
                else if (Emailid == "neworders@abstractshop.com")
                {
                    mailMessage.To.Add("neworders@abstractshop.com");
                }


                if (Alternative_Email != "")
                {

                    mailMessage.To.Add(Alternative_Email);
                }

                //mailMessage.To.Add("niranjanmurthy@drnds.com");

                txt_Subject.Text = "New Search Request - " + Client_Order_no + "-" + Report_Name.ToString();
                mailMessage.Subject = txt_Subject.Text;

                StringBuilder sb = new StringBuilder();
                sb.Append("Subject: " + txt_Subject.ToString() + "" + Environment.NewLine);



               
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

             

                SmtpClient smtp = new SmtpClient();

             

                if (Emailid == "vendors@drnds.com")
                {
                    smtp.Host = "smtpout.secureserver.net";
                    //NetworkCredential NetworkCred = new NetworkCredential("vendors@drnds.net", "HsPr7S09FT$");
                    NetworkCredential NetworkCred = new NetworkCredential("vendors@drnds.com", "AecXmC9T07DcP$");
                    smtp.UseDefaultCredentials= true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25;
                }
                else if (Emailid == "neworders@abstractshop.com")
                {
                    smtp.Host = "smtpout.secureserver.net";
                    NetworkCredential NetworkCred1 = new NetworkCredential("neworders@abstractshop.com", "DinNavABS");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred1;
                    smtp.Port = 80;
                }

                Hashtable ht_GetMax_Ab_Order_EmailNum = new Hashtable();
                DataTable dt_GetMax_Ab_Order_EmailNum = new DataTable();

                ht_GetMax_Ab_Order_EmailNum.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
                ht_GetMax_Ab_Order_EmailNum.Add("@Order_Id", Order_Id);
                dt_GetMax_Ab_Order_EmailNum = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_EmailNum);

                if (dt_GetMax_Ab_Order_EmailNum.Rows.Count > 0)
                {

                    Ab_Order_Email_Number = int.Parse(dt_GetMax_Ab_Order_EmailNum.Rows[0]["Ab_Order_Email_Number"].ToString());
                }

                Hashtable ht_Insert = new Hashtable();
                DataTable dt_Insert = new DataTable();
                ht_Insert.Add("@Trans", "INSERT");
                ht_Insert.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
                ht_Insert.Add("@Order_Id", Order_Id);
                ht_Insert.Add("@Abstractor_Id", abstarctor_id);
                ht_Insert.Add("@To", Email);
                ht_Insert.Add("@From", "vendors@drnds.com");
                ht_Insert.Add("@Cc", Emailid);
                ht_Insert.Add("@Subject", txt_Subject.Text);
                ht_Insert.Add("@Message", body.ToString());
                ht_Insert.Add("@Send_By", User_Id);
                ht_Insert.Add("@status", "True");
                dt_Insert = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", ht_Insert);

                //Use for Test purpose

                //if (Emailid == "vendors@drnds.com")
                //{
                //    NetworkCredential NetworkCred = new NetworkCredential("techteam@drnds.com", "nop539");
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Credentials = NetworkCred;
                //    smtp.Port = 80;
                //}
                //else if (Emailid == "neworders@abstractshop.com")
                //{
                //    NetworkCredential NetworkCred1 = new NetworkCredential("techteam@drnds.com", "nop539");
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Credentials = NetworkCred1;
                //    smtp.Port = 80;
                //}

                
               smtp.Send(mailMessage);


            
          






            }
        }

     
        public void Assign_Orders_ToAbstractor()
        {
            Hashtable htinsertrec = new Hashtable();
            DataTable dtinsertrec = new System.Data.DataTable();
            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            string time = date.ToString("hh:mm tt");

            htinsertrec.Add("@Trans", "INSERT");
            htinsertrec.Add("@Order_Id", Order_Id);
            htinsertrec.Add("@Abstractor_Id", abstarctor_id);
            htinsertrec.Add("@Order_Status_Id", 2);
            htinsertrec.Add("@Order_Progress_Id", 6);
            htinsertrec.Add("@Assigned_Date", dateeval);
            htinsertrec.Add("@Assigned_By", User_Id);
            htinsertrec.Add("@Inserted_By", User_Id);
            htinsertrec.Add("@Inserted_date", date);
            htinsertrec.Add("@status", "True");
            if (Emailid == "vendors@drnds.com")
            {
                htinsertrec.Add("@Email_Id", "DRN");
            }
            else if (Emailid == "neworders@abstractshop.com")
            {
                htinsertrec.Add("@Email_Id", "ABS");
            }
            dtinsertrec = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", htinsertrec);

            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new System.Data.DataTable();
            htupdate.Add("@Trans", "UPDATE_STATUS");
            htupdate.Add("@Order_ID", Order_Id);
            htupdate.Add("@Order_Status", 17);
            htupdate.Add("@Modified_By", User_Id);
            htupdate.Add("@Modified_Date", date);
            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

            Hashtable htprogress = new Hashtable();
            DataTable dtprogress = new System.Data.DataTable();
            htprogress.Add("@Trans", "UPDATE_PROGRESS");
            htprogress.Add("@Order_ID", Order_Id);
            htprogress.Add("@Order_Progress", 6);
            htprogress.Add("@Modified_By", User_Id);
            htprogress.Add("@Modified_Date", date);
            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



        }

        public void Update_Abstractor_Order_Status()
        {

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id", Order_Id);

            dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Order_Status", htcheck);
            int Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            if (Check == 0)
            {
                Hashtable htinsertrec = new Hashtable();
                DataTable dtinsertrec = new System.Data.DataTable();
                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htinsertrec.Add("@Trans", "INSERT");
                htinsertrec.Add("@Order_Id", Order_Id);
                htinsertrec.Add("@Abstractor_Status", 2);
                htinsertrec.Add("@Abstractor_Progress", 6);
                htinsertrec.Add("@Inserted_By", User_Id);
                htinsertrec.Add("@Inserted_date", date);
                htinsertrec.Add("@status", "True");
                dtinsertrec = dataaccess.ExecuteSP("Sp_Abstractor_Order_Status", htinsertrec);
            }




        }

        private void Send_Email_Load(object sender, EventArgs e)
        {

        }

   
     

        

    
    }
}
