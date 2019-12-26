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

namespace Ordermanagement_01
{
    public partial class Order_Check_List : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();
        int userid = 0, Task, Task_Confirm_Id, Order_Status, Order_ID,Check;
        bool confirmed;
        string Preview;
        string Node_Type;
        string Option_Message, Reason_Message;
        int Option_Id, Reason_Id;
        int Task_Sub_Id, Task_Child_Id;
        int Check_Sub,Check_Child;
        string Operation;
        int Check_List_Count;
        string Pop_Op_Value;
        public Order_Check_List(int Task_Confirm_ID,int user_id,int ORDER_ID,int ORDER_STTAUS,int TASK_SUB_ID,int TASK_CHILD_ID,string NODETYPE,string OPERATION)
        {
            InitializeComponent();
            Order_ID = ORDER_ID;
            Order_Status = ORDER_STTAUS;
            Task_Confirm_Id = Task_Confirm_ID;
            Task_Sub_Id = TASK_SUB_ID;
            Task_Child_Id = TASK_CHILD_ID;
            userid = user_id;
            Node_Type = NODETYPE.ToString();
            Operation = OPERATION;

        }



        private void Order_Check_List_Load(object sender, EventArgs e)
        {
            lbl_Reason.Visible = false;
            txt_Reason.Visible = false;
            group_Option.Visible = false;
            group_Reason.Visible = false;
            
                Load_Task_Details_Before();
            
        }

        public void Load_Task_Details_Before()
        {



            if (Node_Type == "Parent")
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                htComments.Add("@Trans", "SELECT_BY_PARENT_TASK_CONFIRM_ID");
                htComments.Add("@Task_Confirm_Id", Task_Confirm_Id);
                dtComments = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htComments);

