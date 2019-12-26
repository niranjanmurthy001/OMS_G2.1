using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01
{
    public partial class Employee_Production_Score_View : Form
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
        int pagesize = 100;
        int Valuegrd, scoreuserid;
        string date_ScoreBoard;
        int User_id, Role_Id;
        string scoreboard_name;
        string Employee_Completd, Employee_User_Id;
        string Employee_Hour;
        string First_Date, Second_Date;
        public Employee_Production_Score_View(string OrderTarget, string TimeZone, string Order_ViewType, int Value, int score_user_id, string Tat, string score_user, string pending_header, string date, int USER_ID, string USER_ROLE_ID, string Scoreboard_name)
        {
            InitializeComponent();

            Order_Target = OrderTarget;
            Time_Zone = TimeZone;
            First_Date = Order_ViewType;
            Second_Date = date;
            OrderViewType = Order_ViewType;

            Employee_Completd = score_user_id.ToString();



            Employee_User_Id = TimeZone.ToString();

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
        }

        private void Employee_Production_Score_View_Load(object sender, EventArgs e)
        {

            if (Valuegrd == 4)
            {


                Gridview_Employee_Production_First_Date_Dataview();
                Bind_Grid_View_Total();
            }
            else if (Valuegrd == 5)
            {


                Gridview_Employee_Production_24_7_Date_Dataview();
                Bind_Grid_View_Total();
            }
            btnFirst_Click(sender, e);
        }
        private void Bind_Grid_View_Total()
        {

            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();
            htcount.Add("@Trans", "SELECT_BY_TOTAL_LIVE_Completed_Order_WORK_TYPE_Count");
            dtcount = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htcount);

            if (dtcount.Rows.Count > 0) { Grid_Total.DataSource = dtcount; } else { Grid_Total.Rows.Clear(); }

        }

        private void Gridview_Employee_Production_First_Date_Dataview()
        {
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();





            Hashtable htlivecount = new Hashtable();
            Hashtable htreworkcount = new Hashtable();
            Hashtable htsuperqccount = new Hashtable();
            DataTable dtlivecount = new DataTable();
            DataTable dtreworkcount = new DataTable();
            DataTable dtsuperqccount = new DataTable();


            Hashtable htlivetype = new Hashtable();
            DataTable dtlivetype = new DataTable();

            Hashtable htReworktype = new Hashtable();
            DataTable dtReworktype = new DataTable();

            Hashtable htSuperqctype = new Hashtable();
            DataTable dtsuperqctype = new DataTable();










            if (Employee_Completd != "0")
            {


                htlivecount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_LIVE_Completed_Order_Status_Count");
                htlivecount.Add("@Firstdate", First_Date);
                htlivecount.Add("@Hour", Employee_Hour);
                htlivecount.Add("@User_Id", Employee_User_Id);

                htreworkcount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_REWORK_Completed_Order_Status_Count");
                htreworkcount.Add("@Firstdate", First_Date);
                htreworkcount.Add("@Hour", Employee_Hour);
                htreworkcount.Add("@User_Id", Employee_User_Id);


                htsuperqccount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_SUPER_QC_Completed_Order_Status_Count");
                htsuperqccount.Add("@Firstdate", First_Date);
                htsuperqccount.Add("@Hour", Employee_Hour);
                htsuperqccount.Add("@User_Id", Employee_User_Id);




                htlivetype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_LIVE_Completed_OrderType_Wise_Status_Count");
                htlivetype.Add("@Firstdate", First_Date);
                htlivetype.Add("@Hour", Employee_Hour);
                htlivetype.Add("@User_Id", Employee_User_Id);


                htReworktype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_REWORK_Completed_OrderType_Wise_Status_Count");
                htReworktype.Add("@Firstdate", First_Date);
                htReworktype.Add("@Hour", Employee_Hour);
                htReworktype.Add("@User_Id", Employee_User_Id);

                htSuperqctype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_SUPER_QC_Completed_OrderType_Wise_Status_Count");
                htSuperqctype.Add("@Firstdate", First_Date);
                htSuperqctype.Add("@Hour", Employee_Hour);
                htSuperqctype.Add("@User_Id", Employee_User_Id);





            }
            else if (Employee_Completd == "0")
            {
                htlivecount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_LIVE_NOT_Completed_Order_Status_Count");
                htlivecount.Add("@Firstdate", First_Date);
                htlivecount.Add("@Hour", Employee_Hour);
                htlivecount.Add("@User_Id", Employee_User_Id);

                htreworkcount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_REWORK_NOT_Completed_Order_Status_Count");
                htreworkcount.Add("@Firstdate", First_Date);
                htreworkcount.Add("@Hour", Employee_Hour);
                htreworkcount.Add("@User_Id", Employee_User_Id);


                htsuperqccount.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_SUPER_QC_NOT_Completed_Order_Status_Count");
                htsuperqccount.Add("@Firstdate", First_Date);
                htsuperqccount.Add("@Hour", Employee_Hour);
                htsuperqccount.Add("@User_Id", Employee_User_Id);




                htlivetype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_LIVE_NOT_Completed_OrderType_Wise_Status_Count");
                htlivetype.Add("@Firstdate", First_Date);
                htlivetype.Add("@Hour", Employee_Hour);
                htlivetype.Add("@User_Id", Employee_User_Id);


                htReworktype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_REWORK_NOT_Completed_OrderType_Wise_Status_Count");
                htReworktype.Add("@Firstdate", First_Date);
                htReworktype.Add("@Hour", Employee_Hour);
                htReworktype.Add("@User_Id", Employee_User_Id);

                htSuperqctype.Add("@Trans", "SELECT_BY_HOUR_BY_FIRST_DATE_SUPER_QC_NOT_Completed_OrderType_Wise_Status_Count");
                htSuperqctype.Add("@Firstdate", First_Date);
                htSuperqctype.Add("@Hour", Employee_Hour);
                htSuperqctype.Add("@User_Id", Employee_User_Id);
            }


            dtlivecount = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htlivecount);
            dtreworkcount = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htreworkcount);
            dtsuperqccount = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htsuperqccount);

            dtlivetype = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htlivetype);
            dtReworktype = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htReworktype);
            dtsuperqctype = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htSuperqctype);


            if (dtlivecount.Rows.Count > 0) { Grid_Live_Task_Count.DataSource = dtlivecount; } else { Grid_Live_Task_Count.Rows.Clear(); }

            if (dtlivetype.Rows.Count > 0) { Grdi_Live_OrderType_Count.DataSource = dtlivetype; } else { Grdi_Live_OrderType_Count.Rows.Clear(); }



            if (dtreworkcount.Rows.Count > 0) { Grid_Rework_Task_Count.DataSource = dtreworkcount; } else { Grid_Rework_Task_Count.Rows.Clear(); }
            if (dtReworktype.Rows.Count > 0) { Grdi_Rework_OrderType_Count.DataSource = dtReworktype; } else { Grdi_Rework_OrderType_Count.Rows.Clear(); }



            if (dtsuperqccount.Rows.Count > 0) { Grid_Super_Qc_Task_Count.DataSource = dtsuperqccount; } else { Grid_Super_Qc_Task_Count.Rows.Clear(); }
            if (dtsuperqctype.Rows.Count > 0) { Grdi_Super_Qc_OrderType_Count.DataSource = dtsuperqctype; } else { Grdi_Super_Qc_OrderType_Count.Rows.Clear(); }

            ArrangeGrid(Grid_Live_Task_Count);
            ArrangeGrid(Grdi_Live_OrderType_Count);
            ArrangeGrid(Grid_Rework_Task_Count);
            ArrangeGrid(Grdi_Rework_OrderType_Count);
            ArrangeGrid(Grid_Super_Qc_Task_Count);
            ArrangeGrid(Grdi_Super_Qc_OrderType_Count);

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
            dttargetorder = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", ht);

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




                    grd_Targetorder.ColumnCount = 14;

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

                    grd_Targetorder.Columns[4].Name = "Client Name";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "SubProcessName";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[5].Width = 220;

                    grd_Targetorder.Columns[6].Name = "OrderType";
                    grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                    grd_Targetorder.Columns[6].Width = 160;

                    grd_Targetorder.Columns[7].Name = "Order_Work_Type";
                    grd_Targetorder.Columns[7].HeaderText = "ORDER WORK TYPE";
                    grd_Targetorder.Columns[7].DataPropertyName = "Order_Work_Type";
                    grd_Targetorder.Columns[7].Width = 160;

                    grd_Targetorder.Columns[8].Name = "Task";
                    grd_Targetorder.Columns[8].HeaderText = "TASK";
                    grd_Targetorder.Columns[8].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[8].Width = 120;

                    grd_Targetorder.Columns[9].Name = "Status";
                    grd_Targetorder.Columns[9].HeaderText = "PROGRESS STATUS";
                    grd_Targetorder.Columns[9].DataPropertyName = "Progress_Status";
                    grd_Targetorder.Columns[9].Width = 160;


                    grd_Targetorder.Columns[10].Name = "StartTime";
                    grd_Targetorder.Columns[10].HeaderText = "START TIME";
                    grd_Targetorder.Columns[10].DataPropertyName = "Start_Time";
                    grd_Targetorder.Columns[10].Width = 120;

                    grd_Targetorder.Columns[11].Name = "EndTime";
                    grd_Targetorder.Columns[11].HeaderText = "END TIME";
                    grd_Targetorder.Columns[11].DataPropertyName = "End_Time";
                    grd_Targetorder.Columns[11].Width = 120;

                    grd_Targetorder.Columns[12].Name = "TotalTime";
                    grd_Targetorder.Columns[12].HeaderText = "TOTAL TIME";
                    grd_Targetorder.Columns[12].DataPropertyName = "Total_Time";
                    grd_Targetorder.Columns[12].Width = 100;


                    grd_Targetorder.Columns[13].Name = "Order Id";
                    grd_Targetorder.Columns[13].HeaderText = "Order id";
                    grd_Targetorder.Columns[13].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[13].Width = 100;
                    grd_Targetorder.Columns[13].Visible = false;

                    grd_Targetorder.DataSource = temptable;




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
        private void GetDataRow_Target_Orders(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dttargetorder.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        public static void ArrangeGrid(DataGridView Grid)
        {
            int twidth = 0;
            if (Grid.Rows.Count > 0)
            {
                twidth = (Grid.Width * Grid.Columns.Count) / 75;
                for (int i = 0; i < Grid.Columns.Count; i++)
                {
                    Grid.Columns[i].Width = 75;
                }

            }
        }


        //public static void ArrangeGrid(DataGridView Grid)
        //{
        //    int twidth = 0;
        //    if (Grid.Rows.Count > 0)
        //    {
        //        twidth = (Grid.Width * Grid.Columns.Count) / 75;
        //        for (int i = 0; i < Grid.Columns.Count; i++)
        //        {
        //            Grid.Columns[i].Width = twidth;
        //        }

        //    }
        //}

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
            dtinsert = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htinsert);


            Hashtable htlivecount = new Hashtable();
            Hashtable htreworkcount = new Hashtable();
            Hashtable htsuperqccount = new Hashtable();
            DataTable dtlivecount = new DataTable();
            DataTable dtreworkcount = new DataTable();
            DataTable dtsuperqccount = new DataTable();


            Hashtable htlivetype = new Hashtable();
            DataTable dtlivetype = new DataTable();

            Hashtable htReworktype = new Hashtable();
            DataTable dtReworktype = new DataTable();

            Hashtable htSuperqctype = new Hashtable();
            DataTable dtsuperqctype = new DataTable();



            htlivecount.Add("@Trans", "SELECT_BY_TOTAL_LIVE_Completed_Order_Status_Count");
            htreworkcount.Add("@Trans", "SELECT_BY_TOTAL_REWORK_Completed_Order_Status_Count");
            htsuperqccount.Add("@Trans", "SELECT_BY_TOTAL_SUPER_QC_Completed_Order_Status_Count");


            htlivetype.Add("@Trans", "SELECT_BY_TOTAL_LIVE_Completed_OrderType_Wise_Status_Count");
            htReworktype.Add("@Trans", "SELECT_BY_TOTAL_REWORK_Completed_OrderType_Wise_Status_Count");
            htSuperqctype.Add("@Trans", "SELECT_BY_TOTAL_SUPER_QC_Completed_OrderType_Wise_Status_Count");





            dtlivecount = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htlivecount);
            dtreworkcount = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htreworkcount);
            dtsuperqccount = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htsuperqccount);

            dtlivetype = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htlivetype);
            dtReworktype = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htReworktype);
            dtsuperqctype = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", htSuperqctype);

            if (dtlivecount.Rows.Count > 0) { Grid_Live_Task_Count.DataSource = dtlivecount; } else { Grid_Live_Task_Count.Rows.Clear(); }
            if (dtlivetype.Rows.Count > 0) { Grdi_Live_OrderType_Count.DataSource = dtlivetype; } else { Grdi_Live_OrderType_Count.Rows.Clear(); }



            if (dtreworkcount.Rows.Count > 0) { Grid_Rework_Task_Count.DataSource = dtreworkcount; } else { Grid_Rework_Task_Count.Rows.Clear(); }
            if (dtReworktype.Rows.Count > 0) { Grdi_Rework_OrderType_Count.DataSource = dtReworktype; } else { Grdi_Rework_OrderType_Count.Rows.Clear(); }



            if (dtsuperqccount.Rows.Count > 0) { Grid_Super_Qc_Task_Count.DataSource = dtsuperqccount; } else { Grid_Super_Qc_Task_Count.Rows.Clear(); }
            if (dtsuperqctype.Rows.Count > 0) { Grdi_Super_Qc_OrderType_Count.DataSource = dtsuperqctype; } else { Grdi_Super_Qc_OrderType_Count.Rows.Clear(); }



            ht.Add("@Trans", "TOTAL_ORDER_COMPLETED_BY_USER_WISE");



            dttargetorder = dataaccess.ExecuteSP("Sp_User_24_7_Employee_Production_Report", ht);

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




                    grd_Targetorder.ColumnCount = 14;

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

                    grd_Targetorder.Columns[4].Name = "Client Name";
                    grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                    grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                    grd_Targetorder.Columns[4].Width = 130;

                    grd_Targetorder.Columns[5].Name = "SubProcessName";
                    grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                    grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                    grd_Targetorder.Columns[5].Width = 220;

                    grd_Targetorder.Columns[6].Name = "OrderType";
                    grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                    grd_Targetorder.Columns[6].Width = 160;

                    grd_Targetorder.Columns[7].Name = "Order_Work_Type";
                    grd_Targetorder.Columns[7].HeaderText = "ORDER WORK TYPE";
                    grd_Targetorder.Columns[7].DataPropertyName = "Order_Work_Type";
                    grd_Targetorder.Columns[7].Width = 160;

                    grd_Targetorder.Columns[8].Name = "Task";
                    grd_Targetorder.Columns[8].HeaderText = "TASK";
                    grd_Targetorder.Columns[8].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[8].Width = 120;

                    grd_Targetorder.Columns[9].Name = "Status";
                    grd_Targetorder.Columns[9].HeaderText = "PROGRESS STATUS";
                    grd_Targetorder.Columns[9].DataPropertyName = "Progress_Status";
                    grd_Targetorder.Columns[9].Width = 160;


                    grd_Targetorder.Columns[10].Name = "StartTime";
                    grd_Targetorder.Columns[10].HeaderText = "START TIME";
                    grd_Targetorder.Columns[10].DataPropertyName = "Start_Time";
                    grd_Targetorder.Columns[10].Width = 120;

                    grd_Targetorder.Columns[11].Name = "EndTime";
                    grd_Targetorder.Columns[11].HeaderText = "END TIME";
                    grd_Targetorder.Columns[11].DataPropertyName = "End_Time";
                    grd_Targetorder.Columns[11].Width = 120;

                    grd_Targetorder.Columns[12].Name = "TotalTime";
                    grd_Targetorder.Columns[12].HeaderText = "TOTAL TIME";
                    grd_Targetorder.Columns[12].DataPropertyName = "Total_Time";
                    grd_Targetorder.Columns[12].Width = 100;


                    grd_Targetorder.Columns[13].Name = "Order Id";
                    grd_Targetorder.Columns[13].HeaderText = "Order id";
                    grd_Targetorder.Columns[13].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[13].Width = 100;
                    grd_Targetorder.Columns[13].Visible = false;

                    grd_Targetorder.DataSource = temptable;



                    if (Role_Id == 2)
                    {

                        grd_Targetorder.Columns[3].Visible = false;
                        grd_Targetorder.Columns[4].Visible = false;
                        grd_Targetorder.Columns[5].Visible = false;


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
        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;

            if (Valuegrd == 4)
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

            if (Valuegrd == 4)
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

        private void btnNext_Click(object sender, EventArgs e)
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

            if (Valuegrd == 4)
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

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dttargetorder.Rows.Count) / pagesize) - 1;
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;

            if (Valuegrd == 4)
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




                grd_Targetorder.ColumnCount = 14;

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

                grd_Targetorder.Columns[4].Name = "Client Name";
                grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                grd_Targetorder.Columns[4].Width = 130;

                grd_Targetorder.Columns[5].Name = "SubProcessName";
                grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                grd_Targetorder.Columns[5].Width = 220;

                grd_Targetorder.Columns[6].Name = "OrderType";
                grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[6].Width = 160;

                grd_Targetorder.Columns[7].Name = "Order_Work_Type";
                grd_Targetorder.Columns[7].HeaderText = "ORDER WORK TYPE";
                grd_Targetorder.Columns[7].DataPropertyName = "Order_Work_Type";
                grd_Targetorder.Columns[7].Width = 160;

                grd_Targetorder.Columns[8].Name = "Task";
                grd_Targetorder.Columns[8].HeaderText = "TASK";
                grd_Targetorder.Columns[8].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[8].Width = 120;

                grd_Targetorder.Columns[9].Name = "Status";
                grd_Targetorder.Columns[9].HeaderText = "PROGRESS STATUS";
                grd_Targetorder.Columns[9].DataPropertyName = "Progress_Status";
                grd_Targetorder.Columns[9].Width = 160;


                grd_Targetorder.Columns[10].Name = "StartTime";
                grd_Targetorder.Columns[10].HeaderText = "START TIME";
                grd_Targetorder.Columns[10].DataPropertyName = "Start_Time";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "EndTime";
                grd_Targetorder.Columns[11].HeaderText = "END TIME";
                grd_Targetorder.Columns[11].DataPropertyName = "End_Time";
                grd_Targetorder.Columns[11].Width = 120;

                grd_Targetorder.Columns[12].Name = "TotalTime";
                grd_Targetorder.Columns[12].HeaderText = "TOTAL TIME";
                grd_Targetorder.Columns[12].DataPropertyName = "Total_Time";
                grd_Targetorder.Columns[12].Width = 100;


                grd_Targetorder.Columns[13].Name = "Order Id";
                grd_Targetorder.Columns[13].HeaderText = "Order id";
                grd_Targetorder.Columns[13].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[13].Width = 100;
                grd_Targetorder.Columns[13].Visible = false;

                grd_Targetorder.DataSource = temptable;




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




                grd_Targetorder.ColumnCount = 14;

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

                grd_Targetorder.Columns[4].Name = "Client Name";
                grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                grd_Targetorder.Columns[4].DataPropertyName = "Client_Name";
                grd_Targetorder.Columns[4].Width = 130;

                grd_Targetorder.Columns[5].Name = "SubProcessName";
                grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                grd_Targetorder.Columns[5].DataPropertyName = "Sub_ProcessName";
                grd_Targetorder.Columns[5].Width = 220;

                grd_Targetorder.Columns[6].Name = "OrderType";
                grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                grd_Targetorder.Columns[6].Width = 160;

                grd_Targetorder.Columns[7].Name = "Order_Work_Type";
                grd_Targetorder.Columns[7].HeaderText = "ORDER WORK TYPE";
                grd_Targetorder.Columns[7].DataPropertyName = "Order_Work_Type";
                grd_Targetorder.Columns[7].Width = 160;

                grd_Targetorder.Columns[8].Name = "Task";
                grd_Targetorder.Columns[8].HeaderText = "TASK";
                grd_Targetorder.Columns[8].DataPropertyName = "Order_Status";
                grd_Targetorder.Columns[8].Width = 120;

                grd_Targetorder.Columns[9].Name = "Status";
                grd_Targetorder.Columns[9].HeaderText = "PROGRESS STATUS";
                grd_Targetorder.Columns[9].DataPropertyName = "Progress_Status";
                grd_Targetorder.Columns[9].Width = 160;


                grd_Targetorder.Columns[10].Name = "StartTime";
                grd_Targetorder.Columns[10].HeaderText = "START TIME";
                grd_Targetorder.Columns[10].DataPropertyName = "Start_Time";
                grd_Targetorder.Columns[10].Width = 120;

                grd_Targetorder.Columns[11].Name = "EndTime";
                grd_Targetorder.Columns[11].HeaderText = "END TIME";
                grd_Targetorder.Columns[11].DataPropertyName = "End_Time";
                grd_Targetorder.Columns[11].Width = 120;

                grd_Targetorder.Columns[12].Name = "TotalTime";
                grd_Targetorder.Columns[12].HeaderText = "TOTAL TIME";
                grd_Targetorder.Columns[12].DataPropertyName = "Total_Time";
                grd_Targetorder.Columns[12].Width = 100;


                grd_Targetorder.Columns[13].Name = "Order Id";
                grd_Targetorder.Columns[13].HeaderText = "Order id";
                grd_Targetorder.Columns[13].DataPropertyName = "Order_ID";
                grd_Targetorder.Columns[13].Width = 100;
                grd_Targetorder.Columns[13].Visible = false;

                grd_Targetorder.DataSource = temptable;




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

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;

            foreach (DataGridViewRow row in grd_Targetorder.Rows)
            {
                if (txt_SearchOrdernumber.Text != "" && row.Cells[3].Value.ToString().StartsWith(txt_SearchOrdernumber.Text, true, CultureInfo.InvariantCulture) || row.Cells[3].Value.ToString().StartsWith(txt_SearchOrdernumber.Text, true, CultureInfo.InvariantCulture))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }

        private void grd_Targetorder_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 14 && Role_Id != 2)
                {


                    string Order_Id = grd_Targetorder.Rows[e.RowIndex].Cells[13].Value.ToString();
                    string Work_Type = grd_Targetorder.Rows[e.RowIndex].Cells[7].Value.ToString();

                    if (Work_Type == "Live")
                    {

                        Ordermanagement_01.Order_Entry Order_Entry = new Ordermanagement_01.Order_Entry(int.Parse(Order_Id.ToString()), User_id, Role_Id.ToString(),"");
                        Order_Entry.Show();
                    }
                    else if (Work_Type == "Rework")
                    {
                        Ordermanagement_01.Rework_Superqc_Order_Entry orderentry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(Order_Id.ToString()), User_id, "Rework", Role_Id.ToString(),"");
                        orderentry.Show();

                    }
                    else if (Work_Type == "Super Qc")
                    {
                        Ordermanagement_01.Rework_Superqc_Order_Entry orderentry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(Order_Id.ToString()), User_id, "Superqc", Role_Id.ToString(),"");
                        orderentry.Show();

                    }



                }
            }

        }

       

      

        

    }
}
