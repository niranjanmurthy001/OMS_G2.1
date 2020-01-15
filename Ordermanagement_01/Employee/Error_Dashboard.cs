using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;

namespace Ordermanagement_01.Employee
{
    public partial class Error_Dashboard : Form
    {
        int User_Id, User_Role;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        DataTable dtgetAcceptedErrors = new DataTable();

        DialogResult dialogResult;
        int Error_Report = 0;
        string Path1;
        string Production_Date;
        int workttype;
        string worktypename;

        public Error_Dashboard(int USER_ID, int USER_ROLE, string PRODUCTION_DATE)
        {
            InitializeComponent();
            User_Id = USER_ID;
            User_Role = USER_ROLE;
            Production_Date = PRODUCTION_DATE;
        }
        private void Error_Dashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            //pnl_New_Error_Reporting_To.Enabled = true;
            lbl_Sub_New_Error_Header.Text = "New Error Report ";
            Erro_Dashboard_Count();

            if (User_Role == 2)
            {
                btn_My_Errors_Click(sender, e);
                //  My_New_Errors();

                lbl_Dispute_Error_Header.Text = "My Disputed Errors";

                lbl_Error_Reprt_Header.Text = "My Errors Report";


            }
            dbc.Bind_Manager_Supervisor_Users(ddl_New_Error_Reporting_User_Name);
            dbc.Bind_Manager_Supervisor_Users(ddl_Errors_Reporting_User_Name);
            dbc.Bind_Manager_Supervisor_Users(ddl_Dispute_Reporting_Username);
            BindErrorFrom(ddlErrorFrom);
            BindErrorFrom(ddlDisputeErrorFrom);
            BindErrorFrom(ddlReportsErrorFrom);
            Grd_New_Errors.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // Grd_New_Errors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            if (User_Role == 1 || User_Role == 6 || User_Role == 4 || User_Role == 3)
            {
                btn_Error_Analysis_Report.Visible = true;
            }
            if (User_Role == 2)
            {
                pnl_User_Role.Visible = false;
                lbl_Dispute_Reporting_To.Visible = false;
                ddl_Dispute_Reporting_Username.Visible = false;
                btn_My_Dispute.Visible = false;
                btn_All_Dispute.Visible = false;
                lbl_Error_Reporting_To.Visible = false;
                ddl_Errors_Reporting_User_Name.Visible = false;
                btn_My_Error_Report.Visible = false;
                btn_All_Employee_Report.Visible = false;

                lbl_Dispute_ErrorOnUser.Visible = false;
                ddl_Dispute_ErrorOnUser.Visible = false;

                ddl_Error_Report_ErrorOnUser.Visible = false;
                lbl_Error_User_Name.Visible = false;

                lblErrorFrom.Visible = false;
                ddlReportsErrorFrom.Visible = false;
            }
            else
            {
                pnl_User_Role.Visible = true;


            }

            tbl_Layout_Dispute_Accept.Visible = false;
            pnl_Dispute_Dates.Visible = true;
            lbl_Dispute_Reporting_To.Visible = false;
            ddl_Dispute_Reporting_Username.Visible = false;
            lbl_Error_Reporting_To.Visible = false;
            ddl_Errors_Reporting_User_Name.Visible = false;

            string D1 = DateTime.Now.ToString("MM/dd/yyyy");
            string D2 = DateTime.Now.ToString("MM/dd/yyyy");

            txt_Accepted_First_date.Format = DateTimePickerFormat.Custom;
            txt_Accepted_Second_Date.Format = DateTimePickerFormat.Custom;
            txt_Accepted_First_date.CustomFormat = "MM/dd/yyyy";
            txt_Accepted_Second_Date.CustomFormat = "MM/dd/yyyy";

            txt_Accepted_First_date.Text = D1;
            txt_Accepted_Second_Date.Text = D2;

            txt_Dispute_From_Date.Format = DateTimePickerFormat.Custom;
            txt_Dispute_From_Date.CustomFormat = "MM/dd/yyyy";

            txt_Dispute_To_Date.Format = DateTimePickerFormat.Custom;
            txt_Dispute_To_Date.CustomFormat = "MM/dd/yyyy";
            txt_Dispute_From_Date.Text = D1;
            txt_Dispute_To_Date.Text = D2;

