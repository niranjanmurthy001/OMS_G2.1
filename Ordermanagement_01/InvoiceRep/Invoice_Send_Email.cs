using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ordermanagement_01.InvoiceRep
{
    public partial class Invoice_Send_Email : Form
    {
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id;
        string[] FName;
        string Document_Name;
        string Client_Order_no;
        int Order_Type;
        int abstarctor_id, User_Id;
        string Path1;
        string Merge_Attach_Path = "";
        string Attachment_Name;
        string Directory_Path;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        string Order_Number;
        Tables CrTables;
        string Email, Alternative_Email;
        int Invoice_Id;
        string View_File_Path;
        string Invoice_Status;
        DialogResult dialogResult;
        string Forms;
        string Package = "";
        string P1, P2;
        string FP1, FP2;
        string TP1, TP2;
        string TF1, TF2;
        string TFOut_Put, FTp_File_Out_Put;
        string Montly_Invoice_File_Out_Put_Path;
        int Index;
        int Sub_Process_ID;
        NetworkCredential NetworkCred;
        string From_Date, To_date;
        string Invoice_Month_Name;
        int Invoice_Attchment_Type_Id;
        string Search_Document_Path, Invoice_Document_Path;
        string Search_Attachment_Name, Invoice_Attachment_Name;
        int Client_Id;
        string Ftp_Domain_Name, Ftp_User_Name, Ftp_Password;
        string Month, Year;
        string Destination_Path;
        string Temp_Source;
        DataTable dt_Email_Details = new DataTable();
        public Invoice_Send_Email(string ORDER_NUMBER, int USER_ID, int ORDER_ID, int INVOICE_ID, string INVOICE_STATUS, string FORM, int SUB_PROCESS_ID)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            User_Id = USER_ID;

            Forms = FORM.ToString();
            Order_Number = ORDER_NUMBER.ToString();

            Invoice_Id = INVOICE_ID;
            //Client_Id = CLIENT_ID;
            Invoice_Status = INVOICE_STATUS;
            Sub_Process_ID = SUB_PROCESS_ID;

            Hashtable htdate = new Hashtable();
            DataTable dtdate = new DataTable();

            htdate.Add("@Trans", "SELECT");
            htdate.Add("@Sub_Process_Id", Sub_Process_ID);
            dtdate = dataaccess.ExecuteSP("Sp_Client_Mail", htdate);
            if (dtdate.Rows.Count > 0)
            {
                Client_Id = int.Parse(dtdate.Rows[0]["Client_Id"].ToString());
            }
            DataTable dt_month_year = dbc.Get_Month_Year_Details();
            if (dt_month_year.Rows.Count > 0)
            {
                Month = dt_month_year.Rows[0]["Month"].ToString();
                Year = dt_month_year.Rows[0]["Year"].ToString();
            }

            DataTable dt_ftp_Details = dbc.Get_Ftp_Details();

            if (dt_ftp_Details.Rows.Count > 0)
            {
                Ftp_Domain_Name = dt_ftp_Details.Rows[0]["Ftp_Host_Name"].ToString();

                Ftp_User_Name = dt_ftp_Details.Rows[0]["Ftp_User_Name"].ToString();

                string Ftp_pass = dt_ftp_Details.Rows[0]["Ftp_Password"].ToString();

                if (Ftp_pass != "")
                {
                    Ftp_Password = dbc.Decrypt(Ftp_pass);
                }
                Get_Invoice_Attachment_Type();
                if (Forms == "Order_Invoice")
                {
                    if (Invoice_Attchment_Type_Id == 1)
                    {
                        if (Client_Id == 11)
                        {

                            Export_Report();
                            Merge_Document();
                        }
                    }
                    else if (Invoice_Attchment_Type_Id == 2)
                    {
                        if (Client_Id == 11)
                        {
                            Export_Report();
                            NotCombine_Document();
                        }
                    }
                    else if (Invoice_Attchment_Type_Id == 3)
                    {
                        Get_Search_package();
                    }
                }
                else if (Forms == "Monthly_Invoice")
                {
                    Export_Monthly_Invoice_Report();
                    Get_From_To_Date();
                    Send_Html_Email_Body();
                }

                if (Forms == "Order_Invoice")
                {
                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();
                    htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
                    htsearch.Add("@Order_ID", Order_Id);
                    dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
                    if (dtsearch.Rows.Count > 0)
                    {
                        Send_Html_Email_Body();
                    }
                    else
                    {
                        MessageBox.Show("SearchPackage is Not Added Please Check it");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ftp File Path was not found; You cannot upload the documents please check with administrator");
            }
        }

        public void Merge_Document()
        {
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_ID", Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
            if (dtsearch.Rows.Count > 0)
            {
                // P2 = dtsearch.Rows[0]["Document_Path"].ToString();
                FP2 = dtsearch.Rows[0]["New_Document_Path"].ToString();
                string File_Name = Path.GetFileName(FP2.ToString());
                FName = File_Name.ToString().Split('\\');
                TF2 = File_Name.Replace("%20", " "); ;
                string Date_Time_Now = DateTime.Now.ToString("dd");
                // this is invoice folder which is created in client pc with hidden
                if (Directory.Exists(@"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now))
                {
                    TP2 = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                }
                else
                {
                    TP2 = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                    DirectoryInfo di = Directory.CreateDirectory(TP2);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                Download_Temp_File_Path(TF2, FP2, TP2);
            }

            Hashtable htinvoice = new Hashtable();
            DataTable dtinvoice = new DataTable();
            htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
            htinvoice.Add("@Order_ID", Order_Id);
            dtinvoice = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htinvoice);
            if (dtinvoice.Rows.Count > 0)
            {
                FP1 = dtinvoice.Rows[0]["New_Document_Path"].ToString();
                string File_Name = Path.GetFileName(FP1);
                // Download to Temp path 
                //TF1 = File_Name.ToString();
                //FName = FP1.ToString().Split('\\');
                //string File_Name1 = FName.ToString();
                // Download to Temp path 
                TF1 = File_Name.Replace("%20", " "); ;
                string Date_Time_Now = DateTime.Now.ToString("dd");
                // this is invoice folder which is created in client pc with hidden
                if (Directory.Exists(@"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now))
                {
                    TP1 = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                }
                else
                {
                    TP1 = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                    DirectoryInfo di = Directory.CreateDirectory(TP1);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                Download_Temp_File_Path(TF1, FP1, TP1);
            }

            DataSet ds = new DataSet();
            ds.Clear();
            if (Invoice_Status == "True")
            {
                //ds.Tables.Add(dtinvoice);
                //ds.Merge(dtsearch);
            }
            else if (Invoice_Status == "False")
            {
                ////ds.Tables.Add(dtsearch);
            }
            if (Invoice_Status == "True")
            {
                if (dtsearch.Rows.Count > 0)
                {
                    //Define a new output document and its size, type
                    Package = "InvoiceAndSearch";
                    Merge_Invoice_Search();
                }
                else
                {
                    MessageBox.Show("SearchPackage is Not Added Please Check it");
                }
            }
            else if (Invoice_Status == "False")
            {
                if (dtsearch.Rows.Count > 0)
                {
                    Package = "Search";
                    Merge_Invoice_Search();
                }
                else
                {
                    MessageBox.Show("Search package is not uploaded check it");
                }
            }
        }

        public void NotCombine_Document()
        {
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_ID", Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
            if (dtsearch.Rows.Count > 0)
            {
                P2 = dtsearch.Rows[0]["New_Document_Path"].ToString();
                Search_Document_Path = P2.ToString();
            }
            Hashtable htinvoice = new Hashtable();
            DataTable dtinvoice = new DataTable();
            htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
            htinvoice.Add("@Order_ID", Order_Id);
            dtinvoice = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htinvoice);
            if (dtinvoice.Rows.Count > 0)
            {
                P1 = dtinvoice.Rows[0]["New_Document_Path"].ToString();
                Invoice_Document_Path = P1.ToString();
            }
            DataSet ds = new DataSet();
            ds.Clear();
            if (Invoice_Status == "True")
            {
                //ds.Tables.Add(dtinvoice);
                //ds.Merge(dtsearch);
            }
            else if (Invoice_Status == "False")
            {
                ////ds.Tables.Add(dtsearch);
            }
            if (Invoice_Status == "True")
            {
                if (dtsearch.Rows.Count > 0)
                {
                    //Define a new output document and its size, type
                    Package = "InvoiceAndSearch";
                }
                else
                {
                    MessageBox.Show("SearchPackage is Not Added Please Check it");
                }
            }
            else if (Invoice_Status == "False")
            {
                if (dtsearch.Rows.Count > 0)
                {
                    Package = "Search";
                }
                else
                {
                    MessageBox.Show("Search package is not uploaded check it");
                }
            }
        }

        public void Get_Search_package()
        {
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_ID", Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);
            if (dtsearch.Rows.Count > 0)
            {
                P2 = dtsearch.Rows[0]["New_Document_Path"].ToString();
                Search_Document_Path = P2.ToString();
            }
        }
        public void Merge_Invoice_Search()
        {
            // This is Merging From Temp_path

            //lstFiles[0] = @"C:/Users/DRNASM0001/Desktop/15-59989-Search Package.pdf";
            //lstFiles[1] = @"C:/Users/DRNASM0001/Desktop/Invoice.pdf";
            if (Invoice_Status == "True" && Package == "InvoiceAndSearch")
            {
                Index = 3;
            }
            else if (Invoice_Status == "False" && Package == "Search")
            {
                Index = 2;
            }
            string[] lstFiles = new string[Index];
            if (Invoice_Status == "True" && Package == "InvoiceAndSearch")
            {
                lstFiles[0] = TP1 + "\\" + TF1;// temp Path+File
                lstFiles[1] = TP2 + "\\" + TF2;//temp Path+File
            }
            else if (Invoice_Status == "False" && Package == "Search")
            {
                lstFiles[0] = TP2 + "\\" + TF2;// temp Path+File
            }
            //lstFiles[2] = @"C:/pdf/3.pdf";
            // Download Sample_File_From_Server
            Download_Ftp_File("Invoicemerge.pdf", "ftp://titlelogy.com/Ftp_Application_Files/OMS/Invoice_Reports/Invoicemerge.pdf");
            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;

            // Exporting to Pdf From Source Path            
            string outputPdfPath = Temp_Source + "\\" + "Invoicemerge.pdf";// temp Path+File
            TFOut_Put = Temp_Source + "\\" + "Invoicemerge.pdf"; ;
            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new FileStream(outputPdfPath, FileMode.Create));
            //Open the output file
            sourceDocument.Open();
            try
            {
                //Loop through the files list
                for (int f = 0; f < lstFiles.Length - 1; f++)
                {
                    int pages = get_pageCcount(lstFiles[f]);
                    reader = new PdfReader(lstFiles[f]);
                    //Add pages of current file
                    for (int i = 1; i <= pages; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }
                    reader.Close();
                }
                //At the end save the output file
                sourceDocument.Close();
                // Uploading this File to Ftp_Server
                string FileName = "Invoicemerge.pdf";
                string filePath = Temp_Source + "\\" + FileName;
                string file = FileName;

                if (Invoice_Attchment_Type_Id == 1)
                {

                    Merge_Attach_Path = Destination_Path + "/" + file;
                }

                Upload_Ftp_File_to_serevr("Invoicemerge.pdf");

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Get_From_To_Date()
        {

            Hashtable htdate = new Hashtable();
            DataTable dtdate = new DataTable();
            htdate.Add("@Trans", "GET_FROM_TO_DATE");
            htdate.Add("@MonthlyInvoice_Id", Invoice_Id);
            dtdate = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htdate);
            if (dtdate.Rows.Count > 0)
            {


                From_Date = dtdate.Rows[0]["Invoice_From_date"].ToString();
                To_date = dtdate.Rows[0]["Invoice_To_Date"].ToString();
                Invoice_Month_Name = dtdate.Rows[0]["Month_Name"].ToString();
            }
        }

        private int get_pageCcount(string file)
        {
            PdfReader pdfReader = new PdfReader(File.OpenRead(file));
            int numberOfPages = pdfReader.NumberOfPages;
            //return matches.Count;

            return numberOfPages;
        }
        public void Logon_To_Crystal()
        {
            List<string> cl_Lgoin = Comclass.Crystal_report_Login();
            crConnectionInfo.ServerName = cl_Lgoin[0].ToString();
            crConnectionInfo.DatabaseName = cl_Lgoin[1].ToString();
            crConnectionInfo.UserID = cl_Lgoin[2].ToString();
            crConnectionInfo.Password = cl_Lgoin[3].ToString();
            CrTables = rptDoc.Database.Tables;

            foreach (Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            foreach (ReportDocument sr in rptDoc.Subreports)
            {
                foreach (Table CrTable in sr.Database.Tables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);

                }
            }


        }

        public void Export_Report()
        {

            rptDoc = new InvoiceRep.InvReport.Invoice_Report();

            Logon_To_Crystal();

            rptDoc.SetParameterValue("@Order_ID", Order_Id);
            rptDoc.SetParameterValue("User_Role", "1"); // this is for User Role --Client and Subclient Name are visable in crystal Report

            ExportOptions CrExportOptions;
            string Invoice_Order_Number = Order_Id.ToString();

            string File_Name = "" + Order_Id + "- Invoice.pdf";
            // string Temp_File_Name = "0x0100" +Order_Id +".pdf";

            Download_Ftp_File(File_Name, "ftp://titlelogy.com/Ftp_Application_Files/OMS/Invoice_Reports/Invoice.pdf");

            string Ftp_Directory = Year + "/" + Month + "/" + Client_Id + "/" + Order_Id + "";

            Destination_Path = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Invoice_Reports/" + Ftp_Directory;

            Create_Ftp_Directory(Ftp_Directory, "Ftp_Application_Files/OMS/Invoice_Reports");

            Hashtable htpath = new Hashtable();
            DataTable dtpath = new DataTable();

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htcheck);
            int check;
            if (dtcheck.Rows.Count > 0)
            {
                check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {
                check = 0;
            }
            if (check == 0)
            {
                htpath.Add("@Trans", "INSERT");
                htpath.Add("@Order_Id", Order_Id);
                htpath.Add("@Invoice_Id", Invoice_Id);
                htpath.Add("@Document_Path", Destination_Path + "/" + File_Name);
                dtpath = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htpath);
            }

            // This is not Requried we hare copying file to server.

            //Hashtable htgetpath = new Hashtable();
            //DataTable dtgetpath = new DataTable();
            //htgetpath.Add("@Trans", "GET_PATH");
            //htgetpath.Add("@Order_Id", Order_Id);
            //dtgetpath = dataaccess.ExecuteSP("Sp_Order_Invoice_Document_upload", htgetpath);

            //if (dtgetpath.Rows.Count > 0)
            //{
            //    View_File_Path = dtgetpath.Rows[0]["Document_Path"].ToString();
            //}
            // Getting the path from Local System and Exporting To temp Source

            FileInfo newFile = new FileInfo(Temp_Source + "\\" + File_Name);
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
            CrExportOptions = rptDoc.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            rptDoc.Export();
            // Uploading Exporting Local Folder File into Ftp File Folder
            Upload_Ftp_File_to_serevr(File_Name);

            //   Upload_Ftp_File(File_Name);         
        }



        private void Download_Ftp_File(string File_Name, string File_path)
        {
            try
            {

                string Date_Time_Now = DateTime.Now.ToString("dd");


                Date_Time_Now = Date_Time_Now.Replace("/", "");
                Date_Time_Now = Date_Time_Now.Replace(":", "");
                Date_Time_Now = Date_Time_Now.Replace(" ", "");
                Date_Time_Now = Date_Time_Now.Replace("AM", "");
                Date_Time_Now = Date_Time_Now.Replace("PM", "");
                // this is invoice folder which is created in client pc with hidden
                if (Directory.Exists(@"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now))
                {
                    Temp_Source = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                }
                else
                {
                    Temp_Source = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                    DirectoryInfo di = Directory.CreateDirectory(Temp_Source);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }

                FtpWebRequest reqFTP;
                FileStream outputStream = new FileStream("" + Temp_Source + "" + "\\" + File_Name, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(File_path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Dispose();
                outputStream.Dispose();
                response.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in Downloading Files please Check with Administrator");
            }
        }

        private void Download_Temp_File_Path(string File_Name, string Destination_Path, string Source_Temp_Path)
        {
            try
            {
                string Date_Time_Now = DateTime.Now.ToString("dd");
                Date_Time_Now = Date_Time_Now.Replace("/", "");
                Date_Time_Now = Date_Time_Now.Replace(":", "");
                Date_Time_Now = Date_Time_Now.Replace(" ", "");
                Date_Time_Now = Date_Time_Now.Replace("AM", "");
                Date_Time_Now = Date_Time_Now.Replace("PM", "");

                FtpWebRequest reqFTP;
                FileStream outputStream = new FileStream("" + Source_Temp_Path + "" + "\\" + File_Name, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(Destination_Path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Dispose();
                outputStream.Dispose();
                response.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in Downloading Files please Check with Administrator");
            }
        }

        private void Invoice_Send_Email_Load(object sender, EventArgs e)
        {

        }

        private void Download_Ftp_Out_Put_File(string File_Name, string File_path)
        {
            try
            {
                string Date_Time_Now = DateTime.Now.ToString("dd");
                Date_Time_Now = Date_Time_Now.Replace("/", "");
                Date_Time_Now = Date_Time_Now.Replace(":", "");
                Date_Time_Now = Date_Time_Now.Replace(" ", "");
                Date_Time_Now = Date_Time_Now.Replace("AM", "");
                Date_Time_Now = Date_Time_Now.Replace("PM", "");
                // this is invoice folder which is created in client pc with hidden
                if (Directory.Exists(@"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now))
                {
                    Temp_Source = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                }
                else
                {
                    Temp_Source = @"C:\OMS\Temp\0x1200\" + Order_Id + @"\" + Date_Time_Now;
                    DirectoryInfo di = Directory.CreateDirectory(Temp_Source);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                FtpWebRequest reqFTP;
                FileStream outputStream = new FileStream("" + Temp_Source + "" + "\\" + File_Name, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(File_path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in Downloading Files please Check with Administrator");
            }
        }

        private void Create_Ftp_Directory(string Upload_Direcory, string Ftp_Path)
        {
            try
            {
                Upload_Direcory = Upload_Direcory.ToString();
                string Ftp_Host_Name = Ftp_Domain_Name;
                Ftp_Path = Ftp_Host_Name + "/Ftp_Application_Files/OMS/Invoice_Reports";
                string[] folderArray = Upload_Direcory.Split('/');
                string folderName = "";
                for (int i = 0; i < folderArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(folderArray[i]))
                    {
                        try
                        {
                            folderName = string.IsNullOrEmpty(folderName) ? folderArray[i] : folderName + "/" + folderArray[i];
                            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create("ftp://" + Ftp_Path + "/" + folderName);
                            ftp.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
                            FtpWebResponse CreateForderResponse = (FtpWebResponse)ftp.GetResponse();
                            if (CreateForderResponse.StatusCode == FtpStatusCode.PathnameCreated)
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }


        private void Upload_Ftp_File_to_serevr(string FileName)
        {
            try
            {
                // copy file from local to Ftp Server
                // getting the file from Temp Path which has created Once order Opend.
                string filePath = Temp_Source + "\\" + FileName;
                string file = FileName;

           
                FtpWebRequest ftpUpLoadFile = (FtpWebRequest)WebRequest.Create(Destination_Path + "/" + file);
                ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                ftpUpLoadFile.KeepAlive = true;
                ftpUpLoadFile.UseBinary = true;
                ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream fs = File.OpenRead(filePath);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (Exception ex)
            {
                string filePath = Temp_Source;
                string file = FileName;
               
                FtpWebRequest ftpUpLoadFile = (FtpWebRequest)WebRequest.Create(Destination_Path + "/" + file);
                ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                ftpUpLoadFile.KeepAlive = true;
                ftpUpLoadFile.UseBinary = true;
                ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream fs = File.OpenRead(filePath);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
        }

        private void Upload_Ftp_File(string FileName)
        {

            FileInfo fileInf = new FileInfo(Temp_Source + "\\" + FileName);



            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(Destination_Path + "/" + fileInf.Name));

            // Provide the WebPermission Credintials

            reqFTP.Credentials = new NetworkCredential(@"clone\ftpuser", "Power@123");

            // By default KeepAlive is true, where the control connection is not closed

            // after a command is executed.

            reqFTP.KeepAlive = false;

            // Specify the command to be executed.

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.

            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file

            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb

            int buffLength = 2048;

            byte[] buff = new byte[buffLength];

            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded

            FileStream fs = fileInf.OpenRead();

            try
            {

                // Stream to which the file to be upload is written

                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time

                contentLen = fs.Read(buff, 0, buffLength);

                // Until Stream content ends

                while (contentLen != 0)
                {

                    // Write Content from the file stream to the FTP Upload Stream

                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);

                }

                // Close the file stream and the Request Stream

                strm.Close();

                fs.Close();

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Upload Error");

            }
        }
        public void Export_Monthly_Invoice_Report()
        {
            rptDoc = new InvReport.Invoice_Monthly_Report();
            Logon_To_Crystal();
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", Invoice_Id);
            rptDoc.SetParameterValue("@Monthly_Invoice_Id", Invoice_Id, "Individual");
            ExportOptions CrExportOptions;
            // Donwload Sample File From Ftp Server
            string File_Name = "" + Order_Id + "- Monthly_Invoice.pdf";
            Download_Ftp_File(File_Name, "ftp://titlelogy.com/Ftp_Application_Files/OMS/Invoice_Reports/InvoiceMonthly.pdf");
            string Ftp_Directory = Year + "/" + Month + "/" + Client_Id + "/" + Order_Id + "";
            Destination_Path = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Invoice_Reports/" + Ftp_Directory;
            Create_Ftp_Directory(Ftp_Directory, "Ftp_Application_Files/OMS/Invoice_Reports");
            // Taking Monthly invoice Output
            Montly_Invoice_File_Out_Put_Path = Temp_Source + "\\" + File_Name;
            FileInfo newFile = new FileInfo(Temp_Source + "\\" + File_Name);
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



        public void Send_Html_Email_Body()
        {
            using (MailMessage mm = new MailMessage())
            {
                try
                {
                    //LinkedResource imagelink = new LinkedResource(Environment.CurrentDirectory + @"\Drn_Email_Logo.png", "image/png");
                    //imagelink.ContentId = "imageId";
                    //imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    mm.IsBodyHtml = true;
                    string body = this.PopulateBody();
                    if (Forms == "Order_Invoice")
                    {
                        if (Client_Id == 11)
                        {
                            SendHtmlFormattedEmail("neworders@abstractshop.com", "Sample", body);
                        }
                        else if (Client_Id == 12)
                        {
                            SendHtmlFormattedEmail("Ave365@drnds.com", "Sample", body);
                        }
                    }
                    else if (Forms == "Monthly_Invoice")
                    {
                        if (Client_Id == 11)
                        {
                            SendHtmlFormattedEmail("accounts@abstractshop.com", "Sample", body);
                        }
                    }
                    this.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    // return;
                }
            }

        }

        public string PopulateBody()
        {
            Hashtable htdate = new Hashtable();
            DataTable dtdate = new DataTable();
            htdate.Add("@Trans", "SELECT");
            htdate.Add("@Sub_Process_Id", Sub_Process_ID);
            dtdate = dataaccess.ExecuteSP("Sp_Client_Mail", htdate);
            if (dtdate.Rows.Count > 0)
            {
                Client_Id = int.Parse(dtdate.Rows[0]["Client_Id"].ToString());
            }
            string body = string.Empty;

            if (Client_Id == 11)
            {
                if (Forms == "Order_Invoice")
                {
                    body = Read_Body_From_Url("https://titlelogy.com/Ftp_Application_Files/OMS/Oms_Email_Templates/Invoice_Email.htm");
                }
                else if (Forms == "Monthly_Invoice")
                {
                    body = Read_Body_From_Url("https://titlelogy.com/Ftp_Application_Files/OMS/Oms_Email_Templates/Monthly_Invoice_Email.htm");
                }
            }
            else if (Client_Id == 12)
            {
                body = Read_Body_From_Url("https://titlelogy.com/Ftp_Application_Files/OMS/Oms_Email_Templates/Invoice_Avn.htm");
            }
            else
            {
                MessageBox.Show("No Message body found Plese Check with Administrator");
            }


            //Hashtable htorder = new Hashtable();
            //DataTable dtorder = new DataTable();
            //htorder.Add("@Trans", "GET_INVOICE_ORDER_DETAILS_FOR_EMAIL");
            //htorder.Add("@Order_ID", Order_Id);
            //dtorder = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htorder);
            //if (dtorder.Rows.Count > 0)
            //{


            //}
            if (Forms == "Order_Invoice")
            {
                if (Invoice_Status == "True")
                {
                    body = body.Replace("{Text}", "Please find the attached search report.");
                }
                else if (Invoice_Status == "False")
                {
                    body = body.Replace("{Text}", "Please find the attached search report.");
                }
            }
            else if (Forms == "Monthly_Invoice")
            {
                body = body.Replace("{From_Date}", From_Date.ToString());
                body = body.Replace("{To_Date}", To_date.ToString());
            }
            //body = body.Replace("{OrderType}", dtorder.Rows[0]["Order_Type"].ToString());
            //body = body.Replace("{OwnerName}", dtorder.Rows[0]["Borrower_Name"].ToString());
            //body = body.Replace("{Property_Address}", dtorder.Rows[0]["Address"].ToString());
            //body = body.Replace("{County}", dtorder.Rows[0]["County"].ToString());
            return body;
        }

        public string Read_Body_From_Url(string Url)
        {
            WebResponse Result = null;
            string Page_Source_Code;
            WebRequest req = WebRequest.Create(Url);
            Result = req.GetResponse();
            Stream RStream = Result.GetResponseStream();
            StreamReader sr = new StreamReader(RStream);
            new StreamReader(RStream);
            Page_Source_Code = sr.ReadToEnd();
            sr.Dispose();
            string body = Page_Source_Code.ToString();
            return body;
        }
        private async void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            long maxsize;
            long size;
            using (MailMessage mailMessage = new MailMessage())
            {
                try
                {
                    if (Forms == "Order_Invoice")
                    {
                        if (Client_Id == 11)
                        {
                            mailMessage.From = new MailAddress("neworders@abstractshop.com");
                        }
                        else if (Client_Id == 12)
                        {

                            mailMessage.From = new MailAddress("ave365@drnds.com ");
                        }
                        if (Invoice_Attchment_Type_Id == 1)
                        {


                            Path1 = TFOut_Put;// This is Ftp Output File Attaching From Local Pc By Combaining Two Pdf Files


                            Attachment_Name = Order_Number.ToString() + ".pdf";




                        }
                        else if (Invoice_Attchment_Type_Id == 2)
                        {

                            Search_Attachment_Name = Order_Number.ToString() + " - Search Package" + ".pdf";
                            Invoice_Attachment_Name = Order_Number.ToString() + "- Invoice" + ".pdf";

                            Path1 = Search_Document_Path.ToString();

                        }
                        else if (Invoice_Attchment_Type_Id == 3)
                        {
                            Search_Attachment_Name = Order_Number.ToString() + " - Search Package" + ".pdf";
                            Path1 = Search_Document_Path.ToString();

                            Attachment_Name = Search_Attachment_Name.ToString();
                        }

                    }
                    else if (Forms == "Monthly_Invoice")
                    {
                        mailMessage.From = new MailAddress("accounts@abstractshop.com");

                        // Assigning Ftp Exported Montly Invoice Output to path
                        Path1 = Montly_Invoice_File_Out_Put_Path;


                        Attachment_Name = "MonthlyInvoice.pdf";
                    }
                    maxsize = 15 * 1024 * 1000;
                    string fileName;
                    size = 0;
                    if (Invoice_Attchment_Type_Id == 1)
                    {
                        fileName = Path1;
                        FileInfo fi = new FileInfo(fileName);
                        size = fi.Length;
                    }
                    //else
                    //{

                    //  //  size = new FileInfo(Path1).Length; 
                    //}
                    MemoryStream ms = new MemoryStream();
                    MemoryStream ms1 = new MemoryStream();

                    if (size <= maxsize)
                    {
                        if (Invoice_Attchment_Type_Id == 2 && Forms == "Order_Invoice")
                        {
                            // here attaching From Ftp Server path to the Mail

                            // Search Attachment
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Search_Document_Path);
                            request.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            request.Method = WebRequestMethods.Ftp.DownloadFile;
                            Stream contentStream = request.GetResponse().GetResponseStream();
                            Attachment attachment = new Attachment(contentStream, Search_Attachment_Name);
                            mailMessage.Attachments.Add(attachment);

                            // Invoice Attachment

                            FtpWebRequest request_Invoice = (FtpWebRequest)WebRequest.Create(Invoice_Document_Path);
                            request_Invoice.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            request_Invoice.Method = WebRequestMethods.Ftp.DownloadFile;
                            Stream contentStream_Invoice = request_Invoice.GetResponse().GetResponseStream();
                            Attachment attachment_Invoice = new Attachment(contentStream_Invoice, Invoice_Attachment_Name);
                            mailMessage.Attachments.Add(attachment_Invoice);


                            //ms = new MemoryStream(File.ReadAllBytes(Search_Document_Path));
                            //ms1 = new MemoryStream(File.ReadAllBytes(Invoice_Document_Path));
                        }
                        else
                        {


                            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Path1);
                            //request.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            //request.Method = WebRequestMethods.Ftp.DownloadFile;
                            //Stream contentStream = request.GetResponse().GetResponseStream();
                            //Attachment attachment = new Attachment(contentStream, Attachment_Name.ToString());
                            //mailMessage.Attachments.Add(attachment);

                            // ms = new MemoryStream(File.ReadAllBytes(Path1));                        
                        }

                        if (Forms == "Monthly_Invoice")
                        {

                            ms = new MemoryStream(File.ReadAllBytes(Path1));
                            //  FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Path1);
                            //request.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                            //  request.Method = WebRequestMethods.Ftp.DownloadFile;
                            // Stream contentStream = request.GetResponse().GetResponseStream();
                            //  Attachment attachment = new Attachment(contentStream, Attachment_Name.ToString());
                            //  mailMessage.Attachments.Add(attachment);

                            mailMessage.Attachments.Add(new Attachment(ms, Attachment_Name.ToString()));



                        }
                        else if (Forms == "Order_Invoice" && Invoice_Attchment_Type_Id == 1 || Invoice_Attchment_Type_Id == 3)
                        {


                            if (Invoice_Attchment_Type_Id == 1)
                            {

                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Merge_Attach_Path);
                                request.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                Stream contentStream = request.GetResponse().GetResponseStream();
                                Attachment attachment = new Attachment(contentStream, Attachment_Name.ToString());
                                mailMessage.Attachments.Add(attachment);


                                //ms = new MemoryStream(File.ReadAllBytes(Path1));
                                //mailMessage.Attachments.Add(new Attachment(ms, Attachment_Name.ToString()));


                                //  mailMessage.Attachments.Add(new Attachment(ms, Attachment_Name.ToString()));

                            }
                            else if (Invoice_Attchment_Type_Id == 3)
                            {


                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Path1);
                                request.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                request.Method = WebRequestMethods.Ftp.DownloadFile;
                                Stream contentStream = request.GetResponse().GetResponseStream();
                                Attachment attachment = new Attachment(contentStream, Attachment_Name.ToString());
                                mailMessage.Attachments.Add(attachment);

                            }


                        }



                        Hashtable htdate = new Hashtable();
                        DataTable dtdate = new DataTable();
                        htdate.Add("@Trans", "SELECT");
                        htdate.Add("@Sub_Process_Id", Sub_Process_ID);
                        dtdate = dataaccess.ExecuteSP("Sp_Client_Mail", htdate);
                        if (dtdate.Rows.Count > 0)
                        {

                            Email = "Avilable";
                            Alternative_Email = "Avilable";
                            Client_Id = int.Parse(dtdate.Rows[0]["Client_Id"].ToString());

                        }
                        else
                        {

                            Email = "";
                            Alternative_Email = "";
                        }


                        if (Email != "")
                        {


                            for (int j = 0; j < dtdate.Rows.Count; j++)
                            {
                               mailMessage.To.Add(dtdate.Rows[j]["Email-ID"].ToString());

                            }

                          //  mailMessage.To.Add("techteam@drnds.com");


                            if (Forms == "Monthly_Invoice")
                            {
                                if (Client_Id == 11)
                                {
                                     mailMessage.CC.Add("accounts@abstractshop.com");
                                }

                            }
                            else
                            {
                                if (Client_Id == 11)
                                {

                                   mailMessage.CC.Add("neworders@abstractshop.com");
                                }
                                else if (Client_Id == 12)
                                {

                                    mailMessage.CC.Add("ave365@drnds.com");

                                }

                            }

                            if (Forms == "Order_Invoice")
                            {
                                Hashtable htorder = new Hashtable();
                                DataTable dtorder = new DataTable();
                                htorder.Add("@Trans", "GET_INVOICE_ORDER_DETAILS_FOR_EMAIL");
                                htorder.Add("@Order_ID", Order_Id);
                                dtorder = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htorder);
                                if (dtorder.Rows.Count > 0)
                                {


                                }
                                string Title = dtorder.Rows[0]["Order_Type"].ToString();
                                string Subject = "" + Order_Number + "-" + Title.ToString();
                                mailMessage.Subject = Subject.ToString();

                                StringBuilder sb = new StringBuilder();
                                sb.Append("Subject: " + Subject.ToString() + "" + Environment.NewLine);

                            }
                            else if (Forms == "Monthly_Invoice")
                            {
                                string Subject = "Invoice - " + Invoice_Month_Name.ToString();
                                mailMessage.Subject = Subject.ToString();

                            }


                            mailMessage.Body = body;
                            mailMessage.IsBodyHtml = true;



                            SmtpClient smtp = new SmtpClient();



                            if (Forms == "Order_Invoice")
                            {

                                if (Client_Id == 11)
                                {
                                    await Get_Email_Details("neworders@abstractshop.com");
                                    if (dt_Email_Details.Rows.Count > 0)
                                    {
                                        smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                        NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                       
                                     
                                        smtp.Credentials = NetworkCred;
                                        smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());
                                    }
                                    else
                                    {
                                        MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                                    }
                                    //smtp.Host = "smtpout.secureserver.net";
                                    //NetworkCred = new NetworkCredential("neworders@abstractshop.com", "Shopgratman221");
                                    //smtp.Port = 3535;

                                }
                                else if (Client_Id == 12)
                                {
                                    await Get_Email_Details("ave365@drnds.com");
                                    if (dt_Email_Details.Rows.Count > 0)
                                    {
                                        smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                        NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                      
                                        smtp.Credentials = NetworkCred;
                                        smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());
                                    }
                                    else
                                    {
                                        MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                                    }
                                    //smtp.Host = "smtpout.secureserver.net";
                                    //NetworkCred = new NetworkCredential("ave365@drnds.com", "AecXmC9T07DcP$");
                                    //smtp.UseDefaultCredentials = true;
                                    //smtp.Port = 25;
                                }

                            }
                            else if (Forms == "Monthly_Invoice")
                            {
                                if (Client_Id == 11)
                                {
                                    await Get_Email_Details("accounts@abstractshop.com");
                                    if (dt_Email_Details.Rows.Count > 0)
                                    {
                                        smtp.Host = dt_Email_Details.Rows[0]["Outgoing_Mail_Server"].ToString();
                                        NetworkCredential NetworkCred = new NetworkCredential(dt_Email_Details.Rows[0]["User_Name"].ToString(), dt_Email_Details.Rows[0]["Password"].ToString());
                                      
                                        smtp.Credentials = NetworkCred;
                                        smtp.Port = int.Parse(dt_Email_Details.Rows[0]["Outgoing_Server_Port"].ToString());
                                    }
                                    else
                                    {
                                        MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                                    }

                                    //smtp.Host = "smtpout.secureserver.net";
                                    //NetworkCred = new NetworkCredential("accounts@abstractshop.com", "Gratmanshop221");
                                    //smtp.Port = 3535;
                                    //smtp.UseDefaultCredentials = true;
                                }
                            }

                            //  smtp.Timeout = Math.Max(attachments.Sum(Function(Item) (DirectCast(Item, MailAttachment).Size / 1024)), 100) * 1000

                            if (dt_Email_Details.Rows.Count > 0)
                            {
                                smtp.Timeout = (60 * 5 * 1000);
                             
                             

                                smtp.Send(mailMessage);
                                smtp.Dispose();
                                if (Forms == "Order_Invoice")
                                {
                                    Update_Invoice_Email_Status();
                                }
                                else if (Forms == "Monthly_Invoice")
                                {
                                    Update_Monthly_Invoice_Email_Status();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Smtp Credientials Were Not Added To Send an  Email ");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Email is Not Added Kindly Check It");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Attachment Size should less than 10 mb ");
                    }
                }
                catch (Exception ex)
                   {

                    MessageBox.Show(ex.Message);

                }


            }
        }
        public long Ftp_File_Size(string Ftp_File_Path)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(Ftp_File_Path));
            request.Proxy = null;
            request.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            long size = response.ContentLength;
            response.Close();
            return size;
        }
        public void Update_Invoice_Email_Status()
        {
            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new DataTable();
            htupdate.Add("@Trans", "UPDATE_EMAIL_STATUS");
            htupdate.Add("@Order_ID", Order_Id);
            dtupdate = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htupdate);
        }
        public void Update_Monthly_Invoice_Email_Status()
        {
            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new DataTable();
            htupdate.Add("@Trans", "UPDATE_EMAIL_STATUS");
            htupdate.Add("@MonthlyInvoice_Id", Invoice_Id);
            dtupdate = dataaccess.ExecuteSP("Sp_Monthly_Invoice", htupdate);
        }
        public void Get_Invoice_Attachment_Type()
        {
            Hashtable htinv = new Hashtable();
            DataTable dtinv = new DataTable();
            htinv.Add("@Trans", "GET_INOICE_ATACHMENT_TYPE_SUBPORCESSWISE");
            htinv.Add("@Subprocess_Id", Sub_Process_ID);
            dtinv = dataaccess.ExecuteSP("Sp_Client_SubProcess", htinv);
            if (dtinv.Rows.Count > 0)
            {

                Invoice_Attchment_Type_Id = int.Parse(dtinv.Rows[0]["Invoice_Attchement_Type"].ToString());
            }
            else
            {
                Invoice_Attchment_Type_Id = 0;
            }
        }
        private async Task<DataTable> Get_Email_Details(string Email)
        {

            // Call api
            dt_Email_Details.Clear();
            try
            {

                var dictionary = new Dictionary<string, object>()
                                            {
                                               { "@Trans", "SELECT_BY_EMAIL"},
                                               { "@Email_Address",Email},
                                          };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient2 = new HttpClient())
                {
                    var response2 = await httpClient2.PostAsync(Base_Url.Url + "/EmailSettings/BindData", data);
                    if (response2.IsSuccessStatusCode)
                    {
                        if (response2.StatusCode == HttpStatusCode.OK)
                        {
                            var result2 = await response2.Content.ReadAsStringAsync();
                            dt_Email_Details = JsonConvert.DeserializeObject<DataTable>(result2);
                        }
                    }
                }

                // get data

                return dt_Email_Details;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
