using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace Ordermanagement_01.Employee
{
    public partial class Error_History : Form
    {
       
         Commonclass Comclass = new Commonclass();
         DataAccess dataaccess = new DataAccess();
         DropDownistBindClass dbc = new DropDownistBindClass();
         int Order_Id, Error_Info_Id;
        string Order_No;
        public Error_History(int ORDER_ID, int ERROR_INFO_ID, string ORDER_NUMBER)
        {
            InitializeComponent();

            Order_Id = ORDER_ID;
            Error_Info_Id = ERROR_INFO_ID;
            Order_No = ORDER_NUMBER;

            lbl_Header.Text = ""+Order_No+" - ERROR HISTORY";
            this.Text = "" + Order_No + " - ERROR HISTORY";
        }
        private void Bind_Error_History()
        {

            Hashtable htget = new Hashtable();
            DataTable dtget = new DataTable();

            htget.Add("@Trans", "SELECT");
           htget.Add("@Order_Id",Order_Id);
           htget.Add("@Error_Info_Id",Error_Info_Id);
           dtget = dataaccess.ExecuteSP("Sp_Error_Info_History", htget);

            if (dtget.Rows.Count > 0)
            {

                Grid_Error.Rows.Clear();
                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    Grid_Error.Rows.Add();
                    Grid_Error.Rows[i].Cells[0].Value = dtget.Rows[i]["Comments"].ToString();
                    Grid_Error.Rows[i].Cells[1].Value = dtget.Rows[i]["User_Name"].ToString();
                    Grid_Error.Rows[i].Cells[2].Value = dtget.Rows[i]["Entered_Date"].ToString();
                    Grid_Error.Rows[i].Cells[3].Value = dtget.Rows[i]["Error_History_Id"].ToString();
                }
            }
            else
            {

                Grid_Error.Rows.Clear();
            }
        }

        private void Error_History_Load(object sender, EventArgs e)
        {
            Bind_Error_History();
        }
    }
}
