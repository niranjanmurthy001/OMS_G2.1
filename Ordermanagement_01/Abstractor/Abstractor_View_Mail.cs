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
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;
using MarkupConverter;
using System.Web;
namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_View_Mail : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        public Abstractor_View_Mail()
        {
            InitializeComponent();
        }
        string bodys;
        string real,Real2;
        string Exact;
        Classes.HtmlToText HtmlToText = new Classes.HtmlToText();
      
    private string ReturnString;
        private void Abstractor_View_Mail_Load(object sender, EventArgs e)
        {
            this.Read_Emails();
           
        }
        private void Read_Emails()
        {
               Pop3Client pop3Client;
           
                pop3Client = new Pop3Client();
                pop3Client.Connect("pop.asia.secureserver.net", 995, true);

                pop3Client.Authenticate("sgratman@abstractshop.com", "Titlereport247", AuthenticationMethod.UsernameAndPassword);

                int count = pop3Client.GetMessageCount();
                DataTable dtMessages = new DataTable();
              
                dtMessages.Columns.Add("MessageNumber");
                dtMessages.Columns.Add("TO");
                dtMessages.Columns.Add("From");
                dtMessages.Columns.Add("Subject");
                dtMessages.Columns.Add("DateSent");
            


                int counter = 0;
                for (int i = count; i >= 1; i--)
                {

                    OpenPop.Mime.Header.MessageHeader msgheader = pop3Client.GetMessageHeaders(i);
                  
                    string From = msgheader.From.ToString();
                    if (From == "sgratman@abstractshop.com")
                    {
                        string subject = msgheader.Subject.ToString();
                        if (From == "Mail Delivery System")
                        {




                            //message = pop3Client.GetMessage(i);
                        }
                        else
                        {
                            // OpenPop.Mime.Message message = pop3Client.GetMessage(i);
                            dtMessages.Rows.Add();
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageNumber"] = i;
                      
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["From"] = msgheader.From.DisplayName;
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["Subject"] = msgheader.Subject;
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["DateSent"] = msgheader.DateSent;
                          
                            counter++;
                            if (counter > 1)
                            {
                                break;
                            }
                        }
                    }
                }
                dataGridView1.DataSource =dtMessages;
           
        }


        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {

                    mm.From = new MailAddress("niranjanmurthy@drnds.com");




                   // mm.To.Add(Email);
                    mm.To.Add("niranjanmurthy@drnds.com");
                    //if (Alternative_Email != "")
                    //{

                    //    mm.To.Add(Alternative_Email);
                    //}
                  //  txt_Subject.Text = "New Search Request - " + Client_Order_no + "-" + Report_Name.ToString();
                    mm.Subject = "txt_Subject.Text";

                    StringBuilder sb = new StringBuilder();
                    //sb.Append("Subject: " + txt_Subject.ToString() + "" + Environment.NewLine);


                    //Hashtable htorder = new Hashtable();
                    //DataTable dtorder = new DataTable();
                    //htorder.Add("@Order_ID", Order_Id);
                    //htorder.Add("@Abstractor_Id", abstarctor_id);
                    //dtorder = dataaccess.ExecuteSP("Sp_Rpt_Abstractor_Order", htorder);
                    //if (dtorder.Rows.Count > 0)
                    //{


                    //}
                    //Search_Instructions();
                    string Path1 = @"\\192.168.12.33\oms-reports\AbstractorReport.pdf";


                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));

                    //string Attachment_Name = Report_Name.ToString() + '-' + Client_Order_no.ToString() + ".pdf";

                    //mm.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));


                    LinkedResource imagelink = new LinkedResource(@"\\192.168.12.33\Oms-Image-Files\Drn_Email_Logo.png", "image/png");
                    imagelink.ContentId = "imageId";
                    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                    mm.IsBodyHtml = true;

                 

                    mm.Body = "<p><font size=2.5 face='Century Gothic'>Hello</font> <br /><br /> <font size=2.5 face='Century Gothic'>Please consider this email as formal request to provide the title search report as per the order instruction in the attachment.<br/>Kindly acknowledge this email and provide us the ETA. </font> " +
