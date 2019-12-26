using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
namespace Ordermanagement_01
{
    public partial class Order_Check_List_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dt = new System.Data.DataTable();
        ReportDocument rptDoc = new ReportDocument();
        DataSet ds = new DataSet();
        int userid = 0, Task, Task_Confirm_Id, Order_Status, Order_ID, Check;
         System.Data.DataTable dttaskexport = new  System.Data.DataTable();
         System.Data.DataTable dtDocumentExport = new  System.Data.DataTable();
         TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
         TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
         ConnectionInfo crConnectionInfo = new ConnectionInfo();
         Tables CrTables;
         string VIew_Type;
         int Work_Type_Id;
         string User_Role;
         int Status_ID;
         int User_ID;
        public Order_Check_List_View(int user_id, int ORDER_ID, int ORDER_STTAUS,string VIEW_TYPE,int WORK_TYPE_ID,string USER_ROLE)
        {
           
            InitializeComponent();
            Order_ID = ORDER_ID;
            VIew_Type = VIEW_TYPE;
            Work_Type_Id = WORK_TYPE_ID;
            User_Role = USER_ROLE;
            User_ID = user_id;
            if (ORDER_STTAUS != 0)
            {
                Order_Status = ORDER_STTAUS;
                Status_ID = Order_Status;
            }
            else
            {
                Order_Status = 2;

                Status_ID = Order_Status;

            }
           
         
        }
        //public void Load_Task_Details_Before()
        //{



        //    Hashtable htComments = new Hashtable();
        //     System.Data.DataTable dtComments = new System.Data.DataTable();
        //     dttaskexport.Clear();
        //    htComments.Add("@Trans", "SELECT_AFTER");
        //    htComments.Add("@Order_Status_Id", Order_Status);
        //    htComments.Add("@Order_ID",Order_ID);
        //    dtComments = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htComments);
        //    dttaskexport = dtComments;
        //    if (dtComments.Rows.Count > 0)
        //    {
        //        grd_Error.Rows.Clear();
        //        for (int i = 0; i < dtComments.Rows.Count; i++)
        //        {
        //            grd_Error.Rows.Add();
        //            grd_Error.Rows[i].Cells[0].Value = i + 1;
        //            grd_Error.Rows[i].Cells[1].Value = dtComments.Rows[i]["User_Name"].ToString();
        //            grd_Error.Rows[i].Cells[2].Value = dtComments.Rows[i]["EnteredDate"].ToString();
        //            grd_Error.Rows[i].Cells[3].Value = dtComments.Rows[i]["Confirmation_Message"].ToString();
        //            grd_Error.Rows[i].Cells[4].Value = dtComments.Rows[i]["Confirmed"].ToString();
        //            grd_Error.Rows[i].Cells[5].Value = dtComments.Rows[i]["Reason"].ToString();

        //        }


        //    }
        //    else
        //    {
        //        grd_Error.Rows.Clear();


        //    }


        //}
        //public void Load_Document_Details_Before()
        //{



        //    Hashtable htComments = new Hashtable();
        //     System.Data.DataTable dtComments = new System.Data.DataTable();
        //     dtDocumentExport.Clear();
        //    htComments.Add("@Trans", "SELECT_AFTER");
        //    htComments.Add("@Order_Id", Order_ID);
        //    htComments.Add("@Order_Status", Order_Status);
        //    dtComments = dataaccess.ExecuteSP("Sp_Order_Document_List", htComments);
        //    dtDocumentExport = dtComments;
        //    if (dtComments.Rows.Count > 0)
        //    {
        //        Gridview_Document_List.Rows.Clear();
        //        for (int i = 0; i < dtComments.Rows.Count; i++)
        //        {
        //            Gridview_Document_List.AutoGenerateColumns = false;
                
        //            Gridview_Document_List.Rows.Add();
        //            Gridview_Document_List.Rows[i].Cells[0].Value = i + 1;
        //            Gridview_Document_List.Rows[i].Cells[1].Value = dtComments.Rows[i]["EnteredDate"].ToString();
        //            Gridview_Document_List.Rows[i].Cells[2].Value = dtComments.Rows[i]["User_Name"].ToString();
        //            Gridview_Document_List.Rows[i].Cells[3].Value = dtComments.Rows[i]["Document_List_Name"].ToString();
        //            Gridview_Document_List.Rows[i].Cells[4].Value = dtComments.Rows[i]["No_Of_Documents"].ToString();



        //        }


        //    }
        //    else
        //    {
        //        Gridview_Document_List.Rows.Clear();


        //    }


        //}

