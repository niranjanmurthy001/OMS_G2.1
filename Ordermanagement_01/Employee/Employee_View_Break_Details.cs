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
    public partial class Employee_View_Break_Details : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        string First_date, Secod_Date;
        int User_Id;
        string View_Type;
        string Production_Date;
        public Employee_View_Break_Details(string PRODUCTION_DATE, int USER_ID, string VIEW_TYPE)
        {
            InitializeComponent();
            Production_Date = PRODUCTION_DATE;
            User_Id = USER_ID;
            View_Type = VIEW_TYPE;

            if (View_Type == "Break")
            {
                this.Text = "Break Details";
            }
            else if (View_Type == "Ideal")
            {
                this.Text = "Ideal Timings Details";
            }
            else if (View_Type == "Production")
            {
                this.Text = "Production Timings Details";
            }
        }

        public void Bind_Break_Details()
        {
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            htComments.Add("@Trans", "GET_BREAK_DETAILS_UPDTAED");
            htComments.Add("@Production_Date", Production_Date);
            htComments.Add("@User_Id", User_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_User_Break_Details", htComments);

            if (dtComments.Rows.Count > 0)
            {
                GridView_General_Updates.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    GridView_General_Updates.AutoGenerateColumns = false;
                    GridView_General_Updates.Rows.Add();
                    GridView_General_Updates.Rows[i].Cells[0].Value = dtComments.Rows[i]["Order_Break_Id"].ToString();
                    GridView_General_Updates.Rows[i].Cells[1].Value = dtComments.Rows[i]["Break_Mode"].ToString();
                    GridView_General_Updates.Rows[i].Cells[2].Value = dtComments.Rows[i]["Start_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[3].Value = dtComments.Rows[i]["End_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[4].Value = dtComments.Rows[i]["Total_Time"].ToString();
                }
            }
            else
            {
                GridView_General_Updates.Rows.Clear();
            }
        }


        public void Bind_Ideal_Time_Details()
        {
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            htComments.Add("@Trans", "GET_TOTAL_IDEL_TIME_UPDATED");
            htComments.Add("@Production_Date", Production_Date);
            htComments.Add("@User_Id", User_Id);
            dtComments = dataaccess.ExecuteSP("Sp_User_Order_Ideal_Timings", htComments);

            if (dtComments.Rows.Count > 0)
            {
                GridView_General_Updates.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    GridView_General_Updates.AutoGenerateColumns = false;
                    GridView_General_Updates.Rows.Add();
                    GridView_General_Updates.Rows[i].Cells[0].Value = dtComments.Rows[i]["Idle_Type"].ToString();
                    GridView_General_Updates.Rows[i].Cells[1].Value = dtComments.Rows[i]["Reason"].ToString(); 
                    GridView_General_Updates.Rows[i].Cells[2].Value = dtComments.Rows[i]["Start_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[3].Value = dtComments.Rows[i]["End_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[4].Value = dtComments.Rows[i]["Total_Time"].ToString();
                }
                GridView_General_Updates.Columns[0].Visible = true;
                GridView_General_Updates.Columns[0].HeaderText = "Idle Type";
                GridView_General_Updates.Columns[1].HeaderText = "Reason";
            }
            else
            {
                GridView_General_Updates.Rows.Clear();
            }
        }

        public void Bind_Production_Time_Details()
        {
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            htComments.Add("@Trans", "SELECT");
            htComments.Add("@Sel_Production_Date", Production_Date);
            htComments.Add("@User_Id", User_Id);
            dtComments = dataaccess.ExecuteSP("Sp_User_Production_Timing", htComments);

            if (dtComments.Rows.Count > 0)
            {
                GridView_General_Updates.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    GridView_General_Updates.AutoGenerateColumns = false;
                    GridView_General_Updates.Rows.Add();
                    GridView_General_Updates.Rows[i].Cells[0].Value = dtComments.Rows[i]["Production_Time_Id"].ToString();
                    GridView_General_Updates.Rows[i].Cells[1].Value = dtComments.Rows[i]["User_Id"].ToString();
                    GridView_General_Updates.Rows[i].Cells[2].Value = dtComments.Rows[i]["Start_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[3].Value = dtComments.Rows[i]["End_Time"].ToString();
                    GridView_General_Updates.Rows[i].Cells[4].Value = dtComments.Rows[i]["Total_Time"].ToString();


                }

                GridView_General_Updates.Columns[0].Visible = false;
                GridView_General_Updates.Columns[1].Visible = false;
            }
            else
            {
                GridView_General_Updates.Rows.Clear();
            }
        }

        private void Employee_View_Break_Details_Load(object sender, EventArgs e)
        {
            if (View_Type == "Break")
            {
                lbl_Header.Text = "BREAK DETAILS";
                Bind_Break_Details();
                this.Name = lbl_Header.Text;
            }
            else if (View_Type == "Ideal")
            {

                lbl_Header.Text = "IDEAL TIMING DETAILS";
                Bind_Ideal_Time_Details();
                this.Name = lbl_Header.Text;
            }
            else if (View_Type == "Production")
            {
                lbl_Header.Text = "PRODUCTION TIMING DETAILS";
                Bind_Production_Time_Details();
                this.Name = lbl_Header.Text;
            }
        }
    }
}
