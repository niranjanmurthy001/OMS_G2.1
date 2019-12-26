using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using System.Collections;
using System.Globalization;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
//using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using ClosedXML.Excel;


namespace Ordermanagement_01.Employee
{
    public partial class Error_Details : Form
    {
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_Role, User_ID, Error_Count, subclientid, Couty_Id, ErrorUserId;
        string Production_Date;
        string Error_Type="", From_Date, To_Date, ErrorTabPage,  Type_Name;
        string OrderTask="", OrderStatus="",ErrorTab="";
        
        public Error_Details(int User_Id, int Roleid, string ErrorTypename, string typename, int ErrorCount, string Fromdate, string Todate, string ProductionDate, string Error_Tab_Page, int Error_User_Id,string Order_Task,string Order_Status)
        {
            InitializeComponent();

            User_ID = User_Id;
            User_Role = Roleid;
            Error_Count = ErrorCount;
            Error_Type = ErrorTypename;
            From_Date = Fromdate;
            To_Date = Todate;
            Production_Date = ProductionDate;
            ErrorTabPage = Error_Tab_Page;
            Type_Name = typename;

            ErrorUserId = Error_User_Id;
            OrderTask=Order_Task;
            OrderStatus = Order_Status;
             //ErrorTab = Error_Tab;
            Grd_Errors_Detail.Rows.Clear();
            Grd_Errors_Detail.DataSource = null;
        }

        private void Error_Details_Load(object sender, EventArgs e)
        {
            Grd_Errors_Detail.Rows.Clear();
            Grd_Errors_Detail.DataSource = null;
           
            if(ErrorTabPage=="Error_Tab")
            {
              
              Errors_Deatils();
              OrderTask = "";
              OrderStatus = "";
              Error_Type = "";
              ErrorUserId = 0;
            }
            else if (ErrorTabPage == "Error_Description")
            {
                Error_Desc_Details();
                OrderTask = "";
                OrderStatus = "";
                Error_Type = "";
                ErrorUserId = 0;
            }
            else if (ErrorTabPage == "Error_On_User")
            {
                Error_On_User_Details();
                OrderTask = "";
                OrderStatus = "";
                Error_Type = "";
                ErrorUserId = 0;
            }
            else if (ErrorTabPage == "Error_On_Client")
            {
                Error_On_Client_Details();
                OrderTask = "";
                OrderStatus = "";
                Error_Type = "";
                ErrorUserId = 0;
            }
            else if (ErrorTabPage == "Error_On_Subclient")
            {
                Error_On_Client_Details();
                OrderTask = "";
                OrderStatus = "";
                Error_Type = "";
                ErrorUserId = 0;
            }
            else if (ErrorTabPage == "Error_On_State")
            {
                Error_On_State_Details();
                OrderTask = "";
                OrderStatus = "";
                Error_Type = "";
                ErrorUserId = 0;
            }
            else if (ErrorTabPage == "Error_On_County")
            {
               
                Error_On_State_Details();
                OrderTask = "";
                OrderStatus = "";
                Error_Type = "";
                ErrorUserId = 0;
            }

            //if (OrderTask != "0" && OrderTask != "" && Error_Type == null && ErrorUserId == 0 && OrderStatus == "")
            //{
            //    Order_Task_Wise_Error_Deatils();
            //}
            this.WindowState = FormWindowState.Maximized;
        }