        private void CR_Report(int Order_Id, int Status_ID)
        {
            int chk_count = 0;

            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            ht.Add("@Trans", "CHECK_COUNT");
            ht.Add("@Order_Id", Order_Id);
            ht.Add("@Order_Task", Status_ID);
            ht.Add("@Work_Type_Id", Work_Type_Id);
            dt = dataaccess.ExecuteSP("SP_Checklist_Detail_Report", ht);
            if (int.Parse(dt.Rows[0]["COUNT"].ToString()) > 0)
            {
                chk_count = 1;
            }
            else
            {
                chk_count = 0;

            }

            if (chk_count == 0)
            {
                rptDoc = new Reports.CrystalReport.Checklist_Detail_Report();
                Logon_Cr();
                if (Work_Type_Id == 1)
                {
                    rptDoc.SetParameterValue("@Trans", "SELECT_USER_TASK_WISE");
                }
                else if (Work_Type_Id == 2)
                {
                    rptDoc.SetParameterValue("@Trans", "SELECT_USER_REWORK_TASK_WISE");

                }
                else if (Work_Type_Id == 3)
                {
                    rptDoc.SetParameterValue("@Trans", "SELECT_USER_SUPER_QC_TASK_WISE");

                }

            }
            else if (chk_count == 1)
            {
                rptDoc = new Reports.CrystalReport.Checklist_Detail_Report_New();
                Logon_Cr();
                if (Work_Type_Id == 1)
                {
                    rptDoc.SetParameterValue("@Trans", "SELECT_USER_TASK_WISE_NEW");
                }
                else if (Work_Type_Id == 2)
                {
                    rptDoc.SetParameterValue("@Trans", "SELECT_USER_REWORK_TASK_WISE_NEW");

                }
                else if (Work_Type_Id == 3)
                {
                    rptDoc.SetParameterValue("@Trans", "SELECT_USER_SUPER_QC_TASK_WISE_NEW");

                }

            }

            rptDoc.SetParameterValue("@Order_Id", Order_Id);
            rptDoc.SetParameterValue("@Order_Task", Status_ID);

            rptDoc.SetParameterValue("@Log_In_Userid", User_ID);

            rptDoc.SetParameterValue("@Work_Type_Id", Work_Type_Id);

            crystalReportViewer1.ReportSource = rptDoc;

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
            //string Status = "";
            //if (Status_ID == 2)
            //{
            //    Status = "SEARCH_";
            //}
            //else if (Status_ID == 3)
            //{
            //    Status = "SEARCHQC_";
            //}
            //homeFolder = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No + @"\" + Status + "Task_Confirmation.pdf";
            //DirectoryEntry de = new DirectoryEntry(homeFolder, "administrator", "password1$");
            //de.Username = "administrator";
            //de.Password = "password1$";

            //System.IO.Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No);
            //File.Copy(@"\\192.168.12.33\oms\Task_Confirmation.pdf", homeFolder, true);
            //Hashtable htorderkb = new Hashtable();
            //DataTable dtorderkb = new DataTable();

            //htorderkb.Add("@Trans", "INSERT");
            //htorderkb.Add("@Instuction", Status + "TaskConformation");
            //htorderkb.Add("@Order_ID", Order_Id);
            //htorderkb.Add("@Document_Name", Status + "Task_Confirmation.pdf");
            ////htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
            //// htorderkb.Add("@Extension", extension);
            //htorderkb.Add("@Document_Path", homeFolder);
            //htorderkb.Add("@Inserted_By", User_Id);
            //htorderkb.Add("@Inserted_date", DateTime.Now);
            //dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
            //this.Close();
        }
        private void Logon_Cr()
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

           
        }
        private void rbtn_Search_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 2;
            CR_Report(Order_ID,int.Parse(Order_Status.ToString()));
            //Load_Task_Details_Before();
            //Load_Document_Details_Before();
        }

        private void rbt_Search_Qc_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 3;
            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));
            //Load_Task_Details_Before();
            //Load_Document_Details_Before();

        }

        private void rbtn_Typing_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 4;
            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));
           // Load_Task_Details_Before();
        }

        private void rbtn_Typing_Qc_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 7;
            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));
           // Load_Task_Details_Before();
        }

        private void rbtn_Upload_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 12;
            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));
            //Load_Task_Details_Before();
        }

        private void Order_Check_List_View_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            if (Work_Type_Id == 1)
            {
                ddl_Work_Type.SelectedIndex = 0;
            }
            else if (Work_Type_Id == 2)
            {
                ddl_Work_Type.SelectedIndex = 1;

            }
            else if (Work_Type_Id == 3)
            {

                ddl_Work_Type.SelectedIndex = 2;
            }
            //if (Order_Status == 0)
            //{

            //    rbtn_Search.Enabled = true;
            //    rbt_Search_Qc.Enabled = true;
            //    rbtn_Typing.Enabled = true;
            //    rbtn_Typing_Qc.Enabled = true;
            //    rbtn_Upload.Enabled = true;
            //    rbtn_Document_Search.Enabled = true;
            //    rbtn_Document_Search_Qc.Enabled = true;

            //}
            //else if (Order_Status == 2)
            //{
            //    rbtn_Search.Checked = true;
            //    rbtn_Search.Enabled = true;

            //    rbt_Search_Qc.Enabled = false;
            //    rbtn_Typing.Enabled = false;
            //    rbtn_Typing_Qc.Enabled = false;
            //    rbtn_Upload.Enabled = false;

            //  //  Load_Task_Details_Before();

            //    rbtn_Document_Search.Enabled = true;
            //    rbtn_Document_Search_Qc.Enabled = false;
            //   // Load_Document_Details_Before(); 
            //}
            //else if (Order_Status == 3)
            //{
            //    rbt_Search_Qc.Checked = true;
            //    rbtn_Search.Enabled = true;
            //    rbt_Search_Qc.Enabled = true;
            //    rbtn_Typing.Enabled = false;
            //    rbtn_Typing_Qc.Enabled = false;
            //    rbtn_Upload.Enabled = false;
            //   // Load_Task_Details_Before();

            //    rbtn_Document_Search.Enabled = true;
            //    rbtn_Document_Search_Qc.Enabled = true;
            //    //Load_Document_Details_Before(); 
                
            //}
            //else if (Order_Status == 4)
            //{
            //    rbtn_Typing.Checked = true;
            //    rbtn_Search.Enabled = true;
            //    rbt_Search_Qc.Enabled = true;
            //    rbtn_Typing.Enabled = true;
            //    rbtn_Typing_Qc.Enabled = false;
            //    rbtn_Upload.Enabled = false;
            //  //  Load_Task_Details_Before();
            //}
            //else if (Order_Status == 7)
            //{
            //    rbtn_Typing_Qc.Checked = true;
            //    rbtn_Search.Enabled = true;
            //    rbt_Search_Qc.Enabled = true;
            //    rbtn_Typing.Enabled = true;
            //    rbtn_Typing_Qc.Enabled = true;
            //    rbtn_Upload.Enabled = false;
            //   // Load_Task_Details_Before();
            //}
            //else if (Order_Status == 12)
            //{
            //    rbtn_Upload.Checked = true;
            //    rbtn_Search.Enabled = true;
            //    rbt_Search_Qc.Enabled = true;
            //    rbtn_Typing.Enabled = true;
            //    rbtn_Typing_Qc.Enabled = true;
            //    rbtn_Upload.Enabled = true;
            //  //  Load_Task_Details_Before();

            //    rbtn_Document_Search.Enabled = true;
            //    rbtn_Document_Search_Qc.Enabled = true;
            //  //  Load_Document_Details_Before(); 
            //}
            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            
            DataSet dsexport = new DataSet();

            ds.Clear();
            ds.Tables.Add(dttaskexport);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Convert_Dataset_to_Excel();
            }
            ds.Clear();
            dttaskexport.Clear();
        }
        private void Convert_Dataset_to_Excel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = true;
            Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            DataTableCollection collection = ds.Tables;

            for (int i = collection.Count; i > 0; i--)
            {
                Sheets xlSheets = null;
                Worksheet xlWorksheet = null;
                //Create Excel Sheets
                xlSheets = ExcelApp.Worksheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                               Type.Missing, Type.Missing, Type.Missing);

                System.Data.DataTable table = collection[i - 1];
                xlWorksheet.Name = table.TableName;

                for (int j = 1; j < table.Columns.Count + 1; j++)
                {
                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                }

                // Storing Each row and column value to excel sheet
                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 0; l < table.Columns.Count; l++)
                    {
                        ExcelApp.Cells[k + 2, l + 1] =
                        table.Rows[k].ItemArray[l].ToString();
                    }
                }
                ExcelApp.Columns.AutoFit();
            }
            ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
            ExcelApp.Visible = true;

        }

        private void rbtn_Document_Search_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 2;
            
          //  Load_Document_Details_Before();
        }

        private void rbtn_Document_Search_Qc_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 3;
           
            //Load_Document_Details_Before();
        }

        private void btn_Doc_Export_Click(object sender, EventArgs e)
        {
            DataSet dsexport = new DataSet();

            ds.Clear();
            ds.Tables.Add(dtDocumentExport);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Convert_Dataset_to_Excel();
            }
            ds.Clear();
            dttaskexport.Clear();
        }

        private void ddl_Work_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            string work_Type = ddl_Work_Type.Text.Trim().ToString();

            if (work_Type=="Live")
            {

                Work_Type_Id = 1;
            }
            else if (work_Type=="Rework")
            {

                Work_Type_Id = 2;
            }
            else if (work_Type == "SuperQc")
            {

                Work_Type_Id = 3;
                rbtn_Search.Enabled=false;
                rbtn_Final_Qc.Enabled = false;
                rbtn_Typing.Enabled = false;
                rbtn_Exception.Enabled = false;
                rbtn_Upload.Enabled = false;
            }

            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));


        }

        private void rbtn_Final_Qc_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 23;
            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));
        }

        private void rbtn_Exception_CheckedChanged(object sender, EventArgs e)
        {
            Order_Status = 24;
            CR_Report(Order_ID, int.Parse(Order_Status.ToString()));
        }

       
    }
}
