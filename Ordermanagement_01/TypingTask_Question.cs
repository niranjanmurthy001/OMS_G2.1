using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.DirectoryServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class Questions : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass(); int Countne_Question, Insertion_Count, Insert_question;
        int Question_count, Next_Question, previous, next, previouse_val = 0, comments = 0, Complete_check = 0, previous_Q, next_Q, previous_quest;
        int User_id, Order_Id, User_count, Available_count; string Task_id, Order_Type, Order_Type_ABS, Task_Type;

        string[] FName;
        string Document_Name;

        bool IsOpen = false;

        string Path1;
        string File_Name;
        string Directory_Path;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        //Classes.Load_Progres form_loader = new Classes.Load_Progres();
        ReportDocument rptDoc = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;


        string server = "192.168.12.33";
        string Db = "TITLELOGY-OMS";
        string Username = "sa";
        string Password = "password1$";
        string VIew_Type;
        string ORDER_NUMBER, CLIENT_NAME, SUB_CLIENT_NAME;
        int ORDER_STATUS, clientid, subprocessid;
        int Work_Type_Id;
        string ordernumber, ordertasktype;
        public Questions(int userid, int Clientid, int Subprocessid, string Taskid, string order_type, string tasktype, int orderid, int user_count, int available_count, string Order_Number, string client_Name, string Sub_Client_Name, int Order_Status, int WORK_TYPE_ID, string order_no, string order_task_type)
        {
            InitializeComponent();
            clientid = Clientid;
            subprocessid = Subprocessid;
            User_id = userid;
            ordernumber = order_no;
            ordertasktype = order_task_type;
            Task_id = Taskid;
            Order_Id = orderid;
            Task_Type = tasktype;
            Order_Type = order_type;
            User_count = user_count;
            ORDER_NUMBER = Order_Number;
            CLIENT_NAME = client_Name;
            SUB_CLIENT_NAME = Sub_Client_Name;
            ORDER_STATUS = Order_Status;
            Available_count = available_count;
            Work_Type_Id = WORK_TYPE_ID;
        }
        private void clear()
        {
            rdo_Yes.Checked = false;
            rdo_No.Checked = false;
            txt_Comments.Text = "";
            txt_Comments.Focus();
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            Submiit_Data();
            txt_Comments.Focus();

            //rdo_Yes.Focus();
        }


        private void Submiit_Data()
        { 
        
        
            Hashtable ht_check = new Hashtable();
            DataTable dt_check = new DataTable();
            ht_check.Add("@Trans", "CHECK");
            ht_check.Add("@Question_No", lbl_Question_No.Text);
            ht_check.Add("@Question", lbl_Question.Text);
            ht_check.Add("@User_id", User_id);
            ht_check.Add("@Order_Task", Task_id);
            ht_check.Add("@Order_Id", Order_Id);
            ht_check.Add("@Order_Type_ABS", Order_Type_ABS);
            ht_check.Add("@Work_Type",Work_Type_Id);
            dt_check = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_check);
            if (dt_check.Rows.Count > 0)
            {
                //updation
                if (Task_Type == "Typing")
                {
                    // int Question_Count;
                    if (rdo_Yes.Checked)
                    {
                        if (txt_Comments.Text != "" && txt_Comments.Text.Length > 2)
                        {
                            Hashtable ht_update_y = new Hashtable();
                            DataTable dt_update_y = new DataTable();
                            ht_update_y.Add("@Trans", "UPDATE_YES");
                            ht_update_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_update_y.Add("@Question", lbl_Question.Text);
                            ht_update_y.Add("@Question_Output", "True");
                            ht_update_y.Add("@Comments", txt_Comments.Text);
                            ht_update_y.Add("@User_id", User_id);
                            ht_update_y.Add("@Order_Task", Task_id);
                            ht_update_y.Add("@Order_Id", Order_Id);
                            ht_update_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_update_y.Add("@Work_Type", Work_Type_Id);
                            dt_update_y = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_update_y);

                            //Exit
                            //Hashtable ht_exit = new Hashtable();
                            //DataTable dt_exit = new DataTable();
                            //ht_exit.Add("@Trans", "QUESTION_COUNT");
                            //ht_exit.Add("@Order_Type_ABS", "COS");
                            //ht_exit.Add("@Task_Confirmation", "True");
                            //ht_exit.Add("@Order_Status", Task_id);
                            //dt_exit = dataaccess.ExecuteSP("Sp_Check_List", ht_exit);
                            //if (dt_exit.Rows.Count > 0)
                            //{
                            //    Question_Count = dt_exit.Rows.Count;
                            //}


                            //Load true question
                            Hashtable ht_load_y = new Hashtable();
                            DataTable dt_load_y = new DataTable();
                            ht_load_y.Add("@Trans", "LOAD_TRUE_NEXTQUESTION");
                            ht_load_y.Add("@Question_No", lbl_Question_No.Text);
                            if (Order_Type_ABS != "US")
                            {
                                ht_load_y.Add("@Order_Type_ABS", "COS");
                            }
                            else
                            {

                                ht_load_y.Add("@Order_Type_ABS", "US");
                            }
                            ht_load_y.Add("@Order_Status", Task_id);
                            ht_load_y.Add("@Work_Type", Work_Type_Id);
                            dt_load_y = dataaccess.ExecuteSP("Sp_Check_List", ht_load_y);
                            if (dt_load_y.Rows.Count > 0)
                            {
                                if (dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }

                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_y = new Hashtable();
                                DataTable dt_set_y = new DataTable();
                                ht_set_y.Add("@Trans", "SET_TRUE_QUESTION");
                                ht_set_y.Add("@Next_Confirmation_Id", Next_Question);
                               

                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_y.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_y.Add("@Order_Type_ABS", "US");
                                }
                                ht_set_y.Add("@Order_Status", Task_id);
                                ht_set_y.Add("@Work_Type", Work_Type_Id);
                                dt_set_y = dataaccess.ExecuteSP("Sp_Check_List", ht_set_y);
                                if (dt_set_y.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_y.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_y.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments or Type Yes and click submit");
                            txt_Comments.Focus();
                            rdo_Yes.Checked = true;
                            
                            comments = 1;
                        

                        }
                    }
                    else if (rdo_No.Checked)
                    {
                        if (txt_Comments.Text != "")
                        {
                            Hashtable ht_update_n = new Hashtable();
                            DataTable dt_update_n = new DataTable();
                            ht_update_n.Add("@Trans", "UPDATE_NO");
                            ht_update_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_update_n.Add("@Question", lbl_Question.Text);
                            ht_update_n.Add("@Question_Output", "False");
                            ht_update_n.Add("@Comments", txt_Comments.Text);
                            ht_update_n.Add("@Order_Id", Order_Id);
                            ht_update_n.Add("@User_id", User_id);
                            ht_update_n.Add("@Order_Task", Task_id);
                            ht_update_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_update_n.Add("@Work_Type", Work_Type_Id);
                            dt_update_n = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_update_n);

                            //Load false question
                            Hashtable ht_load_n = new Hashtable();
                            DataTable dt_load_n = new DataTable();
                            ht_load_n.Add("@Trans", "LOAD_FALSE_NEXTQUESTION");
                            ht_load_n.Add("@Question_No", lbl_Question_No.Text);
                           
                            if (Order_Type_ABS != "US")
                            {
                                ht_load_n.Add("@Order_Type_ABS", "COS");
                            }
                            else
                            {

                                ht_load_n.Add("@Order_Type_ABS", "US");
                            }
                            ht_load_n.Add("@Order_Status", Task_id);
                            ht_load_n.Add("@Work_Type", Work_Type_Id);
                            dt_load_n = dataaccess.ExecuteSP("Sp_Check_List", ht_load_n);
                            if (dt_load_n.Rows.Count > 0)
                            {
                                if (dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }
                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_n = new Hashtable();
                                DataTable dt_set_n = new DataTable();
                                ht_set_n.Add("@Trans", "SET_FALSE_QUESTION");
                                ht_set_n.Add("@Next_Confirmation_Id", Next_Question);
                          

                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_n.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_n.Add("@Order_Type_ABS", "US");
                                }
                                ht_set_n.Add("@Order_Status", Task_id);
                                ht_set_n.Add("@Work_Type", Work_Type_Id);
                                dt_set_n = dataaccess.ExecuteSP("Sp_Check_List", ht_set_n);
                                if (dt_set_n.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_n.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_n.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();


                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();

                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments and click submit");
                            txt_Comments.Focus();
                            rdo_No.Checked = true;
                            comments = 1;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Kindly Give Yes or No option");
                    }
                }
                else if (Task_Type == "Typing QC")
                {
                    if (rdo_Yes.Checked)
                    {
                        if (txt_Comments.Text != "" && txt_Comments.Text.Length > 2)
                        {
                            Hashtable ht_update_y = new Hashtable();
                            DataTable dt_update_y = new DataTable();
                            ht_update_y.Add("@Trans", "UPDATE_YES");
                            ht_update_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_update_y.Add("@Question", lbl_Question.Text);
                            ht_update_y.Add("@Question_Output", "True");
                            ht_update_y.Add("@Comments", txt_Comments.Text);
                            ht_update_y.Add("@User_id", User_id);
                            ht_update_y.Add("@Order_Id", Order_Id);
                            ht_update_y.Add("@Order_Task", Task_id);
                            ht_update_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_update_y.Add("@Work_Type", Work_Type_Id);
                            dt_update_y = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_update_y);


                            //Load true question
                            Hashtable ht_load_y = new Hashtable();
                            DataTable dt_load_y = new DataTable();
                            ht_load_y.Add("@Trans", "LOAD_TRUE_NEXTQUESTION");
                            ht_load_y.Add("@Question_No", lbl_Question_No.Text);
                           


                            if (Order_Type_ABS != "US")
                            {
                                ht_load_y.Add("@Order_Type_ABS", "COS");
                            }
                            else
                            {

                                ht_load_y.Add("@Order_Type_ABS", "US");
                            }
                            ht_load_y.Add("@Order_Status", Task_id);
                            ht_load_y.Add("@Work_Type", Work_Type_Id);
                            dt_load_y = dataaccess.ExecuteSP("Sp_Check_List", ht_load_y);
                            if (dt_load_y.Rows.Count > 0)
                            {
                                if (dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }

                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_y = new Hashtable();
                                DataTable dt_set_y = new DataTable();
                                ht_set_y.Add("@Trans", "SET_TRUE_QUESTION");
                                ht_set_y.Add("@Next_Confirmation_Id", Next_Question);
                              


                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_y.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_y.Add("@Order_Type_ABS", "US");
                                } 
                                
                                ht_set_y.Add("@Order_Status", Task_id);
                                ht_set_y.Add("@Work_Type", Work_Type_Id);
                                dt_set_y = dataaccess.ExecuteSP("Sp_Check_List", ht_set_y);
                                if (dt_set_y.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_y.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_y.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments or Type Yes and click submit");
                            txt_Comments.Focus();
                            rdo_Yes.Checked = true;

                            comments = 1;


                        }

                    }
                    else if (rdo_No.Checked)
                    {
                        if (txt_Comments.Text != "")
                        {
                            Hashtable ht_update_n = new Hashtable();
                            DataTable dt_update_n = new DataTable();
                            ht_update_n.Add("@Trans", "UPDATE_NO");
                            ht_update_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_update_n.Add("@Question", lbl_Question.Text);
                            ht_update_n.Add("@Question_Output", "False");
                            ht_update_n.Add("@Comments", txt_Comments.Text);
                            ht_update_n.Add("@User_id", User_id);
                            ht_update_n.Add("@Order_Id", Order_Id);
                            ht_update_n.Add("@Order_Task", Task_id);
                            ht_update_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_update_n.Add("@Work_Type", Work_Type_Id);
                            dt_update_n = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_update_n);

                            //Load false question
                            Hashtable ht_load_n = new Hashtable();
                            DataTable dt_load_n = new DataTable();
                            ht_load_n.Add("@Trans", "LOAD_FALSE_NEXTQUESTION");

                            ht_load_n.Add("@Question_No", lbl_Question_No.Text);
                              if (Order_Type_ABS != "US")
                                {
                                    ht_load_n.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_load_n.Add("@Order_Type_ABS", "US");
                                } 
                          
                            ht_load_n.Add("@Order_Status", Task_id);
                            ht_load_n.Add("@Work_Type", Work_Type_Id);
                            dt_load_n = dataaccess.ExecuteSP("Sp_Check_List", ht_load_n);
                            if (dt_load_n.Rows.Count > 0)
                            {
                                if (dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }
                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_n = new Hashtable();
                                DataTable dt_set_n = new DataTable();
                                ht_set_n.Add("@Trans", "SET_FALSE_QUESTION");
                                ht_set_n.Add("@Next_Confirmation_Id", Next_Question);
                        
                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_n.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_n.Add("@Order_Type_ABS", "US");
                                } 
                                ht_set_n.Add("@Order_Status", Task_id);
                                ht_set_n.Add("@Work_Type", Work_Type_Id);
                                dt_set_n = dataaccess.ExecuteSP("Sp_Check_List", ht_set_n);
                                if (dt_set_n.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_n.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_n.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();

                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments and click submit");
                            txt_Comments.Focus();
                            rdo_No.Checked = true;
                            comments = 1;
                        }


                    }
                    else
                    {
                        MessageBox.Show("Kindly Give Yes or No option");
                    }
                }
                else
                {
                    if (rdo_Yes.Checked)
                    {
                        if (txt_Comments.Text != "" && txt_Comments.Text.Length > 2)
                        {
                            Hashtable ht_update_y = new Hashtable();
                            DataTable dt_update_y = new DataTable();
                            ht_update_y.Add("@Trans", "UPDATE_YES");
                            ht_update_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_update_y.Add("@Question", lbl_Question.Text);
                            ht_update_y.Add("@Question_Output", "True");
                            ht_update_y.Add("@Comments", txt_Comments.Text);
                            ht_update_y.Add("@User_id", User_id);
                            ht_update_y.Add("@Order_Id", Order_Id);
                            ht_update_y.Add("@Order_Task", Task_id);
                            ht_update_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_update_y.Add("@Work_Type", Work_Type_Id);
                            dt_update_y = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_update_y);


                            //Load true question
                            Hashtable ht_load_y = new Hashtable();
                            DataTable dt_load_y = new DataTable();
                            ht_load_y.Add("@Trans", "LOAD_TRUE_NEXTQUESTION");
                            ht_load_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_load_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_load_y.Add("@Order_Status", Task_id);
                            ht_load_y.Add("@Work_Type", Work_Type_Id);
                            dt_load_y = dataaccess.ExecuteSP("Sp_Check_List", ht_load_y);
                            if (dt_load_y.Rows.Count > 0)
                            {
                                if (dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }

                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_y = new Hashtable();
                                DataTable dt_set_y = new DataTable();
                                ht_set_y.Add("@Trans", "SET_TRUE_QUESTION");
                                ht_set_y.Add("@Next_Confirmation_Id", Next_Question);
                                ht_set_y.Add("@Order_Type_ABS", Order_Type_ABS);
                                ht_set_y.Add("@Order_Status", Task_id);
                                ht_set_y.Add("@Work_Type", Work_Type_Id);
                                dt_set_y = dataaccess.ExecuteSP("Sp_Check_List", ht_set_y);
                                if (dt_set_y.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_y.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_y.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments or Type Yes and click submit");
                            txt_Comments.Focus();
                            rdo_Yes.Checked = true;

                            comments = 1;


                        }
                    }
                    else if (rdo_No.Checked)
                    {
                        if (txt_Comments.Text != "")
                        {
                            Hashtable ht_update_n = new Hashtable();
                            DataTable dt_update_n = new DataTable();
                            ht_update_n.Add("@Trans", "UPDATE_NO");
                            ht_update_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_update_n.Add("@Question", lbl_Question.Text);
                            ht_update_n.Add("@Question_Output", "False");
                            ht_update_n.Add("@Comments", txt_Comments.Text);
                            ht_update_n.Add("@User_id", User_id);
                            ht_update_n.Add("@Order_Id", Order_Id);
                            ht_update_n.Add("@Order_Task", Task_id);
                            ht_update_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_update_n.Add("@Work_Type", Work_Type_Id);
                            dt_update_n = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_update_n);

                            //Load false question
                            Hashtable ht_load_n = new Hashtable();
                            DataTable dt_load_n = new DataTable();
                            ht_load_n.Add("@Trans", "LOAD_FALSE_NEXTQUESTION");
                            ht_load_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_load_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_load_n.Add("@Order_Status", Task_id);
                            ht_load_n.Add("@Work_Type", Work_Type_Id);
                            dt_load_n = dataaccess.ExecuteSP("Sp_Check_List", ht_load_n);
                            if (dt_load_n.Rows.Count > 0)
                            {
                                if (dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }
                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_n = new Hashtable();
                                DataTable dt_set_n = new DataTable();
                                ht_set_n.Add("@Trans", "SET_FALSE_QUESTION");
                                ht_set_n.Add("@Next_Confirmation_Id", Next_Question);
                                ht_set_n.Add("@Order_Type_ABS", Order_Type_ABS);
                                ht_set_n.Add("@Order_Status", Task_id);
                                ht_set_n.Add("@Work_Type", Work_Type_Id);
                                dt_set_n = dataaccess.ExecuteSP("Sp_Check_List", ht_set_n);
                                if (dt_set_n.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_n.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_n.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();

                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments and click submit");
                            txt_Comments.Focus();
                            rdo_No.Checked = true;
                            comments = 1;
                        }


                    }
                    else
                    {
                        MessageBox.Show("Kindly Give Yes or No option");
                    }
                }
            }
            else
            {
                //Insertion

                if (Task_Type == "Typing")
                {
                    // int Question_Count;
                    if (rdo_Yes.Checked)
                    {
                        if (txt_Comments.Text != "" && txt_Comments.Text.Length > 2)
                        {
                            Hashtable ht_insert_y = new Hashtable();
                            DataTable dt_insert_y = new DataTable();
                            ht_insert_y.Add("@Trans", "INSERT_YES");
                            ht_insert_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_insert_y.Add("@Question", lbl_Question.Text);
                            ht_insert_y.Add("@Question_Output", "True");
                            ht_insert_y.Add("@Comments", txt_Comments.Text);
                            ht_insert_y.Add("@User_id", User_id);
                            ht_insert_y.Add("@Order_Task", Task_id);
                            ht_insert_y.Add("@Order_Id", Order_Id);
                            ht_insert_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_insert_y.Add("@Work_Type", Work_Type_Id);
                            dt_insert_y = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_insert_y);

                            //Exit
                            //Hashtable ht_exit = new Hashtable();
                            //DataTable dt_exit = new DataTable();
                            //ht_exit.Add("@Trans", "QUESTION_COUNT");
                            //ht_exit.Add("@Order_Type_ABS", "COS");
                            //ht_exit.Add("@Task_Confirmation", "True");
                            //ht_exit.Add("@Order_Status", Task_id);
                            //dt_exit = dataaccess.ExecuteSP("Sp_Check_List", ht_exit);
                            //if (dt_exit.Rows.Count > 0)
                            //{
                            //    Question_Count = dt_exit.Rows.Count;
                            //}


                            //Load true question
                            Hashtable ht_load_y = new Hashtable();
                            DataTable dt_load_y = new DataTable();
                            ht_load_y.Add("@Trans", "LOAD_TRUE_NEXTQUESTION");
                            ht_load_y.Add("@Question_No", lbl_Question_No.Text);
                            if (Order_Type_ABS != "US")
                            {
                                ht_load_y.Add("@Order_Type_ABS", "COS");
                            }
                            else
                            
                            {

                                ht_load_y.Add("@Order_Type_ABS", "US");
                            }
                            ht_load_y.Add("@Order_Status", Task_id);
                            ht_load_y.Add("@Work_Type", Work_Type_Id);
                            dt_load_y = dataaccess.ExecuteSP("Sp_Check_List", ht_load_y);
                            if (dt_load_y.Rows.Count > 0)
                            {
                                if (dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }

                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_y = new Hashtable();
                                DataTable dt_set_y = new DataTable();
                                ht_set_y.Add("@Trans", "SET_TRUE_QUESTION");
                                ht_set_y.Add("@Next_Confirmation_Id", Next_Question);
                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_y.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_y.Add("@Order_Type_ABS", "US");
                                }
                              

                                ht_set_y.Add("@Order_Status", Task_id);
                                ht_set_y.Add("@Work_Type", Work_Type_Id);
                                dt_set_y = dataaccess.ExecuteSP("Sp_Check_List", ht_set_y);
                                if (dt_set_y.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_y.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_y.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments or Type Yes and click submit");
                            txt_Comments.Focus();
                            rdo_Yes.Checked = true;

                            comments = 1;


                        }

                    }
                    else if (rdo_No.Checked)
                    {
                        if (txt_Comments.Text != "")
                        {
                            Hashtable ht_insert_n = new Hashtable();
                            DataTable dt_insert_n = new DataTable();
                            ht_insert_n.Add("@Trans", "INSERT_NO");
                            ht_insert_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_insert_n.Add("@Question", lbl_Question.Text);
                            ht_insert_n.Add("@Question_Output", "False");
                            ht_insert_n.Add("@Comments", txt_Comments.Text);
                            ht_insert_n.Add("@Order_Id", Order_Id);
                            ht_insert_n.Add("@User_id", User_id);
                            ht_insert_n.Add("@Order_Task", Task_id);
                            ht_insert_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_insert_n.Add("@Work_Type", Work_Type_Id);
                            dt_insert_n = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_insert_n);

                            //Load false question
                            Hashtable ht_load_n = new Hashtable();
                            DataTable dt_load_n = new DataTable();
                            ht_load_n.Add("@Trans", "LOAD_FALSE_NEXTQUESTION");
                            ht_load_n.Add("@Question_No", lbl_Question_No.Text);
                            if (Order_Type_ABS != "US")
                            {
                                ht_load_n.Add("@Order_Type_ABS", "COS");
                            }
                            else
                            {

                                ht_load_n.Add("@Order_Type_ABS", "US");
                            }
                          
                            ht_load_n.Add("@Order_Status", Task_id);
                            ht_load_n.Add("@Work_Type", Work_Type_Id);
                            dt_load_n = dataaccess.ExecuteSP("Sp_Check_List", ht_load_n);
                            if (dt_load_n.Rows.Count > 0)
                            {
                                if (dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }
                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_n = new Hashtable();
                                DataTable dt_set_n = new DataTable();
                                ht_set_n.Add("@Trans", "SET_FALSE_QUESTION");
                                ht_set_n.Add("@Next_Confirmation_Id", Next_Question);
                               
                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_n.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_n.Add("@Order_Type_ABS", "US");
                                }
                                ht_set_n.Add("@Order_Status", Task_id);
                                ht_set_n.Add("@Work_Type", Work_Type_Id);
                                dt_set_n = dataaccess.ExecuteSP("Sp_Check_List", ht_set_n);
                                if (dt_set_n.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_n.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_n.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();

                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments and click submit");
                            txt_Comments.Focus();
                            rdo_No.Checked = true;
                            comments = 1;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Kindly Give Yes or No option");
                    }
                }
                else if (Task_Type == "Typing QC")
                {
                    if (rdo_Yes.Checked)
                    {
                        if (txt_Comments.Text != "" && txt_Comments.Text.Length > 2)
                        {
                            Hashtable ht_insert_y = new Hashtable();
                            DataTable dt_insert_y = new DataTable();
                            ht_insert_y.Add("@Trans", "INSERT_YES");
                            ht_insert_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_insert_y.Add("@Question", lbl_Question.Text);
                            ht_insert_y.Add("@Question_Output", "True");
                            ht_insert_y.Add("@Comments", txt_Comments.Text);
                            ht_insert_y.Add("@User_id", User_id);
                            ht_insert_y.Add("@Order_Id", Order_Id);
                            ht_insert_y.Add("@Order_Task", Task_id);
                            ht_insert_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_insert_y.Add("@Work_Type", Work_Type_Id);
                            dt_insert_y = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_insert_y);


                            //Load true question
                            Hashtable ht_load_y = new Hashtable();
                            DataTable dt_load_y = new DataTable();
                            ht_load_y.Add("@Trans", "LOAD_TRUE_NEXTQUESTION");
                            ht_load_y.Add("@Question_No", lbl_Question_No.Text);
                            if (Order_Type_ABS != "US")
                            {
                                ht_load_y.Add("@Order_Type_ABS", "COS");
                            }
                            else
                            {

                                ht_load_y.Add("@Order_Type_ABS", "US");
                            }
                           
                            ht_load_y.Add("@Order_Status", Task_id);
                            ht_load_y.Add("@Work_Type", Work_Type_Id);
                            dt_load_y = dataaccess.ExecuteSP("Sp_Check_List", ht_load_y);
                            if (dt_load_y.Rows.Count > 0)
                            {
                                if (dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }

                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_y = new Hashtable();
                                DataTable dt_set_y = new DataTable();
                                ht_set_y.Add("@Trans", "SET_TRUE_QUESTION");
                                ht_set_y.Add("@Next_Confirmation_Id", Next_Question);
                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_y.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_y.Add("@Order_Type_ABS", "US");
                                }
                          
                                ht_set_y.Add("@Order_Status", Task_id);
                                ht_set_y.Add("@Work_Type", Work_Type_Id);
                                dt_set_y = dataaccess.ExecuteSP("Sp_Check_List", ht_set_y);
                                if (dt_set_y.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_y.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_y.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments or Type Yes and click submit");
                            txt_Comments.Focus();
                            rdo_Yes.Checked = true;

                            comments = 1;


                        }

                    }
                    else if (rdo_No.Checked)
                    {
                        if (txt_Comments.Text != "")
                        {
                        
                            Hashtable ht_insert_n = new Hashtable();
                            DataTable dt_insert_n = new DataTable();
                            ht_insert_n.Add("@Trans", "INSERT_NO");
                            ht_insert_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_insert_n.Add("@Question", lbl_Question.Text);
                            ht_insert_n.Add("@Question_Output", "False");
                            ht_insert_n.Add("@Comments", txt_Comments.Text);
                            ht_insert_n.Add("@User_id", User_id);
                            ht_insert_n.Add("@Order_Id", Order_Id);
                            ht_insert_n.Add("@Order_Task", Task_id);
                            ht_insert_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_insert_n.Add("@Work_Type", Work_Type_Id);
                            dt_insert_n = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_insert_n);

                            //Load false question
                            Hashtable ht_load_n = new Hashtable();
                            DataTable dt_load_n = new DataTable();
                            ht_load_n.Add("@Trans", "LOAD_FALSE_NEXTQUESTION");
                            ht_load_n.Add("@Question_No", lbl_Question_No.Text);
                         

                            if (Order_Type_ABS != "US")
                            {
                                ht_load_n.Add("@Order_Type_ABS", "COS");
                            }
                            else
                            {

                                ht_load_n.Add("@Order_Type_ABS", "US");
                            }
                          
                            ht_load_n.Add("@Order_Status", Task_id);
                            ht_load_n.Add("@Work_Type", Work_Type_Id);
                            dt_load_n = dataaccess.ExecuteSP("Sp_Check_List", ht_load_n);
                            if (dt_load_n.Rows.Count > 0)
                            {
                                if (dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }
                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_n = new Hashtable();
                                DataTable dt_set_n = new DataTable();
                                ht_set_n.Add("@Trans", "SET_FALSE_QUESTION");
                                ht_set_n.Add("@Next_Confirmation_Id", Next_Question);
                                

                                if (Order_Type_ABS != "US")
                                {
                                    ht_set_n.Add("@Order_Type_ABS", "COS");
                                }
                                else
                                {

                                    ht_set_n.Add("@Order_Type_ABS", "US");
                                }
                                ht_set_n.Add("@Order_Status", Task_id);
                                ht_set_n.Add("@Work_Type", Work_Type_Id);
                                dt_set_n = dataaccess.ExecuteSP("Sp_Check_List", ht_set_n);
                                if (dt_set_n.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_n.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_n.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }

                            clear();

                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments and click submit");
                            txt_Comments.Focus();
                            rdo_No.Checked = true;
                            comments = 1;
                        }


                    }
                    else
                    {
                        MessageBox.Show("Kindly Give Yes or No option");
                    }
                }
                else
                {
                    if (rdo_Yes.Checked)
                    {
                        if (txt_Comments.Text != "" && txt_Comments.Text.Length>2)
                        {
                            Hashtable ht_insert_y = new Hashtable();
                            DataTable dt_insert_y = new DataTable();
                            ht_insert_y.Add("@Trans", "INSERT_YES");
                            ht_insert_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_insert_y.Add("@Question", lbl_Question.Text);
                            ht_insert_y.Add("@Question_Output", "True");
                            ht_insert_y.Add("@Comments", txt_Comments.Text);
                            ht_insert_y.Add("@User_id", User_id);
                            ht_insert_y.Add("@Order_Id", Order_Id);
                            ht_insert_y.Add("@Order_Task", Task_id);
                            ht_insert_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_insert_y.Add("@Work_Type", Work_Type_Id);
                            dt_insert_y = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_insert_y);


                            //Load true question
                            Hashtable ht_load_y = new Hashtable();
                            DataTable dt_load_y = new DataTable();
                            ht_load_y.Add("@Trans", "LOAD_TRUE_NEXTQUESTION");
                            ht_load_y.Add("@Question_No", lbl_Question_No.Text);
                            ht_load_y.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_load_y.Add("@Order_Status", Task_id);
                            ht_load_y.Add("@Work_Type", Work_Type_Id);
                            dt_load_y = dataaccess.ExecuteSP("Sp_Check_List", ht_load_y);
                            if (dt_load_y.Rows.Count > 0)
                            {
                                if (dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_y.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }

                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_y = new Hashtable();
                                DataTable dt_set_y = new DataTable();
                                ht_set_y.Add("@Trans", "SET_TRUE_QUESTION");
                                ht_set_y.Add("@Next_Confirmation_Id", Next_Question);
                                ht_set_y.Add("@Order_Type_ABS", Order_Type_ABS);
                                ht_set_y.Add("@Order_Status", Task_id);
                                ht_set_y.Add("@Work_Type", Work_Type_Id);
                                dt_set_y = dataaccess.ExecuteSP("Sp_Check_List", ht_set_y);
                                if (dt_set_y.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_y.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_y.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments or Type Yes and click submit");
                            txt_Comments.Focus();
                            rdo_Yes.Checked = true;

                            comments = 1;


                        }
                    }
                    else if (rdo_No.Checked)
                    {
                        if (txt_Comments.Text != "")
                        {
                            Hashtable ht_insert_n = new Hashtable();
                            DataTable dt_insert_n = new DataTable();
                            ht_insert_n.Add("@Trans", "INSERT_NO");
                            ht_insert_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_insert_n.Add("@Question", lbl_Question.Text);
                            ht_insert_n.Add("@Question_Output", "False");
                            ht_insert_n.Add("@Comments", txt_Comments.Text);
                            ht_insert_n.Add("@User_id", User_id);
                            ht_insert_n.Add("@Order_Id", Order_Id);
                            ht_insert_n.Add("@Order_Task", Task_id);
                            ht_insert_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_insert_n.Add("@Work_Type", Work_Type_Id);
                            dt_insert_n = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_insert_n);

                            //Load false question
                            Hashtable ht_load_n = new Hashtable();
                            DataTable dt_load_n = new DataTable();
                            ht_load_n.Add("@Trans", "LOAD_FALSE_NEXTQUESTION");
                            ht_load_n.Add("@Question_No", lbl_Question_No.Text);
                            ht_load_n.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_load_n.Add("@Order_Status", Task_id);
                            ht_load_n.Add("@Work_Type", Work_Type_Id);
                            dt_load_n = dataaccess.ExecuteSP("Sp_Check_List", ht_load_n);
                            if (dt_load_n.Rows.Count > 0)
                            {
                                if (dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "0" && dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString() != "")
                                {
                                    Next_Question = int.Parse(dt_load_n.Rows[0]["Next_Confirmation_Id"].ToString());
                                }
                                else
                                {
                                    Next_Question = 0;
                                }
                            }
                            if (Next_Question != 0)
                            {
                                //set next question
                                Hashtable ht_set_n = new Hashtable();
                                DataTable dt_set_n = new DataTable();
                                ht_set_n.Add("@Trans", "SET_FALSE_QUESTION");
                                ht_set_n.Add("@Next_Confirmation_Id", Next_Question);
                                ht_set_n.Add("@Order_Type_ABS", Order_Type_ABS);
                                ht_set_n.Add("@Order_Status", Task_id);
                                ht_set_n.Add("@Work_Type", Work_Type_Id);
                                dt_set_n = dataaccess.ExecuteSP("Sp_Check_List", ht_set_n);
                                if (dt_set_n.Rows.Count > 0)
                                {
                                    lbl_Question.Text = dt_set_n.Rows[0]["Confirmation_Message"].ToString();
                                    lbl_Question_No.Text = dt_set_n.Rows[0]["Question_No"].ToString();
                                }
                            }
                            else
                            {
                                btn_submit.Enabled = false;
                                Copy_Check_List_To_Server();
                                MessageBox.Show("Checklist Completed Successfully");
                                txt_Comments.Focus();
                                this.Close();

                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Text == "Employee_Order_Entry")
                                    {
                                        IsOpen = true;
                                        f.Focus();
                                        f.Enabled = true;
                                        f.Show();
                                        break;
                                    }
                                }
                            }
                            clear();

                        }
                        else
                        {
                            MessageBox.Show("Please fill the comments and click submit");
                            rdo_No.Checked = true;
                            comments = 1;
                        }


                    }
                    else
                    {
                        MessageBox.Show("Kindly Give Yes or No option");
                    }
                }
            }

            rdo_Yes.Focus();
        }
        
        private void TypingTask_Question_Load(object sender, EventArgs e)
        {
           
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABBR");
            ht_BIND.Add("@Order_Type", Order_Type);
            dt_BIND = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_BIND);
            if (dt_BIND.Rows.Count > 0)
            {
                Order_Type_ABS = dt_BIND.Rows[0]["Order_Type_Abbreviation"].ToString();
            }
            
            


            
                if (Task_Type != "Typing" && Task_Type != "Typing QC")
                {
                    Hashtable ht_load = new Hashtable();
                    DataTable dt_load = new DataTable();
                    ht_load.Add("@Trans", "LOAD_QUESTION");
                    ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                    ht_load.Add("@Order_Status", Task_id);
                    dt_load = dataaccess.ExecuteSP("Sp_Check_List", ht_load);
                    if (dt_load.Rows.Count > 0)
                    {
                        Question_count = dt_load.Rows.Count;
                        lbl_Question.Text = dt_load.Rows[0]["Confirmation_Message"].ToString();

                    }
                }
                else if (Task_Type == "Typing")
                {


                    Hashtable ht_load = new Hashtable();
                    DataTable dt_load = new DataTable();
                    ht_load.Add("@Trans", "LOAD_QUESTION");
                    if (Order_Type_ABS != "US")
                    {
                        ht_load.Add("@Order_Type_ABS", "COS");
                        ht_load.Add("@Order_Status", "4");
                    }
                    else if (Order_Type_ABS == "US")
                    {
                        ht_load.Add("@Order_Type_ABS", "US");
                        ht_load.Add("@Order_Status", "4");

                    }
                    dt_load = dataaccess.ExecuteSP("Sp_Check_List", ht_load);
                    if (dt_load.Rows.Count > 0)
                    {
                        Question_count = dt_load.Rows.Count;
                        lbl_Question.Text = dt_load.Rows[0]["Confirmation_Message"].ToString();

                    }

                }
                else if (Task_Type == "Typing QC")
                {
                    Hashtable ht_load = new Hashtable();
                    DataTable dt_load = new DataTable();
                    ht_load.Add("@Trans", "LOAD_QUESTION");
                    if (Order_Type_ABS != "US")
                    {
                        ht_load.Add("@Order_Type_ABS", "COS");
                        ht_load.Add("@Order_Status", "7");
                    }
                    else if (Order_Type_ABS == "US")
                    {
                        ht_load.Add("@Order_Type_ABS", "US");
                        ht_load.Add("@Order_Status", "7");

                    }
                    dt_load = dataaccess.ExecuteSP("Sp_Check_List", ht_load);
                    if (dt_load.Rows.Count > 0)
                    {
                        Question_count = dt_load.Rows.Count;
                        lbl_Question.Text = dt_load.Rows[0]["Confirmation_Message"].ToString();

                    }
                }
            


            
            
            
            lbl_Question_No.Text = "1";
            btn_Next.Enabled = false;

        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            if (lbl_Question_No.Text == "1")
            {
                previous = 0;
                MessageBox.Show("This is the First question... kindly answer it");
            }
            else
            {
                Hashtable ht_load = new Hashtable();
                DataTable dt_load = new DataTable();
                if (btn_Next.Enabled == false)
                {
                    ht_load.Add("@Trans", "CHECK_PREVIOUS_QUESTION1");
                    ht_load.Add("@Order_Task", Task_id);
                    ht_load.Add("@Question_No", lbl_Question_No.Text);
                    //ht_load.Add("@Client", clientid);
                    //ht_load.Add("@Subclient", subprocessid);
                    ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                    ht_load.Add("@Order_Id", Order_Id);
                    ht_load.Add("@User_id", User_id);
                    ht_load.Add("@Work_Type", Work_Type_Id);
                    dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                    if (dt_load.Rows.Count > 0 && dt_load.Rows[0]["Question_Sequence_Id"].ToString() != "")
                    {
                        previous_quest = int.Parse(dt_load.Rows[0]["Question_Sequence_Id"].ToString());


                        ht_load.Clear(); dt_load.Clear();
                        ht_load.Add("@Trans", "LOAD_PREVIOUS_QUESTION1");
                        ht_load.Add("@Question_Sequence_Id", previous_quest - 1);
                        ht_load.Add("@Order_Task", Task_id);
                        ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                        ht_load.Add("@Order_Id", Order_Id);
                        ht_load.Add("@User_id", User_id);
                        ht_load.Add("@Work_Type", Work_Type_Id);
                        dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                        if (dt_load.Rows.Count > 0)
                        {
                            if (dt_load.Rows[0]["Question_Output"].ToString() == "True")
                            {
                                rdo_Yes.Checked = true;
                                txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                            }
                            else if (dt_load.Rows[0]["Question_Output"].ToString() == "False")
                            {
                                rdo_No.Checked = true;
                                txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                            }
                            lbl_Question_No.Text = dt_load.Rows[0]["Question_no"].ToString();
                            lbl_Question.Text = dt_load.Rows[0]["Question"].ToString();

                        }


                        previous_quest = previous_quest - 1;
                        //}
                        btn_Next.Enabled = true;
                    }
                    else
                    {
                        ht_load.Clear(); dt_load.Clear();
                        //PREVIOUS_QUESTION1
                        ht_load.Add("@Trans", "PREVIOUS_QUESTION1");
                        ht_load.Add("@Order_Task", Task_id);
                        ht_load.Add("@Question_No", lbl_Question_No.Text);
                        ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                        ht_load.Add("@Order_Id", Order_Id);
                        ht_load.Add("@User_id", User_id);
                        ht_load.Add("@Work_Type", Work_Type_Id);
                        dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                        if (dt_load.Rows.Count > 0)
                        {
                            previous_quest = int.Parse(dt_load.Rows[0]["Question_Sequence_Id"].ToString());

                            ht_load.Clear(); dt_load.Clear();
                            ht_load.Add("@Trans", "LOAD_PREVIOUS_QUESTION1");
                            ht_load.Add("@Question_Sequence_Id", previous_quest);
                            ht_load.Add("@Order_Task", Task_id);
                            ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_load.Add("@Order_Id", Order_Id);
                            ht_load.Add("@User_id", User_id);
                            ht_load.Add("@Work_Type", Work_Type_Id);
                            dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                            if (dt_load.Rows.Count > 0)
                            {
                                if (dt_load.Rows[0]["Question_Output"].ToString() == "True")
                                {
                                    rdo_Yes.Checked = true;
                                    txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                                }
                                else if (dt_load.Rows[0]["Question_Output"].ToString() == "False")
                                {
                                    rdo_No.Checked = true;
                                    txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                                }
                                lbl_Question_No.Text = dt_load.Rows[0]["Question_no"].ToString();
                                lbl_Question.Text = dt_load.Rows[0]["Question"].ToString();

                            }


                            previous_quest = previous_quest - 1;

                            btn_Next.Enabled = true;
                        }
                    }

                }
                else
                {

                    ht_load.Add("@Trans", "CHECK_PREVIOUS_QUESTION1");
                    ht_load.Add("@Order_Task", Task_id);
                    ht_load.Add("@Question_No", lbl_Question_No.Text);
                    //ht_load.Add("@Client", clientid);
                    //ht_load.Add("@Subclient", subprocessid);
                    ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                    ht_load.Add("@Order_Id", Order_Id);
                    ht_load.Add("@User_id", User_id);
                    ht_load.Add("@Work_Type", Work_Type_Id);
                    dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                    if (dt_load.Rows.Count > 0 && dt_load.Rows[0]["Question_Sequence_Id"].ToString() != "")
                    {
                        previous_quest = int.Parse(dt_load.Rows[0]["Question_Sequence_Id"].ToString());


                        ht_load.Clear(); dt_load.Clear();
                        ht_load.Add("@Trans", "LOAD_PREVIOUS_QUESTION1");
                        ht_load.Add("@Question_Sequence_Id", previous_quest - 1);
                        ht_load.Add("@Order_Task", Task_id);
                        ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                        ht_load.Add("@Order_Id", Order_Id);
                        ht_load.Add("@User_id", User_id);
                        ht_load.Add("@Work_Type", Work_Type_Id);
                        dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                        if (dt_load.Rows.Count > 0)
                        {
                            if (dt_load.Rows[0]["Question_Output"].ToString() == "True")
                            {
                                rdo_Yes.Checked = true;
                                txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                            }
                            else if (dt_load.Rows[0]["Question_Output"].ToString() == "False")
                            {
                                rdo_No.Checked = true;
                                txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                            }
                            lbl_Question_No.Text = dt_load.Rows[0]["Question_no"].ToString();
                            lbl_Question.Text = dt_load.Rows[0]["Question"].ToString();

                        }


                        previous_quest = previous_quest - 1;
                        //}
                        btn_Next.Enabled = true;
                    }
                    else
                    {
                        ht_load.Clear(); dt_load.Clear();
                        //PREVIOUS_QUESTION1
                        ht_load.Add("@Trans", "PREVIOUS_QUESTION1");
                        ht_load.Add("@Order_Task", Task_id);
                        ht_load.Add("@Question_No", lbl_Question_No.Text);
                        ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                        ht_load.Add("@Order_Id", Order_Id);
                        ht_load.Add("@User_id", User_id);
                        ht_load.Add("@Work_Type", Work_Type_Id);
                        dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                        if (dt_load.Rows.Count > 0)
                        {
                            previous_quest = int.Parse(dt_load.Rows[0]["Question_Sequence_Id"].ToString());

                            ht_load.Clear(); dt_load.Clear();
                            ht_load.Add("@Trans", "LOAD_PREVIOUS_QUESTION1");
                            ht_load.Add("@Question_Sequence_Id", previous_quest );
                            ht_load.Add("@Order_Task", Task_id);
                            ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
                            ht_load.Add("@Order_Id", Order_Id);
                            ht_load.Add("@User_id", User_id);
                            ht_load.Add("@Work_Type", Work_Type_Id);
                            dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
                            if (dt_load.Rows.Count > 0)
                            {
                                if (dt_load.Rows[0]["Question_Output"].ToString() == "True")
                                {
                                    rdo_Yes.Checked = true;
                                    txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                                }
                                else if (dt_load.Rows[0]["Question_Output"].ToString() == "False")
                                {
                                    rdo_No.Checked = true;
                                    txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                                }
                                lbl_Question_No.Text = dt_load.Rows[0]["Question_no"].ToString();
                                lbl_Question.Text = dt_load.Rows[0]["Question"].ToString();

                            }


                            previous_quest = previous_quest - 1;

                            btn_Next.Enabled = true;
                        }
                    }
                }

            }

        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            previous_quest = previous_quest + 1;
            Hashtable ht_load = new Hashtable();
            DataTable dt_load = new DataTable();
            ht_load.Add("@Trans", "LOAD_PREVIOUS_QUESTION1");
            ht_load.Add("@Question_Sequence_Id", previous_quest);
            ht_load.Add("@Order_Task", Task_id);
            //ht_load.Add("@Client", clientid);
            //ht_load.Add("@Subclient", subprocessid);
            ht_load.Add("@Order_Type_ABS", Order_Type_ABS);
            ht_load.Add("@Order_Id", Order_Id);
            ht_load.Add("@User_id", User_id);
            ht_load.Add("@Work_Type", Work_Type_Id);
            dt_load = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_load);
            if (dt_load.Rows.Count > 0)
            {
                if (dt_load.Rows[0]["Question_Output"].ToString() == "True")
                {
                    rdo_Yes.Checked = true;
                    txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                }
                else if (dt_load.Rows[0]["Question_Output"].ToString() == "False")
                {
                    rdo_No.Checked = true;
                    txt_Comments.Text = dt_load.Rows[0]["Comments"].ToString();
                }
                lbl_Question_No.Text = dt_load.Rows[0]["Question_no"].ToString();
                lbl_Question.Text = dt_load.Rows[0]["Question"].ToString();

            }
            else
            {
                MessageBox.Show("This is the question you last entered give Submit to load next question");
            }


        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete all answered questions?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Hashtable htentercount = new Hashtable();
                DataTable dtentercount = new DataTable();
                htentercount.Add("@Trans", "COUNT_NO_QUESTION_USER_ENTERED");
                htentercount.Add("@Order_Status", Task_id);
                htentercount.Add("@Order_Id", Order_Id);
                htentercount.Add("@User_id", User_id);
                htentercount.Add("@Order_Type_ABS", Order_Type_ABS);
                dtentercount = dataaccess.ExecuteSP("Sp_Check_List", htentercount);
                if (dtentercount.Rows.Count > 0)
                {
                    User_count = int.Parse(dtentercount.Rows[0]["count"].ToString());
                }
                for (int i = 0; i <= User_count; i++)
                {
                    Hashtable ht_del = new Hashtable();
                    DataTable dt_del = new DataTable();
                    ht_del.Add("@Trans", "DELETE");
                    ht_del.Add("@User_id", User_id);
                    ht_del.Add("@Order_Task", Task_id);
                    ht_del.Add("@Order_Id", Order_Id);
                    ht_del.Add("@Order_Type_ABS", Order_Type_ABS);
                    ht_del.Add("@Work_Type", Work_Type_Id);
                    dt_del = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_del);
                }
                this.Close();

                
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == "Employee_Order_Entry")
                    {
                        IsOpen = true;
                        f.Focus();
                        f.Enabled = true;
                        break;
                    }
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void TypingTask_Question_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Complete_check == 0)
            {
                
            }
        }


        protected override void OnKeyPress(KeyPressEventArgs ex)
        {
           
        }

        private void txt_Comments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                    btn_submit_Click(sender, e);
                }
                
            }
        }

       
       
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
           
        }

        private void rdo_Yes_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Questions_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:

                    if (e.KeyData == Keys.Left)
                    {

                        rdo_Yes.Checked = true;

                    }
                    else if (e.KeyData == Keys.Right)
                    {

                        rdo_No.Checked = true;
                    }
                    else if (e.KeyData == Keys.Down)
                    {

                        Submiit_Data();
                    }
                    else if (e.KeyData == Keys.Up)
                    {

                        rdo_Yes.Checked = true;
                    }


                    break;
            }
        }

        private void rdo_Yes_KeyPress(object sender, KeyPressEventArgs e)
        {
            Submiit_Data();
        }


        //Copying Source File Into Destional Folder
        private void Copy_Check_List_To_Server()
        {
           cProbar.startProgress();
           //form_loader.Start_progres();
            string Source = @"\\192.168.12.33\OMS-REPORTS\Order Check List Report.pdf";

            if (Work_Type_Id == 1)
            {
                File_Name = "" + Order_Id + "-" + Task_Type.ToString() + "CheckList Report" + ".pdf";
            }
            else if (Work_Type_Id == 2)
            {

                File_Name = "" + Order_Id + " - " + " REWORK " + Task_Type.ToString() + "CheckList" + ".pdf";
            }
            else if (Work_Type_Id ==3)
            {

                File_Name = "" + Order_Id + " - " + " SUPER QC " + Task_Type.ToString() + "CheckList" + ".pdf";
            }
            Path1 = @"\\192.168.12.33\oms\" + CLIENT_NAME + @"\" + SUB_CLIENT_NAME + @"\" + Order_Id + @"\" + File_Name;
            DirectoryEntry de = new DirectoryEntry(Path1, "administrator", "password1$");
            de.Username = "administrator";
            de.Password = "password1$";
            Directory.CreateDirectory(@"\\192.168.12.33\oms\" + CLIENT_NAME + @"\" + SUB_CLIENT_NAME + @"\" + Order_Id.ToString());
            File.Copy(Source, Path1, true);
            CR_Report();
           cProbar.stopProgress();
        }


        //Loadig Report To Save

        private void CR_Report()
        {
          
            rptDoc = new Reports.CrystalReport.Order_Check_List1();
        
            Logon_Cr();

            rptDoc.SetParameterValue("@Trans", "SELECT_USER_TASK_WISE");
            rptDoc.SetParameterValue("@Order_Id", Order_Id);
            rptDoc.SetParameterValue("@Task",ORDER_STATUS);
            rptDoc.SetParameterValue("@From_date", 0);
            rptDoc.SetParameterValue("@To_date", 0);
            rptDoc.SetParameterValue("@Client_Id", 0);
            rptDoc.SetParameterValue("@Sub_Client_Id", 0);
            rptDoc.SetParameterValue("@User_Id", User_id);
            rptDoc.SetParameterValue("@Log_In_Userid", User_id);
            rptDoc.SetParameterValue("@Work_Type_Id", Work_Type_Id);

            ExportOptions CrExportOptions;
            FileInfo newFile = new FileInfo(Path1);

            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();

            PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
            CrExportOptions = rptDoc.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            rptDoc.Export();

            Hashtable htorderkb = new Hashtable();
            DataTable dtorderkb = new DataTable();

            htorderkb.Add("@Trans", "INSERT");
            if (Work_Type_Id == 1)
            {
                htorderkb.Add("@Instuction", "" + Task_Type.ToString() + "Check List Report");
            }
            else if (Work_Type_Id == 2)
            {
                htorderkb.Add("@Instuction", "REWORK -" + Task_Type.ToString() + "Check List Report");

            }
            else if (Work_Type_Id == 2)
            {
                htorderkb.Add("@Instuction", "SUPER QC -" + Task_Type.ToString() + "Check List Report");

            }
            htorderkb.Add("@Order_ID", Order_Id);
            htorderkb.Add("@Document_Name", File_Name);
            htorderkb.Add("@Document_Path", Path1);
            htorderkb.Add("@Inserted_By", User_id);
            htorderkb.Add("@Inserted_date", DateTime.Now);
            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);







        }
        private void Logon_Cr()
        {
            crConnectionInfo.ServerName = server;
            crConnectionInfo.DatabaseName = Db;
            crConnectionInfo.UserID = Username;
            crConnectionInfo.Password = Password;
            CrTables = rptDoc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }


        }

       
    }
}
