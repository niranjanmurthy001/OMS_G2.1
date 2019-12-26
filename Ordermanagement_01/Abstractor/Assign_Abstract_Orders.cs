using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Speech.Synthesis;
using System.IO;
using System.IO;
using System.Net;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Globalization;

namespace Ordermanagement_01.Abstractor
{
    public partial class Assign_Abstract_Orders : Form
    {
      
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        DataTable dtselect = new System.Data.DataTable();
        
        DataTable dtchild = new DataTable();
        string Order_Process;
        int Order_Status_Id;
        int Tree_View_UserId;
        int User_id;
        int PausePlay = 0;
        string County_Type,deedchain;
        bool Abstractor_Check;
        // int MouseEnterNode;
        int State, County, Order_Type_Id,Order_Id,User_Role_ID;
        string Client_Order_Number,emailid;
        int abstractor_Id,clientid;
        string Email,Alternative_Email;
        Genral gen = new Genral();
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        DialogResult dialogResult;
        InfiniteProgressBar.frmProgress form = new InfiniteProgressBar.frmProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string Operation;
       
        public Assign_Abstract_Orders(int USER_ID,string ROLE_ID,int ORDER_ID)
        {
            User_id = USER_ID;
            User_Role_ID = int.Parse(ROLE_ID);
         
            Order_Id = ORDER_ID;
            InitializeComponent();
            if (User_Role_ID == 1)
            {
                dbc.BindClientName_rpt(ddl_Client_Name);
            }
            else 
            {

                dbc.BindClientNo(ddl_Client_Name);
            }
            Sub_AddParent();
            if (Order_Id == 0)
            {
                Geridview_Bind_Abstractor_Orders();
            }
            else
            {

                Geridview_Bind_Abstractor_Orders_By_Order_Id();
               Load_Abstractor_By_Order_Wise();
             
            }

        }

        private void Load_Abstractor_By_Order_Wise()
        {

                   grd_order.Rows[0].Cells[0].Value =true;
                   Order_Type_Id = int.Parse(grd_order.Rows[0].Cells[13].Value.ToString());
                    State = int.Parse(grd_order.Rows[0].Cells[14].Value.ToString());
                    County = int.Parse(grd_order.Rows[0].Cells[15].Value.ToString());
                    Order_Id = int.Parse(grd_order.Rows[0].Cells[12].Value.ToString());
                    Client_Order_Number = grd_order.Rows[0].Cells[3].Value.ToString();

                    string sKeyTemp = "User Name";
                    string sTempText = "User Name";
                    TreeView1.Nodes.Clear();

                    AddChilds_On_Cost_TAT(sKeyTemp);
                    Load_Abstractor_Details_On_Cost_Tat();
        }

      
        private void Assign_Abstract_Orders_Load(object sender, EventArgs e)
        {
          
            Color toolover = System.Drawing.ColorTranslator.FromHtml("#6E828E");
            Grid_Abstractor_Cost_Tat.ColumnHeadersDefaultCellStyle.BackColor = toolover;
            Grid_Abstractor_Cost_Tat.EnableHeadersVisualStyles = false;
            Color gridallocated = System.Drawing.ColorTranslator.FromHtml("#197D9A");
            grd_order_Allocated.ColumnHeadersDefaultCellStyle.BackColor = gridallocated;
            grd_order_Allocated.EnableHeadersVisualStyles = false;
        }

        private void Sub_AddParent()
        {

            string sKeyTemp = "User Name";
            string sTempText = "User Name";
            TreeView1.Nodes.Clear();
            // TreeView1.Nodes.Add("User Name", "User Name");
            AddChilds(sKeyTemp);
        }
        private void AddChilds(string sKeyTemp)
        {
            Button nodebutton;


            Hashtable htselect = new Hashtable();
            

            htselect.Add("@Trans", "ABSTRACT_USER_ORDER_COUNT");
            dtselect = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", htselect);
            
            dtchild = gen.FillChildTable();
            for (int i = 0; i < dtchild.Rows.Count; i++)
            {
                TreeView1.Nodes.Add(dtchild.Rows[i]["User_id"].ToString(), dtchild.Rows[i]["User_Name"].ToString());

            }
            foreach (TreeNode myNode in TreeView1.Nodes)
            {
                //myNode.BackColor = Color.White;
            }
        }


        private void AddChilds_On_Cost_TAT(string sKeyTemp)
        {

            Button nodebutton;


            Hashtable htselect = new Hashtable();
            DataTable dtselect = new System.Data.DataTable();

            htselect.Add("@Trans", "ABSTRACT_USER_ORDER_COUNT_WITH_COST_TAT");
            htselect.Add("@State", State);
            htselect.Add("@County",County);
            htselect.Add("@Order_Type_Id",Order_Type_Id);
            dtselect = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", htselect);
          
            Hashtable htchild = new Hashtable();
            
            htchild.Add("@Trans", "GET_ABSTARCT_TO_TREEVIEW");
            htchild.Add("@State", State);
            htchild.Add("@County", County);
            htchild.Add("@Order_Type_Id", Order_Type_Id);
            dtselect = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", htchild);

            TreeView1.Nodes.Clear();
            for (int i = 0; i < dtselect.Rows.Count; i++)
            {
                TreeView1.Nodes.Add(dtselect.Rows[i]["Abstractor_Id"].ToString(), dtselect.Rows[i]["Name"].ToString());

            }
            foreach (TreeNode myNode in TreeView1.Nodes)
            {
                //myNode.BackColor = Color.White;
            }
        }

