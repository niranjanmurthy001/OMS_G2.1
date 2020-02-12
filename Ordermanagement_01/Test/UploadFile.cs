using DevExpress.Xpf.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Ordermanagement_01.Test
{
    public partial class UploadFile : XtraForm
    {
        DataAccess da = new DataAccess();
        private string SystemFolder;
        private DialogResult dialogResult;

        string path;
        string DownloadedFilePath;
        private string FolderPath;

        public string File_size { get; private set; }

        public UploadFile()
        {
            InitializeComponent();
            foreach (int rows in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(rows);
            }
        }

        private void UploadFile_Load(object sender, EventArgs e)
        {
            BindGrid();
            this.DragDrop += gridControl1_DragDrop;
            // SystemFolder = "C:\\VijayKumar\\FileUpload";
            // path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "xxx.pdf");
            path = "C:\\VijayKumar\\FileUpload";
            Directory.CreateDirectory(path);
            DownloadedFilePath = "C:\\VijayKumar\\DownloadedFile";
            //Directory.CreateDirectory(DownloadedFilePath);
            ////this.DragEnter += gridControl1_DragEnter;
            //FolderPath =Path.GetPathRoot(DownloadedFilePath).ToUpper();
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            // Can only drop files, so check
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                ht.Clear();
                dt.Clear();
                string dest = path + "//" + Path.GetFileName(file);
                //string DownloadPath = DownloadedFilePath + "//" + Path.GetFileName(file);
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

                            ht.Add("@Trans", "INSERT");
                            ht.Add("@FileName", Path.GetFileName(file));
                            ht.Add("@FileSize", File_size);
                            ht.Add("@InsertedDate", DateTime.Now);
                            ht.Add("@DocumentPath", dest);
                            int Value = Convert.ToInt32(da.ExecuteSPForScalar("Sp_TblFileUploads", ht));
                            break;

                        case DragDropEffects.Move:
                            if (isFile)
                                File.Move(file, dest);
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@FileName", Path.GetFileName(file));
                            ht.Add("@File_Size", File_size);
                            ht.Add("@InsertedDate", DateTime.Now);
                            ht.Add("@DocumentPath", dest);
                            int dt2 = Convert.ToInt32(da.ExecuteSPForScalar("Sp_TblFileUploads", ht));

                            break;

                    }
                }
                catch (IOException ex)
                {
                    dialogResult = XtraMessageBox.Show("This File Is Alerday Exist,Do you want to Replace?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {


                        switch (e.Effect)
                        {

                            case DragDropEffects.Copy:
                                if (isFile)                 // TODO: Need to handle folders
                                    File.Copy(file, dest, true);
                                ht.Add("@Trans", "INSERT");
                                ht.Add("@FileName", Path.GetFileName(file));
                                ht.Add("@File_Size", File_size);
                                ht.Add("@InsertedDate", DateTime.Now);
                                ht.Add("@DocumentPath", dest);
                                int dt1 = Convert.ToInt32(da.ExecuteSPForScalar("Sp_TblFileUploads", ht));
                                break;


                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }

            }
            BindGrid();
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

        private void gridControl1_DragEnter_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;

                this.DragDrop += gridControl1_DragDrop;
                //XtraMessageBox.Show("Drag Enter Event Occured");
            }
            else
            {
                e.Effect = DragDropEffects.Move;

                this.DragDrop += gridControl1_DragDrop;


            }
        }
        public void BindGrid()
        {
            Hashtable ht1 = new Hashtable();
            DataTable dt1 = new DataTable();
            ht1.Add("@Trans", "Select");
            dt1 = da.ExecuteSP("Sp_TblFileUploads", ht1);
            if (dt1.Rows.Count > 0)
            {
                gridControl1.DataSource = dt1;
                gridControl1.ForceInitialize();
            }
            else
            {
                gridControl1.DataSource = null;
            }
        }
        private void gridControl1_DragOver(object sender, DragEventArgs e)
        {

            MessageBox.Show("Drag Over");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;


                //XtraMessageBox.Show("Drag Enter Event Occured");
            }
            else
            {
                e.Effect = DragDropEffects.Move;
                //int[] selectedRows = gridView1.GetSelectedRows();
                //foreach (int rowHandle in selectedRows)
                //{
                //    if (rowHandle >= 0)
                //    {
                //        var cellvalue = (string)gridView1.GetRowCellValue(rowHandle, "DocumentPath");
                //        if (cellvalue != null)
                //        {
                //            folderBrowserDialog1.ShowDialog();
                //            string tpath = folderBrowserDialog1.SelectedPath.ToString();
                //            for (int i = 0; i < cellvalue.Length; i++)
                //            {
                //                string[] filename = cellvalue[i].ToString().Split('/');
                //                string DocName =path.GetFileName(filename.ToString());
                //                File.Copy(filename[0], @"C:\\Vijaykumar\\DownlodedFiles" + DocName, true);
                //            }
                //        }

                //    }
                //    XtraMessageBox.Show("Drag Move Event Occured");
                //}
            }

        }

        //private void gridView1_DragObjectOver(object sender, DevExpress.XtraGrid.Views.Base.DragObjectOverEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        e.Effect = DragDropEffects.Copy;


        //        //XtraMessageBox.Show("Drag Enter Event Occured");
        //    }
        //    else
        //    {
        //        e.Effect = DragDropEffects.Move;
        //        int[] selectedRows = gridView1.GetSelectedRows();
        //        foreach (int rowHandle in selectedRows)
        //        {
        //            if (rowHandle >= 0)
        //            {
        //                var cellvalue = (string)gridView1.GetRowCellValue(rowHandle, "DocumentPath");
        //                if (cellvalue != null)
        //                {
        //                    folderBrowserDialog1.ShowDialog();
        //                    string tpath = folderBrowserDialog1.SelectedPath.ToString();
        //                    for (int i = 0; i < cellvalue.Length; i++)
        //                    {
        //                        string[] filename = cellvalue[i].ToString().Split('/');
        //                        string DocName = Path.GetFileName(filename.ToString());
        //                        File.Copy(filename[0], @"C:\\Vijaykumar\\DownlodedFiles" + DocName, true);
        //                    }
        //                }

        //            }
        //            XtraMessageBox.Show("Drag Move Event Occured");
        //        }
        //    }

        //}

        private void gridControl1_DragLeave(object sender, EventArgs e)
        {
            MessageBox.Show("Drag Leave");
            //int[] selectedRows = gridView1.GetSelectedRows();
            //foreach (int rowHandle in selectedRows)
            //{
            //    if (rowHandle >= 0)
            //    {
            //        var cellvalue = (string)gridView1.GetRowCellValue(rowHandle, "DocumentPath");
            //        if (cellvalue != null)
            //        {
            //            folderBrowserDialog1.ShowDialog();
            //            string tpath = folderBrowserDialog1.SelectedPath.ToString();
            //            for (int i = 0; i < cellvalue.Length; i++)
            //            {
            //                string[] filename = cellvalue[i].ToString().Split('/');
            //                string DocName = Path.GetFileName(filename.ToString());
            //                File.Copy(filename[0], @"C:\\Vijaykumar\\DownlodedFiles" + DocName, true);
            //            }
            //        }

            //    }
            //    XtraMessageBox.Show("Drag Move Event Occured");
            //}
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                string caption = info.Column.Caption;
                if (caption == "DocumentPath")
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right || e.Button == MouseButtons.Left)
                    {

                        DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                        gridControl1.DoDragDrop(row, DragDropEffects.Move);

                        // int Order_ID = int.Parse(row["Order_Id"].ToString());
                        // obj_Order_Details_List.Order_Id = Order_ID;
                        // obj_Order_Details_List.Work_Type_Id = Work_Type_Id;
                        // Comment_Card cmd = new Comment_Card(obj_Order_Details_List);
                        // cmd.Show();
                    }
                }


                //if (e.Button == System.Windows.Forms.MouseButtons.Right)
                //{

                //    GridView view = sender as GridView;
                //    GridHitInfo hi = view.CalcHitInfo(e.Location);
                //    DataRow dr = view.GetDataRow(hi.RowHandle);

                //    gridControl1.DoDragDrop(dr, DragDropEffects.Move);



                //}





            }
        }

        private void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs ea1 = e as DevExpress.Utils.DXMouseEventArgs;
            GridView view1 = (GridView)sender;
           // GridHitInfo hitinfo = view1.CalcHitInfo(new Point(e.X, e.Y));
        }
    }
}



