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
namespace Ordermanagement_01.Tax
{
    public partial class Tax_New_Dashboard : Form
    {
        string user_role, user_id, user_name;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        string Client_Type;

        public Tax_New_Dashboard(string User_Role, string User_id, string User_name)
        {
            InitializeComponent();
            user_role = User_Role;
            user_id = User_id;
            user_name = User_name;
            lbl_username.Text = "Welcome," + user_name.ToString();
        }

       

        

        public void Get_Count_Of_Tax_Orders()
        {

            //Internal Tax Orders ----------

            Hashtable htInternalTaxProcessing = new Hashtable();
            System.Data.DataTable dtInternalTaxProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htInternalTaxProcessing.Add("@Trans", "SEARCH_TAX_REQUEST_WORK_ORDERS_ADMIN");
            }
            else
            {

                htInternalTaxProcessing.Add("@Trans", "SEARCH_TAX_REQUEST_WORK_ORDERS_USER");

            }
            dtInternalTaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxProcessing);
            if (dtInternalTaxProcessing.Rows.Count > 0)
            {

                lbl_Internal_Tax_Request_Assigned_Count.Text = "" + dtInternalTaxProcessing.Rows[0]["count"].ToString() + "";

            }



            Hashtable htInternalTaxAllocation = new Hashtable();
            System.Data.DataTable dtInternalTaxAllocation = new System.Data.DataTable();

            htInternalTaxAllocation.Add("@Trans", "NO_OF_SEARCH_TAX_REQUEST_ALLOCATION_COUNT");


            dtInternalTaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxAllocation);
            if (dtInternalTaxAllocation.Rows.Count > 0)
            {

                lbl_Internal_Tax_Request_Order_Allocation_Count.Text = "" + dtInternalTaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }




            Hashtable htInternalTaxQCAllocation = new Hashtable();
            System.Data.DataTable dtInternalQCTaxAllocation = new System.Data.DataTable();

            htInternalTaxQCAllocation.Add("@Trans", "NO_OF_SEARCH_TAX_REQUEST_QC_ALLOCATION_COUNT");


            dtInternalQCTaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxQCAllocation);
            if (dtInternalTaxAllocation.Rows.Count > 0)
            {

                lbl_Internal_Tax_Qc_Allocation.Text = "" + dtInternalQCTaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }


            Hashtable htInternalTaxQCProcessing = new Hashtable();
            System.Data.DataTable dtInternalTaxQCProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htInternalTaxQCProcessing.Add("@Trans", "SEARCH_TAX_QC_REQUEST_WORK_ORDERS_ADMIN");
            }
            else
            {

                htInternalTaxQCProcessing.Add("@Trans", "SEARCH_TAX_QC_REQUEST_WORK_ORDERS_USER");

            }
            dtInternalTaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxQCProcessing);
            if (dtInternalTaxQCProcessing.Rows.Count > 0)
            {

                lbl_Internal_Tax_Qc_Processing.Text = "" + dtInternalTaxQCProcessing.Rows[0]["count"].ToString() + "";

            }





            Hashtable htInternal_Tax_completed = new Hashtable();
            System.Data.DataTable dtInternal_Tax_completed = new System.Data.DataTable();
            htInternal_Tax_completed.Add("@Trans", "INTERNAL_TAX_ORDERS_COMPLETED_COUNT");
            dtInternal_Tax_completed = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternal_Tax_completed);
            if (dtInternal_Tax_completed.Rows.Count > 0)
            {

                lbl_Internal_Tax_Completed_Count.Text = "COMPLETED " + "[" + dtInternal_Tax_completed.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }


           


            Hashtable htInternal_Tax_Cancelled = new Hashtable();
            System.Data.DataTable dtInternal_Tax_Cancelled = new System.Data.DataTable();
            htInternal_Tax_Cancelled.Add("@Trans", "INTERNAL_TAX_ORDERS_CANCELLED_COUNT");
            dtInternal_Tax_Cancelled = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternal_Tax_Cancelled);
            if (dtInternal_Tax_Cancelled.Rows.Count > 0)
            {

                lbl_Internal_Tax_Cancelled_count.Text = "CANCELLED " + "[" + dtInternal_Tax_Cancelled.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }


            Hashtable htInternal_Tax_pending = new Hashtable();
            System.Data.DataTable dtInternal_Tax_pending = new System.Data.DataTable();
            htInternal_Tax_pending.Add("@Trans", "INTERNAL_TAX_ORDERS_PENDING_COUNT");
            dtInternal_Tax_pending = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternal_Tax_pending);
            if (dtInternal_Tax_pending.Rows.Count > 0)
            {


                lbl_Internal_Pending_Count.Text = "PENDING " + "[" + dtInternal_Tax_pending.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }


            Hashtable htInternal_Tax_mailway = new Hashtable();
            System.Data.DataTable dtInternal_Tax_mailway = new System.Data.DataTable();
            htInternal_Tax_mailway.Add("@Trans", "INTERNAL_TAX_ORDERS_MAILWAY_COUNT");
            dtInternal_Tax_mailway = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternal_Tax_mailway);
            if (dtInternal_Tax_mailway.Rows.Count > 0)
            {


                lbl_Internal_Tax_Mailway_Count.Text = "MAILWAY " + "[" + dtInternal_Tax_mailway.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }




            Hashtable htInternal_Tax_Exception = new Hashtable();
            System.Data.DataTable dtInternal_Tax_Exception = new System.Data.DataTable();
            htInternal_Tax_Exception.Add("@Trans", "INTERNAL_TAX_ORDERS_EXCEPTION_COUNT");
            dtInternal_Tax_Exception = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternal_Tax_Exception);
            if (dtInternal_Tax_Exception.Rows.Count > 0)
            {


                lbl_InternalTax_Exception_Count.Text = "EXCEPTION " + "[" + dtInternal_Tax_Exception.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }



            Hashtable htInternalTax_Reassigned_Order_Allocation_Count = new Hashtable();
            System.Data.DataTable dtInternalTax_Reassigned_Order_Allocation_Count = new System.Data.DataTable();
            if (user_role == "1")
            {
                htInternalTax_Reassigned_Order_Allocation_Count.Add("@Trans", "NO_OF_REASSIGNED_ORDER_ALLOCATION_COUNT");
                dtInternalTax_Reassigned_Order_Allocation_Count = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTax_Reassigned_Order_Allocation_Count);
                if (dtInternalTax_Reassigned_Order_Allocation_Count.Rows.Count > 0)
                {
                    lbl_Tax_Reassigned_Allocation_Count.Text = "" + dtInternalTax_Reassigned_Order_Allocation_Count.Rows[0]["Tax_Request_Count"].ToString() + "";


                }
            }

            //External Tax Orders ================================


            Hashtable htExternal_TaxProcessing = new Hashtable();
            System.Data.DataTable dtExternal_TaxProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htExternal_TaxProcessing.Add("@Trans", "TAX_REQUEST_WORK_ORDERS_ADMIN");
            }
            else 
            {

                htExternal_TaxProcessing.Add("@Trans", "TAX_REQUEST_WORK_ORDERS_USER");

            }

            dtExternal_TaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxProcessing);
            if (dtExternal_TaxProcessing.Rows.Count > 0)
            {

                lbl_External_Tax_Request_Processing.Text = "" + dtExternal_TaxProcessing.Rows[0]["count"].ToString() + "";

            }



         

            Hashtable htExternal_TaxQCProcessing = new Hashtable();
            System.Data.DataTable dtExternal_TaxQCProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htExternal_TaxQCProcessing.Add("@Trans", "TAX_REQUEST_QC_WORK_ORDERS_ADMIN");
            }
            else 
            {

                htExternal_TaxQCProcessing.Add("@Trans", "TAX_REQUEST_QC_WORK_ORDERS_USER");

            }

            dtExternal_TaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxQCProcessing);
            if (dtExternal_TaxQCProcessing.Rows.Count > 0)
            {

                lbl_External_Tax_Request_QC_Processing.Text = "" + dtExternal_TaxQCProcessing.Rows[0]["count"].ToString() + "";

            }




         

            Hashtable htExternal_TaxAllocation = new Hashtable();
            System.Data.DataTable dtExternal_TaxAllocation = new System.Data.DataTable();

            htExternal_TaxAllocation.Add("@Trans", "NO_OF_TAX_REQUEST_ALLOCATION_COUNT");


            dtExternal_TaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxAllocation);
            if (dtExternal_TaxAllocation.Rows.Count > 0)
            {

                lbl_External_Tax_Request_Allocation_Count.Text = "" + dtExternal_TaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }





         


            Hashtable htExternal_TaxQCAllocation = new Hashtable();
            System.Data.DataTable dtExternal_TaxQCAllocation = new System.Data.DataTable();

            htExternal_TaxQCAllocation.Add("@Trans", "NO_OF_TAX_REQUEST_QC_ALLOCATION_COUNT");


            dtExternal_TaxQCAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxQCAllocation);
            if (dtExternal_TaxQCAllocation.Rows.Count > 0)
            {

                lbl_External_Tax_Request_Qc_Allocation.Text = "" + dtExternal_TaxQCAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }






            Hashtable htExternal_Tax_completed = new Hashtable();
            System.Data.DataTable dtExternal_Tax_completed = new System.Data.DataTable();
            htExternal_Tax_completed.Add("@Trans", "EXTERNAL_TAX_ORDERS_COMPLETED_COUNT");
            dtExternal_Tax_completed = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_Tax_completed);
            if (dtExternal_Tax_completed.Rows.Count > 0)
            {
              
                lbl_External_Tax_Completed_Count.Text = "COMPLETED " + "[" + dtExternal_Tax_completed.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }

            Hashtable htdelivery = new Hashtable();
            System.Data.DataTable dtdelivery = new System.Data.DataTable();
            htdelivery.Add("@Trans", "EXTERNAL_TAX_ORDERS_DELIVERY_COUNT");
            dtdelivery = dataaccess.ExecuteSP("Sp_Tax_Orders", htdelivery);
            if (dtdelivery.Rows.Count > 0)
            {

                btn_Completed_Email.Text = "DELIVERY QUEUE " + "[" + dtdelivery.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }


            Hashtable htExternal_Tax_Cancelled = new Hashtable();
            System.Data.DataTable dtExternal_Tax_Cancelled = new System.Data.DataTable();
            htExternal_Tax_Cancelled.Add("@Trans", "EXTERNAL_TAX_ORDERS_CANCELLED_COUNT");
            dtExternal_Tax_Cancelled = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_Tax_Cancelled);
            if (dtExternal_Tax_Cancelled.Rows.Count > 0)
            {

                lbl_External_Tax_cancelled.Text = "CANCELLED " + "[" + dtExternal_Tax_Cancelled.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }


            Hashtable htExternal_Tax_pending = new Hashtable();
            System.Data.DataTable dtExternal_Tax_pending = new System.Data.DataTable();
            htExternal_Tax_pending.Add("@Trans", "EXTERNAL_TAX_ORDERS_PENDING_COUNT");
            dtExternal_Tax_pending = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_Tax_pending);
            if (dtExternal_Tax_pending.Rows.Count > 0)
            {


                lbl_External_Tax_Pending.Text = "PENDING " + "[" + dtExternal_Tax_pending.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }


            Hashtable htExternal_Tax_mailway = new Hashtable();
            System.Data.DataTable dtExternal_Tax_mailway = new System.Data.DataTable();
            htExternal_Tax_mailway.Add("@Trans", "EXTERNAL_TAX_ORDERS_MAILWAY_COUNT");
            dtExternal_Tax_mailway = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_Tax_mailway);
            if (dtExternal_Tax_mailway.Rows.Count > 0)
            {


                lbl_External_Tax_Mailaway.Text = "MAILWAY " + "[" + dtExternal_Tax_mailway.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }




            Hashtable htExternal_Tax_Exception = new Hashtable();
            System.Data.DataTable dtExternal_Tax_Exception = new System.Data.DataTable();
            htExternal_Tax_Exception.Add("@Trans", "EXTERNAL_TAX_ORDERS_EXCEPTION_COUNT");
            dtExternal_Tax_Exception = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_Tax_Exception);
            if (dtExternal_Tax_Exception.Rows.Count > 0)
            {


                lbl_External_Tax_Exception.Text = "EXCEPTION " + "[" + dtExternal_Tax_Exception.Rows[0]["Tax_Request_Count"].ToString() + "]";

            }




        }


        public void Get_Count_Of_Internal_External_Tax_Order_For_Employee()
        {

            Hashtable htInternalTaxProcessing = new Hashtable();
            System.Data.DataTable dtInternalTaxProcessing = new System.Data.DataTable();
           

            htInternalTaxProcessing.Add("@Trans", "SEARCH_TAX_REQUEST_WORK_ORDERS_USER");

          
            htInternalTaxProcessing.Add("@User_Id", user_id);

            dtInternalTaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Internal_Order_Dashboard_New", htInternalTaxProcessing);
            if (dtInternalTaxProcessing.Rows.Count > 0)
            {
                lbl_Internal_Tax_Request_Assigned_Count.Text = "" + dtInternalTaxProcessing.Rows[0]["count"].ToString() + "";
            }


            Hashtable htInternalTaxQCProcessing = new Hashtable();
            System.Data.DataTable dtInternalTaxQCProcessing = new System.Data.DataTable();
           

                htInternalTaxQCProcessing.Add("@Trans", "SEARCH_TAX_QC_REQUEST_WORK_ORDERS_USER");

            
            htInternalTaxQCProcessing.Add("@User_Id", user_id);
            dtInternalTaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Internal_Order_Dashboard_New", htInternalTaxQCProcessing);
            if (dtInternalTaxQCProcessing.Rows.Count > 0)
            {

                lbl_Internal_Tax_Qc_Processing.Text = "" + dtInternalTaxQCProcessing.Rows[0]["count"].ToString() + "";

            }



            Hashtable htExternal_TaxProcessing = new Hashtable();
            System.Data.DataTable dtExternal_TaxProcessing = new System.Data.DataTable();
           

                htExternal_TaxProcessing.Add("@Trans", "TAX_REQUEST_WORK_ORDERS_USER");

          
            htExternal_TaxProcessing.Add("@User_Id", user_id);
            dtExternal_TaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Internal_Order_Dashboard_New", htExternal_TaxProcessing);
            if (dtExternal_TaxProcessing.Rows.Count > 0)
            {

                lbl_External_Tax_Request_Processing.Text = "" + dtExternal_TaxProcessing.Rows[0]["count"].ToString() + "";

            }



            Hashtable htExternal_TaxQCProcessing = new Hashtable();
            System.Data.DataTable dtExternal_TaxQCProcessing = new System.Data.DataTable();
          

                htExternal_TaxQCProcessing.Add("@Trans", "TAX_REQUEST_QC_WORK_ORDERS_USER");

            
            htExternal_TaxQCProcessing.Add("@User_Id", user_id);
            dtExternal_TaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Internal_Order_Dashboard_New", htExternal_TaxQCProcessing);
            if (dtExternal_TaxQCProcessing.Rows.Count > 0)
            {

                lbl_External_Tax_Request_QC_Processing.Text = "" + dtExternal_TaxQCProcessing.Rows[0]["count"].ToString() + "";

            }


          

           


        }

        public void Get_Count_Of_Tax_Order_Employee()
        {

            //Internal Tax Orders ----------

            Hashtable htInternalTaxProcessing = new Hashtable();
            System.Data.DataTable dtInternalTaxProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htInternalTaxProcessing.Add("@Trans", "SEARCH_TAX_REQUEST_WORK_ORDERS_ADMIN");
            }
           
            htInternalTaxProcessing.Add("@User_Id", user_id);

            dtInternalTaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxProcessing);
            if (dtInternalTaxProcessing.Rows.Count > 0)
            {

                lbl_Internal_Tax_Request_Assigned_Count.Text = "" + dtInternalTaxProcessing.Rows[0]["count"].ToString() + "";

            }

            Hashtable htInternalTaxAllocation = new Hashtable();
            System.Data.DataTable dtInternalTaxAllocation = new System.Data.DataTable();

            htInternalTaxAllocation.Add("@Trans", "NO_OF_SEARCH_TAX_REQUEST_ALLOCATION_COUNT");


            dtInternalTaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxAllocation);
            if (dtInternalTaxAllocation.Rows.Count > 0)
            {

                lbl_Internal_Tax_Request_Order_Allocation_Count.Text = "" + dtInternalTaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }


            Hashtable htInternalTaxQCAllocation = new Hashtable();
            System.Data.DataTable dtInternalQCTaxAllocation = new System.Data.DataTable();

            htInternalTaxQCAllocation.Add("@Trans", "NO_OF_SEARCH_TAX_REQUEST_QC_ALLOCATION_COUNT");


            dtInternalQCTaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxQCAllocation);
            if (dtInternalTaxAllocation.Rows.Count > 0)
            {

                lbl_Internal_Tax_Qc_Allocation.Text = "" + dtInternalQCTaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }


            Hashtable htInternalTaxQCProcessing = new Hashtable();
            System.Data.DataTable dtInternalTaxQCProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htInternalTaxQCProcessing.Add("@Trans", "SEARCH_TAX_QC_REQUEST_WORK_ORDERS_ADMIN");
            }
           
            htInternalTaxQCProcessing.Add("@User_Id", user_id);
            dtInternalTaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htInternalTaxQCProcessing);
            if (dtInternalTaxQCProcessing.Rows.Count > 0)
            {

                lbl_Internal_Tax_Qc_Processing.Text = "" + dtInternalTaxQCProcessing.Rows[0]["count"].ToString() + "";

            }




            // This is internal_external tax_Order_Count_for_Employees

            Get_Count_Of_Internal_External_Tax_Order_For_Employee();




            //External Tax Orders ================================


            Hashtable htExternal_TaxProcessing = new Hashtable();
            System.Data.DataTable dtExternal_TaxProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htExternal_TaxProcessing.Add("@Trans", "TAX_REQUEST_WORK_ORDERS_ADMIN");
            }
           
            htExternal_TaxProcessing.Add("@User_Id",user_id);
            dtExternal_TaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxProcessing);
            if (dtExternal_TaxProcessing.Rows.Count > 0)
            {

                lbl_External_Tax_Request_Processing.Text = "" + dtExternal_TaxProcessing.Rows[0]["count"].ToString() + "";

            }





            Hashtable htExternal_TaxQCProcessing = new Hashtable();
            System.Data.DataTable dtExternal_TaxQCProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htExternal_TaxQCProcessing.Add("@Trans", "TAX_REQUEST_QC_WORK_ORDERS_ADMIN");
            }
            
            htExternal_TaxQCProcessing.Add("@User_Id", user_id);
            dtExternal_TaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxQCProcessing);
            if (dtExternal_TaxQCProcessing.Rows.Count > 0)
            {

                lbl_External_Tax_Request_QC_Processing.Text = "" + dtExternal_TaxQCProcessing.Rows[0]["count"].ToString() + "";

            }






            Hashtable htExternal_TaxAllocation = new Hashtable();
            System.Data.DataTable dtExternal_TaxAllocation = new System.Data.DataTable();

            htExternal_TaxAllocation.Add("@Trans", "NO_OF_TAX_REQUEST_ALLOCATION_COUNT");


            dtExternal_TaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxAllocation);
            if (dtExternal_TaxAllocation.Rows.Count > 0)
            {

                lbl_External_Tax_Request_Allocation_Count.Text = "" + dtExternal_TaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }








            Hashtable htExternal_TaxQCAllocation = new Hashtable();
            System.Data.DataTable dtExternal_TaxQCAllocation = new System.Data.DataTable();

            htExternal_TaxQCAllocation.Add("@Trans", "NO_OF_TAX_REQUEST_QC_ALLOCATION_COUNT");


            dtExternal_TaxQCAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htExternal_TaxQCAllocation);
            if (dtExternal_TaxQCAllocation.Rows.Count > 0)
            {

                lbl_External_Tax_Request_Qc_Allocation.Text = "" + dtExternal_TaxQCAllocation.Rows[0]["Tax_Request_Count"].ToString() + "";

            }





        }

        private void Tax_New_Dashboard_Load(object sender, EventArgs e)
        
        {
            
            load_Progressbar.Start_progres();
            if (user_role == "1")
            {
                Get_Count_Of_Tax_Orders();
               
               

            }
            else
            {

                Get_Count_Of_Tax_Order_Employee();
           
                Group_Internal_Status.Visible = false;
                Group_External_Status.Visible = false;
                //groupBox3.Enabled = false;
                //lbl_Internal_Tax_Request_Order_Allocation_Count.Enabled = false;
               // lbl_Internal_Tax_Qc_Allocation.Enabled = false;
                //lbl_External_Tax_Request_Allocation_Count.Enabled = false;
//lbl_External_Tax_Request_Qc_Allocation.Enabled = false;
                AdminstrationToolStripMenuItem.Enabled = false;
                ReportsToolStripMenuItem.Enabled = false;

            }
            this.WindowState = FormWindowState.Maximized;
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
             if (user_role != "2")
            {
                Get_Count_Of_Tax_Orders();
               
               

            }
             else 
             {
                 Get_Count_Of_Tax_Order_Employee();
             }
        }

        private void UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.User_Create Create_User = new User_Create(int.Parse(user_id.ToString()), 2,lbl_username.Text);
            Create_User.Show();

        }

        private void activeDeactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Users.ActiveInactive ActiveInactive = new Ordermanagement_01.Users.ActiveInactive(int.Parse(user_id.ToString()), 2);
            ActiveInactive.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (user_role != "2")
            {
                Get_Count_Of_Tax_Orders();



            }
            else if (user_role == "2")
            {
                Get_Count_Of_Tax_Order_Employee();
            }
        }

        private void lbl_Internal_Tax_Request_Order_Allocation_Count_Click(object sender, EventArgs e)
        {
            if (user_role != "2")
            {
                Client_Type = "INTERNAL";


                Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_Tax_Request_Orders_For_Allocate", Client_Type);
                    tx.Show();
               
            }
            else
            {

                MessageBox.Show("Unauthorised Access");
            }
        }

        private void lbl_Internal_Tax_Request_Assigned_Count_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View txemp = new Tax_Employee_Order_View(user_id, user_role, "Internal_Tax_Request");
            txemp.Show();
        }

        private void lbl_Internal_Tax_Qc_Allocation_Click(object sender, EventArgs e)
        {
            if (user_role != "2")
            {
                Client_Type = "INTERNAL";
                Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_TAx_Request_Qc_For_Order_Allocate", Client_Type);
                tx.Show();

            }
            else
            {

                MessageBox.Show("Unauthorised Access");
            }
            
        }

        private void lbl_Internal_Tax_Qc_Processing_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View tx = new Tax_Employee_Order_View(user_id, user_role, "Internal_Tax_Request_Qc");
            tx.Show();
        }

        private void lbl_External_Tax_Request_Allocation_Count_Click(object sender, EventArgs e)
        {

            if (user_role != "2")
            {
                Client_Type = "EXTERNAL";
                Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "External_TaxRequest_Order_For_Allocate", Client_Type);
                tx.Show();

            }
            else
            {

                MessageBox.Show("Unauthorised Access");
            }
            
            
        }

        private void lbl_External_Tax_Request_Processing_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View txemp = new Tax_Employee_Order_View(user_id, user_role, "External_Tax_Request");
            txemp.Show();
        }

        private void lbl_External_Tax_Request_Qc_Allocation_Click(object sender, EventArgs e)
        {

            if (user_role != "2")
            {
                Client_Type = "EXTERNAL";
                Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "External_Tax_Request_Qc_For_Order_Allocate", Client_Type);
                tx.Show();
            }
            else
            {

                MessageBox.Show("Unauthorised Access");
            }
            
        }

        private void lbl_External_Tax_Request_QC_Processing_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View tx = new Tax_Employee_Order_View(user_id, user_role, "External_Tax_Request_Qc");
            tx.Show(); 
        }

        private void lbl_Internal_Tax_Completed_Count_Click(object sender, EventArgs e)
        {
            Client_Type = "INTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_Completed", Client_Type);
            tx.Show(); 

        }

        private void lbl_Internal_Pending_Count_Click(object sender, EventArgs e)
        {
            Client_Type = "INTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_Pending", Client_Type);
            tx.Show(); 
        }

        private void lbl_Internal_Tax_Mailway_Count_Click(object sender, EventArgs e)
        {
            Client_Type = "INTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_Mailway", Client_Type);
            tx.Show(); 
        }

        private void lbl_InternalTax_Exception_Count_Click(object sender, EventArgs e)
        {
            Client_Type = "INTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_Exception", Client_Type);
            tx.Show(); 

        }

        private void lbl_Internal_Tax_Cancelled_count_Click(object sender, EventArgs e)
        {
            Client_Type = "INTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_Cancelled", Client_Type);
            tx.Show(); 
        }

        private void lbl_External_Tax_Completed_Count_Click(object sender, EventArgs e)
        {
            Client_Type = "EXTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "External_Completed", Client_Type);
            tx.Show(); 
        }

        private void lbl_External_Tax_Pending_Click(object sender, EventArgs e)
        {
            Client_Type = "EXTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "External_Pending", Client_Type);
            tx.Show(); 

        }

        private void lbl_External_Tax_Mailaway_Click(object sender, EventArgs e)
        {
            Client_Type = "EXTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "External_Mailway", Client_Type);
            tx.Show(); 

        }

        private void lbl_External_Tax_Exception_Click(object sender, EventArgs e)
        {
            Client_Type = "EXTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "External_Exception", Client_Type);
            tx.Show(); 
        }

        private void lbl_External_Tax_cancelled_Click(object sender, EventArgs e)
        {
            Client_Type = "EXTERNAL";
            Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "External_Cancelled", Client_Type);
            tx.Show(); 
        }

        private void btn_log_Out_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to Log Out", "Log Out Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {

                System.Environment.Exit(1);
            }
          //  System.Environment.Exit(1);
        }

        private void btn_Refresh_Click_1(object sender, EventArgs e)
        {

        }

        private void myProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Users.User_Profile UserProfile = new Ordermanagement_01.Users.User_Profile(int.Parse(user_id));
            UserProfile.Show();
        }

        private void btn_Tax_Search_Click(object sender, EventArgs e)
        {
            if (user_role != "2")
            {
                Ordermanagement_01.Tax.Tax_Order_Reallocate Taxrelocate = new Tax_Order_Reallocate(user_id,user_role);
                Taxrelocate.Show();
            }
            else

            {

                MessageBox.Show("Unauthorised Access");
            }
        }

        private void mnuReportExplorer_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Tax.Tax_Reports Taxreport = new Tax_Reports();
            Taxreport.Show();

        }

        private void btn_Completed_Email_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Tax.Tax_Completed_Mail mail = new Ordermanagement_01.Tax.Tax_Completed_Mail(user_id,user_role);
            mail.Show();
        }

        private void lbl_Tax_Reassigned_Allocation_Count_Click(object sender, EventArgs e)
        {
            if (user_role != "2")
            {
                Client_Type = "INTERNAL";


                Tax_Order_Allocation_New tx = new Tax_Order_Allocation_New(user_id, user_role, "Internal_Tax_Reassigned_Request_Orders_For_Allocate", Client_Type);
                tx.Show();

            }
            else
            {

                MessageBox.Show("Unauthorised Access");
            }
        }

        private void taxInhouseSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Tax_Summary ts = new Tax_Summary(Convert.ToInt32(user_id),user_role);
            ts.Show();
        }

     

       
    }
}
