using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Ordermanagement_01.Employee
{
    public partial class Production_Time_Update : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Production_Date;
        public Production_Time_Update(string PRODUCTION_DATE)
        {
            InitializeComponent();
            Production_Date = PRODUCTION_DATE;
        }
        private void Production_Time_Update_Load(object sender, EventArgs e)
        {
            Hide();
            Visible = false;
            ShowInTaskbar = false;
        }

        private void Production_Timer_Tick(object sender, EventArgs e)
        {
            Employees_New_Update_effeciency();
        }
        private void Employees_New_Update_effeciency()
        {
            Hashtable htemp = new Hashtable();
            DataTable dtemp = new DataTable();
            Hashtable htuser_Order_Details = new Hashtable();
            DataTable dtOrder_Details = new DataTable();
            htuser_Order_Details.Add("@Trans", "DAILY_ALL_USER_NEW_UPDATED_EFF");
            DateTime Prd_Date = Convert.ToDateTime(Production_Date.ToString());
            string Prd_Date1 = Prd_Date.ToString("MM/dd/yyyy");
            htuser_Order_Details.Add("@Production_Date", Prd_Date1);
            dtOrder_Details = dataaccess.ExecuteSP("Sp_Score_Board", htuser_Order_Details);
            Hashtable htget_Emp_Eff = new Hashtable();
            DataTable dtget_Emp_Eff = new DataTable();
            htget_Emp_Eff.Add("@Trans", "GET_ALL_USER_DAILY_UPDATED_EFF");
            dtget_Emp_Eff = dataaccess.ExecuteSP("Sp_Score_Board", htget_Emp_Eff);            
        }
    }
}