            // This is for Error Report
            Error_Report = 1;
            tabControl1.TabIndex = 1;
            if (User_Role == 1 || User_Role == 6 || User_Role == 4 || User_Role == 3)
            {
                btn_All_Employee_Report_Click(sender, e);
                btn_All_User_Errors_Click(sender, e);
                btn_All_Dispute_Click(sender, e);
                lbl_Dispute_Error_Header.Text = "All Disputed Errors";
                lbl_Error_Reprt_Header.Text = "All Errors Report";
                lbl_Sub_New_Error_Header.Text = "All New Errors";
            }
            //
            dbc.Bind_UserName_In_ErrorDashboard(ddl_NewErrors_Error_On_User);
            dbc.Bind_UserName_In_ErrorDashboard(ddl_Dispute_ErrorOnUser);
            dbc.Bind_UserName_In_ErrorDashboard(ddl_Error_Report_ErrorOnUser);
        }
        private void BindErrorFrom(ComboBox ddlErrorFrom)
        {
            var dictionary = new Dictionary<object, string>()
            {
                [0] = "ALL",
                [false] = "INTERNAL",
                [true] = "EXTERNAL"
            };
            ddlErrorFrom.DataSource = new BindingSource(dictionary, null);
            ddlErrorFrom.DisplayMember = "Value";
            ddlErrorFrom.ValueMember = "Key";
        }
        private void Erro_Dashboard_Count()
        {
            Hashtable hterro_Count = new Hashtable();
            DataTable dterror_Count = new DataTable();
            if (User_Role == 2)
            {
                hterro_Count.Add("@Trans", "NEW_ERRORS_COUNT_FOR_USER_WISE");
            }
            else
            {
                hterro_Count.Add("@Trans", "NEW_ERRORS_COUNT_FOR_ADMIN_WISE");
            }

            hterro_Count.Add("@Error_From_User_Id", User_Id);
            dterror_Count = dataaccess.ExecuteSP("Sp_Error_Dashboard", hterro_Count);

            if (dterror_Count.Rows.Count > 0)
            {
                tabControl1.TabPages[0].Text = "New Errors  " + "(" + dterror_Count.Rows[0]["Error_Count"].ToString() + ")";
            }
            else
            {
                tabControl1.TabPages[0].Text = "New Errors  " + "(0)";
            }


            Hashtable hterro_Count2 = new Hashtable();
            DataTable dterror_Count2 = new DataTable();
            if (User_Role == 2)
            {
                hterro_Count2.Add("@Trans", "NEW_ERRORS_COUNT_FOR_USER_WISE");
                hterro_Count2.Add("@Error_On_User_Id", User_Id);
                dterror_Count2 = dataaccess.ExecuteSP("Sp_Error_Dashboard", hterro_Count2);
                if (dterror_Count2.Rows.Count > 0)
                {
                    btn_My_Errors.Text = "My Errors  " + "(" + dterror_Count2.Rows[0]["Error_Count"].ToString() + ")";
                }
                else
                {
                    btn_My_Errors.Text = "My Errors  " + "(0)";
                }
            }

            // Rejected Error Count


            Hashtable htRejected_erro_Count = new Hashtable();
            DataTable dtRejected_erro_Count = new DataTable();

            if (User_Role == 1 || User_Role == 6 || User_Role == 4 || User_Role == 3)
            {
                htRejected_erro_Count.Add("@Trans", "REJECT_ERRORS_COUNT_FOR_ADMIN_WISE");
            }

            htRejected_erro_Count.Add("@Error_On_User_Id", User_Id);
            dtRejected_erro_Count = dataaccess.ExecuteSP("Sp_Error_Dashboard", htRejected_erro_Count);

            if (User_Role == 1 || User_Role == 6 || User_Role == 4 || User_Role == 3)
            {
                if (dtRejected_erro_Count.Rows.Count > 0)
                {
                    tabControl1.TabPages[1].Text = "Dispute  " + "(" + dtRejected_erro_Count.Rows[0]["Error_Count"].ToString() + ")";
                    btn_All_Dispute.Text = "All Dispute  " + "(" + dtRejected_erro_Count.Rows[0]["Error_Count"].ToString() + ")";
                }
                else
                {
                    tabControl1.TabPages[1].Text = "Dispute  " + "(0)";
                }
            }
            if (User_Role == 1 || User_Role == 6 || User_Role == 4 || User_Role == 3)
            {

                Hashtable hterro_Count1 = new Hashtable();
                DataTable dterror_Count1 = new DataTable();

                hterro_Count1.Add("@Trans", "NEW_ERRORS_COUNT_FOR_ADMIN_WISE");
                hterro_Count1.Add("@Error_On_User_Id", User_Id);
                dterror_Count1 = dataaccess.ExecuteSP("Sp_Error_Dashboard", hterro_Count1);

                if (dterror_Count1.Rows.Count > 0)
                {
                    btn_All_User_Errors.Text = "All User Errors  " + "(" + dterror_Count1.Rows[0]["Error_Count"].ToString() + ")";
                }
                else
                {
                    btn_All_User_Errors.Text = "All User Errors  " + "(0)";
                }
            }
        }
        private void My_New_Errors()
        {
            try
            {
                Hashtable htget_New_Erros = new Hashtable();
                DataTable dtget_New_Errors = new DataTable();

                htget_New_Erros.Add("@Trans", "GET_NEW_ERRORS_USER_WISE");
                // htget_New_Erros.Add("@Error_From_User_Id", User_Id);
                htget_New_Erros.Add("@Error_On_User_Id", User_Id);
                dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);

                if (dtget_New_Errors.Rows.Count > 0)
                {
                    Grd_New_Errors.Rows.Clear();

                    for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                    {
                        Grd_New_Errors.Rows.Add();
                        Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                        Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                        Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                        Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                        Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                        Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                        Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                        Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                        Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                        Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                        Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                        Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                        Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                        Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                        Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                        Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                        Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                        Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                        Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();
                        Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                        DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                        htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                        htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                        dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                        if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                        {
                            Grd_New_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                        }
                        else
                        {
                            Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                        }
                        Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                        if (User_Role == 2)
                        {
                            Grd_New_Errors.Columns[4].Visible = false;
                            Grd_New_Errors.Columns[14].Visible = false;
                            Grd_New_Errors.Columns[15].Visible = false;
                        }
                    }
                }
                else
                {
                    Grd_New_Errors.Rows.Clear();
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in With Binding Data;Please Contact Administrator");
            }
        }
        private void All_User_New_Errors()
        {
            try
            {
                Hashtable htget_New_Erros = new Hashtable();
                DataTable dtget_New_Errors = new DataTable();

                htget_New_Erros.Add("@Trans", "GET_NEW_ERRORS_ADMIN_WISE");
                dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);
                if (dtget_New_Errors.Rows.Count > 0)
                {
                    Grd_New_Errors.Rows.Clear();
                    for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                    {
                        Grd_New_Errors.Rows.Add();
                        Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                        Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                        Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                        Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                        Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                        Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                        Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                        Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                        Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                        Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                        Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                        Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                        Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                        Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                        Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                        Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                        Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                        Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                        Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();

                        if (User_Role == 2)
                        {
                            Grd_New_Errors.Columns[14].Visible = false;
                            Grd_New_Errors.Columns[15].Visible = false;
                            Grd_New_Errors.Columns[4].Visible = false;
                        }
                    }

                    foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                    {
                        row.Height = 50;
                    }

                }
                else
                {

                    Grd_New_Errors.Rows.Clear();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void btn_My_Errors_Click(object sender, EventArgs e)
        {
            Grd_New_Errors.Rows.Clear();

            My_New_Errors();

            pnl_New_Error_Reporting_To.Visible = false;

            lbl_Sub_New_Error_Header.Text = "My Errors";

            btn_All_User_Errors.Visible = true;
            btn_My_Errors.Visible = true;

        }
        private void btn_All_User_Errors_Click(object sender, EventArgs e)
        {
            ddl_New_Error_Reporting_User_Name.SelectedIndex = 0;
            // ddl_NewErrors_Error_On_User.SelectedIndex = 0;

            pnl_New_Error_Reporting_To.Visible = true;
            Grd_New_Errors.Rows.Clear();
            lbl_Sub_New_Error_Header.Text = "All User Errors";
            All_User_New_Errors();
        }
        private void btn_New_Error_Accept_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("do you Want to Accepted?", "New Errors", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    if (Validate_New_Error() != false)
                    {
                        int Reject_Count = 0;
                        for (int i = 0; i < Grd_New_Errors.Rows.Count; i++)
                        {
                            bool isChecked = (bool)Grd_New_Errors[0, i].FormattedValue;

                            if (isChecked == true)
                            {
                                Reject_Count = 1;
                                int Error_Info_Id = int.Parse(Grd_New_Errors.Rows[i].Cells[19].Value.ToString());
                                int Order_Id = int.Parse(Grd_New_Errors.Rows[i].Cells[20].Value.ToString());

                                Hashtable htupdate_Erro_Infp = new Hashtable();
                                DataTable dtupdate_Error_Info = new System.Data.DataTable();

                                htupdate_Erro_Infp.Add("@Trans", "UPDATE_ERROR_USER_COMMENTS");
                                htupdate_Erro_Infp.Add("@User_Comments", txt_User_New_Error_Comments.Text.Trim().ToString());
                                htupdate_Erro_Infp.Add("@ErrorInfo_ID", Error_Info_Id);
                                dtupdate_Error_Info = dataaccess.ExecuteSP("Sp_Error_Info", htupdate_Erro_Infp);

                                Hashtable htupdate_Error_Status = new Hashtable();
                                DataTable dtupdate_Error_Status = new System.Data.DataTable();

                                htupdate_Error_Status.Add("@Trans", "UPDATE_ERROR_STATUS");
                                htupdate_Error_Status.Add("@Error_Status", 2);
                                htupdate_Error_Status.Add("@ErrorInfo_ID", Error_Info_Id);
                                dtupdate_Error_Status = dataaccess.ExecuteSP("Sp_Error_Info", htupdate_Error_Status);


                                Hashtable hterror_history = new Hashtable();
                                DataTable dterror_history = new DataTable();
                                hterror_history.Add("@Trans", "INSERT");
                                hterror_history.Add("@Order_Id", Order_Id);
                                hterror_history.Add("@Error_Info_Id", Error_Info_Id);
                                hterror_history.Add("@Comments", "Error Accepted");
                                hterror_history.Add("@User_Id", User_Id);
                                dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);


                            }

                        }
                        if (Reject_Count >= 1)
                        {
                            txt_User_New_Error_Comments.Text = "";
                            My_New_Errors();
                            MessageBox.Show("Errors Were Accepted Sucessfully, These Will Move to Accepted Error Queue");

                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Problem in With Accepting Error;Please Contact Administrator");

                }
            }
            else if (dialogResult == DialogResult.No)
            {


            }
        }
        private bool Validate_New_Error()
        {
            try
            {
                if (txt_User_New_Error_Comments.Text == "" && txt_User_New_Error_Comments.Text.Length <= 10)
                {
                    MessageBox.Show("Need to Enter Reject Comments");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        private bool Validate_Dispute_Error()
        {
            try
            {
                if (txt_Manager_Reject_Comments.Text == "" && txt_Manager_Reject_Comments.Text.Length <= 10)
                {
                    MessageBox.Show("Need to Manager Comments");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        private void btn_New_Error_Reject_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("do you Want to Dispute?", "New Errors", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    if (Validate_New_Error() != false)
                    {
                        int Reject_Count = 0;
                        for (int i = 0; i < Grd_New_Errors.Rows.Count; i++)
                        {
                            bool isChecked = (bool)Grd_New_Errors[0, i].FormattedValue;

                            if (isChecked == true)
                            {
                                Reject_Count = 1;
                                int Error_Info_Id = int.Parse(Grd_New_Errors.Rows[i].Cells[19].Value.ToString());
                                int Order_Id = int.Parse(Grd_New_Errors.Rows[i].Cells[20].Value.ToString());

                                Hashtable htupdate_Erro_Infp = new Hashtable();
                                DataTable dtupdate_Error_Info = new System.Data.DataTable();

                                htupdate_Erro_Infp.Add("@Trans", "UPDATE_ERROR_USER_COMMENTS");
                                htupdate_Erro_Infp.Add("@User_Comments", txt_User_New_Error_Comments.Text.Trim().ToString());
                                htupdate_Erro_Infp.Add("@ErrorInfo_ID", Error_Info_Id);
                                dtupdate_Error_Info = dataaccess.ExecuteSP("Sp_Error_Info", htupdate_Erro_Infp);

                                Hashtable htupdate_Error_Status = new Hashtable();
                                DataTable dtupdate_Error_Status = new System.Data.DataTable();

                                htupdate_Error_Status.Add("@Trans", "UPDATE_ERROR_STATUS");
                                htupdate_Error_Status.Add("@Error_Status", 3);
                                htupdate_Error_Status.Add("@ErrorInfo_ID", Error_Info_Id);
                                dtupdate_Error_Status = dataaccess.ExecuteSP("Sp_Error_Info", htupdate_Error_Status);

                                Hashtable hterror_history = new Hashtable();
                                DataTable dterror_history = new DataTable();
                                hterror_history.Add("@Trans", "INSERT");
                                hterror_history.Add("@Order_Id", Order_Id);
                                hterror_history.Add("@Error_Info_Id", Error_Info_Id);
                                hterror_history.Add("@Comments", "Error Disputed");
                                hterror_history.Add("@User_Id", User_Id);
                                dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);

                            }
                        }
                        if (Reject_Count >= 1)
                        {
                            txt_User_New_Error_Comments.Text = "";
                            My_New_Errors();
                            MessageBox.Show("Errors Were Disputed Sucessfully, These Will Move to Dispute Queue");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem in With Dispute Error;Please Contact Administrator");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
        private void Grd_New_Errors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    Order_Entry Order_Entry = new Order_Entry(int.Parse(Grd_New_Errors.Rows[e.RowIndex].Cells[20].Value.ToString()), User_Id, User_Role.ToString(), Production_Date);
                    Order_Entry.Show();
                }
                if (e.ColumnIndex == 3)
                {

                    Error_Documents Error_Doc_View = new Error_Documents(int.Parse(Grd_New_Errors.Rows[e.RowIndex].Cells[20].Value.ToString()), User_Role, int.Parse(Grd_New_Errors.Rows[e.RowIndex].Cells[19].Value.ToString()), User_Id, Grd_New_Errors.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Error_Doc_View.Show();
                }
                if (e.ColumnIndex == 4)
                {
                    Error_History Error_Hist_View = new Error_History(int.Parse(Grd_New_Errors.Rows[e.RowIndex].Cells[20].Value.ToString()), int.Parse(Grd_New_Errors.Rows[e.RowIndex].Cells[19].Value.ToString()), Grd_New_Errors.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Error_Hist_View.Show();
                }
                if (e.ColumnIndex == 10)
                {
                    if (!string.IsNullOrEmpty(Grd_New_Errors.Rows[e.RowIndex].Cells[10].Value.ToString()))
                    {
                        ErrorComments comments = new ErrorComments(Grd_New_Errors.Rows[e.RowIndex].Cells[10].Value.ToString());
                        this.Invoke(new MethodInvoker(delegate
                       {
                           comments.Show();
                       }));
                    }
                }
            }

        }
        private void btn_Accepted_Error_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                load_Progressbar.Start_progres();
                string Error_From_Date = txt_Accepted_First_date.Text.ToString();
                string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();

                Hashtable htget_Accepted_Errors = new Hashtable();
                DataTable dtget_Accepted_Errors = new DataTable();
                if (Error_Report == 1)
                {
                    htget_Accepted_Errors.Add("@Trans", "GET_EMPLOYEE_WISE_ERROR_REPORT");
                }
                else if (Error_Report == 2)
                {
                    if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0)
                    {
                        htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROS_REPORTING_WISE");
                        htget_Accepted_Errors.Add("@Reporting_User_Id", int.Parse(ddl_Errors_Reporting_User_Name.SelectedValue.ToString()));
                    }
                    else
                    {
                        htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROR_REPORT");
                    }
                }

                htget_Accepted_Errors.Add("@Error_On_User_Id", User_Id);
                htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
                htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
                dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

                if (dtget_Accepted_Errors.Rows.Count > 0)
                {

                    Grid_Error.Rows.Clear();
                    for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                    {
                        Grid_Error.Rows.Add();

                        Grid_Error.Rows[i].Cells[0].Value = i + 1;
                        Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                        Grid_Error.Rows[i].Cells[3].Value = "View";
                        Grid_Error.Rows[i].Cells[4].Value = "Edit";
                        Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                        Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                        Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                        Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                        Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                        Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                        Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                        Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                        Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                        Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                        Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                        Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                        Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                        Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                        Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                        Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                        Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                        Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                        Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                        Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                        Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                        Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                        Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();


                        Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                        DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                        htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                        htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                        dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                        if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                        {
                            Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                        }
                        else
                        {
                            Grid_Error.Rows[i].Cells[2].Value = "Upload";
                        }
                        if (User_Role == 2)
                        {
                            Grid_Error.Columns[3].Visible = false;
                            Grid_Error.Columns[4].Visible = false;
                            Grid_Error.Columns[14].Visible = false;
                            Grid_Error.Columns[15].Visible = false;
                            Grid_Error.Columns[23].Visible = false;
                            Grid_Error.Columns[24].Visible = false;
                        }
                    }
                    foreach (DataGridViewRow row in Grid_Error.Rows)
                    {
                        row.Height = 50;
                    }
                }
                else
                {
                    Grid_Error.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void btn_My_Dispute_Click(object sender, EventArgs e)
        {
            pnl_Dispute_Dates.Visible = true;
            tbl_Layout_Dispute_Accept.Visible = false;
            lbl_Dispute_Reporting_To.Visible = false;
            ddl_Dispute_Reporting_Username.Visible = false;
            Grid_Disputed_Errors.Rows.Clear();

            lbl_Dispute_Error_Header.Text = "My Dispute Errors";

            lbl_Dispute_ErrorOnUser.Visible = false;
            ddl_Dispute_ErrorOnUser.Visible = false;

        }
        private void Bind_User_Wise_Disputes()
        {
            try
            {
                Hashtable ht_All_Disputes = new Hashtable();
                DataTable dt_All_Disputes = new System.Data.DataTable();
                ht_All_Disputes.Add("@Trans", "GET_EMPLOYEE_WISE_REJECTED_ERRORS");

                ht_All_Disputes.Add("@Error_On_User_Id", User_Id);
                ht_All_Disputes.Add("@Error_From_Date", txt_Dispute_From_Date.Text);
                ht_All_Disputes.Add("@Error_To_Date", txt_Dispute_To_Date.Text);

                dt_All_Disputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_All_Disputes);
                if (dt_All_Disputes.Rows.Count > 0)
                {
                    Grid_Disputed_Errors.Rows.Clear();
                    for (int i = 0; i < dt_All_Disputes.Rows.Count; i++)
                    {
                        Grid_Disputed_Errors.Rows.Add();
                        Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                        Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_All_Disputes.Rows[i]["Client_Order_Number"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                        Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                        Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_All_Disputes.Rows[i]["Work_Type"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_All_Disputes.Rows[i]["Error_From"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_All_Disputes.Rows[i]["New_Error_Type"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_All_Disputes.Rows[i]["Error_Type"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_All_Disputes.Rows[i]["Error_description"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_All_Disputes.Rows[i]["Comments"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_All_Disputes.Rows[i]["Error_On_Task"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_All_Disputes.Rows[i]["Error_On_User_Name"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_All_Disputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_All_Disputes.Rows[i]["Error_Entered_From_Task"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_All_Disputes.Rows[i]["Error_Entered_From"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_All_Disputes.Rows[i]["Entered_Date"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_All_Disputes.Rows[i]["User_Comments"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_All_Disputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_All_Disputes.Rows[i]["Supervisor_Name"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_All_Disputes.Rows[i]["Manger_Error_Status"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Order_ID"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_All_Disputes.Rows[i]["Error_On_User_Id"].ToString();

                        //  Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Error_Type_Id"].ToString();
                        //  Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_All_Disputes.Rows[i]["Error_description_Id"].ToString();

                        Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_All_Disputes.Rows[i]["Reporting_1"].ToString();
                        Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_All_Disputes.Rows[i]["Reporting_2"].ToString();
                        //Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_All_Disputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();

                        Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                        DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                        htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                        htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString());
                        dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                        if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                        {

                            Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                        }
                        else
                        {
                            Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                        }
                        if (User_Role == 2)
                        {
                            Grid_Disputed_Errors.Columns[15].Visible = false;
                            Grid_Disputed_Errors.Columns[16].Visible = false;
                            Grid_Disputed_Errors.Columns[4].Visible = false;
                            Grid_Disputed_Errors.Columns[5].Visible = false;
                        }

                    }

                    foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                    {
                        row.Height = 50;
                    }

                }
                else
                {
                    Grid_Disputed_Errors.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void BindNewErrors()
        {
            try
            {
                if (ddl_New_Error_Reporting_User_Name.SelectedIndex == 0 && ddl_NewErrors_Error_On_User.SelectedIndex == 0 && ddlErrorFrom.SelectedIndex == 0)
                {
                    pnl_New_Error_Reporting_To.Visible = true;
                    ddl_NewErrors_Error_On_User.SelectedIndex = 0;
                    btn_All_User_Errors.PerformClick();
                }
                if (ddl_New_Error_Reporting_User_Name.SelectedIndex > 0 && ddl_NewErrors_Error_On_User.SelectedIndex == 0 && ddlErrorFrom.SelectedIndex == 0)
                {
                    Bind_User_New_Errors_Users_reporting_Wise(int.Parse(ddl_New_Error_Reporting_User_Name.SelectedValue.ToString()));
                    pnl_New_Error_Reporting_To.Visible = true;
                }
                if (ddl_New_Error_Reporting_User_Name.SelectedIndex > 0 && ddl_NewErrors_Error_On_User.SelectedIndex > 0 && ddlErrorFrom.SelectedIndex == 0)
                {
                    Bind_NewErrorsUser_By_ReportingTo_Error_On_User_Wise(int.Parse(ddl_New_Error_Reporting_User_Name.SelectedValue.ToString()), int.Parse(ddl_NewErrors_Error_On_User.SelectedValue.ToString()));
                }
                if (ddl_New_Error_Reporting_User_Name.SelectedIndex > 0 && ddl_NewErrors_Error_On_User.SelectedIndex > 0 && ddlErrorFrom.SelectedIndex > 0)
                {
                    BindReportingWiseUserWiseErrorFrom(ddl_New_Error_Reporting_User_Name.SelectedValue, ddl_NewErrors_Error_On_User.SelectedValue, ddlErrorFrom.SelectedValue);
                }
                else if (ddl_New_Error_Reporting_User_Name.SelectedIndex > 0 && ddl_NewErrors_Error_On_User.SelectedIndex == 0 && ddlErrorFrom.SelectedIndex > 0)
                {
                    BindReportingWiseErrorsFrom(ddl_New_Error_Reporting_User_Name.SelectedValue, ddlErrorFrom.SelectedValue);
                }

                else if (ddl_New_Error_Reporting_User_Name.SelectedIndex == 0 && ddl_NewErrors_Error_On_User.SelectedIndex > 0 && ddlErrorFrom.SelectedIndex == 0)
                {
                    Bind_NewErrors_Error_On_User_Wise(int.Parse(ddl_NewErrors_Error_On_User.SelectedValue.ToString()));
                }
                else if (ddl_New_Error_Reporting_User_Name.SelectedIndex == 0 && ddl_NewErrors_Error_On_User.SelectedIndex > 0 && ddlErrorFrom.SelectedIndex > 0)
                {
                    BindUserWiseErrorFrom(ddl_NewErrors_Error_On_User.SelectedValue, ddlErrorFrom.SelectedValue);
                }
                else if (ddl_New_Error_Reporting_User_Name.SelectedIndex == 0 && ddl_NewErrors_Error_On_User.SelectedIndex == 0 && ddlErrorFrom.SelectedIndex > 0)
                {
                    NewErrorFrom(ddlErrorFrom.SelectedValue);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void ddl_NewErrors_Error_On_User_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindNewErrors();
        }
        private void ddl_New_Error_Reporting_User_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindNewErrors();
        }
        private void ddlErrorFrom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindNewErrors();
        }
        private void NewErrorFrom(object errorFrom)
        {
            Hashtable htget_New_Erros = new Hashtable();
            DataTable dtget_New_Errors = new DataTable();

            htget_New_Erros.Add("@Trans", "ERROR_FROM");
            htget_New_Erros.Add("@External_Error", errorFrom);
            dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);
            if (dtget_New_Errors.Rows.Count > 0)
            {
                Grd_New_Errors.Rows.Clear();
                for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                {
                    Grd_New_Errors.Rows.Add();
                    Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                    Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                    Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                    Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                    Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                    Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                    Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                    Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();
                    if (User_Role == 2)
                    {
                        Grd_New_Errors.Columns[14].Visible = false;
                        Grd_New_Errors.Columns[15].Visible = false;
                    }
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grd_New_Errors.Rows.Clear();
            }
        }
        private void BindUserWiseErrorFrom(object errorOnUser, object errorFrom)
        {
            Hashtable htget_New_Erros = new Hashtable();
            DataTable dtget_New_Errors = new DataTable();

            htget_New_Erros.Add("@Trans", "SELECT_BY_ERROR_ON_USER_ERROR_FROM");
            htget_New_Erros.Add("@Error_On_User_Id", errorOnUser);
            htget_New_Erros.Add("@External_Error", errorFrom);
            dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);
            if (dtget_New_Errors.Rows.Count > 0)
            {
                Grd_New_Errors.Rows.Clear();
                for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                {
                    Grd_New_Errors.Rows.Add();
                    Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                    Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                    Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                    Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                    Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                    Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                    Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                    Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();
                    if (User_Role == 2)
                    {
                        Grd_New_Errors.Columns[14].Visible = false;
                        Grd_New_Errors.Columns[15].Visible = false;
                    }
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grd_New_Errors.Rows.Clear();
            }
        }
        private void BindReportingWiseUserWiseErrorFrom(object reportingTo, object errorOnUser, object errorFrom)
        {
            Hashtable htget_New_Erros = new Hashtable();
            DataTable dtget_New_Errors = new DataTable();

            htget_New_Erros.Add("@Trans", "GET_NEW_ERRORS_REPORTINGTO_ERRORONUSER_WISE_ERROR_FROM");
            htget_New_Erros.Add("@Reporting_User_Id", reportingTo);
            htget_New_Erros.Add("@Error_On_User_Id", errorOnUser);
            htget_New_Erros.Add("@External_Error", errorFrom);
            dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);
            if (dtget_New_Errors.Rows.Count > 0)
            {
                Grd_New_Errors.Rows.Clear();
                for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                {
                    Grd_New_Errors.Rows.Add();
                    Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                    Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                    Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                    Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                    Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                    Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                    Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                    Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();
                    if (User_Role == 2)
                    {
                        Grd_New_Errors.Columns[14].Visible = false;
                        Grd_New_Errors.Columns[15].Visible = false;
                    }
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grd_New_Errors.Rows.Clear();
            }
        }
        private void BindReportingWiseErrorsFrom(object reportingTo, object errorFrom)
        {
            Hashtable htget_New_Erros = new Hashtable();
            DataTable dtget_New_Errors = new DataTable();

            htget_New_Erros.Add("@Trans", "GET_NEW_ERRORS_REPORTING_WISE_ERROR_FROM");
            htget_New_Erros.Add("@Reporting_User_Id", reportingTo);
            htget_New_Erros.Add("@External_Error", errorFrom);
            dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);
            if (dtget_New_Errors.Rows.Count > 0)
            {
                Grd_New_Errors.Rows.Clear();
                for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                {
                    Grd_New_Errors.Rows.Add();
                    Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                    Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                    Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                    Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                    Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                    Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                    Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                    Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();
                    if (User_Role == 2)
                    {
                        Grd_New_Errors.Columns[14].Visible = false;
                        Grd_New_Errors.Columns[15].Visible = false;
                    }
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grd_New_Errors.Rows.Clear();
            }
        }
        private void Bind_NewErrorsUser_By_ReportingTo_Error_On_User_Wise(int Reporting_User_Id, int Error_On_User_Id)
        {
            Hashtable htget_New_Erros = new Hashtable();
            DataTable dtget_New_Errors = new DataTable();

            htget_New_Erros.Add("@Trans", "GET_NEW_ERRORS_REPORTINGTO_ERRORONUSER_WISE");
            htget_New_Erros.Add("@Reporting_User_Id", Reporting_User_Id);
            htget_New_Erros.Add("@Error_On_User_Id", Error_On_User_Id);
            dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);
            if (dtget_New_Errors.Rows.Count > 0)
            {
                Grd_New_Errors.Rows.Clear();
                for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                {
                    Grd_New_Errors.Rows.Add();
                    Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                    Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                    Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                    Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                    Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                    Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                    Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                    Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();

                    if (User_Role == 2)
                    {
                        Grd_New_Errors.Columns[14].Visible = false;
                        Grd_New_Errors.Columns[15].Visible = false;
                    }
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grd_New_Errors.Rows.Clear();
            }
        }
        private void Bind_User_New_Errors_Users_reporting_Wise(int Reporting_User_Id)
        {
            Hashtable htget_New_Erros = new Hashtable();
            DataTable dtget_New_Errors = new DataTable();

            htget_New_Erros.Add("@Trans", "GET_NEW_ERRORS_REPORTING_WISE");
            htget_New_Erros.Add("@Reporting_User_Id", Reporting_User_Id);
            dtget_New_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_New_Erros);
            if (dtget_New_Errors.Rows.Count > 0)
            {
                Grd_New_Errors.Rows.Clear();
                for (int i = 0; i < dtget_New_Errors.Rows.Count; i++)
                {
                    Grd_New_Errors.Rows.Add();
                    Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grd_New_Errors.Rows[i].Cells[2].Value = dtget_New_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                    Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                    Grd_New_Errors.Rows[i].Cells[5].Value = dtget_New_Errors.Rows[i]["Work_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[6].Value = dtget_New_Errors.Rows[i]["Error_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[7].Value = dtget_New_Errors.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                    Grd_New_Errors.Rows[i].Cells[8].Value = dtget_New_Errors.Rows[i]["Error_Type"].ToString();
                    Grd_New_Errors.Rows[i].Cells[9].Value = dtget_New_Errors.Rows[i]["Error_description"].ToString();
                    Grd_New_Errors.Rows[i].Cells[10].Value = dtget_New_Errors.Rows[i]["Comments"].ToString();
                    Grd_New_Errors.Rows[i].Cells[11].Value = dtget_New_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[12].Value = dtget_New_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[13].Value = dtget_New_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grd_New_Errors.Rows[i].Cells[14].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_New_Errors.Rows[i].Cells[15].Value = dtget_New_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grd_New_Errors.Rows[i].Cells[16].Value = dtget_New_Errors.Rows[i]["Entered_Date"].ToString();
                    Grd_New_Errors.Rows[i].Cells[17].Value = dtget_New_Errors.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[18].Value = dtget_New_Errors.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[19].Value = dtget_New_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_New_Errors.Rows[i].Cells[20].Value = dtget_New_Errors.Rows[i]["Order_Id"].ToString();
                    Grd_New_Errors.Rows[i].Cells[21].Value = dtget_New_Errors.Rows[i]["Reporting_1"].ToString();
                    Grd_New_Errors.Rows[i].Cells[22].Value = dtget_New_Errors.Rows[i]["Reporting_2"].ToString();

                    if (User_Role == 2)
                    {
                        Grd_New_Errors.Columns[14].Visible = false;
                        Grd_New_Errors.Columns[15].Visible = false;
                    }
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grd_New_Errors.Rows.Clear();
            }
        }
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Erro_Dashboard_Count();
            if (User_Role == 2)
            {
                My_New_Errors();
                ddl_NewErrors_Error_On_User.Visible = false;
                btn_Export_New_Errors.Visible = true;
                btn_All_User_Errors.Visible = false;
                btn_My_Errors.Visible = true;
                pnl_New_Error_Reporting_To.Visible = false;

                lbl_Dispute_ErrorOnUser.Visible = false;
                ddl_Dispute_ErrorOnUser.Visible = false;

                lbl_Error_User_Name.Visible = false;
                ddl_Error_Report_ErrorOnUser.Visible = false;

                lblErrorFrom.Visible = false;
                ddlReportsErrorFrom.Visible = false;

            }
            else
            {
                btn_All_User_Errors.Visible = true;

                if (tabControl1.SelectedIndex == 0)
                {
                    btn_All_User_Errors_Click(sender, e);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    btn_All_Dispute_Click(sender, e);
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    btn_All_Employee_Report_Click(sender, e);
                    btn_Accepted_Error_Submit_Click(sender, e);
                }
            }
            //tab1
            ddl_New_Error_Reporting_User_Name.SelectedIndex = 0;
            // ddl_NewErrors_Error_On_User.SelectedIndex = 0;

            //tab2
            ddl_Dispute_Reporting_Username.SelectedIndex = 0;
            //ddl_Dispute_ErrorOnUser.SelectedIndex = 0;

            //tab3
            ddl_Errors_Reporting_User_Name.SelectedIndex = 0;
            //  ddl_Error_Report_ErrorOnUser.SelectedIndex = 0;


        }
        private void btn_All_Dispute_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            ddl_Dispute_Reporting_Username.SelectedIndex = 0;
            //ddl_Dispute_ErrorOnUser.SelectedIndex = 0;

            tbl_Layout_Dispute_Accept.Visible = true;
            pnl_Dispute_Dates.Visible = false;
            lbl_Dispute_Reporting_To.Visible = true;
            ddl_Dispute_Reporting_Username.Visible = true;
            Grid_Disputed_Errors.Rows.Clear();
            Bind_All_Disputes(0);

            lbl_Dispute_Error_Header.Text = "All Dispute Errors";

            lbl_Dispute_ErrorOnUser.Visible = true;
            ddl_Dispute_ErrorOnUser.Visible = true;
        }
        private void Bind_All_Disputes(int Reporting_User_Id)
        {
            Hashtable ht_All_Disputes = new Hashtable();
            DataTable dt_All_Disputes = new DataTable();
            if (Reporting_User_Id == 0)
            {
                ht_All_Disputes.Add("@Trans", "GET_ALL_EMPLOYEE_WISE_REJECTED_ERRORS");
            }
            else
            {
                ht_All_Disputes.Add("@Trans", "GET_ALL_EMPLOYEE_REPORTING_WISE_REJECTED_ERRORS");
            }
            ht_All_Disputes.Add("@Reporting_User_Id", Reporting_User_Id);
            dt_All_Disputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_All_Disputes);
            if (dt_All_Disputes.Rows.Count > 0)
            {
                Grid_Disputed_Errors.Rows.Clear();
                for (int i = 0; i < dt_All_Disputes.Rows.Count; i++)
                {
                    Grid_Disputed_Errors.Rows.Add();
                    Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_All_Disputes.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                    Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                    Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_All_Disputes.Rows[i]["Work_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_All_Disputes.Rows[i]["Error_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_All_Disputes.Rows[i]["New_Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_All_Disputes.Rows[i]["Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_All_Disputes.Rows[i]["Error_description"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_All_Disputes.Rows[i]["Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_All_Disputes.Rows[i]["Error_On_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_All_Disputes.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_All_Disputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_All_Disputes.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_All_Disputes.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_All_Disputes.Rows[i]["Entered_Date"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_All_Disputes.Rows[i]["User_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_All_Disputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_All_Disputes.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_All_Disputes.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Order_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_All_Disputes.Rows[i]["Error_On_User_Id"].ToString();
                    //  Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["Error_Type_Id"].ToString();
                    //  Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Error_description_Id"].ToString();

                    Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_All_Disputes.Rows[i]["Reporting_1"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_All_Disputes.Rows[i]["Reporting_2"].ToString();


                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Disputed_Errors.Columns[15].Visible = false;
                        Grid_Disputed_Errors.Columns[16].Visible = false;
                        Grid_Disputed_Errors.Columns[4].Visible = false;
                        Grid_Disputed_Errors.Columns[5].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grid_Disputed_Errors.Rows.Clear();
            }
        }
        private void Grid_Disputed_Errors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    Order_Entry Order_Entry = new Order_Entry(int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[23].Value.ToString()), User_Id, User_Role.ToString(), Production_Date);
                    Order_Entry.Show();
                }
                if (e.ColumnIndex == 3)
                {

                    Error_Documents Error_Doc_View = new Error_Documents(int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[23].Value.ToString()), User_Role, int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[22].Value.ToString()), User_Id, Grid_Disputed_Errors.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Error_Doc_View.Show();
                }
                if (e.ColumnIndex == 4)
                {

                    Error_History Error_Hist_View = new Error_History(int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[23].Value.ToString()), int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[22].Value.ToString()), Grid_Disputed_Errors.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Error_Hist_View.Show();
                }
                if (e.ColumnIndex == 5)
                {

                    string Ordertask = Grid_Disputed_Errors.Rows[e.RowIndex].Cells[12].Value.ToString();
                    string ordreno = Grid_Disputed_Errors.Rows[e.RowIndex].Cells[2].Value.ToString();
                    int erroinfotypeid = int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[22].Value.ToString());
                    int orderid = int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[23].Value.ToString());
                    int userid = int.Parse(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[24].Value.ToString());

                    string wrktype = "Super QC";
                    if (worktypename == "SuperQC")
                    {
                        worktypename = wrktype;
                    }

                    Hashtable ht_getwrktype = new Hashtable();
                    DataTable dt_getwrktype = new DataTable();
                    ht_getwrktype.Add("@Trans", "GET_WORKTYPEID");
                    ht_getwrktype.Add("@Order_Work_Type", worktypename);
                    dt_getwrktype = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getwrktype);
                    if (dt_getwrktype.Rows.Count > 0)
                    {
                        workttype = int.Parse(dt_getwrktype.Rows[0]["Order_Wok_Type_ID"].ToString());
                    }

                    Employee_Error_Entry Emp_Error_entry_View = new Employee_Error_Entry(userid, User_Role.ToString(), Ordertask, orderid, 3, workttype, ordreno, Production_Date, erroinfotypeid, 0);
                    Emp_Error_entry_View.Show();
                }

                if (e.ColumnIndex == 11)
                {
                    if (!string.IsNullOrEmpty(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[11].Value.ToString()))
                    {
                        ErrorComments comments = new ErrorComments(Grid_Disputed_Errors.Rows[e.RowIndex].Cells[11].Value.ToString());
                        this.Invoke(new MethodInvoker(delegate
                        {
                            comments.Show();
                        }));
                    }
                }
            }
        }
        private void btn_Approve_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("do you Want to Dsipute?", "Disputed Errors", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {

                    if (Validate_Dispute_Error() != false)
                    {
                        int Reject_Count = 0;

                        for (int i = 0; i < Grid_Disputed_Errors.Rows.Count; i++)
                        {
                            bool isChecked = (bool)Grid_Disputed_Errors[0, i].FormattedValue;

                            if (isChecked == true)
                            {
                                Reject_Count = 1;
                                int Error_Info_Id = int.Parse(Grid_Disputed_Errors.Rows[i].Cells[22].Value.ToString());
                                int Order_Id = int.Parse(Grid_Disputed_Errors.Rows[i].Cells[23].Value.ToString());

                                Hashtable htupdate_Erro_Infp = new Hashtable();
                                DataTable dtupdate_Error_Info = new System.Data.DataTable();

                                htupdate_Erro_Infp.Add("@Trans", "UPDATE_ERROR_MANAGER_COMMENTS");
                                htupdate_Erro_Infp.Add("@Manager_Supervisor_Comments", txt_Manager_Reject_Comments.Text.Trim().ToString());
                                htupdate_Erro_Infp.Add("@Manager_User_Id", User_Id);
                                htupdate_Erro_Infp.Add("@Manger_Error_Status", 1);
                                htupdate_Erro_Infp.Add("@ErrorInfo_ID", Error_Info_Id);
                                dtupdate_Error_Info = dataaccess.ExecuteSP("Sp_Error_Info", htupdate_Erro_Infp);


                                Hashtable hterror_history = new Hashtable();
                                DataTable dterror_history = new DataTable();
                                hterror_history.Add("@Trans", "INSERT");
                                hterror_history.Add("@Order_Id", Order_Id);
                                hterror_history.Add("@Error_Info_Id", Error_Info_Id);
                                hterror_history.Add("@Comments", "Error Disputed By Supervisor/Manager");
                                hterror_history.Add("@User_Id", User_Id);
                                dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);

                            }

                        }
                        if (Reject_Count >= 1)
                        {
                            txt_Manager_Reject_Comments.Text = "";
                            Bind_All_Disputes(User_Id);
                            MessageBox.Show("Errors Were Disputed Sucessfully, These Will Move to Disputed Error Queue");

                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Problem in With Disputing Error;Please Contact Administrator");
                }
            }
            else if (dialogResult == DialogResult.No)
            {


            }

        }
        private void BindDisputeErrors()
        {
            try
            {
                load_Progressbar.Start_progres();
                if (ddl_Dispute_Reporting_Username.SelectedIndex == 0 && ddl_Dispute_ErrorOnUser.SelectedIndex == 0 && ddlDisputeErrorFrom.SelectedIndex == 0)
                {
                    Bind_All_Disputes(0);
                }
                if (ddl_Dispute_Reporting_Username.SelectedIndex > 0 && ddl_Dispute_ErrorOnUser.SelectedIndex == 0 && ddlDisputeErrorFrom.SelectedIndex == 0)
                {
                    Bind_All_Disputes(int.Parse(ddl_Dispute_Reporting_Username.SelectedValue.ToString()));
                }
                if (ddl_Dispute_Reporting_Username.SelectedIndex > 0 && ddl_Dispute_ErrorOnUser.SelectedIndex > 0 && ddlDisputeErrorFrom.SelectedIndex == 0)
                {
                    Bind_FilterDisputes_By_Reporting_ErrorOnUser(int.Parse(ddl_Dispute_Reporting_Username.SelectedValue.ToString()), int.Parse(ddl_Dispute_ErrorOnUser.SelectedValue.ToString()));
                }

                if (ddl_Dispute_Reporting_Username.SelectedIndex > 0 && ddl_Dispute_ErrorOnUser.SelectedIndex == 0 && ddlDisputeErrorFrom.SelectedIndex > 0)
                {
                    BindDisputeReportingWiseErrorFrom(ddl_Dispute_Reporting_Username.SelectedValue, ddlDisputeErrorFrom.SelectedValue);
                }
                if (ddl_Dispute_Reporting_Username.SelectedIndex > 0 && ddl_Dispute_ErrorOnUser.SelectedIndex > 0 && ddlDisputeErrorFrom.SelectedIndex > 0)
                {
                    BindDisputeReportingWiseUserWiseErrorFrom(ddl_Dispute_Reporting_Username.SelectedValue, ddl_Dispute_ErrorOnUser.SelectedValue, ddlDisputeErrorFrom.SelectedValue);
                }
                if (ddl_Dispute_Reporting_Username.SelectedIndex == 0 && ddl_Dispute_ErrorOnUser.SelectedIndex > 0 && ddlDisputeErrorFrom.SelectedIndex == 0)
                {
                    Bind_AllDisputes_ErrorOnUser_Wise_Filter(int.Parse(ddl_Dispute_ErrorOnUser.SelectedValue.ToString()));
                }
                if (ddl_Dispute_Reporting_Username.SelectedIndex == 0 && ddl_Dispute_ErrorOnUser.SelectedIndex > 0 && ddlDisputeErrorFrom.SelectedIndex > 0)
                {
                    BindDisputeErrorsUserWiseErrorFrom(ddl_Dispute_ErrorOnUser.SelectedValue, ddlDisputeErrorFrom.SelectedValue);
                }
                if (ddl_Dispute_Reporting_Username.SelectedIndex == 0 && ddl_Dispute_ErrorOnUser.SelectedIndex == 0 && ddlDisputeErrorFrom.SelectedIndex > 0)
                {
                    BindDisputeErrorsFrom(ddlDisputeErrorFrom.SelectedValue);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void BindDisputeReportingWiseErrorFrom(object reportingTo, object errorFrom)
        {
            Hashtable ht_All_Disputes = new Hashtable();
            DataTable dt_All_Disputes = new DataTable();

            ht_All_Disputes.Add("@Trans", "GET_ALL_EMPLOYEE_REPORTING_WISE_REJECTED_ERRORS_ERROR_FROM");
            ht_All_Disputes.Add("@Reporting_User_Id", reportingTo);
            ht_All_Disputes.Add("@External_Error", errorFrom);
            dt_All_Disputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_All_Disputes);
            if (dt_All_Disputes.Rows.Count > 0)
            {
                Grid_Disputed_Errors.Rows.Clear();
                for (int i = 0; i < dt_All_Disputes.Rows.Count; i++)
                {
                    Grid_Disputed_Errors.Rows.Add();
                    Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_All_Disputes.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                    Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                    Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_All_Disputes.Rows[i]["Work_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_All_Disputes.Rows[i]["Error_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_All_Disputes.Rows[i]["New_Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_All_Disputes.Rows[i]["Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_All_Disputes.Rows[i]["Error_description"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_All_Disputes.Rows[i]["Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_All_Disputes.Rows[i]["Error_On_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_All_Disputes.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_All_Disputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_All_Disputes.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_All_Disputes.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_All_Disputes.Rows[i]["Entered_Date"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_All_Disputes.Rows[i]["User_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_All_Disputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_All_Disputes.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_All_Disputes.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Order_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_All_Disputes.Rows[i]["Error_On_User_Id"].ToString();
                    //  Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["Error_Type_Id"].ToString();
                    //  Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Error_description_Id"].ToString();

                    Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_All_Disputes.Rows[i]["Reporting_1"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_All_Disputes.Rows[i]["Reporting_2"].ToString();


                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Disputed_Errors.Columns[15].Visible = false;
                        Grid_Disputed_Errors.Columns[16].Visible = false;
                        Grid_Disputed_Errors.Columns[4].Visible = false;
                        Grid_Disputed_Errors.Columns[5].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grid_Disputed_Errors.Rows.Clear();
            }
        }
        private void BindDisputeErrorsFrom(object errorFrom)
        {
            Hashtable ht_AllDisputes = new Hashtable();
            DataTable dt_AllDisputes = new DataTable();
            ht_AllDisputes.Add("@Trans", "DISPUTE_ERROR_FROM");
            ht_AllDisputes.Add("@External_Error", errorFrom);

            dt_AllDisputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_AllDisputes);
            if (dt_AllDisputes.Rows.Count > 0)
            {
                Grid_Disputed_Errors.Rows.Clear();
                for (int i = 0; i < dt_AllDisputes.Rows.Count; i++)
                {
                    Grid_Disputed_Errors.Rows.Add();
                    Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_AllDisputes.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                    Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                    Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_AllDisputes.Rows[i]["Work_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_AllDisputes.Rows[i]["Error_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_AllDisputes.Rows[i]["New_Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_AllDisputes.Rows[i]["Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_AllDisputes.Rows[i]["Error_description"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_AllDisputes.Rows[i]["Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_AllDisputes.Rows[i]["Error_On_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_AllDisputes.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_AllDisputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_AllDisputes.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_AllDisputes.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_AllDisputes.Rows[i]["Entered_Date"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_AllDisputes.Rows[i]["User_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_AllDisputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_AllDisputes.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_AllDisputes.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_AllDisputes.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_AllDisputes.Rows[i]["Order_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_AllDisputes.Rows[i]["Error_On_User_Id"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_AllDisputes.Rows[i]["Reporting_1"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_AllDisputes.Rows[i]["Reporting_2"].ToString();

                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_AllDisputes.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Disputed_Errors.Columns[15].Visible = false;
                        Grid_Disputed_Errors.Columns[16].Visible = false;
                        Grid_Disputed_Errors.Columns[4].Visible = false;
                        Grid_Disputed_Errors.Columns[5].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {
                Grid_Disputed_Errors.Rows.Clear();

            }
        }
        private void BindDisputeErrorsUserWiseErrorFrom(object errorUser, object errorFrom)
        {
            Hashtable ht_AllDisputes = new Hashtable();
            DataTable dt_AllDisputes = new DataTable();

            ht_AllDisputes.Add("@Trans", "SEARCH_DISPUTE_ERROR_BY_ERRORON_USER_ERROR_FROM");
            ht_AllDisputes.Add("@User_Id", errorUser);
            ht_AllDisputes.Add("@External_Error", errorFrom);

            dt_AllDisputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_AllDisputes);
            if (dt_AllDisputes.Rows.Count > 0)
            {
                Grid_Disputed_Errors.Rows.Clear();
                for (int i = 0; i < dt_AllDisputes.Rows.Count; i++)
                {
                    Grid_Disputed_Errors.Rows.Add();
                    Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_AllDisputes.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                    Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                    Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_AllDisputes.Rows[i]["Work_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_AllDisputes.Rows[i]["Error_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_AllDisputes.Rows[i]["New_Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_AllDisputes.Rows[i]["Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_AllDisputes.Rows[i]["Error_description"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_AllDisputes.Rows[i]["Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_AllDisputes.Rows[i]["Error_On_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_AllDisputes.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_AllDisputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_AllDisputes.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_AllDisputes.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_AllDisputes.Rows[i]["Entered_Date"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_AllDisputes.Rows[i]["User_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_AllDisputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_AllDisputes.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_AllDisputes.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_AllDisputes.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_AllDisputes.Rows[i]["Order_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_AllDisputes.Rows[i]["Error_On_User_Id"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_AllDisputes.Rows[i]["Reporting_1"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_AllDisputes.Rows[i]["Reporting_2"].ToString();

                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_AllDisputes.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Disputed_Errors.Columns[15].Visible = false;
                        Grid_Disputed_Errors.Columns[16].Visible = false;
                        Grid_Disputed_Errors.Columns[4].Visible = false;
                        Grid_Disputed_Errors.Columns[5].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {
                Grid_Disputed_Errors.Rows.Clear();

            }
        }
        private void BindDisputeReportingWiseUserWiseErrorFrom(object reportingTo, object errorUser, object errorFrom)
        {
            Hashtable ht_All_Disputes = new Hashtable();
            DataTable dt_All_Disputes = new System.Data.DataTable();

            ht_All_Disputes.Add("@Trans", "FILTER_BY_REPORTINGTO_ERROR_ON_USER_REJECTED_ERRORS_ERROR_FROM");
            ht_All_Disputes.Add("@Reporting_User_Id", reportingTo);
            ht_All_Disputes.Add("@Error_On_User_Id", errorUser);
            ht_All_Disputes.Add("@External_Error", errorFrom);
            dt_All_Disputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_All_Disputes);
            if (dt_All_Disputes.Rows.Count > 0)
            {
                Grid_Disputed_Errors.Rows.Clear();
                for (int i = 0; i < dt_All_Disputes.Rows.Count; i++)
                {
                    Grid_Disputed_Errors.Rows.Add();
                    Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_All_Disputes.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                    Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                    Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_All_Disputes.Rows[i]["Work_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_All_Disputes.Rows[i]["Error_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_All_Disputes.Rows[i]["New_Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_All_Disputes.Rows[i]["Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_All_Disputes.Rows[i]["Error_description"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_All_Disputes.Rows[i]["Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_All_Disputes.Rows[i]["Error_On_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_All_Disputes.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_All_Disputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_All_Disputes.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_All_Disputes.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_All_Disputes.Rows[i]["Entered_Date"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_All_Disputes.Rows[i]["User_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_All_Disputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_All_Disputes.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_All_Disputes.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Order_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_All_Disputes.Rows[i]["Error_On_User_Id"].ToString();

                    //  Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["Error_Type_Id"].ToString();
                    //  Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Error_description_Id"].ToString();

                    Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_All_Disputes.Rows[i]["Reporting_1"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_All_Disputes.Rows[i]["Reporting_2"].ToString();


                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {

                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {

                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                    }


                    if (User_Role == 2)
                    {
                        Grid_Disputed_Errors.Columns[15].Visible = false;
                        Grid_Disputed_Errors.Columns[16].Visible = false;
                        Grid_Disputed_Errors.Columns[4].Visible = false;
                        Grid_Disputed_Errors.Columns[5].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {
                Grid_Disputed_Errors.Rows.Clear();

            }

        }
        private void ddl_Dispute_Reporting_Username_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindDisputeErrors();
        }
        private void ddl_Dispute_ErrorOnUser_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindDisputeErrors();
        }
        private void Bind_FilterDisputes_By_Reporting_ErrorOnUser(int Reporting_User_Id, int Error_On_User_Id)
        {

            Hashtable ht_All_Disputes = new Hashtable();
            DataTable dt_All_Disputes = new System.Data.DataTable();

            ht_All_Disputes.Add("@Trans", "FILTER_BY_REPORTINGTO_ERROR_ON_USER_REJECTED_ERRORS");
            ht_All_Disputes.Add("@Reporting_User_Id", Reporting_User_Id);
            ht_All_Disputes.Add("@Error_On_User_Id", Error_On_User_Id);
            dt_All_Disputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_All_Disputes);
            if (dt_All_Disputes.Rows.Count > 0)
            {
                Grid_Disputed_Errors.Rows.Clear();
                for (int i = 0; i < dt_All_Disputes.Rows.Count; i++)
                {
                    Grid_Disputed_Errors.Rows.Add();
                    Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_All_Disputes.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                    Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                    Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_All_Disputes.Rows[i]["Work_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_All_Disputes.Rows[i]["Error_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_All_Disputes.Rows[i]["New_Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_All_Disputes.Rows[i]["Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_All_Disputes.Rows[i]["Error_description"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_All_Disputes.Rows[i]["Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_All_Disputes.Rows[i]["Error_On_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_All_Disputes.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_All_Disputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_All_Disputes.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_All_Disputes.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_All_Disputes.Rows[i]["Entered_Date"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_All_Disputes.Rows[i]["User_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_All_Disputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_All_Disputes.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_All_Disputes.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Order_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_All_Disputes.Rows[i]["Error_On_User_Id"].ToString();

                    //  Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_All_Disputes.Rows[i]["Error_Type_Id"].ToString();
                    //  Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_All_Disputes.Rows[i]["Error_description_Id"].ToString();

                    Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_All_Disputes.Rows[i]["Reporting_1"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_All_Disputes.Rows[i]["Reporting_2"].ToString();


                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_All_Disputes.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {

                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {

                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                    }


                    if (User_Role == 2)
                    {
                        Grid_Disputed_Errors.Columns[15].Visible = false;
                        Grid_Disputed_Errors.Columns[16].Visible = false;
                        Grid_Disputed_Errors.Columns[4].Visible = false;
                        Grid_Disputed_Errors.Columns[5].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {
                Grid_Disputed_Errors.Rows.Clear();

            }


        }
        private void ddlDisputeErrorFrom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindDisputeErrors();
        }
        private void btn_Dispute_Vew_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            Bind_User_Wise_Disputes();
        }
        private void btn_My_Error_Report_Click(object sender, EventArgs e)
        {
            Error_Report = 1;

            lbl_Error_Reporting_To.Visible = false;
            ddl_Errors_Reporting_User_Name.Visible = false;
            ddl_Errors_Reporting_User_Name.SelectedIndex = 0;

            lbl_Error_User_Name.Visible = false;
            ddl_Error_Report_ErrorOnUser.Visible = false;

            lblErrorFrom.Visible = false;
            ddlReportsErrorFrom.Visible = false;

            lbl_Error_Reprt_Header.Text = "My Error Report";
            Grid_Error.Rows.Clear();
        }
        private void btn_All_Employee_Report_Click(object sender, EventArgs e)
        {
            Error_Report = 2;

            lbl_Error_Reporting_To.Visible = true;
            ddl_Errors_Reporting_User_Name.Visible = true;
            ddl_Errors_Reporting_User_Name.SelectedIndex = 0;

            lbl_Error_User_Name.Visible = true;
            ddl_Error_Report_ErrorOnUser.Visible = true;

            lblErrorFrom.Visible = true;
            ddlReportsErrorFrom.Visible = true;

            lbl_Error_Reprt_Header.Text = "All Error Report";
            Grid_Error.Rows.Clear();
        }
        private void Grid_Error_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {

                    Order_Entry Order_Entry = new Order_Entry(int.Parse(Grid_Error.Rows[e.RowIndex].Cells[26].Value.ToString()), User_Id, User_Role.ToString(), Production_Date);
                    Order_Entry.Show();
                }
                if (e.ColumnIndex == 2)
                {

                    Error_Documents Error_Doc_View = new Error_Documents(int.Parse(Grid_Error.Rows[e.RowIndex].Cells[26].Value.ToString()), User_Role, int.Parse(Grid_Error.Rows[e.RowIndex].Cells[25].Value.ToString()), User_Id, Grid_Error.Rows[e.RowIndex].Cells[1].Value.ToString());
                    Error_Doc_View.Show();
                }
                if (e.ColumnIndex == 3)
                {

                    Error_History Error_Hist_View = new Error_History(int.Parse(Grid_Error.Rows[e.RowIndex].Cells[26].Value.ToString()), int.Parse(Grid_Error.Rows[e.RowIndex].Cells[25].Value.ToString()), Grid_Error.Rows[e.RowIndex].Cells[1].Value.ToString());
                    Error_Hist_View.Show();
                }

                if (e.ColumnIndex == 4)
                {
                    string Order_task = Grid_Error.Rows[e.RowIndex].Cells[11].Value.ToString();
                    string ordre_no = Grid_Error.Rows[e.RowIndex].Cells[1].Value.ToString();
                    int erro_info_id = int.Parse(Grid_Error.Rows[e.RowIndex].Cells[25].Value.ToString());
                    int order_id = int.Parse(Grid_Error.Rows[e.RowIndex].Cells[26].Value.ToString());
                    int user_id = int.Parse(Grid_Error.Rows[e.RowIndex].Cells[27].Value.ToString());



                    string wrktype = "Super QC";
                    if (worktypename == "SuperQC")
                    {
                        worktypename = wrktype;
                    }

                    Hashtable ht_getwrktype = new Hashtable();
                    DataTable dt_getwrktype = new DataTable();
                    ht_getwrktype.Add("@Trans", "GET_WORKTYPEID");
                    ht_getwrktype.Add("@Order_Work_Type", worktypename);
                    dt_getwrktype = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_getwrktype);
                    if (dt_getwrktype.Rows.Count > 0)
                    {
                        workttype = int.Parse(dt_getwrktype.Rows[0]["Order_Wok_Type_ID"].ToString());

                    }
                    Ordermanagement_01.Employee_Error_Entry Emp_Error_entry_View = new Ordermanagement_01.Employee_Error_Entry(user_id, User_Role.ToString(), Order_task, order_id, 3, workttype, ordre_no, Production_Date, erro_info_id, 0);
                    Emp_Error_entry_View.Show();

                }
                if (e.ColumnIndex == 10)
                {
                    if (!string.IsNullOrEmpty(Grid_Error.Rows[e.RowIndex].Cells[10].Value.ToString()))
                    {
                        ErrorComments comments = new ErrorComments(Grid_Error.Rows[e.RowIndex].Cells[10].Value.ToString());
                        this.Invoke(new MethodInvoker(delegate
                        {
                            comments.Show();
                        }));
                    }
                }
            }
        }
        private void btn_Dispute_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("do you Want to Reject?", "Disputed Errors", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {

                    if (Validate_Dispute_Error() != false)
                    {
                        int Reject_Count = 0;
                        for (int i = 0; i < Grid_Disputed_Errors.Rows.Count; i++)
                        {
                            bool isChecked = (bool)Grid_Disputed_Errors[0, i].FormattedValue;

                            if (isChecked == true)
                            {
                                Reject_Count = 1;
                                int Error_Info_Id = int.Parse(Grid_Disputed_Errors.Rows[i].Cells[22].Value.ToString());
                                int Order_Id = int.Parse(Grid_Disputed_Errors.Rows[i].Cells[23].Value.ToString());


                                Hashtable htupdate_Erro_Infp = new Hashtable();
                                DataTable dtupdate_Error_Info = new System.Data.DataTable();

                                htupdate_Erro_Infp.Add("@Trans", "UPDATE_ERROR_MANAGER_COMMENTS");
                                htupdate_Erro_Infp.Add("@Manager_Supervisor_Comments", txt_Manager_Reject_Comments.Text.Trim().ToString());
                                htupdate_Erro_Infp.Add("@Manager_User_Id", User_Id);
                                htupdate_Erro_Infp.Add("@Manger_Error_Status", 2);
                                htupdate_Erro_Infp.Add("@ErrorInfo_ID", Error_Info_Id);
                                dtupdate_Error_Info = dataaccess.ExecuteSP("Sp_Error_Info", htupdate_Erro_Infp);



                                Hashtable hterror_history = new Hashtable();
                                DataTable dterror_history = new DataTable();
                                hterror_history.Add("@Trans", "INSERT");
                                hterror_history.Add("@Order_Id", Order_Id);
                                hterror_history.Add("@Error_Info_Id", Error_Info_Id);
                                hterror_history.Add("@Comments", "Error Rejected By Supervisor/Manager");
                                hterror_history.Add("@User_Id", User_Id);
                                dterror_history = dataaccess.ExecuteSP("Sp_Error_Info_History", hterror_history);


                            }

                        }
                        if (Reject_Count >= 1)
                        {
                            txt_Manager_Reject_Comments.Text = "";
                            Bind_All_Disputes(User_Id);
                            MessageBox.Show("Errors Were Rejected Sucessfully");

                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Problem in With Rejecting Error;Please Contact Administrator");
                }
            }
            else if (dialogResult == DialogResult.No)
            {


            }
        }
        private void Export_New_Error_ReportData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Clear();

                //Adding the Columns
                foreach (DataGridViewColumn column in Grd_New_Errors.Columns)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }
                    }

                }

                //Adding the Rows
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                    }
                }

                string Export_Title_Name = "New_Errors";
                //Exporting to Excel
                string folderPath = "C:\\Temp\\";
                Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, Export_Title_Name.ToString());
                    try
                    {
                        wb.SaveAs(Path1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("File is Opened, Please Close and Export it");
                    }
                }
                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception e)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void Export_Disputed_Error_ReportData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Clear();

                //Adding the Columns
                foreach (DataGridViewColumn column in Grid_Disputed_Errors.Columns)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }
                    }

                }

                //Adding the Rows
                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            if (User_Role == 2 || User_Role == 3)
                            {
                                if (cell.ColumnIndex == 12 || cell.ColumnIndex == 13)
                                {
                                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = "";
                                }
                                else
                                {
                                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                                }
                            }
                            else
                            {

                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                            }
                        }
                    }
                }

                string Export_Title_Name = "Disputed_Errors";
                //Exporting to Excel
                string folderPath = "C:\\Temp\\";
                Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, Export_Title_Name.ToString());
                    try
                    {
                        wb.SaveAs(Path1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("File is Opened, Please Close and Export it");
                    }
                }
                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception e)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void Export_All_Error_ReportData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Clear();
                //Adding the Columns
                foreach (DataGridViewColumn column in Grid_Error.Columns)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }
                    }
                }

                //Adding the Rows
                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            if (User_Role == 2)
                            {
                                if (cell.ColumnIndex == 11 || cell.ColumnIndex == 12 || cell.ColumnIndex == 20 || cell.ColumnIndex == 21)
                                {
                                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = "";
                                }
                                else
                                {
                                    dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                                }
                            }
                            else
                            {
                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                            }
                        }
                    }
                }

                string Export_Title_Name = "All_Errors";
                //Exporting to Excel
                string folderPath = "C:\\Temp\\";
                Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, Export_Title_Name.ToString());
                    try
                    {
                        wb.SaveAs(Path1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("File is Opened, Please Close and Export it");
                    }
                }
                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception e)
            {
                MessageBox.Show("something went wrong contact admin");
            }

        }
        private void btn_Export_New_Errors_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (Grd_New_Errors.Rows.Count > 0)
            {
                Export_New_Error_ReportData();
            }
            else
            {
                MessageBox.Show("No Data Found to Export");
            }
        }
        private void btn_Dispute_Export_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (Grid_Disputed_Errors.Rows.Count > 0)
            {
                Export_Disputed_Error_ReportData();
            }
            else
            {
                MessageBox.Show("No Data Found to Export");
            }
        }
        private void btn_All_Error_Report_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (Grid_Error.Rows.Count > 0)
            {
                Export_All_Error_ReportData();
            }
            else
            {
                MessageBox.Show("No Data Found to Export");
            }
        }
        private void Bind_NewErrors_Error_On_User_Wise(int Error_On_User_ID)
        {
            try
            {
                Hashtable ht_ErroOnUser_List = new Hashtable();
                DataTable dt_ErroOnUser_List = new DataTable();

                //01-05-2018
                ht_ErroOnUser_List.Add("@Trans", "SELECT_BY_ERROR_ON_USER");
                ht_ErroOnUser_List.Add("@Error_On_User_Id", Error_On_User_ID);
                dt_ErroOnUser_List = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_ErroOnUser_List);

                if (dt_ErroOnUser_List.Rows.Count > 0)
                {
                    Grd_New_Errors.Rows.Clear();

                    for (int i = 0; i < dt_ErroOnUser_List.Rows.Count; i++)
                    {
                        Grd_New_Errors.Rows.Add();
                        Grd_New_Errors.Rows[i].Cells[1].Value = i + 1;
                        Grd_New_Errors.Rows[i].Cells[2].Value = dt_ErroOnUser_List.Rows[i]["Client_Order_Number"].ToString();

                        Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                        Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                        Grd_New_Errors.Rows[i].Cells[5].Value = dt_ErroOnUser_List.Rows[i]["Work_Type"].ToString();
                        Grd_New_Errors.Rows[i].Cells[6].Value = dt_ErroOnUser_List.Rows[i]["Error_From"].ToString();
                        Grd_New_Errors.Rows[i].Cells[7].Value = dt_ErroOnUser_List.Rows[i]["New_Error_Type"].ToString(); // 30-04/2018
                        Grd_New_Errors.Rows[i].Cells[8].Value = dt_ErroOnUser_List.Rows[i]["Error_Type"].ToString();
                        Grd_New_Errors.Rows[i].Cells[9].Value = dt_ErroOnUser_List.Rows[i]["Error_description"].ToString();
                        Grd_New_Errors.Rows[i].Cells[10].Value = dt_ErroOnUser_List.Rows[i]["Comments"].ToString();
                        Grd_New_Errors.Rows[i].Cells[11].Value = dt_ErroOnUser_List.Rows[i]["Error_On_Task"].ToString();
                        Grd_New_Errors.Rows[i].Cells[12].Value = dt_ErroOnUser_List.Rows[i]["Error_On_User_Name"].ToString();
                        Grd_New_Errors.Rows[i].Cells[13].Value = dt_ErroOnUser_List.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                        Grd_New_Errors.Rows[i].Cells[14].Value = dt_ErroOnUser_List.Rows[i]["Error_Entered_From_Task"].ToString();
                        Grd_New_Errors.Rows[i].Cells[15].Value = dt_ErroOnUser_List.Rows[i]["Error_Entered_From"].ToString();
                        Grd_New_Errors.Rows[i].Cells[16].Value = dt_ErroOnUser_List.Rows[i]["Entered_Date"].ToString();
                        Grd_New_Errors.Rows[i].Cells[17].Value = dt_ErroOnUser_List.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[18].Value = dt_ErroOnUser_List.Rows[i]["Error_Entered_From_User_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[19].Value = dt_ErroOnUser_List.Rows[i]["ErrorInfo_ID"].ToString();
                        Grd_New_Errors.Rows[i].Cells[20].Value = dt_ErroOnUser_List.Rows[i]["Order_Id"].ToString();
                        Grd_New_Errors.Rows[i].Cells[21].Value = dt_ErroOnUser_List.Rows[i]["Reporting_1"].ToString();
                        Grd_New_Errors.Rows[i].Cells[22].Value = dt_ErroOnUser_List.Rows[i]["Reporting_2"].ToString();


                        Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                        DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                        htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                        htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_ErroOnUser_List.Rows[i]["ErrorInfo_ID"].ToString());
                        dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                        if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                        {
                            Grd_New_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                        }
                        else
                        {
                            Grd_New_Errors.Rows[i].Cells[3].Value = "Upload";
                        }

                        Grd_New_Errors.Rows[i].Cells[4].Value = "View";
                        if (User_Role == 2)
                        {
                            Grd_New_Errors.Columns[4].Visible = false;
                            Grd_New_Errors.Columns[14].Visible = false;
                            Grd_New_Errors.Columns[15].Visible = false;
                        }
                    }
                }
                else
                {
                    Grd_New_Errors.Rows.Clear();
                }
                foreach (DataGridViewRow row in Grd_New_Errors.Rows)
                {
                    row.Height = 50;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem in With Binding Data;Please Contact Administrator");
            }
        }
        private void Bind_AllDisputes_ErrorOnUser_Wise_Filter(int ErrorOnUser_Id)
        {
            Hashtable ht_AllDisputes = new Hashtable();
            DataTable dt_AllDisputes = new System.Data.DataTable();

            ht_AllDisputes.Add("@Trans", "SEARCH_DISPUTE_ERROR_BY_ERRORON_USER");
            ht_AllDisputes.Add("@User_Id", ErrorOnUser_Id);

            dt_AllDisputes = dataaccess.ExecuteSP("Sp_Error_Dashboard", ht_AllDisputes);
            if (dt_AllDisputes.Rows.Count > 0)
            {
                Grid_Disputed_Errors.Rows.Clear();
                for (int i = 0; i < dt_AllDisputes.Rows.Count; i++)
                {
                    Grid_Disputed_Errors.Rows.Add();
                    Grid_Disputed_Errors.Rows[i].Cells[1].Value = i + 1;
                    Grid_Disputed_Errors.Rows[i].Cells[2].Value = dt_AllDisputes.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[4].Value = "View";
                    Grid_Disputed_Errors.Rows[i].Cells[5].Value = "Edit";
                    Grid_Disputed_Errors.Rows[i].Cells[6].Value = dt_AllDisputes.Rows[i]["Work_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[7].Value = dt_AllDisputes.Rows[i]["Error_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[8].Value = dt_AllDisputes.Rows[i]["New_Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[9].Value = dt_AllDisputes.Rows[i]["Error_Type"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[10].Value = dt_AllDisputes.Rows[i]["Error_description"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[11].Value = dt_AllDisputes.Rows[i]["Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[12].Value = dt_AllDisputes.Rows[i]["Error_On_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[13].Value = dt_AllDisputes.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[14].Value = dt_AllDisputes.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[15].Value = dt_AllDisputes.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[16].Value = dt_AllDisputes.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[17].Value = dt_AllDisputes.Rows[i]["Entered_Date"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[18].Value = dt_AllDisputes.Rows[i]["User_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[19].Value = dt_AllDisputes.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[20].Value = dt_AllDisputes.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[21].Value = dt_AllDisputes.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[22].Value = dt_AllDisputes.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[23].Value = dt_AllDisputes.Rows[i]["Order_ID"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[24].Value = dt_AllDisputes.Rows[i]["Error_On_User_Id"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[27].Value = dt_AllDisputes.Rows[i]["Reporting_1"].ToString();
                    Grid_Disputed_Errors.Rows[i].Cells[28].Value = dt_AllDisputes.Rows[i]["Reporting_2"].ToString();

                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dt_AllDisputes.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Disputed_Errors.Rows[i].Cells[3].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Disputed_Errors.Columns[15].Visible = false;
                        Grid_Disputed_Errors.Columns[16].Visible = false;
                        Grid_Disputed_Errors.Columns[4].Visible = false;
                        Grid_Disputed_Errors.Columns[5].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Disputed_Errors.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {
                Grid_Disputed_Errors.Rows.Clear();

            }


        }
        private void ddl_Errors_Reporting_User_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindErrorsReport();
        }
        private void ddl_Error_Report_ErrorOnUser_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindErrorsReport();
        }
        private void ddlReportsErrorFrom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindErrorsReport();
        }
        private void BindErrorsReport()
        {
            try
            {
                load_Progressbar.Start_progres();
                if (ddl_Errors_Reporting_User_Name.SelectedIndex == 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex == 0 && ddlReportsErrorFrom.SelectedIndex == 0)
                {
                    Bind_Error_Report(0);
                }
                if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex == 0 && ddlReportsErrorFrom.SelectedIndex == 0)
                {
                    Bind_Error_Report(int.Parse(ddl_Errors_Reporting_User_Name.SelectedValue.ToString()));
                }
                if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex > 0 && ddlReportsErrorFrom.SelectedIndex == 0)
                {
                    Bind_Filter_Error_Report(int.Parse(ddl_Errors_Reporting_User_Name.SelectedValue.ToString()), int.Parse(ddl_Error_Report_ErrorOnUser.SelectedValue.ToString()));
                }
                if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex > 0 && ddlReportsErrorFrom.SelectedIndex > 0)
                {
                    BindErrorReportReportingUserErrorWise(ddl_Errors_Reporting_User_Name.SelectedValue, ddl_Error_Report_ErrorOnUser.SelectedValue, ddlReportsErrorFrom.SelectedValue);
                }
                if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex == 0 && ddlReportsErrorFrom.SelectedIndex > 0)
                {
                    BindErrorReportReportingErrorWise(ddl_Errors_Reporting_User_Name.SelectedValue, ddlReportsErrorFrom.SelectedValue);
                }
                if (ddl_Errors_Reporting_User_Name.SelectedIndex == 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex > 0 && ddlReportsErrorFrom.SelectedIndex == 0)
                {
                    Bind_Filter_Error_On_User_Wise(int.Parse(ddl_Error_Report_ErrorOnUser.SelectedValue.ToString()));
                }
                if (ddl_Errors_Reporting_User_Name.SelectedIndex == 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex > 0 && ddlReportsErrorFrom.SelectedIndex > 0)
                {
                    BindErrorsReportUserWiseErrorFrom(ddl_Error_Report_ErrorOnUser.SelectedValue, ddlReportsErrorFrom.SelectedValue);
                }
                if (ddl_Errors_Reporting_User_Name.SelectedIndex == 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex == 0 && ddlReportsErrorFrom.SelectedIndex > 0)
                {
                    BindErrorsReportErrorFrom(ddlReportsErrorFrom.SelectedValue);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("something went wrong contact admin");
            }
        }
        private void BindErrorsReportErrorFrom(object errorFrom)
        {
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();
            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new DataTable();
            htget_Accepted_Errors.Add("@Trans", "ERROR_REPORT_ERROR_FROM");
            htget_Accepted_Errors.Add("@External_Error", errorFrom);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);
            if (dtget_Accepted_Errors.Rows.Count > 0)
            {
                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();
                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";

                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();

                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;

                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grid_Error.Rows.Clear();
            }
        }
        private void BindErrorsReportUserWiseErrorFrom(object errorUser, object errorFrom)
        {
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();
            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new DataTable();
            htget_Accepted_Errors.Add("@Trans", "SEARCH_BY_ERROR_ON_USER_FOR_ERROR_REPORT_ERROR_FROM");
            htget_Accepted_Errors.Add("@Error_On_User_Id", errorUser);
            htget_Accepted_Errors.Add("@External_Error", errorFrom);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

            if (dtget_Accepted_Errors.Rows.Count > 0)
            {
                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();
                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";

                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();

                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;

                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grid_Error.Rows.Clear();
            }
        }
        private void BindErrorReportReportingErrorWise(object reportingTo, object errorFrom)
        {
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();
            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new DataTable();
            htget_Accepted_Errors.Add("@Trans", "FILTER_BY_REPORTINGTO_REJECTED_ERRORS_ERROR_FROM");
            htget_Accepted_Errors.Add("@Reporting_User_Id", reportingTo);
            htget_Accepted_Errors.Add("@External_Error", errorFrom);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

            if (dtget_Accepted_Errors.Rows.Count > 0)
            {
                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();
                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";

                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();

                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {
                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;

                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;

                    }
                }

                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grid_Error.Rows.Clear();
            }
        }
        private void BindErrorReportReportingUserErrorWise(object reportingTo, object errorUser, object errorFrom)
        {
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();
            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new DataTable();
            htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROR_REPORT_BY_FILTER_ERROR_FROM");
            htget_Accepted_Errors.Add("@Reporting_User_Id", reportingTo);
            htget_Accepted_Errors.Add("@Error_On_User_Id", errorUser);
            htget_Accepted_Errors.Add("@External_Error", errorFrom);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

            if (dtget_Accepted_Errors.Rows.Count > 0)
            {

                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();

                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";

                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();



                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {

                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {

                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }


                    if (User_Role == 2)
                    {

                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;

                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;

                    }
                }

                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }



            }
            else
            {
                Grid_Error.Rows.Clear();

            }

        }
        private void Bind_Filter_Error_Report(int Reporting_user_Id, int Error_On_UserId)
        {
            load_Progressbar.Start_progres();
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();
            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new DataTable();
            if (Error_Report == 1)
            {
                htget_Accepted_Errors.Add("@Trans", "GET_EMPLOYEE_WISE_ERROR_REPORT");
            }
            else if (Error_Report == 2)
            {
                if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0)
                {
                    if (ddl_Error_Report_ErrorOnUser.SelectedIndex > 0)
                    {
                        htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROR_REPORT_BY_FILTER");
                        htget_Accepted_Errors.Add("@Reporting_User_Id", int.Parse(ddl_Errors_Reporting_User_Name.SelectedValue.ToString()));
                        // htget_Accepted_Errors.Add("@User_Id", Error_On_UserId);
                    }
                }
                else
                {
                    htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROR_REPORT");
                }

            }

            htget_Accepted_Errors.Add("@Error_On_User_Id", Error_On_UserId);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

            if (dtget_Accepted_Errors.Rows.Count > 0)
            {

                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();

                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";

                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();



                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {

                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {

                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }


                    if (User_Role == 2)
                    {

                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;

                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;

                    }
                }

                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }



            }
            else
            {
                Grid_Error.Rows.Clear();

            }
        }
        private void Bind_Error_Report(int Reporting_user_Id)
        {
            load_Progressbar.Start_progres();
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();
            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new System.Data.DataTable();
            if (Error_Report == 1)
            {
                htget_Accepted_Errors.Add("@Trans", "GET_EMPLOYEE_WISE_ERROR_REPORT");
            }
            else if (Error_Report == 2)
            {
                if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0)
                {
                    htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROS_REPORTING_WISE");
                    htget_Accepted_Errors.Add("@Reporting_User_Id", int.Parse(ddl_Errors_Reporting_User_Name.SelectedValue.ToString()));

                }
                else
                {
                    htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROR_REPORT");
                }

            }

            htget_Accepted_Errors.Add("@Error_On_User_Id", User_Id);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

            if (dtget_Accepted_Errors.Rows.Count > 0)
            {

                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();

                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";
                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();

                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {

                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {

                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }


                    if (User_Role == 2)
                    {

                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;
                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;

                    }
                }

                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }



            }
            else
            {
                Grid_Error.Rows.Clear();

            }
        }
        private void Bind_Search_Error_Report(int Reporting_user_Id, int User_Id)
        {
            load_Progressbar.Start_progres();
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();

            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new DataTable();
            if (Error_Report == 1)
            {
                htget_Accepted_Errors.Add("@Trans", "GET_EMPLOYEE_WISE_ERROR_REPORT");
            }
            else if (Error_Report == 2)
            {
                if (ddl_Errors_Reporting_User_Name.SelectedIndex > 0)
                {
                    if (ddl_Error_Report_ErrorOnUser.SelectedIndex > 0)
                    {
                        htget_Accepted_Errors.Add("@Trans", "SEARCH_BY_REPORTING_TO_ERROR_ON_USER");
                        htget_Accepted_Errors.Add("@Reporting_User_Id", int.Parse(ddl_Errors_Reporting_User_Name.SelectedValue.ToString()));
                    }
                }
                else
                {
                    htget_Accepted_Errors.Add("@Trans", "GET_ALL_EMPLOYEE_ERROR_REPORT");
                }
            }
            htget_Accepted_Errors.Add("@Error_On_User_Id", User_Id);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

            if (dtget_Accepted_Errors.Rows.Count > 0)
            {
                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();
                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";
                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();


                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {

                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;
                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;

                    }
                }
                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grid_Error.Rows.Clear();
            }
        }
        private void Bind_Filter_Error_On_User_Wise(int Error_On_User_Id)
        {
            load_Progressbar.Start_progres();
            string Error_From_Date = txt_Accepted_First_date.Text.ToString();
            string Error_To_Date = txt_Accepted_Second_Date.Text.ToString();

            Hashtable htget_Accepted_Errors = new Hashtable();
            DataTable dtget_Accepted_Errors = new System.Data.DataTable();

            if (ddl_Errors_Reporting_User_Name.SelectedIndex == 0 && ddl_Error_Report_ErrorOnUser.SelectedIndex > 0)
            {
                htget_Accepted_Errors.Add("@Trans", "SEARCH_BY_ERROR_ON_USER_FOR_ERROR_REPORT");
            }

            htget_Accepted_Errors.Add("@Error_On_User_Id", Error_On_User_Id);
            htget_Accepted_Errors.Add("@Error_From_Date", Error_From_Date);
            htget_Accepted_Errors.Add("@Error_To_Date", Error_To_Date);
            dtget_Accepted_Errors = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Accepted_Errors);

            if (dtget_Accepted_Errors.Rows.Count > 0)
            {
                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget_Accepted_Errors.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();
                    Grid_Error.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error.Rows[i].Cells[1].Value = dtget_Accepted_Errors.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = "View";
                    Grid_Error.Rows[i].Cells[4].Value = "Edit";
                    Grid_Error.Rows[i].Cells[5].Value = dtget_Accepted_Errors.Rows[i]["Work_Type"].ToString();
                    Grid_Error.Rows[i].Cells[6].Value = dtget_Accepted_Errors.Rows[i]["Error_From"].ToString();
                    Grid_Error.Rows[i].Cells[7].Value = dtget_Accepted_Errors.Rows[i]["New_Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[8].Value = dtget_Accepted_Errors.Rows[i]["Error_Type"].ToString();
                    Grid_Error.Rows[i].Cells[9].Value = dtget_Accepted_Errors.Rows[i]["Error_description"].ToString();
                    Grid_Error.Rows[i].Cells[10].Value = dtget_Accepted_Errors.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[11].Value = dtget_Accepted_Errors.Rows[i]["Error_On_Task"].ToString();
                    Grid_Error.Rows[i].Cells[12].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[13].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_On_User_Branch_Name"].ToString();
                    Grid_Error.Rows[i].Cells[14].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grid_Error.Rows[i].Cells[15].Value = dtget_Accepted_Errors.Rows[i]["Error_Entered_From"].ToString();
                    Grid_Error.Rows[i].Cells[16].Value = dtget_Accepted_Errors.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[17].Value = dtget_Accepted_Errors.Rows[i]["User_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[18].Value = dtget_Accepted_Errors.Rows[i]["Manager_Supervisor_Comments"].ToString();
                    Grid_Error.Rows[i].Cells[19].Value = dtget_Accepted_Errors.Rows[i]["Supervisor_Name"].ToString();
                    Grid_Error.Rows[i].Cells[20].Value = dtget_Accepted_Errors.Rows[i]["Emp_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[21].Value = dtget_Accepted_Errors.Rows[i]["Manger_Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[22].Value = dtget_Accepted_Errors.Rows[i]["Error_Status"].ToString();
                    Grid_Error.Rows[i].Cells[23].Value = dtget_Accepted_Errors.Rows[i]["Reporting_1"].ToString();
                    Grid_Error.Rows[i].Cells[24].Value = dtget_Accepted_Errors.Rows[i]["Reporting_2"].ToString();
                    Grid_Error.Rows[i].Cells[25].Value = dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString();
                    Grid_Error.Rows[i].Cells[26].Value = dtget_Accepted_Errors.Rows[i]["Order_ID"].ToString();
                    Grid_Error.Rows[i].Cells[27].Value = dtget_Accepted_Errors.Rows[i]["Error_On_User_Id"].ToString();



                    Hashtable htget_Count_Of_Doc_Uploaded = new Hashtable();
                    DataTable dtget_Count_of_Doc_Uploaded = new System.Data.DataTable();
                    htget_Count_Of_Doc_Uploaded.Add("@Trans", "GET_COUNT_OF_DOCUMENT_UPLOADED");
                    htget_Count_Of_Doc_Uploaded.Add("@Error_Info_Id", dtget_Accepted_Errors.Rows[i]["ErrorInfo_ID"].ToString());
                    dtget_Count_of_Doc_Uploaded = dataaccess.ExecuteSP("Sp_Error_Document_Upload", htget_Count_Of_Doc_Uploaded);
                    if (dtget_Count_of_Doc_Uploaded.Rows.Count > 0)
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload" + "  [" + dtget_Count_of_Doc_Uploaded.Rows[0]["count"].ToString() + "]";
                    }
                    else
                    {
                        Grid_Error.Rows[i].Cells[2].Value = "Upload";
                    }
                    if (User_Role == 2)
                    {

                        Grid_Error.Columns[3].Visible = false;
                        Grid_Error.Columns[4].Visible = false;
                        Grid_Error.Columns[14].Visible = false;
                        Grid_Error.Columns[15].Visible = false;
                        Grid_Error.Columns[23].Visible = false;
                        Grid_Error.Columns[24].Visible = false;

                    }
                }
                foreach (DataGridViewRow row in Grid_Error.Rows)
                {
                    row.Height = 50;
                }
            }
            else
            {
                Grid_Error.Rows.Clear();
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                btn_Refresh_Click(sender, e);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                btn_Refresh_Click(sender, e);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                btn_Refresh_Click(sender, e);
            }
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Error_Analysis_Report_Click(object sender, EventArgs e)
        {
            Chart.Chart_Filter ch_report = new Chart.Chart_Filter(User_Id, User_Role, Production_Date);
            ch_report.Show();

        }
    }
}