        private void Order_Task_Wise_Error_Deatils()
        {
            load_Progressbar.Start_progres();
            Hashtable htget = new Hashtable();
            DataTable dtget = new DataTable();
            htget.Clear();
            dtget.Clear();


            htget.Add("@Trans", "TASK_WISE_DETAILS");
            htget.Add("@Error_Task", OrderTask);
            htget.Add("@Error_From_Date", From_Date);
            htget.Add("@Error_To_Date", To_Date);

            dtget = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget);
            if (dtget.Rows.Count > 0)
            {

                Grd_Errors_Detail.Rows.Clear();
                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    Grd_Errors_Detail.Rows.Add();
                    Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;

                    Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role == 1)
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget.Rows[i]["Client_Name"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget.Rows[i]["Client_Number"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget.Rows[i]["Subprocess_Number"].ToString();

                    }

                    Grd_Errors_Detail.Rows[i].Cells[5].Value = dtget.Rows[i]["Work_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[6].Value = dtget.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
                    Grd_Errors_Detail.Rows[i].Cells[7].Value = dtget.Rows[i]["Error_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[8].Value = dtget.Rows[i]["Error_description"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[9].Value = dtget.Rows[i]["Comments"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[10].Value = dtget.Rows[i]["Error_On_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[11].Value = dtget.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget.Rows[i]["Error_Entered_From"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget.Rows[i]["Entered_Date"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget.Rows[i]["Reporting_1"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget.Rows[i]["Reporting_2"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget.Rows[i]["State"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget.Rows[i]["County"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget.Rows[i]["Order_Id"].ToString();

                    //if (User_Role == 2)
                    //{
                    //    Grd_Errors_Detail.Columns[12].Visible = false;
                    //    Grd_Errors_Detail.Columns[13].Visible = false;

                    //}
                }

                foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {

                Grd_Errors_Detail.Rows.Clear();
            }


        }


        private void Errors_Deatils()
        {
            load_Progressbar.Start_progres();
            Hashtable htget = new Hashtable();
            DataTable dtget = new DataTable();
            Grd_Errors_Detail.Rows.Clear();
            Grd_Errors_Detail.DataSource = null; 

            //user wise  
            htget.Clear();
            dtget.Clear();
            if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type == "" )
            {
               
                htget.Add("@Trans", "USER_WISE_DETAILS");
             
            }

            //user wise  and and error type
            //if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Error_Type != null)
            //{
            if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" )
            {
               
                htget.Add("@Trans", "GET_ERROR_TAb_USER_WISE_DETAILS");
             
            }
            // task wise and error type
            //else if (OrderTask != "" && ErrorUserId == 0 && OrderStatus == "" && Error_Type != "" && Error_Type != null)
            //{
            else if (OrderTask != "" && ErrorUserId == 0 && OrderStatus == "" && Error_Type != "" )
            {
              
                htget.Add("@Trans", "GET_ERRORTAB_ERROR_TASK_WISE_DETAILS");
              
            }
            // status and error type wise
            //else if (OrderStatus != "" && OrderTask == "" && ErrorUserId == 0 && Error_Type != "" && Error_Type != null)
            //{
            else if (OrderStatus != "" && OrderTask == "" && ErrorUserId == 0 && Error_Type != "" )
            {
              
                htget.Add("@Trans", "GET_ERRORTAB_ERROR_STATUS_WISE_DETAILS");
            
              
            }
            //task and user, error type  wise
            //else if (OrderTask != "" && ErrorUserId != 0 && OrderStatus == "" && Error_Type != "" && Error_Type != null)
            //{
            else if (OrderTask != "" && ErrorUserId != 0 && OrderStatus == "" && Error_Type != "" )
            {
              
                htget.Add("@Trans", "GET_ERRORTAB_TASK_AND_USER_WISE_DETAILS");
             
       
            }
            //status, user and error type wise
            //else if (OrderStatus != "" && ErrorUserId != 0 && OrderTask == "" && Error_Type != "" && Error_Type != null)
            //{
            else if (OrderStatus != "" && ErrorUserId != 0 && OrderTask == "" && Error_Type != "" )
            {

                htget.Add("@Trans", "GET_ERRORTAB_STATUS_AND_USER_WISE_DETAILS");
            }
            // task and status  and error type wise
            //else if (OrderTask != "" && ErrorUserId == 0 && OrderStatus != "" && Error_Type != "" && Error_Type != null)
            //{
            else if (OrderTask != "" && ErrorUserId == 0 && OrderStatus != "" && Error_Type != "" )
            {
             
                htget.Add("@Trans", "GET_ERRORTAB_ERROR_TASK_AND_STATUS_WISE_DETAILS");

            }
            // task, status and user and Error_Type wise
            //else if (OrderTask != "" && OrderStatus != "" && ErrorUserId != 0 && Error_Type != "" && Error_Type != null)
            //{
            else if (OrderTask != "" && OrderStatus != "" && ErrorUserId != 0 && Error_Type != "" )
            {
                htget.Add("@Trans", "GET_ERRORTAB_ERROR_TASK_AND_STATUS_AND_USER_WISE_DETAILS");
                
               
            }
            // task wise
            else if (OrderTask != "" && ErrorUserId == 0 && OrderStatus == "" && Error_Type == "")
            {
               
                htget.Add("@Trans", "TASK_WISE_DETAILS");
              
            }
            // status wise
            else if (OrderStatus != "" && ErrorUserId == 0 && OrderTask == "" && Error_Type == "")
            {

                htget.Add("@Trans", "STATUS_WISE_DETAILS");

            }
            // task and sttaus wise
            else if (OrderStatus != "" && OrderTask != "" && ErrorUserId == 0 && Error_Type == "")
            {
             
                htget.Add("@Trans", "TASK_STATUS_WISE_DETAILS");

            }

                 // task and sttaus and User wise
            else if (OrderStatus != "" && OrderTask != "" && ErrorUserId != 0 && Error_Type == "")
            {
               
                htget.Add("@Trans", "TASK_STATUS_USER_WISE_DETAILS");
              
            }
            // error type wise
            //else if (OrderStatus == "" && ErrorUserId == 0 && OrderTask == "" && Error_Type != "" && Error_Type != null)
            //{
            else if (OrderStatus == "" && ErrorUserId == 0 && OrderTask == "" && Error_Type != "" )
            {
                htget.Add("@Trans", "GET_ERROR_TAb_DETAILS");
                

            }

                //task and user wise
            else if (OrderStatus == "" && OrderTask != "" && ErrorUserId != 0 && Error_Type == "")
            {
              
                htget.Add("@Trans", "TASK_USER_WISE_DETAILS");
              
            }
            // Status and User wise
            else if (OrderStatus != "" && OrderTask == "" && ErrorUserId != 0 && Error_Type == "")
            {
             
                htget.Add("@Trans", "STATUS_USER_WISE_DETAILS");
               
            }
            // DATE WISE
            else if (OrderStatus == "" && OrderTask == "" && ErrorUserId == 0 && Error_Type == "")
            {
               
                htget.Add("@Trans", "DATE_WISE_DETAILS");
              
            }


            //else
            //{
            //     htget.Add("@Trans", "GET_ERROR_TAb_DETAILS");
            //     htget.Add("@Error_Type_Name", Error_Type);
            //}


            //htget.Add("@Error_Type_Name", Error_Type);

            htget.Add("@Error_From_Date", From_Date);
            htget.Add("@Error_To_Date", To_Date);
            htget.Add("@User_Id", ErrorUserId);
            htget.Add("@Error_Type_Name", Error_Type);
            htget.Add("@Error_OnTask", OrderTask);
            htget.Add("@Error_OnStatus", OrderStatus);
         

            dtget = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget);
            Grd_Errors_Detail.Rows.Clear();
            if (dtget.Rows.Count > 0)
            {

                Grd_Errors_Detail.Rows.Clear();
                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    Grd_Errors_Detail.Rows.Add();
                    Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;

                    Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role == 1)
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget.Rows[i]["Client_Name"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget.Rows[i]["Client_Number"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget.Rows[i]["Subprocess_Number"].ToString();
                       
                    }

                    Grd_Errors_Detail.Rows[i].Cells[5].Value =  dtget.Rows[i]["Work_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[6].Value =  dtget.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
                    Grd_Errors_Detail.Rows[i].Cells[7].Value =  dtget.Rows[i]["Error_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[8].Value =  dtget.Rows[i]["Error_description"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[9].Value =  dtget.Rows[i]["Comments"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[10].Value =  dtget.Rows[i]["Error_On_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[11].Value =  dtget.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget.Rows[i]["Error_Entered_From"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget.Rows[i]["Entered_Date"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget.Rows[i]["State"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget.Rows[i]["County"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget.Rows[i]["Reporting_1"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget.Rows[i]["Reporting_2"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget.Rows[i]["Order_Id"].ToString();

                    //if (User_Role == 2)
                    //{
                    //    Grd_Errors_Detail.Columns[12].Visible = false;
                    //    Grd_Errors_Detail.Columns[13].Visible = false;
                       
                    //}
                }

                foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
                {
                    row.Height = 50;
                }
              
            }
            else
            {

                Grd_Errors_Detail.Rows.Clear();
            }


        }


        private void Error_Desc_Details()
        {
            load_Progressbar.Start_progres();
            Hashtable htget_Errdesc = new Hashtable();
            DataTable dtget_Errdesc = new DataTable();
            Grd_Errors_Detail.Rows.Clear();
            Grd_Errors_Detail.DataSource = null;


            //if (ErrorUserId != 0)
            //{
            //    htget_Errdesc.Add("@Trans", "GET_ERROR_DESC_USER_WISE_DETAILS");
            //    htget_Errdesc.Add("@User_Id", ErrorUserId);
            //}
            //else
            //{
            //    htget_Errdesc.Add("@Trans", "GET_ERROR_DESC_DETAILS");

            //}

           

           
            htget_Errdesc.Clear();
            dtget_Errdesc.Clear();
            //1)user wise  
            if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_USER_WISE_DETAILS");
            }

            //2)user  and error Field
            if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" )
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_USER_AND_FIELD_WISE_DETAILS");
            }
            //3)user   and Task
            if (ErrorUserId != 0 && OrderTask != "" && OrderStatus == "" && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_USER_AND_TASK_WISE_DETAILS");
            }
            // 4)user   and Status
            if (ErrorUserId != 0 && OrderTask == "" && OrderStatus != "" && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_USER_AND_STATUS_WISE_DETAILS");
            }
            // 5) user  and Task and Status
            if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_USER_AND_TASK_AND_STATUS_WISE_DETAILS");
            }
            // 6)user  and Task and Status and Field 
            if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "")
            {

                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_USER_AND_TASK_AND_STATUS_AND_FIELD_WISE_DETAILS");
            }
            // 7)task wise
            else if (OrderTask != "" && ErrorUserId == 0 && OrderStatus == "" && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_TASK_WISE_DETAILS");
            }
            // 8)status wise
            else if (OrderStatus != "" && ErrorUserId == 0 && OrderTask == "" && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_STATUS_WISE_DETAILS");
            }
            // 9)task and sttaus wise
            else if (OrderStatus != "" && OrderTask != "" && ErrorUserId == 0 && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_TASK_AND_STATUS_WISE_DETAILS");
            }
            // 10)FIELD wise
            else if (OrderStatus == "" && ErrorUserId == 0 && OrderTask == "" && Error_Type != "" )
            {
                htget_Errdesc.Add("@Trans", "GET_ERROR_FIELD_WISE_DETAILS");
            }
              // 11)FIELD and task wise
            else if (OrderStatus == "" && ErrorUserId == 0 && OrderTask != "" && Error_Type != "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_TASK_AND_FIELD_WISE_DETAILS");
            }
            // 12)FIELD and Status wise
            else if (OrderStatus != "" && ErrorUserId == 0 && OrderTask == "" && Error_Type != "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_STATUS_AND_FIELD_WISE_DETAILS");
            }
            // 13)FIELD and TASK AND Status wise
            else if (OrderStatus != "" && ErrorUserId == 0 && OrderTask != "" && Error_Type != "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_TASK_AND_STATUS_AND_FIELD_WISE_DETAILS");
            }
            //// 14)FIELD and TASK AND Status and User wise
            //else if (OrderStatus != "" && ErrorUserId != 0 && OrderTask != "" && Error_Type != "")
            //{
            //    htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_TASK_AND_STATUS_AND_FIELD_NAD_USER_WISE_DETAILS");
            //}

            //15) DATE WISE
            else if (OrderStatus == "" && OrderTask == "" && ErrorUserId == 0 && Error_Type == "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_DATE_WISE_DETAILS");
            }
            // 16)FIELD and Status and USer wise
            else if (OrderStatus != "" && ErrorUserId != 0 && OrderTask == "" && Error_Type != "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_STATUS_AND_FIELD_AND_USER_WISE_DETAILS");
            }
            // 17)FIELD and Task and USer wise
            else if (OrderStatus == "" && ErrorUserId != 0 && OrderTask != "" && Error_Type != "")
            {
                htget_Errdesc.Add("@Trans", "GET_ERRORFIELD_TASK_AND_FIELD_AND_USER_WISE_DETAILS");
            }
            

            
            htget_Errdesc.Add("@Error_From_Date", From_Date);
            htget_Errdesc.Add("@Error_To_Date", To_Date);
            htget_Errdesc.Add("@Error_OnTask", OrderTask);
            htget_Errdesc.Add("@Error_OnStatus", OrderStatus);
            htget_Errdesc.Add("@Error_description", Error_Type);
            htget_Errdesc.Add("@User_Id", ErrorUserId);
           

            dtget_Errdesc = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Errdesc);
            if (dtget_Errdesc.Rows.Count > 0)
            {
                Grd_Errors_Detail.Rows.Clear();
                for (int i = 0; i < dtget_Errdesc.Rows.Count; i++)
                {
                    Grd_Errors_Detail.Rows.Add();
                    Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;
                    Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget_Errdesc.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role == 1)
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_Errdesc.Rows[i]["Client_Name"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_Errdesc.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_Errdesc.Rows[i]["Client_Number"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_Errdesc.Rows[i]["Subprocess_Number"].ToString();

                    }

                    Grd_Errors_Detail.Rows[i].Cells[5].Value = dtget_Errdesc.Rows[i]["Work_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[6].Value = dtget_Errdesc.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
                    Grd_Errors_Detail.Rows[i].Cells[7].Value = dtget_Errdesc.Rows[i]["Error_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[8].Value = dtget_Errdesc.Rows[i]["Error_description"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[9].Value = dtget_Errdesc.Rows[i]["Comments"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[10].Value = dtget_Errdesc.Rows[i]["Error_On_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[11].Value = dtget_Errdesc.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget_Errdesc.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget_Errdesc.Rows[i]["Error_Entered_From"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget_Errdesc.Rows[i]["Entered_Date"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget_Errdesc.Rows[i]["Reporting_1"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget_Errdesc.Rows[i]["Reporting_2"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget_Errdesc.Rows[i]["State"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget_Errdesc.Rows[i]["County"].ToString();

                  

                    Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget_Errdesc.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget_Errdesc.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget_Errdesc.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget_Errdesc.Rows[i]["Order_Id"].ToString();


                    //if (User_Role == 2)
                    //{
                    //    Grd_Errors_Detail.Columns[12].Visible = false;
                    //    Grd_Errors_Detail.Columns[13].Visible = false;
                    //    //Grd_Errors_Detail.Columns[4].Visible = false;
                    //}
                }

                foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {

                Grd_Errors_Detail.Rows.Clear();
            }


        }

        private void Error_On_User_Details()
        {
            load_Progressbar.Start_progres();
            Hashtable htget_ErrorOnuser = new Hashtable();
            DataTable dtget_ErrorOnuser = new DataTable();

            Grd_Errors_Detail.Rows.Clear();
            Grd_Errors_Detail.DataSource = null;

            htget_ErrorOnuser.Clear();
            dtget_ErrorOnuser.Clear();

            // user  wise  // chart click 
            if (Error_Type == "" && OrderTask == "" && OrderStatus == "" && ErrorUserId != 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_USER_WISE_DETAILS");

            }
            //user (user id) and task wise   // chart click 
            if (Error_Type == "" && OrderTask != "" && OrderStatus == "" && ErrorUserId != 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_USER_AND_TASK_WISEDETAILS");

            }
            //user and status wise   // chrat click wise
            if (Error_Type == "" && OrderTask == "" && OrderStatus != "" && ErrorUserId != 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_USER_AND_STATUS_WISEDETAILS");

            }
            //user (user id) and task and status wise // Chart control click
            if (Error_Type == "" && OrderTask != "" && OrderStatus != "" && ErrorUserId != 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_USER_TASK_STATUS_WISEDETAILS");

            }


            //// Error Type and USer wise 
            //if (Error_Type != "" && OrderTask == "" && OrderStatus == "" && ErrorUserId != 0)
            //{
            //    htget_ErrorOnuser.Add("@Trans", "GET_ERROR_TYPE_USER_WISEDETAILS");

            //}

            // Error Type  wise // grid click
            if (Error_Type != "" && OrderTask == "" && OrderStatus == "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_ERROR_TYPE_DETAILS");

            }

            // task wise  // grid click 
            if (Error_Type == "" && OrderTask != "" && OrderStatus == "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_TASK_WISEDETAILS");

            }
            // status wise // grid click 
            if (Error_Type == "" && OrderTask == "" && OrderStatus != "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_STATUS_WISEDETAILS");

            }
            // task and Errro Type wise   // grid click
            if (Error_Type != "" && OrderTask != "" && OrderStatus == "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_ERROR_TYPE_AND_TASK_WISEDETAILS");

            }

            //status and ERROR TYPE wise // grid click 
            if (Error_Type != "" && OrderTask == "" && OrderStatus != "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_ERROR_TYPE_AND_STATUS_WISEDETAILS");

            }

            //TASk and status wise   // grdi click 
            if (Error_Type == "" && OrderTask != "" && OrderStatus != "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_TASK_AND_STATUS_WISEDETAILS");

            }
           
            //task and status and ERROR TYPE wise  // grid click 
            if (Error_Type != "" && OrderTask != "" && OrderStatus != "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_ERROR_TYPE_AND_TASK_AND_STATUS_WISEDETAILS");

            }

        
            
            ////task and status and ERROR TYPE and User wise   //
            //if (Error_Type != "" && OrderTask != "" && OrderStatus != "" && ErrorUserId != 0)
            //{
            //    htget_ErrorOnuser.Add("@Trans", "GET_ERROR_TYPE_AND_TASK_AND_STATUS_USER_WISEDETAILS");

            //}


            //DATE wise 
            if (Error_Type == "" && OrderTask == "" && OrderStatus == "" && ErrorUserId == 0)
            {
                htget_ErrorOnuser.Add("@Trans", "GET_ERROR_ON_DATE_WISE_DETAILS");

            }

           // htget_ErrorOnuser.Add("@Trans", "GET_ERROR_ONUSER_DETAILS");

           
            htget_ErrorOnuser.Add("@Error_From_Date", From_Date);
            htget_ErrorOnuser.Add("@Error_To_Date", To_Date);
            htget_ErrorOnuser.Add("@Error_On_User_Name", Error_Type);
            htget_ErrorOnuser.Add("@Error_OnTask", OrderTask);
            htget_ErrorOnuser.Add("@Error_OnStatus", OrderStatus);
            htget_ErrorOnuser.Add("@User_Id", ErrorUserId);

            dtget_ErrorOnuser = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_ErrorOnuser);
            if (dtget_ErrorOnuser.Rows.Count > 0)
            {
                Grd_Errors_Detail.Rows.Clear();
                for (int i = 0; i < dtget_ErrorOnuser.Rows.Count; i++)
                {
                    Grd_Errors_Detail.Rows.Add();
                    Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;
                    Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget_ErrorOnuser.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role == 1)
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_ErrorOnuser.Rows[i]["Client_Name"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_ErrorOnuser.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_ErrorOnuser.Rows[i]["Client_Number"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_ErrorOnuser.Rows[i]["Subprocess_Number"].ToString();

                    }

                    Grd_Errors_Detail.Rows[i].Cells[5].Value = dtget_ErrorOnuser.Rows[i]["Work_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[6].Value = dtget_ErrorOnuser.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
                    Grd_Errors_Detail.Rows[i].Cells[7].Value = dtget_ErrorOnuser.Rows[i]["Error_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[8].Value = dtget_ErrorOnuser.Rows[i]["Error_description"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[9].Value = dtget_ErrorOnuser.Rows[i]["Comments"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[10].Value = dtget_ErrorOnuser.Rows[i]["Error_On_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[11].Value = dtget_ErrorOnuser.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget_ErrorOnuser.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget_ErrorOnuser.Rows[i]["Error_Entered_From"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget_ErrorOnuser.Rows[i]["Entered_Date"].ToString();
                  
                    Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget_ErrorOnuser.Rows[i]["Reporting_1"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget_ErrorOnuser.Rows[i]["Reporting_2"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget_ErrorOnuser.Rows[i]["State"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget_ErrorOnuser.Rows[i]["County"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget_ErrorOnuser.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget_ErrorOnuser.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget_ErrorOnuser.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget_ErrorOnuser.Rows[i]["Order_Id"].ToString();


                    if (User_Role == 2)
                    {
                        Grd_Errors_Detail.Columns[10].Visible = false;
                        Grd_Errors_Detail.Columns[11].Visible = false;
                        //Grd_Errors_Detail.Columns[4].Visible = false;
                    }
                }

                foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {

                Grd_Errors_Detail.Rows.Clear();
            }


        }

        //private void Error_On_Client_Details()
        //{
        //    //load_Progressbar.Start_progres();
        //    Hashtable htget_Client = new Hashtable();
        //    DataTable dtget_Client = new DataTable();

        //    if (ErrorTabPage == "Error_On_Client")
        //    {
        //        if (ErrorUserId != 0)
        //        {
        //            htget_Client.Add("@Trans", "GET_ERROR_CLIENT_USER_WISE_DETAILS");
        //           // htget_Client.Add("@User_Id", ErrorUserId);
        //        }
        //        else
        //        {
        //            htget_Client.Add("@Trans", "GET_ERROR_CLIENT_DETAILS");

        //        }
                
        //         //htget_Client.Add("@Client_Name", Error_Type);

        //    }
        //    else if (ErrorTabPage == "Error_On_Subclient")
        //    {
        //        if (ErrorUserId != 0)
        //        {
        //            htget_Client.Add("@Trans", "GET_ERROR_CLIENT_SUBPROCESS_USER_WISE_DETAILS");
        //            //htget_Client.Add("@User_Id", ErrorUserId);
        //        }
        //        else
        //        {
        //            htget_Client.Add("@Trans", "GET_ERROR_CLIENT_SUBPROCESS_DETAILS");
                   
        //        }
        //        //htget_Client.Add("@SuProcess_Name", Error_Type);
        //        //htget_Client.Add("@Client_Name", Type_Name);
        //    }


        //    htget_Client.Add("@Error_From_Date", From_Date);
        //    htget_Client.Add("@Error_To_Date", To_Date);
        //    htget_Client.Add("@Client_Name", Type_Name);
        //    htget_Client.Add("@SuProcess_Name", Error_Type);
        //    htget_Client.Add("@Error_OnTask", OrderTask);
        //    htget_Client.Add("@Error_OnStatus", OrderStatus);
        //    htget_Client.Add("@User_Id", ErrorUserId);

        //    dtget_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Client);
        //    if (dtget_Client.Rows.Count > 0)
        //    {
        //        Grd_Errors_Detail.Rows.Clear();
        //        for (int i = 0; i < dtget_Client.Rows.Count; i++)
        //        {
        //            Grd_Errors_Detail.Rows.Add();
        //            Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;
        //            Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget_Client.Rows[i]["Client_Order_Number"].ToString();
        //            if (User_Role == 1)
        //            {
        //                Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_Client.Rows[i]["Client_Name"].ToString();
        //                Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_Client.Rows[i]["Sub_ProcessName"].ToString();
        //            }
        //            else
        //            {
        //                Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_Client.Rows[i]["Client_Number"].ToString();
        //                Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_Client.Rows[i]["Subprocess_Number"].ToString();

        //            }

        //            Grd_Errors_Detail.Rows[i].Cells[5].Value = dtget_Client.Rows[i]["Work_Type"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[6].Value = dtget_Client.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
        //            Grd_Errors_Detail.Rows[i].Cells[7].Value = dtget_Client.Rows[i]["Error_Type"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[8].Value = dtget_Client.Rows[i]["Error_description"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[9].Value = dtget_Client.Rows[i]["Comments"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[10].Value = dtget_Client.Rows[i]["Error_On_Task"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[11].Value = dtget_Client.Rows[i]["Error_On_User_Name"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget_Client.Rows[i]["Error_Entered_From_Task"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget_Client.Rows[i]["Error_Entered_From"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget_Client.Rows[i]["Entered_Date"].ToString();

        //            Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget_Client.Rows[i]["Reporting_1"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget_Client.Rows[i]["Reporting_2"].ToString();

        //            Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget_Client.Rows[i]["State"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget_Client.Rows[i]["County"].ToString();

        //            Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget_Client.Rows[i]["Error_Entered_Task_From_Id"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget_Client.Rows[i]["Error_Entered_From_User_Id"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget_Client.Rows[i]["ErrorInfo_ID"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget_Client.Rows[i]["Order_Id"].ToString();

        //            if (User_Role == 2)
        //            {
        //                Grd_Errors_Detail.Columns[12].Visible = false;
        //                Grd_Errors_Detail.Columns[13].Visible = false;
                      
        //            }
        //        }

        //        foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
        //        {
        //            row.Height = 50;
        //        }

        //    }
        //    else
        //    {

        //        Grd_Errors_Detail.Rows.Clear();
        //    }


        //}


        private void Error_On_Client_Details()
        {
            //load_Progressbar.Start_progres();
            Hashtable htget_Client = new Hashtable();
            DataTable dtget_Client = new DataTable();

            if (ErrorTabPage == "Error_On_Client")
            {
                // 1) Date Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_DATE_WISE_DETAILS");

                }
                // 2)User wsie
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_WISE_DETAILS");
                    
                }
                // 3)Task Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_WISE_DETAILS");

                }
                // 4) Status Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_STATUS_WISE_DETAILS");

                }
                //6) Client  Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_CLIENT_WISE_DETAILS");

                }
                //7) User AND  Task Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_WISE_DETAILS");

                }

                // 8) user NAd Status  Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_STATUS_WISE_DETAILS");

                }

                // 10) user NAd Client  Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_CLIENT_WISE_DETAILS");

                }

                // 11) task NAd Status  Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_AND_STATUS_WISE_DETAILS");

                }


                // 18) user ,Task AND Status  Clinet Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_AND_STATUS_AND_CLIENT_WISE_DETAILS");

                }

                // 19) user ,Task AND Status  Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_AND_STATUS_WISE_DETAILS");

                }

                //20) user and task  and Client  Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_AND_CLIENT_WISE_DETAILS");

                }
                //21) user and  Status  and Client  Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_STATUS_AND_CLIENT_WISE_DETAILS");

                }

