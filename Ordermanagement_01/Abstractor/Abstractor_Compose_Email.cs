using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
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
using System.DirectoryServices;
using System.Diagnostics;





namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Compose_Email : Form
    {
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        string server = "192.168.12.33";
        string database = "TITLELOGY_VENDOR_TEST";
        string UserID = "sa";
        string password = "password1$";
        int Order_Id,Abstractor_Id;
        int Ab_Order_Email_Id;
        int Email_Attachment_Id;
        string[]  FName,destname;
        string Email, Alternative_Email;
        string Client_Order_no, DEED_CHAIN, Order_number;
        int Order_Type;
        int abstarctor_id;
        int User_Id; string File_size, file_ext, filename,Report_Name;
        string Email_Id, From;
        int Ab_Order_Email_Number;
        int Ab_Order_Email_ID;
        int abemailid;
       string  Attachment_Path,File_Name;
       static int currentPageIndex = 0;
       private int pageSize = 20;
       System.Data.DataTable dt_Grid = new System.Data.DataTable();

        public Abstractor_Compose_Email(int orderId,int abstractorId,int userid)
        {
            InitializeComponent();
            Order_Id = orderId;
            Abstractor_Id = abstractorId;
            User_Id = userid;
        }

        private void Abstractor_Compose_Email_Load(object sender, EventArgs e)
        {
            this.Read_Emails();
            Get_Abstractor_Email();
            //txt_Abstractor_Cc.Text = "vendors@drnds.com";
            txt_Abstractor_Cc.Text = "techteam@drnds.com";
            Get_Abstractor_Order_Num();
            Grid_Read_Email_Bind();
            Grid_Email_Bind();
            PopulateListView();
            // Send_Html_Email_Body();
            listView1.Items.Clear();

            Clear();
            Abstractor_Rich_Email_Body.Focus();
            //RefreshView();
           
           // Clear();
           
        }

        private void Link_Attachment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         {
             //listView1.Items.Clear();
             int count = 0;
             int Chk = 0;

            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in open.FileNames)
                {

                    FName = s.Split('\\');
                    //txt_Attachments.ReadOnly = true;
                    //txt_Attachments.Text = open.SafeFileName.ToString();
                              
                    filename = open.FileName.ToString();                                     
                    string Docname = FName[FName.Length - 1].ToString();
                    listView1.Text = Docname.ToString();
                    for (int i = 0; i < listView1.Items.Count; i++)                
                    {

                        if (Docname == listView1.Items[i].Text)
                        {
                            Chk = 1;
                          
                            break;
                        }
                        else
                        {
                            Chk = 0;
                        }

                    }


                    System.IO.FileInfo f = new System.IO.FileInfo(filename);
                    double filesize = f.Length;
                    GetFileSize(filesize);
                    file_ext = f.Extension;
                    ////string destpath = @"\\192.168.12.33\Abstractor Email Attachements\" + txt_Attachments.Text.ToString();
                 //   string destpath = @"\\192.168.12.33\Abstractor Email Attachements\" + Order_Id + @"\" + listView1.Text.ToString();
        

                    //Directory.CreateDirectory(@"\\192.168.12.33\Abstractor Email Attachements\" + Order_Id + @"\" + listView1.Text.ToString());
                  
                    if (Chk == 0)
                    {
                      try
                      {
                          string destpath = @"\\192.168.12.33\Abstractor Email Attachements\" + Order_Id + @"\" + listView1.Text.ToString();
                          DirectoryEntry de = new DirectoryEntry(destpath, "administrator", "password1$");
                          de.Username = "administrator";
                          de.Password = "password1$";
                          Directory.CreateDirectory(@"\\192.168.12.33\Abstractor Email Attachements\" + Order_Id);
                        File.Copy(s, destpath, true);
                        count++;

                        Hashtable ht = new Hashtable();
                        DataTable dt = new DataTable();

                        Hashtable ht_GetMax_Ab_Order_Email_Id_Num = new Hashtable();
                        DataTable dt_GetMax_Ab_Order_Email_Id_Num = new DataTable();

                        ht_GetMax_Ab_Order_Email_Id_Num.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
                        ht_GetMax_Ab_Order_Email_Id_Num.Add("@Order_Id", Order_Id);
                        dt_GetMax_Ab_Order_Email_Id_Num = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_Email_Id_Num);

                        if (dt_GetMax_Ab_Order_Email_Id_Num.Rows.Count > 0)
                        {

                            Ab_Order_Email_Number = int.Parse(dt_GetMax_Ab_Order_Email_Id_Num.Rows[0]["Ab_Order_Email_Number"].ToString());
                        }

                        //DateTime date = new DateTime();
                        //date = DateTime.Now;
                        //string dateeval = date.ToString("dd/MM/yyyy");


                        //23-01-2018
                        DateTime date = new DateTime();
                        DateTime time;
                      date= DateTime.Now;
                        string dateeval = date.ToString("MM/dd/yyyy");


                        ht.Add("@Trans", "INSERT");
                        ht.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
                        ht.Add("@Order_Id", Order_Id);
                        ht.Add("@Attached_Path", destpath);
                        ht.Add("@File_Name", Docname);
                        ht.Add("@File_Size", filesize);
                        ht.Add("@File_Extension", file_ext);
                        ht.Add("@Message_Status", "True");
                        ht.Add("@Attached_By", User_Id);
                        ht.Add("@Attached_Date", date);
                        // ht.Add("@status", "True");
                        dt = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht);
                     
                       
                    }
                      catch (Exception ex)
                      {
                          //dialogResult=   MessageBox.Show("This is File Is Already Exist Do you want to Replace?","Warning",MessageBoxButtons.YesNo);

                          //if (dialogResult == DialogResult.Yes)
                          //{
                          //    File.Copy(s, destpath, true);
                          //    count++;

                          //    Hashtable ht = new Hashtable();
                          //    DataTable dt = new DataTable();

                          //    Hashtable ht_GetMax_Ab_Order_Email_Id_Num = new Hashtable();
                          //    DataTable dt_GetMax_Ab_Order_Email_Id_Num = new DataTable();

                          //    ht_GetMax_Ab_Order_Email_Id_Num.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
                          //    ht_GetMax_Ab_Order_Email_Id_Num.Add("@Order_Id", Order_Id);
                          //    dt_GetMax_Ab_Order_Email_Id_Num = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_Email_Id_Num);

                          //    if (dt_GetMax_Ab_Order_Email_Id_Num.Rows.Count > 0)
                          //    {

                          //        Ab_Order_Email_ID = int.Parse(dt_GetMax_Ab_Order_Email_Id_Num.Rows[0]["Ab_Order_Email_ID"].ToString());
                          //    }

                          //    ht.Add("@Trans", "INSERT");
                          //    ht.Add("@Ab_Order_Email_ID", Ab_Order_Email_ID);
                          //    ht.Add("@Order_Id", Order_Id);
                          //    ht.Add("@Attached_Path", destpath);
                          //    ht.Add("@File_Name", listView1.Text);
                          //    ht.Add("@File_Size", filesize);
                          //    ht.Add("@File_Extension", file_ext);
                          //    ht.Add("@Message_Status", "True");
                          //    ht.Add("@Attached_By", User_Id);
                          //    ht.Add("@Attached_Date", DateTime.Now);
                          //    // ht.Add("@status", "True");
                          //    dt = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht);
                             
                          //}
                          //else
                          //{


                          //}
                          MessageBox.Show(ex.Message);
                      }
                    }
                    else 
                    { 
                        dialogResult = MessageBox.Show("This is File Is Already Exist Do you want to Replace?", "Warning", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            string destpath = @"\\192.168.12.33\Abstractor Email Attachements\" + Order_Id + @"\" + listView1.Text.ToString();
                            DirectoryEntry de = new DirectoryEntry(destpath, "administrator", "password1$");
                            de.Username = "administrator";
                            de.Password = "password1$";
                            Directory.CreateDirectory(@"\\192.168.12.33\Abstractor Email Attachements\" + Order_Id);
                            File.Copy(s, destpath, true);
                            count++;

                         
                            Hashtable ht_GetMax_Ab_Order_Email_Id_Num = new Hashtable();
                            DataTable dt_GetMax_Ab_Order_Email_Id_Num = new DataTable();

                            ht_GetMax_Ab_Order_Email_Id_Num.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
                            ht_GetMax_Ab_Order_Email_Id_Num.Add("@Order_Id", Order_Id);
                            dt_GetMax_Ab_Order_Email_Id_Num = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_Email_Id_Num);

                            if (dt_GetMax_Ab_Order_Email_Id_Num.Rows.Count > 0)
                            {

                                Ab_Order_Email_Number = int.Parse(dt_GetMax_Ab_Order_Email_Id_Num.Rows[0]["Ab_Order_Email_Number"].ToString());
                            }
                            //DateTime date = new DateTime();
                            //date = DateTime.Now;
                            //string dateeval = date.ToString("dd/MM/yyyy");


                            //23-01-2018
                            DateTime date = new DateTime();
                            DateTime time;
                          date= DateTime.Now;
                            string dateeval = date.ToString("MM/dd/yyyy");

                            Hashtable ht = new Hashtable();
                            DataTable dt = new DataTable();

                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
                            ht.Add("@Order_Id", Order_Id);
                            ht.Add("@Attached_Path", destpath);
                            ht.Add("@File_Name", listView1.Text);
                            ht.Add("@File_Size", filesize);
                            ht.Add("@File_Extension", file_ext);
                            ht.Add("@Message_Status", "True");
                            ht.Add("@Attached_By", User_Id);
                            ht.Add("@Attached_Date", date);
                            // ht.Add("@status", "True");
                            dt = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht);
                        
                        
                        }
                    
                     
                    }
                   
                }

                MessageBox.Show("File is Attached Sucessfully");
                RefreshView();
               
            }
        }

        private void Clear()
        {
            txt_Abstractor_Subject.Clear();
            txt_Abstractor_Subject.Text = "";
            Abstractor_Rich_Email_Body.Text = "";
            listView1.Items.Clear();
            listView1.Refresh();

            Abstractor_Rich_Email_Body.Focus();

            RefreshView();
        }

        private void btn_Abstractor_Send_Click(object sender, EventArgs e)
        {

            if (validation()!=false)
            {
                try{


                        //MailSettings.SMTPServer = Convert.ToString(ConfigurationManager.AppSettings["HostName"]);
                        MailMessage mailMessage = new MailMessage();


                        mailMessage.From = new MailAddress("techteam@drnds.com");
                        mailMessage.To.Add("techteam@drnds.com");
                        mailMessage.CC.Add("niranjanmurthya@drnds.com");
                        mailMessage.Subject = txt_Abstractor_Subject.Text;

                       mailMessage.IsBodyHtml = true;

                       mailMessage.Body = this.PopulateBody(Abstractor_Rich_Email_Body.Text);


                      // mailMessage.BodyFormat = MailFormat.Html;

                        Hashtable ht_Get_Attachment = new Hashtable();
                        DataTable dt_Get_Attachment = new DataTable();

                        ht_Get_Attachment.Add("@Trans", "GET_ATTACHMENT");
                        ht_Get_Attachment.Add("@Order_Id", Order_Id);
                        dt_Get_Attachment = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_Get_Attachment);

                        if (dt_Get_Attachment.Rows.Count > 0)
                        {


                            for (int i = 0; i < dt_Get_Attachment.Rows.Count; i++)
                            {
                                Attachment_Path = dt_Get_Attachment.Rows[i]["Attached_Path"].ToString();
                                File_Name = dt_Get_Attachment.Rows[i]["File_Name"].ToString();
                                mailMessage.Attachments.Add(new Attachment(Attachment_Path));
                            }

                        }
                        //string destpath = @"\\192.168.12.33\Abstractor Email Attachements\" + Order_Id + @"\" + listView1.Text.ToString();              
                        //if (destpath != "")
                        //{

                        //  // mailMessage.Attachments.Add(new Attachment(txt_Attachments.Text));

                 
                        //    mailMessage.Attachments.Add(new Attachment(destpath));       
                        //}
               
                        SmtpClient smtp = new SmtpClient();
                        smtp.UseDefaultCredentials = false;
                     //   smtp.Credentials = new System.Net.NetworkCredential("vendors@drnds.com", "dts3526$");
                        smtp.Credentials = new System.Net.NetworkCredential("techteam@drnds.com", "nop539");
                        smtp.Host = "smtpout.asia.secureserver.net";
                        smtp.Port = 25;
                        smtp.EnableSsl = false;
            
         //========================================================================================================
                    Hashtable ht_GetMax_Ab_Order_EmailNum = new Hashtable();
                    DataTable dt_GetMax_Ab_Order_EmailNum = new DataTable();

                    ht_GetMax_Ab_Order_EmailNum.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
                    ht_GetMax_Ab_Order_EmailNum.Add("@Order_Id", Order_Id);
                    dt_GetMax_Ab_Order_EmailNum = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_EmailNum);

                    if (dt_GetMax_Ab_Order_EmailNum.Rows.Count > 0)
                    {

                        Ab_Order_Email_Number = int.Parse(dt_GetMax_Ab_Order_EmailNum.Rows[0]["Ab_Order_Email_Number"].ToString());
                    }
                    //DateTime date = new DateTime();
                    //date = DateTime.Now;
                    //string dateeval = date.ToString("dd/MM/yyyy");


                    //23-01-2018
                    DateTime date = new DateTime();
                    DateTime time;
                  date= DateTime.Now;
                    string dateeval = date.ToString("MM/dd/yyyy");

                    Hashtable ht_Insert = new Hashtable();
                    DataTable dt_Insert = new DataTable();
                    ht_Insert.Add("@Trans", "INSERT");
                    ht_Insert.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
                    ht_Insert.Add("@Order_Id", Order_Id);
                    ht_Insert.Add("@Abstractor_Id", Abstractor_Id);
                    ht_Insert.Add("@To", txt_Abstractor_To.Text);
                    ht_Insert.Add("@From","vendors@drnds.com");
                    ht_Insert.Add("@Cc", txt_Abstractor_Cc.Text);
                    ht_Insert.Add("@Subject",txt_Abstractor_Subject.Text);
                    ht_Insert.Add("@Message", Abstractor_Rich_Email_Body.Text);
                    ht_Insert.Add("@Send_By", User_Id);
                    ht_Insert.Add("@Send_Date", dateeval);
                    ht_Insert.Add("@status", "True");
                    dt_Insert = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", ht_Insert);

                    smtp.Send(mailMessage);
                    MessageBox.Show("Mesage sent Successfully");
                    Grid_Email_Bind();          
                    Clear();
          
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
          }
        }

        private Boolean validation()
        {
            if(txt_Abstractor_To.Text=="")
            {
                MessageBox.Show("Enter To Email");
                txt_Abstractor_To.Focus();
                return false;

            }
            if (txt_Abstractor_Cc.Text == "")
            {
                MessageBox.Show("Enter cc email ");
                txt_Abstractor_Cc.Focus();
                return false;
            }
            if (txt_Abstractor_Subject.Text == "")
            {
                MessageBox.Show("Enter Subject ");
                txt_Abstractor_Subject.Focus();
                return false;
            }
            if (Abstractor_Rich_Email_Body.Text == "")
            {
                MessageBox.Show("Enter Text  in the body");
                Abstractor_Rich_Email_Body.Focus();
                return false;
            }
            return true;
        }

        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtpout.secureserver.net");
              //  mailMessage.From = new MailAddress("vendors@drnds.com");
                mailMessage.From = new MailAddress("techteam@drnds.com");
                mailMessage.To.Add("techteam@drnds.com");
               // mailMessage.To = txt_Abstractor_To.Text;
                mailMessage.Subject = txt_Abstractor_Subject.Text;
                mailMessage.Body = Abstractor_Rich_Email_Body.Text;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                // mailMessage.To = txt_Abstractor_To.Text;

                //string Path1 = @"\\192.168.12.33\Abstractor-Email-Attachment";

                //MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));

                //string Attachment_Name = Report_Name.ToString() + '-' + Order_number.ToString() + ".txt";

                //mailMessage.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));



                // txt_Abstractor_Subject.Text = "New Search Request - " + Order_number + "-" + Report_Name.ToString();


             
   
                //  smtp.Host = "smtpout.secureserver.net";
                // NetworkCredential NetworkCred = new NetworkCredential("techteam@drnds.com", "nop539");
                smtp.Credentials = new System.Net.NetworkCredential("techteam@drnds.com", "nop539");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Port = 80;
                //  smtp.Credentials = NetworkCred;

                smtp.Send(mailMessage);
                MessageBox.Show("Mesage sent Successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }


        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {
                    mm.IsBodyHtml = true;
                  //  string body = this.PopulateBody();
                    SendHtmlFormattedEmail("vendors@drnds.com","Sample","body");  

                    this.Close();

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }
                
               
                 

        }

        private string PopulateBody(string Abstractor_Rich_Email_Body)
        {
            string mesg_body = string.Empty;

            StreamReader reader = new StreamReader(@"C:\Kavita\OMS Application12-06-2016\Ordermanagement_01.A.36(VENDOR)\Ordermanagement_01\ComposeEmail.htm");
            mesg_body = reader.ReadToEnd();
            //C:\Kavita\OMS Application12-06-2016\Ordermanagement_01.A.36(VENDOR)\Ordermanagement_01\Abstractor\ComposeEmail.htm
            mesg_body = mesg_body.Replace("{Abstractor_Rich_Email_Body}", Abstractor_Rich_Email_Body);

            return mesg_body;
        }


        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";
            File_size = size;
            return size;
        }

         private void Grid_Email_Bind()
         {
             
             Hashtable ht_Grid = new Hashtable();
             DataTable dt_Grid = new DataTable();
             ht_Grid.Add("@Trans", "SELECT_GRID_BIND_EMAIL");
             ht_Grid.Add("@Order_Id", Order_Id);
             ht_Grid.Add("@User_Id", User_Id);
           //  ht_Grid.Add("@Email_Attachment_Id", Email_Attachment_Id);
             dt_Grid = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", ht_Grid);
             if (dt_Grid.Rows.Count > 0)
             {
                GridView_Abstractor_Email_Details.Rows.Clear();
                 for (int i = 0; i < dt_Grid.Rows.Count; i++)
                 {
                     GridView_Abstractor_Email_Details.Rows.Add();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[0].Value = dt_Grid.Rows[i]["Ab_Order_Email_Number"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[1].Value = dt_Grid.Rows[i]["Subject"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[2].Value = dt_Grid.Rows[i]["To"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[3].Value = dt_Grid.Rows[i]["User_Name"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[4].Value = dt_Grid.Rows[i]["Send_Date"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[6].Value = dt_Grid.Rows[i]["Message"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[5].Value = dt_Grid.Rows[i]["Order_Id"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[7].Value = dt_Grid.Rows[i]["File_Name"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[8].Value = dt_Grid.Rows[i]["Cc"].ToString();
                     GridView_Abstractor_Email_Details.Rows[i].Cells[9].Value = dt_Grid.Rows[i]["From"].ToString();
                 }
                            
             }
            else
            {
                GridView_Abstractor_Email_Details.Rows.Clear();
                GridView_Abstractor_Email_Details.Visible = true;
                GridView_Abstractor_Email_Details.DataSource = null;               
            }
             
            // lbl_Total_Orders.Text = "Total Mail:" + dt_Grid.Rows.Count.ToString();
            // lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt_Grid.Rows.Count) / pageSize);
         }

         private void Get_Abstractor_Email()
         {
             Hashtable ht_GetEmail = new Hashtable();
             DataTable dt_GetEmail = new DataTable();
             ht_GetEmail.Add("@Trans", "GET_ABSTRACTOR_EMAIL");
             ht_GetEmail.Add("@Abstractor_Id", Abstractor_Id);

             dt_GetEmail = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", ht_GetEmail);
             if (dt_GetEmail.Rows.Count > 0)
             {
                 txt_Abstractor_To.Text = dt_GetEmail.Rows[0]["Email"].ToString();
             }
         }

         private void Get_Abstractor_Order_Num()
         {
             Hashtable ht_Get_Order_Num = new Hashtable();
             DataTable dt_Get_Order_Num = new DataTable();
             ht_Get_Order_Num.Add("@Trans", "GET_ORDER_NUMBER");
             ht_Get_Order_Num.Add("@Order_Id", Order_Id);

             dt_Get_Order_Num = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", ht_Get_Order_Num);
             if (dt_Get_Order_Num.Rows.Count > 0)
             {
                 lbl_Order_Num.Text = dt_Get_Order_Num.Rows[0]["Client_Order_Number"].ToString();
             }
         }

         private void RefreshView()
         {
             listView1.Items.Clear();
             Hashtable ht_GetMax_Ab_Order_Email_Id_Num = new Hashtable();
             DataTable dt_GetMax_Ab_Order_Email_Id_Num = new DataTable();

             ht_GetMax_Ab_Order_Email_Id_Num.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
             ht_GetMax_Ab_Order_Email_Id_Num.Add("@Order_Id", Order_Id);
             dt_GetMax_Ab_Order_Email_Id_Num = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_Email_Id_Num);

             if (dt_GetMax_Ab_Order_Email_Id_Num.Rows.Count > 0)
             {
                 Ab_Order_Email_Number = int.Parse(dt_GetMax_Ab_Order_Email_Id_Num.Rows[0]["Ab_Order_Email_Number"].ToString());
               //  Email_Attachment_Id = int.Parse(dt_GetMax_Ab_Order_Email_Id_Num.Rows[0]["Email_Attachment_Id"].ToString());
             }

           

             Hashtable htDocument_Select = new Hashtable();
             System.Data.DataTable dtDocument_Select = new System.Data.DataTable();
             

                 htDocument_Select.Add("@Trans", "SELECT_ATTACH_FILE");

                 htDocument_Select.Add("@Order_Id", Order_Id);
                 htDocument_Select.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
              //   htDocument_Select.Add("@Email_Attachment_Id", Email_Attachment_Id);

             dtDocument_Select = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", htDocument_Select);

             listView1.Items.Clear();

             listView1.FullRowSelect = true;
             for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
             {
                 DataRow dr = dtDocument_Select.Rows[i];
                 ListViewItem listitem = new ListViewItem(dr[4].ToString());
                
                    listitem.SubItems.Add("Delete");
                    listitem.SubItems.Add(dr[0].ToString());
                    
        
                 //listitem.SubItems.Add(dr[0].ToString());
                 listView1.Items.Add(listitem);
                

             }
         }

         private void PopulateListView()
        {
            //listView1.Width = 370;
        
            listView1.Size = new System.Drawing.Size(160, 353);
          //  listView1.Location = new System.Drawing.Point(10, 10);

	        // Declare and construct the ColumnHeader objects.
            ColumnHeader header1, header2; 
            
	        header1 = new ColumnHeader();
            header2 = new ColumnHeader();

	        // Set the text, alignment and width for each column header.
	        header1.Text = "File name";
            header1.TextAlign = HorizontalAlignment.Left;
         
	        header1.Width = 100;

            header2.TextAlign = HorizontalAlignment.Left;
            header2.Text = "Delete";
            header2.Width = 60;

	        // Add the headers to the ListView control.
            listView1.Columns.Add(header1);
            listView1.Columns.Add(header2);

               // Specify that each item appears on a separate line.
            listView1.Scrollable = true;
            listView1.View = View.Details;
        }
 
         private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

         private void listView1_Click(object sender, EventArgs e)
        {

        }

         private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo listViewHitTestInfo = listView1.HitTest(e.X, e.Y);

            // Index of the clicked ListView column
            int columnIndex = listViewHitTestInfo.Item.SubItems.IndexOf(listViewHitTestInfo.SubItem);

            var value = listViewHitTestInfo.Item.SubItems[0].Text.ToString();
            var attachment_Id = listViewHitTestInfo.Item.SubItems[2].Text.ToString();
            if (columnIndex == 1)
            {

                int attach_id = int.Parse(attachment_Id.ToString());

                Hashtable htdelete = new Hashtable();
                DataTable dtdelete = new DataTable();
                htdelete.Add("@Trans", "DELETE");
                htdelete.Add("@Email_Attachment_Id", attach_id);

                dtdelete = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", htdelete);

                RefreshView();


            }
        }

         private void txt_Order_Number_TextChanged(object sender, EventArgs e)
        {
            Search_Detail();
        }

        private void Search_Detail()
        {
            System.Data.DataTable dtselect = new System.Data.DataTable();
            //System.Data.DataTable tmptable = dt_Grid.Clone();

            Hashtable ht_Grid = new Hashtable();
            DataTable dt_Grid = new DataTable();
            ht_Grid.Add("@Trans", "SELECT_GRID_BIND_EMAIL");
            ht_Grid.Add("@Order_Id", Order_Id);
            ht_Grid.Add("@User_Id", User_Id);
            dt_Grid = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", ht_Grid);
            if (dt_Grid.Rows.Count > 0)
            {
                GridView_Abstractor_Email_Details.Rows.Clear();
                for (int i = 0; i < dt_Grid.Rows.Count; i++)
                {
                    GridView_Abstractor_Email_Details.Rows.Add();
                    GridView_Abstractor_Email_Details.Rows[i].Cells[0].Value = i + 1;
                    GridView_Abstractor_Email_Details.Rows[i].Cells[1].Value = dt_Grid.Rows[i]["Subject"].ToString();
                    GridView_Abstractor_Email_Details.Rows[i].Cells[2].Value = dt_Grid.Rows[i]["To"].ToString();
                    GridView_Abstractor_Email_Details.Rows[i].Cells[3].Value = dt_Grid.Rows[i]["User_Name"].ToString();
                    GridView_Abstractor_Email_Details.Rows[i].Cells[4].Value = dt_Grid.Rows[i]["Send_Date"].ToString();

                }
            }


            DataView dtsearch = new DataView(dt_Grid);

            if (txt_Order_Number.Text != "")
            {
                var search = txt_Order_Number.Text.ToString();

                dtsearch.RowFilter ="Subject Like '%" + search.ToString() + "%'  or Send_Date Like '%" + search.ToString() + "%' or User_Name Like '%" + search.ToString() + "%' or To Like '%" + search.ToString() + "%'  ";

                System.Data.DataTable dt = new System.Data.DataTable();

                dt = dtsearch.ToTable();
                //System.Data.DataTable temptable = dt.Clone();
                //int startindex = currentPageIndex * pageSize;
                //int endindex = currentPageIndex * pageSize + pageSize;
                //if (endindex > dt.Rows.Count)
                //{
                //    endindex = dt.Rows.Count;
                //}
                //for (int i = startindex; i < endindex; i++)
                //{
                //    DataRow row = temptable.NewRow();
                //    Get_Row_Table_Search(ref row, dt.Rows[i]);
                //    temptable.Rows.Add(row);
                //}

                //if (temptable.Rows.Count > 0)
                //{
                if(dt.Rows.Count>0)
                {
                    GridView_Abstractor_Email_Details.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridView_Abstractor_Email_Details.Rows.Add();
                        GridView_Abstractor_Email_Details.Rows[i].Cells[0].Value = i + 1;
                        GridView_Abstractor_Email_Details.Rows[i].Cells[1].Value = dt.Rows[i]["Subject"].ToString();
                        GridView_Abstractor_Email_Details.Rows[i].Cells[2].Value = dt.Rows[i]["To"].ToString();
                        GridView_Abstractor_Email_Details.Rows[i].Cells[3].Value = dt.Rows[i]["User_Name"].ToString();
                        GridView_Abstractor_Email_Details.Rows[i].Cells[4].Value = dt.Rows[i]["Send_Date"].ToString();
                                        
                        GridView_Abstractor_Email_Details.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.PowderBlue;


                    }
                }
                //lbl_Total_Orders.Text = dt.Rows.Count.ToString();
               // lbl_Total_Orders.Text = "Total Orders: " + dt_Grid.Rows.Count.ToString();
                //lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pageSize);
            }
            else
            {
                Grid_Email_Bind();
            }
        }

        private void Get_Row_Table_Search(ref DataRow dest, DataRow source)
        {
          //  System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataColumn col in dt_Grid.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        
        }

        //private void btnFirst_Click(object sender, EventArgs e)
        //{
        //    Cursor currentCursor = this.Cursor;
        //    this.Cursor = Cursors.WaitCursor;

        //    currentPageIndex = 0;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = true;
        //    btnLast.Enabled = true;
        //    btnFirst.Enabled = false;
        //    Grid_Email_Bind();

        //    this.Cursor = currentCursor;
        //}

        //private void btnPrevious_Click(object sender, EventArgs e)
        //{
        //    Cursor currentCursor = this.Cursor;
        //    this.Cursor = Cursors.WaitCursor;
        //    // splitContainer1.Enabled = false;
        //    currentPageIndex--;
        //    if (currentPageIndex == 0)
        //    {
        //        btnPrevious.Enabled = false;
        //        btnFirst.Enabled = false;
        //    }
        //    else
        //    {
        //        btnPrevious.Enabled = true;
        //        btnFirst.Enabled = true;

        //    }
        //    btnNext.Enabled = true;
        //    btnLast.Enabled = true;
        //    Grid_Email_Bind();
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    Cursor currentCursor = this.Cursor;
        //    this.Cursor = Cursors.WaitCursor;

        //    currentPageIndex++;
        //    if (currentPageIndex == (int)Math.Ceiling(Convert.ToDecimal(dt_Grid.Rows.Count) / pageSize) - 1)
        //    {
        //        btnNext.Enabled = false;
        //        btnLast.Enabled = false;
        //    }
        //    else
        //    {
        //        btnNext.Enabled = true;
        //        btnLast.Enabled = true;
        //    }
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = true;

        //    Grid_Email_Bind();
        //    this.Cursor = currentCursor;
        //}

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    Cursor currentCursor = this.Cursor;
        //    this.Cursor = Cursors.WaitCursor;

        //    currentPageIndex = (int)Math.Ceiling(Convert.ToDecimal(dt_Grid.Rows.Count) / pageSize) - 1;
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = true;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = false;
        //    Grid_Email_Bind();
        //    this.Cursor = currentCursor;
        //}

        private void GridView_Abstractor_Email_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Ab_Order_Email_Number = int.Parse(GridView_Abstractor_Email_Details.Rows[e.RowIndex].Cells[0].Value.ToString());
              //  Ab_Order_Email_ID = int.Parse(GridView_Abstractor_Email_Details.Rows[e.RowIndex].Cells[0].Value.ToString());
                Order_Id = int.Parse(GridView_Abstractor_Email_Details.Rows[e.RowIndex].Cells[5].Value.ToString());
              
                Hashtable htemail = new Hashtable();
                DataTable dtemail = new DataTable();

                htemail.Add("@Trans", "SELECT_DETAIL_BY_ID");
                htemail.Add("@Order_Id", Order_Id);
                htemail.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
               // htemail.Add("@Ab_Order_Email_ID", Ab_Order_Email_ID);
                dt_Grid = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", htemail);
                if (dt_Grid.Rows.Count > 0)
                {
                    txt_Abstractor_Subject.Text = dt_Grid.Rows[0]["Subject"].ToString();
                    txt_Abstractor_To.Text = dt_Grid.Rows[0]["TO"].ToString();
                    txt_Abstractor_Cc.Text = dt_Grid.Rows[0]["Cc"].ToString();                   
                    Abstractor_Rich_Email_Body.Text = dt_Grid.Rows[0]["Message"].ToString();

                    
                    listView1.Items.Clear();
                  
                    Hashtable htDocument_Select = new Hashtable();
                    System.Data.DataTable dtDocument_Select = new System.Data.DataTable();


                    htDocument_Select.Add("@Trans", "SELECT_LISTVIEW_FILES");

                    htDocument_Select.Add("@Order_Id", Order_Id);

                    htDocument_Select.Add("@Ab_Order_Email_ID", Ab_Order_Email_Number);

                    dtDocument_Select = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", htDocument_Select);

                    listView1.Items.Clear();

                    listView1.FullRowSelect = true;
                    for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
                    {
                        DataRow dr = dtDocument_Select.Rows[i];
                        ListViewItem listitem = new ListViewItem(dr[2].ToString());

                        listitem.SubItems.Add("Delete");
                        listitem.SubItems.Add(dr[0].ToString());


                        //listitem.SubItems.Add(dr[0].ToString());
                        listView1.Items.Add(listitem);
                    }
                   
                   
                }
            }
        }

        private void Tool_Strip_New_Mail_Click(object sender, EventArgs e)
        {

            txt_Abstractor_Subject.Clear();
            txt_Abstractor_Subject.Text = "";
            Abstractor_Rich_Email_Body.Text = "";
            listView1.Items.Clear();
            listView1.Refresh();

            Abstractor_Rich_Email_Body.Focus();

                


        }

       

        //private void BindListView(string p)
        //{

        //}

        //private void BindListView(int Ab_Order_Email_ID)
        //{

        //    listView1.Items.Clear();
        //    Hashtable ht_GetMax_Ab_Order_Email_Id_Num = new Hashtable();
        //    DataTable dt_GetMax_Ab_Order_Email_Id_Num = new DataTable();

        //    ht_GetMax_Ab_Order_Email_Id_Num.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
        //    ht_GetMax_Ab_Order_Email_Id_Num.Add("@Order_Id", Order_Id);
        //    dt_GetMax_Ab_Order_Email_Id_Num = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_Email_Id_Num);

        //    if (dt_GetMax_Ab_Order_Email_Id_Num.Rows.Count > 0)
        //    {
        //        Ab_Order_Email_Number = int.Parse(dt_GetMax_Ab_Order_Email_Id_Num.Rows[0]["Ab_Order_Email_Number"].ToString());
        //        //  Email_Attachment_Id = int.Parse(dt_GetMax_Ab_Order_Email_Id_Num.Rows[0]["Email_Attachment_Id"].ToString());
        //    }


        //    Hashtable htDocument_Select = new Hashtable();
        //    System.Data.DataTable dtDocument_Select = new System.Data.DataTable();


        //    htDocument_Select.Add("@Trans", "SELECT_ATTACH_FILE");

        //    htDocument_Select.Add("@Order_Id", Order_Id);
        //    htDocument_Select.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
        //    //   htDocument_Select.Add("@Email_Attachment_Id", Email_Attachment_Id);

        //    dtDocument_Select = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", htDocument_Select);

        //    listView1.Items.Clear();

        //    listView1.FullRowSelect = true;
        //    for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
        //    {
        //        DataRow dr = dtDocument_Select.Rows[i];
        //        ListViewItem listitem = new ListViewItem(dr[4].ToString());

        //        listitem.SubItems.Add("Delete");
        //        listitem.SubItems.Add(dr[0].ToString());


        //        //listitem.SubItems.Add(dr[0].ToString());
        //        listView1.Items.Add(listitem);


        //    }
        //}

        

     

         //private void GridView_Abstractor_Email_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
         //{
         //    if (e.RowIndex != -1)
         //    {
         //        if (e.ColumnIndex == 5)
         //        {
         //            //delete
         //            Hashtable ht_Delete = new Hashtable();
         //            DataTable dt_Delete = new DataTable();
         //            dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
         //            if (dialogResult == DialogResult.Yes)
         //            {
         //                ht_Delete.Clear(); dt_Delete.Clear();
         //                ht_Delete.Add("@Trans", "DELETE");
         //                ht_Delete.Add("@Ab_Order_Email_ID", int.Parse(GridView_Abstractor_Email_Details.Rows[e.RowIndex].Cells[0].Value.ToString()));
         //                dt_Delete = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Details", ht_Delete);
         //                MessageBox.Show("Record Deleted Successfully");
         //               // Ab_Order_Email_ID = 0;
         //                Grid_Email_Bind();
                                              
         //            }
         //        }
         //    }
         //}


        private void Read_Emails()
        {
            Pop3Client pop3Client;
            System.Net.ServicePointManager.Expect100Continue = false;
            pop3Client = new Pop3Client();
            pop3Client.Connect("pop.asia.secureserver.net", 995, true);

            pop3Client.Authenticate("techteam@drnds.com", "nop539", AuthenticationMethod.UsernameAndPassword);

            int count = pop3Client.GetMessageCount();
            DataTable dtMessages = new DataTable();

            dtMessages.Columns.Add("Ab_Order_Email_ID");
            dtMessages.Columns.Add("To");
            dtMessages.Columns.Add("From");
           // dtMessages.Columns.Add("Cc");
            dtMessages.Columns.Add("Subject");
            dtMessages.Columns.Add("SendDate");
          //  dtMessages.Columns.Add("MessageBody");

          
              
            int counter = 0;
            for (int i = count; i >= 1; i--)
            {

                OpenPop.Mime.Header.MessageHeader msgheader = pop3Client.GetMessageHeaders(i);

                string From = msgheader.From.ToString();
              
                if (From == "Mail Delivery System")
                {

                    //message = pop3Client.GetMessage(i);
                }
                else
                {
                    string subject = msgheader.Subject.ToString();

                    if (subject == "Request for Leave Approval")
                    {

                        if (From.Contains("niranjanmurthy@drnds.com"))
                        {

                            ////string To = msgheader.Sender.DisplayName;
                            //// string mbody = msgheader.
                            ////  string Cc = msgheader.Cc.ToString();
                            //if (From == "Mail Delivery System")
                            //{

                            //    //message = pop3Client.GetMessage(i);
                            //}
                            //else
                            //{
                            // OpenPop.Mime.Message message = pop3Client.GetMessage(i);
                            dtMessages.Rows.Clear();
                            dtMessages.Rows.Add();
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["Ab_Order_Email_ID"] = i;
                            //  dtMessages.Rows[dtMessages.Rows.Count - 1]["To"] = msgheader.To.ToString();
                            //   dtMessages.Rows[dtMessages.Rows.Count - 1]["Cc"] = msgheader.Cc.ToString();
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["From"] = msgheader.From.DisplayName;
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["Subject"] = msgheader.Subject;
                            dtMessages.Rows[dtMessages.Rows.Count - 1]["SendDate"] = msgheader.DateSent;
                            //  dtMessages.Rows[dtMessages.Rows.Count - 1]["MessageBody"] = msgheader.ContentTransferEncoding;

                            counter++;
                            if (counter > 4)
                            {
                                break;
                            }
                            Hashtable ht_GetMax_Ab_Order_REmailNum = new Hashtable();
                            DataTable dt_GetMax_Ab_Order_REmailNum = new DataTable();

                            ht_GetMax_Ab_Order_REmailNum.Add("@Trans", "GET_MAX_AD_ORDER_EMAIL_ID");
                            ht_GetMax_Ab_Order_REmailNum.Add("@Order_Id", Order_Id);
                            dt_GetMax_Ab_Order_REmailNum = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Attachments", ht_GetMax_Ab_Order_REmailNum);

                            if (dt_GetMax_Ab_Order_REmailNum.Rows.Count > 0)
                            {

                                Ab_Order_Email_Number = int.Parse(dt_GetMax_Ab_Order_REmailNum.Rows[0]["Ab_Order_Email_Number"].ToString());
                            }
                            //DateTime date = new DateTime();
                            //date = DateTime.Now;
                            //string dateeval = date.ToString("dd/MM/yyyy");


                            //23-01-2018
                            DateTime date = new DateTime();
                            DateTime time;
                          date= DateTime.Now;
                            string dateeval = date.ToString("MM/dd/yyyy");

                            Hashtable ht_RInsert = new Hashtable();
                            DataTable dt_RInsert = new DataTable();
                            ht_RInsert.Add("@Trans", "INSERT");
                            ht_RInsert.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
                            ht_RInsert.Add("@Order_Id", Order_Id);
                            ht_RInsert.Add("@Abstractor_Id", Abstractor_Id);
                            ht_RInsert.Add("@To", "");
                            ht_RInsert.Add("@From", "niranjanmurthya@drnds.com");
                            ht_RInsert.Add("@Cc", "");
                            ht_RInsert.Add("@Subject", subject);
                            ht_RInsert.Add("@Message", "");
                            ht_RInsert.Add("@Send_By", User_Id);
                            ht_RInsert.Add("@Send_Date", dateeval);
                            ht_RInsert.Add("@status", "True");
                            dt_RInsert = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Recieval_Details", ht_RInsert);
                            Grid_Read_Email_Bind();
                            //GridView_Read_Mail.Visible = true;
                            //GridView_Read_Mail.DataSource = dt_RInsert;
                            //}

                        }


                    }
                    //Grid_Read_Email_Bind();
                    //GridView_Read_Mail.Visible = true;

                    //GridView_Read_Mail.DataSource = dt_RInsert;
                    //  Grid_Read_Email_Bind();
                }
            }
        

        }


        private void Grid_Read_Email_Bind()
        {

            Hashtable ht_Grid_ReadMail = new Hashtable();
            DataTable dt_Grid_ReadMail= new DataTable();
            ht_Grid_ReadMail.Add("@Trans", "SELECT_GRID_BIND_READ_EMAIL");
            ht_Grid_ReadMail.Add("@Order_Id", Order_Id);
            ht_Grid_ReadMail.Add("@User_Id", User_Id);
            //  ht_Grid.Add("@Email_Attachment_Id", Email_Attachment_Id);
            dt_Grid_ReadMail = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Recieval_Details", ht_Grid_ReadMail);
            if (dt_Grid_ReadMail.Rows.Count > 0)
            {
                GridView_Read_Mail.Rows.Clear();
                for (int i = 0; i < dt_Grid_ReadMail.Rows.Count; i++)
                {
                    GridView_Read_Mail.Rows.Add();
                    GridView_Read_Mail.Rows[i].Cells[0].Value = dt_Grid_ReadMail.Rows[i]["Ab_Order_Email_ID"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[1].Value = dt_Grid_ReadMail.Rows[i]["Subject"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[2].Value = dt_Grid_ReadMail.Rows[i]["To"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[3].Value = dt_Grid_ReadMail.Rows[i]["User_Name"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[4].Value = dt_Grid_ReadMail.Rows[i]["Send_Date"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[6].Value = dt_Grid_ReadMail.Rows[i]["Message"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[5].Value = dt_Grid_ReadMail.Rows[i]["Order_Id"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[7].Value = dt_Grid_ReadMail.Rows[i]["File_Name"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[8].Value = dt_Grid_ReadMail.Rows[i]["Cc"].ToString();
                    GridView_Read_Mail.Rows[i].Cells[9].Value = dt_Grid_ReadMail.Rows[i]["From"].ToString();
                }

            }
            else
            {
                GridView_Read_Mail.Rows.Clear();
                GridView_Read_Mail.Visible = true;
                GridView_Read_Mail.DataSource = null;
            }

          
        }

        private void GridView_Read_Mail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
               // Ab_Order_Email_Number = int.Parse(GridView_Read_Mail.Rows[e.RowIndex].Cells[0].Value.ToString());
                Ab_Order_Email_ID = int.Parse(GridView_Read_Mail.Rows[e.RowIndex].Cells[0].Value.ToString());
                Order_Id = int.Parse(GridView_Read_Mail.Rows[e.RowIndex].Cells[5].Value.ToString());

                Hashtable ht_read_email = new Hashtable();
                DataTable dt_read_email = new DataTable();

                ht_read_email.Add("@Trans", "SELECT_READ_EMAIL_DETAIL_BY_ID");
                ht_read_email.Add("@Order_Id", Order_Id);
               // ht_read_email.Add("@Ab_Order_Email_Number", Ab_Order_Email_Number);
                 ht_read_email.Add("@Ab_Order_Email_ID", Ab_Order_Email_ID);
                dt_read_email = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Recieval_Details", ht_read_email);
                if (dt_read_email.Rows.Count > 0)
                {
                    txt_Abstractor_Subject.Text = dt_read_email.Rows[0]["Subject"].ToString();
                    txt_Abstractor_To.Text = dt_read_email.Rows[0]["TO"].ToString();
                    txt_Abstractor_Cc.Text = dt_read_email.Rows[0]["Cc"].ToString();
                    Abstractor_Rich_Email_Body.Text = dt_read_email.Rows[0]["Message"].ToString();


                    listView1.Items.Clear();

                    Hashtable ht_Document_Select = new Hashtable();
                    System.Data.DataTable dt_Document_Select = new System.Data.DataTable();


                    ht_Document_Select.Add("@Trans", "SELECT_LISTVIEW_READ_FILES");

                    ht_Document_Select.Add("@Order_Id", Order_Id);

                    ht_Document_Select.Add("@Ab_Order_Email_ID", Ab_Order_Email_Number);

                    dt_Document_Select = dataaccess.ExecuteSP("Sp_Abstractor_Order_Email_Recieval_Details", ht_Document_Select);

                    listView1.Items.Clear();

                    listView1.FullRowSelect = true;
                    for (int i = 0; i < dt_Document_Select.Rows.Count; i++)
                    {
                        DataRow dr = dt_Document_Select.Rows[i];
                        ListViewItem listitem = new ListViewItem(dr[2].ToString());

                        listitem.SubItems.Add("Delete");
                        listitem.SubItems.Add(dr[0].ToString());


                        //listitem.SubItems.Add(dr[0].ToString());
                        listView1.Items.Add(listitem);
                    }


                }
            }
        }







  
       
    }
}
