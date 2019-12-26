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
namespace Ordermanagement_01.Employee
{
    public partial class Employee_Order_Count_Details : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string View_Type;
        string User_Role;
        int User_Id;
        private readonly int branch;
        private string branchName;
        public Employee_Order_Count_Details(string VIEW_TYPE, int USER_ID, string USER_ROLE, int branch)
        {
            InitializeComponent();
            User_Id = USER_ID;
            User_Role = USER_ROLE;
            View_Type = VIEW_TYPE;
            this.branch = branch;
            if (branch > 0)
            {
                branchName = GetBranchName(branch);
            }
            else branchName = "";
            if (View_Type == "Total")
            {
                lbl_Header.Text = branchName+" ALL USERS";
            }
            if (View_Type == "Online")
            {
                lbl_Header.Text = branchName+" LIST OF ONLINE USERS";
            }
            else if (View_Type == "No_Orders")
            {
                lbl_Header.Text = branchName + " LIST OF USERS DO NOT HAVE ORDERS";
            }
            else if (View_Type == "Orders")
            {
                lbl_Header.Text = branchName + " LIST OF USERS HAVING ORDERS";
            }

            if (branch == 0)
            {
                Bind_User_Details();
            }
            else {
                Bind_User_Details(branch);
            }
        }

        private string GetBranchName(int branch)
        {
            var ht = new Hashtable();
            ht.Add("Trans", "BRANCH_NAME");
            ht.Add("@Branch_ID", branch);
            var dt = dataaccess.ExecuteSP("Sp_Order_Assignment", ht);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Branch_Name"].ToString();
            }
            return String.Empty;
        }

        private void Bind_User_Details(int branch)
        {
            Hashtable htUser = new Hashtable();
            DataTable dtUser = new System.Data.DataTable();
            htUser.Add("@Trans", "ORDER_ALLOCATED_DETAILS_USER_WISE");                

            if (View_Type == "Total")
            {
                htUser.Add("@Filter_Type", "GET_ALL_USERS_BY_BRANCH");                
            }
            if (View_Type == "Online")
            {
                htUser.Add("@Filter_Type", "GET_ONLINE_USERS_BY_BRANCH");
            }
            else if (View_Type == "No_Orders")
            {
                htUser.Add("@Filter_Type", "GET_NO_ORDERS_USERS_BY_BRANCH");
            }
            else if (View_Type == "Orders")
            {
                htUser.Add("@Filter_Type", "GET_ORDERS_USERS_BY_BRANCH");
            }
            htUser.Add("@Branch_ID", branch);
            dtUser = dataaccess.ExecuteSP("Sp_Order_Assignment", htUser);

            if (dtUser.Rows.Count > 0)
            {
                GridView_General_Updates.Rows.Clear();

                GridView_General_Updates.Columns[0].Width = 20;
                GridView_General_Updates.Columns[1].Width = 80;
                GridView_General_Updates.Columns[4].Width = 30;
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    GridView_General_Updates.AutoGenerateColumns = false;
                    GridView_General_Updates.Rows.Add();

                    GridView_General_Updates.Rows[i].Cells[1].Value = dtUser.Rows[i]["User_Status"].ToString();
                    GridView_General_Updates.Rows[i].Cells[2].Value = dtUser.Rows[i]["Branch_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[3].Value = dtUser.Rows[i]["User_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[4].Value = dtUser.Rows[i]["No_OF_Orders"].ToString();
                    GridView_General_Updates.Rows[i].Cells[6].Value = dtUser.Rows[i]["Process_Status"].ToString();
                    GridView_General_Updates.Rows[i].Cells[7].Value = dtUser.Rows[i]["Emp_Job_Role"].ToString();
                    GridView_General_Updates.Rows[i].Cells[8].Value = dtUser.Rows[i]["Shift_Type_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[9].Value = dtUser.Rows[i]["Reporting_To_1"].ToString();
                    GridView_General_Updates.Rows[i].Cells[10].Value = dtUser.Rows[i]["Reporting_To_2"].ToString();
                    GridView_General_Updates.Rows[i].Cells[11].Value = dtUser.Rows[i]["User_id"].ToString();

                    string On_Off = dtUser.Rows[i]["User_Status"].ToString();
                    if (On_Off == "Online")
                    {
                        Image image = Properties.Resources.Online;

                        GridView_General_Updates.Rows[i].Cells[0].Value = image;
                        GridView_General_Updates.Rows[i].Cells[0].ToolTipText = "Online";
                    }
                    else if (On_Off == "Offline")
                    {
                        Image image = Properties.Resources.Offline;

                        GridView_General_Updates.Rows[i].Cells[0].Value = image;
                        GridView_General_Updates.Rows[i].Cells[0].ToolTipText = "Offline";

                    }

                    string User_Process_Status = dtUser.Rows[i]["Process_Status"].ToString();

                    if (User_Process_Status == "WORK IN PROGRESS")
                    {

                        Image image = Properties.Resources.Progress_32;

                        GridView_General_Updates.Rows[i].Cells[5].Value = image;
                        GridView_General_Updates.Rows[i].Cells[5].ToolTipText = "WORK IN PROGRESS";
                    }
                    else if (User_Process_Status == "NOT WORKING")
                    {

                        Image image = Properties.Resources.Not_Processing;

                        GridView_General_Updates.Rows[i].Cells[5].Value = image;
                        GridView_General_Updates.Rows[i].Cells[5].ToolTipText = "NOT WORKING";
                    }

                }
            }
            else
            {

                GridView_General_Updates.Rows.Clear();
            }
        }

        public void Bind_User_Details()
        {

            Hashtable htUser = new Hashtable();
            DataTable dtUser = new System.Data.DataTable();

            htUser.Add("@Trans", "ORDER_ALLOCATED_DETAILS_USER_WISE");
            if (View_Type == "Total")
            {
                htUser.Add("@Filter_Type", "GET_ALL_USERS");
            }
            if (View_Type == "Online")
            {
                htUser.Add("@Filter_Type", "GET_ONLINE_USERS");
            }
            else if (View_Type == "No_Orders")
            {
                htUser.Add("@Filter_Type", "GET_NO_ORDERS_USERS");
            }
            else if (View_Type == "Orders")
            {
                htUser.Add("@Filter_Type", "GET_ORDERS_USERS");
            }
            dtUser = dataaccess.ExecuteSP("Sp_Order_Assignment", htUser);

            if (dtUser.Rows.Count > 0)
            {
                GridView_General_Updates.Rows.Clear();

                GridView_General_Updates.Columns[0].Width=20;
                GridView_General_Updates.Columns[1].Width = 80;
                GridView_General_Updates.Columns[4].Width = 30;
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    GridView_General_Updates.AutoGenerateColumns = false;
                    GridView_General_Updates.Rows.Add();
                   

                    GridView_General_Updates.Rows[i].Cells[1].Value = dtUser.Rows[i]["User_Status"].ToString();
                    GridView_General_Updates.Rows[i].Cells[2].Value = dtUser.Rows[i]["Branch_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[3].Value = dtUser.Rows[i]["User_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[4].Value = dtUser.Rows[i]["No_OF_Orders"].ToString();
                    GridView_General_Updates.Rows[i].Cells[6].Value = dtUser.Rows[i]["Process_Status"].ToString();
                    GridView_General_Updates.Rows[i].Cells[7].Value = dtUser.Rows[i]["Emp_Job_Role"].ToString();
                    GridView_General_Updates.Rows[i].Cells[8].Value = dtUser.Rows[i]["Shift_Type_Name"].ToString();
                    GridView_General_Updates.Rows[i].Cells[9].Value = dtUser.Rows[i]["Reporting_To_1"].ToString();
                    GridView_General_Updates.Rows[i].Cells[10].Value = dtUser.Rows[i]["Reporting_To_2"].ToString();
                    GridView_General_Updates.Rows[i].Cells[11].Value = dtUser.Rows[i]["User_id"].ToString();
                   

                    string On_Off = dtUser.Rows[i]["User_Status"].ToString();
                    if (On_Off == "Online")
                    {
                        Image image = Properties.Resources.Online;

                        GridView_General_Updates.Rows[i].Cells[0].Value = image;
                        GridView_General_Updates.Rows[i].Cells[0].ToolTipText = "Online";
                    }
                    else if (On_Off == "Offline")
                    {
                        Image image = Properties.Resources.Offline;

                        GridView_General_Updates.Rows[i].Cells[0].Value = image;
                        GridView_General_Updates.Rows[i].Cells[0].ToolTipText = "Offline";

                    }

                    string User_Process_Status = dtUser.Rows[i]["Process_Status"].ToString();

                    if (User_Process_Status == "WORK IN PROGRESS")
                    {

                        Image image = Properties.Resources.Progress_32;

                        GridView_General_Updates.Rows[i].Cells[5].Value = image;
                        GridView_General_Updates.Rows[i].Cells[5].ToolTipText = "WORK IN PROGRESS";
                    }
                    else if (User_Process_Status == "NOT WORKING")
                    {

                        Image image = Properties.Resources.Not_Processing;

                        GridView_General_Updates.Rows[i].Cells[5].Value = image;
                        GridView_General_Updates.Rows[i].Cells[5].ToolTipText = "NOT WORKING";
                    }

                }
            }
            else
            {

                GridView_General_Updates.Rows.Clear();
            }
        }

        private void Employee_Order_Count_Details_Load(object sender, EventArgs e)
        {

        }

        private void btn_Work_In_Progres_Order_Details_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee.Employee_Order_Processing_Det Employee_View = new Ordermanagement_01.Employee.Employee_Order_Processing_Det(User_Id, User_Role,branch);
            Employee_View.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GridView_General_Updates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_Header_Click(object sender, EventArgs e)
        {

        }

      
    }
}
