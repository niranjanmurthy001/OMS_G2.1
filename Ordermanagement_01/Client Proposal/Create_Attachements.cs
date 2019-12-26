using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.DirectoryServices;
using System.Diagnostics;
using System.IO;

namespace Ordermanagement_01.Client_Proposal
{
    public partial class Create_Attachements : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, Proposal_attachid; string File_size, file_ext, filename;
        string[]  FName,destname;
        Hashtable ht = new Hashtable();
        DataTable dt = new System.Data.DataTable();

        DataTable dtsearch = new DataTable();
        public Create_Attachements(int userid)
        {
            InitializeComponent();
            Userid = userid;
        }

        private void Create_Attachements_Load(object sender, EventArgs e)
        {
            Bind_Attachement_Grid();

            dbc.Bind_Proposal_From(ddl_Proposal_From);
        }
        private void Bind_Attachement_Grid()
        {
            ht.Clear();
            ht.Add("@Trans", "SELECT_ATTACH");
            dtsearch = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
            if (dtsearch.Rows.Count > 0)
            {
                grd_Attach_file.Rows.Clear();
                for (int i = 0; i < dtsearch.Rows.Count; i++)
                {
                    grd_Attach_file.Rows.Add();
                    grd_Attach_file.Rows[i].Cells[0].Value = i + 1;
                    grd_Attach_file.Rows[i].Cells[1].Value = dtsearch.Rows[i]["Proposal_From"].ToString();
                    grd_Attach_file.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Attachment_Name"].ToString();
                    grd_Attach_file.Rows[i].Cells[3].Value = dtsearch.Rows[i]["File_Name"].ToString();
                    grd_Attach_file.Rows[i].Cells[4].Value = dtsearch.Rows[i]["File_Size"].ToString();
                    if (dtsearch.Rows[i]["Status"].ToString() == "True")
                    {
                        grd_Attach_file[5, i].Value = true;
                    }
                    else
                    {
                        grd_Attach_file[5, i].Value = false;
                    }
                    grd_Attach_file.Rows[i].Cells[6].Value = "View";
                    grd_Attach_file.Rows[i].Cells[7].Value = "Edit";
                    grd_Attach_file.Rows[i].Cells[8].Value = "Delete";
                    grd_Attach_file.Rows[i].Cells[9].Value = dtsearch.Rows[i]["Proposal_Attachment_Id"].ToString();
                    grd_Attach_file.Rows[i].Cells[10].Value = dtsearch.Rows[i]["File_Path"].ToString();

                }
            }
            
        }
        private bool Validation()
        {
            if (ddl_Proposal_From.SelectedIndex <= 0)
            {

                MessageBox.Show("kindly Select the Proposal From");
                return false;
            }

            if (txt_Attachment_name.Text == "")
            {
                MessageBox.Show("Kindly Enter Attachement name");
                return false;
            }
            else if (txt_Filename.Text == "")
            {
                MessageBox.Show("Kindly Enter File name");
                return false;
            }
            return true;
        }
        private bool Validate_File_name()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_ATTACH");
            dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["File_Name"].ToString() == txt_Filename.Text)
                    {
                        MessageBox.Show("Attachement File Name Already Exists");
                        return false;
                    }
                }
            }
            return true;

        }
       

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            txt_Filename.Text = "";
            txt_Attachment_name.Text = "";
            txt_Attachment_name.ReadOnly = false;
            ddl_Proposal_From.SelectedIndex = 0;
            Bind_Attachement_Grid();
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


        private void btn_Upld_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in open.FileNames)
                {

                    FName = s.Split('\\');
                    txt_Filename.ReadOnly = true;

                    txt_Filename.Text = open.SafeFileName.ToString();
                    filename = open.FileName.ToString();
                   
                    System.IO.FileInfo f = new System.IO.FileInfo(filename);
                    double filesize = f.Length;
                    GetFileSize(filesize);
                    file_ext = f.Extension;
                    if (Proposal_attachid != 0 && Validation() != false && Validate_File_name() != false)
                    {
                        //Update
                        string destpath = @"\\192.168.12.33\Titlelogy_Documents\Client_Proposal_Attachements\" + txt_Filename.Text.ToString();
                        
                        File.Copy(s, destpath, false);

                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "UPDATE_ATTACH");
                        ht.Add("@Proposal_Attachment_Id", Proposal_attachid);
                        ht.Add("@Proposal_From_Id", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                        ht.Add("@Attachment_Name", txt_Attachment_name.Text);
                        ht.Add("@File_Name", txt_Filename.Text);
                        ht.Add("@File_Extension", file_ext);
                        ht.Add("@File_Path", destpath);
                        ht.Add("@File_Size", File_size);
                        ht.Add("@Modified_by", Userid);
                        dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                        MessageBox.Show("Record Updated Successfully");
                        clear();
                    }
                    else if (Validation() != false && Validate_File_name() != false)
                    {
                        try
                        {
                            string destpath = @"\\192.168.12.33\Titlelogy_Documents\Client_Proposal_Attachements\" + txt_Filename.Text.ToString();
                           
                            File.Copy(s, destpath, false);

                            //insert
                            ht.Clear(); dt.Clear();
                            ht.Add("@Trans", "INSERT_ATTACH");
                            ht.Add("@Attachment_Name", txt_Attachment_name.Text);
                            ht.Add("@Proposal_From_Id", int.Parse(ddl_Proposal_From.SelectedValue.ToString()));
                            ht.Add("@File_Name", txt_Filename.Text);
                            ht.Add("@File_Extension", file_ext);
                            ht.Add("@File_Size", File_size);
                            ht.Add("@File_Path", destpath);
                            ht.Add("@Inserted_by", Userid);
                            dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                            MessageBox.Show("Record Inserted Successfully");
                            clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                }

                }
            }
        }

        private void txt_Search_File_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_File.Text != "" && txt_Search_File.Text != "Search File Name...")
            {
                DataView dts = new DataView(dtsearch);
                dts.RowFilter = "Attachment_Name like '%" + txt_Search_File.Text.ToString() + "%' or File_Name like '%" + txt_Search_File.Text.ToString() +
                    "%' or File_Size like '%" + txt_Search_File.Text.ToString() + "%'";

                dt = dts.ToTable();
                if (dt.Rows.Count > 0)
                {
                    grd_Attach_file.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Attach_file.Rows.Add();
                        grd_Attach_file.Rows[i].Cells[0].Value = i + 1;
                        grd_Attach_file.Rows[i].Cells[1].Value = dt.Rows[i]["Proposal_From"].ToString();
                        grd_Attach_file.Rows[i].Cells[2].Value = dt.Rows[i]["Attachment_Name"].ToString();
                        grd_Attach_file.Rows[i].Cells[3].Value = dt.Rows[i]["File_Name"].ToString();
                        grd_Attach_file.Rows[i].Cells[4].Value = dt.Rows[i]["File_Size"].ToString();
                        if (dtsearch.Rows[i]["Status"].ToString() == "True")
                        {
                            grd_Attach_file[5, i].Value = true;
                        }
                        else
                        {
                            grd_Attach_file[5, i].Value = false;
                        }
                        grd_Attach_file.Rows[i].Cells[6].Value = "View";
                        grd_Attach_file.Rows[i].Cells[7].Value = "Edit";
                        grd_Attach_file.Rows[i].Cells[8].Value = "Delete";
                        grd_Attach_file.Rows[i].Cells[9].Value = dt.Rows[i]["Proposal_Attachment_Id"].ToString();
                        grd_Attach_file.Rows[i].Cells[10].Value = dt.Rows[i]["File_Path"].ToString();
                        
                    }
                }
            }
            else
            {
                Bind_Attachement_Grid();
            }
        }

        private void grd_Attach_file_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                FName = grd_Attach_file.Rows[e.RowIndex].Cells[3].Value.ToString().Split('\\');
                string Source_Path = grd_Attach_file.Rows[e.RowIndex].Cells[10].Value.ToString();
                System.IO.Directory.CreateDirectory(@"C:\temp");

                File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
                Hashtable htUpload = new Hashtable();
                System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);
            }
            else if (e.ColumnIndex == 7)
            {
                Proposal_attachid = int.Parse(grd_Attach_file.Rows[e.RowIndex].Cells[9].Value.ToString());
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "SELECT_ID_ATTACH");
                ht.Add("@Proposal_Attachment_Id", Proposal_attachid);
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                if (dt.Rows.Count > 0)
                {
                    txt_Attachment_name.Text = dt.Rows[0]["Attachment_Name"].ToString();
                    txt_Filename.Text = dt.Rows[0]["File_Name"].ToString();
                    ddl_Proposal_From.SelectedValue = dt.Rows[0]["Proposal_From_Id"].ToString();
                    txt_Filename.ReadOnly = true;
                }
                
            }
            else if (e.ColumnIndex == 8)
            {
                Proposal_attachid = int.Parse(grd_Attach_file.Rows[e.RowIndex].Cells[9].Value.ToString());
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "DELETE_ATTACH");
                ht.Add("@Proposal_Attachment_Id", Proposal_attachid);
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                MessageBox.Show("Record Deleted Successfully");
                clear();
            }
        }

        private void txt_Search_File_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_File.Text == "Search File Name...")
            {
                txt_Search_File.Text = "";
            }
        }

        private void grd_Attach_file_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                bool ischeck = (bool)grd_Attach_file[4, e.RowIndex].FormattedValue;
                if (ischeck == true)
                {
                    //Update status false
                    ht.Clear(); dt.Clear();
                    ht.Add("@Trans", "UPDATE_FALSE_ATTACH");
                    ht.Add("@Proposal_Attachment_Id", int.Parse(grd_Attach_file.Rows[e.RowIndex].Cells[9].Value.ToString()));
                    dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                    MessageBox.Show("Record Updated Successfully");
                    Bind_Attachement_Grid();
                }
                else if (ischeck == false)
                {
                    //Update status true
                    ht.Clear(); dt.Clear();
                    ht.Add("@Trans", "UPDATE_TRUE_ATTACH");
                    ht.Add("@Proposal_Attachment_Id", int.Parse(grd_Attach_file.Rows[e.RowIndex].Cells[9].Value.ToString()));
                    dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                    MessageBox.Show("Record Updated Successfully");
                    Bind_Attachement_Grid();
                }
            }
        }

        private void lbl_Branch_Click(object sender, EventArgs e)
        {

        }
    }
}
