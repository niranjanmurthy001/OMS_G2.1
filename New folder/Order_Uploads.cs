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

namespace Ordermanagement_01
{
    public partial class Order_Uploads : Form
    {
        int OrderId;
        int userid;
        string Order_No;
        string[] FName;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Client_Name;
        string Sub_Client;
        public Order_Uploads(int Order_id, int user_id,string OrderNo,string Client,string Subclient)
        {
            InitializeComponent();
            OrderId = Order_id;
            userid = user_id;
            Order_No = OrderNo;
            Client_Name = Client;
            Sub_Client = Subclient;
        }

        private void Order_Uploads_Load(object sender, EventArgs e)
        {
            Grd_Document_upload_Load();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void btn_upload_Click(object sender, EventArgs e)
        {
            Hashtable htorderkb = new Hashtable();
            DataTable dtorderkb = new System.Data.DataTable();
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = true;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.xls";
           // txt_path.Text = op1.FileName;
            int count = 0;
           
            foreach (string s in op1.FileNames)
            {
                FName = s.Split('\\');
                string dest_path1 = @"\\192.168.12.33\oms\" + Client_Name+@"\"+Sub_Client+@"\"+Order_No+@"\" + FName[FName.Length - 1];
                DirectoryEntry de = new DirectoryEntry(dest_path1, "webserver01","password1$");
                de.Username = "webserver01";
                de.Password = "password1$";
               
                Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_Name + @"\" + Sub_Client + @"\" + Order_No);
              
                File.Copy(s, dest_path1,true);
                count++;
                htorderkb.Clear();
                dtorderkb.Clear();
                htorderkb.Add("@Trans", "INSERT");
                htorderkb.Add("@Instuction", txt_path.Text);
                htorderkb.Add("@Order_ID", OrderId);
                htorderkb.Add("@Document_Name", op1.SafeFileName);
                //htorderkb.Add("@Chk_UploadPackage", chk_Upload.Checked);
                // htorderkb.Add("@Extension", extension);
                htorderkb.Add("@Document_Path", dest_path1);
                htorderkb.Add("@Inserted_By", userid);
                htorderkb.Add("@Inserted_date", DateTime.Now);
                dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);
                Grd_Document_upload_Load();
            }
            MessageBox.Show(Convert.ToString(count) + " File(s) copied");
        }
        protected void Grd_Document_upload_Load()
        {
             DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
           DataGridViewButtonColumn btn = new DataGridViewButtonColumn();  
             DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
             DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            Hashtable htDocument_Select = new Hashtable();
            DataTable dtDocument_Select = new System.Data.DataTable();
            htDocument_Select.Add("@Trans", "SELECT");
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

                Grd_Document_upload.Columns[3].Name = "username";
                Grd_Document_upload.Columns[3].HeaderText = "USER NAME";
                Grd_Document_upload.Columns[3].DataPropertyName = "User_Name";

                Grd_Document_upload.Columns[4].Name = "upload_id";
                Grd_Document_upload.Columns[4].HeaderText = "upload_id";
                Grd_Document_upload.Columns[4].DataPropertyName = "Document_Upload_Id";
                Grd_Document_upload.Columns[4].Visible = false;

                Grd_Document_upload.Columns.Add(chk);
                chk.HeaderText = "UPLOAD PACKAGE";
                chk.Name = "check";
                //bool ischecked=(bool).dtDocument_Select
                
                
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
                    Grd_Document_upload.Rows[j].Cells[7].Value = "View";
                    Grd_Document_upload.Rows[j].Cells[8].Value = "Edit";
                    Grd_Document_upload.Rows[j].Cells[9].Value = "Delete";
                    if (dtDocument_Select.Rows[i]["Document_Upload_Id"].ToString() == Grd_Document_upload.Rows[j].Cells[4].Value.ToString())
                    {
                        Grd_Document_upload.Columns[5].Visible = false;
                        bool ischecked=Convert.ToBoolean(dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString());
                        if (ischecked == true)
                        {
                            //chk.DataPropertyName=;
                           
                            Grd_Document_upload.Rows[j].Cells[6].Value = dtDocument_Select.Rows[i]["Chk_UploadPackage"].ToString();
                        }

                    }
                  }
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
            if (e.ColumnIndex == 6)
            {
                Hashtable htorderkbchk = new Hashtable();
                DataTable dtorderkbchk = new System.Data.DataTable();
                htorderkbchk.Add("@Trans", "CHKUPDATE");
                htorderkbchk.Add("@Order_ID", OrderId);
                dtorderkbchk = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkbchk);

                Hashtable htorderkbup = new Hashtable();
                DataTable dtorderkbup = new System.Data.DataTable();
                htorderkbup.Add("@Trans", "UPDATE_CHK");
                htorderkbup.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[4].Value);
                dtorderkbup = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkbup);
                Grd_Document_upload_Load();
            }
            if (e.ColumnIndex == 7)
            {
                FName = Grd_Document_upload.Rows[e.RowIndex].Cells[2].Value.ToString().Split('\\');
                //OpenFileDialog op1 = new OpenFileDialog();
                //string loc_path = @"C:\Temp\" + Order_No + @"\" + FName[FName.Length - 1];
                //Directory.CreateDirectory(@"C:\Temp\" + Order_No);
                string Source_Path=Grd_Document_upload.Rows[e.RowIndex].Cells[1].Value.ToString();
               // Directory.CreateDirectory(Source_Path);
                System.Diagnostics.Process.Start(Source_Path);
                //File.Copy(Source_Path, loc_path,true);
                //op1.InitialDirectory = @"C:\Temp\" + Order_No;
                //op1.ShowDialog();
            }
            if (e.ColumnIndex == 9)
            {
                Hashtable htdelete = new Hashtable();
                DataTable dtdelete = new System.Data.DataTable();
                htdelete.Add("@Trans", "DELETE");
                htdelete.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[4].Value);
                dtdelete = dataaccess.ExecuteSP("Sp_Document_Upload", htdelete);
                Grd_Document_upload_Load();
            }
            if (e.ColumnIndex == 8)
            {
                Hashtable htEdit = new Hashtable();
                DataTable dtEdit= new System.Data.DataTable();
                htEdit.Add("@Trans", "UPDATE");
                htEdit.Add("@Document_Upload_Id", Grd_Document_upload.Rows[e.RowIndex].Cells[4].Value);
                htEdit.Add("@Instuction", Grd_Document_upload.Rows[e.RowIndex].Cells[0].Value);
                dtEdit = dataaccess.ExecuteSP("Sp_Document_Upload", htEdit);
                Grd_Document_upload_Load();
            }
        }

        private void txt_path_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
