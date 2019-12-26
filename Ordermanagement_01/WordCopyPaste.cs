using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Runtime.InteropServices;
using System.IO;
using System.DirectoryServices;
using System.Collections;
using ClosedXML.Excel;
using Spire.Doc;
using System.Management;
using System.Diagnostics;

using Spire.Pdf;
using Spire;



namespace Ordermanagement_01
{
    public partial class WordCopyPaste : Form
    {

        DataAccess dataaccess = new DataAccess();


        public WordCopyPaste()
        {
            InitializeComponent();
        }
        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }
        private  void CopyWordDoc()
        {
            var fileName = "P1.docx";
            Object oMissing = System.Reflection.Missing.Value;
            //var oTemplatePath = @"C:\Documents\wrkDocuments\" + fileName;
            var oTemplatePath = @"C:\Downloads\" + fileName;
            var wordApp = new Word.Application();
          
            var originalDoc = wordApp.Documents.Open(@oTemplatePath);
          
            // you can do the line above by passing ReadOnly=False like this as well
            //var originalDoc = wordApp.Documents.Open(@oTemplatePath, oMissing, false);
            

            originalDoc.ActiveWindow.Selection.WholeStory();
          

            originalDoc.ActiveWindow.Selection.Copy();
            //copy the sourcefile to Destination to  Pate
            originalDoc.Close();
            var fileName1 = "P2.docx";
            var cTemplatePath = @"C:\Downloads\" + fileName1;

        
            var Distfilepath = @"C:\Downloads\Content\" + fileName1;


            //if (Directory.Exists(Distfilepath))
            //{

            //    Directory.Delete(Distfilepath);
            //}

            //else if (!Directory.Exists(Distfilepath))
            //{
            //    Directory.CreateDirectory(Distfilepath);
            //    File.Copy(cTemplatePath, Distfilepath);
            //}
        

          

            //Pasting the Content to Destnation Path

            var Destinationdoc = wordApp.Documents.Open(@Distfilepath);

           
            Destinationdoc.ActiveWindow.Selection.WholeStory();

            Destinationdoc.ActiveWindow.Selection.Paste();

         
            
            

            Destinationdoc.SaveAs(@Distfilepath);
            Destinationdoc.Save();
            Destinationdoc.Close();

            //var copingdoc= wordApp.Documents.Open(@cTemplatePath);
        
            //copingdoc.ActiveWindow.Selection.Paste();
            //copingdoc.SaveAs(@"C:\Downloads\T2.docx");
            //Create Now document and Paste
            //var newDocument = new Word.Document();
            //newDocument.ActiveWindow.Selection.Paste();

           

            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(originalDoc);
           // System.Runtime.InteropServices.Marshal.ReleaseComObject(newDocument);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(Destinationdoc);
            GC.Collect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CopyWordDoc();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Hashtable httable = new Hashtable();
            System.Data.DataTable dttable = new System.Data.DataTable();
            httable.Add("@Trans", "SELECT");
            dttable = dataaccess.ExecuteSP("Sp_sample1", httable);

            if (dttable.Rows.Count > 0)
            {

                dataGridView1.DataSource = dttable;

            }

           
        }

        private void Export_ReportData()
        {



            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.HeaderText != "")
                {
                    if (column.ValueType == null)
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();


                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }

