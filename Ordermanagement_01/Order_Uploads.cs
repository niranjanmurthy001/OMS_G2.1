using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using Ordermanagement_01.Tax;
using System.Net;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01
{
    public partial class Order_Uploads : Form
    {

        private ContextMenu contextMenu1;
        private MenuItem copyMenuItem;
        private MenuItem pasteMenuItem;
        private MenuItem refreshMenuItem;
        private string homeFolder = "";
        private string homeDisk = "";
        private FileSystemWatcher fsw;

        const int ALT = 32;
        const int CTRL = 8;

        const int SHIFT = 4;
        int OrderId;
        int userid;
        string Order_No;
        string[] FName;
        string Content_Path;
        string Header_Path;
        string Order_status;
        string Order_Type_Abbrivation, Sub_Process_Id;
        int Typing_Count;
        string File_size;
        int Tax_Order_Task;
        static string dest_path1;
        string User_Role_Id;
        int File_Count, Existance_File_Copied;
        int External_Client_Id, External_Sub_Client_Id;
        string External_Client_Order_Number;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();

        DropDownistBindClass dbc = new DropDownistBindClass();
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        string View_File_Path;
        DialogResult dialogResult;
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        string Client_Name;
        string Sub_Client;
        string Operation;
        string extension;
        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern short GetKeyState(VirtualKeyStates nVirtKey);

        int External_Clinet_Id, External_Sub_client_Id, External_Order_Id;
        int Inhouse_Client_Id, Inhosue_Sub_Client_id;
        string Package = "";
        string P1, P2;
        string Inv_Status;
        int Index;
        ReportDocument rptDoc = new ReportDocument();
        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        string server = "192.168.12.33";
        string database = "TITLELOGY_NEW_OMS";
        string UserID = "sa";
        string password = "password1$";


        string Client_Order_no;

        int User_id;
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        CrystalDecisions.CrystalReports.Engine.Tables CrTables;
        private string year;
        private string month;
        private string Ftp_Domain_Name;
        private string Ftp_User_Name;
        private string Ftp_Password;
        private string Ftp_Path;
        private string mainPath;
        private string ftpfullpath;

        enum VirtualKeyStates : int
        {
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
        }

        public Order_Uploads(string OPERATION, int Order_id, int user_id, string OrderNo, string Client, string Subclient)
        {
            InitializeComponent();
            OrderId = Order_id;
            userid = user_id;
            Order_No = OrderNo;
            Client_Name = Client;
            Sub_Client = Subclient;
            Operation = OPERATION;
        }

        bool IsKeyPressed(VirtualKeyStates testKey)
        {
            bool keyPressed = false;
            short result = GetKeyState(testKey);
            switch (result)
            {
                case 0:
                    keyPressed = false;
                    break;
                case 1:
                    keyPressed = false;
                    break;
                default:
                    keyPressed = true;
                    break;
            }
            return keyPressed;
        }

        private void GetActiveWindowHandle()
        {
            const int nChars = 256;
            int handle = 0;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
        }

        private void pasteMenuItem_Click(object sender, System.EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (!data.GetDataPresent(DataFormats.FileDrop))
                return;
            string[] files = (string[])data.GetData(DataFormats.FileDrop);
            MemoryStream stream = (MemoryStream)data.GetData("Preferred DropEffect", true);
            int flag = stream.ReadByte();
            if (flag != 2 && flag != 5)
                return;
            bool cut = (flag == 2);
            foreach (string file in files)
            {
                string dest = homeFolder + "\\" + Path.GetFileName(file);
                try
                {
                    if (cut)
                        File.Move(file, dest);
                    else
                        File.Copy(file, dest, false);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(this, "Failed to perform the specified operation:\n\n" + ex.Message, "File operation failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            RefreshView();
        }

        private void copyMenuItem_Click(object sender, System.EventArgs e)
        {
            CopyToClipboard(false);
        }

        /// <summary>
        /// Write files to clipboard (from http://blogs.wdevs.com/idecember/archive/2005/10/27/10979.aspx)
        /// </summary>
        /// <param name="cut">True if cut, false if copy</param>
        void CopyToClipboard(bool cut)
        {
            string[] files = GetSelection();
            if (files != null)
            {
                IDataObject data = new DataObject(DataFormats.FileDrop, files);
                MemoryStream memo = new MemoryStream(4);
                byte[] bytes = new byte[] { (byte)(cut ? 2 : 5), 0, 0, 0 };
                memo.Write(bytes, 0, bytes.Length);
                data.SetData("Preferred DropEffect", memo);
                Clipboard.SetDataObject(data);
            }
        }

        private void refreshMenuItem_Click(object sender, System.EventArgs e)
        {
            RefreshView();
        }


        private void Load_External_Client_Order_Client_Details()
        {

            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            ht.Add("@Trans", "GET_EXTERNAL_CLIENT_SUB_CLIENT_ORDER_ID");
            ht.Add("@Order_Id", OrderId);
            dt = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", ht);
            if (dt.Rows.Count > 0)
            {
                External_Clinet_Id = int.Parse(dt.Rows[0]["Clinet_Id"].ToString());
                External_Sub_client_Id = int.Parse(dt.Rows[0]["Sub_Client_Id"].ToString());
                External_Order_Id = int.Parse(dt.Rows[0]["External_Order_Id"].ToString());
                External_Client_Order_Number = dt.Rows[0]["Order_Number"].ToString();
                Inhouse_Client_Id = int.Parse(dt.Rows[0]["Client_Id"].ToString());
                Inhosue_Sub_Client_id = int.Parse(dt.Rows[0]["Subprocess_Id"].ToString());
            }
            else
            {
                External_Clinet_Id = 0;
            }
            Gridview_bind_External_Client_Document_Upload();
        }



        private void Order_Uploads_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;

            var dt = dbc.Get_Month_Year();
            if (dt != null && dt.Rows.Count > 0)
            {
                year = dt.Rows[0]["Year"].ToString();
                month = dt.Rows[0]["Month"].ToString();
            }

            System.Data.DataTable dt_ftp_Details = dbc.Get_Ftp_Details();

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


            //tabControl1.TabPages.Remove(tabPage1);           
            Hashtable htos = new Hashtable();
            System.Data.DataTable dtos = new System.Data.DataTable();
            htos.Add("@Trans", "GET_CURRENT_ORDER_STATUS_OF_ORDER");
            htos.Add("@Order_ID", OrderId);
            dtos = dataaccess.ExecuteSP("Sp_Document_Upload", htos);
            if (dtos.Rows.Count > 0)
            {
                Order_status = dtos.Rows[0]["Order_Status"].ToString();
            }

            Hashtable htuserrole = new Hashtable();
            System.Data.DataTable dtuserrole = new System.Data.DataTable();
            htuserrole.Add("@Trans", "GET_USER_ROLE");
            htuserrole.Add("@User_Id", userid);
            dtuserrole = dataaccess.ExecuteSP("Sp_Document_Upload", htuserrole);

            if (dtuserrole.Rows.Count > 0)
            {
                User_Role_Id = dtuserrole.Rows[0]["User_RoleId"].ToString();
            }

            Hashtable htchup = new Hashtable();
            System.Data.DataTable dtchup = new System.Data.DataTable();
            htchup.Add("@Trans", "CHEK_UPLOAD_TAB_DOCUMENT_TO_SHOW");
            htchup.Add("@Order_ID", OrderId);
            htchup.Add("@order_Status", Order_status);
            htchup.Add("@User_Id", userid);
            dtchup = dataaccess.ExecuteSP("Sp_Document_Upload", htchup);

            int Doccount;
            if (dtchup.Rows.Count > 0)
            {
                Doccount = int.Parse(dtchup.Rows[0]["count"].ToString());
            }
            else
            {
                Doccount = 0;
            }

            if (User_Role_Id == "2" && Doccount > 0)
            {
                tabControl1.TabPages.Add(tabPage1);
            }
            else if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                tabControl1.TabPages.Add(tabPage1);
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage1);
            }

            if (Operation == "Insert")
            {

                Grd_TempDocument_upload_Load();
            }
            else if (Operation == "Update")
            {
                Load_External_Client_Order_Client_Details();
                Grd_Document_upload_Load();
                Gridview_bindInhouse_Final_Document_Upload();
                RefreshView();
            }
            dbc.BindDocumentType(ddl_Dcoument_Type);
            dbc.BindDocumentType(ddl_Inhouse_Doc_Type);

            Get_Order_Type_Abb();
            //try
            //{
            //homeFolder = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId;
            //System.IO.Directory.CreateDirectory(homeFolder);
            //}

            //catch (Exception ex)
            //{
            //  MessageBox.Show(ex.Message);
            //}


            //homeDisk = Path.GetPathRoot(homeFolder).ToUpper();		// C:\ or D:\
            //this.Text = "Files in ";



            //// Raise Event handlers.
            //fsw = new FileSystemWatcher(homeFolder, "*.*");

            //fsw.IncludeSubdirectories = true;
            //// Monitor all changes specified in the NotifyFilters.

            //fsw.NotifyFilter = NotifyFilters.Attributes |
            //               NotifyFilters.CreationTime |
            //               NotifyFilters.DirectoryName |
            //               NotifyFilters.FileName |
            //               NotifyFilters.LastAccess |
            //               NotifyFilters.LastWrite |
            //               NotifyFilters.Security |
            //               NotifyFilters.Size;
            //// Watch on events.
            ////fsw.EnableRaisingEvents = true;
            //fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            //fsw.Deleted += new FileSystemEventHandler(fsw_Changed);
            //fsw.Created += new FileSystemEventHandler(fsw_Changed);



            //  fsw.EnableRaisingEvents = true;
            Hashtable htordertask = new Hashtable();
            System.Data.DataTable dtordertask = new System.Data.DataTable();
            htordertask.Add("@Trans", "GET_ORDER_TASK");
            htordertask.Add("@Order_Id", OrderId);


            dtordertask = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htordertask);

            if (dtordertask.Rows.Count > 0)
            {
                Tax_Order_Task = int.Parse(dtordertask.Rows[0]["Order_Task"].ToString());
            }

            Hashtable httaxcount = new Hashtable();
            System.Data.DataTable dttaxcount = new System.Data.DataTable();
            if (Tax_Order_Task == 21)
            {
                httaxcount.Add("@Trans", "COUNT_OF_EXTERNAL_TAX_DOCUMENTS_BY_ORDER");
            }
            else if (Tax_Order_Task == 22 || Tax_Order_Task == 26)
            {
                httaxcount.Add("@Trans", "COUNT_OF_INTERNAL_TAX_DOCUMENTS_BY_ORDER");
            }
            httaxcount.Add("@Order_Id", OrderId);
            dttaxcount = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", httaxcount);
            if (dttaxcount.Rows.Count > 0)
            {
                tabControl1.TabPages[1].Text = "Tax  " + "(" + dttaxcount.Rows[0]["count"].ToString() + ")";
            }
            else
            {
                tabControl1.TabPages[1].Text = "Tax  " + "(0)";
            }

            Gridview_bind_Tax_Document_Upload();

            Hashtable htvendoccount = new Hashtable();
            System.Data.DataTable dtvendocount = new System.Data.DataTable();
            htvendoccount.Add("@Trans", "COUNT_NO_DOC");
            htvendoccount.Add("@Order_Id", OrderId);
            dtvendocount = dataaccess.ExecuteSP("Sp_Vendor_Order_Documents", htvendoccount);
            if (dtvendocount.Rows.Count > 0)
            {
                tabControl1.TabPages[3].Text = "Vendor  " + "(" + dtvendocount.Rows[0]["count"].ToString() + ")";
            }
            else
            {
                tabControl1.TabPages[3].Text = "Vendor  " + "(0)";
            }

            Grid_Bind_VendorDocuments();

            if (User_Role_Id == "1" || User_Role_Id == "4" || User_Role_Id == "6")
            {
                btn_View_package.Visible = true;
            }
            else
            {
                btn_View_package.Visible = false;
            }
        }

        private void CreateDirectory(string mainPath, string directoryPath)
        {
            try
            {
                string Ftp_Host_Name = Ftp_Domain_Name;
                Ftp_Path = Ftp_Host_Name + "/Ftp_Application_Files/OMS/" + mainPath + "";
                string[] folderArray = directoryPath.Split('/');
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
                        catch (WebException ex)
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
        private void CreateDirectoryTitlelogy(string mainPath, string directoryPath)
        {
            try
            {
                string Ftp_Host_Name = Ftp_Domain_Name;
                Ftp_Path = Ftp_Host_Name + "/TITLELOGY/" + mainPath + "";
                string[] folderArray = directoryPath.Split('/');
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

        private void Get_Order_Type_Abb()
        {
            Hashtable htop = new Hashtable();
            System.Data.DataTable dtop = new System.Data.DataTable();
            htop.Add("@Trans", "GET_ORDER_TYPE_ABRIVATION");
            htop.Add("@Order_ID", OrderId);
            dtop = dataaccess.ExecuteSP("Sp_Document_Upload", htop);

            if (dtop.Rows.Count > 0)
            {
                Sub_Process_Id = dtop.Rows[0]["Sub_ProcessId"].ToString();
                Order_Type_Abbrivation = dtop.Rows[0]["Order_Type_Abrivation"].ToString();
            }
        }

        private void RefreshView()
        {
            Hashtable htDocument_Select = new Hashtable();
            System.Data.DataTable dtDocument_Select = new System.Data.DataTable();
            if (User_Role_Id != "2")
            {
                htDocument_Select.Add("@Trans", "SELECT");
            }
            else
            {
                htDocument_Select.Add("@Trans", "SELECT_FOR_EMPLOYEE_ROLE");
            }
            htDocument_Select.Add("@Order_Id", OrderId);
            // htselSourceuploadkb.Add("@Tax_Type_Id", Tax_Type_Id);
            dtDocument_Select = dataaccess.ExecuteSP("Sp_Document_Upload", htDocument_Select);
            listView1.Items.Clear();
            for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
            {
                DataRow dr = dtDocument_Select.Rows[i];
                ListViewItem items = new ListViewItem(dr[4].ToString());
                //ListViewItem items1 = new ListViewItem(dr[7].ToString());
                ////items.SubItems.Add(dr[4].ToString());
                ////items.SubItems.Add(dr[7].ToString());
                //listView1.Items.Add(items);
                //listView1.Items.Add(items1);
                listView1.Items.Add(items);
            }
        }

        private delegate void ChangeHandler(object sender, FileSystemEventArgs e);

        private string[] GetSelection()
        {
            if (listView1.SelectedItems.Count == 0)
                return null;
            string[] files = new string[listView1.SelectedItems.Count];
            int i = 0;
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                files[i++] = item.Text;
            }
            return files;
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ChangeHandler(fsw_Changed), new object[] { sender, e });
                return;
            }

            string[] files = Directory.GetFiles(homeFolder);
            if (files.Length != listView1.Items.Count)			// You probably want to do something better
            {
                listView1.Items.Clear();
                foreach (string file in files)
                {
                    listView1.Items.Add(file);
                }
            }
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        //
        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = string.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = string.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = string.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";
            File_size = size;
            return size;
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (Operation == "Update")
            {
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                    Hashtable htorderkb = new Hashtable();
                    System.Data.DataTable dtorderkb = new System.Data.DataTable();
                    OpenFileDialog op1 = new OpenFileDialog();
                    op1.Multiselect = true;
                    op1.ShowDialog();
                    op1.Filter = "allfiles|*.xls";
                    int count = 0;
                    foreach (string s in op1.FileNames)
                    {
                        FName = s.Split('\\');
                        string file = op1.FileName.ToString();
                        string Docname = new FileInfo(file).Name;
                        FileInfo f = new FileInfo(file);
                        File_size = GetFileSize(f.Length);
                        homeFolder = year + "/" + month + "/" + Client_Name + "/" + OrderId + "";
                        mainPath = "Orders_Files";
                        CreateDirectory(mainPath, homeFolder);
                        ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/" + mainPath + "/" + homeFolder + "";
                        string ftpUploadFullPath = "" + ftpfullpath + "/" + f.Name + "";

                        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP Address  
                        ftpRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password); // Credentials  
                        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                        FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());
                        HashSet<string> files = new HashSet<string>(); // create list to store directories.   
                        string line = streamReader.ReadLine();
                        while (!string.IsNullOrEmpty(line))
                        {
                            files.Add(line); // Add Each Directory to the List.  
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
                            byte[] buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, buffer.Length);
                            fs.Close();
                            Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                            ftpstream.Write(buffer, 0, buffer.Length);
                            ftpstream.Close();
                            count++;
                            htorderkb.Clear();
                            dtorderkb.Clear();
                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", txt_path.Text);
                            htorderkb.Add("@Order_ID", OrderId);
                            htorderkb.Add("@File_Size", File_size);
                            htorderkb.Add("@Document_Name", op1.SafeFileName);
                            htorderkb.Add("@Document_Path", ftpUploadFullPath);
                            htorderkb.Add("@Inserted_By", userid);
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);

                        }
                        else
                        {
                            throw new WebException("File already exists");
                        }
                    }
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show(count + " File(s) copied");
                    Grd_Document_upload_Load();
                    RefreshView();
                }
                catch (WebException ex)
                {
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show("something went wrong");
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
        }

        protected void Grd_Document_upload_Load()
        {
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn chk_Inv = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn chk_Typ = new DataGridViewCheckBoxColumn();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            Hashtable htDocument_Select = new Hashtable();
            System.Data.DataTable dtDocument_Select = new System.Data.DataTable();
            if (User_Role_Id != "2")
            {
                htDocument_Select.Add("@Trans", "SELECT");
            }
            else
            {

                htDocument_Select.Add("@Trans", "SELECT_FOR_EMPLOYEE_ROLE");
            }

            htDocument_Select.Add("@Order_Id", OrderId);
            // htselSourceuploadkb.Add("@Tax_Type_Id", Tax_Type_Id);
            dtDocument_Select = dataaccess.ExecuteSP("Sp_Document_Upload", htDocument_Select);

            //Grd_Document_upload.Columns[0].Width = 100;
            //Grd_Document_upload.Columns[1].Width = 50;
            //Grd_Document_upload.Columns[2].Width = 200;
            //Grd_Document_upload.Columns[3].Width = 30;
            //Grd_Document_upload.Columns[4].Width = 80;
            //Grd_Document_upload.Columns[5].Width = 100;
            //Grd_Document_upload.Columns[6].Width = 50;
            //Grd_Document_upload.Columns[7].Width = 50;

            if (dtDocument_Select.Rows.Count > 0)
            {

                Grd_Document_upload.DataSource = null;
                Grd_Document_upload.Columns.Clear();
                Grd_Document_upload.Rows.Clear();
                //ex2.Visible = true;
                Grd_Document_upload.Visible = true;
                Grd_Document_upload.AutoGenerateColumns = false;
                Grd_Document_upload.ColumnCount = 10;

                Grd_Document_upload.Columns[0].Name = "Instuction";
                Grd_Document_upload.Columns[0].HeaderText = "INSTRUCTION";
                Grd_Document_upload.Columns[0].DataPropertyName = "Instuction";
                Grd_Document_upload.Columns[0].Width = 250;

                Grd_Document_upload.Columns[1].Name = "DocumentPath";
                Grd_Document_upload.Columns[1].HeaderText = "FILE PATH";
                Grd_Document_upload.Columns[1].DataPropertyName = "New_Document_Path";
                Grd_Document_upload.Columns[1].Visible = false;

                Grd_Document_upload.Columns[2].Name = "FileName";
                Grd_Document_upload.Columns[2].HeaderText = "FILE NAME";
                Grd_Document_upload.Columns[2].DataPropertyName = "Document_Name";
                Grd_Document_upload.Columns[2].Width = 300;

                Grd_Document_upload.Columns[3].Name = "FileSize";
                Grd_Document_upload.Columns[3].HeaderText = "FILE SIZE";
                Grd_Document_upload.Columns[3].DataPropertyName = "File_Size";
                Grd_Document_upload.Columns[3].Width = 100;

                Grd_Document_upload.Columns[4].Name = "Inserted_date";
                Grd_Document_upload.Columns[4].HeaderText = "Date";
                Grd_Document_upload.Columns[4].DataPropertyName = "Inserted_date";
                Grd_Document_upload.Columns[4].Width = 120;

                Grd_Document_upload.Columns[5].Name = "username";
                Grd_Document_upload.Columns[5].HeaderText = "USER NAME";
                Grd_Document_upload.Columns[5].DataPropertyName = "User_Name";
                Grd_Document_upload.Columns[5].Width = 200;

                Grd_Document_upload.Columns[6].Name = "upload_id";
                Grd_Document_upload.Columns[6].HeaderText = "upload_id";
                Grd_Document_upload.Columns[6].DataPropertyName = "Document_Upload_Id";
                Grd_Document_upload.Columns[6].Visible = false;

                Grd_Document_upload.Columns[7].Name = "User_id";
                Grd_Document_upload.Columns[7].HeaderText = "User_id";
                Grd_Document_upload.Columns[7].DataPropertyName = "User_id";
                Grd_Document_upload.Columns[7].Visible = false;


                Grd_Document_upload.Columns[8].Name = "Document_Type";
                Grd_Document_upload.Columns[8].HeaderText = "Document_Type";
                Grd_Document_upload.Columns[8].DataPropertyName = "Document_Type";
                Grd_Document_upload.Columns[8].Visible = false;


                Grd_Document_upload.Columns[9].Name = "Work_Type_Id";
                Grd_Document_upload.Columns[9].HeaderText = "Work_Type_Id";
                Grd_Document_upload.Columns[9].DataPropertyName = "Work_Type_Id";
                Grd_Document_upload.Columns[9].Visible = false;


                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4" || User_Role_Id == "5" || User_Role_Id == "3")
                {
                    Grd_Document_upload.Columns[5].Visible = true;
                }

                else
                {
                    Grd_Document_upload.Columns[5].Visible = false;
                }


                Grd_Document_upload.Columns.Add(chk);
                chk.HeaderText = "UPLOAD PACKAGE";
                chk.Name = "check";
                //Grd_Document_upload.Columns[7].Width = 90;
                //bool ischecked=(bool).dtDocument_Select

                Grd_Document_upload.Columns.Add(chk_Inv);
                chk_Inv.HeaderText = "INVOICE";
                chk_Inv.Name = "check_Inv";
                // Grd_Document_upload.Columns[8].Width = 90;

                Grd_Document_upload.Columns.Add(chk_Typ);
                chk_Typ.HeaderText = "TYPING";
                chk_Typ.Name = "check_Typeing";
                // Grd_Document_upload.Columns[9].Width = 90;





                Grd_Document_upload.Columns.Add(btn);

                btn.HeaderText = "Open";
                btn.Text = "Open";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;
                Grd_Document_upload.Columns[13].Width = 100;

                Grd_Document_upload.Columns.Add(btnEdit);
                btnEdit.HeaderText = "Edit";
                btnEdit.Text = "Edit";
                btnEdit.Name = "btnEdit";
                btnEdit.UseColumnTextForButtonValue = true;
                Grd_Document_upload.Columns[14].Width = 100;

                Grd_Document_upload.Columns.Add(btnDelete);
                btnDelete.HeaderText = "Delete";
                btnDelete.Text = "Delete";
                btnDelete.Name = "btnDelete";
                btnDelete.UseColumnTextForButtonValue = true;
                Grd_Document_upload.Columns[15].Width = 100;



                Grd_Document_upload.DataSource = dtDocument_Select;

            }
            else
            {
                Grd_Document_upload.DataSource = null;

            }
            for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
            {
                for (int j = 0; j < Grd_Document_upload.Rows.Count; j++)
                {
                    Grd_Document_upload.Rows[j].Cells[13].Value = "View";
                    Grd_Document_upload.Rows[j].Cells[14].Value = "Edit";
                    Grd_Document_upload.Rows[j].Cells[15].Value = "Delete";
                    if (dtDocument_Select.Rows[i]["Document_Upload_Id"].ToString() == Grd_Document_upload.Rows[j].Cells[6].Value.ToString())
                    {
                        Grd_Document_upload.Columns[6].Visible = false;
                        bool ischecked = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString());
                        if (ischecked == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[10].Value = dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString();
                        }

                        bool ischecked1 = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString());
                        if (ischecked1 == true)
                        {
                            //chk.DataPropertyName=;
                            Grd_Document_upload.Rows[j].Cells[11].Value = dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString();
                        }

                        bool ischecked2 = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_Typing_Package"].ToString());
                        if (ischecked2 == true)
                        {
                            //chk.DataPropertyName=;
                            Grd_Document_upload.Rows[j].Cells[12].Value = dtDocument_Select.Rows[i]["Chk_Typing_Package"].ToString();
                        }
                    }
                }
            }


        }


        protected void Grd_TempDocument_upload_Load()
        {
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn chk_Inv = new DataGridViewCheckBoxColumn();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            DataGridViewCheckBoxColumn chk_Typ = new DataGridViewCheckBoxColumn();
            Hashtable htDocument_Select = new Hashtable();
            System.Data.DataTable dtDocument_Select = new System.Data.DataTable();
            htDocument_Select.Add("@Trans", "SELECT");
            htDocument_Select.Add("@Order_Number", Order_No);
            // htselSourceuploadkb.Add("@Tax_Type_Id", Tax_Type_Id);
            dtDocument_Select = dataaccess.ExecuteSP("Sp__Temp_Document_Upload", htDocument_Select);
            if (dtDocument_Select.Rows.Count > 0)
            {

                Grd_Document_upload.DataSource = null;
                Grd_Document_upload.Columns.Clear();
                Grd_Document_upload.Rows.Clear();
                //ex2.Visible = true;
                Grd_Document_upload.Visible = true;
                Grd_Document_upload.AutoGenerateColumns = false;
                Grd_Document_upload.ColumnCount = 8;

                Grd_Document_upload.Columns[0].Name = "Instuction";
                Grd_Document_upload.Columns[0].HeaderText = "INSTRUCTION";
                Grd_Document_upload.Columns[0].DataPropertyName = "Instuction";
                Grd_Document_upload.Columns[0].Width = 250;

                Grd_Document_upload.Columns[1].Name = "DocumentPath";
                Grd_Document_upload.Columns[1].HeaderText = "FILE PATH";
                Grd_Document_upload.Columns[1].DataPropertyName = "Document_Path";
                Grd_Document_upload.Columns[1].Visible = false;

                Grd_Document_upload.Columns[2].Name = "FileName";
                Grd_Document_upload.Columns[2].HeaderText = "FILE NAME";
                Grd_Document_upload.Columns[2].DataPropertyName = "Document_Name";
                Grd_Document_upload.Columns[2].Width = 300;

                Grd_Document_upload.Columns[3].Name = "FileSize";
                Grd_Document_upload.Columns[3].HeaderText = "FILE SIZE";
                Grd_Document_upload.Columns[3].DataPropertyName = "File_Size";
                Grd_Document_upload.Columns[3].Width = 100;

                Grd_Document_upload.Columns[4].Name = "Inserted_date";
                Grd_Document_upload.Columns[4].HeaderText = "Date";
                Grd_Document_upload.Columns[4].DataPropertyName = "Inserted_date";
                Grd_Document_upload.Columns[4].Width = 120;

                Grd_Document_upload.Columns[5].Name = "username";
                Grd_Document_upload.Columns[5].HeaderText = "USER NAME";
                Grd_Document_upload.Columns[5].DataPropertyName = "User_Name";
                Grd_Document_upload.Columns[5].Width = 200;

                Grd_Document_upload.Columns[6].Name = "upload_id";
                Grd_Document_upload.Columns[6].HeaderText = "upload_id";
                Grd_Document_upload.Columns[6].DataPropertyName = "Document_Upload_Id";
                Grd_Document_upload.Columns[6].Visible = false;

                Grd_Document_upload.Columns[7].Name = "User_id";
                Grd_Document_upload.Columns[7].HeaderText = "User_id";
                Grd_Document_upload.Columns[7].DataPropertyName = "User_id";
                Grd_Document_upload.Columns[7].Visible = false;

                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4" || User_Role_Id == "5" || User_Role_Id == "3")
                {
                    Grd_Document_upload.Columns[5].Visible = true;
                }

                else
                {
                    Grd_Document_upload.Columns[5].Visible = false;
                }


                Grd_Document_upload.Columns.Add(chk);
                chk.HeaderText = "UPLOAD PACKAGE";
                chk.Name = "check";
                //Grd_Document_upload.Columns[7].Width = 90;
                //bool ischecked=(bool).dtDocument_Select

                Grd_Document_upload.Columns.Add(chk_Inv);
                chk_Inv.HeaderText = "INVOICE";
                chk_Inv.Name = "check_Inv";
                // Grd_Document_upload.Columns[8].Width = 90;

                Grd_Document_upload.Columns.Add(chk_Typ);
                chk_Typ.HeaderText = "TYPING";
                chk_Typ.Name = "check_Typeing";
                // Grd_Document_upload.Columns[9].Width = 90;

                Grd_Document_upload.Columns.Add(btn);

                btn.HeaderText = "Open";
                btn.Text = "Open";
                btn.Name = "btn";
                Grd_Document_upload.Columns[10].Width = 100;

                Grd_Document_upload.Columns.Add(btnEdit);
                btnEdit.HeaderText = "Edit";
                btnEdit.Text = "Edit";
                btnEdit.Name = "btnEdit";
                Grd_Document_upload.Columns[11].Width = 100;

                Grd_Document_upload.Columns.Add(btnDelete);
                btnDelete.HeaderText = "Delete";
                btnDelete.Text = "Delete";
                btnDelete.Name = "btnDelete";
                Grd_Document_upload.Columns[12].Width = 100;

                Grd_Document_upload.DataSource = dtDocument_Select;

            }
            else
            {
                Grd_Document_upload.DataSource = null;

            }
            for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
            {
                for (int j = 0; j < Grd_Document_upload.Rows.Count; j++)
                {
                    Grd_Document_upload.Rows[j].Cells[11].Value = "View";
                    Grd_Document_upload.Rows[j].Cells[12].Value = "Edit";
                    Grd_Document_upload.Rows[j].Cells[13].Value = "Delete";
                    if (dtDocument_Select.Rows[i]["Document_Upload_Id"].ToString() == Grd_Document_upload.Rows[j].Cells[6].Value.ToString())
                    {
                        Grd_Document_upload.Columns[6].Visible = false;
                        bool ischecked = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString());
                        if (ischecked == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[8].Value = dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString();
                        }

                        bool ischecked1 = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString());
                        if (ischecked1 == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[9].Value = dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString();
                        }

                        bool ischecked2 = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_Typing_Package"].ToString());
                        if (ischecked2 == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[10].Value = dtDocument_Select.Rows[i]["Chk_Typing_Package"].ToString();
                        }
                    }
                }
            }
        }

        private void Grid_Bind_VendorDocuments()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            Hashtable ht_Vendor = new Hashtable();
            System.Data.DataTable dt_Vendor = new System.Data.DataTable();
            ht_Vendor.Add("@Trans", "SELECT");
            ht_Vendor.Add("@Order_Id", OrderId);

            dt_Vendor = dataaccess.ExecuteSP("Sp_Vendor_Order_Documents", ht_Vendor);
            if (dt_Vendor.Rows.Count > 0)
            {
                grd_Vendor_Documents.ColumnHeadersVisible = true;
                grd_Vendor_Documents.Rows.Clear();
                Document_Name.Visible = true;
                Document_Size.Visible = true;
                Uploaded_By.Visible = true;
                Date.Visible = true;
                View.Visible = true;
                for (int i = 0; i < dt_Vendor.Rows.Count; i++)
                {
                    grd_Vendor_Documents.Rows.Add();
                    grd_Vendor_Documents.Rows[i].Cells[1].Value = dt_Vendor.Rows[i]["Document_Type"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[2].Value = dt_Vendor.Rows[i]["Description"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[3].Value = dt_Vendor.Rows[i]["Document_Name"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[4].Value = dt_Vendor.Rows[i]["File_Size"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[5].Value = dt_Vendor.Rows[i]["User_Name"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[6].Value = dt_Vendor.Rows[i]["Inserted_Date"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[7].Value = "View";
                    grd_Vendor_Documents.Rows[i].Cells[8].Value = dt_Vendor.Rows[i]["New_Document_Path"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[9].Value = dt_Vendor.Rows[i]["Order_Document_Id"].ToString();

                    string Iscopied = dt_Vendor.Rows[i]["IsCopied_To_Inhouse"].ToString();
                    if (Iscopied == "True")
                    {
                        grd_Vendor_Documents.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
            }
            else
            {
                grd_Vendor_Documents.Rows.Clear();
            }

        }

        private void btn_open_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Grd_Document_upload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 10)
                {
                    Hashtable htorderkbchk = new Hashtable();
                    System.Data.DataTable dtorderkbchk = new System.Data.DataTable();
                    htorderkbchk.Add("@Trans", "CHKUPDATE");
                    htorderkbchk.Add("@Order_ID", OrderId);
                    dtorderkbchk = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkbchk);

                    Hashtable htorderkbup = new Hashtable();
                    System.Data.DataTable dtorderkbup = new System.Data.DataTable();
                    htorderkbup.Add("@Trans", "UPDATE_CHK");
                    htorderkbup.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value);
                    dtorderkbup = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkbup);
                    Grd_Document_upload_Load();
                }
                if (e.ColumnIndex == 11)
                {
                    string myFilePath = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string ext = Path.GetExtension(myFilePath);
                    bool ischeck = (bool)Grd_Document_upload[11, e.RowIndex].FormattedValue;
                    Hashtable htupdate = new Hashtable();
                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                    htupdate.Add("@Trans", "UPDATE_INVOICE_FALSE");
                    htupdate.Add("Order_ID", OrderId);
                    dtupdate = dataaccess.ExecuteSP("Sp_Document_Upload", htupdate);
                    if (ext == ".pdf" || ext == ".PDF")
                    {
                        Hashtable htorderkbup = new Hashtable();
                        System.Data.DataTable dtorderkbup = new System.Data.DataTable();
                        htorderkbup.Add("@Trans", "UPDATE_INVOICE_PACKAGE");

                        string Value = Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value.ToString();
                        Hashtable htin = new Hashtable();
                        System.Data.DataTable dtin = new System.Data.DataTable();
                        htin.Add("@Trans", "GET_INVOICE_UPLOAD_CHECK_BYID");
                        htin.Add("@Document_Upload_Id", Value);
                        dtin = dataaccess.ExecuteSP("Sp_Document_Upload", htin);

                        string Status = dtin.Rows[0]["Chk_Invoice_Pakage"].ToString();

                        if (Status == "True")
                        {


                            htorderkbup.Add("@Chk_Invoice_Pakage", "False");

                        }
                        else if (Status == "False")
                        {
                            htorderkbup.Add("@Chk_Invoice_Pakage", "True");

                        }
                        htorderkbup.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value);
                        dtorderkbup = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkbup);
                        Grd_Document_upload_Load();
                    }
                    else
                    {
                        ischeck = (bool)Grd_Document_upload[11, e.RowIndex].FormattedValue;
                        if (ischeck == false)
                        {
                            //Grd_Document_upload.Rows[e.RowIndex].Cells[8].Value = false;
                            //Grd_Document_upload[8, e.RowIndex].Value = false;
                            //ischeck = false;
                            foreach (DataGridViewRow row in Grd_Document_upload.Rows)
                            {
                                (row.Cells[11] as DataGridViewCheckBoxCell).Value = false;
                            }
                            //(Grd_Document_upload.Rows[e.RowIndex].Cells[8] as DataGridViewCheckBoxCell).Value = false;
                        }
                        MessageBox.Show("Please Select only PDF Document");
                        //foreach (DataGridViewRow row in Grd_Document_upload.Rows)
                        //{
                        //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[8];
                        //    if (chk.Value == chk.FalseValue)
                        //    {
                        //        chk.Value = chk.FalseValue;
                        //    }

                        //}


                    }
                }
                if (e.ColumnIndex == 12)
                {
                    string myFilePath = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string ext = Path.GetExtension(myFilePath);
                    Hashtable htupdate = new Hashtable();
                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                    htupdate.Add("@Trans", "UPDATE_TYPING_FALSE");
                    htupdate.Add("Order_ID", OrderId);
                    dtupdate = dataaccess.ExecuteSP("Sp_Document_Upload", htupdate);
                    if (ext == ".doc" || ext == ".docx")
                    {

                        Hashtable htorderkbup = new Hashtable();
                        System.Data.DataTable dtorderkbup = new System.Data.DataTable();
                        htorderkbup.Add("@Trans", "UPDATE_TYPING_PACKAGE");

                        string Value = Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value.ToString();
                        Hashtable htin = new Hashtable();
                        System.Data.DataTable dtin = new System.Data.DataTable();
                        htin.Add("@Trans", "GET_TYPING_PACKAGE_BY_ID");
                        htin.Add("@Document_Upload_Id", Value);
                        dtin = dataaccess.ExecuteSP("Sp_Document_Upload", htin);

                        string Status = dtin.Rows[0]["Chk_Typing_Package"].ToString();

                        if (Status == "True")
                        {
                            htorderkbup.Add("@Chk_Typing_Package", "False");
                        }
                        else if (Status == "False")
                        {
                            htorderkbup.Add("@Chk_Typing_Package", "True");
                        }
                        htorderkbup.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value);
                        dtorderkbup = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkbup);
                        Grd_Document_upload_Load();
                    }
                    else
                    {
                        MessageBox.Show("Please Select only Word Document");
                    }
                }
                if (e.ColumnIndex == 13)
                {
                    try
                    {
                        string Document_Type = string.Empty;
                        string Work_Type = string.Empty;
                        string myFilePath = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string ext = Path.GetExtension(myFilePath);
                        string Uploaded_User_Id = Grd_Document_upload.Rows[e.RowIndex].Cells[7].Value.ToString();
                        if (!string.IsNullOrEmpty(Grd_Document_upload.Rows[e.RowIndex].Cells[8].Value.ToString()))
                        {
                            Document_Type = Grd_Document_upload.Rows[e.RowIndex].Cells[8].Value.ToString();
                        }
                        if (!string.IsNullOrEmpty(Grd_Document_upload.Rows[e.RowIndex].Cells[9].Value.ToString()))
                        {
                            Work_Type = Grd_Document_upload.Rows[e.RowIndex].Cells[9].Value.ToString();
                        }
                        FName = Grd_Document_upload.Rows[e.RowIndex].Cells[2].Value.ToString().Split('\\');
                        string Source_Path = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();

                        // this is for only Note pad 
                        if (Document_Type == "11")
                        {
                            Employee.Search_NotePad sn = new Employee.Search_NotePad(OrderId, int.Parse(Work_Type.ToString()), userid, int.Parse(Uploaded_User_Id.ToString()), 0, "View_By_User_Wise", Order_No.ToString());
                            sn.Show();
                        }
                        else
                        {
                            string fileName = Path.GetFileName(Source_Path).Replace("%20", " ");
                            Download_Ftp_File(fileName, Source_Path);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Problem in File Opening");
                    }

                    //System.Diagnostics.Process.Start(Source_Path);

                }

                if (e.ColumnIndex == 14)
                {
                    Hashtable htEdit = new Hashtable();
                    System.Data.DataTable dtEdit = new System.Data.DataTable();
                    htEdit.Add("@Trans", "UPDATE");
                    htEdit.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value);
                    htEdit.Add("@Instuction", Grd_Document_upload.Rows[e.RowIndex].Cells[0].Value);
                    dtEdit = dataaccess.ExecuteSP("Sp_Document_Upload", htEdit);
                    Grd_Document_upload_Load();
                }
                if (e.ColumnIndex == 15)
                {
                    if (MessageBox.Show("Do you Want to Proceed?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            Hashtable htUser = new Hashtable();
                            System.Data.DataTable dtUser = new System.Data.DataTable();
                            htUser.Add("@Trans", "SELPASS");
                            htUser.Add("@User_id", userid);
                            dtUser = dataaccess.ExecuteSP("Sp_User", htUser);
                            if (dtUser.Rows[0]["User_Name"].ToString().ToUpper() == Grd_Document_upload.Rows[e.RowIndex].Cells[5].Value.ToString().ToUpper())
                            {
                                string filePath = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
                                FtpWebRequest ftRequestDelete = (FtpWebRequest)WebRequest.Create(new Uri(filePath));
                                ftRequestDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                                ftRequestDelete.UseBinary = true;
                                ftRequestDelete.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                                FtpWebResponse response = (FtpWebResponse)ftRequestDelete.GetResponse();
                                if (response.StatusCode == FtpStatusCode.FileActionOK)
                                {
                                    Hashtable htdelete = new Hashtable();
                                    System.Data.DataTable dtdelete = new System.Data.DataTable();
                                    htdelete.Add("@Trans", "DELETE");
                                    htdelete.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value);
                                    dtdelete = dataaccess.ExecuteSP("Sp_Document_Upload", htdelete);
                                    Grd_Document_upload_Load();
                                    RefreshView();
                                }
                                else
                                {
                                    MessageBox.Show("Unable to delete file");
                                }
                            }
                            else
                            {
                                MessageBox.Show("You have no permission for delete this document");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void Download_Ftp_File(string p, string Source_Path)
        {
            try
            {
                //string File_Name = p;
                //File_Name = File_Name.Replace("%20"," ");
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
                ftpStream.CopyTo(outputStream);
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                System.Diagnostics.Process.Start(localPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Problem in Downloading Files please Check with Administrator");
            }
        }


        private void Grd_Document_upload_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            //{
            //    e.Effect = DragDropEffects.Move;
            //}



            //DataGridViewSelectedRowCollection rows = (DataGridViewSelectedRowCollection)e.Data.GetData(typeof(DataGridViewSelectedRowCollection));
            // foreach (DataGridViewRow row in rows)
            //{
            //    Hashtable ht_docpath = new Hashtable();
            //    DataTable dt_docpath = new DataTable();
            //    ht_docpath.Add("@Trans", "Doc_Path");
            //    ht_docpath.Add("@Document_Upload_Id", row.Cells[5].Value.ToString());
            //    dt_docpath = dataaccess.ExecuteSP("Sp_Document_Upload", ht_docpath);
            //     for(int i=0;i>dt_docpath.Rows.Count;i++)
            //     {
            //         File.Copy(dt_docpath.Rows[i]["Document_Path"].ToString(), Environment.GetFolderPath(Environment.SpecialFolder.System), true);
            //     }
            // }

            // Call OpenFiles function passing array of strings to it

        }

        void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            Console.Write("Column Resizing");
            e.NewWidth = this.listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] files = GetSelection();
            folderBrowserDialog1.ShowDialog();
            string tpath = folderBrowserDialog1.SelectedPath.ToString();
            for (int i = 0; i < files.Length; i++)
            {
                string[] filename = files[i].ToString().Split('/');
                string Filename;
                Filename = filename[filename.Length - 1];
                Filename = Filename.Replace("%20", " ");
                Download_Ftp_File1(Filename.ToString(), files[i].ToString(), tpath.ToString());
            }
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            // Determine whether file data exists in the drop data. If not, then
            // the drop effect reflects that the drop cannot occur.
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            // Set the effect based upon the KeyState.
            // Can't get links to work - Use of Ole1 services requiring DDE windows is disabled
            //			if ((e.KeyState & (CTRL | ALT)) == (CTRL | ALT) &&
            //				(e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) 
            //			{
            //				e.Effect = DragDropEffects.Link;
            //			}
            //			
            //			else if ((e.KeyState & ALT) == ALT && 
            //				(e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link) 
            //			{
            //				e.Effect = DragDropEffects.Link;
            //
            //			} 
            //			else
            if ((e.KeyState & SHIFT) == SHIFT &&
                (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;

            }
            else if ((e.KeyState & CTRL) == CTRL &&
                (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                // By default, the drop action should be move, if allowed.
                e.Effect = DragDropEffects.Move;

                // Implement the rather strange behaviour of explorer that if the disk
                // is different, then default to a COPY operation
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && !files[0].ToUpper().StartsWith(homeDisk) &&			// Probably better ways to do this
                (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)

                    e.Effect = DragDropEffects.Copy;

                Upload_Ftp_Files(files);
            }
            else
                e.Effect = DragDropEffects.None;

            // This is an example of how to get the item under the mouse
            System.Drawing.Point pt = listView1.PointToClient(new System.Drawing.Point(e.X, e.Y));
            ListViewItem itemUnder = listView1.GetItemAt(pt.X, pt.Y);
        }


        private void Upload_Ftp_Files(string[] Upload_Files)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Hashtable htorderkb = new Hashtable();
                System.Data.DataTable dtorderkb = new System.Data.DataTable();
                homeFolder = year + "/" + month + "/" + Client_Name + "/" + OrderId + "";
                mainPath = "Orders_Files";
                CreateDirectory(mainPath, homeFolder);
                ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/" + mainPath + "/" + homeFolder + "";
                int count = 0;
                foreach (string file in Upload_Files)
                {
                    htorderkb.Clear();
                    dtorderkb.Clear();
                    bool isFolder = Directory.Exists(file);
                    bool isFile = File.Exists(file);
                    FileInfo f = new FileInfo(file);
                    double filesize = f.Length;
                    File_size = GetFileSize(filesize);
                    if (!isFolder && !isFile)
                        continue;
                    string ftpUploadFullPath = "" + ftpfullpath + "/" + f.Name + "";
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP Address  
                    ftpRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password); // Credentials  
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    HashSet<string> files = new HashSet<string>();
                    string line = streamReader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        files.Add(line);
                        line = streamReader.ReadLine();
                    }
                    if (!files.Contains(f.Name))
                    {
                        FtpWebRequest ftpUpLoadFile = (FtpWebRequest)FtpWebRequest.Create(ftpUploadFullPath);
                        ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                        ftpUpLoadFile.KeepAlive = true;
                        ftpUpLoadFile.UseBinary = true;
                        ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;
                        FileStream fs = File.OpenRead(file);
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Close();
                        Stream ftpstream = ftpUpLoadFile.GetRequestStream();
                        ftpstream.Write(buffer, 0, buffer.Length);
                        ftpstream.Close();
                        count++;
                        htorderkb.Clear();
                        dtorderkb.Clear();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Instuction", txt_path.Text);
                        htorderkb.Add("@Order_ID", OrderId);
                        htorderkb.Add("@File_Size", File_size);
                        htorderkb.Add("@Document_Name", f.Name);
                        htorderkb.Add("@Document_Path", ftpUploadFullPath);
                        htorderkb.Add("@Inserted_By", userid);
                        htorderkb.Add("@Inserted_date", DateTime.Now);
                        dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                    }
                    else
                    {
                        throw new WebException("File already exists");
                    }
                }
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(count + " File(s) copied");
                Grd_Document_upload_Load();
                RefreshView();
            }
            catch (WebException ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }


        private void btn_Client_Upload_Click(object sender, EventArgs e)
        {
            if (External_Clinet_Id != 0)
            {
                Hashtable htorderkb = new Hashtable();
                System.Data.DataTable dtorderkb = new System.Data.DataTable();
                OpenFileDialog op1 = new OpenFileDialog();
                op1.Multiselect = true;
                op1.ShowDialog();
                op1.Filter = "allfiles|*.xls";
                // txt_path.Text = op1.FileName;

                int count = 0;
                int Chk = 0;

                foreach (string s in op1.FileNames)
                {
                    string file = op1.FileName.ToString();
                    System.IO.FileInfo f = new System.IO.FileInfo(file);
                    double filesize = f.Length;
                    File_size = GetFileSize(filesize);
                    FName = s.Split('\\');
                    string Docname = f.Name;


                    homeFolder = year + "/" + month + "/" + Client_Name + " / " + OrderId + "";
                    mainPath = "Order_Files";
                    ftpfullpath = "ftp://" + Ftp_Domain_Name + "/TITLELOGY/" + mainPath + "/" + homeFolder + "";
                    CreateDirectoryTitlelogy(mainPath, homeFolder);

                    try
                    {
                        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                        ftp.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                        ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                        FtpWebResponse CreateForderResponse = (FtpWebResponse)ftp.GetResponse();

                        if (CreateForderResponse.StatusCode == FtpStatusCode.PathnameCreated)
                        {

                            //If folder created, upload file

                            string ftpUploadFullPath = "" + ftpfullpath + "/" + f.Name + "";



                            // Checking File Exit or not

                            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP Address  

                            ftpRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password); // Credentials  

                            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;

                            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                            StreamReader streamReader = new StreamReader(response.GetResponseStream());

                            List<string> directories = new List<string>(); // create list to store directories.   

                            string line = streamReader.ReadLine();

                            while (!string.IsNullOrEmpty(line))
                            {
                                directories.Add(line); // Add Each Directory to the List.  
                                line = streamReader.ReadLine();
                            }


                            int File_Check = 0;

                            for (int i = 0; i <= directories.Count - 1; i++)
                            {

                                string FileName = directories[i].ToString();

                                if (FileName == f.Name)
                                {
                                    File_Check = 1;

                                    break;

                                }
                                else
                                {

                                    File_Check = 0;
                                }

                            }

                            if (File_Check == 0)
                            {

                                FtpWebRequest ftpUpLoadFile = (FtpWebRequest)FtpWebRequest.Create(ftpUploadFullPath);
                                ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);

                                ftpUpLoadFile.KeepAlive = true;

                                ftpUpLoadFile.UseBinary = true;

                                ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;

                                FileStream fs = File.OpenRead(file);

                                byte[] buffer = new byte[fs.Length];

                                fs.Read(buffer, 0, buffer.Length);

                                fs.Close();

                                Stream ftpstream = ftpUpLoadFile.GetRequestStream();

                                ftpstream.Write(buffer, 0, buffer.Length);

                                ftpstream.Close();





                                //string dest_path1 = @"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + Order_No + @"\" + FName[FName.Length - 1];
                                //DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                                //de.Username = "administrator";
                                //de.Password = "password1$";

                                //Directory.CreateDirectory(@"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + Order_No);


                                extension = Path.GetExtension(Docname);
                                // File.Copy(s, dest_path1, true);
                                count++;
                                htorderkb.Clear();
                                dtorderkb.Clear();
                                htorderkb.Add("@Trans", "INSERT");
                                htorderkb.Add("@Document_Type_Id", int.Parse(ddl_Dcoument_Type.SelectedValue.ToString()));
                                htorderkb.Add("@Order_Id", External_Order_Id);
                                htorderkb.Add("@Document_From", 2);
                                htorderkb.Add("@Document_File_Type", extension.ToString());
                                htorderkb.Add("@Description", txt_Dscription.Text.ToString());
                                htorderkb.Add("@Document_Path", ftpUploadFullPath);
                                htorderkb.Add("@File_Size", File_size);

                                htorderkb.Add("@Inserted_date", DateTime.Now);
                                htorderkb.Add("@status", "True");
                                dtorderkb = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htorderkb);
                                Gridview_bind_External_Client_Document_Upload();
                            }
                            else
                            {

                                MessageBox.Show("File already exist");
                            }
                        }

                    }
                    catch (Exception)
                    {
                        string ftpUploadFullPath = "" + ftpfullpath + "/" + f.Name + "";



                        // Checking File Exit or not

                        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP Address  

                        ftpRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password); // Credentials  

                        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;

                        FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());

                        List<string> directories = new List<string>(); // create list to store directories.   

                        string line = streamReader.ReadLine();

                        while (!string.IsNullOrEmpty(line))
                        {
                            directories.Add(line); // Add Each Directory to the List.  
                            line = streamReader.ReadLine();
                        }


                        int File_Check = 0;

                        for (int i = 0; i <= directories.Count - 1; i++)
                        {

                            string FileName = directories[i].ToString();

                            if (FileName == f.Name)
                            {
                                File_Check = 1;

                                break;

                            }
                            else
                            {

                                File_Check = 0;
                            }

                        }

                        if (File_Check == 0)
                        {

                            FtpWebRequest ftpUpLoadFile = (FtpWebRequest)FtpWebRequest.Create(ftpUploadFullPath);
                            ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);

                            ftpUpLoadFile.KeepAlive = true;

                            ftpUpLoadFile.UseBinary = true;

                            ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;

                            FileStream fs = File.OpenRead(file);

                            byte[] buffer = new byte[fs.Length];

                            fs.Read(buffer, 0, buffer.Length);

                            fs.Close();

                            Stream ftpstream = ftpUpLoadFile.GetRequestStream();

                            ftpstream.Write(buffer, 0, buffer.Length);

                            ftpstream.Close();





                            //string dest_path1 = @"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + Order_No + @"\" + FName[FName.Length - 1];
                            //DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                            //de.Username = "administrator";
                            //de.Password = "password1$";

                            //Directory.CreateDirectory(@"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + Order_No);


                            extension = Path.GetExtension(Docname);
                            //  File.Copy(s, dest_path1, true);
                            count++;
                            htorderkb.Clear();
                            dtorderkb.Clear();
                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Document_Type_Id", int.Parse(ddl_Dcoument_Type.SelectedValue.ToString()));
                            htorderkb.Add("@Order_Id", External_Order_Id);
                            htorderkb.Add("@Document_From", 2);
                            htorderkb.Add("@Document_File_Type", extension.ToString());
                            htorderkb.Add("@Description", txt_Dscription.Text.ToString());
                            htorderkb.Add("@Document_Path", ftpUploadFullPath);
                            htorderkb.Add("@File_Size", File_size);

                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            htorderkb.Add("@status", "True");
                            dtorderkb = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htorderkb);
                            Gridview_bind_External_Client_Document_Upload();
                        }
                        else
                        {

                            MessageBox.Show("File already exist");
                        }
                    }

                }
                MessageBox.Show(Convert.ToString(count) + " File(s) copied");
            }
            else
            {
                MessageBox.Show("Order is Not updated in Titlelogy website");

            }
        }

        private void Gridview_bind_External_Client_Document_Upload()
        {
            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            if (User_Role_Id == "2")
            {
                htselect.Add("@Trans", "GET_DOCUMENTS_FOR_INHOSE");
            }
            else
            {

                htselect.Add("@Trans", "GET_DOCUMENTS_FOR_INHOSE_ADMIN");
            }
            htselect.Add("@Order_Id", OrderId);
            dtselect = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Grid_Client_Upload.Columns[0].Width = 100;
                Grid_Client_Upload.Columns[1].Width = 100;
                Grid_Client_Upload.Columns[2].Width = 200;
                Grid_Client_Upload.Columns[3].Width = 80;
                Grid_Client_Upload.Columns[4].Width = 80;
                Grid_Client_Upload.Columns[5].Width = 60;
                Grid_Client_Upload.Columns[6].Width = 60;
                Grid_Client_Upload.Columns[7].Width = 60;
                Grid_Client_Upload.Columns[8].Width = 60;

                Grid_Client_Upload.Columns[9].Width = 60;


                if (dtselect.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Client_Upload.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        Grid_Client_Upload.Rows.Add();
                        bool Value = Convert.ToBoolean(dtselect.Rows[i]["Check_File"].ToString());
                        Grid_Client_Upload.Rows[i].Cells[0].Value = Value;
                        Grid_Client_Upload.Rows[i].Cells[1].Value = dtselect.Rows[i]["Document_Type"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[2].Value = dtselect.Rows[i]["Description"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[3].Value = dtselect.Rows[i]["File_Size"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[4].Value = dtselect.Rows[i]["Document_From"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[5].Value = dtselect.Rows[i]["Inserted_Date"].ToString();

                        Grid_Client_Upload.Rows[i].Cells[6].Value = "View";
                        Grid_Client_Upload.Rows[i].Cells[7].Value = "Edit";
                        Grid_Client_Upload.Rows[i].Cells[8].Value = "Delete";
                        Grid_Client_Upload.Rows[i].Cells[9].Value = dtselect.Rows[i]["Order_Document_Id"].ToString();

                        Grid_Client_Upload.Rows[i].Cells[10].Value = dtselect.Rows[i]["New_Document_Path"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[12].Value = dtselect.Rows[i]["Order_Id"].ToString();

                    }



                }
                else
                {

                    Grid_Client_Upload.Rows.Clear();

                    Grid_Client_Upload.DataSource = null;

                }
            }
            else
            {
                Grid_Client_Upload.Rows.Clear();

                Grid_Client_Upload.DataSource = null;

            }
        }

        private void Grid_Client_Upload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                string Source_Path = Grid_Client_Upload.Rows[e.RowIndex].Cells[10].Value.ToString();
                if (Source_Path != "")
                {
                    string fileName = Path.GetFileName(Source_Path).Replace("%20", " ");
                    Download_Ftp_File(fileName, Source_Path);
                }
            }
            else if (e.ColumnIndex == 8)
            {
                dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string DeletingType = Grid_Client_Upload.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string Document_ID = Grid_Client_Upload.Rows[e.RowIndex].Cells[9].Value.ToString();
                    if (DeletingType == "Client")
                    {

                        MessageBox.Show("Client Document cannot be Delete");

                    }
                    else
                    {

                        Hashtable htdel = new Hashtable();
                        System.Data.DataTable dtdel = new System.Data.DataTable();
                        htdel.Add("@Trans", "DELETE");
                        htdel.Add("@Order_Document_Id", Document_ID.ToString());
                        dtdel = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htdel);
                        Gridview_bind_External_Client_Document_Upload();
                    }
                }

                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }


            }
            else if (e.ColumnIndex == 0)
            {
                string myFilePath = Grid_Client_Upload.Rows[e.RowIndex].Cells[10].Value.ToString();
                string ext = Path.GetExtension(myFilePath);
                bool ischeck = (bool)Grid_Client_Upload[0, e.RowIndex].FormattedValue;
                string Document_ID = Grid_Client_Upload.Rows[e.RowIndex].Cells[9].Value.ToString();
                Hashtable htupdate = new Hashtable();
                System.Data.DataTable dtupdate = new System.Data.DataTable();

                //htupdate.Add("@Trans", "UPDATE_FILE_BY_ORDER");
                //htupdate.Add("@Order_Id", External_Order_Id);
                //dtupdate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htupdate);

                if (ext == ".pdf" || ext == ".PDF" || ext == ".doc" || ext == ".docx")
                {
                    htupdate.Clear();
                    dtupdate.Clear();

                    if (ischeck == false)
                    {


                        htupdate.Add("@Trans", "UPDATE_FILE");
                        htupdate.Add("@Order_Document_Id", Document_ID);
                        htupdate.Add("@Check_File", "True");
                        dtupdate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htupdate);
                        //  MessageBox.Show("File Status Checkd");

                    }
                    else
                    {


                        htupdate.Add("@Trans", "UPDATE_FILE");
                        htupdate.Add("@Order_Document_Id", Document_ID);
                        htupdate.Add("@Check_File", "False");
                        dtupdate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htupdate);
                        //   MessageBox.Show("File Status Un Checkd");

                    }




                    //foreach (DataGridViewRow dr in Grid_Client_Upload.Rows)
                    //{
                    //    dr.Cells[0].Value = false;//sıfırın
                    //}
                    //Gridview_bind_External_Client_Document_Upload();


                }
                else
                {

                    MessageBox.Show("Select Only Pdf Files");
                    // Gridview_bind_External_Client_Document_Upload();
                }




            }


        }

        private void chkItems_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Grid_Client_Upload.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (chk.Value == chk.TrueValue)
                {
                    chk.Value = chk.FalseValue;
                }
                else
                {
                    chk.Value = chk.TrueValue;
                }
            }
            Grid_Client_Upload.EndEdit();
        }

        private void btn_Inhouse_Upload_Click(object sender, EventArgs e)
        {
            Hashtable htorderkb = new Hashtable();
            System.Data.DataTable dtorderkb = new System.Data.DataTable();
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = true;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.xls";
            // txt_path.Text = op1.FileName;
            int count = 0;
            int Chk = 0;

            foreach (string s in op1.FileNames)
            {
                FName = s.Split('\\');
                //string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId + @"\" + FName[FName.Length - 1];
                //DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                //de.Username = "administrator";
                //de.Password = "password1$";
                string file = op1.FileName.ToString();
                System.IO.FileInfo f = new System.IO.FileInfo(file);
                double filesize = f.Length;
                File_size = GetFileSize(filesize);
                //Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId);

                //File.Copy(s, dest_path1, true);
                homeFolder = year + "/" + month + "/" + Client_Name + "/" + OrderId + "";
                mainPath = "Vendor_Upload_Documents";
                ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/" + mainPath + "/" + homeFolder + "";
                CreateDirectory(mainPath, homeFolder);
                try
                {
                    // File.Copy(s, dest_path1, false);

                    FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(ftpfullpath);
                    ftp.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                    ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                    FtpWebResponse CreateForderResponse = (FtpWebResponse)ftp.GetResponse();

                    if (CreateForderResponse.StatusCode == FtpStatusCode.PathnameCreated)
                    {

                        //If folder created, upload file

                        string ftpUploadFullPath = "" + ftpfullpath + "/" + f.Name + "";



                        // Checking File Exit or not

                        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP Address  

                        ftpRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password); // Credentials  

                        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;

                        FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());

                        List<string> directories = new List<string>(); // create list to store directories.   

                        string line = streamReader.ReadLine();

                        while (!string.IsNullOrEmpty(line))
                        {
                            directories.Add(line); // Add Each Directory to the List.  
                            line = streamReader.ReadLine();
                        }


                        int File_Check = 0;

                        for (int i = 0; i <= directories.Count - 1; i++)
                        {

                            string FileName = directories[i].ToString();

                            if (FileName == f.Name)
                            {
                                File_Check = 1;

                                break;
                                //1111
                            }
                            else
                            {

                                File_Check = 0;
                            }

                        }

                        if (File_Check == 0)
                        {

                            FtpWebRequest ftpUpLoadFile = (FtpWebRequest)FtpWebRequest.Create(ftpUploadFullPath);
                            ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);

                            ftpUpLoadFile.KeepAlive = true;

                            ftpUpLoadFile.UseBinary = true;

                            ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;

                            FileStream fs = File.OpenRead(file);

                            byte[] buffer = new byte[fs.Length];

                            fs.Read(buffer, 0, buffer.Length);

                            fs.Close();

                            Stream ftpstream = ftpUpLoadFile.GetRequestStream();

                            ftpstream.Write(buffer, 0, buffer.Length);

                            ftpstream.Close();
                            count++;
                            htorderkb.Clear();
                            dtorderkb.Clear();
                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", txt_Inhouse_Instruction.Text);
                            htorderkb.Add("@Document_Type", int.Parse(ddl_Inhouse_Doc_Type.SelectedValue.ToString()));
                            htorderkb.Add("@Order_ID", OrderId);
                            htorderkb.Add("@Document_Name", op1.SafeFileName);
                            htorderkb.Add("@File_Size", File_size);

                            htorderkb.Add("@Document_Path", dest_path1);
                            htorderkb.Add("@Inserted_By", userid);
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                        }
                        else
                        {
                            MessageBox.Show("File already exist");
                        }
                    }
                }
                catch (Exception)
                {

                    string ftpUploadFullPath = "" + ftpfullpath + "/" + f.Name + "";



                    // Checking File Exit or not

                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath); // FTP Address  

                    ftpRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password); // Credentials  

                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;

                    FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());

                    List<string> directories = new List<string>(); // create list to store directories.   

                    string line = streamReader.ReadLine();

                    while (!string.IsNullOrEmpty(line))
                    {
                        directories.Add(line); // Add Each Directory to the List.  
                        line = streamReader.ReadLine();
                    }


                    int File_Check = 0;

                    for (int i = 0; i <= directories.Count - 1; i++)
                    {

                        string FileName = directories[i].ToString();

                        if (FileName == f.Name)
                        {
                            File_Check = 1;

                            break;
                            //1111
                        }
                        else
                        {

                            File_Check = 0;
                        }

                    }

                    if (File_Check == 0)
                    {

                        FtpWebRequest ftpUpLoadFile = (FtpWebRequest)FtpWebRequest.Create(ftpUploadFullPath);
                        ftpUpLoadFile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);

                        ftpUpLoadFile.KeepAlive = true;

                        ftpUpLoadFile.UseBinary = true;

                        ftpUpLoadFile.Method = WebRequestMethods.Ftp.UploadFile;

                        FileStream fs = File.OpenRead(file);

                        byte[] buffer = new byte[fs.Length];

                        fs.Read(buffer, 0, buffer.Length);

                        fs.Close();

                        Stream ftpstream = ftpUpLoadFile.GetRequestStream();

                        ftpstream.Write(buffer, 0, buffer.Length);

                        ftpstream.Close();
                        count++;
                        htorderkb.Clear();
                        dtorderkb.Clear();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Instuction", txt_Inhouse_Instruction.Text);
                        htorderkb.Add("@Document_Type", int.Parse(ddl_Inhouse_Doc_Type.SelectedValue.ToString()));
                        htorderkb.Add("@Order_ID", OrderId);
                        htorderkb.Add("@Document_Name", op1.SafeFileName);
                        htorderkb.Add("@File_Size", File_size);

                        htorderkb.Add("@Document_Path", dest_path1);
                        htorderkb.Add("@Inserted_By", userid);
                        htorderkb.Add("@Inserted_date", DateTime.Now);
                        dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                    }
                    else
                    {
                        MessageBox.Show("File already exist");
                    }
                }


            }
            MessageBox.Show(Convert.ToString(count) + " File(s) copied");
            Gridview_bindInhouse_Final_Document_Upload();
        }

        private void Gridview_bindInhouse_Final_Document_Upload()
        {
            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            htselect.Add("@Trans", "SELECT_FINAL_DOCUMENT");
            htselect.Add("@Order_Id", OrderId);
            dtselect = dataaccess.ExecuteSP("Sp_Document_Upload", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grid_Inhouse_Final_Document_Upload.Columns[0].Width = 100;
                grid_Inhouse_Final_Document_Upload.Columns[1].Width = 200;
                grid_Inhouse_Final_Document_Upload.Columns[2].Width = 80;
                grid_Inhouse_Final_Document_Upload.Columns[3].Width = 80;
                grid_Inhouse_Final_Document_Upload.Columns[4].Width = 60;
                grid_Inhouse_Final_Document_Upload.Columns[5].Width = 60;
                grid_Inhouse_Final_Document_Upload.Columns[6].Width = 60;
                grid_Inhouse_Final_Document_Upload.Columns[7].Width = 60;

                grid_Inhouse_Final_Document_Upload.Columns[8].Width = 60;
                grid_Inhouse_Final_Document_Upload.Columns[9].Width = 60;

                if (dtselect.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    grid_Inhouse_Final_Document_Upload.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grid_Inhouse_Final_Document_Upload.Rows.Add();
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[0].Value = dtselect.Rows[i]["Document_Type"].ToString();
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[1].Value = dtselect.Rows[i]["Instuction"].ToString();
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[2].Value = dtselect.Rows[i]["File_Size"].ToString();
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_Name"].ToString();
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[4].Value = dtselect.Rows[i]["Inserted_date"].ToString();
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[5].Value = "View";
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[6].Value = "Edit";
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[7].Value = "Delete";
                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[8].Value = dtselect.Rows[i]["Document_Upload_Id"].ToString();

                        grid_Inhouse_Final_Document_Upload.Rows[i].Cells[9].Value = dtselect.Rows[i]["Document_Path"].ToString();

                    }



                }
            }
        }

        private void grid_Inhouse_Final_Document_Upload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {


                string Source_Path = grid_Inhouse_Final_Document_Upload.Rows[e.RowIndex].Cells[9].Value.ToString();
                if (Source_Path != "")
                {
                    string FileName = Path.GetFileName(Source_Path.ToString());
                    string docName = Path.GetFileName(Source_Path.ToString());

                    System.IO.Directory.CreateDirectory(@"C:\temp");

                    File.Copy(Source_Path, @"C:\temp\" + FileName, true);

                    System.Diagnostics.Process.Start(@"C:\temp\" + FileName);

                    //System.Diagnostics.Process.Start(Source_Path);
                }

            }
            else if (e.ColumnIndex == 7)
            {
                dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string DeletingType = grid_Inhouse_Final_Document_Upload.Rows[e.RowIndex].Cells[3].Value.ToString();
                    string Document_ID = grid_Inhouse_Final_Document_Upload.Rows[e.RowIndex].Cells[8].Value.ToString();


                    Hashtable htdel = new Hashtable();
                    System.Data.DataTable dtdel = new System.Data.DataTable();
                    htdel.Add("@Trans", "DELETE");
                    htdel.Add("@Document_Upload_Id", Document_ID.ToString());
                    dtdel = dataaccess.ExecuteSP("Sp_Document_Upload", htdel);
                    Gridview_bindInhouse_Final_Document_Upload();

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }


            }
        }

        private bool Validaet_Typing_Doc()
        {

            Hashtable htsel = new Hashtable();
            System.Data.DataTable dtsel = new System.Data.DataTable();
            htsel.Add("@Trans", "GET_TYPING_PACKAGE_BY_ORDER_ID");
            htsel.Add("@Order_ID", OrderId);
            dtsel = dataaccess.ExecuteSP("Sp_Document_Upload", htsel);
            if (dtsel.Rows.Count > 0)
            {


                Typing_Count = dtsel.Rows.Count;


                if (Typing_Count > 1)
                {
                    MessageBox.Show("Only one document need to select");
                    return false;

                }
                else
                {

                    return true;
                }

            }
            else
            {

                MessageBox.Show("Please select Any one document");

                return false;
            }





        }



        private void btn_Typing_Final_Report_Click(object sender, EventArgs e)
        {


            //getting the Contentent of Template_Path
            if (Validaet_Typing_Doc() != false)
            {
                cProbar.startProgress();
                Hashtable htsel = new Hashtable();
                System.Data.DataTable dtsel = new System.Data.DataTable();
                htsel.Add("@Trans", "GET_TYPING_PACKAGE_BY_ORDER_ID");
                htsel.Add("@Order_ID", OrderId);
                dtsel = dataaccess.ExecuteSP("Sp_Document_Upload", htsel);
                if (dtsel.Rows.Count > 0)
                {

                    //get the Header Template path from Server

                    Hashtable htheader = new Hashtable();

                    System.Data.DataTable dtheader = new System.Data.DataTable();

                    htheader.Add("@Trans", "SELECT_PATH_BY_ORDERTYPE_WISE");
                    htheader.Add("@Sub_Client_Id", Sub_Process_Id);
                    htheader.Add("@Order_Type_Abrivation", Order_Type_Abbrivation);
                    dtheader = dataaccess.ExecuteSP("Sp_Client_Template", htheader);

                    if (dtheader.Rows.Count > 0)
                    {

                        Content_Path = dtsel.Rows[0]["Document_Path"].ToString();
                        Header_Path = dtheader.Rows[0]["Header_Template_Path"].ToString();

                        Object oMissing = System.Reflection.Missing.Value;

                        var wordApp = new Word.Application();

                        var ContentDoc = wordApp.Documents.Open(@Content_Path);


                        // you can do the line above by passing ReadOnly=False like this as well
                        //var originalDoc = wordApp.Documents.Open(@oTemplatePath, oMissing, false);


                        ContentDoc.ActiveWindow.Selection.WholeStory();


                        ContentDoc.ActiveWindow.Selection.Copy();
                        //copy the sourcefile to Destination to  Pate
                        ContentDoc.Close();


                        //Copy File into Order file 

                        string Header_FileName = "Final_Property_report" + Order_No + ".docx";

                        string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId + @"\" + Header_FileName;
                        DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                        de.Username = "administrator";
                        de.Password = "password1$";


                        Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId);

                        File.Copy(Header_Path, dest_path1, true);




                        //Need to Paste the Contents to Destination path

                        var Destinationdoc = wordApp.Documents.Open(@dest_path1);


                        Destinationdoc.ActiveWindow.Selection.WholeStory();



                        Destinationdoc.ActiveWindow.Selection.PasteAndFormat(WdRecoveryType.wdUseDestinationStylesRecovery);



                        Destinationdoc.SaveAs(@dest_path1);
                        Destinationdoc.Save();
                        Destinationdoc.Close();



                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ContentDoc);

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(Destinationdoc);
                        GC.Collect();





                        Hashtable htdel = new Hashtable();
                        System.Data.DataTable dtdel = new System.Data.DataTable();
                        htdel.Add("@Trans", "DELETE_BY_DOCUMENT_TYPE");
                        htdel.Add("@Order_ID", OrderId);
                        htdel.Add("@Document_Type", 2);
                        dtdel = dataaccess.ExecuteSP("Sp_Document_Upload", htdel);

                        Hashtable htorderkb = new Hashtable();
                        System.Data.DataTable dtorderkb = new System.Data.DataTable();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Instuction", "Typing Final Report");
                        htorderkb.Add("@Document_Type", 2);
                        htorderkb.Add("@Order_ID", OrderId);
                        htorderkb.Add("@Document_Name", Header_FileName);
                        htorderkb.Add("@Document_Path", dest_path1);
                        htorderkb.Add("@Inserted_By", userid);
                        htorderkb.Add("@Inserted_date", DateTime.Now);
                        dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                        Gridview_bindInhouse_Final_Document_Upload();
                        MessageBox.Show("Final Report Submitted Check in Upload Tab");

                    }
                    else
                    {

                        MessageBox.Show("Header Template is not Added please check it");
                    }


                }

                cProbar.stopProgress();
            }


        }

        private void grd_Vendor_Documents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 7)
                {
                    //1111
                    string Source_Path = grd_Vendor_Documents.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (Source_Path != "")
                    {
                        string fileName = Path.GetFileName(Source_Path).Replace("%20", " ");
                        Download_Ftp_File(fileName, Source_Path);
                    }
                }
            }

        }

        private void Grd_Document_upload_DragDrop(object sender, DragEventArgs e)
        {
            Hashtable htorderkb = new Hashtable();
            System.Data.DataTable dtorderkb = new System.Data.DataTable();
            // Can only drop files, so check
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                htorderkb.Clear();
                dtorderkb.Clear();
                string dest = homeFolder + "\\" + Path.GetFileName(file);
                bool isFolder = Directory.Exists(file);
                bool isFile = File.Exists(file);
                System.IO.FileInfo f = new System.IO.FileInfo(file);
                double filesize = f.Length;
                GetFileSize(filesize);
                if (!isFolder && !isFile)                    // Ignore if it doesn't exist

                    continue;

                try
                {
                    switch (e.Effect)
                    {
                        case DragDropEffects.Copy:
                            if (isFile)					// TODO: Need to handle folders
                                File.Copy(file, dest, false);

                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", txt_path.Text);
                            htorderkb.Add("@Order_ID", OrderId);
                            htorderkb.Add("@Document_Name", Path.GetFileName(file));
                            htorderkb.Add("@File_Size", File_size);
                            //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                            // htorderkb.Add("@Extension", extension);
                            htorderkb.Add("@Document_Path", dest);
                            if (userid != 0)
                            {
                                htorderkb.Add("@Inserted_By", userid);
                            }
                            else if (userid == 0)
                            {


                            }
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                            break;
                        case DragDropEffects.Move:
                            if (isFile)
                                File.Move(file, dest);

                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", txt_path.Text);
                            htorderkb.Add("@Order_ID", OrderId);
                            htorderkb.Add("@Document_Name", Path.GetFileName(file));
                            htorderkb.Add("@File_Size", File_size);
                            //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                            // htorderkb.Add("@Extension", extension);
                            htorderkb.Add("@Document_Path", dest);
                            htorderkb.Add("@Inserted_By", userid);
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                            break;
                        case DragDropEffects.Link:
                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", txt_path.Text);
                            htorderkb.Add("@Order_ID", OrderId);
                            htorderkb.Add("@Document_Name", Path.GetFileName(file));
                            htorderkb.Add("@File_Size", File_size);
                            //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                            // htorderkb.Add("@Extension", extension);
                            htorderkb.Add("@Document_Path", dest);
                            htorderkb.Add("@Inserted_By", userid);
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);// TODO: Need to handle links
                            break;

                    }
                }
                catch (IOException ex)
                {

                    dialogResult = MessageBox.Show("This File Is Alerday Exist,Do you want to Replace?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {


                        switch (e.Effect)
                        {

                            case DragDropEffects.Copy:
                                if (isFile)					// TODO: Need to handle folders
                                    File.Copy(file, dest, true);

                                htorderkb.Add("@Trans", "INSERT");
                                htorderkb.Add("@Instuction", txt_path.Text);
                                htorderkb.Add("@Order_ID", OrderId);
                                htorderkb.Add("@Document_Name", Path.GetFileName(file));
                                htorderkb.Add("@File_Size", File_size);
                                //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                                // htorderkb.Add("@Extension", extension);
                                htorderkb.Add("@Document_Path", dest);
                                htorderkb.Add("@Inserted_By", userid);
                                htorderkb.Add("@Inserted_date", DateTime.Now);
                                dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                                break;
                        }
                    }
                    else
                    {

                    }



                }
            }

            RefreshView();
            Grd_Document_upload_Load();
        }


        private void Gridview_bind_Tax_Document_Upload()
        {


            System.Data.DataTable dt_Upload = new System.Data.DataTable();



            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();

            if (Tax_Order_Task == 21)
            {
                htselect.Add("@Trans", "GET_TAX_DOCUMENTS");
            }
            else if (Tax_Order_Task == 22 || Tax_Order_Task == 26)
            {
                htselect.Add("@Trans", "GET_INTERNAL_TAX_DOCUMENTS");

            }
            htselect.Add("@Order_Id", OrderId);
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htselect);
            if (Tax_Order_Task == 21)
            {
                dt_Upload = dtselect;
            }
            else if (Tax_Order_Task == 22 || Tax_Order_Task == 26)
            {

                Hashtable htselect1 = new Hashtable();
                System.Data.DataTable dtselect1 = new System.Data.DataTable();

                htselect1.Add("@Trans", "GET_INTERNAL_TAX_DOCUMENTS_LAST_UPDTAED_TAX_DETAILS");

                htselect1.Add("@Order_Id", OrderId);

                dtselect1 = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htselect1);




                dt_Upload = dtselect.Copy();
                dt_Upload.Merge(dtselect1);


            }



            if (dt_Upload.Rows.Count > 0)
            {
                Grid_Tax_Upload.Columns[0].Width = 100;
                Grid_Tax_Upload.Columns[1].Width = 50;
                Grid_Tax_Upload.Columns[2].Width = 100;
                Grid_Tax_Upload.Columns[3].Width = 80;
                Grid_Tax_Upload.Columns[4].Width = 60;
                Grid_Tax_Upload.Columns[5].Width = 60;
                Grid_Tax_Upload.Columns[6].Width = 60;
                Grid_Tax_Upload.Columns[7].Width = 60;



                if (dt_Upload.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Tax_Upload.Rows.Clear();
                    for (int i = 0; i < dt_Upload.Rows.Count; i++)
                    {
                        Grid_Tax_Upload.Rows.Add();
                        Grid_Tax_Upload.Rows[i].Cells[0].Value = dt_Upload.Rows[i]["Instuction"].ToString();



                        Grid_Tax_Upload.Rows[i].Cells[1].Value = dt_Upload.Rows[i]["FileSize"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[2].Value = dt_Upload.Rows[i]["User_Name"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[3].Value = dt_Upload.Rows[i]["Inserted_date"].ToString();

                        Grid_Tax_Upload.Rows[i].Cells[4].Value = "View";

                        Grid_Tax_Upload.Rows[i].Cells[5].Value = "Delete";
                        Grid_Tax_Upload.Rows[i].Cells[6].Value = dt_Upload.Rows[i]["Tax_Document_Upload_Id"].ToString();

                        Grid_Tax_Upload.Rows[i].Cells[7].Value = dt_Upload.Rows[i]["New_Document_Path"].ToString();

                        Grid_Tax_Upload.Rows[i].Cells[8].Value = dt_Upload.Rows[i]["Document_Type_Id"].ToString();

                    }



                }
            }
            else
            {
                Grid_Tax_Upload.Rows.Clear();
                Grid_Tax_Upload.DataSource = null;

            }
        }




        private void Grid_Tax_Upload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {


                string Source_Path = Grid_Tax_Upload.Rows[e.RowIndex].Cells[7].Value.ToString();
                string Document_Type_Id = Grid_Tax_Upload.Rows[e.RowIndex].Cells[8].Value.ToString();
                if (Source_Path != "" && Document_Type_Id != "16")
                {

                    string fileName = Path.GetFileName(Source_Path).Replace("%20", " ");
                    Download_Ftp_File(fileName, Source_Path);
                    //string docName = Path.GetFileName(Source_Path.ToString());

                    //System.IO.Directory.CreateDirectory(@"C:\temp");

                    //File.Copy(Source_Path, @"C:\temp\" + docName, true);

                    //System.Diagnostics.Process.Start(@"C:\temp\" + docName);
                }
                else if (Document_Type_Id == "16")
                {

                    Tax_Order_Note_Pad txnote = new Tax_Order_Note_Pad(OrderId, 0, 0, 0, "View_Tax_Details", Order_No);
                    txnote.Show();
                }

            }
        }

        //private void Grd_Document_upload_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {

                for (int i = 0; i < grd_Vendor_Documents.Rows.Count; i++)
                {

                    grd_Vendor_Documents[0, i].Value = true;
                }
            }
            else if (chk_All.Checked == false)
            {

                for (int i = 0; i < grd_Vendor_Documents.Rows.Count; i++)
                {

                    grd_Vendor_Documents[0, i].Value = false;
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Hashtable htorderkb = new Hashtable();
            System.Data.DataTable dtorderkb = new System.Data.DataTable();

            for (int i = 0; i < grd_Vendor_Documents.Rows.Count; i++)
            {


                bool isChecked = (bool)grd_Vendor_Documents[0, i].FormattedValue;
                if (isChecked == true)
                {





                    string s = grd_Vendor_Documents.Rows[i].Cells[8].Value.ToString();
                    FName = grd_Vendor_Documents.Rows[i].Cells[3].Value.ToString().Split('\\');
                    string docname = grd_Vendor_Documents.Rows[i].Cells[3].Value.ToString();

                    string Doc_Size = grd_Vendor_Documents.Rows[i].Cells[4].Value.ToString();



                    string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId + @"\" + FName[FName.Length - 1];
                    string Order_Document_Id = grd_Vendor_Documents.Rows[i].Cells[4].Value.ToString();
                    DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                    de.Username = "administrator";
                    de.Password = "password1$";


                    Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId);
                    try
                    {
                        if (docname != "")
                        {

                            File.Copy(s, dest_path1, false);
                        }
                        File_Count++;
                        htorderkb.Clear();
                        dtorderkb.Clear();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Instuction", grd_Vendor_Documents.Rows[i].Cells[1].Value.ToString() + " - " + grd_Vendor_Documents.Rows[i].Cells[2].Value.ToString());
                        htorderkb.Add("@Order_ID", OrderId);
                        htorderkb.Add("@File_Size", Doc_Size);
                        htorderkb.Add("@Document_Name", docname);
                        //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                        // htorderkb.Add("@Extension", extension);
                        if (docname != "")
                        {
                            htorderkb.Add("@Document_Path", dest_path1);
                        }
                        htorderkb.Add("@Inserted_By", userid);
                        htorderkb.Add("@Inserted_date", DateTime.Now);
                        dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);



                        Hashtable htupdate_iscopied = new Hashtable();
                        System.Data.DataTable dtupdate_iscopied = new System.Data.DataTable();
                        htupdate_iscopied.Add("@Trans", "Update_Iscopied");
                        htupdate_iscopied.Add("@Order_Document_Id", grd_Vendor_Documents.Rows[i].Cells[9].Value.ToString());
                        dtupdate_iscopied = dataaccess.ExecuteSP("Sp_Vendor_Order_Documents", htupdate_iscopied);
                    }
                    catch (Exception ex)
                    {


                        dialogResult = MessageBox.Show("This is File Is Already Exist Do you want to Replace?", "Warning", MessageBoxButtons.YesNo);
                        Existance_File_Copied = 1;
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (docname != "")
                            {
                                File.Copy(s, dest_path1, true);
                            }
                            File_Count++;
                            htorderkb.Clear();
                            dtorderkb.Clear();
                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", grd_Vendor_Documents.Rows[i].Cells[1].Value.ToString() + " - " + grd_Vendor_Documents.Rows[i].Cells[2].Value.ToString());
                            htorderkb.Add("@Order_ID", OrderId);
                            htorderkb.Add("@File_Size", Doc_Size);
                            htorderkb.Add("@Document_Name", docname);
                            //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                            // htorderkb.Add("@Extension", extension);
                            if (docname != "")
                            {
                                htorderkb.Add("@Document_Path", dest_path1);
                            }
                            htorderkb.Add("@Inserted_By", userid);
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);

                            Hashtable htupdate_iscopied = new Hashtable();
                            System.Data.DataTable dtupdate_iscopied = new System.Data.DataTable();
                            htupdate_iscopied.Add("@Trans", "Update_Iscopied");
                            htupdate_iscopied.Add("@Order_Document_Id", grd_Vendor_Documents.Rows[i].Cells[9].Value.ToString());
                            dtupdate_iscopied = dataaccess.ExecuteSP("Sp_Vendor_Order_Documents", htupdate_iscopied);

                        }
                        else
                        {


                        }
                    }




                }




            }


            if (File_Count > 0)
            {

                MessageBox.Show(Convert.ToString(File_Count) + " File(s) copied");

                Grd_Document_upload_Load();
                RefreshView();
                Grid_Bind_VendorDocuments();

                chk_All.Checked = false;
                if (chk_All.Checked == true)
                {

                    for (int i = 0; i < grd_Vendor_Documents.Rows.Count; i++)
                    {

                        grd_Vendor_Documents[0, i].Value = true;
                    }
                }
                else if (chk_All.Checked == false)
                {

                    for (int i = 0; i < grd_Vendor_Documents.Rows.Count; i++)
                    {

                        grd_Vendor_Documents[0, i].Value = false;
                    }
                }


            }

        }

        private bool Validate_Invoice_Genrated()
        {

            // Checking for Titlelogy vendor Db title Client Invoice is Genrated or not
            if (Inhouse_Client_Id == 33)
            {

                Hashtable ht_check = new Hashtable();
                System.Data.DataTable dt_check = new System.Data.DataTable();

                ht_check.Add("@Trans", "CHECK");
                ht_check.Add("@Order_ID", External_Order_Id);
                dt_check = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_check);
                int check = int.Parse(dt_check.Rows[0]["Count"].ToString());
                if (check == 0)
                {
                    MessageBox.Show("Invoice is Not Genrated please Genrate Invoice");
                    return false;
                }
                else
                {

                    return true;
                }
            }
            else
            {

                return true;
            }


        }

        private void btn_View_package_Click(object sender, EventArgs e)
        {
            if (Grid_Client_Upload.Rows.Count > 0 && Validate_Invoice_Genrated() != false)
            {
                External_Order_Id = int.Parse(Grid_Client_Upload.Rows[0].Cells[12].Value.ToString());

                Export_Report();

                Merge_Document_2();
            }
            else
            {

                MessageBox.Show("Check Search Package");
            }


        }


        public void Export_Report()
        {
            // This is only for DB title client and vendor
            if (Inhouse_Client_Id == 33 && Inhosue_Sub_Client_id != 263)
            {
                rptDoc = new InvoiceRep.InvReport.InvoiceReport_DbTitle();
                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_Id", External_Order_Id);




                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_ID", External_Order_Id);
                ExportOptions CrExportOptions;
                string Invoice_Order_Number = External_Client_Order_Number.ToString();
                string Source = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoice.pdf";

                string File_Name = "" + External_Client_Order_Number + ".pdf";
                //string Docname = FName[FName.Length - 1].ToString();
                string dest_path1 = @"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + External_Client_Order_Number + @"\" + File_Name;
                DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                de.Username = "administrator";
                de.Password = "password1$";


                Directory.CreateDirectory(@"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + External_Client_Order_Number);
                extension = Path.GetExtension(File_Name);
                File.Copy(Source, dest_path1, true);


                Hashtable htpath = new Hashtable();
                System.Data.DataTable dtpath = new System.Data.DataTable();

                Hashtable htcheck = new Hashtable();
                System.Data.DataTable dtcheck = new System.Data.DataTable();
                htcheck.Add("@Trans", "CHECK_INVOICE_FILE");
                htcheck.Add("@Order_Id", External_Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htcheck);
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
                    htpath.Add("@Document_Type_Id", 12);
                    htpath.Add("@Order_Id", External_Order_Id);
                    htpath.Add("@Document_From", 2);
                    htpath.Add("@Document_File_Type", extension.ToString());
                    htpath.Add("@Description", "Invoice");
                    htpath.Add("@Document_Path", dest_path1);
                    htpath.Add("@File_Size", File_size);

                    htpath.Add("@Inserted_date", DateTime.Now);
                    htpath.Add("@status", "True");
                    dtpath = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htpath);

                }

                Hashtable htgetpath = new Hashtable();
                System.Data.DataTable dtgetpath = new System.Data.DataTable();
                htgetpath.Add("@Trans", "GET_PATH");
                htgetpath.Add("@Order_Id", External_Order_Id);
                dtgetpath = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htgetpath);

                if (dtgetpath.Rows.Count > 0)
                {
                    View_File_Path = dtgetpath.Rows[0]["Document_Path"].ToString();
                }
                FileInfo newFile = new FileInfo(View_File_Path);

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


        }

        private void Grd_Document_upload_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {

        }

        public void Logon_To_Crystal()
        {

            crConnectionInfo.ServerName = server;
            crConnectionInfo.DatabaseName = database;
            crConnectionInfo.UserID = UserID;
            crConnectionInfo.Password = password;
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

        public void Merge_Document_2()
        {
            Hashtable htsearch = new Hashtable();
            System.Data.DataTable dtsearch = new System.Data.DataTable();
            htsearch.Add("@Trans", "GET_SEARCH_PACKAGE_DOCUEMNT_PATH");
            htsearch.Add("@Order_Id", External_Order_Id);
            dtsearch = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htsearch);
            if (dtsearch.Rows.Count > 0)
            {
                P2 = dtsearch.Rows[0]["Document_Path"].ToString();
            }
            Hashtable htinvoice = new Hashtable();
            System.Data.DataTable dtinvoice = new System.Data.DataTable();
            htinvoice.Add("@Trans", "GET_INVOICE_DOCUEMNT_PATH");
            htinvoice.Add("@Order_Id", External_Order_Id);
            dtinvoice = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htinvoice);
            if (dtinvoice.Rows.Count > 0)
            {
                P1 = dtinvoice.Rows[0]["Document_Path"].ToString();
            }

            Hashtable htin = new Hashtable();
            System.Data.DataTable dtin = new System.Data.DataTable();
            htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
            htin.Add("@Sub_Process_Id", Inhosue_Sub_Client_id);
            dtin = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htin);
            if (dtin.Rows.Count > 0)
            {
                Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

            }
            DataSet ds = new DataSet();
            ds.Clear();



            if (Inv_Status == "True")
            {
                //ds.Tables.Add(dtinvoice);
                //ds.Merge(dtsearch);
            }
            else if (Inv_Status == "False")
            {

                //  ds.Tables.Add(dtsearch);
            }

            if (Inv_Status == "True")
            {
                if (dtsearch.Rows.Count > 0)
                {



                    //Define a new output document and its size, type

                    Package = "InvoiceAndSearch";
                    Merge_Invoice_Search();
                    FName = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf".Split('\\');
                    string Source_Path = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf";
                    System.IO.Directory.CreateDirectory(@"C:\temp");

                    File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
                    System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);


                }
                else
                {

                    MessageBox.Show("SearchPackage is Not Added Please Check it");

                }
            }
            else if (Inv_Status == "False")
            {
                if (dtsearch.Rows.Count > 0)
                {

                    Package = "Search";
                    Merge_Invoice_Search();
                    FName = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf".Split('\\');
                    string Source_Path = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf";
                    System.IO.Directory.CreateDirectory(@"C:\temp");
                    File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
                    System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);
                }
                else
                {

                    MessageBox.Show("Search package is not uploaded check it");
                }

            }

        }

        public void Merge_Invoice_Search()
        {


            //lstFiles[0] = @"C:/Users/DRNASM0001/Desktop/15-59989-Search Package.pdf";
            //lstFiles[1] = @"C:/Users/DRNASM0001/Desktop/Invoice.pdf";
            if (Inv_Status == "True" && Package == "InvoiceAndSearch")
            {
                Index = 3;

            }
            else if (Inv_Status == "False" && Package == "Search")
            {

                Index = 2;
            }
            string[] lstFiles = new string[Index];
            if (Inv_Status == "True" && Package == "InvoiceAndSearch")
            {

                lstFiles[0] = P1;

                lstFiles[1] = P2;
            }
            else if (Inv_Status == "False" && Package == "Search")
            {

                lstFiles[0] = P2;


            }
            //lstFiles[2] = @"C:/pdf/3.pdf";

            PdfReader reader = null;
            iTextSharp.text.Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            string outputPdfPath = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoicemerge.pdf";


            sourceDocument = new iTextSharp.text.Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

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
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private int get_pageCcount(string file)
        {

            //Regex regex = new Regex(@"/Type\s*/Page[^s]");
            //MatchCollection matches = regex.Matches(sr.ReadToEnd());
            PdfReader pdfReader = new PdfReader(File.OpenRead(file));
            int numberOfPages = pdfReader.NumberOfPages;
            //return matches.Count;

            return numberOfPages;

        }

        private void Grid_Client_Upload_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // if (e.ColumnIndex == 0)
            //{
            //    string myFilePath = Grid_Client_Upload.Rows[e.RowIndex].Cells[10].Value.ToString();
            //    string ext = Path.GetExtension(myFilePath);
            //    bool ischeck = (bool)Grid_Client_Upload[0, e.RowIndex].FormattedValue;
            //    string Document_ID = Grid_Client_Upload.Rows[e.RowIndex].Cells[9].Value.ToString();
            //    Hashtable htupdate = new Hashtable();
            //    System.Data.DataTable dtupdate = new System.Data.DataTable();

            //    htupdate.Add("@Trans", "UPDATE_FILE_BY_ORDER");
            //    htupdate.Add("@Order_Id", External_Order_Id);
            //    dtupdate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htupdate);

            //    if (ext == ".pdf" || ext == ".PDF")
            //    {
            //        htupdate.Clear();
            //        dtupdate.Clear();

            //        if (ischeck == false)
            //        {


            //            htupdate.Add("@Trans", "UPDATE_FILE");
            //            htupdate.Add("@Order_Document_Id", Document_ID);
            //            htupdate.Add("@Check_File", "True");
            //            dtupdate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htupdate);
            //            MessageBox.Show("File Status Checkd");

            //        }
            //        else
            //        {


            //            htupdate.Add("@Trans", "UPDATE_FILE");
            //            htupdate.Add("@Order_Document_Id", Document_ID);
            //            htupdate.Add("@Check_File", "False");
            //            dtupdate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htupdate);
            //            MessageBox.Show("File Status Un Checkd");

            //        }




            //        foreach (DataGridViewRow dr in Grid_Client_Upload.Rows)
            //        {
            //            dr.Cells[0].Value = false;//sıfırın
            //        }
            //        Gridview_bind_External_Client_Document_Upload();


            //    }
            //    else
            //    {

            //        MessageBox.Show("Select Only Pdf Files");
            //       // Gridview_bind_External_Client_Document_Upload();
            //    }




            //}
        }

        #region Download Ftp File

        /// <summary>
        /// Download File: When the Download ToolStripButton is clicked. Displays a SaveFileDialog.
        /// </summary>
        /// <param name="FileName">Name of the File to Download</param>
        /// <param name="CurrentDirectory">CurrentDirectory (Directory from which to download on server)</param>

        private void Download_Ftp_File1(string p, string File_path, string Target_Path)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + p;
                FileStream outputStream = new FileStream("" + Target_Path + "\\" + fileName, FileMode.Create);
                FtpWebRequest ftpDownloadReq = (FtpWebRequest)WebRequest.Create(new Uri(File_path));
                ftpDownloadReq.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpDownloadReq.UseBinary = true;
                ftpDownloadReq.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)ftpDownloadReq.GetResponse();
                Stream stream = response.GetResponseStream();
                stream.CopyTo(outputStream);
                stream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in Downloading Files please Check with Administrator");
            }
        }


        #endregion

        private void CommonSaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }


    }
}