                // 22)Task and client Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_AND_CLIENT_WISE_DETAILS");

                }
                // 23)Status and client Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_STATUS_AND_CLIENT_WISE_DETAILS");

                }

                // 24) task and Status and client Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_AND_STATUS_AND_CLIENT_WISE_DETAILS");

                }

                //if (ErrorUserId != 0)
                //{
                //    htget_Client.Add("@Trans", "GET_ERROR_CLIENT_USER_WISE_DETAILS");
                //}
                //else
                //{
                //    htget_Client.Add("@Trans", "GET_ERROR_CLIENT_DETAILS");
                //}

            }
            else if (ErrorTabPage == "Error_On_Subclient")
            {
                //5) Client and Subclient Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_CLIENT_AND_SUBLCIENT_WISE_DETAILS");

                }
                //) 9) user and Client and Subclient Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_CLIENT_AND_SUBLCIENT_WISE_DETAILS");

                }

                //) 12) task  and Client and Subclient Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_AND_CLIENT_AND_SUBLCIENT_WISE_DETAILS");

                }
                //) 14) Status  and Client and Subclient Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_STATUS_AND_CLIENT_AND_SUBLCIENT_WISE_DETAILS");

                }
                //) 16)task and Status  and Client and Subclient Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_AND_STATUS_AND_CLIENT_SUBCLIENT_WISE_DETAILS");

                }
                //17) user and task and Status  and Client and Subclient Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_AND_STATUS_AND_CLIENT_SUBCLIENT_WISE_DETAILS");

                }

                //) 25) user and task and Client and Subclient Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_AND_CLIENT_SUBCLIENT_WISE_DETAILS");

                }
                //) 26) user and STATUS and Client and Subclient Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_STATUS_AND_CLIENT_SUBCLIENT_WISE_DETAILS");

                }

                ////) 27) user and Task and STATUS and Client and Subclient Wise
                //if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                //{
                //    htget_Client.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_AND_STATUS_AND_CLIENT_SUBCLIENT_WISE_DETAILS");

                //}


                //if (ErrorUserId != 0)
                //{
                //    htget_Client.Add("@Trans", "GET_ERROR_CLIENT_SUBPROCESS_USER_WISE_DETAILS");
                   
                //}
                //else
                //{
                //    htget_Client.Add("@Trans", "GET_ERROR_CLIENT_SUBPROCESS_DETAILS");

                //}
            }


            htget_Client.Add("@Error_From_Date", From_Date);
            htget_Client.Add("@Error_To_Date", To_Date);
            htget_Client.Add("@Client_Name", Error_Type);
            htget_Client.Add("@SuProcess_Name", Type_Name);
            htget_Client.Add("@Error_OnTask", OrderTask);
            htget_Client.Add("@Error_OnStatus", OrderStatus);
            htget_Client.Add("@User_Id", ErrorUserId);

            dtget_Client = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_Client);
            if (dtget_Client.Rows.Count > 0)
            {
                Grd_Errors_Detail.Rows.Clear();
                for (int i = 0; i < dtget_Client.Rows.Count; i++)
                {
                    Grd_Errors_Detail.Rows.Add();
                    Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;
                    Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget_Client.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role == 1)
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_Client.Rows[i]["Client_Name"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_Client.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_Client.Rows[i]["Client_Number"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_Client.Rows[i]["Subprocess_Number"].ToString();

                    }

                    Grd_Errors_Detail.Rows[i].Cells[5].Value = dtget_Client.Rows[i]["Work_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[6].Value = dtget_Client.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
                    Grd_Errors_Detail.Rows[i].Cells[7].Value = dtget_Client.Rows[i]["Error_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[8].Value = dtget_Client.Rows[i]["Error_description"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[9].Value = dtget_Client.Rows[i]["Comments"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[10].Value = dtget_Client.Rows[i]["Error_On_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[11].Value = dtget_Client.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget_Client.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget_Client.Rows[i]["Error_Entered_From"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget_Client.Rows[i]["Entered_Date"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget_Client.Rows[i]["Reporting_1"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget_Client.Rows[i]["Reporting_2"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget_Client.Rows[i]["State"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget_Client.Rows[i]["County"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget_Client.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget_Client.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget_Client.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget_Client.Rows[i]["Order_Id"].ToString();

                    //if (User_Role == 2)
                    //{
                    //    Grd_Errors_Detail.Columns[12].Visible = false;
                    //    Grd_Errors_Detail.Columns[13].Visible = false;

                    //}
                }

                foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {

                Grd_Errors_Detail.Rows.Clear();
            }


        }


        private void Error_On_State_Details()
        {
            //load_Progressbar.Start_progres();
            Hashtable htget_County = new Hashtable();
            DataTable dtget_County = new DataTable();

            if (ErrorTabPage == "Error_On_State")
            {
                // 1) Date Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_DATE_WISE_DETAILS");

                }
                // 2)User wsie
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_WISE_DETAILS");

                }
                //7) User AND  Task Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_WISE_DETAILS");

                }
                // 8) user NAd Status  Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_STATUS_WISE_DETAILS");

                }
                // 3)Task Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus == "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_WISE_DETAILS");

                }
                // 4) Status Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_STATUS_WISE_DETAILS");

                }
                // 11) task NAd Status  Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_TASK_AND_STATUS_WISE_DETAILS");

                }
                // 19) user ,Task AND Status  Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type == "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_CLIENTTAB_ERROR_USER_AND_TASK_AND_STATUS_WISE_DETAILS");

                }
                // i) STATE  Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_ERRORSTATE_STATE_WISE_DETAILS");

                }
               
                // ii) user NAd State  Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_USER_AND_STATE_WISE_DETAILS");

                }

                // iii) user ,Task AND Status and  State Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_USER_AND_TASK_AND_STATUS_AND_STATE_WISE_DETAILS");

                }

                //iv)user and task  and State  Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_USER_AND_TASK_AND_STATE_WISE_DETAILS");

                }
                //v) user and  Status  and state  Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_USER_AND_STATUS_AND_STATE_WISE_DETAILS");

                }

                // vi)Task and STATE Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_TASK_AND_STATE_WISE_DETAILS");

                }
                // vii)Status and State Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_STATUS_AND_STATE_WISE_DETAILS");

                }

                // viii) task and Status and STATE Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name == "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_TASK_AND_STATUS_AND_STATE_WISE_DETAILS");

                }

               
            }
            else if (ErrorTabPage == "Error_On_County")
            {
                //ix) State and County Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_STATE_AND_COUNTY_WISE_DETAILS");

                }
                //) x) user and State and County Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_ERRORSTATE_USER_AND_STATE_AND_COUNTY_WISE_DETAILS");

                }

                //) xi) USer and  task  and State and County Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_USER_AND_TASK_AND_STATE_AND_COUNTY_WISE_DETAILS");

                }
                //) xii) user and  Status  and State and County Wise
                if (ErrorUserId != 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_USER_AND_STATUS_AND_STATE_AND_COUNTY_WISE_DETAILS");

                }
                //) xiii)User and task and Status  and State and County Wise
                if (ErrorUserId != 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_USER_AND_TASK_AND_STATUS_AND_STATE_AND_COUNTY_WISE_DETAILS");

                }

                //) xiv)  task  and State and County Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus == "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_TASK_AND_STATE_AND_COUNTY_WISE_DETAILS");

                }

                //) xv)  Status  and State and County Wise
                if (ErrorUserId == 0 && OrderTask == "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_STATUS_AND_STATE_AND_COUNTY_WISE_DETAILS");

                }

                //) xvi) taks and  Status  and State and County Wise
                if (ErrorUserId == 0 && OrderTask != "" && OrderStatus != "" && Error_Type != "" && Type_Name != "")
                {
                    htget_County.Add("@Trans", "GET_STATETAB_ERROR_TASK_AND_STATUS_AND_STATE_AND_COUNTY_WISE_DETAILS");

                }

            }

            htget_County.Add("@Error_From_Date", From_Date);
            htget_County.Add("@Error_To_Date", To_Date);
            htget_County.Add("@State_Name", Error_Type);
            htget_County.Add("@County_Name", Type_Name);
            htget_County.Add("@User_Id", ErrorUserId);
            htget_County.Add("@Error_OnTask", OrderTask);
            htget_County.Add("@Error_OnStatus", OrderStatus);

            dtget_County = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_County);
            if (dtget_County.Rows.Count > 0)
            {
                Grd_Errors_Detail.Rows.Clear();
                for (int i = 0; i < dtget_County.Rows.Count; i++)
                {
                    Grd_Errors_Detail.Rows.Add();
                    Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;
                    Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget_County.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role == 1)
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_County.Rows[i]["Client_Name"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_County.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_County.Rows[i]["Client_Number"].ToString();
                        Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_County.Rows[i]["Subprocess_Number"].ToString();
                    }

                    Grd_Errors_Detail.Rows[i].Cells[5].Value = dtget_County.Rows[i]["Work_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[6].Value = dtget_County.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
                    Grd_Errors_Detail.Rows[i].Cells[7].Value = dtget_County.Rows[i]["Error_Type"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[8].Value = dtget_County.Rows[i]["Error_description"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[9].Value = dtget_County.Rows[i]["Comments"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[10].Value = dtget_County.Rows[i]["Error_On_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[11].Value = dtget_County.Rows[i]["Error_On_User_Name"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget_County.Rows[i]["Error_Entered_From_Task"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget_County.Rows[i]["Error_Entered_From"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget_County.Rows[i]["Entered_Date"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget_County.Rows[i]["Reporting_1"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget_County.Rows[i]["Reporting_2"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget_County.Rows[i]["State"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget_County.Rows[i]["County"].ToString();

                    Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget_County.Rows[i]["Error_Entered_Task_From_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget_County.Rows[i]["Error_Entered_From_User_Id"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget_County.Rows[i]["ErrorInfo_ID"].ToString();
                    Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget_County.Rows[i]["Order_Id"].ToString();

                    //if (User_Role == 2)
                    //{
                    //    Grd_Errors_Detail.Columns[12].Visible = false;
                    //    Grd_Errors_Detail.Columns[13].Visible = false;

                    //}
                }

                foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
                {
                    row.Height = 50;
                }

            }
            else
            {

                Grd_Errors_Detail.Rows.Clear();
            }


        }

        //private void Error_On_State_Details()
        //{
        //    //load_Progressbar.Start_progres();
        //    Hashtable htget_County = new Hashtable();
        //    DataTable dtget_County = new DataTable();

        //    if (ErrorTabPage == "Error_On_State")
        //    {
        //        if (ErrorUserId != 0)
        //        {
        //              htget_County.Add("@Trans", "GET_ERROR_STATE_USER_WISE_DETAILS");
        //              htget_County.Add("@User_Id", ErrorUserId);
        //        }
        //        else
        //        {
        //              htget_County.Add("@Trans", "GET_ERROR_STATE_DETAILS");
        //        }
              
        //        htget_County.Add("@State_Name", Error_Type);
        //    }
        //    else if (ErrorTabPage == "Error_On_County")
        //    {
        //        if (ErrorUserId != 0)
        //        {
        //            htget_County.Add("@Trans", "GET_ERROR_COUNTY_USER_WISE_DETAILS");
        //            htget_County.Add("@User_Id", ErrorUserId);
        //        }
        //        else
        //        {
        //            htget_County.Add("@Trans", "GET_ERROR_COUNTY_DETAILS");
        //        }
               
        //        htget_County.Add("@County_Name", Error_Type);
        //        htget_County.Add("@State_Name", Type_Name);
                
        //    }
           
        //    htget_County.Add("@Error_From_Date", From_Date);
        //    htget_County.Add("@Error_To_Date", To_Date);

        //    dtget_County = dataaccess.ExecuteSP("Sp_Error_Dashboard", htget_County);
        //    if (dtget_County.Rows.Count > 0)
        //    {
        //        Grd_Errors_Detail.Rows.Clear();
        //        for (int i = 0; i < dtget_County.Rows.Count; i++)
        //        {
        //            Grd_Errors_Detail.Rows.Add();
        //            Grd_Errors_Detail.Rows[i].Cells[1].Value = i + 1;
        //            Grd_Errors_Detail.Rows[i].Cells[2].Value = dtget_County.Rows[i]["Client_Order_Number"].ToString();
        //            if (User_Role == 1)
        //            {
        //                Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_County.Rows[i]["Client_Name"].ToString();
        //                Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_County.Rows[i]["Sub_ProcessName"].ToString();
        //            }
        //            else
        //            {
        //                Grd_Errors_Detail.Rows[i].Cells[3].Value = dtget_County.Rows[i]["Client_Number"].ToString();
        //                Grd_Errors_Detail.Rows[i].Cells[4].Value = dtget_County.Rows[i]["Subprocess_Number"].ToString();
        //            }

        //            Grd_Errors_Detail.Rows[i].Cells[5].Value =  dtget_County.Rows[i]["Work_Type"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[6].Value =  dtget_County.Rows[i]["New_Error_Type"].ToString();  // 30-04/2018
        //            Grd_Errors_Detail.Rows[i].Cells[7].Value =  dtget_County.Rows[i]["Error_Type"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[8].Value =  dtget_County.Rows[i]["Error_description"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[9].Value =  dtget_County.Rows[i]["Comments"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[10].Value = dtget_County.Rows[i]["Error_On_Task"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[11].Value = dtget_County.Rows[i]["Error_On_User_Name"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[12].Value = dtget_County.Rows[i]["Error_Entered_From_Task"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[13].Value = dtget_County.Rows[i]["Error_Entered_From"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[14].Value = dtget_County.Rows[i]["Entered_Date"].ToString();
                 
        //            Grd_Errors_Detail.Rows[i].Cells[15].Value = dtget_County.Rows[i]["Reporting_1"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[16].Value = dtget_County.Rows[i]["Reporting_2"].ToString();

        //            Grd_Errors_Detail.Rows[i].Cells[17].Value = dtget_County.Rows[i]["State"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[18].Value = dtget_County.Rows[i]["County"].ToString();

        //            Grd_Errors_Detail.Rows[i].Cells[19].Value = dtget_County.Rows[i]["Error_Entered_Task_From_Id"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[20].Value = dtget_County.Rows[i]["Error_Entered_From_User_Id"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[21].Value = dtget_County.Rows[i]["ErrorInfo_ID"].ToString();
        //            Grd_Errors_Detail.Rows[i].Cells[22].Value = dtget_County.Rows[i]["Order_Id"].ToString();

        //            //if (User_Role == 2)
        //            //{
        //            //    Grd_Errors_Detail.Columns[12].Visible = false;
        //            //    Grd_Errors_Detail.Columns[13].Visible = false;
                       
        //            //}
        //        }

        //        foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
        //        {
        //            row.Height = 50;
        //        }

        //    }
        //    else
        //    {

        //        Grd_Errors_Detail.Rows.Clear();
        //    }


        //}

      

        private void Grd_Errors_Detail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    Order_Entry Order_Entry = new Order_Entry(int.Parse(Grd_Errors_Detail.Rows[e.RowIndex].Cells[22].Value.ToString()), User_ID, User_Role.ToString(), Production_Date);
                    Order_Entry.Show();
                }
                if (e.ColumnIndex == 9)
                {
                    ErrorComments comments = new ErrorComments(Grd_Errors_Detail.Rows[e.RowIndex].Cells[9].Value.ToString());
                    comments.Show();
                }
            }
        }


        private void Grid_Export_Data()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in Grd_Errors_Detail.Columns)
            {
                if (column.Index != 0 && column.Index != 19 && column.Index != 20 && column.Index != 21 && column.Index != 22)
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
            }
            //Adding rows in Excel
            foreach (DataGridViewRow row in Grd_Errors_Detail.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex != 0 && cell.ColumnIndex != 19 && cell.ColumnIndex != 20 && cell.ColumnIndex != 21 && cell.ColumnIndex != 22)
                    {
                  
                  
                            if (cell.Value != null && cell.Value.ToString() != "")
                            {
                                dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex-1] = cell.Value.ToString();
                            }
                    }
                 }
            }


            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "ERROR_DETAILS" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "ERROR-DETAILS");
                try
                {
                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it", title);
                }
            }
            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_Error_Tab_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Grid_Export_Data();
        }
       









    }
}