"  <br /><br /> " +
"  <font size=2.5 face='Century Gothic'><b>Order Summary:</b></font> " +
  "<br /><br /> <font size=2.5 face='Century Gothic'><b>Order #.:</b>" + "Sample" + "</font>" +
  "<br /><font size=2.5 face='Century Gothic'><b>Order Type:</b> " + "Sample" + "</font>" +
 "<br /><font size=2.5 face='Century Gothic'><b>Owner Name:</b>" + "Sample" + "</font>" +
 "<br /><font size=2.5 face='Century Gothic'><b>Property Address:</b>" + "Sample" + "</font>" +
"<br /><font size=2.5 face='Century Gothic'><b>County:</b>" + "Sample" + "</font>" +
"<br />" + "Sample" + "" +
"<br /><br /><br /><ul><li><font size=2.5 face='Century Gothic'  >Send completed search packages to: <u>vendors@drnds.com.</u></font></li>" +
"</ul><br /><br /><font size=2.5 face='Century Gothic', CenturyGothic, AppleGothic, sans-serif>Thank you</font><br /><br />" +
"<font size=4 face='Monotype Corsiva'><b>Order Entry Team</b></font><br /> <img alt=\"\" hspace=0 src=\"cid:imageId\" align=baseline border=0 >" +
"<br /><font size=3  color= #0070C0  face='Century Gothic'><b>DRN Definite Solutions LLC</b></font><br />" +
"<font size=2.5 face=Calibri color=#1F497D><b>Corp: 3240 East State Street Ext Hamilton, NJ 08619</b></font><br />" +
"<font size=2.5 face=Calibri color=#1F497D><b>Direct: Office: 1- (443)-221-4551|Fax:(760)-280-6000</b> </font><br />" +
"<font size=3 face=Calibri><u>Vendors@drnds.com</u> | <u>www.DRNDS.com</u></font><br /><br /><br />" +
"<font size=2.5 face='Century Gothic'  > ----------------------------------------------------------------<br />" +
"-------------------------------------------------------------------------------------------------<br />" +
"-------------------------------------------------------------------------------------------------</font><br />" +
"<font size=2.5 face='Century Gothic'>This e-mail and any files transmitted with it are for the sole use of the intended </font>" +
"<font size=2.5 face='Century Gothic'>recipient(s) and may contain confidential and privileged information. If you are not the</font>" +
"<font size=2.5 face='Century Gothic'>intended recipient, please contact the sender by reply e-mail and destroy all copies of </font>" +
"<font size=2.5 face='Century Gothic'>the original message.  Any unauthorized review, use, disclosure, dissemination, </font>" +
"<font size=2.5 face='Century Gothic'>forwarding, printing or copying of this email or any action taken in reliance on this e-mail </font>" +
"<font size=2.5 face='Century Gothic'>is strictly prohibited and may be unlawful.</font><br/>" +
"<font size=2.5 face='Century Gothic'> ----------------------------------------------------------------<br />" +
"-------------------------------------------------------------------------------------------------<br />" +
"-------------------------------------------------------------------------------------------------</font>";

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mm.Body, null, "text/html");

                    htmlView.LinkedResources.Add(imagelink);
                    mm.AlternateViews.Add(htmlView);

                    SmtpClient smtp = new SmtpClient();
                    // smtp.Host = "smtp.gmail.com";
                    smtp.Host = "smtpout.secureserver.net";
                    // smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("niranjanmurthy@drnds.com", "123nir");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    //smtp.Port = 587;
                    smtp.Port = 80;
                    smtp.Send(mm);

                    //Assign_Orders_ToAbstractor();
                    //Update_Abstractor_Order_Status();



                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }

        }

        public void Send_New_Html_Email_Body()
        {

            using (MailMessage mm = new MailMessage())
            {
                try
                {

                    mm.From = new MailAddress("niranjanmurthy@drnds.com");




                    // mm.To.Add(Email);
                    mm.To.Add("niranjanmurthy@drnds.com");
                    //if (Alternative_Email != "")
                    //{

                    //    mm.To.Add(Alternative_Email);
                    //}
                    //  txt_Subject.Text = "New Search Request - " + Client_Order_no + "-" + Report_Name.ToString();
                    mm.Subject = "txt_Subject.Text";

                    StringBuilder sb = new StringBuilder();
                    //sb.Append("Subject: " + txt_Subject.ToString() + "" + Environment.NewLine);


                    Hashtable htorder = new Hashtable();
                    DataTable dtorder = new DataTable();
                    htorder.Add("@Order_ID", 10835);
                    htorder.Add("@Abstractor_Id", 1);
                    dtorder = dataaccess.ExecuteSP("Sp_Rpt_Abstractor_Order", htorder);
                    if (dtorder.Rows.Count > 0)
                    {


                    }
                    //Search_Instructions();
                    string Path1 = @"\\192.168.12.33\oms-reports\AbstractorReport.pdf";


                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));

                    //string Attachment_Name = Report_Name.ToString() + '-' + Client_Order_no.ToString() + ".pdf";

                    //mm.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));


                    LinkedResource imagelink = new LinkedResource(@"\\192.168.12.33\Oms-Image-Files\Drn_Email_Logo.png", "image/png");
                    imagelink.ContentId = "imageId";
                    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                    string Path = @"\\192.168.12.33\Oms-Image-Files\Drn_Email_Logo.png";
                    mm.IsBodyHtml = true;
                    string sample="ist sample inst";
                 string image_Withpath= "<img alt='' src="+Path+" />";
                 string body = this.PopulateBody(dtorder.Rows[0]["Client_Order_Number"].ToString(), dtorder.Rows[0]["Order_Type"].ToString(), dtorder.Rows[0]["Borrower_Name"].ToString(), dtorder.Rows[0]["Address"].ToString(), dtorder.Rows[0]["County"].ToString(), sample, image_Withpath);
                    SendHtmlFormattedEmail("niranjanmurthy@drnds.com","Sample",  body);





                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }


        }
        public string PopulateBody(string Order_Number, string OrderType, string OwnerName, string Property_Address, string County, string Instructions,string Image_Path)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\SendEmail.htm"))
            {
                
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Order_Number}", Order_Number);
            body = body.Replace("{OrderType}", OrderType);
            body = body.Replace("{OwnerName}", OwnerName);
            body = body.Replace("{Property_Address}", Property_Address);
            body = body.Replace("{County}", County);
            body = body.Replace("{Instructions}", Instructions);
            body = body.Replace("{Image}", Image_Path);
            return body;
        }
       

    
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          //  Pop3Client pop3Client;
           
          //  pop3Client = new Pop3Client();
          //  pop3Client.Connect("pop.asia.secureserver.net", 995, true);

          //  pop3Client.Authenticate("sgratman@abstractshop.com", "Titlereport247", AuthenticationMethod.UsernameAndPassword);

          //  int messageNumber = int.Parse( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
          //  OpenPop.Mime.Message message = pop3Client.GetMessage(messageNumber);
          ////  MessagePart messagePart = message.MessagePart.MessageParts[0];

          //  MessagePart body = message.FindFirstPlainTextVersion();

          //  richTextBox1.Text = "";
          //  if (body != null)
          //  {
          //       bodys = body.GetBodyAsText();

          //       richTextBox1.Text = bodys.ToString();
          //  }
          //  else
          //  {
          //     //real=   Regex.Replace(bodys, "<(.|\n)*?>", "");
          //      body = message.FindFirstHtmlVersion();
          //      bodys = body.GetBodyAsText();
              
          //      var webBrowser = new WebBrowser();
          //      webBrowser.CreateControl(); // only if needed
          //      webBrowser.DocumentText = bodys.ToString();
          //      while (webBrowser.DocumentText != bodys.ToString())

          //          Application.DoEvents();
          //      webBrowser.Document.ExecCommand("SelectAll", false, null);
          //      webBrowser.Document.ExecCommand("Copy", false, null);


          //      richTextBox1.Paste();
          //      richTextBox1.ReadOnly = true;





             
          //  }


           // richTextBox1.Text = Real2.ToString();
          //  richTextBox1.Text = bodys.ToString();

          
               // richTextBox1.Text = bodys.ToString();
            

        }

        private static string ConvertToText(string HTML)
        {
            string text = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            XmlDocument xsl = new XmlDocument();
            xmlDoc.LoadXml(@"\HTMLPage1.htm");
            xsl.CreateEntityReference("nbsp");
            xsl.Load(System.Web.HttpContext.Current.Server.MapPath("/Convert_Email.xslt"));

            //creating xsltD:\Niranjan\Ordermanagement_14.0\Ordermanagement_01\Convert_Email.xslt
            XslTransform xslt = new XslTransform();
            xslt.Load(xsl, null, null);

            //creating stringwriter
            StringWriter writer = new System.IO.StringWriter();

            //Transform the xml.
            xslt.Transform(xmlDoc, null, writer, null);

            //return string
            text = writer.ToString();
      string   Exact = text.ToString();
            writer.Close();
           
            return text;
        }
        private string StripHTML(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                Real2 = result.ToString();
                return result;
            }
            catch
            {
                MessageBox.Show("Error");
                return source;
            }
        }

        public void Send_New_Designed_EMail()
        {

            RTFBuilderbase sb = new RTFBuilder();

            BuilderCode(sb);



            this.richTextBox1.Rtf = sb.ToString();
        }

        private void BuilderCode(RTFBuilderbase sb)
        {
            sb.FontSize(20).Font(RTFFont.CenturyGothic).AppendLine("Hello:");
            sb.Append(Environment.NewLine);
            sb.FontSize(20).Font(RTFFont.CenturyGothic).AppendLine("Please consider this email as formal request to provide the title search report as per the order instruction in the attachment");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.FontSize(20).FontStyle(FontStyle.Bold).Font(RTFFont.CenturyGothic).AppendLine("Order Summary:");
            sb.Append(Environment.NewLine);

            sb.FontSize(20).FontStyle(FontStyle.Bold).Font(RTFFont.CenturyGothic).Append("Order # :").FontSize(20).Font(RTFFont.Candara).Append("Ni").AppendLine();
            sb.FontSize(20).FontStyle(FontStyle.Bold).Font(RTFFont.CenturyGothic).Append("Order Type :").FontSize(20).Font(RTFFont.Candara).Append("Ni").AppendLine();
            sb.FontSize(20).FontStyle(FontStyle.Bold).Font(RTFFont.CenturyGothic).Append("Property Address :").FontSize(20).Font(RTFFont.Candara).Append("Ni").AppendLine();
            sb.FontSize(20).FontStyle(FontStyle.Bold).Font(RTFFont.CenturyGothic).Append("County:").FontSize(20).Font(RTFFont.Candara).Append("Ni").AppendLine();

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("• Send completed search packages to: Searches@drnds.com." + Environment.NewLine);
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("• Production related clarifications email to:vendors@drnds.com." + Environment.NewLine);
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("• All invoices related queries email to VendorInvoices@drnds.com" + Environment.NewLine);

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("Thank you" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.InsertImage(Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\Drn_Email_Logo.png"));
            sb.FontSize(25).Font(RTFFont.Candara).AppendLine("Order Entry Team");
            sb.FontSize(25).Font(RTFFont.Candara).AppendLine("DRN Definite Solutions LLC");
            sb.FontSize(25).Font(RTFFont.Candara).AppendLine("Corp: 3240 East State Street Ext Hamilton, NJ 08619");
            sb.FontSize(25).Font(RTFFont.Candara).AppendLine("Direct: Office: 1- (443)-221-4551|Fax:(760)-280-6000");
            sb.FontSize(25).Font(RTFFont.Candara).AppendLine("Vendors@drnds.com | www.DRNDS.com");


            sb.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
            sb.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
            sb.Append("-------------------------------------------------------------------------" + Environment.NewLine);

            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("This e-mail and any files transmitted with it are for the sole use of the intended");
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("recipient(s) and may contain confidential and privileged information. If you are not the ");
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("intended recipient, please contact the sender by reply e-mail and destroy all copies of ");
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("the original message.  Any unauthorized review, use, disclosure, dissemination, ");
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("forwarding, printing or copying of this email or any action taken in reliance on this e-mail  ");
            sb.FontSize(20).Font(RTFFont.Candara).AppendLine("is strictly prohibited and may be unlawful. ");

            sb.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
            sb.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
            sb.Append("-------------------------------------------------------------------------" + Environment.NewLine);




        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Send_Html_Email_Body();
            Send_New_Html_Email_Body();
           
            //Send_Email_2();
           // Send_Email_3();
        }

        public void Send_Email_3()
        {

            //create the mail message
            MailMessage mail = new MailMessage();

            //set the addresses
            mail.From = new MailAddress("niranjanmurthy@drnds.com");
            mail.To.Add("niranjanmurthy@drnds.com");
            mail.Subject = "This is an email";
         //  mail.Body= "this is a sample body with html in it. <b>This is bold</b> <font color=#336699>This is blue</font>";
            mail.Body = richTextBox1.Text;
         
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, MediaTypeNames.Text.RichText);
            mail.AlternateViews.Add(htmlView);

           //richTextBox1.Rtf = mail.BodyRTF;
           // string Body = MarkupConverter.RtfToHtmlConverter(richTextBox1.Rtf);
         //   richTextBox1.Text = richTextBox1.Text;
//string myhtml = doParsing(mail.Body);
            // THIS IS WHAT YOU NEED
            //mail.IsBodyHtml = false;
          
         
            // after that use your SmtpClient code to send the email
            SmtpClient smtp = new SmtpClient();
            // smtp.Host = "smtp.gmail.com";
            smtp.Host = "smtpout.secureserver.net";
            // smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("niranjanmurthy@drnds.com", "123nir");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            //smtp.Port = 587;
            smtp.Port = 80;
            smtp.Send(mail);
        }
        public void Send_Email_2()
        {


            MailMessage message = new MailMessage(
                "niranjanmurthy@drnds.com",
                "niranjanmurthy@drnds.com");

            // Construct the alternate body as HTML. 
            //string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            //body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            //body += "</HEAD><BODY><DIV><FONT face=Arial color=#ff0000 size=2>this is some HTML text";
            //body += "</FONT></DIV></BODY></HTML>";

            message.Subject = "This is Testing Email";

            
            //message.IsBodyHtml = true; 
            //string body = "<!DOCTYPE HTML>";
            //body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=UTF-8\">";
            //body += "</HEAD><BODY><DIV><p><font size=2.5 face='Century Gothic'>Hello</font>";
            //body += "</DIV></BODY></HTML>";
         string   sHtml = "<HTML>\n" +
              "<HEAD>\n" +
              "<TITLE>Sample GIF</TITLE>\n" +
              "</HEAD>\n" +
              "<BODY><P>\n" +
              "<h1><Font Color=Green>Inline graphics</Font></h1></P>\n" +
              "</BODY>\n" +
              "</HTML>";



         message.Body = sHtml;

            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            // Add the alternate body to the message.

            AlternateView alternate = AlternateView.CreateAlternateViewFromString(sHtml, mimeType);
            message.AlternateViews.Add(alternate);

            // Send the message.
            //SmtpClient client = new SmtpClient(server);
            //client.Credentials = CredentialCache.DefaultNetworkCredentials;

            SmtpClient smtp = new SmtpClient();
            // smtp.Host = "smtp.gmail.com";
            smtp.Host = "smtpout.secureserver.net";
            // smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("niranjanmurthy@drnds.com", "123nir");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            //smtp.Port = 587;
            smtp.Port = 80;
            smtp.Send(message);
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("niranjanmurthy@drnds.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                
                mailMessage.To.Add(new MailAddress("niranjanmurthy@drnds.com"));
              
                SmtpClient smtp = new SmtpClient();
              
                smtp.Host = "smtpout.secureserver.net";
          
                NetworkCredential NetworkCred = new NetworkCredential("niranjanmurthy@drnds.com", "123nir");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
             
                smtp.Port = 80;
                smtp.Send(mailMessage);
             
            }
        }
        //method to send email to outlook
        public void sendEMailThroughOUTLOOK()
        {
            try
            {
                // Create the Outlook application.
                Outlook.Application oApp = new Outlook.Application();
                // Create a new mail item.
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                // Set HTMLBody. 
                //add the body of the email
                oMsg.HTMLBody = "Hello, Jawed your message body will go here!!";
                //Add an attachment.
                String sDisplayName = "MyAttachment";
                int iPosition = (int)oMsg.Body.Length + 1;
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                //now attached the file
              //  Outlook.Attachment oAttach = oMsg.Attachments.Add(@"C:\\fileName.jpg", iAttachType, iPosition, sDisplayName);
                //Subject line
                oMsg.Subject = "Your Subject will go here.";
                // Add a recipient.
                Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
                // Change the recipient in the next line if necessary.
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add("jawed.ace@gmail.com");
                oRecip.Resolve();
                // Send.
                oMsg.Send();
                // Clean up.
                oRecip = null;
                oRecips = null;
                oMsg = null;
                oApp = null;
            }//end of try block
            catch (Exception ex)
            {
            }//end of catch
        }//end of Email Method


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
    }
   
}
