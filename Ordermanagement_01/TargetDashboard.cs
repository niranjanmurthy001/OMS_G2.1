using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01
{
    public partial class TargetDashboard : Form
    {
          Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        System.Data.DataTable dtexport = new System.Data.DataTable();
        System.Data.DataTable dttargetorder = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();

       // InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string Order_Target, Time_Zone, OrderViewType, Tat_id, score_board, header_Pending;
        static int currentpageindex = 0;
        int pagesize = 15;
        int Valuegrd, scoreuserid;
        string date_ScoreBoard;
        int User_id, Role_Id;
        string scoreboard_name;
        string Employee_Completd,Employee_User_Id;
        string Employee_Hour;
        string First_Date, Second_Date;
        public TargetDashboard(string OrderTarget, string TimeZone, string Order_ViewType, int Value, int score_user_id, string Tat, string score_user, string pending_header, string date, int USER_ID, string USER_ROLE_ID, string Scoreboard_name)
        {
            InitializeComponent();
            Order_Target = OrderTarget;
            Time_Zone = TimeZone;
            First_Date = Order_ViewType;
            Second_Date = date;
            OrderViewType = Order_ViewType;

            Employee_Completd = score_user_id.ToString();



       //     Employee_User_Id = TimeZone.ToString();

            Employee_Hour = Tat;
            Valuegrd = Value;
            User_id = int.Parse(USER_ID.ToString());
            Role_Id = int.Parse(USER_ROLE_ID.ToString());
            scoreuserid = score_user_id;
            score_board = score_user;
            header_Pending = pending_header;
            date_ScoreBoard = date;
            // tot_orderinfo = total_orders;
            Tat_id = Tat;
            this.Text = Tat_id;
            scoreboard_name = Scoreboard_name;
            /// TargetDashboard.ActiveForm.Text = "utryuih";
            //  Get_Target_Orders_Client_WiseTo_GridviewBind();
        }
        private void GetDataRow_Target_Orders(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dttargetorder.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        protected void Get_Target_orders_to_next()
        {
            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            if (temptable.Rows.Count > 0)
            {

                //   grd_Targetorder.DataBind();

                grd_Targetorder.DataSource = null;
                grd_Targetorder.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                grd_Targetorder.EnableHeadersVisualStyles = false;
                grd_Targetorder.AutoGenerateColumns = false;
                grd_Targetorder.ColumnCount = 13;


                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;


                grd_Targetorder.Columns[1].Name = "Order Number";
                grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                grd_Targetorder.Columns[1].DataPropertyName = "Client_Order_Number";
                grd_Targetorder.Columns[1].Width = 200;
                grd_Targetorder.Columns[1].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "ORDER NUMBER";
                link.DataPropertyName = "Client_Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;

                if (Role_Id == 1)
                {
                    grd_Targetorder.Columns[2].Name = "Customer";
                    grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[2].Width = 140;

                    grd_Targetorder.Columns[3].Name = "SubProcess";
                    grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[3].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[3].Width = 220;

                }
                else
                {
                    grd_Targetorder.Columns[2].Name = "Client_Number";
                    grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "Client_Number";
                    grd_Targetorder.Columns[2].Width = 140;

                    grd_Targetorder.Columns[3].Name = "Subprocess_Number";
                    grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[3].DataPropertyName = "Subprocess_Number";
                    grd_Targetorder.Columns[3].Width = 220;
                

                }

                grd_Targetorder.Columns[4].Name = "Submited";
                grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                grd_Targetorder.Columns[4].DataPropertyName = "Date";
                grd_Targetorder.Columns[4].Width = 120;

                grd_Targetorder.Columns[5].Name = "OrderType";
                grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[5].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[5].Width = 160;

                grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                grd_Targetorder.Columns[6].DataPropertyName = "Client_Order_Ref";
                grd_Targetorder.Columns[6].Width = 170;

                grd_Targetorder.Columns[7].Name = "SearchType";
                grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                grd_Targetorder.Columns[7].DataPropertyName = "County_Type";
                grd_Targetorder.Columns[7].Width = 160;

                grd_Targetorder.Columns[8].Name = "County";
                grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                grd_Targetorder.Columns[8].DataPropertyName = "County";
                grd_Targetorder.Columns[8].Width = 140;

                grd_Targetorder.Columns[9].Name = "State";
                grd_Targetorder.Columns[9].HeaderText = "STATE";
                grd_Targetorder.Columns[9].DataPropertyName = "State";
                grd_Targetorder.Columns[9].Width = 120;

                grd_Targetorder.Columns[10].Name = "Task";
                grd_Targetorder.Columns[10].HeaderText = "TASK";
                grd_Targetorder.Columns[10].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "User";
                grd_Targetorder.Columns[11].HeaderText = "USER NAME";
                grd_Targetorder.Columns[11].DataPropertyName = "User_Name";
                grd_Targetorder.Columns[11].Width = 100;

                grd_Targetorder.Columns[12].Name = "Order Id";
                grd_Targetorder.Columns[12].HeaderText = "Order id";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[12].Width = 100;
                grd_Targetorder.Columns[12].Visible = false;

                //  grd_Targetorder.Columns[12].Visible = false;
                grd_Targetorder.DataSource = temptable;

            }
            else
            {
                grd_Targetorder.Visible = true;
               // grd_Targetorder.Rows.Clear();
                grd_Targetorder.DataSource = null;
                //grd_Targetorder.DataBind();
            }
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);

            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }
            lbl_total.Text = dttargetorder.Rows.Count.ToString();
        }
         protected void Get_Target_Orders_To_GridviewBind()
        {
            
            if (header_Pending == "PENDING ORDERS")
            {
                lbl_Headername.Text = "PENDING ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
                
                //TargetDashboard.ActiveForm.Name = "PENDING ORDERS";
                
            }
            else if (header_Pending == "RECEIVED ORDERS")
            {
                lbl_Headername.Text = "RECEIVED ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
               
            }
            else if (header_Pending == "COMPLETED ORDERS")
            {
                lbl_Headername.Text = "COMPLETED ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
                
            }
            else if (header_Pending == "WORK PROGRESSING ORDERS")
            {
                lbl_Headername.Text = "WORK PROGRESSING ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
               
            }
            else if (header_Pending == "OPENED ORDERS")
            {
                lbl_Headername.Text = "OPENED ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
               
            }
            else if (header_Pending == "NOT ASSIGNED ORDERS")
            {
                lbl_Headername.Text = "NOT ASSIGNED ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
               
            }
            else if (header_Pending == "HOLD/CLARIFICATION ORDERS")
            {
                lbl_Headername.Text = "HOLD/CLARIFICATION ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
                
            }
            else if (header_Pending == "CANCELLED ORDERS")
            {
                lbl_Headername.Text = "CANCELLED ORDERS";
                lbl_Username.Text = "";
                lbl_Name.Text = "";
                
            }
           
          //  Order_Target = ViewState["Order_Target"].ToString();
          //  Time_Zone = ViewState["Time_Zone"].ToString();
            Hashtable httargetorder = new Hashtable();
           // DataTable dttargetorder = new System.Data.DataTable();
            httargetorder.Add("@Trans", Order_Target);
            httargetorder.Add("@OrdersZone", Time_Zone);
            dttargetorder = dataaccess.ExecuteSP("Sp_Order_Target_Info", httargetorder);
            dtexport = dttargetorder;

            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            
            if (temptable.Rows.Count > 0)
            {
              
             //   grd_Targetorder.DataBind();

                grd_Targetorder.DataSource = null;
                grd_Targetorder.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                grd_Targetorder.EnableHeadersVisualStyles = false;        
                grd_Targetorder.AutoGenerateColumns = false;
                grd_Targetorder.ColumnCount = 13;


                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;


                grd_Targetorder.Columns[1].Name = "Order Number";
                grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                grd_Targetorder.Columns[1].DataPropertyName = "Client_Order_Number";
                grd_Targetorder.Columns[1].Width = 200;
                grd_Targetorder.Columns[1].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "ORDER NUMBER";
                link.DataPropertyName = "Client_Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;

                if (Role_Id == 1)
                {
                    grd_Targetorder.Columns[2].Name = "Customer";
                    grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[2].Width = 140;

                    grd_Targetorder.Columns[3].Name = "SubProcess";
                    grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[3].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[3].Width = 220;
                }
                else
                {
                    grd_Targetorder.Columns[2].Name = "Client_Number";
                    grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "Client_Number";
                    grd_Targetorder.Columns[2].Width = 140;

                    grd_Targetorder.Columns[3].Name = "Subprocess_Number";
                    grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[3].DataPropertyName = "Subprocess_Number";
                    grd_Targetorder.Columns[3].Width = 220;
                

                }
                grd_Targetorder.Columns[4].Name = "Submited";
                grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                grd_Targetorder.Columns[4].DataPropertyName = "Date";
                grd_Targetorder.Columns[4].Width = 120;

                grd_Targetorder.Columns[5].Name = "OrderType";
                grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[5].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[5].Width = 160;

                grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                grd_Targetorder.Columns[6].DataPropertyName = "Client_Order_Ref";
                grd_Targetorder.Columns[6].Width = 170;

                grd_Targetorder.Columns[7].Name = "SearchType";
                grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                grd_Targetorder.Columns[7].DataPropertyName = "County_Type";
                grd_Targetorder.Columns[7].Width = 160;

                grd_Targetorder.Columns[8].Name = "County";
                grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                grd_Targetorder.Columns[8].DataPropertyName = "County";
                grd_Targetorder.Columns[8].Width = 140;

                grd_Targetorder.Columns[9].Name = "State";
                grd_Targetorder.Columns[9].HeaderText = "STATE";
                grd_Targetorder.Columns[9].DataPropertyName = "State";
                grd_Targetorder.Columns[9].Width = 120;

                grd_Targetorder.Columns[10].Name = "Task";
                grd_Targetorder.Columns[10].HeaderText = "TASK";
                grd_Targetorder.Columns[10].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "User";
                grd_Targetorder.Columns[11].HeaderText = "USER NAME";
                grd_Targetorder.Columns[11].DataPropertyName = "User_Name";
                grd_Targetorder.Columns[11].Width = 100;

                grd_Targetorder.Columns[12].Name = "Order Id";
                grd_Targetorder.Columns[12].HeaderText = "Order id";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[12].Width = 100;
                grd_Targetorder.Columns[12].Visible = false;

              //  grd_Targetorder.Columns[12].Visible = false;
                grd_Targetorder.DataSource = temptable;
           
            }
            else
            {
                grd_Targetorder.Visible = true;
               // grd_Targetorder.Rows.Clear();
                grd_Targetorder.DataSource = null;
                //grd_Targetorder.DataBind();
            }
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);

            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }
            lbl_total.Text = dttargetorder.Rows.Count.ToString();
        }
         protected void Get_Target_Orders_Client_WiseTo_Next()
         {
             System.Data.DataTable temptable = dttargetorder.Clone();
             int startindex = currentpageindex * pagesize;
             int endindex = currentpageindex * pagesize + pagesize;
             if (endindex > dttargetorder.Rows.Count)
             {
                 endindex = dttargetorder.Rows.Count;
             }
             for (int i = startindex; i < endindex; i++)
             {
                 DataRow row = temptable.NewRow();
                 GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                 temptable.Rows.Add(row);
             }
             if (temptable.Rows.Count > 0)
             {
                 grd_Targetorder.DataSource = null;

                 grd_Targetorder.AutoGenerateColumns = false;
                 grd_Targetorder.ColumnCount = 13;

                 grd_Targetorder.Columns[0].Name = "SNo";
                 grd_Targetorder.Columns[0].HeaderText = "S. No";
                 grd_Targetorder.Columns[0].Width = 65;

                 grd_Targetorder.Columns[1].Name = "Order Number";
                 grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                 grd_Targetorder.Columns[1].DataPropertyName = "Client_Order_Number";
                 grd_Targetorder.Columns[1].Width = 200;
                 grd_Targetorder.Columns[1].Visible = false;

                 DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                 grd_Targetorder.Columns.Add(link);
                 link.Name = "Order Number";
                 link.HeaderText = "ORDER NUMBER";
                 link.DataPropertyName = "Client_Order_Number";
                 link.Width = 200;
                 link.DisplayIndex = 1;

                 if (Role_Id == 1)
                 {
                     grd_Targetorder.Columns[2].Name = "Customer";
                     grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                     grd_Targetorder.Columns[2].DataPropertyName = "Client_Name";
                     grd_Targetorder.Columns[2].Width = 140;

                     grd_Targetorder.Columns[3].Name = "SubProcess";
                     grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                     grd_Targetorder.Columns[3].DataPropertyName = "Sub_ProcessName";
                     grd_Targetorder.Columns[3].Width = 220;
                 }
                 else
                 {
                     grd_Targetorder.Columns[2].Name = "Client_Number";
                     grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                     grd_Targetorder.Columns[2].DataPropertyName = "Client_Number";
                     grd_Targetorder.Columns[2].Width = 140;

                     grd_Targetorder.Columns[3].Name = "Subprocess_Number";
                     grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                     grd_Targetorder.Columns[3].DataPropertyName = "Subprocess_Number";
                     grd_Targetorder.Columns[3].Width = 220;

                 }





                 grd_Targetorder.Columns[4].Name = "Submited";
                 grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                 grd_Targetorder.Columns[4].DataPropertyName = "Date";
                 grd_Targetorder.Columns[4].Width = 120;


                 grd_Targetorder.Columns[5].Name = "OrderType";
                 grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                 grd_Targetorder.Columns[5].DataPropertyName = "Order_Type";
                 grd_Targetorder.Columns[5].Width = 160;


                 grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                 grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                 grd_Targetorder.Columns[6].DataPropertyName = "Client_Order_Ref";
                 grd_Targetorder.Columns[6].Width = 170;


                 grd_Targetorder.Columns[7].Name = "SearchType";
                 grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                 grd_Targetorder.Columns[7].DataPropertyName = "County_Type";
                 grd_Targetorder.Columns[7].Width = 160;


                 grd_Targetorder.Columns[8].Name = "County";
                 grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                 grd_Targetorder.Columns[8].DataPropertyName = "County";
                 grd_Targetorder.Columns[8].Width = 140;


                 grd_Targetorder.Columns[9].Name = "State";
                 grd_Targetorder.Columns[9].HeaderText = "STATE";
                 grd_Targetorder.Columns[9].DataPropertyName = "State";
                 grd_Targetorder.Columns[9].Width = 120;


                 grd_Targetorder.Columns[10].Name = "Task";
                 grd_Targetorder.Columns[10].HeaderText = "TASK";
                 grd_Targetorder.Columns[10].DataPropertyName = "Order_Status";
                 grd_Targetorder.Columns[10].Width = 120;


                 grd_Targetorder.Columns[11].Name = "User";
                 grd_Targetorder.Columns[11].HeaderText = "USER";
                 grd_Targetorder.Columns[11].DataPropertyName = "User_Name";
                 grd_Targetorder.Columns[11].Width = 100;

                 grd_Targetorder.Columns[12].Name = "Order Id";
                 grd_Targetorder.Columns[12].HeaderText = "Order id";
                 grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                 grd_Targetorder.Columns[12].Width = 100;
                 grd_Targetorder.Columns[12].Visible = false;
                 //  grd_Targetorder.Columns[12].Visible = false;
                 grd_Targetorder.DataSource = temptable;




             }
             else
             {

                 grd_Targetorder.DataSource = null;
                 //grd_Targetorder.DataBind();
             }
             lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
             for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
             {
                 grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
             }
         }

        protected void Get_Target_Orders_Client_WiseTo_GridviewBind()
        {
            if (Tat_id == "CLIENT TAT")
            {
                lbl_Headername.Text = "CLIENT TAT";
            }
            else
            {
                lbl_Headername.Text = "EMPLOYEE TAT";
            }
            Hashtable httargetorder = new Hashtable();
           // DataTable dttargetorder = new System.Data.DataTable();
            httargetorder.Add("@Trans", Order_Target);
            lbl_Name.Text = "";
            lbl_Username.Text = Tat_id;
            lbl_Username.ForeColor = Color.Brown;
            
            if (OrderViewType == "TAT-C")
            {
                dttargetorder = dataaccess.ExecuteSP("Sp_Order_Client_Target_Info", httargetorder);
                lbl_total.Text = dttargetorder.Rows.Count.ToString();
            }
            else
            {

                dttargetorder = dataaccess.ExecuteSP("Sp_Order_Employee_Target_Info", httargetorder);
                lbl_total.Text = dttargetorder.Rows.Count.ToString();
            }
            dtexport = dttargetorder;

            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            if (temptable.Rows.Count > 0)
            {
              grd_Targetorder.DataSource = null;
                        
                grd_Targetorder.AutoGenerateColumns = false;
                grd_Targetorder.ColumnCount = 13;

                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;
                
                grd_Targetorder.Columns[1].Name = "Order Number";
                grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                grd_Targetorder.Columns[1].DataPropertyName = "Client_Order_Number";
                grd_Targetorder.Columns[1].Width = 200;
                grd_Targetorder.Columns[1].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "ORDER NUMBER";
                link.DataPropertyName = "Client_Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;

                if (Role_Id == 1)
                {
                    grd_Targetorder.Columns[2].Name = "Customer";
                    grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[2].Width = 140;

                    grd_Targetorder.Columns[3].Name = "SubProcess";
                    grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                    grd_Targetorder.Columns[3].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[3].Width = 220;

                }
                else
                {


                    grd_Targetorder.Columns[2].Name = "Client_Number";
                    grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "Client_Number";
                    grd_Targetorder.Columns[2].Width = 140;

                    grd_Targetorder.Columns[3].Name = "Subprocess_Number";
                    grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                    grd_Targetorder.Columns[3].DataPropertyName = "Subprocess_Number";
                    grd_Targetorder.Columns[3].Width = 220;
                }

                grd_Targetorder.Columns[4].Name = "Submited";
                grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                grd_Targetorder.Columns[4].DataPropertyName = "Date";
                grd_Targetorder.Columns[4].Width = 120;


                grd_Targetorder.Columns[5].Name = "OrderType";
                grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[5].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[5].Width = 160;


                grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                grd_Targetorder.Columns[6].DataPropertyName = "Client_Order_Ref";
                grd_Targetorder.Columns[6].Width = 170;


                grd_Targetorder.Columns[7].Name = "SearchType";
                grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                grd_Targetorder.Columns[7].DataPropertyName = "County_Type";
                grd_Targetorder.Columns[7].Width = 160;


                grd_Targetorder.Columns[8].Name = "County";
                grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                grd_Targetorder.Columns[8].DataPropertyName = "County";
                grd_Targetorder.Columns[8].Width = 140;


                grd_Targetorder.Columns[9].Name = "State";
                grd_Targetorder.Columns[9].HeaderText = "STATE";
                grd_Targetorder.Columns[9].DataPropertyName = "State";
                grd_Targetorder.Columns[9].Width = 120;


                grd_Targetorder.Columns[10].Name = "Task";
                grd_Targetorder.Columns[10].HeaderText = "TASK";
                grd_Targetorder.Columns[10].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[10].Width = 120;


                grd_Targetorder.Columns[11].Name = "User";
                grd_Targetorder.Columns[11].HeaderText = "USER";
                grd_Targetorder.Columns[11].DataPropertyName = "User_Name";
                grd_Targetorder.Columns[11].Width = 100;

                grd_Targetorder.Columns[12].Name = "Order Id";
                grd_Targetorder.Columns[12].HeaderText = "Order id";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[12].Width = 100;
                grd_Targetorder.Columns[12].Visible = false;
              //  grd_Targetorder.Columns[12].Visible = false;
                grd_Targetorder.DataSource = temptable;
              

               

            }
            else
            {

                grd_Targetorder.DataSource = null;
                //grd_Targetorder.DataBind();
            }
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }

            }



        private void TargetDashboard_Load(object sender, EventArgs e)
        {
            //TargetDashboard.ActiveForm.Text = "utryuih";
            grd_Targetorder.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
            grd_Targetorder.EnableHeadersVisualStyles = false;
            Grid_Count.Visible = false;
            Grid_Order_Type_Count.Visible = false;
            grd_Targetorder.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            if (Valuegrd == 0)
            {
                Get_Target_Orders_To_GridviewBind();
            }
            else if (Valuegrd == 1)
            {
                Get_Target_Orders_Client_WiseTo_GridviewBind();
            }
            else if (Valuegrd == 2 || Valuegrd == 3)
            {
                Get_Score_Board_GridviewBind();
            }
            else if (Valuegrd == 4)
            {
                Grid_Count.Visible = true;
                Grid_Order_Type_Count.Visible = true;
                Gridview_Employee_Production_First_Date_Dataview();

            }
            else if (Valuegrd == 5)
            {
                Grid_Count.Visible = true;
                Grid_Order_Type_Count.Visible = true;
                Gridview_Employee_Production_24_7_Date_Dataview();

            }
            btnFirst_Click(sender, e);

        }


        private void Gridview_Employee_Production_24_7_Date_Dataview()
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            Hashtable htinsert = new Hashtable();
            DataTable dtinsert = new DataTable();
            htinsert.Add("@Trans", "INSERT_INTO_TEMP");
            htinsert.Add("@Firstdate", First_Date);
            htinsert.Add("@Second_Date", Second_Date);
            htinsert.Add("@User_Id", Employee_User_Id);
            dtinsert = dataaccess.ExecuteSP("Sp_User_24_7_Production_Report", htinsert);


            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();


            Hashtable httype = new Hashtable();
            DataTable dttype = new DataTable();



                htcount.Add("@Trans", "SELECT_BY_TOTAL_Completed_Order_Status_Count");
              


                httype.Add("@Trans", "SELECT_BY_TOTAL_Completed_OrderType_Wise_Status_Count");
              


           

            dtcount = dataaccess.ExecuteSP("Sp_User_24_7_Production_Report", htcount);

            dttype = dataaccess.ExecuteSP("Sp_User_24_7_Production_Report", httype);

            if (dtcount.Rows.Count > 0)
            {

                Grid_Count.DataSource = dtcount;

            }
            else
            {


                Grid_Count.Rows.Clear();
            }

            if (dttype.Rows.Count > 0)
            {

                Grid_Order_Type_Count.DataSource = dttype;

            }
            else
            {


                Grid_Order_Type_Count.Rows.Clear();
            }





            ht.Add("@Trans", "TOTAL_ORDER_COMPLETED_BY_USER_WISE");

         
           
            dttargetorder = dataaccess.ExecuteSP("Sp_User_24_7_Production_Report", ht);

            if (dttargetorder.Rows.Count > 0)
            {

                System.Data.DataTable temptable = dttargetorder.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dttargetorder.Rows.Count)
                {
                    endindex = dttargetorder.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                    temptable.Rows.Add(row);
                }
                lbl_total.Text = dttargetorder.Rows.Count.ToString();
                if (temptable.Rows.Count > 0)
                {
                    lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
                }
                if (temptable.Rows.Count > 0)
                {

                    //   grd_Targetorder.DataBind();





                    grd_Targetorder.DataSource = null;
                    grd_Targetorder.AutoGenerateColumns = false;




                    grd_Targetorder.ColumnCount = 13;

                    grd_Targetorder.Columns[0].Name = "SNo";
                    grd_Targetorder.Columns[0].HeaderText = "S. No";
                    grd_Targetorder.Columns[0].Width = 65;

                    grd_Targetorder.Columns[1].Name = "Production Date";
                    grd_Targetorder.Columns[1].HeaderText = "PRODUCTION DATE";
                    grd_Targetorder.Columns[1].DataPropertyName = "Order_Production_Date";
                    grd_Targetorder.Columns[1].Width = 150;

                    grd_Targetorder.Columns[2].Name = "User Name";
                    grd_Targetorder.Columns[2].HeaderText = "USER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "User_Name";
                    grd_Targetorder.Columns[2].Width = 110;


                    grd_Targetorder.Columns[3].Name = "Order Number";
                    grd_Targetorder.Columns[3].HeaderText = "ORDER NUMBER";
                    grd_Targetorder.Columns[3].DataPropertyName = "Client_Order_Number";
                    grd_Targetorder.Columns[3].Width = 175;
                    grd_Targetorder.Columns[3].Visible = false;

                    DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                    grd_Targetorder.Columns.Add(link);
                    link.Name = "Order Number";
                    link.HeaderText = "ORDER NUMBER";
                    link.DataPropertyName = "Client_Order_Number";
                    link.Width = 200;
                    link.DisplayIndex = 1;

                    if (Role_Id == 1)
                    {
                        grd_Targetorder.Columns[4].Name = "Client Name";
                        grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                        grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                        grd_Targetorder.Columns[4].Width = 130;

                        grd_Targetorder.Columns[5].Name = "SubProcessName";
                        grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                        grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                        grd_Targetorder.Columns[5].Width = 220;
                    }
                    else
                    {
                        grd_Targetorder.Columns[4].Name = "Client_Number";
                        grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                        grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                        grd_Targetorder.Columns[4].Width = 130;

                        grd_Targetorder.Columns[5].Name = "Subprocess_Number";
                        grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                        grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                        grd_Targetorder.Columns[5].Width = 220;


                     
                    }
                    grd_Targetorder.Columns[6].Name = "OrderType";
                    grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                    grd_Targetorder.Columns[6].Width = 160;

                    grd_Targetorder.Columns[7].Name = "Task";
                    grd_Targetorder.Columns[7].HeaderText = "TASK";
                    grd_Targetorder.Columns[7].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[7].Width = 120;

                    grd_Targetorder.Columns[8].Name = "Status";
                    grd_Targetorder.Columns[8].HeaderText = "PROGRESS STATUS";
                    grd_Targetorder.Columns[8].DataPropertyName = "Progress_Status";
                    grd_Targetorder.Columns[8].Width = 160;


                    grd_Targetorder.Columns[9].Name = "StartTime";
                    grd_Targetorder.Columns[9].HeaderText = "START TIME";
                    grd_Targetorder.Columns[9].DataPropertyName = "Start_Time";
                    grd_Targetorder.Columns[9].Width = 120;

                    grd_Targetorder.Columns[10].Name = "EndTime";
                    grd_Targetorder.Columns[10].HeaderText = "END TIME";
                    grd_Targetorder.Columns[10].DataPropertyName = "End_Time";
                    grd_Targetorder.Columns[10].Width = 120;

                    grd_Targetorder.Columns[11].Name = "TotalTime";
                    grd_Targetorder.Columns[11].HeaderText = "TOTAL TIME";
                    grd_Targetorder.Columns[11].DataPropertyName = "Total_Time";
                    grd_Targetorder.Columns[11].Width = 100;


                    grd_Targetorder.Columns[12].Name = "Order Id";
                    grd_Targetorder.Columns[12].HeaderText = "Order id";
                    grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[12].Width = 100;
                    grd_Targetorder.Columns[12].Visible = false;

                    grd_Targetorder.DataSource = temptable;



                    if (User_id == scoreuserid && Role_Id == 2)
                    {

                        grd_Targetorder.Columns[4].Visible = false;
                        grd_Targetorder.Columns[5].Visible = false;
                        grd_Targetorder.Columns[9].Visible = false;
                        grd_Targetorder.Columns[10].Visible = false;
                        grd_Targetorder.Columns[11].Visible = false;
                    }
                    else if (Role_Id == 1 || Role_Id == 6 || Role_Id == 4)
                    {
                        grd_Targetorder.Columns[4].Visible = true;
                        grd_Targetorder.Columns[5].Visible = true;
                        grd_Targetorder.Columns[9].Visible = true;
                        grd_Targetorder.Columns[10].Visible = true;
                        grd_Targetorder.Columns[11].Visible = true;

                    }
                    else
                    {
                        grd_Targetorder.Visible = false;
                        //grd_Targetorder.Rows.Clear();
                        grd_Targetorder.DataSource = null;
                    }



                }
                else
                {
                    grd_Targetorder.Visible = true;
                    //grd_Targetorder.Rows.Clear();
                    grd_Targetorder.DataSource = null;
                }
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
                for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
                {
                    grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
                }
            }







        }
        private void Gridview_Employee_Production_First_Date_Dataview()
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();

            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();


            Hashtable httype = new Hashtable();
            DataTable dttype = new DataTable();


            if (Employee_Completd != "0")
            {
                htcount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_Completed_Order_Status_Count");
                htcount.Add("@Firstdate", First_Date);
                htcount.Add("@Hour", Employee_Hour);
                htcount.Add("@User_Id", Employee_User_Id);

                
                httype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_Completed_OrderType_Wise_Status_Count");
                httype.Add("@Firstdate", First_Date);
                httype.Add("@Hour", Employee_Hour);
                httype.Add("@User_Id", Employee_User_Id);


            }
            else if (Employee_Completd == "0")
            {
                htcount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_NOT_Completed_Order_Status_Count");
                htcount.Add("@Firstdate", First_Date);
                htcount.Add("@Hour", Employee_Hour);
                htcount.Add("@User_Id", Employee_User_Id);


                httype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_NOT_Completed_OrderType_Wise_Status_Count");
                httype.Add("@Firstdate", First_Date);
                httype.Add("@Hour", Employee_Hour);
                httype.Add("@User_Id", Employee_User_Id);

            }


            dtcount = dataaccess.ExecuteSP("Sp_User_24_7_Production_Report", htcount);

            dttype = dataaccess.ExecuteSP("Sp_User_24_7_Production_Report", httype);

            if (dtcount.Rows.Count > 0)
            {

                Grid_Count.DataSource = dtcount;

            }
            else
            {


                Grid_Count.Rows.Clear();
            }

            if (dttype.Rows.Count > 0)
            {

                Grid_Order_Type_Count.DataSource = dttype;

            }
            else
            {


                Grid_Order_Type_Count.Rows.Clear();
            }


            if (Employee_Completd != "0")
            {


                ht.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_COMPLETED");

            }
            else
            {

                ht.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_NOT_COMPLETED");

            }

            ht.Add("@Hour", Employee_Hour);
            ht.Add("@Firstdate", First_Date);
            ht.Add("@User_Id", Employee_User_Id);
            dttargetorder = dataaccess.ExecuteSP("Sp_User_24_7_Production_Report", ht);

            if (dttargetorder.Rows.Count > 0)
            {

                System.Data.DataTable temptable = dttargetorder.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dttargetorder.Rows.Count)
                {
                    endindex = dttargetorder.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                    temptable.Rows.Add(row);
                }
                lbl_total.Text = dttargetorder.Rows.Count.ToString();
                if (temptable.Rows.Count > 0)
                {
                    lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
                }
                if (temptable.Rows.Count > 0)
                {

                    //   grd_Targetorder.DataBind();





                    grd_Targetorder.DataSource = null;
                    grd_Targetorder.AutoGenerateColumns = false;




                    grd_Targetorder.ColumnCount = 13;

                    grd_Targetorder.Columns[0].Name = "SNo";
                    grd_Targetorder.Columns[0].HeaderText = "S. No";
                    grd_Targetorder.Columns[0].Width = 65;

                    grd_Targetorder.Columns[1].Name = "Production Date";
                    grd_Targetorder.Columns[1].HeaderText = "PRODUCTION DATE";
                    grd_Targetorder.Columns[1].DataPropertyName = "Order_Production_Date";
                    grd_Targetorder.Columns[1].Width = 150;

                    grd_Targetorder.Columns[2].Name = "User Name";
                    grd_Targetorder.Columns[2].HeaderText = "USER NAME";
                    grd_Targetorder.Columns[2].DataPropertyName = "User_Name";
                    grd_Targetorder.Columns[2].Width = 110;


                    grd_Targetorder.Columns[3].Name = "Order Number";
                    grd_Targetorder.Columns[3].HeaderText = "ORDER NUMBER";
                    grd_Targetorder.Columns[3].DataPropertyName = "Client_Order_Number";
                    grd_Targetorder.Columns[3].Width = 175;
                    grd_Targetorder.Columns[3].Visible = false;

                    DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                    grd_Targetorder.Columns.Add(link);
                    link.Name = "Order Number";
                    link.HeaderText = "ORDER NUMBER";
                    link.DataPropertyName = "Client_Order_Number";
                    link.Width = 200;
                    link.DisplayIndex = 1;

                    if (Role_Id == 1)
                    {

                        grd_Targetorder.Columns[4].Name = "Client Name";
                        grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                        grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                        grd_Targetorder.Columns[4].Width = 130;

                        grd_Targetorder.Columns[5].Name = "SubProcessName";
                        grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                        grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                        grd_Targetorder.Columns[5].Width = 220;
                    }
                    else
                    {
                        grd_Targetorder.Columns[4].Name = "Client_Number";
                        grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                        grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                        grd_Targetorder.Columns[4].Width = 130;

                        grd_Targetorder.Columns[5].Name = "Subprocess_Number";
                        grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                        grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                        grd_Targetorder.Columns[5].Width = 220;

                    }

                




                    grd_Targetorder.Columns[6].Name = "OrderType";
                    grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                    grd_Targetorder.Columns[6].Width = 160;

                    grd_Targetorder.Columns[7].Name = "Task";
                    grd_Targetorder.Columns[7].HeaderText = "TASK";
                    grd_Targetorder.Columns[7].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[7].Width = 120;

                    grd_Targetorder.Columns[8].Name = "Status";
                    grd_Targetorder.Columns[8].HeaderText = "PROGRESS STATUS";
                    grd_Targetorder.Columns[8].DataPropertyName = "Progress_Status";
                    grd_Targetorder.Columns[8].Width = 160;


                    grd_Targetorder.Columns[9].Name = "StartTime";
                    grd_Targetorder.Columns[9].HeaderText = "START TIME";
                    grd_Targetorder.Columns[9].DataPropertyName = "Start_Time";
                    grd_Targetorder.Columns[9].Width = 120;

                    grd_Targetorder.Columns[10].Name = "EndTime";
                    grd_Targetorder.Columns[10].HeaderText = "END TIME";
                    grd_Targetorder.Columns[10].DataPropertyName = "End_Time";
                    grd_Targetorder.Columns[10].Width = 120;

                    grd_Targetorder.Columns[11].Name = "TotalTime";
                    grd_Targetorder.Columns[11].HeaderText = "TOTAL TIME";
                    grd_Targetorder.Columns[11].DataPropertyName = "Total_Time";
                    grd_Targetorder.Columns[11].Width = 100;


                    grd_Targetorder.Columns[12].Name = "Order Id";
                    grd_Targetorder.Columns[12].HeaderText = "Order id";
                    grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[12].Width = 100;
                    grd_Targetorder.Columns[12].Visible = false;

                    grd_Targetorder.DataSource = temptable;



                    if (User_id == scoreuserid && Role_Id == 2)
                    {

                        grd_Targetorder.Columns[4].Visible = false;
                        grd_Targetorder.Columns[5].Visible = false;
                        grd_Targetorder.Columns[9].Visible = false;
                        grd_Targetorder.Columns[10].Visible = false;
                        grd_Targetorder.Columns[11].Visible = false;
                    }
                    else if (Role_Id == 1 || Role_Id == 6 || Role_Id == 4)
                    {
                        grd_Targetorder.Columns[4].Visible = true;
                        grd_Targetorder.Columns[5].Visible = true;
                        grd_Targetorder.Columns[9].Visible = true;
                        grd_Targetorder.Columns[10].Visible = true;
                        grd_Targetorder.Columns[11].Visible = true;

                    }
                    else
                    {
                        grd_Targetorder.Visible = false;
                        //grd_Targetorder.Rows.Clear();
                        grd_Targetorder.DataSource = null;
                    }



                }
                else
                {
                    grd_Targetorder.Visible = true;
                    //grd_Targetorder.Rows.Clear();
                    grd_Targetorder.DataSource = null;
                }
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
                for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
                {
                    grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
                }
            }







        }
        protected void Get_Score_Board_Next()
        {
            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            lbl_total.Text = dttargetorder.Rows.Count.ToString();
            if (temptable.Rows.Count > 0)
            {
                lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
            }
            if (temptable.Rows.Count > 0)
            {

                //   grd_Targetorder.DataBind();





                grd_Targetorder.DataSource = null;
                grd_Targetorder.AutoGenerateColumns = false;




                grd_Targetorder.ColumnCount = 13;

                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;

                grd_Targetorder.Columns[1].Name = "Production Date";
                grd_Targetorder.Columns[1].HeaderText = "PRODUCTION DATE";
                grd_Targetorder.Columns[1].DataPropertyName = "Order_Production_Date";
                grd_Targetorder.Columns[1].Width = 150;

                grd_Targetorder.Columns[2].Name = "User Name";
                grd_Targetorder.Columns[2].HeaderText = "USER NAME";
                grd_Targetorder.Columns[2].DataPropertyName = "User_Name";
                grd_Targetorder.Columns[2].Width = 110;


                grd_Targetorder.Columns[3].Name = "Order Number";
                grd_Targetorder.Columns[3].HeaderText = "ORDER NUMBER";
                grd_Targetorder.Columns[3].DataPropertyName = "Client_Order_Number";
                grd_Targetorder.Columns[3].Width = 175;
                grd_Targetorder.Columns[3].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "ORDER NUMBER";
                link.DataPropertyName = "Client_Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;
                if (Role_Id == 1)
                {
                    grd_Targetorder.Columns[4].Name = "Client Name";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "SubProcessName";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[5].Width = 220;
                }
                else
                {

                    grd_Targetorder.Columns[4].Name = "Client_Number";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "Subprocess_Number";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                    grd_Targetorder.Columns[5].Width = 220;

                }

                grd_Targetorder.Columns[6].Name = "OrderType";
                grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[6].Width = 160;

                grd_Targetorder.Columns[7].Name = "Task";
                grd_Targetorder.Columns[7].HeaderText = "TASK";
                grd_Targetorder.Columns[7].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[7].Width = 120;

                grd_Targetorder.Columns[8].Name = "Status";
                grd_Targetorder.Columns[8].HeaderText = "PROGRESS STATUS";
                grd_Targetorder.Columns[8].DataPropertyName = "Progress_Status";
                grd_Targetorder.Columns[8].Width = 160;


                grd_Targetorder.Columns[9].Name = "StartTime";
                grd_Targetorder.Columns[9].HeaderText = "START TIME";
                grd_Targetorder.Columns[9].DataPropertyName = "Start_Time";
                grd_Targetorder.Columns[9].Width = 120;

                grd_Targetorder.Columns[10].Name = "EndTime";
                grd_Targetorder.Columns[10].HeaderText = "END TIME";
                grd_Targetorder.Columns[10].DataPropertyName = "End_Time";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "TotalTime";
                grd_Targetorder.Columns[11].HeaderText = "TOTAL TIME";
                grd_Targetorder.Columns[11].DataPropertyName = "Total_Time";
                grd_Targetorder.Columns[11].Width = 100;


                grd_Targetorder.Columns[12].Name = "Order Id";
                grd_Targetorder.Columns[12].HeaderText = "Order id";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[12].Width = 100;
                grd_Targetorder.Columns[12].Visible = false;

                grd_Targetorder.DataSource = temptable;



                if (User_id == scoreuserid && Role_Id == 2)
                {

                    grd_Targetorder.Columns[4].Visible = false;
                    grd_Targetorder.Columns[5].Visible = false;
                    grd_Targetorder.Columns[9].Visible = false;
                    grd_Targetorder.Columns[10].Visible = false;
                    grd_Targetorder.Columns[11].Visible = false;
                }
                else if (Role_Id == 1 || Role_Id == 6 || Role_Id == 4)
                {
                    grd_Targetorder.Columns[4].Visible = true;
                    grd_Targetorder.Columns[5].Visible = true;
                    grd_Targetorder.Columns[9].Visible = true;
                    grd_Targetorder.Columns[10].Visible = true;
                    grd_Targetorder.Columns[11].Visible = true;

                }
                else
                {
                    grd_Targetorder.Visible = false;
                    grd_Targetorder.Rows.Clear();
                    grd_Targetorder.DataSource = null;
                }



            }
            else
            {
                grd_Targetorder.Visible = true;
                //grd_Targetorder.Rows.Clear();
                grd_Targetorder.DataSource = null;
            }
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }
        }

        protected void Get_User_Hour_Wise_Next()
        {
            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            lbl_total.Text = dttargetorder.Rows.Count.ToString();
            if (temptable.Rows.Count > 0)
            {
                lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
            }
            if (temptable.Rows.Count > 0)
            {

                //   grd_Targetorder.DataBind();





                grd_Targetorder.DataSource = null;
                grd_Targetorder.AutoGenerateColumns = false;




                grd_Targetorder.ColumnCount = 13;

                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;

                grd_Targetorder.Columns[1].Name = "Production Date";
                grd_Targetorder.Columns[1].HeaderText = "PRODUCTION DATE";
                grd_Targetorder.Columns[1].DataPropertyName = "Order_Production_Date";
                grd_Targetorder.Columns[1].Width = 150;

                grd_Targetorder.Columns[2].Name = "User Name";
                grd_Targetorder.Columns[2].HeaderText = "USER NAME";
                grd_Targetorder.Columns[2].DataPropertyName = "User_Name";
                grd_Targetorder.Columns[2].Width = 110;


                grd_Targetorder.Columns[3].Name = "Order Number";
                grd_Targetorder.Columns[3].HeaderText = "ORDER NUMBER";
                grd_Targetorder.Columns[3].DataPropertyName = "Client_Order_Number";
                grd_Targetorder.Columns[3].Width = 175;
                grd_Targetorder.Columns[3].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "ORDER NUMBER";
                link.DataPropertyName = "Client_Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;
                if (Role_Id == 1)
                {
                    grd_Targetorder.Columns[4].Name = "Client Name";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "SubProcessName";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[5].Width = 220;
                }
                else
                {


                    grd_Targetorder.Columns[4].Name = "Client_Number";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "Subprocess_Number";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                    grd_Targetorder.Columns[5].Width = 220;

                }

                grd_Targetorder.Columns[6].Name = "OrderType";
                grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[6].Width = 160;

                grd_Targetorder.Columns[7].Name = "Task";
                grd_Targetorder.Columns[7].HeaderText = "TASK";
                grd_Targetorder.Columns[7].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[7].Width = 120;

                grd_Targetorder.Columns[8].Name = "Status";
                grd_Targetorder.Columns[8].HeaderText = "PROGRESS STATUS";
                grd_Targetorder.Columns[8].DataPropertyName = "Progress_Status";
                grd_Targetorder.Columns[8].Width = 160;


                grd_Targetorder.Columns[9].Name = "StartTime";
                grd_Targetorder.Columns[9].HeaderText = "START TIME";
                grd_Targetorder.Columns[9].DataPropertyName = "Start_Time";
                grd_Targetorder.Columns[9].Width = 120;

                grd_Targetorder.Columns[10].Name = "EndTime";
                grd_Targetorder.Columns[10].HeaderText = "END TIME";
                grd_Targetorder.Columns[10].DataPropertyName = "End_Time";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "TotalTime";
                grd_Targetorder.Columns[11].HeaderText = "TOTAL TIME";
                grd_Targetorder.Columns[11].DataPropertyName = "Total_Time";
                grd_Targetorder.Columns[11].Width = 100;


                grd_Targetorder.Columns[12].Name = "Order Id";
                grd_Targetorder.Columns[12].HeaderText = "Order id";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[12].Width = 100;
                grd_Targetorder.Columns[12].Visible = false;

                grd_Targetorder.DataSource = temptable;



                if (User_id == scoreuserid && Role_Id == 2)
                {

                    grd_Targetorder.Columns[4].Visible = false;
                    grd_Targetorder.Columns[5].Visible = false;
                    grd_Targetorder.Columns[9].Visible = false;
                    grd_Targetorder.Columns[10].Visible = false;
                    grd_Targetorder.Columns[11].Visible = false;
                }
                else if (Role_Id == 1 || Role_Id==6 || Role_Id==4)
                {
                    grd_Targetorder.Columns[4].Visible = true;
                    grd_Targetorder.Columns[5].Visible = true;
                    grd_Targetorder.Columns[9].Visible = true;
                    grd_Targetorder.Columns[10].Visible = true;
                    grd_Targetorder.Columns[11].Visible = true;

                }
                else
                {
                    grd_Targetorder.Visible = false;
                    grd_Targetorder.Rows.Clear();
                    grd_Targetorder.DataSource = null;
                }



            }
            else
            {
                grd_Targetorder.Visible = true;
                //grd_Targetorder.Rows.Clear();
                grd_Targetorder.DataSource = null;
            }
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }
        }

        protected void Get_User_24_7_Hour_Wise_Next()
        {
            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            lbl_total.Text = dttargetorder.Rows.Count.ToString();
            if (temptable.Rows.Count > 0)
            {
                lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
            }
            if (temptable.Rows.Count > 0)
            {

                //   grd_Targetorder.DataBind();





                grd_Targetorder.DataSource = null;
                grd_Targetorder.AutoGenerateColumns = false;




                grd_Targetorder.ColumnCount = 13;

                grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;

                grd_Targetorder.Columns[1].Name = "Production Date";
                grd_Targetorder.Columns[1].HeaderText = "PRODUCTION DATE";
                grd_Targetorder.Columns[1].DataPropertyName = "Order_Production_Date";
                grd_Targetorder.Columns[1].Width = 150;

                grd_Targetorder.Columns[2].Name = "User Name";
                grd_Targetorder.Columns[2].HeaderText = "USER NAME";
                grd_Targetorder.Columns[2].DataPropertyName = "User_Name";
                grd_Targetorder.Columns[2].Width = 110;


                grd_Targetorder.Columns[3].Name = "Order Number";
                grd_Targetorder.Columns[3].HeaderText = "ORDER NUMBER";
                grd_Targetorder.Columns[3].DataPropertyName = "Client_Order_Number";
                grd_Targetorder.Columns[3].Width = 175;
                grd_Targetorder.Columns[3].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "ORDER NUMBER";
                link.DataPropertyName = "Client_Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;

                if (Role_Id == 1)
                {
                    grd_Targetorder.Columns[4].Name = "Client Name";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "SubProcessName";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[5].Width = 220;

                }
                else
                {

                    grd_Targetorder.Columns[4].Name = "Client_Number";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "Subprocess_Number";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                    grd_Targetorder.Columns[5].Width = 220;
                }


                grd_Targetorder.Columns[6].Name = "OrderType";
                grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[6].Width = 160;

                grd_Targetorder.Columns[7].Name = "Task";
                grd_Targetorder.Columns[7].HeaderText = "TASK";
                grd_Targetorder.Columns[7].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[7].Width = 120;

                grd_Targetorder.Columns[8].Name = "Status";
                grd_Targetorder.Columns[8].HeaderText = "PROGRESS STATUS";
                grd_Targetorder.Columns[8].DataPropertyName = "Progress_Status";
                grd_Targetorder.Columns[8].Width = 160;


                grd_Targetorder.Columns[9].Name = "StartTime";
                grd_Targetorder.Columns[9].HeaderText = "START TIME";
                grd_Targetorder.Columns[9].DataPropertyName = "Start_Time";
                grd_Targetorder.Columns[9].Width = 120;

                grd_Targetorder.Columns[10].Name = "EndTime";
                grd_Targetorder.Columns[10].HeaderText = "END TIME";
                grd_Targetorder.Columns[10].DataPropertyName = "End_Time";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "TotalTime";
                grd_Targetorder.Columns[11].HeaderText = "TOTAL TIME";
                grd_Targetorder.Columns[11].DataPropertyName = "Total_Time";
                grd_Targetorder.Columns[11].Width = 100;


                grd_Targetorder.Columns[12].Name = "Order Id";
                grd_Targetorder.Columns[12].HeaderText = "Order id";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[12].Width = 100;
                grd_Targetorder.Columns[12].Visible = false;

                grd_Targetorder.DataSource = temptable;



                if (User_id == scoreuserid && Role_Id == 2)
                {

                    grd_Targetorder.Columns[4].Visible = false;
                    grd_Targetorder.Columns[5].Visible = false;
                    grd_Targetorder.Columns[9].Visible = false;
                    grd_Targetorder.Columns[10].Visible = false;
                    grd_Targetorder.Columns[11].Visible = false;
                }
                else if (Role_Id ==1 || Role_Id==6 || Role_Id==4)
                {
                    grd_Targetorder.Columns[4].Visible = true;
                    grd_Targetorder.Columns[5].Visible = true;
                    grd_Targetorder.Columns[9].Visible = true;
                    grd_Targetorder.Columns[10].Visible = true;
                    grd_Targetorder.Columns[11].Visible = true;

                }
                else
                {
                    grd_Targetorder.Visible = false;
                    grd_Targetorder.Rows.Clear();
                    grd_Targetorder.DataSource = null;
                }



            }
            else
            {
                grd_Targetorder.Visible = true;
                //grd_Targetorder.Rows.Clear();
                grd_Targetorder.DataSource = null;
            }
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
            for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }
        }
        protected void Get_Score_Board_GridviewBind()
        {
            Grid_Count.Visible = true;
            Grid_Order_Type_Count.Visible = true;
            //TargetDashboard.ActiveForm.Text = "SCORE BOARD";
            lbl_Headername.Text = "SCORE BOARD";
            DateTime d1 = DateTime.Now;
            d1 = d1.AddDays(-1);
            //  Order_Target = ViewState["Order_Target"].ToString();
            //  Time_Zone = ViewState["Time_Zone"].ToString();
            Hashtable httargetorder = new Hashtable();
            //DataTable dttargetorder = new System.Data.DataTable();

            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();


            Hashtable httype = new Hashtable();
            DataTable dttype = new DataTable();

            if (Valuegrd == 2)
            {
                httargetorder.Add("@Trans", "User_ID");
                htcount.Add("@Trans", "Current_Date_Completed_Order_Status_Count");
                httype.Add("@Trans", "Current_Date_Completed_OrderType_Wise_Status_Count");

            }
            else if (Valuegrd == 3)
            {

                httargetorder.Add("@Trans", "GET_PENDING_USER_ID");

                htcount.Add("@Trans", "Current_Date_PENDING_Order_Status_Count");
                httype.Add("@Trans", "Current_Date_PENDING_Order_Type_Wise_Status_Count");
            }

            DateTime dt = DateTime.ParseExact(date_ScoreBoard, "dd/MM/yyyy", null);
            //DateTime dt = Convert.ToDateTime(date_ScoreBoard.ToString());

            string date = dt.ToString("dd/MM/yyyy");
            httargetorder.Add("@User_Id", scoreuserid);
            httargetorder.Add("@Date", dt);
            htcount.Add("@User_Id", scoreuserid);
            htcount.Add("@To_date", date);
            httype.Add("@User_Id", scoreuserid);
            httype.Add("@To_date", date);
            if (scoreboard_name == "Rework")
            {
                dttargetorder = dataaccess.ExecuteSP("Sp_Rework_Score_Board", httargetorder);
                dtcount = dataaccess.ExecuteSP("Sp_Rework_Score_Board", htcount);
                dttype = dataaccess.ExecuteSP("Sp_Rework_Score_Board", httype);
            }
            else if (scoreboard_name == "Current")
            {
                dttargetorder = dataaccess.ExecuteSP("Sp_Score_Board", httargetorder);
                dtcount = dataaccess.ExecuteSP("Sp_Score_Board", htcount);
                dttype = dataaccess.ExecuteSP("Sp_Score_Board", httype);
            }
            else if (scoreboard_name == "Superqc")
            {
                dttargetorder = dataaccess.ExecuteSP("Sp_SuperQc_Score_Board", httargetorder);
                dtcount = dataaccess.ExecuteSP("Sp_SuperQc_Score_Board", htcount);
                dttype = dataaccess.ExecuteSP("Sp_SuperQc_Score_Board", httype);
            }
            dtexport = dttargetorder;


            if (dtcount.Rows.Count > 0)
            {

                Grid_Count.DataSource = dtcount;

            }
            else
            {


                Grid_Count.Rows.Clear();
            }

            if (dttype.Rows.Count > 0)
            {

                Grid_Order_Type_Count.DataSource = dttype;

            }
            else
            {


                Grid_Order_Type_Count.Rows.Clear();
            }

            System.Data.DataTable temptable = dttargetorder.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dttargetorder.Rows.Count)
            {
                endindex = dttargetorder.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow row = temptable.NewRow();
                GetDataRow_Target_Orders(ref row, dttargetorder.Rows[i]);
                temptable.Rows.Add(row);
            }
            lbl_total.Text = dttargetorder.Rows.Count.ToString();
            if (temptable.Rows.Count > 0)
            {
                lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
            }
            if (temptable.Rows.Count > 0)
            {

                //   grd_Targetorder.DataBind();





                grd_Targetorder.DataSource = null;
                grd_Targetorder.AutoGenerateColumns = false;


              

                grd_Targetorder.ColumnCount = 13;

               grd_Targetorder.Columns[0].Name = "SNo";
                grd_Targetorder.Columns[0].HeaderText = "S. No";
                grd_Targetorder.Columns[0].Width = 65;

                grd_Targetorder.Columns[1].Name = "Production Date";
                grd_Targetorder.Columns[1].HeaderText = "PRODUCTION DATE";
                grd_Targetorder.Columns[1].DataPropertyName = "Order_Production_Date";
                grd_Targetorder.Columns[1].Width = 150;

                grd_Targetorder.Columns[2].Name = "User Name";
                grd_Targetorder.Columns[2].HeaderText = "USER NAME";
                grd_Targetorder.Columns[2].DataPropertyName = "User_Name";
                grd_Targetorder.Columns[2].Width = 110;


                grd_Targetorder.Columns[3].Name = "Order Number";
                grd_Targetorder.Columns[3].HeaderText = "ORDER NUMBER";
                grd_Targetorder.Columns[3].DataPropertyName = "Client_Order_Number";
                grd_Targetorder.Columns[3].Width = 175;
                grd_Targetorder.Columns[3].Visible = false;

                DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                grd_Targetorder.Columns.Add(link);
                link.Name = "Order Number";
                link.HeaderText = "ORDER NUMBER";
                link.DataPropertyName = "Client_Order_Number";
                link.Width = 200;
                link.DisplayIndex = 1;

                if (Role_Id == 1)
                {
                    grd_Targetorder.Columns[4].Name = "Client Name";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "SubProcessName";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[5].Width = 220;

                }
                else
                {
                    grd_Targetorder.Columns[4].Name = "Client_Number";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "Subprocess_Number";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                    grd_Targetorder.Columns[5].Width = 220;


                

                }


                grd_Targetorder.Columns[6].Name = "OrderType";
                grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[6].Width = 160;

                grd_Targetorder.Columns[7].Name = "Task";
                grd_Targetorder.Columns[7].HeaderText = "TASK";
                grd_Targetorder.Columns[7].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[7].Width = 120;

                grd_Targetorder.Columns[8].Name = "Status";
                grd_Targetorder.Columns[8].HeaderText = "PROGRESS STATUS";
                grd_Targetorder.Columns[8].DataPropertyName = "Progress_Status";
                grd_Targetorder.Columns[8].Width = 160;


                grd_Targetorder.Columns[9].Name = "StartTime";
                grd_Targetorder.Columns[9].HeaderText = "START TIME";
                grd_Targetorder.Columns[9].DataPropertyName = "Start_Time";
                grd_Targetorder.Columns[9].Width = 120;

                grd_Targetorder.Columns[10].Name = "EndTime";
                grd_Targetorder.Columns[10].HeaderText = "END TIME";
                grd_Targetorder.Columns[10].DataPropertyName = "End_Time";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "TotalTime";
                grd_Targetorder.Columns[11].HeaderText = "TOTAL TIME";
                grd_Targetorder.Columns[11].DataPropertyName = "Total_Time";
                grd_Targetorder.Columns[11].Width = 100;


                grd_Targetorder.Columns[12].Name = "Order Id";
                grd_Targetorder.Columns[12].HeaderText = "Order id";
                grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[12].Width = 100;
                grd_Targetorder.Columns[12].Visible = false;

                grd_Targetorder.DataSource = temptable;



                if (User_id == scoreuserid && Role_Id == 2)
                {

                    grd_Targetorder.Columns[4].Visible = false;
                    grd_Targetorder.Columns[5].Visible = false;
                    grd_Targetorder.Columns[9].Visible = false;
                    grd_Targetorder.Columns[10].Visible = false;
                    grd_Targetorder.Columns[11].Visible = false;
                }
                else if (Role_Id == 1 || Role_Id==6 || Role_Id==4)
                {
                    grd_Targetorder.Columns[4].Visible = true;
                    grd_Targetorder.Columns[5].Visible = true;
                    grd_Targetorder.Columns[9].Visible = true;
                    grd_Targetorder.Columns[10].Visible = true;
                    grd_Targetorder.Columns[11].Visible = true;

                }
                else
                {
                    grd_Targetorder.Visible = false;
                    //grd_Targetorder.Rows.Clear();
                    grd_Targetorder.DataSource = null;
                }



            }
            else
            {
                grd_Targetorder.Visible = true;
                //grd_Targetorder.Rows.Clear();
                grd_Targetorder.DataSource = null;
            }
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize);
              for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
            {
                grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
            }

            }

        private void grd_Targetorder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)
            {
                //cProbar.startProgress();
                form_loader.Start_progres();
                if (scoreboard_name == "Rework")
                {
                    Ordermanagement_01.Rework_Superqc_Order_Entry OrderEntry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(grd_Targetorder.Rows[e.RowIndex].Cells[12].Value.ToString()), User_id,"Rework",Role_Id.ToString(),"");
                    OrderEntry.Show();
                }
                else if (scoreboard_name == "Superqc")
                {
                    Ordermanagement_01.Rework_Superqc_Order_Entry OrderEntry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(grd_Targetorder.Rows[e.RowIndex].Cells[12].Value.ToString()), User_id, "Superqc", Role_Id.ToString(),"");
                    OrderEntry.Show();
                }
                else
                {
                    Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(grd_Targetorder.Rows[e.RowIndex].Cells[12].Value.ToString()), User_id,Convert.ToString(Role_Id),"");
                    OrderEntry.Show();
                }
                //cProbar.stopProgress();
            }
        }
        protected void Get_Table_Row_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            this.Cursor = currentCursor;
        }
        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            DataView dtsearch = new DataView(dtexport);


            if (txt_SearchOrdernumber.Text != "")
            {
                string search = txt_SearchOrdernumber.Text.ToString();
                dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%'";
                
                dt = dtsearch.ToTable();
                System.Data.DataTable temptable = dt.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dt.Rows.Count)
                {
                    endindex = dt.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    Get_Table_Row_Search(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }


                if (temptable.Rows.Count > 0)
                {
                    if (lbl_Headername.Text == "Pending Orders" || lbl_Headername.Text == "RECEIVED ORDERS"
                        || lbl_Headername.Text == "COMPLETED ORDERS" || lbl_Headername.Text == "WORK PROGRESSING ORDERS"
                        || lbl_Headername.Text == "OPENED ORDERS" || lbl_Headername.Text == "NOT ASSIGNED ORDERS"
                        || lbl_Headername.Text == "HOLD/CLARIFICATION ORDERS" || lbl_Headername.Text == "CANCELLED ORDERS")
                    {
                        grd_Targetorder.DataSource = null;
                        grd_Targetorder.AutoGenerateColumns = false;
                        grd_Targetorder.ColumnCount = 13;

                        grd_Targetorder.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                        grd_Targetorder.EnableHeadersVisualStyles = false;

                        grd_Targetorder.Columns[1].Name = "Order Number";
                        grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                        grd_Targetorder.Columns[1].DataPropertyName = "Client_Order_Number";
                        grd_Targetorder.Columns[1].Width = 200;
                        grd_Targetorder.Columns[1].Visible = false;

                        DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                        grd_Targetorder.Columns.Add(link);
                        link.Name = "Order Number";
                        link.HeaderText = "ORDER NUMBER";
                        link.DataPropertyName = "Client_Order_Number";
                        link.Width = 200;
                        link.DisplayIndex = 1;

                        if (Role_Id == 1)
                        {
                            grd_Targetorder.Columns[2].Name = "Customer";
                            grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                            grd_Targetorder.Columns[2].DataPropertyName = "Client_Name";
                            grd_Targetorder.Columns[2].Width = 140;

                            grd_Targetorder.Columns[3].Name = "SubProcess";
                            grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS NAME";
                            grd_Targetorder.Columns[3].DataPropertyName = "Sub_ProcessName";
                            grd_Targetorder.Columns[3].Width = 220;
                        }
                        else
                        {

                     
                            grd_Targetorder.Columns[2].Name = "Client_Number";
                            grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                            grd_Targetorder.Columns[2].DataPropertyName = "Client_Number";
                            grd_Targetorder.Columns[2].Width = 140;

                            grd_Targetorder.Columns[3].Name = "Client_Number";
                            grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS NAME";
                            grd_Targetorder.Columns[3].DataPropertyName = "Sub_PrClient_NumberocessName";
                            grd_Targetorder.Columns[3].Width = 220;

                        }
                        grd_Targetorder.Columns[4].Name = "Submited";
                        grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                        grd_Targetorder.Columns[4].DataPropertyName = "Date";
                        grd_Targetorder.Columns[4].Width = 120;

                        grd_Targetorder.Columns[5].Name = "OrderType";
                        grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                        grd_Targetorder.Columns[5].DataPropertyName = "Order_Type";
                        grd_Targetorder.Columns[5].Width = 160;

                        grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                        grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                        grd_Targetorder.Columns[6].DataPropertyName = "Client_Order_Ref";
                        grd_Targetorder.Columns[6].Width = 170;

                        grd_Targetorder.Columns[7].Name = "SearchType";
                        grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                        grd_Targetorder.Columns[7].DataPropertyName = "County_Type";
                        grd_Targetorder.Columns[7].Width = 160;

                        grd_Targetorder.Columns[8].Name = "County";
                        grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                        grd_Targetorder.Columns[8].DataPropertyName = "County";
                        grd_Targetorder.Columns[8].Width = 140;

                        grd_Targetorder.Columns[9].Name = "State";
                        grd_Targetorder.Columns[9].HeaderText = "STATE";
                        grd_Targetorder.Columns[9].DataPropertyName = "State";
                        grd_Targetorder.Columns[9].Width = 120;

                        grd_Targetorder.Columns[10].Name = "Task";
                        grd_Targetorder.Columns[10].HeaderText = "TASK";
                        grd_Targetorder.Columns[10].DataPropertyName = "Order_Status";
                        grd_Targetorder.Columns[10].Width = 120;

                        grd_Targetorder.Columns[11].Name = "User";
                        grd_Targetorder.Columns[11].HeaderText = "USER NAME";
                        grd_Targetorder.Columns[11].DataPropertyName = "User_Name";
                        grd_Targetorder.Columns[11].Width = 100;

                        grd_Targetorder.Columns[12].Name = "Order Id";
                        grd_Targetorder.Columns[12].HeaderText = "Order id";
                        grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                        grd_Targetorder.Columns[12].Width = 100;
                        grd_Targetorder.Columns[12].Visible = false;

                        //  grd_Targetorder.Columns[12].Visible = false;
                        grd_Targetorder.DataSource = temptable;
                    }
                    else if (lbl_Headername.Text == "CLIENT TAT" || lbl_Headername.Text == "EMPLOYEE TAT")
                    {
                        grd_Targetorder.DataSource = null;

                        grd_Targetorder.AutoGenerateColumns = false;
                        grd_Targetorder.ColumnCount = 13;

                        grd_Targetorder.Columns[0].Name = "SNo";
                        grd_Targetorder.Columns[0].HeaderText = "S. No";
                        grd_Targetorder.Columns[0].Width = 65;
                        grd_Targetorder.Columns[0].Visible = false;

                        grd_Targetorder.Columns[1].Name = "Order Number";
                        grd_Targetorder.Columns[1].HeaderText = "CLIENT ORDER NUMBER";
                        grd_Targetorder.Columns[1].DataPropertyName = "Client_Order_Number";
                        grd_Targetorder.Columns[1].Width = 200;
                        grd_Targetorder.Columns[1].Visible = false;

                        DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                        grd_Targetorder.Columns.Add(link);
                        link.Name = "Order Number";
                        link.HeaderText = "ORDER NUMBER";
                        link.DataPropertyName = "Client_Order_Number";
                        link.Width = 200;
                        link.DisplayIndex = 1;

                        if (Role_Id == 1)
                        {
                            grd_Targetorder.Columns[2].Name = "Client_Name";
                            grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                            grd_Targetorder.Columns[2].DataPropertyName = "Client_Name";
                            grd_Targetorder.Columns[2].Width = 140;

                            grd_Targetorder.Columns[3].Name = "SubProcess";
                            grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                            grd_Targetorder.Columns[3].DataPropertyName = "Sub_ProcessName";
                            grd_Targetorder.Columns[3].Width = 220;
                        }
                        else
                        {
                            grd_Targetorder.Columns[2].Name = "Client_Number";
                            grd_Targetorder.Columns[2].HeaderText = "CUSTOMER NAME";
                            grd_Targetorder.Columns[2].DataPropertyName = "Client_Number";
                            grd_Targetorder.Columns[2].Width = 140;

                            grd_Targetorder.Columns[3].Name = "Subprocess_Number";
                            grd_Targetorder.Columns[3].HeaderText = "SUB PROCESS";
                            grd_Targetorder.Columns[3].DataPropertyName = "Subprocess_Number";
                            grd_Targetorder.Columns[3].Width = 220;

                        }


                        grd_Targetorder.Columns[4].Name = "Submited";
                        grd_Targetorder.Columns[4].HeaderText = "SUBMITTED DATE";
                        grd_Targetorder.Columns[4].DataPropertyName = "Date";
                        grd_Targetorder.Columns[4].Width = 120;


                        grd_Targetorder.Columns[5].Name = "OrderType";
                        grd_Targetorder.Columns[5].HeaderText = "ORDER TYPE";
                        grd_Targetorder.Columns[5].DataPropertyName = "Order_Type";
                        grd_Targetorder.Columns[5].Width = 160;


                        grd_Targetorder.Columns[6].Name = "ClientOrderRef";
                        grd_Targetorder.Columns[6].HeaderText = "CLIENT ORDER REF. NO";
                        grd_Targetorder.Columns[6].DataPropertyName = "Client_Order_Ref";
                        grd_Targetorder.Columns[6].Width = 170;


                        grd_Targetorder.Columns[7].Name = "SearchType";
                        grd_Targetorder.Columns[7].HeaderText = "SEARCH TYPE";
                        grd_Targetorder.Columns[7].DataPropertyName = "County_Type";
                        grd_Targetorder.Columns[7].Width = 160;


                        grd_Targetorder.Columns[8].Name = "County";
                        grd_Targetorder.Columns[8].HeaderText = "COUNTY";
                        grd_Targetorder.Columns[8].DataPropertyName = "County";
                        grd_Targetorder.Columns[8].Width = 140;


                        grd_Targetorder.Columns[9].Name = "State";
                        grd_Targetorder.Columns[9].HeaderText = "STATE";
                        grd_Targetorder.Columns[9].DataPropertyName = "State";
                        grd_Targetorder.Columns[9].Width = 120;


                        grd_Targetorder.Columns[10].Name = "Task";
                        grd_Targetorder.Columns[10].HeaderText = "TASK";
                        grd_Targetorder.Columns[10].DataPropertyName = "Order_Status";
                        grd_Targetorder.Columns[10].Width = 120;


                        grd_Targetorder.Columns[11].Name = "User";
                        grd_Targetorder.Columns[11].HeaderText = "USER";
                        grd_Targetorder.Columns[11].DataPropertyName = "User_Name";
                        grd_Targetorder.Columns[11].Width = 100;

                        grd_Targetorder.Columns[12].Name = "Order Id";
                        grd_Targetorder.Columns[12].HeaderText = "Order id";
                        grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                        grd_Targetorder.Columns[12].Width = 100;
                        grd_Targetorder.Columns[12].Visible = false;
                        //  grd_Targetorder.Columns[12].Visible = false;
                        grd_Targetorder.DataSource = temptable;
                    }
                    else
                    {
                        grd_Targetorder.DataSource = null;
                        grd_Targetorder.AutoGenerateColumns = false;




                        grd_Targetorder.ColumnCount = 13;

                        grd_Targetorder.Columns[0].Name = "SNo";
                        grd_Targetorder.Columns[0].HeaderText = "S. No";
                        grd_Targetorder.Columns[0].Width = 65;
                        grd_Targetorder.Columns[0].Visible = false;

                        grd_Targetorder.Columns[1].Name = "Production Date";
                        grd_Targetorder.Columns[1].HeaderText = "PRODUCTION DATE";
                        grd_Targetorder.Columns[1].DataPropertyName = "Order_Production_Date";
                        grd_Targetorder.Columns[1].Width = 150;

                        grd_Targetorder.Columns[2].Name = "User Name";
                        grd_Targetorder.Columns[2].HeaderText = "USER NAME";
                        grd_Targetorder.Columns[2].DataPropertyName = "User_Name";
                        grd_Targetorder.Columns[2].Width = 110;


                        grd_Targetorder.Columns[3].Name = "Order Number";
                        grd_Targetorder.Columns[3].HeaderText = "ORDER NUMBER";
                        grd_Targetorder.Columns[3].DataPropertyName = "Client_Order_Number";
                        grd_Targetorder.Columns[3].Width = 175;
                        grd_Targetorder.Columns[3].Visible = false;

                        DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                        grd_Targetorder.Columns.Add(link);
                        link.Name = "Order Number";
                        link.HeaderText = "ORDER NUMBER";
                        link.DataPropertyName = "Client_Order_Number";
                        link.Width = 200;
                        link.DisplayIndex = 1;

                        if (Role_Id == 1)
                        {
                            grd_Targetorder.Columns[4].Name = "Client Name";
                            grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                            grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                            grd_Targetorder.Columns[4].Width = 130;

                            grd_Targetorder.Columns[5].Name = "SubProcessName";
                            grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                            grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                            grd_Targetorder.Columns[5].Width = 220;

                        }
                        else
                        {
                            grd_Targetorder.Columns[4].Name = "Client_Number";
                            grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                            grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                            grd_Targetorder.Columns[4].Width = 130;

                            grd_Targetorder.Columns[5].Name = "Subprocess_Number";
                            grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                            grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                            grd_Targetorder.Columns[5].Width = 220;



                      
                        

                        }

                        grd_Targetorder.Columns[6].Name = "OrderType";
                        grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                        grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                        grd_Targetorder.Columns[6].Width = 160;

                        grd_Targetorder.Columns[7].Name = "Task";
                        grd_Targetorder.Columns[7].HeaderText = "TASK";
                        grd_Targetorder.Columns[7].DataPropertyName = "Order_Status";
                        grd_Targetorder.Columns[7].Width = 120;

                        grd_Targetorder.Columns[8].Name = "Status";
                        grd_Targetorder.Columns[8].HeaderText = "PROGRESS STATUS";
                        grd_Targetorder.Columns[8].DataPropertyName = "Progress_Status";
                        grd_Targetorder.Columns[8].Width = 160;


                        grd_Targetorder.Columns[9].Name = "StartTime";
                        grd_Targetorder.Columns[9].HeaderText = "START TIME";
                        grd_Targetorder.Columns[9].DataPropertyName = "Start_Time";
                        grd_Targetorder.Columns[9].Width = 120;

                        grd_Targetorder.Columns[10].Name = "EndTime";
                        grd_Targetorder.Columns[10].HeaderText = "END TIME";
                        grd_Targetorder.Columns[10].DataPropertyName = "End_Time";
                        grd_Targetorder.Columns[10].Width = 120;

                        grd_Targetorder.Columns[11].Name = "TotalTime";
                        grd_Targetorder.Columns[11].HeaderText = "TOTAL TIME";
                        grd_Targetorder.Columns[11].DataPropertyName = "Total_Time";
                        grd_Targetorder.Columns[11].Width = 100;


                        grd_Targetorder.Columns[12].Name = "Order Id";
                        grd_Targetorder.Columns[12].HeaderText = "Order id";
                        grd_Targetorder.Columns[12].DataPropertyName = "Order_ID";
                        grd_Targetorder.Columns[12].Width = 100;
                        grd_Targetorder.Columns[12].Visible = false;

                        grd_Targetorder.DataSource = temptable;



                        if (User_id == scoreuserid && Role_Id == 2)
                        {

                            grd_Targetorder.Columns[4].Visible = false;
                            grd_Targetorder.Columns[5].Visible = false;
                            grd_Targetorder.Columns[9].Visible = false;
                            grd_Targetorder.Columns[10].Visible = false;
                            grd_Targetorder.Columns[11].Visible = false;
                        }
                        else if (Role_Id == 1 || Role_Id == 6 || Role_Id == 4)
                        {
                            grd_Targetorder.Columns[4].Visible = true;
                            grd_Targetorder.Columns[5].Visible = true;
                            grd_Targetorder.Columns[9].Visible = true;
                            grd_Targetorder.Columns[10].Visible = true;
                            grd_Targetorder.Columns[11].Visible = true;

                        }
                        else
                        {
                            grd_Targetorder.Visible = false;

                            grd_Targetorder.DataSource = null;
                        }
                    }
                    First_Page();


                }
                else
                {
                    grd_Targetorder.DataSource = null;
                }
            }
            else
            {

                First_Page();
                //grd_Targetorder.DataSource = dtsearch;
                if (Valuegrd == 0)
                {
                    Get_Target_orders_to_next();

                }
                else if (Valuegrd == 1)
                {
                    Get_Target_Orders_Client_WiseTo_Next();
                    //Get_Target_Orders_Client_WiseTo_GridviewBind();
                }
                else if (Valuegrd == 2 || Valuegrd == 3)
                {
                    Get_Score_Board_Next();
                    //Get_Score_Board_GridviewBind();
                }
            }
            //dttargetorder
            //Client_Order_Number
        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentpageindex + 1 <= (dttargetorder.Rows.Count / pagesize))
            {
                Cursor currentCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;

                currentpageindex++;
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                if (Valuegrd == 0)
                {
                    Get_Target_orders_to_next();

                }
                else if (Valuegrd == 1)
                {
                    Get_Target_Orders_Client_WiseTo_Next();
                    //Get_Target_Orders_Client_WiseTo_GridviewBind();
                }
                else if (Valuegrd == 2 || Valuegrd == 3)
                {
                    Get_Score_Board_Next();
                    //Get_Score_Board_GridviewBind();
                }
                else if (Valuegrd == 4)
                {
                    Get_User_Hour_Wise_Next();
                    //Get_Score_Board_GridviewBind();
                }
                else if (Valuegrd == 5)
                {
                    Get_User_24_7_Hour_Wise_Next();
                    //Get_Score_Board_GridviewBind();
                }
                this.Cursor = currentCursor;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize) - 1;
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            if (Valuegrd == 0)
            {
                Get_Target_orders_to_next();
            }
            else if (Valuegrd == 1)
            {
                Get_Target_Orders_Client_WiseTo_Next();
                //Get_Target_Orders_Client_WiseTo_GridviewBind();
            }
            else if (Valuegrd == 2 || Valuegrd == 3)
            {
                Get_Score_Board_Next();
                //Get_Score_Board_GridviewBind();
            }
            else if (Valuegrd == 4)
            {
                Get_User_Hour_Wise_Next();
                //Get_Score_Board_GridviewBind();
            }
            else if (Valuegrd == 5)
            {
                Get_User_24_7_Hour_Wise_Next();
                //Get_Score_Board_GridviewBind();
            }
            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentpageindex >= 1)
            {
                Cursor currentCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                // splitContainer1.Enabled = false;
                currentpageindex--;
                if (currentpageindex == 0)
                {
                    btnPrevious.Enabled = false;
                    btnFirst.Enabled = false;
                }
                else
                {
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;

                }
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                if (Valuegrd == 0)
                {
                    Get_Target_orders_to_next();
                }
                else if (Valuegrd == 1)
                {
                    Get_Target_Orders_Client_WiseTo_Next();
                    //Get_Target_Orders_Client_WiseTo_GridviewBind();
                }
                else if (Valuegrd == 2 || Valuegrd == 3)
                {
                    Get_Score_Board_Next();
                    //Get_Score_Board_GridviewBind();
                }
                else if (Valuegrd == 4)
                {
                    Get_User_Hour_Wise_Next();
                    //Get_Score_Board_GridviewBind();
                }
                else if (Valuegrd == 5)
                {
                    Get_User_24_7_Hour_Wise_Next();
                    //Get_Score_Board_GridviewBind();
                }
                this.Cursor = currentCursor;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (Valuegrd == 0)
            {
                Get_Target_orders_to_next();
            }
            else if (Valuegrd == 1)
            {
                Get_Target_Orders_Client_WiseTo_Next();
                //Get_Target_Orders_Client_WiseTo_GridviewBind();
            }
            else if (Valuegrd == 2 || Valuegrd == 3)
            {
                Get_Score_Board_Next();
                //Get_Score_Board_GridviewBind();
            }
            else if (Valuegrd == 4)
            {
                Get_User_Hour_Wise_Next();
                //Get_Score_Board_GridviewBind();
            }
            else if (Valuegrd == 5)
            {
                Get_User_24_7_Hour_Wise_Next();
                //Get_Score_Board_GridviewBind();
            }
            this.Cursor = currentCursor;
        }
        
        
        }
    }
    