        private void Geridview_Bind_Abstractor_Orders()
        {
            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();
            htuser.Add("@Trans", "ABSTRACTORS_ALLOCATING_ORDERS");
            dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Order_Count", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            //grd_order.EnableHeadersVisualStyles = false;
            if (dtuser.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_order.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Order_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role_ID == 1)
                    {
                        grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Client_Name"].ToString();
                    }
                    else 
                    {
                        grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Client_Number"].ToString();

                    }
                    if (User_Role_ID == 1)
                    {
                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else 
                    {

                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Type"].ToString();
                    
                    if (dtuser.Rows[i]["Deed_Chain"].ToString() != "" && dtuser.Rows[i]["Deed_Chain"].ToString() == "True")
                    {
                        grd_order.Rows[i].Cells[7].Value = "Yes";
                    }
                    else if (dtuser.Rows[i]["Deed_Chain"].ToString() == "False" || dtuser.Rows[i]["Deed_Chain"].ToString() == "")
                    {
                        grd_order.Rows[i].Cells[7].Value = "No";
                    }
                    grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Order_Progress_ID"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[13].Value = dtuser.Rows[i]["Order_Type_ID"].ToString();
                    grd_order.Rows[i].Cells[14].Value = dtuser.Rows[i]["State_ID"].ToString();
                    grd_order.Rows[i].Cells[15].Value = dtuser.Rows[i]["County_Id"].ToString();
                    grd_order.Rows[i].Cells[16].Value = dtuser.Rows[i]["Client_Id"].ToString();
                    grd_order.Rows[i].Cells[20].Value = dtuser.Rows[i]["Subprocess_Id"].ToString();
                    grd_order.Rows[i].Cells[17].Value = "vendors@drnds.com";

                    if (int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()) == 34 && int.Parse(grd_order.Rows[i].Cells[20].Value.ToString()) == 210)
                    {

                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
               
                lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();


            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.DataSource = null;
                lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }



        }


        private void Geridview_Bind_Abstractor_Orders_By_Order_Id()
        {
            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();
            htuser.Add("@Trans", "ABSTRACTORS_ALLOCATING_ORDERS_BY_ORDER_ID");
          
                htuser.Add("@Order_ID", Order_Id);
            dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Order_Count", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            //grd_order.EnableHeadersVisualStyles = false;
            if (dtuser.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_order.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Order_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role_ID == 1)
                    {
                        grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Client_Name"].ToString();
                    }
                    else 
                    {
                        grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Client_Number"].ToString();
                    }

                    if (User_Role_ID == 1)
                    {
                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else 
                    {

                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();
                    }

                    grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Type"].ToString();

                    if (dtuser.Rows[i]["Deed_Chain"].ToString() != "" && dtuser.Rows[i]["Deed_Chain"].ToString() == "True")
                    {
                        grd_order.Rows[i].Cells[7].Value = "Yes";
                    }
                    else if (dtuser.Rows[i]["Deed_Chain"].ToString() == "False" || dtuser.Rows[i]["Deed_Chain"].ToString() == "")
                    {
                        grd_order.Rows[i].Cells[7].Value = "No";
                    }
                    grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Order_Progress_ID"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[13].Value = dtuser.Rows[i]["Order_Type_ID"].ToString();
                    grd_order.Rows[i].Cells[14].Value = dtuser.Rows[i]["State_ID"].ToString();
                    grd_order.Rows[i].Cells[15].Value = dtuser.Rows[i]["County_Id"].ToString();
                    grd_order.Rows[i].Cells[16].Value = dtuser.Rows[i]["Client_Id"].ToString();
                    grd_order.Rows[i].Cells[20].Value = dtuser.Rows[i]["Subprocess_Id"].ToString();
                    grd_order.Rows[i].Cells[17].Value = "vendors@drnds.com";

                    if (int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()) == 34 && int.Parse(grd_order.Rows[i].Cells[20].Value.ToString()) == 210)
                    {

                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
                    }
                }

                lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();


            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.DataSource = null;
                lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }



        }
        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    //for (int i = 0; i < grd_order.Rows.Count; i++)
                    //{
                    //    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    //    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    //    if (isChecked == true)
                    //    {

