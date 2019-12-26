using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;
using System.DirectoryServices;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01
{
    public partial class CheckList : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        int user_ID, Check, Check_List_Tran_ID=0;
        string User_Name;
        bool check_yes, check_no;
        int Ref_Checklist_Master_Type_Id, Checklist_Id, User_id, Order_Id, Order_Task, OrderType_ABS_Id, Chklist_Client_Trans_ID;
        string Comments, Question;
        int Entered_Count,Question_Count;
        int   User_count, Available_count; string Task_id, Order_Type, Order_Type_ABS, Task_Type;
       
      
        ReportDocument rpt_Doc = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        string[] FName;
        string Document_Name;

        bool IsOpen = false;

        string Path1;
        string File_Name;
        string Directory_Path;

        string VIew_Type;
        string ORDER_NUMBER, CLIENT_NAME, SUB_CLIENT_NAME;
        int ORDER_STATUS, clientid, subprocessid;
        int Work_Type_Id;
        string ordernumber, ordertasktype;
        int Error_Count = 0;
        int Error_Tab_Count = 0;
        int Select_Tab_Index;
        int Check_Count = 0;
        int Defined_Tab_Index = 0;
      public CheckList(int userid, int Clientid, int Subprocessid, string Taskid, string order_type, string tasktype, int orderid, int user_count, int available_count, string Order_Number, string client_Name, string Sub_Client_Name, int Order_Status, int WORK_TYPE_ID, string order_no, string order_task_type,int Ordertype_ABS_ID)
        {
            InitializeComponent();

            //user_ID = User_id;
           // User_Name = Username;
            //Order_Id = orderid;
            //Order_Task = Order_Status;
            //OrderType_ABS_Id = Ordertype_ABS_id;
            //Work_Type_Id=  WorkType_ID;
            //Client_ID = Client_id;
            //role_Id = roleid;
            //CLIENT_NAME = Client_Name;

            clientid = Clientid;
            subprocessid = Subprocessid;
            user_ID = userid;
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
            OrderType_ABS_Id = Ordertype_ABS_ID;
            Order_Task = Order_Status;
        }

        

        private void CheckList_Load(object sender, EventArgs e)
        {
            
            //tabPage1.Focus();

            //tabPage2.Enabled = true;

            
            //Grid_Bind_General_CheckList();
            //Grid_Bind_Assessor_Taxes_CheckList();
            //Grid_Bind_Deed_CheckList();
            //Grid_Bind_Mortgage_CheckList();
            //Grid_Bind_JudgmentLiens_CheckList();
            //Grid_Bind_Others_CheckList();


            //21-07-2017

           // Grid_Bind_All_General();
            //Grid_Bind_All_AssessorTax();
            //Grid_Bind_All_Deed();
            //Grid_Bind_All_Mortgage();
            //Grid_Bind_All_JudgmLien();
            //Grid_Bind_All_Others();

           // Grid_Bind_All_Clients();


            // 09/08/2017

            Bind_GenralView();
            Bind_AssessorView();
            Bind_DeedView();
            Bind_MortgageView();
            Bind_JudgmentLienView();
            Bind_OthersView();
            Grid_Bind_All_Clients();
            Bind_Client_View();

        }



        private void Bind_Client_View()
        {
            Hashtable ht_Check = new Hashtable();
            DataTable dt_Check = new System.Data.DataTable();

            ht_Check.Add("@Trans", "CHECK_ORDER_ID_TASK_USER_WISE");
            ht_Check.Add("@Ref_Checklist_Master_Type_Id", 7);
            ht_Check.Add("@Order_Id", Order_Id);
            ht_Check.Add("@Order_Task", Order_Task);
            ht_Check.Add("@User_id", user_ID);

            ht_Check.Add("@Work_Type", Work_Type_Id);
            dt_Check = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Check);
            if (dt_Check.Rows.Count != 0)
            {
                Client_View();
            }
            else
            {

                Grid_Bind_All_Clients();
            }
        }

        private void Grid_Bind_All_Clients()
        {
            Hashtable ht_Clnt = new Hashtable();
            DataTable dt_Clnt = new System.Data.DataTable();

            ht_Clnt.Add("@Trans", "GET_CLIENT_DETAILS");
            ht_Clnt.Add("@Ref_Checklist_Master_Type_Id", 7);
            ht_Clnt.Add("@Order_Task", Order_Task);
            ht_Clnt.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
            ht_Clnt.Add("@Client_Id", clientid);
            dt_Clnt = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_Clnt);
            if (dt_Clnt.Rows.Count > 0)
            {
                grd_Client_Specification.Rows.Clear();
                for (int i = 0; i < dt_Clnt.Rows.Count; i++)
                {
                    grd_Client_Specification.Rows.Add();
                    grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                    grd_Client_Specification.Rows[i].Cells[2].Value = dt_Clnt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[3].Value = dt_Clnt.Rows[i]["Question"].ToString();
                    grd_Client_Specification.Rows[i].Cells[4].Value = dt_Clnt.Rows[i]["Checklist_Id"].ToString();

                    grd_Client_Specification.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_Specification.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_Specification.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                }
            }
            else
            {
                grd_Client_Specification.Rows.Clear();
            }
        }

        private void Client_View()
        {
            Hashtable ht_Client_List = new Hashtable();
            DataTable dt_Client_List = new DataTable();

            //ht_Client_List.Add("@Trans", "GET_ALL_CLIENT_VIEW");
            //ht_Client_List.Add("@Ref_Checklist_Master_Type_Id", 7);
            //ht_Client_List.Add("@Client_Id", Client_ID);
            //ht_Client_List.Add("@Order_Task", Order_Task);
            //ht_Client_List.Add("@Order_Id", Order_Id);
            //ht_Client_List.Add("@User_Id", user_ID);

            //dt_Client_List = dataaccess.ExecuteSP("Sp_CheckList_ClientSpecification_Detail", ht_Client_List);

            ht_Client_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Client_List.Add("@Ref_Checklist_Master_Type_Id", 7);
            ht_Client_List.Add("@Order_Task", Order_Task);
            ht_Client_List.Add("@Order_Id", Order_Id);
            ht_Client_List.Add("@User_Id", user_ID);
            ht_Client_List.Add("@Work_Type", Work_Type_Id);
            dt_Client_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Client_List);
            if (dt_Client_List.Rows.Count > 0)
            {
                grd_Client_Specification.Rows.Clear();
                for (int i = 0; i < dt_Client_List.Rows.Count; i++)
                {
                    grd_Client_Specification.Rows.Add();
                    grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                    grd_Client_Specification.Rows[i].Cells[1].Value = dt_Client_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Client_Specification.Rows[i].Cells[2].Value = dt_Client_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[3].Value = dt_Client_List.Rows[i]["Question"].ToString();
                    grd_Client_Specification.Rows[i].Cells[4].Value = dt_Client_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[5].Value = dt_Client_List.Rows[i]["Yes"].ToString();
                    grd_Client_Specification.Rows[i].Cells[6].Value = dt_Client_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Client_Specification.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Client_Specification.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Client_Specification[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Client_Specification[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Client_Specification[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Client_Specification[6, i].Value = null;
                    }
                    grd_Client_Specification.Rows[i].Cells[7].Value = dt_Client_List.Rows[i]["Comments"].ToString();
                }
            }
            else
            {
                grd_Client_Specification.Rows.Clear();

                Grid_Bind_All_Clients();
            }
        }


        // General


        private void Bind_GenralView()
        {
            Hashtable ht_Check = new Hashtable();
            DataTable dt_Check = new System.Data.DataTable();

            ht_Check.Add("@Trans", "CHECK_ORDER_ID_TASK_USER_WISE");
            ht_Check.Add("@Ref_Checklist_Master_Type_Id", 1);
            ht_Check.Add("@Order_Id", Order_Id);
            ht_Check.Add("@Order_Task", Order_Task);
            ht_Check.Add("@User_id", user_ID);
            ht_Check.Add("@Work_Type", Work_Type_Id);
           
             
            dt_Check = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Check);
            if (dt_Check.Rows.Count !=0)
            {
                General_View();
            }
            else
            {

                Grid_Bind_All_General();
            }
        }

        public void Grid_Bind_All_General()
        {
                Hashtable ht = new Hashtable();
                DataTable dt = new System.Data.DataTable();

                ht.Add("@Trans", "GET_ALL_DETAILS");
                ht.Add("@Ref_Checklist_Master_Type_Id", 1);
                ht.Add("@Order_Task", Order_Task);
                ht.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
                if (dt.Rows.Count > 0)
                {
                    grd_General_Checklist.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_General_Checklist.Rows.Add();
                        grd_General_Checklist.Rows[i].Cells[0].Value = i + 1;
                        grd_General_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                        grd_General_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
                        grd_General_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();

                        grd_General_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_General_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_General_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                }
                else
                {
                    grd_General_Checklist.Rows.Clear();
                }
                      
        }

        private void General_View()
        {
            Hashtable ht_general_list = new Hashtable();
            DataTable dt_general_list = new DataTable();

            ht_general_list.Add("@Trans", "GET_ALL_VIEW");
            ht_general_list.Add("@Ref_Checklist_Master_Type_Id", 1);
            ht_general_list.Add("@Order_Task", Order_Task);
            ht_general_list.Add("@Order_Id", Order_Id);
            ht_general_list.Add("@User_Id", user_ID);
            ht_general_list.Add("@Work_Type", Work_Type_Id);

            dt_general_list = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_general_list);
            if (dt_general_list.Rows.Count > 0)
            {
                grd_General_Checklist.Rows.Clear();
                for (int i = 0; i < dt_general_list.Rows.Count; i++)
                {
                    grd_General_Checklist.Rows.Add();
                    grd_General_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_General_Checklist.Rows[i].Cells[1].Value = dt_general_list.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_General_Checklist.Rows[i].Cells[2].Value = dt_general_list.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_General_Checklist.Rows[i].Cells[3].Value = dt_general_list.Rows[i]["Question"].ToString();
                    grd_General_Checklist.Rows[i].Cells[4].Value = dt_general_list.Rows[i]["Checklist_Id"].ToString();
                    grd_General_Checklist.Rows[i].Cells[5].Value = dt_general_list.Rows[i]["Yes"].ToString();
                    grd_General_Checklist.Rows[i].Cells[6].Value = dt_general_list.Rows[i]["No"].ToString();

                    string chk_yes = grd_General_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_General_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_General_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_General_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_General_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_General_Checklist[6, i].Value = null;
                    }
                    grd_General_Checklist.Rows[i].Cells[7].Value = dt_general_list.Rows[i]["Comments"].ToString();

                    grd_General_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_General_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_General_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_General_Checklist.Rows.Clear();
                Grid_Bind_All_General();
            }
        }

        //public void Grid_Bind_General_CheckList()
        //{
        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    //ht.Add("@Trans", "SELECT");
        //    //ht.Add("@Ref_Checklist_Master_Type_Id", 1);

        //    ht.Add("@Trans", "GET_GENERAL_DETAILS");
        //    ht.Add("@Ref_Checklist_Master_Type_Id", 1);
        //    ht.Add("@Order_Task", 2);
        //    ht.Add("@OrderType_ABS_Id", 1);
        //    dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
        //    if (dt.Rows.Count > 0)
        //    {
        //        grd_General_Checklist.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            grd_General_Checklist.Rows.Add();
        //            grd_General_Checklist.Rows[i].Cells[0].Value = i + 1;
        //            grd_General_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_General_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
        //            grd_General_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_General_Checklist.Rows.Clear();
        //    }
        //}

        private void Save_General_List()
        {
            int inertval = 0;
            int error = 0;
            int comm_error = 0;
            int empty = 0;

         
            for (int i = 0; i < grd_General_Checklist.Rows.Count; i++)
            {
                
                    grd_General_Checklist.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                   
                    bool chk_yes = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells["Column5"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells["Column7"].FormattedValue);

                    if (chk_yes != null && chk_yes != false)
                    {
                        chk_yes = true;
                    }
                    else
                    {
                        chk_yes = false;
                    }
                    if (chk_no != null && chk_no != false)
                    {
                        chk_no = true;
                    }
                    else
                    {
                        chk_no = false;
                    }

                   
                    if (grd_General_Checklist[7, i].Value == null || grd_General_Checklist[7, i].Value == "")
                    {
                        if (chk_no == true)
                        {
                            grd_General_Checklist[7, i].Style.BackColor = Color.Red;
                        
                        }

                        Comments = "";
                       

                    }
                    else
                    {
                        grd_General_Checklist[7, i].Style.BackColor = Color.White;
                        Comments = grd_General_Checklist.Rows[i].Cells[7].Value.ToString();
                    }

                   
                 

                    if (grd_General_Checklist.Rows[i].Cells[5].Style.BackColor != Color.Red && grd_General_Checklist.Rows[i].Cells[6].Style.BackColor != Color.Red)
                    {
                        if (grd_General_Checklist[7, i].Style.BackColor != Color.Red)
                        {
                            //  Check_List_Tran_ID = int.Parse(grd_General_Checklist.Rows[i].Cells[1].Value.ToString());
                            Ref_Checklist_Master_Type_Id = int.Parse(grd_General_Checklist.Rows[i].Cells[2].Value.ToString());
                            Checklist_Id = int.Parse(grd_General_Checklist.Rows[i].Cells[4].Value.ToString());
                            Question = grd_General_Checklist.Rows[i].Cells[3].Value.ToString();

                            Hashtable htcheck = new Hashtable();
                            DataTable dtcheck = new DataTable();
                            htcheck.Add("@Trans", "CHECK");
                            htcheck.Add("@Checklist_Id", Checklist_Id);
                            htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                         //  htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            htcheck.Add("@User_id", user_ID);
                            htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            htcheck.Add("@Order_Id", Order_Id);
                            htcheck.Add("@Order_Task", Order_Task);
                            htcheck.Add("@Work_Type", Work_Type_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);

                            if (dtcheck.Rows.Count > 0 )
                            {

                                Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
                                Hashtable ht_Chklist = new Hashtable();
                                DataTable dt_Chklist = new DataTable();

                                ht_Chklist.Add("@Trans", "UPDATE");
                                ht_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
                                ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                                ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                               //  ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                                ht_Chklist.Add("@Yes", chk_yes);
                                ht_Chklist.Add("@No", chk_no);
                                ht_Chklist.Add("@Order_Id", Order_Id);
                                ht_Chklist.Add("@Order_Task", Order_Task);
                                ht_Chklist.Add("@Work_Type", Work_Type_Id);
                                ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                                ht_Chklist.Add("@Comments", Comments);
                                ht_Chklist.Add("@Status", "True");
                                ht_Chklist.Add("@User_id", user_ID);
                                ht_Chklist.Add("@Modified_By", user_ID);
                                ht_Chklist.Add("@Modified_Date", DateTime.Now);
                                dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);

                            }
                            else if (dtcheck.Rows.Count == 0)
                            {
                                Hashtable ht_Chk_list = new Hashtable();
                                DataTable dt_Chk_list = new DataTable();

                                ht_Chk_list.Add("@Trans", "INSERT");
                                ht_Chk_list.Add("@Checklist_Id", Checklist_Id);
                                ht_Chk_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //ht_Chk_list.Add("@Question", Question);
                                ht_Chk_list.Add("@Yes", chk_yes);
                                ht_Chk_list.Add("@No", chk_no);
                                ht_Chk_list.Add("@Order_Id", Order_Id);
                                ht_Chk_list.Add("@Order_Task", Order_Task);
                                ht_Chk_list.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                                ht_Chk_list.Add("@Work_Type", Work_Type_Id);
                                ht_Chk_list.Add("@Comments", Comments);
                                ht_Chk_list.Add("@Status", "True");
                                ht_Chk_list.Add("@User_id", user_ID);
                                ht_Chk_list.Add("@Inserted_Date", DateTime.Now);

                                object dtcount = dataaccess.ExecuteSPForScalar("Sp_Checklist_Detail", ht_Chk_list);

                                int checklistId = int.Parse(dtcount.ToString());
                               
                            }
                        }
                    }
                   
                
            }

           
           
           
        }

     

        //private void Get_General_Details()
        //{
        
        //    Hashtable htget = new Hashtable();
        //    DataTable dtget = new System.Data.DataTable();
        //    htget.Add("@Trans", "GET_GENERAL_DETAILS");
        //    dtget = dataaccess.ExecuteSP("Sp_Checklist_Detail", htget);

        //    if (dtget.Rows.Count > 0)
        //    {
        //        Ref_Checklist_Master_Type_Id = int.Parse(dtget.Rows[0]["Ref_Checklist_Master_Type_Id"].ToString());
        //        Checklist_Id = int.Parse(dtget.Rows[0]["Checklist_Id"].ToString());
        //        Question = dtget.Rows[0]["Question"].ToString();
        //        Comments = dtget.Rows[0]["Comments"].ToString();
        //    }
        //}

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (Validate_Genral_Question_New() != false)
            {
               
                    Error_Tab_Count = 0;
                    tabControl1.SelectTab("tabPage2");
                
              //  Save_General_List();

            }
        }

        private bool Validate_Genral_Question()
        { 
                Hashtable htgetmax_num = new Hashtable();
                DataTable dtgetmax_num = new DataTable();

                htgetmax_num.Add("@Trans", "CHECK_COUNT");
                htgetmax_num.Add("@Order_Id", Order_Id);
                htgetmax_num.Add("@Order_Task", Order_Task);
                htgetmax_num.Add("@Ref_Checklist_Master_Type_Id", 1);
                htgetmax_num.Add("@Work_Type", Work_Type_Id);
                htgetmax_num.Add("@User_id", user_ID);
                dtgetmax_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", htgetmax_num);

                if (dtgetmax_num.Rows.Count > 0)
                {
                    Entered_Count = int.Parse(dtgetmax_num.Rows[0]["count"].ToString());
                }
                else
                {
                    Entered_Count = 0;
                }
                Question_Count = int.Parse(grd_General_Checklist.Rows.Count.ToString());

                if (Entered_Count==Question_Count && Error_Count==0)
                {
                    return true;
                }
               
                else
                {

                    Error_Count = 0;
                    Defined_Tab_Index = 1;
                    if (Defined_Tab_Index != 0)
                    {
                     
                    }
                    else
                    {
                        MessageBox.Show("Need to Enter All the Fields");

                    }
                   
                    return false;
                }
            }

        private bool Validate_Genral_Question_New()
        {

            int Checked_Cell_Count = 0;

            for (int i = 0; i < grd_General_Checklist.Rows.Count; i++)
            {



                bool chk_yes = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells["Column5"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells["Column7"].FormattedValue);




                if (chk_yes == true)
                    {
                        int check_Count = 1;
                        Checked_Cell_Count += check_Count;

                    }
                if (chk_no == true)
                    {
                        int check_Count = 1;

                        Checked_Cell_Count += check_Count;

                    }


                if (chk_yes == false && chk_no == false)
                {
                    grd_General_Checklist.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grd_General_Checklist.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    Error_Count = 1;

                    Error_Tab_Count = 1;
                }
                else
                {

                    grd_General_Checklist.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
                    grd_General_Checklist.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;
                }

                if (grd_General_Checklist[7, i].Value == null || grd_General_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_General_Checklist[7, i].Style.BackColor = Color.Red;

                    }

                    Comments = "";


                }
                else
                {
                    grd_General_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_General_Checklist.Rows[i].Cells[7].Value.ToString();
                }

                if (chk_no == true && Comments.Trim().ToString() == "")
                {
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                    grd_General_Checklist[7, i].Style.BackColor = Color.Red;
                    break;
                }
                else

                {

                    Error_Count = 0;
                }
                 


            }

            if (grd_General_Checklist.Rows.Count <= 0)
            {
                return true;
            }

            if (grd_General_Checklist.Rows.Count == Checked_Cell_Count && Error_Count!=1)
            {

                return true;
            }
            else
            {
                Error_Count = 1;
                Error_Tab_Count = 1;
                Defined_Tab_Index = 1;
                MessageBox.Show("Need to Enter all Fields");
                return false;
            }
        




        }



        private void btn_General_Save_Click(object sender, EventArgs e)
        {
            Save_General_List();
        }

        private void btn_General_View_Detail_Click(object sender, EventArgs e)
        {

            Hashtable ht_general_list = new Hashtable();
            DataTable dt_general_list = new DataTable();

            //ht_general_list.Add("@Trans", "ALL_GENERAL");
            ht_general_list.Add("@Trans", "GET_ALL_VIEW");
            ht_general_list.Add("@Ref_Checklist_Master_Type_Id", 1);
            dt_general_list = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_general_list);
            if (dt_general_list.Rows.Count > 0)
            {
                grd_General_Checklist.Rows.Clear();
                for (int i = 0; i < dt_general_list.Rows.Count; i++)
                {
                    grd_General_Checklist.Rows.Add();
                    grd_General_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_General_Checklist.Rows[i].Cells[1].Value = dt_general_list.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_General_Checklist.Rows[i].Cells[2].Value = dt_general_list.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_General_Checklist.Rows[i].Cells[3].Value = dt_general_list.Rows[i]["Question"].ToString();
                    grd_General_Checklist.Rows[i].Cells[4].Value = dt_general_list.Rows[i]["Checklist_Id"].ToString();
                    grd_General_Checklist.Rows[i].Cells[5].Value = dt_general_list.Rows[i]["Yes"].ToString();
                    grd_General_Checklist.Rows[i].Cells[6].Value = dt_general_list.Rows[i]["No"].ToString();

                    string chk_yes = grd_General_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_General_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_General_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_General_Checklist[5, i].Value = null;
                    }
                    if (chk_no =="true")
                    {
                        grd_General_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_General_Checklist[6, i].Value = null;
                    }
                    grd_General_Checklist.Rows[i].Cells[7].Value = dt_general_list.Rows[i]["Comments"].ToString();

                    grd_General_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_General_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_General_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_General_Checklist.Rows.Clear();
                Grid_Bind_All_General();
            }
        }

       

        private void grd_General_Checklist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                
                if (e.ColumnIndex == 5)
                {
                   bool chk_yes = Convert.ToBoolean(grd_General_Checklist.Rows[e.RowIndex].Cells["Column5"].FormattedValue);
                   bool chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[e.RowIndex].Cells["Column7"].FormattedValue);

                   if (chk_yes != false)
                   {
                       grd_General_Checklist[6, e.RowIndex].Value = false;
                       grd_General_Checklist[5, e.RowIndex].Value = true;

                       grd_General_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                       grd_General_Checklist[7, e.RowIndex].ReadOnly = true;
                       

                   }
                   else if (chk_yes != true)
                   {
                       grd_General_Checklist[5, e.RowIndex].Value = true;
                       grd_General_Checklist[6, e.RowIndex].Value = false;
                       grd_General_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                       grd_General_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                       grd_General_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                       //MessageBox.Show("Enter Comments");
                       grd_General_Checklist[7, e.RowIndex].ReadOnly = false;
                       grd_General_Checklist[7, e.RowIndex].Value = "";
                   }
                
                }
            
                if (e.ColumnIndex == 6)
                {
                   bool chk_no = false;
                   chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[e.RowIndex].Cells["Column7"].FormattedValue);
                   
                   if (chk_no != false)
                   {
                       grd_General_Checklist[6, e.RowIndex].Value = true;
                       grd_General_Checklist[5, e.RowIndex].Value = false;
                       grd_General_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                      grd_General_Checklist[7, e.RowIndex].ReadOnly = true;

                      if (chk_no != false)
                      {
                          grd_General_Checklist[6, e.RowIndex].Value = false;
                          grd_General_Checklist[5, e.RowIndex].Value = false;
                          grd_General_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                          grd_General_Checklist[7, e.RowIndex].ReadOnly = true;
                          grd_General_Checklist[7, e.RowIndex].Value = "";
                      }

                   }
                   else if (chk_no != true)
                   {
                       grd_General_Checklist[5, e.RowIndex].Value = false;
                       grd_General_Checklist[6, e.RowIndex].Value = true;
                       grd_General_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                       grd_General_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                       grd_General_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                       grd_General_Checklist[7, e.RowIndex].ReadOnly = false;
                       grd_General_Checklist[7, e.RowIndex].Value = "";
                   }

                }

                if (e.ColumnIndex == 7)
                {
                    

                    bool chk_yes = Convert.ToBoolean(grd_General_Checklist.Rows[e.RowIndex].Cells["Column5"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[e.RowIndex].Cells["Column7"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_General_Checklist[7, e.RowIndex].ReadOnly = true;
                        grd_General_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                       
                    }
                    else if (chk_yes != true)
                    {
                     
                        grd_General_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_General_Checklist[7, e.RowIndex].Style.BackColor = Color.White; 
                     
                    }
                 
                    if (chk_yes == false && chk_no == false)
                    {
                       
                        grd_General_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                        grd_General_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        grd_General_Checklist[7, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        grd_General_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                        grd_General_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
                    }

                 
                   
                }
            }
           
        }

        private void grd_General_Checklist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex != -1)
            //    {
            //       // bool chk_yes =Convert.ToBoolean(grd_General_Checklist.Rows[e.RowIndex].Cells["Column5"].Value);
            //       // bool chk_no =Convert.ToBoolean(grd_General_Checklist.Rows[e.RowIndex].Cells["Column7"].Value);
            //       //// bool check = false;
            //       // if (chk_yes != null && chk_yes != false)
            //       // {
            //       //   //  check = true;
            //       //     grd_General_Checklist.Rows[e.RowIndex].Cells["Column7"].ReadOnly = true;
            //       //     grd_General_Checklist.Rows[e.RowIndex].Cells["Column6"].ReadOnly = true;
            //       // }
            //       // else if (chk_yes!=true)
            //       // {
            //       //     grd_General_Checklist.Rows[e.RowIndex].Cells["Column7"].ReadOnly = false;
            //       // }
            //       // if (chk_no != null && chk_no != false)
            //       // {

            //       //     //check = false;
            //       //     grd_General_Checklist.Rows[e.RowIndex].Cells["Column5"].ReadOnly = true;
            //       //     grd_General_Checklist.Rows[e.RowIndex].Cells["Column6"].ReadOnly = false;   // comments column
            //       // }
            //       // else if (chk_no!=true)
            //       // {
            //       //     grd_General_Checklist.Rows[e.RowIndex].Cells["Column5"].ReadOnly = false;
            //       // }

            //    }
            //}
         }

        private void TextboxNumeric_KeyPress1(object sender, KeyPressEventArgs e)
        {
            //Boolean nonNumberEntered;
            //nonNumberEntered = true;
            //if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            //{
            //    nonNumberEntered = false;
            //}
            //if (nonNumberEntered == true)
            //{
            //    // Stop the character from being entered into the control since it is non-numerical.
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Handled = false;
            //}


            //for (int i = 0; i < grd_General_Checklist.Rows.Count; i++)
            //{
            //    bool chk_yes = Convert.ToBoolean(grd_General_Checklist.Rows[0].Cells[5].FormattedValue);
            //    bool chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[0].Cells[7].FormattedValue);
            //    this.grd_General_Checklist.Rows[0].Cells[7].ReadOnly = true;

            //    if (chk_yes != false && chk_no == false)
            //    {
            //        grd_General_Checklist[7,i].ReadOnly = true;
            //    }
            //}
          

            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{

            //    e.Handled = true;
            //}
            

        }


        private void grd_General_Checklist_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {


            //if ((char)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 7)
            //{
            //    e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);

            //}
           
        }


        // Assessor and taxes



        private void Bind_AssessorView()
        {
            Hashtable ht_Check = new Hashtable();
            DataTable dt_Check = new System.Data.DataTable();

            ht_Check.Add("@Trans", "CHECK_ORDER_ID_TASK_USER_WISE");
            ht_Check.Add("@Ref_Checklist_Master_Type_Id", 2);
            ht_Check.Add("@Order_Id", Order_Id);
            ht_Check.Add("@Order_Task", Order_Task);
            ht_Check.Add("@User_id", user_ID);
            ht_Check.Add("@Work_Type", Work_Type_Id);

            dt_Check = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Check);
            if (dt_Check.Rows.Count > 0)
            {
                Assessor_View();
            }
            else
            {

                Grid_Bind_All_AssessorTax();
            }
        }

        private void Assessor_View()
        {
            Hashtable ht_Asses_Tax_List = new Hashtable();
            DataTable dt_Asses_Tax_List = new DataTable();
            ht_Asses_Tax_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Asses_Tax_List.Add("@Ref_Checklist_Master_Type_Id", 2);
            ht_Asses_Tax_List.Add("@Order_Task", Order_Task);
            ht_Asses_Tax_List.Add("@Order_Id", Order_Id);
            ht_Asses_Tax_List.Add("@User_Id", user_ID);
            ht_Asses_Tax_List.Add("@Work_Type", Work_Type_Id);
            dt_Asses_Tax_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Asses_Tax_List);

            if (dt_Asses_Tax_List.Rows.Count > 0)
            {
                grd_AssessorTaxes_Chklist.Rows.Clear();
                for (int i = 0; i < dt_Asses_Tax_List.Rows.Count; i++)
                {
                    grd_AssessorTaxes_Chklist.Rows.Add();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[0].Value = i + 1;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[1].Value = dt_Asses_Tax_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[2].Value = dt_Asses_Tax_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[3].Value = dt_Asses_Tax_List.Rows[i]["Question"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[4].Value = dt_Asses_Tax_List.Rows[i]["Checklist_Id"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Value = dt_Asses_Tax_List.Rows[i]["Yes"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Value = dt_Asses_Tax_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "True")
                    {
                        grd_AssessorTaxes_Chklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_AssessorTaxes_Chklist[5, i].Value = null;
                    }
                    if (chk_no == "False")
                    {
                        grd_AssessorTaxes_Chklist[6, i].Value = false;
                    }
                    else if (chk_no == "")
                    {
                        grd_AssessorTaxes_Chklist[6, i].Value = null;
                    }
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value = dt_Asses_Tax_List.Rows[i]["Comments"].ToString();


                    grd_AssessorTaxes_Chklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_AssessorTaxes_Chklist.Rows.Clear();
                Grid_Bind_All_AssessorTax();
            }

        }


        public void Grid_Bind_All_AssessorTax()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_ALL_DETAILS");
            ht.Add("@Ref_Checklist_Master_Type_Id", 2);
            ht.Add("@Order_Task", Order_Task);
            ht.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
            dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
            if (dt.Rows.Count > 0)
            {
                grd_AssessorTaxes_Chklist.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_AssessorTaxes_Chklist.Rows.Add();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[0].Value = i + 1;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();

                    grd_AssessorTaxes_Chklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_AssessorTaxes_Chklist.Rows.Clear();
            }
        }

        //public void Grid_Bind_Assessor_Taxes_CheckList()
        //{

        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    //ht.Add("@Trans", "SELECT");
        //    //ht.Add("@Ref_Checklist_Master_Type_Id", 2);

        //    ht.Add("@Trans", "GET_ASSESSOR_TAX_DETAILS");
        //    ht.Add("@Order_Task", 2);
        //    ht.Add("@OrderType_ABS_Id", 1);
        //    dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
        //    if (dt.Rows.Count > 0)
        //    {
        //        grd_AssessorTaxes_Chklist.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            grd_AssessorTaxes_Chklist.Rows.Add();
        //            grd_AssessorTaxes_Chklist.Rows[i].Cells[0].Value = i + 1;
        //            grd_AssessorTaxes_Chklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_AssessorTaxes_Chklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
        //            grd_AssessorTaxes_Chklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_AssessorTaxes_Chklist.Rows.Clear();

        //    }
        //}

        private bool Validate_AssessorTax_Question()
        {

            Hashtable htgetmax_num = new Hashtable();
            DataTable dtgetmax_num = new DataTable();

            htgetmax_num.Add("@Trans", "CHECK_COUNT");
            htgetmax_num.Add("@Ref_Checklist_Master_Type_Id", 2);
            htgetmax_num.Add("@Order_Id", Order_Id);
            htgetmax_num.Add("@Order_Task", Order_Task);
            htgetmax_num.Add("@Work_Type", Work_Type_Id);
            htgetmax_num.Add("@User_id", user_ID);
            dtgetmax_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", htgetmax_num);

            if (dtgetmax_num.Rows.Count > 0)
            {
                Entered_Count = int.Parse(dtgetmax_num.Rows[0]["count"].ToString());
            }
            else
            {
                Entered_Count = 0;
               
            }
            Question_Count = int.Parse(grd_AssessorTaxes_Chklist.Rows.Count.ToString());

            if (Entered_Count == Question_Count && Error_Count == 0)
            {
               
                return true;
            }
            else
            {
             
               // tabControl1.SelectTab("tabPage2");
               // tabControl1.TabPages[1].Focus();

                Error_Count = 0;
                Defined_Tab_Index = 2;
                
                if (Defined_Tab_Index != 0)
                {
                }
                else
                {
                    MessageBox.Show("Need to Enter All the Fields");
                }

                return false;
            }
        }

        private bool Validate_AssessorTax_New()
        {
            int Checked_Cell_Count = 0;

            for (int i = 0; i < grd_AssessorTaxes_Chklist.Rows.Count; i++)
            {



                bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column14"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column15"].FormattedValue);




                if (chk_yes == true)
                {
                    int check_Count = 1;
                    Checked_Cell_Count += check_Count;

                }
                if (chk_no == true)
                {
                    int check_Count = 1;

                    Checked_Cell_Count += check_Count;

                }


                if (chk_yes == false && chk_no == false)
                {
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                }
                else
                {
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;

                }

                if (grd_AssessorTaxes_Chklist[7, i].Value == null || grd_AssessorTaxes_Chklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_AssessorTaxes_Chklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";

                }
                else
                {
                    grd_AssessorTaxes_Chklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value.ToString();
                }


                if (chk_no == true && Comments.Trim().ToString() == "")
                {
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                    grd_AssessorTaxes_Chklist[7, i].Style.BackColor = Color.Red;
                }

                else
                {

                    Error_Count = 0;
                }




            }

            if (grd_AssessorTaxes_Chklist.Rows.Count <= 0)
            {
                return true;
            }

            if (grd_AssessorTaxes_Chklist.Rows.Count == Checked_Cell_Count &&   Error_Count!=1)
            {

                return true;
            }
            else
            {
                Error_Count = 1;
                Error_Tab_Count = 1;
                Defined_Tab_Index = 2;
                MessageBox.Show("Need to Enter All the Fields");
                return false;
            }
        

        }

        private void Save_Assessor_Tax_List()
        {
            int inertval = 0;
            int error = 0;
            int comm_error = 0;
            int empty = 0;

            for (int i = 0; i < grd_AssessorTaxes_Chklist.Rows.Count; i++)
            {
                grd_AssessorTaxes_Chklist.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column14"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column15"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    chk_yes = true;
                }
                else
                {
                    chk_yes = false;
                }
                if (chk_no != null && chk_no != false)
                {
                    chk_no = true;
                }
                else
                {
                    chk_no = false;
                }

               

                if (grd_AssessorTaxes_Chklist[7, i].Value == null || grd_AssessorTaxes_Chklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_AssessorTaxes_Chklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";
                    
                }
                else
                {
                    grd_AssessorTaxes_Chklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value.ToString();
                }


               

                if (grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Style.BackColor != Color.Red && grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Style.BackColor != Color.Red)
                {
                    if (grd_AssessorTaxes_Chklist[7, i].Style.BackColor != Color.Red)
                    {
                        Ref_Checklist_Master_Type_Id = int.Parse(grd_AssessorTaxes_Chklist.Rows[i].Cells[2].Value.ToString());
                        Checklist_Id = int.Parse(grd_AssessorTaxes_Chklist.Rows[i].Cells[4].Value.ToString());
                        Question = grd_AssessorTaxes_Chklist.Rows[i].Cells[3].Value.ToString();

                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Checklist_Id", Checklist_Id);
                        htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                        htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                      
                        htcheck.Add("@User_id", user_ID);
                        htcheck.Add("@Order_Id", Order_Id);
                        htcheck.Add("@Order_Task", Order_Task);
                        htcheck.Add("@Work_Type", Work_Type_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);

                        if (dtcheck.Rows.Count > 0)
                        {

                            Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "UPDATE");
                            ht_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);

                           
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Modified_By", user_ID);
                            ht_Chklist.Add("@Modified_Date", DateTime.Now);
                            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);

                        }
                        else if (dtcheck.Rows.Count == 0)
                        {

                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "INSERT");
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);

                            object dtcount = dataaccess.ExecuteSPForScalar("Sp_Checklist_Detail", ht_Chklist);

                            int checklistId = int.Parse(dtcount.ToString());
                        }
                    }
                }
              
            }
            

        }

        private void btn_Assessor_Tax_Next_Click(object sender, EventArgs e)
        {
            //Save_AssessorTaxes_List();               //07-07-2017
            //tabControl1.SelectTab("tabPage3");          //07-07-2017

            if (Validate_AssessorTax_New() != false)
            {
                Error_Tab_Count = 0;
                tabControl1.SelectTab("tabPage3");

            }

          //  Save_Assessor_Tax_List();                         // 10-07-2017
           
        }

        private void btn_AssessorTaxe_Save_Click(object sender, EventArgs e)
        {
            int inertval = 0;
            for (int i = 0; i < grd_AssessorTaxes_Chklist.Rows.Count; i++)
            {

                if (grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value != "" && grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value != null)
                {
                    Comments = grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value.ToString();
                }
                bool check = false;
                bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column14"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column15"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    check = true;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells["Column15"].ReadOnly = true;
                }
                if (chk_no != null && chk_no != false)
                {
                    check = false;
                }
                Ref_Checklist_Master_Type_Id = int.Parse(grd_AssessorTaxes_Chklist.Rows[i].Cells[2].Value.ToString());
                Checklist_Id = int.Parse(grd_AssessorTaxes_Chklist.Rows[i].Cells[4].Value.ToString());
                Question = grd_AssessorTaxes_Chklist.Rows[i].Cells[3].Value.ToString();

                if (Check_List_Tran_ID == 0)
                {
                    Hashtable ht_Chklist = new Hashtable();
                    DataTable dt_Chklist = new DataTable();

                    ht_Chklist.Add("@Trans", "INSERT");
                    ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                    ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                     ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                    ht_Chklist.Add("@Checked", check);
                    ht_Chklist.Add("@Order_Id", Order_Id);
                    ht_Chklist.Add("@Order_Task", Order_Task);
                    ht_Chklist.Add("@Comments", Comments);
                    ht_Chklist.Add("@Status", "True");
                    ht_Chklist.Add("@User_id", user_ID);
                    ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                    dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
                    inertval = 1;
                }

            }
            if (inertval == 1)
            {
                MessageBox.Show("Assessor/Taxes CheckList Added Successfully");
                Grid_Bind_All_AssessorTax();
             
            }    
        }

        private void btn_Assessor_Liens_View_Click(object sender, EventArgs e)
        {

            Hashtable ht_Asses_Tax_List = new Hashtable();
            DataTable dt_Asses_Tax_List = new DataTable();
            //ht_Asses_Tax_List.Add("@Trans", "ALL_ASSESSOR_TAXES");
            ht_Asses_Tax_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Asses_Tax_List.Add("@Ref_Checklist_Master_Type_Id", 2);
            dt_Asses_Tax_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Asses_Tax_List);

            if (dt_Asses_Tax_List.Rows.Count > 0)
            {
                grd_AssessorTaxes_Chklist.Rows.Clear();
                for (int i = 0; i < dt_Asses_Tax_List.Rows.Count; i++)
                {
                    grd_AssessorTaxes_Chklist.Rows.Add();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[0].Value = i + 1;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[1].Value = dt_Asses_Tax_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[2].Value = dt_Asses_Tax_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[3].Value = dt_Asses_Tax_List.Rows[i]["Question"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[4].Value = dt_Asses_Tax_List.Rows[i]["Checklist_Id"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Value = dt_Asses_Tax_List.Rows[i]["Yes"].ToString();
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Value = dt_Asses_Tax_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "True")
                    {
                        grd_AssessorTaxes_Chklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_AssessorTaxes_Chklist[5, i].Value = null;
                    }
                    if (chk_no == "False")
                    {
                        grd_AssessorTaxes_Chklist[6, i].Value = false;
                    }
                    else if (chk_no == "")
                    {
                        grd_AssessorTaxes_Chklist[6, i].Value = null;
                    }
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value = dt_Asses_Tax_List.Rows[i]["Comments"].ToString();


                    grd_AssessorTaxes_Chklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_AssessorTaxes_Chklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_AssessorTaxes_Chklist.Rows.Clear();
               // Grid_Bind_Assessor_Taxes_CheckList();
                Grid_Bind_All_AssessorTax();
            }

          
        }

        private void grd_AssessorTaxes_Chklist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column14"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);
                  
                    if (chk_yes != false)
                    {
                        grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
                        grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;

                    }
                    else if (chk_yes != true)
                    {
                        grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = true;
                        grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_AssessorTaxes_Chklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_AssessorTaxes_Chklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = false;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Value = "";
                    }


                }
                if (e.ColumnIndex == 6)
                {
                    bool chk_no = false;
                    chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);

                    if(chk_no != false)
                    {
                        grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
                        grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;

                        if (chk_no != false)
                        {
                            grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
                            grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
                            grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                            grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;
                            grd_AssessorTaxes_Chklist[7, e.RowIndex].Value = "";
                        }
                    }
                    else if(chk_no != true)
                    {
                        grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
                        grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = true;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_AssessorTaxes_Chklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_AssessorTaxes_Chklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = false;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Value = "";
                    }
                }

                if (e.ColumnIndex == 7)
                {
                    bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column14"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;

                    }
                    else if (chk_yes != true)
                    {
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = false;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = Color.White;
                    }

                    if (chk_yes == false && chk_no == false)
                    {
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
                    }
                }
            }
           




            //*************11-07-2017***************

            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex == 5)
            //    {
            //        bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column14"].FormattedValue);
            //        bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);

            //        //if (chk_yes != null && chk_yes != false)
            //        //{
            //        //    grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
            //        //    grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = true;
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = false;
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;

            //        //}
            //        //else if (chk_yes != true)
            //        //{
            //        //    grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = true;
            //        //    grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
            //        //    grd_AssessorTaxes_Chklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
            //        //    grd_AssessorTaxes_Chklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
            //        //}

            //        //if (chk_yes != false)
            //        //{
            //        //    grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
            //        //    grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = true;

            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;

            //        //}
            //        //else if (chk_yes != true)
            //        //{
            //        //    grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = true;
            //        //    grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
            //        //    grd_AssessorTaxes_Chklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
            //        //    grd_AssessorTaxes_Chklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
            //        //    //MessageBox.Show("Enter Comments");
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = false;
            //        //    grd_AssessorTaxes_Chklist[7, e.RowIndex].Value = "";
            //        //}


                   

            //    }
            //    if (e.ColumnIndex == 6)
            //    {
            //        bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);
            //        if (chk_no != null && chk_no != false)
            //        {
            //            grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = true;
            //            grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;

            //        }
            //        else if (chk_no != true)
            //        {
            //            grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
            //            grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = true;
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = Color.White;
            //            grd_AssessorTaxes_Chklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
            //            grd_AssessorTaxes_Chklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = false;
            //        }

            //    }

            //    if (e.ColumnIndex == 7)
            //    {
            //        bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column14"].FormattedValue);
            //        bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);
            //        if (chk_yes != null && chk_yes != false)
            //        {
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;

            //        }
            //        else if (chk_yes != true)
            //        {

            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = false;
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].Style.BackColor = Color.White;

            //        }

            //        if (chk_yes == false && chk_no == false)
            //        {

            //            grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
            //            grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
            //            grd_AssessorTaxes_Chklist[7, e.RowIndex].ReadOnly = true;
            //        }
            //        else
            //        {
            //            grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
            //            grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
            //        }

            //    }
            //}

            //***********************************************************************
            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex == 5)
            //    {
            //        bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column14"].FormattedValue);

            //        if (chk_yes != null && chk_yes != false)
            //        {
            //            grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
            //            grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = true;
            //        }
            //        else if (chk_yes != true)
            //        {
            //            grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = true;
            //            grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = false;
            //        }
            //    }
            //    if (e.ColumnIndex == 6)
            //    {
            //        bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);
            //        if (chk_no != null && chk_no != false)
            //        {
            //            grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = true;
            //            grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
            //        }
            //        else if (chk_no != true)
            //        {
            //            grd_AssessorTaxes_Chklist[5, e.RowIndex].Value = false;
            //            grd_AssessorTaxes_Chklist[6, e.RowIndex].Value = true;
            //        }
            //    }

            //}
        }

        //private void Save_AssessorTaxes_List()
        //{
        //    for (int i = 0; i < grd_AssessorTaxes_Chklist.Rows.Count; i++)
        //    {
        //        if (grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value != "" && grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value != null)
        //        {
        //            Comments = grd_AssessorTaxes_Chklist.Rows[i].Cells[7].Value.ToString();
        //        }
        //        bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column14"].FormattedValue);
        //        bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells["Column15"].FormattedValue);

        //        if (chk_yes != null && chk_yes != false)
        //        {
        //            chk_yes = true;

        //        }
        //        else
        //        {
        //            chk_yes = false;
        //        }
        //        if (chk_no != null && chk_no != false)
        //        {
        //            chk_no = true;
        //        }
        //        else
        //        {
        //            chk_no = false;
        //        }
        //        Ref_Checklist_Master_Type_Id = int.Parse(grd_AssessorTaxes_Chklist.Rows[i].Cells[2].Value.ToString());
        //        Checklist_Id = int.Parse(grd_AssessorTaxes_Chklist.Rows[i].Cells[4].Value.ToString());
        //        Question = grd_AssessorTaxes_Chklist.Rows[i].Cells[3].Value.ToString();

        //        Hashtable htcheck = new Hashtable();
        //        DataTable dtcheck = new DataTable();
        //        htcheck.Add("@Trans", "CHECK");
        //        htcheck.Add("@Checklist_Id", Checklist_Id);
        //        htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
        //       htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
        //        htcheck.Add("@User_id", user_ID);
        //        htcheck.Add("@Order_Id", Order_Id);
        //        htcheck.Add("@Order_Task", Order_Task);

        //        dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);
        //        if (dtcheck.Rows.Count > 0)
        //        {
        //            Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
        //            Hashtable ht_Chklist = new Hashtable();
        //            DataTable dt_Chklist = new DataTable();

        //            ht_Chklist.Add("@Trans", "UPDATE");
        //            ht_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
        //            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
        //            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
        //             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
        //            ht_Chklist.Add("@Yes", chk_yes);
        //            ht_Chklist.Add("@No", chk_no);
        //            ht_Chklist.Add("@Order_Id", Order_Id);
        //            ht_Chklist.Add("@Order_Task", Order_Task);
        //            ht_Chklist.Add("@Comments", Comments);
        //            ht_Chklist.Add("@Status", "True");
        //            ht_Chklist.Add("@User_id", user_ID);
        //            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
        //            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);

        //        }
        //        else
        //        {
        //            Hashtable ht_Chklist = new Hashtable();
        //            DataTable dt_Chklist = new DataTable();

        //            ht_Chklist.Add("@Trans", "INSERT");
        //            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
        //            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
        //             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
        //            //ht_Chklist.Add("@Checked", check);
        //            ht_Chklist.Add("@Yes", chk_yes);
        //            ht_Chklist.Add("@No", chk_no);
        //            ht_Chklist.Add("@Order_Id", Order_Id);
        //            ht_Chklist.Add("@Order_Task", Order_Task);
        //            ht_Chklist.Add("@Comments", Comments);
        //            ht_Chklist.Add("@Status", "True");
        //            ht_Chklist.Add("@User_id", user_ID);
        //            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
        //            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
        //        }

        //    }
        //    if (Validate_AssessorTax_Question() != false)
        //    {
        //        tabControl1.SelectTab("tabPage3");
        //    }
        //}

        private void grd_AssessorTaxes_Chklist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex != -1)
                {
                    bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column14"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].FormattedValue);
                    bool check = false;
                    if (chk_yes != null && chk_yes != false)
                    {
                        check = true;
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column15"].ReadOnly = true;
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column16"].ReadOnly = true;

                    }
                    if (chk_no != null && chk_no != false)
                    {

                        check = false;
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column14"].ReadOnly = true;
                        grd_AssessorTaxes_Chklist.Rows[e.RowIndex].Cells["Column16"].ReadOnly = false;   // comments column
                    }

                }
            }
        }

        private void btn_Assesor_tex_Previous_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < grd_General_Checklist.Rows.Count; i++)
            //{
            //    bool chk_yes = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells["Column5"].FormattedValue);
            //    bool chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells["Column7"].FormattedValue);

            //    if (chk_yes != null && chk_yes != false)
            //    {
            //        chk_yes = true;
            //    }
            //    else
            //    {
            //        chk_yes = false;
            //    }
            //    if (chk_no != null && chk_no != false)
            //    {
            //        chk_no = true;
            //    }
            //    else
            //    {
            //        chk_no = false;
            //    }

            //    if (chk_yes == false && chk_no == false)
            //    {
            //        grd_General_Checklist.Rows[i].Cells[5].Style.BackColor = Color.Red;
            //        grd_General_Checklist.Rows[i].Cells[6].Style.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        grd_General_Checklist.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
            //        grd_General_Checklist.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;
            //    }

            //    if (grd_General_Checklist[7, i].Value == null || grd_General_Checklist[7, i].Value == "")
            //    {
            //        if (chk_no == true)
            //        {
            //            grd_General_Checklist[7, i].Style.BackColor = Color.Red;
            //        }
            //        Comments = "";

            //    }
            //    else
            //    {
            //        grd_General_Checklist[7, i].Style.BackColor = Color.White;
            //        Comments = grd_General_Checklist.Rows[i].Cells[7].Value.ToString();
            //    }


            //}



            tabControl1.SelectTab("tabPage1");
          
        }

        // Deed


        private void Bind_DeedView()
        {
            Hashtable ht_deed_Check = new Hashtable();
            DataTable dt_deed_Check = new System.Data.DataTable();

            ht_deed_Check.Add("@Trans", "CHECK_ORDER_ID_TASK_USER_WISE");
            ht_deed_Check.Add("@Ref_Checklist_Master_Type_Id", 3);
            ht_deed_Check.Add("@Order_Id", Order_Id);
            ht_deed_Check.Add("@Order_Task", Order_Task);
            ht_deed_Check.Add("@User_id", user_ID);
            ht_deed_Check.Add("@Work_Type", Work_Type_Id);
            dt_deed_Check = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_deed_Check);
            if (dt_deed_Check.Rows.Count > 0)
            {
                Deed_View();
            }
            else
            {

                Grid_Bind_All_Deed();
            }
        }

        private void Deed_View()
        {
            Hashtable ht_Deed_List = new Hashtable();
            DataTable dt_Deed_List = new DataTable();

            ht_Deed_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Deed_List.Add("@Ref_Checklist_Master_Type_Id", 3);
            ht_Deed_List.Add("@Order_Task", Order_Task);
            ht_Deed_List.Add("@Order_Id", Order_Id);
            ht_Deed_List.Add("@User_Id", user_ID);
            ht_Deed_List.Add("@Work_Type", Work_Type_Id);
            dt_Deed_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Deed_List);
            if (dt_Deed_List.Rows.Count > 0)
            {
                grd_Deed_Checklist.Rows.Clear();
                for (int i = 0; i < dt_Deed_List.Rows.Count; i++)
                {
                    grd_Deed_Checklist.Rows.Add();
                    grd_Deed_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Deed_Checklist.Rows[i].Cells[1].Value = dt_Deed_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[2].Value = dt_Deed_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[3].Value = dt_Deed_List.Rows[i]["Question"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[4].Value = dt_Deed_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[5].Value = dt_Deed_List.Rows[i]["Yes"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[6].Value = dt_Deed_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Deed_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Deed_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Deed_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Deed_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Deed_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Deed_Checklist[6, i].Value = null;
                    }
                    grd_Deed_Checklist.Rows[i].Cells[7].Value = dt_Deed_List.Rows[i]["Comments"].ToString();

                    grd_Deed_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Deed_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Deed_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            else
            {
                grd_Deed_Checklist.Rows.Clear();
                Grid_Bind_All_Deed();
            }
        }

        public void Grid_Bind_All_Deed()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_ALL_DETAILS");
            ht.Add("@Ref_Checklist_Master_Type_Id", 3);
            ht.Add("@Order_Task", Order_Task);
            ht.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
            dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Deed_Checklist.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Deed_Checklist.Rows.Add();
                    grd_Deed_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Deed_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();

                    grd_Deed_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Deed_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Deed_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Deed_Checklist.Rows.Clear();
            }
        }

        //public void Grid_Bind_Deed_CheckList()
        //{

        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    //ht.Add("@Trans", "SELECT");
        //    //ht.Add("@Ref_Checklist_Master_Type_Id", 3);

        //    ht.Add("@Trans", "GET_DEED_DETAILS");
        //    ht.Add("@Order_Task", 2);
        //    ht.Add("@OrderType_ABS_Id", 1);
        //    dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
        //    if (dt.Rows.Count > 0)
        //    {
        //        grd_Deed_Checklist.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {

        //            grd_Deed_Checklist.Rows.Add();
        //            grd_Deed_Checklist.Rows[i].Cells[0].Value = i + 1;
        //            grd_Deed_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_Deed_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
        //            grd_Deed_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_Deed_Checklist.Rows.Clear();

        //    }
        //}

        private bool Validate_Deed_Question()
        {
            Hashtable htgetmax_num = new Hashtable();
            DataTable dtgetmax_num = new DataTable();

            htgetmax_num.Add("@Trans", "CHECK_COUNT");
            htgetmax_num.Add("@Order_Id", Order_Id);
            htgetmax_num.Add("@Ref_Checklist_Master_Type_Id", 3);
            htgetmax_num.Add("@Order_Task", Order_Task);
            htgetmax_num.Add("@Work_Type", Work_Type_Id);
            htgetmax_num.Add("@User_id", user_ID);
            dtgetmax_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", htgetmax_num);

            if (dtgetmax_num.Rows.Count > 0)
            {
                Entered_Count = int.Parse(dtgetmax_num.Rows[0]["count"].ToString());
            }
            else
            {
                Entered_Count = 0;
            }
            Question_Count = int.Parse(grd_Deed_Checklist.Rows.Count.ToString());
            if (Entered_Count == Question_Count && Error_Count == 0)
            {
                return true;
            }

            else
            {

                Error_Count = 0;
                Defined_Tab_Index = 3;
                

                if (Defined_Tab_Index != 0)
                {
                }
                else
                {
                    MessageBox.Show("Need to Enter All the Fields");
                }

                return false;
            }
        }

        private bool Validate_Deed_Question_New()
        {
            int Checked_Cell_Count = 0;

            for (int i = 0; i < grd_Deed_Checklist.Rows.Count; i++)
            {



                bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column22"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column23"].FormattedValue);



                if (chk_yes == true)
                {
                    int check_Count = 1;
                    Checked_Cell_Count += check_Count;

                }
                if (chk_no == true)
                {
                    int check_Count = 1;

                    Checked_Cell_Count += check_Count;

                }


                if (chk_yes == false && chk_no == false)
                {
                    grd_Deed_Checklist.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grd_Deed_Checklist.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                }
                else
                {
                    grd_Deed_Checklist.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
                    grd_Deed_Checklist.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;

                }

                if (grd_Deed_Checklist[7, i].Value == null || grd_Deed_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Deed_Checklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";


                }
                else
                {
                    grd_Deed_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Deed_Checklist.Rows[i].Cells[7].Value.ToString();
                }

                if (chk_no == true && Comments.Trim().ToString() == "")
                {
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                    grd_Deed_Checklist[7, i].Style.BackColor = Color.Red;
                }

                else
                {

                    Error_Count = 0;
                }


            }

            if (grd_Deed_Checklist.Rows.Count <= 0)
            {
                return true;
            }

            if (grd_Deed_Checklist.Rows.Count == Checked_Cell_Count   && Error_Count!=1)
            {

                return true;
            }
            else
            {
                Error_Count = 1;
                Error_Tab_Count = 1;
                Defined_Tab_Index = 3;
                MessageBox.Show("Need to Enter All the Fields");
                return false;
            }
        

        }

        private void Save_Deed_List()
        {
            for (int i = 0; i < grd_Deed_Checklist.Rows.Count; i++)
            {
                grd_Deed_Checklist.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                //if (grd_Deed_Checklist.Rows[i].Cells[7].Value != "" && grd_Deed_Checklist.Rows[i].Cells[7].Value != null)
                //{
                //    Comments = grd_Deed_Checklist.Rows[i].Cells[7].Value.ToString();
                //}
                bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column22"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column23"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    chk_yes = true;
                }
                else
                {
                    chk_yes = false;
                }
                if (chk_no != null && chk_no != false)
                {
                    chk_no = true;
                }
                else
                {
                    chk_no = false;
                }

               

                if (grd_Deed_Checklist[7, i].Value == null || grd_Deed_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Deed_Checklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";

                   
                }
                else
                {
                    grd_Deed_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Deed_Checklist.Rows[i].Cells[7].Value.ToString();
                }

               
                if (grd_Deed_Checklist.Rows[i].Cells[5].Style.BackColor != Color.Red && grd_Deed_Checklist.Rows[i].Cells[6].Style.BackColor != Color.Red)
                {
                    if (grd_Deed_Checklist[7, i].Style.BackColor != Color.Red)
                    {
                        Ref_Checklist_Master_Type_Id = int.Parse(grd_Deed_Checklist.Rows[i].Cells[2].Value.ToString());
                        Checklist_Id = int.Parse(grd_Deed_Checklist.Rows[i].Cells[4].Value.ToString());
                        Question = grd_Deed_Checklist.Rows[i].Cells[3].Value.ToString();

                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Checklist_Id", Checklist_Id);
                        htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                       htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                        htcheck.Add("@User_id", user_ID);
                        htcheck.Add("@Order_Id", Order_Id);
                        htcheck.Add("@Order_Task", Order_Task);
                        htcheck.Add("@Work_Type", Work_Type_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);

                        if (dtcheck.Rows.Count > 0)
                        {

                            Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "UPDATE");
                            ht_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Modified_By", user_ID);
                            ht_Chklist.Add("@Modified_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);
                            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);

                        }
                        else if (dtcheck.Rows.Count == 0)
                        {

                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "INSERT");
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);

                            object dtcount = dataaccess.ExecuteSPForScalar("Sp_Checklist_Detail", ht_Chklist);

                            int checklistId = int.Parse(dtcount.ToString());
                        }
                    }
                }
               
            }
           


        }

        private void btn_Deed_Next_Click(object sender, EventArgs e)
        {
            //Save_DeedList();                            07-07-2017
            //tabControl1.SelectTab("tabPage4");         07-07-2017
            if (Validate_Deed_Question_New() != false)
            {
                Error_Tab_Count = 0;
                tabControl1.SelectTab("tabPage4");
            }
            //Save_Deed_List();                           //  10-07-2017
        }

        private void grd_Deed_Checklist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column22"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column23"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_Deed_Checklist[6, e.RowIndex].Value = false;
                        grd_Deed_Checklist[5, e.RowIndex].Value = true;

                        grd_Deed_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Deed_Checklist[7, e.RowIndex].ReadOnly = true;

                    }
                    else if (chk_yes != true)
                    {
                        grd_Deed_Checklist[5, e.RowIndex].Value = true;
                        grd_Deed_Checklist[6, e.RowIndex].Value = false;
                        grd_Deed_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Deed_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Deed_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        //MessageBox.Show("Enter Comments");
                        grd_Deed_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Deed_Checklist[7, e.RowIndex].Value = "";
                    }


                }
                if (e.ColumnIndex == 6)
                {
                    bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column23"].FormattedValue);
                    if (chk_no != false)
                    {
                        grd_Deed_Checklist[6, e.RowIndex].Value = true;
                        grd_Deed_Checklist[5, e.RowIndex].Value = false;
                        grd_Deed_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Deed_Checklist[7, e.RowIndex].ReadOnly = true;

                        if (chk_no != false)
                        {
                            grd_Deed_Checklist[6, e.RowIndex].Value = false;
                            grd_Deed_Checklist[5, e.RowIndex].Value = false;
                            grd_Deed_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                            grd_Deed_Checklist[7, e.RowIndex].ReadOnly = true;
                            grd_Deed_Checklist[7, e.RowIndex].Value = "";
                        }
                    }
                    else if (chk_no != true)
                    {
                        grd_Deed_Checklist[5, e.RowIndex].Value = false;
                        grd_Deed_Checklist[6, e.RowIndex].Value = true;
                        grd_Deed_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Deed_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Deed_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Deed_Checklist[7, e.RowIndex].ReadOnly = false;
                    }
                }

                if (e.ColumnIndex == 7)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column22"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column23"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_Deed_Checklist[7, e.RowIndex].ReadOnly = true;
                        grd_Deed_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                    }
                    else if (chk_yes != true)
                    {

                        grd_Deed_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Deed_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                    }

                    if (chk_yes == false && chk_no == false)
                    {
                        grd_Deed_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                        grd_Deed_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        grd_Deed_Checklist[7, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        grd_Deed_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                        grd_Deed_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
                    }

                }
            }
           
        }

        private void btn_Deed_View_Detail_Click(object sender, EventArgs e)
        {
            Hashtable ht_Deed_List = new Hashtable();
            DataTable dt_Deed_List = new DataTable();

            //ht_Deed_List.Add("@Trans", "ALL_DEED");
            ht_Deed_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Deed_List.Add("@Ref_Checklist_Master_Type_Id", 3);

            dt_Deed_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Deed_List);
            if (dt_Deed_List.Rows.Count > 0)
            {
                grd_Deed_Checklist.Rows.Clear();
                for (int i = 0; i < dt_Deed_List.Rows.Count; i++)
                {
                    grd_Deed_Checklist.Rows.Add();
                    grd_Deed_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Deed_Checklist.Rows[i].Cells[1].Value = dt_Deed_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[2].Value = dt_Deed_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[3].Value = dt_Deed_List.Rows[i]["Question"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[4].Value = dt_Deed_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[5].Value = dt_Deed_List.Rows[i]["Yes"].ToString();
                    grd_Deed_Checklist.Rows[i].Cells[6].Value = dt_Deed_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Deed_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Deed_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Deed_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Deed_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Deed_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Deed_Checklist[6, i].Value = null;
                    }
                    grd_Deed_Checklist.Rows[i].Cells[7].Value = dt_Deed_List.Rows[i]["Comments"].ToString();

                    grd_Deed_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Deed_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Deed_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
               
            }
            else
            {
                grd_Deed_Checklist.Rows.Clear();
                //Grid_Bind_Deed_CheckList();
                Grid_Bind_All_Deed();
            }
        }

        private void btn_Deed_Save_Click(object sender, EventArgs e)
        {
            int inertval = 0;
            for (int i = 0; i < grd_Deed_Checklist.Rows.Count; i++)
            {

                if (grd_Deed_Checklist.Rows[i].Cells[7].Value != "" && grd_Deed_Checklist.Rows[i].Cells[7].Value != null)
                {
                    Comments = grd_Deed_Checklist.Rows[i].Cells[7].Value.ToString();
                }
                bool check = false;
                bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column22"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column23"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    check = true;
                    grd_Deed_Checklist.Rows[i].Cells["Column23"].ReadOnly = true;
                }
                if (chk_no != null && chk_no != false)
                {
                    check = false;
                }
                Ref_Checklist_Master_Type_Id = int.Parse(grd_Deed_Checklist.Rows[i].Cells[2].Value.ToString());
                Checklist_Id = int.Parse(grd_Deed_Checklist.Rows[i].Cells[4].Value.ToString());
                Question = grd_Deed_Checklist.Rows[i].Cells[3].Value.ToString();

                if (Check_List_Tran_ID == 0)
                {
                    Hashtable ht_Chklist = new Hashtable();
                    DataTable dt_Chklist = new DataTable();

                    ht_Chklist.Add("@Trans", "INSERT");
                    ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                    ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                     ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                    ht_Chklist.Add("@Checked", check);
                    ht_Chklist.Add("@Order_Id", Order_Id);
                    ht_Chklist.Add("@Order_Task", Order_Task);
                    ht_Chklist.Add("@Comments", Comments);
                    ht_Chklist.Add("@Status", "True");
                    ht_Chklist.Add("@User_id", user_ID);
                    ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                    dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
                    inertval = 1;
                }

            }
            if (inertval == 1)
            {
                MessageBox.Show("Deed CheckList Added Successfully");
                //Grid_Bind_Deed_CheckList();
                Grid_Bind_All_Deed();
            }    
        }

        //private void Save_DeedList()
        //{
        //    int inertval = 0;
        //    for (int i = 0; i < grd_Deed_Checklist.Rows.Count; i++)
        //    {

        //        if (grd_Deed_Checklist.Rows[i].Cells[7].Value != "" && grd_Deed_Checklist.Rows[i].Cells[7].Value != null)
        //        {
        //            Comments = grd_Deed_Checklist.Rows[i].Cells[7].Value.ToString();
        //        }
        //        bool check = false;
        //        bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column22"].FormattedValue);
        //        bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells["Column23"].FormattedValue);

        //        if (chk_yes != null && chk_yes != false)
        //        {
        //            check = true;
        //            grd_Deed_Checklist.Rows[i].Cells["Column23"].ReadOnly = true;
        //        }
        //        if (chk_no != null && chk_no != false)
        //        {
        //            check = false;
        //        }
        //        Ref_Checklist_Master_Type_Id = int.Parse(grd_Deed_Checklist.Rows[i].Cells[2].Value.ToString());
        //        Checklist_Id = int.Parse(grd_Deed_Checklist.Rows[i].Cells[4].Value.ToString());
        //        Question = grd_Deed_Checklist.Rows[i].Cells[3].Value.ToString();

        //        if (Check_List_Tran_ID == 0)
        //        {
        //            Hashtable ht_Chklist = new Hashtable();
        //            DataTable dt_Chklist = new DataTable();

        //            ht_Chklist.Add("@Trans", "INSERT");
        //            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
        //            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
        //             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
        //            ht_Chklist.Add("@Checked", check);
        //            ht_Chklist.Add("@Order_Id", Order_Id);
        //            ht_Chklist.Add("@Order_Task", Order_Task);
        //            ht_Chklist.Add("@Comments", Comments);
        //            ht_Chklist.Add("@Status", "True");
        //            ht_Chklist.Add("@User_id", user_ID);
        //            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
        //            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
        //            inertval = 1;
        //        }

        //    }
        //    if (inertval == 1)
        //    {
        //        MessageBox.Show("Deed CheckList Added Successfully");
        //        Grid_Bind_Deed_CheckList();
        //    }    
        //}

  

        private void grd_Deed_Checklist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex != -1)
            //    {
            //        bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column22"].FormattedValue);
            //        bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column23"].FormattedValue);
            //        bool check = false;
            //        if (chk_yes != null && chk_yes != false)
            //        {
            //            check = true;
            //            grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column23"].ReadOnly = true;
            //            grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column24"].ReadOnly = true;

            //        }
            //        if (chk_no != null && chk_no != false)
            //        {

            //            check = false;
            //            grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column22"].ReadOnly = true;
            //            grd_Deed_Checklist.Rows[e.RowIndex].Cells["Column24"].ReadOnly = false;   // comments column
            //        }

            //    }
            //}
        }



        //Mortgage

        private void Bind_MortgageView()
        {
            Hashtable ht_Mort_Check = new Hashtable();
            DataTable dt_Mort_Check = new System.Data.DataTable();

            ht_Mort_Check.Add("@Trans", "CHECK_ORDER_ID_TASK_USER_WISE");
            ht_Mort_Check.Add("@Ref_Checklist_Master_Type_Id", 4);
            ht_Mort_Check.Add("@Order_Id", Order_Id);
            ht_Mort_Check.Add("@Order_Task", Order_Task);
            ht_Mort_Check.Add("@User_id", user_ID);
            ht_Mort_Check.Add("@Work_Type", Work_Type_Id);
            dt_Mort_Check = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Mort_Check);
            if (dt_Mort_Check.Rows.Count > 0)
            {
                Mortgage_View();
            }
            else
            {

                Grid_Bind_All_Mortgage();
            }
        }

        private void Mortgage_View()
        {
            Hashtable ht_Mortgage_List = new Hashtable();
            DataTable dt_Mortgage_List = new DataTable();

            ht_Mortgage_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Mortgage_List.Add("@Ref_Checklist_Master_Type_Id", 4);
            ht_Mortgage_List.Add("@Order_Task", Order_Task);
            ht_Mortgage_List.Add("@Order_Id", Order_Id);
            ht_Mortgage_List.Add("@User_Id", user_ID);
            ht_Mortgage_List.Add("@Work_Type", Work_Type_Id);
            dt_Mortgage_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Mortgage_List);
            if (dt_Mortgage_List.Rows.Count > 0)
            {
                grd_Mortgage_Checklist.Rows.Clear();
                for (int i = 0; i < dt_Mortgage_List.Rows.Count; i++)
                {
                    grd_Mortgage_Checklist.Rows.Add();
                    grd_Mortgage_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Mortgage_Checklist.Rows[i].Cells[1].Value = dt_Mortgage_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[2].Value = dt_Mortgage_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[3].Value = dt_Mortgage_List.Rows[i]["Question"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[4].Value = dt_Mortgage_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[5].Value = dt_Mortgage_List.Rows[i]["Yes"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[6].Value = dt_Mortgage_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Mortgage_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Mortgage_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Mortgage_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Mortgage_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Mortgage_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Mortgage_Checklist[6, i].Value = null;
                    }
                    grd_Mortgage_Checklist.Rows[i].Cells[7].Value = dt_Mortgage_List.Rows[i]["Comments"].ToString();

                    grd_Mortgage_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Mortgage_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Mortgage_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Mortgage_Checklist.Rows.Clear();
                Grid_Bind_All_Mortgage();
            }
        }

        public void Grid_Bind_All_Mortgage()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_ALL_DETAILS");
            ht.Add("@Ref_Checklist_Master_Type_Id", 4);
            ht.Add("@Order_Task", Order_Task);
            ht.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

            dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Mortgage_Checklist.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Mortgage_Checklist.Rows.Add();
                    grd_Mortgage_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Mortgage_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();

                    grd_Mortgage_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Mortgage_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Mortgage_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Mortgage_Checklist.Rows.Clear();
            }
        }

        //public void Grid_Bind_Mortgage_CheckList()
        //{

        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    //ht.Add("@Trans", "SELECT");
        //    //ht.Add("@Ref_Checklist_Master_Type_Id", 4);

        //    ht.Add("@Trans", "GET_MORTGAGE_DETAILS");
        //    ht.Add("@Order_Task", 2);
        //    ht.Add("@OrderType_ABS_Id", 1);
        //    dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
        //    if (dt.Rows.Count > 0)
        //    {
        //        grd_Mortgage_Checklist.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {

        //            grd_Mortgage_Checklist.Rows.Add();
        //            grd_Mortgage_Checklist.Rows[i].Cells[0].Value = i + 1;
        //            grd_Mortgage_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_Mortgage_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
        //            grd_Mortgage_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_Mortgage_Checklist.Rows.Clear();

        //    }
        //}

        private bool Validate_Mortgage_Question()
        {
            Hashtable htgetmax_num = new Hashtable();
            DataTable dtgetmax_num = new DataTable();

            htgetmax_num.Add("@Trans", "CHECK_COUNT");
            htgetmax_num.Add("@Order_Id", Order_Id);
            htgetmax_num.Add("@Order_Task", Order_Task);
            htgetmax_num.Add("@Ref_Checklist_Master_Type_Id", 4);
            htgetmax_num.Add("@Work_Type", Work_Type_Id);
            htgetmax_num.Add("@User_id", user_ID);
            dtgetmax_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", htgetmax_num);

            if (dtgetmax_num.Rows.Count > 0)
            {
                Entered_Count = int.Parse(dtgetmax_num.Rows[0]["count"].ToString());
            }
            else
            {
                Entered_Count = 0;
            }
            Question_Count = int.Parse(grd_Mortgage_Checklist.Rows.Count.ToString());
            if (Entered_Count == Question_Count && Error_Count == 0)
            {
                return true;
            }

            else
            {
                Error_Count = 0;
                Defined_Tab_Index = 4;
            

                if (Defined_Tab_Index != 0)
                {

                }
                else
                {
                    MessageBox.Show("Need to Enter All the Fields");
                }
               
                return false;
            }
        }

        private bool Validate_Mortgage_Question_New()

        {
            int Checked_Cell_Count = 0;

            for (int i = 0; i < grd_Mortgage_Checklist.Rows.Count; i++)
            {



                bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column30"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column31"].FormattedValue);



                if (chk_yes == true)
                {
                    int check_Count = 1;
                    Checked_Cell_Count += check_Count;

                }
                if (chk_no == true)
                {
                    int check_Count = 1;

                    Checked_Cell_Count += check_Count;

                }


                if (chk_yes == false && chk_no == false)
                {
                    grd_Mortgage_Checklist.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grd_Mortgage_Checklist.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                }
                else
                {
                    grd_Mortgage_Checklist.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
                    grd_Mortgage_Checklist.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;

                }

                if (grd_Mortgage_Checklist[7, i].Value == null || grd_Mortgage_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Mortgage_Checklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";

                }
                else
                {
                    grd_Mortgage_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Mortgage_Checklist.Rows[i].Cells[7].Value.ToString();
                }

                if (chk_no == true && Comments.Trim().ToString() == "")
                {
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                    grd_Mortgage_Checklist[7, i].Style.BackColor = Color.Red;
                }

                else
                {

                    Error_Count = 0;
                }


            }

            if (grd_Mortgage_Checklist.Rows.Count <= 0)
            {
                return true;
            }

            if (grd_Mortgage_Checklist.Rows.Count == Checked_Cell_Count && Error_Count != 1)
            {

                return true;
            }
            else
            {
                Error_Count = 1;
                Error_Tab_Count = 1;
                Defined_Tab_Index = 4;
                MessageBox.Show("Need to Enter All the Fields");
                return false;
            }
        

        }
        private void Save_Mortgage_List()
        {
            for (int i = 0; i < grd_Mortgage_Checklist.Rows.Count; i++)
            {
                grd_Mortgage_Checklist.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                //if (grd_Mortgage_Checklist.Rows[i].Cells[7].Value != "" && grd_Mortgage_Checklist.Rows[i].Cells[7].Value != null)
                //{
                //    Comments = grd_Mortgage_Checklist.Rows[i].Cells[7].Value.ToString();
                //}
                bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column30"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column31"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    chk_yes = true;
                }
                else
                {
                    chk_yes = false;
                }
                if (chk_no != null && chk_no != false)
                {
                    chk_no = true;
                }
                else
                {
                    chk_no = false;
                }

               

                if (grd_Mortgage_Checklist[7, i].Value == null || grd_Mortgage_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Mortgage_Checklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";
                    
                }
                else
                {
                    grd_Mortgage_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Mortgage_Checklist.Rows[i].Cells[7].Value.ToString();
                }

               
                if (grd_Mortgage_Checklist.Rows[i].Cells[5].Style.BackColor != Color.Red && grd_Mortgage_Checklist.Rows[i].Cells[6].Style.BackColor != Color.Red)
                {
                    if (grd_Mortgage_Checklist[7, i].Style.BackColor != Color.Red)
                    {
                        Ref_Checklist_Master_Type_Id = int.Parse(grd_Mortgage_Checklist.Rows[i].Cells[2].Value.ToString());
                        Checklist_Id = int.Parse(grd_Mortgage_Checklist.Rows[i].Cells[4].Value.ToString());
                        Question = grd_Mortgage_Checklist.Rows[i].Cells[3].Value.ToString();

                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Checklist_Id", Checklist_Id);
                        htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                       htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                        htcheck.Add("@User_id", user_ID);
                        htcheck.Add("@Order_Id", Order_Id);
                        htcheck.Add("@Order_Task", Order_Task);
                        htcheck.Add("@Work_Type", Work_Type_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);

                        if (dtcheck.Rows.Count > 0)
                        {

                            Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "UPDATE");
                            ht_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Modified_By", user_ID);
                            ht_Chklist.Add("@Modified_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);
                            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);

                        }
                        else if (dtcheck.Rows.Count == 0)
                        {

                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "INSERT");
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);

                            object dtcount = dataaccess.ExecuteSPForScalar("Sp_Checklist_Detail", ht_Chklist);

                            int checklistId = int.Parse(dtcount.ToString());
                        }
                    }
                }
               
            }
            


        }

        private void btn_Mortgage_Next_Click(object sender, EventArgs e)
        {
            //MortgageList();                              //07-07-2017
            //tabControl1.SelectTab("tabPage5");          //07-07-2017

            if (Validate_Mortgage_Question_New() != false)
            {
                Error_Tab_Count = 0;
                tabControl1.SelectTab("tabPage5");
            

            }
           // Save_Mortgage_List();                      //07-07-2017
        }

        private void grd_Mortgage_Checklist_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column30"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column31"].FormattedValue);

                    if (chk_yes != false)
                    {
                        grd_Mortgage_Checklist[6, e.RowIndex].Value = false;
                        grd_Mortgage_Checklist[5, e.RowIndex].Value = true;

                        grd_Mortgage_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = true;

                    }
                    else if (chk_yes != true)
                    {
                        grd_Mortgage_Checklist[5, e.RowIndex].Value = true;
                        grd_Mortgage_Checklist[6, e.RowIndex].Value = false;
                        grd_Mortgage_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Mortgage_Checklist[7, e.RowIndex].Value = "";
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    bool chk_no = false;
                    chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column31"].FormattedValue);
                    if (chk_no != false)
                    {
                        grd_Mortgage_Checklist[6, e.RowIndex].Value = true;
                        grd_Mortgage_Checklist[5, e.RowIndex].Value = false;
                        grd_Mortgage_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = true;

                        if (chk_no != false)
                        {
                            grd_Mortgage_Checklist[6, e.RowIndex].Value = false;
                            grd_Mortgage_Checklist[5, e.RowIndex].Value = false;
                            grd_Mortgage_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                            grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = true;
                            grd_Mortgage_Checklist[7, e.RowIndex].Value = "";
                        }
                    }
                    else if (chk_no != true)
                    {
                        grd_Mortgage_Checklist[5, e.RowIndex].Value = false;
                        grd_Mortgage_Checklist[6, e.RowIndex].Value = true;
                        grd_Mortgage_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Mortgage_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Mortgage_Checklist[7, e.RowIndex].Value = "";
                    }
                }

                if (e.ColumnIndex == 7)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column30"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column31"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = true;
                        grd_Mortgage_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                    }
                    else if (chk_yes != true)
                    {
                        grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Mortgage_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                    }

                    if (chk_yes == false && chk_no == false)
                    {
                        grd_Mortgage_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                        grd_Mortgage_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        grd_Mortgage_Checklist[7, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        grd_Mortgage_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                        grd_Mortgage_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
                    }

                }
            }

        }

        private void btn_Mortgage_View_detail_Click(object sender, EventArgs e)
        {
            Hashtable ht_Mortgage_List = new Hashtable();
            DataTable dt_Mortgage_List = new DataTable();

            //ht_Mortgage_List.Add("@Trans", "ALL_MORTGAGE");
            ht_Mortgage_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Mortgage_List.Add("@Ref_Checklist_Master_Type_Id", 4);

            dt_Mortgage_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Mortgage_List);
            if (dt_Mortgage_List.Rows.Count > 0)
            {
                grd_Mortgage_Checklist.Rows.Clear();
                for (int i = 0; i < dt_Mortgage_List.Rows.Count; i++)
                {
                    grd_Mortgage_Checklist.Rows.Add();
                    grd_Mortgage_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Mortgage_Checklist.Rows[i].Cells[1].Value = dt_Mortgage_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[2].Value = dt_Mortgage_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[3].Value = dt_Mortgage_List.Rows[i]["Question"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[4].Value = dt_Mortgage_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[5].Value = dt_Mortgage_List.Rows[i]["Yes"].ToString();
                    grd_Mortgage_Checklist.Rows[i].Cells[6].Value = dt_Mortgage_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Mortgage_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Mortgage_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Mortgage_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Mortgage_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Mortgage_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Mortgage_Checklist[6, i].Value = null;
                    }
                    grd_Mortgage_Checklist.Rows[i].Cells[7].Value = dt_Mortgage_List.Rows[i]["Comments"].ToString();

                    grd_Mortgage_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Mortgage_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Mortgage_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Mortgage_Checklist.Rows.Clear();
                //Grid_Bind_Mortgage_CheckList();
                Grid_Bind_All_Mortgage();
            }
        }


        private void btn_Mortgage_Save_Click(object sender, EventArgs e)
        {
            int inertval = 0;
            for (int i = 0; i < grd_Mortgage_Checklist.Rows.Count; i++)
            {

                if (grd_Mortgage_Checklist.Rows[i].Cells[7].Value != "" && grd_Mortgage_Checklist.Rows[i].Cells[7].Value != null)
                {
                    Comments = grd_Mortgage_Checklist.Rows[i].Cells[7].Value.ToString();
                }
                bool check = false;
                bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column30"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column31"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    check = true;
                    grd_Mortgage_Checklist.Rows[i].Cells["Column31"].ReadOnly = true;
                }
                if (chk_no != null && chk_no != false)
                {
                    check = false;
                }
                Ref_Checklist_Master_Type_Id = int.Parse(grd_Mortgage_Checklist.Rows[i].Cells[2].Value.ToString());
                Checklist_Id = int.Parse(grd_Mortgage_Checklist.Rows[i].Cells[4].Value.ToString());
                Question = grd_Mortgage_Checklist.Rows[i].Cells[3].Value.ToString();

                if (Check_List_Tran_ID == 0)
                {
                    Hashtable ht_Chklist = new Hashtable();
                    DataTable dt_Chklist = new DataTable();

                    ht_Chklist.Add("@Trans", "INSERT");
                    ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                    ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                     ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                    ht_Chklist.Add("@Checked", check);
                    ht_Chklist.Add("@Order_Id", Order_Id);
                    ht_Chklist.Add("@Order_Task", Order_Task);
                    ht_Chklist.Add("@Comments", Comments);
                    ht_Chklist.Add("@Status", "True");
                    ht_Chklist.Add("@User_id", user_ID);
                    ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                    dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
                    inertval = 1;
                }

            }
            if (inertval == 1)
            {
                MessageBox.Show("Mortgage CheckList Added Successfully");
                //Grid_Bind_Mortgage_CheckList();
                Grid_Bind_All_Mortgage();
            }
        }

        //private void MortgageList()
        //{
        //    int inertval = 0;
        //    for (int i = 0; i < grd_Mortgage_Checklist.Rows.Count; i++)
        //    {

        //        if (grd_Mortgage_Checklist.Rows[i].Cells[7].Value != "" && grd_Mortgage_Checklist.Rows[i].Cells[7].Value != null)
        //        {
        //            Comments = grd_Mortgage_Checklist.Rows[i].Cells[7].Value.ToString();
        //        }
        //        bool check = false;
        //        bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column30"].FormattedValue);
        //        bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells["Column31"].FormattedValue);

        //        if (chk_yes != null && chk_yes != false)
        //        {
        //            check = true;
        //            grd_Mortgage_Checklist.Rows[i].Cells["Column31"].ReadOnly = true;
        //        }
        //        if (chk_no != null && chk_no != false)
        //        {
        //            check = false;
        //        }
        //        Ref_Checklist_Master_Type_Id = int.Parse(grd_Mortgage_Checklist.Rows[i].Cells[2].Value.ToString());
        //        Checklist_Id = int.Parse(grd_Mortgage_Checklist.Rows[i].Cells[4].Value.ToString());
        //        Question = grd_Mortgage_Checklist.Rows[i].Cells[3].Value.ToString();

        //        if (Check_List_Tran_ID == 0)
        //        {
        //            Hashtable ht_Chklist = new Hashtable();
        //            DataTable dt_Chklist = new DataTable();

        //            ht_Chklist.Add("@Trans", "INSERT");
        //            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
        //            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
        //             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
        //            ht_Chklist.Add("@Checked", check);
        //            ht_Chklist.Add("@Order_Id", Order_Id);
        //            ht_Chklist.Add("@Order_Task", Order_Task);
        //            ht_Chklist.Add("@Comments", Comments);
        //            ht_Chklist.Add("@Status", "True");
        //            ht_Chklist.Add("@User_id", user_ID);
        //            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
        //            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
        //            inertval = 1;
        //        }

        //    }
        //    if (inertval == 1)
        //    {
        //        MessageBox.Show("Mortgage CheckList Added Successfully");
        //        Grid_Bind_Mortgage_CheckList();
        //    }    
        //}

        private void grd_Mortgage_Checklist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex != -1)
            //    {
            //        bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column30"].FormattedValue);
            //        bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column31"].FormattedValue);
                  
            //        if (chk_yes != false)
            //        {
                       
            //            grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column31"].ReadOnly = true;
            //            grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column32"].ReadOnly = true;

            //        }
            //        if (chk_no != false)
            //        {

                       
            //            grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column30"].ReadOnly = true;
            //            grd_Mortgage_Checklist.Rows[e.RowIndex].Cells["Column32"].ReadOnly = false;   // comments column
            //        }

            //    }
            //}
        }

        //Judgment/Liens


        private void Bind_JudgmentLienView()
        {
            Hashtable ht_Mort_Check = new Hashtable();
            DataTable dt_Mort_Check = new System.Data.DataTable();

            ht_Mort_Check.Add("@Trans", "CHECK_ORDER_ID_TASK_USER_WISE");
            ht_Mort_Check.Add("@Ref_Checklist_Master_Type_Id", 5);
            ht_Mort_Check.Add("@Order_Id", Order_Id);
            ht_Mort_Check.Add("@Order_Task", Order_Task);
            ht_Mort_Check.Add("@User_id", user_ID);
            ht_Mort_Check.Add("@Work_Type", Work_Type_Id);

            dt_Mort_Check = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Mort_Check);
            if (dt_Mort_Check.Rows.Count > 0)
            {
                JudgmentLien_View();
            }
            else
            {

                Grid_Bind_All_JudgmLien();
            }
        }

        private void JudgmentLien_View()
        {
            Hashtable ht_JudgLiens_List = new Hashtable();
            DataTable dt_JudgLiens_List = new DataTable();

            ht_JudgLiens_List.Add("@Trans", "GET_ALL_VIEW");
            ht_JudgLiens_List.Add("@Ref_Checklist_Master_Type_Id", 5);
            ht_JudgLiens_List.Add("@Order_Task", Order_Task);
            ht_JudgLiens_List.Add("@Order_Id", Order_Id);
            ht_JudgLiens_List.Add("@User_Id", user_ID);
            ht_JudgLiens_List.Add("@Work_Type", Work_Type_Id);
            dt_JudgLiens_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_JudgLiens_List);
            if (dt_JudgLiens_List.Rows.Count > 0)
            {
                grd_Judgment_Liens_Checklist.Rows.Clear();
                for (int i = 0; i < dt_JudgLiens_List.Rows.Count; i++)
                {
                    grd_Judgment_Liens_Checklist.Rows.Add();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[1].Value = dt_JudgLiens_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[2].Value = dt_JudgLiens_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[3].Value = dt_JudgLiens_List.Rows[i]["Question"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[4].Value = dt_JudgLiens_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Value = dt_JudgLiens_List.Rows[i]["Yes"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Value = dt_JudgLiens_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Judgment_Liens_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Judgment_Liens_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Judgment_Liens_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Judgment_Liens_Checklist[6, i].Value = null;
                    }
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value = dt_JudgLiens_List.Rows[i]["Comments"].ToString();

                    grd_Judgment_Liens_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Judgment_Liens_Checklist.Rows.Clear();
                Grid_Bind_All_JudgmLien();
            }
        }

        public void Grid_Bind_All_JudgmLien()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_ALL_DETAILS");
            ht.Add("@Ref_Checklist_Master_Type_Id", 5);
            ht.Add("@Order_Task", Order_Task);
            ht.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
            dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Judgment_Liens_Checklist.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Judgment_Liens_Checklist.Rows.Add();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();

                    grd_Judgment_Liens_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Judgment_Liens_Checklist.Rows.Clear();
            }
        }

        //public void Grid_Bind_JudgmentLiens_CheckList()
        //{

        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    //ht.Add("@Trans", "SELECT");
        //    //ht.Add("@Ref_Checklist_Master_Type_Id", 5);

        //    ht.Add("@Trans", "GET_JUDG_LIEN_DETAILS");
        //    ht.Add("@Order_Task", 2);
        //    ht.Add("@OrderType_ABS_Id", 1);
        //    dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
        //    if (dt.Rows.Count > 0)
        //    {
        //        grd_Judgment_Liens_Checklist.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {

        //            grd_Judgment_Liens_Checklist.Rows.Add();
        //            grd_Judgment_Liens_Checklist.Rows[i].Cells[0].Value = i + 1;
        //            grd_Judgment_Liens_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_Judgment_Liens_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
        //            grd_Judgment_Liens_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_Judgment_Liens_Checklist.Rows.Clear();

        //    }
        //}

        private void btn_Judgment_Liens_Next_Click(object sender, EventArgs e)
        {                                
            //JudgmentLiensList();                            // 07-07-2017
            //tabControl1.SelectTab("tabPage6");             // 07-07-2017
            if (Validate_Judgment_Liens_Question_New() != false)
            {
                Error_Tab_Count = 0;
                tabControl1.SelectTab("tabPage6");

            }
           // Save_Judgment_Liens_List();                         // 10-07-2017
        }

        private bool Validate_Judgment_Liens_Question()
        {
            Hashtable htgetmax_num = new Hashtable();
            DataTable dtgetmax_num = new DataTable();

            htgetmax_num.Add("@Trans", "CHECK_COUNT");
            htgetmax_num.Add("@Order_Id", Order_Id);
            htgetmax_num.Add("@Order_Task", Order_Task);
            htgetmax_num.Add("@Ref_Checklist_Master_Type_Id", 5);
            htgetmax_num.Add("@Work_Type", Work_Type_Id);
            htgetmax_num.Add("@User_id", user_ID);
            dtgetmax_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", htgetmax_num);

            if (dtgetmax_num.Rows.Count > 0)
            {
                Entered_Count = int.Parse(dtgetmax_num.Rows[0]["count"].ToString());
            }
            else
            {
                Entered_Count = 0;
            }
            Question_Count = int.Parse(grd_Judgment_Liens_Checklist.Rows.Count.ToString());
            if (Entered_Count == Question_Count && Error_Count == 0)
            {
                return true;
            }

            else
            {
                Error_Count = 0;
                Defined_Tab_Index = 5;


                if (Defined_Tab_Index != 0)
                {

                }
                else
                {
                    MessageBox.Show("Need to Enter All the Fields");
                }
             
                return false;
            }
        }

        private bool Validate_Judgment_Liens_Question_New()
        {

            int Checked_Cell_Count = 0;

            for (int i = 0; i < grd_Judgment_Liens_Checklist.Rows.Count; i++)
            {



                bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column38"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column39"].FormattedValue);


                if (chk_yes == true)
                {
                    int check_Count = 1;
                    Checked_Cell_Count += check_Count;

                }
                if (chk_no == true)
                {
                    int check_Count = 1;

                    Checked_Cell_Count += check_Count;

                }


                if (chk_yes == false && chk_no == false)
                {
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                }
                else
                {
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;

                }

                if (grd_Judgment_Liens_Checklist[7, i].Value == null || grd_Judgment_Liens_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Judgment_Liens_Checklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";

                }
                else
                {
                    grd_Judgment_Liens_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value.ToString();
                }

                if (chk_no == true && Comments.Trim().ToString() == "")
                {
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                    grd_Judgment_Liens_Checklist[7, i].Style.BackColor = Color.Red;

                }
                else
                {

                    Error_Count = 0;
                }

            }

            if (grd_Judgment_Liens_Checklist.Rows.Count <= 0)
            {
                return true;
            }

            if (grd_Judgment_Liens_Checklist.Rows.Count == Checked_Cell_Count && Error_Count != 1)
            {

                return true;
            }
            else
            {
                Error_Count = 1;
                Error_Tab_Count = 1;
                Defined_Tab_Index = 5;
                MessageBox.Show("Need to Enter All the Fields");
                return false;
            }
        
        }

        private void Save_Judgment_Liens_List()
        {
            for (int i = 0; i < grd_Judgment_Liens_Checklist.Rows.Count; i++)
            {
                grd_Judgment_Liens_Checklist.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                //if (grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value != "" && grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value != null)
                //{
                //    Comments = grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value.ToString();
                //}
                bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column38"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column39"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    chk_yes = true;
                }
                else
                {
                    chk_yes = false;
                }
                if (chk_no != null && chk_no != false)
                {
                    chk_no = true;
                }
                else
                {
                    chk_no = false;
                }

              

                if (grd_Judgment_Liens_Checklist[7, i].Value == null || grd_Judgment_Liens_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Judgment_Liens_Checklist[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";
                   
                }
                else
                {
                    grd_Judgment_Liens_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value.ToString();
                }

               
                if (grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Style.BackColor != Color.Red && grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Style.BackColor != Color.Red)
                {
                    if (grd_Judgment_Liens_Checklist[7, i].Style.BackColor != Color.Red)
                    {
                        Ref_Checklist_Master_Type_Id = int.Parse(grd_Judgment_Liens_Checklist.Rows[i].Cells[2].Value.ToString());
                        Checklist_Id = int.Parse(grd_Judgment_Liens_Checklist.Rows[i].Cells[4].Value.ToString());
                        Question = grd_Judgment_Liens_Checklist.Rows[i].Cells[3].Value.ToString();

                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Checklist_Id", Checklist_Id);
                        htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                       htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                        htcheck.Add("@User_id", user_ID);
                        htcheck.Add("@Order_Id", Order_Id);
                        htcheck.Add("@Order_Task", Order_Task);
                        htcheck.Add("@Work_Type", Work_Type_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);

                        if (dtcheck.Rows.Count > 0)
                        {

                            Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "UPDATE");
                            ht_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Modified_By", user_ID);
                            ht_Chklist.Add("@Modified_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);
                            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);

                        }
                        else if (dtcheck.Rows.Count == 0)
                        {

                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "INSERT");
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);

                            object dtcount = dataaccess.ExecuteSPForScalar("Sp_Checklist_Detail", ht_Chklist);

                            int checklistId = int.Parse(dtcount.ToString());
                        }
                    }
                }
               
            }
            


        }

        private void grd_Judgment_Liens_Checklist_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column38"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column39"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_Judgment_Liens_Checklist[6, e.RowIndex].Value = false;
                        grd_Judgment_Liens_Checklist[5, e.RowIndex].Value = true;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = true;

                    }
                    else if (chk_yes != true)
                    {
                        grd_Judgment_Liens_Checklist[5, e.RowIndex].Value = true;
                        grd_Judgment_Liens_Checklist[6, e.RowIndex].Value = false;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Judgment_Liens_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Judgment_Liens_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Value = "";
                    }
                }
                if (e.ColumnIndex == 6)
                {

                    bool chk_no = false;
                    chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column39"].FormattedValue);

                    if (chk_no != false)
                    {
                        grd_Judgment_Liens_Checklist[6, e.RowIndex].Value = true;
                        grd_Judgment_Liens_Checklist[5, e.RowIndex].Value = false;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = true;

                        if (chk_no != false)
                        {
                            grd_Judgment_Liens_Checklist[6, e.RowIndex].Value = false;
                            grd_Judgment_Liens_Checklist[5, e.RowIndex].Value = false;
                            grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                            grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = true;
                            grd_Judgment_Liens_Checklist[7, e.RowIndex].Value = "";
                        }

                    }
                    else if (chk_no != true)
                    {
                        grd_Judgment_Liens_Checklist[5, e.RowIndex].Value = false;
                        grd_Judgment_Liens_Checklist[6, e.RowIndex].Value = true;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Judgment_Liens_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Judgment_Liens_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Value = "";
                    }
                   
                }

                if (e.ColumnIndex == 7)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column38"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column39"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = true;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;

                    }
                    else if (chk_yes != true)
                    {

                        grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = Color.White;

                    }

                    if (chk_yes == false && chk_no == false)
                    {

                        grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                        grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                        grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
                    }

                }
            }

        }

        private void btn_JudgmentLiens_View_Detail_Click(object sender, EventArgs e)
        {
            Hashtable ht_JudgLiens_List = new Hashtable();
            DataTable dt_JudgLiens_List = new DataTable();

          //  ht_JudgLiens_List.Add("@Trans", "ALL_JUDGMENT_LIENS");
            ht_JudgLiens_List.Add("@Trans", "GET_ALL_VIEW");
            ht_JudgLiens_List.Add("@Ref_Checklist_Master_Type_Id", 5);
            dt_JudgLiens_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_JudgLiens_List);
            if (dt_JudgLiens_List.Rows.Count > 0)
            {
                grd_Judgment_Liens_Checklist.Rows.Clear();
                for (int i = 0; i < dt_JudgLiens_List.Rows.Count; i++)
                {
                    grd_Judgment_Liens_Checklist.Rows.Add();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[1].Value = dt_JudgLiens_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[2].Value = dt_JudgLiens_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[3].Value = dt_JudgLiens_List.Rows[i]["Question"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[4].Value = dt_JudgLiens_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Value = dt_JudgLiens_List.Rows[i]["Yes"].ToString();
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Value = dt_JudgLiens_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Judgment_Liens_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Judgment_Liens_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Judgment_Liens_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Judgment_Liens_Checklist[6, i].Value = null;
                    }
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value = dt_JudgLiens_List.Rows[i]["Comments"].ToString();

                    grd_Judgment_Liens_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Judgment_Liens_Checklist.Rows.Clear();
                //Grid_Bind_JudgmentLiens_CheckList();
                Grid_Bind_All_JudgmLien();
            }
        }


        private void btn_Judgment_Liens_Save_Click(object sender, EventArgs e)
        {
            int inertval = 0;
            for (int i = 0; i < grd_Judgment_Liens_Checklist.Rows.Count; i++)
            {

                if (grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value != "" && grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value != null)
                {
                    Comments = grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value.ToString();
                }
                bool check = false;
                bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column38"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column39"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    check = true;
                    grd_Judgment_Liens_Checklist.Rows[i].Cells["Column39"].ReadOnly = true;
                }
                if (chk_no != null && chk_no != false)
                {
                    check = false;
                }
                Ref_Checklist_Master_Type_Id = int.Parse(grd_Judgment_Liens_Checklist.Rows[i].Cells[2].Value.ToString());
                Checklist_Id = int.Parse(grd_Judgment_Liens_Checklist.Rows[i].Cells[4].Value.ToString());
                Question = grd_Judgment_Liens_Checklist.Rows[i].Cells[3].Value.ToString();

                if (Check_List_Tran_ID == 0)
                {
                    Hashtable ht_Chklist = new Hashtable();
                    DataTable dt_Chklist = new DataTable();

                    ht_Chklist.Add("@Trans", "INSERT");
                    ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                    ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                     ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                    ht_Chklist.Add("@Checked", check);
                    ht_Chklist.Add("@Order_Id", Order_Id);
                    ht_Chklist.Add("@Order_Task", Order_Task);
                    ht_Chklist.Add("@Comments", Comments);
                    ht_Chklist.Add("@Status", "True");
                    ht_Chklist.Add("@User_id", user_ID);
                    ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                    dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
                    inertval = 1;
                }

            }
            if (inertval == 1)
            {
                MessageBox.Show("Judgemnet/Liens CheckList Added Successfully");
                //Grid_Bind_JudgmentLiens_CheckList();
                Grid_Bind_All_JudgmLien();
            }    
        }

        //private void JudgmentLiensList()
        //{
        //    int inertval = 0;
        //    for (int i = 0; i < grd_Judgment_Liens_Checklist.Rows.Count; i++)
        //    {

        //        if (grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value != "" && grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value != null)
        //        {
        //            Comments = grd_Judgment_Liens_Checklist.Rows[i].Cells[7].Value.ToString();
        //        }
        //        bool check = false;
        //        bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column38"].FormattedValue);
        //        bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells["Column39"].FormattedValue);

        //        if (chk_yes != null && chk_yes != false)
        //        {
        //            check = true;
        //            grd_Judgment_Liens_Checklist.Rows[i].Cells["Column39"].ReadOnly = true;
        //        }
        //        if (chk_no != null && chk_no != false)
        //        {
        //            check = false;
        //        }
        //        Ref_Checklist_Master_Type_Id = int.Parse(grd_Judgment_Liens_Checklist.Rows[i].Cells[2].Value.ToString());
        //        Checklist_Id = int.Parse(grd_Judgment_Liens_Checklist.Rows[i].Cells[4].Value.ToString());
        //        Question = grd_Judgment_Liens_Checklist.Rows[i].Cells[3].Value.ToString();

        //        if (Check_List_Tran_ID == 0)
        //        {
        //            Hashtable ht_Chklist = new Hashtable();
        //            DataTable dt_Chklist = new DataTable();

        //            ht_Chklist.Add("@Trans", "INSERT");
        //            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
        //            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
        //             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
        //            ht_Chklist.Add("@Checked", check);
        //            ht_Chklist.Add("@Order_Id", Order_Id);
        //            ht_Chklist.Add("@Order_Task", Order_Task);
        //            ht_Chklist.Add("@Comments", Comments);
        //            ht_Chklist.Add("@Status", "True");
        //            ht_Chklist.Add("@User_id", user_ID);
        //            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
        //            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
        //            inertval = 1;
        //        }

        //    }
        //    if (inertval == 1)
        //    {
        //        MessageBox.Show("Judgemnet/Liens CheckList Added Successfully");
        //        Grid_Bind_JudgmentLiens_CheckList();
        //    }    
        //}

        private void grd_Judgment_Liens_Checklist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex != -1)
            //    {
            //        bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column38"].FormattedValue);
            //        bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column39"].FormattedValue);
            //        bool check = false;
            //        if (chk_yes != null && chk_yes != false)
            //        {
            //            check = true;
            //            grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column39"].ReadOnly = true;
            //            grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column40"].ReadOnly = true;

            //        }
            //        if (chk_no != null && chk_no != false)
            //        {

            //            check = false;
            //            grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column38"].ReadOnly = true;
            //            grd_Judgment_Liens_Checklist.Rows[e.RowIndex].Cells["Column40"].ReadOnly = false;   // comments column
            //        }

            //    }
            //}
        }


        //others

        private void Bind_OthersView()
        {
            Hashtable ht_Others_Check = new Hashtable();
            DataTable dt_Others_Check = new System.Data.DataTable();

            ht_Others_Check.Add("@Trans", "CHECK_ORDER_ID_TASK_USER_WISE");
            ht_Others_Check.Add("@Ref_Checklist_Master_Type_Id", 6);
            ht_Others_Check.Add("@Order_Id", Order_Id);
            ht_Others_Check.Add("@Order_Task", Order_Task);
            ht_Others_Check.Add("@User_id", user_ID);
            ht_Others_Check.Add("@Work_Type", Work_Type_Id);
            dt_Others_Check = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Others_Check);
            if (dt_Others_Check.Rows.Count > 0)
            {
                Others_View();
            }
            else
            {

                Grid_Bind_All_Others();
            }
        }

        private void Others_View()
        {
            Hashtable ht_Others_List = new Hashtable();
            DataTable dt_Others_List = new DataTable();

            ht_Others_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Others_List.Add("@Ref_Checklist_Master_Type_Id", 6);
            ht_Others_List.Add("@Order_Task", Order_Task);
            ht_Others_List.Add("@Order_Id", Order_Id);
            ht_Others_List.Add("@User_Id", user_ID);
            ht_Others_List.Add("@Work_Type", Work_Type_Id);
            dt_Others_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Others_List);
            if (dt_Others_List.Rows.Count > 0)
            {
                grd_Others_Checklist.Rows.Clear();
                for (int i = 0; i < dt_Others_List.Rows.Count; i++)
                {
                    grd_Others_Checklist.Rows.Add();
                    grd_Others_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Others_Checklist.Rows[i].Cells[1].Value = dt_Others_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[2].Value = dt_Others_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[3].Value = dt_Others_List.Rows[i]["Question"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[4].Value = dt_Others_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[5].Value = dt_Others_List.Rows[i]["Yes"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[6].Value = dt_Others_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Others_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Others_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Others_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Others_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Others_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Others_Checklist[6, i].Value = null;
                    }
                    grd_Others_Checklist.Rows[i].Cells[7].Value = dt_Others_List.Rows[i]["Comments"].ToString();

                    grd_Others_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Others_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Others_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Others_Checklist.Rows.Clear();
                Grid_Bind_All_Others();
            }
        }

        public void Grid_Bind_All_Others()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_ALL_DETAILS");
            ht.Add("@Ref_Checklist_Master_Type_Id", 6);
            ht.Add("@Order_Task", Order_Task);
            ht.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
            dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Others_Checklist.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Others_Checklist.Rows.Add();
                    grd_Others_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Others_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();

                    grd_Others_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Others_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Others_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Others_Checklist.Rows.Clear();
            }
        }

        //public void Grid_Bind_Others_CheckList()
        //{

        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    //ht.Add("@Trans", "SELECT");
        //    //ht.Add("@Ref_Checklist_Master_Type_Id", 6);

        //    ht.Add("@Trans", "GET_OTHERS_DETAILS");
        //    ht.Add("@Order_Task", 2);
        //    ht.Add("@OrderType_ABS_Id", 1);
        //    dt = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht);
        //    if (dt.Rows.Count > 0)
        //    {
        //        grd_Others_Checklist.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {

        //            grd_Others_Checklist.Rows.Add();
        //            grd_Others_Checklist.Rows[i].Cells[0].Value = i + 1;
        //            grd_Others_Checklist.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_Others_Checklist.Rows[i].Cells[3].Value = dt.Rows[i]["Question"].ToString();
        //            grd_Others_Checklist.Rows[i].Cells[4].Value = dt.Rows[i]["Checklist_Id"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_Others_Checklist.Rows.Clear();

        //    }
        //}

        private bool Validate_Others_Question()
        {
            Hashtable htgetmax_num = new Hashtable();
            DataTable dtgetmax_num = new DataTable();

            htgetmax_num.Add("@Trans", "CHECK_COUNT");
            htgetmax_num.Add("@Order_Id", Order_Id);
            htgetmax_num.Add("@Order_Task", Order_Task);
            htgetmax_num.Add("@Ref_Checklist_Master_Type_Id", 6);
            htgetmax_num.Add("@Work_Type", Work_Type_Id);
            htgetmax_num.Add("@User_id", user_ID);
            dtgetmax_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", htgetmax_num);

            if (dtgetmax_num.Rows.Count > 0)
            {
                Entered_Count = int.Parse(dtgetmax_num.Rows[0]["count"].ToString());
            }
            else
            {
                Entered_Count = 0;
            }
            Question_Count = int.Parse(grd_Others_Checklist.Rows.Count.ToString());
            if (Entered_Count == Question_Count && Error_Count == 0)
            {
                
                return true;
            }
            
            else
            {
                Error_Count = 0;
                Defined_Tab_Index = 6;


                if (Defined_Tab_Index != 0)
                {

                }
                else
                {
                    MessageBox.Show("Need to Enter All the Fields");
                }
                
                return false;
            }
        }
        private bool Validate_Others_Question_New()
        {

            int Checked_Cell_Count = 0;

            for (int i = 0; i < grd_Others_Checklist.Rows.Count; i++)
            {


                bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column46"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column47"].FormattedValue);



                if (chk_yes == true)
                {
                    int check_Count = 1;
                    Checked_Cell_Count += check_Count;

                }
                if (chk_no == true)
                {
                    int check_Count = 1;

                    Checked_Cell_Count += check_Count;

                }


                if (chk_yes == false && chk_no == false)
                {
                    grd_Others_Checklist.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grd_Others_Checklist.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                }
                else
                {
                    grd_Others_Checklist.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
                    grd_Others_Checklist.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;

                }

                if (grd_Others_Checklist[7, i].Value == null || grd_Others_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        //Error_Count = 1;
                        grd_Others_Checklist[7, i].Style.BackColor = Color.Red;

                    }
                    Comments = "";

                }
                else
                {
                    grd_Others_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Others_Checklist.Rows[i].Cells[7].Value.ToString();
                }

                if (chk_no == true && Comments.Trim().ToString() == "")
                {
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                    grd_Others_Checklist[7, i].Style.BackColor = Color.Red;

                }
                else
                {

                    Error_Count = 0;
                }



            }

            if (grd_Others_Checklist.Rows.Count <= 0)
            {
                return true;
            }

            if (grd_Others_Checklist.Rows.Count == Checked_Cell_Count && Error_Count != 1)
            {

                return true;
            }
            else
            {
                Error_Count = 1;
                Error_Tab_Count = 1;
                Defined_Tab_Index = 6;
                MessageBox.Show("Need to Enter All the Fields");
                return false;
            }
        
        }
        private void Save_Others_List()
        {
            for (int i = 0; i < grd_Others_Checklist.Rows.Count; i++)
            {
                grd_Others_Checklist.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                //if (grd_Others_Checklist.Rows[i].Cells[7].Value != "" && grd_Others_Checklist.Rows[i].Cells[7].Value != null)
                //{
                //    Comments = grd_Others_Checklist.Rows[i].Cells[7].Value.ToString();
                //}
                bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column46"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column47"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    chk_yes = true;
                }
                else
                {
                    chk_yes = false;
                }
                if (chk_no != null && chk_no != false)
                {
                    chk_no = true;
                }
                else
                {
                    chk_no = false;
                }

              

                if (grd_Others_Checklist[7, i].Value == null || grd_Others_Checklist[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        //Error_Count = 1;
                        grd_Others_Checklist[7, i].Style.BackColor = Color.Red;
                       
                    }
                    Comments = "";
                   
                }
                else
                {
                    grd_Others_Checklist[7, i].Style.BackColor = Color.White;
                    Comments = grd_Others_Checklist.Rows[i].Cells[7].Value.ToString();
                }

              

                if (grd_Others_Checklist.Rows[i].Cells[5].Style.BackColor != Color.Red && grd_Others_Checklist.Rows[i].Cells[6].Style.BackColor != Color.Red)
                {
                    if (grd_Others_Checklist[7, i].Style.BackColor != Color.Red)
                    {
                        Ref_Checklist_Master_Type_Id = int.Parse(grd_Others_Checklist.Rows[i].Cells[2].Value.ToString());
                        Checklist_Id = int.Parse(grd_Others_Checklist.Rows[i].Cells[4].Value.ToString());
                        Question = grd_Others_Checklist.Rows[i].Cells[3].Value.ToString();

                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Checklist_Id", Checklist_Id);
                        htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                       htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                        htcheck.Add("@User_id", user_ID);
                        htcheck.Add("@Order_Id", Order_Id);
                        htcheck.Add("@Order_Task", Order_Task);
                        htcheck.Add("@Work_Type", Work_Type_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);

                        if (dtcheck.Rows.Count > 0)
                        {

                            Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "UPDATE");
                            ht_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Modified_By", user_ID);
                            ht_Chklist.Add("@Modified_Date", DateTime.Now);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);
                            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);

                        }
                        else if (dtcheck.Rows.Count == 0)
                        {

                            Hashtable ht_Chklist = new Hashtable();
                            DataTable dt_Chklist = new DataTable();

                            ht_Chklist.Add("@Trans", "INSERT");
                            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist.Add("@Yes", chk_yes);
                            ht_Chklist.Add("@No", chk_no);
                            ht_Chklist.Add("@Order_Id", Order_Id);
                            ht_Chklist.Add("@Order_Task", Order_Task);
                            ht_Chklist.Add("@Comments", Comments);
                            ht_Chklist.Add("@Status", "True");
                            ht_Chklist.Add("@User_id", user_ID);
                            ht_Chklist.Add("@Work_Type", Work_Type_Id);
                            ht_Chklist.Add("@Inserted_Date", DateTime.Now);

                            object dtcount = dataaccess.ExecuteSPForScalar("Sp_Checklist_Detail", ht_Chklist);

                            int checklistId = int.Parse(dtcount.ToString());
                        }
                    }
                }
              
            }
           

           
        }

        private void btn_Others_Finish_Click(object sender, EventArgs e)
        {
            //Others_List();                //7-07-2017
            if (Validate_Others_Question_New() != false)
            {
                Error_Tab_Count = 0;
                tabControl1.SelectTab("tabPage7");

            }
           // Save_Others_List();                             //10-07-2017
        }


        private void grd_Others_Checklist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[e.RowIndex].Cells["Column46"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[e.RowIndex].Cells["Column47"].FormattedValue);

                    if (chk_yes != false)
                    {
                        grd_Others_Checklist[6, e.RowIndex].Value = false;
                        grd_Others_Checklist[5, e.RowIndex].Value = true;
                        grd_Others_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Others_Checklist[7, e.RowIndex].ReadOnly = true;

                    }
                    else if (chk_yes != true)
                    {
                        grd_Others_Checklist[5, e.RowIndex].Value = true;
                        grd_Others_Checklist[6, e.RowIndex].Value = false;
                        grd_Others_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Others_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Others_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Others_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Others_Checklist[7, e.RowIndex].Value = "";
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    bool chk_no = false;
                    chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[e.RowIndex].Cells["Column47"].FormattedValue);

                    if (chk_no != false)
                    {
                        grd_Others_Checklist[6, e.RowIndex].Value = true;
                        grd_Others_Checklist[5, e.RowIndex].Value = false;
                        grd_Others_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Others_Checklist[7, e.RowIndex].ReadOnly = true;

                        if (chk_no != false)
                        {
                            grd_Others_Checklist[6, e.RowIndex].Value = false;
                            grd_Others_Checklist[5, e.RowIndex].Value = false;
                            grd_Others_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                            grd_Others_Checklist[7, e.RowIndex].ReadOnly = true;
                            grd_Others_Checklist[7, e.RowIndex].Value = "";
                        }

                    }
                    else if (chk_no != true)
                    {
                        grd_Others_Checklist[5, e.RowIndex].Value = false;
                        grd_Others_Checklist[6, e.RowIndex].Value = true;
                        grd_Others_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Others_Checklist[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Others_Checklist[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Others_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Others_Checklist[7, e.RowIndex].Value = "";
                    }
                }
                if (e.ColumnIndex == 7)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[e.RowIndex].Cells["Column46"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[e.RowIndex].Cells["Column47"].FormattedValue);
                    string comments = grd_Others_Checklist.Rows[e.RowIndex].Cells["Column48"].ToString();

                    if (chk_yes != false)
                    {
                        grd_Others_Checklist[7, e.RowIndex].ReadOnly = true;
                        grd_Others_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                    
                    }
                    else if (chk_yes != true)
                    {
                        grd_Others_Checklist[7, e.RowIndex].ReadOnly = false;
                        grd_Others_Checklist[7, e.RowIndex].Style.BackColor = Color.White;
                    }

                    if (chk_yes == false && chk_no == false)
                    {
                        grd_Others_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                        grd_Others_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        grd_Others_Checklist[7, e.RowIndex].ReadOnly = true;
                     
                    }
                    else
                    {
                        grd_Others_Checklist.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                        grd_Others_Checklist.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
                    }
                }
            }
         
        }

        private void btn_Others_View_Detail_Click(object sender, EventArgs e)
        {
            Hashtable ht_Others_List = new Hashtable();
            DataTable dt_Others_List = new DataTable();

           // ht_Others_List.Add("@Trans", "ALL_OTHERS");
            ht_Others_List.Add("@Trans", "GET_ALL_VIEW");
            ht_Others_List.Add("@Ref_Checklist_Master_Type_Id", 6);
            dt_Others_List = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Others_List);
            if (dt_Others_List.Rows.Count > 0)
            {
                grd_Others_Checklist.Rows.Clear();
                for (int i = 0; i < dt_Others_List.Rows.Count; i++)
                {
                    grd_Others_Checklist.Rows.Add();
                    grd_Others_Checklist.Rows[i].Cells[0].Value = i + 1;
                    grd_Others_Checklist.Rows[i].Cells[1].Value = dt_Others_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[2].Value = dt_Others_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[3].Value = dt_Others_List.Rows[i]["Question"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[4].Value = dt_Others_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[5].Value = dt_Others_List.Rows[i]["Yes"].ToString();
                    grd_Others_Checklist.Rows[i].Cells[6].Value = dt_Others_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Others_Checklist.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Others_Checklist.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Others_Checklist[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Others_Checklist[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Others_Checklist[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Others_Checklist[6, i].Value = null;
                    }
                    grd_Others_Checklist.Rows[i].Cells[7].Value = dt_Others_List.Rows[i]["Comments"].ToString();

                    grd_Others_Checklist.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Others_Checklist.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Others_Checklist.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Others_Checklist.Rows.Clear();
                //Grid_Bind_Others_CheckList();
                Grid_Bind_All_Others();
            }
        }

        //private void Others_List()
        //{
        //    int inertval = 0;
        //    for (int i = 0; i < grd_Others_Checklist.Rows.Count; i++)
        //    {

        //        if (grd_Others_Checklist.Rows[i].Cells[7].Value != "" && grd_Others_Checklist.Rows[i].Cells[7].Value != null)
        //        {
        //            Comments = grd_Others_Checklist.Rows[i].Cells[7].Value.ToString();
        //        }
        //        bool check = false;
        //        bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column46"].FormattedValue);
        //        bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column47"].FormattedValue);

        //        if (chk_yes != null && chk_yes != false)
        //        {
        //            check = true;
        //            grd_Others_Checklist.Rows[i].Cells["Column47"].ReadOnly = true;
        //        }
        //        if (chk_no != null && chk_no != false)
        //        {
        //            check = false;
        //        }
        //        Ref_Checklist_Master_Type_Id = int.Parse(grd_Others_Checklist.Rows[i].Cells[2].Value.ToString());
        //        Checklist_Id = int.Parse(grd_Others_Checklist.Rows[i].Cells[4].Value.ToString());
        //        Question = grd_Others_Checklist.Rows[i].Cells[3].Value.ToString();

        //        if (Check_List_Tran_ID == 0)
        //        {
        //            Hashtable ht_Chklist = new Hashtable();
        //            DataTable dt_Chklist = new DataTable();

        //            ht_Chklist.Add("@Trans", "INSERT");
        //            ht_Chklist.Add("@Checklist_Id", Checklist_Id);
        //            ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
        //             ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
        //            ht_Chklist.Add("@Checked", check);
        //            ht_Chklist.Add("@Order_Id", Order_Id);
        //            ht_Chklist.Add("@Order_Task", Order_Task);
        //            ht_Chklist.Add("@Comments", Comments);
        //            ht_Chklist.Add("@Status", "True");
        //            ht_Chklist.Add("@User_id", user_ID);
        //            ht_Chklist.Add("@Inserted_Date", DateTime.Now);
        //            dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
        //            inertval = 1;
        //        }

        //    }
        //    if (inertval == 1)
        //    {

        //        MessageBox.Show("Others CheckList Added Successfully");
        //        Grid_Bind_Others_CheckList();
        //    } 
        //}

        private void btn_Others_Save_Click(object sender, EventArgs e)
        {
            int inertval = 0;
            for (int i = 0; i < grd_Others_Checklist.Rows.Count; i++)
            {

                if (grd_Others_Checklist.Rows[i].Cells[7].Value != "" && grd_Others_Checklist.Rows[i].Cells[7].Value != null)
                {
                    Comments = grd_Others_Checklist.Rows[i].Cells[7].Value.ToString();
                }
                bool check = false;
                bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column46"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells["Column47"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    check = true;
                    grd_Others_Checklist.Rows[i].Cells["Column47"].ReadOnly = true;
                }
                if (chk_no != null && chk_no != false)
                {
                    check = false;
                }
                Ref_Checklist_Master_Type_Id = int.Parse(grd_Others_Checklist.Rows[i].Cells[2].Value.ToString());
                Checklist_Id = int.Parse(grd_Others_Checklist.Rows[i].Cells[4].Value.ToString());
                Question = grd_Others_Checklist.Rows[i].Cells[3].Value.ToString();

                if (Check_List_Tran_ID == 0)
                {
                    Hashtable ht_Chklist = new Hashtable();
                    DataTable dt_Chklist = new DataTable();

                    ht_Chklist.Add("@Trans", "INSERT");
                    ht_Chklist.Add("@Checklist_Id", Checklist_Id);
                    ht_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                     ht_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                    ht_Chklist.Add("@Checked", check);
                    ht_Chklist.Add("@Order_Id", Order_Id);
                    ht_Chklist.Add("@Order_Task", Order_Task);
                    ht_Chklist.Add("@Comments", Comments);
                    ht_Chklist.Add("@Status", "True");
                    ht_Chklist.Add("@User_id", user_ID);
                    ht_Chklist.Add("@Inserted_Date", DateTime.Now);
                    dt_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist);
                    inertval = 1;
                }

            }
            if (inertval == 1)
            {

                MessageBox.Show("Others CheckList Added Successfully");
                //Grid_Bind_Others_CheckList();
                Grid_Bind_All_Others();
            }
        }

        private void grd_Others_Checklist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex != -1)
            //    {
            //        bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[e.RowIndex].Cells["Column46"].FormattedValue);
            //        bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[e.RowIndex].Cells["Column47"].FormattedValue);
            //        bool check = false;
            //        if (chk_yes != null && chk_yes != false)
            //        {
            //            check = true;
            //            grd_Others_Checklist.Rows[e.RowIndex].Cells["Column47"].ReadOnly = true;
            //            grd_Others_Checklist.Rows[e.RowIndex].Cells["Column48"].ReadOnly = true;

            //        }
            //        if (chk_no != null && chk_no != false)
            //        {

            //            check = false;
            //            grd_Others_Checklist.Rows[e.RowIndex].Cells["Column46"].ReadOnly = true;
            //            grd_Others_Checklist.Rows[e.RowIndex].Cells["Column48"].ReadOnly = false;   // comments column
            //        }

            //    }
            //}
        }



        // tab control index change event
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate_Tab_Selcted_Index();

            //if (Check_Count != 1)
            //{
            //    int aa = Error_Count;
            //    if (tabControl1.SelectedIndex == 1)
            //    {
            //        Save_General_List();
            //        if (Validate_Genral_Question() != false)
            //        {
                       
            //            if (Error_Tab_Count == 1)
            //            {

            //                Check_Count = 1;
            //                tabControl1.SelectTab("tabPage1");
            //            }
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;


            //            return;
            //        }
            //        else
            //        {
            //            Check_Count = 1;

            //            tabControl1.SelectTab("tabPage1");
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;
            //            return;
            //        }
            //    }
            //    else if (tabControl1.SelectedIndex == 2)
            //    {

            //        Save_Assessor_Tax_List();
            //        if (Validate_Genral_Question() != false && Validate_AssessorTax_Question() != false)
            //        {
                       

            //            if (Error_Tab_Count == 1)
            //            {

            //                Check_Count = 1;
            //                tabControl1.SelectTab("tabPage2");
            //            }
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;

            //            return;
            //        }
            //        else
            //        {
            //            Check_Count = 1;

            //            if (Defined_Tab_Index == 1)
            //            {
            //                Save_General_List();
            //                tabControl1.SelectTab("tabPage1");
            //            }
            //            else if (Defined_Tab_Index == 2)
            //            {
            //                Save_Assessor_Tax_List();
            //                tabControl1.SelectTab("tabPage2");

            //            }

            //            Error_Tab_Count = 0;
            //            Check_Count = 0;
            //            return;
            //        }
            //    }
            //    else if (tabControl1.SelectedIndex == 3 )
            //    {
            //        Save_Deed_List();
            //        if (Validate_Genral_Question() != false && Validate_AssessorTax_Question() != false && Validate_Deed_Question()!=false)
            //        {
                      
            //            if (Error_Tab_Count == 1)
            //            {

            //                Check_Count = 1;
            //                tabControl1.SelectTab("tabPage3");
            //            }
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;

            //            return;
            //        }
            //        else
            //        {
            //            Check_Count = 1;
            //            if (Defined_Tab_Index == 1)
            //            {
            //                Save_General_List();
            //                tabControl1.SelectTab("tabPage1");
            //            }
            //            else if (Defined_Tab_Index == 2)
            //            {
            //                Save_Assessor_Tax_List();
            //                tabControl1.SelectTab("tabPage2");

            //            }
            //            else if (Defined_Tab_Index == 3)
            //            {
            //                Save_Deed_List();
            //                //tabControl1.SelectTab("tabPage3");

            //            }


                     
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;
            //            return;
            //        }

            //    }

            //    else if (tabControl1.SelectedIndex == 4)
            //    {
            //        Save_Mortgage_List();
            //        if (Validate_Genral_Question() != false && Validate_AssessorTax_Question() != false && Validate_Deed_Question() != false && Validate_Mortgage_Question() != false)
            //        {
                      
            //            if (Error_Tab_Count == 1)
            //            {

            //                Check_Count = 1;
            //                tabControl1.SelectTab("tabPage4");
            //            }
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;

            //            return;
            //        }
            //        else
            //        {
            //            Check_Count = 1;
            //            if (Defined_Tab_Index == 1)
            //            {
            //                Save_General_List();
            //                tabControl1.SelectTab("tabPage1");
            //            }
            //            else if (Defined_Tab_Index == 2)
            //            {
            //                Save_Assessor_Tax_List();
            //                tabControl1.SelectTab("tabPage2");

            //            }
            //            else if (Defined_Tab_Index == 3)
            //            {
            //                Save_Deed_List();
            //                tabControl1.SelectTab("tabPage3");

            //            }
            //            else if (Defined_Tab_Index == 4)
            //            {
            //                Save_Mortgage_List();
            //                tabControl1.SelectTab("tabPage4");

            //            }



            //            Error_Tab_Count = 0;
            //            Check_Count = 0;
            //            return;
            //        }


            //    }

            //    else if (tabControl1.SelectedIndex == 5)
            //    {
            //        Save_Judgment_Liens_List();
            //        if (Validate_Genral_Question() != false && Validate_AssessorTax_Question() != false && Validate_Deed_Question() != false && Validate_Mortgage_Question() != false && Validate_Judgment_Liens_Question() != false)
            //        {
                      
            //            if (Error_Tab_Count == 1)
            //            {

            //                Check_Count = 1;
            //                tabControl1.SelectTab("tabPage5");
            //            }
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;

            //            return;
            //        }
            //        else
            //        {
            //            Check_Count = 1;
            //            if (Defined_Tab_Index == 1)
            //            {
            //                Save_General_List();
            //                tabControl1.SelectTab("tabPage1");
            //            }
            //            else if (Defined_Tab_Index == 2)
            //            {
            //                Save_Assessor_Tax_List();
            //                tabControl1.SelectTab("tabPage2");

            //            }
            //            else if (Defined_Tab_Index == 3)
            //            {
            //                Save_Deed_List();
            //                tabControl1.SelectTab("tabPage3");

            //            }
            //            else if (Defined_Tab_Index == 4)
            //            {
            //                Save_Mortgage_List();
            //               tabControl1.SelectTab("tabPage4");

            //            }
            //            else if (Defined_Tab_Index == 5)
            //            {
            //                Save_Judgment_Liens_List();
            //                tabControl1.SelectTab("tabPage5");

            //            }



            //            Error_Tab_Count = 0;
            //            Check_Count = 0;
            //            return;
            //        }

            //    }

            //    else if (tabControl1.SelectedIndex == 6)
            //    {
            //        Save_Others_List();
            //        if (Validate_Genral_Question() != false && Validate_AssessorTax_Question() != false && Validate_Deed_Question() != false && Validate_Mortgage_Question() != false && Validate_Judgment_Liens_Question() != false && Validate_Others_Question() != false)
            //        {
                       
            //            if (Error_Tab_Count == 1)
            //            {

            //                Check_Count = 1;
            //                tabControl1.SelectTab("tabPage6");
            //            }
            //            Error_Tab_Count = 0;
            //            Check_Count = 0;

            //            return;
            //        }
            //        else
            //        {
            //            Check_Count = 1;
            //            if (Defined_Tab_Index == 1)
            //            {
            //                Save_General_List();
            //                tabControl1.SelectTab("tabPage1");
            //            }
            //            else if (Defined_Tab_Index == 2)
            //            {
            //                Save_Assessor_Tax_List();
            //                tabControl1.SelectTab("tabPage2");

            //            }
            //            else if (Defined_Tab_Index == 3)
            //            {
            //                Save_Deed_List();
            //                tabControl1.SelectTab("tabPage3");

            //            }
            //            else if (Defined_Tab_Index == 4)
            //            {
            //                Save_Mortgage_List();
            //                tabControl1.SelectTab("tabPage4");

            //            }
            //            else if (Defined_Tab_Index == 5)
            //            {
            //                Save_Judgment_Liens_List();
            //                tabControl1.SelectTab("tabPage5");

            //            }
            //            else if (Defined_Tab_Index == 6)
            //            {
            //                Save_Others_List();
            //                tabControl1.SelectTab("tabPage6");

            //            }


            //            Error_Tab_Count = 0;
            //            Check_Count = 0;
            //            return;
            //        }

            //    }
                
            //}
          
         

        }

        private void Validate_Tab_Selcted_Index()
        {

            if (Check_Count != 1)
            {
                int aa = Error_Count;
                if (tabControl1.SelectedIndex == 1)
                {
                    
                    if (Validate_Genral_Question_New() != false)
                    {

                        if (Error_Tab_Count == 1)
                        {

                            Check_Count = 1;
                            tabControl1.SelectTab("tabPage1");
                        }
                        Error_Tab_Count = 0;
                        Check_Count = 0;


                        return;
                    }
                    else
                    {
                        Check_Count = 1;

                        tabControl1.SelectTab("tabPage1");
                        Error_Tab_Count = 0;
                        Check_Count = 0;
                        return;
                    }
                }
                else if (tabControl1.SelectedIndex == 2)
                {

                  
                    if (Validate_Genral_Question_New() != false && Validate_AssessorTax_New() != false)
                    {


                        if (Error_Tab_Count == 1)
                        {

                            Check_Count = 1;
                            tabControl1.SelectTab("tabPage2");
                        }
                        Error_Tab_Count = 0;
                        Check_Count = 0;

                        return;
                    }
                    else
                    {
                        Check_Count = 1;

                        if (Defined_Tab_Index == 1)
                        {
                           
                            tabControl1.SelectTab("tabPage1");
                        }
                        else if (Defined_Tab_Index == 2)
                        {
                         
                            tabControl1.SelectTab("tabPage2");

                        }

                        Error_Tab_Count = 0;
                        Check_Count = 0;
                        return;
                    }
                }
                else if (tabControl1.SelectedIndex == 3)
                {
                  
                    if (Validate_Genral_Question_New() != false && Validate_AssessorTax_New() != false && Validate_Deed_Question_New() != false)
                    {

                        if (Error_Tab_Count == 1)
                        {

                            Check_Count = 1;
                            tabControl1.SelectTab("tabPage3");
                        }
                        Error_Tab_Count = 0;
                        Check_Count = 0;

                        return;
                    }
                    else
                    {
                        Check_Count = 1;
                        if (Defined_Tab_Index == 1)
                        {
                           
                            tabControl1.SelectTab("tabPage1");
                        }
                        else if (Defined_Tab_Index == 2)
                        {
                           
                            tabControl1.SelectTab("tabPage2");

                        }
                        else if (Defined_Tab_Index == 3)
                        {
                          
                            //tabControl1.SelectTab("tabPage3");

                        }



                        Error_Tab_Count = 0;
                        Check_Count = 0;
                        return;
                    }

                }

                else if (tabControl1.SelectedIndex == 4)
                {
                    
                    if (Validate_Genral_Question_New() != false && Validate_AssessorTax_New() != false && Validate_Deed_Question_New() != false && Validate_Mortgage_Question_New() != false)
                    {

                        if (Error_Tab_Count == 1)
                        {

                            Check_Count = 1;
                            tabControl1.SelectTab("tabPage4");
                        }
                        Error_Tab_Count = 0;
                        Check_Count = 0;

                        return;
                    }
                    else
                    {
                        Check_Count = 1;
                        if (Defined_Tab_Index == 1)
                        {
                           
                            tabControl1.SelectTab("tabPage1");
                        }
                        else if (Defined_Tab_Index == 2)
                        {
                            
                            tabControl1.SelectTab("tabPage2");

                        }
                        else if (Defined_Tab_Index == 3)
                        {
                         
                            tabControl1.SelectTab("tabPage3");

                        }
                        else if (Defined_Tab_Index == 4)
                        {
                           
                            tabControl1.SelectTab("tabPage4");

                        }



                        Error_Tab_Count = 0;
                        Check_Count = 0;
                        return;
                    }


                }

                else if (tabControl1.SelectedIndex == 5)
                {
                
                    if (Validate_Genral_Question_New() != false && Validate_AssessorTax_New() != false && Validate_Deed_Question_New() != false && Validate_Mortgage_Question_New() != false && Validate_Judgment_Liens_Question_New() != false)
                    {

                        if (Error_Tab_Count == 1)
                        {

                            Check_Count = 1;
                            tabControl1.SelectTab("tabPage5");
                        }
                        Error_Tab_Count = 0;
                        Check_Count = 0;

                        return;
                    }
                    else
                    {
                        Check_Count = 1;
                        if (Defined_Tab_Index == 1)
                        {
                         
                            tabControl1.SelectTab("tabPage1");
                        }
                        else if (Defined_Tab_Index == 2)
                        {
                            
                            tabControl1.SelectTab("tabPage2");

                        }
                        else if (Defined_Tab_Index == 3)
                        {
                           
                            tabControl1.SelectTab("tabPage3");

                        }
                        else if (Defined_Tab_Index == 4)
                        {
                            
                            tabControl1.SelectTab("tabPage4");

                        }
                        else if (Defined_Tab_Index == 5)
                        {
                           
                            tabControl1.SelectTab("tabPage5");

                        }



                        Error_Tab_Count = 0;
                        Check_Count = 0;
                        return;
                    }

                }

                else if (tabControl1.SelectedIndex == 6)
                {
                   
                    if (Validate_Genral_Question_New() != false && Validate_AssessorTax_New() != false && Validate_Deed_Question_New() != false && Validate_Mortgage_Question_New() != false && Validate_Judgment_Liens_Question_New() != false && Validate_Others_Question_New() != false)
                    {

                        if (Error_Tab_Count == 1)
                        {

                            Check_Count = 1;
                            tabControl1.SelectTab("tabPage6");
                        }
                        Error_Tab_Count = 0;
                        Check_Count = 0;

                        return;
                    }
                    else
                    {
                        Check_Count = 1;
                        if (Defined_Tab_Index == 1)
                        {
                           
                            tabControl1.SelectTab("tabPage1");
                        }
                        else if (Defined_Tab_Index == 2)
                        {
                         
                            tabControl1.SelectTab("tabPage2");

                        }
                        else if (Defined_Tab_Index == 3)
                        {
                           
                            tabControl1.SelectTab("tabPage3");

                        }
                        else if (Defined_Tab_Index == 4)
                        {
                           
                            tabControl1.SelectTab("tabPage4");

                        }
                        else if (Defined_Tab_Index == 5)
                        {
                           
                            tabControl1.SelectTab("tabPage5");

                        }
                        else if (Defined_Tab_Index == 6)
                        {
                            
                            tabControl1.SelectTab("tabPage6");

                        }


                        Error_Tab_Count = 0;
                        Check_Count = 0;
                        return;
                    }

                }

            }
        }

        private void btn_Refresh_All_Click(object sender, EventArgs e)
        {
            //Grid_Bind_All_General();
            //Grid_Bind_Assessor_Taxes_CheckList();
            //Grid_Bind_Deed_CheckList();
            //Grid_Bind_Mortgage_CheckList();
            //Grid_Bind_JudgmentLiens_CheckList();
            //Grid_Bind_Others_CheckList();


              SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
              try
              {
                  Grid_Bind_All_General();
                  Grid_Bind_All_AssessorTax();
                  Grid_Bind_All_Deed();
                  Grid_Bind_All_Mortgage();
                  Grid_Bind_All_Others();
                  Grid_Bind_All_JudgmLien();
              }
              catch (Exception ex)
              {

                  //Close Wait Form
                  SplashScreenManager.CloseForm(false);

                  MessageBox.Show("Error Occured Please Check With Administrator");
              }
              finally
              {
                  //Close Wait Form
                  SplashScreenManager.CloseForm(false);
              }

      
        }

        private void btn_Deed_Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage2");
            //btn_Assessor_Liens_View_Click(sender, e);

        }

        private void btn_Mortgage_Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage3");
        }

        private void btn_JudgLiens_Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage4");
        }

        private void btn_Others_Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage5");
        }

        private void grd_General_Checklist_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar == (char)Keys.Tab)
             {
              
                 for(int i=0;i<grd_General_Checklist.Rows.Count;i++)
                 {
                     bool chk_yes = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells[5].FormattedValue);
                     bool chk_no = Convert.ToBoolean(grd_General_Checklist.Rows[i].Cells[6].FormattedValue);

                     

                     if (chk_yes==true)
                     {
                         grd_General_Checklist[7,i].ReadOnly = true;
                         grd_General_Checklist[7, i].Style.BackColor = SystemColors.Control;
                     }

                  }
                 return;
             }
        }

        private void grd_AssessorTaxes_Chklist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                for (int i = 0; i < grd_AssessorTaxes_Chklist.Rows.Count; i++)
                {
                    bool chk_yes = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells[5].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_AssessorTaxes_Chklist.Rows[i].Cells[6].FormattedValue);


                    if (chk_yes == true)
                    {
                        grd_AssessorTaxes_Chklist[7, i].ReadOnly = true;
                        grd_AssessorTaxes_Chklist[7, i].Style.BackColor = SystemColors.Control;
                    }
                }
                return;
            }
        }

        private void grd_Deed_Checklist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                for (int i = 0; i < grd_Deed_Checklist.Rows.Count; i++)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells[5].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Deed_Checklist.Rows[i].Cells[6].FormattedValue);


                    if (chk_yes == true)
                    {
                        grd_Deed_Checklist[7, i].ReadOnly = true;
                        grd_Deed_Checklist[7, i].Style.BackColor = SystemColors.Control;
                    }

                }
                return;
            }
        }

        private void grd_Mortgage_Checklist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                for (int i = 0; i < grd_Mortgage_Checklist.Rows.Count; i++)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells[5].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Mortgage_Checklist.Rows[i].Cells[6].FormattedValue);


                    if (chk_yes == true)
                    {
                        grd_Mortgage_Checklist[7, i].ReadOnly = true;
                        grd_Mortgage_Checklist[7, i].Style.BackColor = SystemColors.Control;
                    }

                }
                return;
            }
        }

        private void grd_Judgment_Liens_Checklist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                for (int i = 0; i < grd_Judgment_Liens_Checklist.Rows.Count; i++)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells[5].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Judgment_Liens_Checklist.Rows[i].Cells[6].FormattedValue);


                    if (chk_yes == true)
                    {
                        grd_Judgment_Liens_Checklist[7, i].ReadOnly = true;
                        grd_Judgment_Liens_Checklist[7, i].Style.BackColor = SystemColors.Control;
                    }

                }
                return;
            }
        }

        private void grd_Others_Checklist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                for (int i = 0; i < grd_Others_Checklist.Rows.Count; i++)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells[5].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Others_Checklist.Rows[i].Cells[6].FormattedValue);


                    if (chk_yes == true)
                    {
                        grd_Others_Checklist[7, i].ReadOnly = true;
                        grd_Others_Checklist[7, i].Style.BackColor = SystemColors.Control;
                    }

                }
                return;
            }
        }

       

        //private void grd_General_Checklist_KeyDown(object sender, KeyEventArgs e)
        //{
        //     if (e.KeyCode == Keys.Tab)
        //    {
        //        //SelectNextEditableCell(DataGridView grd_General_Checklist);

        //        DataGridViewCell currentCell = grd_General_Checklist.CurrentCell;
        //        if (currentCell != null)
        //        {
        //            int nextRow = currentCell.RowIndex;
        //            int nextCol = currentCell.ColumnIndex + 1;
        //            if (nextCol == grd_General_Checklist.ColumnCount)
        //            {
        //                nextCol = 0;
        //                nextRow++;
        //            }
        //            if (nextRow == grd_General_Checklist.RowCount)
        //            {
        //                nextRow = 0;
        //            }
        //            DataGridViewCell nextCell = grd_General_Checklist.Rows[nextRow].Cells[nextCol];
        //            if (nextCell != null && nextCell.Visible)
        //            {
        //                grd_General_Checklist.CurrentCell = nextCell;
        //            }
        //        }

        //    }
        //}

        //private void grd_General_Checklist_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (grd_General_Checklist.CurrentRow.Cells[7].ReadOnly)
        //    {
        //        SendKeys.Send("{tab}");
        //    }
        //}


        // CLient Specification

        // Client 

        private void btn_Client_Sumbit_Click(object sender, EventArgs e)
        {
           

            if (Validate_Client_Question_New() != false)
            {
                //cProbar.startProgress();

                 SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                 try
                 {
                     Save_General_List();
                     Save_Assessor_Tax_List();
                     Save_Deed_List();
                     Save_Mortgage_List();
                     Save_Judgment_Liens_List();
                     Save_Others_List();
                     Save_Client_List();




                     Copy_Check_List_To_Server();

                    // cProbar.stopProgress();

                     MessageBox.Show(" Check List is Updated Successfully");
                    if (InvokeRequired == false)
                    {

                        this.Invoke(new MethodInvoker(delegate
                        {

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
                        }));


                    }
                    else
                    {

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
                            
                    

                    this.Close();
                }
                 catch (Exception ex)
                 {

                     //Close Wait Form
                     SplashScreenManager.CloseForm(false);

                     MessageBox.Show("Error Occured Please Check With Administrator");
                 }
                 finally
                 {
                     //Close Wait Form
                     SplashScreenManager.CloseForm(false);
                 }
            }

         
        }

        private void Save_Client_List()
        {
            for (int i = 0; i < grd_Client_Specification.Rows.Count; i++)
            {
                grd_Client_Specification.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;

                bool chk_yes = Convert.ToBoolean(grd_Client_Specification.Rows[i].Cells["Column55"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Client_Specification.Rows[i].Cells["Column56"].FormattedValue);

                if (chk_yes != null && chk_yes != false)
                {
                    chk_yes = true;
                }
                else
                {
                    chk_yes = false;
                }
                if (chk_no != null && chk_no != false)
                {
                    chk_no = true;
                }
                else
                {
                    chk_no = false;
                }


                if (grd_Client_Specification[7, i].Value == null || grd_Client_Specification[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Client_Specification[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";

                 
                }
                else
                {
                    grd_Client_Specification[7, i].Style.BackColor = Color.White;
                    Comments = grd_Client_Specification.Rows[i].Cells[7].Value.ToString();
                }


             
                if (grd_Client_Specification.Rows[i].Cells[5].Style.BackColor != Color.Red && grd_Client_Specification.Rows[i].Cells[6].Style.BackColor != Color.Red)
                {
                    if (grd_Client_Specification[7, i].Style.BackColor != Color.Red)
                    {
                        Ref_Checklist_Master_Type_Id = int.Parse(grd_Client_Specification.Rows[i].Cells[2].Value.ToString());
                        Checklist_Id = int.Parse(grd_Client_Specification.Rows[i].Cells[4].Value.ToString());
                        Question = grd_Client_Specification.Rows[i].Cells[3].Value.ToString();

                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Checklist_Id", Checklist_Id);
                        htcheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                        htcheck.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                        htcheck.Add("@User_id", user_ID);
                        htcheck.Add("@Order_Id", Order_Id);
                        htcheck.Add("@Order_Task", Order_Task);
                        //htcheck.Add("@Client_Id", Client_ID);
                        htcheck.Add("@Work_Type", Work_Type_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Checklist_Detail", htcheck);

                        if (dtcheck.Rows.Count > 0)
                        {

                            //Chklist_Client_Trans_ID = int.Parse(dtcheck.Rows[0]["Chklist_Client_Trans_ID"].ToString());
                            Check_List_Tran_ID = int.Parse(dtcheck.Rows[0]["Check_List_Tran_ID"].ToString());
                            Hashtable ht_Client_Chklist = new Hashtable();
                            DataTable dt_Client_Chklist = new DataTable();

                            ht_Client_Chklist.Add("@Trans", "UPDATE");
                            //ht_Client_Chklist.Add("@Chklist_Client_Trans_ID", Chklist_Client_Trans_ID);

                            ht_Client_Chklist.Add("@Check_List_Tran_ID", Check_List_Tran_ID);
                            ht_Client_Chklist.Add("@Checklist_Id", Checklist_Id);
                            ht_Client_Chklist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            ht_Client_Chklist.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Client_Chklist.Add("@Yes", chk_yes);
                            ht_Client_Chklist.Add("@No", chk_no);
                            ht_Client_Chklist.Add("@Order_Id", Order_Id);
                            ht_Client_Chklist.Add("@Order_Task", Order_Task);
                            ht_Client_Chklist.Add("@Work_Type", Work_Type_Id);
                            ht_Client_Chklist.Add("@Comments", Comments);
                            ht_Client_Chklist.Add("@Status", "True");
                            ht_Client_Chklist.Add("@User_id", user_ID);
                            ht_Client_Chklist.Add("@Modified_By", user_ID);
                            ht_Client_Chklist.Add("@Modified_Date", DateTime.Now);
                            ht_Client_Chklist.Add("@Client_Id", clientid);
                            dt_Client_Chklist = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Client_Chklist);

                        }
                        else if (dtcheck.Rows.Count == 0)
                        {

                            Hashtable ht_Chklist_Insert = new Hashtable();
                            DataTable dt_Chklist_Insert = new DataTable();

                            ht_Chklist_Insert.Add("@Trans", "INSERT");
                            ht_Chklist_Insert.Add("@Checklist_Id", Checklist_Id);
                            ht_Chklist_Insert.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            ht_Chklist_Insert.Add("@Order_Type_Abs_Id", OrderType_ABS_Id);
                            ht_Chklist_Insert.Add("@Yes", chk_yes);
                            ht_Chklist_Insert.Add("@No", chk_no);
                            ht_Chklist_Insert.Add("@Order_Id", Order_Id);
                            ht_Chklist_Insert.Add("@Order_Task", Order_Task);
                            ht_Chklist_Insert.Add("@Comments", Comments);
                            ht_Chklist_Insert.Add("@Status", "True");
                            ht_Chklist_Insert.Add("@User_id", user_ID);
                            ht_Chklist_Insert.Add("@Inserted_Date", DateTime.Now);
                            ht_Chklist_Insert.Add("@Work_Type", Work_Type_Id);
                            ht_Chklist_Insert.Add("@Client_Id", clientid);
                            dt_Chklist_Insert = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_Chklist_Insert);
                        }
                    }
                }
              
            }
           


        }

        private bool Validate_Client_Question()
        {
            Hashtable ht_get_max_num = new Hashtable();
            DataTable dt_get_max_num = new DataTable();

            //ht_get_max_num.Add("@Trans", "CHECK_CLIENT_COUNT");
            //ht_get_max_num.Add("@Order_Id", Order_Id);
            //ht_get_max_num.Add("@Client_Id", Client_ID);
            //ht_get_max_num.Add("@Order_Task", Order_Task);
            //ht_get_max_num.Add("@Ref_Checklist_Master_Type_Id", 7);
            //dt_get_max_num = dataaccess.ExecuteSP("Sp_CheckList_ClientSpecification_Detail", ht_get_max_num);



            ht_get_max_num.Add("@Trans", "CHECK_COUNT");
            ht_get_max_num.Add("@Order_Id", Order_Id);
            ht_get_max_num.Add("@Order_Task", Order_Task);
            ht_get_max_num.Add("@Ref_Checklist_Master_Type_Id", 7);
            ht_get_max_num.Add("@Work_Type", Work_Type_Id);
            ht_get_max_num.Add("@User_id", user_ID);
            dt_get_max_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", ht_get_max_num);
            if (dt_get_max_num.Rows.Count > 0)
            {
                Entered_Count = int.Parse(dt_get_max_num.Rows[0]["count"].ToString());
            }
            else
            {
                Entered_Count = 0;
            }
            Question_Count = int.Parse(grd_Client_Specification.Rows.Count.ToString());
            if (Entered_Count == Question_Count && Error_Count == 0)
            {
                return true;
            }

            else
            {
                Error_Count = 0;
                Defined_Tab_Index = 7;


                if (Defined_Tab_Index != 0)
                {

                }
                else
                {
                    MessageBox.Show("Need to Enter All the Fields");
                }
                return false;
            }

        }

        private bool Validate_Client_Question_New()
        {

            int Checked_Cell_Count = 0;

            for (int i = 0; i < grd_Client_Specification.Rows.Count; i++)
            {



                bool chk_yes = Convert.ToBoolean(grd_Client_Specification.Rows[i].Cells["Column55"].FormattedValue);
                bool chk_no = Convert.ToBoolean(grd_Client_Specification.Rows[i].Cells["Column56"].FormattedValue);


                if (chk_yes == true)
                {
                    int check_Count = 1;
                    Checked_Cell_Count += check_Count;

                }
                if (chk_no == true)
                {
                    int check_Count = 1;

                    Checked_Cell_Count += check_Count;

                }


                if (chk_yes == false && chk_no == false)
                {
                    grd_Client_Specification.Rows[i].Cells[5].Style.BackColor = Color.Red;
                    grd_Client_Specification.Rows[i].Cells[6].Style.BackColor = Color.Red;
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                }
                else
                {
                    grd_Client_Specification.Rows[i].Cells[5].Style.BackColor = SystemColors.Control;
                    grd_Client_Specification.Rows[i].Cells[6].Style.BackColor = SystemColors.Control;

                }

                if (grd_Client_Specification[7, i].Value == null || grd_Client_Specification[7, i].Value == "")
                {
                    if (chk_no == true)
                    {
                        grd_Client_Specification[7, i].Style.BackColor = Color.Red;
                    }
                    Comments = "";


                }
                else
                {
                    grd_Client_Specification[7, i].Style.BackColor = Color.White;
                    Comments = grd_Client_Specification.Rows[i].Cells[7].Value.ToString();
                }


                if (chk_no == true && Comments.Trim().ToString() == "")
                {
                    Error_Count = 1;
                    Error_Tab_Count = 1;
                    grd_Client_Specification[7, i].Style.BackColor = Color.Red;

                    break;
                }
                else
                {

                    Error_Count = 0;

                }



            }

            if (grd_Client_Specification.Rows.Count <= 0)
            {
                return true;
            }



            if (grd_Client_Specification.Rows.Count == Checked_Cell_Count && Error_Count!=1)
            {

                return true;
            }
            else
            {
                Error_Count = 1;
                Error_Tab_Count = 1;
                MessageBox.Show("Need to Enter All the Fields");
                return false;
            }
        }
        //private void Client_View()
        //{
        //    Hashtable ht_Client_List = new Hashtable();
        //    DataTable dt_Client_List = new DataTable();

        //    ht_Client_List.Add("@Trans", "GET_ALL_CLIENT_VIEW");
        //    ht_Client_List.Add("@Ref_Checklist_Master_Type_Id", 7);
        //    ht_Client_List.Add("@Client_Id", Client_ID);
        //    ht_Client_List.Add("@Order_Task", Order_Task);
        //    ht_Client_List.Add("@Order_Id", Order_Id);
        //    ht_Client_List.Add("@User_Id", user_ID);

        //    dt_Client_List = dataaccess.ExecuteSP("Sp_CheckList_ClientSpecification_Detail", ht_Client_List);
        //    if (dt_Client_List.Rows.Count > 0)
        //    {
        //        grd_Client_Specification.Rows.Clear();
        //        for (int i = 0; i < dt_Client_List.Rows.Count; i++)
        //        {
        //            grd_Client_Specification.Rows.Add();
        //            grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
        //            grd_Client_Specification.Rows[i].Cells[1].Value = dt_Client_List.Rows[i]["Chklist_Client_Trans_ID"].ToString();
        //            grd_Client_Specification.Rows[i].Cells[2].Value = dt_Client_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_Client_Specification.Rows[i].Cells[3].Value = dt_Client_List.Rows[i]["Question"].ToString();
        //            grd_Client_Specification.Rows[i].Cells[4].Value = dt_Client_List.Rows[i]["Checklist_Id"].ToString();
        //            grd_Client_Specification.Rows[i].Cells[5].Value = dt_Client_List.Rows[i]["Yes"].ToString();
        //            grd_Client_Specification.Rows[i].Cells[6].Value = dt_Client_List.Rows[i]["No"].ToString();

        //            string chk_yes = grd_Client_Specification.Rows[i].Cells[5].Value.ToString();
        //            string chk_no = grd_Client_Specification.Rows[i].Cells[6].Value.ToString();
        //            if (chk_yes == "true")
        //            {
        //                grd_Client_Specification[5, i].Value = true;
        //            }
        //            else if (chk_yes == "")
        //            {
        //                grd_Client_Specification[5, i].Value = null;
        //            }
        //            if (chk_no == "true")
        //            {
        //                grd_Client_Specification[6, i].Value = true;
        //            }
        //            else if (chk_no == "")
        //            {
        //                grd_Client_Specification[6, i].Value = null;
        //            }
        //            grd_Client_Specification.Rows[i].Cells[7].Value = dt_Client_List.Rows[i]["Comments"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_Client_Specification.Rows.Clear();

        //        Grid_Bind_All_Clients();
        //    }
        //}

        //private void Bind_Client_View()
        //{
        //    Hashtable ht_Client_Check = new Hashtable();
        //    DataTable dt_Client_Check = new System.Data.DataTable();

        //    ht_Client_Check.Add("@Trans", "CHECK_CLIENT_ID_TASK_USER_WISE");
        //    ht_Client_Check.Add("@Ref_Checklist_Master_Type_Id", 7);
        //    ht_Client_Check.Add("@Order_Id", Order_Id);
        //    ht_Client_Check.Add("@Client_Id", Client_ID);
        //    ht_Client_Check.Add("@Order_Task", Order_Task);
        //    ht_Client_Check.Add("@User_id", user_ID);
        //    dt_Client_Check = dataaccess.ExecuteSP("Sp_CheckList_ClientSpecification_Detail", ht_Client_Check);
        //    if (dt_Client_Check.Rows.Count > 0)
        //    {
        //        Client_View();
        //    }
        //    else
        //    {

        //        Grid_Bind_All_Clients();
        //    }
        //}

        //private void Grid_Bind_All_Clients()
        //{
        //    Hashtable ht_Clnt = new Hashtable();
        //    DataTable dt_Clnt = new System.Data.DataTable();

        //    ht_Clnt.Add("@Trans", "GET_CLIENT_DETAILS");
        //    ht_Clnt.Add("@Ref_Checklist_Master_Type_Id", 7);
        //    ht_Clnt.Add("@Order_Task", Order_Task);
        //    ht_Clnt.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //    ht_Clnt.Add("@Client_Id", Client_ID);
        //    dt_Clnt = dataaccess.ExecuteSP("Sp_CheckList_ClientSpecification_Detail", ht_Clnt);
        //    if (dt_Clnt.Rows.Count > 0)
        //    {
        //        grd_Client_Specification.Rows.Clear();
        //        for (int i = 0; i < dt_Clnt.Rows.Count; i++)
        //        {
        //            grd_Client_Specification.Rows.Add();
        //            grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
        //            grd_Client_Specification.Rows[i].Cells[2].Value = dt_Clnt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
        //            grd_Client_Specification.Rows[i].Cells[3].Value = dt_Clnt.Rows[i]["Question"].ToString();
        //            grd_Client_Specification.Rows[i].Cells[4].Value = dt_Clnt.Rows[i]["Checklist_Id"].ToString();

        //            grd_Client_Specification.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //            grd_Client_Specification.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //            grd_Client_Specification.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


        //        }
        //    }
        //    else
        //    {
        //        grd_Client_Specification.Rows.Clear();
        //    }
        //}

        private void grd_Client_Specification_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Client_Specification.Rows[e.RowIndex].Cells["Column55"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Client_Specification.Rows[e.RowIndex].Cells["Column56"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_Client_Specification[6, e.RowIndex].Value = false;
                        grd_Client_Specification[5, e.RowIndex].Value = true;
                        grd_Client_Specification[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Client_Specification[7, e.RowIndex].ReadOnly = true;

                    }
                    else if (chk_yes != true)
                    {
                        grd_Client_Specification[5, e.RowIndex].Value = true;
                        grd_Client_Specification[6, e.RowIndex].Value = false;
                        grd_Client_Specification[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Client_Specification[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Client_Specification[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Client_Specification[7, e.RowIndex].ReadOnly = false;
                        grd_Client_Specification[7, e.RowIndex].Value = "";
                    }
                }
                if (e.ColumnIndex == 6)
                {

                    bool chk_no = false;
                    chk_no = Convert.ToBoolean(grd_Client_Specification.Rows[e.RowIndex].Cells["Column56"].FormattedValue);

                    if (chk_no != false)
                    {
                        grd_Client_Specification[6, e.RowIndex].Value = true;
                        grd_Client_Specification[5, e.RowIndex].Value = false;
                        grd_Client_Specification[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Client_Specification[7, e.RowIndex].ReadOnly = true;

                        if (chk_no != false)
                        {
                            grd_Judgment_Liens_Checklist[6, e.RowIndex].Value = false;
                            grd_Judgment_Liens_Checklist[5, e.RowIndex].Value = false;
                            grd_Judgment_Liens_Checklist[7, e.RowIndex].Style.BackColor = SystemColors.Control;
                            grd_Judgment_Liens_Checklist[7, e.RowIndex].ReadOnly = true;
                            grd_Judgment_Liens_Checklist[7, e.RowIndex].Value = "";
                        }

                    }
                    else if (chk_no != true)
                    {
                        grd_Client_Specification[5, e.RowIndex].Value = false;
                        grd_Client_Specification[6, e.RowIndex].Value = true;
                        grd_Client_Specification[7, e.RowIndex].Style.BackColor = Color.White;
                        grd_Client_Specification[6, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Client_Specification[5, e.RowIndex].Style.BackColor = SystemColors.Control;
                        grd_Client_Specification[7, e.RowIndex].ReadOnly = false;
                        grd_Client_Specification[7, e.RowIndex].Value = "";
                    }

                }

                if (e.ColumnIndex == 7)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Client_Specification.Rows[e.RowIndex].Cells["Column55"].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Client_Specification.Rows[e.RowIndex].Cells["Column56"].FormattedValue);
                    if (chk_yes != false)
                    {
                        grd_Client_Specification[7, e.RowIndex].ReadOnly = true;
                        grd_Client_Specification[7, e.RowIndex].Style.BackColor = SystemColors.Control;

                    }
                    else if (chk_yes != true)
                    {

                        grd_Client_Specification[7, e.RowIndex].ReadOnly = false;
                        grd_Client_Specification[7, e.RowIndex].Style.BackColor = Color.White;

                    }

                    if (chk_yes == false && chk_no == false)
                    {

                        grd_Client_Specification.Rows[e.RowIndex].Cells[5].Style.BackColor = Color.Red;
                        grd_Client_Specification.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Red;
                        grd_Client_Specification[7, e.RowIndex].ReadOnly = true;
                    }
                    else
                    {
                        grd_Client_Specification.Rows[e.RowIndex].Cells[5].Style.BackColor = SystemColors.Control;
                        grd_Client_Specification.Rows[e.RowIndex].Cells[6].Style.BackColor = SystemColors.Control;
                    }

                }
            }

        }

        private void grd_Client_Specification_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grd_Client_Specification_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                for (int i = 0; i < grd_Client_Specification.Rows.Count; i++)
                {
                    bool chk_yes = Convert.ToBoolean(grd_Client_Specification.Rows[i].Cells[5].FormattedValue);
                    bool chk_no = Convert.ToBoolean(grd_Client_Specification.Rows[i].Cells[6].FormattedValue);


                    if (chk_yes == true)
                    {
                        grd_Client_Specification[7, i].ReadOnly = true;
                        grd_Client_Specification[7, i].Style.BackColor = SystemColors.Control;
                    }

                }
                return;
            }
        }

        private void btn_Client_View_Click(object sender, EventArgs e)
        {
            Hashtable ht_Client_List = new Hashtable();
            DataTable dt_Client_List = new DataTable();

            ht_Client_List.Add("@Trans", "GET_ALL_CLIENT_VIEW");
            ht_Client_List.Add("@Ref_Checklist_Master_Type_Id", 7);
            ht_Client_List.Add("@Client_Id", clientid);
            dt_Client_List = dataaccess.ExecuteSP("Sp_CheckList_ClientSpecification_Detail", ht_Client_List);
            if (dt_Client_List.Rows.Count > 0)
            {
                grd_Client_Specification.Rows.Clear();
                for (int i = 0; i < dt_Client_List.Rows.Count; i++)
                {
                    grd_Client_Specification.Rows.Add();
                    grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                    grd_Client_Specification.Rows[i].Cells[1].Value = dt_Client_List.Rows[i]["Check_List_Tran_ID"].ToString();
                    grd_Client_Specification.Rows[i].Cells[2].Value = dt_Client_List.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[3].Value = dt_Client_List.Rows[i]["Question"].ToString();
                    grd_Client_Specification.Rows[i].Cells[4].Value = dt_Client_List.Rows[i]["Checklist_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[5].Value = dt_Client_List.Rows[i]["Yes"].ToString();
                    grd_Client_Specification.Rows[i].Cells[6].Value = dt_Client_List.Rows[i]["No"].ToString();

                    string chk_yes = grd_Client_Specification.Rows[i].Cells[5].Value.ToString();
                    string chk_no = grd_Client_Specification.Rows[i].Cells[6].Value.ToString();
                    if (chk_yes == "true")
                    {
                        grd_Client_Specification[5, i].Value = true;
                    }
                    else if (chk_yes == "")
                    {
                        grd_Client_Specification[5, i].Value = null;
                    }
                    if (chk_no == "true")
                    {
                        grd_Client_Specification[6, i].Value = true;
                    }
                    else if (chk_no == "")
                    {
                        grd_Client_Specification[6, i].Value = null;
                    }
                    grd_Client_Specification.Rows[i].Cells[7].Value = dt_Client_List.Rows[i]["Comments"].ToString();
                }
            }
            else
            {
                grd_Client_Specification.Rows.Clear();

                Grid_Bind_All_Clients();
            }
        }

        private void btn_Client_Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage6");
        }



        //Copying Source File Into Destional Folder
        private void Copy_Check_List_To_Server()
        {
          
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
            else if (Work_Type_Id == 3)
            {

                File_Name = "" + Order_Id + " - " + " SUPER QC " + Task_Type.ToString() + "CheckList" + ".pdf";
            }
            Path1 = @"\\192.168.12.33\oms\" + clientid + @"\" + subprocessid + @"\" + Order_Id + @"\" + File_Name;
            DirectoryEntry de = new DirectoryEntry(Path1, "administrator", "password1$");
            de.Username = "administrator";
            de.Password = "password1$";
            Directory.CreateDirectory(@"\\192.168.12.33\oms\" + clientid + @"\" + subprocessid + @"\" + Order_Id.ToString());
            File.Copy(Source, Path1, true);
            CR_Report();
          
        }

        private void CR_Report()
        {
            //rpt_Doc = new Reports.CrystalReport.Check_List_Report();
            rpt_Doc = new Reports.CrystalReport.Checklist_Detail_Report();
            Logon_Cr();

            rpt_Doc.SetParameterValue("@Trans", "SELECT_USER_TASK_WISE");
            rpt_Doc.SetParameterValue("@Order_Id", Order_Id);
            rpt_Doc.SetParameterValue("@Order_Task", Order_Task);
          
            rpt_Doc.SetParameterValue("@Log_In_Userid", 0);
            rpt_Doc.SetParameterValue("@Work_Type_Id", Work_Type_Id);//

            ExportOptions CrExportOptions;
            FileInfo newFile = new FileInfo(Path1);

            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();

            PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
            CrExportOptions = rpt_Doc.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            rpt_Doc.Export();

            Hashtable htorderkb = new Hashtable();
            DataTable dtorderkb = new DataTable();

            htorderkb.Add("@Trans", "INSERT");
            if (Work_Type_Id == 1)
            {
                htorderkb.Add("@Instuction", "" + Order_Task.ToString() + "Check List Report");
            }
            else if (Work_Type_Id == 2)
            {
                htorderkb.Add("@Instuction", "REWORK -" + Order_Task.ToString() + "Check List Report");

            }
            else if (Work_Type_Id == 2)
            {
                htorderkb.Add("@Instuction", "SUPER QC -" + Order_Task.ToString() + "Check List Report");

            }
            htorderkb.Add("@Order_ID", Order_Id);
            htorderkb.Add("@Document_Name", File_Name);
            htorderkb.Add("@Document_Path", Path1);
            htorderkb.Add("@Inserted_By", user_ID);
            htorderkb.Add("@Inserted_date", DateTime.Now);
            dtorderkb = dataaccess.ExecuteSP("Sp_Document_Upload", htorderkb);

        }


        private void Logon_Cr()
        {
            List<string> cl_Lgoin = Comclass.Crystal_report_Login();
            crConnectionInfo.ServerName = cl_Lgoin[0].ToString();
            crConnectionInfo.DatabaseName = cl_Lgoin[1].ToString();
            crConnectionInfo.UserID = cl_Lgoin[2].ToString();
            crConnectionInfo.Password = cl_Lgoin[3].ToString();
            CrTables = rpt_Doc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
        }

      

      



    }

}
