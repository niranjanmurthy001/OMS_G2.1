using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using System.DirectoryServices;
using System.IO;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Configuration;
using DevExpress.XtraSplashScreen;
using System.Net;

namespace Ordermanagement_01.InvoiceRep
{
    public partial class Invoice_Order_Preview : Form
    {
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        int Order_Id;

        string Client_Order_no;
        int Order_Type, State_Id;
        int abstarctor_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;

        int Client_Id;
        int External_Order_Id;
        string User_Role;
        private string Ftp_Domain_Name;
        private string Ftp_User_Name;
        private string Ftp_Password;
        public Invoice_Order_Preview(int ORDER_ID, int CLIENT_ID, int EXTERNAL_ORDER_ID, string USER_ROLE, int State)
        {
            InitializeComponent();

            Order_Id = ORDER_ID;
            Client_Id = CLIENT_ID;
            State_Id = State;
            External_Order_Id = EXTERNAL_ORDER_ID;
            User_Role = USER_ROLE;

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


        }
        private void Invoice_Order_Preview_Load(object sender, EventArgs e)
        {
            if (External_Order_Id == 0)
            {
                try
                {

                    if (Client_Id == 11)
                    {
                        rptDoc = new InvReport.Invoice_Report();
                        Logon_To_Crystal();
                        rptDoc.SetParameterValue("@Order_ID", Order_Id);

                        crViewer.ReportSource = rptDoc;

                    }
                    else if (Client_Id == 55)
                    {
                        rptDoc = new InvReport.Invoice_75000();
                        Logon_To_Crystal();
                        rptDoc.SetParameterValue("@Order_ID", Order_Id);

                        crViewer.ReportSource = rptDoc;
                    }
                    else if (Client_Id == 18 || Client_Id == 19)
                    {
                        rptDoc = new InvoiceRep.InvReport.Invoice_Nssclient();
                        Logon_To_Crystal();
                        rptDoc.SetParameterValue("@Order_ID", Order_Id);
                        crViewer.ReportSource = rptDoc;
                    }
                    else if (Client_Id == 8)
                    {
                        rptDoc = new InvoiceRep.InvReport.Invoice_Netco();
                        Logon_To_Crystal();
                        rptDoc.SetParameterValue("@Order_ID", Order_Id);
                        crViewer.ReportSource = rptDoc;
                    }


                    else if (Client_Id == 4)
                    {

                        if (Client_Id == 4 && State_Id == 31)
                        {
                            rptDoc = new InvoiceRep.InvReport.Invoice_RDCClientNJ();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Order_ID", Order_Id);
                            crViewer.ReportSource = rptDoc;
                        }
                        else
                        {
                            rptDoc = new InvReport.Invoice_RDC_Client();
                            Logon_To_Crystal();
                            rptDoc.SetParameterValue("@Order_ID", Order_Id);
                            crViewer.ReportSource = rptDoc;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Preview Report is not available");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {


                }
            }
            else if (External_Order_Id > 0)
            {
                // for Titlelogy DB title Vendor Client Invoice Reports
                if (Client_Id == 33)
                {
                    rptDoc = new InvoiceRep.InvReport.InvoiceReport_DbTitle();
                    Logon_To_Crystal();
                    rptDoc.SetParameterValue("@Order_Id", External_Order_Id);
                    crViewer.ReportSource = rptDoc;
                }
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
            }
            else
            {
                MessageBox.Show("Ftp File Path was not found; You cannot upload the documents please check with administrator");
            }
        }

        private void btn_New_Invoice_Click(object sender, EventArgs e)
        {
            Export_Report();
        }
        private void Download_Ftp_File(string p, string Source_Path)
        {
            try
            {
                FtpWebRequest reqFTP;
                string Folder_Path = "C:\\OMS\\Temp";
                if (!Directory.Exists(Folder_Path))
                {
                    Directory.CreateDirectory(Folder_Path);
                }
                string localPath = Folder_Path + "\\" + p;
                FileStream outputStream = new FileStream(localPath, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Source_Path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.CopyTo(outputStream);
                ftpStream.Dispose();
                outputStream.Dispose();
                response.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Problem in Downloading Files please Check with Administrator");
            }
        }
        public void Export_Report()
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                rptDoc = new InvReport.Invoice_Report();
                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_ID", Order_Id);
                ExportOptions CrExportOptions;

                string dirTemp = "C:\\OMS\\Temp";
                if (!Directory.Exists(dirTemp))
                {
                    var dirInfo = Directory.CreateDirectory(dirTemp);
                }
                string sourcePath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Invoice_Reports/Invoice.pdf";
                Download_Ftp_File("Invoice.pdf", sourcePath);
                string Abstractor_Name = "00001";
                string File_Name = "0001.pdf";
                string dest_path1 = dirTemp + "\\" + Abstractor_Name + "\\" + File_Name;
                FileInfo newFile = new FileInfo(dest_path1);
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
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_Merge_Click(object sender, EventArgs e)
        {
            string dirTemp = "C:\\OMS\\Temp";
            if (!Directory.Exists(dirTemp))
            {
                var dirInfo = Directory.CreateDirectory(dirTemp);
            }
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_ID", Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htsearch);

            Hashtable htinvoice = new Hashtable();
            DataTable dtinvoice = new DataTable();
            htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
            htinvoice.Add("@Order_ID", Order_Id);
            dtinvoice = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htinvoice);

            DataSet ds = new DataSet();
            ds.Clear();
            ds.Tables.Add(dtinvoice);
            ds.Merge(dtsearch);

            List<PdfReader> readerList = new List<PdfReader>();
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow gvrow1 in table.Rows)
                {
                    string path = gvrow1["New_Document_Path"].ToString();
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Path.GetFileName(path);
                    Download_Ftp_File(FileName, path);
                    FileStream fs = new FileStream(dirTemp + "\\" + FileName, FileMode.Open);
                    PdfReader pf = new PdfReader(fs);
                    readerList.Add(pf);
                }
            }
            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Get instance response output stream to write output file.
            string sourcePath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Invoice_Reports/Invoicemerge.pdf";
            Download_Ftp_File("Invoicemerge.pdf", sourcePath);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(dirTemp + "\\Invoicemerge.pdf", FileMode.Create));
            document.Open();

            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();
        }

        public static void MergePdfFiles(string destinationfile, List<string> files)
        {

            Document document = null;

            try
            {
                List<PdfReader> readers = new List<PdfReader>();
                List<int> pages = new List<int>();


                foreach (string file in files)
                {
                    readers.Add(new PdfReader(file));
                }

                document = new Document(readers[0].GetPageSizeWithRotation(1));

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationfile, FileMode.Create));

                document.Open();

                foreach (PdfReader reader in readers)
                {
                    pages.Add(reader.NumberOfPages);
                    WritePage(reader, document, writer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error occurred");
            }
            finally
            {
                document.Close();
            }
        }

        private static void WritePage(PdfReader reader, iTextSharp.text.Document document, PdfWriter writer)
        {
            try
            {
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;

                int rotation = 0;

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    document.SetPageSize(reader.GetPageSizeWithRotation(i));
                    document.NewPage();

                    page = writer.GetImportedPage(reader, i);

                    rotation = reader.GetPageRotation(i);

                    if (rotation == 90 || rotation == 270)
                    {
                        cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                    }
                    else
                    {
                        cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error occurred");
            }
        }
    }
}
