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
using System.Globalization;


namespace Ordermanagement_01.Vendors
{
    public partial class Vendors_Order_Move : Form
    {
        SpeechSynthesizer reader;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Order_Process;
        int Order_Status_Id;
        int Tree_View_UserId;
        int User_id;
        int PausePlay = 0;
        string County_Type;
        bool Abstractor_Check;
        // int MouseEnterNode;
        Genral gen = new Genral();
        int State, County, Order_Type_Id, Order_Id, User_Role_ID;
        string Client_Order_Number;
        int abstractor_Id;
        string Email;
        string Operation;
        public Vendors_Order_Move(int USER_ID, string ROLE_ID)
        {
            InitializeComponent();
            User_id = USER_ID;
            User_Role_ID = int.Parse(ROLE_ID);
            
            dbc.BindOrderStatus(ddl_Order_Status_Reallocate);

        

        }

        private void Geridview_Bind_Vendor_Orders()
        {


            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();

         
            htuser.Add("@Trans", "GET_VENDORS_RETURNED_ORDER");
            
            dtuser = dataaccess.ExecuteSP("Sp_Vendor_Order_Count", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
         
          
            //grd_order.Columns[14].Width = 62;
            //grd_order.Columns[15].Width = 62;
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
                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else 
                    {
                        grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();
                    
                    }
                    grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Vendor_Name"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Assigned_Date_Time"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Completed_Date"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_ID"].ToString();
                   
                   
                }
            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.DataSource = null;

                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
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
                    btn_Allocate.Enabled = true;
                    // Gridview_Bind_Orders_Wise_Treeview_Selected();

                }
                else
                {

                    lbl_allocated_user.Text = TreeView1.SelectedNode.Text;
                    lbl_allocated_user.ForeColor = System.Drawing.Color.DeepPink;
                    Tree_View_UserId = int.Parse(TreeView1.SelectedNode.Name);
                    // ViewState["User_Wise_Count"] = lbl_allocated_user.Text;
                    //   Gridview_Bind_Orders_Wise_Treeview_Selected();
                    // Restrict_Controls();
                    //  btn_Allocate.CssClass = "Windowbutton";
                    btn_Allocate.Enabled = true;


                }
            }
            // GridviewOrderUrgent();
            lbl_allocated_user.Text = TreeView1.SelectedNode.Text;
            lbl_allocated_user.ForeColor = System.Drawing.Color.Black;
            Tree_View_UserId = int.Parse(TreeView1.SelectedNode.Name);
        }

        private void Vendors_Order_Move_Load(object sender, EventArgs e)
        {
            Sub_AddParent();
            grd_order.AllowUserToAddRows = false;
            Rb_Task_CheckedChanged(sender, e);
            Geridview_Bind_Vendor_Orders();
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
            DataTable dtselect = new System.Data.DataTable();

            htselect.Add("@Trans", "ALL_ORDER_ALLOCATE");
            // htselect.Add("@Subprocess_id", Subprocess_id);
            dtselect = dataaccess.ExecuteSP("Sp_Orders_Que", htselect);
            DataTable dtchild = new DataTable();
            dtchild = gen.FillChildTable();
            for (int i = 0; i < dtchild.Rows.Count; i++)
            {
                TreeView1.Nodes.Add(dtchild.Rows[i]["User_ID"].ToString(), dtchild.Rows[i]["User_Name"].ToString());

            }
            foreach (TreeNode myNode in TreeView1.Nodes)
            {
                //myNode.BackColor = Color.White;
            }
        }

        private void Rb_Task_CheckedChanged(object sender, EventArgs e)
        {


            if (Rb_Task.Checked == true)
            {

                TreeView1.Visible = false;

                rb_Users.Checked = false;

            }

        }

        private void rb_Users_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Users.Checked == true)
            {
                Rb_Task.Checked = false;
                TreeView1.Visible = true;

            }
        }

        private void btn_Allocate_Click(object sender, EventArgs e)
        {
            string message = "Are You Proceed?";
            string title = "Submitting";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                if (Rb_Task.Checked == true)
                {
                    int CheckedCount1 = 0;
                    if (ddl_Order_Status_Reallocate.SelectedIndex > 0)
                    {

                        for (int i = 0; i < grd_order.Rows.Count; i++)
                        {
                            bool isChecked = (bool)grd_order[0, i].FormattedValue;

                            // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                            //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                            if (isChecked == true)
                            {
                                CheckedCount1 = 1;
                                string lbl_Order_Id = grd_order.Rows[i].Cells[12].Value.ToString();
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

                                if (ddl_Order_Status_Reallocate.SelectedIndex > 0)
                                {

                                    Order_Status_Id = int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString());
                                }



                                Hashtable htupdate = new Hashtable();
                                DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_STATUS");
                                htupdate.Add("@Order_ID", lbl_Order_Id);
                                htupdate.Add("@Order_Status", Order_Status_Id);
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);



                                Hashtable htprogress = new Hashtable();
                                DataTable dtprogress = new System.Data.DataTable();
                                htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                htprogress.Add("@Order_ID", lbl_Order_Id);
                                htprogress.Add("@Order_Progress", 8);
                                htprogress.Add("@Modified_By", User_id);
                                htprogress.Add("@Modified_Date", date);
                                dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);

                                /*-----------------------ORder history---------------------*/
                                Hashtable hthistroy = new Hashtable();
                                DataTable dthistroy = new DataTable();
                                hthistroy.Add("@Trans", "INSERT");
                                hthistroy.Add("@Order_Id", lbl_Order_Id);
                                // hthistroy.Add("@User_Id", Tree_View_UserId);
                                hthistroy.Add("@Status_Id", ddl_Order_Status_Reallocate.SelectedValue);
                                hthistroy.Add("@Progress_Id", 8);
                                hthistroy.Add("@Assigned_By", User_id);
                                hthistroy.Add("@Modification_Type", "Order Moved from Vendor return queue to "+ddl_Order_Status_Reallocate.Text+"");
                                hthistroy.Add("@Work_Type", 1);
                                dthistroy = dataaccess.ExecuteSP("Sp_Order_History", hthistroy);

                                //TreeView1.SelectedNode.Value =ViewState["User_Id"].ToString();
                                //   lbl_allocated_user.Text = ViewState["User_Wise_Count"].ToString();
                                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                            }

                        }
                        if (CheckedCount1 >= 1)
                        {
                            Geridview_Bind_Vendor_Orders();

                            //  Restrict_Controls();
                            Sub_AddParent();
                            // string task = ddl_Order_Status_Reallocate.SelectedItem.ToString();
                            MessageBox.Show("Order Moved Successfully");

                        }



                    }
                    else
                    {

                        MessageBox.Show("Please Select Task");

                    }
                }
                else if (rb_Users.Checked == true && validatetree() != false)
                {
                    int CheckedCount = 0;
                    if (Tree_View_UserId != 0)
                    {


                        int allocated_Userid = Tree_View_UserId;



                        for (int i = 0; i < grd_order.Rows.Count; i++)
                        {
                            bool isChecked = (bool)grd_order[0, i].FormattedValue;

                            // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                            //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                            if (isChecked == true)
                            {
                                CheckedCount = 1;
                                string lbl_Order_Id = grd_order.Rows[i].Cells[12].Value.ToString();
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



                                if (ddl_Order_Status_Reallocate.SelectedIndex > 0)
                                {

                                    Order_Status_Id = int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString());
                                }

                                htinsertrec.Add("@Trans", "INSERT");
                                htinsertrec.Add("@Order_Id", lbl_Order_Id);
                                htinsertrec.Add("@User_Id", allocated_Userid);
                                htinsertrec.Add("@Order_Status_Id", Order_Status_Id);
                                htinsertrec.Add("@Order_Progress_Id", 6);
                                htinsertrec.Add("@Assigned_Date", dateeval);
                                htinsertrec.Add("@Assigned_By", User_id);
                                htinsertrec.Add("@Inserted_By", User_id);
                                htinsertrec.Add("@Inserted_date", date);
                                htinsertrec.Add("@status", "True");
                                dtinsertrec = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                Hashtable htupdate = new Hashtable();
                                DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_STATUS");
                                htupdate.Add("@Order_ID", lbl_Order_Id);
                                htupdate.Add("@Order_Status", Order_Status_Id);
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);
                                Hashtable htprogress = new Hashtable();
                                DataTable dtprogress = new System.Data.DataTable();
                                htprogress.Add("@Trans", "UPDATE_PROGRESS");
                                htprogress.Add("@Order_ID", lbl_Order_Id);
                                htprogress.Add("@Order_Progress", 6);
                                htprogress.Add("@Modified_By", User_id);
                                htprogress.Add("@Modified_Date", date);
                                dtprogress = dataaccess.ExecuteSP("Sp_Order", htprogress);

                             
                         

                                /*-----------------------ORder history---------------------*/
                                Hashtable hthistroy = new Hashtable();
                                DataTable dthistroy = new DataTable();
                                hthistroy.Add("@Trans", "INSERT");
                                hthistroy.Add("@Order_Id", lbl_Order_Id);
                                hthistroy.Add("@User_Id", Tree_View_UserId);
                                hthistroy.Add("@Status_Id", ddl_Order_Status_Reallocate.SelectedValue);
                                hthistroy.Add("@Progress_Id", 6);
                                hthistroy.Add("@Assigned_By", User_id);
                                hthistroy.Add("@Modification_Type", "Move from Vendor return queue");
                                hthistroy.Add("@Work_Type", 1);
                                dthistroy = dataaccess.ExecuteSP("Sp_Order_History", hthistroy);
                                //TreeView1.SelectedNode.Value =ViewState["User_Id"].ToString();
                                //   lbl_allocated_user.Text = ViewState["User_Wise_Count"].ToString();
                                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                            }


                        }

                    }
                    if (CheckedCount >= 1)
                    {
                        MessageBox.Show("Order Moved to " + TreeView1.SelectedNode.Text + " Successfully");
                        Geridview_Bind_Vendor_Orders();

                    }


                    //  Restrict_Controls();
                    Sub_AddParent();
                }
            }
            else
            {


            }
        }

        public bool validatetree()
        {

            if (ddl_Order_Status_Reallocate.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select task");
                return false;
            }
            if (TreeView1.SelectedNode.Text == "" || TreeView1.SelectedNode.Text == null)
            {
                MessageBox.Show("Please Select Username");
                return false;
            }
            return true;
        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            foreach (DataGridViewRow row in grd_order.Rows)
            {
                if (txt_SearchOrdernumber.Text != "")
                {
                    if (row.Cells[2].Value.ToString().StartsWith(txt_SearchOrdernumber.Text, true, CultureInfo.InvariantCulture))
                    {
                        row.Visible = true;

                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
                else
                {
                    row.Visible = true;
                }

            }
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {

                    int Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString());

                    Vendor_Order_View view = new Vendor_Order_View(Order_Id, User_id, User_Role_ID);
                    view.Show();



                }

            }

        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";

        }

    }
}
