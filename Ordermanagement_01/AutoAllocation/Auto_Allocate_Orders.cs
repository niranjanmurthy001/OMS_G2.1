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

namespace Ordermanagement_01.AutoAllocation
{
    public partial class Auto_Allocate_Orders : Form
    {
        DataAccess dataaccess = new DataAccess();
        int Search_Count, Search_Qc_Count, Typing_Count, Typing_Qc_Count, Upload_Count, Order_Assigned_Count;
        int Order_Id;
        static int External_Client_Order_Id, External_Client_Order_Task_Id, Check_External_Production;
        int Client_Id, List_Id, Task_Id, Order_Type_Abs_Id, State_Id, County_Id, Team_Id;
        int User_Condition_Satisfied;
        string Online_Users, Production_Users, No_Orders_Users;
        int Most_Condition_Satisfied_User_Id;
        int Minimum_User_Condition_Satisfy;
        int Check_List, Check_Order_Type_Abs, Check_Search, Check_Typing, Check_User_Team;

        int User_Clint_Priority, User_List_PRiority, User_Task_Priority, User_Order_Type_Bas_Priority, Min_Client_Priority, Min_List_Priority, Min_Task_Priority, Min_Order_Type_Abs_Priority;
        int User_Team_Id, Order_Status_Id;
        string County_Type;
        bool Abstractor_Check;
        int User_Id;


        // ============================Updated Auto Allocation Format======================================
        int User_Client_Priority;
        int User_Client_Id;
        string User_Client_Priotiy_Task;
        string User_Order_Type_Abs;
        System.Data.DataTable dt_User_Client_Priority_Task = new System.Data.DataTable();

        DataTable dt_Get_User_Team_Client = new System.Data.DataTable();

        System.Data.DataTable dt_User_Client_Task = new System.Data.DataTable();

        public Auto_Allocate_Orders(int USER_ID)
        {
            InitializeComponent();
            User_Id = USER_ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Get_User_List_Task_Team_Order_Type_Abbrivation_Client_Details();
            Auto_Assign_Orders();
            
           

            //New_Auto_Allocation();

           // Auto_Deallocat_Orders();
        }


        private void New_Auto_Allocation()
        {
            //Get the user who are in online now

          //  Assign_Order_Client_Wise_Priority();

            //Hashtable htgetuseronline = new Hashtable();
            //DataTable dtcheckuseronline = new System.Data.DataTable();
            //htgetuseronline.Add("@Trans", "GET_ALL_USERS_ONLINE");
            //dtcheckuseronline = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetuseronline);

            //Online_Users = string.Empty;
            //DataTable dt_Users_No_orders = new System.Data.DataTable();


            //if (dtcheckuseronline.Rows.Count > 0)
            //{


            //    for (int i = 0; i < dtcheckuseronline.Rows.Count; i++)
            //    {
            //        Online_Users = Online_Users + dtcheckuseronline.Rows[i]["User_id"].ToString();
            //        Online_Users += (i < dtcheckuseronline.Rows.Count) ? "," : string.Empty;

            //    }

            //    Online_Users = Online_Users.TrimEnd(',');

            //}

            // get all the Users who are in Production check from the users who are in online


            //Hashtable htgetusersinproduction = new Hashtable();
            //DataTable dtgetusersinproduction = new System.Data.DataTable();
            //htgetusersinproduction.Add("@Trans", "GET_USER_IN_PRODUCTION");
            //htgetusersinproduction.Add("@Users_Id",User_Id);
            //dtgetusersinproduction = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetusersinproduction);

            //if (dtgetusersinproduction.Rows.Count > 0)
            //{

            //    // Check User is Having Orders 



            //    Hashtable htcounttotal_orders = new Hashtable();
            //    DataTable dtcounttotal_orders = new System.Data.DataTable();

            //    htcounttotal_orders.Add("@Trans", "COUNT_TOTAL_ORDERS");
            //    htcounttotal_orders.Add("@User_Id", dtgetusersinproduction.Rows[j]["User_Id"].ToString());
            //    dtcounttotal_orders = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcounttotal_orders);

            //    int Total_Order_Count;
            //    if (dtcounttotal_orders.Rows.Count > 0)
            //    {
            //        Total_Order_Count = int.Parse(dtcounttotal_orders.Rows[0]["count"].ToString());
            //    }
            //    else
            //    {

            //        Total_Order_Count = 0;


            //    }

            //    if (Total_Order_Count == 0) 
            //    {

            //        // get the List of Orders





                   
            //    }
            //    else
            //    {

            //        if (Total_Order_Count > 0)
            //        {

            //           // break;
            //        }

            //    }


            //    //Get the Order Details to do Auto Allocation


            //    Hashtable htselect = new Hashtable();
            //    System.Data.DataTable dtselect = new System.Data.DataTable();

            //    htselect.Add("@Trans", "GET_ORDER_COUNT");
            //    //htselect.Add("@Sub_ProcessId", Subprocess_id);
            //    dtselect = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htselect);


            //    if (dtselect.Rows.Count > 0)
            //    {






            //        dt_Users_No_orders.Columns.Add("User_Id");

            //        DataTable dt_User_Condition_satisfied = new System.Data.DataTable();
            //        dt_User_Condition_satisfied.Columns.Add("User_Id");
            //        dt_User_Condition_satisfied.Columns.Add("No_Conditions_Satsified");



            //        for (int i = 0; i < dtselect.Rows.Count; i++)
            //        {
            //            dt_User_Condition_satisfied.Clear();
            //            dt_Users_No_orders.Clear();


            //            for (int j = 0; j < dtgetusersinproduction.Rows.Count; j++)
            //            {
            //                DataRow dr = dt_Users_No_orders.NewRow();


            //                Hashtable htcounttotal_orders = new Hashtable();
            //                DataTable dtcounttotal_orders = new System.Data.DataTable();

            //                htcounttotal_orders.Add("@Trans", "COUNT_TOTAL_ORDERS");
            //                htcounttotal_orders.Add("@User_Id", dtgetusersinproduction.Rows[j]["User_Id"].ToString());
            //                dtcounttotal_orders = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcounttotal_orders);

            //                int Total_Order_Count;
            //                if (dtcounttotal_orders.Rows.Count > 0)
            //                {
            //                    Total_Order_Count = int.Parse(dtcounttotal_orders.Rows[0]["count"].ToString());
            //                }
            //                else
            //                {

            //                    Total_Order_Count = 0;


            //                }



            //                if (Total_Order_Count < 1)
            //                {


            //                    dr["User_Id"] = dtgetusersinproduction.Rows[j]["User_Id"].ToString();

            //                    dt_Users_No_orders.Rows.Add(dr);



            //                }
            //            }


            //            if (dt_Users_No_orders.Rows.Count > 0)
            //            {


            //                Client_Id = int.Parse(dtselect.Rows[i]["Client_Id"].ToString());
            //                State_Id = int.Parse(dtselect.Rows[i]["State"].ToString());
            //                County_Id = int.Parse(dtselect.Rows[i]["County"].ToString());

            //                Task_Id = int.Parse(dtselect.Rows[i]["Order_Status"].ToString());

            //                Order_Type_Abs_Id = int.Parse(dtselect.Rows[i]["OrderType_ABS_Id"].ToString());

            //                Order_Id = int.Parse(dtselect.Rows[i]["Order_ID"].ToString());



            //                Hashtable htgetlist_Id = new Hashtable();
            //                DataTable dtget_list_id = new System.Data.DataTable();

            //                htgetlist_Id.Add("@Trans", "GET_LIST_ID_BY_STATE_COUNTY");
            //                htgetlist_Id.Add("@State_Id", State_Id);
            //                htgetlist_Id.Add("@County_Id", County_Id);
            //                dtget_list_id = dataaccess.ExecuteSP("Sp_Auto_Allocation_Genral", htgetlist_Id);

            //                if (dtget_list_id.Rows.Count > 0)
            //                {

            //                    List_Id = int.Parse(dtget_list_id.Rows[0]["List_Id"].ToString());
            //                }
            //                else
            //                {

            //                    List_Id = 0;
            //                    //drerror["Order_Id"] = Order_Id;
            //                    //drerror["Error"] = "List Not Found For This Order State and County";


            //                }

            //                //check the order is assigened or not

            //                //Check orders Assigned for users ---Only one order to be allocated


            //                Hashtable htcheckorderasgned = new Hashtable();
            //                DataTable dtcheckorderassgned = new System.Data.DataTable();

            //                htcheckorderasgned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
            //                htcheckorderasgned.Add("@Order_ID", Order_Id);
            //                dtcheckorderassgned = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcheckorderasgned);
            //                if (dtcheckorderassgned.Rows.Count > 0)
            //                {

            //                    Order_Assigned_Count = int.Parse(dtcheckorderassgned.Rows[0]["count"].ToString());

            //                }
            //                else
            //                {

            //                    Order_Assigned_Count = 0;
            //                }


            //                if (Order_Assigned_Count == 0)
            //                {
            //                    //Gettting All users having less order with comma separted

            //                    if (dt_Users_No_orders.Rows.Count > 0)
            //                    {
            //                        No_Orders_Users = "";
            //                        for (int m = 0; m < dt_Users_No_orders.Rows.Count; m++)
            //                        {
            //                            No_Orders_Users = No_Orders_Users + dt_Users_No_orders.Rows[m]["User_id"].ToString();
            //                            No_Orders_Users += (m < dt_Users_No_orders.Rows.Count) ? "," : string.Empty;

            //                        }

            //                        No_Orders_Users = No_Orders_Users.TrimEnd(',');
            //                    }

            //                    //Get the Team Id for This order 

            //                    Hashtable htgetteam = new Hashtable();
            //                    DataTable dtgetteam = new System.Data.DataTable();

            //                    htgetteam.Add("@Trans", "GET_TEAM_BY_CLIENT_ID");
            //                    htgetteam.Add("@Client_Id", Client_Id);
            //                    dtgetteam = dataaccess.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htgetteam);
            //                    if (dtgetteam.Rows.Count > 0)
            //                    {

            //                        Team_Id = int.Parse(dtgetteam.Rows[0]["Team_Id"].ToString());


            //                        // Get the User for this Team will do this client and this task

            //                        Hashtable htgetuserclienttask = new Hashtable();
            //                        DataTable dtgetuserclienttask = new System.Data.DataTable();
            //                        if (Task_Id == 2)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_SEARCH_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", No_Orders_Users);
            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);

            //                        }

            //                        else if (Task_Id == 3)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_SEARCH_QC_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", No_Orders_Users);
            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);

            //                        }

