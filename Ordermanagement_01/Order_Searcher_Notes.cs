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

namespace Ordermanagement_01
{
    public partial class Order_Searcher_Notes : Form
    {
        DataAccess dataaccess = new DataAccess();
        int User_Id, Order_Searcherid = 0,Order_Taskid=0,Inserted_User_Id=0;
        string user_roleid, OrderId, Client, Subclient, Orderno, File_size, Order_Task, User_Name, file_extension = "";
        string src, des, src_qc, des_qc;
        double filesize;
        public Order_Searcher_Notes(int userid,string User_roleid,string orderid,string clientname,string subclient,string orderno,string order_task)
        {
            InitializeComponent();
            User_Id = userid;
            user_roleid = User_roleid;
            OrderId = orderid;
            Client = clientname;
            Subclient = subclient;
            Orderno = orderno;
            Order_Task = order_task;
        }
        private string LoadUserInfo()
        {
            string username = "";
            Hashtable ht=new Hashtable();
            DataTable dt=new DataTable();
            ht.Add("@Trans","SELECT_USERNAME");
            ht.Add("@Inserted_by",User_Id );
            dt=dataaccess.ExecuteSP("Sp_Order_Searcher_Notes",ht);
            if(dt.Rows.Count>0)
            {
                username = dt.Rows[0]["User_Name"].ToString();
            }
            return username;
        }
        private void Order_Searcher_Notes_Load(object sender, EventArgs e)
        {
            Load_OrderInfo();
            User_Name = LoadUserInfo();
        }
        private void Load_OrderInfo()
        {
            Hashtable htget = new Hashtable();
            DataTable dtget = new DataTable();
            htget.Add("@Trans", "GET_ORDER_TABLE");
            htget.Add("@OrderId", OrderId);
            dtget = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htget);
            if (dtget.Rows.Count > 0)
            {
                txt_OrderNo.Text = dtget.Rows[0]["Client_Order_Number"].ToString();
                txt_BorrowerName.Text = dtget.Rows[0]["Borrower_Name"].ToString();
                txt_Apn.Text = dtget.Rows[0]["APN"].ToString();
                txt_Address.Text = dtget.Rows[0]["Address"].ToString();
                txt_County.Text = dtget.Rows[0]["County"].ToString();
                txt_State.Text = dtget.Rows[0]["Abbreviation"].ToString() + " - " + dtget.Rows[0]["State"].ToString();

            }
            htget.Clear();dtget.Clear();
            htget.Add("@Trans","GET_ORDER_INFO");
            htget.Add("@OrderId",OrderId);
            htget.Add("@Order_Task_Id", int.Parse(Order_Task.ToString())); 
            dtget = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htget);
            if (dtget.Rows.Count > 0)
            {
                txt_Names_Run.Text = dtget.Rows[0]["Names_Run"].ToString();
                txt_Effective_date.Text = dtget.Rows[0]["S_Effective_Date"].ToString();
                txt_LegalRef.Text = dtget.Rows[0]["Legal_Reference"].ToString();
                txt_DataDepth.Text = dtget.Rows[0]["Data_Depth"].ToString();
                txt_Deeds.Text = dtget.Rows[0]["Deeds"].ToString();
                txt_Mortgages.Text = dtget.Rows[0]["Mortgages"].ToString();
                txt_JudgmentLies.Text = dtget.Rows[0]["Judgments_Liens"].ToString();
                txt_Comments.Text = dtget.Rows[0]["General_Comments"].ToString();
                txt_Closed_Items.Text = dtget.Rows[0]["Closed_Items"].ToString();
                txt_client_Instructions.Text = dtget.Rows[0]["Client_Instruction"].ToString();
                txt_Additional_Documents.Text = dtget.Rows[0]["Additional_Documents"].ToString();

                if (dtget.Rows[0]["Order_Searcher_Notes_Id"].ToString() != "" && dtget.Rows[0]["Order_Searcher_Notes_Id"].ToString() != null)
                {
                    Order_Searcherid = int.Parse(dtget.Rows[0]["Order_Searcher_Notes_Id"].ToString());
                }
                else
                {
                    Order_Searcherid = 0;
                }
                if (dtget.Rows[0]["Order_Task_Id"].ToString() != "" && dtget.Rows[0]["Order_Task_Id"].ToString() != null)
                {
                    Order_Taskid = int.Parse(dtget.Rows[0]["Order_Task_Id"].ToString());
                }
                else
                {
                    Order_Taskid = 0;
                }
                if (dtget.Rows[0]["Modified_by"].ToString() == "" )
                {
                    Inserted_User_Id = int.Parse(dtget.Rows[0]["Inserted_by"].ToString());
                }
                else
                {
                    Inserted_User_Id = int.Parse(dtget.Rows[0]["Modified_by"].ToString());
                }
                
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
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_Apn.Text != "" && txt_Apn.Text.Length > 0)
            {

                StringBuilder bs = new StringBuilder();
                bs.AppendLine("ORDER NO #      :" + " " + txt_OrderNo.Text.ToString());
                bs.AppendLine("ADDRESS		   :" + " " + txt_Address.Text.ToString());
                bs.AppendLine("COUNTY		   :" + " " + txt_County.Text.ToString());
                bs.AppendLine("APN		       :" + " " + txt_Apn.Text.ToString());
                bs.AppendLine("BORROWER NAME   :" + " " + txt_BorrowerName.Text.ToString());
                bs.AppendLine("EFFECTIVE DATE  :" + " " + txt_Effective_date.Text.ToString());
                bs.AppendLine(Environment.NewLine);
                bs.AppendLine("Names Run	   :" + "" + Environment.NewLine);
                bs.AppendLine(txt_Names_Run.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Legal Reference :" + " " + txt_LegalRef.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Data Depth      :" + " " + txt_DataDepth.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Open Items:" + "" + Environment.NewLine);
                bs.AppendLine("Deeds:   " + "" + Environment.NewLine);
                bs.AppendLine(txt_Deeds.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Mortgages:   " + "" + Environment.NewLine);
                bs.AppendLine(txt_Mortgages.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Judgments/Liens: " + "" + Environment.NewLine);
                bs.AppendLine(txt_JudgmentLies.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Additional documents : " + "" + Environment.NewLine);
                bs.AppendLine(txt_Additional_Documents.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Closed Items:" + "" + Environment.NewLine);
                bs.AppendLine(txt_Closed_Items.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("General Comments:" + "" + Environment.NewLine);
                bs.AppendLine(txt_Comments.Text.ToString() + "" + Environment.NewLine);
                bs.AppendLine("Client instructions/requirements:" + "" + Environment.NewLine);
                bs.AppendLine(txt_client_Instructions.Text.ToString() + "" + Environment.NewLine);
                if (Directory.Exists(@"C:\OMS_Notes"))
                {
                    src = @"C:\OMS_Notes\Searcher Notes-" + User_Name.ToString() + ".txt";
                    des = @"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + OrderId + @"\Searcher Notes-" + User_Name.ToString() + ".txt";

                    src_qc = @"C:\OMS_Notes\Searcher QC Notes-" + User_Name.ToString() + ".txt";
                    des_qc = @"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + OrderId + @"\Searcher QC Notes-" + User_Name.ToString() + ".txt";


                }
                else
                {
                    //strisrc1=@"C:\OMS_Notes";
                    Directory.CreateDirectory(@"C:\OMS_Notes"); //Directory.CreateDirectory(src_qc);
                    src = @"C:\OMS_Notes\Searcher Notes-" + User_Name.ToString() + ".txt";
                    des = @"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + OrderId + @"\Searcher Notes-" + User_Name.ToString() + ".txt";

                    src_qc = @"C:\OMS_Notes\Searcher QC Notes-" + User_Name.ToString() + ".txt";
                    des_qc = @"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + OrderId + @"\Searcher QC Notes-" + User_Name.ToString() + ".txt";

                }



                Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + OrderId);
                DirectoryEntry de = new DirectoryEntry(@"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + OrderId, "administrator", "password1$");
                de.Username = "administrator";
                de.Password = "password1$";

                if (Order_Task == "2")
                {

                    FileStream fs = new FileStream(src, FileMode.Append, FileAccess.Write, FileShare.Write);
                    fs.Flush();
                    fs.Close();
                    File.WriteAllText(src, bs.ToString());
                    File.Copy(src, des, true);
                    System.IO.FileInfo f = new System.IO.FileInfo(src);
                    System.IO.FileInfo f1 = new System.IO.FileInfo(des);
                    filesize = f.Length;
                    file_extension = f.Extension;
                }
                else if (Order_Task == "3")
                {

                    FileStream fs = new FileStream(src_qc, FileMode.Append, FileAccess.Write, FileShare.Write);
                    fs.Flush();
                    fs.Close();
                    File.WriteAllText(src_qc, bs.ToString());
                    File.Copy(src_qc, des_qc, true);
                    System.IO.FileInfo f = new System.IO.FileInfo(src_qc);
                    System.IO.FileInfo f1 = new System.IO.FileInfo(des_qc);
                    filesize = f.Length;
                    file_extension = f.Extension;
                }



                GetFileSize(filesize);


                if (Order_Searcherid != 0 && Order_Taskid == int.Parse(Order_Task.ToString()) && Inserted_User_Id != 0)
                {

                    Hashtable htup = new Hashtable();
                    DataTable dtup = new DataTable();

                    htup.Add("@Trans", "CHECK_EXIST_DOCUMENT");
                    htup.Add("@OrderId", OrderId);
                    if (Order_Task == "2")
                    {
                        htup.Add("@Document_Name", "Searcher Notes");
                    }
                    else if (Order_Task == "3")
                    {
                        htup.Add("@Document_Name", "Search_QC Notes");
                    }
                    dtup = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htup);
                    if (dtup.Rows.Count > 0)
                    {
                        //UPDATE_DOCUMENT_PATH
                        htup.Clear(); dtup.Clear();
                        htup.Add("@Trans", "UPDATE_DOCUMENT_PATH");
                        if (Order_Task == "2")
                        {
                            htup.Add("@Document_Path", des);
                        }
                        else if (Order_Task == "3")
                        {
                            htup.Add("@Document_Path", des_qc);
                        }
                        htup.Add("@OrderId", OrderId);
                        dtup = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htup);

                    }
                    else
                    {
                        //INSERT
                        htup.Clear(); dtup.Clear();
                        htup.Add("@Trans", "INSERT");

                        htup.Add("@Order_ID", int.Parse(OrderId.ToString()));
                        htup.Add("@File_Size", File_size);
                        if (Order_Task == "2")
                        {
                            htup.Add("@Instuction", "Searcher Notes");
                            htup.Add("@Document_Path", des);
                            htup.Add("@Document_Name", "Searcher Notes");
                        }
                        else if (Order_Task == "3")
                        {
                            htup.Add("@Instuction", "Search_QC Notes");
                            htup.Add("@Document_Path", des_qc);
                            htup.Add("@Document_Name", "Search_QC Notes");
                        }
                        htup.Add("@Extension", file_extension);

                        htup.Add("@Inserted_By", User_Id);
                        htup.Add("@Inserted_date", DateTime.Now);
                        dtup = dataaccess.ExecuteSP("Sp_Document_Upload", htup);
                    }
                    //update the order search notes record
                    htup.Clear(); dtup.Clear();
                    htup.Add("@Trans", "UPDATE_NOTES");
                    htup.Add("@Order_Searcher_Notes_Id", Order_Searcherid);
                    htup.Add("@Names_Run", txt_Names_Run.Text);
                    htup.Add("@Effective_Date", txt_Effective_date.Text);
                   
                    htup.Add("@Legal_Reference", txt_LegalRef.Text);
                    htup.Add("@Data_Depth", txt_DataDepth.Text);
                    htup.Add("@Deeds", txt_Deeds.Text);
                    htup.Add("@Mortgages", txt_Mortgages.Text);
                    htup.Add("@Judgments_Liens", txt_JudgmentLies.Text);
                    htup.Add("@Closed_Items", txt_Closed_Items.Text);
                    htup.Add("@General_Comments", txt_Comments.Text);
                    htup.Add("@Client_Instruction", txt_client_Instructions.Text);
                    htup.Add("@Additional_Documents", txt_Additional_Documents.Text);
                    htup.Add("@Modified_by", User_Id);
                    //htup.Add("@OrderId", OrderId); 
                    dtup = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htup);
                    MessageBox.Show("Record Updated Successfully");
                }
                else if (Order_Taskid != int.Parse(Order_Task.ToString()))
                {
                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();

                    htin.Add("@Trans", "CHECK_EXIST_DOCUMENT");
                    htin.Add("@OrderId", OrderId);
                    if (Order_Task == "2")
                    {
                        htin.Add("@Document_Name", "Searcher Notes");
                    }
                    else if (Order_Task == "3")
                    {
                        htin.Add("@Document_Name", "Search_QC Notes");
                    }
                    dtin = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htin);
                    if (dtin.Rows.Count > 0)
                    {
                        //UPDATE_DOCUMENT_PATH
                        htin.Clear(); dtin.Clear();
                        htin.Add("@Trans", "UPDATE_DOCUMENT_PATH");
                        if (Order_Task == "2")
                        {
                            htin.Add("@Document_Path", des);
                        }
                        else if (Order_Task == "3")
                        {
                            htin.Add("@Document_Path", des_qc);
                        }
                        htin.Add("@OrderId", OrderId);
                        dtin = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htin);

                    }
                    else
                    {
                        //INSERT
                        htin.Clear(); dtin.Clear();
                        htin.Add("@Trans", "INSERT");

                        htin.Add("@Order_ID", int.Parse(OrderId.ToString()));
                        htin.Add("@File_Size", File_size);
                        if (Order_Task == "2")
                        {
                            htin.Add("@Instuction", "Searcher Notes");
                            htin.Add("@Document_Path", des);
                            htin.Add("@Document_Name", "Searcher Notes");
                        }
                        else if (Order_Task == "3")
                        {
                            htin.Add("@Instuction", "Search_QC Notes");
                            htin.Add("@Document_Path", des_qc);
                            htin.Add("@Document_Name", "Search_QC Notes");
                        }
                        htin.Add("@Extension", file_extension);

                        htin.Add("@Inserted_By", User_Id);
                        htin.Add("@Inserted_date", DateTime.Now);
                        dtin = dataaccess.ExecuteSP("Sp_Document_Upload", htin);
                    }

                    htin.Clear(); htin.Clear();

                    //insert

                    htin.Add("@Trans", "INSERT_NOTES");
                    htin.Add("@Names_Run", txt_Names_Run.Text);
                    htin.Add("@Effective_Date", txt_Effective_date.Text);
                    htin.Add("@Legal_Reference", txt_LegalRef.Text);
                    htin.Add("@Data_Depth", txt_DataDepth.Text);
                    htin.Add("@Deeds", txt_Deeds.Text);
                    htin.Add("@Mortgages", txt_Mortgages.Text);
                    htin.Add("@Judgments_Liens", txt_JudgmentLies.Text);
                    htin.Add("@General_Comments", txt_Comments.Text);
                    htin.Add("@Closed_Items", txt_Closed_Items.Text);
                    htin.Add("@Inserted_by", User_Id);
                    htin.Add("@OrderId", OrderId.ToString());
                    htin.Add("@Order_Task_Id", int.Parse(Order_Task.ToString()));
                    htin.Add("@Client_Instruction", txt_client_Instructions.Text);
                    htin.Add("@Additional_Documents", txt_Additional_Documents.Text);
                    dtin = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htin);


                    //Update APN

                   
                    MessageBox.Show("Record Inserted Successfully");
                }
                Hashtable htapn = new Hashtable();
                DataTable dtapn = new DataTable();

                htapn.Add("@Trans", "UPDATE_APN");
                htapn.Add("@APN", txt_Apn.Text);
                htapn.Add("@Order_ID", OrderId);
                dtapn = dataaccess.ExecuteSP("Sp_Order", htapn);

            }
            else
            {

                MessageBox.Show("Please Enter APN Number");
                txt_Apn.Focus();


            }
        }

        private void clear()
        {
            txt_Deeds.Text = ""; txt_Mortgages.Text = ""; txt_JudgmentLies.Text = ""; txt_Comments.Text = ""; txt_Names_Run.Text = ""; txt_LegalRef.Text = ""; txt_DataDepth.Text = "";
            Order_Searcherid = 0;
            txt_Closed_Items.Text = "";
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_Deeds_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == '\t' || e.KeyChar == (char)13)
            //    e.Handled = true;

            
        }

        private void txt_Deeds_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;

            }
        }

        private void txt_Mortgages_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;

            }
        }

        private void txt_JudgmentLies_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;

            }
        }

        private void txt_Closed_Items_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;

            }
        }

        private void txt_Comments_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;

            }
        }

        private void txt_Additional_Documents_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;

            }
        }

        

        private void txt_Effective_date_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txt_Names_Run.Focus();
            }
        }

        private void txt_DataDepth_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txt_Deeds.Focus();
            }
        }
    }
}
