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
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01.Employee
{
    public partial class Employee_Order_Processing_Det : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string User_Role;
        int User_Id;
        int branch;
        decimal Order_Percentage;
        public Employee_Order_Processing_Det(int USER_ID, string USER_ROLE, int branch)
        {
            InitializeComponent();
            User_Id = USER_ID;
            User_Role = USER_ROLE;
            this.branch = branch;
            BindUserOrders();
        }
        private void BindUserOrders()
        {
            if (branch == 0)
            {
                Bind_User_Order_Details();
            }
            else
            {
                Bind_User_Order_Details(branch);
            }
        }
        private void Bind_User_Order_Details(int branch)
        {
            Hashtable htUser = new Hashtable();
            DataTable dtUser = new System.Data.DataTable();

            htUser.Add("@Trans", "GET_USER_WORKING_PROGRESS_ORDERS_DETAILS_BY_BRANCH");
            htUser.Add("@Branch_ID", branch);
            dtUser = dataaccess.ExecuteSP("Sp_Order_Assignment", htUser);
            GridView_General_Updates.Columns[9].Width = 50;
            if (dtUser.Rows.Count > 0)
            {
                GridView_General_Updates.Rows.Clear();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    GridView_General_Updates.AutoGenerateColumns = false;
                    GridView_General_Updates.Rows.Add();
                    GridView_General_Updates.Rows[i].Cells[0].Value = dtUser.Rows[i]["Branch_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[1].Value = dtUser.Rows[i]["User_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[2].Value = dtUser.Rows[i]["Emp_Job_Role"].ToString();
                    GridView_General_Updates.Rows[i].Cells[3].Value = dtUser.Rows[i]["Reporting_To_1"].ToString();
                    GridView_General_Updates.Rows[i].Cells[4].Value = dtUser.Rows[i]["Reporting_To_2"].ToString();
                    GridView_General_Updates.Rows[i].Cells[5].Value = dtUser.Rows[i]["Order_Number"].ToString();
                    if (User_Role == "1")
                    {
                        GridView_General_Updates.Rows[i].Cells[6].Value = dtUser.Rows[i]["Client_Name"].ToString();
                        GridView_General_Updates.Rows[i].Cells[7].Value = dtUser.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        GridView_General_Updates.Rows[i].Cells[6].Value = dtUser.Rows[i]["Client_Number"].ToString();
                        GridView_General_Updates.Rows[i].Cells[7].Value = dtUser.Rows[i]["Subprocess_Number"].ToString();
                    }
                    GridView_General_Updates.Rows[i].Cells[8].Value = dtUser.Rows[i]["Order_Type"].ToString();
                    GridView_General_Updates.Rows[i].Cells[9].Value = dtUser.Rows[i]["Date"].ToString();
                    GridView_General_Updates.Rows[i].Cells[10].Value = dtUser.Rows[i]["Abbreviation"].ToString();
                    GridView_General_Updates.Rows[i].Cells[11].Value = dtUser.Rows[i]["County"].ToString();
                    GridView_General_Updates.Rows[i].Cells[12].Value = dtUser.Rows[i]["Order_Task"].ToString();
                    GridView_General_Updates.Rows[i].Cells[13].Value = dtUser.Rows[i]["Start_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[16].Value = dtUser.Rows[i]["Order_Id"].ToString();
                    GridView_General_Updates.Rows[i].Cells[17].Value = dtUser.Rows[i]["User_id"].ToString();

                    int Total_Working_Time;
                    TimeSpan t;
                    Total_Working_Time = int.Parse(dtUser.Rows[i]["Total_Time"].ToString());
                    t = TimeSpan.FromSeconds(Total_Working_Time);
                    string formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                           t.Hours,
                           t.Minutes,
                           t.Seconds);
                    GridView_General_Updates.Rows[i].Cells[15].Value = formatedTime.ToString();
                    Order_Percentage = decimal.Parse(dtUser.Rows[i]["Order_Percentage"].ToString());
                    decimal Assign_Time_to_Minutes_Seconds = 0;
                    decimal allocated_Count = 0;
                    if (Order_Percentage != 0)
                    {
                        allocated_Count = 100 / Order_Percentage;
                        Assign_Time_to_Minutes_Seconds = (480 / allocated_Count) * 60;
                    }
                    else
                    {
                        Assign_Time_to_Minutes_Seconds = 0;
                    }
                    int Allocated_Time = decimal.ToInt32(Assign_Time_to_Minutes_Seconds);

                    TimeSpan t_a;
                    t_a = TimeSpan.FromSeconds(Allocated_Time);
                    string formatedTime1 = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                           t_a.Hours,
                           t_a.Minutes,
                           t_a.Seconds);
                    GridView_General_Updates.Rows[i].Cells[14].Value = formatedTime1.ToString();





                    if (Total_Working_Time > Allocated_Time)
                    {

                        GridView_General_Updates.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    }
                    else if (Allocated_Time != 0)
                    {


                        decimal three_Fourth_Of_Allocated_Time;

                        decimal dd = decimal.Parse(Allocated_Time.ToString());

                        decimal three_four = (decimal)0.75;

                        three_Fourth_Of_Allocated_Time = three_four * dd;

                        int Three_Fourth_Time = decimal.ToInt32(three_Fourth_Of_Allocated_Time);

                        if (Total_Working_Time >= Three_Fourth_Time && Total_Working_Time < Allocated_Time)
                        {
                            GridView_General_Updates.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;

                        }
                        else
                        {

                            //GridView_General_Updates.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;


                        }



                    }
                    else if (Allocated_Time == 0)
                    {



                    }







                }
            }
            else
            {

                GridView_General_Updates.Rows.Clear();
            }
        }
        private void Employee_Order_Processing_Det_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        public void Bind_User_Order_Details()
        {

            Hashtable htUser = new Hashtable();
            DataTable dtUser = new System.Data.DataTable();

            htUser.Add("@Trans", "GET_USER_WORKING_PROGRESS_ORDERS_DETAILS");

            dtUser = dataaccess.ExecuteSP("Sp_Order_Assignment", htUser);
            GridView_General_Updates.Columns[9].Width = 50;
            if (dtUser.Rows.Count > 0)
            {
                GridView_General_Updates.Rows.Clear();


                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    GridView_General_Updates.AutoGenerateColumns = false;

                    GridView_General_Updates.Rows.Add();
                    GridView_General_Updates.Rows[i].Cells[0].Value = dtUser.Rows[i]["Branch_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[1].Value = dtUser.Rows[i]["User_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[2].Value = dtUser.Rows[i]["Emp_Job_Role"].ToString();

                    GridView_General_Updates.Rows[i].Cells[3].Value = dtUser.Rows[i]["Reporting_To_1"].ToString();
                    GridView_General_Updates.Rows[i].Cells[4].Value = dtUser.Rows[i]["Reporting_To_2"].ToString();
                    GridView_General_Updates.Rows[i].Cells[5].Value = dtUser.Rows[i]["Order_Number"].ToString();
                    if (User_Role == "1")
                    {
                        GridView_General_Updates.Rows[i].Cells[6].Value = dtUser.Rows[i]["Client_Name"].ToString();
                        GridView_General_Updates.Rows[i].Cells[7].Value = dtUser.Rows[i]["Sub_ProcessName"].ToString();


                    }
                    else
                    {
                        GridView_General_Updates.Rows[i].Cells[6].Value = dtUser.Rows[i]["Client_Number"].ToString();
                        GridView_General_Updates.Rows[i].Cells[7].Value = dtUser.Rows[i]["Subprocess_Number"].ToString();

                    }

                    GridView_General_Updates.Rows[i].Cells[8].Value = dtUser.Rows[i]["Order_Type"].ToString();
                    GridView_General_Updates.Rows[i].Cells[9].Value = dtUser.Rows[i]["Date"].ToString();
                    GridView_General_Updates.Rows[i].Cells[10].Value = dtUser.Rows[i]["Abbreviation"].ToString();
                    GridView_General_Updates.Rows[i].Cells[11].Value = dtUser.Rows[i]["County"].ToString();
                    GridView_General_Updates.Rows[i].Cells[12].Value = dtUser.Rows[i]["Order_Task"].ToString();
                    GridView_General_Updates.Rows[i].Cells[13].Value = dtUser.Rows[i]["Start_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[16].Value = dtUser.Rows[i]["Order_Id"].ToString();
                    GridView_General_Updates.Rows[i].Cells[17].Value = dtUser.Rows[i]["User_id"].ToString();

                    int Total_Working_Time;
                    TimeSpan t;
                    Total_Working_Time = int.Parse(dtUser.Rows[i]["Total_Time"].ToString());

                    t = TimeSpan.FromSeconds(Total_Working_Time);



                    string formatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                           t.Hours,
                           t.Minutes,
                           t.Seconds);
                    GridView_General_Updates.Rows[i].Cells[15].Value = formatedTime.ToString();


                    Order_Percentage = decimal.Parse(dtUser.Rows[i]["Order_Percentage"].ToString());
                    decimal Assign_Time_to_Minutes_Seconds = 0;
                    decimal allocated_Count = 0;
                    if (Order_Percentage != 0)
                    {
                        allocated_Count = 100 / Order_Percentage;

                        Assign_Time_to_Minutes_Seconds = (480 / allocated_Count) * 60;
                        // Assign_Time_to_Minutes_Seconds = ((8 * 60) / Order_Percentage) * 60;
                    }
                    else
                    {

                        Assign_Time_to_Minutes_Seconds = 0;
                    }

                    int Allocated_Time = decimal.ToInt32(Assign_Time_to_Minutes_Seconds);

                    TimeSpan t_a;
                    t_a = TimeSpan.FromSeconds(Allocated_Time);



                    string formatedTime1 = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                           t_a.Hours,
                           t_a.Minutes,
                           t_a.Seconds);
                    GridView_General_Updates.Rows[i].Cells[14].Value = formatedTime1.ToString();





                    if (Total_Working_Time > Allocated_Time)
                    {

                        GridView_General_Updates.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    }
                    else if (Allocated_Time != 0)


                    {


                        decimal three_Fourth_Of_Allocated_Time;

                        decimal dd = decimal.Parse(Allocated_Time.ToString());

                        decimal three_four = (decimal)0.75;

                        three_Fourth_Of_Allocated_Time = three_four * dd;

                        int Three_Fourth_Time = decimal.ToInt32(three_Fourth_Of_Allocated_Time);

                        if (Total_Working_Time >= Three_Fourth_Time && Total_Working_Time < Allocated_Time)
                        {
                            GridView_General_Updates.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;

                        }
                        else
                        {

                            //GridView_General_Updates.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;


                        }

                    }
                    else if (Allocated_Time == 0)
                    {

                    }
                }
            }
            else
            {
                GridView_General_Updates.Rows.Clear();
            }
        }
        private void GridView_General_Updates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 4)
                {
                    Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(GridView_General_Updates.Rows[e.RowIndex].Cells[16].Value.ToString()), User_Id, User_Role, "");
                    OrderEntry.Show();
                }
            }
        }
        private void pictureBoxRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                BindUserOrders();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Something went wrong contact admin.");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
    }
}
