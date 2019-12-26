using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;


namespace Ordermanagement_01.Employee
{
    public partial class State_Wise_Tax_Due_Date : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();

        int userid = 0, Task, Task_Confirm_Id;
        string State_Id, user_Role;
        public State_Wise_Tax_Due_Date(int user_id, string STATE_ID,string USER_ROLE)
        {
            InitializeComponent();
            State_Id = STATE_ID;
            this.Text = "U.S State Tax Office Due Dates";
            user_Role = USER_ROLE;
            
        }
        private void Bind_Tax()
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT");
            ht.Add("@State_Id", State_Id);
            dt = dataaccess.ExecuteSP("Sp_State_Tax_Due_Date", ht);
            if (dt.Rows.Count > 0)
            {

                //Grid_Tax.Rows.Clear();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{

                //    Grid_Tax.Rows[i].Cells[0].Value = dt.Rows[i]["Abbreviation"].ToString();
                //    Grid_Tax.Rows[i].Cells[1].Value = dt.Rows[i]["Tax_Year"].ToString();
                //    Grid_Tax.Rows[i].Cells[2].Value = dt.Rows[i]["Billing_Cycle"].ToString();
                //    Grid_Tax.Rows[i].Cells[3].Value = dt.Rows[i]["Due_Date1"].ToString();
                //    Grid_Tax.Rows[i].Cells[4].Value = dt.Rows[i]["Due_Date2"].ToString();
                //    Grid_Tax.Rows[i].Cells[5].Value = dt.Rows[i]["Due_Date3"].ToString();
                //    Grid_Tax.Rows[i].Cells[6].Value = dt.Rows[i]["Due_Date4"].ToString();

                //}

                Grid_Tax.DataSource = dt;






            }
            else
            {

                Grid_Tax.DataSource = null;

                Grid_Tax.Rows.Clear();
            }
        }
       

        private void State_Wise_Tax_Due_Date_Load(object sender, EventArgs e)
        {
            if (user_Role == "2")
            {

                this.ControlBox = false;
            }
            else 
            {

                this.ControlBox = true;
            }
            Bind_Tax();
        }

    }
}
