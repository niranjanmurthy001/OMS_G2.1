using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Ordermanagement_01
{
    public partial class Abstractor_Order_Entry : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid;
        int Order_Id;
         string roleid;
         string SESSION_ORDER_NO;
         string Efftectiv_date;
         int Sub_ProcessId;
         string Client_Name;
         string Sub_ProcessName;
         string SESSSION_ORDER_TYPE;
         string SESSION_ORDER_TASK;
         DateTime date2;
         int No_Of_Pages;
         string OPERATE_PRODUCTION_DATE;
         int Chk_Order_Search_Cost;
         string OPERATE_SEARCH_COST;
         int MAX_TIME_ID;
         int Chk_Production_date;
   
         decimal SearchCost, Copy_Cost, Abstractor_Cost;
         public Abstractor_Order_Entry(string SESSIONORDERNO, int Orderid, int User_id, string Role_id, string OrderProcess, int SESSIONORDERTASK)
        {
            userid = User_id;
            Order_Id = Orderid;
            roleid = Role_id;
            SESSION_ORDER_TASK = Convert.ToString(SESSIONORDERTASK);
            InitializeComponent();
            ddl_Order_Source.Items.Insert(0, "Online");
            ddl_Order_Source.Items.Insert(1, "Subscription");
            ddl_Order_Source.Items.Insert(2, "Plant");
            ddl_Order_Source.Items.Insert(3, "Abstractor");
            dbc.Bind_Order_Progress(ddl_order_Staus);
            SESSION_ORDER_NO = SESSIONORDERNO;
            SESSSION_ORDER_TYPE = OrderProcess;
            Get_Order_Details();
            Geydview_Bind_Comments();
            Get_Order_Search_Cost_Details();
        }
        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT");
            htComments.Add("@Order_Id", Order_Id);
            dtComments = dataaccess.ExecuteSP("Sp_Order_Comments", htComments);
            if (dtComments.Rows.Count > 0)
            {
                //ex2.Visible = true;
                 Grid_Comments.Rows.Clear();
                 for (int i = 0; i < dtComments.Rows.Count; i++)
                 {
                     Grid_Comments.Rows.Add();
                     Grid_Comments.Rows[i].Cells[0].Value = i + 1;
                     Grid_Comments.Rows[i].Cells[1].Value = dtComments.Rows[i]["Comment_Id"].ToString();
                     Grid_Comments.Rows[i].Cells[2].Value = dtComments.Rows[i]["Comment"].ToString();
                     Grid_Comments.Rows[i].Cells[3].Value = dtComments.Rows[i]["User_Name"].ToString();
                 }
            }
            else
            {
               


            }


        }
        protected void Get_Order_Search_Cost_Details()
        {
            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT");
            ht_Select_Order_Details.Add("@Order_Id", Order_Id);

            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                ddl_Order_Source.Text = dt_Select_Order_Details.Rows[0]["Source"].ToString();
                txt_Order_Search_Cost.Text = dt_Select_Order_Details.Rows[0]["Search_Cost"].ToString();
                txt_Legal_Reference.Text = dt_Select_Order_Details.Rows[0]["Copy_Cost"].ToString();
                txt_Order_Abstractor_Cost.Text = dt_Select_Order_Details.Rows[0]["Abstractor_Cost"].ToString();
                txt_Order_No_Of_Pages.Text = dt_Select_Order_Details.Rows[0]["No_Of_pages"].ToString();
            }
        }
        protected void Get_Order_Details()
        {

            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_NO_WISE");
            ht_Select_Order_Details.Add("@Client_Order_Number", SESSION_ORDER_NO.ToString());
            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);

            if (dt_Select_Order_Details.Rows.Count > 0)
            {
               // Order_Id = Order_Id;
               // Order_Id = Order_Id;
                lbl_Order_Number.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                lbl_customer_No.Text = dt_Select_Order_Details.Rows[0]["Client_Number"].ToString();
                lbl_Order_Type.Text = dt_Select_Order_Details.Rows[0]["Order_Type"].ToString();
                lbl_Property_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                lbl_State.Text = dt_Select_Order_Details.Rows[0]["State"].ToString();
                lbl_County.Text = dt_Select_Order_Details.Rows[0]["County"].ToString();
                lbl_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                lbl_Order_Refno.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Ref"].ToString();
                lbl_Barrower_Name.Text = dt_Select_Order_Details.Rows[0]["Borrower_Name"].ToString();
                lbl_Notes.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();
                Efftectiv_date = dt_Select_Order_Details.Rows[0]["Effective_date"].ToString();
                txt_Preliminary_date.Text = dt_Select_Order_Details.Rows[0]["Effective_date"].ToString();
                Sub_ProcessId =int.Parse(dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString());
                Client_Name = dt_Select_Order_Details.Rows[0]["Client_Name"].ToString();
                Sub_ProcessName = dt_Select_Order_Details.Rows[0]["Sub_ProcessName"].ToString();
            }
        }
        private void Employee_Order_Entry_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads(Order_Id);
            Orderuploads.Show();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            

        if (SESSSION_ORDER_TYPE == "Search" && ddl_Order_Source.SelectedValue == "SELECT")
        {
            ddl_Order_Source.Focus();
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Enter Order Source')</script>", false);
            MessageBox.Show("Enter Order Source");
        }
        else
        {

            if (Validate_Order_Info() != false)
            {
                if (Chk_Self_Allocate.Checked == true )
                {
                    Hashtable htuser_Status = new Hashtable();
                    DataTable dtuser_Status = new System.Data.DataTable();
                    htuser_Status.Add("@Trans", "SELECT_STATUSID");
                    htuser_Status.Add("@Order_Status", ddl_order_Task.SelectedItem);
                    dtuser_Status = dataaccess.ExecuteSP("Sp_Order_Status", htuser_Status);
                    Hashtable htSelf_Allocate = new Hashtable();
                    DataTable dtSelf_Allocate = new System.Data.DataTable();
                    htSelf_Allocate.Add("@Trans", "UPDATE_SELF_ALLOCATE_STATUS");
                    htSelf_Allocate.Add("@Order_ID", Order_Id);
                    htSelf_Allocate.Add("Order_Status_Id", int.Parse(dtuser_Status.Rows[0]["Order_Status_ID"].ToString()));
                    htSelf_Allocate.Add("@User_Id", userid);
                    htSelf_Allocate.Add("@Modified_By", userid);
                    htSelf_Allocate.Add("@Assigned_By", userid);
                    htSelf_Allocate.Add("@Modified_Date", DateTime.Now);
                    htSelf_Allocate.Add("@Assigned_Date", DateTime.Now);
                    dtSelf_Allocate = dataaccess.ExecuteSP("Sp_Order_Assignment", htSelf_Allocate);

                    

                    Hashtable htProgress_update = new Hashtable();
                    DataTable dtProgress_update = new System.Data.DataTable();
                    htProgress_update.Add("@Trans", "UPDATE_PROGRESS");
                    htProgress_update.Add("@Order_Progress", 6);
                    htProgress_update.Add("@Order_ID", Order_Id);
                    dtProgress_update = dataaccess.ExecuteSP("Sp_Order", htProgress_update);

                    Hashtable ht_Status_Update = new Hashtable();
                    DataTable dt_Status_Update = new System.Data.DataTable();
                    ht_Status_Update.Add("@Trans", "UPDATE_STATUS");
                    ht_Status_Update.Add("@Order_ID", Order_Id);
                    ht_Status_Update.Add("@Order_Status", int.Parse(dtuser_Status.Rows[0]["Order_Status_ID"].ToString()));
                    ht_Status_Update.Add("@Modified_By", userid);
                    ht_Status_Update.Add("@Modified_Date", DateTime.Now);
                    dt_Status_Update = dataaccess.ExecuteSP("Sp_Order", ht_Status_Update);
                    string url = "AdminDashboard.aspx";
                    MessageBox.Show("Order Submitted Sucessfully");
                }
                else if (Chk_Self_Allocate.Checked == false)
                {

                int Order_Task = int.Parse(SESSION_ORDER_TASK.ToString().ToString());

                if (txt_Preliminary_date.Text != "")
                {

                    if (txt_Prdoductiondate.Text != "" && ValidateProductionDate() != false)
                    {
                        //if (Order_Task == 1 || Order_Task == 2)
                        //{
                        DateTime date1 = DateTime.Now;
                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        Hashtable htupdate = new Hashtable();
                        DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_PROGRESS");

                        htupdate.Add("@Order_ID", Order_Id);

                        if (ddl_order_Task.Visible != true)
                        {
                            htupdate.Add("@Order_Status", SESSION_ORDER_TASK.ToString());
                            htupdate.Add("@Order_Progress", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                        }
                        else if (ddl_order_Task.Visible == true && ddl_order_Task.SelectedItem!= "Upload Completed")
                        {
                            Hashtable htuser = new Hashtable();
                            DataTable dtuser = new System.Data.DataTable();
                            htuser.Add("@Trans", "SELECT_STATUSID");
                            htuser.Add("@Order_Status", ddl_order_Task.SelectedItem);
                            dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                            htupdate.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                            htupdate.Add("@Order_Progress", 8);
                        }
                        else if (ddl_order_Task.Visible == true && ddl_order_Task.SelectedItem == "Upload Completed")
                        {
                            Hashtable htuser = new Hashtable();
                            DataTable dtuser = new System.Data.DataTable();
                            htuser.Add("@Trans", "SELECT_STATUSID");
                            htuser.Add("@Order_Status", ddl_order_Task.SelectedItem);
                            dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                            htupdate.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                            htupdate.Add("@Order_Progress", 3);
                        }
                        htupdate.Add("@Modified_By", userid);
                        htupdate.Add("@Modified_Date", dateeval);
                        dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);


                        Hashtable htprogress = new Hashtable();
                        DataTable dtprogress = new System.Data.DataTable();
                        htprogress.Add("@Trans", "UPDATE");
                        htprogress.Add("@Order_ID", Order_Id);
                        if (ddl_order_Task.Visible != true)
                        {
                            htprogress.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                        }
                        else if (ddl_order_Task.Visible == true && ddl_order_Task.SelectedItem != "Upload Completed")
                        {
                            htprogress.Add("@Order_Progress_Id", 8);
                        }
                        else if (ddl_order_Task.Visible == true && ddl_order_Task.SelectedItem == "Upload Completed")
                        {
                            htprogress.Add("@Order_Progress_Id", 3);
                        }
                        htprogress.Add("@Modified_By", userid);
                        htprogress.Add("@Modified_Date", date);
                        dtprogress = dataaccess.ExecuteSP("Sp_Order_Assignment", htprogress);

                        Hashtable ht_Status = new Hashtable();
                        DataTable dt_Status = new System.Data.DataTable();
                        ht_Status.Add("@Trans", "UPDATE_STATUS");
                        ht_Status.Add("@Order_ID", Order_Id);

                        if (ddl_order_Task.Visible != true)
                        {
                            ht_Status.Add("@Order_Status", SESSION_ORDER_TASK.ToString());
                            ht_Status.Add("@Order_Progress", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                        }
                        else if (ddl_order_Task.Visible == true && ddl_order_Task.SelectedItem != "Upload Completed")
                        {
                            Hashtable htuser = new Hashtable();
                            DataTable dtuser = new System.Data.DataTable();
                            htuser.Add("@Trans", "SELECT_STATUSID");
                            htuser.Add("@Order_Status", ddl_order_Task.SelectedItem);
                            dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                            ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                            ht_Status.Add("@Order_Progress", 8);
                        }
                        else if (ddl_order_Task.Visible == true && ddl_order_Task.SelectedItem == "Upload Completed")
                        {
                            Hashtable htuser = new Hashtable();
                            DataTable dtuser = new System.Data.DataTable();
                            htuser.Add("@Trans", "SELECT_STATUSID");
                            htuser.Add("@Order_Status", ddl_order_Task.SelectedItem);
                            dtuser = dataaccess.ExecuteSP("Sp_Order_Status", htuser);
                            ht_Status.Add("@Order_Status", int.Parse(dtuser.Rows[0]["Order_Status_ID"].ToString()));
                            ht_Status.Add("@Order_Progress", 8);
                        }
                        ht_Status.Add("@Modified_By", userid);
                        ht_Status.Add("@Modified_Date", dateeval);
                        dt_Status = dataaccess.ExecuteSP("Sp_Order", ht_Status);
                        if (ddl_order_Staus.SelectedItem != "USER HOLD" )
                        {
                            Hashtable ht_Chk_Order = new Hashtable();
                            DataTable dt_Chk_Order = new DataTable();
                            ht_Chk_Order.Add("@Trans", "Emp_Order_Count");
                            ht_Chk_Order.Add("@Employee_Id", userid);
                            dt_Chk_Order = dataaccess.ExecuteSP("Sp_Order_Auto_Allocation", ht_Chk_Order);
                            if (int.Parse(dt_Chk_Order.Rows[0]["count_Order"].ToString()) <= 0)
                            {
                                Hashtable ht_Update_Emp_Status = new Hashtable();
                                DataTable dt_Update_Emp_Status = new DataTable();
                                ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                ht_Update_Emp_Status.Add("@Employee_Id", userid);
                                ht_Update_Emp_Status.Add("@Allocate_Status", "False");
                                dt_Update_Emp_Status = dataaccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);
                            }
                        }
                        Hashtable htEffectivedate = new Hashtable();
                        DataTable dtEffectivdate = new System.Data.DataTable();
                        htEffectivedate.Add("@Trans", "UPDATE_EFFECTIVEDATE");
                        htEffectivedate.Add("@Order_ID", Order_Id);
                        htEffectivedate.Add("@Effective_date", txt_Preliminary_date.Text);
                        htEffectivedate.Add("@Modified_By", userid);
                        htEffectivedate.Add("@Modified_Date", dateeval);
                        dtEffectivdate = dataaccess.ExecuteSP("Sp_Order", htEffectivedate);
                        Hashtable ht_Productiondate = new Hashtable();
                        DataTable dt_Production_date = new DataTable();

                        ht_Productiondate.Add("@Trans", "CHK_PRODUCTION_DATE");
                        ht_Productiondate.Add("@Order_ID", Order_Id);
                        ht_Productiondate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                        dt_Production_date = dataaccess.ExecuteSP("Sp_Order_ProductionDate", ht_Productiondate);

                        if (dt_Production_date.Rows.Count > 0)
                        {

                            Chk_Production_date = int.Parse(dt_Production_date.Rows[0]["count"].ToString());


                        }
                        else
                        {

                            Chk_Production_date = 0;
                        }

                        if (Chk_Production_date > 0)
                        {
                            OPERATE_PRODUCTION_DATE = "UPDATE";
                            Insert_ProductionDate(sender, e);

                        }
                        else if (Chk_Production_date == 0)
                        {
                            OPERATE_PRODUCTION_DATE = "INSERT";
                            Insert_ProductionDate(sender, e);
                        }
                        Insert_OrderComments(sender, e);
                       
                        Geydview_Bind_Notes();
                        Geydview_Bind_Comments();
                        if (Order_Task == 1 || Order_Task == 2)
                        {
                            Hashtable ht_Select_Order_Details = new Hashtable();
                            DataTable dt_Select_Order_Details = new DataTable();

                            ht_Select_Order_Details.Add("@Trans", "CHECK_ORDER_SEARCH_COUNT");
                            ht_Select_Order_Details.Add("@Order_ID", Order_Id);

                            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);

                            if (dt_Select_Order_Details.Rows.Count > 0)
                            {

                                Chk_Order_Search_Cost = int.Parse(dt_Select_Order_Details.Rows[0]["count"].ToString());


                            }
                            else
                            {

                                Chk_Order_Search_Cost = 0;
                            }

                            if (Chk_Order_Search_Cost > 0)
                            {
                                OPERATE_SEARCH_COST = "UPDATE";
                                Insert_Order_Search_Cost(sender, e);

                            }
                            else if (Chk_Order_Search_Cost == 0)
                            {
                                OPERATE_SEARCH_COST = "INSERT";
                                Insert_Order_Search_Cost(sender, e);
                            }
                        }
                        Update_User_Order_Time_Info();
                        Clear();
                       


                        string url = "AdminDashboard.aspx";
                        MessageBox.Show("Order Submitted Sucessfully");
                     
                        

                    }
                    else
                    {
                        txt_Prdoductiondate.Focus();
                      
                        MessageBox.Show("Enter Production  Date");
                    }
                }
                else
                {
                    txt_Preliminary_date.Focus();
                    MessageBox.Show("Enter Effective Date");
                 
                }

            }
            }
        }
        this.Close();
        }
        protected void Clear()
        {

            ddl_order_Staus.SelectedIndex = 0;
            txt_Comments.Text = "";
            //  txt_Notes.Text = "";
            ddl_Order_Source.SelectedIndex = 0;
            txt_Order_Abstractor_Cost.Text = "";
            txt_Legal_Reference.Text = "";
            txt_Order_No_Of_Pages.Text = "";
            txt_Order_Search_Cost.Text = "";
        }
        protected void Update_User_Order_Time_Info()
        {
            Get_maximum_Time_Id();
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            DateTime date1 = new DateTime();
            date1 = DateTime.Now;
            string dateeval1 = date1.ToString("dd/MM/yyyy");
            string time1 = date1.ToString("hh:mm tt");

            htComments.Add("@Trans", "UPDATE");
            htComments.Add("@Order_Time_Id", MAX_TIME_ID);
            htComments.Add("@End_Time", date1);
            htComments.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
            dtComments = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htComments);


        }
        protected void Get_maximum_Time_Id()
        {
            Hashtable htTime = new Hashtable();
            DataTable dtTime = new System.Data.DataTable();

            htTime.Add("@Trans", "MAX_TIME_ID");
            htTime.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
            htTime.Add("@Order_Id", Order_Id);
            htTime.Add("@User_Id", userid);
            dtTime = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htTime);
            if (dtTime.Rows.Count > 0)
            {
                MAX_TIME_ID = int.Parse(dtTime.Rows[0]["MAX_TIME_ID"].ToString());
              //  ViewState["MAX_TIME_ID"] = MAX_TIME_ID;

            }

        }
        protected void Geydview_Bind_Notes()
        {

            Hashtable htNotes = new Hashtable();
            DataTable dtNotes = new System.Data.DataTable();

            htNotes.Add("@Trans", "SELECT");
            htNotes.Add("@Order_Id", Order_Id);
            dtNotes = dataaccess.ExecuteSP("Sp_Order_Notes", htNotes);
            if (dtNotes.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_Error.Visible = true;
                grd_Error.DataSource = dtNotes;
              

            }
            else
            {
          



            }


        }
        private bool Validate_Order_Info()
        {

            if (ddl_order_Staus.SelectedIndex <= 0)
            {

               MessageBox.Show("Please Select Order Status");
                return false;
            }
            else
            {
                return true;

            }


        }
        bool ReturnValue()
        {
            return false;
        }
        private bool ValidateProductionDate()
        {
            DateTime dates = DateTime.Now;
            string dateeval1 = dates.ToString("MM/dd/yyyy");
            DateTime date1 = Convert.ToDateTime(dateeval1.ToString());

            if (txt_Prdoductiondate.Text != "")
            {
                date2 = Convert.ToDateTime(txt_Prdoductiondate.Text);
            }
            int result = DateTime.Compare(date1, date2);

            if (result >= 0)
            {


                return true;
            }
            else
            {
                MessageBox.Show("Date Enter Properly");
               
                return false;
            }
        }
        protected void Insert_OrderComments(object sender, EventArgs e)
        {

            if (txt_Comments.Text != "")
            {

                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");

                htComments.Add("@Trans", "INSERT");
                htComments.Add("@Order_Id", Order_Id);
                htComments.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                htComments.Add("@Comment", txt_Comments.Text);
                htComments.Add("@Inserted_By", userid);
                htComments.Add("@Inserted_date", date);
                htComments.Add("@Modified_By", userid);
                htComments.Add("@Modified_Date", date);
                htComments.Add("@status", "True");
                dtComments = dataaccess.ExecuteSP("Sp_Order_Comments", htComments);

                Geydview_Bind_Comments();

            }
        }
        protected void Insert_Order_Search_Cost(object sender, EventArgs e)
        {


            if (txt_Order_Search_Cost.Text != "") { SearchCost = Convert.ToDecimal(txt_Order_Search_Cost.Text.ToString()); } else { SearchCost = 0; }
            if (txt_Legal_Reference.Text != "") { Copy_Cost = Convert.ToDecimal(txt_Legal_Reference.Text.ToString()); } else { Copy_Cost = 0; }
            if (txt_Order_Abstractor_Cost.Text != "") { Abstractor_Cost = Convert.ToDecimal(txt_Order_Abstractor_Cost.Text.ToString()); } else { Abstractor_Cost = 0; }

            if (txt_Order_No_Of_Pages.Text != "") { No_Of_Pages = Convert.ToInt32(txt_Order_No_Of_Pages.Text.ToString()); } else { No_Of_Pages = 0; }
            Hashtable htsearch = new Hashtable();
            DataTable dtsearch = new System.Data.DataTable();

            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            string time = date.ToString("hh:mm tt");
            if (OPERATE_SEARCH_COST == "INSERT")
            {
                htsearch.Add("@Trans", "INSERT");
                htsearch.Add("@Order_Id", Order_Id);
                htsearch.Add("@Source", ddl_Order_Source.SelectedItem.ToString());
                htsearch.Add("@Search_Cost", SearchCost);
                htsearch.Add("@Copy_Cost", Copy_Cost);
                htsearch.Add("@Abstractor_Cost", Abstractor_Cost);
                htsearch.Add("@No_Of_pages", No_Of_Pages);
                htsearch.Add("@Inserted_By", userid);
                htsearch.Add("@Inserted_date", date);

                htsearch.Add("@status", "True");
                dtsearch = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htsearch);
            }
            else if (OPERATE_SEARCH_COST == "UPDATE")
            {

                htsearch.Add("@Trans", "UPDATE");
                htsearch.Add("@Order_Id", Order_Id);
                htsearch.Add("@Source", ddl_Order_Source.SelectedItem.ToString());
                htsearch.Add("@Search_Cost", SearchCost);
                htsearch.Add("@Copy_Cost", Copy_Cost);
                htsearch.Add("@Abstractor_Cost", Abstractor_Cost);
                htsearch.Add("@No_Of_pages", No_Of_Pages);

                htsearch.Add("@Modified_By", userid);
                htsearch.Add("@Modified_Date", date);
                htsearch.Add("@status", "True");
                dtsearch = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", htsearch);
            }
        }
        protected void Insert_ProductionDate(object sender, EventArgs e)
        {

            if (txt_Prdoductiondate.Text != "")
            {
                if (OPERATE_PRODUCTION_DATE == "INSERT")
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@Inserted_date", date);

                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htProductionDate);
                }
                else if (OPERATE_PRODUCTION_DATE == "UPDATE")
                {

                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");

                    Hashtable htProductionDate = new Hashtable();
                    DataTable dtproductiondate = new System.Data.DataTable();
                    htProductionDate.Add("@Trans", "INSERT");
                    htProductionDate.Add("@Order_Id", Order_Id);
                    htProductionDate.Add("@Order_Production_Date", txt_Prdoductiondate.Text);
                    htProductionDate.Add("@Order_Status_Id", SESSION_ORDER_TASK.ToString());
                    htProductionDate.Add("@Order_Progress_Id", int.Parse(ddl_order_Staus.SelectedValue.ToString()));
                    htProductionDate.Add("@Inserted_By", userid);
                    htProductionDate.Add("@Inserted_date", date);

                    htProductionDate.Add("@status", "True");
                    dtproductiondate = dataaccess.ExecuteSP("Sp_Order_ProductionDate", htProductionDate);
                }
            }
        }

        private void ddl_order_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ddl_order_Staus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_order_Staus.SelectedValue.ToString() == "3")
            {
                // Label81.Visible = true;
                ddl_order_Task.Visible = true;
                Chk_Self_Allocate.Visible = true;
                if (SESSSION_ORDER_TYPE == "Search")
                {
                    ddl_order_Task.Items.Insert(0, "Searh QC");
                    ddl_order_Task.Items.Insert(1, "Typing");
                    ddl_order_Task.Items.Insert(2, "Upload");
                    ddl_order_Task.Items.Insert(3, "Upload Completed");
                }
                if (SESSSION_ORDER_TYPE == "Searh QC")
                {
                    ddl_order_Task.Items.Insert(0, "Typing");
                    ddl_order_Task.Items.Insert(1, "Upload");
                    ddl_order_Task.Items.Insert(2, "Upload Completed");
                }
                if (SESSSION_ORDER_TYPE == "Type")
                {
                    ddl_order_Task.Items.Insert(0, "Typing QC");
                    ddl_order_Task.Items.Insert(1, "Upload");
                    ddl_order_Task.Items.Insert(2, "Upload Completed");
                }
                if (SESSSION_ORDER_TYPE == "Typing QC")
                {
                    ddl_order_Task.Items.Insert(0, "Upload");
                    ddl_order_Task.Items.Insert(1, "Upload Completed");
                }
                if (SESSSION_ORDER_TYPE == "Upload")
                {
                    ddl_order_Task.Items.Insert(0, "Upload Completed");
                }
            }
            else if (ddl_order_Staus.SelectedValue.ToString() == "1" || ddl_order_Staus.SelectedValue.ToString() == "5" || ddl_order_Staus.SelectedValue.ToString() == "4" || ddl_order_Staus.SelectedValue.ToString() == "9")
            {
                //txt_UserName.Text = "";
                //txt_Password.Text = "";
                //ModalPopupExtender1.Show();
                //  btn_submit.Enabled = false;

                ddl_order_Task.Visible = false;
            }
            else
            {
                btn_submit.Enabled = true;

                ddl_order_Task.Visible = false;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
