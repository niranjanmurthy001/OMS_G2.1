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
    public partial class Super_Qc_Orders : Form
    {
        int User_Id;
        string User_Role_Id;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Super_Qc_Orders(int USER_ID, string USER_ROLE_ID)
        {
            InitializeComponent();

            User_Id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
        }
        protected void Get_Count_Of_Orders()
        {



            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();



           
           

            //search ac Work Orders Qc
            Hashtable htSearch_Qc_Work_Orders = new Hashtable();
            System.Data.DataTable dtSearch_Qc_Work_Orders = new System.Data.DataTable();
            if (User_Role_Id == "2")
            {
                htSearch_Qc_Work_Orders.Add("@Trans", "SUPER_QC_SEARCH_QC_ORDER_FOR_USER_COUNT");
            }
            else if (User_Role_Id == "1")
            {
                htSearch_Qc_Work_Orders.Add("@Trans", "SUPER_QC_SEARCH_QC_ORDER_FOR_ADMIN_COUNT");
            }
            htSearch_Qc_Work_Orders.Add("@User_Id", User_Id);

            dtSearch_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", htSearch_Qc_Work_Orders);
            if (dtSearch_Qc_Work_Orders.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Search_orders_Qc_Count.Text = "SEARCH QC " + "(" + dtSearch_Qc_Work_Orders.Rows[0]["count"].ToString() + ")";

            }
            else
            {

                //div_mail_work.Visible = false;
            }

           
            //Typing Work Orders Qc
            Hashtable htTyping_Qc_Work_Orders = new Hashtable();
            System.Data.DataTable dtTyping_Qc_Work_Orders = new System.Data.DataTable();
            if (User_Role_Id == "2")
            {
                htTyping_Qc_Work_Orders.Add("@Trans", "SUPER_QC_TYPING_QC_ORDER_FOR_USER_COUNT");
            }
            else if (User_Role_Id == "1")
            {
                htTyping_Qc_Work_Orders.Add("@Trans", "SUPER_QC_TYPING_QC_ORDER_FOR_ADMIN_COUNT");
            }
            htTyping_Qc_Work_Orders.Add("@User_Id", User_Id);

            dtTyping_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", htTyping_Qc_Work_Orders);
            if (dtTyping_Qc_Work_Orders.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Typing_Qc_Orders_Work_Count.Text = "TYPING QC " + "(" + dtTyping_Qc_Work_Orders.Rows[0]["count"].ToString() + ")";

            }
            else
            {

                //div_mail_work.Visible = false;
            }

            







            Hashtable htcompleted = new Hashtable();
            System.Data.DataTable dtcompleted = new System.Data.DataTable();

            htcompleted.Add("@Trans", "SUPER_QC_TOTAL_COMPLETED_ORDER_COUNT");

            htcompleted.Add("@User_Id", User_Id);

            dtcompleted = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", htcompleted);
            if (dtcompleted.Rows.Count > 0)
            {
                //div_Web_Work.Visible = true;
                lbl_COmpleted_Order_Count.Text = "COMPLETED  " + "(" + dtcompleted.Rows[0]["count"].ToString() + ")";

            }
            else
            {

                //  div_Web_Work.Visible = false;
            }







            Hashtable htsearchPending = new Hashtable();
            System.Data.DataTable dtsearchPending = new System.Data.DataTable();
            htsearchPending.Add("@Trans", "SUPER_QC_TOTAL_CLARIFICATION_ORDER_COUNT");
            htsearchPending.Add("@User_Id", User_Id);
            dtsearchPending = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", htsearchPending);
            if (dtsearchPending.Rows.Count > 0)
            {
                //div_Web_Work.Visible = true;
                if (User_Role_Id == "1")
                {
                    Lbl_Clarification_orders.Text = "CLARIFICATION " + "(" + dtsearchPending.Rows[0]["count"].ToString() + ")";
                }
                else if (User_Role_Id == "2")
                {
                    Lbl_Clarification_orders.Text = "CLARIFICATION " + "(" + "0" + ")";
                }

            }
            else
            {

                //  div_Web_Work.Visible = false;
            }

            Hashtable htsearchQcPending = new Hashtable();
            System.Data.DataTable dtsearchQcPending = new System.Data.DataTable();
            htsearchQcPending.Add("@Trans", "SUPER_QC_TOTAL_HOLD_ORDER_COUNT");
            htsearchQcPending.Add("@User_Id", User_Id);
            dtsearchQcPending = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", htsearchQcPending);
            if (dtsearchQcPending.Rows.Count > 0)
            {
                //div_Web_Work.Visible = true;
                if (User_Role_Id == "1")
                {
                    lbl_Hold.Text = "   HOLD      " + "(" + dtsearchQcPending.Rows[0]["count"].ToString() + ")";
                }
                else if (User_Role_Id == "2")
                {
                    lbl_Hold.Text = "   HOLD       " + "(" + "0" + ")";
                }


            }
            else
            {

                //  div_Web_Work.Visible = false;
            }
            Hashtable htTypingPending = new Hashtable();
            System.Data.DataTable dtTypingPending = new System.Data.DataTable();
            htTypingPending.Add("@Trans", "SUPER_QC_TOTAL_CANCELLED_ORDER_COUNT");
            htTypingPending.Add("@User_Id", User_Id);
            dtTypingPending = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", htTypingPending);
            if (dtTypingPending.Rows.Count > 0)
            {
                //div_Web_Work.Visible = true;
                if (User_Role_Id == "1")
                {
                    lbl_CANCELLED.Text = "CANCELLED " + "(" + dtTypingPending.Rows[0]["count"].ToString() + ")";
                }
                else if (User_Role_Id == "2")
                {
                    lbl_CANCELLED.Text = "CANCELLED " + "(" + "0" + ")";
                }
            }
            else
            {
                //  div_Web_Work.Visible = false;
            }




            // Get_No_Of_Orders_ToDashboard();
        }

        private void Super_Qc_Orders_Load(object sender, EventArgs e)
        {
            Get_Count_Of_Orders();

            if (User_Role_Id == "1")
            {
                Grp_Processing.Visible = true;
                grp_Pending.Visible = true;
                grp_Allocation.Visible = true;
                btn_reallocate.Visible = true;
            }
            else if (User_Role_Id == "2")
            {

                Grp_Processing.Visible = true;
                grp_Pending.Visible = false;
                grp_Allocation.Visible = false;
                btn_reallocate.Visible = false;
            }

             if (User_Id==17 || User_Id==22 || User_Id==74)
            {
               
                Grp_Processing.Visible = true;
                grp_Pending.Visible = true;


                grp_Allocation.Visible = true;
                btn_reallocate.Visible = true;

            }
        }

      

        private void lbl_Search_orders_Qc_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(3, "Search_Qc", User_Id, User_Role_Id, "SuperQc",3);
            Emp_view.Show();
        }

     

        private void lbl_Typing_Qc_Orders_Work_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(7, "Typing_Qc", User_Id, User_Role_Id, "SuperQc",3);
            Emp_view.Show();

        }

        private void lbl_Upload_Order_Work_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(12, "Upload_ORDERS_ALLOCATE", User_Id, User_Role_Id, "SuperQc",3);
            Emp_view.Show();
        }

        private void lbl_search_Qc_Allocate_Count_Click(object sender, EventArgs e)
        {

            Sper_Qc_Allocation scq = new Sper_Qc_Allocation("SEARCH_SUPER_QC_ORDER_ALLOCATE", 3, User_Id, 3,User_Role_Id);

            scq.Show();
        }

        private void lbl_Typing_Allocate_Qc_Count_Click(object sender, EventArgs e)
        {
            Sper_Qc_Allocation scq = new Sper_Qc_Allocation("TYPING_SUPER_QC_ORDER_ALLOCATE", 7, User_Id, 7, User_Role_Id);

            scq.Show();
        }

        private void btn_reallocate_Click(object sender, EventArgs e)
        {

            Super_Qc_Search_Order sc = new Super_Qc_Search_Order(User_Id,User_Role_Id);
            sc.Show();
        }

        private void Lbl_Clarification_orders_Click(object sender, EventArgs e)
        {
            Super_Qc_Order_View sv = new Super_Qc_Order_View(1, User_Id, User_Role_Id);
            sv.Show();
        }

        private void lbl_COmpleted_Order_Count_Click(object sender, EventArgs e)
        {
            Super_Qc_Order_View sv = new Super_Qc_Order_View(3, User_Id, User_Role_Id);
            sv.Show();
        }

        private void lbl_Hold_Click(object sender, EventArgs e)
        {
            Super_Qc_Order_View sv = new Super_Qc_Order_View(5, User_Id, User_Role_Id);
            sv.Show();
        }

        private void lbl_CANCELLED_Click(object sender, EventArgs e)
        {
            Super_Qc_Order_View sv = new Super_Qc_Order_View(4, User_Id, User_Role_Id);
            sv.Show();

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();   
            Get_Count_Of_Orders();
            
        }

        private void grp_Allocation_Enter(object sender, EventArgs e)
        {


        }
    }
}
