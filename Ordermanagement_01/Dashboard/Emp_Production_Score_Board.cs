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
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01.Dashboard
{
    public partial class Emp_Production_Score_Board : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int Emp_User_Id;
        string User_Role_Id, Production_Date;

        System.Data.DataTable dtexport = new System.Data.DataTable();
        System.Data.DataTable dttargetorder = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        TimeSpan prod_t, Ideal_t, Break_t, Total_t, Allocte_t;
        int Production_Time, Ideal_Time, Break_Time, Total_Time, Allocated_In_Sec;
        private readonly string efficiency;
        public Emp_Production_Score_Board(int EMP_USER_ID, string USER_ROLE, string PRODUCTION_DATE, string efficiency)
        {
            InitializeComponent();
            Emp_User_Id = EMP_USER_ID;
            User_Role_Id = USER_ROLE;
            Production_Date = PRODUCTION_DATE;
            this.efficiency = efficiency;
        }

        private void Bind_Grid_View_Total()
        {
            Hashtable htcount = new Hashtable();
            DataTable dtcount = new DataTable();
            htcount.Add("@Trans", "SELECT_BY_TOTAL_Completed_Order_WORK_TYPE_Count");
            dtcount = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htcount);

            if (dtcount.Rows.Count > 0) { Grid_Total.DataSource = dtcount; } else { Grid_Total.Rows.Clear(); }

        }

        private void Gridview_Employee_Production_Dataview()
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

            htlivecount.Add("@Trans", "SELECT_LIVE_Completed_Order_Status_Count");
            htlivecount.Add("@User_Id", Emp_User_Id);

            htreworkcount.Add("@Trans", "SELECT_REWORK_Completed_Order_Status_Count");
            htreworkcount.Add("@User_Id", Emp_User_Id);

            htsuperqccount.Add("@Trans", "SELECT_SUPER_QC_Completed_Order_Status_Count");
            htsuperqccount.Add("@User_Id", Emp_User_Id);

            htlivetype.Add("@Trans", "SELECT_LIVE_Completed_OrderType_Wise_Count");
            htlivetype.Add("@User_Id", Emp_User_Id);


            htReworktype.Add("@Trans", "SELECT_REWORK_Completed_OrderType_Wise_Count");
            htReworktype.Add("@User_Id", Emp_User_Id);

            htSuperqctype.Add("@Trans", "SELECT_SUPER_QC_Completed_OrderType_Wise_Count");
            htSuperqctype.Add("@User_Id", Emp_User_Id);

            dtlivecount = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htlivecount);
            dtreworkcount = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htreworkcount);
            dtsuperqccount = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htsuperqccount);

            dtlivetype = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htlivetype);
            dtReworktype = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htReworktype);
            dtsuperqctype = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", htSuperqctype);

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

            ht.Add("@Trans", "GET_COMPLETED_ORDERS");
            ht.Add("@Production_Date", Production_Date);
            ht.Add("@User_Id", Emp_User_Id);
            dttargetorder = dataaccess.ExecuteSP("Sp_Employee_Production_Score_Board", ht);

            if (dttargetorder.Rows.Count > 0)
            {
                lbl_total.Text = dttargetorder.Rows.Count.ToString();
                if (dttargetorder.Rows.Count > 0)
                {
                    lbl_Name.Text = dttargetorder.Rows[0]["User_Name"].ToString();
                }
                if (dttargetorder.Rows.Count > 0)
                {
                    grd_Targetorder.DataSource = null;
                    grd_Targetorder.AutoGenerateColumns = false;
                    grd_Targetorder.ColumnCount = 17;
                    grd_Targetorder.Columns[0].Name = "SNo";
                    grd_Targetorder.Columns[0].HeaderText = "S. No";
                    grd_Targetorder.Columns[0].Width = 65;
                    grd_Targetorder.Columns[2].Name = "Production Date";
                    grd_Targetorder.Columns[2].HeaderText = "PRODUCTION DATE";
                    grd_Targetorder.Columns[2].DataPropertyName = "Order_Production_Date";
                    grd_Targetorder.Columns[2].Width = 150;
                    grd_Targetorder.Columns[3].Name = "User_Name";
                    grd_Targetorder.Columns[3].HeaderText = "USER NAME";
                    grd_Targetorder.Columns[3].DataPropertyName = "User_Name";
                    grd_Targetorder.Columns[3].Width = 110;
                    grd_Targetorder.Columns[1].Name = "Order Number";
                    grd_Targetorder.Columns[1].HeaderText = "ORDER NUMBER";
                    grd_Targetorder.Columns[1].DataPropertyName = "Client_Order_Number";
                    grd_Targetorder.Columns[1].Width = 175;
                    grd_Targetorder.Columns[1].Visible = false;

                    DataGridViewLinkColumn link = new DataGridViewLinkColumn();
                    grd_Targetorder.Columns.Add(link);
                    link.Name = "Order Number";
                    link.HeaderText = "ORDER NUMBER";
                    link.DataPropertyName = "Client_Order_Number";
                    link.Width = 200;
                    link.DisplayIndex = 1;

                    if (User_Role_Id == "1")
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
                        grd_Targetorder.Columns[4].Name = "Client Name";
                        grd_Targetorder.Columns[4].HeaderText = "CLIENT NAME";
                        grd_Targetorder.Columns[4].DataPropertyName = "Client_Number";
                        grd_Targetorder.Columns[4].Width = 130;

                        grd_Targetorder.Columns[5].Name = "SubProcessName";
                        grd_Targetorder.Columns[5].HeaderText = "SUB PROCESS NAME";
                        grd_Targetorder.Columns[5].DataPropertyName = "Subprocess_Number";
                        grd_Targetorder.Columns[5].Width = 220;
                    }

                    grd_Targetorder.Columns[6].Name = "OrderType";
                    grd_Targetorder.Columns[6].HeaderText = "ORDER TYPE";
                    grd_Targetorder.Columns[6].DataPropertyName = "Order_Type";
                    grd_Targetorder.Columns[6].Width = 160;

                    grd_Targetorder.Columns[7].Name = "TargetCategory";
                    grd_Targetorder.Columns[7].HeaderText = "Target Category";
                    grd_Targetorder.Columns[7].DataPropertyName = "Order_Source_Type_Name";
                    grd_Targetorder.Columns[7].Width = 140;


                    grd_Targetorder.Columns[8].Name = "Order_Work_Type";
                    grd_Targetorder.Columns[8].HeaderText = "ORDER WORK TYPE";
                    grd_Targetorder.Columns[8].DataPropertyName = "Order_Work_Type";
                    grd_Targetorder.Columns[8].Width = 160;

                    grd_Targetorder.Columns[9].Name = "Task";
                    grd_Targetorder.Columns[9].HeaderText = "TASK";
                    grd_Targetorder.Columns[9].DataPropertyName = "Order_Status";
                    grd_Targetorder.Columns[9].Width = 120;

                    grd_Targetorder.Columns[10].Name = "Status";
                    grd_Targetorder.Columns[10].HeaderText = "PROGRESS STATUS";
                    grd_Targetorder.Columns[10].DataPropertyName = "Progress_Status";
                    grd_Targetorder.Columns[10].Width = 160;


                    grd_Targetorder.Columns[11].Name = "Order_Production_Date";
                    grd_Targetorder.Columns[11].HeaderText = "PRODUCTION DATE";
                    grd_Targetorder.Columns[11].DataPropertyName = "Order_Production_Date";
                    grd_Targetorder.Columns[11].Width = 160;

                    grd_Targetorder.Columns[12].Name = "StartTime";
                    grd_Targetorder.Columns[12].HeaderText = "START TIME";
                    grd_Targetorder.Columns[12].DataPropertyName = "Start_Time";
                    grd_Targetorder.Columns[12].Width = 120;

                    grd_Targetorder.Columns[13].Name = "EndTime";
                    grd_Targetorder.Columns[13].HeaderText = "END TIME";
                    grd_Targetorder.Columns[13].DataPropertyName = "End_Time";
                    grd_Targetorder.Columns[13].Width = 120;

                    grd_Targetorder.Columns[14].Name = "TotalTime";
                    grd_Targetorder.Columns[14].HeaderText = "TOTAL TIME";
                    grd_Targetorder.Columns[14].DataPropertyName = "Total_Time_hh_mm_ss";
                    grd_Targetorder.Columns[14].Width = 100;


                    grd_Targetorder.Columns[15].Name = "Order_User_Effeciency";
                    grd_Targetorder.Columns[15].HeaderText = "ORDER EFF";
                    grd_Targetorder.Columns[15].DataPropertyName = "Order_User_Effeciency";
                    grd_Targetorder.Columns[15].Width = 100;
                    grd_Targetorder.Columns[15].Visible = true;


                    grd_Targetorder.Columns[16].Name = "Order Id";
                    grd_Targetorder.Columns[16].HeaderText = "Order id";
                    grd_Targetorder.Columns[16].DataPropertyName = "Order_ID";
                    grd_Targetorder.Columns[16].Width = 100;
                    grd_Targetorder.Columns[16].Visible = false;

                    grd_Targetorder.DataSource = dttargetorder;
                }
                else
                {
                    grd_Targetorder.Visible = true;
                    //grd_Targetorder.Rows.Clear();
                    grd_Targetorder.DataSource = null;
                }

                for (int i = 0; i < grd_Targetorder.Rows.Count; i++)
                {
                    grd_Targetorder.Rows[i].Cells[0].Value = i + 1;
                }
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

        private void grd_Targetorder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 17 && User_Role_Id != "2")
                {


                    string Order_Id = grd_Targetorder.Rows[e.RowIndex].Cells[16].Value.ToString();
                    string Work_Type = grd_Targetorder.Rows[e.RowIndex].Cells[8].Value.ToString();

                    if (Work_Type == "Live")
                    {

                        Ordermanagement_01.Order_Entry Order_Entry = new Ordermanagement_01.Order_Entry(int.Parse(Order_Id.ToString()), Emp_User_Id, User_Role_Id.ToString(), "");
                        Order_Entry.Show();
                    }
                    else if (Work_Type == "Rework")
                    {
                        Ordermanagement_01.Rework_Superqc_Order_Entry orderentry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(Order_Id.ToString()), Emp_User_Id, "Rework", User_Role_Id.ToString(), "");
                        orderentry.Show();

                    }
                    else if (Work_Type == "Super Qc")
                    {
                        Ordermanagement_01.Rework_Superqc_Order_Entry orderentry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(Order_Id.ToString()), Emp_User_Id, "Superqc", User_Role_Id.ToString(), "");
                        orderentry.Show();

                    }



                }
            }
        }

        private void Emp_Production_Score_Board_Load(object sender, EventArgs e)
        {


            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);

            try
            {
                Bind_Grid_View_Total();
                Employee_New_Update_effeciency();
                Gridview_Employee_Production_Dataview();

                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {

                //Close Wait Form
                this.Enabled = true;
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                this.Enabled = true;
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }


        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_SearchOrdernumber.ForeColor = Color.Black;

                foreach (DataGridViewRow row in grd_Targetorder.Rows)
                {
                    if (txt_SearchOrdernumber.Text != "" && row.Cells[1].Value.ToString().StartsWith(txt_SearchOrdernumber.Text, true, CultureInfo.InvariantCulture) || row.Cells[1].Value.ToString().StartsWith(txt_SearchOrdernumber.Text, true, CultureInfo.InvariantCulture))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void Employee_New_Update_effeciency()
        {
            try
            {

                Hashtable htemp = new Hashtable();
                System.Data.DataTable dtemp = new System.Data.DataTable();


                grd_DashEmployee_efficency.Rows.Clear();
                grd_DashEmployee_efficency.Rows.Add();
                Hashtable htuser_Order_Details = new Hashtable();
                System.Data.DataTable dtOrder_Details = new System.Data.DataTable();

                Hashtable htget_Emp_Eff = new Hashtable();
                System.Data.DataTable dtget_Emp_Eff = new System.Data.DataTable();

                htget_Emp_Eff.Add("@Trans", "DAILY_USER_NEW_UPDATED_EFF");
                htget_Emp_Eff.Add("@User_Id", Emp_User_Id);
                DateTime Prd_Date = Convert.ToDateTime(Production_Date.ToString());
                string Prd_Date1 = Prd_Date.ToString("MM/dd/yyyy");
                htget_Emp_Eff.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Eff = dataaccess.ExecuteSP("Sp_Score_Board_Updated", htget_Emp_Eff);





                Hashtable htget_Emp_Prod_Idel_Time = new Hashtable();
                System.Data.DataTable dtget_Emp_Prod_Idel_Time = new System.Data.DataTable();

                htget_Emp_Prod_Idel_Time.Add("@Trans", "GET_BREAK_HOURS");
                htget_Emp_Prod_Idel_Time.Add("@User_Id", Emp_User_Id);
                htget_Emp_Prod_Idel_Time.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Prod_Idel_Time = dataaccess.ExecuteSP("Sp_Score_Board", htget_Emp_Prod_Idel_Time);

                if (dtget_Emp_Prod_Idel_Time.Rows.Count > 0)
                {

                    Break_Time = int.Parse(dtget_Emp_Prod_Idel_Time.Rows[0]["Total_Break_Time"].ToString());

                }
                else
                {
                    Break_Time = 0;

                }

                Hashtable htget_Emp_Prod_Idel_Time1 = new Hashtable();
                System.Data.DataTable dtget_Emp_Prod_Idel_Time1 = new System.Data.DataTable();

                htget_Emp_Prod_Idel_Time1.Add("@Trans", "GET_IDEAL_HOURS");
                htget_Emp_Prod_Idel_Time1.Add("@User_Id", Emp_User_Id);
                htget_Emp_Prod_Idel_Time1.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Prod_Idel_Time1 = dataaccess.ExecuteSP("Sp_Score_Board", htget_Emp_Prod_Idel_Time1);

                if (dtget_Emp_Prod_Idel_Time1.Rows.Count > 0)
                {

                    Ideal_Time = int.Parse(dtget_Emp_Prod_Idel_Time1.Rows[0]["Total_Ideal_Time"].ToString());

                }
                else
                {
                    Ideal_Time = 0;

                }

                Hashtable htget_Emp_Prod_Idel_Time2 = new Hashtable();
                System.Data.DataTable dtget_Emp_Prod_Idel_Time2 = new System.Data.DataTable();

                htget_Emp_Prod_Idel_Time2.Add("@Trans", "GET_PRODUCTION_HOURS");
                htget_Emp_Prod_Idel_Time2.Add("@User_Id", Emp_User_Id);
                htget_Emp_Prod_Idel_Time2.Add("@Production_Date", Prd_Date1);
                dtget_Emp_Prod_Idel_Time2 = dataaccess.ExecuteSP("Sp_Score_Board", htget_Emp_Prod_Idel_Time2);

                if (dtget_Emp_Prod_Idel_Time2.Rows.Count > 0)
                {

                    Production_Time = int.Parse(dtget_Emp_Prod_Idel_Time2.Rows[0]["Total_Production_Time"].ToString());

                }
                else
                {
                    Production_Time = 0;
                }
                if (dtget_Emp_Eff.Rows.Count > 0)
                {
                    grd_DashEmployee_efficency.Rows[0].Cells[0].Value = int.Parse(dtget_Emp_Eff.Rows[0]["Total_Orders"].ToString());
                    if (string.IsNullOrEmpty(efficiency))
                    {
                        grd_DashEmployee_efficency.Rows[0].Cells[6].Value = int.Parse(dtget_Emp_Eff.Rows[0]["Effecinecy"].ToString());
                    }
                    else
                    {
                        grd_DashEmployee_efficency.Rows[0].Cells[6].Value = efficiency;
                    }
                    grd_DashEmployee_efficency.Rows[0].Cells[7].Value = int.Parse(dtget_Emp_Eff.Rows[0]["User_Id"].ToString());
                    Allocated_In_Sec = int.Parse(dtget_Emp_Eff.Rows[0]["Allocated_In_Sec"].ToString());
                }
                else
                {
                    grd_DashEmployee_efficency.Rows[0].Cells[0].Value = "0";
                    grd_DashEmployee_efficency.Rows[0].Cells[6].Value = "0";
                    grd_DashEmployee_efficency.Rows[0].Cells[7].Value = Emp_User_Id;
                    Allocated_In_Sec = 0;
                }
                Total_Time = Production_Time + Break_Time + Ideal_Time;
                Total_t = TimeSpan.FromSeconds(Total_Time);
                string Total_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
             Total_t.Hours,
             Total_t.Minutes,
             Total_t.Seconds);
                grd_DashEmployee_efficency.Rows[0].Cells[1].Value = Total_formatedTime.ToString();

                prod_t = TimeSpan.FromSeconds(Production_Time);
                string Prd_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                       prod_t.Hours,
                       prod_t.Minutes,
                       prod_t.Seconds);
                grd_DashEmployee_efficency.Rows[0].Cells[2].Value = Prd_formatedTime.ToString();


                Ideal_t = TimeSpan.FromSeconds(Ideal_Time);
                string idl_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                   Ideal_t.Hours,
                   Ideal_t.Minutes,
                   Ideal_t.Seconds);
                grd_DashEmployee_efficency.Rows[0].Cells[4].Value = idl_formatedTime.ToString();




                Break_t = TimeSpan.FromSeconds(Break_Time);
                string brk_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                   Break_t.Hours,
                   Break_t.Minutes,
                   Break_t.Seconds);
                grd_DashEmployee_efficency.Rows[0].Cells[5].Value = brk_formatedTime.ToString();




                Allocte_t = TimeSpan.FromSeconds(Allocated_In_Sec);
                string Allocate_formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                   Allocte_t.Hours,
                   Allocte_t.Minutes,
                   Allocte_t.Seconds);
                grd_DashEmployee_efficency.Rows[0].Cells[3].Value = Allocate_formatedTime.ToString();


            }
            catch (Exception ex)
            {


            }




        }

        private void grd_DashEmployee_efficency_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 4)
                {
                    Ordermanagement_01.Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(Production_Date, Emp_User_Id, "Ideal");

                    emb.Show();

                }
                else if (e.ColumnIndex == 5)
                {
                    Ordermanagement_01.Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(Production_Date, Emp_User_Id, "Break");

                    emb.Show();


                }
                else if (e.ColumnIndex == 2)
                {
                    Ordermanagement_01.Employee.Employee_View_Break_Details emb = new Employee.Employee_View_Break_Details(Production_Date, Emp_User_Id, "Production");

                    emb.Show();


                }

            }
        }


    }
}