            //Exporting to Excel
           string Export_Title_Name = "Order_Allocation";
            string folderPath = "C:\\Temp\\";
           string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, Export_Title_Name.ToString());


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Export_ReportData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
            //wordDocument = appWord.Documents.Open(FileNameTextBox.Text);
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "PDF Documents|*.pdf";
            //try
            //{
            //    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        string ext = System.IO.Path.GetExtension(sfd.FileName);
            //        switch (ext)
            //        {
            //            case ".pdf":
            //                wordDocument.ExportAsFixedFormat(sfd.FileName, WdExportFormat.wdExportFormatPDF);
            //                break;

            //        }
            //        pdfFileName.Text = sfd.FileName;
            //    }
            //    System.Windows.Forms.MessageBox.Show("File Converted Successfully..");
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message);
            //}
            //System.Diagnostics.Process.Start(sfd.FileName); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Spire.Doc.Document doc1 = new Spire.Doc.Document();

            doc1.LoadFromFile(@"C:\Downloads\Tax_Header_Document.docx");
           Spire.Doc.HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;
           Spire.Doc.Document doc2 = new Spire.Doc.Document(@"C:\Downloads\2301-233608- Tax Cert.docx");
            foreach (Spire.Doc.Section section in doc2.Sections)
            {
                foreach (DocumentObject obj in header.ChildObjects)
                {
                    section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                }
            }
            //doc2.SaveToFile(@"C:\Downloads\TAx_Final.docx", Spire.Doc.FileFormat.Docx);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var processName = "Ordermanagement_01.exe";

            var connectoptions = new ConnectionOptions();
            connectoptions.Username = @"WEBSERVER\Administrator";
            connectoptions.Password = "password1$";

            string ipAddress = "192.168.12.33";
            ManagementScope scope = new ManagementScope(@"\\" + ipAddress + @"\root\cimv2", connectoptions);


            // WMI query
            var query = new SelectQuery("select * from Win32_process where name = '" + processName + "'");

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                foreach (ManagementObject process in searcher.Get()) // this is the fixed line
                {
                    process.InvokeMethod("Terminate", null);

                    
                }
            }

         

           
            Console.ReadLine();


        }

        private void button7_Click(object sender, EventArgs e)
        {
      
    



            if (CanPing("WEBSERVER"))
            {
                string remoteMachine = "WEBSERVER";

                ConnectionOptions connOptions = new ConnectionOptions();
                connOptions.Impersonation = ImpersonationLevel.Impersonate;
                connOptions.EnablePrivileges = true;
                connOptions.Username = @"WEBSERVER\Administrator";
                connOptions.Password = "password1$";

                string ipAddress = "192.168.12.33";
                ManagementScope manScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", remoteMachine), connOptions);
                manScope.Connect();

                ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                ManagementPath managementPath = new ManagementPath("Win32_Process");
                ManagementClass processClass = new ManagementClass(manScope, managementPath, objectGetOptions);


                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = @"C:\temp\niranjan1.txt";
                info.Arguments = @"\\" + remoteMachine + @" -i C:\temp\niranjan1.txt";
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                Process p = Process.Start(info);

              ////  inParams["CommandLine"] = "netstat -an | find \":5900\" >D:\temp\niranjan1.txt";
              //  inParams["CommandLine"] = "cmd.exe /c \"netstat -an | find \":5900\" >D:\\temp\\niranjan1.txt\""; 
              //  ManagementBaseObject outParams = processClass.InvokeMethod("Start", inParams, null);

              //  String outMess = "Creation of the process returned: " + outParams["returnValue"] + "\n";
              //  outMess = outMess + "Process ID: " + outParams["processId"];
              //  MessageBox.Show(outMess);
            }
    


        }
        Boolean CanPing(String compi)
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply reply = ping.Send(compi);

            return (reply.Status.ToString() == "Success");
        }



    
        private static void runProcessOnRemoteMachine(string remoteMachine, string strPathToTheExe, string Parameter, string usernameAndDomain, string password)
        {
            try
            {
                if (File.Exists(strPathToTheExe))
                {
                    ConnectionOptions connOptions = new ConnectionOptions();
                    connOptions.Impersonation = ImpersonationLevel.Impersonate;
                    connOptions.EnablePrivileges = true;
                    if (Environment.MachineName.ToUpper() != remoteMachine.ToUpper())
                    {
                        connOptions.Username = @"WEBSERVER\Administrator"; //It should be like domain\username
                        connOptions.Password = "password1$";
                    }
                    ManagementScope manScope = new ManagementScope
                    (String.Format(@"\\{0}\ROOT\CIMV2", remoteMachine), connOptions);
                    manScope.Connect();
                    ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                    ManagementPath managementPath = new ManagementPath("Win32_Process");
                    ManagementClass processClass = new ManagementClass
                    (manScope, managementPath, objectGetOptions);
                    ManagementBaseObject inParams = processClass.GetMethodParameters("Create");
                    inParams["CommandLine"] = strPathToTheExe + " \"" + Parameter + "\"";
                    ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);
                    Console.WriteLine("Creation of the process returned: " + outParams["returnValue"]);
                    Console.WriteLine("Process ID: " + outParams["processId"]);
                }

                else
                {
                    Console.WriteLine("Could not find the executable");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured in method runProcessOnRemoteMachine : " + ex.Message.ToString());
            }
        }


        private static void InitiateProcessOnRemoteMachine(string _remoteMachineName, string executableFileCompletePath, string ParamForExeFile)
        {

            string psexec = @"d:\temp\Niranjan.txt"; //Path to psexec
            //If we need to pass some parameter then we need to pass it with the exe enclose in double coutes else can just use path to executable
            string command = executableFileCompletePath + " \"" + ParamForExeFile + "\"";
            string node = @"\\" + _remoteMachineName;
            string arguments = "-i " + node + " -u " + "UserName" + " -p " + "password" + " " + command; //parameters
            Console.WriteLine(arguments); //The arguments ready for psexec

            Process Psexec = new Process();
            Psexec.StartInfo.FileName = psexec;
            Psexec.StartInfo.Arguments = "-accepteula";
            Psexec.Start();
            System.Threading.Thread.Sleep(3000);

            Process GenTCM = new Process();
            GenTCM.StartInfo.FileName = psexec;
            GenTCM.StartInfo.Arguments = arguments;
            GenTCM.StartInfo.UseShellExecute = false;
            GenTCM.StartInfo.RedirectStandardError = true;
            GenTCM.Start();
            int intTimeOut = 100;
            int Flag = 0;
            while (GenTCM.HasExited == false)
            {
                
                Flag = Flag + 1;
                if (intTimeOut == Flag)
                {
                    Process[] psexecProcess = Process.GetProcessesByName("psexec");
                    foreach (Process p in psexecProcess)
                    { p.Kill(); }
                    break;
                }
            }
            string actualOutput = "";
            string errorOutput = GenTCM.StandardError.ReadToEnd();
            if (errorOutput.Contains("error code") && !errorOutput.Contains("error code 0"))
            {
                string[] output = errorOutput.Split(new[] { '\n' });
                errorOutput = output[5];
                Console.WriteLine("[ERROR] " + errorOutput + "\r\n");
            }
            else
            {
                string[] output = errorOutput.Split(new[] { '\n' });
                actualOutput = output[5];
                actualOutput = actualOutput.Replace("\r", "");
                Console.WriteLine("[SUCCESS] TCM Command is launched.\r\n" + actualOutput);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {






            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(@"\\192.168.12.33\inhouse-tax-documents\Supporting Documents.pdf");
            int no_Of_Pages = doc.Pages.Count;
            string Author_Name = doc.DocumentInformation.Author.ToString();
            string Title = doc.DocumentInformation.Title.ToString();
            doc.DocumentInformation.Author = "Test";
            doc.DocumentInformation.Title = "niranjan";
        

            



            doc.SaveToFile(@"\\192.168.12.33\inhouse-tax-documents\Supporting Documents.pdf");
          
         

        }

        private void WordCopyPaste_Load(object sender, EventArgs e)
        {

        }

    }

}