            //                        else if (Task_Id == 4)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_TYPING_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", No_Orders_Users);

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }

            //                        else if (Task_Id == 7)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_TYPING_QC_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", No_Orders_Users);

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }

            //                        else if (Task_Id == 12)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_UPLOAD_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", No_Orders_Users);

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }
            //                        else if (Task_Id == 23)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_FINAL_QC_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", No_Orders_Users);

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }
            //                        else if (Task_Id == 24)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_EXCEPTION_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", No_Orders_Users);

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }



            //                        if (dtgetuserclienttask.Rows.Count > 0)
            //                        {


            //                            for (int j = 0; j < dtgetuserclienttask.Rows.Count; j++)
            //                            {


            //                                //check the user will do this list and Order type abs order 
            //                                Hashtable htcheck_user_Wise_List = new Hashtable();
            //                                DataTable dtcheck_User_Wise_List = new System.Data.DataTable();

            //                                htcheck_user_Wise_List.Add("@Trans", "CHECK_LIST_BY_USER");
            //                                htcheck_user_Wise_List.Add("@List_Id", List_Id);
            //                                htcheck_user_Wise_List.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                dtcheck_User_Wise_List = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htcheck_user_Wise_List);

            //                                if (dtcheck_User_Wise_List.Rows.Count > 0)
            //                                {
            //                                    Check_List = int.Parse(dtcheck_User_Wise_List.Rows[0]["count"].ToString());

            //                                }
            //                                else
            //                                {
            //                                    Check_List = 0;

            //                                }

            //                                Hashtable htcheck_user_Wise_Order = new Hashtable();
            //                                DataTable dtcheck_User_Wise_Order = new System.Data.DataTable();

            //                                htcheck_user_Wise_Order.Add("@Trans", "CHECK_ORDERTYPEABS_BY_USER");
            //                                htcheck_user_Wise_Order.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
            //                                htcheck_user_Wise_Order.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                dtcheck_User_Wise_Order = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htcheck_user_Wise_Order);

            //                                if (dtcheck_User_Wise_Order.Rows.Count > 0)
            //                                {
            //                                    Check_Order_Type_Abs = int.Parse(dtcheck_User_Wise_Order.Rows[0]["count"].ToString());
            //                                }
            //                                else
            //                                {

            //                                    Check_Order_Type_Abs = 0;
            //                                }


            //                                //check this user is have Team Setup

            //                                Hashtable htcheck_user_Wise_Team = new Hashtable();
            //                                DataTable dtcheck_User_Wise_Team = new System.Data.DataTable();

            //                                htcheck_user_Wise_Team.Add("@Trans", "CHECK_USER_IN_TEAM");
            //                                htcheck_user_Wise_Team.Add("@Team_Id", Team_Id);
            //                                htcheck_user_Wise_Team.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                dtcheck_User_Wise_Team = dataaccess.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htcheck_user_Wise_Team);

            //                                if (dtcheck_User_Wise_Team.Rows.Count > 0)
            //                                {
            //                                    Check_User_Team = int.Parse(dtcheck_User_Wise_Team.Rows[0]["count"].ToString());
            //                                }
            //                                else
            //                                {

            //                                    Check_User_Team = 0;
            //                                }






            //                                if (Check_List != 0 && Check_Order_Type_Abs != 0 && Check_User_Team != 0)
            //                                {

            //                                    //Check Search and Typing order from same user for search qc and typing qc


            //                                    if (Task_Id == 3)
            //                                    {

            //                                        Hashtable htchektask = new Hashtable();
            //                                        DataTable dtchecktask = new System.Data.DataTable();



            //                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
            //                                        htchektask.Add("@Order_Id", Order_Id);
            //                                        htchektask.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        htchektask.Add("@Task_Id", 2);
            //                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


            //                                        if (dtchecktask.Rows.Count > 0)
            //                                        {

            //                                            Check_Search = 1;
            //                                        }
            //                                        else
            //                                        {

            //                                            Check_Search = 0;


            //                                        }

            //                                        if (Check_Search == 0)
            //                                        {

            //                                            Hashtable htchk_Assign = new Hashtable();
            //                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
            //                                            htchk_Assign.Add("@Trans", "CHECK");
            //                                            htchk_Assign.Add("@Order_Id", Order_Id);
            //                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            //                                            if (dtchk_Assign.Rows.Count > 0)
            //                                            {


            //                                                Hashtable htupassin = new Hashtable();
            //                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

            //                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
            //                                                htupassin.Add("@Order_Id", Order_Id);


            //                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
            //                                            }


            //                                            Hashtable htinsertrec = new Hashtable();
            //                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
            //                                            DateTime date = new DateTime();
            //                                            date = DateTime.Now;
            //                                            string dateeval = date.ToString("dd/MM/yyyy");
            //                                            string time = date.ToString("hh:mm tt");

            //                                            htinsertrec.Add("@Trans", "INSERT");
            //                                            htinsertrec.Add("@Order_Id", Order_Id);
            //                                            htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            htinsertrec.Add("@Order_Status_Id", Task_Id);
            //                                            htinsertrec.Add("@Order_Progress_Id", 6);
            //                                            htinsertrec.Add("@Assigned_Date", dateeval);


            //                                            htinsertrec.Add("@Inserted_date", date);
            //                                            htinsertrec.Add("@status", "True");
            //                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

            //                                            Hashtable htupdate = new Hashtable();
            //                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
            //                                            htupdate.Add("@Trans", "UPDATE_STATUS");
            //                                            htupdate.Add("@Order_ID", Order_Id);
            //                                            htupdate.Add("@Order_Status", Task_Id);

            //                                            htupdate.Add("@Modified_Date", date);
            //                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
            //                                            Hashtable htprogress = new Hashtable();
            //                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
            //                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
            //                                            htprogress.Add("@Order_ID", Order_Id);
            //                                            htprogress.Add("@Order_Progress", 6);

            //                                            htprogress.Add("@Modified_Date", date);
            //                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



            //                                            //OrderHistory
            //                                            Hashtable ht_Order_History = new Hashtable();
            //                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
            //                                            ht_Order_History.Add("@Trans", "INSERT");
            //                                            ht_Order_History.Add("@Order_Id", Order_Id);
            //                                            ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Status_Id", dtselect.Rows[i]["Order_Status"].ToString());
            //                                            ht_Order_History.Add("@Progress_Id", 6);
            //                                            ht_Order_History.Add("@Work_Type", 1);
            //                                            ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
            //                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


            //                                            //==================================External Client_Vendor_Orders=====================================================


            //                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            //                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            //                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            //                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            //                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            //                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
            //                                            {

            //                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
            //                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



            //                                                if (External_Client_Order_Task_Id != 18)
            //                                                {
            //                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
            //                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

            //                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
            //                                                }




            //                                            }





            //                                        }

            //                                    }
            //                                    else if (Task_Id == 7)
            //                                    {

            //                                        Hashtable htchektask = new Hashtable();
            //                                        DataTable dtchecktask = new System.Data.DataTable();



            //                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
            //                                        htchektask.Add("@Order_Id", Order_Id);
            //                                        htchektask.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        htchektask.Add("@Task_Id", 4);
            //                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


            //                                        if (dtchecktask.Rows.Count > 0)
            //                                        {

            //                                            Check_Typing = 1;
            //                                        }
            //                                        else
            //                                        {

            //                                            Check_Typing = 0;


            //                                        }

            //                                        if (Check_Typing == 0)
            //                                        {



            //                                            Hashtable htchk_Assign = new Hashtable();
            //                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
            //                                            htchk_Assign.Add("@Trans", "CHECK");
            //                                            htchk_Assign.Add("@Order_Id", Order_Id);
            //                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            //                                            if (dtchk_Assign.Rows.Count > 0)
            //                                            {


            //                                                Hashtable htupassin = new Hashtable();
            //                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

            //                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
            //                                                htupassin.Add("@Order_Id", Order_Id);


            //                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
            //                                            }


            //                                            Hashtable htinsertrec = new Hashtable();
            //                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
            //                                            DateTime date = new DateTime();
            //                                            date = DateTime.Now;
            //                                            string dateeval = date.ToString("dd/MM/yyyy");
            //                                            string time = date.ToString("hh:mm tt");

            //                                            htinsertrec.Add("@Trans", "INSERT");
            //                                            htinsertrec.Add("@Order_Id", Order_Id);
            //                                            htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            htinsertrec.Add("@Order_Status_Id", Task_Id);
            //                                            htinsertrec.Add("@Order_Progress_Id", 6);
            //                                            htinsertrec.Add("@Assigned_Date", dateeval);


            //                                            htinsertrec.Add("@Inserted_date", date);
            //                                            htinsertrec.Add("@status", "True");
            //                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

            //                                            Hashtable htupdate = new Hashtable();
            //                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
            //                                            htupdate.Add("@Trans", "UPDATE_STATUS");
            //                                            htupdate.Add("@Order_ID", Order_Id);
            //                                            htupdate.Add("@Order_Status", Task_Id);

            //                                            htupdate.Add("@Modified_Date", date);
            //                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
            //                                            Hashtable htprogress = new Hashtable();
            //                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
            //                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
            //                                            htprogress.Add("@Order_ID", Order_Id);
            //                                            htprogress.Add("@Order_Progress", 6);

            //                                            htprogress.Add("@Modified_Date", date);
            //                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



            //                                            //OrderHistory
            //                                            Hashtable ht_Order_History = new Hashtable();
            //                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
            //                                            ht_Order_History.Add("@Trans", "INSERT");
            //                                            ht_Order_History.Add("@Order_Id", Order_Id);
            //                                            ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Status_Id", dtselect.Rows[i]["Order_Status"].ToString());
            //                                            ht_Order_History.Add("@Progress_Id", 6);
            //                                            ht_Order_History.Add("@Work_Type", 1);
            //                                            ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
            //                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


            //                                            //==================================External Client_Vendor_Orders=====================================================


            //                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            //                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            //                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            //                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            //                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            //                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
            //                                            {

            //                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
            //                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



            //                                                if (External_Client_Order_Task_Id != 18)
            //                                                {
            //                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
            //                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

            //                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
            //                                                }




            //                                            }

            //                                        }
            //                                    }
            //                                    else
            //                                    {

            //                                        Hashtable htchk_Assign = new Hashtable();
            //                                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
            //                                        htchk_Assign.Add("@Trans", "CHECK");
            //                                        htchk_Assign.Add("@Order_Id", Order_Id);
            //                                        dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            //                                        if (dtchk_Assign.Rows.Count > 0)
            //                                        {


            //                                            Hashtable htupassin = new Hashtable();
            //                                            System.Data.DataTable dtupassign = new System.Data.DataTable();

            //                                            htupassin.Add("@Trans", "DELET_BY_ORDER");
            //                                            htupassin.Add("@Order_Id", Order_Id);


            //                                            dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
            //                                        }


            //                                        Hashtable htinsertrec = new Hashtable();
            //                                        System.Data.DataTable dtinsertrec = new System.Data.DataTable();
            //                                        DateTime date = new DateTime();
            //                                        date = DateTime.Now;
            //                                        string dateeval = date.ToString("dd/MM/yyyy");
            //                                        string time = date.ToString("hh:mm tt");

            //                                        htinsertrec.Add("@Trans", "INSERT");
            //                                        htinsertrec.Add("@Order_Id", Order_Id);
            //                                        htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        htinsertrec.Add("@Order_Status_Id", Task_Id);
            //                                        htinsertrec.Add("@Order_Progress_Id", 6);
            //                                        htinsertrec.Add("@Assigned_Date", dateeval);


            //                                        htinsertrec.Add("@Inserted_date", date);
            //                                        htinsertrec.Add("@status", "True");
            //                                        dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

            //                                        Hashtable htupdate = new Hashtable();
            //                                        System.Data.DataTable dtupdate = new System.Data.DataTable();
            //                                        htupdate.Add("@Trans", "UPDATE_STATUS");
            //                                        htupdate.Add("@Order_ID", Order_Id);
            //                                        htupdate.Add("@Order_Status", Task_Id);

            //                                        htupdate.Add("@Modified_Date", date);
            //                                        dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
            //                                        Hashtable htprogress = new Hashtable();
            //                                        System.Data.DataTable dtprogress = new System.Data.DataTable();
            //                                        htprogress.Add("@Trans", "UPDATE_PROGRESS");
            //                                        htprogress.Add("@Order_ID", Order_Id);
            //                                        htprogress.Add("@Order_Progress", 6);

            //                                        htprogress.Add("@Modified_Date", date);
            //                                        dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



            //                                        //OrderHistory
            //                                        Hashtable ht_Order_History = new Hashtable();
            //                                        System.Data.DataTable dt_Order_History = new System.Data.DataTable();
            //                                        ht_Order_History.Add("@Trans", "INSERT");
            //                                        ht_Order_History.Add("@Order_Id", Order_Id);
            //                                        ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        ht_Order_History.Add("@Status_Id", dtselect.Rows[i]["Order_Status"].ToString());
            //                                        ht_Order_History.Add("@Progress_Id", 6);
            //                                        ht_Order_History.Add("@Work_Type", 1);
            //                                        ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
            //                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


            //                                        //==================================External Client_Vendor_Orders=====================================================


            //                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            //                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            //                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            //                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            //                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            //                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
            //                                        {

            //                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
            //                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



            //                                            if (External_Client_Order_Task_Id != 18)
            //                                            {
            //                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
            //                                                System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

            //                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
            //                                            }




            //                                        }


            //                                    }



            //                                }







            //                            }







            //                        }








            //                    }






            //                }
            //                //







            //            }




            //        }

            //    }

            //}


            ////gettting the user with no order with Particular Team wise
            //if (dt_Users_No_orders.Rows.Count > 0)
            //{

            //    for (int i = 0; i < dt_Users_No_orders.Rows.Count; i++)
            //    {


            //        Hashtable htgetteamuserwise = new Hashtable();
            //        DataTable dtgetteamuserwise = new System.Data.DataTable();
            //        htgetteamuserwise.Add("@Trans", "GET_THE_USER_TEAM");
            //        htgetteamuserwise.Add("@User_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
            //        dtgetteamuserwise = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetteamuserwise);

            //        if (dtgetteamuserwise.Rows.Count > 0)
            //        {

            //            User_Team_Id = int.Parse(dtgetteamuserwise.Rows[0]["Team_Id"].ToString());
            //        }
            //        else
            //        {

            //            User_Team_Id = 0;
            //        }
            //        if (User_Team_Id != 0)
            //        {


            //            //check and get the orders in the userteam


            //            //Hashtable htgetuserteamorder = new Hashtable();
            //            //DataTable dtgetuserteamorder = new System.Data.DataTable();
            //            //htgetuserteamorder.Add("@Trans", "GET_ORDERS_BY_USER_TEAM_CLIENT_WISE");
            //            //htgetuserteamorder.Add("@User_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
            //            //htgetuserteamorder.Add("@Team_Id", User_Team_Id);
            //            //dtgetuserteamorder = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetuserteamorder);

            //            //if (dtgetuserteamorder.Rows.Count > 0)
            //            //{

            //            //    //Order will Get assign in the first loop




            //            //}
            //            //else
            //            //{ 

            //            //get the order by the userwise client

            //            Hashtable htgetuserclientorder = new Hashtable();
            //            DataTable dtgetuserclientorder = new System.Data.DataTable();
            //            htgetuserclientorder.Add("@Trans", "GET_ORDER_USER_CLIENT_WISE");
            //            htgetuserclientorder.Add("@User_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
            //            htgetuserclientorder.Add("@Team_Id", User_Team_Id);
            //            dtgetuserclientorder = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetuserclientorder);

            //            if (dtgetuserclientorder.Rows.Count > 0)
            //            {

            //                //

            //                for (int jk = 0; jk < dtgetuserclientorder.Rows.Count; jk++)
            //                {

            //                    Hashtable htcounttotal_orders = new Hashtable();
            //                    DataTable dtcounttotal_orders = new System.Data.DataTable();

            //                    htcounttotal_orders.Add("@Trans", "COUNT_TOTAL_ORDERS");
            //                    htcounttotal_orders.Add("@User_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
            //                    dtcounttotal_orders = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcounttotal_orders);

            //                    int Total_Order_Count;
            //                    if (dtcounttotal_orders.Rows.Count > 0)
            //                    {
            //                        Total_Order_Count = int.Parse(dtcounttotal_orders.Rows[0]["count"].ToString());
            //                    }
            //                    else
            //                    {

            //                        Total_Order_Count = 0;


            //                    }

            //                    if (Total_Order_Count < 1)
            //                    {


            //                        Client_Id = int.Parse(dtgetuserclientorder.Rows[jk]["Client_Id"].ToString());
            //                        State_Id = int.Parse(dtgetuserclientorder.Rows[jk]["State"].ToString());
            //                        County_Id = int.Parse(dtgetuserclientorder.Rows[jk]["County"].ToString());

            //                        Task_Id = int.Parse(dtgetuserclientorder.Rows[jk]["Order_Status"].ToString());

            //                        Order_Type_Abs_Id = int.Parse(dtgetuserclientorder.Rows[jk]["OrderType_ABS_Id"].ToString());

            //                        Order_Id = int.Parse(dtgetuserclientorder.Rows[jk]["Order_ID"].ToString());








            //                        Hashtable htgetlist_Id = new Hashtable();
            //                        DataTable dtget_list_id = new System.Data.DataTable();

            //                        htgetlist_Id.Add("@Trans", "GET_LIST_ID_BY_STATE_COUNTY");
            //                        htgetlist_Id.Add("@State_Id", State_Id);
            //                        htgetlist_Id.Add("@County_Id", County_Id);
            //                        dtget_list_id = dataaccess.ExecuteSP("Sp_Auto_Allocation_Genral", htgetlist_Id);

            //                        if (dtget_list_id.Rows.Count > 0)
            //                        {

            //                            List_Id = int.Parse(dtget_list_id.Rows[0]["List_Id"].ToString());
            //                        }
            //                        else
            //                        {

            //                            List_Id = 0;
            //                            //drerror["Order_Id"] = Order_Id;
            //                            //drerror["Error"] = "List Not Found For This Order State and County";


            //                        }

            //                        //check the order is assigened or not

            //                        //Check orders Assigned for users ---Only one order to be allocated


            //                        Hashtable htcheckorderasgned = new Hashtable();
            //                        DataTable dtcheckorderassgned = new System.Data.DataTable();

            //                        htcheckorderasgned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
            //                        htcheckorderasgned.Add("@Order_ID", Order_Id);
            //                        dtcheckorderassgned = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcheckorderasgned);
            //                        if (dtcheckorderassgned.Rows.Count > 0)
            //                        {

            //                            Order_Assigned_Count = int.Parse(dtcheckorderassgned.Rows[0]["count"].ToString());

            //                        }
            //                        else
            //                        {

            //                            Order_Assigned_Count = 0;
            //                        }

            //                        // Get the User for this Team will do this client and this task

            //                        Hashtable htgetuserclienttask = new Hashtable();
            //                        DataTable dtgetuserclienttask = new System.Data.DataTable();
            //                        if (Task_Id == 2)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_SEARCH_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);

            //                        }

            //                        else if (Task_Id == 3)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_SEARCH_QC_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);

            //                        }

            //                        else if (Task_Id == 4)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_TYPING_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }

            //                        else if (Task_Id == 7)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_TYPING_QC_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }

            //                        else if (Task_Id == 12)
            //                        {

            //                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_UPLOAD_TASK");
            //                            htgetuserclienttask.Add("@Client_Id", Client_Id);
            //                            htgetuserclienttask.Add("@Client_Status", "True");
            //                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

            //                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
            //                        }



            //                        if (dtgetuserclienttask.Rows.Count > 0)
            //                        {


            //                            for (int j = 0; j < dtgetuserclienttask.Rows.Count; j++)
            //                            {


            //                                //check the user will do this list and Order type abs order 
            //                                Hashtable htcheck_user_Wise_List = new Hashtable();
            //                                DataTable dtcheck_User_Wise_List = new System.Data.DataTable();

            //                                htcheck_user_Wise_List.Add("@Trans", "CHECK_LIST_BY_USER");
            //                                htcheck_user_Wise_List.Add("@List_Id", List_Id);
            //                                htcheck_user_Wise_List.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                dtcheck_User_Wise_List = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htcheck_user_Wise_List);

            //                                if (dtcheck_User_Wise_List.Rows.Count > 0)
            //                                {
            //                                    Check_List = int.Parse(dtcheck_User_Wise_List.Rows[0]["count"].ToString());

            //                                }
            //                                else
            //                                {
            //                                    Check_List = 0;

            //                                }

            //                                Hashtable htcheck_user_Wise_Order = new Hashtable();
            //                                DataTable dtcheck_User_Wise_Order = new System.Data.DataTable();

            //                                htcheck_user_Wise_Order.Add("@Trans", "CHECK_ORDERTYPEABS_BY_USER");
            //                                htcheck_user_Wise_Order.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
            //                                htcheck_user_Wise_Order.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                dtcheck_User_Wise_Order = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htcheck_user_Wise_Order);

            //                                if (dtcheck_User_Wise_Order.Rows.Count > 0)
            //                                {
            //                                    Check_Order_Type_Abs = int.Parse(dtcheck_User_Wise_Order.Rows[0]["count"].ToString());
            //                                }
            //                                else
            //                                {

            //                                    Check_Order_Type_Abs = 0;
            //                                }


            //                                ////check this user is have Team Setup is not required this is userwise client order allocation

            //                                //Hashtable htcheck_user_Wise_Team = new Hashtable();
            //                                //DataTable dtcheck_User_Wise_Team = new System.Data.DataTable();

            //                                //htcheck_user_Wise_Team.Add("@Trans", "CHECK_USER_IN_TEAM");
            //                                //htcheck_user_Wise_Team.Add("@Team_Id", Team_Id);
            //                                //htcheck_user_Wise_Team.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                //dtcheck_User_Wise_Team = dataaccess.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htcheck_user_Wise_Team);

            //                                //if (dtcheck_User_Wise_Team.Rows.Count > 0)
            //                                //{
            //                                //    Check_User_Team = int.Parse(dtcheck_User_Wise_Team.Rows[0]["count"].ToString());
            //                                //}
            //                                //else
            //                                //{

            //                                //    Check_User_Team = 0;
            //                                //}


            //                                if (Check_List == 0 || Order_Type_Abs_Id == 0)
            //                                {

            //                                    break;
            //                                }



            //                                else if (Check_List != 0 && Check_Order_Type_Abs != 0)
            //                                {

            //                                    //Check Search and Typing order from same user for search qc and typing qc


            //                                    if (Task_Id == 3)
            //                                    {

            //                                        Hashtable htchektask = new Hashtable();
            //                                        DataTable dtchecktask = new System.Data.DataTable();



            //                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
            //                                        htchektask.Add("@Order_Id", Order_Id);
            //                                        htchektask.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        htchektask.Add("@Task_Id", 2);
            //                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


            //                                        if (dtchecktask.Rows.Count > 0)
            //                                        {

            //                                            Check_Search = 1;
            //                                        }
            //                                        else
            //                                        {

            //                                            Check_Search = 0;


            //                                        }

            //                                        if (Check_Search == 0)
            //                                        {

            //                                            Hashtable htchk_Assign = new Hashtable();
            //                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
            //                                            htchk_Assign.Add("@Trans", "CHECK");
            //                                            htchk_Assign.Add("@Order_Id", Order_Id);
            //                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            //                                            if (dtchk_Assign.Rows.Count > 0)
            //                                            {


            //                                                Hashtable htupassin = new Hashtable();
            //                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

            //                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
            //                                                htupassin.Add("@Order_Id", Order_Id);


            //                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
            //                                            }


            //                                            Hashtable htinsertrec = new Hashtable();
            //                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
            //                                            DateTime date = new DateTime();
            //                                            date = DateTime.Now;
            //                                            string dateeval = date.ToString("dd/MM/yyyy");
            //                                            string time = date.ToString("hh:mm tt");

            //                                            htinsertrec.Add("@Trans", "INSERT");
            //                                            htinsertrec.Add("@Order_Id", Order_Id);
            //                                            htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            htinsertrec.Add("@Order_Status_Id", Task_Id);
            //                                            htinsertrec.Add("@Order_Progress_Id", 6);
            //                                            htinsertrec.Add("@Assigned_Date", dateeval);


            //                                            htinsertrec.Add("@Inserted_date", date);
            //                                            htinsertrec.Add("@status", "True");
            //                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

            //                                            Hashtable htupdate = new Hashtable();
            //                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
            //                                            htupdate.Add("@Trans", "UPDATE_STATUS");
            //                                            htupdate.Add("@Order_ID", Order_Id);
            //                                            htupdate.Add("@Order_Status", Task_Id);

            //                                            htupdate.Add("@Modified_Date", date);
            //                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
            //                                            Hashtable htprogress = new Hashtable();
            //                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
            //                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
            //                                            htprogress.Add("@Order_ID", Order_Id);
            //                                            htprogress.Add("@Order_Progress", 6);

            //                                            htprogress.Add("@Modified_Date", date);
            //                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



            //                                            //OrderHistory
            //                                            Hashtable ht_Order_History = new Hashtable();
            //                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
            //                                            ht_Order_History.Add("@Trans", "INSERT");
            //                                            ht_Order_History.Add("@Order_Id", Order_Id);
            //                                            ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Status_Id", Task_Id);
            //                                            ht_Order_History.Add("@Progress_Id", 6);
            //                                            ht_Order_History.Add("@Work_Type", 1);
            //                                            ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
            //                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


            //                                            //==================================External Client_Vendor_Orders=====================================================


            //                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            //                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            //                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            //                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            //                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            //                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
            //                                            {

            //                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
            //                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



            //                                                if (External_Client_Order_Task_Id != 18)
            //                                                {
            //                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
            //                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

            //                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
            //                                                }




            //                                            }





            //                                        }

            //                                    }
            //                                    else if (Task_Id == 7)
            //                                    {

            //                                        Hashtable htchektask = new Hashtable();
            //                                        DataTable dtchecktask = new System.Data.DataTable();



            //                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
            //                                        htchektask.Add("@Order_Id", Order_Id);
            //                                        htchektask.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        htchektask.Add("@Task_Id", 4);
            //                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


            //                                        if (dtchecktask.Rows.Count > 0)
            //                                        {

            //                                            Check_Typing = 1;
            //                                        }
            //                                        else
            //                                        {

            //                                            Check_Typing = 0;


            //                                        }

            //                                        if (Check_Typing == 0)
            //                                        {



            //                                            Hashtable htchk_Assign = new Hashtable();
            //                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
            //                                            htchk_Assign.Add("@Trans", "CHECK");
            //                                            htchk_Assign.Add("@Order_Id", Order_Id);
            //                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            //                                            if (dtchk_Assign.Rows.Count > 0)
            //                                            {


            //                                                Hashtable htupassin = new Hashtable();
            //                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

            //                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
            //                                                htupassin.Add("@Order_Id", Order_Id);


            //                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
            //                                            }


            //                                            Hashtable htinsertrec = new Hashtable();
            //                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
            //                                            DateTime date = new DateTime();
            //                                            date = DateTime.Now;
            //                                            string dateeval = date.ToString("dd/MM/yyyy");
            //                                            string time = date.ToString("hh:mm tt");

            //                                            htinsertrec.Add("@Trans", "INSERT");
            //                                            htinsertrec.Add("@Order_Id", Order_Id);
            //                                            htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            htinsertrec.Add("@Order_Status_Id", Task_Id);
            //                                            htinsertrec.Add("@Order_Progress_Id", 6);
            //                                            htinsertrec.Add("@Assigned_Date", dateeval);


            //                                            htinsertrec.Add("@Inserted_date", date);
            //                                            htinsertrec.Add("@status", "True");
            //                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

            //                                            Hashtable htupdate = new Hashtable();
            //                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
            //                                            htupdate.Add("@Trans", "UPDATE_STATUS");
            //                                            htupdate.Add("@Order_ID", Order_Id);
            //                                            htupdate.Add("@Order_Status", Task_Id);

            //                                            htupdate.Add("@Modified_Date", date);
            //                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
            //                                            Hashtable htprogress = new Hashtable();
            //                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
            //                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
            //                                            htprogress.Add("@Order_ID", Order_Id);
            //                                            htprogress.Add("@Order_Progress", 6);

            //                                            htprogress.Add("@Modified_Date", date);
            //                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



            //                                            //OrderHistory
            //                                            Hashtable ht_Order_History = new Hashtable();
            //                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
            //                                            ht_Order_History.Add("@Trans", "INSERT");
            //                                            ht_Order_History.Add("@Order_Id", Order_Id);
            //                                            ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Status_Id", Task_Id);
            //                                            ht_Order_History.Add("@Progress_Id", 6);
            //                                            ht_Order_History.Add("@Work_Type", 1);
            //                                            ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
            //                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


            //                                            //==================================External Client_Vendor_Orders=====================================================


            //                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            //                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            //                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            //                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            //                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            //                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
            //                                            {

            //                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
            //                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



            //                                                if (External_Client_Order_Task_Id != 18)
            //                                                {
            //                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
            //                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
            //                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

            //                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
            //                                                }




            //                                            }

            //                                        }
            //                                    }
            //                                    else
            //                                    {

            //                                        Hashtable htchk_Assign = new Hashtable();
            //                                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
            //                                        htchk_Assign.Add("@Trans", "CHECK");
            //                                        htchk_Assign.Add("@Order_Id", Order_Id);
            //                                        dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
            //                                        if (dtchk_Assign.Rows.Count > 0)
            //                                        {


            //                                            Hashtable htupassin = new Hashtable();
            //                                            System.Data.DataTable dtupassign = new System.Data.DataTable();

            //                                            htupassin.Add("@Trans", "DELET_BY_ORDER");
            //                                            htupassin.Add("@Order_Id", Order_Id);


            //                                            dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
            //                                        }


            //                                        Hashtable htinsertrec = new Hashtable();
            //                                        System.Data.DataTable dtinsertrec = new System.Data.DataTable();
            //                                        DateTime date = new DateTime();
            //                                        date = DateTime.Now;
            //                                        string dateeval = date.ToString("dd/MM/yyyy");
            //                                        string time = date.ToString("hh:mm tt");

            //                                        htinsertrec.Add("@Trans", "INSERT");
            //                                        htinsertrec.Add("@Order_Id", Order_Id);
            //                                        htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        htinsertrec.Add("@Order_Status_Id", Task_Id);
            //                                        htinsertrec.Add("@Order_Progress_Id", 6);
            //                                        htinsertrec.Add("@Assigned_Date", dateeval);


            //                                        htinsertrec.Add("@Inserted_date", date);
            //                                        htinsertrec.Add("@status", "True");
            //                                        dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

            //                                        Hashtable htupdate = new Hashtable();
            //                                        System.Data.DataTable dtupdate = new System.Data.DataTable();
            //                                        htupdate.Add("@Trans", "UPDATE_STATUS");
            //                                        htupdate.Add("@Order_ID", Order_Id);
            //                                        htupdate.Add("@Order_Status", Task_Id);

            //                                        htupdate.Add("@Modified_Date", date);
            //                                        dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
            //                                        Hashtable htprogress = new Hashtable();
            //                                        System.Data.DataTable dtprogress = new System.Data.DataTable();
            //                                        htprogress.Add("@Trans", "UPDATE_PROGRESS");
            //                                        htprogress.Add("@Order_ID", Order_Id);
            //                                        htprogress.Add("@Order_Progress", 6);

            //                                        htprogress.Add("@Modified_Date", date);
            //                                        dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



            //                                        //OrderHistory
            //                                        Hashtable ht_Order_History = new Hashtable();
            //                                        System.Data.DataTable dt_Order_History = new System.Data.DataTable();
            //                                        ht_Order_History.Add("@Trans", "INSERT");
            //                                        ht_Order_History.Add("@Order_Id", Order_Id);
            //                                        ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        ht_Order_History.Add("@Status_Id", Task_Id);
            //                                        ht_Order_History.Add("@Progress_Id", 6);
            //                                        ht_Order_History.Add("@Work_Type", 1);
            //                                        ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
            //                                        ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
            //                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


            //                                        //==================================External Client_Vendor_Orders=====================================================


            //                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            //                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            //                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            //                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            //                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            //                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
            //                                        {

            //                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
            //                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



            //                                            if (External_Client_Order_Task_Id != 18)
            //                                            {
            //                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
            //                                                System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
            //                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

            //                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
            //                                            }




            //                                        }


            //                                    }



            //                                }







            //                            }
            //                        }







            //                    }
            //                    else
            //                        if (Total_Order_Count > 1)
            //                        {

            //                            break;
            //                        }


            //                }

            //            }



            //        }



            //    }




            //}



        }

        private void Assign_Order_Client_Wise_Priority()
        {
            //Get the user who are in online now

            Hashtable htgetuseronline = new Hashtable();
            DataTable dtcheckuseronline = new System.Data.DataTable();
            htgetuseronline.Add("@Trans", "GET_ALL_USERS_ONLINE");
            dtcheckuseronline = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetuseronline);

            Online_Users = string.Empty;
            DataTable dt_Users_No_orders = new System.Data.DataTable();


            if (dtcheckuseronline.Rows.Count > 0)
            {


                for (int i = 0; i < dtcheckuseronline.Rows.Count; i++)
                {
                    Online_Users = Online_Users + dtcheckuseronline.Rows[i]["User_id"].ToString();
                    Online_Users += (i < dtcheckuseronline.Rows.Count) ? "," : string.Empty;

                }

                Online_Users = Online_Users.TrimEnd(',');

            }


            Hashtable htgetusersinproduction = new Hashtable();
            DataTable dtgetusersinproduction = new System.Data.DataTable();
            htgetusersinproduction.Add("@Trans", "GET_ALL_THE_USERS_WHO_ARE_IN_PRODUCTION");
            htgetusersinproduction.Add("@Users_Id", Online_Users);
            dtgetusersinproduction = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetusersinproduction);

            if (dtgetusersinproduction.Rows.Count > 0)
            {


                // Getting the Users who are not have orders in que

                //Get the Order Details to do Auto Allocation





                dt_Users_No_orders.Columns.Add("User_Id");

                DataTable dt_User_Condition_satisfied = new System.Data.DataTable();
                dt_User_Condition_satisfied.Columns.Add("User_Id");
                dt_User_Condition_satisfied.Columns.Add("No_Conditions_Satsified");



                dt_User_Condition_satisfied.Clear();
                dt_Users_No_orders.Clear();


                for (int j = 0; j < dtgetusersinproduction.Rows.Count; j++)
                {
                    DataRow dr = dt_Users_No_orders.NewRow();


                    Hashtable htcounttotal_orders = new Hashtable();
                    DataTable dtcounttotal_orders = new System.Data.DataTable();

                    htcounttotal_orders.Add("@Trans", "COUNT_TOTAL_ORDERS");
                    htcounttotal_orders.Add("@User_Id", dtgetusersinproduction.Rows[j]["User_Id"].ToString());
                    dtcounttotal_orders = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcounttotal_orders);

                    int Total_Order_Count;
                    if (dtcounttotal_orders.Rows.Count > 0)
                    {
                        Total_Order_Count = int.Parse(dtcounttotal_orders.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Total_Order_Count = 0;


                    }



                    if (Total_Order_Count < 1)
                    {


                        dr["User_Id"] = dtgetusersinproduction.Rows[j]["User_Id"].ToString();

                        dt_Users_No_orders.Rows.Add(dr);



                    }
                }


                if (dt_Users_No_orders.Rows.Count > 0)
                {



                    for (int i = 0; i < dt_Users_No_orders.Rows.Count; i++)
                    {


                        Hashtable htgetteamuserwise = new Hashtable();
                        DataTable dtgetteamuserwise = new System.Data.DataTable();
                        htgetteamuserwise.Add("@Trans", "GET_THE_USER_TEAM");
                        htgetteamuserwise.Add("@User_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
                        dtgetteamuserwise = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetteamuserwise);

                        if (dtgetteamuserwise.Rows.Count > 0)
                        {

                            User_Team_Id = int.Parse(dtgetteamuserwise.Rows[0]["Team_Id"].ToString());
                        }
                        else
                        {

                            User_Team_Id = 0;
                        }
                        if (User_Team_Id != 0)
                        {


                            //check and get the orders in the userteam


                            //get the order by the userwise client

                            Hashtable htgetuserclientorder = new Hashtable();
                            DataTable dtgetuserclientorder = new System.Data.DataTable();
                            htgetuserclientorder.Add("@Trans", "GET_ORDER_USER_CLIENT_PRIORITY_WISE");
                            htgetuserclientorder.Add("@User_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
                            htgetuserclientorder.Add("@Team_Id", User_Team_Id);
                            dtgetuserclientorder = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetuserclientorder);

                            if (dtgetuserclientorder.Rows.Count > 0)
                            {

                                //

                                for (int jk = 0; jk < dtgetuserclientorder.Rows.Count; jk++)
                                {

                                    Hashtable htcounttotal_orders = new Hashtable();
                                    DataTable dtcounttotal_orders = new System.Data.DataTable();

                                    htcounttotal_orders.Add("@Trans", "COUNT_TOTAL_ORDERS");
                                    htcounttotal_orders.Add("@User_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
                                    dtcounttotal_orders = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcounttotal_orders);

                                    int Total_Order_Count;
                                    if (dtcounttotal_orders.Rows.Count > 0)
                                    {
                                        Total_Order_Count = int.Parse(dtcounttotal_orders.Rows[0]["count"].ToString());
                                    }
                                    else
                                    {

                                        Total_Order_Count = 0;


                                    }

                                    if (Total_Order_Count < 1)
                                    {


                                        Client_Id = int.Parse(dtgetuserclientorder.Rows[jk]["Client_Id"].ToString());
                                        State_Id = int.Parse(dtgetuserclientorder.Rows[jk]["State"].ToString());
                                        County_Id = int.Parse(dtgetuserclientorder.Rows[jk]["County"].ToString());

                                        Task_Id = int.Parse(dtgetuserclientorder.Rows[jk]["Order_Status"].ToString());

                                        Order_Type_Abs_Id = int.Parse(dtgetuserclientorder.Rows[jk]["OrderType_ABS_Id"].ToString());

                                        Order_Id = int.Parse(dtgetuserclientorder.Rows[jk]["Order_ID"].ToString());








                                        Hashtable htgetlist_Id = new Hashtable();
                                        DataTable dtget_list_id = new System.Data.DataTable();

                                        htgetlist_Id.Add("@Trans", "GET_LIST_ID_BY_STATE_COUNTY");
                                        htgetlist_Id.Add("@State_Id", State_Id);
                                        htgetlist_Id.Add("@County_Id", County_Id);
                                        dtget_list_id = dataaccess.ExecuteSP("Sp_Auto_Allocation_Genral", htgetlist_Id);

                                        if (dtget_list_id.Rows.Count > 0)
                                        {

                                            List_Id = int.Parse(dtget_list_id.Rows[0]["List_Id"].ToString());
                                        }
                                        else
                                        {

                                            List_Id = 0;
                                            //drerror["Order_Id"] = Order_Id;
                                            //drerror["Error"] = "List Not Found For This Order State and County";


                                        }

                                        //check the order is assigened or not

                                        //Check orders Assigned for users ---Only one order to be allocated


                                        Hashtable htcheckorderasgned = new Hashtable();
                                        DataTable dtcheckorderassgned = new System.Data.DataTable();

                                        htcheckorderasgned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
                                        htcheckorderasgned.Add("@Order_ID", Order_Id);
                                        dtcheckorderassgned = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcheckorderasgned);
                                        if (dtcheckorderassgned.Rows.Count > 0)
                                        {

                                            Order_Assigned_Count = int.Parse(dtcheckorderassgned.Rows[0]["count"].ToString());

                                        }
                                        else
                                        {

                                            Order_Assigned_Count = 0;
                                        }

                                        // Get the User for this Team will do this client and this task

                                        Hashtable htgetuserclienttask = new Hashtable();
                                        DataTable dtgetuserclienttask = new System.Data.DataTable();
                                        if (Task_Id == 2)
                                        {

                                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_SEARCH_TASK");
                                            htgetuserclienttask.Add("@Client_Id", Client_Id);
                                            htgetuserclienttask.Add("@Client_Status", "True");
                                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
                                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);

                                        }

                                        else if (Task_Id == 3)
                                        {

                                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_SEARCH_QC_TASK");
                                            htgetuserclienttask.Add("@Client_Id", Client_Id);
                                            htgetuserclienttask.Add("@Client_Status", "True");
                                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());
                                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);

                                        }

                                        else if (Task_Id == 4)
                                        {

                                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_TYPING_TASK");
                                            htgetuserclienttask.Add("@Client_Id", Client_Id);
                                            htgetuserclienttask.Add("@Client_Status", "True");
                                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

                                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
                                        }

                                        else if (Task_Id == 7)
                                        {

                                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_TYPING_QC_TASK");
                                            htgetuserclienttask.Add("@Client_Id", Client_Id);
                                            htgetuserclienttask.Add("@Client_Status", "True");
                                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

                                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
                                        }

                                        else if (Task_Id == 12)
                                        {

                                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_UPLOAD_TASK");
                                            htgetuserclienttask.Add("@Client_Id", Client_Id);
                                            htgetuserclienttask.Add("@Client_Status", "True");
                                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

                                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
                                        }

                                        else if (Task_Id == 23)
                                        {

                                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_FINAL_QC_TASK");
                                            htgetuserclienttask.Add("@Client_Id", Client_Id);
                                            htgetuserclienttask.Add("@Client_Status", "True");
                                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

                                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
                                        }
                                        else if (Task_Id == 24)
                                        {

                                            htgetuserclienttask.Add("@Trans", "GET_THE_USERS_BY_CLIENT_AND_EXCEPTION_TASK");
                                            htgetuserclienttask.Add("@Client_Id", Client_Id);
                                            htgetuserclienttask.Add("@Client_Status", "True");
                                            htgetuserclienttask.Add("@Users_Id", dt_Users_No_orders.Rows[i]["User_Id"].ToString());

                                            dtgetuserclienttask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetuserclienttask);
                                        }



                                        if (dtgetuserclienttask.Rows.Count > 0)
                                        {


                                            for (int j = 0; j < dtgetuserclienttask.Rows.Count; j++)
                                            {


                                                //check the user will do this list and Order type abs order 
                                                Hashtable htcheck_user_Wise_List = new Hashtable();
                                                DataTable dtcheck_User_Wise_List = new System.Data.DataTable();

                                                htcheck_user_Wise_List.Add("@Trans", "CHECK_LIST_BY_USER");
                                                htcheck_user_Wise_List.Add("@List_Id", List_Id);
                                                htcheck_user_Wise_List.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                dtcheck_User_Wise_List = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htcheck_user_Wise_List);

                                                if (dtcheck_User_Wise_List.Rows.Count > 0)
                                                {
                                                    Check_List = int.Parse(dtcheck_User_Wise_List.Rows[0]["count"].ToString());

                                                }
                                                else
                                                {
                                                    Check_List = 0;

                                                }

                                                Hashtable htcheck_user_Wise_Order = new Hashtable();
                                                DataTable dtcheck_User_Wise_Order = new System.Data.DataTable();

                                                htcheck_user_Wise_Order.Add("@Trans", "CHECK_ORDERTYPEABS_BY_USER");
                                                htcheck_user_Wise_Order.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                                htcheck_user_Wise_Order.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                dtcheck_User_Wise_Order = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htcheck_user_Wise_Order);

                                                if (dtcheck_User_Wise_Order.Rows.Count > 0)
                                                {
                                                    Check_Order_Type_Abs = int.Parse(dtcheck_User_Wise_Order.Rows[0]["count"].ToString());
                                                }
                                                else
                                                {

                                                    Check_Order_Type_Abs = 0;
                                                }


                                                ////check this user is have Team Setup is not required this is userwise client order allocation

                                                //Hashtable htcheck_user_Wise_Team = new Hashtable();
                                                //DataTable dtcheck_User_Wise_Team = new System.Data.DataTable();

                                                //htcheck_user_Wise_Team.Add("@Trans", "CHECK_USER_IN_TEAM");
                                                //htcheck_user_Wise_Team.Add("@Team_Id", Team_Id);
                                                //htcheck_user_Wise_Team.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                //dtcheck_User_Wise_Team = dataaccess.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htcheck_user_Wise_Team);

                                                //if (dtcheck_User_Wise_Team.Rows.Count > 0)
                                                //{
                                                //    Check_User_Team = int.Parse(dtcheck_User_Wise_Team.Rows[0]["count"].ToString());
                                                //}
                                                //else
                                                //{

                                                //    Check_User_Team = 0;
                                                //}


                                                if (Check_List == 0 || Order_Type_Abs_Id == 0)
                                                {

                                                    break;
                                                }



                                                else if (Check_List != 0 && Check_Order_Type_Abs != 0)
                                                {

                                                    //Check Search and Typing order from same user for search qc and typing qc


                                                    if (Task_Id == 3)
                                                    {

                                                        Hashtable htchektask = new Hashtable();
                                                        DataTable dtchecktask = new System.Data.DataTable();



                                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
                                                        htchektask.Add("@Order_Id", Order_Id);
                                                        htchektask.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                        htchektask.Add("@Task_Id", 2);
                                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


                                                        if (dtchecktask.Rows.Count > 0)
                                                        {

                                                            Check_Search = 1;
                                                        }
                                                        else
                                                        {

                                                            Check_Search = 0;


                                                        }

                                                        if (Check_Search == 0)
                                                        {

                                                            Hashtable htchk_Assign = new Hashtable();
                                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                                            htchk_Assign.Add("@Trans", "CHECK");
                                                            htchk_Assign.Add("@Order_Id", Order_Id);
                                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                                            if (dtchk_Assign.Rows.Count > 0)
                                                            {


                                                                Hashtable htupassin = new Hashtable();
                                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

                                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                                htupassin.Add("@Order_Id", Order_Id);


                                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                                            }


                                                            Hashtable htinsertrec = new Hashtable();
                                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                                                            DateTime date = new DateTime();
                                                            date = DateTime.Now;
                                                            string dateeval = date.ToString("dd/MM/yyyy");
                                                            string time = date.ToString("hh:mm tt");

                                                            htinsertrec.Add("@Trans", "INSERT");
                                                            htinsertrec.Add("@Order_Id", Order_Id);
                                                            htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                            htinsertrec.Add("@Order_Status_Id", Task_Id);
                                                            htinsertrec.Add("@Order_Progress_Id", 6);
                                                            htinsertrec.Add("@Assigned_Date", dateeval);


                                                            htinsertrec.Add("@Inserted_date", date);
                                                            htinsertrec.Add("@status", "True");
                                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                                            Hashtable htupdate = new Hashtable();
                                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                                                            htupdate.Add("@Trans", "UPDATE_STATUS");
                                                            htupdate.Add("@Order_ID", Order_Id);
                                                            htupdate.Add("@Order_Status", Task_Id);

                                                            htupdate.Add("@Modified_Date", date);
                                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
                                                            Hashtable htprogress = new Hashtable();
                                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
                                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                                            htprogress.Add("@Order_ID", Order_Id);
                                                            htprogress.Add("@Order_Progress", 6);

                                                            htprogress.Add("@Modified_Date", date);
                                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



                                                            //OrderHistory
                                                            Hashtable ht_Order_History = new Hashtable();
                                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                                            ht_Order_History.Add("@Trans", "INSERT");
                                                            ht_Order_History.Add("@Order_Id", Order_Id);
                                                            ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                            ht_Order_History.Add("@Status_Id", Task_Id);
                                                            ht_Order_History.Add("@Progress_Id", 6);
                                                            ht_Order_History.Add("@Work_Type", 1);
                                                            ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
                                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                            //==================================External Client_Vendor_Orders=====================================================


                                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                            {

                                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                                if (External_Client_Order_Task_Id != 18)
                                                                {
                                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                                }




                                                            }





                                                        }

                                                    }
                                                    else if (Task_Id == 7)
                                                    {

                                                        Hashtable htchektask = new Hashtable();
                                                        DataTable dtchecktask = new System.Data.DataTable();



                                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
                                                        htchektask.Add("@Order_Id", Order_Id);
                                                        htchektask.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                        htchektask.Add("@Task_Id", 4);
                                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


                                                        if (dtchecktask.Rows.Count > 0)
                                                        {

                                                            Check_Typing = 1;
                                                        }
                                                        else
                                                        {

                                                            Check_Typing = 0;


                                                        }

                                                        if (Check_Typing == 0)
                                                        {



                                                            Hashtable htchk_Assign = new Hashtable();
                                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                                            htchk_Assign.Add("@Trans", "CHECK");
                                                            htchk_Assign.Add("@Order_Id", Order_Id);
                                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                                            if (dtchk_Assign.Rows.Count > 0)
                                                            {


                                                                Hashtable htupassin = new Hashtable();
                                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

                                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                                htupassin.Add("@Order_Id", Order_Id);


                                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                                            }


                                                            Hashtable htinsertrec = new Hashtable();
                                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                                                            DateTime date = new DateTime();
                                                            date = DateTime.Now;
                                                            string dateeval = date.ToString("dd/MM/yyyy");
                                                            string time = date.ToString("hh:mm tt");

                                                            htinsertrec.Add("@Trans", "INSERT");
                                                            htinsertrec.Add("@Order_Id", Order_Id);
                                                            htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                            htinsertrec.Add("@Order_Status_Id", Task_Id);
                                                            htinsertrec.Add("@Order_Progress_Id", 6);
                                                            htinsertrec.Add("@Assigned_Date", dateeval);


                                                            htinsertrec.Add("@Inserted_date", date);
                                                            htinsertrec.Add("@status", "True");
                                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                                            Hashtable htupdate = new Hashtable();
                                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                                                            htupdate.Add("@Trans", "UPDATE_STATUS");
                                                            htupdate.Add("@Order_ID", Order_Id);
                                                            htupdate.Add("@Order_Status", Task_Id);

                                                            htupdate.Add("@Modified_Date", date);
                                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
                                                            Hashtable htprogress = new Hashtable();
                                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
                                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                                            htprogress.Add("@Order_ID", Order_Id);
                                                            htprogress.Add("@Order_Progress", 6);

                                                            htprogress.Add("@Modified_Date", date);
                                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



                                                            //OrderHistory
                                                            Hashtable ht_Order_History = new Hashtable();
                                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                                            ht_Order_History.Add("@Trans", "INSERT");
                                                            ht_Order_History.Add("@Order_Id", Order_Id);
                                                            ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                            ht_Order_History.Add("@Status_Id", Task_Id);
                                                            ht_Order_History.Add("@Progress_Id", 6);
                                                            ht_Order_History.Add("@Work_Type", 1);
                                                            ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
                                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                            //==================================External Client_Vendor_Orders=====================================================


                                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                            {

                                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                                if (External_Client_Order_Task_Id != 18)
                                                                {
                                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
                                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                                }




                                                            }

                                                        }
                                                    }
                                                    else
                                                    {

                                                        Hashtable htchk_Assign = new Hashtable();
                                                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                                        htchk_Assign.Add("@Trans", "CHECK");
                                                        htchk_Assign.Add("@Order_Id", Order_Id);
                                                        dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                                        if (dtchk_Assign.Rows.Count > 0)
                                                        {


                                                            Hashtable htupassin = new Hashtable();
                                                            System.Data.DataTable dtupassign = new System.Data.DataTable();

                                                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                            htupassin.Add("@Order_Id", Order_Id);


                                                            dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                                        }


                                                        Hashtable htinsertrec = new Hashtable();
                                                        System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                                                        DateTime date = new DateTime();
                                                        date = DateTime.Now;
                                                        string dateeval = date.ToString("dd/MM/yyyy");
                                                        string time = date.ToString("hh:mm tt");

                                                        htinsertrec.Add("@Trans", "INSERT");
                                                        htinsertrec.Add("@Order_Id", Order_Id);
                                                        htinsertrec.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                        htinsertrec.Add("@Order_Status_Id", Task_Id);
                                                        htinsertrec.Add("@Order_Progress_Id", 6);
                                                        htinsertrec.Add("@Assigned_Date", dateeval);


                                                        htinsertrec.Add("@Inserted_date", date);
                                                        htinsertrec.Add("@status", "True");
                                                        dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                                        Hashtable htupdate = new Hashtable();
                                                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                                                        htupdate.Add("@Trans", "UPDATE_STATUS");
                                                        htupdate.Add("@Order_ID", Order_Id);
                                                        htupdate.Add("@Order_Status", Task_Id);

                                                        htupdate.Add("@Modified_Date", date);
                                                        dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
                                                        Hashtable htprogress = new Hashtable();
                                                        System.Data.DataTable dtprogress = new System.Data.DataTable();
                                                        htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                                        htprogress.Add("@Order_ID", Order_Id);
                                                        htprogress.Add("@Order_Progress", 6);

                                                        htprogress.Add("@Modified_Date", date);
                                                        dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



                                                        //OrderHistory
                                                        Hashtable ht_Order_History = new Hashtable();
                                                        System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                                        ht_Order_History.Add("@Trans", "INSERT");
                                                        ht_Order_History.Add("@Order_Id", Order_Id);
                                                        ht_Order_History.Add("@User_Id", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                        ht_Order_History.Add("@Status_Id", Task_Id);
                                                        ht_Order_History.Add("@Progress_Id", 6);
                                                        ht_Order_History.Add("@Work_Type", 1);
                                                        ht_Order_History.Add("@Assigned_By", dtgetuserclienttask.Rows[j]["User_Id"].ToString());
                                                        ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
                                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                        //==================================External Client_Vendor_Orders=====================================================


                                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                        {

                                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                            if (External_Client_Order_Task_Id != 18)
                                                            {
                                                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                                System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Task_Id);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                            }

                                                        }

                                                    }

                                                }


                                            }
                                        }







                                    }
                                    else
                                        if (Total_Order_Count > 1)
                                        {

                                            break;
                                        }


                                }

                            }



                        }



                    }


                }


            }
        }

 

        // Get the User Client,List,Task,Team Details

        private void Get_User_List_Task_Team_Order_Type_Abbrivation_Client_Details()
        { 

            // Getting  the Client Prioity and Task Details
            Hashtable ht_Get_User_Client_Task_Detials = new Hashtable();
            DataTable dt_Get_User_Client_Task_Detials = new System.Data.DataTable();
            ht_Get_User_Client_Task_Detials.Add("@Trans", "GET_USER_CLIENT_PRIOTIY_TASK");
            ht_Get_User_Client_Task_Detials.Add("@User_Id", User_Id);
            dt_Get_User_Client_Task_Detials = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", ht_Get_User_Client_Task_Detials);

            dt_User_Client_Priority_Task.Columns.Clear();

            dt_User_Client_Priority_Task.Columns.Add("Order_Task");

            // Get the Client and Task Details
            if (dt_Get_User_Client_Task_Detials.Rows.Count > 0)
            {

                User_Client_Priority = int.Parse(dt_Get_User_Client_Task_Detials.Rows[0]["Client_Id"].ToString());

                if(dt_Get_User_Client_Task_Detials.Rows[0]["Search"].ToString()=="True")
                {
                  //  DataRow dr = dt_Get_User_Client_Task_Detials.NewRow();
                    dt_User_Client_Priority_Task.Rows.Add(2);// Search
                }
                if (dt_Get_User_Client_Task_Detials.Rows[0]["Search_Qc"].ToString() == "True")
                {
                    //User_Client_Priotiy_Task = "2,3";
                    dt_User_Client_Priority_Task.Rows.Add(3);// Search_Qc
                }
                if (dt_Get_User_Client_Task_Detials.Rows[0]["Typing"].ToString() == "True")
                {
                    dt_User_Client_Priority_Task.Rows.Add(4);// typing_Qc
                }
                if (dt_Get_User_Client_Task_Detials.Rows[0]["Typing_Qc"].ToString() == "True")
                {
                    dt_User_Client_Priority_Task.Rows.Add(7);// typing_Qc
                }
                if (dt_Get_User_Client_Task_Detials.Rows[0]["Upload"].ToString() == "True")
                {
                    dt_User_Client_Priority_Task.Rows.Add(12);// Upload
                }
                if (dt_Get_User_Client_Task_Detials.Rows[0]["Final_Qc"].ToString() == "True")
                {
                    dt_User_Client_Priority_Task.Rows.Add(23);// Final_Qc
                }
                if (dt_Get_User_Client_Task_Detials.Rows[0]["Exception"].ToString() == "True")
                {
                    dt_User_Client_Priority_Task.Rows.Add(24);// Exception
                }


                for (int i = 0; i < dt_User_Client_Priority_Task.Rows.Count; i++)
                {
                    User_Client_Priotiy_Task = User_Client_Priotiy_Task + dt_User_Client_Priority_Task.Rows[i]["Order_Task"].ToString();
                    User_Client_Priotiy_Task += (i < dt_User_Client_Priority_Task.Rows.Count) ? "," : string.Empty;

                }

            }

            // get the User Order_Type_Abs Details

            Hashtable ht_Get_User_Order_Type_Abs_Detials = new Hashtable();
            DataTable dt_Get_User_Order_Type_Abs_Detials = new System.Data.DataTable();
            ht_Get_User_Order_Type_Abs_Detials.Add("@Trans", "GET_USER_ORDER_TYPE_ABS");
            ht_Get_User_Order_Type_Abs_Detials.Add("@User_Id", User_Id);
            dt_Get_User_Order_Type_Abs_Detials = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", ht_Get_User_Order_Type_Abs_Detials);

            if (dt_Get_User_Order_Type_Abs_Detials.Rows.Count > 0)
            {

                for (int i = 0; i < dt_Get_User_Order_Type_Abs_Detials.Rows.Count; i++)
                {
                    User_Order_Type_Abs = User_Order_Type_Abs + dt_Get_User_Order_Type_Abs_Detials.Rows[i]["Order_Type_Abs_Id"].ToString();
                    User_Order_Type_Abs += (i < dt_Get_User_Order_Type_Abs_Detials.Rows.Count) ? "," : string.Empty;

                }

            }




            // get the User_team and Client Details

            Hashtable ht_Get_User_Team_Client = new Hashtable();

            ht_Get_User_Team_Client.Add("@Trans", "GET_USER_TEAM_CLIENTS");
            ht_Get_User_Team_Client.Add("@User_Id", User_Id);
            dt_Get_User_Team_Client = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", ht_Get_User_Team_Client);

            //

        }


        private void Auto_Assign_Orders()
        {

            // Step1: Get the Orders Based On the Client and Priority Wise User Wise not Assigned Orders
            Hashtable ht_Check_User_Order_Count1 = new Hashtable();
            DataTable dt_Check_User_order_Count1 = new System.Data.DataTable();

            ht_Check_User_Order_Count1.Add("@Trans", "COUNT_TOTAL_ORDERS");
            ht_Check_User_Order_Count1.Add("@User_Id", User_Id);
            dt_Check_User_order_Count1 = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", ht_Check_User_Order_Count1);

            if (dt_Check_User_order_Count1.Rows.Count > 0)
            {
                Hashtable ht_get_Orders = new Hashtable();
                DataTable dt_get_Orders = new System.Data.DataTable();

                ht_get_Orders.Add("@Trans", "GET_ORDERS_BY_USER_CLIENT_PRIORITY_LIST_TASK_ORDER_ABBR");
                ht_get_Orders.Add("@Client_Id", User_Client_Priority);
                ht_get_Orders.Add("@User_Order_Task", User_Client_Priotiy_Task);
                ht_get_Orders.Add("@User_Order_Type_Abs", User_Order_Type_Abs);
                ht_get_Orders.Add("@User_Id", User_Id);
                dt_get_Orders = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", ht_get_Orders);

                // This is Getting Client Priority Orders
                if (dt_get_Orders.Rows.Count > 0)
                {


                    Hashtable ht_Check_User_Order_Count = new Hashtable();
                    DataTable dt_Check_User_order_Count = new System.Data.DataTable();

                    ht_Check_User_Order_Count.Add("@Trans", "COUNT_TOTAL_ORDERS");
                    ht_Check_User_Order_Count.Add("@User_Id", User_Id);
                    dt_Check_User_order_Count = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", ht_Check_User_Order_Count);

                    int Total_Order_Count = 0;

                    if (dt_Check_User_order_Count.Rows.Count > 0)
                    {

                        Total_Order_Count = int.Parse(dt_Check_User_order_Count.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Total_Order_Count = 0;
                    }

                    if (Total_Order_Count == 0)
                    {

                        for (int i = 0; i < dt_get_Orders.Rows.Count; i++)
                        {

                            int Order_Task_Id = int.Parse(dt_get_Orders.Rows[i]["Order_Status"].ToString());
                            int Order_Id = int.Parse(dt_get_Orders.Rows[i]["Order_ID"].ToString());

                            Hashtable htcheckorderasgned = new Hashtable();
                            DataTable dtcheckorderassgned = new System.Data.DataTable();

                            htcheckorderasgned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
                            htcheckorderasgned.Add("@Order_ID", Order_Id);
                            dtcheckorderassgned = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htcheckorderasgned);
                            if (dtcheckorderassgned.Rows.Count > 0)
                            {

                                Order_Assigned_Count = int.Parse(dtcheckorderassgned.Rows[0]["count"].ToString());

                            }
                            else
                            {

                                Order_Assigned_Count = 0;
                            }

                            // this is for  Qc Orders
                            if (Order_Assigned_Count == 0)
                            {
                                if (Order_Task_Id == 3 || Order_Task_Id == 7)
                                {
                                    if (Order_Task_Id == 3 || Order_Task_Id==23)
                                    {


                                        Hashtable htchektask = new Hashtable();
                                        DataTable dtchecktask = new System.Data.DataTable();



                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
                                        htchektask.Add("@Order_Id", Order_Id);
                                        htchektask.Add("@User_Id", User_Id);
                                        htchektask.Add("@Task_Id", 2);
                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


                                        if (dtchecktask.Rows.Count > 0)
                                        {

                                            Check_Search = 1;
                                        }
                                        else
                                        {

                                            Check_Search = 0;


                                        }

                                        if (Check_Search == 0)
                                        {

                                            Hashtable htchk_Assign = new Hashtable();
                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                            htchk_Assign.Add("@Trans", "CHECK");
                                            htchk_Assign.Add("@Order_Id", Order_Id);
                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                            if (dtchk_Assign.Rows.Count > 0)
                                            {


                                                Hashtable htupassin = new Hashtable();
                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                htupassin.Add("@Order_Id", Order_Id);


                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                            }


                                            Hashtable htinsertrec = new Hashtable();
                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            htinsertrec.Add("@Trans", "INSERT");
                                            htinsertrec.Add("@Order_Id", Order_Id);
                                            htinsertrec.Add("@User_Id", User_Id);
                                            htinsertrec.Add("@Order_Status_Id", Order_Task_Id);
                                            htinsertrec.Add("@Order_Progress_Id", 6);
                                            htinsertrec.Add("@Assigned_Date", dateeval);


                                            htinsertrec.Add("@Inserted_date", date);
                                            htinsertrec.Add("@status", "True");
                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                            Hashtable htupdate = new Hashtable();
                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                                            htupdate.Add("@Trans", "UPDATE_STATUS");
                                            htupdate.Add("@Order_ID", Order_Id);
                                            htupdate.Add("@Order_Status", Order_Task_Id);

                                            htupdate.Add("@Modified_Date", date);
                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
                                            Hashtable htprogress = new Hashtable();
                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                            htprogress.Add("@Order_ID", Order_Id);
                                            htprogress.Add("@Order_Progress", 6);

                                            htprogress.Add("@Modified_Date", date);
                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", Order_Id);
                                            ht_Order_History.Add("@User_Id", User_Id);
                                            ht_Order_History.Add("@Status_Id", Order_Task_Id);
                                            ht_Order_History.Add("@Progress_Id", 6);
                                            ht_Order_History.Add("@Work_Type", 1);
                                            ht_Order_History.Add("@Assigned_By", User_Id);
                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                            //==================================External Client_Vendor_Orders=====================================================


                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                            {

                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                if (External_Client_Order_Task_Id != 18)
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_Task_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }




                                            }



                                            break;

                                        }





                                    }
                                    else if (Order_Task_Id == 7 || Order_Task_Id==23)
                                    {

                                        Hashtable htchektask = new Hashtable();
                                        DataTable dtchecktask = new System.Data.DataTable();

                                        htchektask.Add("@Trans", "CHECK_USER_DONE_SEARCH_TYPING");
                                        htchektask.Add("@Order_Id", Order_Id);
                                        htchektask.Add("@User_Id", User_Id);
                                        htchektask.Add("@Task_Id", 4);
                                        dtchecktask = dataaccess.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchektask);


                                        if (dtchecktask.Rows.Count > 0)
                                        {

                                            Check_Typing = 1;
                                        }
                                        else
                                        {

                                            Check_Typing = 0;


                                        }

                                        if (Check_Typing == 0)
                                        {



                                            Hashtable htchk_Assign = new Hashtable();
                                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                            htchk_Assign.Add("@Trans", "CHECK");
                                            htchk_Assign.Add("@Order_Id", Order_Id);
                                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                            if (dtchk_Assign.Rows.Count > 0)
                                            {


                                                Hashtable htupassin = new Hashtable();
                                                System.Data.DataTable dtupassign = new System.Data.DataTable();

                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                htupassin.Add("@Order_Id", Order_Id);


                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                            }


                                            Hashtable htinsertrec = new Hashtable();
                                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            htinsertrec.Add("@Trans", "INSERT");
                                            htinsertrec.Add("@Order_Id", Order_Id);
                                            htinsertrec.Add("@User_Id", User_Id);
                                            htinsertrec.Add("@Order_Status_Id", Order_Task_Id);
                                            htinsertrec.Add("@Order_Progress_Id", 6);
                                            htinsertrec.Add("@Assigned_Date", dateeval);


                                            htinsertrec.Add("@Inserted_date", date);
                                            htinsertrec.Add("@status", "True");
                                            dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                            Hashtable htupdate = new Hashtable();
                                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                                            htupdate.Add("@Trans", "UPDATE_STATUS");
                                            htupdate.Add("@Order_ID", Order_Id);
                                            htupdate.Add("@Order_Status", Order_Task_Id);

                                            htupdate.Add("@Modified_Date", date);
                                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
                                            Hashtable htprogress = new Hashtable();
                                            System.Data.DataTable dtprogress = new System.Data.DataTable();
                                            htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                            htprogress.Add("@Order_ID", Order_Id);
                                            htprogress.Add("@Order_Progress", 6);

                                            htprogress.Add("@Modified_Date", date);
                                            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", Order_Id);
                                            ht_Order_History.Add("@User_Id", User_Id);
                                            ht_Order_History.Add("@Status_Id", Order_Task_Id);
                                            ht_Order_History.Add("@Progress_Id", 6);
                                            ht_Order_History.Add("@Work_Type", 1);
                                            ht_Order_History.Add("@Assigned_By", User_Id);
                                            ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                            //==================================External Client_Vendor_Orders=====================================================


                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                            {

                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                if (External_Client_Order_Task_Id != 18)
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_Task_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }




                                            }


                                            break;
                                        }

                                    }




                                }

                                else
                                {
                                    // This is for Non Qc Orders



                                    Hashtable htchk_Assign = new Hashtable();
                                    System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                    htchk_Assign.Add("@Trans", "CHECK");
                                    htchk_Assign.Add("@Order_Id", Order_Id);
                                    dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                    if (dtchk_Assign.Rows.Count > 0)
                                    {


                                        Hashtable htupassin = new Hashtable();
                                        System.Data.DataTable dtupassign = new System.Data.DataTable();

                                        htupassin.Add("@Trans", "DELET_BY_ORDER");
                                        htupassin.Add("@Order_Id", Order_Id);


                                        dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                    }


                                    Hashtable htinsertrec = new Hashtable();
                                    System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                                    DateTime date = new DateTime();
                                    date = DateTime.Now;
                                    string dateeval = date.ToString("dd/MM/yyyy");
                                    string time = date.ToString("hh:mm tt");

                                    htinsertrec.Add("@Trans", "INSERT");
                                    htinsertrec.Add("@Order_Id", Order_Id);
                                    htinsertrec.Add("@User_Id", User_Id);
                                    htinsertrec.Add("@Order_Status_Id", Order_Task_Id);
                                    htinsertrec.Add("@Order_Progress_Id", 6);
                                    htinsertrec.Add("@Assigned_Date", dateeval);


                                    htinsertrec.Add("@Inserted_date", date);
                                    htinsertrec.Add("@status", "True");
                                    dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                    Hashtable htupdate = new Hashtable();
                                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                                    htupdate.Add("@Trans", "UPDATE_STATUS");
                                    htupdate.Add("@Order_ID", Order_Id);
                                    htupdate.Add("@Order_Status", Order_Task_Id);

                                    htupdate.Add("@Modified_Date", date);
                                    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
                                    Hashtable htprogress = new Hashtable();
                                    System.Data.DataTable dtprogress = new System.Data.DataTable();
                                    htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                    htprogress.Add("@Order_ID", Order_Id);
                                    htprogress.Add("@Order_Progress", 6);

                                    htprogress.Add("@Modified_Date", date);
                                    dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



                                    //OrderHistory
                                    Hashtable ht_Order_History = new Hashtable();
                                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                    ht_Order_History.Add("@Trans", "INSERT");
                                    ht_Order_History.Add("@Order_Id", Order_Id);
                                    ht_Order_History.Add("@User_Id", User_Id);
                                    ht_Order_History.Add("@Status_Id", Order_Task_Id);
                                    ht_Order_History.Add("@Progress_Id", 6);
                                    ht_Order_History.Add("@Work_Type", 1);
                                    ht_Order_History.Add("@Assigned_By", User_Id);
                                    ht_Order_History.Add("@Modification_Type", "Order Auto Allocation");
                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                    //==================================External Client_Vendor_Orders=====================================================


                                    Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                    System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                    htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                    htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                    dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                    if (dt_Order_InTitleLogy.Rows.Count > 0)
                                    {

                                        External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                        External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                        if (External_Client_Order_Task_Id != 18)
                                        {
                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_Task_Id);
                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                        }




                                    }

                                    break;




                                }


                            }
                            else
                            {

                                // Else no need to assign;



                            }




                        }
                    }
                }
            }

            else
            {

                // Check User Team Client Orders and Task and Order_Type_Abs Wise

                //if (dt_Get_User_Team_Client.Rows.Count > 0)
                //{

                //    for (int i = 0; i < dt_Get_User_Team_Client.Rows.Count; i++)
                //    { 

                //        // Getting the User Client Wise Task Details

                //        dt_User_Client_Task.Columns.Clear();

                //        dt_User_Client_Task.Columns.Add("Order_Task");

                //        // Get the Client and Task Details
                //        if (dt_Get_User_Team_Client.Rows.Count > 0)
                //        {

                //            User_Client_Id = int.Parse(dt_User_Client_Task.Rows[i]["Client_Id"].ToString());

                //            if (dt_User_Client_Task.Rows[i]["Search"].ToString() == "True")
                //            {
                //                //  DataRow dr = dt_Get_User_Client_Task_Detials.NewRow();
                //                dt_User_Client_Priority_Task.Rows.Add(2);// Search
                //            }
                //            if (dt_User_Client_Task.Rows[i]["Search_Qc"].ToString() == "True")
                //            {
                //                //User_Client_Priotiy_Task = "2,3";
                //                dt_User_Client_Priority_Task.Rows.Add(3);// Search_Qc
                //            }
                //            if (dt_User_Client_Task.Rows[i]["Typing"].ToString() == "True")
                //            {
                //                dt_User_Client_Priority_Task.Rows.Add(4);// typing_Qc
                //            }
                //            if (dt_User_Client_Task.Rows[i]["Typing_Qc"].ToString() == "True")
                //            {
                //                dt_User_Client_Priority_Task.Rows.Add(7);// typing_Qc
                //            }
                //            if (dt_User_Client_Task.Rows[i]["Upload"].ToString() == "True")
                //            {
                //                dt_User_Client_Priority_Task.Rows.Add(12);// Upload
                //            }
                //            if (dt_User_Client_Task.Rows[i]["Final_Qc"].ToString() == "True")
                //            {
                //                dt_User_Client_Priority_Task.Rows.Add(23);// Final_Qc
                //            }
                //            if (dt_User_Client_Task.Rows[i]["Exception"].ToString() == "True")
                //            {
                //                dt_User_Client_Priority_Task.Rows.Add(24);// Exception
                //            }



                //            // Getting the Order Details based on the User Client and Task and Orde Type Abs







                //        }



                //    }



                //}



            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            // Clinet Priority Wise Order Allocate has done

                Hashtable htgetusersinproduction = new Hashtable();
                System.Data.DataTable dtgetusersinproduction = new System.Data.DataTable();
                htgetusersinproduction.Add("@Trans", "GET_USER_IN_PRODUCTION");
                htgetusersinproduction.Add("@User_Id",User_Id);
                dtgetusersinproduction = dataaccess.ExecuteSP("Sp_Auto_Allocation_Orders", htgetusersinproduction);

                if (dtgetusersinproduction.Rows.Count > 0)
                {
                    Get_User_List_Task_Team_Order_Type_Abbrivation_Client_Details();
                    Auto_Assign_Orders();
                }
            
        }

        private void Auto_Allocate_Orders_Load(object sender, EventArgs e)
        {
            this.Hide();

            this.Visible = false;

            this.ShowInTaskbar = false;
          
        }

    }
}
