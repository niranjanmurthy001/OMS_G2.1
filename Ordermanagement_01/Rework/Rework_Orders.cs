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
    public partial class Rework_Orders : Form
    {
        int User_Id;
        string User_Role_Id;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        public Rework_Orders(int USER_ID, string USER_ROLE_ID)
        {
            InitializeComponent();
            User_Id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
        }

        private void Rework_Orders_Load(object sender, EventArgs e)
        {
            Get_Count_Of_Orders();
            //if (User_Role_Id != "2")
            //{

            //    btn_reallocate.Visible = true;
            //    groupBox1.Visible = true;
            //    groupBox2.Visible = true;

            

            //}
            //else
            //{


            //    btn_reallocate.Visible = false;
            //    groupBox1.Visible = false;
            //    groupBox2.Visible = false;
            //}

            ////  this is for Team lead and supervisiors
            //if (User_Id == 22 || User_Id == 17 || User_Id==74)
            //{
            //    btn_reallocate.Visible = true;
            //    groupBox1.Visible = true;
            //    groupBox2.Visible = true;


            //}
        }

        protected void Get_Count_Of_Orders()
        {
           

            //Processing Count


            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();



            Hashtable htSearch = new Hashtable();
            System.Data.DataTable dtsearch = new System.Data.DataTable();
           
            if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                htSearch.Add("@Trans", "REWORK_SEARCH_ORDER_FOR_ADMIN");
            }
            else
            {
                htSearch.Add("@Trans", "REWORK_SEARCH_ORDER_FOR_USER");

            }
            htSearch.Add("@User_Id", User_Id);

            dtsearch = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htSearch);
            if (dtsearch.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Search_Work_Orders_Count.Text = "SEARCH     " + "(" + dtsearch.Rows[0]["count"].ToString() + ")";

            
            }
            else
            {

                //div_mail_work.Visible = false;
            }

            //Typing Work Orders Qc
            Hashtable htSearch_Qc_Work_Orders = new Hashtable();
            System.Data.DataTable dtSearch_Qc_Work_Orders = new System.Data.DataTable();
          

            if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                htSearch_Qc_Work_Orders.Add("@Trans", "REWORK_SEARCH_QC_ORDER_FOR_ADMIN");
            }
            else
            {
                htSearch_Qc_Work_Orders.Add("@Trans", "REWORK_SEARCH_QC_ORDER_FOR_USER");

            }
            htSearch_Qc_Work_Orders.Add("@User_Id", User_Id);

            dtSearch_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htSearch_Qc_Work_Orders);
            if (dtSearch_Qc_Work_Orders.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Search_orders_Qc_Count.Text = "SEARCH QC " + "(" + dtSearch_Qc_Work_Orders.Rows[0]["count"].ToString() + ")";
                
            }
            else
            {

                //div_mail_work.Visible = false;
            }

            Hashtable htTyping = new Hashtable();
            System.Data.DataTable dtTyping = new System.Data.DataTable();
            

            if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                htTyping.Add("@Trans", "REWORK_TYPING_ORDER_FOR_ADMIN");
            }
            else
            {
                htTyping.Add("@Trans", "REWORK_TYPING_ORDER_FOR_USER");

            }
            htTyping.Add("@User_Id", User_Id);

            dtTyping = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htTyping);
            if (dtTyping.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Typing_work_Count.Text = "TYPING     " + "(" + dtTyping.Rows[0]["count"].ToString() + ")";

            }
            else
            {

                //div_mail_work.Visible = false;
            }

            //Typing Work Orders Qc
            Hashtable htTyping_Qc_Work_Orders = new Hashtable();
            System.Data.DataTable dtTyping_Qc_Work_Orders = new System.Data.DataTable();
            

            if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                htTyping_Qc_Work_Orders.Add("@Trans", "REWORK_TYPING_QC_ORDER_FOR_ADMIN");
            }
            else
            {
                htTyping_Qc_Work_Orders.Add("@Trans", "REWORK_TYPING_QC_ORDER_FOR_USER");

            }
            htTyping_Qc_Work_Orders.Add("@User_Id", User_Id);
           
            dtTyping_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htTyping_Qc_Work_Orders);
            if (dtTyping_Qc_Work_Orders.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Typing_Qc_Orders_Work_Count.Text = "TYPING QC " + "(" + dtTyping_Qc_Work_Orders.Rows[0]["count"].ToString() + ")";

            }
            else
            {

                //div_mail_work.Visible = false;
            }


            //Final Work Orders Qc
            Hashtable htFinal_Qc_Work_Orders = new Hashtable();
            System.Data.DataTable dtFinal_Qc_Work_Orders = new System.Data.DataTable();
           

            if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                htFinal_Qc_Work_Orders.Add("@Trans", "REWORK_FINAL_QC_ORDER_FOR_ADMIN");
            }
            else
            {
                htFinal_Qc_Work_Orders.Add("@Trans", "REWORK_FINAL_QC_ORDER_FOR_USER");

            }
            htFinal_Qc_Work_Orders.Add("@User_Id", User_Id);

            dtFinal_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htFinal_Qc_Work_Orders);
            if (dtFinal_Qc_Work_Orders.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_FInal_Qc_Order_Work_Count.Text = "FINAL QC " + "(" + dtFinal_Qc_Work_Orders.Rows[0]["count"].ToString() + ")";
                
            }
            else
            {

                //div_mail_work.Visible = false;
            }


            //Exception Work Orders Qc
            Hashtable htException_Work_Orders = new Hashtable();
            System.Data.DataTable dtException_Work_Orders = new System.Data.DataTable();
            

            if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                htException_Work_Orders.Add("@Trans", "REWORK_EXCEPTION_ORDER_FOR_ADMIN");
            }
            else
            {
                htException_Work_Orders.Add("@Trans", "REWORK_EXCEPTION_ORDER_FOR_USER");

            }
            htException_Work_Orders.Add("@User_Id", User_Id);

            dtException_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htException_Work_Orders);
            if (dtException_Work_Orders.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Exception_Order_Work_Count.Text = "EXCEPTION " + "(" + dtException_Work_Orders.Rows[0]["count"].ToString() + ")";
                
            }
            else
            {

                //div_mail_work.Visible = false;
            }


            Hashtable htUpload_Work_Orders = new Hashtable();
            System.Data.DataTable dtUpload_Work_Orders = new System.Data.DataTable();
        

            if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
            {
                htUpload_Work_Orders.Add("@Trans", "REWORK_UPLOAD_ORDER_FOR_ADMIN");
            }
            else
            {
                htUpload_Work_Orders.Add("@Trans", "REWORK_UPLOAD_ORDER_FOR_USER");

            }
            htUpload_Work_Orders.Add("@User_Id", User_Id);

            dtUpload_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htUpload_Work_Orders);
            if (dtUpload_Work_Orders.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;
                lbl_Upload_Order_Work_Count.Text = "UPLOAD " + "(" + dtUpload_Work_Orders.Rows[0]["count"].ToString() + ")";
                
            }
            else
            {

                //div_mail_work.Visible = false;
            }



            //Pending Count



            Hashtable htcompleted = new Hashtable();
            System.Data.DataTable dtcompleted = new System.Data.DataTable();

            htcompleted.Add("@Trans", "REWORK_COMPLETED_ORDER_COUNT");

            htcompleted.Add("@User_Id", User_Id);
           
            dtcompleted = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htcompleted);
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
            htsearchPending.Add("@Trans", "REWORK_ORDER_ALLOCATE_CLARIFICATION");
            htsearchPending.Add("@User_Id", User_Id);
            dtsearchPending = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htsearchPending);
            if (dtsearchPending.Rows.Count > 0)
            {
                //div_Web_Work.Visible = true;
                if (User_Role_Id == "1" || User_Role_Id=="6" || User_Role_Id=="4")
                {
                    Lbl_Clarification_orders.Text = "CLARIFICATION " + "(" + dtsearchPending.Rows[0]["count"].ToString() + ")";
                }
                else 
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
            htsearchQcPending.Add("@Trans", "REWORK_ORDER_ALLOCATE_HOLD");
            htsearchQcPending.Add("@User_Id", User_Id);
            dtsearchQcPending = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htsearchQcPending);
            if (dtsearchQcPending.Rows.Count > 0)
            {
                //div_Web_Work.Visible = true;
                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {
                    lbl_Hold.Text = "   HOLD      " + "(" + dtsearchQcPending.Rows[0]["count"].ToString() + ")";
                }
                else 
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
            htTypingPending.Add("@Trans", "REWORK_ORDER_ALLOCATE_CANCELLED");
            htTypingPending.Add("@User_Id", User_Id);
            dtTypingPending = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htTypingPending);
            if (dtTypingPending.Rows.Count > 0)
            {
                //div_Web_Work.Visible = true;
                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {
                    lbl_CANCELLED.Text = "CANCELLED " + "(" + dtTypingPending.Rows[0]["count"].ToString() + ")";
                }
                else
                {
                    lbl_CANCELLED.Text = "CANCELLED " + "(" + "0" + ")";
                }
            }
            else
            {
                //  div_Web_Work.Visible = false;
            }





            //ALlocation Count



            Hashtable htSearchalloc = new Hashtable();
            System.Data.DataTable dtsearchalloc = new System.Data.DataTable();

            htSearchalloc.Add("@Trans", "REWORK_SEARCH_ALLOCATE_COUNT");
            dtsearchalloc = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htSearchalloc);
            if (dtsearchalloc.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;


                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {
                    lb_Search_Order_ALlocate_count.Text = "SEARCH     " + "(" + dtsearchalloc.Rows[0]["count"].ToString() + ")";
                }
                else 
                {
                    lb_Search_Order_ALlocate_count.Text = "SEARCH     " + "(" + "0" + ")";
                }
            }
            else
            {

                //div_mail_work.Visible = false;
            }

            //Typing Work Orders Qc
            Hashtable htSearch_Qc_Work_Ordersalloc = new Hashtable();
            System.Data.DataTable dtSearch_Qc_Work_Ordersalloc = new System.Data.DataTable();

            htSearch_Qc_Work_Ordersalloc.Add("@Trans", "REWORK_SEARCH_QC_ALLOCATE_COUNT");
           
        

            dtSearch_Qc_Work_Ordersalloc = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htSearch_Qc_Work_Ordersalloc);
            if (dtSearch_Qc_Work_Ordersalloc.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;


                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {
                    lbl_search_Qc_Allocate_Count.Text = "SEARCH QC     " + "(" + dtSearch_Qc_Work_Ordersalloc.Rows[0]["count"].ToString() + ")";
                }
                else
                {
                    lbl_search_Qc_Allocate_Count.Text = "SEARCH QC     " + "(" + "0" + ")";
                }
                
            }
            else
            {

                //div_mail_work.Visible = false;
            }

            Hashtable htTypingalloc = new Hashtable();
            System.Data.DataTable dtTypingalloc = new System.Data.DataTable();

            htTypingalloc.Add("@Trans", "REWORK_TYPING_ALLOCATE_COUNT");
            
            htTypingalloc.Add("@User_Id", User_Id);

            dtTypingalloc = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htTypingalloc);
            if (dtTypingalloc.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;

                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {
                    lbl_typing_Allocate_Count.Text = "TYPING     " + "(" + dtTypingalloc.Rows[0]["count"].ToString() + ")";
                }
                else 
                {
                    lbl_typing_Allocate_Count.Text = "TYPING     " + "(" + "0" + ")";
                }
            }
            else
            {

                //div_mail_work.Visible = false;
            }

            //Typing Work Orders Qc
            Hashtable htTyping_Qc_Work_Ordersalloc = new Hashtable();
            System.Data.DataTable dtTyping_Qc_Work_Ordersalloc = new System.Data.DataTable();

            htTyping_Qc_Work_Ordersalloc.Add("@Trans", "REWORK_TYPING_QC_ALLOCATE_COUNT");
         
            htTyping_Qc_Work_Ordersalloc.Add("@User_Id", User_Id);

            dtTyping_Qc_Work_Ordersalloc = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htTyping_Qc_Work_Ordersalloc);
            if (dtTyping_Qc_Work_Ordersalloc.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;




                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {
                    lbl_Typing_Allocate_Qc_Count.Text = "TYPING QC " + "(" + dtTyping_Qc_Work_Ordersalloc.Rows[0]["count"].ToString() + ")";
                }
                else 
                {
                    lbl_Typing_Allocate_Qc_Count.Text = "TYPING QC     " + "(" + "0" + ")";
                }
            }
            else
            {

                //div_mail_work.Visible = false;
            }


            //Final_Qc Work Orders Qc
            Hashtable htFinal_Qc_Work_Ordersalloc = new Hashtable();
            System.Data.DataTable dtFinal_Qc_Work_Ordersalloc = new System.Data.DataTable();

            htFinal_Qc_Work_Ordersalloc.Add("@Trans", "REWORK_FINAL_QC_ALLOCATE_COUNT");

            htFinal_Qc_Work_Ordersalloc.Add("@User_Id", User_Id);

            dtFinal_Qc_Work_Ordersalloc = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htFinal_Qc_Work_Ordersalloc);
            if (dtFinal_Qc_Work_Ordersalloc.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;




                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {

                    lbl_Final_Qc_Orders_Allocate_Count.Text = "FINAL QC " + "(" + dtFinal_Qc_Work_Ordersalloc.Rows[0]["count"].ToString() + ")";
                }
                else 
                {
                    lbl_Final_Qc_Orders_Allocate_Count.Text = "FINAL QC     " + "(" + "0" + ")";
                }
            }
            else
            {

                //div_mail_work.Visible = false;
            }


            //Exception Work Orders Qc
            Hashtable htException_Work_Ordersalloc = new Hashtable();
            System.Data.DataTable dtException_Work_Ordersalloc = new System.Data.DataTable();

            htException_Work_Ordersalloc.Add("@Trans", "REWORK_EXCEPTION_ALLOCATE_COUNT");

            htException_Work_Ordersalloc.Add("@User_Id", User_Id);

            dtException_Work_Ordersalloc = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htException_Work_Ordersalloc);
            if (dtException_Work_Ordersalloc.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;




                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {

                    lbl_Exception_Orders_Allocate_Count.Text = "EXCEPTION " + "(" + dtException_Work_Ordersalloc.Rows[0]["count"].ToString() + ")";
                }
                else
                {
                    lbl_Exception_Orders_Allocate_Count.Text = "EXCEPTION   " + "(" + "0" + ")";
                }
            }
            else
            {

                //div_mail_work.Visible = false;
            }
            Hashtable htUpload_Work_Ordersalloc = new Hashtable();
            System.Data.DataTable dtUpload_Work_Ordersalloc = new System.Data.DataTable();

            htUpload_Work_Ordersalloc.Add("@Trans", "REWORK_UPLOAD_ALLOCATE_COUNT");
            
            htUpload_Work_Ordersalloc.Add("@User_Id", User_Id);

            dtUpload_Work_Ordersalloc = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htUpload_Work_Ordersalloc);
            if (dtUpload_Work_Ordersalloc.Rows.Count > 0)
            {
                //div_mail_work.Visible = true;

                if (User_Role_Id == "1" || User_Role_Id == "6" || User_Role_Id == "4")
                {
                    lbl_Upload_Orders_Allocate_Count.Text = "UPLOAD " + "(" + dtUpload_Work_Ordersalloc.Rows[0]["count"].ToString() + ")";

                }
                else
                {


                    lbl_Upload_Orders_Allocate_Count.Text = "UPLOAD     " + "(" + "0" + ")";
                }
            }
            else
            {

                //div_mail_work.Visible = false;
            }


          
          
       

            // Get_No_Of_Orders_ToDashboard();
        }

        private void lbl_Search_Work_Orders_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(2, "Search", User_Id, User_Role_Id,"Rework",2);
            Emp_view.Show();
        }

        private void lbl_Search_orders_Qc_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(3, "Search_Qc", User_Id, User_Role_Id, "Rework",2);
            Emp_view.Show();
        }

        private void lbl_Typing_work_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(4, "Type", User_Id, User_Role_Id, "Rework",2);
            Emp_view.Show();
        }

        private void lbl_Typing_Qc_Orders_Work_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(7, "Typing_Qc", User_Id, User_Role_Id, "Rework",2);
            Emp_view.Show();

        }

        private void lbl_Upload_Order_Work_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(12, "Upload_ORDERS_ALLOCATE", User_Id, User_Role_Id, "Rework",2);
            Emp_view.Show();
        }

        private void Lbl_Clarification_orders_Click(object sender, EventArgs e)
        {
            Rework_Order_View rw = new Rework_Order_View(1, User_Id, User_Role_Id);
            rw.Show();

        }

        private void lb_Search_Order_ALlocate_count_Click(object sender, EventArgs e)
        {

            Rework_Order_Allocate rv = new Rework_Order_Allocate("REWORK_SEARCH_ORDER_ALLOCATE", 2, User_Id, 2, User_Role_Id);
            rv.Show();

        }

        private void lbl_search_Qc_Allocate_Count_Click(object sender, EventArgs e)
        {
            Rework_Order_Allocate rv = new Rework_Order_Allocate("REWORK_SEARCH_QC_ORDER_ALLOCATE", 3, User_Id, 3, User_Role_Id);
            rv.Show();

        }

        private void lbl_typing_Allocate_Count_Click(object sender, EventArgs e)
        {
            Rework_Order_Allocate rv = new Rework_Order_Allocate("REWORK_TYPING_ORDER_ALLOCATE", 4, User_Id, 4, User_Role_Id);
            rv.Show();

        }

        private void lbl_Typing_Allocate_Qc_Count_Click(object sender, EventArgs e)
        {
            Rework_Order_Allocate rv = new Rework_Order_Allocate("REWORK_TYPING_QC_ORDERS_ALLOCATE", 2, User_Id, 7, User_Role_Id);
            rv.Show();
        }

        private void lbl_Upload_Orders_Allocate_Count_Click(object sender, EventArgs e)
        {
            Rework_Order_Allocate rv = new Rework_Order_Allocate("REWORK_UPLOAD_ORDERS_ALLOCATE", 2, User_Id, 12, User_Role_Id);
            rv.Show();
        }


        private void WorkerThreadFunc()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(WorkerThreadFunc), null);
                return;
            }
            IntPtr handle = new Form().Handle;
        }

        private void btn_reallocate_Click(object sender, EventArgs e)
        {
            Rework_Search_Order rs = new Rework_Search_Order(User_Id, User_Role_Id,"");
            rs.Show();
        }

        private void lbl_Hold_Click(object sender, EventArgs e)
        {
            Rework_Order_View rw = new Rework_Order_View(5, User_Id, User_Role_Id);
            rw.Show();
        }

        private void lbl_CANCELLED_Click(object sender, EventArgs e)
        {
            Rework_Order_View rw = new Rework_Order_View(4, User_Id, User_Role_Id);
            rw.Show();
        }

        private void lbl_COmpleted_Order_Count_Click(object sender, EventArgs e)
        {
            Rework_Order_View rw = new Rework_Order_View(3, User_Id, User_Role_Id);
            rw.Show();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Get_Count_Of_Orders();
        }

        private void lbl_Exception_Orders_Allocate_Count_Click(object sender, EventArgs e)
        {
            Rework_Order_Allocate rv = new Rework_Order_Allocate("REWORK_EXCEPTION_ORDERS_ALLOCATE", 2, User_Id, 24, User_Role_Id);
            rv.Show();
        }

        private void lbl_Final_Qc_Orders_Allocate_Count_Click(object sender, EventArgs e)
        {
            Rework_Order_Allocate rv = new Rework_Order_Allocate("REWORK_FINAL_QC_ORDERS_ALLOCATE", 2, User_Id, 23, User_Role_Id);
            rv.Show();
        }

        private void lbl_FInal_Qc_Order_Work_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(23, "FInal Qc", User_Id, User_Role_Id, "Rework", 2);
            Emp_view.Show();
        }

        private void lbl_Exception_Order_Work_Count_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_View Emp_view = new Ordermanagement_01.Employee_View(24, "Exception", User_Id, User_Role_Id, "Rework", 2);
            Emp_view.Show();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
