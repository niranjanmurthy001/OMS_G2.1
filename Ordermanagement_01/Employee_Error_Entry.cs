using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01
{
    public partial class Employee_Error_Entry : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int result;
        int userid;
        string ErrorType,externalErrorType;
        string ErrorInfoID;
        string ORDERTASK, Username;
        int orderid, Tilte_Exam_Order_Id, SubProcessId, Error_User, AdminStatus,externalErrorUser;
        string Order_num;
        int Work_Type_Id;
        string User_Role;
        string Order_Number;
        string Ordernumber;
        int Order_ID, ErrorInfo_ID,externalErrorInfoId;
        int Client_Id, Sub_ProcessId;
        string Production_Date, erroronuser;

        public Employee_Error_Entry(int User_id, string USER_ROLE, string SESSIONORDERTASK, int Order_Id, int Admin_Status, int WORK_TYPE_ID, string Order_No, string PRODUCTION_DATE, int Error_Info_ID, int CLIENT_ID)
        {
            InitializeComponent();
            userid = User_id;
            Work_Type_Id = WORK_TYPE_ID;
            ORDERTASK = Convert.ToString(SESSIONORDERTASK);
            orderid = Order_Id;
            User_Role = USER_ROLE;
            AdminStatus = Admin_Status;
            Production_Date = PRODUCTION_DATE;
            ErrorInfo_ID = Error_Info_ID;
            Client_Id = CLIENT_ID;
            Order_Number = Order_No;

            //  string OrderNo = Order_Number;
            string OrderNo = "";
            if (Client_Id == 40)
            {
                if (Order_Number.Contains("-N"))
                {
                    OrderNo = Order_Number.Remove(Order_Number.Length - 2);
                }
                else
                {
                    OrderNo = Order_Number;
                }
            }
            // This is for All Client Orders
            //==================================================

            if (Client_Id != 40)
            {
                Hashtable ht_get = new Hashtable();
                DataTable dt_get = new DataTable();
                ht_get.Add("@Trans", "GET_ORDER_ID_BY_ORDER_NUM");
                ht_get.Add("@Client_Order_Number", Order_Number);                   //Order_Number
                dt_get = dataaccess.ExecuteSP("Sp_Error_Info", ht_get);
                if (dt_get.Rows.Count > 0)
                {
                    Order_num = dt_get.Rows[0]["Client_Order_Number"].ToString();
                    Tilte_Exam_Order_Id = int.Parse(dt_get.Rows[0]["Order_ID"].ToString());
                    Sub_ProcessId = int.Parse(dt_get.Rows[0]["Sub_ProcessId"].ToString());
                }
            }

            // This is for Title Exam Orders 40 Client Id
            //==================================================

            if (Client_Id == 40)
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "GET_ORDER_ID_BY_ORDER_NUM");
                ht.Add("@Client_Order_Number", OrderNo);   //OrderNo
                dt = dataaccess.ExecuteSP("Sp_Error_Info", ht);
                if (dt.Rows.Count > 0)
                {
                    Ordernumber = dt.Rows[0]["Client_Order_Number"].ToString();
                    Tilte_Exam_Order_Id = int.Parse(dt.Rows[0]["Order_ID"].ToString());
                    Sub_ProcessId = int.Parse(dt.Rows[0]["Sub_ProcessId"].ToString());
                }
            }
        }

        private void Employee_Error_Entry_Load(object sender, EventArgs e)
        {
            tabPageExternalErrorEntry.PageVisible = false;
            if (User_Role == "1" || User_Role == "4" || User_Role == "6")
            {
                tabPageExternalErrorEntry.PageVisible = true;
               // BindExternalErrorEntry();
            }
            tabPaneErrorEntry.SelectedPage = tabPageInternalErrorEntry;

            Hashtable htuserid = new Hashtable();
            DataTable dtuserid = new DataTable();
            htuserid.Add("@Trans", "USERNAME");
            htuserid.Add("@User_id", userid);
            dtuserid = dataaccess.ExecuteSP("Sp_Error_Info", htuserid);
            if (dtuserid.Rows.Count > 0)
            {
                Username = dtuserid.Rows[0]["User_Name"].ToString();
            }

            Bind_New_ErrorType();
            BindErrorType();
            BindgrdError();

            BindExternalNewErrorType();
            BindExternalErrorType();
            BindGridExternalErrors();

            if (Work_Type_Id == 3)
            {
                if (AdminStatus == 2)// This is For Super Qc
                {
                    dbc.BindError_Task_Super_Qc(Cbo_Task, int.Parse(ORDERTASK));
                    dbc.BindError_Task_Super_Qc(ddlExternalTask, Convert.ToInt32(ORDERTASK));
                }
                else
                {
                    dbc.BindOrderStatus(Cbo_Task);
                    dbc.BindOrderStatus(ddlExternalTask);
                }
            }
            else
            {
                if (AdminStatus == 2)
                {
                    if (Client_Id == 40)// this is for Title Exam order 40 Client Id
                    {
                        dbc.BindError_Task_For_Title_Exam(Cbo_Task);
                        dbc.BindError_Task_For_Title_Exam(ddlExternalTask);
                    }
                    else
                    {
                        dbc.BindError_Task(Cbo_Task, int.Parse(ORDERTASK));
                        dbc.BindError_Task(ddlExternalTask, Convert.ToInt32(ORDERTASK));
                    }
                }
                else
                {
                    dbc.BindOrderStatus(Cbo_Task);
                    dbc.BindOrderStatus(ddlExternalTask);
                }
            }
            grd_Error.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            grd_Error.EnableHeadersVisualStyles = false;
            grd_Error.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            gridExternalError.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            gridExternalError.EnableHeadersVisualStyles = false;
            gridExternalError.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

            ddl_User.Visible = false;
            ddlExternalUser.Visible = false;

            BindgrdError();
            cbo_ErrorCatogery.Focus();
            //Error info details
            if (ErrorInfo_ID != 0)
            {
                Error_Info_Details();
                grd_Error.Visible = false;
            }
        }
        private void Error_Info_Details()
        {
            //Error Dispute
            Hashtable ht_ErrorInfo_Edit = new Hashtable();
            DataTable dt_ErrorInfo_Edit = new DataTable();
            ht_ErrorInfo_Edit.Add("@Trans", "SELECT_BY_ORDER_ID");
            ht_ErrorInfo_Edit.Add("@ErrorInfo_ID", ErrorInfo_ID);
            dt_ErrorInfo_Edit = dataaccess.ExecuteSP("Sp_Error_Info", ht_ErrorInfo_Edit);

            if (dt_ErrorInfo_Edit.Rows.Count > 0)
            {
                ddl_New_Error_Type.SelectedValue = int.Parse(dt_ErrorInfo_Edit.Rows[0]["New_Error_Type_Id"].ToString());
                cbo_ErrorCatogery.SelectedValue = dt_ErrorInfo_Edit.Rows[0]["Error_Type_Id"].ToString();
                cbo_ErrorDes.SelectedValue = dt_ErrorInfo_Edit.Rows[0]["Error_description_Id"].ToString();
                Cbo_Task.SelectedValue = dt_ErrorInfo_Edit.Rows[0]["Error_Task_Id"].ToString();
                txt_ErrorCmt.Text = dt_ErrorInfo_Edit.Rows[0]["Comments"].ToString();

                ErrorInfo_ID = int.Parse(dt_ErrorInfo_Edit.Rows[0]["ErrorInfo_ID"].ToString());
                Lbl_User.Text = dt_ErrorInfo_Edit.Rows[0]["Error_User_Name"].ToString();
                btn_ErrorSub.Text = "Edit";
                Cbo_Task.Enabled = false;
            }
        }
        private void BindgrdError()
        {
            grd_Error.Rows.Clear();
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            if (AdminStatus == 2)
            {
                htselect.Add("@Trans", "SELECT");
                htselect.Add("@Order_ID", orderid);
                htselect.Add("@User_id", userid);
                htselect.Add("@Work_Type", Work_Type_Id);
                dtselect = dataaccess.ExecuteSP("Sp_Error_Info", htselect);
            }
            else
            {
                htselect.Add("@Trans", "BIND_Live");
                htselect.Add("@Order_ID", orderid);
                htselect.Add("@Work_Type", Work_Type_Id);
                dtselect = dataaccess.ExecuteSP("Sp_Error_Info", htselect);
            }
            if (dtselect.Rows.Count > 0)
            {
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Error.Rows.Add();
                    grd_Error.Rows[i].Cells[0].Value = i + 1;
                    grd_Error.Rows[i].Cells[1].Value = dtselect.Rows[i]["New_Error_Type"].ToString();
                    grd_Error.Rows[i].Cells[2].Value = dtselect.Rows[i]["Error_Type"].ToString();
                    grd_Error.Rows[i].Cells[3].Value = dtselect.Rows[i]["Error_Description"].ToString();
                    grd_Error.Rows[i].Cells[4].Value = dtselect.Rows[i]["Comments"].ToString();
                    grd_Error.Rows[i].Cells[5].Value = dtselect.Rows[i]["Error_Task"].ToString();
                    grd_Error.Rows[i].Cells[6].Value = dtselect.Rows[i]["Error_User_Name"].ToString();
                    grd_Error.Rows[i].Cells[7].Value = "Delete";
                    grd_Error.Rows[i].Cells[8].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    grd_Error.Rows[i].Cells[9].Value = dtselect.Rows[i]["User_id"].ToString();
                    grd_Error.Rows[i].Cells[10].Value = dtselect.Rows[i]["ErrorInfo_ID"].ToString();
                    if (dtselect.Rows[i]["Order_Status"].ToString() == "")
                    {
                        grd_Error.Rows[i].Cells[11].Value = "Admin Task";
                    }
                    else
                    {
                        grd_Error.Rows[i].Cells[11].Value = dtselect.Rows[i]["Order_Status"].ToString();
                    }
                    grd_Error.Rows[i].Cells[12].Value = dtselect.Rows[i]["User_name"].ToString();
                    grd_Error.Rows[i].Cells[13].Value = dtselect.Rows[i]["New_Error_Type_Id"].ToString();
                    if (User_Role == "1" || User_Role == "6")
                    {
                        grd_Error.Columns[6].Visible = true;
                        grd_Error.Columns[7].Visible = true;
                    }
                    else
                    {
                        grd_Error.Columns[6].Visible = false;
                        grd_Error.Columns[7].Visible = false;
                    }
                }
            }
        }
        private void BindErrorType()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Error_Type");
            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[0] = "SELECT";
            dtselect.Rows.InsertAt(dr, 0);
            cbo_ErrorCatogery.DataSource = dtselect;
            cbo_ErrorCatogery.ValueMember = "Error_Type_Id";
            cbo_ErrorCatogery.DisplayMember = "Error_Type";
        }
        private void Bind_New_ErrorType()
        {
            Hashtable ht_select = new Hashtable();
            DataTable dt_select = new DataTable();
            ht_select.Add("@Trans", "BIND_NEW_ERROR_TYPE");
            dt_select = dataaccess.ExecuteSP("Sp_Error_Info", ht_select);
            DataRow dr = dt_select.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_select.Rows.InsertAt(dr, 0);
            ddl_New_Error_Type.DataSource = dt_select;
            ddl_New_Error_Type.ValueMember = "New_Error_Type_Id";
            ddl_New_Error_Type.DisplayMember = "New_Error_Type";
        }
        private void cbo_ErrorCatogery_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Error_Type = cbo_ErrorCatogery.Text;

            Hashtable hterror = new Hashtable();
            DataTable dterror = new DataTable();
            hterror.Add("@Trans", "ERROR_TYPE");
            hterror.Add("@Error_Type", Error_Type);
            dterror = dataaccess.ExecuteSP("Sp_Errors_Details", hterror);
            if (dterror.Rows.Count > 0)
            {
                result = int.Parse(dterror.Rows[0]["Error_Type_Id"].ToString());
            }

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Error_description");
            htselect.Add("@Error_Type_Id", result);
            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[0] = "SELECT";
            dtselect.Rows.InsertAt(dr, 0);
            cbo_ErrorDes.DataSource = dtselect;
            cbo_ErrorDes.ValueMember = "Error_description_Id";
            cbo_ErrorDes.DisplayMember = "Error_description";
        }
        private bool Validation()
        {
            if (cbo_ErrorCatogery.Text == "" || cbo_ErrorCatogery.SelectedIndex == 0)
            {
                MessageBox.Show("Select Proper Error Tab");
                cbo_ErrorCatogery.BackColor = Color.Red;
                return false;
            }
            if (cbo_ErrorDes.Text == "" || cbo_ErrorDes.SelectedIndex == 0)
            {
                MessageBox.Show("Select Proper Error Field");
                cbo_ErrorDes.BackColor = Color.Red;
                return false;
            }
            if (txt_ErrorCmt.Text == "")
            {
                MessageBox.Show("Enter Error Comments");
                txt_ErrorCmt.BackColor = Color.Red;
                return false;
            }

            if (ddl_New_Error_Type.Text == "" || ddl_New_Error_Type.SelectedIndex == 0)
            {
                MessageBox.Show("Select Proper  Error Type");
                ddl_New_Error_Type.BackColor = Color.Red;
                return false;
            }
            return true;
        }
        private void btn_ErrorSub_Click(object sender, EventArgs e)
        {
            if (Validation() != false)
            {
                if (btn_ErrorSub.Text == "Submit")
                {
                    if (Cbo_Task.SelectedIndex != 0)
                    {
                        if (Error_User != 0)
                        {
                            ErrorType = cbo_ErrorCatogery.Text;
                            Hashtable htinsert = new Hashtable();
                            DataTable dtinsert = new DataTable();
                            htinsert.Add("@Trans", "INSERT");
                            htinsert.Add("@New_Error_Type_Id", ddl_New_Error_Type.SelectedValue.ToString());  // Added one more column from master New_Error_Type_Id
                            htinsert.Add("@Error_Type", cbo_ErrorCatogery.SelectedValue.ToString());  // Error_Type means Error_Tab new chnages Has done
                            htinsert.Add("@Error_Description", cbo_ErrorDes.SelectedValue.ToString());  // error description means error Field new chnages has done
                            htinsert.Add("@Comments", txt_ErrorCmt.Text);
                            htinsert.Add("@Task", ORDERTASK);
                            htinsert.Add("@User_name", Username);

                            htinsert.Add("@Order_ID", orderid);

                            htinsert.Add("@Error_Task", Cbo_Task.SelectedValue.ToString());
                            htinsert.Add("@Error_Status", 1);//New Error
                            htinsert.Add("@Error_User", Error_User);

                            htinsert.Add("@User_ID", userid);
                            htinsert.Add("@Entered_Date", DateTime.Now);
                            htinsert.Add("@Status", "True");
                            htinsert.Add("@Work_Type", Work_Type_Id);
                            htinsert.Add("@Production_Date", Production_Date);
                            htinsert.Add("@External_Error", false);  //false - It is Internal Error
                            object Error_info_Id = dataaccess.ExecuteSPForScalar("Sp_Error_Info", htinsert);
                            int Ent_error_info_Id = int.Parse(Error_info_Id.ToString());

                            Hashtable hterror_history = new Hashtable();
                            DataTable dterror_history = new DataTable();
                            hterror_history.Add("@Trans", "INSERT");
                            hterror_history.Add("@Order_Id", orderid);
                            hterror_history.Add("@Error_Info_Id", Ent_error_info_Id);
                            hterror_history.Add("@Comments", "Error Created");
                            hterror_history.Add("@User_Id", userid);
                            dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);

                            MessageBox.Show("Error Info Added Successfully");
                            BindgrdError();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Select Properly");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Task");
                    }
                }
                else if (btn_ErrorSub.Text == "Edit" && ErrorInfo_ID != 0)
                {
                    Hashtable htupdate = new Hashtable();
                    DataTable dtupdate = new DataTable();

                    htupdate.Add("@Trans", "UPDATE");
                    htupdate.Add("@ErrorInfo_ID", ErrorInfo_ID);
                    htupdate.Add("@New_Error_Type_Id", ddl_New_Error_Type.SelectedValue.ToString());
                    htupdate.Add("@Error_Type", cbo_ErrorCatogery.SelectedValue.ToString());
                    htupdate.Add("@Error_Description", cbo_ErrorDes.SelectedValue.ToString());
                    htupdate.Add("@Comments", txt_ErrorCmt.Text);

                    dtupdate = dataaccess.ExecuteSP("Sp_Error_Info", htupdate);
                    BindgrdError();
                    MessageBox.Show("Error Info Updated Successfully");

                    clear();
                }
            }
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            ddl_New_Error_Type.SelectedIndex = 0;
            cbo_ErrorCatogery.SelectedIndex = 0;
            cbo_ErrorDes.SelectedIndex = 0;
            txt_ErrorCmt.Text = "";
            cbo_ErrorCatogery.BackColor = Color.WhiteSmoke;
            cbo_ErrorDes.BackColor = Color.WhiteSmoke;
            txt_ErrorCmt.BackColor = Color.WhiteSmoke;
            Cbo_Task.SelectedIndex = 0;
            Lbl_User.Text = "";
            Error_User = 0;
            btn_ErrorSub.Text = "Submit";
        }
        private void grd_Error_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                Hashtable htdel = new Hashtable();
                DataTable dtdel = new DataTable();
                htdel.Add("@Trans", "DELETE");
                htdel.Add("@ErrorInfo_ID", grd_Error.Rows[e.RowIndex].Cells[10].Value);
                dtdel = dataaccess.ExecuteSP("Sp_Error_Info", htdel);

                Hashtable hterror_history = new Hashtable();
                DataTable dterror_history = new DataTable();
                hterror_history.Add("@Trans", "INSERT");
                hterror_history.Add("@Order_Id", orderid);
                hterror_history.Add("@Error_Info_Id", grd_Error.Rows[e.RowIndex].Cells[10].Value);
                hterror_history.Add("@Comments", "Error Deleted");
                hterror_history.Add("@User_Id", userid);
                dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);

                MessageBox.Show(grd_Error.Rows[e.RowIndex].Cells[4].Value + "Deleted Successfully");
                BindgrdError();
            }
        }
        private void cbo_ErrorCatogery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbo_ErrorDes.Focus();
            }
        }
        private void cbo_ErrorDes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_ErrorCmt.Focus();
            }
        }
        private void txt_ErrorCmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ErrorSub.Focus();
            }
        }
        private void btn_ErrorSub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Clear.Focus();
            }
        }
        private void Cbo_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cbo_Task.SelectedIndex != 0)
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                if (Work_Type_Id == 1)
                {
                    ht.Add("@Trans", "Task_User");
                }
                else if (Work_Type_Id == 2)
                {
                    ht.Add("@Trans", "REWORK_TASK_USER");
                }
                else if (Work_Type_Id == 3)
                {
                    ht.Add("@Trans", "Task_User");
                }

                ht.Add("@Task", Cbo_Task.SelectedValue.ToString());

                // ht.Add("@Order_ID", orderid);

                if (Sub_ProcessId != 330 && Sub_ProcessId > 0)
                {
                    ht.Add("@Order_ID", Tilte_Exam_Order_Id);
                }
                else if (Sub_ProcessId > 0)
                {
                    ht.Add("@Order_ID", orderid);
                }

                //  ht.Add("@Order_ID", Order_ID);
                dt = dataaccess.ExecuteSP("Sp_Error_Info", ht);
                if (dt.Rows.Count > 0)
                {
                    Lbl_User.Text = dt.Rows[dt.Rows.Count - 1]["User_Name"].ToString();
                    Error_User = int.Parse(dt.Rows[dt.Rows.Count - 1]["User_id"].ToString());
                    if (User_Role == "2")
                    {
                        Lbl_User.Text = "**********";
                    }
                }
                else
                {
                    Lbl_User.Text = "";
                    Error_User = 0;
                    chk_Username.Visible = true;
                    dbc.Bind_Users_For_Error_Info(ddl_User);
                }
            }
            else
            {
                ddl_User.Visible = false;
                Lbl_User.Text = "";
                Error_User = 0;
            }
        }
        private void ddl_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_User.SelectedIndex > 0)
            {
                Lbl_User.Text = ddl_User.Text.ToString();
                Error_User = int.Parse(ddl_User.SelectedValue.ToString());
                chk_Username.Checked = false;
            }
            else
            {
                Lbl_User.Text = "";
                Error_User = 0;
            }
        }
        private void chk_Username_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Username.Checked == true)
            {
                ddl_User.Visible = true;
            }
            else
            {
                ddl_User.Visible = false;
            }
        }

        //External Error Entry
        private void BindExternalErrorEntry()
        {
            Hashtable ht_ErrorInfo_Edit = new Hashtable();
            DataTable dt_ErrorInfo_Edit = new DataTable();
            ht_ErrorInfo_Edit.Add("@Trans", "SELECT_BY_ORDER_ID");
            ht_ErrorInfo_Edit.Add("@ErrorInfo_ID", externalErrorInfoId);
            dt_ErrorInfo_Edit = dataaccess.ExecuteSP("Sp_Error_Info", ht_ErrorInfo_Edit);

            if (dt_ErrorInfo_Edit.Rows.Count > 0)
            {
                ddlExternalNewErrorType.SelectedValue = int.Parse(dt_ErrorInfo_Edit.Rows[0]["New_Error_Type_Id"].ToString());
                ddlExternalErrorCategory.SelectedValue = dt_ErrorInfo_Edit.Rows[0]["Error_Type_Id"].ToString();
                ddlExternalErrorDesc.SelectedValue = dt_ErrorInfo_Edit.Rows[0]["Error_description_Id"].ToString();
                ddlExternalTask.SelectedValue = dt_ErrorInfo_Edit.Rows[0]["Error_Task_Id"].ToString();
                txtExternalErrorComment.Text = dt_ErrorInfo_Edit.Rows[0]["Comments"].ToString();

                externalErrorInfoId = int.Parse(dt_ErrorInfo_Edit.Rows[0]["ErrorInfo_ID"].ToString());
                lblExternalUser.Text = dt_ErrorInfo_Edit.Rows[0]["Error_User_Name"].ToString();
                btnSubmitExternalError.Text = "Edit";
                ddlExternalTask.Enabled = false;
            }
        }
        private void BindGridExternalErrors()
        {
            gridExternalError.Rows.Clear();
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            if (AdminStatus == 2)
            {
                htselect.Add("@Trans", "SELECT_EXTERNAL");
                htselect.Add("@Order_ID", orderid);
                htselect.Add("@User_id", userid);
                htselect.Add("@Work_Type", Work_Type_Id);
                dtselect = dataaccess.ExecuteSP("Sp_Error_Info", htselect);
            }
            else
            {
                htselect.Add("@Trans", "BIND_Live_External");
                htselect.Add("@Order_ID", orderid);
                htselect.Add("@Work_Type", Work_Type_Id);
                dtselect = dataaccess.ExecuteSP("Sp_Error_Info", htselect);
            }
            if (dtselect.Rows.Count > 0)
            {
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    gridExternalError.Rows.Add();
                    gridExternalError.Rows[i].Cells[0].Value = i + 1;
                    gridExternalError.Rows[i].Cells[1].Value = dtselect.Rows[i]["New_Error_Type"].ToString();
                    gridExternalError.Rows[i].Cells[2].Value = dtselect.Rows[i]["Error_Type"].ToString();
                    gridExternalError.Rows[i].Cells[3].Value = dtselect.Rows[i]["Error_Description"].ToString();
                    gridExternalError.Rows[i].Cells[4].Value = dtselect.Rows[i]["Comments"].ToString();
                    gridExternalError.Rows[i].Cells[5].Value = dtselect.Rows[i]["Error_Task"].ToString();
                    gridExternalError.Rows[i].Cells[6].Value = dtselect.Rows[i]["Error_User_Name"].ToString();
                    gridExternalError.Rows[i].Cells[7].Value = "Delete";
                    gridExternalError.Rows[i].Cells[8].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    gridExternalError.Rows[i].Cells[9].Value = dtselect.Rows[i]["User_id"].ToString();
                    gridExternalError.Rows[i].Cells[10].Value = dtselect.Rows[i]["ErrorInfo_ID"].ToString();
                    if (dtselect.Rows[i]["Order_Status"].ToString() == "")
                    {
                        gridExternalError.Rows[i].Cells[11].Value = "Admin Task";
                    }
                    else
                    {
                        gridExternalError.Rows[i].Cells[11].Value = dtselect.Rows[i]["Order_Status"].ToString();
                    }
                    gridExternalError.Rows[i].Cells[12].Value = dtselect.Rows[i]["User_name"].ToString();
                    gridExternalError.Rows[i].Cells[13].Value = dtselect.Rows[i]["New_Error_Type_Id"].ToString();
                    if (User_Role == "1" || User_Role == "6" || User_Role == "4")
                    {
                        gridExternalError.Columns[6].Visible = true;
                        gridExternalError.Columns[7].Visible = true;
                    }
                    else
                    {
                        gridExternalError.Columns[6].Visible = false;
                        gridExternalError.Columns[7].Visible = false;
                    }
                }
            }
        }
        private void BindExternalErrorType()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Error_Type");
            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[0] = "SELECT";
            dtselect.Rows.InsertAt(dr, 0);
            ddlExternalErrorCategory.DataSource = dtselect;
            ddlExternalErrorCategory.ValueMember = "Error_Type_Id";
            ddlExternalErrorCategory.DisplayMember = "Error_Type";
        }
        private void txtExternalErrorComment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmitExternalError.Focus();
            }
        }
        private void btnSubmitExternalError_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnClearExternalError.Focus();
            }
        }
        private void gridExternalError_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                Hashtable htdel = new Hashtable();
                DataTable dtdel = new DataTable();
                htdel.Add("@Trans", "DELETE");
                htdel.Add("@ErrorInfo_ID", gridExternalError.Rows[e.RowIndex].Cells[10].Value);
                dtdel = dataaccess.ExecuteSP("Sp_Error_Info", htdel);

                Hashtable hterror_history = new Hashtable();
                DataTable dterror_history = new DataTable();
                hterror_history.Add("@Trans", "INSERT");
                hterror_history.Add("@Order_Id", orderid);
                hterror_history.Add("@Error_Info_Id", grd_Error.Rows[e.RowIndex].Cells[10].Value);
                hterror_history.Add("@Comments", "Error Deleted");
                hterror_history.Add("@User_Id", userid);
                dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);

                MessageBox.Show(grd_Error.Rows[e.RowIndex].Cells[4].Value + "Deleted Successfully");
                BindGridExternalErrors();
            }
        }
        private void ddlExternalTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExternalTask.SelectedIndex > 0)
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                if (Work_Type_Id == 1)
                {
                    ht.Add("@Trans", "Task_User");
                }
                else if (Work_Type_Id == 2)
                {
                    ht.Add("@Trans", "REWORK_TASK_USER");
                }
                else if (Work_Type_Id == 3)
                {
                    ht.Add("@Trans", "Task_User");
                }

                ht.Add("@Task", ddlExternalTask.SelectedValue.ToString());

                // ht.Add("@Order_ID", orderid);

                if (Sub_ProcessId != 330 && Sub_ProcessId > 0)
                {
                    ht.Add("@Order_ID", Tilte_Exam_Order_Id);
                }
                else if (Sub_ProcessId > 0)
                {
                    ht.Add("@Order_ID", orderid);
                }

                //  ht.Add("@Order_ID", Order_ID);
                dt = dataaccess.ExecuteSP("Sp_Error_Info", ht);
                if (dt.Rows.Count > 0)
                {
                    lblExternalUser.Text = dt.Rows[dt.Rows.Count - 1]["User_Name"].ToString();
                    externalErrorUser = int.Parse(dt.Rows[dt.Rows.Count - 1]["User_id"].ToString());
                    if (User_Role == "2")
                    {
                        lblExternalUser.Text = "**********";
                    }
                }
                else
                {
                    lblExternalUser.Text = "";
                    Error_User = 0;
                    checkBoxExternalUsername.Visible = true;
                    dbc.Bind_Users_For_Error_Info(ddlExternalUser);
                }
            }
            else
            {
                ddlExternalUser.Visible = false;
                lblExternalUser.Text = "";
                externalErrorUser = 0;
            }
        }
        private void ddlExternalUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExternalUser.SelectedIndex > 0)
            {
                lblExternalUser.Text = ddlExternalUser.Text.ToString();
                externalErrorUser = int.Parse(ddlExternalUser.SelectedValue.ToString());
                checkBoxExternalUsername.Checked = false;
            }
            else
            {
                lblExternalUser.Text = "";
                externalErrorUser = 0;
            }
        }
        private void checkBoxExternalUsername_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxExternalUsername.Checked)
            {
                ddlExternalUser.Visible = true;
            }
            else
            {
                ddlExternalUser.Visible = false;
            }
        }
        private void BindExternalNewErrorType()
        {
            Hashtable ht_select = new Hashtable();
            DataTable dt_select = new DataTable();
            ht_select.Add("@Trans", "BIND_NEW_ERROR_TYPE");
            dt_select = dataaccess.ExecuteSP("Sp_Error_Info", ht_select);
            DataRow dr = dt_select.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_select.Rows.InsertAt(dr, 0);
            ddlExternalNewErrorType.DataSource = dt_select;
            ddlExternalNewErrorType.ValueMember = "New_Error_Type_Id";
            ddlExternalNewErrorType.DisplayMember = "New_Error_Type";
        }
        private bool ValidateExternalErrors()
        {
            if (ddlExternalErrorCategory.SelectedIndex == 0)
            {
                MessageBox.Show("Select error tab");
                ddlExternalErrorCategory.BackColor = Color.Red;
                return false;
            }
            if (ddlExternalErrorDesc.SelectedIndex == 0)
            {
                MessageBox.Show("Select error field");
                ddlExternalErrorDesc.BackColor = Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(txtExternalErrorComment.Text.Trim()))
            {
                MessageBox.Show("Enter error comments");
                txtExternalErrorComment.BackColor = Color.Red;
                return false;
            }
            if (ddlExternalNewErrorType.SelectedIndex == 0)
            {
                MessageBox.Show("Select error type");
                ddlExternalNewErrorType.BackColor = Color.Red;
                return false;
            }
            return true;
        }
        private void btnClearExternalError_Click(object sender, EventArgs e)
        {
            clearExternalFields();
        }
        private void clearExternalFields()
        {
            ddlExternalNewErrorType.SelectedIndex = 0;
            ddlExternalErrorCategory.SelectedIndex = 0;
            ddlExternalErrorDesc.SelectedIndex = 0;
            txtExternalErrorComment.Text = "";
            ddlExternalErrorCategory.BackColor = Color.WhiteSmoke;
            ddlExternalErrorDesc.BackColor = Color.WhiteSmoke;
            txtExternalErrorComment.BackColor = Color.WhiteSmoke;
            ddlExternalTask.SelectedIndex = 0;
            lblExternalUser.Text = "";
            externalErrorUser = 0;
            btnSubmitExternalError.Text = "Submit";
        }
        private void ddlExternalErrorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string externalErrorType = ddlExternalErrorCategory.Text;

            Hashtable hterror = new Hashtable();
            DataTable dterror = new DataTable();
            hterror.Add("@Trans", "ERROR_TYPE");
            hterror.Add("@Error_Type", externalErrorType);
            dterror = dataaccess.ExecuteSP("Sp_Errors_Details", hterror);
            if (dterror.Rows.Count > 0)
            {
                result = int.Parse(dterror.Rows[0]["Error_Type_Id"].ToString());
            }

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Error_description");
            htselect.Add("@Error_Type_Id", result);
            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[0] = "SELECT";
            dtselect.Rows.InsertAt(dr, 0);
            ddlExternalErrorDesc.DataSource = dtselect;
            ddlExternalErrorDesc.ValueMember = "Error_description_Id";
            ddlExternalErrorDesc.DisplayMember = "Error_description";
        }
        private void ddlExternalErrorCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddlExternalErrorDesc.Focus();
            }
        }
        private void ddlExternalErrorDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtExternalErrorComment.Focus();
            }
        }
        private void btnSubmitExternalError_Click(object sender, EventArgs e)
        {
            if (ValidateExternalErrors() != false)
            {
                if (btnSubmitExternalError.Text == "Submit")
                {
                    if (ddlExternalTask.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select Task");
                        ddlExternalTask.Focus();
                        return;
                    }
                    if (externalErrorUser != 0)
                    {
                        externalErrorType = ddlExternalErrorCategory.Text;
                        Hashtable htinsert = new Hashtable();
                        DataTable dtinsert = new DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@New_Error_Type_Id", ddlExternalNewErrorType.SelectedValue.ToString());  // Added one more column from master New_Error_Type_Id
                        htinsert.Add("@Error_Type", ddlExternalErrorCategory.SelectedValue.ToString());  // Error_Type means Error_Tab new chnages Has done
                        htinsert.Add("@Error_Description", ddlExternalErrorDesc.SelectedValue.ToString());  // error description means error Field new chnages has done
                        htinsert.Add("@Comments", txtExternalErrorComment.Text);
                        htinsert.Add("@Task", ddlExternalTask.SelectedValue);
                        htinsert.Add("@User_name", Username);

                        htinsert.Add("@Order_ID", orderid);

                        htinsert.Add("@Error_Task", ddlExternalTask.SelectedValue.ToString());
                        htinsert.Add("@Error_Status", 1);//New Error
                        htinsert.Add("@Error_User", externalErrorUser);

                        htinsert.Add("@User_ID", userid);
                        htinsert.Add("@Entered_Date", DateTime.Now);
                        htinsert.Add("@Status", "True");
                        htinsert.Add("@Work_Type", Work_Type_Id);
                        htinsert.Add("@Production_Date", Production_Date);
                        htinsert.Add("@External_Error", true); //ture - It is External Error

                        object Error_info_Id = dataaccess.ExecuteSPForScalar("Sp_Error_Info", htinsert);
                        int Ent_error_info_Id = int.Parse(Error_info_Id.ToString());

                        Hashtable hterror_history = new Hashtable();
                        DataTable dterror_history = new DataTable();
                        hterror_history.Add("@Trans", "INSERT");
                        hterror_history.Add("@Order_Id", orderid);
                        hterror_history.Add("@Error_Info_Id", Ent_error_info_Id);
                        hterror_history.Add("@Comments", "Error Created");
                        hterror_history.Add("@User_Id", userid);
                        dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);

                        MessageBox.Show("Error Info Added Successfully");
                        BindGridExternalErrors();
                        clearExternalFields();
                    }
                    else
                    {
                        MessageBox.Show("Select user");
                    }
                }
                else if (btnSubmitExternalError.Text == "Edit" && externalErrorInfoId != 0)
                {
                    Hashtable htupdate = new Hashtable();
                    DataTable dtupdate = new DataTable();

                    htupdate.Add("@Trans", "UPDATE");
                    htupdate.Add("@ErrorInfo_ID", externalErrorInfoId);
                    htupdate.Add("@New_Error_Type_Id", ddlExternalNewErrorType.SelectedValue.ToString());
                    htupdate.Add("@Error_Type", ddlExternalErrorCategory.SelectedValue.ToString());
                    htupdate.Add("@Error_Description", ddlExternalErrorDesc.SelectedValue.ToString());
                    htupdate.Add("@Comments", txtExternalErrorComment.Text);

                    dtupdate = dataaccess.ExecuteSP("Sp_Error_Info", htupdate);                   
                    MessageBox.Show("Error Info Updated Successfully");
                    BindGridExternalErrors();
                    clearExternalFields();
                }
            }
        }
    }
}
