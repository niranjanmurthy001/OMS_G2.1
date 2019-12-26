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
    public partial class Employee_User_Order_Data_Update : Form
    {
        int Max_Time_Id, Work_Type;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

 
        public Employee_User_Order_Data_Update(int MAX_ID,int WORK_TYPE)
        {
            InitializeComponent();
            Max_Time_Id = MAX_ID;
            Work_Type = WORK_TYPE;
        }

        private void User_Order_Timer_Tick(object sender, EventArgs e)
        {

            if (Work_Type == 1)
            {

                Update_User_Order_Time_Info();
            }
            else if (Work_Type == 2)
            {
                Update_Rework_User_Order_Time_Info();

            }
            else if (Work_Type == 3)
            {

                Update_Super_Qc_User_Order_Time_Info();
            }

        }

        protected void Update_User_Order_Time_Info()
        {

         
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE_ON_TIME");
            htComments.Add("@Order_Time_Id", Max_Time_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htComments);

        }

        protected void Update_Rework_User_Order_Time_Info()
        {
          
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE_ON_TIME");
            htComments.Add("@Order_Time_Id", Max_Time_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_Rework_User_Wise_Time_Track", htComments);

        }


        protected void Update_Super_Qc_User_Order_Time_Info()
        {
          
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "UPDATE_ON_TIME");
            htComments.Add("@Order_Time_Id", Max_Time_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_Super_Qc_User_Wise_Time_Track", htComments);

        }

        private void Employee_User_Order_Data_Update_Load(object sender, EventArgs e)
        {
            this.Hide();

            this.Visible = false;

            this.ShowInTaskbar = false;
        }

    
    }
}
