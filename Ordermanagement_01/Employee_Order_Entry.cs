using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Models;
using Ordermanagement_01.New_Dashboard.Orders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class Employee_Order_Entry : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid, State_Id, County_Id, AVAILABLE_COUNT, USERCOUNT; int Taskid, Document_Count, Order_Type_ABS_id, Order_Type_Id;
        int Order_Id, Order_comp;
        bool IsOpen_jud = false, IsOpen_us = false, IsOpen_state = false, IsOpen_emp = false;

        string roleid, Order_Type_ABS;
        string SESSION_ORDER_NO;
        string Efftectiv_date;
        int Sub_ProcessId;
        string Client_Name;
        string Sub_ProcessName;
        string SESSSION_ORDER_TYPE;
        string SESSION_ORDER_TASK;
        DateTime date2;
        int No_Of_Pages;
        string OPERATE_PRODUCTION_DATE;
        int Chk_Order_Search_Cost;
        string OPERATE_SEARCH_COST;
        int MAX_TIME_ID;
        int Chk_Production_date, Check_delay_Count;
        int formProcess;
        string Client;
        string Subclient;
        int Error_Type_id;
        int Column_index;
        int Chk, Client_id;
        int DateCustom = 0;
        string[] FName;
        string Document_Name;
        string File_size;
        string View_File_Path;
        string extension;
        string Path1;
        string File_Name;
        string Directory_Path;
        int Check_List_Count, check_Docuement_List;
        DataGridViewComboBoxColumn ddl_Error_description = new DataGridViewComboBoxColumn();
        decimal SearchCost, Copy_Cost, Abstractor_Cost;
        string Check_Perform;
        int Efective_Date_Custom = 0;
        DateTime Today_Date;
        int Check_Sub, Check_Child;
        int Parent_Count, Chk_Error_Info;
        int Task_Confirm_Id;
        int Task_Question = 0;
        int External_Client_Order_Id, External_Client_Order_Task_Id, External_Client_Id, External_Sub_Client_Id;
        int Document_List_Count;
        int Title_Logy_Order_Task_Id, Title_Logy_Order_Status_Id;
        int Check_External_Production;
        int Email_Sent_Count;
        int Check_Order_Progress;
        string Inv_Status;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        ReportDocument rptDoc = new ReportDocument();
        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Tables CrTables;
        DialogResult dialogResult;
        string VIew_Type, username;
        int Package_Count;
        int Selected_Order_Id;
        int Work_Type_Id;
        int Max_Time_Id;
        int Message_Count, Internal_Tax_Check;
        int Tax_Completed;
        int Day, Hour, Prv_day;
        int Current_Holiday, Previous_Holiday;
        int Emp_Job_role_Id, Emp_Sal_Job_role_Id, Emp_Sal_Cat_Id, Eff_Client_Id, Eff_Order_Type_Abs_Id, Eff_Order_Task_Id, Eff_Order_Source_Type_Id, Eff_State_Id, Eff_County_Id, Eff_Sub_Process_Id;
        string External_Client_Order_Number;
        decimal Emp_Sal, Emp_cat_Value, Emp_Eff_Allocated_Order_Count, Eff_Order_User_Effecncy;

        int Invoice_Check_For_Condition;

        //=============================== Titlelogy Db Title Vendor Invoice ====================
        int Autoinvoice_No;
        int Invoice_No;



        string Operation;
        string Inv_Num;

        decimal invoice_Search_Cost, Invoice_Copy_Cost, Invoice_Order_Cost;
        decimal Inhouse_Search_Cost, Inhouse_Copy_Cost, Inhouse_Order_Cost;
        int Title_No_Of_Pages, Inhouse_No_Of_Pages;
        string Invoice_Number;
        int Invoice_Search_Packake_Order;
        int Search_Package_Order;
        int Invoice_Package, invoice_check, Check_Invoice_gen, Chk_Inv_Value, Chk_Inv_Page;
        decimal Titelogy_Order_Type_Wise_Invoice_Amount, Title_Logy_Probate_Cost, Title_Logy_Platmap_Cost, Total_Titlelogy_Order_Cost;
        int Title_Peak_Inv_No_Of_Pages, Title_Peak_Inv_No_Probate_Pages, Title_Peak_Inv_No_Plat_Map_Pages, Title_Peak_Inv_Total_No_Probate_And_Plat_Map_Pages;
        bool Chk_Plat_Map, Chk_Tax_Information;
        string src, des, src_qc, des_qc;
        string file_extension = "";
        double filesize;
        int Pass_Max_Time_Id;
        Thread t;
        int Tax_Completed_Count = 0;
        int Order_Task;
        int Order_Status_Id;
        int Document_Check_Type_Id = 0;
        Order_Passing_Params obj_Order_Details_List = new Order_Passing_Params();

        private bool btn_Submit_Clicked = false;
        public Employee_Order_Entry(string SESSIONORDERNO, int Orderid, int User_id, string Role_id, string OrderProcess, string SESSSIONORDERTYPE, int SESSIONORDERTASK, int WORK_TYPE_ID, int MAX_TIMING_ID, int TAX_COMPLETED)
        {
            Chk = 0;
            InitializeComponent();
            userid = User_id;

            //  username = Username;
            Order_Id = Orderid;
            Selected_Order_Id = Orderid;
            roleid = Role_id;
            SESSION_ORDER_TASK = Convert.ToString(SESSIONORDERTASK);
            Max_Time_Id = MAX_TIMING_ID;

            dbc.Bind_Employee_Order_source(ddl_Order_Source);
            dbc.Bind_Order_Progress_For_Employee_Side(ddl_order_Staus);
            SESSION_ORDER_NO = SESSIONORDERNO;
            SESSSION_ORDER_TYPE = SESSSIONORDERTYPE;
            lbl_Order_Task_Type.Text = SESSSIONORDERTYPE;
            lbl_Task_Type.Text = SESSSIONORDERTYPE;
            Work_Type_Id = WORK_TYPE_ID;
            Tax_Completed = TAX_COMPLETED;
            ddl_Order_Source.SelectedIndex = -1;
            Employee_View Emp_View = new Employee_View(3, "Search_Qc", userid, roleid, "normal", Work_Type_Id);
            Emp_View.Close();
            //Error_Cbo_Load();
        }
        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT");
            htComments.Add("@Order_Id", Order_Id);
            htComments.Add("@Work_Type", Work_Type_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_Comments", htComments);
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
                    Grid_Comments.Rows[i].Cells[1].Value = dtComments.Rows[i]["Comment_Id"].ToString();
                    Grid_Comments.Rows[i].Cells[2].Value = dtComments.Rows[i]["Comment"].ToString();
                    Grid_Comments.Rows[i].Cells[3].Value = dtComments.Rows[i]["User_Name"].ToString();

                    if (roleid == "2" || roleid == "3")
                    {

                        Grid_Comments.Columns[3].Visible = false;
                    }
                    else
                    {
                        Grid_Comments.Columns[3].Visible = true;

                    }
                }


            }
            else
            {
            }
        }



        private void Populate_Production_Date()
        {


            Hashtable htget_day = new Hashtable();
            DataTable dtget_Day = new DataTable();

            htget_day.Add("@Trans", "GET_WEEK_END_DAY");
            dtget_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_day);
            if (dtget_Day.Rows.Count > 0)
            {

                Day = int.Parse(dtget_Day.Rows[0]["Day"].ToString());

            }




            Hashtable htget_Hour = new Hashtable();
            DataTable dtget_Hour = new DataTable();

            htget_Hour.Add("@Trans", "GET_HOUR");
            dtget_Hour = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Hour);
            if (dtget_Hour.Rows.Count > 0)
            {

                Hour = int.Parse(dtget_Hour.Rows[0]["Hour"].ToString());

            }

            if (Day != null && Hour != null)
            {

                //Check Day in Week days

                //Tuesday To Friday For day Shift
                if (Day == 3 || Day == 4 || Day == 5 || Day == 6)
                {


                    //Check Hours

                    //For Day Shift
                    if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16 || Hour == 17 || Hour == 18)
                    {

                        //Check the Current Day is Holiday 

                        Hashtable htcheck_Holiday = new Hashtable();
                        DataTable dtcheck_Holiday = new DataTable();
                        htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_CURRENT_DATE");
                        dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);
                        Current_Holiday = 0;
                        if (dtcheck_Holiday.Rows.Count > 0)
                        {


                            Hashtable htget_Current_day = new Hashtable();
                            DataTable dtget_Current_Day = new DataTable();
                            htget_Current_day.Add("@Trans", "GET_CURRENT_DAY");
                            dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                            if (dtget_Current_Day.Rows.Count > 0)
                            {
                                Current_Holiday = 1;

                                txt_Prdoductiondate.Text = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                            }


                        }
                        else
                        {




                            // Check the previous Day is Holiday or not 

                            //Checking 

                            Previous_Holiday = 0;
                            Hashtable htget_Day_prod_date = new Hashtable();
                            DataTable dtget_Day_Prod_Date = new DataTable();

                            htget_Day_prod_date.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY");
                            dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                            if (dtget_Day_Prod_Date.Rows.Count > 0)
                            {



                                // txt_Prdoductiondate.Text = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();

                                htcheck_Holiday.Clear();
                                htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_DATE");
                                htcheck_Holiday.Add("@Date", dtget_Day_Prod_Date.Rows[0]["Production_Date"]);
                                dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);

                                if (dtcheck_Holiday.Rows.Count > 0)
                                {

                                    //if the Previous Day is Holiday

                                    Previous_Holiday = 1;

                                    Hashtable htget_day1 = new Hashtable();
                                    DataTable dtget_Day1 = new DataTable();

                                    htget_day1.Add("@Trans", "GET_DAY_NO_BY_DATE");
                                    htget_day1.Add("@Date", dtcheck_Holiday.Rows[0]["Holiday_date"].ToString());
                                    dtget_Day1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_day1);
                                    if (dtget_Day1.Rows.Count > 0)
                                    {

                                        Prv_day = int.Parse(dtget_Day1.Rows[0]["Day"].ToString());

                                    }


                                    if (Prv_day == 3 || Prv_day == 4 || Prv_day == 5 || Prv_day == 6)
                                    {

                                        //If its Weekdays ====== Prod.date=Holiday.Date-1


                                        Hashtable htget_Day_prod_date1 = new Hashtable();
                                        DataTable dtget_Day_prod_date1 = new DataTable();

                                        htget_Day_prod_date1.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY_BY_HOLIDAYDATE");
                                        htget_Day_prod_date1.Add("@Date", dtcheck_Holiday.Rows[0]["Holiday_date"].ToString());
                                        dtget_Day_prod_date1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date1);

                                        if (dtget_Day_prod_date1.Rows.Count > 0)
                                        {

                                            txt_Prdoductiondate.Text = dtget_Day_prod_date1.Rows[0]["Production_Date"].ToString();
                                        }


                                    }
                                    else if (Prv_day == 2)
                                    {




                                        //For Day Shift

                                        if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16 || Hour == 17 || Hour == 18)
                                        {

                                            //Gettting Friday Day if the day is monday



                                            htget_Day_prod_date.Clear();
                                            dtget_Day_Prod_Date.Clear();
                                            htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                            dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                            if (dtget_Day_Prod_Date.Rows.Count > 0)
                                            {
                                                //Check The Friday Is Holiday Or Not

                                                htcheck_Holiday.Clear();
                                                htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_DATE");
                                                htcheck_Holiday.Add("@Date", dtget_Day_Prod_Date.Rows[0]["Production_Date"]);
                                                dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);

                                                if (dtcheck_Holiday.Rows.Count > 0)
                                                {


                                                    htget_Day_prod_date.Clear();
                                                    dtget_Day_Prod_Date.Clear();
                                                    htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_LEAVE_ON_MONDAY");
                                                    dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                                    if (dtget_Day_Prod_Date.Rows.Count > 0)
                                                    {

                                                        txt_Prdoductiondate.Text = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                                    }



                                                }

                                                else
                                                {


                                                    htget_Day_prod_date.Clear();
                                                    dtget_Day_Prod_Date.Clear();
                                                    htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                                    dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                                    if (dtget_Day_Prod_Date.Rows.Count > 0)
                                                    {

                                                        txt_Prdoductiondate.Text = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                                    }


                                                }


                                            }




                                        }





                                    }





                                }

                                else
                                {


                                    if (Previous_Holiday == 0 && Current_Holiday == 0)
                                    {

                                        //This IS Current Day is Not holiday and Previous day is not Holiday Then

                                        //This is from Tuesday-Friday
                                        if (Day == 3 || Day == 4 || Day == 5 || Day == 6)
                                        {

                                            //Check Hours

                                            //For Day Shift
                                            if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16 || Hour == 17 || Hour == 18)
                                            {

                                                Hashtable htget_Day_prod_date1 = new Hashtable();
                                                DataTable dtget_Day_prod_date1 = new DataTable();

                                                htget_Day_prod_date1.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY");
                                                dtget_Day_prod_date1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date1);

                                                if (dtget_Day_prod_date1.Rows.Count > 0)
                                                {

                                                    txt_Prdoductiondate.Text = dtget_Day_prod_date1.Rows[0]["Production_Date"].ToString();
                                                }







                                            }
                                        }





                                    }
                                    else
                                    {

                                        //If not Prvious Day Holiday Then Prd.date=Prv.Day
                                        if (Prv_day == 3 || Prv_day == 4 || Prv_day == 5 || Prv_day == 6)
                                        {
                                            Hashtable htget_Day_prod_date1 = new Hashtable();
                                            DataTable dtget_Day_prod_date1 = new DataTable();

                                            htget_Day_prod_date1.Add("@Trans", "GET_DAY_SHIFT_PRV_DAY");
                                            dtget_Day_prod_date1 = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date1);

                                            if (dtget_Day_prod_date1.Rows.Count > 0)
                                            {

                                                txt_Prdoductiondate.Text = dtget_Day_prod_date1.Rows[0]["Production_Date"].ToString();
                                            }


                                        }
                                        else if (Prv_day == 2)
                                        {



                                            //For Day Shift
                                            if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16 || Hour == 17 || Hour == 18)
                                            {

                                                //Gettting Friday Day if the day is monday


                                                htget_Day_prod_date.Clear();
                                                dtget_Day_Prod_Date.Clear();
                                                htget_day.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                                dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                                if (dtget_Day_Prod_Date.Rows.Count > 0)
                                                {

                                                    txt_Prdoductiondate.Text = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                                }




                                            }



                                        }
                                    }




                                }






                            }


                        }









                    }
                    //This is For Night Shift
                    else if (Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
                    {




                        Hashtable htcheck_Holiday = new Hashtable();
                        DataTable dtcheck_Holiday = new DataTable();
                        htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_CURRENT_DATE_FOR_NIGHT_SHIFT");
                        dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);
                        Current_Holiday = 0;
                        if (dtcheck_Holiday.Rows.Count > 0)
                        {


                            Hashtable htget_Current_day = new Hashtable();
                            DataTable dtget_Current_Day = new DataTable();
                            htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                            dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                            if (dtget_Current_Day.Rows.Count > 0)
                            {


                                txt_Prdoductiondate.Text = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                            }


                        }
                        else
                        {

                            Hashtable htget_Current_day = new Hashtable();
                            DataTable dtget_Current_Day = new DataTable();
                            htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                            dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                            if (dtget_Current_Day.Rows.Count > 0)
                            {


                                txt_Prdoductiondate.Text = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                            }

                        }






                    }

                    //Ho

                    //else 
                    //if()
                    //{


                    //}

                }
                //For Monday Day Shift
                else if (Day == 2)
                {

                    //Check Hours

                    //For Day Shift
                    if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16 || Hour == 17 || Hour == 18)
                    {
                        Hashtable htget_Day_prod_date = new Hashtable();
                        DataTable dtget_Day_Prod_Date = new DataTable();


                        Hashtable htcheck_Holiday = new Hashtable();
                        DataTable dtcheck_Holiday = new DataTable();
                        //Gettting Friday Day if the day is monday

                        htget_Day_prod_date.Clear();
                        dtget_Day_Prod_Date.Clear();
                        htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                        dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                        if (dtget_Day_Prod_Date.Rows.Count > 0)
                        {
                            //Check The Friday Is Holiday Or Not

                            htcheck_Holiday.Clear();
                            htcheck_Holiday.Add("@Trans", "GET_HOLIDAY_BY_DATE");
                            htcheck_Holiday.Add("@Date", dtget_Day_Prod_Date.Rows[0]["Production_Date"]);
                            dtcheck_Holiday = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htcheck_Holiday);

                            if (dtcheck_Holiday.Rows.Count > 0)
                            {


                                htget_Day_prod_date.Clear();
                                dtget_Day_Prod_Date.Clear();
                                htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_LEAVE_ON_MONDAY");
                                dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                if (dtget_Day_Prod_Date.Rows.Count > 0)
                                {

                                    txt_Prdoductiondate.Text = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                }



                            }

                            else
                            {


                                htget_Day_prod_date.Clear();
                                dtget_Day_Prod_Date.Clear();
                                htget_Day_prod_date.Add("@Trans", "GET_FRIDAY_DATE_FOR_MONDAY_DAY_SHIFT");
                                dtget_Day_Prod_Date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Day_prod_date);

                                if (dtget_Day_Prod_Date.Rows.Count > 0)
                                {

                                    txt_Prdoductiondate.Text = dtget_Day_Prod_Date.Rows[0]["Production_Date"].ToString();
                                }


                            }


                        }




                    }
                    //This is For Night Shift
                    else if (Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
                    {
                        Hashtable htget_Current_day = new Hashtable();
                        DataTable dtget_Current_Day = new DataTable();
                        htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                        dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                        if (dtget_Current_Day.Rows.Count > 0)
                        {


                            txt_Prdoductiondate.Text = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                        }


                    }

                }
                //For Sat-Sunday Day Shift
                else if (Day == 7 || Day == 1)
                {

                    //Check Hours

                    //For Day Shift
                    if (Hour == 7 || Hour == 8 || Hour == 9 || Hour == 10 || Hour == 11 || Hour == 12 || Hour == 13 || Hour == 14 || Hour == 15 || Hour == 16 || Hour == 17 || Hour == 18)
                    {

                        //Prod.Date=Current.Date
                        Hashtable htget_Current_day = new Hashtable();
                        DataTable dtget_Current_Day = new DataTable();
                        htget_Current_day.Add("@Trans", "GET_CURRENT_DAY");
                        dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                        if (dtget_Current_Day.Rows.Count > 0)
                        {
                            Current_Holiday = 1;

                            txt_Prdoductiondate.Text = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                        }

                    }

                    if (Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
                    {
                        Hashtable htget_Current_day = new Hashtable();
                        DataTable dtget_Current_Day = new DataTable();
                        htget_Current_day.Add("@Trans", "GET_NIGHT_SHIFT_DATE");
                        dtget_Current_Day = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htget_Current_day);

                        if (dtget_Current_Day.Rows.Count > 0)
                        {


                            txt_Prdoductiondate.Text = dtget_Current_Day.Rows[0]["Production_Date"].ToString();
                        }


                    }


                }





            }






        }


        // This Area Belong to Emolyee Individual Order Effecincy
        private void Get_Employee_Details()
        {
            Hashtable htget_empdet = new Hashtable();
            DataTable dtget_empdet = new DataTable();

            htget_empdet.Add("@Trans", "GET_EMP_DETAILS");
            htget_empdet.Add("@User_Id", userid);
            dtget_empdet = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_empdet);
            if (dtget_empdet.Rows.Count > 0)
            {
                if (dtget_empdet.Rows[0]["Job_Role_Salary_Category"].ToString() != "" && dtget_empdet.Rows[0]["Job_Role_Salary_Category"].ToString() != null)
                {
                    Emp_Job_role_Id = int.Parse(dtget_empdet.Rows[0]["Job_Role_Salary_Category"].ToString());
                    Emp_Sal = decimal.Parse(dtget_empdet.Rows[0]["Salary"].ToString());
                }
                else
                {

                    Emp_Job_role_Id = 0;
                    Emp_Sal = 0;
                }

            }
            else
            {

                Emp_Job_role_Id = 0;
                Emp_Sal = 0;
            }



        }

        private void Get_Effecncy_Category()
        {
            if (Emp_Job_role_Id != 0 && Emp_Sal != 0)
            {

                Hashtable htget_Category = new Hashtable();
                DataTable dtget_Category = new DataTable();
                if (Emp_Job_role_Id == 1)
                {
                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_SEARCHER");
                }
                else if (Emp_Job_role_Id == 2)
                {

                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_TYPER");
                }
                htget_Category.Add("@Salary", Emp_Sal);
                htget_Category.Add("@Job_Role_Id", Emp_Job_role_Id);

                dtget_Category = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Category);


                if (dtget_Category.Rows.Count > 0)
                {
                    Emp_Sal_Cat_Id = int.Parse(dtget_Category.Rows[0]["Category_ID"].ToString());
                    Emp_cat_Value = decimal.Parse(dtget_Category.Rows[0]["Category_Name"].ToString());
                }
                else
                {

                    Emp_Sal_Cat_Id = 0;
                    Emp_cat_Value = 0;
                }

            }
            else
            {

                MessageBox.Show("Please Setup Employee job Role");


            }



        }



        //Get the Ordertyap_Abs_Id

        //private void Get_Order_Type_Abs()
        //{

        //    Hashtable htget_Orde_Type_Abs_Id = new Hashtable();
        //    DataTable dtget_Order_Type_Abs_Id = new DataTable();

        //    htget_Orde_Type_Abs_Id.Add("@Trans", "SELECT_BY_ORDER_TYPE_ID");
        //    htget_Orde_Type_Abs_Id.Add("@Order_Type_ID",);

        //}

        //get the ordertyap_abs_id

        private void Get_Order_Source_Type_For_Effeciency()
        {

            // Check for the Search Task

            //Check its Plant  or Technical For Searcher

            if (Eff_Order_Task_Id == 2 || Eff_Order_Task_Id == 3)
            {
                Hashtable htcheckplant_Technical = new Hashtable();
                DataTable dtcheckplant_Technical = new DataTable();
                htcheckplant_Technical.Add("@Trans", "GET_ORDER_SOURCE_TYPE_ID");
                htcheckplant_Technical.Add("@State_Id", Eff_State_Id);
                htcheckplant_Technical.Add("@County", Eff_County_Id);
                dtcheckplant_Technical = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheckplant_Technical);

                if (dtcheckplant_Technical.Rows.Count > 0)
                {

                    Eff_Order_Source_Type_Id = int.Parse(dtcheckplant_Technical.Rows[0]["Order_Source_Type_ID"].ToString());

                }
                else
                {
                    Eff_Order_Source_Type_Id = 0;

                }

                // If its an Technical or Plant

                if (Eff_Order_Source_Type_Id != 0)
                {
                    //Get the Allocated Count in the Efffecincy Matrix
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                    }
                    else
                    {

                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                        }
                        else
                        {

                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                    }
                    else
                    {

                        Emp_Eff_Allocated_Order_Count = 0;
                    }



                }
                else if (Emp_Eff_Allocated_Order_Count != 0 && Eff_Order_Source_Type_Id != 0)
                {
                    //Get the Allocated Count in the Efffecincy Matrix for Online
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                    }
                    else
                    {



                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                        }
                        else
                        {

                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                    }
                    else
                    {

                        Emp_Eff_Allocated_Order_Count = 0;
                    }



                }
                else
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {

                        Emp_Eff_Allocated_Order_Count = 0;
                        Eff_Order_User_Effecncy = 0;
                    }

                }








            }
            else if (Eff_Order_Task_Id == 4 || Eff_Order_Task_Id == 7)
            {

                // this is for Deed Chain Order and Typing 


                Hashtable htcheck_Deed_Chain = new Hashtable();
                DataTable dtcheck_Deed_Chain = new DataTable();
                htcheck_Deed_Chain.Add("@Trans", "GET_ORDER_SOURCE_TYPE_ID_BY_SUB_CLIENT");
                htcheck_Deed_Chain.Add("@Subprocess_Id", Eff_Sub_Process_Id);
                dtcheck_Deed_Chain = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheck_Deed_Chain);

                if (dtcheck_Deed_Chain.Rows.Count > 0)
                {

                    Eff_Order_Source_Type_Id = int.Parse(dtcheck_Deed_Chain.Rows[0]["Order_Source_Type_ID"].ToString());

                }
                else
                {
                    Eff_Order_Source_Type_Id = 0;

                }

                if (Eff_Order_Source_Type_Id != 0)
                {

                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    }
                    else
                    {


                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;


                        }
                        else
                        {

                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;


                    }
                    else
                    {

                        Emp_Eff_Allocated_Order_Count = 0;
                    }


                }
                else if (Eff_Order_Source_Type_Id != 0 && Emp_Eff_Allocated_Order_Count != 0)
                {

                    //Get the Allocated Count in the Efffecincy Matrix for Online
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    }
                    else
                    {

                        Emp_Eff_Allocated_Order_Count = 0;
                    }

                    Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;



                }

                else
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    DataTable dtget_Effeciency_Value = new DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {

                        Emp_Eff_Allocated_Order_Count = 0;
                        Eff_Order_User_Effecncy = 0;
                    }

                }





            }
            else  // this is for not Search and Typing Qc
            {


                Hashtable htget_Effecicy_Value = new Hashtable();
                DataTable dtget_Effeciency_Value = new DataTable();

                htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                if (dtget_Effeciency_Value.Rows.Count > 0)
                {
                    Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                }
                else
                {

                    Emp_Eff_Allocated_Order_Count = 0;
                    Eff_Order_User_Effecncy = 0;
                }




            }




        }





        protected void Bind_Order_IssueDetails()
        {
            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT_BY_ORDER_TASK_USER");
            ht_Select_Order_Details.Add("@Order_Id", Order_Id);
            ht_Select_Order_Details.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
            ht_Select_Order_Details.Add("@User_Id", userid);
            ht_Select_Order_Details.Add("@Work_Type_Id", Work_Type_Id);
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order_Issue_Details", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                ddl_Issue_Category.SelectedValue = dt_Select_Order_Details.Rows[0]["Issue_Id"].ToString();

                txt_Delay_Text.Text = dt_Select_Order_Details.Rows[0]["Reason"].ToString();
            }
            else
            {

                txt_Delay_Text.Text = "";
                ddl_Issue_Category.SelectedIndex = 0;

            }



        }

        protected void Get_Order_Search_Cost_Details()
        {
            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT");
            ht_Select_Order_Details.Add("@Order_Id", Order_Id);

            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                ddl_Order_Source.Text = dt_Select_Order_Details.Rows[0]["Source"].ToString();


                txt_Order_Abstractor_Cost.Text = dt_Select_Order_Details.Rows[0]["Abstractor_Cost"].ToString();
                txt_Order_No_Of_Pages.Text = dt_Select_Order_Details.Rows[0]["No_Of_pages"].ToString();
                if (dt_Select_Order_Details.Rows[0]["Search_Cost"].ToString() == "0.00")
                {
                    txt_Order_Search_Cost.Text = "";
                }
                else
                {
                    txt_Order_Search_Cost.Text = dt_Select_Order_Details.Rows[0]["Search_Cost"].ToString();
                }
                if (dt_Select_Order_Details.Rows[0]["Copy_Cost"].ToString() == "0.00")
                {
                    txt_Order_Copy_Cost.Text = "";
                }
                else
                {
                    txt_Order_Copy_Cost.Text = dt_Select_Order_Details.Rows[0]["Copy_Cost"].ToString();
                }
                if (dt_Select_Order_Details.Rows[0]["User_Password_Id"] != DBNull.Value || dt_Select_Order_Details.Rows[0]["User_Password_Id"].ToString() != "")
                {
                    ddl_Web_search_sites.SelectedValue = dt_Select_Order_Details.Rows[0]["User_Password_Id"].ToString();

                    if (dt_Select_Order_Details.Rows[0]["User_Password_Id"].ToString() == "43")
                    {
                        txt_Website.Text = dt_Select_Order_Details.Rows[0]["Website_Name"].ToString();

                        txt_Website.Visible = true;
                        lbl_Enter_Website.Visible = true;
                    }
                    else
                    {
                        txt_Website.Visible = false;
                        lbl_Enter_Website.Visible = false;


                    }
                }
                if (dt_Select_Order_Details.Rows[0]["No_of_Hits"] != DBNull.Value || dt_Select_Order_Details.Rows[0]["No_of_Hits"].ToString() != "")
                {
                    lbl_No_Of_hits.Visible = true;
                    txt_No_Of_Hits.Visible = true;
                    txt_No_Of_Hits.Text = dt_Select_Order_Details.Rows[0]["No_of_Hits"].ToString();
                }
                if (dt_Select_Order_Details.Rows[0]["No_Of_Documents"] != DBNull.Value || dt_Select_Order_Details.Rows[0]["No_Of_Documents"].ToString() != "")
                {
                    lbl_No_of_Documents.Visible = true;
                    txt_No_of_documents.Visible = true;
                    txt_No_of_documents.Text = dt_Select_Order_Details.Rows[0]["No_Of_Documents"].ToString();
                }

            }
        }

        protected void Get_Order_Details()
        {

            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_NO_WISE_FOR_EMPLOYEE_ORDER_ENTRY");
            ht_Select_Order_Details.Add("@Order_ID", Selected_Order_Id);
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                // Order_Id = Order_Id;
                // Order_Id = Order_Id;
                Client = dt_Select_Order_Details.Rows[0]["Client_Name"].ToString();
                Subclient = dt_Select_Order_Details.Rows[0]["Sub_ProcessName"].ToString();
                txt_Subprocess.Text = dt_Select_Order_Details.Rows[0]["Subprocess_Number"].ToString();
                lbl_Order_Number.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                lbl_customer_No.Text = dt_Select_Order_Details.Rows[0]["Client_Number"].ToString();
                lbl_Order_Type.Text = dt_Select_Order_Details.Rows[0]["Order_Type"].ToString();
                Order_Type_Id = int.Parse(dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString());
                lbl_Property_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                State_Id = int.Parse(dt_Select_Order_Details.Rows[0]["stateid"].ToString());
                County_Id = int.Parse(dt_Select_Order_Details.Rows[0]["CountyId"].ToString());
                lbl_State.Text = dt_Select_Order_Details.Rows[0]["State"].ToString();
                lbl_County.Text = dt_Select_Order_Details.Rows[0]["County"].ToString();
                txt_City.Text = dt_Select_Order_Details.Rows[0]["City"].ToString();

                txt_Zipcode.Text = dt_Select_Order_Details.Rows[0]["Zip"].ToString();
                Client_id = int.Parse(dt_Select_Order_Details.Rows[0]["Client_Id"].ToString());
                lbl_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                lbl_Order_Refno.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Ref"].ToString();
                lbl_Barrower_Name.Text = dt_Select_Order_Details.Rows[0]["Borrower_Name"].ToString();
                lbl_Notes.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();
                Efftectiv_date = dt_Select_Order_Details.Rows[0]["Effective_date"].ToString();
                Order_Type_ABS_id = int.Parse(dt_Select_Order_Details.Rows[0]["OrderType_ABS_Id"].ToString());
                if (Efftectiv_date != "")
                {
                    txt_Effectivedate.Text = Efftectiv_date.ToString();
                }
                Sub_ProcessId = int.Parse(dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString());
                Client_Name = dt_Select_Order_Details.Rows[0]["Client_Name"].ToString();
                Sub_ProcessName = dt_Select_Order_Details.Rows[0]["Subprocess_Number"].ToString();
                txt_ReceivedDate.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();
            }
        }

        protected void Get_Order_Production_Date_Details()
        {
            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT");
            ht_Select_Order_Details.Add("@Order_Id", Order_Id);
            ht_Select_Order_Details.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                //txt_Prdoductiondate.Text = dt_Select_Order_Details.Rows[0]["Order_Production_Date"].ToString();

                txt_Prdoductiondate.Text = "";
            }
            else
            {
                txt_Prdoductiondate.Text = "";
            }
        }


        protected void Get_User_Track_Details()
        {

            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "GET_TASK_USER");
            ht_Select_Order_Details.Add("@Client_Order_Number", SESSION_ORDER_NO.ToString());
            ht_Select_Order_Details.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString()));
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                string UserName = dt_Select_Order_Details.Rows[0]["User_Name"].ToString();
                string Task = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                string TaskProgress = dt_Select_Order_Details.Rows[0]["Progress_Status"].ToString();

                string Message = "User " + UserName + " Has Selected " + Task + " and it is " + TaskProgress + " Do You Want to Proceed?";

                //   ViewState["Message"] = Message.ToString();

            }
            else
            {

                //   ViewState["Message"] = "User Has Canceled Do You Want to Proceed?";
            }



        }

        private void Error_Cbo_Load()
        {
            DataGridViewComboBoxColumn ddl_Error_Type = new DataGridViewComboBoxColumn();
            //grd_Error.DataSource = null;
            //grd_Error.AutoGenerateColumns = false;
            //grd_Error.ColumnCount = 2;

            //grd_Error.Columns[0].Name = "SNo";
            //grd_Error.Columns[0].HeaderText = "S. No";
            //grd_Error.Columns[0].Width = 65;


            //grd_Error.Columns[1].Name = "Comments";
            //grd_Error.Columns[1].HeaderText = "Comments";
            //grd_Error.Columns[1].DataPropertyName = "Error_Description";
            //grd_Error.Columns[1].Width = 200;


            //ddl_Error_Type.HeaderText = "Error Category";
            //ddl_Error_Type.Name = "ddl_Error_Type";
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Error_Type");
            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[0] = "Select";
            dtselect.Rows.InsertAt(dr, 0);
            //cbo_ErrorCatogery.DataSource = dtselect;
            //cbo_ErrorCatogery.ValueMember = "Error_Type_Id";
            //cbo_ErrorCatogery.DisplayMember = "Error_Type";
            // grd_Error.Columns.Add(ddl_Error_Type);

            //grd_Error.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(grd_Error_EditingControlShowing);

            //grd_Error.Columns.Add(ddl_Error_description);

        }

        protected void Geydview_Bind_Notes()
        {

            //Hashtable htNotes = new Hashtable();
            //DataTable dtNotes = new System.Data.DataTable();

            //htNotes.Add("@Trans", "SELECT");
            //htNotes.Add("@Order_Id", Order_Id);
            //dtNotes = dataaccess.ExecuteSP("Sp_Order_Notes", htNotes);
            //if (dtNotes.Rows.Count > 0)
            //{
            //    //ex2.Visible = true;
            //    grd_Error.Visible = true;
            //    grd_Error.DataSource = dtNotes;
            //}
            //else
            //{
            //}


        }

        private void Employee_Order_Entry_Load(object sender, EventArgs e)
        {
            if (SESSION_ORDER_TASK == "12" || SESSION_ORDER_TASK == "22")
            {
                btn_submit.Enabled = true;
                btn_Checklist.Enabled = false;
            }
            else
            {
                btn_submit.Enabled = false;
                btn_Checklist.Enabled = true;
            }

            if (SESSION_ORDER_TASK == "3" || SESSION_ORDER_TASK == "4" || SESSION_ORDER_TASK == "7" || SESSION_ORDER_TASK == "23" || SESSION_ORDER_TASK == "12" || SESSION_ORDER_TASK == "24")
            {
                btn_ErrorEntry.Visible = true;
            }
            else
            {
                btn_ErrorEntry.Visible = false;
            }

            Today_Date = DateTime.Now;
            txt_Prdoductiondate.Value = DateTime.Now;

            txt_Effectivedate.Focus();
            txt_Effectivedate.Text = "";
            txt_Website.Visible = false;
            lbl_Enter_Website.Visible = false;
            if (SESSION_ORDER_TASK == "4" || SESSION_ORDER_TASK == "7")
            {
                txt_Effectivedate.Enabled = true;
                ddl_Order_Source.Enabled = false;
                txt_Order_Search_Cost.Enabled = false;
                txt_Order_Copy_Cost.Enabled = false;
                txt_Order_Abstractor_Cost.Enabled = false;
                txt_Order_No_Of_Pages.Enabled = true;

            }
            if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
            {
                Btn_Marker_Maker.Enabled = true;
                btn_OrderSearhcerNotes.Enabled = true;
            }
            else
            {
                btn_OrderSearhcerNotes.Enabled = false;

                Btn_Marker_Maker.Enabled = true;
            }

            dbc.BindWebsiteNames(ddl_Web_search_sites);
            dbc.Bind_Issue_Type(ddl_Issue_Category);
            ddl_Order_Source.SelectedIndex = -1;
            dbc.Bind_Tax_Internal_Status(ddl_Tax_Task);
            Get_Order_Details();
            Bind_Order_IssueDetails();
            Geydview_Bind_Comments();
            Get_Order_Search_Cost_Details();
            Error_Cbo_Load();


            if (Sub_ProcessId == 330 || Sub_ProcessId == 395)// this is for Title Exam Orders-->Client_Id=40 & Client Id =51
            {

                btn_ErrorEntry.Visible = true;
            }

            if (Sub_ProcessId == 330 && Client_id == 40)// this is for Title Exam Orders-->Client_Id=40 
            {
                btn_Searcher_Link.Enabled = false;
                btn_OrderSearhcerNotes.Enabled = false;

            }





            string no_of_pages = txt_Order_No_Of_Pages.Text;
            if (no_of_pages == "0")
            {

                txt_Order_No_Of_Pages.Text = "";
            }
            //Order submission Changes

            if (ddl_order_Task.Visible == false)
            {
                txt_Task.Visible = true;

            }
            else
            {
                txt_Task.Visible = false;
            }


            if (Work_Type_Id == 3)
            {
                lbl_Next_Task.Visible = false;
                ddl_order_Task.Visible = false;
            }
            else
            {
                lbl_Next_Task.Visible = true;
                ddl_order_Task.Visible = true;
            }

            if (Work_Type_Id == 1)
            {

                btn_Send_Tax_Request.Visible = true;
                //btn_Cancel_Tax_Request.Visible = true;

                ddl_Order_Source.Enabled = true;
                ddl_Web_search_sites.Enabled = true;
                txt_Order_Search_Cost.Enabled = true;
                txt_Order_Copy_Cost.Enabled = true;
                txt_Order_Abstractor_Cost.Enabled = true;
                btn_Send_Tax_Request.Visible = true;
                //  btn_Cancel_Tax_Request.Visible = true;
                if (lbl_Order_Task_Type.Text == "Typing" || lbl_Order_Task_Type.Text == "Typing QC")
                {
                    lbl_webSearch.Visible = true;
                    lbl_webSearch.Enabled = false;
                    ddl_Web_search_sites.Visible = true;
                    ddl_Web_search_sites.Enabled = false;
                    txt_Website.Enabled = false;
                }

            }
            else
            {

                ddl_Order_Source.Enabled = false;
                ddl_Web_search_sites.Enabled = false;
                txt_Order_Search_Cost.Enabled = false;
                txt_Order_Copy_Cost.Enabled = false;
                txt_Order_Abstractor_Cost.Enabled = false;
                txt_Website.Enabled = true;
                btn_Send_Tax_Request.Visible = false;
                btn_Cancel_Tax_Request.Visible = false;

            }

            if (ddl_Issue_Category.SelectedIndex > 0)
            {

                txt_Delay_Text.Enabled = true;
            }
            else
            {
                txt_Delay_Text.Enabled = false;

            }




            Check_Tax_Request();



            if (Tax_Completed == 1)
            {
                btn_Cancel_Tax_Request.Visible = false;
                btn_Send_Tax_Request.Visible = false;
                /// MessageBox.Show("Tax Certificate Received kindly check in Upload Document - Tax Tab ");
            }


            Populate_Production_Date();

            // This is for Employee Effecincy Calculate Purpose

            Eff_Client_Id = Client_id;
            Eff_Order_Task_Id = int.Parse(SESSION_ORDER_TASK);

            Eff_Order_Type_Abs_Id = Order_Type_ABS_id;
            Eff_Sub_Process_Id = Sub_ProcessId;
            Eff_State_Id = State_Id;
            Eff_County_Id = County_Id;

            Get_Employee_Details();
            Get_Effecncy_Category();
            Get_Order_Source_Type_For_Effeciency();
            this.WindowState = FormWindowState.Maximized;


            // this for Titlogy Vendor Db Tilte Invoice Purpose
            if (roleid == "1" || roleid == "6")
            {

                btn_Genrate_Invoice.Visible = true;
            }
            else
            {
                btn_Genrate_Invoice.Visible = false;

            }

            // this is for Titlelogy Invocie Related

            if (Client_id == 33 && Sub_ProcessId == 300)
            {

                if (Order_Type_Id == 113 || Order_Type_Id == 116 || Order_Type_Id == 117 || Order_Type_Id == 119 || Order_Type_Id == 115 || Order_Type_Id == 114)
                {




                    if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3" || SESSION_ORDER_TASK == "4" || SESSION_ORDER_TASK == "7" || SESSION_ORDER_TASK == "23" || SESSION_ORDER_TASK == "24")
                    {

                        chk_Plat_Yes.Enabled = true;
                        chk_plat_No.Enabled = true;
                        chk_Tax_Yes.Enabled = true;
                        chk_Tax_No.Enabled = true;


                    }




                    Load_Titlelogy_Invoice_Pages_and_Price_Details();
                }
                else
                {


                    grp_Titlelogy_Invoice.Visible = false;
                }
            }

            else
            {


                grp_Titlelogy_Invoice.Visible = false;

            }

            if (Client_id == 33 && Sub_ProcessId == 300)
            {

                if (Order_Type_Id == 113 || Order_Type_Id == 115 || Order_Type_Id == 117)
                {
                    if (SESSION_ORDER_TASK == "23" || SESSION_ORDER_TASK == "24")
                    {

                        lbl_Title_no_of_pages.Visible = true;
                        txt_Invoice_No_Of_Pages.Visible = true;
                        lbl_Title1.Visible = true;


                        lbl_Probate.Visible = true;
                        txt_Probate_Pages.Visible = true;
                        lbl_Title2.Visible = true;


                        lbl_Platmap.Visible = true;
                        txt_Platmap_Pages.Visible = true;
                        lbl_Title3.Visible = true;







                    }
                }
            }


            // For Title Exam Need to  disable Search link button
            if (Order_Type_Id == 93 || Order_Type_Id == 138)
            {
                //btn_Searcher_Link.Visible = false;
                btn_Searcher_Link.Enabled = false;
            }
            else
            {

                btn_Searcher_Link.Visible = true;
            }



            if (Work_Type_Id == 1)
            {

                Pass_Max_Time_Id = Max_Time_Id;
            }
            else if (Work_Type_Id == 2)
            {

                Get_Rework_maximum_Time_Id();
                Pass_Max_Time_Id = MAX_TIME_ID;
            }
            else if (Work_Type_Id == 3)
            {

                Get_Super_Qc_maximum_Time_Id();
                Pass_Max_Time_Id = MAX_TIME_ID;
            }


            Enable_Tax_Client_Wise_Task_Wise();

            Enabled = false;
        }

        private void Check_Tax_Request()
        {


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_INTERNALTAX_STATUS");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
            int check = 0;
            if (dtcheck.Rows.Count > 0)
            {

                check = int.Parse(dtcheck.Rows[0]["Search_Tax_Request"].ToString());


            }
            else
            {
                check = 0;
            }

            if (check != 2)
            {

                btn_Send_Tax_Request.Visible = true;
                //  btn_Cancel_Tax_Request.Visible = false;

            }

            else
            {

                //  btn_Cancel_Tax_Request.Visible = true;
                btn_Send_Tax_Request.Visible = false;
            }

            if (check == 2)
            {

                Internal_Tax_Check = 1;
            }
            else
            {
                Internal_Tax_Check = 0;

            }


        }


        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, SESSION_ORDER_NO, Client_id.ToString(), Sub_ProcessId.ToString());
            Orderuploads.Show();
        }



        private void btn_submit_Click(object sender, EventArgs e)
        {
          

            if (Work_Type_Id == 1)
            {
                btn_Submit_Clicked = true;
                int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString());
                if (Order_Task == 27 || Order_Task == 28 || Order_Task == 29)
                {

                    Submit_Live_Data_For_Image_req_Tax_Req_DataDepth_req();
                }
                else
                {
                    Submit_Live_data();
                }

            }
            else if (Work_Type_Id == 2)
            {

                Submit_Rework_data();
            }
            else if (Work_Type_Id == 3)
            {

                Submit_Super_Qc_data();
            }
            if (InvokeRequired)
            {

                this.Invoke(new MethodInvoker(delegate
                {

                    foreach (Form f in Application.OpenForms)
                    {

                        if (f.Name == "Judgement_Period_Create_View")
                        {
                            IsOpen_jud = true;
                            f.Close();
                            break;
                        }

                    }
                    foreach (Form f1 in Application.OpenForms)
                    {
                        if (f1.Name == "State_Wise_Tax_Due_Date")
                        {
                            IsOpen_state = true;
                            f1.Close();
                            break;
                        }
                    }
                    foreach (Form f2 in Application.OpenForms)
                    {
                        if (f2.Name == "Employee_Order_Information")
                        {
                            IsOpen_emp = true;
                            f2.Close();
                            break;
                        }
                    }
                    foreach (Form f3 in Application.OpenForms)
                    {
                        if (f3.Name == "Order_Template_View")
                        {
                            IsOpen_us = true;
                            f3.Close();
                            break;
                        }
                    }

                    foreach (Form f4 in Application.OpenForms)
                    {
                        if (f4.Name == "Employee_Alert_Message")
                        {
                            IsOpen_us = true;
                            f4.Close();
                            break;
                        }
                    }
                    foreach (Form f5 in Application.OpenForms)
                    {
                        if (f5.Name == "Order_Uploads")
                        {
                            IsOpen_us = true;
                            f5.Close();
                            break;
                        }
                    }

                }));
            }
            else
            {

                foreach (Form f in Application.OpenForms)
                {

                    if (f.Name == "Judgement_Period_Create_View")
                    {
                        IsOpen_jud = true;
                        f.Close();
                        break;
                    }

                }
                foreach (Form f1 in Application.OpenForms)
                {
                    if (f1.Name == "State_Wise_Tax_Due_Date")
                    {
                        IsOpen_state = true;
                        f1.Close();
                        break;
                    }
                }
                foreach (Form f2 in Application.OpenForms)
                {
                    if (f2.Name == "Employee_Order_Information")
                    {
                        IsOpen_emp = true;
                        f2.Close();
                        break;
                    }
                }
                foreach (Form f3 in Application.OpenForms)
                {
                    if (f3.Name == "Order_Template_View")
                    {
                        IsOpen_us = true;
                        f3.Close();
                        break;
                    }
                }

                foreach (Form f4 in Application.OpenForms)
                {
                    if (f4.Name == "Employee_Alert_Message")
                    {
                        IsOpen_us = true;
                        f4.Close();
                        break;
                    }
                }
                foreach (Form f5 in Application.OpenForms)
                {
                    if (f5.Name == "Order_Uploads")
                    {
                        IsOpen_us = true;
                        f5.Close();
                        break;
                    }
                }

            }


        }


        private void Submit_Live_Data_For_Image_req_Tax_Req_DataDepth_req()
        {
            if (ddl_order_Staus.SelectedValue.ToString() == "3")
            {

                if (Validate_Order_Info() != false)
                {

                    SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                    try
                    {

                        if (Chk_Self_Allocate.Checked == false)
                        {


                            int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                            form_loader.Start_progres();


                            if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                            {



                                Hashtable htuser = new Hashtable();
                                DataTable dtuser = new System.Data.DataTable();
                                htuser.Add("@Trans", "SELECT_STATUSID");
                                htuser.Add("@Order_Status", ddl_order_Task.Text);
                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);




                                Hashtable htEffectivedate = new Hashtable();
                                DataTable dtEffectivdate = new System.Data.DataTable();
                                htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                                htEffectivedate.Add("@Order_ID", Order_Id);
                                htEffectivedate.Add("@Effective_date", txt_Effectivedate.Text);
                                htEffectivedate.Add("@Modified_By", userid);
                                dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);


                                Hashtable ht_Productiondate = new Hashtable();
                                DataTable dt_Production_date = new DataTable();

                                ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                ht_Productiondate.Add("@Order_ID", Order_Id);
                                ht_Productiondate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                                dt_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Productiondate);

                                if (dt_Production_date.Rows.Count > 0)
                                {

                                    Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                }
                                else
                                {

                                    Chk_Production_date = 0;
                                }

                                if (Chk_Production_date > 0)
                                {
                                    OPERATE_PRODUCTION_DATE = "UPDATE";
                                    Insert_ProductionDate();

                                }
                                else if (Chk_Production_date == 0)
                                {
                                    OPERATE_PRODUCTION_DATE = "INSERT";
                                    Insert_ProductionDate();
                                }

                                int Next_Order_Task = 0;

                                Hashtable ht_Status = new Hashtable();
                                DataTable dt_Status = new System.Data.DataTable();

                                Hashtable htupdate = new Hashtable();
                                DataTable dtupdate = new System.Data.DataTable();


                                ht_Status.Add("@Trans", "UPDATE_STATUS");
                                ht_Status.Add("@Order_ID", Order_Id);

                                if (ddl_order_Task.Visible != true)
                                {
                                    ht_Status.Add("@Order_Status", SESSION_ORDER_TASK.ToString());
                                    htupdate.Add("@Order_Progress", int.Parse(ddl_order_Staus.SelectedValue.ToString()));

                                }
                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                {
                                    htuser.Clear();
                                    dtuser.Clear();
                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                    htuser.Add("@Order_Status", ddl_order_Task.Text);

                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);

                                    ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));

                                    htupdate.Add("@Order_Progress", 8);

                                }
                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                {
                                    htuser.Clear();
                                    dtuser.Clear();
                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                    htuser.Add("@Order_Status", ddl_order_Task.Text);
                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);

                                    Next_Order_Task = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                    ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                    
                                    htupdate.Add("@Order_Progress", 3);

                                }
                                ht_Status.Add("@Modified_By", userid);

                                dt_Status = dataaccess.ExecuteSP("Sp_Order", ht_Status);



                                htupdate.Add("@Trans", "UPDATE_PROGRESS");
                                htupdate.Add("@Order_ID", Order_Id);

                                htupdate.Add("@Modified_By", userid);

                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);


                                Update_User_Order_Time_Info_Status();


                                //OrderHistory
                                Hashtable ht_Order_History = new Hashtable();
                                DataTable dt_Order_History = new DataTable();
                                ht_Order_History.Add("@Trans", "INSERT");
                                ht_Order_History.Add("@Order_Id", Order_Id);
                                //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                ht_Order_History.Add("@Status_Id", Next_Order_Task);
                                ht_Order_History.Add("@Progress_Id", 8);
                                ht_Order_History.Add("@Work_Type", 1);
                                ht_Order_History.Add("@Assigned_By", userid);
                                ht_Order_History.Add("@Modification_Type", "Order Complete");
                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                SplashScreenManager.CloseForm(false);
                                MessageBox.Show("Order Submitted Sucessfully");

                                this.Close();


                            }

                        }

                    }
                    catch (Exception ex)
                    {

                        SplashScreenManager.CloseForm(false);
                    }

                    finally
                    {

                    }


                }
            }
        }

        public void Submit_Live_data()
        {
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABR");
            ht_BIND.Add("@Order_Type", lbl_Order_Type.Text);
            dt_BIND = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_BIND);
            if (dt_BIND.Rows.Count > 0)
            {
                Order_Type_ABS = dt_BIND.Rows[0]["Order_Type_Abrivation"].ToString();
            }
            Hashtable ht_task = new Hashtable();
            DataTable dt_task = new DataTable();
            ht_task.Add("@Trans", "SELECT_STATUSID");
            ht_task.Add("@Order_Status", lbl_Order_Task_Type.Text);
            dt_task = dataaccess.ExecuteSP("Sp_Order_Status", ht_task);
            if (dt_task.Rows.Count > 0)
            {
                Taskid = int.Parse(dt_task.Rows[0]["Order_Status_ID"].ToString());
            }


            ////Update Checklist
            //COUNT_NO_QUESTION_AVLIABLE

            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();
            htcount.Add("@Trans", "COUNT_NO_QUESTION_AVLIABLE");
            htcount.Add("@Order_Status", Taskid);
            if (lbl_Order_Task_Type.Text == "Search" || lbl_Order_Task_Type.Text == "Search QC")
            {
                htcount.Add("@Order_Type_ABS", Order_Type_ABS);
            }
            else
            {
                htcount.Add("@Order_Type_ABS", "COS");
            }
            dtcount = dataaccess.ExecuteSP("Sp_Check_List", htcount);
            if (dtcount.Rows.Count > 0)
            {
                AVAILABLE_COUNT = int.Parse(dtcount.Rows[0]["count"].ToString());
            }



            //COUNT_NO_QUESTION_USER_ENTERED


            //heare Checklist is not Requried for Exceprtion,Upload and Tax Orders 
            if (int.Parse(SESSION_ORDER_TASK.ToString()) != 12 && int.Parse(SESSION_ORDER_TASK.ToString()) != 22 && int.Parse(SESSION_ORDER_TASK.ToString()) != 24 && ddl_order_Staus.SelectedValue.ToString() == "3")
            {

                Hashtable htentercount = new Hashtable();
                DataTable dtentercount = new DataTable();
                htentercount.Add("@Trans", "COUNT_NO_QUESTION_USER_ENTERED");
                htentercount.Add("@Order_Task", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                htentercount.Add("@Order_Id", Order_Id);
                htentercount.Add("@User_id", userid);
                htentercount.Add("@Order_Type_Abs_Id", Order_Type_ABS_id);
                htentercount.Add("@Work_Type", Work_Type_Id);
                dtentercount = dataaccess.ExecuteSP("Sp_Checklist_Detail", htentercount);
                if (dtentercount.Rows.Count > 0)
                {
                    USERCOUNT = int.Parse(dtentercount.Rows[0]["count"].ToString());
                }
                else
                {
                    USERCOUNT = 0;
                }

                if (USERCOUNT == 0)
                {
                    MessageBox.Show("Checklist questions not entered");

                }
                else
                {

                    USERCOUNT = 1;
                }



            }
            else
            {

                USERCOUNT = 1;
            }

            if (USERCOUNT > 0)
            {

                int Next_Status = 0;
                int Prog = 0;
                string Prog_Val = "";
                if (ddl_order_Staus.Text != "Select")
                {
                    Prog = int.Parse(ddl_order_Staus.SelectedValue.ToString());
                    Prog_Val = ddl_order_Staus.Text;
                }


                Hashtable htdatalist = new Hashtable();
                DataTable dtdatalist = new DataTable();
                htdatalist.Add("@Trans", "CHECK_ORDER_WISE");
                htdatalist.Add("@Order_Status", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                htdatalist.Add("@Order_Id", Order_Id);
                htdatalist.Add("@Work_Type_Id", Work_Type_Id);
                dtdatalist = dataaccess.ExecuteSP("Sp_Order_Document_List", htdatalist);

                int checkdatalistcount = int.Parse(dtdatalist.Rows[0]["count"].ToString());



                if (ddl_order_Staus.SelectedValue.ToString() == "3")
                {




                    if (Chk == 0)
                    {
                        if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9" || ddl_order_Staus.SelectedValue.ToString() == "7")
                        {
                            //employee order entry form enabled false
                            this.Enabled = false;


                            Ordermanagement_01.Task_Conformation Taskconfomation = new Ordermanagement_01.Task_Conformation(userid, Order_Id, Order_Task, Order_Status_Id);
                            Taskconfomation.ShowDialog();
                            Chk = 1;
                            ddl_order_Task.Visible = false;


                        }
                    }
                    else if (SESSSION_ORDER_TYPE == "Search" && ddl_Order_Source.Text == "" && Chk != 1)
                    {
                        ddl_Order_Source.Focus();
                        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter Order Source')</script>", false);
                        MessageBox.Show("Enter Order Source");
                    }
                    else
                    {

                      
                            
                    

                        if (Validate_Order_Info() != false && Validate_Document_Check_Type(int.Parse(SESSION_ORDER_TASK),true) != false && Valid_date() != false && validate_subscription() != false && validate_subscription_Website() != false && Validate_Effective_Date() != false && Validate_Document_List() != false && Validate_Search_Cost() != false && Validate_Error_Entry() != false && Validate_Tax_Internal_Status() != false && Validate_Tax_Internal_Status_Client_Sub_Client_Wise() != false && Validate_Search_And_Search_Qc_Note() != false && Validate_Searcher_Link() != false && Validate_Email_Check() != false )
                        {
                            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                            try
                            {

                                if (Chk_Self_Allocate.Checked == false)
                                {


                                    int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                                    if (Order_Task == 2 || Order_Task == 3)
                                    {
                                        Hashtable ht_Select_Order_Details = new Hashtable();
                                        DataTable dt_Select_Order_Details = new DataTable();

                                        ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                        ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                        dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                        if (dt_Select_Order_Details.Rows.Count > 0)
                                        {

                                            Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());

                                        }
                                        else
                                        {

                                            Chk_Order_Search_Cost = 0;
                                        }

                                        if (Chk_Order_Search_Cost > 0)
                                        {
                                            OPERATE_SEARCH_COST = "UPDATE";
                                            Insert_Order_Search_Cost();

                                        }
                                        else if (Chk_Order_Search_Cost == 0)
                                        {
                                            OPERATE_SEARCH_COST = "INSERT";
                                            Insert_Order_Search_Cost();
                                        }
                                    }

                                    form_loader.Start_progres();

                                    if (txt_Effectivedate.Text != "")
                                    {

                                        if (txt_Prdoductiondate.Text != "" && Valid_date() != false)
                                        {



                                            //This is for non Tax Orders 22 indicates Tax Internal Orders
                                            DateTime date1 = DateTime.Now;
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            if (int.Parse(SESSION_ORDER_TASK.ToString()) != 22)
                                            {



                                                Hashtable htupdate = new Hashtable();
                                                DataTable dtupdate = new System.Data.DataTable();
                                                htupdate.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate.Add("@Order_ID", Order_Id);

                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    htupdate.Add("@Order_Status", SESSION_ORDER_TASK.ToString());
                                                    htupdate.Add("@Order_Progress", int.Parse(ddl_order_Staus.SelectedValue.ToString()));

                                                    //For Titlelogy Updaters
                                                    Title_Logy_Order_Task_Id = int.Parse(SESSION_ORDER_TASK.ToString());
                                                    Title_Logy_Order_Status_Id = int.Parse(ddl_order_Staus.SelectedValue.ToString());
                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                                {
                                                    Hashtable htuser = new Hashtable();
                                                    DataTable dtuser = new System.Data.DataTable();
                                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                                    htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);

                                                    htupdate.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                    htupdate.Add("@Order_Progress", 8);

                                                    //for Titlelogy============from Niranjan

                                                    if (Client_id != 33)// this is condition onluy for Db title
                                                    {
                                                        Title_Logy_Order_Task_Id = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                                    }
                                                    else

                                                    {
                                                        Title_Logy_Order_Task_Id = 2;
                                                    }
                                                    Title_Logy_Order_Status_Id = 14;



                                                    Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());


                                                    //Title Logy External Order Status

                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                                {

                                                    Hashtable htuser = new Hashtable();
                                                    DataTable dtuser = new System.Data.DataTable();
                                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                                    htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                    htupdate.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                    htupdate.Add("@Order_Progress", 3);
                                                    Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());


                                                    //for Titlelogy============from Niranjan

                                                    Title_Logy_Order_Task_Id = 15;
                                                    Title_Logy_Order_Status_Id = 3;



                                                }

                                                htupdate.Add("@Modified_By", userid);
                                                htupdate.Add("@Modified_Date", dateeval);
                                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);


                                            }

                                            //==================================External Client_Vendor_Orders(Titlelogy)=====================================================







                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                            {

                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());

                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());
                                                External_Client_Order_Number = dt_Order_InTitleLogy.Rows[0]["Order_Number"].ToString();

                                                Hashtable htcheckExternalProduction = new Hashtable();
                                                DataTable dtcheckExternalProduction = new DataTable();
                                                htcheckExternalProduction.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                htcheckExternalProduction.Add("@External_Order_Id", External_Client_Order_Id);
                                                htcheckExternalProduction.Add("@Order_Task", SESSION_ORDER_TASK);
                                                dtcheckExternalProduction = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htcheckExternalProduction);



                                                if (dtcheckExternalProduction.Rows.Count > 0)
                                                {


                                                    Check_External_Production = int.Parse(dtcheckExternalProduction.Rows[0]["count"].ToString());
                                                }
                                                else
                                                {

                                                    Check_External_Production = 0;
                                                }

                                                // this is commented for Not Using this concept on 04/04/2018 onwards
                                                //// this is for DB-Prak Title Client
                                                //if (Client_id == 33 && Sub_ProcessId == 300)
                                                //{
                                                //    if (validate_Titlelogy_Invoice() != false)
                                                //    {

                                                //        if (Order_Type_Id == 113 || Order_Type_Id == 115 || Order_Type_Id == 116 || Order_Type_Id == 117 || Order_Type_Id == 119 || Order_Type_Id == 121 || Order_Type_Id == 123 || Order_Type_Id == 130 || Order_Type_Id == 133 || Order_Type_Id == 134 || Order_Type_Id == 135 || Order_Type_Id == 136)
                                                //        {
                                                //            if (chk_Plat_Yes.Checked == true)
                                                //            {

                                                //                Chk_Plat_Map = true;
                                                //            }
                                                //            else if (chk_plat_No.Checked == true)
                                                //            {

                                                //                Chk_Plat_Map = false;
                                                //            }

                                                //            if (chk_Tax_Yes.Checked == true)
                                                //            {

                                                //                Chk_Tax_Information = true;
                                                //            }
                                                //            else if (chk_Tax_No.Checked == true)
                                                //            {

                                                //                Chk_Tax_Information = false;
                                                //            }


                                                //            Hashtable htchk = new Hashtable();
                                                //            DataTable dtchk = new DataTable();

                                                //            htchk.Add("@Trans", "CEHCK");
                                                //            htchk.Add("@Order_Id", External_Client_Order_Id);
                                                //            htchk.Add("@Order_Task", SESSION_ORDER_TASK);
                                                //            dtchk = dataaccess.ExecuteSP("Sp_External_Client_Orders_Invoice_Check_List_Details", htchk);
                                                //            int chk_Count = 0;
                                                //            if (dtchk.Rows.Count > 0)
                                                //            {
                                                //                chk_Count = int.Parse(dtchk.Rows[0]["count"].ToString());
                                                //            }
                                                //            else
                                                //            {
                                                //                chk_Count = 0;

                                                //            }

                                                //            if (chk_Count == 0)
                                                //            {
                                                //                Hashtable htin = new Hashtable();
                                                //                DataTable dtin = new DataTable();

                                                //                htin.Add("@Trans", "INSERT");
                                                //                htin.Add("@Order_Id", External_Client_Order_Id);
                                                //                htin.Add("@Order_Task", SESSION_ORDER_TASK);
                                                //                htin.Add("@Plat_Map", Chk_Plat_Map);
                                                //                htin.Add("@Tax_Information", Chk_Tax_Information);
                                                //                htin.Add("@User_Id", userid);
                                                //                dtin = dataaccess.ExecuteSP("Sp_External_Client_Orders_Invoice_Check_List_Details", htin);

                                                //            }
                                                //            else
                                                //            {
                                                //                Hashtable htin = new Hashtable();
                                                //                DataTable dtin = new DataTable();

                                                //                htin.Add("@Trans", "UPDATE");
                                                //                htin.Add("@Order_Id", External_Client_Order_Id);
                                                //                htin.Add("@Order_Task", SESSION_ORDER_TASK);
                                                //                htin.Add("@Plat_Map", Chk_Plat_Map);
                                                //                htin.Add("@Tax_Information", Chk_Tax_Information);
                                                //                htin.Add("@User_Id", userid);
                                                //                dtin = dataaccess.ExecuteSP("Sp_External_Client_Orders_Invoice_Check_List_Details", htin);


                                                //            }


                                                //            Genrate_Invoice_Titlelogy_Client_For_Db_Title();


                                                //        }

                                                //    }


                                                //}


                                                // Check Email Sending Option 

                                                Hashtable htcheck_Email_sending = new Hashtable();
                                                DataTable dtcheck_Email_Sending = new DataTable();

                                                htcheck_Email_sending.Add("@Trans", "GET_EMAIL_STATUS");
                                                htcheck_Email_sending.Add("@Subprocess_Id", Sub_ProcessId);
                                                dtcheck_Email_Sending = dataaccess.ExecuteSP("Sp_Client_SubProcess", htcheck_Email_sending);

                                                string Email_Sending_Status = "";
                                                if (dtcheck_Email_Sending.Rows.Count > 0)
                                                {
                                                    Email_Sending_Status = dtcheck_Email_Sending.Rows[0]["Email_Sending"].ToString();
                                                }
                                                else
                                                {
                                                    Email_Sending_Status = "False";

                                                }

                                                // This is For Check Invoice For Db Title Client
                                                if (Title_Logy_Order_Task_Id == 15 && Client_id == 33)
                                                {
                                                    if (Email_Sending_Status == "True") // 
                                                    {
                                                        if (Validate_Package_Uploaded() != false && Validate_Report_File() != false && Validate_Invoice_Genrated() != false && Validate_Invoice_Genrated_Document_Uploaded() != false && Validate_Email_Check() != false)
                                                        {
                                                            if (chk_Email_Yes.Checked == true)
                                                            {
                                                                Send_Completed_Order_Email();
                                                            }

                                                        }
                                                        else if (Invoice_Search_Packake_Order == 0)
                                                        {

                                                            return;
                                                        }


                                                    }



                                                }


                                                if (Title_Logy_Order_Task_Id == 15 && Client_id == 33)
                                                {
                                                    if (Email_Sending_Status == "True") // this is for 32013 Client Restrict To not to send email;
                                                    {
                                                        if (chk_Email_Yes.Checked == true)
                                                        {

                                                            if (validate_Email_Sent() == false)
                                                            {

                                                                MessageBox.Show("Email is Not Sent, Please Re Submit the Order to Complete");

                                                                return;
                                                            }
                                                        }
                                                    }

                                                }


                                                if (External_Client_Order_Task_Id == 18 && Title_Logy_Order_Task_Id == 15)
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();

                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Title_Logy_Order_Task_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", Title_Logy_Order_Status_Id);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);


                                                    if (Title_Logy_Order_Task_Id == 15)
                                                    {




                                                        date = DateTime.Now;


                                                        Hashtable htProductionDate = new Hashtable();
                                                        DataTable dtproductiondate = new System.Data.DataTable();
                                                        htProductionDate.Add("@Trans", "INSERT");
                                                        htProductionDate.Add("@External_Order_Id", External_Client_Order_Id);
                                                        htProductionDate.Add("@Order_Task", 15);
                                                        htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                                        htProductionDate.Add("@Order_Production_date", txt_Prdoductiondate.Text);
                                                        htProductionDate.Add("@Inserted_By", userid);
                                                        htProductionDate.Add("@Inserted_date", date);
                                                        htProductionDate.Add("@status", "True");
                                                        dtproductiondate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htProductionDate);




                                                        // Update Tax information Status and Plat Map Page status


                                                    }

                                                    if (Check_External_Production == 0)
                                                    {

                                                        OPERATE_PRODUCTION_DATE = "INSERT";
                                                        Insert_External_CLient_ProductionDate();


                                                    }
                                                    else if (Check_External_Production > 0)
                                                    {


                                                        OPERATE_PRODUCTION_DATE = "UPDATE";
                                                        Insert_External_CLient_ProductionDate();
                                                    }



                                                }


                                                else if (External_Client_Order_Task_Id != 18 && validate_Titlelogy_Invoice() != false)
                                                {


                                                    // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    if (Client_id != 33)
                                                    {
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Title_Logy_Order_Task_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", Title_Logy_Order_Status_Id);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                    }

                                                    if (Check_External_Production == 0)
                                                    {

                                                        OPERATE_PRODUCTION_DATE = "INSERT";
                                                        Insert_External_CLient_ProductionDate();


                                                    }
                                                    else if (Check_External_Production > 0)
                                                    {


                                                        OPERATE_PRODUCTION_DATE = "UPDATE";
                                                        Insert_External_CLient_ProductionDate();
                                                    }

                                                    if (Email_Sending_Status == "True") // this is for 32013 Client Restrict To not to send email;
                                                    {

                                                        if (Validate_Email_Check() != false && chk_Email_Yes.Checked == true)
                                                        {
                                                            if (Title_Logy_Order_Task_Id == 15 && Validate_Titlelogy_Inovice_Page() != false && validate_Email_Sent() != false)
                                                            {
                                                                date = DateTime.Now;






                                                                Hashtable htProductionDate = new Hashtable();
                                                                DataTable dtproductiondate = new System.Data.DataTable();
                                                                htProductionDate.Add("@Trans", "INSERT");
                                                                htProductionDate.Add("@External_Order_Id", External_Client_Order_Id);
                                                                htProductionDate.Add("@Order_Task", 15);
                                                                htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                                                htProductionDate.Add("@Order_Production_date", txt_Prdoductiondate.Text);
                                                                htProductionDate.Add("@Inserted_By", userid);
                                                                htProductionDate.Add("@Inserted_date", date);
                                                                htProductionDate.Add("@status", "True");
                                                                dtproductiondate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htProductionDate);


                                                                // Update Tax information Status and Plat Map Page status

                                                                // this is for DB-Prak Title Client
                                                                if (Client_id == 33 && Sub_ProcessId == 300)
                                                                {
                                                                    if (Order_Type_Id == 116 || Order_Type_Id == 113 || Order_Type_Id == 117 || Order_Type_Id == 7)
                                                                    {
                                                                        if (chk_Plat_Yes.Checked == true)
                                                                        {

                                                                            Chk_Plat_Map = true;
                                                                        }
                                                                        else if (chk_plat_No.Checked == true)
                                                                        {

                                                                            Chk_Plat_Map = false;
                                                                        }

                                                                        if (chk_Tax_Yes.Checked == true)
                                                                        {

                                                                            Chk_Tax_Information = true;
                                                                        }
                                                                        else if (chk_Tax_No.Checked == true)
                                                                        {

                                                                            Chk_Tax_Information = false;
                                                                        }


                                                                        Hashtable htchk = new Hashtable();
                                                                        DataTable dtchk = new DataTable();

                                                                        htchk.Add("@Trans", "CEHCK");
                                                                        htchk.Add("@Order_Id", External_Client_Order_Id);
                                                                        htchk.Add("@Order_Task", SESSION_ORDER_TASK);
                                                                        dtchk = dataaccess.ExecuteSP("Sp_External_Client_Orders_Invoice_Check_List_Details", htchk);
                                                                        int chk_Count = 0;
                                                                        if (dtchk.Rows.Count > 0)
                                                                        {
                                                                            chk_Count = int.Parse(dtchk.Rows[0]["count"].ToString());
                                                                        }
                                                                        else
                                                                        {
                                                                            chk_Count = 0;

                                                                        }

                                                                        if (chk_Count == 0)
                                                                        {
                                                                            Hashtable htin = new Hashtable();
                                                                            DataTable dtin = new DataTable();

                                                                            htin.Add("@Trans", "INSERT");
                                                                            htin.Add("@Order_Id", External_Client_Order_Id);
                                                                            htin.Add("@Order_Task", SESSION_ORDER_TASK);
                                                                            htin.Add("@Plat_Map", Chk_Plat_Map);
                                                                            htin.Add("@Tax_Information", Chk_Tax_Information);
                                                                            htin.Add("@User_Id", userid);
                                                                            dtchk = dataaccess.ExecuteSP("Sp_External_Client_Orders_Invoice_Check_List_Details", htchk);

                                                                        }
                                                                        else
                                                                        {
                                                                            Hashtable htin = new Hashtable();
                                                                            DataTable dtin = new DataTable();

                                                                            htin.Add("@Trans", "UPDATE");
                                                                            htin.Add("@Order_Id", External_Client_Order_Id);
                                                                            htin.Add("@Order_Task", SESSION_ORDER_TASK);
                                                                            htin.Add("@Plat_Map", Chk_Plat_Map);
                                                                            htin.Add("@Tax_Information", Chk_Tax_Information);
                                                                            htin.Add("@User_Id", userid);
                                                                            dtchk = dataaccess.ExecuteSP("Sp_External_Client_Orders_Invoice_Check_List_Details", htchk);


                                                                        }
                                                                    }




                                                                }




                                                                // Updating Titlelogy Order Completed

                                                                ht_Titlelogy_Order_Task_Status.Clear();
                                                                dt_TitleLogy_Order_Task_Status.Clear();

                                                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 15);
                                                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 3);

                                                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);


                                                            }
                                                        }
                                                        else if (Validate_Email_Check() != false)
                                                        {
                                                            Hashtable htProductionDate = new Hashtable();
                                                            DataTable dtproductiondate = new System.Data.DataTable();
                                                            htProductionDate.Add("@Trans", "INSERT");
                                                            htProductionDate.Add("@External_Order_Id", External_Client_Order_Id);
                                                            htProductionDate.Add("@Order_Task", 15);
                                                            htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                                            htProductionDate.Add("@Order_Production_date", txt_Prdoductiondate.Text);
                                                            htProductionDate.Add("@Inserted_By", userid);
                                                            htProductionDate.Add("@Inserted_date", date);
                                                            htProductionDate.Add("@status", "True");
                                                            dtproductiondate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htProductionDate);


                                                            ht_Titlelogy_Order_Task_Status.Clear();
                                                            dt_TitleLogy_Order_Task_Status.Clear();

                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 15);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 3);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);

                                                        }

                                                    }


                                                }
                                                else
                                                {


                                                }






                                            }




                                            if (validate_Titlelogy_Invoice() != false)
                                            {





                                                // This is for Non Internal Tax Orders

                                                if (int.Parse(SESSION_ORDER_TASK.ToString()) != 22)
                                                {

                                                    Hashtable htprogress = new Hashtable();
                                                    DataTable dtprogress = new System.Data.DataTable();
                                                    htprogress.Add("@Trans", "UPDATE");
                                                    htprogress.Add("@Order_ID", Order_Id);
                                                    if (ddl_order_Task.Visible != true)
                                                    {
                                                        htprogress.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));

                                                        //Title logy Order Progress
                                                        Title_Logy_Order_Status_Id = int.Parse(ddl_order_Staus.SelectedValue.ToString());
                                                    }
                                                    else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                                    {
                                                        htprogress.Add("@Order_Progress_Id", 8);
                                                        Title_Logy_Order_Status_Id = 14;
                                                    }
                                                    else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                                    {
                                                        htprogress.Add("@Order_Progress_Id", 3);
                                                        Title_Logy_Order_Status_Id = 3;
                                                    }


                                                    htprogress.Add("@Modified_By", userid);
                                                    htprogress.Add("@Modified_Date", date);
                                                    dtprogress = dataaccess.ExecuteSP("Sp_Order_Assignment", htprogress);

                                                    Hashtable ht_Status = new Hashtable();
                                                    DataTable dt_Status = new System.Data.DataTable();
                                                    ht_Status.Add("@Trans", "UPDATE_STATUS");
                                                    ht_Status.Add("@Order_ID", Order_Id);

                                                    if (ddl_order_Task.Visible != true)
                                                    {
                                                        ht_Status.Add("@Order_Status", SESSION_ORDER_TASK.ToString());
                                                        ht_Status.Add("@Order_Progress", int.Parse(ddl_order_Staus.SelectedValue.ToString()));

                                                    }
                                                    else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                                    {
                                                        Hashtable htuser = new Hashtable();
                                                        DataTable dtuser = new System.Data.DataTable();
                                                        htuser.Add("@Trans", "SELECT_STATUSID");
                                                        htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                        dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                        ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                        ht_Status.Add("@Order_Progress", 8);
                                                        Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                                    }
                                                    else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                                    {
                                                        Hashtable htuser = new Hashtable();
                                                        DataTable dtuser = new System.Data.DataTable();
                                                        htuser.Add("@Trans", "SELECT_STATUSID");
                                                        htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                        dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                        ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                        ht_Status.Add("@Order_Progress", 3);
                                                        Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                                    }
                                                    ht_Status.Add("@Modified_By", userid);
                                                    ht_Status.Add("@Modified_Date", dateeval);
                                                    dt_Status = dataaccess.ExecuteSP("Sp_Order", ht_Status);
                                                    if (ddl_order_Staus.SelectedItem != "USER HOLD")
                                                    {
                                                        Hashtable ht_Chk_Order = new Hashtable();
                                                        DataTable dt_Chk_Order = new DataTable();
                                                        ht_Chk_Order.Add("@Trans", "Emp_Order_Count");
                                                        ht_Chk_Order.Add("@Employee_Id", userid);
                                                        dt_Chk_Order = dataaccess.ExecuteSP("Sp_Order_Auto_Allocation", ht_Chk_Order);
                                                        if (int.Parse(dt_Chk_Order.Rows[0]["count_Order"].ToString()) <= 0)
                                                        {
                                                            Hashtable ht_Update_Emp_Status = new Hashtable();
                                                            DataTable dt_Update_Emp_Status = new DataTable();
                                                            ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                                            ht_Update_Emp_Status.Add("@Employee_Id", userid);
                                                            ht_Update_Emp_Status.Add("@Allocate_Status", "False");
                                                            dt_Update_Emp_Status = dataaccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);
                                                        }
                                                    }
                                                    Hashtable htEffectivedate = new Hashtable();
                                                    DataTable dtEffectivdate = new System.Data.DataTable();
                                                    htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                                                    htEffectivedate.Add("@Order_ID", Order_Id);
                                                    htEffectivedate.Add("@Effective_date", txt_Effectivedate.Text);
                                                    htEffectivedate.Add("@Modified_By", userid);
                                                    htEffectivedate.Add("@Modified_Date", dateeval);
                                                    dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);


                                                    Hashtable ht_Productiondate = new Hashtable();
                                                    DataTable dt_Production_date = new DataTable();

                                                    ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                    ht_Productiondate.Add("@Order_ID", Order_Id);
                                                    ht_Productiondate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                                                    dt_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Productiondate);

                                                    if (dt_Production_date.Rows.Count > 0)
                                                    {

                                                        Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                                    }
                                                    else
                                                    {

                                                        Chk_Production_date = 0;
                                                    }

                                                    if (Chk_Production_date > 0)
                                                    {
                                                        OPERATE_PRODUCTION_DATE = "UPDATE";
                                                        Insert_ProductionDate();

                                                    }
                                                    else if (Chk_Production_date == 0)
                                                    {
                                                        OPERATE_PRODUCTION_DATE = "INSERT";
                                                        Insert_ProductionDate();
                                                    }

                                                    if (ddl_order_Task.Text == "Upload Completed")
                                                    {
                                                        Hashtable ht_Comp_Productiondate = new Hashtable();
                                                        DataTable dt_Comp_Production_date = new DataTable();

                                                        ht_Comp_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                        ht_Comp_Productiondate.Add("@Order_ID", Order_Id);
                                                        ht_Comp_Productiondate.Add("@Order_Status_Id", 15);
                                                        dt_Comp_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Comp_Productiondate);

                                                        if (dt_Production_date.Rows.Count > 0)
                                                        {

                                                            Chk_Production_date = int.Parse(dt_Comp_Production_date.Rows[0]["count"].ToString());


                                                        }
                                                        else
                                                        {

                                                            Chk_Production_date = 0;
                                                        }

                                                        if (Chk_Production_date > 0)
                                                        {
                                                            OPERATE_PRODUCTION_DATE = "UPDATE";
                                                            Insert_Order_Completed_ProductionDate();

                                                        }
                                                        else if (Chk_Production_date == 0)
                                                        {
                                                            OPERATE_PRODUCTION_DATE = "INSERT";
                                                            Insert_Order_Completed_ProductionDate();
                                                        }

                                                    }



                                                    Insert_OrderComments();
                                                    Insert_delay_Order_Comments(1);
                                                    Geydview_Bind_Notes();
                                                    Geydview_Bind_Comments();
                                                    if (Order_Task == 1 || Order_Task == 2)
                                                    {
                                                        Hashtable ht_Select_Order_Details = new Hashtable();
                                                        DataTable dt_Select_Order_Details = new DataTable();

                                                        ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                                        ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                                        dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                                        if (dt_Select_Order_Details.Rows.Count > 0)
                                                        {

                                                            Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                                        }
                                                        else
                                                        {

                                                            Chk_Order_Search_Cost = 0;
                                                        }

                                                        if (Chk_Order_Search_Cost > 0)
                                                        {
                                                            OPERATE_SEARCH_COST = "UPDATE";
                                                            Insert_Order_Search_Cost();

                                                        }
                                                        else if (Chk_Order_Search_Cost == 0)
                                                        {
                                                            OPERATE_SEARCH_COST = "INSERT";
                                                            Insert_Order_Search_Cost();
                                                        }
                                                    }






                                                    Update_User_Order_Time_Info_Status();


                                                    //OrderHistory
                                                    Hashtable ht_Order_History = new Hashtable();
                                                    DataTable dt_Order_History = new DataTable();
                                                    ht_Order_History.Add("@Trans", "INSERT");
                                                    ht_Order_History.Add("@Order_Id", Order_Id);
                                                    //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                    ht_Order_History.Add("@Status_Id", Next_Status);
                                                    ht_Order_History.Add("@Progress_Id", 8);
                                                    ht_Order_History.Add("@Work_Type", 1);
                                                    ht_Order_History.Add("@Assigned_By", userid);
                                                    ht_Order_History.Add("@Modification_Type", "Order Complete");
                                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                    //Inserting Internal Tax_Status

                                                    if (Internal_Tax_Check == 1 && ddl_Tax_Task.SelectedIndex > 0)
                                                    {


                                                        Hashtable htinsert_tax = new Hashtable();
                                                        DataTable dtinternal_tax = new DataTable();
                                                        htinsert_tax.Add("@Trans", "INSERT");
                                                        htinsert_tax.Add("@Order_Id", Order_Id);
                                                        htinsert_tax.Add("@Order_Task", Next_Status);
                                                        htinsert_tax.Add("@Order_Status", 3);
                                                        htinsert_tax.Add("@Tax_Internal_Status_Id", int.Parse(ddl_Tax_Task.SelectedValue.ToString()));
                                                        htinsert_tax.Add("@User_Id", userid);
                                                        htinsert_tax.Add("@Production_Date", txt_Prdoductiondate.Text);
                                                        dtinternal_tax = dataaccess.ExecuteSP("Sp_Tax_Order_Internal_Status", htinsert_tax);

                                                        //OrderHistory for Tax
                                                        Hashtable ht_Order_History_1 = new Hashtable();
                                                        DataTable dt_Order_History_1 = new DataTable();
                                                        ht_Order_History_1.Add("@Trans", "INSERT");
                                                        ht_Order_History_1.Add("@Order_Id", Order_Id);
                                                        //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                        ht_Order_History_1.Add("@Status_Id", Next_Status);
                                                        ht_Order_History_1.Add("@Progress_Id", 8);
                                                        ht_Order_History_1.Add("@Work_Type", 1);
                                                        ht_Order_History_1.Add("@Assigned_By", userid);
                                                        ht_Order_History_1.Add("@Modification_Type", "Tax Status Selected as " + ddl_Tax_Task.Text.ToString() + "");
                                                        dt_Order_History_1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History_1);



                                                    }

                                                }
                                                // This for Internal Tax Orders
                                                else
                                                {


                                                    Hashtable ht_Productiondate = new Hashtable();
                                                    DataTable dt_Production_date = new DataTable();

                                                    ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                    ht_Productiondate.Add("@Order_ID", Order_Id);
                                                    ht_Productiondate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                                                    dt_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Productiondate);

                                                    if (dt_Production_date.Rows.Count > 0)
                                                    {

                                                        Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                                    }
                                                    else
                                                    {

                                                        Chk_Production_date = 0;
                                                    }

                                                    if (Chk_Production_date > 0)
                                                    {
                                                        OPERATE_PRODUCTION_DATE = "UPDATE";
                                                        Insert_ProductionDate();

                                                    }
                                                    else if (Chk_Production_date == 0)
                                                    {
                                                        OPERATE_PRODUCTION_DATE = "INSERT";
                                                        Insert_ProductionDate();
                                                    }



                                                    // This for Order Completed Production Date

                                                    if (ddl_order_Task.Text == "Upload Completed")
                                                    {
                                                        Hashtable ht_Comp_Productiondate = new Hashtable();
                                                        DataTable dt_Comp_Production_date = new DataTable();

                                                        ht_Comp_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                        ht_Comp_Productiondate.Add("@Order_ID", Order_Id);
                                                        ht_Comp_Productiondate.Add("@Order_Status_Id", 15);
                                                        dt_Comp_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Comp_Productiondate);

                                                        if (dt_Production_date.Rows.Count > 0)
                                                        {

                                                            Chk_Production_date = int.Parse(dt_Comp_Production_date.Rows[0]["count"].ToString());


                                                        }
                                                        else
                                                        {

                                                            Chk_Production_date = 0;
                                                        }

                                                        if (Chk_Production_date > 0)
                                                        {
                                                            OPERATE_PRODUCTION_DATE = "UPDATE";
                                                            Insert_Order_Completed_ProductionDate();

                                                        }
                                                        else if (Chk_Production_date == 0)
                                                        {
                                                            OPERATE_PRODUCTION_DATE = "INSERT";
                                                            Insert_Order_Completed_ProductionDate();
                                                        }

                                                    }




                                                    Insert_OrderComments();
                                                    Insert_delay_Order_Comments(1);
                                                    Geydview_Bind_Notes();
                                                    Geydview_Bind_Comments();


                                                    //Updating Internal Tax Order tatus in Orders 

                                                    Hashtable htupdate_tax_Internal_Status = new Hashtable();
                                                    DataTable dtuodate_tax_Internal_Status = new DataTable();

                                                    htupdate_tax_Internal_Status.Add("@Trans", "UPDATE_INTERNAL_TAX_STATUS");
                                                    htupdate_tax_Internal_Status.Add("@Search_Tax_Req_Inhouse_Status", 3);
                                                    htupdate_tax_Internal_Status.Add("@Modified_By", userid);
                                                    htupdate_tax_Internal_Status.Add("@Order_ID", Order_Id);
                                                    dtuodate_tax_Internal_Status = dataaccess.ExecuteSP("Sp_Order", htupdate_tax_Internal_Status);




                                                    Update_User_Order_Time_Info_Status();

                                                    //OrderHistory
                                                    Hashtable ht_Order_History = new Hashtable();
                                                    DataTable dt_Order_History = new DataTable();
                                                    ht_Order_History.Add("@Trans", "INSERT");
                                                    ht_Order_History.Add("@Order_Id", Order_Id);
                                                    ht_Order_History.Add("@Status_Id", 22);
                                                    ht_Order_History.Add("@Progress_Id", 3);
                                                    ht_Order_History.Add("@Work_Type", 1);
                                                    ht_Order_History.Add("@Assigned_By", userid);
                                                    ht_Order_History.Add("@Modification_Type", "Tax Task Completed");
                                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                                }

                                                // This is For Sending to Internal Tax Request for particular 25000 Client after Search Completes

                                                if (Sub_ProcessId == 200)
                                                {
                                                    if (Order_Task == 2 && ddl_order_Staus.SelectedValue.ToString() == "3")
                                                    {


                                                        // this is checking this clients belongs which product type for that tax will move
                                                        Hashtable ht_check = new Hashtable();
                                                        DataTable dt_check = new System.Data.DataTable();
                                                        ht_check.Add("@Trans", "CHECK");
                                                        ht_check.Add("@Client_Id", 26);// this Subprocess belongs to this client id
                                                        ht_check.Add("@Order_Type_Id", Order_Type_Id);
                                                        ht_check.Add("@flag", "False");
                                                        dt_check = dataaccess.ExecuteSP("Sp_Tax_Order_Movement_Client_Product_Type", ht_check);

                                                        int Check_Count = 0;
                                                        if (dt_check.Rows.Count > 0)
                                                        {

                                                            Check_Count = int.Parse(dt_check.Rows[0]["COUNT"].ToString());
                                                        }
                                                        else
                                                        {

                                                            Check_Count = 0;
                                                        }


                                                        if (Check_Count > 0)
                                                        {

                                                            Hashtable htcheck = new Hashtable();
                                                            DataTable dtcheck = new DataTable();
                                                            htcheck.Add("@Trans", "CHECK_ORDER");
                                                            htcheck.Add("@Order_Id", Order_Id);
                                                            dtcheck = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
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
                                                                // Moving to Tax Qc Stage
                                                                if (ddl_Tax_Task.SelectedValue.ToString() == "1")
                                                                {

                                                                    Insert_Internal_Tax_Order_Status(2);
                                                                }// Moving to Tax Agent level
                                                                else if (ddl_Tax_Task.SelectedValue.ToString() == "2")
                                                                {
                                                                    Insert_Internal_Tax_Order_Status(1);

                                                                }
                                                            }
                                                        }

                                                    }
                                                }

                                                Clear();



                                                SplashScreenManager.CloseForm(false);

                                                MessageBox.Show("Order Submitted Sucessfully");
                                                formProcess = 1;
                                                this.Close();
                                                foreach (Form f in Application.OpenForms)
                                                {
                                                    if (f.Text == "Judgement_Period_Create_View")
                                                    {
                                                        IsOpen_us = true;
                                                        f.Focus();
                                                        f.Enabled = true;
                                                        f.Show();
                                                        break;
                                                    }
                                                    if (f.Text == "State_Wise_Tax_Due_Date")
                                                    {
                                                        IsOpen_jud = true;
                                                        f.Focus();
                                                        f.Enabled = true;
                                                        f.Show();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SplashScreenManager.CloseForm(false);
                                            txt_Prdoductiondate.Focus();

                                            MessageBox.Show("Enter Production  Date");
                                        }
                                    }
                                    else
                                    {
                                        SplashScreenManager.CloseForm(false);
                                        txt_Effectivedate.Focus();
                                        MessageBox.Show("Enter Effective Date");

                                    }

                                    //cProbar.stopProgress();
                                }
                            }
                            catch (Exception ex)
                            {
                                SplashScreenManager.CloseForm(false);

                                MessageBox.Show("Error Occured Please Check With Administrator");
                            }

                            finally
                            {

                                SplashScreenManager.CloseForm(false);


                            }
                            // }
                        }
                    }
                }
                else if (ddl_order_Staus.SelectedValue != "3")
                {


                    if (Chk == 0)
                    {
                        if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9")
                        {
                            //employee order entry form enabled false
                            this.Enabled = false;
                            Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());
                            Order_Status_Id = Convert.ToInt32(ddl_order_Staus.SelectedValue);
                            Ordermanagement_01.Task_Conformation Taskconfomation = new Ordermanagement_01.Task_Conformation(userid, Order_Id, Order_Task, Order_Status_Id);
                            Taskconfomation.ShowDialog();
                            Chk = 1;
                            ddl_order_Task.Visible = false;


                        }
                    }

                    else
                    {

                        if (Validate_Order_Info() != false && Valid_date() != false && Validate_Effective_Date() != false && validate_subscription() != false)
                        {
                            if (Chk_Self_Allocate.Checked == false)
                            {
                                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                                try
                                {
                                    int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                                    if (txt_Effectivedate.Text != "")
                                    {

                                        if (txt_Prdoductiondate.Text != "" && Valid_date() != false)
                                        {


                                            // This is for Non Tax Orders



                                            if (int.Parse(SESSION_ORDER_TASK.ToString()) != 22)
                                            {

                                                DateTime date1 = DateTime.Now;
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");
                                                Hashtable htupdate = new Hashtable();
                                                DataTable dtupdate = new System.Data.DataTable();
                                                htupdate.Add("@Trans", "UPDATE_PROGRESS");

                                                htupdate.Add("@Order_ID", Order_Id);

                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    htupdate.Add("@Order_Status", SESSION_ORDER_TASK.ToString());
                                                    htupdate.Add("@Order_Progress", int.Parse(ddl_order_Staus.SelectedValue.ToString()));

                                                    //for Titlelogy Niranjan
                                                    if (int.Parse(ddl_order_Staus.SelectedValue.ToString()) == 7)
                                                    {

                                                        Title_Logy_Order_Task_Id = int.Parse(SESSION_ORDER_TASK.ToString());

                                                        Title_Logy_Order_Status_Id = 14;
                                                    }
                                                    else
                                                    {
                                                        Title_Logy_Order_Task_Id = int.Parse(SESSION_ORDER_TASK.ToString());

                                                        Title_Logy_Order_Status_Id = int.Parse(ddl_order_Staus.SelectedValue.ToString());

                                                    }

                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                                {
                                                    Hashtable htuser = new Hashtable();
                                                    DataTable dtuser = new System.Data.DataTable();
                                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                                    htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                    htupdate.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                    htupdate.Add("@Order_Progress", 8);
                                                    //for Titlelogy Niranjan
                                                    Title_Logy_Order_Task_Id = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                                    Title_Logy_Order_Status_Id = 14;

                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                                {
                                                    Hashtable htuser = new Hashtable();
                                                    DataTable dtuser = new System.Data.DataTable();
                                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                                    htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                    htupdate.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                    htupdate.Add("@Order_Progress", 3);

                                                    //for Titlelogy Niranjan
                                                    Title_Logy_Order_Task_Id = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                                    Title_Logy_Order_Status_Id = 3;

                                                }
                                                htupdate.Add("@Modified_By", userid);
                                                htupdate.Add("@Modified_Date", dateeval);
                                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);






                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                {

                                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                    if (External_Client_Order_Task_Id == 18 && Title_Logy_Order_Task_Id == 15)
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Title_Logy_Order_Task_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", Title_Logy_Order_Status_Id);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);

                                                    }
                                                    else if (External_Client_Order_Task_Id != 18)
                                                    {
                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Title_Logy_Order_Task_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", Title_Logy_Order_Status_Id);

                                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);


                                                    }




                                                }


                                                Hashtable htprogress = new Hashtable();
                                                DataTable dtprogress = new System.Data.DataTable();
                                                htprogress.Add("@Trans", "UPDATE");
                                                htprogress.Add("@Order_ID", Order_Id);
                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    if (ddl_order_Task.Text != "Upload Completed")
                                                    {
                                                        htprogress.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                                    }
                                                    else if (ddl_order_Task.Text == "Upload Completed")
                                                    {
                                                        htprogress.Add("@Order_Progress_Id", 3);
                                                    }
                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                                {
                                                    htprogress.Add("@Order_Progress_Id", 8);
                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                                {
                                                    htprogress.Add("@Order_Progress_Id", 3);
                                                }
                                                htprogress.Add("@Modified_By", userid);
                                                htprogress.Add("@Modified_Date", date);
                                                dtprogress = dataaccess.ExecuteSP("Sp_Order_Assignment", htprogress);

                                                Hashtable ht_Status = new Hashtable();
                                                DataTable dt_Status = new System.Data.DataTable();
                                                ht_Status.Add("@Trans", "UPDATE_STATUS");
                                                ht_Status.Add("@Order_ID", Order_Id);

                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    ht_Status.Add("@Order_Status", SESSION_ORDER_TASK.ToString());
                                                    ht_Status.Add("@Order_Progress", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                                {
                                                    Hashtable htuser = new Hashtable();
                                                    DataTable dtuser = new System.Data.DataTable();
                                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                                    htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                    ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                    ht_Status.Add("@Order_Progress", 8);
                                                }
                                                else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                                {
                                                    Hashtable htuser = new Hashtable();
                                                    DataTable dtuser = new System.Data.DataTable();
                                                    htuser.Add("@Trans", "SELECT_STATUSID");
                                                    htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                    dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                    ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                    ht_Status.Add("@Order_Progress", 3);
                                                }
                                                ht_Status.Add("@Modified_By", userid);
                                                ht_Status.Add("@Modified_Date", dateeval);
                                                dt_Status = dataaccess.ExecuteSP("Sp_Order", ht_Status);




                                                Hashtable htEffectivedate = new Hashtable();
                                                DataTable dtEffectivdate = new System.Data.DataTable();
                                                htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                                                htEffectivedate.Add("@Order_ID", Order_Id);
                                                htEffectivedate.Add("@Effective_date", txt_Effectivedate.Text);
                                                htEffectivedate.Add("@Modified_By", userid);
                                                htEffectivedate.Add("@Modified_Date", dateeval);
                                                dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);


                                                if (Order_Task == 1 || Order_Task == 2)
                                                {
                                                    Hashtable ht_Select_Order_Details = new Hashtable();
                                                    DataTable dt_Select_Order_Details = new DataTable();

                                                    ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                                    ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                                    dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                                    if (dt_Select_Order_Details.Rows.Count > 0)
                                                    {

                                                        Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                                    }
                                                    else
                                                    {

                                                        Chk_Order_Search_Cost = 0;
                                                    }

                                                    if (Chk_Order_Search_Cost > 0)
                                                    {
                                                        OPERATE_SEARCH_COST = "UPDATE";
                                                        Insert_Order_Search_Cost();

                                                    }
                                                    else if (Chk_Order_Search_Cost == 0)
                                                    {
                                                        OPERATE_SEARCH_COST = "INSERT";
                                                        Insert_Order_Search_Cost();
                                                    }
                                                }


                                                Hashtable ht_Productiondate = new Hashtable();
                                                DataTable dt_Production_date = new DataTable();
                                                ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                ht_Productiondate.Add("@Order_ID", Order_Id);
                                                ht_Productiondate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                                                dt_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Productiondate);

                                                if (dt_Production_date.Rows.Count > 0)
                                                {

                                                    Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                                }
                                                else
                                                {

                                                    Chk_Production_date = 0;
                                                }

                                                if (Chk_Production_date > 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "UPDATE";
                                                    Insert_ProductionDate();

                                                }
                                                else if (Chk_Production_date == 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "INSERT";
                                                    Insert_ProductionDate();
                                                }
                                                Insert_OrderComments();
                                                Insert_delay_Order_Comments(1);
                                                Geydview_Bind_Notes();
                                                Geydview_Bind_Comments();



                                                Update_User_Order_Time_Info_Status();
                                                Clear();



                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                ht_Order_History.Add("@Status_Id", SESSION_ORDER_TASK.ToString());
                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    ht_Order_History.Add("@Progress_Id", Prog);
                                                    ht_Order_History.Add("@Modification_Type", "Order " + Prog_Val);
                                                }
                                                else
                                                {
                                                    ht_Order_History.Add("@Progress_Id", 8);
                                                    ht_Order_History.Add("@Modification_Type", "Order User Hold");
                                                }
                                                ht_Order_History.Add("@Assigned_By", userid);

                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                            }


                                            // This is for Tax Orders

                                            else
                                            {


                                                Hashtable ht_Productiondate = new Hashtable();
                                                DataTable dt_Production_date = new DataTable();
                                                ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                ht_Productiondate.Add("@Order_ID", Order_Id);
                                                ht_Productiondate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                                                dt_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Productiondate);

                                                if (dt_Production_date.Rows.Count > 0)
                                                {

                                                    Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                                }
                                                else
                                                {

                                                    Chk_Production_date = 0;
                                                }

                                                if (Chk_Production_date > 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "UPDATE";
                                                    Insert_ProductionDate();

                                                }
                                                else if (Chk_Production_date == 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "INSERT";
                                                    Insert_ProductionDate();
                                                }
                                                Insert_OrderComments();
                                                Insert_delay_Order_Comments(1);
                                                Geydview_Bind_Notes();
                                                Geydview_Bind_Comments();



                                                Update_User_Order_Time_Info_Status();
                                                //Updating Internal Tax Order tatus in Orders 

                                                Hashtable htupdate_tax_Internal_Status = new Hashtable();
                                                DataTable dtuodate_tax_Internal_Status = new DataTable();

                                                htupdate_tax_Internal_Status.Add("@Trans", "UPDATE_INTERNAL_TAX_STATUS");
                                                htupdate_tax_Internal_Status.Add("@Search_Tax_Req_Inhouse_Status", 7);
                                                htupdate_tax_Internal_Status.Add("@Modified_By", userid);
                                                htupdate_tax_Internal_Status.Add("@Order_ID", Order_Id);
                                                dtuodate_tax_Internal_Status = dataaccess.ExecuteSP("Sp_Order", htupdate_tax_Internal_Status);





                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                ht_Order_History.Add("@Status_Id", SESSION_ORDER_TASK.ToString());
                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    ht_Order_History.Add("@Progress_Id", Prog);
                                                    ht_Order_History.Add("@Modification_Type", "Order " + Prog_Val);
                                                }
                                                else
                                                {
                                                    ht_Order_History.Add("@Progress_Id", 8);
                                                    ht_Order_History.Add("@Modification_Type", "Tax Task User Hold");
                                                }
                                                ht_Order_History.Add("@Assigned_By", userid);

                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);



                                                Clear();

                                            }






                                            SplashScreenManager.CloseForm(false);


                                            MessageBox.Show("Order Submitted Sucessfully");
                                            formProcess = 1;
                                            this.Close();
                                            foreach (Form f in Application.OpenForms)
                                            {
                                                if (f.Text == "Judgement_Period_Create_View")
                                                {
                                                    IsOpen_us = true;
                                                    f.Focus();
                                                    f.Enabled = true;
                                                    f.Show();
                                                    break;
                                                }
                                                if (f.Text == "State_Wise_Tax_Due_Date")
                                                {
                                                    IsOpen_jud = true;
                                                    f.Focus();
                                                    f.Enabled = true;
                                                    f.Show();
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SplashScreenManager.CloseForm(false);

                                            txt_Prdoductiondate.Focus();

                                            MessageBox.Show("Enter Production  Date");
                                        }
                                    }
                                    else
                                    {
                                        SplashScreenManager.CloseForm(false);

                                        txt_Effectivedate.Focus();

                                        MessageBox.Show("Enter Effective Date");

                                    }
                                }
                                catch (Exception ex)

                                {
                                    SplashScreenManager.CloseForm(false);

                                    MessageBox.Show("Error Occured Please Check With Administrator");
                                }
                                finally

                                {
                                    SplashScreenManager.CloseForm(false);


                                }

                            }
                        }
                    }


                }

            }

        }




        public void Submit_Rework_data()
        {
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABR");
            ht_BIND.Add("@Order_Type", lbl_Order_Type.Text);
            dt_BIND = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_BIND);
            if (dt_BIND.Rows.Count > 0)
            {
                Order_Type_ABS = dt_BIND.Rows[0]["Order_Type_Abrivation"].ToString();
            }
            Hashtable ht_task = new Hashtable();
            DataTable dt_task = new DataTable();
            ht_task.Add("@Trans", "SELECT_STATUSID");
            ht_task.Add("@Order_Status", lbl_Order_Task_Type.Text);
            dt_task = dataaccess.ExecuteSP("Sp_Order_Status", ht_task);
            if (dt_task.Rows.Count > 0)
            {
                Taskid = int.Parse(dt_task.Rows[0]["Order_Status_ID"].ToString());
            }


            ////Update Checklist
            //COUNT_NO_QUESTION_AVLIABLE

            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();
            htcount.Add("@Trans", "COUNT_NO_QUESTION_AVLIABLE");
            htcount.Add("@Order_Status", Taskid);
            if (lbl_Order_Task_Type.Text == "Search" || lbl_Order_Task_Type.Text == "Search QC")
            {
                htcount.Add("@Order_Type_ABS", Order_Type_ABS);
            }
            else
            {
                htcount.Add("@Order_Type_ABS", "COS");
            }
            dtcount = dataaccess.ExecuteSP("Sp_Check_List", htcount);
            if (dtcount.Rows.Count > 0)
            {
                AVAILABLE_COUNT = int.Parse(dtcount.Rows[0]["count"].ToString());
            }



            //COUNT_NO_QUESTION_USER_ENTERED
            if (int.Parse(SESSION_ORDER_TASK.ToString().ToString()) != 12 && int.Parse(SESSION_ORDER_TASK.ToString()) != 24 && ddl_order_Staus.SelectedValue.ToString() == "3")
            {
                // USERCOUNT = 1;
                Hashtable htentercount = new Hashtable();
                DataTable dtentercount = new DataTable();
                htentercount.Add("@Trans", "COUNT_NO_QUESTION_USER_ENTERED");
                htentercount.Add("@Order_Task", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                htentercount.Add("@Order_Id", Order_Id);
                htentercount.Add("@User_id", userid);
                htentercount.Add("@Order_Type_Abs_Id", Order_Type_ABS_id);
                htentercount.Add("@Work_Type", Work_Type_Id);
                dtentercount = dataaccess.ExecuteSP("Sp_Checklist_Detail", htentercount);
                if (dtentercount.Rows.Count > 0)
                {
                    USERCOUNT = int.Parse(dtentercount.Rows[0]["count"].ToString());
                }
                else
                {
                    USERCOUNT = 0;
                }

                if (USERCOUNT == 0)
                {
                    MessageBox.Show("Checklist questions not entered");

                }
                else
                {

                    USERCOUNT = 1;
                }



            }
            else
            {

                USERCOUNT = 1;
            }

            if (USERCOUNT > 0)
            {

                int Next_Status = 0;
                int Prog = 0;
                string Prog_Val = "";
                if (ddl_order_Staus.Text != "Select")
                {
                    Prog = int.Parse(ddl_order_Staus.SelectedValue.ToString());
                    Prog_Val = ddl_order_Staus.Text;
                }


                Hashtable htdatalist = new Hashtable();
                DataTable dtdatalist = new DataTable();
                htdatalist.Add("@Trans", "CHECK_ORDER_WISE");
                htdatalist.Add("@Order_Status", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                htdatalist.Add("@Order_Id", Order_Id);
                htdatalist.Add("@Work_Type_Id", Work_Type_Id);
                dtdatalist = dataaccess.ExecuteSP("Sp_Order_Document_List", htdatalist);

                int checkdatalistcount = int.Parse(dtdatalist.Rows[0]["count"].ToString());



                if (ddl_order_Staus.SelectedValue.ToString() == "3")
                {




                    if (Chk == 0)
                    {
                        if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9")
                        {
                            //employee order entry form enabled false
                            this.Enabled = false;


                            Ordermanagement_01.Task_Conformation Taskconfomation = new Ordermanagement_01.Task_Conformation(userid, Order_Id, Order_Task, Order_Status_Id);
                            Taskconfomation.ShowDialog();
                            Chk = 1;
                            ddl_order_Task.Visible = false;


                        }
                    }
                    else if (SESSSION_ORDER_TYPE == "Search" && ddl_Order_Source.Text == "" && Chk != 1)
                    {
                        ddl_Order_Source.Focus();
                        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter Order Source')</script>", false);
                        MessageBox.Show("Enter Order Source");
                    }
                    else
                    {

                        if (Validate_Order_Info() != false && Valid_date() != false && Validate_Effective_Date() != false && Validate_Document_List() != false && Validate_Search_Cost() != false && Validate_Error_Entry() != false)
                        {
                            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                            try
                            {



                                Hashtable ht_Task_Complete = new Hashtable();
                                DataTable dt_Task_Complete = new DataTable();
                                ht_Task_Complete.Add("@Trans", "Task_Complete");
                                ht_Task_Complete.Add("@Order_ID1", Order_Id);
                                ht_Task_Complete.Add("@Status_ID1", SESSION_ORDER_TASK);

                                dt_Task_Complete = dataaccess.ExecuteSP("Sp_rpt_Task_Conformation_Trans", ht_Task_Complete);


                                if (Chk_Self_Allocate.Checked == false)
                                {
                                    int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                                    if (Order_Task == 2 || Order_Task == 3)
                                    {
                                        Hashtable ht_Select_Order_Details = new Hashtable();
                                        DataTable dt_Select_Order_Details = new DataTable();

                                        ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                        ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                        dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                        if (dt_Select_Order_Details.Rows.Count > 0)
                                        {

                                            Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                        }
                                        else
                                        {

                                            Chk_Order_Search_Cost = 0;
                                        }

                                        if (Chk_Order_Search_Cost > 0)
                                        {
                                            OPERATE_SEARCH_COST = "UPDATE";
                                            //Insert_Order_Search_Cost();

                                        }
                                        else if (Chk_Order_Search_Cost == 0)
                                        {
                                            OPERATE_SEARCH_COST = "INSERT";
                                            // Insert_Order_Search_Cost();
                                        }
                                    }

                                    //cProbar.startProgress();
                                    form_loader.Start_progres();

                                    if (txt_Effectivedate.Text != "")
                                    {

                                        if (txt_Prdoductiondate.Text != "" && Valid_date() != false)
                                        {
                                            //if (Order_Task == 1 || Order_Task == 2)
                                            //{
                                            DateTime date1 = DateTime.Now;
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");
                                            Hashtable htupdate = new Hashtable();
                                            DataTable dtupdate = new System.Data.DataTable();

                                            //Updating Rework Status
                                            htupdate.Add("@Trans", "UPDATE_STATUS");
                                            htupdate.Add("@Order_ID", Order_Id);

                                            if (ddl_order_Task.Visible != true)
                                            {
                                                htupdate.Add("@Current_Task", SESSION_ORDER_TASK.ToString());
                                                htupdate.Add("@Cureent_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));



                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text); //Order_Sttaus means current_task 
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);

                                                htupdate.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                htupdate.Add("@Cureent_Status", 8);   //Order Progress means Current Status


                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());




                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);


                                                htupdate.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                htupdate.Add("@Cureent_Status", 3);
                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()); // For Order Histroy



                                            }

                                            htupdate.Add("@Modified_By", userid);

                                            dtupdate = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);




                                            //==================================Rework Order Status=====================================================


                                            Hashtable htprogress = new Hashtable();
                                            DataTable dtprogress = new System.Data.DataTable();
                                            htprogress.Add("@Trans", "UPDATE");
                                            htprogress.Add("@Order_ID", Order_Id);
                                            if (ddl_order_Task.Visible != true)
                                            {
                                                htprogress.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));


                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                            {
                                                htprogress.Add("@Order_Progress_Id", 8);

                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                            {
                                                htprogress.Add("@Order_Progress_Id", 3);

                                            }


                                            htprogress.Add("@Modified_By", userid);

                                            dtprogress = dataaccess.ExecuteSP("Sp_Order_Rework_Assignment", htprogress);

                                            Hashtable ht_Status = new Hashtable();
                                            DataTable dt_Status = new System.Data.DataTable();
                                            ht_Status.Add("@Trans", "UPDATE_TASK");  // Update order Task
                                            ht_Status.Add("@Order_Id", Order_Id);

                                            if (ddl_order_Task.Visible != true)
                                            {
                                                ht_Status.Add("@Current_Task", SESSION_ORDER_TASK.ToString());
                                                ht_Status.Add("@Cureent_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString())); // Order-progres =cureent_Status

                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                ht_Status.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                ht_Status.Add("@Cureent_Status", 8);
                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                ht_Status.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                ht_Status.Add("@Cureent_Status", 8);
                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                            }
                                            ht_Status.Add("@Modified_By", userid);

                                            dt_Status = dataaccess.ExecuteSP("Sp_Order_Rework_Status", ht_Status);



                                            Hashtable htEffectivedate = new Hashtable();
                                            DataTable dtEffectivdate = new System.Data.DataTable();
                                            htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                                            htEffectivedate.Add("@Order_ID", Order_Id);
                                            htEffectivedate.Add("@Effective_date", txt_Effectivedate.Text);
                                            htEffectivedate.Add("@Modified_By", userid);
                                            htEffectivedate.Add("@Modified_Date", dateeval);
                                            dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);

                                            Hashtable ht_Productiondate = new Hashtable();
                                            DataTable dt_Production_date = new DataTable();

                                            ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                            ht_Productiondate.Add("@Order_ID", Order_Id);
                                            ht_Productiondate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                                            dt_Production_date = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", ht_Productiondate);

                                            if (dt_Production_date.Rows.Count > 0)
                                            {

                                                Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());

                                            }
                                            else
                                            {

                                                Chk_Production_date = 0;
                                            }

                                            if (Chk_Production_date > 0)
                                            {
                                                OPERATE_PRODUCTION_DATE = "UPDATE";
                                                Insert_Rework_ProductionDate();

                                            }
                                            else if (Chk_Production_date == 0)
                                            {
                                                OPERATE_PRODUCTION_DATE = "INSERT";
                                                Insert_Rework_ProductionDate();
                                            }

                                            if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                            {

                                                Hashtable ht_Comp_Productiondate = new Hashtable();
                                                DataTable dt_Comp_Production_date = new DataTable();

                                                ht_Comp_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                ht_Comp_Productiondate.Add("@Order_ID", Order_Id);
                                                ht_Comp_Productiondate.Add("@Order_Task", 15);
                                                dt_Comp_Production_date = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", ht_Comp_Productiondate);

                                                if (dt_Comp_Production_date.Rows.Count > 0)
                                                {

                                                    Chk_Production_date = int.Parse(dt_Comp_Production_date.Rows[0]["count"].ToString());

                                                }
                                                else
                                                {

                                                    Chk_Production_date = 0;
                                                }

                                                if (Chk_Production_date > 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "UPDATE";
                                                    Insert_Rework_Order_Completed_ProductionDate();

                                                }
                                                else if (Chk_Production_date == 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "INSERT";
                                                    Insert_Rework_Order_Completed_ProductionDate();
                                                }


                                            }





                                            Insert_OrderComments();
                                            Insert_delay_Order_Comments(2);
                                            Geydview_Bind_Notes();
                                            Geydview_Bind_Comments();
                                            if (Order_Task == 1 || Order_Task == 2)
                                            {
                                                Hashtable ht_Select_Order_Details = new Hashtable();
                                                DataTable dt_Select_Order_Details = new DataTable();

                                                ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                                ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                                dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                                if (dt_Select_Order_Details.Rows.Count > 0)
                                                {

                                                    Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                                }
                                                else
                                                {

                                                    Chk_Order_Search_Cost = 0;
                                                }

                                                if (Chk_Order_Search_Cost > 0)
                                                {
                                                    OPERATE_SEARCH_COST = "UPDATE";
                                                    //Insert_Order_Search_Cost();

                                                }
                                                else if (Chk_Order_Search_Cost == 0)
                                                {
                                                    OPERATE_SEARCH_COST = "INSERT";
                                                    //Insert_Order_Search_Cost();
                                                }
                                            }


                                            Update_Rework_User_Order_Time_Info_Status();
                                            Clear();


                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            DataTable dt_Order_History = new DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", Order_Id);
                                            //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                            ht_Order_History.Add("@Status_Id", Next_Status);
                                            ht_Order_History.Add("@Progress_Id", 8);
                                            ht_Order_History.Add("@Assigned_By", userid);
                                            ht_Order_History.Add("@Modification_Type", "Rework Order Completed");
                                            ht_Order_History.Add("@Work_Type", Work_Type_Id);
                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                            //


                                            // string url = "AdminDashboard.aspx";
                                            // cProbar.stopProgress();
                                            SplashScreenManager.CloseForm(false);
                                            MessageBox.Show("Order Submitted Sucessfully");
                                            formProcess = 1;
                                            this.Close();
                                            foreach (Form f in Application.OpenForms)
                                            {
                                                if (f.Text == "Judgement_Period_Create_View")
                                                {
                                                    IsOpen_us = true;
                                                    f.Focus();
                                                    f.Enabled = true;
                                                    f.Show();
                                                    break;
                                                }
                                                if (f.Text == "State_Wise_Tax_Due_Date")
                                                {
                                                    IsOpen_jud = true;
                                                    f.Focus();
                                                    f.Enabled = true;
                                                    f.Show();
                                                    break;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            SplashScreenManager.CloseForm(false);
                                            txt_Prdoductiondate.Focus();

                                            MessageBox.Show("Enter Production  Date");
                                        }
                                    }
                                    else
                                    {
                                        SplashScreenManager.CloseForm(false);
                                        txt_Effectivedate.Focus();

                                        MessageBox.Show("Enter Effective Date");

                                    }

                                    // cProbar.stopProgress();
                                }
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
                            // }
                        }
                    }
                }
                else if (ddl_order_Staus.SelectedValue != "3")
                {


                    if (Chk == 0)
                    {
                        if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9")
                        {
                            //employee order entry form enabled false
                            this.Enabled = false;

                            Ordermanagement_01.Task_Conformation Taskconfomation = new Ordermanagement_01.Task_Conformation(userid, Order_Id, Order_Task, Order_Status_Id);
                            Taskconfomation.ShowDialog();
                            Chk = 1;
                            ddl_order_Task.Visible = false;


                        }
                    }
                    //else if (SESSSION_ORDER_TYPE == "Search" && ddl_Order_Source.Text == "" && Chk != 1)
                    //{
                    //    ddl_Order_Source.Focus();
                    //    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter Order Source')</script>", false);
                    //    MessageBox.Show("Enter Order Source");
                    //}
                    else
                    {

                        if (Validate_Order_Info() != false && Valid_date() != false && Validate_Effective_Date() != false)
                        {

                            if (Chk_Self_Allocate.Checked == false)
                            {
                                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                                try
                                {

                                    int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                                    if (txt_Effectivedate.Text != "")
                                    {

                                        if (txt_Prdoductiondate.Text != "" && Valid_date() != false)
                                        {
                                            //if (Order_Task == 1 || Order_Task == 2)
                                            //{
                                            DateTime date1 = DateTime.Now;
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            Hashtable htupdate = new Hashtable();
                                            DataTable dtupdate = new System.Data.DataTable();
                                            //Updating Rework Status
                                            htupdate.Add("@Trans", "UPDATE_STATUS");
                                            htupdate.Add("@Order_ID", Order_Id);

                                            if (ddl_order_Task.Visible != true)
                                            {
                                                htupdate.Add("@Current_Task", SESSION_ORDER_TASK.ToString());
                                                htupdate.Add("@Cureent_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));



                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text); //Order_Sttaus means current_task 
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);

                                                htupdate.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                htupdate.Add("@Cureent_Status", 8);   //Order Progress means Current Status


                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());




                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);


                                                htupdate.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                htupdate.Add("@Cureent_Status", 3);
                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()); // For Order Histroy



                                            }

                                            htupdate.Add("@Modified_By", userid);

                                            dtupdate = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);




                                            //==================================Rework Order Status=====================================================


                                            Hashtable htprogress = new Hashtable();
                                            DataTable dtprogress = new System.Data.DataTable();
                                            htprogress.Add("@Trans", "UPDATE");
                                            htprogress.Add("@Order_ID", Order_Id);
                                            if (ddl_order_Task.Visible != true)
                                            {
                                                htprogress.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));


                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                            {
                                                htprogress.Add("@Order_Progress_Id", 8);

                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                            {
                                                htprogress.Add("@Order_Progress_Id", 3);

                                            }


                                            htprogress.Add("@Modified_By", userid);

                                            dtprogress = dataaccess.ExecuteSP("Sp_Order_Rework_Assignment", htprogress);

                                            Hashtable ht_Status = new Hashtable();
                                            DataTable dt_Status = new System.Data.DataTable();
                                            ht_Status.Add("@Trans", "UPDATE_TASK");  // Update order Task
                                            ht_Status.Add("@Order_Id", Order_Id);

                                            if (ddl_order_Task.Visible != true)
                                            {
                                                ht_Status.Add("@Current_Task", SESSION_ORDER_TASK.ToString());
                                                ht_Status.Add("@Cureent_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString())); // Order-progres =cureent_Status

                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text != "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                                                ht_Status.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                ht_Status.Add("@Cureent_Status", 8);
                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                            }
                                            else if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                            {
                                                Hashtable htuser = new Hashtable();
                                                DataTable dtuser = new System.Data.DataTable();
                                                htuser.Add("@Trans", "SELECT_STATUSID");
                                                htuser.Add("@Order_Status", ddl_order_Task.Text);
                                                dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);

                                                ht_Status.Add("@Current_Task", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                                                ht_Status.Add("@Cureent_Status", 8);
                                                Next_Status = int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString());
                                            }
                                            ht_Status.Add("@Modified_By", userid);

                                            dt_Status = dataaccess.ExecuteSP("Sp_Order_Rework_Status", ht_Status);



                                            Hashtable htEffectivedate = new Hashtable();
                                            DataTable dtEffectivdate = new System.Data.DataTable();
                                            htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                                            htEffectivedate.Add("@Order_ID", Order_Id);
                                            htEffectivedate.Add("@Effective_date", txt_Effectivedate.Text);
                                            htEffectivedate.Add("@Modified_By", userid);
                                            htEffectivedate.Add("@Modified_Date", dateeval);
                                            dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);

                                            Hashtable ht_Productiondate = new Hashtable();
                                            DataTable dt_Production_date = new DataTable();

                                            ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                            ht_Productiondate.Add("@Order_ID", Order_Id);
                                            ht_Productiondate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                                            dt_Production_date = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", ht_Productiondate);

                                            if (dt_Production_date.Rows.Count > 0)
                                            {

                                                Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                            }
                                            else
                                            {

                                                Chk_Production_date = 0;
                                            }

                                            if (Chk_Production_date > 0)
                                            {
                                                OPERATE_PRODUCTION_DATE = "UPDATE";
                                                Insert_Rework_ProductionDate();

                                            }
                                            else if (Chk_Production_date == 0)
                                            {
                                                OPERATE_PRODUCTION_DATE = "INSERT";
                                                Insert_Rework_ProductionDate();
                                            }
                                            Insert_OrderComments();
                                            Insert_delay_Order_Comments(2);
                                            Geydview_Bind_Notes();
                                            Geydview_Bind_Comments();
                                            if (Order_Task == 1 || Order_Task == 2)
                                            {
                                                Hashtable ht_Select_Order_Details = new Hashtable();
                                                DataTable dt_Select_Order_Details = new DataTable();

                                                ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                                ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                                dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                                if (dt_Select_Order_Details.Rows.Count > 0)
                                                {

                                                    Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                                }
                                                else
                                                {

                                                    Chk_Order_Search_Cost = 0;
                                                }

                                                if (Chk_Order_Search_Cost > 0)
                                                {
                                                    OPERATE_SEARCH_COST = "UPDATE";
                                                    Insert_Order_Search_Cost();

                                                }
                                                else if (Chk_Order_Search_Cost == 0)
                                                {
                                                    OPERATE_SEARCH_COST = "INSERT";
                                                    Insert_Order_Search_Cost();
                                                }
                                            }


                                            Update_Rework_User_Order_Time_Info_Status();
                                            Clear();

                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            DataTable dt_Order_History = new DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", Order_Id);
                                            //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                            ht_Order_History.Add("@Status_Id", SESSION_ORDER_TASK.ToString());
                                            if (ddl_order_Task.Visible != true)
                                            {
                                                ht_Order_History.Add("@Progress_Id", Prog);
                                                ht_Order_History.Add("@Modification_Type", "Order " + Prog_Val);
                                            }
                                            else
                                            {
                                                ht_Order_History.Add("@Progress_Id", 8);
                                                ht_Order_History.Add("@Modification_Type", "Order User Hold");
                                            }
                                            ht_Order_History.Add("@Assigned_By", userid);

                                            ht_Order_History.Add("@Work_Type", Work_Type_Id);
                                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                            // string url = "AdminDashboard.aspx";
                                            //cProbar.stopProgress();

                                            SplashScreenManager.CloseForm(false);
                                            MessageBox.Show("Order Submitted Sucessfully");
                                            formProcess = 1;
                                            this.Close();
                                            foreach (Form f in Application.OpenForms)
                                            {
                                                if (f.Text == "Judgement_Period_Create_View")
                                                {
                                                    IsOpen_us = true;
                                                    f.Focus();
                                                    f.Enabled = true;
                                                    f.Show();
                                                    break;
                                                }
                                                if (f.Text == "State_Wise_Tax_Due_Date")
                                                {
                                                    IsOpen_jud = true;
                                                    f.Focus();
                                                    f.Enabled = true;
                                                    f.Show();
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SplashScreenManager.CloseForm(false);
                                            txt_Prdoductiondate.Focus();

                                            MessageBox.Show("Enter Production  Date");
                                        }
                                    }
                                    else
                                    {
                                        SplashScreenManager.CloseForm(false);
                                        txt_Effectivedate.Focus();
                                        MessageBox.Show("Enter Effective Date");

                                    }

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
                    }


                }

            }

        }


        public void Submit_Super_Qc_data()
        {
            Hashtable ht_BIND = new Hashtable();
            DataTable dt_BIND = new DataTable();
            ht_BIND.Add("@Trans", "GET_ORDER_ABR");
            ht_BIND.Add("@Order_Type", lbl_Order_Type.Text);
            dt_BIND = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", ht_BIND);
            if (dt_BIND.Rows.Count > 0)
            {
                Order_Type_ABS = dt_BIND.Rows[0]["Order_Type_Abrivation"].ToString();
            }
            Hashtable ht_task = new Hashtable();
            DataTable dt_task = new DataTable();
            ht_task.Add("@Trans", "SELECT_STATUSID");
            ht_task.Add("@Order_Status", lbl_Order_Task_Type.Text);
            dt_task = dataaccess.ExecuteSP("Sp_Order_Status", ht_task);
            if (dt_task.Rows.Count > 0)
            {
                Taskid = int.Parse(dt_task.Rows[0]["Order_Status_ID"].ToString());
            }


            ////Update Checklist
            //COUNT_NO_QUESTION_AVLIABLE

            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();
            htcount.Add("@Trans", "COUNT_NO_QUESTION_AVLIABLE");
            htcount.Add("@Order_Status", Taskid);
            if (lbl_Order_Task_Type.Text == "Search" || lbl_Order_Task_Type.Text == "Search QC")
            {
                htcount.Add("@Order_Type_ABS", Order_Type_ABS);
            }
            else
            {
                htcount.Add("@Order_Type_ABS", "COS");
            }
            dtcount = dataaccess.ExecuteSP("Sp_Check_List", htcount);
            if (dtcount.Rows.Count > 0)
            {
                AVAILABLE_COUNT = int.Parse(dtcount.Rows[0]["count"].ToString());
            }



            //COUNT_NO_QUESTION_USER_ENTERED
            if (int.Parse(SESSION_ORDER_TASK.ToString().ToString()) != 12 && ddl_order_Staus.SelectedValue.ToString() == "3")
            {
                // USERCOUNT = 1;
                Hashtable htentercount = new Hashtable();
                DataTable dtentercount = new DataTable();
                htentercount.Add("@Trans", "COUNT_NO_QUESTION_USER_ENTERED");
                htentercount.Add("@Order_Task", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                htentercount.Add("@Order_Id", Order_Id);
                htentercount.Add("@User_id", userid);
                htentercount.Add("@Order_Type_Abs_Id", Order_Type_ABS_id);
                htentercount.Add("@Work_Type", Work_Type_Id);
                dtentercount = dataaccess.ExecuteSP("Sp_Checklist_Detail", htentercount);
                if (dtentercount.Rows.Count > 0)
                {
                    USERCOUNT = int.Parse(dtentercount.Rows[0]["count"].ToString());
                }
                else
                {
                    USERCOUNT = 0;
                }

                if (USERCOUNT == 0)
                {
                    MessageBox.Show("Checklist questions not entered");

                }
                else
                {

                    USERCOUNT = 1;
                }



            }
            else
            {

                USERCOUNT = 1;
            }

            if (USERCOUNT > 0)
            {

                int Next_Status = 0;
                int Prog = 0;
                string Prog_Val = "";
                if (ddl_order_Staus.Text != "Select")
                {
                    Prog = int.Parse(ddl_order_Staus.SelectedValue.ToString());
                    Prog_Val = ddl_order_Staus.Text;
                }


                Hashtable htdatalist = new Hashtable();
                DataTable dtdatalist = new DataTable();
                htdatalist.Add("@Trans", "CHECK_ORDER_WISE");
                htdatalist.Add("@Order_Status", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                htdatalist.Add("@Order_Id", Order_Id);
                htdatalist.Add("@Work_Type_Id", Work_Type_Id);
                dtdatalist = dataaccess.ExecuteSP("Sp_Order_Document_List", htdatalist);

                int checkdatalistcount = int.Parse(dtdatalist.Rows[0]["count"].ToString());



                if (ddl_order_Staus.SelectedValue.ToString() == "3")
                {




                    if (Chk == 0)
                    {
                        if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9")
                        {
                            //employee order entry form enabled false
                            this.Enabled = false;


                            Ordermanagement_01.Task_Conformation Taskconfomation = new Ordermanagement_01.Task_Conformation(userid, Order_Id, Order_Task, Order_Status_Id);
                            Taskconfomation.ShowDialog();
                            Chk = 1;
                            ddl_order_Task.Visible = false;


                        }

                        else
                        {

                            if (Validate_Order_Info() != false && Valid_date() != false && Validate_Effective_Date() != false && Validate_Document_List() != false && Validate_Search_Cost() != false && Validate_Error_Entry() != false)
                            {


                                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                                try
                                {
                                    Hashtable ht_Task_Complete = new Hashtable();
                                    DataTable dt_Task_Complete = new DataTable();
                                    ht_Task_Complete.Add("@Trans", "Task_Complete");
                                    ht_Task_Complete.Add("@Order_ID1", Order_Id);
                                    ht_Task_Complete.Add("@Status_ID1", SESSION_ORDER_TASK);

                                    dt_Task_Complete = dataaccess.ExecuteSP("Sp_rpt_Task_Conformation_Trans", ht_Task_Complete);


                                    if (Chk_Self_Allocate.Checked == false)
                                    {
                                        int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                                        if (Order_Task == 2 || Order_Task == 3)
                                        {
                                            Hashtable ht_Select_Order_Details = new Hashtable();
                                            DataTable dt_Select_Order_Details = new DataTable();

                                            ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                            ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                            if (dt_Select_Order_Details.Rows.Count > 0)
                                            {

                                                Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                            }
                                            else
                                            {

                                                Chk_Order_Search_Cost = 0;
                                            }

                                            if (Chk_Order_Search_Cost > 0)
                                            {
                                                OPERATE_SEARCH_COST = "UPDATE";
                                                // Insert_Order_Search_Cost();

                                            }
                                            else if (Chk_Order_Search_Cost == 0)
                                            {
                                                OPERATE_SEARCH_COST = "INSERT";
                                                //  Insert_Order_Search_Cost();
                                            }
                                        }

                                        //cProbar.startProgress();
                                        form_loader.Start_progres();

                                        if (txt_Effectivedate.Text != "")
                                        {

                                            if (txt_Prdoductiondate.Text != "" && Valid_date() != false)
                                            {
                                                //if (Order_Task == 1 || Order_Task == 2)
                                                //{
                                                DateTime date1 = DateTime.Now;
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");
                                                Hashtable htupdate = new Hashtable();
                                                DataTable dtupdate = new System.Data.DataTable();



                                                //Updating Rework Status
                                                htupdate.Add("@Trans", "UPDATE_STATUS");
                                                htupdate.Add("@Order_ID", Order_Id);


                                                htupdate.Add("@Current_Task", SESSION_ORDER_TASK.ToString());
                                                htupdate.Add("@Cureent_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));



                                                htupdate.Add("@Modified_By", userid);

                                                dtupdate = dataaccess.ExecuteSP("Sp_Super_Qc_Status", htupdate);




                                                //==================================Super Qc Order Status=====================================================


                                                Hashtable htprogress = new Hashtable();
                                                DataTable dtprogress = new System.Data.DataTable();
                                                htprogress.Add("@Trans", "UPDATE");
                                                htprogress.Add("@Order_ID", Order_Id);
                                                htprogress.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    htprogress.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));


                                                }


                                                htprogress.Add("@Modified_By", userid);

                                                dtprogress = dataaccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htprogress);





                                                Hashtable htEffectivedate = new Hashtable();
                                                DataTable dtEffectivdate = new System.Data.DataTable();
                                                htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                                                htEffectivedate.Add("@Order_ID", Order_Id);
                                                htEffectivedate.Add("@Effective_date", txt_Effectivedate.Text);
                                                htEffectivedate.Add("@Modified_By", userid);
                                                htEffectivedate.Add("@Modified_Date", dateeval);
                                                dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);


                                                Hashtable ht_Productiondate = new Hashtable();
                                                DataTable dt_Production_date = new DataTable();

                                                ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                ht_Productiondate.Add("@Order_ID", Order_Id);
                                                ht_Productiondate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                                                dt_Production_date = dataaccess.ExecuteSP("Sp_Order_Super_Qc_ProductionDate", ht_Productiondate);

                                                if (dt_Production_date.Rows.Count > 0)
                                                {

                                                    Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                                }
                                                else
                                                {

                                                    Chk_Production_date = 0;
                                                }

                                                if (Chk_Production_date > 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "UPDATE";

                                                    Insert_Super_Qc_ProductionDate();
                                                }
                                                else if (Chk_Production_date == 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "INSERT";
                                                    Insert_Super_Qc_ProductionDate();
                                                }

                                                if (ddl_order_Task.Visible == true && ddl_order_Task.Text == "Upload Completed")
                                                {

                                                    Hashtable ht_Comp_Productiondate = new Hashtable();
                                                    DataTable dt_Comp_Production_date = new DataTable();

                                                    ht_Comp_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                    ht_Comp_Productiondate.Add("@Order_ID", Order_Id);
                                                    ht_Comp_Productiondate.Add("@Order_Task", 15);
                                                    dt_Comp_Production_date = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", ht_Comp_Productiondate);

                                                    if (dt_Comp_Production_date.Rows.Count > 0)
                                                    {

                                                        Chk_Production_date = int.Parse(dt_Comp_Production_date.Rows[0]["count"].ToString());

                                                    }
                                                    else
                                                    {

                                                        Chk_Production_date = 0;
                                                    }

                                                    if (Chk_Production_date > 0)
                                                    {
                                                        OPERATE_PRODUCTION_DATE = "UPDATE";
                                                        Insert_Super_Qc_Order_Completed_ProductionDate();

                                                    }
                                                    else if (Chk_Production_date == 0)
                                                    {
                                                        OPERATE_PRODUCTION_DATE = "INSERT";
                                                        Insert_Super_Qc_Order_Completed_ProductionDate();
                                                    }


                                                }


                                                Insert_OrderComments();
                                                Insert_delay_Order_Comments(3);
                                                Geydview_Bind_Notes();
                                                Geydview_Bind_Comments();
                                                if (Order_Task == 1 || Order_Task == 2)
                                                {
                                                    Hashtable ht_Select_Order_Details = new Hashtable();
                                                    DataTable dt_Select_Order_Details = new DataTable();

                                                    ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                                    ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                                    dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                                    if (dt_Select_Order_Details.Rows.Count > 0)
                                                    {

                                                        Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                                    }
                                                    else
                                                    {

                                                        Chk_Order_Search_Cost = 0;
                                                    }

                                                    if (Chk_Order_Search_Cost > 0)
                                                    {
                                                        OPERATE_SEARCH_COST = "UPDATE";
                                                        // Insert_Order_Search_Cost();

                                                    }
                                                    else if (Chk_Order_Search_Cost == 0)
                                                    {
                                                        OPERATE_SEARCH_COST = "INSERT";
                                                        // Insert_Order_Search_Cost();
                                                    }
                                                }


                                                Update_Super_Qc_User_Order_Time_Info_Status();
                                                Clear();


                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                ht_Order_History.Add("@Status_Id", SESSION_ORDER_TASK.ToString());
                                                ht_Order_History.Add("@Progress_Id", 3);
                                                ht_Order_History.Add("@Assigned_By", userid);
                                                ht_Order_History.Add("@Modification_Type", "Super qc Order Completed");
                                                ht_Order_History.Add("@Work_Type", Work_Type_Id);
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                                //
                                                SplashScreenManager.CloseForm(false);

                                                // string url = "AdminDashboard.aspx";
                                                // cProbar.stopProgress();
                                                MessageBox.Show("Order Submitted Sucessfully");
                                                formProcess = 1;

                                                if (InvokeRequired == false)
                                                {

                                                    this.Invoke(new MethodInvoker(delegate
                                                    {

                                                        foreach (Form f in Application.OpenForms)
                                                        {
                                                            if (f.Text == "Judgement_Period_Create_View")
                                                            {
                                                                IsOpen_us = true;
                                                                f.Focus();
                                                                f.Enabled = true;
                                                                f.Show();
                                                                break;
                                                            }
                                                            if (f.Text == "State_Wise_Tax_Due_Date")
                                                            {
                                                                IsOpen_jud = true;
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
                                                        if (f.Text == "Judgement_Period_Create_View")
                                                        {
                                                            IsOpen_us = true;
                                                            f.Focus();
                                                            f.Enabled = true;
                                                            f.Show();
                                                            break;
                                                        }
                                                        if (f.Text == "State_Wise_Tax_Due_Date")
                                                        {
                                                            IsOpen_jud = true;
                                                            f.Focus();
                                                            f.Enabled = true;
                                                            f.Show();
                                                            break;
                                                        }
                                                    }

                                                }


                                                this.Close();

                                            }
                                            else
                                            {
                                                SplashScreenManager.CloseForm(false);
                                                txt_Prdoductiondate.Focus();

                                                MessageBox.Show("Enter Production  Date");
                                            }
                                        }
                                        else
                                        {
                                            SplashScreenManager.CloseForm(false);
                                            txt_Effectivedate.Focus();
                                            MessageBox.Show("Enter Effective Date");

                                        }

                                        // cProbar.stopProgress();
                                    }

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
                                // }
                            }
                        }
                    }
                    else if (ddl_order_Staus.SelectedValue != "3")
                    {


                        if (Chk == 0)
                        {
                            if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9")
                            {
                                //employee order entry form enabled false
                                this.Enabled = false;

                                Ordermanagement_01.Task_Conformation Taskconfomation = new Ordermanagement_01.Task_Conformation(userid, Order_Id, Order_Task, Order_Status_Id);
                                Taskconfomation.ShowDialog();
                                Chk = 1;
                                ddl_order_Task.Visible = false;


                            }
                        }
                        //else if (SESSSION_ORDER_TYPE == "Search" && ddl_Order_Source.Text == "" && Chk != 1)
                        //{
                        //    ddl_Order_Source.Focus();
                        //    // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter Order Source')</script>", false);
                        //    MessageBox.Show("Enter Order Source");
                        //}
                        else
                        {

                            if (Validate_Order_Info() != false && Valid_date() != false && Validate_Effective_Date() != false)
                            {

                                if (Chk_Self_Allocate.Checked == false)
                                {
                                    SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                                    try
                                    {

                                        int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                                        if (txt_Effectivedate.Text != "")
                                        {

                                            if (txt_Prdoductiondate.Text != "" && Valid_date() != false)
                                            {
                                                //if (Order_Task == 1 || Order_Task == 2)
                                                //{
                                                DateTime date1 = DateTime.Now;
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");



                                                //Updating Super Qc Status
                                                Hashtable htupdate = new Hashtable();
                                                DataTable dtupdate = new System.Data.DataTable();
                                                htupdate.Add("@Trans", "UPDATE_STATUS");
                                                htupdate.Add("@Order_ID", Order_Id);
                                                htupdate.Add("@Current_Task", SESSION_ORDER_TASK.ToString());
                                                htupdate.Add("@Cureent_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                                                htupdate.Add("@Modified_By", userid);
                                                dtupdate = dataaccess.ExecuteSP("Sp_Super_Qc_Status", htupdate);




                                                //==================================Super Qc Order Status=====================================================


                                                Hashtable htprogress = new Hashtable();
                                                DataTable dtprogress = new System.Data.DataTable();
                                                htprogress.Add("@Trans", "UPDATE");
                                                htprogress.Add("@Order_ID", Order_Id);
                                                htprogress.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    htprogress.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));


                                                }


                                                htprogress.Add("@Modified_By", userid);

                                                dtprogress = dataaccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htprogress);





                                                Hashtable htEffectivedate = new Hashtable();
                                                DataTable dtEffectivdate = new System.Data.DataTable();
                                                htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                                                htEffectivedate.Add("@Order_ID", Order_Id);
                                                htEffectivedate.Add("@Effective_date", txt_Effectivedate.Text);
                                                htEffectivedate.Add("@Modified_By", userid);
                                                htEffectivedate.Add("@Modified_Date", dateeval);
                                                dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);


                                                Hashtable ht_Productiondate = new Hashtable();
                                                DataTable dt_Production_date = new DataTable();

                                                ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                ht_Productiondate.Add("@Order_ID", Order_Id);
                                                ht_Productiondate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                                                dt_Production_date = dataaccess.ExecuteSP("Sp_Order_Super_Qc_ProductionDate", ht_Productiondate);

                                                if (dt_Production_date.Rows.Count > 0)
                                                {

                                                    Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                                                }
                                                else
                                                {

                                                    Chk_Production_date = 0;
                                                }

                                                if (Chk_Production_date > 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "UPDATE";

                                                    Insert_Super_Qc_ProductionDate();
                                                }
                                                else if (Chk_Production_date == 0)
                                                {
                                                    OPERATE_PRODUCTION_DATE = "INSERT";
                                                    Insert_Super_Qc_ProductionDate();
                                                }
                                                Insert_OrderComments();
                                                Insert_delay_Order_Comments(3);
                                                Geydview_Bind_Notes();
                                                Geydview_Bind_Comments();
                                                if (Order_Task == 1 || Order_Task == 2)
                                                {
                                                    Hashtable ht_Select_Order_Details = new Hashtable();
                                                    DataTable dt_Select_Order_Details = new DataTable();

                                                    ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                                                    ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                                                    dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                                                    if (dt_Select_Order_Details.Rows.Count > 0)
                                                    {

                                                        Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                                                    }
                                                    else
                                                    {

                                                        Chk_Order_Search_Cost = 0;
                                                    }

                                                    if (Chk_Order_Search_Cost > 0)
                                                    {
                                                        OPERATE_SEARCH_COST = "UPDATE";
                                                        //Insert_Order_Search_Cost();

                                                    }
                                                    else if (Chk_Order_Search_Cost == 0)
                                                    {
                                                        OPERATE_SEARCH_COST = "INSERT";
                                                        // Insert_Order_Search_Cost();
                                                    }
                                                }


                                                Update_Super_Qc_User_Order_Time_Info_Status();
                                                Clear();

                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Order_History.Add("@Status_Id", SESSION_ORDER_TASK.ToString());
                                                if (ddl_order_Task.Visible != true)
                                                {
                                                    ht_Order_History.Add("@Progress_Id", Prog);
                                                    ht_Order_History.Add("@Modification_Type", "Order " + Prog_Val);
                                                }
                                                else
                                                {
                                                    ht_Order_History.Add("@Progress_Id", 8);
                                                    ht_Order_History.Add("@Modification_Type", "Order User Hold");
                                                }
                                                ht_Order_History.Add("@Assigned_By", userid);

                                                ht_Order_History.Add("@Work_Type", Work_Type_Id);
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                // string url = "AdminDashboard.aspx";
                                                //cProbar.stopProgress();
                                                SplashScreenManager.CloseForm(false);
                                                MessageBox.Show("Order Submitted Sucessfully");
                                                formProcess = 1;

                                                if (InvokeRequired == false)
                                                {

                                                    this.Invoke(new MethodInvoker(delegate
                                                    {

                                                        foreach (Form f in Application.OpenForms)
                                                        {
                                                            if (f.Text == "Judgement_Period_Create_View")
                                                            {
                                                                IsOpen_us = true;
                                                                f.Focus();
                                                                f.Enabled = true;
                                                                f.Show();
                                                                break;
                                                            }
                                                            if (f.Text == "State_Wise_Tax_Due_Date")
                                                            {
                                                                IsOpen_jud = true;
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
                                                        if (f.Text == "Judgement_Period_Create_View")
                                                        {
                                                            IsOpen_us = true;
                                                            f.Focus();
                                                            f.Enabled = true;
                                                            f.Show();
                                                            break;
                                                        }
                                                        if (f.Text == "State_Wise_Tax_Due_Date")
                                                        {
                                                            IsOpen_jud = true;
                                                            f.Focus();
                                                            f.Enabled = true;
                                                            f.Show();
                                                            break;
                                                        }
                                                    }
                                                }

                                                this.Close();
                                            }
                                            else
                                            {
                                                SplashScreenManager.CloseForm(false);
                                                txt_Prdoductiondate.Focus();

                                                MessageBox.Show("Enter Production  Date");
                                            }
                                        }
                                        else
                                        {
                                            SplashScreenManager.CloseForm(false);
                                            txt_Effectivedate.Focus();
                                            MessageBox.Show("Enter Effective Date");

                                        }

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
                        }


                    }

                }

            }



        }

        public void Check_Parent_Sub_Chld()
        {

            Hashtable htchecklist = new Hashtable();
            DataTable dtcecklist = new DataTable();
            htchecklist.Add("@Trans", "SELECT_BEFORE");
            htchecklist.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
            htchecklist.Add("@Order_ID", Order_Id);

            dtcecklist = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htchecklist);
            Check_List_Count = int.Parse(dtcecklist.Rows.Count.ToString());
            if (Check_List_Count > 0)
            {

                Hashtable htsubcount = new Hashtable();
                DataTable dtsubcount = new DataTable();

                htsubcount.Add("@Trans", "GET_COUNT_TASK_CONFIRM_ID");
                htsubcount.Add("@Order_ID", Order_Id);
                htsubcount.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                dtsubcount = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htsubcount);
                int count = int.Parse(dtsubcount.Rows[0]["count"].ToString());

                Hashtable htget_Parent = new Hashtable();
                DataTable dtget_Partent = new DataTable();
                if (count == 0)
                {
                    htget_Parent.Add("@Trans", "GET_ORDER_WISE_TASK_ID");
                }
                else if (count > 0)
                {
                    htget_Parent.Add("@Trans", "GET_NOT_ENTERED_ORDER_WISE_TASK_CONFIRM_ID");

                }
                htget_Parent.Add("@Order_ID", Order_Id);
                htget_Parent.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                dtget_Partent = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_Parent);

                if (dtget_Partent.Rows.Count > 0)
                {

                    Hashtable htget_enteredsub = new Hashtable();
                    DataTable dtget_enteredsub = new DataTable();
                    dtget_enteredsub.Rows.Clear();
                    htget_enteredsub.Add("@Trans", "GET_ENTERED_SUB_ID");
                    htget_enteredsub.Add("@Task_Confirm_Id", dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString());
                    htget_enteredsub.Add("@Order_ID", Order_Id);
                    htget_enteredsub.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                    dtget_enteredsub = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_enteredsub);

                    if (dtget_enteredsub.Rows.Count > 0)
                    {
                        Hashtable htget_child = new Hashtable();
                        DataTable dtget_child = new DataTable();
                        htget_child.Add("@Trans", "GET_ALL_CHILD_QUESTION_ON_TASK_SUB_ID");
                        htget_child.Add("@Task_Confirm_Id", dtget_enteredsub.Rows[0]["Task_Confirm_Id"].ToString());
                        htget_child.Add("@Task_Confirm_Sub_Id", int.Parse(dtget_enteredsub.Rows[0]["Task_Confirm_Sub_Id"].ToString()));
                        htget_child.Add("@Order_ID", Order_Id);
                        htget_child.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                        dtget_child = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_child);

                        if (dtget_child.Rows.Count > 0)
                        {
                            Order_Check_List chk = new Order_Check_List(int.Parse(dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_Id, int.Parse(SESSION_ORDER_TASK.ToString().ToString()), int.Parse(dtget_enteredsub.Rows[0]["Task_Confirm_Sub_Id"].ToString()), int.Parse(dtget_child.Rows[0]["Task_Confirm_Child_Id"].ToString()), "Child", "Pop_Old");
                            chk.Show();


                        }
                        else
                        {



                            Check_List_Count = int.Parse(dtcecklist.Rows.Count.ToString());
                            Order_Check_List chk = new Order_Check_List(int.Parse(dtcecklist.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_Id, int.Parse(SESSION_ORDER_TASK.ToString().ToString()), 0, 0, "Parent", "Pop_New");
                            chk.Show();

                        }

                    }

                    else
                    {
                        Hashtable htget_sub = new Hashtable();
                        DataTable dtget_sub = new DataTable();
                        htget_sub.Add("@Trans", "GET_ALL_SUB_QUESION_ON_TASK_CONFIRM_ID");
                        htget_sub.Add("@Task_Confirm_Id", dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString());
                        htget_sub.Add("@Order_ID", Order_Id);
                        htget_sub.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
                        dtget_sub = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_sub);

                        if (dtget_sub.Rows.Count > 0)
                        {

                            Order_Check_List chk = new Order_Check_List(int.Parse(dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_Id, int.Parse(SESSION_ORDER_TASK.ToString().ToString()), int.Parse(dtget_sub.Rows[0]["Task_Confirm_Sub_Id"].ToString()), 0, "Sub", "Pop_Old");
                            chk.Show();
                        }
                    }


                }

            }





        }

        private bool validate_Email_Sent()
        {


            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);
            string Check_Task = ddl_order_Task.Text.ToString();
            if (dt_Order_InTitleLogy.Rows.Count > 0 && Check_Task == "Upload Completed")
            {
                //if (Validate_Package_Uploaded() != false)
                //{
                Hashtable htcount = new Hashtable();
                DataTable dtcount = new DataTable();
                htcount.Add("@Trans", "CHECK_EMAIL_SENT_SUCESS");
                htcount.Add("@Order_Id", Order_Id);
                dtcount = dataaccess.ExecuteSP("Sp_Order_Email_Notification", htcount);

                if (dtcount.Rows.Count > 0)
                {
                    Email_Sent_Count = int.Parse(dtcount.Rows[0]["count"].ToString());

                }
                else
                {
                    Email_Sent_Count = 0;
                }

                if (Email_Sent_Count == 0)
                {

                    dialogResult = MessageBox.Show("Email is not Sent to Client, Resubmit it?", "Some Title", MessageBoxButtons.OK);
                    if (dialogResult == DialogResult.OK)
                    {
                        //cProbar.startProgress();
                        //form_loader.Start_progres();
                        //Send_Completed_Order_Email();
                        //cProbar.stopProgress();
                    }


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


        private bool Validate_Invoice_Genrated()
        {

            // Checking for Titlelogy vendor Db title Client Invoice is Genrated or not
            if (Client_id == 33)
            {

                Hashtable htin = new Hashtable();
                System.Data.DataTable dtin = new System.Data.DataTable();
                htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
                htin.Add("@Sub_Process_Id", Sub_ProcessId);
                dtin = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htin);
                if (dtin.Rows.Count > 0)
                {
                    Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

                }
                else
                {

                    Inv_Status = "False";
                }

                if (Inv_Status == "True")
                {
                    if (validate_Titlelogy_Invoice() != false && Validate_Titlelogy_Inovice_Page() != false)
                    {

                        // this method is commnented for not using

                        //  Genrate_Invoice_Titlelogy_Client_For_Db_Title();

                        //if (Client_id == 33 && Sub_ProcessId == 300)
                        //{


                        //    if (Order_Type_Id == 113 || Order_Type_Id == 116 || Order_Type_Id == 117 || Order_Type_Id == 119 || Order_Type_Id == 115 || Order_Type_Id == 114)
                        //    {
                        //        Hashtable ht_check = new Hashtable();
                        //        DataTable dt_check = new DataTable();

                        //        ht_check.Add("@Trans", "CHECK");
                        //        ht_check.Add("@Order_ID", External_Client_Order_Id);
                        //        dt_check = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_check);
                        //        int check = int.Parse(dt_check.Rows[0]["Count"].ToString());
                        //        if (check == 0)
                        //        {
                        //            Invoice_Search_Packake_Order = 0;
                        //            MessageBox.Show("Invoice is Not Genrated please Genrate Invoice");
                        //            return false;
                        //        }
                        //        else
                        //        {
                        //            Invoice_Search_Packake_Order = 1;
                        //            return true;
                        //        }
                        //    }
                        //    else
                        //    {

                        //        return true;
                        //    }
                        //}


                        Hashtable ht_check = new Hashtable();
                        DataTable dt_check = new DataTable();

                        ht_check.Add("@Trans", "CHECK");
                        ht_check.Add("@Order_ID", External_Client_Order_Id);
                        dt_check = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_check);
                        int check = int.Parse(dt_check.Rows[0]["Count"].ToString());
                        if (check == 0)
                        {
                            Invoice_Search_Packake_Order = 0;
                            MessageBox.Show("Invoice is Not Genrated please Genrate Invoice");
                            return false;
                        }
                        else
                        {
                            Invoice_Search_Packake_Order = 1;
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
            else
            {
                Search_Package_Order = 1;
                return true;
            }


        }

        private bool validate_Titlelogy_Invoice()
        {

            if (Client_id == 33 && Sub_ProcessId == 300)
            {


                if (Order_Type_Id == 113 || Order_Type_Id == 116 || Order_Type_Id == 117 || Order_Type_Id == 119 || Order_Type_Id == 115 || Order_Type_Id == 114)
                {


                    if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3" || SESSION_ORDER_TASK == "23" || SESSION_ORDER_TASK == "4" || SESSION_ORDER_TASK == "7")
                    {

                        if (chk_Plat_Yes.Checked == false && chk_plat_No.Checked == false)
                        {


                            chk_Plat_Yes.Focus();
                            Chk_Inv_Value = 1;
                            Invoice_Search_Packake_Order = 1;
                            MessageBox.Show("Please Check Plat Map");

                            return false;

                        }
                        else
                        {
                            Invoice_Search_Packake_Order = 0;
                            Chk_Inv_Value = 0;
                        }




                    }
                    if (SESSION_ORDER_TASK == "4" || SESSION_ORDER_TASK == "7" || SESSION_ORDER_TASK == "23" || SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                    {


                        if (chk_Tax_Yes.Checked == false && chk_Tax_No.Checked == false)
                        {

                            chk_Tax_Yes.Focus();
                            Chk_Inv_Value = 1;
                            Invoice_Search_Packake_Order = 1;
                            MessageBox.Show("Please Check Tax Information");

                            return false;

                        }
                        else
                        {
                            Invoice_Search_Packake_Order = 0;
                            Chk_Inv_Value = 0;
                        }


                    }


                    if (Chk_Inv_Value == 1)
                    {
                        Invoice_Search_Packake_Order = 1;
                        return false;
                    }
                    else
                    {
                        Invoice_Search_Packake_Order = 0;
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

        private bool Validate_Titlelogy_Inovice_Page()
        {
            Chk_Inv_Page = 0;
            if (Client_id == 33 && Sub_ProcessId == 300)
            {
                Chk_Inv_Page = 0;
                if (SESSION_ORDER_TASK == "23" || SESSION_ORDER_TASK == "24" || SESSION_ORDER_TASK == "12")
                {
                    if (Order_Type_Id == 113 || Order_Type_Id == 117 || Order_Type_Id == 115)
                    {
                        if (txt_Invoice_No_Of_Pages.Text == "")
                        {
                            Chk_Inv_Page = 1;
                            MessageBox.Show("Enter No Of Pages in Titlelogy Invoice");
                        }
                        if (txt_Platmap_Pages.Text == "")
                        {

                            Chk_Inv_Page = 2;
                            MessageBox.Show("Enter No Of Probate Pages");

                        }
                        if (txt_Probate_Pages.Text == "")
                        {
                            Chk_Inv_Page = 3;
                            MessageBox.Show("Enter No Of Plat Map Pages");

                        }
                    }
                    else
                    {
                        Chk_Inv_Page = 0;
                        return true;

                    }
                }

                if (Chk_Inv_Page == 1 || Chk_Inv_Page == 2 || Chk_Inv_Page == 3)
                {
                    Invoice_Search_Packake_Order = 1;
                    return false;
                }
                else
                {
                    Invoice_Search_Packake_Order = 0;
                    return true;
                }

            }
            else
            {

                return true;
            }



        }

        // For 2500 
        private void Insert_Internal_Tax_Order_Status(int Tax_Task_Id)
        {
            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", Order_Id);
            httax.Add("@Order_Task", 22);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", Tax_Task_Id);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);


            Hashtable htupdate = new Hashtable();
            System.Data.DataTable dtupdate = new System.Data.DataTable();
            htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
            htupdate.Add("@Order_ID", Order_Id);
            htupdate.Add("@Search_Tax_Request", "True");

            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

            Hashtable httaxupdate = new Hashtable();
            System.Data.DataTable dttaxupdate = new System.Data.DataTable();
            httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
            httaxupdate.Add("@Order_ID", Order_Id);
            httaxupdate.Add("@Search_Tax_Request_Progress", 14);

            dttaxupdate = dataaccess.ExecuteSP("Sp_Order", httaxupdate);


        }



        private bool Validate_Invoice_Genrated_Document_Uploaded()
        {
            // this is for Db title Vendor and Client

            Hashtable htin = new Hashtable();
            System.Data.DataTable dtin = new System.Data.DataTable();
            htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
            htin.Add("@Sub_Process_Id", Sub_ProcessId);
            dtin = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htin);
            if (dtin.Rows.Count > 0)
            {
                Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

            }
            else
            {

                Inv_Status = "False";
            }

            if (Inv_Status == "True")
            {




                if (Client_id == 33 && Sub_ProcessId == 300)
                {


                    if (Order_Type_Id == 113 || Order_Type_Id == 116 || Order_Type_Id == 117 || Order_Type_Id == 119 || Order_Type_Id == 115 || Order_Type_Id == 114)
                    {

                        Hashtable ht_check = new Hashtable();
                        DataTable dt_check = new DataTable();

                        ht_check.Add("@Trans", "GET_EXTERNAL_INVOICE_DOCUMENT_ID");
                        ht_check.Add("@Order_Id", External_Client_Order_Id);
                        dt_check = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", ht_check);

                        if (dt_check.Rows.Count > 0)
                        {
                            invoice_check = int.Parse(dt_check.Rows[0]["Count"].ToString());
                        }
                        if (invoice_check == 0)
                        {
                            Invoice_Package = 0;
                            Invoice_Search_Packake_Order = 0;
                            MessageBox.Show("Invoice File is not uploaded");
                            return false;
                        }
                        else
                        {
                            Invoice_Package = 1;
                            Invoice_Search_Packake_Order = 1;
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

                    Hashtable ht_check = new Hashtable();
                    DataTable dt_check = new DataTable();

                    ht_check.Add("@Trans", "GET_EXTERNAL_INVOICE_DOCUMENT_ID");
                    ht_check.Add("@Order_Id", External_Client_Order_Id);
                    dt_check = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", ht_check);

                    if (dt_check.Rows.Count > 0)
                    {
                        invoice_check = int.Parse(dt_check.Rows[0]["Count"].ToString());
                    }
                    if (invoice_check == 0)
                    {
                        Invoice_Package = 0;
                        Invoice_Search_Packake_Order = 0;
                        MessageBox.Show("Invoice File is not uploaded");
                        return false;
                    }
                    else
                    {
                        Invoice_Package = 1;
                        Invoice_Search_Packake_Order = 1;
                        return true;
                    }
                }





            }
            else
            {

                return true;
            }

        }

        // This is Genrating the Invoice For 

        // this method is not using
        private void Genrate_Invoice_Titlelogy_Client_For_Db_Title()
        {

            if (External_Client_Order_Id != 0)
            {

                Hashtable htin = new Hashtable();
                System.Data.DataTable dtin = new System.Data.DataTable();
                htin.Add("@Trans", "CHECK_INVOICE_ENABLED_DISABLED");
                htin.Add("@Sub_Process_Id", Sub_ProcessId);
                dtin = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htin);
                if (dtin.Rows.Count > 0)
                {
                    Inv_Status = dtin.Rows[0]["Invoice_Status"].ToString();

                }
                else
                {

                    Inv_Status = "False";
                }

                if (Inv_Status == "True")
                {


                    if (Sub_ProcessId == 300)
                    {
                        if (SESSION_ORDER_TASK == "23" || SESSION_ORDER_TASK == "24" || Title_Logy_Order_Task_Id == 15)
                        {

                            if (Order_Type_Id == 113 || Order_Type_Id == 115 || Order_Type_Id == 116 || Order_Type_Id == 117 || Order_Type_Id == 119 || Order_Type_Id == 121 || Order_Type_Id == 123 || Order_Type_Id == 130 || Order_Type_Id == 133 || Order_Type_Id == 134 || Order_Type_Id == 135 || Order_Type_Id == 136)
                            {
                                Hashtable htget_Invoice_Details_Order_Type_Wise = new Hashtable();
                                DataTable dtget_Invoice_Details_by_Order_Type_Wise = new DataTable();



                                htget_Invoice_Details_Order_Type_Wise.Add("@Trans", "GET_ORDER_COST_BY_CLIENT_ORDER_TYPE_WISE");
                                htget_Invoice_Details_Order_Type_Wise.Add("@Client_Id", Client_id);
                                htget_Invoice_Details_Order_Type_Wise.Add("@Subprocess_ID", Sub_ProcessId);
                                htget_Invoice_Details_Order_Type_Wise.Add("@Order_Type_Id", Order_Type_Id);

                                dtget_Invoice_Details_by_Order_Type_Wise = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htget_Invoice_Details_Order_Type_Wise);

                                if (dtget_Invoice_Details_by_Order_Type_Wise.Rows.Count > 0)
                                {


                                    Titelogy_Order_Type_Wise_Invoice_Amount = decimal.Parse(dtget_Invoice_Details_by_Order_Type_Wise.Rows[0]["Order_Cost"].ToString());



                                }
                                else if (dtget_Invoice_Details_by_Order_Type_Wise.Rows.Count <= 0)
                                {
                                    htget_Invoice_Details_Order_Type_Wise.Clear();

                                    htget_Invoice_Details_Order_Type_Wise.Add("@Trans", "GET_CLIENT_ORDER_COST");
                                    htget_Invoice_Details_Order_Type_Wise.Add("@Client_Id", Client_id);
                                    htget_Invoice_Details_Order_Type_Wise.Add("@Subprocess_ID", Sub_ProcessId);
                                    htget_Invoice_Details_Order_Type_Wise.Add("@state_Id", State_Id);
                                    htget_Invoice_Details_Order_Type_Wise.Add("@County_Id", County_Id);
                                    htget_Invoice_Details_Order_Type_Wise.Add("@Order_Type_Id", Order_Type_Id);

                                    dtget_Invoice_Details_by_Order_Type_Wise = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htget_Invoice_Details_Order_Type_Wise);

                                    if (dtget_Invoice_Details_by_Order_Type_Wise.Rows.Count > 0)
                                    {


                                        Titelogy_Order_Type_Wise_Invoice_Amount = decimal.Parse(dtget_Invoice_Details_by_Order_Type_Wise.Rows[0]["Order_Cost"].ToString());



                                    }
                                }
                                else
                                {


                                    Titelogy_Order_Type_Wise_Invoice_Amount = 0;

                                    MessageBox.Show("Invoice Not Genrated kindly check with Administrator");

                                    return;

                                }



                                if (Titelogy_Order_Type_Wise_Invoice_Amount > 0)
                                {
                                    Create_Order_Invoice_Entry();

                                }
                                else
                                {
                                    Titelogy_Order_Type_Wise_Invoice_Amount = 0;


                                }
                            }
                        }
                        else
                        {

                            Titelogy_Order_Type_Wise_Invoice_Amount = 0;
                        }
                    }
                    else
                    {
                        Titelogy_Order_Type_Wise_Invoice_Amount = 0;


                        Hashtable htcheck_in_Invoice_Master = new Hashtable();
                        DataTable dtcheck_Invoice_Master = new DataTable();
                        htcheck_in_Invoice_Master.Add("@Trans", "CHECK_BY_STATE_COUNTY_CLIENT_WISE");
                        htcheck_in_Invoice_Master.Add("@Client_Id", Client_id);
                        htcheck_in_Invoice_Master.Add("@Subprocess_ID", Sub_ProcessId);
                        htcheck_in_Invoice_Master.Add("@state_Id", State_Id);
                        htcheck_in_Invoice_Master.Add("@County_Id", County_Id);
                        htcheck_in_Invoice_Master.Add("@Order_Type_Id", Order_Type_Id);
                        dtcheck_Invoice_Master = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htcheck_in_Invoice_Master);

                        if (dtcheck_Invoice_Master.Rows.Count > 0)
                        {

                            Invoice_Check_For_Condition = int.Parse(dtcheck_Invoice_Master.Rows[0]["count"].ToString());
                        }
                        else
                        {
                            Invoice_Check_For_Condition = 0;

                        }


                        if (Invoice_Check_For_Condition > 0)
                        {

                            Create_Order_Invoice_Entry();

                        }
                        else if (Invoice_Check_For_Condition == 0)
                        {

                            Hashtable ht_check = new Hashtable();
                            DataTable dt_check = new DataTable();

                            ht_check.Add("@Trans", "CHECK");
                            ht_check.Add("@Order_ID", External_Client_Order_Id);
                            dt_check = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_check);

                            if (dt_check.Rows.Count > 0)
                            {
                                Check_Invoice_gen = int.Parse(dt_check.Rows[0]["Count"].ToString());
                            }
                            else
                            {

                                Check_Invoice_gen = 0;
                            }

                            if (Check_Invoice_gen > 0)
                            {
                                Export_Report();

                            }

                        }




                    }
                }



            }




        }

        private void Create_Order_Invoice_Entry()
        {

            if (Validate_Titlelogy_Inovice_Page() != false)
            {

                Hashtable ht_Select_Order_Details = new Hashtable();
                DataTable dt_Select_Order_Details = new DataTable();

                ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_WISE");
                ht_Select_Order_Details.Add("@Order_ID", Order_Id);
                dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);

                if (dt_Select_Order_Details.Rows.Count > 0)
                {

                    External_Client_Id = int.Parse(dt_Select_Order_Details.Rows[0]["External_Client_Id"].ToString());
                    External_Sub_Client_Id = int.Parse(dt_Select_Order_Details.Rows[0]["External_Sub_Client_Id"].ToString());
                }


                Hashtable ht_max = new Hashtable();
                DataTable dt_max = new DataTable();
                ht_max.Add("@Trans", "GET_MAX_EXTERNAL_INVOICE_AUTO_NUMBER");
                ht_max.Add("@Client_Id", External_Client_Id);
                dt_max = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_max);

                if (dt_max.Rows.Count > 0)
                {
                    Autoinvoice_No = int.Parse(dt_max.Rows[0]["Invoice_Auto_No"].ToString());
                }

                Hashtable htmax_Invoice_No = new Hashtable();
                DataTable dtmax_invoice_No = new DataTable();
                htmax_Invoice_No.Add("@Trans", "GET_MAX_EXTERNAL_INVOICE_NUMBER");
                htmax_Invoice_No.Add("@Client_Id", External_Client_Id);
                dtmax_invoice_No = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htmax_Invoice_No);

                if (dtmax_invoice_No.Rows.Count > 0)
                {
                    Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
                }


                Hashtable htget_Order_cost = new Hashtable();
                DataTable dtget_Order_Cost = new DataTable();

                htget_Order_cost.Add("@Trans", "GET_CLIENT_ORDER_COST");
                htget_Order_cost.Add("@Client_Id", Client_id);
                htget_Order_cost.Add("@Subprocess_ID", Sub_ProcessId);
                htget_Order_cost.Add("@state_Id", State_Id);
                htget_Order_cost.Add("@County_Id", County_Id);
                htget_Order_cost.Add("@Order_Type_Id", Order_Type_Id);

                dtget_Order_Cost = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htget_Order_cost);

                if (dtget_Order_Cost.Rows.Count > 0)
                {

                    invoice_Search_Cost = Convert.ToDecimal(dtget_Order_Cost.Rows[0]["Order_Cost"].ToString());
                }
                else
                {

                    invoice_Search_Cost = 0;

                }

                Invoice_Copy_Cost = 0;


                Title_No_Of_Pages = 0;


                Hashtable ht_check = new Hashtable();
                DataTable dt_check = new DataTable();

                ht_check.Add("@Trans", "CHECK");
                ht_check.Add("@Order_ID", External_Client_Order_Id);
                dt_check = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_check);
                int check = int.Parse(dt_check.Rows[0]["Count"].ToString());
                if (check == 0)
                {
                    if (Titelogy_Order_Type_Wise_Invoice_Amount == 0)
                    {

                        Hashtable ht_insert = new Hashtable();
                        DataTable dt_insert = new DataTable();

                        ht_insert.Add("@Trans", "INSERT");
                        ht_insert.Add("@Client_Id", External_Client_Id);
                        ht_insert.Add("@Order_ID", External_Client_Order_Id);
                        ht_insert.Add("@Subprocess_ID", External_Sub_Client_Id);
                        ht_insert.Add("@Invoice_Auto_No", Autoinvoice_No);
                        ht_insert.Add("@Invoice_No", Invoice_Number);
                        ht_insert.Add("@Order_Cost", Invoice_Order_Cost);
                        ht_insert.Add("@Search_Cost", invoice_Search_Cost);
                        ht_insert.Add("@Copy_Cost", Invoice_Copy_Cost);
                        ht_insert.Add("@No_Of_Pages", No_Of_Pages);

                        Hashtable htget_est_time = new Hashtable();
                        DataTable dtget_est_time = new DataTable();

                        htget_est_time.Add("@Trans", "GET_PST_TIME");
                        dtget_est_time = dataaccess.ExecuteSP("Sp_External_Client_Orders", htget_est_time);


                        ht_insert.Add("@Invoice_Date", dtget_est_time.Rows[0]["Date"].ToString());
                        ht_insert.Add("@Production_Unit_Type", 1);
                        ht_insert.Add("@Status", "True");
                        ht_insert.Add("@Inserted_By", userid);

                        dt_insert = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_insert);

                        Export_Report();
                        MessageBox.Show("Invoice Genrated Sucessfully");
                    }
                    else
                    {
                        // this is for DB title-Peak Title Order Type Wise Amount 


                        invoice_Search_Cost = Titelogy_Order_Type_Wise_Invoice_Amount;

                        Hashtable ht_insert = new Hashtable();
                        DataTable dt_insert = new DataTable();


                        if (txt_Invoice_No_Of_Pages.Text != "")
                        {
                            Title_Peak_Inv_No_Of_Pages = int.Parse(txt_Invoice_No_Of_Pages.Text);

                        }
                        else
                        {
                            Title_Peak_Inv_No_Of_Pages = 0;


                        }

                        if (txt_Platmap_Pages.Text != "")
                        {

                            Title_Peak_Inv_No_Plat_Map_Pages = int.Parse(txt_Platmap_Pages.Text);
                        }
                        else
                        {
                            Title_Peak_Inv_No_Plat_Map_Pages = 0;

                        }

                        if (txt_Probate_Pages.Text != "")
                        {

                            Title_Peak_Inv_No_Probate_Pages = int.Parse(txt_Probate_Pages.Text);
                        }
                        else
                        {
                            Title_Peak_Inv_No_Probate_Pages = 0;

                        }

                        // this is the Requiremnet above page 15 means *** No. of copies: 46 (46-15 = 31 copies will be billed).  Per copy cost $0.50 *31 =  $15.50 ***
                        if (Title_Peak_Inv_No_Of_Pages > 15)
                        {

                            Invoice_Copy_Cost = Convert.ToDecimal((Title_Peak_Inv_No_Of_Pages - 15) * (0.50));


                        }
                        else
                        {

                            Invoice_Copy_Cost = 0;
                        }


                        // Plat map And Probate Price Are 1$

                        Title_Logy_Probate_Cost = Title_Peak_Inv_No_Probate_Pages * 1;

                        Title_Logy_Platmap_Cost = Title_Peak_Inv_No_Plat_Map_Pages * 1;




                        // Total Order Cost= As per the Require Ment ----Search Cost + Copycost + sum(Probate+Plat Map) = $187.50


                        Total_Titlelogy_Order_Cost = (invoice_Search_Cost + Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost);

                        Invoice_Copy_Cost = Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost;




                        ht_insert.Add("@Trans", "INSERT");
                        ht_insert.Add("@Client_Id", External_Client_Id);
                        ht_insert.Add("@Order_ID", External_Client_Order_Id);
                        ht_insert.Add("@Subprocess_ID", External_Sub_Client_Id);
                        ht_insert.Add("@Invoice_Auto_No", Autoinvoice_No);
                        ht_insert.Add("@Invoice_No", Invoice_Number);
                        ht_insert.Add("@Order_Cost", Total_Titlelogy_Order_Cost);
                        ht_insert.Add("@Search_Cost", invoice_Search_Cost);
                        ht_insert.Add("@Copy_Cost", Invoice_Copy_Cost);
                        if (Title_Peak_Inv_No_Of_Pages > 0)
                        {
                            ht_insert.Add("@No_Of_Pages", Title_Peak_Inv_No_Of_Pages);
                        }
                        else
                        {
                            ht_insert.Add("@No_Of_Pages", No_Of_Pages);

                        }
                        //   ht_insert.Add("@Invoice_No_Of_Page", No_Of_Pages);
                        ht_insert.Add("@Probate_Pages", Title_Peak_Inv_No_Probate_Pages);
                        ht_insert.Add("@Plat_Map_Pages", Title_Peak_Inv_No_Plat_Map_Pages);
                        ht_insert.Add("@Probate_Cost", Title_Logy_Probate_Cost);
                        ht_insert.Add("@Plat_Map_Cost", Title_Logy_Platmap_Cost);


                        Hashtable htget_est_time = new Hashtable();
                        DataTable dtget_est_time = new DataTable();

                        htget_est_time.Add("@Trans", "GET_PST_TIME");
                        dtget_est_time = dataaccess.ExecuteSP("Sp_External_Client_Orders", htget_est_time);


                        ht_insert.Add("@Invoice_Date", dtget_est_time.Rows[0]["Date"].ToString());
                        ht_insert.Add("@Production_Unit_Type", 1);
                        ht_insert.Add("@Status", "True");
                        ht_insert.Add("@Inserted_By", userid);

                        dt_insert = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_insert);

                        Export_Report();
                        MessageBox.Show("Invoice Genrated Sucessfully");
                    }

                }
                else
                {

                    if (Titelogy_Order_Type_Wise_Invoice_Amount == 0)
                    {
                        // this is for All 32000 Clients

                        // This Is Commenting Because Invoice Updating to Zero
                        //Hashtable ht_Update = new Hashtable();
                        //DataTable dt_Update = new DataTable();

                        //ht_Update.Add("@Trans", "UPDATE");
                        //ht_Update.Add("@Order_ID", External_Client_Order_Id);
                        //ht_Update.Add("@Invoice_No", Invoice_Number);
                        //ht_Update.Add("@Order_Cost", Invoice_Order_Cost);
                        //ht_Update.Add("@Search_Cost", invoice_Search_Cost);
                        //ht_Update.Add("@Copy_Cost", Invoice_Copy_Cost);
                        //ht_Update.Add("@No_Of_Pages", No_Of_Pages);
                        //Hashtable htget_est_time = new Hashtable();
                        //DataTable dtget_est_time = new DataTable();

                        //htget_est_time.Add("@Trans", "GET_PST_TIME");
                        //dtget_est_time = dataaccess.ExecuteSP("Sp_External_Client_Orders", htget_est_time);

                        //ht_Update.Add("@Invoice_Date", dtget_est_time.Rows[0]["Date"].ToString());

                        //ht_Update.Add("@Modified_By", userid);

                        //dt_Update = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_Update);
                        Export_Report();
                        MessageBox.Show("Invoice Updated Sucessfully");

                    }
                    else if (Titelogy_Order_Type_Wise_Invoice_Amount > 0)
                    {

                        invoice_Search_Cost = Titelogy_Order_Type_Wise_Invoice_Amount;

                        Hashtable ht_insert = new Hashtable();
                        DataTable dt_insert = new DataTable();


                        if (txt_Invoice_No_Of_Pages.Text != "")
                        {
                            Title_Peak_Inv_No_Of_Pages = int.Parse(txt_Invoice_No_Of_Pages.Text);

                        }
                        else
                        {
                            Title_Peak_Inv_No_Of_Pages = 0;


                        }

                        if (txt_Platmap_Pages.Text != "")
                        {

                            Title_Peak_Inv_No_Plat_Map_Pages = int.Parse(txt_Platmap_Pages.Text);
                        }
                        else
                        {
                            Title_Peak_Inv_No_Plat_Map_Pages = 0;

                        }

                        if (txt_Probate_Pages.Text != "")
                        {

                            Title_Peak_Inv_No_Probate_Pages = int.Parse(txt_Probate_Pages.Text);
                        }
                        else
                        {
                            Title_Peak_Inv_No_Probate_Pages = 0;

                        }

                        // this is the Requiremnet above page 15 means *** No. of copies: 46 (46-15 = 31 copies will be billed).  Per copy cost $0.50 *31 =  $15.50 ***
                        if (Title_Peak_Inv_No_Of_Pages > 15)
                        {

                            Invoice_Copy_Cost = Convert.ToDecimal((Title_Peak_Inv_No_Of_Pages - 15) * (0.50));


                        }
                        else
                        {

                            Invoice_Copy_Cost = 0;
                        }


                        // Plat map And Probate Price Are 1$

                        Title_Logy_Probate_Cost = Title_Peak_Inv_No_Probate_Pages * 1;

                        Title_Logy_Platmap_Cost = Title_Peak_Inv_No_Plat_Map_Pages * 1;




                        // Total Order Cost= As per the Require Ment ----Search Cost + Copycost + sum(Probate+Plat Map) = $187.50


                        Total_Titlelogy_Order_Cost = (invoice_Search_Cost + Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost);

                        Invoice_Copy_Cost = Invoice_Copy_Cost + Title_Logy_Probate_Cost + Title_Logy_Platmap_Cost;

                        // this is for 32700 SUb CLient
                        ht_insert.Add("@Trans", "UPDATE_SUB_CLIENT_WISE");
                        ht_insert.Add("@Order_ID", External_Client_Order_Id);
                        ht_insert.Add("@Order_Cost", Total_Titlelogy_Order_Cost);
                        ht_insert.Add("@Search_Cost", invoice_Search_Cost);
                        ht_insert.Add("@Copy_Cost", Invoice_Copy_Cost);
                        if (Title_Peak_Inv_No_Of_Pages > 0)
                        {
                            ht_insert.Add("@No_Of_Pages", Title_Peak_Inv_No_Of_Pages);
                        }
                        else
                        {
                            ht_insert.Add("@No_Of_Pages", No_Of_Pages);

                        }
                        //   ht_insert.Add("@Invoice_No_Of_Page", No_Of_Pages);
                        ht_insert.Add("@Probate_Pages", Title_Peak_Inv_No_Probate_Pages);
                        ht_insert.Add("@Plat_Map_Pages", Title_Peak_Inv_No_Plat_Map_Pages);
                        ht_insert.Add("@Probate_Cost", Title_Logy_Probate_Cost);
                        ht_insert.Add("@Plat_Map_Cost", Title_Logy_Platmap_Cost);

                        Hashtable htget_est_time = new Hashtable();
                        DataTable dtget_est_time = new DataTable();

                        htget_est_time.Add("@Trans", "GET_PST_TIME");
                        dtget_est_time = dataaccess.ExecuteSP("Sp_External_Client_Orders", htget_est_time);
                        ht_insert.Add("@Invoice_Date", dtget_est_time.Rows[0]["Date"].ToString());
                        ht_insert.Add("@Modified_By", userid);
                        dt_insert = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_insert);

                        Export_Report();
                        MessageBox.Show("Invoice Updated Sucessfully");

                    }


                }
            }
        }


        // Db-Title unser Subclient Peak Title
        private void Create_Order_Invoice_Entry_Peak_Title_Client()
        {


            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_WISE");
            ht_Select_Order_Details.Add("@Order_ID", Order_Id);
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {

                External_Client_Id = int.Parse(dt_Select_Order_Details.Rows[0]["External_Client_Id"].ToString());
                External_Sub_Client_Id = int.Parse(dt_Select_Order_Details.Rows[0]["External_Sub_Client_Id"].ToString());
            }


            Hashtable ht_max = new Hashtable();
            DataTable dt_max = new DataTable();
            ht_max.Add("@Trans", "GET_MAX_EXTERNAL_INVOICE_AUTO_NUMBER");
            ht_max.Add("@Client_Id", External_Client_Id);
            dt_max = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_max);

            if (dt_max.Rows.Count > 0)
            {
                Autoinvoice_No = int.Parse(dt_max.Rows[0]["Invoice_Auto_No"].ToString());
            }

            Hashtable htmax_Invoice_No = new Hashtable();
            DataTable dtmax_invoice_No = new DataTable();
            htmax_Invoice_No.Add("@Trans", "GET_MAX_EXTERNAL_INVOICE_NUMBER");
            htmax_Invoice_No.Add("@Client_Id", External_Client_Id);
            dtmax_invoice_No = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htmax_Invoice_No);

            if (dtmax_invoice_No.Rows.Count > 0)
            {
                Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
            }




            invoice_Search_Cost = Titelogy_Order_Type_Wise_Invoice_Amount;

            Title_Peak_Inv_No_Of_Pages = int.Parse(txt_Invoice_No_Of_Pages.Text.ToString());
            if (Title_Peak_Inv_No_Of_Pages > 15)
            {



            }



            Invoice_Copy_Cost = 0;


            Title_No_Of_Pages = 0;


            Hashtable ht_check = new Hashtable();
            DataTable dt_check = new DataTable();

            ht_check.Add("@Trans", "CHECK");
            ht_check.Add("@Order_ID", External_Client_Order_Id);
            dt_check = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_check);
            int check = int.Parse(dt_check.Rows[0]["Count"].ToString());
            if (check == 0)
            {
                Hashtable ht_insert = new Hashtable();
                DataTable dt_insert = new DataTable();

                ht_insert.Add("@Trans", "INSERT");
                ht_insert.Add("@Client_Id", External_Client_Id);
                ht_insert.Add("@Order_ID", External_Client_Order_Id);
                ht_insert.Add("@Subprocess_ID", External_Sub_Client_Id);
                ht_insert.Add("@Invoice_Auto_No", Autoinvoice_No);
                ht_insert.Add("@Invoice_No", Invoice_Number);
                ht_insert.Add("@Order_Cost", Invoice_Order_Cost);
                ht_insert.Add("@Search_Cost", invoice_Search_Cost);
                ht_insert.Add("@Copy_Cost", Invoice_Copy_Cost);
                ht_insert.Add("@No_Of_Pages", No_Of_Pages);

                Hashtable htget_est_time = new Hashtable();
                DataTable dtget_est_time = new DataTable();

                htget_est_time.Add("@Trans", "GET_PST_TIME");
                dtget_est_time = dataaccess.ExecuteSP("Sp_External_Client_Orders", htget_est_time);


                ht_insert.Add("@Invoice_Date", dtget_est_time.Rows[0]["Date"].ToString());
                ht_insert.Add("@Production_Unit_Type", 1);
                ht_insert.Add("@Status", "True");
                ht_insert.Add("@Inserted_By", userid);

                dt_insert = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_insert);

                Export_Report();
                MessageBox.Show("Invoice Genrated Sucessfully");


            }
            else
            {


                Export_Report();

                //Hashtable ht_Update = new Hashtable();
                //DataTable dt_Update = new DataTable();

                //ht_Update.Add("@Trans", "UPDATE");
                //ht_Update.Add("@Client_Id", External_Client_Id);
                //ht_Update.Add("@Order_ID", External_Client_Order_Id);
                //ht_Update.Add("@Subprocess_ID", External_Sub_Client_Id);
                //ht_Update.Add("@Invoice_Auto_No", Autoinvoice_No);
                //ht_Update.Add("@Invoice_No", Invoice_Number);
                //ht_Update.Add("@Order_Cost", Invoice_Order_Cost);
                //ht_Update.Add("@Search_Cost", invoice_Search_Cost);
                //ht_Update.Add("@Copy_Cost", Invoice_Copy_Cost);
                //ht_Update.Add("@No_Of_Pages", No_Of_Pages);
                //Hashtable htget_est_time = new Hashtable();
                //DataTable dtget_est_time = new DataTable();

                //htget_est_time.Add("@Trans", "GET_PST_TIME");
                //dtget_est_time = dataaccess.ExecuteSP("Sp_External_Client_Orders", htget_est_time);



                //ht_Update.Add("@Invoice_Date", dtget_est_time.Rows[0]["Date"].ToString());
                //ht_Update.Add("@Inhouse_Search_Cost", 0);
                //ht_Update.Add("@Inhouse_Copy_Cost", 0);
                //ht_Update.Add("@Production_Unit_Type", 1);
                //ht_Update.Add("@Inhouse_No_Pages", 0);
                //ht_Update.Add("@Status", "True");
                //ht_Update.Add("@Modified_By", userid);

                //dt_Update = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht_Update);
                //MessageBox.Show("Invoice Updated Sucessfully");

            }
        }


        private bool Validate_Package_Uploaded()
        {


            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();
            if (Client_id != 33)
            {
                htcount.Add("@Trans", "CHECK_ORDER_PACKAGE_UPLOADED");
            }
            else
            {
                htcount.Add("@Trans", "CHECK_SEARCH_PACKAGE");

            }

            htcount.Add("@Order_Id", External_Client_Order_Id);
            dtcount = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htcount);

            if (dtcount.Rows.Count > 0)
            {
                Package_Count = int.Parse(dtcount.Rows[0]["count"].ToString());

            }
            else
            {
                Package_Count = 0;

            }

            if (Package_Count == 0)
            {
                Search_Package_Order = 0;
                Invoice_Search_Packake_Order = 0;
                MessageBox.Show("Search Package is not uploaded or File is Not Checked in Titlelogy Doucment Tab");
                return false;

            }
            else
            {
                Invoice_Search_Packake_Order = 1;
                Search_Package_Order = 1;
                return true;
            }

        }

        // This is for client==33 and Subprocess==300
        private bool Validate_Report_File()
        {

            if (Sub_ProcessId == 300)
            {
                Hashtable htcount = new Hashtable();
                DataTable dtcount = new DataTable();

                htcount.Add("@Trans", "CHECK_REPORT_FILE");
                htcount.Add("@Order_Id", External_Client_Order_Id);
                dtcount = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htcount);

                if (dtcount.Rows.Count > 0)
                {
                    Package_Count = int.Parse(dtcount.Rows[0]["count"].ToString());

                }
                else
                {
                    Package_Count = 0;

                }

                if (Package_Count == 0)
                {

                    Search_Package_Order = 0;
                    MessageBox.Show("Report File is not uploaded or File is Not Checked in Titlelogy Doucment Tab");
                    return false;
                }
                else
                {

                    Search_Package_Order = 1;
                    return true;

                }
            }
            else

            {

                Search_Package_Order = 1;
                return true;


            }

        }


        private void Insert_External_Client_Order_Production_Date()
        {


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();

            htcheck.Add("@Trans", "CHK_PRODUCTION_DATE");
            htcheck.Add("@External_Order_Id", External_Client_Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htcheck);

            int check;
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

                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new DataTable();
                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@External_Order_Id", External_Client_Order_Id);
                htinsert.Add("@Order_Task", 15);
                htinsert.Add("@Order_Status", 3);
                htinsert.Add("@Inserted_By", userid);
                dtinsert = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htinsert);
            }
            else if (check > 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new DataTable();
                htinsert.Add("@Trans", "UPDATE");
                htinsert.Add("@External_Order_Id", External_Client_Order_Id);
                htinsert.Add("@Order_Task", 15);
                htinsert.Add("@Order_Status", 3);
                htinsert.Add("@Inserted_By", userid);
                dtinsert = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htinsert);


            }









        }


        public void Export_Report()
        {
            // this is only for Titlelogy Db title vendor and Client
            if (Client_id == 33 && Sub_ProcessId != 263)
            {
                rptDoc = new InvoiceRep.InvReport.InvoiceReport_DbTitle();
                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_Id", External_Client_Order_Id);




                Logon_To_Crystal();
                rptDoc.SetParameterValue("@Order_ID", External_Client_Order_Id);
                ExportOptions CrExportOptions;
                string Invoice_Order_Number = External_Client_Order_Number.ToString();
                string Source = @"\\192.168.12.33\Invoice-Reports\Titlelogy_Invoice.pdf";

                string File_Name = "" + External_Client_Order_Number + " - Invoice.pdf";
                //string Docname = FName[FName.Length - 1].ToString();
                string dest_path1 = @"\\192.168.12.33\Titlelogy\" + External_Client_Id + @"\" + External_Sub_Client_Id + @"\" + External_Client_Order_Number + @"\" + File_Name;
                DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                de.Username = "administrator";
                de.Password = "password1$";


                Directory.CreateDirectory(@"\\192.168.12.33\Titlelogy\" + External_Client_Id + @"\" + External_Sub_Client_Id + @"\" + External_Client_Order_Number);
                extension = Path.GetExtension(File_Name);
                File.Copy(Source, dest_path1, true);


                Hashtable htpath = new Hashtable();
                System.Data.DataTable dtpath = new System.Data.DataTable();

                Hashtable htcheck = new Hashtable();
                System.Data.DataTable dtcheck = new System.Data.DataTable();
                htcheck.Add("@Trans", "CHECK_INVOICE_FILE");
                htcheck.Add("@Order_Id", External_Client_Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htcheck);
                int check;
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


                    htpath.Add("@Trans", "INSERT");
                    htpath.Add("@Document_Type_Id", 12);
                    htpath.Add("@Order_Id", External_Client_Order_Id);
                    htpath.Add("@Document_From", 2);
                    htpath.Add("@Document_File_Type", extension.ToString());
                    htpath.Add("@Description", "Invoice");
                    htpath.Add("@Document_Path", dest_path1);
                    htpath.Add("@File_Size", File_size);

                    htpath.Add("@Inserted_date", DateTime.Now);
                    htpath.Add("@status", "True");
                    dtpath = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htpath);

                }

                Hashtable htgetpath = new Hashtable();
                System.Data.DataTable dtgetpath = new System.Data.DataTable();
                htgetpath.Add("@Trans", "GET_PATH");
                htgetpath.Add("@Order_Id", External_Client_Order_Id);
                dtgetpath = dataaccess.ExecuteSP("Sp_External_Client_Orders_Documents", htgetpath);

                if (dtgetpath.Rows.Count > 0)
                {
                    View_File_Path = dtgetpath.Rows[0]["Document_Path"].ToString();
                }
                FileInfo newFile = new FileInfo(View_File_Path);

                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();

                PdfFormatOptions CrFormatTypeOptions = new PdfFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = newFile.ToString();
                CrExportOptions = rptDoc.ExportOptions;
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                rptDoc.Export();

            }



        }

        public void Logon_To_Crystal()
        {
            List<string> cl_Lgoin = Comclass.Crystal_report_Login();
            crConnectionInfo.ServerName = cl_Lgoin[0].ToString();
            crConnectionInfo.DatabaseName = cl_Lgoin[1].ToString();
            crConnectionInfo.UserID = cl_Lgoin[2].ToString();
            crConnectionInfo.Password = cl_Lgoin[3].ToString();
            CrTables = rptDoc.Database.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            foreach (ReportDocument sr in rptDoc.Subreports)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in sr.Database.Tables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);

                }
            }


        }

        public void Get_Parent_Task_Confirmation_()
        {



            Hashtable htget_Parent = new Hashtable();
            DataTable dtget_Partent = new DataTable();
            htget_Parent.Add("@Trans", "GET_ORDER_WISE_TASK_ID");
            htget_Parent.Add("@Order_ID", Order_Id);
            htget_Parent.Add("@Order_Status_Id", int.Parse(SESSION_ORDER_TASK.ToString().ToString()));
            dtget_Partent = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_Parent);

            if (dtget_Partent.Rows.Count > 0)
            {

                for (int i = 0; i < dtget_Partent.Rows.Count; i++)
                {


                }
            }

        }

        protected void Update_User_Order_Time_Info_On_Cancel_Logout()
        {


            if (Work_Type_Id == 1)
            {

                MAX_TIME_ID = Max_Time_Id;
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date1 = new DateTime();
                date1 = DateTime.Now;
                string dateeval1 = date1.ToString("dd/MM/yyyy");
                string time1 = date1.ToString("hh:mm tt");

                htComments.Add("@Trans", "UPDATE_ON_LOGOUT");
                htComments.Add("@Order_Time_Id", MAX_TIME_ID);
                htComments.Add("@End_Time", date1);
                htComments.Add("@Open_Status", "False");
                dtComments = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htComments);



            }

        }
        protected void Clear()
        {

            ddl_order_Staus.SelectedIndex = 0;
            ddl_Issue_Category.SelectedIndex = 0;
            txt_Delay_Text.Text = "";
            txt_Comments.Text = "";
            //  txt_Notes.Text = "";
            ddl_Order_Source.SelectedIndex = 0;
            txt_Order_Abstractor_Cost.Text = "";
            txt_Order_Copy_Cost.Text = "";
            txt_Order_No_Of_Pages.Text = "";
            txt_Order_Search_Cost.Text = "";
        }






        protected void Update_User_Order_Time_Info()
        {


            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE_ON_TIME");
            htComments.Add("@Order_Time_Id", Pass_Max_Time_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htComments);

        }

        protected void Update_Rework_User_Order_Time_Info()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE_ON_TIME");
            htComments.Add("@Order_Time_Id", Pass_Max_Time_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_Rework_User_Wise_Time_Track", htComments);

        }


        protected void Update_Super_Qc_User_Order_Time_Info()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE_ON_TIME");
            htComments.Add("@Order_Time_Id", Pass_Max_Time_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_Super_Qc_User_Wise_Time_Track", htComments);

        }


        protected void Update_User_Order_Time_Info_Status()
        {


            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE");
            htComments.Add("@Order_Time_Id", Pass_Max_Time_Id);
            htComments.Add("@Order_Progress_Id", ddl_order_Staus.SelectedValue.ToString());
            dtComments = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htComments);

        }

        protected void Update_Rework_User_Order_Time_Info_Status()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE");
            htComments.Add("@Order_Time_Id", Pass_Max_Time_Id);
            htComments.Add("@Order_Progress_Id", ddl_order_Staus.SelectedValue.ToString());
            dtComments = dataaccess.ExecuteSP("Sp_Order_Rework_User_Wise_Time_Track", htComments);

        }


        protected void Update_Super_Qc_User_Order_Time_Info_Status()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE");
            htComments.Add("@Order_Time_Id", Pass_Max_Time_Id);
            htComments.Add("@Order_Progress_Id", ddl_order_Staus.SelectedValue.ToString());
            dtComments = dataaccess.ExecuteSP("Sp_Order_Super_Qc_User_Wise_Time_Track", htComments);

        }


        protected void Get_Rework_maximum_Time_Id()
        {
            Hashtable htTime = new Hashtable();
            DataTable dtTime = new System.Data.DataTable();

            htTime.Add("@Trans", "MAX_TIME_ID");
            htTime.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
            htTime.Add("@Order_Id", Order_Id);
            htTime.Add("@User_Id", userid);
            dtTime = dataaccess.ExecuteSP("Sp_Order_Rework_User_Wise_Time_Track", htTime);
            if (dtTime.Rows.Count > 0)
            {
                MAX_TIME_ID = int.Parse(dtTime.Rows[0]["MAX_TIME_ID"].ToString());


            }

        }

        protected void Get_Super_Qc_maximum_Time_Id()
        {
            Hashtable htTime = new Hashtable();
            DataTable dtTime = new System.Data.DataTable();

            htTime.Add("@Trans", "MAX_TIME_ID");
            htTime.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
            htTime.Add("@Order_Id", Order_Id);
            htTime.Add("@User_Id", userid);
            dtTime = dataaccess.ExecuteSP("Sp_Order_Super_Qc_User_Wise_Time_Track", htTime);
            if (dtTime.Rows.Count > 0)
            {
                MAX_TIME_ID = int.Parse(dtTime.Rows[0]["MAX_TIME_ID"].ToString());
                //  ViewState["MAX_TIME_ID"] = MAX_TIME_ID;

            }

        }


        //protected void Geydview_Bind_Notes()
        //{

        //    Hashtable htNotes = new Hashtable();
        //    DataTable dtNotes = new System.Data.DataTable();

        //    htNotes.Add("@Trans", "SELECT");
        //    htNotes.Add("@Order_Id", Order_Id);
        //    dtNotes = dataaccess.ExecuteSP("Sp_Order_Notes", htNotes);
        //    if (dtNotes.Rows.Count > 0)
        //    {
        //        //ex2.Visible = true;
        //        grd_Error.Visible = true;
        //        grd_Error.DataSource = dtNotes;


        //    }
        //    else
        //    {




        //    }


        //}
        private bool Validate_Order_Info()
        {

            if (ddl_order_Staus.SelectedIndex <= 0)
            {

                MessageBox.Show("Please Select Order Status");
                ddl_order_Staus.Focus();
                return false;
            }
            //if (txt_Order_No_Of_Pages.Text == "")
            //{
            //    MessageBox.Show("Please Enter No of Pages");
            //    txt_Order_No_Of_Pages.Focus();
            //    return false;

            //}
            if (txt_Effectivedate.Text == " ")
            {
                MessageBox.Show("Please Enter Effective Date");
                txt_Effectivedate.Focus();

                return false;


            }
            //if (ddl_Order_Source.SelectedIndex <= 0)
            //{

            //    MessageBox.Show("Please select Order Source");
            //    ddl_Order_Source.Focus();

            //    return false;
            //}


            else
            {
                return true;

            }


        }

        private bool validate_subscription()
        {


            if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
            {

                if (ddl_Order_Source.Text == "Subscription" && ddl_Web_search_sites.SelectedIndex <= 0)
                {

                    MessageBox.Show("Please Select Subscription Website Name");
                    ddl_Web_search_sites.Focus();
                    return false;
                }
                else if (txt_No_Of_Hits.Text == "")
                {
                    MessageBox.Show("Please Enter No Of Hits");
                    txt_No_Of_Hits.Focus();
                    return false;
                }

                else if (txt_No_of_documents.Text == "")
                {
                    MessageBox.Show("Please Enter No Of Documents");
                    txt_No_of_documents.Focus();
                    return false;
                }
                //else if (ddl_Order_Source.Text == "Data Trace" && txt_No_Of_Hits.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits of Data Tree");
                //    txt_No_Of_Hits.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 8 && txt_No_of_documents.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits of Data Tree");
                //    txt_No_of_documents.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 8 && txt_No_of_documents.Text == "" && txt_No_Of_Hits.Text == "")
                //{

                //    MessageBox.Show("Please Enter No Of Hits and documents of Data Tree");
                //    txt_No_Of_Hits.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 6 && txt_No_Of_Hits.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits of Data Trace");
                //    txt_No_Of_Hits.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 6 && txt_No_of_documents.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits of Data Trace");
                //    txt_No_of_documents.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 6 && txt_No_Of_Hits.Text == "" && txt_No_of_documents.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits and documents of Data Tree");
                //    txt_No_Of_Hits.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 7 && txt_No_Of_Hits.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits of Data Trace");
                //    txt_No_Of_Hits.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 7 && txt_No_of_documents.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits of Data Trace");
                //    txt_No_of_documents.Focus();
                //    return false;
                //}
                //else if (ddl_Order_Source.SelectedIndex == 7 && txt_No_Of_Hits.Text == "" && txt_No_of_documents.Text == "")
                //{
                //    MessageBox.Show("Please Enter No Of Hits and documents of Data Tree");
                //    txt_No_Of_Hits.Focus();
                //    return false;
                //}


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

        private bool validate_subscription_Website()
        {
            if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
            {
                int Website_User_PAssword_Id;

                if (ddl_Order_Source.Text == "Subscription" && ddl_Web_search_sites.SelectedIndex > 0)
                {
                    Website_User_PAssword_Id = int.Parse(ddl_Web_search_sites.SelectedValue.ToString());
                    if (Website_User_PAssword_Id == 43 && txt_Website.Text == "")
                    {
                        MessageBox.Show("Please Enter Subscription Website Name");
                        txt_Website.Focus();

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
        bool ReturnValue()
        {
            return false;
        }
        private bool Valid_date()
        {
            if (txt_Prdoductiondate.Text == " ")
            {
                MessageBox.Show("Production Date Enter Properly");
                return false;
            }
            if (ddl_order_Staus.Text == "COMPLETED" && ddl_order_Task.Text == "" && Work_Type_Id != 3 && lbl_Order_Task_Type.Text != "Search Tax Request")
            {
                MessageBox.Show("Please Select Next Task");
                ddl_order_Task.Focus();
                return false;

            }
            else
            {


                return true;
            }
        }
        private bool ValidateProductionDate()
        {
            DateTime dates = DateTime.Now;
            string dateeval1 = dates.ToString("MM/dd/yyyy");
            DateTime date1 = Convert.ToDateTime(dateeval1.ToString());

            if (txt_Prdoductiondate.Text != " ")
            {
                date2 = Convert.ToDateTime(txt_Prdoductiondate.Text);
            }
            int result = DateTime.Compare(date1, date2);

            if (result >= 0)
            {


                return true;
            }
            else
            {
                MessageBox.Show("Date Enter Properly");

                return false;
            }
        }

        private bool Validate_Effective_Date()
        {


            System.DateTime firstDate = DateTime.ParseExact(txt_Effectivedate.Text, "MM/dd/yyyy", null);

            string s_Date = DateTime.Now.ToString("MM/dd/yyyy");
            System.DateTime secondDate = DateTime.ParseExact(s_Date, "MM/dd/yyyy", null);

            System.TimeSpan diff = secondDate.Subtract(firstDate);
            System.TimeSpan diff1 = secondDate - firstDate;

            String diff2 = (firstDate - secondDate).TotalDays.ToString();

            decimal Datediff = Convert.ToDecimal(diff2.ToString());
            var roundate = Math.Round(Datediff, 0);
            if (roundate >= -1)
            {

                MessageBox.Show("Effectiv date Should Not be Greater than " + DateTime.Now.ToString() + " and Previous date");
                return false;
            }
            else
            {

                return true;
            }


        }
        protected void Insert_OrderComments()
        {

            if (txt_Comments.Text != "")
            {

                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");


                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Order_Id", Order_Id);
                htComments.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                htComments.Add("@Comment", txt_Comments.Text);
                htComments.Add("@Inserted_By", userid);
                htComments.Add("@Inserted_date", date);
                htComments.Add("@Modified_By", userid);
                htComments.Add("@Modified_Date", date);
                htComments.Add("@Work_Type", Work_Type_Id);
                htComments.Add("@status", "True");
                dtComments = dataaccess.ExecuteSP("Sp_Order_Comments", htComments);

                Geydview_Bind_Comments();

            }
        }


        private bool Validate_Document_Check_Type(int Order_Task,bool btn_Submit_Check)
        {
            // this is validate only for search task
            if (Order_Task == 2)
            {


                int Count = 0;

                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();
                htcheck.Add("@Trans", "CHECK_BY_USER");
                htcheck.Add("@Order_Id", Order_Id);
                htcheck.Add("@Order_Task", SESSION_ORDER_TASK);
                htcheck.Add("@User_Id", userid);
                dtcheck = dataaccess.ExecuteSP("usp_Docuement_Check_Type", htcheck);
                if (dtcheck.Rows.Count > 0)
                {
                    Count = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                }


                if (btn_Submit_Check == false && Count >= 0)
                {
                    if (ddl_order_Staus.SelectedValue.ToString() == "3")

                    {
                        MessageBox.Show("Please Select Document Check Type");

                        if (Application.OpenForms["Document_Check_Type"] != null)
                        {

                            Application.OpenForms["Document_Check_Type"].Focus();
                        }
                        else
                        {
                            Ordermanagement_01.New_Dashboard.Employee.Document_Check_Type Doc_Check_Type = new New_Dashboard.Employee.Document_Check_Type(obj_Order_Details_List, this);

                            Doc_Check_Type.ShowDialog();
                        }
                       
                    }
                    return false;
                }

                else if (btn_Submit_Check == true && Count > 0)
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

        public void Disable_Next_Task_Method()
        {


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_DOCUMENT_TYPE_FOR_ORDER");
            htcheck.Add("@Order_Id", Order_Id);
            htcheck.Add("@Order_Task", SESSION_ORDER_TASK);
            htcheck.Add("@User_Id", userid);
            dtcheck = dataaccess.ExecuteSP("usp_Docuement_Check_Type", htcheck);

           int Document_Check_Count=0;
            if (dtcheck.Rows.Count > 0)
            {
                Document_Check_Count = int.Parse(dtcheck.Rows[0]["Check_Count"].ToString());

            }

            if (Document_Check_Count > 1)
            {

                ddl_order_Staus.SelectedValue = "1";



            }
            else if (Document_Check_Count == 1)
            {
                Document_Check_Type_Id = int.Parse(dtcheck.Rows[0]["Document_Check_Type_Id"].ToString());
                if (Document_Check_Type_Id != 4)
                {

                    ddl_order_Task.Enabled = false;
                    ddl_order_Task.Items.Insert(0, "Image Request");
                    ddl_order_Task.Items.Insert(1, "Datadepth Request");
                    ddl_order_Task.Items.Insert(2, "Tax Certificate Request");

                    //   dbc.Bind_Order_Task_Document_Check_Type_Wise(ddl_order_Task);
                    if (Document_Check_Type_Id == 1) ddl_order_Task.SelectedIndex = 0;
                    else if (Document_Check_Type_Id == 2) ddl_order_Task.SelectedIndex = 1;
                    else if (Document_Check_Type_Id == 3) ddl_order_Task.SelectedIndex = 2;

                }
                else
                {

                    if (Document_Check_Type_Id==4)
                    {

                        ddl_order_Task.Enabled = true;


                        ddl_order_Task.Visible = true;
                        txt_Task.Visible = false;
                        // Chk_Self_Allocate.Visible = true;
                        ddl_order_Task.Items.Clear();

                        if (SESSSION_ORDER_TYPE == "Search")
                        {
                            ddl_order_Task.Items.Insert(0, "Search QC");
                            ddl_order_Task.Items.Insert(1, "Typing");
                            ddl_order_Task.Items.Insert(2, "Final QC");
                            ddl_order_Task.Items.Insert(3, "Exception");

                            // This option is enabled only for 40 client id
                            if (Client_id == 40 || Client_id == 4)
                            {
                                ddl_order_Task.Items.Insert(4, "Upload Completed");
                            }
                            // This is for 52002 Sub Clients

                            if (Sub_ProcessId == 395)
                            {

                                ddl_order_Task.Items.Insert(4, "Upload Completed");
                            }
                        }
                        



                    }


                }

            }
          
            
        }
        protected void Insert_delay_Order_Comments(int Work_Type_Id)
        {

            if (txt_Delay_Text.Text != "" && ddl_Issue_Category.SelectedIndex > 0)
            {



                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                Hashtable htdelaycheckComments = new Hashtable();
                DataTable dtdelaycheckComments = new System.Data.DataTable();

                htdelaycheckComments.Add("@Trans", "CHECK");
                htdelaycheckComments.Add("@Order_Id", Order_Id);
                htdelaycheckComments.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                htdelaycheckComments.Add("@User_Id", userid);
                htdelaycheckComments.Add("@Work_Type_Id", Work_Type_Id);
                dtdelaycheckComments = dataaccess.ExecuteSP("Sp_Order_Issue_Details", htdelaycheckComments);

                if (dtdelaycheckComments.Rows.Count > 0)
                {

                    Check_delay_Count = int.Parse(dtdelaycheckComments.Rows[0]["count"].ToString());


                }
                else
                {

                    Check_delay_Count = 0;
                }


                if (Check_delay_Count == 0)
                {

                    htComments.Add("@Trans", "INSERT");
                    htComments.Add("@Order_Id", Order_Id);
                    htComments.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                    htComments.Add("@Issue_Id", int.Parse(ddl_Issue_Category.SelectedValue.ToString()));
                    htComments.Add("@Reason", txt_Delay_Text.Text);
                    htComments.Add("@User_Id", userid);
                    htComments.Add("@Work_Type_Id", Work_Type_Id);
                    dtComments = dataaccess.ExecuteSP("Sp_Order_Issue_Details", htComments);
                }
                else

                {

                    htComments.Add("@Trans", "UPDATE");
                    htComments.Add("@Order_Id", Order_Id);
                    htComments.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                    htComments.Add("@Issue_Id", int.Parse(ddl_Issue_Category.SelectedValue.ToString()));
                    htComments.Add("@Reason", txt_Delay_Text.Text);
                    htComments.Add("@User_Id", userid);
                    htComments.Add("@Work_Type_Id", Work_Type_Id);
                    dtComments = dataaccess.ExecuteSP("Sp_Order_Issue_Details", htComments);
                }


            }
        }
        private void Insert_Order_Search_Cost()
        {


            if (txt_Order_Search_Cost.Text != "")
            {
                SearchCost = Convert.ToDecimal(txt_Order_Search_Cost.Text.ToString());
            }
            if (txt_Order_Copy_Cost.Text != "") { Copy_Cost = Convert.ToDecimal(txt_Order_Copy_Cost.Text.ToString()); }
            if (txt_Order_Abstractor_Cost.Text != "") { Abstractor_Cost = Convert.ToDecimal(txt_Order_Abstractor_Cost.Text.ToString()); }

            if (txt_Order_No_Of_Pages.Text != "") { No_Of_Pages = Convert.ToInt32(txt_Order_No_Of_Pages.Text.ToString()); }
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new System.Data.DataTable();

            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            string time = date.ToString("hh:mm tt");
            if (ddl_Order_Source.Text != "")
            {
                if (OPERATE_SEARCH_COST == "INSERT")
                {
                    htsearch.Add("@Trans", "INSERT");
                    htsearch.Add("@Order_Id", Order_Id);
                    htsearch.Add("@Source", ddl_Order_Source.Text);
                    htsearch.Add("@Order_source", ddl_Order_Source.SelectedValue);
                    htsearch.Add("@Search_Cost", SearchCost);
                    htsearch.Add("@Copy_Cost", Copy_Cost);
                    htsearch.Add("@Abstractor_Cost", Abstractor_Cost);
                    htsearch.Add("@No_Of_pages", No_Of_Pages);
                    htsearch.Add("@Inserted_By", userid);
                    htsearch.Add("@Inserted_date", date);
                    if (Work_Type_Id == 1)
                    {
                        if (lbl_Order_Task_Type.Text == "Search" || lbl_Order_Task_Type.Text == "Search QC")
                        {
                            htsearch.Add("@User_Password_Id", ddl_Web_search_sites.SelectedValue);
                            //if (ddl_Order_Source.Text == "Online/Data Tree" || ddl_Order_Source.Text=="Data Trace" || ddl_Order_Source.Text=="Data Tree" || ddl_Order_Source.Text=="Data Tree/Data Trace" || ddl_Order_Source.Text=="Subscription/Data Trace" || ddl_Order_Source.Text=="Subscription/Data Tree")
                            //{
                            //    htsearch.Add("@No_Of_Hits", txt_No_Of_Hits.Text);
                            //    htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                            //}
                            ////else if (ddl_Order_Source.SelectedIndex == 6)
                            ////{
                            ////    htsearch.Add("@No_Of_Hits", txt_No_Of_Hits.Text);
                            ////    htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                            ////}
                            //else if (ddl_Order_Source.Text == "Title Point")
                            //{
                            //    htsearch.Add("@No_Of_Hits", txt_No_Of_Hits.Text);
                            //    htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                            //}
                            htsearch.Add("@No_Of_Hits", txt_No_Of_Hits.Text);
                            htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                        }

                    }
                    htsearch.Add("@Website_Name", txt_Website.Text);
                    htsearch.Add("@status", "True");
                    dtsearch = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htsearch);
                }
                else if (OPERATE_SEARCH_COST == "UPDATE")
                {

                    htsearch.Add("@Trans", "UPDATE_EMPLOYEE_WISE");
                    htsearch.Add("@Order_Id", Order_Id);
                    htsearch.Add("@Source", ddl_Order_Source.Text);
                    htsearch.Add("@Order_source", ddl_Order_Source.SelectedValue);
                    htsearch.Add("@Search_Cost", SearchCost);
                    htsearch.Add("@Copy_Cost", Copy_Cost);
                    htsearch.Add("@Abstractor_Cost", Abstractor_Cost);
                    htsearch.Add("@No_Of_pages", No_Of_Pages);
                    if (Work_Type_Id == 1)
                    {
                        if (lbl_Order_Task_Type.Text == "Search" || lbl_Order_Task_Type.Text == "Search QC")
                        {
                            htsearch.Add("@User_Password_Id", ddl_Web_search_sites.SelectedValue);
                            //if (ddl_Order_Source.Text == "Online/Data Tree" || ddl_Order_Source.Text == "Data Trace" || ddl_Order_Source.Text == "Data Tree" || ddl_Order_Source.Text == "Data Tree/Data Trace" || ddl_Order_Source.Text == "Subscription/Data Trace" || ddl_Order_Source.Text == "Subscription/Data Tree")
                            //{
                            htsearch.Add("@No_Of_Hits", txt_No_Of_Hits.Text);
                            htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                            //}
                            //else if (ddl_Order_Source.SelectedIndex == 6)
                            //{
                            //    htsearch.Add("@No_Of_Hits", txt_No_Of_Hits.Text);
                            //    htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                            //}
                            //else if (ddl_Order_Source.Text == "Title Point")
                            //{
                            //htsearch.Add("@No_Of_Hits", txt_No_Of_Hits.Text);
                            //htsearch.Add("@No_Of_Documents", txt_No_of_documents.Text);
                            // }
                        }
                    }
                    htsearch.Add("@Website_Name", txt_Website.Text);
                    htsearch.Add("@Modified_By", userid);
                    htsearch.Add("@Modified_Date", date);
                    htsearch.Add("@status", "True");
                    dtsearch = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htsearch);
                }
            }
            else
            {
                MessageBox.Show("Source is Not Selected");
            }
        }


        private void Insert_ProductionDate()
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@Inserted_date", date);

                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@Inserted_date", date);

                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htProductionDate);
                }
            }
        }


        private void Insert_Order_Completed_ProductionDate()
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Progress_Id", 3);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status_Id", 15);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@Inserted_date", date);

                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status_Id", 15);
                    htProductionDate.Add("@Order_Progress_Id", 3);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@Inserted_date", date);

                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htProductionDate);
                }
            }
        }
        private void Insert_External_CLient_ProductionDate()
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@External_Order_Id", External_Client_Order_Id);
                    htProductionDate.Add("@Order_Task", SESSION_ORDER_TASK);
                    htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Order_Production_date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@Inserted_date", date);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "UPDATE");
                    htProductionDate.Add("@External_Order_Id", External_Client_Order_Id);
                    htProductionDate.Add("@Order_Task", SESSION_ORDER_TASK);
                    htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Order_Production_date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Modified_By", userid);
                    htProductionDate.Add("@Modified_Date", date);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_External_Client_Orders_Production", htProductionDate);
                }
            }
        }

        private void Insert_Rework_ProductionDate()
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", htProductionDate);
                }
            }
        }

        private void Insert_Rework_Order_Completed_ProductionDate()
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Task", 15);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status", 3);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Task", 15);
                    htProductionDate.Add("@Order_Status", 3);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Rework_ProductionDate", htProductionDate);
                }
            }
        }

        private void Insert_Super_Qc_ProductionDate()
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Super_Qc_ProductionDate", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Order_Task", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Order_Status", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Super_Qc_ProductionDate", htProductionDate);
                }
            }
        }


        private void Insert_Super_Qc_Order_Completed_ProductionDate()
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Task", 15);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Order_Status", 3);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Super_Qc_ProductionDate", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_User_Effeciency", Eff_Order_User_Effecncy);
                    htProductionDate.Add("@Order_Task", 15);
                    htProductionDate.Add("@Order_Status", 3);
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@User_Id", userid);
                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_Super_Qc_ProductionDate", htProductionDate);
                }
            }
        }




        private void Count_Of_Docuemnt_list()
        {


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_ORDER_WISE");
            htcheck.Add("@Order_Id", Order_Id);
            htcheck.Add("@Order_Status", SESSION_ORDER_TASK);
            htcheck.Add("@Work_Type_Id", Work_Type_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Document_List", htcheck);

            if (dtcheck.Rows.Count > 0)
            {
                Document_List_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());

            }
            else
            {

                Document_List_Count = 0;
            }
        }
        private bool Validate_Document_List()
        {




            Count_Of_Docuemnt_list();
            if (SESSION_ORDER_TASK != "12" && ddl_order_Staus.SelectedValue.ToString() == "3" && Document_List_Count <= 0 && lbl_Order_Task_Type.Text != "Search Tax Request")
            {


                MessageBox.Show("Please Enter Document List");
                Ordermanagement_01.Order_Document_List Order_Document_List = new Ordermanagement_01.Order_Document_List(userid, Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()), Work_Type_Id);
                Order_Document_List.Show();
                return false;


            }
            else
            {

                return true;
            }

            //return true;

        }

        private bool Validate_Search_And_Search_Qc_Note()
        {
            if (Work_Type_Id == 1 && btn_OrderSearhcerNotes.Enabled == true)
            {
                Hashtable htcheck_search_Note = new Hashtable();
                DataTable dtcheck_serch_Note = new DataTable();
                if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                {
                    htcheck_search_Note.Add("@Trans", "CHECK_COUNT");
                    htcheck_search_Note.Add("@Order_Id", Order_Id);
                    htcheck_search_Note.Add("@user_Id", userid);
                    htcheck_search_Note.Add("@Order_Task", SESSION_ORDER_TASK);
                    htcheck_search_Note.Add("@Work_Type_Id", Work_Type_Id);
                    dtcheck_serch_Note = dataaccess.ExecuteSP("Sp_Order_Search_Note_Pad", htcheck_search_Note);
                    if (dtcheck_serch_Note != null && dtcheck_serch_Note.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtcheck_serch_Note.Rows[0]["Count"]) > 0) return true;
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name == "Search_NotePad")
                            {
                                f.Close();
                                break;
                            }
                        }
                        MessageBox.Show("Please Enter Search Notes");

                        Ordermanagement_01.Employee.Search_NotePad form_Search_Note_Pad = new Employee.Search_NotePad(Order_Id, Work_Type_Id, userid, userid, int.Parse(SESSION_ORDER_TASK.ToString()), "Create", lbl_Order_Number.Text);

                        Invoke(new MethodInvoker(delegate { form_Search_Note_Pad.Show(); }));



                        return false;
                    }
                    else
                    {
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Name == "Search_NotePad")
                            {
                                f.Close();
                                break;
                            }
                        }
                        MessageBox.Show("Please Enter Search Notes");
                        return false;
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


        private bool Validate_Search_And_Search_Qc_Note_Old()
        {
            //if (btn_OrderSearhcerNotes.Visible != false)
            //{
            if (Work_Type_Id == 1 && btn_OrderSearhcerNotes.Enabled == true)
            {
                bool is_opened;
                Hashtable htcheck_search_Note = new Hashtable();
                DataTable dtcheck_serch_Note = new DataTable();
                if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                {

                    htcheck_search_Note.Add("@Trans", "CHECK_COUNT");
                    htcheck_search_Note.Add("@OrderId", Order_Id);
                    htcheck_search_Note.Add("@User_Id", userid);
                    htcheck_search_Note.Add("@Order_Task_Id", SESSION_ORDER_TASK);
                    dtcheck_serch_Note = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htcheck_search_Note);




                    if (dtcheck_serch_Note.Rows.Count > 0)
                    {

                        return true;
                    }
                    else
                    {
                        foreach (Form f in Application.OpenForms)
                        {

                            if (f.Name == "Order_Searcher_Notes")
                            {
                                is_opened = true;
                                f.Close();
                                break;
                            }

                        }

                        MessageBox.Show("Please Enter Search Notes");

                        //if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                        //{
                        //    Ordermanagement_01.Order_Searcher_Notes searcher_notes = new Ordermanagement_01.Order_Searcher_Notes(userid, roleid, Convert.ToString(Order_Id), lbl_customer_No.Text.ToString(), txt_Subprocess.Text.ToString(), lbl_Order_Number.Text.ToString(), SESSION_ORDER_TASK);
                        //    searcher_notes.Show();
                        //}


                        return false;
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
        private bool Validate_Searcher_Link()
        {

            if (btn_Searcher_Link.Enabled == true)
            {

                if (Order_Type_Id != 93 && Order_Type_Id != 138)
                {

                    if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                    {


                        Hashtable htcheck_search_link = new Hashtable();
                        DataTable dtcheck_serch_link = new DataTable();

                        htcheck_search_link.Add("@Trans", "CHECK_COUNT");
                        htcheck_search_link.Add("@Order_Id", Order_Id);
                        htcheck_search_link.Add("@Inserted_By", userid);
                        htcheck_search_link.Add("@Order_Task", SESSION_ORDER_TASK);
                        dtcheck_serch_link = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htcheck_search_link);


                        int Check_Count = 0;



                        if (dtcheck_serch_link.Rows.Count > 0)
                        {
                            Check_Count = int.Parse(dtcheck_serch_link.Rows[0]["count"].ToString());

                        }
                        else
                        {

                            Check_Count = 0;
                        }

                        if (Check_Count == 0)
                        {

                            MessageBox.Show("Please Enter Searcher Links");
                            if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                            {
                                Ordermanagement_01.Employee.Searcher_New_Link_history Search_LinkHistory = new Ordermanagement_01.Employee.Searcher_New_Link_history(Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()), userid, int.Parse(roleid.ToString()), lbl_Order_Number.Text, County_Id);
                                Search_LinkHistory.Show();
                            }
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
            }//
            else
            {

                return true;
            }
        }
        private bool Validate_Search_Cost()
        {
            if (Work_Type_Id == 1)
            {
                if (SESSION_ORDER_TASK != "12" && SESSION_ORDER_TASK != "4" && SESSION_ORDER_TASK != "7" && lbl_Order_Task_Type.Text != "Search Tax Request")
                {


                    if (ddl_Order_Source.SelectedIndex == 0 || ddl_Order_Source.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select Order source ");
                        ddl_Order_Source.Focus();
                        return false;
                    }
                    else if (txt_Order_Search_Cost.Text == "")
                    {
                        MessageBox.Show("Enter Order Search Cost");
                        txt_Order_Search_Cost.Focus();
                        return false;
                    }
                    else if (txt_Order_Copy_Cost.Text == "")
                    {
                        MessageBox.Show("Enter Order Copy Cost");
                        txt_Order_Copy_Cost.Focus();
                        return false;
                    }
                    else if (txt_Order_Abstractor_Cost.Text == "")
                    {
                        MessageBox.Show("Enter Order Abstractor Cost");
                        txt_Order_Abstractor_Cost.Focus();
                        return false;
                    }
                    else if (txt_Order_No_Of_Pages.Text == "")
                    {
                        MessageBox.Show("Enter No Of pages");
                        txt_Order_No_Of_Pages.Focus();
                        return false;
                    }



                }
                else
                {

                    return true;
                }

            }
            return true;

        }


        private bool Validate_Error_Entry()
        {
            if (SESSION_ORDER_TASK != "2" && SESSION_ORDER_TASK != "4" && SESSION_ORDER_TASK != "12" && lbl_Order_Task_Type.Text != "Search Tax Request")
            {
                if (SESSION_ORDER_TASK == "3")
                {

                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "CHECK_ERROR_COUNT");
                    htcheck.Add("@Order_ID", Order_Id);
                    htcheck.Add("@Task", int.Parse(SESSION_ORDER_TASK));
                    htcheck.Add("@Work_Type", Work_Type_Id);
                    dtcheck = dataaccess.ExecuteSP("Sp_Error_Info", htcheck);
                    if (dtcheck.Rows.Count == 0)
                    {
                        MessageBox.Show("Check Error Entry Not added");
                        Ordermanagement_01.Employee_Error_Entry Error_entry = new Ordermanagement_01.Employee_Error_Entry(userid, roleid, SESSION_ORDER_TASK, Order_Id, 2, Work_Type_Id, SESSION_ORDER_NO, txt_Prdoductiondate.Text, 0, Client_id);
                        Error_entry.Show();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (SESSION_ORDER_TASK == "7")
                {
                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "CHECK_ERROR_COUNT");
                    htcheck.Add("@Order_ID", Order_Id);
                    htcheck.Add("@Task", int.Parse(SESSION_ORDER_TASK));
                    htcheck.Add("@Work_Type", Work_Type_Id);

                    dtcheck = dataaccess.ExecuteSP("Sp_Error_Info", htcheck);
                    if (dtcheck.Rows.Count == 0)
                    {
                        MessageBox.Show("Check Error Entry Not added");
                        Ordermanagement_01.Employee_Error_Entry Error = new Ordermanagement_01.Employee_Error_Entry(userid, roleid, SESSION_ORDER_TASK, Order_Id, 2, Work_Type_Id, SESSION_ORDER_NO, txt_Prdoductiondate.Text, 0, Client_id);
                        Error.Show();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                else if (SESSION_ORDER_TASK == "24")
                {
                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "CHECK_ERROR_COUNT");
                    htcheck.Add("@Order_ID", Order_Id);
                    htcheck.Add("@Task", int.Parse(SESSION_ORDER_TASK));
                    htcheck.Add("@Work_Type", Work_Type_Id);

                    dtcheck = dataaccess.ExecuteSP("Sp_Error_Info", htcheck);
                    if (dtcheck.Rows.Count == 0)
                    {
                        MessageBox.Show("Check Error Entry Not added");
                        Ordermanagement_01.Employee_Error_Entry Error = new Ordermanagement_01.Employee_Error_Entry(userid, roleid, SESSION_ORDER_TASK, Order_Id, 2, Work_Type_Id, SESSION_ORDER_NO, txt_Prdoductiondate.Text, 0, Client_id);
                        Error.Show();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return true;
        }


        private bool Validate_Tax_Internal_Status()
        {
            // this is for all the clients
            if (Sub_ProcessId != 200)
            {

                if (Internal_Tax_Check == 1 && ddl_order_Task.SelectedItem == "Upload Completed" && ddl_Tax_Task.SelectedIndex <= 0)
                {

                    MessageBox.Show("Please Select the Tax Stataus");
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

        private bool Validate_Tax_Internal_Status_Client_Sub_Client_Wise()
        {

            if (Sub_ProcessId == 200)
            {
                string Order_Status = ddl_order_Staus.SelectedValue.ToString();
                if (SESSION_ORDER_TASK == "2" && Order_Status == "3" && ddl_Tax_Task.SelectedIndex <= 0)
                {

                    MessageBox.Show("Please Select the Tax Stataus");
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
        private bool ValidateOrderSearchNotes()
        {
            if (btn_OrderSearhcerNotes.Visible != false)
            {
                if (SESSION_ORDER_TASK != "4" && SESSION_ORDER_TASK != "12")
                {
                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                    {
                        htcheck.Add("@Trans", "CHECK_COUNT");
                        htcheck.Add("@OrderId", Order_Id);
                        htcheck.Add("@Order_Task_Id", int.Parse(SESSION_ORDER_TASK));
                        dtcheck = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htcheck);
                        if (dtcheck.Rows.Count == 0)
                        {
                            MessageBox.Show("Check Order Searcher Notest Not added");
                            Ordermanagement_01.Order_Searcher_Notes Searchnotes = new Ordermanagement_01.Order_Searcher_Notes(userid, roleid, Convert.ToString(Order_Id), lbl_customer_No.Text.ToString(), txt_Subprocess.Text.ToString(), lbl_Order_Number.Text.ToString(), SESSION_ORDER_TASK);
                            Searchnotes.Show();
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return true;
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Update_User_Order_Time_Info_On_Cancel_Logout();
            foreach (Form f in Application.OpenForms)
            {

                if (f.Name == "Judgement_Period_Create_View")
                {
                    IsOpen_jud = true;
                    f.Close();
                    break;
                }

            }
            foreach (Form f1 in Application.OpenForms)
            {
                if (f1.Name == "State_Wise_Tax_Due_Date")
                {
                    IsOpen_state = true;
                    f1.Close();
                    break;
                }
            }
            foreach (Form f2 in Application.OpenForms)
            {
                if (f2.Name == "Employee_Order_Information")
                {
                    IsOpen_emp = true;
                    f2.Close();
                    break;
                }
            }
            foreach (Form f3 in Application.OpenForms)
            {
                if (f3.Name == "Order_Template_View")
                {
                    IsOpen_us = true;
                    f3.Close();
                    break;
                }
            }

            foreach (Form f4 in Application.OpenForms)
            {
                if (f4.Name == "Employee_Alert_Message")
                {
                    IsOpen_us = true;
                    f4.Close();
                    break;
                }
            }
            this.Close();

        }

        private void txt_Effectivedate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_Order_Source.Focus();
            }
        }

        private void ddl_Order_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Work_Type_Id == 1)
            {
                if (lbl_Order_Task_Type.Text == "Search" || lbl_Order_Task_Type.Text == "Search QC")
                {
                    if (ddl_Order_Source.Text == "Subscription" || ddl_Order_Source.Text == "Subscription/Data Trace" || ddl_Order_Source.Text == "Subscription/Data Tree")//web search
                    {
                        if (ddl_Web_search_sites.Text == "Others")
                        {
                            lbl_Enter_Website.Visible = true;
                            txt_Website.Visible = true;
                        }
                        lbl_webSearch.Visible = true;
                        lbl_mand_web.Visible = true;
                        ddl_Web_search_sites.Visible = true;


                        //lbl_No_Of_hits.Visible = false;
                        //lbl_mand_noofhits.Visible = false;
                        //lbl_No_of_Documents.Visible = false;
                        //lbl_mandnoofdoc.Visible = false;
                        //txt_No_Of_Hits.Visible = false;
                        //txt_No_of_documents.Visible = false;

                    }
                    //else if (ddl_Order_Source.SelectedIndex == 8)//data tree
                    //{
                    //    if (lbl_Enter_Website.Visible == true && txt_Website.Visible==true)
                    //    {
                    //        lbl_Enter_Website.Visible = false;
                    //        txt_Website.Visible = false;
                    //    }
                    //    lbl_No_Of_hits.Visible = true;
                    //    lbl_mand_noofhits.Visible = true;
                    //    lbl_No_of_Documents.Visible = true;
                    //    lbl_mandnoofdoc.Visible = true;
                    //    txt_No_Of_Hits.Visible = true;
                    //    txt_No_of_documents.Visible = true;

                    //    lbl_webSearch.Visible = false;
                    //    lbl_mand_web.Visible = false;
                    //    ddl_Web_search_sites.Visible = false;
                    //    lbl_man_enterweb.Visible = false;
                    //}
                    //else if (ddl_Order_Source.SelectedIndex == 6)//data trace
                    //{
                    //    if (lbl_Enter_Website.Visible == true && txt_Website.Visible == true)
                    //    {
                    //        lbl_Enter_Website.Visible = false;
                    //        txt_Website.Visible = false;
                    //    }
                    //    lbl_No_Of_hits.Visible = true;
                    //    lbl_mand_noofhits.Visible = true;
                    //    lbl_No_of_Documents.Visible = true;
                    //    lbl_mandnoofdoc.Visible = true;
                    //    txt_No_Of_Hits.Visible = true;
                    //    txt_No_of_documents.Visible = true;

                    //    lbl_webSearch.Visible = false;
                    //    lbl_mand_web.Visible = false;
                    //    ddl_Web_search_sites.Visible = false;
                    //    lbl_man_enterweb.Visible = false;
                    //}
                    //else if (ddl_Order_Source.SelectedIndex == 7)//title point
                    //{
                    //    if (lbl_Enter_Website.Visible == true && txt_Website.Visible == true)
                    //    {
                    //        lbl_Enter_Website.Visible = false;
                    //        txt_Website.Visible = false;
                    //    }
                    //    lbl_No_Of_hits.Visible = true;
                    //    lbl_mand_noofhits.Visible = true;
                    //    lbl_No_of_Documents.Visible = true;
                    //    lbl_mandnoofdoc.Visible = true;
                    //    txt_No_Of_Hits.Visible = true;
                    //    txt_No_of_documents.Visible = true;

                    //    lbl_webSearch.Visible = false;
                    //    lbl_mand_web.Visible = false;
                    //    ddl_Web_search_sites.Visible = false;
                    //    lbl_man_enterweb.Visible = false;
                    //}
                    else if (lbl_Order_Task_Type.Text == "Typing" || lbl_Order_Task_Type.Text == "Typing QC")
                    {
                        lbl_webSearch.Visible = true;
                        lbl_mand_web.Visible = true;
                        ddl_Web_search_sites.Visible = true;
                        ddl_Web_search_sites.Enabled = false;

                        //lbl_No_Of_hits.Visible = true;
                        //lbl_mand_noofhits.Visible = true;
                        //txt_No_Of_Hits.Enabled = false;
                        //lbl_No_of_Documents.Visible = true;
                        //lbl_mandnoofdoc.Visible = true;
                        //txt_No_of_documents.Visible = true;
                        //txt_No_of_documents.Enabled = false;
                        lbl_man_enterweb.Visible = false;
                    }
                    else
                    {
                        if (lbl_Enter_Website.Visible == true && txt_Website.Visible == true)
                        {
                            lbl_Enter_Website.Visible = false;
                            txt_Website.Visible = false;
                        }
                        lbl_mand_web.Visible = false;
                        lbl_mandnoofdoc.Visible = false;
                        //lbl_mand_noofhits.Visible = false;
                        lbl_webSearch.Visible = false;
                        ddl_Web_search_sites.Visible = false;
                        //lbl_No_Of_hits.Visible = false;
                        //txt_No_Of_Hits.Visible = false;
                        //lbl_No_of_Documents.Visible = false;
                        //txt_No_of_documents.Visible = false;
                        lbl_man_enterweb.Visible = false;
                    }
                }
            }
            else
            {
                if (lbl_Enter_Website.Visible == true && txt_Website.Visible == true)
                {
                    lbl_Enter_Website.Visible = false;
                    txt_Website.Visible = false;
                }
                lbl_mand_web.Visible = false;
                //lbl_mandnoofdoc.Visible = false;
                //lbl_mand_noofhits.Visible = false;
                lbl_webSearch.Visible = false;
                ddl_Web_search_sites.Visible = false;
                //lbl_No_Of_hits.Visible = false;
                //txt_No_Of_Hits.Visible = false;
                //lbl_No_of_Documents.Visible = false;
                //txt_No_of_documents.Visible = false;
            }
        }

        private void ddl_Order_Source_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Order_Search_Cost.Focus();
            }
        }

        private void txt_Order_Copy_Cost_TextChanged(object sender, EventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Order_Search_Cost.Text))
            {


                txt_Order_Search_Cost.Text = null;
            }
        }

        private void txt_Order_Copy_Cost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Order_Abstractor_Cost.Focus();
            }

        }

        private void txt_Order_No_Of_Pages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Comments.Focus();
            }
        }

        private void txt_Comments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_order_Staus.Focus();
            }
        }

        private void ddl_order_Staus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_order_Task.Focus();
            }

        }

        private void txt_Order_Search_Cost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Order_Copy_Cost.Focus();
            }
        }

        private void txt_Order_Abstractor_Cost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Order_No_Of_Pages.Focus();
            }
        }

        private void ddl_order_Staus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Work_Type_Id == 1 || Work_Type_Id == 2)
            {


                if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9")
                {
                    Chk = 0;
                    txt_Task.Visible = true;

                    //Userhold,hold,clarification queues
                    btn_Checklist.Enabled = false;
                    btn_submit.Enabled = true;
                }
                else
                {
                    Chk = 1;
                }
                if (ddl_order_Staus.SelectedValue.ToString() == "3")
                {
                    if (lbl_Order_Task_Type.Text == "Upload" || lbl_Order_Task_Type.Text == "Exception" || lbl_Order_Task_Type.Text == "Search Tax Request")
                    {
                        // Userhold,hold,clarification queues
                        btn_Checklist.Enabled = false;
                        btn_submit.Enabled = true;
                    }

                    else
                    {
                        btn_Checklist.Enabled = true;
                        btn_submit.Enabled = false;

                    }





                    //// For issue Updatyed

                    //btn_submit.Enabled = true;

                    //btn_Checklist.Enabled = false;
                    //============================



                    int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString());



                    ////its is to restrict the task on client wise

                    //Hashtable htget_Client_Wise_Restricted_task = new Hashtable();
                    //DataTable dtget_Client_Wise_Restricted_task = new DataTable();

                    //htget_Client_Wise_Restricted_task.Add("@Trans", "GET_TASK_LIST_CLIENT_AND_TASK_WISE");
                    //htget_Client_Wise_Restricted_task.Add("@Client_Id",Client_id);
                    //htget_Client_Wise_Restricted_task.Add("@Task_Stage_Id",Order_Task);
                    //dtget_Client_Wise_Restricted_task = dataaccess.ExecuteSP("Sp_Client_Task_Stage_Target", htget_Client_Wise_Restricted_task);
                    //ddl_order_Task.Visible = true;
                    //if (dtget_Client_Wise_Restricted_task.Rows.Count > 0)
                    //{
                    //    txt_Task.Visible = false;


                    //    dbc.Bind_Order_Task_Client_Wise(ddl_order_Task, Client_id, Order_Task);


                    //}
                    //else
                    //{

                    // This for Search Task

                    if (Order_Task == 2 || Order_Task == 3)
                    {




                        ddl_order_Task.Visible = true;
                        txt_Task.Visible = false;
                        // Chk_Self_Allocate.Visible = true;
                        ddl_order_Task.Items.Clear();

                        if (SESSSION_ORDER_TYPE == "Search")
                        {
                            ddl_order_Task.Items.Insert(0, "Search QC");
                            ddl_order_Task.Items.Insert(1, "Typing");
                            ddl_order_Task.Items.Insert(2, "Final QC");
                            ddl_order_Task.Items.Insert(3, "Exception");

                            // This option is enabled only for 40 client id
                            if (Client_id == 40 || Client_id == 4)
                            {
                                ddl_order_Task.Items.Insert(4, "Upload Completed");
                            }
                            // This is for 52002 Sub Clients

                            if (Sub_ProcessId == 395)
                            {

                                ddl_order_Task.Items.Insert(4, "Upload Completed");
                            }
                        }
                        if (SESSSION_ORDER_TYPE == "Search QC")
                        {
                            ddl_order_Task.Items.Insert(0, "Typing");
                            ddl_order_Task.Items.Insert(1, "Final QC");
                            ddl_order_Task.Items.Insert(2, "Exception");
                            // This option is enabled only for 40 client id
                            if (Client_id == 40 || Client_id == 4)
                            {
                                ddl_order_Task.Items.Insert(3, "Upload Completed");
                            }
                            // This is for 52002 Sub Clients

                            if (Sub_ProcessId == 395)
                            {

                                ddl_order_Task.Items.Insert(4, "Upload Completed");
                            }
                        }
                        if (SESSSION_ORDER_TYPE == "Typing")
                        {
                            ddl_order_Task.Items.Insert(0, "Typing QC");
                            ddl_order_Task.Items.Insert(1, "Final QC");
                            ddl_order_Task.Items.Insert(2, "Exception");
                            // This option is enabled only for 40 client id
                            if (Client_id == 40 || Client_id == 4 || Client_id == 9)
                            {
                                ddl_order_Task.Items.Insert(3, "Upload Completed");
                            }

                            if (Client_id == 26)
                            {
                                ddl_order_Task.Items.Insert(3, "Upload Completed");
                            }

                            // This is for 52002 Sub Clients

                            if (Sub_ProcessId == 395)
                            {

                                ddl_order_Task.Items.Insert(4, "Upload Completed");
                            }
                        }
                        if (SESSSION_ORDER_TYPE == "Typing QC")
                        {
                            ddl_order_Task.Items.Insert(0, "Final QC");
                            ddl_order_Task.Items.Insert(1, "Exception");
                            // This option is enabled only for 40 client id
                            if (Client_id == 40 || Client_id == 4 || Client_id == 9)
                            {
                                ddl_order_Task.Items.Insert(2, "Upload Completed");
                            }
                            if (Client_id == 26)
                            {
                                ddl_order_Task.Items.Insert(2, "Upload Completed");
                            }

                            // This is for 52002 Sub Clients

                            if (Sub_ProcessId == 395)
                            {

                                ddl_order_Task.Items.Insert(4, "Upload Completed");
                            }
                        }
                        if (SESSSION_ORDER_TYPE == "Upload")
                        {
                            ddl_order_Task.Items.Insert(0, "Final QC");
                            ddl_order_Task.Items.Insert(1, "Exception");
                            ddl_order_Task.Items.Insert(2, "Upload Completed");
                        }
                        if (SESSSION_ORDER_TYPE == "Final QC")
                        {
                            ddl_order_Task.Items.Insert(0, "Exception");
                            ddl_order_Task.Items.Insert(1, "Upload");
                            ddl_order_Task.Items.Insert(2, "Upload Completed");
                        }
                        if (SESSSION_ORDER_TYPE == "Exception")
                        {
                            ddl_order_Task.Items.Insert(0, "Upload");
                            ddl_order_Task.Items.Insert(1, "Upload Completed");
                        }
                        if (SESSSION_ORDER_TYPE == "Search Tax Request")
                        {


                            ddl_order_Task.Visible = false;

                            //ddl_order_Task.SelectedIndex = 0;
                        }

                        // Not required we are validating during Submit
                        //if (SESSSION_ORDER_TYPE != "Search Tax Request")
                        //{
                        //    // this is commited for Server issue

                        //    Ordermanagement_01.Order_Document_List Order_Document_List = new Ordermanagement_01.Order_Document_List(userid, Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()), Work_Type_Id);
                        //    Order_Document_List.Show();

                        //}



                    }

                    if (Order_Task == 2)
                    {
                        btn_Submit_Clicked = false;
                        Validate_Document_Check_Type(Order_Task, btn_Submit_Clicked);

                    }

                    if (Order_Task == 27 || Order_Task == 28 || Order_Task == 29)
                    {
                        btn_Checklist.Enabled = false;
                        btn_submit.Enabled = true;

                        ddl_order_Task.Items.Insert(0, "Search QC");
                        ddl_order_Task.Items.Insert(1, "Typing");
                        ddl_order_Task.Items.Insert(2, "Final QC");
                        ddl_order_Task.Items.Insert(3, "Exception");

                    }

                    

                   

                    else if (Order_Task != 2 && Order_Task != 3)
                    {

                        if (Order_Task != 27 && Order_Task != 28 && Order_Task != 29)
                        {

                            // Label81.Visible = true;
                            ddl_order_Task.Visible = true;
                            txt_Task.Visible = false;
                            ddl_order_Task.Items.Clear();
                            // Chk_Self_Allocate.Visible = true;
                            if (SESSSION_ORDER_TYPE == "Search")
                            {
                                ddl_order_Task.Items.Insert(0, "Search QC");
                                ddl_order_Task.Items.Insert(1, "Typing");
                                ddl_order_Task.Items.Insert(2, "Final QC");
                                ddl_order_Task.Items.Insert(3, "Exception");
                                // This option is enabled only for 40 client id
                                if (Client_id == 40)
                                {
                                    ddl_order_Task.Items.Insert(4, "Upload Completed");
                                }
                            }
                            if (SESSSION_ORDER_TYPE == "Search QC")
                            {
                                ddl_order_Task.Items.Insert(0, "Typing");
                                ddl_order_Task.Items.Insert(1, "Final QC");
                                ddl_order_Task.Items.Insert(2, "Exception");
                                // This option is enabled only for 40 client id
                                if (Client_id == 40)
                                {
                                    ddl_order_Task.Items.Insert(3, "Upload Completed");
                                }
                            }
                            if (SESSSION_ORDER_TYPE == "Typing")
                            {
                                ddl_order_Task.Items.Insert(0, "Typing QC");
                                ddl_order_Task.Items.Insert(1, "Final QC");
                                ddl_order_Task.Items.Insert(2, "Exception");
                                // This option is enabled only for 40 client id
                                if (Client_id == 40 || Client_id == 4 || Client_id == 9)
                                {
                                    ddl_order_Task.Items.Insert(3, "Upload Completed");
                                }
                                if (Client_id == 26)
                                {
                                    ddl_order_Task.Items.Insert(3, "Upload Completed");
                                }
                            }
                            if (SESSSION_ORDER_TYPE == "Typing QC")
                            {
                                ddl_order_Task.Items.Insert(0, "Final QC");
                                ddl_order_Task.Items.Insert(1, "Exception");
                                // This option is enabled only for 40 client id
                                if (Client_id == 40 || Client_id == 4 || Client_id == 9)
                                {
                                    ddl_order_Task.Items.Insert(2, "Upload Completed");
                                }
                                if (Client_id == 26)
                                {
                                    ddl_order_Task.Items.Insert(2, "Upload Completed");
                                }
                            }
                            if (SESSSION_ORDER_TYPE == "Upload")
                            {
                                ddl_order_Task.Items.Insert(0, "Final QC");

                                ddl_order_Task.Items.Insert(1, "Upload Completed");
                            }
                            if (SESSSION_ORDER_TYPE == "Final QC")
                            {
                                ddl_order_Task.Items.Insert(0, "Exception");
                                ddl_order_Task.Items.Insert(1, "Upload");
                                ddl_order_Task.Items.Insert(2, "Upload Completed");
                            }
                            if (SESSSION_ORDER_TYPE == "Exception")
                            {
                                ddl_order_Task.Items.Insert(0, "Upload");
                                ddl_order_Task.Items.Insert(1, "Upload Completed");
                            }
                            if (SESSSION_ORDER_TYPE == "Search Tax Request")
                            {


                                ddl_order_Task.Visible = false;

                                //ddl_order_Task.SelectedIndex = 0;
                            }






                        }


                    }
                    else
                    {


                    }

                }








                else
                {
                    txt_Task.Visible = true;
                    //btn_submit.Enabled = true;

                    ddl_order_Task.Visible = false;


                    //Userhold,hold,clarification queues
                    btn_Checklist.Enabled = false;
                    btn_submit.Enabled = true;
                }
            }

            Enable_Tax_Client_Wise_Task_Wise();

        }

        private void Employee_Order_Entry_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (formProcess != 1)
            {
                if (Work_Type_Id == 1)
                {
                    Update_User_Order_Time_Info_On_Cancel_Logout();
                }
                else if (Work_Type_Id == 2)
                {

                    Update_Rework_User_Order_Time_Info();

                }
                else if (Work_Type_Id == 3)
                {

                    Update_Super_Qc_User_Order_Time_Info();
                }
            }



            if (MessageBox.Show("Exit or No?",
                      "Msg",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {

                foreach (Form f in Application.OpenForms)
                {

                    if (f.Name == "Judgement_Period_Create_View")
                    {
                        IsOpen_jud = true;
                        f.Close();
                        break;
                    }

                }
                foreach (Form f1 in Application.OpenForms)
                {
                    if (f1.Name == "State_Wise_Tax_Due_Date")
                    {
                        IsOpen_state = true;
                        f1.Close();
                        break;
                    }
                }
                foreach (Form f2 in Application.OpenForms)
                {
                    if (f2.Name == "Employee_Order_Information")
                    {
                        IsOpen_emp = true;
                        f2.Close();
                        break;
                    }
                }
                foreach (Form f3 in Application.OpenForms)
                {
                    if (f3.Name == "Order_Template_View")
                    {
                        IsOpen_us = true;
                        f3.Close();
                        break;
                    }
                }

                foreach (Form f4 in Application.OpenForms)
                {
                    if (f4.Name == "Employee_Alert_Message")
                    {
                        IsOpen_us = true;
                        f4.Close();
                        break;
                    }
                }


            }



        }


        private void button2_Click(object sender, EventArgs e)
        {
            string State_ID = State_Id.ToString();
            obj_Order_Details_List = new Order_Passing_Params()
            {
                Client_Id = Client_id,
                Sub_Client_Id = Sub_ProcessId,
                Order_Id = Order_Id,
                Order_Task_Id = Convert.ToInt32(SESSION_ORDER_TASK),
                Order_Type_Id = Order_Type_Id,
                Work_Type_Id = Work_Type_Id,
                Order_Type_Abs_Id = Order_Type_ABS_id,
                User_Id = userid,
                County_Id = County_Id,
                State_Id = State_Id,
                Form_View_Type = "View",
            };
            New_Dashboard.Orders.Order_Instruction emp = new Order_Instruction(obj_Order_Details_List);
            emp.Show();
        }

        private void btn_templete_Click(object sender, EventArgs e)
        {

            Order_Template_View ot = new Order_Template_View(Sub_ProcessId, lbl_Order_Number.Text, userid, Order_Id);
            ot.Show();

            //string Path;
            //DataAccess dataaccess = new DataAccess();
            //Hashtable htselect = new Hashtable();
            //DataTable dtselect = new DataTable();
            //htselect.Add("@Trans", "SELECTSUBPROCESSWISE");
            //htselect.Add("@Subprocess_Id", Sub_ProcessId);
            //dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
            //Path = dtselect.Rows[0]["Templete_Upload_Path"].ToString();
            //System.Diagnostics.Process.Start(Path);
        }
        private void Btn_Marker_Maker_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "PACKAGE_VALIDATE");
            ht.Add("@Order_Id", Order_Id);
            dt = dataaccess.ExecuteSP("Sp_Document_Upload", ht);
            if (dt.Rows.Count > 0)
            {
                // System.Diagnostics.Process.Start(dt.Rows[0]["Document_Path"].ToString());
                //Ordermanagement_01.MarkerMaker.Image_Marker_Maker Markermaker = new Ordermanagement_01.MarkerMaker.Image_Marker_Maker(Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()), lbl_Order_Number.Text, lbl_Order_Task_Type.Text, lbl_customer_No.Text, txt_Subprocess.Text, Client_id);
                // Markermaker.Show();
            }
            else
            {
                MessageBox.Show("Please select search Package in uploaddocuments");
            }
        }

        private void txt_Order_Search_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (char.IsLetter(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

            //var text = txt_Order_Search_Cost.Text.Trim();
            //if (Regex.IsMatch(text, @"^\d{1,2}(\.\d{1,2})?$"))
            //{
            //    // Do something here
            //}
            //else
            //{
            //    MessageBox.Show("Doesn't match pattern");
            //}


            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
        e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;

        }

        private void txt_Order_Copy_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
      e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Order_Abstractor_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
        e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;

        }

        private void txt_Order_No_Of_Pages_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar == 46 && e.KeyChar != 44 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_Order_Search_Cost_TextChanged(object sender, EventArgs e)
        {
            //decimal x;
            //decimal.TryParse(txt_Order_Search_Cost.Text, out x);
            //txt_Order_Search_Cost.Text = x.ToString(".00");

            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Order_Search_Cost.Text))
            {



                txt_Order_Search_Cost.Text = "";

            }


        }

        private void txt_Order_Abstractor_Cost_TextChanged(object sender, EventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\{}':;,<>/?[\]""_-]");

            if (r.IsMatch(txt_Order_Search_Cost.Text))
            {


                txt_Order_Search_Cost.Text = "";
            }

        }

        //private void grd_Error_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 4)
        //    {
        private void grd_Error_SelectionChanged(object sender, EventArgs e)
        {
            //string result = ((ComboBox)sender).SelectedItem.ToString();
            //Hashtable htselect = new Hashtable();
            //DataTable dtselect = new DataTable();
            //htselect.Add("@Trans", "SELECT_Error_description");
            //htselect.Add("@Error_Type_Id", result);
            //dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            //DataRow dr = dtselect.NewRow();
            //dr[0] = 0;
            //dr[0] = "Select";
            //dtselect.Rows.InsertAt(dr, 1);
            //ddl_Error_Type.DataSource = dtselect;
            //ddl_Error_Type.ValueMember = "Error_description_Id";
            //ddl_Error_Type.DisplayMember = "Error_description";
        }

        //private void grd_Error_KeyDown(object sender, KeyEventArgs e)
        //{

        //    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)grd_Error.Rows[0].Cells[2];
        //    if (cell.Value != null)
        //    {
        //        Error_Type_id = int.Parse(cell.Value.ToString());
        //    }
        //}

        private void Employee_Order_Entry_FormClosed(object sender, FormClosedEventArgs e)
        {


        }

        private void txt_Prdoductiondate_ValueChanged(object sender, EventArgs e)
        {

            if (DateCustom != 0)
            {
                txt_Prdoductiondate.CustomFormat = "MM/dd/yyyy";
            }
            DateCustom = 1;
        }

        private void cbo_ErrorCatogery_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string Error_Type=cbo_ErrorCatogery.Text;

            //Hashtable hterror = new Hashtable();
            //DataTable dterror = new DataTable();
            //hterror.Add("@Trans", "ERROR_TYPE");
            //hterror.Add("@Error_Type",Error_Type);
            //dterror = dataaccess.ExecuteSP("Sp_Errors_Details", hterror);
            //if (dterror.Rows.Count > 0)
            //{
            //    result = int.Parse(dterror.Rows[0]["Error_Type_Id"].ToString());
            //}
            //Hashtable htselect = new Hashtable();
            //DataTable dtselect = new DataTable();
            //htselect.Add("@Trans", "SELECT_Error_description");
            //htselect.Add("@Error_Type_Id", result);
            //dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            //DataRow dr = dtselect.NewRow();
            //dr[0] = 0;
            //dr[0] = "Select";
            //dtselect.Rows.InsertAt(dr, 1);
            //cbo_ErrorDes.DataSource = dtselect;
            //cbo_ErrorDes.ValueMember = "Error_description_Id";
            //cbo_ErrorDes.DisplayMember = "Error_description";
        }

        //private void btn_ErrorSub_Click(object sender, EventArgs e)
        //{

        //}

        private void btn_ErrorEntry_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_Error_Entry Error_Entry = new Ordermanagement_01.Employee_Error_Entry(userid, roleid, SESSION_ORDER_TASK, Order_Id, 2, Work_Type_Id, SESSION_ORDER_NO, txt_Prdoductiondate.Text, 0, Client_id);
            Error_Entry.Show();
        }

        private void btn_Preview_Check_List_Click(object sender, EventArgs e)
        {

            //Ordermanagement_01.Order_Check_List_View check_List_View = new Ordermanagement_01.Order_Check_List_View(userid, Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()),"UserWise",Work_Type_Id,roleid);
            //check_List_View.Show();

            Ordermanagement_01.Order_Check_List_View check_List_View = new Ordermanagement_01.Order_Check_List_View(userid, Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()), "UserWise", Work_Type_Id, roleid);
            check_List_View.Show();
        }

        private void txt_Effectivedate_ValueChanged(object sender, EventArgs e)
        {

            if (Efective_Date_Custom != 0)
            {
                txt_Effectivedate.CustomFormat = "MM/dd/yyyy";
            }
            Efective_Date_Custom = 1;
        }

        protected void Task_Question_Form()
        {
            //Ordermanagement_01.Questions TaskQuestion = new Ordermanagement_01.Questions(userid, Client_id, Sub_ProcessId, SESSION_ORDER_TASK.ToString(), lbl_Order_Type.Text, lbl_Order_Task_Type.Text, Order_Id, USERCOUNT, AVAILABLE_COUNT, lbl_Order_Number.Text, Client_Name, Sub_ProcessName, int.Parse(SESSION_ORDER_TASK.ToString()), Work_Type_Id, lbl_Order_Number.Text, lbl_Order_Task_Type.Text);
            ////Ordermanagement_01.Questions TaskQuestion = new Ordermanagement_01.Questions(userid, SESSION_ORDER_TASK.ToString(), lbl_Order_Type.Text, lbl_Order_Task_Type.Text, Order_Id, USERCOUNT, AVAILABLE_COUNT, lbl_Order_Number.Text, Client_Name, Sub_ProcessName, int.Parse(SESSION_ORDER_TASK.ToString()), Work_Type_Id, lbl_Order_Number.Text, lbl_Order_Task_Type.Text);
            //TaskQuestion.Show();

            ////Task_Question TaskQuestion = new Task_Question(Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()), userid, lbl_Order_Number.Text, Client_Name, Subclient);
            ////TaskQuestion.Show();


            //Thread t = new Thread((ThreadStart)delegate { System.Windows.Forms.Application.Run(new Ordermanagement_01.CheckList(userid, Client_id, Sub_ProcessId, SESSION_ORDER_TASK.ToString(), lbl_Order_Type.Text, lbl_Order_Task_Type.Text, Order_Id, USERCOUNT, AVAILABLE_COUNT, lbl_Order_Number.Text, Client_Name, Sub_ProcessName, int.Parse(SESSION_ORDER_TASK.ToString()), Work_Type_Id, lbl_Order_Number.Text, lbl_Order_Task_Type.Text, Order_Type_ABS_id)); });
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();


            //------ 19-apr-2019
            Thread t = new Thread((ThreadStart)delegate { Application.Run(new Check_List_New(userid, Client_id, Sub_ProcessId, SESSION_ORDER_TASK.ToString(), lbl_Order_Type.Text, lbl_Order_Task_Type.Text, Order_Id, USERCOUNT, AVAILABLE_COUNT, lbl_Order_Number.Text, Client_Name, Sub_ProcessName, int.Parse(SESSION_ORDER_TASK.ToString()), Work_Type_Id, lbl_Order_Number.Text, lbl_Order_Task_Type.Text, Order_Type_ABS_id)); });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();





            //Ordermanagement_01.CheckList TaskQuestion = new Ordermanagement_01.CheckList(userid, Client_id, Sub_ProcessId, SESSION_ORDER_TASK.ToString(), lbl_Order_Type.Text, lbl_Order_Task_Type.Text, Order_Id, USERCOUNT, AVAILABLE_COUNT, lbl_Order_Number.Text, Client_Name, Sub_ProcessName, int.Parse(SESSION_ORDER_TASK.ToString()), Work_Type_Id, lbl_Order_Number.Text, lbl_Order_Task_Type.Text, Order_Type_ABS_id);
            //TaskQuestion.Show();

        }

        private void btn_County_Link_Click(object sender, EventArgs e)
        {
            EmployeeCounty_Link county_Link = new EmployeeCounty_Link(State_Id, County_Id, lbl_Order_Number.Text);
            county_Link.Show();
        }

        private void btn_Checklist_Click(object sender, EventArgs e)
        {
            if (ddl_order_Staus.Text == "COMPLETED")
            {
                Task_Question_Form();
                btn_submit.Enabled = true;
                this.Enabled = false;
            }
            else
            {
                MessageBox.Show("Kindly Select the Completed Status in Order Status");
            }
        }

        private void Send_Completed_Order_Email()
        {


            Ordermanagement_01.Completed_Order_Mail cm = new Completed_Order_Mail(Client_id, lbl_Order_Number.Text, Order_Id, userid, Sub_ProcessId, Order_Type_Id);


        }

        private void btn_Judgement_Period_Click(object sender, EventArgs e)
        {
            string State_ID = State_Id.ToString();
            Masters.Judgement_Period_Create_View js = new Masters.Judgement_Period_Create_View(userid, State_ID, roleid);
            js.Show();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //btn_Judgement_Period_Click(sender, e);
            //Emp_Alert();
            //btn_Employee_Order_Info_Click(sender, e);
            obj_Order_Details_List = new Order_Passing_Params()
            {
                Client_Id = Client_id,
                Sub_Client_Id = Sub_ProcessId,
                Order_Id = Order_Id,
                Order_Task_Id = Convert.ToInt32(SESSION_ORDER_TASK),
                Order_Type_Id = Order_Type_Id,
                Work_Type_Id = Work_Type_Id,
                Order_Type_Abs_Id = Order_Type_ABS_id,
                User_Id = userid,
                County_Id = County_Id,
                State_Id = State_Id,
                Form_View_Type = "",
            };
            Order_Instruction instructions = new Order_Instruction(obj_Order_Details_List);
            Invoke(new MethodInvoker(delegate { instructions.Show(); }));
            timer1.Enabled = false;
            //   this.Enabled = true;
        }

        private void ddl_Web_search_sites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Web_search_sites.SelectedIndex > 0)
            {
                if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
                {

                    int Website_User_PAssword_Id = int.Parse(ddl_Web_search_sites.SelectedValue.ToString());
                    if (Website_User_PAssword_Id == 43)
                    {

                        lbl_Enter_Website.Visible = true;
                        txt_Website.Visible = true;
                        lbl_man_enterweb.Visible = true;
                    }
                    else
                    {

                        lbl_Enter_Website.Visible = false;
                        txt_Website.Visible = false;
                        lbl_man_enterweb.Visible = false;
                    }


                }
                else
                {

                    lbl_Enter_Website.Visible = true;
                    txt_Website.Visible = true;
                }


            }
        }

        private void btn_Tax_due_dates_Click(object sender, EventArgs e)
        {
            string State_ID = State_Id.ToString();
            Ordermanagement_01.Employee.State_Wise_Tax_Due_Date tax = new Ordermanagement_01.Employee.State_Wise_Tax_Due_Date(userid, State_ID, roleid);
            tax.Show();
        }

        private void btn_Employee_Order_Info_Click(object sender, EventArgs e)
        {
            string State_ID = State_Id.ToString();
            Employee.Employee_Order_Information emp = new Employee.Employee_Order_Information(userid, State_ID, Order_Id, roleid, Work_Type_Id, Convert.ToInt32(SESSION_ORDER_TASK));
            emp.Show();
        }

        private void btn_Emp_Alert_Click(object sender, EventArgs e)
        {
            Employee.Employee_Alert_Message alert = new Employee.Employee_Alert_Message(Client_id, Sub_ProcessId, State_Id, County_Id, Order_Type_ABS_id, roleid);
            alert.Show();

            //Ordermanagement_01.Gen_Forms.notification_2 alert = new Ordermanagement_01.Gen_Forms.notification_2(Client_id, Sub_ProcessId, State_Id, County_Id, Order_Type_ABS_id, roleid);
            //alert.Show();

            //Ordermanagement_01.Gen_Forms.notification_2 alert = new Ordermanagement_01.Gen_Forms.notification_2();
            //alert.Show();



            //DiffuseDlgDemo.Notification notify = new DiffuseDlgDemo.Notification(userid, lbl_Order_Number.Text, Client_id, Sub_ProcessId, State_Id, County_Id, Order_Type_ABS_id);
            //notify.Show();
        }
        private void Emp_Alert()
        {
            //Hashtable ht = new Hashtable();
            //DataTable dt = new DataTable();
            //ht.Add("@Trans", "CHECK_ALL_CLIENT_SUB_ORDER_ST_COUNTY");
            //ht.Add("@Client_Id", Client_id);
            //dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            //if (dt.Rows.Count > 0)
            //{
            DiffuseDlgDemo.Notification notify = new DiffuseDlgDemo.Notification(userid, lbl_Order_Number.Text, Client_id, Sub_ProcessId, State_Id, County_Id, Order_Type_ABS_id, roleid);
            notify.Show();
            // }

            //        }
            //    }
            //    //DiffuseDlgDemo.Notification notify = new DiffuseDlgDemo.Notification(userid, lbl_Order_Number.Text, Client_id, Sub_ProcessId, State_Id, County_Id, Order_Type_ABS_id);
            //    //notify.Show();
            //    //txt_Order_Instructions.Text = dt.Rows[0]["Instructions"].ToString();

            //}
            //else
            //{
            //    //no action
            //}
        }

        private void btn_OrderSearhcerNotes_Click(object sender, EventArgs e)
        {
            //Create_Search_Notepad_Document();

            Ordermanagement_01.Employee.Search_NotePad form_Search_Note_Pad = new Employee.Search_NotePad(Order_Id, Work_Type_Id, userid, userid, int.Parse(SESSION_ORDER_TASK.ToString()), "Create", lbl_Order_Number.Text);

            Invoke(new MethodInvoker(delegate { form_Search_Note_Pad.Show(); }));


            //if (SESSION_ORDER_TASK == "2" || SESSION_ORDER_TASK == "3")
            //{
            //    Ordermanagement_01.Order_Searcher_Notes searcher_notes = new Ordermanagement_01.Order_Searcher_Notes(userid, roleid, Convert.ToString(Order_Id), lbl_customer_No.Text.ToString(), txt_Subprocess.Text.ToString(), lbl_Order_Number.Text.ToString(), SESSION_ORDER_TASK);
            //    searcher_notes.Show();
            //}
        }

        private void Create_Search_Notes()
        {

            if (btn_OrderSearhcerNotes.Visible != false)
            {

                Hashtable htup = new Hashtable();
                DataTable dtup = new DataTable();

                htup.Add("@Trans", "CHECK_SEARCH_DOC_BY_NAME");
                htup.Add("@OrderId", Order_Id);
                htup.Add("@User_Id", userid);
                if (Work_Type_Id == 1)
                {
                    if (SESSION_ORDER_TASK == "2")
                    {
                        htup.Add("@Document_Name", "Searcher Notes-Live");
                    }
                    else if (SESSION_ORDER_TASK == "3")
                    {
                        htup.Add("@Document_Name", "Search_QC Notes-Live");
                    }
                }
                else if (Work_Type_Id == 2)
                {
                    if (SESSION_ORDER_TASK == "2")
                    {
                        htup.Add("@Document_Name", "Searcher Notes-Rework");
                    }
                    else if (SESSION_ORDER_TASK == "3")
                    {
                        htup.Add("@Document_Name", "Search_QC Notes-Rework");
                    }


                }
                else if (Work_Type_Id == 3)
                {
                    if (SESSION_ORDER_TASK == "2")
                    {
                        htup.Add("@Document_Name", "Searcher Notes-Superqc");
                    }
                    else if (SESSION_ORDER_TASK == "3")
                    {
                        htup.Add("@Document_Name", "Search_QC Notes-Superqc");
                    }


                }
                dtup = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htup);

                if (dtup.Rows.Count > 0)
                {



                }
                else
                {
                    //INSERT
                    htup.Clear(); dtup.Clear();



                    htup.Add("@Trans", "INSERT");
                    htup.Add("@Order_ID", int.Parse(Order_Id.ToString()));
                    htup.Add("@File_Size", File_size);
                    if (SESSION_ORDER_TASK == "2")
                    {
                        htup.Add("@Document_Path", des);
                    }
                    else if (SESSION_ORDER_TASK == "3")
                    {
                        htup.Add("@Document_Path", des_qc);

                    }

                    if (Work_Type_Id == 1)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {
                            htup.Add("@Instuction", "Searcher Notes-Live");
                            htup.Add("@Document_Name", "Searcher Notes-Live");
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {
                            htup.Add("@Instuction", "Searcher Notes");
                            htup.Add("@Document_Name", "Search_QC Notes-Live");
                        }
                    }
                    else if (Work_Type_Id == 2)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {
                            htup.Add("@Instuction", "Searcher Notes-Rework");
                            htup.Add("@Document_Name", "Searcher Notes-Rework");
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {
                            htup.Add("@Instuction", "Search_QC Notes-Rework");
                            htup.Add("@Document_Name", "Search_QC Notes-Rework");
                        }


                    }
                    else if (Work_Type_Id == 3)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {
                            htup.Add("@Instuction", "Searcher Notes-Superqc");
                            htup.Add("@Document_Name", "Searcher Notes-Superqc");
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {
                            htup.Add("@Instuction", "Search_QC Notes-Superqc");
                            htup.Add("@Document_Name", "Search_QC Notes-Superqc");
                        }


                    }

                    htup.Add("@Extension", file_extension);

                    htup.Add("@Inserted_By", userid);
                    htup.Add("@Inserted_date", DateTime.Now);
                    dtup = dataaccess.ExecuteSP("Sp_Document_Upload", htup);


                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();



                    htin.Add("@Trans", "INSERT_NOTES");
                    htin.Add("@Names_Run", "");
                    htin.Add("@Effective_Date", "");
                    htin.Add("@Legal_Reference", "");
                    htin.Add("@Data_Depth", "");
                    htin.Add("@Deeds", "");
                    htin.Add("@Mortgages", "");
                    htin.Add("@Judgments_Liens", "");
                    htin.Add("@General_Comments", "");
                    htin.Add("@Closed_Items", "");
                    htin.Add("@Inserted_by", userid);
                    htin.Add("@OrderId", Order_Id);
                    htin.Add("@Order_Task_Id", int.Parse(SESSION_ORDER_TASK.ToString()));
                    htin.Add("@Client_Instruction", "");
                    htin.Add("@Additional_Documents", "");
                    dtin = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htin);


                }

            }
        }

        private void Create_Search_Notepad_Document()
        {

            Hashtable htup = new Hashtable();
            DataTable dtup = new DataTable();

            htup.Add("@Trans", "CHECK_SEARCH_DOC_BY_NAME");
            htup.Add("@OrderId", Order_Id);
            htup.Add("@User_Id", userid);
            if (Work_Type_Id == 1)
            {
                if (SESSION_ORDER_TASK == "2")
                {
                    Document_Name = "Searcher Notes-Live";
                    htup.Add("@Document_Name", "Searcher Notes-Live");
                }
                else if (SESSION_ORDER_TASK == "3")
                {
                    Document_Name = "Search_QC Notes-Live";
                    htup.Add("@Document_Name", "Search_QC Notes-Live");
                }
            }
            else if (Work_Type_Id == 2)
            {
                if (SESSION_ORDER_TASK == "2")
                {
                    Document_Name = "Searcher Notes-Rework";
                    htup.Add("@Document_Name", "Searcher Notes-Rework");
                }
                else if (SESSION_ORDER_TASK == "3")
                {
                    Document_Name = "Search_QC Notes-Rework";
                    htup.Add("@Document_Name", "Search_QC Notes-Rework");
                }


            }
            else if (Work_Type_Id == 3)
            {
                if (SESSION_ORDER_TASK == "2")
                {
                    Document_Name = "Searcher Notes-Superqc";
                    htup.Add("@Document_Name", "Searcher Notes-Superqc");
                }
                else if (SESSION_ORDER_TASK == "3")
                {
                    Document_Name = "Search_QC Notes-Superqc";
                    htup.Add("@Document_Name", "Search_QC Notes-Superqc");
                }


            }
            dtup = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htup);

            if (dtup.Rows.Count > 0)
            {


                Hashtable htopen_Notepad = new Hashtable();
                DataTable dtopen_Notepad = new DataTable();

                htopen_Notepad.Add("@Trans", "GET_SEARCH_NOT_PAD_BY_DOCUMENT_TYPE");

                htopen_Notepad.Add("@OrderId", Order_Id);
                htopen_Notepad.Add("@User_Id", userid);
                htopen_Notepad.Add("@Document_Name", Document_Name);
                dtopen_Notepad = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htopen_Notepad);

                try
                {
                    if (dtopen_Notepad.Rows.Count > 0)
                    {
                        string myFilePath = dtopen_Notepad.Rows[0]["Document_Path"].ToString();

                        System.Diagnostics.Process.Start("notepad.exe", myFilePath);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }



            }
            else
            {
                StringBuilder bs = new StringBuilder();
                bs.AppendLine("ORDER NO #      :" + " " + lbl_Order_Number.Text.ToString());
                bs.AppendLine("ADDRESS		   :" + " " + lbl_Property_Address.Text.ToString());
                bs.AppendLine("STATE		   :" + " " + lbl_State.Text.ToString());
                bs.AppendLine("COUNTY		   :" + " " + lbl_County.Text.ToString());
                bs.AppendLine("APN		       :" + " " + lbl_APN.Text.ToString());
                bs.AppendLine("BORROWER NAME   :" + " " + lbl_Barrower_Name.Text.ToString());
                bs.AppendLine("EFFECTIVE DATE  :" + " " + "");
                bs.AppendLine(Environment.NewLine);
                bs.AppendLine("Names Run	   :" + "" + "");
                bs.AppendLine("" + "" + Environment.NewLine);
                bs.AppendLine("Legal Reference :" + " " + "" + "" + Environment.NewLine);
                bs.AppendLine("Data Depth      :" + " " + "" + "" + Environment.NewLine);
                bs.AppendLine("Open Items:" + "" + Environment.NewLine);
                bs.AppendLine("Deeds:   " + "" + Environment.NewLine);
                bs.AppendLine("" + "" + Environment.NewLine);
                bs.AppendLine("Mortgages:   " + "" + Environment.NewLine);
                bs.AppendLine("" + "" + Environment.NewLine);
                bs.AppendLine("Judgments/Liens: " + "" + Environment.NewLine);
                bs.AppendLine("" + "" + Environment.NewLine);
                bs.AppendLine("Additional documents : " + "" + Environment.NewLine);
                bs.AppendLine("" + "" + Environment.NewLine);
                bs.AppendLine("Closed Items:" + "" + Environment.NewLine);
                bs.AppendLine("" + "" + Environment.NewLine);
                bs.AppendLine("General Comments:" + "" + Environment.NewLine);
                bs.AppendLine("" + "" + Environment.NewLine);
                bs.AppendLine("Client instructions/requirements:" + "" + Environment.NewLine);
                bs.AppendLine("" + "" + Environment.NewLine);
                if (Directory.Exists(@"C:\OMS_Notes"))
                {
                    if (Work_Type_Id == 1)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {
                            src = @"C:\OMS_Notes\Searcher Notes_Live-" + userid.ToString() + ".txt";
                            des = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_Notes_Live-" + userid + ".txt";
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {
                            src_qc = @"C:\OMS_Notes\Searcher_QC_Notes_Live-" + userid.ToString() + ".txt";
                            des_qc = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_QC_Notes Live-" + userid.ToString() + ".txt";
                        }
                    }
                    else if (Work_Type_Id == 2)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {

                            src = @"C:\OMS_Notes\Searcher_Notes_Rework-" + userid.ToString() + ".txt";
                            des = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_Notes_Rework-" + userid + ".txt";
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {
                            src_qc = @"C:\OMS_Notes\Searcher_QC_Notes_Rework-" + userid.ToString() + ".txt";
                            des_qc = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_QC_Notes_Rework-" + userid.ToString() + ".txt";
                        }
                    }
                    else if (Work_Type_Id == 3)
                    {

                        if (SESSION_ORDER_TASK == "2")
                        {

                            src = @"C:\OMS_Notes\Searcher_Notes_Superqc-" + userid.ToString() + ".txt";
                            des = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_Notes_Superqc-" + userid + ".txt";
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {
                            src_qc = @"C:\OMS_Notes\Searcher_QC_Notes_Superqc-" + userid.ToString() + ".txt";
                            des_qc = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_QC_Notes_Superqc-" + userid.ToString() + ".txt";
                        }
                    }


                }
                else
                {
                    //strisrc1=@"C:\OMS_Notes";
                    Directory.CreateDirectory(@"C:\OMS_Notes"); //Directory.CreateDirectory(src_qc);


                    if (Work_Type_Id == 1)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {
                            src = @"C:\OMS_Notes\Searcher Notes-" + userid.ToString() + ".txt";


                            des = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_Notes_Live-" + userid + ".txt";
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {
                            src_qc = @"C:\OMS_Notes\Searcher_QC_Notes_Live-" + userid.ToString() + ".txt";
                            des_qc = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_QC_Notes Live-" + userid.ToString() + ".txt";

                        }
                    }
                    else if (Work_Type_Id == 2)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {
                            src = @"C:\OMS_Notes\Searcher_Notes_Rework-" + userid.ToString() + ".txt";


                            des = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_Notes_Rework-" + userid + ".txt";
                        }
                        else if (SESSION_ORDER_TASK == "3")
                        {

                            src_qc = @"C:\OMS_Notes\Searcher_QC_Notes_Rework-" + userid.ToString() + ".txt";
                            des_qc = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_QC_Notes_Rework-" + userid.ToString() + ".txt";
                        }
                    }
                    else if (Work_Type_Id == 3)
                    {
                        if (SESSION_ORDER_TASK == "2")
                        {

                            src = @"C:\OMS_Notes\Searcher_Notes_Superqc-" + userid.ToString() + ".txt";


                            des = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_Notes_Superqc-" + userid + ".txt";
                        }

                        else if (SESSION_ORDER_TASK == "3")
                        {
                            src_qc = @"C:\OMS_Notes\Searcher_QC_Notes_Superqc-" + userid.ToString() + ".txt";
                            des_qc = @"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id + @"\Searcher_QC_Notes_Superqc-" + userid.ToString() + ".txt";

                        }

                    }

                }



                Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id);
                DirectoryEntry de = new DirectoryEntry(@"\\192.168.12.33\oms\" + Client_id + @"\" + Sub_ProcessId + @"\" + Order_Id, "administrator", "password1$");
                de.Username = "administrator";
                de.Password = "password1$";

                if (SESSION_ORDER_TASK == "2")
                {
                    if (Work_Type_Id == 1)
                    {
                        Document_Name = "Searcher Notes-Live";
                    }
                    else if (Work_Type_Id == 2)
                    {
                        Document_Name = "Searcher Notes-Rework";

                    }
                    else if (Work_Type_Id == 3)
                    {
                        Document_Name = "Searcher Notes-Superqc";


                    }
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
                else if (SESSION_ORDER_TASK == "3")
                {
                    if (Work_Type_Id == 1)
                    {
                        Document_Name = "Search_QC Notes-Live";
                    }
                    else if (Work_Type_Id == 2)
                    {
                        Document_Name = "Search_QC Notes-Rework";

                    }
                    else if (Work_Type_Id == 3)
                    {
                        Document_Name = "Search_QC Notes-Superqc";


                    }
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


                Create_Search_Notes();


                Hashtable htopen_Notepad = new Hashtable();
                DataTable dtopen_Notepad = new DataTable();

                htopen_Notepad.Add("@Trans", "GET_SEARCH_NOT_PAD_BY_DOCUMENT_TYPE");

                htopen_Notepad.Add("@OrderId", Order_Id);
                htopen_Notepad.Add("@User_Id", userid);
                htopen_Notepad.Add("@Document_Name", Document_Name);
                dtopen_Notepad = dataaccess.ExecuteSP("Sp_Order_Searcher_Notes", htopen_Notepad);

                try
                {
                    if (dtopen_Notepad.Rows.Count > 0)
                    {
                        string myFilePath = dtopen_Notepad.Rows[0]["Document_Path"].ToString();

                        System.Diagnostics.Process.Start("notepad.exe", myFilePath);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
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





        private void txt_No_of_documents_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
      e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_No_Of_Hits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
      e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (Work_Type_Id == 1)
            {

                Update_User_Order_Time_Info();
            }
            else if (Work_Type_Id == 2)
            {
                Update_Rework_User_Order_Time_Info();

            }
            else if (Work_Type_Id == 3)
            {

                Update_Super_Qc_User_Order_Time_Info();
            }



            // this is commented for Server issue

            //if (Work_Type_Id == 1)
            //{
            //    MAX_TIME_ID = Max_Time_Id;
            //    Hashtable htComments = new Hashtable();
            //    DataTable dtComments = new System.Data.DataTable();

            //    DateTime date1 = new DateTime();
            //    date1 = DateTime.Now;
            //    string dateeval1 = date1.ToString("dd/MM/yyyy");
            //    string time1 = date1.ToString("hh:mm tt");

            //    htComments.Add("@Trans", "UPDATE_ON_TIME");
            //    htComments.Add("@Order_Time_Id", MAX_TIME_ID);
            //    htComments.Add("@End_Time", date1);
            //    dtComments = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htComments);
            //}
        }

        private void ddl_Issue_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Issue_Category.SelectedIndex > 0)
            {

                txt_Delay_Text.Enabled = true;
            }
            else
            {
                txt_Delay_Text.Enabled = false;
                txt_Delay_Text.Text = "";

            }
        }


        private bool Validate_Tax_Resending()
        {


            if (Tax_Completed_Count > 0 && txt_Tax_Comments.Text == "")
            {


                MessageBox.Show("Please Enter Tax Comments to Resend to the Tax Team");
                txt_Tax_Comments.Focus();

                return false;
            }
            else
            {

                return true;
            }

        }

        private void btn_Send_Tax_Request_Click(object sender, EventArgs e)
        {
            dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Hashtable htselect_Orderid = new Hashtable();
                DataTable dtselect_Orderid = new System.Data.DataTable();
                htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
                htselect_Orderid.Add("@Client_Order_Number", lbl_Order_Number.Text);
                dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);
                Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_ID"].ToString());
                Message_Count = 0;
                if (Order_Id != null)
                {


                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "CHECK_ORDER");
                    htcheck.Add("@Order_Id", Order_Id);
                    dtcheck = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
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
                        Insert_Tax_Order_Status(Order_Id);
                    }
                    else
                    {
                        // Check Tax Order Comepleted or Not

                        Hashtable ht_Check_Tax_Order_Completed = new Hashtable();
                        DataTable dt_Check_Tax_Order_Completed = new DataTable();

                        ht_Check_Tax_Order_Completed.Add("@Trans", "CHECK_ORDER_COMPLETED");
                        ht_Check_Tax_Order_Completed.Add("@Order_Id", Order_Id);
                        dt_Check_Tax_Order_Completed = dataaccess.ExecuteSP("Sp_Tax_Order_Status", ht_Check_Tax_Order_Completed);



                        if (dt_Check_Tax_Order_Completed.Rows.Count > 0)
                        {


                            Tax_Completed_Count = int.Parse(dt_Check_Tax_Order_Completed.Rows[0]["count"].ToString());

                        }
                        else
                        {

                            Tax_Completed_Count = 0;
                        }




                    }


                    if (Tax_Completed_Count > 0)
                    {

                        dialogResult = MessageBox.Show("Tax Certificate Already Recived Still You want to Resubmit to Tax Team?", "Warning", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            txt_Tax_Comments.ReadOnly = false;

                            if (Validate_Tax_Resending() != false)
                            {


                                Message_Count = 1;

                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                                htupdate.Add("@Order_ID", Order_Id);
                                htupdate.Add("@Search_Tax_Request", "True");

                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                                Hashtable httaxupdate = new Hashtable();
                                System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                                httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
                                httaxupdate.Add("@Order_ID", Order_Id);
                                httaxupdate.Add("@Search_Tax_Request_Progress", 14);

                                dttaxupdate = dataaccess.ExecuteSP("Sp_Order", httaxupdate);




                                // Check the Order is Reassigned Queue or Not

                                Hashtable htcheck_Reassigned = new Hashtable();
                                DataTable dtcheck_Reassigned = new DataTable();

                                htcheck_Reassigned.Add("@Trans", "CHECK_ORDER_REASSIGNED_QUEUE");
                                htcheck_Reassigned.Add("@Order_Id", Order_Id);
                                dtcheck_Reassigned = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htcheck_Reassigned);

                                int Check_Reassigned_Count = 0;
                                if (dtcheck_Reassigned.Rows.Count > 0)
                                {

                                    Check_Reassigned_Count = int.Parse(dtcheck_Reassigned.Rows[0]["count"].ToString());
                                }
                                else
                                {
                                    Check_Reassigned_Count = 0;

                                }

                                if (Check_Reassigned_Count == 0)
                                {

                                    Hashtable httax = new Hashtable();
                                    DataTable dttax = new DataTable();

                                    httax.Add("@Trans", "INSERT");
                                    httax.Add("@Order_Id", Order_Id);
                                    httax.Add("@Order_Task", 22);
                                    httax.Add("@Order_Status", 8);
                                    httax.Add("@Tax_Task", 3);
                                    httax.Add("@Tax_Status", 6);
                                    httax.Add("@Inserted_By", userid);
                                    httax.Add("@Status", "True");
                                    dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);

                                }
                                else
                                {
                                    Hashtable httax = new Hashtable();
                                    DataTable dttax = new DataTable();

                                    httax.Add("@Trans", "UPDTAE_ASSIGNED_DATE");
                                    httax.Add("@Order_Id", Order_Id);
                                    dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);

                                }



                                //OrderHistory
                                Hashtable ht_Order_History = new Hashtable();
                                DataTable dt_Order_History = new DataTable();
                                ht_Order_History.Add("@Trans", "INSERT");
                                ht_Order_History.Add("@Order_Id", Order_Id);
                                ht_Order_History.Add("@User_Id", userid);
                                ht_Order_History.Add("@Status_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString()));
                                ht_Order_History.Add("@Progress_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString()));
                                ht_Order_History.Add("@Work_Type", 1);
                                ht_Order_History.Add("@Assigned_By", userid);
                                ht_Order_History.Add("@Modification_Type", "Order Send to Search Tax Request; Comments:" + txt_Tax_Comments.Text.Trim() + "");
                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                            }



                        }
                    }
                    else
                    {
                        txt_Tax_Comments.ReadOnly = true;

                        Message_Count = 1;
                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                        htupdate.Add("@Order_ID", Order_Id);
                        htupdate.Add("@Search_Tax_Request", "True");

                        dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                        Hashtable httaxupdate = new Hashtable();
                        System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                        httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
                        httaxupdate.Add("@Order_ID", Order_Id);
                        httaxupdate.Add("@Search_Tax_Request_Progress", 14);

                        dttaxupdate = dataaccess.ExecuteSP("Sp_Order", httaxupdate);


                        int Tax_Order_Task = 0; int Tax_Order_Status = 0;
                        Hashtable ht_Check_Tax_Order_Task_Status = new Hashtable();
                        DataTable dt_Check_Tax_Order_Task_Status = new DataTable();

                        ht_Check_Tax_Order_Task_Status.Add("@Trans", "SELECT_TAX_ORDER_TASK_STATUS");
                        ht_Check_Tax_Order_Task_Status.Add("@Order_Id", Order_Id);
                        dt_Check_Tax_Order_Task_Status = dataaccess.ExecuteSP("Sp_Tax_Order_Status", ht_Check_Tax_Order_Task_Status);

                        if (dt_Check_Tax_Order_Task_Status.Rows.Count > 0)
                        {

                            Tax_Order_Task = int.Parse(dt_Check_Tax_Order_Task_Status.Rows[0]["Tax_Task"].ToString());
                            Tax_Order_Status = int.Parse(dt_Check_Tax_Order_Task_Status.Rows[0]["Tax_Status"].ToString());

                        }


                        Hashtable ht_Update_Tax_Order_Task = new Hashtable();
                        DataTable dt_Update_Tax_Order_Task = new DataTable();

                        ht_Update_Tax_Order_Task.Add("@Trans", "UPDATE_TAX_TASK_AND_STATUS");
                        ht_Update_Tax_Order_Task.Add("@Order_Id", Order_Id);

                        ht_Update_Tax_Order_Task.Add("@Tax_Task", Tax_Order_Task);
                        ht_Update_Tax_Order_Task.Add("@Tax_Status", 6);
                        ht_Update_Tax_Order_Task.Add("@Modified_By", userid);
                        ht_Update_Tax_Order_Task.Add("@Status", "True");
                        dt_Update_Tax_Order_Task = dataaccess.ExecuteSP("Sp_Tax_Order_Status", ht_Update_Tax_Order_Task);



                        //OrderHistory
                        Hashtable ht_Order_History = new Hashtable();
                        DataTable dt_Order_History = new DataTable();
                        ht_Order_History.Add("@Trans", "INSERT");
                        ht_Order_History.Add("@Order_Id", Order_Id);
                        ht_Order_History.Add("@User_Id", userid);
                        ht_Order_History.Add("@Status_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString()));
                        ht_Order_History.Add("@Progress_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString()));
                        ht_Order_History.Add("@Work_Type", 1);
                        ht_Order_History.Add("@Assigned_By", userid);
                        ht_Order_History.Add("@Modification_Type", "Order Send to Search Tax Request; Comments:" + txt_Tax_Comments.Text.Trim() + "");
                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                    }






                }
            }
            else
            {


            }

            if (Message_Count == 1)
            {


                MessageBox.Show("Order Send to Search Tax Request");
                Check_Tax_Request();
                Enable_Tax();
                Message_Count = 0;
            }
        }


        private void Insert_Tax_Order_Status(int Order_Id)
        {



            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", Order_Id);
            httax.Add("@Order_Task", 22);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", 1);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataaccess.ExecuteSP("Sp_Tax_Order_Status", httax);



        }

        private bool check_Order_In_Tax_Queau(int Order_Id)
        {

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_ORDER");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
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

                return true;
            }
            else
            {
                MessageBox.Show("This Order is alreaday Sent for Tax Request");
                return false;
            }
        }


        private bool check_Order_In_Tax_Queau_For_Cancel(int Order_Id)
        {

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_ORDER");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
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
                MessageBox.Show("This Order is not yet Sent for Tax Request");
                return false;


            }
            else
            {

                return true;
            }
        }

        private void btn_Cancel_Tax_Request_Click(object sender, EventArgs e)
        {

            dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Hashtable htselect_Orderid = new Hashtable();
                DataTable dtselect_Orderid = new System.Data.DataTable();
                htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
                htselect_Orderid.Add("@Client_Order_Number", lbl_Order_Number.Text);
                dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);
                Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                int Tax_User_Order_Diff_Time = 0;
                if (Order_Id != null && check_Order_In_Tax_Queau_For_Cancel(Order_Id) != false)
                {
                    Message_Count = 1;


                    Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                    DataTable dt_Get_Tax_Diff_Time = new DataTable();

                    ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                    ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id);
                    dt_Get_Tax_Diff_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

                    if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
                    {

                        Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());

                    }
                    else
                    {

                        Tax_User_Order_Diff_Time = 0;
                    }



                    if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                    {

                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                        htupdate.Add("@Order_ID", Order_Id);
                        htupdate.Add("@Search_Tax_Request", "False");

                        dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                        // Cancelling the Order in Tax

                        Hashtable htupdateOrderTaxStatus = new Hashtable();
                        System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
                        Hashtable htupdateTaxStatus = new Hashtable();
                        System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();


                        htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                        htupdateTaxStatus.Add("@Tax_Status", 4);
                        htupdateTaxStatus.Add("@Modified_By", userid);
                        htupdateTaxStatus.Add("@Order_Id", Order_Id);
                        dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);


                        //OrderHistory
                        Hashtable ht_Order_History = new Hashtable();
                        DataTable dt_Order_History = new DataTable();
                        ht_Order_History.Add("@Trans", "INSERT");
                        ht_Order_History.Add("@Order_Id", Order_Id);
                        ht_Order_History.Add("@User_Id", userid);
                        ht_Order_History.Add("@Status_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString()));
                        ht_Order_History.Add("@Progress_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString()));
                        ht_Order_History.Add("@Work_Type", 1);
                        ht_Order_History.Add("@Assigned_By", userid);
                        ht_Order_History.Add("@Modification_Type", "Tax Request Cancelled");
                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);
                    }

                    else
                    {

                        if (Tax_User_Order_Diff_Time != 0 && Tax_User_Order_Diff_Time < 30)
                        {

                            dialogResult = MessageBox.Show("Tax Team is Processing Order do you want to Cancel Tax Request?", "Warning", MessageBoxButtons.YesNo);

                            if (dialogResult == DialogResult.Yes)
                            {


                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                                htupdate.Add("@Order_ID", Order_Id);
                                htupdate.Add("@Search_Tax_Request", "False");

                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);



                                //OrderHistory
                                Hashtable ht_Order_History = new Hashtable();
                                DataTable dt_Order_History = new DataTable();
                                ht_Order_History.Add("@Trans", "INSERT");
                                ht_Order_History.Add("@Order_Id", Order_Id);
                                ht_Order_History.Add("@User_Id", userid);
                                ht_Order_History.Add("@Status_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString()));
                                ht_Order_History.Add("@Progress_Id", int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString()));
                                ht_Order_History.Add("@Work_Type", 1);
                                ht_Order_History.Add("@Assigned_By", userid);
                                ht_Order_History.Add("@Modification_Type", "Tax Request Cancelled");
                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                            }
                            else
                            {





                            }
                        }


                    }


                }

            }
            else
            {


            }

            if (Message_Count == 1)
            {

                MessageBox.Show("Tax Request Cancelled");
                Check_Tax_Request();
                Enable_Tax();
                Message_Count = 0;
            }


        }


        private void Enable_Tax()
        {
            if (ddl_order_Task.SelectedIndex >= 0)
            {

                string ss = ddl_order_Task.SelectedItem.ToString();

                if (ss == "Upload Completed" && Internal_Tax_Check == 1)
                {
                    ddl_Tax_Task.Visible = true;
                    lbl_tax.Visible = true;


                }
                else
                {

                    ddl_Tax_Task.Visible = false;
                    lbl_tax.Visible = false;
                }

            }



        }


        // 25000 Client and search Completed and 200 Sub Client Id

        private void Enable_Tax_Client_Wise_Task_Wise()
        {
            if (SESSION_ORDER_TASK == "2" && Sub_ProcessId == 200)
            {
                if (ddl_order_Staus.SelectedIndex > 0)
                {

                    string ss = ddl_order_Staus.SelectedValue.ToString();

                    if (ss == "3")
                    {
                        ddl_Tax_Task.Visible = true;
                        lbl_tax.Visible = true;


                    }
                    else
                    {

                        ddl_Tax_Task.Visible = false;
                        lbl_tax.Visible = false;
                    }

                }
            }


        }




        private void ddl_order_Task_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Tax Enabled Done separate for 25000 Clinet and Subclient
            if (Sub_ProcessId != 200)
            {
                Enable_Tax();
            }
            // Enable Email Sending Option for 32000 Client

            if (ddl_order_Task.SelectedIndex >= 0)
            {

                string ss = ddl_order_Task.SelectedItem.ToString();

                if (ss == "Upload Completed" && Client_id == 33)
                {
                    Panel_Email.Visible = true;
                }
                else
                {

                    Panel_Email.Visible = false;
                }
            }
        }

        private void btn_Pxt_File_Form_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee.PXT_File_Form_Entry pxtfile = new Ordermanagement_01.Employee.PXT_File_Form_Entry(userid, Order_Id, Client_Name, Subclient, lbl_Order_Number.Text);

            pxtfile.Show();
        }

        private void btn_Genrate_Invoice_Click(object sender, EventArgs e)
        {

            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            if (dt_Order_InTitleLogy.Rows.Count > 0)
            {
                Ordermanagement_01.InvoiceRep.Titlelogy_Invoice_Entry tinv = new InvoiceRep.Titlelogy_Invoice_Entry(Order_Id, userid, Order_Type_Id);
                tinv.Show();
            }
            else
            {

                MessageBox.Show("This Order is Not Imported From Titlelogy");
            }
        }

        private void lbl_No_of_Documents_Click(object sender, EventArgs e)
        {

        }



        private void chk_Plat_Yes_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Plat_Yes.Checked == true)
            {

                chk_plat_No.Checked = false;

            }

        }

        private void chk_Tax_Yes_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Tax_Yes.Checked == true)
            {

                chk_Tax_No.Checked = false;

            }


        }

        // this is for DB Titlep-Peak Title Client Orders
        private void Load_Titlelogy_Invoice_Pages_and_Price_Details()
        {

            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
            dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

            if (dt_Order_InTitleLogy.Rows.Count > 0)
            {

                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());

            }
            else { External_Client_Order_Id = 0; }



            if (External_Client_Order_Id > 0)
            {
                Hashtable htget = new Hashtable();
                DataTable dtget = new DataTable();

                htget.Add("@Trans", "SELECT_BY_TASK");
                htget.Add("@Order_Id", External_Client_Order_Id);
                htget.Add("@Order_Task", SESSION_ORDER_TASK);
                dtget = dataaccess.ExecuteSP("Sp_External_Client_Orders_Invoice_Check_List_Details", htget);

                if (dtget.Rows.Count > 0)
                {

                    Chk_Plat_Map = Convert.ToBoolean(dtget.Rows[0]["Plat_Map"].ToString());
                    Chk_Tax_Information = Convert.ToBoolean(dtget.Rows[0]["Tax_Information"].ToString());




                }
                else
                {
                    Chk_Plat_Map = false;
                    Chk_Tax_Information = false;
                }

                if (Chk_Plat_Map == true)
                {

                    chk_Plat_Yes.Checked = true;
                    chk_plat_No.Checked = false;
                }

                if (Chk_Tax_Information == true)
                {

                    chk_Tax_Yes.Checked = true;
                    chk_Tax_No.Checked = false;

                }



                Hashtable htget1 = new Hashtable();
                DataTable dtget1 = new DataTable();

                htget1.Add("@Trans", "SELECT");
                htget1.Add("@Order_ID", External_Client_Order_Id);
                dtget1 = dataaccess.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", htget1);

                if (dtget1.Rows.Count > 0)
                {

                    txt_Invoice_No_Of_Pages.Text = dtget1.Rows[0]["No_Of_Pages"].ToString();
                    txt_Platmap_Pages.Text = dtget1.Rows[0]["Plat_Map_Pages"].ToString();
                    txt_Probate_Pages.Text = dtget1.Rows[0]["Probate_Pages"].ToString();


                }
                else
                {
                    txt_Invoice_No_Of_Pages.Text = "";
                    txt_Platmap_Pages.Text = "";
                    txt_Probate_Pages.Text = "";

                }

            }
        }

        private void chk_plat_No_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_plat_No.Checked == true)
            {

                chk_Plat_Yes.Checked = false;
            }
        }

        private void chk_Tax_No_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_Tax_No.Checked == true)
            {

                chk_Tax_Yes.Checked = false;

                MessageBox.Show("Send Tax Request on Clicking Send Tax Request Button");
            }
        }

        private void txt_Order_No_Of_Pages_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Searcher_Link_Click(object sender, EventArgs e)
        {

            Ordermanagement_01.Employee.Searcher_New_Link_history Search_LinkHistory = new Ordermanagement_01.Employee.Searcher_New_Link_history(Order_Id, int.Parse(SESSION_ORDER_TASK.ToString()), userid, int.Parse(roleid.ToString()), lbl_Order_Number.Text, County_Id);
            Search_LinkHistory.Show();

        }

        private void chk_Email_Yes_CheckStateChanged(object sender, EventArgs e)
        {

            if (chk_Email_Yes.Checked == true)
            {
                Chk_Email_No.Checked = false;
            }
        }

        private void Chk_Email_No_CheckStateChanged(object sender, EventArgs e)
        {

            if (Chk_Email_No.Checked == true)
            {
                chk_Email_Yes.Checked = false;
            }
        }


        private bool Validate_Email_Check()
        {
            string Check_Task = ddl_order_Task.Text.ToString();

            if (Client_id == 33 && Check_Task == "Upload Completed")
            {


                if (chk_Email_Yes.Checked == false && Chk_Email_No.Checked == false)
                {


                    MessageBox.Show("Slect Email Option");

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

        private void btn_TypingEntry_Click(object sender, EventArgs e)
        {

            // System.Diagnostics.Process.Start("http://localhost:7928/Order_Entry_Portal1.0/Orders/EntryTyping_Document.aspx?Order_Id=" + Order_Id.ToString() + "&User_Id=" + userid.ToString() + "");
            //  System.Diagnostics.Process.Start("https://titlelogy.com/Typing/Orders/EntryTyping_Document.aspx?Order_Id=" + Order_Id.ToString() + "&User_Id=" + userid.ToString() + "");
            Order_Entry_Type_Document typingDoc = new Order_Entry_Type_Document(Order_Id, userid);
            typingDoc.Show();
        }
    }

}
