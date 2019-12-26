using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
namespace Ordermanagement_01
{
    public partial class Order_Status : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DataSet ds = new DataSet();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Grid_Data gd = new Classes.Grid_Data();
        
        DataTable dt;
        DateTime Recived_Datetime,TAT_END_TIME;
        int No_Tat_days, No_Of_TAT_Hours;
        int User_ID,Check;
        public Order_Status(DataTable DT,int USER_ID)
        {
            InitializeComponent();
            User_ID = USER_ID;
            dt = DT;
        }

        

        private void Order_Status_Load(object sender, EventArgs e)
        {

            dbc.BindOrder_Priority(ddl_Order_Priority);
            ddl_Order_Priority.SelectedIndex = 0;
            ddl_Tat_Effect_From.SelectedIndex = 1;
            ddl_Tat_Effect_In_Day_Hour.SelectedIndex = 0;
            Get_Order_List();
           
        }

        private void Get_Order_List()
        {




            if (dt.Rows.Count > 0)
            {
                grid_Orders.Rows.Clear();
                grid_Orders.AutoGenerateColumns = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    grid_Orders.Rows.Add();
                    grid_Orders.Rows[i].Cells[0].Value = dt.Rows[i]["Order_Number"].ToString();
                    grid_Orders.Rows[i].Cells[1].Value = dt.Rows[i]["Order_Id"].ToString();





                }
            }
            else
            {

                grid_Orders.DataSource = null;

            }


        }

        private void ddl_Tat_Effect_In_Day_Hour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Tat_Effect_In_Day_Hour.SelectedIndex == 0)
            {
                lbl_Tat_Day_Hour.Text = "Order TAT Hour (in hours)";

             
            }
            else if (ddl_Tat_Effect_In_Day_Hour.SelectedIndex == 1)
            {
                lbl_Tat_Day_Hour.Text = "Order TAT Days (in Days)";
              
            }
            else if (ddl_Tat_Effect_In_Day_Hour.SelectedIndex == 2)
            {
                lbl_Tat_Day_Hour.Text = "Order TAT Minutes (in Minutes)";

            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Get_Order_List();
        }


        private bool Vlaidate()
        {

         
            if (txt_Order_Tat_Hour.Text == "")
            {

                txt_Order_Tat_Hour.Focus();
                MessageBox.Show("Please enter Order TAT");
                return false;
                
            }
            return true;
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Vlaidate() != false)
            {

                for (int i = 0; i < grid_Orders.Rows.Count; i++)
                {

                    //Updating Order Priority Of Orders
                    Hashtable ht = new Hashtable();
                    DataTable dt = new System.Data.DataTable();
                    ht.Add("@Trans", "UPDATE_ORDER_PRIORITY");
                    ht.Add("@Order_Priority",int.Parse(ddl_Order_Priority.SelectedValue.ToString()));
                    ht.Add("@Order_ID",grid_Orders.Rows[i].Cells[1].Value.ToString());
                    dt = dataaccess.ExecuteSP("Sp_Order", ht);

                    //Inserting and updating Order TAT 


                    if (ddl_Tat_Effect_From.SelectedIndex == 0)
                    {


                        Hashtable htgetrecived_Date = new Hashtable();
                        DataTable dt_recived_Date = new System.Data.DataTable();

                        htgetrecived_Date.Add("@Trans", "GET_RECIVED_DATE_TIME");
                        htgetrecived_Date.Add("@Order_ID", grid_Orders.Rows[i].Cells[1].Value.ToString());

                        dt_recived_Date = dataaccess.ExecuteSP("Sp_Order", htgetrecived_Date);
                        Recived_Datetime = Convert.ToDateTime(dt_recived_Date.Rows[0]["R_Time"].ToString());



                    }
                    else
                    {

                        Hashtable htdate = new Hashtable();
                        DataTable dtdate = new System.Data.DataTable();
                        htdate.Add("@Trans", "GET_CURRENT_DATE_TIME");
                        dtdate = dataaccess.ExecuteSP("Sp_Order_Wise_TAT", htdate);
                        if (dtdate.Rows.Count > 0)
                        {
                        

                        }



                        Recived_Datetime = Convert.ToDateTime(dtdate.Rows[0]["date"].ToString());

                    }
                    

                    if(ddl_Tat_Effect_In_Day_Hour.SelectedIndex==1)
                    {




                        No_Tat_days = int.Parse(txt_Order_Tat_Hour.Text);
                        TAT_END_TIME = Recived_Datetime.AddDays(No_Tat_days);


                    }
                    else if (ddl_Tat_Effect_In_Day_Hour.SelectedIndex == 2)
                    {




                        No_Tat_days = int.Parse(txt_Order_Tat_Hour.Text);
                        TAT_END_TIME = Recived_Datetime.AddMinutes(No_Tat_days);


                    }
                    else if(ddl_Tat_Effect_In_Day_Hour.SelectedIndex==0)
                    {
                    

                        No_Of_TAT_Hours=int.Parse(txt_Order_Tat_Hour.Text);

                        TAT_END_TIME = Recived_Datetime.AddHours(No_Of_TAT_Hours);

                    }



                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new System.Data.DataTable();

                    htcheck.Add("@Trans", "CHECK");
                    htcheck.Add("@Order_Id", grid_Orders.Rows[i].Cells[1].Value.ToString());
                    dtcheck = dataaccess.ExecuteSP("Sp_Order_Wise_TAT", htcheck);
                    if (dtcheck.Rows.Count > 0)
                    {

                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Check = 0;
                    }

                    if (Check == 0)
                    {
                        Hashtable ht1 = new Hashtable();
                        DataTable dt1 = new System.Data.DataTable();
                        ht1.Add("@Trans", "INSERT");
                        ht1.Add("@Order_Id", grid_Orders.Rows[i].Cells[1].Value.ToString());
                        ht1.Add("@Tat_Start_Date_Time", Recived_Datetime);
                        ht1.Add("@Tat_End_Date_Time", TAT_END_TIME);
                        ht1.Add("@Inserted_By", User_ID);
                        ht1.Add("@Status", "True");
                        dt1 = dataaccess.ExecuteSP("Sp_Order_Wise_TAT", ht1);
                    }
                    else if (Check > 0)
                    {

                        Hashtable ht1 = new Hashtable();
                        DataTable dt1 = new System.Data.DataTable();
                        ht1.Add("@Trans", "UPDATE");
                        ht1.Add("@Order_Id", grid_Orders.Rows[i].Cells[1].Value.ToString());
                        ht1.Add("@Tat_Start_Date_Time", Recived_Datetime);
                        ht1.Add("@Tat_End_Date_Time", TAT_END_TIME);
                        ht1.Add("@Modified_By", User_ID);
                        ht1.Add("@Status", "True");
                        dt1 = dataaccess.ExecuteSP("Sp_Order_Wise_TAT", ht1);

                    }

                   

                }

                MessageBox.Show("Record Submitted Successfully");

                this.Close();
            }



        }
    }
}
