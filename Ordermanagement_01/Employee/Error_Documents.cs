using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.DirectoryServices;
using System.IO;
namespace Ordermanagement_01.Employee
{
    public partial class Error_Documents : Form
    {
        int Order_Id, User_Id, User_Role, Error_Info_Id;
        string Order_No;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string File_size;
        string[] FName;
        string extension;
        DialogResult dialogResult;
        public Error_Documents(int ORDER_ID,int USER_ROLE,int ERROR_INFO_ID,int USER_ID,string ORDER_NO)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            User_Role = USER_ROLE;
            Error_Info_Id = ERROR_INFO_ID;
            User_Id = USER_ID;
            Order_No = ORDER_NO;
            Lbl_Header.Text = ""+Order_No+" ERRORS UPLOADED DOCUMENTS";
            this.Text = "" + Order_No + " - ERRORS DOCS"; ;
        }

        private void Error_Documents_Load(object sender, EventArgs e)
        {
            Bind_Error_Documents();

        }

        private void Bind_Error_Documents()
        {

            Hashtable htupload = new Hashtable();
            DataTable dtupload = new System.Data.DataTable();


            htupload.Add("@Trans", "SELECT_BY_ORDER");
            htupload.Add("@Order_Id",Order_Id);
            htupload.Add("@Error_Info_Id", Error_Info_Id);
            dtupload = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htupload);

            if (dtupload.Rows.Count > 0)
            { 
                     Grid_Error_Doc_Upload.Rows.Clear();
                    for (int i = 0; i < dtupload.Rows.Count; i++)
                    {
                        Grid_Error_Doc_Upload.Rows.Add();
                     
                        Grid_Error_Doc_Upload.Rows[i].Cells[0].Value = dtupload.Rows[i]["Error_Doc_Description"].ToString();
                        Grid_Error_Doc_Upload.Rows[i].Cells[1].Value = dtupload.Rows[i]["User_Name"].ToString();
                        Grid_Error_Doc_Upload.Rows[i].Cells[2].Value = dtupload.Rows[i]["Uploaded_Date"].ToString();
                        Grid_Error_Doc_Upload.Rows[i].Cells[3].Value = "View";
                        Grid_Error_Doc_Upload.Rows[i].Cells[4].Value = "Delete";
                        Grid_Error_Doc_Upload.Rows[i].Cells[5].Value = dtupload.Rows[i]["Error_Doc_Path"].ToString();
                        Grid_Error_Doc_Upload.Rows[i].Cells[6].Value = dtupload.Rows[i]["Error_Doc_Upload_Id"].ToString();
                        Grid_Error_Doc_Upload.Rows[i].Cells[7].Value = dtupload.Rows[i]["Order_Id"].ToString();
                        Grid_Error_Doc_Upload.Rows[i].Cells[8].Value = dtupload.Rows[i]["Error_Doc_Upload_Id"].ToString();
                        Grid_Error_Doc_Upload.Rows[i].Cells[9].Value = dtupload.Rows[i]["User_Id"].ToString();

                    }



                }
                else
                {

                    Grid_Error_Doc_Upload.Rows.Clear();

                    Grid_Error_Doc_Upload.DataSource = null;

                }

            }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (Error_Info_Id != 0)
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

                    string dest_path1 = @"\\192.168.12.33\Oms_Error_Upload_Documents\" + Order_Id + @"\" + FName[FName.Length - 1];
                    DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                    de.Username = "administrator";
                    de.Password = "password1$";

                    Directory.CreateDirectory(@"\\192.168.12.33\Oms_Error_Upload_Documents\" + Order_Id );
                    extension = Path.GetExtension(Docname);
                    File.Copy(s, dest_path1, true);
                    count++;
                    htorderkb.Clear();
                    dtorderkb.Clear();
                    htorderkb.Add("@Trans", "INSERT");
               
                    htorderkb.Add("@Order_Id", Order_Id);
                    htorderkb.Add("@Error_Info_Id", Error_Info_Id);
                    htorderkb.Add("@Error_Doc_Description", txt_Error_Doc_Discription.Text);
                    htorderkb.Add("@Error_Doc_Path", dest_path1);
                    htorderkb.Add("@Document_File_Type",extension);
                    htorderkb.Add("@File_Size", File_size);
                    htorderkb.Add("@User_Id", User_Id);
                    htorderkb.Add("@Status", "True");
                    dtorderkb = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htorderkb);
                    Bind_Error_Documents();
                }
                MessageBox.Show(Convert.ToString(count) + " File(s) copied");
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

        private void Grid_Error_Doc_Upload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 3)
                {

                    string Source_Path = Grid_Error_Doc_Upload.Rows[e.RowIndex].Cells[5].Value.ToString();

                        if (Source_Path != "")
                        {
                            string docName = Path.GetFileName(Source_Path.ToString());

                            System.IO.Directory.CreateDirectory(@"C:\temp");

                            File.Copy(Source_Path, @"C:\temp\" + docName, true);

                            System.Diagnostics.Process.Start(@"C:\temp\" + docName);
                        }

                    

                }
                else if (e.ColumnIndex == 4)
                {
                       dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                       if (dialogResult == DialogResult.Yes)
                       {

                           Hashtable htdelete = new Hashtable();
                           DataTable dtdelete = new System.Data.DataTable();

                           string Error_Doc_up_id = Grid_Error_Doc_Upload.Rows[e.RowIndex].Cells[8].Value.ToString();
                           int User_UpId = int.Parse(Grid_Error_Doc_Upload.Rows[e.RowIndex].Cells[9].Value.ToString());
                           if (User_UpId == User_Id)
                           {
                               htdelete.Add("@Trans", "DELETE");
                               htdelete.Add("@Error_Doc_Upload_Id", Error_Doc_up_id);
                               dtdelete = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htdelete);
                               Bind_Error_Documents();
                               MessageBox.Show("Document Deleted Sucessfully");

                           }
                           else
                           {

                               MessageBox.Show("Document Can't be Deleted by you");
                           }
                       }
                       else if (dialogResult == DialogResult.No)
                       { 
                       

                       }




                }
            }
        }

        
    }
}
