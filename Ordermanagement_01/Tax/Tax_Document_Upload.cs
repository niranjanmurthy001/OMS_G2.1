using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.DirectoryServices;
using Spire.Pdf;
using Spire;
using DevExpress.XtraEditors;
using System.Net;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Document_Upload : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Order_Id, User_ID, Order_Number, Task_Id, User_Role;
        string[] FName;
        string File_size, extension;
        DialogResult dialogResult;
        string Document_ID;
        private string client_Id, year, month;
        private string directoryPath;
        private string Ftp_Domain_Name;
        private string Ftp_User_Name;
        private string Ftp_Password;
        private string Upload_Directory;
        private string Ftp_Path;
        private string ftpfullpath;
        public Tax_Document_Upload(string ORDER_ID, string USER_ID, string ORDER_NUMBER, string TASK_ID, string USER_ROLE)
        {
            Order_Id = ORDER_ID;
            User_ID = USER_ID;
            Order_Number = ORDER_NUMBER;
            Task_Id = TASK_ID;
            User_Role = USER_ROLE;
            InitializeComponent();
            lbl_Header.Text = "" + Order_Number + " - Tax Document Upload";
            Text = lbl_Header.Text;
            dbc.Bind_Tax_Document_Type(ddl_document_Type);
            if (User_Role == "1")
            {
                Gridview_bind_Tax_Admin_Side_Document_Upload();
            }
            else
            {
                Gridview_bind_Tax_Employee_Side_Document_Upload();
            }
        }
        private void Tax_Document_Upload_Load(object sender, EventArgs e)
        {
            var ht = new Hashtable();
            ht.Add("@Trans", "GET_CLIENT_DETAILS");
            ht.Add("@Order_Id", Order_Id);
            var dt_client = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", ht);
            if (dt_client != null && dt_client.Rows.Count > 0)
            {
                client_Id = dt_client.Rows[0]["Client_Id"].ToString();
            }
            var dt = dbc.Get_Month_Year();
            if (dt != null && dt.Rows.Count > 0)
            {
                year = dt.Rows[0]["Year"].ToString();
                month = dt.Rows[0]["Month"].ToString();
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
            directoryPath = year + "/" + month + "/" + client_Id + "/" + Order_Id + "";
            CreateDirectory(directoryPath);
            btn_Update.Visible = false;
            btn_Tax_Upload.Visible = true;
        }

        private void CreateDirectory(string directoryPath)
        {
            try
            {
                Upload_Directory = directoryPath;
                string Ftp_Host_Name = Ftp_Domain_Name;
                Ftp_Path = Ftp_Host_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents";
                string[] folderArray = Upload_Directory.Split('/');
                string folderName = "";
                for (int i = 0; i < folderArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(folderArray[i]))
                    {
                        try
                        {
                            folderName = string.IsNullOrEmpty(folderName) ? folderArray[i] : folderName + "/" + folderArray[i];
                            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create("ftp://" + Ftp_Path + "/" + folderName);
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


        private void Gridview_bind_Tax_Admin_Side_Document_Upload()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "GET_TAX_DOCUMENTS_FOR_ADMIN");
            htselect.Add("@Order_Id", Order_Id);
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Grid_Tax_Upload.Columns[0].Width = 30;
                Grid_Tax_Upload.Columns[1].Width = 120;
                Grid_Tax_Upload.Columns[2].Width = 120;
                Grid_Tax_Upload.Columns[3].Width = 80;
                Grid_Tax_Upload.Columns[4].Width = 60;
                Grid_Tax_Upload.Columns[5].Width = 60;
                Grid_Tax_Upload.Columns[6].Width = 60;
                Grid_Tax_Upload.Columns[7].Width = 60;
                Grid_Tax_Upload.Columns[8].Width = 60;
                Grid_Tax_Upload.Columns[9].Width = 60;
                if (dtselect.Rows.Count > 0)
                {
                    Grid_Tax_Upload.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        Grid_Tax_Upload.Rows.Add();
                        string Value = dtselect.Rows[i]["Check_Status"].ToString();
                        if (Value == "True")
                        {
                            Grid_Tax_Upload.Rows[i].Cells[0].Value = true;
                        }
                        else if (Value == "false")
                        {
                            Grid_Tax_Upload.Rows[i].Cells[0].Value = false;
                        }
                        Grid_Tax_Upload.Rows[i].Cells[1].Value = dtselect.Rows[i]["Document_Type"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[2].Value = dtselect.Rows[i]["Instuction"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[3].Value = dtselect.Rows[i]["FileSize"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[4].Value = dtselect.Rows[i]["User_Name"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[5].Value = dtselect.Rows[i]["Tax_Task"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[6].Value = dtselect.Rows[i]["Inserted_date"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[7].Value = "View";
                        Grid_Tax_Upload.Rows[i].Cells[8].Value = "Edit";
                        Grid_Tax_Upload.Rows[i].Cells[9].Value = "Delete";
                        Grid_Tax_Upload.Rows[i].Cells[10].Value = dtselect.Rows[i]["Tax_Document_Upload_Id"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[11].Value = dtselect.Rows[i]["New_Document_Path"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[12].Value = dtselect.Rows[i]["Document_Type_Id"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[13].Value = dtselect.Rows[i]["User_id"].ToString();
                    }
                }
            }
            else
            {
                Grid_Tax_Upload.Rows.Clear();
                Grid_Tax_Upload.DataSource = null;
            }
        }

        private void Gridview_bind_Tax_Employee_Side_Document_Upload()
        {
            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            htselect.Add("@Trans", "GET_TAX_DOCUMENTS_FOR_EMPLOYEE");
            htselect.Add("@Order_Id", Order_Id);
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Grid_Tax_Upload.Columns[0].Width = 30;
                Grid_Tax_Upload.Columns[1].Width = 100;
                Grid_Tax_Upload.Columns[2].Width = 120;
                Grid_Tax_Upload.Columns[3].Width = 80;
                Grid_Tax_Upload.Columns[4].Width = 60;
                Grid_Tax_Upload.Columns[5].Width = 60;
                Grid_Tax_Upload.Columns[6].Width = 60;
                Grid_Tax_Upload.Columns[7].Width = 60;
                Grid_Tax_Upload.Columns[8].Width = 60;
                Grid_Tax_Upload.Columns[9].Width = 60;
                if (dtselect.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Tax_Upload.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        Grid_Tax_Upload.Rows.Add();
                        string Value = dtselect.Rows[i]["Check_Status"].ToString();
                        if (Value == "True")
                        {
                            Grid_Tax_Upload.Rows[i].Cells[0].Value = true;
                        }
                        else if (Value == "false")
                        {
                            Grid_Tax_Upload.Rows[i].Cells[0].Value = false;
                        }
                        Grid_Tax_Upload.Rows[i].Cells[1].Value = dtselect.Rows[i]["Document_Type"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[2].Value = dtselect.Rows[i]["Instuction"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[3].Value = dtselect.Rows[i]["FileSize"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[4].Value = dtselect.Rows[i]["User_Name"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[5].Value = dtselect.Rows[i]["Tax_Task"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[6].Value = dtselect.Rows[i]["Inserted_date"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[7].Value = "View";
                        Grid_Tax_Upload.Rows[i].Cells[8].Value = "Edit";
                        Grid_Tax_Upload.Rows[i].Cells[9].Value = "Delete";
                        Grid_Tax_Upload.Rows[i].Cells[10].Value = dtselect.Rows[i]["Tax_Document_Upload_Id"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[11].Value = dtselect.Rows[i]["New_Document_Path"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[12].Value = dtselect.Rows[i]["Document_Type_Id"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[13].Value = dtselect.Rows[i]["User_id"].ToString();
                    }
                }
            }
            else
            {
                Grid_Tax_Upload.Rows.Clear();
                Grid_Tax_Upload.DataSource = null;
            }
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



        private void btn_Tax_Upload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Tax_Dscription.Text.Trim()))
            {
                MessageBox.Show("Enter Description of Uploading File");
                txt_Tax_Dscription.Focus();
                return;
            }
            if (ddl_document_Type.SelectedIndex < 1)
            {
                MessageBox.Show("Select Document Type");
                ddl_document_Type.Focus();
                return;
            }
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Hashtable htorderkb = new Hashtable();
                DataTable dtorderkb = new DataTable();
                OpenFileDialog op1 = new OpenFileDialog();
                op1.Multiselect = true;
                op1.ShowDialog();
                op1.Filter = "allfiles|*.xls";
                int count = 0;
                foreach (string file in op1.FileNames)
                {
                    FileInfo f = new FileInfo(file);
                    File_size = GetFileSize(f.Length);
                    ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/" + directoryPath + "";
                    string ftpUploadFullPath = "" + ftpfullpath + "/" + f.Name + "";
                    // Checking File Exit or not
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP Address  
                    ftpRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password); // Credentials  
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    HashSet<string> files = new HashSet<string>(); // create list to store directories.   
                    string line = streamReader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        files.Add(line); // Add Each file to the List.  
                        line = streamReader.ReadLine();
                    }
                    if (!files.Contains(f.Name))
                    {
                        FtpWebRequest ftpUpLoadFile = (FtpWebRequest)WebRequest.Create(ftpUploadFullPath);
                        ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                        ftpUpLoadFile.KeepAlive = true;
                        ftpUpLoadFile.UseBinary = true;
                        ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;
                        FileStream fs = File.OpenRead(file);
                        Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                        fs.CopyTo(ftpstream);
                        fs.Close();
                        ftpstream.Close();
                        count++;
                        htorderkb.Clear();
                        dtorderkb.Clear();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Order_Id", Order_Id);
                        htorderkb.Add("@Instuction", txt_Tax_Dscription.Text.ToString());
                        htorderkb.Add("@Document_Path", ftpUploadFullPath);
                        htorderkb.Add("@Document_Type", ddl_document_Type.SelectedValue.ToString());
                        htorderkb.Add("@Tax_Task", Task_Id);
                        htorderkb.Add("@FileSize", File_size);
                        htorderkb.Add("@File_Extension", f.Extension);
                        htorderkb.Add("@Inserted_By", User_ID);
                        htorderkb.Add("@status", "True");
                        htorderkb.Add("@Check_Status", "False");
                        dtorderkb = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htorderkb);
                    }
                    else
                    {
                        throw new WebException("File Already Exists");
                    }
                }
                if (User_Role == "1")
                {
                    Gridview_bind_Tax_Admin_Side_Document_Upload();
                }
                else
                {
                    Gridview_bind_Tax_Employee_Side_Document_Upload();
                }
                txt_Tax_Dscription.Text = "";
                //ddl_document_Type.SelectedIndex = 0;
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(count + " File(s) copied");
            }
            catch (WebException ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Grid_Tax_Upload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 7)
                {
                    try
                    {
                        string Source_Path = Grid_Tax_Upload.Rows[e.RowIndex].Cells[11].Value.ToString();
                        string Document_Type_Id = Grid_Tax_Upload.Rows[e.RowIndex].Cells[12].Value.ToString();
                        // string filename = Path.GetFileName(Source_Path);
                        if (Source_Path != "" && Document_Type_Id != "16")
                        {

                            string fileName = Path.GetFileName(Source_Path).Replace("%20", " ");
                            DownloadFtpFile(fileName, Source_Path);
                        }

                        else if (Document_Type_Id == "16")
                        {
                            string User_Id = Grid_Tax_Upload.Rows[e.RowIndex].Cells[13].Value.ToString();
                            Tax_Order_Note_Pad txnote = new Tax_Order_Note_Pad(int.Parse(Order_Id), int.Parse(Task_Id), 0, int.Parse(User_Id.ToString()), "View_By_User_Wise", Order_Number);
                            txnote.Show();
                        }
                        else
                        {
                            MessageBox.Show("File path not found to download files");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Problem in downloading file");
                    }
                }
                else if (e.ColumnIndex == 8)
                {
                    btn_Update.Visible = true;
                    btn_Tax_Upload.Visible = false;
                    Document_ID = Grid_Tax_Upload.Rows[e.RowIndex].Cells[10].Value.ToString();
                    txt_Tax_Dscription.Text = Grid_Tax_Upload.Rows[e.RowIndex].Cells[2].Value.ToString();
                    ddl_document_Type.SelectedValue = Grid_Tax_Upload.Rows[e.RowIndex].Cells[12].Value.ToString();
                }
                else if (e.ColumnIndex == 9)
                {
                    if (MessageBox.Show("Do you Want to Proceed?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Document_ID = Grid_Tax_Upload.Rows[e.RowIndex].Cells[10].Value.ToString();
                        string Document_Type_Id = Grid_Tax_Upload.Rows[e.RowIndex].Cells[12].Value.ToString();
                        if (Document_Type_Id == "16")
                        {
                            MessageBox.Show("This Document Genrated by System You are not able to Delete; Please Check with Administrator");
                            return;
                        }
                        string filePath = Grid_Tax_Upload.Rows[e.RowIndex].Cells[11].Value.ToString();
                        FtpWebRequest ftRequestDelete = (FtpWebRequest)WebRequest.Create(new Uri(filePath));
                        ftRequestDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                        ftRequestDelete.UseBinary = true;
                        ftRequestDelete.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                        FtpWebResponse response = (FtpWebResponse)ftRequestDelete.GetResponse();
                        if (response.StatusCode == FtpStatusCode.FileActionOK)
                        {
                            Hashtable htdel = new Hashtable();
                            DataTable dtdel = new DataTable();
                            htdel.Add("@Trans", "DELETE");
                            htdel.Add("@Tax_Document_Upload_Id", Document_ID.ToString());
                            dtdel = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htdel);
                            if (User_Role == "1")
                            {
                                Gridview_bind_Tax_Admin_Side_Document_Upload();
                            }
                            else if (User_Role == "2")
                            {
                                Gridview_bind_Tax_Employee_Side_Document_Upload();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete file");
                        }
                    }
                }
            }
        }

        private void DownloadFtpFile_old(string p, string Source_Path)
        {
            try
            {

                string Folder_Path = @"C:\temp\";
                if (!Directory.Exists(Folder_Path))
                {

                    Directory.CreateDirectory(Folder_Path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + p;
                string localPath = @"C:\temp\" + fileName;






                FtpWebRequest reqFTP;
                FileStream outputStream = new FileStream(localPath, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(Source_Path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.CopyTo(outputStream);
                ftpStream.Close();
                response.Close();
                System.Diagnostics.Process.Start(localPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Problem in Downloading Files please Check with Administrator");
            }
        }

        private void DownloadFtpFile(string p, string Source_Path)
        {
            try
            {
                FtpWebRequest reqFTP;
                string Folder_Path = "C:\\OMS\\Temp\\";
                if (!Directory.Exists(Folder_Path))
                {
                    Directory.CreateDirectory(Folder_Path);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss-") + p;
                string localPath = "C:\\OMS\\Temp\\" + "\\" + fileName;
                FileStream outputStream = new FileStream(localPath, FileMode.Create);                
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(Source_Path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
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
                Process.Start(localPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Problem in Downloading Files please Check with Administrator");
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            btn_Update.Visible = false;
            btn_Tax_Upload.Visible = true;
            ddl_document_Type.SelectedIndex = 0;
            txt_Tax_Dscription.Clear();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (ddl_document_Type.SelectedIndex > 0 && txt_Tax_Dscription.Text != "")
            {
                Hashtable htupdate = new Hashtable();
                DataTable dtupdate = new DataTable();
                htupdate.Add("@Trans", "UPDATE_INSTRUCTION");
                htupdate.Add("@Instuction", txt_Tax_Dscription.Text);
                htupdate.Add("@Document_Type", ddl_document_Type.SelectedValue.ToString());
                htupdate.Add("@Tax_Document_Upload_Id", Document_ID);
                dtupdate = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htupdate);
                MessageBox.Show("Description Updated");
                btn_Update.Visible = false;
                btn_Tax_Upload.Visible = true;
                txt_Tax_Dscription.Text = "";
                if (User_Role == "1")
                {
                    Gridview_bind_Tax_Admin_Side_Document_Upload();
                }
                else
                {
                    Gridview_bind_Tax_Employee_Side_Document_Upload();
                }
            }
        }
        private void Grid_Tax_Upload_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    Document_ID = Grid_Tax_Upload.Rows[e.RowIndex].Cells[10].Value.ToString();
                    if ((bool)Grid_Tax_Upload.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                    {
                        Hashtable htupdatecheck = new Hashtable();
                        DataTable dtupdatecheck = new DataTable();
                        htupdatecheck.Add("@Trans", "UPDATE_CHECK_STATUS");
                        htupdatecheck.Add("@Check_Status", "True");
                        htupdatecheck.Add("@Tax_Document_Upload_Id", Document_ID);
                        dtupdatecheck = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htupdatecheck);
                    }
                    else if ((bool)Grid_Tax_Upload.Rows[e.RowIndex].Cells[0].EditedFormattedValue == false)
                    {
                        Hashtable htupdatecheck = new Hashtable();
                        DataTable dtupdatecheck = new DataTable();
                        htupdatecheck.Add("@Trans", "UPDATE_CHECK_STATUS");
                        htupdatecheck.Add("@Check_Status", "False");
                        htupdatecheck.Add("@Tax_Document_Upload_Id", Document_ID);
                        dtupdatecheck = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htupdatecheck);
                    }
                }
            }
        }
    }
}
