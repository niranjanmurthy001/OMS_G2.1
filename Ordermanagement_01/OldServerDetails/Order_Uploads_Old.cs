using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Runtime.InteropServices;
using System.IO;
using System.DirectoryServices;
using System.Diagnostics;
using Ordermanagement_01.Classes;

namespace Ordermanagement_01
{
    public partial class Order_Uploads_Old : Form
    {

        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem cutMenuItem;
        private System.Windows.Forms.MenuItem copyMenuItem;
        private System.Windows.Forms.MenuItem pasteMenuItem;
        private System.Windows.Forms.MenuItem refreshMenuItem;
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
        string Final_Typing_Report_Path,Order_Type_Abbrivation,Sub_Process_Id,USer_Role_Id;
        int Typing_Count;
        string File_size;
        int Tax_Order_Task;
        static string dest_path1;
        string User_Role_Id;
        int File_Count,Existance_File_Copied;

        Olddb_Datacess dataaccess = new Olddb_Datacess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();

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

        string External_Clinet_Id, External_Sub_client_Id,External_Order_Id;
        enum VirtualKeyStates : int
        {
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
        }
        public Order_Uploads_Old(string OPERATION, int Order_id, int user_id, string OrderNo, string Client, string Subclient)
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

            

                External_Clinet_Id = dt.Rows[0]["Clinet_Id"].ToString();
                External_Sub_client_Id = dt.Rows[0]["Sub_Client_Id"].ToString();
                External_Order_Id = dt.Rows[0]["External_Order_Id"].ToString();
            }
            else
            {
                External_Clinet_Id = "0";
            }
            Gridview_bind_External_Client_Document_Upload();
        }



        private void Order_Uploads_Load(object sender, EventArgs e)
        {
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
            htchup.Add("@Order_ID",OrderId);
            htchup.Add("@order_Status", Order_status);
            htchup.Add("@User_Id",userid);
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

            //if (User_Role_Id == "2" && Doccount > 0)
            //{


            //    tabControl1.TabPages.Add(tabPage1);


            //}
            //else if (User_Role_Id == "1")
            //{

            //    tabControl1.TabPages.Add(tabPage1);
              

            //}
            //else
            //{
            //    tabControl1.TabPages.Remove(tabPage1);

            //}


           

       
            
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
           homeFolder = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId;
            System.IO.Directory.CreateDirectory(homeFolder);
            homeDisk = Path.GetPathRoot(homeFolder).ToUpper();		// C:\ or D:\
           this.Text = "Files in " + homeFolder;
            //this.Text = username + " - " + Order_No + " - " + "Documents Upload";
           // RefreshView();

            // Need to watch the folder for cut & paste operations as we get no notification when paste happens

            // Set Filter.

            //   fsw.Filter = (homeFolder.Equals(String.Empty)) ? "*.*" : homeFolder;

            // Monitor files and subdirectories.


            // Raise Event handlers.
            fsw = new FileSystemWatcher(homeFolder, "*.*");

            fsw.IncludeSubdirectories = true;
            // Monitor all changes specified in the NotifyFilters.

            fsw.NotifyFilter = NotifyFilters.Attributes |
                           NotifyFilters.CreationTime |
                           NotifyFilters.DirectoryName |
                           NotifyFilters.FileName |
                           NotifyFilters.LastAccess |
                           NotifyFilters.LastWrite |
                           NotifyFilters.Security |
                           NotifyFilters.Size;
            // Watch on events.
            //fsw.EnableRaisingEvents = true;
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.Deleted += new FileSystemEventHandler(fsw_Changed);
            fsw.Created += new FileSystemEventHandler(fsw_Changed);
            //  fsw.EnableRaisingEvents = true;
              Hashtable htordertask = new Hashtable();
            System.Data.DataTable dtordertask = new System.Data.DataTable();
            htordertask.Add("@Trans","GET_ORDER_TASK");
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
            else if (Tax_Order_Task == 22)
            {

                httaxcount.Add("@Trans", "COUNT_OF_INTERNAL_TAX_DOCUMENTS_BY_ORDER");
            }
            httaxcount.Add("@Order_Id",OrderId);
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
            

        }

        private void Get_Order_Type_Abb()
        {

            Hashtable htop = new Hashtable();
            System.Data.DataTable dtop = new System.Data.DataTable();
            htop.Add("@Trans", "GET_ORDER_TYPE_ABRIVATION");
            htop.Add("@Order_ID",OrderId);
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
            if (User_Role_Id == "1")
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
                ListViewItem listitem = new ListViewItem(dr[3].ToString());
                
                //listitem.SubItems.Add(dr[0].ToString());
                listView1.Items.Add(listitem);
              

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
            if (this.InvokeRequired)
            {
                this.Invoke(new ChangeHandler(fsw_Changed), new object[] { sender, e });
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
        private void btn_upload_Click(object sender, EventArgs e)
        {
            
                if (Operation == "Update")
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
                        string file = op1.FileName.ToString();
                        string Docname = FName[FName.Length - 1].ToString();
                        for (int i = 0; i < Grd_Document_upload.Rows.Count; i++)
                        {
                            if (Docname == Grd_Document_upload.Rows[i].Cells[2].Value.ToString())
                            {
                                Chk = 1;
                                break;
                            }
                            else
                            {
                                Chk = 0;
                            }
                            
                        }
                        System.IO.FileInfo f = new System.IO.FileInfo(file);
                        double filesize = f.Length;
                        
                        GetFileSize(filesize);

                        dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId + @"\" + FName[FName.Length - 1];
                        if (Chk == 0)
                        {
                            dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId + @"\" + FName[FName.Length - 1];
                            //double temp = ((FName.Length) / 1024f) / 1024f;

                            //string File_size = Convert.ToString(temp);
                            
                            

                            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
                            // show a single decimal place, and no space.
                            //string result = String.Format("{0:0.##} {1}", len, sizes[order]);
                            

                            DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                            de.Username = "administrator";
                            de.Password = "password1$";

                            Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId);
                            try
                            {
                                File.Copy(s, dest_path1, false);
                                count++;
                                htorderkb.Clear();
                                dtorderkb.Clear();
                                htorderkb.Add("@Trans", "INSERT");
                                htorderkb.Add("@Instuction", txt_path.Text);
                                htorderkb.Add("@Order_ID", OrderId);
                                htorderkb.Add("@File_Size", File_size);
                                htorderkb.Add("@Document_Name", op1.SafeFileName);
                                //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                                // htorderkb.Add("@Extension", extension);
                                htorderkb.Add("@Document_Path", dest_path1);
                                htorderkb.Add("@Inserted_By", userid);
                                htorderkb.Add("@Inserted_date", DateTime.Now);
                                dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                                Grd_Document_upload_Load();
                                RefreshView();
                            }
                            catch (Exception ex)
                            {

                                
                             dialogResult=   MessageBox.Show("This is File Is Already Exist Do you want to Replace?","Warning",MessageBoxButtons.YesNo);

                             if (dialogResult == DialogResult.Yes)
                             {

                                 File.Copy(s, dest_path1, true);
                                 count++;
                                 htorderkb.Clear();
                                 dtorderkb.Clear();
                                 htorderkb.Add("@Trans", "INSERT");
                                 htorderkb.Add("@Instuction", txt_path.Text);
                                 htorderkb.Add("@Order_ID", OrderId);
                                 htorderkb.Add("@File_Size", File_size);
                                 htorderkb.Add("@Document_Name", op1.SafeFileName);
                                 //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                                 // htorderkb.Add("@Extension", extension);
                                 htorderkb.Add("@Document_Path", dest_path1);
                                 htorderkb.Add("@Inserted_By", userid);
                                 htorderkb.Add("@Inserted_date", DateTime.Now);
                                 dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                                 Grd_Document_upload_Load();
                                 RefreshView();


                             }
                             else
                             { 
                             

                             }



                            }
                        }
                        else
                        {
                            dialogResult = MessageBox.Show("This is File Is Already Exist Do you want to Replace?", "Warning", MessageBoxButtons.YesNo);

                            if (dialogResult == DialogResult.Yes)
                            {

                                File.Copy(s, dest_path1, true);
                                count++;
                                htorderkb.Clear();
                                dtorderkb.Clear();
                                htorderkb.Add("@Trans", "INSERT");
                                htorderkb.Add("@Instuction", txt_path.Text);
                                htorderkb.Add("@Order_ID", OrderId);
                                htorderkb.Add("@File_Size", File_size);
                                htorderkb.Add("@Document_Name", op1.SafeFileName);
                                //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                                // htorderkb.Add("@Extension", extension);
                                htorderkb.Add("@Document_Path", dest_path1);
                                htorderkb.Add("@Inserted_By", userid);
                                htorderkb.Add("@Inserted_date", DateTime.Now);
                                dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                                Grd_Document_upload_Load();
                                RefreshView();


                            }
                            else
                            {


                            }
                          
                        }
                    }
                    MessageBox.Show(Convert.ToString(count) + " File(s) copied");
                }
                else if (Operation == "Insert")
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
                        string Docname = FName[FName.Length - 1].ToString();
                        for (int i = 0; i < Grd_Document_upload.Rows.Count; i++)
                        {
                            if (Docname == Grd_Document_upload.Rows[i].Cells[2].Value.ToString())
                            {
                                Chk = 1;
                                break;
                            }
                            else
                            {
                                Chk = 0;
                            }
                        }
                        if (Chk == 0)
                        {
                            string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId + @"\" + FName[FName.Length - 1];
                            DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                            de.Username = "administrator";
                            de.Password = "password1$";

                            Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + OrderId);

                            File.Copy(s, dest_path1, true);
                            count++;
                            htorderkb.Clear();
                            dtorderkb.Clear();
                            htorderkb.Add("@Trans", "INSERT");
                            htorderkb.Add("@Instuction", txt_path.Text);
                            htorderkb.Add("@Order_ID", OrderId);
                            htorderkb.Add("@File_Size", File_size);
                            htorderkb.Add("@Document_Name", op1.SafeFileName);
                            //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                            // htorderkb.Add("@Extension", extension);
                            htorderkb.Add("@Document_Path", dest_path1);
                            htorderkb.Add("@Inserted_By", userid);
                            htorderkb.Add("@Inserted_date", DateTime.Now);
                            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                            Grd_Document_upload_Load();
                        }
                        else
                        {
                            MessageBox.Show("File name already exists");
                        }
                    }
                    MessageBox.Show(Convert.ToString(count) + " File(s) copied");
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
            if (User_Role_Id == "1")
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
            if (dtDocument_Select.Rows.Count > 0)
            {

                Grd_Document_upload.DataSource = null;
                Grd_Document_upload.Columns.Clear();
                Grd_Document_upload.Rows.Clear();
                //ex2.Visible = true;
                Grd_Document_upload.Visible = true;
                Grd_Document_upload.AutoGenerateColumns = false;
                Grd_Document_upload.ColumnCount = 7;

                Grd_Document_upload.Columns[0].Name = "Instuction";
                Grd_Document_upload.Columns[0].HeaderText = "INSTRUCTION";
                Grd_Document_upload.Columns[0].DataPropertyName = "Instuction";


                Grd_Document_upload.Columns[1].Name = "DocumentPath";
                Grd_Document_upload.Columns[1].HeaderText = "FILE PATH";
                Grd_Document_upload.Columns[1].DataPropertyName = "Document_Path";
                Grd_Document_upload.Columns[1].Visible = false;

                Grd_Document_upload.Columns[2].Name = "FileName";
                Grd_Document_upload.Columns[2].HeaderText = "FILE NAME";
                Grd_Document_upload.Columns[2].DataPropertyName = "Document_Name";

                Grd_Document_upload.Columns[3].Name = "FileSize";
                Grd_Document_upload.Columns[3].HeaderText = "FILE SIZE";
                Grd_Document_upload.Columns[3].DataPropertyName = "File_Size";

                Grd_Document_upload.Columns[4].Name = "Inserted_date";
                Grd_Document_upload.Columns[4].HeaderText = "Date";
                Grd_Document_upload.Columns[4].DataPropertyName = "Inserted_date";

                Grd_Document_upload.Columns[5].Name = "username";
                Grd_Document_upload.Columns[5].HeaderText = "USER NAME";
                Grd_Document_upload.Columns[5].DataPropertyName = "User_Name";

                Grd_Document_upload.Columns[6].Name = "upload_id";
                Grd_Document_upload.Columns[6].HeaderText = "upload_id";
                Grd_Document_upload.Columns[6].DataPropertyName = "Document_Upload_Id";
                Grd_Document_upload.Columns[6].Visible = false;


                if (User_Role_Id == "1")
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
                //bool ischecked=(bool).dtDocument_Select

                Grd_Document_upload.Columns.Add(chk_Inv);
                chk_Inv.HeaderText = "INVOICE";
                chk_Inv.Name = "check_Inv";

                Grd_Document_upload.Columns.Add(chk_Typ);
                chk_Typ.HeaderText = "TYPING";
                chk_Typ.Name = "check_Typeing";





                Grd_Document_upload.Columns.Add(btn);
                btn.HeaderText = "Open";
                btn.Text = "Open";
                btn.Name = "btn";

                Grd_Document_upload.Columns.Add(btnEdit);
                btnEdit.HeaderText = "Edit";
                btnEdit.Text = "Edit";
                btnEdit.Name = "btnEdit";

                Grd_Document_upload.Columns.Add(btnDelete);
                btnDelete.HeaderText = "Delete";
                btnDelete.Text = "Delete";
                btnDelete.Name = "btnDelete";
                //Grd_Document_upload.Columns[3].Name = "Open";
                //Grd_Document_upload.Columns[3].HeaderText = "Open";


                ////restricting the username in checklist for employee role
                //string Check_Clist = Grid_Tax_Upload.Rows[i].Cells[0].Value.ToString();


                //if (User_Role_Id == "2")
                //{
                //    if (Check_Clist.Contains("Check List Report") == true)
                //    {

                //        Grid_Tax_Upload.Rows.RemoveAt(i);
                //    }
                //}
                Grd_Document_upload.DataSource = dtDocument_Select;
                //Grd_Document_upload.DataBind();
                //btnDownload.Visible = true;
            }
            else
            {
                Grd_Document_upload.DataSource = null;
                //Grd_Document_upload.EmptyDataText = "No Records Are Avilable";
                //Grd_Document_upload.DataBind();
            }
            for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
            {
                for (int j = 0; j < Grd_Document_upload.Rows.Count; j++)
                {
                    Grd_Document_upload.Rows[j].Cells[10].Value = "View";
                    Grd_Document_upload.Rows[j].Cells[11].Value = "Edit";
                    Grd_Document_upload.Rows[j].Cells[12].Value = "Delete";
                    if (dtDocument_Select.Rows[i]["Document_Upload_Id"].ToString() == Grd_Document_upload.Rows[j].Cells[6].Value.ToString())
                    {
                        Grd_Document_upload.Columns[6].Visible = false;
                        bool ischecked = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString());
                        if (ischecked == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[7].Value = dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString();
                        }

                        bool ischecked1 = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString());
                        if (ischecked1 == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[8].Value = dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString();
                        }

                        bool ischecked2 = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_Typing_Package"].ToString());
                        if (ischecked2 == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[9].Value = dtDocument_Select.Rows[i]["Chk_Typing_Package"].ToString();
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
                Grd_Document_upload.ColumnCount = 6;

                Grd_Document_upload.Columns[0].Name = "Instuction";
                Grd_Document_upload.Columns[0].HeaderText = "INSTRUCTION";
                Grd_Document_upload.Columns[0].DataPropertyName = "Instuction";

                Grd_Document_upload.Columns[1].Name = "DocumentPath";
                Grd_Document_upload.Columns[1].HeaderText = "FILE PATH";
                Grd_Document_upload.Columns[1].DataPropertyName = "Document_Path";
                Grd_Document_upload.Columns[1].Visible = false;

                Grd_Document_upload.Columns[2].Name = "FileName";
                Grd_Document_upload.Columns[2].HeaderText = "FILE NAME";
                Grd_Document_upload.Columns[2].DataPropertyName = "Document_Name";

                Grd_Document_upload.Columns[3].Name = "Inserted_date";
                Grd_Document_upload.Columns[3].HeaderText = "Date";
                Grd_Document_upload.Columns[3].DataPropertyName = "Inserted_date";

                Grd_Document_upload.Columns[4].Name = "username";
                Grd_Document_upload.Columns[4].HeaderText = "USER NAME";
                Grd_Document_upload.Columns[4].DataPropertyName = "User_Name";

                Grd_Document_upload.Columns[5].Name = "upload_id";
                Grd_Document_upload.Columns[5].HeaderText = "upload_id";
                Grd_Document_upload.Columns[5].DataPropertyName = "Document_Upload_Id";
                Grd_Document_upload.Columns[5].Visible = false;

                Grd_Document_upload.Columns.Add(chk);
                chk.HeaderText = "UPLOAD PACKAGE";
                chk.Name = "check";
                //bool ischecked=(bool).dtDocument_Select
                Grd_Document_upload.Columns.Add(chk_Inv);
                chk_Inv.HeaderText = "INVOICE";
                chk_Inv.Name = "check_Inv";

                Grd_Document_upload.Columns.Add(btn);
                btn.HeaderText = "Open";
                btn.Text = "Open";
                btn.Name = "btn";

                Grd_Document_upload.Columns.Add(btnEdit);
                btnEdit.HeaderText = "Edit";
                btnEdit.Text = "Edit";
                btnEdit.Name = "btnEdit";

                Grd_Document_upload.Columns.Add(btnDelete);
                btnDelete.HeaderText = "Delete";
                btnDelete.Text = "Delete";
                btnDelete.Name = "btnDelete";
                //Grd_Document_upload.Columns[3].Name = "Open";
                //Grd_Document_upload.Columns[3].HeaderText = "Open";
                Grd_Document_upload.DataSource = dtDocument_Select;
                //Grd_Document_upload.DataBind();
                //btnDownload.Visible = true;
            }
            else
            {
                Grd_Document_upload.DataSource = null;
                //Grd_Document_upload.EmptyDataText = "No Records Are Avilable";
                //Grd_Document_upload.DataBind();
            }
            for (int i = 0; i < dtDocument_Select.Rows.Count; i++)
            {
                for (int j = 0; j < Grd_Document_upload.Rows.Count; j++)
                {
                    Grd_Document_upload.Rows[j].Cells[8].Value = "View";
                    Grd_Document_upload.Rows[j].Cells[9].Value = "Edit";
                    Grd_Document_upload.Rows[j].Cells[10].Value = "Delete";
                    if (dtDocument_Select.Rows[i]["Document_Upload_Id"].ToString() == Grd_Document_upload.Rows[j].Cells[4].Value.ToString())
                    {
                        Grd_Document_upload.Columns[5].Visible = false;
                        bool ischecked = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString());
                        if (ischecked == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[6].Value = dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString();
                        }
                        bool ischecked1 = Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString());
                        if (ischecked1 == true)
                        {
                            //chk.DataPropertyName=;

                            Grd_Document_upload.Rows[j].Cells[7].Value = dtDocument_Select.Rows[i]["Chk_Invoice_Pakage"].ToString();
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
                    grd_Vendor_Documents.Rows[i].Cells[8].Value = dt_Vendor.Rows[i]["Document_Path"].ToString();
                    grd_Vendor_Documents.Rows[i].Cells[9].Value = dt_Vendor.Rows[i]["Order_Document_Id"].ToString();

                    string Iscopied = dt_Vendor.Rows[i]["IsCopied_To_Inhouse"].ToString();
                    if (Iscopied=="True")
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
            if (e.ColumnIndex == 7)
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
            if (e.ColumnIndex == 8)
            {
                string myFilePath = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
                string ext = Path.GetExtension(myFilePath);
                bool ischeck = (bool)Grd_Document_upload[8, e.RowIndex].FormattedValue;
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
                    ischeck = (bool)Grd_Document_upload[8, e.RowIndex].FormattedValue;
                    if (ischeck == false)
                    {
                        //Grd_Document_upload.Rows[e.RowIndex].Cells[8].Value = false;
                        //Grd_Document_upload[8, e.RowIndex].Value = false;
                        //ischeck = false;
                        foreach (DataGridViewRow row in Grd_Document_upload.Rows)
                        {
                            (row.Cells[8] as DataGridViewCheckBoxCell).Value = false;
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
            if (e.ColumnIndex == 9)
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
            if (e.ColumnIndex == 10)
            {
                string myFilePath = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
                string ext = Path.GetExtension(myFilePath);
               
                FName = Grd_Document_upload.Rows[e.RowIndex].Cells[2].Value.ToString().Split('\\');
                string Source_Path = Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
                System.IO.Directory.CreateDirectory(@"C:\temp");

                File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
                Hashtable htUpload = new Hashtable();
                System.Data.DataTable dtUpload = new System.Data.DataTable();
                htUpload.Add("@Trans", "SELECT_EMP");
                htUpload.Add("@Employee_Id", userid);
                dtUpload = dataaccess.ExecuteSP("Sp_Employee_Status", htUpload);
                if (ext == ".txt")
                {

                    System.Diagnostics.Process.Start("notepad.exe", @"C:\temp\" + FName[FName.Length - 1]);

                }
                else if (ext == ".doc" || ext == ".docx")
                {

                    System.Diagnostics.Process.Start("WINWORD.EXE", @"C:\temp\" + FName[FName.Length - 1]);
                }
                else if (ext == ".xls" || ext == ".xlsx")
                {

                    System.Diagnostics.Process.Start("EXCEL.EXE", @"C:\temp\" + FName[FName.Length - 1]);
                }
                else
                {

                    System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);
                }
                
                //System.Diagnostics.Process.Start(Source_Path);

            }
           
            if (e.ColumnIndex == 11)
            {
                Hashtable htEdit = new Hashtable();
                System.Data.DataTable dtEdit = new System.Data.DataTable();
                htEdit.Add("@Trans", "UPDATE");
                htEdit.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[6].Value);
                htEdit.Add("@Instuction", Grd_Document_upload.Rows[e.RowIndex].Cells[0].Value);
                dtEdit = dataaccess.ExecuteSP("Sp_Document_Upload", htEdit);
                Grd_Document_upload_Load();
            }
            if (e.ColumnIndex == 12)
            {   dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Hashtable htUser = new Hashtable();
                System.Data.DataTable dtUser = new System.Data.DataTable();
                htUser.Add("@Trans", "SELPASS");
                htUser.Add("@User_id", userid);
                dtUser = dataaccess.ExecuteSP("Sp_User", htUser);
                if (dtUser.Rows[0]["User_Name"].ToString().ToUpper() == Grd_Document_upload.Rows[e.RowIndex].Cells[5].Value.ToString().ToUpper())
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
                    MessageBox.Show("You have no permission for delete this document");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            }
        }

        private void txt_path_TextChanged(object sender, EventArgs e)
        {

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
        private void Grd_Document_upload_MouseDown(object sender, MouseEventArgs e)
        {

            //    Grd_Document_upload.DoDragDrop(Grd_Document_upload.SelectedRows, DragDropEffects.Move);

        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {

        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] files = GetSelection();
            if (files != null)
            {
                DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy | DragDropEffects.Move /* | DragDropEffects.Link */);
                RefreshView();
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
            }
            else
                e.Effect = DragDropEffects.None;

            // This is an example of how to get the item under the mouse
            System.Drawing.Point pt = listView1.PointToClient(new System.Drawing.Point(e.X, e.Y));
            ListViewItem itemUnder = listView1.GetItemAt(pt.X, pt.Y);
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
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

        private void btn_Client_Upload_Click(object sender, EventArgs e)
        {
            if (External_Clinet_Id != "0")
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
                    GetFileSize(filesize);
                    FName = s.Split('\\');
                    string Docname = FName[FName.Length - 1].ToString();

                    string dest_path1 = @"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + Order_No + @"\" + FName[FName.Length - 1];
                    DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                    de.Username = "administrator";
                    de.Password = "password1$";

                    Directory.CreateDirectory(@"\\192.168.12.33\Titlelogy\" + External_Clinet_Id + @"\" + External_Sub_client_Id + @"\" + Order_No);
                    extension = Path.GetExtension(Docname);
                    File.Copy(s, dest_path1, true);
                    count++;
                    htorderkb.Clear();
                    dtorderkb.Clear();
                    htorderkb.Add("@Trans", "INSERT");
                    htorderkb.Add("@Document_Type_Id", int.Parse(ddl_Dcoument_Type.SelectedValue.ToString()));
                    htorderkb.Add("@Order_Id", External_Order_Id);
                    htorderkb.Add("@Document_From", 2);
                    htorderkb.Add("@Document_File_Type", extension.ToString());
                    htorderkb.Add("@Description", txt_Dscription.Text.ToString());
                    htorderkb.Add("@Document_Path", dest_path1);
                    htorderkb.Add("@File_Size", File_size);

                    htorderkb.Add("@Inserted_date", DateTime.Now);
                    htorderkb.Add("@status", "True");
                    dtorderkb = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htorderkb);
                    Gridview_bind_External_Client_Document_Upload();
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
            htselect.Add("@Trans", "GET_DOCUMENTS_FOR_INHOSE");
            htselect.Add("@Order_Id", OrderId);
            dtselect = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Grid_Client_Upload.Columns[0].Width = 100;
                Grid_Client_Upload.Columns[1].Width = 200;
                Grid_Client_Upload.Columns[2].Width = 80;
                Grid_Client_Upload.Columns[3].Width = 80;
                Grid_Client_Upload.Columns[4].Width = 60;
                Grid_Client_Upload.Columns[5].Width = 60;
                Grid_Client_Upload.Columns[6].Width = 60;
                Grid_Client_Upload.Columns[7].Width = 60;

                Grid_Client_Upload.Columns[8].Width = 60;


                if (dtselect.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Client_Upload.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        Grid_Client_Upload.Rows.Add();
                        Grid_Client_Upload.Rows[i].Cells[0].Value = dtselect.Rows[i]["Document_Type"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[1].Value = dtselect.Rows[i]["Description"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[2].Value = dtselect.Rows[i]["File_Size"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[3].Value = dtselect.Rows[i]["Document_From"].ToString();
                        Grid_Client_Upload.Rows[i].Cells[4].Value = dtselect.Rows[i]["Inserted_Date"].ToString();

                        Grid_Client_Upload.Rows[i].Cells[5].Value = "View";
                        Grid_Client_Upload.Rows[i].Cells[6].Value = "Edit";
                        Grid_Client_Upload.Rows[i].Cells[7].Value = "Delete";
                        Grid_Client_Upload.Rows[i].Cells[8].Value = dtselect.Rows[i]["Order_Document_Id"].ToString();

                        Grid_Client_Upload.Rows[i].Cells[9].Value = dtselect.Rows[i]["Document_Path"].ToString();

                    }



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
            if (e.ColumnIndex == 5)
            {


                string Source_Path = Grid_Client_Upload.Rows[e.RowIndex].Cells[9].Value.ToString();
                if (Source_Path != "")
                {
                    string docName = Path.GetFileName(Source_Path.ToString());

                    System.IO.Directory.CreateDirectory(@"C:\temp");

                    File.Copy(Source_Path, @"C:\temp\" + docName, true);

                    System.Diagnostics.Process.Start(@"C:\temp\" + docName);
                }
                
            }
            else if (e.ColumnIndex == 7)
            {
                  dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                  if (dialogResult == DialogResult.Yes)
                  {
                      string DeletingType = Grid_Client_Upload.Rows[e.RowIndex].Cells[3].Value.ToString();
                      string Document_ID = Grid_Client_Upload.Rows[e.RowIndex].Cells[8].Value.ToString();
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
                string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No + @"\" + FName[FName.Length - 1];
                DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                de.Username = "administrator";
                de.Password = "password1$";
                string file = op1.FileName.ToString();
                System.IO.FileInfo f = new System.IO.FileInfo(file);
                double filesize = f.Length;
                GetFileSize(filesize);
                Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No);

                File.Copy(s, dest_path1, true);
                count++;
                htorderkb.Clear();
                dtorderkb.Clear();
                htorderkb.Add("@Trans", "INSERT");
                htorderkb.Add("@Instuction", txt_Inhouse_Instruction.Text);
                htorderkb.Add("@Document_Type",int.Parse(ddl_Inhouse_Doc_Type.SelectedValue.ToString()));
                htorderkb.Add("@Order_ID", OrderId);
                htorderkb.Add("@Document_Name", op1.SafeFileName);
                htorderkb.Add("@File_Size", File_size);

                htorderkb.Add("@Document_Path", dest_path1);
                htorderkb.Add("@Inserted_By", userid);
                htorderkb.Add("@Inserted_date", DateTime.Now);
                dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
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

                    File.Copy(Source_Path, @"C:\temp\"+FileName, true);

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
                    htheader.Add("@Sub_Client_Id",Sub_Process_Id);
                    htheader.Add("@Order_Type_Abrivation",Order_Type_Abbrivation);
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

                        string Header_FileName = "Final_Property_report"+Order_No+".docx";

                        string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No + @"\" + Header_FileName;
                        DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                        de.Username = "administrator";
                        de.Password = "password1$";

                        
                        Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No);

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
                        htdel.Add("@Trans","DELETE_BY_DOCUMENT_TYPE");
                        htdel.Add("@Order_ID",OrderId);
                        htdel.Add("@Document_Type",2);
                        dtdel = dataaccess.ExecuteSP("Sp_Document_Upload", htdel);

                        Hashtable htorderkb = new Hashtable();
                        System.Data.DataTable dtorderkb = new System.Data.DataTable();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Instuction","Typing Final Report");
                        htorderkb.Add("@Document_Type",2);
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
                    string path = grd_Vendor_Documents.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (path != "" || !string.IsNullOrEmpty(path))
                    {
                        Process.Start(path);
                    }
                    else
                    {

                        MessageBox.Show("Document Is not Upload");
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

          

            
            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();

            if (Tax_Order_Task == 21)
            {
                htselect.Add("@Trans", "GET_TAX_DOCUMENTS");
            }
            else if (Tax_Order_Task == 22)
            {
                htselect.Add("@Trans", "GET_INTERNAL_TAX_DOCUMENTS");

            }
            htselect.Add("@Order_Id", OrderId);
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Grid_Tax_Upload.Columns[0].Width = 100;
                Grid_Tax_Upload.Columns[1].Width = 50;
                Grid_Tax_Upload.Columns[2].Width = 100;
                Grid_Tax_Upload.Columns[3].Width = 80;
                Grid_Tax_Upload.Columns[4].Width = 60;
                Grid_Tax_Upload.Columns[5].Width = 60;
                Grid_Tax_Upload.Columns[6].Width = 60;
                Grid_Tax_Upload.Columns[7].Width = 60;



                if (dtselect.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Tax_Upload.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        Grid_Tax_Upload.Rows.Add();
                        Grid_Tax_Upload.Rows[i].Cells[0].Value = dtselect.Rows[i]["Instuction"].ToString();

                        
                      
                        Grid_Tax_Upload.Rows[i].Cells[1].Value = dtselect.Rows[i]["FileSize"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[2].Value = dtselect.Rows[i]["User_Name"].ToString();
                        Grid_Tax_Upload.Rows[i].Cells[3].Value = dtselect.Rows[i]["Inserted_date"].ToString();

                        Grid_Tax_Upload.Rows[i].Cells[4].Value = "View";

                        Grid_Tax_Upload.Rows[i].Cells[5].Value = "Delete";
                        Grid_Tax_Upload.Rows[i].Cells[6].Value = dtselect.Rows[i]["Tax_Document_Upload_Id"].ToString();

                        Grid_Tax_Upload.Rows[i].Cells[7].Value = dtselect.Rows[i]["Document_Path"].ToString();

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
                if (Source_Path != "")
                {
                    string docName = Path.GetFileName(Source_Path.ToString());

                    System.IO.Directory.CreateDirectory(@"C:\temp");

                    File.Copy(Source_Path, @"C:\temp\" + docName, true);

                    System.Diagnostics.Process.Start(@"C:\temp\" + docName);
                }

            }
        }

        private void Grd_Document_upload_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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

                   
                    
                      string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No + @"\" + FName[FName.Length - 1];
                     string Order_Document_Id=grd_Vendor_Documents.Rows[i].Cells[4].Value.ToString();
                      DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                      de.Username = "administrator";
                      de.Password = "password1$";

                  
                      Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No);
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
                          htorderkb.Add("@Instuction",  grd_Vendor_Documents.Rows[i].Cells[1].Value.ToString() + " - " +grd_Vendor_Documents.Rows[i].Cells[2].Value.ToString());
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
                          htupdate_iscopied.Add("@Order_Document_Id",grd_Vendor_Documents.Rows[i].Cells[9].Value.ToString() );
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

    }
}
    

    