                    Order_Type_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());
                    State = int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString());
                    County = int.Parse(grd_order.Rows[e.RowIndex].Cells[15].Value.ToString());
                    Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString());
                    Client_Order_Number = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();

                    string sKeyTemp = "User Name";
                    string sTempText = "User Name";
                    TreeView1.Nodes.Clear();

                    AddChilds_On_Cost_TAT(sKeyTemp);
                    Load_Abstractor_Details_On_Cost_Tat();


                    //    }
                    //}

                }
                else if (e.ColumnIndex == 3)
                {



                    form_loader.Start_progres();
                    //cProbar.startProgress();
                    Order_Type_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());
                    State = int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString());
                    County = int.Parse(grd_order.Rows[e.RowIndex].Cells[15].Value.ToString());
                    Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString());
                    Client_Order_Number = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();



                    Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(Order_Id, User_id, Convert.ToString(User_Role_ID),"");
                    OrderEntry.Show();

                    //cProbar.stopProgress();
                }
                else if (e.ColumnIndex == 18)//preview option
                {
                    try
                    {
                        if (grd_order.Rows[e.RowIndex].Cells[17].Value.ToString() == "vendors@drnds.com")
                        {
                            if (TreeView1.SelectedNode.Name != "" || TreeView1.SelectedNode.Name != null)
                            {
                                form_loader.Start_progres();
                                //cProbar.startProgress();
                                Order_Type_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());
                                State = int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString());
                                County = int.Parse(grd_order.Rows[e.RowIndex].Cells[15].Value.ToString());
                                Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString());
                                Client_Order_Number = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                                deedchain = grd_order.Rows[e.RowIndex].Cells[7].Value.ToString();
                                emailid = grd_order.Rows[e.RowIndex].Cells[17].Value.ToString();
                                clientid = int.Parse(grd_order.Rows[e.RowIndex].Cells[16].Value.ToString());
                                grd_order.Rows[e.RowIndex].Cells[2].Value = "DRN-" + Order_Id.ToString();
                                abstractor_Id = int.Parse(TreeView1.SelectedNode.Name);
                                Abstractor_Order_Preview ab_orderpreview = new Abstractor_Order_Preview(Order_Id, abstractor_Id, Client_Order_Number, Order_Type_Id, deedchain, emailid, clientid);
                                ab_orderpreview.Show();


                                //cProbar.stopProgress();
                            }

                            else
                            {

                                MessageBox.Show("Select any one Abstractor");
                            }
                        }
                        else if (grd_order.Rows[e.RowIndex].Cells[17].Value.ToString() == "neworders@abstractshop.com")
                        {
                            if (TreeView1.SelectedNode.Name != "" || TreeView1.SelectedNode.Name != null)
                            {
                                form_loader.Start_progres();
                                //cProbar.startProgress();
                                Order_Type_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());
                                State = int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString());
                                County = int.Parse(grd_order.Rows[e.RowIndex].Cells[15].Value.ToString());
                                Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString());
                                Client_Order_Number = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                                deedchain = grd_order.Rows[e.RowIndex].Cells[7].Value.ToString();
                                emailid = grd_order.Rows[e.RowIndex].Cells[17].Value.ToString();

                                grd_order.Rows[e.RowIndex].Cells[2].Value = "ABS-" + Order_Id.ToString();
                                clientid = int.Parse(grd_order.Rows[e.RowIndex].Cells[16].Value.ToString());
                                abstractor_Id = int.Parse(TreeView1.SelectedNode.Name);
                                Abstractor_Order_Preview ab_orderpreview = new Abstractor_Order_Preview(Order_Id, abstractor_Id, Client_Order_Number, Order_Type_Id, deedchain, emailid, clientid);
                                ab_orderpreview.Show();


                                //cProbar.stopProgress();
                            }

                            else
                            {

                                MessageBox.Show("Select any one Abstractor");
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Select any one abstractor");
                    }
                }
                else if (e.ColumnIndex == 19)//Email sending
                {
                    dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {

                            if (TreeView1.SelectedNode.Name != "")
                            {
                                form_loader.Start_progres();
                                //cProbar.startProgress();
                                abstractor_Id = int.Parse(TreeView1.SelectedNode.Name);
                                Get_Email();

                                Order_Type_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());
                                State = int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString());
                                County = int.Parse(grd_order.Rows[e.RowIndex].Cells[15].Value.ToString());
                                Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString());
                                Client_Order_Number = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                                string Deedchain = grd_order.Rows[e.RowIndex].Cells[7].Value.ToString();
                                emailid = grd_order.Rows[e.RowIndex].Cells[17].Value.ToString();
                                if (Email != "")
                                {
                                    try
                                    {
                                        Send_Email email = new Send_Email(Order_Id, abstractor_Id, Client_Order_Number, Order_Type_Id, Email, Alternative_Email, User_id, Deedchain, emailid);

                                        // email.Show();

                                        Geridview_Bind_Abstractor_Orders();
                                        Sub_AddParent();
                                        // MessageBox.Show("Email Sent");

                                    }
                                    catch (Exception ex)
                                    {

                                        MessageBox.Show(ex.ToString());
                                    }

                                    //--------------For Order history ------------------//
                                    Hashtable hthistroy = new Hashtable();
                                    DataTable dthistory = new DataTable();
                                    hthistroy.Add("@Trans", "INSERT");
                                    hthistroy.Add("@Order_Id", Order_Id);
                                    hthistroy.Add("@Progress_Id", 6);
                                    hthistroy.Add("@Assigned_By", User_id);
                                    hthistroy.Add("@Modification_Type", "Assign to Abstractor");
                                    hthistroy.Add("@Work_Type", 1);
                                    dthistory = dataaccess.ExecuteSP("Sp_Order_History", hthistroy);



                                    if (Order_Id == 0)
                                    {

                                        foreach (Form f1 in Application.OpenForms)
                                        {
                                            if (f1.Name == "ReSearch_Order_Allocate")
                                            {

                                                f1.Close();
                                                break;

                                            }
                                        }

                                        ReSearch_Order_Allocate rs = new ReSearch_Order_Allocate(User_id, User_Role_ID.ToString());
                                        rs.Refresh();
                                        rs.Show();

                                    }

                                }
                                else
                                {

                                    MessageBox.Show("Email id is not avilable");
                                }




                                // cProbar.stopProgress();
                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Select any one abstractor");
                        }
                    }


                }
                else if (e.ColumnIndex == 18)//changing email id value
                {

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }

            }
        }

        public void Load_Abstractor_Details_On_Cost_Tat()
        {

           
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new System.Data.DataTable();
           
            htselect.Add("@Trans", "GET_ABSTRACTOR_COST_TAT");
            htselect.Add("@State", State);
            htselect.Add("@County", County);
            htselect.Add("@Order_Type_Id", Order_Type_Id);
            dtselect.Rows.Clear();
            dtselect = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", htselect);



            Grid_Abstractor_Cost_Tat.EnableHeadersVisualStyles = false;
            Grid_Abstractor_Cost_Tat.Columns[0].Width = 50;
            Grid_Abstractor_Cost_Tat.Columns[1].Width = 200;
            Grid_Abstractor_Cost_Tat.Columns[2].Width = 50;
            Grid_Abstractor_Cost_Tat.Columns[3].Width = 50;
         
            if (dtselect.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Abstractor_Cost_Tat.Rows.Clear();
            
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    Grid_Abstractor_Cost_Tat.Rows.Add();
                    Grid_Abstractor_Cost_Tat.Rows[i].Cells[0].Value = i + 1;
                    Grid_Abstractor_Cost_Tat.Rows[i].Cells[1].Value = dtselect.Rows[i]["Name"].ToString();
                    Grid_Abstractor_Cost_Tat.Rows[i].Cells[2].Value = dtselect.Rows[i]["min_cost"].ToString();
                    Grid_Abstractor_Cost_Tat.Rows[i].Cells[3].Value = dtselect.Rows[i]["min_Tat"].ToString();
                 
                }
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        public void Assign_Orders_ToAbstractor()
        {
            Hashtable htinsertrec = new Hashtable();
            DataTable dtinsertrec = new System.Data.DataTable();


            //DateTime date = new DateTime();
            //date = DateTime.Now;
            //string dateeval = date.ToString("dd/MM/yyyy");
            //string time = date.ToString("hh:mm tt");


            //23-01-2018
            DateTime date = new DateTime();
            DateTime time;
          date= DateTime.Now;
            string dateeval = date.ToString("MM/dd/yyyy");

            htinsertrec.Add("@Trans", "INSERT");
            htinsertrec.Add("@Order_Id", Order_Id);
            htinsertrec.Add("@Abstractor_Id", abstractor_Id);
            htinsertrec.Add("@Order_Status_Id", 17);
            htinsertrec.Add("@Order_Progress_Id", 6);
            htinsertrec.Add("@Assigned_Date", dateeval);
            htinsertrec.Add("@Assigned_By", User_id);
            htinsertrec.Add("@Inserted_By", User_id);
            htinsertrec.Add("@Inserted_date", date);
            htinsertrec.Add("@status", "True");
            dtinsertrec = dataaccess.ExecuteSP("Sp__Abstractor_Order_Assignment", htinsertrec);

            Hashtable htupdate = new Hashtable();
            DataTable dtupdate = new System.Data.DataTable();
            htupdate.Add("@Trans", "UPDATE_STATUS");
            htupdate.Add("@Order_ID",orderid);
            htupdate.Add("@Order_Status", 17);
            htupdate.Add("@Modified_By", User_id);
            htupdate.Add("@Modified_Date", date);
            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
            Hashtable htprogress = new Hashtable();
            DataTable dtprogress = new System.Data.DataTable();
            htprogress.Add("@Trans", "UPDATE_PROGRESS");
            htprogress.Add("@Order_ID", orderid);
            htprogress.Add("@Order_Progress", 6);
            htprogress.Add("@Modified_By", User_id);
            htprogress.Add("@Modified_Date", date);
            dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);



        }


        public void Refresh()
        {
            Sub_AddParent();
            Grid_Abstractor_Cost_Tat.Rows.Clear();
            Grid_Abstractor_Cost_Tat.DataSource = null;
            Geridview_Bind_Abstractor_Orders();
        }


        public void Get_Email()
        {


            Hashtable htdate = new Hashtable();
            System.Data.DataTable dtdate = new System.Data.DataTable();
            htdate.Add("@Trans", "GET_EMAIL");
            htdate.Add("@Abstractor_Id",abstractor_Id);
            dtdate = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htdate);
            if (dtdate.Rows.Count > 0)
            {

                Email = dtdate.Rows[0]["Email"].ToString();
                Alternative_Email = dtdate.Rows[0]["Alternative_Email"].ToString();


            }
            else
            {

                Email = "";
                Alternative_Email = "";
            }


        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreeView1.SelectedNode.Text != "")
            {
                var selectedNode = TreeView1.SelectedNode;

                if (selectedNode.Parent == null)
                {
                    int NODE = int.Parse(TreeView1.SelectedNode.Name);
                    Tree_View_UserId = NODE;
                    // Gridview_Bind_All_Orders();
                   // btn_Allocate.Enabled = true;
                    Gridview_Bind_Orders_Wise_Treeview_Selected();

                }
                else
                {

                    lbl_allocated_user.Text = TreeView1.SelectedNode.Text;
                    lbl_allocated_user.ForeColor = System.Drawing.Color.DeepPink;
                    Tree_View_UserId = int.Parse(TreeView1.SelectedNode.Name);
                    // ViewState["User_Wise_Count"] = lbl_allocated_user.Text;
                    Gridview_Bind_Orders_Wise_Treeview_Selected();
                    // Restrict_Controls();
                    //  btn_Allocate.CssClass = "Windowbutton";
                 

                }
            }
            // GridviewOrderUrgent();
            lbl_allocated_user.Text = TreeView1.SelectedNode.Text;
            lbl_allocated_user.ForeColor = System.Drawing.Color.Black;
            Tree_View_UserId = int.Parse(TreeView1.SelectedNode.Name);
        }

        protected void Gridview_Bind_Orders_Wise_Treeview_Selected()
        {
            if (Tree_View_UserId.ToString() != "")
            {

                Hashtable htuser = new Hashtable();
                DataTable dtuser = new System.Data.DataTable();
                htuser.Add("@Trans", "GET_ABSTRACTOR_ORDER_ABSTRACTOR_WISE");
                htuser.Add("@Abstractor_Id", Tree_View_UserId);
                //   htuser.Add("@Subprocess_id", Sub_ProcessName);

                dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Order_Count", htuser);
                grd_order_Allocated.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                grd_order_Allocated.EnableHeadersVisualStyles = false;
                grd_order_Allocated.Columns[0].Width = 35;
                grd_order_Allocated.Columns[1].Width = 50;
                grd_order_Allocated.Columns[2].Width = 100;
                grd_order_Allocated.Columns[3].Width = 100;
                grd_order_Allocated.Columns[4].Width = 120;
                grd_order_Allocated.Columns[5].Width = 100;
                grd_order_Allocated.Columns[6].Width = 100;
                grd_order_Allocated.Columns[7].Width = 80;
                grd_order_Allocated.Columns[9].Width = 100;
                grd_order_Allocated.Columns[10].Width = 80;
                grd_order_Allocated.Columns[12].Width = 80;
                if (dtuser.Rows.Count > 0)
                {
                    grd_order_Allocated.Rows.Clear();

                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order_Allocated.Rows.Add();
                        grd_order_Allocated.Rows[i].Cells[1].Value = i + 1;
                        if (User_Role_ID == 1)
                        {
                            grd_order_Allocated.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Name"].ToString();
                        }
                        else 
                        {

                            grd_order_Allocated.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Number"].ToString();
                        }

                        if (User_Role_ID == 1)
                        {
                            grd_order_Allocated.Rows[i].Cells[3].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else 
                        {
                            grd_order_Allocated.Rows[i].Cells[3].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();
                        
                        }
                        grd_order_Allocated.Rows[i].Cells[4].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                        grd_order_Allocated.Rows[i].Cells[5].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                        grd_order_Allocated.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        grd_order_Allocated.Rows[i].Cells[7].Value = dtuser.Rows[i]["Date"].ToString();
                        grd_order_Allocated.Rows[i].Cells[8].Value = dtuser.Rows[i]["Name"].ToString();
                        grd_order_Allocated.Rows[i].Cells[9].Value = dtuser.Rows[i]["Order_Status"].ToString();
                        grd_order_Allocated.Rows[i].Cells[10].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order_Allocated.Rows[i].Cells[11].Value = dtuser.Rows[i]["Abstractor_Id"].ToString();
                        grd_order_Allocated.Rows[i].Cells[12].Value = dtuser.Rows[i]["Progress_Status"].ToString();
                       // grd_order_Allocated.Rows[i].Cells[13].Value = dtuser.Rows[i]["County_Id"].ToString();
                     //   grd_order_Allocated.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.DarkCyan;

                    }
                }
                else
                {

                    grd_order_Allocated.DataSource = null;
                    grd_order_Allocated.Rows.Clear();

                }

            }
            // GridviewOrderUrgent();
        }

       

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Refresh();
            Order_Id = 1;
        }

        private void grd_order_Allocated_CellClick(object sender, DataGridViewCellEventArgs e)
        {
               if (e.ColumnIndex == 4)
            {



                form_loader.Start_progres();
                //cProbar.startProgress();

                Order_Id = int.Parse(grd_order_Allocated.Rows[e.RowIndex].Cells[10].Value.ToString());
          

              

                Ordermanagement_01.Abstractor.Abstractor_Order_View OrderEntry = new Ordermanagement_01.Abstractor.Abstractor_Order_View(Order_Id,User_id,User_Role_ID.ToString());
                OrderEntry.Show();

                //cProbar.stopProgress();


            }
        }

        private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (ddl_Client_Name.SelectedIndex > 0)
            {
                if (User_Role_ID == 1)
                {
                    dbc.BindSubProcessName(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                }
                else
                {

                    dbc.BindSubProcessNumber(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                }

                Hashtable htuser = new Hashtable();
                DataTable dtuser = new System.Data.DataTable();

                if (ddl_Client_SubProcess.SelectedIndex == 0)
                {
                    htuser.Add("@Trans", "ABSTRACTORS_ALLOCATING_ORDERS_CLIENT_WISE");
                }
                else if (ddl_Client_SubProcess.SelectedIndex > 0)
                {

                    htuser.Add("@Trans", "ABSTRACTORS_ALLOCATING_ORDERS_SubProce_WISE");
                    htuser.Add("@Sub_ProcessId", int.Parse(ddl_Client_SubProcess.SelectedValue.ToString()));
                }
                htuser.Add("@Client_id", int.Parse(ddl_Client_Name.SelectedValue.ToString()));

                dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Order_Count", htuser);
                grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                //grd_order.EnableHeadersVisualStyles = false;
                
                if (dtuser.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                        if (User_Role_ID == 1)
                        {
                            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Name"].ToString();
                        }
                        else
                        {
                            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Number"].ToString();
                        }
                        if (User_Role_ID == 1)
                        {
                            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else 
                        {
                            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();
                        
                        }
                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        
                        if (dtuser.Rows[i]["Deed_Chain"].ToString() != "" && dtuser.Rows[i]["Deed_Chain"].ToString() == "True")
                        {
                            grd_order.Rows[i].Cells[7].Value = "Yes";
                        }
                        else if (dtuser.Rows[i]["Deed_Chain"].ToString() == "False" || dtuser.Rows[i]["Deed_Chain"].ToString() == "")
                        {
                            grd_order.Rows[i].Cells[7].Value = "No";
                        }
                        grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Order_Progress_ID"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dtuser.Rows[i]["Order_Type_ID"].ToString();
                        grd_order.Rows[i].Cells[14].Value = dtuser.Rows[i]["State_ID"].ToString();
                        grd_order.Rows[i].Cells[15].Value = dtuser.Rows[i]["County_Id"].ToString();

                        grd_order.Rows[i].Cells[16].Value = dtuser.Rows[i]["Client_Id"].ToString();
                        grd_order.Rows[i].Cells[20].Value = dtuser.Rows[i]["Subprocess_Id"].ToString();
                    

                        if (int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()) == 34 && int.Parse(grd_order.Rows[i].Cells[20].Value.ToString()) == 210)
                        {

                            grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
                        }
                    }
                    lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
                }
                else
                {
                    grd_order.Rows.Clear();
                    grd_order.DataSource = null;
                    lbl_Total_Orders.Text = "0";
                    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                    //grd_Admin_orders.DataBind();
                }

            }
            else if(ddl_Client_Name.SelectedIndex==0)
            {

                Hashtable htuser = new Hashtable();
                DataTable dtuser = new System.Data.DataTable();

           
                htuser.Add("@Trans", "ABSTRACTORS_ALLOCATING_ORDERS");
                 
                
                dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Order_Count", htuser);
                grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                //grd_order.EnableHeadersVisualStyles = false;
                if (dtuser.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                        if (User_Role_ID == 1)
                        {
                            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Name"].ToString();
                        }
                        else 
                        {
                            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Number"].ToString();
                        }
                        if (User_Role_ID == 1)
                        {
                            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else 
                        {
                            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();
                        
                        }
                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                        if (dtuser.Rows[i]["Deed_Chain"].ToString() != "" && dtuser.Rows[i]["Deed_Chain"].ToString() == "True")
                        {
                            grd_order.Rows[i].Cells[7].Value = "Yes";
                        }
                        else if (dtuser.Rows[i]["Deed_Chain"].ToString() == "False" || dtuser.Rows[i]["Deed_Chain"].ToString() == "")
                        {
                            grd_order.Rows[i].Cells[7].Value = "No";
                        }
                        grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Order_Progress_ID"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_Type_ID"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dtuser.Rows[i]["State_ID"].ToString();
                        grd_order.Rows[i].Cells[14].Value = dtuser.Rows[i]["County_Id"].ToString();
                    }
                    lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
                }
                else
                {
                    grd_order.Rows.Clear();
                    grd_order.DataSource = null;
                    lbl_Total_Orders.Text = "0";
                    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                    //grd_Admin_orders.DataBind();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Send_Email email = new Send_Email(0, 0, "0", 0, "0","0", 0);
            //email.Show();
           // Sending_Email();

        }
        public void Sending_Email()
        {

            using (MailMessage mm = new MailMessage())
            {
                try
                {

                    mm.From = new MailAddress("niranjanmurthy@drnds.com");

                    mm.To.Add("niranjanmurthy@drnds.com");
                    //mm.To.Add("niranjanmurthy@drnds.com");
                    //txt_Subject.Text = "Abstractor Report - " + Client_Order_no + "";
                    //mm.Subject = txt_Subject.Text;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Subject: " + "Sample" + "" + Environment.NewLine);


                    //String str = sb.ToString();
                    //string Message_Body = str.ToString();
                    //mm.Body = Message_Body;


                  //  string Path1 = @"\\192.168.12.33\oms-reports\AbstractorReport.pdf";


                  //  MemoryStream ms = new MemoryStream(File.ReadAllBytes(Path1));

                  //  string Attachment_Name = "Some" + '-' + "123" + ".pdf";

                  //  mm.Attachments.Add(new System.Net.Mail.Attachment(ms, Attachment_Name.ToString()));

                    mm.IsBodyHtml = false;

                    StringBuilder messageBuilder = new StringBuilder(2000);
                    string header = "Title";

                    header = string.Format(@"\b {0}\b0", header);
                   

                    messageBuilder.Append("Hello:");
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append("Please consider this email as formal request to provide the title search." + Environment.NewLine);
                    messageBuilder.Append("summary as per the product definition in the attachment");
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append("Order Summary:");
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append("Order:" + "dfgdsfgdsfg"+ Environment.NewLine);
                    messageBuilder.Append("Order Type: "+ header.ToString() + Environment.NewLine);
                    messageBuilder.Append("County: " + ""+ Environment.NewLine);
                    messageBuilder.Append("Property Address: " + "" + Environment.NewLine);
                    messageBuilder.Append("Owner Name: " + "" + Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append("• Send completed search packages to: Searches@drnds.com." + Environment.NewLine);
                    messageBuilder.Append("• Production related clarifications email to:vendors@drnds.com." + Environment.NewLine);
                    messageBuilder.Append("• All invoices related queries email to VendorInvoices@drnds.com" + Environment.NewLine);

                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append("Thank you" + Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append("Order Entry Team" + Environment.NewLine);
                    messageBuilder.Append("DRN Definite Solutions" + Environment.NewLine);
                    messageBuilder.Append("Corp: 3240 East State Street Ext Hamilton, NJ 08619" + Environment.NewLine);
                    messageBuilder.Append("Direct: Office: 1- (443)-221-4551|Fax:(760)-280-6000" + Environment.NewLine);
                    messageBuilder.Append("Vendors@drnds.com | www.DRNDS.com" + Environment.NewLine);



                    messageBuilder.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
                    messageBuilder.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
                    messageBuilder.Append("-------------------------------------------------------------------------" + Environment.NewLine);

                    messageBuilder.Append("This e-mail and any files transmitted with it are for the sole use of the intended" + Environment.NewLine);
                    messageBuilder.Append("recipient(s) and may contain confidential and privileged information. If you are not the " + Environment.NewLine);
                    messageBuilder.Append("intended recipient, please contact the sender by reply e-mail and destroy all copies of " + Environment.NewLine);
                    messageBuilder.Append("the original message.  Any unauthorized review, use, disclosure, dissemination, " + Environment.NewLine);
                    messageBuilder.Append("forwarding, printing or copying of this email or any action taken in reliance on this e-mail  " + Environment.NewLine);
                    messageBuilder.Append("is strictly prohibited and may be unlawful. " + Environment.NewLine);

                    messageBuilder.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
                    messageBuilder.Append("----------------------------------------------------------------------------------------------" + Environment.NewLine);
                    messageBuilder.Append("-------------------------------------------------------------------------" + Environment.NewLine);









                    string Message_Body = messageBuilder.ToString();
                    mm.Body = Message_Body;
                    SmtpClient smtp = new SmtpClient();
                    // smtp.Host = "smtp.gmail.com";
                    smtp.Host = "smtpout.secureserver.net";
                    // smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("vendors@drnds.com", "123ven");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    //smtp.Port = 587;
                    smtp.Port = 80;
                    smtp.Send(mm);
                    
                    //Assign_Orders_ToAbstractor();
                    //Update_Abstractor_Order_Status();

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return;

                }
            }
        }

        private void ddl_Client_SubProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
               if(ddl_Client_SubProcess.SelectedIndex>0)
               {

                Hashtable htuser = new Hashtable();
                DataTable dtuser = new System.Data.DataTable();

               

                    htuser.Add("@Trans", "ABSTRACTORS_ALLOCATING_ORDERS_SubProce_WISE");
                    htuser.Add("@Sub_ProcessId", int.Parse(ddl_Client_SubProcess.SelectedValue.ToString()));
                
                htuser.Add("@Client_id", int.Parse(ddl_Client_Name.SelectedValue.ToString()));

                dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Order_Count", htuser);
                grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                //grd_order.EnableHeadersVisualStyles = false;
                if (dtuser.Rows.Count > 0)
                {
                    //ex2.Visible = true;
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                        if (User_Role_ID == 1)
                        {
                            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Name"].ToString();
                        }
                        else 
                        {
                            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Number"].ToString();

                        } 
                        if (User_Role_ID == 1)
                        {
                            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else 
                        {
                            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();

                        }
                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        
                        if (dtuser.Rows[i]["Deed_Chain"].ToString() != "" && dtuser.Rows[i]["Deed_Chain"].ToString() == "True")
                        {
                            grd_order.Rows[i].Cells[7].Value = "Yes";
                        }
                        else if (dtuser.Rows[i]["Deed_Chain"].ToString() == "False" || dtuser.Rows[i]["Deed_Chain"].ToString() == "")
                        {
                            grd_order.Rows[i].Cells[7].Value = "No";
                        }
                        grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Order_Progress_ID"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dtuser.Rows[i]["Order_Type_ID"].ToString();
                        grd_order.Rows[i].Cells[14].Value = dtuser.Rows[i]["State_ID"].ToString();
                        grd_order.Rows[i].Cells[15].Value = dtuser.Rows[i]["County_Id"].ToString();


                        grd_order.Rows[i].Cells[16].Value = dtuser.Rows[i]["Client_Id"].ToString();
                        grd_order.Rows[i].Cells[20].Value = dtuser.Rows[i]["Subprocess_Id"].ToString();


                        if (int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()) == 34 && int.Parse(grd_order.Rows[i].Cells[20].Value.ToString()) == 210)
                        {

                            grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
                        }
                    }
                    lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
                }
                else
                {
                    grd_order.Rows.Clear();
                    grd_order.DataSource = null;
                    lbl_Total_Orders.Text = "0";
                    //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                    //grd_Admin_orders.DataBind();
                }

            }
        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;

            foreach (DataGridViewRow row in grd_order.Rows)
            {
                if (txt_SearchOrdernumber.Text != "" && row.Cells[2].Value.ToString().StartsWith(txt_SearchOrdernumber.Text, true, CultureInfo.InvariantCulture) || row.Cells[3].Value.ToString().StartsWith(txt_SearchOrdernumber.Text, true, CultureInfo.InvariantCulture))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }

        private void txt_SearchAllocated_TextChanged(object sender, EventArgs e)
        {
            txt_SearchAllocated.ForeColor = Color.Black;
            foreach (DataGridViewRow row in grd_order_Allocated.Rows)
            {
                if(txt_SearchAllocated.Text!="")
                {
                    if (txt_SearchAllocated.Text != "" && row.Cells[2].Value.ToString().StartsWith(txt_SearchAllocated.Text, true, CultureInfo.InvariantCulture))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void txt_SearchAllocated_Click(object sender, EventArgs e)
        {
            txt_SearchAllocated.ForeColor = Color.Black;
            txt_SearchAllocated.Text = "";
        }

        private void grd_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_SearchAbs_Validated(object sender, EventArgs e)
        {

        }

        private void txt_SearchAbs_TextChanged(object sender, EventArgs e)
        {
            if (txt_SearchAbs.Text != "Search by Abstractor...")
            {
                DataView dtsearch1 = new DataView(dtchild);
                DataView dtsearch = new DataView(dtselect);

                if (dtselect.Rows.Count > 0)
                {
                    dtsearch.RowFilter = "Name like '%" + txt_SearchAbs.Text.ToString() + "%'";
                    DataTable dt = new DataTable();

                    dt = dtsearch.ToTable();

                    TreeView1.Nodes.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeView1.Nodes.Add(dt.Rows[i]["User_id"].ToString(), dt.Rows[i]["User_Name"].ToString());
                    }
                }
                else if (dtchild.Rows.Count > 0)
                {
                    dtsearch1.RowFilter = "User_Name like '%" + txt_SearchAbs.Text.ToString() + "%'";
                    DataTable dt1 = new DataTable();

                    dt1 = dtsearch1.ToTable();

                    TreeView1.Nodes.Clear();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        TreeView1.Nodes.Add(dt1.Rows[i]["User_id"].ToString(), dt1.Rows[i]["User_Name"].ToString());
                    }
                }
            }
        }

        private void txt_SearchAbs_MouseEnter(object sender, EventArgs e)
        {
            if (txt_SearchAbs.Text == "Search by Abstractor...")
            {
                txt_SearchAbs.Text = "";
                txt_SearchAbs.ForeColor = Color.Black;
            }
        }

        private void grd_order_Allocated_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Move_To_Inhouse_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_order.Rows.Count; i++)
            {

                bool chkId = (bool)grd_order[0, i].FormattedValue;

                if (chkId == true)
                {
                    string lbl_Order_Id = grd_order.Rows[i].Cells[12].Value.ToString();

                    Hashtable htstatus = new Hashtable();
                    DataTable dtstatus = new System.Data.DataTable();
                    htstatus.Add("@Trans", "UPDATE_STATUS");
                    htstatus.Add("@Order_ID", lbl_Order_Id);
                    htstatus.Add("@Order_Status", 2);
                    htstatus.Add("@Modified_By", User_id);
                    htstatus.Add("@Modified_Date", DateTime.Now);
                    dtstatus = dataaccess.ExecuteSP("Sp_Order", htstatus);


                    Hashtable htupdate_ABS = new Hashtable();
                    DataTable dtupdate_ABS = new System.Data.DataTable();
                    htupdate_ABS.Add("@Trans", "UPDATE_ABSTRACTOR");
                    htupdate_ABS.Add("@Order_ID", lbl_Order_Id);
                    htupdate_ABS.Add("@Order_Progress", 8);
                    htupdate_ABS.Add("@Modified_By", User_id);
                    htupdate_ABS.Add("@Modified_Date", DateTime.Now);
                    dtupdate_ABS = dataaccess.ExecuteSP("Sp_Order", htupdate_ABS);




                    Hashtable htupdate = new Hashtable();
                    DataTable dtupdate = new System.Data.DataTable();
                    htupdate.Add("@Trans", "UPDATE_ORDER_ASSIGN_TYPE");
                    htupdate.Add("@Order_ID", lbl_Order_Id);
                    htupdate.Add("@Order_Assign_Type", 4);
                    htupdate.Add("@Modified_By", User_id);
                    htupdate.Add("@Modified_Date", DateTime.Now);
                    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);


                    /*-----------------Order History---------------------------*/
                    Hashtable hthistroy = new Hashtable();
                    DataTable dthistroy = new DataTable();
                    hthistroy.Add("@Trans", "INSERT");
                    hthistroy.Add("@Order_Id", lbl_Order_Id);
                    //hthistroy.Add("@User_Id", Tree_View_UserId);
                    hthistroy.Add("@Status_Id", 2);
                    hthistroy.Add("@Progress_Id", 8);
                    hthistroy.Add("@Assigned_By", User_id);
                    hthistroy.Add("@Modification_Type", "Abstractor order Reallocated");
                    hthistroy.Add("@Work_Type", 1);
                    dthistroy = dataaccess.ExecuteSP("Sp_Order_History", hthistroy);

                 
                }


            }

            Geridview_Bind_Abstractor_Orders();
        }

        private void btn_Move_To_Research_Click(object sender, EventArgs e)
        {
            int Check_Count = 0;
            for (int i = 0; i < grd_order.Rows.Count; i++)
            {
                bool isChecked = (bool)grd_order[0, i].FormattedValue;


                if (isChecked == true)
                {
                    Check_Count = 1;
                    string lbl_Order_Id = grd_order.Rows[i].Cells[12].Value.ToString();


                    Hashtable htupdate = new Hashtable();
                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                    htupdate.Add("@Trans", "UPDATE_STATUS");
                    htupdate.Add("@Order_ID", lbl_Order_Id);
                    htupdate.Add("@Order_Status", 25);
                    htupdate.Add("@Modified_By", User_id);
                    htupdate.Add("@Modified_Date", DateTime.Now);
                    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                    Hashtable htupdate_Prog = new Hashtable();
                    System.Data.DataTable dtupdate_Prog = new System.Data.DataTable();
                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                    htupdate_Prog.Add("@Order_ID", lbl_Order_Id);
                    htupdate_Prog.Add("@Order_Progress", 8);
                    htupdate_Prog.Add("@Modified_By", User_id);
                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);

                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                    //OrderHistory
                    Hashtable ht_Order_History = new Hashtable();
                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                    ht_Order_History.Add("@Trans", "INSERT");
                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                    //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                    ht_Order_History.Add("@Status_Id", 25);
                    ht_Order_History.Add("@Progress_Id", 8);
                    ht_Order_History.Add("@Assigned_By", User_id);
                    ht_Order_History.Add("@Work_Type", 1);
                    ht_Order_History.Add("@Modification_Type", "Order Moved to ReSearch Order Queue");
                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                }



            }

            if (Check_Count == 1)
            {

                MessageBox.Show("Order were moved to Research Order Queue");
                
                    Geridview_Bind_Abstractor_Orders();
                
                
            }
        }
       
    }
   
}
