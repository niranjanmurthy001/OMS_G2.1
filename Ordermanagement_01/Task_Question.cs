using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.DirectoryServices;
namespace Ordermanagement_01
{
    public partial class Task_Question : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        DataTable dt = new System.Data.DataTable();
        ReportDocument rptDoc = new ReportDocument();
      
        int[] tmp=new int[500];
        string server;
        string Db;
        string Username;
        string Password;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        bool confirmed;
        string Order_No, homeFolder = "";
        string Client_Name;
        string Sub_Client;
        int Question_No = 0, Question_ID = 0, Order_Id, Status_ID, User_Id,Last_Question=0,z=0;
        int order_task;

        public Task_Question(int OrderId, int StatusId, int UserId, string OrderNo, string Client, string Subclient)
        {
            InitializeComponent();
            Order_Id = OrderId;
            Status_ID = StatusId;
            User_Id = UserId;
            Order_No = OrderNo;
            Client_Name = Client;
            Sub_Client = Subclient;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Maste_Question_Load()
        {
           
            ht.Add("@Trans", "SELECT_MASTER");
            dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", ht);
           
        }

        private void Task_Question_Load(object sender, System.EventArgs e)
        {

            SqlConnectionStringBuilder decoder1 = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            server = decoder1.DataSource;
            Db = decoder1.InitialCatalog;
            Username = decoder1.UserID;
            Password = decoder1.Password;
            ddl_Reason.Visible = false;
            txt_Reason.Visible = false;
            ddl_Option.Visible = false;
            lbl_op.Visible = false;
            rbtn_Yes.Visible = false;
            rbtn_No.Visible = false;
            confirmed = true;

            Maste_Question_Load();
            Txt_Question.Text = dt.Rows[0]["Confirmation_Message"].ToString();
            Question_ID = int.Parse(dt.Rows[0]["Task_Confirm_Id"].ToString());
            Frm_Control();
            if (User_Id == 4)
            {
                btn_Skip.Visible = true;
            }
           
        }
        private void Clear()
        {
            txt_Reason.Text = "";
            rbtn_Yes.Checked = true;
            rbtn_No.Checked = false;
            ddl_Option.Text = "";
            ddl_Reason.Text = "";
        }
        private void btn_Submit_Click(object sender, System.EventArgs e)
        {
            
            if (validate() == true)
            {
                if (Question_ID == 1 && confirmed == true)
                {
                    Table_Trans();

                    Question_No = 1;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 1 && confirmed == false && ddl_Reason.Text != "")
                {
                    Table_Trans();
                    Question_No = 1;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 2 && confirmed == true)
                {
                    Table_Trans();
                    Question_No =3;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 2 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 2;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 3)
                {
                    Table_Trans();
                    Question_No = 3;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 4)
                {
                    Table_Trans();
                    Question_No = 4;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 5 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 7;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 5 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 5;
                    Load_Question();
                    Frm_Control();
                }

                else if (Question_ID == 6 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 7;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 6 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 6;
                    Load_Question();
                    Frm_Control();
                }
                    //5.1
                else if (Question_ID == 7 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 7;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 7 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 7;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 7 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 16;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 8 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 16;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 8 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 8;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 9 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 9;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 9 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 9;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 10 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 10;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 10 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 10;
                    Load_Question();
                    Frm_Control();
                }

                else if (Question_ID == 11 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 11;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 11 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 11;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 12 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 12;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 12 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 12;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 13 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 13;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 13 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 13;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 14 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 14;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 14 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 14;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 15 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 15;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 15 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 16;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 16 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 16;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 16 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 16;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 17 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 17;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 17 && confirmed == false)
                {
                    Table_Trans();
                    Question_No =17;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 18 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 18;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 18 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 18;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 19 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 20;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 19 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 23;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 20 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 23;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 20 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 23;
                    Load_Question();
                    Frm_Control();
                }

                else if (Question_ID == 21 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 21;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 21 && confirmed == true)
                {
                    Table_Trans();
                    Question_No =21;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 23 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 19;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 23 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 19;
                    Load_Question();
                    Frm_Control();
                }
               ///////Q7
                else if (Question_ID == 26 && confirmed == true)
                {
                    Table_Trans();
                    Question_No =24;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 26 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 27;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 27 && ddl_Option.Text == "Judicial")
                {
                    Table_Trans();
                    Question_No = 25;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 28 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 27;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 28 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 27;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 27 && ddl_Option.Text == "Non Judicial")
                {
                    Table_Trans();
                    Question_No = 26;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 29 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 27;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 29 && confirmed == false)
                {
                    Table_Trans();
                    Question_No =27;
                    Load_Question();
                    Frm_Control();
                }
                //Q 8
                else if (Question_ID == 30 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 29;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 30 && confirmed == false)
                {
                    Table_Trans();
                    Question_No =28;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 31 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 29;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 31 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 29;
                    Load_Question();
                    Frm_Control();
                }
                // Q 9
                else if (Question_ID == 32 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 30;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 32 && confirmed == false)
                {
                    Table_Trans();
                    Question_No =30;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 33 && ddl_Option.Text == "Section/Land")
                {
                    Table_Trans();
                    Question_No = 33;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 33 && ddl_Option.Text == "Lot/Block")
                {
                    Table_Trans();
                    Question_No = 33;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 33 && ddl_Option.Text == "Lease Hold")
                {
                    Table_Trans();
                    Question_No = 33;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 33 && ddl_Option.Text == "Contractual Agreement")
                {
                    Table_Trans();
                    Question_No = 33;
                    Load_Question();
                    Frm_Control();
                }


                else if (Question_ID == 36 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 49;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 36 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 49;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 39 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 46;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 39 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 46;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 49 && confirmed == false)
                {
                    Table_Trans();
                    Question_No =47;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 49 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 47;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 50 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 34;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 50 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 34;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 37 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 38;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 37 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 38;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 41 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 39;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 41 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 39;
                    Load_Question();
                    Frm_Control();
                }
                ////
               else if   (Question_ID == 42 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 40;
                    Load_Question();
                    Frm_Control();

                }

               else if   (Question_ID == 42 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 41;
                    Load_Question();
                    Frm_Control();

                }
                else if  (Question_ID == 43 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 41;
                    Load_Question();
                    Frm_Control();

                }
              else if (Question_ID == 43 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 41;
                    Load_Question();
                    Frm_Control();

                }
                else if (Question_ID == 44 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 42;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 44 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 43;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 45 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 43;
                    Load_Question();
                    Frm_Control();

                }
                else if (Question_ID == 45 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 43;
                    Load_Question();
                    Frm_Control();
                }
                //else if (Question_ID == 45 && confirmed == false)
                //{
                //    Question_No = 44;
                //    Load_Question();
                //    Frm_Control();

                //}
                //else if (Question_ID == 45 && confirmed == true)
                //{
                //    Question_No = 44;
                //    Load_Question();
                //    Frm_Control();
                //}
                else if (Question_ID == 46 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 44;
                    Load_Question();
                    Frm_Control();

                }
                else if (Question_ID == 46 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 44;
                    Load_Question();
                    Frm_Control();
                }
                else if (Question_ID == 47 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 45;
                    Load_Question();
                    Frm_Control();

                }
                else if (Question_ID == 47 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 45;
                    Load_Question();
                    Frm_Control();
                    //CR_Report();
                    //this.Close();
                }
                else if (Question_ID == 48 && confirmed == true)
                {
                    Table_Trans();
                    Question_No = 45;
                    Load_Question();
                    Frm_Control();
                    Last_Question = 1;
                   // CR_Report();
                    this.Close();
                }
                else if (Question_ID == 48 && confirmed == false)
                {
                    Table_Trans();
                    Question_No = 45;
                    Load_Question();
                    Last_Question = 1;
                    Frm_Control();
                   // CR_Report();
                    this.Close();
                }
             //   tmp = new int[tmp.Length + 1];
                if (z >= 0)
                {
                    tmp[z] = Question_ID;

                }
                z = z + 1;
            }
            Clear();
        }

        private void CR_Report()
        {
            rptDoc = new Task_Report();
            rptDoc.SetParameterValue("@Trans", "Task_Report");
            rptDoc.SetParameterValue("@Order_ID1", Order_Id);
            rptDoc.SetParameterValue("@Status_ID1", Status_ID);
            Logon_Cr();

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = @"\\192.168.12.33\oms\Task_Confirmation.pdf";
            CrExportOptions = rptDoc.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            rptDoc.Export();


            //ExportOptions CrExportOptions;

            //FileInfo newFile = new FileInfo(@"\\192.168.12.33\oms\Task_Confirmation.pdf");
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();

            //PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
            //CrExportOptions = rptDoc.ExportOptions;
            //CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //rptDoc.Export();
            string Status="";
            if(Status_ID==2)
            {
                Status="SEARCH_";
            }
            else if(Status_ID==3)
            {
                Status="SEARCHQC_";
            }
            homeFolder = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No + @"\"+Status+"Task_Confirmation.pdf" ;
            DirectoryEntry de = new DirectoryEntry(homeFolder, "administrator", "password1$");
            de.Username = "administrator";
            de.Password = "password1$";

            System.IO.Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No);
            File.Copy(@"\\192.168.12.33\oms\Task_Confirmation.pdf", homeFolder, true);
            Hashtable htorderkb = new Hashtable();
            DataTable dtorderkb = new DataTable();

            htorderkb.Add("@Trans", "INSERT");
            htorderkb.Add("@Instuction",Status +"TaskConformation");
            htorderkb.Add("@Order_ID", Order_Id);
            htorderkb.Add("@Document_Name",Status + "Task_Confirmation.pdf");
            //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
            // htorderkb.Add("@Extension", extension);
            htorderkb.Add("@Document_Path", homeFolder);
            htorderkb.Add("@Inserted_By", User_Id);
            htorderkb.Add("@Inserted_date", DateTime.Now);
            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
            this.Close();
        }
        private void Logon_Cr()
        {
            crConnectionInfo.ServerName = server;
            crConnectionInfo.DatabaseName = Db;
            crConnectionInfo.UserID = Username;
            crConnectionInfo.Password = Password;
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
        private void Table_Trans_Delete()
        {
             Hashtable ht_Task_Complete = new Hashtable();
            DataTable dt_Task_Complete = new DataTable();
            ht_Task_Complete.Add("@Trans", "Task_Complete");
            ht_Task_Complete.Add("@Order_ID", Order_Id);
            ht_Task_Complete.Add("@Status_ID", Status_ID);
            ht_Task_Complete.Add("@Question_Id", Question_ID);
            dt_Task_Complete = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_Task_Complete);
            if (dt_Task_Complete.Rows.Count > 0)
            {
                Hashtable ht_DELETE = new Hashtable();
                DataTable dt_DELETE = new System.Data.DataTable();
                ht_DELETE.Add("@Trans", "DELETE");
                ht_DELETE.Add("@Order_Id", Order_Id);
                ht_DELETE.Add("@Status_Id", Status_ID);
                ht_DELETE.Add("@User_Id", User_Id);
                ht_DELETE.Add("@Question_Id", Question_ID);
                ht_DELETE.Add("@Modified_By", User_Id);
                dt_DELETE = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_DELETE);
            }
        }
        private void Table_Trans()
        {
            Hashtable ht_Task_Complete = new Hashtable();
            DataTable dt_Task_Complete = new DataTable();
            ht_Task_Complete.Add("@Trans", "Task_Complete");
            ht_Task_Complete.Add("@Order_ID", Order_Id);
            ht_Task_Complete.Add("@Status_ID", Status_ID);
            ht_Task_Complete.Add("@Question_Id", Question_ID);
            dt_Task_Complete = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_Task_Complete);
            if (dt_Task_Complete.Rows.Count <= 0)
            {
                Hashtable ht_INSERT = new Hashtable();
                DataTable dt_INSERT = new System.Data.DataTable();
                ht_INSERT.Add("@Trans", "INSERT");
                ht_INSERT.Add("@Order_Id", Order_Id);
                ht_INSERT.Add("@Status_Id", Status_ID);
                ht_INSERT.Add("@User_Id", User_Id);
                ht_INSERT.Add("@Question_Id", Question_ID);
                ht_INSERT.Add("@YesNo", confirmed);
                if (ddl_Option.Text != "")
                {
                    ht_INSERT.Add("@Option_Id", ddl_Option.SelectedValue.ToString());
                }
                if (ddl_Reason.Text != "")
                {
                    ht_INSERT.Add("@Reason", ddl_Reason.Text);
                }
                if (txt_Reason.Text != "")
                {
                    ht_INSERT.Add("@Reason", txt_Reason.Text);
                }
                ht_INSERT.Add("@Inserted_By", User_Id);
                dt_INSERT = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_INSERT);
            }
            else
            {
                Hashtable ht_UPDATE = new Hashtable();
                DataTable dt_UPDATE = new System.Data.DataTable();
                ht_UPDATE.Add("@Trans", "Update");
                ht_UPDATE.Add("@Order_Id", Order_Id);
                ht_UPDATE.Add("@Status_Id", Status_ID);
                ht_UPDATE.Add("@User_Id", User_Id);
                ht_UPDATE.Add("@Question_Id", Question_ID);
                ht_UPDATE.Add("@YesNo", confirmed);
                if (ddl_Option.Text != "")
                {
                    ht_UPDATE.Add("@Option_Id", ddl_Option.SelectedValue.ToString());
                }
                if (ddl_Reason.Text != "")
                {
                    ht_UPDATE.Add("@Reason", ddl_Reason.Text);
                }
                if (txt_Reason.Text != "")
                {
                    ht_UPDATE.Add("@Reason", txt_Reason.Text);
                }
                ht_UPDATE.Add("@Modified_By", User_Id);
                dt_UPDATE = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_UPDATE);
            }
        }

        private void Txt_Question_TextChanged(object sender, System.EventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Confirmation_Message"].ToString() == Txt_Question.Text)
                {
                    Question_ID = int.Parse(dt.Rows[i]["Task_Confirm_Id"].ToString());
                }
            }
            Frm_Control();
        }
        private void Frm_Control()
        {
            Hashtable ht_ddl = new Hashtable();
            DataTable dt_ddl = new System.Data.DataTable();

            if (Question_ID == 1 )
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
            }

            if (Question_ID == 1 && confirmed == false)
            {
                ddl_Reason.Visible = true;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 1 && confirmed == true)
            {
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
             if (Question_ID == 2)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
             if (Question_ID == 3)
             {
                 ht_ddl.Add("@Trans", "BIND_DDL");
                 ht_ddl.Add("@Task_Confirm_Id", Question_ID);
                 dt_ddl = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_ddl);
                 ddl_Option.DataSource = dt_ddl;
                 ddl_Option.DisplayMember = "Options";
                 ddl_Option.ValueMember = "Option_Id";
                 rbtn_No.Visible = false;
                 rbtn_Yes.Visible = false;
                 ddl_Reason.Visible = true;
                 label2.Visible = true;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = true;
                 lbl_op.Visible = true;

             }
             if (Question_ID == 4)
            {
                rbtn_No.Visible = false;
                rbtn_Yes.Visible = false;
                ddl_Reason.Visible = false;
                label2.Visible = true;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
            }

             if (Question_ID == 5)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 6)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 7)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 7 && confirmed==false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
           
             if (Question_ID == 8)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }

             if (Question_ID == 8 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }


             if (Question_ID == 9)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 9 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 10)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 10 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 11)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 11 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 12)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 12 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 13)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 13 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 14)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 14 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 15)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 15 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 16)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 16 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 17)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }

             if (Question_ID == 17 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 18)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 18 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 19)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 19 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }
             if (Question_ID == 20)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 20 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = true;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = true;
             }

             if (Question_ID == 21)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 21 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }

             if (Question_ID == 23)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }
             if (Question_ID == 23 && confirmed == false)
             {
                 rbtn_No.Visible = true;
                 rbtn_Yes.Visible = true;
                 ddl_Reason.Visible = false;
                 txt_Reason.Visible = false;
                 ddl_Option.Visible = false;
                 lbl_op.Visible = false;
                 label2.Visible = false;
             }


            if (Question_ID == 27)
            {
                ht_ddl.Add("@Trans", "BIND_DDL");
                ht_ddl.Add("@Task_Confirm_Id", Question_ID);
                dt_ddl = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_ddl);
                ddl_Option.DataSource = dt_ddl;
                ddl_Option.DisplayMember = "Options";
                ddl_Option.ValueMember = "Option_Id";

                label2.Visible = false;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = true;
                lbl_op.Visible = true;
                rbtn_Yes.Visible = false;
                rbtn_No.Visible = false;
            }


            if (Question_ID == 28)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
             if (Question_ID == 28 && confirmed==false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }

            if (Question_ID == 29)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 29 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 30)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 31)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 31 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 32)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 32 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 33)
            {
                ht_ddl.Add("@Trans", "BIND_DDL");
                ht_ddl.Add("@Task_Confirm_Id", Question_ID);
                dt_ddl = dataaccess.ExecuteSP("Sp_Task_Conformation_Trans", ht_ddl);
                ddl_Option.DataSource = dt_ddl;
                ddl_Option.DisplayMember = "Options";
                ddl_Option.ValueMember = "Option_Id";
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = true;
                lbl_op.Visible = true;
                rbtn_Yes.Visible = false;
                rbtn_No.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 36)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 36 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 39)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 39 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 49)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 49 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 50)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 50 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 37)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 37 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 42)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 34)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 34 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 35)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 35 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 36)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 36 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 41)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 41 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 43)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 43 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 44)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 45)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 45 && confirmed == false)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = true;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = true;
            }
            if (Question_ID == 46)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 47)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }
            if (Question_ID == 48)
            {
                rbtn_No.Visible = true;
                rbtn_Yes.Visible = true;
                ddl_Reason.Visible = false;
                txt_Reason.Visible = false;
                ddl_Option.Visible = false;
                lbl_op.Visible = false;
                label2.Visible = false;
            }


        }
        private bool validate()
        {

            if (rbtn_Yes.Checked == false && rbtn_No.Checked == false)
            {

                MessageBox.Show("Please Select Yes or No");
                rbtn_Yes.Focus();
                return false;
            }
            if (txt_Reason.Visible == true && txt_Reason.Text == "")
            {

                MessageBox.Show("Please Enter Reason");
                txt_Reason.Focus();
                return false;
            }

            if (ddl_Option.Visible == true && ddl_Option.Text == "")
            {

                MessageBox.Show("Please select Option");
                ddl_Option.Focus();
                return false;
            }
            if (ddl_Reason.Visible == true && ddl_Reason.Text=="")
            {
                MessageBox.Show("Please select Reason");
                ddl_Reason.Focus();
                return false;
            }

            return true;

        }
        private void rbtn_Yes_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbtn_Yes.Checked == true)
            {
                confirmed = true;
                Frm_Control();
            }
        }

      
       
        private void Load_Question()
        {
            Question_ID = int.Parse(dt.Rows[Question_No]["Task_Confirm_Id"].ToString());
            Txt_Question.Text = dt.Rows[Question_No]["Confirmation_Message"].ToString();
        }
        private void Load_DB_Question()
        {
            Hashtable ht_Question = new Hashtable();
            DataTable dt_Question = new DataTable();
           

            ht_Question.Add("@Trans", "SELECT_QUESTION");
            ht_Question.Add("@Task_Confirm_Id", Question_ID);
            dt_Question = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", ht_Question);
            if (dt_Question.Rows.Count > 0)
            {
                Txt_Question.Text = dt_Question.Rows[0]["Confirmation_Message"].ToString();
            }

        }
        private void rbtn_No_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbtn_No.Checked == true)
            {
                confirmed = false;
                Frm_Control();
            }
        }

        private void btn_Skip_Click(object sender, System.EventArgs e)
        {
            Ordermanagement_01.Task_Conformation Taskconfomation = new Ordermanagement_01.Task_Conformation(User_Id, Order_Id, order_task, Status_ID);
            Taskconfomation.ShowDialog();
            this.Close();
        }

        private void btn_Previous_Click(object sender, System.EventArgs e)
        {
            if (z >= 0)
            {
                int[] Question_Array = new int[z];
                int j = 0;
                foreach (int i in tmp)
                {
                    if (z > j)
                    {
                        Question_Array[j] = i;
                        j = j + 1;
                    }
                }

                Table_Trans_Delete();
                Question_Array = Question_Array.Take(Question_Array.Count() - 1).ToArray();
                if (z < 2)
                {
                    Question_ID = 1;
                    Table_Trans_Delete();
                    Load_DB_Question();
                    Frm_Control();
                    z = z - 1;
                }
                if (z >= 2)
                {
                    Question_ID = Question_Array[z - 2];
                    Table_Trans_Delete();
                    Load_DB_Question();
                    Frm_Control();
                    z = z - 1;
                }
            }
            //if (Question_ID == 1)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 0;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 2)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 0;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 2 || Question_ID==3)
            //{
            //    for (int i = 2; i < 4; i++)
            //    {
            //        Question_ID = i;
            //        Table_Trans_Delete();
            //    }
            //    Question_No = 1;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
           
            //if (Question_ID == 4)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 1;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 5)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 3;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 6 || Question_ID == 7 )
            //{

             
            //        Question_ID = 7;
            //        Table_Trans_Delete();
            //        Question_ID = 6;
            //        Table_Trans_Delete();

            //        Question_No = 4;
            //        Load_Question();
            //        Frm_Control();
            //        Table_Trans_Delete();
            //}
            //if (Question_ID == 8)
            //{

            //    Table_Trans_Delete();
            //    Question_No = 4;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 9 || Question_ID == 10 || Question_ID == 11 || Question_ID == 12 || Question_ID == 13 || Question_ID == 14 || Question_ID == 15 || Question_ID == 16)
            //{
            //    Question_ID = 16;
            //    Table_Trans_Delete();
            //    Question_ID = 15;
            //    Table_Trans_Delete();
            //    Question_ID = 14;
            //    Table_Trans_Delete();
            //    Question_ID = 13;
            //    Table_Trans_Delete();
            //    Question_ID = 12;
            //    Table_Trans_Delete();
            //    Question_ID = 11;
            //    Table_Trans_Delete();
            //    Question_ID = 10;
            //    Table_Trans_Delete();
            //    Question_ID = 9;
            //    Table_Trans_Delete();


            //    Question_No = 7;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 17)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 7;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 18 || Question_ID == 19 || Question_ID == 20 || Question_ID == 21 || Question_ID == 23)
            //{
            //    Question_ID = 23;
            //    Table_Trans_Delete();
            //    Question_ID = 21;
            //    Table_Trans_Delete();
            //    Question_ID = 20;
            //    Table_Trans_Delete();
            //    Question_ID = 19;
            //    Table_Trans_Delete();
            //    Question_ID = 18;
            //    Table_Trans_Delete();

            //    Table_Trans_Delete();
            //    Question_No = 16;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 26)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 16;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 27 || Question_ID == 28 || Question_ID == 29)
            //{
            //    Question_ID = 29;
            //    Table_Trans_Delete();
            //    Question_ID = 28;
            //    Table_Trans_Delete();
            //    Question_ID = 27;
            //    Table_Trans_Delete();

               
            //    Question_No = 23;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 30)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 23;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 31 || Question_ID == 32)
            //{
            //    Question_ID = 32;
            //    Table_Trans_Delete();
            //    Question_ID = 30;
            //    Table_Trans_Delete();

              
            //    Question_No = 27;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 33)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 27;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 34 || Question_ID == 35 || Question_ID == 36 || Question_ID == 37 || Question_ID == 38 || Question_ID == 39 || Question_ID == 40 || Question_ID == 41)
            //{

            //    Question_ID = 41;
            //    Table_Trans_Delete();
            //    Question_ID = 40;
            //    Table_Trans_Delete();
            //    Question_ID = 39;
            //    Table_Trans_Delete();
            //    Question_ID = 38;
            //    Table_Trans_Delete();
            //    Question_ID = 37;
            //    Table_Trans_Delete();
            //    Question_ID = 36;
            //    Table_Trans_Delete();
            //    Question_ID = 35;
            //    Table_Trans_Delete();
            //    Question_ID = 34;
            //    Table_Trans_Delete();


               
            //    Question_No = 30;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
           
            //if (Question_ID == 43)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 39;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 42)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 30;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 45)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 41;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 44)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 39;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 46 )
            //{
            //    Table_Trans_Delete();
            //    Question_No = 41;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 47)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 43;
            //    Table_Trans_Delete();
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
            //if (Question_ID == 48)
            //{
            //    Table_Trans_Delete();
            //    Question_No = 44;
            //    Load_Question();
            //    Frm_Control();
            //    Table_Trans_Delete();
            //}
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