                if (dtComments.Rows.Count > 0)
                {

                    lbl_Message.Text = dtComments.Rows[0]["Confirmation_Message"].ToString();
                    Node_Type = dtComments.Rows[0]["NodeType"].ToString();

                }
            }
            else if (Node_Type == "Sub")
            {

                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                htComments.Add("@Trans", "SELECT_BY_SUB_TASK_CONFIRM_ID");
                htComments.Add("@Task_Confirm_Id", Task_Confirm_Id);
                htComments.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                dtComments = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htComments);

                if (dtComments.Rows.Count > 0)
                {

                    lbl_Message.Text = dtComments.Rows[0]["Confirmation_Message"].ToString();
                    Node_Type = dtComments.Rows[0]["Node_Type"].ToString();

                }
            }
            else if (Node_Type == "Child")
            {
                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                htComments.Add("@Trans", "SELECT_BY_CHILD_TASK_CONFIRM_ID");
                htComments.Add("@Task_Confirm_Id", Task_Confirm_Id);
                htComments.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htComments.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                dtComments = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htComments);

                if (dtComments.Rows.Count > 0)
                {

                    lbl_Message.Text = dtComments.Rows[0]["Confirmation_Message"].ToString();
                    Node_Type = dtComments.Rows[0]["NodeType"].ToString();

                }

            }

        }

        public void Check_Parent_Sub_Chld()
        {
             Hashtable htchecklist = new Hashtable();
            DataTable dtcecklist = new DataTable();
            htchecklist.Add("@Trans", "SELECT_BEFORE");
            htchecklist.Add("@Order_Status_Id",Order_Status);
            htchecklist.Add("@Order_ID", Order_ID);

            dtcecklist = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htchecklist);


         
                Check_List_Count = int.Parse(dtcecklist.Rows.Count.ToString());
                if (Check_List_Count > 0)
                {

                

            Hashtable htsubcount = new Hashtable();
            DataTable dtsubcount = new DataTable();

            htsubcount.Add("@Trans", "GET_COUNT_TASK_CONFIRM_ID");
            htsubcount.Add("@Order_ID",Order_ID);
            htsubcount.Add("@Order_Status_Id", Order_Status);
            dtsubcount = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htsubcount);
            int count = int.Parse(dtsubcount.Rows[0]["count"].ToString());

            Hashtable htget_Parent = new Hashtable();
            DataTable dtget_Partent = new DataTable();
            if (count == 0)
            {
                htget_Parent.Add("@Trans", "GET_ORDER_WISE_TASK_ID");
            }
            else if (count > 0)
            {
                htget_Parent.Add("@Trans", "GET_NOT_ENTERED_ORDER_WISE_TASK_CONFIRM_ID");

            }
            htget_Parent.Add("@Order_ID", Order_ID);
            htget_Parent.Add("@Order_Status_Id", Order_Status);
            dtget_Partent = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_Parent);

            if (dtget_Partent.Rows.Count > 0)
            {

                Hashtable htget_enteredsub = new Hashtable();
                DataTable dtget_enteredsub = new DataTable();
                dtget_enteredsub.Rows.Clear();
                htget_enteredsub.Add("@Trans", "GET_ENTERED_SUB_ID");
                htget_enteredsub.Add("@Task_Confirm_Id", dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString());
                htget_enteredsub.Add("@Order_ID", Order_ID);
                htget_enteredsub.Add("@Order_Status_Id", Order_Status);
                dtget_enteredsub = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_enteredsub);

                if (dtget_enteredsub.Rows.Count > 0)
                {
                    Hashtable htget_child = new Hashtable();
                    DataTable dtget_child = new DataTable();
                    htget_child.Add("@Trans", "GET_ALL_CHILD_QUESTION_ON_TASK_SUB_ID");
                    htget_child.Add("@Task_Confirm_Id", dtget_enteredsub.Rows[0]["Task_Confirm_Id"].ToString());
                    htget_child.Add("@Task_Confirm_Sub_Id", int.Parse(dtget_enteredsub.Rows[0]["Task_Confirm_Sub_Id"].ToString()));
                    htget_child.Add("@Order_ID", Order_ID);
                    htget_child.Add("@Order_Status_Id", Order_Status);
                    dtget_child = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_child);

                    if (dtget_child.Rows.Count > 0)
                    {
                        Order_Check_List chk = new Order_Check_List(int.Parse(dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID, Order_Status, int.Parse(dtget_enteredsub.Rows[0]["Task_Confirm_Sub_Id"].ToString()), int.Parse(dtget_child.Rows[0]["Task_Confirm_Child_Id"].ToString()), "Child", "Pop_Old");
                        chk.Show();


                    }

                }

                else
                {
                    Hashtable htget_sub = new Hashtable();
                    DataTable dtget_sub = new DataTable();
                    htget_sub.Add("@Trans", "GET_ALL_SUB_QUESION_ON_TASK_CONFIRM_ID");
                    htget_sub.Add("@Task_Confirm_Id", dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString());
                    htget_sub.Add("@Order_ID", Order_ID);
                    htget_sub.Add("@Order_Status_Id", Order_Status);
                    dtget_sub = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htget_sub);

                    if (dtget_sub.Rows.Count > 0)
                    {

                        Order_Check_List chk = new Order_Check_List(int.Parse(dtget_Partent.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID, Order_Status, int.Parse(dtget_sub.Rows[0]["Task_Confirm_Sub_Id"].ToString()), 0, "Sub", "Pop_Old");
                        chk.Show();
                    }
                }


            }
            else
            {

                Check_List_Count = int.Parse(dtcecklist.Rows.Count.ToString());
                Order_Check_List chk = new Order_Check_List(int.Parse(dtcecklist.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID, Order_Status, 0, 0, "Parent", "Pop_New");
                chk.Show();
            }
                    }
                    //else
                    //{
                    
                    //     Check_List_Count = int.Parse(dtcecklist.Rows.Count.ToString());
                    //        Order_Check_List chk = new Order_Check_List(int.Parse(dtcecklist.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID,Order_Status, 0, 0, "Parent", "Pop_New");
                    //        chk.Show();
                    //}




        }

        private bool validate()
        {

            if (rbtn_Yes.Checked == false && rbtn_No.Checked == false)
            {

                MessageBox.Show("Please Select Yes or No");
                rbtn_Yes.Focus();
                return false;
            }
            if (txt_Reason.Visible == true && txt_Reason.Text == "")
            {

                MessageBox.Show("Please Enter Reason");
                txt_Reason.Focus();
                return false;
            }

            if (ddl_Option.Visible == true && ddl_Option.SelectedIndex <= 0)
            {

                MessageBox.Show("Please select Option");
                ddl_Option.Focus();
                return false;
            }
            if (ddl_Reason.Visible == true && ddl_Reason.SelectedIndex <= 0)

            {
                MessageBox.Show("Please select Reason");
                ddl_Reason.Focus();
                return false;
            }
            
            return true;

        }
        private void btn_validate_Click(object sender, EventArgs e)
        {

            if (validate() != false)
            {
                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Clear();
                dtcheck.Clear();
                if (Node_Type == "Parent")
                {
                    htcheck.Add("@Trans", "CHECK_PARENT");
                }
               
               
                htcheck.Add("@Order_ID", Order_ID);
                htcheck.Add("@Order_Status_Id", Order_Status);
                htcheck.Add("@Task_Confirm_Id", Task_Confirm_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htcheck);

                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["Count"].ToString());
                }

                if (Check == 0)
                {

                  
                    Hashtable hsforSP = new Hashtable();
                    DataTable dt = new System.Data.DataTable();
                    hsforSP.Clear();
                    dt.Clear();

                    if (Node_Type == "Parent")
                    {
                        hsforSP.Add("@Task_Confirm_Id", Task_Confirm_Id);
                        hsforSP.Add("@Node_Type","Parent");
                    }
                    else if (Node_Type == "Sub")
                    {
                        hsforSP.Add("@Task_Confirm_Id", Task_Confirm_Id);
                        hsforSP.Add("@Task_Confirm_Sub_Id",Task_Sub_Id);
                        hsforSP.Add("@Node_Type", "Sub");

                    }
                    else if (Node_Type == "Child")
                    {

                        hsforSP.Add("@Task_Confirm_Id", Task_Confirm_Id);
                        hsforSP.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                        hsforSP.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                        hsforSP.Add("@Node_Type", "Child");

                    }

                    if (ddl_Option.SelectedIndex > 0)
                    {
                        Option_Id = int.Parse(ddl_Option.SelectedValue.ToString());

                    }
                    else
                    {

                        Option_Id = 0;
                    }
                    if (ddl_Reason.SelectedIndex > 0)
                    {

                        Reason_Id = int.Parse(ddl_Reason.SelectedValue.ToString());
                    }
                    else
                    {

                        Reason_Id = 0;
                    }
                    hsforSP.Add("@Trans", "INSERT");
                    hsforSP.Add("@Order_ID", Order_ID);
                    hsforSP.Add("@Order_Status_Id", Order_Status);
                   
                    hsforSP.Add("@Confirmed",confirmed);
                 
                    hsforSP.Add("@Option_Id", Option_Id);
                    if (Reason_Id != 0)
                    {
                        hsforSP.Add("@Reason_Id",Reason_Id);

                    }
                    else
                    {
                        hsforSP.Add("@Reason", txt_Reason.Text);
                    }
                    hsforSP.Add("@User_Id", userid);
                    hsforSP.Add("@EnteredDate", DateTime.Now);
                    hsforSP.Add("@status", "True");
                    hsforSP.Add("@Inserted_By", userid);
                    hsforSP.Add("@Instered_Date", DateTime.Now);
                    dt = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", hsforSP);
                    this.Close();
                 //   Check_Parent_Sub_Chld();
                   Populate_SubQuestion();
                   
                }

            }




        }

        private void Populate_List()
        
        { 
        
        
            Hashtable htchecklist = new Hashtable();
            DataTable dtcecklist = new DataTable();
            htchecklist.Add("@Trans", "SELECT_BEFORE");
            htchecklist.Add("@Order_Status_Id",Order_Status);
            htchecklist.Add("@Order_ID", Order_ID);

            dtcecklist = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htchecklist);

            if (dtcecklist.Rows.Count > 0)
            {

                Order_Check_List chk = new Order_Check_List(int.Parse(dtcecklist.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID, Order_Status,0,0,"Parent","Pop_New");
                chk.Show();
            }
        }

        public void  Populate_SubQuestion()
        {

            Hashtable htgetsub = new Hashtable();
            DataTable dtgetsub = new DataTable();


            Hashtable htchecklist = new Hashtable();
            DataTable dtcecklist = new DataTable();



            htgetsub.Add("@Trans", "SELECT_SUB");
            htgetsub.Add("@Order_Status_Id",Order_Status);
            htgetsub.Add("@Task_Confirm_Id", Task_Confirm_Id);
            dtgetsub = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htgetsub);
            if (dtgetsub.Rows.Count > 0)
            {

                Task_Sub_Id = int.Parse(dtgetsub.Rows[0]["Task_Confirm_Sub_Id"].ToString());
            }
            else
            {

                Task_Sub_Id = 0;
            }



            if (Task_Sub_Id != 0)
            {
                htchecklist.Add("@Trans", "SELECT_BEFORE_SUB");
                htchecklist.Add("@Order_Status_Id", Order_Status);
                htchecklist.Add("@Task_Confirm_Id", Task_Confirm_Id);
                htchecklist.Add("@Question_PoPulate",Pop_Op_Value);
                htchecklist.Add("@Order_ID", Order_ID);

                dtcecklist = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htchecklist);

                if (dtcecklist.Rows.Count > 0)
                {

                    Order_Check_List chk = new Order_Check_List(int.Parse(dtcecklist.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID, Order_Status, int.Parse(dtcecklist.Rows[0]["Task_Confirm_Sub_Id"].ToString()), 0, "Sub", "Pop_New");
                    chk.Show();
                }
                else
                {

                    Populate_Child_Quesion();
                }


            }
            else
            {

                Populate_List();
            }

        }
        public void Populate_Child_Quesion()
        {

            Hashtable htchecklistparent = new Hashtable();
            DataTable dtcecklistparent = new DataTable();
            htchecklistparent.Add("@Trans", "SELECT_BEFORE");
            htchecklistparent.Add("@Order_Status_Id", Order_Status);
            htchecklistparent.Add("@Order_ID", Order_ID);

            dtcecklistparent = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htchecklistparent);

            if(dtcecklistparent.Rows.Count>0)
            {

            Hashtable htgetsub = new Hashtable();
            DataTable dtgetsub = new DataTable();


            Hashtable htchecklist = new Hashtable();
            DataTable dtcecklist = new DataTable();



            htgetsub.Add("@Trans", "SELECT_CHILD");
            htgetsub.Add("@Order_Status_Id", Order_Status);
            htgetsub.Add("@Task_Confirm_Id", Task_Confirm_Id);
            htgetsub.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
            dtgetsub = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htgetsub);
            if (dtgetsub.Rows.Count > 0)
            {

                Task_Child_Id = int.Parse(dtgetsub.Rows[0]["Task_Confirm_Sub_Id"].ToString());
            }
            else
            {

                Task_Child_Id = 0;
            }



            if (Task_Child_Id != 0)
            {
                htchecklist.Add("@Trans", "SELECT_BEFORE_CHILD");
                htchecklist.Add("@Order_Status_Id", Order_Status);
                htchecklist.Add("@Task_Confirm_Id", Task_Confirm_Id);
                htchecklist.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htchecklist.Add("@Question_PoPulate",Pop_Op_Value);
                htchecklist.Add("@Order_ID", Order_ID);

                dtcecklist = dataaccess.ExecuteSP("Sp_Order_Task_Confirmation", htchecklist);

                if (dtcecklist.Rows.Count > 0)
                {

                    Order_Check_List chk = new Order_Check_List(int.Parse(dtcecklist.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID, Order_Status, int.Parse(dtcecklist.Rows[0]["Task_Confirm_Sub_Id"].ToString()), int.Parse(dtcecklist.Rows[0]["Task_Confirm_Child_Id"].ToString()), "Child", "Pop_New");
                    chk.Show();
                }
                else
                {


                    Order_Check_List chk = new Order_Check_List(int.Parse(dtcecklistparent.Rows[0]["Task_Confirm_Id"].ToString()), userid, Order_ID, Order_Status, 0, 0, "Parent", "Pop_New");
                    chk.Show();

                }
            }

            }
        

        }
        private void rbtn_Yes_CheckedChanged(object sender, EventArgs e)
        {
            lbl_Reason.Visible = false;
            txt_Reason.Visible = false;
            group_Option.Visible = false;
            group_Reason.Visible = false;
            confirmed = true;
            txt_Reason.Text = "";
            Pop_Op_Value = "Yes";
        }

        private void rbtn_No_CheckedChanged(object sender, EventArgs e)
        {
            Hashtable htoptionReason = new Hashtable();
            DataTable dtoptionreason = new DataTable();
            Hashtable htoption = new Hashtable();
            DataTable dtoption = new DataTable();
            Hashtable htReason = new Hashtable();
            DataTable dtreason = new DataTable();
            htoptionReason.Clear();
            dtoptionreason.Clear();
            htoption.Clear();
            dtoption.Clear();
            htReason.Clear();
            dtreason.Clear();
            Pop_Op_Value = "No";
            group_Reason.Visible = true;
         
                if (Node_Type == "Parent")
                {
                    htoptionReason.Add("@Trans", "GET_REASON_OPTION_PARENT");
                    htoptionReason.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htoptionReason.Add("@Node_Type", "Parent");
                }
                else if (Node_Type == "Sub")
                {

                    htoptionReason.Add("@Trans", "GET_REASON_OPTION_SUB");
                    htoptionReason.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htoptionReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                    htoptionReason.Add("@Node_Type", "Sub");
                }
                else if (Node_Type == "Child")
                {

                    htoptionReason.Add("@Trans", "GET_REASON_OPTION_CHILD");
                    htoptionReason.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htoptionReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                    htoptionReason.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                    htoptionReason.Add("@Node_Type", "Child");
                }
               
               
                dtoptionreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htoptionReason);
                if (dtoptionreason.Rows.Count > 0)
                {

                    Option_Message = dtoptionreason.Rows[0]["Option_Message"].ToString();
                    Reason_Message = dtoptionreason.Rows[0]["Reason_Message"].ToString();

                    lbl_Option_Header.Text = Option_Message.ToString();

                    lbl_Reason_Header.Text = Reason_Message.ToString();
                }

                else
                {
                    Option_Message = "";

                    Reason_Message = "";
                }

                if (Option_Message == "") { lbl_Option_Header.Visible = false; }
                else {lbl_Option_Header.Visible = true; }

                if (Reason_Message == "") { lbl_Reason_Header.Visible = false; } else { lbl_Reason_Header.Visible = true; }

                if (Node_Type == "Parent")
                {
                    htoption.Add("@Trans", "GET_OPTION_PARENT");
                    htoption.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htoption.Add("@Node_Type", "Parent");
                }
                else if (Node_Type == "Sub")
                {
                    htoption.Add("@Trans", "GET_OPTION_SUB");
                    htoption.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                    htoption.Add("@Node_Type", "Sub");

                }
                else if (Node_Type == "Child")
                {

                    htoption.Add("@Trans", "GET_OPTION_CHILD");
                    htoption.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                    htoption.Add("@Task_Confirm_Child_Id", Task_Child_Id);  
                    htoption.Add("@Node_Type", "Child");
                }
                dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Option", htoption);
                if (dtoption.Rows.Count > 0)
                {
                    group_Option.Visible = true;
                    ddl_Option.Visible = true;
                    DataRow dr = dtoption.NewRow();
                    dr[0] = 0;
                    dr[1] = "Select";
                    dtoption.Rows.InsertAt(dr, 0);
                    ddl_Option.DataSource = dtoption;
                    ddl_Option.DisplayMember = "Option_Name";
                    ddl_Option.ValueMember = "Option_Id";
                }
                else
                {
                    group_Option.Visible = false;
                    ddl_Option.Visible = false;
                
                }

                if (Node_Type == "Parent")
                {
                    htReason.Add("@Trans", "GET_REASON_PARENT");
                    htReason.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htReason.Add("@Node_Type", "Parent");
                }
                else if(Node_Type=="Sub")
                {
                    htReason.Add("@Trans", "GET_REASON_SUB");
                    htReason.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                    htReason.Add("@Node_Type", "Sub");

                }
                else if (Node_Type == "Sub")
                {
                    htReason.Add("@Trans", "GET_REASON_SUB");
                    htReason.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    htReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                    htReason.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                    htReason.Add("@Node_Type", "Child");

                }
                dtreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason", htReason);
                if (dtreason.Rows.Count > 0)
                {
                    txt_Reason.Visible = false;
                    ddl_Reason.Visible = true;
                    DataRow dr = dtreason.NewRow();
                    dr[0] = 0;
                    dr[1] = "Select";
                    dtreason.Rows.InsertAt(dr, 0);
                    ddl_Reason.DataSource = dtreason;
                    ddl_Reason.DisplayMember = "Reason_Name";
                    ddl_Reason.ValueMember = "Reason_Id";
                }
                else
                {
                    txt_Reason.Visible = true;
                    ddl_Reason.Visible = false;
                }





       
            
            //lbl_Reason.Visible = true;
            ////txt_Reason.Visible = true;
            //confirmed = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
