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
    public partial class Tax_Dashboard : Form
    {
        string user_role, user_id, user_name;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        int Tree_View_UserId;
        public Tax_Dashboard(string User_Role, string User_id, string User_name)
        {
            InitializeComponent();
            user_role = User_Role;
            user_id = User_id;
            user_name = User_name;
            lbl_username.Text = "Welcome," + user_name.ToString();

        }

        private void lbl_Search_Tax_Request_Count_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View txemp = new Tax_Employee_Order_View(user_id,user_role,"Search_Tax_Request");
            txemp.Show();
        }

        private void lbl_Tax_Search_Tax_Request_Order_Allocation_Count_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "Search_Tax_Request_Orders_For_Allocate","");
            tx.Show();
        }


        public void Get_Count_Of_Tax_Orders()
        
        
        
        {

            Hashtable htsearchTaxProcessing = new Hashtable();
            System.Data.DataTable dtsearchTaxProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htsearchTaxProcessing.Add("@Trans", "SEARCH_TAX_REQUEST_WORK_ORDERS_ADMIN");
            }
            else
            {

                htsearchTaxProcessing.Add("@Trans", "SEARCH_TAX_REQUEST_WORK_ORDERS_USER");

            }
            dtsearchTaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htsearchTaxProcessing);
            if (dtsearchTaxProcessing.Rows.Count > 0)
            {

                lbl_Search_Tax_Request_Count.Text = "SEARCH TAX REQUEST  " + "(" + dtsearchTaxProcessing.Rows[0]["count"].ToString() + ")";

            }


            Hashtable htTaxProcessing = new Hashtable();
            System.Data.DataTable dtTaxProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htTaxProcessing.Add("@Trans", "TAX_REQUEST_WORK_ORDERS_ADMIN");
            }
            else 
            {

                htTaxProcessing.Add("@Trans", "TAX_REQUEST_WORK_ORDERS_USER");

            }
       
            dtTaxProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxProcessing);
            if (dtTaxProcessing.Rows.Count > 0)
            {

                lbl_Tax_Request_Count.Text = "TAX REQUEST  " + "(" + dtTaxProcessing.Rows[0]["count"].ToString() + ")";

            }



            Hashtable htsearchTaxQCProcessing = new Hashtable();
            System.Data.DataTable dtsearchTaxQCProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htsearchTaxQCProcessing.Add("@Trans", "SEARCH_TAX_QC_REQUEST_WORK_ORDERS_ADMIN");
            }
            else 
            {

                htsearchTaxQCProcessing.Add("@Trans", "SEARCH_TAX_QC_REQUEST_WORK_ORDERS_USER");

            }
            dtsearchTaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htsearchTaxQCProcessing);
            if (dtsearchTaxQCProcessing.Rows.Count > 0)
            {

                lbl_Search_Tax_Qc_Processing.Text = "SEARCH TAX REQUEST  " + "(" + dtsearchTaxQCProcessing.Rows[0]["count"].ToString() + ")";

            }


            Hashtable htTaxQCProcessing = new Hashtable();
            System.Data.DataTable dtTaxQCProcessing = new System.Data.DataTable();
            if (user_role == "1")
            {
                htTaxQCProcessing.Add("@Trans", "TAX_REQUEST_QC_WORK_ORDERS_ADMIN");
            }
            else 
            {

                htTaxQCProcessing.Add("@Trans", "TAX_REQUEST_QC_WORK_ORDERS_USER");

            }

            dtTaxQCProcessing = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxQCProcessing);
            if (dtTaxQCProcessing.Rows.Count > 0)
            {

                lbl_Tax_Request_QC_Processing.Text = "TAX REQUEST  " + "(" + dtTaxQCProcessing.Rows[0]["count"].ToString() + ")";

            }




            Hashtable htsearchTaxAllocation = new Hashtable();
            System.Data.DataTable dtsearchTaxAllocation = new System.Data.DataTable();

            htsearchTaxAllocation.Add("@Trans", "NO_OF_SEARCH_TAX_REQUEST_ALLOCATION_COUNT");
            
           
            dtsearchTaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htsearchTaxAllocation);
            if (dtsearchTaxAllocation.Rows.Count > 0)
            {

                lbl_Tax_Search_Tax_Request_Order_Allocation_Count.Text = "SEARCH TAX REQUEST  " + "(" + dtsearchTaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }


            Hashtable htTaxAllocation = new Hashtable();
            System.Data.DataTable dtTaxAllocation = new System.Data.DataTable();

            htTaxAllocation.Add("@Trans", "NO_OF_TAX_REQUEST_ALLOCATION_COUNT");
          

            dtTaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxAllocation);
            if (dtTaxAllocation.Rows.Count > 0)
            {

                lbl_Tax_Request_Allocation_Count.Text = "TAX REQUEST  " + "(" + dtTaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }





            Hashtable htsearchTaxQCAllocation = new Hashtable();
            System.Data.DataTable dtsearchQCTaxAllocation = new System.Data.DataTable();

            htsearchTaxQCAllocation.Add("@Trans", "NO_OF_SEARCH_TAX_REQUEST_QC_ALLOCATION_COUNT");


            dtsearchQCTaxAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htsearchTaxQCAllocation);
            if (dtsearchTaxAllocation.Rows.Count > 0)
            {
                
                lbl_Search_Tax_Qc_Allocation.Text = "SEARCH TAX REQUEST  " + "(" + dtsearchQCTaxAllocation.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }


            Hashtable htTaxQCAllocation = new Hashtable();
            System.Data.DataTable dtTaxQCAllocation = new System.Data.DataTable();

            htTaxQCAllocation.Add("@Trans", "NO_OF_TAX_REQUEST_QC_ALLOCATION_COUNT");


            dtTaxQCAllocation = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxQCAllocation);
            if (dtTaxQCAllocation.Rows.Count > 0)
            {

                lbl_Tax_Request_Qc_Allocation.Text = "TAX REQUEST  " + "(" + dtTaxQCAllocation.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }


            Hashtable htcompleted = new Hashtable();
            System.Data.DataTable dtcompleted = new System.Data.DataTable();
            htcompleted.Add("@Trans", "TAX_ORDERS_COMPLETED_COUNT");
            dtcompleted = dataaccess.ExecuteSP("Sp_Tax_Orders", htcompleted);
            if (dtcompleted.Rows.Count > 0)
            {

                lbl_Tax_Completed_Count.Text = "COMPLETED " + "(" + dtcompleted.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }


            Hashtable htTaxCancelled = new Hashtable();
            System.Data.DataTable dtTaxCancelled = new System.Data.DataTable();
            htTaxCancelled.Add("@Trans", "TAX_ORDERS_CANCELLED_COUNT");
            dtTaxCancelled = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxCancelled);
            if (dtTaxCancelled.Rows.Count > 0)
            {

                lbl_Tax_Cancelled_count.Text = "CANCELLED " + "(" + dtTaxCancelled.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }


            Hashtable htTaxpending = new Hashtable();
            System.Data.DataTable dtTaxpending = new System.Data.DataTable();
            htTaxpending.Add("@Trans", "TAX_ORDERS_PENDING_COUNT");
            dtTaxpending = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxpending);
            if (dtTaxpending.Rows.Count > 0)
            {


                lbl_Tax_Pending_Count.Text = "PENDING " + "(" + dtTaxpending.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }


            Hashtable htTaxmailway = new Hashtable();
            System.Data.DataTable dtTaxmailway = new System.Data.DataTable();
            htTaxmailway.Add("@Trans", "TAX_ORDERS_MAILWAY_COUNT");
            dtTaxmailway = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxmailway);
            if (dtTaxmailway.Rows.Count > 0)
            {


                lbl_Tax_Mailway_Count.Text = "MAILWAY " + "(" + dtTaxmailway.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }




            Hashtable htTaxException = new Hashtable();
            System.Data.DataTable dtTaxException = new System.Data.DataTable();
            htTaxException.Add("@Trans", "TAX_ORDERS_EXCEPTION_COUNT");
            dtTaxException = dataaccess.ExecuteSP("Sp_Tax_Orders", htTaxException);
            if (dtTaxException.Rows.Count > 0)
            {


                lbl_Tax_Exception_Count.Text = "EXCEPTION " + "(" + dtTaxException.Rows[0]["Tax_Request_Count"].ToString() + ")";

            }



        }

        private void Tax_Dashboard_Load(object sender, EventArgs e)
        {
            Get_Count_Of_Tax_Orders();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Get_Count_Of_Tax_Orders();

        }

        private void lbl_Tax_Request_Allocation_Count_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "TaxRequest_Order_For_Allocate","");
            tx.Show(); 
            
        }

        private void lbl_Search_Tax_Qc_Allocation_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "Search_TAx_Request_Qc_For_Order_Allocate","");
            tx.Show(); 
            
        }

        private void lbl_Tax_Request_Qc_Allocation_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "Tax_Request_Qc_For_Order_Allocate","");
            tx.Show(); 
            
        }

        private void lbl_Tax_Request_Count_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Tax_Request_Count_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View txemp = new Tax_Employee_Order_View(user_id, user_role, "Tax_Request");
            txemp.Show();
        }

        private void lbl_Search_Tax_Qc_Processing_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View tx = new Tax_Employee_Order_View(user_id, user_role, "Search_Tax_Request_Qc");
            tx.Show(); 
        }

        private void lbl_Tax_Request_QC_Processing_Click(object sender, EventArgs e)
        {
            Tax_Employee_Order_View tx = new Tax_Employee_Order_View(user_id, user_role, "Tax_Request_Qc");
            tx.Show(); 
        }

        private void ToolStripButton12_Click(object sender, EventArgs e)
        {

        }

        private void activeDeactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Users.ActiveInactive ActiveInactive = new Ordermanagement_01.Users.ActiveInactive(int.Parse(user_id.ToString()), 2);
            ActiveInactive.Show();
        }

        private void UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.User_Create Create_User = new User_Create(int.Parse(user_id.ToString()), 2,lbl_username.Text);
            Create_User.Show();

        }

        private void btn_log_Out_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void lbl_Tax_Completed_Count_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role,"Completed","");
            tx.Show(); 
        }

        private void lbl_Tax_Pending_Count_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "Pending","");
            tx.Show(); 
        }

        private void lbl_Tax_Mailway_Count_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "Mailway","");
            tx.Show(); 
        }

        private void lbl_Tax_Exception_Count_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "Exception","");
            tx.Show(); 
        }

        private void lbl_Tax_Cancelled_count_Click(object sender, EventArgs e)
        {
            Tax_Order_Allocation tx = new Tax_Order_Allocation(user_id, user_role, "Cancelled","");
            tx.Show(); 

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Get_Count_Of_Tax_Orders();
        }

       

    }
}
