using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Ordermanagement_01.InvoiceRep
{
    public partial class Order_Cost_Details : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();
        string Order_Target, Time_Zone, Recived_Time;
        int Order_Id = 0;
        int userid;
        string Empname;
        int Count;
        int BRANCH_ID;
        int No_Of_Orders;
        int client_Id, Subprocess_id;
        string User_Role_Id;
        string OrderStatus;
        int Chk_Userid;
        string MAX_ORDER_NUMBER;
        int chk_Order_no;
        double zipcode;
        decimal SearchCost, Copy_Cost, Abstractor_Cost;
        int No_Of_Pages;
        int Chk_Order_Search_Cost;
        string OPERATE_SEARCH_COST;
        int SubprocessId, ClientId;
        decimal Totalcost;
        int Autoinvoice_No;
        int Invoice_No;
        decimal check_Ordercost;
        decimal Orders_Cost, Invoice_Copy_Cost;
        string Invoice_Number;
        string Operation;
        string Inv_Num;
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        public Order_Cost_Details(int Orderid, int User_Id, string OPERATION,string USER_ROLE_ID)
        {
            InitializeComponent();

           
            userid = User_Id;
            Operation = OPERATION;
            Order_Id = Orderid;
            User_Role_Id = USER_ROLE_ID;
          
            if (User_Role_Id=="1")
            {
              
              
                 dbc.BindClientName(ddl_Client_Search);
                 int index = ddl_ClientName.FindString("NETCO");

                 ddl_ClientName.SelectedIndex = index;
             
               
            }
            else
            {
                 dbc.BindClientName_For_Order_Cost(ddl_Client_Search);

                 string val = "8";
                 ddl_ClientName.ValueMember = val.ToString();
                ddl_ClientName.Visible = false;
                ddl_SubProcess.Visible = false;
                lbl_Client.Visible = false;
                lbl_Subprocess.Visible = false;
            }
         
            txt_Production_Date.Text = DateTime.Now.ToString();

            if (Order_Id != 0)
            {
                if (Operation == "Insert")
                {
                    load_order_masters();
                    Order_Load();
                    dbc.Bind_Client_Email(ddl_Client_Email,client_Id);
                    txt_Invoice_Order_Number.Visible = true;
                    lbl_Enter_Order.Visible = true;
                    Group_Order_Numbers.Enabled = true;
                   
                    txt_Order_Cost_Date.Text = DateTime.Now.ToString();

                    txt_Production_Date.Text = DateTime.Now.ToString();
                }
                else if (Operation == "Update")
                {
                    txt_Invoice_Order_Number.Visible = false;
                    lbl_Enter_Order.Visible = false;
                  
                    btn_Save.Text = "Edit order Cost";
                    Group_Order_Numbers.Enabled = false;
                
                    load_order_masters();
                    Order_Load();
                    dbc.Bind_Client_Email(ddl_Client_Email, client_Id);
                    Load_order_Cost_Details();
                   
                }


            }
            bind_OrderList_On_Load();
        }
        public void load_order_masters()
        {

            if (User_Role_Id == "1")
            {
                dbc.BindClientName(ddl_Client_Search);
            }
            else 
            {
                dbc.BindClientName_For_Order_Cost(ddl_Client_Search);
            }

            dbc.BindClientName(ddl_ClientName);
            dbc.BindOrderType(ddl_ordertype);

            dbc.BindState(ddl_State);
           // client_Id = int.Parse(ddl_ClientName.SelectedValue.ToString());

            ddl_ordertask.Visible = true;
            ddl_ordertask.Items.Insert(0, "Search");
            ddl_ordertask.Items.Insert(1, "Typing");
            ddl_ordertask.Items.Insert(2, "Upload");
            ddl_ordertask.Items.Insert(3, "Upload Completed");
            ddl_ordertask.Items.Insert(4, "Abstractor");
            //   ddl_Search_Type.Visible = true;
            // ddl_Search_Type.Items.Insert(0, "SELECT");
            //ddl_Search_Type.Items.Insert(0, "TIER 1");
            //ddl_Search_Type.Items.Insert(1, "TIER 2");
            //ddl_Search_Type.Items.Insert(2, "TIER 2-In house");

            //   ddl_Order_Source.Items.Insert(0, "SELECT");
            ddl_Order_Source.Items.Insert(0, "Online");
            ddl_Order_Source.Items.Insert(1, "Subscription");
            ddl_Order_Source.Items.Insert(2, "Plant");
            ddl_Order_Source.Items.Insert(3, "Abstractor");
            ddl_Order_Source.Items.Insert(4, "Online/Abstractor");
            //Order_Controls_Load();


            // SetMyCustomFormat();


            if (ddl_ClientName.SelectedIndex != 0)
            {
                dbc.BindSubProcessName(ddl_SubProcess, client_Id);
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
        }

    
        public void Load_order_Cost_Details()
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT");
            ht.Add("@Order_ID", Order_Id);
            dt = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", ht);
            if (dt.Rows.Count > 0)
            {

                txt_Order_Cost.Text = dt.Rows[0]["Order_Cost"].ToString();

                if (dt.Rows[0]["Order_Cost_Date"].ToString() != "")
                {
                    txt_Order_Cost_Date.Text = dt.Rows[0]["Order_Cost_Date"].ToString();
                }
                else
                {


                    {


                        txt_Order_Cost_Date.Text = "";

                    }
                }
                txt_order_comments.Text = dt.Rows[0]["Comments"].ToString();
                ddl_Client_Email.SelectedValue = dt.Rows[0]["Default_Cleint_Email_Id"].ToString();
               

            }


        }

        private void Order_Cost_Details_Load(object sender, EventArgs e)
        {
            if (Order_Id != 0)
            {

          
                
                int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
                ddl_Client_Search_SelectedIndexChanged(sender, e);
                if (ddl_ClientName.SelectedIndex != 0)
                {
                    //  int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                    // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                    ddl_SubProcess.Focus();

                }

                Order_Load();
                if (User_Role_Id=="1" || User_Role_Id=="6")
                { 
                
                }
                else
                {

                    txt_Order_Cost.Enabled = false;
                    txt_Order_Cost.PasswordChar = '*';
                }


            }

         

        }
        private void ddl_Client_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_OrderList();
        }
        private void Order_Load()
        {
            if (Order_Id != 0)
            {


                Control_Enable_false();

                Hashtable ht_Select_Order_Details = new Hashtable();
                DataTable dt_Select_Order_Details = new DataTable();

                ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_WISE");
                ht_Select_Order_Details.Add("@Order_ID", Order_Id);
                dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);

                if (dt_Select_Order_Details.Rows.Count > 0)
                {
                    //ViewState["Orderid"] = order_Id.ToString();
                    //Session["order_id"] = order_Id.ToString();
                    txt_OrderNumber.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                    txt_Date.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();

                    ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                    ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                 
                    
                     client_Id = int.Parse(ddl_ClientName.SelectedValue.ToString());
                     dbc.BindSubProcessName(ddl_SubProcess, client_Id);
                    

                    ddl_SubProcess.SelectedValue = dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString();

                    Subprocess_id = int.Parse(ddl_SubProcess.SelectedValue.ToString());

              

                    dbc.Bind_Client_Email(ddl_Client_Email, client_Id);
                    txt_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                    txt_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                    txt_City.Text = dt_Select_Order_Details.Rows[0]["City"].ToString();
                    txt_Zip.Text = dt_Select_Order_Details.Rows[0]["Zip"].ToString();
                    ddl_State.SelectedValue = dt_Select_Order_Details.Rows[0]["stateid"].ToString();
                    //ddl_Search_Type.SelectedItem = lblSearch_Type.Text;
                    //  ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                    //  ddl_Search_Type.Text = dt_Select_Order_Details.Rows[0]["Search_Type"].ToString();
                    txt_Client_order_ref.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Ref"].ToString();
                    lbl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();
                    ddl_Hour.Text = dt_Select_Order_Details.Rows[0]["HH"].ToString();
                    ddl_Minute.Text = dt_Select_Order_Details.Rows[0]["MM"].ToString();
                    ddl_Sec.Text = dt_Select_Order_Details.Rows[0]["SS"].ToString();
                    if (ddl_State.SelectedIndex > 0)
                    {
                        int stateid = int.Parse(ddl_State.SelectedValue.ToString());
                        dbc.BindCounty(ddl_County, stateid);

                    }
                    if (dt_Select_Order_Details.Rows[0]["CountyId"].ToString() != "0")
                    {
                        ddl_County.SelectedValue = dt_Select_Order_Details.Rows[0]["CountyId"].ToString();
                    }
                    else
                    {
                        ddl_County.SelectedValue = "SELECT";
                    }
                    ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                    txt_Borrowername.Text = dt_Select_Order_Details.Rows[0]["Borrower_Name"].ToString();
                    ddl_Client_Email.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Email_Id"].ToString();
                    txt_Notes.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();
                    txt_Order_Cost_Date.Text = DateTime.Now.ToString();


                }

            }


        }
        private void bind_OrderList_On_Load()
        {

            dt.Clear();
            Hashtable ht = new Hashtable();
          
           
         
           
                ht.Add("@Trans", "GET_ORDERS_LIST_MONTHLY_WISE");

            
            ht.Add("@Client_Id", 8);
        


            dt = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", ht);

            if (dt.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Invoice_Details.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Grid_Invoice_Details.Rows.Add();

                    Grid_Invoice_Details.Rows[i].Cells[0].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                    Grid_Invoice_Details.Rows[i].Cells[1].Value = dt.Rows[i]["Order_ID"].ToString();

                }
            }
            else
            {
                Grid_Invoice_Details.Rows.Clear();
                Grid_Invoice_Details.DataSource = null;
                // lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }

        }


        private void bind_OrderList()
        {
            load_Progressbar.Start_progres();

            dt.Clear();

            Hashtable ht = new Hashtable();

            if (ddl_Client_Search.SelectedIndex > 0 && txt_Production_Date.Text != "")
            {
                if (rbtn_Month_Wise.Checked == true)
                {

                    ht.Add("@Trans", "GET_ORDERS_LIST_MONTHLY_WISE");
                }
                else if (rbtn_Date_Wise.Checked == true)
                {

                    ht.Add("@Trans", "GET_ORDERS_LIST");
                }

                ht.Add("@Production_Date",Convert.ToDateTime(txt_Production_Date.Text));
                ht.Add("@Client_Id", int.Parse(ddl_Client_Search.SelectedValue.ToString()));



                dt = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", ht);

                if (dt.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    Grid_Invoice_Details.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Grid_Invoice_Details.Rows.Add();

                        Grid_Invoice_Details.Rows[i].Cells[0].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[1].Value = dt.Rows[i]["Order_ID"].ToString();

                    }
                }
                else
                {
                    Grid_Invoice_Details.Rows.Clear();
                    Grid_Invoice_Details.DataSource = null;
                    // lbl_Total_Orders.Text = "0";
                    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                    //grd_Admin_orders.DataBind();
                }
            }

        }
       

        private void Control_Enable_false()
        {
            ddl_ClientName.Enabled = false;
            ddl_SubProcess.Enabled = false;
            ddl_Hour.Enabled = false;
            ddl_Minute.Enabled = false;
            ddl_Sec.Enabled = false;
            txt_Date.Enabled = false;
            ddl_ordertype.Enabled = false;
            txt_OrderNumber.ReadOnly = true;
            txt_APN.ReadOnly = true;
            txt_Client_order_ref.ReadOnly = true;
            txt_Borrowername.ReadOnly = true;
            txt_Address.ReadOnly = true;
            ddl_State.Enabled = false;
            ddl_County.Enabled = false;
            txt_City.ReadOnly = true;
            txt_Zip.ReadOnly = true;
            ddl_ordertask.Enabled = false;
            //  ddl_Search_Type.Enabled = false;
            ddl_Order_Source.Enabled = false;
            txt_Search_cost.ReadOnly = true;
            txt_Copy_cost.ReadOnly = true;
            txt_Abstractor_Cost.ReadOnly = true;
            txt_noofpage.ReadOnly = true;
            txt_Notes.ReadOnly = true;

        }

        private void txt_Invoice_Order_Number_TextChanged(object sender, EventArgs e)
        {
            string Order_Number = txt_Invoice_Order_Number.Text;
            if (Order_Number != "")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "GET_ORDER_ID");
                ht.Add("@Client_Order_Number", Order_Number);
                dt = dataaccess.ExecuteSP("Sp_Order", ht);

                if (dt.Rows.Count > 0)
                {
                    Order_Id = int.Parse(dt.Rows[0]["Order_ID"].ToString());
               
                    load_order_masters();
                 
                    Order_Load();
                    Load_Order_County_Cost();
                    if (userid == 1 || userid == 4 || userid == 99)
                    {

                    }
                    else
                    {

                        txt_Order_Cost.Enabled = false;
                        txt_Order_Cost.PasswordChar = '*';
                    }

                  
                }

           

            }
        }
            public void Load_Order_County_Cost()
        { 
          Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "GET_COUNTY_WISE_COST");
            ht.Add("@Client_Id", client_Id);
            ht.Add("@Sub_Process_Id", Subprocess_id);
            ht.Add("@State_Id", ddl_State.SelectedValue.ToString());
            ht.Add("@County_Id",ddl_County.SelectedValue.ToString());
            ht.Add("@Order_Type_Id", ddl_ordertype.SelectedValue.ToString());
            dt = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht);
            if (dt.Rows.Count > 0)
            {

                txt_Order_Cost.Text = dt.Rows[0]["Order_Cost"].ToString();
            }
            else
            {

                txt_Order_Cost.Text = "0";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.SelectedValue.ToString(), ddl_SubProcess.SelectedValue.ToString());
            Orderuploads.Show();
        }
        public bool Validate_Invoice()
        {


            if (txt_Order_Cost.Text == "")
            {

                MessageBox.Show("Please Enter Order Cost");
                txt_Order_Cost.Focus();
                return false;
            }
         
            if (txt_Order_Cost_Date.Text == "")
            {

                MessageBox.Show("Please enter Order Cost Date");
                txt_Order_Cost_Date.Focus();
                return false;
            }
            if (ddl_Client_Email.SelectedIndex <= 0)
            {
                MessageBox.Show("Please Select Client Email");
                ddl_Client_Email.Focus();
                return false;

            }
            if (txt_Order_Cost.Text != "")
            {

                check_Ordercost = Convert.ToDecimal(txt_Order_Cost.Text);
                if (check_Ordercost == 0)
                {

                    MessageBox.Show("Order Cost Should not be 0 and Blank");
                    return false;
                }
                else
                {

                    return true;
                }


            }
            return true;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (Validate_Invoice() != false && btn_Save.Text == "Genrate Order Cost"  )
            {



                if (txt_Order_Cost.Text != "")
                {

                    Orders_Cost = Convert.ToDecimal(txt_Order_Cost.Text.ToString());

                }
                else
                {
                    Orders_Cost = 0;

                }

               



                //string nnn = no.ToString();
                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Order_ID", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htcheck);
                int check = int.Parse(dtcheck.Rows[0]["Count"].ToString());
                if (check == 0)
                {

                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();

                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Order_ID", Order_Id);
                    ht.Add("@Client_Id", client_Id);
                    ht.Add("@Subprocess_ID", Subprocess_id);
                    ht.Add("@Order_Cost", Orders_Cost);
                    ht.Add("@Order_Cost_Date", txt_Order_Cost_Date.Text);
                    ht.Add("@Comments", txt_order_comments.Text);
                    ht.Add("@Default_Cleint_Email_Id",int.Parse(ddl_Client_Email.SelectedValue.ToString()));
                    ht.Add("@Email_Status", "False");
                    ht.Add("@Revised", "False");
                    ht.Add("@Status", "True");
                    ht.Add("@Inserted_By", userid);


                    dt = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", ht);

                    MessageBox.Show("Record Genrated Sucessfully");
                    Clear();
                }
                else
                {

                    MessageBox.Show("This order Number Cost already Genrated Please Check");


                }
            }
            else if (Validate_Invoice() != false && btn_Save.Text == "Edit order Cost")
            {

                if (txt_Order_Cost.Text != "")
                {

                    Orders_Cost = Convert.ToDecimal(txt_Order_Cost.Text.ToString());

                }
                else
                {
                    Orders_Cost = 0;

                }

              


                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@Trans", "UPDATE");
                ht.Add("@Order_ID", Order_Id);
                ht.Add("@Order_Cost", Orders_Cost);
                ht.Add("@Order_Cost_Date", txt_Order_Cost_Date.Text);
                ht.Add("@Comments", txt_order_comments.Text);
                ht.Add("@Default_Cleint_Email_Id", int.Parse(ddl_Client_Email.SelectedValue.ToString()));
                ht.Add("@Status", "True");
                ht.Add("@Modified_By", userid);

                dt = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", ht);

                MessageBox.Show("Record Updated Sucessfully");
                this.Close();
                Clear();

            }
        }

        private void ddl_Client_Search_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            bind_OrderList();
        }

        private void txt_Production_Date_ValueChanged(object sender, EventArgs e)
        {
            bind_OrderList();
        }
        public void Clear()
        {

            txt_Invoice_Order_Number.Text = "";
            txt_order_comments.Text = "";
            txt_Order_Cost.Text = "";
          
            txt_order_comments.Text = "";
            btn_Save.Text = "Genrate Order Cost";
            txt_Invoice_Order_Number.Visible = true;
            lbl_Enter_Order.Visible = true;

            bind_OrderList();
            Order_Id = 0;
            //load_order_masters();
            //Order_Load();
            txt_Invoice_Order_Number.Focus();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            Clear();
            this.Close();
           
        }

        private void Grid_Invoice_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                Order_Id = int.Parse(Grid_Invoice_Details.Rows[e.RowIndex].Cells[1].Value.ToString());
                load_order_masters();
                Order_Load();
                Load_Order_County_Cost();

                if (userid == 1 || userid == 4 || userid == 99)
                {

                }
                else
                {

                    txt_Order_Cost.Enabled = false;
                    txt_Order_Cost.PasswordChar = '*';
                }
            }
        }

        private void rbtn_Month_Wise_CheckedChanged(object sender, EventArgs e)
        {
            Grid_Invoice_Details.Rows.Clear();
            bind_OrderList();
          

        }

        private void rbtn_Date_Wise_CheckedChanged(object sender, EventArgs e)
        {

            Grid_Invoice_Details.Rows.Clear();
          
            bind_OrderList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             DataView dtsearch = new DataView(dt);

             if (textBox1.Text != "")
             {
                 string search = textBox1.Text;

                 //dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%' or Client_Name like '%" + search.ToString() + "%' or  Sub_ProcessName like '%" + search.ToString() + "%' or Order_Type like '%" + search.ToString() + "%'  or STATECOUNTY like '%" + search.ToString() + "%' or Date like '%" + search.ToString() + "%' or Order_Cost like '%" + search.ToString() + "%' or Order_Cost_Date like '%" + search.ToString() + "%' or  Order_ID like '%" + search.ToString() +"%' or  Sub_ProcessId like '%" + search.ToString() +"%' or  Order_Cost_Id like '%" + search.ToString() + "%'";
                 dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%'";

                 DataTable dts = new DataTable();
                 dts = dtsearch.ToTable();
                 if (dts.Rows.Count > 0)
                 {
                     Grid_Invoice_Details.Rows.Clear();
                     for (int i = 0; i < dts.Rows.Count; i++)
                     {
                         Grid_Invoice_Details.Rows.Add();
                         Grid_Invoice_Details.Rows[i].Cells[0].Value = dts.Rows[i]["Client_Order_Number"].ToString();
                         Grid_Invoice_Details.Rows[i].Cells[1].Value = dts.Rows[i]["Order_ID"].ToString();

                     }
                 }
                 else
                 {

                     Grid_Invoice_Details.Rows.Clear();

                 }

             }
             else
             {
                 bind_OrderList();

             
             
             }



        }

        private void Grid_Invoice_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

       
    }
}
