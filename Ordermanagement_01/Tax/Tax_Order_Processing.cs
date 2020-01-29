using System;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.IO;
using System.DirectoryServices;
using System.Net;
using System.Collections.Generic;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Order_Processing : Form
    {
        string Order_Id, Order_TaskId, Tax_Task_Id, Tax_Status_Id, User_Id, Order_Number, User_Role;

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.TaxClass taxcls = new Classes.TaxClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        InfiniteProgressBar.clsProgress clsLoader = new InfiniteProgressBar.clsProgress();
        int DateCustom = 0, Follow_Up_Custom = 0;
        string Tax_Content_Path, Tax_Header_Path;
        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }
        string Error_Status;
        int Order_Type_Id;
        int Client_Id;
        int Order_Time_Id;
        int Order_Task_Inhouse;
        int Sub_Client_Id;
        int clientId;

        private string directoryPath;
        private string year;
        private string month;
        private string Ftp_Domain_Name;
        private string Ftp_User_Name;
        private string Ftp_Password;
        private string Ftp_Path;
        private string mainPath;
        private string ftpfullpath;
        private string Upload_Directory;


        public Tax_Order_Processing(string ORDER_ID, string ORDER_TASK_ID, string TAX_TASK_ID, string TAX_STSTAUS_ID, string USER_ID, string ORDER_NUMBER, string USER_ROLE, int ORDER_TIME_ID)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            Order_TaskId = ORDER_TASK_ID;
            Tax_Task_Id = TAX_TASK_ID;
            Tax_Status_Id = TAX_STSTAUS_ID;
            User_Id = USER_ID;
            Order_Number = ORDER_NUMBER;
            User_Role = USER_ROLE;
            Order_Time_Id = ORDER_TIME_ID;

            this.Text = "Order Number:" + Order_Number + " - Processing";

            if (Order_Type_Id == 110)
            {


                btn_Tax_ViolationEntry.Visible = true;
                btn_Tax_Details_Note_Pad.Visible = false;

            }
            else
            {
                btn_Tax_ViolationEntry.Visible = false;
                btn_Tax_Details_Note_Pad.Visible = true;
            }
            lbl_Header.Text = this.Text;
        }


        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            System.Data.DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "GET_TAX_ORDER_COMMENTS");
            htComments.Add("@Order_Id", Order_Id);

            dtComments = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htComments);
            Grid_Comments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            Grid_Comments.EnableHeadersVisualStyles = false;
            Grid_Comments.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            Grid_Comments.Columns[0].Width = 50;
            Grid_Comments.Columns[2].Width = 400;
            Grid_Comments.Columns[3].Width = 130;
            if (dtComments.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Comments.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    Grid_Comments.Rows.Add();
                    Grid_Comments.Rows[i].Cells[0].Value = i + 1;
                    Grid_Comments.Rows[i].Cells[1].Value = dtComments.Rows[i]["Tax_Order_Production_Id"].ToString();
                    Grid_Comments.Rows[i].Cells[2].Value = dtComments.Rows[i]["Comment"].ToString();
                    Grid_Comments.Rows[i].Cells[3].Value = dtComments.Rows[i]["User_Name"].ToString();
                }
            }
            else
            {
            }
        }



        private void Tax_Order_Processing_Load(object sender, EventArgs e)
        {
            this.ResizeRedraw = true;
            taxcls.BindTax_Status(ddl_order_Staus);
            taxcls.BindTax_Order_Source(ddl_Order_Source);
            Bind_Order_Details();
            Geydview_Bind_Comments();
            DateCustom = 1;

            txt_Prdoductiondate.Value = DateTime.Now;
            txt_followup_Date.Value = DateTime.Now;
            this.WindowState = FormWindowState.Maximized;

            if (Tax_Task_Id == "1")
            {

                // pnl_Error.Visible = false;
                lbl_Error_Status.Visible = false;
                chk_Yes.Visible = false;
                chk_No.Visible = false;
                txt_Error_Description.Visible = false;
                lbl_Error_Notes.Visible = false;

            }
            else
            {
                //   pnl_Error.Visible = true;
                lbl_Error_Status.Visible = true;
                chk_Yes.Visible = true;
                chk_No.Visible = true;
                txt_Error_Description.Visible = true;
                lbl_Error_Notes.Visible = true;

            }

            var dt = dbc.Get_Month_Year();
            if (dt != null && dt.Rows.Count > 0)
            {
                year = dt.Rows[0]["Year"].ToString();
                month = dt.Rows[0]["Month"].ToString();
            }
            var ht = new Hashtable();
            ht.Add("@Trans", "GET_CLIENT_DETAILS");
            ht.Add("@Order_Id", Order_Id);
            var dt_client = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", ht);
            if (dt_client != null && dt_client.Rows.Count > 0)
            {
                clientId = Convert.ToInt32(dt_client.Rows[0]["Client_Id"].ToString());
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
            directoryPath = year + "/" + month + "/" + clientId + "/" + Order_Id + "";
            CreateDirectory(directoryPath);
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

        private void Bind_Order_Details()
        {
            BindOrderSourceDetails();
            Hashtable htorderdetail = new Hashtable();
            System.Data.DataTable dtorderdetail = new System.Data.DataTable();
            htorderdetail.Add("@Trans", "GET_ORDER_DETAILS");
            htorderdetail.Add("@Order_Id", Order_Id);
            dtorderdetail = dataaccess.ExecuteSP("Sp_Tax_Orders", htorderdetail);
            if (dtorderdetail.Rows.Count > 0)
            {

                txt_Order_Number.Text = dtorderdetail.Rows[0]["Client_Order_Number"].ToString();
                txt_Order_Type.Text = dtorderdetail.Rows[0]["Order_Type"].ToString();

                txt_Barrower_Name.Text = dtorderdetail.Rows[0]["Borrower_Name"].ToString();
                txt_Property_Address.Text = dtorderdetail.Rows[0]["Address"].ToString();
                txt_APN.Text = dtorderdetail.Rows[0]["APN"].ToString();
                txt_State.Text = dtorderdetail.Rows[0]["State"].ToString();
                txt_ReceivedDate.Text = dtorderdetail.Rows[0]["Recieved_Date"].ToString();
                txt_County.Text = dtorderdetail.Rows[0]["County"].ToString();
                txt_Notes.Text = dtorderdetail.Rows[0]["Notes"].ToString();
                txtClientOrderRef.Text = dtorderdetail.Rows[0]["Client_Order_Ref"].ToString();
                txtTargetCategory.Text = dtorderdetail.Rows[0]["Target Category"].ToString();
                txt_Assigned_Date.Text = dtorderdetail.Rows[0]["Assigned_Date"].ToString();
                Client_Id = int.Parse(dtorderdetail.Rows[0]["Client_Id"].ToString());
                if (Client_Id == 36)
                {
                    label17.Visible = true;
                    checkBoxOrderSubmitted.Visible = true;
                    if (!string.IsNullOrEmpty(dtorderdetail.Rows[0]["Submitted_To_Portal"].ToString()))
                    {
                        checkBoxOrderSubmitted.Checked = Convert.ToBoolean(dtorderdetail.Rows[0]["Submitted_To_Portal"].ToString());
                    }
                    else
                    {
                        checkBoxOrderSubmitted.Checked = false;
                    }
                }
                Sub_Client_Id = int.Parse(dtorderdetail.Rows[0]["Sub_ProcessId"].ToString());
                Order_Type_Id = int.Parse(dtorderdetail.Rows[0]["Order_Type_ID"].ToString());
                txt_City.Text = dtorderdetail.Rows[0]["City"].ToString();
                txt_Zipcode.Text = dtorderdetail.Rows[0]["Zip"].ToString();

                Order_Task_Inhouse = int.Parse(dtorderdetail.Rows[0]["Order_Status"].ToString());// This is Current Order Task.
            }

        }

        private void BindOrderSourceDetails()
        {
            var ht = new Hashtable();
            ht.Add("@Trans", "GET_ORDER_SOURCE_DETAILS");
            ht.Add("@Order_Id", Order_Id);
            var dt = new DataAccess().ExecuteSP("Sp_Tax_Order_Status", ht);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["Tax_Order_Source_Id"].ToString() != "")
                {
                    ddl_Order_Source.SelectedValue = Convert.ToInt32(dt.Rows[0]["Tax_Order_Source_Id"].ToString());
                    if (Convert.ToInt32(ddl_Order_Source.SelectedValue) == 4)
                    {
                        txtOrder_Source_Details.Enabled = true;
                        txtOrder_Source_Details.Text = dt.Rows[0]["Tax_Order_Source_Details"].ToString();
                    }
                }


                if (dt.Rows[0]["Delq_Status"].ToString() != "")
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Delq_Status"].ToString()))
                    {
                        checkBoxDelq_Status_Yes.Checked = true;
                    }
                    else
                    {


                        checkBoxDelq_Status_No.Checked = true;
                    }

                }

            }
        }

        private void txt_Prdoductiondate_ValueChanged(object sender, EventArgs e)
        {
            if (DateCustom != 0)
            {
                txt_Prdoductiondate.CustomFormat = "MM/dd/yyyy";
            }
            DateCustom = 1;
        }

        private void Download_Ftp_File(string p, string Source_Path)
        {
            try
            {
                FtpWebRequest reqFTP;
                FileStream outputStream = new FileStream("C:\\OMS\\Temp" + "\\" + p, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(Source_Path));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(Ftp_User_Name, Ftp_Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.CopyTo(outputStream);
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            //load_Progressbar.Start_progres();

            try
            {
                if (Validate() != false && validateStatus() != false && validateOrderSource() != false && validateDelqStatus() != false)
                {

                    var HtUpdateOrderSource = new Hashtable();
                    bool DelqStatus = false;
                    if (checkBoxDelq_Status_Yes.Checked == true)
                    {
                        DelqStatus = true;
                    }
                    if (checkBoxDelq_Status_No.Checked == true)
                    {
                        DelqStatus = false;
                    }

                    HtUpdateOrderSource.Add("@Trans", "UPDATE_TAX_ORDER_SOURCE");
                    HtUpdateOrderSource.Add("@Tax_Order_Source_Id", Convert.ToInt32(ddl_Order_Source.SelectedValue.ToString()));
                    HtUpdateOrderSource.Add("@Tax_Order_Source_Details", txtOrder_Source_Details.Text);
                    HtUpdateOrderSource.Add("@Delq_Status", DelqStatus);
                    HtUpdateOrderSource.Add("@Order_Id", Order_Id);
                    var dt = dataaccess.ExecuteSP("Sp_Tax_Order_Status", HtUpdateOrderSource);

                    if (Client_Id == 36)
                    {
                        if (checkBoxOrderSubmitted.Checked)
                        {
                            Hashtable htUpdatePortal = new Hashtable(){
                                {"@Trans","UPDATE_PORTAL_SUBMISSION"},
                                {"@Submitted_To_Portal",checkBoxOrderSubmitted.Checked},
                                {"@Order_Id",Order_Id }
                            };
                            var dtPortal = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htUpdatePortal);
                        }
                    }


                    Hashtable htupdateTaxTask = new Hashtable();
                    System.Data.DataTable dtupdateTask = new System.Data.DataTable();
                    Hashtable htupdateTaxStatus = new Hashtable();
                    System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();


                    Hashtable htupdateTaxfollowupdate = new Hashtable();
                    System.Data.DataTable dtupdateTaxfollowupdate = new System.Data.DataTable();

                    Hashtable htupdateOrderTaxStatus = new Hashtable();
                    System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();

                    Hashtable htorderassign = new Hashtable();
                    System.Data.DataTable dtorderassign = new System.Data.DataTable();
                    Hashtable htinsert = new Hashtable();
                    System.Data.DataTable dtinert = new System.Data.DataTable();




                    int Tax_StatusId = int.Parse(ddl_order_Staus.SelectedValue.ToString());

                    if (Tax_Task_Id == "1")
                    {

                        //Tax Order Qc Completes
                        if (Tax_StatusId == 1 && Validate_Tax_Details() != false && Validate_Tax_Document_Upload() != false && ValidateTax_Violation_Entry() != false)
                        {
                            htupdateTaxTask.Add("@Trans", "UPDATE_TAX_TASK");
                            htupdateTaxTask.Add("@Tax_Task", 2);
                            htupdateTaxTask.Add("@Modified_By", User_Id);
                            htupdateTaxTask.Add("@Order_Id", Order_Id);
                            dtupdateTask = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxTask);


                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                            htupdateTaxStatus.Add("@Tax_Status", 6);
                            htupdateTaxStatus.Add("@Modified_By", User_Id);
                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);


                            htupdateOrderTaxStatus.Add("@Trans", "UPDATE_ORDER_TAX_STATUS");
                            htupdateOrderTaxStatus.Add("@Order_Status", 14);
                            htupdateOrderTaxStatus.Add("@Modified_By", User_Id);
                            htupdateOrderTaxStatus.Add("@Order_Id", Order_Id);
                            dtupdateOrderTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateOrderTaxStatus);


                            //Updaet Tbl_Order_Assign

                            htorderassign.Add("@Trans", "DELET_BY_ORDER");
                            htorderassign.Add("@Order_Id", Order_Id);
                            dtorderassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htorderassign);

                            htinsert.Add("@Trans", "INSERT");
                            htinsert.Add("@Order_Id", Order_Id);
                            htinsert.Add("@Tax_Task_Id", Tax_Task_Id);
                            htinsert.Add("@Tax_Status_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                            htinsert.Add("@User_Id", User_Id);
                            htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                            htinsert.Add("@Comments", txt_Comments.Text);
                            htinsert.Add("@Inserted_By", User_Id);
                            htinsert.Add("@Status", "True");
                            dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);

                            // Updating the Insternal Tax Request Order Staus 

                            if (Order_Task_Inhouse == 26)
                            {



                                Hashtable htupdate1 = new Hashtable();
                                System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                                htupdate1.Add("@Order_ID", Order_Id);
                                htupdate1.Add("@Order_Progress", 6);// Open Status
                                dtupdate1 = dataaccess.ExecuteSP("Sp_Order", htupdate1);

                            }

                            // Updating User Order Timings

                            Hashtable ht_Update_Time = new Hashtable();
                            System.Data.DataTable dt_Update_Time = new System.Data.DataTable();
                            ht_Update_Time.Add("@Trans", "UPDATE");
                            ht_Update_Time.Add("@Order_Time_Id", Order_Time_Id);
                            ht_Update_Time.Add("@Tax_Task", Tax_Task_Id);
                            ht_Update_Time.Add("@Tax_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                            dt_Update_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Update_Time);

                            //Update Order Source and Delq status


                            MessageBox.Show("Order Submitted Sucessfully");
                            this.Close();

                        }

                        else if (Tax_StatusId != 1 && validateStatus() != false)
                        {


                            // clsLoader.startProgress();
                            htupdateTaxTask.Add("@Trans", "UPDATE_TAX_TASK");
                            htupdateTaxTask.Add("@Tax_Task", 1);
                            htupdateTaxTask.Add("@Modified_By", User_Id);
                            htupdateTaxTask.Add("@Order_Id", Order_Id);
                            dtupdateTask = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxTask);


                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                            htupdateTaxStatus.Add("@Tax_Status", Tax_StatusId);
                            htupdateTaxStatus.Add("@Modified_By", User_Id);
                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);


                            htupdateTaxfollowupdate.Add("@Trans", "UPDATE_TAX_FOLLOWUP_DATE");
                            htupdateTaxfollowupdate.Add("@Order_Id", Order_Id);
                            htupdateTaxfollowupdate.Add("@Followup_Date", txt_followup_Date.Text);
                            dtupdateTaxfollowupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxfollowupdate);


                            htupdateOrderTaxStatus.Add("@Trans", "UPDATE_ORDER_TAX_STATUS");
                            htupdateOrderTaxStatus.Add("@Order_Status", 17);
                            htupdateOrderTaxStatus.Add("@Modified_By", User_Id);
                            htupdateOrderTaxStatus.Add("@Order_Id", Order_Id);
                            dtupdateOrderTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateOrderTaxStatus);

                            // Updating the Insternal Tax Request Order Staus 

                            if (Order_Task_Inhouse == 26)
                            {

                                Hashtable htupdate1 = new Hashtable();
                                System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                                htupdate1.Add("@Order_ID", Order_Id);
                                htupdate1.Add("@Order_Progress", 17);
                                dtupdate1 = dataaccess.ExecuteSP("Sp_Order", htupdate1);

                            }

                            htinsert.Add("@Trans", "INSERT");
                            htinsert.Add("@Order_Id", Order_Id);
                            htinsert.Add("@Tax_Task_Id", Tax_Task_Id);
                            htinsert.Add("@Tax_Status_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                            htinsert.Add("@User_Id", User_Id);
                            htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                            htinsert.Add("@Comments", txt_Comments.Text);
                            htinsert.Add("@Inserted_By", User_Id);
                            htinsert.Add("@Status", "True");
                            dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);


                            // Updating User Order Timings

                            Hashtable ht_Update_Time = new Hashtable();
                            System.Data.DataTable dt_Update_Time = new System.Data.DataTable();
                            ht_Update_Time.Add("@Trans", "UPDATE");
                            ht_Update_Time.Add("@Order_Time_Id", Order_Time_Id);
                            ht_Update_Time.Add("@Tax_Task", Tax_Task_Id);
                            ht_Update_Time.Add("@Tax_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                            dt_Update_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Update_Time);





                            MessageBox.Show("Order Submitted Sucessfully");
                            this.Close();

                        }


                    }

                    else if (Tax_Task_Id == "2")
                    {

                        if (Tax_StatusId == 1 && Validate_Tax_Document_Upload_In_Qc() != false && Validate_Tax_Details_Qc() != false && Validate_Error_Status() != false && ValidateTax_Violation_Entry() != false)
                        {

                            //  This is for External Orders // for Converting word file and merging heder and Content word and converting into pdf
                            if (Order_TaskId == "21")
                            {

                                if (Client_Id == 36)
                                {
                                    if (chk_Yes.Checked == true)
                                    {

                                        Error_Status = "True";
                                    }
                                    else if (chk_No.Checked == true)
                                    {
                                        Error_Status = "false";

                                    }

                                    Hashtable hterror = new Hashtable();
                                    System.Data.DataTable dterror = new System.Data.DataTable();
                                    hterror.Add("@Trans", "INSERT");
                                    hterror.Add("@Order_Id", Order_Id);
                                    hterror.Add("@Tax_Task", 2);
                                    hterror.Add("@User_Id", User_Id);
                                    hterror.Add("@Error_Status", Error_Status);
                                    hterror.Add("@Error_Note", txt_Error_Description.Text.ToString());
                                    dterror = dataaccess.ExecuteSP("Sp_Tax_Order_Error_Details", hterror);







                                    htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                    htupdateTaxStatus.Add("@Tax_Status", Tax_StatusId);
                                    htupdateTaxStatus.Add("@Modified_By", User_Id);
                                    htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                    dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);



                                    htupdateOrderTaxStatus.Add("@Trans", "UPDATE_ORDER_TAX_STATUS");
                                    htupdateOrderTaxStatus.Add("@Order_Status", 3);
                                    htupdateOrderTaxStatus.Add("@Modified_By", User_Id);
                                    htupdateOrderTaxStatus.Add("@Order_Id", Order_Id);
                                    dtupdateOrderTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateOrderTaxStatus);
                                    //Updaet Tbl_Order_Assign

                                    htorderassign.Add("@Trans", "DELET_BY_ORDER");
                                    htorderassign.Add("@Order_Id", Order_Id);
                                    dtorderassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htorderassign);

                                    htinsert.Add("@Trans", "INSERT");
                                    htinsert.Add("@Order_Id", Order_Id);
                                    htinsert.Add("@Tax_Task_Id", Tax_Task_Id);
                                    htinsert.Add("@Tax_Status_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                    htinsert.Add("@User_Id", User_Id);
                                    htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                                    htinsert.Add("@Comments", txt_Comments.Text);
                                    htinsert.Add("@Inserted_By", User_Id);
                                    htinsert.Add("@Status", "True");
                                    dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);


                                    clsLoader.stopProgress();
                                    MessageBox.Show("Order Submitted Sucessfully");

                                    this.Close();


                                }
                                else
                                {
                                    Hashtable ht = new Hashtable();
                                    // System.Data.DataTable dt = new System.Data.DataTable();
                                    ht.Add("@Trans", "GET_TAX_DOCUMENT_UPLOADED");
                                    ht.Add("@Order_Id", Order_Id);
                                    dt = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", ht);
                                    if (dt.Rows.Count > 0 && Client_Id != 36)
                                    {
                                        Tax_Content_Path = dt.Rows[0]["New_Document_Path"].ToString();
                                        string Extension = Path.GetExtension(Tax_Content_Path);
                                        if (Extension == ".doc" || Extension == ".docx")
                                        {

                                            string dirTemp = "C:\\OMS\\Temp";
                                            if (!Directory.Exists(dirTemp))
                                            {
                                                var dirInfo = Directory.CreateDirectory(dirTemp);
                                            }
                                            string sourceFileName = Order_Number + "-Source.docx";
                                            string fileSource = dirTemp + "\\" + sourceFileName;

                                            Download_Ftp_File(sourceFileName, Tax_Content_Path);

                                            object oMissing = System.Reflection.Missing.Value;
                                            var wordApp = new Word.Application();
                                            var contentSource = wordApp.Documents.Open(fileSource);
                                            contentSource.ActiveWindow.Selection.WholeStory();
                                            contentSource.ActiveWindow.Selection.Copy();
                                            contentSource.Close();

                                            string destFileName = Order_Number + "-Dest.docx";
                                            string fileDest = dirTemp + "\\" + destFileName;

                                            string sourcePathHeader = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/Tax_Header_Document.docx";
                                            Download_Ftp_File(destFileName, sourcePathHeader);                                                      

                                            var tempDoc = wordApp.Documents.Open(fileDest);
                                            tempDoc.ActiveWindow.Selection.WholeStory();
                                            tempDoc.ActiveWindow.Selection.PasteAndFormat(WdRecoveryType.wdUseDestinationStylesRecovery);
                                            tempDoc.SaveAs(fileDest);
                                            tempDoc.Save();
                                            tempDoc.Close();

                                            Marshal.ReleaseComObject(wordApp);
                                            Marshal.ReleaseComObject(contentSource);
                                            Marshal.ReleaseComObject(tempDoc);
                                            GC.Collect();

                                            Stream fileStream = new FileStream(dirTemp + "\\" + destFileName, FileMode.Open);
                                            string File_Name = "Tax_Header_" + Order_Number + ".docx";
                                            ftpfullpath = "ftp://" + Ftp_Domain_Name + "/Ftp_Application_Files/OMS/Inhouse_Tax_Documents/" + directoryPath + "";
                                            string ftpUploadFullPath = "" + ftpfullpath + "/" + File_Name + "";

                                            FtpWebRequest reqUploadCheckfile = (FtpWebRequest)WebRequest.Create(new Uri(ftpfullpath));
                                            reqUploadCheckfile.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                            reqUploadCheckfile.Method = WebRequestMethods.Ftp.ListDirectory;
                                            StreamReader reader = new StreamReader(reqUploadCheckfile.GetResponse().GetResponseStream());

                                            HashSet<string> directories = new HashSet<string>(); // create list to store directories.   
                                            string line = reader.ReadLine();
                                            while (!string.IsNullOrEmpty(line))
                                            {
                                                directories.Add(line); // Add Each Directory to the List.  
                                                line = reader.ReadLine();
                                            }
                                            if (directories.Contains(File_Name))
                                            {
                                                FtpWebRequest delRequest = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                                                delRequest.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                                delRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                                                FtpWebResponse responseDelete = (FtpWebResponse)delRequest.GetResponse();
                                                if (responseDelete.StatusCode == FtpStatusCode.FileActionOK)
                                                {
                                                    FtpWebRequest uploadReq = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                                                    uploadReq.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                                    uploadReq.Method = WebRequestMethods.Ftp.UploadFile;
                                                    Stream ftpUploadStream = uploadReq.GetRequestStream();
                                                    fileStream.CopyTo(ftpUploadStream);
                                                    fileStream.Close();
                                                    ftpUploadStream.Close();
                                                }
                                            }
                                            else
                                            {
                                                FtpWebRequest uploadReq = (FtpWebRequest)WebRequest.Create(new Uri(ftpUploadFullPath));
                                                uploadReq.Credentials = new NetworkCredential(@"" + Ftp_User_Name + "", Ftp_Password);
                                                uploadReq.Method = WebRequestMethods.Ftp.UploadFile;
                                                Stream ftpUploadStream = uploadReq.GetRequestStream();
                                                fileStream.CopyTo(ftpUploadStream);
                                                fileStream.Close();
                                                ftpUploadStream.Close();
                                            }

                                            string F_Extension = Path.GetExtension(ftpUploadFullPath);
                                            Hashtable htorderkb = new Hashtable();
                                            System.Data.DataTable dtorderkb = new System.Data.DataTable();
                                            htorderkb.Add("@Trans", "INSERT");
                                            htorderkb.Add("@Order_Id", Order_Id);
                                            htorderkb.Add("@Instuction", "Qc Final Tax Certificate");
                                            htorderkb.Add("@Document_Path", ftpUploadFullPath);
                                            htorderkb.Add("@File_Extension", F_Extension);
                                            htorderkb.Add("@Tax_Task", 2);
                                            htorderkb.Add("@Inserted_By", User_Id);
                                            htorderkb.Add("@status", "True");
                                            dtorderkb = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htorderkb);

                                            Hashtable htemailstatus = new Hashtable();
                                            System.Data.DataTable dtemailstatus = new System.Data.DataTable();

                                            htemailstatus.Add("@Trans", "UPDATE_TAX_EMAIL_STATUS");
                                            htemailstatus.Add("@Order_Id", Order_Id);
                                            dtemailstatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htemailstatus);

                                            if (chk_Yes.Checked == true)
                                            {
                                                Error_Status = "True";
                                            }
                                            else if (chk_No.Checked == true)
                                            {
                                                Error_Status = "false";
                                            }

                                            Hashtable hterror = new Hashtable();
                                            System.Data.DataTable dterror = new System.Data.DataTable();
                                            hterror.Add("@Trans", "INSERT");
                                            hterror.Add("@Order_Id", Order_Id);
                                            hterror.Add("@Tax_Task", 2);
                                            hterror.Add("@User_Id", User_Id);
                                            hterror.Add("@Error_Status", Error_Status);
                                            hterror.Add("@Error_Note", txt_Error_Description.Text.ToString());
                                            dterror = dataaccess.ExecuteSP("Sp_Tax_Order_Error_Details", hterror);

                                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                            htupdateTaxStatus.Add("@Tax_Status", Tax_StatusId);
                                            htupdateTaxStatus.Add("@Modified_By", User_Id);
                                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                            htupdateOrderTaxStatus.Add("@Trans", "UPDATE_ORDER_TAX_STATUS");
                                            htupdateOrderTaxStatus.Add("@Order_Status", 3);
                                            htupdateOrderTaxStatus.Add("@Modified_By", User_Id);
                                            htupdateOrderTaxStatus.Add("@Order_Id", Order_Id);
                                            dtupdateOrderTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateOrderTaxStatus);
                                            //Updaet Tbl_Order_Assign

                                            htorderassign.Add("@Trans", "DELET_BY_ORDER");
                                            htorderassign.Add("@Order_Id", Order_Id);
                                            dtorderassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htorderassign);

                                            htinsert.Add("@Trans", "INSERT");
                                            htinsert.Add("@Order_Id", Order_Id);
                                            htinsert.Add("@Tax_Task_Id", Tax_Task_Id);
                                            htinsert.Add("@Tax_Status_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                            htinsert.Add("@User_Id", User_Id);
                                            htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                                            htinsert.Add("@Comments", txt_Comments.Text);
                                            htinsert.Add("@Inserted_By", User_Id);
                                            htinsert.Add("@Status", "True");
                                            dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);

                                            clsLoader.stopProgress();
                                            MessageBox.Show("Order Submitted Sucessfully");
                                            Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please Upload Word Format Tax Certificate");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Tax Document is not Uploaded please check it");
                                    }
                                }
                            }
                            else if (Order_TaskId == "22" || Order_Task_Inhouse == 26)
                            {


                                if (chk_Yes.Checked == true)
                                {

                                    Error_Status = "True";
                                }
                                else if (chk_No.Checked == true)
                                {
                                    Error_Status = "false";

                                }

                                Hashtable hterror = new Hashtable();
                                System.Data.DataTable dterror = new System.Data.DataTable();
                                hterror.Add("@Trans", "INSERT");
                                hterror.Add("@Order_Id", Order_Id);
                                hterror.Add("@Tax_Task", 2);
                                hterror.Add("@User_Id", User_Id);
                                hterror.Add("@Error_Status", Error_Status);
                                hterror.Add("@Error_Note", txt_Error_Description.Text.ToString());
                                dterror = dataaccess.ExecuteSP("Sp_Tax_Order_Error_Details", hterror);

                                htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                htupdateTaxStatus.Add("@Tax_Status", Tax_StatusId);
                                htupdateTaxStatus.Add("@Modified_By", User_Id);
                                htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);



                                htupdateOrderTaxStatus.Add("@Trans", "UPDATE_ORDER_TAX_STATUS");
                                htupdateOrderTaxStatus.Add("@Order_Status", 3);
                                htupdateOrderTaxStatus.Add("@Modified_By", User_Id);
                                htupdateOrderTaxStatus.Add("@Order_Id", Order_Id);
                                dtupdateOrderTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateOrderTaxStatus);
                                //Updaet Tbl_Order_Assign

                                htorderassign.Add("@Trans", "DELET_BY_ORDER");
                                htorderassign.Add("@Order_Id", Order_Id);
                                dtorderassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htorderassign);

                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Order_Id", Order_Id);
                                htinsert.Add("@Tax_Task_Id", Tax_Task_Id);
                                htinsert.Add("@Tax_Status_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                htinsert.Add("@User_Id", User_Id);
                                htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                                htinsert.Add("@Comments", txt_Comments.Text);
                                htinsert.Add("@Inserted_By", User_Id);
                                htinsert.Add("@Status", "True");
                                dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);

                                // Updating the Insternal Tax Request Updating Order to search Allocation Queue

                                if (Order_Task_Inhouse == 26)
                                {
                                    Hashtable htupdate1 = new Hashtable();
                                    System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                    htupdate1.Add("@Trans", "UPDATE_STATUS");
                                    htupdate1.Add("@Order_ID", Order_Id);
                                    htupdate1.Add("@Order_Status", 2);// Search Task
                                    dtupdate1 = dataaccess.ExecuteSP("Sp_Order", htupdate1);


                                    Hashtable htupdate2 = new Hashtable();
                                    System.Data.DataTable dtupdate2 = new System.Data.DataTable();
                                    htupdate2.Add("@Trans", "UPDATE_PROGRESS");
                                    htupdate2.Add("@Order_ID", Order_Id);
                                    htupdate2.Add("@Order_Progress", 8);// Not Assign Status
                                    dtupdate2 = dataaccess.ExecuteSP("Sp_Order", htupdate2);



                                    //OrderHistory
                                    Hashtable ht_Order_History1 = new Hashtable();
                                    System.Data.DataTable dt_Order_History1 = new System.Data.DataTable();
                                    ht_Order_History1.Add("@Trans", "INSERT");
                                    ht_Order_History1.Add("@Order_Id", Order_Id);
                                    ht_Order_History1.Add("@User_Id", User_Id);
                                    ht_Order_History1.Add("@Status_Id", 26);
                                    ht_Order_History1.Add("@Progress_Id", 8);
                                    ht_Order_History1.Add("@Work_Type", 1);
                                    ht_Order_History1.Add("@Assigned_By", User_Id);
                                    ht_Order_History1.Add("@Modification_Type", "Order Completed From Tax Qc level and Moved to Seach Allocation");
                                    dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                }

                                // Updating User Order Timings

                                Hashtable ht_Update_Time = new Hashtable();
                                System.Data.DataTable dt_Update_Time = new System.Data.DataTable();
                                ht_Update_Time.Add("@Trans", "UPDATE");
                                ht_Update_Time.Add("@Order_Time_Id", Order_Time_Id);
                                ht_Update_Time.Add("@Tax_Task", Tax_Task_Id);
                                ht_Update_Time.Add("@Tax_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                dt_Update_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Update_Time);






                                MessageBox.Show("Order Submitted Sucessfully");
                                this.Close();


                            }
                            else if (Order_TaskId != "21")
                            {

                                //This is for internal Order Assign



                                //Update Tbl_Order Search Tax Status once Qc Completes

                                Hashtable htorder_taxStatus = new Hashtable();
                                System.Data.DataTable dtorder_taxstatus = new System.Data.DataTable();

                                htorder_taxStatus.Add("@Trans", "UPDATE_ORDER_TAX_SERACH_REQUEST_STATUS");
                                htorder_taxStatus.Add("@Order_Id", Order_Id);
                                dtorder_taxstatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htorder_taxStatus);


                                //Update tbl_orders Search_request_Internal_Status



                                Hashtable htorder_taxinternalStatus = new Hashtable();
                                System.Data.DataTable dtorder_taxinternalstatus = new System.Data.DataTable();

                                htorder_taxinternalStatus.Add("@Trans", "UPDATE_INTERNAL_TAX_STATUS");
                                htorder_taxinternalStatus.Add("@Order_Id", Order_Id);
                                htorder_taxinternalStatus.Add("@Search_Tax_Req_Inhouse_Status", 8);
                                dtorder_taxinternalstatus = dataaccess.ExecuteSP("Sp_Order", htorder_taxinternalStatus);



                                //Tax Order Status


                                htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                htupdateTaxStatus.Add("@Tax_Status", Tax_StatusId);
                                htupdateTaxStatus.Add("@Modified_By", User_Id);
                                htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);






                                htupdateOrderTaxStatus.Add("@Trans", "UPDATE_ORDER_TAX_STATUS");
                                htupdateOrderTaxStatus.Add("@Order_Status", 3);
                                htupdateOrderTaxStatus.Add("@Modified_By", User_Id);
                                htupdateOrderTaxStatus.Add("@Order_Id", Order_Id);
                                dtupdateOrderTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateOrderTaxStatus);
                                //Updaet Tbl_Order_Assign

                                htorderassign.Add("@Trans", "DELET_BY_ORDER");
                                htorderassign.Add("@Order_Id", Order_Id);
                                dtorderassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htorderassign);

                                //// Updating the Insternal Tax Request Order Staus 

                                //if (Order_TaskId == "26")
                                //{

                                //    Hashtable htupdate1 = new Hashtable();
                                //    System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                //    htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                                //    htupdate1.Add("@Order_ID", Order_Id);
                                //    htupdate1.Add("@Order_Progress", 17);
                                //    dtupdate1 = dataaccess.ExecuteSP("Sp_Order", htupdate1);

                                //}



                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Order_Id", Order_Id);
                                htinsert.Add("@Tax_Task_Id", Tax_Task_Id);
                                htinsert.Add("@Tax_Status_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                htinsert.Add("@User_Id", User_Id);
                                htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                                htinsert.Add("@Comments", txt_Comments.Text);
                                htinsert.Add("@Inserted_By", User_Id);
                                htinsert.Add("@Status", "True");
                                dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);

                                // Updating User Order Timings

                                Hashtable ht_Update_Time = new Hashtable();
                                System.Data.DataTable dt_Update_Time = new System.Data.DataTable();
                                ht_Update_Time.Add("@Trans", "UPDATE");
                                ht_Update_Time.Add("@Order_Time_Id", Order_Time_Id);
                                ht_Update_Time.Add("@Tax_Task", Tax_Task_Id);
                                ht_Update_Time.Add("@Tax_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                dt_Update_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Update_Time);






                                MessageBox.Show("Order Submitted Sucessfully");
                                this.Close();
                            }


                        }


                        else if (Tax_StatusId != 1 && validateStatus() != false)
                        {


                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                            htupdateTaxStatus.Add("@Tax_Status", Tax_StatusId);
                            htupdateTaxStatus.Add("@Modified_By", User_Id);
                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);


                            htupdateOrderTaxStatus.Add("@Trans", "UPDATE_ORDER_TAX_STATUS");
                            htupdateOrderTaxStatus.Add("@Order_Status", 17);
                            htupdateOrderTaxStatus.Add("@Modified_By", User_Id);
                            htupdateOrderTaxStatus.Add("@Order_Id", Order_Id);
                            dtupdateOrderTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateOrderTaxStatus);

                            // Updating the Insternal Tax Request Order Staus 

                            if (Order_Task_Inhouse == 26)
                            {

                                Hashtable htupdate1 = new Hashtable();
                                System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                                htupdate1.Add("@Order_ID", Order_Id);
                                htupdate1.Add("@Order_Progress", 17);
                                dtupdate1 = dataaccess.ExecuteSP("Sp_Order", htupdate1);

                            }



                            // Updating User Order Timings

                            Hashtable ht_Update_Time = new Hashtable();
                            System.Data.DataTable dt_Update_Time = new System.Data.DataTable();
                            ht_Update_Time.Add("@Trans", "UPDATE");
                            ht_Update_Time.Add("@Order_Time_Id", Order_Time_Id);
                            ht_Update_Time.Add("@Tax_Task", Tax_Task_Id);
                            ht_Update_Time.Add("@Tax_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                            dt_Update_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Update_Time);




                            htinsert.Add("@Trans", "INSERT");
                            htinsert.Add("@Order_Id", Order_Id);
                            htinsert.Add("@Tax_Task_Id", Tax_Task_Id);
                            htinsert.Add("@Tax_Status_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                            htinsert.Add("@User_Id", User_Id);
                            htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                            htinsert.Add("@Comments", txt_Comments.Text);
                            htinsert.Add("@Inserted_By", User_Id);
                            htinsert.Add("@Status", "True");
                            dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);

                            MessageBox.Show("Order Submitted Sucessfully");
                            this.Close();

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // load_Progressbar.Stop_Progress();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool Validate_Error_Status()
        {


            if (chk_Yes.Checked == false && chk_No.Checked == false)
            {

                MessageBox.Show("Please Check Error Status");
                chk_No.Focus();
                return false;
            }
            if (chk_Yes.Checked == true && txt_Error_Description.Text == "")
            {

                MessageBox.Show("Please Enter Error Note");
                txt_Error_Description.Focus();
                return false;
            }
            return true;



        }

        public void Insert_Order_Commnets()
        {

            if (txt_Comments.Text != "")
            {
                Hashtable htinsert = new Hashtable();
                System.Data.DataTable dtinert = new System.Data.DataTable();
                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Tax_Task", Tax_Task_Id);
                htinsert.Add("@Tax_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                htinsert.Add("@User_Id", User_Id);
                htinsert.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                htinsert.Add("@Comments", txt_Comments.Text);
                htinsert.Add("@Inserted_By", User_Id);
                htinsert.Add("@Status", "True");
                dtinert = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htinsert);


            }

        }

        private bool Validate()
        {
            if (txt_Prdoductiondate.Text == "")
            {
                MessageBox.Show("Enter Production Date");
                txt_Prdoductiondate.Focus();
                return false;
            }
            if (ddl_order_Staus.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Order Status");
                ddl_order_Staus.Focus();
                return false;
            }

            if (ddl_order_Staus.SelectedIndex > 0 && ddl_order_Staus.SelectedValue.ToString() == "2" && txt_followup_Date.Text == "")
            {
                MessageBox.Show("Enter Followup Date");
                txt_followup_Date.Focus();
                return false;
            }
            if (ddl_Order_Source.SelectedValue.ToString() == "1")
            {
                if (Client_Id == 36 && !checkBoxOrderSubmitted.Checked)
                {
                    MessageBox.Show("Select Submitted in Portal");
                    checkBoxOrderSubmitted.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool validateStatus()
        {

            int Status = int.Parse(ddl_order_Staus.SelectedValue.ToString());
            if (Status != 1 && txt_Comments.Text == "")
            {
                MessageBox.Show("Please Enter Comments");
                txt_Comments.Focus();
                return false;


            }
            else
            {

                return true;
            }

        }
        private bool validateOrderSource()
        {
            bool isValid = false;
            if (ddl_Order_Source.SelectedIndex > 0)
            {
                if (ddl_Order_Source.SelectedIndex == 4 && String.IsNullOrWhiteSpace(txtOrder_Source_Details.Text))
                {
                    MessageBox.Show("Enter the order source details");
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            }
            else
            {
                MessageBox.Show("Select Order source");
                isValid = false;
            }

            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validateDelqStatus()
        {
            if (ddl_Order_Source.SelectedValue.ToString() == "1")
            {
                bool isValid = false;
                if (checkBoxDelq_Status_Yes.Checked == false && checkBoxDelq_Status_No.Checked == false)
                {
                    MessageBox.Show("Select Delq Status");
                    checkBoxDelq_Status_No.Focus();
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
                if (isValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        private bool Validate_Tax_Document_Upload()
        {

            if (Order_TaskId == "21")
            {
                if (Tax_Task_Id == "1" && Order_Type_Id != 110)
                {

                    Hashtable htcheck = new Hashtable();
                    System.Data.DataTable dtcheck = new System.Data.DataTable();
                    htcheck.Add("@Trans", "CHECK_TAX_DOCUMENT_UPLOADED");
                    htcheck.Add("@Order_Id", Order_Id);
                    htcheck.Add("@User_id", User_Id);
                    dtcheck = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htcheck);

                    int check = 0;
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

                        MessageBox.Show("Upload Tax Certificate");
                        Tax_Document_Upload txdoc = new Tax_Document_Upload(Order_Id, User_Id, txt_Order_Number.Text, Tax_Task_Id, User_Role);
                        txdoc.Show();
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
            else
            {

                return true;
            }

        }

        private bool Validate_Tax_Details()
        {

            if (Order_TaskId == "22" || Order_Task_Inhouse == 26)
            {

                if (Tax_Task_Id == "1" && Order_Type_Id != 110)
                {
                    Hashtable htcheck_Note = new Hashtable();
                    System.Data.DataTable dtcheck_Note = new System.Data.DataTable();

                    htcheck_Note.Add("@Trans", "CHECK");
                    htcheck_Note.Add("@Tax_Task", Tax_Task_Id);
                    htcheck_Note.Add("@Order_Id", Order_Id);
                    htcheck_Note.Add("@Inserted_By", User_Id);
                    dtcheck_Note = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htcheck_Note);

                    int Check_Count = 0;
                    if (dtcheck_Note.Rows.Count > 0)
                    {

                        Check_Count = int.Parse(dtcheck_Note.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Check_Count = 0;
                    }

                    if (Check_Count == 0)
                    {
                        MessageBox.Show("Tax Details are not Entered please Enter");
                        Tax_Order_Note_Pad txnote = new Tax_Order_Note_Pad(int.Parse(Order_Id), int.Parse(Tax_Task_Id), int.Parse(Tax_Status_Id), int.Parse(User_Id), "Create", txt_Order_Number.Text);
                        txnote.Show();
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
            else
            {
                return true;

            }
        }

        private bool Validate_Tax_Document_Upload_In_Qc()
        {


            if (Tax_Task_Id == "2" && Order_TaskId == "21" && Client_Id != 36)
            {
                Hashtable htcheck = new Hashtable();
                System.Data.DataTable dtcheck = new System.Data.DataTable();
                htcheck.Add("@Trans", "CHECK_DOCUMENT_IN_QC_STAGE");
                htcheck.Add("@Order_Id", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htcheck);

                int check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    check = int.Parse(dtcheck.Rows[0]["count"].ToString());


                }
                else
                {
                    check = 0;
                }

                if (check == 0 && Order_Type_Id != 110)
                {

                    MessageBox.Show("Tax Document is not Uploaded Check it");
                    Tax_Document_Upload txdoc = new Tax_Document_Upload(Order_Id, User_Id, txt_Order_Number.Text, Tax_Task_Id, User_Role);
                    txdoc.Show();
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

        private bool Validate_Tax_Details_Qc()
        {
            if (Order_TaskId == "22" || Order_Task_Inhouse == 26)
            {
                Hashtable htcheck_Note = new Hashtable();
                System.Data.DataTable dtcheck_Note = new System.Data.DataTable();

                htcheck_Note.Add("@Trans", "GET_LAST_UPDATED_DETAILS");
                htcheck_Note.Add("@Order_Id", Order_Id);

                dtcheck_Note = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htcheck_Note);

                int Check_Count = 0;
                if (dtcheck_Note.Rows.Count > 0)
                {

                    Check_Count = 1;
                }
                else
                {

                    Check_Count = 0;
                }

                if (Check_Count == 0 && Order_Type_Id != 110)
                {
                    MessageBox.Show("Tax Details are not Entered please Enter");
                    Tax_Order_Note_Pad txnote = new Tax_Order_Note_Pad(int.Parse(Order_Id), int.Parse(Tax_Task_Id), int.Parse(Tax_Status_Id), int.Parse(User_Id), "Create", txt_Order_Number.Text);
                    txnote.Show();
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
        private bool ValidateTax_Violation_Entry()
        {
            if (Order_Type_Id == 110)
            {
                int Check = 0;
                Hashtable htcheck = new Hashtable();
                System.Data.DataTable dtcheck = new System.Data.DataTable();
                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Order_Id", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Tax_Violation_Entry", htcheck);

                if (dtcheck.Rows.Count > 0)
                {

                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {

                    Check = 0;
                }
                if (Check == 0)
                {

                    MessageBox.Show("Enter Tax Violation Form");
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

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            Tax_Document_Upload txdoc = new Tax_Document_Upload(Order_Id, User_Id, txt_Order_Number.Text, Tax_Task_Id, User_Role);
            txdoc.Show();

        }



        private void chk_Yes_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Yes.Checked == true)
            {

                chk_No.Checked = false;
            }
        }

        private void chk_No_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_No.Checked == true)
            {

                chk_Yes.Checked = false;
            }
        }

        private void btn_Tax_ViolationEntry_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Tax.Tax_Order_Violation_Entry tax_v = new Tax_Order_Violation_Entry(Order_Id, Order_TaskId, Tax_Task_Id, Tax_Status_Id, User_Id, Order_Number, User_Role);
            tax_v.Show();
        }

        private void ddl_order_Staus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_order_Staus.SelectedIndex > 0)
            {

                if (ddl_order_Staus.SelectedValue.ToString() == "2")
                {

                    txt_followup_Date.Enabled = true;
                    MessageBox.Show("Enter Followup Date");

                }
            }
        }

        private void txt_followup_Date_ValueChanged(object sender, EventArgs e)
        {
            if (Follow_Up_Custom != 0)
            {
                txt_followup_Date.CustomFormat = "MM/dd/yyyy";
            }
            Follow_Up_Custom = 1;
        }

        private void btn_Tax_Details_Note_Pad_Click(object sender, EventArgs e)
        {
            Tax_Order_Note_Pad txnote = new Tax_Order_Note_Pad(int.Parse(Order_Id), int.Parse(Tax_Task_Id), int.Parse(Tax_Status_Id), int.Parse(User_Id), "Create", txt_Order_Number.Text);
            txnote.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            Hashtable ht_InserT_Time = new Hashtable();
            System.Data.DataTable dt_Insert_Time = new System.Data.DataTable();
            ht_InserT_Time.Add("@Trans", "UPDATE_END_TIME");
            ht_InserT_Time.Add("@Order_Time_Id", Order_Time_Id);
            dt_Insert_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_InserT_Time);

        }

        private void ddl_Order_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Order_Source.SelectedIndex > 0)
            {
                if (ddl_Order_Source.SelectedIndex == 4)
                {
                    txtOrder_Source_Details.Enabled = true;

                }
                else
                {
                    txtOrder_Source_Details.Text = String.Empty;
                    txtOrder_Source_Details.Enabled = false;
                }
            }
        }

        private void checkBoxDelq_Status_Yes_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDelq_Status_Yes.Checked == true)
            {
                checkBoxDelq_Status_No.Checked = false;
            }
        }

        private void checkBoxDelq_Status_No_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDelq_Status_No.Checked == true)
            {
                checkBoxDelq_Status_Yes.Checked = false;
            }
        }

        private void btn_Inhouse_Documents_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", int.Parse(Order_Id.ToString()), int.Parse(User_Id.ToString()), txt_Order_Number.Text.ToString(), Client_Id.ToString(), Sub_Client_Id.ToString());
            Orderuploads.Show();
        }
    }
}
